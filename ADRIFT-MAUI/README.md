# ADRIFT 5 - .NET MAUI Edition

Cross-platform Interactive Fiction Development and Runtime Environment built with .NET MAUI.

## 🚀 What is This?

This is a **modernized, cross-platform version** of ADRIFT 5, migrated from Windows Forms (.NET Framework) to **.NET MAUI (.NET 8)**.

ADRIFT (Adventure Development & Runner - Interactive Fiction Toolkit) is a complete system for creating and playing text-based adventure games.

## ✨ Key Features

- **📱 Cross-Platform**: Runs on Windows, macOS, iOS, and Android
- **🎨 Modern UI**: Touch-friendly, responsive interface
- **⚡ Fast**: Built on .NET 8 for optimal performance
- **🏗️ MVVM Architecture**: Clean separation of UI and business logic
- **🔧 Extensible**: Modular design with dependency injection

## 📦 What's Included

### ADRIFT.Developer
Full-featured IDE for creating interactive fiction adventures:
- Location editor with rich text support
- Object, Character, Task, and Event editors
- Interactive map viewer
- Property management system
- Built-in testing

### ADRIFT.Runner
Cross-platform player for playing ADRIFT adventures:
- Rich text game output
- Command-line interface with autocomplete
- Map visualization
- Save/load game support
- Multimedia support (images, sound)

### ADRIFT.Core
Shared business logic library:
- Adventure data model
- Game engine
- Natural language parser (Babel)
- File I/O (TAF, Blorb formats)
- Expression evaluator

## 🛠️ Technology Stack

- **.NET 8** - Latest .NET runtime
- **.NET MAUI** - Microsoft's cross-platform UI framework
- **MVVM Toolkit** - Community Toolkit for MVVM pattern
- **Syncfusion Controls** - Advanced UI components
- **DevExpress Controls** - Rich text editing
- **CommunityToolkit.Maui** - Additional MAUI components

## 📋 Requirements

### Development
- **Visual Studio 2022** (17.8 or later) or **Visual Studio Code**
- **.NET 8 SDK** (8.0.100 or later)
- **MAUI Workload**: `dotnet workload install maui`

### Platform-Specific
- **Windows**: Windows 10 1809 or later
- **macOS**: macOS 10.15 or later, Xcode 14+
- **iOS**: iOS 11.0 or later
- **Android**: API 21 (Android 5.0) or later

## 🚀 Quick Start

### Clone and Build

```bash
# Clone the repository
git clone https://github.com/yourusername/ADRIFT-5.git
cd ADRIFT-5/ADRIFT-MAUI

# Restore NuGet packages
dotnet restore

# Build all projects
dotnet build

# Run Developer (Windows)
dotnet run --project ADRIFT.Developer -f net8.0-windows10.0.19041.0

# Run Runner
dotnet run --project ADRIFT.Runner -f net8.0-windows10.0.19041.0
```

### Run on Specific Platforms

```bash
# Windows
dotnet build -f net8.0-windows10.0.19041.0

# macOS
dotnet build -f net8.0-maccatalyst

# iOS (requires Mac)
dotnet build -f net8.0-ios

# Android
dotnet build -f net8.0-android
```

## 📖 Documentation

- **[MAUI Migration Guide](MAUI-MIGRATION-GUIDE.md)** - Complete migration documentation
- **[Control Mapping Guide](CONTROL-MAPPING-GUIDE.md)** - WinForms to MAUI control mappings
- **[API Documentation](docs/API.md)** - Core API reference (TODO)

## 🏗️ Project Structure

```
ADRIFT-MAUI/
├── ADRIFT.Core/                 # Shared business logic (.NET 8)
│   ├── Models/                  # Data models (clsAdventure, clsLocation, etc.)
│   └── Services/                # Core services
├── ADRIFT.Developer/            # MAUI IDE app
│   ├── Views/                   # XAML pages
│   ├── ViewModels/              # MVVM view models
│   ├── Services/                # App-specific services
│   └── Resources/               # Images, fonts, styles
├── ADRIFT.Runner/               # MAUI player app
│   ├── Views/                   # Game UI
│   └── ViewModels/              # Game state management
├── CONTROL-MAPPING-GUIDE.md     # WinForms → MAUI mappings
├── MAUI-MIGRATION-GUIDE.md      # Complete migration guide
└── README.md                    # This file
```

## 🎯 Current Status

**Phase**: Early Development 🚧

### ✅ Completed
- [x] Project structure and solution setup
- [x] Core business logic integration
- [x] Service layer architecture (IAdventureService, IFileService)
- [x] Main application shell and navigation
- [x] Example pages (MainPage, LocationEditorPage)
- [x] MVVM infrastructure with CommunityToolkit
- [x] Comprehensive documentation

### 🚧 In Progress
- [ ] All editor pages (27+ forms to migrate)
- [ ] Custom controls (GenTextBox, Map viewer)
- [ ] File operations (Open, Save, New)
- [ ] Platform-specific implementations

### 📝 Planned
- [ ] Complete Developer IDE
- [ ] Complete Runner app
- [ ] Testing and bug fixes
- [ ] iOS and Android optimizations
- [ ] Microsoft Store / App Store publishing

## 🤝 Contributing

This is a migration project. Contributions welcome!

### Development Workflow
1. Pick a form to migrate from the [Migration Guide](MAUI-MIGRATION-GUIDE.md)
2. Create ViewModel following MVVM pattern
3. Create XAML view
4. Register in DI container (MauiProgram.cs)
5. Test on multiple platforms
6. Submit pull request

### Code Style
- Follow Microsoft C# coding conventions
- Use MVVM pattern throughout
- Use `[ObservableProperty]` and `[RelayCommand]` attributes
- Comprehensive XML documentation comments

## 🐛 Known Issues

- **Rich Text Editor**: Currently using basic `Editor`, needs DevExpress integration
- **Docking UI**: No direct MAUI equivalent, using Shell navigation
- **Complex Forms**: Task editor needs UX redesign for mobile
- **Platform Testing**: iOS and Android testing in progress

See [Issues](https://github.com/yourusername/ADRIFT-5/issues) for full list.

## 📄 License

Copyright © Campbell Wild 1998-2025

See the original ADRIFT project for license terms.

## 🙏 Acknowledgments

- **Campbell Wild** - Original ADRIFT creator
- **ADRIFT Community** - Testing and feedback
- **Microsoft** - .NET MAUI framework
- **Syncfusion** - UI components (community license available)
- **DevExpress** - Rich text editing components

## 📞 Support

- **Documentation**: See `MAUI-MIGRATION-GUIDE.md`
- **Issues**: [GitHub Issues](https://github.com/yourusername/ADRIFT-5/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/ADRIFT-5/discussions)
- **Original ADRIFT**: http://www.adrift.co

## 🗺️ Roadmap

### Version 1.0 (Target: Q2 2025)
- ✅ Core infrastructure
- ⏳ All editor pages migrated
- ⏳ Developer IDE functional
- ⏳ Runner fully functional
- ⏳ Windows/macOS support

### Version 2.0 (Target: Q4 2025)
- iOS and Android optimization
- Cloud save integration
- Collaborative editing
- Mobile-first UX improvements

### Version 3.0 (Future)
- Web-based editor (Blazor)
- AI-assisted adventure creation
- Community adventure sharing
- Real-time multiplayer

---

**Status**: 🚧 Active Development
**Last Updated**: 2025-11-18
**Version**: 0.1.0-alpha
