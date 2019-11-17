# SeeSharp

impaired vision sheet music reader 

inspired by [MagniPy](http://www.makersbox.us/2013/10/magnipy-low-vision-music-reader.html),

built with [osu-framework](https://github.com/ppy/osu-framework)

## Build

Windows 7 64-bit
```
dotnet publish -o C:\SeeSharp\win7\x64 -c RELEASE -f netcoreapp3.0 -r win7-x64 --self-contained

mkdir C:\SeeSharp\win7\x64\pages
```
Windows 7 32-bit
```
dotnet publish -o C:\SeeSharp\win7\x86 -c RELEASE -f netcoreapp3.0 -r win7-x86 --self-contained

mkdir C:\SeeSharp\win7\x86\pages
```
Windows 10 64-bit
```
dotnet publish -o C:\SeeSharp\win10\x64 -c RELEASE -f netcoreapp3.0 -r win10-x64 --self-contained

mkdir C:\SeeSharp\win10\x64\pages
```
Windows 10 32-bit
```
dotnet publish -o C:\SeeSharp\win10\x86 -c RELEASE -f netcoreapp3.0 -r win10-x86 --self-contained

mkdir C:\SeeSharp\win10\x86\pages
```
