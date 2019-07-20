using System;
using System.Runtime.InteropServices;

namespace StbImageWriteSharp
{
	unsafe partial class StbImageWrite
	{
		public static int stbi_write_png_compression_level = 0;
		
		public static int stbi_write_force_png_filter = 0;
		
		public static int stbi_write_png_compression_level = (int)(8);
		
		public static int stbi__flip_vertically_on_write = (int)(0);
		
		public static int stbi_write_force_png_filter = (int)(-1);
		
		public static byte[] stbiw__jpg_ZigZag = { 0, 1, 5, 6, 14, 15, 27, 28, 2, 4, 7, 13, 16, 26, 29, 42, 3, 8, 12, 17, 25, 30, 41, 43, 9, 11, 18, 24, 31, 40, 44, 53, 10, 19, 23, 32, 39, 45, 52, 54, 20, 22, 33, 38, 46, 51, 55, 60, 21, 34, 37, 47, 50, 56, 59, 61, 35, 36, 48, 49, 57, 58, 62, 63 };
		
		public static void stbi_flip_vertically_on_write(int flag)
		{
			stbi__flip_vertically_on_write = (int)(flag);
		}

		public static void stbiw__putc(stbi__write_context s, byte c)
		{
			s.func(s.context, &c, (int)(1));
		}

		public static void stbiw__write3(stbi__write_context s, byte a, byte b, byte c)
		{
			byte* arr = stackalloc byte[3];
			arr[0] = (byte)(a);
			arr[1] = (byte)(b);
			arr[2] = (byte)(c);
			s.func(s.context, arr, (int)(3));
		}

		public static void stbiw__write_pixel(stbi__write_context s, int rgb_dir, int comp, int write_alpha, int expand_mono, byte* d)
		{
			byte* bg = stackalloc byte[3];
bg[0] = (byte)(255);
bg[1] = (byte)(0);
bg[2] = (byte)(255);
byte* px = stackalloc byte[3];
			int k = 0;
			if ((write_alpha) < (0)) s.func(s.context, &d[comp - 1], (int)(1));
			switch (comp){
case 2:case 1:if ((expand_mono) != 0) stbiw__write3(s, (byte)(d[0]), (byte)(d[0]), (byte)(d[0])); else s.func(s.context, d, (int)(1));break;case 4:if (write_alpha== 0) {
for (k = (int)(0); (k) < (3); ++k) {px[k] = (byte)(bg[k] + ((d[k] - bg[k]) * d[3]) / 255);}stbiw__write3(s, (byte)(px[1 - rgb_dir]), (byte)(px[1]), (byte)(px[1 + rgb_dir]));break;}
case 3:stbiw__write3(s, (byte)(d[1 - rgb_dir]), (byte)(d[1]), (byte)(d[1 + rgb_dir]));break;}

			if ((write_alpha) > (0)) s.func(s.context, &d[comp - 1], (int)(1));
		}

		public static void stbiw__write_pixels(stbi__write_context s, int rgb_dir, int vdir, int x, int y, int comp, void * data, int write_alpha, int scanline_pad, int expand_mono)
		{
			uint zero = (uint)(0);
			int i = 0;int j = 0;int j_end = 0;
			if (y <= 0) return;
			if ((stbi__flip_vertically_on_write) != 0) vdir *= (int)(-1);
			if ((vdir) < (0)) {
j_end = (int)(-1);j = (int)(y - 1);}
 else {
j_end = (int)(y);j = (int)(0);}

			for (; j != j_end; j += (int)(vdir)) {
for (i = (int)(0); (i) < (x); ++i) {
byte* d = (byte*)(data) + (j * x + i) * comp;stbiw__write_pixel(s, (int)(rgb_dir), (int)(comp), (int)(write_alpha), (int)(expand_mono), d);}s.func(s.context, &zero, (int)(scanline_pad));}
		}

		public static int stbi_write_bmp_core(stbi__write_context s, int x, int y, int comp, void * data)
		{
			int pad = (int)((-x * 3) & 3);
			return (int)(stbiw__outfile(s, (int)(-1), (int)(-1), (int)(x), (int)(y), (int)(comp), (int)(1), data, (int)(0), (int)(pad), "11 4 22 44 44 22 444444", (int)('B'), (int)('M'), (int)(14 + 40 + (x * 3 + pad) * y), (int)(0), (int)(0), (int)(14 + 40), (int)(40), (int)(x), (int)(y), (int)(1), (int)(24), (int)(0), (int)(0), (int)(0), (int)(0), (int)(0), (int)(0)));
		}

		public static int stbi_write_tga_core(stbi__write_context s, int x, int y, int comp, void * data)
		{
			int has_alpha = (((comp) == (2)) || ((comp) == (4)))?1:0;
			int colorbytes = (int)((has_alpha) != 0?comp - 1:comp);
			int format = (int)((colorbytes) < (2)?3:2);
			if (((y) < (0)) || ((x) < (0))) return (int)(0);
			if (stbi_write_tga_with_rle== 0) {
return (int)(stbiw__outfile(s, (int)(-1), (int)(-1), (int)(x), (int)(y), (int)(comp), (int)(0), data, (int)(has_alpha), (int)(0), "111 221 2222 11", (int)(0), (int)(0), (int)(format), (int)(0), (int)(0), (int)(0), (int)(0), (int)(0), (int)(x), (int)(y), (int)((colorbytes + has_alpha) * 8), (int)(has_alpha * 8)));}
 else {
int i = 0;int j = 0;int k = 0;int jend = 0;int jdir = 0;stbiw__writef(s, "111 221 2222 11", (int)(0), (int)(0), (int)(format + 8), (int)(0), (int)(0), (int)(0), (int)(0), (int)(0), (int)(x), (int)(y), (int)((colorbytes + has_alpha) * 8), (int)(has_alpha * 8));if ((stbi__flip_vertically_on_write) != 0) {
j = (int)(0);jend = (int)(y);jdir = (int)(1);}
 else {
j = (int)(y - 1);jend = (int)(-1);jdir = (int)(-1);}
for (; j != jend; j += (int)(jdir)) {
byte* row = (byte*)(data) + j * x * comp;int len = 0;for (i = (int)(0); (i) < (x); i += (int)(len)) {
byte* begin = row + i * comp;int diff = (int)(1);len = (int)(1);if ((i) < (x - 1)) {
++len;diff = (int)(CRuntime.memcmp(begin, row + (i + 1) * comp, (ulong)(comp)));if ((diff) != 0) {
byte* prev = begin;for (k = (int)(i + 2); ((k) < (x)) && ((len) < (128)); ++k) {
if ((CRuntime.memcmp(prev, row + k * comp, (ulong)(comp))) != 0) {
prev += comp;++len;}
 else {
--len;break;}
}}
 else {
for (k = (int)(i + 2); ((k) < (x)) && ((len) < (128)); ++k) {
if (CRuntime.memcmp(begin, row + k * comp, (ulong)(comp))== 0) {
++len;}
 else {
break;}
}}
}
if ((diff) != 0) {
byte header = (byte)((len - 1) & 0xff);s.func(s.context, &header, (int)(1));for (k = (int)(0); (k) < (len); ++k) {
stbiw__write_pixel(s, (int)(-1), (int)(comp), (int)(has_alpha), (int)(0), begin + k * comp);}}
 else {
byte header = (byte)((len - 129) & 0xff);s.func(s.context, &header, (int)(1));stbiw__write_pixel(s, (int)(-1), (int)(comp), (int)(has_alpha), (int)(0), begin);}
}}}

			return (int)(1);
		}

		public static void stbiw__linear_to_rgbe(byte* rgbe, float* linear)
		{
			int exponent = 0;
			float maxcomp = (float)((linear[0]) > ((linear[1]) > (linear[2])?(linear[1]):(linear[2]))?(linear[0]):((linear[1]) > (linear[2])?(linear[1]):(linear[2])));
			if ((maxcomp) < (1e-32f)) {
rgbe[0] = (byte)(rgbe[1] = (byte)(rgbe[2] = (byte)(rgbe[3] = (byte)(0))));}
 else {
float normalize = (float)((float)(CRuntime.frexp((double)(maxcomp), &exponent)) * 256.0f / maxcomp);rgbe[0] = ((byte)(linear[0] * normalize));rgbe[1] = ((byte)(linear[1] * normalize));rgbe[2] = ((byte)(linear[2] * normalize));rgbe[3] = ((byte)(exponent + 128));}

		}

		public static void stbiw__write_run_data(stbi__write_context s, int length, byte databyte)
		{
			byte lengthbyte = (byte)((length + 128) & 0xff);
			(void)((!!(length + 128 <= 255)) || (_wassert("length+128 <= 255", "stb_image_write.h", (uint)(621)) , 0));
			s.func(s.context, &lengthbyte, (int)(1));
			s.func(s.context, &databyte, (int)(1));
		}

		public static void stbiw__write_dump_data(stbi__write_context s, int length, byte* data)
		{
			byte lengthbyte = (byte)((length) & 0xff);
			(void)((!!(length <= 128)) || (_wassert("length <= 128", "stb_image_write.h", (uint)(629)) , 0));
			s.func(s.context, &lengthbyte, (int)(1));
			s.func(s.context, data, (int)(length));
		}

		public static void stbiw__write_hdr_scanline(stbi__write_context s, int width, int ncomp, byte* scratch, float* scanline)
		{
			byte* scanlineheader = stackalloc byte[4];
scanlineheader[0] = (byte)(2);
scanlineheader[1] = (byte)(2);
scanlineheader[2] = (byte)(0);
scanlineheader[3] = (byte)(0);

			byte* rgbe = stackalloc byte[4];
			float* linear = stackalloc float[3];
			int x = 0;
			scanlineheader[2] = (byte)((width & 0xff00) >> 8);
			scanlineheader[3] = (byte)(width & 0x00ff);
			if (((width) < (8)) || ((width) >= (32768))) {
for (x = (int)(0); (x) < (width); x++) {
switch (ncomp){
case 4:case 3:linear[2] = (float)(scanline[x * ncomp + 2]);linear[1] = (float)(scanline[x * ncomp + 1]);linear[0] = (float)(scanline[x * ncomp + 0]);break;default: linear[0] = (float)(linear[1] = (float)(linear[2] = (float)(scanline[x * ncomp + 0])));break;}
stbiw__linear_to_rgbe(rgbe, linear);s.func(s.context, rgbe, (int)(4));}}
 else {
int c = 0;int r = 0;for (x = (int)(0); (x) < (width); x++) {
switch (ncomp){
case 4:case 3:linear[2] = (float)(scanline[x * ncomp + 2]);linear[1] = (float)(scanline[x * ncomp + 1]);linear[0] = (float)(scanline[x * ncomp + 0]);break;default: linear[0] = (float)(linear[1] = (float)(linear[2] = (float)(scanline[x * ncomp + 0])));break;}
stbiw__linear_to_rgbe(rgbe, linear);scratch[x + width * 0] = (byte)(rgbe[0]);scratch[x + width * 1] = (byte)(rgbe[1]);scratch[x + width * 2] = (byte)(rgbe[2]);scratch[x + width * 3] = (byte)(rgbe[3]);}s.func(s.context, scanlineheader, (int)(4));for (c = (int)(0); (c) < (4); c++) {
byte* comp = &scratch[width * c];x = (int)(0);while ((x) < (width)) {
r = (int)(x);while ((r + 2) < (width)) {
if (((comp[r]) == (comp[r + 1])) && ((comp[r]) == (comp[r + 2]))) break;++r;}if ((r + 2) >= (width)) r = (int)(width);while ((x) < (r)) {
int len = (int)(r - x);if ((len) > (128)) len = (int)(128);stbiw__write_dump_data(s, (int)(len), &comp[x]);x += (int)(len);}if ((r + 2) < (width)) {
while (((r) < (width)) && ((comp[r]) == (comp[x]))) {++r;}while ((x) < (r)) {
int len = (int)(r - x);if ((len) > (127)) len = (int)(127);stbiw__write_run_data(s, (int)(len), (byte)(comp[x]));x += (int)(len);}}
}}}

		}

		public static void * stbiw__sbgrowf(void ** arr, int increment, int itemsize)
		{
			int m = (int)(*arr != null?2 * ((int*)(*arr) - 2)[0] + increment:increment + 1);
			void * p = CRuntime.realloc(*arr != null?((int*)(*arr) - 2):null, (ulong)(itemsize * m + sizeof(int) * 2));
			(void)((!!(p)) || (_wassert("p", "stb_image_write.h", (uint)(793)) , 0));
			if ((p) != null) {
if (*arr== null) ((int*)(p))[1] = (int)(0);*arr = (void *)((int*)(p) + 2);((int*)(*arr) - 2)[0] = (int)(m);}

			return *arr;
		}

		public static byte* stbiw__zlib_flushf(byte* data, uint* bitbuffer, int* bitcount)
		{
			while ((*bitcount) >= (8)) {
(((((data) == (null)) || ((((int*)(data) - 2)[1] + (1)) >= (((int*)(data) - 2)[0])))?stbiw__sbgrowf((void **)(&(data)), (int)(1), sizeof(byte)):null) , (data)[((int*)(data) - 2)[1]++] = ((byte)((*bitbuffer) & 0xff)));*bitbuffer >>= 8;*bitcount -= (int)(8);}
			return data;
		}

		public static int stbiw__zlib_bitrev(int code, int codebits)
		{
			int res = (int)(0);
			while ((codebits--) != 0) {
res = (int)((res << 1) | (code & 1));code >>= 1;}
			return (int)(res);
		}

		public static uint stbiw__zlib_countm(byte* a, byte* b, int limit)
		{
			int i = 0;
			for (i = (int)(0); ((i) < (limit)) && ((i) < (258)); ++i) {if (a[i] != b[i]) break;}
			return (uint)(i);
		}

		public static uint stbiw__zhash(byte* data)
		{
			uint hash = (uint)(data[0] + (data[1] << 8) + (data[2] << 16));
			hash ^= (uint)(hash << 3);
			hash += (uint)(hash >> 5);
			hash ^= (uint)(hash << 4);
			hash += (uint)(hash >> 17);
			hash ^= (uint)(hash << 25);
			hash += (uint)(hash >> 6);
			return (uint)(hash);
		}

		public static byte* stbi_zlib_compress(byte* data, int data_len, int* out_len, int quality)
		{
			ushort* lengthc = stackalloc ushort[30];
lengthc[0] = (ushort)(3);
lengthc[1] = (ushort)(4);
lengthc[2] = (ushort)(5);
lengthc[3] = (ushort)(6);
lengthc[4] = (ushort)(7);
lengthc[5] = (ushort)(8);
lengthc[6] = (ushort)(9);
lengthc[7] = (ushort)(10);
lengthc[8] = (ushort)(11);
lengthc[9] = (ushort)(13);
lengthc[10] = (ushort)(15);
lengthc[11] = (ushort)(17);
lengthc[12] = (ushort)(19);
lengthc[13] = (ushort)(23);
lengthc[14] = (ushort)(27);
lengthc[15] = (ushort)(31);
lengthc[16] = (ushort)(35);
lengthc[17] = (ushort)(43);
lengthc[18] = (ushort)(51);
lengthc[19] = (ushort)(59);
lengthc[20] = (ushort)(67);
lengthc[21] = (ushort)(83);
lengthc[22] = (ushort)(99);
lengthc[23] = (ushort)(115);
lengthc[24] = (ushort)(131);
lengthc[25] = (ushort)(163);
lengthc[26] = (ushort)(195);
lengthc[27] = (ushort)(227);
lengthc[28] = (ushort)(258);
lengthc[29] = (ushort)(259);

			byte* lengtheb = stackalloc byte[29];
lengtheb[0] = (byte)(0);
lengtheb[1] = (byte)(0);
lengtheb[2] = (byte)(0);
lengtheb[3] = (byte)(0);
lengtheb[4] = (byte)(0);
lengtheb[5] = (byte)(0);
lengtheb[6] = (byte)(0);
lengtheb[7] = (byte)(0);
lengtheb[8] = (byte)(1);
lengtheb[9] = (byte)(1);
lengtheb[10] = (byte)(1);
lengtheb[11] = (byte)(1);
lengtheb[12] = (byte)(2);
lengtheb[13] = (byte)(2);
lengtheb[14] = (byte)(2);
lengtheb[15] = (byte)(2);
lengtheb[16] = (byte)(3);
lengtheb[17] = (byte)(3);
lengtheb[18] = (byte)(3);
lengtheb[19] = (byte)(3);
lengtheb[20] = (byte)(4);
lengtheb[21] = (byte)(4);
lengtheb[22] = (byte)(4);
lengtheb[23] = (byte)(4);
lengtheb[24] = (byte)(5);
lengtheb[25] = (byte)(5);
lengtheb[26] = (byte)(5);
lengtheb[27] = (byte)(5);
lengtheb[28] = (byte)(0);

			ushort* distc = stackalloc ushort[31];
distc[0] = (ushort)(1);
distc[1] = (ushort)(2);
distc[2] = (ushort)(3);
distc[3] = (ushort)(4);
distc[4] = (ushort)(5);
distc[5] = (ushort)(7);
distc[6] = (ushort)(9);
distc[7] = (ushort)(13);
distc[8] = (ushort)(17);
distc[9] = (ushort)(25);
distc[10] = (ushort)(33);
distc[11] = (ushort)(49);
distc[12] = (ushort)(65);
distc[13] = (ushort)(97);
distc[14] = (ushort)(129);
distc[15] = (ushort)(193);
distc[16] = (ushort)(257);
distc[17] = (ushort)(385);
distc[18] = (ushort)(513);
distc[19] = (ushort)(769);
distc[20] = (ushort)(1025);
distc[21] = (ushort)(1537);
distc[22] = (ushort)(2049);
distc[23] = (ushort)(3073);
distc[24] = (ushort)(4097);
distc[25] = (ushort)(6145);
distc[26] = (ushort)(8193);
distc[27] = (ushort)(12289);
distc[28] = (ushort)(16385);
distc[29] = (ushort)(24577);
distc[30] = (ushort)(32768);

			byte* disteb = stackalloc byte[30];
disteb[0] = (byte)(0);
disteb[1] = (byte)(0);
disteb[2] = (byte)(0);
disteb[3] = (byte)(0);
disteb[4] = (byte)(1);
disteb[5] = (byte)(1);
disteb[6] = (byte)(2);
disteb[7] = (byte)(2);
disteb[8] = (byte)(3);
disteb[9] = (byte)(3);
disteb[10] = (byte)(4);
disteb[11] = (byte)(4);
disteb[12] = (byte)(5);
disteb[13] = (byte)(5);
disteb[14] = (byte)(6);
disteb[15] = (byte)(6);
disteb[16] = (byte)(7);
disteb[17] = (byte)(7);
disteb[18] = (byte)(8);
disteb[19] = (byte)(8);
disteb[20] = (byte)(9);
disteb[21] = (byte)(9);
disteb[22] = (byte)(10);
disteb[23] = (byte)(10);
disteb[24] = (byte)(11);
disteb[25] = (byte)(11);
disteb[26] = (byte)(12);
disteb[27] = (byte)(12);
disteb[28] = (byte)(13);
disteb[29] = (byte)(13);

			uint bitbuf = (uint)(0);
			int i = 0;int j = 0;int bitcount = (int)(0);
			byte* _out_ = ((void *)(0));
			byte*** hash_table = (byte***)(CRuntime.malloc((ulong)(16384 * sizeof(char**))));
			if ((hash_table) == ((void *)(0))) return ((void *)(0));
			if ((quality) < (5)) quality = (int)(5);
			(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = (byte)(0x78));
			(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = (byte)(0x5e));
			(bitbuf |= (uint)((1) << bitcount) , bitcount += (int)(1) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));
			(bitbuf |= (uint)((1) << bitcount) , bitcount += (int)(2) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));
			for (i = (int)(0); (i) < (16384); ++i) {hash_table[i] = ((void *)(0));}
			i = (int)(0);
			while ((i) < (data_len - 3)) {
int h = (int)(stbiw__zhash(data + i) & (16384 - 1));int best = (int)(3);byte* bestloc = null;byte** hlist = hash_table[h];int n = (int)((hlist != null)?((int*)(hlist) - 2)[1]:0);for (j = (int)(0); (j) < (n); ++j) {
if ((hlist[j] - data) > (i - 32768)) {
int d = (int)(stbiw__zlib_countm(hlist[j], data + i, (int)(data_len - i)));if ((d) >= (best)) {
best = (int)(d);bestloc = hlist[j];}
}
}if (((hash_table[h]) != null) && ((((int*)(hash_table[h]) - 2)[1]) == (2 * quality))) {
CRuntime.memmove(hash_table[h], hash_table[h] + quality, (ulong)(sizeof(byte*) * quality));((int*)(hash_table[h]) - 2)[1] = (int)(quality);}
(((((hash_table[h]) == (null)) || ((((int*)(hash_table[h]) - 2)[1] + (1)) >= (((int*)(hash_table[h]) - 2)[0])))?stbiw__sbgrowf((void **)(&(hash_table[h])), (int)(1), sizeof(byte*)):null) , (hash_table[h])[((int*)(hash_table[h]) - 2)[1]++] = (data + i));if ((bestloc) != null) {
h = (int)(stbiw__zhash(data + i + 1) & (16384 - 1));hlist = hash_table[h];n = (int)((hlist != null)?((int*)(hlist) - 2)[1]:0);for (j = (int)(0); (j) < (n); ++j) {
if ((hlist[j] - data) > (i - 32767)) {
int e = (int)(stbiw__zlib_countm(hlist[j], data + i + 1, (int)(data_len - i - 1)));if ((e) > (best)) {
bestloc = ((void *)(0));break;}
}
}}
if ((bestloc) != null) {
int d = (int)(data + i - bestloc);(void)((!!((d <= 32767) && (best <= 258))) || (_wassert("d <= 32767 && best <= 258", "stb_image_write.h", (uint)(922)) , 0));for (j = (int)(0); (best) > (lengthc[j + 1] - 1); ++j) {}((j + 257) <= 143?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x30 + (j + 257)), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(j + 257) <= 255?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x190 + (j + 257) - 144), (int)(9))) << bitcount) , bitcount += (int)(9) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(j + 257) <= 279?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0 + (j + 257) - 256), (int)(7))) << bitcount) , bitcount += (int)(7) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0xc0 + (j + 257) - 280), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))));if ((lengtheb[j]) != 0) (bitbuf |= (uint)((best - lengthc[j]) << bitcount) , bitcount += (int)(lengtheb[j]) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));for (j = (int)(0); (d) > (distc[j + 1] - 1); ++j) {}(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(j), (int)(5))) << bitcount) , bitcount += (int)(5) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));if ((disteb[j]) != 0) (bitbuf |= (uint)((d - distc[j]) << bitcount) , bitcount += (int)(disteb[j]) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));i += (int)(best);}
 else {
((data[i]) <= 143?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x30 + (data[i])), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x190 + (data[i]) - 144), (int)(9))) << bitcount) , bitcount += (int)(9) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))));++i;}
}
			for (; (i) < (data_len); ++i) {((data[i]) <= 143?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x30 + (data[i])), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x190 + (data[i]) - 144), (int)(9))) << bitcount) , bitcount += (int)(9) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))));}
			((256) <= 143?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x30 + (256)), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(256) <= 255?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0x190 + (256) - 144), (int)(9))) << bitcount) , bitcount += (int)(9) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(256) <= 279?(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0 + (256) - 256), (int)(7))) << bitcount) , bitcount += (int)(7) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))):(bitbuf |= (uint)((stbiw__zlib_bitrev((int)(0xc0 + (256) - 280), (int)(8))) << bitcount) , bitcount += (int)(8) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount))));
			while ((bitcount) != 0) {(bitbuf |= (uint)((0) << bitcount) , bitcount += (int)(1) , (_out_ = stbiw__zlib_flushf(_out_, &bitbuf, &bitcount)));}
			for (i = (int)(0); (i) < (16384); ++i) {(void)((hash_table[i] != null)?CRuntime.free(((int*)(hash_table[i]) - 2)) , 0:0);}
			CRuntime.free(hash_table);
			{
uint s1 = (uint)(1);uint s2 = (uint)(0);int blocklen = (int)(data_len % 5552);j = (int)(0);while ((j) < (data_len)) {
for (i = (int)(0); (i) < (blocklen); ++i) {
s1 += (uint)(data[j + i]);s2 += (uint)(s1);}s1 %= (uint)(65521);s2 %= (uint)(65521);j += (int)(blocklen);blocklen = (int)(5552);}(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = ((byte)((s2 >> 8) & 0xff)));(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = ((byte)((s2) & 0xff)));(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = ((byte)((s1 >> 8) & 0xff)));(((((_out_) == (null)) || ((((int*)(_out_) - 2)[1] + (1)) >= (((int*)(_out_) - 2)[0])))?stbiw__sbgrowf((void **)(&(_out_)), (int)(1), sizeof(byte)):null) , (_out_)[((int*)(_out_) - 2)[1]++] = ((byte)((s1) & 0xff)));}

			*out_len = (int)(((int*)(_out_) - 2)[1]);
			CRuntime.memmove(((int*)(_out_) - 2), _out_, (ulong)(*out_len));
			return (byte*)((int*)(_out_) - 2);
		}

		public static uint stbiw__crc32(byte* buffer, int len)
		{
			uint* crc_table = stackalloc uint[256];
crc_table[0] = (uint)(0x00000000);
crc_table[1] = (uint)(0x77073096);
crc_table[2] = (uint)(0xEE0E612C);
crc_table[3] = (uint)(0x990951BA);
crc_table[4] = (uint)(0x076DC419);
crc_table[5] = (uint)(0x706AF48F);
crc_table[6] = (uint)(0xE963A535);
crc_table[7] = (uint)(0x9E6495A3);
crc_table[8] = (uint)(0x0eDB8832);
crc_table[9] = (uint)(0x79DCB8A4);
crc_table[10] = (uint)(0xE0D5E91E);
crc_table[11] = (uint)(0x97D2D988);
crc_table[12] = (uint)(0x09B64C2B);
crc_table[13] = (uint)(0x7EB17CBD);
crc_table[14] = (uint)(0xE7B82D07);
crc_table[15] = (uint)(0x90BF1D91);
crc_table[16] = (uint)(0x1DB71064);
crc_table[17] = (uint)(0x6AB020F2);
crc_table[18] = (uint)(0xF3B97148);
crc_table[19] = (uint)(0x84BE41DE);
crc_table[20] = (uint)(0x1ADAD47D);
crc_table[21] = (uint)(0x6DDDE4EB);
crc_table[22] = (uint)(0xF4D4B551);
crc_table[23] = (uint)(0x83D385C7);
crc_table[24] = (uint)(0x136C9856);
crc_table[25] = (uint)(0x646BA8C0);
crc_table[26] = (uint)(0xFD62F97A);
crc_table[27] = (uint)(0x8A65C9EC);
crc_table[28] = (uint)(0x14015C4F);
crc_table[29] = (uint)(0x63066CD9);
crc_table[30] = (uint)(0xFA0F3D63);
crc_table[31] = (uint)(0x8D080DF5);
crc_table[32] = (uint)(0x3B6E20C8);
crc_table[33] = (uint)(0x4C69105E);
crc_table[34] = (uint)(0xD56041E4);
crc_table[35] = (uint)(0xA2677172);
crc_table[36] = (uint)(0x3C03E4D1);
crc_table[37] = (uint)(0x4B04D447);
crc_table[38] = (uint)(0xD20D85FD);
crc_table[39] = (uint)(0xA50AB56B);
crc_table[40] = (uint)(0x35B5A8FA);
crc_table[41] = (uint)(0x42B2986C);
crc_table[42] = (uint)(0xDBBBC9D6);
crc_table[43] = (uint)(0xACBCF940);
crc_table[44] = (uint)(0x32D86CE3);
crc_table[45] = (uint)(0x45DF5C75);
crc_table[46] = (uint)(0xDCD60DCF);
crc_table[47] = (uint)(0xABD13D59);
crc_table[48] = (uint)(0x26D930AC);
crc_table[49] = (uint)(0x51DE003A);
crc_table[50] = (uint)(0xC8D75180);
crc_table[51] = (uint)(0xBFD06116);
crc_table[52] = (uint)(0x21B4F4B5);
crc_table[53] = (uint)(0x56B3C423);
crc_table[54] = (uint)(0xCFBA9599);
crc_table[55] = (uint)(0xB8BDA50F);
crc_table[56] = (uint)(0x2802B89E);
crc_table[57] = (uint)(0x5F058808);
crc_table[58] = (uint)(0xC60CD9B2);
crc_table[59] = (uint)(0xB10BE924);
crc_table[60] = (uint)(0x2F6F7C87);
crc_table[61] = (uint)(0x58684C11);
crc_table[62] = (uint)(0xC1611DAB);
crc_table[63] = (uint)(0xB6662D3D);
crc_table[64] = (uint)(0x76DC4190);
crc_table[65] = (uint)(0x01DB7106);
crc_table[66] = (uint)(0x98D220BC);
crc_table[67] = (uint)(0xEFD5102A);
crc_table[68] = (uint)(0x71B18589);
crc_table[69] = (uint)(0x06B6B51F);
crc_table[70] = (uint)(0x9FBFE4A5);
crc_table[71] = (uint)(0xE8B8D433);
crc_table[72] = (uint)(0x7807C9A2);
crc_table[73] = (uint)(0x0F00F934);
crc_table[74] = (uint)(0x9609A88E);
crc_table[75] = (uint)(0xE10E9818);
crc_table[76] = (uint)(0x7F6A0DBB);
crc_table[77] = (uint)(0x086D3D2D);
crc_table[78] = (uint)(0x91646C97);
crc_table[79] = (uint)(0xE6635C01);
crc_table[80] = (uint)(0x6B6B51F4);
crc_table[81] = (uint)(0x1C6C6162);
crc_table[82] = (uint)(0x856530D8);
crc_table[83] = (uint)(0xF262004E);
crc_table[84] = (uint)(0x6C0695ED);
crc_table[85] = (uint)(0x1B01A57B);
crc_table[86] = (uint)(0x8208F4C1);
crc_table[87] = (uint)(0xF50FC457);
crc_table[88] = (uint)(0x65B0D9C6);
crc_table[89] = (uint)(0x12B7E950);
crc_table[90] = (uint)(0x8BBEB8EA);
crc_table[91] = (uint)(0xFCB9887C);
crc_table[92] = (uint)(0x62DD1DDF);
crc_table[93] = (uint)(0x15DA2D49);
crc_table[94] = (uint)(0x8CD37CF3);
crc_table[95] = (uint)(0xFBD44C65);
crc_table[96] = (uint)(0x4DB26158);
crc_table[97] = (uint)(0x3AB551CE);
crc_table[98] = (uint)(0xA3BC0074);
crc_table[99] = (uint)(0xD4BB30E2);
crc_table[100] = (uint)(0x4ADFA541);
crc_table[101] = (uint)(0x3DD895D7);
crc_table[102] = (uint)(0xA4D1C46D);
crc_table[103] = (uint)(0xD3D6F4FB);
crc_table[104] = (uint)(0x4369E96A);
crc_table[105] = (uint)(0x346ED9FC);
crc_table[106] = (uint)(0xAD678846);
crc_table[107] = (uint)(0xDA60B8D0);
crc_table[108] = (uint)(0x44042D73);
crc_table[109] = (uint)(0x33031DE5);
crc_table[110] = (uint)(0xAA0A4C5F);
crc_table[111] = (uint)(0xDD0D7CC9);
crc_table[112] = (uint)(0x5005713C);
crc_table[113] = (uint)(0x270241AA);
crc_table[114] = (uint)(0xBE0B1010);
crc_table[115] = (uint)(0xC90C2086);
crc_table[116] = (uint)(0x5768B525);
crc_table[117] = (uint)(0x206F85B3);
crc_table[118] = (uint)(0xB966D409);
crc_table[119] = (uint)(0xCE61E49F);
crc_table[120] = (uint)(0x5EDEF90E);
crc_table[121] = (uint)(0x29D9C998);
crc_table[122] = (uint)(0xB0D09822);
crc_table[123] = (uint)(0xC7D7A8B4);
crc_table[124] = (uint)(0x59B33D17);
crc_table[125] = (uint)(0x2EB40D81);
crc_table[126] = (uint)(0xB7BD5C3B);
crc_table[127] = (uint)(0xC0BA6CAD);
crc_table[128] = (uint)(0xEDB88320);
crc_table[129] = (uint)(0x9ABFB3B6);
crc_table[130] = (uint)(0x03B6E20C);
crc_table[131] = (uint)(0x74B1D29A);
crc_table[132] = (uint)(0xEAD54739);
crc_table[133] = (uint)(0x9DD277AF);
crc_table[134] = (uint)(0x04DB2615);
crc_table[135] = (uint)(0x73DC1683);
crc_table[136] = (uint)(0xE3630B12);
crc_table[137] = (uint)(0x94643B84);
crc_table[138] = (uint)(0x0D6D6A3E);
crc_table[139] = (uint)(0x7A6A5AA8);
crc_table[140] = (uint)(0xE40ECF0B);
crc_table[141] = (uint)(0x9309FF9D);
crc_table[142] = (uint)(0x0A00AE27);
crc_table[143] = (uint)(0x7D079EB1);
crc_table[144] = (uint)(0xF00F9344);
crc_table[145] = (uint)(0x8708A3D2);
crc_table[146] = (uint)(0x1E01F268);
crc_table[147] = (uint)(0x6906C2FE);
crc_table[148] = (uint)(0xF762575D);
crc_table[149] = (uint)(0x806567CB);
crc_table[150] = (uint)(0x196C3671);
crc_table[151] = (uint)(0x6E6B06E7);
crc_table[152] = (uint)(0xFED41B76);
crc_table[153] = (uint)(0x89D32BE0);
crc_table[154] = (uint)(0x10DA7A5A);
crc_table[155] = (uint)(0x67DD4ACC);
crc_table[156] = (uint)(0xF9B9DF6F);
crc_table[157] = (uint)(0x8EBEEFF9);
crc_table[158] = (uint)(0x17B7BE43);
crc_table[159] = (uint)(0x60B08ED5);
crc_table[160] = (uint)(0xD6D6A3E8);
crc_table[161] = (uint)(0xA1D1937E);
crc_table[162] = (uint)(0x38D8C2C4);
crc_table[163] = (uint)(0x4FDFF252);
crc_table[164] = (uint)(0xD1BB67F1);
crc_table[165] = (uint)(0xA6BC5767);
crc_table[166] = (uint)(0x3FB506DD);
crc_table[167] = (uint)(0x48B2364B);
crc_table[168] = (uint)(0xD80D2BDA);
crc_table[169] = (uint)(0xAF0A1B4C);
crc_table[170] = (uint)(0x36034AF6);
crc_table[171] = (uint)(0x41047A60);
crc_table[172] = (uint)(0xDF60EFC3);
crc_table[173] = (uint)(0xA867DF55);
crc_table[174] = (uint)(0x316E8EEF);
crc_table[175] = (uint)(0x4669BE79);
crc_table[176] = (uint)(0xCB61B38C);
crc_table[177] = (uint)(0xBC66831A);
crc_table[178] = (uint)(0x256FD2A0);
crc_table[179] = (uint)(0x5268E236);
crc_table[180] = (uint)(0xCC0C7795);
crc_table[181] = (uint)(0xBB0B4703);
crc_table[182] = (uint)(0x220216B9);
crc_table[183] = (uint)(0x5505262F);
crc_table[184] = (uint)(0xC5BA3BBE);
crc_table[185] = (uint)(0xB2BD0B28);
crc_table[186] = (uint)(0x2BB45A92);
crc_table[187] = (uint)(0x5CB36A04);
crc_table[188] = (uint)(0xC2D7FFA7);
crc_table[189] = (uint)(0xB5D0CF31);
crc_table[190] = (uint)(0x2CD99E8B);
crc_table[191] = (uint)(0x5BDEAE1D);
crc_table[192] = (uint)(0x9B64C2B0);
crc_table[193] = (uint)(0xEC63F226);
crc_table[194] = (uint)(0x756AA39C);
crc_table[195] = (uint)(0x026D930A);
crc_table[196] = (uint)(0x9C0906A9);
crc_table[197] = (uint)(0xEB0E363F);
crc_table[198] = (uint)(0x72076785);
crc_table[199] = (uint)(0x05005713);
crc_table[200] = (uint)(0x95BF4A82);
crc_table[201] = (uint)(0xE2B87A14);
crc_table[202] = (uint)(0x7BB12BAE);
crc_table[203] = (uint)(0x0CB61B38);
crc_table[204] = (uint)(0x92D28E9B);
crc_table[205] = (uint)(0xE5D5BE0D);
crc_table[206] = (uint)(0x7CDCEFB7);
crc_table[207] = (uint)(0x0BDBDF21);
crc_table[208] = (uint)(0x86D3D2D4);
crc_table[209] = (uint)(0xF1D4E242);
crc_table[210] = (uint)(0x68DDB3F8);
crc_table[211] = (uint)(0x1FDA836E);
crc_table[212] = (uint)(0x81BE16CD);
crc_table[213] = (uint)(0xF6B9265B);
crc_table[214] = (uint)(0x6FB077E1);
crc_table[215] = (uint)(0x18B74777);
crc_table[216] = (uint)(0x88085AE6);
crc_table[217] = (uint)(0xFF0F6A70);
crc_table[218] = (uint)(0x66063BCA);
crc_table[219] = (uint)(0x11010B5C);
crc_table[220] = (uint)(0x8F659EFF);
crc_table[221] = (uint)(0xF862AE69);
crc_table[222] = (uint)(0x616BFFD3);
crc_table[223] = (uint)(0x166CCF45);
crc_table[224] = (uint)(0xA00AE278);
crc_table[225] = (uint)(0xD70DD2EE);
crc_table[226] = (uint)(0x4E048354);
crc_table[227] = (uint)(0x3903B3C2);
crc_table[228] = (uint)(0xA7672661);
crc_table[229] = (uint)(0xD06016F7);
crc_table[230] = (uint)(0x4969474D);
crc_table[231] = (uint)(0x3E6E77DB);
crc_table[232] = (uint)(0xAED16A4A);
crc_table[233] = (uint)(0xD9D65ADC);
crc_table[234] = (uint)(0x40DF0B66);
crc_table[235] = (uint)(0x37D83BF0);
crc_table[236] = (uint)(0xA9BCAE53);
crc_table[237] = (uint)(0xDEBB9EC5);
crc_table[238] = (uint)(0x47B2CF7F);
crc_table[239] = (uint)(0x30B5FFE9);
crc_table[240] = (uint)(0xBDBDF21C);
crc_table[241] = (uint)(0xCABAC28A);
crc_table[242] = (uint)(0x53B39330);
crc_table[243] = (uint)(0x24B4A3A6);
crc_table[244] = (uint)(0xBAD03605);
crc_table[245] = (uint)(0xCDD70693);
crc_table[246] = (uint)(0x54DE5729);
crc_table[247] = (uint)(0x23D967BF);
crc_table[248] = (uint)(0xB3667A2E);
crc_table[249] = (uint)(0xC4614AB8);
crc_table[250] = (uint)(0x5D681B02);
crc_table[251] = (uint)(0x2A6F2B94);
crc_table[252] = (uint)(0xB40BBE37);
crc_table[253] = (uint)(0xC30C8EA1);
crc_table[254] = (uint)(0x5A05DF1B);
crc_table[255] = (uint)(0x2D02EF8D);

			uint crc = (uint)(~0u);
			int i = 0;
			for (i = (int)(0); (i) < (len); ++i) {crc = (uint)((crc >> 8) ^ crc_table[buffer[i] ^ (crc & 0xff)]);}
			return (uint)(~crc);
		}

		public static void stbiw__wpcrc(byte** data, int len)
		{
			uint crc = (uint)(stbiw__crc32(*data - len - 4, (int)(len + 4)));
			((*data)[0] = ((byte)(((crc) >> 24) & 0xff)) , (*data)[1] = ((byte)(((crc) >> 16) & 0xff)) , (*data)[2] = ((byte)(((crc) >> 8) & 0xff)) , (*data)[3] = ((byte)((crc) & 0xff)) , (*data) += 4);
		}

		public static byte stbiw__paeth(int a, int b, int c)
		{
			int p = (int)(a + b - c);int pa = (int)(CRuntime.abs((int)(p - a)));int pb = (int)(CRuntime.abs((int)(p - b)));int pc = (int)(CRuntime.abs((int)(p - c)));
			if ((pa <= pb) && (pa <= pc)) return (byte)((a) & 0xff);
			if (pb <= pc) return (byte)((b) & 0xff);
			return (byte)((c) & 0xff);
		}

		public static void stbiw__encode_png_line(byte* pixels, int stride_bytes, int width, int height, int y, int n, int filter_type, sbyte* line_buffer)
		{
			int* mapping = stackalloc int[5];
mapping[0] = (int)(0);
mapping[1] = (int)(1);
mapping[2] = (int)(2);
mapping[3] = (int)(3);
mapping[4] = (int)(4);

			int* firstmap = stackalloc int[5];
firstmap[0] = (int)(0);
firstmap[1] = (int)(1);
firstmap[2] = (int)(0);
firstmap[3] = (int)(5);
firstmap[4] = (int)(6);

			int* mymap = (y != 0)?mapping:firstmap;
			int i = 0;
			int type = (int)(mymap[filter_type]);
			byte* z = pixels + stride_bytes * ((stbi__flip_vertically_on_write) != 0?height - 1 - y:y);
			int signed_stride = (int)((stbi__flip_vertically_on_write) != 0?-stride_bytes:stride_bytes);
			if ((type) == (0)) {
CRuntime.memcpy(line_buffer, z, (ulong)(width * n));return;}

			for (i = (int)(0); (i) < (n); ++i) {
switch (type){
case 1:line_buffer[i] = (sbyte)(z[i]);break;case 2:line_buffer[i] = (sbyte)(z[i] - z[i - signed_stride]);break;case 3:line_buffer[i] = (sbyte)(z[i] - (z[i - signed_stride] >> 1));break;case 4:line_buffer[i] = ((sbyte)(z[i] - stbiw__paeth((int)(0), (int)(z[i - signed_stride]), (int)(0))));break;case 5:line_buffer[i] = (sbyte)(z[i]);break;case 6:line_buffer[i] = (sbyte)(z[i]);break;}
}
			switch (type){
case 1:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - z[i - n]);}break;case 2:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - z[i - signed_stride]);}break;case 3:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - ((z[i - n] + z[i - signed_stride]) >> 1));}break;case 4:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - stbiw__paeth((int)(z[i - n]), (int)(z[i - signed_stride]), (int)(z[i - signed_stride - n])));}break;case 5:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - (z[i - n] >> 1));}break;case 6:for (i = (int)(n); (i) < (width * n); ++i) {line_buffer[i] = (sbyte)(z[i] - stbiw__paeth((int)(z[i - n]), (int)(0), (int)(0)));}break;}

		}

		public static byte* stbi_write_png_to_mem(byte* pixels, int stride_bytes, int x, int y, int n, int* out_len)
		{
			int force_filter = (int)(stbi_write_force_png_filter);
			int* ctype = stackalloc int[5];
ctype[0] = (int)(-1);
ctype[1] = (int)(0);
ctype[2] = (int)(4);
ctype[3] = (int)(2);
ctype[4] = (int)(6);

			byte* sig = stackalloc byte[8];
sig[0] = (byte)(137);
sig[1] = (byte)(80);
sig[2] = (byte)(78);
sig[3] = (byte)(71);
sig[4] = (byte)(13);
sig[5] = (byte)(10);
sig[6] = (byte)(26);
sig[7] = (byte)(10);

			byte* _out_;byte* o;byte* filt;byte* zlib;
			sbyte* line_buffer;
			int j = 0;int zlen = 0;
			if ((stride_bytes) == (0)) stride_bytes = (int)(x * n);
			if ((force_filter) >= (5)) {
force_filter = (int)(-1);}

			filt = (byte*)(CRuntime.malloc((ulong)((x * n + 1) * y)));
			if (filt== null) return null;
			line_buffer = (sbyte*)(CRuntime.malloc((ulong)(x * n)));
			if (line_buffer== null) {
CRuntime.free(filt);return null;}

			for (j = (int)(0); (j) < (y); ++j) {
int filter_type = 0;if ((force_filter) > (-1)) {
filter_type = (int)(force_filter);stbiw__encode_png_line((pixels), (int)(stride_bytes), (int)(x), (int)(y), (int)(j), (int)(n), (int)(force_filter), line_buffer);}
 else {
int best_filter = (int)(0);int best_filter_val = (int)(0x7fffffff);int est = 0;int i = 0;for (filter_type = (int)(0); (filter_type) < (5); filter_type++) {
stbiw__encode_png_line((pixels), (int)(stride_bytes), (int)(x), (int)(y), (int)(j), (int)(n), (int)(filter_type), line_buffer);est = (int)(0);for (i = (int)(0); (i) < (x * n); ++i) {
est += (int)(CRuntime.abs((int)(line_buffer[i])));}if ((est) < (best_filter_val)) {
best_filter_val = (int)(est);best_filter = (int)(filter_type);}
}if (filter_type != best_filter) {
stbiw__encode_png_line((pixels), (int)(stride_bytes), (int)(x), (int)(y), (int)(j), (int)(n), (int)(best_filter), line_buffer);filter_type = (int)(best_filter);}
}
filt[j * (x * n + 1)] = ((byte)(filter_type));CRuntime.memmove(filt + j * (x * n + 1) + 1, line_buffer, (ulong)(x * n));}
			CRuntime.free(line_buffer);
			zlib = stbi_zlib_compress(filt, (int)(y * (x * n + 1)), &zlen, (int)(stbi_write_png_compression_level));
			CRuntime.free(filt);
			if (zlib== null) return null;
			_out_ = (byte*)(CRuntime.malloc((ulong)(8 + 12 + 13 + 12 + zlen + 12)));
			if (_out_== null) return null;
			*out_len = (int)(8 + 12 + 13 + 12 + zlen + 12);
			o = _out_;
			CRuntime.memmove(o, sig, (ulong)(8));
			o += 8;
			((o)[0] = ((byte)(((13) >> 24) & 0xff)) , (o)[1] = ((byte)(((13) >> 16) & 0xff)) , (o)[2] = ((byte)(((13) >> 8) & 0xff)) , (o)[3] = ((byte)((13) & 0xff)) , (o) += 4);
			((o)[0] = ((byte)(("IHDR"[0]) & 0xff)) , (o)[1] = ((byte)(("IHDR"[1]) & 0xff)) , (o)[2] = ((byte)(("IHDR"[2]) & 0xff)) , (o)[3] = ((byte)(("IHDR"[3]) & 0xff)) , (o) += 4);
			((o)[0] = ((byte)(((x) >> 24) & 0xff)) , (o)[1] = ((byte)(((x) >> 16) & 0xff)) , (o)[2] = ((byte)(((x) >> 8) & 0xff)) , (o)[3] = ((byte)((x) & 0xff)) , (o) += 4);
			((o)[0] = ((byte)(((y) >> 24) & 0xff)) , (o)[1] = ((byte)(((y) >> 16) & 0xff)) , (o)[2] = ((byte)(((y) >> 8) & 0xff)) , (o)[3] = ((byte)((y) & 0xff)) , (o) += 4);
			*o++ = (byte)(8);
			*o++ = ((byte)((ctype[n]) & 0xff));
			*o++ = (byte)(0);
			*o++ = (byte)(0);
			*o++ = (byte)(0);
			stbiw__wpcrc(&o, (int)(13));
			((o)[0] = ((byte)(((zlen) >> 24) & 0xff)) , (o)[1] = ((byte)(((zlen) >> 16) & 0xff)) , (o)[2] = ((byte)(((zlen) >> 8) & 0xff)) , (o)[3] = ((byte)((zlen) & 0xff)) , (o) += 4);
			((o)[0] = ((byte)(("IDAT"[0]) & 0xff)) , (o)[1] = ((byte)(("IDAT"[1]) & 0xff)) , (o)[2] = ((byte)(("IDAT"[2]) & 0xff)) , (o)[3] = ((byte)(("IDAT"[3]) & 0xff)) , (o) += 4);
			CRuntime.memmove(o, zlib, (ulong)(zlen));
			o += zlen;
			CRuntime.free(zlib);
			stbiw__wpcrc(&o, (int)(zlen));
			((o)[0] = ((byte)(((0) >> 24) & 0xff)) , (o)[1] = ((byte)(((0) >> 16) & 0xff)) , (o)[2] = ((byte)(((0) >> 8) & 0xff)) , (o)[3] = ((byte)((0) & 0xff)) , (o) += 4);
			((o)[0] = ((byte)(("IEND"[0]) & 0xff)) , (o)[1] = ((byte)(("IEND"[1]) & 0xff)) , (o)[2] = ((byte)(("IEND"[2]) & 0xff)) , (o)[3] = ((byte)(("IEND"[3]) & 0xff)) , (o) += 4);
			stbiw__wpcrc(&o, (int)(0));
			(void)((!!((o) == (_out_ + *out_len))) || (_wassert("o == out + *out_len", "stb_image_write.h", (uint)(1155)) , 0));
			return _out_;
		}

		public static void stbiw__jpg_writeBits(stbi__write_context s, int* bitBufP, int* bitCntP, ushort* bs)
		{
			int bitBuf = (int)(*bitBufP);int bitCnt = (int)(*bitCntP);
			bitCnt += (int)(bs[1]);
			bitBuf |= (int)(bs[0] << (24 - bitCnt));
			while ((bitCnt) >= (8)) {
byte c = (byte)((bitBuf >> 16) & 255);stbiw__putc(s, (byte)(c));if ((c) == (255)) {
stbiw__putc(s, (byte)(0));}
bitBuf <<= 8;bitCnt -= (int)(8);}
			*bitBufP = (int)(bitBuf);
			*bitCntP = (int)(bitCnt);
		}

		public static void stbiw__jpg_DCT(float* d0p, float* d1p, float* d2p, float* d3p, float* d4p, float* d5p, float* d6p, float* d7p)
		{
			float d0 = (float)(*d0p);float d1 = (float)(*d1p);float d2 = (float)(*d2p);float d3 = (float)(*d3p);float d4 = (float)(*d4p);float d5 = (float)(*d5p);float d6 = (float)(*d6p);float d7 = (float)(*d7p);
			float z1 = 0;float z2 = 0;float z3 = 0;float z4 = 0;float z5 = 0;float z11 = 0;float z13 = 0;
			float tmp0 = (float)(d0 + d7);
			float tmp7 = (float)(d0 - d7);
			float tmp1 = (float)(d1 + d6);
			float tmp6 = (float)(d1 - d6);
			float tmp2 = (float)(d2 + d5);
			float tmp5 = (float)(d2 - d5);
			float tmp3 = (float)(d3 + d4);
			float tmp4 = (float)(d3 - d4);
			float tmp10 = (float)(tmp0 + tmp3);
			float tmp13 = (float)(tmp0 - tmp3);
			float tmp11 = (float)(tmp1 + tmp2);
			float tmp12 = (float)(tmp1 - tmp2);
			d0 = (float)(tmp10 + tmp11);
			d4 = (float)(tmp10 - tmp11);
			z1 = (float)((tmp12 + tmp13) * 0.707106781f);
			d2 = (float)(tmp13 + z1);
			d6 = (float)(tmp13 - z1);
			tmp10 = (float)(tmp4 + tmp5);
			tmp11 = (float)(tmp5 + tmp6);
			tmp12 = (float)(tmp6 + tmp7);
			z5 = (float)((tmp10 - tmp12) * 0.382683433f);
			z2 = (float)(tmp10 * 0.541196100f + z5);
			z4 = (float)(tmp12 * 1.306562965f + z5);
			z3 = (float)(tmp11 * 0.707106781f);
			z11 = (float)(tmp7 + z3);
			z13 = (float)(tmp7 - z3);
			*d5p = (float)(z13 + z2);
			*d3p = (float)(z13 - z2);
			*d1p = (float)(z11 + z4);
			*d7p = (float)(z11 - z4);
			*d0p = (float)(d0);
			*d2p = (float)(d2);
			*d4p = (float)(d4);
			*d6p = (float)(d6);
		}

		public static void stbiw__jpg_calcBits(int val, ushort* bits)
		{
			int tmp1 = (int)((val) < (0)?-val:val);
			val = (int)((val) < (0)?val - 1:val);
			bits[1] = (ushort)(1);
			while ((tmp1 >>= 1) != 0) {
++bits[1];}
			bits[0] = (ushort)(val & ((1 << bits[1]) - 1));
		}

		public static int stbiw__jpg_processDU(stbi__write_context s, int* bitBuf, int* bitCnt, float* CDU, float* fdtbl, int DC, ushort** HTDC, ushort** HTAC)
		{
			ushort* EOB = stackalloc ushort[2];
EOB[0] = (ushort)(HTAC[0x00][0]);
EOB[1] = (ushort)(HTAC[0x00][1]);

			ushort* M16zeroes = stackalloc ushort[2];
M16zeroes[0] = (ushort)(HTAC[0xF0][0]);
M16zeroes[1] = (ushort)(HTAC[0xF0][1]);

			int dataOff = 0;int i = 0;int diff = 0;int end0pos = 0;
			int* DU = stackalloc int[64];
			for (dataOff = (int)(0); (dataOff) < (64); dataOff += (int)(8)) {
stbiw__jpg_DCT(&CDU[dataOff], &CDU[dataOff + 1], &CDU[dataOff + 2], &CDU[dataOff + 3], &CDU[dataOff + 4], &CDU[dataOff + 5], &CDU[dataOff + 6], &CDU[dataOff + 7]);}
			for (dataOff = (int)(0); (dataOff) < (8); ++dataOff) {
stbiw__jpg_DCT(&CDU[dataOff], &CDU[dataOff + 8], &CDU[dataOff + 16], &CDU[dataOff + 24], &CDU[dataOff + 32], &CDU[dataOff + 40], &CDU[dataOff + 48], &CDU[dataOff + 56]);}
			for (i = (int)(0); (i) < (64); ++i) {
float v = (float)(CDU[i] * fdtbl[i]);DU[stbiw__jpg_ZigZag[i]] = ((int)((v) < (0)?v - 0.5f:v + 0.5f));}
			diff = (int)(DU[0] - DC);
			if ((diff) == (0)) {
stbiw__jpg_writeBits(s, bitBuf, bitCnt, HTDC[0]);}
 else {
ushort* bits = stackalloc ushort[2];stbiw__jpg_calcBits((int)(diff), bits);stbiw__jpg_writeBits(s, bitBuf, bitCnt, HTDC[bits[1]]);stbiw__jpg_writeBits(s, bitBuf, bitCnt, bits);}

			end0pos = (int)(63);
			for (; ((end0pos) > (0)) && ((DU[end0pos]) == (0)); --end0pos) {
}
			if ((end0pos) == (0)) {
stbiw__jpg_writeBits(s, bitBuf, bitCnt, EOB);return (int)(DU[0]);}

			for (i = (int)(1); i <= end0pos; ++i) {
int startpos = (int)(i);int nrzeroes = 0;ushort* bits = stackalloc ushort[2];for (; ((DU[i]) == (0)) && (i <= end0pos); ++i) {
}nrzeroes = (int)(i - startpos);if ((nrzeroes) >= (16)) {
int lng = (int)(nrzeroes >> 4);int nrmarker = 0;for (nrmarker = (int)(1); nrmarker <= lng; ++nrmarker) {stbiw__jpg_writeBits(s, bitBuf, bitCnt, M16zeroes);}nrzeroes &= (int)(15);}
stbiw__jpg_calcBits((int)(DU[i]), bits);stbiw__jpg_writeBits(s, bitBuf, bitCnt, HTAC[(nrzeroes << 4) + bits[1]]);stbiw__jpg_writeBits(s, bitBuf, bitCnt, bits);}
			if (end0pos != 63) {
stbiw__jpg_writeBits(s, bitBuf, bitCnt, EOB);}

			return (int)(DU[0]);
		}

		public static int stbi_write_jpg_core(stbi__write_context s, int width, int height, int comp, void * data, int quality)
		{
			byte* std_dc_luminance_nrcodes = stackalloc byte[17];
std_dc_luminance_nrcodes[0] = (byte)(0);
std_dc_luminance_nrcodes[1] = (byte)(0);
std_dc_luminance_nrcodes[2] = (byte)(1);
std_dc_luminance_nrcodes[3] = (byte)(5);
std_dc_luminance_nrcodes[4] = (byte)(1);
std_dc_luminance_nrcodes[5] = (byte)(1);
std_dc_luminance_nrcodes[6] = (byte)(1);
std_dc_luminance_nrcodes[7] = (byte)(1);
std_dc_luminance_nrcodes[8] = (byte)(1);
std_dc_luminance_nrcodes[9] = (byte)(1);
std_dc_luminance_nrcodes[10] = (byte)(0);
std_dc_luminance_nrcodes[11] = (byte)(0);
std_dc_luminance_nrcodes[12] = (byte)(0);
std_dc_luminance_nrcodes[13] = (byte)(0);
std_dc_luminance_nrcodes[14] = (byte)(0);
std_dc_luminance_nrcodes[15] = (byte)(0);
std_dc_luminance_nrcodes[16] = (byte)(0);

			byte* std_dc_luminance_values = stackalloc byte[12];
std_dc_luminance_values[0] = (byte)(0);
std_dc_luminance_values[1] = (byte)(1);
std_dc_luminance_values[2] = (byte)(2);
std_dc_luminance_values[3] = (byte)(3);
std_dc_luminance_values[4] = (byte)(4);
std_dc_luminance_values[5] = (byte)(5);
std_dc_luminance_values[6] = (byte)(6);
std_dc_luminance_values[7] = (byte)(7);
std_dc_luminance_values[8] = (byte)(8);
std_dc_luminance_values[9] = (byte)(9);
std_dc_luminance_values[10] = (byte)(10);
std_dc_luminance_values[11] = (byte)(11);

			byte* std_ac_luminance_nrcodes = stackalloc byte[17];
std_ac_luminance_nrcodes[0] = (byte)(0);
std_ac_luminance_nrcodes[1] = (byte)(0);
std_ac_luminance_nrcodes[2] = (byte)(2);
std_ac_luminance_nrcodes[3] = (byte)(1);
std_ac_luminance_nrcodes[4] = (byte)(3);
std_ac_luminance_nrcodes[5] = (byte)(3);
std_ac_luminance_nrcodes[6] = (byte)(2);
std_ac_luminance_nrcodes[7] = (byte)(4);
std_ac_luminance_nrcodes[8] = (byte)(3);
std_ac_luminance_nrcodes[9] = (byte)(5);
std_ac_luminance_nrcodes[10] = (byte)(5);
std_ac_luminance_nrcodes[11] = (byte)(4);
std_ac_luminance_nrcodes[12] = (byte)(4);
std_ac_luminance_nrcodes[13] = (byte)(0);
std_ac_luminance_nrcodes[14] = (byte)(0);
std_ac_luminance_nrcodes[15] = (byte)(1);
std_ac_luminance_nrcodes[16] = (byte)(0x7d);

			byte* std_ac_luminance_values = stackalloc byte[162];
std_ac_luminance_values[0] = (byte)(0x01);
std_ac_luminance_values[1] = (byte)(0x02);
std_ac_luminance_values[2] = (byte)(0x03);
std_ac_luminance_values[3] = (byte)(0x00);
std_ac_luminance_values[4] = (byte)(0x04);
std_ac_luminance_values[5] = (byte)(0x11);
std_ac_luminance_values[6] = (byte)(0x05);
std_ac_luminance_values[7] = (byte)(0x12);
std_ac_luminance_values[8] = (byte)(0x21);
std_ac_luminance_values[9] = (byte)(0x31);
std_ac_luminance_values[10] = (byte)(0x41);
std_ac_luminance_values[11] = (byte)(0x06);
std_ac_luminance_values[12] = (byte)(0x13);
std_ac_luminance_values[13] = (byte)(0x51);
std_ac_luminance_values[14] = (byte)(0x61);
std_ac_luminance_values[15] = (byte)(0x07);
std_ac_luminance_values[16] = (byte)(0x22);
std_ac_luminance_values[17] = (byte)(0x71);
std_ac_luminance_values[18] = (byte)(0x14);
std_ac_luminance_values[19] = (byte)(0x32);
std_ac_luminance_values[20] = (byte)(0x81);
std_ac_luminance_values[21] = (byte)(0x91);
std_ac_luminance_values[22] = (byte)(0xa1);
std_ac_luminance_values[23] = (byte)(0x08);
std_ac_luminance_values[24] = (byte)(0x23);
std_ac_luminance_values[25] = (byte)(0x42);
std_ac_luminance_values[26] = (byte)(0xb1);
std_ac_luminance_values[27] = (byte)(0xc1);
std_ac_luminance_values[28] = (byte)(0x15);
std_ac_luminance_values[29] = (byte)(0x52);
std_ac_luminance_values[30] = (byte)(0xd1);
std_ac_luminance_values[31] = (byte)(0xf0);
std_ac_luminance_values[32] = (byte)(0x24);
std_ac_luminance_values[33] = (byte)(0x33);
std_ac_luminance_values[34] = (byte)(0x62);
std_ac_luminance_values[35] = (byte)(0x72);
std_ac_luminance_values[36] = (byte)(0x82);
std_ac_luminance_values[37] = (byte)(0x09);
std_ac_luminance_values[38] = (byte)(0x0a);
std_ac_luminance_values[39] = (byte)(0x16);
std_ac_luminance_values[40] = (byte)(0x17);
std_ac_luminance_values[41] = (byte)(0x18);
std_ac_luminance_values[42] = (byte)(0x19);
std_ac_luminance_values[43] = (byte)(0x1a);
std_ac_luminance_values[44] = (byte)(0x25);
std_ac_luminance_values[45] = (byte)(0x26);
std_ac_luminance_values[46] = (byte)(0x27);
std_ac_luminance_values[47] = (byte)(0x28);
std_ac_luminance_values[48] = (byte)(0x29);
std_ac_luminance_values[49] = (byte)(0x2a);
std_ac_luminance_values[50] = (byte)(0x34);
std_ac_luminance_values[51] = (byte)(0x35);
std_ac_luminance_values[52] = (byte)(0x36);
std_ac_luminance_values[53] = (byte)(0x37);
std_ac_luminance_values[54] = (byte)(0x38);
std_ac_luminance_values[55] = (byte)(0x39);
std_ac_luminance_values[56] = (byte)(0x3a);
std_ac_luminance_values[57] = (byte)(0x43);
std_ac_luminance_values[58] = (byte)(0x44);
std_ac_luminance_values[59] = (byte)(0x45);
std_ac_luminance_values[60] = (byte)(0x46);
std_ac_luminance_values[61] = (byte)(0x47);
std_ac_luminance_values[62] = (byte)(0x48);
std_ac_luminance_values[63] = (byte)(0x49);
std_ac_luminance_values[64] = (byte)(0x4a);
std_ac_luminance_values[65] = (byte)(0x53);
std_ac_luminance_values[66] = (byte)(0x54);
std_ac_luminance_values[67] = (byte)(0x55);
std_ac_luminance_values[68] = (byte)(0x56);
std_ac_luminance_values[69] = (byte)(0x57);
std_ac_luminance_values[70] = (byte)(0x58);
std_ac_luminance_values[71] = (byte)(0x59);
std_ac_luminance_values[72] = (byte)(0x5a);
std_ac_luminance_values[73] = (byte)(0x63);
std_ac_luminance_values[74] = (byte)(0x64);
std_ac_luminance_values[75] = (byte)(0x65);
std_ac_luminance_values[76] = (byte)(0x66);
std_ac_luminance_values[77] = (byte)(0x67);
std_ac_luminance_values[78] = (byte)(0x68);
std_ac_luminance_values[79] = (byte)(0x69);
std_ac_luminance_values[80] = (byte)(0x6a);
std_ac_luminance_values[81] = (byte)(0x73);
std_ac_luminance_values[82] = (byte)(0x74);
std_ac_luminance_values[83] = (byte)(0x75);
std_ac_luminance_values[84] = (byte)(0x76);
std_ac_luminance_values[85] = (byte)(0x77);
std_ac_luminance_values[86] = (byte)(0x78);
std_ac_luminance_values[87] = (byte)(0x79);
std_ac_luminance_values[88] = (byte)(0x7a);
std_ac_luminance_values[89] = (byte)(0x83);
std_ac_luminance_values[90] = (byte)(0x84);
std_ac_luminance_values[91] = (byte)(0x85);
std_ac_luminance_values[92] = (byte)(0x86);
std_ac_luminance_values[93] = (byte)(0x87);
std_ac_luminance_values[94] = (byte)(0x88);
std_ac_luminance_values[95] = (byte)(0x89);
std_ac_luminance_values[96] = (byte)(0x8a);
std_ac_luminance_values[97] = (byte)(0x92);
std_ac_luminance_values[98] = (byte)(0x93);
std_ac_luminance_values[99] = (byte)(0x94);
std_ac_luminance_values[100] = (byte)(0x95);
std_ac_luminance_values[101] = (byte)(0x96);
std_ac_luminance_values[102] = (byte)(0x97);
std_ac_luminance_values[103] = (byte)(0x98);
std_ac_luminance_values[104] = (byte)(0x99);
std_ac_luminance_values[105] = (byte)(0x9a);
std_ac_luminance_values[106] = (byte)(0xa2);
std_ac_luminance_values[107] = (byte)(0xa3);
std_ac_luminance_values[108] = (byte)(0xa4);
std_ac_luminance_values[109] = (byte)(0xa5);
std_ac_luminance_values[110] = (byte)(0xa6);
std_ac_luminance_values[111] = (byte)(0xa7);
std_ac_luminance_values[112] = (byte)(0xa8);
std_ac_luminance_values[113] = (byte)(0xa9);
std_ac_luminance_values[114] = (byte)(0xaa);
std_ac_luminance_values[115] = (byte)(0xb2);
std_ac_luminance_values[116] = (byte)(0xb3);
std_ac_luminance_values[117] = (byte)(0xb4);
std_ac_luminance_values[118] = (byte)(0xb5);
std_ac_luminance_values[119] = (byte)(0xb6);
std_ac_luminance_values[120] = (byte)(0xb7);
std_ac_luminance_values[121] = (byte)(0xb8);
std_ac_luminance_values[122] = (byte)(0xb9);
std_ac_luminance_values[123] = (byte)(0xba);
std_ac_luminance_values[124] = (byte)(0xc2);
std_ac_luminance_values[125] = (byte)(0xc3);
std_ac_luminance_values[126] = (byte)(0xc4);
std_ac_luminance_values[127] = (byte)(0xc5);
std_ac_luminance_values[128] = (byte)(0xc6);
std_ac_luminance_values[129] = (byte)(0xc7);
std_ac_luminance_values[130] = (byte)(0xc8);
std_ac_luminance_values[131] = (byte)(0xc9);
std_ac_luminance_values[132] = (byte)(0xca);
std_ac_luminance_values[133] = (byte)(0xd2);
std_ac_luminance_values[134] = (byte)(0xd3);
std_ac_luminance_values[135] = (byte)(0xd4);
std_ac_luminance_values[136] = (byte)(0xd5);
std_ac_luminance_values[137] = (byte)(0xd6);
std_ac_luminance_values[138] = (byte)(0xd7);
std_ac_luminance_values[139] = (byte)(0xd8);
std_ac_luminance_values[140] = (byte)(0xd9);
std_ac_luminance_values[141] = (byte)(0xda);
std_ac_luminance_values[142] = (byte)(0xe1);
std_ac_luminance_values[143] = (byte)(0xe2);
std_ac_luminance_values[144] = (byte)(0xe3);
std_ac_luminance_values[145] = (byte)(0xe4);
std_ac_luminance_values[146] = (byte)(0xe5);
std_ac_luminance_values[147] = (byte)(0xe6);
std_ac_luminance_values[148] = (byte)(0xe7);
std_ac_luminance_values[149] = (byte)(0xe8);
std_ac_luminance_values[150] = (byte)(0xe9);
std_ac_luminance_values[151] = (byte)(0xea);
std_ac_luminance_values[152] = (byte)(0xf1);
std_ac_luminance_values[153] = (byte)(0xf2);
std_ac_luminance_values[154] = (byte)(0xf3);
std_ac_luminance_values[155] = (byte)(0xf4);
std_ac_luminance_values[156] = (byte)(0xf5);
std_ac_luminance_values[157] = (byte)(0xf6);
std_ac_luminance_values[158] = (byte)(0xf7);
std_ac_luminance_values[159] = (byte)(0xf8);
std_ac_luminance_values[160] = (byte)(0xf9);
std_ac_luminance_values[161] = (byte)(0xfa);

			byte* std_dc_chrominance_nrcodes = stackalloc byte[17];
std_dc_chrominance_nrcodes[0] = (byte)(0);
std_dc_chrominance_nrcodes[1] = (byte)(0);
std_dc_chrominance_nrcodes[2] = (byte)(3);
std_dc_chrominance_nrcodes[3] = (byte)(1);
std_dc_chrominance_nrcodes[4] = (byte)(1);
std_dc_chrominance_nrcodes[5] = (byte)(1);
std_dc_chrominance_nrcodes[6] = (byte)(1);
std_dc_chrominance_nrcodes[7] = (byte)(1);
std_dc_chrominance_nrcodes[8] = (byte)(1);
std_dc_chrominance_nrcodes[9] = (byte)(1);
std_dc_chrominance_nrcodes[10] = (byte)(1);
std_dc_chrominance_nrcodes[11] = (byte)(1);
std_dc_chrominance_nrcodes[12] = (byte)(0);
std_dc_chrominance_nrcodes[13] = (byte)(0);
std_dc_chrominance_nrcodes[14] = (byte)(0);
std_dc_chrominance_nrcodes[15] = (byte)(0);
std_dc_chrominance_nrcodes[16] = (byte)(0);

			byte* std_dc_chrominance_values = stackalloc byte[12];
std_dc_chrominance_values[0] = (byte)(0);
std_dc_chrominance_values[1] = (byte)(1);
std_dc_chrominance_values[2] = (byte)(2);
std_dc_chrominance_values[3] = (byte)(3);
std_dc_chrominance_values[4] = (byte)(4);
std_dc_chrominance_values[5] = (byte)(5);
std_dc_chrominance_values[6] = (byte)(6);
std_dc_chrominance_values[7] = (byte)(7);
std_dc_chrominance_values[8] = (byte)(8);
std_dc_chrominance_values[9] = (byte)(9);
std_dc_chrominance_values[10] = (byte)(10);
std_dc_chrominance_values[11] = (byte)(11);

			byte* std_ac_chrominance_nrcodes = stackalloc byte[17];
std_ac_chrominance_nrcodes[0] = (byte)(0);
std_ac_chrominance_nrcodes[1] = (byte)(0);
std_ac_chrominance_nrcodes[2] = (byte)(2);
std_ac_chrominance_nrcodes[3] = (byte)(1);
std_ac_chrominance_nrcodes[4] = (byte)(2);
std_ac_chrominance_nrcodes[5] = (byte)(4);
std_ac_chrominance_nrcodes[6] = (byte)(4);
std_ac_chrominance_nrcodes[7] = (byte)(3);
std_ac_chrominance_nrcodes[8] = (byte)(4);
std_ac_chrominance_nrcodes[9] = (byte)(7);
std_ac_chrominance_nrcodes[10] = (byte)(5);
std_ac_chrominance_nrcodes[11] = (byte)(4);
std_ac_chrominance_nrcodes[12] = (byte)(4);
std_ac_chrominance_nrcodes[13] = (byte)(0);
std_ac_chrominance_nrcodes[14] = (byte)(1);
std_ac_chrominance_nrcodes[15] = (byte)(2);
std_ac_chrominance_nrcodes[16] = (byte)(0x77);

			byte* std_ac_chrominance_values = stackalloc byte[162];
std_ac_chrominance_values[0] = (byte)(0x00);
std_ac_chrominance_values[1] = (byte)(0x01);
std_ac_chrominance_values[2] = (byte)(0x02);
std_ac_chrominance_values[3] = (byte)(0x03);
std_ac_chrominance_values[4] = (byte)(0x11);
std_ac_chrominance_values[5] = (byte)(0x04);
std_ac_chrominance_values[6] = (byte)(0x05);
std_ac_chrominance_values[7] = (byte)(0x21);
std_ac_chrominance_values[8] = (byte)(0x31);
std_ac_chrominance_values[9] = (byte)(0x06);
std_ac_chrominance_values[10] = (byte)(0x12);
std_ac_chrominance_values[11] = (byte)(0x41);
std_ac_chrominance_values[12] = (byte)(0x51);
std_ac_chrominance_values[13] = (byte)(0x07);
std_ac_chrominance_values[14] = (byte)(0x61);
std_ac_chrominance_values[15] = (byte)(0x71);
std_ac_chrominance_values[16] = (byte)(0x13);
std_ac_chrominance_values[17] = (byte)(0x22);
std_ac_chrominance_values[18] = (byte)(0x32);
std_ac_chrominance_values[19] = (byte)(0x81);
std_ac_chrominance_values[20] = (byte)(0x08);
std_ac_chrominance_values[21] = (byte)(0x14);
std_ac_chrominance_values[22] = (byte)(0x42);
std_ac_chrominance_values[23] = (byte)(0x91);
std_ac_chrominance_values[24] = (byte)(0xa1);
std_ac_chrominance_values[25] = (byte)(0xb1);
std_ac_chrominance_values[26] = (byte)(0xc1);
std_ac_chrominance_values[27] = (byte)(0x09);
std_ac_chrominance_values[28] = (byte)(0x23);
std_ac_chrominance_values[29] = (byte)(0x33);
std_ac_chrominance_values[30] = (byte)(0x52);
std_ac_chrominance_values[31] = (byte)(0xf0);
std_ac_chrominance_values[32] = (byte)(0x15);
std_ac_chrominance_values[33] = (byte)(0x62);
std_ac_chrominance_values[34] = (byte)(0x72);
std_ac_chrominance_values[35] = (byte)(0xd1);
std_ac_chrominance_values[36] = (byte)(0x0a);
std_ac_chrominance_values[37] = (byte)(0x16);
std_ac_chrominance_values[38] = (byte)(0x24);
std_ac_chrominance_values[39] = (byte)(0x34);
std_ac_chrominance_values[40] = (byte)(0xe1);
std_ac_chrominance_values[41] = (byte)(0x25);
std_ac_chrominance_values[42] = (byte)(0xf1);
std_ac_chrominance_values[43] = (byte)(0x17);
std_ac_chrominance_values[44] = (byte)(0x18);
std_ac_chrominance_values[45] = (byte)(0x19);
std_ac_chrominance_values[46] = (byte)(0x1a);
std_ac_chrominance_values[47] = (byte)(0x26);
std_ac_chrominance_values[48] = (byte)(0x27);
std_ac_chrominance_values[49] = (byte)(0x28);
std_ac_chrominance_values[50] = (byte)(0x29);
std_ac_chrominance_values[51] = (byte)(0x2a);
std_ac_chrominance_values[52] = (byte)(0x35);
std_ac_chrominance_values[53] = (byte)(0x36);
std_ac_chrominance_values[54] = (byte)(0x37);
std_ac_chrominance_values[55] = (byte)(0x38);
std_ac_chrominance_values[56] = (byte)(0x39);
std_ac_chrominance_values[57] = (byte)(0x3a);
std_ac_chrominance_values[58] = (byte)(0x43);
std_ac_chrominance_values[59] = (byte)(0x44);
std_ac_chrominance_values[60] = (byte)(0x45);
std_ac_chrominance_values[61] = (byte)(0x46);
std_ac_chrominance_values[62] = (byte)(0x47);
std_ac_chrominance_values[63] = (byte)(0x48);
std_ac_chrominance_values[64] = (byte)(0x49);
std_ac_chrominance_values[65] = (byte)(0x4a);
std_ac_chrominance_values[66] = (byte)(0x53);
std_ac_chrominance_values[67] = (byte)(0x54);
std_ac_chrominance_values[68] = (byte)(0x55);
std_ac_chrominance_values[69] = (byte)(0x56);
std_ac_chrominance_values[70] = (byte)(0x57);
std_ac_chrominance_values[71] = (byte)(0x58);
std_ac_chrominance_values[72] = (byte)(0x59);
std_ac_chrominance_values[73] = (byte)(0x5a);
std_ac_chrominance_values[74] = (byte)(0x63);
std_ac_chrominance_values[75] = (byte)(0x64);
std_ac_chrominance_values[76] = (byte)(0x65);
std_ac_chrominance_values[77] = (byte)(0x66);
std_ac_chrominance_values[78] = (byte)(0x67);
std_ac_chrominance_values[79] = (byte)(0x68);
std_ac_chrominance_values[80] = (byte)(0x69);
std_ac_chrominance_values[81] = (byte)(0x6a);
std_ac_chrominance_values[82] = (byte)(0x73);
std_ac_chrominance_values[83] = (byte)(0x74);
std_ac_chrominance_values[84] = (byte)(0x75);
std_ac_chrominance_values[85] = (byte)(0x76);
std_ac_chrominance_values[86] = (byte)(0x77);
std_ac_chrominance_values[87] = (byte)(0x78);
std_ac_chrominance_values[88] = (byte)(0x79);
std_ac_chrominance_values[89] = (byte)(0x7a);
std_ac_chrominance_values[90] = (byte)(0x82);
std_ac_chrominance_values[91] = (byte)(0x83);
std_ac_chrominance_values[92] = (byte)(0x84);
std_ac_chrominance_values[93] = (byte)(0x85);
std_ac_chrominance_values[94] = (byte)(0x86);
std_ac_chrominance_values[95] = (byte)(0x87);
std_ac_chrominance_values[96] = (byte)(0x88);
std_ac_chrominance_values[97] = (byte)(0x89);
std_ac_chrominance_values[98] = (byte)(0x8a);
std_ac_chrominance_values[99] = (byte)(0x92);
std_ac_chrominance_values[100] = (byte)(0x93);
std_ac_chrominance_values[101] = (byte)(0x94);
std_ac_chrominance_values[102] = (byte)(0x95);
std_ac_chrominance_values[103] = (byte)(0x96);
std_ac_chrominance_values[104] = (byte)(0x97);
std_ac_chrominance_values[105] = (byte)(0x98);
std_ac_chrominance_values[106] = (byte)(0x99);
std_ac_chrominance_values[107] = (byte)(0x9a);
std_ac_chrominance_values[108] = (byte)(0xa2);
std_ac_chrominance_values[109] = (byte)(0xa3);
std_ac_chrominance_values[110] = (byte)(0xa4);
std_ac_chrominance_values[111] = (byte)(0xa5);
std_ac_chrominance_values[112] = (byte)(0xa6);
std_ac_chrominance_values[113] = (byte)(0xa7);
std_ac_chrominance_values[114] = (byte)(0xa8);
std_ac_chrominance_values[115] = (byte)(0xa9);
std_ac_chrominance_values[116] = (byte)(0xaa);
std_ac_chrominance_values[117] = (byte)(0xb2);
std_ac_chrominance_values[118] = (byte)(0xb3);
std_ac_chrominance_values[119] = (byte)(0xb4);
std_ac_chrominance_values[120] = (byte)(0xb5);
std_ac_chrominance_values[121] = (byte)(0xb6);
std_ac_chrominance_values[122] = (byte)(0xb7);
std_ac_chrominance_values[123] = (byte)(0xb8);
std_ac_chrominance_values[124] = (byte)(0xb9);
std_ac_chrominance_values[125] = (byte)(0xba);
std_ac_chrominance_values[126] = (byte)(0xc2);
std_ac_chrominance_values[127] = (byte)(0xc3);
std_ac_chrominance_values[128] = (byte)(0xc4);
std_ac_chrominance_values[129] = (byte)(0xc5);
std_ac_chrominance_values[130] = (byte)(0xc6);
std_ac_chrominance_values[131] = (byte)(0xc7);
std_ac_chrominance_values[132] = (byte)(0xc8);
std_ac_chrominance_values[133] = (byte)(0xc9);
std_ac_chrominance_values[134] = (byte)(0xca);
std_ac_chrominance_values[135] = (byte)(0xd2);
std_ac_chrominance_values[136] = (byte)(0xd3);
std_ac_chrominance_values[137] = (byte)(0xd4);
std_ac_chrominance_values[138] = (byte)(0xd5);
std_ac_chrominance_values[139] = (byte)(0xd6);
std_ac_chrominance_values[140] = (byte)(0xd7);
std_ac_chrominance_values[141] = (byte)(0xd8);
std_ac_chrominance_values[142] = (byte)(0xd9);
std_ac_chrominance_values[143] = (byte)(0xda);
std_ac_chrominance_values[144] = (byte)(0xe2);
std_ac_chrominance_values[145] = (byte)(0xe3);
std_ac_chrominance_values[146] = (byte)(0xe4);
std_ac_chrominance_values[147] = (byte)(0xe5);
std_ac_chrominance_values[148] = (byte)(0xe6);
std_ac_chrominance_values[149] = (byte)(0xe7);
std_ac_chrominance_values[150] = (byte)(0xe8);
std_ac_chrominance_values[151] = (byte)(0xe9);
std_ac_chrominance_values[152] = (byte)(0xea);
std_ac_chrominance_values[153] = (byte)(0xf2);
std_ac_chrominance_values[154] = (byte)(0xf3);
std_ac_chrominance_values[155] = (byte)(0xf4);
std_ac_chrominance_values[156] = (byte)(0xf5);
std_ac_chrominance_values[157] = (byte)(0xf6);
std_ac_chrominance_values[158] = (byte)(0xf7);
std_ac_chrominance_values[159] = (byte)(0xf8);
std_ac_chrominance_values[160] = (byte)(0xf9);
std_ac_chrominance_values[161] = (byte)(0xfa);

			ushort** YDC_HT = stackalloc ushort[256];
YDC_HT[0] = { 0, 2 };
YDC_HT[1] = { 2, 3 };
YDC_HT[2] = { 3, 3 };
YDC_HT[3] = { 4, 3 };
YDC_HT[4] = { 5, 3 };
YDC_HT[5] = { 6, 3 };
YDC_HT[6] = { 14, 4 };
YDC_HT[7] = { 30, 5 };
YDC_HT[8] = { 62, 6 };
YDC_HT[9] = { 126, 7 };
YDC_HT[10] = { 254, 8 };
YDC_HT[11] = { 510, 9 };

			ushort** UVDC_HT = stackalloc ushort[256];
UVDC_HT[0] = { 0, 2 };
UVDC_HT[1] = { 1, 2 };
UVDC_HT[2] = { 2, 2 };
UVDC_HT[3] = { 6, 3 };
UVDC_HT[4] = { 14, 4 };
UVDC_HT[5] = { 30, 5 };
UVDC_HT[6] = { 62, 6 };
UVDC_HT[7] = { 126, 7 };
UVDC_HT[8] = { 254, 8 };
UVDC_HT[9] = { 510, 9 };
UVDC_HT[10] = { 1022, 10 };
UVDC_HT[11] = { 2046, 11 };

			ushort** YAC_HT = stackalloc ushort[256];
YAC_HT[0] = { 10, 4 };
YAC_HT[1] = { 0, 2 };
YAC_HT[2] = { 1, 2 };
YAC_HT[3] = { 4, 3 };
YAC_HT[4] = { 11, 4 };
YAC_HT[5] = { 26, 5 };
YAC_HT[6] = { 120, 7 };
YAC_HT[7] = { 248, 8 };
YAC_HT[8] = { 1014, 10 };
YAC_HT[9] = { 65410, 16 };
YAC_HT[10] = { 65411, 16 };
YAC_HT[11] = { 0, 0 };
YAC_HT[12] = { 0, 0 };
YAC_HT[13] = { 0, 0 };
YAC_HT[14] = { 0, 0 };
YAC_HT[15] = { 0, 0 };
YAC_HT[16] = { 0, 0 };
YAC_HT[17] = { 12, 4 };
YAC_HT[18] = { 27, 5 };
YAC_HT[19] = { 121, 7 };
YAC_HT[20] = { 502, 9 };
YAC_HT[21] = { 2038, 11 };
YAC_HT[22] = { 65412, 16 };
YAC_HT[23] = { 65413, 16 };
YAC_HT[24] = { 65414, 16 };
YAC_HT[25] = { 65415, 16 };
YAC_HT[26] = { 65416, 16 };
YAC_HT[27] = { 0, 0 };
YAC_HT[28] = { 0, 0 };
YAC_HT[29] = { 0, 0 };
YAC_HT[30] = { 0, 0 };
YAC_HT[31] = { 0, 0 };
YAC_HT[32] = { 0, 0 };
YAC_HT[33] = { 28, 5 };
YAC_HT[34] = { 249, 8 };
YAC_HT[35] = { 1015, 10 };
YAC_HT[36] = { 4084, 12 };
YAC_HT[37] = { 65417, 16 };
YAC_HT[38] = { 65418, 16 };
YAC_HT[39] = { 65419, 16 };
YAC_HT[40] = { 65420, 16 };
YAC_HT[41] = { 65421, 16 };
YAC_HT[42] = { 65422, 16 };
YAC_HT[43] = { 0, 0 };
YAC_HT[44] = { 0, 0 };
YAC_HT[45] = { 0, 0 };
YAC_HT[46] = { 0, 0 };
YAC_HT[47] = { 0, 0 };
YAC_HT[48] = { 0, 0 };
YAC_HT[49] = { 58, 6 };
YAC_HT[50] = { 503, 9 };
YAC_HT[51] = { 4085, 12 };
YAC_HT[52] = { 65423, 16 };
YAC_HT[53] = { 65424, 16 };
YAC_HT[54] = { 65425, 16 };
YAC_HT[55] = { 65426, 16 };
YAC_HT[56] = { 65427, 16 };
YAC_HT[57] = { 65428, 16 };
YAC_HT[58] = { 65429, 16 };
YAC_HT[59] = { 0, 0 };
YAC_HT[60] = { 0, 0 };
YAC_HT[61] = { 0, 0 };
YAC_HT[62] = { 0, 0 };
YAC_HT[63] = { 0, 0 };
YAC_HT[64] = { 0, 0 };
YAC_HT[65] = { 59, 6 };
YAC_HT[66] = { 1016, 10 };
YAC_HT[67] = { 65430, 16 };
YAC_HT[68] = { 65431, 16 };
YAC_HT[69] = { 65432, 16 };
YAC_HT[70] = { 65433, 16 };
YAC_HT[71] = { 65434, 16 };
YAC_HT[72] = { 65435, 16 };
YAC_HT[73] = { 65436, 16 };
YAC_HT[74] = { 65437, 16 };
YAC_HT[75] = { 0, 0 };
YAC_HT[76] = { 0, 0 };
YAC_HT[77] = { 0, 0 };
YAC_HT[78] = { 0, 0 };
YAC_HT[79] = { 0, 0 };
YAC_HT[80] = { 0, 0 };
YAC_HT[81] = { 122, 7 };
YAC_HT[82] = { 2039, 11 };
YAC_HT[83] = { 65438, 16 };
YAC_HT[84] = { 65439, 16 };
YAC_HT[85] = { 65440, 16 };
YAC_HT[86] = { 65441, 16 };
YAC_HT[87] = { 65442, 16 };
YAC_HT[88] = { 65443, 16 };
YAC_HT[89] = { 65444, 16 };
YAC_HT[90] = { 65445, 16 };
YAC_HT[91] = { 0, 0 };
YAC_HT[92] = { 0, 0 };
YAC_HT[93] = { 0, 0 };
YAC_HT[94] = { 0, 0 };
YAC_HT[95] = { 0, 0 };
YAC_HT[96] = { 0, 0 };
YAC_HT[97] = { 123, 7 };
YAC_HT[98] = { 4086, 12 };
YAC_HT[99] = { 65446, 16 };
YAC_HT[100] = { 65447, 16 };
YAC_HT[101] = { 65448, 16 };
YAC_HT[102] = { 65449, 16 };
YAC_HT[103] = { 65450, 16 };
YAC_HT[104] = { 65451, 16 };
YAC_HT[105] = { 65452, 16 };
YAC_HT[106] = { 65453, 16 };
YAC_HT[107] = { 0, 0 };
YAC_HT[108] = { 0, 0 };
YAC_HT[109] = { 0, 0 };
YAC_HT[110] = { 0, 0 };
YAC_HT[111] = { 0, 0 };
YAC_HT[112] = { 0, 0 };
YAC_HT[113] = { 250, 8 };
YAC_HT[114] = { 4087, 12 };
YAC_HT[115] = { 65454, 16 };
YAC_HT[116] = { 65455, 16 };
YAC_HT[117] = { 65456, 16 };
YAC_HT[118] = { 65457, 16 };
YAC_HT[119] = { 65458, 16 };
YAC_HT[120] = { 65459, 16 };
YAC_HT[121] = { 65460, 16 };
YAC_HT[122] = { 65461, 16 };
YAC_HT[123] = { 0, 0 };
YAC_HT[124] = { 0, 0 };
YAC_HT[125] = { 0, 0 };
YAC_HT[126] = { 0, 0 };
YAC_HT[127] = { 0, 0 };
YAC_HT[128] = { 0, 0 };
YAC_HT[129] = { 504, 9 };
YAC_HT[130] = { 32704, 15 };
YAC_HT[131] = { 65462, 16 };
YAC_HT[132] = { 65463, 16 };
YAC_HT[133] = { 65464, 16 };
YAC_HT[134] = { 65465, 16 };
YAC_HT[135] = { 65466, 16 };
YAC_HT[136] = { 65467, 16 };
YAC_HT[137] = { 65468, 16 };
YAC_HT[138] = { 65469, 16 };
YAC_HT[139] = { 0, 0 };
YAC_HT[140] = { 0, 0 };
YAC_HT[141] = { 0, 0 };
YAC_HT[142] = { 0, 0 };
YAC_HT[143] = { 0, 0 };
YAC_HT[144] = { 0, 0 };
YAC_HT[145] = { 505, 9 };
YAC_HT[146] = { 65470, 16 };
YAC_HT[147] = { 65471, 16 };
YAC_HT[148] = { 65472, 16 };
YAC_HT[149] = { 65473, 16 };
YAC_HT[150] = { 65474, 16 };
YAC_HT[151] = { 65475, 16 };
YAC_HT[152] = { 65476, 16 };
YAC_HT[153] = { 65477, 16 };
YAC_HT[154] = { 65478, 16 };
YAC_HT[155] = { 0, 0 };
YAC_HT[156] = { 0, 0 };
YAC_HT[157] = { 0, 0 };
YAC_HT[158] = { 0, 0 };
YAC_HT[159] = { 0, 0 };
YAC_HT[160] = { 0, 0 };
YAC_HT[161] = { 506, 9 };
YAC_HT[162] = { 65479, 16 };
YAC_HT[163] = { 65480, 16 };
YAC_HT[164] = { 65481, 16 };
YAC_HT[165] = { 65482, 16 };
YAC_HT[166] = { 65483, 16 };
YAC_HT[167] = { 65484, 16 };
YAC_HT[168] = { 65485, 16 };
YAC_HT[169] = { 65486, 16 };
YAC_HT[170] = { 65487, 16 };
YAC_HT[171] = { 0, 0 };
YAC_HT[172] = { 0, 0 };
YAC_HT[173] = { 0, 0 };
YAC_HT[174] = { 0, 0 };
YAC_HT[175] = { 0, 0 };
YAC_HT[176] = { 0, 0 };
YAC_HT[177] = { 1017, 10 };
YAC_HT[178] = { 65488, 16 };
YAC_HT[179] = { 65489, 16 };
YAC_HT[180] = { 65490, 16 };
YAC_HT[181] = { 65491, 16 };
YAC_HT[182] = { 65492, 16 };
YAC_HT[183] = { 65493, 16 };
YAC_HT[184] = { 65494, 16 };
YAC_HT[185] = { 65495, 16 };
YAC_HT[186] = { 65496, 16 };
YAC_HT[187] = { 0, 0 };
YAC_HT[188] = { 0, 0 };
YAC_HT[189] = { 0, 0 };
YAC_HT[190] = { 0, 0 };
YAC_HT[191] = { 0, 0 };
YAC_HT[192] = { 0, 0 };
YAC_HT[193] = { 1018, 10 };
YAC_HT[194] = { 65497, 16 };
YAC_HT[195] = { 65498, 16 };
YAC_HT[196] = { 65499, 16 };
YAC_HT[197] = { 65500, 16 };
YAC_HT[198] = { 65501, 16 };
YAC_HT[199] = { 65502, 16 };
YAC_HT[200] = { 65503, 16 };
YAC_HT[201] = { 65504, 16 };
YAC_HT[202] = { 65505, 16 };
YAC_HT[203] = { 0, 0 };
YAC_HT[204] = { 0, 0 };
YAC_HT[205] = { 0, 0 };
YAC_HT[206] = { 0, 0 };
YAC_HT[207] = { 0, 0 };
YAC_HT[208] = { 0, 0 };
YAC_HT[209] = { 2040, 11 };
YAC_HT[210] = { 65506, 16 };
YAC_HT[211] = { 65507, 16 };
YAC_HT[212] = { 65508, 16 };
YAC_HT[213] = { 65509, 16 };
YAC_HT[214] = { 65510, 16 };
YAC_HT[215] = { 65511, 16 };
YAC_HT[216] = { 65512, 16 };
YAC_HT[217] = { 65513, 16 };
YAC_HT[218] = { 65514, 16 };
YAC_HT[219] = { 0, 0 };
YAC_HT[220] = { 0, 0 };
YAC_HT[221] = { 0, 0 };
YAC_HT[222] = { 0, 0 };
YAC_HT[223] = { 0, 0 };
YAC_HT[224] = { 0, 0 };
YAC_HT[225] = { 65515, 16 };
YAC_HT[226] = { 65516, 16 };
YAC_HT[227] = { 65517, 16 };
YAC_HT[228] = { 65518, 16 };
YAC_HT[229] = { 65519, 16 };
YAC_HT[230] = { 65520, 16 };
YAC_HT[231] = { 65521, 16 };
YAC_HT[232] = { 65522, 16 };
YAC_HT[233] = { 65523, 16 };
YAC_HT[234] = { 65524, 16 };
YAC_HT[235] = { 0, 0 };
YAC_HT[236] = { 0, 0 };
YAC_HT[237] = { 0, 0 };
YAC_HT[238] = { 0, 0 };
YAC_HT[239] = { 0, 0 };
YAC_HT[240] = { 2041, 11 };
YAC_HT[241] = { 65525, 16 };
YAC_HT[242] = { 65526, 16 };
YAC_HT[243] = { 65527, 16 };
YAC_HT[244] = { 65528, 16 };
YAC_HT[245] = { 65529, 16 };
YAC_HT[246] = { 65530, 16 };
YAC_HT[247] = { 65531, 16 };
YAC_HT[248] = { 65532, 16 };
YAC_HT[249] = { 65533, 16 };
YAC_HT[250] = { 65534, 16 };
YAC_HT[251] = { 0, 0 };
YAC_HT[252] = { 0, 0 };
YAC_HT[253] = { 0, 0 };
YAC_HT[254] = { 0, 0 };
YAC_HT[255] = { 0, 0 };

			ushort** UVAC_HT = stackalloc ushort[256];
UVAC_HT[0] = { 0, 2 };
UVAC_HT[1] = { 1, 2 };
UVAC_HT[2] = { 4, 3 };
UVAC_HT[3] = { 10, 4 };
UVAC_HT[4] = { 24, 5 };
UVAC_HT[5] = { 25, 5 };
UVAC_HT[6] = { 56, 6 };
UVAC_HT[7] = { 120, 7 };
UVAC_HT[8] = { 500, 9 };
UVAC_HT[9] = { 1014, 10 };
UVAC_HT[10] = { 4084, 12 };
UVAC_HT[11] = { 0, 0 };
UVAC_HT[12] = { 0, 0 };
UVAC_HT[13] = { 0, 0 };
UVAC_HT[14] = { 0, 0 };
UVAC_HT[15] = { 0, 0 };
UVAC_HT[16] = { 0, 0 };
UVAC_HT[17] = { 11, 4 };
UVAC_HT[18] = { 57, 6 };
UVAC_HT[19] = { 246, 8 };
UVAC_HT[20] = { 501, 9 };
UVAC_HT[21] = { 2038, 11 };
UVAC_HT[22] = { 4085, 12 };
UVAC_HT[23] = { 65416, 16 };
UVAC_HT[24] = { 65417, 16 };
UVAC_HT[25] = { 65418, 16 };
UVAC_HT[26] = { 65419, 16 };
UVAC_HT[27] = { 0, 0 };
UVAC_HT[28] = { 0, 0 };
UVAC_HT[29] = { 0, 0 };
UVAC_HT[30] = { 0, 0 };
UVAC_HT[31] = { 0, 0 };
UVAC_HT[32] = { 0, 0 };
UVAC_HT[33] = { 26, 5 };
UVAC_HT[34] = { 247, 8 };
UVAC_HT[35] = { 1015, 10 };
UVAC_HT[36] = { 4086, 12 };
UVAC_HT[37] = { 32706, 15 };
UVAC_HT[38] = { 65420, 16 };
UVAC_HT[39] = { 65421, 16 };
UVAC_HT[40] = { 65422, 16 };
UVAC_HT[41] = { 65423, 16 };
UVAC_HT[42] = { 65424, 16 };
UVAC_HT[43] = { 0, 0 };
UVAC_HT[44] = { 0, 0 };
UVAC_HT[45] = { 0, 0 };
UVAC_HT[46] = { 0, 0 };
UVAC_HT[47] = { 0, 0 };
UVAC_HT[48] = { 0, 0 };
UVAC_HT[49] = { 27, 5 };
UVAC_HT[50] = { 248, 8 };
UVAC_HT[51] = { 1016, 10 };
UVAC_HT[52] = { 4087, 12 };
UVAC_HT[53] = { 65425, 16 };
UVAC_HT[54] = { 65426, 16 };
UVAC_HT[55] = { 65427, 16 };
UVAC_HT[56] = { 65428, 16 };
UVAC_HT[57] = { 65429, 16 };
UVAC_HT[58] = { 65430, 16 };
UVAC_HT[59] = { 0, 0 };
UVAC_HT[60] = { 0, 0 };
UVAC_HT[61] = { 0, 0 };
UVAC_HT[62] = { 0, 0 };
UVAC_HT[63] = { 0, 0 };
UVAC_HT[64] = { 0, 0 };
UVAC_HT[65] = { 58, 6 };
UVAC_HT[66] = { 502, 9 };
UVAC_HT[67] = { 65431, 16 };
UVAC_HT[68] = { 65432, 16 };
UVAC_HT[69] = { 65433, 16 };
UVAC_HT[70] = { 65434, 16 };
UVAC_HT[71] = { 65435, 16 };
UVAC_HT[72] = { 65436, 16 };
UVAC_HT[73] = { 65437, 16 };
UVAC_HT[74] = { 65438, 16 };
UVAC_HT[75] = { 0, 0 };
UVAC_HT[76] = { 0, 0 };
UVAC_HT[77] = { 0, 0 };
UVAC_HT[78] = { 0, 0 };
UVAC_HT[79] = { 0, 0 };
UVAC_HT[80] = { 0, 0 };
UVAC_HT[81] = { 59, 6 };
UVAC_HT[82] = { 1017, 10 };
UVAC_HT[83] = { 65439, 16 };
UVAC_HT[84] = { 65440, 16 };
UVAC_HT[85] = { 65441, 16 };
UVAC_HT[86] = { 65442, 16 };
UVAC_HT[87] = { 65443, 16 };
UVAC_HT[88] = { 65444, 16 };
UVAC_HT[89] = { 65445, 16 };
UVAC_HT[90] = { 65446, 16 };
UVAC_HT[91] = { 0, 0 };
UVAC_HT[92] = { 0, 0 };
UVAC_HT[93] = { 0, 0 };
UVAC_HT[94] = { 0, 0 };
UVAC_HT[95] = { 0, 0 };
UVAC_HT[96] = { 0, 0 };
UVAC_HT[97] = { 121, 7 };
UVAC_HT[98] = { 2039, 11 };
UVAC_HT[99] = { 65447, 16 };
UVAC_HT[100] = { 65448, 16 };
UVAC_HT[101] = { 65449, 16 };
UVAC_HT[102] = { 65450, 16 };
UVAC_HT[103] = { 65451, 16 };
UVAC_HT[104] = { 65452, 16 };
UVAC_HT[105] = { 65453, 16 };
UVAC_HT[106] = { 65454, 16 };
UVAC_HT[107] = { 0, 0 };
UVAC_HT[108] = { 0, 0 };
UVAC_HT[109] = { 0, 0 };
UVAC_HT[110] = { 0, 0 };
UVAC_HT[111] = { 0, 0 };
UVAC_HT[112] = { 0, 0 };
UVAC_HT[113] = { 122, 7 };
UVAC_HT[114] = { 2040, 11 };
UVAC_HT[115] = { 65455, 16 };
UVAC_HT[116] = { 65456, 16 };
UVAC_HT[117] = { 65457, 16 };
UVAC_HT[118] = { 65458, 16 };
UVAC_HT[119] = { 65459, 16 };
UVAC_HT[120] = { 65460, 16 };
UVAC_HT[121] = { 65461, 16 };
UVAC_HT[122] = { 65462, 16 };
UVAC_HT[123] = { 0, 0 };
UVAC_HT[124] = { 0, 0 };
UVAC_HT[125] = { 0, 0 };
UVAC_HT[126] = { 0, 0 };
UVAC_HT[127] = { 0, 0 };
UVAC_HT[128] = { 0, 0 };
UVAC_HT[129] = { 249, 8 };
UVAC_HT[130] = { 65463, 16 };
UVAC_HT[131] = { 65464, 16 };
UVAC_HT[132] = { 65465, 16 };
UVAC_HT[133] = { 65466, 16 };
UVAC_HT[134] = { 65467, 16 };
UVAC_HT[135] = { 65468, 16 };
UVAC_HT[136] = { 65469, 16 };
UVAC_HT[137] = { 65470, 16 };
UVAC_HT[138] = { 65471, 16 };
UVAC_HT[139] = { 0, 0 };
UVAC_HT[140] = { 0, 0 };
UVAC_HT[141] = { 0, 0 };
UVAC_HT[142] = { 0, 0 };
UVAC_HT[143] = { 0, 0 };
UVAC_HT[144] = { 0, 0 };
UVAC_HT[145] = { 503, 9 };
UVAC_HT[146] = { 65472, 16 };
UVAC_HT[147] = { 65473, 16 };
UVAC_HT[148] = { 65474, 16 };
UVAC_HT[149] = { 65475, 16 };
UVAC_HT[150] = { 65476, 16 };
UVAC_HT[151] = { 65477, 16 };
UVAC_HT[152] = { 65478, 16 };
UVAC_HT[153] = { 65479, 16 };
UVAC_HT[154] = { 65480, 16 };
UVAC_HT[155] = { 0, 0 };
UVAC_HT[156] = { 0, 0 };
UVAC_HT[157] = { 0, 0 };
UVAC_HT[158] = { 0, 0 };
UVAC_HT[159] = { 0, 0 };
UVAC_HT[160] = { 0, 0 };
UVAC_HT[161] = { 504, 9 };
UVAC_HT[162] = { 65481, 16 };
UVAC_HT[163] = { 65482, 16 };
UVAC_HT[164] = { 65483, 16 };
UVAC_HT[165] = { 65484, 16 };
UVAC_HT[166] = { 65485, 16 };
UVAC_HT[167] = { 65486, 16 };
UVAC_HT[168] = { 65487, 16 };
UVAC_HT[169] = { 65488, 16 };
UVAC_HT[170] = { 65489, 16 };
UVAC_HT[171] = { 0, 0 };
UVAC_HT[172] = { 0, 0 };
UVAC_HT[173] = { 0, 0 };
UVAC_HT[174] = { 0, 0 };
UVAC_HT[175] = { 0, 0 };
UVAC_HT[176] = { 0, 0 };
UVAC_HT[177] = { 505, 9 };
UVAC_HT[178] = { 65490, 16 };
UVAC_HT[179] = { 65491, 16 };
UVAC_HT[180] = { 65492, 16 };
UVAC_HT[181] = { 65493, 16 };
UVAC_HT[182] = { 65494, 16 };
UVAC_HT[183] = { 65495, 16 };
UVAC_HT[184] = { 65496, 16 };
UVAC_HT[185] = { 65497, 16 };
UVAC_HT[186] = { 65498, 16 };
UVAC_HT[187] = { 0, 0 };
UVAC_HT[188] = { 0, 0 };
UVAC_HT[189] = { 0, 0 };
UVAC_HT[190] = { 0, 0 };
UVAC_HT[191] = { 0, 0 };
UVAC_HT[192] = { 0, 0 };
UVAC_HT[193] = { 506, 9 };
UVAC_HT[194] = { 65499, 16 };
UVAC_HT[195] = { 65500, 16 };
UVAC_HT[196] = { 65501, 16 };
UVAC_HT[197] = { 65502, 16 };
UVAC_HT[198] = { 65503, 16 };
UVAC_HT[199] = { 65504, 16 };
UVAC_HT[200] = { 65505, 16 };
UVAC_HT[201] = { 65506, 16 };
UVAC_HT[202] = { 65507, 16 };
UVAC_HT[203] = { 0, 0 };
UVAC_HT[204] = { 0, 0 };
UVAC_HT[205] = { 0, 0 };
UVAC_HT[206] = { 0, 0 };
UVAC_HT[207] = { 0, 0 };
UVAC_HT[208] = { 0, 0 };
UVAC_HT[209] = { 2041, 11 };
UVAC_HT[210] = { 65508, 16 };
UVAC_HT[211] = { 65509, 16 };
UVAC_HT[212] = { 65510, 16 };
UVAC_HT[213] = { 65511, 16 };
UVAC_HT[214] = { 65512, 16 };
UVAC_HT[215] = { 65513, 16 };
UVAC_HT[216] = { 65514, 16 };
UVAC_HT[217] = { 65515, 16 };
UVAC_HT[218] = { 65516, 16 };
UVAC_HT[219] = { 0, 0 };
UVAC_HT[220] = { 0, 0 };
UVAC_HT[221] = { 0, 0 };
UVAC_HT[222] = { 0, 0 };
UVAC_HT[223] = { 0, 0 };
UVAC_HT[224] = { 0, 0 };
UVAC_HT[225] = { 16352, 14 };
UVAC_HT[226] = { 65517, 16 };
UVAC_HT[227] = { 65518, 16 };
UVAC_HT[228] = { 65519, 16 };
UVAC_HT[229] = { 65520, 16 };
UVAC_HT[230] = { 65521, 16 };
UVAC_HT[231] = { 65522, 16 };
UVAC_HT[232] = { 65523, 16 };
UVAC_HT[233] = { 65524, 16 };
UVAC_HT[234] = { 65525, 16 };
UVAC_HT[235] = { 0, 0 };
UVAC_HT[236] = { 0, 0 };
UVAC_HT[237] = { 0, 0 };
UVAC_HT[238] = { 0, 0 };
UVAC_HT[239] = { 0, 0 };
UVAC_HT[240] = { 1018, 10 };
UVAC_HT[241] = { 32707, 15 };
UVAC_HT[242] = { 65526, 16 };
UVAC_HT[243] = { 65527, 16 };
UVAC_HT[244] = { 65528, 16 };
UVAC_HT[245] = { 65529, 16 };
UVAC_HT[246] = { 65530, 16 };
UVAC_HT[247] = { 65531, 16 };
UVAC_HT[248] = { 65532, 16 };
UVAC_HT[249] = { 65533, 16 };
UVAC_HT[250] = { 65534, 16 };
UVAC_HT[251] = { 0, 0 };
UVAC_HT[252] = { 0, 0 };
UVAC_HT[253] = { 0, 0 };
UVAC_HT[254] = { 0, 0 };
UVAC_HT[255] = { 0, 0 };

			int* YQT = stackalloc int[64];
YQT[0] = (int)(16);
YQT[1] = (int)(11);
YQT[2] = (int)(10);
YQT[3] = (int)(16);
YQT[4] = (int)(24);
YQT[5] = (int)(40);
YQT[6] = (int)(51);
YQT[7] = (int)(61);
YQT[8] = (int)(12);
YQT[9] = (int)(12);
YQT[10] = (int)(14);
YQT[11] = (int)(19);
YQT[12] = (int)(26);
YQT[13] = (int)(58);
YQT[14] = (int)(60);
YQT[15] = (int)(55);
YQT[16] = (int)(14);
YQT[17] = (int)(13);
YQT[18] = (int)(16);
YQT[19] = (int)(24);
YQT[20] = (int)(40);
YQT[21] = (int)(57);
YQT[22] = (int)(69);
YQT[23] = (int)(56);
YQT[24] = (int)(14);
YQT[25] = (int)(17);
YQT[26] = (int)(22);
YQT[27] = (int)(29);
YQT[28] = (int)(51);
YQT[29] = (int)(87);
YQT[30] = (int)(80);
YQT[31] = (int)(62);
YQT[32] = (int)(18);
YQT[33] = (int)(22);
YQT[34] = (int)(37);
YQT[35] = (int)(56);
YQT[36] = (int)(68);
YQT[37] = (int)(109);
YQT[38] = (int)(103);
YQT[39] = (int)(77);
YQT[40] = (int)(24);
YQT[41] = (int)(35);
YQT[42] = (int)(55);
YQT[43] = (int)(64);
YQT[44] = (int)(81);
YQT[45] = (int)(104);
YQT[46] = (int)(113);
YQT[47] = (int)(92);
YQT[48] = (int)(49);
YQT[49] = (int)(64);
YQT[50] = (int)(78);
YQT[51] = (int)(87);
YQT[52] = (int)(103);
YQT[53] = (int)(121);
YQT[54] = (int)(120);
YQT[55] = (int)(101);
YQT[56] = (int)(72);
YQT[57] = (int)(92);
YQT[58] = (int)(95);
YQT[59] = (int)(98);
YQT[60] = (int)(112);
YQT[61] = (int)(100);
YQT[62] = (int)(103);
YQT[63] = (int)(99);

			int* UVQT = stackalloc int[64];
UVQT[0] = (int)(17);
UVQT[1] = (int)(18);
UVQT[2] = (int)(24);
UVQT[3] = (int)(47);
UVQT[4] = (int)(99);
UVQT[5] = (int)(99);
UVQT[6] = (int)(99);
UVQT[7] = (int)(99);
UVQT[8] = (int)(18);
UVQT[9] = (int)(21);
UVQT[10] = (int)(26);
UVQT[11] = (int)(66);
UVQT[12] = (int)(99);
UVQT[13] = (int)(99);
UVQT[14] = (int)(99);
UVQT[15] = (int)(99);
UVQT[16] = (int)(24);
UVQT[17] = (int)(26);
UVQT[18] = (int)(56);
UVQT[19] = (int)(99);
UVQT[20] = (int)(99);
UVQT[21] = (int)(99);
UVQT[22] = (int)(99);
UVQT[23] = (int)(99);
UVQT[24] = (int)(47);
UVQT[25] = (int)(66);
UVQT[26] = (int)(99);
UVQT[27] = (int)(99);
UVQT[28] = (int)(99);
UVQT[29] = (int)(99);
UVQT[30] = (int)(99);
UVQT[31] = (int)(99);
UVQT[32] = (int)(99);
UVQT[33] = (int)(99);
UVQT[34] = (int)(99);
UVQT[35] = (int)(99);
UVQT[36] = (int)(99);
UVQT[37] = (int)(99);
UVQT[38] = (int)(99);
UVQT[39] = (int)(99);
UVQT[40] = (int)(99);
UVQT[41] = (int)(99);
UVQT[42] = (int)(99);
UVQT[43] = (int)(99);
UVQT[44] = (int)(99);
UVQT[45] = (int)(99);
UVQT[46] = (int)(99);
UVQT[47] = (int)(99);
UVQT[48] = (int)(99);
UVQT[49] = (int)(99);
UVQT[50] = (int)(99);
UVQT[51] = (int)(99);
UVQT[52] = (int)(99);
UVQT[53] = (int)(99);
UVQT[54] = (int)(99);
UVQT[55] = (int)(99);
UVQT[56] = (int)(99);
UVQT[57] = (int)(99);
UVQT[58] = (int)(99);
UVQT[59] = (int)(99);
UVQT[60] = (int)(99);
UVQT[61] = (int)(99);
UVQT[62] = (int)(99);
UVQT[63] = (int)(99);

			float* aasf = stackalloc float[8];
aasf[0] = (float)(1.0f * 2.828427125f);
aasf[1] = (float)(1.387039845f * 2.828427125f);
aasf[2] = (float)(1.306562965f * 2.828427125f);
aasf[3] = (float)(1.175875602f * 2.828427125f);
aasf[4] = (float)(1.0f * 2.828427125f);
aasf[5] = (float)(0.785694958f * 2.828427125f);
aasf[6] = (float)(0.541196100f * 2.828427125f);
aasf[7] = (float)(0.275899379f * 2.828427125f);

			int row = 0;int col = 0;int i = 0;int k = 0;
			float* fdtbl_Y = stackalloc float[64];float* fdtbl_UV = stackalloc float[64];
			byte* YTable = stackalloc byte[64];byte* UVTable = stackalloc byte[64];
			if (((((data== null) || (width== 0)) || (height== 0)) || ((comp) > (4))) || ((comp) < (1))) {
return (int)(0);}

			quality = (int)((quality) != 0?quality:90);
			quality = (int)((quality) < (1)?1:(quality) > (100)?100:quality);
			quality = (int)((quality) < (50)?5000 / quality:200 - quality * 2);
			for (i = (int)(0); (i) < (64); ++i) {
int uvti = 0;int yti = (int)((YQT[i] * quality + 50) / 100);YTable[stbiw__jpg_ZigZag[i]] = ((byte)((yti) < (1)?1:(yti) > (255)?255:yti));uvti = (int)((UVQT[i] * quality + 50) / 100);UVTable[stbiw__jpg_ZigZag[i]] = ((byte)((uvti) < (1)?1:(uvti) > (255)?255:uvti));}
			for (row = (int)(0) , k = (int)(0); (row) < (8); ++row) {
for (col = (int)(0); (col) < (8); ++col , ++k) {
fdtbl_Y[k] = (float)(1 / (YTable[stbiw__jpg_ZigZag[k]] * aasf[row] * aasf[col]));fdtbl_UV[k] = (float)(1 / (UVTable[stbiw__jpg_ZigZag[k]] * aasf[row] * aasf[col]));}}
			{
byte* head0 = stackalloc byte[25];
head0[0] = (byte)(0xFF);
head0[1] = (byte)(0xD8);
head0[2] = (byte)(0xFF);
head0[3] = (byte)(0xE0);
head0[4] = (byte)(0);
head0[5] = (byte)(0x10);
head0[6] = (byte)('J');
head0[7] = (byte)('F');
head0[8] = (byte)('I');
head0[9] = (byte)('F');
head0[10] = (byte)(0);
head0[11] = (byte)(1);
head0[12] = (byte)(1);
head0[13] = (byte)(0);
head0[14] = (byte)(0);
head0[15] = (byte)(1);
head0[16] = (byte)(0);
head0[17] = (byte)(1);
head0[18] = (byte)(0);
head0[19] = (byte)(0);
head0[20] = (byte)(0xFF);
head0[21] = (byte)(0xDB);
head0[22] = (byte)(0);
head0[23] = (byte)(0x84);
head0[24] = (byte)(0);
byte* head2 = stackalloc byte[14];
head2[0] = (byte)(0xFF);
head2[1] = (byte)(0xDA);
head2[2] = (byte)(0);
head2[3] = (byte)(0xC);
head2[4] = (byte)(3);
head2[5] = (byte)(1);
head2[6] = (byte)(0);
head2[7] = (byte)(2);
head2[8] = (byte)(0x11);
head2[9] = (byte)(3);
head2[10] = (byte)(0x11);
head2[11] = (byte)(0);
head2[12] = (byte)(0x3F);
head2[13] = (byte)(0);
byte* head1 = stackalloc byte[24];
head1[0] = (byte)(0xFF);
head1[1] = (byte)(0xC0);
head1[2] = (byte)(0);
head1[3] = (byte)(0x11);
head1[4] = (byte)(8);
head1[5] = (byte)(height >> 8);
head1[6] = (byte)((height) & 0xff);
head1[7] = (byte)(width >> 8);
head1[8] = (byte)((width) & 0xff);
head1[9] = (byte)(3);
head1[10] = (byte)(1);
head1[11] = (byte)(0x11);
head1[12] = (byte)(0);
head1[13] = (byte)(2);
head1[14] = (byte)(0x11);
head1[15] = (byte)(1);
head1[16] = (byte)(3);
head1[17] = (byte)(0x11);
head1[18] = (byte)(1);
head1[19] = (byte)(0xFF);
head1[20] = (byte)(0xC4);
head1[21] = (byte)(0x01);
head1[22] = (byte)(0xA2);
head1[23] = (byte)(0);
s.func(s.context, (void *)(head0), (int)(sizeof((head0))));s.func(s.context, (void *)(YTable), (int)(sizeof((YTable))));stbiw__putc(s, (byte)(1));s.func(s.context, UVTable, (int)(sizeof((UVTable))));s.func(s.context, (void *)(head1), (int)(sizeof((head1))));s.func(s.context, (void *)(std_dc_luminance_nrcodes + 1), (int)(sizeof((std_dc_luminance_nrcodes)) - 1));s.func(s.context, (void *)(std_dc_luminance_values), (int)(sizeof((std_dc_luminance_values))));stbiw__putc(s, (byte)(0x10));s.func(s.context, (void *)(std_ac_luminance_nrcodes + 1), (int)(sizeof((std_ac_luminance_nrcodes)) - 1));s.func(s.context, (void *)(std_ac_luminance_values), (int)(sizeof((std_ac_luminance_values))));stbiw__putc(s, (byte)(1));s.func(s.context, (void *)(std_dc_chrominance_nrcodes + 1), (int)(sizeof((std_dc_chrominance_nrcodes)) - 1));s.func(s.context, (void *)(std_dc_chrominance_values), (int)(sizeof((std_dc_chrominance_values))));stbiw__putc(s, (byte)(0x11));s.func(s.context, (void *)(std_ac_chrominance_nrcodes + 1), (int)(sizeof((std_ac_chrominance_nrcodes)) - 1));s.func(s.context, (void *)(std_ac_chrominance_values), (int)(sizeof((std_ac_chrominance_values))));s.func(s.context, (void *)(head2), (int)(sizeof((head2))));}

			{
ushort* fillBits = stackalloc ushort[2];
fillBits[0] = (ushort)(0x7F);
fillBits[1] = (ushort)(7);
byte* imageData = (byte*)(data);int DCY = (int)(0);int DCU = (int)(0);int DCV = (int)(0);int bitBuf = (int)(0);int bitCnt = (int)(0);int ofsG = (int)((comp) > (2)?1:0);int ofsB = (int)((comp) > (2)?2:0);int x = 0;int y = 0;int pos = 0;for (y = (int)(0); (y) < (height); y += (int)(8)) {
for (x = (int)(0); (x) < (width); x += (int)(8)) {
float* YDU = stackalloc float[64];float* UDU = stackalloc float[64];float* VDU = stackalloc float[64];for (row = (int)(y) , pos = (int)(0); (row) < (y + 8); ++row) {
int clamped_row = (int)(((row) < (height))?row:height - 1);int base_p = (int)(((stbi__flip_vertically_on_write) != 0?(height - 1 - clamped_row):clamped_row) * width * comp);for (col = (int)(x); (col) < (x + 8); ++col , ++pos) {
float r = 0;float g = 0;float b = 0;int p = (int)(base_p + (((col) < (width))?col:(width - 1)) * comp);r = (float)(imageData[p + 0]);g = (float)(imageData[p + ofsG]);b = (float)(imageData[p + ofsB]);YDU[pos] = (float)(+0.29900f * r + 0.58700f * g + 0.11400f * b - 128);UDU[pos] = (float)(-0.16874f * r - 0.33126f * g + 0.50000f * b);VDU[pos] = (float)(+0.50000f * r - 0.41869f * g - 0.08131f * b);}}DCY = (int)(stbiw__jpg_processDU(s, &bitBuf, &bitCnt, YDU, fdtbl_Y, (int)(DCY), YDC_HT, YAC_HT));DCU = (int)(stbiw__jpg_processDU(s, &bitBuf, &bitCnt, UDU, fdtbl_UV, (int)(DCU), UVDC_HT, UVAC_HT));DCV = (int)(stbiw__jpg_processDU(s, &bitBuf, &bitCnt, VDU, fdtbl_UV, (int)(DCV), UVDC_HT, UVAC_HT));}}stbiw__jpg_writeBits(s, &bitBuf, &bitCnt, fillBits);}

			stbiw__putc(s, (byte)(0xFF));
			stbiw__putc(s, (byte)(0xD9));
			return (int)(1);
		}

		public static int stbi_write_jpg_to_func(void (void *, void *, int)* func, void * context, int x, int y, int comp, void * data, int quality)
		{
			stbi__write_context s = new stbi__write_context();
			stbi__start_write_callbacks(s, func, context);
			return (int)(stbi_write_jpg_core(s, (int)(x), (int)(y), (int)(comp), data, (int)(quality)));
		}

	}
}
