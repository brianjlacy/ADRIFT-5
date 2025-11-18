using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmDebugger
{

    private New System.Text.StringBuilder sText;
    private New ImageList imgList;
    private Color DisabledColour = SystemColors.InactiveCaptionText;


    Friend ReadOnly Property HasText As Boolean
        {
            get
            {
            return sText.ToString.Length > 0;
        }
    }


    private void frmDebugger_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
#if Mono
#else
        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(fRunner.UTMMain.Tools("Debugger")).Checked = false;
#endif
        SaveFormPosition(Me);
        SaveSetting("ADRIFT", "Runner", "ShowTimes", CInt(chkShowTimes.Checked).ToString);
        For Each opt As RadioButton In New RadioButton() {optLow, optMedium, optHigh}
            If opt.Checked Then SaveSetting("ADRIFT", "Runner", "DetailLevel", opt.Tag.ToString)
        Next;
        'If optDetail.CheckedItem IsNot Nothing Then SaveSetting("ADRIFT", "Runner", "DetailLevel", optDetail.CheckedItem.DataValue.ToString)
        SaveSetting("ADRIFT", "Runner", "DebugSplit", Me.SplitContainer1.SplitterDistance.ToString);
    }


    private void frmDebugger_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me, null, null, true);
        chkShowTimes.Checked = SafeBool(GetSetting("ADRIFT", "Runner", "ShowTimes", "0"));
        For Each opt As RadioButton In New RadioButton() {optLow, optMedium, optHigh}
            If CInt(GetSetting("ADRIFT", "Runner", "DetailLevel", "1")) = CInt(opt.Tag) Then opt.Checked = true
        Next;
        'SetOptSet(optDetail, Math.Min(CInt(GetSetting("ADRIFT", "Runner", "DetailLevel", "1")), 2))
        SplitContainer1.SplitterDistance = CInt(GetSetting("ADRIFT", "Runner", "DebugSplit", "200"));
        SplitContainer2.Panel2Collapsed = true;
        With imgList.Images;
            .Add("", New Bitmap(1, 1));
            .Add("imgLocation16", My.Resources.Resources.imgLocation16);
            .Add("imgObjectStatic16", My.Resources.Resources.imgObjectStatic16);
            .Add("imgObjectDynamic16", My.Resources.Resources.imgObjectDynamic16);
            .Add("imgTaskGeneral16", My.Resources.Resources.imgTaskGeneral16);
            .Add("imgTaskSpecific16", My.Resources.Resources.imgTaskSpecific16);
            .Add("imgTaskSystem16", My.Resources.Resources.imgTaskSystem16);
            .Add("imgEvent16", My.Resources.Resources.imgEvent16);
            .Add("imgCharacter16", My.Resources.Resources.imgCharacter16);
            .Add("imgVariable16", My.Resources.Resources.imgVariable16);
        }
        treeItemBrowser.ImageList = imgList;
        treeItemBrowser.TreeViewNodeSorter = New NodeSorter;
        BuildTree();
    }


private class NodeSorter
    {
        Implements IComparer;

        public Integer Implements System.Collections.IComparer.Compare Compare(object x, object y)
        {
            private TreeNode tx = CType(x, TreeNode);
            private TreeNode ty = CType(y, TreeNode);

            If tx.Parent != null Then Return tx.Text.CompareTo(ty.Text)
        }
    }


    public void BuildTree()
    {

        If Adventure == null Then Exit Sub
        private TreeNode n;

        treeItemBrowser.CheckBoxes = true;
        treeItemBrowser.Nodes.Clear();
        n = treeItemBrowser.Nodes.Add("Locations", "Locations")
        n.ImageKey = "imgLocation16";
        n.SelectedImageKey = n.ImageKey;
        n = treeItemBrowser.Nodes.Add("Objects", "Objects")
        n.ImageKey = "imgObjectStatic16";
        n.SelectedImageKey = n.ImageKey;
        n = treeItemBrowser.Nodes.Add("Tasks", "Tasks")
        n.ImageKey = "imgTaskGeneral16";
        n.SelectedImageKey = n.ImageKey;
        n = treeItemBrowser.Nodes.Add("Events", "Events")
        n.ImageKey = "imgEvent16";
        n.SelectedImageKey = n.ImageKey;
        n = treeItemBrowser.Nodes.Add("Characters", "Characters")
        n.ImageKey = "imgCharacter16";
        n.SelectedImageKey = n.ImageKey;
        n = treeItemBrowser.Nodes.Add("Variables", "Variables")
        n.ImageKey = "imgVariable16";
        n.SelectedImageKey = n.ImageKey;

        For Each l As clsLocation In Adventure.htblLocations.Values
            'Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode = treeItemBrowser.Nodes("Locations").Nodes.Add(l.Key, l.ShortDescription.ToString)
            n = treeItemBrowser.Nodes("Locations").Nodes.Add(l.Key, l.ShortDescription.ToString)
            'n.LeftImages.Add(My.Resources.Resources.imgLocation16)
            n.ImageKey = "imgLocation16";
            n.SelectedImageKey = n.ImageKey;
        Next;
        'treeItemBrowser.Nodes("Locations").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Locations").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Locations").Checked = true;

        For Each o As clsObject In Adventure.htblObjects.Values
            'Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode = treeItemBrowser.Nodes("Objects").Nodes.Add(o.Key, o.FullName)
            n = treeItemBrowser.Nodes("Objects").Nodes.Add(o.Key, o.FullName)
            if (o.IsStatic)
            {
                'n.LeftImages.Add(My.Resources.Resources.imgObjectStatic16)
                n.ImageKey = "imgObjectStatic16";
            Else
                'n.LeftImages.Add(My.Resources.Resources.imgObjectDynamic16)
                n.ImageKey = "imgObjectDynamic16";
            }
            n.SelectedImageKey = n.ImageKey;
        Next;
        'treeItemBrowser.Nodes("Objects").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Objects").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Objects").Checked = true;

        For Each t As clsTask In Adventure.htblTasks.Values
            if (t.TaskType <> clsTask.TaskTypeEnum.Specific)
            {
                n = treeItemBrowser.Nodes("Tasks").Nodes.Add(t.Key, t.Description)
                switch (t.TaskType)
                {
                    case clsTask.TaskTypeEnum.General:
                        {
                        n.ImageKey = "imgTaskGeneral16";
                    case clsTask.TaskTypeEnum.System:
                        {
                        n.ImageKey = "imgTaskSystem16";
                }
                n.SelectedImageKey = n.ImageKey;
            }
        Next;
        For Each t As clsTask In Adventure.htblTasks.Values
            if (t.TaskType = clsTask.TaskTypeEnum.Specific)
            {
                Dim nParent() As TreeNode = treeItemBrowser.Nodes.Find(t.Parent, True)
                if (nParent.Length = 1)
                {
                    n = nParent(0).Nodes.Add(t.Key, t.Description)
                    n.ImageKey = "imgTaskSpecific16";
                    n.SelectedImageKey = n.ImageKey;
                }
            }
        Next;

        'treeItemBrowser.Nodes("Tasks").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Tasks").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Tasks").Checked = true;

        For Each e As clsEvent In Adventure.htblEvents.Values
            'Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode = treeItemBrowser.Nodes("Events").Nodes.Add(e.Key, e.Description)
            n = treeItemBrowser.Nodes("Events").Nodes.Add(e.Key, e.Description)
            'n.LeftImages.Add(My.Resources.Resources.imgEvent16)
            if (e.EventType = clsEvent.EventTypeEnum.TurnBased)
            {
                n.ImageKey = "imgEvent16";
            Else
                n.ImageKey = "imgTimeEvent16";
            }
            n.SelectedImageKey = n.ImageKey;
        Next;
        'treeItemBrowser.Nodes("Events").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Events").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Events").Checked = true;

        For Each c As clsCharacter In Adventure.htblCharacters.Values
            'Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode = treeItemBrowser.Nodes("Characters").Nodes.Add(c.Key, c.Name)
            n = treeItemBrowser.Nodes("Characters").Nodes.Add(c.Key, c.Name)
            'n.LeftImages.Add(My.Resources.Resources.imgCharacter16)
            n.ImageKey = "imgCharacter16";
            n.SelectedImageKey = n.ImageKey;
        Next;
        'treeItemBrowser.Nodes("Characters").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Characters").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Characters").Checked = true;

        For Each v As clsVariable In Adventure.htblVariables.Values
            'Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode = treeItemBrowser.Nodes("Variables").Nodes.Add(v.Key, v.Name)
            n = treeItemBrowser.Nodes("Variables").Nodes.Add(v.Key, v.Name)
            'n.LeftImages.Add(My.Resources.Resources.imgVariable16)
            n.ImageKey = "imgVariable16";
            n.SelectedImageKey = n.ImageKey;
        Next;
        'treeItemBrowser.Nodes("Variables").Nodes.Override.Sort = Infragistics.Win.UltraWinTree.SortType.Ascending
        ' TODO
        'treeItemBrowser.Nodes("Variables").CheckedState = CheckState.Checked
        treeItemBrowser.Nodes("Variables").Checked = true;

    }


    private void treeItemBrowser_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {

        if (e.Node.GetNodeCount(false) > 0 Then ' e.TreeNode.HasNodes)
        {
            For Each n As TreeNode In e.Node.Nodes ' e.TreeNode.Nodes
                n.Checked = e.Node.Checked ' e.TreeNode.CheckedState;
            Next;
        }

    }


    internal void Print(ItemEnum eItemType, string sKey, DebugDetailLevelEnum eDetailLevel, string sMessage, bool bNewLine)
    {

        'Dim bEndOfTurn As Boolean = False
        Static bLastHadNewLine As Boolean = true;

        'If bNewLine Then
        '    Debug.WriteLine(sMessage)
        'Else
        '    Debug.Write(sMessage)
        'End If
        sMessage = sMessage.Replace(">", "&gt;").Replace("<", "&lt;")

        'If optDetail.CheckedItem Is Nothing Then Exit Sub
        if (sMessage <> "ENDOFTURN")
        {
            if (eDetailLevel = DebugDetailLevelEnum.Error || eDetailLevel = DebugDetailLevelEnum.Low || (eDetailLevel = DebugDetailLevelEnum.Medium && (optMedium.Checked || optHigh.Checked)) || (eDetailLevel = DebugDetailLevelEnum.High && optHigh.Checked))
            {
                private TreeNode[] Nodes = treeItemBrowser.Nodes.Find(sKey, true) '   treeItemBrowser.GetNodeByKey(sKey);
                if ((sKey = "" && treeItemBrowser.Nodes.ContainsKey("Tasks") && treeItemBrowser.Nodes("Tasks").Checked) || Nodes.Length = 1)
                {
                    private TreeNode Node = null;
                    If Nodes.Length = 1 Then Node = Nodes(0)
                    if (sKey = "" || (Node IsNot null && Node.Checked))
                    {
                        switch (eDetailLevel)
                        {
                            case DebugDetailLevelEnum.Error:
                                {
                                sMessage = "<c><b>ERROR: " & sMessage & "</b></c>"
                            case DebugDetailLevelEnum.High:
                                {
                                sMessage = "<i>" & sMessage & "</i>"
                            case DebugDetailLevelEnum.Medium:
                                {
                                ' Leave
                            case DebugDetailLevelEnum.Low:
                                {
                                sMessage = "<b>" & sMessage & "</b>"
                        }
                        If Me.chkShowTimes.Checked && bLastHadNewLine Then sMessage = "[" + Mid(Now.Subtract(UserSession.dtDebug).ToString, 7, 5) + "]  " + Space(Math.Max(UserSession.iDebugIndent, 0) * 4) + sMessage
                        'sMessage = "<font face=""courier new"">" & sMessage & "</font>"
                        switch (eItemType)
                        {
                            case ItemEnum.Task:
                                {
                                sMessage = "<font size=10 color=#005500>" & sMessage & "</font>"
                            case ItemEnum.Event:
                                {
                                sMessage = "<font size=10 color=#000055>" & sMessage & "</font>"
                            case ItemEnum.Character:
                                {
                                sMessage = "<font size=10 color=#550000>" & sMessage & "</font>"
                            default:
                                {
                                sMessage = "<font size=10 color=#000000>" & sMessage & "</font>"
                        }
                        If bNewLine Then sMessage &= "<br>"
                        'If sMessage.Contains("ENDOFTURN") Then
                        '    sMessage = "<center><font color=#BBBBBB size=-2>...............................................</font><br></center>"
                        '    bEndOfTurn = True
                        'End If

                        'Source2HTML(sMessage, Me.rtxtDebug, False)
                        'Me.rtxtDebug.AppendText(sMessage)
                        sText.Append(sMessage);
                    }
                }
            }
        Else
            sText.Append("<center><font color=#BBBBBB size=-2>...............................................</font><br></center>");
            Source2HTML(sText.ToString, rtxtDebug, false, true);
            sText = New System.Text.StringBuilder
            RefreshValues();
        }

        'If bEndOfTurn Then
        '    'rtxtDebug.AppendText(sText.ToString)
        '    Source2HTML(sText.ToString, rtxtDebug, False)
        '    sText = New System.Text.StringBuilder
        'End If

        bLastHadNewLine = bNewLine

        'If treeItemBrowser.SelectedNode IsNot Nothing Then '     .SelectedNode.Count = 1 Then
        '    Dim nod As TreeNode = treeItemBrowser.SelectedNode '  .SelectedNodes(0)
        '    nod.Selected = False
        '    nod.Selected = True
        'End If

    }


    public void RefreshValues()
    {
        If treeItemBrowser.SelectedNode != null Then RefreshFromKey(treeItemBrowser.SelectedNode.Name)
    }


private enum ColumnType
    {
        Combo;
        Text;
        Number;
    }

    private void AddProperty(clsProperty p)
    {

        switch (p.Type)
        {
            case clsProperty.PropertyTypeEnum.CharacterKey:
                {
                private New List<KeyValue> dictCharacters;
                For Each c As clsCharacter In Adventure.htblCharacters.Values
                    'listCharacters.Add(c.Key) 'c.Name & "~" & c.Key)
                    dictCharacters.Add(New KeyValue(c.Key, c.Name));
                Next;
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo, dictCharacters);
            case clsProperty.PropertyTypeEnum.Integer:
                {
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Number);
            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                {
                private New List<KeyValue> dictKeys;
                For Each g As clsGroup In Adventure.htblGroups.Values
                    If g.GroupType = clsGroup.GroupTypeEnum.Locations Then dictKeys.Add(New KeyValue(g.Key, g.Name))
                Next;
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo, dictKeys);
            case clsProperty.PropertyTypeEnum.LocationKey:
                {
                private New List<KeyValue> dictLocations;
                For Each l As clsLocation In Adventure.htblLocations.Values
                    dictLocations.Add(New KeyValue(l.Key, l.ShortDescription.ToString)) 'c.Name + "~" + c.Key);
                Next;
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo, dictLocations);
            case clsProperty.PropertyTypeEnum.ObjectKey:
                {
                private New List<KeyValue> dictObjects;
                For Each o As clsObject In Adventure.htblObjects.Values
                    dictObjects.Add(New KeyValue(o.Key, o.FullName)) 'c.Name + "~" + c.Key);
                Next;
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo, dictObjects);
            case clsProperty.PropertyTypeEnum.StateList:
                {
                private New List<KeyValue> dictStates;
                For Each s As String In p.arlStates
                    dictStates.Add(New KeyValue(s));
                Next;
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo, dictStates);
            case clsProperty.PropertyTypeEnum.Text:
                {
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Text);
            case clsProperty.PropertyTypeEnum.ValueList:
                {
                AddProperty(p.Key, p.Description, p.Value, ColumnType.Combo);
        }
        if (p.FromGroup)
        {
            With grdProperties.Rows(grdProperties.Rows.Count - 1);
                'grdProperties.Rows(grdProperties.Rows.Count - 1).Cells(1).Style.ForeColor = DisabledColour
                .Cells(1).Style.ForeColor = DisabledColour;
                .Cells(1).ReadOnly = true;
                'If .Cells(3) IsNot Nothing Then
                '    CType(.Cells(1), DataGridViewComboBoxCell).
                'End If
            }


        }

    }


    <SmartAssembly.Attributes.DoNotObfuscateType, SmartAssembly.Attributes.DoNotObfuscate, SmartAssembly.Attributes.DoNotPrune, SmartAssembly.Attributes.DoNotPruneType> _;
private class KeyValue
    {

        public object Value { get; set; }
        public string Display { get; set; }

        public void New(object Value, string Display = "")
        {
            If Display = "" Then Display = Value.ToString
            Me.Display = Display;
            Me.Value = Value;
        }

    }



    private void AddProperty(string sPropertyKey, string sPropertyName, object PropertyValue, ColumnType ePropertyType = ColumnType.Text, List(Of KeyValue dictValidValues)
    {

        if (TypeOf PropertyValue Is Boolean)
        {
            'listValidValues = New List(Of String)
            dictValidValues = New List(Of KeyValue)
            dictValidValues.Add(New KeyValue(true));
            dictValidValues.Add(New KeyValue(false));
            'listValidValues.Add(True.ToString)
            'listValidValues.Add(False.ToString)
            ePropertyType = ColumnType.Combo
        }

        'Dim sValidValues As String = ""
        'If listValidValues IsNot Nothing Then
        '    For Each sValue As String In listValidValues
        '        sValidValues &= sValue & "|"
        '    Next
        '    If sValidValues.EndsWith("|") Then sValidValues = sValidValues.Substring(0, sValidValues.Length - 1)
        'End If

        private int iIndex = grdProperties.Rows.Add(sPropertyName, PropertyValue.ToString, CInt(ePropertyType), dictValidValues, sPropertyKey);
        private DataGridViewRow row = null;

        if (iIndex > -1)
        {
            row = grdProperties.Rows(iIndex)
            if (ePropertyType = ColumnType.Number)
            {
                row.Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }



        if (row IsNot null)
        {
            if (dictValidValues IsNot null)
            {
                private New DataGridViewComboBoxCell cellCombo;
                cellCombo.Value = PropertyValue;
                cellCombo.DataSource = dictValidValues;
                cellCombo.DisplayMember = "Display";
                cellCombo.ValueMember = "Value";
                row.Cells(1) = cellCombo;
            }

            row.Cells(0).Style.BackColor = SystemColors.Control;
        }

    }


    private void treeItemBrowser_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
        RefreshFromKey(e.Node.Name);
    }


    private void RefreshFromKey(string sKey)
    {

        grdProperties.Rows.Clear();
        grdProperties.Tag = sKey;

        switch (Adventure.GetTypeFromKeyNice(sKey))
        {
            case "Location":
                {
                With Adventure.htblLocations(sKey);
                    AddProperty("@SEEN", "Seen", .SeenBy(Adventure.Player.Key));
                    For Each p As clsProperty In .htblProperties.Values
                        AddProperty(p);
                    Next;
                }

            case "Object":
                {
                With Adventure.htblObjects(sKey);
                    AddProperty("@SEEN", "Seen", .SeenBy(Adventure.Player.Key));
                    For Each p As clsProperty In Adventure.htblObjects(sKey).htblProperties.Values
                        AddProperty(p);
                    Next;
                }

            case "Task":
                {
                With Adventure.htblTasks(sKey);
                    AddProperty("@TASKCOMPLETED", "Completed", .Completed);
                    AddProperty("@SCORED", "Scored", .Scored);
                }

            case "Character":
                {
                With Adventure.htblCharacters(sKey);
                    AddProperty("@SEEN", "Seen", .SeenBy(Adventure.Player.Key));
                    For Each p As clsProperty In .htblProperties.Values
                        AddProperty(p);
                    Next;
                }

            case "Variable":
                {
                With Adventure.htblVariables(sKey);
                    if (.Length = 1)
                    {
                        switch (.Type)
                        {
                            case clsVariable.VariableTypeEnum.Numeric:
                                {
                                AddProperty("@VALUE", "Value", .IntValue, ColumnType.Number);
                                grdProperties.Rows(grdProperties.Rows.Count - 1).Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            case clsVariable.VariableTypeEnum.Text:
                                {
                                AddProperty("@VALUE", "Value", .StringValue, ColumnType.Text);
                        }
                    Else
                        for (int i = 0; i <= .Length - 1; i++)
                        {
                             Select .Type;
                                case clsVariable.VariableTypeEnum.Numeric:
                                    {
                                    AddProperty("@VALUE_" + i, "Value [" + i + 1 + "]", .IntValue(i + 1), ColumnType.Number);
                                    grdProperties.Rows(grdProperties.Rows.Count - 1).Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                case clsVariable.VariableTypeEnum.Text:
                                    {
                                    AddProperty("@VALUE_" + i, "Value [" + i + 1 + "]", .StringValue(i + 1), ColumnType.Text);
                            }
                        Next;
                    }
                }

            case "Event":
                {
                With Adventure.htblEvents(sKey);
                    private New List<KeyValue> dictStatuses;
                    dictStatuses.Add(New KeyValue(clsEvent.StatusEnum.CountingDownToStart, "Counting Down"));
                    dictStatuses.Add(New KeyValue(clsEvent.StatusEnum.Finished, "Finished"));
                    dictStatuses.Add(New KeyValue(clsEvent.StatusEnum.NotYetStarted, "! Yet Started"));
                    dictStatuses.Add(New KeyValue(clsEvent.StatusEnum.Paused, "Paused"));
                    dictStatuses.Add(New KeyValue(clsEvent.StatusEnum.Running, "Running"));
                    AddProperty("@STATUS", "Status", .Status, ColumnType.Combo, dictStatuses);
                    AddProperty("@TIMER", "Timer", .TimerFromStartOfEvent, ColumnType.Number);
                }

        }

        SplitContainer2.Panel2Collapsed = ! grdProperties.Rows.Count > 0;

    }

    private void grdProperties_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {

        if (e.ColumnIndex = 1 && e.RowIndex > -1 && e.RowIndex < grdProperties.Rows.Count)
        {
            If grdProperties.Rows(e.RowIndex).Cells(1).Style.ForeColor = DisabledColour Then e.Cancel = true
        }

    }


    private void grdProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {

        try
        {
            if (grdProperties.Tag IsNot null)
            {
                private string sKey = SafeString(grdProperties.Tag);
                private DataGridViewRow row = grdProperties.Rows(e.RowIndex);
                private string sPropKey = row.Cells(4).Value.ToString;

                switch (Adventure.GetTypeFromKeyNice(sKey))
                {
                    case "Location":
                        {
                        With Adventure.htblLocations(sKey);
                            switch (sPropKey)
                            {
                                case "@SEEN":
                                    {
                                    .SeenBy(Adventure.Player.Key) = SafeBool(row.Cells(1).Value);
                                default:
                                    {
                                    .htblProperties(sPropKey).Value = row.Cells(1).Value.ToString;
                            }
                        }

                    case "Object":
                        {
                        With Adventure.htblObjects(sKey);
                            switch (sPropKey)
                            {
                                case "@SEEN":
                                    {
                                    .SeenBy(Adventure.Player.Key) = SafeBool(row.Cells(1).Value);
                                default:
                                    {
                                    .htblProperties(sPropKey).Value = row.Cells(1).Value.ToString;
                            }
                        }

                    case "Task":
                        {
                        With Adventure.htblTasks(sKey);
                            switch (sPropKey)
                            {
                                case "@TASKCOMPLETED":
                                    {
                                    .Completed = SafeBool(row.Cells(1).Value);
                                case "@SCORED":
                                    {
                                    .Scored = SafeBool(row.Cells(1).Value);
                                default:
                                    {
                            }
                        }

                    case "Character":
                        {
                        With Adventure.htblCharacters(sKey);
                            switch (sPropKey)
                            {
                                case "@SEEN":
                                    {
                                    .SeenBy(Adventure.Player.Key) = SafeBool(row.Cells(1).Value);
                                default:
                                    {
                                    .htblProperties(sPropKey).Value = row.Cells(1).Value.ToString;
                                    switch (sPropKey)
                                    {
                                        case "CharacterAtLocation":
                                            {
                                            private New clsCharacterLocation(Adventure.htblCharacters(sKey)) dest;

                                            ' Default new destination to current location
                                            dest.ExistWhere = .Location.ExistWhere;
                                            dest.Position = .Location.Position;
                                            dest.Key = row.Cells(1).Value.ToString;

                                            .Move(dest);
                                    }
                            }
                        }

                    case "Variable":
                        {
                        With Adventure.htblVariables(sKey);
                            switch (sPropKey)
                            {
                                case "@VALUE":
                                    {
                                    if (row.Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleRight)
                                    {
                                        .IntValue = SafeInt(row.Cells(1).Value);
                                    Else
                                        .StringValue = SafeString(row.Cells(1).Value);
                                    }
                                default:
                                    {
                                    if (sPropKey.StartsWith("@VALUE_"))
                                    {
                                        private int iIndex = SafeInt(sPropKey.Replace("@VALUE_", "")) + 1;
                                        if (row.Cells(1).Style.Alignment = DataGridViewContentAlignment.MiddleRight)
                                        {
                                            .IntValue(iIndex) = SafeInt(row.Cells(1).Value);
                                        Else
                                            .StringValue(iIndex) = SafeString(row.Cells(1).Value);
                                        }
                                    }
                            }
                        }

                    case "Event":
                        {
                        With Adventure.htblEvents(sKey);
                            switch (sPropKey)
                            {
                                case "@STATUS":
                                    {
                                    .Status = (clsEvent.StatusEnum)(row.Cells(1).Value);
                                case "@TIMER":
                                    {
                                    .TimerToEndOfEvent = Adventure.htblEvents(sKey).Length.Value - SafeInt(row.Cells(1).Value);
                                default:
                                    {
                            }
                        }

                }
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error setting property value", ex);
        }

    }


    private void grdProperties_SelectionChanged(object sender, EventArgs e)
    {
        For Each cell As DataGridViewCell In grdProperties.SelectedCells
            If cell.ColumnIndex = 0 || (cell.ColumnIndex = 1 && cell.Style.ForeColor = DisabledColour) Then cell.Selected = false
        Next;
    }


    private void grdProperties_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
    {
        if (e.Column.Index = 1)
        {
            e.SortResult = SafeString(e.CellValue1).CompareTo(SafeString(e.CellValue2));
            e.Handled = true;
        }
    }

}
}