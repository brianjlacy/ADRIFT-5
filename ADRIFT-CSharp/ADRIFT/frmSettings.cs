using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmSettings
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
    Friend WithEvents _lblInfo As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents lvwLibraries As System.Windows.Forms.ListView;
    Friend WithEvents btnRemove As Infragistics.Win.Misc.UltraButton;
    Public WithEvents Library As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabLibraries As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents tabsOptions As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents tabGeneral As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents cmbViewStyle As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents lblTheme As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents chkHideLibraryItems As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbDictionary As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblDictionary As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents dbUserDictionary As ADRIFT.DirectoryBox;
    Friend WithEvents dbDictionary As ADRIFT.DirectoryBox;
    Friend WithEvents chkShowInTaskbar As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbReciprocalLink As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents cmbReciprocalRestriction As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbColour As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents cmbOverwriteLibraries As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbReplaceNames As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents chkSimpleMode As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkAutoComplete As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkAudio As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkGraphics As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkPreview As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkVersion As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents cmbDefaultTaskActions As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraGroupBox4 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraGroupBox3 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraLabel14 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblLibraryDescription As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents chkKeyNames As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkShowKeys As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem8 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem9 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem21 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem22 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem23 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem26 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem27 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem28 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem29 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem24 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem25 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem11 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem17 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem12 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem13 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.tabGeneral = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.UltraGroupBox4 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.chkGraphics = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkPreview = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkAudio = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.UltraGroupBox3 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbReciprocalLink = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbReciprocalRestriction = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.cmbReplaceNames = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbDictionary = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.lblDictionary = New Infragistics.Win.Misc.UltraLabel();
        Me.dbDictionary = New ADRIFT.DirectoryBox();
        Me.dbUserDictionary = New ADRIFT.DirectoryBox();
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblTheme = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbViewStyle = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.chkShowKeys = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkShowInTaskbar = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.cmbColour = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel14 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkSimpleMode = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkAutoComplete = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbDefaultTaskActions = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkVersion = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.tabLibraries = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblLibraryDescription = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbOverwriteLibraries = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkHideLibraryItems = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.btnDown = New Infragistics.Win.Misc.UltraButton();
        Me.btnUp = New Infragistics.Win.Misc.UltraButton();
        Me.btnRemove = New Infragistics.Win.Misc.UltraButton();
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton();
        Me._lblInfo = New Infragistics.Win.Misc.UltraLabel();
        Me.lvwLibraries = New System.Windows.Forms.ListView();
        Me.Library = (System.Windows.Forms.ColumnHeader)(New System.Windows.Forms.ColumnHeader());
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.chkKeyNames = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.tabsOptions = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.ofd = New System.Windows.Forms.OpenFileDialog();
        Me.tabGeneral.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox4).BeginInit();
        Me.UltraGroupBox4.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkGraphics).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkPreview).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAudio).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox3).BeginInit();
        Me.UltraGroupBox3.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReciprocalLink).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReciprocalRestriction).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReplaceNames).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).BeginInit();
        Me.UltraGroupBox2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbDictionary).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbViewStyle).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowKeys).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowInTaskbar).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbColour).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkSimpleMode).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAutoComplete).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbDefaultTaskActions).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkVersion).BeginInit();
        Me.tabLibraries.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbOverwriteLibraries).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkHideLibraryItems).BeginInit();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkKeyNames).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsOptions).BeginInit();
        Me.tabsOptions.SuspendLayout();
        Me.SuspendLayout();
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.UltraGroupBox4);
        Me.tabGeneral.Controls.Add(Me.UltraGroupBox3);
        Me.tabGeneral.Controls.Add(Me.UltraGroupBox2);
        Me.tabGeneral.Controls.Add(Me.UltraGroupBox1);
        Me.tabGeneral.Controls.Add(Me.cmbDefaultTaskActions);
        Me.tabGeneral.Controls.Add(Me.UltraLabel7);
        Me.tabGeneral.Controls.Add(Me.chkVersion);
        Me.tabGeneral.Location = New System.Drawing.Point(1, 22);
        Me.tabGeneral.Name = "tabGeneral";
        Me.tabGeneral.Size = New System.Drawing.Size(568, 458);
        '
        'UltraGroupBox4
        '
        Me.UltraGroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraGroupBox4.Controls.Add(Me.chkGraphics);
        Me.UltraGroupBox4.Controls.Add(Me.chkPreview);
        Me.UltraGroupBox4.Controls.Add(Me.chkAudio);
        Me.UltraGroupBox4.Location = New System.Drawing.Point(326, 290);
        Me.UltraGroupBox4.Name = "UltraGroupBox4";
        Me.UltraGroupBox4.Size = New System.Drawing.Size(233, 116);
        Me.UltraGroupBox4.TabIndex = 46;
        Me.UltraGroupBox4.Text = "Text Boxes";
        Me.UltraGroupBox4.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'chkGraphics
        '
        Me.chkGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.chkGraphics.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkGraphics.Location = New System.Drawing.Point(31, 29);
        Me.chkGraphics.Name = "chkGraphics";
        Me.chkGraphics.Size = New System.Drawing.Size(109, 20);
        Me.chkGraphics.TabIndex = 39;
        Me.chkGraphics.Text = "Enable Graphics";
        '
        'chkPreview
        '
        Me.chkPreview.BackColor = System.Drawing.Color.Transparent;
        Me.chkPreview.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkPreview.Location = New System.Drawing.Point(31, 81);
        Me.chkPreview.Name = "chkPreview";
        Me.chkPreview.Size = New System.Drawing.Size(109, 20);
        Me.chkPreview.TabIndex = 41;
        Me.chkPreview.Text = "Enable Preview";
        '
        'chkAudio
        '
        Me.chkAudio.BackColor = System.Drawing.Color.Transparent;
        Me.chkAudio.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkAudio.Location = New System.Drawing.Point(31, 55);
        Me.chkAudio.Name = "chkAudio";
        Me.chkAudio.Size = New System.Drawing.Size(109, 20);
        Me.chkAudio.TabIndex = 40;
        Me.chkAudio.Text = "Enable Audio";
        '
        'UltraGroupBox3
        '
        Me.UltraGroupBox3.Controls.Add(Me.UltraLabel3);
        Me.UltraGroupBox3.Controls.Add(Me.cmbReciprocalLink);
        Me.UltraGroupBox3.Controls.Add(Me.UltraLabel4);
        Me.UltraGroupBox3.Controls.Add(Me.cmbReciprocalRestriction);
        Me.UltraGroupBox3.Controls.Add(Me.cmbReplaceNames);
        Me.UltraGroupBox3.Controls.Add(Me.UltraLabel6);
        Me.UltraGroupBox3.Location = New System.Drawing.Point(11, 289);
        Me.UltraGroupBox3.Name = "UltraGroupBox3";
        Me.UltraGroupBox3.Size = New System.Drawing.Size(309, 117);
        Me.UltraGroupBox3.TabIndex = 30;
        Me.UltraGroupBox3.Text = "Prompts";
        Me.UltraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'UltraLabel3
        '
        Me.UltraLabel3.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel3.Location = New System.Drawing.Point(20, 29);
        Me.UltraLabel3.Name = "UltraLabel3";
        Me.UltraLabel3.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel3.TabIndex = 32;
        Me.UltraLabel3.Text = "Copy reciprocal links:";
        '
        'cmbReciprocalLink
        '
        Me.cmbReciprocalLink.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem7.DataValue = 0;
        ValueListItem7.DisplayText = "Prompt";
        ValueListItem8.DataValue = 6;
        ValueListItem8.DisplayText = "Always";
        ValueListItem9.DataValue = 7;
        ValueListItem9.DisplayText = "Never";
        Me.cmbReciprocalLink.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem7, ValueListItem8, ValueListItem9});
        Me.cmbReciprocalLink.Location = New System.Drawing.Point(185, 25);
        Me.cmbReciprocalLink.Name = "cmbReciprocalLink";
        Me.cmbReciprocalLink.Size = New System.Drawing.Size(80, 21);
        Me.cmbReciprocalLink.TabIndex = 31;
        Me.cmbReciprocalLink.Text = "Prompt";
        '
        'UltraLabel4
        '
        Me.UltraLabel4.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel4.Location = New System.Drawing.Point(20, 57);
        Me.UltraLabel4.Name = "UltraLabel4";
        Me.UltraLabel4.Size = New System.Drawing.Size(153, 16);
        Me.UltraLabel4.TabIndex = 34;
        Me.UltraLabel4.Text = "Copy reciprocal restrictions:";
        '
        'cmbReciprocalRestriction
        '
        Me.cmbReciprocalRestriction.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem4.DataValue = 0;
        ValueListItem4.DisplayText = "Prompt";
        ValueListItem5.DataValue = 6;
        ValueListItem5.DisplayText = "Always";
        ValueListItem6.DataValue = 7;
        ValueListItem6.DisplayText = "Never";
        Me.cmbReciprocalRestriction.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem4, ValueListItem5, ValueListItem6});
        Me.cmbReciprocalRestriction.Location = New System.Drawing.Point(185, 53);
        Me.cmbReciprocalRestriction.Name = "cmbReciprocalRestriction";
        Me.cmbReciprocalRestriction.Size = New System.Drawing.Size(80, 21);
        Me.cmbReciprocalRestriction.TabIndex = 33;
        Me.cmbReciprocalRestriction.Text = "Prompt";
        '
        'cmbReplaceNames
        '
        Me.cmbReplaceNames.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem21.DataValue = 0;
        ValueListItem21.DisplayText = "Prompt";
        ValueListItem22.DataValue = 6;
        ValueListItem22.DisplayText = "Always";
        ValueListItem23.DataValue = 7;
        ValueListItem23.DisplayText = "Never";
        Me.cmbReplaceNames.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem21, ValueListItem22, ValueListItem23});
        Me.cmbReplaceNames.Location = New System.Drawing.Point(185, 80);
        Me.cmbReplaceNames.Name = "cmbReplaceNames";
        Me.cmbReplaceNames.Size = New System.Drawing.Size(80, 21);
        Me.cmbReplaceNames.TabIndex = 36;
        Me.cmbReplaceNames.Text = "Prompt";
        '
        'UltraLabel6
        '
        Me.UltraLabel6.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel6.Location = New System.Drawing.Point(20, 84);
        Me.UltraLabel6.Name = "UltraLabel6";
        Me.UltraLabel6.Size = New System.Drawing.Size(160, 17);
        Me.UltraLabel6.TabIndex = 37;
        Me.UltraLabel6.Text = "Replace Character Names:";
        '
        'UltraGroupBox2
        '
        Me.UltraGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraGroupBox2.Controls.Add(Me.UltraLabel2);
        Me.UltraGroupBox2.Controls.Add(Me.UltraLabel1);
        Me.UltraGroupBox2.Controls.Add(Me.cmbDictionary);
        Me.UltraGroupBox2.Controls.Add(Me.lblDictionary);
        Me.UltraGroupBox2.Controls.Add(Me.dbDictionary);
        Me.UltraGroupBox2.Controls.Add(Me.dbUserDictionary);
        Me.UltraGroupBox2.Location = New System.Drawing.Point(11, 136);
        Me.UltraGroupBox2.Name = "UltraGroupBox2";
        Me.UltraGroupBox2.Size = New System.Drawing.Size(548, 147);
        Me.UltraGroupBox2.TabIndex = 39;
        Me.UltraGroupBox2.Text = "Spell Check";
        Me.UltraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'UltraLabel2
        '
        Me.UltraLabel2.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Location = New System.Drawing.Point(20, 32);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(109, 16);
        Me.UltraLabel2.TabIndex = 24;
        Me.UltraLabel2.Text = "Dictionary language:";
        '
        'UltraLabel1
        '
        Me.UltraLabel1.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Location = New System.Drawing.Point(20, 106);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel1.TabIndex = 23;
        Me.UltraLabel1.Text = "User dictionary location:";
        '
        'cmbDictionary
        '
        Me.cmbDictionary.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem1.DataValue = "English";
        ValueListItem2.DataValue = "Dutch";
        ValueListItem3.DataValue = "French";
        ValueListItem26.DataValue = "German";
        ValueListItem27.DataValue = "Italian";
        ValueListItem28.DataValue = "Portugese";
        ValueListItem29.DataValue = "Spanish";
        Me.cmbDictionary.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem26, ValueListItem27, ValueListItem28, ValueListItem29});
        Me.cmbDictionary.Location = New System.Drawing.Point(155, 28);
        Me.cmbDictionary.Name = "cmbDictionary";
        Me.cmbDictionary.Size = New System.Drawing.Size(144, 21);
        Me.cmbDictionary.TabIndex = 25;
        Me.cmbDictionary.Text = "English";
        '
        'lblDictionary
        '
        Me.lblDictionary.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblDictionary.Location = New System.Drawing.Point(20, 69);
        Me.lblDictionary.Name = "lblDictionary";
        Me.lblDictionary.Size = New System.Drawing.Size(134, 16);
        Me.lblDictionary.TabIndex = 27;
        Me.lblDictionary.Text = "Main dictionary location:";
        '
        'dbDictionary
        '
        Me.dbDictionary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.dbDictionary.BackColor = System.Drawing.Color.Transparent;
        Me.dbDictionary.BoxType = ADRIFT.DirectoryBox.BoxTypeEnum.File;
        Me.dbDictionary.Directory = "*** Incorrect BoxType ***";
        Me.dbDictionary.FileFilter = "Dictionary Files (*.dict)|*.dict|All Files (*.*)|*.*";
        Me.dbDictionary.Filename = "";
        Me.dbDictionary.Location = New System.Drawing.Point(155, 64);
        Me.dbDictionary.Name = "dbDictionary";
        Me.dbDictionary.OpenOrSave = ADRIFT.DirectoryBox.OpenOrSaveEnum.Open;
        Me.dbDictionary.Size = New System.Drawing.Size(370, 21);
        Me.dbDictionary.TabIndex = 28;
        '
        'dbUserDictionary
        '
        Me.dbUserDictionary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.dbUserDictionary.BackColor = System.Drawing.Color.Transparent;
        Me.dbUserDictionary.BoxType = ADRIFT.DirectoryBox.BoxTypeEnum.File;
        Me.dbUserDictionary.Directory = "*** Incorrect BoxType ***";
        Me.dbUserDictionary.FileFilter = "Dictionary Files (*.dict)|*.dict|All Files (*.*)|*.*";
        Me.dbUserDictionary.Filename = "";
        Me.dbUserDictionary.Location = New System.Drawing.Point(155, 102);
        Me.dbUserDictionary.Name = "dbUserDictionary";
        Me.dbUserDictionary.OpenOrSave = ADRIFT.DirectoryBox.OpenOrSaveEnum.Open;
        Me.dbUserDictionary.Size = New System.Drawing.Size(370, 21);
        Me.dbUserDictionary.TabIndex = 29;
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraGroupBox1.Controls.Add(Me.lblTheme);
        Me.UltraGroupBox1.Controls.Add(Me.cmbViewStyle);
        Me.UltraGroupBox1.Controls.Add(Me.chkShowKeys);
        Me.UltraGroupBox1.Controls.Add(Me.chkShowInTaskbar);
        Me.UltraGroupBox1.Controls.Add(Me.cmbColour);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel14);
        Me.UltraGroupBox1.Controls.Add(Me.chkSimpleMode);
        Me.UltraGroupBox1.Controls.Add(Me.chkAutoComplete);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel8);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel13);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel12);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel9);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel10);
        Me.UltraGroupBox1.Location = New System.Drawing.Point(11, 12);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(548, 118);
        Me.UltraGroupBox1.TabIndex = 45;
        Me.UltraGroupBox1.Text = "Appearance";
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'lblTheme
        '
        Me.lblTheme.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblTheme.Location = New System.Drawing.Point(20, 37);
        Me.lblTheme.Name = "lblTheme";
        Me.lblTheme.Size = New System.Drawing.Size(91, 16);
        Me.lblTheme.TabIndex = 0;
        Me.lblTheme.Text = "Window theme:";
        '
        'cmbViewStyle
        '
        Me.cmbViewStyle.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbViewStyle.Location = New System.Drawing.Point(155, 32);
        Me.cmbViewStyle.Name = "cmbViewStyle";
        Me.cmbViewStyle.Size = New System.Drawing.Size(144, 21);
        Me.cmbViewStyle.TabIndex = 20;
        '
        'chkShowKeys
        '
        Me.chkShowKeys.BackColor = System.Drawing.Color.Transparent;
        Me.chkShowKeys.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkShowKeys.Location = New System.Drawing.Point(24, 65);
        Me.chkShowKeys.Name = "chkShowKeys";
        Me.chkShowKeys.Size = New System.Drawing.Size(160, 20);
        Me.chkShowKeys.TabIndex = 21;
        Me.chkShowKeys.Text = "Show Keys on Edit forms";
        '
        'chkShowInTaskbar
        '
        Me.chkShowInTaskbar.BackColor = System.Drawing.Color.Transparent;
        Me.chkShowInTaskbar.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkShowInTaskbar.Location = New System.Drawing.Point(262, 65);
        Me.chkShowInTaskbar.Name = "chkShowInTaskbar";
        Me.chkShowInTaskbar.Size = New System.Drawing.Size(198, 20);
        Me.chkShowInTaskbar.TabIndex = 30;
        Me.chkShowInTaskbar.Text = "Show all open windows in taskbar";
        '
        'cmbColour
        '
        Me.cmbColour.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbColour.Location = New System.Drawing.Point(305, 32);
        Me.cmbColour.Name = "cmbColour";
        Me.cmbColour.Size = New System.Drawing.Size(114, 21);
        Me.cmbColour.TabIndex = 35;
        Me.cmbColour.Visible = false;
        '
        'UltraLabel14
        '
        Me.UltraLabel14.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel14.Location = New System.Drawing.Point(-215, 152);
        Me.UltraLabel14.Name = "UltraLabel14";
        Me.UltraLabel14.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel14.TabIndex = 23;
        Me.UltraLabel14.Text = "User dictionary location:";
        '
        'chkSimpleMode
        '
        Me.chkSimpleMode.BackColor = System.Drawing.Color.Transparent;
        Me.chkSimpleMode.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkSimpleMode.Location = New System.Drawing.Point(24, 91);
        Me.chkSimpleMode.Name = "chkSimpleMode";
        Me.chkSimpleMode.Size = New System.Drawing.Size(98, 20);
        Me.chkSimpleMode.TabIndex = 38;
        Me.chkSimpleMode.Text = "Simple Mode";
        '
        'chkAutoComplete
        '
        Me.chkAutoComplete.BackColor = System.Drawing.Color.Transparent;
        Me.chkAutoComplete.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkAutoComplete.Location = New System.Drawing.Point(262, 91);
        Me.chkAutoComplete.Name = "chkAutoComplete";
        Me.chkAutoComplete.Size = New System.Drawing.Size(184, 20);
        Me.chkAutoComplete.TabIndex = 38;
        Me.chkAutoComplete.Text = "Auto Complete dropdown lists";
        '
        'UltraLabel8
        '
        Me.UltraLabel8.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel8.Location = New System.Drawing.Point(-215, 152);
        Me.UltraLabel8.Name = "UltraLabel8";
        Me.UltraLabel8.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel8.TabIndex = 23;
        Me.UltraLabel8.Text = "User dictionary location:";
        '
        'UltraLabel13
        '
        Me.UltraLabel13.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel13.Location = New System.Drawing.Point(-215, 78);
        Me.UltraLabel13.Name = "UltraLabel13";
        Me.UltraLabel13.Size = New System.Drawing.Size(109, 16);
        Me.UltraLabel13.TabIndex = 24;
        Me.UltraLabel13.Text = "Dictionary language:";
        '
        'UltraLabel12
        '
        Me.UltraLabel12.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel12.Location = New System.Drawing.Point(-215, 115);
        Me.UltraLabel12.Name = "UltraLabel12";
        Me.UltraLabel12.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel12.TabIndex = 27;
        Me.UltraLabel12.Text = "Main dictionary location:";
        '
        'UltraLabel9
        '
        Me.UltraLabel9.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel9.Location = New System.Drawing.Point(-215, 78);
        Me.UltraLabel9.Name = "UltraLabel9";
        Me.UltraLabel9.Size = New System.Drawing.Size(109, 16);
        Me.UltraLabel9.TabIndex = 24;
        Me.UltraLabel9.Text = "Dictionary language:";
        '
        'UltraLabel10
        '
        Me.UltraLabel10.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel10.Location = New System.Drawing.Point(-215, 115);
        Me.UltraLabel10.Name = "UltraLabel10";
        Me.UltraLabel10.Size = New System.Drawing.Size(134, 16);
        Me.UltraLabel10.TabIndex = 27;
        Me.UltraLabel10.Text = "Main dictionary location:";
        '
        'cmbDefaultTaskActions
        '
        Me.cmbDefaultTaskActions.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem24.DataValue = 0;
        ValueListItem24.DisplayText = "Before Actions";
        ValueListItem25.DataValue = 1;
        ValueListItem25.DisplayText = "After Actions";
        Me.cmbDefaultTaskActions.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem24, ValueListItem25});
        Me.cmbDefaultTaskActions.Location = New System.Drawing.Point(196, 421);
        Me.cmbDefaultTaskActions.Name = "cmbDefaultTaskActions";
        Me.cmbDefaultTaskActions.Size = New System.Drawing.Size(94, 21);
        Me.cmbDefaultTaskActions.TabIndex = 43;
        Me.cmbDefaultTaskActions.Text = "After Actions";
        '
        'UltraLabel7
        '
        Me.UltraLabel7.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel7.Location = New System.Drawing.Point(26, 425);
        Me.UltraLabel7.Name = "UltraLabel7";
        Me.UltraLabel7.Size = New System.Drawing.Size(169, 17);
        Me.UltraLabel7.TabIndex = 44;
        Me.UltraLabel7.Text = "Default task messages to output";
        '
        'chkVersion
        '
        Me.chkVersion.BackColor = System.Drawing.Color.Transparent;
        Me.chkVersion.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkVersion.Location = New System.Drawing.Point(357, 422);
        Me.chkVersion.Name = "chkVersion";
        Me.chkVersion.Size = New System.Drawing.Size(191, 20);
        Me.chkVersion.TabIndex = 42;
        Me.chkVersion.Text = "Check for new version on startup";
        '
        'tabLibraries
        '
        Me.tabLibraries.Controls.Add(Me.lblLibraryDescription);
        Me.tabLibraries.Controls.Add(Me.cmbOverwriteLibraries);
        Me.tabLibraries.Controls.Add(Me.UltraLabel5);
        Me.tabLibraries.Controls.Add(Me.chkHideLibraryItems);
        Me.tabLibraries.Controls.Add(Me.btnDown);
        Me.tabLibraries.Controls.Add(Me.btnUp);
        Me.tabLibraries.Controls.Add(Me.btnRemove);
        Me.tabLibraries.Controls.Add(Me.btnAdd);
        Me.tabLibraries.Controls.Add(Me._lblInfo);
        Me.tabLibraries.Controls.Add(Me.lvwLibraries);
        Me.tabLibraries.Location = New System.Drawing.Point(-10000, -10000);
        Me.tabLibraries.Name = "tabLibraries";
        Me.tabLibraries.Size = New System.Drawing.Size(568, 458);
        '
        'lblLibraryDescription
        '
        Me.lblLibraryDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lblLibraryDescription.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
        Me.lblLibraryDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblLibraryDescription.Location = New System.Drawing.Point(16, 202);
        Me.lblLibraryDescription.Name = "lblLibraryDescription";
        Me.lblLibraryDescription.Size = New System.Drawing.Size(512, 183);
        Me.lblLibraryDescription.TabIndex = 35;
        '
        'cmbOverwriteLibraries
        '
        Me.cmbOverwriteLibraries.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.cmbOverwriteLibraries.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem11.DataValue = 0;
        ValueListItem11.DisplayText = "Prompt per library";
        ValueListItem17.DataValue = 1;
        ValueListItem17.DisplayText = "Prompt per item";
        ValueListItem12.DataValue = 2;
        ValueListItem12.DisplayText = "Always overwrite";
        ValueListItem13.DataValue = 3;
        ValueListItem13.DisplayText = "Never overwrite";
        Me.cmbOverwriteLibraries.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem11, ValueListItem17, ValueListItem12, ValueListItem13});
        Me.cmbOverwriteLibraries.Location = New System.Drawing.Point(356, 423);
        Me.cmbOverwriteLibraries.Name = "cmbOverwriteLibraries";
        Me.cmbOverwriteLibraries.Size = New System.Drawing.Size(128, 21);
        Me.cmbOverwriteLibraries.TabIndex = 33;
        Me.cmbOverwriteLibraries.Text = "Prompt per library";
        '
        'UltraLabel5
        '
        Me.UltraLabel5.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.UltraLabel5.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel5.Location = New System.Drawing.Point(18, 427);
        Me.UltraLabel5.Name = "UltraLabel5";
        Me.UltraLabel5.Size = New System.Drawing.Size(332, 16);
        Me.UltraLabel5.TabIndex = 34;
        Me.UltraLabel5.Text = "When loading a newer library item than in the current adventure:";
        '
        'chkHideLibraryItems
        '
        Me.chkHideLibraryItems.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.chkHideLibraryItems.BackColor = System.Drawing.Color.Transparent;
        Me.chkHideLibraryItems.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkHideLibraryItems.Location = New System.Drawing.Point(16, 391);
        Me.chkHideLibraryItems.Name = "chkHideLibraryItems";
        Me.chkHideLibraryItems.Size = New System.Drawing.Size(160, 20);
        Me.chkHideLibraryItems.TabIndex = 22;
        Me.chkHideLibraryItems.Text = "Hide Library Items";
        '
        'btnDown
        '
        Me.btnDown.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnDown.Enabled = false;
        Me.btnDown.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnDown.Location = New System.Drawing.Point(534, 126);
        Me.btnDown.Name = "btnDown";
        Me.btnDown.Size = New System.Drawing.Size(24, 23);
        Me.btnDown.TabIndex = 8;
        Me.btnDown.Text = "6";
        '
        'btnUp
        '
        Me.btnUp.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnUp.Enabled = false;
        Me.btnUp.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnUp.Location = New System.Drawing.Point(534, 97);
        Me.btnUp.Name = "btnUp";
        Me.btnUp.Size = New System.Drawing.Size(24, 23);
        Me.btnUp.TabIndex = 7;
        Me.btnUp.Text = "5";
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnRemove.Enabled = false;
        Me.btnRemove.Location = New System.Drawing.Point(400, 389);
        Me.btnRemove.Name = "btnRemove";
        Me.btnRemove.Size = New System.Drawing.Size(128, 22);
        Me.btnRemove.TabIndex = 5;
        Me.btnRemove.Text = "Remove Library";
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnAdd.Location = New System.Drawing.Point(266, 389);
        Me.btnAdd.Name = "btnAdd";
        Me.btnAdd.Size = New System.Drawing.Size(128, 22);
        Me.btnAdd.TabIndex = 4;
        Me.btnAdd.Text = "Add New Library";
        '
        '_lblInfo
        '
        Me._lblInfo.BackColorInternal = System.Drawing.Color.Transparent;
        Me._lblInfo.Location = New System.Drawing.Point(16, 16);
        Me._lblInfo.Name = "_lblInfo";
        Me._lblInfo.Size = New System.Drawing.Size(464, 24);
        Me._lblInfo.TabIndex = 1;
        Me._lblInfo.Text = "The following Libraries will be pre-loaded in all new games and older version upg" &;
    "rades.";
        '
        'lvwLibraries
        '
        Me.lvwLibraries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lvwLibraries.CheckBoxes = true;
        Me.lvwLibraries.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Library});
        Me.lvwLibraries.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lvwLibraries.FullRowSelect = true;
        Me.lvwLibraries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        Me.lvwLibraries.HideSelection = false;
        Me.lvwLibraries.Location = New System.Drawing.Point(16, 48);
        Me.lvwLibraries.Name = "lvwLibraries";
        Me.lvwLibraries.Size = New System.Drawing.Size(512, 148);
        Me.lvwLibraries.TabIndex = 0;
        Me.lvwLibraries.UseCompatibleStateImageBehavior = false;
        Me.lvwLibraries.View = System.Windows.Forms.View.Details;
        '
        'Library
        '
        Me.Library.Width = 284;
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.chkKeyNames);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(568, 458);
        '
        'chkKeyNames
        '
        Me.chkKeyNames.BackColor = System.Drawing.Color.Transparent;
        Me.chkKeyNames.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkKeyNames.Location = New System.Drawing.Point(30, 39);
        Me.chkKeyNames.Name = "chkKeyNames";
        Me.chkKeyNames.Size = New System.Drawing.Size(237, 20);
        Me.chkKeyNames.TabIndex = 22;
        Me.chkKeyNames.Text = "Generate Key names from item names";
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(466, 489);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 18;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(370, 489);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 17;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(274, 489);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 16;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 481);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(570, 48);
        Me.UltraStatusBar1.TabIndex = 15;
        '
        'tabsOptions
        '
        Me.tabsOptions.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsOptions.Controls.Add(Me.tabLibraries);
        Me.tabsOptions.Controls.Add(Me.tabGeneral);
        Me.tabsOptions.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsOptions.Location = New System.Drawing.Point(0, 0);
        Me.tabsOptions.Name = "tabsOptions";
        Me.tabsOptions.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsOptions.Size = New System.Drawing.Size(570, 481);
        Me.tabsOptions.TabIndex = 19;
        UltraTab1.Key = "General";
        UltraTab1.TabPage = Me.tabGeneral;
        UltraTab1.Text = "General";
        UltraTab2.Key = "Libraries";
        UltraTab2.TabPage = Me.tabLibraries;
        UltraTab2.Text = "Libraries";
        UltraTab3.Key = "Advanced";
        UltraTab3.TabPage = Me.UltraTabPageControl1;
        UltraTab3.Text = "Advanced";
        Me.tabsOptions.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3});
        Me.tabsOptions.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(568, 458);
        '
        'frmSettings
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(570, 529);
        Me.Controls.Add(Me.tabsOptions);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(586, 567);
        Me.Name = "frmSettings";
        Me.ShowInTaskbar = false;
        Me.Text = "Settings";
        Me.tabGeneral.ResumeLayout(false);
        Me.tabGeneral.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox4).EndInit();
        Me.UltraGroupBox4.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkGraphics).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkPreview).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAudio).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox3).EndInit();
        Me.UltraGroupBox3.ResumeLayout(false);
        Me.UltraGroupBox3.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReciprocalLink).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReciprocalRestriction).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReplaceNames).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).EndInit();
        Me.UltraGroupBox2.ResumeLayout(false);
        Me.UltraGroupBox2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbDictionary).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.UltraGroupBox1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbViewStyle).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowKeys).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkShowInTaskbar).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbColour).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkSimpleMode).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAutoComplete).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbDefaultTaskActions).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkVersion).EndInit();
        Me.tabLibraries.ResumeLayout(false);
        Me.tabLibraries.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbOverwriteLibraries).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkHideLibraryItems).EndInit();
        Me.UltraTabPageControl1.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkKeyNames).EndInit();
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

        ' Load Libraries from registry
        'Dim sLibraries() As String = GetSetting("ADRIFT", "Generator", "Libraries").Split("|"c)

        private ListViewItem lvi = null;
        For Each sLibrary As String In GetLibraries
            if (sLibrary <> "")
            {
                private string sFile;
                private bool bChecked = true;
                if (sLibrary.Contains("#"))
                {
                    sFile = sLibrary.Split("#"c)(0)
                    bChecked = CBool(sLibrary.Split("#"c)(1))
                Else
                    sFile = sLibrary
                }

                lvi = lvwLibraries.Items.Add(sFile)
                lvi.Checked = bChecked;

            }
        Next;

        'cmbViewStyle.Items.Add(Infragistics.Win.Misc.GroupBoxViewStyle.Office2000, Infragistics.Win.Misc.GroupBoxViewStyle.Office2000.ToString)
        'cmbViewStyle.Items.Add(Infragistics.Win.Misc.GroupBoxViewStyle.XP, Infragistics.Win.Misc.GroupBoxViewStyle.XP.ToString)

        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.Default, Infragistics.Win.UltraWinToolbars.ToolbarStyle.Default.ToString);
        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003, Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003.ToString);
        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007, Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007.ToString);
        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010, Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010.ToString);
        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013, Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013.ToString);
        cmbViewStyle.Items.Add(Infragistics.Win.UltraWinToolbars.ToolbarStyle.VisualStudio2005, Infragistics.Win.UltraWinToolbars.ToolbarStyle.VisualStudio2005.ToString);

        'Dim eViewStyle As Infragistics.Win.UltraWinToolbars.ToolbarStyle = EnumParseViewStyle(GetSetting("ADRIFT", "Generator", "ViewStyle", "Standard"))
        'SetCombo(cmbViewStyle, eViewStyle)

        'cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Blue, Infragistics.Win.Office2007ColorScheme.Blue.ToString)
        'cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Silver, Infragistics.Win.Office2007ColorScheme.Silver.ToString)
        'cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Black, Infragistics.Win.Office2007ColorScheme.Black.ToString)

        'Dim eColourScheme2007 As Infragistics.Win.Office2007ColorScheme = EnumParseColourScheme2007(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"))
        'SetCombo(cmbColour, eColourScheme)

        private Infragistics.Win.UltraWinToolbars.ToolbarStyle eViewStyle = EnumParseViewStyle(GetSetting("ADRIFT", "Generator", "ViewStyle", "Office2007"));
        SetCombo(cmbViewStyle, eViewStyle);
        switch (eStyle)
        {
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007:
                {
                private Infragistics.Win.Office2007ColorScheme eColourScheme2007 = EnumParseColourScheme2007(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"));
                SetCombo(cmbColour, eColourScheme2007);
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010:
                {
                private Infragistics.Win.Office2010ColorScheme eColourScheme2010 = EnumParseColourScheme2010(GetSetting("ADRIFT", "Generator", "ColourScheme", "Blue"));
                SetCombo(cmbColour, eColourScheme2010);
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013:
                {
                private Infragistics.Win.Office2013ColorScheme eColourScheme2013 = EnumParseColourScheme2013(GetSetting("ADRIFT", "Generator", "ColourScheme", "LightGray"));
                SetCombo(cmbColour, eColourScheme2013);
        }

        chkShowKeys.Checked = SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0"));
        chkHideLibraryItems.Checked = SafeBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "-1"));
        chkShowInTaskbar.Checked = SafeBool(GetSetting("ADRIFT", "Generator", "ShowInTaskBar", "0"));
        chkSimpleMode.Checked = fGenerator.SimpleMode;
        chkAutoComplete.Checked = fGenerator.AutoComplete;
        chkVersion.Checked = SafeBool(CInt(GetSetting("ADRIFT", "Shared", "AutoCheck", "1")));
        chkKeyNames.Checked = SafeBool(CInt(GetSetting("ADRIFT", "Generator", "KeyNames", "-1")));

        SetCombo(cmbDictionary, sDictionary);
        dbUserDictionary.Filename = sUserDictionary;
        dbDictionary.Filename = sMainDictionary;

        SetCombo(cmbReciprocalLink, SafeInt(GetSetting("ADRIFT", "Generator", "ReciprocalLink", "0")));
        SetCombo(cmbReciprocalRestriction, SafeInt(GetSetting("ADRIFT", "Generator", "ReciprocalRestriction", "0")));
        SetCombo(cmbReplaceNames, SafeInt(GetSetting("ADRIFT", "Generator", "ReplaceCharacterNames", "0")));
        SetCombo(cmbOverwriteLibraries, CInt(eOverwriteLibraries));
        SetCombo(cmbDefaultTaskActions, SafeInt(GetSetting("ADRIFT", "Generator", "DefaultActions", "1")));

        chkGraphics.Checked = bEnableGraphics;
        chkAudio.Checked = bEnableAudio;
        chkPreview.Checked = bEnablePreview;

        Changed = False
        Me.Show();

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplySettings();
        CloseSettings(Me);
    }


    private void ApplySettings()
    {

        ' Save Libraries to Registry
        private string sLibraries = "";
        For Each lvi As ListViewItem In lvwLibraries.Items
            sLibraries &= lvi.Text + "#" + lvi.Checked + "|";
        Next;
        SaveSetting("ADRIFT", "Generator", "Libraries", sLibraries);
        SaveSetting("ADRIFT", "Generator", "ShowKeys", CInt(chkShowKeys.Checked).ToString);
        SaveSetting("ADRIFT", "Generator", "HideLibraryItems", CInt(chkHideLibraryItems.Checked).ToString);
        SaveSetting("ADRIFT", "Generator", "KeyNames", CInt(chkKeyNames.Checked).ToString);

        ' Show/Hide any current library items as applicable
        fGenerator.FolderList1.HideLibrary = chkHideLibraryItems.Checked;
        For Each child As frmFolder In fGenerator.MDIFolders
            child.Folder.HideLibrary = chkHideLibraryItems.Checked;
        Next;
        'If chkHideLibraryItems.Checked Then
        '    'SaveListsToXML() ' Save where all our library tasks are, in case we turn them back on...

        '    For Each frmList As frmList In fGenerator.MdiChildren
        '        For Each lvi As ListViewItem In frmList.ItemList.lvwList.Items
        '            Dim sKey As String = lvi.SubItems(2).Text
        '            If CType(Adventure.dictAllItems(sKey), clsItem).IsLibrary Then
        '                If TypeOf Adventure.dictAllItems(sKey) Is clsTask OrElse TypeOf Adventure.dictAllItems(sKey) Is clsProperty Then lvi.Remove()
        '            End If
        '        Next
        '    Next
        'Else
        '    'LoadLists(False)
        'End If

        SaveSetting("ADRIFT", "Generator", "ShowInTaskbar", CInt(chkShowInTaskbar.Checked).ToString);
        fGenerator.SimpleMode = chkSimpleMode.Checked;
        fGenerator.AutoComplete = chkAutoComplete.Checked;
        tabsOptions.Tabs("Advanced").Visible = ! fGenerator.SimpleMode;

        eStyle = CType(cmbViewStyle.SelectedItem.DataValue, Infragistics.Win.UltraWinToolbars.ToolbarStyle)
        SaveSetting("ADRIFT", "Generator", "ViewStyle", eStyle.ToString);
        switch (eStyle)
        {
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013:
                {
                eColour2013 = CType(cmbColour.SelectedItem.DataValue, Infragistics.Win.Office2013ColorScheme)
                SaveSetting("ADRIFT", "Generator", "ColourScheme", eColour2013.ToString);
                Infragistics.Win.Office2013ColorTable.ColorScheme = eColour2013;
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010:
                {
                eColour2010 = CType(cmbColour.SelectedItem.DataValue, Infragistics.Win.Office2010ColorScheme)
                SaveSetting("ADRIFT", "Generator", "ColourScheme", eColour2010.ToString);
                Infragistics.Win.Office2010ColorTable.ColorScheme = eColour2010;
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007:
                {
                eColour2007 = CType(cmbColour.SelectedItem.DataValue, Infragistics.Win.Office2007ColorScheme)
                SaveSetting("ADRIFT", "Generator", "ColourScheme", eColour2007.ToString);
                Infragistics.Win.Office2007ColorTable.ColorScheme = eColour2007;
        }

        'For Each fChild As Form In fGenerator.MdiChildren

        'Next
        sDictionary = cmbDictionary.SelectedItem.DataValue.ToString
        SaveSetting("ADRIFT", "Generator", "DictionaryLanguage", sDictionary);
        sUserDictionary = dbUserDictionary.Filename
        SaveSetting("ADRIFT", "Generator", "UserDictionaryLocation", sUserDictionary);
        sMainDictionary = dbDictionary.Filename
        SaveSetting("ADRIFT", "Generator", "DictionaryLocation", sMainDictionary);
        SaveSetting("ADRIFT", "Generator", "ReciprocalLink", cmbReciprocalLink.SelectedItem.DataValue.ToString);
        SaveSetting("ADRIFT", "Generator", "ReplaceCharacterNames", cmbReplaceNames.SelectedItem.DataValue.ToString);
        SaveSetting("ADRIFT", "Generator", "ReciprocalRestriction", cmbReciprocalRestriction.SelectedItem.DataValue.ToString);
        eOverwriteLibraries = CType(cmbOverwriteLibraries.SelectedItem.DataValue, OverwriteLibrariesEnum)
        SaveSetting("ADRIFT", "Generator", "OverwriteLibraries", CInt(eOverwriteLibraries).ToString);
        bEnableGraphics = chkGraphics.Checked
        SaveSetting("ADRIFT", "Generator", "EnableGraphics", CInt(bEnableGraphics).ToString);
        bEnableAudio = chkAudio.Checked
        SaveSetting("ADRIFT", "Generator", "EnableAudio", CInt(bEnableAudio).ToString);
        bEnablePreview = chkPreview.Checked
        SaveSetting("ADRIFT", "Generator", "EnablePreview", CInt(bEnablePreview).ToString);
        SaveSetting("ADRIFT", "Shared", "AutoCheck", CInt(chkVersion.Checked).ToString);
        SaveSetting("ADRIFT", "Generator", "DefaultActions", CInt(cmbDefaultTaskActions.SelectedItem.DataValue).ToString);

        For Each f As Form In colAllForms
            SetWindowStyle(f, fGenerator.UTMMain, fGenerator.UDMGenerator);
        Next;

    }


    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplySettings();
        Changed = False
    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplySettings()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseSettings(Me);
    }

    private void btnAdd_Click(System.Object sender, System.EventArgs e)
    {

        if (Not IsRegistered)
        {
            NotAvailable();
            Exit Sub;
        }

        With ofd;
            .DefaultExt = "amf";
            .FileName = "";
            .Filter = "ADRIFT Module (*.amf)|*.amf|All Files (*.*)|*.*";
            .Title = "Please select an ADRIFT Module";
            if (.ShowDialog = Windows.Forms.DialogResult.OK)
            {
                private ListViewItem lvi = lvwLibraries.Items.Add(.FileName);
                lvi.Checked = true;
                lvwLibraries.SelectedItems.Clear();
                lvi.Selected = true;
                Changed = True
                DoButtons()
                lvwLibraries.Columns(lvwLibraries.Columns.Count - 1).Width = -1;
            }

        }

    }

    private void lvwLibraries_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        DoButtons()

        try
        {
            if (lvwLibraries.SelectedItems.Count = 1 && IO.File.Exists(lvwLibraries.SelectedItems(0).Text))
            {
                private New Xml.XmlDocument xmlDoc;
                private string sDescription = "";
                xmlDoc.Load(lvwLibraries.SelectedItems(0).Text);
                if (xmlDoc.Item("Adventure") IsNot null)
                {
                    With xmlDoc.Item("Adventure");
                        If .Item("Title") != null Then sDescription = .Item("Title").InnerText
                        If .Item("Author") != null && .Item("Author").InnerText <> "" Then sDescription &= " by " + .Item("Author").InnerText
                        if (.Item("Description") IsNot null && .Item("Description").InnerText <> "")
                        {
                            If sDescription <> "" Then sDescription &= vbCrLf + vbCrLf
                            sDescription &= .Item("Description").InnerText;
                        }
                    }
                }
                lblLibraryDescription.Text = sDescription;
            }

        }
        catch (Exception ex)
        {

        }

    }

    private void btnRemove_Click(System.Object sender, System.EventArgs e)
    {

        if (lvwLibraries.SelectedItems.Count > 0)
        {
            lvwLibraries.SelectedItems(0).Remove();
            Changed = True
            DoButtons()
        }

    }

    private void DoButtons()
    {

        if (lvwLibraries.SelectedItems.Count > 0)
        {
            btnRemove.Enabled = true;
            If lvwLibraries.SelectedItems(0).Index > 0 Then btnUp.Enabled = true Else btnUp.Enabled = false
            If lvwLibraries.SelectedItems(0).Index < lvwLibraries.Items.Count - 1 Then btnDown.Enabled = true Else btnDown.Enabled = false
        Else
            btnRemove.Enabled = false;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
        }

    }

    private void btnDown_Click(System.Object sender, System.EventArgs e)
    {

        private int iUpDown = CInt(IIf(sender Is btnUp, -1, +1));

        private int iSelIndex = lvwLibraries.SelectedItems(0).Index;
        private string sTemp = lvwLibraries.SelectedItems(0).Text;
        private bool bChecked = lvwLibraries.SelectedItems(0).Checked;
        lvwLibraries.SelectedItems(0).Text = lvwLibraries.Items(iSelIndex + iUpDown).Text;
        lvwLibraries.SelectedItems(0).Checked = lvwLibraries.Items(iSelIndex + iUpDown).Checked;
        lvwLibraries.Items(iSelIndex + iUpDown).Text = sTemp;
        lvwLibraries.Items(iSelIndex + iUpDown).Checked = bChecked;
        lvwLibraries.Items(iSelIndex).Selected = false;
        lvwLibraries.Items(iSelIndex + iUpDown).Selected = true;
        DoButtons()
        lvwLibraries.Focus();
        Changed = True

    }

    private void frmSettings_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        fGenerator.fSettings = null;
    }

    'Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click

    '    Dim iSelIndex As Integer = lvwLibraries.SelectedItems(0).Index
    '    Dim sTemp As String = lvwLibraries.SelectedItems(0).Text
    '    lvwLibraries.SelectedItems(0).Text = lvwLibraries.Items(iSelIndex - 1).Text
    '    lvwLibraries.Items(iSelIndex - 1).Text = sTemp
    '    lvwLibraries.Items(iSelIndex).Selected = False
    '    lvwLibraries.Items(iSelIndex - 1).Selected = True
    '    DoButtons()
    '    lvwLibraries.Focus()

    'End Sub

    private void frmOptions_Resize(object sender, System.EventArgs e)
    {
        'lvwLibraries.Columns(0).Width = lvwLibraries.Width - 6
    }

    private void frmOptions_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgSettings16.GetHicon);
        GetFormPosition(Me);
        tabsOptions.Tabs("Advanced").Visible = ! fGenerator.SimpleMode;
    }


    private void cmbViewStyle_SelectionChanged(object sender, System.EventArgs e)
    {

        switch (CType(cmbViewStyle.SelectedItem.DataValue, Infragistics.Win.UltraWinToolbars.ToolbarStyle))
        {
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007:
                {
                if (cmbColour.Items.Count = 0 || CType(cmbColour.Items(0).DataValue, Infragistics.Win.Office2007ColorScheme) <> Infragistics.Win.Office2007ColorScheme.Blue || cmbColour.Items(0).DisplayText <> Infragistics.Win.Office2007ColorScheme.Blue.ToString)
                {

                    cmbColour.Items.Clear();
                    cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Blue, Infragistics.Win.Office2007ColorScheme.Blue.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Silver, Infragistics.Win.Office2007ColorScheme.Silver.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2007ColorScheme.Black, Infragistics.Win.Office2007ColorScheme.Black.ToString);
                    switch (_PreviousColour)
                    {
                        case "Blue":
                        case "LightGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2007ColorScheme.Blue);
                        case "Black":
                        case "DarkGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2007ColorScheme.Black);
                        case "Silver":
                        case "White":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2007ColorScheme.Silver);
                    }
                }
                cmbColour.Visible = true;

            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010:
                {
                if (cmbColour.Items.Count = 0 || CType(cmbColour.Items(0).DataValue, Infragistics.Win.Office2010ColorScheme) <> Infragistics.Win.Office2010ColorScheme.Blue || cmbColour.Items(0).DisplayText <> Infragistics.Win.Office2010ColorScheme.Blue.ToString)
                {
                    cmbColour.Items.Clear();
                    cmbColour.Items.Add(Infragistics.Win.Office2010ColorScheme.Blue, Infragistics.Win.Office2010ColorScheme.Blue.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2010ColorScheme.Silver, Infragistics.Win.Office2010ColorScheme.Silver.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2010ColorScheme.Black, Infragistics.Win.Office2010ColorScheme.Black.ToString);
                    switch (_PreviousColour)
                    {
                        case "Blue":
                        case "LightGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2010ColorScheme.Blue);
                        case "Black":
                        case "DarkGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2010ColorScheme.Black);
                        case "Silver":
                        case "White":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2010ColorScheme.Silver);
                    }
                }
                cmbColour.Visible = true;

            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013:
                {
                if (cmbColour.Items.Count = 0 || CType(cmbColour.Items(0).DataValue, Infragistics.Win.Office2013ColorScheme) <> Infragistics.Win.Office2013ColorScheme.LightGray || cmbColour.Items(0).DisplayText <> Infragistics.Win.Office2013ColorScheme.LightGray.ToString)
                {
                    cmbColour.Items.Clear();
                    cmbColour.Items.Add(Infragistics.Win.Office2013ColorScheme.LightGray, Infragistics.Win.Office2013ColorScheme.LightGray.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2013ColorScheme.White, Infragistics.Win.Office2013ColorScheme.White.ToString);
                    cmbColour.Items.Add(Infragistics.Win.Office2013ColorScheme.DarkGray, Infragistics.Win.Office2013ColorScheme.DarkGray.ToString);
                    switch (_PreviousColour)
                    {
                        case "Blue":
                        case "LightGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2013ColorScheme.LightGray);
                        case "Black":
                        case "DarkGray":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2013ColorScheme.DarkGray);
                        case "Silver":
                        case "White":
                            {
                            SetCombo(cmbColour, Infragistics.Win.Office2013ColorScheme.White);
                    }
                }
                cmbColour.Visible = true;
            default:
                {
                cmbColour.Visible = false;
        }

    }


    private void StuffChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
    }


    private string _PreviousColour = "";
    private void cmbColour_SelectionChanged(object o, EventArgs e)
    {
        _PreviousColour = cmbColour.SelectedItem.DisplayText
    }


    protected override void WndProc(ref m As System.Windows.Forms.Message)
    {

        private const int WM_PAINT = &HF  ' 15;

        switch (m.Msg)
        {
            case WM_PAINT:
                {
                if (lvwLibraries.View = View.Details && lvwLibraries.Columns.Count > 0)
                {
                    lvwLibraries.Columns(lvwLibraries.Columns.Count - 1).Width = -1 ' -2;
                }
        }

        MyBase.WndProc(m);

    }

    private void Checkboxes_Click(object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void frmSettings_Shown(object sender, System.EventArgs e)
    {
        Changed = False
    }

    private void frmSettings_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Settings");
    }

}

}