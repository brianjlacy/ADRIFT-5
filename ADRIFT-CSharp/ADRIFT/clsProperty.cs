using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsProperty
{
    Inherits clsItem;

internal enum PropertyOfEnum
    {
        Locations = 0
        Objects = 1
        Characters = 2
    }

internal enum PropertyTypeEnum
    {
        SelectionOnly;
        [Integer];
        Text;
        ObjectKey;
        StateList;
        CharacterKey;
        LocationKey;
        LocationGroupKey;
        ValueList;
    }

    'Private sKey As String
    private PropertyTypeEnum eType;
    private int iIntData;
    private Description sStringData;
    private bool bSelected;
    private string sDescription;
    private string sDependentKey;
    private string sDependentValue;
    private bool bMandatory;
    private bool bFromGroup;
    private string sRestrictProperty;
    private string sRestrictValue;

    internal New StringArrayList arlStates;
    internal New Dictionary<string, int> ValueList;

    'Public Property Key() As String
    '    Get
    '        Return sKey
    '    End Get
    '    Set(ByVal Value As String)
    '        ' Can't do this check, as this property is not just member of Adventure.htblAllProperties
    '        'If Not KeyExists(Value) Then
    '        sKey = Value
    '        'Else
    '        '    Throw New Exception("Key " & sKey & " already exists")
    '        'End If
    '    End Set
    'End Property

    private PropertyOfEnum ePropertyOf;
    internal PropertyOfEnum PropertyOf { get; set; }
        {
            get
            {
            return ePropertyOf;
        }
set(ByVal PropertyOfEnum)
            ePropertyOf = value
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

    public bool Mandatory { get; set; }
        {
            get
            {
            return bMandatory;
        }
set(ByVal Value As Boolean)
            bMandatory = Value
        }
    }

    public bool FromGroup { get; set; }
        {
            get
            {
            return bFromGroup;
        }
set(ByVal Boolean)
            bFromGroup = value
        }
    }

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

    private string sAppendToProperty;
    public string AppendToProperty { get; set; }
        {
            get
            {
            return sAppendToProperty;
        }
set(ByVal String)
            sAppendToProperty = value
        }
    }

    private bool bGroupOnly;
    public bool GroupOnly
        {
            get
            {
            return bGroupOnly;
        }
set(Boolean)
            bGroupOnly = value
        }
    }

    internal PropertyTypeEnum Type { get; set; }
        {
            get
            {
            return eType;
        }
set(ByVal Value As PropertyTypeEnum)
            eType = Value
            switch (eType)
            {
                case PropertyTypeEnum.StateList:
                    {
                    if (Not arlStates.Contains(StringData.ToString) && arlStates.Count > 0)
                    {
                        StringData = New Description(arlStates(0)) ' Default the value to the first state
                    }
                case PropertyTypeEnum.ValueList:
                    {
                    if (Not ValueList.ContainsValue(iIntData) && ValueList.Count > 0)
                    {
                        'StringData = New Description(arlStates(0)) ' Default the value to the first state
                        For Each iValue As Integer In ValueList.Values
                            iIntData = iValue
                            Exit For;
                        Next;
                    }
            }
        }
    }

    public int IntData { get; set; }
        {
            get
            {
            return iIntData;
        }
set(ByVal Value As Integer)
            iIntData = Value
        }
    }

    internal Description StringData { get; set; }
        {
            get
            {
            return sStringData;
        }
set(ByVal Value As Description)
            sStringData = Value
        }
    }

    public bool Selected { get; set; }
        {
            get
            {
            return bSelected;
        }
set(ByVal Value As Boolean)
            bSelected = Value
        }
    }

    private int _Indent = 0;
    public int Indent
        {
            get
            {
            return _Indent;
        }
set(Integer)
            _Indent = value
        }
    }

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            'Return CType(Me.MemberwiseClone, clsProperty)
            return Copy();
        }
    }

    'Public Property BoolData() As Boolean
    '    Get
    '        If iIntData = 0 Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If Value Then
    '            iIntData = 1
    '        Else
    '            iIntData = 0
    '        End If
    '    End Set
    'End Property

    public string DependentKey { get; set; }
        {
            get
            {
            return sDependentKey;
        }
set(ByVal Value As String)
            if (iLoading > 0 || Value Is null || KeyExists(Value))
            {
                sDependentKey = Value
            Else
                throw New Exception("Key " & Value & " doesn't exist")
            }
        }
    }

    public string DependentValue { get; set; }
        {
            get
            {
            return sDependentValue;
        }
set(ByVal Value As String)
            ' TODO - check that it's a valid value
            sDependentValue = Value
        }
    }

    public string RestrictProperty { get; set; }
        {
            get
            {
            return sRestrictProperty;
        }
set(ByVal Value As String)
            sRestrictProperty = Value
        }
    }

    public string RestrictValue { get; set; }
        {
            get
            {
            return sRestrictValue;
        }
set(ByVal Value As String)
            sRestrictValue = Value
        }
    }


    Friend ReadOnly Property PossibleValues(Optional ByVal CurrentProperties As PropertyHashTable = Nothing) As StringArrayList
        {
            get
            {
            private New StringArrayList sValues;

            switch (Type)
            {
                case clsProperty.PropertyTypeEnum.CharacterKey:
                    {
                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                        if ((RestrictProperty Is null || ch.HasProperty(RestrictProperty)))
                        {
                            if (RestrictValue Is null || ch.GetPropertyValue(RestrictProperty) = RestrictValue)
                            {
                                sValues.Add(ch.Key);
                            }
                        }
                    Next;
                case clsProperty.PropertyTypeEnum.Integer:
                    {
                    TODO("Integer values");
                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    {
                    TODO("Location Group values");
                case clsProperty.PropertyTypeEnum.LocationKey:
                    {
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        if ((RestrictProperty Is null || loc.HasProperty(RestrictProperty)))
                        {
                            if (RestrictValue Is null || loc.GetPropertyValue(RestrictProperty) = RestrictValue)
                            {
                                sValues.Add(loc.Key);
                            }
                        }
                    Next;
                case clsProperty.PropertyTypeEnum.ObjectKey:
                    {
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        if ((RestrictProperty Is null || ob.HasProperty(RestrictProperty)))
                        {
                            if (RestrictValue Is null || ob.GetPropertyValue(RestrictProperty) = RestrictValue)
                            {
                                sValues.Add(ob.Key);
                            }
                        }
                    Next;
                case clsProperty.PropertyTypeEnum.SelectionOnly:
                    {
                    sValues.Add(sSELECTED);
                    sValues.Add(sUNSELECTED) ' ! strictly a property value, but only way for action to remove the property;
                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    For Each sState As String In arlStates
                        sValues.Add(sState);
                    Next;
                    If CurrentProperties == null Then CurrentProperties = Adventure.htblAllProperties
                    For Each prop As clsProperty In CurrentProperties.Values
                        if ((CurrentProperties Is Adventure.htblAllProperties || prop.Selected) && prop.Type = PropertyTypeEnum.StateList && prop.AppendToProperty = Me.Key)
                        {
                            For Each sState As String In prop.arlStates
                                sValues.Add(sState);
                            Next;
                        }
                    Next;
                    'End If
                case clsProperty.PropertyTypeEnum.Text:
                    {
                    TODO("Text values");
            }

            return sValues;
        }
    }

    Public Property Value(Optional ByVal bTesting As Boolean = False) As String
        {
            get
            {
            switch (eType)
            {
                case PropertyTypeEnum.StateList:
                case PropertyTypeEnum.ObjectKey:
                case PropertyTypeEnum.CharacterKey:
                case PropertyTypeEnum.LocationKey:
                case PropertyTypeEnum.LocationGroupKey:
                    {
                    return sStringData.ToString;
                case PropertyTypeEnum.ValueList:
                    {
                    return iIntData.ToString 'SafeString(ValueList(sStringData.ToString));
                case PropertyTypeEnum.Text:
                    {
                    return sStringData.ToString(bTesting);
                case PropertyTypeEnum.Integer:
                    {
                    return iIntData.ToString;
                case PropertyTypeEnum.SelectionOnly:
                    {
                    return true.ToString;
            }
            return null;
        }
set(ByVal Value As String)
            try
            {
                switch (eType)
                {
                    case PropertyTypeEnum.StateList:
                        {
                        if (iLoading > 0 || Value Is null || PossibleValues.Contains(Value))
                        {
                            sStringData = New Description(Value)
                        ElseIf PossibleValues.Count > 0 Then
                            ' Perhaps it's an expression that resolves to a valid state...
                            private New clsVariable v;
                            v.Type = clsVariable.VariableTypeEnum.Text;
                            v.SetToExpression(Value);
                            if (v.StringValue IsNot null && PossibleValues.Contains(v.StringValue))
                            {
                                sStringData = New Description(v.StringValue)
                            Else
                                throw New Exception("'" & Value & "' is not a valid state.")
                            }
                        }
                    case PropertyTypeEnum.ValueList:
                        {
                        if (iLoading > 0 || Value Is null || ValueList.ContainsKey(Value) || ValueList.ContainsValue(SafeInt(Value)))
                        {
                            if (ValueList.ContainsKey(Value))
                            {
                                iIntData = ValueList(Value)
                            Else
                                iIntData = SafeInt(Value)
                            }
                        ElseIf ValueList.Count > 0 Then
                            ' Perhaps it's an expression that resolves to a valid state...
                            private New clsVariable v;
                            v.Type = clsVariable.VariableTypeEnum.Text;
                            v.SetToExpression(Value);
                            if (v.StringValue IsNot null && ValueList.ContainsKey(v.StringValue))
                            {
                                iIntData = ValueList(v.StringValue)
                            Else
                                throw New Exception("'" & Value & "' is not a valid state.")
                            }
                        }
                    case PropertyTypeEnum.ObjectKey:
                    case PropertyTypeEnum.CharacterKey:
                    case PropertyTypeEnum.LocationKey:
                    case PropertyTypeEnum.LocationGroupKey:
                        {
                        sStringData = New Description(Value)
                    case PropertyTypeEnum.Text:
                        {
                        sStringData = New Description(Value)
                    case PropertyTypeEnum.Integer:
                        {
                        if (IsNumeric(Value))
                        {
                            iIntData = SafeInt(Value)
                        Else
                            private New clsVariable var;
                            var.Type = clsVariable.VariableTypeEnum.Numeric;
                            var.SetToExpression(Value);
                            iIntData = var.IntValue
                        }
                }
            }
            catch (Exception ex)
            {
                ErrMsg("Error Setting Property " + Description + " to """ + Value + """", ex);
            }
        }
    }

    public clsProperty Copy()
    {

        private New clsProperty p;

        With p;
            .arlStates = Me.arlStates.Clone;
            .ValueList.Clear();
            For Each sLabel As String In Me.ValueList.Keys
                .ValueList.Add(sLabel, Me.ValueList(sLabel));
            Next;
            .DependentKey = Me.DependentKey;
            .DependentValue = Me.DependentValue;
            .RestrictProperty = Me.RestrictProperty;
            .RestrictValue = Me.RestrictValue;
            .Description = Me.Description;
            .IntData = Me.IntData;
            .Key = Me.Key;
            .Mandatory = Me.Mandatory;
            .Selected = Me.Selected;
            .StringData = Me.StringData.Copy;
            .Type = Me.Type;
            .PropertyOf = Me.PropertyOf;
            .GroupOnly = Me.GroupOnly;
            .FromGroup = Me.FromGroup;
            switch (.Type)
            {
                case PropertyTypeEnum.ObjectKey:
                case PropertyTypeEnum.CharacterKey:
                case PropertyTypeEnum.LocationKey:
                case PropertyTypeEnum.LocationGroupKey:
                case PropertyTypeEnum.Text:
                case PropertyTypeEnum.ValueList:
                    {
                    ' These will all re-write StringData, so we can ignore
                default:
                    {
                    .Value = Me.Value;
            }
            .AppendToProperty = Me.AppendToProperty;
        }

        return p;

    }


    Public Shadows Function ToString() As String
        return Me.Description + " (" + Me.Value + ")";
    }

    public void New()
    {
        Me.StringData = New Description;
    }

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
            all.Add(Me.StringData);
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
        private New frmProperty(Me, True) fProperty;
#endif
    }


    public override int ReferencesKey(string sKey)
    {
        private int iCount = 0;
        If Me.DependentKey = sKey Then iCount += 1
        If Me.RestrictProperty = sKey Then iCount += 1
        return iCount;
    }


    public override bool DeleteKey(string sKey)
    {
        If Me.DependentKey = sKey Then Me.DependentKey = null
        If Me.RestrictProperty = sKey Then Me.RestrictProperty = null
        return true;
    }

}

}