using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsEvent
{
    Inherits clsItem;

    'Private sKey As String
    private string sDescription;
public enum WhenStartEnum
    {
        Immediately = 1
        BetweenXandYTurns = 2
        AfterATask = 3
    }
    internal WhenStartEnum WhenStart;
    private bool bRepeating;
    internal EventDescriptionArrayList arlEventDescriptions;
    private int iTimerToEndOfEvent;
    internal int iLastSubEventTime = 0;
    private New Stack stackLookText;
private enum Command
    {
        [null] = 0;
        Start = 1
        [Stop] = 2;
        Pause = 3
        [Resume] = 4;
    }
    private Command NextCommand = Command.null;

private class clsLookText
    {
        public string sDescription;
        public string sLocationKey;

        public void New(string sKey, string sDescription)
        {
            Me.sLocationKey = sKey;
            Me.sDescription = sDescription;
        }

    }


public enum StatusEnum
    {
        NotYetStarted = 0
        Running = 1
        CountingDownToStart = 2
        Paused = 3
        Finished = 4
    }
    internal StatusEnum Status;

    Friend EventControls() As EventOrWalkControl = {}

public class SubEvent
    {
        Implements ICloneable;

        private string sParentKey;
        public void New(string sEventKey)
        {
            sParentKey = sEventKey
        }


public enum WhenEnum
        {
            FromLastSubEvent;
            FromStartOfEvent;
            BeforeEndOfEvent;
        }
        public WhenEnum eWhen;

public enum WhatEnum
        {
            DisplayMessage;
            SetLook;
            ExecuteTask;
            UnsetTask;
        }
        public WhatEnum eWhat;

public enum MeasureEnum
        {
            Turns;
            Seconds;
        }
        public MeasureEnum eMeasure;

        internal New FromTo ftTurns;
        internal New Description oDescription;
        public string sKey;

        private Object Implements System.ICloneable.Clone Clone()
        {
            private SubEvent se;
            se = CType(Me.MemberwiseClone, SubEvent)
            se.ftTurns = ftTurns.CloneMe;
            se.oDescription = oDescription.Copy;
            return se;
        }
        public SubEvent CloneMe()
        {
            return CType(Me.Clone, SubEvent);
        }

#if Runner
        Public WithEvents tmrTrigger As System.Windows.Forms.Timer
        public DateTime dtStart;
        public int iMilliseconds;

        private void tmrTrigger_Tick(object sender, System.EventArgs e)
        {
            tmrTrigger.Enabled = false;
            tmrTrigger.Stop();
            UserSession.TriggerTimerEvent(sParentKey, Me);
        }
#endif

    }


    Public SubEvents() As SubEvent = {}


    internal FromTo StartDelay;
    internal FromTo Length;


    'Public Property Key() As String
    '    Get
    '        Return sKey
    '    End Get
    '    Set(ByVal Value As String)
    '        If Not KeyExists(Value) Then
    '            sKey = Value
    '        Else
    '            Throw New Exception("Key " & sKey & " already exists")
    '        End If
    '    End Set
    'End Property


    public string Description { get; set; }
        {
            get
            {
            return sDescription;
        }
set(ByVal Value As String)
            sDescription = Value
        }
    }

    public bool Repeating { get; set; }
        {
            get
            {
            return bRepeating;
        }
set(ByVal Boolean)
            bRepeating = value
        }
    }

    public void New()
    {
        Me.StartDelay = New FromTo;
        Me.Length = New FromTo;
    }

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            private clsEvent ev = CType(Me.MemberwiseClone, clsEvent);
            ev.StartDelay = ev.StartDelay.CloneMe;
            ev.Length = ev.Length.CloneMe;
            return ev;
        }
    }

#if Runner

    public string LookText()
    {

        if (Status = StatusEnum.Running)
        {

            ' Pop the first matching LookText off the stack
            private bool bOkToDisplay = false;
            private string sLookText = null;

            For Each lt As clsLookText In stackLookText.ToArray
                if (Adventure.Player.IsInGroupOrLocation(lt.sLocationKey))
                {
                    sLookText = lt.sDescription
                    bOkToDisplay = True
                    Exit For;
                }
            Next;

            If bOkToDisplay Then Return sLookText

        }

        return null;

    }


    public int TimerToEndOfEvent { get; set; }
        {
            get
            {
            return iTimerToEndOfEvent;
        }
set(ByVal Integer)
            private int iStartValue = iTimerToEndOfEvent;
            iTimerToEndOfEvent = value

            ' If the timer has ticked down and we're ready to start
            if (Status = StatusEnum.CountingDownToStart && TimerFromStartOfEvent = 0 Then ' && iStartValue < 0)
            {
                Start(true);
            }

            ' If we've reached the end of the timer
            if (Status = StatusEnum.Running && iTimerToEndOfEvent = 0 Then ' iTimerToEndOfEvent < 0 && iStartValue > iTimerToEndOfEvent)
            {
                lStop(true);
            }

        }
    }

    internal SubEvent LastSubEvent;
    private int TimerFromLastSubEvent { get; }
        {
            get
            {
            return TimerFromStartOfEvent - iLastSubEventTime;
        }
    }
    private int TimerFromStartOfEvent { get; }
        {
            get
            {
            return Length.Value - TimerToEndOfEvent ' + 1;
        }
    }


    public string sTriggeringTask;


    public bool bJustStarted = false;
    public void Start(bool bForce = false)
    {
        if (bForce || UserSession.bEventsRunning)
        {
            lStart();
        Else
            NextCommand = Command.Start
        }
    }
    private void lStart(bool bRestart = false)
    {
        if (Status = StatusEnum.NotYetStarted || Status = StatusEnum.CountingDownToStart || Status = StatusEnum.Finished || (Status = StatusEnum.Running && bRestart))
        {
            If ! bRestart Then UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "Starting event " + Description)
            Status = StatusEnum.Running
            Length.Reset();

            LastSubEvent = Nothing
            iLastSubEventTime = 0

            For Each se As clsEvent.SubEvent In SubEvents
                se.ftTurns.Reset();
                if (se.eMeasure = SubEvent.MeasureEnum.Seconds)
                {
                    se.tmrTrigger = New System.Windows.Forms.Timer;
                    if (se.eWhen = SubEvent.WhenEnum.FromStartOfEvent || (se.eWhen = SubEvent.WhenEnum.FromLastSubEvent && se Is SubEvents(0)))
                    {
                        se.tmrTrigger.Interval = se.ftTurns.Value * 1000;
                        se.tmrTrigger.Start();
                        se.dtStart = Now;
                    }
                }
            Next;

            ' WHAT TO DO HERE?
            ' If it's length 0, we need to run our start actions
            ' if it's length 2 we don't want it being set to 1 immediately from the incrementtimer
            'Dim bRunBefore As Boolean = False
            'If Length.Value = 0 Then bRunBefore = True
            'If bRunBefore Then DoAnySubEvents() ' Because setting TimerToEnd below will trigger a stop for 0 legnth event
            TimerToEndOfEvent = Length.Value '+ 1 ' Needs to be +1 because we'll increment it the same turn we start it
            'If Not bRunBefore Then DoAnySubEvents()
            ''If Length.Value > 0 Then DoAnySubEvents()
            If TimerFromStartOfEvent = 0 Then DoAnySubEvents() ' To run 'after 0 turns' subevents

            If WhenStart = WhenStartEnum.Immediately Then WhenStart = WhenStartEnum.BetweenXandYTurns ' So we get 'after 0 turns' on any repeats
            bJustStarted = True
        Else
            'Throw New Exception("Can't Start an Event that isn't waiting!")
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Error, "Can't Start an Event that isn't waiting!");
        }
    }


    public void Pause()
    {
        If UserSession.bEventsRunning Then lPause() Else NextCommand = Command.Pause
    }
    private void lPause()
    {
        if (Status = StatusEnum.Running)
        {
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "Pausing event " + Description);
            Status = StatusEnum.Paused
            For Each se As SubEvent In SubEvents
                if (se.tmrTrigger IsNot null && se.tmrTrigger.Enabled)
                {
                    se.iMilliseconds = CInt(se.ftTurns.Value * 1000 - Now.Subtract(se.dtStart).TotalMilliseconds);
                    se.tmrTrigger.Stop();
                }
            Next;
        Else
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Error, "Can't Pause an Event that isn't running!");
        }
    }


    Public Sub [Resume]()
        If UserSession.bEventsRunning Then lResume() Else NextCommand = Command.Resume
    }
    private void lResume()
    {
        if (Status = StatusEnum.Paused)
        {
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "Resuming event " + Description);
            Status = StatusEnum.Running
            For Each se As SubEvent In SubEvents
                if (se.tmrTrigger IsNot null)
                {
                    if (se.iMilliseconds > 0)
                    {
                        se.tmrTrigger.Interval = se.iMilliseconds;
                        se.iMilliseconds = 0;
                        se.tmrTrigger.Start();
                    }
                }
            Next;
        Else
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Error, "Can't Resume an Event that isn't paused!");
        }
    }


    Public Sub [Stop]()
        ' If an event runs a task and that task starts/stops an event, do it immediately
        If UserSession.bEventsRunning Then lStop() Else NextCommand = Command.Stop
    }
    private void lStop(bool bRunSubEvents = false)
    {

        If bRunSubEvents Then DoAnySubEvents()
        If Status = StatusEnum.Paused Then Exit Sub
        Status = StatusEnum.Finished
        For Each se As SubEvent In SubEvents
            If se.tmrTrigger != null Then se.tmrTrigger.Stop()
        Next;
        if (Me.bRepeating && TimerToEndOfEvent = 0)
        {
            if (Length.Value > 0)
            {
                UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "Restarting event " + Description);
                lStart(true) ' Make sure we don't get ourselves in a loop for zero length events;
            Else
                UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "! restarting event " + Description + " otherwise we'd get in an infinite loop as zero length.");
            }
        Else
            UserSession.DebugPrint(ItemEnum.Event, Me.Key, DebugDetailLevelEnum.Low, "Finishing event " + Description);
        }

    }


    public void IncrementTimer()
    {

        if (NextCommand <> Command.null)
        {
            switch (NextCommand)
            {
                case Command.Start:
                    {
                    lStart();
                case Command.Stop:
                    {
                    lStop();
                case Command.Pause:
                    {
                    lPause();
                case Command.Resume:
                    {
                    lResume();
            }
            NextCommand = Command.Nothing
            sTriggeringTask = ""
        }

        If Status = StatusEnum.Running || Status = StatusEnum.CountingDownToStart Then UserSession.DebugPrint(ItemEnum.Event, Key, DebugDetailLevelEnum.High, "Event " + Description + " [" + TimerFromStartOfEvent + 1 + "/" + Length.Value + "]")

        ' Why are we running subevents before we've incremented the timer?
        'If TimerToEndOfEvent > 0 AndAlso TimerFromStartOfEvent > 0 Then DoAnySubEvents()

        ' Split this into 2 case statements, as changing timer here may change status
        switch (Status)
        {
            case StatusEnum.NotYetStarted:
                {
            case StatusEnum.CountingDownToStart:
                {
                TimerToEndOfEvent -= 1;
            case StatusEnum.Running:
                {
                If ! bJustStarted Then TimerToEndOfEvent -= 1
            case StatusEnum.Paused:
                {
            case StatusEnum.Finished:
                {
        }

        If ! bJustStarted Then DoAnySubEvents()

        bJustStarted = False
        'If Status = StatusEnum.Running Then DebugPrint(ItemEnum.Event, Key, DebugDetailLevelEnum.High, "Event " & Description & " [" & TimerFromStartOfEvent & "/" & Length.Value & "]")

    }


    'Friend Enum WhatSubEvent
    '    Starting
    '    Stopping
    '    Running
    'End Enum
    internal void DoAnySubEvents()
    {

        switch (Status)
        {
            case StatusEnum.Running:
                {
                ' Check all the subevents to see if we need to do anything
                private int iIndex = 0;
                For Each se As SubEvent In SubEvents
                    if (se.eMeasure = SubEvent.MeasureEnum.Turns)
                    {
                        private bool bRunSubEvent = false;
                        switch (se.eWhen)
                        {
                            case SubEvent.WhenEnum.FromStartOfEvent:
                                {
                                If TimerFromStartOfEvent = se.ftTurns.Value && se.ftTurns.Value <= Length.Value && (se.ftTurns.Value > 0 || Me.WhenStart <> WhenStartEnum.Immediately) Then bRunSubEvent = true
                            case SubEvent.WhenEnum.FromLastSubEvent:
                                {
                                if (TimerFromLastSubEvent = se.ftTurns.Value)
                                {
                                    If (LastSubEvent == null && iIndex = 0) || (iIndex > 0 && LastSubEvent == SubEvents(iIndex - 1)) Then bRunSubEvent = true
                                }
                            case SubEvent.WhenEnum.BeforeEndOfEvent:
                                {
                                If TimerToEndOfEvent = se.ftTurns.Value Then bRunSubEvent = true
                        }

                        If bRunSubEvent Then RunSubEvent(se)

                    }
                    iIndex += 1;
                Next;
        }

    }


    public void RunSubEvent(SubEvent se)
    {

        switch (se.eWhat)
        {
            case SubEvent.WhatEnum.DisplayMessage:
                {
                If se.sKey <> "" && Adventure.Player.IsInGroupOrLocation(se.sKey) Then UserSession.Display(se.oDescription.ToString)
            case SubEvent.WhatEnum.ExecuteTask:
                {
                if (Adventure.htblTasks.ContainsKey(se.sKey))
                {
                    UserSession.DebugPrint(ItemEnum.Event, Key, DebugDetailLevelEnum.Medium, "Event '" + Description + "' attempting to execute task '" + Adventure.htblTasks(se.sKey).Description + "'");
                    UserSession.AttemptToExecuteTask(se.sKey, true, , , , , , , true);
                }
            case SubEvent.WhatEnum.SetLook:
                {
                ' Push a LookText onto the stack
                stackLookText.Push(New clsLookText(se.sKey, se.oDescription.ToString));
            case SubEvent.WhatEnum.UnsetTask:
                {
                UserSession.DebugPrint(ItemEnum.Event, Key, DebugDetailLevelEnum.Medium, "Event '" + Description + "' unsetting task '" + Adventure.htblTasks(se.sKey).Description + "'");
                Adventure.htblTasks(se.sKey).Completed = false;
        }
        iLastSubEventTime = TimerFromStartOfEvent
        LastSubEvent = se

        private int i = 0;
        For Each ose As SubEvent In SubEvents
            i += 1;
            if (ose Is se)
            {
                if (i < SubEvents.Length)
                {
                    With SubEvents(i);
                        if (.eMeasure = SubEvent.MeasureEnum.Seconds)
                        {
                            if (.eWhen = SubEvent.WhenEnum.FromLastSubEvent)
                            {
                                .tmrTrigger = New System.Windows.Forms.Timer;
                                if (.ftTurns.Value > 0)
                                {
                                    .tmrTrigger.Interval = .ftTurns.Value * 1000;
                                    .tmrTrigger.Start();
                                    .dtStart = Now;
                                Else
                                    RunSubEvent(SubEvents(i));
                                }
                            }
                        }
                    }
                    Exit For;
                }
            }
        Next;
    }


#endif

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Description;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            For Each se As SubEvent In SubEvents
                all.Add(se.oDescription);
            Next;
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sDescription, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmEvent(Me, True) fEvent;
#endif
    }

    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        for (int i = 0; i <= EventControls.Length - 1; i++)
        {
            If EventControls(i).sTaskKey = sKey Then iCount += 1
        Next;
        for (int i = 0; i <= SubEvents.Length - 1; i++)
        {
            With SubEvents(i);
                iCount += .oDescription.ReferencesKey(sKey);
                If .sKey = sKey Then iCount += 1
            }
        Next;
        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        for (int i = EventControls.Length - 1; i <= 0; i += -1)
        {
            if (EventControls(i).sTaskKey = sKey)
            {
                for (int j = EventControls.Length - 2; j <= i; j += -1)
                {
                    EventControls(j) = EventControls(j + 1);
                Next;
                ReDim Preserve EventControls(EventControls.Length - 2);
            }
        Next;
        for (int i = 0; i <= SubEvents.Length - 1; i++)
        {
            With SubEvents(i);
                If ! .oDescription.DeleteKey(sKey) Then Return false
                If .sKey = sKey Then .sKey = ""
            }
        Next;

        return true;

    }

}


public class clsEventDescription
{

    internal New StringArrayList arlShowInRooms;
public enum WhenShowEnum
    {
        EventStart;
        WhenPlayerLooks;
        XTurnsFromStart;
        XTurnsFromFinish;
        EventFinish;
    }
    internal WhenShowEnum WhenShow;

}
}