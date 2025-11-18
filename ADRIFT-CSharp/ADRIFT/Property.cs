using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class GenericProperty
{
    Inherits System.Windows.Forms.UserControl;

#Region " Windows Form Designer generated code "

    internal void New(clsProperty prop, ref fOwner As Properties, PropertyHashTable htblCurrentProperties, bool bGroup)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        Me.fOwner = fOwner;
        LoadProperty(prop, htblCurrentProperties, bGroup);

    }

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Timer1 As System.Windows.Forms.Timer;
    Friend WithEvents cmbStates As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents chkSelected As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents GenTextbox1 As ADRIFT.GenTextbox;
    Friend WithEvents optSet As Infragistics.Win.UltraWinEditors.UltraOptionSet;
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip;
    Friend WithEvents txtNumber As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components);
        Me.cmbStates = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.txtNumber = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.optSet = New Infragistics.Win.UltraWinEditors.UltraOptionSet();
        Me.GenTextbox1 = New ADRIFT.GenTextbox();
        Me.chkSelected = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components);
        (System.ComponentModel.ISupportInitialize)(Me.cmbStates).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtNumber).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.optSet).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkSelected).BeginInit();
        Me.SuspendLayout();
        '
        'Timer1
        '
        '
        'cmbStates
        '
        Me.cmbStates.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbStates.Enabled = false;
        Me.cmbStates.Location = New System.Drawing.Point(216, 5);
        Me.cmbStates.Name = "cmbStates";
        Me.cmbStates.Size = New System.Drawing.Size(208, 21);
        Me.cmbStates.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbStates.TabIndex = 5;
        Me.cmbStates.Visible = false;
        '
        'txtNumber
        '
        Appearance1.TextHAlignAsString = "Right";
        Me.txtNumber.Appearance = Appearance1;
        Me.txtNumber.Enabled = false;
        Me.txtNumber.Location = New System.Drawing.Point(216, 5);
        Me.txtNumber.Name = "txtNumber";
        Me.txtNumber.Size = New System.Drawing.Size(48, 21);
        Me.txtNumber.TabIndex = 6;
        Me.txtNumber.Visible = false;
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.optSet);
        Me.UltraGroupBox1.Controls.Add(Me.GenTextbox1);
        Me.UltraGroupBox1.Controls.Add(Me.chkSelected);
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(440, 160);
        Me.UltraGroupBox1.TabIndex = 7;
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'optSet
        '
        Appearance2.BackColorDisabled = System.Drawing.Color.Transparent;
        Appearance2.TextHAlignAsString = "Center";
        Me.optSet.Appearance = Appearance2;
        Me.optSet.BackColor = System.Drawing.Color.Transparent;
        Me.optSet.BackColorInternal = System.Drawing.Color.Transparent;
        Me.optSet.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
        Me.optSet.Enabled = false;
        Appearance3.TextHAlignAsString = "Center";
        Me.optSet.ItemAppearance = Appearance3;
        Me.optSet.ItemOrigin = New System.Drawing.Point(30, 0);
        ValueListItem1.DataValue = "Default Item";
        ValueListItem1.DisplayText = "Static";
        ValueListItem2.DataValue = "ValueListItem1";
        ValueListItem2.DisplayText = "Dynamic";
        Me.optSet.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2});
        Me.optSet.ItemSpacingHorizontal = 10;
        Me.optSet.Location = New System.Drawing.Point(216, 8);
        Me.optSet.Name = "optSet";
        Me.optSet.Size = New System.Drawing.Size(312, 16);
        Me.optSet.TabIndex = 4;
        Me.optSet.Visible = false;
        '
        'GenTextbox1
        '
        Me.GenTextbox1.AllowAlternateDescriptions = true;
        Me.GenTextbox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.GenTextbox1.BackColor = System.Drawing.Color.Transparent;
        Me.GenTextbox1.Enabled = false;
        Me.GenTextbox1.FirstTabHasRestrictions = false;
        Me.GenTextbox1.Location = New System.Drawing.Point(8, 29);
        Me.GenTextbox1.Name = "GenTextbox1";
        Me.GenTextbox1.sCommand = null;
        Me.GenTextbox1.Size = New System.Drawing.Size(424, 123);
        Me.GenTextbox1.TabIndex = 1;
        '
        'chkSelected
        '
        Me.chkSelected.BackColor = System.Drawing.Color.Transparent;
        Me.chkSelected.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkSelected.Location = New System.Drawing.Point(8, 3);
        Me.chkSelected.Name = "chkSelected";
        Me.chkSelected.Size = New System.Drawing.Size(192, 26);
        Me.chkSelected.TabIndex = 0;
        Me.chkSelected.Text = "UltraCheckEditor1";
        '
        'GenericProperty
        '
        Me.Controls.Add(Me.txtNumber);
        Me.Controls.Add(Me.cmbStates);
        Me.Controls.Add(Me.UltraGroupBox1);
        Me.Name = "GenericProperty";
        Me.Size = New System.Drawing.Size(440, 160);
        (System.ComponentModel.ISupportInitialize)(Me.cmbStates).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtNumber).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.optSet).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkSelected).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }

#End Region

    public Properties fOwner;
    Private [Property] As clsProperty
    private bool bLoading;

    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)


    private void LoadProperty(ref prop As clsProperty, PropertyHashTable htblCurrentProperties, bool bGroup)
    {

        bLoading = True

        Me.[Property] = prop;

        Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right

        With prop;
            chkSelected.Checked = .Selected;

            if ((.Mandatory || .FromGroup) && Not bGroup)
            {
                chkSelected.Checked = true;
                chkSelected.Enabled = false;
            }
            if (Not (.Selected || .Mandatory))
            {
                optSet.Enabled = false;
            }

            chkSelected.Text = .Description;
            Height = 30
            cmbStates.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;

            switch (.Type)
            {

                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    cmbStates.Items.Clear();
                    cmbStates.SortStyle = Infragistics.Win.ValueListSortStyle.None;
                    For Each sState As String In .PossibleValues(htblCurrentProperties)
                        cmbStates.Items.Add(sState, sState);
                    Next;
                    if (.PossibleValues(htblCurrentProperties).Count = 2)
                    {
                        optSet.Items(0).DisplayText = .PossibleValues.Item(0);
                        optSet.Items(1).DisplayText = .PossibleValues.Item(1);

                        if (.Value = .PossibleValues.Item(1))
                        {
                            optSet.CheckedItem = optSet.Items(1);
                        Else
                            optSet.CheckedItem = optSet.Items(0);
                        }
                        optSet.Visible = true;
                    Else
                        SetCombo(cmbStates, .Value);
                        If cmbStates.SelectedIndex = -1 Then cmbStates.SelectedIndex = 0
                        cmbStates.Visible = true;
                    }

                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    cmbStates.Items.Clear();
                    cmbStates.SortStyle = Infragistics.Win.ValueListSortStyle.None;
                    For Each sLabel As String In .ValueList.Keys
                        cmbStates.Items.Add(.ValueList(sLabel), sLabel);
                    Next;
                    if (.ValueList.Count = 2)
                    {
                        private int i = 0;
                        For Each sLabel As String In .ValueList.Keys
                            optSet.Items(i).DisplayText = sLabel;
                            optSet.Items(i).DataValue = .ValueList(sLabel);
                            i += 1;
                        Next;

                        if (.Value = SafeString(optSet.Items(1).DataValue))
                        {
                            optSet.CheckedItem = optSet.Items(1);
                        Else
                            optSet.CheckedItem = optSet.Items(0);
                        }
                        optSet.Visible = true;
                    Else
                        SetCombo(cmbStates, .Value);
                        If cmbStates.SelectedIndex = -1 Then cmbStates.SelectedIndex = 0
                        cmbStates.Visible = true;
                    }

                case clsProperty.PropertyTypeEnum.Text:
                    {
                    GenTextbox1.Description = .StringData.Copy ' New Description(.Value);
                    If .GroupOnly Then GenTextbox1.FirstTabHasRestrictions = true
                    GenTextbox1.Visible = true;
                    Height = 160

                case clsProperty.PropertyTypeEnum.ObjectKey:
                    {
                    cmbStates.Items.Clear();
                    'For Each ob As clsObject In Adventure.Objects("StaticOrDynamic", "Dynamic").Values
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        if (Me.fOwner.OwnerKey <> ob.Key && (prop.RestrictProperty Is null || ob.htblProperties.ContainsKey(prop.RestrictProperty)))
                        {
                            if (prop.RestrictValue Is null || ob.htblProperties(prop.RestrictProperty).Value = prop.RestrictValue)
                            {
                                cmbStates.Items.Add(ob.Key, ob.FullName);
                            }
                        }
                    Next;
                    SetCombo(cmbStates, .Value);
                    cmbStates.Visible = true;

                case clsProperty.PropertyTypeEnum.Integer:
                    {

                    if (.RestrictProperty <> "" && Adventure.htblAllProperties.ContainsKey(.RestrictProperty))
                    {
                        private clsProperty prop2 = Adventure.htblAllProperties(.RestrictProperty);
                        private New List<int> lValues;

                        cmbStates.Items.Clear();
                        cmbStates.SortStyle = Infragistics.Win.ValueListSortStyle.None;

                        For Each sLabel As String In prop2.ValueList.Keys
                            cmbStates.Items.Add(prop2.ValueList(sLabel), sLabel);
                            lValues.Add(prop2.ValueList(sLabel));
                        Next;

                        ' Find the highest number <= our value
                        lValues.Sort(Function(i1 As Integer, i2 As Integer);
                                         return i2.CompareTo(i1);
                                     End Function)

                        ' Then see if that's a multiple of the value
                        For Each iValue As Integer In lValues
                            if (iValue <= SafeInt(.Value))
                            {
                                if (SafeInt(.Value) / iValue % 1 > -0.01 && SafeInt(.Value) / iValue % 1 < 0.01)
                                {
                                    SetCombo(cmbStates, iValue);
                                    txtNumber.Text = SafeInt(SafeInt(.Value) / iValue).ToString;
                                    Exit For;
                                }
                            }
                        Next;
                        if (txtNumber.Text = "")
                        {
                            txtNumber.Text = .Value;
                        }

                        cmbStates.Visible = true;
                    Else
                        txtNumber.Text = .Value;
                    }
                    txtNumber.Visible = true;

                case clsProperty.PropertyTypeEnum.CharacterKey:
                    {
                    cmbStates.Items.Clear();
                    ' Don't allow to pick same key from list, otherwise you can put player on himself etc
                    If Me.fOwner.OwnerKey <> Adventure.Player.Key Then cmbStates.Items.Add(THEPLAYER, "[ The Player Character ]")
                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                        if (Me.fOwner.OwnerKey <> ch.Key && (prop.RestrictProperty Is null || ch.htblProperties.ContainsKey(prop.RestrictProperty)))
                        {
                            if (prop.RestrictValue Is null || ch.htblProperties(prop.RestrictProperty).Value = prop.RestrictValue)
                            {
                                cmbStates.Items.Add(ch.Key, ch.ProperName);
                            }
                        }
                    Next;
                    SetCombo(cmbStates, .Value);
                    cmbStates.Visible = true;

                case clsProperty.PropertyTypeEnum.LocationKey:
                    {
                    cmbStates.Items.Clear();
                    For Each l As clsLocation In Adventure.htblLocations.Values
                        cmbStates.Items.Add(l.Key, l.ShortDescription.ToString);
                    Next;
                    SetCombo(cmbStates, .Value);
                    cmbStates.Visible = true;

                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    {
                    cmbStates.Items.Clear();
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        if (g.GroupType = clsGroup.GroupTypeEnum.Locations)
                        {
                            cmbStates.Items.Add(g.Key, g.Name);
                        }
                    Next;
                    SetCombo(cmbStates, .Value);
                    cmbStates.Visible = true;

            }

            If .PopupDescription <> "" Then SetTooltip(Me, .PopupDescription)

        }

        Visible = True

        bLoading = False

    }


    private void SetTooltip(Control ctrl, string sDescription)
    {

        For Each ctrlSub As Control In ctrl.Controls
            SetTooltip(ctrlSub, sDescription);
        Next;
        ToolTip1.SetToolTip(ctrl, sDescription);

    }


    private void chkSelected_CheckedChanged(System.Object sender, System.EventArgs e)
    {

        switch (chkSelected.Checked)
        {
            case true:
                {
                Me.optSet.Enabled = true;
                Me.GenTextbox1.Enabled = true;
                Me.txtNumber.Enabled = true;
                Me.cmbStates.Enabled = true;
            case false:
                {
                Me.optSet.Enabled = false;
                Me.GenTextbox1.Enabled = false;
                Me.txtNumber.Enabled = false;
                Me.cmbStates.Enabled = false;
        }

        [Property].Selected = chkSelected.Checked;
        If ! bLoading Then fOwner.RefreshProperties()
        RaiseEvent Changed(Me, e);

    }

    private void GenericProperty_Load(object sender, System.EventArgs e)
    {
        SetControlStyle(Me);
    }

    private void GenericProperty_Resize(object sender, System.EventArgs e)
    {
        Me.chkSelected.Width = CInt(Me.Width / 2);
        Me.optSet.Left = CInt(Me.Width / 2) - 10;
        Me.optSet.Width = CInt(Me.Width / 2);
        Me.optSet.ItemOrigin = New Point(CInt(Me.Width / 12), 0);
        Me.txtNumber.Left = CInt(Me.Width / 2);
        if (Me.Property IsNot null && Me.Property.Type = clsProperty.PropertyTypeEnum.Integer && Me.Property.RestrictProperty <> "" && Adventure.htblAllProperties.ContainsKey(Me.Property.RestrictProperty))
        {
            Me.cmbStates.Left = txtNumber.Left + txtNumber.Width + 10;
            Me.cmbStates.Width = CInt(Me.Width / 2) - 20 - txtNumber.Width;
        Else
            Me.cmbStates.Left = CInt(Me.Width / 2);
            Me.cmbStates.Width = CInt(Me.Width / 2) - 10;
        }

    }

    private void optSet_ValueChanged(System.Object sender, System.EventArgs e)
    {
        [Property].Value = optSet.Text;
        Timer1.Interval = 1;
        If ! bLoading Then Timer1.Start()
        RaiseEvent Changed(Me, e);
    }

    private void Timer1_Tick(object sender, System.EventArgs e)
    {
        Timer1.Stop();
        fOwner.RefreshProperties();
    }

    private void txtNumber_ValueChanged(System.Object sender, System.EventArgs e)
    {

        If bLoading Then Exit Sub

        if (cmbStates.Visible && cmbStates.SelectedItem IsNot null)
        {
            [Property].Value = (Val(txtNumber.Text) * Val(cmbStates.SelectedItem.DataValue)).ToString;
        Else
            [Property].Value = Val(txtNumber.Text).ToString;
        }
        RaiseEvent Changed(Me, e);
    }


    private void txtNumber_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {

        if ((Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) And Asc(e.KeyChar) <> 8 And (e.KeyChar <> "-"c || txtNumber.Text.Contains("-")))
        {
            e.Handled = true;
        }

    }

    private void GenTextbox1_txtSourceChanged(object sender, System.EventArgs e)
    {
        if (Not [Property] Is null)
        {
            '[Property].Value = GenTextbox1.Description.ToString
            [Property].StringData = GenTextbox1.Description;
            RaiseEvent Changed(Me, e);
        }
    }

    private void cmbStates_SelectionChanged(object sender, System.EventArgs e)
    {

        If bLoading Then Exit Sub

        if ([Property].Type = clsProperty.PropertyTypeEnum.ValueList)
        {
            [Property].Value = CStr(cmbStates.SelectedItem.DisplayText);
        ElseIf [Property].Type = clsProperty.PropertyTypeEnum.Integer Then
            [Property].Value = (Val(txtNumber.Text) * Val(cmbStates.SelectedItem.DataValue)).ToString;
        Else
            [Property].Value = CStr(cmbStates.SelectedItem.DataValue);
        }

        Timer1.Interval = 1;
        If ! bLoading Then Timer1.Start()
        RaiseEvent Changed(Me, e);
    }

}

}