using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmEvent
{

    public bool bKeepOpen = false;

    public void New(ref lEvent As clsEvent, bool bShow)
    {

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmEvent)
            {
                if (CType(w, frmEvent).cEvent.Key = lEvent.Key)
                {
                    w.BringToFront();
                    w.Focus();
                    Exit Sub;
                }
            }
        Next;

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        Application.EnableVisualStyles();
        LoadForm(lEvent, bShow);
        bKeepOpen = Not bShow

    }


    private clsEvent cEvent;
    private bool bChanged;
    Private WithEvents tmrButtons As New Timer
    private SubEventGUI LastSelectedSubEvent;
    private EventControl LastSelectedControl;
    Private TempSubEvents() As clsEvent.SubEvent
    Private TempControls() As EventOrWalkControl


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

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyEvent();
        CloseEvent(Me);
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyEvent();
        Changed = False
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyEvent()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseEvent(Me);
    }


    private void ApplyEvent()
    {

        With cEvent;
            .EventType = (clsEvent.EventTypeEnum)(optEventType.Value);
            .Description = txtName.Text;
            .Repeating = chkRepeat.Checked;
            .RepeatCountdown = chkRepeatCountdown.Checked;
            .Length.iFrom = x2yHowLong.From;
            .Length.iTo = x2yHowLong.To;

            switch (optStart.CheckedIndex)
            {
                case 0:
                    {
                    .WhenStart = clsEvent.WhenStartEnum.AfterATask;
                case 1:
                    {
                    .WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns;
                    .StartDelay.iFrom = x2yStartWait.From;
                    .StartDelay.iTo = x2yStartWait.To;
                case 2:
                    {
                    .WhenStart = clsEvent.WhenStartEnum.Immediately;
            }

            .SubEvents = TempSubEvents;
            .EventControls = TempControls;
            .LastUpdated = Now;
            .IsLibrary = false;

            If .Description = "" Then .Description = "Unnamed Event"

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Event");
                Adventure.htblEvents.Add(cEvent, .Key);
            }

            UpdateListItem(.Key, .Description);
        }

        Adventure.Changed = true;

    }


    private void LoadForm(ref cEvent As clsEvent, bool bShow)
    {

        Me.cEvent = cEvent;
        ReDim TempSubEvents(cEvent.SubEvents.Length - 1);
        for (int i = 0; i <= cEvent.SubEvents.Length - 1; i++)
        {
            TempSubEvents(i) = cEvent.SubEvents(i).CloneMe;
        Next;
        ReDim TempControls(cEvent.EventControls.Length - 1);
        for (int i = 0; i <= cEvent.EventControls.Length - 1; i++)
        {
            TempControls(i) = cEvent.EventControls(i).CloneMe;
        Next;



        With cEvent;
            Text = "Event - " & .Description
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            If .Description = "" Then Text = "New Event"

            optEventType.Value = CInt(.EventType);
            txtName.Text = .Description;
            chkRepeat.Checked = .Repeating;
            chkRepeatCountdown.Checked = .RepeatCountdown;
            x2yHowLong.SetValues(.Length.iFrom, .Length.iTo);
            switch (.WhenStart)
            {
                case clsEvent.WhenStartEnum.AfterATask:
                    {
                    optStart.CheckedIndex = 0;
                case clsEvent.WhenStartEnum.BetweenXandYTurns:
                    {
                    optStart.CheckedIndex = 1;
                    x2yStartWait.SetValues(.StartDelay.iFrom, .StartDelay.iTo);
                case clsEvent.WhenStartEnum.Immediately:
                    {
                    optStart.CheckedIndex = 2;
            }

            For Each ec As EventOrWalkControl In TempControls ' .EventControls
                AddEventControl(ec);
            Next;
            For Each se As clsEvent.SubEvent In TempSubEvents
                AddSubEvent(se);
            Next;

        }

        If bShow Then Me.Show()
        Changed = False

        OpenForms.Add(Me);

    }


    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void frmEvent_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }

    private void frmEvent_Load(object sender, System.EventArgs e)
    {
        'Me.Icon = Icon.FromHandle(My.Resources.Resources.imgEvent16.GetHicon)
        GetFormPosition(Me);
    }

    private void optStart_ValueChanged(object sender, System.EventArgs e)
    {
        if (optStart.CheckedIndex = 1)
        {
            x2yStartWait.Enabled = true;
            chkRepeatCountdown.Enabled = chkRepeat.Checked;
        Else
            x2yStartWait.Enabled = false;
            chkRepeatCountdown.Enabled = false;
            chkRepeatCountdown.Checked = false;
        }

        Changed = True

    }


    private void btnAddControl_Click(System.Object sender, System.EventArgs e)
    {
        AddEventControl();
    }


    private void AddEventControl(EventOrWalkControl cec = null)
    {

        private bool bAdding = (cec Is null);
        private New EventControl ec;
        ec.Parent = pnlTaskControlInner;
        pnlTaskControlInner.Height = pnlTaskControlInner.Controls.Count * ec.Height;
        ec.Left = 0;
        ec.Top = (pnlTaskControlInner.Controls.Count - 1) * ec.Height;
        ec.Width = pnlTaskControlInner.Width;
        ec.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

        if (Not bAdding)
        {
            ec.LoadEventControl(cec);
        Else
            ReDim Preserve TempControls(TempControls.Length);
            TempControls(TempControls.Length - 1) = ec.cec;
        }

        Changed = True

    }


    private void btnAddSubEvent_Click(System.Object sender, System.EventArgs e)
    {
        AddSubEvent();
    }


    public void DoControlButtons()
    {
        tmrButtons.Interval = 201 ' This gap needs to be enough to allow the click event on the Remove button to fire before we disable it;
        tmrButtons.Enabled = true;
    }

    private void ControlButtons()
    {

        tmrButtons.Enabled = false;

        private bool bRemoveControl = false;
        'Dim bUp As Boolean = False
        'Dim bDown As Boolean = False

        LastSelectedControl = Nothing
        For Each ctrl As EventControl In pnlTaskControlInner.Controls
            if (ctrl.Focused)
            {
                'If Not sev Is pnlSubEventsInner.Controls(0) Then bUp = True
                'If Not sev Is pnlSubEventsInner.Controls(pnlSubEventsInner.Controls.Count - 1) Then bDown = True
                LastSelectedControl = ctrl
                bRemoveControl = True
            }
        Next;

        btnRemoveControl.Enabled = bRemoveControl;
        'btnUp.Enabled = bUp
        'btnDown.Enabled = bDown

    }


    public void DoSubEventButtons()
    {
        tmrButtons.Interval = 200 ' This gap needs to be enough to allow the click event on the Remove button to fire before we disable it;
        tmrButtons.Enabled = true;
    }


    private void SubEventButtons()
    {

        tmrButtons.Enabled = false;

        private bool bRemoveSubEvent = false;
        private bool bUp = false;
        private bool bDown = false;

        LastSelectedSubEvent = Nothing
        For Each sev As SubEventGUI In pnlSubEventsInner.Controls
            if (sev.Focused)
            {
                If ! sev == pnlSubEventsInner.Controls(0) Then bUp = true
                If ! sev == pnlSubEventsInner.Controls(pnlSubEventsInner.Controls.Count - 1) Then bDown = true
                LastSelectedSubEvent = sev
                bRemoveSubEvent = True
            }
        Next;

        btnRemoveSubEvent.Enabled = bRemoveSubEvent;
        btnUp.Enabled = bUp;
        btnDown.Enabled = bDown;

    }


    private void AddSubEvent(clsEvent.SubEvent cse = null)
    {

        private bool bAdding = (cse Is null);
        private int iTop;
        For Each sev As SubEventGUI In pnlSubEventsInner.Controls
            iTop += sev.Height;
        Next;

        private New SubEventGUI se;
        if (CInt(optEventType.Value) = 0)
        {
            se.cmbTurnsSeconds.Value = "Turns";
        Else
            se.cmbTurnsSeconds.Value = "Seconds";
        }
        If pnlSubEventsInner.Controls.Count = 0 Then se.cmbFromStart.Value = "FromStart"
        se.Parent = pnlSubEventsInner;
        pnlSubEventsInner.Height = iTop + se.Height;
        se.Left = 0;
        se.Top = iTop;
        se.Width = pnlSubEventsInner.Width;
        se.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        AddHandler se.ValueChanged, AddressOf Me.CheckEventLength;

        ' Default location dropdown to same as all the rest, if the same
        private string sKey = "";
        For Each ose As clsEvent.SubEvent In TempSubEvents
            if (sKey = "")
            {
                sKey = ose.sKey
            Else
                if (sKey <> ose.sKey)
                {
                    sKey = "***"
                }
            }
        Next;
        if (sKey <> "" && sKey <> "***")
        {
            se.cse.sKey = sKey;
            se.isLocationGroup.Key = sKey;
        }

        'If Not cse Is Nothing Then se.LoadSubEvent(cse)
        'ReDim Preserve TempSubEvents(TempSubEvents.Length)
        'cse = se.cse
        'TempSubEvents(TempSubEvents.Length - 1) = cse
        if (Not bAdding)
        {
            se.LoadSubEvent(cse);
        Else
            ReDim Preserve TempSubEvents(TempSubEvents.Length);
            TempSubEvents(TempSubEvents.Length - 1) = se.cse;
        }

        Changed = True

    }


    private void RemoveSubEvent(SubEventGUI se)
    {

        private bool bShuffle = false;

        RemoveHandler se.ValueChanged, AddressOf CheckEventLength;

        for (int i = 0; i <= TempSubEvents.Length - 1; i++)
        {
            if (TempSubEvents(i) Is se.cse)
            {
                pnlSubEventsInner.Controls.RemoveAt(i);
                se.cse = null;
                bShuffle = True
            }
            If bShuffle && i < TempSubEvents.Length - 1 Then TempSubEvents(i) = TempSubEvents(i + 1)
        Next;
        'pnlSubEventsInner.Controls.Remove(se)
        ReDim Preserve TempSubEvents(TempSubEvents.Length - 2);
        RedrawSubEvents();

        Changed = True

    }


    private void RemoveControl(EventControl ctrl)
    {

        private bool bShuffle = false;

        for (int i = 0; i <= TempControls.Length - 1; i++)
        {
            if (TempControls(i) Is ctrl.cec)
            {
                pnlTaskControlInner.Controls.RemoveAt(i);
                ctrl.cec = null;
                bShuffle = True
            }
            If bShuffle && i < TempControls.Length - 1 Then TempControls(i) = TempControls(i + 1)
        Next;
        'pnlSubEventsInner.Controls.Remove(se)
        ReDim Preserve TempControls(TempControls.Length - 2);
        RedrawControls();

        Changed = True

    }


    public void RedrawSubEvents()
    {

        'For Each se As SubEvent In pnlSubEventsInner.Controls
        private int iHeight = 0;
        if (pnlSubEventsInner.Controls.Count > 0)
        {
            pnlSubEventsInner.Controls(0).Top = 0;
            iHeight = pnlSubEventsInner.Controls(0).Height
        }

        for (int iSE = 1; iSE <= pnlSubEventsInner.Controls.Count - 1; iSE++)
        {
            iHeight += pnlSubEventsInner.Controls(iSE).Height;
            pnlSubEventsInner.Controls(iSE).Top = pnlSubEventsInner.Controls(iSE - 1).Top + pnlSubEventsInner.Controls(iSE - 1).Height;
        Next;
        pnlSubEventsInner.Height = iHeight;
        SubEventButtons();

    }


    public void RedrawControls()
    {

        'For Each se As SubEvent In pnlSubEventsInner.Controls
        private int iHeight = 0;
        if (pnlTaskControlInner.Controls.Count > 0)
        {
            pnlTaskControlInner.Controls(0).Top = 0;
            iHeight = pnlTaskControlInner.Controls(0).Height
        }

        for (int iSE = 1; iSE <= pnlTaskControlInner.Controls.Count - 1; iSE++)
        {
            iHeight += pnlTaskControlInner.Controls(iSE).Height;
            pnlTaskControlInner.Controls(iSE).Top = pnlTaskControlInner.Controls(iSE - 1).Top + pnlTaskControlInner.Controls(iSE - 1).Height;
        Next;
        pnlTaskControlInner.Height = iHeight;
        ControlButtons();

    }


    private void MoveUpDown(bool bIsUp)
    {

        private clsEvent.SubEvent se = LastSelectedSubEvent.cse;
        private clsEvent.SubEvent seSwap;
        private int iIndex;

        for (int i = 0; i <= TempSubEvents.Length - 1; i++)
        {
            if (TempSubEvents(i) Is LastSelectedSubEvent.cse)
            {
                iIndex = i
                Exit For;
            }
        Next;

        if (bIsUp)
        {
            seSwap = TempSubEvents(iIndex - 1)
            LastSelectedSubEvent.LoadSubEvent(seSwap);
            (SubEventGUI)(pnlSubEventsInner.Controls(iIndex - 1)).LoadSubEvent(se);
            (SubEventGUI)(pnlSubEventsInner.Controls(iIndex - 1)).Focus();
        Else
            seSwap = TempSubEvents(iIndex + 1)
            LastSelectedSubEvent.LoadSubEvent(seSwap);
            (SubEventGUI)(pnlSubEventsInner.Controls(iIndex + 1)).LoadSubEvent(se);
            (SubEventGUI)(pnlSubEventsInner.Controls(iIndex + 1)).Focus();
        }

        TempSubEvents(iIndex) = seSwap;
        TempSubEvents(CInt(IIf(bIsUp, iIndex - 1, iIndex + 1))) = se;

        RedrawSubEvents();


    }


    private void btnRemoveSubEvent_MouseDown(object sender, MouseEventArgs e)
    {
        If ! LastSelectedSubEvent == null Then RemoveSubEvent(LastSelectedSubEvent)
    }


    private void btnUpDown_Click(object sender, System.EventArgs e)
    {
        If ! LastSelectedSubEvent == null Then MoveUpDown(sender == btnUp)
    }

    private void tmrButtons_Tick(object sender, System.EventArgs e)
    {
        if (tmrButtons.Interval = 200)
        {
            SubEventButtons();
        Else
            ControlButtons();
        }
    }


    private void btnRemoveControl_Click(object sender, System.EventArgs e)
    {
        If ! LastSelectedControl == null Then RemoveControl(LastSelectedControl)
    }

    private void frmEvent_Shown(object sender, System.EventArgs e)
    {
        if (txtName.Text = "")
        {
            txtName.Focus();
        Else
            ' Dunno...
        }
    }

    private void chkRepeat_CheckedChanged(object sender, System.EventArgs e)
    {

        if (optStart.CheckedIndex = 1)
        {
            chkRepeatCountdown.Enabled = chkRepeat.Checked;
            If ! chkRepeatCountdown.Enabled Then chkRepeatCountdown.Checked = false
        Else
            chkRepeatCountdown.Enabled = false;
            chkRepeatCountdown.Checked = false;
        }
    }

    private void frmEvent_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Events");
    }


    private void optEventType_ValueChanged(object sender, EventArgs e)
    {

        switch (CType(optEventType.Value, clsEvent.EventTypeEnum))
        {
            case clsEvent.EventTypeEnum.TurnBased:
                {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgEvent16.GetHicon);
                lblTurns.Text = "turns";
                lblTurns2.Text = "turns";
            case clsEvent.EventTypeEnum.TimeBased:
                {
                Me.Icon = Icon.FromHandle(My.Resources.Resources.imgTimeEvent16.GetHicon);
                lblTurns.Text = "seconds";
                lblTurns2.Text = "seconds";
        }

        For Each se As SubEventGUI In pnlSubEventsInner.Controls
            se.SortDropdowns();
        Next;

        Changed = True

    }


    private void CheckEventLength(object o, EventArgs e)
    {

        private int iLength = 0;
        private int iPrev = 0;

        For Each se As SubEventGUI In pnlSubEventsInner.Controls
            switch (SafeString(se.cmbFromStart.SelectedItem.DataValue))
            {
                case "FromStart":
                    {
                    iPrev = se.XtoYturns1.To
                    iLength = Math.Max(iLength, iPrev)
                case "FromLast":
                    {
                    iPrev += se.XtoYturns1.To;
                    iLength = Math.Max(iLength, iPrev)
                case "BeforeEnd":
                    {
                    ' ignore
            }
        Next;

        if (x2yHowLong.To < iLength && iLength > 0)
        {
            x2yHowLong.To = iLength;
            If x2yHowLong.txtFrom.Width > x2yHowLong.txtTo.Width Then x2yHowLong.From = iLength
        }

    }

    private void StuffChanged()
    {
        Changed = True
    }

}
}