using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class AboutBox
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

    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox;
    Friend WithEvents LabelVersion As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblRegistered As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents OKButton As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents LabelCopyright As Infragistics.Win.Misc.UltraLabel;

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _;
    private void InitializeComponent()
    {
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox();
        Me.LabelVersion = New Infragistics.Win.Misc.UltraLabel();
        Me.LabelCopyright = New Infragistics.Win.Misc.UltraLabel();
        Me.lblRegistered = New Infragistics.Win.Misc.UltraLabel();
        Me.OKButton = New Infragistics.Win.Misc.UltraButton();
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox();
        Me.btnDonate = New Infragistics.Win.Misc.UltraButton();
        Me.lblInfo = New Infragistics.Win.Misc.UltraLabel();
        (System.ComponentModel.ISupportInitialize)(Me.LogoPictureBox).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        Me.SuspendLayout();
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
        Me.LogoPictureBox.Image = Global.ADRIFT.My.Resources.Resources.Adrift;
        Me.LogoPictureBox.Location = New System.Drawing.Point(9, 13);
        Me.LogoPictureBox.Name = "LogoPictureBox";
        Me.LogoPictureBox.Size = New System.Drawing.Size(337, 252);
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        Me.LogoPictureBox.TabIndex = 0;
        Me.LogoPictureBox.TabStop = false;
        '
        'LabelVersion
        '
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Appearance2.TextHAlignAsString = "Left";
        Me.LabelVersion.Appearance = Appearance2;
        Me.LabelVersion.Location = New System.Drawing.Point(356, 38);
        Me.LabelVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0);
        Me.LabelVersion.Name = "LabelVersion";
        Me.LabelVersion.Size = New System.Drawing.Size(314, 17);
        Me.LabelVersion.TabIndex = 0;
        Me.LabelVersion.Text = "Version 5.0.13";
        '
        'LabelCopyright
        '
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Appearance3.TextHAlignAsString = "Left";
        Me.LabelCopyright.Appearance = Appearance3;
        Me.LabelCopyright.Location = New System.Drawing.Point(356, 55);
        Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0);
        Me.LabelCopyright.Name = "LabelCopyright";
        Me.LabelCopyright.Size = New System.Drawing.Size(314, 17);
        Me.LabelCopyright.TabIndex = 0;
        Me.LabelCopyright.Text = "Copyright";
        '
        'lblRegistered
        '
        Appearance5.BackColor = System.Drawing.Color.Transparent;
        Appearance5.TextHAlignAsString = "Left";
        Me.lblRegistered.Appearance = Appearance5;
        Me.lblRegistered.Location = New System.Drawing.Point(356, 84);
        Me.lblRegistered.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0);
        Me.lblRegistered.Name = "lblRegistered";
        Me.lblRegistered.Size = New System.Drawing.Size(314, 17);
        Me.lblRegistered.TabIndex = 0;
        Me.lblRegistered.Text = "Unregistered";
        '
        'OKButton
        '
        Me.OKButton.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.OKButton.Location = New System.Drawing.Point(569, 238);
        Me.OKButton.Name = "OKButton";
        Me.OKButton.Size = New System.Drawing.Size(101, 27);
        Me.OKButton.TabIndex = 0;
        Me.OKButton.Text = "OK";
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.btnDonate);
        Me.UltraGroupBox1.Controls.Add(Me.lblInfo);
        Me.UltraGroupBox1.Controls.Add(Me.OKButton);
        Me.UltraGroupBox1.Controls.Add(Me.LabelCopyright);
        Me.UltraGroupBox1.Controls.Add(Me.LabelVersion);
        Me.UltraGroupBox1.Controls.Add(Me.lblRegistered);
        Me.UltraGroupBox1.Controls.Add(Me.LogoPictureBox);
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(682, 276);
        Me.UltraGroupBox1.TabIndex = 1;
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'btnDonate
        '
        Me.btnDonate.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnDonate.Location = New System.Drawing.Point(462, 238);
        Me.btnDonate.Name = "btnDonate";
        Me.btnDonate.Size = New System.Drawing.Size(101, 27);
        Me.btnDonate.TabIndex = 2;
        Me.btnDonate.Text = "Donate";
        '
        'lblInfo
        '
        Appearance4.BackColor = System.Drawing.Color.Transparent;
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark;
        Appearance4.TextHAlignAsString = "Left";
        Me.lblInfo.Appearance = Appearance4;
        Me.lblInfo.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
        Me.lblInfo.Location = New System.Drawing.Point(355, 106);
        Me.lblInfo.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0);
        Me.lblInfo.Name = "lblInfo";
        Me.lblInfo.Size = New System.Drawing.Size(314, 123);
        Me.lblInfo.TabIndex = 1;
        Me.lblInfo.Text = "Unregistered";
        '
        'AboutBox
        '
        Me.AcceptButton = Me.btnDonate;
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.CancelButton = Me.OKButton;
        Me.ClientSize = New System.Drawing.Size(682, 276);
        Me.Controls.Add(Me.UltraGroupBox1);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "AboutBox";
        Me.ShowInTaskbar = false;
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Me.Text = "About ADRIFT";
        (System.ComponentModel.ISupportInitialize)(Me.LogoPictureBox).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.ResumeLayout(false);

    }
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents lblInfo As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents btnDonate As Infragistics.Win.Misc.UltraButton;

}

}