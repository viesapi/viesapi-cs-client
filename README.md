# VIES API Client for .NET

This is the official repository for [VIES API](https://viesapi.eu) Client for .NET.

[VIES API](https://viesapi.eu) provides selected entrepreneurs' data using, among others, web services, programming
libraries, and dedicated applications. By using the available software (libraries, applications and Excel add-in)
your customers will be able to:

* check contractors' EU VAT number status in VIES system,
* download company details from VIES system,
* automatically fill in the invoice forms,

in the fastest possible way.

## Documentation

The documentation and samples are available [here](https://viesapi.eu/vies-programming-libraries/).

## Build

Microsoft Visual Studio 2022 is required to build this library. Simply open the solution file (viesapiLibrary.sln) in the
IDE and build the _Release_ version. You can also build it from the _Developer Command Prompt for Visual Studio_:

```bash
git clone https://github.com/viesapi/viesapi-cs-client.git
cd viesapi-cs-client

msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net35
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net452
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net462
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net472
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net48
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=netstandard2.0
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=netstandard2.0
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=netcoreapp3.1
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net5.0
msbuild viesapiLibrary.sln /t:Build /p:Configuration=Release /p:TargetFramework=net6.0
```

## How to use

Add the following dependency using the _Package Manager_ prompt:

```bash
PM> Install-Package VIESAPI.VIESAPIClient
```

The release version of the library is also published in _NuGet Gallery_. If you don't want to build the library
yourself, you can use our published version from [this](https://www.nuget.org/packages/VIESAPI.VIESAPIClient) location.


## License

This project is delivered under Apache License, Version 2.0:

- [![License (Apache 2.0)](https://img.shields.io/badge/license-Apache%20version%202.0-blue.svg?style=flat-square)](http://www.apache.org/licenses/LICENSE-2.0)