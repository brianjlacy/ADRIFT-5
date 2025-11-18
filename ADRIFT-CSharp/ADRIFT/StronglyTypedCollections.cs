using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


internal static class StronglyTypedCollections
{

    ' TODO - phase out the individual hashtables in favour of a single dictionary

public class ItemDictionary
    {
        'Inherits Generic.Dictionary(Of String, clsItem)
        private Generic.Dictionary<string, clsItem> AllItems;

        public void New()
        {
            AllItems = New Generic.Dictionary(Of String, clsItem)
        }

        public Generic.Dictionary<string, clsItem> Values { get; }
            {
                get
                {
                return AllItems.Values;
            }
        }

        public Generic.Dictionary<string, clsItem> Keys { get; }
            {
                get
                {
                return AllItems.Keys;
            }
        }

        public bool ContainsKey(string key)
        {
            return AllItems.ContainsKey(key);
        }

        public bool TryGetValue(string key, ref item As clsItem)
        {
            return AllItems.TryGetValue(key, item);
        }

        Default Public Property Item(ByVal key As String) As clsItem;
            {
                get
                {
                private clsItem itm = null;
                If TryGetValue(key, itm) Then Return itm
                return null;
            }
set(ByVal clsItem)
                AllItems(key) = value;
            }
        }

        public void AddBase(clsItem item)
        {
            AllItems.Add(item.Key, item);
        }

        public void RemoveBase(string key)
        {
            AllItems.Remove(key);
        }

        ' This is currently just a view of the existing hashtables

        Shadows Sub Add(ByVal item As clsItem);
            switch (true)
            {
                case TypeOf item Is clsLocation:
                    {
                    'If Not Adventure.htblLocations.ContainsKey(item.Key) Then
                    Adventure.htblLocations.Add((clsLocation)(item), item.Key);
                case TypeOf item Is clsObject:
                    {
                    'If Not Adventure.htblObjects.ContainsKey(item.Key) Then
                    Adventure.htblObjects.Add((clsObject)(item), item.Key);
                case TypeOf item Is clsTask:
                    {
                    'If Not Adventure.htblTasks.ContainsKey(item.Key) Then
                    Adventure.htblTasks.Add((clsTask)(item), item.Key);
                case TypeOf item Is clsEvent:
                    {
                    'If Not Adventure.htblEvents.ContainsKey(item.Key) Then
                    Adventure.htblEvents.Add((clsEvent)(item), item.Key);
                case TypeOf item Is clsCharacter:
                    {
                    'If Not Adventure.htblCharacters.ContainsKey(item.Key) Then
                    Adventure.htblCharacters.Add((clsCharacter)(item), item.Key);
                case TypeOf item Is clsVariable:
                    {
                    'If Not Adventure.htblVariables.ContainsKey(item.Key) Then
                    Adventure.htblVariables.Add((clsVariable)(item), item.Key);
                case TypeOf item Is clsALR:
                    {
                    'If Not Adventure.htblALRs.ContainsKey(item.Key) Then
                    Adventure.htblALRs.Add((clsALR)(item), item.Key);
                case TypeOf item Is clsGroup:
                    {
                    'If Not Adventure.htblGroups.ContainsKey(item.Key) Then
                    Adventure.htblGroups.Add((clsGroup)(item), item.Key);
                case TypeOf item Is clsHint:
                    {
                    'If Not Adventure.htblHints.ContainsKey(item.Key) Then
                    Adventure.htblHints.Add((clsHint)(item), item.Key);
                case TypeOf item Is clsProperty:
                    {
                    'If Not Adventure.htblAllProperties.ContainsKey(item.Key) Then
                    Adventure.htblAllProperties.Add((clsProperty)(item));
                case TypeOf item Is clsUserFunction:
                    {
                    Adventure.htblUDFs.Add((clsUserFunction)(item), item.Key);
#if Generator
                case TypeOf item Is clsFolder:
                    {
                    Adventure.dictFolders.Add(item.Key, (clsFolder)(item));
#endif
                default:
                    {
                    TODO("Add item of type " + item.ToString);
            }
        }


        Shadows Sub Remove(ByVal key As String);
            if (Adventure.htblLocations.ContainsKey(key))
            {
                Adventure.htblLocations.Remove(key);
            ElseIf Adventure.htblObjects.ContainsKey(key) Then
                Adventure.htblObjects.Remove(key);
            ElseIf Adventure.htblTasks.ContainsKey(key) Then
                Adventure.htblTasks.Remove(key);
            ElseIf Adventure.htblEvents.ContainsKey(key) Then
                Adventure.htblEvents.Remove(key);
            ElseIf Adventure.htblCharacters.ContainsKey(key) Then
                Adventure.htblCharacters.Remove(key);
            ElseIf Adventure.htblVariables.ContainsKey(key) Then
                Adventure.htblVariables.Remove(key);
            ElseIf Adventure.htblALRs.ContainsKey(key) Then
                Adventure.htblALRs.Remove(key);
            ElseIf Adventure.htblAllProperties.ContainsKey(key) Then
                Adventure.htblAllProperties.Remove(key);
            ElseIf Adventure.htblHints.ContainsKey(key) Then
                Adventure.htblHints.Remove(key);
            ElseIf Adventure.htblGroups.ContainsKey(key) Then
                Adventure.htblGroups.Remove(key);
            ElseIf Adventure.htblUDFs.ContainsKey(key) Then
                Adventure.htblUDFs.Remove(key);
#if Generator
            ElseIf Adventure.dictFolders.ContainsKey(key) Then
                Adventure.dictFolders.Remove(key);
#endif
            Else
                TODO("Remove item of type " + key);
            }
        }

    }


#if Generator
public class FolderDictionary
    {
        Inherits Generic.Dictionary(Of String, clsFolder);

        Public Shadows Sub Add(ByVal key As String, ByVal folder As clsFolder)
            MyBase.Add(key, folder);
            Adventure.dictAllItems.AddBase(folder);
        }

        Public Shadows Sub Remove(ByVal key As String)
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }
    }
#endif


public class LocationHashTable
    {
        Inherits Dictionary(Of String, clsLocation);

        Shadows Sub Add(ByVal loc As clsLocation, ByVal key As String);
            MyBase.Add(key, loc);
            If Me == Adventure.htblLocations Then Adventure.dictAllItems.AddBase(loc)
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            If Me == Adventure.htblLocations Then Adventure.dictAllItems.RemoveBase(key)
        }

        Default Shadows Property Item(ByVal key As String) As clsLocation;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsLocation)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsLocation)
                MyBase.Item(key) = Value;
            }
        }

        public string List(string sSeparator = "and")
        {
            private int iCount = MyBase.Count;

            List = Nothing

            For Each loc As clsLocation In MyBase.Values
                List &= loc.ShortDescription.ToString(true);
                iCount -= 1;
                If iCount > 1 Then List &= ", "
                If iCount = 1 Then List &= " " + sSeparator + " "
            Next;
            If List = "" Then List = "nowhere"

        }
    }


public class ObjectHashTable
    {
        Inherits Dictionary(Of String, clsObject);

        Shadows Sub Add(ByVal ob As clsObject, ByVal key As String);
            MyBase.Add(key, ob);
            If Me == Adventure.htblObjects Then Adventure.dictAllItems.AddBase(ob)
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            If Me == Adventure.htblObjects Then Adventure.dictAllItems.RemoveBase(key)
        }

        Default Shadows Property Item(ByVal key As String) As clsObject;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsObject)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsObject)
                MyBase.Item(key) = Value;
            }
        }

        public string List(string sSeparator = "and", bool bIncludeSubObjects = false, ArticleTypeEnum Article = ArticleTypeEnum.Definite)
        {
            private int iCount = MyBase.Count;

            List = Nothing

            For Each ob As clsObject In MyBase.Values
                List &= ob.FullName(Article);
                iCount -= 1;
                If iCount > 1 Then List &= ", "
                If iCount = 1 Then List &= " " + sSeparator + " "
            Next;
            If List = "" Then List = "nothing"
            'List &= "."

            if (bIncludeSubObjects)
            {
                For Each ob As clsObject In MyBase.Values
                    'If Not ob.Openable OrElse ob.IsOpen Then
                    '    If ob.ChildrenInside.Count > 0 Then
                    '        List &= ".  Inside " & ob.FullName & " is " & ob.ChildrenInside.list("and", True, False)
                    '    End If
                    'End If
                    'If ob.ChildrenOn.Count > 0 Then List &= ".  On " & ob.FullName & " is " & ob.ChildrenOn.List("and", True, False)

                    if (ob.Children(clsObject.WhereChildrenEnum.OnObject).Count > 0)
                    {
                        If List <> "" Then List &= ".  "
                        List &= ToProper(ob.Children(clsObject.WhereChildrenEnum.OnObject).List("and", false, Article));
                        if (ob.Children(clsObject.WhereChildrenEnum.OnObject).Count = 1)
                        {
                            List &= " is on ";
                        Else
                            List &= " are on ";
                        }
                        List &= ob.FullName(ArticleTypeEnum.Definite);
                    }
                    if ((ob.Openable && ob.IsOpen) || Not ob.Openable)
                    {

                        if (ob.Children(clsObject.WhereChildrenEnum.InsideObject).Count > 0)
                        {
                            if (ob.Children(clsObject.WhereChildrenEnum.OnObject).Count > 0)
                            {
                                List &= ", and inside";
                            Else
                                If List <> "" Then List &= ".  "
                                List &= "Inside " + ob.FullName(ArticleTypeEnum.Definite);
                            }
                            if (ob.Children(clsObject.WhereChildrenEnum.InsideObject).Count = 1)
                            {
                                List &= " is ";
                            Else
                                List &= " are ";
                            }
                            List &= ob.Children(clsObject.WhereChildrenEnum.InsideObject).List("and", false, Article);
                        }

                    }
                    'If ob.ChildrenOn.Count > 0 OrElse (ob.Openable AndAlso ob.IsOpen AndAlso ob.ChildrenInside.Count > 0) Then List &= "."

                Next;
            }
        }

#if Runner
        public ObjectHashTable SeenBy(string sCharKey = "")
        {

            If sCharKey = "" || sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private New ObjectHashTable htbl;

            For Each ob As clsObject In Me.Values
                If ob.SeenBy(sCharKey) Then htbl.Add(ob, ob.Key)
            Next;

            return htbl;

        }

        public ObjectHashTable VisibleTo(string sCharKey = "")
        {

            If sCharKey = "" || sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private New ObjectHashTable htbl;

            For Each ob As clsObject In Me.Values
                If ob.IsVisibleTo(sCharKey) Then htbl.Add(ob, ob.Key)
            Next;

            return htbl;

        }
#endif

    }


public class TaskHashTable
    {
        Inherits Dictionary(Of String, clsTask);

        Shadows Sub Add(ByVal task As clsTask, ByVal key As String);
            if (MyBase.ContainsKey(key))
            {
                key = key
            }
            MyBase.Add(key, task);
            If Me == Adventure.htblTasks Then Adventure.dictAllItems.AddBase(task)
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            If Me == Adventure.htblTasks Then Adventure.dictAllItems.RemoveBase(key)
        }

        Default Shadows Property Item(ByVal key As String) As clsTask;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsTask)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsTask)
                MyBase.Item(key) = Value;
            }
        }
    }


    ' Exposes an array of keys that we can iterate that are ordered in our original way
public class OrderedHashTable
    {
        Inherits Hashtable;

        public New StringArrayList OrderedKeys;


        Shadows Sub Add(ByVal key As String, ByVal value As Object);
            MyBase.Add(key, value);
            OrderedKeys.Add(key);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            OrderedKeys.Remove(key);
        }

        Shadows Sub Clear();
            MyBase.Clear();
            OrderedKeys.Clear();
        }

        public void Insert(string key, object value, int iPosition)
        {
            MyBase.Add(key, value);
            OrderedKeys.Insert(iPosition, key);
        }

        Public Shadows Function Clone() As OrderedHashTable
            private New OrderedHashTable htbl;

            For Each sKey As String In OrderedKeys
                htbl.Add(sKey, Item(sKey));
            Next;

            return htbl;
        }

    }



public class StringHashTable
    {
        Inherits Dictionary(Of String, String);

        'Shadows Sub Add(ByVal key As String, ByVal str As String)
        '    MyBase.Add(key, str)
        'End Sub

        'Shadows Sub Remove(ByVal key As String)
        '    MyBase.Remove(key)
        'End Sub

        Default Shadows Property Item(ByVal key As String) As String;
            {
                get
                {
                'Return CStr(MyBase.Item(key))
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As String)
                MyBase.Item(key) = Value;
            }
        }

        Shadows Function Clone() As StringHashTable;

            ' This probably isn't ideal, but you can't cast an ArrayList to a StringArrayList,
            ' so we need to do the shallow copy ourselves.

            'Dim htblTemp As Hashtable = CType(MyBase.Clone, Hashtable)
            'Dim shtblTemp As New StringHashTable

            'For Each sKey As String In htblTemp.Keys
            '    shtblTemp.Add(sKey, CStr(htblTemp(sKey)))
            'Next

            'Return shtblTemp

            private New StringHashTable htblReturn;

            For Each entry As KeyValuePair(Of String, String) In Me
                htblReturn.Add(entry.Key, entry.Value);
            Next;

            return htblReturn;

        }

    }



public class EventHashTable
    {
        Inherits Dictionary(Of String, clsEvent);

        Shadows Sub Add(ByVal evnt As clsEvent, ByVal key As String);
            MyBase.Add(key, evnt);
            Adventure.dictAllItems.AddBase(evnt);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsEvent;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(key), clsEvent)
            }
set(ByVal Value As clsEvent)
                MyBase.Item(key) = Value;
            }
        }
    }


public class CharacterHashTable
    {
        Inherits Dictionary(Of String, clsCharacter);

        Shadows Sub Add(ByVal chrr As clsCharacter, ByVal key As String);
            MyBase.Add(key, chrr);
            If Me == Adventure.htblCharacters Then Adventure.dictAllItems.AddBase(chrr)
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            If Me == Adventure.htblCharacters Then Adventure.dictAllItems.RemoveBase(key)
        }

        Default Shadows Property Item(ByVal key As String) As clsCharacter;
            {
                get
                {
                if (key = "%Player%")
                {
                    If Adventure.Player == null Then Return null
                    key = Adventure.Player.Key
                }
                return MyBase.Item(key) ' CType(MyBase.Item(key), clsCharacter);
            }
set(ByVal Value As clsCharacter)
                MyBase.Item(key) = Value;
            }
        }

        public string List(string sSeparator = "and")
        {
            private int iCount = MyBase.Count;

            List = Nothing

            For Each ch As clsCharacter In MyBase.Values
                List &= "%CharacterName[" + ch.Key + "]%" ' ch.Name;
                iCount -= 1;
                If iCount > 1 Then List &= ", "
                If iCount = 1 Then List &= " " + sSeparator + " "
            Next;
            If List = "" Then List = "noone"

        }

        public CharacterHashTable SeenBy(string sCharKey = "")
        {

            If sCharKey = "" || sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private New CharacterHashTable htbl;

            For Each ch As clsCharacter In Me.Values
                If ch.SeenBy(sCharKey) Then htbl.Add(ch, ch.Key)
            Next;

            return htbl;

        }

        public CharacterHashTable VisibleTo(string sCharKey = "")
        {

            If sCharKey = "" || sCharKey = "%Player%" Then sCharKey = Adventure.Player.Key

            private New CharacterHashTable htbl;

#if Runner
            For Each ch As clsCharacter In Me.Values
                If ch.CanSeeCharacter(sCharKey) Then htbl.Add(ch, ch.Key)
            Next;
#endif

            return htbl;

        }

    }


public class GroupHashTable
    {
        Inherits Dictionary(Of String, clsGroup);

        Shadows Sub Add(ByVal grp As clsGroup, ByVal key As String);
            MyBase.Add(key, grp);
            Adventure.dictAllItems.AddBase(grp);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsGroup;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsGroup)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsGroup)
                MyBase.Item(key) = Value;
            }
        }
    }


public class ALRHashTable
    {
        Inherits Dictionary(Of String, clsALR);

        Shadows Sub Add(ByVal alr As clsALR, ByVal key As String);
            MyBase.Add(key, alr);
            Adventure.dictAllItems.AddBase(alr);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsALR;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsALR)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsALR)
                MyBase.Item(key) = Value;
            }
        }
    }


public class UDFHashTable
    {
        Inherits Dictionary(Of String, clsUserFunction);

        Shadows Sub Add(ByVal UDF As clsUserFunction, ByVal key As String);
            MyBase.Add(key, UDF);
            Adventure.dictAllItems.AddBase(UDF);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsUserFunction;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsUserFunction)
                MyBase.Item(key) = Value;
            }
        }
    }


public class VariableHashTable
    {
        Inherits Dictionary(Of String, clsVariable);

        Shadows Sub Add(ByVal var As clsVariable, ByVal key As String);
            MyBase.Add(key, var);
            Adventure.dictAllItems.AddBase(var);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsVariable;
            {
                get
                {
                'Return CType(MyBase.Item(key), clsVariable)
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsVariable)
                MyBase.Item(key) = Value;
            }
        }
    }


public class SynonymHashTable
    {
        Inherits Dictionary(Of String, clsSynonym);

        Shadows Sub Add(ByVal syn As clsSynonym);
            MyBase.Add(syn.Key, syn);
            Adventure.dictAllItems.AddBase(syn);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsSynonym;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsSynonym)
                MyBase.Item(key) = Value;
            }
        }
    }



public class RestrictionArrayList
    {
        Inherits List(Of clsRestriction) ' ArrayList;
        Implements ICloneable;

        private string sBracketSequence;

        public string BracketSequence { get; set; }
            {
                get
                {
                return sBracketSequence;
            }
set(ByVal Value As String)
                if (sBracketSequence <> Value)
                {
                    sBracketSequence = Value
                }
            }
        }


        public int ReferencesKey(string sKey)
        {

            private int iCount = 0;
            For Each r As clsRestriction In Me
                If r.ReferencesKey(sKey) Then iCount += 1
            Next;
            return iCount;

        }


        public bool DeleteKey(string sKey)
        {

            for (int i = MyBase.Count - 1; i <= 0; i += -1)
            {
                if (CType(MyBase.Item(i), clsRestriction).ReferencesKey(sKey))
                {
                    RemoveAt(i);
                    if (MyBase.Count = 0)
                    {
                        BracketSequence = ""
                    ElseIf MyBase.Count = 1 Then
                        BracketSequence = "#"
                    Else
                        StripRestriction(i);
                    }
                }
            Next;

            return true;

        }


        private bool StripRestriction(int iRest)
        {

            private int iFound = 0;

            for (int i = 0; i <= BracketSequence.Length - 1; i++)
            {
                If sMid(BracketSequence, i, 1) = "#" Then iFound += 1
                if (iFound = iRest + 1)
                {
                    ' Delete the marker
                    BracketSequence = sLeft(BracketSequence, i) & sRight(BracketSequence, BracketSequence.Length - i - 1)
                    ' Remove any trailing And/Or markers
                    if (BracketSequence.Length >= i && (sMid(BracketSequence, i, 1) = "A" || sMid(BracketSequence, i, 1) = "O"))
                    {
                        BracketSequence = sLeft(BracketSequence, i) & sRight(BracketSequence, BracketSequence.Length - i - 1)
                    }
                    ' Correct any duff brackets
                    Exit For;
                }
            Next;

        }


        public bool IsBracketsValid()
        {

            If sBracketSequence == null Then Return true
            private string sBS = sBracketSequence.Replace("[", "((").Replace("]", "))");
            private string sTemp = null;

            while (sTemp <> sBS)
            {
                sTemp = sBS
                sBS = Replace(sBS, "exprAexpr", "expr")
                sBS = Replace(sBS, "exprOexpr", "expr")
                sBS = Replace(sBS, "#", "expr")
                sBS = Replace(sBS, "(expr)", "expr")
                sBS = Replace(sBS, "[expr]", "expr")
                'Debug.Print(brackstring)
            }

            if (sBS = "expr" || (Me.Count = 0 && sBS = ""))
            {
                return true;
            Else
                return false;
            }

        }

        'Shadows Sub Add(ByVal rest As clsRestriction)
        '    MyBase.Add(rest)
        'End Sub

        'Shadows Sub Remove(ByVal rest As clsRestriction)
        '    MyBase.Remove(rest)
        'End Sub

        Default Shadows Property Item(ByVal idx As Integer) As clsRestriction;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(idx), clsRestriction)
            }
set(ByVal Value As clsRestriction)
                MyBase.Item(idx) = Value;
            }
        }

        public RestrictionArrayList Copy()
        {

            private New RestrictionArrayList ral;

            ral.BracketSequence = sBracketSequence;
            for (int i = 0; i <= MyBase.Count - 1; i++)
            {
                private clsRestriction rest = CType(MyBase.Item(i), clsRestriction).Copy;
                ral.Add(rest);
            Next;

            return ral;

        }

        private Object Implements ICloneable.Clone Clone()
        {
            return Me.MemberwiseClone;
        }
        public RestrictionArrayList CloneMe()
        {
            return CType(Clone(), RestrictionArrayList);
        }
    }



public class ActionArrayList
    {
        Inherits List(Of clsAction) ' ArrayList;

        'Shadows Sub Add(ByVal act As clsAction)
        '    MyBase.Add(act)
        'End Sub

        'Shadows Sub Remove(ByVal act As clsAction)
        '    MyBase.Remove(act)
        'End Sub

        public int ReferencesKey(string sKey)
        {

            private int iCount = 0;
            For Each a As clsAction In Me
                If a.ReferencesKey(sKey) Then iCount += 1
            Next;
            return iCount;

        }

        public bool DeleteKey(string sKey)
        {

            for (int i = MyBase.Count - 1; i <= 0; i += -1)
            {
                if (CType(MyBase.Item(i), clsAction).ReferencesKey(sKey))
                {
                    RemoveAt(i);
                }
            Next;

            return true;

        }

        Default Shadows Property Item(ByVal idx As Integer) As clsAction;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(idx), clsAction)
            }
set(ByVal Value As clsAction)
                MyBase.Item(idx) = Value;
            }
        }

        public ActionArrayList Copy()
        {

            private New ActionArrayList aal;

            for (int i = 0; i <= MyBase.Count - 1; i++)
            {
                private clsAction act = CType(MyBase.Item(i), clsAction).Copy;
                aal.Add(act);
            Next;

            return aal;

        }

    }


public class EventDescriptionArrayList
    {
        Inherits List(Of clsEventDescription) ' ArrayList;

        'Shadows Sub Add(ByVal ed As clsEventDescription)
        '    MyBase.Add(ed)
        'End Sub

        'Shadows Sub Remove(ByVal ed As clsEventDescription)
        '    MyBase.Remove(ed)
        'End Sub

        Default Shadows Property Item(ByVal idx As Integer) As clsEventDescription;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(idx), clsEventDescription)
            }
set(ByVal Value As clsEventDescription)
                MyBase.Item(idx) = Value;
            }
        }
    }


public class PropertyHashTable
    {
        Inherits Dictionary(Of String, clsProperty);

        Shadows Sub Add(ByVal prop As clsProperty);

            MyBase.Add(prop.Key, prop);
            if (Me Is Adventure.htblAllProperties)
            {
                switch (prop.PropertyOf)
                {
                    case clsProperty.PropertyOfEnum.Objects:
                        {
                        Adventure.htblObjectProperties.Add(prop);
                    case clsProperty.PropertyOfEnum.Locations:
                        {
                        Adventure.htblLocationProperties.Add(prop);
                    case clsProperty.PropertyOfEnum.Characters:
                        {
                        Adventure.htblCharacterProperties.Add(prop);
                    case clsProperty.PropertyOfEnum.AnyItem:
                        {
                        Adventure.htblObjectProperties.Add(prop);
                        Adventure.htblLocationProperties.Add(prop);
                        Adventure.htblCharacterProperties.Add(prop);
                }
                Adventure.dictAllItems.AddBase(prop);
            }
        }

        Shadows Sub Remove(ByVal key As String);
            if (Me Is Adventure.htblAllProperties)
            {
                switch (Adventure.htblAllProperties(key).PropertyOf)
                {
                    case clsProperty.PropertyOfEnum.Objects:
                        {
                        Adventure.htblObjectProperties.Remove(key);
                    case clsProperty.PropertyOfEnum.Characters:
                        {
                        Adventure.htblCharacterProperties.Remove(key);
                    case clsProperty.PropertyOfEnum.Locations:
                        {
                        Adventure.htblLocationProperties.Remove(key);
                    case clsProperty.PropertyOfEnum.AnyItem:
                        {
                        Adventure.htblObjectProperties.Remove(key);
                        Adventure.htblCharacterProperties.Remove(key);
                        Adventure.htblLocationProperties.Remove(key);
                }
                Adventure.dictAllItems.RemoveBase(key);
            }
            MyBase.Remove(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsProperty;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(key), clsProperty)
            }
set(ByVal Value As clsProperty)
                MyBase.Item(key) = Value;
            }
        }

        public PropertyHashTable CopySelected(bool bGroup = false)
        {

            private New PropertyHashTable pht;

            For Each prop As clsProperty In MyBase.Values
                If prop.Selected && (bGroup || ! (prop.GroupOnly || prop.FromGroup)) Then pht.Add(prop.Copy)
            Next;

            return pht;

        }

        Shadows Function Clone() As PropertyHashTable;

            ' This probably isn't ideal, but you can't cast an ArrayList to a StringArrayList,
            ' so we need to do the shallow copy ourselves.

            'Dim htblTemp As Hashtable = CType(MyBase.Clone, Hashtable)
            'Dim phtblTemp As New PropertyHashTable

            'For Each key As String In htblTemp.Keys
            '    phtblTemp.Add(CType(CType(htblTemp(key), clsProperty).Clone, clsProperty), key)
            'Next

            'Return phtblTemp

            private New PropertyHashTable htblReturn;

            For Each entry As KeyValuePair(Of String, clsProperty) In Me
                htblReturn.Add((clsProperty)(entry.Value.Clone));
            Next;

            return htblReturn;

        }


        public int ReferencesKey(string sKey)
        {

            private int iCount = 0;

            If MyBase.ContainsKey(sKey) Then iCount += 1
            For Each p As clsProperty In MyBase.Values
                If p.AppendToProperty = sKey Then iCount += 1
                If p.DependentKey = sKey Then iCount += 1
                If p.RestrictProperty = sKey Then iCount += 1
                if (p.Type = clsProperty.PropertyTypeEnum.CharacterKey || p.Type = clsProperty.PropertyTypeEnum.LocationGroupKey || p.Type = clsProperty.PropertyTypeEnum.LocationKey || p.Type = clsProperty.PropertyTypeEnum.ObjectKey)
                {
                    If p.Value = sKey Then iCount += 1
                }
            Next;

            return iCount;

        }


        ''' <summary>
        '''
        ''' </summary>
        ''' <param name="p">Property to reset or remove</param>
        ''' <returns>True, if we remove a property</returns>
        ''' <remarks></remarks>
        private bool ResetOrRemoveProperty(clsProperty p)
        {

            ' If we're not mandatory we can just remove the property
            if (Not p.Mandatory)
            {
                ' Ok, just remove it
                Remove(p.Key);
                return true;
            Else
                ' If we are mandatory, we may be able to reset the value, depending on the property type
                private bool bDelete = false;
                switch (p.Type)
                {
                    case clsProperty.PropertyTypeEnum.CharacterKey:
                    case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    case clsProperty.PropertyTypeEnum.LocationKey:
                    case clsProperty.PropertyTypeEnum.ObjectKey:
                        {
                        ' Key types can't be reset, must be deleted, as they can't be Null on a mandatory property
                        bDelete = True
                    case clsProperty.PropertyTypeEnum.Integer:
                        {
                        p = p
                    case clsProperty.PropertyTypeEnum.Text:
                        {
                        p = p
                    case clsProperty.PropertyTypeEnum.StateList:
                        {
                        ' Ok, we just need to make sure we change the value to something that doesn't produce child property
                        private bool bAssignedValue = false;
                        For Each sValue As String In p.arlStates
                            private bool bSafeValue = true;
                            ' If we're unable to do this, we need to remove, and reset/remove parent
                            For Each pOther As clsProperty In Me.Values
                                if (pOther.DependentKey = p.Key && pOther.DependentValue = sValue)
                                {
                                    bSafeValue = False
                                    Exit For;
                                }
                            Next;
                            if (bSafeValue)
                            {
                                ' Ok to set the value to this
                                p.Value = sValue;
                                bAssignedValue = True
                                Exit For;
                            }
                        Next;
                        If ! bAssignedValue Then bDelete = true
                }

                ' If we can't reset the property, we need to reset or remove our parent
                if (bDelete)
                {
                    if (ContainsKey(p.DependentKey))
                    {
                        private clsProperty pParent = Me.Item(p.DependentKey);
                        ResetOrRemoveProperty(pParent);
                    }
                    Remove(p.Key);
                    return true;
                }
            }

            return false;

        }


        public bool DeleteKey(string sKey)
        {

restart:;
            For Each p As clsProperty In MyBase.Values
                If p.AppendToProperty = sKey Then p.AppendToProperty = ""
                If p.DependentKey = sKey Then p.DependentKey = ""
                If p.RestrictProperty = sKey Then p.RestrictProperty = ""
                if (p.Type = clsProperty.PropertyTypeEnum.CharacterKey || p.Type = clsProperty.PropertyTypeEnum.LocationGroupKey || p.Type = clsProperty.PropertyTypeEnum.LocationKey || p.Type = clsProperty.PropertyTypeEnum.ObjectKey)
                {
                    if (p.Value = sKey)
                    {
                        If ResetOrRemoveProperty(p) Then GoTo restart
                    }
                }
            Next;
            If ContainsKey(sKey) Then Remove(sKey)

            return true;

        }


        ' Ensures any child properties have their Selected properties set
        public void SetSelected()
        {
            For Each prop As clsProperty In MyBase.Values
                prop.Selected = IsPropertySelected(prop);
            Next;
        }

        private bool IsPropertySelected(clsProperty prop)
        {

            if (SafeString(prop.DependentKey) <> "")
            {
                if (MyBase.ContainsKey(prop.DependentKey))
                {
                    if (prop.DependentValue Is null || Item(prop.DependentKey).Value = prop.DependentValue)
                    {
                        return IsPropertySelected(Item(prop.DependentKey));
                    }
                Else
                    WarnOnce($"Property ""{prop.Description}"" is dependent on ""{prop.DependentKey}"" but this property doesn't exist");
                }
            Else
                return prop.Selected;
            }

        }

    }


public class BooleanHashTable
    {
        Inherits Dictionary(Of String, Boolean);

        Shadows Sub Add(ByVal bool As Boolean, ByVal key As String);
            MyBase.Add(key, bool);
        }

        'Shadows Sub Remove(ByVal key As String)
        '    MyBase.Remove(key)
        'End Sub

        Default Shadows Property Item(ByVal key As String) As Boolean;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CBool(MyBase.Item(key))
            }
set(ByVal Value As Boolean)
                MyBase.Item(key) = Value;
            }
        }

    }


public class HintHashTable
    {
        Inherits Dictionary(Of String, clsHint);

        Shadows Sub Add(ByVal hint As clsHint, ByVal key As String);
            MyBase.Add(key, hint);
            Adventure.dictAllItems.AddBase(hint);
        }

        Shadows Sub Remove(ByVal key As String);
            MyBase.Remove(key);
            Adventure.dictAllItems.RemoveBase(key);
        }

        Default Shadows Property Item(ByVal key As String) As clsHint;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(key), clsHint)
            }
set(ByVal Value As clsHint)
                MyBase.Item(key) = Value;
            }
        }
    }


public class TopicHashTable
    {
        Inherits Dictionary(Of String, clsTopic);

        Shadows Sub Add(ByVal topic As clsTopic);
            MyBase.Add(topic.Key, topic);
        }

        'Shadows Sub Remove(ByVal key As String)
        '    MyBase.Remove(key)
        'End Sub

        Default Shadows Property Item(ByVal key As String) As clsTopic;
            {
                get
                {
                try
                {
                    return MyBase.Item(key);
                }
                catch (Exception ex)
                {
                    return null;
                }
                'Return CType(MyBase.Item(key), clsTopic)
            }
set(ByVal Value As clsTopic)
                MyBase.Item(key) = Value;
            }
        }

        Shadows Function Clone() As TopicHashTable;

            '' This probably isn't ideal, but you can't cast an ArrayList to a StringArrayList,
            '' so we need to do the shallow copy ourselves.

            'Dim htblTemp As Hashtable = CType(MyBase.Clone, Hashtable)
            'Dim thtblTemp As New TopicHashTable

            'For Each key As String In htblTemp.Keys
            '    thtblTemp.Add(CType(htblTemp(key), clsTopic).Clone)
            'Next

            'Return thtblTemp

            private New TopicHashTable htblReturn;

            For Each entry As KeyValuePair(Of String, clsTopic) In Me
                htblReturn.Add(entry.Value.Clone);
            Next;

            return htblReturn;

        }

        internal bool DoesTopicHaveChildren(string key)
        {

            For Each sKey As String In Me.Keys
                if (key <> sKey)
                {
                    If Me.Item(sKey).ParentKey = key Then Return true
                }
            Next;
            return false;

        }

    }


public class StringArrayList
    {
        Inherits List(Of String) ' ArrayList;

        'Shadows Sub Add(ByVal strg As String)
        '    MyBase.Add(strg)
        'End Sub

        'Shadows Sub Remove(ByVal strg As String)
        '    MyBase.Remove(strg)
        'End Sub

        Default Shadows Property Item(ByVal idx As Integer) As String;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As String)
                try
                {
                    MyBase.Item(idx) = Value;
                }
                catch (Exception ex)
                {
                    ErrMsg("Error assigning value """ + Value + """ to index " + idx);
                }
            }
        }

        private void Merge(StringArrayList sal)
        {

            For Each s As String In sal
                MyBase.Add(s);
            Next;

        }

        Shadows Function Clone() As StringArrayList;

            '' This probably isn't ideal, but you can't cast an ArrayList to a StringArrayList,
            '' so we need to do the shallow copy ourselves.

            'Dim arlTemp As ArrayList = CType(MyBase.Clone, ArrayList)
            'Dim salTemp As New StringArrayList

            'For i As Integer = 0 To arlTemp.Count - 1
            '    salTemp.Add(CStr(arlTemp(i)))
            'Next i

            'Return salTemp

            private New StringArrayList htblReturn;

            For Each entry As String In Me
                htblReturn.Add(entry);
            Next;

            return htblReturn;

        }


        Shadows Function Contains(ByVal strg As String) As Boolean;
            return MyBase.Contains(strg);
        }


        private string List()
        {

            private int iCount = MyBase.Count;

            List = Nothing

            For Each s As String In Me
                List &= s;
                iCount -= 1;
                If iCount > 1 Then List &= ", "
                If iCount = 1 Then List &= " and "
            Next;
            If List = "" Then List = "nothing"

        }

        'Function ContainsKey(ByVal sKey As String) As Boolean

        '    For i As Integer = 0 To MyBase.Count - 1
        '        If CStr(MyBase.Item(i)) = sKey Then
        '            Return True
        '        End If
        '    Next
        '    Return False

        'End Function

    }


public class DirectionArrayList
    {
        Inherits List(Of clsDirection) ' ArrayList;

        'Shadows Sub Add(ByVal dir As clsDirection)
        '    MyBase.Add(dir)
        'End Sub

        'Shadows Sub Remove(ByVal dir As clsDirection)
        '    MyBase.Remove(dir)
        'End Sub

        Default Shadows Property Item(ByVal idx As DirectionsEnum) As clsDirection;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsDirection)
                MyBase.Item(idx) = Value;
            }
        }

    }



public class WalkArrayList
    {
        Inherits List(Of clsWalk) ' ArrayList;

        'Shadows Sub Add(ByVal walk As clsWalk)
        '    MyBase.Add(walk)
        'End Sub

        'Shadows Sub Remove(ByVal walk As clsWalk)
        '    MyBase.Remove(walk)
        'End Sub

        Default Shadows Property Item(ByVal idx As Integer) As clsWalk;
            {
                get
                {
                try
                {
                    return MyBase.Item(idx);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
set(ByVal Value As clsWalk)
                MyBase.Item(idx) = Value;
            }
        }

    }


}

}