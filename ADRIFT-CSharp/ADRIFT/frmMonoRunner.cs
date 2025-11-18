using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmRunner
{
    Inherits System.Windows.Forms.Form;

    'Friend salCommands As New StringArrayList
    private int iCommand;
    'Friend WithEvents Map As ADRIFT.Map

    'Friend WithEvents DockableWindow1 As Infragistics.Win.UltraWinDock.DockableWindow
    Friend WithEvents pnlTop As System.Windows.Forms.Panel;
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel;
    Friend WithEvents SplitContainerInputOutput As System.Windows.Forms.SplitContainer;
    'Friend WithEvents WindowDockingArea3 As Infragistics.Win.UltraWinDock.WindowDockingArea
    Friend WithEvents btnMore As Windows.Forms.Button;
    Friend WithEvents btnSubmit As Windows.Forms.Button;
    Friend WithEvents ctxMenu As New ContextMenuStrip;
    private Splash fSplash;
    private bool bEXE = false;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        UserSession = New RunnerSession

        'If Environment.GetCommandLineArgs.Length = 1 Then

        '    ' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
        '    ' Grab out the last 8 bytes, and see if it converts to a size
        '    Dim bData(5) As Byte
        '    Dim fStream As New IO.FileStream(Application.ExecutablePath, IO.FileMode.Open, IO.FileAccess.Read)
        '    fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin)
        '    fStream.Read(bData, 0, 6)
        '    fStream.Close()

        '    Dim sFileSize As String = (New System.Text.ASCIIEncoding).GetString(bData).ToUpper
        '    If IsHex(sFileSize) Then
        '        Dim lFileSize As Long = CLng("&H" & sFileSize)
        '        If lFileSize > 0 Then
        '            ' Ok, check the offset to see that the appended data is really a TAF...
        '            fStream = New IO.FileStream(Application.ExecutablePath, IO.FileMode.Open, IO.FileAccess.Read)
        '            fStream.Seek(lFileSize, IO.SeekOrigin.Begin)
        '            ReDim bData(11)
        '            fStream.Read(bData, 0, 12)
        '            fStream.Close()
        '            Dim sVersion As String = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(bData), 1))
        '            If sVersion = "Version 5.00" Then bEXE = True
        '        End If
        '    End If
        'End If

        if (Not bEXE)
        {
            fSplash = New Splash
            fSplash.Show(Me);
            Application.DoEvents();
        }

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        if (bEXE)
        {
            miOpenAdventure.Visible = false;
            miOpenGame.Visible = false;
            miRecentAdventures.Visible = false;
            miAboutADRIFT.Visible = false;
            miDebugger.Visible = false;
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
    'Friend WithEvents _frmRunnerUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    'Friend WithEvents _frmRunnerUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    'Friend WithEvents _frmRunnerUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    'Friend WithEvents _frmRunnerUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    'Friend WithEvents _frmRunner_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    'Friend WithEvents _frmRunner_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    'Friend WithEvents _frmRunner_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    'Friend WithEvents _frmRunner_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    'Friend WithEvents _frmRunnerAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
    'Friend WithEvents UTMMain As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    'Friend WithEvents UDMRunner As Infragistics.Win.UltraWinDock.UltraDockManager
    'Friend WithEvents StatusBar As Infragistics.Win.UltraWinStatusBar.UltraStatusBar
    Friend WithEvents StatusBar As StatusBar;
    'Friend WithEvents DockableWindow2 As Infragistics.Win.UltraWinDock.DockableWindow
    'Friend WithEvents WindowDockingArea2 As Infragistics.Win.UltraWinDock.WindowDockingArea
    Friend WithEvents pbxGraphics As clsImage;
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents txtInput As System.Windows.Forms.RichTextBox;
    Friend WithEvents txtOutput As System.Windows.Forms.RichTextBox;

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmRunner));
        Me.StatusBar = New System.Windows.Forms.StatusBar();
        Me.Description = New System.Windows.Forms.StatusBarPanel();
        Me.UserStatus = New System.Windows.Forms.StatusBarPanel();
        Me.Score = New System.Windows.Forms.StatusBarPanel();
        Me.txtInput = New System.Windows.Forms.RichTextBox();
        Me.txtOutput = New System.Windows.Forms.RichTextBox();
        Me.ofd = New System.Windows.Forms.OpenFileDialog();
        Me.pnlTop = New System.Windows.Forms.Panel();
        Me.pnlBottom = New System.Windows.Forms.Panel();
        Me.btnMore = New System.Windows.Forms.Button();
        Me.SplitContainerInputOutput = New System.Windows.Forms.SplitContainer();
        Me.SplitContainerTextOther = New System.Windows.Forms.SplitContainer();
        Me.SplitContainerMapGraphics = New System.Windows.Forms.SplitContainer();
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip();
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miOpenAdventure = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator();
        Me.miOpenGame = New System.Windows.Forms.ToolStripMenuItem();
        Me.miRestartGame = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator();
        Me.miSaveGame = New System.Windows.Forms.ToolStripMenuItem();
        Me.miSaveGameAs = New System.Windows.Forms.ToolStripMenuItem();
        Me.miStartTranscript = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator();
        Me.miRecentAdventures = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator();
        Me.miExit = New System.Windows.Forms.ToolStripMenuItem();
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miAutoComplete = New System.Windows.Forms.ToolStripMenuItem();
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miFullScreen = New System.Windows.Forms.ToolStripMenuItem();
        Me.miDebugger = New System.Windows.Forms.ToolStripMenuItem();
        Me.miOptions = New System.Windows.Forms.ToolStripMenuItem();
        Me.miMacros = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator();
        Me.miEditMacros = New System.Windows.Forms.ToolStripMenuItem();
        Me.WindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miShowMap = New System.Windows.Forms.ToolStripMenuItem();
        Me.miShowGraphics = New System.Windows.Forms.ToolStripMenuItem();
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miAboutADRIFT = New System.Windows.Forms.ToolStripMenuItem();
        Me.miAboutAdventure = New System.Windows.Forms.ToolStripMenuItem();

        Me.pbxGraphics = New ADRIFT.clsImage();
        Me.btnSubmit = New System.Windows.Forms.Button();
        (System.ComponentModel.ISupportInitialize)(Me.Description).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UserStatus).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.Score).BeginInit();
        Me.pnlTop.SuspendLayout();
        Me.pnlBottom.SuspendLayout();
        Me.SplitContainerInputOutput.Panel1.SuspendLayout();
        Me.SplitContainerInputOutput.Panel2.SuspendLayout();
        Me.SplitContainerInputOutput.SuspendLayout();
        Me.SplitContainerTextOther.Panel1.SuspendLayout();
        Me.SplitContainerTextOther.Panel2.SuspendLayout();
        Me.SplitContainerTextOther.SuspendLayout();
        Me.SplitContainerMapGraphics.Panel1.SuspendLayout();
        Me.SplitContainerMapGraphics.Panel2.SuspendLayout();
        Me.SplitContainerMapGraphics.SuspendLayout();
        Me.MenuStrip1.SuspendLayout();
        Me.SuspendLayout();
        '
        'StatusBar
        '
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!);
        Me.StatusBar.Location = New System.Drawing.Point(0, 407);
        Me.StatusBar.Name = "StatusBar";
        Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.Description, Me.UserStatus, Me.Score});
        Me.StatusBar.ShowPanels = true;
        Me.StatusBar.Size = New System.Drawing.Size(611, 23);
        Me.StatusBar.TabIndex = 11;
        Me.StatusBar.Tag = "";
        '
        'Description
        '
        Me.Description.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
        Me.Description.Name = "Description";
        Me.Description.Text = "Please open an adventure file";
        Me.Description.Width = 215;
        '
        'UserStatus
        '
        Me.UserStatus.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
        Me.UserStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
        Me.UserStatus.Name = "UserStatus";
        Me.UserStatus.Width = 369;
        '
        'Score
        '
        Me.Score.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
        Me.Score.Name = "Score";
        Me.Score.Width = 10;
        '
        'txtInput
        '
        Me.txtInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtInput.BackColor = System.Drawing.Color.Black;
        Me.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.txtInput.ForeColor = System.Drawing.SystemColors.HighlightText;
        Me.txtInput.Location = New System.Drawing.Point(48, -2);
        Me.txtInput.Multiline = false;
        Me.txtInput.Name = "txtInput";
        Me.txtInput.Size = New System.Drawing.Size(366, 24);
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
        Me.txtOutput.Size = New System.Drawing.Size(367, 351);
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
        Me.pnlTop.Size = New System.Drawing.Size(418, 355);
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
        Me.pnlBottom.Size = New System.Drawing.Size(418, 27);
        Me.pnlBottom.TabIndex = 0;
        '
        'btnMore
        '
        Me.btnMore.BackColor = System.Drawing.SystemColors.ButtonFace;
        Me.btnMore.ForeColor = System.Drawing.SystemColors.ControlText;
        Me.btnMore.Location = New System.Drawing.Point(104, -2);
        Me.btnMore.Name = "btnMore";
        Me.btnMore.Size = New System.Drawing.Size(273, 26);
        Me.btnMore.TabIndex = 12;
        Me.btnMore.Text = "Press any key to continue";
        Me.btnMore.UseVisualStyleBackColor = false;
        Me.btnMore.Visible = false;
        '
        'SplitContainerInputOutput
        '
        Me.SplitContainerInputOutput.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainerInputOutput.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        Me.SplitContainerInputOutput.Location = New System.Drawing.Point(0, 0);
        Me.SplitContainerInputOutput.Name = "SplitContainerInputOutput";
        Me.SplitContainerInputOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
        '
        'SplitContainerInputOutput.Panel1
        '
        Me.SplitContainerInputOutput.Panel1.Controls.Add(Me.pnlTop);
        '
        'SplitContainerInputOutput.Panel2
        '
        Me.SplitContainerInputOutput.Panel2.Controls.Add(Me.pnlBottom);
        Me.SplitContainerInputOutput.Size = New System.Drawing.Size(418, 383);
        Me.SplitContainerInputOutput.SplitterDistance = 355;
        Me.SplitContainerInputOutput.SplitterWidth = 1;
        Me.SplitContainerInputOutput.TabIndex = 1;
        Me.SplitContainerInputOutput.TabStop = false;
        '
        'SplitContainerTextOther
        '
        Me.SplitContainerTextOther.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainerTextOther.Location = New System.Drawing.Point(0, 24);
        Me.SplitContainerTextOther.Name = "SplitContainerTextOther";
        '
        'SplitContainerTextOther.Panel1
        '
        Me.SplitContainerTextOther.Panel1.Controls.Add(Me.SplitContainerInputOutput);
        '
        'SplitContainerTextOther.Panel2
        '
        Me.SplitContainerTextOther.Panel2.Controls.Add(Me.SplitContainerMapGraphics);
        Me.SplitContainerTextOther.Size = New System.Drawing.Size(611, 383);
        Me.SplitContainerTextOther.SplitterDistance = 418;
        Me.SplitContainerTextOther.TabIndex = 12;
        '
        'SplitContainerMapGraphics
        '
        Me.SplitContainerMapGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainerMapGraphics.Location = New System.Drawing.Point(0, 0);
        Me.SplitContainerMapGraphics.Name = "SplitContainerMapGraphics";
        Me.SplitContainerMapGraphics.Orientation = System.Windows.Forms.Orientation.Horizontal;
        '
        'SplitContainerMapGraphics.Panel1
        '
        Me.SplitContainerMapGraphics.Panel1.Controls.Add(UserSession.Map);
        '
        'SplitContainerMapGraphics.Panel2
        '
        Me.SplitContainerMapGraphics.Panel2.Controls.Add(Me.pbxGraphics);
        Me.SplitContainerMapGraphics.Size = New System.Drawing.Size(189, 383);
        Me.SplitContainerMapGraphics.SplitterDistance = 190;
        Me.SplitContainerMapGraphics.TabIndex = 2;
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem, Me.miMacros, Me.WindowToolStripMenuItem, Me.HelpToolStripMenuItem});
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0);
        Me.MenuStrip1.Name = "MenuStrip1";
        Me.MenuStrip1.Size = New System.Drawing.Size(611, 24);
        Me.MenuStrip1.TabIndex = 13;
        Me.MenuStrip1.Text = "MenuStrip1";
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miOpenAdventure, Me.ToolStripSeparator1, Me.miOpenGame, Me.miRestartGame, Me.ToolStripSeparator2, Me.miSaveGame, Me.miSaveGameAs, Me.miStartTranscript, Me.ToolStripSeparator3, Me.miRecentAdventures, Me.ToolStripSeparator4, Me.miExit});
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20);
        Me.FileToolStripMenuItem.Text = "&File";
        '
        'miOpenAdventure
        '
        Me.miOpenAdventure.Name = "miOpenAdventure";
        Me.miOpenAdventure.ShortcutKeys = (System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
        Me.miOpenAdventure.Size = New System.Drawing.Size(203, 22);
        Me.miOpenAdventure.Text = "Open Adventure";
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1";
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(200, 6);
        '
        'miOpenGame
        '
        Me.miOpenGame.Enabled = false;
        Me.miOpenGame.Name = "miOpenGame";
        Me.miOpenGame.ShortcutKeys = (System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
        Me.miOpenGame.Size = New System.Drawing.Size(203, 22);
        Me.miOpenGame.Text = "Open Game...";
        '
        'miRestartGame
        '
        Me.miRestartGame.Enabled = false;
        Me.miRestartGame.Name = "miRestartGame";
        Me.miRestartGame.ShortcutKeys = (System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
        Me.miRestartGame.Size = New System.Drawing.Size(203, 22);
        Me.miRestartGame.Text = "Restart Game";
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2";
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(200, 6);
        '
        'miSaveGame
        '
        Me.miSaveGame.Enabled = false;
        Me.miSaveGame.Name = "miSaveGame";
        Me.miSaveGame.ShortcutKeys = (System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
        Me.miSaveGame.Size = New System.Drawing.Size(203, 22);
        Me.miSaveGame.Text = "Save Game";
        '
        'miSaveGameAs
        '
        Me.miSaveGameAs.Enabled = false;
        Me.miSaveGameAs.Name = "miSaveGameAs";
        Me.miSaveGameAs.Size = New System.Drawing.Size(203, 22);
        Me.miSaveGameAs.Text = "Save Game As...";
        '
        'miStartTranscript
        '
        Me.miStartTranscript.Name = "miStartTranscript";
        Me.miStartTranscript.Size = New System.Drawing.Size(203, 22);
        Me.miStartTranscript.Text = "Start Transcript...";
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3";
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(200, 6);
        '
        'miRecentAdventures
        '
        Me.miRecentAdventures.Name = "miRecentAdventures";
        Me.miRecentAdventures.Size = New System.Drawing.Size(203, 22);
        Me.miRecentAdventures.Text = "Recent Adventures";
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4";
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(200, 6);
        '
        'miExit
        '
        Me.miExit.Name = "miExit";
        Me.miExit.Size = New System.Drawing.Size(203, 22);
        Me.miExit.Text = "E&xit";
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miAutoComplete});
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20);
        Me.EditToolStripMenuItem.Text = "&Edit";
        '
        'miAutoComplete
        '
        Me.miAutoComplete.CheckOnClick = true;
        Me.miAutoComplete.Name = "miAutoComplete";
        Me.miAutoComplete.Size = New System.Drawing.Size(155, 22);
        Me.miAutoComplete.Text = "Auto Complete";
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miFullScreen, Me.miDebugger, Me.miOptions});
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20);
        Me.ViewToolStripMenuItem.Text = "&View";
        '
        'miFullScreen
        '
        Me.miFullScreen.CheckOnClick = true;
        Me.miFullScreen.Name = "miFullScreen";
        Me.miFullScreen.Size = New System.Drawing.Size(131, 22);
        Me.miFullScreen.Text = "Full Screen";
        '
        'miDebugger
        '
        Me.miDebugger.CheckOnClick = true;
        Me.miDebugger.Name = "miDebugger";
        Me.miDebugger.Size = New System.Drawing.Size(131, 22);
        Me.miDebugger.Text = "Debugger";
        '
        'miOptions
        '
        Me.miOptions.Name = "miOptions";
        Me.miOptions.Size = New System.Drawing.Size(131, 22);
        Me.miOptions.Text = "Options";
        '
        'miMacros
        '
        Me.miMacros.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator5, Me.miEditMacros});
        Me.miMacros.Enabled = false;
        Me.miMacros.Name = "miMacros";
        Me.miMacros.Size = New System.Drawing.Size(58, 20);
        Me.miMacros.Text = "&Macros";
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5";
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(133, 6);
        '
        'miEditMacros
        '
        Me.miEditMacros.Name = "miEditMacros";
        Me.miEditMacros.Size = New System.Drawing.Size(136, 22);
        Me.miEditMacros.Text = "Edit Macros";
        '
        'WindowToolStripMenuItem
        '
        Me.WindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miShowMap, Me.miShowGraphics});
        Me.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem";
        Me.WindowToolStripMenuItem.Size = New System.Drawing.Size(63, 20);
        Me.WindowToolStripMenuItem.Text = "&Window";
        '
        'miShowMap
        '
        Me.miShowMap.Checked = true;
        Me.miShowMap.CheckOnClick = true;
        Me.miShowMap.CheckState = System.Windows.Forms.CheckState.Checked;
        Me.miShowMap.Name = "miShowMap";
        Me.miShowMap.Size = New System.Drawing.Size(152, 22);
        Me.miShowMap.Text = "Show Map";
        '
        'miShowGraphics
        '
        Me.miShowGraphics.Checked = true;
        Me.miShowGraphics.CheckOnClick = true;
        Me.miShowGraphics.CheckState = System.Windows.Forms.CheckState.Checked;
        Me.miShowGraphics.Name = "miShowGraphics";
        Me.miShowGraphics.Size = New System.Drawing.Size(152, 22);
        Me.miShowGraphics.Text = "Show Graphics";
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miAboutADRIFT, Me.miAboutAdventure});
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20);
        Me.HelpToolStripMenuItem.Text = "&Help";
        '
        'miAboutADRIFT
        '
        Me.miAboutADRIFT.Name = "miAboutADRIFT";
        Me.miAboutADRIFT.Size = New System.Drawing.Size(165, 22);
        Me.miAboutADRIFT.Text = "About ADRIFT";
        '
        'miAboutAdventure
        '
        Me.miAboutAdventure.Name = "miAboutAdventure";
        Me.miAboutAdventure.Size = New System.Drawing.Size(165, 22);
        Me.miAboutAdventure.Text = "About Adventure";
        '
        'Map
        '
        'Me.Map.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.Map.Location = New System.Drawing.Point(0, 0)
        'Me.Map.Map = Nothing
        'Me.Map.Name = "Map"
        'Me.Map.ShowAxes = False
        'Me.Map.ShowGrid = False
        'Me.Map.Size = New System.Drawing.Size(189, 190)
        'Me.Map.TabIndex = 0
        '
        'pbxGraphics
        '
        Me.pbxGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.pbxGraphics.BackColour = System.Drawing.Color.Transparent;
        Me.pbxGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.pbxGraphics.Image = null;
        Me.pbxGraphics.Location = New System.Drawing.Point(0, 0);
        Me.pbxGraphics.Name = "pbxGraphics";
        Me.pbxGraphics.Size = New System.Drawing.Size(189, 189);
        Me.pbxGraphics.SizeMode = ADRIFT.clsImage.SizeModeEnum.ActualSizeCentred;
        Me.pbxGraphics.TabIndex = 0;
        Me.pbxGraphics.TabStop = false;
        '
        'btnSubmit
        '
        Me.btnSubmit.BackColor = System.Drawing.SystemColors.ButtonFace;
        Me.btnSubmit.ForeColor = System.Drawing.SystemColors.ControlText;
        Me.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
        Me.btnSubmit.Location = New System.Drawing.Point(389, 0);
        Me.btnSubmit.Name = "btnSubmit";
        Me.btnSubmit.Size = New System.Drawing.Size(49, 21);
        Me.btnSubmit.TabIndex = 13;
        Me.btnSubmit.Text = "Submit";
        Me.btnSubmit.Visible = false;
        '
        'frmRunner
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        Me.ClientSize = New System.Drawing.Size(611, 430);
        Me.Controls.Add(Me.SplitContainerTextOther);
        Me.Controls.Add(Me.StatusBar);
        Me.Controls.Add(Me.MenuStrip1);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.MainMenuStrip = Me.MenuStrip1;
        Me.Name = "frmRunner";
        Me.Text = "ADRIFT Runner";
        (System.ComponentModel.ISupportInitialize)(Me.Description).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UserStatus).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.Score).EndInit();
        Me.pnlTop.ResumeLayout(false);
        Me.pnlBottom.ResumeLayout(false);
        Me.SplitContainerInputOutput.Panel1.ResumeLayout(false);
        Me.SplitContainerInputOutput.Panel2.ResumeLayout(false);
        Me.SplitContainerInputOutput.ResumeLayout(false);
        Me.SplitContainerTextOther.Panel1.ResumeLayout(false);
        Me.SplitContainerTextOther.Panel2.ResumeLayout(false);
        Me.SplitContainerTextOther.ResumeLayout(false);
        Me.SplitContainerMapGraphics.Panel1.ResumeLayout(false);
        Me.SplitContainerMapGraphics.Panel2.ResumeLayout(false);
        Me.SplitContainerMapGraphics.ResumeLayout(false);
        Me.MenuStrip1.ResumeLayout(false);
        Me.MenuStrip1.PerformLayout();
        Me.ResumeLayout(false);
        Me.PerformLayout();

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
            FormBorderStyle = Windows.Forms.FormBorderStyle.None
            WindowState = FormWindowState.Maximized
            txtOutput.Visible = true;
            'Me.txtOutput.BorderStyle = BorderStyle.None
            'Me.txtInput.BorderStyle = BorderStyle.None
            'Show()
        Else
            WindowState = FormWindowState.Normal
            FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            Me.ControlBox = true;
            Me.Text = "ADRIFT Runner";
            'Hide()

            pnlTop.BorderStyle = BorderStyle.Fixed3D;
            pnlBottom.BorderStyle = BorderStyle.Fixed3D;

            'Show()
            'Me.txtOutput.BorderStyle = BorderStyle.Fixed3D
            'Me.txtInput.BorderStyle = BorderStyle.Fixed3D
            }
        'Application.DoEvents()
        miFullScreen.Checked = bActivate;
        SaveSetting("ADRIFT", "Runner", "FullScreen", bActivate.ToString);
    }

    'Private Sub UTMMain_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles UTMMain.ToolClick
    '    DoToolClick(e.Tool.Key, e.Tool.SharedProps.Caption, CStr(e.Tool.SharedProps.Tag))
    'End Sub



    'Private Sub DoToolClick(ByVal sKey As String, ByVal sCaption As String, ByVal sTag As String)

    '    Try
    '        If sTag = "_RECENT_" Then
    internal void miRecentAdventures_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.OpenAdventure(SafeString((ToolStripItem)(sender).Tag));
    }
    '        End If

    '        Select Case sKey
    '            Case "About"
    private void miAboutADRIFT_Click(System.Object sender, System.EventArgs e)
    {
        private New AboutBox fAbout;
        try
        {
            fAbout.ShowDialog();
        Catch
        }
    }

    '                'MessageBox.Show("ADRIFT Runner" & vbCrLf & "Version " & Application.ProductVersion & vbCrLf & "© Campbell Wild 2010" & vbCrLf & vbCrLf & "Alpha Release.  Registered users only." & vbCrLf & "Splash image ""Adrift"" © V. Milovic 2010 (www.brokentoyland.com)", "About ADRIFT Runner", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Case "AboutAdventure"
    private void miAboutAdventure_Click(System.Object sender, System.EventArgs e)
    {
        private string sVersion = "";
        if (Adventure IsNot null && Adventure.BabelTreatyInfo IsNot null)
        {
            if (Adventure.BabelTreatyInfo.Stories.Length = 1 && Adventure.BabelTreatyInfo.Stories(0).Releases IsNot null)
            {
                With Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release;
                    sVersion = vbCrLf & "Version: " & .Version
                    If .ReleaseDate > Date.MinValue Then sVersion &= vbCrLf + "Released: " + .ReleaseDate.ToLongDateString
                }
            }
        }
        If Adventure != null Then MessageBox.Show(Adventure.Title + vbCrLf + "By " + Adventure.Author + sVersion, "About Adventure", MessageBoxButtons.OK, MessageBoxIcon.Information)
    }
    '            Case "AutoComplete"
    private void miAutoComplete_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.bAutoComplete = miAutoComplete.Checked;
        SaveSetting("ADRIFT", "Generator", "Auto Complete", UserSession.bAutoComplete.ToString);
    }
    '            Case "Debugger"
    private void miDebugger_Click(System.Object sender, System.EventArgs e)
    {
        If UserSession.Debugger == null || UserSession.Debugger.IsDisposed Then UserSession.Debugger = New frmDebugger
        UserSession.Debugger.Visible = miDebugger.Checked ' (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("Debugger")).Checked;
    }
    '            Case "Exit"
    private void miExit_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.Quit();
    }
    '            Case "mnuFullScreen"
    private void miFullScreen_Click(System.Object sender, System.EventArgs e)
    {
        FullScreen(miFullScreen.Checked);
    }
    '            Case "mnuOpenAdv"
    private void OpenAdventureToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
    {
        OpenAdventureDialog(fRunner.ofd);
    }
    '            Case "RestartGame"
    private void miRestartGame_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.Restart();
    }
    '            Case "OpenGame"
    private void miOpenGame_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.Restore();
    }
    '            Case "SaveGame", "SaveGameAs"
    private void miSaveGame_Click(System.Object sender, System.EventArgs e)
    {
        UserSession.SaveGame(sender == miSaveGameAs);
    }
    '            Case "ShowGraphics"
    private void miShowGraphics_Click(System.Object sender, System.EventArgs e)
    {
        If ! Me.Visible Then Exit Sub
        if (miShowGraphics.Checked)
        {
            SplitContainerMapGraphics.Panel2Collapsed = false;
            If ! miShowMap.Checked Then SplitContainerTextOther.Panel2Collapsed = false
        Else
            SplitContainerMapGraphics.Panel2Collapsed = true;
            If ! miShowMap.Checked Then SplitContainerTextOther.Panel2Collapsed = true
        }
        SaveSetting("ADRIFT", "Runner", "ShowGraphics", CInt(miShowGraphics.Checked).ToString);
        '                For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
        '                    If cp.Text = "Graphics" AndAlso Not cp.IsVisible Then
        '                        cp.Show()
        '                        UTMMain.Tools("ShowGraphics").SharedProps.Enabled = False
        '                        SetMargins()
        '                    End If
        '                Next
    }

    '            Case "ShowMap"
    private void miShowMap_Click(System.Object sender, System.EventArgs e)
    {
        If ! Me.Visible Then Exit Sub
        if (miShowMap.Checked)
        {
            SplitContainerMapGraphics.Panel1Collapsed = false;
            If ! miShowGraphics.Checked Then SplitContainerTextOther.Panel2Collapsed = false
        Else
            SplitContainerMapGraphics.Panel1Collapsed = true;
            If ! miShowGraphics.Checked Then SplitContainerTextOther.Panel2Collapsed = true
        }
        SaveSetting("ADRIFT", "Runner", "ShowMap", CInt(miShowMap.Checked).ToString);
        '                For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In UDMRunner.ControlPanes
        '                    If cp.Text = "Map" AndAlso Not cp.IsVisible Then
        '                        cp.Show()
        '                        UTMMain.Tools("ShowMap").SharedProps.Enabled = False
        '                        SetMargins()
        '                    End If
        '                Next
        '                'If Not UDMRunner.ControlPanes.Exists("") Then Exit Sub
        '                'UDM.ControlPanes(sPane).Closed = False ' We're getting an Infragistics Stack overflow bug whenever we try to reopen a pane that was restored as closed...
        '                'UDM.ControlPanes(sPane).Activate()
        '                'CType(sender, ToolStripMenuItem).Visible = False
        '                'If Not arlOpenPanes.Contains(sPane) Then arlOpenPanes.Add(sPane)
    }
    '            Case "miStartTranscript"
    private void miStartTranscript_Click(System.Object sender, System.EventArgs e)
    {
        if (miStartTranscript.Text = "Start Transcript...")
        {
            StartTranscript();
        Else
            StopTranscript();
        }
    }

    '            Case "Options"
    private void miOptions_Click(System.Object sender, System.EventArgs e)
    {
        private New frmOptionsRunner frmOptions;
        frmOptions.ShowDialog();
    }
    '            Case "Edit Macros"
    private void miEditMacros_Click(System.Object sender, System.EventArgs e)
    {
        private New frmMacros frmMacros;
        'frmMacros.dictMacros = CType(dictMacros.Clon, StringHashTable)
        frmMacros.ldictMacros = New Generic.Dictionary(Of String, clsMacro);
        For Each macro As clsMacro In UserSession.dictMacros.Values
            frmMacros.ldictMacros.Add(macro.Key, (clsMacro)(macro.Clone));
        Next;
        frmMacros.Text = "Edit Macros - " + Adventure.Title;
        frmMacros.Show();
    }


    '            Case "MapPlan"
    '                Map.PlanView()

    '            Case "MapCentre"
    '                Map.CentreMap()

    '            Case "MapZoomIn"
    '                Map.Zoom(True)

    '            Case "MapZoomOut"
    '                Map.Zoom(False)

    '            Case "CentreMapLock"
    '                Map.LockMapCentre = CType(UTMMain.Tools(sKey), Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked

    '            Case "MapPlayerLock"
    '                Map.LockPlayerCentre = CType(UTMMain.Tools(sKey), Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked

    '            Case Else
    '                'ErrMsg("Tool " & sKey & " not yet programmed!")

    '        End Select

    private void miMacros_Click(System.Object sender, System.EventArgs e)
    {
        if (sLeft(CType(sender, ToolStripMenuItem).Name, 6) = "Macro-")
        {
            RunMacro((ToolStripMenuItem)(sender).Name.Replace("Macro-", ""));
        }
    }


    '    Catch ex As Exception
    '        ErrMsg("Tool Click error", ex)
    '    End Try

    'End Sub

    private void frmRunner_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyData = Keys.Escape)
        {
            bStopMacro = True
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


    public void UpdateStatusBar(string sDescription, string sScore, string sUserStatus)
    {
        With StatusBar;
            .Panels("Description").Text = sDescription;
            if (sScore <> "")
            {
                .Panels("Score").Text = sScore;
                '.Panels("Score").Visible = True
            Else
                '.Panels("Score").Visible = False
            }
            if (sUserStatus <> "")
            {
                .Panels("UserStatus").Text = ReplaceALRs(sUserStatus);
                '.Panels("UserStatus").Visible = True
                '.Panels("Description").SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic
            Else
                '.Panels("UserStatus").Visible = False
                '.Panels("Description").SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring
            }
        }
    }


    ' Reload the macros on the form
    public void ReloadMacros()
    {

        'Dim mnuMacros As PopupMenuTool = CType(UTMMain.Tools("Macros"), PopupMenuTool)

        for (int iTool = miMacros.DropDownItems.Count - 1; iTool <= 0; iTool += -1)
        {
            If miMacros.DropDownItems(iTool).Name <> "miEditMacros" && miMacros.DropDownItems(iTool).Name <> "ToolStripSeparator5" Then miMacros.DropDownItems.RemoveAt(iTool)
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
                    if (Not miMacros.DropDownItems.ContainsKey(sToolKey))
                    {
                        private New ToolStripMenuItem(.Title, Nothing, AddressOf miMacros_Click) ' ButtonTool(sToolKey) newTool;
                        newTool.Name = sToolKey;
                        miMacros.DropDownItems.Insert(miMacros.DropDownItems.Count - 2, newTool);
                    }

                    if (.Shortcut <> Keys.None)
                    {
                        (ToolStripMenuItem)(miMacros.DropDownItems(sToolKey)).ShortcutKeys = (Keys)(.Shortcut);
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
            MenuStrip1.Enabled = ! bLocked;
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
            txtInput.Focus();
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
            txtInput.Top = -2;
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
            If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("txt") Then sfd.FileName = ""
            if (sfd.ShowDialog() = Windows.Forms.DialogResult.OK)
            {
                'UTMMain.Tools("miStartTranscript").SharedProps.Caption = "Stop Transcript"
                .sTranscriptFile = sfd.FileName;
            }
        }

    }


    private void StopTranscript()
    {
        MessageBox.Show("Saving Transcript stopped." + vbCrLf + vbCrLf + "The file was saved as """ + UserSession.sTranscriptFile + """.", "ADRIFT Runner", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (UserSession.UserSplash IsNot null && UserSession.UserSplash.Visible)
                {
                    e.Handled = true;
                    UserSession.UserSplash.Hide();
                    Exit Sub;
                }

                If iCommand > .salCommands.Count - 1 Then iCommand = .salCommands.Count - 1

                switch (e.KeyCode)
                {
                    'Case Keys.Escape
                    '    OpenAdventureDialog(Me.ofd)
                    case Keys.Enter:
                        {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        bIgnore = True
                        'If txtInput.Text <> "" AndAlso .salCommands.Count > 0 Then
                        '    .salCommands.Add("")
                        '    iCommand = .salCommands.Count - 1
                        '    .salCommands(iCommand - 1) = txtInput.Text.Substring(2)
                        '    UserSession.ClearAutoCompletes()
                        '    UserSession.Process(txtInput.Text.Substring(2).Trim)
                        'End If
                        SubmitCommand();
                    case Keys.Up:
                        {
                        if (iCommand > 0)
                        {
                            iCommand -= 1;
                            'UserSession.InitialiseInputBox()
                            ''txtInput.SelectedText = salCommands(iCommand)
                            ''txtInput.SelectionStart = txtInput.Text.Length
                            'txtInput.AppendText(.salCommands(iCommand))
                            SetInput(.salCommands(iCommand));
                        }
                        e.Handled = true;
                    case Keys.Down:
                        {
                        if (iCommand < .salCommands.Count - 1)
                        {
                            iCommand += 1;
                            'UserSession.InitialiseInputBox()
                            ''txtInput.SelectedText = salCommands(iCommand)
                            ''txtInput.SelectionStart = txtInput.Text.Length
                            'txtInput.AppendText(.salCommands(iCommand))
                            SetInput(.salCommands(iCommand));
                        }
                        e.Handled = true;
                    case Keys.Left:
                        {
                        If txtInput.SelectionStart <= 2 Then e.Handled = true
                    case Keys.Back:
                        {
                        if (txtInput.SelectionStart <= 2)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        ' Otherwise, let it flow...
                    case Keys.OemQuotes:
                        {
                        if (txtInput.SelectionStart = 2)
                        {
                            UserSession.InitialiseInputBox("@");
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                        }
                    default:
                        {
                        Application.DoEvents();
                        if (iCommand > -1 && .salCommands.Count > iCommand)
                        {
                            if (txtInput.Text.Length = 2)
                            {
                                ' Weird Mono bug
                                private char cTyped = ChrW(e.KeyValue);
                                if (Char.IsLetterOrDigit(cTyped))
                                {
                                    if (e.Shift)
                                    {
                                        cTyped = UCase(cTyped)
                                    Else
                                        cTyped = LCase(cTyped)
                                    }
                                    txtInput.AppendText(cTyped);
                                }
                            }
                            .salCommands(iCommand) = txtInput.Text.Substring(2);
                        }

                        If ! .bAutoComplete || (Adventure != null && Adventure.eGameState <> clsAction.EndGameEnum.Running) || (txtInput.Tag != null && txtInput.Tag.ToString = "Comment") Then Exit Sub
                        If txtInput.Text.Length < 2 Then Exit Sub

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
        txtInput.AppendText(sText);
    }


    public void SubmitCommand()
    {

        With UserSession;
            if (txtInput.Text <> "" && .salCommands.Count > 0)
            {
                btnSubmit.Enabled = false;
                .salCommands.Add("");
                iCommand = .salCommands.Count - 1
                .salCommands(iCommand - 1) = txtInput.Text.Substring(2);
                .ClearAutoCompletes();
                Adventure.Turns += 1;
                .Process(txtInput.Text.Substring(2).Trim);
                btnSubmit.Visible = false;
                btnSubmit.Enabled = true;
            }
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
        GlobalStartup();
        SetBackgroundColour();
        SetInputColour();

        'Dim colBackground As Color = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Background", ColorTranslator.ToOle(Color.Black).ToString)))
        'fRunner.txtOutput.BackColor = colBackground
        'fRunner.pnlTop.BackColor = colBackground
        'fRunner.txtInput.BackColor = colBackground
        'fRunner.pnlBottom.BackColor = colBackground
        'colInput = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text1", ColorTranslator.ToOle(Color.FromArgb(210, 37, 39)).ToString)))
        'colOutput = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text2", ColorTranslator.ToOle(Color.FromArgb(25, 165, 138)).ToString)))

#if Not Mono
        RestoreLayout();
        eStyle = EnumParseViewStyle(GetSetting("ADRIFT", "Generator", "ViewStyle", "Standard"))
        eColour = EnumParseColourScheme(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"))
        Infragistics.Win.Office2007ColorTable.ColorScheme = eColour;
        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(UTMMain.Tools("AutoComplete")).Checked = CBool(GetSetting("ADRIFT", "Generator", "Auto Complete", "true"));
        GetFormPosition(Me, UTMMain, UDMRunner);
#else
        miShowMap.Checked = CBool(GetSetting("ADRIFT", "Runner", "ShowMap", "1"));
        miShowGraphics.Checked = CBool(GetSetting("ADRIFT", "Runner", "ShowGraphics", "1"));
        miAutoComplete.Checked = CBool(GetSetting("ADRIFT", "Generator", "Auto Complete", "true"));
        If miAutoComplete.Checked Then UserSession.bAutoComplete = true ' Doesn't seem to fire on load
        FullScreen(CBool(GetSetting("ADRIFT", "Runner", "FullScreen", "0")));
        GetFormPosition(Me);
#endif
        UserSession.Map.LockMapCentre = SafeBool(GetSetting("ADRIFT", "Runner", "CentreMapLock", "0"));
        UserSession.Map.LockPlayerCentre = SafeBool(GetSetting("ADRIFT", "Runner", "MapPlayerLock", "0"));
        AddPrevious();
        UserSession.LoadMacros();
        UserSession.RunnerStartup();
    }

    private void frmRunner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SaveFormPosition(Me);
        'SaveLayout()

        'If Me.WindowState = FormWindowState.Normal Then
        '    SaveSetting("ADRIFT", "Runner", "Window Height", (Me.Height * 15).ToString)
        '    SaveSetting("ADRIFT", "Runner", "Window Width", (Me.Width * 15).ToString)
        'End If

    }

    private void txtOutput_GotFocus(object sender, System.EventArgs e)
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


    private bool bSettingSelection = false;
    private void txtInput_SelectionChanged(object sender, System.EventArgs e)
    {
        If bSettingSelection Then Exit Sub
        bSettingSelection = True
        if (txtInput.SelectionStart < 2 && txtInput.Text.Length > 1)
        {
            txtInput.SelectionStart = 2;
            if (Adventure IsNot null)
            {
                txtInput.SelectionFont = Adventure.DefaultFont;
            Else
                txtInput.SelectionFont = New Font("Arial", 12);
            }
        }
        bSettingSelection = False
    }


    private void txtInput_TextChanged(System.Object sender, System.EventArgs e)
    {

        'If txtInput.Text = "" Then
        UserSession.BuildAutos();
    }



    'Private Sub UDMRunner_AfterDockChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneEventArgs) Handles UDMRunner.AfterDockChange, UDMRunner.AfterToggleDockState
    '    If Map.ParentForm.Name = Me.Name Then
    '        SetMapButtons(True)
    '        Map.ToolStrip1.Visible = False
    '    Else
    '        SetMapButtons(False)
    '        Map.ToolStrip1.Visible = True
    '    End If
    'End Sub



    'Private Sub UDMRunner_AfterPaneButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneButtonEventArgs) Handles UDMRunner.AfterPaneButtonClick
    '    If e.Button = Infragistics.Win.UltraWinDock.PaneButton.Close Then
    '        If Not Map.Visible Then SetMapButtons(False)
    '    End If
    'End Sub


    'Private Sub UDMRunner_AfterSplitterDrag(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PanesEventArgs) Handles UDMRunner.AfterSplitterDrag
    '    SetMargins()
    'End Sub


    'Private Sub UDMRunner_PaneHidden(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneHiddenEventArgs) Handles UDMRunner.PaneHidden

    '    If e.Pane.Closed Then
    '        If e.Pane.Text = "Graphics" Then
    '            UTMMain.Tools("ShowGraphics").SharedProps.Enabled = True
    '        ElseIf e.Pane.Text = "Map" Then
    '            UTMMain.Tools("ShowMap").SharedProps.Enabled = True
    '            SetMapButtons(False)
    '        End If
    '    End If
    '    SetMargins()

    'End Sub


    private void frmRunner_Resize(object sender, System.EventArgs e)
    {
        SetMargins();
    }


    private bool IsHex(string sHex)
    {
        for (int i = 0; i <= sHex.Length - 1; i++)
        {
            If "0123456789ABCDEF".IndexOf(sHex(i)) = -1 Then Return false
        Next;
        return true;
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
                If bEXE && UserSession.OpenAdventure(Application.ExecutablePath, true) Then Exit Sub
                'End If
            }

            UserSession.Display("ADRIFT Runner Version 5.0<br><>© Campbell Wild 1998-2022<br>Last build: 23rd April 2022 (Release " + SafeInt(Application.ProductVersion.Replace("5.0.", "")).ToString("0") + ")", true) '©;

        }
        catch (Exception ex)
        {
            ErrMsg("Startup Error", ex);
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

    private void tmrSplash_Tick(object sender, System.EventArgs e)
    {
        tmrSplash.Stop();
        If fSplash != null Then fSplash.Close()
    }

    private void txtOutput_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        OutputClicked(txtInput, txtOutput, ctxMenu, MousePosition, btnSubmit, e);
    }

    private void txtOutput_VScroll(object sender, System.EventArgs e)
    {
        ' If we have scrolled the screen right to the bottom, get rid of the button
        if (btnMore.Visible)
        {
            if (txtOutput.GetCharIndexFromPosition(new Point(txtOutput.Width, txtOutput.Height)) >= txtOutput.TextLength - 1)
            {
                btnMore.Visible = false;
            }
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

    Friend WithEvents SplitContainerTextOther As System.Windows.Forms.SplitContainer;
    Friend WithEvents SplitContainerMapGraphics As System.Windows.Forms.SplitContainer;
    Friend WithEvents Description As System.Windows.Forms.StatusBarPanel;
    Friend WithEvents Score As System.Windows.Forms.StatusBarPanel;
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip;
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miMacros As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents WindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miOpenAdventure As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miOpenGame As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miRestartGame As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miSaveGame As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miSaveGameAs As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miStartTranscript As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miRecentAdventures As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miExit As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miFullScreen As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miDebugger As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miOptions As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miAutoComplete As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miEditMacros As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miShowMap As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miShowGraphics As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miAboutADRIFT As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miAboutAdventure As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents UserStatus As System.Windows.Forms.StatusBarPanel;

    'Private Sub UDMRunner_PaneDisplayed(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneDisplayedEventArgs) Handles UDMRunner.PaneDisplayed
    '    Select Case True
    '        Case e.Pane.Control Is Map
    '            SetMapButtons(True)
    '    End Select
    'End Sub

}


}