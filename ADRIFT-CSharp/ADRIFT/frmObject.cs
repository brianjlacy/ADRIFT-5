using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmObject
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public bool bKeepOpen = false;

    public void New(ref ob As clsObject, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmObject)
            {
                if (CType(w, frmObject).cObject.Key = ob.Key && ob.Key IsNot null)
                {
                    w.BringToFront();
                    w.Focus();
                    Exit Sub;
                }
            }
        Next;

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        LoadForm(ob, bShow);
        bKeepOpen = Not bShow

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
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents tabsObject As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents Properties1 As ADRIFT.Properties;
    Friend WithEvents txtDescription As ADRIFT.GenTextbox;
    Friend WithEvents lblArticle As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblPrefix As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblNouns As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtArticle As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents txtPrefix As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents cmbNames As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider;
    Friend WithEvents cmbLocation2 As ADRIFT.ItemSelector;
    Friend WithEvents cmbInitialLocation As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents chkDynamic As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkStatic As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance6 = new Infragistics.Win.Appearance();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmObject));
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.UltraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.cmbInitialLocation = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.cmbLocation2 = New ADRIFT.ItemSelector();
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.chkDynamic = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkStatic = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.txtDescription = New ADRIFT.GenTextbox();
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel();
        Me.txtPrefix = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.txtArticle = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.cmbNames = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.lblNouns = New Infragistics.Win.Misc.UltraLabel();
        Me.lblPrefix = New Infragistics.Win.Misc.UltraLabel();
        Me.lblArticle = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.Properties1 = New ADRIFT.Properties();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.tabsObject = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).BeginInit();
        Me.UltraGroupBox2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbInitialLocation).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkDynamic).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkStatic).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtPrefix).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtArticle).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbNames).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsObject).BeginInit();
        Me.tabsObject.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraGroupBox2);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraGroupBox1);
        Me.UltraTabPageControl1.Controls.Add(Me.txtDescription);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel1);
        Me.UltraTabPageControl1.Controls.Add(Me.txtPrefix);
        Me.UltraTabPageControl1.Controls.Add(Me.txtArticle);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbNames);
        Me.UltraTabPageControl1.Controls.Add(Me.lblNouns);
        Me.UltraTabPageControl1.Controls.Add(Me.lblPrefix);
        Me.UltraTabPageControl1.Controls.Add(Me.lblArticle);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(548, 396);
        '
        'UltraGroupBox2
        '
        Me.UltraGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.UltraGroupBox2.Appearance = Appearance1;
        Me.UltraGroupBox2.Controls.Add(Me.cmbInitialLocation);
        Me.UltraGroupBox2.Controls.Add(Me.cmbLocation2);
        Me.UltraGroupBox2.Location = New System.Drawing.Point(179, 60);
        Me.UltraGroupBox2.Name = "UltraGroupBox2";
        Me.UltraGroupBox2.Size = New System.Drawing.Size(363, 50);
        Me.UltraGroupBox2.TabIndex = 24;
        Me.UltraGroupBox2.Text = "Initial Location:";
        '
        'cmbInitialLocation
        '
        Me.cmbInitialLocation.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem3.DataValue = "ValueListItem0";
        ValueListItem3.DisplayText = "Part of Character";
        Me.cmbInitialLocation.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem3});
        Me.cmbInitialLocation.Location = New System.Drawing.Point(12, 19);
        Me.cmbInitialLocation.Name = "cmbInitialLocation";
        Me.cmbInitialLocation.Size = New System.Drawing.Size(129, 21);
        Me.cmbInitialLocation.TabIndex = 22;
        Me.cmbInitialLocation.Text = "Part of Character";
        '
        'cmbLocation2
        '
        Me.cmbLocation2.AllowAddEdit = true;
        Me.cmbLocation2.AllowedListType = ADRIFT.ItemSelector.ItemEnum.Character;
        Me.cmbLocation2.AllowHidden = false;
        Me.cmbLocation2.BackColor = System.Drawing.Color.Transparent;
        Me.cmbLocation2.Key = null;
        Me.cmbLocation2.ListType = ADRIFT.ItemSelector.ItemEnum.Character;
        Me.cmbLocation2.Location = New System.Drawing.Point(149, 19);
        Me.cmbLocation2.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbLocation2.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbLocation2.Name = "cmbLocation2";
        Me.cmbLocation2.Size = New System.Drawing.Size(202, 21);
        Me.cmbLocation2.TabIndex = 21;
        '
        'UltraGroupBox1
        '
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Me.UltraGroupBox1.Appearance = Appearance2;
        Me.UltraGroupBox1.Controls.Add(Me.chkDynamic);
        Me.UltraGroupBox1.Controls.Add(Me.chkStatic);
        Me.UltraGroupBox1.Location = New System.Drawing.Point(8, 60);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(153, 50);
        Me.UltraGroupBox1.TabIndex = 23;
        Me.UltraGroupBox1.Text = "Object Type:";
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
        '
        'chkDynamic
        '
        Appearance5.Image = Global.ADRIFT.My.Resources.Resources.imgObjectDynamic16;
        Appearance5.ImageHAlign = Infragistics.Win.HAlign.Left;
        Appearance5.TextHAlignAsString = "Center";
        Me.chkDynamic.Appearance = Appearance5;
        Me.chkDynamic.BackColor = System.Drawing.Color.Transparent;
        Me.chkDynamic.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkDynamic.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
        Me.chkDynamic.Location = New System.Drawing.Point(71, 17);
        Me.chkDynamic.Name = "chkDynamic";
        Me.chkDynamic.Size = New System.Drawing.Size(73, 26);
        Me.chkDynamic.Style = Infragistics.Win.EditCheckStyle.Button;
        Me.chkDynamic.TabIndex = 30;
        Me.chkDynamic.Text = "Dynamic";
        Me.chkDynamic.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'chkStatic
        '
        Appearance6.Image = Global.ADRIFT.My.Resources.Resources.imgObjectStatic16;
        Appearance6.ImageHAlign = Infragistics.Win.HAlign.Left;
        Appearance6.TextHAlignAsString = "Center";
        Me.chkStatic.Appearance = Appearance6;
        Me.chkStatic.BackColor = System.Drawing.Color.Transparent;
        Me.chkStatic.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkStatic.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
        Me.chkStatic.Location = New System.Drawing.Point(11, 17);
        Me.chkStatic.Name = "chkStatic";
        Me.chkStatic.Size = New System.Drawing.Size(56, 26);
        Me.chkStatic.Style = Infragistics.Win.EditCheckStyle.Button;
        Me.chkStatic.TabIndex = 29;
        Me.chkStatic.Text = "Static";
        Me.chkStatic.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'txtDescription
        '
        Me.txtDescription.AllowAlternateDescriptions = true;
        Me.txtDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDescription.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.txtDescription.FirstTabHasRestrictions = false;
        Me.txtDescription.Location = New System.Drawing.Point(8, 131);
        Me.txtDescription.Name = "txtDescription";
        Me.txtDescription.sCommand = null;
        Me.txtDescription.Size = New System.Drawing.Size(536, 250);
        Me.txtDescription.TabIndex = 3;
        '
        'UltraLabel1
        '
        Me.UltraLabel1.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Location = New System.Drawing.Point(8, 115);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(100, 23);
        Me.UltraLabel1.TabIndex = 19;
        Me.UltraLabel1.Text = "Description:";
        '
        'txtPrefix
        '
        Me.txtPrefix.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.HelpProvider1.SetHelpString(Me.txtPrefix, resources.GetString("txtPrefix.HelpString"));
        Me.txtPrefix.Location = New System.Drawing.Point(112, 24);
        Me.txtPrefix.Name = "txtPrefix";
        Me.HelpProvider1.SetShowHelp(Me.txtPrefix, true);
        Me.txtPrefix.Size = New System.Drawing.Size(208, 26);
        Me.txtPrefix.TabIndex = 1;
        Me.txtPrefix.Text = "orange";
        '
        'txtArticle
        '
        Me.txtArticle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.HelpProvider1.SetHelpString(Me.txtArticle, resources.GetString("txtArticle.HelpString"));
        Me.txtArticle.Location = New System.Drawing.Point(8, 24);
        Me.txtArticle.Name = "txtArticle";
        Me.HelpProvider1.SetShowHelp(Me.txtArticle, true);
        Me.txtArticle.Size = New System.Drawing.Size(96, 26);
        Me.txtArticle.TabIndex = 0;
        Me.txtArticle.Text = "an";
        '
        'cmbNames
        '
        Me.cmbNames.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbNames.Location = New System.Drawing.Point(328, 24);
        Me.cmbNames.Name = "cmbNames";
        Me.cmbNames.Size = New System.Drawing.Size(208, 26);
        Me.cmbNames.TabIndex = 2;
        '
        'lblNouns
        '
        Me.lblNouns.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblNouns.Location = New System.Drawing.Point(328, 8);
        Me.lblNouns.Name = "lblNouns";
        Me.lblNouns.Size = New System.Drawing.Size(88, 16);
        Me.lblNouns.TabIndex = 18;
        Me.lblNouns.Text = "Name/Noun(s):";
        '
        'lblPrefix
        '
        Me.lblPrefix.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblPrefix.Location = New System.Drawing.Point(112, 8);
        Me.lblPrefix.Name = "lblPrefix";
        Me.lblPrefix.Size = New System.Drawing.Size(88, 16);
        Me.lblPrefix.TabIndex = 17;
        Me.lblPrefix.Text = "Prefix/Adjective:";
        '
        'lblArticle
        '
        Me.lblArticle.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblArticle.Location = New System.Drawing.Point(8, 8);
        Me.lblArticle.Name = "lblArticle";
        Me.lblArticle.Size = New System.Drawing.Size(100, 23);
        Me.lblArticle.TabIndex = 16;
        Me.lblArticle.Text = "Article:";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Properties1);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(548, 396);
        '
        'Properties1
        '
        Me.Properties1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Properties1.BackColor = System.Drawing.Color.Transparent;
        Me.Properties1.Location = New System.Drawing.Point(8, 8);
        Me.Properties1.Name = "Properties1";
        Me.Properties1.Size = New System.Drawing.Size(536, 384);
        Me.Properties1.TabIndex = 0;
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 422);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(552, 48);
        Me.UltraStatusBar1.TabIndex = 5;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(448, 432);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 14;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(352, 432);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 13;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(256, 432);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 12;
        Me.btnOK.Text = "OK";
        '
        'tabsObject
        '
        Me.tabsObject.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsObject.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsObject.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsObject.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsObject.Location = New System.Drawing.Point(0, 0);
        Me.tabsObject.Name = "tabsObject";
        Me.tabsObject.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsObject.Size = New System.Drawing.Size(552, 422);
        Me.tabsObject.TabIndex = 15;
        UltraTab1.Key = "Description";
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Description";
        UltraTab2.Key = "Properties";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Properties";
        Me.tabsObject.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(548, 396);
        '
        'frmObject
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(552, 470);
        Me.Controls.Add(Me.tabsObject);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(568, 508);
        Me.Name = "frmObject";
        Me.ShowInTaskbar = false;
        Me.Text = "Object - ";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox2).EndInit();
        Me.UltraGroupBox2.ResumeLayout(false);
        Me.UltraGroupBox2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbInitialLocation).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkDynamic).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkStatic).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtPrefix).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtArticle).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbNames).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsObject).EndInit();
        Me.tabsObject.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region


    private clsObject cObject;
    private bool bChanged;
    private int iSelectedIndex = 0;
    private bool bAllowChangeValue = true;


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


    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True

        if (Properties1.htblProperties.ContainsKey("StaticOrDynamic"))
        {
            if (Properties1.htblProperties("StaticOrDynamic").StringData.ToString = "Dynamic")
            {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectDynamic16.GetHicon);
            Else
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectStatic16.GetHicon);
            }
        }
    }


    private void SetName()
    {
        With cObject;
            Text = "Object - " & .FullName
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            If .FullName = "Undefined Object" Then Text = "New Object"
        }
    }

    private void LoadForm(ref cObject As clsObject, bool bShow)
    {

        Me.cObject = cObject;


        With cObject;

            SetName();

            txtDescription.Description = .Description.Copy;
            txtArticle.Text = .Article;
            txtPrefix.Text = .Prefix;
            For Each sName As String In .arlNames
                cmbNames.Items.Add(sName);
            Next;
            if (cmbNames.Items.Count > 0)
            {
                iSelectedIndex = 0
                cmbNames.SelectedIndex = 0;
            Else
                cmbNames.Items.Add("");
            }

            private string sLocation2Property = "";
            if (.IsStatic)
            {
                chkStatic.Checked = true;
                if (.htblProperties.ContainsKey("StaticLocation"))
                {
                    cmbInitialLocation.Text = .htblProperties("StaticLocation").Value;
                    'SetCombo(cmbInitialLocation, .htblProperties("StaticLocation").Value)
                    switch (.htblProperties("StaticLocation").Value)
                    {
                        case "Nowhere":
                        case "Hidden":
                            {
                            ' Nothing to do
                        case "Single Location":
                            {
                            sLocation2Property = "AtLocation"
                        case "Location Group":
                            {
                            sLocation2Property = "AtLocationGroup"
                        case "Everywhere":
                            {
                            ' Nothing to do
                        case "Part of Character":
                            {
                            sLocation2Property = "PartOfWho"
                        case "Part of Object":
                            {
                            sLocation2Property = "PartOfWhat"
                        default:
                            {
                            TODO("Additional Static Location property");
                    }
                }
            Else
                chkDynamic.Checked = true;
                if (.htblProperties.ContainsKey("DynamicLocation"))
                {
                    cmbInitialLocation.Text = .htblProperties("DynamicLocation").Value;
                    'SetCombo(cmbInitialLocation, .htblProperties("DynamicLocation").Value)
                    switch (.htblProperties("DynamicLocation").Value)
                    {
                        case "Held By Character":
                            {
                            sLocation2Property = "HeldByWho"
                        case "Hidden":
                            {
                            ' Nothing to do
                        case "In Location":
                            {
                            sLocation2Property = "InLocation"
                        case "Inside Object":
                            {
                            sLocation2Property = "InsideWhat"
                        case "On Object":
                            {
                            sLocation2Property = "OnWhat"
                        case "Worn By Character":
                            {
                            sLocation2Property = "WornByWho"
                        default:
                            {
                            TODO("Additional Static Location property");
                    }
                }
            }

            if (sLocation2Property <> "" && .htblProperties.ContainsKey(sLocation2Property))
            {
                cmbLocation2.Key = .htblProperties(sLocation2Property).Value;
            }

            ' Pad out the local Object hashtable with unselected properties
            '.bCalculatedGroups = False
            .ResetInherited();
            private PropertyHashTable htblProperties = .htblProperties.Clone ' .GetPropertiesIncludingGroups.Clone ' .htblProperties.Clone;
            For Each prop As clsProperty In Adventure.htblObjectProperties.Values
                If ! htblProperties.ContainsKey(prop.Key) Then htblProperties.Add(prop.Copy)
            Next;
            Me.Properties1.htblProperties = htblProperties;
            Me.Properties1.OwnerKey = .Key;
            Changed = False
        }

        GetFormPosition(Me);
        UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
        UltraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;

        If bShow Then Me.Show()

        OpenForms.Add(Me);
    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        If ApplyObject() Then CloseObject(Me)
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        If ApplyObject() Then Changed = false
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (result = Windows.Forms.DialogResult.Yes)
            {
                If ! ApplyObject() Then Exit Sub
            }
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseObject(Me);
    }


    private bool ValidateObject()
    {

        private string sFailingProperty = null;
        if (Not Properties1.ValidateProperties(sFailingProperty))
        {
            switch (sFailingProperty)
            {
                case "InLocation":
                    {
                    tabsObject.SelectedTab = tabsObject.Tabs("Description");
                default:
                    {
                    tabsObject.SelectedTab = tabsObject.Tabs("Properties");
            }
            return false;
        }

        ' Make sure a recursive location hasn't been created
        switch (cmbInitialLocation.Text)
        {
            case "Inside Object":
            case "On Object":
                {
                if (cObject.Key <> "" && (cObject.Key = cmbLocation2.Key || Adventure.htblObjects(cObject.Key).Children(clsObject.WhereChildrenEnum.InsideOrOnObject, true).ContainsKey(cmbLocation2.Key)))
                {
                    ErrMsg("Error - This would create a recursive location for the object.");
                    tabsObject.SelectedTab = tabsObject.Tabs("Description");
                    return false;
                }
        }

        'If txtArticle.Text = "the" Then
        '    MessageBox.Show("ADRIFT expects an indefinite article (for example ""a"", ""an"" etc) rather than a definite one (i.e. ""the"").", "Incorrect Article", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    tabsObject.SelectedTab = tabsObject.Tabs("Description")
        '    txtArticle.Focus()
        '    Return False
        'End If
        return true;
    }


    private bool ApplyObject()
    {

        If ! ValidateObject() Then Return false

        ' remember to strip off the unselected properties
        With cObject;
            '.Description = txtName.Text
            '.CompletionMessage = txtCompletion.txtSource
            .Article = txtArticle.Text.Trim;
            .Prefix = txtPrefix.Text.Trim;
            .arlNames.Clear();
            For Each vli As Infragistics.Win.ValueListItem In cmbNames.Items
                If vli.DisplayText <> "" && ! .arlNames.Contains(vli.DisplayText.Trim) Then .arlNames.Add(vli.DisplayText.Trim)
            Next;
            .Description = txtDescription.Description.Copy;
            .LastUpdated = Now;
            .IsLibrary = false;

            .htblActualProperties = Me.Properties1.htblProperties.CopySelected;

            if (chkStatic.Checked)
            {
                .IsStatic = true;
                .SetPropertyValue("StaticLocation", cmbInitialLocation.Text);
                private string sStaticLocation = String.Empty;
                If cmbInitialLocation.SelectedItem != null Then sStaticLocation = SafeString(cmbInitialLocation.SelectedItem.DataValue)
                If cmbLocation2.Enabled && cmbLocation2.Key <> "" Then .SetPropertyValue(sStaticLocation, cmbLocation2.Key)
            ElseIf chkDynamic.Checked Then
                .IsStatic = false;
                .SetPropertyValue("DynamicLocation", cmbInitialLocation.Text);
                private string sDynamicLocation = String.Empty;
                If cmbInitialLocation.SelectedItem != null Then sDynamicLocation = SafeString(cmbInitialLocation.SelectedItem.DataValue)
                If cmbLocation2.Enabled && cmbLocation2.Key <> "" Then .SetPropertyValue(sDynamicLocation, cmbLocation2.Key)
            }

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Object");
                Adventure.htblObjects.Add(cObject, .Key);
                Me.Properties1.OwnerKey = .Key;
            }

            SetName();

            UpdateListItem(.Key, .FullName);

            For Each w As Form In OpenForms
                if (TypeOf w Is frmLocation)
                {
                    private string sLocKey = CType(w, frmLocation).Folder1.sParentKey;
                    If (! .IsStatic && .Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation && .Location.Key = sLocKey) _
                     || (.IsStatic && (.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.AllRooms || (.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation && .LocationRoots.ContainsKey(sLocKey)))) Then;
                        (frmLocation)(w).Folder1.AddSingleItem(.Key);
                    }
                }
            Next;

        }

        Adventure.Changed = true;

        return true;

    }


    private void frmObject_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmObject_Resize(object sender, System.EventArgs e)
    {

        Me.txtPrefix.Width = CInt(Me.Width / 2) - 72;
        Me.cmbNames.Width = CInt(Me.Width / 2) - 72;
        Me.cmbNames.Left = CInt(Me.Width / 2) + 48;
        Me.lblNouns.Left = Me.cmbNames.Left;
        cmbInitialLocation.Width = cmbNames.Left - UltraGroupBox2.Left - 24;
        cmbLocation2.Left = cmbInitialLocation.Width + 24;
        cmbLocation2.Width = cmbNames.Width - 5;

    }


    private void cmbNames_Enter(object sender, System.EventArgs e)
    {
        Me.AcceptButton = null;
    }



    private void txtPrefix_TextChanged(object sender, System.EventArgs e)
    {

        if (txtArticle.Text = "")
        {
            if (txtPrefix.Text <> "")
            {
                switch (sLeft(txtPrefix.Text, 1).ToLower)
                {
                    case "a":
                    case "e":
                    case "i":
                    case "o":
                    case "u":
                        {
                        txtArticle.Text = "an";
                    default:
                        {
                        txtArticle.Text = "a";
                }
            ElseIf txtPrefix.Text = "" && cmbNames.Text <> "" Then
                switch (sLeft(cmbNames.Text, 1).ToLower)
                {
                    case "a":
                    case "e":
                    case "i":
                    case "o":
                    case "u":
                        {
                        txtArticle.Text = "an";
                    default:
                        {
                        txtArticle.Text = "a";
                }
            }
        }
        Changed = True

    }


    private void cmbNames_Leave(object sender, System.EventArgs e)
    {
        'AddName(True)
        for (int i = cmbNames.Items.Count - 1; i <= 0; i += -1)
        {
            if (cmbNames.Items(i).DisplayText = "")
            {
                If iSelectedIndex = i Then iSelectedIndex = 0
                If iSelectedIndex > i Then iSelectedIndex -= 1
                If cmbNames.Items.Count > 1 Then cmbNames.Items.RemoveAt(i)
            }
        Next;
        Me.AcceptButton = btnOK;
    }

    'Private Sub frmObject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    GetFormPosition(Me)
    'End Sub


    private void cmbNames_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        switch (e.KeyData)
        {
            case Keys.Enter:
                {
                if (iSelectedIndex = cmbNames.Items.Count - 1)
                {
                    iSelectedIndex += 1;
                    cmbNames.Items.Add("");
                    bAllowChangeValue = False
                    cmbNames.Clear();
                    bAllowChangeValue = True
                Else
                    iSelectedIndex += 1;
                    If cmbNames.Items.Count > iSelectedIndex Then cmbNames.SelectedItem = cmbNames.Items(iSelectedIndex)
                    If cmbNames.SelectedItem != null Then cmbNames.SelectedText = cmbNames.SelectedItem.DisplayText
                    cmbNames.SelectionStart = 0;
                    cmbNames.SelectionLength = cmbNames.Text.Length;
                }
                'NamesDebug()
            case Keys.Up:
                {
                If iSelectedIndex > 0 Then iSelectedIndex -= 1
            case Keys.Down:
                {
                If iSelectedIndex < cmbNames.Items.Count - 1 Then iSelectedIndex += 1
            case Keys.Delete:
                {
                If cmbNames.SelectedText = cmbNames.Text && iSelectedIndex > -1 Then cmbNames.Items(iSelectedIndex).DataValue = ""
        }

        'If cmbNames.Text = "" Then
        '    Debug.WriteLine("Text cleared on item " & iSelectedIndex)
        'End If
        'cmbNames.Items(iSelectedIndex).DisplayText = cmbNames.Text
        'NamesDebug()

    }



    private void cmbNames_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        If cmbNames.Items.Count > iSelectedIndex Then cmbNames.Items(iSelectedIndex).DisplayText = cmbNames.Text
        'NamesDebug()
    }


    private void cmbNames_SelectionChanged(object sender, System.EventArgs e)
    {
        if (bAllowChangeValue && cmbNames.SelectedIndex > -1 && Not (cmbNames.Text = "" && cmbNames.Items(cmbNames.SelectedIndex).DisplayText <> ""))
        {
            ' text won't match item when we change selection with mouse
            iSelectedIndex = cmbNames.SelectedIndex
            '            Debug.WriteLine("Selected entry " & iSelectedIndex)
            'NamesDebug()
        Else
            If cmbNames.SelectedIndex > -1 Then Debug.WriteLine("text: " + cmbNames.Text + ", item: " + cmbNames.Items(cmbNames.SelectedIndex).DisplayText)
        }
    }


    'Private Sub NamesDebug()
    '    For i As Integer = 0 To cmbNames.Items.Count - 1
    '        Debug.WriteLine("Name " & i & ": " & cmbNames.Items(i).DisplayText)
    '    Next
    'End Sub

    private void frmObject_Shown(object sender, System.EventArgs e)
    {
        txtPrefix.Focus();
    }

    private void frmObject_Load(System.Object sender, System.EventArgs e)
    {
        Me.Properties1.PropertyType = clsProperty.PropertyOfEnum.Objects;
        'Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectStatic32.GetHicon)
        if (Properties1.htblProperties.ContainsKey("StaticOrDynamic"))
        {
            if (Properties1.htblProperties("StaticOrDynamic").StringData.ToString = "Dynamic")
            {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectDynamic16.GetHicon);
            Else
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectStatic16.GetHicon);
            }
        }
    }


    private bool bChangingCheck = false;
    private void chkStatic_CheckedChanged(System.Object sender, System.EventArgs e)
    {

        If bChangingCheck Then Exit Sub
        bChangingCheck = True

        For Each chk As Infragistics.Win.UltraWinEditors.UltraCheckEditor In New Infragistics.Win.UltraWinEditors.UltraCheckEditor() {chkStatic, chkDynamic}
            If chk != sender Then chk.Checked = ! (Infragistics.Win.UltraWinEditors.UltraCheckEditor)(sender).Checked
        Next;

        private string sOldWhere = "";
        If cmbInitialLocation.SelectedItem != null Then sOldWhere = cmbInitialLocation.SelectedItem.DataValue.ToString
        private string sOldKey = cmbLocation2.Key;

        cmbInitialLocation.SuspendLayout();
        cmbLocation2.SuspendLayout();

        cmbInitialLocation.Items.Clear();
        switch (true)
        {
            case sender Is chkStatic:
                {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectStatic16.GetHicon);
                cmbInitialLocation.Items.Add("Hidden", "Hidden");
                cmbInitialLocation.Items.Add("AtLocation", "Single Location");
                cmbInitialLocation.Items.Add("AtLocationGroup", "Location Group");
                cmbInitialLocation.Items.Add("Everywhere", "Everywhere");
                cmbInitialLocation.Items.Add("PartOfWhat", "Part of Object");
                cmbInitialLocation.Items.Add("PartOfWho", "Part of Character");
                If Properties1.htblProperties != null && Properties1.htblProperties.ContainsKey("StaticOrDynamic") Then Properties1.htblProperties("StaticOrDynamic").Value = "Static"
                switch (sOldWhere)
                {
                    case "InLocation":
                        {
                        SetCombo(cmbInitialLocation, "AtLocation");
                        cmbLocation2.Key = sOldKey;
                    case "InsideWhat":
                    case "OnWhat":
                        {
                        SetCombo(cmbInitialLocation, "PartOfWhat");
                        cmbLocation2.Key = sOldKey;
                    case "HeldByWho":
                    case "WornByWho":
                        {
                        SetCombo(cmbInitialLocation, "PartOfWho");
                        cmbLocation2.Key = sOldKey;
                }
            case sender Is chkDynamic:
                {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgObjectDynamic16.GetHicon);
                'For Each sState As String In Adventure.htblObjectProperties("DynamicLocation").arlStates
                '    cmbInitialLocation.Items.Add(sState)
                'Next
                cmbInitialLocation.Items.Add("Hidden", "Hidden");
                cmbInitialLocation.Items.Add("HeldByWho", "Held By Character");
                cmbInitialLocation.Items.Add("WornByWho", "Worn By Character");
                cmbInitialLocation.Items.Add("InLocation", "In Location");
                cmbInitialLocation.Items.Add("InsideWhat", "Inside Object");
                cmbInitialLocation.Items.Add("OnWhat", "On Object");
                If Properties1.htblProperties != null && Properties1.htblProperties.ContainsKey("StaticOrDynamic") Then Properties1.htblProperties("StaticOrDynamic").Value = "Dynamic"
                switch (sOldWhere)
                {
                    case "AtLocation":
                        {
                        SetCombo(cmbInitialLocation, "InLocation");
                        cmbLocation2.Key = sOldKey;
                    case "PartOfWhat":
                        {
                        private clsObject ob = Adventure.htblObjects(sOldKey);
                        if (ob IsNot null)
                        {
                            if (ob.HasSurface)
                            {
                                SetCombo(cmbInitialLocation, "OnWhat");
                                cmbLocation2.Key = sOldKey;
                            ElseIf ob.IsContainer Then
                                SetCombo(cmbInitialLocation, "InsideWhat");
                                cmbLocation2.Key = sOldKey;
                            }
                        }
                    case "PartOfWho":
                        {
                        SetCombo(cmbInitialLocation, "HeldByWho");
                        cmbLocation2.Key = sOldKey;
                }
        }
        If cmbInitialLocation.SelectedItem == null Then cmbInitialLocation.Text = "Hidden"
        If cmbInitialLocation.SelectedItem == null Then cmbInitialLocation.SelectedIndex = 0

        cmbInitialLocation.ResumeLayout();
        cmbLocation2.ResumeLayout();

        Properties1.RefreshProperties();

        bChangingCheck = False
        Changed = True

    }


    private void cmbInitialLocation_SelectionChanged(object sender, System.EventArgs e)
    {

        private bool bEnabled = false;

        cmbLocation2.SuspendLayout();

        'cmbLocation2.Enabled = False
        For Each prop As clsProperty In Adventure.htblObjectProperties.Values
            If prop.Mandatory && ((chkStatic.Checked && prop.DependentKey = "StaticLocation") || (chkDynamic.Checked && prop.DependentKey = "DynamicLocation")) _
                && prop.DependentValue = SafeString(cmbInitialLocation.SelectedItem.DisplayText) Then;
                switch (prop.Type)
                {
                    case clsProperty.PropertyTypeEnum.CharacterKey:
                        {
                        cmbLocation2.AllowedListType = ItemSelector.ItemEnum.Character;
                    case clsProperty.PropertyTypeEnum.LocationGroupKey:
                        {
                        cmbLocation2.AllowedListType = ItemSelector.ItemEnum.LocationGroup;
                    case clsProperty.PropertyTypeEnum.LocationKey:
                        {
                        cmbLocation2.AllowedListType = ItemSelector.ItemEnum.Location;
                    case clsProperty.PropertyTypeEnum.ObjectKey:
                        {
                        cmbLocation2.AllowedListType = ItemSelector.ItemEnum.Object;
                    default:
                        {
                        TODO();
                }
                cmbLocation2.RestrictProperty = prop.RestrictProperty;
                ' TODO - cmbLocation2.RestrictValue = prop.RestrictValue
                'cmbLocation2.Enabled = True
                bEnabled = True
            }
        Next;

        cmbLocation2.Enabled = bEnabled;

        private string sProperty = "";
        if (chkStatic.Checked)
        {
            sProperty = "StaticLocation"
        ElseIf chkDynamic.Checked Then
            sProperty = "DynamicLocation"
        }
        if (Properties1.htblProperties IsNot null && Properties1.htblProperties.ContainsKey(sProperty))
        {
            Properties1.htblProperties(sProperty).Value = cmbInitialLocation.Text;
            Properties1.RefreshProperties();
        }
        Changed = True

        cmbLocation2.ResumeLayout();

    }


    private void cmbLocation2_SelectionChanged(object sender, System.EventArgs e)
    {

        private string sLocation = String.Empty;
        If cmbInitialLocation.SelectedItem != null Then sLocation = SafeString(cmbInitialLocation.SelectedItem.DataValue)
        if (Properties1.htblProperties IsNot null && Properties1.htblProperties.ContainsKey(sLocation))
        {
            Properties1.htblProperties(sLocation).Value = cmbLocation2.Key;
            Properties1.RefreshProperties();
        }
        Changed = True

    }


    private void frmObject_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Objects");
    }

}

}