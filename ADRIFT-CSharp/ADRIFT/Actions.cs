using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Actions
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
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnDelete As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents pnlContainer As System.Windows.Forms.Panel;
    Friend WithEvents pnlLines As System.Windows.Forms.Panel;
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip;
    Friend WithEvents miAdd As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miEdit As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miDelete As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miCopy As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miPaste As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents lstSummary As System.Windows.Forms.ListBox;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton();
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton();
        Me.btnDelete = New Infragistics.Win.Misc.UltraButton();
        Me.btnUp = New Infragistics.Win.Misc.UltraButton();
        Me.btnDown = New Infragistics.Win.Misc.UltraButton();
        Me.pnlContainer = New System.Windows.Forms.Panel();
        Me.lstSummary = New System.Windows.Forms.ListBox();
        Me.pnlLines = New System.Windows.Forms.Panel();
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components);
        Me.miAdd = New System.Windows.Forms.ToolStripMenuItem();
        Me.miEdit = New System.Windows.Forms.ToolStripMenuItem();
        Me.miDelete = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator();
        Me.miCopy = New System.Windows.Forms.ToolStripMenuItem();
        Me.miPaste = New System.Windows.Forms.ToolStripMenuItem();
        Me.pnlContainer.SuspendLayout();
        Me.cmsMenu.SuspendLayout();
        Me.SuspendLayout();
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnAdd.Location = New System.Drawing.Point(0, 232);
        Me.btnAdd.Name = "btnAdd";
        Me.btnAdd.Size = New System.Drawing.Size(75, 23);
        Me.btnAdd.TabIndex = 1;
        Me.btnAdd.Text = "&Add";
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnEdit.Enabled = false;
        Me.btnEdit.Location = New System.Drawing.Point(88, 232);
        Me.btnEdit.Name = "btnEdit";
        Me.btnEdit.Size = New System.Drawing.Size(75, 23);
        Me.btnEdit.TabIndex = 2;
        Me.btnEdit.Text = "&Edit";
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnDelete.Enabled = false;
        Me.btnDelete.Location = New System.Drawing.Point(176, 232);
        Me.btnDelete.Name = "btnDelete";
        Me.btnDelete.Size = New System.Drawing.Size(75, 23);
        Me.btnDelete.TabIndex = 3;
        Me.btnDelete.Text = "&Delete";
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
        Me.btnUp.Enabled = false;
        Me.btnUp.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnUp.Location = New System.Drawing.Point(248, 88);
        Me.btnUp.Name = "btnUp";
        Me.btnUp.Size = New System.Drawing.Size(24, 23);
        Me.btnUp.TabIndex = 5;
        Me.btnUp.Text = "5";
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
        Me.btnDown.Enabled = false;
        Me.btnDown.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnDown.Location = New System.Drawing.Point(248, 120);
        Me.btnDown.Name = "btnDown";
        Me.btnDown.Size = New System.Drawing.Size(24, 23);
        Me.btnDown.TabIndex = 6;
        Me.btnDown.Text = "6";
        '
        'pnlContainer
        '
        Me.pnlContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlContainer.AutoScroll = true;
        Me.pnlContainer.BackColor = System.Drawing.SystemColors.AppWorkspace;
        Me.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.pnlContainer.ContextMenuStrip = Me.cmsMenu;
        Me.pnlContainer.Controls.Add(Me.lstSummary);
        Me.pnlContainer.Controls.Add(Me.pnlLines);
        Me.pnlContainer.Location = New System.Drawing.Point(0, 0);
        Me.pnlContainer.Name = "pnlContainer";
        Me.pnlContainer.Size = New System.Drawing.Size(248, 224);
        Me.pnlContainer.TabIndex = 10;
        '
        'lstSummary
        '
        Me.lstSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lstSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.lstSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstSummary.ItemHeight = 18;
        Me.lstSummary.Location = New System.Drawing.Point(0, 0);
        Me.lstSummary.Name = "lstSummary";
        Me.lstSummary.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        Me.lstSummary.Size = New System.Drawing.Size(246, 18);
        Me.lstSummary.TabIndex = 22;
        Me.lstSummary.Visible = false;
        '
        'pnlLines
        '
        Me.pnlLines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlLines.BackColor = System.Drawing.SystemColors.ControlDark;
        Me.pnlLines.Location = New System.Drawing.Point(0, 0);
        Me.pnlLines.Name = "pnlLines";
        Me.pnlLines.Size = New System.Drawing.Size(242, 0);
        Me.pnlLines.TabIndex = 13;
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miAdd, Me.miEdit, Me.miDelete, Me.ToolStripSeparator1, Me.miCopy, Me.miPaste});
        Me.cmsMenu.Name = "cmsMenu";
        Me.cmsMenu.Size = New System.Drawing.Size(108, 120);
        '
        'miAdd
        '
        Me.miAdd.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.miAdd.Name = "miAdd";
        Me.miAdd.Size = New System.Drawing.Size(107, 22);
        Me.miAdd.Text = "&Add";
        '
        'miEdit
        '
        Me.miEdit.Image = Global.ADRIFT.My.Resources.Resources.imgEdit16;
        Me.miEdit.Name = "miEdit";
        Me.miEdit.Size = New System.Drawing.Size(107, 22);
        Me.miEdit.Text = "&Edit";
        '
        'miDelete
        '
        Me.miDelete.Image = Global.ADRIFT.My.Resources.Resources.imgDelete;
        Me.miDelete.Name = "miDelete";
        Me.miDelete.Size = New System.Drawing.Size(107, 22);
        Me.miDelete.Text = "&Delete";
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1";
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(104, 6);
        '
        'miCopy
        '
        Me.miCopy.Image = Global.ADRIFT.My.Resources.Resources.imgCopy;
        Me.miCopy.Name = "miCopy";
        Me.miCopy.Size = New System.Drawing.Size(107, 22);
        Me.miCopy.Text = "&Copy";
        '
        'miPaste
        '
        Me.miPaste.Image = Global.ADRIFT.My.Resources.Resources.imgPaste;
        Me.miPaste.Name = "miPaste";
        Me.miPaste.Size = New System.Drawing.Size(107, 22);
        Me.miPaste.Text = "&Paste";
        '
        'Actions
        '
        Me.BackColor = System.Drawing.SystemColors.Control;
        Me.Controls.Add(Me.pnlContainer);
        Me.Controls.Add(Me.btnDown);
        Me.Controls.Add(Me.btnUp);
        Me.Controls.Add(Me.btnDelete);
        Me.Controls.Add(Me.btnEdit);
        Me.Controls.Add(Me.btnAdd);
        Me.Name = "Actions";
        Me.Size = New System.Drawing.Size(272, 256);
        Me.pnlContainer.ResumeLayout(false);
        Me.cmsMenu.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    internal ActionArrayList arlActions;
    private const int iHEIGHT = 18;
    public string sCommand;


    internal void LoadActions(ref arlAct As ActionArrayList)
    {
        try
        {
            Me.arlActions = New ActionArrayList;
            LoadGrid(arlAct);
        }
        catch (Exception ex)
        {
            ErrMsg("Error loading actions", ex);
        }
    }


    private void LoadGrid(ActionArrayList actions)
    {

        private int iAct = lstSummary.Items.Count;

        For Each act As clsAction In actions
            arlActions.Add(act);
            AddAct(act);
        Next;

        DoHeight()

    }

    private void AddAct(clsAction act)
    {

        lstSummary.Items.Add(act);
        DoButtons()

    }


    private DialogResult EditAction(ref act As clsAction)
    {

        private New frmAction(Me.sCommand) fAction;

        fAction.LoadAction(act.Copy);
        If fAction.ShowDialog = DialogResult.OK Then act = fAction.Action

        return fAction.DialogResult;

    }


    private void btnAdd_Click(System.Object sender, System.EventArgs e)
    {

        private New clsAction act;

        if (EditAction(act) = DialogResult.OK)
        {

            arlActions.Add(act);

            DoHeight()
            AddAct(act);
        }

    }


    private void DoHeight()
    {

        if (arlActions.Count > 0)
        {
            lstSummary.Visible = true;
        Else
            lstSummary.Visible = false;
        }

        lstSummary.Height = arlActions.Count * iHEIGHT;

    }


    private void lst_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private ListBox lst = CType(sender, ListBox);
        private int iSelectedIndex = lst.IndexFromPoint(e.X, e.Y);
        if (iSelectedIndex > -1)
        {

            if (ModifierKeys = Keys.Shift)
            {
                ' Range
                for (int i = lst.SelectedIndex; i <= iSelectedIndex; i += CInt(If(iSelectedIndex > lst.SelectedIndex, 1, -1)))
                {
                    lst.SetSelected(i, true);
                Next;
            ElseIf ModifierKeys = Keys.Control Then
                ' No intervention necessary, it would seem
            Else
                lst.SelectedIndex = iSelectedIndex;
            }
            SyncLists(lst);
        }

    }


    private void SyncLists(ListBox lstSource)
    {

        'Dim iIndex As Integer = lstSource.SelectedIndex

        'lstSummary.SelectedIndex = iIndex

        DoButtons()

    }


    private void DoButtons()
    {

        if (lstSummary.SelectedItems.Count = 1)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            If lstSummary.SelectedIndex < 1 Then btnUp.Enabled = false Else btnUp.Enabled = true
            If lstSummary.SelectedIndex < 0 || lstSummary.SelectedIndex = lstSummary.Items.Count - 1 Then btnDown.Enabled = false Else btnDown.Enabled = true
        ElseIf lstSummary.SelectedItems.Count > 1 Then
            btnEdit.Enabled = false;
            btnDelete.Enabled = true;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            If lstSummary.Items.Count > 0 && ! lstSummary.GetSelected(0) Then btnUp.Enabled = true Else btnUp.Enabled = false
            If lstSummary.Items.Count > 0 && ! lstSummary.GetSelected(lstSummary.Items.Count - 1) Then btnDown.Enabled = true Else btnDown.Enabled = false
        Else
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
        }

    }
    'Private Sub DoButtons()

    '    If lstSummary.SelectedIndex > -1 Then
    '        btnEdit.Enabled = True
    '        btnDelete.Enabled = True
    '    Else
    '        btnEdit.Enabled = False
    '        btnDelete.Enabled = False
    '    End If

    '    If lstSummary.SelectedIndex < 1 Then btnUp.Enabled = False Else btnUp.Enabled = True
    '    If lstSummary.SelectedIndex < 0 OrElse lstSummary.SelectedIndex = lstSummary.Items.Count - 1 Then btnDown.Enabled = False Else btnDown.Enabled = True

    'End Sub


    private void btnDelete_Click(System.Object sender, System.EventArgs e)
    {

        for (int iIndex = lstSummary.Items.Count - 1; iIndex <= 0; iIndex += -1)
        {
            if (lstSummary.GetSelected(iIndex))
            {
                arlActions.Remove((clsAction)(lstSummary.Items(iIndex)));

                lstSummary.Items.RemoveAt(iIndex);
            }
        Next;
        'Dim iIndex As Integer = lstSummary.SelectedIndex

        DoHeight()
        DoButtons()

    }

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    '    Dim iIndex As Integer = lstSummary.SelectedIndex

    '    arlActions.Remove(CType(lstSummary.Items(iIndex), clsAction))

    '    lstSummary.Items.RemoveAt(iIndex)

    '    DoHeight()
    '    DoButtons()

    'End Sub

    private void btnUpDown_Click(System.Object sender, System.EventArgs e)
    {

        private int iUpDown = CInt(IIf(sender Is btnUp, -1, 1));

        private New List<int> lstSelected;
        For Each i As Integer In lstSummary.SelectedIndices
            lstSelected.Add(i);
        Next;

        for (int i = CInt(If(iUpDown = -1, 0, lstSummary.Items.Count - 1)); i <= CInt(If(iUpDown = -1, lstSummary.Items.Count - 1, 0)); i += -iUpDown)
        {
            if (lstSummary.GetSelected(i))
            {
                private clsAction tempact;

                tempact = CType(lstSummary.Items(i), clsAction)
                lstSummary.Items(i) = lstSummary.Items(i + iUpDown);
                lstSummary.Items(i + iUpDown) = tempact;

                arlActions(i) = (clsAction)(lstSummary.Items(i));
                arlActions(i + iUpDown) = (clsAction)(lstSummary.Items(i + iUpDown));
            }

        Next;

        lstSummary.ClearSelected();
        For Each i As Integer In lstSelected
            lstSummary.SetSelected(i + iUpDown, true);
        Next;

        DoButtons()

    }



    'Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click

    '    Dim tempact As clsAction
    '    Dim i As Integer = lstSummary.SelectedIndex

    '    tempact = CType(lstSummary.Items(i), clsAction)
    '    lstSummary.Items(i) = lstSummary.Items(i - 1)
    '    lstSummary.Items(i - 1) = tempact

    '    arlActions(i) = CType(lstSummary.Items(i), clsAction)
    '    arlActions(i - 1) = CType(lstSummary.Items(i - 1), clsAction)

    '    lstSummary.SelectedIndex = i - 1
    '    DoButtons()

    'End Sub

    'Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

    '    Dim tempact As clsAction
    '    Dim i As Integer = lstSummary.SelectedIndex

    '    tempact = CType(lstSummary.Items(i), clsAction)
    '    lstSummary.Items(i) = lstSummary.Items(i + 1)
    '    lstSummary.Items(i + 1) = tempact

    '    arlActions(i) = CType(lstSummary.Items(i), clsAction)
    '    arlActions(i + 1) = CType(lstSummary.Items(i + 1), clsAction)

    '    lstSummary.SelectedIndex = i + 1
    '    DoButtons()

    'End Sub

    private void btnEdit_Click(object sender, System.EventArgs e)
    {
        Edit();
    }

    private void Edit()
    {

        private clsAction act = arlActions(Me.lstSummary.SelectedIndex).Copy;

        if (EditAction(act) = DialogResult.OK)
        {

            arlActions(lstSummary.SelectedIndex) = act;
            lstSummary.Items(lstSummary.SelectedIndex) = act;

            DoHeight()
        }

    }

    private void lstSummary_DoubleClick(object sender, System.EventArgs e)
    {
        If lstSummary.SelectedIndex > -1 Then Edit()
    }

    private void cmsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        miCopy.Enabled = lstSummary.SelectedIndex > -1;
        miPaste.Enabled = xmlActions != null;
    }

    private void btnDelete_EnabledChanged(object sender, EventArgs e)
    {
        miDelete.Enabled = btnDelete.Enabled;
    }

    private void btnEdit_EnabledChanged(object sender, EventArgs e)
    {
        miEdit.Enabled = btnEdit.Enabled;
    }

    private void miAdd_Click(object sender, EventArgs e)
    {
        btnAdd_Click(sender, e);
    }

    private void miEdit_Click(object sender, EventArgs e)
    {
        btnEdit_Click(sender, e);
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
        btnDelete_Click(sender, e);
    }

    private void miCopy_Click(object sender, EventArgs e)
    {

        private New ActionArrayList copyActions;
        private New IO.MemoryStream ms;
        private New Xml.XmlTextWriter(ms, System.Text.Encoding.UTF8) xml;
        xml.Formatting = System.Xml.Formatting.Indented;
        xml.WriteStartDocument();
        xml.WriteStartElement("Actions");

        for (int i = 0; i <= lstSummary.Items.Count - 1; i++)
        {
            if (lstSummary.GetSelected(i))
            {
                copyActions.Add((clsAction)(lstSummary.Items(i)).Copy);
            }
        Next;
        SaveActions(xml, copyActions);
        xml.WriteEndElement();
        xml.WriteEndDocument();
        xml.Flush();

        xmlActions = ms.ToArray

    }

    private void miPaste_Click(object sender, EventArgs e)
    {

        private New IO.MemoryStream(xmlActions) ms;
        private New Xml.XmlDocument xmlDoc;
        xmlDoc.Load(ms);
        private ActionArrayList pasteActions = FileIO.LoadActions(xmlDoc.DocumentElement);
        private int iStart = lstSummary.Items.Count;
        LoadGrid(pasteActions);

        lstSummary.SelectedItems.Clear();
        for (int i = iStart; i <= lstSummary.Items.Count - 1; i++)
        {
            lstSummary.SelectedIndices.Add(i);
        Next;
        SyncLists(lstSummary);

    }

}

}