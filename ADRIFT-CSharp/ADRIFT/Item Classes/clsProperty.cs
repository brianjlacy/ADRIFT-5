using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<DebuggerDisplay("Name: {Description}, Type: {Type}")> _;
public class clsProperty
{
    Inherits clsItem;

internal enum PropertyOfEnum
    {
        Locations = 0
        Objects = 1
        Characters = 2
        AnyItem = 3
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


    private PropertyTypeEnum eType;
    private string sDependentKey;

    internal New StringArrayList arlStates;
    internal New Dictionary<string, Integer)(System.StringComparer.OrdinalIgnoreCase> ValueList;

    internal PropertyOfEnum PropertyOf { get; set; }
    public String = "" PrivateTo { get; set; }
    public bool Mandatory { get; set; }
    public bool FromGroup { get; set; }
    public string Description { get; set; }
    public string AppendToProperty { get; set; }
    public bool GroupOnly { get; set; }
    public int IntData { get; set; }
    internal Description StringData { get; set; }
    public bool Selected { get; set; }
    public Integer = 0 Indent { get; set; }
    public string DependentValue { get; set; }
    public string RestrictProperty { get; set; }
    public string RestrictValue { get; set; }
    public string PopupDescription { get; set; }


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
                    if (Not ValueList.ContainsValue(IntData) && ValueList.Count > 0)
                    {
                        For Each iValue As Integer In ValueList.Values
                            IntData = iValue
                            Exit For;
                        Next;
                    }
            }
        }
    }



    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return Copy();
        }
    }


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
                    return StringData.ToString;
                case PropertyTypeEnum.ValueList:
                    {
                    return IntData.ToString 'SafeString(ValueList(sStringData.ToString));
                case PropertyTypeEnum.Text:
                    {
                    return StringData.ToString(bTesting);
                case PropertyTypeEnum.Integer:
                    {
                    return IntData.ToString;
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
                            StringData = New Description(Value)
                        ElseIf PossibleValues.Count > 0 Then
                            ' Perhaps it's an expression that resolves to a valid state...
                            private New clsVariable v;
                            v.Type = clsVariable.VariableTypeEnum.Text;
                            v.SetToExpression(Value);
                            if (v.StringValue IsNot null && PossibleValues.Contains(v.StringValue))
                            {
                                StringData = New Description(v.StringValue)
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
                                IntData = ValueList(Value)
                            Else
                                IntData = SafeInt(Value)
                            }
                        ElseIf ValueList.Count > 0 Then
                            ' Perhaps it's an expression that resolves to a valid state...
                            private New clsVariable v;
                            v.Type = clsVariable.VariableTypeEnum.Text;
                            v.SetToExpression(Value);
                            if (v.StringValue IsNot null && ValueList.ContainsKey(v.StringValue))
                            {
                                IntData = ValueList(v.StringValue)
                            Else
                                throw New Exception("'" & Value & "' is not a valid state.")
                            }
                        }
                    case PropertyTypeEnum.ObjectKey:
                    case PropertyTypeEnum.CharacterKey:
                    case PropertyTypeEnum.LocationKey:
                    case PropertyTypeEnum.LocationGroupKey:
                        {
                        StringData = New Description(Value)
                    case PropertyTypeEnum.Text:
                        {
                        if (StringData.Count < 2)
                        {
                            ' Simple text stored
                            If ! (StringData.Count = 1 && StringData(0).Description = Value) Then StringData = New Description(Value)
                        Else
                            ' We have a complex description here, probably with restrictions etc - if we set the property, we'll lose it
                        }
                    case PropertyTypeEnum.Integer:
                        {
                        if (IsNumeric(Value))
                        {
                            IntData = SafeInt(Value)
                        Else
                            private New clsVariable var;
                            var.Type = clsVariable.VariableTypeEnum.Numeric;
                            var.SetToExpression(Value);
                            IntData = var.IntValue
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
            .AppendToProperty = Me.AppendToProperty;
            .RestrictProperty = Me.RestrictProperty;
            .RestrictValue = Me.RestrictValue;
            .Description = Me.Description;
            .IntData = Me.IntData;
            .Key = Me.Key;
            .Mandatory = Me.Mandatory;
            .Selected = Me.Selected;
            .Type = Me.Type ' Needs to be set before StringData, otherwise an appended StateList won't be allowed as a valid value (e.g. Locked will be reset to Open);
            .StringData = Me.StringData.Copy;
            .PropertyOf = Me.PropertyOf;
            .GroupOnly = Me.GroupOnly;
            .FromGroup = Me.FromGroup;
            .PrivateTo = Me.PrivateTo;
            .PopupDescription = Me.PopupDescription;

            switch (.Type)
            {
                case PropertyTypeEnum.ObjectKey:
                case PropertyTypeEnum.CharacterKey:
                case PropertyTypeEnum.LocationKey:
                case PropertyTypeEnum.LocationGroupKey:
                case PropertyTypeEnum.Text:
                case PropertyTypeEnum.ValueList:
                case PropertyTypeEnum.StateList:
                    {
                    ' These will all re-write StringData, so we can ignore
                default:
                    {
                    .Value = Me.Value;
            }
        }

        return p;

    }



    Public Shadows Function ToString() As String
        return Me.Description + " (" + Me.Value + ")";
    }



    public void New()
    {
        Me.StringData = New Description;
        Me.PropertyOf = PropertyOfEnum.Objects;
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
        iReplacements += MyBase.FindStringInStringProperty(Description, sSearchString, sReplace, bFindAll);
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