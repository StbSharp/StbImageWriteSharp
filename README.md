# StbImageWriteSharp
[![NuGet](https://img.shields.io/nuget/v/StbImageWriteSharp.svg)](https://www.nuget.org/packages/StbImageWriteSharp/) 
[![Build status](https://ci.appveyor.com/api/projects/status/2fp00srd5th2g3gv?svg=true)](https://ci.appveyor.com/project/RomanShapiro/stbimagewritesharp)

StbImageWriteSharp is C# port of the stb_image_write.h, which is C library to write images in JPG, PNG, BMP, TGA and HDR formats.

It is important to note, that this project is **port**(not **wrapper**). Original C code had been ported to C#. Therefore StbImageWriteSharp doesnt require any native binaries.

The porting hasn't been done by hand, but using [Sichem](https://github.com/rds1983/Sichem), which is the C to C# code converter utility.

# Usage
StbImageWriteSharp exposes API similar to stb_image_write.h. However that API is complicated and deals with raw unsafe pointers.

Thus utility class ImageWriter had been made to wrap that functionality.

I.e. this code saves RGBA image in PNG format:
```c#
using (Stream stream = File.OpenWrite(path))
{
	ImageWriter writer = new ImageWriter();
	writer.WritePng(data, width, height, ColorComponents.RedGreenBlueAlpha, stream);
}
```

# License
Public Domain

# Credits
* [stb](https://github.com/nothings/stb)


