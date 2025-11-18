using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{

public class ItemSelector
{

    Public Shadows Event GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    private bool bNeedsReload = false;


    <Flags()>;
public enum ItemEnum
    {
        [null] = 0;
        Location = 1
        [Object] = 2;
        Task = 4
        [Event] = 8;
        Character = 16
        Hint = 32
        [Property] = 64;
        LocationGroup = 128
        Variable = 256
        Expression = 512
        ValueList = 1024
    }
    private ItemEnum eAllowedListTypes;
    private ItemEnum eCurrentListType;

    Public Event FilledList(ByVal sender As Object, ByVal e As EventArgs)

    'Public Overrides Property BackColor() As Color
    '    Get
    '        Return MyBase.BackColor
    '    End Get
    '    Set(ByVal value As Color)
    '        MyBase.BackColor = value
    '        btnItemType.Appearance.BackColor = value
    '        btnEdit.Appearance.BackColor = value
    '        btnNew.Appearance.BackColor = value
    '    End Set
    'End Property

    private bool bAllowHidden = false;
    public bool AllowHidden
        {
            get
            {
            return bAllowHidden;
        }
set(Boolean)
            bAllowHidden = value
        }
    }


    private bool bAllowAddEdit = true;
    public bool AllowAddEdit { get; set; }
        {
            get
            {
            return bAllowAddEdit;
        }
set(ByVal Boolean)
            if (value <> bAllowAddEdit)
            {
                if (value)
                {
                    btnNew.Visible = true;
                    btnEdit.Visible = true;
                    cmbList.Width -= 20;
                Else
                    btnNew.Visible = false;
                    btnEdit.Visible = false;
                    cmbList.Width += 20;
                }
            }
            bAllowAddEdit = value
        }
    }

    public ItemEnum AllowedListType { get; set; }
        {
            get
            {
            return eAllowedListTypes;
        }
set(ByVal ItemEnum)
            eAllowedListTypes = value

            try
            {
                ' Default eCurrentListType to first one we find
                private int iCount = 0;
                For Each e As ItemEnum In New ItemEnum() {ItemEnum.Location, ItemEnum.Object, ItemEnum.Task, ItemEnum.Event, ItemEnum.Character, ItemEnum.LocationGroup, ItemEnum.Hint, ItemEnum.Property, ItemEnum.Variable, ItemEnum.Expression, ItemEnum.ValueList}
                    if (bIsItemInList(e))
                    {
                        If iCount = 0 Then ListType = e ' SetListType(e)
                        iCount += 1;
                    }
                Next;
                if (iCount = 1)
                {
                    btnItemType.Visible = false;
                    cmbList.Left = 0;
                    cmbList.Width = Me.Width - CInt(IIf(bAllowAddEdit, btnNew.Width - 2, 0));
                Else
                    btnItemType.Visible = true;
                    cmbList.Left = btnItemType.Width;
                    cmbList.Width = Me.Width - CInt(IIf(bAllowAddEdit, btnNew.Width - 2, 0)) - btnItemType.Width;
                }
            }
            catch (Exception ex)
            {
                ErrMsg("AllowedListType error", ex);
            }
        }
    }


    private string _sRestrictProperty;
    public string RestrictProperty
        {
            get
            {
            return _sRestrictProperty;
        }
set(String)
            if (value <> _sRestrictProperty)
            {
                _sRestrictProperty = value
                LoadList(ListType);
            }
        }
    }

    private string _ValueListPropertyKey;
    Public WriteOnly Property ValueListPropertyKey As String
set(String)
            _ValueListPropertyKey = value
            ListType = ItemEnum.ValueList
        }
    }


    private bool bLoaded = false;
    private string sKey;
    private string sLastValidKey;
    public string Key { get; set; }
        {
            get
            {
            if (Expression.Visible)
            {
                return Expression.Value;
            Else
                return sKey;
            }
        }
set(ByVal String)
            If value == null Then Exit Property ' Designer likes to give us a Key = null, doh!

            cmbList.SuspendLayout();

            private Type typKey = Adventure.GetTypeFromKey(value);
            if (typKey IsNot null)
            {
                switch (true)
                {
                    case typKey Is GetType(clsLocation):
                        {
                        If bIsItemInList(ItemEnum.Location) Then ListType = ItemEnum.Location ' SetListType(ItemEnum.Location)
                    case typKey Is GetType(clsObject):
                        {
                        If bIsItemInList(ItemEnum.Object) Then ListType = ItemEnum.Object ' SetListType(ItemEnum.Object)
                    case typKey Is GetType(clsTask):
                        {
                        If bIsItemInList(ItemEnum.Task) Then ListType = ItemEnum.Task ' SetListType(ItemEnum.Task)
                    case typKey Is GetType(clsEvent):
                        {
                        If bIsItemInList(ItemEnum.Event) Then ListType = ItemEnum.Event ' SetListType(ItemEnum.Event)
                    case typKey Is GetType(clsCharacter):
                        {
                        If bIsItemInList(ItemEnum.Character) Then ListType = ItemEnum.Character ' SetListType(ItemEnum.Character)
                    case typKey Is GetType(clsGroup):
                        {
                        If bIsItemInList(ItemEnum.LocationGroup) Then ListType = ItemEnum.LocationGroup ' SetListType(ItemEnum.LocationGroup)
                    case typKey Is GetType(clsVariable):
                        {
                        If bIsItemInList(ItemEnum.Variable) Then ListType = ItemEnum.Variable ' SetListType(ItemEnum.Variable)
                        '... etc
                }
            }

            sKey = value
            SetCombo(cmbList, sKey);

            cmbList.ResumeLayout();

            If ListType = ItemEnum.Expression Then Expression.Value = value

        }
    }


    public ItemEnum ListType { get; set; }
        {
            get
            {
            return eCurrentListType;
        }
set(ByVal ItemEnum)

            switch (value)
            {
                case ItemEnum.Character:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgCharacter16;
                case ItemEnum.Event:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgEvent16;
                case ItemEnum.Hint:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgHint16;
                case ItemEnum.Location:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgLocation16;
                case ItemEnum.LocationGroup:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgGroup16;
                case ItemEnum.Object:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgObjectDynamic16;
                case ItemEnum.Property:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgProperty16;
                case ItemEnum.Task:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgTaskGeneral16;
                case ItemEnum.Variable:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgVariable16;
                case ItemEnum.Expression:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgExpression;
                case ItemEnum.ValueList:
                    {
                    btnItemType.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgVariable16;
            }

            If DesignMode Then Exit Property

            if (value = ItemEnum.Expression)
            {
                switch (eCurrentListType)
                {
                    case ItemEnum.Character:
                    case ItemEnum.Event:
                    case ItemEnum.Hint:
                    case ItemEnum.Location:
                    case ItemEnum.LocationGroup:
                    case ItemEnum.Object:
                    case ItemEnum.Task:
                        {
                        Expression.Value = sLastValidKey;
                    case ItemEnum.Property:
                        {
                        TODO();
                    case ItemEnum.ValueList:
                        {
                        If sLastValidKey <> "" Then Expression.Value = sLastValidKey
                    case ItemEnum.Variable:
                        {
                        If sLastValidKey <> "" Then Expression.Value = "%" + sLastValidKey + "%"
                }
            Else
                if (eCurrentListType = ItemEnum.Expression)
                {
                    switch (value)
                    {
                        case ItemEnum.Property:
                            {
                            TODO();
                        case ItemEnum.ValueList:
                            {
                            If IsNumeric(Expression.Value) Then sLastValidKey = Expression.Value
                        case ItemEnum.Variable:
                            {
                            if (Expression.Value.StartsWith("%") && Expression.Value.EndsWith("%"))
                            {
                                sLastValidKey = Expression.Value.Substring(1, Expression.Value.Length - 2)
                            }
                    }
                }
            }

            eCurrentListType = value
            LoadList(value);

            If ComboContainsKey(cmbList, sLastValidKey) Then SetCombo(cmbList, sLastValidKey)

            RaiseEvent SelectionChanged(Me, New EventArgs);
        }
    }



    private void btnGroup_Click(System.Object sender, System.EventArgs e)
    {

        ' Got to cycle thru to the next allowed item type

        private ItemEnum eNewListType = CType(IIf(eCurrentListType = ItemEnum.ValueList, ItemEnum.Location, eCurrentListType * 2), ItemEnum);
        if (eNewListType = ItemEnum.null)
        {
            ErrMsg("List Type of null!");
            Exit Sub;
        }
        while (Not bIsItemInList(eNewListType))
        {
            eNewListType = CType(IIf(eNewListType = ItemEnum.ValueList, ItemEnum.Location, eNewListType * 2), ItemEnum)
        }

        sKey = ""
        ListType = eNewListType ' SetListType(eNewListType)

    }



    private bool bIsItemInList(ItemEnum eItem)
    {
        return CBool(eItem And eAllowedListTypes);
    }



    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return Me.cmbList.Focused || Me.btnItemType.Focused || Me.btnNew.Focused;
        }
    }


    private void cmbList_BeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e)
    {
        If bNeedsReload Then LoadList(ListType) ' ItemEnum.LocationGroup) ' In case any names changed - can't do after edit because we're not modal
        bNeedsReload = False
    }


    private void cmbList_Click(object sender, System.EventArgs e)
    {
        If bNeedsReload Then LoadList(ListType) ' ItemEnum.LocationGroup) ' In case any names changed - can't do after edit because we're not modal
        bNeedsReload = False
    }



    private void LocationGroup_GotFocus(object sender, System.EventArgs e)
    {
        RaiseEvent GotFocus(Me, e);
    }

    private void LocationGroup_LostFocus(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then RaiseEvent LostFocus(Me, e)
    }



    private void btnNew_Click(System.Object sender, System.EventArgs e)
    {

        switch (eCurrentListType)
        {
            case ItemEnum.Character:
                {
                private New clsCharacter chNew;
                private New frmCharacter(chNew, False) fCharacter;
                fCharacter.ShowDialog();
                sKey = chNew.Key
                LoadList(ItemEnum.Character);
                fCharacter.Dispose();

            case ItemEnum.Event:
                {
            case ItemEnum.Hint:
                {
            case ItemEnum.Location:
                {
                private New clsLocation locNew;
                private New frmLocation(locNew, False) fLocation;
                fLocation.ShowDialog();
                fLocation.Dispose();
                sKey = locNew.Key
                LoadList(ItemEnum.Location);
                fLocation.Dispose();

            case ItemEnum.LocationGroup:
                {
                ' Bring up rooms list
                ' If we create a group with same rooms as an existing group, ask if we want to use existing

                private New clsGroup grp;
                private New frmGroup(grp, False) fGroup;
                if (fGroup.ShowDialog = DialogResult.OK)
                {
                    ' Check to see if we have an existing group with the same locations...
                    For Each g As clsGroup In Adventure.htblGroups.Values
                        if (g.arlMembers.Count = grp.arlMembers.Count)
                        {
                            For Each s As String In grp.arlMembers
                                If ! g.arlMembers.Contains(s) Then GoTo CheckNextGroup
                            Next;
                            ' Ok, we have the same members as group g
                            if (MessageBox.Show("You already have a group '" + g.Name + "' with the same Locations.  Would you like to use this instead?", "Existing group found", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes)
                            {
                                sKey = g.Key
                                SetCombo(cmbList, sKey);
                                Exit Sub;
                            }
                        }
CheckNextGroup:;
                    Next;
                    Adventure.htblGroups.Add(grp, grp.Key);
                    sKey = grp.Key
                    LoadList(ItemEnum.LocationGroup);
                }
                fGroup.Dispose();

            case ItemEnum.Object:
                {
                private New clsObject obNew;
                private New frmObject(obNew, False) fObject;
                fObject.ShowDialog();
                fObject.Dispose();
                sKey = obNew.Key
                LoadList(ItemEnum.Object);
                fObject.Dispose();

            case ItemEnum.Property:
                {

            case ItemEnum.Task:
                {
                private New clsTask tasNew;
                private New frmTask(tasNew, False) fTask;
                fTask.ShowDialog();
                fTask.Dispose();
                sKey = tasNew.Key
                LoadList(ItemEnum.Task);
                fTask.Dispose();

            case ItemEnum.Variable:
                {
                private New clsVariable varNew;
                private New frmVariable(varNew, False) fVar;
                fVar.ShowDialog();
                fVar.Dispose();
                sKey = varNew.Key
                LoadList(ItemEnum.Variable);
                fVar.Dispose();

        }

    }



    private void LoadList(ItemEnum eListType)
    {

        ' cmbList.SuspendLayout()

        private bool bCmbVisible = true;
        cmbList.Items.Clear();
        cmbList.SortStyle = Infragistics.Win.ValueListSortStyle.Ascending;

        switch (eListType)
        {
            case ItemEnum.Character:
                {
                cmbList.Items.Add("", "[ No Character Selected ]");
                'Application.DoEvents()
                cmbList.Items.Add(THEPLAYER, "[ The Player Character ]");
                private IEnumerable<ValueListItem> chars = From ch In Adventure.htblCharacters.Values;
                                                             Where RestrictProperty = "" || ch.HasProperty(RestrictProperty);
                                                             Select New ValueListItem(ch.Key, ch.Name);
                cmbList.Items.AddRange(chars.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to characters)");
                ToolTip1.SetToolTip(btnNew, "Add new character");
                ToolTip1.SetToolTip(btnEdit, "Edit character");
            case ItemEnum.Event:
                {
            case ItemEnum.Hint:
                {
            case ItemEnum.Location:
                {
                if (bAllowHidden)
                {
                    cmbList.Items.Add("", "[  No Location Selected  ]");
                Else
                    cmbList.Items.Add("", "[ No Location Selected ]");
                }
                'Application.DoEvents()
                If bAllowHidden Then cmbList.Items.Add(HIDDEN, "[ Hidden ]")
                private IEnumerable<ValueListItem> locs = From loc In Adventure.htblLocations.Values;
                                                            Where RestrictProperty = "" || loc.HasProperty(RestrictProperty);
                                                            Select New ValueListItem(loc.Key, loc.ShortDescription.ToString);
                cmbList.Items.AddRange(locs.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to locations)");
                ToolTip1.SetToolTip(btnNew, "Add new location");
                ToolTip1.SetToolTip(btnEdit, "Edit location");
            case ItemEnum.LocationGroup:
                {
                cmbList.Items.Add("", "[ No Group Selected ]");
                'Application.DoEvents()
                private IEnumerable<ValueListItem> grps = From grp In Adventure.htblGroups.Values;
                                                            Where grp.GroupType = clsGroup.GroupTypeEnum.Locations;
                                                            Select New ValueListItem(grp.Key, grp.Name);
                cmbList.Items.AddRange(grps.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to groups)");
                ToolTip1.SetToolTip(btnNew, "Add new group");
                ToolTip1.SetToolTip(btnEdit, "Edit group");
            case ItemEnum.Object:
                {
                cmbList.Items.Add("", "[ No Object Selected ]");
                'Application.DoEvents()
                private IEnumerable<ValueListItem> objs = From obj In Adventure.htblObjects.Values;
                                                            Where RestrictProperty = "" || obj.HasProperty(RestrictProperty);
                                                            Select New ValueListItem(obj.Key, obj.FullName);
                cmbList.Items.AddRange(objs.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to objects)");
                ToolTip1.SetToolTip(btnNew, "Add new object");
                ToolTip1.SetToolTip(btnEdit, "Edit object");
            case ItemEnum.Property:
                {
            case ItemEnum.Task:
                {
                cmbList.Items.Add("", "[ No Task Selected ]");
                'Application.DoEvents()
                private IEnumerable<ValueListItem> tsks = From tsk In Adventure.htblTasks.Values;
                                                            Select New ValueListItem(tsk.Key, tsk.Description);
                cmbList.Items.AddRange(tsks.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to tasks)");
                ToolTip1.SetToolTip(btnNew, "Add new task");
                ToolTip1.SetToolTip(btnEdit, "Edit task");
            case ItemEnum.Variable:
                {
                cmbList.Items.Add("", "[ No Variable Selected ]");
                'Application.DoEvents()
                private IEnumerable<ValueListItem> vars = From var In Adventure.htblVariables.Values;
                                                            Select New ValueListItem(var.Key, var.Name);
                cmbList.Items.AddRange(vars.ToArray());
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to variables)");
                ToolTip1.SetToolTip(btnNew, "Add new variable");
                ToolTip1.SetToolTip(btnEdit, "Edit variable");
            case ItemEnum.Expression:
                {
                bCmbVisible = False
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to expressions)");
                ToolTip1.SetToolTip(btnNew, "Add new variable");
                ToolTip1.SetToolTip(btnEdit, "Edit variable");
                'Expression.Value = sKey
            case ItemEnum.ValueList:
                {
                cmbList.Items.Add("", "[ No Value Selected ]");
                'Application.DoEvents()
                private clsProperty p = null;
                if (Adventure.htblAllProperties.TryGetValue(_ValueListPropertyKey, p))
                {
                    cmbList.SortStyle = ValueListSortStyle.None;
                    private IEnumerable<ValueListItem> props = From val In p.ValueList.Keys;
                                                                 Select New ValueListItem(p.ValueList(val), val);
                    cmbList.Items.AddRange(props.ToArray());
                }
                ToolTip1.SetToolTip(btnItemType, "Change list type (currently set to valuelist)");
                ToolTip1.SetToolTip(btnEdit, "Edit valuelist");
                'Expression.Value = ""
        }

        cmbList.Visible = bCmbVisible;
        btnNew.Visible = bCmbVisible;
        btnEdit.Visible = bCmbVisible;
        Expression.Visible = ! bCmbVisible;
        If eListType = ItemEnum.ValueList Then btnNew.Visible = false

        SetCombo(cmbList, sKey);
        If cmbList.SelectedIndex = -1 Then cmbList.SelectedIndex = 0
        cmbList_SelectionChanged(cmbList, New EventArgs) ' force selection changed, because it now fires differently due to null values;
        RaiseEvent FilledList(Me, New EventArgs);

        'cmbList.ResumeLayout()

    }



    private void cmbList_SelectionChanged(object sender, System.EventArgs e)
    {

        If cmbList.SelectedItem != null Then sKey = CStr(cmbList.SelectedItem.DataValue)

        if (sKey <> "")
        {
            sLastValidKey = sKey
            btnEdit.Visible = true;
            btnNew.Visible = false;
        Else
            btnEdit.Visible = false;
            If ListType <> ItemEnum.ValueList Then btnNew.Visible = true
        }

        RaiseEvent SelectionChanged(Me, New EventArgs);

    }


    private void btnEdit_Click(object sender, System.EventArgs e)
    {
        switch (eCurrentListType)
        {
            case ItemEnum.Character:
                {
                If sKey = THEPLAYER Then sKey = Adventure.Player.Key
                private New frmCharacter(Adventure.htblCharacters(sKey), True) fCharacter;
            case ItemEnum.Event:
                {
                private New frmEvent(Adventure.htblEvents(sKey), True) fEvent;
            case ItemEnum.Hint:
                {
                private New frmHint(Adventure.htblHints(sKey), True) fHint;
            case ItemEnum.Location:
                {
                private New frmLocation(Adventure.htblLocations(sKey), True) fLocation;
            case ItemEnum.LocationGroup:
                {
                private New frmGroup(Adventure.htblGroups(sKey), True) fRoomGroup;
            case ItemEnum.Object:
                {
                private New frmObject(Adventure.htblObjects(sKey), True) fObject;
            case ItemEnum.Property:
                {
                private New frmProperty(Adventure.htblAllProperties(sKey), True) fProperty;
            case ItemEnum.Task:
                {
                private New frmTask(Adventure.htblTasks(sKey), True) fTask;
            case ItemEnum.Variable:
                {
                private New frmVariable(Adventure.htblVariables(sKey), True) fVar;
        }
        bNeedsReload = True

    }


    private void Expression_ValueChanged(object sender, System.EventArgs e)
    {
        'sKey = Expression.Value
        RaiseEvent SelectionChanged(sender, e);
    }

}

}