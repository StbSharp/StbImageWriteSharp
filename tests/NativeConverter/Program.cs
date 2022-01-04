using StbImageSharp;
using StbNative;
using System;
using System.IO;

namespace Converter
{
	static class Program
	{
		private enum OutputType
		{
			Jpg,
			Png,
			Tga,
			Bmp,
			Hdr
		}

		private static OutputType DetermineOutputType(string outputFile)
		{
			var ext = Path.GetExtension(outputFile);
			if (string.IsNullOrEmpty(ext))
			{
				throw new Exception("Output file lacks extension. Hence it is not possible to determine output file type");
			}

			if (ext.StartsWith("."))
			{
				ext = ext.Substring(1);
			}

			ext = ext.ToLower();

			OutputType outputType;
			switch (ext)
			{
				case "jpg":
				case "jpeg":
					outputType = OutputType.Jpg;
					break;
				case "png":
					outputType = OutputType.Png;
					break;
				case "tga":
					outputType = OutputType.Tga;
					break;
				case "bmp":
					outputType = OutputType.Bmp;
					break;
				case "hdr":
					outputType = OutputType.Hdr;
					break;
				default:
					throw new Exception("Output format '" + ext + "' is not supported.");
			}

			return outputType;
		}

		private static void WriteOutputImage(string outputFile, OutputType outputType, 
				int width, int height, int comp, byte[] bitmap, int jpgQuality)
		{
			Console.WriteLine("Writing {0}", outputFile);
			using (var stream = new MemoryStream())
			{
				switch (outputType)
				{
					case OutputType.Jpg:
						Native.save_to_jpg(bitmap, width, height, comp, stream, jpgQuality);
						break;
					case OutputType.Png:
						Native.save_to_stream(bitmap, width, height, comp, 3, stream);
						break;
					case OutputType.Tga:
						Native.save_to_stream(bitmap, width, height, comp, 1, stream);
						break;
					case OutputType.Bmp:
						Native.save_to_stream(bitmap, width, height, comp, 0, stream);
						break;
					case OutputType.Hdr:
						Native.save_to_stream(bitmap, width, height, comp, 2, stream);
						break;
				}

				var save = stream.ToArray();
				File.WriteAllBytes(outputFile, save);
			}
		}


		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("Usage: NativeConverter.exe <input_file> <output_file> [jpgQuality]");
				return;
			}

			try
			{
				ImageResult image;
				using (var stream = File.OpenRead(args[0]))
				{
					image = ImageResult.FromStream(stream);
				}

				var outputType = DetermineOutputType(args[1]);

				var jpgQuality = 90;
				if (args.Length > 2)
				{
					jpgQuality = int.Parse(args[2]);
				}
				WriteOutputImage(args[1], outputType, image.Width, image.Height,
					(int)image.Comp, image.Data, jpgQuality);

				// Load again
				using (var stream = File.OpenRead(args[1]))
				{
					image = ImageResult.FromStream(stream);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}