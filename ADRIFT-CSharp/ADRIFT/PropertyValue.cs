using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class PropertyValue
{

    private clsProperty prop;
    private New StringArrayList arlDirectionRefs;
    private New StringArrayList arlObjectRefs;
    private New StringArrayList arlCharacterRefs;
    private string sCommand = "";


    Public Event ValueChanged(sender As Object, e As System.EventArgs)


    public string PropertyKey
        {
            get
            {
            if (prop IsNot null)
            {
                return prop.Key;
            Else
                return "";
            }
        }
set(String)
            if (prop Is null || value <> prop.Key)
            {
                if (Adventure IsNot null && Adventure.htblAllProperties.ContainsKey(value))
                {
                    prop = Adventure.htblAllProperties(value)
                    LoadList();
                }
            }
        }
    }


    Public WriteOnly Property Command As String
set(String)
            if (value <> sCommand)
            {
                sCommand = value
                arlDirectionRefs = GetReferences(ReferencesType.Direction, sCommand)
                arlObjectRefs = GetReferences(ReferencesType.Object, sCommand)
                arlCharacterRefs = GetReferences(ReferencesType.Character, sCommand)
            }
        }
    }


    private void LoadList()
    {

        With cmbList;
            .Items.Clear();

            switch (prop.Type)
            {
                case clsProperty.PropertyTypeEnum.CharacterKey:
                    {
                    .Items.Add(THEPLAYER, "[ The Player Character ]");
                    .Items.Add(ANYCHARACTER, "[ Any Character ]");
                    For Each c As String In arlCharacterRefs
                        .Items.Add(c, Adventure.GetNameFromKey(c, false));
                    Next;
                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                        .Items.Add(c.Key, c.Name);
                    Next;
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.Integer:
                    {
                    ExpressionOrVariable.Visible = true;
                    ExpressionOrVariable.AllowedListType = ItemSelector.ItemEnum.Expression | ItemSelector.ItemEnum.Variable;
                    ExpressionOrVariable.ListType = ItemSelector.ItemEnum.Expression;
                    cmbList.Visible = false;
                    ExpressionOrVariable.BringToFront();
                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    {
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        If g.GroupType = clsGroup.GroupTypeEnum.Locations Then .Items.Add(g.Key, g.Name)
                    Next;
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.LocationKey:
                    {
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        .Items.Add(loc.Key, loc.ShortDescription.ToString);
                    Next;
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.ObjectKey:
                    {
                    For Each o As String In arlObjectRefs
                        .Items.Add(o, Adventure.GetNameFromKey(o, false));
                    Next;
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        'If ob.HasSurface Then
                        .Items.Add(ob.Key, ob.FullName);
                    Next;
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.SelectionOnly:
                    {
                    .Items.Add(sSELECTED);
                    .Items.Add(sUNSELECTED);
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.StateList:
                    {
                    For Each s As String In prop.PossibleValues
                        .Items.Add(s, s);
                    Next;
                    ExpressionOrVariable.Visible = false;
                    cmbList.Visible = true;
                case clsProperty.PropertyTypeEnum.Text:
                    {
                    ExpressionOrVariable.Visible = true;
                    ExpressionOrVariable.AllowedListType = ItemSelector.ItemEnum.Expression | ItemSelector.ItemEnum.Variable;
                    ExpressionOrVariable.ListType = ItemSelector.ItemEnum.Expression;
                    cmbList.Visible = false;
                    ExpressionOrVariable.BringToFront();
                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    ExpressionOrVariable.Visible = true;
                    ExpressionOrVariable.ValueListPropertyKey = prop.Key;
                    ExpressionOrVariable.AllowedListType = ItemSelector.ItemEnum.Expression | ItemSelector.ItemEnum.ValueList;
                    ExpressionOrVariable.ListType = ItemSelector.ItemEnum.ValueList;
                    cmbList.Visible = false;
                    ExpressionOrVariable.BringToFront();
            }

        }

    }


    private void cmbList_ValueChanged(object sender, System.EventArgs e)
    {
        RaiseEvent ValueChanged(Me, e);
    }


    public string Value
        {
            get
            {
            if (cmbList.Visible)
            {
                If cmbList.SelectedItem == null Then Return null
                return cmbList.SelectedItem.DataValue.ToString;
            Else
                if (ExpressionOrVariable.ListType = ItemSelector.ItemEnum.Variable && Adventure.htblVariables.ContainsKey(ExpressionOrVariable.Key))
                {
                    return "%" + Adventure.htblVariables(ExpressionOrVariable.Key).Name + "%";
                Else
                    return ExpressionOrVariable.Key;
                }
            }
        }
set(String)
            if (prop IsNot null)
            {
                switch (prop.Type)
                {
                    case clsProperty.PropertyTypeEnum.CharacterKey:
                    case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    case clsProperty.PropertyTypeEnum.LocationKey:
                    case clsProperty.PropertyTypeEnum.ObjectKey:
                    case clsProperty.PropertyTypeEnum.StateList:
                    case clsProperty.PropertyTypeEnum.SelectionOnly:
                        {
                        SetCombo(cmbList, value);
                        cmbList.Visible = true;
                        ExpressionOrVariable.Visible = false;
                    case clsProperty.PropertyTypeEnum.Integer:
                    case clsProperty.PropertyTypeEnum.StateList:
                    case clsProperty.PropertyTypeEnum.Text:
                        {
                        private bool bSetVar = false;
                        if (value.Length > 2 && value.StartsWith("%") && value.EndsWith("%") Then '&& Adventure.htblVariables.ContainsKey(value.Substring(1, value.Length - 2)))
                        {
                            For Each v As clsVariable In Adventure.htblVariables.Values
                                if (value = "%" + v.Name + "%")
                                {
                                    ExpressionOrVariable.Key = v.Key;
                                    bSetVar = True
                                    Exit For;
                                }
                            Next;
                        }
                        if (Not bSetVar)
                        {
                            ExpressionOrVariable.ListType = ItemSelector.ItemEnum.Expression;
                            ExpressionOrVariable.Expression.Value = value;
                        }

                        cmbList.Visible = false;
                        ExpressionOrVariable.Visible = true;
                    case clsProperty.PropertyTypeEnum.ValueList:
                        {
                        private bool bSetVL = false;

                        if (IsNumeric(value))
                        {
                            private int iValue = SafeInt(value);

                            For Each sValue As String In prop.ValueList.Keys
                                if (prop.ValueList(sValue) = iValue)
                                {
                                    ExpressionOrVariable.Key = iValue.ToString;
                                    bSetVL = True
                                }
                            Next;
                        }
                        if (Not bSetVL)
                        {
                            ExpressionOrVariable.ListType = ItemSelector.ItemEnum.Expression;
                            ExpressionOrVariable.Expression.Value = value;
                        }
                }
            }
        }
    }


    private void PropertyValue_Resize(object sender, System.EventArgs e)
    {
        cmbList.DropDownListWidth = Math.Max(cmbList.Width, 170);
    }

}

}