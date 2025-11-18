using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmGenerator
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        fSplash = New Splash
        fSplash.Show(Me);
        Application.DoEvents();

        private string sIde = "ide";

        private string sDLL = GetWinSysDir() + sIde + "sync" + "." + Chr(&H64) + "ll";
        If IO.File.Exists(sDLL) || IO.File.Exists(sDLL.ToLower.Replace("system32", "syswow64")) Then PartOne = true

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call

    }

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        if (disposing)
        {
            if (Not (components Is null))
            {
                components.Dispose();
            }
        }
        MyBase.Dispose(disposing);
    }

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents _frmGenerator_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmGenerator_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmGenerator_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmGenerator_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents StatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents UTMMain As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager;
    'Friend WithEvents UltraTabbedMdiManager1 As Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager
    Friend WithEvents FolderList1 As ADRIFT.FolderList;
    Friend WithEvents _frmGeneratorAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl;
    Friend WithEvents UDMGenerator As Infragistics.Win.UltraWinDock.UltraDockManager;
    Friend WithEvents WindowDockingArea1 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents DockableWindow1 As Infragistics.Win.UltraWinDock.DockableWindow;
    Friend WithEvents _frmGeneratorUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmGeneratorUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmGeneratorUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmGeneratorUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents Map1 As ADRIFT.Map;
    Friend WithEvents DockableWindow2 As Infragistics.Win.UltraWinDock.DockableWindow;
    Friend WithEvents WindowDockingArea3 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents WindowDockingArea4 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog;
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider;
    Friend WithEvents cmsMain As System.Windows.Forms.ContextMenuStrip;
    Friend WithEvents miAddFolder As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNew");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpen");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSaveAs");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Import");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Export");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProtectAdventure");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool157 = new Infragistics.Win.UltraWinToolbars.ButtonTool("IntroductionWinning");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Settings");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool155 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
        private Infragistics.Win.UltraWinToolbars.LabelTool LabelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("Recent Documents");
        private Infragistics.Win.UltraWinToolbars.ContextualTabGroup ContextualTabGroup1 = new Infragistics.Win.UltraWinToolbars.ContextualTabGroup("ctgMapTools");
        private Infragistics.Win.UltraWinToolbars.RibbonTab RibbonTab1 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Home");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup1 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup2");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup2 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup1");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Location");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Object");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Task");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Character");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Event");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Variable");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Group");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Property");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Text Override");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Hint");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool172 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Synonym");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool170 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UserFunction");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup3 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup3");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Find");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FindNext");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Replace");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup4 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup4");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Options");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RunAdventure");
        private Infragistics.Win.UltraWinToolbars.RibbonTab RibbonTab2 = new Infragistics.Win.UltraWinToolbars.RibbonTab("View");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup5 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup1");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FolderList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Map");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cascade");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileHorizontally");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileVertically");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool147 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Tile");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseAllWindows");
        private Infragistics.Win.UltraWinToolbars.RibbonTab RibbonTab3 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Map");
        private Infragistics.Win.UltraWinToolbars.RibbonGroup RibbonGroup6 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("MapTools");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("NewPage");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool67 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapLocation");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool68 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapPlan");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool69 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapCentre");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool70 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomIn");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool71 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomOut");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowGridLines", "");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Show Axes", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool165 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Print");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool158 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RunAdventure");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool149 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapWarning");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Register");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutGenerator");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool167 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Help");
        private Infragistics.Win.UltraWinToolbars.UltraToolbar UltraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tools");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool73 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNew");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool74 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpen");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool75 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool76 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool77 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool78 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool79 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Location");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool80 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Object");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool81 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Task");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool82 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Event");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool83 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Character");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool84 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Variable");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool85 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Group");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool86 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Text Override");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool87 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Hint");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool88 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Property");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool89 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RunAdventure");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Find");
        private Infragistics.Win.UltraWinToolbars.UltraToolbar UltraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("MainMenuBar");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool21 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool22 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool23 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool24 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuAdventure");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool25 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuWindow");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool26 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool27 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool90 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNew");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool91 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpen");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool92 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool93 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSaveAs");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool28 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Import");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool29 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Export");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool30 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuRecentAdventures");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Settings");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool94 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool95 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNew");
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool96 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpen");
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance6 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool31 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool97 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool98 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool99 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool100 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Find");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FindNext");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Replace");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool32 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool102 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCreateNewList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool103 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool104 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRenameList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool105 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCreateNewList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool106 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRenameList");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool107 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteList");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool33 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuAdventure");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool34 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Add New");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("IntroductionWinning");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Options");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RunAdventure");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool35 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuWindow");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cascade");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseAllWindows");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileHorizontally");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileVertically");
        private Infragistics.Win.UltraWinToolbars.MdiWindowListTool MdiWindowListTool3 = new Infragistics.Win.UltraWinToolbars.MdiWindowListTool("MDIWindowListTool1");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool36 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool109 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutGenerator");
        private Infragistics.Win.UltraWinToolbars.MdiWindowListTool MdiWindowListTool4 = new Infragistics.Win.UltraWinToolbars.MdiWindowListTool("MDIWindowListTool1");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool110 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
        private Infragistics.Win.Appearance Appearance7 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance8 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool111 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
        private Infragistics.Win.Appearance Appearance9 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool112 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
        private Infragistics.Win.Appearance Appearance10 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool113 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
        private Infragistics.Win.Appearance Appearance11 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool114 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
        private Infragistics.Win.Appearance Appearance12 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance13 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool115 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Find");
        private Infragistics.Win.Appearance Appearance14 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool116 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Location");
        private Infragistics.Win.Appearance Appearance15 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance16 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool117 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Object");
        private Infragistics.Win.Appearance Appearance17 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance18 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool118 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Task");
        private Infragistics.Win.Appearance Appearance19 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance20 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool119 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Event");
        private Infragistics.Win.Appearance Appearance21 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance22 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool120 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Character");
        private Infragistics.Win.Appearance Appearance23 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance24 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool121 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Variable");
        private Infragistics.Win.Appearance Appearance25 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance26 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool37 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Add New");
        private Infragistics.Win.Appearance Appearance27 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool122 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Location");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool123 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Object");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool124 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Task");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool125 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Event");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool126 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Character");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool127 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Variable");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool128 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Group");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool129 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Text Override");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool130 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Hint");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool131 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Property");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool132 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSaveAs");
        private Infragistics.Win.Appearance Appearance28 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool38 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuRecentAdventures");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Tab Windows", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool133 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Group");
        private Infragistics.Win.Appearance Appearance29 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance30 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool134 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Text Override");
        private Infragistics.Win.Appearance Appearance31 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance32 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool135 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Hint");
        private Infragistics.Win.Appearance Appearance33 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance34 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool136 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportModule");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool137 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportModule");
        private Infragistics.Win.Appearance Appearance35 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool39 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Import");
        private Infragistics.Win.Appearance Appearance36 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance37 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool138 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportModule");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool153 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportBlorb");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool162 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportTrizbort");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool159 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LanguageResource");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool40 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Export");
        private Infragistics.Win.Appearance Appearance38 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance39 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool139 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportModule");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("iFictionRecord");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CreateExecutable");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool151 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBlorb");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool168 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportLanguageResource");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool140 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Settings");
        private Infragistics.Win.Appearance Appearance40 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance41 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool141 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Property");
        private Infragistics.Win.Appearance Appearance42 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance43 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool142 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutGenerator");
        private Infragistics.Win.Appearance Appearance44 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool143 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RunAdventure");
        private Infragistics.Win.Appearance Appearance45 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance46 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool144 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
        private Infragistics.Win.Appearance Appearance47 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("IntroductionWinning");
        private Infragistics.Win.Appearance Appearance48 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Options");
        private Infragistics.Win.Appearance Appearance49 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance50 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("iFictionRecord");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ArrangeIcons");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cascade");
        private Infragistics.Win.Appearance Appearance51 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseAllWindows");
        private Infragistics.Win.Appearance Appearance52 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileHorizontally");
        private Infragistics.Win.Appearance Appearance53 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TileVertically");
        private Infragistics.Win.Appearance Appearance54 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Replace");
        private Infragistics.Win.Appearance Appearance55 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FindNext");
        private Infragistics.Win.Appearance Appearance56 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance57 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance58 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CreateExecutable");
        private Infragistics.Win.UltraWinToolbars.LabelTool LabelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("Recent Documents");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ProtectAdventure");
        private Infragistics.Win.Appearance Appearance59 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FolderList");
        private Infragistics.Win.Appearance Appearance60 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Register");
        private Infragistics.Win.Appearance Appearance61 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool66 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Map");
        private Infragistics.Win.Appearance Appearance62 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance63 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool72 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapLocation");
        private Infragistics.Win.Appearance Appearance64 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool101 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapPlan");
        private Infragistics.Win.Appearance Appearance65 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool108 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapCentre");
        private Infragistics.Win.Appearance Appearance66 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool145 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomIn");
        private Infragistics.Win.Appearance Appearance67 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool146 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomOut");
        private Infragistics.Win.Appearance Appearance68 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool148 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapWarning");
        private Infragistics.Win.Appearance Appearance69 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Show Axes", "");
        private Infragistics.Win.Appearance Appearance70 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowGridLines", "");
        private Infragistics.Win.Appearance Appearance71 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance72 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool150 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Tile");
        private Infragistics.Win.Appearance Appearance73 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool152 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBlorb");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool154 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportBlorb");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool156 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("NewPage");
        private Infragistics.Win.Appearance Appearance74 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance75 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool160 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportTrizbort");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool161 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LanguageResource");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool163 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Print");
        private Infragistics.Win.Appearance Appearance76 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance77 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool164 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PrintPreview");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool166 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportLanguageResource");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool169 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Help");
        private Infragistics.Win.Appearance Appearance78 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool171 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UserFunction");
        private Infragistics.Win.Appearance Appearance79 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance80 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool173 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Synonym");
        private Infragistics.Win.Appearance Appearance81 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance82 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft, new System.Guid("c3e7f400-f214-486a-bfea-b1991baa81b0"));
        private Infragistics.Win.UltraWinDock.DockableControlPane DockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("3a96af15-ca01-4fb2-b71d-e418e2057ad0"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("c3e7f400-f214-486a-bfea-b1991baa81b0"), -1);
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, new System.Guid("02625fb5-30ea-419f-a5ee-f435e59086b2"));
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane3 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedTop, new System.Guid("da8e20b7-4eff-497d-ac07-a6389585feb4"));
        private Infragistics.Win.UltraWinDock.DockableControlPane DockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("b6db8285-030c-4edd-89ef-aaaf5b21d655"), new System.Guid("02625fb5-30ea-419f-a5ee-f435e59086b2"), 0, new System.Guid("da8e20b7-4eff-497d-ac07-a6389585feb4"), 0);
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmGenerator));
        Me.FolderList1 = New ADRIFT.FolderList();
        Me.Map1 = New ADRIFT.Map();
        Me.ofd = New System.Windows.Forms.OpenFileDialog();
        Me.StatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.sfd = New System.Windows.Forms.SaveFileDialog();
        Me._frmGenerator_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me.UTMMain = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components);
        Me._frmGenerator_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me._frmGenerator_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me._frmGenerator_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me.UDMGenerator = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components);
        Me._frmGeneratorUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmGeneratorUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmGeneratorUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmGeneratorUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmGeneratorAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl();
        Me.WindowDockingArea1 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.DockableWindow1 = New Infragistics.Win.UltraWinDock.DockableWindow();
        Me.DockableWindow2 = New Infragistics.Win.UltraWinDock.DockableWindow();
        Me.WindowDockingArea3 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.WindowDockingArea4 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog();
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider();
        Me.cmsMain = New System.Windows.Forms.ContextMenuStrip(Me.components);
        Me.miAddFolder = New System.Windows.Forms.ToolStripMenuItem();
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UTMMain).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UDMGenerator).BeginInit();
        Me.WindowDockingArea1.SuspendLayout();
        Me.DockableWindow1.SuspendLayout();
        Me.DockableWindow2.SuspendLayout();
        Me.WindowDockingArea4.SuspendLayout();
        Me.cmsMain.SuspendLayout();
        Me.SuspendLayout();
        '
        'FolderList1
        '
        Me.FolderList1.Location = New System.Drawing.Point(0, 20);
        Me.FolderList1.Name = "FolderList1";
        Me.FolderList1.Size = New System.Drawing.Size(194, 460);
        Me.StatusBar1.SetStatusBarText(Me.FolderList1, "Double-click a folder to open a new window, or single click to load in the active" + _;
        " window");
        Me.FolderList1.TabIndex = 12;
        '
        'Map1
        '
        Me.Map1.Location = New System.Drawing.Point(0, 20);
        Me.Map1.Map = null;
        Me.Map1.Name = "Map1";
        Me.Map1.ShowAxes = true;
        Me.Map1.ShowGrid = true;
        Me.Map1.Size = New System.Drawing.Size(714, 217);
        Me.Map1.TabIndex = 35;
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 644);
        Me.StatusBar1.Name = "StatusBar1";
        UltraStatusPanel1.MinWidth = 150;
        UltraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel1.Text = "File version:";
        UltraStatusPanel2.MinWidth = 150;
        UltraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel2.Text = "File size:";
        UltraStatusPanel3.MinWidth = 150;
        UltraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel3.Text = "Maximum score:";
        UltraStatusPanel4.MinWidth = 150;
        UltraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel4.Text = "Simple Mode On";
        UltraStatusPanel5.KeyStateInfo.Key = Infragistics.Win.UltraWinStatusBar.KeyState.CapsLock;
        UltraStatusPanel5.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.KeyState;
        Me.StatusBar1.Panels.AddRange(New Infragistics.Win.UltraWinStatusBar.UltraStatusPanel() {UltraStatusPanel1, UltraStatusPanel2, UltraStatusPanel3, UltraStatusPanel4, UltraStatusPanel5});
        Me.StatusBar1.Size = New System.Drawing.Size(929, 24);
        Me.StatusBar1.TabIndex = 5;
        Me.StatusBar1.Text = "UltraStatusBar1";
        '
        '_frmGenerator_Toolbars_Dock_Area_Left
        '
        Me._frmGenerator_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmGenerator_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(191), Integer), (Byte)(CType(219), Integer), (Byte)(CType(255), Integer));
        Me._frmGenerator_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
        Me._frmGenerator_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmGenerator_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 8;
        Me._frmGenerator_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 164);
        Me._frmGenerator_Toolbars_Dock_Area_Left.Name = "_frmGenerator_Toolbars_Dock_Area_Left";
        Me._frmGenerator_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(8, 480);
        Me._frmGenerator_Toolbars_Dock_Area_Left.ToolbarsManager = Me.UTMMain;
        '
        'UTMMain
        '
        Me.UTMMain.DesignerFlags = 1;
        Me.UTMMain.DockWithinContainer = Me;
        Me.UTMMain.DockWithinContainerBaseType = GetType(System.Windows.Forms.Form);
        Me.UTMMain.Ribbon.ApplicationMenu.FooterToolbar.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool59});
        PopupMenuTool1.InstanceProps.IsFirstInGroup = true;
        ButtonTool50.InstanceProps.IsFirstInGroup = true;
        ButtonTool157.InstanceProps.IsFirstInGroup = true;
        ButtonTool157.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        Me.UTMMain.Ribbon.ApplicationMenu.ToolAreaLeft.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool36, ButtonTool37, ButtonTool38, ButtonTool39, PopupMenuTool1, PopupMenuTool2, ButtonTool50, ButtonTool157, ButtonTool40, ButtonTool155});
        Me.UTMMain.Ribbon.ApplicationMenu.ToolAreaRight.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1});
        Me.UTMMain.Ribbon.ApplicationMenu.ToolAreaRight.Settings.LabelDisplayStyle = Infragistics.Win.UltraWinToolbars.LabelMenuDisplayStyle.Header;
        Me.UTMMain.Ribbon.ApplicationMenu.ToolAreaRight.Settings.UseLargeImages = Infragistics.Win.DefaultableBoolean.[false];
        Me.UTMMain.Ribbon.ApplicationMenuButtonImage = Global.ADRIFT.My.Resources.Resources.imgDeveloper24;
        ContextualTabGroup1.Caption = "Map Tools";
        ContextualTabGroup1.Key = "ctgMapTools";
        ContextualTabGroup1.Visible = false;
        Me.UTMMain.Ribbon.NonInheritedContextualTabGroups.AddRange(New Infragistics.Win.UltraWinToolbars.ContextualTabGroup() {ContextualTabGroup1});
        RibbonTab1.Caption = "Home";
        RibbonGroup1.Caption = "Clipboard";
        ButtonTool45.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        RibbonGroup1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool42, ButtonTool43, ButtonTool44, ButtonTool45});
        RibbonGroup2.Caption = "Add Items";
        ButtonTool25.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool27.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool28.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool30.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool29.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool31.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool34.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool33.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool32.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool35.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool172.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool170.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        RibbonGroup2.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool25, ButtonTool27, ButtonTool28, ButtonTool30, ButtonTool29, ButtonTool31, ButtonTool34, ButtonTool33, ButtonTool32, ButtonTool35, ButtonTool172, ButtonTool170});
        RibbonGroup3.Caption = "Search";
        RibbonGroup3.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool46, ButtonTool47, ButtonTool48});
        RibbonGroup4.Caption = "General";
        ButtonTool49.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool58.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        RibbonGroup4.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool49, ButtonTool58});
        RibbonTab1.Groups.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonGroup() {RibbonGroup1, RibbonGroup2, RibbonGroup3, RibbonGroup4});
        RibbonTab2.Caption = "View";
        RibbonGroup5.Caption = "Window";
        ButtonTool61.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool65.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool52.InstanceProps.IsFirstInGroup = true;
        RibbonGroup5.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool61, ButtonTool65, ButtonTool52, ButtonTool53, ButtonTool54, ButtonTool147, ButtonTool55});
        RibbonTab2.Groups.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonGroup() {RibbonGroup5});
        RibbonTab3.Caption = "Map";
        RibbonTab3.ContextualTabGroupKey = "ctgMapTools";
        RibbonGroup6.Caption = "Map Tools";
        ButtonTool64.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool67.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool68.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool69.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool70.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool71.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        StateButtonTool3.InstanceProps.IsFirstInGroup = true;
        StateButtonTool3.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        StateButtonTool1.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        ButtonTool165.InstanceProps.IsFirstInGroup = true;
        ButtonTool165.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
        RibbonGroup6.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool64, ButtonTool67, ButtonTool68, ButtonTool69, ButtonTool70, ButtonTool71, StateButtonTool3, StateButtonTool1, ButtonTool165});
        RibbonTab3.Groups.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonGroup() {RibbonGroup6});
        Me.UTMMain.Ribbon.NonInheritedRibbonTabs.AddRange(New Infragistics.Win.UltraWinToolbars.RibbonTab() {RibbonTab1, RibbonTab2, RibbonTab3});
        Me.UTMMain.Ribbon.QuickAccessToolbar.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool57, ButtonTool158});
        Me.UTMMain.Ribbon.TabItemToolbar.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool149, ButtonTool56, ButtonTool51, ButtonTool167});
        Me.UTMMain.Ribbon.Visible = true;
        Me.UTMMain.ShowFullMenusDelay = 500;
        Me.UTMMain.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007;
        UltraToolbar1.DockedColumn = 0;
        UltraToolbar1.DockedRow = 1;
        ButtonTool76.InstanceProps.IsFirstInGroup = true;
        ButtonTool79.InstanceProps.IsFirstInGroup = true;
        ButtonTool89.InstanceProps.IsFirstInGroup = true;
        ButtonTool7.InstanceProps.IsFirstInGroup = true;
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool73, ButtonTool74, ButtonTool75, ButtonTool76, ButtonTool77, ButtonTool78, ButtonTool79, ButtonTool80, ButtonTool81, ButtonTool82, ButtonTool83, ButtonTool84, ButtonTool85, ButtonTool86, ButtonTool87, ButtonTool88, ButtonTool89, ButtonTool7});
        UltraToolbar1.Text = "Default";
        UltraToolbar2.DockedColumn = 0;
        UltraToolbar2.DockedRow = 0;
        UltraToolbar2.FloatingLocation = New System.Drawing.Point(480, 362);
        UltraToolbar2.FloatingSize = New System.Drawing.Size(342, 22);
        UltraToolbar2.IsMainMenuBar = true;
        UltraToolbar2.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {PopupMenuTool21, PopupMenuTool22, PopupMenuTool23, PopupMenuTool24, PopupMenuTool25, PopupMenuTool26});
        UltraToolbar2.Text = "MainMenuBar";
        Me.UTMMain.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1, UltraToolbar2});
        PopupMenuTool27.SharedPropsInternal.Caption = "&File";
        PopupMenuTool27.SharedPropsInternal.Category = "Imported";
        PopupMenuTool28.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool30.InstanceProps.IsFirstInGroup = true;
        ButtonTool3.InstanceProps.IsFirstInGroup = true;
        ButtonTool94.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool27.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool90, ButtonTool91, ButtonTool92, ButtonTool93, PopupMenuTool28, PopupMenuTool29, PopupMenuTool30, ButtonTool3, ButtonTool94});
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgNew32;
        ButtonTool95.SharedPropsInternal.AppearancesLarge.Appearance = Appearance3;
        Appearance4.Image = Global.ADRIFT.My.Resources.Resources.imgNew16;
        ButtonTool95.SharedPropsInternal.AppearancesSmall.Appearance = Appearance4;
        ButtonTool95.SharedPropsInternal.Caption = "&New";
        ButtonTool95.SharedPropsInternal.Category = "Imported";
        ButtonTool95.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
        Appearance5.Image = (Object)(resources.GetObject("Appearance5.Image"));
        ButtonTool96.SharedPropsInternal.AppearancesLarge.Appearance = Appearance5;
        Appearance6.Image = (Object)(resources.GetObject("Appearance6.Image"));
        ButtonTool96.SharedPropsInternal.AppearancesSmall.Appearance = Appearance6;
        ButtonTool96.SharedPropsInternal.Caption = "&Open";
        ButtonTool96.SharedPropsInternal.Category = "Imported";
        PopupMenuTool31.SharedPropsInternal.Caption = "&Edit";
        PopupMenuTool31.SharedPropsInternal.Category = "Imported";
        ButtonTool21.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool31.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool97, ButtonTool98, ButtonTool99, ButtonTool100, ButtonTool21, ButtonTool22, ButtonTool20});
        PopupMenuTool32.SharedPropsInternal.Caption = "&View";
        PopupMenuTool32.SharedPropsInternal.Category = "Imported";
        PopupMenuTool32.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool102, ButtonTool103, ButtonTool104});
        ButtonTool105.SharedPropsInternal.Caption = "Create &New List";
        ButtonTool105.SharedPropsInternal.Category = "Imported";
        ButtonTool106.SharedPropsInternal.Caption = "&Rename List";
        ButtonTool106.SharedPropsInternal.Category = "Imported";
        ButtonTool106.SharedPropsInternal.Spring = true;
        ButtonTool107.SharedPropsInternal.Caption = "&Delete List";
        ButtonTool107.SharedPropsInternal.Category = "Imported";
        ButtonTool107.SharedPropsInternal.Spring = true;
        PopupMenuTool33.SharedPropsInternal.Caption = "&Adventure";
        PopupMenuTool33.SharedPropsInternal.Category = "Imported";
        PopupMenuTool33.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {PopupMenuTool34, ButtonTool1, ButtonTool4, ButtonTool6});
        PopupMenuTool35.SharedPropsInternal.Caption = "&Window";
        PopupMenuTool35.SharedPropsInternal.Category = "Imported";
        MdiWindowListTool3.InstanceProps.IsFirstInGroup = false;
        PopupMenuTool35.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool10, ButtonTool11, ButtonTool12, ButtonTool13, MdiWindowListTool3});
        PopupMenuTool36.SharedPropsInternal.Caption = "&Help";
        PopupMenuTool36.SharedPropsInternal.Category = "Imported";
        PopupMenuTool36.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool109});
        MdiWindowListTool4.DisplayArrangeIconsCommand = Infragistics.Win.UltraWinToolbars.MdiWindowListCommandDisplayStyle.Hide;
        MdiWindowListTool4.DisplayCascadeCommand = Infragistics.Win.UltraWinToolbars.MdiWindowListCommandDisplayStyle.Hide;
        MdiWindowListTool4.DisplayCloseWindowsCommand = Infragistics.Win.UltraWinToolbars.MdiWindowListCommandDisplayStyle.Hide;
        MdiWindowListTool4.DisplayTileHorizontalCommand = Infragistics.Win.UltraWinToolbars.MdiWindowListCommandDisplayStyle.Hide;
        MdiWindowListTool4.DisplayTileVerticalCommand = Infragistics.Win.UltraWinToolbars.MdiWindowListCommandDisplayStyle.Hide;
        MdiWindowListTool4.SharedPropsInternal.Caption = "MDIWindowListTool1";
        Appearance7.Image = (Object)(resources.GetObject("Appearance7.Image"));
        ButtonTool110.SharedPropsInternal.AppearancesLarge.Appearance = Appearance7;
        Appearance8.Image = Global.ADRIFT.My.Resources.Resources.imgSave16;
        ButtonTool110.SharedPropsInternal.AppearancesSmall.Appearance = Appearance8;
        ButtonTool110.SharedPropsInternal.Caption = "&Save";
        ButtonTool110.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
        Appearance9.Image = Global.ADRIFT.My.Resources.Resources.imgCut;
        ButtonTool111.SharedPropsInternal.AppearancesSmall.Appearance = Appearance9;
        ButtonTool111.SharedPropsInternal.Caption = "&Cut";
        ButtonTool111.SharedPropsInternal.Enabled = false;
        ButtonTool111.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
        Appearance10.Image = Global.ADRIFT.My.Resources.Resources.imgCopy;
        ButtonTool112.SharedPropsInternal.AppearancesSmall.Appearance = Appearance10;
        ButtonTool112.SharedPropsInternal.Caption = "&Copy";
        ButtonTool112.SharedPropsInternal.Enabled = false;
        ButtonTool112.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
        Appearance11.Image = Global.ADRIFT.My.Resources.Resources.imgPaste;
        ButtonTool113.SharedPropsInternal.AppearancesSmall.Appearance = Appearance11;
        ButtonTool113.SharedPropsInternal.Caption = "&Paste";
        ButtonTool113.SharedPropsInternal.Enabled = false;
        ButtonTool113.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
        Appearance12.Image = (Object)(resources.GetObject("Appearance12.Image"));
        ButtonTool114.SharedPropsInternal.AppearancesLarge.Appearance = Appearance12;
        Appearance13.Image = Global.ADRIFT.My.Resources.Resources.imgDelete;
        ButtonTool114.SharedPropsInternal.AppearancesSmall.Appearance = Appearance13;
        ButtonTool114.SharedPropsInternal.Caption = "&Delete";
        ButtonTool114.SharedPropsInternal.Enabled = false;
        Appearance14.Image = Global.ADRIFT.My.Resources.Resources.imgFind;
        ButtonTool115.SharedPropsInternal.AppearancesSmall.Appearance = Appearance14;
        ButtonTool115.SharedPropsInternal.Caption = "Find";
        ButtonTool115.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
        Appearance15.Image = Global.ADRIFT.My.Resources.Resources.imgLocation32;
        ButtonTool116.SharedPropsInternal.AppearancesLarge.Appearance = Appearance15;
        Appearance16.Image = Global.ADRIFT.My.Resources.Resources.imgLocation16;
        ButtonTool116.SharedPropsInternal.AppearancesSmall.Appearance = Appearance16;
        ButtonTool116.SharedPropsInternal.Caption = "Location";
        ButtonTool116.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
        ButtonTool116.SharedPropsInternal.ToolTipText = "Locations are the places characters can visit";
        ButtonTool116.SharedPropsInternal.ToolTipTitle = "Add Location";
        Appearance17.Image = Global.ADRIFT.My.Resources.Resources.imgObjectDynamic32;
        ButtonTool117.SharedPropsInternal.AppearancesLarge.Appearance = Appearance17;
        Appearance18.Image = (Object)(resources.GetObject("Appearance18.Image"));
        ButtonTool117.SharedPropsInternal.AppearancesSmall.Appearance = Appearance18;
        ButtonTool117.SharedPropsInternal.Caption = "Object";
        ButtonTool117.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
        ButtonTool117.SharedPropsInternal.ToolTipText = "Objects are anything within a location that can be looked at or manipulated";
        ButtonTool117.SharedPropsInternal.ToolTipTitle = "Add Object";
        Appearance19.Image = Global.ADRIFT.My.Resources.Resources.imgTaskGeneral32;
        ButtonTool118.SharedPropsInternal.AppearancesLarge.Appearance = Appearance19;
        Appearance20.Image = (Object)(resources.GetObject("Appearance20.Image"));
        ButtonTool118.SharedPropsInternal.AppearancesSmall.Appearance = Appearance20;
        ButtonTool118.SharedPropsInternal.Caption = " Task ";
        ButtonTool118.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
        ButtonTool118.SharedPropsInternal.ToolTipText = "Tasks allow you to create custom actions in your game";
        ButtonTool118.SharedPropsInternal.ToolTipTitle = "Add Task";
        Appearance21.Image = Global.ADRIFT.My.Resources.Resources.imgEvent32;
        ButtonTool119.SharedPropsInternal.AppearancesLarge.Appearance = Appearance21;
        Appearance22.Image = (Object)(resources.GetObject("Appearance22.Image"));
        ButtonTool119.SharedPropsInternal.AppearancesSmall.Appearance = Appearance22;
        ButtonTool119.SharedPropsInternal.Caption = "Event";
        ButtonTool119.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
        ButtonTool119.SharedPropsInternal.ToolTipText = "Events allow you to make things happen at particular times in your game";
        ButtonTool119.SharedPropsInternal.ToolTipTitle = "Add Event";
        Appearance23.Image = Global.ADRIFT.My.Resources.Resources.imgCharacter32;
        ButtonTool120.SharedPropsInternal.AppearancesLarge.Appearance = Appearance23;
        Appearance24.Image = Global.ADRIFT.My.Resources.Resources.imgCharacter16;
        ButtonTool120.SharedPropsInternal.AppearancesSmall.Appearance = Appearance24;
        ButtonTool120.SharedPropsInternal.Caption = "Character";
        ButtonTool120.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
        ButtonTool120.SharedPropsInternal.ToolTipText = "Characters are people or animals you may encounter in your game";
        ButtonTool120.SharedPropsInternal.ToolTipTitle = "Add Character";
        Appearance25.Image = Global.ADRIFT.My.Resources.Resources.imgVariable32;
        ButtonTool121.SharedPropsInternal.AppearancesLarge.Appearance = Appearance25;
        Appearance26.Image = Global.ADRIFT.My.Resources.Resources.imgVariable16;
        ButtonTool121.SharedPropsInternal.AppearancesSmall.Appearance = Appearance26;
        ButtonTool121.SharedPropsInternal.Caption = "Variable";
        ButtonTool121.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftV;
        ButtonTool121.SharedPropsInternal.ToolTipText = "Variables allow you to keep track of numbers or text in your game";
        ButtonTool121.SharedPropsInternal.ToolTipTitle = "Add Variable";
        Appearance27.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        PopupMenuTool37.SharedPropsInternal.AppearancesSmall.Appearance = Appearance27;
        PopupMenuTool37.SharedPropsInternal.Caption = "Add New";
        PopupMenuTool37.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool122, ButtonTool123, ButtonTool124, ButtonTool125, ButtonTool126, ButtonTool127, ButtonTool128, ButtonTool129, ButtonTool130, ButtonTool131});
        Appearance28.Image = (Object)(resources.GetObject("Appearance28.Image"));
        ButtonTool132.SharedPropsInternal.AppearancesLarge.Appearance = Appearance28;
        ButtonTool132.SharedPropsInternal.Caption = "Save &As";
        ButtonTool132.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
        PopupMenuTool38.SharedPropsInternal.Caption = "Recent Adventures";
        StateButtonTool4.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        StateButtonTool4.SharedPropsInternal.Caption = "Tab Windows";
        Appearance29.Image = Global.ADRIFT.My.Resources.Resources.imgGroup32;
        ButtonTool133.SharedPropsInternal.AppearancesLarge.Appearance = Appearance29;
        Appearance30.Image = Global.ADRIFT.My.Resources.Resources.imgGroup16;
        ButtonTool133.SharedPropsInternal.AppearancesSmall.Appearance = Appearance30;
        ButtonTool133.SharedPropsInternal.Caption = "Group";
        ButtonTool133.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
        ButtonTool133.SharedPropsInternal.ToolTipText = "Locations, Objects and Characters can all be added to groups to all be treated th" + _;
    "e same";
        ButtonTool133.SharedPropsInternal.ToolTipTitle = "Add Group";
        Appearance31.Image = Global.ADRIFT.My.Resources.Resources.imgALR32;
        ButtonTool134.SharedPropsInternal.AppearancesLarge.Appearance = Appearance31;
        Appearance32.Image = Global.ADRIFT.My.Resources.Resources.imgALR16;
        ButtonTool134.SharedPropsInternal.AppearancesSmall.Appearance = Appearance32;
        ButtonTool134.SharedPropsInternal.Caption = "Text Override";
        ButtonTool134.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
        ButtonTool134.SharedPropsInternal.ToolTipText = "Text Overrides allow you to change any final output text in Runner";
        ButtonTool134.SharedPropsInternal.ToolTipTitle = "Add Text Override";
        Appearance33.Image = Global.ADRIFT.My.Resources.Resources.imgHint32;
        ButtonTool135.SharedPropsInternal.AppearancesLarge.Appearance = Appearance33;
        Appearance34.Image = (Object)(resources.GetObject("Appearance34.Image"));
        ButtonTool135.SharedPropsInternal.AppearancesSmall.Appearance = Appearance34;
        ButtonTool135.SharedPropsInternal.Caption = "Hint";
        ButtonTool135.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
        ButtonTool135.SharedPropsInternal.ToolTipText = "You can add hints to give players a little help whilst playing your game";
        ButtonTool135.SharedPropsInternal.ToolTipTitle = "Add Hint";
        ButtonTool136.SharedPropsInternal.Caption = "Module";
        ButtonTool136.SharedPropsInternal.DescriptionOnMenu = "Insert everything from a module into this adventure";
        Appearance35.Image = Global.ADRIFT.My.Resources.Resources.imgGroup32;
        ButtonTool137.SharedPropsInternal.AppearancesLarge.Appearance = Appearance35;
        ButtonTool137.SharedPropsInternal.Caption = "Module";
        ButtonTool137.SharedPropsInternal.DescriptionOnMenu = "Export a module for distribution, or for use as a library";
        Appearance36.Image = Global.ADRIFT.My.Resources.Resources.imgImport32;
        PopupMenuTool39.SharedPropsInternal.AppearancesLarge.Appearance = Appearance36;
        Appearance37.Image = Global.ADRIFT.My.Resources.Resources.imgImport16;
        PopupMenuTool39.SharedPropsInternal.AppearancesSmall.Appearance = Appearance37;
        PopupMenuTool39.SharedPropsInternal.Caption = "&Import";
        PopupMenuTool39.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
        PopupMenuTool39.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool138, ButtonTool153, ButtonTool162, ButtonTool159});
        Appearance38.Image = Global.ADRIFT.My.Resources.Resources.imgExport32;
        PopupMenuTool40.SharedPropsInternal.AppearancesLarge.Appearance = Appearance38;
        Appearance39.Image = Global.ADRIFT.My.Resources.Resources.imgExport16;
        PopupMenuTool40.SharedPropsInternal.AppearancesSmall.Appearance = Appearance39;
        PopupMenuTool40.SharedPropsInternal.Caption = "&Export";
        PopupMenuTool40.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool139, ButtonTool9, ButtonTool26, ButtonTool151, ButtonTool168});
        Appearance40.Image = Global.ADRIFT.My.Resources.Resources.imgSettings32;
        ButtonTool140.SharedPropsInternal.AppearancesLarge.Appearance = Appearance40;
        Appearance41.Image = Global.ADRIFT.My.Resources.Resources.imgSettings16;
        ButtonTool140.SharedPropsInternal.AppearancesSmall.Appearance = Appearance41;
        ButtonTool140.SharedPropsInternal.Caption = "Settings...";
        Appearance42.Image = Global.ADRIFT.My.Resources.Resources.imgProperty32;
        ButtonTool141.SharedPropsInternal.AppearancesLarge.Appearance = Appearance42;
        Appearance43.Image = Global.ADRIFT.My.Resources.Resources.imgProperty16;
        ButtonTool141.SharedPropsInternal.AppearancesSmall.Appearance = Appearance43;
        ButtonTool141.SharedPropsInternal.Caption = "Property";
        ButtonTool141.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
        ButtonTool141.SharedPropsInternal.ToolTipText = "Locations, Objects and Characters can all be given particular attributes";
        ButtonTool141.SharedPropsInternal.ToolTipTitle = "Add Property";
        Appearance44.Image = Global.ADRIFT.My.Resources.Resources.imgInfo16;
        ButtonTool142.SharedPropsInternal.AppearancesSmall.Appearance = Appearance44;
        ButtonTool142.SharedPropsInternal.Caption = "About ADRIFT Developer";
        Appearance45.Image = Global.ADRIFT.My.Resources.Resources.imgRun32;
        ButtonTool143.SharedPropsInternal.AppearancesLarge.Appearance = Appearance45;
        Appearance46.Image = Global.ADRIFT.My.Resources.Resources.imgRun16;
        ButtonTool143.SharedPropsInternal.AppearancesSmall.Appearance = Appearance46;
        ButtonTool143.SharedPropsInternal.Caption = "Run Adventure";
        ButtonTool143.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.F5;
        Appearance47.Image = Global.ADRIFT.My.Resources.Resources.imgCancel16;
        ButtonTool144.SharedPropsInternal.AppearancesSmall.Appearance = Appearance47;
        ButtonTool144.SharedPropsInternal.Caption = "E&xit Developer";
        ButtonTool144.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
        Appearance48.Image = Global.ADRIFT.My.Resources.Resources.imgSynonym32;
        ButtonTool2.SharedPropsInternal.AppearancesLarge.Appearance = Appearance48;
        ButtonTool2.SharedPropsInternal.Caption = "Introduction && End of Game";
        Appearance49.Image = Global.ADRIFT.My.Resources.Resources.imgOptions32;
        ButtonTool5.SharedPropsInternal.AppearancesLarge.Appearance = Appearance49;
        Appearance50.Image = Global.ADRIFT.My.Resources.Resources.imgOptions16;
        ButtonTool5.SharedPropsInternal.AppearancesSmall.Appearance = Appearance50;
        ButtonTool5.SharedPropsInternal.Caption = "Options";
        ButtonTool8.SharedPropsInternal.Caption = "iFiction Record";
        ButtonTool8.SharedPropsInternal.DescriptionOnMenu = "Export the bibliographic record for this text adventure";
        ButtonTool14.SharedPropsInternal.Caption = "Arrange Icons";
        Appearance51.Image = Global.ADRIFT.My.Resources.Resources.imgCascade16;
        ButtonTool15.SharedPropsInternal.AppearancesSmall.Appearance = Appearance51;
        ButtonTool15.SharedPropsInternal.Caption = "Cascade";
        Appearance52.Image = Global.ADRIFT.My.Resources.Resources.imgDelete;
        ButtonTool16.SharedPropsInternal.AppearancesSmall.Appearance = Appearance52;
        ButtonTool16.SharedPropsInternal.Caption = "Close All Windows";
        Appearance53.Image = Global.ADRIFT.My.Resources.Resources.imgArrangeHorz16;
        ButtonTool17.SharedPropsInternal.AppearancesSmall.Appearance = Appearance53;
        ButtonTool17.SharedPropsInternal.Caption = "Tile Horizontally";
        Appearance54.Image = (Object)(resources.GetObject("Appearance54.Image"));
        ButtonTool18.SharedPropsInternal.AppearancesSmall.Appearance = Appearance54;
        ButtonTool18.SharedPropsInternal.Caption = "Tile Vertically";
        Appearance55.Image = (Object)(resources.GetObject("Appearance55.Image"));
        ButtonTool19.SharedPropsInternal.AppearancesSmall.Appearance = Appearance55;
        ButtonTool19.SharedPropsInternal.Caption = "Replace";
        ButtonTool19.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
        Appearance56.Image = Global.ADRIFT.My.Resources.Resources.imgFindNext;
        ButtonTool23.SharedPropsInternal.AppearancesLarge.Appearance = Appearance56;
        Appearance57.Image = Global.ADRIFT.My.Resources.Resources.imgFindNext;
        ButtonTool23.SharedPropsInternal.AppearancesSmall.Appearance = Appearance57;
        Appearance58.Image = Global.ADRIFT.My.Resources.Resources.imgFindNext;
        ButtonTool23.SharedPropsInternal.AppearancesSmall.AppearanceOnMenu = Appearance58;
        ButtonTool23.SharedPropsInternal.Caption = "Find Next";
        ButtonTool23.SharedPropsInternal.Enabled = false;
        ButtonTool23.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
        ButtonTool24.SharedPropsInternal.Caption = "Windows Executable";
        ButtonTool24.SharedPropsInternal.DescriptionOnMenu = "Create a file that can be distributed as a standalone executable";
        LabelTool2.SharedPropsInternal.Caption = "Recent Documents";
        Appearance59.Image = Global.ADRIFT.My.Resources.Resources.imgUnlock32;
        ButtonTool60.SharedPropsInternal.AppearancesLarge.Appearance = Appearance59;
        ButtonTool60.SharedPropsInternal.Caption = "Protect Adventure";
        Appearance60.Image = Global.ADRIFT.My.Resources.Resources.imgFolderList32;
        ButtonTool62.SharedPropsInternal.AppearancesLarge.Appearance = Appearance60;
        ButtonTool62.SharedPropsInternal.Caption = "Folder List";
        ButtonTool62.SharedPropsInternal.Enabled = false;
        Appearance61.Image = Global.ADRIFT.My.Resources.Resources.imgKey16;
        ButtonTool63.SharedPropsInternal.AppearancesSmall.Appearance = Appearance61;
        ButtonTool63.SharedPropsInternal.Caption = "Unlock ADRIFT";
        Appearance62.Image = (Object)(resources.GetObject("Appearance62.Image"));
        ButtonTool66.SharedPropsInternal.AppearancesLarge.Appearance = Appearance62;
        Appearance63.Image = (Object)(resources.GetObject("Appearance63.Image"));
        ButtonTool66.SharedPropsInternal.AppearancesSmall.Appearance = Appearance63;
        ButtonTool66.SharedPropsInternal.Caption = "Map";
        ButtonTool66.SharedPropsInternal.Enabled = false;
        Appearance64.Image = Global.ADRIFT.My.Resources.Resources.imgLocation32;
        ButtonTool72.SharedPropsInternal.AppearancesLarge.Appearance = Appearance64;
        ButtonTool72.SharedPropsInternal.Caption = "Add Location";
        Appearance65.Image = Global.ADRIFT.My.Resources.Resources.imgPlanView32;
        ButtonTool101.SharedPropsInternal.AppearancesLarge.Appearance = Appearance65;
        ButtonTool101.SharedPropsInternal.Caption = "Plan View";
        ButtonTool101.SharedPropsInternal.ToolTipText = "Reset map to plan view";
        Appearance66.Image = Global.ADRIFT.My.Resources.Resources.imgCentre32;
        ButtonTool108.SharedPropsInternal.AppearancesLarge.Appearance = Appearance66;
        ButtonTool108.SharedPropsInternal.Caption = "Centralise Map";
        Appearance67.Image = Global.ADRIFT.My.Resources.Resources.imgZoomIn32;
        ButtonTool145.SharedPropsInternal.AppearancesLarge.Appearance = Appearance67;
        ButtonTool145.SharedPropsInternal.Caption = "Zoom In";
        Appearance68.Image = Global.ADRIFT.My.Resources.Resources.imgZoomOut32;
        ButtonTool146.SharedPropsInternal.AppearancesLarge.Appearance = Appearance68;
        ButtonTool146.SharedPropsInternal.Caption = "Zoom Out";
        Appearance69.Image = Global.ADRIFT.My.Resources.Resources.imgWarn16;
        ButtonTool148.SharedPropsInternal.AppearancesSmall.Appearance = Appearance69;
        ButtonTool148.SharedPropsInternal.Caption = "Map Layout Warning";
        ButtonTool148.SharedPropsInternal.Visible = false;
        Appearance70.Image = Global.ADRIFT.My.Resources.Resources.imgAxes32;
        StateButtonTool2.SharedPropsInternal.AppearancesLarge.Appearance = Appearance70;
        StateButtonTool2.SharedPropsInternal.Caption = "Show Axes";
        Appearance71.Image = Global.ADRIFT.My.Resources.Resources.imgGrid16;
        StateButtonTool5.SharedPropsInternal.AppearancesLarge.Appearance = Appearance71;
        Appearance72.Image = Global.ADRIFT.My.Resources.Resources.imgGrid16;
        StateButtonTool5.SharedPropsInternal.AppearancesSmall.Appearance = Appearance72;
        StateButtonTool5.SharedPropsInternal.Caption = "Show Grid Lines";
        Appearance73.Image = Global.ADRIFT.My.Resources.Resources.imgTile16;
        ButtonTool150.SharedPropsInternal.AppearancesSmall.Appearance = Appearance73;
        ButtonTool150.SharedPropsInternal.Caption = "Tile";
        ButtonTool152.SharedPropsInternal.Caption = "Blorb";
        ButtonTool152.SharedPropsInternal.DescriptionOnMenu = "Package your adventure into a single file containing all media used";
        ButtonTool154.SharedPropsInternal.Caption = "Blorb";
        ButtonTool154.SharedPropsInternal.DescriptionOnMenu = "Extract all the media from a Blorb file";
        ButtonTool156.SharedPropsInternal.Caption = "Properties";
        Appearance74.Image = Global.ADRIFT.My.Resources.Resources.imgNew32;
        ButtonTool41.SharedPropsInternal.AppearancesLarge.Appearance = Appearance74;
        Appearance75.Image = Global.ADRIFT.My.Resources.Resources.imgNew16;
        ButtonTool41.SharedPropsInternal.AppearancesSmall.Appearance = Appearance75;
        ButtonTool41.SharedPropsInternal.Caption = "Add Page";
        ButtonTool160.SharedPropsInternal.Caption = "Trizbort";
        ButtonTool160.SharedPropsInternal.DescriptionOnMenu = "Import map layout";
        ButtonTool161.SharedPropsInternal.Caption = "Language Resource";
        ButtonTool161.SharedPropsInternal.DescriptionOnMenu = "Import Text Overrides from a text file";
        Appearance76.Image = Global.ADRIFT.My.Resources.Resources.imgPrint32;
        ButtonTool163.SharedPropsInternal.AppearancesLarge.Appearance = Appearance76;
        Appearance77.Image = Global.ADRIFT.My.Resources.Resources.imgPrint16;
        ButtonTool163.SharedPropsInternal.AppearancesSmall.Appearance = Appearance77;
        ButtonTool163.SharedPropsInternal.Caption = "Print";
        ButtonTool164.SharedPropsInternal.Caption = "Print Preview...";
        ButtonTool166.SharedPropsInternal.Caption = "Language Resource";
        ButtonTool166.SharedPropsInternal.DescriptionOnMenu = "Export Text Overrides into a text file";
        Appearance78.Image = Global.ADRIFT.My.Resources.Resources.imgHelp16;
        ButtonTool169.SharedPropsInternal.AppearancesSmall.Appearance = Appearance78;
        ButtonTool169.SharedPropsInternal.Caption = "Help";
        Appearance79.Image = Global.ADRIFT.My.Resources.Resources.imgFunction32;
        ButtonTool171.SharedPropsInternal.AppearancesLarge.Appearance = Appearance79;
        Appearance80.Image = Global.ADRIFT.My.Resources.Resources.imgFunction16;
        ButtonTool171.SharedPropsInternal.AppearancesSmall.Appearance = Appearance80;
        ButtonTool171.SharedPropsInternal.Caption = "User Function";
        Appearance81.Image = Global.ADRIFT.My.Resources.Resources.imgSynonym32;
        ButtonTool173.SharedPropsInternal.AppearancesLarge.Appearance = Appearance81;
        Appearance82.Image = Global.ADRIFT.My.Resources.Resources.imgSynonym16;
        ButtonTool173.SharedPropsInternal.AppearancesSmall.Appearance = Appearance82;
        ButtonTool173.SharedPropsInternal.Caption = "Synonym";
        Me.UTMMain.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {PopupMenuTool27, ButtonTool95, ButtonTool96, PopupMenuTool31, PopupMenuTool32, ButtonTool105, ButtonTool106, ButtonTool107, PopupMenuTool33, PopupMenuTool35, PopupMenuTool36, MdiWindowListTool4, ButtonTool110, ButtonTool111, ButtonTool112, ButtonTool113, ButtonTool114, ButtonTool115, ButtonTool116, ButtonTool117, ButtonTool118, ButtonTool119, ButtonTool120, ButtonTool121, PopupMenuTool37, ButtonTool132, PopupMenuTool38, StateButtonTool4, ButtonTool133, ButtonTool134, ButtonTool135, ButtonTool136, ButtonTool137, PopupMenuTool39, PopupMenuTool40, ButtonTool140, ButtonTool141, ButtonTool142, ButtonTool143, ButtonTool144, ButtonTool2, ButtonTool5, ButtonTool8, ButtonTool14, ButtonTool15, ButtonTool16, ButtonTool17, ButtonTool18, ButtonTool19, ButtonTool23, ButtonTool24, LabelTool2, ButtonTool60, ButtonTool62, ButtonTool63, ButtonTool66, ButtonTool72, ButtonTool101, ButtonTool108, ButtonTool145, ButtonTool146, ButtonTool148, StateButtonTool2, StateButtonTool5, ButtonTool150, ButtonTool152, ButtonTool154, ButtonTool156, ButtonTool41, ButtonTool160, ButtonTool161, ButtonTool163, ButtonTool164, ButtonTool166, ButtonTool169, ButtonTool171, ButtonTool173});
        '
        '_frmGenerator_Toolbars_Dock_Area_Right
        '
        Me._frmGenerator_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmGenerator_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(191), Integer), (Byte)(CType(219), Integer), (Byte)(CType(255), Integer));
        Me._frmGenerator_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
        Me._frmGenerator_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmGenerator_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 8;
        Me._frmGenerator_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(921, 164);
        Me._frmGenerator_Toolbars_Dock_Area_Right.Name = "_frmGenerator_Toolbars_Dock_Area_Right";
        Me._frmGenerator_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(8, 480);
        Me._frmGenerator_Toolbars_Dock_Area_Right.ToolbarsManager = Me.UTMMain;
        '
        '_frmGenerator_Toolbars_Dock_Area_Top
        '
        Me._frmGenerator_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmGenerator_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(191), Integer), (Byte)(CType(219), Integer), (Byte)(CType(255), Integer));
        Me._frmGenerator_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
        Me._frmGenerator_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmGenerator_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0);
        Me._frmGenerator_Toolbars_Dock_Area_Top.Name = "_frmGenerator_Toolbars_Dock_Area_Top";
        Me._frmGenerator_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(929, 164);
        Me._frmGenerator_Toolbars_Dock_Area_Top.ToolbarsManager = Me.UTMMain;
        '
        '_frmGenerator_Toolbars_Dock_Area_Bottom
        '
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(191), Integer), (Byte)(CType(219), Integer), (Byte)(CType(255), Integer));
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 644);
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.Name = "_frmGenerator_Toolbars_Dock_Area_Bottom";
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(929, 0);
        Me._frmGenerator_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.UTMMain;
        '
        'UDMGenerator
        '
        Me.UDMGenerator.CompressUnpinnedTabs = false;
        DockAreaPane1.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.VerticalSplit
        DockAreaPane1.DockedBefore = New System.Guid("02625fb5-30ea-419f-a5ee-f435e59086b2")
        DockableControlPane1.Control = Me.FolderList1
        DockableControlPane1.FlyoutSize = New System.Drawing.Size(194, -1)
        DockableControlPane1.OriginalControlBounds = New System.Drawing.Rectangle(12, 54, 192, 429)
        Appearance1.Image = Global.ADRIFT.My.Resources.Resources.imgFolder16;
        DockableControlPane1.Settings.TabAppearance = Appearance1
        DockableControlPane1.Size = New System.Drawing.Size(100, 100)
        DockableControlPane1.Text = "Folders"
        DockAreaPane1.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane1})
        DockAreaPane1.Size = New System.Drawing.Size(194, 480)
        DockAreaPane2.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.VerticalSplit
        DockAreaPane2.DockedBefore = New System.Guid("da8e20b7-4eff-497d-ac07-a6389585feb4")
        DockAreaPane2.FloatingLocation = New System.Drawing.Point(567, 408)
        DockAreaPane2.Size = New System.Drawing.Size(186, 326)
        DockAreaPane3.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.VerticalSplit
        DockAreaPane3.FloatingLocation = New System.Drawing.Point(567, 408)
        DockableControlPane2.Control = Me.Map1
        DockableControlPane2.FlyoutSize = New System.Drawing.Size(-1, 237)
        DockableControlPane2.OriginalControlBounds = New System.Drawing.Rectangle(593, 296, 266, 254)
        Appearance2.Image = (Object)(resources.GetObject("Appearance2.Image"));
        DockableControlPane2.Settings.TabAppearance = Appearance2
        DockableControlPane2.Size = New System.Drawing.Size(100, 100)
        DockableControlPane2.Text = "Map"
        DockAreaPane3.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane2})
        DockAreaPane3.Size = New System.Drawing.Size(714, 237)
        Me.UDMGenerator.DockAreas.AddRange(New Infragistics.Win.UltraWinDock.DockAreaPane() {DockAreaPane1, DockAreaPane2, DockAreaPane3});
        Me.UDMGenerator.DragWindowStyle = Infragistics.Win.UltraWinDock.DragWindowStyle.LayeredWindowWithIndicators;
        Me.UDMGenerator.HostControl = Me;
        Me.UDMGenerator.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.VisualStudio2008;
        '
        '_frmGeneratorUnpinnedTabAreaLeft
        '
        Me._frmGeneratorUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
        Me._frmGeneratorUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmGeneratorUnpinnedTabAreaLeft.Location = New System.Drawing.Point(8, 164);
        Me._frmGeneratorUnpinnedTabAreaLeft.Name = "_frmGeneratorUnpinnedTabAreaLeft";
        Me._frmGeneratorUnpinnedTabAreaLeft.Owner = Me.UDMGenerator;
        Me._frmGeneratorUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 480);
        Me._frmGeneratorUnpinnedTabAreaLeft.TabIndex = 24;
        '
        '_frmGeneratorUnpinnedTabAreaRight
        '
        Me._frmGeneratorUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
        Me._frmGeneratorUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmGeneratorUnpinnedTabAreaRight.Location = New System.Drawing.Point(921, 164);
        Me._frmGeneratorUnpinnedTabAreaRight.Name = "_frmGeneratorUnpinnedTabAreaRight";
        Me._frmGeneratorUnpinnedTabAreaRight.Owner = Me.UDMGenerator;
        Me._frmGeneratorUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 480);
        Me._frmGeneratorUnpinnedTabAreaRight.TabIndex = 25;
        '
        '_frmGeneratorUnpinnedTabAreaTop
        '
        Me._frmGeneratorUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
        Me._frmGeneratorUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmGeneratorUnpinnedTabAreaTop.Location = New System.Drawing.Point(8, 164);
        Me._frmGeneratorUnpinnedTabAreaTop.Name = "_frmGeneratorUnpinnedTabAreaTop";
        Me._frmGeneratorUnpinnedTabAreaTop.Owner = Me.UDMGenerator;
        Me._frmGeneratorUnpinnedTabAreaTop.Size = New System.Drawing.Size(913, 0);
        Me._frmGeneratorUnpinnedTabAreaTop.TabIndex = 26;
        '
        '_frmGeneratorUnpinnedTabAreaBottom
        '
        Me._frmGeneratorUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        Me._frmGeneratorUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmGeneratorUnpinnedTabAreaBottom.Location = New System.Drawing.Point(8, 644);
        Me._frmGeneratorUnpinnedTabAreaBottom.Name = "_frmGeneratorUnpinnedTabAreaBottom";
        Me._frmGeneratorUnpinnedTabAreaBottom.Owner = Me.UDMGenerator;
        Me._frmGeneratorUnpinnedTabAreaBottom.Size = New System.Drawing.Size(913, 0);
        Me._frmGeneratorUnpinnedTabAreaBottom.TabIndex = 27;
        '
        '_frmGeneratorAutoHideControl
        '
        Me._frmGeneratorAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmGeneratorAutoHideControl.Location = New System.Drawing.Point(30, 160);
        Me._frmGeneratorAutoHideControl.Name = "_frmGeneratorAutoHideControl";
        Me._frmGeneratorAutoHideControl.Owner = Me.UDMGenerator;
        Me._frmGeneratorAutoHideControl.Size = New System.Drawing.Size(199, 484);
        Me._frmGeneratorAutoHideControl.TabIndex = 28;
        '
        'WindowDockingArea1
        '
        Me.WindowDockingArea1.Controls.Add(Me.DockableWindow1);
        Me.WindowDockingArea1.Dock = System.Windows.Forms.DockStyle.Left;
        Me.WindowDockingArea1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea1.Location = New System.Drawing.Point(8, 164);
        Me.WindowDockingArea1.Name = "WindowDockingArea1";
        Me.WindowDockingArea1.Owner = Me.UDMGenerator;
        Me.WindowDockingArea1.Size = New System.Drawing.Size(199, 480);
        Me.WindowDockingArea1.TabIndex = 29;
        '
        'DockableWindow1
        '
        Me.DockableWindow1.Controls.Add(Me.FolderList1);
        Me.DockableWindow1.Location = New System.Drawing.Point(0, 0);
        Me.DockableWindow1.Name = "DockableWindow1";
        Me.DockableWindow1.Owner = Me.UDMGenerator;
        Me.DockableWindow1.Size = New System.Drawing.Size(194, 480);
        Me.DockableWindow1.TabIndex = 48;
        '
        'DockableWindow2
        '
        Me.DockableWindow2.Controls.Add(Me.Map1);
        Me.DockableWindow2.Location = New System.Drawing.Point(0, 0);
        Me.DockableWindow2.Name = "DockableWindow2";
        Me.DockableWindow2.Owner = Me.UDMGenerator;
        Me.DockableWindow2.Size = New System.Drawing.Size(714, 237);
        Me.DockableWindow2.TabIndex = 49;
        '
        'WindowDockingArea3
        '
        Me.WindowDockingArea3.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.WindowDockingArea3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea3.Location = New System.Drawing.Point(8, 8);
        Me.WindowDockingArea3.Name = "WindowDockingArea3";
        Me.WindowDockingArea3.Owner = Me.UDMGenerator;
        Me.WindowDockingArea3.Size = New System.Drawing.Size(186, 326);
        Me.WindowDockingArea3.TabIndex = 0;
        '
        'WindowDockingArea4
        '
        Me.WindowDockingArea4.Controls.Add(Me.DockableWindow2);
        Me.WindowDockingArea4.Dock = System.Windows.Forms.DockStyle.Top;
        Me.WindowDockingArea4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea4.Location = New System.Drawing.Point(207, 164);
        Me.WindowDockingArea4.Name = "WindowDockingArea4";
        Me.WindowDockingArea4.Owner = Me.UDMGenerator;
        Me.WindowDockingArea4.Size = New System.Drawing.Size(714, 242);
        Me.WindowDockingArea4.TabIndex = 42;
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "ADRIFT 5 Help.chm";
        '
        'cmsMain
        '
        Me.cmsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miAddFolder});
        Me.cmsMain.Name = "cmsMain";
        Me.cmsMain.Size = New System.Drawing.Size(133, 26);
        '
        'miAddFolder
        '
        Me.miAddFolder.Image = Global.ADRIFT.My.Resources.Resources.imgFolder16;
        Me.miAddFolder.Name = "miAddFolder";
        Me.miAddFolder.Size = New System.Drawing.Size(132, 22);
        Me.miAddFolder.Text = "Add Folder";
        '
        'frmGenerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.ClientSize = New System.Drawing.Size(929, 668);
        Me.ContextMenuStrip = Me.cmsMain;
        Me.Controls.Add(Me._frmGeneratorAutoHideControl);
        Me.Controls.Add(Me.WindowDockingArea4);
        Me.Controls.Add(Me.WindowDockingArea1);
        Me.Controls.Add(Me._frmGeneratorUnpinnedTabAreaBottom);
        Me.Controls.Add(Me._frmGeneratorUnpinnedTabAreaTop);
        Me.Controls.Add(Me._frmGeneratorUnpinnedTabAreaRight);
        Me.Controls.Add(Me._frmGeneratorUnpinnedTabAreaLeft);
        Me.Controls.Add(Me._frmGenerator_Toolbars_Dock_Area_Left);
        Me.Controls.Add(Me._frmGenerator_Toolbars_Dock_Area_Right);
        Me.Controls.Add(Me._frmGenerator_Toolbars_Dock_Area_Bottom);
        Me.Controls.Add(Me.StatusBar1);
        Me.Controls.Add(Me._frmGenerator_Toolbars_Dock_Area_Top);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.IsMdiContainer = true;
        Me.Name = "frmGenerator";
        Me.Text = "ADRIFT Developer";
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UTMMain).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UDMGenerator).EndInit();
        Me.WindowDockingArea1.ResumeLayout(false);
        Me.DockableWindow1.ResumeLayout(false);
        Me.DockableWindow2.ResumeLayout(false);
        Me.WindowDockingArea4.ResumeLayout(false);
        Me.cmsMain.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    'Private Declare Function GetActiveWindow Lib "user32" () As Int32
    Private Declare Function GetCurrentThreadId Lib "kernel32" () As Integer
    Private Declare Function GetForegroundWindow Lib "user32" () As IntPtr
    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As IntPtr, lpdwProcessId As Integer) As Integer


    'Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
    Private WithEvents tmrElapsed As New Timer
    public frmSettings fSettings;
    public frmOptions fOptions;
    private string _sDestinationList;
    public string sDestinationList
        {
            get
            {
            return _sDestinationList;
        }
set(String)
            _sDestinationList = value
        }
    }
    private Splash fSplash;
    public Folder ActiveFolder;
    public bool PartOne = false;
    private bool bPartTwo = false;
    public bool PartTwo
        {
            get
            {
            return bPartTwo;
        }
set(Boolean)
            bPartTwo = value
            iRegistered = 0
        }
    }


    private string sKeyPrefix = "" 'UNDEFINED";
    public string KeyPrefix
        {
            get
            {
            'If sKeyPrefix = "UNDEFINED" Then
            'sKeyPrefix = GetSetting("ADRIFT", "Generator", "KeyPrefix", "")
            'End If
            return sKeyPrefix;
        }
set(String)
            sKeyPrefix = value
            'SaveSetting("ADRIFT", "Generator", "KeyPrefix", value)
        }
    }


    private bool bSimpleMode = false;
    private bool bLoadedSimpleMode = false;
    public bool SimpleMode
        {
            get
            {
            if (Not bLoadedSimpleMode)
            {
                bSimpleMode = SafeBool(GetSetting("ADRIFT", "Generator", "SimpleMode", CInt(True).ToString))
                bLoadedSimpleMode = True
                SetSimple(bSimpleMode);
            }
            return bSimpleMode;
        }
set(Boolean)
            if (value <> bSimpleMode)
            {
                bSimpleMode = value
                SaveSetting("ADRIFT", "Generator", "SimpleMode", CInt(bSimpleMode).ToString);
                SetSimple(bSimpleMode);
            }
        }
    }



    private bool _AutoComplete = false;
    private bool _LoadedAutoComplete = false;
    public bool AutoComplete
        {
            get
            {
            if (Not _LoadedAutoComplete)
            {
                _AutoComplete = SafeBool(GetSetting("ADRIFT", "Generator", "AutoComplete", CInt(True).ToString))
                _LoadedAutoComplete = True
            }
            return _AutoComplete;
        }
set(Boolean)
            if (value <> _AutoComplete)
            {
                _AutoComplete = value
                SaveSetting("ADRIFT", "Generator", "AutoComplete", CInt(_AutoComplete).ToString);
            }
        }
    }


    private void SetSimple(bool bSimple)
    {
        SetToolVisible(fGenerator.UTMMain.Tools("Variable"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("Group"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("Property"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("Text Override"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("Hint"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("Synonym"), ! bSimple);
        SetToolVisible(fGenerator.UTMMain.Tools("UserFunction"), ! bSimple);
        if (bSimple)
        {
            StatusBar1.Panels(3).Text = "Simple Mode: On";
        Else
            StatusBar1.Panels(3).Text = "";
        }
        'UpdateMainTitle()
    }


    private void SetToolVisible(Infragistics.Win.UltraWinToolbars.ToolBase tool, bool bVisible, Infragistics.Win.UltraWinToolbars.RibbonGroup grp = null)
    {

        if (Not fGenerator.UTMMain.Ribbon.Tabs("Home").Groups.Exists("Hidden"))
        {
            fGenerator.UTMMain.Ribbon.Tabs("Home").Groups.Add("Hidden");
            fGenerator.UTMMain.Ribbon.Tabs("Home").Groups("Hidden").Visible = false;
        }

        If grp == null Then grp = fGenerator.UTMMain.Ribbon.Tabs("Home").Groups("ribbonGroup1")

        private Infragistics.Win.UltraWinToolbars.RibbonGroup grpSource;
        private Infragistics.Win.UltraWinToolbars.RibbonGroup grpDest;

        if (bVisible)
        {
            grpSource = fGenerator.UTMMain.Ribbon.Tabs("Home").Groups("Hidden")
            grpDest = grp
        Else
            grpSource = grp
            grpDest = fGenerator.UTMMain.Ribbon.Tabs("Home").Groups("Hidden")
        }

        if (Not grpDest.Tools.Exists(tool.Key))
        {
            grpSource.Tools.Remove(tool);
            grpDest.Tools.AddTool(tool.Key);
            With grpDest.Tools(tool.Key);
                .InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
            }
        }

    }


    private void frmGenerator_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        If ! CheckForSave() Then e.Cancel = true
        UDMGenerator.SaveAsXML(DataPath(true) + "DeveloperLayout.xml");
    }


    ' Returns True if nothing needs saving, or saves OK, or user doesn't want to save
    ' Returns False if user cancels
    private bool CheckForSave()
    {

        if (Adventure IsNot null && Adventure.Changed)
        {
            switch (MessageBox.Show("Would you like to save your changes to this adventure?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case Windows.Forms.DialogResult.Yes:
                    {
                    return mnuSave();
                case Windows.Forms.DialogResult.No:
                    {
                    return true;
                case Windows.Forms.DialogResult.Cancel:
                    {
                    return false;
            }
        Else
            return true;
        }

    }


    private string GetWinSysDir()
    {

        'Dim strPath As String
        'strPath = Space$(1024)
        'Dim iLen As Integer = CInt(GetSystemDirectory(strPath, Len(strPath)))
        'strPath = sLeft(strPath, iLen)
        'If sRight(strPath, 1) <> "\" Then strPath = strPath & "\"

        'GetWinSysDir = strPath
        return Environment.SystemDirectory + IO.Path.DirectorySeparatorChar;
    }



    private void frmGenerator_Load(object sender, System.EventArgs e)
    {

        try
        {

            fGenerator = Me

            'If Not IsRegistered() Then
            '    MessageBox.Show("ADRIFT Developer v5.0 Alpha is only available to registered users.  Sorry.", "Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End
            'End If

            GlobalStartup();
            IntroMessage();
            GetSettings();

            private bool bSetSimple = SimpleMode;

            UTMMain.Tools("Register").SharedProps.Visible = ! IsRegistered;

            'If Not IO.Directory.Exists(sLocalDataPath) Then IO.Directory.CreateDirectory(sLocalDataPath)

            if (IO.File.Exists(DataPath(true) + "DeveloperLayout.xml"))
            {
                try
                {
                    UDMGenerator.LoadFromXML(DataPath(true) + "DeveloperLayout.xml");
                Catch
                    ' It's not the end of the world if this fails
                }
            }

            if (Not ControlInForm(Map1))
            {
                UTMMain.Tools("Map").SharedProps.Enabled = true;
                UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = false;
            }
            If ! ControlInForm(FolderList1) Then UTMMain.Tools("FolderList").SharedProps.Enabled = true

            eStyle = EnumParseViewStyle(GetSetting("ADRIFT", "Generator", "ViewStyle", "Office2007"))
            switch (eStyle)
            {
                case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013:
                    {
                    eColour2013 = EnumParseColourScheme2013(GetSetting("ADRIFT", "Generator", "ColourScheme", "LightGray"))
                    Infragistics.Win.Office2013ColorTable.ColorScheme = eColour2013;
                case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010:
                    {
                    eColour2010 = EnumParseColourScheme2010(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"))
                    Infragistics.Win.Office2010ColorTable.ColorScheme = eColour2010;
                case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007:
                    {
                    eColour2007 = EnumParseColourScheme2007(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"))
                    Infragistics.Win.Office2007ColorTable.ColorScheme = eColour2007;
            }

            eOverwriteLibraries = CType(SafeInt(GetSetting("ADRIFT", "Generator", "OverwriteLibraries", "0")), OverwriteLibrariesEnum)
            bEnableGraphics = CBool(SafeInt(GetSetting("ADRIFT", "Generator", "EnableGraphics", "-1")))
            bEnableAudio = CBool(SafeInt(GetSetting("ADRIFT", "Generator", "EnableAudio", "-1")))
            bEnablePreview = CBool(SafeInt(GetSetting("ADRIFT", "Generator", "EnablePreview", "-1")))

            GetFormPosition(Me, UTMMain, UDMGenerator);
            NewAdventure();
            AddPrevious(fGenerator.UTMMain, "Generator");

            if (Environment.GetCommandLineArgs.Length > 1)
            {
                if (IO.File.Exists(Environment.GetCommandLineArgs(1)))
                {
                    OpenAdventure(Environment.GetCommandLineArgs(1));
                }
            }

            tmrElapsed.Interval = 1000;
            tmrElapsed.Start();

        }
        catch (Exception ex)
        {
            ErrMsg("Error starting up Developer", ex);
        }

    }


    private bool mnuSave()
    {

        if (Adventure.Filename <> "" && Adventure.Filename <> "untitled.taf")
        {
            if (Adventure.dVersion < 5)
            {
                If MessageBox.Show("This adventure is version " + Adventure.dVersion.ToString("0.0") + ".  Are you sure you wish to overwrite with version 5.0 format?", "Save Adventure", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Function
            }
            try
            {
                private string sBackup = Adventure.FullPath;
                If sBackup.EndsWith(".taf") Then sBackup = sBackup.Replace(".taf", ".bak") Else sBackup &= ".bak"
                IO.File.Copy(Adventure.FullPath, sBackup, true);
            }
            catch (Exception ex)
            {

            }
            if (SaveFile(Adventure.FullPath, true))
            {
                Adventure.Changed = false;
                mnuSave = True
                AddFileToList(Adventure.FullPath);
            }
        Else
            return mnuSaveAs();
        }

    }


    private bool mnuSaveAs()
    {

        sfd.Filter = "ADRIFT Text Adventures (*.taf)|*.taf|All Files (*.*)|*.*";
        sfd.DefaultExt = "taf";
        If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("taf") Then sfd.FileName = ""
        sfd.Title = "Save ADRIFT Adventure";
        sfd.ShowDialog();

        if (sfd.FileName <> "")
        {
            if (SaveFile(sfd.FileName, true))
            {
                Adventure.Changed = false;
                mnuSaveAs = True
                AddFileToList(Adventure.FullPath);
            }
        }

    }


    private void ExportLanguageResource()
    {

        sfd.Filter = "Language Resource Files (*.alr)|*.alr|All Files (*.*)|*.*";
        sfd.DefaultExt = "alr";
        If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("alr") Then sfd.FileName = ""
        sfd.ShowDialog();

        If sfd.FileName <> "" Then ExportALR(sfd.FileName)

    }


    private void ExportBlorb()
    {

        sfd.Filter = "ADRIFT Blorb File (*.blorb)|*.blorb|All Files (*.*)|*.*";
        sfd.DefaultExt = "blorb";
        If sfd.FileName.Length > 5 && ! sfd.FileName.ToLower.EndsWith("blorb") Then sfd.FileName = ""
        sfd.Title = "Export ADRIFT Blorb";
        if (sfd.ShowDialog() = Windows.Forms.DialogResult.OK)
        {
            if (sfd.FileName <> "")
            {
                If SaveBlorb(sfd.FileName) Then Adventure.Changed = true
            }
        }
    }


    private bool OutputBlorb(clsBlorb blorb)
    {

        if (clsBlorb.ExecType <> "" && clsBlorb.ExecType <> "ADRI")
        {
            switch (MessageBox.Show("This Blorb contains an executable that is not in ADRIFT format.  Are you sure you wish to expand the Blorb?", "Output Blorb", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                case Windows.Forms.DialogResult.Yes:
                    {
                    ' Continue
                case Windows.Forms.DialogResult.No:
                    {
                    return false;
            }
        }

        fbd.Description = "Select folder to output Blorb contents to";
        fbd.ShowDialog();
        if (fbd.SelectedPath <> "")
        {
            If blorb.OutputToFolder(fbd.SelectedPath) Then MessageBox.Show("Blorb extracted successfully.", "Output Blorb", MessageBoxButtons.OK, MessageBoxIcon.Information)
        }

        return true;

    }



    private void AddFolder(string sFolder)
    {
        ExportList.Add(sFolder);

        For Each sMember As String In Adventure.dictFolders(sFolder).Members
            if (Adventure.dictFolders.ContainsKey(sMember))
            {
                AddFolder(sMember);
            Else
                ExportList.Add(sMember);
            }
        Next;

    }


    internal void ExportModule(string sFolder = "")
    {

        sfd.Filter = "ADRIFT Module File (*.amf)|*.amf|All Files (*.*)|*.*";
        sfd.DefaultExt = "amf";
        If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("amf") Then sfd.FileName = ""
        if (sFolder = "")
        {
            sfd.Title = "Export Module";
        Else
            sfd.Title = "Export folder """ + Adventure.dictFolders(sFolder).Name + """ as a Module";
        }

        sfd.ShowDialog();

        if (sfd.FileName <> "")
        {
            ' Check to see if user only wants to save selected items
            private int iSelected = 0;

            if (sFolder = "")
            {
                For Each folder As frmFolder In fGenerator.MDIFolders
                    iSelected += folder.Folder.lstContents.SelectedItems.Count;
                    If iSelected > 3 Then Exit For
                Next;
            }

            if (iSelected > 3 && sFolder = "")
            {
                switch (MessageBox.Show("Would you like to export the selected items only?", "Export Module", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case Windows.Forms.DialogResult.Yes:
                        {
                        iSelected = -1
                    case Windows.Forms.DialogResult.No:
                        {
                        iSelected = 0
                    case Windows.Forms.DialogResult.Cancel:
                        {
                        Exit Sub;
                }
            }

            if (sFolder <> "")
            {
                ExportList = New List(Of String)
                AddFolder(sFolder);
                iSelected = -1
            Else
                ExportList = Nothing
            }

            SaveFile(sfd.FileName, false, bSelectedOnly:=CBool(iSelected));
        }

    }


    private void ExportiFictionRecord()
    {

        sfd.Filter = "iFiction Records (*.iFiction)|*.iFiction|All Files (*.*)|*.*";
        sfd.DefaultExt = "iFiction";
        If sfd.FileName.Length > 8 && ! sfd.FileName.ToLower.EndsWith("iFiction") Then sfd.FileName = ""
        sfd.Title = "Export iFiction Record";
        sfd.ShowDialog();

        If sfd.FileName <> "" Then SaveiFictionRecord(sfd.FileName)

    }


    private void mnuOpen()
    {
        OpenAdventureDialog(ofd);
    }



    'Private Sub ListClick(ByVal sList As String)

    '    Try
    '        ' First, check to see if the list is already open
    '        For Each lst As frmList In Me.MdiChildren
    '            If lst.Text = sList Then
    '                lst.Show()
    '                lst.Focus()
    '                Exit Sub
    '            End If
    '        Next

    '        Dim ListInfo As ListInfo = CType(colLists(sList), ListInfo)
    '        ShowList(ListInfo)

    '    Catch ex As Exception
    '        MessageBox.Show("Error defining List: " & ex.Message, "ADRIFT Developer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Sub



    private void NewAdventure()
    {
        if (CheckForSave())
        {
            'SaveListsToXML()
            'ClearMenuLists()
            Adventure = New clsAdventure
            'LoadDefaults()
            LoadLibraries(LoadWhatEnum.All);
            'CreateMandatoryProperties()

            private bool bNoLibrary = Adventure.dictAllItems.Keys.Count = 0;
            fGenerator.FolderList1.InitialiseTree();
            'If UDMGenerator.ControlPanes.Count > 1 Then
            '    ActiveFolder = CType(UDMGenerator.ControlPanes(1).Control, Folder)
            'End If
            'LoadLists(True)
            'Me.Text = "Untitled - ADRIFT Developer - [" & Adventure.Filename & "]"
            UpdateMainTitle();
            fGenerator.StatusBar1.Panels(0).Text = "File version: " + Adventure.Version;
            fGenerator.StatusBar1.Panels(1).Text = "File size: 0 bytes";
            fGenerator.StatusBar1.Panels(2).Text = "Maximum score: 0";
            fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.Caption = "Protect Adventure";
            fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Resources.imgUnlock32;

            if (bNoLibrary)
            {
                if (MessageBox.Show("You do not appear to have any libraries loaded.  Would you like to set these now?", "new Adventure", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes)
                {
                    Me.Show();
                    DoToolClick("Settings", "", "Libraries")
                    if (fSettings IsNot null && fSettings.Visible)
                    {
                        fSettings.TopMost = true;
                        fSettings.BringToFront();
                    }
                }
            }

        }
    }



    private void frmGenerator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        'SaveListsToXML()
        SaveFormPosition(Me);
        'If Me.WindowState = FormWindowState.Normal Then
        '    SaveSetting("ADRIFT", "Generator", "Window Height", (Me.Height * 15).ToString)
        '    SaveSetting("ADRIFT", "Generator", "Window Width", (Me.Width * 15).ToString)
        'End If
    }


    private void UTMMain_BeforeToolActivate(object sender, Infragistics.Win.UltraWinToolbars.CancelableToolEventArgs e)
    {
        Debug.WriteLine(e.Tool.Key);
    }

    private void UTMMain_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
    {
        'call FindPropId on the ChangeInfo of the event args and pass in Infragistics.Win.UltraWinToolbars.PropertyIds.IsMinimized.
        'If the PropChangeInfo returned is not null and its Source property is the Ribbon, its minimization has changed.
        Debug.WriteLine(e.ChangeInfo.PropId.ToString);
        private Infragistics.Shared.PropChangeInfo prop = e.ChangeInfo.FindPropId(Infragistics.Win.UltraWinToolbars.PropertyIds.IsMinimized);
        if (prop IsNot null && prop.Source Is UTMMain.Ribbon)
        {
            SaveSetting("ADRIFT", "Generator", "RibbonMinimized", CInt(UTMMain.Ribbon.IsMinimized).ToString);
        }
    }



    private void UTMMain_ToolClick(System.Object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
        DoToolClick(e.Tool.Key, e.Tool.SharedProps.Caption, CStr(e.Tool.SharedProps.Tag))
    }



    private void DoToolClick(string sKey, string sCaption, string sTag)
    {

        'If sTag = "_LIST_" Then
        '    ListClick(sCaption)
        '    Exit Sub
        'End If

        if (sTag = "_RECENT_")
        {
            If CheckForSave() Then OpenAdventure(sKey)
            Exit Sub;
        }

        switch (sKey)
        {
            case "Location":
            case "Object":
            case "Task":
            case "Event":
            case "Character":
            case "Variable":
            case "Property":
            case "Group":
            case "Text Override":
            case "Hint":
                {
                sDestinationList = sKey.Replace("Property", "Propertie") & "s"
        }

        switch (sKey)
        {
            case "AboutGenerator":
                {
                private New AboutBox fAbout;
                try
                {
                    fAbout.ShowDialog();
                Catch
                }

                'Case "mnuCreateNewList"
                '    CreateNewList()
                'Case "mnuDeleteList"
                '    DeleteList()
            case "Properties":
                {
                Properties();
            case "Location":
                {
                private New frmLocation(New clsLocation, True) fLocation;
            case "Object":
                {
                private New frmObject(New clsObject, True) fObject;
            case "Task":
                {
                private New frmTask(New clsTask, True) fTask;
            case "Event":
                {
                private New frmEvent(New clsEvent, True) fEvent;
            case "Character":
                {
                private New frmCharacter(New clsCharacter, True) fCharacter;
            case "Variable":
                {
                private New frmVariable(New clsVariable, True) fVariable;
            case "Property":
                {
                private New frmProperty(New clsProperty, True) fProperty;
            case "Group":
                {
                private New frmGroup(New clsGroup, True) fGroup;
            case "Hint":
                {
                private New frmHint(New clsHint, True) fHint;
            case "Text Override":
                {
                private New frmTextOverride(New clsALR) fALR;
            case "UserFunction":
                {
                private New frmUserFunction(New clsUserFunction) fUDF;
            case "Synonym":
                {
                private New frmSynonym(New clsSynonym) fSynonym;
            case "mnuNew":
                {
                NewAdventure();
            case "mnuOpen":
                {
                mnuOpen();
            case "mnuSave":
                {
                mnuSave();
            case "mnuSaveAs":
                {
                Adventure.BabelTreatyInfo.Stories(0).Identification.GenerateNewIFID();
                mnuSaveAs();
                'Case "mnuRenameList"
                '    CType(Me.ActiveMdiChild, frmList).Rename()
            case "RunAdventure":
                {
                if (IO.File.Exists(BinPath(true) + "run500.exe"))
                {
                    If CheckForSave() Then System.Diagnostics.Process.Start(BinPath(true) + "run500.exe", Adventure.FullPath)
                Else
                    ErrMsg("Runner executable not found.");
                }
                'Case "Tab Windows"
                '    TabWindows()
            case "ImportModule":
                {
                OpenModuleDialog(ofd);
            case "ImportTrizbort":
                {
                OpenTrizbortDialog(ofd);
            case "LanguageResource":
                {
                OpenALRDialog(ofd);
            case "ExportLanguageResource":
                {
                ExportLanguageResource();
            case "ExportModule":
                {
                ExportModule();
            case "ExportBlorb":
                {
                ExportBlorb();
            case "ImportBlorb":
                {
                private clsBlorb blorbImported = OpenBlorbDialog(ofd);
                If blorbImported != null Then OutputBlorb(blorbImported)
                'blorbImported.Dispose()
            case "Options":
                {
                if (fOptions Is null)
                {
                    fOptions = New frmOptions
                Else
                    fOptions.Focus();
                }
            case "Settings":
                {
                if (fSettings Is null)
                {
                    fSettings = New frmSettings
                Else
                    fSettings.Focus();
                }
                If sTag = "Libraries" Then fSettings.tabsOptions.SelectedTab = fSettings.tabsOptions.Tabs(1)
            case "Cut":
                {
                'CutItem(CType(ActiveMdiChild, frmList).ItemList.lvwList.SelectedItems(0).SubItems(2).Text)
                If Form.ActiveForm == fGenerator && ActiveFolder != null Then CutItems(ActiveFolder.SelectedKeys)
            case "Copy":
                {
                'CopyItem(CType(ActiveMdiChild, frmList).ItemList.lvwList.SelectedItems(0).SubItems(2).Text)
                If Form.ActiveForm == fGenerator && ActiveFolder != null Then CopyItems(ActiveFolder.SelectedKeys)
            case "Delete":
                {
                If Form.ActiveForm == fGenerator && ActiveFolder != null Then DeleteItems(ActiveFolder.SelectedKeys)
            case "Paste":
                {
                'PasteItem()
                If Form.ActiveForm == fGenerator && ActiveFolder != null Then PasteItems()
            case "Exit":
                {
                Me.Close();
            case "IntroductionWinning":
                {
                private New frmIntroWinning fIntro;
            case "iFictionRecord":
                {
                ExportiFictionRecord();
            case "ArrangeIcons":
                {
                Me.LayoutMdi(MdiLayout.ArrangeIcons);
            case "Cascade":
                {
                'Me.LayoutMdi(MdiLayout.Cascade)
                private Size sizeMDI;
                For Each ctl As Control In Me.Controls
                    if (TypeOf ctl Is MdiClient)
                    {
                        sizeMDI = New Size(ctl.Size.Width - 4, ctl.Size.Height - 4)
                        Exit For;
                    }
                Next;
                private int iCount = Me.MdiChildren.Length - 1;
                for (int i = 0; i <= iCount; i++)
                {
                    Me.MdiChildren(i).Location = New Point(i * CInt(sizeMDI.Width / (iCount + 2)), i * 32);
                    Me.MdiChildren(i).Size = New Size(CInt(2 * sizeMDI.Width / (iCount + 2)), sizeMDI.Height - (32 * iCount + 1));
                    Me.MdiChildren(i).BringToFront();
                Next;
            case "CloseAllWindows":
                {
                for (int i = Me.MdiChildren.Length - 1; i <= 0; i += -1)
                {
                    Me.MdiChildren(i).Close();
                Next;
            case "Tile":
                {
                private Size sizeMDI;
                For Each ctl As Control In Me.Controls
                    if (TypeOf ctl Is MdiClient)
                    {
                        sizeMDI = New Size(ctl.Size.Width - 4, ctl.Size.Height - 4)
                        Exit For;
                    }
                Next;
                if (Me.MdiChildren.Length > 0)
                {
                    private int iCols = CInt(Math.Ceiling(Math.Sqrt(Me.MdiChildren.Length)));
                    private int iRows = CInt(Math.Ceiling((Me.MdiChildren.Length) / iCols));
                    for (int iRow = 0; iRow <= iRows - 1; iRow++)
                    {
                        for (int iCol = 0; iCol <= iCols - 1; iCol++)
                        {
                            private int i = iRow * iCols + iCol;
                            if (i < MdiChildren.Length)
                            {
                                With MdiChildren(i);
                                    .Width = CInt(sizeMDI.Width / iCols);
                                    .Height = CInt(sizeMDI.Height / iRows);
                                    .Location = New Point(.Width * iCol, .Height * iRow);
                                    while (.Location.X + .Width > sizeMDI.Width)
                                    {
                                        private int iWidth = .Width;
                                        .Width -= 1;
                                        If iWidth = .Width Then Exit While ' Bug 8595
                                    }
                                    while (.Location.Y + .Height > sizeMDI.Height)
                                    {
                                        private int iHeight = .Height;
                                        .Height -= 1;
                                        If iHeight = .Height Then Exit While
                                    }
                                }
                            }
                        Next;
                    Next;
                }
            case "TileHorizontally":
            case "TileVertically":
                {
                private MdiClient mc = null;
                for (int i = 0; i <= Me.Controls.Count - 1; i++)
                {
                    if ((Me.Controls(i).GetType().Equals(GetType(MdiClient))))
                    {
                        mc = CType(Me.Controls(i), MdiClient)
                    }
                Next;

                private int iVisCount = 0;
                For Each child As Form In MdiChildren
                    If child.Visible Then iVisCount += 1
                Next;

                private int iCount = 0;
                private int iSum = 0;
                For Each frmChild As Form In MdiChildren
                    if (frmChild.Visible)
                    {
                        frmChild.WindowState = FormWindowState.Normal;
                        if (sKey = "TileHorizontally")
                        {
                            private int iHeight = CInt(Math.Floor((mc.Height - 4) / iVisCount));
                            iSum += iHeight;
                            frmChild.Size = New Size(mc.Width - 4, iHeight);
                            frmChild.Location = New Point(0, iCount * iHeight);
                            if (iCount = iVisCount - 1)
                            {
                                frmChild.Height = frmChild.Height + mc.Height - iSum - 4;
                            }
                        Else
                            private int iWidth = CInt(Math.Floor((mc.Width - 4) / iVisCount));
                            iSum += iWidth;
                            frmChild.Size = New Size(iWidth, mc.Height - 4);
                            frmChild.Location = New Point(iCount * iWidth, 0);
                            if (iCount = iVisCount - 1)
                            {
                                frmChild.Width = frmChild.Width + mc.Width - iSum - 4;
                            }
                        }
                        iCount += 1;
                    }
                Next;

            case "Find":
                {
                private New SearchReplace frmFind;
                If fSearch != null && ! fSearch.IsDisposed Then frmFind = fSearch Else frmFind = New SearchReplace
                frmFind.SetFind();
                frmFind.Owner = Me;
                frmFind.Show();
                'Dim sFind As String = InputBox("Enter search string:", "Find text")
                'If sFind <> "" Then Search(sFind)

            case "FindNext":
                {
                If SearchOptions.sLastSearch <> "" Then Search(SearchOptions.sLastSearch)

            case "Replace":
                {
                private SearchReplace frmReplace;
                If fSearch != null && ! fSearch.IsDisposed Then frmReplace = fSearch Else frmReplace = New SearchReplace
                frmReplace.SetReplace();
                frmReplace.Owner = Me;
                frmReplace.Show();
                'Dim sFind As String = InputBox("Enter search string:", "Replace text")
                'If sFind <> "" Then
                '    Dim sReplace As String = InputBox("Enter replace string:", "Replace text")
                '    If sReplace <> "" Then SearchAndReplace(sFind, sReplace)
                'End If

            case "CreateExecutable":
                {
                CreateExecutable();

            case "ProtectAdventure":
                {
                private New Password fPassword;
                With fPassword;
                    .txtPassword.Text = "";
                    if (sCaption = "Protect Adventure")
                    {
                        .Text = "Protect Adventure";
                        .lblText.Text = "Enter password to protect adventure with:";
                        if (fPassword.ShowDialog = Windows.Forms.DialogResult.OK)
                        {
                            private string sPass1 = .txtPassword.Text;
                            if (sPass1 <> "")
                            {
                                .lblText.Text = "Please confirm password:";
                                .txtPassword.Text = "";
                                if (fPassword.ShowDialog = Windows.Forms.DialogResult.OK)
                                {
                                    if (sPass1 = .txtPassword.Text)
                                    {
                                        Adventure.Password = sPass1;
                                        MessageBox.Show("Adventure is now password protected.", "Protect Adventure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.Caption = "Unprotect Adventure";
                                        fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Resources.imgLock32;
                                    Else
                                        ErrMsg("Passwords do not match.  Adventure is not protected.");
                                    }
                                }
                            Else
                                ErrMsg("You cannot protect an adventure with a blank password!");
                            }
                        }
                    Else
                        .Text = "Unprotect Adventure";
                        .lblText.Text = "Enter existing password to remove it from the adventure:";
                        if (fPassword.ShowDialog = Windows.Forms.DialogResult.OK)
                        {
                            if (.txtPassword.Text = Adventure.Password)
                            {
                                Adventure.Password = "";
                                MessageBox.Show("Password has now been removed from adventure.", "Unprotect Adventure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.Caption = "Protect Adventure";
                                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Resources.imgUnlock32;
                            Else
                                ErrMsg("Incorrect password.  Adventure is still protected.");
                            }
                        }
                    }
                }

            case "FolderList":
                {
                For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMGenerator.ControlPanes
                    if (cp.Control Is FolderList1)
                    {
                        cp.Closed = false;
                        UTMMain.Tools("FolderList").SharedProps.Enabled = false;
                    }
                Next;

            case "Map":
                {
                For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMGenerator.ControlPanes
                    if (cp.Control Is Map1)
                    {
                        cp.Closed = false;
                        UTMMain.Tools("Map").SharedProps.Enabled = false;
                    }
                Next;

            case "Register":
                {
                private New frmRegister fRegister;
                fRegister.ShowDialog();

            case "MapLocation":
                {
                Map1.AddLocation();

            case "MapPlan":
                {
                Map1.PlanView();

            case "MapCentre":
                {
                Map1.CentreMap();

            case "MapZoomIn":
                {
                Map1.Zoom(true);

            case "MapZoomOut":
                {
                Map1.Zoom(false);

            case "MapWarning":
                {
                Map1.tabsMap.Tabs(UTMMain.Tools("MapWarning").Tag.ToString).Selected = true;

            case "Show Axes":
                {
                Map1.ShowAxes = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools(sKey)).Checked;

            case "ShowGridLines":
                {
                Map1.ShowGrid = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools(sKey)).Checked;

            case "NewPage":
                {
                Map1.AddPage();

            case "Print":
                {
                Map1.Print();

            case "Help":
                {
                ShowHelp(Me);

            default:
                {
                TODO("Tool Click : " + sKey);

        }

    }


    private void Properties()
    {
        If Adventure == null Then Exit Sub

        private TimeSpan tElapsed = new TimeSpan(0, 0, Adventure.iElapsed);
        private string sElapsed;
        if (tElapsed.TotalDays > 24)
        {
            sElapsed = Math.Floor(tElapsed.TotalDays) & " Day" & IIf(Math.Floor(tElapsed.TotalDays) > 1, "s ", " ").ToString & Math.Floor(tElapsed.Hours) & " Hour" & IIf(Math.Floor(tElapsed.Hours) > 1, "s", "").ToString
        ElseIf tElapsed.TotalHours > 1 Then
            sElapsed = Math.Floor(tElapsed.TotalHours) & " Hour" & IIf(Math.Floor(tElapsed.TotalHours) > 1, "s ", " ").ToString & Math.Floor(tElapsed.Minutes) & " Minute" & IIf(Math.Floor(tElapsed.Minutes) > 1, "s", "").ToString
        Else
            sElapsed = Math.Floor(tElapsed.TotalMinutes) & " Minute" & IIf(Math.Floor(tElapsed.TotalMinutes) > 1, "s", "").ToString
        }

        private int iTotal = 0;
        private int iTotalIncLibrary = 0;

        private int iCount = 0;
        private string sLocations = "";
        For Each l As clsLocation In Adventure.htblLocations.Values
            If ! l.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblLocations.Count)
        {
            sLocations = iCount.ToString
        Else
            sLocations = iCount & " (" & Adventure.htblLocations.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblLocations.Count;

        iCount = 0
        private string sObjects = "";
        For Each o As clsObject In Adventure.htblObjects.Values
            If ! o.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblObjects.Count)
        {
            sObjects = iCount.ToString
        Else
            sObjects = iCount & " (" & Adventure.htblObjects.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblObjects.Count;

        iCount = 0
        private string sTasks = "";
        For Each t As clsTask In Adventure.htblTasks.Values
            If ! t.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblTasks.Count)
        {
            sTasks = iCount.ToString
        Else
            sTasks = iCount & " (" & Adventure.htblTasks.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblTasks.Count;

        iCount = 0
        private string sCharacters = "";
        For Each c As clsCharacter In Adventure.htblCharacters.Values
            If ! c.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblCharacters.Count)
        {
            sCharacters = iCount.ToString
        Else
            sCharacters = iCount & " (" & Adventure.htblCharacters.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblCharacters.Count;

        iCount = 0
        private string sEvents = "";
        For Each e As clsEvent In Adventure.htblEvents.Values
            If ! e.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblEvents.Count)
        {
            sEvents = iCount.ToString
        Else
            sEvents = iCount & " (" & Adventure.htblEvents.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblEvents.Count;

        iCount = 0
        private string sVariables = "";
        For Each v As clsVariable In Adventure.htblVariables.Values
            If ! v.IsLibrary Then iCount += 1
        Next;
        if (iCount = Adventure.htblVariables.Count)
        {
            sVariables = iCount.ToString
        Else
            sVariables = iCount & " (" & Adventure.htblVariables.Count & " including libraries)"
        }
        iTotal += iCount;
        iTotalIncLibrary += Adventure.htblVariables.Count;

        private string sSize = GetFileSize(Adventure.FullPath);
        If sSize = "" Then sSize = "! saved yet"

        MessageBox.Show("Size: " + sSize + vbCrLf + vbCrLf _;
 + "Locations: " + sLocations + vbCrLf _;
 + "Objects: " + sObjects + vbCrLf _;
 + "Tasks: " + sTasks + vbCrLf _;
 + "Characters: " + sCharacters + vbCrLf _;
 + "Events: " + sEvents + vbCrLf _;
 + "Variables: " + sVariables + vbCrLf + vbCrLf _;
 + "Total Items: " + iTotal + " (" + iTotalIncLibrary + " including libraries)" + vbCrLf + vbCrLf _;
 + "Total Editing Time: " + sElapsed + vbCrLf _;
                , Adventure.Title + " Properties", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    public Generic.List<frmFolder> MDIFolders { get; }
        {
            get
            {
            private New Generic.List<frmFolder> folders;
            For Each child As Form In MdiChildren
                If TypeOf child == frmFolder Then folders.Add((frmFolder)(child))
            Next;
            return folders;
        }
    }


    private void CreateExecutable()
    {

        try
        {
            if (Not IsRegistered)
            {
                NotAvailable();
                Exit Sub;
            }

            if (IO.File.Exists(BinPath + IO.Path.DirectorySeparatorChar + "run500.exe"))
            {
                if (CheckForSave())
                {
                    'System.Diagnostics.Process.Start(Application.StartupPath & "\run500.exe", Adventure.FullPath)
                    sfd.Filter = "Executables (*.exe)|*.exe|All Files (*.*)|*.*";
                    sfd.DefaultExt = "exe";
                    If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("exe") Then sfd.FileName = ""
                    sfd.ShowDialog();

                    if (sfd.FileName <> "")
                    {
                        IO.File.Copy(BinPath + IO.Path.DirectorySeparatorChar + "run500.exe", sfd.FileName, true);
                        'SaveFile(sfd.FileName, True, New IO.FileInfo(Application.StartupPath & "\run500.exe").Length)
                        SaveBlorb(sfd.FileName, IO.FileMode.Append);

                        ' Write the size of Runner to the end of the file for reference later
                        private New IO.FileStream(sfd.FileName, IO.FileMode.Append) stmEXE;
                        private long lLength = new IO.FileInfo(BinPath + IO.Path.DirectorySeparatorChar + "run500.exe").Length;
                        stmEXE.Write((New System.Text.UTF8Encoding).GetBytes(String.Format("{0:X6}", lLength)), 0, 6);
                        stmEXE.Close();
                    }

                }
            Else
                ErrMsg("Runner executable not found for packaging.");
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Unable to create executable", ex);
        }

    }


    'Private Sub TabWindows()
    '    Me.UltraTabbedMdiManager1.Enabled = CType(Me.UTMMain.Tools("Tab Windows"), Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked
    'End Sub


    'Private Sub DeleteList()

    '    Dim frmSource As frmList = CType(Me.ActiveMdiChild, frmList)
    '    If frmSource Is Nothing Then Exit Sub

    '    If MessageBox.Show("Are you sure you wish to delete list '" & frmSource.Text & "'?", "Delete List", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
    '        ' Move all list items from this list to another one

    '        Dim SourceList As ListInfo = frmSource.ListInfo
    '        Dim DestList As ListInfo = Nothing

    '        For Each li As ListInfo In colLists
    '            If li.sName <> Me.ActiveMdiChild.Text Then
    '                DestList = li
    '                Exit For
    '            End If
    '        Next

    '        If Not DestList Is Nothing Then
    '            Dim frmDest As frmList = DestList.GetForm
    '            If Not frmDest Is Nothing Then
    '                For Each lvi As ListViewItem In frmSource.ItemList.lvwList.Items
    '                    frmSource.ItemList.lvwList.Items.Remove(lvi)
    '                    frmDest.ItemList.lvwList.Items.Add(lvi)
    '                Next
    '            End If
    '            ' Copy colKeys too
    '            For Each sKey As String In SourceList.arlKeys
    '                DestList.arlKeys.Add(sKey)
    '            Next

    '            frmSource.Visible = False
    '            frmSource.Dispose()

    '            colLists.Remove(SourceList.sName)
    '            UTMMain.Tools.Remove(UTMMain.Tools("_LIST_" & SourceList.sName))
    '            SourceList = Nothing

    '        Else
    '            ErrMsg("Sorry, you must have at least one defined list.")
    '        End If
    '    End If

    'End Sub

    'Private Sub CreateNewList()

    '    Dim sName As String = InputBox("Please enter name for new list", "New List", "")

    '    If sName <> "" Then

    '        If Not DoesListExist(sName) Then
    '            Dim NewList As New ListInfo(sName)
    '            With NewList
    '                '.sName = sName
    '                .bVisible = True
    '                .s = New Size(200, 500)
    '                .p = New Point(100, 20)
    '            End With
    '            colLists.Add(NewList, NewList.sName)

    '            Dim frmList As New frmList(NewList)
    '        Else
    '            ErrMsg("List already exists.")
    '        End If

    '    End If

    'End Sub

    'Private Function DoesListExist(ByVal sKey As String) As Boolean

    '    For Each li As ListInfo In colLists
    '        If li.sName = sKey Then Return True
    '    Next

    '    Return False

    'End Function

    Private WithEvents tmrSplash As New Timer
    private void frmGenerator_Shown(object sender, System.EventArgs e)
    {
        tmrSplash.Interval = 3000;
        tmrSplash.Start();
        If CBool(CInt(GetSetting("ADRIFT", "Shared", "AutoCheck", "1"))) Then CheckForUpdate()
    }


    private void tmrSplash_Tick(object sender, System.EventArgs e)
    {
        fSplash.Close();
        tmrSplash.Enabled = false;
    }

    private void UDMGenerator_AfterDockChange(object sender, Infragistics.Win.UltraWinDock.PaneEventArgs e)
    {
        if (Map1.ParentForm.Name = Me.Name)
        {
            UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = true;
            Map1.ToolStrip1.Visible = false;
        Else
            UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = false;
            Map1.ToolStrip1.Visible = true;
        }
    }

    private void UDMGenerator_AfterPaneButtonClick(object sender, Infragistics.Win.UltraWinDock.PaneButtonEventArgs e)
    {
        if (e.Button = Infragistics.Win.UltraWinDock.PaneButton.Close)
        {
            If ! Map1.Visible Then UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = false
        }
    }

    private void UDMGenerator_PaneActivate(object sender, Infragistics.Win.UltraWinDock.ControlPaneEventArgs e)
    {
        'If e.Pane.Control Is Map1 AndAlso Not e.Pane.DockedState = Infragistics.Win.UltraWinDock.DockedState.Floating Then UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = True
        if (e.Pane.Control Is Map1 && UTMMain.Ribbon.SelectedTab.Key = "Home")
        {
            ' Only do this if we click on the map - so we don't auto select the Map tab when the app gets focus for example
            if (Map1.PointToClient(MousePosition).X > 0 && Map1.PointToClient(MousePosition).Y > 0 && Map1.PointToClient(MousePosition).X < Map1.Width && Map1.PointToClient(MousePosition).Y < Map1.Height)
            {
                UTMMain.Ribbon.SelectedTab = UTMMain.Ribbon.Tabs("Map");
            }
        }
    }

    private void UDMGenerator_PaneDeactivate(object sender, Infragistics.Win.UltraWinDock.ControlPaneEventArgs e)
    {
        'If e.Pane.Control Is Map1 Then UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = False
        If e.Pane.Control == Map1 && UTMMain.Ribbon.SelectedTab.Key = "Map" Then UTMMain.Ribbon.SelectedTab = UTMMain.Ribbon.Tabs("Home")
    }

    private void UDMGenerator_PaneDisplayed(object sender, Infragistics.Win.UltraWinDock.PaneDisplayedEventArgs e)
    {
        switch (true)
        {
            case e.Pane.Control Is Map1:
                {
                UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = true;
        }
    }

    private void UDMGenerator_PaneHidden(object sender, Infragistics.Win.UltraWinDock.PaneHiddenEventArgs e)
    {
        switch (true)
        {
            case e.Pane.Control Is FolderList1:
                {
                If e.Pane.Closed Then UTMMain.Tools("FolderList").SharedProps.Enabled = true
            case e.Pane.Control Is Map1:
                {
                If e.Pane.Closed Then UTMMain.Tools("Map").SharedProps.Enabled = true
                UTMMain.Ribbon.ContextualTabGroups("ctgMapTools").Visible = false;
        }
    }


    private bool ControlInForm(Control ctrl)
    {

        For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMGenerator.ControlPanes
            If cp.Control == ctrl Then Return ! cp.Closed
        Next;
        'If ctrl Is Me Then
        '    Return True
        'ElseIf TypeOf ctrl Is Infragistics.Win.UltraWinDock.DockableControlPane Then ' MdiClient Then
        '    Return CType(ctrl, MdiClient).Visible
        'ElseIf ctrl.Parent Is Nothing Then
        '    Return False
        'Else
        '    Return ControlInForm(ctrl.Parent)
        'End If

    }


    private void tmrElapsed_Tick(System.Object sender, System.EventArgs e)
    {
        private int iWhere = 0;
        try
        {
            if (Adventure IsNot null)
            {
                private IntPtr hWnd;
                private int ProcessID;
                private int CurrentThreadID;
                private int ActiveThreadID;

                iWhere = 1
                hWnd = GetForegroundWindow
                iWhere = 2
                ActiveThreadID = GetWindowThreadProcessId(hWnd, ProcessID)
                iWhere = 3
                CurrentThreadID = GetCurrentThreadId
                iWhere = 4
                if (CurrentThreadID = ActiveThreadID)
                {
                    Adventure.iElapsed += 1;
                }

            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error obtaining active thread ," + iWhere, ex);
        }
    }


    private void StatusBar1_PanelDoubleClick(object sender, Infragistics.Win.UltraWinStatusBar.PanelClickEventArgs e)
    {

        if (e.Panel Is StatusBar1.Panels(2))
        {
            if (Adventure.htblVariables.ContainsKey("MaxScore"))
            {
                private clsVariable var = Adventure.htblVariables("MaxScore");
                var.EditItem();
            }
        ElseIf e.Panel == StatusBar1.Panels(3) Then
            SimpleMode = Not SimpleMode
        }

    }


    private void miAddFolder_Click(object sender, EventArgs e)
    {

        private New clsFolder newFolder;

        if (Adventure IsNot null)
        {
            Adventure.dictFolders.Add(newFolder.Key, newFolder);
            Adventure.dictFolders("ROOT").Members.Add(newFolder.Key);
        }

        'Dim subitems() As Infragistics.Win.UltraWinListView.UltraListViewSubItem = {New Infragistics.Win.UltraWinListView.UltraListViewSubItem, New Infragistics.Win.UltraWinListView.UltraListViewSubItem, New Infragistics.Win.UltraWinListView.UltraListViewSubItem, New Infragistics.Win.UltraWinListView.UltraListViewSubItem}
        'subitems(0).Value = newFolder.Created
        'subitems(1).Value = newFolder.LastUpdated
        'subitems(2).Value = "Folder"
        'subitems(3).Value = newFolder.Key

        'Dim itmFolder As New Infragistics.Win.UltraWinListView.UltraListViewItem(newFolder.Name, subitems)
        'itmFolder.Key = newFolder.Key
        'itmFolder.Appearance.Image = My.Resources.Resources.imgFolderClosed16

        'Dim nodFolder As Infragistics.Win.UltraWinTree.UltraTreeNode = fGenerator.FolderList1.AddFolder(newFolder, fGenerator.FolderList1.treeFolders.GetNodeByKey("Desktop"))
        'nodFolder.Selected = True
        'fGenerator.FolderList1.OpenNewFolder(nodFolder.Key)
        'fGenerator.FolderList1.RenameFolder(nodFolder)

        private Infragistics.Win.UltraWinTree.UltraTreeNode nodFolder = fGenerator.FolderList1.AddFolder(newFolder);
        nodFolder.Selected = true;
        fGenerator.FolderList1.OpenNewFolder(nodFolder.Key);
        fGenerator.FolderList1.RenameFolder(nodFolder);

        For Each folderGUI As frmFolder In fGenerator.MDIFolders
            if (folderGUI.Folder.folder.Members.Contains(newFolder.Key))
            {
                folderGUI.Folder.AddSingleItem(newFolder.Key);
            }
        Next;

    }

}

}