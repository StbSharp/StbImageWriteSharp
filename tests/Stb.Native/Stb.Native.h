// Stb.Native.h

#pragma once

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;
using namespace System::Threading;

#include <stdio.h>
#include <vector>
#include <functional>

#define STBI_WRITE_NO_STDIO
#define STB_IMAGE_WRITE_IMPLEMENTATION
#define STB_IMAGE_WRITE_STATIC
#include "../../generation/StbImageWriteSharp.Generator/stb_image_write.h"

namespace StbNative {
	void write_func(void *context, void *data, int size);

	public ref class Native
	{
	public:
		static Dictionary<int, Stream ^> ^writeInfo = gcnew Dictionary<int, Stream ^>();
		static int _id = 0;

		static int GenerateId()
		{
			int %trackRefCounter = _id;
			return System::Threading::Interlocked::Increment(trackRefCounter);
		}

		// TODO: Add your methods for this class here.
		static void save_to_stream(array<unsigned char> ^bytes, int x, int y, int comp, int type, Stream ^output)
		{
			Monitor::Enter(writeInfo);
			int id;
			try {
				id = GenerateId();

				writeInfo->Add(id, output);
			}
			finally
			{
				Monitor::Exit(writeInfo);
			}

			pin_ptr<unsigned char> p = &bytes[0];
			unsigned char *ptr = (unsigned char *)p;

			std::vector<float> ff;
			switch (type)
			{
				case 0:
					stbi_write_bmp_to_func(write_func, (void *)id, x, y, comp, ptr);
					break;
				case 1:
					stbi_write_tga_to_func(write_func, (void *)id, x, y, comp, ptr);
					break;
				case 2:
				{
					ff.resize(bytes->Length);
					for (int i = 0; i < bytes->Length; ++i)
					{
						ff[i] = (float)(bytes[i] / 255.0f);
					}

					stbi_write_hdr_to_func(write_func, (void *)id, x, y, comp, &ff[0]);
					break;
				}
				case 3:
					stbi_write_png_to_func(write_func, (void *)id, x, y, comp, ptr, x * comp);
					break;
			}

			Monitor::Enter(writeInfo);
			try {
				writeInfo->Remove(id);
			}
			finally
			{
				Monitor::Exit(writeInfo);
			}
		}

		static void save_to_jpg(array<unsigned char> ^bytes, int x, int y, int comp, Stream ^output, int quality)
		{
			Monitor::Enter(writeInfo);
			int id;
			try {
				id = GenerateId();
				writeInfo->Add(id, output);
			}
			finally
			{
				Monitor::Exit(writeInfo);
			}

			pin_ptr<unsigned char> p = &bytes[0];
			unsigned char *ptr = (unsigned char *)p;

			stbi_write_jpg_to_func(write_func, (void *)id, x, y, comp, ptr, quality);

			Monitor::Enter(writeInfo);
			try {
				writeInfo->Remove(id);
			}
			finally
			{
				Monitor::Exit(writeInfo);
			}
		}
	};

	void write_func(void *context, void *data, int size)
	{
		Stream ^ info;
		Monitor::Enter(Native::writeInfo);
		int id = (int)context;
		try {
			info = Native::writeInfo[id];
		}
		finally
		{
			Monitor::Exit(Native::writeInfo);
		}

		unsigned char *bptr = (unsigned char *)data;
		for (int i = 0; i < size; ++i)
		{
			info->WriteByte(*bptr);
			++bptr;
		}
	}
}
