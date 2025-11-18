using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class clsImage
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
        Me.components = New System.ComponentModel.Container;
        Me.imgGraphics = New System.Windows.Forms.PictureBox;
        Me.tmrLabel = New System.Windows.Forms.Timer(Me.components);
        Me.lblMode = New System.Windows.Forms.Label;
        (System.ComponentModel.ISupportInitialize)(Me.imgGraphics).BeginInit();
        Me.SuspendLayout();
        '
        'imgGraphics
        '
        Me.imgGraphics.BackColor = System.Drawing.Color.Transparent;
        Me.imgGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.imgGraphics.InitialImage = null;
        Me.imgGraphics.Location = New System.Drawing.Point(0, 0);
        Me.imgGraphics.Name = "imgGraphics";
        Me.imgGraphics.Size = New System.Drawing.Size(405, 283);
        Me.imgGraphics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        Me.imgGraphics.TabIndex = 2;
        Me.imgGraphics.TabStop = false;
        '
        'tmrLabel
        '
        Me.tmrLabel.Interval = 1000;
        '
        'lblMode
        '
        Me.lblMode.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.lblMode.AutoSize = true;
        Me.lblMode.BackColor = System.Drawing.Color.Transparent;
        Me.lblMode.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lblMode.ForeColor = System.Drawing.Color.Lime;
        Me.lblMode.Location = New System.Drawing.Point(153, 9);
        Me.lblMode.Name = "lblMode";
        Me.lblMode.Size = New System.Drawing.Size(240, 16);
        Me.lblMode.TabIndex = 4;
        Me.lblMode.Text = "Mode: Fit Window, Keep Aspect";
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        Me.lblMode.Visible = false;
        '
        'Image
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.lblMode);
        Me.Controls.Add(Me.imgGraphics);
        Me.Name = "Image";
        Me.Size = New System.Drawing.Size(405, 283);
        (System.ComponentModel.ISupportInitialize)(Me.imgGraphics).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents imgGraphics As System.Windows.Forms.PictureBox;
    Friend WithEvents tmrLabel As System.Windows.Forms.Timer;
    Friend WithEvents lblMode As System.Windows.Forms.Label;

}

}