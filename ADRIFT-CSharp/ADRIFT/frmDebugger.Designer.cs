using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmDebugger
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
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmDebugger));
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer();
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer();
        Me.treeItemBrowser = New System.Windows.Forms.TreeView();
        Me.pnlEditItems = New System.Windows.Forms.Panel();
        Me.grdProperties = New System.Windows.Forms.DataGridView();
        Me.rtxtDebug = New System.Windows.Forms.RichTextBox();
        Me.Panel1 = New System.Windows.Forms.Panel();
        Me.UltraGroupBox1 = New System.Windows.Forms.GroupBox();
        Me.optHigh = New System.Windows.Forms.RadioButton();
        Me.optMedium = New System.Windows.Forms.RadioButton();
        Me.optLow = New System.Windows.Forms.RadioButton();
        Me.chkShowTimes = New System.Windows.Forms.CheckBox();
        Me.sProperty = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.ValidValues = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.Key = New System.Windows.Forms.DataGridViewTextBoxColumn();
        Me.SplitContainer1.Panel1.SuspendLayout();
        Me.SplitContainer1.Panel2.SuspendLayout();
        Me.SplitContainer1.SuspendLayout();
        Me.SplitContainer2.Panel1.SuspendLayout();
        Me.SplitContainer2.Panel2.SuspendLayout();
        Me.SplitContainer2.SuspendLayout();
        Me.pnlEditItems.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grdProperties).BeginInit();
        Me.Panel1.SuspendLayout();
        Me.UltraGroupBox1.SuspendLayout();
        Me.SuspendLayout();
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0);
        Me.SplitContainer1.Name = "SplitContainer1";
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2);
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtxtDebug);
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1);
        Me.SplitContainer1.Size = New System.Drawing.Size(738, 621);
        Me.SplitContainer1.SplitterDistance = 202;
        Me.SplitContainer1.TabIndex = 3;
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0);
        Me.SplitContainer2.Name = "SplitContainer2";
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.treeItemBrowser);
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlEditItems);
        Me.SplitContainer2.Panel2MinSize = 20;
        Me.SplitContainer2.Size = New System.Drawing.Size(202, 621);
        Me.SplitContainer2.SplitterDistance = 378;
        Me.SplitContainer2.TabIndex = 3;
        '
        'treeItemBrowser
        '
        Me.treeItemBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.treeItemBrowser.HideSelection = false;
        Me.treeItemBrowser.Location = New System.Drawing.Point(0, 0);
        Me.treeItemBrowser.Name = "treeItemBrowser";
        Me.treeItemBrowser.Size = New System.Drawing.Size(202, 378);
        Me.treeItemBrowser.TabIndex = 1;
        '
        'pnlEditItems
        '
        Me.pnlEditItems.Controls.Add(Me.grdProperties);
        Me.pnlEditItems.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.pnlEditItems.Location = New System.Drawing.Point(0, 0);
        Me.pnlEditItems.Name = "pnlEditItems";
        Me.pnlEditItems.Size = New System.Drawing.Size(202, 239);
        Me.pnlEditItems.TabIndex = 4;
        '
        'grdProperties
        '
        Me.grdProperties.AllowUserToAddRows = false;
        Me.grdProperties.AllowUserToDeleteRows = false;
        Me.grdProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        Me.grdProperties.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.sProperty, Me.Value, Me.Type, Me.ValidValues, Me.Key});
        Me.grdProperties.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.grdProperties.Location = New System.Drawing.Point(0, 0);
        Me.grdProperties.Name = "grdProperties";
        Me.grdProperties.RowHeadersVisible = false;
        Me.grdProperties.Size = New System.Drawing.Size(202, 239);
        Me.grdProperties.TabIndex = 0;
        '
        'rtxtDebug
        '
        Me.rtxtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.rtxtDebug.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.rtxtDebug.Location = New System.Drawing.Point(0, 80);
        Me.rtxtDebug.Name = "rtxtDebug";
        Me.rtxtDebug.Size = New System.Drawing.Size(532, 541);
        Me.rtxtDebug.TabIndex = 3;
        Me.rtxtDebug.Text = "";
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraGroupBox1);
        Me.Panel1.Controls.Add(Me.chkShowTimes);
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
        Me.Panel1.Location = New System.Drawing.Point(0, 0);
        Me.Panel1.Name = "Panel1";
        Me.Panel1.Size = New System.Drawing.Size(532, 80);
        Me.Panel1.TabIndex = 2;
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.optHigh);
        Me.UltraGroupBox1.Controls.Add(Me.optMedium);
        Me.UltraGroupBox1.Controls.Add(Me.optLow);
        Me.UltraGroupBox1.Location = New System.Drawing.Point(19, 16);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(275, 50);
        Me.UltraGroupBox1.TabIndex = 9;
        Me.UltraGroupBox1.TabStop = false;
        Me.UltraGroupBox1.Text = "Detail Level";
        '
        'optHigh
        '
        Me.optHigh.AutoSize = true;
        Me.optHigh.Location = New System.Drawing.Point(188, 21);
        Me.optHigh.Name = "optHigh";
        Me.optHigh.Size = New System.Drawing.Size(47, 17);
        Me.optHigh.TabIndex = 7;
        Me.optHigh.Tag = "2";
        Me.optHigh.Text = "High";
        Me.optHigh.UseVisualStyleBackColor = true;
        '
        'optMedium
        '
        Me.optMedium.AutoSize = true;
        Me.optMedium.Checked = true;
        Me.optMedium.Location = New System.Drawing.Point(101, 21);
        Me.optMedium.Name = "optMedium";
        Me.optMedium.Size = New System.Drawing.Size(62, 17);
        Me.optMedium.TabIndex = 6;
        Me.optMedium.TabStop = true;
        Me.optMedium.Tag = "1";
        Me.optMedium.Text = "Medium";
        Me.optMedium.UseVisualStyleBackColor = true;
        '
        'optLow
        '
        Me.optLow.AutoSize = true;
        Me.optLow.Location = New System.Drawing.Point(34, 21);
        Me.optLow.Name = "optLow";
        Me.optLow.Size = New System.Drawing.Size(45, 17);
        Me.optLow.TabIndex = 5;
        Me.optLow.Tag = "0";
        Me.optLow.Text = "Low";
        Me.optLow.UseVisualStyleBackColor = true;
        '
        'chkShowTimes
        '
        Me.chkShowTimes.Location = New System.Drawing.Point(323, 35);
        Me.chkShowTimes.Name = "chkShowTimes";
        Me.chkShowTimes.Size = New System.Drawing.Size(99, 21);
        Me.chkShowTimes.TabIndex = 5;
        Me.chkShowTimes.Text = "Show Times";
        '
        'sProperty
        '
        Me.sProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
        Me.sProperty.Frozen = true;
        Me.sProperty.HeaderText = "Property";
        Me.sProperty.Name = "sProperty";
        Me.sProperty.ReadOnly = true;
        Me.sProperty.Width = 71;
        '
        'Value
        '
        Me.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        Me.Value.HeaderText = "Value";
        Me.Value.Name = "Value";
        '
        'Type
        '
        Me.Type.HeaderText = "Type";
        Me.Type.Name = "Type";
        Me.Type.Visible = false;
        '
        'ValidValues
        '
        Me.ValidValues.HeaderText = "ValidValues";
        Me.ValidValues.Name = "ValidValues";
        Me.ValidValues.Visible = false;
        '
        'Key
        '
        Me.Key.HeaderText = "Key";
        Me.Key.Name = "Key";
        Me.Key.Visible = false;
        '
        'frmDebugger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.ClientSize = New System.Drawing.Size(738, 621);
        Me.Controls.Add(Me.SplitContainer1);
        Me.Icon = (System.Drawing.Icon)(resources.GetObject("$this.Icon"));
        Me.Name = "frmDebugger";
        Me.Text = "ADRIFT Debugger";
        Me.SplitContainer1.Panel1.ResumeLayout(false);
        Me.SplitContainer1.Panel2.ResumeLayout(false);
        Me.SplitContainer1.ResumeLayout(false);
        Me.SplitContainer2.Panel1.ResumeLayout(false);
        Me.SplitContainer2.Panel2.ResumeLayout(false);
        Me.SplitContainer2.ResumeLayout(false);
        Me.pnlEditItems.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.grdProperties).EndInit;
        Me.Panel1.ResumeLayout(false);
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.UltraGroupBox1.PerformLayout;
        Me.ResumeLayout(false);

}
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer;
    Friend WithEvents treeItemBrowser As System.Windows.Forms.TreeView ' Infragistics.Win.UltraWinTree.UltraTree;
    Friend WithEvents Panel1 As System.Windows.Forms.Panel;
    Friend WithEvents rtxtDebug As System.Windows.Forms.RichTextBox;
    Friend WithEvents chkShowTimes As System.Windows.Forms.CheckBox ' Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents UltraGroupBox1 As System.Windows.Forms.GroupBox ' Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer;
    Friend WithEvents pnlEditItems As System.Windows.Forms.Panel;
    'Infragistics.Win.UltraWinEditors.UltraCheckEditor
    ' Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents optHigh As System.Windows.Forms.RadioButton;
    Friend WithEvents optMedium As System.Windows.Forms.RadioButton;
    Friend WithEvents optLow As System.Windows.Forms.RadioButton;
    Friend WithEvents grdProperties As System.Windows.Forms.DataGridView;
    Friend WithEvents sProperty As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents ValidValues As System.Windows.Forms.DataGridViewTextBoxColumn;
    Friend WithEvents Key As System.Windows.Forms.DataGridViewTextBoxColumn;
}

}