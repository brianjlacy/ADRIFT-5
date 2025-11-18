using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmError
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
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmError));
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab;
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab;
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab;
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
        Me.lblInfo = New System.Windows.Forms.Label;
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
        Me.lblFeedback = New Infragistics.Win.Misc.UltraLabel;
        Me.txtFeedback = New Infragistics.Win.UltraWinEditors.UltraTextEditor;
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
        Me.txtStackTrace = New Infragistics.Win.UltraWinEditors.UltraTextEditor;
        Me.lblErrorMessage = New Infragistics.Win.Misc.UltraLabel;
        Me.btnOK = New Infragistics.Win.Misc.UltraButton;
        Me.imgCross = New System.Windows.Forms.PictureBox;
        Me.btnUp = New System.Windows.Forms.Button;
        Me.btnDown = New System.Windows.Forms.Button;
        Me.imgExclamation = New System.Windows.Forms.PictureBox;
        Me.tabsError = New Infragistics.Win.UltraWinTabControl.UltraTabControl;
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
        Me.UltraTabPageControl3.SuspendLayout();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtFeedback).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtStackTrace).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.imgCross).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.imgExclamation).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsError).BeginInit();
        Me.tabsError.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.lblInfo);
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3";
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(430, 115);
        '
        'lblInfo
        '
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.lblInfo.Location = New System.Drawing.Point(0, 0);
        Me.lblInfo.Name = "lblInfo";
        Me.lblInfo.Size = New System.Drawing.Size(430, 115);
        Me.lblInfo.TabIndex = 0;
        Me.lblInfo.Text = "This bug was completed on 5th September 1976.  You need to upgrade to version 1.0" + _;
            ".0 to prevent this error from reoccurring.";
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.lblFeedback);
        Me.UltraTabPageControl1.Controls.Add(Me.txtFeedback);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(472, 115);
        '
        'lblFeedback
        '
        Appearance1.TextHAlignAsString = "Center";
        Appearance1.TextVAlignAsString = "Middle";
        Me.lblFeedback.Appearance = Appearance1;
        Me.lblFeedback.Dock = System.Windows.Forms.DockStyle.Top;
        Me.lblFeedback.Location = New System.Drawing.Point(0, 0);
        Me.lblFeedback.Name = "lblFeedback";
        Me.lblFeedback.Size = New System.Drawing.Size(472, 24);
        Me.lblFeedback.TabIndex = 2;
        Me.lblFeedback.Text = "Please enter as much feedback about what you were doing as you can, in order to h" + _;
            "elp us eliminate this error.  Please include your email address if you are happy" + _;
            " for me to contact you.";
        '
        'txtFeedback
        '
        Me.txtFeedback.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance2.BackColor = System.Drawing.Color.White;
        Appearance2.BackColorDisabled = System.Drawing.Color.White;
        Appearance2.ForeColorDisabled = System.Drawing.Color.Black;
        Me.txtFeedback.Appearance = Appearance2;
        Me.txtFeedback.BackColor = System.Drawing.Color.White;
        Me.txtFeedback.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;
        Me.txtFeedback.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtFeedback.Location = New System.Drawing.Point(-1, 24);
        Me.txtFeedback.Multiline = true;
        Me.txtFeedback.Name = "txtFeedback";
        Me.txtFeedback.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
        Me.txtFeedback.Size = New System.Drawing.Size(474, 92);
        Me.txtFeedback.TabIndex = 1;
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.txtStackTrace);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(430, 115);
        '
        'txtStackTrace
        '
        Me.txtStackTrace.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance3.BackColor = System.Drawing.Color.White;
        Appearance3.BackColorDisabled = System.Drawing.Color.White;
        Appearance3.ForeColorDisabled = System.Drawing.Color.Black;
        Me.txtStackTrace.Appearance = Appearance3;
        Me.txtStackTrace.BackColor = System.Drawing.Color.White;
        Me.txtStackTrace.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;
        Me.txtStackTrace.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtStackTrace.Location = New System.Drawing.Point(-1, -1);
        Me.txtStackTrace.Multiline = true;
        Me.txtStackTrace.Name = "txtStackTrace";
        Me.txtStackTrace.ReadOnly = true;
        Me.txtStackTrace.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
        Me.txtStackTrace.Size = New System.Drawing.Size(432, 117);
        Me.txtStackTrace.TabIndex = 0;
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance4.TextHAlignAsString = "Center";
        Appearance4.TextVAlignAsString = "Middle";
        Me.lblErrorMessage.Appearance = Appearance4;
        Me.lblErrorMessage.Location = New System.Drawing.Point(87, 10);
        Me.lblErrorMessage.Name = "lblErrorMessage";
        Me.lblErrorMessage.Size = New System.Drawing.Size(377, 64);
        Me.lblErrorMessage.TabIndex = 2;
        Me.lblErrorMessage.Text = "There has been a really nasty error within ADRIFT.";
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnOK.Location = New System.Drawing.Point(206, 82);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(80, 28);
        Me.btnOK.TabIndex = 3;
        Me.btnOK.Text = "OK";
        '
        'imgCross
        '
        Me.imgCross.BackColor = System.Drawing.Color.Transparent;
        Me.imgCross.Image = (System.Drawing.Image)(resources.GetObject("imgCross.Image"));
        Me.imgCross.Location = New System.Drawing.Point(20, 20);
        Me.imgCross.Name = "imgCross";
        Me.imgCross.Size = New System.Drawing.Size(48, 48);
        Me.imgCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        Me.imgCross.TabIndex = 5;
        Me.imgCross.TabStop = false;
        '
        'btnUp
        '
        Me.btnUp.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnUp.Font = New System.Drawing.Font("Wingdings", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnUp.Image = (System.Drawing.Image)(resources.GetObject("btnUp.Image"));
        Me.btnUp.Location = New System.Drawing.Point(450, 82);
        Me.btnUp.Name = "btnUp";
        Me.btnUp.Size = New System.Drawing.Size(32, 32);
        Me.btnUp.TabIndex = 9;
        Me.btnUp.UseVisualStyleBackColor = true;
        '
        'btnDown
        '
        Me.btnDown.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.btnDown.Font = New System.Drawing.Font("Wingdings", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnDown.Image = (System.Drawing.Image)(resources.GetObject("btnDown.Image"));
        Me.btnDown.Location = New System.Drawing.Point(450, 82);
        Me.btnDown.Name = "btnDown";
        Me.btnDown.Size = New System.Drawing.Size(32, 32);
        Me.btnDown.TabIndex = 10;
        Me.btnDown.UseVisualStyleBackColor = true;
        '
        'imgExclamation
        '
        Me.imgExclamation.BackColor = System.Drawing.Color.Transparent;
        Me.imgExclamation.Image = (System.Drawing.Image)(resources.GetObject("imgExclamation.Image"));
        Me.imgExclamation.Location = New System.Drawing.Point(20, 20);
        Me.imgExclamation.Name = "imgExclamation";
        Me.imgExclamation.Size = New System.Drawing.Size(48, 48);
        Me.imgExclamation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        Me.imgExclamation.TabIndex = 11;
        Me.imgExclamation.TabStop = false;
        '
        'tabsError
        '
        Me.tabsError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.tabsError.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsError.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsError.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsError.Controls.Add(Me.UltraTabPageControl3);
        Me.tabsError.Location = New System.Drawing.Point(8, 120);
        Me.tabsError.Name = "tabsError";
        Me.tabsError.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsError.Size = New System.Drawing.Size(476, 141);
        Me.tabsError.TabIndex = 12;
        Me.tabsError.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft;
        UltraTab1.Key = "Info";
        UltraTab1.TabPage = Me.UltraTabPageControl3;
        UltraTab1.Text = "Additional Information";
        UltraTab2.Key = "Feedback";
        UltraTab2.TabPage = Me.UltraTabPageControl1;
        UltraTab2.Text = "Feedback";
        UltraTab3.Key = "StackTrace";
        UltraTab3.TabPage = Me.UltraTabPageControl2;
        UltraTab3.Text = "Stack Trace";
        Me.tabsError.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(472, 115);
        '
        'frmError
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.CancelButton = Me.btnOK;
        Me.ClientSize = New System.Drawing.Size(490, 268);
        Me.ControlBox = false;
        Me.Controls.Add(Me.tabsError);
        Me.Controls.Add(Me.imgExclamation);
        Me.Controls.Add(Me.imgCross);
        Me.Controls.Add(Me.btnDown);
        Me.Controls.Add(Me.btnUp);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.lblErrorMessage);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(300, 150);
        Me.Name = "frmError";
        Me.ShowInTaskbar = false;
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Me.Text = "ADRIFT Error";
        Me.TopMost = true;
        Me.UltraTabPageControl3.ResumeLayout(false);
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtFeedback).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.UltraTabPageControl2.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtStackTrace).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.imgCross).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.imgExclamation).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsError).EndInit();
        Me.tabsError.ResumeLayout(false);
        Me.ResumeLayout(false);

    }
    Friend WithEvents lblErrorMessage As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents txtStackTrace As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents imgCross As System.Windows.Forms.PictureBox;
    Private WithEvents btnUp As System.Windows.Forms.Button
    Private WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents imgExclamation As System.Windows.Forms.PictureBox;
    Friend WithEvents tabsError As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents txtFeedback As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents lblFeedback As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents lblInfo As System.Windows.Forms.Label;
}

}