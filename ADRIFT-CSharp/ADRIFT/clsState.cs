using ComponentAce.Compression.Libs.zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ADRIFT
{


' Gives us the same functionality as a stack, but restricts it to 100
internal class MyStack
{

    private const int MAXLENGTH = 100;

    Private States(MAXLENGTH) As clsGameState
    private int iStart = -1;
    private int iEnd = -1;

    internal void Push(clsGameState state)
    {
        If iStart > iEnd Then iStart += 1
        If iStart > MAXLENGTH Then iStart = 0
        iEnd += 1;

        if (iEnd > MAXLENGTH)
        {
            iEnd = 0
            If iStart = 0 Then iStart = 1
        }
        If iStart = -1 Then iStart = 0

        States(iEnd) = state;
    }


    internal int Count()
    {
        if (iEnd >= iStart)
        {
            If iStart = -1 && iEnd = -1 Then Return 0
            return iEnd - iStart + 1;
        Else
            return MAXLENGTH + 1 ' iEnd + MAXLENGTH - iStart;
        }
    }


    internal clsGameState Peek()
    {
        If Count() = 0 Then Return null
        return States(iEnd);
    }


    internal clsGameState Pop()
    {

        private clsGameState state = Peek();
        if (state IsNot null)
        {
            iEnd -= 1;
            if (iEnd < 0)
            {
                iEnd = MAXLENGTH
            }
        }
        return state;

    }


    internal void Clear()
    {
        iStart = -1
        iEnd = -1
    }

}


internal class StateStack
{
    Inherits MyStack;


    Friend Shadows Sub Push(ByVal state As clsGameState);
        If stateCurrent != null Then stateLast = stateCurrent
        MyBase.Push(state);
        Debug.WriteLine("Pushed (" + MyBase.Count + " on stack)");
        stateCurrent = state
    }

    Shadows Sub Pop();
        RestoreState((clsGameState)(MyBase.Pop));
        Debug.WriteLine("Popped (" + MyBase.Count + " on stack)");
    }



    private void SaveDisplayOnce(List(Of Description AllDescriptions)
    {

        private int iDesc = 0;
        For Each d As Description In AllDescriptions
            iDesc += 1;
            private int iSing = 0;
            For Each sd As SingleDescription In d
                iSing += 1;
                if (sd.DisplayOnce && sd.Displayed)
                {
                    private string sKey = iDesc + "-" + iSing;
                    htblStore.Add(sKey, true);
                }
            Next;
        Next;
    }


    ' Get the current game state, and store in a GameState class
    internal clsGameState GetState()
    {
        private New clsGameState NewState;

        With NewState;

            .sOutputText = UserSession.sTurnOutput;

            For Each loc As clsLocation In Adventure.htblLocations.Values
                private New clsGameState.clsLocationState locs;
                For Each sPropKey As String In loc.htblActualProperties.Keys ' loc.htblProperties.Keys
                    private clsProperty prop = loc.htblProperties(sPropKey);
                    private New clsGameState.clsLocationState.clsStateProperty props;
                    props.Value = prop.Value(true);
                    locs.htblProperties.Add(sPropKey, props);
                Next;
                SaveDisplayOnce(loc.AllDescriptions, locs.htblDisplayedDescriptions);
                'locs.bSeen = UserSession.Map.Map.FindNode(loc.Key).Seen
                locs.bSeen = Adventure.Player.HasSeenLocation(loc.Key);
                .htblLocationStates.Add(loc.Key, locs);
            Next;

            For Each ob As clsObject In Adventure.htblObjects.Values
                private New clsGameState.clsObjectState obs;
                obs.Location = ob.Location.Copy;
                For Each sPropKey As String In ob.htblActualProperties.Keys ' ob.htblProperties.Keys
                    private clsProperty prop = ob.htblProperties(sPropKey);
                    private New clsGameState.clsObjectState.clsStateProperty props;
                    props.Value = prop.Value(true);
                    obs.htblProperties.Add(sPropKey, props);
                Next;
                SaveDisplayOnce(ob.AllDescriptions, obs.htblDisplayedDescriptions);
                .htblObjectStates.Add(ob.Key, obs);
            Next;

            For Each tas As clsTask In Adventure.htblTasks.Values
                private New clsGameState.clsTaskState tass;
                tass.Completed = tas.Completed;
                tass.Scored = tas.Scored;
                SaveDisplayOnce(tas.AllDescriptions, tass.htblDisplayedDescriptions);
                .htblTaskStates.Add(tas.Key, tass);
            Next;

            For Each ev As clsEvent In Adventure.htblEvents.Values
                private New clsGameState.clsEventState evs;
                evs.Status = ev.Status;
                evs.TimerToEndOfEvent = ev.TimerToEndOfEvent;
                evs.iLastSubEventTime = ev.iLastSubEventTime;
                for (int i = 0; i <= ev.SubEvents.Length - 1; i++)
                {
                    if (ev.LastSubEvent Is ev.SubEvents(i))
                    {
                        evs.iLastSubEventIndex = i;
                        Exit For;
                    }
                Next;
                SaveDisplayOnce(ev.AllDescriptions, evs.htblDisplayedDescriptions);
                .htblEventStates.Add(ev.Key, evs);
            Next;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                private New clsGameState.clsCharacterState chs;
                chs.Location = ch.Location;
                For Each w As clsWalk In ch.arlWalks
                    private New clsGameState.clsCharacterState.clsWalkState ws;
                    ws.Status = w.Status;
                    ws.TimerToEndOfWalk = w.iTimerToEndOfWalk;
                    chs.lWalks.Add(ws);
                Next;
                chs.lSeenKeys.Clear();
                For Each sLocKey As String In Adventure.htblLocations.Keys
                    If ch.HasSeenLocation(sLocKey) Then chs.lSeenKeys.Add(sLocKey)
                Next;
                For Each sObKey As String In Adventure.htblObjects.Keys
                    If ch.HasSeenObject(sObKey) Then chs.lSeenKeys.Add(sObKey)
                Next;
                For Each sChKey As String In Adventure.htblCharacters.Keys
                    If ch.HasSeenCharacter(sChKey) Then chs.lSeenKeys.Add(sChKey)
                Next;
                For Each sPropKey As String In ch.htblActualProperties.Keys ' ch.htblProperties.Keys
                    private clsProperty prop = ch.htblProperties(sPropKey);
                    private New clsGameState.clsCharacterState.clsStateProperty props;
                    props.Value = prop.Value(true);
                    chs.htblProperties.Add(sPropKey, props);
                Next;
                if (Not chs.htblProperties.ContainsKey("ProperName"))
                {
                    private New clsGameState.clsCharacterState.clsStateProperty props;
                    props.Value = ch.ProperName;
                    chs.htblProperties.Add("ProperName", props);
                }
                SaveDisplayOnce(ch.AllDescriptions, chs.htblDisplayedDescriptions);
                .htblCharacterStates.Add(ch.Key, chs);
            Next;

            For Each var As clsVariable In Adventure.htblVariables.Values
                private New clsGameState.clsVariableState vars;
                ReDim vars.Value(var.Length - 1);
                for (int i = 0; i <= var.Length - 1; i++)
                {
                    if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                    {
                        vars.Value(i) = var.IntValue(i + 1).ToString;
                    Else
                        vars.Value(i) = var.StringValue(i + 1);
                    }
                Next;
                SaveDisplayOnce(var.AllDescriptions, vars.htblDisplayedDescriptions);
                ' TODO: Arrays
                .htblVariableStates.Add(var.Key, vars);
            Next;

            'For Each grp As clsGroup In Adventure.htblGroups.Values
            '    If .htblGroupStates.ContainsKey(grp.Key) Then
            '        Dim grps As clsGameState.clsGroupState = CType(.htblGroupStates(grp.Key), clsGameState.clsGroupState)
            '        grp.arlMembers.Clear()
            '        For Each sMember As String In grps.lstMembers
            '            grp.arlMembers.Add(sMember)
            '        Next
            '    End If
            'Next
            For Each grp As clsGroup In Adventure.htblGroups.Values
                private New clsGameState.clsGroupState grps;
                For Each sMembers As String In grp.arlMembers
                    grps.lstMembers.Add(sMembers);
                Next;
                .htblGroupStates.Add(grp.Key, grps);
            Next;

        }

        return NewState;

    }


    ' Save the current game state onto our stack
    public void RecordState()
    {
        Push(GetState);
    }



    ' Load from file, and restore
    internal void LoadState(string sFilePath)
    {
        private clsGameState NewState = null;
        LoadFile(sFilePath, FileTypeEnum.GameState_TAS, LoadWhatEnum.All, false, , NewState);
        If NewState != null Then RestoreState(NewState)
    }


    private void RestoreDisplayOnce(List(Of Description AllDescriptions)
    {

        private int iDesc = 0;
        For Each d As Description In AllDescriptions
            iDesc += 1;
            private int iSing = 0;
            For Each sd As SingleDescription In d
                iSing += 1;
                if (sd.DisplayOnce)
                {
                    private string sKey = iDesc + "-" + iSing;
                    If htblStore.ContainsKey(sKey) Then sd.Displayed = true Else sd.Displayed = false
                }
            Next;
        Next;

    }


    internal void RestoreState(clsGameState state)
    {

        If state == null Then Exit Sub

        With state;

            UserSession.sTurnOutput = .sOutputText;

            For Each loc As clsLocation In Adventure.htblLocations.Values
                if (.htblLocationStates.ContainsKey(loc.Key))
                {
                    private clsGameState.clsLocationState locs = CType(.htblLocationStates(loc.Key), clsGameState.clsLocationState);
                    private New List<string> lDelete;
                    For Each prop As clsProperty In loc.htblProperties.Values
                        if (locs.htblProperties.ContainsKey(prop.Key))
                        {
                            private clsGameState.clsLocationState.clsStateProperty props = CType(locs.htblProperties(prop.Key), clsGameState.clsLocationState.clsStateProperty);
                            prop.Value = props.Value;
                        Else
                            lDelete.Add(prop.Key);
                        }
                    Next;
                    For Each sKey As String In lDelete
                        loc.RemoveProperty(sKey);
                    Next;
                    For Each sPropKey As String In locs.htblProperties.Keys
                        if (Not loc.htblActualProperties.ContainsKey(sPropKey))
                        {
                            if (Adventure.htblLocationProperties.ContainsKey(sPropKey))
                            {
                                private clsProperty propAdd = CType(Adventure.htblLocationProperties(sPropKey).Clone, clsProperty);
                                if (propAdd.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                {
                                    propAdd.Selected = true;
                                    loc.AddProperty(propAdd);
                                }
                            }
                        }
                    Next;
                    'UserSession.Map.Map.FindNode(loc.Key).Seen = locs.bSeen
                    'Adventure.Player.HasSeenLocation(loc.Key) = locs.bSeen
                    loc.ResetInherited();
                    RestoreDisplayOnce(loc.AllDescriptions, locs.htblDisplayedDescriptions);
                }
            Next;

            For Each ob As clsObject In Adventure.htblObjects.Values
                if (.htblObjectStates.ContainsKey(ob.Key))
                {
                    private clsGameState.clsObjectState obs = CType(.htblObjectStates(ob.Key), clsGameState.clsObjectState);
                    ob.Location = obs.Location.Copy;
                    private New List<string> lDelete;
                    For Each prop As clsProperty In ob.htblProperties.Values
                        if (obs.htblProperties.ContainsKey(prop.Key))
                        {
                            private clsGameState.clsObjectState.clsStateProperty props = CType(obs.htblProperties(prop.Key), clsGameState.clsObjectState.clsStateProperty);
                            prop.Value = props.Value;
                        Else
                            lDelete.Add(prop.Key);
                        }
                    Next;
                    For Each sKey As String In lDelete
                        ob.RemoveProperty(sKey);
                    Next;
                    For Each sPropKey As String In obs.htblProperties.Keys
                        if (Not ob.htblActualProperties.ContainsKey(sPropKey))
                        {
                            if (Adventure.htblObjectProperties.ContainsKey(sPropKey))
                            {
                                private clsProperty propAdd = CType(Adventure.htblObjectProperties(sPropKey).Clone, clsProperty);
                                if (propAdd.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                {
                                    propAdd.Selected = true;
                                    ob.AddProperty(propAdd);
                                }
                            }
                        }
                    Next;
                    ob.ResetInherited();
                    RestoreDisplayOnce(ob.AllDescriptions, obs.htblDisplayedDescriptions);
                }
            Next;

            For Each tas As clsTask In Adventure.htblTasks.Values
                if (.htblTaskStates.ContainsKey(tas.Key))
                {
                    private clsGameState.clsTaskState tass = CType(.htblTaskStates(tas.Key), clsGameState.clsTaskState);
                    tas.Completed = tass.Completed;
                    tas.Scored = tass.Scored;
                    RestoreDisplayOnce(tas.AllDescriptions, tass.htblDisplayedDescriptions);
                }
            Next;

            For Each ev As clsEvent In Adventure.htblEvents.Values
                if (.htblEventStates.ContainsKey(ev.Key))
                {
                    private clsGameState.clsEventState evs = CType(.htblEventStates(ev.Key), clsGameState.clsEventState);
                    ev.Status = evs.Status;
                    ev.TimerToEndOfEvent = evs.TimerToEndOfEvent;
                    ev.iLastSubEventTime = evs.iLastSubEventTime;
                    If ev.SubEvents != null && ev.SubEvents.Length > evs.iLastSubEventIndex Then ev.LastSubEvent = ev.SubEvents(evs.iLastSubEventIndex)
                    RestoreDisplayOnce(ev.AllDescriptions, evs.htblDisplayedDescriptions);
                }
            Next;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if (.htblCharacterStates.ContainsKey(ch.Key))
                {
                    private clsGameState.clsCharacterState chs = CType(.htblCharacterStates(ch.Key), clsGameState.clsCharacterState);
                    ch.Location = chs.Location;
                    if (ch.arlWalks.Count = chs.lWalks.Count)
                    {
                        for (int i = 0; i <= ch.arlWalks.Count - 1; i++)
                        {
                            private clsWalk w = ch.arlWalks(i);
                            private clsGameState.clsCharacterState.clsWalkState ws = chs.lWalks(i);
                            w.Status = ws.Status;
                            w.iTimerToEndOfWalk = ws.TimerToEndOfWalk;
                        Next;
                    }
                    For Each sLocKey As String In Adventure.htblLocations.Keys
                        ch.HasSeenLocation(sLocKey) = chs.lSeenKeys.Contains(sLocKey);
                    Next;
                    For Each sObKey As String In Adventure.htblObjects.Keys
                        ch.HasSeenObject(sObKey) = chs.lSeenKeys.Contains(sObKey);
                    Next;
                    For Each sChKey As String In Adventure.htblCharacters.Keys
                        ch.HasSeenCharacter(sChKey) = chs.lSeenKeys.Contains(sChKey);
                    Next;
                    private New List<string> lDelete;
                    For Each prop As clsProperty In ch.htblProperties.Values
                        if (chs.htblProperties.ContainsKey(prop.Key))
                        {
                            private clsGameState.clsCharacterState.clsStateProperty props = CType(chs.htblProperties(prop.Key), clsGameState.clsCharacterState.clsStateProperty);
                            prop.Value = props.Value;
                        Else
                            lDelete.Add(prop.Key);
                        }
                    Next;
                    For Each sKey As String In lDelete
                        ch.RemoveProperty(sKey);
                    Next;
                    For Each sPropKey As String In chs.htblProperties.Keys
                        if (Not ch.htblActualProperties.ContainsKey(sPropKey))
                        {
                            if (Adventure.htblCharacterProperties.ContainsKey(sPropKey))
                            {
                                private clsProperty propAdd = CType(Adventure.htblCharacterProperties(sPropKey).Clone, clsProperty);
                                if (propAdd.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                {
                                    propAdd.Selected = true;
                                    ch.AddProperty(propAdd);
                                }
                            Else
                                switch (sPropKey)
                                {
                                    case "ProperName":
                                        {
                                        ch.ProperName = chs.htblProperties(sPropKey).Value;
                                }
                            }
                        }
                    Next;
                    ch.ResetInherited();
                    RestoreDisplayOnce(ch.AllDescriptions, chs.htblDisplayedDescriptions);
                }
            Next;

            For Each var As clsVariable In Adventure.htblVariables.Values
                if (.htblVariableStates.ContainsKey(var.Key))
                {
                    private clsGameState.clsVariableState vars = CType(.htblVariableStates(var.Key), clsGameState.clsVariableState);
                    for (int i = 0; i <= var.Length - 1; i++)
                    {
                        If var.Type = clsVariable.VariableTypeEnum.Numeric Then var.IntValue(i + 1) = SafeInt(vars.Value(i)) Else var.StringValue(i + 1) = vars.Value(i)
                    Next;
                    RestoreDisplayOnce(var.AllDescriptions, vars.htblDisplayedDescriptions);
                }
            Next;

            For Each grp As clsGroup In Adventure.htblGroups.Values
                if (.htblGroupStates.ContainsKey(grp.Key))
                {
                    private clsGameState.clsGroupState grps = CType(.htblGroupStates(grp.Key), clsGameState.clsGroupState);
                    private New List<string> lMembersToReset;
                    For Each sMember As String In grp.arlMembers
                        lMembersToReset.Add(sMember);
                    Next;
                    grp.arlMembers.Clear();
                    For Each sMember As String In grps.lstMembers
                        grp.arlMembers.Add(sMember);
                        If lMembersToReset.Contains(sMember) Then lMembersToReset.Remove(sMember) Else lMembersToReset.Add(sMember)
                    Next;
                    For Each sMember As String In lMembersToReset
                        If Adventure.dictAllItems.ContainsKey(sMember) Then (clsItemWithProperties)(Adventure.dictAllItems(sMember)).ResetInherited()
                    Next;
                }
            Next;

        }

    }


    private clsGameState stateLast;
    private clsGameState stateCurrent;

    public bool SetLastState()
    {
        if (MyBase.Count > 1)
        {
            'If Adventure.eGameState = clsAction.EndGameEnum.Running Then
            MyBase.Pop() ' Discard current state;
            RestoreState((clsGameState)(MyBase.Peek));
            Adventure.eGameState = clsAction.EndGameEnum.Running;
            Debug.WriteLine("Popped (" + MyBase.Count + " on stack)");
            return true;
        Else
            return false;
            'RecordState() ' To make sure we have the latest on the stack
            'RestoreState(stateLast)
        }
    }

    public void SetCurrentState()
    {
        RestoreState(stateCurrent);
    }

    Public Shadows Sub Clear()
        MyBase.Clear();
        stateLast = Nothing
        stateCurrent = Nothing
    }

}


internal class clsGameState
{

    internal string sOutputText;

internal class clsObjectState
    {
internal class clsStateProperty
        {
            internal string Value;
        }

        public clsObjectLocation Location;
        internal New Dictionary<string, clsStateProperty> htblProperties;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
    }
    public New Dictionary<string, clsObjectState> htblObjectStates;

internal class clsCharacterState
    {
internal class clsWalkState
        {
            internal clsWalk.StatusEnum Status;
            internal int TimerToEndOfWalk;
        }
        <DebuggerDisplay("CharState={Value}")> _;
internal class clsStateProperty
        {
            internal string Value;
        }

        internal clsCharacterLocation Location;
        internal New List<clsWalkState> lWalks;
        internal New List<string> lSeenKeys;
        internal New Dictionary<string, clsStateProperty> htblProperties;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
    }
    public New Dictionary<string, clsCharacterState> htblCharacterStates;

internal class clsTaskState
    {
        internal bool Completed;
        internal bool Scored;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
    }
    internal New Dictionary<string, clsTaskState> htblTaskStates;

internal class clsEventState
    {
        internal clsEvent.StatusEnum Status;
        internal int TimerToEndOfEvent;
        internal int iLastSubEventTime;
        internal int iLastSubEventIndex;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
    }
    internal New Dictionary<string, clsEventState> htblEventStates;

internal class clsVariableState
    {
        Friend Value() As String;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
    }
    internal New Dictionary<string, clsVariableState> htblVariableStates;

internal class clsLocationState
    {
internal class clsStateProperty
        {
            internal string Value;
        }

        internal New Dictionary<string, clsStateProperty> htblProperties;
        internal New Dictionary<string, bool> htblDisplayedDescriptions;
        internal bool bSeen;
    }
    public New Dictionary<string, clsLocationState> htblLocationStates;

internal class clsGroupState
    {
        internal New List<string> lstMembers;
    }
    internal New Dictionary<string, clsGroupState> htblGroupStates;

}

}