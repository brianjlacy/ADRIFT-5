using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmOptions
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        LoadForm();

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
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents tabsOptions As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents chkShowExits As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents txtIFID As System.Windows.Forms.TextBox
    Friend WithEvents lnkBabel As System.Windows.Forms.LinkLabel;
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents fileGraphics As ADRIFT.DirectoryBox;
    Friend WithEvents btnSetFont As System.Windows.Forms.Button;
    Private WithEvents txtFont As System.Windows.Forms.TextBox
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents fd As System.Windows.Forms.FontDialog;
    Private WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbForgiveness As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents txtHeadline As System.Windows.Forms.TextBox
    Friend WithEvents lblAuthor As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents cmbGenre As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbPerspective As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents nudVersion As System.Windows.Forms.NumericUpDown;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents optHighestPriorityPassingTask As System.Windows.Forms.RadioButton;
    Friend WithEvents optHighestPriorityTask As System.Windows.Forms.RadioButton;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents chkEnableMenu As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Private WithEvents txtKeyPrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblKeyPrefix As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents txtStatusbar As OOTextbox
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents imgGraphics As ADRIFT.clsImage;
    Friend WithEvents chkDebugger As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents txtWait As ADRIFT.NumericTextbox;
    Friend WithEvents UltraLabel11 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents lblLinkColour As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents pnlLinkColour As System.Windows.Forms.Panel;
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblOutputColour As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents pnlBackgroundColour As System.Windows.Forms.Panel;
    Friend WithEvents lblInputColour As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents pnlOutputColour As System.Windows.Forms.Panel;
    Friend WithEvents pnlInputColour As System.Windows.Forms.Panel;
    Friend WithEvents tabGeneral As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance6 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance7 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance8 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance11 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance14 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem18 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem19 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem20 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance13 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem8 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem9 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem10 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem11 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem12 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem13 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem14 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem15 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem16 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem17 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance16 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem35 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem36 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem37 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem38 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem39 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance10 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance17 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance9 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance12 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance15 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance18 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.tabGeneral = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.UltraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblLinkColour = New Infragistics.Win.Misc.UltraLabel();
        Me.pnlLinkColour = New System.Windows.Forms.Panel();
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel();
        Me.lblOutputColour = New Infragistics.Win.Misc.UltraLabel();
        Me.pnlBackgroundColour = New System.Windows.Forms.Panel();
        Me.lblInputColour = New Infragistics.Win.Misc.UltraLabel();
        Me.pnlOutputColour = New System.Windows.Forms.Panel();
        Me.pnlInputColour = New System.Windows.Forms.Panel();
        Me.txtWait = New ADRIFT.NumericTextbox();
        Me.UltraLabel11 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkDebugger = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.txtStatusbar = New ADRIFT.OOTextbox();
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkEnableMenu = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.cmbPerspective = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel();
        Me.btnSetFont = New System.Windows.Forms.Button();
        Me.txtFont = New System.Windows.Forms.TextBox();
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkShowExits = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.imgGraphics = New ADRIFT.clsImage();
        Me.nudVersion = New System.Windows.Forms.NumericUpDown();
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbGenre = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel();
        Me.txtDescription = New System.Windows.Forms.TextBox();
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbForgiveness = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel();
        Me.txtHeadline = New System.Windows.Forms.TextBox();
        Me.lblAuthor = New Infragistics.Win.Misc.UltraLabel();
        Me.txtAuthor = New System.Windows.Forms.TextBox();
        Me.lblTitle = New Infragistics.Win.Misc.UltraLabel();
        Me.txtTitle = New System.Windows.Forms.TextBox();
        Me.fileGraphics = New ADRIFT.DirectoryBox();
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel();
        Me.lnkBabel = New System.Windows.Forms.LinkLabel();
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel();
        Me.txtIFID = New System.Windows.Forms.TextBox();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.txtKeyPrefix = New System.Windows.Forms.TextBox();
        Me.lblKeyPrefix = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.optHighestPriorityPassingTask = New System.Windows.Forms.RadioButton();
        Me.optHighestPriorityTask = New System.Windows.Forms.RadioButton();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.tabsOptions = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.ofd = New System.Windows.Forms.OpenFileDialog();
        Me.fd = New System.Windows.Forms.FontDialog();
        Me.tabGeneral.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).BeginInit();
        Me.UltraGroupBox2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkDebugger).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkEnableMenu).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPerspective).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowExits).BeginInit();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.nudVersion).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbGenre).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbForgiveness).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsOptions).BeginInit();
        Me.tabsOptions.SuspendLayout();
        Me.SuspendLayout();
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.UltraGroupBox2);
        Me.tabGeneral.Controls.Add(Me.txtWait);
        Me.tabGeneral.Controls.Add(Me.UltraLabel11);
        Me.tabGeneral.Controls.Add(Me.chkDebugger);
        Me.tabGeneral.Controls.Add(Me.txtStatusbar);
        Me.tabGeneral.Controls.Add(Me.UltraLabel10);
        Me.tabGeneral.Controls.Add(Me.chkEnableMenu);
        Me.tabGeneral.Controls.Add(Me.cmbPerspective);
        Me.tabGeneral.Controls.Add(Me.UltraLabel9);
        Me.tabGeneral.Controls.Add(Me.btnSetFont);
        Me.tabGeneral.Controls.Add(Me.txtFont);
        Me.tabGeneral.Controls.Add(Me.UltraLabel4);
        Me.tabGeneral.Controls.Add(Me.chkShowExits);
        Me.tabGeneral.Location = New System.Drawing.Point(1, 23);
        Me.tabGeneral.Name = "tabGeneral";
        Me.tabGeneral.Size = New System.Drawing.Size(524, 562);
        '
        'UltraGroupBox2
        '
        Me.UltraGroupBox2.Controls.Add(Me.lblLinkColour);
        Me.UltraGroupBox2.Controls.Add(Me.pnlLinkColour);
        Me.UltraGroupBox2.Controls.Add(Me.UltraLabel12);
        Me.UltraGroupBox2.Controls.Add(Me.lblOutputColour);
        Me.UltraGroupBox2.Controls.Add(Me.pnlBackgroundColour);
        Me.UltraGroupBox2.Controls.Add(Me.lblInputColour);
        Me.UltraGroupBox2.Controls.Add(Me.pnlOutputColour);
        Me.UltraGroupBox2.Controls.Add(Me.pnlInputColour);
        Me.UltraGroupBox2.Location = New System.Drawing.Point(11, 66);
        Me.UltraGroupBox2.Name = "UltraGroupBox2";
        Me.UltraGroupBox2.Size = New System.Drawing.Size(308, 103);
        Me.UltraGroupBox2.TabIndex = 28;
        Me.UltraGroupBox2.Text = "Default Colours";
        '
        'lblLinkColour
        '
        Appearance5.BackColor = System.Drawing.Color.Transparent;
        Appearance5.TextHAlignAsString = "Right";
        Me.lblLinkColour.Appearance = Appearance5;
        Me.lblLinkColour.Location = New System.Drawing.Point(18, 68);
        Me.lblLinkColour.Name = "lblLinkColour";
        Me.lblLinkColour.Size = New System.Drawing.Size(100, 19);
        Me.lblLinkColour.TabIndex = 26;
        Me.lblLinkColour.Text = "Link colour";
        '
        'pnlLinkColour
        '
        Me.pnlLinkColour.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(75), Integer), (Byte)(CType(215), Integer), (Byte)(CType(188), Integer));
        Me.pnlLinkColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlLinkColour.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.pnlLinkColour.Location = New System.Drawing.Point(124, 62);
        Me.pnlLinkColour.Name = "pnlLinkColour";
        Me.pnlLinkColour.Size = New System.Drawing.Size(25, 25);
        Me.pnlLinkColour.TabIndex = 23;
        '
        'UltraLabel12
        '
        Appearance6.BackColor = System.Drawing.Color.Transparent;
        Appearance6.TextHAlignAsString = "Right";
        Me.UltraLabel12.Appearance = Appearance6;
        Me.UltraLabel12.Location = New System.Drawing.Point(18, 37);
        Me.UltraLabel12.Name = "UltraLabel12";
        Me.UltraLabel12.Size = New System.Drawing.Size(100, 19);
        Me.UltraLabel12.TabIndex = 25;
        Me.UltraLabel12.Text = "Background colour";
        '
        'lblOutputColour
        '
        Appearance7.BackColor = System.Drawing.Color.Transparent;
        Me.lblOutputColour.Appearance = Appearance7;
        Me.lblOutputColour.Location = New System.Drawing.Point(186, 68);
        Me.lblOutputColour.Name = "lblOutputColour";
        Me.lblOutputColour.Size = New System.Drawing.Size(100, 19);
        Me.lblOutputColour.TabIndex = 23;
        Me.lblOutputColour.Text = "Output colour";
        '
        'pnlBackgroundColour
        '
        Me.pnlBackgroundColour.BackColor = System.Drawing.Color.Black;
        Me.pnlBackgroundColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlBackgroundColour.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.pnlBackgroundColour.Location = New System.Drawing.Point(124, 31);
        Me.pnlBackgroundColour.Name = "pnlBackgroundColour";
        Me.pnlBackgroundColour.Size = New System.Drawing.Size(25, 25);
        Me.pnlBackgroundColour.TabIndex = 24;
        '
        'lblInputColour
        '
        Appearance8.BackColor = System.Drawing.Color.Transparent;
        Me.lblInputColour.Appearance = Appearance8;
        Me.lblInputColour.Location = New System.Drawing.Point(186, 37);
        Me.lblInputColour.Name = "lblInputColour";
        Me.lblInputColour.Size = New System.Drawing.Size(100, 19);
        Me.lblInputColour.TabIndex = 21;
        Me.lblInputColour.Text = "Input colour";
        '
        'pnlOutputColour
        '
        Me.pnlOutputColour.BackColor = System.Drawing.Color.SeaGreen;
        Me.pnlOutputColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlOutputColour.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.pnlOutputColour.Location = New System.Drawing.Point(155, 62);
        Me.pnlOutputColour.Name = "pnlOutputColour";
        Me.pnlOutputColour.Size = New System.Drawing.Size(25, 25);
        Me.pnlOutputColour.TabIndex = 22;
        '
        'pnlInputColour
        '
        Me.pnlInputColour.BackColor = System.Drawing.Color.Red;
        Me.pnlInputColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlInputColour.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.pnlInputColour.Location = New System.Drawing.Point(155, 31);
        Me.pnlInputColour.Name = "pnlInputColour";
        Me.pnlInputColour.Size = New System.Drawing.Size(25, 25);
        Me.pnlInputColour.TabIndex = 20;
        '
        'txtWait
        '
        Me.txtWait.Location = New System.Drawing.Point(294, 269);
        Me.txtWait.MaxDecimalPlaces = 0;
        Me.txtWait.MinDecimalPlaces = 0;
        Me.txtWait.Name = "txtWait";
        Me.txtWait.Size = New System.Drawing.Size(59, 22);
        Me.txtWait.TabIndex = 16;
        Me.txtWait.Value = 3.0R;
        '
        'UltraLabel11
        '
        Appearance11.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel11.Appearance = Appearance11;
        Me.UltraLabel11.Location = New System.Drawing.Point(16, 274);
        Me.UltraLabel11.Name = "UltraLabel11";
        Me.UltraLabel11.Size = New System.Drawing.Size(313, 23);
        Me.UltraLabel11.TabIndex = 15;
        Me.UltraLabel11.Text = "How many turns should pass when the player waits:";
        '
        'chkDebugger
        '
        Me.chkDebugger.BackColor = System.Drawing.Color.Transparent;
        Me.chkDebugger.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkDebugger.Location = New System.Drawing.Point(133, 362);
        Me.chkDebugger.Name = "chkDebugger";
        Me.chkDebugger.Size = New System.Drawing.Size(288, 20);
        Me.chkDebugger.TabIndex = 14;
        Me.chkDebugger.Text = "Enable debugger";
        '
        'txtStatusbar
        '
        Me.txtStatusbar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtStatusbar.AutoWordSelection = false;
        Me.txtStatusbar.DetectUrls = false;
        Me.txtStatusbar.EnableAutoDragDrop = false;
        Me.txtStatusbar.Location = New System.Drawing.Point(133, 231);
        Me.txtStatusbar.Multiline = false;
        Me.txtStatusbar.Name = "txtStatusbar";
        Me.txtStatusbar.Padding = New System.Drawing.Size(0, 2);
        Me.txtStatusbar.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
        Me.txtStatusbar.SelectedText = null;
        Me.txtStatusbar.SelectionLength = 0;
        Me.txtStatusbar.SelectionStart = -1;
        Me.txtStatusbar.Size = New System.Drawing.Size(373, 20);
        Me.txtStatusbar.TabIndex = 13;
        Me.txtStatusbar.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
        Me.txtStatusbar.Value = null;
        Me.txtStatusbar.WrapText = false;
        '
        'UltraLabel10
        '
        Appearance14.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel10.Appearance = Appearance14;
        Me.UltraLabel10.Location = New System.Drawing.Point(16, 234);
        Me.UltraLabel10.Name = "UltraLabel10";
        Me.UltraLabel10.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel10.TabIndex = 12;
        Me.UltraLabel10.Text = "Custom Statusbar:";
        '
        'chkEnableMenu
        '
        Me.chkEnableMenu.BackColor = System.Drawing.Color.Transparent;
        Me.chkEnableMenu.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkEnableMenu.Location = New System.Drawing.Point(133, 336);
        Me.chkEnableMenu.Name = "chkEnableMenu";
        Me.chkEnableMenu.Size = New System.Drawing.Size(288, 20);
        Me.chkEnableMenu.TabIndex = 10;
        Me.chkEnableMenu.Text = "Enable context sensitive menu in games";
        '
        'cmbPerspective
        '
        Me.cmbPerspective.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem18.DataValue = 1;
        ValueListItem18.DisplayText = "First Person (e.g. I am...)";
        ValueListItem19.DataValue = 2;
        ValueListItem19.DisplayText = "Second Person (e.g. You are...)";
        ValueListItem20.DataValue = 3;
        ValueListItem20.DisplayText = "Third Person (e.g. Player is...)";
        Me.cmbPerspective.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem18, ValueListItem19, ValueListItem20});
        Me.cmbPerspective.Location = New System.Drawing.Point(133, 189);
        Me.cmbPerspective.Name = "cmbPerspective";
        Me.cmbPerspective.Size = New System.Drawing.Size(196, 21);
        Me.cmbPerspective.TabIndex = 9;
        Me.cmbPerspective.Text = "Second Person (e.g. You are...)";
        '
        'UltraLabel9
        '
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel9.Appearance = Appearance3;
        Me.UltraLabel9.Location = New System.Drawing.Point(16, 193);
        Me.UltraLabel9.Name = "UltraLabel9";
        Me.UltraLabel9.Size = New System.Drawing.Size(119, 23);
        Me.UltraLabel9.TabIndex = 8;
        Me.UltraLabel9.Text = "Player Perspective:";
        '
        'btnSetFont
        '
        Me.btnSetFont.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnSetFont.Location = New System.Drawing.Point(431, 28);
        Me.btnSetFont.Name = "btnSetFont";
        Me.btnSetFont.Size = New System.Drawing.Size(75, 23);
        Me.btnSetFont.TabIndex = 7;
        Me.btnSetFont.Text = "Set Font";
        Me.btnSetFont.UseVisualStyleBackColor = true;
        '
        'txtFont
        '
        Me.txtFont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtFont.Location = New System.Drawing.Point(133, 30);
        Me.txtFont.Name = "txtFont";
        Me.txtFont.Size = New System.Drawing.Size(298, 20);
        Me.txtFont.TabIndex = 6;
        '
        'UltraLabel4
        '
        Appearance13.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel4.Appearance = Appearance13;
        Me.UltraLabel4.Location = New System.Drawing.Point(16, 33);
        Me.UltraLabel4.Name = "UltraLabel4";
        Me.UltraLabel4.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel4.TabIndex = 5;
        Me.UltraLabel4.Text = "Default font:";
        '
        'chkShowExits
        '
        Me.chkShowExits.BackColor = System.Drawing.Color.Transparent;
        Me.chkShowExits.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkShowExits.Location = New System.Drawing.Point(133, 310);
        Me.chkShowExits.Name = "chkShowExits";
        Me.chkShowExits.Size = New System.Drawing.Size(288, 20);
        Me.chkShowExits.TabIndex = 4;
        Me.chkShowExits.Text = "Show exits from locations along with descriptions";
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.imgGraphics);
        Me.UltraTabPageControl1.Controls.Add(Me.nudVersion);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel2);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbGenre);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel8);
        Me.UltraTabPageControl1.Controls.Add(Me.txtDescription);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel7);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbForgiveness);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel6);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel5);
        Me.UltraTabPageControl1.Controls.Add(Me.txtHeadline);
        Me.UltraTabPageControl1.Controls.Add(Me.lblAuthor);
        Me.UltraTabPageControl1.Controls.Add(Me.txtAuthor);
        Me.UltraTabPageControl1.Controls.Add(Me.lblTitle);
        Me.UltraTabPageControl1.Controls.Add(Me.txtTitle);
        Me.UltraTabPageControl1.Controls.Add(Me.fileGraphics);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel3);
        Me.UltraTabPageControl1.Controls.Add(Me.lnkBabel);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel1);
        Me.UltraTabPageControl1.Controls.Add(Me.txtIFID);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(524, 562);
        '
        'imgGraphics
        '
        Me.imgGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.imgGraphics.BackColour = System.Drawing.Color.Transparent;
        Me.imgGraphics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.imgGraphics.Image = null;
        Me.imgGraphics.Location = New System.Drawing.Point(127, 314);
        Me.imgGraphics.Name = "imgGraphics";
        Me.imgGraphics.Size = New System.Drawing.Size(240, 240);
        Me.imgGraphics.SizeMode = ADRIFT.clsImage.SizeModeEnum.ActualSizeCentred;
        Me.imgGraphics.TabIndex = 29;
        '
        'nudVersion
        '
        Me.nudVersion.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.nudVersion.Location = New System.Drawing.Point(430, 53);
        Me.nudVersion.Name = "nudVersion";
        Me.nudVersion.Size = New System.Drawing.Size(49, 20);
        Me.nudVersion.TabIndex = 28;
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Appearance = Appearance2;
        Me.UltraLabel2.Location = New System.Drawing.Point(379, 56);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel2.TabIndex = 27;
        Me.UltraLabel2.Text = "Version:";
        '
        'cmbGenre
        '
        ValueListItem1.DataValue = "Children's Fiction";
        ValueListItem2.DataValue = "Collegiate Fiction";
        ValueListItem3.DataValue = "Comedy";
        ValueListItem4.DataValue = "Erotica";
        ValueListItem5.DataValue = "Fairy Tale";
        ValueListItem6.DataValue = "Fantasy";
        ValueListItem7.DataValue = "Fiction";
        ValueListItem8.DataValue = "Historical";
        ValueListItem9.DataValue = "Horror";
        ValueListItem10.DataValue = "Mystery";
        ValueListItem11.DataValue = "Non-Fiction";
        ValueListItem12.DataValue = "Other";
        ValueListItem13.DataValue = "Religious Fiction";
        ValueListItem14.DataValue = "Romance";
        ValueListItem15.DataValue = "Science Fiction";
        ValueListItem16.DataValue = "Surreal";
        ValueListItem17.DataValue = "Western";
        Me.cmbGenre.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7, ValueListItem8, ValueListItem9, ValueListItem10, ValueListItem11, ValueListItem12, ValueListItem13, ValueListItem14, ValueListItem15, ValueListItem16, ValueListItem17});
        Me.cmbGenre.Location = New System.Drawing.Point(359, 157);
        Me.cmbGenre.Name = "cmbGenre";
        Me.cmbGenre.Size = New System.Drawing.Size(120, 21);
        Me.cmbGenre.TabIndex = 26;
        Me.cmbGenre.Text = "Fiction";
        '
        'UltraLabel8
        '
        Appearance4.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel8.Appearance = Appearance4;
        Me.UltraLabel8.Location = New System.Drawing.Point(312, 161);
        Me.UltraLabel8.Name = "UltraLabel8";
        Me.UltraLabel8.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel8.TabIndex = 25;
        Me.UltraLabel8.Text = "Genre:";
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDescription.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtDescription.Location = New System.Drawing.Point(127, 184);
        Me.txtDescription.Multiline = true;
        Me.txtDescription.Name = "txtDescription";
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        Me.txtDescription.Size = New System.Drawing.Size(352, 96);
        Me.txtDescription.TabIndex = 24;
        '
        'UltraLabel7
        '
        Appearance16.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel7.Appearance = Appearance16;
        Me.UltraLabel7.Location = New System.Drawing.Point(21, 190);
        Me.UltraLabel7.Name = "UltraLabel7";
        Me.UltraLabel7.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel7.TabIndex = 23;
        Me.UltraLabel7.Text = "Description:";
        '
        'cmbForgiveness
        '
        Me.cmbForgiveness.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem35.DataValue = "Merciful";
        ValueListItem36.DataValue = "Polite";
        ValueListItem37.DataValue = "Tough";
        ValueListItem38.DataValue = "Nasty";
        ValueListItem39.DataValue = "Cruel";
        Me.cmbForgiveness.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem35, ValueListItem36, ValueListItem37, ValueListItem38, ValueListItem39});
        Me.cmbForgiveness.Location = New System.Drawing.Point(127, 157);
        Me.cmbForgiveness.Name = "cmbForgiveness";
        Me.cmbForgiveness.Size = New System.Drawing.Size(120, 21);
        Me.cmbForgiveness.TabIndex = 22;
        Me.cmbForgiveness.Text = "Merciful";
        '
        'UltraLabel6
        '
        Appearance10.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel6.Appearance = Appearance10;
        Me.UltraLabel6.Location = New System.Drawing.Point(21, 161);
        Me.UltraLabel6.Name = "UltraLabel6";
        Me.UltraLabel6.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel6.TabIndex = 21;
        Me.UltraLabel6.Text = "Forgiveness:";
        '
        'UltraLabel5
        '
        Appearance17.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel5.Appearance = Appearance17;
        Me.UltraLabel5.Location = New System.Drawing.Point(21, 134);
        Me.UltraLabel5.Name = "UltraLabel5";
        Me.UltraLabel5.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel5.TabIndex = 20;
        Me.UltraLabel5.Text = "Headline:";
        '
        'txtHeadline
        '
        Me.txtHeadline.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtHeadline.Location = New System.Drawing.Point(127, 131);
        Me.txtHeadline.Name = "txtHeadline";
        Me.txtHeadline.Size = New System.Drawing.Size(352, 20);
        Me.txtHeadline.TabIndex = 19;
        '
        'lblAuthor
        '
        Appearance9.BackColor = System.Drawing.Color.Transparent;
        Me.lblAuthor.Appearance = Appearance9;
        Me.lblAuthor.Location = New System.Drawing.Point(21, 82);
        Me.lblAuthor.Name = "lblAuthor";
        Me.lblAuthor.Size = New System.Drawing.Size(100, 23);
        Me.lblAuthor.TabIndex = 18;
        Me.lblAuthor.Text = "Author:";
        '
        'txtAuthor
        '
        Me.txtAuthor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtAuthor.Location = New System.Drawing.Point(127, 79);
        Me.txtAuthor.Name = "txtAuthor";
        Me.txtAuthor.Size = New System.Drawing.Size(352, 20);
        Me.txtAuthor.TabIndex = 17;
        '
        'lblTitle
        '
        Appearance12.BackColor = System.Drawing.Color.Transparent;
        Me.lblTitle.Appearance = Appearance12;
        Me.lblTitle.Location = New System.Drawing.Point(21, 56);
        Me.lblTitle.Name = "lblTitle";
        Me.lblTitle.Size = New System.Drawing.Size(100, 23);
        Me.lblTitle.TabIndex = 16;
        Me.lblTitle.Text = "Title:";
        '
        'txtTitle
        '
        Me.txtTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtTitle.Location = New System.Drawing.Point(127, 53);
        Me.txtTitle.Name = "txtTitle";
        Me.txtTitle.Size = New System.Drawing.Size(229, 20);
        Me.txtTitle.TabIndex = 15;
        '
        'fileGraphics
        '
        Me.fileGraphics.AllowDrop = true;
        Me.fileGraphics.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.fileGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.fileGraphics.BoxType = ADRIFT.DirectoryBox.BoxTypeEnum.File;
        Me.fileGraphics.Directory = "*** Incorrect BoxType ***";
        Me.fileGraphics.FileFilter = "Image Files|*.jpg;*.png|All Files (*.*)|*.*";
        Me.fileGraphics.Filename = "";
        Me.fileGraphics.Location = New System.Drawing.Point(127, 286);
        Me.fileGraphics.Name = "fileGraphics";
        Me.fileGraphics.OpenOrSave = ADRIFT.DirectoryBox.OpenOrSaveEnum.Open;
        Me.fileGraphics.Size = New System.Drawing.Size(384, 22);
        Me.fileGraphics.TabIndex = 13;
        '
        'UltraLabel3
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel3.Appearance = Appearance1;
        Me.UltraLabel3.Location = New System.Drawing.Point(21, 290);
        Me.UltraLabel3.Name = "UltraLabel3";
        Me.UltraLabel3.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel3.TabIndex = 12;
        Me.UltraLabel3.Text = "Cover image:";
        '
        'lnkBabel
        '
        Me.lnkBabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lnkBabel.BackColor = System.Drawing.Color.Transparent;
        Me.lnkBabel.LinkArea = New System.Windows.Forms.LinkArea(86, 26);
        Me.lnkBabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        Me.lnkBabel.Location = New System.Drawing.Point(11, 12);
        Me.lnkBabel.Name = "lnkBabel";
        Me.lnkBabel.Size = New System.Drawing.Size(504, 29);
        Me.lnkBabel.TabIndex = 11;
        Me.lnkBabel.TabStop = true;
        Me.lnkBabel.Text = "This page allows you to populate data according to The Treaty of Babel.  Please v" + _;
    "isit http://babel.ifarchive.org  for more information.";
        Me.lnkBabel.UseCompatibleTextRendering = true;
        '
        'UltraLabel1
        '
        Appearance15.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Appearance = Appearance15;
        Me.UltraLabel1.Location = New System.Drawing.Point(21, 108);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel1.TabIndex = 8;
        Me.UltraLabel1.Text = "IFID:";
        '
        'txtIFID
        '
        Me.txtIFID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtIFID.Enabled = false;
        Me.txtIFID.Location = New System.Drawing.Point(127, 105);
        Me.txtIFID.Name = "txtIFID";
        Me.txtIFID.Size = New System.Drawing.Size(352, 20);
        Me.txtIFID.TabIndex = 7;
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.txtKeyPrefix);
        Me.UltraTabPageControl2.Controls.Add(Me.lblKeyPrefix);
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGroupBox1);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(524, 562);
        '
        'txtKeyPrefix
        '
        Me.txtKeyPrefix.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtKeyPrefix.Location = New System.Drawing.Point(148, 171);
        Me.txtKeyPrefix.Name = "txtKeyPrefix";
        Me.txtKeyPrefix.Size = New System.Drawing.Size(203, 20);
        Me.txtKeyPrefix.TabIndex = 17;
        '
        'lblKeyPrefix
        '
        Appearance18.BackColor = System.Drawing.Color.Transparent;
        Me.lblKeyPrefix.Appearance = Appearance18;
        Me.lblKeyPrefix.Location = New System.Drawing.Point(42, 174);
        Me.lblKeyPrefix.Name = "lblKeyPrefix";
        Me.lblKeyPrefix.Size = New System.Drawing.Size(100, 23);
        Me.lblKeyPrefix.TabIndex = 18;
        Me.lblKeyPrefix.Text = "Key Prefix:";
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraGroupBox1.Controls.Add(Me.optHighestPriorityPassingTask);
        Me.UltraGroupBox1.Controls.Add(Me.optHighestPriorityTask);
        Me.UltraGroupBox1.Location = New System.Drawing.Point(16, 18);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(495, 135);
        Me.UltraGroupBox1.TabIndex = 11;
        Me.UltraGroupBox1.Text = "Task Execution logic";
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'optHighestPriorityPassingTask
        '
        Me.optHighestPriorityPassingTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.optHighestPriorityPassingTask.BackColor = System.Drawing.Color.Transparent;
        Me.optHighestPriorityPassingTask.Location = New System.Drawing.Point(26, 59);
        Me.optHighestPriorityPassingTask.Name = "optHighestPriorityPassingTask";
        Me.optHighestPriorityPassingTask.Size = New System.Drawing.Size(458, 62);
        Me.optHighestPriorityPassingTask.TabIndex = 1;
        Me.optHighestPriorityPassingTask.TabStop = true;
        Me.optHighestPriorityPassingTask.Text = "Execute the highest priority task matching command input that passes restrictions" + _;
    ".  If none are found, execute the highest priority task matching command input t" + _;
    "hat fails restrictions.";
        Me.optHighestPriorityPassingTask.UseVisualStyleBackColor = false;
        '
        'optHighestPriorityTask
        '
        Me.optHighestPriorityTask.AutoSize = true;
        Me.optHighestPriorityTask.BackColor = System.Drawing.Color.Transparent;
        Me.optHighestPriorityTask.Location = New System.Drawing.Point(26, 36);
        Me.optHighestPriorityTask.Name = "optHighestPriorityTask";
        Me.optHighestPriorityTask.Size = New System.Drawing.Size(417, 17);
        Me.optHighestPriorityTask.TabIndex = 0;
        Me.optHighestPriorityTask.TabStop = true;
        Me.optHighestPriorityTask.Text = "Execute the highest priority task matching command input, whether it passes or no" + _;
    "t.";
        Me.optHighestPriorityTask.UseVisualStyleBackColor = false;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(424, 594);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 18;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(328, 594);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 17;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(232, 594);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 16;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 588);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(528, 48);
        Me.UltraStatusBar1.TabIndex = 15;
        '
        'tabsOptions
        '
        Me.tabsOptions.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsOptions.Controls.Add(Me.tabGeneral);
        Me.tabsOptions.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsOptions.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsOptions.Location = New System.Drawing.Point(0, 0);
        Me.tabsOptions.Name = "tabsOptions";
        Me.tabsOptions.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsOptions.Size = New System.Drawing.Size(528, 588);
        Me.tabsOptions.TabIndex = 19;
        UltraTab1.Key = "General";
        UltraTab1.TabPage = Me.tabGeneral;
        UltraTab1.Text = "General";
        UltraTab2.Key = "Bibliography";
        UltraTab2.TabPage = Me.UltraTabPageControl1;
        UltraTab2.Text = "Bibliography";
        UltraTab3.Key = "Advanced";
        UltraTab3.TabPage = Me.UltraTabPageControl2;
        UltraTab3.Text = "Advanced";
        Me.tabsOptions.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(524, 562);
        '
        'frmOptions
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(528, 636);
        Me.Controls.Add(Me.tabsOptions);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.MinimumSize = New System.Drawing.Size(544, 672);
        Me.Name = "frmOptions";
        Me.ShowInTaskbar = false;
        Me.Text = "Options";
        Me.tabGeneral.ResumeLayout(false);
        Me.tabGeneral.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).EndInit();
        Me.UltraGroupBox2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkDebugger).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkEnableMenu).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPerspective).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowExits).EndInit();
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.nudVersion).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbGenre).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbForgiveness).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.UltraTabPageControl2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.UltraGroupBox1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsOptions).EndInit();
        Me.tabsOptions.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private bool bChanged;


    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            if (bChanged)
            {
                btnApply.Enabled = true;
            Else
                btnApply.Enabled = false;
            }
        }
    }


    private void LoadForm()
    {

        If Adventure.Player != null Then SetCombo(cmbPerspective, CInt(Adventure.Player.Perspective))
        txtTitle.Text = Adventure.Title;
        txtAuthor.Text = Adventure.Author;
        txtFont.Text = Adventure.DefaultFontName + ", " + Adventure.DefaultFontSize;
        txtStatusbar.Text = Adventure.sUserStatus;
        pnlBackgroundColour.BackColor = Adventure.DeveloperDefaultBackgroundColour;
        pnlInputColour.BackColor = Adventure.DeveloperDefaultInputColour;
        pnlOutputColour.BackColor = Adventure.DeveloperDefaultOutputColour;
        pnlLinkColour.BackColor = Adventure.DeveloperDefaultLinkColour;

        With Adventure.BabelTreatyInfo.Stories(0);
            txtIFID.Text = .Identification.IFID;
            if (.Cover IsNot null)
            {
                imgGraphics.Image = .Cover.imgCoverArt;
                fileGraphics.Filename = Adventure.CoverFilename ' .Cover.Filename;
            }
            if (.Bibliographic IsNot null)
            {
                txtTitle.Text = .Bibliographic.Title;
                txtAuthor.Text = .Bibliographic.Author;
                txtHeadline.Text = .Bibliographic.Headline;
                SetCombo(cmbGenre, .Bibliographic.Genre);
                If cmbGenre.Text <> .Bibliographic.Genre Then cmbGenre.Text = .Bibliographic.Genre
                txtDescription.Text = SafeString(.Bibliographic.Description).Replace("<br>", vbCrLf);
                SetCombo(cmbForgiveness, .Bibliographic.Forgiveness.ToString);
            }
            if (.Releases IsNot null && .Releases.Attached IsNot null && .Releases.Attached.Release IsNot null)
            {
                nudVersion.Value = .Releases.Attached.Release.Version;
            }
        }
        if (Adventure.TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityTask)
        {
            optHighestPriorityTask.Checked = true;
        Else
            optHighestPriorityPassingTask.Checked = true;
        }

        chkShowExits.Checked = Adventure.ShowExits;
        chkEnableMenu.Checked = Adventure.EnableMenu;
        chkDebugger.Checked = Adventure.EnableDebugger;

        txtWait.Value = Adventure.WaitTurns;

        tabsOptions.Tabs("Advanced").Visible = ! fGenerator.SimpleMode;
        if (Adventure.KeyPrefix <> "")
        {
            txtKeyPrefix.Text = Adventure.KeyPrefix;
        Else
            txtKeyPrefix.Text = fGenerator.KeyPrefix;
        }

        Changed = False
        Me.Show();

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyOptions();
        CloseOptions(Me);
    }


    private void ApplyOptions()
    {

        Adventure.Title = txtTitle.Text;
        Adventure.Author = txtAuthor.Text;
        Adventure.ShowExits = chkShowExits.Checked;
        Adventure.EnableMenu = chkEnableMenu.Checked;
        Adventure.EnableDebugger = chkDebugger.Checked;
        Adventure.WaitTurns = CInt(txtWait.Value);

        If Adventure.Player != null Then Adventure.Player.Perspective = (PerspectiveEnum)(cmbPerspective.SelectedItem.DataValue)
        Adventure.sUserStatus = txtStatusbar.Text;
        if (txtFont.Text.Contains(","))
        {
            Adventure.DefaultFontName = txtFont.Text.Split(","c)(0);
            Adventure.DefaultFontSize = SafeInt(txtFont.Text.Split(","c)(1));
        }
        Adventure.DeveloperDefaultBackgroundColour = pnlBackgroundColour.BackColor;
        Adventure.DeveloperDefaultInputColour = pnlInputColour.BackColor;
        Adventure.DeveloperDefaultOutputColour = pnlOutputColour.BackColor;
        Adventure.DeveloperDefaultLinkColour = pnlLinkColour.BackColor;

        With Adventure.BabelTreatyInfo.Stories(0);
            Adventure.CoverFilename = fileGraphics.Filename;
            '.Cover = New clsBabelTreatyInfo.clsStory.clsCover
            'With .Cover

            '    .imgCoverArt = imgGraphics.Image
            'End With
            With .Bibliographic;
                .Title = txtTitle.Text;
                .Author = txtAuthor.Text;
                .Headline = txtHeadline.Text;
                .Genre = cmbGenre.Text 'SelectedItem.DataValue.ToString;
                .Description = txtDescription.Text.Replace(vbCrLf, "<br>");
                .Forgiveness = (cmbForgiveness.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsBabelTreatyInfo.clsStory.clsBibliographic.ForgivenessEnum)), clsBabelTreatyInfo.clsStory.clsBibliographic.ForgivenessEnum);
            }
            With .Releases.Attached.Release;
                .Version = CInt(nudVersion.Value);
            }
        }
        if (optHighestPriorityTask.Checked)
        {
            Adventure.TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityTask;
        Else
            Adventure.TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityPassingTask;
        }

        tabsOptions.Tabs("Advanced").Visible = ! fGenerator.SimpleMode;
        fGenerator.KeyPrefix = txtKeyPrefix.Text;
        Adventure.KeyPrefix = txtKeyPrefix.Text;

        For Each f As Form In colAllForms
            SetWindowStyle(f, fGenerator.UTMMain, fGenerator.UDMGenerator);
        Next;

        Adventure.Changed = true;

    }


    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyOptions();
        Changed = False
    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyOptions()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseOptions(Me);
    }


    private void frmOptions_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        fGenerator.fOptions = null;
    }



    private void frmOptions_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgOptions16.GetHicon);
        GetFormPosition(Me);
        imgGraphics.AllowDrop = true;
        fileGraphics.AllowDrop = true;
    }



    private void lnkBabel_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("http://babel.ifarchive.org/");
    }

    private void fileGraphics_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
        private string sFilename = String.Empty;
        if (GetFilename(sFilename, e))
        {
            fileGraphics.Filename = sFilename;
        }
    }

    private void fileGraphics_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {
        private string sFilename = String.Empty;
        if (GetFilename(sFilename, e))
        {
            e.Effect = DragDropEffects.Copy;
        Else
            e.Effect = DragDropEffects.None;
        }
    }

    private bool GetFilename(ref filename As String, DragEventArgs e)
    {

        try
        {
            private bool ret = false;
            filename = String.Empty

            if (((e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy))
            {
                filename = CStr((e.Data).GetData("System.String")) ' FileName into Array...?

                private string ext = IO.Path.GetExtension(filename).ToLower;
                switch (ext)
                {
                    case ".jpg":
                    case ".png":
                        {
                        return true;
                    default:
                        {
                        return false;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }

    }


    private void fileGraphics_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            if (fileGraphics.Filename <> "")
            {
                if (IO.File.Exists(fileGraphics.Filename) || fileGraphics.Filename.ToLower.StartsWith("http"))
                {
                    fileGraphics.txtDir.ForeColor = Color.DarkRed;
                    imgGraphics.Load(fileGraphics.Filename) ' Loads either from file or from URL;
                    fileGraphics.txtDir.ForeColor = SystemColors.ControlText;
                }
            }

        }
        catch (System.Net.WebException exWeb)
        {
            imgGraphics.Image = null;
        }
        catch (IO.FileNotFoundException exFNF)
        {
            imgGraphics.Image = null;
        }
        catch (IO.IOException exIO)
        {
            imgGraphics.Image = null;
        }
        catch (Exception ex)
        {
            ErrMsg("Load Graphics error", ex);
        }
    }



    private void frmOptions_Resize(object sender, System.EventArgs e)
    {

        private int iHeight = Height - 440;
        private int iWidth = Width - 190 ' 296;

        iHeight = Math.Min(iHeight, iWidth)
        iWidth = Math.Min(iHeight, iWidth)

        imgGraphics.Size = New Size(iHeight, iWidth);

    }


    private void txtDescription_GotFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = null;
    }


    private void txtDescription_LostFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = btnOK;
    }



    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }


    private void btnSetFont_Click(System.Object sender, System.EventArgs e)
    {

        With fd;
            .FontMustExist = false;
            .MinSize = 8;
            .MaxSize = 36;

            if (txtFont.Text <> "")
            {
                private string sName = "";
                private float emSize = 0;

                Dim sFont() As String = txtFont.Text.Split(","c)
                If sFont(0) <> "" Then sName = sFont(0)

                if (sFont(1) <> "")
                {
                    If SafeDbl(sFont(1)) > 0 Then emSize = CSng(SafeDbl(sFont(1)))
                }
                if (sName <> "" && emSize > 0)
                {
                    private New Font(sName, emSize) f;
                    .Font = f;
                }
            }
            if (.ShowDialog = Windows.Forms.DialogResult.OK)
            {
                Dim sFontInfo() As String = .Font.ToString.Split(","c)
                txtFont.Text = sFontInfo(0).Replace("[Font: Name=", "") + ", " + SafeInt(sFontInfo(1).Replace(" Size=", ""));
            }
        }
    }

    private void pnlBackgroundColour_Click(object sender, System.EventArgs e)
    {

        private New ColorDialog dlgColour;
        dlgColour.Color = (Panel)(sender).BackColor;

        if (dlgColour.ShowDialog = Windows.Forms.DialogResult.OK)
        {
            (Panel)(sender).BackColor = dlgColour.Color;
            Changed = True
        }

    }

}

}