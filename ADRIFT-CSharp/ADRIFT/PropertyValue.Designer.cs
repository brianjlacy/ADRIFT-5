using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class PropertyValue
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
        Me.ExpressionOrVariable = New ADRIFT.ItemSelector();
        Me.cmbList = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        (System.ComponentModel.ISupportInitialize)(Me.cmbList).BeginInit();
        Me.SuspendLayout();
        '
        'Expression
        '
        Me.ExpressionOrVariable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.ExpressionOrVariable.BackColor = System.Drawing.Color.Transparent;
        Me.ExpressionOrVariable.Location = New System.Drawing.Point(0, 0);
        Me.ExpressionOrVariable.Name = "Expression";
        Me.ExpressionOrVariable.Size = New System.Drawing.Size(185, 21);
        Me.ExpressionOrVariable.TabIndex = 6;
        Me.ExpressionOrVariable.Visible = false;
        Me.ExpressionOrVariable.AllowedListType = ItemSelector.ItemEnum.Expression;
        '
        'cmbList
        '
        Me.cmbList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbList.AutoSize = false;
        Me.cmbList.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        Me.cmbList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbList.Location = New System.Drawing.Point(0, 0);
        Me.cmbList.Name = "cmbList";
        Me.cmbList.Size = New System.Drawing.Size(187, 21);
        Me.cmbList.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;
        Me.cmbList.TabIndex = 7;
        '
        'PropertyValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.ExpressionOrVariable);
        Me.Controls.Add(Me.cmbList);
        Me.Name = "PropertyValue";
        Me.Size = New System.Drawing.Size(187, 21);
        (System.ComponentModel.ISupportInitialize)(Me.cmbList).EndInit();
        Me.ResumeLayout(false);

    }
    Friend WithEvents ExpressionOrVariable As ADRIFT.ItemSelector;
    Friend WithEvents cmbList As Infragistics.Win.UltraWinEditors.UltraComboEditor;

}

}