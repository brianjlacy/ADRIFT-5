using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class RestrictDetails
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
    Friend WithEvents lstLB As System.Windows.Forms.ListBox;
    Friend WithEvents lstSummary As System.Windows.Forms.ListBox;
    Friend WithEvents lstRB As System.Windows.Forms.ListBox;
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip;
    Friend WithEvents miAdd As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miEdit As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miDelete As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator;
    Friend WithEvents miCopy As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miPaste As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents lstAndOr As System.Windows.Forms.ListBox;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container();
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton();
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton();
        Me.btnDelete = New Infragistics.Win.Misc.UltraButton();
        Me.btnUp = New Infragistics.Win.Misc.UltraButton();
        Me.btnDown = New Infragistics.Win.Misc.UltraButton();
        Me.pnlContainer = New System.Windows.Forms.Panel();
        Me.lstAndOr = New System.Windows.Forms.ListBox();
        Me.lstRB = New System.Windows.Forms.ListBox();
        Me.lstSummary = New System.Windows.Forms.ListBox();
        Me.lstLB = New System.Windows.Forms.ListBox();
        Me.pnlLines = New System.Windows.Forms.Panel();
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components);
        Me.miAdd = New System.Windows.Forms.ToolStripMenuItem();
        Me.miEdit = New System.Windows.Forms.ToolStripMenuItem();
        Me.miDelete = New System.Windows.Forms.ToolStripMenuItem();
        Me.miCopy = New System.Windows.Forms.ToolStripMenuItem();
        Me.miPaste = New System.Windows.Forms.ToolStripMenuItem();
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator();
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
        Me.btnAdd.Text = "Add";
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnEdit.Enabled = false;
        Me.btnEdit.Location = New System.Drawing.Point(88, 232);
        Me.btnEdit.Name = "btnEdit";
        Me.btnEdit.Size = New System.Drawing.Size(75, 23);
        Me.btnEdit.TabIndex = 2;
        Me.btnEdit.Text = "Edit";
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnDelete.Enabled = false;
        Me.btnDelete.Location = New System.Drawing.Point(176, 232);
        Me.btnDelete.Name = "btnDelete";
        Me.btnDelete.Size = New System.Drawing.Size(75, 23);
        Me.btnDelete.TabIndex = 3;
        Me.btnDelete.Text = "Delete";
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
        Me.pnlContainer.Controls.Add(Me.lstAndOr);
        Me.pnlContainer.Controls.Add(Me.lstRB);
        Me.pnlContainer.Controls.Add(Me.lstSummary);
        Me.pnlContainer.Controls.Add(Me.lstLB);
        Me.pnlContainer.Controls.Add(Me.pnlLines);
        Me.pnlContainer.Location = New System.Drawing.Point(0, 0);
        Me.pnlContainer.Name = "pnlContainer";
        Me.pnlContainer.Size = New System.Drawing.Size(248, 224);
        Me.pnlContainer.TabIndex = 10;
        '
        'lstAndOr
        '
        Me.lstAndOr.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.lstAndOr.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.lstAndOr.ContextMenuStrip = Me.cmsMenu;
        Me.lstAndOr.Cursor = System.Windows.Forms.Cursors.Arrow;
        Me.lstAndOr.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstAndOr.ItemHeight = 18;
        Me.lstAndOr.Location = New System.Drawing.Point(206, 0);
        Me.lstAndOr.Name = "lstAndOr";
        Me.lstAndOr.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        Me.lstAndOr.Size = New System.Drawing.Size(40, 18);
        Me.lstAndOr.TabIndex = 24;
        Me.lstAndOr.Visible = false;
        '
        'lstRB
        '
        Me.lstRB.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.lstRB.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.lstRB.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.lstRB.ContextMenuStrip = Me.cmsMenu;
        Me.lstRB.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.lstRB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstRB.ItemHeight = 18;
        Me.lstRB.Location = New System.Drawing.Point(190, 0);
        Me.lstRB.Name = "lstRB";
        Me.lstRB.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        Me.lstRB.Size = New System.Drawing.Size(16, 18);
        Me.lstRB.TabIndex = 23;
        Me.lstRB.Visible = false;
        '
        'lstSummary
        '
        Me.lstSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.lstSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.lstSummary.ContextMenuStrip = Me.cmsMenu;
        Me.lstSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstSummary.ItemHeight = 18;
        Me.lstSummary.Location = New System.Drawing.Point(16, 0);
        Me.lstSummary.Name = "lstSummary";
        Me.lstSummary.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        Me.lstSummary.Size = New System.Drawing.Size(174, 18);
        Me.lstSummary.TabIndex = 22;
        Me.lstSummary.Visible = false;
        '
        'lstLB
        '
        Me.lstLB.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.lstLB.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.lstLB.ContextMenuStrip = Me.cmsMenu;
        Me.lstLB.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.lstLB.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstLB.ItemHeight = 18;
        Me.lstLB.Location = New System.Drawing.Point(0, 0);
        Me.lstLB.Name = "lstLB";
        Me.lstLB.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        Me.lstLB.Size = New System.Drawing.Size(16, 18);
        Me.lstLB.TabIndex = 21;
        Me.lstLB.Visible = false;
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
        Me.cmsMenu.Size = New System.Drawing.Size(153, 142);
        '
        'miAdd
        '
        Me.miAdd.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.miAdd.Name = "miAdd";
        Me.miAdd.Size = New System.Drawing.Size(152, 22);
        Me.miAdd.Text = "&Add";
        '
        'miEdit
        '
        Me.miEdit.Image = Global.ADRIFT.My.Resources.Resources.imgEdit16;
        Me.miEdit.Name = "miEdit";
        Me.miEdit.Size = New System.Drawing.Size(152, 22);
        Me.miEdit.Text = "&Edit";
        '
        'miDelete
        '
        Me.miDelete.Image = Global.ADRIFT.My.Resources.Resources.imgDelete;
        Me.miDelete.Name = "miDelete";
        Me.miDelete.Size = New System.Drawing.Size(152, 22);
        Me.miDelete.Text = "&Delete";
        '
        'miCopy
        '
        Me.miCopy.Image = Global.ADRIFT.My.Resources.Resources.imgCopy;
        Me.miCopy.Name = "miCopy";
        Me.miCopy.Size = New System.Drawing.Size(152, 22);
        Me.miCopy.Text = "&Copy";
        '
        'miPaste
        '
        Me.miPaste.Image = Global.ADRIFT.My.Resources.Resources.imgPaste;
        Me.miPaste.Name = "miPaste";
        Me.miPaste.Size = New System.Drawing.Size(152, 22);
        Me.miPaste.Text = "&Paste";
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1";
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6);
        '
        'RestrictDetails
        '
        Me.BackColor = System.Drawing.SystemColors.Control;
        Me.Controls.Add(Me.pnlContainer);
        Me.Controls.Add(Me.btnDown);
        Me.Controls.Add(Me.btnUp);
        Me.Controls.Add(Me.btnDelete);
        Me.Controls.Add(Me.btnEdit);
        Me.Controls.Add(Me.btnAdd);
        Me.Name = "RestrictDetails";
        Me.Size = New System.Drawing.Size(272, 256);
        Me.pnlContainer.ResumeLayout(false);
        Me.cmsMenu.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    internal RestrictionArrayList arlRestrictions;
    'Private arlRestrictLines As ArrayList
    private const int iHEIGHT = 18;
    public string sCommand;
    internal List<clsUserFunction.Argument> Arguments { get; set; }
    Public Event RestrictionsChanged()
    public bool bNoElseText = false;


    internal void LoadRestrictions(ref arlRest As RestrictionArrayList)
    {
        try
        {
            'Me.arlRestrictions = arlRest
            arlRestrictions = New RestrictionArrayList
            LoadGrid(arlRest);
        }
        catch (Exception ex)
        {
            ErrMsg("Error loading restrictions", ex);
        }
    }


    private void LoadGrid(RestrictionArrayList restrictions)
    {

        private int iRest = lstSummary.Items.Count;

        If arlRestrictions.Count > 0 && restrictions.Count > 0 Then arlRestrictions.BracketSequence &= "A"
        For Each rest As clsRestriction In restrictions
            arlRestrictions.Add(rest);
            AddRest(rest);
        Next;
        arlRestrictions.BracketSequence &= restrictions.BracketSequence;

        LoadBrackets(restrictions.BracketSequence, iRest);

        DoHeight()

    }


    private void LoadBrackets(string sBrackTemp, int iRest = 0)
    {

        while (sBrackTemp <> "")
        {
            if (sLeft(sBrackTemp, 1) = "[")
            {
                lstLB.Items(iRest) = "((";
                sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            }
            if (sLeft(sBrackTemp, 1) = "(")
            {
                lstLB.Items(iRest) = " (";
                sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            }
            sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            if (sLeft(sBrackTemp, 1) = ")")
            {
                lstRB.Items(iRest) = ") ";
                sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            }
            if (sLeft(sBrackTemp, 1) = "]")
            {
                lstRB.Items(iRest) = "))";
                sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            }
            if (sBrackTemp <> "")
            {
                If sLeft(sBrackTemp, 1) = "O" Then lstAndOr.Items(iRest) = "OR"
                sBrackTemp = sRight(sBrackTemp, Len(sBrackTemp) - 1)
            }
            iRest += 1;
        }

    }


    private void AddRest(clsRestriction rest)
    {

        lstLB.Items.Add("");
        lstSummary.Items.Add(rest);
        lstRB.Items.Add("");
        lstAndOr.Items.Add("");
        if (lstAndOr.Items.Count > 1 && CStr(lstAndOr.Items(lstAndOr.Items.Count - 2)) = "")
        {
            lstAndOr.Items(lstAndOr.Items.Count - 2) = "AND";
            lstAndOr.Cursor = Cursors.Hand;
        }

        DoButtons()

    }


    private DialogResult EditRestriction(ref rest As clsRestriction)
    {

        private New frmRestriction fRestriction;

        fRestriction.sCommand = Me.sCommand;
        fRestriction.Arguments = Me.Arguments;
        fRestriction.LoadRestriction(rest.Copy);
        fRestriction.txtMessage.Enabled = ! bNoElseText;
        if (fRestriction.ShowDialog = DialogResult.OK)
        {
            rest = fRestriction.Restriction
            RaiseEvent RestrictionsChanged();
        }

        return fRestriction.DialogResult;

    }


    private void btnAdd_Click(System.Object sender, System.EventArgs e)
    {

        private New clsRestriction rest;
        rest.StringValue = "*NEW*";

        if (EditRestriction(rest) = DialogResult.OK)
        {

            arlRestrictions.Add(rest);

            DoHeight()
            AddRest(rest);
            UpdateBracketSequence();
        }

    }


    private void DoHeight()
    {

        if (arlRestrictions.Count > 0)
        {
            lstLB.Visible = true;
            lstSummary.Visible = true;
            lstRB.Visible = true;
            lstAndOr.Visible = true;
        Else
            lstLB.Visible = false;
            lstSummary.Visible = false;
            lstRB.Visible = false;
            lstAndOr.Visible = false;
        }

        lstLB.Height = arlRestrictions.Count * iHEIGHT;
        lstSummary.Height = arlRestrictions.Count * iHEIGHT;
        lstRB.Height = arlRestrictions.Count * iHEIGHT;
        lstAndOr.Height = arlRestrictions.Count * iHEIGHT;

    }

    private void lstLB_Click(object sender, System.EventArgs e)
    {

        private ListBox lst = CType(sender, ListBox);
        private int iSelectedIndex = lst.IndexFromPoint(lst.PointToClient(MousePosition));

        if (iSelectedIndex > -1)
        {
            bSyncing = True
            if (CStr(lst.Items(iSelectedIndex)) = "")
            {
                if ((Windows.Forms.Control.ModifierKeys And Keys.Control) <> 0)
                {
                    lst.Items(iSelectedIndex) = If(lst == lstLB, "((", "))").ToString;
                Else
                    lst.Items(iSelectedIndex) = If(lst == lstLB, " (", ") ").ToString;
                }
            Else
                lst.Items(iSelectedIndex) = "";
            }
            UpdateBracketSequence();
            bSyncing = False
        }

    }

    'Private Sub lstRB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRB.Click
    '    If CStr(lstRB.SelectedItem) = "" Then
    '        If (Windows.Forms.Control.ModifierKeys And Keys.Control) <> 0 Then
    '            lstRB.Items(lstRB.SelectedIndex) = "))"
    '        Else
    '            lstRB.Items(lstRB.SelectedIndex) = ") "
    '        End If
    '    Else
    '        lstRB.Items(lstRB.SelectedIndex) = ""
    '    End If
    '    UpdateBracketSequence()
    'End Sub

    private void lst_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private ListBox lst = CType(sender, ListBox);
        private int iSelectedIndex = lst.IndexFromPoint(e.X, e.Y);

        if (sender Is lstLB || sender Is lstRB || sender Is lstAndOr)
        {
            bSyncing = True
            lst.ClearSelected();
            'lst.Refresh()
            For Each i As Integer In lstSummary.SelectedIndices
                lst.SetSelected(i, true);
            Next;
            'If iSelectedIndex > -1 Then lst.SetSelected(iSelectedIndex, Not lst.GetSelected(iSelectedIndex))
            bSyncing = False
            'SyncLists(lstSummary)
            bBlockMouseEvent = True
            Exit Sub;
        }

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
                if (sender Is lstLB || sender Is lstRB)
                {
                    bSyncing = True
                    lst.SetSelected(iSelectedIndex, ! lst.GetSelected(iSelectedIndex));
                    bSyncing = False
                    Exit Sub ' Interferes with (( ));
                }
            Else
                lst.SelectedIndex = iSelectedIndex;
            }
            SyncLists(lst);
        }

    }


    'Private Sub tmrMouse_Tick(sender As Object, e As EventArgs) Handles tmrMouse.Tick
    '    tmrMouse.Stop()
    '    Application.DoEvents()
    '    If tmrList IsNot Nothing Then SyncLists(tmrList)
    'End Sub


    private bool bSyncing = false;
    private void SyncLists(ListBox lstSource)
    {

        If bSyncing Then Exit Sub
        bSyncing = True

        private New List<int> lstUnselectedIndices;
        for (int i = 0; i <= lstSource.Items.Count - 1; i++)
        {
            lstUnselectedIndices.Add(i);
        Next;
        For Each i As Integer In lstSource.SelectedIndices
            lstUnselectedIndices.Remove(i);
            If ! lstLB.GetSelected(i) Then lstLB.SetSelected(i, true)
            If ! lstSummary.GetSelected(i) Then lstSummary.SetSelected(i, true)
            If ! lstRB.GetSelected(i) Then lstRB.SetSelected(i, true)
            If ! lstAndOr.GetSelected(i) Then lstAndOr.SetSelected(i, true)
        Next;
        For Each i As Integer In lstUnselectedIndices
            lstLB.SetSelected(i, false);
            lstSummary.SetSelected(i, false);
            lstRB.SetSelected(i, false);
            lstAndOr.SetSelected(i, false);
        Next;

        bSyncing = False

        'For i As Integer = 0 To lstSource.Items.Count - 1
        '    lstLB.se()
        'Next
        'Dim iIndex As Integer = lstSource.SelectedIndex

        'lstLB.SelectedIndex = iIndex
        'lstSummary.SelectedIndex = iIndex
        'lstRB.SelectedIndex = iIndex
        'lstAndOr.SelectedIndex = iIndex

        DoButtons()

    }



    private void UpdateBracketSequence()
    {

        private string sBS = "";

        for (int i = 0; i <= lstSummary.Items.Count - 1; i++)
        {
            sBS &= LineBracket(i);
        Next;

        arlRestrictions.BracketSequence = sLeft(sBS, Len(sBS) - 1);

    }


    private string LineBracket(int iRow)
    {
        LineBracket = ""
        If CStr(lstLB.Items(iRow)) = " (" Then LineBracket &= "("
        If CStr(lstLB.Items(iRow)) = "((" Then LineBracket &= "["
        LineBracket &= "#";
        If CStr(lstRB.Items(iRow)) = ") " Then LineBracket &= ")"
        If CStr(lstRB.Items(iRow)) = "))" Then LineBracket &= "]"
        If CStr(lstAndOr.Items(iRow)) = "AND" Then LineBracket &= "A" Else LineBracket &= "O"
    }

    private void lstAndOr_Click(object sender, System.EventArgs e)
    {

        private int iSelectedIndex = lstAndOr.IndexFromPoint(lstAndOr.PointToClient(MousePosition));

        if (iSelectedIndex > -1 && iSelectedIndex < lstAndOr.Items.Count - 1)
        {
            if (CStr(lstAndOr.Items(iSelectedIndex)) = "OR")
            {
                lstAndOr.Items(iSelectedIndex) = "AND";
            Else
                lstAndOr.Items(iSelectedIndex) = "OR";
            }
            UpdateBracketSequence();
        }

    }


    private void DoButtons()
    {

        if (lstSummary.SelectedItems.Count = 1)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            If lstSummary.SelectedIndex < 1 Then btnUp.Enabled = false Else btnUp.Enabled = true
            If lstSummary.SelectedIndex < 0 || lstSummary.SelectedIndex = lstSummary.Items.Count - 1 Then btnDown.Enabled = false Else btnDown.Enabled = true
            miCopy.Enabled = true;
        ElseIf lstSummary.SelectedItems.Count > 1 Then
            btnEdit.Enabled = false;
            btnDelete.Enabled = true;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            If lstSummary.Items.Count > 0 && ! lstSummary.GetSelected(0) Then btnUp.Enabled = true Else btnUp.Enabled = false
            If lstSummary.Items.Count > 0 && ! lstSummary.GetSelected(lstSummary.Items.Count - 1) Then btnDown.Enabled = true Else btnDown.Enabled = false
            miCopy.Enabled = true;
        Else
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            miCopy.Enabled = false;
        }

    }

    private void btnDelete_Click(System.Object sender, System.EventArgs e)
    {

        for (int iIndex = lstSummary.Items.Count - 1; iIndex <= 0; iIndex += -1)
        {
            if (lstSummary.GetSelected(iIndex))
            {
                arlRestrictions.Remove((clsRestriction)(lstSummary.Items(iIndex)));

                lstLB.Items.RemoveAt(iIndex);
                lstSummary.Items.RemoveAt(iIndex);
                lstRB.Items.RemoveAt(iIndex);
                lstAndOr.Items.RemoveAt(iIndex);
            }
        Next;
        'Dim iIndex As Integer = lstSummary.SelectedIndex

        DoHeight()
        DoButtons()
        UpdateBracketSequence();

    }



    private void btnUp_Click(System.Object sender, System.EventArgs e)
    {

        private int iUpDown = CInt(IIf(sender Is btnUp, -1, 1));

        bSyncing = True

        private New List<int> lstSelected;
        For Each i As Integer In lstSummary.SelectedIndices
            lstSelected.Add(i);
        Next;

        for (int i = CInt(If(iUpDown = -1, 0, lstSummary.Items.Count - 1)); i <= CInt(If(iUpDown = -1, lstSummary.Items.Count - 1, 0)); i += -iUpDown)
        {
            if (lstSummary.GetSelected(i))
            {
                private clsRestriction temprest;

                temprest = CType(lstSummary.Items(i), clsRestriction)
                lstSummary.Items(i) = lstSummary.Items(i + iUpDown);
                lstSummary.Items(i + iUpDown) = temprest;

                private string sTempBrac = lstLB.Items(i).ToString;
                lstLB.Items(i) = lstLB.Items(i + iUpDown);
                lstLB.Items(i + iUpDown) = sTempBrac;
                sTempBrac = lstRB.Items(i).ToString
                lstRB.Items(i) = lstRB.Items(i + iUpDown);
                lstRB.Items(i + iUpDown) = sTempBrac;

                if (Not ((i = lstSummary.Items.Count - 2 && iUpDown = 1) || (i = lstSummary.Items.Count - 1 && iUpDown = -1)))
                {
                    private string sTempAndOr = lstAndOr.Items(i).ToString;
                    lstAndOr.Items(i) = lstAndOr.Items(i + iUpDown);
                    lstAndOr.Items(i + iUpDown) = sTempAndOr;
                }

                arlRestrictions(i) = (clsRestriction)(lstSummary.Items(i));
                arlRestrictions(i + iUpDown) = (clsRestriction)(lstSummary.Items(i + iUpDown));

            }

            'If i - iUpDown > -1 AndAlso i - iUpDown < lstSummary.Items.Count Then
            '    lstSummary.SetSelected(i, lstSummary.GetSelected(i - iUpDown))
            'Else
            '    lstSummary.SetSelected(i, False)
            'End If
        Next;

        lstLB.ClearSelected();
        lstSummary.ClearSelected();
        lstRB.ClearSelected();
        lstAndOr.ClearSelected();
        For Each i As Integer In lstSelected
            lstLB.SetSelected(i + iUpDown, true);
            lstSummary.SetSelected(i + iUpDown, true);
            lstRB.SetSelected(i + iUpDown, true);
            lstAndOr.SetSelected(i + iUpDown, true);
        Next;
        'lstLB.SelectedIndex = i + iUpDown
        'lstSummary.SelectedIndex = i + iUpDown
        'lstRB.SelectedIndex = i + iUpDown
        'lstAndOr.SelectedIndex = i + iUpDown

        bSyncing = False

        DoButtons()
        UpdateBracketSequence();

    }

    'Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

    '    Dim temprest As clsRestriction
    '    Dim i As Integer = lstSummary.SelectedIndex

    '    temprest = CType(lstSummary.Items(i), clsRestriction)
    '    lstSummary.Items(i) = lstSummary.Items(i + 1)
    '    lstSummary.Items(i + 1) = temprest

    '    Dim sTempBrac As String = lstLB.Items(i).ToString
    '    lstLB.Items(i) = lstLB.Items(i + 1)
    '    lstLB.Items(i + 1) = sTempBrac
    '    sTempBrac = lstRB.Items(i).ToString
    '    lstRB.Items(i) = lstRB.Items(i + 1)
    '    lstRB.Items(i + 1) = sTempBrac

    '    arlRestrictions(i) = CType(lstSummary.Items(i), clsRestriction)
    '    arlRestrictions(i + 1) = CType(lstSummary.Items(i + 1), clsRestriction)

    '    lstLB.SelectedIndex = i + 1
    '    lstSummary.SelectedIndex = i + 1
    '    lstRB.SelectedIndex = i + 1
    '    lstAndOr.SelectedIndex = i + 1
    '    DoButtons()
    '    UpdateBracketSequence()

    'End Sub

    private void btnEdit_Click(object sender, System.EventArgs e)
    {
        Edit();
    }

    private void Edit()
    {

        private clsRestriction rest = arlRestrictions(Me.lstSummary.SelectedIndex).Copy;

        if (EditRestriction(rest) = DialogResult.OK)
        {

            arlRestrictions(lstSummary.SelectedIndex) = rest;
            lstSummary.Items(lstSummary.SelectedIndex) = rest;

            DoHeight()
            UpdateBracketSequence();
        }

    }


    private void lstSummary_DoubleClick(object sender, System.EventArgs e)
    {
        If lstSummary.SelectedIndex > -1 Then Edit()
    }


    private bool bBlockMouseEvent = false;
    private void lstSummary_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        If bSyncing Then Exit Sub

        private ListBox listChanged = CType(sender, ListBox);
        ''For Each ctrl As ListBox In New ListBox() {lstSummary, lstAndOr, lstLB, lstRB}
        ''    If ctrl IsNot listChanged Then ctrl.SelectedIndex = listChanged.SelectedIndex
        ''Next
        ''DoButtons()
        'If ModifierKeys = Keys.Control AndAlso

        if (bBlockMouseEvent && (sender Is lstLB || sender Is lstRB || sender Is lstAndOr))
        {
            bBlockMouseEvent = False
            Exit Sub;
        }

        SyncLists(listChanged);

    }

    private void cmsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        miCopy.Enabled = lstSummary.SelectedIndex > -1;
        miPaste.Enabled = xmlRestrictions != null;
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

        private New RestrictionArrayList copyRestrictions;
        private New IO.MemoryStream ms;
        private New Xml.XmlTextWriter(ms, System.Text.Encoding.UTF8) xml;
        xml.Formatting = System.Xml.Formatting.Indented;
        xml.WriteStartDocument();
        xml.WriteStartElement("Restrictions");

        for (int i = 0; i <= lstSummary.Items.Count - 1; i++)
        {
            if (lstSummary.GetSelected(i))
            {
                copyRestrictions.Add((clsRestriction)(lstSummary.Items(i)).Copy);
                copyRestrictions.BracketSequence &= LineBracket(i);
            }
        Next;
        copyRestrictions.BracketSequence = sLeft(copyRestrictions.BracketSequence, copyRestrictions.BracketSequence.Length - 1);
        SaveRestrictions(xml, copyRestrictions);
        xml.WriteEndElement();
        xml.WriteEndDocument();
        xml.Flush();

        xmlRestrictions = ms.ToArray

    }

    private void miPaste_Click(object sender, EventArgs e)
    {

        private New IO.MemoryStream(xmlRestrictions) ms;
        private New Xml.XmlDocument xmlDoc;
        xmlDoc.Load(ms);
        private RestrictionArrayList pasteRestrictions = FileIO.LoadRestrictions(xmlDoc.DocumentElement);
        private int iStart = lstSummary.Items.Count;
        LoadGrid(pasteRestrictions);

        lstSummary.SelectedItems.Clear();
        for (int i = iStart; i <= lstSummary.Items.Count - 1; i++)
        {
            lstSummary.SelectedIndices.Add(i);
        Next;
        SyncLists(lstSummary);

    }

}

}