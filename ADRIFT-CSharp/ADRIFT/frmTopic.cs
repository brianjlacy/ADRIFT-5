using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmTopic
{
    Inherits System.Windows.Forms.Form;

    private clsTopic cTopic;
    private TopicHashTable htblTopics;
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider;
    private bool bChanged;

    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            'If bChanged Then
            '    btnApply.Enabled = True
            'Else
            '    btnApply.Enabled = False
            'End If
        }
    }

#Region " Windows Form Designer generated code "

    internal void New(clsTopic Topic, TopicHashTable htblTopics)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        LoadForm(Topic, htblTopics);

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

    'Me.RestrictDetails1 = New RestrictDetails
    'Me.Actions1 = New Actions

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents tabsMain As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents RestrictDetails1 As ADRIFT.RestrictDetails;
    Friend WithEvents lblSummary As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblKeywords As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents lblConversation As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtConversation As ADRIFT.GenTextbox;
    Friend WithEvents txtKeywords As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents grpType As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents chkIntro As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkFarewell As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkCommand As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkTell As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkAsk As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents Actions1 As ADRIFT.Actions;
    Friend WithEvents chkStay As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents txtSummary As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmTopic));
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.grpType = New Infragistics.Win.Misc.UltraGroupBox();
        Me.chkIntro = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkFarewell = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkCommand = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkTell = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkAsk = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.txtKeywords = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.txtSummary = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.txtConversation = New ADRIFT.GenTextbox();
        Me.lblConversation = New Infragistics.Win.Misc.UltraLabel();
        Me.lblKeywords = New Infragistics.Win.Misc.UltraLabel();
        Me.lblSummary = New Infragistics.Win.Misc.UltraLabel();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.RestrictDetails1 = New ADRIFT.RestrictDetails();
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.chkStay = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.Actions1 = New ADRIFT.Actions();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.tabsMain = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).BeginInit();
        Me.grpType.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkIntro).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkFarewell).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkCommand).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkTell).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAsk).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtKeywords).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtSummary).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        Me.UltraTabPageControl3.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkStay).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).BeginInit();
        Me.tabsMain.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grpType);
        Me.UltraTabPageControl1.Controls.Add(Me.txtKeywords);
        Me.UltraTabPageControl1.Controls.Add(Me.txtSummary);
        Me.UltraTabPageControl1.Controls.Add(Me.txtConversation);
        Me.UltraTabPageControl1.Controls.Add(Me.lblConversation);
        Me.UltraTabPageControl1.Controls.Add(Me.lblKeywords);
        Me.UltraTabPageControl1.Controls.Add(Me.lblSummary);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(516, 414);
        '
        'grpType
        '
        Me.grpType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grpType.Controls.Add(Me.chkIntro);
        Me.grpType.Controls.Add(Me.chkFarewell);
        Me.grpType.Controls.Add(Me.chkCommand);
        Me.grpType.Controls.Add(Me.chkTell);
        Me.grpType.Controls.Add(Me.chkAsk);
        Me.grpType.Location = New System.Drawing.Point(8, 57);
        Me.grpType.Name = "grpType";
        Me.grpType.Size = New System.Drawing.Size(494, 52);
        Me.grpType.TabIndex = 13;
        Me.grpType.Text = "Type of topic";
        Me.grpType.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
        '
        'chkIntro
        '
        Me.chkIntro.BackColor = System.Drawing.Color.Transparent;
        Me.chkIntro.BackColorInternal = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkIntro, resources.GetString("chkIntro.HelpString"));
        Me.chkIntro.Location = New System.Drawing.Point(26, 25);
        Me.chkIntro.Name = "chkIntro";
        Me.HelpProvider1.SetShowHelp(Me.chkIntro, true);
        Me.chkIntro.Size = New System.Drawing.Size(89, 20);
        Me.chkIntro.TabIndex = 12;
        Me.chkIntro.Text = "Introduction";
        '
        'chkFarewell
        '
        Me.chkFarewell.BackColor = System.Drawing.Color.Transparent;
        Me.chkFarewell.BackColorInternal = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkFarewell, resources.GetString("chkFarewell.HelpString"));
        Me.chkFarewell.Location = New System.Drawing.Point(404, 25);
        Me.chkFarewell.Name = "chkFarewell";
        Me.HelpProvider1.SetShowHelp(Me.chkFarewell, true);
        Me.chkFarewell.Size = New System.Drawing.Size(67, 20);
        Me.chkFarewell.TabIndex = 16;
        Me.chkFarewell.Text = "Farewell";
        '
        'chkCommand
        '
        Me.chkCommand.BackColor = System.Drawing.Color.Transparent;
        Me.chkCommand.BackColorInternal = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkCommand, "If a topic has a general command, the command must be matched in the same way as " + _;
        "normal task commands.");
        Me.chkCommand.Location = New System.Drawing.Point(262, 25);
        Me.chkCommand.Name = "chkCommand";
        Me.HelpProvider1.SetShowHelp(Me.chkCommand, true);
        Me.chkCommand.Size = New System.Drawing.Size(117, 20);
        Me.chkCommand.TabIndex = 15;
        Me.chkCommand.Text = "General Command";
        '
        'chkTell
        '
        Me.chkTell.BackColor = System.Drawing.Color.Transparent;
        Me.chkTell.BackColorInternal = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkTell, "If a topic is marked as a Tell topic, you can execute Ask commands, matching on a" + _;
        "ny of the given keywords.");
        Me.chkTell.Location = New System.Drawing.Point(196, 25);
        Me.chkTell.Name = "chkTell";
        Me.HelpProvider1.SetShowHelp(Me.chkTell, true);
        Me.chkTell.Size = New System.Drawing.Size(46, 20);
        Me.chkTell.TabIndex = 14;
        Me.chkTell.Text = "Tell";
        '
        'chkAsk
        '
        Me.chkAsk.BackColor = System.Drawing.Color.Transparent;
        Me.chkAsk.BackColorInternal = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkAsk, "If a topic is marked as an Ask topic, you can execute Ask commands, matching on a" + _;
        "ny of the given keywords.");
        Me.chkAsk.Location = New System.Drawing.Point(129, 25);
        Me.chkAsk.Name = "chkAsk";
        Me.HelpProvider1.SetShowHelp(Me.chkAsk, true);
        Me.chkAsk.Size = New System.Drawing.Size(46, 20);
        Me.chkAsk.TabIndex = 13;
        Me.chkAsk.Text = "Ask";
        '
        'txtKeywords
        '
        Me.txtKeywords.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtKeywords.Enabled = false;
        Me.txtKeywords.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtKeywords.Location = New System.Drawing.Point(8, 137);
        Me.txtKeywords.Name = "txtKeywords";
        Me.txtKeywords.Size = New System.Drawing.Size(494, 24);
        Me.txtKeywords.TabIndex = 6;
        '
        'txtSummary
        '
        Me.txtSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtSummary.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtSummary.Location = New System.Drawing.Point(8, 24);
        Me.txtSummary.Name = "txtSummary";
        Me.txtSummary.Size = New System.Drawing.Size(494, 24);
        Me.txtSummary.TabIndex = 5;
        '
        'txtConversation
        '
        Me.txtConversation.AllowAlternateDescriptions = true;
        Me.txtConversation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtConversation.BackColor = System.Drawing.Color.Transparent;
        Me.txtConversation.FirstTabHasRestrictions = false;
        Me.txtConversation.Location = New System.Drawing.Point(8, 188);
        Me.txtConversation.Name = "txtConversation";
        Me.txtConversation.sCommand = null;
        Me.txtConversation.Size = New System.Drawing.Size(496, 218);
        Me.txtConversation.TabIndex = 4;
        '
        'lblConversation
        '
        Me.lblConversation.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblConversation.Location = New System.Drawing.Point(8, 169);
        Me.lblConversation.Name = "lblConversation";
        Me.lblConversation.Size = New System.Drawing.Size(82, 16);
        Me.lblConversation.TabIndex = 2;
        Me.lblConversation.Text = "Conversation:";
        '
        'lblKeywords
        '
        Me.lblKeywords.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblKeywords.Location = New System.Drawing.Point(8, 119);
        Me.lblKeywords.Name = "lblKeywords";
        Me.lblKeywords.Size = New System.Drawing.Size(64, 16);
        Me.lblKeywords.TabIndex = 1;
        Me.lblKeywords.Text = "Keywords:";
        '
        'lblSummary
        '
        Me.lblSummary.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblSummary.Location = New System.Drawing.Point(8, 8);
        Me.lblSummary.Name = "lblSummary";
        Me.lblSummary.Size = New System.Drawing.Size(56, 16);
        Me.lblSummary.TabIndex = 0;
        Me.lblSummary.Text = "Summary:";
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.RestrictDetails1);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(516, 414);
        '
        'RestrictDetails1
        '
        Me.RestrictDetails1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.RestrictDetails1.BackColor = System.Drawing.Color.Transparent;
        Me.RestrictDetails1.Location = New System.Drawing.Point(16, 8);
        Me.RestrictDetails1.Name = "RestrictDetails1";
        Me.RestrictDetails1.Size = New System.Drawing.Size(496, 398);
        Me.RestrictDetails1.TabIndex = 0;
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.chkStay);
        Me.UltraTabPageControl3.Controls.Add(Me.Actions1);
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(2, 21);
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3";
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(516, 414);
        '
        'chkStay
        '
        Me.chkStay.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.chkStay.BackColor = System.Drawing.Color.Transparent;
        Me.chkStay.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkStay.Enabled = false;
        Me.chkStay.Location = New System.Drawing.Point(388, 377);
        Me.chkStay.Name = "chkStay";
        Me.chkStay.Size = New System.Drawing.Size(127, 33);
        Me.chkStay.TabIndex = 14;
        Me.chkStay.Text = "Stay in this conversation node";
        '
        'Actions1
        '
        Me.Actions1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.Actions1.BackColor = System.Drawing.Color.Transparent;
        Me.Actions1.Location = New System.Drawing.Point(16, 8);
        Me.Actions1.Name = "Actions1";
        Me.Actions1.Size = New System.Drawing.Size(496, 398);
        Me.Actions1.TabIndex = 15;
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(416, 443);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 14;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(320, 443);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 13;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 437);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(520, 48);
        Me.UltraStatusBar1.TabIndex = 12;
        '
        'tabsMain
        '
        Me.tabsMain.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsMain.Controls.Add(Me.UltraTabPageControl3);
        Me.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsMain.Location = New System.Drawing.Point(0, 0);
        Me.tabsMain.Name = "tabsMain";
        Me.tabsMain.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsMain.Size = New System.Drawing.Size(520, 437);
        Me.tabsMain.TabIndex = 16;
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Descriptions";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Restrictions";
        UltraTab3.TabPage = Me.UltraTabPageControl3;
        UltraTab3.Text = "Actions";
        Me.tabsMain.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3});
        Me.tabsMain.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005;
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(516, 414);
        '
        'frmTopic
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(520, 485);
        Me.Controls.Add(Me.tabsMain);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(536, 521);
        Me.Name = "frmTopic";
        Me.Text = "Conversation Topic - ";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).EndInit();
        Me.grpType.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkIntro).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkFarewell).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkCommand).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkTell).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkAsk).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtKeywords).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtSummary).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.UltraTabPageControl3.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkStay).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.tabsMain).EndInit();
        Me.tabsMain.ResumeLayout(false);
        Me.ResumeLayout(false);

    }

#End Region


    private void LoadForm(ref cTopic As clsTopic, ref htblTopics As TopicHashTable)
    {

        Me.cTopic = cTopic;
        Me.htblTopics = htblTopics;

        With cTopic;
            Text = "Conversation Topic - " & .Summary
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= " [" + .Key + "]"
            If .Summary = "" Then Text = "New Conversation Topic"

            txtSummary.Text = .Summary;
            txtKeywords.Text = .Keywords;
            txtConversation.Description = .oConversation;

            chkAsk.Checked = .bAsk;
            chkCommand.Checked = .bCommand;
            chkFarewell.Checked = .bFarewell;
            chkIntro.Checked = .bIntroduction;
            chkStay.Checked = .bStayInNode;
            chkTell.Checked = .bTell;

            RestrictDetails1.LoadRestrictions(.arlRestrictions.Copy);
            Actions1.LoadActions(.arlActions.Copy);

            chkStay.Enabled = .ParentKey <> "";
        }

        Changed = False
        Me.ShowDialog();

    }


    private string GetNewKey()
    {

        private int iNum = 1;

        while (htblTopics.ContainsKey("Topic" + iNum))
        {
            iNum += 1;
        }

        return "Topic" + iNum;

    }


    private void ApplyTopic()
    {

        With cTopic;
            .Summary = txtSummary.Text;
            .Keywords = txtKeywords.Text;
            .oConversation = txtConversation.Description.Copy;

            .arlRestrictions = Me.RestrictDetails1.arlRestrictions.Copy;
            .arlActions = Me.Actions1.arlActions.Copy;

            .bAsk = chkAsk.Checked;
            .bCommand = chkCommand.Checked;
            .bFarewell = chkFarewell.Checked;
            .bIntroduction = chkIntro.Checked;
            .bStayInNode = chkStay.Checked;
            .bTell = chkTell.Checked;

            if (txtSummary.Text = "")
            {
                if (.bAsk && .bTell)
                {
                    .Summary = "Ask or Tell about " + .Keywords;
                ElseIf .bAsk Then
                    .Summary = "Ask about " + .Keywords;
                ElseIf .bTell Then
                    .Summary = "Ask about " + .Keywords;
                }
                if (.bIntroduction)
                {
                    if (Not .bCommand)
                    {
                        .Summary = "Implicit introduction";
                    Else
                        .Summary = "Explicit introduction";
                    }
                }
            }

            if (.Key = "")
            {
                .Key = GetNewKey();
                'Adventure.htblHints.Add(cHint, .Key)
            }

            'UpdateListItem(.Key, .Question)
        }

    }


    private void frmTopic_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyTopic();
        DialogResult = Windows.Forms.DialogResult.OK
    }


private enum BoolChange
    {
        NoChange;
        [false];
        [true];
    }

    private void chkAsk_CheckedChanged(System.Object sender, System.EventArgs e)
    {

        if (chkCommand.Checked)
        {
            lblKeywords.Text = "Command:";
        Else
            lblKeywords.Text = "Keywords:";
        }

        if (chkAsk.Checked || chkCommand.Checked || chkTell.Checked)
        {
            lblKeywords.Enabled = true;
            txtKeywords.Enabled = true;
        Else
            lblKeywords.Enabled = false;
            txtKeywords.Enabled = false;
        }

        private BoolChange bAsk = BoolChange.NoChange;
        private BoolChange bCommand = BoolChange.NoChange;
        private BoolChange bFarewell = BoolChange.NoChange;
        private BoolChange bIntro = BoolChange.NoChange;
        private BoolChange bTell = BoolChange.NoChange;
        switch (true)
        {
            case sender Is chkIntro:
                {
                if (chkIntro.Checked)
                {
                    bFarewell = BoolChange.False
                    bAsk = BoolChange.False
                    bTell = BoolChange.False
                Else
                    If ! chkAsk.Checked && ! chkTell.Checked Then bFarewell = BoolChange.true
                    if (Not chkCommand.Checked && Not chkFarewell.Checked)
                    {
                        bAsk = BoolChange.True
                        bTell = BoolChange.True
                    }
                }
            case sender Is chkAsk:
                {
                if (chkAsk.Checked)
                {
                    bCommand = BoolChange.False
                    bIntro = BoolChange.False
                    bFarewell = BoolChange.False
                Else
                    If ! chkTell.Checked Then bCommand = BoolChange.true
                    if (Not chkTell.Checked || chkFarewell.Checked)
                    {
                        bIntro = BoolChange.True
                        bFarewell = BoolChange.True
                    }
                }
            case sender Is chkTell:
                {
                if (chkTell.Checked)
                {
                    bCommand = BoolChange.False
                    bIntro = BoolChange.False
                    bFarewell = BoolChange.False
                Else
                    If ! chkAsk.Checked Then bCommand = BoolChange.true
                    if (Not chkAsk.Checked || chkFarewell.Checked)
                    {
                        bIntro = BoolChange.True
                        bFarewell = BoolChange.True
                    }
                }
            case sender Is chkCommand:
                {
                if (chkCommand.Checked)
                {
                    bAsk = BoolChange.False
                    bTell = BoolChange.False
                Else
                    if (Not chkIntro.Checked && Not chkFarewell.Checked)
                    {
                        bAsk = BoolChange.True
                        bTell = BoolChange.True
                    }
                }
            case sender Is chkFarewell:
                {
                if (chkFarewell.Checked)
                {
                    bIntro = BoolChange.False
                    bAsk = BoolChange.False
                    bTell = BoolChange.False
                Else
                    If ! chkAsk.Checked || chkTell.Checked Then bIntro = BoolChange.true
                    if (Not chkIntro.Checked && Not chkCommand.Checked)
                    {
                        bAsk = BoolChange.True
                        bTell = BoolChange.True
                    }
                }
        }


        If bAsk <> BoolChange.NoChange Then chkAsk.Enabled = CBool(IIf(bAsk = BoolChange.true, true, false))
        If bCommand <> BoolChange.NoChange Then chkCommand.Enabled = CBool(IIf(bCommand = BoolChange.true, true, false))
        If bFarewell <> BoolChange.NoChange Then chkFarewell.Enabled = CBool(IIf(bFarewell = BoolChange.true, true, false))
        If bIntro <> BoolChange.NoChange Then chkIntro.Enabled = CBool(IIf(bIntro = BoolChange.true, true, false))
        If bTell <> BoolChange.NoChange Then chkTell.Enabled = CBool(IIf(bTell = BoolChange.true, true, false))

    }

    private void frmTopic_Shown(object sender, System.EventArgs e)
    {
        if (txtSummary.Text = "")
        {
            txtSummary.Focus();
        Else
            'txtConversation.rtxtSource.SelectionStart = txtConversation.rtxtSource.TextLength
            txtConversation.rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.DocumentEnd);
            txtConversation.rtxtSource.Focus();
        }
    }

}

}