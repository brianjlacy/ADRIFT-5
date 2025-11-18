using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class SubWalk
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
        private Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem;
        private Infragistics.Win.Appearance Appearance5 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance;
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance;
        Me.cmbFromStart = New Infragistics.Win.UltraWinEditors.UltraComboEditor;
        Me.cmbAction = New Infragistics.Win.UltraWinEditors.UltraComboEditor;
        Me.lblHighlight = New Infragistics.Win.Misc.UltraLabel;
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox;
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel;
        Me.lblAfterWith = New Infragistics.Win.Misc.UltraLabel;
        Me.lblGroup = New Infragistics.Win.Misc.UltraLabel;
        Me.isObjectChar = New ADRIFT.ItemSelector;
        Me.XtoYturns1 = New ADRIFT.XtoYturns;
        Me.txtDescription = New ADRIFT.GenTextbox;
        Me.isLocationGroup = New ADRIFT.ItemSelector;
        Me.isTasks = New ADRIFT.ItemSelector;
        (System.ComponentModel.ISupportInitialize)(Me.cmbFromStart).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAction).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).BeginInit();
        Me.UltraGroupBox1.SuspendLayout();
        Me.SuspendLayout();
        '
        'cmbFromStart
        '
        Me.cmbFromStart.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem1.DataValue = "FromStart";
        ValueListItem1.DisplayText = "from Start of Walk";
        ValueListItem2.DataValue = "FromLast";
        ValueListItem2.DisplayText = "from Last Sub Walk";
        ValueListItem3.DataValue = "BeforeEnd";
        ValueListItem3.DisplayText = "before End of Walk";
        ValueListItem4.DataValue = "ComesAcross";
        ValueListItem4.DisplayText = "character comes across";
        Me.cmbFromStart.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4});
        Me.cmbFromStart.Location = New System.Drawing.Point(215, 10);
        Me.cmbFromStart.Name = "cmbFromStart";
        Me.cmbFromStart.Size = New System.Drawing.Size(154, 21);
        Me.cmbFromStart.TabIndex = 3;
        '
        'cmbAction
        '
        Me.cmbAction.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        Me.cmbAction.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        ValueListItem5.DataValue = "DisplayMessage";
        ValueListItem5.DisplayText = "Display message";
        ValueListItem6.DataValue = "ExecuteTask";
        ValueListItem6.DisplayText = "Run Task";
        ValueListItem7.DataValue = "UnsetTask";
        ValueListItem7.DisplayText = "Unset Task";
        Me.cmbAction.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem5, ValueListItem6, ValueListItem7});
        Me.cmbAction.Location = New System.Drawing.Point(375, 10);
        Me.cmbAction.Name = "cmbAction";
        Me.cmbAction.Size = New System.Drawing.Size(149, 21);
        Me.cmbAction.TabIndex = 4;
        '
        'lblHighlight
        '
        Appearance5.BackColor = System.Drawing.SystemColors.Highlight;
        Appearance5.BackColor2 = System.Drawing.Color.Transparent;
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
        Me.lblHighlight.Appearance = Appearance5;
        Me.lblHighlight.Location = New System.Drawing.Point(2, 3);
        Me.lblHighlight.Name = "lblHighlight";
        Me.lblHighlight.Size = New System.Drawing.Size(6, 148);
        Me.lblHighlight.TabIndex = 12;
        Me.lblHighlight.Visible = false;
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.isTasks);
        Me.UltraGroupBox1.Controls.Add(Me.UltraLabel2);
        Me.UltraGroupBox1.Controls.Add(Me.isObjectChar);
        Me.UltraGroupBox1.Controls.Add(Me.XtoYturns1);
        Me.UltraGroupBox1.Controls.Add(Me.lblAfterWith);
        Me.UltraGroupBox1.Controls.Add(Me.txtDescription);
        Me.UltraGroupBox1.Controls.Add(Me.isLocationGroup);
        Me.UltraGroupBox1.Controls.Add(Me.lblGroup);
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0);
        Me.UltraGroupBox1.Name = "UltraGroupBox1";
        Me.UltraGroupBox1.Size = New System.Drawing.Size(538, 153);
        Me.UltraGroupBox1.TabIndex = 15;
        Me.UltraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'UltraLabel2
        '
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Appearance = Appearance2;
        Me.UltraLabel2.Location = New System.Drawing.Point(180, 13);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(30, 23);
        Me.UltraLabel2.TabIndex = 18;
        Me.UltraLabel2.Tag = "bg";
        Me.UltraLabel2.Text = "turns";
        '
        'lblAfterWith
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.lblAfterWith.Appearance = Appearance1;
        Me.lblAfterWith.Location = New System.Drawing.Point(8, 13);
        Me.lblAfterWith.Name = "lblAfterWith";
        Me.lblAfterWith.Size = New System.Drawing.Size(34, 15);
        Me.lblAfterWith.TabIndex = 15;
        Me.lblAfterWith.Tag = "bg";
        Me.lblAfterWith.Text = "After";
        '
        'lblGroup
        '
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Me.lblGroup.Appearance = Appearance3;
        Me.lblGroup.Location = New System.Drawing.Point(8, 127);
        Me.lblGroup.Name = "lblGroup";
        Me.lblGroup.Size = New System.Drawing.Size(252, 17);
        Me.lblGroup.TabIndex = 8;
        Me.lblGroup.Tag = "bg";
        Me.lblGroup.Text = "This will only apply when the Player is at location:";
        '
        'isObjectChar
        '
        Me.isObjectChar.AllowAddEdit = true;
        Me.isObjectChar.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.[Object] | ADRIFT.ItemSelector.ItemEnum.Character));
        Me.isObjectChar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.isObjectChar.BackColor = System.Drawing.Color.Transparent;
        Me.isObjectChar.Key = null;
        Me.isObjectChar.Location = New System.Drawing.Point(189, 10);
        Me.isObjectChar.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.isObjectChar.MinimumSize = New System.Drawing.Size(10, 21);
        Me.isObjectChar.Name = "isObjectChar";
        Me.isObjectChar.Size = New System.Drawing.Size(182, 21);
        Me.isObjectChar.TabIndex = 17;
        Me.isObjectChar.Visible = false;
        '
        'XtoYturns1
        '
        Me.XtoYturns1.BackColor = System.Drawing.Color.Transparent;
        Me.XtoYturns1.From = 0;
        Me.XtoYturns1.Location = New System.Drawing.Point(41, 8);
        Me.XtoYturns1.MaxValue = 2147483647;
        Me.XtoYturns1.MinValue = -2147483648;
        Me.XtoYturns1.Name = "XtoYturns1";
        Me.XtoYturns1.Size = New System.Drawing.Size(132, 23);
        Me.XtoYturns1.TabIndex = 16;
        Me.XtoYturns1.To = 0;
        '
        'txtDescription
        '
        Me.txtDescription.AllowAlternateDescriptions = true;
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDescription.BackColor = System.Drawing.Color.Transparent;
        Me.txtDescription.Location = New System.Drawing.Point(8, 37);
        Me.txtDescription.Name = "txtDescription";
        Me.txtDescription.sCommand = null;
        Me.txtDescription.Size = New System.Drawing.Size(524, 84);
        Me.txtDescription.TabIndex = 14;
        '
        'isLocationGroup
        '
        Me.isLocationGroup.AllowAddEdit = true;
        Me.isLocationGroup.AllowedListType = (ADRIFT.ItemSelector.ItemEnum)((ADRIFT.ItemSelector.ItemEnum.Location | ADRIFT.ItemSelector.ItemEnum.LocationGroup));
        Me.isLocationGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.isLocationGroup.BackColor = System.Drawing.Color.Transparent;
        Me.isLocationGroup.Key = null;
        Me.isLocationGroup.Location = New System.Drawing.Point(256, 123);
        Me.isLocationGroup.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.isLocationGroup.MinimumSize = New System.Drawing.Size(10, 21);
        Me.isLocationGroup.Name = "isLocationGroup";
        Me.isLocationGroup.Size = New System.Drawing.Size(268, 21);
        Me.isLocationGroup.TabIndex = 13;
        '
        'isTasks
        '
        Me.isTasks.AllowAddEdit = true;
        Me.isTasks.AllowedListType = ADRIFT.ItemSelector.ItemEnum.Task;
        Me.isTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.isTasks.BackColor = System.Drawing.Color.Transparent;
        Me.isTasks.Key = null;
        Me.isTasks.Location = New System.Drawing.Point(215, 37);
        Me.isTasks.MaximumSize = New System.Drawing.Size(1000, 21);
        Me.isTasks.MinimumSize = New System.Drawing.Size(10, 21);
        Me.isTasks.Name = "isTasks";
        Me.isTasks.Size = New System.Drawing.Size(309, 21);
        Me.isTasks.TabIndex = 19;
        Me.isTasks.Visible = false;
        '
        'SubWalk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.lblHighlight);
        Me.Controls.Add(Me.cmbAction);
        Me.Controls.Add(Me.cmbFromStart);
        Me.Controls.Add(Me.UltraGroupBox1);
        Me.Name = "SubWalk";
        Me.Size = New System.Drawing.Size(538, 153);
        (System.ComponentModel.ISupportInitialize)(Me.cmbFromStart).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.cmbAction).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.UltraGroupBox1).EndInit();
        Me.UltraGroupBox1.ResumeLayout(false);
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }
    Friend WithEvents cmbFromStart As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents cmbAction As Infragistics.Win.UltraWinEditors.UltraComboEditor;
    Friend WithEvents lblHighlight As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents isObjectChar As ADRIFT.ItemSelector;
    Friend WithEvents XtoYturns1 As ADRIFT.XtoYturns;
    Friend WithEvents lblAfterWith As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtDescription As ADRIFT.GenTextbox;
    Friend WithEvents isLocationGroup As ADRIFT.ItemSelector;
    Friend WithEvents lblGroup As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents isTasks As ADRIFT.ItemSelector;

}

}