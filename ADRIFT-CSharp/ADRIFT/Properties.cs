using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Properties
{
    Inherits System.Windows.Forms.UserControl;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        Me.BackColor = Color.Transparent;

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
    Friend WithEvents pnlContainer As System.Windows.Forms.Panel;
    Friend WithEvents btnAddProperty As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents pnlLines As System.Windows.Forms.Panel;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance;
        Me.pnlContainer = New System.Windows.Forms.Panel;
        Me.pnlLines = New System.Windows.Forms.Panel;
        Me.btnAddProperty = New Infragistics.Win.Misc.UltraButton;
        Me.pnlContainer.SuspendLayout();
        Me.SuspendLayout();
        '
        'pnlContainer
        '
        Me.pnlContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlContainer.AutoScroll = true;
        Me.pnlContainer.BackColor = System.Drawing.SystemColors.Control;
        Me.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlContainer.Controls.Add(Me.pnlLines);
        Me.pnlContainer.Location = New System.Drawing.Point(0, 0);
        Me.pnlContainer.Name = "pnlContainer";
        Me.pnlContainer.Size = New System.Drawing.Size(312, 312);
        Me.pnlContainer.TabIndex = 0;
        '
        'pnlLines
        '
        Me.pnlLines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlLines.BackColor = System.Drawing.SystemColors.Control;
        Me.pnlLines.Location = New System.Drawing.Point(0, 0);
        Me.pnlLines.Name = "pnlLines";
        Me.pnlLines.Size = New System.Drawing.Size(310, 56);
        Me.pnlLines.TabIndex = 0;
        '
        'btnAddProperty
        '
        Me.btnAddProperty.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Appearance1.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.btnAddProperty.Appearance = Appearance1;
        Me.btnAddProperty.Location = New System.Drawing.Point(9, 315);
        Me.btnAddProperty.Name = "btnAddProperty";
        Me.btnAddProperty.Size = New System.Drawing.Size(132, 25);
        Me.btnAddProperty.TabIndex = 1;
        Me.btnAddProperty.Text = "Add New Property";
        '
        'Properties
        '
        Me.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.Controls.Add(Me.btnAddProperty);
        Me.Controls.Add(Me.pnlContainer);
        Me.Name = "Properties";
        Me.Size = New System.Drawing.Size(312, 344);
        Me.pnlContainer.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private bool bLoading;
    private New ArrayList arlPropertyForms;
    private int iPropHeight;
    internal PropertyHashTable htblProperties;
    public String ' E.g. key of the object this property list is for OwnerKey;
    private New List<string> arlProperties;
    public bool bGroup = false;
    private List<string> lstPrivateKeys;

    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    private clsProperty.PropertyOfEnum ePropertyType = CType(-1, clsProperty.PropertyOfEnum) ' Uninitialised;
    internal clsProperty.PropertyOfEnum PropertyType { get; set; }
        {
            get
            {
            return ePropertyType;
        }
set(ByVal clsProperty.PropertyOfEnum)
            if (value <> ePropertyType)
            {
                ePropertyType = value
                For Each propGUI As GenericProperty In arlPropertyForms
                    propGUI.Dispose();
                    'RemoveProperty(propGUI)
                Next;
                'For Each prop As clsProperty In htblProperties.Values
                '    RemoveProperty(prop)
                'Next
                arlPropertyForms.Clear();
                LoadProperties();
            }
        }
    }

    private void Properties_Disposed(object sender, System.EventArgs e)
    {
        For Each prop As clsProperty In htblProperties.Values
            RemoveProperty(prop);
        Next;
    }



    private void Properties_Load(object sender, System.EventArgs e)
    {
        'RefreshProperties()
    }


    private void PropertyChanged(object sender, System.EventArgs e)
    {
        RaiseEvent Changed(Me, e);
    }

    private void AddProperty(clsProperty prop, int iOffset)
    {

        With prop;

            '' Don't show the hidden properties, i.e. ones that we have proper forms for
            'Select Case .Key
            '    Case LONGLOCATIONDESCRIPTION, SHORTLOCATIONDESCRIPTION, CHARACTERPROPERNAME
            '        Exit Sub
            'End Select

            if (prop.Type = clsProperty.PropertyTypeEnum.StateList && .AppendToProperty <> "")
            {
                if (PropOnShow(.AppendToProperty))
                {
                    private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbStateList = null;
                    private Infragistics.Win.UltraWinEditors.UltraOptionSet optStateList = null;
                    For Each propGUI As GenericProperty In arlPropertyForms
                        if (SafeString(propGUI.Tag) = .AppendToProperty)
                        {
                            cmbStateList = propGUI.cmbStates
                            optStateList = propGUI.optSet
                            Exit For;
                        }
                    Next;
                    if (cmbStateList IsNot null)
                    {
                        private clsProperty propOther = htblProperties(.AppendToProperty);
                        For Each sState As String In prop.arlStates
                            If ! ComboContainsKey(cmbStateList, sState) Then cmbStateList.Items.Add(sState)
                        Next;
                        if (cmbStateList.Items.Count = 2)
                        {
                            optStateList.Items(0).DisplayText = cmbStateList.Items(0).DisplayText;
                            optStateList.Items(1).DisplayText = cmbStateList.Items(1).DisplayText;
                            cmbStateList.Visible = false;
                            if (propOther.Value = optStateList.Items(1).DisplayText)
                            {
                                optStateList.CheckedItem = optStateList.Items(1);
                            Else
                                optStateList.CheckedItem = optStateList.Items(0);
                            }
                            optStateList.BringToFront();
                            optStateList.Visible = true;
                        Else
                            optStateList.Visible = false;
                            SetCombo(cmbStateList, propOther.Value);
                            cmbStateList.BringToFront();
                            cmbStateList.Visible = true;
                        }
                    }
                }
            Else
                if (PropOnShow(.Key))
                {
                    'DebugTimeRecord("Add Property " & prop.Description)
                    private New GenericProperty(prop, Me, htblProperties, bGroup) fProperty;
                    'DebugTimeFinish("Add Property " & prop.Description)
                    fProperty.Parent = Me.pnlLines;
                    fProperty.Width = pnlLines.Width;
                    fProperty.Tag = .Key;
                    fProperty.Top = iOffset 'iPropHeight;
                    iPropHeight += fProperty.Height;
                    pnlLines.Height = iPropHeight;
                    fProperty.chkSelected.Left = .Indent + 8;

                    For Each p As GenericProperty In arlPropertyForms
                        if (p.Top >= iOffset)
                        {
                            p.Top += fProperty.Height;
                        }
                    Next;
                    arlPropertyForms.Add(fProperty);

                    AddHandler fProperty.Changed, AddressOf PropertyChanged;
                    Debug.WriteLine("AddHandler " + .Key);

                    if (prop.FromGroup)
                    {
                        fProperty.Enabled = false;
                    }
                Else
                    prop.Selected = false;
                }
            }
        }

    }


    private void RemoveProperty(string sKey)
    {
        private clsProperty prop = htblProperties(sKey);
        RemoveProperty(prop);
    }
    private void RemoveProperty(clsProperty prop)
    {

        If prop == null Then Exit Sub

        prop.Selected = false;
        if (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.AppendToProperty <> "")
        {
            ' Remove the states
            if (PropOnShow(prop.AppendToProperty))
            {
                private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbStateList = null;
                private Infragistics.Win.UltraWinEditors.UltraOptionSet optStateList = null;
                For Each propGUI As GenericProperty In arlPropertyForms
                    if (SafeString(propGUI.Tag) = prop.AppendToProperty)
                    {
                        cmbStateList = propGUI.cmbStates
                        optStateList = propGUI.optSet
                        Exit For;
                    }
                Next;
                if (cmbStateList IsNot null)
                {
                    private clsProperty propOther = htblProperties(prop.AppendToProperty);
                    For Each sState As String In prop.arlStates
                        For Each vli As Infragistics.Win.ValueListItem In cmbStateList.Items
                            if (vli.DisplayText = sState)
                            {
                                cmbStateList.Items.Remove(vli);
                                Exit For;
                            }
                        Next;
                    Next;
                    if (cmbStateList.Items.Count = 2)
                    {
                        optStateList.Items(0).DisplayText = cmbStateList.Items(0).DisplayText;
                        optStateList.Items(1).DisplayText = cmbStateList.Items(1).DisplayText;
                        cmbStateList.Visible = false;
                        if (propOther.Value = optStateList.Items(1).DisplayText)
                        {
                            optStateList.CheckedItem = optStateList.Items(1);
                        Else
                            optStateList.CheckedItem = optStateList.Items(0);
                        }
                        optStateList.BringToFront();
                        optStateList.Visible = true;
                    Else
                        optStateList.Visible = false;
                        SetCombo(cmbStateList, propOther.Value);
                        cmbStateList.BringToFront();
                        cmbStateList.Visible = true;
                    }
                }
            }
        Else
            For Each propGUI As GenericProperty In arlPropertyForms
                if (propGUI.Tag.ToString = prop.Key)
                {
                    iPropHeight -= propGUI.Height;
                    propGUI.Visible = false;

                    For Each p As GenericProperty In arlPropertyForms
                        if (p.Top > propGUI.Top)
                        {
                            p.Top -= propGUI.Height;
                        }
                    Next;
                    arlPropertyForms.Remove(propGUI);

                    RemoveHandler propGUI.Changed, AddressOf PropertyChanged;
                    Debug.WriteLine("RemoveHandler " + prop.Key);

                    propGUI.Dispose();
                    Exit Sub;
                }
            Next;
        }

    }


    private void GetProperty(clsProperty prop)
    {

        With prop;

            if (PropOnShow(.Key))
            {
                arlProperties.Add(.Key);
                GetProperties(.Key, prop.Selected, prop.Indent + 12);
            }

        }

    }


    private void GetProperties(string sParentKey, bool bSelected, int iIndent = 0)
    {

        if (bSelected)
        {

            ' Do all children that are Mandatory first
            For Each prop As clsProperty In htblProperties.Values
                With prop;
                    if ((.Mandatory || .FromGroup) && .DependentKey = sParentKey && (.PrivateTo = "" || .PrivateTo = OwnerKey || lstPrivateKeys.Contains(.Key)))
                    {
                        if (.DependentValue Is null || (htblProperties.ContainsKey(sParentKey) && .DependentValue = htblProperties(sParentKey).Value))
                        {
                            If ! bGroup Then prop.Selected = true
                            prop.Indent = iIndent;
                            GetProperty(prop);
                        }
                    }
                }
            Next;

            ' Then add all other children
            For Each prop As clsProperty In htblProperties.Values
                With prop;
                    if (.DependentKey = sParentKey && Not (.Mandatory || .FromGroup) && (.PrivateTo = "" || .PrivateTo = OwnerKey || lstPrivateKeys.Contains(.Key)))
                    {
                        if (.DependentValue Is null || (htblProperties.ContainsKey(sParentKey) && .DependentValue = htblProperties(sParentKey).Value))
                        {
                            prop.Indent = iIndent;
                            GetProperty(prop);
                        }
                    }
                }
            Next;

        Else

            ' Add any properties that depend on this one being there but NOT selected
            For Each prop As clsProperty In htblProperties.Values
                With prop;
                    if (.DependentKey = sParentKey && .DependentValue = "false" && (.PrivateTo = "" || .PrivateTo = OwnerKey || lstPrivateKeys.Contains(.Key)))
                    {
                        prop.Indent = iIndent;
                        GetProperty(prop);
                    }
                }
            Next;

        }

    }


    private void LoadProperties()
    {

        If bLoading Then Exit Sub
        bLoading = True

        private New ArrayList arlLastProperties;
        if (arlPropertyForms.Count > 0)
        {
            For Each sKey As String In arlProperties
                arlLastProperties.Add(sKey);
            Next;
        }
        'arlLastProperties = CType(arlProperties.Clon, ArrayList)
        arlProperties.Clear();

        ' Work out a list of all properties private to groups that this item is a member of
        lstPrivateKeys = New List(Of String)
        For Each grp As clsGroup In Adventure.htblGroups.Values
            if (grp.arlMembers.Contains(OwnerKey))
            {
                For Each prop As clsProperty In Adventure.htblAllProperties.Values
                    If prop.PrivateTo = grp.Key Then lstPrivateKeys.Add(prop.Key)
                Next;
            }
        Next;

        GetProperties(null, true);

        For Each sKey As String In arlLastProperties
            if (Not arlProperties.Contains(sKey))
            {
                ' Remove item
                Debug.WriteLine("Need to remove property " + sKey);
                RemoveProperty(sKey);
            }
        Next;
        private int iOffset = 0;
        private int iCount = 0;
        For Each sKey As String In arlProperties
            private clsProperty prop = htblProperties(sKey);
            if (Not prop.GroupOnly || TypeOf Me.ParentForm Is frmGroup)
            {
                if (Not arlLastProperties.Contains(sKey))
                {
                    ' Add item
                    Debug.WriteLine("Need to add property " + sKey + " at index " + iCount);
                    AddProperty(prop, iOffset);
                }
                iCount += 1;
                if (sKey IsNot null && Adventure.htblAllProperties.ContainsKey(sKey) && Not Adventure.htblAllProperties(sKey).AppendToProperty <> "")
                {
                    if (Adventure.htblAllProperties(sKey).Type = clsProperty.PropertyTypeEnum.Text)
                    {
                        iOffset += 160;
                    Else
                        iOffset += 30;
                    }
                }
            }
        Next;

        bLoading = False

    }


    'Private Sub ClearPropertyForms()

    '    For Each frm As GenericProperty In arlPropertyForms
    '        frm.Visible = False
    '        frm.Dispose()
    '    Next

    '    arlPropertyForms.Clear()
    '    iPropHeight = 0

    'End Sub

    private bool PropOnShow(string sKey)
    {

        With htblProperties(sKey);
            if (.DependentKey Is null || .DependentKey = "")
            {
                return true;
            Else
                if (htblProperties.ContainsKey(.DependentKey))
                {
                    if (Not htblProperties(.DependentKey).Selected && .DependentValue = "false")
                    {
                        return true;
                    ElseIf htblProperties(.DependentKey).Selected Then
                        If .DependentValue == null || .DependentValue = htblProperties(.DependentKey).Value Then Return true
                    }
                }
                'If htblProperties.ContainsKey(.DependentKey) AndAlso htblProperties(.DependentKey).Selected Then
                '    If .DependentValue Is Nothing OrElse .DependentValue = htblProperties(.DependentKey).Value Then Return True
                'End If
            }
        }

    }

    public void RefreshProperties()
    {

        If htblProperties == null Then Exit Sub ' For when control is ran in design mode

        Windows.Forms.Cursor.Current = Cursors.WaitCursor;

        try
        {
            Static salProps As New StringArrayList;
            'Static iPropsOnShow As Integer = 0
            'Dim iNewPropsOnShow As Integer
            private New StringArrayList salNewProps;

            For Each prop As clsProperty In htblProperties.Values
                If PropOnShow(prop.Key) Then salNewProps.Add(prop.Key) ' iNewPropsOnShow += 1
            Next;

            private bool bPropertiesChanged;
            if (salNewProps.Count <> salProps.Count)
            {
                bPropertiesChanged = True
            Else
                For Each sKey As String In salProps
                    if (Not salNewProps.Contains(sKey))
                    {
                        bPropertiesChanged = True
                        Exit For;
                    }
                Next;
            }

            if (bPropertiesChanged)
            {
                'If iNewPropsOnShow <> iPropsOnShow Then
                private int iTop = pnlLines.Top;
                LoadProperties();
                'iPropsOnShow = iNewPropsOnShow
                salProps = salNewProps
                If pnlLines.Height > pnlContainer.Height Then pnlContainer.AutoScrollPosition = New Point(0, -Math.Max(iTop, pnlContainer.Height - pnlLines.Height))
            }
        }
        catch (Exception ex)
        {
            ErrMsg("RefreshProperties error", ex);
        }

        Windows.Forms.Cursor.Current = Cursors.Default;

    }


    public bool ValidateProperties(ref FailingProperty As String = Nothing)
    {

        ' Check all properties that are checked are also populated
        For Each prop As clsProperty In htblProperties.Values
            if (PropOnShow(prop.Key) && prop.Selected)
            {
                switch (prop.Type)
                {
                    case clsProperty.PropertyTypeEnum.CharacterKey:
                    case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    case clsProperty.PropertyTypeEnum.LocationKey:
                    case clsProperty.PropertyTypeEnum.ObjectKey:
                    case clsProperty.PropertyTypeEnum.StateList:
                        {
                        if (prop.Value = "")
                        {
                            FailingProperty = prop.Key
                            ErrMsg("Property """ + prop.Description + """ cannot be blank");
                            return false;
                        }
                }
            }
        Next;
        return true;

    }


    private void btnAddProperty_Click(System.Object sender, System.EventArgs e)
    {

        private New clsProperty prop;
        prop.PropertyOf = PropertyType;
        private New frmProperty(prop, False) frmProp;
        frmProp.chkPrivate.Visible = true;
        frmProp.OwnerKey = OwnerKey;
        frmProp.ShowDialog();
        frmProp.Dispose();
        private string sKey = prop.Key;
        if (sKey IsNot null)
        {
            htblProperties.Add(prop.Copy);
            RefreshProperties();
        }
        frmProp.Dispose();

    }

}

}