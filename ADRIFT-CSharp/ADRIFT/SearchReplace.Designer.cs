using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class SearchReplace
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
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
        Me.btnFind = New Infragistics.Win.Misc.UltraButton();
        Me.btnReplace = New Infragistics.Win.Misc.UltraButton();
        Me.btnReplaceAll = New Infragistics.Win.Misc.UltraButton();
        Me.StatusBar = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.pnlFind = New System.Windows.Forms.Panel();
        Me.lblFind = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbFind = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.grpSearchOptions = New Infragistics.Win.Misc.UltraExpandableGroupBox();
        Me.pnlOptions = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
        Me.chkMatchWholeWord = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.chkMatchCase = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.pnlReplace = New System.Windows.Forms.Panel();
        Me.lblReplace = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbReplace = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.pnlLookIn = New System.Windows.Forms.Panel();
        Me.lblLookIn = New Infragistics.Win.Misc.UltraLabel();
        Me.cmbLookIn = New Infragistics.Win.UltraWinEditors.UltraComboEditor();
        Me.toolsSearchReplace = New System.Windows.Forms.ToolStrip();
        Me.btnSelectFind = New System.Windows.Forms.ToolStripButton();
        Me.btnSelectReplace = New System.Windows.Forms.ToolStripButton();
        Me.btnFindAll = New Infragistics.Win.Misc.UltraButton();
        Me.lvwFoundItems = New Infragistics.Win.UltraWinListView.UltraListView();
        Me.btnClose = New System.Windows.Forms.Button();
        Me.cmsItems = New System.Windows.Forms.ContextMenuStrip(Me.components);
        Me.miEditItem = New System.Windows.Forms.ToolStripMenuItem();
        Me.miOpenFolder = New System.Windows.Forms.ToolStripMenuItem();
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar).BeginInit();
        Me.pnlFind.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbFind).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpSearchOptions).BeginInit();
        Me.grpSearchOptions.SuspendLayout();
        Me.pnlOptions.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.chkMatchWholeWord).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkMatchCase).BeginInit();
        Me.pnlReplace.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReplace).BeginInit();
        Me.pnlLookIn.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLookIn).BeginInit();
        Me.toolsSearchReplace.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.lvwFoundItems).BeginInit();
        Me.cmsItems.SuspendLayout();
        Me.SuspendLayout();
        '
        'btnFind
        '
        Me.btnFind.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnFind.Enabled = false;
        Me.btnFind.Location = New System.Drawing.Point(52, 456);
        Me.btnFind.Name = "btnFind";
        Me.btnFind.Size = New System.Drawing.Size(85, 26);
        Me.btnFind.TabIndex = 0;
        Me.btnFind.Text = "&Find";
        '
        'btnReplace
        '
        Me.btnReplace.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnReplace.Enabled = false;
        Me.btnReplace.Location = New System.Drawing.Point(143, 456);
        Me.btnReplace.Name = "btnReplace";
        Me.btnReplace.Size = New System.Drawing.Size(85, 26);
        Me.btnReplace.TabIndex = 1;
        Me.btnReplace.Text = "&Replace";
        '
        'btnReplaceAll
        '
        Me.btnReplaceAll.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnReplaceAll.Enabled = false;
        Me.btnReplaceAll.Location = New System.Drawing.Point(234, 456);
        Me.btnReplaceAll.Name = "btnReplaceAll";
        Me.btnReplaceAll.Size = New System.Drawing.Size(85, 26);
        Me.btnReplaceAll.TabIndex = 2;
        Me.btnReplaceAll.Text = "Replace &All";
        '
        'StatusBar
        '
        Me.StatusBar.Location = New System.Drawing.Point(0, 450);
        Me.StatusBar.Name = "StatusBar";
        Me.StatusBar.Size = New System.Drawing.Size(331, 38);
        Me.StatusBar.TabIndex = 9;
        '
        'pnlFind
        '
        Me.pnlFind.Controls.Add(Me.lblFind);
        Me.pnlFind.Controls.Add(Me.cmbFind);
        Me.pnlFind.Dock = System.Windows.Forms.DockStyle.Top;
        Me.pnlFind.Location = New System.Drawing.Point(0, 23);
        Me.pnlFind.Name = "pnlFind";
        Me.pnlFind.Size = New System.Drawing.Size(331, 52);
        Me.pnlFind.TabIndex = 10;
        '
        'lblFind
        '
        Me.lblFind.Location = New System.Drawing.Point(8, 7);
        Me.lblFind.Name = "lblFind";
        Me.lblFind.Size = New System.Drawing.Size(66, 15);
        Me.lblFind.TabIndex = 1;
        Me.lblFind.Text = "Fi&nd what:";
        '
        'cmbFind
        '
        Me.cmbFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbFind.Location = New System.Drawing.Point(8, 25);
        Me.cmbFind.Name = "cmbFind";
        Me.cmbFind.Size = New System.Drawing.Size(316, 21);
        Me.cmbFind.TabIndex = 0;
        '
        'grpSearchOptions
        '
        Me.grpSearchOptions.Controls.Add(Me.pnlOptions);
        Me.grpSearchOptions.Dock = System.Windows.Forms.DockStyle.Top;
        Me.grpSearchOptions.ExpandedSize = New System.Drawing.Size(331, 81);
        Me.grpSearchOptions.Location = New System.Drawing.Point(0, 179);
        Me.grpSearchOptions.Name = "grpSearchOptions";
        Me.grpSearchOptions.Size = New System.Drawing.Size(331, 81);
        Me.grpSearchOptions.TabIndex = 0;
        Me.grpSearchOptions.Text = "Search &options";
        '
        'pnlOptions
        '
        Me.pnlOptions.Controls.Add(Me.chkMatchWholeWord);
        Me.pnlOptions.Controls.Add(Me.chkMatchCase);
        Me.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.pnlOptions.Location = New System.Drawing.Point(3, 19);
        Me.pnlOptions.Name = "pnlOptions";
        Me.pnlOptions.Size = New System.Drawing.Size(325, 59);
        Me.pnlOptions.TabIndex = 0;
        '
        'chkMatchWholeWord
        '
        Me.chkMatchWholeWord.BackColor = System.Drawing.Color.Transparent;
        Me.chkMatchWholeWord.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkMatchWholeWord.Location = New System.Drawing.Point(24, 31);
        Me.chkMatchWholeWord.Name = "chkMatchWholeWord";
        Me.chkMatchWholeWord.Size = New System.Drawing.Size(129, 22);
        Me.chkMatchWholeWord.TabIndex = 1;
        Me.chkMatchWholeWord.Text = "Match whole word";
        '
        'chkMatchCase
        '
        Me.chkMatchCase.BackColor = System.Drawing.Color.Transparent;
        Me.chkMatchCase.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkMatchCase.Location = New System.Drawing.Point(24, 3);
        Me.chkMatchCase.Name = "chkMatchCase";
        Me.chkMatchCase.Size = New System.Drawing.Size(100, 22);
        Me.chkMatchCase.TabIndex = 0;
        Me.chkMatchCase.Text = "Match case";
        '
        'pnlReplace
        '
        Me.pnlReplace.Controls.Add(Me.lblReplace);
        Me.pnlReplace.Controls.Add(Me.cmbReplace);
        Me.pnlReplace.Dock = System.Windows.Forms.DockStyle.Top;
        Me.pnlReplace.Location = New System.Drawing.Point(0, 75);
        Me.pnlReplace.Name = "pnlReplace";
        Me.pnlReplace.Size = New System.Drawing.Size(331, 52);
        Me.pnlReplace.TabIndex = 11;
        '
        'lblReplace
        '
        Me.lblReplace.Location = New System.Drawing.Point(8, 7);
        Me.lblReplace.Name = "lblReplace";
        Me.lblReplace.Size = New System.Drawing.Size(83, 15);
        Me.lblReplace.TabIndex = 1;
        Me.lblReplace.Text = "Re&place with:";
        '
        'cmbReplace
        '
        Me.cmbReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbReplace.Location = New System.Drawing.Point(8, 25);
        Me.cmbReplace.Name = "cmbReplace";
        Me.cmbReplace.Size = New System.Drawing.Size(316, 21);
        Me.cmbReplace.TabIndex = 0;
        '
        'pnlLookIn
        '
        Me.pnlLookIn.Controls.Add(Me.lblLookIn);
        Me.pnlLookIn.Controls.Add(Me.cmbLookIn);
        Me.pnlLookIn.Dock = System.Windows.Forms.DockStyle.Top;
        Me.pnlLookIn.Location = New System.Drawing.Point(0, 127);
        Me.pnlLookIn.Name = "pnlLookIn";
        Me.pnlLookIn.Size = New System.Drawing.Size(331, 52);
        Me.pnlLookIn.TabIndex = 12;
        '
        'lblLookIn
        '
        Me.lblLookIn.Location = New System.Drawing.Point(8, 7);
        Me.lblLookIn.Name = "lblLookIn";
        Me.lblLookIn.Size = New System.Drawing.Size(83, 15);
        Me.lblLookIn.TabIndex = 1;
        Me.lblLookIn.Text = "&Look in:";
        '
        'cmbLookIn
        '
        Me.cmbLookIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.cmbLookIn.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem1.DataValue = 0;
        ValueListItem1.DisplayText = "All items";
        ValueListItem2.DataValue = 1;
        ValueListItem2.DisplayText = "Non-library items";
        Me.cmbLookIn.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2});
        Me.cmbLookIn.Location = New System.Drawing.Point(8, 25);
        Me.cmbLookIn.Name = "cmbLookIn";
        Me.cmbLookIn.Size = New System.Drawing.Size(316, 21);
        Me.cmbLookIn.TabIndex = 0;
        Me.cmbLookIn.Text = "Non-library items";
        '
        'toolsSearchReplace
        '
        Me.toolsSearchReplace.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSelectFind, Me.btnSelectReplace});
        Me.toolsSearchReplace.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
        Me.toolsSearchReplace.Location = New System.Drawing.Point(0, 0);
        Me.toolsSearchReplace.Name = "toolsSearchReplace";
        Me.toolsSearchReplace.Size = New System.Drawing.Size(331, 23);
        Me.toolsSearchReplace.TabIndex = 15;
        Me.toolsSearchReplace.Text = "ToolStrip1";
        '
        'btnSelectFind
        '
        Me.btnSelectFind.Image = Global.ADRIFT.My.Resources.Resources.imgFind;
        Me.btnSelectFind.ImageTransparentColor = System.Drawing.Color.Magenta;
        Me.btnSelectFind.Name = "btnSelectFind";
        Me.btnSelectFind.Size = New System.Drawing.Size(50, 20);
        Me.btnSelectFind.Text = "Find";
        '
        'btnSelectReplace
        '
        Me.btnSelectReplace.Image = Global.ADRIFT.My.Resources.Resources.imgReplace16;
        Me.btnSelectReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
        Me.btnSelectReplace.Name = "btnSelectReplace";
        Me.btnSelectReplace.Size = New System.Drawing.Size(68, 20);
        Me.btnSelectReplace.Text = "Replace";
        '
        'btnFindAll
        '
        Me.btnFindAll.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnFindAll.Location = New System.Drawing.Point(234, 456);
        Me.btnFindAll.Name = "btnFindAll";
        Me.btnFindAll.Size = New System.Drawing.Size(85, 26);
        Me.btnFindAll.TabIndex = 16;
        Me.btnFindAll.Text = "Find &All";
        Me.btnFindAll.Enabled = false;
        '
        'lvwFoundItems
        '
        Me.lvwFoundItems.ContextMenuStrip = Me.cmsItems;
        Me.lvwFoundItems.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.lvwFoundItems.Location = New System.Drawing.Point(0, 260);
        Me.lvwFoundItems.Name = "lvwFoundItems";
        Me.lvwFoundItems.Size = New System.Drawing.Size(331, 190);
        Me.lvwFoundItems.TabIndex = 17;
        Me.lvwFoundItems.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.Details;
        Me.lvwFoundItems.ViewSettingsDetails.AllowColumnMoving = false;
        Me.lvwFoundItems.ViewSettingsDetails.FullRowSelect = true;
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnClose.Location = New System.Drawing.Point(89, 319);
        Me.btnClose.Name = "btnClose";
        Me.btnClose.Size = New System.Drawing.Size(23, 20);
        Me.btnClose.TabIndex = 18;
        Me.btnClose.Text = "Button1";
        Me.btnClose.UseVisualStyleBackColor = true;
        '
        'cmsItems
        '
        Me.cmsItems.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miEditItem, Me.miOpenFolder});
        Me.cmsItems.Name = "cmsItems";
        Me.cmsItems.Size = New System.Drawing.Size(140, 48);
        '
        'miEditItem
        '
        Me.miEditItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold);
        Me.miEditItem.Name = "miEditItem";
        Me.miEditItem.Size = New System.Drawing.Size(152, 22);
        Me.miEditItem.Text = "Edit Item";
        '
        'miOpenFolder
        '
        Me.miOpenFolder.Name = "miOpenFolder";
        Me.miOpenFolder.Size = New System.Drawing.Size(152, 22);
        Me.miOpenFolder.Text = "Open Folder";
        '
        'SearchReplace
        '
        Me.AcceptButton = Me.btnFind;
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.CancelButton = Me.btnClose;
        Me.ClientSize = New System.Drawing.Size(331, 488);
        Me.Controls.Add(Me.lvwFoundItems);
        Me.Controls.Add(Me.btnClose);
        Me.Controls.Add(Me.grpSearchOptions);
        Me.Controls.Add(Me.btnFind);
        Me.Controls.Add(Me.btnFindAll);
        Me.Controls.Add(Me.btnReplace);
        Me.Controls.Add(Me.btnReplaceAll);
        Me.Controls.Add(Me.StatusBar);
        Me.Controls.Add(Me.pnlLookIn);
        Me.Controls.Add(Me.pnlReplace);
        Me.Controls.Add(Me.pnlFind);
        Me.Controls.Add(Me.toolsSearchReplace);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        Me.KeyPreview = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "SearchReplace";
        Me.ShowInTaskbar = false;
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Me.Text = "Search and Replace";
        Me.TopMost = true;
        (System.ComponentModel.ISupportInitialize)(Me.StatusBar).EndInit();
        Me.pnlFind.ResumeLayout(false);
        Me.pnlFind.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbFind).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpSearchOptions).EndInit();
        Me.grpSearchOptions.ResumeLayout(false);
        Me.pnlOptions.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.chkMatchWholeWord).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkMatchCase).EndInit();
        Me.pnlReplace.ResumeLayout(false);
        Me.pnlReplace.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbReplace).EndInit();
        Me.pnlLookIn.ResumeLayout(false);
        Me.pnlLookIn.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.cmbLookIn).EndInit();
        Me.toolsSearchReplace.ResumeLayout(false);
        Me.toolsSearchReplace.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.lvwFoundItems).EndInit();
        Me.cmsItems.ResumeLayout(false);
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents btnFind As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnReplace As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnReplaceAll As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents StatusBar As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents pnlFind As System.Windows.Forms.Panel;
    Friend WithEvents lblFind As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbFind As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents grpSearchOptions As Infragistics.Win.Misc.UltraExpandableGroupBox;
    Friend WithEvents pnlOptions As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel;
    Friend WithEvents pnlReplace As System.Windows.Forms.Panel;
    Friend WithEvents lblReplace As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbReplace As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents pnlLookIn As System.Windows.Forms.Panel;
    Friend WithEvents lblLookIn As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents cmbLookIn As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents chkMatchWholeWord As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents chkMatchCase As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents btnSelectFind As System.Windows.Forms.ToolStripButton;
    Friend WithEvents btnSelectReplace As System.Windows.Forms.ToolStripButton;
    Private WithEvents toolsSearchReplace As System.Windows.Forms.ToolStrip
    Friend WithEvents btnFindAll As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents lvwFoundItems As Infragistics.Win.UltraWinListView.UltraListView;
    Friend WithEvents btnClose As System.Windows.Forms.Button;
    Friend WithEvents cmsItems As System.Windows.Forms.ContextMenuStrip;
    Friend WithEvents miEditItem As System.Windows.Forms.ToolStripMenuItem;
    Friend WithEvents miOpenFolder As System.Windows.Forms.ToolStripMenuItem;

}

}