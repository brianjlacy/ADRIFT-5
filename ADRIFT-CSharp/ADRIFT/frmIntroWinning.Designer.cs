using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmIntroWinning
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
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbStartLocation = New ADRIFT.ItemSelector();
        Me.chkDisplayFirstRoom = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.txtIntro = New ADRIFT.GenTextbox();
        Me.lblSubtleHint = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.txtWinning = New ADRIFT.GenTextbox();
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.tabsMain = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkDisplayFirstRoom).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).BeginInit();
        Me.tabsMain.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel1);
        Me.UltraTabPageControl1.Controls.Add(Me.cmbStartLocation);
        Me.UltraTabPageControl1.Controls.Add(Me.chkDisplayFirstRoom);
        Me.UltraTabPageControl1.Controls.Add(Me.txtIntro);
        Me.UltraTabPageControl1.Controls.Add(Me.lblSubtleHint);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(519, 366);
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.UltraLabel1.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Location = New System.Drawing.Point(11, 337);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(162, 16);
        Me.UltraLabel1.TabIndex = 6;
        Me.UltraLabel1.Text = "Start the adventure at location:";
        '
        'cmbStartLocation
        '
        Me.cmbStartLocation.AllowAddEdit = true;
        Me.cmbStartLocation.AllowedListType = ADRIFT.ItemSelector.ItemEnum.Location;
        Me.cmbStartLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbStartLocation.BackColor = System.Drawing.Color.Transparent;
        Me.cmbStartLocation.Key = null;
        Me.cmbStartLocation.Location = New System.Drawing.Point(175, 334);
        Me.cmbStartLocation.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.cmbStartLocation.MinimumSize = New System.Drawing.Size(10, 21);
        Me.cmbStartLocation.Name = "cmbStartLocation";
        Me.cmbStartLocation.Size = New System.Drawing.Size(335, 21);
        Me.cmbStartLocation.TabIndex = 5;
        '
        'chkDisplayFirstRoom
        '
        Me.chkDisplayFirstRoom.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.chkDisplayFirstRoom.BackColor = System.Drawing.Color.Transparent;
        Me.chkDisplayFirstRoom.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkDisplayFirstRoom.Location = New System.Drawing.Point(342, 12);
        Me.chkDisplayFirstRoom.Name = "chkDisplayFirstRoom";
        Me.chkDisplayFirstRoom.Size = New System.Drawing.Size(172, 20);
        Me.chkDisplayFirstRoom.TabIndex = 4;
        Me.chkDisplayFirstRoom.Text = "Display first room description";
        '
        'txtIntro
        '
        Me.txtIntro.AllowAlternateDescriptions = true;
        Me.txtIntro.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtIntro.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.txtIntro.Location = New System.Drawing.Point(11, 38);
        Me.txtIntro.Name = "txtIntro";
        Me.txtIntro.sCommand = null;
        Me.txtIntro.Size = New System.Drawing.Size(499, 285);
        Me.txtIntro.TabIndex = 3;
        '
        'lblSubtleHint
        '
        Me.lblSubtleHint.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblSubtleHint.Location = New System.Drawing.Point(11, 16);
        Me.lblSubtleHint.Name = "lblSubtleHint";
        Me.lblSubtleHint.Size = New System.Drawing.Size(139, 16);
        Me.lblSubtleHint.TabIndex = 1;
        Me.lblSubtleHint.Text = "Text to display on startup:";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.txtWinning);
        Me.UltraTabPageControl2.Controls.Add(Me.UltraLabel2);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(519, 366);
        '
        'txtWinning
        '
        Me.txtWinning.AllowAlternateDescriptions = true;
        Me.txtWinning.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtWinning.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.txtWinning.Location = New System.Drawing.Point(11, 38);
        Me.txtWinning.Name = "txtWinning";
        Me.txtWinning.sCommand = null;
        Me.txtWinning.Size = New System.Drawing.Size(499, 316);
        Me.txtWinning.TabIndex = 5;
        '
        'UltraLabel2
        '
        Me.UltraLabel2.BackColorInternal = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Location = New System.Drawing.Point(11, 16);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(451, 16);
        Me.UltraLabel2.TabIndex = 4;
        Me.UltraLabel2.Text = "The following message will be displayed after the game finishes.";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 392);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(523, 48);
        Me.UltraStatusBar1.TabIndex = 13;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(423, 400);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 19;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(327, 400);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 18;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(231, 400);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 17;
        Me.btnOK.Text = "OK";
        '
        'tabsMain
        '
        Me.tabsMain.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsMain.Location = New System.Drawing.Point(0, 0);
        Me.tabsMain.Name = "tabsMain";
        Me.tabsMain.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsMain.Size = New System.Drawing.Size(523, 392);
        Me.tabsMain.TabIndex = 20;
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Introduction";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "End of Game";
        Me.tabsMain.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(519, 366);
        '
        'frmIntroWinning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.ClientSize = New System.Drawing.Size(523, 440);
        Me.Controls.Add(Me.tabsMain);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.MinimumSize = New System.Drawing.Size(539, 476);
        Me.Name = "frmIntroWinning";
        Me.Text = "Introduction + End of Game";
        Me.UltraTabPageControl1.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkDisplayFirstRoom).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).EndInit();
        Me.tabsMain.ResumeLayout(false);
        Me.ResumeLayout(false);

    }
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents tabsMain As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents txtIntro As ADRIFT.GenTextbox;
    Friend WithEvents lblSubtleHint As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbStartLocation As ADRIFT.ItemSelector;
    Friend WithEvents chkDisplayFirstRoom As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents txtWinning As ADRIFT.GenTextbox;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;

}

}