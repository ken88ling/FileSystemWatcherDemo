# FileSystemWatcherDemo
File System Watcher Demo - using C# asp.net core console

1. Upgrade to C# 7.1 above

```c#
2. Install nuget 
Microsoft.Extensions.Hosting V2.2.0
System.IO.FileSystem.Watcher V4.3.0
System.ServiceProcess.ServiceController V4.5.0
```

Modify FileWatcherDemo.csproj

```c#
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <LangVersion>7.1</LangVersion>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  </PropertyGroup>
```


Add service ServiceBaseLifetime helper https://github.com/aspnet/Hosting/blob/2a98db6a73512b8e36f55a1e6678461c34f4cc4d/samples/GenericHostSample/ServiceBaseLifetime.cs


