using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class Splash
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
        Me.imgAdrift = New System.Windows.Forms.PictureBox();
        Me.Panel1 = New System.Windows.Forms.Panel();
        Me.Label1 = New System.Windows.Forms.Label();
        (System.ComponentModel.ISupportInitialize)(Me.imgAdrift).BeginInit();
        Me.Panel1.SuspendLayout();
        Me.SuspendLayout();
        '
        'imgAdrift
        '
        Me.imgAdrift.Image = Global.ADRIFT.My.Resources.Resources.Adrift;
        Me.imgAdrift.Location = New System.Drawing.Point(3, 3);
        Me.imgAdrift.Name = "imgAdrift";
        Me.imgAdrift.Size = New System.Drawing.Size(550, 416);
        Me.imgAdrift.TabIndex = 0;
        Me.imgAdrift.TabStop = false;
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White;
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        Me.Panel1.Controls.Add(Me.imgAdrift);
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.Panel1.Location = New System.Drawing.Point(0, 0);
        Me.Panel1.Name = "Panel1";
        Me.Panel1.Size = New System.Drawing.Size(558, 424);
        Me.Panel1.TabIndex = 1;
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0);
        Me.Label1.Name = "Label1";
        Me.Label1.Size = New System.Drawing.Size(100, 23);
        Me.Label1.TabIndex = 0;
        '
        'Splash
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        Me.BackColor = System.Drawing.Color.White;
        Me.ClientSize = New System.Drawing.Size(558, 424);
        Me.ControlBox = false;
        Me.Controls.Add(Me.Panel1);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Me.Name = "Splash";
        Me.ShowInTaskbar = false;
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Me.Text = "Splash";
        (System.ComponentModel.ISupportInitialize)(Me.imgAdrift).EndInit();
        Me.Panel1.ResumeLayout(false);
        Me.ResumeLayout(false);

    }
    Friend WithEvents imgAdrift As System.Windows.Forms.PictureBox;
    Friend WithEvents Panel1 As System.Windows.Forms.Panel;
    Friend WithEvents Label1 As System.Windows.Forms.Label;
}

}