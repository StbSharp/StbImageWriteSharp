using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StbImageSharp;
using StbImageWriteSharp;

namespace StbSharp.Tests
{
	internal static class Program
	{
		private static int tasksStarted;
		private static int filesProcessed;
		private static int stbSharpWrite;

		private delegate void WriteDelegate(byte[] data, int width, int height, StbImageWriteSharp.ColorComponents components, Stream stream);

		private static readonly int[] JpgQualities = {1, 4, 8, 16, 25, 32, 50, 64, 72, 80, 90, 100};
		private static readonly string[] FormatNames = {"BMP", "TGA", "PNG", "JPG", "HDR"};

		public static void Log(string message)
		{
			Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " -- " + message);
		}

		public static void Log(string format, params object[] args)
		{
			Log(string.Format(format, args));
		}

		private static void BeginWatch(Stopwatch sw)
		{
			sw.Restart();
		}

		private static int EndWatch(Stopwatch sw)
		{
			sw.Stop();
			return (int) sw.ElapsedMilliseconds;
		}

		public static bool RunTests(string imagesPath)
		{
			var files = Directory.EnumerateFiles(imagesPath, "*.*", SearchOption.AllDirectories).ToArray();
			Log("Files count: {0}", files.Length);

			foreach (var file in files)
			{
				Task.Factory.StartNew(() => { ThreadProc(file); });
				tasksStarted++;
			}

			while (true)
			{
				Thread.Sleep(1000);

				if (tasksStarted == 0)
				{
					break;
				}
			}

			return true;
		}

		private static void EnsureEqual(string path, string outType, ImageResult image1, ImageResult image2,
			bool testData)
		{
			path = Path.GetFileName(path);

			if (image1.Width != image2.Width)
			{
				throw new Exception(string.Format("Inconsistent width('{0}'|{1}): Width1={2}, Width={3}",
					path, outType, image1.Width, image2.Width));
			}

			if (image1.Height != image2.Height)
			{
				throw new Exception(string.Format("Inconsistent height('{0}'|{1}): Height1={2}, Height={3}",
					path, outType, image1.Height, image2.Height));
			}

			if (!testData)
			{
				return;
			}

			if (image1.Comp != image2.Comp)
			{
				throw new Exception(string.Format("Inconsistent comp('{0}'|{1}): Comp1={2}, Comp2={3}",
					path, outType, image1.Comp, image2.Comp));
			}

			for (var i = 0; i < image1.Data.Length; ++i)
			{
				var delta = image1.Data[i] - image2.Data[i];
				if (Math.Abs(delta) > 0)
				{
					throw new Exception(string.Format("Inconsistent data('{0}'|{1}): Index={2}, Byte1={3}, Byte2={4}",
						path, outType, i, image1.Data[i], image2.Data[i]));
				}
			}
		}

		private static void ThreadProc(string f)
		{
			try
			{
				var sw = new Stopwatch();

				if (!f.EndsWith(".bmp") && !f.EndsWith(".jpg") && !f.EndsWith(".png") &&
				    !f.EndsWith(".jpg") && !f.EndsWith(".psd") && !f.EndsWith(".pic") &&
				    !f.EndsWith(".tga"))
				{
					return;
				}

				Log(string.Empty);
				Log("{0} -- #{1}: Loading {2} into memory", DateTime.Now.ToLongTimeString(), filesProcessed, f);
				var data = File.ReadAllBytes(f);
				Log("----------------------------");
				var image = ImageResult.FromMemory(data);

				for (var k = 0; k < FormatNames.Length; ++k)
				{
					var formatName = FormatNames[k];
					Log("Saving as {0}", formatName);

					if (formatName != "JPG")
					{
						var writer = new ImageWriter();
						WriteDelegate wd = null;
						switch (formatName)
						{
							case "BMP":
								wd = writer.WriteBmp;
								break;
							case "TGA":
								wd = writer.WriteTga;
								break;
							case "HDR":
								wd = writer.WriteHdr;
								break;
							case "PNG":
								wd = writer.WritePng;
								break;
						}

						byte[] save;
						BeginWatch(sw);
						using (var stream = new MemoryStream())
						{
							wd(image.Data, image.Width, image.Height, (StbImageWriteSharp.ColorComponents)image.Comp, stream);
							save = stream.ToArray();
						}

						var passed = EndWatch(sw);
						stbSharpWrite += passed;
						Log("Span: {0} ms", passed);
						Log("StbSharp Size: {0}", save.Length);

						// Load back
						var image2 = ImageResult.FromMemory(save);

						var testData = true;
						if (formatName == "HDR" ||
							(formatName == "BMP" && (int)image.Comp <= (int)StbImageSharp.ColorComponents.GreyAlpha))
						{
							// Skip testing, since greyalpha bmp is written in 3 colors
							testData = false;
						}

						EnsureEqual(f, FormatNames[k], image, image2, testData);
					}
					else
					{
						for (var qi = 0; qi < JpgQualities.Length; ++qi)
						{
							var quality = JpgQualities[qi];
							Log("Saving as JPG with quality={0}", quality);
							byte[] save;
							BeginWatch(sw);
							using (var stream = new MemoryStream())
							{
								var writer = new ImageWriter();
								writer.WriteJpg(image.Data, image.Width, image.Height, (StbImageWriteSharp.ColorComponents)image.Comp, stream, quality);
								save = stream.ToArray();
							}

							var passed = EndWatch(sw);
							stbSharpWrite += passed;

							Log("Span: {0} ms", passed);
							Log("StbSharp Size: {0}", save.Length);

							// Load back
							var image2 = ImageResult.FromMemory(save);
							EnsureEqual(f, FormatNames[k] + "/" + quality, image, image2, false);
						}
					}
				}

				Log("Total StbSharp Write Time: {0} ms", stbSharpWrite);
				Log("GC Memory: {0}", GC.GetTotalMemory(true));
				Log("Native Allocations: {0}", MemoryStats.Allocations);

				++filesProcessed;
				Log(DateTime.Now.ToLongTimeString() + " -- " + " Files processed: " + filesProcessed);

			}
			catch (Exception ex)
			{
				Log("Error: " + ex.Message);
			}
			finally
			{
				--tasksStarted;
			}
		}

		public static int Main(string[] args)
		{
			try
			{
				if (args == null || args.Length < 1)
				{
					Console.WriteLine("Usage: StbImageWriteSharp.Testing <path_to_folder_with_images>");
					return 1;
				}

				var start = DateTime.Now;

				var res = RunTests(args[0]);
				var passed = DateTime.Now - start;
				Log("Span: {0} ms", passed.TotalMilliseconds);
				Log(DateTime.Now.ToLongTimeString() + " -- " + (res ? "Success" : "Failure"));

				return res ? 1 : 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}
	}
}