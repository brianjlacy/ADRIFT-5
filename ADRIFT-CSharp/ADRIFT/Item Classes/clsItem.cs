using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public abstract class clsItem
{

    public string Key { get; set; }
    public bool IsLibrary { get; set; }
    public Boolean ' To distinguish library items loaded from the game - we want to prompt to overwrite library items in games, but not library items loaded from a library LoadedFromLibrary { get; set; }
    public object Tag { get; set; }

    Private ReadOnly Property GenerateKey As String
        {
            get
            {
            private string sKey = Me.CommonName;
            try
            {
                If sKey = "New Folder" Then sKey = "Folder"
                If TypeOf Me == clsObject && (clsObject)(Me).arlNames.Count > 0 Then sKey = (clsObject)(Me).arlNames(0)

                private New System.Text.StringBuilder sNewKey;
                private bool bCaps = true;
                private List<string> AllKeys = Adventure.AllKeys;

                If sKey == null Then sKey = ""
                for (int iPos = 0; iPos <= sKey.Length - 1; iPos++)
                {
                    if (Char.IsLetterOrDigit(sKey(iPos)))
                    {
                        sNewKey.Append(If(bCaps, Char.ToUpper(sKey(iPos)), Char.ToLower(sKey(iPos))).ToString);
                        bCaps = False
                    Else
                        bCaps = True
                    }
                Next;

                sKey = sLeft(sNewKey.ToString, 10)
                if (sKey.Length < 2)
                {
                    sKey = GetNewKey(True)
#if Generator
                Else
                    sKey = fGenerator.KeyPrefix.ToLower & sKey
#endif
                }
                If AllKeys.FindAll(Function(s) s.IndexOf(sKey, StringComparison.OrdinalIgnoreCase) >= 0).Count = 0 Then Return sKey ' Case insensitive Keys.Contains

                private int i = 1;
                do
                {
                    If AllKeys.FindAll(Function(s) s.IndexOf(sKey + i, StringComparison.OrdinalIgnoreCase) >= 0).Count = 0 Then Return sKey + i
                    i += 1;
                Loop;
            }
            finally
            {
                If Adventure.listExcludedItems.Contains(sKey) Then Adventure.listExcludedItems.Remove(sKey)
            }
        }
    }


    public string GetNewKey(bool bForceOldStyle = false)
    {

        if (Not bForceOldStyle && SafeBool(CInt(GetSetting("ADRIFT", "Generator", "KeyNames", "-1"))))
        {
            return GenerateKey;
        Else
            private string sType = "";
            switch (true)
            {
                case TypeOf Me Is clsLocation:
                    {
                    sType = "Location"
                case TypeOf Me Is clsObject:
                    {
                    sType = "Object"
                case TypeOf Me Is clsTask:
                    {
                    sType = "Task"
                case TypeOf Me Is clsEvent:
                    {
                    sType = "Event"
                case TypeOf Me Is clsCharacter:
                    {
                    sType = "Character"
                case TypeOf Me Is clsVariable:
                    {
                    sType = "Variable"
                case TypeOf Me Is clsALR:
                    {
                    sType = "ALR"
                case TypeOf Me Is clsGroup:
                    {
                    sType = "Group"
                case TypeOf Me Is clsHint:
                    {
                    sType = "Hint"
                case TypeOf Me Is clsProperty:
                    {
                    sType = "Property"
                case TypeOf Me Is clsSynonym:
                    {
                    sType = "Synonym"
                case TypeOf Me Is clsUserFunction:
                    {
                    sType = "User Function"
#if Generator
                case TypeOf Me Is clsFolder:
                    {
                    sType = "Folder"
#endif
                default:
                    {
                    TODO("Other types");
            }

            if (sType <> "")
            {
                Key = GetNewKey(sType)
                return Key;
            Else
                return null;
            }
        }

    }


    public string GetNewKey(string sPrefix)
    {

        private int iNum = 1;

#if Generator
        sPrefix = fGenerator.KeyPrefix.ToLower & sPrefix.Replace(" ", "")
#endif

        while (Not Adventure.GetTypeFromKey(sPrefix + iNum) Is null)
        {
            iNum += 1;
        }

        private string sKey = sPrefix + iNum;

        If Adventure.listExcludedItems.Contains(sKey) Then Adventure.listExcludedItems.Remove(sKey)

        return sKey;

    }


    private DateTime dtCreated = Date.MinValue;
    internal DateTime Created { get; set; }
        {
            get
            {
            if (dtCreated = Date.MinValue)
            {
                if (dtLastUpdated < Now && dtLastUpdated > Date.MinValue)
                {
                    dtCreated = dtLastUpdated
                Else
                    dtCreated = Now
                }
            }
            return dtCreated;
        }
set(ByVal Date)
            dtCreated = value
        }
    }


    private DateTime dtLastUpdated;
    internal DateTime LastUpdated { get; set; }
        {
            get
            {
            if (dtLastUpdated > Date.MinValue)
            {
                return dtLastUpdated;
            Else
                return Now;
            }
        }
set(ByVal Date)
            dtLastUpdated = value
        }
    }


    Public MustOverride ReadOnly Property CommonName() As String
    Public MustOverride ReadOnly Property Clone() As clsItem
    Friend MustOverride ReadOnly Property AllDescriptions() As Generic.List(Of Description);

    public abstract void EditItem();

    public abstract int ReferencesKey(string sKey);
    public abstract bool DeleteKey(string sKey);
    internal abstract object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0);


    public bool SearchFor(string sSearchString)
    {
        private object oSearch = FindString(sSearchString, , false);
        if (TypeOf oSearch Is SingleDescription)
        {
            return true;
        Else
            return CInt(oSearch) > 0;
        }
        'Return FindString(sSearchString, , False) IsNot Nothing
    }


    public object FindString(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {

        private string sCommonName = Me.CommonName;
        try
        {
            For Each desc As Description In AllDescriptions
                private object o = FindStringInDescription(desc, sSearchString, sReplace, bFindAll, iReplacements);
                If o != null && ! bFindAll Then Return o
            Next;
            return FindStringLocal(sSearchString, sReplace, bFindAll, iReplacements);
#if False
            For Each p As System.Reflection.PropertyInfo In Me.GetType.GetProperties(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                switch (true)
                {
                    case p.PropertyType Is GetType(String):
                        {
                        try
                        {
                            if (p.CanWrite)
                            {
                                if (Not p.Name.ToLower.Contains("key"))
                                {
                                    Dim params() As System.Reflection.ParameterInfo = p.GetIndexParameters()
                                    if (params.Length = 0)
                                    {
                                        private string sValue = SafeString(p.GetValue(Me, null));
                                        if (FindText(sValue, sSearchString))
                                        {
                                            If sReplace != null Then p.SetValue(Me, ReplaceString(sValue, sSearchString, sReplace, iReplacements), null)
                                            If ! bFindAll Then Return sValue
                                        }
                                    }
                                }
                            }
                        Catch
                        }
                    case p.PropertyType Is GetType(StringArrayList):
                        {
                        try
                        {
                            if (p.CanWrite)
                            {
                                Dim params() As System.Reflection.ParameterInfo = p.GetIndexParameters()
                                if (params.Length = 0)
                                {
                                    private StringArrayList arlValue = CType(p.GetValue(Me, null), StringArrayList);
                                    for (int i = arlValue.Count - 1; i <= 0; i += -1)
                                    {
                                        private string sValue = arlValue(i);
                                        if (FindText(sValue, sSearchString))
                                        {
                                            If sReplace != null Then arlValue(i) = ReplaceString(sValue, sSearchString, sReplace, iReplacements)
                                            If ! bFindAll Then Return sValue
                                        }
                                    Next;
                                }
                            }
                        Catch
                        }
                }
            Next;
#endif
        }
        catch (Exception ex)
        {
            ErrMsg("Error finding string " + sSearchString + " in item " + CommonName, ex);
        }
        finally
        {
#if Generator
            If sCommonName <> Me.CommonName Then UpdateListItem(Me.Key, Me.CommonName)
#endif
        }

        return null;

    }


    private bool FindText(string sTextToSearch, string sTextToFind)
    {

        If sTextToSearch == null Then Return false

        private System.Text.RegularExpressions.Regex re;
        private string sWordBound = CStr(IIf(SearchOptions.bFindExactWord, "\b", ""));
        sTextToFind = sTextToFind.Replace("\", "\\").Replace("*", ".*").Replace("?", "\?").Replace("[", "\[").Replace("]", "\]").Replace("(", "\(").Replace(")", "\)").Replace(".", "\.")
        if (SearchOptions.bSearchMatchCase)
        {
            re = New System.Text.RegularExpressions.Regex(sWordBound & sTextToFind & sWordBound)
        Else
            re = New System.Text.RegularExpressions.Regex(sWordBound & sTextToFind & sWordBound, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        }

        return re.IsMatch(sTextToSearch);

    }


    internal int FindStringInStringProperty(ref text As String, string sSearchString, string sReplace = null, bool bFindAll = true)
    {

        if (FindText(text, sSearchString))
        {
            private int iReplacements = 0;
            if (sReplace IsNot null)
            {
                text = ReplaceString(text, sSearchString, sReplace, iReplacements)
            Else
                iReplacements = 1
            }
            return iReplacements;
        }

        return null;

    }


    internal SingleDescription FindStringInDescription(Description d, string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {

        For Each sd As SingleDescription In d
            if (FindText(sd.Description, sSearchString) Then ' sd.Description.Contains(sSearchString))
            {
                If sReplace != null Then sd.Description = ReplaceString(sd.Description, sSearchString, sReplace, iReplacements)
                If ! bFindAll Then Return sd
            }
            For Each r As clsRestriction In sd.Restrictions
                if (r.oMessage IsNot null)
                {
                    private SingleDescription sdr = FindStringInDescription(r.oMessage, sSearchString, sReplace, bFindAll, iReplacements);
                    If sdr != null && ! bFindAll Then Return sdr
                }
            Next;
        Next;
        return null;

    }


    private string ReplaceString(ref sText As String, string sFind, string sReplace, ref iReplacements As Integer)
    {

        'Return re.IsMatch(sTextToSearch)

        'While sText.Contains(sFind)
        '    sText = Replace(sText, sFind, sReplace, , 1)
        '    iReplacements += 1
        'End While
        'Dim iStart As Integer = 0
        'Dim iLast As Integer = -1
        'Dim comp As System.StringComparison = StringComparison.CurrentCultureIgnoreCase
        'If SearchOptions.bSearchMatchCase Then comp = StringComparison.CurrentCulture
        'While iStart <> iLast AndAlso iStart > -1
        '    iLast = iStart
        '    iStart = sText.IndexOf(sFind, iStart, comp)
        '    If iStart > -1 AndAlso iStart <> iLast Then iReplacements += 1
        'End While

        'sText = sText.Replace(sFind, sReplace)
        private System.Text.RegularExpressions.Regex re;
        private string sWordBound = CStr(IIf(SearchOptions.bFindExactWord, "\b", ""));
        sFind = sFind.Replace("\", "\\").Replace("*", ".*").Replace("?", "\?")
        if (SearchOptions.bSearchMatchCase)
        {
            re = New System.Text.RegularExpressions.Regex(sWordBound & sFind & sWordBound)
        Else
            re = New System.Text.RegularExpressions.Regex(sWordBound & sFind & sWordBound, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        }
        iReplacements += re.Matches(sText).Count;
        sText = re.Replace(sText, sReplace)

        return sText;

    }


    public int SearchAndReplace(string sFind, string sReplace)
    {

        private int iReplacements = 0;
        FindString(sFind, sReplace, true, iReplacements);
        return iReplacements;

    }

}


public abstract class clsItemWithProperties
{
    Inherits clsItem;


    ' These are the properties that belong to the item
    private New PropertyHashTable _htblActualProperties;
    internal PropertyHashTable htblActualProperties
        {
            get
            {
            return _htblActualProperties;
        }
set(PropertyHashTable)
            _htblActualProperties = value
            'bCalculatedGroups = False
            _htblProperties = Nothing ' To Ensure any subsequent calls to htblProperties doesn't take stale values
        }
    }

    ' Any properties inherited from the group/class
    private PropertyHashTable _htblInheritedProperties;
    private PropertyHashTable htblInheritedProperties
        {
            get
            {
            if (_htblInheritedProperties Is null)
            {
                _htblInheritedProperties = New PropertyHashTable
                For Each grp As clsGroup In Adventure.htblGroups.Values
                    if (grp.GroupType = PropertyGroupType)
                    {
                        if (grp.arlMembers.Contains(Key))
                        {
                            For Each prop As clsProperty In grp.htblProperties.Values
                                If ! _htblInheritedProperties.ContainsKey(prop.Key) Then _htblInheritedProperties.Add(prop.Copy)
                                With _htblInheritedProperties(prop.Key);
                                    .Value = prop.Value;
                                    .StringData = prop.StringData.Copy;
                                    .FromGroup = true;
                                }
                            Next;
                        }
                    }
                Next;
            }
            return _htblInheritedProperties;
        }
set(PropertyHashTable)
            _htblInheritedProperties = value
            _htblProperties = Nothing
        }
    }

    Protected MustOverride ReadOnly Property PropertyGroupType() As clsGroup.GroupTypeEnum

    Friend MustOverride ReadOnly Property Parent As String;

    ' Set this to False whenever the groups change
    'Friend bCalculatedGroups As Boolean = False


    ' This is a view of Actual + Inherited properties for the item
    ' If both lists have the same property, inherited one should take precendence if it has a value
    ' If it doesn't have a value, item property should take precendence
    '
    ' We can cache a view of the properties.  But if we add/delete any we need to recalc
    '
    private PropertyHashTable _htblProperties;
    Friend ReadOnly Property htblProperties As PropertyHashTable
        {
            get
            {
            if (_htblProperties Is null)
            {
                ' Need a shallow clone of actual properties (so we have unique list of original properties)
                _htblProperties = New PropertyHashTable
                For Each prop As clsProperty In _htblActualProperties.Values
                    _htblProperties.Add(prop);
                Next;

                ' Take all actual properties, then layer on the inherited ones
                For Each prop As clsProperty In htblInheritedProperties.Values
                    if (Not HasProperty(prop.Key))
                    {
                        _htblProperties.Add(prop.Copy);
                    Else
                        ' We have property in both actual and inherited.  Check what we want to do here...
                        With _htblProperties(prop.Key);
                            .Value = prop.Value;
                            .StringData = prop.StringData.Copy;
                            .FromGroup = true;
                        }
                    }
                Next;
            }
            return _htblProperties;
        }
    }


    ' Required if we do an action that changes the properties of the object, such as add/remove from a group
    internal void ResetInherited()
    {
        _htblInheritedProperties = Nothing
        _htblProperties = Nothing
    }

    'Friend Function RecalculateProperties() As PropertyHashTable

    '    Dim htblProp As PropertyHashTable = htblActualProperties.Clone
    '    For Each grp As clsGroup In Adventure.htblGroups.Values
    '        If grp.GroupType = PropertyGroupType Then
    '            If grp.arlMembers.Contains(Key) Then
    '                For Each prop As clsProperty In grp.htblProperties.Values
    '                    If Not htblProp.ContainsKey(prop.Key) Then htblProp.Add(prop.Copy)
    '                    With htblProp(prop.Key)
    '                        .Value = prop.Value
    '                        .StringData = prop.StringData.Copy
    '                        .FromGroup = True
    '                    End With
    '                Next
    '            End If
    '        End If
    '    Next

    '    ' Copy existing values back
    '    If m_htblProperties IsNot Nothing Then
    '        For Each p As clsProperty In m_htblProperties.Values
    '            If htblProp.ContainsKey(p.Key) Then
    '                htblProp(p.Key).Value = p.Value(True)
    '                htblProp(p.Key).StringData = p.StringData.Copy
    '            End If
    '        Next
    '    End If

    '    Return htblProp

    'End Function

    internal clsProperty GetProperty(string sPropertyKey)
    {
        return htblProperties(sPropertyKey);
    }


    internal string GetPropertyValue(string sPropertyKey)
    {
        if (HasProperty(sPropertyKey))
        {
            return htblProperties(sPropertyKey).Value;
        Else
            return null;
        }
        '        Dim lhtblProperties As PropertyHashTable
        '#If Runner Then
        '        lhtblProperties = htblProperties
        '#Else
        '        lhtblProperties = htblActualProperties
        '#End If
        '        If lHasProperty(sPropertyKey) Then
        '            Return lhtblProperties(sPropertyKey).Value
        '        Else
        '            Return Nothing
        '        End If
    }


    internal void SetPropertyValue(string sPropertyKey, Description StringData)
    {
        AddProperty(sPropertyKey);
        htblProperties(sPropertyKey).StringData = StringData;
    }


    internal void SetPropertyValue(string sPropertyKey, string sValue)
    {
        '        Dim lhtblProperties As PropertyHashTable
        '#If Runner Then
        '        If HasProperty(sPropertyKey) Then
        '            lhtblProperties = htblProperties
        '        Else
        '            lhtblProperties = htblActualProperties
        '            bCalculatedGroups = False
        '        End If
        '#Else
        '        lhtblProperties = htblActualProperties
        '#End If
        '        If Not lHasProperty(sPropertyKey) Then
        '            If Adventure.htblAllProperties.ContainsKey(sPropertyKey) Then
        '                Dim p As clsProperty = Adventure.htblAllProperties(sPropertyKey).Copy
        '                lhtblProperties.Add(p)
        '            End If
        '        End If

        '        lhtblProperties(sPropertyKey).Value = sValue
        'AddProperty(sPropertyKey)
        AddProperty(sPropertyKey);
        htblProperties(sPropertyKey).Value = sValue;
    }


    internal void SetPropertyValue(string sPropertyKey, bool bValue)
    {
        '        Dim lhtblProperties As PropertyHashTable
        '#If Runner Then
        '        If HasProperty(sPropertyKey) Then
        '            lhtblProperties = htblProperties
        '        Else
        '            lhtblProperties = htblActualProperties
        '            bCalculatedGroups = False
        '        End If
        '#Else
        '        lhtblProperties = htblActualProperties
        '#End If
        '        If bValue Then
        '            If Not lHasProperty(sPropertyKey) Then
        '                If Adventure.htblAllProperties.ContainsKey(sPropertyKey) Then
        '                    Dim p As clsProperty = Adventure.htblAllProperties(sPropertyKey).Copy
        '                    p.Selected = True
        '                    lhtblProperties.Add(p)
        '                End If
        '            End If
        '        Else
        '            If lHasProperty(sPropertyKey) Then lhtblProperties.Remove(sPropertyKey)
        '        End If

        ' Add/Remove the property from our collection
        if (bValue)
        {
            AddProperty(sPropertyKey);
            htblProperties(sPropertyKey).Selected = true;
        Else
            RemoveProperty(sPropertyKey);
        }

    }


    internal bool HasProperty(string sPropertyKey)
    {
        return htblProperties.ContainsKey(sPropertyKey);
        '        Dim lhtblProperties As PropertyHashTable
        '#If Runner Then
        '        lhtblProperties = htblProperties
        '#Else
        '        lhtblProperties = htblActualProperties
        '#End If
        '        Return lHasProperty(sPropertyKey)
    }


    '    Friend Function RemoveProperty(ByVal sPropertyKey As String) As Boolean
    '        Dim lhtblProperties As PropertyHashTable
    '#If Runner Then
    '        lhtblProperties = htblProperties
    '#Else
    '        lhtblProperties = htblActualProperties
    '#End If
    '        If lHasProperty(sPropertyKey) Then lhtblProperties.Remove(sPropertyKey)
    '    End Function
    internal void RemoveProperty(string sPropertyKey)
    {
        if (_htblActualProperties.ContainsKey(sPropertyKey))
        {
            _htblActualProperties.Remove(sPropertyKey);
            _htblProperties = Nothing
        }
    }

    internal void AddProperty(clsProperty prop, bool bActualProperties = true)
    {
        If HasProperty(prop.Key) Then RemoveProperty(prop.Key)
        'With htblProperties(prop.Key)
        '    .Value = prop.Value
        '    .StringData = prop.StringData.Copy
        'End With
        'Else
        if (bActualProperties)
        {
            _htblActualProperties.Add(prop);
        Else
            _htblInheritedProperties.Add(prop);
        }
        'End If
        _htblProperties = Nothing
    }


    internal void AddProperty(string sPropertyKey, bool bActualProperties = true)
    {
        if (Not HasProperty(sPropertyKey))
        {
            if (Adventure.htblAllProperties.ContainsKey(sPropertyKey))
            {
                AddProperty(Adventure.htblAllProperties(sPropertyKey).Copy, bActualProperties);
            Else
                throw New Exception("Property " & sPropertyKey & " does not exist!")
            }
            'SetPropertyValue(sPropertyKey, sValue)
        }
    }

    Friend Overrides ReadOnly Property AllDescriptions As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {

        }
    }

    Public Overrides ReadOnly Property Clone As clsItem
        {
            get
            {

        }
    }

    Public Overrides ReadOnly Property CommonName As String
        {
            get
            {

        }
    }

    public override bool DeleteKey(string sKey)
    {

    }

    public override void EditItem()
    {

    }

    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {

    }

    public override int ReferencesKey(string sKey)
    {

    }
}
}