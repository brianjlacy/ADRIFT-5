using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsObject
{
    Inherits clsItemWithProperties;

    'Private sKey As String
    private string sPrefix;
    private Description oDescription;
    private clsObjectLocation cLocation;
    private string sArticle;
    'Private htblSeenBy As New BooleanHashTable
    'Private sParent As String

    internal New StringArrayList m_arlNames;
    internal StringArrayList arlNames
        {
            get
            {
            return m_arlNames;
        }
set(StringArrayList)
            m_arlNames = value
        }
    }
    'Friend htblProperties As New PropertyHashTable

    'Friend Function GetPropertiesIncludingGroups() As PropertyHashTable

    '    Dim htblProp As PropertyHashTable = htblProperties.Clone
    '    For Each grp As clsGroup In Adventure.htblGroups.Values
    '        If grp.GroupType = clsGroup.GroupTypeEnum.Objects Then
    '            If grp.arlMembers.Contains(Key) Then
    '                For Each prop As clsProperty In grp.htblProperties.Values
    '                    If Not htblProp.ContainsKey(prop.Key) Then htblProp.Add(prop, prop.Key)
    '                    With htblProp(prop.Key)
    '                        .Value = prop.Value
    '                        .FromGroup = True
    '                    End With
    '                Next
    '            End If
    '        End If
    '    Next
    '    Return htblProp

    'End Function

    private StringArrayList sarlPlurals;
    internal StringArrayList arlPlurals { get; }
        {
            get
            {
            if (sarlPlurals Is null)
            {
                private New StringArrayList arl;

                For Each sNoun As String In arlNames
                    'arl.Add(sNoun & "|" & GuessPluralFromNoun(sNoun)) ' This means we're matching non-plurals as plurals, so "get ball" will be the same as "get balls" and therefore take all balls without disambiguation
                    arl.Add(GuessPluralFromNoun(sNoun));
                Next;
                sarlPlurals = arl
            }

            return sarlPlurals;
        }
    }

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

    internal string GuessPluralFromNoun(string sNoun)
    {

        If sNoun == null Then Return ""

        switch (sNoun)
        {
            case "deer":
            case "fish":
            case "cod":
            case "mackerel":
            case "trout":
            case "moose":
            case "sheep":
            case "swine":
            case "aircraft":
            case "blues":
            case "cannon" ' Identical Singular + Plural nouns:
                {
                return sNoun;
            case "ox" ' Irregular plurals:
                {
                return "oxen";
            case "cow":
                {
                return "kine";
            case "child":
                {
                return "children";
            case "foot" ' Umlaut plurals:
                {
                return "feet";
            case "goose":
                {
                return "geese";
            case "louse":
                {
                return "lice";
            case "mouse":
                {
                return "mice";
            case "tooth":
                {
                return "teeth";
        }

        switch (sNoun.Length)
        {
            case 0:
                {
                return "";
            case 1:
            case 2:
                {
                return sNoun + "s";
            default:
                {
                switch (sNoun.Substring(sNoun.Length - 3, 3))
                {
                    case "man" ' Umlaut plural:
                        {
                        return sNoun.Substring(0, sNoun.Length - 2) + "en";
                    case "ies":
                        {
                        return sNoun;
                }
                switch (sNoun.Substring(sNoun.Length - 2, 2))
                {
                    case "sh":
                    case "ss":
                    case "ch" ' Sibilant sounds:
                        {
                        return sNoun + "es";
                    case "ge":
                    case "se" ' Sibilant sounds:
                    case ending with 'e':
                        {
                        return sNoun + "s";
                    case "ex":
                        {
                        return sNoun.Substring(0, sNoun.Length - 2) + "ices";
                    case "is":
                        {
                        return sNoun.Substring(0, sNoun.Length - 2) + "es";
                        'Case "on"
                        '    Return sNoun.Substring(0, sNoun.Length - 2) & "a"
                    case "um":
                        {
                        return sNoun.Substring(0, sNoun.Length - 2) + "a";
                    case "us":
                        {
                        return sNoun.Substring(0, sNoun.Length - 2) + "i";
                }
                switch (sNoun.Substring(sNoun.Length - 1, 1))
                {
                    case "f":
                        {
                        'Select Case sNoun.Substring(sNoun.Length - 2, 1)
                        'Case "l"
                        switch (sNoun)
                        {
                            case "dwarf":
                            case "hoof":
                            case "roof"  ' the exceptions:
                                {
                                return (sNoun + "s");
                            default:
                                {
                                return sNoun.Substring(0, sNoun.Length - 1) + "ves";
                        }

                        'End Select
                    case "o" ' nouns ending in 'o' preceded by a consonant:
                        {
                        switch (sNoun.Substring(sNoun.Length - 2, 1))
                        {
                            case "b":
                            case "c":
                            case "d":
                            case "f":
                            case "g":
                            case "h":
                            case "j":
                            case "k":
                            case "l":
                            case "m":
                            case "n":
                            case "p":
                            case "q":
                            case "r":
                            case "s":
                            case "t":
                            case "v":
                            case "w":
                            case "x":
                            case "z":
                                {
                                return sNoun + "es";
                        }
                    case "x" ' normally ends in "es":
                        {
                        return sNoun + "es";
                    case "y" ' nouns ending in y preceeded by a consonant usually drop y and add ies:
                        {
                        switch (sNoun.Substring(sNoun.Length - 2, 1))
                        {
                            case "b":
                            case "c":
                            case "d":
                            case "f":
                            case "g":
                            case "h":
                            case "j":
                            case "k":
                            case "l":
                            case "m":
                            case "n":
                            case "p":
                            case "q":
                            case "r":
                            case "s":
                            case "t":
                            case "v":
                            case "w":
                            case "x":
                            case "z":
                                {
                                return sNoun.Substring(0, sNoun.Length - 1) + "ies";
                        }
                }
        }

        if (sNoun.EndsWith("s"))
        {
            return sNoun;
        Else
            return sNoun + "s";
        }

    }


    'Public Class AlternativeDescription
    '    Public sTaskKey As String
    '    Public bTaskState As Boolean
    '    Public sDescription As String
    'End Class
    'Public colAlternativeDescriptions As New Collection





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


    private int iIsStaticCache = Integer.MinValue;
    public bool IsStatic { get; set; }
        {
            get
            {
            if (iIsStaticCache <> Integer.MinValue)
            {
                return CBool(iIsStaticCache);
            Else
                return GetPropertyValue("StaticOrDynamic") <> "Dynamic";
            }
        }
set(Boolean)
            SetPropertyValue("StaticOrDynamic", IIf(value, "Static", "Dynamic").ToString);
            iIsStaticCache = CInt(value)
        }
    }

    'Public Property IsStatic() As Boolean
    '    Get
    '        If htblActualProperties.ContainsKey("StaticOrDynamic") Then
    '            With htblActualProperties("StaticOrDynamic")
    '                If .StringData.ToString = "Static" Then
    '                    Return True
    '                Else
    '                    Return False
    '                End If
    '            End With
    '        Else
    '            Return True
    '        End If
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        With htblActualProperties("StaticOrDynamic")
    '            If Value Then
    '                .StringData = New Description("Static")
    '            Else
    '                .StringData = New Description("Dynamic")
    '            End If
    '        End With
    '    End Set
    'End Property


    public bool HasSurface { get; set; }
        {
            get
            {
            return GetPropertyValue("Surface") IsNot null;
        }
set(Boolean)
            'SetPropertyValue("StaticOrDynamic", IIf(value, "Static", "Dynamic").ToString)
            TODO("Set HasSurface()");
        }
    }
    'Public Property HasSurface() As Boolean
    '    Get
    '        If htblActualProperties.ContainsKey("Surface") Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        Throw New Exception("Not done this yet...")
    '    End Set
    'End Property


    ' TODO - Enhance this, or make it user configuratble
    Public ReadOnly Property IsPlural As Boolean
        {
            get
            {
            return Article = "some";
        }
    }


    public bool IsContainer { get; set; }
        {
            get
            {
            return GetPropertyValue("Container") IsNot null;
        }
set(Boolean)
            'SetPropertyValue("StaticOrDynamic", IIf(value, "Static", "Dynamic").ToString)
            TODO("Set IsContainer()");
        }
    }
    'Public Property IsContainer() As Boolean
    '    Get
    '        If htblActualProperties.ContainsKey("Container") Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        Throw New Exception("Not done this yet...")
    '    End Set
    'End Property

    public bool IsWearable { get; set; }
        {
            get
            {
            return GetPropertyValue("Wearable") IsNot null;
        }
set(Boolean)
            'SetPropertyValue("StaticOrDynamic", IIf(value, "Static", "Dynamic").ToString)
            'TODO("Set IsWearable()")
            SetPropertyValue("Wearable", value);
        }
    }
    'Public Property IsWearable() As Boolean
    '    Get
    '        If htblActualProperties.ContainsKey("Wearable") Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        Throw New Exception("Not done this yet...")
    '    End Set
    'End Property


    public bool ExplicitlyList { get; set; }
        {
            get
            {
            'If HasProperty("ExplicitlyList") Then
            '    Return True
            'Else
            '    Return False
            'End If
            return HasProperty("ExplicitlyList");
        }
set(ByVal Value As Boolean)
            SetPropertyValue("ExplicitlyList", Value);
            'If Value Then
            '    If Not HasProperty("ExplicitlyList") AndAlso Adventure.htblObjectProperties.ContainsKey("ExplicitlyList") Then
            '        Dim p As New clsProperty
            '        p = Adventure.htblObjectProperties("ExplicitlyList").Copy
            '        p.Selected = True
            '        htblProperties.Add(p)
            '    End If
            'Else
            '    If HasProperty("ExplicitlyList") Then htblProperties.Remove("ExplicitlyList")
            'End If
        }
    }


    public string ListDescription { get; set; }
        {
            get
            {
            if (IsStatic)
            {
                return GetPropertyValue("ListDescription");
                'If HasProperty("ListDescription") Then Return htblProperties("ListDescription").Value
            Else
                return GetPropertyValue("ListDescriptionDynamic");
                'If HasProperty("ListDescriptionDynamic") Then Return htblProperties("ListDescriptionDynamic").Value
            }
            return "";
        }
set(ByVal Value As String)
            if (IsStatic)
            {
                SetPropertyValue("ListDescription", Value);
                'htblProperties("ListDescription").Value = Value
            Else
                SetPropertyValue("ListDescriptionDynamic", Value);
                'htblProperties("ListDescriptionDynamic").Value = Value
            }
        }
    }


    public bool ExplicitlyExclude { get; set; }
        {
            get
            {
            'If HasProperty("ExplicitlyExclude") Then
            '    Return True
            'Else
            '    Return False
            'End If
            return HasProperty("ExplicitlyExclude");
        }
set(ByVal Value As Boolean)
            SetPropertyValue("ExplicitlyExclude", Value);
            'If Value Then
            '    If Not HasProperty("ExplicitlyExclude") AndAlso Adventure.htblObjectProperties.ContainsKey("ExplicitlyExclude") Then
            '        Dim p As New clsProperty
            '        p = Adventure.htblObjectProperties("ExplicitlyExclude").Copy
            '        p.Selected = True
            '        htblProperties.Add(p)
            '    End If
            'Else
            '    If HasProperty("ExplicitlyExclude") Then htblProperties.Remove("ExplicitlyExclude")
            'End If
        }
    }



    public bool IsLieable { get; set; }
        {
            get
            {
            return HasProperty("Lieable");
            'If HasProperty("Lieable") Then
            '    Return True
            'Else
            '    Return False
            'End If
        }
set(ByVal Value As Boolean)
            SetPropertyValue("Lieable", Value);
            'If Value Then
            '    If Not HasProperty("Lieable") Then
            '        Dim p As New clsProperty
            '        p = Adventure.htblAllProperties("Lieable").Copy
            '        p.Selected = True
            '        htblProperties.Add(p)
            '    End If
            'Else
            '    If HasProperty("Lieable") Then htblProperties.Remove("Lieable")
            'End If
        }
    }


    public bool IsSittable { get; set; }
        {
            get
            {
            return HasProperty("Sittable");
            'If HasProperty("Sittable") Then
            '    Return True
            'Else
            '    Return False
            'End If
        }
set(ByVal Value As Boolean)
            SetPropertyValue("Sittable", Value);
            'If Value Then
            '    If Not HasProperty("Sittable") Then
            '        Dim p As New clsProperty
            '        p = Adventure.htblAllProperties("Sittable").Copy
            '        p.Selected = True
            '        htblProperties.Add(p)
            '    End If
            'Else
            '    If HasProperty("Sittable") Then
            '        htblProperties.Remove("Sittable")
            '    End If
            'End If
        }
    }


    public bool IsStandable { get; set; }
        {
            get
            {
            return HasProperty("Standable");
            'If HasProperty("Standable") Then
            '    Return True
            'Else
            '    Return False
            'End If
        }
set(ByVal Value As Boolean)
            SetPropertyValue("Standable", Value);
            'If Value Then
            '    If Not HasProperty("Standable") Then
            '        Dim p As New clsProperty
            '        p = Adventure.htblAllProperties("Standable").Copy
            '        p.Selected = True
            '        htblProperties.Add(p)
            '    End If
            'Else
            '    If HasProperty("Standable") Then
            '        htblProperties.Remove("Standable")
            '    End If
            'End If
        }
    }


    ' Returns True if object is directly on parent, or if on/in something that is on it
    Public ReadOnly Property IsOn(ByVal sParentKey As String) As Boolean
        {
            get
            {
            if ((Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject || Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject) && Parent <> "")
            {
                if (Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject && Parent = sParentKey)
                {
                    return true;
                Else
                    private clsObject obParent = Adventure.htblObjects(Parent);
                    return obParent.IsOn(sParentKey);
                }
            Else
                return false;
            }
        }
    }


    ' Returns True if object is directly inside parent, or if on/in something that is inside it
    Public ReadOnly Property IsInside(ByVal sParentKey As String) As Boolean
        {
            get
            {
            if ((Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject || Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject) && Parent <> "")
            {
                if (Me.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject && Parent = sParentKey)
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


    Public ReadOnly Property ExistsAtLocation(ByVal sLocKey As String, Optional ByVal bDirectly As Boolean = False) As Boolean
        {
            get
            {
            if (IsStatic)
            {
                switch (Location.StaticExistWhere)
                {
                    case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                        {
                        return true;
                    case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                        {
                        return sLocKey = HIDDEN 'false;
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblCharacters(Location.Key).Location.Key = sLocKey
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblObjects(Location.Key).ExistsAtLocation(sLocKey)
                    case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                        {
                        return Adventure.htblGroups(Location.Key).arlMembers.Contains(sLocKey);
                    case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                        {
                        return sLocKey = Location.Key;
                }
            Else
                switch (Location.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblCharacters(Location.Key).Location.Key = sLocKey
                    case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                        {
                        return sLocKey = HIDDEN ' false;
                    case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                        {
                        return sLocKey = Location.Key '  Adventure.htblObjects(Location.Key).ExistsAtLocation(sLocKey);
                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblObjects(Location.Key).ExistsAtLocation(sLocKey)
                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblObjects(Location.Key).ExistsAtLocation(sLocKey)
                    case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                        {
                        If bDirectly Then Return false Else Return Adventure.htblCharacters(Location.Key).Location.Key = sLocKey
                }
            }
        }
    }


    public bool IsTransparent
        {
            get
            {
            return false ' TODO;
        }
set(Boolean)
            TODO();
        }
    }



    public bool IsOpen { get; set; }
        {
            get
            {
            'If HasProperty("OpenStatus") Then
            '    With htblProperties("OpenStatus")
            '        If .StringData.ToString = "Open" Then
            '            Return True
            '        Else
            '            Return False
            '        End If
            '    End With
            'Else
            '    Return True
            'End If
            private string s = GetPropertyValue("OpenStatus");
            return s Is null || s = "Open";
        }
set(ByVal Value As Boolean)
            SetPropertyValue("OpenStatus", IIf(Value, "Open", "Closed").ToString);
            'With htblProperties("OpenStatus")
            '    If Value Then
            '        .StringData = New Description("Open")
            '    Else
            '        .StringData = New Description("Closed")
            '    End If
            'End With
        }
    }


    Friend ReadOnly Property FullName(Optional ByVal Article As ArticleTypeEnum = ArticleTypeEnum.Indefinite) As String
        {
            get
            {
            if (arlNames.Count > 0)
            {
                private string sArticle2 = null;
                switch (Article)
                {
                    case ArticleTypeEnum.Definite:
                        {
                        sArticle2 = "the "
                    case ArticleTypeEnum.Indefinite:
                        {
                        sArticle2 = sArticle & " "
                    case ArticleTypeEnum.None:
                        {
                        sArticle2 = ""
                }
                if (sPrefix <> "")
                {
                    return sArticle2 + sPrefix + " " + arlNames(0);
                Else
                    return sArticle2 + arlNames(0);
                }
            Else
                return "Undefined Object";
            }
        }
    }

    internal string Article { get; set; }
        {
            get
            {
            return sArticle;
        }
set(ByVal Value As String)
            sArticle = Value
        }
    }

    internal string Prefix { get; set; }
        {
            get
            {
            return sPrefix;
        }
set(ByVal Value As String)
            sPrefix = Value
        }
    }



    public string DisplayCharacterChildren()
    {

        private string sReturn = "";

        if (ChildrenCharacters(WhereChildrenEnum.OnObject).Count > 0)
        {
            sReturn = pSpace(sReturn)
            if (sReturn = "")
            {
                sReturn &= "%PCase[" + ChildrenCharacters(WhereChildrenEnum.OnObject).List("and") + "]%";
            Else
                sReturn &= ChildrenCharacters(WhereChildrenEnum.OnObject).List("and");
            }
            if (ChildrenCharacters(WhereChildrenEnum.OnObject).Count = 1)
            {
                sReturn &= " [am/are/is] on ";
            Else
                sReturn &= " are on ";
            }
            sReturn &= FullName(ArticleTypeEnum.Definite);
        }
        if (Not Openable || IsOpen Then ' (Openable && IsOpen) || Not Openable)
        {
            if (ChildrenCharacters(WhereChildrenEnum.InsideObject).Count > 0)
            {
                if (ChildrenCharacters(WhereChildrenEnum.OnObject).Count > 0)
                {
                    sReturn &= ", and ";
                    sReturn &= ChildrenCharacters(WhereChildrenEnum.InsideObject).List("and");
                Else
                    sReturn = pSpace(sReturn)
                    if (sReturn = "")
                    {
                        sReturn &= "%PCase[" + ChildrenCharacters(WhereChildrenEnum.InsideObject).List("and") + "]%";
                    Else
                        sReturn &= ChildrenCharacters(WhereChildrenEnum.InsideObject).List("and");
                    }
                }
                if (ChildrenCharacters(WhereChildrenEnum.InsideObject).Count = 1)
                {
                    sReturn &= " [am/are/is] ";
                Else
                    sReturn &= " are ";
                }
                sReturn &= "inside " + FullName(ArticleTypeEnum.Definite);
            }
        }
        If ChildrenCharacters(WhereChildrenEnum.OnObject).Count > 0 || (((Openable && IsOpen) || ! Openable) && ChildrenCharacters(WhereChildrenEnum.InsideObject).Count > 0) Then sReturn &= "."

        return sReturn;

    }



    public string DisplayObjectChildren()
    {

        private string sReturn = "";

        if (Children(WhereChildrenEnum.OnObject).Count > 0)
        {
            'If sReturn <> "" Then sReturn &= "  "
            sReturn = pSpace(sReturn)
            sReturn &= ToProper(Children(WhereChildrenEnum.OnObject).List("and", false, ArticleTypeEnum.Indefinite));
            if (Children(WhereChildrenEnum.OnObject).Count = 1)
            {
                sReturn &= " is on ";
            Else
                sReturn &= " are on ";
            }
            sReturn &= FullName(ArticleTypeEnum.Definite);
        }
        if ((Openable && IsOpen) || Not Openable)
        {
            if (Children(WhereChildrenEnum.InsideObject).Count > 0)
            {
                if (Children(WhereChildrenEnum.OnObject).Count > 0)
                {
                    sReturn &= ", and inside";
                Else
                    sReturn = pSpace(sReturn) ' If sReturn <> "" Then sReturn &= "  "
                    sReturn &= "Inside " + FullName(ArticleTypeEnum.Definite);
                }
                if (Children(WhereChildrenEnum.InsideObject).Count = 1)
                {
                    sReturn &= " is ";
                Else
                    sReturn &= " are ";
                }
                sReturn &= Children(WhereChildrenEnum.InsideObject).List("and", false, ArticleTypeEnum.Indefinite);
            }
        }
        If Children(WhereChildrenEnum.OnObject).Count > 0 || (((Openable && IsOpen) || ! Openable) && Children(WhereChildrenEnum.InsideObject).Count > 0) Then sReturn &= "."

        If sReturn = "" Then sReturn = "null is on or inside " + FullName(ArticleTypeEnum.Definite) + "."
        return sReturn;

    }


    internal Description Description { get; set; }
        {
            get
            {
#if Runner
            if (oDescription.ToString(true) = "")
            {
                oDescription(0).Description = "There is nothing special about " + FullName(ArticleTypeEnum.Definite) + ".";
            }
#endif
            return oDescription;
        }
set(ByVal Value As Description)
            oDescription = Value
        }
    }


    ' Returns the actual rooms an object is in, regardless of actual location
    internal LocationHashTable LocationRoots { get; }
        {
            get
            {
            private New LocationHashTable htblLocRoot;

            if (Me.IsStatic)
            {
                switch (Location.StaticExistWhere)
                {
                    case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                        {
                        return Adventure.htblLocations;
                    case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                        {
                        ' Fall out
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                        {
                        If Adventure.htblCharacters.ContainsKey(Location.Key) Then ' We could be loading...
                            private clsCharacterLocation locChar = Adventure.htblCharacters(Location.Key).Location;
                            If locChar.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation Then htblLocRoot.Add(Adventure.htblLocations(locChar.Key), locChar.Key)
                        }
                        return htblLocRoot;

                    case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                        {
                        return Adventure.htblObjects(Location.Key).LocationRoots;

                    case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                        {
                        For Each sMemberKey As String In Adventure.htblGroups(Location.Key).arlMembers
                            htblLocRoot.Add(Adventure.htblLocations(sMemberKey), sMemberKey);
                        Next;

                    case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                        {
                        htblLocRoot.Add(Adventure.htblLocations(Location.Key), Location.Key);

                }
            Else
                switch (Location.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                        {
                        private clsCharacterLocation locChar = Adventure.htblCharacters(Location.Key).Location;
                        If locChar.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation Then htblLocRoot.Add(Adventure.htblLocations(locChar.Key), locChar.Key)
                        return htblLocRoot;

                    case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                        {
                        ' Fall out

                    case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                        {
                        If Location.Key != null && Location.Key <> "Hidden" Then htblLocRoot.Add(Adventure.htblLocations(Location.Key), Location.Key)

                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                        {
                        return Adventure.htblObjects(Location.Key).LocationRoots;

                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                        {
                        return Adventure.htblObjects(Location.Key).LocationRoots;

                    case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                        {
                        private clsCharacterLocation locChar = Adventure.htblCharacters(Location.Key).Location;
                        If locChar.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation Then htblLocRoot.Add(Adventure.htblLocations(locChar.Key), locChar.Key)
                        return htblLocRoot;

                }
            }

            return htblLocRoot;
        }
    }


    public clsObjectLocation Location { get; set; }
        {
            get
            {
            'If cLocation IsNot Nothing Then Return cLocation -- this doesn't update if we change underlying properties such as OnWhat... :-/
            'If cLocation Is Nothing Then cLocation = New clsObjectLocation
            cLocation = New clsObjectLocation
            '            Dim htbl As PropertyHashTable
            '#If Runner Then
            '            htbl = htblProperties
            '#Else
            '            htbl = htblActualProperties
            '#End If
            if (Not Me.IsStatic && HasProperty("DynamicLocation"))
            {

                switch (GetPropertyValue("DynamicLocation"))
                {
                    case "Held By Character":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter;
                        If HasProperty("HeldByWho") Then cLocation.Key = GetPropertyValue("HeldByWho")
                    case "Hidden":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                    case "In Location":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                        If HasProperty("InLocation") Then cLocation.Key = GetPropertyValue("InLocation")
                    case "Inside Object":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject;
                        If HasProperty("InsideWhat") Then cLocation.Key = GetPropertyValue("InsideWhat")
                    case "On Object":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject;
                        If HasProperty("OnWhat") Then cLocation.Key = GetPropertyValue("OnWhat")
                    case "Worn By Character":
                        {
                        cLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter;
                        If HasProperty("WornByWho") Then cLocation.Key = GetPropertyValue("WornByWho")
                }

            ElseIf Me.IsStatic && HasProperty("StaticLocation") Then

                switch (GetPropertyValue("StaticLocation"))
                {
                    case "Nowhere":
                    case "Hidden":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                    case "Single Location":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
                        If HasProperty("AtLocation") Then cLocation.Key = GetPropertyValue("AtLocation") ' StaticKey
                    case "Location Group":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.LocationGroup;
                        If HasProperty("AtLocationGroup") Then cLocation.Key = GetPropertyValue("AtLocationGroup") ' StaticKey
                    case "Everywhere":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.AllRooms;
                    case "Part of Character":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter;
                        If HasProperty("PartOfWho") Then cLocation.Key = GetPropertyValue("PartOfWho")
                    case "Part of Object":
                        {
                        cLocation.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject;
                        If HasProperty("PartOfWhat") Then cLocation.Key = GetPropertyValue("PartOfWhat")
                }

            }

            return cLocation;

        }
set(ByVal Value As clsObjectLocation)
            cLocation = Value

            '            Dim htbl As PropertyHashTable
            '#If Runner Then
            '            htbl = htblProperties
            '#Else
            '            htbl = htblActualProperties
            '#End If

            if (Not Me.IsStatic && HasProperty("DynamicLocation"))
            {
                switch (cLocation.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                        {
                        SetPropertyValue("DynamicLocation", "Held By Character");
                        'If HasProperty("HeldByWho") Then
                        AddProperty("HeldByWho");
                        SetPropertyValue("HeldByWho", cLocation.Key);
                    case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                        {
                        SetPropertyValue("DynamicLocation", "Hidden");
                    case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                        {
                        SetPropertyValue("DynamicLocation", "In Location");
                        'If HasProperty("InLocation") Then
                        AddProperty("InLocation");
                        SetPropertyValue("InLocation", cLocation.Key);
                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                        {
                        SetPropertyValue("DynamicLocation", "Inside Object");
                        'If HasProperty("InsideWhat") Then
                        AddProperty("InsideWhat");
                        SetPropertyValue("InsideWhat", cLocation.Key);
                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                        {
                        SetPropertyValue("DynamicLocation", "On Object");
                        'If HasProperty("OnWhat") Then
                        AddProperty("OnWhat");
                        SetPropertyValue("OnWhat", cLocation.Key);
                    case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                        {
                        SetPropertyValue("DynamicLocation", "Worn By Character");
                        'If HasProperty("WornByWho") Then
                        AddProperty("WornByWho");
                        SetPropertyValue("WornByWho", cLocation.Key);
                }

            ElseIf Me.IsStatic && HasProperty("StaticLocation") Then

                switch (cLocation.StaticExistWhere)
                {
                    case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                        {
                        SetPropertyValue("StaticLocation", "Everywhere");
                    case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                        {
                        SetPropertyValue("StaticLocation", "Location Group");
                        'If HasProperty("AtLocationGroup") Then
                        AddProperty("AtLocationGroup");
                        SetPropertyValue("AtLocationGroup", cLocation.Key);
                    case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                        {
                        SetPropertyValue("StaticLocation", "Hidden");
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                        {
                        SetPropertyValue("StaticLocation", "Part of Character");
                        'If HasProperty("PartOfWho") Then
                        AddProperty("PartOfWho");
                        SetPropertyValue("PartOfWho", cLocation.Key);
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                        {
                        SetPropertyValue("StaticLocation", "Part of Object");
                        'If HasProperty("PartOfWhat") Then
                        AddProperty("PartOfWhat");
                        SetPropertyValue("PartOfWhat", cLocation.Key);
                    case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                        {
                        SetPropertyValue("StaticLocation", "Single Location");
                        'If HasProperty("AtLocation") Then
                        AddProperty("AtLocation");
                        SetPropertyValue("AtLocation", cLocation.Key);
                }

            }

        }
    }

    public bool Openable { get; }
        {
            get
            {
            'Return HasProperty("Openable")
            return HasProperty("Openable");
        }
    }

    public bool Lockable { get; }
        {
            get
            {
            return HasProperty("Lockable") ' HasProperty("Lockable");
        }
    }

    public bool Readable { get; }
        {
            get
            {
            return HasProperty("Readable") ' HasProperty("Readable");
        }
    }

    public string ReadText { get; set; }
        {
            get
            {
            return GetPropertyValue("ReadText") ' htblProperties("ReadText").Value;
        }
set(ByVal Value As String)
            'htblProperties("ReadText").Value = Value
            SetPropertyValue("ReadText", Value);
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
            if (IsStatic)
            {
                switch (Location.StaticExistWhere)
                {
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                    case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                        {
                        return Location.Key;
                    case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                        {
                        return Location.Key;
                    case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                        {
                        return HIDDEN;
                    case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                        {
                        return ALLROOMS;
                    case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                        {
                        return Location.Key;
                }
            Else
                switch (Location.DynamicExistWhere)
                {
                    case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                    case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                    case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                    case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                        {
                        return Location.Key;
                    case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                        {
                        return Location.Key;
                    case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                        {
                        return HIDDEN;
                }
            }
            return null;
            'Return sParent
        }
        'Set(ByVal Value As String)
        '    sParent = Value
        'End Set
    }


    Public ReadOnly Property sRegularExpressionString(Optional ByVal bMyRE As Boolean = False, Optional ByVal bPlural As Boolean = False, Optional ByVal bPrefixMandatory As Boolean = False) As String
        {
            get
            {
            private StringArrayList arl = CType(IIf(bPlural, arlPlurals, arlNames), StringArrayList);

            if (bMyRE)
            {
                ' The ADRIFT 'advanced command construction' expression
                private string sRE = "{" + sArticle + "/the} ";
                if (sPrefix <> "")
                {
                    For Each sSinglePrefix As String In Split(sPrefix, " ")
                        sRE &= "{" + sSinglePrefix + "} ";
                    Next;
                }
                sRE &= "[";

                For Each sNoun As String In arl
                    sRE &= sNoun + "/";
                Next;
                return Left(sRE, sRE.Length - 1) + "]";
            Else
                ' Real Regular Expressions
                private string sRE = "(" + sArticle + " |the )?";
                For Each sSinglePrefix As String In Split(sPrefix, " ")
                    If sSinglePrefix <> "" Then sRE &= "(" + MakeTextRESafe(sSinglePrefix) + " )?"
                Next;
                sRE &= "(";
                For Each sNoun As String In arl
                    sRE &= MakeTextRESafe(sNoun) + "|";
                Next;
                If arl.Count = 0 Then sRE &= "|" ' Fudge
                return Left(sRE, sRE.Length - 1) + ")";
            }
        }
    }


    'Public Property SeenBy(ByVal sCharKey As String) As Boolean
    '    Get
    '        Return CBool(htblSeenBy(sCharKey))
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If Not htblSeenBy.ContainsKey(sCharKey) Then
    '            htblSeenBy.Add(Value, sCharKey)
    '        Else
    '            htblSeenBy(sCharKey) = Value
    '        End If
    '    End Set
    'End Property

    Public Property SeenBy(ByVal sCharKey As String) As Boolean
        {
            get
            {
            If sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key
            return Adventure.htblCharacters(sCharKey).HasSeenObject(Key);
        }
set(ByVal Value As Boolean)
            Adventure.htblCharacters(sCharKey).HasSeenObject(Key) = Value;
        }
    }


    public bool IsHeldByAnyone { get; }
        {
            get
            {
            switch (Location.DynamicExistWhere)
            {
                case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                    {
                    return true;
                case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                    {
                    return false;
                case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                    {
                    return false;
                case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                    {
                    return Adventure.htblObjects(Location.Key).IsHeldByAnyone;
                case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                    {
                    return Adventure.htblObjects(Location.Key).IsHeldByAnyone;
                case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                    {
                    return false;
            }
        }
    }

    public bool IsWornByAnyone { get; }
        {
            get
            {
            switch (Location.DynamicExistWhere)
            {
                case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                    {
                    return false;
                case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                    {
                    return false;
                case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                    {
                    return false;
                case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                    {
                    return false ' Adventure.htblObjects(Location.Key).IsWornByAnyone;
                case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                    {
                    return false ' Adventure.htblObjects(Location.Key).IsWornByAnyone;
                case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                    {
                    return true;
            }
        }
    }


#if Runner

    ' The maximum container for the visibility of the object
    Public ReadOnly Property BoundVisible As String
        {
            get
            {
            With Me.Location;
                if (Me.IsStatic)
                {
                    switch (.StaticExistWhere)
                    {
                        case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                            {
                            return ALLROOMS;
                        case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                            {
                            return .Key;
                        case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                            {
                            return HIDDEN;
                        case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                            {
                            return Adventure.htblCharacters(.Key).BoundVisible;
                        case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                            {
                            return Adventure.htblObjects(.Key).BoundVisible;
                        case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                            {
                            return .Key;
                    }
                Else
                    switch (.DynamicExistWhere)
                    {
                        case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                            {
                            return Adventure.htblCharacters(.Key).BoundVisible;
                        case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                            {
                            return HIDDEN;
                        case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                            {
                            return .Key;
                        case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                            {
                            With Adventure.htblObjects(.Key);
                                if (Not .Openable || .IsOpen || .IsTransparent)
                                {
                                    return Adventure.htblObjects(.Key).BoundVisible;
                                Else
                                    return .Key;
                                }
                            }
                        case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                            {
                            return Adventure.htblObjects(.Key).BoundVisible;
                        case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                            {
                            return Adventure.htblCharacters(.Key).BoundVisible;
                    }
                }
            }
            return HIDDEN;
        }
    }

    Public ReadOnly Property IsVisibleTo(ByVal sCharKey As String) As Boolean
        {
            get
            {
            If sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private string sBoundVisible = BoundVisible;
            private string sBoundVisibleCh = Adventure.htblCharacters(sCharKey).BoundVisible;

            switch (sBoundVisible)
            {
                case HIDDEN:
                    {
                    return false;
                case ALLROOMS:
                    {
                    return Adventure.htblLocations.ContainsKey(sBoundVisibleCh);
            }


            if (Adventure.htblGroups.ContainsKey(sBoundVisible))
            {
                return Adventure.htblGroups(sBoundVisible).arlMembers.Contains(sBoundVisibleCh);
            Else
                return sBoundVisible = sBoundVisibleCh;
            }
        }
    }

    Public ReadOnly Property IsVisibleAtLocation(ByVal sLocationKey As String) As Boolean
        {
            get
            {
            private string sBoundVisible = BoundVisible;

            switch (sBoundVisible)
            {
                case HIDDEN:
                    {
                    return false;
                case ALLROOMS:
                    {
                    return Adventure.htblLocations.ContainsKey(sLocationKey);
            }

            if (Adventure.htblGroups.ContainsKey(sBoundVisible))
            {
                return Adventure.htblGroups(sBoundVisible).arlMembers.Contains(sLocationKey);
            Else
                return sBoundVisible = sLocationKey;
            }
        }
    }

    'Public ReadOnly Property IsVisibleTo(ByVal sCharKey As String) As Boolean
    '    Get
    '        If sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key
    '        Dim locChar As clsCharacterLocation = Adventure.htblCharacters(sCharKey).Location
    '        Select Case locChar.ExistWhere
    '            Case clsCharacterLocation.ExistsWhereEnum.Hidden
    '                Return False
    '            Case clsCharacterLocation.ExistsWhereEnum.AtLocation
    '                Return IsVisibleAtLocation(locChar.Key)
    '            Case clsCharacterLocation.ExistsWhereEnum.InObject
    '                ' If the object is closed, then we can only see it if it is inside with us
    '                If Adventure.htblObjects(locChar.Key).IsOpen Then
    '                    For Each sLocKey As String In Adventure.htblObjects(locChar.Key).LocationRoots.Keys
    '                        Return IsVisibleAtLocation(sLocKey)
    '                    Next
    '                Else
    '                    ' We can also see objects we are inside
    '                    Return locChar.Key = Me.Key OrElse Adventure.htblObjects(locChar.Key).Children(WhereChildrenEnum.InsideObject, True).ContainsKey(Me.Key)
    '                End If
    '            Case clsCharacterLocation.ExistsWhereEnum.OnCharacter
    '                Return IsVisibleTo(Adventure.htblCharacters(locChar.Key).Key)
    '            Case clsCharacterLocation.ExistsWhereEnum.OnObject
    '                For Each sLocKey As String In Adventure.htblObjects(locChar.Key).LocationRoots.Keys
    '                    Return IsVisibleAtLocation(sLocKey)
    '                Next
    '        End Select
    '    End Get
    'End Property


    '' Returns the key of the location of the object, if visible
    'Public ReadOnly Property IsVisibleAtLocation(ByVal sLocationKey As String) As Boolean
    '    Get
    '        With Me.Location
    '            If Me.IsStatic Then
    '                Select Case .StaticExistWhere
    '                    Case clsObjectLocation.StaticExistsWhereEnum.AllRooms
    '                        Return True
    '                    Case clsObjectLocation.StaticExistsWhereEnum.NoRooms
    '                        Return False
    '                    Case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter
    '                        If .Key <> "" Then Return sLocationKey = Adventure.htblCharacters(.Key).Location.LocationKey ' StaticKey
    '                    Case clsObjectLocation.StaticExistsWhereEnum.PartOfObject
    '                        If .Key <> "" Then Return Adventure.htblObjects(.Key).IsVisibleAtLocation(sLocationKey) 'To(sCharKey) ' StaticKey
    '                    Case clsObjectLocation.StaticExistsWhereEnum.LocationGroup
    '                        If .Key <> "" Then Return Adventure.htblGroups(.Key).arlMembers.Contains(sLocationKey) ' StaticKey
    '                    Case clsObjectLocation.StaticExistsWhereEnum.SingleLocation
    '                        Return sLocationKey = .Key ' StaticKey
    '                End Select
    '            Else
    '                Select Case .DynamicExistWhere
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter
    '                        If .Key <> "" Then Return Adventure.htblCharacters(.Key).IsVisibleAtLocation(sLocationKey)
    '                        'If .Key <> "" Then Return sLocationKey = Adventure.htblCharacters(.Key).Location.Key AndAlso Adventure.htblCharacters(.Key).Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.InLocation
    '                        Return sLocationKey = .Key
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.Hidden
    '                        Return False
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.InObject
    '                        If .Key <> "" AndAlso .Key <> Key Then
    '                            Dim parent As clsObject = Adventure.htblObjects(.Key)
    '                            If parent.IsVisibleAtLocation(sLocationKey) Then ' parent.IsVisibleTo(sCharKey) Then
    '                                If Not parent.Openable OrElse parent.IsOpen Then
    '                                    Return True
    '                                End If
    '                            End If
    '                        End If
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.OnObject
    '                        If .Key <> "" AndAlso .Key <> Key Then Return Adventure.htblObjects(.Key).IsVisibleAtLocation(sLocationKey)
    '                    Case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter
    '                        If .Key <> "" Then Return Adventure.htblCharacters(.Key).IsVisibleAtLocation(sLocationKey)
    '                        'If .Key <> "" Then Return sLocationKey = Adventure.htblCharacters(.Key).Location.Key AndAlso Adventure.htblCharacters(.Key).Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation
    '                End Select
    '            End If
    '        End With

    '        'return adventure.htblCharacters(scharkey).
    '        Return False ' for now
    '    End Get
    'End Property
#endif


    public void Move(clsObjectLocation ToWhere)
    {

        ' If we're moving into or onto an object, make sure we're not going recursive
        if (ToWhere.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject || ToWhere.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject)
        {
            if (ToWhere.Key = Me.Key || Adventure.htblObjects(Key).Children(WhereChildrenEnum.Everything).ContainsKey(ToWhere.Key))
            {
                DisplayError("Can't move object " + Me.FullName + " " + If(ToWhere.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject, "inside", "onto").ToString + " " + Adventure.htblObjects(ToWhere.Key).FullName + " as that would create a recursive location.");
                Exit Sub;
            }
        }

        sPrevParent = Parent
        Me.Location = ToWhere;

        'If Me.IsStatic Then
        '    Dim sNewKey As String = Location.Key ' As this gets wiped when we add the new properties...
        '    Select Case Me.Location.StaticExistWhere
        '        Case clsObjectLocation.StaticExistsWhereEnum.AllRooms, clsObjectLocation.StaticExistsWhereEnum.NoRooms
        '            ' Do nothing
        '        Case clsObjectLocation.StaticExistsWhereEnum.LocationGroup
        '            'If Not HasProperty("AtLocationGroup") Then
        '            '    p = Adventure.htblAllProperties("AtLocationGroup").Copy
        '            '    htblActualProperties.Add(p)
        '            'End If
        '            SetPropertyValue("AtLocationGroup", sNewKey)
        '        Case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter
        '            'If Not HasProperty("PartOfWho") Then
        '            '    p = Adventure.htblAllProperties("PartOfWho").Copy
        '            '    htblActualProperties.Add(p)
        '            'End If
        '            SetPropertyValue("PartOfWho", sNewKey)
        '        Case clsObjectLocation.StaticExistsWhereEnum.PartOfObject
        '            'If Not HasProperty("PartOfWhat") Then
        '            '    p = Adventure.htblAllProperties("PartOfWhat").Copy
        '            '    htblActualProperties.Add(p)
        '            'End If
        '            SetPropertyValue("PartOfWhat", sNewKey)
        '        Case clsObjectLocation.StaticExistsWhereEnum.SingleLocation
        '            'If Not HasProperty("AtLocation") Then
        '            '    p = Adventure.htblAllProperties("AtLocation").Copy
        '            '    htblActualProperties.Add(p)
        '            'End If
        '            SetPropertyValue("AtLocation", sNewKey)
        '    End Select
        'Else

        'End If


#if Runner
        ' Update any 'seen' things
        if (Me.Key IsNot null)
        {
            For Each Loc As clsLocation In LocationRoots.Values
                if (Loc IsNot null && IsVisibleAtLocation(Loc.Key))
                {
                    For Each ch As clsCharacter In Adventure.htblLocations(Loc.Key).CharactersVisibleAtLocation.Values
                        ch.HasSeenObject(Me.Key) = true;
                    Next;
                }
            Next;
        }
#endif

        ' Do any specifics...
        'Select Case ToWhere.DynamicExistWhere
        '    Case clsObjectLocation.DynamicExistsWhereEnum.InLocation
        '    Case clsObjectLocation.DynamicExistsWhereEnum.InObject
        '    Case clsObjectLocation.DynamicExistsWhereEnum.OnObject
        '    Case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter
        '        Me.Location = ToWhere
        '    Case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter

        'End Select
    }


internal enum WhereChildrenEnum
    {
        InsideOrOnObject;
        InsideObject;
        OnObject;
        Everything ' Includes objects that are part of this object;
    }

    Friend ReadOnly Property Children(ByVal WhereChildren As WhereChildrenEnum, Optional ByVal bRecursive As Boolean = False) As ObjectHashTable
        {
            get
            {
            private New ObjectHashTable htblChildren;

            For Each ob As clsObject In Adventure.htblObjects.Values
                private bool bCheckSubObject = false;
                switch (WhereChildren)
                {
                    case WhereChildrenEnum.Everything:
                        {
                        bCheckSubObject = (ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject OrElse ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject OrElse ob.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject)
                    case WhereChildrenEnum.InsideOrOnObject:
                        {
                        bCheckSubObject = (ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject OrElse ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject)
                    case WhereChildrenEnum.InsideObject:
                        {
                        bCheckSubObject = ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject
                    case WhereChildrenEnum.OnObject:
                        {
                        bCheckSubObject = ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject
                }
                '                If (WhereChildren <> WhereChildrenEnum.OnObject AndAlso ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject) OrElse (WhereChildren <> WhereChildrenEnum.InsideObject AndAlso ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject) orelse (WhereChildren=WhereChildrenEnum.Everything andals Then
                if (bCheckSubObject)
                {
                    if (ob.Location.Key = Me.Key)
                    {
                        htblChildren.Add(ob, ob.Key);
                        if (bRecursive)
                        {
                            For Each obChild As clsObject In ob.Children(WhereChildrenEnum.Everything, True).Values
                                htblChildren.Add(obChild, obChild.Key);
                            Next;
                        }
                    }
                }
            Next;
            '#If Runner Then
            if (bRecursive)
            {
                ' Also got to check all characters inside the object to include anything held/worn by them
                For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    if ((WhereChildren <> WhereChildrenEnum.OnObject && ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject) || (WhereChildren <> WhereChildrenEnum.InsideObject && ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject))
                    {
                        if (ch.Location.Key = Me.Key)
                        {
                            For Each obHeldWornByChar As clsObject In ch.ChildrenObjects(WhereChildrenEnum.InsideOrOnObject, True).Values
                                htblChildren.Add(obHeldWornByChar, obHeldWornByChar.Key);
                            Next;
                        }
                    }
                Next;
            }
            '#End If
                return htblChildren;
        }
    }


    Friend ReadOnly Property ChildrenCharacters(ByVal WhereChildren As WhereChildrenEnum) As CharacterHashTable
        {
            get
            {
            private New CharacterHashTable htblChildren;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if ((WhereChildren <> WhereChildrenEnum.OnObject && ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject) || (WhereChildren <> WhereChildrenEnum.InsideObject && ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject))
                {
                    if (ch.Location.Key = Me.Key)
                    {
                        htblChildren.Add(ch, ch.Key);
                    }
                }
            Next;

            return htblChildren;
        }
    }


    public void New()
    {
        oDescription = New Description
    }

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            private clsObject ob = CType(Me.MemberwiseClone, clsObject);
            ob.htblActualProperties = (PropertyHashTable)(ob.htblActualProperties.Clone);
            ob.arlNames = ob.arlNames.Clone;
            return ob;
        }
    }

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return FullName;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            all.Add(Me.Description);
            For Each p As clsProperty In htblActualProperties.Values
                all.Add(p.StringData);
            Next;
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sArticle, sSearchString, sReplace, bFindAll);
        iReplacements += MyBase.FindStringInStringProperty(Me.sPrefix, sSearchString, sReplace, bFindAll);
        for (int i = arlNames.Count - 1; i <= 0; i += -1)
        {
            iReplacements += MyBase.FindStringInStringProperty(Me.arlNames(i), sSearchString, sReplace, bFindAll);
        Next;
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmObject(Me, True) fObject;
#endif
    }

    Protected Overrides ReadOnly Property PropertyGroupType() As clsGroup.GroupTypeEnum
        {
            get
            {
            return clsGroup.GroupTypeEnum.Objects;
        }
    }

    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        'If sKey = Location.Key Then iCount += 1
        iCount += htblActualProperties.ReferencesKey(sKey);

        return iCount;

    }

    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            d.DeleteKey(sKey);
        Next;
        'If Location.Key = sKey Then
        '    Location.Key = ""
        '    Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms
        '    Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden
        '    'Move(
        'End If
        If ! htblActualProperties.DeleteKey(sKey) Then Return false

        ' TODO - Need to do something clever here.  E.g. if we have properties Where=InLocation then Location = theroom, we need to set Where=Hidden and remove Location property

        return true;

    }

}


public class clsObjectLocation
{

    private string _Key;

public enum DynamicExistsWhereEnum
    {
        Hidden;
        InLocation;
        InObject;
        OnObject;
        HeldByCharacter;
        WornByCharacter;
    }
    public DynamicExistsWhereEnum DynamicExistWhere { get; set; }

public enum StaticExistsWhereEnum
    {
        NoRooms;
        SingleLocation;
        LocationGroup;
        AllRooms;
        PartOfCharacter;
        PartOfObject;
    }
    public StaticExistsWhereEnum StaticExistWhere { get; set; }

    public int Number { get; set; }

    public string Key { get; set; }
        {
            get
            {
            If _Key = "%Player%" && Adventure.Player != null Then _Key = Adventure.Player.Key
            return _Key;
        }
set(ByVal Value As String)
            if (_Key IsNot null && Value <> _Key)
            {
                _Key = _Key
            }
            _Key = Value
        }
    }

    public clsObjectLocation Copy()
    {
        private New clsObjectLocation loc;
        loc.Key = _Key;
        loc.DynamicExistWhere = DynamicExistWhere;
        loc.StaticExistWhere = StaticExistWhere;
        loc.Number = Number;
        return loc;
    }

}



}