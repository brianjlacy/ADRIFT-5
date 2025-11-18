using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmHint
{
    Inherits System.Windows.Forms.Form;

    private clsHint cHint;
    private bool bChanged;


    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            if (bChanged)
            {
                btnApply.Enabled = true;
            Else
                btnApply.Enabled = false;
            }
        }
    }

#Region " Windows Form Designer generated code "

    public bool bKeepOpen = false;

    public void New(clsHint Hint, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmHint)
            {
                if (CType(w, frmHint).cHint.Key = Hint.Key && Hint.Key IsNot null)
                {
                    w.BringToFront();
                    w.Focus();
                    Exit Sub;
                }
            }
        Next;

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Me.RestrictDetails1 = New Generator.RestrictDetails - got deleted :-(

        'Add any initialization after the InitializeComponent() call
        LoadForm(Hint, bShow);
        bKeepOpen = Not bShow

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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents tabsMain As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents RestrictDetails1 As ADRIFT.RestrictDetails;
    Friend WithEvents lblQuestion As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblSubtleHint As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblObviousHint As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtSubtle As ADRIFT.GenTextbox;
    Friend WithEvents txtObvious As ADRIFT.GenTextbox;
    Friend WithEvents txtQuestion As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.txtQuestion = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.txtObvious = New ADRIFT.GenTextbox();
        Me.txtSubtle = New ADRIFT.GenTextbox();
        Me.lblObviousHint = New Infragistics.Win.Misc.UltraLabel();
        Me.lblSubtleHint = New Infragistics.Win.Misc.UltraLabel();
        Me.lblQuestion = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.RestrictDetails1 = New ADRIFT.RestrictDetails();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.tabsMain = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtQuestion).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).BeginInit();
        Me.tabsMain.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.txtQuestion);
        Me.UltraTabPageControl1.Controls.Add(Me.txtObvious);
        Me.UltraTabPageControl1.Controls.Add(Me.txtSubtle);
        Me.UltraTabPageControl1.Controls.Add(Me.lblObviousHint);
        Me.UltraTabPageControl1.Controls.Add(Me.lblSubtleHint);
        Me.UltraTabPageControl1.Controls.Add(Me.lblQuestion);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(516, 364);
        '
        'txtQuestion
        '
        Me.txtQuestion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtQuestion.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtQuestion.Location = New System.Drawing.Point(8, 24);
        Me.txtQuestion.Name = "txtQuestion";
        Me.txtQuestion.Size = New System.Drawing.Size(496, 24);
        Me.txtQuestion.TabIndex = 5;
        '
        'txtObvious
        '
        Me.txtObvious.AllowAlternateDescriptions = true;
        Me.txtObvious.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtObvious.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.txtObvious.FirstTabHasRestrictions = false;
        Me.txtObvious.Location = New System.Drawing.Point(8, 224);
        Me.txtObvious.Name = "txtObvious";
        Me.txtObvious.sCommand = null;
        Me.txtObvious.Size = New System.Drawing.Size(496, 132);
        Me.txtObvious.TabIndex = 4;
        '
        'txtSubtle
        '
        Me.txtSubtle.AllowAlternateDescriptions = true;
        Me.txtSubtle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtSubtle.BackColor = System.Drawing.SystemColors.ControlLight;
        Me.txtSubtle.FirstTabHasRestrictions = false;
        Me.txtSubtle.Location = New System.Drawing.Point(8, 72);
        Me.txtSubtle.Name = "txtSubtle";
        Me.txtSubtle.sCommand = null;
        Me.txtSubtle.Size = New System.Drawing.Size(496, 128);
        Me.txtSubtle.TabIndex = 3;
        '
        'lblObviousHint
        '
        Me.lblObviousHint.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblObviousHint.Location = New System.Drawing.Point(8, 208);
        Me.lblObviousHint.Name = "lblObviousHint";
        Me.lblObviousHint.Size = New System.Drawing.Size(160, 16);
        Me.lblObviousHint.TabIndex = 2;
        Me.lblObviousHint.Text = "Really obvious hint: (optional)";
        '
        'lblSubtleHint
        '
        Me.lblSubtleHint.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblSubtleHint.Location = New System.Drawing.Point(8, 56);
        Me.lblSubtleHint.Name = "lblSubtleHint";
        Me.lblSubtleHint.Size = New System.Drawing.Size(64, 16);
        Me.lblSubtleHint.TabIndex = 1;
        Me.lblSubtleHint.Text = "Subtle Hint:";
        '
        'lblQuestion
        '
        Me.lblQuestion.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblQuestion.Location = New System.Drawing.Point(8, 8);
        Me.lblQuestion.Name = "lblQuestion";
        Me.lblQuestion.Size = New System.Drawing.Size(56, 16);
        Me.lblQuestion.TabIndex = 0;
        Me.lblQuestion.Text = "Question:";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.RestrictDetails1);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(516, 364);
        '
        'RestrictDetails1
        '
        Me.RestrictDetails1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictDetails1.BackColor = System.Drawing.SystemColors.Control;
        Me.RestrictDetails1.Location = New System.Drawing.Point(16, 8);
        Me.RestrictDetails1.Name = "RestrictDetails1";
        Me.RestrictDetails1.Size = New System.Drawing.Size(496, 348);
        Me.RestrictDetails1.TabIndex = 0;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(416, 396);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 15;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(320, 396);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 14;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(224, 396);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 13;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 390);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(520, 48);
        Me.UltraStatusBar1.TabIndex = 12;
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
        Me.tabsMain.Size = New System.Drawing.Size(520, 390);
        Me.tabsMain.TabIndex = 16;
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Descriptions";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Restrictions";
        Me.tabsMain.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(516, 364);
        '
        'frmHint
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(520, 438);
        Me.Controls.Add(Me.tabsMain);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(496, 472);
        Me.Name = "frmHint";
        Me.Text = "Hint - ";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtQuestion).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).EndInit();
        Me.tabsMain.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region

    private void LoadForm(ref cHint As clsHint, bool bShow)
    {

        Me.cHint = cHint;


        With cHint;
            Text = "Hint - " & .Question
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= " [" + .Key + "]"
            If .Question = "" Then Text = "New Hint"

            txtQuestion.Text = .Question;
            txtSubtle.Description = .SubtleHint;
            txtObvious.Description = .SledgeHammerHint;

            RestrictDetails1.LoadRestrictions(.arlRestrictions.Copy);

        }

        If bShow Then Me.Show()
        Changed = False

        OpenForms.Add(Me);

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyHint();
        CloseHint(Me);
    }


    private void ApplyHint()
    {

        With cHint;
            .Question = txtQuestion.Text;
            .SubtleHint = txtSubtle.Description.Copy;
            .SledgeHammerHint = txtObvious.Description.Copy;
            .LastUpdated = Now;
            .IsLibrary = false;

            .arlRestrictions = Me.RestrictDetails1.arlRestrictions.Copy;

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Hint");
                Adventure.htblHints.Add(cHint, .Key);
            }

            UpdateListItem(.Key, .Question);
        }

        Adventure.Changed = true;

    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {

        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyHint()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseHint(Me);

    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyHint();
        Changed = False
    }

    private void StuffChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void frmHint_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }

    private void frmHint_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgHint16.GetHicon);
        GetFormPosition(Me);
    }

    private void frmHint_Shown(object sender, System.EventArgs e)
    {
        if (txtQuestion.Text = "")
        {
            txtQuestion.Focus();
        Else
            'txtSubtle.rtxtSource.SelectionStart = txtSubtle.rtxtSource.TextLength
            txtSubtle.rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.DocumentEnd);
            txtSubtle.rtxtSource.Focus();
        }
    }

    private void frmHint_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Hints");
    }

}

}