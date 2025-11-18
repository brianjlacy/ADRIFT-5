# ADRIFT-5 Migration Guide: VB.NET (.NET Framework 4.7.2) to C# (.NET 8)

## Overview

This document describes the automated migration of the ADRIFT-5 project from Visual Basic.NET targeting .NET Framework 4.7.2 to C# targeting .NET 8.

**Migration Date:** 2025-11-18
**Original Version:** 5.0.36.6
**Files Converted:** 170 VB.NET files → C# files
**Lines of Code:** ~106,500

## What Was Done

### 1. Language Conversion (VB.NET → C#)

All 170 VB.NET files were automatically converted to C# using a custom Python-based converter (`vb_to_csharp_converter.py`). The converter handles:

- **Module → Static Class**: VB.NET modules converted to C# static classes
- **Properties**: VB.NET property syntax converted to C# auto-properties and standard properties
- **Methods**: Functions and Subs converted to C# methods with proper return types
- **Conditional Compilation**: `#If Generator/Runner/www/Mono Then` converted to C# preprocessor directives
- **Type Conversions**: VB.NET types (Integer, String, Boolean, etc.) → C# types (int, string, bool, etc.)
- **Operators**: `AndAlso/OrElse` → `&&/||`, `Nothing` → `null`, etc.
- **Control Structures**: If/Then, Select Case, For Each, While, etc.

### 2. Project Files (.NET Framework → .NET 8 SDK-Style)

Created new SDK-style `.csproj` files for .NET 8:

- **Developer.csproj** - IDE for creating adventures (dev500.exe) - Windows Forms, .NET 8
- **Runner.csproj** - Windows player (run500.exe) - Windows Forms, .NET 8
- **MonoRunner.csproj** - Cross-platform player (MonoRunner.exe) - Windows Forms, .NET 8
- **WebRunner.csproj** - ASP.NET web-based player (WebRunner.dll) - ASP.NET Core, .NET 8

### 3. Solution File

Created new `ADRIFT5.sln` with all four projects configured for .NET 8.

## Known Issues & Required Manual Work

### CRITICAL: Dependencies Requiring Updates

#### 1. **Infragistics Controls** (Developer Project)

**Status:** ⚠️ REQUIRES MANUAL ACTION

The original project uses Infragistics WinForms controls (v20.1):
- `Infragistics4.Win.UltraWinDock`
- `Infragistics4.Win.UltraWinToolbars`
- `Infragistics4.Win.UltraWinTree`
- And 8 other Infragistics libraries

**Options:**
1. **Purchase Infragistics .NET 8 compatible version** (if available)
2. **Replace with open-source alternatives:**
   - Docking: DockPanelSuite, AvalonDock
   - Tree/Grid controls: Use standard .NET 8 WinForms controls
   - Ribbons/Toolbars: Use .NET 8 ToolStrip or migrate to WPF/MAUI

**Action Required:** The project files currently have Infragistics references commented out. You must either:
- Add the correct .NET 8-compatible Infragistics package references
- Refactor the UI code to use alternative controls

#### 2. **DirectX AudioVideoPlayback** (Developer & Runner Projects)

**Status:** ⚠️ REQUIRES MANUAL ACTION

The original project uses `Microsoft.DirectX.AudioVideoPlayback` for multimedia, which is **not available for .NET Core/.NET 8**.

**Replacement:** NAudio has been added as a dependency:
```xml
<PackageReference Include="NAudio" Version="2.2.1" />
```

**Action Required:**
- Refactor all DirectX audio/video code to use NAudio or another .NET 8-compatible library
- Search for usages of `Microsoft.DirectX` namespace and replace

**Files likely affected:**
- `clsSound.cs`
- `Runner.cs`
- `Mono.cs`

#### 3. **zlib.net** → SharpZipLib

**Status:** ✅ HANDLED

Replaced `zlib.net.dll` with modern `SharpZipLib` NuGet package (v1.4.2).

**Action Required:**
- Review compression/decompression code in `Blorb.cs` and other files
- Update namespace imports from `zlib` to `ICSharpCode.SharpZipLib`

### VB.NET to C# Conversion Issues

The automated converter is comprehensive but not perfect. **Manual review is required for:**

#### 1. **VB-Specific Features**

- **My.** namespace (VB.NET): Not available in C#
  - `My.Computer.FileSystem` → Use `System.IO`
  - `My.Settings` → Use configuration system
  - Files affected: Check `My Project` folder

- **Late Binding**: VB.NET's `Option Strict Off` allowed late binding
  - C# requires explicit types
  - Check for `object` types that might need casting

- **Default Properties**: VB.NET allows default indexed properties
  - C# requires explicit indexer syntax
  - Look for collection access patterns

#### 2. **String Handling**

- **VB String Functions**: Replace with C# equivalents
  - `Left()`, `Right()`, `Mid()` → `Substring()`, LINQ methods
  - `Len()` → `.Length`
  - `InStr()` → `IndexOf()`

- **String Comparison**: VB.NET default is case-insensitive
  - Review string comparisons and add `StringComparison.OrdinalIgnoreCase` where needed

#### 3. **Error Handling**

- **On Error GoTo**: VB.NET error handling not available in C#
  - Must use try/catch blocks
  - Search for any legacy error handling

#### 4. **Windows Forms Designer Files**

**Status:** ⚠️ REQUIRES REVIEW

All `*.Designer.cs` files were auto-converted but may have issues:
- Control initialization order
- Event handler wiring
- Resource file references

**Action Required:**
- Test each form by opening in Visual Studio designer
- Fix any designer errors manually
- Regenerate designer code if necessary

#### 5. **ASP.NET Web Forms (WebRunner Project)**

**Status:** ⚠️ MAJOR WORK REQUIRED

The WebRunner project uses **ASP.NET Web Forms**, which is **not supported in .NET 8**.

**Options:**
1. **Keep Web Forms on .NET Framework** (not ideal for modern deployment)
2. **Migrate to ASP.NET Core** (recommended):
   - Blazor Server (similar feel to Web Forms)
   - Blazor WebAssembly (runs in browser)
   - ASP.NET Core MVC/Razor Pages

**Files affected:**
- `Default.aspx` and code-behind
- `Map.aspx` and code-behind
- `About.aspx` and code-behind
- `Site.Master` and code-behind
- Web controls (`Map.ascx`, etc.)
- `Global.asax`

**Recommendation:** Consider rewriting WebRunner as a Blazor Server application for .NET 8.

### Platform-Specific Issues

#### Windows Forms on Linux/macOS

The MonoRunner project targets cross-platform .NET 8, but Windows Forms support on Linux/macOS is **limited**:
- Linux: Requires X11, experimental support
- macOS: No official support

**Recommendation:** Consider migrating to:
- **AvaloniaUI** - Cross-platform XAML framework
- **.NET MAUI** - Microsoft's official cross-platform framework

### Conditional Compilation

The project uses extensive conditional compilation:
- `#if Generator` - Development environment
- `#if Runner` - Runtime player
- `#if www` - Web-based player
- `#if Mono` - Cross-platform player

**Review:** All conditional compilation directives were converted, but verify:
- Correct defines in each project file
- Platform-specific code paths work correctly

## Building the Project

### Prerequisites

1. **.NET 8 SDK** (8.0.404 or later)
   ```bash
   dotnet --version  # Should show 8.0.x
   ```

2. **Visual Studio 2022** (17.8 or later) or **Visual Studio Code** with C# extension

3. **Dependencies** (after resolving Infragistics):
   - All dependencies are managed via NuGet and will auto-restore

### Build Commands

```bash
# Navigate to the C# solution directory
cd /path/to/ADRIFT-5/ADRIFT-CSharp

# Restore NuGet packages
dotnet restore

# Build all projects
dotnet build

# Build specific project
dotnet build Developer.csproj
dotnet build Runner.csproj
dotnet build MonoRunner.csproj
dotnet build WebRunner.csproj  # Will fail until Web Forms migration done

# Build Release configuration
dotnet build -c Release

# Run Developer
dotnet run --project Developer.csproj

# Run Runner
dotnet run --project Runner.csproj
```

### Expected Build Errors

On first build, you will see errors related to:
1. **Infragistics namespaces** - Resolve dependency first
2. **DirectX namespaces** - Replace with NAudio
3. **zlib namespaces** - Update to ICSharpCode.SharpZipLib
4. **Web Forms** - WebRunner won't build until migrated

## Testing Strategy

### Phase 1: Compilation
1. Resolve all dependency issues
2. Fix compiler errors in core classes
3. Ensure all projects build successfully

### Phase 2: Unit Testing
1. Test core game engine logic (`clsAdventure`, `clsTask`, etc.)
2. Test file I/O (`FileIO.cs`, `Blorb.cs`)
3. Test game state management

### Phase 3: Integration Testing
1. **Developer**: Create a simple adventure, verify all editors work
2. **Runner**: Load and play an adventure
3. **MonoRunner**: Test on Linux/macOS
4. **WebRunner**: Test web-based gameplay (after migration)

### Phase 4: Regression Testing
1. Load existing ADRIFT 5 games
2. Verify backward compatibility
3. Test all major features

## Breaking Changes: .NET Framework 4.7.2 → .NET 8

### API Removals
- `System.Configuration.ConfigurationManager` → Use `Microsoft.Extensions.Configuration`
- `System.Drawing` on non-Windows → Limited support, consider `SkiaSharp`
- Binary serialization changes → Review serialization code

### Security
- **Code Access Security (CAS)** removed
- **AppDomain** changes
- Review any security-sensitive code

### Performance
- Garbage collector improvements
- Span<T> and Memory<T> for better performance
- Consider using these in hot paths

## Recommended Next Steps

1. **Immediate (Critical):**
   - [ ] Decide on Infragistics replacement strategy
   - [ ] Replace DirectX with NAudio
   - [ ] Update zlib.net usage to SharpZipLib
   - [ ] Fix compilation errors

2. **Short-term (Important):**
   - [ ] Test and fix Windows Forms designers
   - [ ] Review and test conditional compilation
   - [ ] Set up CI/CD pipeline
   - [ ] Plan WebRunner migration strategy

3. **Medium-term:**
   - [ ] Consider cross-platform UI framework migration
   - [ ] Modernize file I/O to use async/await
   - [ ] Update to use .NET 8 features (required/init properties, etc.)
   - [ ] Performance profiling and optimization

4. **Long-term:**
   - [ ] Consider architectural improvements
   - [ ] Add comprehensive unit tests
   - [ ] Modernize UI/UX
   - [ ] Cloud deployment options

## Resources

- [.NET 8 What's New](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Porting from .NET Framework to .NET 8](https://learn.microsoft.com/en-us/dotnet/core/porting/)
- [Windows Forms in .NET](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [NAudio Documentation](https://github.com/naudio/NAudio)
- [SharpZipLib Documentation](https://github.com/icsharpcode/SharpZipLib)

## Support

For issues with the automated conversion, review the converter script:
- `/home/user/ADRIFT-5/vb_to_csharp_converter.py`

The converter handles most common patterns but may require manual fixes for edge cases.

## Version History

- **v5.0.36.6 → .NET 8 (2025-11-18)**
  - Converted from VB.NET to C#
  - Migrated from .NET Framework 4.7.2 to .NET 8
  - Updated project structure to SDK-style

---

**Note:** This is an automated migration. Thorough testing and manual review are essential before deploying to production.
