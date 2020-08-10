using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sichem;

namespace StbSharp.StbImage.Generator
{
	class Program
	{
		private static void Write(Dictionary<string, string> input, StringBuilder output)
		{
			foreach (var pair in input)
			{
				output.Append(pair.Value);
			}
		}

		private static string PostProcess(string data)
		{
			data = Utility.ReplaceNativeCalls(data);

			data = data.Replace("((void *)(0))", "null");
			data = data.Replace("(void *)(0)", "null");

			data = data.Replace("][", ", ");

			data = data.Replace("'J', 'F', 'I', 'F'",
				"(byte)'J', (byte)'F', (byte)'I', (byte)'F'");

			data = data.Replace("int has_alpha = (int)(((comp) == (2)) || ((comp) == (4)));",
				"int has_alpha = (((comp) == (2)) || ((comp) == (4)))?1:0;");
			data = data.Replace("*arr?",
				"*arr != null?");
			data = data.Replace("sizeof(int)* * 2",
				"sizeof(int) * 2");
			data = data.Replace("(int)(sizeof((*(data))))",
				"sizeof(byte)");
			data = data.Replace("(int)(sizeof((*(_out_))))",
				"sizeof(byte)");
			data = data.Replace("(int)(sizeof((*(hash_table[h]))))",
				"sizeof(byte*)");
			data = data.Replace("sizeof((hash_table[h, 0]))",
				"sizeof(byte*)");
			data = data.Replace("(byte***)(malloc((ulong)(16384 * sizeof(char**)))))",
				"(byte***)(malloc((ulong)(16384 * sizeof(byte**))))");
			data = data.Replace("(hlist)?",
				"(hlist != null)?");
			data = data.Replace("(hash_table[i])?",
				"(hash_table[i] != null)?");

			data = data.Replace("ushort* bs", "ushort bs0, ushort bs1");
			data = data.Replace("bs[0]", "bs0");
			data = data.Replace("bs[1]", "bs1");

			data = data.Replace("ushort** HTDC, ushort** HTAC", "ushort[,] HTDC, ushort[,] HTAC");
			data = data.Replace("HTDC[0]", "HTDC[0, 0], HTDC[0, 1]");
			data = data.Replace("HTDC[bits[1]]", "HTDC[bits[1], 0], HTDC[bits[1], 1]");
			data = data.Replace("HTDC[bits[1]]", "HTDC[bits[1], 0], HTDC[bits[1], 1]");
			data = data.Replace("stbiw__jpg_writeBits(s, bitBuf, bitCnt, bits);",
				"stbiw__jpg_writeBits(s, bitBuf, bitCnt, bits[0], bits[1]);");
			data = data.Replace("stbiw__jpg_writeBits(s, bitBuf, bitCnt, EOB);",
				"stbiw__jpg_writeBits(s, bitBuf, bitCnt, EOB[0], EOB[1]);");
			data = data.Replace("stbiw__jpg_writeBits(s, bitBuf, bitCnt, M16zeroes);",
				"stbiw__jpg_writeBits(s, bitBuf, bitCnt, M16zeroes[0], M16zeroes[1]);");
			data = data.Replace("stbiw__jpg_writeBits(s, &bitBuf, &bitCnt, fillBits);",
				"stbiw__jpg_writeBits(s, &bitBuf, &bitCnt, fillBits[0], fillBits[1]);");
			data = data.Replace("stbiw__jpg_writeBits(s, bitBuf, bitCnt, HTAC[(nrzeroes << 4) + bits[1]]);",
				"stbiw__jpg_writeBits(s, bitBuf, bitCnt, HTAC[(nrzeroes << 4) + bits[1], 0], HTAC[(nrzeroes << 4) + bits[1], 1]);");

			data = data.Replace("s.func(s.context, (void *)(head0), (int)(sizeof((head0))));",
				"fixed (byte* h = head0) { s.func(s.context, h, head0.Length); }");
			data = data.Replace("(int)(sizeof((YTable)))", "64");
			data = data.Replace("(int)(sizeof((UVTable)))", "64");
			data = data.Replace("(int)(sizeof((head1)))", "24");
			data = data.Replace("s.func(s.context, (void *)(std_dc_luminance_nrcodes + 1), (int)(sizeof((std_dc_luminance_nrcodes)) - 1));",
				"fixed (byte* d = &std_dc_luminance_nrcodes[1]) { s.func(s.context, d, std_dc_chrominance_nrcodes.Length - 1); }");
			data = data.Replace("s.func(s.context, (void *)(std_dc_luminance_values), (int)(sizeof((std_dc_luminance_values))));",
				"fixed (byte* d = std_dc_luminance_values) { s.func(s.context, d, std_dc_chrominance_values.Length); }");

			data = data.Replace("s.func(s.context, (void *)(std_ac_luminance_nrcodes + 1), (int)(sizeof((std_ac_luminance_nrcodes)) - 1));",
				"fixed (byte* a = &std_ac_luminance_nrcodes[1]) { s.func(s.context, a, std_ac_luminance_nrcodes.Length - 1); }");
			data = data.Replace("s.func(s.context, (void *)(std_ac_luminance_values), (int)(sizeof((std_ac_luminance_values))));",
				"fixed (byte* d = std_ac_luminance_values) { s.func(s.context, d, std_ac_chrominance_values.Length); }");

			data = data.Replace("s.func(s.context, (void *)(std_dc_chrominance_nrcodes + 1), (int)(sizeof((std_dc_chrominance_nrcodes)) - 1));",
				"fixed (byte* d = &std_dc_chrominance_nrcodes[1]) { s.func(s.context, d, std_dc_chrominance_nrcodes.Length - 1); }");
			data = data.Replace("s.func(s.context, (void *)(std_dc_chrominance_values), (int)(sizeof((std_dc_chrominance_values))));",
				"fixed (byte* d = std_dc_chrominance_values) { s.func(s.context, d, std_dc_chrominance_values.Length); }");

			data = data.Replace("s.func(s.context, (void *)(std_ac_chrominance_nrcodes + 1), (int)(sizeof((std_ac_chrominance_nrcodes)) - 1));",
				"fixed (byte* a = &std_ac_chrominance_nrcodes[1]) { s.func(s.context, a, std_ac_chrominance_nrcodes.Length - 1); }");
			data = data.Replace("s.func(s.context, (void *)(std_ac_chrominance_values), (int)(sizeof((std_ac_chrominance_values))));",
				"fixed (byte* d = std_ac_chrominance_values) { s.func(s.context, d, std_ac_chrominance_values.Length); }");

			data = data.Replace("s.func(s.context, (void *)(head2), (int)(sizeof((head2))));",
				"fixed (byte* h = head2) { s.func(s.context, h, head2.Length); }");

			return data;
		}


		static void Process()
		{
			var parameters = new ConversionParameters
			{
				InputPath = @"stb_image_write.h",
				Defines = new[]
				{
					"STBI_WRITE_NO_STDIO",
					"STB_IMAGE_WRITE_IMPLEMENTATION",
					"STB_IMAGE_WRITE_STATIC"
				},
				Namespace = "StbImageWriteSharp",
				Class = "StbImageWrite",
				SkipStructs = new[]
				{
					"stbi__write_context"
				},
				SkipGlobalVariables = new[]
				{
					"stbi_write_tga_with_rle"
				},
				SkipFunctions = new[]
				{
					"stbi__start_write_callbacks",
					"stbiw__writefv",
					"stbiw__writef",
					"stbiw__outfile",
					"stbi_write_bmp_to_func",
					"stbi_write_tga_to_func",
					"stbi_write_hdr_to_func",
					"stbi_write_png_to_func",
					"stbi_write_jpg_to_func",
					"stbi_write_hdr_core",
				},
				Classes = new[]
				{
					"stbi__write_context"
				},
				GlobalArrays = new[]
				{
					"lengthc",
					"lengtheb",
					"distc",
					"disteb",
					"crc_table",
					"stbiw__jpg_ZigZag",
					"std_dc_luminance_nrcodes",
					"std_dc_luminance_values",
					"std_ac_luminance_nrcodes",
					"std_ac_luminance_values",
					"std_dc_chrominance_nrcodes",
					"std_dc_chrominance_values",
					"std_ac_chrominance_nrcodes",
					"std_ac_chrominance_values",
					"std_dc_chrominance_nrcodes",
					"std_dc_chrominance_values",
					"YDC_HT",
					"UVDC_HT",
					"YAC_HT",
					"UVAC_HT",
					"YQT",
					"UVQT",
					"aasf",
					"head0",
					"head2"
			}
			};

			var cp = new ClangParser();

			var result = cp.Process(parameters);

			// Write output
			var sb = new StringBuilder();
			sb.AppendLine(string.Format("// Generated by Sichem at {0}", DateTime.Now));
			sb.AppendLine();

			sb.AppendLine("using System;");
			sb.AppendLine("using System.Runtime.InteropServices;");

			sb.AppendLine();

			sb.Append("namespace StbImageWriteSharp\n{\n\t");
			sb.AppendLine("unsafe partial class StbImageWrite\n\t{");

			Write(result.Constants, sb);
			Write(result.GlobalVariables, sb);
			Write(result.Enums, sb);
			Write(result.Structs, sb);
			Write(result.Methods, sb);

			sb.Append("}\n}");
			var data = sb.ToString();

			// Post processing
			Logger.Info("Post processing...");
			data = PostProcess(data);

			File.WriteAllText(@"..\..\..\..\..\src\StbImageWrite.Generated.cs", data);
		}

		static void Main(string[] args)
		{
			try
			{
				Process();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}

			Console.WriteLine("Finished. Press any key to quit.");
			Console.ReadKey();
		}
	}
}