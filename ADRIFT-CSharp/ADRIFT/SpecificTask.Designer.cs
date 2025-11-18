using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class SpecificTask
{
    Inherits System.Windows.Forms.UserControl;

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components IsNot null)
        {
            components.Dispose();
        }
        MyBase.Dispose(disposing);
    }

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _;
    private void InitializeComponent()
    {
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance;
        Me.txtSpecific = New System.Windows.Forms.RichTextBox;
        Me.BlueBorder = New Infragistics.Win.UltraWinEditors.UltraTextEditor;
        (System.ComponentModel.ISupportInitialize)(Me.BlueBorder).BeginInit();
        Me.SuspendLayout();
        '
        'txtSpecific
        '
        Me.txtSpecific.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtSpecific.BackColor = System.Drawing.SystemColors.ControlLightLight;
        Me.txtSpecific.BorderStyle = System.Windows.Forms.BorderStyle.None;
        Me.txtSpecific.Cursor = System.Windows.Forms.Cursors.Arrow;
        Me.txtSpecific.DetectUrls = false;
        Me.txtSpecific.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtSpecific.Location = New System.Drawing.Point(2, 2);
        Me.txtSpecific.Multiline = false;
        Me.txtSpecific.Name = "txtSpecific";
        Me.txtSpecific.ReadOnly = true;
        Me.txtSpecific.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
        Me.txtSpecific.Size = New System.Drawing.Size(296, 20);
        Me.txtSpecific.TabIndex = 17;
        Me.txtSpecific.Text = "";
        Me.txtSpecific.WordWrap = false;
        '
        'BlueBorder
        '
        Me.BlueBorder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance2.BackColor = System.Drawing.SystemColors.ControlLightLight;
        Appearance2.Cursor = System.Windows.Forms.Cursors.Arrow;
        Me.BlueBorder.Appearance = Appearance2;
        Me.BlueBorder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.BlueBorder.Location = New System.Drawing.Point(0, 0);
        Me.BlueBorder.Name = "BlueBorder";
        Me.BlueBorder.Size = New System.Drawing.Size(300, 24);
        Me.BlueBorder.TabIndex = 18;
        Me.BlueBorder.Text = "UltraTextEditor1";
        '
        'SpecificTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.txtSpecific);
        Me.Controls.Add(Me.BlueBorder);
        Me.Name = "SpecificTask";
        Me.Size = New System.Drawing.Size(300, 24);
        (System.ComponentModel.ISupportInitialize)(Me.BlueBorder).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents txtSpecific As System.Windows.Forms.RichTextBox;
    Friend WithEvents BlueBorder As Infragistics.Win.UltraWinEditors.UltraTextEditor;

}

}