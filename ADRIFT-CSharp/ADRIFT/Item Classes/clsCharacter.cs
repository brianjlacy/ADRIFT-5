using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<DebuggerDisplay("{Name}")> _;
public class clsCharacter
{
    Inherits clsItemWithProperties;

    'Private sKey As String
    'Private sName As String
    'Private sLocation As String
    private clsCharacterLocation cLocation;
    private int iMaxSize;
    private int iMaxWeight;
    private CharacterTypeEnum eCharacterType = CharacterTypeEnum.NonPlayer;
    'Private ePosition As PositionEnum
    private Description oDescription;
    private string sIsHereDesc;
    'Private eGender As GenderEnum

    private string sName;
    private string sArticle;
    private string sPrefix;
    internal New StringArrayList arlDescriptors;
    internal New WalkArrayList arlWalks;

    private New BooleanHashTable htblSeenLocation;
    private New BooleanHashTable htblSeenObject;
    private New BooleanHashTable htblSeenChar;

    'Friend htblProperties As New PropertyHashTable
    internal New TopicHashTable htblTopics;


    public void New()
    {
        Location = New clsCharacterLocation(Me)
    }


    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Name;
        }
    }


    ' Has the player been told the character exists (e.g. "a man").  Once introduced, they will be referred to as "the man"
    ' until they are no longer in the same location.
    '
    private bool bIntroduced = false;
    internal bool Introduced
        {
            get
            {
            return bIntroduced;
        }
set(Boolean)
            bIntroduced = value
        }
    }

    private string sWalkTo;
    public string WalkTo
        {
            get
            {
            return sWalkTo;
        }
set(String)
            if (value <> sWalkTo)
            {
                sWalkTo = value
            }
        }
    }


    public void DoWalk()
    {

        Static sLastPosition As String = "";

        if (WalkTo <> "")
        {
            'If Location.LocationKey = WalkTo Then
            '    WalkTo = ""
            '    Exit Sub
            'End If
            if (Location.LocationKey = sLastPosition)
            {
                WalkTo = "" ' Something has stopped us moving, so bomb out the walk
                Exit Sub;
            }
            private WalkNode node = Dijkstra(Location.LocationKey, WalkTo);
            if (node IsNot null)
            {
                while (node.iDistance > 1)
                {
                    node = node.Previous
                }
                if (Me Is Adventure.Player)
                {
                    For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                        if (Adventure.htblLocations(Location.LocationKey).arlDirections(d).LocationKey = node.Key)
                        {
                            If node.Key = WalkTo Then WalkTo = ""
#if Runner
                            private bool bAutoComplete = UserSession.bAutoComplete;
                            UserSession.bAutoComplete = false;
                            fRunner.SetInput(d.ToString);
                            sLastPosition = Location.LocationKey
                            fRunner.SubmitCommand();
                            sLastPosition = ""
                            UserSession.bAutoComplete = bAutoComplete;
#endif
                            Exit Sub;
                        }
                    Next;
                }
            Else
                WalkTo = ""
            }
        }

    }


private class WalkNode
    {
        Implements IComparable(Of WalkNode);

        public string Key;
        public int iDistance = Integer.MaxValue;
        public WalkNode Previous = null;

        public Integer Implements System.IComparable<WalkNode> CompareTo(WalkNode other)
        {
            return iDistance.CompareTo(other.iDistance);
        }
    }


    ' Adapted from http://en.wikipedia.org/wiki/Dijkstra's_algorithm
    '
    private WalkNode Dijkstra(string sFrom, string sTo)
    {

        private New Dictionary<string, WalkNode> WalkNodes;
        private New List<WalkNode> Q;

        For Each sKey As String In Adventure.htblLocations.Keys
            private New WalkNode node;
            node.Key = sKey;
            node.iDistance = Integer.MaxValue                   ' Unknown distance function from source to v;
            node.Previous = null                             ' Previous node in optimal path from source;
            WalkNodes.Add(sKey, node);
            Q.Add(node);
        Next;

        WalkNodes(sFrom).iDistance = 0                          ' Distance from source to source;
        Q.Sort();

        while (Q.Count > 0)
        {

            private WalkNode u = Q(0)                            ' vertex in Q with smallest distance;
            If u.iDistance = Integer.MaxValue Then Exit While ' all remaining vertices are inaccessible from source

            If u.Key = sTo Then Return u
            Q.Remove(u);

#if Runner
            For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                if (HasRouteInDirection(d, false, u.Key))
                {
                    private string sDest = Adventure.htblLocations(u.Key).arlDirections(d).LocationKey;
                    if (Adventure.htblLocations(sDest).SeenBy(Me.Key))
                    {
                        private int alt = u.iDistance + 1;
                        if (alt < WalkNodes(sDest).iDistance)
                        {
                            WalkNodes(sDest).iDistance = alt;
                            WalkNodes(sDest).Previous = u;
                            Q.Sort();
                        }
                    }
                }
            Next;
            'For Each d As clsDirection In Adventure.htblLocations(u.Key).arlDirections
            '    If d.LocationKey <> "" Then
            '        Dim alt As Integer = u.iDistance + 1
            '        If alt < WalkNodes(d.LocationKey).iDistance Then
            '            WalkNodes(d.LocationKey).iDistance = alt
            '            WalkNodes(d.LocationKey).Previous = u
            '            Q.Sort()
            '        End If
            '    End If
            'Next
#endif

        }

        return null;

    }



    private PerspectiveEnum ePerspective = PerspectiveEnum.SecondPerson ' Default for Player;
    internal PerspectiveEnum Perspective
        {
            get
            {
            if (eCharacterType = CharacterTypeEnum.Player)
            {
                return ePerspective;
            Else
                return PerspectiveEnum.ThirdPerson;
            }
        }
set(PerspectiveEnum)
            ePerspective = value
        }
    }


    ' This gets updated at the end of each turn, and allows any tasks to
    ' reference the parent before they are updated from task actions
    '
    private string sPrevParent;
    public string PrevParent { get; set; }
        {
            get
            {
            return sPrevParent;
        }
set(ByVal String)
            sPrevParent = value
        }
    }


    Friend Overrides ReadOnly Property Parent() As String;
        {
            get
            {
            return Location.Key;
        }
    }


public enum CharacterTypeEnum
    {
        Player;
        NonPlayer;
    }

    'Public Enum PositionEnum
    '    Standing = 0
    '    Sitting = 1
    '    Lying = 2
    'End Enum

public enum GenderEnum
    {
        Male = 0
        Female = 1
        Unknown = 2
    }

    public GenderEnum Gender { get; set; }
        {
            get
            {
            'Return eGender
            switch (GetPropertyValue("Gender"))
            {
                case "Male":
                    {
                    return GenderEnum.Male;
                case "Female":
                    {
                    return GenderEnum.Female;
                default:
                    {
                    return GenderEnum.Unknown;
            }
            'If Me.HasProperty("Gender") Then
            '    Select Case Me.htblProperties("Gender").Value
            '        Case "Male"
            '            Return GenderEnum.Male
            '        Case "Female"
            '            Return GenderEnum.Female
            '        Case Else
            '            Return GenderEnum.Unknown
            '    End Select
            'End If
        }
set(ByVal Value As GenderEnum)
            SetPropertyValue("Gender", Value.ToString);
            ''eGender = Value
            'Dim p As clsProperty
            'Dim sNewValue As String = Value.ToString ' As this gets wiped when we add the new properties...
            'If Not HasProperty("Gender") Then
            '    p = Adventure.htblAllProperties("Gender").Copy
            '    htblProperties.Add(p)
            'End If
            'htblProperties("Gender").Value = sNewValue
        }
    }


    private bool bKnown;
    public bool Known { get; set; }
        {
            get
            {
            return bKnown;
        }
set(ByVal Boolean)
            if (value <> bKnown)
            {
                'Dim p As clsProperty

                'If Not HasProperty("Known") Then
                '    p = Adventure.htblCharacterProperties("Known").Copy
                '    htblProperties.Add(p)
                'End If
                'htblProperties("Known").Selected = value
                SetPropertyValue("Known", value);
            }
        }
    }


    public CharacterTypeEnum CharacterType { get; set; }
        {
            get
            {
            return eCharacterType;
        }
set(ByVal Value As CharacterTypeEnum)
            eCharacterType = Value
        }
    }


    Public Property HasSeenLocation(ByVal sLocKey As String) As Boolean
        {
            get
            {
            if (htblSeenLocation.ContainsKey(sLocKey))
            {
                return CBool(htblSeenLocation(sLocKey));
            Else
                return false;
            }
        }
set(ByVal Value As Boolean)
            if (Not htblSeenLocation.ContainsKey(sLocKey))
            {
                htblSeenLocation.Add(Value, sLocKey);
            Else
                htblSeenLocation(sLocKey) = Value;
            }
#if Runner
            if (Me Is Adventure.Player)
            {
                For Each p As MapPage In Adventure.Map.Pages.Values
                    For Each n As MapNode In p.Nodes
                        if (n.Key = sLocKey)
                        {
                            n.Seen = Value;
                            Exit Property;
                        }
                    Next;
                Next;
            }
#endif
        }
    }

    Public Property HasSeenObject(ByVal sObKey As String) As Boolean
        {
            get
            {
            if (htblSeenObject.ContainsKey(sObKey))
            {
                return CBool(htblSeenObject(sObKey));
            Else
                return false;
            }
        }
set(ByVal Value As Boolean)
            if (Not htblSeenObject.ContainsKey(sObKey))
            {
                htblSeenObject.Add(Value, sObKey);
            Else
                htblSeenObject(sObKey) = Value;
            }
        }
    }

    Public Property HasSeenCharacter(ByVal sCharKey As String) As Boolean
        {
            get
            {
            if (htblSeenChar.ContainsKey(sCharKey))
            {
                return CBool(htblSeenChar(sCharKey));
            Else
                return false;
            }
        }
set(ByVal Value As Boolean)
            if (Not htblSeenChar.ContainsKey(sCharKey))
            {
                htblSeenChar.Add(Value, sCharKey);
            Else
                htblSeenChar(sCharKey) = Value;
            }
        }
    }

    Public Property SeenBy(ByVal sCharKey As String) As Boolean
        {
            get
            {
            return Adventure.htblCharacters(sCharKey).HasSeenCharacter(Key);
        }
set(ByVal Value As Boolean)
            Adventure.htblCharacters(sCharKey).HasSeenCharacter(Key) = Value;
        }
    }


    Public ReadOnly Property AloneWithChar As String
        {
            get
            {
            private int iCount = 0;
            private string sChar = null;
            'Dim locMe As clsLocation = LocationRoot
            'For Each locMe As clsLocation In LocationRoots.Values
            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if (ch IsNot Me)
                {
                    'Dim locChar As clsLocation = ch.LocationRoot
                    'For Each locChar As clsLocation In ch.LocationRoots.Values
                    if (Location.LocationKey = ch.Location.LocationKey Then ' If locChar IsNot null && locMe IsNot null && locChar.Key = locMe.Key)
                    {
                        iCount += 1;
                        sChar = ch.Key
                    }
                    'Next
                }
                If iCount > 1 Then Return null
            Next;
            'Next
            If iCount = 1 Then Return sChar Else Return null
        }
    }


#if Runner
    ' ExplicitPronoun - The user has said they want it to be this pronoun, so don't auto-switch it
    Friend ReadOnly Property Name(Optional ByVal Pronoun As PronounEnum = PronounEnum.Subjective, Optional bMarkAsSeen As Boolean = True, Optional ByVal bAllowPronouns As Boolean = True, Optional ByVal Article As ArticleTypeEnum = ArticleTypeEnum.Indefinite, Optional ByVal bForcePronoun As Boolean = False, Optional ByVal bExplicitArticle As Boolean = False) As String
        {
            get
            {
            With UserSession;


                private bool bReplaceWithPronoun = bForcePronoun;

                if (.bDisplaying)
                {
                    If ! bExplicitArticle && Introduced && Article = ArticleTypeEnum.Indefinite Then Article = ArticleTypeEnum.Definite
                    if (bMarkAsSeen)
                    {
                        Introduced = True
                        If ! Adventure.lCharMentionedThisTurn(Gender).Contains(Key) Then Adventure.lCharMentionedThisTurn(Gender).Add(Key)
                    }

                    ' If we already have mentioned this key, and it's the most recent key, we can replace with pronoun
                    If Perspective = PerspectiveEnum.FirstPerson || Perspective = PerspectiveEnum.SecondPerson Then bReplaceWithPronoun = true

                    if (.PronounKeys.Count > 0)
                    {
                        private PronounInfo oPreviousPronoun = null;
                        for (int i = .PronounKeys.Count - 1; i <= 0; i += -1)
                        {
                            With .PronounKeys(i);
                                if (.Gender = Gender && .Key = Key)
                                {
                                    oPreviousPronoun = UserSession.PronounKeys(i)
                                    Exit For;
                                }
                            }
                        Next;
                        if (oPreviousPronoun IsNot null)
                        {
                            bReplaceWithPronoun = True
                            if (oPreviousPronoun.Pronoun = PronounEnum.Subjective && Pronoun = PronounEnum.Objective)
                            {
                                Pronoun = PronounEnum.Reflective
                            }
                        }
                    }
                }
                If Pronoun = PronounEnum.None Then bReplaceWithPronoun = false

                if (Not bAllowPronouns || Not bReplaceWithPronoun)
                {
                    ' Display the actual name/descriptor
                    private string sName;
                    if (HasProperty("Known"))
                    {
                        if (GetProperty("Known").Selected)
                        {
                            sName = ProperName
                        Else
                            sName = Descriptor(Article)
                        }
                    Else
                        if (Descriptor <> "")
                        {
                            sName = Descriptor(Article)
                        Else
                            sName = ProperName
                        }
                    }
                    if (Pronoun = PronounEnum.Possessive)
                    {
                        return sName + "'s";
                    Else
                        return sName;
                    }
                Else
                    ' Display the pronoun
                    switch (Perspective)
                    {
                        case PerspectiveEnum.FirstPerson:
                            {
                            switch (Pronoun)
                            {
                                case PronounEnum.Objective:
                                    {
                                    return "me";
                                case PronounEnum.Possessive:
                                    {
                                    return "my" ' "mine";
                                case PronounEnum.Reflective:
                                    {
                                    return "myself";
                                case PronounEnum.Subjective:
                                    {
                                    return "I";
                            }
                        case PerspectiveEnum.SecondPerson:
                            {
                            switch (Pronoun)
                            {
                                case PronounEnum.Objective:
                                    {
                                    return "you";
                                case PronounEnum.Possessive:
                                    {
                                    return "your" ' "yours";
                                case PronounEnum.Reflective:
                                    {
                                    return "yourself";
                                case PronounEnum.Subjective:
                                    {
                                    return "you";
                            }
                        case PerspectiveEnum.ThirdPerson:
                            {
                            switch (Pronoun)
                            {
                                case PronounEnum.Objective:
                                    {
                                    switch (Gender)
                                    {
                                        case GenderEnum.Male:
                                            {
                                            return "him";
                                        case GenderEnum.Female:
                                            {
                                            return "her";
                                        case GenderEnum.Unknown:
                                            {
                                            return "it";
                                    }
                                case PronounEnum.Possessive:
                                    {
                                    switch (Gender)
                                    {
                                        case GenderEnum.Male:
                                            {
                                            return "his";
                                        case GenderEnum.Female:
                                            {
                                            return "her" ' "hers";
                                        case GenderEnum.Unknown:
                                            {
                                            return "its";
                                    }
                                case PronounEnum.Reflective:
                                    {
                                    switch (Gender)
                                    {
                                        case GenderEnum.Male:
                                            {
                                            return "himself";
                                        case GenderEnum.Female:
                                            {
                                            return "herself";
                                        case GenderEnum.Unknown:
                                            {
                                            return "itself";
                                    }
                                case PronounEnum.Subjective:
                                    {
                                    switch (Gender)
                                    {
                                        case GenderEnum.Male:
                                            {
                                            return "he";
                                        case GenderEnum.Female:
                                            {
                                            return "she";
                                        case GenderEnum.Unknown:
                                            {
                                            return "it";
                                    }
                            }
                    }
                }

                return "";

            }
        }
    }
#else
    Friend ReadOnly Property Name(Optional ByVal a As Boolean = False, Optional ByVal b As Boolean = False, Optional ByVal c As Boolean = False, Optional ByVal d As ArticleTypeEnum = ArticleTypeEnum.Indefinite, Optional ByVal e As Boolean = False, Optional f As Boolean = False) As String
        {
            get
            {
            if (ProperName <> "")
            {
                return ProperName;
            Else
                return Descriptor;
            }
        }
    }
#endif



    'Private bChangedName As Boolean = False
#if Generator
    public string ProperName { get; set; }
        {
            get
            {
            return sName;
#else
    Friend Property ProperName(Optional ByVal Pronoun As PronounEnum = PronounEnum.Subjective) As String
        {
            get
            {
            'If bDisplaying Then PronounKeys.Add(Key, Pronoun)
            If sName <> "" Then Return sName Else Return "Anonymous"
#endif
        }
set(ByVal Value As String)
            'If sName <> "" AndAlso Value <> sName Then bChangedName = True
            sName = Value
        }
    }


    Friend ReadOnly Property Descriptor(Optional ByVal Article As ArticleTypeEnum = ArticleTypeEnum.Indefinite) As String
        {
            get
            {
            if (arlDescriptors.Count > 0)
            {
                private string sArticle2 = null ' sArticle;
                switch (Article)
                {
                    case ArticleTypeEnum.Definite:
                        {
                        If sArticle <> "" Then sArticle2 = "the"
                    case ArticleTypeEnum.Indefinite:
                        {
                        sArticle2 = sArticle
                    case ArticleTypeEnum.None:
                        {
                        sArticle2 = ""
                }
                'If TheName AndAlso sArticle <> "" Then sArticle2 = "the"
                If sArticle <> "" Then sArticle2 &= " "
                if (sPrefix <> "")
                {
                    return sArticle2 + sPrefix + " " + arlDescriptors(0);
                Else
                    return sArticle2 + arlDescriptors(0);
                }
            Else
                return "";
            }
        }
    }


    public void Move(string ToWhere)
    {

        if (Adventure.htblLocations.ContainsKey(ToWhere))
        {
            private New clsCharacterLocation(Me) loc;
            loc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
            loc.Key = ToWhere;
            Move(loc);
        ElseIf ToWhere = HIDDEN Then
            private New clsCharacterLocation(Me) loc;
            loc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
            loc.Key = "";
            Move(loc);
        }

    }
    public void Move(clsCharacterLocation ToWhere)
    {

#if Runner
        if (Me Is Adventure.Player)
        {
            For Each t As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.SystemTasks).Values
                if (t.Repeatable || Not t.Completed)
                {
                    'For Each sKey As String In Adventure.Player.LocationRoots.Keys
                    if (Adventure.Player.Location.LocationKey <> ToWhere.LocationKey && t.LocationTrigger = ToWhere.LocationKey)
                    {
                        ' Ok, we need to trigger this task
                        Adventure.qTasksToRun.Enqueue(t.Key);
                    }
                    'Next
                }
            Next;
        }
#endif

        ' Ensure that character Key is not this character, or on this character
        Me.Location = ToWhere;

        ' Update any 'seen' things
        if (ToWhere.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
        {
            'sLastLocationRoot = ToWhere.Key
            Location.sLastLocationKey = ToWhere.Key;
            If htblSeenLocation.ContainsKey(ToWhere.Key) Then htblSeenLocation(ToWhere.Key) = true Else htblSeenLocation.Add(true, ToWhere.Key)
            if (Adventure.htblLocations.ContainsKey(ToWhere.Key))
            {
                For Each ob As clsObject In Adventure.htblLocations(ToWhere.Key).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, True).Values
                    If htblSeenObject.ContainsKey(ob.Key) Then htblSeenObject(ob.Key) = true Else htblSeenObject.Add(true, ob.Key)
                Next;
                For Each ch As clsCharacter In Adventure.htblLocations(ToWhere.Key).CharactersVisibleAtLocation.Values
                    If htblSeenChar.ContainsKey(ch.Key) Then htblSeenChar(ch.Key) = true Else htblSeenChar.Add(true, ch.Key)
                Next;
            }
        }

#if Runner
        if (Adventure.sConversationCharKey <> "")
        {
            if (Adventure.htblCharacters(Adventure.sConversationCharKey).Location.Key <> Adventure.Player.Location.Key)
            {
                if (Me.Key = Adventure.Player.Key)
                {
                    private clsTopic farewell = UserSession.FindConversationNode(Adventure.htblCharacters(Adventure.sConversationCharKey), clsAction.ConversationEnum.Farewell, "");
                    if (farewell IsNot null)
                    {
                        UserSession.Display(farewell.oConversation.ToString);
                    }
                }
                Adventure.sConversationCharKey = "";
                Adventure.sConversationNode = "";
            }
        }

        '#If Not www Then
        if (Me Is Adventure.Player)
        {
            Adventure.Map.RefreshNode(Adventure.Player.Location.LocationKey);
            UserSession.Map.SelectNode(Adventure.Player.Location.LocationKey);
#if www
            fRunner.RefreshMap();
#endif
        }
        '#End If
#endif

    }


    public clsCharacterLocation Location { get; set; }
        {
            get
            {
            return cLocation;
        }
set(ByVal Value As clsCharacterLocation)
            try
            {
                cLocation = Value
            }
            catch (Exception ex)
            {
                ErrMsg("Error moving character " + Name + " to location", ex);
            }
        }

    }


    '' Returns the actual rooms an object is in, regardless of actual location
    'Private sLastLocationRoot As String
    'Friend ReadOnly Property LocationRoot() As clsLocation ' LocationHashTable
    '    Get
    '        'Dim htblLocRoot As New LocationHashTable
    '        LocationRoot = Nothing
    '        Try
    '            Select Case Location.ExistWhere
    '                Case clsCharacterLocation.ExistsWhereEnum.AtLocation
    '                    If Location.Key IsNot Nothing AndAlso Location.Key <> HIDDEN Then Return Adventure.htblLocations(Location.Key) ' htblLocRoot.Add(Adventure.htblLocations(Location.Key), Location.Key)
    '                Case clsCharacterLocation.ExistsWhereEnum.Hidden
    '                    ' Fall out
    '                Case clsCharacterLocation.ExistsWhereEnum.InObject, clsCharacterLocation.ExistsWhereEnum.OnObject
    '                    If Adventure.htblObjects(Location.Key).LocationRoots.ContainsKey(sLastLocationRoot) Then
    '                        Return Adventure.htblLocations(sLastLocationRoot)
    '                    Else
    '                        ' Hmm...
    '                        For Each loc As clsLocation In Adventure.htblObjects(Location.Key).LocationRoots.Values
    '                            Return loc
    '                        Next
    '                    End If
    '                    'Return Adventure.htblObjects(Location.Key).LocationRoots("") ' for now
    '                Case clsCharacterLocation.ExistsWhereEnum.OnCharacter
    '                    Return Adventure.htblCharacters(Location.Key).LocationRoot
    '                    'Dim locChar As clsCharacterLocation = Adventure.htblCharacters(Location.Key).Location
    '                    'If locChar.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation Then htblLocRoot.Add(Adventure.htblLocations(locChar.Key), locChar.Key)
    '                    'Return htblLocRoot
    '                Case Else
    '                    TODO("LocationRoot for " & Location.ExistWhere.ToString)
    '                    Return Nothing
    '            End Select

    '            'Return htblLocRoot
    '        Finally
    '            If LocationRoot IsNot Nothing AndAlso sLastLocationRoot <> LocationRoot.Key Then sLastLocationRoot = LocationRoot.Key
    '        End Try

    '    End Get
    'End Property


    public int MaxSize { get; set; }
        {
            get
            {
            return iMaxSize;
        }
set(ByVal Value As Integer)
            iMaxSize = Value
        }
    }


    public int MaxWeight { get; set; }
        {
            get
            {
            return iMaxWeight;
        }
set(ByVal Value As Integer)
            iMaxWeight = Value
        }
    }

    public string Article { get; set; }
        {
            get
            {
            return sArticle;
        }
set(ByVal Value As String)
            sArticle = Value
        }
    }

    public string Prefix { get; set; }
        {
            get
            {
            return sPrefix;
        }
set(ByVal Value As String)
            sPrefix = Value
        }
    }

    internal Description Description { get; set; }
        {
            get
            {
            If oDescription == null Then oDescription = New Description
            return oDescription;
        }
set(ByVal Value As Description)
            oDescription = Value
        }
    }

    public string IsHereDesc { get; set; }
        {
            get
            {
            return GetPropertyValue("CharHereDesc");
            'If Me.HasProperty("CharHereDesc") Then
            '    Return htblProperties("CharHereDesc").Value
            'Else
            '    Return ""
            'End If
        }
set(ByVal Value As String)
            if (Value <> "")
            {
                'If Not HasProperty("CharHereDesc") Then
                '    Dim p As clsProperty = Adventure.htblCharacterProperties("CharHereDesc").Copy
                '    htblProperties.Add(p)
                'End If
                'htblProperties("CharHereDesc").Value = Value
                SetPropertyValue("CharHereDesc", Value);
            Else
                'If HasProperty("CharHereDesc") Then htblProperties.Remove("CharHereDesc")
                RemoveProperty("CharHereDesc");
            }
        }
    }

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            private clsCharacter ch = CType(Me.MemberwiseClone, clsCharacter);
            ch.htblActualProperties = (PropertyHashTable)(ch.htblActualProperties.Clone);
            ch.arlDescriptors = ch.arlDescriptors.Clone;
            return ch;
        }
    }


    Friend ReadOnly Property ChildrenObjects(ByVal WhereChildren As clsObject.WhereChildrenEnum, Optional ByVal bRecursive As Boolean = False) As ObjectHashTable
        {
            get
            {
            private New ObjectHashTable htblChildren;

            For Each ob As clsObject In Adventure.htblObjects.Values
                if (Not ob.IsStatic)
                {
                    if (ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter Or ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter)
                    {
                        if (ob.Location.Key = Me.Key)
                        {
                            htblChildren.Add(ob, ob.Key);
                            if (bRecursive)
                            {
                                For Each obChild As clsObject In ob.Children(clsObject.WhereChildrenEnum.Everything, True).Values
                                    htblChildren.Add(obChild, obChild.Key);
                                Next;
                            }
                        }
                    }
                }
            Next;

            return htblChildren;
        }
    }


#if Runner
    internal New Generic.Dictionary<string, bool> dictHasRouteCache;
    internal New Generic.Dictionary<string, string> dictRouteErrors;
    internal bool HasRouteInDirection(DirectionsEnum drn, bool bIgnoreRestrictions, string sFromLocation = "", ref sErrorMessage As String = "")
    {

        If sFromLocation = "" Then sFromLocation = Location.LocationKey
        If ! Adventure.htblLocations.ContainsKey(sFromLocation) Then Return false

        private clsDirection d = Adventure.htblLocations(sFromLocation).arlDirections(drn);
        if (d.LocationKey <> "")
        {
            if (bIgnoreRestrictions)
            {
                return true;
            Else
                private bool bResult;
                if (dictHasRouteCache.ContainsKey(sFromLocation + drn.ToString))
                {
                    bResult = dictHasRouteCache(sFromLocation & drn.ToString)
                    If ! bResult Then sErrorMessage = dictRouteErrors(sFromLocation + drn.ToString)
                    return bResult;
                }
                ' evaluate direction restrictions
                bResult = UserSession.PassRestrictions(d.Restrictions)
                If ! bResult Then sErrorMessage = UserSession.sRestrictionText
                dictHasRouteCache.Add(sFromLocation + drn.ToString, bResult);
                dictRouteErrors.Add(sFromLocation + drn.ToString, sErrorMessage);
                If ! bResult Then d.bEverBeenBlocked = true
                return bResult;
            }
        Else
            return false;
        }

    }


    Public ReadOnly Property IsInGroupOrLocation(ByVal sGroupOrLocationKey As String) As Boolean
        {
            get
            {
            if (Adventure.htblLocations.ContainsKey(sGroupOrLocationKey))
            {
                if (Me.Location.LocationKey = sGroupOrLocationKey)
                {
                    return true;
                }
            ElseIf Adventure.htblGroups.ContainsKey(sGroupOrLocationKey) Then
                if (Adventure.htblGroups(sGroupOrLocationKey).arlMembers.Contains(Me.Location.LocationKey))
                {
                    return true;
                }
            }

            return false;
        }
    }


    Public ReadOnly Property BoundVisible As String
        {
            get
            {
            With Me.Location;
                switch (.ExistWhere)
                {
                    case clsCharacterLocation.ExistsWhereEnum.AtLocation:
                        {
                        return .Key;
                    case clsCharacterLocation.ExistsWhereEnum.Hidden:
                        {
                        return HIDDEN;
                    case clsCharacterLocation.ExistsWhereEnum.InObject:
                        {
                        With Adventure.htblObjects(.Key);
                            if (Not .Openable || .IsOpen || .IsTransparent)
                            {
                                return Adventure.htblObjects(.Key).BoundVisible;
                            Else
                                return .Key;
                            }
                        }
                    case clsCharacterLocation.ExistsWhereEnum.OnCharacter:
                        {
                        return Adventure.htblCharacters(.Key).BoundVisible;
                    case clsCharacterLocation.ExistsWhereEnum.OnObject:
                        {
                        return Adventure.htblObjects(.Key).BoundVisible;
                    case clsCharacterLocation.ExistsWhereEnum.Uninitialised:
                        {
                        return HIDDEN;
                }
                return HIDDEN;
            }
        }
    }


    internal bool CanSeeCharacter(string sCharKey)
    {
        return IsVisibleTo(sCharKey);
    }
    Public ReadOnly Property IsVisibleTo(ByVal sCharKey As String) As Boolean
        {
            get
            {
            If sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private string sBoundVisible = BoundVisible;
            private string sBoundVisibleCh = Adventure.htblCharacters(sCharKey).BoundVisible;

            If sBoundVisible = HIDDEN Then Return false

            return sBoundVisible = sBoundVisibleCh;

        }
    }


    Public ReadOnly Property IsVisibleAtLocation(ByVal sLocationKey As String) As Boolean
        {
            get
            {
            private string sBoundVisible = BoundVisible;

            If sBoundVisible = HIDDEN Then Return false

            if (Adventure.htblGroups.ContainsKey(sBoundVisible))
            {
                return Adventure.htblGroups(sBoundVisible).arlMembers.Contains(sLocationKey);
            Else
                return sBoundVisible = sLocationKey;
            }
        }
    }

    Public ReadOnly Property CanSeeObject(ByVal sObKey As String) As Boolean
        {
            get
            {
            private string sBoundVisible = BoundVisible;

            If sBoundVisible = HIDDEN Then Return false

            if (Adventure.htblGroups.ContainsKey(sObKey))
            {
                For Each sOb As String In Adventure.htblGroups(sObKey).arlMembers
                    If CanSeeObject(sOb) Then Return true
                Next;
                return false;
            }

            private string sBoundVisibleOb = Adventure.htblObjects(sObKey).BoundVisible;

            if (Adventure.htblGroups.ContainsKey(sBoundVisibleOb))
            {
                return Adventure.htblGroups(sBoundVisibleOb).arlMembers.Contains(sBoundVisible);
            Else
                return sBoundVisible = sBoundVisibleOb || sBoundVisible = sObKey ' Allow us to see the object we're in, otherwise we can't do anything with it (open it again!);
            }
        }
    }

    'Public ReadOnly Property IsVisibleTo(ByVal sCharKey As String) As Boolean
    '    Get
    '        TODO()
    '    End Get
    'End Property

    'Public ReadOnly Property IsVisibleAtLocation(ByVal sLocationKey As String) As Boolean
    '    Get
    '        With Me.Location
    '            Select Case .ExistWhere
    '                Case clsCharacterLocation.ExistsWhereEnum.AtLocation
    '                    Return .Key = sLocationKey
    '                Case clsCharacterLocation.ExistsWhereEnum.Hidden
    '                    Return False
    '                Case clsCharacterLocation.ExistsWhereEnum.InObject
    '                    With Adventure.htblObjects(.Key)
    '                        If Not .Openable OrElse .IsOpen Then
    '                            Return Adventure.htblObjects(.Key).IsVisibleAtLocation(sLocationKey)
    '                        End If
    '                    End With
    '                Case clsCharacterLocation.ExistsWhereEnum.OnCharacter
    '                    Return Adventure.htblCharacters(.Key).IsVisibleAtLocation(sLocationKey)
    '                Case clsCharacterLocation.ExistsWhereEnum.OnObject
    '                    Return Adventure.htblObjects(.Key).IsVisibleAtLocation(sLocationKey)
    '            End Select
    '        End With
    '    End Get
    'End Property


    Public ReadOnly Property IsWearingObject(ByVal sObKey As String, Optional ByVal bDirectly As Boolean = True) As Boolean
        {
            get
            {
            If sObKey = NOOBJECT Then Return WornObjects.Count = 0
            If sObKey = ANYOBJECT Then Return WornObjects.Count > 0

            With Adventure.htblObjects(sObKey).Location;
                switch (.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                        {
                        If .Key = Me.Key || (.Key = "%Player%" && Me.eCharacterType = CharacterTypeEnum.Player) Then Return true
                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                        {
                        if (Not bDirectly)
                        {
                            return IsWearingObject(Adventure.htblObjects(sObKey).Parent);
                        Else
                            return false;
                        }
                }
                return false;
            }
        }
    }


    Public ReadOnly Property sRegularExpressionString(Optional ByVal bMyRE As Boolean = False, Optional ByVal bPlural As Boolean = False, Optional ByVal bPrefixMandatory As Boolean = False) As String
        {
            get
            {
            private StringArrayList arl = arlDescriptors;

            if (bMyRE)
            {
                ' The ADRIFT 'advanced command construction' expression
                private string sRE = "{" + sArticle + "/the} ";
                if (sPrefix <> "")
                {
                    For Each sSinglePrefix As String In Split(sPrefix, " ")
                        If sSinglePrefix <> "" Then sRE &= "{" + sSinglePrefix.ToLower + "} "
                    Next;
                }
                sRE &= "[";
                For Each sNoun As String In arl
                    sRE &= sNoun.ToLower + "/";
                Next;
                'If arl.Count = 0 Then sRE &= "|" ' Fudge
                If ! Adventure.htblCharacterProperties.ContainsKey("Known") || HasProperty("Known") || arl.Count = 0 Then sRE &= ProperName.ToLower + "/"
                return Left(sRE, sRE.Length - 1) + "]";
            Else
                ' Real Regular Expressions
                private string sRE = "(";
                If sArticle <> "" Then sRE &= sArticle + " |"
                sRE &= "the )?";
                For Each sSinglePrefix As String In Split(sPrefix, " ")
                    If sSinglePrefix <> "" Then sRE &= "(" + sSinglePrefix.ToLower + " )?"
                Next;
                sRE &= "(";
                For Each sNoun As String In arl
                    sRE &= sNoun.ToLower + "|";
                Next;
                'If arl.Count = 0 Then sRE &= "|" ' Fudge
                If ! Adventure.htblCharacterProperties.ContainsKey("Known") || HasProperty("Known") || arl.Count = 0 Then sRE &= ProperName.ToLower + "|"
                return Left(sRE, sRE.Length - 1) + ")";
            }

        }
    }


    ' Returns True if character is directly inside parent, or if on/in something that is inside it
    Public ReadOnly Property IsInside(ByVal sParentKey As String) As Boolean
        {
            get
            {
            if ((Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject || Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject) && Parent <> "")
            {
                if (Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject && Parent = sParentKey)
                {
                    return true;
                Else
                    private clsObject obParent = Adventure.htblObjects(Parent);
                    return obParent.IsInside(sParentKey);
                }
            Else
                return false;
            }
        }
    }


    ' Returns True if character is directly on parent, or if on/in something that is inside it
    Public ReadOnly Property IsOn(ByVal sParentKey As String) As Boolean
        {
            get
            {
            if ((Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject || Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject || Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter) && Parent <> "")
            {
                if ((Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject || Me.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter) && Parent = sParentKey)
                {
                    return true;
                Else
                    if (Adventure.htblObjects.ContainsKey(Parent))
                    {
                        private clsObject obParent = Adventure.htblObjects(Parent);
                        return obParent.IsOn(sParentKey);
                    ElseIf Adventure.htblCharacters.ContainsKey(Parent) Then
                        private clsCharacter chParent = Adventure.htblCharacters(Parent);
                        return chParent.IsOn(sParentKey);
                    }
                }
            Else
                return false;
            }
        }
    }


    Public ReadOnly Property IsHoldingObject(ByVal sObKey As String, Optional ByVal bDirectly As Boolean = False) As Boolean
        {
            get
            {
            If sObKey = NOOBJECT Then Return HeldObjects.Count = 0
            If sObKey = ANYOBJECT Then Return HeldObjects.Count > 0

            With Adventure.htblObjects(sObKey).Location;
                switch (.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                        {
                        If .Key = Me.Key || (.Key = "%Player%" && Me.eCharacterType = CharacterTypeEnum.Player) Then Return true
                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                        {
                        if (Not bDirectly)
                        {
                            'If Adventure.htblObjects(sObKey).Location.Key = sObKey Then Return False
                            return IsHoldingObject(Adventure.htblObjects(sObKey).Location.Key);
                        Else
                            return false;
                        }
                }
                return false;
            }
        }
    }


    public bool IsAlone { get; }
        {
            get
            {
            For Each chr As clsCharacter In Adventure.htblCharacters.Values
                If chr.Key <> Key && chr.Location.LocationKey = Location.LocationKey Then Return false
            Next;
            return true;
        }
    }


    'Public ReadOnly Property CanSeeCharacter(ByVal sCharKey As String) As Boolean
    '    Get
    '        If Adventure.htblCharacters.ContainsKey(sCharKey) Then
    '            ' TODO: Should really make sure neither character is in a closed object
    '            Return Location.LocationKey = Adventure.htblCharacters(sCharKey).Location.LocationKey
    '        Else
    '            Throw New Exception("Key " & sCharKey & " is not a character key.")
    '        End If
    '    End Get
    'End Property



    Friend ReadOnly Property Children(Optional ByVal bRecursive As Boolean = False) As CharacterHashTable
        {
            get
            {
            private New CharacterHashTable htblChildren;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if (ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter)
                {
                    if (ch.Location.Key = Me.Key)
                    {
                        htblChildren.Add(ch, ch.Key);
                        if (bRecursive)
                        {
                            For Each chChild As clsCharacter In ch.Children(True).Values
                                htblChildren.Add(chChild, chChild.Key);
                            Next;
                        }
                    }
                }
            Next;
            if (bRecursive)
            {
                For Each ob As clsObject In Adventure.htblObjects.Values
                    if (ob.IsStatic)
                    {
                        switch (ob.Location.StaticExistWhere)
                        {
                            case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                                {
                                if (ob.Location.Key = Me.Key)
                                {
                                    For Each chInOrOnObThatIsPartOfChar As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                        htblChildren.Add(chInOrOnObThatIsPartOfChar, chInOrOnObThatIsPartOfChar.Key);
                                    Next;
                                }
                        }
                    Else
                        switch (ob.Location.DynamicExistWhere)
                        {
                            case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                            case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                                {
                                if (ob.Location.Key = Me.Key)
                                {
                                    For Each chInOrOnObThatIsHeldWornByChar As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                        htblChildren.Add(chInOrOnObThatIsHeldWornByChar, chInOrOnObThatIsHeldWornByChar.Key);
                                    Next;
                                }
                        }
                    }
                Next;
            }

            return htblChildren;
        }
    }


    'Public ReadOnly Property CanSeeObject(ByVal sObKey As String) As Boolean
    '    Get
    '        If sObKey = ANYOBJECT Then
    '            For Each sObKey2 As String In Adventure.htblObjects.Keys
    '                If CanSeeObject(sObKey2) Then Return True
    '            Next
    '            Return False
    '        ElseIf Adventure.htblObjects.ContainsKey(sObKey) Then
    '            Return Adventure.htblObjects(sObKey).IsVisibleTo(Me.Key)
    '        ElseIf Adventure.htblGroups.ContainsKey(sObKey) AndAlso Adventure.htblGroups(sObKey).GroupType = clsGroup.GroupTypeEnum.Objects Then
    '            ' Can we see any object in this group
    '            With Adventure.htblGroups(sObKey)
    '                For Each sOb As String In .arlMembers
    '                    If CanSeeObject(sOb) Then Return True
    '                Next
    '                Return False
    '            End With
    '        Else
    '            Throw New Exception("Key " & sObKey & " is not an object or object group.")
    '        End If
    '    End Get
    'End Property

    Friend ReadOnly Property HeldObjects(Optional ByVal bRecursive As Boolean = False) As ObjectHashTable
        {
            get
            {
            private New ObjectHashTable htblHeldObjects;

            For Each ob As clsObject In Adventure.DynamicObjects ' Adventure.htblObjects.Values
                if (ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter)
                {
                    if (ob.Location.Key = Me.Key || (ob.Location.Key = "%Player%" && Me.eCharacterType = CharacterTypeEnum.Player))
                    {
                        htblHeldObjects.Add(ob, ob.Key);
                        if (bRecursive)
                        {
                            For Each obChild As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                htblHeldObjects.Add(obChild, obChild.Key);
                            Next;
                        }
                    }
                }
            Next;

            return htblHeldObjects;
        }
    }

    Friend ReadOnly Property WornObjects(Optional ByVal bRecursive As Boolean = False) As ObjectHashTable
        {
            get
            {
            private New ObjectHashTable htblWornObjects;

            For Each ob As clsObject In Adventure.htblObjects.Values
                if (ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter)
                {
                    if (ob.Location.Key = Me.Key || (ob.Location.Key = "%Player%" && Me.eCharacterType = CharacterTypeEnum.Player))
                    {
                        htblWornObjects.Add(ob, ob.Key);
                        if (bRecursive)
                        {
                            For Each obChild As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                htblWornObjects.Add(obChild, obChild.Key);
                            Next;
                        }
                    }
                }
            Next;

            return htblWornObjects;
        }
    }

    public string ListExits(string sFromLocation = "", ref iExitCount As Integer = 0)
    {

        private string sExits = "";

        If sFromLocation = "" Then sFromLocation = Adventure.Player.Location.LocationKey
        for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest ' Adventure.iCompassPoints; d++)
        {
            if (Me.HasRouteInDirection(d, false, sFromLocation))
            {
                sExits &= DirectionName(d) + ", ";
                iExitCount += 1;
            }
        Next;

        If Right(sExits, 2) = ", " Then sExits = Left(sExits, sExits.Length - 2) + ""

        if (iExitCount > 1)
        {
            sExits = Left(sExits, sExits.LastIndexOf(", ")) & " and " & Right(sExits, sExits.Length - sExits.LastIndexOf(", ") - 2)
        }
        'If iExitCount > 2 Then
        '    sExits = Left(sExits, sExits.LastIndexOf(", ")) & " " & Right(sExits, sExits.Length - sExits.LastIndexOf(", ") - 2)
        'End If

        If sExits = "" Then sExits = "nowhere"

        return LCase(sExits);

    }

#endif

    Friend Overrides ReadOnly Property AllDescriptions() As Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            all.Add(oDescription);
#if Runner
            For Each p As clsProperty In htblProperties.Values
#else
            For Each p As clsProperty In htblActualProperties.Values
#endif
                all.Add(p.StringData);
            Next;
            For Each t As clsTopic In htblTopics.Values
                For Each r As clsRestriction In t.arlRestrictions
                    all.Add(r.oMessage);
                Next;
                all.Add(t.oConversation);
            Next;
            For Each w As clsWalk In arlWalks
                for (int i = 0; i <= w.SubWalks.Length - 1; i++)
                {
                    all.Add(w.SubWalks(i).oDescription);
                Next;
            Next;
            return all;
        }
    }





    public override void EditItem()
    {
#if Generator
        private New frmCharacter(Me, True) fCharacter;
#endif
    }

    Protected Overrides ReadOnly Property PropertyGroupType() As clsGroup.GroupTypeEnum
        {
            get
            {
            return clsGroup.GroupTypeEnum.Characters;
        }
    }


    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        For Each w As clsWalk In arlWalks
            For Each s As clsWalk.clsStep In w.arlSteps
                If s.sLocation = sKey Then iCount += 1
            Next;
            for (int i = 0; i <= w.SubWalks.Length - 1; i++)
            {
                With w.SubWalks(i);
                    If .sKey2 = sKey || .sKey3 = sKey Then iCount += 1
                }
            Next;
            for (int i = 0; i <= w.WalkControls.Length - 1; i++)
            {
                With w.WalkControls(i);
                    If .sTaskKey = sKey Then iCount += 1
                }
            Next;
        Next;
        For Each t As clsTopic In htblTopics.Values
            iCount += t.arlRestrictions.ReferencesKey(sKey);
            iCount += t.arlActions.ReferencesKey(sKey);
        Next;
        If Location.Key = sKey Then iCount += 1
        iCount += htblActualProperties.ReferencesKey(sKey);

        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        For Each w As clsWalk In arlWalks
            for (int i = w.arlSteps.Count - 1; i <= 0; i += -1)
            {
                If (clsWalk.clsStep)(w.arlSteps(i)).sLocation = sKey Then w.arlSteps.RemoveAt(i)
            Next;
            for (int i = 0; i <= w.SubWalks.Length - 1; i++)
            {
                If w.SubWalks(i).sKey2 = sKey Then w.SubWalks(i).sKey2 = ""
                If w.SubWalks(i).sKey3 = sKey Then w.SubWalks(i).sKey3 = ""
            Next;
            for (int i = 0; i <= w.WalkControls.Length - 1; i++)
            {
                if (w.WalkControls(i).sTaskKey = sKey)
                {
                    for (int j = w.WalkControls.Length - 2; j <= i; j += -1)
                    {
                        w.WalkControls(j) = w.WalkControls(j + 1);
                    Next;
                    ReDim Preserve w.WalkControls(w.WalkControls.Length - 2);
                }
            Next;
        Next;
        For Each t As clsTopic In htblTopics.Values
            If ! t.arlRestrictions.DeleteKey(sKey) Then Return false
            If ! t.arlActions.DeleteKey(sKey) Then Return false
        Next;
        if (Location.Key = sKey)
        {
            Location.Key = "";
            Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
        }
        If ! htblActualProperties.DeleteKey(sKey) Then Return false

        return true;

    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sName, sSearchString, sReplace, bFindAll);
        iReplacements += MyBase.FindStringInStringProperty(Me.sArticle, sSearchString, sReplace, bFindAll);
        for (int i = arlDescriptors.Count - 1; i <= 0; i += -1)
        {
            iReplacements += MyBase.FindStringInStringProperty(Me.arlDescriptors(i), sSearchString, sReplace, bFindAll);
        Next;
        return iReplacements - iCount;
    }

}



<DebuggerDisplay("{Summary} : {Keywords}")> _;
public class clsTopic
{

    public string Key;
    public string ParentKey;
    public string Summary;
    public string Keywords;
    Public bIntroduction, bAsk, bTell, bCommand, bFarewell, bStayInNode As Boolean
    internal Description oConversation;
    internal New RestrictionArrayList arlRestrictions;
    internal New ActionArrayList arlActions;

    public void New()
    {
        oConversation = New Description
    }

    public clsTopic Clone()
    {
        private clsTopic topic = CType(Me.MemberwiseClone, clsTopic);
        topic.arlRestrictions = topic.arlRestrictions.Copy;
        topic.arlActions = topic.arlActions.Copy;
        return topic;
    }

}


public class clsWalk
{

public class clsStep
    {
        public string sLocation;
        internal New FromTo ftTurns;
    }

    private string sDescription;
    'Private iLength As Integer
    private bool bLoop;
    private string sFromDesc;
    private string sToDesc;
    private bool bShowMove;
    private bool bStartActive;
    internal int iTimerToEndOfWalk;
    private int iLastSubWalkTime = 0;

    public String ' This is the key of the character sKey;

private enum Command
    {
        [null] = 0;
        Start = 1
        [Stop] = 2;
        Pause = 3
        [Resume] = 4;
        Restart = 5
    }
    private Command NextCommand = Command.null;

public enum StatusEnum
    {
        NotYetStarted = 0
        Running = 1
        Paused = 3
        Finished = 4
    }
    internal StatusEnum Status;


public class SubWalk
    {
        Implements ICloneable;

public enum WhenEnum
        {
            FromLastSubWalk;
            FromStartOfWalk;
            BeforeEndOfWalk;
            ComesAcross;
        }
        public WhenEnum eWhen;

public enum WhatEnum
        {
            DisplayMessage;
            ExecuteTask;
            UnsetTask;
        }
        public WhatEnum eWhat;

        internal New FromTo ftTurns;
        internal New Description oDescription;
        public string sKey;
        public string sKey2;
        public string sKey3;

        internal bool bSameLocationAsChar = false;

        private Object Implements System.ICloneable.Clone Clone()
        {
            private SubWalk se;
            se = CType(Me.MemberwiseClone, SubWalk)
            se.ftTurns = ftTurns.CloneMe;
            se.oDescription = oDescription.Copy;
            return se;
        }
        public SubWalk CloneMe()
        {
            return CType(Me.Clone, SubWalk);
        }

    }
    Public SubWalks() As SubWalk = {}

    'Friend Length As FromTo

    public New ArrayList arlSteps;

    Friend WalkControls() As EventOrWalkControl = {}

    public string Description { get; set; }
        {
            get
            {
            return sDescription;
        }
set(ByVal String)
            sDescription = value
        }
    }

    'Public Property xNumberOfSteps() As Integer
    '    Get
    '        Return iLength
    '    End Get
    '    Set(ByVal Value As Integer)
    '        iLength = Value
    '    End Set
    'End Property

    public bool Loops { get; set; }
        {
            get
            {
            return bLoop;
        }
set(ByVal Value As Boolean)
            bLoop = Value
        }
    }

    'Public Property FromDesc() As String
    '    Get
    '        Return sFromDesc
    '    End Get
    '    Set(ByVal Value As String)
    '        sFromDesc = Value
    '    End Set
    'End Property


    'Public Property ToDesc() As String
    '    Get
    '        Return sToDesc
    '    End Get
    '    Set(ByVal Value As String)
    '        sToDesc = Value
    '    End Set
    'End Property

    'Public Property ShowMove() As Boolean
    '    Get
    '        Return bShowMove
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        bShowMove = Value
    '    End Set
    'End Property

    public bool StartActive { get; set; }
        {
            get
            {
            return bStartActive;
        }
set(ByVal Boolean)
            bStartActive = value
        }
    }


    public string GetDefaultDescription()
    {

        private New System.Text.StringBuilder sb;

        For Each stp As clsStep In arlSteps
            switch (stp.sLocation)
            {
                case "Hidden":
                    {
                    sb.Append("Hidden");
                    'Case "Player"
                    '    sb.Append("Follow Player")
                default:
                    {
                    'sb.Append(Adventure.htblLocations(stp.sLocation).ShortDescription)
                    private string sDescription = Adventure.GetNameFromKey(stp.sLocation);
                    If Adventure.htblCharacters.ContainsKey(stp.sLocation) Then sDescription = "Follow " + sDescription
                    if (sDescription <> "")
                    {
                        sb.Append(sDescription);
                    Else
                        sb.Append("<" + stp.sLocation + ">") ' Groups may not be defined yet;
                    }
            }
            if (stp.ftTurns.iFrom = stp.ftTurns.iTo)
            {
                sb.Append(" [" + stp.ftTurns.iFrom + "]");
            Else
                sb.Append(" [" + stp.ftTurns.iFrom + " - " + stp.ftTurns.iTo + "]");
            }

            If stp != arlSteps(arlSteps.Count - 1) Then sb.Append(" -> ")
        Next;
        If bLoop Then sb.Append(" {L}")

        If sb.ToString = "" Then sb.Append("Unnamed walk")

        return sb.ToString;

    }



#if Runner

    private void ResetLength()
    {
        For Each [step] As clsWalk.clsStep In Me.arlSteps
            [step].ftTurns.Reset();
        Next;
    }
    private int Length { get; }
        {
            get
            {
            private int iLength = 0;
            For Each [step] As clsWalk.clsStep In Me.arlSteps
                iLength += [step].ftTurns.Value;
            Next;
            return iLength;
        }
    }


    public int TimerToEndOfWalk { get; set; }
        {
            get
            {
            return iTimerToEndOfWalk;
        }
set(ByVal Integer)
            private int iStartValue = iTimerToEndOfWalk;
            iTimerToEndOfWalk = value

            ' If we've reached the end of the timer
            if (Status = StatusEnum.Running && iTimerToEndOfWalk = 0 Then ' iTimerToEndOfWalk < 1 && iStartValue > iTimerToEndOfWalk)
            {
                lStop(true, true);
            }

        }
    }

    private SubWalk LastSubWalk;
    private int TimerFromLastSubWalk { get; }
        {
            get
            {
            return TimerFromStartOfWalk - iLastSubWalkTime;
        }
    }
    private int TimerFromStartOfWalk { get; }
        {
            get
            {
            return Length - TimerToEndOfWalk '+ 1;
        }
    }


    public bool bJustStarted = false;
    public void Start(bool bForce = false)
    {
        if (bForce)
        {
            lStart();
        Else
            if (NextCommand = Command.Stop)
            {
                NextCommand = Command.Restart
            Else
                NextCommand = Command.Start
            }
        }
    }
    public void lStart(bool bRestart = false)
    {
        if (Status = StatusEnum.NotYetStarted || Status = StatusEnum.Finished || (Status = StatusEnum.Running && bRestart))
        {
            If ! bRestart Then UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Low, "Starting walk " + Description)
            Status = StatusEnum.Running
            ResetLength();
            TimerToEndOfWalk = Length ' + 1

            if (TimerFromStartOfWalk = 0)
            {
                DoAnySteps() '
                DoAnySubWalks() ' To run 'after 0 turns' subevents
            }

            bJustStarted = True
        Else
            'ErrMsg("Can't Start an Walk that isn't waiting!")
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Error, "Can't Start a Walk that isn't waiting!");
        }
    }


    public void Pause()
    {
        NextCommand = Command.Pause
    }
    public void lPause()
    {
        if (Status = StatusEnum.Running)
        {
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Low, "Pausing walk " + Description);
            Status = StatusEnum.Paused
        Else
            'Throw New Exception("Can't Pause a Walk that isn't running!")
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Error, "Can't Pause a Walk that isn't running!");
        }
    }


    public string sTriggeringTask;


    Public Sub [Resume]()
        NextCommand = Command.Resume
    }
    public void lResume()
    {
        if (Status = StatusEnum.Paused)
        {
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Low, "Resuming walk " + Description);
            Status = StatusEnum.Running
        Else
            'Throw New Exception("Can't Resume a Walk that isn't paused!")
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Error, "Can't Resume a Walk that isn't paused!");
        }
    }


    Public Sub [Stop]()
        NextCommand = Command.Stop
    }
    public void lStop(bool bRunSubEvents = false, bool bReachedEnd = false)
    {

        If bRunSubEvents Then DoAnySubWalks()
        Status = StatusEnum.Finished
        if (Me.bLoop && TimerToEndOfWalk = 0 && bReachedEnd)
        {
            If Length > 0 Then ' Only restart if walk comes to and end and it is set to loop - not if it is terminated by task change
                UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Low, "Restarting walk " + Description);
                lStart(true);
            Else
                UserSession.DebugPrint(ItemEnum.Event, sKey, DebugDetailLevelEnum.Low, "! restarting walk " + Description + " otherwise we'd get in an infinite loop as zero length.");
            }
        Else
            UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Low, "Finishing walk " + Description);
        }

    }


    public void IncrementTimer()
    {

        'Dim bJustStarted As Boolean = False

        if (NextCommand <> Command.null)
        {
            switch (NextCommand)
            {
                case Command.Start:
                    {
                    lStart();
                    'bJustStarted = True
                case Command.Stop:
                    {
                    lStop();
                case Command.Pause:
                    {
                    lPause();
                case Command.Resume:
                    {
                    lResume();
                case Command.Restart:
                    {
                    lStart(true);
            }
            NextCommand = Command.Nothing
            sTriggeringTask = ""
        }

        If Status = StatusEnum.Running Then UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.High, "Walk " + Description + " [" + TimerFromStartOfWalk + 1 + "/" + Length + "]")

        'If TimerToEndOfWalk > 0 AndAlso (TimerFromStartOfWalk > 0 OrElse (TimerFromStartOfWalk = 0 AndAlso Not bJustStarted)) Then
        '    DoAnySteps()
        '    DoAnySubWalks()
        'End If

        ' Split this into 2 case statements, as changing timer here may change status
        switch (Status)
        {
            case StatusEnum.NotYetStarted:
                {
            case StatusEnum.Running:
                {
                If ! bJustStarted Then TimerToEndOfWalk -= 1
            case StatusEnum.Paused:
                {
            case StatusEnum.Finished:
                {
        }

        if (Not bJustStarted)
        {
            DoAnySteps()
            DoAnySubWalks()
        }

        bJustStarted = False

    }



    public void DoAnySteps()
    {

        if (Status = StatusEnum.Running)
        {
            private int iStepLength = 0;
            For Each [step] As clsStep In arlSteps
                if (iStepLength = TimerFromStartOfWalk)
                {
                    private string sDestination = "";
                    private string sDestKey = [step].sLocation;
                    If sDestKey = THEPLAYER Then sDestKey = Adventure.Player.Key
                    if (Adventure.htblGroups.ContainsKey(sDestKey))
                    {
                        ' Get an adjacent location in the group
                        private clsGroup grp = Adventure.htblGroups(sDestKey);
                        private clsCharacter ch = Adventure.htblCharacters(sKey);
                        private clsLocation locCurrent = null;
                        If ch.Location.ExistWhere <> clsCharacterLocation.ExistsWhereEnum.Hidden Then locCurrent = Adventure.htblLocations(ch.Location.LocationKey)
                        private bool bHasAdjacent = false;
                        if (locCurrent IsNot null)
                        {
                            For Each sLoc As String In grp.arlMembers
                                if (locCurrent.IsAdjacent(sLoc))
                                {
                                    bHasAdjacent = True
                                    Exit For;
                                }
                            Next;
                        }
                        if (bHasAdjacent)
                        {
                            while (sDestination = "")
                            {
                                private string sPossibleDest = grp.arlMembers(Random(grp.arlMembers.Count - 1)) 'CInt(Rnd() * (grp.arlMembers.Count - 1)));
                                If ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden || locCurrent.IsAdjacent(sPossibleDest) Then sDestination = sPossibleDest
                            }
                        Else
                            ' No adjacent room, so just move to a random room in the group
                            sDestination = grp.arlMembers(Random(grp.arlMembers.Count - 1))
                        }
                        UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Medium, "Character " + Adventure.htblCharacters(sKey).ProperName + " walks to " + Adventure.GetNameFromKey(sDestKey) + " (" + Adventure.htblLocations(sDestination).ShortDescription.ToString + ")");
                    ElseIf Adventure.htblCharacters.ContainsKey(sDestKey) Then
                        ' Only move towards the character if they are in an adjacent room
                        private clsCharacter ch = Adventure.htblCharacters(sKey);
                        private clsCharacter ch2 = Adventure.htblCharacters(sDestKey);

                        if (ch.Location.LocationKey <> ch2.Location.LocationKey)
                        {
                            private clsLocation locCurrent = null;
                            If ch.Location.ExistWhere <> clsCharacterLocation.ExistsWhereEnum.Hidden Then locCurrent = Adventure.htblLocations(ch.Location.LocationKey)
                            if (locCurrent IsNot null && locCurrent.IsAdjacent(ch2.Location.Key))
                            {
                                sDestination = ch2.Location.Key
                                UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Medium, "Character " + Adventure.htblCharacters(sKey).ProperName + " walks to " + ch2.ProperName + " (" + Adventure.htblLocations(sDestination).ShortDescription.ToString + ")");
                            Else
                                ' Character is not adjacent, so don't move
                                UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Error, "Character " + Adventure.htblCharacters(sKey).ProperName + " can't walk to " + ch2.ProperName + " as " + ch2.Gender.ToString.Replace("Male", "he").Replace("Female", "she").Replace("Unknown", "it") + " is not in an adjacent location.");
                                sDestination = ""
                            }
                        }
                    Else
                        sDestination = sDestKey
                        UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Medium, "Character " + Adventure.htblCharacters(sKey).ProperName + " walks to " + Adventure.GetNameFromKey(sDestination));
                    }

                    if (sDestination = HIDDEN || Adventure.htblLocations.ContainsKey(sDestination))
                    {
                        With Adventure.htblCharacters(sKey);
                            if (.HasProperty("ShowEnterExit") && .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
                            {
                                if (.Location.Key = Adventure.Player.Location.LocationKey)
                                {
                                    private string sExits = "exits";
                                    If .HasProperty("CharExits") Then sExits = .GetPropertyValue("CharExits") '.StringData.ToString
                                    private string sLeaves = ToProper(.Name, false) + " " + sExits ' to the North... etc;
                                    if (Adventure.htblLocations(Adventure.Player.Location.LocationKey).IsAdjacent(sDestination))
                                    {
                                        private string sTheDirection = Adventure.htblLocations(.Location.LocationKey).DirectionTo(sDestination);
                                        if (sTheDirection <> "nowhere")
                                        {
                                            switch (sTheDirection)
                                            {
                                                case "outside":
                                                case "inside":
                                                    {
                                                    sLeaves &= " ";
                                                default:
                                                    {
                                                    sLeaves &= " to ";
                                            }
                                            'If sTheDirection <> "outside" Then sLeaves &= " to "
                                            sLeaves &= sTheDirection;
                                        }
                                    }
                                    sLeaves &= ".";
                                    UserSession.Display(sLeaves);
                                ElseIf sDestination = Adventure.Player.Location.LocationKey Then
                                    private string sEnters = "enters";
                                    If .HasProperty("CharEnters") Then sEnters = .GetPropertyValue("CharEnters") '.StringData.ToString
                                    private string sArrives = ToProper(.Name, false) + " " + sEnters ' from the North... etc;
                                    if (Adventure.htblLocations(Adventure.Player.Location.LocationKey).IsAdjacent(.Location.Key))
                                    {
                                        private string sTheDirection = Adventure.htblLocations(sDestination).DirectionTo(.Location.LocationKey);
                                        If sTheDirection <> "nowhere" Then sArrives &= " from " + sTheDirection
                                    }
                                    sArrives &= ".";
                                    UserSession.Display(sArrives);
                                }
                            }

                            .Move(sDestination);
                        }
                    }
                }
                iStepLength += [step].ftTurns.Value;
                'If iStepLength > Length - TimerFromStartOfEvent Then Exit Sub
            Next;
        }

    }




    public void DoAnySubWalks()
    {

        switch (Status)
        {
            case StatusEnum.Running:
                {
                ' Check all the subevents to see if we need to do anything
                private int iIndex = 0;
                For Each sw As SubWalk In SubWalks
                    private bool bRunSubWalk = false;
                    switch (sw.eWhen)
                    {
                        case SubWalk.WhenEnum.FromStartOfWalk:
                            {
                            If TimerFromStartOfWalk = sw.ftTurns.Value && sw.ftTurns.Value <= Length Then bRunSubWalk = true
                        case SubWalk.WhenEnum.FromLastSubWalk:
                            {
                            if (TimerFromLastSubWalk = sw.ftTurns.Value)
                            {
                                If (LastSubWalk == null && iIndex = 0) || (iIndex > 0 && LastSubWalk == SubWalks(iIndex - 1)) Then bRunSubWalk = true
                            }
                        case SubWalk.WhenEnum.BeforeEndOfWalk:
                            {
                            If TimerToEndOfWalk = sw.ftTurns.Value Then bRunSubWalk = true
                        case SubWalk.WhenEnum.ComesAcross:
                            {
                            private bool bPrevSameLocationAsChar = sw.bSameLocationAsChar;
                            sw.bSameLocationAsChar = (Adventure.htblCharacters(sKey).Location.LocationKey = Adventure.Player.Location.LocationKey);

                            if (Not bPrevSameLocationAsChar && sw.bSameLocationAsChar)
                            {
                                bRunSubWalk = True
                            }
                    }

                    if (bRunSubWalk)
                    {
                        switch (sw.eWhat)
                        {
                            case SubWalk.WhatEnum.DisplayMessage:
                                {
                                If sw.sKey3 <> "" && Adventure.Player.IsInGroupOrLocation(sw.sKey3) Then UserSession.Display(sw.oDescription.ToString)
                            case SubWalk.WhatEnum.ExecuteTask:
                                {
                                if (Adventure.htblTasks.ContainsKey(sw.sKey2))
                                {
                                    UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Medium, "Walk '" + Description + "' attempting to execute task '" + Adventure.htblTasks(sw.sKey2).Description + "'");
                                    UserSession.AttemptToExecuteTask(sw.sKey2, true);
                                }
                            case SubWalk.WhatEnum.UnsetTask:
                                {
                                UserSession.DebugPrint(ItemEnum.Character, sKey, DebugDetailLevelEnum.Medium, "Walk '" + Description + "' unsetting task '" + Adventure.htblTasks(sw.sKey2).Description + "'");
                                Adventure.htblTasks(sw.sKey2).Completed = false;
                        }
                        iLastSubWalkTime = TimerFromStartOfWalk
                        LastSubWalk = sw
                    }
                    iIndex += 1;
                Next;
        }

    }

#endif

}



public class clsCharacterLocation
{

    private clsCharacter Parent;
    public void New(clsCharacter Parent)
    {
        If Parent != null && Parent.Location != null Then sLastLocationKey = Parent.Location.sLastLocationKey
        Me.Parent = Parent;
    }

public enum ExistsWhereEnum
    {
        Uninitialised;
        Hidden;
        AtLocation;
        OnObject;
        InObject;
        OnCharacter;
    }

    private ExistsWhereEnum eExistsWhere = ExistsWhereEnum.Uninitialised;
    public ExistsWhereEnum ExistWhere { get; set; }
        {
            get
            {
            if (eExistsWhere = ExistsWhereEnum.Uninitialised)
            {
                if (Parent.HasProperty("CharacterLocation"))
                {
                    switch (Parent.GetPropertyValue("CharacterLocation") ' Parent.htblActualProperties("CharacterLocation").Value)
                    {
                        case "At Location":
                            {
                            eExistsWhere = ExistsWhereEnum.AtLocation
                        case "Hidden":
                            {
                            eExistsWhere = ExistsWhereEnum.Hidden
                        case "In Object":
                            {
                            eExistsWhere = ExistsWhereEnum.InObject
                        case "On Character":
                            {
                            eExistsWhere = ExistsWhereEnum.OnCharacter
                        case "On Object":
                            {
                            eExistsWhere = ExistsWhereEnum.OnObject
                        default:
                            {
                            ' Hmmm....
                    }
                Else
                    eExistsWhere = ExistsWhereEnum.Hidden
                }
            }
            return eExistsWhere;
        }
set(ByVal ExistsWhereEnum)
            if (value <> eExistsWhere)
            {
                private clsProperty p;

                if (Not Parent.HasProperty("CharacterLocation") Then ' Parent.htblActualProperties.ContainsKey("CharacterLocation"))
                {
                    p = Adventure.htblAllProperties("CharacterLocation").Copy
                    p.Selected = true;
                    Parent.AddProperty(p);
                }

                private string sNewLocation = "";
                switch (value)
                {
                    case ExistsWhereEnum.AtLocation:
                        {
                        sNewLocation = "At Location"
                    case ExistsWhereEnum.Hidden:
                        {
                        sNewLocation = "Hidden"
                    case ExistsWhereEnum.InObject:
                        {
                        sNewLocation = "In Object"
                    case ExistsWhereEnum.OnCharacter:
                        {
                        sNewLocation = "On Character"
                    case ExistsWhereEnum.OnObject:
                        {
                        sNewLocation = "On Object"
                }
                Parent.SetPropertyValue("CharacterLocation", sNewLocation) '.Value = sNewLocation;

                For Each sProp As String In New String() {"CharacterAtLocation", "CharInsideWhat", "CharOnWhat", "CharOnWho"}
                    If Parent.HasProperty(sProp) Then Parent.RemoveProperty(sProp)
                Next;

                if (value <> ExistsWhereEnum.Hidden)
                {
                    private string sNewProp = "";
                    switch (value)
                    {
                        case ExistsWhereEnum.AtLocation:
                            {
                            sNewProp = "CharacterAtLocation"
                        case ExistsWhereEnum.InObject:
                            {
                            sNewProp = "CharInsideWhat"
                        case ExistsWhereEnum.OnCharacter:
                            {
                            sNewProp = "CharOnWho"
                        case ExistsWhereEnum.OnObject:
                            {
                            sNewProp = "CharOnWhat"
                    }
                    if (Not Parent.HasProperty(sNewProp) && Adventure.htblAllProperties.ContainsKey(sNewProp))
                    {
                        p = Adventure.htblAllProperties(sNewProp).Copy
                        p.Selected = true;
                        Parent.AddProperty(p);
                    }
                }

                eExistsWhere = value
            }
        }
    }

public enum PositionEnum
    {
        Uninitialised;
        Standing;
        Sitting;
        Lying;
    }
    private PositionEnum ePosition = PositionEnum.Uninitialised;

    private string sKey = null;
    public String ' This could be key of location, object or character Key { get; set; }
        {
            get
            {
            if (sKey Is null)
            {
                switch (ExistWhere)
                {
                    case ExistsWhereEnum.AtLocation:
                        {
                        If Parent.HasProperty("CharacterAtLocation") Then sKey = Parent.GetPropertyValue("CharacterAtLocation") '.Value
                    case ExistsWhereEnum.Hidden:
                        {
                        sKey = ""
                    case ExistsWhereEnum.InObject:
                        {
                        If Parent.HasProperty("CharInsideWhat") Then sKey = Parent.GetPropertyValue("CharInsideWhat") '.Value
                    case ExistsWhereEnum.OnCharacter:
                        {
                        If Parent.HasProperty("CharOnWho") Then sKey = Parent.GetPropertyValue("CharOnWho") '.Value
                        If sKey = THEPLAYER Then sKey = Adventure.Player.Key
                    case ExistsWhereEnum.OnObject:
                        {
                        If Parent.HasProperty("CharOnWhat") Then sKey = Parent.GetPropertyValue("CharOnWhat") '.Value
                }
            }
            return sKey;
        }
set(ByVal Value As String)
            sKey = Value
            switch (ExistWhere)
            {
                case ExistsWhereEnum.AtLocation:
                    {
                    If Parent.HasProperty("CharacterAtLocation") Then Parent.SetPropertyValue("CharacterAtLocation", Value) '.Value = Value
                case ExistsWhereEnum.InObject:
                    {
                    If Parent.HasProperty("CharInsideWhat") Then Parent.SetPropertyValue("CharInsideWhat", Value) '.Value = Value
                case ExistsWhereEnum.OnCharacter:
                    {
                    If Parent.HasProperty("CharOnWho") Then Parent.SetPropertyValue("CharOnWho", Value) '.Value = Value
                case ExistsWhereEnum.OnObject:
                    {
                    If Parent.HasProperty("CharOnWhat") Then Parent.SetPropertyValue("CharOnWhat", Value) '.Value = Value
            }
        }
    }


    internal string sLastLocationKey;
    ' Returns the key of the location that the character is ultimately in
    public string LocationKey { get; }
        {
            get
            {
            LocationKey = ""
            try
            {
                switch (ExistWhere)
                {
                    case ExistsWhereEnum.AtLocation:
                        {
                        return Key;
                    case ExistsWhereEnum.Hidden:
                        {
                        ' Hmm
                        return HIDDEN;
                    case ExistsWhereEnum.InObject:
                    case ExistsWhereEnum.OnObject:
                        {
                        private clsObject ob = null;
                        if (Adventure.htblObjects.TryGetValue(Key, ob))
                        {
                            if (sLastLocationKey <> "" && ob.LocationRoots.ContainsKey(sLastLocationKey))
                            {
                                return sLastLocationKey;
                            Else
                                For Each sLocKey As String In ob.LocationRoots.Keys
                                    return sLocKey;
                                Next;
                            }
                        }
                        return "";
                    case ExistsWhereEnum.OnCharacter:
                        {
                        return Adventure.htblCharacters(Key).Location.LocationKey;
                    default:
                        {
                        return "";
                }
            }
            catch (Exception ex)
            {
                ErrMsg("LocationKey error", ex);
            }
            finally
            {
                If LocationKey <> "" && LocationKey <> sLastLocationKey Then sLastLocationKey = LocationKey
            }

        }
    }


    public void ResetPosition()
    {
        ePosition = PositionEnum.Uninitialised
    }

    public PositionEnum Position { get; set; }
        {
            get
            {
            if (ePosition = PositionEnum.Uninitialised)
            {
                if (Parent.HasProperty("CharacterPosition"))
                {
                    switch (Parent.GetPropertyValue("CharacterPosition"))
                    {
                        case "Standing":
                            {
                            ePosition = PositionEnum.Standing
                        case "Sitting":
                            {
                            ePosition = PositionEnum.Sitting
                        case "Lying":
                            {
                            ePosition = PositionEnum.Lying
                        default:
                            {
                            ' Hmmm....
                    }
                Else
                    ePosition = PositionEnum.Standing
                }
            }
            return ePosition;
        }
set(ByVal PositionEnum)
            if (value <> ePosition)
            {
                Parent.SetPropertyValue("CharacterPosition", value.ToString);
                'Dim p As clsProperty

                'If Adventure.htblAllProperties.ContainsKey("CharacterPosition") Then
                '    If Not Parent.HasProperty("CharacterPosition") Then
                '        p = Adventure.htblAllProperties("CharacterPosition").Copy
                '        Parent.htblProperties.Add(p)
                '    End If
                '    Parent.htblProperties("CharacterPosition").Value = value.ToString
                'End If

                ePosition = value
            }
        }

    }


    Public Shadows ReadOnly Property ToString() As String
        {
            get
            {
            If ExistWhere = ExistsWhereEnum.Hidden Then Return "Hidden"

            private string sWhere = "";

            switch (Position)
            {
                case PositionEnum.Standing:
                    {
                    sWhere = "Standing "
                case PositionEnum.Sitting:
                    {
                    sWhere = "Sitting "
                case PositionEnum.Lying:
                    {
                    sWhere = "Lying "
            }

            switch (ExistWhere)
            {
                case ExistsWhereEnum.AtLocation:
                    {
                    sWhere = "at " & Adventure.htblLocations(Key).ShortDescription.ToString
                case ExistsWhereEnum.OnObject:
                    {
                    sWhere &= "on " + Adventure.htblObjects(Key).FullName;
                case ExistsWhereEnum.InObject:
                    {
                    sWhere &= "in " + Adventure.htblObjects(Key).FullName;
                case ExistsWhereEnum.OnCharacter:
                    {
                    sWhere &= "on " + Adventure.htblCharacters(Key).Name(, false);
            }

            return sWhere;
        }
    }

}

}