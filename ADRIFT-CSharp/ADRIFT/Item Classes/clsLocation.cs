using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsDirection
{
    public string LocationKey;
    internal New RestrictionArrayList Restrictions;
    internal bool bEverBeenBlocked = false;
}



public class clsLocation
{
    Inherits clsItemWithProperties;

    private Description oShortDesc;
    private Description oLongDesc;
    'Private sKey As String

    internal New DirectionArrayList arlDirections;
#if Not www
    public MapNode MapNode;
#endif

    'Public Structure DirectionStruct
    '    Public LocationKey As String
    '    Friend Restrictions As RestrictionArrayList
    'End Structure


    ' For displaying in Listboxes
    public override string ToString()
    {
        return Me.ShortDescription.ToString;
    }


    Protected Overrides ReadOnly Property PropertyGroupType() As clsGroup.GroupTypeEnum
        {
            get
            {
            return clsGroup.GroupTypeEnum.Locations;
        }
    }


    private bool _HideOnMap;
    public bool HideOnMap
        {
            get
            {
            return _HideOnMap;
        }
set(Boolean)
            _HideOnMap = value
        }
    }

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

    Public Property SeenBy(ByVal sCharKey As String) As Boolean
        {
            get
            {
            return Adventure.htblCharacters(sCharKey).HasSeenLocation(Key);
        }
set(ByVal Value As Boolean)
            Adventure.htblCharacters(sCharKey).HasSeenLocation(Key) = Value;
        }
    }


    Friend ReadOnly Property ShortDescriptionSafe As String
        {
            get
            {
            if (Adventure.dVersion < 5)
            {
                ' v4 didn't replace ALRs on statusbar and map
                return StripCarats(ReplaceFunctions(ShortDescription.ToString));
            Else
                return StripCarats(ReplaceALRs(ShortDescription.ToString));
            }
        }
    }

    internal Description ShortDescription { get; set; }
        {
            get
            {
#if Runner
            if (HasProperty(SHORTLOCATIONDESCRIPTION))
            {
                private Description descShortDesc = oShortDesc.Copy;
                For Each sd As SingleDescription In GetProperty(SHORTLOCATIONDESCRIPTION).StringData
                    descShortDesc.Add(sd);
                Next;
                return descShortDesc;
#else
            if (false)
            {
                return null;
#endif
            Else
                return oShortDesc;
            }
        }
set(ByVal Value As Description)
            oShortDesc = Value
        }
    }

    internal Description LongDescription { get; set; }
        {
            get
            {
#if Runner
            if (HasProperty(LONGLOCATIONDESCRIPTION))
            {
                private Description descLongDesc = oLongDesc.Copy;
                For Each sd As SingleDescription In GetProperty(LONGLOCATIONDESCRIPTION).StringData
                    descLongDesc.Add(sd);
                Next;
                return descLongDesc;
#else
            if (false)
            {
                return null;
#endif
            Else
                return oLongDesc;
            }
        }
set(ByVal Value As Description)
            oLongDesc = Value
        }
    }


    Public ReadOnly Property sRegularExpressionString(Optional ByVal bMyRE As Boolean = False) As String
        {
            get
            {
            if (bMyRE)
            {
                ' The ADRIFT 'advanced command construction' expression
                private string sRE = "";
                For Each sWord As String In Split(ShortDescription.ToString(True), " ")
                    If sWord <> "" Then sRE &= "{" + sWord.ToLower + "} "
                Next;
                return sRE;
            Else
                ' Real Regular Expressions
                private string sRE = "";
                For Each sWord As String In Split(ShortDescription.ToString(True), " ")
                    If sRE <> "" Then sRE &= "( )?"
                    If sWord <> "" Then sRE &= "(" + sWord.ToLower + ")?"
                Next;
                return sRE;
            }

        }
    }


    public string ViewLocation { get; }
        {
            get
            {
            private string sView = LongDescription.ToString;
#if Runner


            ' Do any specific listed objects
            private ObjectHashTable htblObjects = ObjectsInLocation(WhichObjectsToListEnum.AllSpecialListedObjects, true);
            For Each ob As clsObject In htblObjects.Values
                pSpace(sView);
                sView &= ob.ListDescription;
            Next;

            htblObjects = ObjectsInLocation(WhichObjectsToListEnum.AllGeneralListedObjects, True)
            if (htblObjects.Count > 0)
            {
                if (sView <> "" || Adventure.dVersion < 5)
                {
                    pSpace(sView);
                    If Adventure.dVersion < 5 && sView = "" Then sView = "  "
                    sView &= "Also here is " + htblObjects.List(, , ArticleTypeEnum.Indefinite) + ".";
                Else
                    sView &= "There is " + htblObjects.List(, , ArticleTypeEnum.Indefinite) + " here.";
                }
            }

            For Each e As clsEvent In Adventure.htblEvents.Values
                private string sLookText = e.LookText();
                If sLookText <> "" Then sView = pSpace(sView) + sLookText
            Next;

            private New Dictionary<string, StringArrayList> dCharDesc;
            For Each sKey As String In CharactersVisibleAtLocation.Keys
                private clsCharacter ch = Adventure.htblCharacters(sKey);
                private string sName = ch.Name(, false);
                private string sIsHereDesc;
                ' Default to Char is here unless we have property (which can set value to blank)
                if (ch.HasProperty("CharHereDesc"))
                {
                    sIsHereDesc = ReplaceFunctions(ch.IsHereDesc)
                Else
                    sIsHereDesc = sName & " is here."
                }
                'Dim sIsHereDesc As String = ReplaceFunctions(ch.IsHereDesc)
                'If sIsHereDesc = "" Then sIsHereDesc = sName & " is here."
                if (sIsHereDesc <> "")
                {
                    private string sDescWithoutName = ReplaceIgnoreCase(sIsHereDesc, sName, "##CHARNAME##");
                    If ! dCharDesc.ContainsKey(sDescWithoutName) Then dCharDesc.Add(sDescWithoutName, New StringArrayList)
                    If ! dCharDesc(sDescWithoutName).Contains(sName) Then dCharDesc(sDescWithoutName).Add(sName)
                }
            Next;
            For Each sDesc As String In dCharDesc.Keys
                With dCharDesc(sDesc);
                    pSpace(sView);
                    if (.Count > 1)
                    {
                        sDesc = sDesc.Replace(" is ", " are ")
                    }
                    sView &= sDesc.Replace("##CHARNAME##", .List);
                }
            Next;
            'Dim htblCharacters As New CharacterHashTable
            'Dim htblCharsOwnDesc As New CharacterHashTable
            'For Each sKey As String In CharactersInLocation.Keys
            '    Dim ch As clsCharacter = Adventure.htblCharacters(sKey)
            '    If ch.IsHereDesc <> "" AndAlso ch.IsHereDesc <> "%CharacterName% is here." AndAlso ch.IsHereDesc <> "%CharacterName[" & ch.Key & "]% is here." AndAlso ch.IsHereDesc <> ch.ProperName & " is here." Then
            '        htblCharsOwnDesc.Add(ch, sKey)
            '    Else
            '        htblCharacters.Add(ch, sKey)
            '    End If
            'Next
            'If htblCharacters.Count > 0 Then
            '    pSpace(sView)
            '    Dim sCharList As String = htblCharacters.List
            '    sView &= UCase(sCharList(0)) & sCharList.Substring(1)
            '    If htblCharacters.Count > 1 Then
            '        sView &= " are here."
            '    Else
            '        sView &= " is here."
            '    End If
            'End If
            'For Each ch As clsCharacter In htblCharsOwnDesc.Values
            '    pSpace(sView)
            '    sView &= ch.IsHereDesc
            'Next

            if (Adventure.ShowExits)
            {
                private int iExitCount = 0;
                private string sListExists = Adventure.Player.ListExits(Me.Key, iExitCount) ' ListExits;
                pSpace(sView);
                if (iExitCount > 1)
                {
                    sView &= "Exits are " + sListExists + ".";
                ElseIf iExitCount = 1 Then
                    sView &= "An exit leads " + sListExists + ".";
                }
            }

            If sView = "" Then sView = "null special."

            if (UserSession.bShowShortLocations)
            {
                sView = "<b>" & ShortDescription.ToString & "</b>" & vbCrLf & sView
                If Adventure.dVersion >= 5 Then sView = vbCrLf + sView
            }
#endif
            return sView;
        }
    }
    '    Public Direction(11) As New DirectionStruct


    public void New()
    {

        for (DirectionsEnum dir = DirectionsEnum.North; dir <= DirectionsEnum.NorthWest; dir++)
        {
            private New clsDirection dirstruct;
            arlDirections.Add(dirstruct);
        Next;
        oLongDesc = New Description
        oShortDesc = New Description

    }

internal enum WhichObjectsToListEnum
    {
        AllObjects;
        AllListedObjects;
        AllGeneralListedObjects;
        AllSpecialListedObjects;
    }
    ' Directly means they have to be directly in the room, i.e. not held by a character etc
    Friend ReadOnly Property ObjectsInLocation(Optional ByVal ListWhat As WhichObjectsToListEnum = WhichObjectsToListEnum.AllListedObjects, Optional ByVal bDirectly As Boolean = True) As ObjectHashTable
        {
            get
            {
            private New ObjectHashTable htblObjectsInLocation;

            For Each ob As clsObject In Adventure.htblObjects.Values
                if (ob.ExistsAtLocation(Key, bDirectly))
                {
                    'If ob.Location.Key = Me.Key Then
                    'If ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation OrElse ob.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation Then
                    switch (ListWhat)
                    {
                        case WhichObjectsToListEnum.AllGeneralListedObjects ' Dynamic objects not excluded plus static objects explicitly included:
                        case unless specially listed:
                            {
                            if (((Not ob.IsStatic && Not ob.ExplicitlyExclude) || (ob.IsStatic && ob.ExplicitlyList)))
                            {
                                If ob.ListDescription = "" Then htblObjectsInLocation.Add(ob, ob.Key)
                            }
                        case WhichObjectsToListEnum.AllListedObjects ' All listed objects:
                        case including special listed:
                            {
                            if (((Not ob.IsStatic && Not ob.ExplicitlyExclude) || (ob.IsStatic && ob.ExplicitlyList)))
                            {
                                htblObjectsInLocation.Add(ob, ob.Key);
                            }
                        case WhichObjectsToListEnum.AllObjects ' Any object in the location:
                        case whether indirectly or not:
                            {
                            htblObjectsInLocation.Add(ob, ob.Key);
                        case WhichObjectsToListEnum.AllSpecialListedObjects ' Specially listed objects only (i.e. they have a special listing description):
                            {
                            if (((Not ob.IsStatic && Not ob.ExplicitlyExclude) || (ob.IsStatic && ob.ExplicitlyList)))
                            {
                                If ob.ListDescription <> "" Then htblObjectsInLocation.Add(ob, ob.Key)
                            }
                    }
                    'End If
                }
            Next;

            return htblObjectsInLocation;
        }
    }


    ' Characters directly in the location
    Friend ReadOnly Property CharactersDirectlyInLocation(Optional ByVal bIncludePlayer As Boolean = False) As CharacterHashTable
        {
            get
            {
            private New CharacterHashTable htblCharactersInLocation;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                If ch.Location.Key = Me.Key && ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation && (bIncludePlayer || ch.Key <> Adventure.Player.Key) Then htblCharactersInLocation.Add(ch, ch.Key) ' && ch.Key <> Adventure.Player.Key
            Next;

            return htblCharactersInLocation;

        }
    }


    ' Characters visible in location (can be in open objects, on objects etc)
    Friend ReadOnly Property CharactersVisibleAtLocation(Optional ByVal bIncludePlayer As Boolean = False) As CharacterHashTable
        {
            get
            {
            private New CharacterHashTable htblCharactersInLocation;

#if Runner
            'Dim sLoc As String = Adventure.Player.Location.LocationKey
            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                If ch != Adventure.Player && ch.IsVisibleAtLocation(Me.Key) Then htblCharactersInLocation.Add(ch, ch.Key)
            Next;
#endif

            return htblCharactersInLocation;

        }
    }


    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsLocation);
        }
    }


    public bool IsAdjacent(string sKey)
    {

        For Each dir As clsDirection In Me.arlDirections
            If dir.LocationKey = sKey Then Return true
        Next;
        return false;

    }


    public string DirectionTo(string sKey)
    {

        If sKey = Me.Key Then Return "not moved"

        for (SharedModule.DirectionsEnum drn = DirectionsEnum.North; drn <= DirectionsEnum.NorthWest; drn++)
        {
            if (arlDirections(drn).LocationKey = sKey)
            {
                switch (drn)
                {
                    case DirectionsEnum.North:
                    case DirectionsEnum.East:
                    case DirectionsEnum.South:
                    case DirectionsEnum.West:
                        {
                        return "the " + drn.ToString.ToLower;
                    case DirectionsEnum.Up:
                        {
                        return "above";
                    case DirectionsEnum.Down:
                        {
                        return "below";
                    case DirectionsEnum.In:
                        {
                        return "inside";
                    case DirectionsEnum.Out:
                        {
                        return "outside";
                    case DirectionsEnum.NorthEast:
                        {
                        return "the north-east";
                    case DirectionsEnum.NorthWest:
                        {
                        return "the north-west";
                    case DirectionsEnum.SouthEast:
                        {
                        return "the south-east";
                    case DirectionsEnum.SouthWest:
                        {
                        return "the south-west";
                }
            }
        Next;

        return "nowhere";

    }


    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return ShortDescription.ToString;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            all.Add(Me.ShortDescription);
            all.Add(Me.LongDescription);
            For Each p As clsProperty In htblActualProperties.Values
                all.Add(p.StringData);
            Next;
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        '
        return iReplacements - iCount;
    }



    public override void EditItem()
    {
#if Generator
        private New frmLocation(Me, True) fLocation;
#endif
    }


    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        For Each d As clsDirection In arlDirections
            If d.LocationKey = sKey Then iCount += 1
            iCount += d.Restrictions.ReferencesKey(sKey);
        Next;
        iCount += htblActualProperties.ReferencesKey(sKey);

        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        For Each d As clsDirection In arlDirections
            if (d.LocationKey = sKey)
            {
                d.Restrictions.Clear();
                d.LocationKey = "";
            Else
                If ! d.Restrictions.DeleteKey(sKey) Then Return false
            }
        Next;
        If ! htblActualProperties.DeleteKey(sKey) Then Return false

        return true;

    }


    Friend Overrides ReadOnly Property Parent As String;
        {
            get
            {
            return "" ' Suppose we could return a Location Group, but what if we were member of more than one?;
        }
    }

}

}