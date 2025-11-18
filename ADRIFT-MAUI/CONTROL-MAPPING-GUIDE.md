# WinForms/Infragistics to .NET MAUI Control Mapping Guide

This guide provides mappings from Windows Forms and Infragistics controls to .NET MAUI equivalents for the ADRIFT-5 migration.

## Quick Reference Table

| WinForms/Infragistics | MAUI Equivalent | Package | Complexity |
|----------------------|-----------------|---------|------------|
| **Infragistics UltraButton** | Button | Built-in | Low |
| **Infragistics UltraLabel** | Label | Built-in | Low |
| **Infragistics UltraTextEditor** | Entry | Built-in | Low |
| **Infragistics UltraCheckEditor** | CheckBox / Switch | Built-in | Low |
| **Infragistics UltraComboEditor** | Picker | Built-in | Low |
| **Infragistics UltraGroupBox** | Frame / Border | Built-in | Low |
| **Infragistics UltraTabControl** | TabbedPage / SfTabView | Syncfusion | Medium |
| **Infragistics UltraStatusBar** | Custom View | Custom | Medium |
| **Infragistics UltraDockManager** | Custom Layout | Custom | **High** |
| **Infragistics UltraToolbarsManager** | MenuBarItem / Toolbar | Custom | **High** |
| **RichTextBox** | Editor / DevExpress RichEdit | DevExpress | **High** |
| **TreeView** | CollectionView / SfTreeView | Syncfusion | Medium |
| **DataGridView** | CollectionView / SfDataGrid | Syncfusion | Medium |
| **SplitContainer** | Grid with GridSplitter | Built-in | Medium |

## Detailed Control Mappings

### 1. Basic Controls

#### Button Controls

**WinForms:**
```csharp
// Infragistics UltraButton
UltraButton ultraButton1 = new UltraButton();
ultraButton1.Text = "Click Me";
ultraButton1.Click += UltraButton1_Click;

// Standard Button
Button button1 = new Button();
button1.Text = "Standard Button";
button1.Click += Button1_Click;
```

**MAUI:**
```xml
<!-- MAUI Button -->
<Button Text="Click Me"
        Command="{Binding ClickCommand}"
        BackgroundColor="{StaticResource Primary}" />
```

```csharp
// Code-behind (MVVM preferred)
[RelayCommand]
private void Click()
{
    // Handle click
}
```

**Migration Notes:**
- Use MVVM pattern with `ICommand` instead of event handlers
- Use `CommunityToolkit.Mvvm` for `RelayCommand` attributes
- Styling done via resources or direct properties

---

#### Text Input Controls

**WinForms:**
```csharp
// Infragistics UltraTextEditor
UltraTextEditor ultraTextEditor1 = new UltraTextEditor();
ultraTextEditor1.Text = "Initial value";
ultraTextEditor1.TextChanged += UltraTextEditor1_TextChanged;

// Standard TextBox
TextBox textBox1 = new TextBox();
textBox1.Multiline = true;
```

**MAUI:**
```xml
<!-- Single-line Entry -->
<Entry Text="{Binding UserName}"
       Placeholder="Enter username"
       TextChanged="Entry_TextChanged" />

<!-- Multi-line Editor -->
<Editor Text="{Binding Description}"
        Placeholder="Enter description"
        HeightRequest="100"
        AutoSize="TextChanges" />
```

**Migration Notes:**
- `Entry` for single-line input
- `Editor` for multi-line input
- Use two-way binding: `Text="{Binding Property}"`
- `TextChanged` event still available but prefer binding

---

#### Check Controls

**WinForms:**
```csharp
// Infragistics UltraCheckEditor
UltraCheckEditor ultraCheckEditor1 = new UltraCheckEditor();
ultraCheckEditor1.Checked = true;
ultraCheckEditor1.CheckedChanged += UltraCheckEditor1_CheckedChanged;

// Standard CheckBox
CheckBox checkBox1 = new CheckBox();
checkBox1.Checked = true;
```

**MAUI:**
```xml
<!-- CheckBox (traditional look) -->
<CheckBox IsChecked="{Binding IsEnabled}"
          CheckedChanged="CheckBox_CheckedChanged" />

<!-- Switch (modern toggle) -->
<Switch IsToggled="{Binding IsEnabled}"
        OnColor="{StaticResource Primary}" />
```

**Migration Notes:**
- Use `CheckBox` for traditional checkbox appearance
- Use `Switch` for modern toggle appearance (recommended)
- Property is `IsChecked` (CheckBox) or `IsToggled` (Switch)

---

### 2. Container Controls

#### Panels and Groups

**WinForms:**
```csharp
// Infragistics UltraGroupBox
UltraGroupBox ultraGroupBox1 = new UltraGroupBox();
ultraGroupBox1.Text = "Settings";
ultraGroupBox1.Controls.Add(childControl);

// Standard Panel
Panel panel1 = new Panel();
panel1.BorderStyle = BorderStyle.FixedSingle;
```

**MAUI:**
```xml
<!-- Frame (with border and shadow) -->
<Frame BorderColor="Gray"
       CornerRadius="8"
       HasShadow="True"
       Padding="16">
    <VerticalStackLayout>
        <!-- Child controls -->
    </VerticalStackLayout>
</Frame>

<!-- Border (simpler) -->
<Border Stroke="Gray"
        StrokeThickness="1"
        BackgroundColor="White"
        Padding="12">
    <VerticalStackLayout>
        <!-- Child controls -->
    </VerticalStackLayout>
</Border>
```

**Migration Notes:**
- `Frame` provides border, shadow, and background
- `Border` is lighter-weight for simple borders
- Use layout containers for child controls:
  - `VerticalStackLayout` - vertical stacking
  - `HorizontalStackLayout` - horizontal stacking
  - `Grid` - grid layout

---

### 3. Tab Controls

**WinForms (Infragistics):**
```csharp
// UltraTabControl
UltraTabControl ultraTabControl1 = new UltraTabControl();
UltraTab tab1 = new UltraTab("Description");
UltraTabPageControl tabPage1 = new UltraTabPageControl();
tab1.TabPage = tabPage1;
ultraTabControl1.Tabs.Add(tab1);
```

**MAUI (Built-in):**
```xml
<!-- TabbedPage (full-screen tabs) -->
<TabbedPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui">
    <ContentPage Title="Description">
        <!-- Content -->
    </ContentPage>
    <ContentPage Title="Properties">
        <!-- Content -->
    </ContentPage>
</TabbedPage>
```

**MAUI (Syncfusion - Recommended):**
```xml
<!-- SfTabView (inline tabs) -->
<sfTabView:SfTabView>
    <sfTabView:SfTabItem Header="Description">
        <VerticalStackLayout>
            <!-- Content -->
        </VerticalStackLayout>
    </sfTabView:SfTabItem>
    <sfTabView:SfTabItem Header="Properties">
        <VerticalStackLayout>
            <!-- Content -->
        </VerticalStackLayout>
    </sfTabView:SfTabItem>
</sfTabView:SfTabView>
```

**Migration Notes:**
- Use `TabbedPage` for full-screen navigation tabs
- Use Syncfusion `SfTabView` for inline tabs (closer to WinForms behavior)
- Syncfusion provides better customization
- Alternative: Implement custom tab control with buttons

---

### 4. Lists and TreeViews

#### Combo Box / Dropdown

**WinForms:**
```csharp
// Infragistics UltraComboEditor
UltraComboEditor ultraComboEditor1 = new UltraComboEditor();
ultraComboEditor1.Items.Add("Option 1");
ultraComboEditor1.Items.Add("Option 2");
ultraComboEditor1.SelectedIndex = 0;
```

**MAUI:**
```xml
<!-- Picker -->
<Picker ItemsSource="{Binding Options}"
        SelectedItem="{Binding SelectedOption}"
        Title="Select an option" />

<!-- With display member binding -->
<Picker ItemsSource="{Binding Locations}"
        ItemDisplayBinding="{Binding Name}"
        SelectedItem="{Binding SelectedLocation}" />
```

**Migration Notes:**
- `Picker` is the MAUI equivalent
- Use `ItemsSource` for data binding
- Use `ItemDisplayBinding` to specify display property
- `SelectedItem` for two-way binding

---

#### TreeView

**WinForms:**
```csharp
// Standard TreeView
TreeView treeView1 = new TreeView();
TreeNode node1 = new TreeNode("Parent");
TreeNode node2 = new TreeNode("Child");
node1.Nodes.Add(node2);
treeView1.Nodes.Add(node1);
```

**MAUI (Syncfusion):**
```xml
<!-- SfTreeView -->
<sfTreeView:SfTreeView ItemsSource="{Binding Nodes}"
                       ChildPropertyName="Children">
    <sfTreeView:SfTreeView.ItemTemplate>
        <DataTemplate>
            <Label Text="{Binding Name}" />
        </DataTemplate>
    </sfTreeView:SfTreeView.ItemTemplate>
</sfTreeView:SfTreeView>
```

**MAUI (Custom with CollectionView):**
```xml
<!-- Nested CollectionView -->
<CollectionView ItemsSource="{Binding RootItems}">
    <CollectionView.ItemTemplate>
        <DataTemplate>
            <VerticalStackLayout>
                <Label Text="{Binding Name}"
                       FontAttributes="Bold" />
                <CollectionView ItemsSource="{Binding Children}"
                                Margin="20,0,0,0">
                    <!-- Child template -->
                </CollectionView>
            </VerticalStackLayout>
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

**Migration Notes:**
- Syncfusion `SfTreeView` provides full TreeView functionality
- For simple hierarchies, nested `CollectionView` works
- Consider data structure (needs hierarchical model)

---

### 5. Complex Controls

#### Docking System (UltraDockManager)

**WinForms (Infragistics):**
```csharp
// UltraDockManager with dockable panels
UltraDockManager ultraDockManager1 = new UltraDockManager();
DockableControlPane pane1 = new DockableControlPane();
pane1.Control = myControl;
ultraDockManager1.DockAreas[0].Panes.Add(pane1);
```

**MAUI (Custom Implementation):**

There's **no direct equivalent** in MAUI. Options:

**Option 1: Shell with FlyoutItems**
```xml
<Shell>
    <FlyoutItem Title="Properties">
        <ShellContent ContentTemplate="{DataTemplate local:PropertiesPage}" />
    </FlyoutItem>
    <FlyoutItem Title="Explorer">
        <ShellContent ContentTemplate="{DataTemplate local:ExplorerPage}" />
    </FlyoutItem>
</Shell>
```

**Option 2: Grid with Splitters**
```xml
<Grid RowDefinitions="*,Auto,*"
      ColumnDefinitions="200,Auto,*">

    <!-- Left Panel -->
    <ContentView Grid.Row="0" Grid.RowSpan="3" Grid.Column="0">
        <!-- Explorer content -->
    </ContentView>

    <!-- Vertical Splitter -->
    <BoxView Grid.Row="0" Grid.RowSpan="3" Grid.Column="1"
             WidthRequest="4"
             BackgroundColor="Gray" />

    <!-- Main Content -->
    <ContentView Grid.Row="0" Grid.Column="2">
        <!-- Main editor -->
    </ContentView>

    <!-- Horizontal Splitter -->
    <BoxView Grid.Row="1" Grid.Column="2"
             HeightRequest="4"
             BackgroundColor="Gray" />

    <!-- Bottom Panel -->
    <ContentView Grid.Row="2" Grid.Column="2">
        <!-- Output/Log -->
    </ContentView>
</Grid>
```

**Option 3: Third-Party (Windows only)**
- Consider Syncfusion Docking Manager (Windows-specific)
- Or implement custom docking behavior

**Migration Strategy for ADRIFT:**
1. **Redesign UI** - Modern MAUI doesn't use traditional docking
2. **Use Shell Navigation** - For switching between major views
3. **Use Tabbed Pages** - For related content
4. **Use Collapsible Panels** - For optional side panels

---

#### Toolbar/Ribbon (UltraToolbarsManager)

**WinForms (Infragistics):**
```csharp
// UltraToolbarsManager with ribbon
UltraToolbarsManager ultraToolbarsManager1 = new UltraToolbarsManager();
RibbonTab ribbonTab1 = new RibbonTab("Home");
RibbonGroup ribbonGroup1 = new RibbonGroup("File");
ButtonTool buttonTool1 = new ButtonTool("New");
ribbonGroup1.Tools.Add(buttonTool1);
```

**MAUI (MenuBar - Simple):**
```xml
<ContentPage>
    <ContentPage.MenuBarItems>
        <MenuBarItem Text="File">
            <MenuFlyoutItem Text="New"
                            Command="{Binding NewCommand}" />
            <MenuFlyoutItem Text="Open"
                            Command="{Binding OpenCommand}" />
            <MenuFlyoutSeparator />
            <MenuFlyoutItem Text="Exit"
                            Command="{Binding ExitCommand}" />
        </MenuBarItem>
        <MenuBarItem Text="Edit">
            <!-- Edit menu items -->
        </MenuBarItem>
    </ContentPage.MenuBarItems>
</ContentPage>
```

**MAUI (Custom Toolbar):**
```xml
<Grid RowDefinitions="Auto,*">
    <!-- Toolbar -->
    <HorizontalStackLayout Grid.Row="0"
                           Spacing="8"
                           Padding="8"
                           BackgroundColor="{StaticResource Primary}">
        <Button Text="New"
                Command="{Binding NewCommand}"
                ImageSource="new.png" />
        <Button Text="Open"
                Command="{Binding OpenCommand}"
                ImageSource="open.png" />
        <Button Text="Save"
                Command="{Binding SaveCommand}"
                ImageSource="save.png" />
        <!-- More buttons -->
    </HorizontalStackLayout>

    <!-- Main Content -->
    <ContentView Grid.Row="1">
        <!-- Page content -->
    </ContentView>
</Grid>
```

**Migration Notes:**
- MAUI doesn't have ribbon controls
- Use `MenuBarItems` for traditional menus (Windows/Mac)
- Create custom toolbar with buttons for common actions
- Consider Syncfusion controls for advanced toolbar needs

---

#### Rich Text Editor (GenTextBox)

**WinForms:**
```csharp
// RichTextBox with formatting
RichTextBox richTextBox1 = new RichTextBox();
richTextBox1.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
richTextBox1.SelectionColor = Color.Red;
```

**MAUI Options:**

**Option 1: Basic Editor (Limited)**
```xml
<Editor Text="{Binding Content}"
        HeightRequest="200"
        AutoSize="TextChanges" />
```

**Option 2: DevExpress RichEdit (Full-featured)**
```xml
<!-- Requires DevExpress.Maui.RichEdit package -->
<dxe:RichEditEdit x:Name="richEdit"
                  IsReadOnly="False"
                  IsSpellCheckEnabled="True" />
```

**Option 3: Custom HTML Editor**
```xml
<!-- WebView with contenteditable HTML -->
<WebView x:Name="htmlEditor"
         HeightRequest="300" />
```

```csharp
// Initialize with contenteditable div
htmlEditor.Source = new HtmlWebViewSource
{
    Html = @"
        <div contenteditable='true'
             style='font-family: sans-serif; padding: 10px;'>
            Type here...
        </div>"
};
```

**Recommendation for ADRIFT:**
- Use **DevExpress RichEdit** for full rich text capabilities
- Alternative: Syncfusion RichTextEditor
- For simple needs: Standard `Editor` control

---

### 6. Dialogs and Popups

**WinForms:**
```csharp
// MessageBox
DialogResult result = MessageBox.Show("Save changes?", "Confirm",
    MessageBoxButtons.YesNoCancel,
    MessageBoxIcon.Question);

// Custom Form Dialog
using (var dialog = new MyCustomDialog())
{
    if (dialog.ShowDialog() == DialogResult.OK)
    {
        // Handle result
    }
}
```

**MAUI:**
```csharp
// Simple Alert
await DisplayAlert("Confirm", "Save changes?", "Yes", "No");

// Action Sheet (multiple options)
string action = await DisplayActionSheet(
    "Choose action",
    "Cancel",
    null,
    "Option 1", "Option 2", "Option 3");

// Prompt for text
string result = await DisplayPromptAsync(
    "Input Required",
    "Enter your name:",
    placeholder: "Name");

// Custom Popup (CommunityToolkit.Maui)
var popup = new MyCustomPopup();
await this.ShowPopupAsync(popup);
```

**Migration Notes:**
- Use `DisplayAlert`, `DisplayActionSheet`, `DisplayPromptAsync` for built-in dialogs
- Use CommunityToolkit.Maui `Popup` for custom dialogs
- All dialog methods are async

---

## MVVM Pattern Migration

### Event Handlers → Commands

**WinForms (Code-behind):**
```csharp
private void SaveButton_Click(object sender, EventArgs e)
{
    // Save logic
    MessageBox.Show("Saved!");
}
```

**MAUI (MVVM with CommunityToolkit):**
```csharp
// ViewModel
[RelayCommand]
private async Task Save()
{
    // Save logic
    await Shell.Current.DisplayAlert("Success", "Saved!", "OK");
}
```

```xml
<!-- View -->
<Button Text="Save"
        Command="{Binding SaveCommand}" />
```

### Property Change Notification

**WinForms:**
```csharp
private string _title;
public string Title
{
    get => _title;
    set
    {
        _title = value;
        titleLabel.Text = value;
    }
}
```

**MAUI (MVVM with CommunityToolkit):**
```csharp
// ViewModel
[ObservableProperty]
private string title = "";

// Automatically generates:
// - public string Title property
// - INotifyPropertyChanged implementation
// - OnTitleChanged() method (optional)
```

```xml
<!-- View -->
<Label Text="{Binding Title}" />
```

---

## Third-Party Component Recommendations

### Syncfusion (Comprehensive UI Suite)

**Website:** https://www.syncfusion.com/maui-controls

**Recommended for ADRIFT:**
- `SfTabView` - Tab controls (replacement for UltraTabControl)
- `SfTreeView` - Hierarchical data (folders, game structure)
- `SfDataGrid` - Grid with sorting/filtering
- `SfComboBox` - Advanced dropdown with filtering
- `SfTextInputLayout` - Material Design text inputs

**License:** Community license free for individuals/small businesses

---

### DevExpress (Enterprise Controls)

**Website:** https://www.devexpress.com/maui/

**Recommended for ADRIFT:**
- `RichEditEdit` - Full rich text editor (for GenTextBox replacement)
- `DataGridView` - Advanced grid control
- `TabView` - Tab control with advanced features

**License:** Commercial (paid)

---

### CommunityToolkit.Maui (Free, Open Source)

**Website:** https://github.com/CommunityToolkit/Maui

**Recommended for ADRIFT:**
- `Popup` - Modal dialogs
- `Expander` - Collapsible sections
- `DrawingView` - Drawing/mapping
- Various behaviors and converters

**License:** MIT (Free)

---

## Migration Checklist

### For Each Form

- [ ] Identify all controls used
- [ ] Map to MAUI equivalents using this guide
- [ ] Create ViewModel with `ObservableObject`
- [ ] Convert properties to `[ObservableProperty]`
- [ ] Convert event handlers to `[RelayCommand]`
- [ ] Create XAML layout
- [ ] Implement data binding
- [ ] Test on target platforms

### For Complex Controls

- [ ] Evaluate third-party options
- [ ] Design custom control if needed
- [ ] Consider UX redesign for mobile/touch

### For Infragistics-Specific Features

- [ ] **Docking** → Redesign with Shell/Grid
- [ ] **Ribbon** → MenuBar + Toolbar buttons
- [ ] **Advanced Grids** → Syncfusion SfDataGrid
- [ ] **Rich Text** → DevExpress RichEdit

---

## Performance Considerations

1. **Virtualization**: Use `CollectionView` for large lists (automatic virtualization)
2. **Async Operations**: Use `async`/`await` for all IO operations
3. **Data Binding**: Prefer one-time binding for static data
4. **Image Optimization**: Use proper image sizes for different platforms
5. **Platform-Specific Code**: Use `#if WINDOWS` for platform-specific features

---

## Common Pitfalls

1. **No Designer**: MAUI doesn't have a visual designer. Use XAML Hot Reload instead.
2. **Different Layout System**: Learn `Grid`, `StackLayout`, `FlexLayout`
3. **Async Everything**: Many APIs are async in MAUI
4. **Platform Differences**: Test on all target platforms
5. **Resource Management**: Use `Resources/` folder structure correctly

---

## Next Steps

See **MAUI-MIGRATION-GUIDE.md** for:
- Detailed migration plan
- Form-by-form conversion guidance
- Testing strategy
- Deployment instructions
