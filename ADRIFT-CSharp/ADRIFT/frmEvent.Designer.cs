using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class frmEvent
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
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance4 = new Infragistics.Win.Appearance();
        private Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.ValueListItem ValueListItem8 = new Infragistics.Win.ValueListItem();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        private Infragistics.Win.UltraWinTabControl.UltraTab UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.grpEventType = New Infragistics.Win.Misc.UltraGroupBox();
        Me.optEventType = New Infragistics.Win.UltraWinEditors.UltraOptionSet();
        Me.chkRepeatCountdown = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.btnRemoveControl = New Infragistics.Win.Misc.UltraButton();
        Me.btnAddControl = New Infragistics.Win.Misc.UltraButton();
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel();
        Me.chkRepeat = New Infragistics.Win.UltraWinEditors.UltraCheckEditor();
        Me.pnlTaskControl = New System.Windows.Forms.Panel();
        Me.pnlTaskControlInner = New System.Windows.Forms.Panel();
        Me.grpHowLong = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblTurns2 = New Infragistics.Win.Misc.UltraLabel();
        Me.lblName = New Infragistics.Win.Misc.UltraLabel();
        Me.txtName = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.grpStart = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblTurns = New Infragistics.Win.Misc.UltraLabel();
        Me.optStart = New Infragistics.Win.UltraWinEditors.UltraOptionSet();
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
        Me.btnDown = New Infragistics.Win.Misc.UltraButton();
        Me.btnUp = New Infragistics.Win.Misc.UltraButton();
        Me.btnRemoveSubEvent = New Infragistics.Win.Misc.UltraButton();
        Me.btnAddSubEvent = New Infragistics.Win.Misc.UltraButton();
        Me.pnlSubEvents = New System.Windows.Forms.Panel();
        Me.pnlSubEventsInner = New System.Windows.Forms.Panel();
        Me.tabsEvent = New Infragistics.Win.UltraWinTabControl.UltraTabControl();
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.x2yHowLong = New ADRIFT.XtoYturns();
        Me.x2yStartWait = New ADRIFT.XtoYturns();
        Me.UltraTabPageControl1.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpEventType).BeginInit();
        Me.grpEventType.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.optEventType).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkRepeatCountdown).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkRepeat).BeginInit();
        Me.pnlTaskControl.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpHowLong).BeginInit();
        Me.grpHowLong.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.txtName).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpStart).BeginInit();
        Me.grpStart.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.optStart).BeginInit();
        Me.UltraTabPageControl2.SuspendLayout();
        Me.pnlSubEvents.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.tabsEvent).BeginInit();
        Me.tabsEvent.SuspendLayout();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        Me.SuspendLayout();
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grpEventType);
        Me.UltraTabPageControl1.Controls.Add(Me.chkRepeatCountdown);
        Me.UltraTabPageControl1.Controls.Add(Me.btnRemoveControl);
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddControl);
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel2);
        Me.UltraTabPageControl1.Controls.Add(Me.chkRepeat);
        Me.UltraTabPageControl1.Controls.Add(Me.pnlTaskControl);
        Me.UltraTabPageControl1.Controls.Add(Me.grpHowLong);
        Me.UltraTabPageControl1.Controls.Add(Me.lblName);
        Me.UltraTabPageControl1.Controls.Add(Me.txtName);
        Me.UltraTabPageControl1.Controls.Add(Me.grpStart);
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23);
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1";
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(565, 392);
        '
        'grpEventType
        '
        Appearance1.BackColor = System.Drawing.Color.Transparent;
        Me.grpEventType.Appearance = Appearance1;
        Me.grpEventType.Controls.Add(Me.optEventType);
        Me.grpEventType.Location = New System.Drawing.Point(12, 42);
        Me.grpEventType.Name = "grpEventType";
        Me.grpEventType.Size = New System.Drawing.Size(268, 49);
        Me.grpEventType.TabIndex = 9;
        Me.grpEventType.Text = "Event Type";
        '
        'optEventType
        '
        Me.optEventType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
        Me.optEventType.CheckedIndex = 0;
        ValueListItem4.CheckState = System.Windows.Forms.CheckState.Checked;
        ValueListItem4.DataValue = 0;
        ValueListItem4.DisplayText = " Turn Based";
        ValueListItem5.DataValue = 1;
        ValueListItem5.DisplayText = "Time Based";
        Me.optEventType.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem4, ValueListItem5});
        Me.optEventType.ItemSpacingHorizontal = 30;
        Me.optEventType.ItemSpacingVertical = 8;
        Me.optEventType.Location = New System.Drawing.Point(35, 20);
        Me.optEventType.Name = "optEventType";
        Me.optEventType.Size = New System.Drawing.Size(216, 23);
        Me.optEventType.TabIndex = 1;
        Me.optEventType.Text = " Turn Based";
        Me.optEventType.TextIndentation = 3;
        '
        'chkRepeatCountdown
        '
        Me.chkRepeatCountdown.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.chkRepeatCountdown.BackColor = System.Drawing.Color.Transparent;
        Me.chkRepeatCountdown.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkRepeatCountdown.Enabled = false;
        Me.chkRepeatCountdown.Location = New System.Drawing.Point(192, 364);
        Me.chkRepeatCountdown.Name = "chkRepeatCountdown";
        Me.chkRepeatCountdown.Size = New System.Drawing.Size(124, 23);
        Me.chkRepeatCountdown.TabIndex = 11;
        Me.chkRepeatCountdown.Text = "Repeat countdown";
        '
        'btnRemoveControl
        '
        Me.btnRemoveControl.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnRemoveControl.Enabled = false;
        Me.btnRemoveControl.Location = New System.Drawing.Point(443, 363);
        Me.btnRemoveControl.Name = "btnRemoveControl";
        Me.btnRemoveControl.Size = New System.Drawing.Size(112, 21);
        Me.btnRemoveControl.TabIndex = 10;
        Me.btnRemoveControl.Text = "Remove Control";
        '
        'btnAddControl
        '
        Me.btnAddControl.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnAddControl.Location = New System.Drawing.Point(325, 363);
        Me.btnAddControl.Name = "btnAddControl";
        Me.btnAddControl.Size = New System.Drawing.Size(112, 21);
        Me.btnAddControl.TabIndex = 9;
        Me.btnAddControl.Text = "Add Control";
        '
        'UltraLabel2
        '
        Appearance2.BackColor = System.Drawing.Color.Transparent;
        Me.UltraLabel2.Appearance = Appearance2;
        Me.UltraLabel2.Location = New System.Drawing.Point(11, 158);
        Me.UltraLabel2.Name = "UltraLabel2";
        Me.UltraLabel2.Size = New System.Drawing.Size(87, 16);
        Me.UltraLabel2.TabIndex = 8;
        Me.UltraLabel2.Text = "Task Control...";
        '
        'chkRepeat
        '
        Me.chkRepeat.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.chkRepeat.BackColor = System.Drawing.Color.Transparent;
        Me.chkRepeat.BackColorInternal = System.Drawing.Color.Transparent;
        Me.chkRepeat.Location = New System.Drawing.Point(11, 364);
        Me.chkRepeat.Name = "chkRepeat";
        Me.chkRepeat.Size = New System.Drawing.Size(164, 23);
        Me.chkRepeat.TabIndex = 7;
        Me.chkRepeat.Text = "Repeat event on completion";
        '
        'pnlTaskControl
        '
        Me.pnlTaskControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlTaskControl.AutoScroll = true;
        Me.pnlTaskControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
        Me.pnlTaskControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.pnlTaskControl.Controls.Add(Me.pnlTaskControlInner);
        Me.pnlTaskControl.Location = New System.Drawing.Point(11, 180);
        Me.pnlTaskControl.Name = "pnlTaskControl";
        Me.pnlTaskControl.Size = New System.Drawing.Size(544, 179);
        Me.pnlTaskControl.TabIndex = 6;
        '
        'pnlTaskControlInner
        '
        Me.pnlTaskControlInner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlTaskControlInner.Location = New System.Drawing.Point(-1, -1);
        Me.pnlTaskControlInner.Name = "pnlTaskControlInner";
        Me.pnlTaskControlInner.Size = New System.Drawing.Size(540, 46);
        Me.pnlTaskControlInner.TabIndex = 0;
        '
        'grpHowLong
        '
        Appearance3.BackColor = System.Drawing.Color.Transparent;
        Me.grpHowLong.Appearance = Appearance3;
        Me.grpHowLong.Controls.Add(Me.lblTurns2);
        Me.grpHowLong.Controls.Add(Me.x2yHowLong);
        Me.grpHowLong.Location = New System.Drawing.Point(12, 98);
        Me.grpHowLong.Name = "grpHowLong";
        Me.grpHowLong.Size = New System.Drawing.Size(268, 49);
        Me.grpHowLong.TabIndex = 5;
        Me.grpHowLong.Text = "How long should this event last?";
        '
        'lblTurns2
        '
        Me.lblTurns2.Location = New System.Drawing.Point(155, 23);
        Me.lblTurns2.Name = "lblTurns2";
        Me.lblTurns2.Size = New System.Drawing.Size(48, 19);
        Me.lblTurns2.TabIndex = 8;
        Me.lblTurns2.Text = "turns";
        '
        'lblName
        '
        Me.lblName.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblName.Location = New System.Drawing.Point(8, 12);
        Me.lblName.Name = "lblName";
        Me.lblName.Size = New System.Drawing.Size(72, 16);
        Me.lblName.TabIndex = 4;
        Me.lblName.Text = "Event Name:";
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtName.Location = New System.Drawing.Point(84, 8);
        Me.txtName.Name = "txtName";
        Me.txtName.Size = New System.Drawing.Size(471, 21);
        Me.txtName.TabIndex = 3;
        '
        'grpStart
        '
        Me.grpStart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Appearance4.BackColor = System.Drawing.Color.Transparent;
        Me.grpStart.Appearance = Appearance4;
        Me.grpStart.Controls.Add(Me.lblTurns);
        Me.grpStart.Controls.Add(Me.x2yStartWait);
        Me.grpStart.Controls.Add(Me.optStart);
        Me.grpStart.Location = New System.Drawing.Point(292, 42);
        Me.grpStart.Name = "grpStart";
        Me.grpStart.Size = New System.Drawing.Size(260, 105);
        Me.grpStart.TabIndex = 0;
        Me.grpStart.Text = "This event should start...";
        '
        'lblTurns
        '
        Me.lblTurns.Location = New System.Drawing.Point(201, 51);
        Me.lblTurns.Name = "lblTurns";
        Me.lblTurns.Size = New System.Drawing.Size(53, 19);
        Me.lblTurns.TabIndex = 6;
        Me.lblTurns.Text = "turns";
        '
        'optStart
        '
        Me.optStart.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
        ValueListItem6.DataValue = "Default Item";
        ValueListItem6.DisplayText = "When triggered by a task   (control below)";
        ValueListItem7.DataValue = "ValueListItem2";
        ValueListItem7.DisplayText = "After";
        ValueListItem8.DataValue = "ValueListItem1";
        ValueListItem8.DisplayText = "Immediately";
        Me.optStart.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem6, ValueListItem7, ValueListItem8});
        Me.optStart.ItemSpacingVertical = 9;
        Me.optStart.Location = New System.Drawing.Point(16, 23);
        Me.optStart.Name = "optStart";
        Me.optStart.Size = New System.Drawing.Size(238, 70);
        Me.optStart.TabIndex = 0;
        Me.optStart.TextIndentation = 3;
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.btnDown);
        Me.UltraTabPageControl2.Controls.Add(Me.btnUp);
        Me.UltraTabPageControl2.Controls.Add(Me.btnRemoveSubEvent);
        Me.UltraTabPageControl2.Controls.Add(Me.btnAddSubEvent);
        Me.UltraTabPageControl2.Controls.Add(Me.pnlSubEvents);
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2";
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(565, 392);
        '
        'btnDown
        '
        Me.btnDown.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.btnDown.Enabled = false;
        Me.btnDown.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnDown.Location = New System.Drawing.Point(42, 363);
        Me.btnDown.Name = "btnDown";
        Me.btnDown.Size = New System.Drawing.Size(24, 21);
        Me.btnDown.TabIndex = 14;
        Me.btnDown.Text = "6";
        '
        'btnUp
        '
        Me.btnUp.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
        Me.btnUp.Enabled = false;
        Me.btnUp.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(2));
        Me.btnUp.Location = New System.Drawing.Point(12, 363);
        Me.btnUp.Name = "btnUp";
        Me.btnUp.Size = New System.Drawing.Size(24, 21);
        Me.btnUp.TabIndex = 13;
        Me.btnUp.Text = "5";
        '
        'btnRemoveSubEvent
        '
        Me.btnRemoveSubEvent.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnRemoveSubEvent.Enabled = false;
        Me.btnRemoveSubEvent.Location = New System.Drawing.Point(444, 363);
        Me.btnRemoveSubEvent.Name = "btnRemoveSubEvent";
        Me.btnRemoveSubEvent.Size = New System.Drawing.Size(112, 21);
        Me.btnRemoveSubEvent.TabIndex = 12;
        Me.btnRemoveSubEvent.Text = "Remove Sub Event";
        '
        'btnAddSubEvent
        '
        Me.btnAddSubEvent.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnAddSubEvent.Location = New System.Drawing.Point(326, 363);
        Me.btnAddSubEvent.Name = "btnAddSubEvent";
        Me.btnAddSubEvent.Size = New System.Drawing.Size(112, 21);
        Me.btnAddSubEvent.TabIndex = 11;
        Me.btnAddSubEvent.Text = "Add Sub Event";
        '
        'pnlSubEvents
        '
        Me.pnlSubEvents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlSubEvents.AutoScroll = true;
        Me.pnlSubEvents.BackColor = System.Drawing.SystemColors.AppWorkspace;
        Me.pnlSubEvents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        Me.pnlSubEvents.Controls.Add(Me.pnlSubEventsInner);
        Me.pnlSubEvents.Location = New System.Drawing.Point(11, 12);
        Me.pnlSubEvents.Name = "pnlSubEvents";
        Me.pnlSubEvents.Size = New System.Drawing.Size(545, 347);
        Me.pnlSubEvents.TabIndex = 7;
        '
        'pnlSubEventsInner
        '
        Me.pnlSubEventsInner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.pnlSubEventsInner.Location = New System.Drawing.Point(-1, -2);
        Me.pnlSubEventsInner.Name = "pnlSubEventsInner";
        Me.pnlSubEventsInner.Size = New System.Drawing.Size(541, 46);
        Me.pnlSubEventsInner.TabIndex = 1;
        '
        'tabsEvent
        '
        Me.tabsEvent.Controls.Add(Me.UltraTabSharedControlsPage1);
        Me.tabsEvent.Controls.Add(Me.UltraTabPageControl1);
        Me.tabsEvent.Controls.Add(Me.UltraTabPageControl2);
        Me.tabsEvent.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.tabsEvent.Location = New System.Drawing.Point(0, 0);
        Me.tabsEvent.Name = "tabsEvent";
        Me.tabsEvent.SharedControlsPage = Me.UltraTabSharedControlsPage1;
        Me.tabsEvent.Size = New System.Drawing.Size(569, 418);
        Me.tabsEvent.TabIndex = 20;
        UltraTab1.Key = "tabEventControl";
        UltraTab1.TabPage = Me.UltraTabPageControl1;
        UltraTab1.Text = "Event Control";
        UltraTab2.Key = "tabSubEvents";
        UltraTab2.TabPage = Me.UltraTabPageControl2;
        UltraTab2.Text = "Sub Events";
        Me.tabsEvent.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2});
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000);
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1";
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(565, 392);
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 418);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(569, 48);
        Me.UltraStatusBar1.TabIndex = 16;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(465, 428);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 23;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(369, 428);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 22;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(273, 428);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 21;
        Me.btnOK.Text = "OK";
        '
        'x2yHowLong
        '
        Me.x2yHowLong.From = 0;
        Me.x2yHowLong.Location = New System.Drawing.Point(35, 19);
        Me.x2yHowLong.MaxValue = 2147483647;
        Me.x2yHowLong.MinValue = -2147483648;
        Me.x2yHowLong.Name = "x2yHowLong";
        Me.x2yHowLong.Size = New System.Drawing.Size(116, 22);
        Me.x2yHowLong.TabIndex = 7;
        Me.x2yHowLong.To = 0;
        '
        'x2yStartWait
        '
        Me.x2yStartWait.Enabled = false;
        Me.x2yStartWait.From = 0;
        Me.x2yStartWait.Location = New System.Drawing.Point(81, 47);
        Me.x2yStartWait.MaxValue = 2147483647;
        Me.x2yStartWait.MinValue = -2147483648;
        Me.x2yStartWait.Name = "x2yStartWait";
        Me.x2yStartWait.Size = New System.Drawing.Size(116, 22);
        Me.x2yStartWait.TabIndex = 5;
        Me.x2yStartWait.To = 0;
        '
        'frmEvent
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(569, 466);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.tabsEvent);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(585, 500);
        Me.Name = "frmEvent";
        Me.Text = "Event";
        Me.UltraTabPageControl1.ResumeLayout(false);
        Me.UltraTabPageControl1.PerformLayout();
        (System.ComponentModel.ISupportInitialize)(Me.grpEventType).EndInit();
        Me.grpEventType.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.optEventType).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkRepeatCountdown).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.chkRepeat).EndInit();
        Me.pnlTaskControl.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.grpHowLong).EndInit();
        Me.grpHowLong.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.txtName).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpStart).EndInit();
        Me.grpStart.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.optStart).EndInit();
        Me.UltraTabPageControl2.ResumeLayout(false);
        Me.pnlSubEvents.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.tabsEvent).EndInit();
        Me.tabsEvent.ResumeLayout(false);
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        Me.ResumeLayout(false);

    }
    Friend WithEvents tabsEvent As Infragistics.Win.UltraWinTabControl.UltraTabControl;
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage;
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents grpStart As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents optStart As Infragistics.Win.UltraWinEditors.UltraOptionSet;
    Friend WithEvents lblName As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents txtName As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents x2yStartWait As ADRIFT.XtoYturns;
    Friend WithEvents lblTurns As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents grpHowLong As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents lblTurns2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents x2yHowLong As ADRIFT.XtoYturns;
    Friend WithEvents chkRepeat As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents pnlTaskControl As System.Windows.Forms.Panel;
    Friend WithEvents btnAddControl As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel;
    Friend WithEvents pnlSubEvents As System.Windows.Forms.Panel;
    Friend WithEvents btnRemoveControl As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnRemoveSubEvent As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnAddSubEvent As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents pnlTaskControlInner As System.Windows.Forms.Panel;
    Friend WithEvents pnlSubEventsInner As System.Windows.Forms.Panel;
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents chkRepeatCountdown As Infragistics.Win.UltraWinEditors.UltraCheckEditor;
    Friend WithEvents grpEventType As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents optEventType As Infragistics.Win.UltraWinEditors.UltraOptionSet;
}

}