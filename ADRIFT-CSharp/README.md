# ADRIFT 5 - C# .NET 8 Version

This is a converted version of ADRIFT 5.0.36.6, migrated from Visual Basic.NET (.NET Framework 4.7.2) to C# (.NET 8).

## ⚠️ Important Notice

This is an **automated conversion** from VB.NET to C#. While the conversion is comprehensive, **manual review and testing are required** before this code is production-ready.

**See [MIGRATION-GUIDE.md](MIGRATION-GUIDE.md) for complete details.**

## What is ADRIFT?

ADRIFT (Adventure Development & Runner - Interactive Fiction Toolkit) is a complete toolkit for creating and playing text-based adventure games (interactive fiction).

## Projects

This solution contains four projects:

1. **Developer** (dev500.exe) - Windows Forms IDE for creating adventures
2. **Runner** (run500.exe) - Windows player for playing adventures
3. **MonoRunner** (MonoRunner.exe) - Cross-platform player (Linux, macOS, Windows)
4. **WebRunner** (WebRunner.dll) - ASP.NET web-based player

## System Requirements

- **.NET 8 SDK** (8.0.404 or later)
- **Windows 10/11** (for Developer and Runner)
- **Linux/macOS** (for MonoRunner)
- **Visual Studio 2022** (17.8+) or **VS Code** with C# extension

## Quick Start

### Build

```bash
cd ADRIFT-CSharp
dotnet restore
dotnet build
```

### Run

```bash
# Developer (IDE)
dotnet run --project Developer.csproj

# Runner (Player)
dotnet run --project Runner.csproj

# Mono Runner (Cross-platform)
dotnet run --project MonoRunner.csproj
```

## Known Issues

⚠️ **Before building, you must resolve:**

1. **Infragistics Controls** - The Developer project requires Infragistics WinForms controls. You must either:
   - Install .NET 8-compatible Infragistics packages
   - Replace with alternative controls

2. **DirectX Dependency** - The audio/video playback code needs updating:
   - Replace `Microsoft.DirectX.AudioVideoPlayback` with NAudio
   - Update affected files: `clsSound.cs`, `Runner.cs`, `Mono.cs`

3. **zlib.net** - Update namespace references:
   - Change from `zlib` to `ICSharpCode.SharpZipLib`
   - Primarily affects `Blorb.cs`

4. **WebRunner** - ASP.NET Web Forms not supported in .NET 8:
   - Consider migrating to Blazor Server or Blazor WebAssembly

**See [MIGRATION-GUIDE.md](MIGRATION-GUIDE.md) for detailed information.**

## Project Structure

```
ADRIFT-CSharp/
├── ADRIFT/                  # Source code (170 C# files)
│   ├── Item Classes/        # Core data model classes
│   ├── My Project/          # Project resources
│   ├── Actions.cs           # Action execution system
│   ├── Babel.cs             # Natural language parser
│   ├── Blorb.cs             # Archive format support
│   ├── clsAdventure.cs      # Main game container
│   ├── clsTask.cs           # Tasks/actions
│   ├── frmGenerator.cs      # IDE main window
│   └── ...
├── Libraries/               # Standard libraries (.amf files)
├── Developer.csproj         # IDE project
├── Runner.csproj            # Windows player project
├── MonoRunner.csproj        # Cross-platform player project
├── WebRunner.csproj         # Web player project
├── ADRIFT5.sln             # Solution file
├── README.md               # This file
└── MIGRATION-GUIDE.md      # Detailed migration documentation
```

## Migration Information

- **Original Language:** Visual Basic.NET
- **Target Language:** C#
- **Original Framework:** .NET Framework 4.7.2
- **Target Framework:** .NET 8
- **Files Converted:** 170 VB.NET files → C# files
- **Conversion Date:** 2025-11-18
- **Conversion Method:** Automated Python script

## Contributing

This is a conversion project. For the official ADRIFT project:
- Website: http://www.adrift.co
- Original Repository: https://github.com/awlck/ADRIFT-5

## License

Copyright © Campbell Wild 1998-2025

See original ADRIFT license for terms and conditions.

## Disclaimer

This converted version is provided as-is. Testing and validation are required before production use. The automated conversion handles most VB.NET to C# patterns but may have edge cases requiring manual correction.

---

**For complete migration details, troubleshooting, and next steps, see [MIGRATION-GUIDE.md](MIGRATION-GUIDE.md).**
