using Infragistics.Win.UltraWinToolbars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


public class frmRunner
{
    Inherits System.Windows.Forms.Form;

    private int iCommand;
    Friend WithEvents DockableWindow1 As Infragistics.Win.UltraWinDock.DockableWindow;
    Friend WithEvents pnlTop As System.Windows.Forms.Panel;
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel;
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer;
    Friend WithEvents WindowDockingArea3 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents btnMore As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnSubmit As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents ctxMenu As New ContextMenuStrip;
    Friend WithEvents DockableWindow3 As Infragistics.Win.UltraWinDock.DockableWindow;
    Friend WithEvents WindowDockingArea1 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents WindowDockingArea5 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    'Friend dictMacros As New Generic.Dictionary(Of String, clsMacro)
    private Splash fSplash;
    'Private bEXE As Boolean = False

    ' MouseWheel scrolling
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    public const int EM_GETLINECOUNT = &HBA;
    public const int EM_LINESCROLL = &HB6;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        UserSession = New RunnerSession
        GlobalStartup();

        if (Environment.GetCommandLineArgs.Length = 1)
        {

            ' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
            ' Grab out the last 8 bytes, and see if it converts to a size
            Dim bData(5) As Byte
            private string sFilename = Application.ExecutablePath;
            private New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read) fStream;
            fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin);
            fStream.Read(bData, 0, 6);
            'fStream.Close()

            private string sFileSize = (new System.Text.ASCIIEncoding).GetString(bData).ToUpper;
            if (IsHex(sFileSize))
            {
                private long lFileSize = CLng("&H" + sFileSize);
                if (lFileSize > 0)
                {
                    Blorb = New clsBlorb
                    fStream.Seek(lFileSize, IO.SeekOrigin.Begin);
                    clsBlorb.bEXE = true;
                    If Blorb.LoadBlorb(fStream, sFilename, lFileSize) Then UserSession.bEXE = true
                    '' Ok, check the offset to see that the appended data is really a TAF...
                    'fStream = New IO.FileStream(Application.ExecutablePath, IO.FileMode.Open, IO.FileAccess.Read)
                    'fStream.Seek(lFileSize, IO.SeekOrigin.Begin)
                    'ReDim bData(11)
                    'fStream.Read(bData, 0, 12)
                    'fStream.Close()
                    'Dim sVersion As String = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(bData), 1))
                    'If sVersion = "Version 5.00" Then bEXE = True
                }
            }
            fStream.Close();
        }

        if (Not UserSession.bEXE)
        {
            fSplash = New Splash
            fSplash.Show(Me);
            Application.DoEvents();
        Else
            UserSession.ShowUserSplash();
        }

        'This call is required by the Windows Form Designer.
        InitializeComponent();
        Me.DockableWindow1.Controls.Clear();
        Me.DockableWindow1.Controls.Add(UserSession.Map);
        (Infragistics.Win.UltraWinDock.DockableControlPane)(UDMRunner.PaneFromKey("Map")).Control = UserSession.Map;
        MapHolder.Parent = null;
        MapHolder.Dispose();
        'Debug.WriteLine(UDMRunner.DockAreas(0).DockAreaPane.Panes("Map").DockedState)

        'Add any initialization after the InitializeComponent() call
        if (UserSession.bEXE)
        {
            UTMMain.Tools("mnuOpenAdv").SharedProps.Visible = false;
            'UTMMain.Tools("OpenGame").SharedProps.Visible = False
            UTMMain.Tools("mnuRecentAdventures").SharedProps.Visible = false;
            UTMMain.Tools("About").SharedProps.Visible = false;
            UTMMain.Tools("Debugger").SharedProps.Visible = false;
        }

        if (UserSession.bRequiresRestoreLayout)
        {
            RestoreLayout();
            Application.DoEvents();
            UserSession.bRequiresRestoreLayout = false;
        }

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
    Friend WithEvents _frmRunnerUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmRunnerUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmRunnerUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmRunnerUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea;
    Friend WithEvents _frmRunner_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmRunner_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmRunner_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmRunner_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea;
    Friend WithEvents _frmRunnerAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl;
    Friend WithEvents UTMMain As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager;
    Friend WithEvents UDMRunner As Infragistics.Win.UltraWinDock.UltraDockManager;
    Friend WithEvents StatusBar As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents WindowDockingArea2 As Infragistics.Win.UltraWinDock.WindowDockingArea;
    Friend WithEvents pbxGraphics As clsImage;
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents txtInput As System.Windows.Forms.RichTextBox;
    Friend WithEvents txtOutput As System.Windows.Forms.RichTextBox;
    internal Control MapHolder;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("8028f595-16e1-4599-8f8e-41450dcae519"));
        private Infragistics.Win.UltraWinDock.DockableControlPane DockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("c3dc8aaa-2fb3-48cf-bcb9-a92e299b503c"), new System.Guid("a048d2fa-390f-49be-ac40-0d4facdead52"), 0, new System.Guid("8028f595-16e1-4599-8f8e-41450dcae519"), 0);
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("810d89e0-1a82-4b9b-9221-07fe4dc6f671"));
        private Infragistics.Win.UltraWinDock.DockableControlPane DockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("4e0755a5-595b-49d9-a388-0b8017a1efd7"), new System.Guid("7300ca1b-647a-4abc-8580-a3435aee91f1"), -1, new System.Guid("810d89e0-1a82-4b9b-9221-07fe4dc6f671"), 0);
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane3 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, new System.Guid("7300ca1b-647a-4abc-8580-a3435aee91f1"));
        private Infragistics.Win.UltraWinDock.DockAreaPane DockAreaPane4 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, new System.Guid("a048d2fa-390f-49be-ac40-0d4facdead52"));
        private Infragistics.Win.UltraWinToolbars.UltraToolbar UltraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("mnuRunner");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Macros");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuWindow");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
        private Infragistics.Win.UltraWinToolbars.UltraToolbar UltraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("ToolBar");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenAdv");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomIn");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomOut");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("CentreMapLock", "");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("MapPlayerLock", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenAdv");
        private Infragistics.Win.Appearance Appearance32 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenAdv");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RestartGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveGameAs");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("miStartTranscript");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuRecentAdventures");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("AutoComplete", "");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuFullScreen", "");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Debugger", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Options");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("About");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutAdventure");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuFullScreen", "");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuHideMenu", "");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuWindow");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ShowMap");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ShowGraphics");
        private Infragistics.Win.UltraWinToolbars.MdiWindowListTool MdiWindowListTool1 = new Infragistics.Win.UltraWinToolbars.MdiWindowListTool("MDIWindowListTool1");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuRecentAdventures");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
        private Infragistics.Win.Appearance Appearance12 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Debugger", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ShowMap");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ShowGraphics");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("About");
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("AutoComplete", "");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveGame");
        private Infragistics.Win.Appearance Appearance33 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenGame");
        private Infragistics.Win.Appearance Appearance34 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveGameAs");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("miStartTranscript");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Options");
        private Infragistics.Win.UltraWinToolbars.PopupMenuTool PopupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Macros");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Edit Macros");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Edit Macros");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RestartGame");
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomIn");
        private Infragistics.Win.Appearance Appearance35 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MapZoomOut");
        private Infragistics.Win.Appearance Appearance36 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("MapPlayerLock", "");
        private Infragistics.Win.Appearance Appearance37 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.StateButtonTool StateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("CentreMapLock", "");
        private Infragistics.Win.Appearance Appearance38 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinToolbars.ButtonTool ButtonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutAdventure");
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmRunner));
        Me.pbxGraphics = New ADRIFT.clsImage();
        Me.MapHolder = New System.Windows.Forms.Control();
        Me.UDMRunner = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components);
        Me._frmRunnerUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmRunnerUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmRunnerUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmRunnerUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea();
        Me._frmRunner_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me.UTMMain = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components);
        Me._frmRunner_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me._frmRunner_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me._frmRunner_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
        Me._frmRunnerAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl();
        Me.StatusBar = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.WindowDockingArea2 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.DockableWindow1 = New Infragistics.Win.UltraWinDock.DockableWindow();
        Me.DockableWindow3 = New Infragistics.Win.UltraWinDock.DockableWindow();
        Me.txtInput = New System.Windows.Forms.RichTextBox();
        Me.txtOutput = New System.Windows.Forms.RichTextBox();
        Me.ofd = New System.Windows.Forms.OpenFileDialog();
        Me.pnlTop = New System.Windows.Forms.Panel();
        Me.pnlBottom = New System.Windows.Forms.Panel();
        Me.btnSubmit = New Infragistics.Win.Misc.UltraButton();
        Me.btnMore = New Infragistics.Win.Misc.UltraButton();
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer();
        Me.WindowDockingArea3 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.WindowDockingArea5 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        Me.WindowDockingArea1 = New Infragistics.Win.UltraWinDock.WindowDockingArea();
        (System.ComponentModel.ISupportInitialize)(Me.UDMRunner).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UTMMain).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar).BeginInit();
        Me.WindowDockingArea2.SuspendLayout();
        Me.DockableWindow1.SuspendLayout();
        Me.DockableWindow3.SuspendLayout();
        Me.pnlTop.SuspendLayout();
        Me.pnlBottom.SuspendLayout();
        Me.SplitContainer1.Panel1.SuspendLayout();
        Me.SplitContainer1.Panel2.SuspendLayout();
        Me.SplitContainer1.SuspendLayout();
        Me.WindowDockingArea3.SuspendLayout();
        Me.SuspendLayout();
        '
        'pbxGraphics
        '
        Me.pbxGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.pbxGraphics.BackColour = System.Drawing.Color.Transparent;
        Me.pbxGraphics.Image = null;
        Me.pbxGraphics.Location = New System.Drawing.Point(0, 20);
        Me.pbxGraphics.Name = "pbxGraphics";
        Me.pbxGraphics.Size = New System.Drawing.Size(183, 377);
        Me.pbxGraphics.SizeMode = ADRIFT.clsImage.SizeModeEnum.ActualSizeCentred;
        Me.pbxGraphics.TabIndex = 0;
        Me.pbxGraphics.TabStop = false;
        '
        'MapHolder
        '
        Me.MapHolder.BackColor = System.Drawing.Color.Blue;
        Me.MapHolder.Location = New System.Drawing.Point(0, 20);
        Me.MapHolder.Name = "MapHolder";
        Me.MapHolder.Size = New System.Drawing.Size(183, 169);
        Me.MapHolder.TabIndex = 0;
        '
        'UDMRunner
        '
        DockAreaPane1.DockedBefore = New System.Guid("810d89e0-1a82-4b9b-9221-07fe4dc6f671")
        DockAreaPane1.FloatingLocation = New System.Drawing.Point(565, 357)
        DockableControlPane1.Control = Me.pbxGraphics
        DockableControlPane1.Key = "Graphics"
        DockableControlPane1.OriginalControlBounds = New System.Drawing.Rectangle(80, 136, 152, 104)
        DockableControlPane1.Size = New System.Drawing.Size(180, 153)
        DockableControlPane1.Text = "Graphics"
        DockAreaPane1.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane1})
        DockAreaPane1.Size = New System.Drawing.Size(183, 397)
        DockAreaPane2.DockedBefore = New System.Guid("7300ca1b-647a-4abc-8580-a3435aee91f1")
        DockableControlPane2.Closed = true
        DockableControlPane2.Control = Me.MapHolder
        DockableControlPane2.Key = "Map"
        DockableControlPane2.OriginalControlBounds = New System.Drawing.Rectangle(232, 136, 153, 132)
        DockableControlPane2.Size = New System.Drawing.Size(100, 141)
        DockableControlPane2.Text = "Map"
        DockAreaPane2.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane2})
        DockAreaPane2.Size = New System.Drawing.Size(183, 397)
        DockAreaPane3.DockedBefore = New System.Guid("a048d2fa-390f-49be-ac40-0d4facdead52")
        DockAreaPane3.FloatingLocation = New System.Drawing.Point(563, 339)
        DockAreaPane3.Size = New System.Drawing.Size(95, 395)
        DockAreaPane4.FloatingLocation = New System.Drawing.Point(565, 357)
        DockAreaPane4.Size = New System.Drawing.Size(183, 397)
        Me.UDMRunner.DockAreas.AddRange(New Infragistics.Win.UltraWinDock.DockAreaPane() {DockAreaPane1, DockAreaPane2, DockAreaPane3, DockAreaPane4});
        Me.UDMRunner.DragWindowStyle = Infragistics.Win.UltraWinDock.DragWindowStyle.LayeredWindowWithIndicators;
        Me.UDMRunner.HostControl = Me;
        Me.UDMRunner.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.VisualStudio2005;
        '
        '_frmRunnerUnpinnedTabAreaLeft
        '
        Me._frmRunnerUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
        Me._frmRunnerUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmRunnerUnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 48);
        Me._frmRunnerUnpinnedTabAreaLeft.Name = "_frmRunnerUnpinnedTabAreaLeft";
        Me._frmRunnerUnpinnedTabAreaLeft.Owner = Me.UDMRunner;
        Me._frmRunnerUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 397);
        Me._frmRunnerUnpinnedTabAreaLeft.TabIndex = 5;
        Me._frmRunnerUnpinnedTabAreaLeft.TabStop = false;
        '
        '_frmRunnerUnpinnedTabAreaRight
        '
        Me._frmRunnerUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
        Me._frmRunnerUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmRunnerUnpinnedTabAreaRight.Location = New System.Drawing.Point(627, 48);
        Me._frmRunnerUnpinnedTabAreaRight.Name = "_frmRunnerUnpinnedTabAreaRight";
        Me._frmRunnerUnpinnedTabAreaRight.Owner = Me.UDMRunner;
        Me._frmRunnerUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 397);
        Me._frmRunnerUnpinnedTabAreaRight.TabIndex = 6;
        Me._frmRunnerUnpinnedTabAreaRight.TabStop = false;
        '
        '_frmRunnerUnpinnedTabAreaTop
        '
        Me._frmRunnerUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
        Me._frmRunnerUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmRunnerUnpinnedTabAreaTop.Location = New System.Drawing.Point(0, 48);
        Me._frmRunnerUnpinnedTabAreaTop.Name = "_frmRunnerUnpinnedTabAreaTop";
        Me._frmRunnerUnpinnedTabAreaTop.Owner = Me.UDMRunner;
        Me._frmRunnerUnpinnedTabAreaTop.Size = New System.Drawing.Size(627, 0);
        Me._frmRunnerUnpinnedTabAreaTop.TabIndex = 3;
        Me._frmRunnerUnpinnedTabAreaTop.TabStop = false;
        '
        '_frmRunnerUnpinnedTabAreaBottom
        '
        Me._frmRunnerUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
        Me._frmRunnerUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmRunnerUnpinnedTabAreaBottom.Location = New System.Drawing.Point(0, 445);
        Me._frmRunnerUnpinnedTabAreaBottom.Name = "_frmRunnerUnpinnedTabAreaBottom";
        Me._frmRunnerUnpinnedTabAreaBottom.Owner = Me.UDMRunner;
        Me._frmRunnerUnpinnedTabAreaBottom.Size = New System.Drawing.Size(627, 0);
        Me._frmRunnerUnpinnedTabAreaBottom.TabIndex = 4;
        Me._frmRunnerUnpinnedTabAreaBottom.TabStop = false;
        '
        '_frmRunner_Toolbars_Dock_Area_Left
        '
        Me._frmRunner_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmRunner_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
        Me._frmRunner_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
        Me._frmRunner_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmRunner_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 48);
        Me._frmRunner_Toolbars_Dock_Area_Left.Name = "_frmRunner_Toolbars_Dock_Area_Left";
        Me._frmRunner_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 397);
        Me._frmRunner_Toolbars_Dock_Area_Left.ToolbarsManager = Me.UTMMain;
        '
        'UTMMain
        '
        Me.UTMMain.DesignerFlags = 1;
        Me.UTMMain.DockWithinContainer = Me;
        Me.UTMMain.DockWithinContainerBaseType = GetType(System.Windows.Forms.Form);
        Me.UTMMain.ShowFullMenusDelay = 500;
        UltraToolbar1.DockedColumn = 0;
        UltraToolbar1.DockedRow = 0;
        UltraToolbar1.FloatingSize = New System.Drawing.Size(232, 44);
        UltraToolbar1.IsMainMenuBar = true;
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {PopupMenuTool1, PopupMenuTool2, PopupMenuTool3, PopupMenuTool13, PopupMenuTool4, PopupMenuTool5});
        UltraToolbar1.Text = "mnuRunner";
        UltraToolbar2.DockedColumn = 0;
        UltraToolbar2.DockedRow = 1;
        UltraToolbar2.FloatingSize = New System.Drawing.Size(107, 50);
        ButtonTool28.InstanceProps.IsFirstInGroup = true;
        UltraToolbar2.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool1, ButtonTool14, ButtonTool6, ButtonTool28, ButtonTool29, StateButtonTool10, StateButtonTool8});
        UltraToolbar2.Text = "ToolBar";
        Me.UTMMain.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1, UltraToolbar2});
        Appearance32.Image = Global.ADRIFT.My.Resources.Resources.imgOpen;
        ButtonTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance32;
        ButtonTool2.SharedPropsInternal.Caption = "Open Adventure...";
        ButtonTool2.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
        PopupMenuTool6.SharedPropsInternal.Caption = "&File";
        PopupMenuTool6.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
        ButtonTool20.InstanceProps.IsFirstInGroup = true;
        ButtonTool18.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool7.InstanceProps.IsFirstInGroup = true;
        ButtonTool4.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool6.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool3, ButtonTool20, ButtonTool24, ButtonTool18, ButtonTool19, ButtonTool16, PopupMenuTool7, ButtonTool4});
        PopupMenuTool8.SharedPropsInternal.Caption = "&Edit";
        StateButtonTool6.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        PopupMenuTool8.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {StateButtonTool6});
        PopupMenuTool9.SharedPropsInternal.Caption = "&View";
        StateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        StateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        PopupMenuTool9.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {StateButtonTool1, StateButtonTool2, ButtonTool22});
        PopupMenuTool10.SharedPropsInternal.Caption = "&Help";
        PopupMenuTool10.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
        PopupMenuTool10.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool12, ButtonTool32});
        StateButtonTool3.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        StateButtonTool3.SharedPropsInternal.Caption = "Full Screen";
        StateButtonTool4.SharedPropsInternal.Caption = "Hide Menu";
        PopupMenuTool11.SharedPropsInternal.Caption = "&Window";
        PopupMenuTool11.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool10, ButtonTool11});
        MdiWindowListTool1.SharedPropsInternal.Caption = "MDIWindowListTool1";
        PopupMenuTool12.SharedPropsInternal.Caption = "Recent Adventures";
        Appearance12.Image = Global.ADRIFT.My.Resources.Resources.imgCancel16;
        ButtonTool5.SharedPropsInternal.AppearancesSmall.Appearance = Appearance12;
        ButtonTool5.SharedPropsInternal.Caption = "E&xit";
        StateButtonTool5.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        StateButtonTool5.SharedPropsInternal.Caption = "Debugger";
        ButtonTool8.SharedPropsInternal.Caption = "Show Map";
        ButtonTool8.SharedPropsInternal.Enabled = false;
        ButtonTool8.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
        ButtonTool9.SharedPropsInternal.Caption = "Show Graphics";
        ButtonTool9.SharedPropsInternal.Enabled = false;
        ButtonTool9.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
        ButtonTool13.SharedPropsInternal.Caption = "About ADRIFT";
        StateButtonTool7.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
        StateButtonTool7.SharedPropsInternal.Caption = "Auto Complete";
        Appearance33.Image = Global.ADRIFT.My.Resources.Resources.imgSave16;
        ButtonTool7.SharedPropsInternal.AppearancesSmall.Appearance = Appearance33;
        ButtonTool7.SharedPropsInternal.Caption = "Save Game";
        ButtonTool7.SharedPropsInternal.Enabled = false;
        ButtonTool7.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
        Appearance34.Image = Global.ADRIFT.My.Resources.Resources.imgFolderBlue16;
        ButtonTool15.SharedPropsInternal.AppearancesSmall.Appearance = Appearance34;
        ButtonTool15.SharedPropsInternal.Caption = "Open Game...";
        ButtonTool15.SharedPropsInternal.Enabled = false;
        ButtonTool15.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
        ButtonTool17.SharedPropsInternal.Caption = "Save Game As...";
        ButtonTool17.SharedPropsInternal.Enabled = false;
        ButtonTool21.SharedPropsInternal.Caption = "Start Transcript...";
        ButtonTool23.SharedPropsInternal.Caption = "Options";
        PopupMenuTool14.SharedPropsInternal.Caption = "&Macros";
        PopupMenuTool14.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
        PopupMenuTool14.SharedPropsInternal.Enabled = false;
        ButtonTool26.InstanceProps.IsFirstInGroup = true;
        PopupMenuTool14.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool26});
        ButtonTool27.SharedPropsInternal.Caption = "Edit Macros";
        ButtonTool25.SharedPropsInternal.Caption = "Restart Game";
        ButtonTool25.SharedPropsInternal.Enabled = false;
        ButtonTool25.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
        Appearance35.Image = Global.ADRIFT.My.Resources.Resources.imgZoomIn16;
        ButtonTool30.SharedPropsInternal.AppearancesSmall.Appearance = Appearance35;
        ButtonTool30.SharedPropsInternal.Caption = "Zoom In";
        Appearance36.Image = Global.ADRIFT.My.Resources.Resources.imgZoomOut16;
        ButtonTool31.SharedPropsInternal.AppearancesSmall.Appearance = Appearance36;
        ButtonTool31.SharedPropsInternal.Caption = "Zoom Out";
        Appearance37.Image = Global.ADRIFT.My.Resources.Resources.imgCentre16;
        StateButtonTool9.SharedPropsInternal.AppearancesSmall.Appearance = Appearance37;
        StateButtonTool9.SharedPropsInternal.Caption = "Player Location lock";
        Appearance38.Image = Global.ADRIFT.My.Resources.Resources.imgCentre16;
        StateButtonTool11.SharedPropsInternal.AppearancesSmall.Appearance = Appearance38;
        StateButtonTool11.SharedPropsInternal.Caption = "Centre Map lock";
        ButtonTool33.SharedPropsInternal.Caption = "About Adventure";
        Me.UTMMain.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool2, PopupMenuTool6, PopupMenuTool8, PopupMenuTool9, PopupMenuTool10, StateButtonTool3, StateButtonTool4, PopupMenuTool11, MdiWindowListTool1, PopupMenuTool12, ButtonTool5, StateButtonTool5, ButtonTool8, ButtonTool9, ButtonTool13, StateButtonTool7, ButtonTool7, ButtonTool15, ButtonTool17, ButtonTool21, ButtonTool23, PopupMenuTool14, ButtonTool27, ButtonTool25, ButtonTool30, ButtonTool31, StateButtonTool9, StateButtonTool11, ButtonTool33});
        '
        '_frmRunner_Toolbars_Dock_Area_Right
        '
        Me._frmRunner_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmRunner_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
        Me._frmRunner_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
        Me._frmRunner_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmRunner_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(627, 48);
        Me._frmRunner_Toolbars_Dock_Area_Right.Name = "_frmRunner_Toolbars_Dock_Area_Right";
        Me._frmRunner_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 397);
        Me._frmRunner_Toolbars_Dock_Area_Right.ToolbarsManager = Me.UTMMain;
        '
        '_frmRunner_Toolbars_Dock_Area_Top
        '
        Me._frmRunner_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmRunner_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
        Me._frmRunner_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
        Me._frmRunner_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmRunner_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0);
        Me._frmRunner_Toolbars_Dock_Area_Top.Name = "_frmRunner_Toolbars_Dock_Area_Top";
        Me._frmRunner_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(627, 48);
        Me._frmRunner_Toolbars_Dock_Area_Top.ToolbarsManager = Me.UTMMain;
        '
        '_frmRunner_Toolbars_Dock_Area_Bottom
        '
        Me._frmRunner_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        Me._frmRunner_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
        Me._frmRunner_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
        Me._frmRunner_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
        Me._frmRunner_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 445);
        Me._frmRunner_Toolbars_Dock_Area_Bottom.Name = "_frmRunner_Toolbars_Dock_Area_Bottom";
        Me._frmRunner_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(627, 0);
        Me._frmRunner_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.UTMMain;
        '
        '_frmRunnerAutoHideControl
        '
        Me._frmRunnerAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me._frmRunnerAutoHideControl.Location = New System.Drawing.Point(0, 0);
        Me._frmRunnerAutoHideControl.Name = "_frmRunnerAutoHideControl";
        Me._frmRunnerAutoHideControl.Owner = Me.UDMRunner;
        Me._frmRunnerAutoHideControl.Size = New System.Drawing.Size(0, 0);
        Me._frmRunnerAutoHideControl.TabIndex = 0;
        Me._frmRunnerAutoHideControl.TabStop = false;
        '
        'StatusBar
        '
        Appearance1.FontData.SizeInPoints = 11.0!;
        Me.StatusBar.Appearance = Appearance1;
        Me.StatusBar.Location = New System.Drawing.Point(0, 445);
        Me.StatusBar.Name = "StatusBar";
        UltraStatusPanel1.Key = "Description";
        UltraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel1.Text = "Please open an adventure file";
        Appearance2.TextHAlignAsString = "Center";
        UltraStatusPanel2.Appearance = Appearance2;
        UltraStatusPanel2.Key = "UserStatus";
        UltraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
        UltraStatusPanel3.Key = "Score";
        UltraStatusPanel3.Text = "Score: 0";
        UltraStatusPanel3.Visible = false;
        Me.StatusBar.Panels.AddRange(New Infragistics.Win.UltraWinStatusBar.UltraStatusPanel() {UltraStatusPanel1, UltraStatusPanel2, UltraStatusPanel3});
        Me.StatusBar.Size = New System.Drawing.Size(627, 23);
        Me.StatusBar.TabIndex = 11;
        Me.StatusBar.Tag = "";
        '
        'WindowDockingArea2
        '
        Me.WindowDockingArea2.Controls.Add(Me.DockableWindow1);
        Me.WindowDockingArea2.Dock = System.Windows.Forms.DockStyle.Right;
        Me.WindowDockingArea2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea2.Location = New System.Drawing.Point(439, 48);
        Me.WindowDockingArea2.Name = "WindowDockingArea2";
        Me.WindowDockingArea2.Owner = Me.UDMRunner;
        Me.WindowDockingArea2.Size = New System.Drawing.Size(188, 397);
        Me.WindowDockingArea2.TabIndex = 2;
        Me.WindowDockingArea2.TabStop = false;
        '
        'DockableWindow1
        '
        Me.DockableWindow1.Controls.Add(Me.pbxGraphics);
        Me.DockableWindow1.Location = New System.Drawing.Point(5, 0);
        Me.DockableWindow1.Name = "DockableWindow1";
        Me.DockableWindow1.Owner = Me.UDMRunner;
        Me.DockableWindow1.Size = New System.Drawing.Size(183, 397);
        Me.DockableWindow1.TabIndex = 12;
        Me.DockableWindow1.TabStop = false;
        '
        'DockableWindow3
        '
        Me.DockableWindow3.Controls.Add(Me.MapHolder);
        Me.DockableWindow3.Location = New System.Drawing.Point(5, 0);
        Me.DockableWindow3.Name = "DockableWindow3";
        Me.DockableWindow3.Owner = Me.UDMRunner;
        Me.DockableWindow3.Size = New System.Drawing.Size(183, 397);
        Me.DockableWindow3.TabIndex = 13;
        '
        'txtInput
        '
        Me.txtInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtInput.BackColor = System.Drawing.Color.Black;
        Me.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.txtInput.ForeColor = System.Drawing.SystemColors.HighlightText;
        Me.txtInput.Location = New System.Drawing.Point(48, 0);
        Me.txtInput.Multiline = false;
        Me.txtInput.Name = "txtInput";
        Me.txtInput.ReadOnly = true;
        Me.txtInput.Size = New System.Drawing.Size(387, 20);
        Me.txtInput.TabIndex = 1;
        Me.txtInput.Text = "";
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtOutput.BackColor = System.Drawing.Color.Black;
        Me.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.txtOutput.Cursor = System.Windows.Forms.Cursors.Default;
        Me.txtOutput.ForeColor = System.Drawing.SystemColors.HighlightText;
        Me.txtOutput.Location = New System.Drawing.Point(48, 0);
        Me.txtOutput.Margin = New System.Windows.Forms.Padding(0);
        Me.txtOutput.Name = "txtOutput";
        Me.txtOutput.ReadOnly = true;
        Me.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
        Me.txtOutput.Size = New System.Drawing.Size(388, 367);
        Me.txtOutput.TabIndex = 0;
        Me.txtOutput.TabStop = false;
        Me.txtOutput.Text = "";
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Black;
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.pnlTop.Controls.Add(Me.txtOutput);
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.pnlTop.Location = New System.Drawing.Point(0, 0);
        Me.pnlTop.Name = "pnlTop";
        Me.pnlTop.Size = New System.Drawing.Size(439, 371);
        Me.pnlTop.TabIndex = 0;
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.Black;
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.pnlBottom.Controls.Add(Me.btnSubmit);
        Me.pnlBottom.Controls.Add(Me.btnMore);
        Me.pnlBottom.Controls.Add(Me.txtInput);
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0);
        Me.pnlBottom.Name = "pnlBottom";
        Me.pnlBottom.Size = New System.Drawing.Size(439, 25);
        Me.pnlBottom.TabIndex = 0;
        '
        'btnSubmit
        '
        Me.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
        Me.btnSubmit.Location = New System.Drawing.Point(386, 0);
        Me.btnSubmit.Name = "btnSubmit";
        Me.btnSubmit.Size = New System.Drawing.Size(49, 21);
        Me.btnSubmit.TabIndex = 13;
        Me.btnSubmit.Text = "Submit";
        Me.btnSubmit.Visible = false;
        '
        'btnMore
        '
        Me.btnMore.Location = New System.Drawing.Point(131, -2);
        Me.btnMore.Name = "btnMore";
        Me.btnMore.Size = New System.Drawing.Size(307, 24);
        Me.btnMore.TabIndex = 12;
        Me.btnMore.Text = "Press any key to continue";
        Me.btnMore.Visible = false;
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 48);
        Me.SplitContainer1.Name = "SplitContainer1";
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlTop);
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlBottom);
        Me.SplitContainer1.Size = New System.Drawing.Size(439, 397);
        Me.SplitContainer1.SplitterDistance = 371;
        Me.SplitContainer1.SplitterWidth = 1;
        Me.SplitContainer1.TabIndex = 1;
        Me.SplitContainer1.TabStop = false;
        '
        'WindowDockingArea3
        '
        Me.WindowDockingArea3.Controls.Add(Me.DockableWindow3);
        Me.WindowDockingArea3.Dock = System.Windows.Forms.DockStyle.Right;
        Me.WindowDockingArea3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea3.Location = New System.Drawing.Point(4, 4);
        Me.WindowDockingArea3.Name = "WindowDockingArea3";
        Me.WindowDockingArea3.Owner = Me.UDMRunner;
        Me.WindowDockingArea3.Size = New System.Drawing.Size(188, 397);
        Me.WindowDockingArea3.TabIndex = 0;
        Me.WindowDockingArea3.TabStop = false;
        '
        'WindowDockingArea5
        '
        Me.WindowDockingArea5.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.WindowDockingArea5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea5.Location = New System.Drawing.Point(8, 8);
        Me.WindowDockingArea5.Name = "WindowDockingArea5";
        Me.WindowDockingArea5.Owner = Me.UDMRunner;
        Me.WindowDockingArea5.Size = New System.Drawing.Size(95, 395);
        Me.WindowDockingArea5.TabIndex = 0;
        '
        'WindowDockingArea1
        '
        Me.WindowDockingArea1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.WindowDockingArea1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.WindowDockingArea1.Location = New System.Drawing.Point(439, 48);
        Me.WindowDockingArea1.Name = "WindowDockingArea1";
        Me.WindowDockingArea1.Owner = Me.UDMRunner;
        Me.WindowDockingArea1.Size = New System.Drawing.Size(183, 397);
        Me.WindowDockingArea1.TabIndex = 13;
        '
        'frmRunner
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.ClientSize = New System.Drawing.Size(627, 468);
        Me.Controls.Add(Me._frmRunnerAutoHideControl);
        Me.Controls.Add(Me.SplitContainer1);
        Me.Controls.Add(Me.WindowDockingArea3);
        Me.Controls.Add(Me.WindowDockingArea2);
        Me.Controls.Add(Me._frmRunnerUnpinnedTabAreaBottom);
        Me.Controls.Add(Me._frmRunnerUnpinnedTabAreaTop);
        Me.Controls.Add(Me._frmRunnerUnpinnedTabAreaRight);
        Me.Controls.Add(Me._frmRunnerUnpinnedTabAreaLeft);
        Me.Controls.Add(Me._frmRunner_Toolbars_Dock_Area_Left);
        Me.Controls.Add(Me._frmRunner_Toolbars_Dock_Area_Right);
        Me.Controls.Add(Me._frmRunner_Toolbars_Dock_Area_Top);
        Me.Controls.Add(Me._frmRunner_Toolbars_Dock_Area_Bottom);
        Me.Controls.Add(Me.StatusBar);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.Name = "frmRunner";
        Me.Text = "ADRIFT Runner";
        (System.ComponentModel.ISupportInitialize)(Me.UDMRunner).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UTMMain).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar).EndInit();
        Me.WindowDockingArea2.ResumeLayout(false);
        Me.DockableWindow1.ResumeLayout(false);
        Me.DockableWindow3.ResumeLayout(false);
        Me.pnlTop.ResumeLayout(false);
        Me.pnlBottom.ResumeLayout(false);
        Me.SplitContainer1.Panel1.ResumeLayout(false);
        Me.SplitContainer1.Panel2.ResumeLayout(false);
        Me.SplitContainer1.ResumeLayout(false);
        Me.WindowDockingArea3.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private void FullScreen(bool bActivate)
    {
        if (bActivate)
        {
            'Hide()
            txtOutput.Visible = false;
            pnlTop.BorderStyle = BorderStyle.None;
            pnlBottom.BorderStyle = BorderStyle.None;
            'Application.DoEvents()

            Me.Text = "";
            Me.ControlBox = false;
            WindowState = FormWindowState.Maximized
            txtOutput.Visible = true;
            'Me.txtOutput.BorderStyle = BorderStyle.None
            'Me.txtInput.BorderStyle = BorderStyle.None
            'Show()
        Else
            WindowState = FormWindowState.Normal
            Me.ControlBox = true;
            Me.Text = "ADRIFT Runner";
            'Hide()

            pnlTop.BorderStyle = BorderStyle.Fixed3D;
            pnlBottom.BorderStyle = BorderStyle.Fixed3D;
            'Show()
            'Me.txtOutput.BorderStyle = BorderStyle.Fixed3D
            'Me.txtInput.BorderStyle = BorderStyle.Fixed3D
        }
        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("mnuFullScreen")).Checked = bActivate;
        SaveSetting("ADRIFT", "Runner", "FullScreen", CInt(bActivate).ToString);
        'Application.DoEvents()
    }


    public void UpdateStatusBar(string sDescription, string sScore, string sUserStatus)
    {
        try
        {
            if (Not StatusBar.IsDisposed)
            {
                With StatusBar;
                    .Panels("Description").Text = sDescription;
                    if (sScore <> "")
                    {
                        .Panels("Score").Text = sScore;
                        .Panels("Score").Visible = true;
                    Else
                        .Panels("Score").Visible = false;
                    }
                    if (sUserStatus <> "")
                    {
                        .Panels("UserStatus").Text = ReplaceALRs(sUserStatus);
                        .Panels("UserStatus").Visible = true;
                        .Panels("Description").SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
                    Else
                        .Panels("UserStatus").Visible = false;
                        .Panels("Description").SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
                    }
                }
            }
        Catch
        }
    }


    private void UTMMain_ToolClick(System.Object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
        DoToolClick(e.Tool.Key, e.Tool.SharedProps.Caption, CStr(e.Tool.SharedProps.Tag))
    }

    private void DoToolClick(string sKey, string sCaption, string sTag)
    {

        try
        {
            if (sTag = "_RECENT_")
            {
                UserSession.OpenAdventure(sKey);
            }

            switch (sKey)
            {
                case "About":
                    {
                    private New AboutBox fAbout;
                    try
                    {
                        fAbout.ShowDialog();
                    Catch
                    }
                    'MessageBox.Show("ADRIFT Runner" & vbCrLf & "Version " & Application.ProductVersion & vbCrLf & "© Campbell Wild 2010" & vbCrLf & vbCrLf & "Alpha Release.  Registered users only." & vbCrLf & "Splash image ""Adrift"" © V. Milovic 2010 (www.brokentoyland.com)", "About ADRIFT Runner", MessageBoxButtons.OK, MessageBoxIcon.Information)
                case "AboutAdventure":
                    {
                    private string sVersion = "";
                    if (Adventure IsNot null)
                    {
                        if (Adventure.BabelTreatyInfo IsNot null)
                        {
                            if (Adventure.BabelTreatyInfo.Stories.Length = 1 && Adventure.BabelTreatyInfo.Stories(0).Releases IsNot null)
                            {
                                With Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release;
                                    sVersion = vbCrLf & "Version: " & .Version
                                    If .ReleaseDate > Date.MinValue Then sVersion &= vbCrLf + "Released: " + .ReleaseDate.ToLongDateString
                                }
                            }
                        }
                        MessageBox.Show(Adventure.Title + vbCrLf + "By " + Adventure.Author + sVersion, "About Adventure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                case "AutoComplete":
                    {
                    'CType(UTMMain.Tools("AutoComplete"), Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked = Not CType(UTMMain.Tools("AutoComplete"), Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked
                    UserSession.bAutoComplete = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("AutoComplete")).Checked;
                    SaveSetting("ADRIFT", "Generator", "Auto Complete", CInt(UserSession.bAutoComplete).ToString);
                case "Debugger":
                    {
                    If UserSession.Debugger == null || UserSession.Debugger.IsDisposed Then UserSession.Debugger = New frmDebugger
                    UserSession.Debugger.Visible = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("Debugger")).Checked;
                case "Exit":
                    {
                    UserSession.Quit();
                case "mnuFullScreen":
                    {
                    FullScreen((Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("mnuFullScreen")).Checked);
                case "mnuOpenAdv":
                    {
                    OpenAdventureDialog(fRunner.ofd);
                case "RestartGame":
                    {
                    UserSession.Restart();
                case "OpenGame":
                    {
                    UserSession.Restore();
                    UserSession.Display(vbCrLf + vbCrLf, true);
                case "SaveGame":
                case "SaveGameAs":
                    {
                    UserSession.SaveGame(sKey = "SaveGameAs");
                    UserSession.Display(vbCrLf + vbCrLf, true);
                case "ShowGraphics":
                    {
                    For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
                        if (cp.Text = "Graphics" && Not cp.IsVisible)
                        {
                            cp.Show();
                            UTMMain.Tools("ShowGraphics").SharedProps.Enabled = false;
                            SetMargins();
                        }
                    Next;
                case "ShowMap":
                    {
                    For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
                        if (cp.Text = "Map" && Not cp.IsVisible)
                        {
                            cp.Show();
                            UTMMain.Tools("ShowMap").SharedProps.Enabled = false;
                            SetMargins();
                        }
                    Next;
                    'If Not UDMRunner.ControlPanes.Exists("") Then Exit Sub
                    'UDM.ControlPanes(sPane).Closed = False ' We're getting an Infragistics Stack overflow bug whenever we try to reopen a pane that was restored as closed...
                    'UDM.ControlPanes(sPane).Activate()
                    'CType(sender, ToolStripMenuItem).Visible = False
                    'If Not arlOpenPanes.Contains(sPane) Then arlOpenPanes.Add(sPane)
                case "miStartTranscript":
                    {
                    if (UTMMain.Tools("miStartTranscript").SharedProps.Caption = "Start Transcript...")
                    {
                        StartTranscript();
                    Else
                        StopTranscript();
                    }

                case "Options":
                    {
                    private New frmOptionsRunner frmOptions;
                    frmOptions.ShowDialog();

                case "Edit Macros":
                    {
                    private New frmMacros frmMacros;
                    'frmMacros.dictMacros = CType(dictMacros.Clon, StringHashTable)
                    frmMacros.ldictMacros = New Generic.Dictionary(Of String, clsMacro);
                    For Each macro As clsMacro In UserSession.dictMacros.Values
                        frmMacros.ldictMacros.Add(macro.Key, (clsMacro)(macro.Clone));
                    Next;
                    frmMacros.Text = "Edit Macros - " + Adventure.Title;
                    frmMacros.Show();

                case "MapPlan":
                    {
                    UserSession.Map.PlanView();

                case "MapCentre":
                    {
                    UserSession.Map.CentreMap();

                case "MapZoomIn":
                    {
                    UserSession.Map.Zoom(true);

                case "MapZoomOut":
                    {
                    UserSession.Map.Zoom(false);

                case "CentreMapLock":
                    {
                    UserSession.Map.LockMapCentre = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools(sKey)).Checked;

                case "MapPlayerLock":
                    {
                    UserSession.Map.LockPlayerCentre = (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools(sKey)).Checked;

                default:
                    {
                    'ErrMsg("Tool " & sKey & " not yet programmed!")

            }

            if (sLeft(sKey, 6) = "Macro-")
            {
                RunMacro(sKey.Replace("Macro-", ""));
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Tool Click error", ex);
        }

    }


    private bool bStopMacro = false;
    private void RunMacro(string sKey)
    {

        bStopMacro = False

        if (UserSession.dictMacros.ContainsKey(sKey))
        {
            if (UserSession.dictMacros(sKey).Commands IsNot null)
            {
                private bool bAutoComplete = UserSession.bAutoComplete;
                UserSession.bAutoComplete = false;
                For Each sCommand As String In UserSession.dictMacros(sKey).Commands.Split(Chr(10))
                    sCommand = ReplaceFunctions(sCommand)
                    if (sCommand.Trim <> "")
                    {
                        if (sLeft(sCommand, 1) = "#" && sCommand.Length > 1)
                        {
                            txtInput_KeyDown(null, New KeyEventArgs(Keys.OemQuotes));
                            txtInput.Text = "@ " + sCommand.Substring(1).Trim;
                        Else
                            txtInput.Text = "xx" + sCommand.Trim;
                        }

                        txtInput_KeyDown(null, New KeyEventArgs(Keys.Enter));
                        Application.DoEvents();
                        If bStopMacro Then Exit Sub
                    }
                Next;
                UserSession.bAutoComplete = bAutoComplete;
            }
        }

    }


    '' Load Macros from file
    'Public Sub LoadMacros()

    '    Try
    '        Dim sMacrosFile As String = sLocalDataPath & "ADRIFTMacros.xml"
    '        dictMacros.Clear()

    '        If IO.File.Exists(sMacrosFile) Then
    '            Dim xmlDoc As New System.Xml.XmlDocument
    '            xmlDoc.Load(sMacrosFile)
    '            Dim bFirst As Boolean = True

    '            For Each nod As System.Xml.XmlNode In xmlDoc.SelectNodes("/Macros/Macro")

    '                Dim sTitle As String = nod.SelectSingleNode("Title").InnerText
    '                Dim sCommands As String = nod.SelectSingleNode("Commands").InnerText
    '                Dim sKey As String = nod.SelectSingleNode("Key").InnerText

    '                If sKey <> "" AndAlso sTitle <> "" AndAlso sCommands <> "" Then
    '                    Dim macro As New clsMacro(sKey)
    '                    macro.Title = sTitle
    '                    macro.Commands = sCommands

    '                    If nod.SelectSingleNode("Shared") IsNot Nothing Then macro.Shared = SafeBool(nod.SelectSingleNode("Shared").InnerText)
    '                    If nod.SelectSingleNode("IFID") IsNot Nothing Then macro.IFID = SafeString(nod.SelectSingleNode("IFID").InnerText)
    '                    If nod.SelectSingleNode("Shortcut") IsNot Nothing Then macro.Shortcut = CType([Enum].Parse(GetType(Shortcut), nod.SelectSingleNode("Shortcut").InnerText), Shortcut)

    '                    dictMacros.Add(macro.Key, macro)
    '                End If
    '            Next
    '        End If

    '        ReloadMacros()

    '    Catch ex As Exception
    '        ErrMsg("Error loading macros", ex)
    '    End Try

    'End Sub


    'Public Sub SaveMacros()

    '    Try
    '        Dim sMacrosFile As String = sLocalDataPath & "ADRIFTMacros.xml"

    '        Dim xmlWriter As New System.Xml.XmlTextWriter(sMacrosFile, System.Text.Encoding.UTF8)
    '        With xmlWriter
    '            .Indentation = 4
    '            .IndentChar = " "c
    '            .Formatting = Xml.Formatting.Indented

    '            .WriteStartDocument()
    '            .WriteComment("File created by ADRIFT v" & Application.ProductVersion)

    '            .WriteStartElement("Macros")

    '            For Each sTitle As String In dictMacros.Keys
    '                .WriteStartElement("Macro")

    '                Dim m As clsMacro = dictMacros(sTitle)

    '                .WriteElementString("Key", m.Key)
    '                .WriteElementString("Title", m.Title)
    '                .WriteElementString("Shared", m.Shared.ToString)
    '                .WriteElementString("IFID", m.IFID)
    '                If m.Shortcut <> Keys.None Then .WriteElementString("Shortcut", m.Shortcut.ToString)
    '                .WriteElementString("Commands", m.Commands)

    '                .WriteEndElement() ' Macro
    '            Next

    '            .WriteEndElement() ' Macros

    '            .WriteEndDocument()

    '            .Flush()
    '            .Close()

    '        End With
    '    Catch ex As Exception
    '        ErrMsg("Error saving macros", ex)
    '    End Try

    'End Sub


    ' Reload the macros on the form
    public void ReloadMacros()
    {

        private PopupMenuTool mnuMacros = CType(UTMMain.Tools("Macros"), PopupMenuTool);

        for (int iTool = mnuMacros.Tools.Count - 1; iTool <= 0; iTool += -1)
        {
            If mnuMacros.Tools(iTool).Key <> "Edit Macros" Then mnuMacros.Tools.RemoveAt(iTool)
        Next;

        'Dim arlKeys As New StringArrayList
        'For Each sTitle As String In dictMacros.Keys
        '    arlKeys.Add(sTitle)
        'Next
        'arlKeys.Sort()

        private int i;
        For Each macro As clsMacro In UserSession.dictMacros.Values
            i += 1;
            With macro;
                if (Adventure IsNot null && .IFID = Adventure.BabelTreatyInfo.Stories(0).Identification.IFID || .Shared)
                {
                    private string sToolKey = "Macro-" + .Key;
                    if (Not UTMMain.Tools.Exists(sToolKey))
                    {
                        private New ButtonTool(sToolKey) newTool;
                        newTool.SharedProps.Caption = .Title;
                        UTMMain.Tools.Add(newTool);
                    }

                    private ButtonTool newinstance = CType(mnuMacros.Tools.InsertTool(mnuMacros.Tools.Count - 1, sToolKey), ButtonTool);
                    if (.Shortcut <> Keys.None)
                    {
                        UTMMain.Tools(sToolKey).SharedProps.Shortcut = .Shortcut;
                    }
                }
            }
        Next;

    }



    private bool bLocked = false;
    public bool Locked { get; set; }
        {
            get
            {
            return bLocked;
        }
set(ByVal Boolean)
            bLocked = value
            'txtInput.Enabled = Not bLocked
            UTMMain.Enabled = ! bLocked;
        }
    }


    private bool bWaitKey = false;
    public void WaitKey()
    {
        bWaitKey = True
        Locked = True
        while (bWaitKey && Visible)
        {
            Application.DoEvents();
            Threading.Thread.Sleep(5);
        }
        Locked = False
        If fRunner.txtOutput != null && ! fRunner.txtOutput.IsDisposed Then UserSession.iPreviousOffset = fRunner.txtOutput.TextLength
    }


    private bool bLoadedMargins = false;
    public void SetMargins()
    {

        With UserSession;
            if (Not bLoadedMargins)
            {
                .iMarginWidth = CInt(GetSetting("ADRIFT", "Runner", "Margin", "10"));
                bLoadedMargins = True
            }
            txtOutput.Width = pnlTop.ClientSize.Width - .iMarginWidth '+ 10;
            txtOutput.Left = .iMarginWidth;
            txtOutput.RightMargin = Math.Max(txtOutput.ClientSize.Width - .iMarginWidth, 0);
            txtInput.Width = pnlBottom.ClientSize.Width - .iMarginWidth ' + 10;
            txtInput.Left = .iMarginWidth;
            'txtInput.Top = -2
        }

    }


    private void StartTranscript()
    {

        With UserSession;
            private New Windows.Forms.SaveFileDialog sfd;
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            If .sTranscriptFile = "" Then .sTranscriptFile = "ADRIFT_1.txt"
            private int i = 1;
            while (IO.File.Exists(.sTranscriptFile))
            {
                .sTranscriptFile = "ADRIFT_" + i + ".txt";
            }
            sfd.FileName = .sTranscriptFile;
            sfd.DefaultExt = "txt";
            sfd.OverwritePrompt = false;
            If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("txt") Then sfd.FileName = ""
            if (sfd.ShowDialog() = Windows.Forms.DialogResult.OK)
            {
                UTMMain.Tools("miStartTranscript").SharedProps.Caption = "Stop Transcript";
                .sTranscriptFile = sfd.FileName;
            }
        }

    }


    private void StopTranscript()
    {
        MessageBox.Show("Saving Transcript stopped." + vbCrLf + vbCrLf + "The file was saved as """ + UserSession.sTranscriptFile + """.", "ADRIFT Runner", MessageBoxButtons.OK, MessageBoxIcon.Information);
        UTMMain.Tools("miStartTranscript").SharedProps.Caption = "Start Transcript...";
    }


    private void txtInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        try
        {
            With UserSession;
                bIgnore = False

                if (Locked)
                {
                    e.Handled = true;
                    Exit Sub;
                }

                if (.UserSplash IsNot null && .UserSplash.Visible)
                {
                    e.Handled = true;
                    .UserSplash.Hide();
                    Exit Sub;
                }

                If iCommand > .salCommands.Count - 1 Then iCommand = .salCommands.Count - 1

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        bIgnore = True
                        'If txtInput.Text.Length > 2 AndAlso .salCommands.Count > 0 Then
                        '    .salCommands.Add("")
                        '    iCommand = .salCommands.Count - 1
                        '    .salCommands(iCommand - 1) = txtInput.Text.Substring(2)
                        '    .ClearAutoCompletes()
                        '    Adventure.Turns += 1
                        '    .Process(txtInput.Text.Substring(2).Trim)
                        'End If
                        SubmitCommand();
                    case Keys.Up:
                        {
                        if (iCommand > 0)
                        {
                            iCommand -= 1;
                            '.InitialiseInputBox()
                            'txtInput.SelectedText = .salCommands(iCommand)
                            'txtInput.SelectionStart = txtInput.Text.Length
                            SetInput(.salCommands(iCommand));
                        }
                        e.Handled = true;
                    case Keys.Down:
                        {
                        if (iCommand < .salCommands.Count - 1)
                        {
                            iCommand += 1;
                            '.InitialiseInputBox()
                            'txtInput.SelectedText = .salCommands(iCommand)
                            'txtInput.SelectionStart = txtInput.Text.Length
                            SetInput(.salCommands(iCommand));
                        }
                        e.Handled = true;
                    case Keys.Left:
                        {
                        If txtInput.SelectionStart <= 2 Then e.Handled = true
                    case Keys.Back:
                        {
                        If txtInput.SelectionStart <= 2 Then e.Handled = true
                        ' Otherwise, let it flow...
                    case Keys.OemQuotes:
                        {
                        if (txtInput.SelectionStart = 2)
                        {
                            .InitialiseInputBox("@");
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                        }
                    default:
                        {
                        Application.DoEvents();
                        if (iCommand > -1 && .salCommands.Count > iCommand Then ' .salCommands.Count > 0 && iCommand > -1)
                        {
                            .salCommands(iCommand) = txtInput.Text.Substring(2);
                        }

                        If ! .bAutoComplete || (Adventure != null && Adventure.eGameState <> clsAction.EndGameEnum.Running) || (txtInput.Tag != null && txtInput.Tag.ToString = "Comment") Then Exit Sub
                        If txtInput.Text.Length < 3 Then Exit Sub

                        .DoAutoComplete();
                }
            }

        }
        catch (Exception ex)
        {
            ErrMsg("KeyDown error", ex);
        }

    }


    public void SetInput(string sText)
    {
        UserSession.InitialiseInputBox();
        txtInput.SelectedText = sText;
        txtInput.SelectionStart = txtInput.Text.Length;
    }


    public void SubmitCommand()
    {

        With UserSession;
            if (txtInput.Text.Length > 2 && .salCommands.Count > 0)
            {
                btnSubmit.Enabled = false;
                .salCommands.Add("");
                iCommand = .salCommands.Count - 1
                .salCommands(iCommand - 1) = txtInput.Text.Substring(2);
                .ClearAutoCompletes();
                Adventure.Turns += 1;
                private string sInput = txtInput.Text.Substring(2).Trim;
                sInput = StripCarats(sInput)
                .Process(sInput);
                btnSubmit.Visible = false;
                btnSubmit.Enabled = true;
            }
        }

    }


    private void AddPrevious()
    {

        private string sFilename;

        for (int iPrevious = 1; iPrevious <= 10; iPrevious++)
        {
            sFilename = GetSetting("ADRIFT", "Runner", "Recent_" & iPrevious, "")
            if (sFilename <> "")
            {
                AddTool(UTMMain, "mnuRecentAdventures", sFilename, "&" + iPrevious + "  " + sFilename, "_RECENT_");
            }
        Next;

    }

    public void AddTool(ref UTMTarget As UltraToolbarsManager, string sTargetTool, string sKey, string sCaption, string sTag = "", bool bFirstInGroup = false)
    {

        try
        {
            private New ButtonTool(sKey) newTool;
            if (Not UTMTarget.Tools.Exists(sKey))
            {
                newTool.SharedProps.Caption = sCaption;
                newTool.SharedProps.Tag = sTag;
                UTMTarget.Tools.Add(newTool);

                private ButtonTool newinstance = CType(CType(UTMTarget.Tools(sTargetTool), PopupMenuTool).Tools.AddTool(sKey), ButtonTool);
                newinstance.InstanceProps.IsFirstInGroup = bFirstInGroup;
            }
        }
        catch (Exception ex)
        {
            ' Probably a duplicate key
            ErrMsg("AddTool error", ex);
        }

    }


    private void frmRunner_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyData = Keys.Escape)
        {
            bStopMacro = True
        ElseIf e.KeyData = (Keys.Control | Keys.M) Then
            For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
                if (cp.Text = "Map" && cp.IsVisible)
                {
                    cp.Close();
                    UTMMain.Tools("ShowMap").SharedProps.Enabled = true;
                    SetMargins();
                }
            Next;
        }
    }


    internal void SetBackgroundColour(Color colBackground = null)
    {
        If colBackground = null Then colBackground = GetBackgroundColour()
        fRunner.txtOutput.BackColor = colBackground;
        fRunner.pnlTop.BackColor = colBackground;
        fRunner.txtInput.BackColor = colBackground;
        fRunner.pnlBottom.BackColor = colBackground;
    }

    internal void SetInputColour(Color colInput = null)
    {
        If colInput = null Then colInput = GetInputColour()
        fRunner.txtInput.ForeColor = colInput;
    }



    private void frmRunner_Load(object sender, System.EventArgs e)
    {
        fRunner = Me
        fRunner.Visible = false;
        SetBackgroundColour();
        SetInputColour();
        '        colInput = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text1", ColorTranslator.ToOle(Color.FromArgb(210, 37, 39)).ToString)))
        '       colOutput = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text2", ColorTranslator.ToOle(Color.FromArgb(25, 165, 138)).ToString)))
        '      colLink = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "LinkColour", ColorTranslator.ToOle(Color.FromArgb(75, 215, 188)).ToString)))

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

        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("AutoComplete")).Checked = SafeBool(GetSetting("ADRIFT", "Generator", "Auto Complete", "-1"));
        RestoreLayout();
        UserSession.Map.LockMapCentre = SafeBool(GetSetting("ADRIFT", "Runner", "CentreMapLock", "0"));
        UserSession.Map.LockPlayerCentre = SafeBool(GetSetting("ADRIFT", "Runner", "MapPlayerLock", "0"));
        AddPrevious();
        UserSession.LoadMacros();
        FullScreen(CBool(GetSetting("ADRIFT", "Runner", "FullScreen", "0")));
        GetFormPosition(Me, UTMMain, UDMRunner);
        fRunner.Visible = true;
        UserSession.RunnerStartup();
    }

    private void frmRunner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        With UserSession;
            .bQuitting = true;
            if (.Quit())
            {
                SaveFormPosition(Me);
                SaveLayout();
                TidyUp();
            Else
                e.Cancel = true;
                .bQuitting = false;
            }
        }
    }

    internal void txtOutput_GotFocus(object sender, System.EventArgs e)
    {
        txtInput.Focus();
    }

    private bool bIgnore = false;
    private void txtInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {

        if (bIgnore)
        {
            bIgnore = False
            Exit Sub ' Avoid an oddity where KeyPress is firing as well as KeyDown.Enter;
        }

        if (bWaitKey && Not btnMore.Visible)
        {
            Adventure.sReferencedText(0) = e.KeyChar;
            e.Handled = true;
            bWaitKey = False
            Exit Sub;
        }

        if (btnMore.Visible)
        {
            btnMore_Click(null, null);
            e.Handled = true;
            Exit Sub;
        }

        switch (e.KeyChar)
        {
            case " "c:
            case "'"c ' keys when pressed skip the auto-complete highlighted word:
                {
                If txtInput.SelectionLength > 0 Then txtInput.SelectionStart = txtInput.Text.Length
        }

    }


    private void txtInput_SelectionChanged(object sender, System.EventArgs e)
    {
        If txtInput.SelectionStart < 2 Then txtInput.SelectionStart = 2
    }


    private void txtInput_TextChanged(System.Object sender, System.EventArgs e)
    {
        UserSession.BuildAutos();
    }






    'Private Function ExpandBlockOld(ByVal sBlock As String) As StringArrayList

    '    Dim sal As New StringArrayList

    '    sal.Add("")

    '    For i As Integer = 0 To sBlock.Length - 1

    '        Dim cNextChar As Char = sBlock.Substring(i, 1).ToCharArray()(0)

    '        Select Case cNextChar
    '            Case "["c, "{"c

    '                ' Get the whole block
    '                Dim cBracket As Char = sBlock.Substring(i).ToCharArray()(0)
    '                Dim iLevel As Integer = 1
    '                Dim sNewBlock As String = ""
    '                'Dim n As Integer = i + 1
    '                Dim bLastOptional As Boolean
    '                While iLevel > 0
    '                    i += 1
    '                    Dim cNewChar As Char = sBlock.Substring(i, 1).ToCharArray()(0)
    '                    bLastOptional = False
    '                    Select Case cNewChar
    '                        Case "["c, "{"c
    '                            iLevel += 1
    '                        Case "]"c, "}"c
    '                            iLevel -= 1
    '                            bLastOptional = (cNewChar = "}"c)
    '                        Case Else
    '                    End Select
    '                    If iLevel > 0 Then sNewBlock &= cNewChar
    '                    'n += 1
    '                End While
    '                'i += n - 1

    '                Dim sal2 As New StringArrayList
    '                'If InStr(sNewBlock, "[") > 0 OrElse InStr(sNewBlock, "{") > 0 Then
    '                '    For Each s As String In sal
    '                '        If bLastOptional Then sal2.Add(s)
    '                '        For Each sNew As String In ExpandBlock(sNewBlock)
    '                '            sal2.Add(s & sNew)
    '                '        Next
    '                '    Next
    '                'Else
    '                '    For Each s As String In sal
    '                '        For Each sNew As String In sNewBlock.Split("/"c)
    '                '            If bLastOptional Then sal2.Add(s)
    '                '            sal2.Add(s & sNew)
    '                '        Next
    '                '    Next
    '                'End If

    '                For Each s As String In sal
    '                    For Each sNew As String In sNewBlock.Split("/"c)
    '                        If bLastOptional Then sal2.Add(s)
    '                        If InStr(sNew, "[") > 0 OrElse InStr(sNew, "{") > 0 Then
    '                            For Each sNew2 As String In ExpandBlock(sNew)
    '                                sal2.Add(s & sNew2)
    '                            Next
    '                        Else
    '                            sal2.Add(s & sNew)
    '                        End If
    '                    Next
    '                Next

    '                sal = sal2

    '            Case Else
    '                For ix As Integer = 0 To sal.Count - 1
    '                    sal(ix) = sal(ix) & cNextChar
    '                Next
    '        End Select
    '    Next

    '    Return sal

    'End Function

    internal string CurrentIFID()
    {

        if (Adventure IsNot null && Adventure.BabelTreatyInfo IsNot null && Adventure.BabelTreatyInfo.Stories.Length = 1)
        {
            return "-" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID;
        }
        return "";

    }

    internal void SaveLayout()
    {
        try
        {
            'If Not IO.Directory.Exists(sLocalDataPath) Then IO.Directory.CreateDirectory(sLocalDataPath)

            UDMRunner.SaveAsXML(DataPath(true) + "RunnerLayout" + CurrentIFID() + ".xml");
        Catch
        }
    }

    internal void RestoreLayout(Infragistics.Win.UltraWinDock.DockableControlPane cpNew = null)
    {

        private string sXMLFile = DataPath(true) + "RunnerLayout" + CurrentIFID() + ".xml";

        try
        {

            If ! IO.File.Exists(sXMLFile) Then sXMLFile = DataPath(true) + "RunnerLayout.xml"
            If IO.File.Exists(sXMLFile) Then UDMRunner.LoadFromXML(sXMLFile)

            ' Restore the sizemode from our secret embedding in the layout
            RestoreDataFromLayoutXML();
            'If pbxGraphics.Parent IsNot Nothing AndAlso CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.OriginalControlBounds.Location.X = 1 Then
            '    pbxGraphics.SizeMode = CType(CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.OriginalControlBounds.Location.Y, clsImage.SizeModeEnum)
            'End If

            private bool bFoundNew = false;
            For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
                if (cp.Closed)
                {
                    if (cp.Text = "Map" && Not cp.IsVisible)
                    {
                        UTMMain.Tools("ShowMap").SharedProps.Enabled = true;
                    }
                    if (cp.Text = "Graphics" && Not cp.IsVisible)
                    {
                        UTMMain.Tools("ShowGraphics").SharedProps.Enabled = true;
                    }
                }
                If cpNew != null && cp.Key = cpNew.Key Then bFoundNew = true
            Next;

            ' If the new control isn't in a stored layout, re-add it to the restored layout
            try
            {
                if (Not bFoundNew && cpNew IsNot null)
                {
                    fRunner.UDMRunner.ControlPanes.Add(cpNew);
                    cpNew.Parent.Panes.RemoveAt(cpNew.Index);
                    fRunner.UDMRunner.DockAreas(0).Panes.Add(cpNew);
                }
            Catch
            }

            SetMargins();
        }
        catch (Exception ex)
        {
            if (MessageBox.Show("There was an error restoring the window layout.  Would you like me to delete the layout file?  This will mean the next time you start up Runner it should display the default layout.  Runner will end after the file is deleted.", "Error restoring layout", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    If IO.File.Exists(sXMLFile) Then IO.File.Delete(sXMLFile)
                    End;
                }
                catch (Exception ex2)
                {
                    ErrMsg("Could not delete file", ex2);
                }
            }
            'ErrMsg("Error restoring layout", ex)
        }
    }


    private void UDMRunner_AfterDockChange(object sender, Infragistics.Win.UltraWinDock.PaneEventArgs e)
    {
        if (UserSession.Map.ParentForm.Name = Me.Name)
        {
            SetMapButtons(true);
            UserSession.Map.ToolStrip1.Visible = false;
        Else
            SetMapButtons(false);
            UserSession.Map.ToolStrip1.Visible = true;
        }
    }

    private void SetMapButtons(bool bVisible)
    {

        UTMMain.Tools("MapZoomIn").SharedProps.Visible = bVisible;
        UTMMain.Tools("MapZoomOut").SharedProps.Visible = bVisible;

    }

    private void UDMRunner_AfterPaneButtonClick(object sender, Infragistics.Win.UltraWinDock.PaneButtonEventArgs e)
    {
        if (e.Button = Infragistics.Win.UltraWinDock.PaneButton.Close)
        {
            If ! UserSession.Map.Visible Then SetMapButtons(false)
        }
    }


    private void UDMRunner_AfterSplitterDrag(object sender, Infragistics.Win.UltraWinDock.PanesEventArgs e)
    {
        SetMargins();
    }


    private void UDMRunner_PaneHidden(object sender, Infragistics.Win.UltraWinDock.PaneHiddenEventArgs e)
    {

        if (e.Pane.Closed)
        {
            if (e.Pane.Text = "Graphics")
            {
                UTMMain.Tools("ShowGraphics").SharedProps.Enabled = true;
            ElseIf e.Pane.Text = "Map" Then
                UTMMain.Tools("ShowMap").SharedProps.Enabled = true;
                SetMapButtons(false);
            }
        }
        SetMargins();

    }


    private bool bSettingSize = false;
    private void frmRunner_Resize(object sender, System.EventArgs e)
    {
        SetMargins();

        If ! bSettingSize Then SaveDataInLayoutXML(RunnerWindowSize:=Me.Size, RunnerWindowState:=Me.WindowState)
    }



    Private Sub SaveDataInLayoutXML(Optional ByVal RunnerWindowSize As Size = Nothing,
                                    Optional ByVal RunnerWindowState As FormWindowState = (FormWindowState)(-1),;
                                    Optional ByVal PictureSizeMode As clsImage.SizeModeEnum = (clsImage.SizeModeEnum)(-1));

        try
        {
            if (pbxGraphics IsNot null && pbxGraphics.Parent IsNot null && CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane IsNot null)
            {

                private Rectangle r = CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.OriginalControlBounds;

                private int iNewSizeMode = r.Y;
                If PictureSizeMode > -1 Then iNewSizeMode = CInt(PictureSizeMode)

                private int iNewWindowState = r.Height - r.Height % 10000;
                If RunnerWindowState > -1 Then iNewWindowState = CInt(RunnerWindowState + 1) * 10000

                private int iNewWindowHeight = r.Height % 10000;
                If RunnerWindowSize.Height > 0 Then iNewWindowHeight = RunnerWindowSize.Height

                private int iNewWindowWidth = r.Width % 10000;
                If RunnerWindowSize.Width > 0 Then iNewWindowWidth = RunnerWindowSize.Width

                ' X is always 1, to denote our encoding
                (Infragistics.Win.UltraWinDock.DockableWindow)(pbxGraphics.Parent).Pane.OriginalControlBounds = New Rectangle(1, iNewSizeMode, iNewWindowWidth, iNewWindowState + iNewWindowHeight);
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error saving layout", ex);
        }

    }


    private void RestoreDataFromLayoutXML()
    {

        if (pbxGraphics IsNot null && pbxGraphics.Parent IsNot null)
        {
            private Rectangle r = CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.OriginalControlBounds;

            if (r.Location.X = 1)
            {

                private int iNewSizeMode = r.Y;
                If iNewSizeMode > -1 && iNewSizeMode < 3 Then pbxGraphics.SizeMode = (clsImage.SizeModeEnum)(iNewSizeMode)

                bSettingSize = True

                private int iNewWindowState = r.Height - r.Height % 10000;
                If iNewWindowState > 9999 Then Me.WindowState = (FormWindowState)(CInt(iNewWindowState / 10000 - 1))

                private int iNewWindowHeight = r.Height % 10000;
                If iNewWindowHeight > 0 Then Me.Height = iNewWindowHeight

                private int iNewWindowWidth = r.Width % 10000;
                If iNewWindowWidth > 0 Then Me.Width = iNewWindowWidth

                bSettingSize = False
            }
        }

    }


    Private WithEvents tmrSplash As New Timer

    private void frmRunner_Shown(object sender, System.EventArgs e)
    {

        tmrSplash.Interval = 3000;
        tmrSplash.Start();

        try
        {
            if (Environment.GetCommandLineArgs.Length > 1)
            {
                private string sExecutable = "";
                for (int i = 1; i <= Environment.GetCommandLineArgs.Length - 1; i++)
                {
                    If sExecutable <> "" Then sExecutable &= " "
                    sExecutable &= Environment.GetCommandLineArgs(i);
                Next;
                if (IO.File.Exists(sExecutable))
                {
                    If UserSession.OpenAdventure(sExecutable) Then Exit Sub
                }
            Else
                '' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
                '' Grab out the last 8 bytes, and see if it converts to a size
                'Dim bData(5) As Byte
                'Dim fStream As New IO.FileStream(Application.ExecutablePath, IO.FileMode.Open, IO.FileAccess.Read)
                'fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin)
                'fStream.Read(bData, 0, 6)
                'fStream.Close()

                'Dim sFileSize As String = (New System.Text.ASCIIEncoding).GetString(bData).ToUpper
                'If IsHex(sFileSize) Then
                If UserSession.bEXE && UserSession.OpenAdventure(Application.ExecutablePath, true) Then Exit Sub
                'End If
            }

            UserSession.Display("ADRIFT Runner Version 5.0<br><>© Campbell Wild 1998-2022<br>Last build: 23rd April 2022 (Release " + CInt(Double.Parse(Application.ProductVersion.Replace("5.0.", ""), Globalization.CultureInfo.InvariantCulture.NumberFormat)).ToString("0") + ")", true) '©;

        }
        catch (Exception ex)
        {
            ErrMsg("Startup Error", ex);
        }

    }


    private void tmrSplash_Tick(object sender, System.EventArgs e)
    {
        tmrSplash.Stop();
        If fSplash != null Then fSplash.Close()
    }


    protected override void WndProc(ref m As Message)
    {
        If m.Msg = 953 Then RepeatMMLoops() ' Notification that sound finished
        MyBase.WndProc(m);
    }

    private void UDMRunner_PaneDisplayed(object sender, Infragistics.Win.UltraWinDock.PaneDisplayedEventArgs e)
    {
        switch (true)
        {
            case e.Pane.Control Is UserSession.Map:
                {
                SetMapButtons(true);
        }
    }


    internal void txtOutput_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        OutputClicked(txtInput, (RichTextBox)(sender), ctxMenu, MousePosition, btnSubmit, e);
    }



    private void txtOutput_VScroll(object sender, System.EventArgs e)
    {
        '    ' Work out whether our previous offset is now off screen
        '    If txtOutput.GetCharIndexFromPosition(New Point(0, 0)) > iPreviousOffset Then
        '        ' Ok, we need to change the scroll position
        '        txtOutput.SelectionStart = iPreviousOffset
        '        txtOutput.ScrollToCaret()
        '    End If
        ' If we have scrolled the screen right to the bottom, get rid of the button
        if (btnMore.Visible)
        {
            if (txtOutput.GetCharIndexFromPosition(new Point(txtOutput.Width, txtOutput.Height)) >= txtOutput.TextLength - 1)
            {
                btnMore.Visible = false;
            }
        }

    }

    private void btnMore_Click(object sender, System.EventArgs e)
    {
        With txtOutput;
            btnMore.Visible = false;
            UserSession.iPreviousOffset = .GetCharIndexFromPosition(New Point(.Width, .Height));
            ScrollToEnd(txtOutput);
        }
    }


    private void pnlBottom_Resize(object sender, System.EventArgs e)
    {
        if (btnMore.Visible)
        {
            btnMore.Size = New Size(pnlBottom.Size.Width - 1, pnlBottom.Size.Height - 1);
        }
    }

    private void btnSubmit_Click(System.Object sender, System.EventArgs e)
    {
        btnSubmit.Enabled = false;
        SubmitCommand();
        btnSubmit.Visible = false;
        btnSubmit.Enabled = true;
    }


    private void ctxMenu_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
    {
        if (e.ClickedItem.Text.StartsWith("Type """))
        {
            txtInput.SelectedText = e.ClickedItem.Text.Substring(6, e.ClickedItem.Text.Length - 7) + " ";
            btnSubmit.Visible = true;
        Else
            txtInput.Text = "XX" + e.ClickedItem.Text;
            SubmitCommand();
        }
    }


    private void txtInput_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If e.Delta <> 0 Then SendMessage(txtOutput.Handle, EM_LINESCROLL, 0, -CInt(e.Delta / 40))
    }


    private void txtOutput_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        txtOutput.SuspendLayout();

        private int iCharPos = txtOutput.GetCharIndexFromPosition(new Point(e.X, e.Y));

        if (iCharPos > -1 Then '&& e.X < txtOutput.GetPositionFromCharIndex(txtOutput.Text.Length).X)
        {
            txtOutput.Cursor = Cursors.Arrow;
            txtOutput.SelectionStart = iCharPos;
            if (txtOutput.SelectionFont.Underline && txtOutput.SelectionColor = GetLinkColour())
            {
                txtOutput.Cursor = Cursors.Hand;
                'Else
                '   txtOutput.Cursor = Cursors.Arrow
            }
        Else
            txtOutput.Cursor = Cursors.Arrow;
        }

        txtOutput.ResumeLayout();

    }




    private void pbxGraphics_SizeModeChanged(clsImage o, clsImage.ImageEventArgs e)
    {
        ' Store the SizeMode in the OriginalControlBounds, so it gets stored in the Infragistics XML
        'CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.OriginalControlBounds = New Rectangle(1, CInt(e.SizeMode), 1, 1)
        'CType(pbxGraphics.Parent, Infragistics.Win.UltraWinDock.DockableWindow).Pane.Key=
        SaveDataInLayoutXML(PictureSizeMode:=e.SizeMode);
    }

}


}