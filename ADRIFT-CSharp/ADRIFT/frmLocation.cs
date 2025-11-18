using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmLocation
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public bool bKeepOpen = false;

    public void New(ref Location As clsLocation, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmLocation)
            {
                if (CType(w, frmLocation).cLocation.Key = Location.Key && Location.Key IsNot null)
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
        LoadForm(Location, bShow);
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
    Friend WithEvents tabsLocation As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents lblShortDesc As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblLongDesc As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtLongDesc As ADRIFT.GenTextbox;
    Friend WithEvents lblRestrictions As System.Windows.Forms.Label;
    Friend WithEvents lblLocation As System.Windows.Forms.Label;
    Friend WithEvents lblDirection As System.Windows.Forms.Label;
    Friend WithEvents RestrictNorth As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictNorthEast As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictEast As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictSouthEast As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictSouth As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictSouthWest As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictWest As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictNorthWest As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictUp As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictDown As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictIn As ADRIFT.RestrictSummary;
    Friend WithEvents RestrictOut As ADRIFT.RestrictSummary;
    Friend WithEvents pgDescription As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents btnAddObject As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip;
    Friend WithEvents cmbNorth As ADRIFT.ItemSelector;
    Friend WithEvents cmbOut As ADRIFT.ItemSelector;
    Friend WithEvents cmbIn As ADRIFT.ItemSelector;
    Friend WithEvents cmbDown As ADRIFT.ItemSelector;
    Friend WithEvents cmbUp As ADRIFT.ItemSelector;
    Friend WithEvents cmbNorthWest As ADRIFT.ItemSelector;
    Friend WithEvents cmbWest As ADRIFT.ItemSelector;
    Friend WithEvents cmbSouthWest As ADRIFT.ItemSelector;
    Friend WithEvents cmbSouth As ADRIFT.ItemSelector;
    Friend WithEvents cmbSouthEast As ADRIFT.ItemSelector;
    Friend WithEvents cmbEast As ADRIFT.ItemSelector;
    Friend WithEvents cmbNorthEast As ADRIFT.ItemSelector;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents Properties1 As ADRIFT.Properties;
    Friend WithEvents txtShortDesc As ADRIFT.ExpandableDescription;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents Folder1 As ADRIFT.Folder;
    Friend WithEvents btnRemoveItem As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents lblNorth As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblOut As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblIn As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblDown As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblUp As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblNorthWest As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblWest As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblSouthWest As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblSouth As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblSouthEast As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblEast As System.Windows.Forms.LinkLabel;
    Friend WithEvents lblNorthEast As System.Windows.Forms.LinkLabel;
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents chkHideOnMap As System.Windows.Forms.CheckBox;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnAddCharacter As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnAddDynamicOb As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents pgDirections As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        Me.pgDescription = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.txtShortDesc = New ADRIFT.ExpandableDescription();
        Me.txtLongDesc = New ADRIFT.GenTextbox();
        Me.lblLongDesc = New Infragistics.Win.Misc.UltraLabel();
        Me.lblShortDesc = New Infragistics.Win.Misc.UltraLabel();
        Me.pgDirections = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblOut = New System.Windows.Forms.LinkLabel();
        Me.lblIn = New System.Windows.Forms.LinkLabel();
        Me.lblDown = New System.Windows.Forms.LinkLabel();
        Me.lblUp = New System.Windows.Forms.LinkLabel();
        Me.lblNorthWest = New System.Windows.Forms.LinkLabel();
        Me.lblWest = New System.Windows.Forms.LinkLabel();
        Me.lblSouthWest = New System.Windows.Forms.LinkLabel();
        Me.lblSouth = New System.Windows.Forms.LinkLabel();
        Me.lblSouthEast = New System.Windows.Forms.LinkLabel();
        Me.lblEast = New System.Windows.Forms.LinkLabel();
        Me.lblNorthEast = New System.Windows.Forms.LinkLabel();
        Me.lblNorth = New System.Windows.Forms.LinkLabel();
        Me.RestrictOut = New ADRIFT.RestrictSummary();
        Me.RestrictIn = New ADRIFT.RestrictSummary();
        Me.RestrictDown = New ADRIFT.RestrictSummary();
        Me.RestrictUp = New ADRIFT.RestrictSummary();
        Me.RestrictNorthWest = New ADRIFT.RestrictSummary();
        Me.RestrictWest = New ADRIFT.RestrictSummary();
        Me.RestrictSouthWest = New ADRIFT.RestrictSummary();
        Me.RestrictSouth = New ADRIFT.RestrictSummary();
        Me.RestrictSouthEast = New ADRIFT.RestrictSummary();
        Me.RestrictEast = New ADRIFT.RestrictSummary();
        Me.RestrictNorthEast = New ADRIFT.RestrictSummary();
        Me.RestrictNorth = New ADRIFT.RestrictSummary();
        Me.cmbOut = New ADRIFT.ItemSelector();
        Me.cmbIn = New ADRIFT.ItemSelector();
        Me.cmbDown = New ADRIFT.ItemSelector();
        Me.cmbUp = New ADRIFT.ItemSelector();
        Me.cmbNorthWest = New ADRIFT.ItemSelector();
        Me.cmbWest = New ADRIFT.ItemSelector();
        Me.cmbSouthWest = New ADRIFT.ItemSelector();
        Me.cmbSouth = New ADRIFT.ItemSelector();
        Me.cmbSouthEast = New ADRIFT.ItemSelector();
        Me.cmbEast = New ADRIFT.ItemSelector();
        Me.cmbNorthEast = New ADRIFT.ItemSelector();
        Me.cmbNorth = New ADRIFT.ItemSelector();
        Me.lblRestrictions = New System.Windows.Forms.Label();
        Me.lblLocation = New System.Windows.Forms.Label();
        Me.lblDirection = New System.Windows.Forms.Label();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.Properties1 = New ADRIFT.Properties();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.btnAddCharacter = New Infragistics.Win.Misc.UltraButton();
        Me.btnAddDynamicOb = New Infragistics.Win.Misc.UltraButton();
        Me.btnRemoveItem = New Infragistics.Win.Misc.UltraButton();
        Me.Folder1 = New ADRIFT.Folder();
        Me.btnAddObject = New Infragistics.Win.Misc.UltraButton();
        Me.chkHideOnMap = New System.Windows.Forms.CheckBox();
        Me.tabsLocation = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components);
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.pgDescription.SuspendLayout();
        Me.pgDirections.SuspendLayout();
        Me.UltraTabPageControl1.SuspendLayout();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.tabsLocation).BeginInit();
        Me.tabsLocation.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        Me.UltraStatusBar1.SuspendLayout();
        Me.SuspendLayout();
        '
        'pgDescription
        '
        Me.pgDescription.Controls.Add(Me.txtShortDesc);
        Me.pgDescription.Controls.Add(Me.txtLongDesc);
        Me.pgDescription.Controls.Add(Me.lblLongDesc);
        Me.pgDescription.Controls.Add(Me.lblShortDesc);
        Me.pgDescription.Location = New System.Drawing.Point(-10000, -10000);
        Me.pgDescription.Name = "pgDescription";
        Me.pgDescription.Size = New System.Drawing.Size(566, 445);
        '
        'txtShortDesc
        '
        Me.txtShortDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtShortDesc.Location = New System.Drawing.Point(118, 16);
        Me.txtShortDesc.Name = "txtShortDesc";
        Me.txtShortDesc.Size = New System.Drawing.Size(428, 22);
        Me.txtShortDesc.TabIndex = 1;
        '
        'txtLongDesc
        '
        Me.txtLongDesc.AllowAlternateDescriptions = true;
        Me.txtLongDesc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtLongDesc.BackColor = System.Drawing.Color.Transparent;
        Me.txtLongDesc.FirstTabHasRestrictions = false;
        Me.txtLongDesc.Location = New System.Drawing.Point(16, 72);
        Me.txtLongDesc.Name = "txtLongDesc";
        Me.txtLongDesc.sCommand = null;
        Me.txtLongDesc.Size = New System.Drawing.Size(530, 363);
        Me.txtLongDesc.TabIndex = 2;
        '
        'lblLongDesc
        '
        Me.lblLongDesc.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblLongDesc.Location = New System.Drawing.Point(16, 51);
        Me.lblLongDesc.Name = "lblLongDesc";
        Me.lblLongDesc.Size = New System.Drawing.Size(128, 16);
        Me.lblLongDesc.TabIndex = 2;
        Me.lblLongDesc.Text = "Long description:";
        '
        'lblShortDesc
        '
        Me.lblShortDesc.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblShortDesc.Location = New System.Drawing.Point(16, 19);
        Me.lblShortDesc.Name = "lblShortDesc";
        Me.lblShortDesc.Size = New System.Drawing.Size(128, 16);
        Me.lblShortDesc.TabIndex = 0;
        Me.lblShortDesc.Text = "Short description:";
        '
        'pgDirections
        '
        Me.pgDirections.Controls.Add(Me.lblOut);
        Me.pgDirections.Controls.Add(Me.lblIn);
        Me.pgDirections.Controls.Add(Me.lblDown);
        Me.pgDirections.Controls.Add(Me.lblUp);
        Me.pgDirections.Controls.Add(Me.lblNorthWest);
        Me.pgDirections.Controls.Add(Me.lblWest);
        Me.pgDirections.Controls.Add(Me.lblSouthWest);
        Me.pgDirections.Controls.Add(Me.lblSouth);
        Me.pgDirections.Controls.Add(Me.lblSouthEast);
        Me.pgDirections.Controls.Add(Me.lblEast);
        Me.pgDirections.Controls.Add(Me.lblNorthEast);
        Me.pgDirections.Controls.Add(Me.lblNorth);
        Me.pgDirections.Controls.Add(Me.RestrictOut);
        Me.pgDirections.Controls.Add(Me.RestrictIn);
        Me.pgDirections.Controls.Add(Me.RestrictDown);
        Me.pgDirections.Controls.Add(Me.RestrictUp);
        Me.pgDirections.Controls.Add(Me.RestrictNorthWest);
        Me.pgDirections.Controls.Add(Me.RestrictWest);
        Me.pgDirections.Controls.Add(Me.RestrictSouthWest);
        Me.pgDirections.Controls.Add(Me.RestrictSouth);
        Me.pgDirections.Controls.Add(Me.RestrictSouthEast);
        Me.pgDirections.Controls.Add(Me.RestrictEast);
        Me.pgDirections.Controls.Add(Me.RestrictNorthEast);
        Me.pgDirections.Controls.Add(Me.RestrictNorth);
        Me.pgDirections.Controls.Add(Me.cmbOut);
        Me.pgDirections.Controls.Add(Me.cmbIn);
        Me.pgDirections.Controls.Add(Me.cmbDown);
        Me.pgDirections.Controls.Add(Me.cmbUp);
        Me.pgDirections.Controls.Add(Me.cmbNorthWest);
        Me.pgDirections.Controls.Add(Me.cmbWest);
        Me.pgDirections.Controls.Add(Me.cmbSouthWest);
        Me.pgDirections.Controls.Add(Me.cmbSouth);
        Me.pgDirections.Controls.Add(Me.cmbSouthEast);
        Me.pgDirections.Controls.Add(Me.cmbEast);
        Me.pgDirections.Controls.Add(Me.cmbNorthEast);
        Me.pgDirections.Controls.Add(Me.cmbNorth);
        Me.pgDirections.Controls.Add(Me.lblRestrictions);
        Me.pgDirections.Controls.Add(Me.lblLocation);
        Me.pgDirections.Controls.Add(Me.lblDirection);
        Me.pgDirections.Location = New System.Drawing.Point(-10000, -10000);
        Me.pgDirections.Name = "pgDirections";
        Me.pgDirections.Size = New System.Drawing.Size(566, 445);
        '
        'lblOut
        '
        Me.lblOut.AutoSize = true;
        Me.lblOut.BackColor = System.Drawing.Color.Transparent;
        Me.lblOut.LinkArea = New System.Windows.Forms.LinkArea(5, 3);
        Me.lblOut.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblOut.Location = New System.Drawing.Point(11, 412);
        Me.lblOut.Name = "lblOut";
        Me.lblOut.Size = New System.Drawing.Size(66, 17);
        Me.lblOut.TabIndex = 63;
        Me.lblOut.TabStop = true;
        Me.lblOut.Text = "Move Out to";
        Me.lblOut.UseCompatibleTextRendering = true;
        '
        'lblIn
        '
        Me.lblIn.AutoSize = true;
        Me.lblIn.BackColor = System.Drawing.Color.Transparent;
        Me.lblIn.LinkArea = New System.Windows.Forms.LinkArea(5, 2);
        Me.lblIn.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblIn.Location = New System.Drawing.Point(11, 380);
        Me.lblIn.Name = "lblIn";
        Me.lblIn.Size = New System.Drawing.Size(57, 17);
        Me.lblIn.TabIndex = 62;
        Me.lblIn.TabStop = true;
        Me.lblIn.Text = "Move In to";
        Me.lblIn.UseCompatibleTextRendering = true;
        '
        'lblDown
        '
        Me.lblDown.AutoSize = true;
        Me.lblDown.BackColor = System.Drawing.Color.Transparent;
        Me.lblDown.LinkArea = New System.Windows.Forms.LinkArea(5, 4);
        Me.lblDown.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblDown.Location = New System.Drawing.Point(11, 348);
        Me.lblDown.Name = "lblDown";
        Me.lblDown.Size = New System.Drawing.Size(76, 17);
        Me.lblDown.TabIndex = 61;
        Me.lblDown.TabStop = true;
        Me.lblDown.Text = "Move Down to";
        Me.lblDown.UseCompatibleTextRendering = true;
        '
        'lblUp
        '
        Me.lblUp.AutoSize = true;
        Me.lblUp.BackColor = System.Drawing.Color.Transparent;
        Me.lblUp.LinkArea = New System.Windows.Forms.LinkArea(5, 2);
        Me.lblUp.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblUp.Location = New System.Drawing.Point(11, 316);
        Me.lblUp.Name = "lblUp";
        Me.lblUp.Size = New System.Drawing.Size(62, 17);
        Me.lblUp.TabIndex = 60;
        Me.lblUp.TabStop = true;
        Me.lblUp.Text = "Move Up to";
        Me.lblUp.UseCompatibleTextRendering = true;
        '
        'lblNorthWest
        '
        Me.lblNorthWest.AutoSize = true;
        Me.lblNorthWest.BackColor = System.Drawing.Color.Transparent;
        Me.lblNorthWest.LinkArea = New System.Windows.Forms.LinkArea(5, 9);
        Me.lblNorthWest.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblNorthWest.Location = New System.Drawing.Point(11, 284);
        Me.lblNorthWest.Name = "lblNorthWest";
        Me.lblNorthWest.Size = New System.Drawing.Size(101, 17);
        Me.lblNorthWest.TabIndex = 59;
        Me.lblNorthWest.TabStop = true;
        Me.lblNorthWest.Text = "Move NorthWest to";
        Me.lblNorthWest.UseCompatibleTextRendering = true;
        '
        'lblWest
        '
        Me.lblWest.AutoSize = true;
        Me.lblWest.BackColor = System.Drawing.Color.Transparent;
        Me.lblWest.LinkArea = New System.Windows.Forms.LinkArea(5, 4);
        Me.lblWest.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblWest.Location = New System.Drawing.Point(11, 252);
        Me.lblWest.Name = "lblWest";
        Me.lblWest.Size = New System.Drawing.Size(73, 17);
        Me.lblWest.TabIndex = 58;
        Me.lblWest.TabStop = true;
        Me.lblWest.Text = "Move West to";
        Me.lblWest.UseCompatibleTextRendering = true;
        '
        'lblSouthWest
        '
        Me.lblSouthWest.AutoSize = true;
        Me.lblSouthWest.BackColor = System.Drawing.Color.Transparent;
        Me.lblSouthWest.LinkArea = New System.Windows.Forms.LinkArea(5, 9);
        Me.lblSouthWest.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblSouthWest.Location = New System.Drawing.Point(11, 220);
        Me.lblSouthWest.Name = "lblSouthWest";
        Me.lblSouthWest.Size = New System.Drawing.Size(103, 17);
        Me.lblSouthWest.TabIndex = 57;
        Me.lblSouthWest.TabStop = true;
        Me.lblSouthWest.Text = "Move SouthWest to";
        Me.lblSouthWest.UseCompatibleTextRendering = true;
        '
        'lblSouth
        '
        Me.lblSouth.AutoSize = true;
        Me.lblSouth.BackColor = System.Drawing.Color.Transparent;
        Me.lblSouth.LinkArea = New System.Windows.Forms.LinkArea(5, 5);
        Me.lblSouth.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblSouth.Location = New System.Drawing.Point(11, 188);
        Me.lblSouth.Name = "lblSouth";
        Me.lblSouth.Size = New System.Drawing.Size(77, 17);
        Me.lblSouth.TabIndex = 56;
        Me.lblSouth.TabStop = true;
        Me.lblSouth.Text = "Move South to";
        Me.lblSouth.UseCompatibleTextRendering = true;
        '
        'lblSouthEast
        '
        Me.lblSouthEast.AutoSize = true;
        Me.lblSouthEast.BackColor = System.Drawing.Color.Transparent;
        Me.lblSouthEast.LinkArea = New System.Windows.Forms.LinkArea(5, 9);
        Me.lblSouthEast.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblSouthEast.Location = New System.Drawing.Point(11, 156);
        Me.lblSouthEast.Name = "lblSouthEast";
        Me.lblSouthEast.Size = New System.Drawing.Size(100, 17);
        Me.lblSouthEast.TabIndex = 55;
        Me.lblSouthEast.TabStop = true;
        Me.lblSouthEast.Text = "Move SouthEast to";
        Me.lblSouthEast.UseCompatibleTextRendering = true;
        '
        'lblEast
        '
        Me.lblEast.AutoSize = true;
        Me.lblEast.BackColor = System.Drawing.Color.Transparent;
        Me.lblEast.LinkArea = New System.Windows.Forms.LinkArea(5, 4);
        Me.lblEast.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblEast.Location = New System.Drawing.Point(11, 124);
        Me.lblEast.Name = "lblEast";
        Me.lblEast.Size = New System.Drawing.Size(70, 17);
        Me.lblEast.TabIndex = 54;
        Me.lblEast.TabStop = true;
        Me.lblEast.Text = "Move East to";
        Me.lblEast.UseCompatibleTextRendering = true;
        '
        'lblNorthEast
        '
        Me.lblNorthEast.AutoSize = true;
        Me.lblNorthEast.BackColor = System.Drawing.Color.Transparent;
        Me.lblNorthEast.LinkArea = New System.Windows.Forms.LinkArea(5, 9);
        Me.lblNorthEast.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblNorthEast.Location = New System.Drawing.Point(11, 92);
        Me.lblNorthEast.Name = "lblNorthEast";
        Me.lblNorthEast.Size = New System.Drawing.Size(98, 17);
        Me.lblNorthEast.TabIndex = 53;
        Me.lblNorthEast.TabStop = true;
        Me.lblNorthEast.Text = "Move NorthEast to";
        Me.lblNorthEast.UseCompatibleTextRendering = true;
        '
        'lblNorth
        '
        Me.lblNorth.AutoSize = true;
        Me.lblNorth.BackColor = System.Drawing.Color.Transparent;
        Me.lblNorth.LinkArea = New System.Windows.Forms.LinkArea(5, 5);
        Me.lblNorth.LinkColor = System.Drawing.Color.DarkBlue;
        Me.lblNorth.Location = New System.Drawing.Point(11, 61);
        Me.lblNorth.Name = "lblNorth";
        Me.lblNorth.Size = New System.Drawing.Size(75, 17);
        Me.lblNorth.TabIndex = 52;
        Me.lblNorth.TabStop = true;
        Me.lblNorth.Text = "Move North to";
        Me.lblNorth.UseCompatibleTextRendering = true;
        '
        'RestrictOut
        '
        Me.RestrictOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictOut.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictOut.Location = New System.Drawing.Point(326, 408);
        Me.RestrictOut.Name = "RestrictOut";
        Me.RestrictOut.Size = New System.Drawing.Size(234, 21);
        Me.RestrictOut.TabIndex = 38;
        '
        'RestrictIn
        '
        Me.RestrictIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictIn.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictIn.Location = New System.Drawing.Point(326, 376);
        Me.RestrictIn.Name = "RestrictIn";
        Me.RestrictIn.Size = New System.Drawing.Size(234, 21);
        Me.RestrictIn.TabIndex = 35;
        '
        'RestrictDown
        '
        Me.RestrictDown.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictDown.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictDown.Location = New System.Drawing.Point(326, 344);
        Me.RestrictDown.Name = "RestrictDown";
        Me.RestrictDown.Size = New System.Drawing.Size(234, 21);
        Me.RestrictDown.TabIndex = 32;
        '
        'RestrictUp
        '
        Me.RestrictUp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictUp.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictUp.Location = New System.Drawing.Point(326, 312);
        Me.RestrictUp.Name = "RestrictUp";
        Me.RestrictUp.Size = New System.Drawing.Size(234, 21);
        Me.RestrictUp.TabIndex = 29;
        '
        'RestrictNorthWest
        '
        Me.RestrictNorthWest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictNorthWest.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictNorthWest.Location = New System.Drawing.Point(326, 280);
        Me.RestrictNorthWest.Name = "RestrictNorthWest";
        Me.RestrictNorthWest.Size = New System.Drawing.Size(234, 21);
        Me.RestrictNorthWest.TabIndex = 26;
        '
        'RestrictWest
        '
        Me.RestrictWest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictWest.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictWest.Location = New System.Drawing.Point(326, 248);
        Me.RestrictWest.Name = "RestrictWest";
        Me.RestrictWest.Size = New System.Drawing.Size(234, 21);
        Me.RestrictWest.TabIndex = 23;
        '
        'RestrictSouthWest
        '
        Me.RestrictSouthWest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictSouthWest.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictSouthWest.Location = New System.Drawing.Point(326, 216);
        Me.RestrictSouthWest.Name = "RestrictSouthWest";
        Me.RestrictSouthWest.Size = New System.Drawing.Size(234, 21);
        Me.RestrictSouthWest.TabIndex = 20;
        '
        'RestrictSouth
        '
        Me.RestrictSouth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictSouth.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictSouth.Location = New System.Drawing.Point(326, 184);
        Me.RestrictSouth.Name = "RestrictSouth";
        Me.RestrictSouth.Size = New System.Drawing.Size(234, 21);
        Me.RestrictSouth.TabIndex = 17;
        '
        'RestrictSouthEast
        '
        Me.RestrictSouthEast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictSouthEast.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictSouthEast.Location = New System.Drawing.Point(326, 152);
        Me.RestrictSouthEast.Name = "RestrictSouthEast";
        Me.RestrictSouthEast.Size = New System.Drawing.Size(234, 21);
        Me.RestrictSouthEast.TabIndex = 14;
        '
        'RestrictEast
        '
        Me.RestrictEast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictEast.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictEast.Location = New System.Drawing.Point(326, 120);
        Me.RestrictEast.Name = "RestrictEast";
        Me.RestrictEast.Size = New System.Drawing.Size(234, 21);
        Me.RestrictEast.TabIndex = 11;
        '
        'RestrictNorthEast
        '
        Me.RestrictNorthEast.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictNorthEast.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictNorthEast.Location = New System.Drawing.Point(326, 88);
        Me.RestrictNorthEast.Name = "RestrictNorthEast";
        Me.RestrictNorthEast.Size = New System.Drawing.Size(234, 21);
        Me.RestrictNorthEast.TabIndex = 8;
        '
        'RestrictNorth
        '
        Me.RestrictNorth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictNorth.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictNorth.Location = New System.Drawing.Point(326, 56);
        Me.RestrictNorth.Name = "RestrictNorth";
        Me.RestrictNorth.Size = New System.Drawing.Size(234, 21);
        Me.RestrictNorth.TabIndex = 5;
        '
        'cmbOut
        '
        Me.cmbOut.AllowAddEdit = true;
        Me.cmbOut.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbOut.AllowHidden = false;
        Me.cmbOut.BackColor = System.Drawing.Color.Transparent;
        Me.cmbOut.Key = null;
        Me.cmbOut.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbOut.Location = New System.Drawing.Point(111, 408);
        Me.cmbOut.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbOut.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbOut.Name = "cmbOut";
        Me.cmbOut.RestrictProperty = null;
        Me.cmbOut.Size = New System.Drawing.Size(207, 21);
        Me.cmbOut.TabIndex = 51;
        '
        'cmbIn
        '
        Me.cmbIn.AllowAddEdit = true;
        Me.cmbIn.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbIn.AllowHidden = false;
        Me.cmbIn.BackColor = System.Drawing.Color.Transparent;
        Me.cmbIn.Key = null;
        Me.cmbIn.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbIn.Location = New System.Drawing.Point(111, 376);
        Me.cmbIn.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbIn.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbIn.Name = "cmbIn";
        Me.cmbIn.RestrictProperty = null;
        Me.cmbIn.Size = New System.Drawing.Size(207, 21);
        Me.cmbIn.TabIndex = 50;
        '
        'cmbDown
        '
        Me.cmbDown.AllowAddEdit = true;
        Me.cmbDown.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbDown.AllowHidden = false;
        Me.cmbDown.BackColor = System.Drawing.Color.Transparent;
        Me.cmbDown.Key = null;
        Me.cmbDown.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbDown.Location = New System.Drawing.Point(111, 344);
        Me.cmbDown.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbDown.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbDown.Name = "cmbDown";
        Me.cmbDown.RestrictProperty = null;
        Me.cmbDown.Size = New System.Drawing.Size(207, 21);
        Me.cmbDown.TabIndex = 49;
        '
        'cmbUp
        '
        Me.cmbUp.AllowAddEdit = true;
        Me.cmbUp.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbUp.AllowHidden = false;
        Me.cmbUp.BackColor = System.Drawing.Color.Transparent;
        Me.cmbUp.Key = null;
        Me.cmbUp.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbUp.Location = New System.Drawing.Point(111, 312);
        Me.cmbUp.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbUp.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbUp.Name = "cmbUp";
        Me.cmbUp.RestrictProperty = null;
        Me.cmbUp.Size = New System.Drawing.Size(207, 21);
        Me.cmbUp.TabIndex = 48;
        '
        'cmbNorthWest
        '
        Me.cmbNorthWest.AllowAddEdit = true;
        Me.cmbNorthWest.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbNorthWest.AllowHidden = false;
        Me.cmbNorthWest.BackColor = System.Drawing.Color.Transparent;
        Me.cmbNorthWest.Key = null;
        Me.cmbNorthWest.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbNorthWest.Location = New System.Drawing.Point(111, 280);
        Me.cmbNorthWest.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbNorthWest.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbNorthWest.Name = "cmbNorthWest";
        Me.cmbNorthWest.RestrictProperty = null;
        Me.cmbNorthWest.Size = New System.Drawing.Size(207, 21);
        Me.cmbNorthWest.TabIndex = 47;
        '
        'cmbWest
        '
        Me.cmbWest.AllowAddEdit = true;
        Me.cmbWest.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbWest.AllowHidden = false;
        Me.cmbWest.BackColor = System.Drawing.Color.Transparent;
        Me.cmbWest.Key = null;
        Me.cmbWest.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbWest.Location = New System.Drawing.Point(111, 248);
        Me.cmbWest.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbWest.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbWest.Name = "cmbWest";
        Me.cmbWest.RestrictProperty = null;
        Me.cmbWest.Size = New System.Drawing.Size(207, 21);
        Me.cmbWest.TabIndex = 46;
        '
        'cmbSouthWest
        '
        Me.cmbSouthWest.AllowAddEdit = true;
        Me.cmbSouthWest.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbSouthWest.AllowHidden = false;
        Me.cmbSouthWest.BackColor = System.Drawing.Color.Transparent;
        Me.cmbSouthWest.Key = null;
        Me.cmbSouthWest.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbSouthWest.Location = New System.Drawing.Point(111, 216);
        Me.cmbSouthWest.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbSouthWest.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbSouthWest.Name = "cmbSouthWest";
        Me.cmbSouthWest.RestrictProperty = null;
        Me.cmbSouthWest.Size = New System.Drawing.Size(207, 21);
        Me.cmbSouthWest.TabIndex = 45;
        '
        'cmbSouth
        '
        Me.cmbSouth.AllowAddEdit = true;
        Me.cmbSouth.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbSouth.AllowHidden = false;
        Me.cmbSouth.BackColor = System.Drawing.Color.Transparent;
        Me.cmbSouth.Key = null;
        Me.cmbSouth.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbSouth.Location = New System.Drawing.Point(111, 184);
        Me.cmbSouth.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbSouth.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbSouth.Name = "cmbSouth";
        Me.cmbSouth.RestrictProperty = null;
        Me.cmbSouth.Size = New System.Drawing.Size(207, 21);
        Me.cmbSouth.TabIndex = 44;
        '
        'cmbSouthEast
        '
        Me.cmbSouthEast.AllowAddEdit = true;
        Me.cmbSouthEast.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbSouthEast.AllowHidden = false;
        Me.cmbSouthEast.BackColor = System.Drawing.Color.Transparent;
        Me.cmbSouthEast.Key = null;
        Me.cmbSouthEast.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbSouthEast.Location = New System.Drawing.Point(111, 152);
        Me.cmbSouthEast.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbSouthEast.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbSouthEast.Name = "cmbSouthEast";
        Me.cmbSouthEast.RestrictProperty = null;
        Me.cmbSouthEast.Size = New System.Drawing.Size(207, 21);
        Me.cmbSouthEast.TabIndex = 43;
        '
        'cmbEast
        '
        Me.cmbEast.AllowAddEdit = true;
        Me.cmbEast.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbEast.AllowHidden = false;
        Me.cmbEast.BackColor = System.Drawing.Color.Transparent;
        Me.cmbEast.Key = null;
        Me.cmbEast.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbEast.Location = New System.Drawing.Point(111, 120);
        Me.cmbEast.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbEast.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbEast.Name = "cmbEast";
        Me.cmbEast.RestrictProperty = null;
        Me.cmbEast.Size = New System.Drawing.Size(207, 21);
        Me.cmbEast.TabIndex = 42;
        '
        'cmbNorthEast
        '
        Me.cmbNorthEast.AllowAddEdit = true;
        Me.cmbNorthEast.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbNorthEast.AllowHidden = false;
        Me.cmbNorthEast.BackColor = System.Drawing.Color.Transparent;
        Me.cmbNorthEast.Key = null;
        Me.cmbNorthEast.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbNorthEast.Location = New System.Drawing.Point(111, 88);
        Me.cmbNorthEast.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbNorthEast.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbNorthEast.Name = "cmbNorthEast";
        Me.cmbNorthEast.RestrictProperty = null;
        Me.cmbNorthEast.Size = New System.Drawing.Size(207, 21);
        Me.cmbNorthEast.TabIndex = 41;
        '
        'cmbNorth
        '
        Me.cmbNorth.AllowAddEdit = true;
        Me.cmbNorth.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.cmbNorth.AllowHidden = false;
        Me.cmbNorth.BackColor = System.Drawing.Color.Transparent;
        Me.cmbNorth.Key = null;
        Me.cmbNorth.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbNorth.Location = New System.Drawing.Point(111, 56);
        Me.cmbNorth.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbNorth.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbNorth.Name = "cmbNorth";
        Me.cmbNorth.RestrictProperty = null;
        Me.cmbNorth.Size = New System.Drawing.Size(207, 21);
        Me.cmbNorth.TabIndex = 40;
        '
        'lblRestrictions
        '
        Me.lblRestrictions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lblRestrictions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.lblRestrictions.Location = New System.Drawing.Point(326, 24);
        Me.lblRestrictions.Name = "lblRestrictions";
        Me.lblRestrictions.Size = New System.Drawing.Size(216, 20);
        Me.lblRestrictions.TabIndex = 4;
        Me.lblRestrictions.Text = "Restrictions";
        Me.lblRestrictions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        '
        'lblLocation
        '
        Me.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.lblLocation.Location = New System.Drawing.Point(133, 24);
        Me.lblLocation.Name = "lblLocation";
        Me.lblLocation.Size = New System.Drawing.Size(161, 20);
        Me.lblLocation.TabIndex = 3;
        Me.lblLocation.Text = "Location";
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        '
        'lblDirection
        '
        Me.lblDirection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.lblDirection.Location = New System.Drawing.Point(11, 24);
        Me.lblDirection.Name = "lblDirection";
        Me.lblDirection.Size = New System.Drawing.Size(96, 20);
        Me.lblDirection.TabIndex = 2;
        Me.lblDirection.Text = "Direction";
        Me.lblDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Properties1);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(566, 445);
        '
        'Properties1
        '
        Me.Properties1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Properties1.BackColor = System.Drawing.Color.Transparent;
        Me.Properties1.Location = New System.Drawing.Point(8, 8);
        Me.Properties1.Name = "Properties1";
        Me.Properties1.Size = New System.Drawing.Size(551, 434);
        Me.Properties1.TabIndex = 1;
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.btnAddCharacter);
        Me.UltraTabPageControl2.Controls.Add(Me.btnAddDynamicOb);
        Me.UltraTabPageControl2.Controls.Add(Me.btnRemoveItem);
        Me.UltraTabPageControl2.Controls.Add(Me.Folder1);
        Me.UltraTabPageControl2.Controls.Add(Me.btnAddObject);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 22);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(566, 445);
        '
        'btnAddCharacter
        '
        Me.btnAddCharacter.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Appearance1.Image = Global.ADRIFT.My.Resources.Resources.imgCharacter16;
        Me.btnAddCharacter.Appearance = Appearance1;
        Me.btnAddCharacter.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
        Me.btnAddCharacter.Location = New System.Drawing.Point(297, 413);
        Me.btnAddCharacter.Name = "btnAddCharacter";
        Me.btnAddCharacter.Size = New System.Drawing.Size(122, 25);
        Me.btnAddCharacter.TabIndex = 10;
        Me.btnAddCharacter.Text = "Add Character";
        Me.ToolTip1.SetToolTip(Me.btnAddCharacter, "Add Character to this Location");
        '
        'btnAddDynamicOb
        '
        Me.btnAddDynamicOb.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Appearance2.Image = Global.ADRIFT.My.Resources.Resources.imgObjectDynamic16;
        Me.btnAddDynamicOb.Appearance = Appearance2;
        Me.btnAddDynamicOb.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
        Me.btnAddDynamicOb.Location = New System.Drawing.Point(148, 413);
        Me.btnAddDynamicOb.Name = "btnAddDynamicOb";
        Me.btnAddDynamicOb.Size = New System.Drawing.Size(143, 25);
        Me.btnAddDynamicOb.TabIndex = 9;
        Me.btnAddDynamicOb.Text = "Add Dynamic Object";
        Me.ToolTip1.SetToolTip(Me.btnAddDynamicOb, "Add Dynamic Object to this Location");
        '
        'btnRemoveItem
        '
        Me.btnRemoveItem.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgDelete;
        Me.btnRemoveItem.Appearance = Appearance3;
        Me.btnRemoveItem.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
        Me.btnRemoveItem.Location = New System.Drawing.Point(425, 413);
        Me.btnRemoveItem.Name = "btnRemoveItem";
        Me.btnRemoveItem.Size = New System.Drawing.Size(134, 25);
        Me.btnRemoveItem.TabIndex = 8;
        Me.btnRemoveItem.Text = "Remove Item(s)";
        '
        'Folder1
        '
        Me.Folder1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Folder1.folder = null;
        Me.Folder1.Location = New System.Drawing.Point(8, 8);
        Me.Folder1.Name = "Folder1";
        Me.Folder1.Size = New System.Drawing.Size(551, 402);
        Me.Folder1.TabIndex = 7;
        Me.Folder1.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.List;
        '
        'btnAddObject
        '
        Me.btnAddObject.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Appearance4.BackColor = System.Drawing.Color.Transparent;
        Appearance4.Image = Global.ADRIFT.My.Resources.Resources.imgObjectStatic16;
        Me.btnAddObject.Appearance = Appearance4;
        Me.btnAddObject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
        Me.btnAddObject.Location = New System.Drawing.Point(8, 413);
        Me.btnAddObject.Name = "btnAddObject";
        Me.btnAddObject.Size = New System.Drawing.Size(134, 25);
        Me.btnAddObject.TabIndex = 6;
        Me.btnAddObject.Text = "Add Static Object";
        Me.ToolTip1.SetToolTip(Me.btnAddObject, "Add Static Object to this Location");
        '
        'chkHideOnMap
        '
        Me.chkHideOnMap.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.chkHideOnMap.AutoSize = true;
        Me.chkHideOnMap.BackColor = System.Drawing.Color.Transparent;
        Me.chkHideOnMap.Location = New System.Drawing.Point(13, 18);
        Me.chkHideOnMap.Name = "chkHideOnMap";
        Me.chkHideOnMap.Size = New System.Drawing.Size(131, 17);
        Me.chkHideOnMap.TabIndex = 34;
        Me.chkHideOnMap.Text = "Hide Location on Map";
        Me.chkHideOnMap.UseVisualStyleBackColor = false;
        '
        'tabsLocation
        '
        Me.tabsLocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.tabsLocation.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsLocation.Controls.Add(Me.pgDescription);
        Me.tabsLocation.Controls.Add(Me.pgDirections);
        Me.tabsLocation.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsLocation.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsLocation.Location = New System.Drawing.Point(0, 0);
        Me.tabsLocation.Name = "tabsLocation";
        Me.tabsLocation.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsLocation.Size = New System.Drawing.Size(568, 468);
        Me.tabsLocation.TabIndex = 0;
        UltraTab1.Key = "tabDescription";
        UltraTab1.TabPage = Me.pgDescription;
        UltraTab1.Text = "Description";
        UltraTab2.Key = "tabDirections";
        UltraTab2.TabPage = Me.pgDirections;
        UltraTab2.Text = "Directions";
        UltraTab3.Key = "tabProperties";
        UltraTab3.TabPage = Me.UltraTabPageControl1;
        UltraTab3.Text = "Properties";
        UltraTab4.Key = "tabContents";
        UltraTab4.TabPage = Me.UltraTabPageControl2;
        UltraTab4.Text = "Contents";
        Me.tabsLocation.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3, UltraTab4});
        Me.tabsLocation.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(566, 445);
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(464, 472);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 15;
        Me.btnApply.Text = "Apply";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(272, 472);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 3;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Controls.Add(Me.chkHideOnMap);
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 462);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        UltraStatusPanel1.Control = Me.chkHideOnMap;
        UltraStatusPanel1.Padding = New System.Drawing.Size(12, 12);
        UltraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
        UltraStatusPanel1.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.ControlContainer;
        UltraStatusPanel1.Width = 160;
        Me.UltraStatusBar1.Panels.AddRange(New Infragistics.Win.UltraWinStatusBar.UltraStatusPanel() {UltraStatusPanel1});
        Me.UltraStatusBar1.Size = New System.Drawing.Size(568, 48);
        Me.UltraStatusBar1.TabIndex = 12;
        Me.UltraStatusBar1.ViewStyle = Infragistics.Win.UltraWinStatusBar.ViewStyle.Office2007;
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(368, 472);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 4;
        Me.btnCancel.Text = "Cancel";
        '
        'frmLocation
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(568, 510);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.Controls.Add(Me.tabsLocation);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(576, 544);
        Me.Name = "frmLocation";
        Me.ShowInTaskbar = false;
        Me.Text = "Location - ";
        Me.pgDescription.ResumeLayout(false);
        Me.pgDirections.ResumeLayout(false);
        Me.pgDirections.PerformLayout();
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.tabsLocation).EndInit();
        Me.tabsLocation.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        Me.UltraStatusBar1.ResumeLayout(false);
        Me.UltraStatusBar1.PerformLayout();
        Me.ResumeLayout(false);

    }

#End Region

    private bool bChanged;
    private clsLocation cLocation;
    private New ArrayList arlCombos;
    private New ArrayList arlRestrictions;


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


    ' Returns True if map ok, or False if we have created an overlap somewhere
    private bool CheckMapValid()
    {

    }


    private bool ValidateLocation()
    {
        if (txtShortDesc.Text = "")
        {
            MessageBox.Show("You must give the location a short description.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tabsLocation.SelectedTab = tabsLocation.Tabs("tabDescription");
            txtShortDesc.Focus();
            return false;
        }
        if (Not Properties1.ValidateProperties)
        {
            tabsLocation.SelectedTab = tabsLocation.Tabs("tabProperties");
            return false;
        }
        return true;
    }


    public bool ApplyLocation()
    {

        If ! ValidateLocation() Then Return false

        With cLocation;
            .ShortDescription = txtShortDesc.Description.Copy;
            .LongDescription = txtLongDesc.Description.Copy;

            for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
            {
                private ADRIFT.ItemSelector cmbDir = CType(arlCombos(eDirection), ADRIFT.ItemSelector);
                private ADRIFT.RestrictSummary Rest = CType(arlRestrictions(eDirection), ADRIFT.RestrictSummary);
                if (cmbDir.Key <> "")
                {
                    .arlDirections(eDirection).LocationKey = cmbDir.Key;
                Else
                    .arlDirections(eDirection).LocationKey = null;
                }
                .arlDirections(eDirection).Restrictions = Rest.arlRestrictions;
            Next;

            .LastUpdated = Now;
            .IsLibrary = false;
            .htblActualProperties = Me.Properties1.htblProperties.CopySelected;
            .HideOnMap = chkHideOnMap.Checked;

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Location");
                Adventure.htblLocations.Add(cLocation, .Key);
                Folder1.sParentKey = .Key;
                Me.Properties1.OwnerKey = .Key;
            }

            UpdateListItem(.Key, .ShortDescription.ToString);
            btnAddObject.Enabled = true;
            btnAddDynamicOb.Enabled = true;
            btnAddCharacter.Enabled = true;

            ' Check reciprocal directions
            for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
            {
                if (.arlDirections(eDirection).LocationKey <> "" && Adventure.htblLocations.ContainsKey(.arlDirections(eDirection).LocationKey))
                {
                    if (Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).LocationKey = "")
                    {
                        switch (YesNoCancel("Would you also like to move " + DirectionName(OppositeDirection(eDirection)) + " from " + Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription.ToString + " to " + .ShortDescription.ToString + "?", "Add reciprocal link", "ReciprocalLink"))
                        {
                            case Windows.Forms.DialogResult.Yes:
                                {
                                Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).LocationKey = .Key;
                            case Windows.Forms.DialogResult.No:
                                {
                                ' Leave as is
                            case Windows.Forms.DialogResult.Cancel:
                                {
                                return false;
                        }
                        'If MessageBox.Show("Would you also like to move " & OppositeDirection(eDirection).ToString & " from " & Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription & " to " & .ShortDescription & "?", "Add reciprocal link", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        '    Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).LocationKey = .Key
                        'End If
                    }
                    if (Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).LocationKey = .Key && .arlDirections(eDirection).Restrictions.Count > 0 && Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).Restrictions.Count = 0)
                    {
                        switch (YesNoCancel("Would you like to copy the restrictions between " + Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription.ToString + " and " + .ShortDescription.ToString + " to " + Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription.ToString + "?", "Add reciprocal restriction", "ReciprocalRestriction"))
                        {
                            case Windows.Forms.DialogResult.Yes:
                                {
                                Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).Restrictions = .arlDirections(eDirection).Restrictions.Copy;
                            case Windows.Forms.DialogResult.No:
                                {
                                ' Leave as is
                            case Windows.Forms.DialogResult.Cancel:
                                {
                                return false;
                        }
                        'If MessageBox.Show("Would you like to copy the restrictions between " & Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription & " and " & .ShortDescription & " to " & Adventure.htblLocations(.arlDirections(eDirection).LocationKey).ShortDescription & "?", "Add reciprocal restriction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        '    Adventure.htblLocations(.arlDirections(eDirection).LocationKey).arlDirections(OppositeDirection(eDirection)).Restrictions = .arlDirections(eDirection).Restrictions.Copy
                        'End If
                    }
                }
            Next;

        }

        Adventure.Map.UpdateMap(cLocation);
        fGenerator.Map1.SelectNode(cLocation.Key);
        Me.Focus();

        Adventure.Changed = true;
        return true;

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        If ! ApplyLocation() Then Exit Sub
        CloseLocation(Me);
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        If ! ApplyLocation() Then Exit Sub
        Changed = False
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyLocation()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseLocation(Me);
    }



    private void LoadForm(ref cLocation As clsLocation, bool bShow)
    {


        Me.cLocation = cLocation;

        arlCombos.Add(cmbNorth);
        arlRestrictions.Add(RestrictNorth);
        arlCombos.Add(cmbEast);
        arlRestrictions.Add(RestrictEast);
        arlCombos.Add(cmbSouth);
        arlRestrictions.Add(RestrictSouth);
        arlCombos.Add(cmbWest);
        arlRestrictions.Add(RestrictWest);
        arlCombos.Add(cmbUp);
        arlRestrictions.Add(RestrictUp);
        arlCombos.Add(cmbDown);
        arlRestrictions.Add(RestrictDown);
        arlCombos.Add(cmbIn);
        arlRestrictions.Add(RestrictIn);
        arlCombos.Add(cmbOut);
        arlRestrictions.Add(RestrictOut);
        arlCombos.Add(cmbNorthEast);
        arlRestrictions.Add(RestrictNorthEast);
        arlCombos.Add(cmbSouthEast);
        arlRestrictions.Add(RestrictSouthEast);
        arlCombos.Add(cmbSouthWest);
        arlRestrictions.Add(RestrictSouthWest);
        arlCombos.Add(cmbNorthWest);
        arlRestrictions.Add(RestrictNorthWest);

        For Each ctrl As Control In tabsLocation.Tabs(1).TabPage.Controls
            if (TypeOf ctrl Is LinkLabel)
            {
                private DirectionsEnum eDirection = GetDirection(CType(ctrl, LinkLabel));
                private string sFirst = Adventure.sDirectionsRE(eDirection);
                If sFirst.Contains("/") Then sFirst = sFirst.Substring(0, sFirst.IndexOf("/"))
                (LinkLabel)(ctrl).Text = "Move " + sFirst + " to";
                (LinkLabel)(ctrl).LinkArea = New LinkArea(5, sFirst.Length);
            }
        Next;
        'For Each cmb As Control In arlCombos
        '    FillComboWithLocations(CType(cmb, Infragistics.Win.UltraWinEditors.UltraComboEditor))
        'Next


        With cLocation;
            Text = "Location - " & .ShortDescription.ToString
            tabsLocation.Tabs("tabProperties").Visible = ! fGenerator.SimpleMode;
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            If .ShortDescription.ToString = "" Then Text = "New Location"
            txtShortDesc.Description = .ShortDescription.Copy;
            txtLongDesc.Description = .LongDescription.Copy;
            chkHideOnMap.Checked = .HideOnMap;

            for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
            {
                private ADRIFT.ItemSelector cmbDir = CType(arlCombos(eDirection), ADRIFT.ItemSelector);
                private ADRIFT.RestrictSummary Rest = CType(arlRestrictions(eDirection), ADRIFT.RestrictSummary);
                if (.arlDirections(eDirection).LocationKey IsNot null && (Adventure.htblLocations.ContainsKey(.arlDirections(eDirection).LocationKey) || Adventure.htblGroups.ContainsKey(.arlDirections(eDirection).LocationKey)))
                {
                    cmbDir.Key = .arlDirections(eDirection).LocationKey 'SetCombo(cmbDir, Adventure.htblLocations(.arlDirections(eDirection).LocationKey).Key, true);
                Else
                    Rest.Enabled = false;
                }
                Rest.LoadRestrictions(.arlDirections(eDirection).Restrictions);
            Next;

            if (.Key = "")
            {
                btnAddObject.Enabled = false;
                btnAddDynamicOb.Enabled = false;
                btnAddCharacter.Enabled = false;
            Else
                btnAddObject.Enabled = true;
                btnAddDynamicOb.Enabled = true;
                btnAddCharacter.Enabled = true;
            }

            ' Pad out the local Location hashtable with unselected properties
            .ResetInherited();
            private PropertyHashTable htblProperties = .htblProperties.Clone ' .GetPropertiesIncludingGroups.Clone ' .htblProperties.Clone;
            For Each prop As clsProperty In Adventure.htblLocationProperties.Values
                If ! htblProperties.ContainsKey(prop.Key) Then htblProperties.Add(prop.Copy)
            Next;
            Me.Properties1.htblProperties = htblProperties;
            Me.Properties1.OwnerKey = .Key;

            Changed = False
        }

        Folder1.sParentKey = cLocation.Key;
        if (cLocation.Key <> "")
        {
            For Each ob As clsObject In Adventure.htblObjects.Values
                If (! ob.IsStatic && ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation && ob.Location.Key = cLocation.Key) _
                    || (ob.IsStatic && (ob.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.AllRooms || (ob.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation && ob.LocationRoots.ContainsKey(cLocation.Key)))) Then;
                    Folder1.AddSingleItem(ob.Key);
                }
            Next;
            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if (ch.Location.LocationKey = cLocation.Key Then ' ch.LocationRoot IsNot null && ch.LocationRoot.Key = cLocation.Key Then ' ch.LocationRoots.ContainsKey(cLocation.Key))
                {
                    Folder1.AddSingleItem(ch.Key);
                }
            Next;
        }

        GetFormPosition(Me);

        If bShow Then Me.Show()

        OpenForms.Add(Me);

    }


    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void cmb_ValueChanged(System.Object sender, EventArgs e)
    {

        If arlCombos.Count = 0 Then Exit Sub

        private ADRIFT.ItemSelector cmb = CType(sender, ADRIFT.ItemSelector);
        private ADRIFT.RestrictSummary Rest = null;

        for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
        {
            if (cmb Is arlCombos(eDirection))
            {
                Rest = CType(arlRestrictions(eDirection), ADRIFT.RestrictSummary)
                Exit For;
            }
        Next;
        if (cmb.Key <> "")
        {
            'cmb.Font = New Font(cmb.Font, FontStyle.Bold)
            Rest.Enabled = true;
        Else
            'cmb.Font = New Font(cmb.Font, FontStyle.Regular)
            Rest.Enabled = false;
        }
        Changed = True

    }

    'Private Sub frmLocation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    GetFormPosition(Me)
    'End Sub

    private void btnAddObject_Click(System.Object sender, System.EventArgs e)
    {

        ' Create a new static object in the current location

        private New clsObject obStatic;
        With obStatic;
            if (Adventure.htblAllProperties.ContainsKey("StaticOrDynamic"))
            {
                private New clsProperty pSoD;
                pSoD = Adventure.htblAllProperties("StaticOrDynamic").Copy
                '.htblActualProperties.Add(pSoD)
                .AddProperty(pSoD);
                .IsStatic = true;
            }

            if (Adventure.htblAllProperties.ContainsKey("StaticLocation"))
            {
                private New clsProperty sl;
                sl = Adventure.htblAllProperties("StaticLocation").Copy
                '.htblActualProperties.Add(sl)
                .AddProperty(sl);
            }

            if (Adventure.htblAllProperties.ContainsKey("AtLocation"))
            {
                private New clsProperty pLoc;
                pLoc = Adventure.htblAllProperties("AtLocation").Copy
                '.htblActualProperties.Add(pLoc)
                .AddProperty(pLoc);
            }

            private New clsObjectLocation StaticLoc;
            StaticLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
            StaticLoc.Key = cLocation.Key;
            .Move(StaticLoc);

        }

        private New frmObject(obStatic, True) fObject;

    }

    private void btnAddDynamicOb_Click(object sender, EventArgs e)
    {

        private New clsObject obDynamic;
        With obDynamic;
            if (Adventure.htblAllProperties.ContainsKey("StaticOrDynamic"))
            {
                private New clsProperty pSoD;
                pSoD = Adventure.htblAllProperties("StaticOrDynamic").Copy
                .AddProperty(pSoD);
                .IsStatic = false;
            }

            if (Adventure.htblAllProperties.ContainsKey("DynamicLocation"))
            {
                private New clsProperty sl;
                sl = Adventure.htblAllProperties("DynamicLocation").Copy
                .AddProperty(sl);
            }

            if (Adventure.htblAllProperties.ContainsKey("AtLocation"))
            {
                private New clsProperty pLoc;
                pLoc = Adventure.htblAllProperties("AtLocation").Copy
                .AddProperty(pLoc);
            }

            private New clsObjectLocation DynamicLoc;
            DynamicLoc.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
            DynamicLoc.Key = cLocation.Key;
            .Move(DynamicLoc);

        }

        private New frmObject(obDynamic, True) fObject;

    }

    private void btnAddCharacter_Click(object sender, EventArgs e)
    {

        private New clsCharacter ch;
        With ch;
            'If Adventure.htblAllProperties.ContainsKey("StaticOrDynamic") Then
            '    Dim pSoD As New clsProperty
            '    pSoD = Adventure.htblAllProperties("StaticOrDynamic").Copy
            '    .AddProperty(pSoD)
            '    .IsStatic = False
            'End If

            'If Adventure.htblAllProperties.ContainsKey("DynamicLocation") Then
            '    Dim sl As New clsProperty
            '    sl = Adventure.htblAllProperties("DynamicLocation").Copy
            '    .AddProperty(sl)
            'End If

            'If Adventure.htblAllProperties.ContainsKey("AtLocation") Then
            '    Dim pLoc As New clsProperty
            '    pLoc = Adventure.htblAllProperties("AtLocation").Copy
            '    .AddProperty(pLoc)
            'End If

            private New clsCharacterLocation(ch) CharLoc;
            CharLoc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
            CharLoc.Key = cLocation.Key;
            .Move(CharLoc);

        }

        private New frmCharacter(ch, True) fCharacter;

    }


    private void frmLocation_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmLocation_Shown(object sender, System.EventArgs e)
    {
        if (txtShortDesc IsNot null)
        {
            if (txtShortDesc.Text = "")
            {
                txtShortDesc.Focus();
            Else
                txtLongDesc.Focus();
                txtLongDesc.rtxtSource.SelectionStart = txtLongDesc.rtxtSource.TextLength;
            }
        }
    }

    private void frmLocation_Load(System.Object sender, System.EventArgs e)
    {
        Me.Properties1.PropertyType = clsProperty.PropertyOfEnum.Locations;
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgLocation16.GetHicon);

        if (DarkInterface())
        {
            chkHideOnMap.ForeColor = Color.White;
        }

    }


    private void btnRemoveItem_Click(object sender, System.EventArgs e)
    {
        Folder1.RemoveSelectedItems();
    }


    private DirectionsEnum GetDirection(LinkLabel label)
    {

        private DirectionsEnum eDirection;

        switch (true)
        {
            case label Is lblNorth:
                {
                eDirection = DirectionsEnum.North
            case label Is lblNorthEast:
                {
                eDirection = DirectionsEnum.NorthEast
            case label Is lblEast:
                {
                eDirection = DirectionsEnum.East
            case label Is lblSouthEast:
                {
                eDirection = DirectionsEnum.SouthEast
            case label Is lblSouth:
                {
                eDirection = DirectionsEnum.South
            case label Is lblSouthWest:
                {
                eDirection = DirectionsEnum.SouthWest
            case label Is lblWest:
                {
                eDirection = DirectionsEnum.West
            case label Is lblNorthWest:
                {
                eDirection = DirectionsEnum.NorthWest
            case label Is lblUp:
                {
                eDirection = DirectionsEnum.Up
            case label Is lblDown:
                {
                eDirection = DirectionsEnum.Down
            case label Is lblIn:
                {
                eDirection = DirectionsEnum.In
            case label Is lblOut:
                {
                eDirection = DirectionsEnum.Out
        }

        return eDirection;

    }



    private void lblNorth_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {

        private DirectionsEnum eDirection = GetDirection(CType(sender, LinkLabel));
        private string sText = Adventure.sDirectionsRE(eDirection);
        private string sNewText = InputBox("Please enter alternative words for direction " + eDirection.ToString + ", separated by / character:", "Direction " + eDirection.ToString, sText);

        if (sNewText <> "" && sNewText <> sText)
        {
            Adventure.sDirectionsRE(eDirection) = sNewText;
            private string sFirst = DirectionName(eDirection);
            'Dim sFirst As String = sNewText
            'If sNewText.Contains("/") Then sFirst = sNewText.Substring(0, sNewText.IndexOf("/"))
            (LinkLabel)(sender).Text = "Move " + sFirst + " to";
            (LinkLabel)(sender).LinkArea = New LinkArea(5, sFirst.Length);
        }

    }


    private void frmLocation_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Locations");
    }

}

}