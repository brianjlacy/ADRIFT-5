using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class EventControl
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
        private Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance;
        Me.cmbStartStop = New Infragistics.Win.UltraWinEditors.UltraComboEditor;
        Me.cmbComplete = New Infragistics.Win.UltraWinEditors.UltraComboEditor;
        Me.lblHighlight = New Infragistics.Win.Misc.UltraLabel;
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox;
        Me.isTasks = New ADRIFT.ItemSelector;
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel;
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel;
        (System.ComponentModel.ISupportInitialize)(Me.cmbStartStop).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbComplete).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        Me.SuspendLayout();
        '
        'cmbStartStop
        '
        Me.cmbStartStop.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem1.DataValue = "Start";
        ValueListItem1.DisplayText = "Start";
        ValueListItem2.DataValue = "Stop";
        ValueListItem2.DisplayText = "Stop";
        ValueListItem3.DataValue = "Suspend";
        ValueListItem3.DisplayText = "Suspend";
        ValueListItem4.DataValue = "Resume";
        ValueListItem4.DisplayText = "Resume";
        Me.cmbStartStop.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4});
        Me.cmbStartStop.Location = New System.Drawing.Point(6, 5);
        Me.cmbStartStop.Name = "cmbStartStop";
        Me.cmbStartStop.Size = New System.Drawing.Size(70, 21);
        Me.cmbStartStop.TabIndex = 1;
        '
        'cmbComplete
        '
        Me.cmbComplete.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem5.DataValue = "completion";
        ValueListItem5.DisplayText = "completion";
        ValueListItem6.DataValue = "uncompletion";
        ValueListItem6.DisplayText = "uncompletion";
        Me.cmbComplete.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem5, ValueListItem6});
        Me.cmbComplete.Location = New System.Drawing.Point(154, 6);
        Me.cmbComplete.Name = "cmbComplete";
        Me.cmbComplete.Size = New System.Drawing.Size(93, 21);
        Me.cmbComplete.TabIndex = 4;
        Me.cmbComplete.Text = "completion";
        '
        'lblHighlight
        '
        Appearance5.BackColor = System.Drawing.SystemColors.Highlight;
        Appearance5.BackColor2 = System.Drawing.SystemColors.Control;
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        Me.lblHighlight.Appearance = Appearance5;
        Me.lblHighlight.Location = New System.Drawing.Point(0, 0);
        Me.lblHighlight.Name = "lblHighlight";
        Me.lblHighlight.Size = New System.Drawing.Size(6, 31);
        Me.lblHighlight.TabIndex = 13;
        Me.lblHighlight.Visible = false;
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel1);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel2);
        Me.UltraGroupBox1.Controls.Add(Me.isTasks);
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(467, 32);
        Me.UltraGroupBox1.TabIndex = 15;
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'isTasks
        '
        Me.isTasks.AllowAddEdit = true;
        Me.isTasks.AllowedListType = ADRIFT.ItemSelector.ItemEnum.Task;
        Me.isTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.isTasks.BackColor = System.Drawing.Color.Transparent;
        Me.isTasks.Key = null;
        Me.isTasks.Location = New System.Drawing.Point(293, 6);
        Me.isTasks.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.isTasks.MinimumSize = New System.Drawing.Size(10, 21);
        Me.isTasks.Name = "isTasks";
        Me.isTasks.Size = New System.Drawing.Size(165, 21);
        Me.isTasks.TabIndex = 17;
        '
        'UltraLabel2
        '
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Appearance = Appearance3;
        Me.UltraLabel2.Location = New System.Drawing.Point(251, 9);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(39, 18);
        Me.UltraLabel2.TabIndex = 18;
        Me.UltraLabel2.Tag = "bg";
        Me.UltraLabel2.Text = "of task";
        '
        'UltraLabel1
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel1.Appearance = Appearance1;
        Me.UltraLabel1.Location = New System.Drawing.Point(82, 9);
        Me.UltraLabel1.Name = "UltraLabel1";
        Me.UltraLabel1.Size = New System.Drawing.Size(70, 18);
        Me.UltraLabel1.TabIndex = 19;
        Me.UltraLabel1.Tag = "bg";
        Me.UltraLabel1.Text = "this event on";
        '
        'EventControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.lblHighlight);
        Me.Controls.Add(Me.cmbComplete);
        Me.Controls.Add(Me.cmbStartStop);
        Me.Controls.Add(Me.UltraGroupBox1);
        Me.Name = "EventControl";
        Me.Size = New System.Drawing.Size(467, 32);
        (System.ComponentModel.ISupportInitialize)(Me.cmbStartStop).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbComplete).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents cmbStartStop As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents cmbComplete As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents lblHighlight As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents isTasks As ADRIFT.ItemSelector;

}

}