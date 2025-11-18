using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class ItemSelector
{
    Inherits System.Windows.Forms.UserControl;

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _;
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && components IsNot null)
            {
                components.Dispose();
            }
        }
        finally
        {
            MyBase.Dispose(disposing);
        }
    }

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _;
    private void InitializeComponent()
    {
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        Me.btnItemType = New Infragistics.Win.Misc.UltraButton();
        Me.btnNew = New Infragistics.Win.Misc.UltraButton();
        Me.cmbList = New AutoCompleteCombo;
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton();
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components);
        Me.Expression = New ADRIFT.Expression();
        (System.ComponentModel.ISupportInitialize)(Me.cmbList).BeginInit();
        Me.SuspendLayout();
        '
        'btnItemType
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Appearance1.Image = Global.ADRIFT.My.Resources.Resources.imgLocation16;
        Me.btnItemType.Appearance = Appearance1;
        Me.btnItemType.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnItemType.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnItemType.Location = New System.Drawing.Point(0, -1);
        Me.btnItemType.Name = "btnItemType";
        Me.btnItemType.Size = New System.Drawing.Size(22, 22);
        Me.btnItemType.TabIndex = 1;
        Me.ToolTip1.SetToolTip(Me.btnItemType, "Change list type");
        Me.btnItemType.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'btnNew
        '
        Me.btnNew.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Appearance2.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.btnNew.Appearance = Appearance2;
        Me.btnNew.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnNew.Location = New System.Drawing.Point(188, 0);
        Me.btnNew.Name = "btnNew";
        Me.btnNew.Size = New System.Drawing.Size(22, 22);
        Me.btnNew.TabIndex = 2;
        Me.btnNew.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'cmbList
        '
        Me.cmbList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbList.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
        Me.cmbList.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
        Me.cmbList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic);
        Me.cmbList.Location = New System.Drawing.Point(21, 0);
        Me.cmbList.Name = "cmbList";
        Me.cmbList.Size = New System.Drawing.Size(169, 21);
        Me.cmbList.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbList.TabIndex = 3;
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgEdit16;
        Me.btnEdit.Appearance = Appearance3;
        Me.btnEdit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Black;
        Me.btnEdit.Location = New System.Drawing.Point(188, -1);
        Me.btnEdit.Name = "btnEdit";
        Me.btnEdit.Size = New System.Drawing.Size(22, 22);
        Me.btnEdit.TabIndex = 4;
        Me.btnEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        Me.btnEdit.Visible = false;
        '
        'Expression
        '
        Me.Expression.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Expression.BackColor = System.Drawing.Color.Transparent;
        Me.Expression.Location = New System.Drawing.Point(21, 0);
        Me.Expression.Name = "Expression";
        Me.Expression.Size = New System.Drawing.Size(186, 21);
        Me.Expression.TabIndex = 5;
        Me.Expression.Value = "";
        Me.Expression.Visible = false;
        '
        'ItemSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.BackColor = System.Drawing.Color.Transparent;
        Me.Controls.Add(Me.cmbList);
        Me.Controls.Add(Me.btnEdit);
        Me.Controls.Add(Me.btnNew);
        Me.Controls.Add(Me.Expression);
        Me.Controls.Add(Me.btnItemType);
        Me.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.MinimumSize = New System.Drawing.Size(10, 21);
        Me.Name = "ItemSelector";
        Me.Size = New System.Drawing.Size(208, 21);
        (System.ComponentModel.ISupportInitialize)(Me.cmbList).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents btnItemType As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnNew As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents cmbList As AutoCompleteCombo;
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip;

    public void New()
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        ' Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

    }
    Friend WithEvents Expression As ADRIFT.Expression;
}

}