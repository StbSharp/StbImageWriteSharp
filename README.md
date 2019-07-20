# StbImageWriteSharp
[![NuGet](https://img.shields.io/nuget/v/StbImageWriteSharp.svg)](https://www.nuget.org/packages/StbImageWriteSharp/) [![Build status](https://ci.appveyor.com/api/projects/status/c9eh0e4c70ki26fy?svg=true)](https://ci.appveyor.com/project/RomanShapiro/StbImageWriteSharp)

StbImageWriteSharp is C# port of the stb_image.h, which in its turn is C library to load images in JPG, PNG, BMP, TGA, PSD and GIF formats.

It is important to note, that this project is **port**(not **wrapper**). Original C code had been ported to C#. Therefore StbImageWriteSharp doesnt require any native binaries.

The porting hasn't been done by hand, but using [Sichem](https://github.com/rds1983/Sichem), which is the C to C# code converter utility.

# Usage
StbImageWriteSharp exposes API similar to stb_image_write.h. However that API is complicated and deals with raw unsafe pointers.

Thus several utility classes had been made to wrap that functionality.