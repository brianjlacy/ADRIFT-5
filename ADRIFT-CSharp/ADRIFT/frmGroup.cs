using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmGroup
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public bool bModal = false;

    public void New(clsGroup RoomGroup, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmGroup)
            {
                if (CType(w, frmGroup).cGroup.Key = RoomGroup.Key && RoomGroup.Key IsNot null)
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
        bModal = Not bShow
        LoadForm(RoomGroup);

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
    Friend WithEvents tabsMain As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents lvwItems As System.Windows.Forms.ListView;
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader;
    Friend WithEvents txtDescription As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents GroupProperties As ADRIFT.Properties;
    Friend WithEvents cmbGroupType As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.cmbGroupType = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel();
        Me.lvwItems = New System.Windows.Forms.ListView();
        Me.ColumnHeader1 = (System.Windows.Forms.ColumnHeader)(New System.Windows.Forms.ColumnHeader());
        Me.txtDescription = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.GroupProperties = New ADRIFT.Properties();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.tabsMain = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbGroupType).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtDescription).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).BeginInit();
        Me.tabsMain.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.cmbGroupType);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel2);
        Me.UltraTabPageControl1.Controls.Add(Me.lvwItems);
        Me.UltraTabPageControl1.Controls.Add(Me.txtDescription);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel1);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(308, 316);
        '
        'cmbGroupType
        '
        Me.cmbGroupType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem5.DataValue = "2";
        ValueListItem5.DisplayText = "Characters";
        ValueListItem3.DataValue = "0";
        ValueListItem3.DisplayText = "Locations";
        ValueListItem4.DataValue = "1";
        ValueListItem4.DisplayText = "Objects";
        Me.cmbGroupType.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem5, ValueListItem3, ValueListItem4});
        Me.cmbGroupType.Location = New System.Drawing.Point(82, 36);
        Me.cmbGroupType.Name = "cmbGroupType";
        Me.cmbGroupType.Size = New System.Drawing.Size(219, 21);
        Me.cmbGroupType.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbGroupType.TabIndex = 26;
        '
        'UltraLabel2
        '
        Me.UltraLabel2.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Location = New System.Drawing.Point(11, 41);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(72, 16);
        Me.UltraLabel2.TabIndex = 25;
        Me.UltraLabel2.Text = "Group Type:";
        '
        'lvwItems
        '
        Me.lvwItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lvwItems.CheckBoxes = true;
        Me.lvwItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1});
        Me.lvwItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lvwItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        Me.lvwItems.Location = New System.Drawing.Point(7, 63);
        Me.lvwItems.MultiSelect = false;
        Me.lvwItems.Name = "lvwItems";
        Me.lvwItems.Size = New System.Drawing.Size(294, 202);
        Me.lvwItems.Sorting = System.Windows.Forms.SortOrder.Ascending;
        Me.lvwItems.TabIndex = 20;
        Me.lvwItems.UseCompatibleStateImageBehavior = false;
        Me.lvwItems.View = System.Windows.Forms.View.Details;
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 300;
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDescription.Location = New System.Drawing.Point(82, 10);
        Me.txtDescription.Name = "txtDescription";
        Me.txtDescription.Size = New System.Drawing.Size(219, 21);
        Me.txtDescription.TabIndex = 3;
        '
        'UltraLabel1
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Appearance = Appearance1;
        Me.UltraLabel1.Location = New System.Drawing.Point(11, 13);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(64, 16);
        Me.UltraLabel1.TabIndex = 2;
        Me.UltraLabel1.Text = "Description:";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.GroupProperties);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(308, 316);
        '
        'GroupProperties
        '
        Me.GroupProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.GroupProperties.BackColor = System.Drawing.Color.Transparent;
        Me.GroupProperties.Location = New System.Drawing.Point(0, 0);
        Me.GroupProperties.Name = "GroupProperties";
        Me.GroupProperties.Size = New System.Drawing.Size(308, 270);
        Me.GroupProperties.TabIndex = 0;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(208, 304);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 18;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(112, 304);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 17;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(16, 304);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 16;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 294);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(312, 48);
        Me.UltraStatusBar1.TabIndex = 15;
        '
        'tabsMain
        '
        Me.tabsMain.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsMain.Location = New System.Drawing.Point(0, 0);
        Me.tabsMain.Name = "tabsMain";
        Me.tabsMain.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsMain.Size = New System.Drawing.Size(312, 342);
        Me.tabsMain.TabIndex = 21;
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Selections";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Properties";
        Me.tabsMain.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(308, 316);
        '
        'frmGroup
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(312, 342);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.Controls.Add(Me.tabsMain);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "frmGroup";
        Me.Text = "Groups";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbGroupType).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtDescription).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).EndInit();
        Me.tabsMain.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private bool bChanged;
    private clsGroup cGroup;
    private StringArrayList arlMembers;

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


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyGroup();
        DialogResult = Windows.Forms.DialogResult.OK
        CloseGroup(Me);
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyGroup()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        DialogResult = Windows.Forms.DialogResult.Cancel
        CloseGroup(Me);
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyGroup();
        Changed = False
    }

    private void ApplyGroup()
    {

        With cGroup;

            .arlMembers.Clear();
            if (.Key <> ALLROOMS && .Key <> NOROOMS)
            {
                For Each lvi As ListViewItem In lvwItems.Items
                    if (lvi.Checked)
                    {
                        .arlMembers.Add(CStr(lvi.Tag));
                    }
                Next;
            }

            .Name = Me.txtDescription.Text;
            .GroupType = (clsGroup.GroupTypeEnum)(cmbGroupType.SelectedItem.DataValue);
            .htblProperties = GroupProperties.htblProperties.CopySelected(true);
            .LastUpdated = Now;
            .IsLibrary = false;

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Group");
                If ! bModal Then Adventure.htblGroups.Add(cGroup, .Key)
                Me.GroupProperties.OwnerKey = .Key;
            }

            If ! bModal Then UpdateListItem(.Key, .Name)

        }

        Adventure.Changed = true;

    }


    private void LoadList(clsGroup.GroupTypeEnum eGroupType)
    {

        If lvwItems.Tag != null && (clsGroup.GroupTypeEnum)(lvwItems.Tag) = eGroupType Then Exit Sub

        lvwItems.Items.Clear();

        switch (eGroupType)
        {
            case clsGroup.GroupTypeEnum.Locations:
                {
                For Each loc As clsLocation In Adventure.htblLocations.Values
                    private New ListViewItem lvi;
                    lvi.Text = loc.ShortDescription.ToString;
                    lvi.SubItems.Add(loc.ShortDescription.ToString);
                    lvi.Tag = loc.Key;
                    lvi.ImageIndex = 0;
                    Me.lvwItems.Items.Add(lvi);
                    If arlMembers.Contains(loc.Key) Then lvwItems.Items(lvi.Index).Checked = true
                Next;
            case clsGroup.GroupTypeEnum.Objects:
                {
                For Each ob As clsObject In Adventure.htblObjects.Values
                    private New ListViewItem lvi;
                    lvi.Text = ob.FullName;
                    lvi.SubItems.Add(ob.FullName);
                    lvi.Tag = ob.Key;
                    if (ob.IsStatic)
                    {
                        lvi.ImageIndex = 1;
                    Else
                        lvi.ImageIndex = 2;
                    }
                    Me.lvwItems.Items.Add(lvi);
                    If arlMembers.Contains(ob.Key) Then lvwItems.Items(lvi.Index).Checked = true
                Next;
            case clsGroup.GroupTypeEnum.Characters:
                {
                For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    private New ListViewItem lvi;
                    lvi.Text = ch.Name;
                    lvi.SubItems.Add(ch.Name);
                    lvi.Tag = ch.Key;
                    if (ch Is Adventure.Player)
                    {
                        lvi.ImageIndex = 3;
                    Else
                        lvi.ImageIndex = 4;
                    }
                    Me.lvwItems.Items.Add(lvi);
                    If arlMembers.Contains(ch.Key) Then lvwItems.Items(lvi.Index).Checked = true
                Next;
        }
        lvwItems.Tag = eGroupType;

    }


    private void LoadForm(ref cRoomGroup As clsGroup)
    {

        Me.cGroup = cRoomGroup;

        With cRoomGroup;
            Text = "Location Group - " & .Name
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            'LoadList(.GroupType)
            If .Name = "" Then Text = "New Group"
            Me.txtDescription.Text = .Name;
            arlMembers = .arlMembers.Clone
            GroupProperties.bGroup = true;
            GroupProperties.OwnerKey = .Key;

            'SetOptSet(optGroupType, .GroupType)
            SetCombo(cmbGroupType, CInt(.GroupType));
        }

        If cRoomGroup.Key = NOROOMS || cRoomGroup.Key = ALLROOMS Then lvwItems.Enabled = false ' Special cases
        Changed = False

        If ! bModal Then Me.Show()

        OpenForms.Add(Me);

    }

    private void frmGroup_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmRoomGroup_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgGroup16.GetHicon);
        GetFormPosition(Me);
        private New ImageList il;
        il.Images.Add("Location", My.Resources.Resources.imgLocation16);
        il.Images.Add("Static", My.Resources.Resources.imgObjectStatic16);
        il.Images.Add("Dynamic", My.Resources.Resources.imgObjectDynamic16);
        il.Images.Add("Player", My.Resources.Resources.imgPlayer16);
        il.Images.Add("Character", My.Resources.Resources.imgCharacter16);
        lvwItems.SmallImageList = il;
    }

    protected override void WndProc(ref m As System.Windows.Forms.Message)
    {

        private const int WM_PAINT = &HF  ' 15;

        switch (m.Msg)
        {
            case WM_PAINT:
                {
                if (lvwItems.View = View.Details && lvwItems.Columns.Count > 0)
                {
                    lvwItems.Columns(lvwItems.Columns.Count - 1).Width = -2;
                }
        }

        MyBase.WndProc(m);

    }

    private void lvwRooms_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        If lvwItems.SelectedItems.Count > 0 && lvwItems.SelectedItems(0).Index > -1 Then lvwItems.Items(lvwItems.SelectedItems(0).Index).Selected = false
    }

    private void cmbGroupType_ValueChanged(object sender, System.EventArgs e)
    {
        If Me.Visible Then cGroup.htblProperties.Clear()
        switch (CType(cmbGroupType.Value, clsGroup.GroupTypeEnum))
        {
            case clsGroup.GroupTypeEnum.Objects:
                {
                GroupProperties.htblProperties = cGroup.htblProperties.Clone;
                ' Pad out the local Object hashtable with unselected properties
                For Each prop As clsProperty In Adventure.htblObjectProperties.Values
                    If ! GroupProperties.htblProperties.ContainsKey(prop.Key) Then GroupProperties.htblProperties.Add(prop.Copy)
                Next;
                GroupProperties.PropertyType = clsProperty.PropertyOfEnum.Objects;
            case clsGroup.GroupTypeEnum.Characters:
                {
                ' Pad out the local Object hashtable with unselected properties
                GroupProperties.htblProperties = cGroup.htblProperties.Clone;
                For Each prop As clsProperty In Adventure.htblCharacterProperties.Values
                    If ! GroupProperties.htblProperties.ContainsKey(prop.Key) Then GroupProperties.htblProperties.Add(prop.Copy)
                Next;
                GroupProperties.PropertyType = clsProperty.PropertyOfEnum.Characters;
            case clsGroup.GroupTypeEnum.Locations:
                {
                ' Pad out the local Object hashtable with unselected properties
                GroupProperties.htblProperties = cGroup.htblProperties.Clone;
                For Each prop As clsProperty In Adventure.htblLocationProperties.Values
                    If ! GroupProperties.htblProperties.ContainsKey(prop.Key) Then GroupProperties.htblProperties.Add(prop.Copy)
                Next;
                GroupProperties.PropertyType = clsProperty.PropertyOfEnum.Locations;
        }
        LoadList((clsGroup.GroupTypeEnum)(cmbGroupType.SelectedItem.DataValue));
    }

    private void lvwItems_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
    {
        if (e.NewValue = CheckState.Checked)
        {
            arlMembers.Add(SafeString(lvwItems.Items(e.Index).Tag));
        Else
            arlMembers.Remove(SafeString(lvwItems.Items(e.Index).Tag));
        }
    }

    private void frmGroup_Shown(object sender, System.EventArgs e)
    {
        if (txtDescription.Text = "")
        {
            txtDescription.Focus();
        Else
            ' Dunno...
        }
    }

    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void frmGroup_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "GroupsClasses");
    }

}

}