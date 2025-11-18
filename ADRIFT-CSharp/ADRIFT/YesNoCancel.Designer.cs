using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmYesNoCancel
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
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmYesNoCancel));
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel;
        Me.btnNo = New System.Windows.Forms.Button;
        Me.btnYes = New System.Windows.Forms.Button;
        Me.btnCancel = New System.Windows.Forms.Button;
        Me.lblText = New Infragistics.Win.Misc.UltraLabel;
        Me.chkRemember = New Infragistics.Win.UltraWinEditors.UltraCheckEditor;
        Me.PictureBox1 = New System.Windows.Forms.PictureBox;
        Me.TableLayoutPanel1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.PictureBox1).BeginInit();
        Me.SuspendLayout();
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.TableLayoutPanel1.ColumnCount = 3;
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!));
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!));
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!));
        Me.TableLayoutPanel1.Controls.Add(Me.btnNo, 0, 0);
        Me.TableLayoutPanel1.Controls.Add(Me.btnYes, 0, 0);
        Me.TableLayoutPanel1.Controls.Add(Me.btnCancel, 2, 0);
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(192, 80);
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1";
        Me.TableLayoutPanel1.RowCount = 1;
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!));
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(214, 29);
        Me.TableLayoutPanel1.TabIndex = 0;
        '
        'btnNo
        '
        Me.btnNo.Anchor = System.Windows.Forms.AnchorStyles.None;
        Me.btnNo.Location = New System.Drawing.Point(74, 3);
        Me.btnNo.Name = "btnNo";
        Me.btnNo.Size = New System.Drawing.Size(65, 23);
        Me.btnNo.TabIndex = 2;
        Me.btnNo.Text = "No";
        '
        'btnYes
        '
        Me.btnYes.Anchor = System.Windows.Forms.AnchorStyles.None;
        Me.btnYes.Location = New System.Drawing.Point(3, 3);
        Me.btnYes.Name = "btnYes";
        Me.btnYes.Size = New System.Drawing.Size(65, 23);
        Me.btnYes.TabIndex = 0;
        Me.btnYes.Text = "Yes";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(145, 3);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(66, 23);
        Me.btnCancel.TabIndex = 1;
        Me.btnCancel.Text = "Cancel";
        '
        'lblText
        '
        Appearance1.TextVAlignAsString = "Middle";
        Me.lblText.Appearance = Appearance1;
        Me.lblText.Location = New System.Drawing.Point(66, 10);
        Me.lblText.Name = "lblText";
        Me.lblText.Size = New System.Drawing.Size(340, 61);
        Me.lblText.TabIndex = 1;
        Me.lblText.Text = "UltraLabel1";
        '
        'chkRemember
        '
        Me.chkRemember.Location = New System.Drawing.Point(12, 83);
        Me.chkRemember.Name = "chkRemember";
        Me.chkRemember.Size = New System.Drawing.Size(151, 20);
        Me.chkRemember.TabIndex = 2;
        Me.chkRemember.Text = "Remember this setting";
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = (System.Drawing.Image)(resources.GetObject("PictureBox1.Image"));
        Me.PictureBox1.Location = New System.Drawing.Point(20, 25);
        Me.PictureBox1.Name = "PictureBox1";
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32);
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        Me.PictureBox1.TabIndex = 3;
        Me.PictureBox1.TabStop = false;
        '
        'frmYesNoCancel
        '
        Me.AcceptButton = Me.btnYes;
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(418, 121);
        Me.Controls.Add(Me.PictureBox1);
        Me.Controls.Add(Me.chkRemember);
        Me.Controls.Add(Me.lblText);
        Me.Controls.Add(Me.TableLayoutPanel1);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "frmYesNoCancel";
        Me.ShowInTaskbar = false;
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Me.Text = "YesNoCancel";
        Me.TableLayoutPanel1.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.PictureBox1).EndInit();
        Me.ResumeLayout(false);

    }
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel;
    Friend WithEvents btnYes As System.Windows.Forms.Button;
    Friend WithEvents btnCancel As System.Windows.Forms.Button;
    Friend WithEvents btnNo As System.Windows.Forms.Button;
    Friend WithEvents lblText As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents chkRemember As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox;

}

}