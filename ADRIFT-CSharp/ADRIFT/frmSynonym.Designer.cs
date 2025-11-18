using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmSynonym
{
    Inherits System.Windows.Forms.Form;

    'Form overrides dispose to clean up the component list.
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
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.lblFrom = New Infragistics.Win.Misc.UltraLabel();
        Me.txtTo = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.lblTo = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbFrom = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtTo).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbFrom).BeginInit();
        Me.SuspendLayout();
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(286, 145);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 5;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(190, 145);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 4;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(94, 145);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 3;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 139);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(386, 48);
        Me.UltraStatusBar1.TabIndex = 19;
        '
        'lblFrom
        '
        Me.lblFrom.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblFrom.Location = New System.Drawing.Point(11, 12);
        Me.lblFrom.Name = "lblFrom";
        Me.lblFrom.Size = New System.Drawing.Size(284, 16);
        Me.lblFrom.TabIndex = 23;
        Me.lblFrom.Text = "Replace any user input containing any of the following:";
        '
        'txtTo
        '
        Me.txtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtTo.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtTo.Location = New System.Drawing.Point(11, 96);
        Me.txtTo.Name = "txtTo";
        Me.txtTo.Size = New System.Drawing.Size(362, 24);
        Me.txtTo.TabIndex = 2;
        '
        'lblTo
        '
        Me.lblTo.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblTo.Location = New System.Drawing.Point(10, 74);
        Me.lblTo.Name = "lblTo";
        Me.lblTo.Size = New System.Drawing.Size(67, 16);
        Me.lblTo.TabIndex = 25;
        Me.lblTo.Text = "with this:";
        '
        'cmbFrom
        '
        Me.cmbFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbFrom.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.cmbFrom.Location = New System.Drawing.Point(11, 34);
        Me.cmbFrom.Name = "cmbFrom";
        Me.cmbFrom.Size = New System.Drawing.Size(363, 24);
        Me.cmbFrom.TabIndex = 1;
        '
        'frmSynonym
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.ClientSize = New System.Drawing.Size(386, 187);
        Me.Controls.Add(Me.cmbFrom);
        Me.Controls.Add(Me.txtTo);
        Me.Controls.Add(Me.lblTo);
        Me.Controls.Add(Me.lblFrom);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MaximumSize = New System.Drawing.Size(1024, 225);
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(324, 225);
        Me.Name = "frmSynonym";
        Me.Text = "Synonym";
        Me.AcceptButton = btnOK;
        Me.CancelButton = btnCancel;
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtTo).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbFrom).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents lblFrom As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtTo As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents lblTo As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbFrom As Infragistics.Win.UltraWinEditors.UltraComboEditor;
}

}