using StbImageSharp;
using StbImageWriteSharp;
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

		private static void WriteOutputImage(string outputFile, OutputType outputType, int width, int height, byte[] bitmap, int jpegQuality)
		{
			Console.WriteLine("Writing {0}", outputFile);
			using (var stream = new MemoryStream())
			{
				var imageWriter = new ImageWriter();
				switch (outputType)
				{
					case OutputType.Jpg:
						imageWriter.WriteJpg(bitmap, width, height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream, jpegQuality);
						break;
					case OutputType.Png:
						imageWriter.WritePng(bitmap, width, height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
						break;
					case OutputType.Tga:
						imageWriter.WriteTga(bitmap, width, height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
						break;
					case OutputType.Bmp:
						imageWriter.WriteBmp(bitmap, width, height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
						break;
					case OutputType.Hdr:
						imageWriter.WriteHdr(bitmap, width, height, StbImageWriteSharp.ColorComponents.RedGreenBlueAlpha, stream);
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
				Console.WriteLine("Usage: Converter.exe <input_file> <output_file> [jpegQuality]");
				return;
			}

			try
			{
				ImageResult image;
				using (var stream = File.OpenRead(args[0]))
				{
					image = ImageResult.FromStream(stream, StbImageSharp.ColorComponents.RedGreenBlueAlpha);
				}

				var outputType = DetermineOutputType(args[1]);

				var jpegQuality = 90;
				if (args.Length > 2)
				{
					jpegQuality = int.Parse(args[2]);
				}

				WriteOutputImage(args[1], outputType, image.Width, image.Height, image.Data, jpegQuality);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}