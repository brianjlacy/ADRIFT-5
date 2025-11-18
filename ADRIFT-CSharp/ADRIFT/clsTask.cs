using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsTask
{
    Inherits clsItem;

    'Private sKey As String
    internal New StringArrayList arlCommands;
    private bool bRepeatable;
    private Boolean ' Multiple Matching _ContinueToExecuteLowerPriority;
    public bool bContinueToExecuteLowerPriority
        {
            get
            {
            return _ContinueToExecuteLowerPriority;
        }
set(Boolean)
            _ContinueToExecuteLowerPriority = value
        }
    }
    'Public Enum ContinueEnum
    '    ContinueNever = 0
    '    ContinueOnFail = 1
    '    ContinueOnNoOutput = 2
    '    ContinueAlways = 3
    'End Enum
    'Public eContinueToExecuteLowerPriority As ContinueEnum
    'Friend arlReverseCommands As New StringArrayList
    'Public Enum ExecuteWhereEnum
    '    NoRooms = 0
    '    SingleLocation = 1
    '    MultipleLocations = 2
    '    AllLocations = 3
    'End Enum
    'Public ExecuteWhere As ExecuteWhereEnum
    'Friend arlCanExecuteHere As New StringArrayList
    internal New RestrictionArrayList arlRestrictions;
    internal New ActionArrayList arlActions;
    private Description oCompletionMessage;
    private string sTriedAgainMessage;
    private string sDescription;
public enum BeforeAfterEnum
    {
        Before;
        After;
    }
    public BeforeAfterEnum eDisplayCompletion;
public enum TaskTypeEnum
    {
        General;
        Specific;
        System;
    }
    public TaskTypeEnum TaskType;
    private int iPriority;
    private int iAutoFillPriority;
    private bool bCompleted;
    private Description oFailOverride;
    public bool LowPriority;
    public bool Scored;
    'Public ExecuteParentActions As Boolean
    public bool ReplaceDuplicateKey;
    public bool PreventOverriding;
    public bool bSystemTask;

#if Runner
    private List<System.Text.RegularExpressions.Regex>[] _RegExs;
    Public ReadOnly Property RegExs As List(Of System.Text.RegularExpressions.Regex)()
        {
            get
            {
            if (_RegExs Is null)
            {
                ReDim _RegExs(arlCommands.Count - 1);
                'For Each sCommand As String In arlCommands
                for (int iCom = 0; iCom <= arlCommands.Count - 1; iCom++)
                {
                    private string sCommand = arlCommands(iCom);
                    _RegExs(iCom) = New List(Of System.Text.RegularExpressions.Regex);
                    For Each regex As System.Text.RegularExpressions.Regex In UserSession.GetRegularExpression(sCommand, "", False)
                        _RegExs(iCom).Add(regex);
                    Next;
                Next;
            }

            return _RegExs;
        }
    }
#endif

    ' Want
    ' Specific Text + Parent Text, specific actions, parent actions     - Before*   (text+actions)
    ' Specific Text, specific actions, parent actions                   - Before    (actions)

    ' Specific Text + Parent Text, specific actions                     - Before    (text)
    ' Specific Text, specific actions                                   - Override*
    ' Parent Text + Specific Text, specific actions                     - After     (text)

    ' Specific Text, parent actions, specific actions                   - After     (actions)
    ' Parent Text + Specific Text, parent actions, specific actions     - After     (text+actions)


    ' Old
    ' Parent with text, Specific with text, Execute parent action => Specific text, specific actions, parent actions
    ' Parent with text, Specific without text, Execute parent action => Parent text, specific actions, parent actions
    ' Parent with text, Specific with text, No parent action => Specific text, specific actions
    ' Parent with text, Specific without text, No parent action => Parent text, specific actions

public enum SpecificOverrideTypeEnum
    {
        BeforeTextAndActions;
        BeforeActionsOnly;
        BeforeTextOnly;
        Override;
        AfterTextOnly;
        AfterActionsOnly;
        AfterTextAndActions;
    }
    public SpecificOverrideTypeEnum SpecificOverrideType = SpecificOverrideTypeEnum.Override;

    private bool bRunImmediately = false;
    public bool RunImmediately { get; set; }
        {
            get
            {
            return TaskType = TaskTypeEnum.System && bRunImmediately;
        }
set(ByVal Boolean)
            bRunImmediately = value
        }
    }


    private string sLocationTrigger = "";
    public string LocationTrigger
        {
            get
            {
            return sLocationTrigger;
        }
set(String)
            sLocationTrigger = value
        }
    }


    public bool Completed { get; set; }
        {
            get
            {
            return bCompleted;
        }
set(ByVal Value As Boolean)
            bCompleted = Value
        }
    }

    'Public Enum SpecificTypeEnum
    '    [Object]
    '    Character
    '    Direction
    '    Number
    '    Text
    'End Enum
internal class Specific
    {
        public ReferencesType ' SpecificTypeEnum Type;
        public bool Multiple;
        public New StringArrayList Keys;

        public string List()
        {

            private string sList = "";

            if (Not Keys Is null && Keys.Count > 0)
            {
                for (int i = 0; i <= Keys.Count - 1; i++)
                {
                    switch (Type)
                    {
                        case ReferencesType.Direction:
                            {
                            If Keys(i) <> "" Then sList &= DirectionName((Keys(i)([Enum].Parse(GetType(DirectionsEnum))), DirectionsEnum))
                        default:
                            {
                            sList &= Adventure.GetNameFromKey(Keys(i), false, false);
                    }
                    If i + 2 < Keys.Count Then sList &= ", "
                    If i + 2 = Keys.Count Then sList &= " and "
                Next;
            }

            return sList;

        }

        public Specific Clone()
        {
            private New Specific spec;

            spec.Type = Type;
            spec.Multiple = Multiple;
            spec.Keys = Keys.Clone;

            return spec;
        }

    }
    Friend Specifics() As Specific;
    public string GeneralKey;


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


    'Private bIsLibrary As Boolean
    'Public Property IsLibrary() As Boolean
    '    Get
    '        Return bIsLibrary
    '    End Get
    '    Set(ByVal value As Boolean)
    '        bIsLibrary = value
    '    End Set
    'End Property

    'Private dtLastUpdated As Date
    'Friend Property LastUpdated() As Date
    '    Get
    '        If dtLastUpdated > Date.MinValue Then
    '            Return dtLastUpdated
    '        Else
    '            Return Now
    '        End If
    '    End Get
    '    Set(ByVal value As Date)
    '        dtLastUpdated = value
    '    End Set
    'End Property

    public string Description { get; set; }
        {
            get
            {
            if (sDescription <> "")
            {
                return sDescription;
            ElseIf arlCommands.Count > 0 Then
                return arlCommands(0);
            Else
                return Key;
            }
        }
set(ByVal Value As String)
            sDescription = Value
        }
    }


    private int iHasReferences = -1;
    public bool HasReferences { get; }
        {
            get
            {
            If iHasReferences > -1 Then Return CBool(IIf(iHasReferences = 0, false, true))
            For Each sRef As String In ReferenceNames()
                For Each sCommand As String In Me.arlCommands
                    if (sCommand.Contains(sRef))
                    {
                        iHasReferences = 1
                        return true;
                    }
                Next;
            Next;
            iHasReferences = 0
            return false;
        }
    }

    'Dim iReferenceCount As Integer = -1
    'Public ReadOnly Property ReferenceCount() As Integer
    '    Get
    '        If iReferenceCount > -1 Then Return iReferenceCount
    '        Dim iTempRefCount As Integer
    '        For Each sCommand As String In Me.arlCommands
    '            iTempRefCount = 0
    '            For Each sRef As String In ReferenceNames()
    '                If sCommand.Contains(sRef) Then
    '                    iTempRefCount += 1
    '                End If
    '            Next
    '            iReferenceCount = Math.Max(iReferenceCount, iTempRefCount)
    '        Next
    '        Return iReferenceCount
    '    End Get
    'End Property


    internal string MakeNice { get; }
        {
            get
            {
            ' Makes things like "[get/take] {the} %object% {from {the/a {green}} box}" into "get the %object% from the box"

            private int iLevel = 0;
            Dim bIgnore() As Boolean
            private string sText = Me.TaskCommand(Me) ' Me.arlCommands(0);

            ReDim bIgnore(0);
            bIgnore(0) = false;
            MakeNice = Nothing

            for (int i = 0; i <= sText.Length - 1; i++)
            {
                switch (sText.Substring(i, 1))
                {
                    case "[":
                    case "{":
                        {
                        iLevel += 1;
                        ReDim Preserve bIgnore(iLevel);
                        bIgnore(iLevel) = bIgnore(iLevel - 1);
                    case "]":
                    case "}":
                        {
                        iLevel -= 1;
                    case "/":
                        {
                        bIgnore(iLevel) = true;
                    default:
                        {
                        If ! bIgnore(iLevel) Then MakeNice &= sText.Substring(i, 1)
                }
            Next;

            MakeNice = MakeNice.Replace(" * ", " ").Replace("* ", " ").Replace(" *", " ")

        }
    }



    internal StringArrayList RefsInCommand(string sCommand)
    {

        private New StringArrayList arlRefs;

        For Each sSection As String In sCommand.Split("%"c)
            For Each sRef As String In ReferenceNames()
                if ("%" + sSection + "%" = sRef)
                {
                    arlRefs.Add(sRef);
                    Exit For;
                }
            Next;
        Next;
        'If sCommand IsNot Nothing Then
        '    For Each sWord As String In sCommand.Split(" "c)
        '        If sWord.StartsWith("%") AndAlso sWord.EndsWith("%") Then
        '            For Each sRef As String In ReferenceNames()
        '                If sWord = sRef Then arlRefs.Add(sRef)
        '                Exit For
        '            Next
        '        End If
        '    Next
        '    'For Each sRef As String In ReferenceNames()
        '    '    If sCommand.Contains(sRef) Then arlRefs.Add(sRef)
        '    'Next
        'End If

        return arlRefs;

    }


    internal StringArrayList References { get; }
        {
            get
            {
            'Dim arlRefs As New StringArrayList

            'Dim sCommand As String = TaskCommand(Me, False)
            'For Each sRef As String In ReferenceNames()
            ' If sCommand.Contains(sRef) Then arlRefs.Add(sRef)
            'Next
            'Return arlRefs
            return RefsInCommand(TaskCommand(Me, false));
        }
    }




    public string TaskCommand(clsTask task, bool bReplaceSpecifics = true)
    {

        private string sTaskCommand = "";
        private int iMatchedTaskCommand = 0;
#if runner
        iMatchedTaskCommand = UserSession.iMatchedTaskCommand
#endif

        switch (task.TaskType)
        {
            case TaskTypeEnum.General:
                {
                ' Find the command with the most refs.  E.g.
                ' # Get Object
                ' get %object%
                private int iRefsCount = 0;
                if (task.arlCommands.Count = 0)
                {
                    ErrMsg("Error, general task """ + sDescription + """ has no commands!");
                    return "";
                }
                if (task.arlCommands.Count > iMatchedTaskCommand)
                {
                    sTaskCommand = task.arlCommands(iMatchedTaskCommand)
                Else
                    sTaskCommand = task.arlCommands(0)
                }
                If RefsInCommand(sTaskCommand).Count > iRefsCount Then ' Need to make sure we're checking against the command we matched on
                    iRefsCount = RefsInCommand(sTaskCommand).Count
                }
                ' Some v4 games may not have same no. of refs on each line, so if any other lines have more, use that instead
                For Each sCommand As String In task.arlCommands
                    if (RefsInCommand(sCommand).Count > iRefsCount)
                    {
                        iRefsCount = RefsInCommand(sCommand).Count
                        sTaskCommand = sCommand
                    }
                Next;

            case TaskTypeEnum.Specific:
                {
                sTaskCommand = TaskCommand(Adventure.htblTasks(task.GeneralKey), bReplaceSpecifics)
                if (bReplaceSpecifics)
                {
                    ' Replace any Specifics from this key
                    for (int iSpec = 0; iSpec <= Specifics.Length - 1; iSpec++)
                    {
                        if (Specifics(iSpec).Keys.Count = 0 || (Specifics(iSpec).Keys.Count = 1 && Specifics(iSpec).Keys(0) = ""))
                        {
                            ' Allow, as it's passing thru the parent as a reference
                        Else
                            ' Replace the parent task %object% with our specific key
                            sTaskCommand = sTaskCommand.Replace(Me.References(iSpec), Specifics(iSpec).List)
                        }
                    Next;
                }
            case TaskTypeEnum.System:
                {
                sTaskCommand = ""
        }

        return sTaskCommand;

    }


    private StringArrayList ParentTaskCommand2(clsTask task)
    {

        while (task.TaskType = clsTask.TaskTypeEnum.Specific)
        {
            task = Adventure.htblTasks(task.GeneralKey)
        }
        for (int iSpec = 0; iSpec <= Specifics.Length; iSpec++)
        {
            switch (Specifics(iSpec).Keys.Count)
            {
                case 0:
                case 1:
                    {
                default:
                    {
                    ' Replace the parent task %object% with our specific key

            }
        Next;
        if (task.arlCommands.Count > 0)
        {
            return task.arlCommands;
        Else
            return new StringArrayList;
        }

    }


    internal Description CompletionMessage { get; set; }
        {
            get
            {
            return oCompletionMessage;
        }
set(ByVal Value As Description)
            oCompletionMessage = Value
        }
    }


    internal Description FailOverride { get; set; }
        {
            get
            {
            return oFailOverride;
        }
set(ByVal Description)
            oFailOverride = value
        }
    }

    public int Priority { get; set; }
        {
            get
            {
            return iPriority;
        }
set(ByVal Value As Integer)
            iPriority = Value
            ' TODO - rejig all the other priorities to insert this one
        }
    }

    public int AutoFillPriority { get; set; }
        {
            get
            {
            return iAutoFillPriority;
        }
set(ByVal Integer)
            iAutoFillPriority = value
        }
    }

    public string TriedAgainMessage { get; set; }
        {
            get
            {
            return sTriedAgainMessage;
        }
set(ByVal Value As String)
            sTriedAgainMessage = Value
        }
    }

    public bool Repeatable { get; set; }
        {
            get
            {
            return bRepeatable;
        }
set(ByVal Value As Boolean)
            bRepeatable = Value
        }
    }


    'Public Property ContinueToExecuteLowerPriority() As ContinueEnum
    '    Get
    '        Return eContinueToExecuteLowerPriority
    '    End Get
    '    Set(ByVal Value As ContinueEnum)
    '        eContinueToExecuteLowerPriority = Value
    '    End Set
    'End Property

    public void New()
    {

        oCompletionMessage = New Description
        oFailOverride = New Description

        if (iLoading = 0)
        {
            'If Adventure.Tasks(clsAdventure.TasksListEnum.GeneralTasks).Count > 0 Then
            '    Me.TaskType = TaskTypeEnum.Specific
            'Else
            Me.TaskType = TaskTypeEnum.General;
            'End If
        }

        bContinueToExecuteLowerPriority = False
        'Me.eContinueToExecuteLowerPriority = ContinueEnum.ContinueOnNoOutput
        Me.iAutoFillPriority = 10;

    }

private class TaskPrioritySortComparer
    {
        Implements System.Collections.Generic.IComparer(Of String);

        'Implements IComparer

        'Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        '    Return Adventure.htblTasks(x.ToString).Priority.CompareTo(Adventure.htblTasks(y.ToString).Priority)
        '    'Return CShort(CType(x, clsTask).Priority).CompareTo(CShort(CType(y, clsTask).Priority))
        'End Function

        public Integer Implements System.Collections.Generic.IComparer<string> Compare(string x, string y)
        {
            return Adventure.htblTasks(x).Priority.CompareTo(Adventure.htblTasks(y).Priority);
        }
    }

    Friend ReadOnly Property Children(Optional ByVal bIncludeCompleted As Boolean = False) As StringArrayList
        {
            get
            {
            private New StringArrayList sal;

            For Each tas As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.SpecificTasks).Values
                If tas.GeneralKey = Me.Key && (bIncludeCompleted || ! tas.Completed || tas.Repeatable) Then sal.Add(tas.Key)
            Next;
            sal.Sort(New TaskPrioritySortComparer);

            return sal;
        }
    }


    public string Parent { get; }
        {
            get
            {
            return Me.GeneralKey;
        }
    }


#if Runner
    public bool IsCompleteable()
    {
        return UserSession.PassRestrictions(Me.arlRestrictions, true, Me);
        'Return PassRestrictions(Me.arlRestrictions)
        return true ' Hmmm...;
    }


    Friend NewReferences() As clsNewReference;
    Friend NewReferencesWorking() As clsNewReference;


    internal clsNewReference) As clsNewReference[] CopyNewRefs(OriginalRefs()
    {

        If OriginalRefs == null Then Return null

        Dim NewRefCopy(OriginalRefs.Length - 1) As clsNewReference

        for (int iRef = 0; iRef <= OriginalRefs.Length - 1; iRef++)
        {
            if (OriginalRefs(iRef) IsNot null)
            {
                private New clsNewReference(OriginalRefs(iRef).ReferenceType) nr;
                'Dim NewItems(OriginalRefs(iRef).Items.Count - 1) As clsNewReference.clsSingleItem
                for (int iItem = 0; iItem <= OriginalRefs(iRef).Items.Count - 1; iItem++)
                {
                    private New clsSingleItem itm;
                    itm.MatchingPossibilities = OriginalRefs(iRef).Items(iItem).MatchingPossibilities.Clone;
                    itm.bExplicitlyMentioned = OriginalRefs(iRef).Items(iItem).bExplicitlyMentioned;
                    itm.sCommandReference = OriginalRefs(iRef).Items(iItem).sCommandReference;
                    'NewItems(iItem) = itm
                    nr.Items.Add(itm);
                Next;
                nr.Index = OriginalRefs(iRef).Index;
                'nr.Items = NewItems
                'nr.Multiple = OriginalRefs(iRef).Multiple
                'nr.ReferenceType = OriginalRefs(iRef).ReferenceType
                NewRefCopy(iRef) = nr;
            }
        Next;

        return NewRefCopy;

    }


    public bool HasObjectExistRestriction()
    {

        Static bChecked As Boolean = false;
        Static bResult As Boolean = false;

        if (Not bChecked)
        {
            For Each rest As clsRestriction In Me.arlRestrictions
                if (rest.eType = clsRestriction.RestrictionTypeEnum.Object && rest.eObject = clsRestriction.ObjectEnum.Exist)
                {
                    bResult = True
                    Exit For;
                }
            Next;
            bChecked = True
            return bResult;
        Else
            return bResult;
        }

    }


    public bool HasCharacterExistRestriction()
    {

        Static bChecked As Boolean = false;
        Static bResult As Boolean = false;

        if (Not bChecked)
        {
            For Each rest As clsRestriction In Me.arlRestrictions
                if (rest.eType = clsRestriction.RestrictionTypeEnum.Character && rest.eCharacter = clsRestriction.CharacterEnum.Exist)
                {
                    bResult = True
                    Exit For;
                }
            Next;
            bChecked = True
            return bResult;
        Else
            return bResult;
        }

    }


    public bool HasLocationExistRestriction()
    {

        Static bChecked As Boolean = false;
        Static bResult As Boolean = false;

        if (Not bChecked)
        {
            For Each rest As clsRestriction In Me.arlRestrictions
                if (rest.eType = clsRestriction.RestrictionTypeEnum.Location && rest.eLocation = clsRestriction.LocationEnum.Exist)
                {
                    bResult = True
                    Exit For;
                }
            Next;
            bChecked = True
            return bResult;
        Else
            return bResult;
        }

    }

#endif

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            private clsTask tas = CType(Me.MemberwiseClone, clsTask);
            tas.arlCommands = tas.arlCommands.Clone;
            tas.arlRestrictions = tas.arlRestrictions.Copy;
            tas.arlActions = tas.arlActions.Copy;
            if (Specifics IsNot null)
            {
                ReDim tas.Specifics(Specifics.Length - 1);
                for (int i = 0; i <= tas.Specifics.Length - 1; i++)
                {
                    tas.Specifics(i) = Specifics(i).Clone;
                Next;
            }
            return tas;
        }
    }

    'Public Function Copy() As clsTask

    '    Dim tas As New clsTask

    '    With tas
    '        .arlActions = Me.arlActions.Copy
    '        .arlCommands = Me.arlCommands.Clone
    '        .arlRestrictions = Me.arlRestrictions.Copy
    '        .bCompleted = Me.bCompleted
    '        .eContinueToExecuteLowerPriority = Me.eContinueToExecuteLowerPriority
    '        .bRepeatable = Me.bRepeatable
    '        .iAutoFillPriority = Me.iAutoFillPriority
    '        .iPriority = Me.iPriority
    '        .LowPriority = Me.LowPriority
    '        .oCompletionMessage = Me.oCompletionMessage
    '        .sDescription = Me.sDescription
    '        .sTriedAgainMessage = Me.sTriedAgainMessage
    '        .TaskType = Me.TaskType
    '    End With

    '    Return tas

    'End Function

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Description;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            For Each r As clsRestriction In Me.arlRestrictions
                all.Add(r.oMessage);
            Next;
            all.Add(Me.CompletionMessage);
            all.Add(Me.FailOverride);
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
        private New frmTask(Me, True) fTask;
#endif
    }


    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        iCount += arlRestrictions.ReferencesKey(sKey);
        iCount += arlActions.ReferencesKey(sKey);
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        If GeneralKey = sKey Then iCount += 1
        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        If ! arlRestrictions.DeleteKey(sKey) Then Return false
        If ! arlActions.DeleteKey(sKey) Then Return false
        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        If GeneralKey = sKey Then GeneralKey = ""

        return true;

    }

}


public class clsRestriction
{

public enum RestrictionTypeEnum
    {
        Location;
        [Object];
        Task;
        Character;
        Variable;
        [Property];
        Direction;
        Expression;
    }
    internal RestrictionTypeEnum eType;

public enum MustEnum
    {
        Must;
        MustNot;
    }
    public MustEnum eMust;

    Public sKey1, sKey2 As String

    internal Description oMessage;

public enum LocationEnum
    {
        HaveBeenSeenByCharacter;
        BeInGroup;
        HaveProperty;
        BeLocation;
        Exist;
    }
    public LocationEnum eLocation;

public enum ObjectEnum
    {
        BeAtLocation;
        BeHeldByCharacter;
        BeWornByCharacter;
        BeVisibleToCharacter;
        BeInsideObject;
        BeOnObject;
        BeInState;
        BeInGroup;
        HaveBeenSeenByCharacter;
        BePartOfObject;
        BePartOfCharacter;
        Exist;
        HaveProperty;
        BeExactText;
        BeHidden;
        BeObject;
    }
    public ObjectEnum eObject;

public enum TaskEnum
    {
        Complete;
    }
    public TaskEnum eTask;

public enum CharacterEnum
    {
        BeAlone;
        BeAloneWith;
        BeAtLocation;
        BeCharacter;
        BeHoldingObject;
        BeInConversationWith;
        BeInPosition;
        BeInSameLocationAsCharacter;
        BeInSameLocationAsObject;
        BeInsideObject;
        BeLyingOnObject;
        BeMemberOfGroup;
        BeOfGender;
        BeOnObject;
        BeOnCharacter;
        BeStandingOnObject;
        BeSittingOnObject;
        BeWearingObject;
        BeWithinLocationGroup;
        Exist;
        HaveProperty;
        HaveRouteInDirection;
        HaveSeenLocation;
        HaveSeenObject;
        HaveSeenCharacter;
    }
    public CharacterEnum eCharacter;

public enum VariableEnum
    {
        LessThan;
        LessThanOrEqualTo;
        EqualTo;
        GreaterThanOrEqualTo;
        GreaterThan;
        'NotEqualTo
    }
    public VariableEnum eVariable;

    public override string ToString()
    {
        return Summary;
    }

    public int IntValue;
    public string StringValue;


    public bool ReferencesKey(string sKey)
    {
        return sKey1 = sKey || sKey2 = sKey;
    }


    public string Summary { get; }
        {
            get
            {
            private string sSummary = "Undefined Restriction";

            try
            {
                if (sKey1 IsNot null || eType = RestrictionTypeEnum.Expression)
                {
                    switch (eType)
                    {
                        case RestrictionTypeEnum.Location:
                            {
                            sSummary = Adventure.GetNameFromKey(sKey1, , , True)
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= " must ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= " must not ";
                            }
                            switch (eLocation)
                            {
                                case LocationEnum.HaveBeenSeenByCharacter:
                                    {
                                    sSummary &= "have been seen by " + Adventure.GetNameFromKey(sKey2);
                                case LocationEnum.BeInGroup:
                                    {
                                    sSummary &= "be in " + Adventure.GetNameFromKey(sKey2);
                                case LocationEnum.HaveProperty:
                                    {
                                    sSummary &= "have " + Adventure.GetNameFromKey(sKey2);
                                case LocationEnum.BeLocation:
                                    {
                                    sSummary &= "be location " + Adventure.GetNameFromKey(sKey2);
                                case LocationEnum.Exist:
                                    {
                                    sSummary &= "exist";
                            }

                        case RestrictionTypeEnum.Object:
                            {
                            sSummary = Adventure.GetNameFromKey(sKey1, , , True)
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= " must ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= " must not ";
                            }
                            switch (eObject)
                            {
                                case ObjectEnum.BeAtLocation:
                                    {
                                    sSummary &= "be at " + Adventure.GetNameFromKey(sKey2);
                                    sSummary = sSummary.Replace("at " & HIDDEN, HIDDEN)
                                case ObjectEnum.BeHeldByCharacter:
                                    {
                                    sSummary &= "be held by " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.BeHidden:
                                    {
                                    sSummary &= "be hidden";
                                case ObjectEnum.BeInGroup:
                                    {
                                    sSummary &= "be in object " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.BeInsideObject:
                                    {
                                    sSummary &= ("be inside " + Adventure.GetNameFromKey(sKey2));
                                case ObjectEnum.BeInState:
                                    {
                                    sSummary &= "be in state '" + sKey2 + "'" 'StringValue;
                                case ObjectEnum.BeOnObject:
                                    {
                                    sSummary &= ("be on " + Adventure.GetNameFromKey(sKey2));
                                case ObjectEnum.BePartOfCharacter:
                                    {
                                    sSummary &= "be part of " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.BePartOfObject:
                                    {
                                    sSummary &= ("be part of " + Adventure.GetNameFromKey(sKey2));
                                case ObjectEnum.HaveBeenSeenByCharacter:
                                    {
                                    sSummary &= "have been seen by " + Adventure.GetNameFromKey(sKey2);
                                    'Case ObjectEnum.StaticOrDynamic
                                    '    sSummary &= sKey2
                                case ObjectEnum.BeVisibleToCharacter:
                                    {
                                    sSummary &= "be visible to " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.BeWornByCharacter:
                                    {
                                    sSummary &= "be worn by " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.Exist:
                                    {
                                    sSummary &= "exist";
                                case ObjectEnum.HaveProperty:
                                    {
                                    sSummary &= "have " + Adventure.GetNameFromKey(sKey2);
                                case ObjectEnum.BeExactText:
                                    {
                                    sSummary &= "be exact text '" + sKey2 + "'";
                                case ObjectEnum.BeObject:
                                    {
                                    sSummary &= "be " + Adventure.GetNameFromKey(sKey2);
                            }

                        case RestrictionTypeEnum.Task:
                            {
                            sSummary = Adventure.GetNameFromKey(sKey1, , , True)
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= " must be complete";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= " must not be complete";
                            }

                        case RestrictionTypeEnum.Character:
                            {
                            sSummary = Adventure.GetNameFromKey(sKey1, , , True)
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= " must ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= " must not ";
                            }
                            switch (eCharacter)
                            {
                                case CharacterEnum.BeAlone:
                                    {
                                    sSummary &= "be alone";
                                case CharacterEnum.BeAloneWith:
                                    {
                                    sSummary &= "be alone with " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeAtLocation:
                                    {
                                    sSummary &= "be at " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeCharacter:
                                    {
                                    sSummary &= "be " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeInConversationWith:
                                    {
                                    sSummary &= "be in conversation with " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.Exist:
                                    {
                                    sSummary &= "exist";
                                case CharacterEnum.HaveRouteInDirection:
                                    {
                                    sSummary &= "have a route available ";
                                    if (sKey2 = ANYDIRECTION)
                                    {
                                        sSummary &= "in any direction";
                                    ElseIf sKey2.StartsWith("ReferencedDirection") Then
                                        sSummary &= "to the " + sKey2.Replace("ReferencedDirection", "Referenced Direction");
                                    Else
                                        switch (EnumParseDirections(sKey2))
                                        {
                                            case DirectionsEnum.North:
                                            case DirectionsEnum.NorthEast:
                                            case DirectionsEnum.East:
                                            case DirectionsEnum.SouthEast:
                                            case DirectionsEnum.South:
                                            case DirectionsEnum.SouthWest:
                                            case DirectionsEnum.West:
                                            case DirectionsEnum.NorthWest:
                                                {
                                                sSummary &= "to the " + DirectionName(EnumParseDirections(sKey2));
                                            case DirectionsEnum.Up:
                                            case DirectionsEnum.Down:
                                            case DirectionsEnum.In:
                                            case DirectionsEnum.Out:
                                                {
                                                sSummary &= DirectionName(EnumParseDirections(sKey2));
                                        }

                                    }
                                case CharacterEnum.HaveProperty:
                                    {
                                    sSummary &= "have " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.HaveSeenCharacter:
                                    {
                                    sSummary &= "have seen " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.HaveSeenLocation:
                                    {
                                    sSummary &= "have seen " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.HaveSeenObject:
                                    {
                                    sSummary &= "have seen " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeHoldingObject:
                                    {
                                    sSummary &= "be holding " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeInSameLocationAsCharacter:
                                    {
                                    sSummary &= "be in the same location as " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeInSameLocationAsObject:
                                    {
                                    if (Adventure.GetTypeFromKeyNice(sKey2) = "Object")
                                    {
                                        sSummary &= "be in the same location as " + Adventure.GetNameFromKey(sKey2);
                                    Else
                                        sSummary &= "be in the same location as any object in " + Adventure.GetNameFromKey(sKey2);
                                    }
                                case CharacterEnum.BeLyingOnObject:
                                    {
                                    sSummary &= "be lying on " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeMemberOfGroup:
                                    {
                                    sSummary &= "be a member of " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeOfGender:
                                    {
                                    sSummary &= "be of gender " + sKey2;
                                case CharacterEnum.BeSittingOnObject:
                                    {
                                    sSummary &= "be sitting on " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeStandingOnObject:
                                    {
                                    sSummary &= "be standing on " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeWearingObject:
                                    {
                                    sSummary &= "be wearing " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeWithinLocationGroup:
                                    {
                                    sSummary &= "be at a location within " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeInPosition:
                                    {
                                    sSummary &= "be in position " + sKey2;
                                case CharacterEnum.BeInsideObject:
                                    {
                                    sSummary &= "be inside " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeOnObject:
                                    {
                                    sSummary &= "be on " + Adventure.GetNameFromKey(sKey2);
                                case CharacterEnum.BeOnCharacter:
                                    {
                                    sSummary &= "be on " + Adventure.GetNameFromKey(sKey2);
                            }

                        case RestrictionTypeEnum.Variable:
                            {
                            private clsVariable.VariableTypeEnum eVarType;

                            switch (sKey1)
                            {
                                case "ReferencedNumber":
                                    {
                                    sSummary = Adventure.GetNameFromKey(sKey1) & " "
                                    eVarType = clsVariable.VariableTypeEnum.Numeric
                                case "ReferencedText":
                                    {
                                    sSummary = Adventure.GetNameFromKey(sKey1) & " "
                                    eVarType = clsVariable.VariableTypeEnum.Text
                                default:
                                    {
                                    private clsVariable var = Adventure.htblVariables(sKey1);
                                    eVarType = var.Type
                                    sSummary = "Variable '" & var.Name
                                    if (sKey2 <> "")
                                    {
                                        if (Adventure.htblVariables.ContainsKey(sKey2))
                                        {
                                            sSummary &= "[%" + Adventure.htblVariables(sKey2).Name + "%]";
                                        Else
                                            sSummary &= "[" + sKey2 + "]";
                                        }
                                    }
                                    sSummary &= "' ";
                            }

                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= "must be ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= "must not be ";
                            }

                            switch (eVariable)
                            {
                                case VariableEnum.EqualTo:
                                    {
                                    sSummary &= "equal to ";
                                case VariableEnum.GreaterThan:
                                    {
                                    sSummary &= "greater than ";
                                case VariableEnum.GreaterThanOrEqualTo:
                                    {
                                    sSummary &= "greater than or equal to ";
                                case VariableEnum.LessThan:
                                    {
                                    sSummary &= "less than ";
                                case VariableEnum.LessThanOrEqualTo:
                                    {
                                    sSummary &= "less than or equal to ";
                                    'Case VariableEnum.NotEqualTo
                                    '    sSummary &= "not equal to "
                            }
                            if (IntValue = Integer.MinValue)
                            {
                                sSummary &= Adventure.GetNameFromKey(StringValue);
                            Else
                                if (eVarType = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    if (StringValue <> "" && StringValue <> IntValue.ToString)
                                    {
                                        sSummary &= StringValue ' Must be an expression resulting in an integer;
                                    Else
                                        sSummary &= IntValue;
                                    }
                                Else
                                    sSummary &= "'" + StringValue + "'";
                                }
                            }

                        case RestrictionTypeEnum.Property:
                            {
                            sSummary = Adventure.GetNameFromKey(sKey1, , , True) & " for " & Adventure.GetNameFromKey(sKey2)
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= " must be ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= " must not be ";
                            }
                            switch (Adventure.htblAllProperties(sKey1).Type)
                            {
                                case clsProperty.PropertyTypeEnum.CharacterKey:
                                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                case clsProperty.PropertyTypeEnum.LocationKey:
                                case clsProperty.PropertyTypeEnum.ObjectKey:
                                    {
                                    sSummary &= "'" + Adventure.GetNameFromKey(StringValue) + "'";
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                case clsProperty.PropertyTypeEnum.Integer:
                                    {
                                    switch (CType(IntValue, VariableEnum))
                                    {
                                        case VariableEnum.LessThan:
                                            {
                                            sSummary &= "< ";
                                        case VariableEnum.LessThanOrEqualTo:
                                            {
                                            sSummary &= "<= ";
                                        case VariableEnum.EqualTo:
                                            {
                                            sSummary &= "= ";
                                        case VariableEnum.GreaterThanOrEqualTo:
                                            {
                                            sSummary &= ">= ";
                                        case VariableEnum.GreaterThan:
                                            {
                                            sSummary &= "> ";
                                    }
                                    sSummary &= "'" + StringValue + "'";
                                case clsProperty.PropertyTypeEnum.StateList:
                                case clsProperty.PropertyTypeEnum.Text:
                                    {
                                    sSummary &= "'" + StringValue + "'";
                            }

                        case RestrictionTypeEnum.Direction:
                            {
                            sSummary = "Referenced Direction "
                            switch (eMust)
                            {
                                case MustEnum.Must:
                                    {
                                    sSummary &= "must be ";
                                case MustEnum.MustNot:
                                    {
                                    sSummary &= "must not be ";
                            }
                            sSummary &= DirectionName(EnumParseDirections(sKey1));

                        case RestrictionTypeEnum.Expression:
                            {
                            sSummary = StringValue

                    }
                }

            Catch
                ErrMsg("Error producing restriction summary");
            }

            return sSummary;

        }
    }


    public clsRestriction Copy()
    {

        private New clsRestriction rest;

        rest.eCharacter = Me.eCharacter;
        rest.eType = Me.eType;
        rest.eLocation = Me.eLocation;
        rest.eMust = Me.eMust;
        rest.eObject = Me.eObject;
        rest.eTask = Me.eTask;
        rest.eVariable = Me.eVariable;
        rest.sKey1 = Me.sKey1;
        rest.sKey2 = Me.sKey2;
        rest.IntValue = Me.IntValue;
        rest.StringValue = Me.StringValue;
        rest.oMessage = Me.oMessage.Copy;

        return rest;

    }

    public void New()
    {
        oMessage = New Description
    }

}



public class clsAction
{

public enum ItemEnum
    {
        ' Objects
        MoveObject;
        AddObjectToGroup;
        RemoveObjectFromGroup;

        ' Characters
        MoveCharacter;
        AddCharacterToGroup;
        RemoveCharacterFromGroup;

        SetProperties;
        Variables;
        SetTasks;
        EndGame;
        Conversation;

        ' Locations
        AddLocationToGroup;
        RemoveLocationFromGroup;

    }
    public ItemEnum eItem;

    Public sKey1, sKey2 As String
    public string sPropertyValue;

public enum MoveObjectWhatEnum
    {
        [Object];
        EverythingHeldBy;
        EverythingWornBy;
        EverythingInside;
        EverythingOn;
        EverythingWithProperty;
        EverythingInGroup;
        EverythingAtLocation;
    }
    public MoveObjectWhatEnum eMoveObjectWhat;

public enum MoveObjectToEnum
    {
        ' Static or Dynamic Moves
        ToLocation;
        ToSameLocationAs ' Object or Character;
        ToLocationGroup ' If static, moves to all locations.  If dynamic, moves to random location;
        '
        ' Dynamic Moves
        '
        InsideObject;
        OntoObject;
        ToCarriedBy;
        ToWornBy;
        '
        ' Static Moves
        ToPartOfCharacter;
        ToPartOfObject;
        '
        ' To/From Group
        ToGroup;
        FromGroup;
    }
    public MoveObjectToEnum eMoveObjectTo;

public enum MoveCharacterWhoEnum
    {
        Character;
        EveryoneInside;
        EveryoneOn;
        EveryoneWithProperty;
        EveryoneInGroup;
        EveryoneAtLocation;
    }
    public MoveCharacterWhoEnum eMoveCharacterWho;

public enum MoveCharacterToEnum
    {
        InDirection;
        ToLocation;
        ToLocationGroup;
        ToSameLocationAs ' Object or Character;
        ToStandingOn;
        ToSittingOn;
        ToLyingOn;
        ToSwitchWith;
        InsideObject;
        OntoCharacter;
        ToParentLocation;
        ToGroup;
        FromGroup;
    }
    public MoveCharacterToEnum eMoveCharacterTo;

public enum MoveLocationWhatEnum
    {
        Location;
        LocationOf;
        EverywhereInGroup;
        EverywhereWithProperty;
    }
    public MoveLocationWhatEnum eMoveLocationWhat;

public enum MoveLocationToEnum
    {
        ' To/From Group
        ToGroup;
        FromGroup;
    }
    public MoveLocationToEnum eMoveLocationTo;

public enum ObjectStatusEnum
    {
        DunnoYet;
    }
    public ObjectStatusEnum eObjectStatus;

public enum VariablesEnum
    {
        Assignment;
        [Loop];
    }
    public VariablesEnum eVariables;

public enum SetTasksEnum
    {
        Execute;
        Unset;
    }
    public SetTasksEnum eSetTasks;

public enum EndGameEnum
    {
        Win;
        Lose;
        Neutral;
        Running;
    }
    public EndGameEnum eEndgame;

internal enum ConversationEnum
    {
        Greet = 1
        Ask = 2
        Tell = 4
        Command = 8
        Farewell = 16
        EnterConversation = 32
        LeaveConversation = 64
    }
    internal ConversationEnum eConversation;

    public int IntValue;
    public string StringValue;

    public clsAction Copy()
    {

        private New clsAction act;

        act.eendgame = Me.eendgame;
        act.eItem = Me.eItem;
        act.eMoveObjectWhat = Me.eMoveObjectWhat;
        act.eMoveObjectTo = Me.eMoveObjectTo;
        act.eMoveCharacterWho = Me.eMoveCharacterWho;
        act.eMoveCharacterTo = Me.eMoveCharacterTo;
        act.eMoveLocationWhat = Me.eMoveLocationWhat;
        act.eMoveLocationTo = Me.eMoveLocationTo;
        act.sPropertyValue = Me.sPropertyValue;
        act.eObjectStatus = Me.eObjectStatus;
        act.eSetTasks = Me.eSetTasks;
        act.eVariables = Me.eVariables;
        act.eConversation = Me.eConversation;
        act.IntValue = Me.IntValue;
        act.sKey1 = Me.sKey1;
        act.sKey2 = Me.sKey2;
        act.StringValue = Me.StringValue;

        return act;

    }


    public override string ToString()
    {
        return Summary;
    }


    public bool ReferencesKey(string sKey)
    {
        return sKey1 = sKey || sKey2 = sKey;
    }



    public string Summary { get; }
        {
            get
            {
            private string sSummary = "Undefined Action";

            try
            {
                switch (eItem)
                {
                    case ItemEnum.MoveObject:
                    case ItemEnum.AddObjectToGroup:
                    case ItemEnum.RemoveObjectFromGroup:
                        {

                        switch (eItem)
                        {
                            case ItemEnum.MoveObject:
                                {
                                sSummary = "Move "
                            case ItemEnum.AddObjectToGroup:
                                {
                                sSummary = "Add "
                            case ItemEnum.RemoveObjectFromGroup:
                                {
                                sSummary = "Remove "
                            default:
                                {
                                ErrMsg("eItem not specified");
                        }

                        private bool bDynamic = true;
                        switch (eMoveObjectWhat)
                        {
                            case MoveObjectWhatEnum.Object:
                                {
                                If ! sKey1.StartsWith("ReferencedObject") Then bDynamic = ! Adventure.htblObjects(sKey1).IsStatic

                            case MoveObjectWhatEnum.EverythingHeldBy:
                                {
                                sSummary &= "everything held by ";

                            case MoveObjectWhatEnum.EverythingAtLocation:
                                {
                                sSummary &= "everything at ";

                            case MoveObjectWhatEnum.EverythingInGroup:
                                {
                                sSummary &= "everything in ";

                            case MoveObjectWhatEnum.EverythingInside:
                                {
                                sSummary &= "everything inside ";

                            case MoveObjectWhatEnum.EverythingOn:
                                {
                                sSummary &= "everything on ";

                            case MoveObjectWhatEnum.EverythingWithProperty:
                                {
                                sSummary &= "everything with " + Adventure.GetNameFromKey(sKey1);
                                if (sPropertyValue <> "")
                                {
                                    private clsProperty prop = Adventure.htblObjectProperties(sKey1);
                                    if (prop IsNot null)
                                    {
                                        sSummary &= " where value is ";
                                        switch (prop.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                            case clsProperty.PropertyTypeEnum.LocationKey:
                                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                                {
                                                sSummary &= Adventure.GetNameFromKey(sPropertyValue);
                                            case clsProperty.PropertyTypeEnum.Integer:
                                            case clsProperty.PropertyTypeEnum.StateList:
                                            case clsProperty.PropertyTypeEnum.Text:
                                                {
                                                sSummary &= "'" + sPropertyValue + "'";
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                ' N/A
                                        }
                                    }
                                }

                            case MoveObjectWhatEnum.EverythingWornBy:
                                {
                                sSummary &= "everything worn by ";

                            default:
                                {
                                ErrMsg("eMoveObjectWhat not specified");
                        }
                        If eMoveObjectWhat <> MoveObjectWhatEnum.EverythingWithProperty Then sSummary &= Adventure.GetNameFromKey(sKey1)

                        switch (eMoveObjectTo)
                        {
                            case MoveObjectToEnum.InsideObject:
                                {
                                sSummary &= " inside " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.OntoObject:
                                {
                                sSummary &= " onto " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToCarriedBy:
                                {
                                sSummary &= " to held by " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToLocation:
                                {
                                if (sKey2 = "Hidden")
                                {
                                    sSummary &= " to Hidden";
                                Else
                                    sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                                }
                            case MoveObjectToEnum.ToLocationGroup:
                                {
                                if (bDynamic)
                                {
                                    sSummary &= " to somewhere in location " + Adventure.GetNameFromKey(sKey2);
                                Else
                                    sSummary &= " to everywhere in location " + Adventure.GetNameFromKey(sKey2);
                                }
                            case MoveObjectToEnum.ToPartOfCharacter:
                                {
                                sSummary &= " to part of " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToPartOfObject:
                                {
                                sSummary &= " to part of " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToSameLocationAs:
                                {
                                sSummary &= " to same location as " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToWornBy:
                                {
                                sSummary &= " to worn by " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.ToGroup:
                                {
                                sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                            case MoveObjectToEnum.FromGroup:
                                {
                                sSummary &= " from " + Adventure.GetNameFromKey(sKey2);
                        }

                    case ItemEnum.MoveCharacter:
                    case ItemEnum.AddCharacterToGroup:
                    case ItemEnum.RemoveCharacterFromGroup:
                        {

                        switch (eItem)
                        {
                            case ItemEnum.MoveCharacter:
                                {
                                sSummary = "Move "
                            case ItemEnum.AddCharacterToGroup:
                                {
                                sSummary = "Add "
                            case ItemEnum.RemoveCharacterFromGroup:
                                {
                                sSummary = "Remove "
                            default:
                                {
                                ErrMsg("eItem not specified");
                        }

                        switch (eMoveCharacterWho)
                        {
                            case MoveCharacterWhoEnum.Character:
                                {
                                '
                            case MoveCharacterWhoEnum.EveryoneAtLocation:
                                {
                                sSummary &= "everyone at ";
                            case MoveCharacterWhoEnum.EveryoneInGroup:
                                {
                                sSummary &= "everyone in ";
                            case MoveCharacterWhoEnum.EveryoneInside:
                                {
                                sSummary &= "everyone inside ";
                            case MoveCharacterWhoEnum.EveryoneOn:
                                {
                                sSummary &= "everyone on ";
                            case MoveCharacterWhoEnum.EveryoneWithProperty:
                                {
                                sSummary &= "everyone with " + Adventure.GetNameFromKey(sKey1);
                                if (sPropertyValue <> "")
                                {
                                    private clsProperty prop = Adventure.htblCharacterProperties(sKey1);
                                    if (prop IsNot null)
                                    {
                                        sSummary &= " where value is ";
                                        switch (prop.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                            case clsProperty.PropertyTypeEnum.LocationKey:
                                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                                {
                                                sSummary &= Adventure.GetNameFromKey(sPropertyValue);
                                            case clsProperty.PropertyTypeEnum.Integer:
                                            case clsProperty.PropertyTypeEnum.StateList:
                                            case clsProperty.PropertyTypeEnum.Text:
                                                {
                                                sSummary &= "'" + sPropertyValue + "'";
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                ' N/A
                                        }
                                    }
                                }
                            default:
                                {
                                ErrMsg("eMoveCharacterWho not specified");
                        }
                        If eMoveCharacterWho <> MoveCharacterWhoEnum.EveryoneWithProperty Then sSummary &= Adventure.GetNameFromKey(sKey1)

                        'sSummary = "Move " & Adventure.GetNameFromKey(sKey1).TrimStart(" "c)
                        switch (eMoveCharacterTo)
                        {
                            case MoveCharacterToEnum.InDirection:
                                {
                                sSummary &= " in direction";
                                if (sKey2.StartsWith("ReferencedDirection"))
                                {
                                    sSummary &= sKey2.Replace("ReferencedDirection", " Referenced Direction ");
                                Else
                                    sSummary &= " " + DirectionName(EnumParseDirections(sKey2)) ' (DirectionsEnum)(IntValue));
                                }
                            case MoveCharacterToEnum.ToLocation:
                                {
                                sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToLocationGroup:
                                {
                                sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToSameLocationAs:
                                {
                                sSummary &= " to same location as " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToStandingOn:
                                {
                                sSummary &= " to standing on " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToSittingOn:
                                {
                                sSummary &= " to sitting on " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToSwitchWith:
                                {
                                sSummary &= " to switch places with " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToLyingOn:
                                {
                                sSummary &= " to lying on " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.InsideObject:
                                {
                                sSummary &= " inside " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.OntoCharacter:
                                {
                                sSummary &= " onto " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.ToParentLocation:
                                {
                                sSummary &= " to parent location";
                            case MoveCharacterToEnum.ToGroup:
                                {
                                sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                            case MoveCharacterToEnum.FromGroup:
                                {
                                sSummary &= " from " + Adventure.GetNameFromKey(sKey2);
                        }
                        'sSummary = sSummary.Replace("on object 'the Floor'", "on the Floor")

                    case ItemEnum.AddLocationToGroup:
                    case ItemEnum.RemoveLocationFromGroup:
                        {

                        switch (eItem)
                        {
                            case ItemEnum.AddLocationToGroup:
                                {
                                sSummary = "Add "
                            case ItemEnum.RemoveLocationFromGroup:
                                {
                                sSummary = "Remove "
                            default:
                                {
                                ErrMsg("eItem not specified");
                        }

                        switch (eMoveLocationWhat)
                        {
                            case MoveLocationWhatEnum.Location:
                                {
                                '
                            case MoveLocationWhatEnum.LocationOf:
                                {
                                sSummary &= "location of ";
                            case MoveLocationWhatEnum.EverywhereInGroup:
                                {
                                sSummary &= "everywhere in ";
                            case MoveLocationWhatEnum.EverywhereWithProperty:
                                {
                                sSummary &= "everywhere with " + Adventure.GetNameFromKey(sKey1);
                                if (sPropertyValue <> "")
                                {
                                    private clsProperty prop = Adventure.htblLocationProperties(sKey1);
                                    if (prop IsNot null)
                                    {
                                        sSummary &= " where value is ";
                                        switch (prop.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                            case clsProperty.PropertyTypeEnum.LocationKey:
                                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                                {
                                                sSummary &= Adventure.GetNameFromKey(sPropertyValue);
                                            case clsProperty.PropertyTypeEnum.Integer:
                                            case clsProperty.PropertyTypeEnum.StateList:
                                            case clsProperty.PropertyTypeEnum.Text:
                                                {
                                                sSummary &= "'" + sPropertyValue + "'";
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                ' N/A
                                        }
                                    }
                                }
                            default:
                                {
                                ErrMsg("eMoveLocationWhat not specified");
                        }
                        If eMoveLocationWhat <> MoveLocationWhatEnum.EverywhereWithProperty Then sSummary &= Adventure.GetNameFromKey(sKey1)

                        'sSummary = "Move " & Adventure.GetNameFromKey(sKey1).TrimStart(" "c)
                        switch (eMoveLocationTo)
                        {
                            case MoveLocationToEnum.ToGroup:
                                {
                                sSummary &= " to " + Adventure.GetNameFromKey(sKey2);
                            case MoveLocationToEnum.FromGroup:
                                {
                                sSummary &= " from " + Adventure.GetNameFromKey(sKey2);
                        }
                        'sSummary = sSummary.Replace("on object 'the Floor'", "on the Floor")

                    case ItemEnum.SetProperties:
                        {
                        sSummary = "Set " & Adventure.GetNameFromKey(sKey2) & " of " & Adventure.GetNameFromKey(sKey1) & " to "
                        private bool bSetVar = false;
                        if (sPropertyValue.Length > 2 && sPropertyValue.StartsWith("%") && sPropertyValue.EndsWith("%") Then '&& Adventure.htblVariables.ContainsKey(value.Substring(1, value.Length - 2)))
                        {
                            For Each v As clsVariable In Adventure.htblVariables.Values
                                if (sPropertyValue = "%" + v.Name + "%")
                                {
                                    sSummary &= Adventure.GetNameFromKey(v.Key);
                                    bSetVar = True
                                    Exit For;
                                }
                            Next;
                        }
                        If ! bSetVar Then sSummary &= "'" + sPropertyValue + "'"

                    case ItemEnum.Variables:
                        {
                        private clsVariable var = Adventure.htblVariables(sKey1);
                        switch (Me.eVariables)
                        {
                            case VariablesEnum.Assignment:
                                {
                                sSummary = "Set variable " & var.Name
                                if (Adventure.htblVariables(sKey1).Length > 1)
                                {
                                    if (IsNumeric(sKey2))
                                    {
                                        sSummary &= "[" + sKey2 + "]";
                                    Else
                                        sSummary &= "[%" + Adventure.htblVariables(sKey2).Name + "%]";
                                    }
                                }
                                if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    sSummary &= " to '" + StringValue + "'";
                                Else
                                    sSummary &= " to " + StringValue ' already has quotes around it;
                                }
                            case VariablesEnum.Loop:
                                {
                                sSummary = "FOR %Loop% = " & IntValue & " TO " & CInt(Val(sKey2)) & " : SET " & var.Name & "[%Loop%] = " & StringValue & " : NEXT %Loop%"
                        }

                    case ItemEnum.SetTasks:
                        {
                        switch (Me.eSetTasks)
                        {
                            case SetTasksEnum.Execute:
                                {
                                sSummary = "Execute " & Adventure.GetNameFromKey(sKey1)
                                If StringValue <> "" Then sSummary &= " (" + StringValue.Replace("|", ", ") + ")"
                            case SetTasksEnum.Unset:
                                {
                                sSummary = "Unset " & Adventure.GetNameFromKey(sKey1)
                        }

                    case ItemEnum.EndGame:
                        {
                        switch (Me.eEndgame)
                        {
                            case EndGameEnum.Win:
                                {
                                sSummary = "End game in Victory"
                            case EndGameEnum.Lose:
                                {
                                sSummary = "End game in Defeat"
                            case EndGameEnum.Neutral:
                                {
                                sSummary = "End game"
                        }

                    case ItemEnum.Conversation:
                        {
                        switch (Me.eConversation)
                        {
                            case ConversationEnum.Greet:
                                {
                                sSummary = "Greet " & Adventure.GetNameFromKey(sKey1) & IIf(StringValue <> "", " with '" & StringValue & "'", "").ToString
                            case ConversationEnum.Ask:
                                {
                                sSummary = "Ask " & Adventure.GetNameFromKey(sKey1) & " about '" & StringValue & "'"
                            case ConversationEnum.Tell:
                                {
                                sSummary = "Tell " & Adventure.GetNameFromKey(sKey1) & " about '" & StringValue & "'"
                            case ConversationEnum.Command:
                                {
                                sSummary = "Say '" & StringValue & "' to " & Adventure.GetNameFromKey(sKey1)
                                'Case ConversationEnum.Farewell
                                '   sSummary = "Leave " & Adventure.GetNameFromKey(sKey1)
                            case ConversationEnum.EnterConversation:
                                {
                                sSummary = "Enter conversation with " & Adventure.GetNameFromKey(sKey1)
                            case ConversationEnum.LeaveConversation:
                                {
                                sSummary = "Leave conversation with " & Adventure.GetNameFromKey(sKey1)
                        }
                }

            Catch
                sSummary = "Bad Action Definition"
            }

            return sSummary;

        }
    }

}
}