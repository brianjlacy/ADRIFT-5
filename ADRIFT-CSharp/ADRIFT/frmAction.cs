using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmAction
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public void New(string Command)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        bLoadingAction = True
        Me.sCommand = Command;
        InitializeComponent();
        bLoadingAction = False
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
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents cmbCharacterMoveTo As AutoCompleteCombo;
    Friend WithEvents cmbCharacterKey2 As AutoCompleteCombo;
    Friend WithEvents cmbCharacterKey1 As AutoCompleteCombo;
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl7 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents tabsActions As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents cmbObjectKey2 As AutoCompleteCombo;
    Friend WithEvents cmbObjectKey1 As AutoCompleteCombo;
    Friend WithEvents cmbObjectMoveTo As AutoCompleteCombo;
    Friend WithEvents lblSet As System.Windows.Forms.Label;
    Friend WithEvents cmbPropertyKey2 As AutoCompleteCombo;
    Friend WithEvents lblFor As System.Windows.Forms.Label;
    Friend WithEvents lblLoop As System.Windows.Forms.Label;
    Friend WithEvents cmbVariable As AutoCompleteCombo;
    Friend WithEvents lblNext As System.Windows.Forms.Label;
    Friend WithEvents lblTo2 As System.Windows.Forms.Label;
    Friend WithEvents txtLoopTo As System.Windows.Forms.TextBox;
    Friend WithEvents txtLoopFrom As System.Windows.Forms.TextBox;
    Friend WithEvents lblRight As System.Windows.Forms.Label;
    Friend WithEvents lblLeft As System.Windows.Forms.Label;
    Friend WithEvents cmbIndex As AutoCompleteCombo;
    Friend WithEvents cmbVariableArray As AutoCompleteCombo;
    Friend WithEvents cmbIndexEdit As AutoCompleteCombo;
    Friend WithEvents cmbTasks As AutoCompleteCombo;
    Friend WithEvents cmbWhatT As AutoCompleteCombo;
    Friend WithEvents btnParams As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents Expression As ADRIFT.Expression;
    Friend WithEvents isPropertyKey1 As ADRIFT.ItemSelector;
    Friend WithEvents Label2 As System.Windows.Forms.Label;
    Friend WithEvents cmbEndGame As AutoCompleteCombo;
    Friend WithEvents cmbObjectMoveWhat As AutoCompleteCombo;
    Friend WithEvents cmbObjectWhat As AutoCompleteCombo;
    Friend WithEvents lblObjectsWithValue As System.Windows.Forms.Label;
    Friend WithEvents cmbObjectPropertyValue As ADRIFT.PropertyValue;
    Friend WithEvents cmbCharacterPropertyValue As ADRIFT.PropertyValue;
    Friend WithEvents lblCharactersWithValue As System.Windows.Forms.Label;
    Friend WithEvents cmbCharacterMoveWho As AutoCompleteCombo;
    Friend WithEvents cmbCharacterWhat As AutoCompleteCombo;
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents cmbLocationKey2 As AutoCompleteCombo;
    Friend WithEvents cmbLocationMoveWhat As AutoCompleteCombo;
    Friend WithEvents cmbLocationWhat As AutoCompleteCombo;
    Friend WithEvents cmbLocationMoveTo As AutoCompleteCombo;
    Friend WithEvents cmbLocationKey1 As AutoCompleteCombo;
    Friend WithEvents cmbLocationPropertyValue As ADRIFT.PropertyValue;
    Friend WithEvents lblLocationsWithValue As System.Windows.Forms.Label;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents chkLoop As System.Windows.Forms.CheckBox;
    Friend WithEvents cmbPropertyValue As ADRIFT.PropertyValue;
    Friend WithEvents UltraTabPageControl8 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents lblConvAbout As System.Windows.Forms.Label;
    Friend WithEvents cmbAskWho As AutoCompleteCombo;
    Friend WithEvents cmbWhatConv As AutoCompleteCombo;
    Friend WithEvents txtConvTopic As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents cmbVariableSet As AutoCompleteCombo;
    Friend WithEvents UltraTabPageControl9 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents Label3 As System.Windows.Forms.Label;
    Friend WithEvents Label1 As System.Windows.Forms.Label;
    Friend WithEvents expTurns As ADRIFT.Expression;
    Friend WithEvents lblTo As System.Windows.Forms.Label;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
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
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem17 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance6 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance7 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance8 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance10 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.Appearance Appearance9 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel UltraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmAction));
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblObjectsWithValue = New System.Windows.Forms.Label();
        Me.cmbObjectMoveWhat = New AutoCompleteCombo();
        Me.cmbObjectWhat = New AutoCompleteCombo();
        Me.cmbObjectKey2 = New AutoCompleteCombo();
        Me.cmbObjectMoveTo = New AutoCompleteCombo();
        Me.cmbObjectKey1 = New AutoCompleteCombo();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblCharactersWithValue = New System.Windows.Forms.Label();
        Me.cmbCharacterMoveWho = New AutoCompleteCombo();
        Me.cmbCharacterWhat = New AutoCompleteCombo();
        Me.cmbCharacterMoveTo = New AutoCompleteCombo();
        Me.cmbCharacterKey2 = New AutoCompleteCombo();
        Me.cmbCharacterKey1 = New AutoCompleteCombo();
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblLocationsWithValue = New System.Windows.Forms.Label();
        Me.cmbLocationKey2 = New AutoCompleteCombo();
        Me.cmbLocationMoveWhat = New AutoCompleteCombo();
        Me.cmbLocationWhat = New AutoCompleteCombo();
        Me.cmbLocationMoveTo = New AutoCompleteCombo();
        Me.cmbLocationKey1 = New AutoCompleteCombo();
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.btnParams = New Infragistics.Win.Misc.UltraButton();
        Me.cmbTasks = New AutoCompleteCombo();
        Me.cmbWhatT = New AutoCompleteCombo();
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.cmbVariableSet = New AutoCompleteCombo();
        Me.cmbIndexEdit = New AutoCompleteCombo();
        Me.cmbVariable = New AutoCompleteCombo();
        Me.lblRight = New System.Windows.Forms.Label();
        Me.cmbIndex = New AutoCompleteCombo();
        Me.cmbVariableArray = New AutoCompleteCombo();
        Me.lblLeft = New System.Windows.Forms.Label();
        Me.lblLoop = New System.Windows.Forms.Label();
        Me.lblNext = New System.Windows.Forms.Label();
        Me.lblTo2 = New System.Windows.Forms.Label();
        Me.txtLoopTo = New System.Windows.Forms.TextBox();
        Me.txtLoopFrom = New System.Windows.Forms.TextBox();
        Me.lblFor = New System.Windows.Forms.Label();
        Me.UltraTabPageControl8 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.cmbAskWho = New AutoCompleteCombo();
        Me.txtConvTopic = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.lblConvAbout = New System.Windows.Forms.Label();
        Me.cmbWhatConv = New AutoCompleteCombo();
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.lblTo = New System.Windows.Forms.Label();
        Me.lblSet = New System.Windows.Forms.Label();
        Me.cmbPropertyKey2 = New AutoCompleteCombo();
        Me.UltraTabPageControl7 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.cmbEndGame = New AutoCompleteCombo();
        Me.Label2 = New System.Windows.Forms.Label();
        Me.chkLoop = New System.Windows.Forms.CheckBox();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.tabsActions = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.UltraTabPageControl9 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.Label1 = New System.Windows.Forms.Label();
        Me.Label3 = New System.Windows.Forms.Label();
        Me.cmbObjectPropertyValue = New ADRIFT.PropertyValue();
        Me.cmbCharacterPropertyValue = New ADRIFT.PropertyValue();
        Me.cmbPropertyValue = New ADRIFT.PropertyValue();
        Me.isPropertyKey1 = New ADRIFT.ItemSelector();
        Me.Expression = New ADRIFT.Expression();
        Me.cmbLocationPropertyValue = New ADRIFT.PropertyValue();
        Me.expTurns = New ADRIFT.Expression();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectMoveWhat).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectWhat).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectKey2).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectMoveTo).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectKey1).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterMoveWho).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterWhat).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterMoveTo).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterKey2).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterKey1).BeginInit();
        Me.UltraTabPageControl5.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationKey2).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationMoveWhat).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationWhat).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationMoveTo).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationKey1).BeginInit();
        Me.UltraTabPageControl6.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbTasks).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbWhatT).BeginInit();
        Me.UltraTabPageControl4.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariableSet).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbIndexEdit).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariable).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbIndex).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariableArray).BeginInit();
        Me.UltraTabPageControl8.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAskWho).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtConvTopic).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbWhatConv).BeginInit();
        Me.UltraTabPageControl3.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPropertyKey2).BeginInit();
        Me.UltraTabPageControl7.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbEndGame).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsActions).BeginInit();
        Me.tabsActions.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        Me.UltraStatusBar1.SuspendLayout();
        Me.UltraTabPageControl9.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectPropertyValue);
        Me.UltraTabPageControl1.Controls.Add(Me.lblObjectsWithValue);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectMoveWhat);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectWhat);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectKey2);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectMoveTo);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbObjectKey1);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(886, 38);
        '
        'lblObjectsWithValue
        '
        Me.lblObjectsWithValue.BackColor = System.Drawing.Color.Transparent;
        Me.lblObjectsWithValue.Location = New System.Drawing.Point(525, 13);
        Me.lblObjectsWithValue.Name = "lblObjectsWithValue";
        Me.lblObjectsWithValue.Size = New System.Drawing.Size(59, 16);
        Me.lblObjectsWithValue.TabIndex = 11;
        Me.lblObjectsWithValue.Text = "with value";
        Me.lblObjectsWithValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        Me.lblObjectsWithValue.Visible = false;
        '
        'cmbObjectMoveWhat
        '
        Me.cmbObjectMoveWhat.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbObjectMoveWhat.Location = New System.Drawing.Point(79, 9);
        Me.cmbObjectMoveWhat.Name = "cmbObjectMoveWhat";
        Me.cmbObjectMoveWhat.Size = New System.Drawing.Size(147, 21);
        Me.cmbObjectMoveWhat.TabIndex = 7;
        '
        'cmbObjectWhat
        '
        Me.cmbObjectWhat.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbObjectWhat.Location = New System.Drawing.Point(11, 9);
        Me.cmbObjectWhat.Name = "cmbObjectWhat";
        Me.cmbObjectWhat.Size = New System.Drawing.Size(69, 21);
        Me.cmbObjectWhat.TabIndex = 6;
        '
        'cmbObjectKey2
        '
        Me.cmbObjectKey2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbObjectKey2.Location = New System.Drawing.Point(936, 9);
        Me.cmbObjectKey2.Name = "cmbObjectKey2";
        Me.cmbObjectKey2.Size = New System.Drawing.Size(199, 21);
        Me.cmbObjectKey2.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbObjectKey2.TabIndex = 5;
        '
        'cmbObjectMoveTo
        '
        Me.cmbObjectMoveTo.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem1.DataValue = "InsideObject";
        ValueListItem1.DisplayText = "inside object";
        ValueListItem2.DataValue = "OntoObject";
        ValueListItem2.DisplayText = "onto object";
        ValueListItem3.DataValue = "ToCarriedBy";
        ValueListItem3.DisplayText = "to held by";
        ValueListItem4.DataValue = "ToLocation";
        ValueListItem4.DisplayText = "to location";
        ValueListItem5.DataValue = "ToLocationGroup";
        ValueListItem5.DisplayText = "to location group";
        ValueListItem6.DataValue = "ToSameLocationAs";
        ValueListItem6.DisplayText = "to same location as";
        ValueListItem7.DataValue = "ToWornBy";
        ValueListItem7.DisplayText = "to worn by";
        Me.cmbObjectMoveTo.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7});
        Me.cmbObjectMoveTo.Location = New System.Drawing.Point(760, 9);
        Me.cmbObjectMoveTo.Name = "cmbObjectMoveTo";
        Me.cmbObjectMoveTo.Size = New System.Drawing.Size(170, 21);
        Me.cmbObjectMoveTo.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbObjectMoveTo.TabIndex = 3;
        '
        'cmbObjectKey1
        '
        Me.cmbObjectKey1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbObjectKey1.Location = New System.Drawing.Point(225, 9);
        Me.cmbObjectKey1.Name = "cmbObjectKey1";
        Me.cmbObjectKey1.Size = New System.Drawing.Size(219, 21);
        Me.cmbObjectKey1.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbObjectKey1.TabIndex = 2;
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterPropertyValue);
        Me.UltraTabPageControl2.Controls.Add(Me.lblCharactersWithValue);
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterMoveWho);
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterWhat);
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterMoveTo);
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterKey2);
        Me.UltraTabPageControl2.Controls.Add(Me.cmbCharacterKey1);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(886, 38);
        '
        'lblCharactersWithValue
        '
        Me.lblCharactersWithValue.BackColor = System.Drawing.Color.Transparent;
        Me.lblCharactersWithValue.Location = New System.Drawing.Point(458, 13);
        Me.lblCharactersWithValue.Name = "lblCharactersWithValue";
        Me.lblCharactersWithValue.Size = New System.Drawing.Size(59, 16);
        Me.lblCharactersWithValue.TabIndex = 13;
        Me.lblCharactersWithValue.Text = "with value";
        Me.lblCharactersWithValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        Me.lblCharactersWithValue.Visible = false;
        '
        'cmbCharacterMoveWho
        '
        Me.cmbCharacterMoveWho.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbCharacterMoveWho.Location = New System.Drawing.Point(79, 9);
        Me.cmbCharacterMoveWho.Name = "cmbCharacterMoveWho";
        Me.cmbCharacterMoveWho.Size = New System.Drawing.Size(147, 21);
        Me.cmbCharacterMoveWho.TabIndex = 11;
        '
        'cmbCharacterWhat
        '
        Me.cmbCharacterWhat.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbCharacterWhat.Location = New System.Drawing.Point(11, 9);
        Me.cmbCharacterWhat.Name = "cmbCharacterWhat";
        Me.cmbCharacterWhat.Size = New System.Drawing.Size(69, 21);
        Me.cmbCharacterWhat.TabIndex = 10;
        '
        'cmbCharacterMoveTo
        '
        Me.cmbCharacterMoveTo.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbCharacterMoveTo.Location = New System.Drawing.Point(712, 9);
        Me.cmbCharacterMoveTo.Name = "cmbCharacterMoveTo";
        Me.cmbCharacterMoveTo.Size = New System.Drawing.Size(168, 21);
        Me.cmbCharacterMoveTo.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbCharacterMoveTo.TabIndex = 9;
        '
        'cmbCharacterKey2
        '
        Me.cmbCharacterKey2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbCharacterKey2.Location = New System.Drawing.Point(900, 9);
        Me.cmbCharacterKey2.MaxDropDownItems = 12;
        Me.cmbCharacterKey2.Name = "cmbCharacterKey2";
        Me.cmbCharacterKey2.Size = New System.Drawing.Size(122, 21);
        Me.cmbCharacterKey2.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbCharacterKey2.TabIndex = 8;
        '
        'cmbCharacterKey1
        '
        Me.cmbCharacterKey1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbCharacterKey1.Location = New System.Drawing.Point(225, 9);
        Me.cmbCharacterKey1.Name = "cmbCharacterKey1";
        Me.cmbCharacterKey1.Size = New System.Drawing.Size(192, 21);
        Me.cmbCharacterKey1.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbCharacterKey1.TabIndex = 6;
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationPropertyValue);
        Me.UltraTabPageControl5.Controls.Add(Me.lblLocationsWithValue);
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationKey2);
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationMoveWhat);
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationWhat);
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationMoveTo);
        Me.UltraTabPageControl5.Controls.Add(Me.cmbLocationKey1);
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5";
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(886, 38);
        '
        'lblLocationsWithValue
        '
        Me.lblLocationsWithValue.BackColor = System.Drawing.Color.Transparent;
        Me.lblLocationsWithValue.Location = New System.Drawing.Point(508, 13);
        Me.lblLocationsWithValue.Name = "lblLocationsWithValue";
        Me.lblLocationsWithValue.Size = New System.Drawing.Size(59, 16);
        Me.lblLocationsWithValue.TabIndex = 13;
        Me.lblLocationsWithValue.Text = "with value";
        Me.lblLocationsWithValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        Me.lblLocationsWithValue.Visible = false;
        '
        'cmbLocationKey2
        '
        Me.cmbLocationKey2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbLocationKey2.Location = New System.Drawing.Point(936, 9);
        Me.cmbLocationKey2.Name = "cmbLocationKey2";
        Me.cmbLocationKey2.Size = New System.Drawing.Size(199, 21);
        Me.cmbLocationKey2.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbLocationKey2.TabIndex = 12;
        '
        'cmbLocationMoveWhat
        '
        Me.cmbLocationMoveWhat.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbLocationMoveWhat.Location = New System.Drawing.Point(79, 9);
        Me.cmbLocationMoveWhat.Name = "cmbLocationMoveWhat";
        Me.cmbLocationMoveWhat.Size = New System.Drawing.Size(153, 21);
        Me.cmbLocationMoveWhat.TabIndex = 11;
        '
        'cmbLocationWhat
        '
        Me.cmbLocationWhat.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbLocationWhat.Location = New System.Drawing.Point(11, 9);
        Me.cmbLocationWhat.Name = "cmbLocationWhat";
        Me.cmbLocationWhat.Size = New System.Drawing.Size(69, 21);
        Me.cmbLocationWhat.TabIndex = 10;
        '
        'cmbLocationMoveTo
        '
        Me.cmbLocationMoveTo.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem8.DataValue = "InsideObject";
        ValueListItem8.DisplayText = "inside object";
        ValueListItem9.DataValue = "OntoObject";
        ValueListItem9.DisplayText = "onto object";
        ValueListItem10.DataValue = "ToCarriedBy";
        ValueListItem10.DisplayText = "to held by";
        ValueListItem11.DataValue = "ToLocation";
        ValueListItem11.DisplayText = "to location";
        ValueListItem12.DataValue = "ToLocationGroup";
        ValueListItem12.DisplayText = "to location group";
        ValueListItem13.DataValue = "ToSameLocationAs";
        ValueListItem13.DisplayText = "to same location as";
        ValueListItem14.DataValue = "ToWornBy";
        ValueListItem14.DisplayText = "to worn by";
        Me.cmbLocationMoveTo.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem8, ValueListItem9, ValueListItem10, ValueListItem11, ValueListItem12, ValueListItem13, ValueListItem14});
        Me.cmbLocationMoveTo.Location = New System.Drawing.Point(760, 9);
        Me.cmbLocationMoveTo.Name = "cmbLocationMoveTo";
        Me.cmbLocationMoveTo.Size = New System.Drawing.Size(170, 21);
        Me.cmbLocationMoveTo.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbLocationMoveTo.TabIndex = 9;
        '
        'cmbLocationKey1
        '
        Me.cmbLocationKey1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbLocationKey1.Location = New System.Drawing.Point(231, 9);
        Me.cmbLocationKey1.Name = "cmbLocationKey1";
        Me.cmbLocationKey1.Size = New System.Drawing.Size(219, 21);
        Me.cmbLocationKey1.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbLocationKey1.TabIndex = 8;
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Controls.Add(Me.btnParams);
        Me.UltraTabPageControl6.Controls.Add(Me.cmbTasks);
        Me.UltraTabPageControl6.Controls.Add(Me.cmbWhatT);
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6";
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(886, 38);
        '
        'btnParams
        '
        Me.btnParams.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnParams.Location = New System.Drawing.Point(801, 9);
        Me.btnParams.Name = "btnParams";
        Me.btnParams.Size = New System.Drawing.Size(76, 21);
        Me.btnParams.TabIndex = 11;
        Me.btnParams.Text = "params";
        Me.btnParams.Visible = false;
        '
        'cmbTasks
        '
        Me.cmbTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbTasks.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbTasks.Location = New System.Drawing.Point(125, 9);
        Me.cmbTasks.MaxDropDownItems = 12;
        Me.cmbTasks.Name = "cmbTasks";
        Me.cmbTasks.Size = New System.Drawing.Size(694, 21);
        Me.cmbTasks.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbTasks.TabIndex = 10;
        '
        'cmbWhatT
        '
        Me.cmbWhatT.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem15.DataValue = "Execute";
        ValueListItem15.DisplayText = "Run Task";
        ValueListItem16.DataValue = "Unset";
        ValueListItem16.DisplayText = "Unset Task";
        Me.cmbWhatT.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem15, ValueListItem16});
        Me.cmbWhatT.Location = New System.Drawing.Point(11, 9);
        Me.cmbWhatT.Name = "cmbWhatT";
        Me.cmbWhatT.Size = New System.Drawing.Size(115, 21);
        Me.cmbWhatT.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbWhatT.TabIndex = 9;
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.cmbVariableSet);
        Me.UltraTabPageControl4.Controls.Add(Me.Expression);
        Me.UltraTabPageControl4.Controls.Add(Me.cmbIndexEdit);
        Me.UltraTabPageControl4.Controls.Add(Me.cmbVariable);
        Me.UltraTabPageControl4.Controls.Add(Me.lblRight);
        Me.UltraTabPageControl4.Controls.Add(Me.cmbIndex);
        Me.UltraTabPageControl4.Controls.Add(Me.cmbVariableArray);
        Me.UltraTabPageControl4.Controls.Add(Me.lblLeft);
        Me.UltraTabPageControl4.Controls.Add(Me.lblLoop);
        Me.UltraTabPageControl4.Controls.Add(Me.lblNext);
        Me.UltraTabPageControl4.Controls.Add(Me.lblTo2);
        Me.UltraTabPageControl4.Controls.Add(Me.txtLoopTo);
        Me.UltraTabPageControl4.Controls.Add(Me.txtLoopFrom);
        Me.UltraTabPageControl4.Controls.Add(Me.lblFor);
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4";
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(886, 38);
        '
        'cmbVariableSet
        '
        Me.cmbVariableSet.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbVariableSet.Location = New System.Drawing.Point(11, 9);
        Me.cmbVariableSet.Name = "cmbVariableSet";
        Me.cmbVariableSet.Size = New System.Drawing.Size(74, 21);
        Me.cmbVariableSet.TabIndex = 16;
        '
        'cmbIndexEdit
        '
        Appearance1.TextHAlignAsString = "Right";
        Me.cmbIndexEdit.Appearance = Appearance1;
        ValueListItem17.DataValue = "ValueListItem1";
        ValueListItem17.DisplayText = "Select a variable";
        Me.cmbIndexEdit.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem17});
        Me.cmbIndexEdit.Location = New System.Drawing.Point(621, 6);
        Me.cmbIndexEdit.Name = "cmbIndexEdit";
        Me.cmbIndexEdit.Size = New System.Drawing.Size(125, 21);
        Me.cmbIndexEdit.TabIndex = 14;
        Me.cmbIndexEdit.Text = "0";
        Me.cmbIndexEdit.Visible = false;
        '
        'cmbVariable
        '
        Me.cmbVariable.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbVariable.Location = New System.Drawing.Point(84, 9);
        Me.cmbVariable.Name = "cmbVariable";
        Me.cmbVariable.Size = New System.Drawing.Size(152, 21);
        Me.cmbVariable.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbVariable.TabIndex = 6;
        '
        'lblRight
        '
        Me.lblRight.AutoSize = true;
        Me.lblRight.BackColor = System.Drawing.Color.Transparent;
        Me.lblRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblRight.Location = New System.Drawing.Point(302, 11);
        Me.lblRight.Name = "lblRight";
        Me.lblRight.Size = New System.Drawing.Size(15, 16);
        Me.lblRight.TabIndex = 11;
        Me.lblRight.Text = "=";
        '
        'cmbIndex
        '
        Me.cmbIndex.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbIndex.Location = New System.Drawing.Point(242, 9);
        Me.cmbIndex.Name = "cmbIndex";
        Me.cmbIndex.Size = New System.Drawing.Size(84, 21);
        Me.cmbIndex.TabIndex = 9;
        Me.cmbIndex.Visible = false;
        '
        'cmbVariableArray
        '
        Me.cmbVariableArray.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbVariableArray.Location = New System.Drawing.Point(108, 59);
        Me.cmbVariableArray.Name = "cmbVariableArray";
        Me.cmbVariableArray.Size = New System.Drawing.Size(125, 21);
        Me.cmbVariableArray.TabIndex = 12;
        '
        'lblLeft
        '
        Me.lblLeft.AutoSize = true;
        Me.lblLeft.BackColor = System.Drawing.Color.Transparent;
        Me.lblLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblLeft.Location = New System.Drawing.Point(162, 11);
        Me.lblLeft.Name = "lblLeft";
        Me.lblLeft.Size = New System.Drawing.Size(12, 16);
        Me.lblLeft.TabIndex = 10;
        Me.lblLeft.Text = "[";
        Me.lblLeft.Visible = false;
        '
        'lblLoop
        '
        Me.lblLoop.AutoSize = true;
        Me.lblLoop.BackColor = System.Drawing.Color.Transparent;
        Me.lblLoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblLoop.Location = New System.Drawing.Point(263, 61);
        Me.lblLoop.Name = "lblLoop";
        Me.lblLoop.Size = New System.Drawing.Size(63, 16);
        Me.lblLoop.TabIndex = 7;
        Me.lblLoop.Text = "[Loop]   =";
        Me.lblLoop.Visible = false;
        '
        'lblNext
        '
        Me.lblNext.AutoSize = true;
        Me.lblNext.BackColor = System.Drawing.Color.Transparent;
        Me.lblNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblNext.Location = New System.Drawing.Point(22, 98);
        Me.lblNext.Name = "lblNext";
        Me.lblNext.Size = New System.Drawing.Size(78, 16);
        Me.lblNext.TabIndex = 4;
        Me.lblNext.Text = "NEXT Loop";
        Me.lblNext.Visible = false;
        '
        'lblTo2
        '
        Me.lblTo2.AutoSize = true;
        Me.lblTo2.BackColor = System.Drawing.Color.Transparent;
        Me.lblTo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblTo2.Location = New System.Drawing.Point(190, 23);
        Me.lblTo2.Name = "lblTo2";
        Me.lblTo2.Size = New System.Drawing.Size(27, 16);
        Me.lblTo2.TabIndex = 3;
        Me.lblTo2.Text = "TO";
        Me.lblTo2.Visible = false;
        '
        'txtLoopTo
        '
        Me.txtLoopTo.Location = New System.Drawing.Point(225, 23);
        Me.txtLoopTo.Name = "txtLoopTo";
        Me.txtLoopTo.Size = New System.Drawing.Size(71, 20);
        Me.txtLoopTo.TabIndex = 2;
        Me.txtLoopTo.Text = "0";
        Me.txtLoopTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        Me.txtLoopTo.Visible = false;
        '
        'txtLoopFrom
        '
        Me.txtLoopFrom.Location = New System.Drawing.Point(108, 22);
        Me.txtLoopFrom.Name = "txtLoopFrom";
        Me.txtLoopFrom.Size = New System.Drawing.Size(71, 20);
        Me.txtLoopFrom.TabIndex = 1;
        Me.txtLoopFrom.Text = "0";
        Me.txtLoopFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        Me.txtLoopFrom.Visible = false;
        '
        'lblFor
        '
        Me.lblFor.AutoSize = true;
        Me.lblFor.BackColor = System.Drawing.Color.Transparent;
        Me.lblFor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblFor.Location = New System.Drawing.Point(22, 23);
        Me.lblFor.Name = "lblFor";
        Me.lblFor.Size = New System.Drawing.Size(80, 16);
        Me.lblFor.TabIndex = 0;
        Me.lblFor.Text = "FOR Loop =";
        Me.lblFor.Visible = false;
        '
        'UltraTabPageControl8
        '
        Me.UltraTabPageControl8.Controls.Add(Me.cmbAskWho);
        Me.UltraTabPageControl8.Controls.Add(Me.txtConvTopic);
        Me.UltraTabPageControl8.Controls.Add(Me.lblConvAbout);
        Me.UltraTabPageControl8.Controls.Add(Me.cmbWhatConv);
        Me.UltraTabPageControl8.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl8.Name = "UltraTabPageControl8";
        Me.UltraTabPageControl8.Size = New System.Drawing.Size(886, 38);
        '
        'cmbAskWho
        '
        Me.cmbAskWho.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbAskWho.Location = New System.Drawing.Point(157, 9);
        Me.cmbAskWho.Name = "cmbAskWho";
        Me.cmbAskWho.Size = New System.Drawing.Size(147, 21);
        Me.cmbAskWho.TabIndex = 13;
        '
        'txtConvTopic
        '
        Me.txtConvTopic.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtConvTopic.Location = New System.Drawing.Point(347, 9);
        Me.txtConvTopic.Name = "txtConvTopic";
        Me.txtConvTopic.Size = New System.Drawing.Size(527, 21);
        Me.txtConvTopic.TabIndex = 17;
        '
        'lblConvAbout
        '
        Me.lblConvAbout.BackColor = System.Drawing.Color.Transparent;
        Me.lblConvAbout.Location = New System.Drawing.Point(309, 13);
        Me.lblConvAbout.Name = "lblConvAbout";
        Me.lblConvAbout.Size = New System.Drawing.Size(45, 16);
        Me.lblConvAbout.TabIndex = 16;
        Me.lblConvAbout.Text = "about";
        '
        'cmbWhatConv
        '
        Me.cmbWhatConv.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbWhatConv.Location = New System.Drawing.Point(11, 9);
        Me.cmbWhatConv.Name = "cmbWhatConv";
        Me.cmbWhatConv.Size = New System.Drawing.Size(147, 21);
        Me.cmbWhatConv.TabIndex = 12;
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.cmbPropertyValue);
        Me.UltraTabPageControl3.Controls.Add(Me.isPropertyKey1);
        Me.UltraTabPageControl3.Controls.Add(Me.lblTo);
        Me.UltraTabPageControl3.Controls.Add(Me.lblSet);
        Me.UltraTabPageControl3.Controls.Add(Me.cmbPropertyKey2);
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3";
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(886, 38);
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.Color.Transparent;
        Me.lblTo.Location = New System.Drawing.Point(362, 13);
        Me.lblTo.Name = "lblTo";
        Me.lblTo.Size = New System.Drawing.Size(16, 16);
        Me.lblTo.TabIndex = 15;
        Me.lblTo.Text = "to";
        '
        'lblSet
        '
        Me.lblSet.BackColor = System.Drawing.Color.Transparent;
        Me.lblSet.Location = New System.Drawing.Point(8, 13);
        Me.lblSet.Name = "lblSet";
        Me.lblSet.Size = New System.Drawing.Size(32, 16);
        Me.lblSet.TabIndex = 14;
        Me.lblSet.Text = "Set";
        '
        'cmbPropertyKey2
        '
        Me.cmbPropertyKey2.Anchor = System.Windows.Forms.AnchorStyles.Top;
        Me.cmbPropertyKey2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbPropertyKey2.Location = New System.Drawing.Point(228, 9);
        Me.cmbPropertyKey2.MaxDropDownItems = 12;
        Me.cmbPropertyKey2.Name = "cmbPropertyKey2";
        Me.cmbPropertyKey2.Size = New System.Drawing.Size(122, 21);
        Me.cmbPropertyKey2.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbPropertyKey2.TabIndex = 12;
        '
        'UltraTabPageControl7
        '
        Me.UltraTabPageControl7.Controls.Add(Me.cmbEndGame);
        Me.UltraTabPageControl7.Controls.Add(Me.Label2);
        Me.UltraTabPageControl7.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl7.Name = "UltraTabPageControl7";
        Me.UltraTabPageControl7.Size = New System.Drawing.Size(886, 38);
        '
        'cmbEndGame
        '
        Me.cmbEndGame.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbEndGame.Location = New System.Drawing.Point(89, 9);
        Me.cmbEndGame.Name = "cmbEndGame";
        Me.cmbEndGame.Size = New System.Drawing.Size(188, 21);
        Me.cmbEndGame.TabIndex = 3;
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent;
        Me.Label2.Location = New System.Drawing.Point(5, 13);
        Me.Label2.Name = "Label2";
        Me.Label2.Size = New System.Drawing.Size(94, 16);
        Me.Label2.TabIndex = 11;
        Me.Label2.Text = "End the game:";
        '
        'chkLoop
        '
        Me.chkLoop.AutoSize = true;
        Me.chkLoop.BackColor = System.Drawing.Color.Transparent;
        Me.chkLoop.Location = New System.Drawing.Point(15, 17);
        Me.chkLoop.Name = "chkLoop";
        Me.chkLoop.Size = New System.Drawing.Size(88, 17);
        Me.chkLoop.TabIndex = 12;
        Me.chkLoop.Text = "I need a loop";
        Me.chkLoop.UseVisualStyleBackColor = false;
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(788, 72);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 7;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(692, 72);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 6;
        Me.btnOK.Text = "OK";
        '
        'tabsActions
        '
        Me.tabsActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.tabsActions.Controls.Add(Me.UltraTabSharedControlsPage2);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl3);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl4);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl6);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl7);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl5);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl8);
        Me.tabsActions.Controls.Add(Me.UltraTabPageControl9);
        Me.tabsActions.Location = New System.Drawing.Point(0, 0);
        Me.tabsActions.Name = "tabsActions";
        Me.tabsActions.SharedControlsPage = Me.UltraTabSharedControlsPage2;
        Me.tabsActions.Size = New System.Drawing.Size(890, 64);
        Me.tabsActions.TabIndex = 8;
        Appearance2.Image = Global.ADRIFT.My.Resources.Resources.imgObjectDynamic16;
        UltraTab1.Appearance = Appearance2;
        UltraTab1.Key = "MoveObjects";
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Move Objects";
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgCharacter16;
        UltraTab2.Appearance = Appearance3;
        UltraTab2.Key = "MoveCharacters";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Move Characters";
        Appearance4.Image = Global.ADRIFT.My.Resources.Resources.imgLocation16;
        UltraTab3.Appearance = Appearance4;
        UltraTab3.Key = "Locations";
        UltraTab3.TabPage = Me.UltraTabPageControl5;
        UltraTab3.Text = "Locations";
        Appearance5.Image = Global.ADRIFT.My.Resources.Resources.imgTaskSpecific16;
        UltraTab4.Appearance = Appearance5;
        UltraTab4.Key = "Tasks";
        UltraTab4.TabPage = Me.UltraTabPageControl6;
        UltraTab4.Text = "Tasks";
        Appearance6.Image = Global.ADRIFT.My.Resources.Resources.imgVariable16;
        UltraTab5.Appearance = Appearance6;
        UltraTab5.Key = "Variables";
        UltraTab5.TabPage = Me.UltraTabPageControl4;
        UltraTab5.Text = "Variables";
        Appearance7.Image = Global.ADRIFT.My.Resources.Resources.imgRefresh16;
        UltraTab6.Appearance = Appearance7;
        UltraTab6.Key = "Conversation";
        UltraTab6.TabPage = Me.UltraTabPageControl8;
        UltraTab6.Text = "Conversation";
        Appearance8.Image = Global.ADRIFT.My.Resources.Resources.imgProperty16;
        UltraTab7.Appearance = Appearance8;
        UltraTab7.Key = "SetProperties";
        UltraTab7.TabPage = Me.UltraTabPageControl3;
        UltraTab7.Text = "Set Properties";
        Appearance10.Image = Global.ADRIFT.My.Resources.Resources.imgEvent16;
        UltraTab9.Appearance = Appearance10;
        UltraTab9.Key = "Time";
        UltraTab9.TabPage = Me.UltraTabPageControl9;
        UltraTab9.Text = "Time";
        Appearance9.Image = Global.ADRIFT.My.Resources.Resources.imgCancel16;
        UltraTab8.Appearance = Appearance9;
        UltraTab8.Key = "EndGame";
        UltraTab8.TabPage = Me.UltraTabPageControl7;
        UltraTab8.Text = "End Game";
        Me.tabsActions.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3, UltraTab4, UltraTab5, UltraTab6, UltraTab7, UltraTab9, UltraTab8});
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2";
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(886, 38);
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Controls.Add(Me.chkLoop);
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 64);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        UltraStatusPanel1.Control = Me.chkLoop;
        UltraStatusPanel1.Padding = New System.Drawing.Size(14, 14);
        UltraStatusPanel1.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.ControlContainer;
        UltraStatusPanel1.Visible = false;
        Me.UltraStatusBar1.Panels.AddRange(New Infragistics.Win.UltraWinStatusBar.UltraStatusPanel() {UltraStatusPanel1});
        Me.UltraStatusBar1.Size = New System.Drawing.Size(890, 48);
        Me.UltraStatusBar1.TabIndex = 10;
        Me.UltraStatusBar1.ViewStyle = Infragistics.Win.UltraWinStatusBar.ViewStyle.Office2007;
        '
        'UltraTabPageControl9
        '
        Me.UltraTabPageControl9.Controls.Add(Me.Label3);
        Me.UltraTabPageControl9.Controls.Add(Me.Label1);
        Me.UltraTabPageControl9.Controls.Add(Me.expTurns);
        Me.UltraTabPageControl9.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl9.Name = "UltraTabPageControl9";
        Me.UltraTabPageControl9.Size = New System.Drawing.Size(886, 38);
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent;
        Me.Label1.Location = New System.Drawing.Point(8, 13);
        Me.Label1.Name = "Label1";
        Me.Label1.Size = New System.Drawing.Size(32, 16);
        Me.Label1.TabIndex = 17;
        Me.Label1.Text = "Skip";
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent;
        Me.Label3.Location = New System.Drawing.Point(186, 13);
        Me.Label3.Name = "Label3";
        Me.Label3.Size = New System.Drawing.Size(32, 16);
        Me.Label3.TabIndex = 18;
        Me.Label3.Text = "turns";
        '
        'cmbObjectPropertyValue
        '
        Me.cmbObjectPropertyValue.BackColor = System.Drawing.Color.Transparent;
        Me.cmbObjectPropertyValue.Location = New System.Drawing.Point(583, 9);
        Me.cmbObjectPropertyValue.Name = "cmbObjectPropertyValue";
        Me.cmbObjectPropertyValue.PropertyKey = "";
        Me.cmbObjectPropertyValue.Size = New System.Drawing.Size(171, 21);
        Me.cmbObjectPropertyValue.TabIndex = 12;
        Me.cmbObjectPropertyValue.Value = null;
        Me.cmbObjectPropertyValue.Visible = false;
        '
        'cmbCharacterPropertyValue
        '
        Me.cmbCharacterPropertyValue.BackColor = System.Drawing.Color.Transparent;
        Me.cmbCharacterPropertyValue.Location = New System.Drawing.Point(516, 9);
        Me.cmbCharacterPropertyValue.Name = "cmbCharacterPropertyValue";
        Me.cmbCharacterPropertyValue.PropertyKey = "";
        Me.cmbCharacterPropertyValue.Size = New System.Drawing.Size(171, 21);
        Me.cmbCharacterPropertyValue.TabIndex = 14;
        Me.cmbCharacterPropertyValue.Value = null;
        Me.cmbCharacterPropertyValue.Visible = false;
        '
        'cmbPropertyValue
        '
        Me.cmbPropertyValue.Location = New System.Drawing.Point(529, 9);
        Me.cmbPropertyValue.Name = "cmbPropertyValue";
        Me.cmbPropertyValue.PropertyKey = "";
        Me.cmbPropertyValue.Size = New System.Drawing.Size(187, 21);
        Me.cmbPropertyValue.TabIndex = 16;
        Me.cmbPropertyValue.Value = null;
        '
        'isPropertyKey1
        '
        Me.isPropertyKey1.AllowAddEdit = false;
        Me.isPropertyKey1.AllowedListType = CType(((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.[Object]) _;
            | ADRIFT.ItemSelector.ItemEnum.Character), ADRIFT.ItemSelector.ItemEnum);
        Me.isPropertyKey1.AllowHidden = false;
        Me.isPropertyKey1.BackColor = System.Drawing.Color.Transparent;
        Me.isPropertyKey1.Key = null;
        Me.isPropertyKey1.ListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.isPropertyKey1.Location = New System.Drawing.Point(30, 9);
        Me.isPropertyKey1.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.isPropertyKey1.MinimumSize = New System.Drawing.Size(10, 21);
        Me.isPropertyKey1.Name = "isPropertyKey1";
        Me.isPropertyKey1.RestrictProperty = null;
        Me.isPropertyKey1.Size = New System.Drawing.Size(192, 21);
        Me.isPropertyKey1.TabIndex = 10;
        '
        'Expression
        '
        Me.Expression.BackColor = System.Drawing.Color.Transparent;
        Me.Expression.Location = New System.Drawing.Point(391, 9);
        Me.Expression.Name = "Expression";
        Me.Expression.Size = New System.Drawing.Size(196, 21);
        Me.Expression.TabIndex = 15;
        Me.Expression.Value = "";
        '
        'cmbLocationPropertyValue
        '
        Me.cmbLocationPropertyValue.BackColor = System.Drawing.Color.Transparent;
        Me.cmbLocationPropertyValue.Location = New System.Drawing.Point(566, 9);
        Me.cmbLocationPropertyValue.Name = "cmbLocationPropertyValue";
        Me.cmbLocationPropertyValue.PropertyKey = "";
        Me.cmbLocationPropertyValue.Size = New System.Drawing.Size(171, 21);
        Me.cmbLocationPropertyValue.TabIndex = 14;
        Me.cmbLocationPropertyValue.Value = null;
        Me.cmbLocationPropertyValue.Visible = false;
        '
        'expTurns
        '
        Me.expTurns.BackColor = System.Drawing.Color.Transparent;
        Me.expTurns.Location = New System.Drawing.Point(46, 8);
        Me.expTurns.Name = "expTurns";
        Me.expTurns.Size = New System.Drawing.Size(124, 21);
        Me.expTurns.TabIndex = 16;
        Me.expTurns.Value = "";
        '
        'frmAction
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(890, 112);
        Me.Controls.Add(Me.tabsActions);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.MaximizeBox = false;
        Me.MaximumSize = New System.Drawing.Size(1600, 148);
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(544, 148);
        Me.Name = "frmAction";
        Me.ShowInTaskbar = false;
        Me.Text = "Action";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectMoveWhat).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectWhat).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectKey2).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectMoveTo).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbObjectKey1).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.UltraTabPageControl2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterMoveWho).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterWhat).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterMoveTo).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterKey2).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbCharacterKey1).EndInit();
        Me.UltraTabPageControl5.ResumeLayout(false);
        Me.UltraTabPageControl5.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationKey2).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationMoveWhat).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationWhat).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationMoveTo).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLocationKey1).EndInit();
        Me.UltraTabPageControl6.ResumeLayout(false);
        Me.UltraTabPageControl6.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbTasks).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbWhatT).EndInit();
        Me.UltraTabPageControl4.ResumeLayout(false);
        Me.UltraTabPageControl4.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariableSet).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbIndexEdit).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariable).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbIndex).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbVariableArray).EndInit();
        Me.UltraTabPageControl8.ResumeLayout(false);
        Me.UltraTabPageControl8.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAskWho).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtConvTopic).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbWhatConv).EndInit();
        Me.UltraTabPageControl3.ResumeLayout(false);
        Me.UltraTabPageControl3.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbPropertyKey2).EndInit();
        Me.UltraTabPageControl7.ResumeLayout(false);
        Me.UltraTabPageControl7.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbEndGame).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsActions).EndInit();
        Me.tabsActions.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        Me.UltraStatusBar1.ResumeLayout(false);
        Me.UltraStatusBar1.PerformLayout();
        Me.UltraTabPageControl9.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    public clsAction Action;
    private string sCommand;

    private New StringArrayList arlDirectionRefs;
    private New StringArrayList arlObjectRefs;
    private New StringArrayList arlCharacterRefs;
    private New StringArrayList arlLocationRefs;
    private New StringArrayList arlNumberRefs;
    private New StringArrayList arlTextRefs;
    private New StringArrayList arlItemRefs;

    private bool bLoadingAction;


    public void LoadAction(clsAction Action)
    {

        bLoadingAction = True

        LoadCombos();

        Me.Action = Action;

        With Action;
            switch (.eItem)
            {
                case clsAction.ItemEnum.MoveObject:
                case clsAction.ItemEnum.AddObjectToGroup:
                case clsAction.ItemEnum.RemoveObjectFromGroup:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("MoveObjects");
                    If .sKey1 != null Then ' To prevent loading combos on first load
                        SetCombo(cmbObjectWhat, .eItem.ToString);
                        SetCombo(cmbObjectMoveWhat, .eMoveObjectWhat.ToString);
                    }
                    SetCombo(cmbObjectKey1, .sKey1);
                    If .sPropertyValue != null Then cmbObjectPropertyValue.Value = .sPropertyValue
                    If .sKey1 != null Then SetCombo(cmbObjectMoveTo, WriteEnum(.eMoveObjectTo))
                    SetCombo(cmbObjectKey2, .sKey2);

                case clsAction.ItemEnum.MoveCharacter:
                case clsAction.ItemEnum.AddCharacterToGroup:
                case clsAction.ItemEnum.RemoveCharacterFromGroup:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("MoveCharacters");
                    SetCombo(cmbCharacterWhat, .eItem.ToString);
                    SetCombo(cmbCharacterMoveWho, .eMoveCharacterWho.ToString);
                    SetCombo(cmbCharacterKey1, .sKey1);
                    If .sPropertyValue != null Then cmbCharacterPropertyValue.Value = .sPropertyValue
                    SetCombo(cmbCharacterMoveTo, WriteEnum(.eMoveCharacterTo));
                    SetCombo(cmbCharacterKey2, .sKey2);

                case clsAction.ItemEnum.AddLocationToGroup:
                case clsAction.ItemEnum.RemoveLocationFromGroup:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("Locations");
                    SetCombo(cmbLocationWhat, .eItem.ToString);
                    SetCombo(cmbLocationMoveWhat, .eMoveLocationWhat.ToString);
                    SetCombo(cmbLocationKey1, .sKey1);
                    If .sPropertyValue != null Then cmbLocationPropertyValue.Value = .sPropertyValue
                    SetCombo(cmbLocationMoveTo, WriteEnum(.eMoveLocationTo));
                    SetCombo(cmbLocationKey2, .sKey2);

                case clsAction.ItemEnum.SetProperties:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("SetProperties");
                    isPropertyKey1.Key = .sKey1 ' SetCombo(cmbProperty, .sKey1);
                    SetCombo(cmbPropertyKey2, .sKey2);
                    'SetCombo(cmbPropertyExtra, .StringValue)
                    If .sPropertyValue != null Then cmbPropertyValue.Value = .sPropertyValue

                case clsAction.ItemEnum.SetVariable:
                case clsAction.ItemEnum.IncreaseVariable:
                case clsAction.ItemEnum.DecreaseVariable:
                    {
                    ' Convert old actions
                    if (.eItem = clsAction.ItemEnum.SetVariable && .sKey1 <> "" && Adventure.htblVariables(.sKey1).Type = clsVariable.VariableTypeEnum.Numeric)
                    {
                        if (.StringValue.Replace(" ", "").StartsWith("%" + Adventure.htblVariables(.sKey1).Name + "%+"))
                        {
                            .eItem = clsAction.ItemEnum.IncreaseVariable;
                            .StringValue = .StringValue.Substring(.StringValue.IndexOf("+") + 1);
                            If .StringValue.StartsWith(" ") Then .StringValue = .StringValue.Substring(1)
                        ElseIf .StringValue.Replace(" ", "").StartsWith("%" + Adventure.htblVariables(.sKey1).Name + "%-") Then
                            .eItem = clsAction.ItemEnum.DecreaseVariable;
                            .StringValue = .StringValue.Substring(.StringValue.IndexOf("-") + 1);
                            If .StringValue.StartsWith(" ") Then .StringValue = .StringValue.Substring(1)
                        }
                    }

                    switch (.eItem)
                    {
                        case clsAction.ItemEnum.SetVariable:
                            {
                            SetCombo(cmbVariableSet, clsAction.ItemEnum.SetVariable.ToString);
                        case clsAction.ItemEnum.IncreaseVariable:
                            {
                            SetCombo(cmbVariableSet, clsAction.ItemEnum.IncreaseVariable.ToString);
                        case clsAction.ItemEnum.DecreaseVariable:
                            {
                            SetCombo(cmbVariableSet, clsAction.ItemEnum.DecreaseVariable.ToString);
                    }
                    tabsActions.SelectedTab = tabsActions.Tabs("Variables");
                    SetCombo(cmbVariable, .sKey1);
                    Expression.Value = .StringValue;
                    'If Expression.Value.StartsWith("""") AndAlso Expression.Value.EndsWith("""") Then
                    '    Expression.Value = Expression.Value.Substring(1, Expression.Value.Length - 2)
                    'End If
                    if (.eVariables = clsAction.VariablesEnum.Assignment)
                    {
                        ' We're a normal assignment
                        if (IsNumeric(.sKey2))
                        {
                            cmbIndexEdit.Visible = true;
                            cmbIndexEdit.Text = .sKey2;
                            cmbIndex.Visible = false;
                        Else
                            'cmbIndex.Visible = True
                            SetCombo(cmbIndex, .sKey2);
                            cmbIndexEdit.Visible = false;
                        }
                    Else
                        ' We're a loop
                        chkLoop.Checked = true;
                        txtLoopFrom.Text = .IntValue.ToString;
                        txtLoopTo.Text = .sKey2;
                    }

                case clsAction.ItemEnum.SetTasks:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("Tasks");
                    SetCombo(cmbWhatT, WriteEnum(.eSetTasks));
                    SetCombo(cmbTasks, .sKey1);
                    btnParams.Tag = .StringValue;
                    if (.IntValue <> 0 || .sPropertyValue <> "")
                    {
                        chkLoop.Checked = true;
                        txtLoopFrom.Text = .IntValue.ToString;
                        txtLoopTo.Text = .sPropertyValue;
                    }

                case clsAction.ItemEnum.Conversation:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("Conversation");
                    SetCombo(cmbWhatConv, CInt(.eConversation));
                    SetCombo(cmbAskWho, .sKey1);
                    txtConvTopic.Text = .StringValue;

                case clsAction.ItemEnum.Time:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("Time");
                    expTurns.Value = .StringValue;

                case clsAction.ItemEnum.EndGame:
                    {
                    tabsActions.SelectedTab = tabsActions.Tabs("EndGame");
                    SetCombo(cmbEndGame, WriteEnum(.eEndgame));

            }
        }

        bLoadingAction = False

    }


    private void cmbObjectWhat_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadObjectMoveToCombo();
    }


    private void cmbObjectMoveWhat_SelectionChanged(object sender, System.EventArgs e)
    {

        If cmbObjectMoveWhat.SelectedItem == null Then Exit Sub

        With cmbObjectKey1;
            .Items.Clear();

            switch (CType([Enum].Parse(GetType(clsAction.MoveObjectWhatEnum), cmbObjectMoveWhat.SelectedItem.DataValue.ToString), clsAction.MoveObjectWhatEnum))
            {
                case clsAction.MoveObjectWhatEnum.EverythingHeldBy:
                case clsAction.MoveObjectWhatEnum.EverythingWornBy:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(c.Key, c.Name);
                    Next;
                case clsAction.MoveObjectWhatEnum.EverythingInGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        If g.GroupType = clsGroup.GroupTypeEnum.Objects Then .Items.Add(g.Key, g.Name)
                    Next;
                case clsAction.MoveObjectWhatEnum.EverythingInside:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.IsContainer Then .Items.Add(ob.Key, ob.FullName)
                    Next;
                case clsAction.MoveObjectWhatEnum.EverythingOn:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.HasSurface Then .Items.Add(ob.Key, ob.FullName)
                    Next;
                case clsAction.MoveObjectWhatEnum.EverythingWithProperty:
                    {
                    For Each p As clsProperty In Adventure.htblObjectProperties.Values
                        .Items.Add(p.Key, p.CommonName);
                    Next;
                case clsAction.MoveObjectWhatEnum.Object:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        .Items.Add(ob.Key, ob.FullName);
                    Next;
                case clsAction.MoveObjectWhatEnum.EverythingAtLocation:
                    {
                    For Each l As String In arlLocationRefs
                        .Items.Add(l, Adventure.GetNameFromKey(l, false));
                    Next;
                    For Each l As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(l.Key, l.ShortDescription.ToString);
                    Next;
            }

        }

    }


    private void cmbCharacterMoveWho_SelectionChanged(object sender, System.EventArgs e)
    {

        If cmbCharacterMoveWho.SelectedItem == null Then Exit Sub

        With cmbCharacterKey1;
            .Items.Clear();

            switch (CType([Enum].Parse(GetType(clsAction.MoveCharacterWhoEnum), cmbCharacterMoveWho.SelectedItem.DataValue.ToString), clsAction.MoveCharacterWhoEnum))
            {
                case clsAction.MoveCharacterWhoEnum.Character:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(c.Key, c.Name);
                    Next;
                case clsAction.MoveCharacterWhoEnum.EveryoneAtLocation:
                    {
                    For Each l As String In arlLocationRefs
                        .Items.Add(l, Adventure.GetNameFromKey(l, false));
                    Next;
                    For Each l As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(l.Key, l.ShortDescription.ToString);
                    Next;
                case clsAction.MoveCharacterWhoEnum.EveryoneInGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        If g.GroupType = clsGroup.GroupTypeEnum.Characters Then .Items.Add(g.Key, g.Name)
                    Next;
                case clsAction.MoveCharacterWhoEnum.EveryoneInside:
                    {
                    For Each o As clsObject In Adventure.htblObjects.Values
                        If o.IsContainer Then .Items.Add(o.Key, o.FullName)
                    Next;
                case clsAction.MoveCharacterWhoEnum.EveryoneOn:
                    {
                    For Each o As clsObject In Adventure.htblObjects.Values
                        If o.HasSurface Then .Items.Add(o.Key, o.FullName)
                    Next;
                case clsAction.MoveCharacterWhoEnum.EveryoneWithProperty:
                    {
                    For Each p As clsProperty In Adventure.htblCharacterProperties.Values
                        .Items.Add(p.Key, p.CommonName);
                    Next;
            }

        }

    }


    private void cmbLocationMoveWhat_SelectionChanged(object sender, System.EventArgs e)
    {

        If cmbLocationMoveWhat.SelectedItem == null Then Exit Sub

        With cmbLocationKey1;
            .Items.Clear();

            switch (CType([Enum].Parse(GetType(clsAction.MoveLocationWhatEnum), cmbLocationMoveWhat.SelectedItem.DataValue.ToString), clsAction.MoveLocationWhatEnum))
            {
                case clsAction.MoveLocationWhatEnum.Location:
                    {
                    For Each l As String In arlLocationRefs
                        .Items.Add(l, Adventure.GetNameFromKey(l, false));
                    Next;
                    For Each l As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(l.Key, l.ShortDescription.ToString);
                    Next;
                case clsAction.MoveLocationWhatEnum.LocationOf:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(c.Key, c.Name);
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.HasSurface Then .Items.Add(ob.Key, ob.FullName)
                    Next;
                case clsAction.MoveLocationWhatEnum.EverywhereInGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        If g.GroupType = clsGroup.GroupTypeEnum.Locations Then .Items.Add(g.Key, g.Name)
                    Next;
                case clsAction.MoveLocationWhatEnum.EverywhereWithProperty:
                    {
                    For Each p As clsProperty In Adventure.htblLocationProperties.Values
                        .Items.Add(p.Key, p.CommonName);
                    Next;
            }

        }

    }


    private void LoadCombos()
    {

        arlDirectionRefs = GetReferences(ReferencesType.Direction, Me.sCommand)
        arlObjectRefs = GetReferences(ReferencesType.Object, Me.sCommand)
        arlCharacterRefs = GetReferences(ReferencesType.Character, Me.sCommand)
        arlLocationRefs = GetReferences(ReferencesType.Location, Me.sCommand)
        arlNumberRefs = GetReferences(ReferencesType.Number, Me.sCommand)
        arlTextRefs = GetReferences(ReferencesType.Text, Me.sCommand)
        arlItemRefs = GetReferences(ReferencesType.Item, Me.sCommand)
        cmbObjectPropertyValue.Command = Me.sCommand;

        With cmbObjectWhat;
            .Items.Clear();
            .Items.Add(clsAction.ItemEnum.MoveObject.ToString, "Move");
            .Items.Add(clsAction.ItemEnum.AddObjectToGroup.ToString, "Add");
            .Items.Add(clsAction.ItemEnum.RemoveObjectFromGroup.ToString, "Remove");
        }
        '       SetCombo(cmbObjectWhat, clsAction.ItemEnum.MoveObject.ToString)

        With cmbObjectMoveWhat;
            .Items.Clear();
            .Items.Add(clsAction.MoveObjectWhatEnum.Object.ToString, "Object");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingAtLocation.ToString, "Everything at location");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingHeldBy.ToString, "Everything held by");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingInGroup.ToString, "Everything in group");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingInside.ToString, "Everything inside object");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingOn.ToString, "Everything on object");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingWithProperty.ToString, "Everything with property");
            .Items.Add(clsAction.MoveObjectWhatEnum.EverythingWornBy.ToString, "Everything worn by");
        }
        '        SetCombo(cmbObjectMoveWhat, clsAction.MoveObjectWhatEnum.Object.ToString)

        cmbObjectKey1.Items.Clear();
        'cmbObject.Items.Add(ALLHELDOBJECTS, "[ All Held Objects ]")
        'cmbObject.Items.Add(ALLWORNOBJECTS, "[ All Worn Objects ]")
        'For Each o As String In arlObjectRefs
        '    cmbObjectKey1.Items.Add(o, Adventure.GetNameFromKey(o, False)) ' o.Replace("ReferencedObject", " Referenced Object ").Replace("Referenced Object s", "Referenced Objects"))
        'Next
        'For Each ob As clsObject In Adventure.htblObjects.Values
        '    cmbObjectKey1.Items.Add(ob.Key, ob.FullName)
        'Next

        With cmbCharacterWhat;
            .Items.Clear();
            .Items.Add(clsAction.ItemEnum.MoveCharacter.ToString, "Move");
            .Items.Add(clsAction.ItemEnum.AddCharacterToGroup.ToString, "Add");
            .Items.Add(clsAction.ItemEnum.RemoveCharacterFromGroup.ToString, "Remove");
        }

        With cmbCharacterMoveWho;
            .Items.Clear();
            .Items.Add(clsAction.MoveCharacterWhoEnum.Character.ToString, "Character");
            .Items.Add(clsAction.MoveCharacterWhoEnum.EveryoneAtLocation.ToString, "Everyone at location");
            .Items.Add(clsAction.MoveCharacterWhoEnum.EveryoneInGroup.ToString, "Everyone in group");
            .Items.Add(clsAction.MoveCharacterWhoEnum.EveryoneInside.ToString, "Everyone inside object");
            .Items.Add(clsAction.MoveCharacterWhoEnum.EveryoneOn.ToString, "Everyone on object");
            .Items.Add(clsAction.MoveCharacterWhoEnum.EveryoneWithProperty.ToString, "Everyone with property");
        }

        cmbCharacterKey1.Items.Clear();

        With cmbLocationWhat;
            .Items.Clear();
            .Items.Add(clsAction.ItemEnum.AddLocationToGroup.ToString, "Add");
            .Items.Add(clsAction.ItemEnum.RemoveLocationFromGroup.ToString, "Remove");
        }

        With cmbLocationMoveWhat;
            .Items.Clear();
            .Items.Add(clsAction.MoveLocationWhatEnum.Location.ToString, "Location");
            .Items.Add(clsAction.MoveLocationWhatEnum.LocationOf.ToString, "Location of");
            .Items.Add(clsAction.MoveLocationWhatEnum.EverywhereInGroup.ToString, "Everywhere in group");
            .Items.Add(clsAction.MoveLocationWhatEnum.EverywhereWithProperty.ToString, "Everywhere with property");
        }

        cmbLocationKey1.Items.Clear();

        With cmbWhatConv;
            .Items.Clear();
            .Items.Add(CInt(clsAction.ConversationEnum.Ask).ToString, "Ask");
            .Items.Add(CInt(clsAction.ConversationEnum.Tell).ToString, "Tell");
            .Items.Add(CInt(clsAction.ConversationEnum.Command).ToString, "Say");
            .Items.Add(CInt(clsAction.ConversationEnum.EnterConversation).ToString, "Enter conversation");
            .Items.Add(CInt(clsAction.ConversationEnum.LeaveConversation).ToString, "Leave conversation");
        }

        With cmbAskWho;
            .Items.Clear();
            .Items.Add(THEPLAYER, "[ The Player Character ]");
            For Each c As String In arlCharacterRefs
                .Items.Add(c, Adventure.GetNameFromKey(c, false));
            Next;
            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                .Items.Add(ch.Key, ch.Name);
            Next;
        }

        cmbPropertyValue.Command = Me.sCommand;
        'cmbProperty.Items.Clear()
        'For Each o As String In arlObjectRefs
        '    cmbProperty.Items.Add(o, Adventure.GetNameFromKey(o, False))
        'Next
        'For Each ob As clsObject In Adventure.htblObjects.Values
        '    cmbProperty.Items.Add(ob.Key, ob.FullName)
        'Next

        With cmbVariableSet;
            .Items.Clear();
            .Items.Add(clsAction.ItemEnum.SetVariable.ToString, "Set");
            .Items.Add(clsAction.ItemEnum.IncreaseVariable.ToString, "Increase");
            .Items.Add(clsAction.ItemEnum.DecreaseVariable.ToString, "Decrease");
        }

        cmbVariable.Items.Clear();
        cmbIndex.Items.Clear();
        cmbIndex.Items.Add("<Enter own value>");
        For Each v As String In arlNumberRefs
            cmbIndex.Items.Add(v, Adventure.GetNameFromKey(v, false));
        Next;
        For Each v As clsVariable In Adventure.htblVariables.Values
            cmbVariable.Items.Add(v.Key, v.Name);
            If v.Length < 2 && v.Type = clsVariable.VariableTypeEnum.Numeric Then cmbIndex.Items.Add(v.Key, v.Name)
            If v.Length > 1 Then cmbVariableArray.Items.Add(v.Key, v.Name)
        Next;
        'cmbLocation.Items.Clear()
        'For Each loc As clsLocation In Adventure.htblLocations.Values
        '    cmbLocation.Items.Add(loc.Key, loc.ShortDescription)
        'Next

        'cmbObject.Items.Clear()
        'For Each ob As clsObject In Adventure.htblObjects.Values
        '    cmbObject.Items.Add(ob.Key, ob.FullName)
        'Next

        cmbTasks.Items.Clear();
        For Each tas As clsTask In Adventure.htblTasks.Values
            cmbTasks.Items.Add(tas.Key, tas.Description);
        Next;

        With cmbEndGame.Items;
            .Clear();
            .Add(clsAction.EndGameEnum.Win.ToString, "in Victory");
            .Add(clsAction.EndGameEnum.Lose.ToString, "in Defeat");
            .Add(clsAction.EndGameEnum.Neutral.ToString, "without a fuss");
        }
        'cmbWhatC.Items.Clear()
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.InDirection.ToString, "in direction")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.InsideObject.ToString, "inside object")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.OntoCharacter.ToString, "onto character")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToLocation.ToString, "to location")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToLocationGroup.ToString, "to location group")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToLyingOn.ToString, "to lying on")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToParentLocation.ToString, "to parent location")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToSameLocationAs.ToString, "to same location as")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToSittingOn.ToString, "to sitting on")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToStandingOn.ToString, "to standing on")
        'cmbWhatC.Items.Add(clsAction.MoveCharacterEnum.ToSwitchWith.ToString, "to switch places with")

    }



    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        Me.DialogResult = Windows.Forms.DialogResult.Cancel;
    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        Me.DialogResult = Windows.Forms.DialogResult.OK;
    }


    private void cmbObjectMoveTo_ValueChanged(object sender, System.EventArgs e)
    {

        If cmbObjectMoveTo.SelectedItem == null Then Exit Sub

        With cmbObjectKey2;
            private string sCurrent = null;
            If .SelectedItem != null Then sCurrent = .SelectedItem.DataValue.ToString

            .Items.Clear();
            .SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;

            switch (EnumParseMoveObject(cmbObjectMoveTo.SelectedItem.DataValue.ToString))
            {
                case clsAction.MoveObjectToEnum.InsideObject:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        if (ob.IsContainer)
                        {
                            .Items.Add(ob.Key, ob.FullName);
                        }
                    Next;
                case clsAction.MoveObjectToEnum.OntoObject:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        if (ob.HasSurface)
                        {
                            .Items.Add(ob.Key, ob.FullName);
                        }
                    Next;
                case clsAction.MoveObjectToEnum.ToCarriedBy:
                case clsAction.MoveObjectToEnum.ToWornBy:
                case clsAction.MoveObjectToEnum.ToPartOfCharacter:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each chr As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(chr.Key, chr.Name);
                    Next;
                case clsAction.MoveObjectToEnum.ToSameLocationAs:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each chr As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(chr.Key, chr.Name);
                    Next;
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        .Items.Add(ob.Key, ob.FullName);
                    Next;
                case clsAction.MoveObjectToEnum.ToLocation:
                    {
                    .Items.Add("Hidden", "[ Hidden ]");
                    For Each l As String In arlLocationRefs
                        .Items.Add(l, Adventure.GetNameFromKey(l, false));
                    Next;
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(loc.Key, loc.ShortDescription.ToString);
                    Next;
                case clsAction.MoveObjectToEnum.ToLocationGroup:
                    {
                    For Each grp As clsGroup In Adventure.htblGroups.Values
                        If grp.GroupType = clsGroup.GroupTypeEnum.Locations Then .Items.Add(grp.Key, grp.Name)
                    Next;
                case clsAction.MoveObjectToEnum.ToPartOfObject:
                    {
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        .Items.Add(ob.Key, ob.FullName);
                    Next;
                case clsAction.MoveObjectToEnum.ToGroup:
                case clsAction.MoveObjectToEnum.FromGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        if (g.GroupType = clsGroup.GroupTypeEnum.Objects)
                        {
                            .Items.Add(g.Key, g.Name);
                        }
                    Next;
            }

            SetCombo(cmbObjectKey2, sCurrent);
        }
    }


    private void cmbCharacterMoveTo_ValueChanged(System.Object sender, System.EventArgs e)
    {

        If cmbCharacterMoveTo.SelectedItem == null Then Exit Sub

        With cmbCharacterKey2;
            private string sCurrent = null;
            If .SelectedItem != null Then sCurrent = .SelectedItem.DataValue.ToString

            .Items.Clear();
            .SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
            cmbCharacterKey2.Enabled = true;
            switch (EnumParseMoveCharacter(CStr(cmbCharacterMoveTo.SelectedItem.DataValue)))
            {
                case clsAction.MoveCharacterToEnum.InDirection:
                    {
                    .SortStyle = Infragistics.Win.ValueListSortStyle.None;
                    For Each d As String In arlDirectionRefs
                        .Items.Add(d.ToString, d);
                    Next;
                    for (DirectionsEnum di = DirectionsEnum.North; di <= DirectionsEnum.NorthWest; di++)
                    {
                        .Items.Add(di.ToString, DirectionName(di));
                    Next;

                case clsAction.MoveCharacterToEnum.ToLocation:
                    {
                    .Items.Add("Hidden", "[ Hidden ]");
                    For Each l As String In arlLocationRefs
                        .Items.Add(l, Adventure.GetNameFromKey(l, false));
                    Next;
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(loc.Key, loc.ShortDescription.ToString);
                    Next;

                case clsAction.MoveCharacterToEnum.ToLocationGroup:
                    {
                    For Each grp As clsGroup In Adventure.htblGroups.Values
                        if (grp.GroupType = clsGroup.GroupTypeEnum.Locations)
                        {
                            .Items.Add(grp.Key, grp.Name);
                        }
                    Next;

                case clsAction.MoveCharacterToEnum.ToSameLocationAs:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each chr As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(chr.Key, chr.Name);
                    Next;
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        .Items.Add(ob.Key, ob.FullName);
                    Next;

                case clsAction.MoveCharacterToEnum.ToSwitchWith:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each chr As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(chr.Key, chr.Name);
                    Next;

                case clsAction.MoveCharacterToEnum.ToStandingOn:
                    {
                    .Items.Add(THEFLOOR, "[ The Floor ]");
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.IsStandable Then .Items.Add(ob.Key, ob.FullName)
                    Next;

                case clsAction.MoveCharacterToEnum.ToSittingOn:
                    {
                    .Items.Add(THEFLOOR, "[ The Floor ]");
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.IsSittable Then .Items.Add(ob.Key, ob.FullName)
                    Next;

                case clsAction.MoveCharacterToEnum.ToLyingOn:
                    {
                    .Items.Add(THEFLOOR, "[ The Floor ]");
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.IsLieable Then .Items.Add(ob.Key, ob.FullName)
                    Next;

                case clsAction.MoveCharacterToEnum.InsideObject:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        If ob.IsContainer Then .Items.Add(ob.Key, ob.FullName)
                    Next;

                case clsAction.MoveCharacterToEnum.OntoCharacter:
                    {
                    ' TODO - don't allow character to move onto itself
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each chr As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(chr.Key, chr.Name);
                    Next;

                case clsAction.MoveCharacterToEnum.ToParentLocation:
                    {
                    cmbCharacterKey2.Enabled = false;

                case clsAction.MoveCharacterToEnum.ToGroup:
                case clsAction.MoveCharacterToEnum.FromGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        if (g.GroupType = clsGroup.GroupTypeEnum.Characters)
                        {
                            .Items.Add(g.Key, g.Name);
                        }
                    Next;

            }

            SetCombo(cmbCharacterKey2, sCurrent);
        }

    }


    private void cmbLocationMoveTo_ValueChanged(System.Object sender, System.EventArgs e)
    {

        If cmbLocationMoveTo.SelectedItem == null Then Exit Sub

        With cmbLocationKey2;
            private string sCurrent = null;
            If .SelectedItem != null Then sCurrent = .SelectedItem.DataValue.ToString

            .Items.Clear();
            .SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
            cmbLocationMoveTo.Enabled = true;
            switch (EnumParseMoveLocation(CStr(cmbLocationMoveTo.SelectedItem.DataValue)))
            {

                case clsAction.MoveLocationToEnum.FromGroup:
                case clsAction.MoveLocationToEnum.ToGroup:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        if (g.GroupType = clsGroup.GroupTypeEnum.Locations)
                        {
                            .Items.Add(g.Key, g.Name);
                        }
                    Next;

            }

            SetCombo(cmbLocationKey2, sCurrent);
        }

    }


    private void UltraTabControl1_SelectedTabChanged(System.Object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {

        RepositionDropdowns();

        frmAction_Resize(null, null);
        SaveAction();
    }

    private void cmbIndex_SelectionChanged(object sender, System.EventArgs e)
    {

        if (cmbIndex.SelectedIndex = 0)
        {
            cmbIndex.Visible = false;
            cmbIndexEdit.Visible = true;
            cmbIndexEdit.Text = "0";
            cmbIndexEdit.Focus();
        }

    }


    private void LoadCharMoveToCombo()
    {

        cmbCharacterPropertyValue.Visible = false;
        lblCharactersWithValue.Visible = false;

        if (cmbCharacterMoveWho.SelectedItem IsNot null)
        {
            switch (CType([Enum].Parse(GetType(clsAction.MoveCharacterWhoEnum), SafeString(cmbCharacterMoveWho.SelectedItem.DataValue)), clsAction.MoveCharacterWhoEnum))
            {
                case clsAction.MoveCharacterWhoEnum.EveryoneWithProperty:
                    {
                    if (cmbCharacterKey1.SelectedItem IsNot null)
                    {
                        private clsProperty prop = Adventure.htblCharacterProperties(cmbCharacterKey1.SelectedItem.DataValue.ToString);
                        if (prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly)
                        {
                            cmbCharacterPropertyValue.PropertyKey = cmbCharacterKey1.SelectedItem.DataValue.ToString;
                            cmbCharacterPropertyValue.Visible = true;
                            lblCharactersWithValue.Visible = true;
                        }
                    }
            }
        }

        frmAction_Resize(null, null);

        if (cmbCharacterWhat.SelectedItem IsNot null)
        {

            switch (CType([Enum].Parse(GetType(clsAction.ItemEnum), cmbCharacterWhat.SelectedItem.DataValue.ToString), clsAction.ItemEnum))
            {
                case clsAction.ItemEnum.MoveCharacter:
                    {
                    private string sCurrent = null;
                    If cmbCharacterMoveTo.SelectedItem != null Then sCurrent = cmbCharacterMoveTo.SelectedItem.DataValue.ToString

                    cmbCharacterMoveTo.Items.Clear();
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.InDirection.ToString, "in direction");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.InsideObject.ToString, "inside object");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.OntoCharacter.ToString, "onto character");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToLocation.ToString, "to location");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToLocationGroup.ToString, "to location group");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToLyingOn.ToString, "to lying on");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToParentLocation.ToString, "to parent location");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToSameLocationAs.ToString, "to same location as");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToSittingOn.ToString, "to sitting on");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToStandingOn.ToString, "to standing on");
                    cmbCharacterMoveTo.Items.Add(clsAction.MoveCharacterToEnum.ToSwitchWith.ToString, "to switch places with");
                    SetCombo(cmbCharacterMoveTo, sCurrent);

                case clsAction.ItemEnum.AddCharacterToGroup:
                    {
                    cmbCharacterMoveTo.Items.Clear();
                    cmbCharacterMoveTo.Items.Add("ToGroup", "to group");
                    SetCombo(cmbCharacterMoveTo, "ToGroup");

                case clsAction.ItemEnum.RemoveCharacterFromGroup:
                    {
                    cmbCharacterMoveTo.Items.Clear();
                    cmbCharacterMoveTo.Items.Add("FromGroup", "from group");
                    SetCombo(cmbCharacterMoveTo, "FromGroup");
            }
        }

    }


    private void LoadObjectMoveToCombo()
    {

        private bool bStatic = false;
        private bool bDynamic = false;

        cmbObjectPropertyValue.Visible = false;
        lblObjectsWithValue.Visible = false;

        if (cmbObjectMoveWhat.SelectedItem IsNot null)
        {
            switch (CType([Enum].Parse(GetType(clsAction.MoveObjectWhatEnum), SafeString(cmbObjectMoveWhat.SelectedItem.DataValue)), clsAction.MoveObjectWhatEnum))
            {
                case clsAction.MoveObjectWhatEnum.Object:
                    {
                    if (cmbObjectKey1.SelectedItem IsNot null && Adventure.htblObjects.ContainsKey(cmbObjectKey1.SelectedItem.DataValue.ToString))
                    {
                        bStatic = Adventure.htblObjects(cmbObjectKey1.SelectedItem.DataValue.ToString).IsStatic
                        bDynamic = Not bStatic
                    Else
                        bStatic = True
                        bDynamic = True
                    }
                case clsAction.MoveObjectWhatEnum.EverythingAtLocation:
                    {
                    bDynamic = True
                case clsAction.MoveObjectWhatEnum.EverythingHeldBy:
                    {
                    bDynamic = True
                case clsAction.MoveObjectWhatEnum.EverythingInGroup:
                    {
                    bDynamic = True
                    bStatic = True
                case clsAction.MoveObjectWhatEnum.EverythingInside:
                    {
                    bDynamic = True
                case clsAction.MoveObjectWhatEnum.EverythingOn:
                    {
                    bDynamic = True
                case clsAction.MoveObjectWhatEnum.EverythingWithProperty:
                    {
                    if (cmbObjectKey1.SelectedItem IsNot null)
                    {
                        private clsProperty prop = Adventure.htblObjectProperties(cmbObjectKey1.SelectedItem.DataValue.ToString);
                        if (prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly)
                        {
                            cmbObjectPropertyValue.PropertyKey = cmbObjectKey1.SelectedItem.DataValue.ToString;
                            cmbObjectPropertyValue.Visible = true;
                            lblObjectsWithValue.Visible = true;
                        }
                    }
                    bDynamic = True
                    bStatic = True
                case clsAction.MoveObjectWhatEnum.EverythingWornBy:
                    {
                    bDynamic = True
            }
        }

        frmAction_Resize(null, null);

        if (cmbObjectWhat.SelectedItem IsNot null)
        {
            switch (CType([Enum].Parse(GetType(clsAction.ItemEnum), cmbObjectWhat.SelectedItem.DataValue.ToString), clsAction.ItemEnum))
            {
                case clsAction.ItemEnum.MoveObject:
                    {
                    private string sCurrent = null;
                    If cmbObjectMoveTo.SelectedItem != null Then sCurrent = cmbObjectMoveTo.SelectedItem.DataValue.ToString
                    cmbObjectMoveTo.Items.Clear();
                    if (bStatic && bDynamic)
                    {
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.InsideObject.ToString, "inside object");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.OntoObject.ToString, "onto object");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToCarriedBy.ToString, "to held by");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocation.ToString, "to location");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocationGroup.ToString, "to somewhere/everywhere in group");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToSameLocationAs.ToString, "to same location as");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToWornBy.ToString, "to worn by");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToPartOfCharacter.ToString, "to part of character");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToPartOfObject.ToString, "to part of object");
                    ElseIf bStatic Then
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocation.ToString, "to location");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocationGroup.ToString, "to everywhere in group");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToPartOfCharacter.ToString, "to part of character");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToPartOfObject.ToString, "to part of object");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToSameLocationAs.ToString, "to same location as");
                    Else
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.InsideObject.ToString, "inside object");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.OntoObject.ToString, "onto object");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToCarriedBy.ToString, "to held by");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocation.ToString, "to location");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToLocationGroup.ToString, "to somewhere in group");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToSameLocationAs.ToString, "to same location as");
                        cmbObjectMoveTo.Items.Add(clsAction.MoveObjectToEnum.ToWornBy.ToString, "to worn by");
                    }
                    SetCombo(cmbObjectMoveTo, sCurrent);

                case clsAction.ItemEnum.AddObjectToGroup:
                    {
                    cmbObjectMoveTo.Items.Clear();
                    cmbObjectMoveTo.Items.Add("ToGroup", "to group");
                    SetCombo(cmbObjectMoveTo, "ToGroup");

                case clsAction.ItemEnum.RemoveObjectFromGroup:
                    {
                    cmbObjectMoveTo.Items.Clear();
                    cmbObjectMoveTo.Items.Add("FromGroup", "from group");
                    SetCombo(cmbObjectMoveTo, "FromGroup");

            }
        }

    }


    private void LoadLocationMoveToCombo()
    {

        cmbLocationPropertyValue.Visible = false;
        lblLocationsWithValue.Visible = false;

        if (cmbLocationMoveWhat.SelectedItem IsNot null)
        {
            switch (CType([Enum].Parse(GetType(clsAction.MoveLocationWhatEnum), SafeString(cmbLocationMoveWhat.SelectedItem.DataValue)), clsAction.MoveLocationWhatEnum))
            {
                case clsAction.MoveLocationWhatEnum.EverywhereWithProperty:
                    {
                    if (cmbLocationKey1.SelectedItem IsNot null)
                    {
                        private clsProperty prop = Adventure.htblLocationProperties(cmbLocationKey1.SelectedItem.DataValue.ToString);
                        if (prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly)
                        {
                            cmbLocationPropertyValue.PropertyKey = cmbLocationKey1.SelectedItem.DataValue.ToString;
                            cmbLocationPropertyValue.Visible = true;
                            lblLocationsWithValue.Visible = true;
                        }
                    }
            }
        }

        frmAction_Resize(null, null);

        if (cmbLocationWhat.SelectedItem IsNot null)
        {

            switch (CType([Enum].Parse(GetType(clsAction.ItemEnum), cmbLocationWhat.SelectedItem.DataValue.ToString), clsAction.ItemEnum))
            {

                case clsAction.ItemEnum.AddLocationToGroup:
                    {
                    cmbLocationMoveTo.Items.Clear();
                    cmbLocationMoveTo.Items.Add("ToGroup", "to group");
                    SetCombo(cmbLocationMoveTo, "ToGroup");

                case clsAction.ItemEnum.RemoveLocationFromGroup:
                    {
                    cmbLocationMoveTo.Items.Clear();
                    cmbLocationMoveTo.Items.Add("FromGroup", "from group");
                    SetCombo(cmbLocationMoveTo, "FromGroup");
            }
        }

    }


    private void cmbObjectKey1_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadObjectMoveToCombo();
    }


    private void cmbCharacterKey1_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadCharMoveToCombo();
    }


    private void cmbLocationKey1_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadLocationMoveToCombo();
    }


    private void cmb_ValueChanged(System.Object sender, System.EventArgs e)
    {
        SaveAction();
    }


    private void SaveAction()
    {

        If bLoadingAction Then Exit Sub

        btnOK.Enabled = false;

        With Action;
            switch (Me.tabsActions.SelectedTab.Text)
            {

                case "Move Objects":
                    {
                    If cmbObjectWhat.SelectedItem != null Then .eItem = (cmbObjectWhat.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.ItemEnum)), clsAction.ItemEnum)
                    If cmbObjectMoveWhat.SelectedItem != null Then .eMoveObjectWhat = (cmbObjectMoveWhat.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.MoveObjectWhatEnum)), clsAction.MoveObjectWhatEnum)
                    If cmbObjectKey1.SelectedItem != null Then .sKey1 = cmbObjectKey1.SelectedItem.DataValue.ToString
                    If cmbObjectPropertyValue != null Then .sPropertyValue = cmbObjectPropertyValue.Value
                    If cmbObjectMoveTo.SelectedItem != null Then .eMoveObjectTo = EnumParseMoveObject(cmbObjectMoveTo.SelectedItem.DataValue.ToString)
                    If cmbObjectKey2.SelectedItem != null Then .sKey2 = cmbObjectKey2.SelectedItem.DataValue.ToString

                    If cmbObjectWhat.SelectedItem != null && cmbObjectMoveWhat.SelectedItem != null _
                        && cmbObjectKey1.SelectedItem != null && (! cmbObjectPropertyValue.Visible || cmbObjectPropertyValue.Value != null) _;
                        && cmbObjectMoveTo.SelectedItem != null && cmbObjectKey2.SelectedItem != null Then;
                        btnOK.Enabled = true;
                    }

                case "Move Characters":
                    {
                    If cmbCharacterWhat.SelectedItem != null Then .eItem = (cmbCharacterWhat.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.ItemEnum)), clsAction.ItemEnum)
                    If cmbCharacterMoveWho.SelectedItem != null Then .eMoveCharacterWho = (cmbCharacterMoveWho.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.MoveCharacterWhoEnum)), clsAction.MoveCharacterWhoEnum)
                    If ! cmbCharacterKey1.SelectedItem == null Then .sKey1 = CStr(cmbCharacterKey1.SelectedItem.DataValue)
                    If cmbCharacterPropertyValue != null Then .sPropertyValue = cmbCharacterPropertyValue.Value
                    If ! cmbCharacterMoveTo.SelectedItem == null Then .eMoveCharacterTo = EnumParseMoveCharacter(cmbCharacterMoveTo.SelectedItem.DataValue.ToString)
                    If ! cmbCharacterKey2.SelectedItem == null Then .sKey2 = CStr(cmbCharacterKey2.SelectedItem.DataValue)

                    If cmbCharacterWhat.SelectedItem != null && cmbCharacterMoveWho.SelectedItem != null _
                       && cmbCharacterKey1.SelectedItem != null && (! cmbCharacterPropertyValue.Visible || cmbCharacterPropertyValue.Value != null) _;
                       && cmbCharacterMoveTo.SelectedItem != null && (cmbCharacterKey2.SelectedItem != null || cmbCharacterMoveTo.SelectedItem.DataValue.ToString = clsAction.MoveCharacterToEnum.ToParentLocation.ToString) Then;
                        btnOK.Enabled = true;
                    }

                    'If Not (cmbCharacterKey1.SelectedItem Is Nothing OrElse cmbCharacterKey2.SelectedItem Is Nothing OrElse (cmbCharacterKey2.SelectedIndex <> 6 AndAlso cmbCharacterMoveTo.SelectedItem Is Nothing)) Then
                    '    btnOK.Enabled = True
                    'End If

                case "Locations":
                    {
                    If cmbLocationWhat.SelectedItem != null Then .eItem = (cmbLocationWhat.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.ItemEnum)), clsAction.ItemEnum)
                    If cmbLocationMoveWhat.SelectedItem != null Then .eMoveLocationWhat = (cmbLocationMoveWhat.SelectedItem.DataValue.ToString)([Enum].Parse(GetType(clsAction.MoveLocationWhatEnum)), clsAction.MoveLocationWhatEnum)
                    If ! cmbLocationKey1.SelectedItem == null Then .sKey1 = CStr(cmbLocationKey1.SelectedItem.DataValue)
                    If cmbLocationPropertyValue != null Then .sPropertyValue = cmbLocationPropertyValue.Value
                    If ! cmbLocationMoveTo.SelectedItem == null Then .eMoveLocationTo = EnumParseMoveLocation(cmbLocationMoveTo.SelectedItem.DataValue.ToString)
                    If ! cmbLocationKey2.SelectedItem == null Then .sKey2 = CStr(cmbLocationKey2.SelectedItem.DataValue)

                    If cmbLocationWhat.SelectedItem != null && cmbLocationMoveWhat.SelectedItem != null _
                       && cmbLocationKey1.SelectedItem != null _;
                       && cmbLocationMoveTo.SelectedItem != null && cmbLocationKey2.SelectedItem != null Then;
                        btnOK.Enabled = true;
                    }

                case "Set Properties":
                    {
                    .eItem = clsAction.ItemEnum.SetProperties;
                    'If Not cmbProperty.SelectedItem Is Nothing Then .sKey1 = CStr(cmbProperty.SelectedItem.DataValue)
                    If isPropertyKey1.Key <> "" Then .sKey1 = isPropertyKey1.Key
                    If ! cmbPropertyKey2.SelectedItem == null Then .sKey2 = CStr(cmbPropertyKey2.SelectedItem.DataValue)
                    'If Not cmbPropertyExtra.SelectedItem Is Nothing Then .StringValue = CStr(cmbPropertyExtra.SelectedItem.DataValue)
                    If cmbPropertyValue.Value != null Then .sPropertyValue = cmbPropertyValue.Value

                    if (isPropertyKey1.Key <> "" && cmbPropertyKey2.SelectedItem IsNot null && cmbPropertyValue.Value IsNot null)
                    {
                        btnOK.Enabled = true;
                    }
                    If cmbPropertyValue.ExpressionOrVariable.Visible && cmbPropertyValue.ExpressionOrVariable.ListType = ItemSelector.ItemEnum.ValueList && cmbPropertyValue.ExpressionOrVariable.cmbList.SelectedIndex = 0 Then btnOK.Enabled = false ' No Value Selected

                case "Variables":
                    {
                    if (cmbVariableSet.SelectedItem Is null)
                    {
                        .eItem = clsAction.ItemEnum.SetVariable;
                    Else
                        switch (cmbVariableSet.SelectedItem.DataValue.ToString)
                        {
                            case clsAction.ItemEnum.SetVariable.ToString:
                                {
                                .eItem = clsAction.ItemEnum.SetVariable;
                            case clsAction.ItemEnum.IncreaseVariable.ToString:
                                {
                                .eItem = clsAction.ItemEnum.IncreaseVariable;
                            case clsAction.ItemEnum.DecreaseVariable.ToString:
                                {
                                .eItem = clsAction.ItemEnum.DecreaseVariable;
                        }
                    }

                    private bool bIndexRequired = false;
                    if (chkLoop.Checked)
                    {
                        .IntValue = CInt(Val(txtLoopFrom.Text));
                        .sKey2 = CInt(Val(txtLoopTo.Text)).ToString;
                        If cmbVariableArray.SelectedItem != null Then .sKey1 = CStr(cmbVariableArray.SelectedItem.DataValue)
                        .eVariables = clsAction.VariablesEnum.Loop;
                    Else
                        .IntValue = -1;
                        If ! cmbIndex.SelectedItem == null && cmbIndex.Visible Then .sKey2 = CStr(cmbIndex.SelectedItem.DataValue)
                        If cmbIndexEdit.Visible Then .sKey2 = CInt(Math.Min(Val(cmbIndexEdit.Text), Integer.MaxValue)).ToString
                        if (Not cmbVariable.SelectedItem Is null)
                        {
                            .sKey1 = CStr(cmbVariable.SelectedItem.DataValue);
                            If Adventure.htblVariables(.sKey1).Length > 1 Then bIndexRequired = true
                        }
                        .eVariables = clsAction.VariablesEnum.Assignment;
                    }
                    .StringValue = Expression.Value;

                    If ((chkLoop.Checked && ! cmbVariableArray.SelectedItem == null) _
                        || (! chkLoop.Checked && ! cmbVariable.SelectedItem == null && (! bIndexRequired || (cmbIndex.Visible && ! cmbIndex.SelectedItem == null) || (cmbIndexEdit.Visible && IsNumeric(cmbIndexEdit.Text))))) _;
                        && .StringValue <> "" Then;
                        btnOK.Enabled = true;
                    }

                case "Tasks":
                    {
                    ' TODO: if unset, should only show tasks that are not repeatable

                    If ! cmbWhatT.SelectedItem == null && cmbWhatT.SelectedItem.DataValue.ToString = "Execute" && ! cmbTasks.SelectedItem == null && Adventure.htblTasks.ContainsKey(cmbTasks.SelectedItem.DataValue.ToString) _
                        && Adventure.htblTasks(cmbTasks.SelectedItem.DataValue.ToString).TaskType = clsTask.TaskTypeEnum.General && Adventure.htblTasks(cmbTasks.SelectedItem.DataValue.ToString).HasReferences Then;
                        btnParams.Visible = true;
                    Else
                        btnParams.Visible = false;
                    }
                    RepositionDropdowns();

                    .eItem = clsAction.ItemEnum.SetTasks;
                    If ! cmbWhatT.SelectedItem == null Then .eSetTasks = EnumParseSetTask(CStr(cmbWhatT.SelectedItem.DataValue))
                    If ! cmbTasks.SelectedItem == null Then .sKey1 = cmbTasks.SelectedItem.DataValue.ToString
                    If ! btnParams.Tag == null && btnParams.Visible Then .StringValue = btnParams.Tag.ToString Else .StringValue = ""
                    if (chkLoop.Checked)
                    {
                        .IntValue = CInt(Val(txtLoopFrom.Text));
                        .sPropertyValue = CInt(Val(txtLoopTo.Text)).ToString;
                    Else
                        .IntValue = 0;
                        .sPropertyValue = "";
                    }

                    if (Not (cmbWhatT.SelectedItem Is null || cmbTasks.SelectedItem Is null || (btnParams.Visible && CStr(btnParams.Tag) = "")))
                    {
                        btnOK.Enabled = true;
                    }

                case "Conversation":
                    {
                    .eItem = clsAction.ItemEnum.Conversation;
                    If cmbWhatConv.SelectedItem != null Then .eConversation = (clsAction.ConversationEnum)(cmbWhatConv.SelectedItem.DataValue)
                    If cmbAskWho.SelectedItem != null Then .sKey1 = cmbAskWho.SelectedItem.DataValue.ToString
                    If txtConvTopic.Visible Then .StringValue = txtConvTopic.Text

                    if (cmbWhatConv.SelectedItem IsNot null && cmbAskWho.SelectedItem IsNot null && (.eConversation = clsAction.ConversationEnum.EnterConversation || .eConversation = clsAction.ConversationEnum.LeaveConversation || txtConvTopic.Text <> ""))
                    {
                        btnOK.Enabled = true;
                    }

                case "Time":
                    {
                    .eItem = clsAction.ItemEnum.Time;
                    .StringValue = expTurns.Value;
                    btnOK.Enabled = expTurns.Value <> "";

                case "End Game":
                    {
                    .eItem = clsAction.ItemEnum.EndGame;
                    if (cmbEndGame.SelectedItem IsNot null)
                    {
                        .eEndgame = EnumParseEndGame(cmbEndGame.SelectedItem.DataValue.ToString);
                        btnOK.Enabled = true;
                    }

            }
        }

    }

    private void frmAction_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        SaveFormPosition(Me);
    }

    private void frmAction_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
        'cmbVariable_SelectionChanged(Nothing, Nothing)
        if (DarkInterface())
        {
            chkLoop.ForeColor = Color.White;
        }
    }

    private void frmAction_Resize(object sender, System.EventArgs e)
    {

        private int iWidth;

        'cmbObjectKey1.Width = cmbObjectMoveTo.Left - cmbObjectKey1.Left + 1
        'cmbObjectKey2.Left = cmbObjectMoveTo.Left + cmbObjectMoveTo.Width - 1
        'cmbObjectKey2.Width = Width - cmbObjectKey2.Left - 22 'CInt(Width / 2 - 130)
        if (lblObjectsWithValue.Visible)
        {
            iWidth = Me.ClientSize.Width - cmbObjectWhat.Width - cmbObjectMoveWhat.Width - lblObjectsWithValue.Width - 30
            cmbObjectKey1.Width = CInt(iWidth / 4);
            lblObjectsWithValue.Left = cmbObjectKey1.Left + cmbObjectKey1.Width;
            cmbObjectPropertyValue.Width = CInt(iWidth / 4);
            cmbObjectPropertyValue.Left = lblObjectsWithValue.Left + lblObjectsWithValue.Width;
            cmbObjectMoveTo.Width = CInt(iWidth / 4);
            cmbObjectMoveTo.Left = cmbObjectPropertyValue.Left + cmbObjectPropertyValue.Width;
            cmbObjectKey2.Left = cmbObjectMoveTo.Left + cmbObjectMoveTo.Width - 1;
            cmbObjectKey2.Width = Me.ClientSize.Width - cmbObjectKey2.Left - 10 ' CInt(iWidth / 4);
        Else
            iWidth = Me.ClientSize.Width - cmbObjectWhat.Width - cmbObjectMoveWhat.Width - 30
            cmbObjectKey1.Width = CInt(iWidth / 3);
            cmbObjectMoveTo.Width = CInt(iWidth / 3);
            cmbObjectMoveTo.Left = cmbObjectKey1.Left + cmbObjectKey1.Width - 1;
            cmbObjectKey2.Left = cmbObjectMoveTo.Left + cmbObjectMoveTo.Width - 1;
            cmbObjectKey2.Width = Me.ClientSize.Width - cmbObjectKey2.Left - 10 ' CInt(iWidth / 4);
        }
        cmbObjectKey1.DropDownListWidth = Math.Max(cmbObjectKey1.Width, 170);
        cmbObjectMoveTo.DropDownListWidth = Math.Max(cmbObjectMoveTo.Width, 130);
        cmbObjectKey2.DropDownListWidth = Math.Max(cmbObjectKey2.Width, 170);

        if (lblCharactersWithValue.Visible)
        {
            iWidth = Me.ClientSize.Width - cmbCharacterWhat.Width - cmbCharacterMoveWho.Width - lblCharactersWithValue.Width - 30
            cmbCharacterKey1.Width = CInt(iWidth / 4);
            lblCharactersWithValue.Left = cmbCharacterKey1.Left + cmbCharacterKey1.Width;
            cmbCharacterPropertyValue.Width = CInt(iWidth / 4);
            cmbCharacterPropertyValue.Left = lblCharactersWithValue.Left + lblCharactersWithValue.Width;
            cmbCharacterMoveTo.Width = CInt(iWidth / 4);
            cmbCharacterMoveTo.Left = cmbCharacterPropertyValue.Left + cmbCharacterPropertyValue.Width;
            cmbCharacterKey2.Left = cmbCharacterMoveTo.Left + cmbCharacterMoveTo.Width - 1;
            cmbCharacterKey2.Width = Me.ClientSize.Width - cmbCharacterKey2.Left - 10 ' CInt(iWidth / 4);
        Else
            iWidth = Me.ClientSize.Width - cmbCharacterWhat.Width - cmbCharacterMoveWho.Width - 30
            cmbCharacterKey1.Width = CInt(iWidth / 3);
            cmbCharacterMoveTo.Width = CInt(iWidth / 3);
            cmbCharacterMoveTo.Left = cmbCharacterKey1.Left + cmbCharacterKey1.Width - 1;
            cmbCharacterKey2.Left = cmbCharacterMoveTo.Left + cmbCharacterMoveTo.Width - 1;
            cmbCharacterKey2.Width = Me.ClientSize.Width - cmbCharacterKey2.Left - 10 ' CInt(iWidth / 4);
        }
        cmbCharacterKey1.DropDownListWidth = Math.Max(cmbCharacterKey1.Width, 170);
        cmbCharacterMoveTo.DropDownListWidth = Math.Max(cmbCharacterMoveTo.Width, 130);
        cmbCharacterKey2.DropDownListWidth = Math.Max(cmbCharacterKey2.Width, 170);

        if (lblLocationsWithValue.Visible)
        {
            iWidth = Me.ClientSize.Width - cmbLocationWhat.Width - cmbLocationMoveWhat.Width - lblLocationsWithValue.Width - 30
            cmbLocationKey1.Width = CInt(iWidth / 4);
            lblLocationsWithValue.Left = cmbLocationKey1.Left + cmbLocationKey1.Width;
            cmbLocationPropertyValue.Width = CInt(iWidth / 4);
            cmbLocationPropertyValue.Left = lblLocationsWithValue.Left + lblLocationsWithValue.Width;
            cmbLocationMoveTo.Width = CInt(iWidth / 4);
            cmbLocationMoveTo.Left = cmbLocationPropertyValue.Left + cmbLocationPropertyValue.Width;
            cmbLocationKey2.Left = cmbLocationMoveTo.Left + cmbLocationMoveTo.Width - 1;
            cmbLocationKey2.Width = Me.ClientSize.Width - cmbLocationKey2.Left - 10 ' CInt(iWidth / 4);
        Else
            iWidth = Me.ClientSize.Width - cmbLocationWhat.Width - cmbLocationMoveWhat.Width - 30
            cmbLocationKey1.Width = CInt(iWidth / 3);
            cmbLocationMoveTo.Width = CInt(iWidth / 3);
            cmbLocationMoveTo.Left = cmbLocationKey1.Left + cmbLocationKey1.Width - 1;
            cmbLocationKey2.Left = cmbLocationMoveTo.Left + cmbLocationMoveTo.Width - 1;
            cmbLocationKey2.Width = Me.ClientSize.Width - cmbLocationKey2.Left - 10 ' CInt(iWidth / 4);
        }
        cmbLocationKey1.DropDownListWidth = Math.Max(cmbLocationKey1.Width, 170);
        cmbLocationMoveTo.DropDownListWidth = Math.Max(cmbLocationMoveTo.Width, 130);
        cmbLocationKey2.DropDownListWidth = Math.Max(cmbLocationKey2.Width, 170);

        'cmbCharacterKey1.Width = cmbCharacterKey2.Left - cmbCharacterKey1.Left
        'cmbCharacterMoveTo.Left = cmbCharacterKey2.Left + cmbCharacterKey2.Width
        'cmbCharacterMoveTo.Width = Width - cmbCharacterMoveTo.Left - 20 ' CInt(Width / 2 - 100)

        iWidth = Me.ClientSize.Width - lblTo.Width - 30
        isPropertyKey1.Width = CInt(iWidth / 3) '- 40;
        cmbPropertyKey2.Left = isPropertyKey1.Left + isPropertyKey1.Width;
        cmbPropertyKey2.Width = CInt(iWidth / 3) '+ 9;
        lblTo.Left = cmbPropertyKey2.Left + cmbPropertyKey2.Width + 3;
        cmbPropertyValue.Left = cmbPropertyKey2.Left + cmbPropertyKey2.Width + 20;
        cmbPropertyValue.Width = Me.ClientSize.Width - cmbPropertyValue.Left - 10;

        if (cmbVariable.SelectedItem IsNot null)
        {
            'Dim v As clsVariable = Nothing
            'If cmbVariable.SelectedItem IsNot Nothing Then v = Adventure.htblVariables(cmbVariable.SelectedItem.DataValue.ToString)
            'If v IsNot Nothing AndAlso v.Length > 1 Then
            '    Expression.Width = Width - Expression.Left - 20 '44
            'Else
            '    Expression.Width = Width - Expression.Left - 25 '49
            'End If
            Expression.Width = Me.ClientSize.Width - Expression.Left - 10;
        Else
            'Expression.Width = Width - Expression.Left - 25 '49
            Expression.Width = Me.ClientSize.Width - Expression.Left - 10;
        }

    }

    private void isPropertyKey1_SelectionChanged(object sender, System.EventArgs e)
    {

        cmbPropertyKey2.Items.Clear();

        switch (isPropertyKey1.ListType)
        {
            case ItemSelector.ItemEnum.Object:
                {
                if (CStr(isPropertyKey1.Key).StartsWith("ReferencedObject"))
                {
                    For Each prop As clsProperty In Adventure.htblObjectProperties.Values
                        'If prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly AndAlso Not (prop.Type = clsProperty.PropertyTypeEnum.StateList AndAlso prop.AppendToProperty <> "") Then
                        ' Need to be able to check/uncheck SelectionOnly properties, e.g. set Purchased to Selected
                        if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                        {
                            cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                        }
                    Next;
                Else
                    if (Adventure.htblObjects.ContainsKey(isPropertyKey1.Key))
                    {
                        private clsObject ob = Adventure.htblObjects(isPropertyKey1.Key);

                        if (ob IsNot null)
                        {
                            For Each prop As clsProperty In Adventure.htblObjectProperties.Values
                                if (ob.htblProperties.ContainsKey(prop.Key) || prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly || prop.GroupOnly)
                                {
                                    if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                                    {
                                        cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                                    }
                                }
                            Next;
                        }
                    }
                }

            case ItemSelector.ItemEnum.Character:
                {
                If isPropertyKey1.Key <> "" Then cmbPropertyKey2.Items.Add(CHARACTERPROPERNAME, "Name") ' A fake property, for access to name
                if (CStr(isPropertyKey1.Key).StartsWith("ReferencedCharacter"))
                {
                    For Each prop As clsProperty In Adventure.htblCharacterProperties.Values
                        if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                        {
                            cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                        }
                    Next;
                Else
                    private clsCharacter ch = null;
                    If Adventure.htblCharacters.ContainsKey(isPropertyKey1.Key) Then ch = Adventure.htblCharacters(isPropertyKey1.Key)
                    If isPropertyKey1.Key = "%Player%" Then ch = Adventure.Player

                    if (ch IsNot null)
                    {
                        For Each prop As clsProperty In ch.htblProperties.Values
                            if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                            {
                                cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                            }
                        Next;
                        ' Add any selection properties that the character doesn't currently have, so they can be added during the game
                        For Each prop As clsProperty In Adventure.htblCharacterProperties.Values
                            if (Not ch.htblProperties.ContainsKey(prop.Key) && prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly || prop.GroupOnly)
                            {
                                cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                            }
                        Next;
                    }

                }

            case ItemSelector.ItemEnum.Location:
                {
                'If isProperty.Key <> "" Then cmbWhatP.Items.Add(CHARACTERPROPERNAME, "Name") ' A fake property, for access to name
                if (CStr(isPropertyKey1.Key).StartsWith("ReferencedLocation"))
                {
                    For Each prop As clsProperty In Adventure.htblLocationProperties.Values
                        if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                        {
                            cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                        }
                    Next;
                Else
                    if (Adventure.htblLocations.ContainsKey(isPropertyKey1.Key))
                    {
                        private clsLocation loc = Adventure.htblLocations(isPropertyKey1.Key);

                        if (loc IsNot null)
                        {
                            For Each prop As clsProperty In loc.htblProperties.Values
                                if (Not (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> ""))
                                {
                                    cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                                }
                            Next;
                            ' Add any selection properties that the character doesn't currently have, so they can be added during the game
                            For Each prop As clsProperty In Adventure.htblLocationProperties.Values
                                if (Not loc.htblProperties.ContainsKey(prop.Key) && prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly || prop.GroupOnly)
                                {
                                    cmbPropertyKey2.Items.Add(prop.Key, prop.Description);
                                }
                            Next;
                        }
                    }
                }

        }

    }

    private void cmbPropertyKey2_SelectionChanged(object sender, System.EventArgs e)
    {

        private string sPropKey = CStr(cmbPropertyKey2.SelectedItem.DataValue);
        cmbPropertyValue.PropertyKey = sPropKey;

    }


    private void chkLoop_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        RepositionDropdowns();
    }


    private void cmbVariableSet_ValueChanged(object sender, EventArgs e)
    {

        If cmbVariableSet.SelectedItem == null Then Exit Sub

        private string sPre = "";
        private clsVariable v = null;
        If cmbVariable.SelectedItem != null Then v = Adventure.htblVariables(cmbVariable.SelectedItem.DataValue.ToString)
        If v != null && v.Length > 1 Then sPre = "]  "

        'bAllowPaint = False
        switch (cmbVariableSet.SelectedItem.DataValue.ToString)
        {
            case clsAction.ItemEnum.SetVariable.ToString:
                {
                lblRight.Text = sPre + "=";
                lblLoop.Text = "[Loop]   =";
            case clsAction.ItemEnum.IncreaseVariable.ToString:
            case clsAction.ItemEnum.DecreaseVariable.ToString:
                {
                lblRight.Text = sPre + " by ";
                lblLoop.Text = "[Loop]   by ";
        }
        'Expression.Visible = False
        'Expression.Left = lblRight.Left + lblRight.Width
        'Expression.Width = Me.ClientSize.Width - Expression.Left - 10
        'Expression.Visible = True
        RepositionDropdowns();

    }


    ' Clever function to suspend painting the form whilst we play around with it, to stop horrible flickers
    private bool bAllowPaint = true;
    protected override void WndProc(ref m As Message)
    {
        if (bAllowPaint || m.Msg <> &HF)
        {
            MyBase.WndProc(m);
        }
    }


    private void RepositionDropdowns()
    {

        private clsVariable v = null;
        If cmbVariable.SelectedItem != null Then v = Adventure.htblVariables(cmbVariable.SelectedItem.DataValue.ToString)

        Expression.Visible = false;
        if (Me.chkLoop.Checked)
        {
            MaximumSize = New Size(1600, 243)
            MinimumSize = New Size(544, 243)
            Height = 243
            lblFor.Visible = true;
            lblTo2.Visible = true;
            txtLoopFrom.Visible = true;
            txtLoopTo.Visible = true;
            lblLoop.Visible = true;
            lblNext.Visible = true;
            cmbVariableArray.Visible = true;
            cmbVariable.Visible = false;
            Expression.Left = lblLoop.Left + lblLoop.Width;
            Expression.Top = 59;
            cmbVariableArray.Location = New Point(135, 59);
            cmbVariableSet.Location = New Point(54, 59);
            cmbWhatT.Location = New Point(54, 59);
            cmbTasks.Location = New Point(168, 59);
            btnParams.Location = New Point(Me.Width - 100, 59);
            cmbTasks.Width = btnParams.Left - cmbWhatT.Location.X - cmbWhatT.Width - 5 + CInt(If(btnParams.Visible, 0, 80));
            cmbIndex.Visible = false;
            cmbIndexEdit.Visible = false;
            lblLeft.Visible = false;
            lblRight.Visible = false;
            If Me.tabsActions.SelectedTab.Text = "Variables" && cmbVariableArray.SelectedItem != null Then SetCombo(cmbVariableArray, cmbVariable.SelectedItem.DataValue.ToString)
        Else
            MinimumSize = New Size(544, 148)
            MaximumSize = New Size(1600, 148)
            Height = 148
            lblFor.Visible = false;
            lblTo2.Visible = false;
            txtLoopFrom.Visible = false;
            txtLoopTo.Visible = false;
            lblLoop.Visible = false;
            lblNext.Visible = false;
            cmbVariableSet.Location = New Point(11, 9);
            cmbWhatT.Location = New Point(11, 9);
            cmbTasks.Location = New Point(125, 9);
            btnParams.Location = New Point(Me.Width - 100, 9);
            cmbTasks.Width = btnParams.Left - cmbWhatT.Location.X - cmbWhatT.Width - 5 + CInt(If(btnParams.Visible, 0, 80));
            if (cmbVariableArray.Visible)
            {
                cmbVariableArray.Visible = false;
                cmbVariable.Visible = true;
                If Me.tabsActions.SelectedTab.Text = "Variables" && cmbVariableArray.SelectedItem != null Then SetCombo(cmbVariable, cmbVariableArray.SelectedItem.DataValue.ToString)
            }
            lblRight.Visible = true;
            cmbVariable_SelectionChanged(null, null);
            private string sPost = "=";
            If cmbVariableSet.SelectedItem != null && cmbVariableSet.SelectedItem.DataValue.ToString <> "SetVariable" Then sPost = " by "
            if (v IsNot null && v.Length > 1)
            {
                cmbIndex.Visible = true;
                lblLeft.Visible = true;
                cmbVariable.Size = New Size(126, 21);
                lblLeft.Left = cmbVariable.Left + cmbVariable.Width;
                cmbIndex.Left = lblLeft.Left + lblLeft.Width;
                cmbIndexEdit.Location = cmbIndex.Location;
                cmbIndexEdit.Size = cmbIndex.Size;
                lblRight.Text = "]  " + sPost;
                lblRight.Left = cmbIndex.Left + cmbIndex.Width;
            Else
                chkLoop.Checked = false;
                UltraStatusBar1.Panels(0).Visible = false ' Show loop checkbox;
                cmbIndex.Visible = false;
                cmbIndexEdit.Visible = false;
                lblLeft.Visible = false;
                cmbVariable.Size = New Size(200, 21);
                lblRight.Text = sPost;
                lblRight.Left = cmbVariable.Left + cmbVariable.Width;
            }
            Expression.Left = lblRight.Left + lblRight.Width;
            Expression.Top = 9;
        }
        Expression.Width = Me.ClientSize.Width - Expression.Left - 10;
        Expression.Visible = true;

        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl page = null;

        if (tabsActions.SelectedTab IsNot null)
        {
            switch (tabsActions.SelectedTab.Text)
            {
                case "Variables":
                    {
                    page = Me.UltraTabPageControl4
                    If cmbVariable.SelectedIndex > -1 && Adventure.htblVariables(cmbVariable.SelectedItem.DataValue.ToString).Length > 1 Then UltraStatusBar1.Panels(0).Visible = true

                case "Tasks":
                    {
                    page = Me.UltraTabPageControl6
                    If chkLoop.Checked && cmbVariableArray.SelectedItem != null Then SetCombo(cmbVariableArray, cmbVariable.SelectedItem.DataValue.ToString)
                    UltraStatusBar1.Panels(0).Visible = true;

                default:
                    {
                    UltraStatusBar1.Panels(0).Visible = false;

            }
        }

        if (page IsNot null)
        {
            lblFor.Parent = page;
            lblTo2.Parent = page;
            txtLoopFrom.Parent = page;
            txtLoopTo.Parent = page;
            lblNext.Parent = page;
        }

    }


    private void cmbVariable_SelectionChanged(object sender, System.EventArgs e)
    {

        private clsVariable v = null;
        If cmbVariable.SelectedItem != null Then v = Adventure.htblVariables(cmbVariable.SelectedItem.DataValue.ToString)

        private string sPost = "=";
        If cmbVariableSet.SelectedItem != null && cmbVariableSet.SelectedItem.DataValue.ToString <> "SetVariable" Then sPost = " by "

        If sender != null Then RepositionDropdowns()

        'If v IsNot Nothing AndAlso v.Length > 1 Then
        '    'chkLoop.Visible = True
        '    UltraStatusBar1.Panels(0).Visible = True ' Show loop checkbox
        '    If Not chkLoop.Checked Then
        '        cmbIndex.Visible = True
        '        lblLeft.Visible = True
        '        cmbVariable.Size = New Size(126, 21)
        '        lblLeft.Left = cmbVariable.Left + cmbVariable.Width
        '        cmbIndex.Left = lblLeft.Left + lblLeft.Width
        '        cmbIndexEdit.Location = cmbIndex.Location
        '        cmbIndexEdit.Size = cmbIndex.Size
        '        lblRight.Text = "]  " & sPost
        '        lblRight.Left = cmbIndex.Left + cmbIndex.Width
        '        Expression.Visible = False
        '        Expression.Left = lblRight.Left + lblRight.Width
        '        'Expression.Top = 50
        '        'Expression.Width = Width - Expression.Left - 20 '44
        '        'Expression.Width = UltraTabPageControl1.Width - Expression.Left - 10
        '        Expression.Width = Me.ClientSize.Width - Expression.Left - 10
        '        Expression.Visible = True
        '    End If
        'Else
        '    chkLoop.Checked = False
        '    UltraStatusBar1.Panels(0).Visible = False ' Show loop checkbox
        '    cmbIndex.Visible = False
        '    cmbIndexEdit.Visible = False
        '    lblLeft.Visible = False
        '    cmbVariable.Size = New Size(200, 21)
        '    lblRight.Text = sPost
        '    lblRight.Left = cmbVariable.Left + cmbVariable.Width
        '    Expression.Visible = False
        '    Expression.Left = lblRight.Left + lblRight.Width
        '    'Expression.Width = Width - Expression.Left - 25 '49
        '    'Expression.Width = UltraTabPageControl1.Width - Expression.Left - 10
        '    Expression.Width = Me.ClientSize.Width - Expression.Left - 10
        '    Expression.Visible = True
        'End If

        if (v IsNot null && v.Type = clsVariable.VariableTypeEnum.Text)
        {
            SetCombo(cmbVariableSet, clsAction.ItemEnum.SetVariable.ToString);
            cmbVariableSet.Enabled = false;
            cmbVariableSet.Appearance.ForeColorDisabled = Color.Black;
        Else
            cmbVariableSet.Enabled = true;
        }

        'RepositionDropdowns()

    }


    private void Expression_ValueChanged(object sender, System.EventArgs e)
    {
        SaveAction();
    }

    private void cmbIndexEdit_SelectionChanged(object sender, System.EventArgs e)
    {

        if (cmbIndexEdit.SelectedIndex = 0)
        {
            cmbIndexEdit.Visible = false;
            cmbIndex.Visible = true;
            cmbIndex.SelectedIndex = 1;
        }

    }


    private void cmbTasks_ValueChanged(System.Object sender, System.EventArgs e)
    {

        If cmbTasks.SelectedItem == null Then Exit Sub

        private clsTask tasSelected = Adventure.htblTasks(cmbTasks.SelectedItem.DataValue.ToString);
        private string sParams = "";

        For Each sRef As String In tasSelected.References
            sParams &= sRef + "|";
        Next;
        If sParams.Length > 0 Then sParams = sLeft(sParams, sParams.Length - 1)

        btnParams.Tag = sParams;

    }


    private void btnParams_Click(System.Object sender, System.EventArgs e)
    {

        'TODO: Replace this with a nice form where you can click on the links again

        private int iRef = 0;
        private string sNewRef;
        private clsTask tasSelected = Adventure.htblTasks(cmbTasks.SelectedItem.DataValue.ToString);
        private string sNewParams = "";

        For Each sRef As String In Split(btnParams.Tag.ToString, "|")
            iRef += 1;
            sNewRef = InputBox("Please enter parameter for reference " & iRef & "." & vbCrLf & vbCrLf & """" & tasSelected.MakeNice & """" & vbCrLf & vbCrLf & "This must either be an object/character key, direction, or a function that resolves to one, e.g. %ParentOf[%object3%]%", "Enter Parameter " & iRef, sRef)
            If sNewRef = "" Then Exit Sub
            sNewParams &= sNewRef + "|";
        Next;
        If sNewParams.Length > 0 Then sNewParams = sLeft(sNewParams, sNewParams.Length - 1)

        btnParams.Tag = sNewParams;
        SaveAction();

    }


    private void isProperty_FilledList(object sender, System.EventArgs e)
    {

        switch (isPropertyKey1.ListType)
        {
            case ItemSelector.ItemEnum.Object:
                {
                arlObjectRefs = GetReferences(ReferencesType.Object, Me.sCommand)

                For Each o As String In arlObjectRefs
                    isPropertyKey1.cmbList.Items.Add(o, Adventure.GetNameFromKey(o, false));
                Next;

            case ItemSelector.ItemEnum.Character:
                {
                arlCharacterRefs = GetReferences(ReferencesType.Character, Me.sCommand)

                For Each c As String In arlCharacterRefs
                    isPropertyKey1.cmbList.Items.Add(c, Adventure.GetNameFromKey(c, false));
                Next;

            case ItemSelector.ItemEnum.Location:
                {
                arlLocationRefs = GetReferences(ReferencesType.Location, Me.sCommand)

                For Each l As String In arlLocationRefs
                    isPropertyKey1.cmbList.Items.Add(l, Adventure.GetNameFromKey(l, false));
                Next;

        }

    }


    private void frmAction_Shown(object sender, System.EventArgs e)
    {
        frmAction_Resize(null, null);
    }


    private void cmbCharacterWhat_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadCharMoveToCombo();
    }

    private void cmbLocationWhat_SelectionChanged(object sender, System.EventArgs e)
    {
        LoadLocationMoveToCombo();
    }

    private void cmbWhatConv_ValueChanged(System.Object sender, System.EventArgs e)
    {

        private clsAction.ConversationEnum eConversationEnum;
        if (cmbWhatConv.SelectedItem Is null)
        {
            eConversationEnum = clsAction.ConversationEnum.Ask
        Else
            eConversationEnum = CType(cmbWhatConv.SelectedItem.DataValue, clsAction.ConversationEnum)
        }

        switch (eConversationEnum)
        {
            case clsAction.ConversationEnum.Ask:
            case clsAction.ConversationEnum.Tell:
                {
                cmbAskWho.Left = cmbWhatConv.Left + cmbWhatConv.Width - 1;
                lblConvAbout.Left = cmbAskWho.Left + cmbAskWho.Width + 5;
                lblConvAbout.Text = "about";
                txtConvTopic.Left = lblConvAbout.Left + lblConvAbout.Width - 7;
                txtConvTopic.Width = Me.Width - txtConvTopic.Left - 30;
                txtConvTopic.Visible = true;
                lblConvAbout.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                cmbAskWho.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            case clsAction.ConversationEnum.Command:
                {
                txtConvTopic.Left = cmbWhatConv.Left + cmbWhatConv.Width - 1;
                txtConvTopic.Width = Me.Width - txtConvTopic.Left - 205;
                lblConvAbout.Text = "to";
                lblConvAbout.Left = txtConvTopic.Left + txtConvTopic.Width + 5;
                cmbAskWho.Left = lblConvAbout.Left + lblConvAbout.Width - 22;
                lblConvAbout.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                cmbAskWho.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                txtConvTopic.Visible = true;

            case clsAction.ConversationEnum.EnterConversation:
            case clsAction.ConversationEnum.LeaveConversation:
                {
                txtConvTopic.Visible = false;
                lblConvAbout.Left = cmbWhatConv.Left + cmbWhatConv.Width + 5;
                lblConvAbout.Text = "with";
                cmbAskWho.Left = lblConvAbout.Left + lblConvAbout.Width - 15;
                lblConvAbout.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                cmbAskWho.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            case clsAction.ConversationEnum.Farewell:
            case clsAction.ConversationEnum.Greet:
                {
                ' Not currently in use
        }
    }


    private void expTurns_ValueChanged(object sender, System.EventArgs e)
    {
        SaveAction();
    }

}

}