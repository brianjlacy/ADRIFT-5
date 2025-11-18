# ADRIFT-5: WinForms to .NET MAUI Migration Guide

## Executive Summary

This document outlines the complete migration of ADRIFT-5 from Windows Forms/.NET Framework to .NET MAUI/.NET 8, enabling:
- **Cross-platform support**: Windows, macOS, iOS, Android
- **Modern UI**: Touch-friendly, responsive design
- **Better performance**: .NET 8 runtime improvements
- **Future-proof**: Active Microsoft support and development

**Estimated Effort**: 3-4 months for full migration
**Complexity**: High (due to Infragistics dependencies and complex UI)

---

## Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Project Structure](#project-structure)
3. [Migration Strategy](#migration-strategy)
4. [Step-by-Step Migration](#step-by-step-migration)
5. [Testing Strategy](#testing-strategy)
6. [Deployment](#deployment)
7. [Known Limitations](#known-limitations)

---

## Architecture Overview

### Original Architecture (WinForms)

```
ADRIFT5.sln
├── Developer.vbproj (WinForms, Infragistics)
│   ├── frmGenerator (main IDE)
│   ├── 27+ editor forms
│   └── Custom controls (GenTextBox, Map, etc.)
├── Runner.vbproj (WinForms, Infragistics)
├── MonoRunner.vbproj (WinForms, limited Infragistics)
└── WebRunner.vbproj (ASP.NET Web Forms)
```

### New Architecture (MAUI)

```
ADRIFT-MAUI.sln
├── ADRIFT.Core (Shared business logic)
│   ├── Models (clsAdventure, clsLocation, etc.)
│   ├── Services (Adventure management, File IO)
│   └── Game Engine (Actions, Babel, Blorb)
├── ADRIFT.Developer (MAUI app for IDE)
│   ├── Views (XAML pages)
│   ├── ViewModels (MVVM pattern)
│   └── Services (Platform-specific)
└── ADRIFT.Runner (MAUI app for player)
    ├── Views (Game UI)
    └── ViewModels (Game state)
```

**Key Changes:**
- Separation of UI and business logic
- MVVM pattern throughout
- Dependency injection for services
- Cross-platform from day one

---

## Project Structure

### ADRIFT.Core (.NET 8 Class Library)

**Purpose**: Shared business logic, no UI dependencies

**Contents:**
- All `clsXXX` classes (clsAdventure, clsLocation, clsTask, etc.)
- Game engine (Actions, Babel, Expression parser)
- File I/O (Blorb, FileIO)
- Utilities and helpers

**No Changes Required**: Business logic remains the same

---

### ADRIFT.Developer (MAUI App)

**Purpose**: Cross-platform IDE for creating adventures

**Structure:**
```
ADRIFT.Developer/
├── App.xaml/cs              # Application entry point
├── AppShell.xaml/cs         # Shell navigation
├── MauiProgram.cs           # Dependency injection setup
├── Platforms/               # Platform-specific code
├── Resources/               # Images, fonts, styles
├── Views/                   # XAML pages
│   ├── MainPage.xaml
│   ├── LocationEditorPage.xaml
│   ├── ObjectEditorPage.xaml
│   └── ... (27+ pages)
├── ViewModels/              # MVVM view models
│   ├── MainViewModel.cs
│   ├── LocationEditorViewModel.cs
│   └── ...
├── Services/                # App services
│   ├── IAdventureService.cs
│   ├── AdventureService.cs
│   ├── IFileService.cs
│   └── FileService.cs
└── Controls/                # Custom controls
    ├── RichTextEditor.xaml
    ├── MapViewer.xaml
    └── ...
```

---

### ADRIFT.Runner (MAUI App)

**Purpose**: Cross-platform player for playing adventures

**Simpler than Developer**, focused on:
- Game display (text output)
- Input parsing
- Map viewing
- Save/load games

---

## Migration Strategy

### Phase 1: Setup and Infrastructure (Week 1-2)

**Goal**: Create MAUI project structure and core services

**Tasks:**
1. ✅ Create MAUI solution and projects
2. ✅ Set up dependency injection (MauiProgram.cs)
3. ✅ Create service interfaces (IAdventureService, IFileService)
4. ✅ Implement basic services
5. ✅ Link ADRIFT.Core business logic
6. Configure NuGet packages (Syncfusion, DevExpress, CommunityToolkit)

**Deliverable**: Buildable MAUI projects with core infrastructure

---

### Phase 2: Main Application Shell (Week 3-4)

**Goal**: Create application shell and navigation

**Tasks:**
1. ✅ Design Shell navigation structure
2. ✅ Create MainPage (dashboard)
3. Implement file operations (New, Open, Save)
4. Create settings page
5. Implement menu bar (File, Edit, View, Help)
6. Design color scheme and styles

**Deliverable**: Navigable application with basic file operations

---

### Phase 3: List Pages (Week 5-6)

**Goal**: Create list views for all item types

**Forms to Migrate:**
- Locations list
- Objects list
- Tasks list
- Characters list
- Events list
- Variables list
- Groups list

**Pattern:**
```xml
<ContentPage Title="Locations">
    <Grid RowDefinitions="Auto,*,Auto">
        <!-- Search/Filter toolbar -->
        <CollectionView ItemsSource="{Binding Locations}">
            <!-- Item template with Edit/Delete -->
        </CollectionView>
        <!-- Add New button -->
    </Grid>
</ContentPage>
```

**Deliverable**: All list pages functional

---

### Phase 4: Editor Pages - Simple (Week 7-10)

**Goal**: Migrate simple editor forms

**Priority Order (Simple → Complex):**

1. **Variable Editor** (Easiest)
   - Name, Type (Number/Text)
   - Initial value
   - Description

2. **Group Editor**
   - Name
   - Member selection (CheckBoxes/CollectionView)

3. **Hint Editor**
   - Question
   - Subtasks
   - Hints list

4. **Synonym Editor**
   - Word
   - Synonym list

**Pattern:**
```csharp
// ViewModel
public partial class VariableEditorViewModel : ObservableObject
{
    [ObservableProperty]
    private string variableName = "";

    [ObservableProperty]
    private VariableType variableType;

    [RelayCommand]
    private async Task Save() { /* ... */ }
}
```

**Deliverable**: 4 simple editors completed

---

### Phase 5: Editor Pages - Medium (Week 11-16)

**Goal**: Migrate medium-complexity forms

**Forms:**

1. **Location Editor** (✅ Example provided)
   - Tab control (Description, Directions, Properties)
   - Rich text for descriptions
   - Direction editor (CollectionView)
   - Property grid

2. **Object Editor**
   - Tabs: Description, Location, Properties, Advanced
   - Article/prefix/noun editing
   - State properties

3. **Character Editor**
   - Tabs: Description, Location, Walk, Conversation
   - Walk editor (complex!)
   - Property management

4. **Event Editor**
   - Tabs: Description, When, Actions
   - Sub-events (CollectionView)
   - Action editor

**Deliverable**: Medium-complexity editors functional

---

### Phase 6: Editor Pages - Complex (Week 17-22)

**Goal**: Migrate most complex forms

**1. Task Editor** (Most Complex - 44+ tabs!)

**Challenge**: Task editor is massive with many subtabs

**Approach:**
- Break into multiple pages/popups
- Use wizard-style flow for task creation
- Simplify UI where possible

**Structure:**
```
Task Editor
├── General (Name, Description, Type)
├── Commands (Multi-step command builder)
├── Restrictions (Complex condition editor)
└── Actions (Success/failure/other actions)
```

**Consider**: Redesign UX for mobile/touch

**2. GenTextBox (Rich Text Editor)**

**Challenge**: Custom 2000+ line control with formatting

**Options:**
- DevExpress RichEdit (recommended)
- Syncfusion RichTextEditor
- Custom HTML editor (WebView)

**Recommendation**: Use DevExpress for full functionality

**3. Map Viewer**

**Challenge**: Custom map rendering

**Approach:**
- Use MAUI Graphics for drawing
- GraphicsView for rendering
- Touch gestures for zoom/pan

**Deliverable**: Complex editors functional (may have reduced features)

---

### Phase 7: Custom Controls (Week 23-25)

**Goal**: Recreate custom WinForms controls

**Controls to Migrate:**

1. **AutoCompleteCombo**
   - Use Syncfusion SfComboBox with autocomplete
   - Or Syncfusion SfAutoComplete

2. **Properties Editor**
   - Custom grid-like control
   - CollectionView with data templates

3. **Expression Builder**
   - Nested CollectionView for logical expressions
   - Drag-drop support (CommunityToolkit.Maui)

4. **Map Control**
   - GraphicsView with custom drawing
   - Touch gestures

**Deliverable**: All custom controls working

---

### Phase 8: Runner Application (Week 26-28)

**Goal**: Migrate Runner to MAUI

**Simpler than Developer!**

**Main Components:**
1. Game display (scrollable text output)
2. Command input (Entry with autocomplete)
3. Map view (reuse from Developer)
4. Status bar
5. File menu (Load, Save, Restart)

**Unique Challenges:**
- Formatting text output (colors, bold)
- Image display within text
- Sound playback (Plugin.Maui.Audio)

**Deliverable**: Functional cross-platform runner

---

### Phase 9: Testing and Polish (Week 29-32)

**Goal**: Comprehensive testing and bug fixes

**Testing Matrix:**

| Feature | Windows | macOS | iOS | Android |
|---------|---------|-------|-----|---------|
| File operations | ✓ | ✓ | ✓ | ✓ |
| Location editor | ✓ | ✓ | ✓ | ✓ |
| Object editor | ✓ | ✓ | ✓ | ✓ |
| Task editor | ✓ | ✓ | ✓ | ✓ |
| Map viewer | ✓ | ✓ | ✓ | ✓ |
| Game playback | ✓ | ✓ | ✓ | ✓ |
| Save/Load | ✓ | ✓ | ✓ | ✓ |

**Polish:**
- Accessibility (screen readers)
- Keyboard shortcuts
- Touch gestures
- Performance optimization
- UI refinements

**Deliverable**: Production-ready MAUI apps

---

## Step-by-Step Migration (Detailed)

### Example: Migrating frmLocation to LocationEditorPage

#### Step 1: Analyze Original Form

**Original (frmLocation.vb):**
```vb
Public Class frmLocation
    Inherits Form

    Private txtShortDesc As UltraTextEditor
    Private txtLongDesc As RichTextBox
    Private tabControl As UltraTabControl
    Private btnOK As UltraButton
    Private btnCancel As UltraButton

    Private _location As clsLocation

    Private Sub btnOK_Click(sender As Object, e As EventArgs)
        _location.ShortDescription = txtShortDesc.Text
        _location.LongDescription = txtLongDesc.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
```

---

#### Step 2: Create ViewModel

**LocationEditorViewModel.cs:**
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ADRIFT.ViewModels;

public partial class LocationEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    private clsLocation? _location;

    public LocationEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
    }

    [ObservableProperty]
    private string shortDescription = "";

    [ObservableProperty]
    private string longDescription = "";

    [ObservableProperty]
    private string locationKey = "";

    public void LoadLocation(clsLocation location)
    {
        _location = location;
        ShortDescription = location.ShortDescription;
        LongDescription = location.LongDescription;
        LocationKey = location.Key;
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (_location == null) return;

        _location.ShortDescription = ShortDescription;
        _location.LongDescription = LongDescription;

        // Save to adventure
        await _adventureService.SaveAdventureAsync();

        // Navigate back
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Cancel()
    {
        var result = await Shell.Current.DisplayAlert(
            "Cancel",
            "Discard changes?",
            "Yes", "No");

        if (result)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
```

---

#### Step 3: Create XAML View

**LocationEditorPage.xaml:**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:ADRIFT.ViewModels"
             x:Class="ADRIFT.Views.LocationEditorPage"
             x:DataType="vm:LocationEditorViewModel"
             Title="Location Editor">

    <Grid RowDefinitions="*,Auto">

        <!-- Main Content -->
        <ScrollView Grid.Row="0">
            <VerticalStackLayout Spacing="16" Padding="20">

                <!-- Short Description -->
                <VerticalStackLayout Spacing="8">
                    <Label Text="Short Description:"
                           FontAttributes="Bold" />
                    <Entry Text="{Binding ShortDescription}"
                           Placeholder="Brief description" />
                </VerticalStackLayout>

                <!-- Long Description -->
                <VerticalStackLayout Spacing="8">
                    <Label Text="Long Description:"
                           FontAttributes="Bold" />
                    <Editor Text="{Binding LongDescription}"
                            Placeholder="Full description"
                            HeightRequest="200"
                            AutoSize="TextChanges" />
                </VerticalStackLayout>

                <!-- Location Key (Read-only) -->
                <VerticalStackLayout Spacing="8">
                    <Label Text="Location Key:"
                           FontAttributes="Bold" />
                    <Entry Text="{Binding LocationKey}"
                           IsReadOnly="True"
                           BackgroundColor="LightGray" />
                </VerticalStackLayout>

            </VerticalStackLayout>
        </ScrollView>

        <!-- Button Bar -->
        <Grid Grid.Row="1"
              ColumnDefinitions="*,Auto,Auto"
              Padding="16"
              BackgroundColor="WhiteSmoke"
              ColumnSpacing="12">

            <Button Grid.Column="1"
                    Text="Cancel"
                    Command="{Binding CancelCommand}"
                    BackgroundColor="Gray" />

            <Button Grid.Column="2"
                    Text="OK"
                    Command="{Binding SaveAndCloseCommand}"
                    BackgroundColor="DodgerBlue" />
        </Grid>

    </Grid>

</ContentPage>
```

---

#### Step 4: Code-Behind

**LocationEditorPage.xaml.cs:**
```csharp
using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class LocationEditorPage : ContentPage
{
    public LocationEditorPage(LocationEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Load location data if passed via navigation
        // var locationKey = Shell.Current.CurrentState.Location.Query["key"];
        // viewModel.LoadLocationByKey(locationKey);
    }
}
```

---

#### Step 5: Register in DI Container

**MauiProgram.cs:**
```csharp
// Register ViewModel
builder.Services.AddTransient<LocationEditorViewModel>();

// Register View
builder.Services.AddTransient<LocationEditorPage>();
```

---

#### Step 6: Navigation

**From list page:**
```csharp
[RelayCommand]
private async Task EditLocation(clsLocation location)
{
    await Shell.Current.GoToAsync(
        $"locationeditor?key={location.Key}");
}
```

---

## Testing Strategy

### Unit Testing

**Test ViewModels:**
```csharp
[Fact]
public async Task SaveLocation_UpdatesAdventure()
{
    // Arrange
    var mockService = new Mock<IAdventureService>();
    var viewModel = new LocationEditorViewModel(mockService.Object);
    viewModel.ShortDescription = "Test Location";

    // Act
    await viewModel.SaveAndCloseCommand.ExecuteAsync(null);

    // Assert
    mockService.Verify(s => s.SaveAdventureAsync(), Times.Once);
}
```

### Integration Testing

**Test Navigation:**
- Create new location → Edit → Save → Verify in list

### Manual Testing

**Per Platform:**
- Windows: Full functionality
- macOS: Full functionality
- iOS: Touch interface, file access
- Android: Touch interface, permissions

---

## Deployment

### Windows

```bash
dotnet publish -f net8.0-windows10.0.19041.0 -c Release
```

**Output**: MSIX package for Microsoft Store or side-loading

### macOS

```bash
dotnet publish -f net8.0-maccatalyst -c Release
```

**Output**: .app bundle for Mac App Store

### iOS

```bash
dotnet publish -f net8.0-ios -c Release
```

**Requirements**: Apple Developer account, certificates

### Android

```bash
dotnet publish -f net8.0-android -c Release
```

**Output**: APK/AAB for Google Play Store

---

## Known Limitations

### Features Not Available in MAUI

1. **Docking UI** - No direct equivalent
   - Solution: Redesign with Shell/Tabs

2. **Ribbon Controls** - No native ribbon
   - Solution: MenuBar + Toolbar buttons

3. **Complex Rich Text** - Limited built-in support
   - Solution: Use DevExpress or Syncfusion

4. **MDI (Multiple Document Interface)** - Not supported
   - Solution: Use tabs or separate windows

### Platform-Specific Limitations

**iOS:**
- File system access limited
- Background processing restricted

**Android:**
- Permissions required for file access
- Different back button behavior

**Web (Blazor Hybrid):**
- Limited offline capabilities
- Performance considerations

---

## Resources

### Documentation
- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- [Syncfusion MAUI](https://help.syncfusion.com/maui/introduction/overview)

### Sample Apps
- [.NET MAUI Samples](https://github.com/dotnet/maui-samples)
- [Weather App](https://github.com/davidortinau/WeatherTwentyOne)

### Support
- Stack Overflow: [maui] tag
- GitHub Issues: dotnet/maui
- Discord: .NET Community

---

## Conclusion

Migrating ADRIFT-5 to .NET MAUI is a significant undertaking but provides:
- ✅ Cross-platform support
- ✅ Modern, touch-friendly UI
- ✅ Better performance
- ✅ Active support and development
- ✅ Future-proof architecture

**Estimated Timeline**: 3-4 months
**Recommended Team**: 2-3 developers
**Risk Level**: Medium-High (due to UI complexity)

**Success Factors:**
1. Strong MVVM architecture
2. Gradual migration (phase by phase)
3. Comprehensive testing
4. User feedback integration

---

**Last Updated**: 2025-11-18
**Version**: 1.0
**Author**: Claude (AI Assistant)
