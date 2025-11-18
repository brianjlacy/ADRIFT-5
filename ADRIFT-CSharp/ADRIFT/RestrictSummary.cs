using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class RestrictSummary
{
    Inherits System.Windows.Forms.UserControl;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents UltraPopupControlContainer1 As Infragistics.Win.Misc.UltraPopupControlContainer;
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip;
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents cmbRestrictions As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.components = New System.ComponentModel.Container;
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance;
        Me.UltraPopupControlContainer1 = New Infragistics.Win.Misc.UltraPopupControlContainer(Me.components);
        Me.cmbRestrictions = New Infragistics.Win.UltraWinEditors.UltraComboEditor;
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton;
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton;
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components);
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictions).BeginInit();
        Me.SuspendLayout();
        '
        'UltraPopupControlContainer1
        '
        Me.UltraPopupControlContainer1.PreferredDropDownSize = New System.Drawing.Size(0, 0);
        '
        'cmbRestrictions
        '
        Me.cmbRestrictions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbRestrictions.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbRestrictions.Location = New System.Drawing.Point(0, 0);
        Me.cmbRestrictions.Name = "cmbRestrictions";
        Me.cmbRestrictions.Size = New System.Drawing.Size(200, 21);
        Me.cmbRestrictions.TabIndex = 2;
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgEdit16;
        Me.btnEdit.Appearance = Appearance3;
        Me.btnEdit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Black;
        Me.btnEdit.Location = New System.Drawing.Point(198, -1);
        Me.btnEdit.Name = "btnEdit";
        Me.btnEdit.Size = New System.Drawing.Size(21, 22);
        Me.btnEdit.TabIndex = 5;
        Me.ToolTip1.SetToolTip(Me.btnEdit, "Edit restrictions");
        Me.btnEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Appearance4.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.btnAdd.Appearance = Appearance4;
        Me.btnAdd.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Black;
        Me.btnAdd.Location = New System.Drawing.Point(198, -1);
        Me.btnAdd.Name = "btnAdd";
        Me.btnAdd.Size = New System.Drawing.Size(21, 22);
        Me.btnAdd.TabIndex = 6;
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add a new restriction");
        Me.btnAdd.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'RestrictSummary
        '
        Me.BackColor = System.Drawing.Color.Transparent;
        Me.Controls.Add(Me.cmbRestrictions);
        Me.Controls.Add(Me.btnEdit);
        Me.Controls.Add(Me.btnAdd);
        Me.Name = "RestrictSummary";
        Me.Size = New System.Drawing.Size(218, 21);
        (System.ComponentModel.ISupportInitialize)(Me.cmbRestrictions).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }

#End Region

    Public Event RestrictionsChanged()

    internal List<clsUserFunction.Argument> Arguments { get; set; }
    internal New RestrictionArrayList arlRestrictions;
    public string sCommand;

    internal void LoadRestrictions(RestrictionArrayList arlRest)
    {
        Me.arlRestrictions = arlRest;

        cmbRestrictions.Items.Clear();

        if (arlRestrictions.Count = 0)
        {
            btnEdit.Visible = false;
            btnAdd.Visible = true;
            cmbRestrictions.Items.Add("<No Restrictions Defined>");
        Else
            btnEdit.Visible = true;
            btnAdd.Visible = false;
            For Each rest As clsRestriction In arlRestrictions
                cmbRestrictions.Items.Add(rest.Summary);
            Next;
        }
        cmbRestrictions.SelectedIndex = 0;
        if (arlRestrictions.Count > 1)
        {
            cmbRestrictions.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always;
        Else
            cmbRestrictions.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never;
        }

    }


    private DialogResult EditRestriction(ref rest As clsRestriction)
    {

        private New frmRestriction fRestriction;

        'fRestriction.Restriction = rest.Copy
        fRestriction.sCommand = Me.sCommand;
        fRestriction.Arguments = Me.Arguments;
        fRestriction.LoadRestriction(rest.Copy);
        If AreWeOnGenTextBox() Then fRestriction.txtMessage.Enabled = false
        If fRestriction.ShowDialog = DialogResult.OK Then rest = fRestriction.Restriction

        return fRestriction.DialogResult;

    }


    private bool AreWeOnGenTextBox()
    {
        if (Me.Parent IsNot null && Me.Parent.Parent IsNot null && Me.Parent.Parent.Parent IsNot null && Me.Parent.Parent.Parent.Parent IsNot null && Me.Parent.Parent.Parent.Parent.Parent IsNot null)
        {
            return TypeOf Me.Parent.Parent.Parent.Parent.Parent Is GenTextbox || TypeOf Me.Parent.Parent.Parent.Parent.Parent Is GenericProperty;
        }
        return false;
    }

    private void btnEdit_Click(System.Object sender, System.EventArgs e)
    {

        if (arlRestrictions.Count > 0)
        {
            ' Show List
            private New frmRestrictions(arlRestrictions.Copy) fRestrictions;
            fRestrictions.RestrictDetails1.sCommand = Me.sCommand;
            fRestrictions.RestrictDetails1.Arguments = Me.Arguments;
            If AreWeOnGenTextBox() Then fRestrictions.RestrictDetails1.bNoElseText = true
            fRestrictions.ShowDialog();

            if (fRestrictions.DialogResult = DialogResult.OK)
            {
                LoadRestrictions(fRestrictions.RestrictDetails1.arlRestrictions);
                If TypeOf Me.Parent.Parent.Parent == frmLocation Then (frmLocation)(Me.Parent.Parent.Parent).Changed = true
            }
        Else
            ' Just simply add a restriction
            private New clsRestriction rest;

            if (EditRestriction(rest) = DialogResult.OK)
            {
                arlRestrictions.Add(rest);
                arlRestrictions.BracketSequence = "#";
                LoadRestrictions(arlRestrictions);
                If TypeOf Me.Parent.Parent.Parent == frmLocation Then (frmLocation)(Me.Parent.Parent.Parent).Changed = true
            }
        }

        if (arlRestrictions.Count > 1)
        {
            cmbRestrictions.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always;
        Else
            cmbRestrictions.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never;
        }

        RaiseEvent RestrictionsChanged();

    }


    'Private Sub RestrictSummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    '    If cmbRestrictions.Width > 300 Then cmbRestrictions.DropDownListWidth = cmbRestrictions.Width Else cmbRestrictions.DropDownListWidth = 300
    'End Sub


    private void cmbRestrictions_BeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e)
    {

        try
        {
            if (cmbRestrictions.Items.Count <= 1)
            {
                e.Cancel = true;
                Exit Sub;
            }

            private Graphics g = CreateGraphics();
            private float iMaxLength;

            For Each vli As Infragistics.Win.ValueListItem In cmbRestrictions.Items
                iMaxLength = Math.Max(iMaxLength, g.MeasureString(vli.DisplayText, cmbRestrictions.Font).Width)
            Next;
            If iMaxLength > cmbRestrictions.Width Then cmbRestrictions.DropDownListWidth = CInt(iMaxLength) + 10 Else cmbRestrictions.DropDownListWidth = 0

        }
        catch (Exception ex)
        {
            ErrMsg("BeforeDropDown error", ex);
        }

    }

}

}