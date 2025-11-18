using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class Expression
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
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        Me.txtExpression = New ADRIFT.OOTextbox();
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton();
        Me.UltraTextEditor1 = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        (System.ComponentModel.ISupportInitialize)(Me.UltraTextEditor1).BeginInit();
        Me.SuspendLayout();
        '
        'txtExpression
        '
        Me.txtExpression.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtExpression.BorderStyle = Infragistics.Win.UIElementBorderStyle.None ' System.Windows.Forms.BorderStyle.None;
        Me.txtExpression.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtExpression.Location = New System.Drawing.Point(3, 3);
        Me.txtExpression.Multiline = false;
        Me.txtExpression.Name = "txtExpression";
        Me.txtExpression.Size = New System.Drawing.Size(202, 17);
        Me.txtExpression.TabIndex = 0;
        Me.txtExpression.Text = "";
        Me.txtExpression.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right;
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Appearance3.Image = Global.ADRIFT.My.Resources.Resources.imgAdd16;
        Me.btnEdit.Appearance = Appearance3;
        Me.btnEdit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Black;
        Me.btnEdit.Location = New System.Drawing.Point(210, 0);
        Me.btnEdit.Name = "btnEdit";
        Me.btnEdit.ShowFocusRect = false;
        Me.btnEdit.Size = New System.Drawing.Size(20, 22);
        Me.btnEdit.TabIndex = 5;
        Me.btnEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.[false];
        '
        'UltraTextEditor1
        '
        Me.UltraTextEditor1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance1.BackColorDisabled = System.Drawing.SystemColors.Window;
        Me.UltraTextEditor1.Appearance = Appearance1;
        Me.UltraTextEditor1.AutoSize = false;
        Me.UltraTextEditor1.Enabled = false;
        Me.UltraTextEditor1.Location = New System.Drawing.Point(0, 0);
        Me.UltraTextEditor1.Name = "UltraTextEditor1";
        Me.UltraTextEditor1.Size = New System.Drawing.Size(208, 21);
        Me.UltraTextEditor1.TabIndex = 6;
        Me.UltraTextEditor1.Text = "UltraTextEditor1";
        '
        'Expression
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.BackColor = System.Drawing.Color.Transparent;
        Me.Controls.Add(Me.txtExpression);
        Me.Controls.Add(Me.UltraTextEditor1);
        Me.Controls.Add(Me.btnEdit);
        Me.Name = "Expression";
        Me.Size = New System.Drawing.Size(227, 21);
        (System.ComponentModel.ISupportInitialize)(Me.UltraTextEditor1).EndInit();
        Me.ResumeLayout(false);

    }
    Friend WithEvents txtExpression As OOTextbox ' Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraTextEditor1 As Infragistics.Win.UltraWinEditors.UltraTextEditor;

}

}