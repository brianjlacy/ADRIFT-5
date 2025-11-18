using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmRestrictions
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    internal void New(ref Restrictions As RestrictionArrayList)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        RestrictDetails1.LoadRestrictions(Restrictions);
    }

    'Form overrides dispose to clean up the component list.
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

    ' Me.RestrictDetails1 = New Generator.RestrictDetails

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents RestrictDetails1 As ADRIFT.RestrictDetails;
    Friend WithEvents btnOk As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.btnOk = New Infragistics.Win.Misc.UltraButton;
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton;
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox;
        Me.RestrictDetails1 = New ADRIFT.RestrictDetails;
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        Me.SuspendLayout();
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnOk.Location = New System.Drawing.Point(56, 288);
        Me.btnOk.Name = "btnOk";
        Me.btnOk.Size = New System.Drawing.Size(88, 32);
        Me.btnOk.TabIndex = 1;
        Me.btnOk.Text = "OK";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(160, 288);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 2;
        Me.btnCancel.Text = "Cancel";
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.UltraGroupBox1.Controls.Add(Me.RestrictDetails1);
        Me.UltraGroupBox1.Location = New System.Drawing.Point(1, 1);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(302, 281);
        Me.UltraGroupBox1.TabIndex = 3;
        '
        'RestrictDetails1
        '
        Me.RestrictDetails1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictDetails1.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictDetails1.Location = New System.Drawing.Point(24, 11);
        Me.RestrictDetails1.Name = "RestrictDetails1";
        Me.RestrictDetails1.Size = New System.Drawing.Size(275, 264);
        Me.RestrictDetails1.TabIndex = 0;
        '
        'frmRestrictions
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(304, 326);
        Me.Controls.Add(Me.UltraGroupBox1);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOk);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        Me.MinimumSize = New System.Drawing.Size(296, 216);
        Me.Name = "frmRestrictions";
        Me.Text = "Restrictions";
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private void btnOk_Click(System.Object sender, System.EventArgs e)
    {

        if (Not RestrictDetails1.arlRestrictions.IsBracketsValid)
        {
            ErrMsg("Invalid Bracket Sequence");
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK;
        }

    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        Me.DialogResult = Windows.Forms.DialogResult.Cancel;
    }

    private void frmRestrictions_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
    }

    private void frmRestrictions_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SaveFormPosition(Me);
    }

}

}