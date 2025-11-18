using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmProperty
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public bool bKeepOpen = false;

    public void New(ref prop As clsProperty, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmProperty)
            {
                if (CType(w, frmProperty).cProperty.Key = prop.Key && prop.Key IsNot null)
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
        LoadForm(prop, bShow);
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
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider;
    Friend WithEvents lblDescription As System.Windows.Forms.Label;
    Friend WithEvents txtDescription As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents grpStateList As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents grpDependencies As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents lblValue As System.Windows.Forms.Label;
    Friend WithEvents lblProperty As System.Windows.Forms.Label;
    Friend WithEvents cmbProperty As AutoCompleteCombo;
    Friend WithEvents cmbValue As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents grpType As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents chkMandatory As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents lblType As System.Windows.Forms.Label;
    Friend WithEvents cmbType As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents lblRestrictProperty As System.Windows.Forms.Label;
    Friend WithEvents cmbRestrictProperty As AutoCompleteCombo;
    Friend WithEvents cmbRestrictValue As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents cmbPropertyOf As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents Label1 As System.Windows.Forms.Label;
    Friend WithEvents cmbAppendToProperty As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents Label2 As System.Windows.Forms.Label;
    Friend WithEvents txtStateList As System.Windows.Forms.TextBox;
    Friend WithEvents grdValueList As System.Windows.Forms.DataGridView;
    Friend WithEvents NumericValue As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents TextValue As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents chkPrivate As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents txtPopupDescription As System.Windows.Forms.TextBox;
    Friend WithEvents Label3 As System.Windows.Forms.Label;
    Friend WithEvents lblRestrictValue As System.Windows.Forms.Label;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem8 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem9 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem10 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem12 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem27 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem19 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem20 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem22 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem21 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem23 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem24 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem25 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem26 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem11 = new Infragistics.Win.ValueListItem();
        private System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        private System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.grpType = New Infragistics.Win.Misc.UltraGroupBox();
        Me.cmbRestrictProperty = New AutoCompleteCombo;
        Me.cmbRestrictValue = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.cmbPropertyOf = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.Label1 = New System.Windows.Forms.Label();
        Me.lblRestrictValue = New System.Windows.Forms.Label();
        Me.lblRestrictProperty = New System.Windows.Forms.Label();
        Me.chkMandatory = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.lblType = New System.Windows.Forms.Label();
        Me.cmbType = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.chkPrivate = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.grpDependencies = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblValue = New System.Windows.Forms.Label();
        Me.cmbValue = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.lblProperty = New System.Windows.Forms.Label();
        Me.cmbProperty = New AutoCompleteCombo;
        Me.grpStateList = New Infragistics.Win.Misc.UltraGroupBox();
        Me.cmbAppendToProperty = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.Label2 = New System.Windows.Forms.Label();
        Me.txtStateList = New System.Windows.Forms.TextBox();
        Me.grdValueList = New System.Windows.Forms.DataGridView();
        Me.NumericValue = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.TextValue = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.txtPopupDescription = New System.Windows.Forms.TextBox();
        Me.Label3 = New System.Windows.Forms.Label();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider();
        Me.lblDescription = New System.Windows.Forms.Label();
        Me.txtDescription = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).BeginInit();
        Me.grpType.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictProperty).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictValue).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPropertyOf).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkMandatory).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbType).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkPrivate).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpDependencies).BeginInit();
        Me.grpDependencies.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbValue).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbProperty).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpStateList).BeginInit();
        Me.grpStateList.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAppendToProperty).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grdValueList).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtDescription).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraTabControl1).BeginInit();
        Me.UltraTabControl1.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grpType);
        Me.UltraTabPageControl1.Controls.Add(Me.chkPrivate);
        Me.UltraTabPageControl1.Controls.Add(Me.grpDependencies);
        Me.UltraTabPageControl1.Controls.Add(Me.grpStateList);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 22);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(448, 477);
        '
        'grpType
        '
        Me.grpType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grpType.Controls.Add(Me.cmbRestrictProperty);
        Me.grpType.Controls.Add(Me.cmbRestrictValue);
        Me.grpType.Controls.Add(Me.cmbPropertyOf);
        Me.grpType.Controls.Add(Me.Label1);
        Me.grpType.Controls.Add(Me.lblRestrictValue);
        Me.grpType.Controls.Add(Me.lblRestrictProperty);
        Me.grpType.Controls.Add(Me.chkMandatory);
        Me.grpType.Controls.Add(Me.lblType);
        Me.grpType.Controls.Add(Me.cmbType);
        Me.grpType.Location = New System.Drawing.Point(14, 11);
        Me.grpType.Name = "grpType";
        Me.grpType.Size = New System.Drawing.Size(421, 152);
        Me.grpType.TabIndex = 1;
        Me.grpType.Text = "Property Type:";
        '
        'cmbRestrictProperty
        '
        Me.cmbRestrictProperty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbRestrictProperty.Enabled = false;
        Me.cmbRestrictProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbRestrictProperty.Location = New System.Drawing.Point(72, 83);
        Me.cmbRestrictProperty.Name = "cmbRestrictProperty";
        Me.cmbRestrictProperty.Size = New System.Drawing.Size(333, 23);
        Me.cmbRestrictProperty.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbRestrictProperty.TabIndex = 3;
        '
        'cmbRestrictValue
        '
        Me.cmbRestrictValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbRestrictValue.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbRestrictValue.Enabled = false;
        Me.cmbRestrictValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        ValueListItem3.DataValue = "CharacterKey";
        ValueListItem3.DisplayText = "Character Key";
        ValueListItem4.DataValue = "Integer";
        ValueListItem4.DisplayText = "Integer";
        ValueListItem5.DataValue = "LocationGroupKey";
        ValueListItem5.DisplayText = "Location Group Key";
        ValueListItem6.DataValue = "LocationKey";
        ValueListItem6.DisplayText = "Location Key";
        ValueListItem7.DataValue = "ObjectKey";
        ValueListItem7.DisplayText = "Object Key";
        ValueListItem8.DataValue = "SelectionOnly";
        ValueListItem8.DisplayText = "Selection Only";
        ValueListItem9.DataValue = "StateList";
        ValueListItem9.DisplayText = "State List";
        ValueListItem10.DataValue = "Text";
        ValueListItem10.DisplayText = "Text";
        Me.cmbRestrictValue.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem3, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7, ValueListItem8, ValueListItem9, ValueListItem10});
        Me.cmbRestrictValue.Location = New System.Drawing.Point(72, 115);
        Me.cmbRestrictValue.Name = "cmbRestrictValue";
        Me.cmbRestrictValue.Size = New System.Drawing.Size(333, 23);
        Me.cmbRestrictValue.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbRestrictValue.TabIndex = 4;
        '
        'cmbPropertyOf
        '
        Me.cmbPropertyOf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbPropertyOf.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbPropertyOf.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        ValueListItem12.DataValue = "AnyItem";
        ValueListItem12.DisplayText = "Any Item";
        ValueListItem1.DataValue = "Characters";
        ValueListItem1.DisplayText = "Characters";
        ValueListItem27.DataValue = "Locations";
        ValueListItem27.DisplayText = "Locations";
        ValueListItem2.DataValue = "Objects";
        ValueListItem2.DisplayText = "Objects";
        Me.cmbPropertyOf.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem12, ValueListItem1, ValueListItem27, ValueListItem2});
        Me.cmbPropertyOf.Location = New System.Drawing.Point(72, 20);
        Me.cmbPropertyOf.Name = "cmbPropertyOf";
        Me.cmbPropertyOf.Size = New System.Drawing.Size(245, 23);
        Me.cmbPropertyOf.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbPropertyOf.TabIndex = 0;
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent;
        Me.Label1.Location = New System.Drawing.Point(11, 25);
        Me.Label1.Name = "Label1";
        Me.Label1.Size = New System.Drawing.Size(63, 21);
        Me.Label1.TabIndex = 32;
        Me.Label1.Text = "Property of:";
        '
        'lblRestrictValue
        '
        Me.lblRestrictValue.BackColor = System.Drawing.Color.Transparent;
        Me.lblRestrictValue.Location = New System.Drawing.Point(11, 112);
        Me.lblRestrictValue.Name = "lblRestrictValue";
        Me.lblRestrictValue.Size = New System.Drawing.Size(64, 32);
        Me.lblRestrictValue.TabIndex = 30;
        Me.lblRestrictValue.Text = "Refine by Value:";
        '
        'lblRestrictProperty
        '
        Me.lblRestrictProperty.BackColor = System.Drawing.Color.Transparent;
        Me.lblRestrictProperty.Location = New System.Drawing.Point(11, 80);
        Me.lblRestrictProperty.Name = "lblRestrictProperty";
        Me.lblRestrictProperty.Size = New System.Drawing.Size(64, 32);
        Me.lblRestrictProperty.TabIndex = 28;
        Me.lblRestrictProperty.Text = "Refine by Property:";
        '
        'chkMandatory
        '
        Me.chkMandatory.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.chkMandatory.BackColor = System.Drawing.Color.Transparent;
        Me.chkMandatory.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkMandatory.Location = New System.Drawing.Point(338, 53);
        Me.chkMandatory.Name = "chkMandatory";
        Me.chkMandatory.Size = New System.Drawing.Size(75, 20);
        Me.chkMandatory.TabIndex = 2;
        Me.chkMandatory.Text = "Mandatory";
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.Color.Transparent;
        Me.lblType.Location = New System.Drawing.Point(11, 56);
        Me.lblType.Name = "lblType";
        Me.lblType.Size = New System.Drawing.Size(48, 16);
        Me.lblType.TabIndex = 25;
        Me.lblType.Text = "Type:";
        '
        'cmbType
        '
        Me.cmbType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        ValueListItem19.DataValue = "CharacterKey";
        ValueListItem19.DisplayText = "Character";
        ValueListItem20.DataValue = "Integer";
        ValueListItem20.DisplayText = "Integer";
        ValueListItem22.DataValue = "LocationKey";
        ValueListItem22.DisplayText = "Location";
        ValueListItem21.DataValue = "LocationGroupKey";
        ValueListItem21.DisplayText = "Location Group";
        ValueListItem23.DataValue = "ObjectKey";
        ValueListItem23.DisplayText = "Object";
        ValueListItem24.DataValue = "SelectionOnly";
        ValueListItem24.DisplayText = "Selection Only";
        ValueListItem25.DataValue = "StateList";
        ValueListItem25.DisplayText = "State";
        ValueListItem26.DataValue = "Text";
        ValueListItem26.DisplayText = "Text";
        ValueListItem11.DataValue = "ValueList";
        ValueListItem11.DisplayText = "Value";
        Me.cmbType.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem19, ValueListItem20, ValueListItem22, ValueListItem21, ValueListItem23, ValueListItem24, ValueListItem25, ValueListItem26, ValueListItem11});
        Me.cmbType.Location = New System.Drawing.Point(72, 51);
        Me.cmbType.MaxDropDownItems = 9;
        Me.cmbType.Name = "cmbType";
        Me.cmbType.Size = New System.Drawing.Size(245, 23);
        Me.cmbType.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbType.TabIndex = 1;
        '
        'chkPrivate
        '
        Me.chkPrivate.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.chkPrivate.AutoSize = true;
        Me.chkPrivate.BackColor = System.Drawing.Color.Transparent;
        Me.chkPrivate.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkPrivate.Location = New System.Drawing.Point(29, 452);
        Me.chkPrivate.Name = "chkPrivate";
        Me.chkPrivate.Size = New System.Drawing.Size(93, 17);
        Me.chkPrivate.TabIndex = 16;
        Me.chkPrivate.Text = "Private to item";
        Me.chkPrivate.Visible = false;
        '
        'grpDependencies
        '
        Me.grpDependencies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grpDependencies.Controls.Add(Me.lblValue);
        Me.grpDependencies.Controls.Add(Me.cmbValue);
        Me.grpDependencies.Controls.Add(Me.lblProperty);
        Me.grpDependencies.Controls.Add(Me.cmbProperty);
        Me.grpDependencies.Location = New System.Drawing.Point(15, 170);
        Me.grpDependencies.Name = "grpDependencies";
        Me.grpDependencies.Size = New System.Drawing.Size(420, 91);
        Me.grpDependencies.TabIndex = 2;
        Me.grpDependencies.Text = "This Property will only appear if the following are true:";
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.Transparent;
        Me.lblValue.Location = New System.Drawing.Point(11, 61);
        Me.lblValue.Name = "lblValue";
        Me.lblValue.Size = New System.Drawing.Size(40, 16);
        Me.lblValue.TabIndex = 22;
        Me.lblValue.Text = "Value:";
        '
        'cmbValue
        '
        Me.cmbValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbValue.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbValue.Location = New System.Drawing.Point(72, 56);
        Me.cmbValue.Name = "cmbValue";
        Me.cmbValue.Size = New System.Drawing.Size(332, 23);
        Me.cmbValue.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbValue.TabIndex = 1;
        '
        'lblProperty
        '
        Me.lblProperty.BackColor = System.Drawing.Color.Transparent;
        Me.lblProperty.Location = New System.Drawing.Point(11, 29);
        Me.lblProperty.Name = "lblProperty";
        Me.lblProperty.Size = New System.Drawing.Size(56, 16);
        Me.lblProperty.TabIndex = 20;
        Me.lblProperty.Text = "Property:";
        '
        'cmbProperty
        '
        Me.cmbProperty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbProperty.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbProperty.Location = New System.Drawing.Point(72, 24);
        Me.cmbProperty.Name = "cmbProperty";
        Me.cmbProperty.Size = New System.Drawing.Size(332, 23);
        Me.cmbProperty.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbProperty.TabIndex = 0;
        '
        'grpStateList
        '
        Me.grpStateList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grpStateList.Controls.Add(Me.cmbAppendToProperty);
        Me.grpStateList.Controls.Add(Me.Label2);
        Me.grpStateList.Controls.Add(Me.txtStateList);
        Me.grpStateList.Controls.Add(Me.grdValueList);
        Me.grpStateList.Enabled = false;
        Me.grpStateList.Location = New System.Drawing.Point(15, 269);
        Me.grpStateList.Name = "grpStateList";
        Me.grpStateList.Size = New System.Drawing.Size(420, 182);
        Me.grpStateList.TabIndex = 3;
        Me.grpStateList.Text = "State List";
        '
        'cmbAppendToProperty
        '
        Me.cmbAppendToProperty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbAppendToProperty.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbAppendToProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbAppendToProperty.Location = New System.Drawing.Point(72, 147);
        Me.cmbAppendToProperty.Name = "cmbAppendToProperty";
        Me.cmbAppendToProperty.Size = New System.Drawing.Size(332, 23);
        Me.cmbAppendToProperty.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbAppendToProperty.TabIndex = 1;
        '
        'Label2
        '
        Me.Label2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.Label2.BackColor = System.Drawing.Color.Transparent;
        Me.Label2.Location = New System.Drawing.Point(11, 152);
        Me.Label2.Name = "Label2";
        Me.Label2.Size = New System.Drawing.Size(64, 16);
        Me.Label2.TabIndex = 22;
        Me.Label2.Text = "Append to:";
        '
        'txtStateList
        '
        Me.txtStateList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtStateList.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtStateList.Location = New System.Drawing.Point(14, 22);
        Me.txtStateList.Multiline = true;
        Me.txtStateList.Name = "txtStateList";
        Me.txtStateList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        Me.txtStateList.Size = New System.Drawing.Size(390, 117);
        Me.txtStateList.TabIndex = 0;
        '
        'grdValueList
        '
        Me.grdValueList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grdValueList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        Me.grdValueList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        Me.grdValueList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NumericValue, Me.TextValue});
        Me.grdValueList.Location = New System.Drawing.Point(14, 22);
        Me.grdValueList.Name = "grdValueList";
        Me.grdValueList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        Me.grdValueList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
        Me.grdValueList.Size = New System.Drawing.Size(390, 117);
        Me.grdValueList.TabIndex = 23;
        '
        'NumericValue
        '
        Me.NumericValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        DataGridViewCellStyle1.Format = "N0";
        DataGridViewCellStyle1.NullValue = null;
        Me.NumericValue.DefaultCellStyle = DataGridViewCellStyle1;
        Me.NumericValue.FillWeight = 101.5228!;
        Me.NumericValue.HeaderText = "Numeric Value";
        Me.NumericValue.Name = "NumericValue";
        Me.NumericValue.Resizable = System.Windows.Forms.DataGridViewTriState.[true];
        Me.NumericValue.Width = 101;
        '
        'TextValue
        '
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[true];
        Me.TextValue.DefaultCellStyle = DataGridViewCellStyle2;
        Me.TextValue.FillWeight = 98.47716!;
        Me.TextValue.HeaderText = "Text Value";
        Me.TextValue.Name = "TextValue";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.txtPopupDescription);
        Me.UltraTabPageControl2.Controls.Add(Me.Label3);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(448, 477);
        '
        'txtPopupDescription
        '
        Me.txtPopupDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtPopupDescription.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtPopupDescription.Location = New System.Drawing.Point(11, 51);
        Me.txtPopupDescription.Multiline = true;
        Me.txtPopupDescription.Name = "txtPopupDescription";
        Me.txtPopupDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        Me.txtPopupDescription.Size = New System.Drawing.Size(426, 415);
        Me.txtPopupDescription.TabIndex = 30;
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Label3.BackColor = System.Drawing.Color.Transparent;
        Me.Label3.Location = New System.Drawing.Point(16, 19);
        Me.Label3.Name = "Label3";
        Me.Label3.Size = New System.Drawing.Size(421, 32);
        Me.Label3.TabIndex = 29;
        Me.Label3.Text = "Enter a description that will appear as pop-up help when this property is display" + _;
    "ed";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 545);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(450, 48);
        Me.UltraStatusBar1.TabIndex = 5;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(346, 555);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 6;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(250, 555);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 5;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(154, 555);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 4;
        Me.btnOK.Text = "OK";
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(10, 17);
        Me.lblDescription.Name = "lblDescription";
        Me.lblDescription.Size = New System.Drawing.Size(64, 16);
        Me.lblDescription.TabIndex = 15;
        Me.lblDescription.Text = "Name:";
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtDescription.Location = New System.Drawing.Point(60, 12);
        Me.txtDescription.Name = "txtDescription";
        Me.txtDescription.Size = New System.Drawing.Size(374, 24);
        Me.txtDescription.TabIndex = 0;
        Me.txtDescription.Text = "Location of the object";
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(1, 20);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(357, 377);
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage3);
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1);
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2);
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 46);
        Me.UltraTabControl1.Name = "UltraTabControl1";
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage3;
        Me.UltraTabControl1.Size = New System.Drawing.Size(450, 500);
        Me.UltraTabControl1.TabIndex = 2;
        UltraTab3.TabPage = Me.UltraTabPageControl1;
        UltraTab3.Text = "Definition";
        UltraTab4.TabPage = Me.UltraTabPageControl2;
        UltraTab4.Text = "Description";
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3, UltraTab4});
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3";
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(448, 477);
        '
        'frmProperty
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(450, 593);
        Me.Controls.Add(Me.UltraTabControl1);
        Me.Controls.Add(Me.txtDescription);
        Me.Controls.Add(Me.lblDescription);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "frmProperty";
        Me.ShowInTaskbar = false;
        Me.Text = "Property - ";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).EndInit();
        Me.grpType.ResumeLayout(false);
        Me.grpType.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictProperty).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictValue).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPropertyOf).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkMandatory).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbType).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkPrivate).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpDependencies).EndInit();
        Me.grpDependencies.ResumeLayout(false);
        Me.grpDependencies.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbValue).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbProperty).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpStateList).EndInit();
        Me.grpStateList.ResumeLayout(false);
        Me.grpStateList.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAppendToProperty).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grdValueList).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.UltraTabPageControl2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtDescription).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraTabControl1).EndInit();
        Me.UltraTabControl1.ResumeLayout(false);
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }

#End Region


    private clsProperty cProperty;
    private bool bChanged;
    private int iSelectedIndex = -1;
    'Private dtValueList As New System.Data.DataTable


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


    private string _OwnerKey;
    public string OwnerKey
        {
            get
            {
            return _OwnerKey;
        }
set(String)
            if (value Is null)
            {
                chkPrivate.Text = "Parent must be applied before properties can be made Private";
                chkPrivate.Enabled = false;
            Else
                chkPrivate.Text = "Private to " + Adventure.GetNameFromKey(value);
                chkPrivate.Enabled = true;
            }
            _OwnerKey = value
        }
    }


    private void LoadForm(ref cProperty As clsProperty, bool bShow)
    {

        Me.cProperty = cProperty;

        'grdValueList.DataSource = dtValueList

        With cProperty;
            Text = "Property - " & .Description
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            If .Description = "" Then Text = "New Property"
            chkMandatory.Checked = .Mandatory;

            txtDescription.Text = .Description;
            SetCombo(cmbPropertyOf, .PropertyOf.ToString);
            SetCombo(cmbType, .Type.ToString);

            'cmbAppendToProperty.Items.Add(Nothing, "<Do not append>")
            SetCombo(cmbProperty, .DependentKey);
            'cmbValue.Items.Add(Nothing, "<No dependent value>")
            SetCombo(cmbValue, .DependentValue);
            If cmbValue.SelectedItem == null && cmbValue.Items.Count > 0 Then cmbValue.SelectedIndex = 0
            SetCombo(cmbRestrictProperty, .RestrictProperty);
            If cmbRestrictProperty.Items.Count = 0 Then cmbRestrictProperty.Items.Add(null, "<No restriction property>")
            If cmbRestrictProperty.SelectedItem == null Then cmbRestrictProperty.SelectedIndex = 0
            'cmbRestrictValue.Items.Add(Nothing, "<No restriction value>")
            SetCombo(cmbRestrictValue, .RestrictValue);
            If cmbRestrictValue.SelectedItem == null && cmbRestrictValue.Items.Count > 0 Then cmbRestrictValue.SelectedIndex = 0
            SetCombo(cmbAppendToProperty, .AppendToProperty);
            txtPopupDescription.Text = .PopupDescription;

            switch (.Type)
            {
                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    txtStateList.Clear();
                    For Each sState As String In .arlStates
                        If txtStateList.Text <> "" Then txtStateList.AppendText(vbCrLf)
                        txtStateList.AppendText(sState);
                    Next;
                    grpStateList.Enabled = true;
                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    For Each sKey As String In .ValueList.Keys
                        grdValueList.Rows.Add(New Object() {.ValueList(sKey), sKey});
                    Next;
                    grpStateList.Enabled = true;
                default:
                    {
                    grpStateList.Enabled = false;
            }

            ' If the property is only used on a single item, display the Private checkbox
            if (.PrivateTo <> "")
            {
                OwnerKey = .PrivateTo
                chkPrivate.Visible = true;
            Else
                if (cProperty.Key <> "" && Not chkPrivate.Visible)
                {
                    private int iCount = 0;
                    private string sOwner = "";
                    For Each itm As clsItem In Adventure.dictAllItems.Values
                        if (TypeOf itm Is clsItemWithProperties)
                        {
                            if (CType(itm, clsItemWithProperties).HasProperty(cProperty.Key))
                            {
                                iCount += 1;
                                sOwner = itm.Key
                                If iCount > 1 Then Exit For
                            }
                        }
                    Next;
                    if (iCount = 1)
                    {
                        chkPrivate.Visible = true;
                        OwnerKey = sOwner
                    }
                }
            }
            If .PrivateTo = OwnerKey && OwnerKey <> "" Then chkPrivate.Checked = true

            Changed = False
        }

        If bShow Then Me.Show()

        OpenForms.Add(Me);

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        If ! ApplyProperty() Then Exit Sub
        CloseProperty(Me);
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        If ! ApplyProperty() Then Exit Sub
        Changed = False
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyProperty()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseProperty(Me);
    }


    private StringArrayList States()
    {

        private New StringArrayList arlStates;

        if (txtStateList.Text <> "")
        {
            For Each sLine As String In txtStateList.Text.Split(Chr(10))
                If sLine.EndsWith(Chr(13)) Then sLine = sLeft(sLine, sLine.Length - 1)
                If sLine <> "" Then arlStates.Add(sLine)
            Next;
        }

        return arlStates;

    }


    private bool ValidateProperty()
    {

        ' Ensure no duplicate values in valuelist
        if (cmbType.SelectedItem IsNot null && cmbType.SelectedItem.DataValue.ToString = "ValueList")
        {
            private New List<string> lValues;
            For Each row As DataGridViewRow In grdValueList.Rows
                if (row.Cells(1).Value IsNot null)
                {
                    if (lValues.Contains(row.Cells(1).Value.ToString))
                    {
                        MessageBox.Show("You must not have duplicate text values in a valuelist", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        grdValueList.Focus();
                        grdValueList.CurrentCell = row.Cells(1);
                        grdValueList.BeginEdit(true);
                        return false;
                    Else
                        lValues.Add(row.Cells(1).Value.ToString);
                    }
                }
            Next;
        }

        return true;
    }


    private bool ApplyProperty()
    {

        If ! ValidateProperty() Then Return false

        With cProperty;
            .Description = txtDescription.Text;
            If .Description = "" Then .Description = "Unnamed Property"
            .Mandatory = chkMandatory.Checked;
            If cmbProperty.SelectedItem != null && cmbProperty.SelectedItem.DataValue != null Then .DependentKey = CStr(cmbProperty.SelectedItem.DataValue) Else .DependentKey = null
            If cmbValue.SelectedItem != null && cmbValue.SelectedItem.DataValue != null Then .DependentValue = CStr(cmbValue.SelectedItem.DataValue) Else .DependentValue = null
            If cmbRestrictProperty.Enabled && cmbRestrictProperty.SelectedItem != null && cmbRestrictProperty.SelectedItem.DataValue != null Then .RestrictProperty = CStr(cmbRestrictProperty.SelectedItem.DataValue) Else .RestrictProperty = null
            If cmbRestrictValue.Enabled && cmbRestrictValue.SelectedItem.DataValue != null Then .RestrictValue = CStr(cmbRestrictValue.SelectedItem.DataValue) Else .RestrictValue = null
            If cmbAppendToProperty.SelectedItem != null && cmbAppendToProperty.SelectedItem.DataValue != null Then .AppendToProperty = CStr(cmbAppendToProperty.SelectedItem.DataValue) Else .AppendToProperty = null
            .PropertyOf = EnumParsePropertyPropertyOf(CStr(cmbPropertyOf.SelectedItem.DataValue));
            .Type = EnumParsePropertyType(CStr(cmbType.SelectedItem.DataValue));
            .arlStates.Clear();
            .ValueList.Clear();
            switch (.Type)
            {
                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    .arlStates = States();
                    If States.Count > 0 Then .Value = States(0)
                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    For Each row As DataGridViewRow In grdValueList.Rows
                        If row.Cells(1).Value != null Then .ValueList.Add(row.Cells(1).Value.ToString, CInt(row.Cells(0).Value))
                    Next;
            }

            .LastUpdated = Now;
            .IsLibrary = false;

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Property");
                Adventure.htblAllProperties.Add(cProperty);
            }

            UpdateListItem(.Key, .Description);
            If chkPrivate.Checked Then .PrivateTo = OwnerKey Else .PrivateTo = ""
            .PopupDescription = txtPopupDescription.Text;

            ' Now go thru all items and update their properties if they contain this one
            if (.PropertyOf = clsProperty.PropertyOfEnum.Locations || .PropertyOf = clsProperty.PropertyOfEnum.AnyItem)
            {
                For Each l As clsLocation In Adventure.htblLocations.Values
                    if (.Mandatory && Not l.htblActualProperties.ContainsKey(.Key))
                    {
                        l.htblActualProperties.Add(.Copy);
                    }
                    if (l.htblActualProperties.ContainsKey(.Key))
                    {
                        private clsProperty p = l.htblActualProperties(.Key);
                        private Description StringData = p.StringData;
                        private int IntData = p.IntData;
                        p = .Copy
                        p.Selected = true;
                        p.StringData = StringData;
                        p.IntData = IntData;
                        l.htblActualProperties(.Key) = p;
                    }
                Next;
            }
            if (.PropertyOf = clsProperty.PropertyOfEnum.Objects || .PropertyOf = clsProperty.PropertyOfEnum.AnyItem)
            {
                For Each o As clsObject In Adventure.htblObjects.Values
                    if (.Mandatory && Not o.htblActualProperties.ContainsKey(.Key))
                    {
                        o.htblActualProperties.Add(.Copy);
                    }
                    if (o.htblActualProperties.ContainsKey(.Key))
                    {
                        private clsProperty p = o.htblActualProperties(.Key);
                        private Description StringData = p.StringData;
                        private int IntData = p.IntData;
                        p = .Copy
                        p.Selected = true;
                        p.StringData = StringData;
                        p.IntData = IntData;
                        o.htblActualProperties(.Key) = p;
                    }
                Next;
            }
            if (.PropertyOf = clsProperty.PropertyOfEnum.Characters || .PropertyOf = clsProperty.PropertyOfEnum.AnyItem)
            {
                For Each c As clsCharacter In Adventure.htblCharacters.Values
                    if (.Mandatory && Not c.htblActualProperties.ContainsKey(.Key))
                    {
                        c.htblActualProperties.Add(.Copy);
                    }
                    if (c.htblActualProperties.ContainsKey(.Key))
                    {
                        private clsProperty p = c.htblActualProperties(.Key);
                        private Description StringData = p.StringData;
                        private int IntData = p.IntData;
                        p = .Copy
                        p.Selected = true;
                        p.StringData = StringData;
                        p.IntData = IntData;
                        c.htblActualProperties(.Key) = p;
                    }
                Next;
            }
        }

        Adventure.Changed = true;
        return true;

    }


    private void frmProperty_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmObject_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgProperty16.GetHicon);
        GetFormPosition(Me);
        ResizeForm();
    }


    private void cmbProp_SelectionChanged(object sender, System.EventArgs e)
    {

        'If cmbType.SelectedItem IsNot Nothing AndAlso CStr(cmbType.SelectedItem.DataValue) = "Integer" Then Exit Sub

        Dim cmbVal, cmbProp As Infragistics.Win.UltraWinEditors.UltraComboEditor
        private System.Windows.Forms.Label lblVal;

        if (sender Is cmbProperty)
        {
            cmbVal = cmbValue
            lblVal = lblValue
            cmbProp = cmbProperty
        Else
            cmbVal = cmbRestrictValue
            lblVal = lblRestrictValue
            cmbProp = cmbRestrictProperty
        }

        cmbVal.Items.Clear();
        cmbVal.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;

        if (sender Is cmbProperty)
        {
            cmbVal.Items.Add(null, "<No dependent value>");
        Else
            cmbVal.Items.Add(null, "<No restriction value>");
        }

        if (cmbProp.SelectedItem Is null || cmbProp.SelectedItem.DataValue Is null)
        {
            cmbVal.Enabled = false;
            lblVal.Enabled = false;
            Exit Sub;
        Else
            cmbVal.Enabled = true;
            lblVal.Enabled = true;
        }

        private clsProperty prop = Adventure.htblAllProperties(CStr(cmbProp.SelectedItem.DataValue));

        if (prop IsNot null)
        {
            switch (prop.Type)
            {
                case clsProperty.PropertyTypeEnum.CharacterKey:
                    {
                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                        cmbVal.Items.Add(c.Key, c.Name);
                    Next;
                case clsProperty.PropertyTypeEnum.Integer:
                    {
                    cmbVal.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
                    cmbVal.Text = "0";
                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    {
                    For Each lc As clsGroup In Adventure.htblGroups.Values
                        If lc.GroupType = clsGroup.GroupTypeEnum.Locations Then cmbVal.Items.Add(lc.Key, lc.Name)
                    Next;
                case clsProperty.PropertyTypeEnum.LocationKey:
                    {
                    For Each l As clsLocation In Adventure.htblLocations.Values
                        cmbVal.Items.Add(l.Key, l.ShortDescription.ToString);
                    Next;
                case clsProperty.PropertyTypeEnum.ObjectKey:
                    {
                    For Each o As clsObject In Adventure.htblObjects.Values
                        cmbVal.Items.Add(o.Key, o.FullName);
                    Next;
                case clsProperty.PropertyTypeEnum.SelectionOnly:
                    {
                    'cmbVal.Enabled = False
                    'lblVal.Enabled = False
                    cmbVal.Items.Clear();
                    cmbVal.Items.Add("true", sSELECTED);
                    cmbVal.Items.Add("false", sUNSELECTED);
                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    For Each sState As String In prop.arlStates
                        cmbVal.Items.Add(sState);
                    Next;
                case clsProperty.PropertyTypeEnum.Text:
                    {
                    cmbVal.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
                    cmbVal.Text = "";
                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    For Each sState As String In prop.ValueList.Keys
                        cmbVal.Items.Add(prop.ValueList(sState), sState);
                    Next;
            }
        }

        SetCombo(cmbVal, "");

    }

    private void cmbType_SelectionChanged(System.Object sender, System.EventArgs e)
    {

        lblRestrictProperty.Enabled = false;
        lblRestrictValue.Enabled = false;
        cmbRestrictValue.Enabled = false;
        switch (CStr(cmbType.SelectedItem.DataValue))
        {
            case "StateList":
                {
                grpStateList.Enabled = true;
                grdValueList.SendToBack();
                cmbRestrictProperty.Enabled = false;
                cmbAppendToProperty.Enabled = true;
                grpStateList.Text = "State List";
            case "ValueList":
                {
                grpStateList.Enabled = true;
                grdValueList.BringToFront();
                cmbRestrictProperty.Enabled = false;
                cmbAppendToProperty.Enabled = false;
                grpStateList.Text = "Value List";
            case "ObjectKey":
            case "CharacterKey":
            case "LocationKey":
                {
                cmbRestrictProperty.Items.Clear();
                cmbRestrictProperty.Items.Add(null, "<No restriction property>");

                For Each prop As clsProperty In Adventure.htblAllProperties.Values
                    If (CStr(cmbType.SelectedItem.DataValue) = "ObjectKey" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Objects || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) _
                        || (CStr(cmbType.SelectedItem.DataValue) = "CharacterKey" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Characters || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) _;
                        || (CStr(cmbType.SelectedItem.DataValue) = "LocationKey" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Locations || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) Then;
                        cmbRestrictProperty.Items.Add(prop.Key, prop.Description);
                    }
                Next;
                cmbProp_SelectionChanged(cmbRestrictProperty, null);
                grpStateList.Enabled = false;
                lblRestrictProperty.Enabled = true;
                cmbRestrictProperty.Enabled = true;
                lblRestrictProperty.Text = "Refine by Property:";
            case "Integer":
                {
                cmbRestrictProperty.Items.Clear();
                cmbRestrictProperty.Items.Add(null, "<Normal Integer>");

                For Each prop As clsProperty In Adventure.htblAllProperties.Values
                    if (prop.Type = clsProperty.PropertyTypeEnum.ValueList)
                    {
                        cmbRestrictProperty.Items.Add(prop.Key, prop.Description);
                    }
                Next;
                grpStateList.Enabled = false;
                cmbRestrictProperty.Enabled = true;
                lblRestrictProperty.Enabled = true;
                lblRestrictProperty.Text = "Integer Type:";
            default:
                {
                grpStateList.Enabled = false;
                cmbRestrictProperty.Enabled = false;
        }

    }


    private void StuffChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
        ResizeForm();
    }


    private void frmProperty_Shown(object sender, System.EventArgs e)
    {
        txtDescription.Focus();
    }

    private void txtStateList_GotFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = null;
    }

    private void txtStateList_LostFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = btnOK;
    }


    private void cmbPropertyOf_SelectionChanged(object sender, System.EventArgs e)
    {

        cmbProperty.Items.Clear();
        cmbProperty.Items.Add(null, "<No dependent property>");
        cmbAppendToProperty.Items.Clear();
        cmbAppendToProperty.Items.Add(null, "<Do not append>");

        For Each prop As clsProperty In Adventure.htblAllProperties.Values
            If (cmbPropertyOf.SelectedItem.DataValue.ToString = "Objects" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Objects || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) _
                || (cmbPropertyOf.SelectedItem.DataValue.ToString = "Characters" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Characters || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) _;
                || (cmbPropertyOf.SelectedItem.DataValue.ToString = "Locations" && (prop.PropertyOf = clsProperty.PropertyOfEnum.Locations || prop.PropertyOf = clsProperty.PropertyOfEnum.AnyItem)) _;
                || cmbPropertyOf.SelectedItem.DataValue.ToString = "AnyItem" Then;
                cmbProperty.Items.Add(prop.Key, prop.Description);
                If prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.Key <> cProperty.Key Then cmbAppendToProperty.Items.Add(prop.Key, prop.Description)
            }
        Next;
        cmbProp_SelectionChanged(cmbProperty, null);

    }


    private DataGridViewTextBoxEditingControl cell;

    private void CheckCell(object sender, Windows.Forms.KeyPressEventArgs e)
    {
        If ! IsNumeric(e.KeyChar) Then e.Handled = true
    }

    private void grdValueList_CellLeave(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
        if (cell IsNot null)
        {
            RemoveHandler cell.KeyPress, AddressOf CheckCell;
            cell = Nothing
        }
    }

    private void grdValueList_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
        Changed = True
    }

    private void grdValueList_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
    {
        if (cell Is null && e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight)
        {
            cell = CType(e.Control, DataGridViewTextBoxEditingControl)
            AddHandler cell.KeyPress, AddressOf CheckCell;
        }
    }


    private void ResizeForm()
    {

        private string sType = "";
        If cmbType.SelectedItem != null Then sType = cmbType.SelectedItem.DataValue.ToString
        switch (sType)
        {
            case "CharacterKey":
            case "Integer":
            case "LocationKey":
            case "ObjectKey":
                {
                lblRestrictProperty.Visible = true;
                cmbRestrictProperty.Visible = true;
                lblRestrictValue.Visible = true;
                cmbRestrictValue.Visible = true;
                grpType.Height = 152;
            default:
                {
                lblRestrictProperty.Visible = false;
                cmbRestrictProperty.Visible = false;
                lblRestrictValue.Visible = false;
                cmbRestrictValue.Visible = false;
                grpType.Height = 85;
        }
        grpDependencies.Top = grpType.Top + grpType.Height + 10;
        switch (sType)
        {
            case "StateList":
            case "ValueList":
                {
                grpStateList.Visible = true;
                If grpStateList.Height < 100 Then grpStateList.Height = 100
                if (sType = "StateList")
                {
                    txtStateList.Visible = true;
                    cmbAppendToProperty.Visible = true;
                    Label2.Visible = true;
                    grdValueList.Visible = false;
                Else
                    txtStateList.Visible = false;
                    cmbAppendToProperty.Visible = false;
                    Label2.Visible = false;
                    grdValueList.Visible = true;
                }
            default:
                {
                grpStateList.Visible = false;
        }
        grpStateList.Top = grpDependencies.Top + grpDependencies.Height + 10;

        private int iHeight = 140 + grpType.Height;
        If grpDependencies.Visible Then iHeight += grpDependencies.Height + 10
        If chkPrivate.Visible Then iHeight += 20

        private int iPaddingHeight = Math.Max(Me.Height - Me.ClientSize.Height, 0);

        if (grpStateList.Visible)
        {
            MinimumSize = New Size(432, iHeight + iPaddingHeight + 170)
            MaximumSize = New Size(1600, 1200)
            grpStateList.Height = iHeight - 164;
            grdValueList.Height = grpStateList.Height - 34;
            txtStateList.Height = grpStateList.Height - 64;
            if (chkPrivate.Visible)
            {
                grpStateList.Height -= 20;
            }
        Else
            MinimumSize = New Size(432, iHeight + iPaddingHeight)
            MaximumSize = New Size(1600, iHeight + iPaddingHeight)
        }

    }


    private void frmProperty_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Properties");
    }

}
}