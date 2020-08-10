using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StbImageSharp;
using StbImageWriteSharp;
using StbNative;

namespace StbSharp.Tests
{
	internal static class Program
	{
		private static int tasksStarted;
		private static int filesProcessed;
		private static int stbSharpWrite;
		private static int stbNativeWrite;

		private delegate void WriteDelegate(byte[] data, int width, int height, StbImageWriteSharp.ColorComponents components, Stream stream);

		private const int LoadTries = 10;

		private static readonly int[] JpgQualities = {1, 4, 8, 16, 25, 32, 50, 64, 72, 80, 90, 100};
		private static readonly string[] FormatNames = {"BMP", "TGA", "HDR", "PNG", "JPG"};

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

				for (var k = 0; k <= 4; ++k)
				{
					Log("Saving as {0} with StbSharp", FormatNames[k]);

					if (k < 4)
					{
						var writer = new ImageWriter();
						WriteDelegate wd = null;
						switch (k)
						{
							case 0:
								wd = writer.WriteBmp;
								break;
							case 1:
								wd = writer.WriteTga;
								break;
							case 2:
								wd = writer.WriteHdr;
								break;
							case 3:
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

						Log("Saving as {0} with Stb.Native", FormatNames[k]);
						BeginWatch(sw);
						byte[] save2;
						using (var stream = new MemoryStream())
						{
							Native.save_to_stream(image.Data, image.Width, image.Height, (int)image.Comp, k, stream);
							save2 = stream.ToArray();
						}

						passed = EndWatch(sw);
						stbNativeWrite += passed;

						Log("Span: {0} ms", passed);
						Log("Stb.Native Size: {0}", save2.Length);

						if (save.Length != save2.Length)
						{
							throw new Exception(string.Format("Inconsistent output size: StbSharp={0}, Stb.Native={1}",
								save.Length, save2.Length));
						}

						for (var i = 0; i < save.Length; ++i)
						{
							if (save[i] != save2[i])
							{
								throw new Exception(string.Format("Inconsistent data: index={0}, StbSharp={1}, Stb.Native={2}",
									i,
									(int) save[i],
									(int) save2[i]));
							}
						}
					}
					else
					{
						for (var qi = 0; qi < JpgQualities.Length; ++qi)
						{
							var quality = JpgQualities[qi];
							Log("Saving as JPG with StbSharp with quality={0}", quality);
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

							Log("Saving as JPG with Stb.Native with quality={0}", quality);
							BeginWatch(sw);
							byte[] save2;
							using (var stream = new MemoryStream())
							{
								Native.save_to_jpg(image.Data, image.Width, image.Height, (int)image.Comp, stream, quality);
								save2 = stream.ToArray();
							}

							passed = EndWatch(sw);
							stbNativeWrite += passed;

							Log("Span: {0} ms", passed);
							Log("Stb.Native Size: {0}", save2.Length);

							if (save.Length != save2.Length)
							{
								throw new Exception(string.Format("Inconsistent output size: StbSharp={0}, Stb.Native={1}",
									save.Length, save2.Length));
							}

							for (var i = 0; i < save.Length; ++i)
							{
								if (save[i] != save2[i])
								{
									throw new Exception(string.Format("Inconsistent data: index={0}, StbSharp={1}, Stb.Native={2}",
										i,
										(int) save[i],
										(int) save2[i]));
								}
							}
						}
					}
				}

				Log("Total StbSharp Write Time: {0} ms", stbSharpWrite);
				Log("Total Stb.Native Write Time: {0} ms", stbNativeWrite);
				Log("GC Memory: {0}", GC.GetTotalMemory(true));
				Log("Native Allocations: {0}", StbImageWriteSharp.MemoryStats.Allocations);

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