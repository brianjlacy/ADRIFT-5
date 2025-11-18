using Infragistics.Win.UltraWinListView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


public class Folder
{

    Private WithEvents tmrActivate As New Timer
    Public Event LoadedFolder(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ActiveChanged(ByVal sender As Object, ByVal bActive As Boolean)
    Public Event ViewTypeChanged(ByVal sender As Object, ByVal Type As UltraListViewStyle)
    Public Event MembersChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public WithEvents folder As clsFolder
    public string sLastKey = "";
    public DateTime dtLoaded = Date.MinValue;
    public string sParentKey;

    public int SortColumn = -1;
    public Infragistics.Win.UltraWinListView.Sorting SortDirection = Sorting.None;

    private bool bMouseInFolder = false;
    private bool bRenaming = false;


    Public WriteOnly Property HideLibrary() As Boolean
set(ByVal Boolean)
            lstContents.BeginUpdate();
            For Each itm As UltraListViewItem In lstContents.Items
                if (value)
                {
                    private clsItem itmGen = Adventure.dictAllItems(itm.SubItems(3).Text);
                    if (itmGen.IsLibrary && (TypeOf itmGen Is clsTask || TypeOf itmGen Is clsProperty))
                    {
                        itm.Visible = false;
                    }
                Else
                    itm.Visible = true;
                }
            Next;
            lstContents.EndUpdate();
        }
    }


    public void AddSingleItem(string sItemKey)
    {
        If ! lstContents.Items.Exists(sItemKey) Then lstContents.Items.Add(CreateItem(sItemKey))
        If folder != null && ! folder.Members.Contains(sItemKey) Then folder.Members.Add(sItemKey)
    }


    public void RemoveCutItems(ref nodes As Generic.List(Of Infragistics.Win.UltraWinTree.UltraTreeNode)
    {

        nodes = New Generic.List(Of Infragistics.Win.UltraWinTree.UltraTreeNode)

        for (int i = lstContents.Items.Count - 1; i <= 0; i += -1)
        {
            if (lstContents.Items(i).Appearance.AlphaLevel = 128)
            {
                private string sItemKey = lstContents.Items(i).SubItems(3).Text;
                Adventure.dictAllItems.Remove(sItemKey);
                folder.Members.Remove(sItemKey);
                lstContents.Items.RemoveAt(i);
                private Infragistics.Win.UltraWinTree.UltraTreeNode treenode = fGenerator.FolderList1.treeFolders.GetNodeByKey(sItemKey);
                If treenode != null Then treenode.Parent.Nodes.Remove(treenode)
                nodes.Add(treenode);
            }
        Next;

    }


    public void UnCutItems()
    {
        For Each item As UltraListViewItem In lstContents.Items
            item.Appearance.AlphaLevel = 255;
        Next;
    }


    public void CutSingleItem(string sItemKey)
    {
        if (folder.Members.Contains(sItemKey))
        {
            For Each item As UltraListViewItem In lstContents.Items
                if (item.SubItems(3).Text = sItemKey)
                {
                    item.Appearance.AlphaLevel = 128;
                    Exit For;
                }
            Next;
        }
    }


    public void RemoveSingleItem(string sItemKey)
    {
        if (folder.Members.Contains(sItemKey))
        {
            For Each item As UltraListViewItem In lstContents.Items
                if (item.SubItems(3).Text = sItemKey)
                {
                    lstContents.Items.Remove(item);
                    Exit For;
                }
            Next;
            folder.Members.Remove(sItemKey);
        }
    }


    private UltraListViewItem CreateItem(string sItemKey)
    {

        private clsItem itemGen = Adventure.GetItemFromKey(sItemKey);
        If itemGen == null Then Return null

        Dim subitems() As UltraListViewSubItem = {New UltraListViewSubItem, New UltraListViewSubItem, New UltraListViewSubItem, New UltraListViewSubItem, New UltraListViewSubItem}
        subitems(0).Value = itemGen.Created;
        subitems(1).Value = itemGen.LastUpdated;
        subitems(2).Value = Adventure.GetTypeFromKeyNice(itemGen.Key);
        subitems(3).Value = itemGen.Key;
        subitems(4).Value = 0;

        return GetItem(itemGen, subitems);

    }


    public static UltraListViewSubItem) As UltraListViewItem GetItem(clsItem itemGen, subitems()
    {

        private New UltraListViewItem(itemGen.CommonName, subitems) item;

        item.Key = itemGen.Key;
        item.Visible = true ' by default (Tasks and Properties may not be);

        switch (true)
        {
            case TypeOf itemGen Is clsFolder:
                {
                item.Appearance.Image = My.Resources.Resources.imgFolderClosed16;
            case TypeOf itemGen Is clsLocation:
                {
                item.Appearance.Image = My.Resources.Resources.imgLocation16;
            case TypeOf itemGen Is clsObject:
                {
                if (CType(itemGen, clsObject).IsStatic)
                {
                    item.Appearance.Image = My.Resources.Resources.imgObjectStatic16;
                Else
                    item.Appearance.Image = My.Resources.Resources.imgObjectDynamic16;
                }
            case TypeOf itemGen Is clsTask:
                {
                switch (CType(itemGen, clsTask).TaskType)
                {
                    case clsTask.TaskTypeEnum.General:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgTaskGeneral16;
                    case clsTask.TaskTypeEnum.Specific:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgTaskSpecific16;
                    case clsTask.TaskTypeEnum.System:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgTaskSystem16;
                }
                If subitems.Length > 3 Then subitems(4).Value = (clsTask)(itemGen).Priority
                item.Visible = ! (itemGen.IsLibrary && SafeBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "1")));
            case TypeOf itemGen Is clsEvent:
                {
                switch (CType(itemGen, clsEvent).EventType)
                {
                    case clsEvent.EventTypeEnum.TurnBased:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgEvent16;
                    case clsEvent.EventTypeEnum.TimeBased:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgTimeEvent16;
                }
            case TypeOf itemGen Is clsCharacter:
                {
                switch (CType(itemGen, clsCharacter).CharacterType)
                {
                    case clsCharacter.CharacterTypeEnum.Player:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgPlayer16;
                    case clsCharacter.CharacterTypeEnum.NonPlayer:
                        {
                        item.Appearance.Image = My.Resources.Resources.imgCharacter16;
                }
            case TypeOf itemGen Is clsGroup:
                {
                item.Appearance.Image = My.Resources.Resources.imgGroup16;
            case TypeOf itemGen Is clsVariable:
                {
                item.Appearance.Image = My.Resources.Resources.imgVariable16;
            case TypeOf itemGen Is clsALR:
                {
                item.Appearance.Image = My.Resources.Resources.imgALR16;
            case TypeOf itemGen Is clsHint:
                {
                item.Appearance.Image = My.Resources.Resources.imgHint16;
            case TypeOf itemGen Is clsProperty:
                {
                item.Appearance.Image = My.Resources.Resources.imgProperty16;
                item.Visible = ! (itemGen.IsLibrary && SafeBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "1")));
            case TypeOf itemGen Is clsUserFunction:
                {
                item.Appearance.Image = My.Resources.Resources.imgFunction16;
            case TypeOf itemGen Is clsSynonym:
                {
                item.Appearance.Image = My.Resources.Resources.imgSynonym16;
        }

        return item;
    }

    public void LoadFolder(string sKey)
    {
        Me.lstContents.Items.Clear();

        if (Adventure.dictFolders.ContainsKey(sKey))
        {

            If folder != null Then sLastKey = folder.Key

            If folder != null Then RemoveHandler folder.ColumnChanged, AddressOf folder_ColumnChanged
            folder = Adventure.dictFolders(sKey)
            AddHandler folder.ColumnChanged, AddressOf folder_ColumnChanged;
            'lstContents.Visible = False
            lstContents.BeginUpdate();
            With folder;
                private New ArrayList itemlist;

                For Each sMember As String In .Members
                    if (Not lstContents.Items.Exists(sMember))
                    {
                        private UltraListViewItem itm = CreateItem(sMember);
                        If itm != null Then itemlist.Add(itm)
                    }
                Next;

                try
                {
                    lstContents.Items.AddRange((UltraListViewItem()(itemlist.ToArray(GetType(UltraListViewItem)))));
                }
                catch (Exception ex)
                {
                    'ErrMsg("Error adding items to list " & folder.Name, ex)
                    ' Hmm, could be a Case Sensitive issue.  Check each one...
                    For Each sMember As String In .Members
                        if (lstContents.Items.Exists(sMember))
                        {
                            if (lstContents.Items(sMember).Key <> sMember)
                            {
                                private UltraListViewItem itm = CreateItem(sMember);
                                while (lstContents.Items.Exists(itm.Key))
                                {
                                    itm.Key = itm.Key + "$";
                                }
                                If itm != null Then lstContents.Items.Add(itm)
                            }
                        Else
                            private UltraListViewItem itm = CreateItem(sMember);
                            If itm != null Then lstContents.Items.Add(itm)
                        }
                    Next;
                }

                lstContents.View = .ViewType;
                private ToolStripMenuItem mi = null;
                switch (.SortColumn)
                {
                    case 0:
                        {
                        mi = miSortName
                    case 1:
                        {
                        mi = miSortCreationDate
                    case 2:
                        {
                        mi = miSortModifiedDate
                    case 3:
                        {
                        mi = miSortType
                    case 5:
                        {
                        mi = miSortTaskPriority
                }
                If mi != null Then miSortBy_Click(mi, null)

                mi = Nothing
                switch (.SortDirection)
                {
                    case Sorting.Ascending:
                        {
                        mi = miSortAscending
                    case Sorting.Descending:
                        {
                        mi = miSortDescending
                }
                If mi != null Then miSortDirection_Click(mi, null)

                switch (.GroupBy)
                {
                    case 0:
                        {
                        miGroupByNone_Click(null, null);
                    case 1:
                        {
                        miGroupByType_Click(null, null);
                }

                switch (.GroupDirection)
                {
                    case Sorting.Ascending:
                        {
                        miGroupAscending_Click(null, null);
                    case Sorting.Descending:
                        {
                        miGroupDescending_Click(null, null);
                }

                .Visible = true;
                dtLoaded = Now
            }
            'lstContents.Visible = True
            lstContents.EndUpdate();

            folder_ColumnChanged(Me, 0, folder.ShowCreatedDate);
            folder_ColumnChanged(Me, 1, folder.ShowModifiedDate);
            folder_ColumnChanged(Me, 2, folder.ShowType);
            folder_ColumnChanged(Me, 3, folder.ShowKey);
            folder_ColumnChanged(Me, 4, folder.ShowPriority);

            if (TypeOf Me.Parent Is Infragistics.Win.UltraWinDock.DockableWindow)
            {
                private Infragistics.Win.UltraWinDock.DockableWindow parent = CType(Me.Parent, Infragistics.Win.UltraWinDock.DockableWindow);
                parent.Pane.Text = fGenerator.FolderList1.treeFolders.GetNodeByKey(sKey).FullPath;
            }

            RaiseEvent LoadedFolder(Me, New EventArgs);
        }
    }

    private void lstContents_DragLeave(object sender, System.EventArgs e)
    {
        bMouseInFolder = False
    }


    private void Folder_GotFocus(object sender, System.EventArgs e)
    {
        'fGenerator.ActiveFolder = Me
        'SetActive()
    }


    private bool bActive = false;
    public void SetActive(bool bActive = true)
    {

        Me.bActive = bActive;
        if (bActive)
        {
            For Each frm As Form In fGenerator.MdiChildren
                if (TypeOf frm Is frmFolder)
                {
                    If (frmFolder)(frm).Folder != Me Then (frmFolder)(frm).Folder.SetActive(false)
                }
            Next;
            'For Each child As frmFolder In fGenerator.MdiChildren
            '    If child.Folder IsNot Me Then child.Folder.SetActive(False)
            'Next
            fGenerator.ActiveFolder = Me;
            SetNodeInTree();
        }
        RaiseEvent ActiveChanged(Me, bActive);

    }


    private void SetNodeInTree()
    {
        if (fGenerator.FolderList1.treeFolders.SelectedNodes.Count <= 1)
        {
            private string sOldKey = "";
            If fGenerator.FolderList1.treeFolders.SelectedNodes.Count = 1 Then sOldKey = fGenerator.FolderList1.treeFolders.SelectedNodes(0).Key
            if (folder IsNot null && folder.Key <> sOldKey)
            {
                fGenerator.FolderList1.bSettingNode = true;
                If sOldKey <> "" Then fGenerator.FolderList1.treeFolders.GetNodeByKey(sOldKey).Selected = false
                If fGenerator.FolderList1.treeFolders.GetNodeByKey(folder.Key) != null Then fGenerator.FolderList1.treeFolders.GetNodeByKey(folder.Key).Selected = true
                fGenerator.FolderList1.bSettingNode = false;
            }
        }
    }


private class MenuItemComparer
    {
        Implements IComparer;

        public Integer Implements System.Collections.IComparer.Compare Compare(object x, object y)
        {
            return CType(x, ToolStripMenuItem).Text.CompareTo(CType(y, ToolStripMenuItem).Text);
        }
    }


    private void cmsFolder_Opening(System.Object sender, System.ComponentModel.CancelEventArgs e)
    {

        if (folder Is null)
        {
            e.Cancel = true;
            Exit Sub;
        }

        private Point p = MousePosition;
        private UltraListViewItem itemCursor = lstContents.ItemFromPoint(lstContents.PointToClient(p));

        miAddItem.Text = "Add Item";
        switch (true)
        {
            case folder.Name.Contains("Location"):
                {
                miAddItem.Text = "Add Location";
                miAddItem.Image = My.Resources.Resources.imgLocation16;
            case folder.Name.Contains("Object"):
                {
                miAddItem.Text = "Add Object";
                miAddItem.Image = My.Resources.Resources.imgObjectDynamic16;
            case folder.Name.Contains("Task"):
                {
                miAddItem.Text = "Add Task";
                miAddItem.Image = My.Resources.Resources.imgTaskGeneral16;
            case folder.Name.Contains("Event"):
                {
                miAddItem.Text = "Add Event";
                miAddItem.Image = My.Resources.Resources.imgEvent16;
            case folder.Name.Contains("Character"):
                {
                miAddItem.Text = "Add Character";
                miAddItem.Image = My.Resources.Resources.imgCharacter16;
            case folder.Name.Contains("Propert"):
                {
                miAddItem.Text = "Add Property";
                miAddItem.Image = My.Resources.Resources.imgProperty16;
            case folder.Name.Contains("Text Override"):
                {
                miAddItem.Text = "Add Text Override";
                miAddItem.Image = My.Resources.Resources.imgALR16;
            case folder.Name.Contains("Group"):
                {
                miAddItem.Text = "Add Group";
                miAddItem.Image = My.Resources.Resources.imgGroup16;
            case folder.Name.Contains("Variable"):
                {
                miAddItem.Text = "Add Variable";
                miAddItem.Image = My.Resources.Resources.imgVariable16;
            case folder.Name.Contains("Hint"):
                {
                miAddItem.Text = "Add Hint";
                miAddItem.Image = My.Resources.Resources.imgHint16;
            case folder.Name.Contains("Synonym"):
                {
                miAddItem.Text = "Add Synonym";
                miAddItem.Image = My.Resources.Resources.imgSynonym16;
            case folder.Name.Contains("Function"):
                {
                miAddItem.Text = "Add User Function";
                miAddItem.Image = My.Resources.Resources.imgFunction16;
        }
        miAddItem.Visible = (miAddItem.Text <> "Add Item");
        sep1.Visible = (miAddItem.Text <> "Add Item");

        if (itemCursor IsNot null && itemCursor.IsSelected)
        {
            miEdit.Visible = true;
            private bool bAddSpecific = false;
            private bool bAddObject = false;
            private bool bAddSubObject = false;

            sep4.Visible = false;
            miExportFolder.Visible = false;

            if (lstContents.SelectedItems.Count = 1)
            {
                private string sItemKey = "";
                If lstContents.SelectedItems(0).SubItems.Count > 3 Then sItemKey = lstContents.SelectedItems(0).SubItems(3).Text

                if (Adventure.htblLocations.ContainsKey(sItemKey))
                {
                    ' Add object to location
                    miAddSpecificTask.DropDownItems.Clear();
                    private New ArrayList arlMenuItems;
                    private clsLocation loc = Adventure.htblLocations(sItemKey);
                    private New ToolStripMenuItem("Player enters " & Adventure.GetNameFromKey(sItemKey), Nothing, AddressOf miAddSpecificTaskForLocation) miPEL;
                    miPEL.Tag = "PlayerEntersLocation";
                    arlMenuItems.Add(miPEL);
                    For Each tas As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.GeneralAndOverrideableSpecificTasks).Values
                        if (Not tas.PreventOverriding)
                        {
                            For Each s As String In tas.References
                                if (s = "%location%")
                                {
                                    private string sDesc = tas.MakeNice.Replace("%location%", loc.ShortDescription.ToString);
                                    sDesc = sDesc.Replace("%object%", "an object").Replace("%object1%", "an object").Replace("%objects%", "an object").Replace("%character%", "a character").Replace("%character1%", "a character").Replace("%characters%", "a character").Replace("%item%", "an item")
                                    for (int i = 2; i <= 5; i++)
                                    {
                                        sDesc = sDesc.Replace("%object" & i & "%", "another object")
                                        sDesc = sDesc.Replace("%character" & i & "%", "another character")
                                    Next;
                                    sDesc = sDesc.Replace("%text%", "something")

                                    private New ToolStripMenuItem(sDesc, Nothing, AddressOf miAddSpecificTaskForLocation) mi;
                                    mi.Tag = tas.Key;
                                    arlMenuItems.Add(mi);
                                }
                            Next;
                        }
                    Next;
                    arlMenuItems.Sort(New MenuItemComparer);
                    miAddSpecificTask.DropDownItems.AddRange((ToolStripMenuItem()(arlMenuItems.ToArray(GetType(ToolStripMenuItem)))));
                    bAddSpecific = miAddSpecificTask.DropDownItems.Count > 0
                    If loc != null Then bAddObject = true
                ElseIf Adventure.htblTasks.ContainsKey(sItemKey) Then
                    ' Add Specific task from General task
                    private clsTask tas = Adventure.htblTasks(sItemKey);
                    If tas != null Then bAddSpecific = tas.TaskType = clsTask.TaskTypeEnum.General
                ElseIf Adventure.htblObjects.ContainsKey(sItemKey) Then
                    ' Add Specific tasks from Objects
                    miAddSpecificTask.DropDownItems.Clear();
                    private New ArrayList arlMenuItems;
                    private clsObject ob = Adventure.htblObjects(sItemKey);
                    'For Each tas As clsTask In Adventure.htblTasks.Values
                    For Each tas As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.GeneralAndOverrideableSpecificTasks).Values
                        If ! tas.PreventOverriding Then ' tas.TaskType = clsTask.TaskTypeEnum.General &&
                            For Each s As String In tas.References
                                if (s = "%object1%" || s = "%object%" || s = "%objects%")
                                {
                                    private New Dictionary<string, clsItem> refs;
                                    refs.Add(s.Replace("%object", "ReferencedObject").Replace("%", ""), ob);
                                    if (PassRestrictions(tas.arlRestrictions, refs, tas))
                                    {
                                        private string sDesc = tas.MakeNice.Replace("%object1%", ob.FullName).Replace("%object%", ob.FullName).Replace("%objects%", ob.FullName);
                                        sDesc = sDesc.Replace("%character%", "a character").Replace("%character1%", "a character").Replace("%characters%", "a character").Replace("%location%", "a location")
                                        for (int i = 2; i <= 5; i++)
                                        {
                                            sDesc = sDesc.Replace("%object" & i & "%", "another object")
                                            sDesc = sDesc.Replace("%character" & i & "%", "another character")
                                        Next;
                                        sDesc = sDesc.Replace("%text%", "something")

                                        private New ToolStripMenuItem(sDesc, Nothing, AddressOf miAddSpecificTaskForObject) mi;
                                        mi.Tag = tas.Key;
                                        arlMenuItems.Add(mi);
                                    }
                                }
                            Next;
                        }
                    Next;
                    arlMenuItems.Sort(New MenuItemComparer);
                    miAddSpecificTask.DropDownItems.AddRange((ToolStripMenuItem()(arlMenuItems.ToArray(GetType(ToolStripMenuItem)))));
                    bAddSpecific = miAddSpecificTask.DropDownItems.Count > 0
                    If ob != null Then bAddSubObject = true
                ElseIf Adventure.htblCharacters.ContainsKey(sItemKey) Then
                    ' Add Specific tasks from Objects
                    miAddSpecificTask.DropDownItems.Clear();
                    private New ArrayList arlMenuItems;
                    private clsCharacter ch = Adventure.htblCharacters(sItemKey);

                    For Each tas As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.GeneralAndOverrideableSpecificTasks).Values
                        if (Not tas.PreventOverriding)
                        {
                            For Each s As String In tas.References
                                if (s = "%character1%" || s = "%character%" || s = "%characters%")
                                {
                                    private string sDesc = tas.MakeNice.Replace("%character1%", ch.Name).Replace("%character%", ch.Name).Replace("%characters%", ch.Name);
                                    sDesc = sDesc.Replace("%object%", "an object").Replace("%object1%", "an object").Replace("%objects%", "an object").Replace("%location%", "a location")
                                    for (int i = 2; i <= 5; i++)
                                    {
                                        sDesc = sDesc.Replace("%object" & i & "%", "another object")
                                        sDesc = sDesc.Replace("%character" & i & "%", "another character")
                                    Next;
                                    sDesc = sDesc.Replace("%text%", "something")

                                    private New ToolStripMenuItem(sDesc, Nothing, AddressOf miAddSpecificTaskForCharacter) mi;
                                    mi.Tag = tas.Key;
                                    arlMenuItems.Add(mi);
                                }
                            Next;
                        }
                    Next;
                    arlMenuItems.Sort(New MenuItemComparer);
                    miAddSpecificTask.DropDownItems.AddRange((ToolStripMenuItem()(arlMenuItems.ToArray(GetType(ToolStripMenuItem)))));
                    bAddSpecific = miAddSpecificTask.DropDownItems.Count > 0
                    If ch != null Then bAddSubObject = true
                ElseIf Adventure.dictFolders.ContainsKey(sItemKey) Then
                    private bool bExportVisible = Not fGenerator.SimpleMode && Adventure.dictFolders.ContainsKey(sItemKey);
                    sep4.Visible = bExportVisible;
                    miExportFolder.Visible = bExportVisible;
                    miExportFolder.Tag = sItemKey;
                }
            }
            miAddSpecificTask.Visible = bAddSpecific;
            miAddObjectToLocation.Visible = bAddObject;
            miAddSubObject.Visible = bAddSubObject;
            sep1.Visible = true;
            miEdit.Enabled = lstContents.SelectedItems.Count = 1;
            miCut.Visible = true;
            miCopy.Visible = true;
            miDelete.Visible = true;
            miRename.Visible = true;
            miRename.Enabled = lstContents.SelectedItems.Count = 1 && lstContents.SelectedItems(0).SubItems(2).Text <> "Object";
            miView.Visible = false;
            miSortBy.Visible = false;
            miGroupBy.Visible = false;
            sep2.Visible = false;
            miPaste.Visible = false;
            miNew.Visible = false;
        Else
            ' Display the list items
            miEdit.Visible = false;
            miAddSpecificTask.Visible = false;
            if (lstContents.SelectedItems.Count > 0)
            {
                miCut.Visible = true;
                miDelete.Visible = true;
                miCopy.Visible = true;
            Else
                miCut.Visible = false;
                miDelete.Visible = false;
                miCopy.Visible = false;
            }
            miRename.Visible = false;
            miView.Visible = true;
            miSortBy.Visible = true;
            miGroupBy.Visible = true;
            miPaste.Visible = true;
            sep2.Visible = true;
            miPaste.Enabled = CopiedItems != null && CopiedItems.Count > 0;
            miNew.Visible = true;
            miExportFolder.Visible = ! fGenerator.SimpleMode;
            sep4.Visible = ! fGenerator.SimpleMode;
            miExportFolder.Tag = "";
        }
        'miDelete.Enabled = False ' for now

    }


    private void SetViewCheck(Infragistics.Win.UltraWinListView.UltraListViewStyle ViewType)
    {

        switch (ViewType)
        {
            case UltraListViewStyle.Details:
                {
                miViewType_Click(miDetails, null);
            case UltraListViewStyle.Icons:
                {
                miViewType_Click(miIcons, null);
            case UltraListViewStyle.List:
                {
                miViewType_Click(miList, null);
                'Case UltraListViewStyle.Thumbnails
                '    miViewType_Click(miThumbnails, Nothing)
                'Case UltraListViewStyle.Tiles
                '    miViewType_Click(miTiles, Nothing)
        }

    }

    private void miViewType_Click(object sender, System.EventArgs e)
    {

        'miTiles.Checked = False
        miDetails.Checked = false;
        miList.Checked = false;
        'miThumbnails.Checked = False
        miIcons.Checked = false;
        (ToolStripMenuItem)(sender).Checked = true;

        private Infragistics.Win.UltraWinListView.UltraListViewStyle View = UltraListViewStyle.Icons;
        switch (true)
        {
            'Case sender Is miTiles
            '    View = UltraListViewStyle.Tiles
            case sender Is miDetails:
                {
                View = UltraListViewStyle.Details
            case sender Is miList:
                {
                View = UltraListViewStyle.List
                'Case sender Is miThumbnails
                '    View = UltraListViewStyle.Thumbnails
            case sender Is miIcons:
                {
                View = UltraListViewStyle.Icons
        }

        folder.ViewType = View;
        lstContents.View = View;
        RaiseEvent ViewTypeChanged(Me, View);

    }


    public void RemoveSelectedItems()
    {

        if (MessageBox.Show("Are you sure you wish to remove th" + IIf(lstContents.SelectedItems.Count = 1, "is item", "ese items").ToString + "?", "Remove Items from Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
        {
            for (int i = lstContents.Items.Count - 1; i <= 0; i += -1)
            {
                if (lstContents.Items(i).IsSelected)
                {
                    private clsItem item = Adventure.dictAllItems(lstContents.Items(i).SubItems(3).Text);

                    if (TypeOf item Is clsObject)
                    {
                        private clsObject ob = CType(item, clsObject);
                        private New clsObjectLocation loc;
                        if (ob.IsStatic)
                        {
                            loc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                        Else
                            loc.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                        }
                        loc.Key = "";
                        ob.Move(loc);
                    ElseIf TypeOf item == clsCharacter Then
                        private clsCharacter ch = CType(item, clsCharacter);
                        private New clsCharacterLocation(ch) loc;
                        loc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                        loc.Key = "";
                        ch.Move(loc);
                    }

                    lstContents.Items.RemoveAt(i);
                    Adventure.Changed = true;
                }
            Next;
        }

    }


    public UltraListViewStyle View
        {
            get
            {
            return lstContents.View;
        }
set(UltraListViewStyle)
            lstContents.View = value;
            SortColumn = 0
            SortDirection = Sorting.Ascending
            lstContents.MainColumn.Sorting = SortDirection;
            lstContents.Items.RefreshSort(true);
        }
    }


    private void miEdit_Click(System.Object sender, System.EventArgs e)
    {
        Edit();
    }


    public Generic.List<string> SelectedKeys()
    {
        private New Generic.List<string> sKeys;
        For Each item As UltraListViewItem In lstContents.SelectedItems
            sKeys.Add(item.SubItems(3).Text);
        Next;
        return sKeys;
    }

    private void miCut_Click(System.Object sender, System.EventArgs e)
    {
        CutItems(SelectedKeys);
    }

    private void miCopy_Click(System.Object sender, System.EventArgs e)
    {
        CopyItems(SelectedKeys);
    }

    private void miDelete_Click(System.Object sender, System.EventArgs e)
    {
        DeleteItems(SelectedKeys);
    }

    private void miRename_Click(System.Object sender, System.EventArgs e)
    {
        If lstContents.ActiveItem != null Then Rename(lstContents.ActiveItem)
    }

    private void miPaste_Click(System.Object sender, System.EventArgs e)
    {
        PasteItems();
    }

    private void miSortBy_Click(System.Object sender, System.EventArgs e)
    {

        if (SortDirection = Sorting.None)
        {
            SortDirection = Sorting.Ascending
            miSortAscending.Checked = true;
        }

        switch (true)
        {
            case sender Is miSortName:
                {
                lstContents.MainColumn.Sorting = SortDirection;
                SortColumn = 0
            case sender Is miSortCreationDate:
                {
                lstContents.SubItemColumns(0).Sorting = SortDirection;
                SortColumn = 1
            case sender Is miSortModifiedDate:
                {
                lstContents.SubItemColumns(1).Sorting = SortDirection;
                SortColumn = 2
            case sender Is miSortType:
                {
                lstContents.SubItemColumns(2).Sorting = SortDirection;
                SortColumn = 3
            case sender Is miSortTaskPriority:
                {
                lstContents.SubItemColumns(4).Sorting = SortDirection;
                SortColumn = 5
        }
        lstContents.Items.RefreshSort(true);

        miSortByNone.Visible = true;
        miSortByNone.Checked = false;
        miSortAscending.Enabled = true;
        miSortDescending.Enabled = true;
        miSortName.Checked = false;
        miSortCreationDate.Checked = false;
        miSortModifiedDate.Checked = false;
        miSortTaskPriority.Checked = false;
        miSortType.Checked = false;
        (ToolStripMenuItem)(sender).Checked = true;

        folder.SortColumn = SortColumn;

    }


    private void miSortDirection_Click(System.Object sender, System.EventArgs e)
    {

        switch (true)
        {
            case sender Is miSortAscending:
                {
                SortDirection = Sorting.Ascending
                miSortByNone.Visible = true;
            case sender Is miSortDescending:
                {
                SortDirection = Sorting.Descending
                miSortByNone.Visible = true;
            case sender Is miSortByNone:
                {
                SortDirection = Sorting.None
                lstContents.MainColumn.Sorting = SortDirection;
                SortColumn = -1
                miSortName.Checked = false;
                miSortCreationDate.Checked = false;
                miSortModifiedDate.Checked = false;
                miSortTaskPriority.Checked = false;
                miSortType.Checked = false;
                miSortByNone.Visible = false;
                miSortAscending.Enabled = false;
                miSortDescending.Enabled = false;
        }

        if (SortColumn > -1)
        {
            if (SortColumn = 0)
            {
                lstContents.MainColumn.Sorting = SortDirection;
            Else
                lstContents.SubItemColumns(SortColumn - 1).Sorting = SortDirection;
            }
            lstContents.Items.RefreshSort(true);
        }

        miSortAscending.Checked = false;
        miSortDescending.Checked = false;
        (ToolStripMenuItem)(sender).Checked = true;

        folder.SortDirection = SortDirection;

    }


    private void Rename(UltraListViewItem lvi)
    {
        lvi.BeginEdit();
    }


    public void Edit(string sEditType = "", clsTask tasGeneral = null, string sKey = "")
    {

        If lstContents.ActiveItem == null && sEditType = "" Then Exit Sub

        if (sEditType = "")
        {
            sEditType = lstContents.ActiveItem.SubItems("sType").Text
            sKey = lstContents.ActiveItem.Key '.SubItems("sKey").Text
            while (sKey.EndsWith("$"))
            {
                sKey = ChopLast(sKey)
            }
        }


        switch (sEditType)
        {
            case "Folder":
                {
                'For Each child As frmFolder In fGenerator.MdiChildren
                '    If child.Folder.folder.Key = sKey Then
                '        child.Folder.SetActive()
                '        Exit Sub
                '    End If
                'Next
                'folder.Visible = False
                'LoadFolder(sKey)
                'SetNodeInTree()
                fGenerator.FolderList1.OpenNewFolder(sKey);
            case "Location":
                {
                private clsLocation loc;
                If sKey <> "" Then loc = Adventure.htblLocations(sKey) Else loc = New clsLocation
                loc.EditItem();
            case "Object":
                {
                private clsObject ob;
                If sKey <> "" Then ob = Adventure.htblObjects(sKey) Else ob = New clsObject
                ob.EditItem();
            case "Task":
                {
                private clsTask tas;
                If sKey <> "" Then tas = Adventure.htblTasks(sKey) Else tas = New clsTask
                if (tasGeneral IsNot null)
                {
                    tas.TaskType = clsTask.TaskTypeEnum.Specific;
                    tas.GeneralKey = tasGeneral.Key;
                    ReDim tas.Specifics(-1);
                }
                tas.EditItem();
            case "Event":
                {
                private clsEvent ev;
                If sKey <> "" Then ev = Adventure.htblEvents(sKey) Else ev = New clsEvent
                ev.EditItem();
            case "Character":
                {
                private clsCharacter ch;
                If sKey <> "" Then ch = Adventure.htblCharacters(sKey) Else ch = New clsCharacter
                ch.EditItem();
            case "Variable":
                {
                private clsVariable var;
                If sKey <> "" Then var = Adventure.htblVariables(sKey) Else var = New clsVariable
                var.EditItem();
            case "Hint":
                {
                private clsHint hint;
                If sKey <> "" Then hint = Adventure.htblHints(sKey) Else hint = New clsHint
                hint.EditItem();
            case "Group":
                {
                private clsGroup grp;
                If sKey <> "" Then grp = Adventure.htblGroups(sKey) Else grp = New clsGroup
                grp.EditItem();
            case "Property":
                {
                private clsProperty prop;
                If sKey <> "" Then prop = Adventure.htblAllProperties(sKey) Else prop = New clsProperty
                prop.EditItem();
            case "Text Override":
                {
                private clsALR ALR;
                If sKey <> "" Then ALR = Adventure.htblALRs(sKey) Else ALR = New clsALR
                ALR.EditItem();
            case "User Function":
                {
                private clsUserFunction UDF;
                If sKey <> "" Then UDF = Adventure.htblUDFs(sKey) Else UDF = New clsUserFunction
                UDF.EditItem();
            case "Synonym":
                {
                private clsSynonym Synonym;
                If sKey <> "" Then Synonym = Adventure.htblSynonyms(sKey) Else Synonym = New clsSynonym
                Synonym.EditItem();
            default:
                {
                TODO();
        }

    }

    private void miNewFolder_Click(System.Object sender, System.EventArgs e)
    {

        private New clsFolder newFolder;
        newFolder.Key = newFolder.GetNewKey ' Adventure.GetNewKey("Folder");
        newFolder.Name = "New Folder";
        Adventure.dictFolders.Add(newFolder.Key, newFolder);

        Dim subitems() As UltraListViewSubItem = {New UltraListViewSubItem, New UltraListViewSubItem, New UltraListViewSubItem, New UltraListViewSubItem}
        subitems(0).Value = newFolder.Created;
        subitems(1).Value = newFolder.LastUpdated;
        subitems(2).Value = "Folder";
        subitems(3).Value = newFolder.Key;

        private New UltraListViewItem(newFolder.Name, subitems) itmFolder;
        itmFolder.Key = newFolder.Key;
        itmFolder.Appearance.Image = My.Resources.Resources.imgFolderClosed16;
        lstContents.Items.Add(itmFolder);

        fGenerator.FolderList1.AddFolder(newFolder, fGenerator.FolderList1.treeFolders.GetNodeByKey(folder.Key));
        folder.Members.Add(newFolder.Key);

        Rename(itmFolder);

    }

    private void miNewLocation_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmLocation(New clsLocation, True) fLocation;
    }

    private void miNewObject_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmObject(New clsObject, True) fObject;
    }

    private void miNewEvent_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmEvent(New clsEvent, True) fEvent;
    }

    private void miNewTask_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmTask(New clsTask, True) fTask;
    }

    private void miNewCharacter_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmCharacter(New clsCharacter, True) fCharacter;
    }

    private void miNewGroup_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmGroup(New clsGroup, True) fGroup;
    }

    private void miNewHint_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmHint(New clsHint, True) fHint;
    }

    private void miNewVariable_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmVariable(New clsVariable, True) fVariable;
    }

    private void miNewProperty_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmProperty(New clsProperty, True) fProperty;
    }

    private void miNewALR_Click(System.Object sender, System.EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmTextOverride(New clsALR) fALR;
    }

    private void miNewSynonym_Click(object sender, EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmSynonym(New clsSynonym) fSynonym;
    }

    private void miNewUserFunction_Click(object sender, EventArgs e)
    {
        fGenerator.sDestinationList = folder.Name;
        private New frmUserFunction(New clsUserFunction) fFunction;
    }

    private void lstContents_ItemExitedEditMode(object sender, Infragistics.Win.UltraWinListView.ItemExitedEditModeEventArgs e)
    {

        private string sKey = e.Item.Key;
        switch (Adventure.GetTypeFromKeyNice(sKey))
        {
            case "Folder":
                {
                Adventure.dictFolders(sKey).Name = e.Item.Text;
                fGenerator.FolderList1.treeFolders.GetNodeByKey(sKey).Text = e.Item.Text;
            case "Location":
                {
                Adventure.htblLocations(sKey).ShortDescription = New Description(e.Item.Text);
                'Case "Object"
                '    Adventure.htblObjects(sKey).
            case "Task":
                {
                Adventure.htblTasks(sKey).Description = e.Item.Text;
            case "Event":
                {
                Adventure.htblEvents(sKey).Description = e.Item.Text;
            case "Character":
                {
                Adventure.htblCharacters(sKey).ProperName = e.Item.Text;
            case "Property":
                {
                Adventure.htblAllProperties(sKey).Description = e.Item.Text;
            case "Variable":
                {
                Adventure.htblVariables(sKey).Name = e.Item.Text;
            case "Hint":
                {
                Adventure.htblHints(sKey).Question = e.Item.Text;
            case "Group":
                {
                Adventure.htblGroups(sKey).Name = e.Item.Text;
            case "Text Override":
                {
                Adventure.htblALRs(sKey).OldText = e.Item.Text;
            default:
                {
                TODO();
        }
        bRenaming = False

    }

    private void miGroupByType_Click(System.Object sender, System.EventArgs e)
    {

        lstContents.Groups.Clear();
        For Each sType As String In New String() {"Folders", "Locations", "Objects - Static", "Objects - Dynamic", "Tasks - System", "Tasks - General", "Tasks - Specific", "Events", "Characters", "Groups", "Properties", "Variables", "Text Replacements"}
            private New UltraListViewGroup grp;
            grp.Text = sType;
            lstContents.Groups.Add(grp);
            For Each itm As UltraListViewItem In lstContents.Items
                switch (sType)
                {
                    case "Folders":
                    case "Locations":
                    case "Events":
                    case "Characters":
                    case "Groups":
                    case "Variables":
                        {
                        If itm.SubItems(2).Text + "s" = sType Then itm.Group = grp
                    case "Objects - Static":
                        {
                        If itm.SubItems(2).Text = "Object" && Adventure.htblObjects(itm.SubItems(3).Text).IsStatic Then itm.Group = grp
                    case "Objects - Dynamic":
                        {
                        If itm.SubItems(2).Text = "Object" && ! Adventure.htblObjects(itm.SubItems(3).Text).IsStatic Then itm.Group = grp
                    case "Tasks - System":
                        {
                        If itm.SubItems(2).Text = "Task" && Adventure.htblTasks(itm.SubItems(3).Text).TaskType = clsTask.TaskTypeEnum.System Then itm.Group = grp
                    case "Tasks - General":
                        {
                        If itm.SubItems(2).Text = "Task" && Adventure.htblTasks(itm.SubItems(3).Text).TaskType = clsTask.TaskTypeEnum.General Then itm.Group = grp
                    case "Tasks - Specific":
                        {
                        If itm.SubItems(2).Text = "Task" && Adventure.htblTasks(itm.SubItems(3).Text).TaskType = clsTask.TaskTypeEnum.Specific Then itm.Group = grp
                    case "Properties":
                        {
                        If itm.SubItems(2).Text = "Property" Then itm.Group = grp
                    case "Text Replacements":
                        {
                        If itm.SubItems(2).Text = "ALR" Then itm.Group = grp
                    default:
                        {
                        TODO("Group type " + sType);
                }

            Next;
        Next;
        miGroupByType.Checked = true;
        miGroupByNone.Visible = true;
        miGroupAscending.Enabled = true;
        miGroupDescending.Enabled = true;

        If ! folder.GroupDirection = Sorting.Descending Then miGroupAscending_Click(null, null)

        folder.GroupBy = 1;
    }


    private void miGroupByNone_Click(System.Object sender, System.EventArgs e)
    {
        lstContents.Groups.Clear();
        miGroupByType.Checked = false;
        miGroupByNone.Visible = false;
        miGroupAscending.Enabled = false;
        miGroupDescending.Enabled = false;
        folder.GroupBy = 0;
    }

    private void miGroupAscending_Click(object sender, System.EventArgs e)
    {

        miGroupAscending.Checked = true;
        miGroupDescending.Checked = false;
        lstContents.Groups.Sort(Sorting.Ascending);
        folder.GroupDirection = Sorting.Ascending;

    }

    private void miGroupDescending_Click(object sender, System.EventArgs e)
    {

        miGroupAscending.Checked = false;
        miGroupDescending.Checked = true;
        lstContents.Groups.Sort(Sorting.Descending);
        folder.GroupDirection = Sorting.Descending;

    }


    private void folder_ColumnChanged(object sender, int iCol, bool bVisible)
    {
        if (lstContents.SubItemColumns.Count = 5)
        {
            if (bVisible)
            {
                lstContents.SubItemColumns(iCol).VisibleInDetailsView = Infragistics.Win.DefaultableBoolean.true;
            Else
                lstContents.SubItemColumns(iCol).VisibleInDetailsView = Infragistics.Win.DefaultableBoolean.false;
            }
        }
    }


#Region "Drag/Drop"

    private New Generic.List<UltraListViewItem> SelectedItems;

    private void lstContents_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        SelectedItems.Clear();
        For Each item As UltraListViewItem In lstContents.SelectedItems
            SelectedItems.Add(item);
        Next;
    }


    private Point ptStart;
    private bool bAllowedToDrag = false;
    private void lstContents_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        'If e.Button = Windows.Forms.MouseButtons.Right Then
        SetActive();

        private UltraListViewItem itemCursor = lstContents.ItemFromPoint(lstContents.PointToClient(MousePosition));
        bAllowedToDrag = (itemCursor IsNot Nothing) AndAlso folder IsNot Nothing ' AndAlso SelectedItems.Contains(itemCursor)) ' itemCursor.IsSelected) ' Only go into drag mode if we re-selected a selected line
        ptStart = lstContents.PointToClient(MousePosition)

        ' If user right-clicks on the text (rather than just the item), select the item
        'If itemCursor.UIElement IsNot Nothing Then
        '    itemCursor = itemCursor
        'End If
        if (itemCursor IsNot null && Not itemCursor.IsSelected && lstContents.PointToClient(MousePosition).X < lstContents.Width / 2)
        {
            lstContents.SelectedItems.Clear();
            if (lstContents.Items.Contains(itemCursor))
            {
                lstContents.SelectedItems.Add(itemCursor);
                lstContents.ActiveItem = itemCursor;
            }
        }

        'End If

    }


    private void SetHotTrackColours()
    {
        private UltraListViewItem itemCursor = lstContents.ItemFromPoint(lstContents.PointToClient(MousePosition));
        if (itemCursor IsNot null && itemCursor.IsSelected)
        {
            ' Hot-tracking over a selected item
            lstContents.ItemSettings.HotTrackingAppearance.BackColor = Color.FromArgb(229, 245, 253);
            lstContents.ItemSettings.HotTrackingAppearance.BackColor2 = Color.FromArgb(201, 234, 250);
        Else
            ' Normal hot-tracking
            lstContents.ItemSettings.HotTrackingAppearance.BackColor = Color.FromArgb(247, 252, 254);
            lstContents.ItemSettings.HotTrackingAppearance.BackColor2 = Color.FromArgb(234, 246, 253);
        }
    }


    private void lstContents_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        SetHotTrackColours();

        'Dim itemCursor As UltraListViewItem = lstContents.ItemFromPoint(lstContents.PointToClient(MousePosition))
        if (e.Button = Windows.Forms.MouseButtons.Left && bAllowedToDrag)
        {
            ' This line prevents the DoubleClick event from firing...
            private Point ptNow = lstContents.PointToClient(MousePosition);
            ' Make sure mouse has moved at least 3 pixels before starting drag
            if (Math.Abs(ptStart.X - ptNow.X) > 3 || Math.Abs(ptStart.Y - ptNow.Y) > 3)
            {
                lstContents.DoDragDrop(lstContents.SelectedItems, DragDropEffects.Move | DragDropEffects.Scroll);
            }
        }

    }


    private void tmrActivate_Tick(object sender, System.EventArgs e)
    {
        tmrActivate.Stop();
        ' Only activate the form if the mouse is still inside it
        'Dim ptCursor As Point = lstContents.PointToClient(MousePosition)
        if (MouseButtons <> Windows.Forms.MouseButtons.None && bMouseInFolder)
        {
            'AndAlso ptCursor.X > 0 AndAlso ptCursor.X < lstContents.Width _
            'AndAlso ptCursor.Y > 0 AndAlso ptCursor.Y < lstContents.Height Then
            If TypeOf Me.Parent == frmFolder Then (frmFolder)(Me.Parent).Activate()
        }

    }


    private void lstContents_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {

        if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
        {
            With (UltraListViewSelectedItemsCollection)(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"));
                if (.Count > 0 && .Item(0).Control Is lstContents)
                {
                    ' Don't allow dragging to same list if we have a sort defined
                    if (SortDirection <> Sorting.None)
                    {
                        e.Effect = DragDropEffects.None;
                        Exit Sub;
                    }
                Else
                    ' Don't allow dragging a folder into itself, or any sub-folder thereof
                    for (int i = .Count - 1; i <= 0; i += -1)
                    {
                        private string sItemKey = .Item(i).SubItems(3).Text;
                        if (folder IsNot null && folder.Key = sItemKey)
                        {
                            e.Effect = DragDropEffects.None;
                            Exit Sub;
                        }
                        if (Adventure.dictFolders.ContainsKey(sItemKey))
                        {
                            if (Adventure.dictFolders(sItemKey).ContainsKey(folder.Key))
                            {
                                e.Effect = DragDropEffects.None;
                                Exit Sub;
                            }
                        }
                    Next;
                }
            }
            if (folder IsNot null || sParentKey <> "")
            {
                e.Effect = DragDropEffects.Move;
                tmrActivate.Interval = 750;
                tmrActivate.Start();
            }
        Else
            e.Effect = DragDropEffects.None;
        }
        bMouseInFolder = True

    }


    private void lstContents_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {

        private UltraListViewItem itemCursor = lstContents.ItemFromPoint(lstContents.PointToClient(MousePosition));
        If itemCursor == null Then Exit Sub

        itemCursor.Activate();

        ' Make sure we scroll the list if we're dragging off top or bottom
        private New Point(e.X, e.Y) ptItem;
        ptItem = lstContents.PointToClient(ptItem)

        if (ptItem.Y < lstContents.ItemSizeResolved.Height * CInt(IIf(lstContents.View = UltraListViewStyle.Details, 2, 1)) && itemCursor.Index > 0)
        {
            lstContents.Items(itemCursor.Index - 1).BringIntoView();
            Threading.Thread.Sleep(15);
        }

        if (ptItem.Y > lstContents.Height - lstContents.ItemSizeResolved.Height && itemCursor.Index < lstContents.Items.Count - 1)
        {
            lstContents.Items(itemCursor.Index + 1).BringIntoView();
            Threading.Thread.Sleep(15);
        }

    }


    private New Generic.List<UltraListViewItem> lItems;
    Private WithEvents tmrDrop As New Timer

    private void lstContents_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {

        try
        {
            private bool bShownError = false;

            if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
            {
                private UltraListViewSelectedItemsCollection items = CType(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"), UltraListViewSelectedItemsCollection);
                If items.Count = 0 Then Exit Sub
                private UltraListView lstSource = items.First.Control;
                lstSource.BeginUpdate();
                lstContents.BeginUpdate();
                If lstSource != lstContents Then lstContents.SelectedItems.Clear()
                lItems.Clear();
                for (int i = items.Count - 1; i <= 0; i += -1)
                {
                    private UltraListViewItem itm = items(i);
                    private string sItemKey = itm.SubItems(3).Text;
                    private int iOldIndex = itm.Index;

                    if (items.Count = 1 && lstContents.ActiveItem Is itm)
                    {
                        lstSource.EndUpdate();
                        lstContents.EndUpdate();
                        Exit Sub;
                    }

                    if (folder IsNot null)
                    {
                        Adventure.dictFolders((Folder)(itm.Control.Parent).folder.Key).Members.Remove(sItemKey);

                        private Infragistics.Win.UltraWinTree.UltraTreeNode treenode = fGenerator.FolderList1.treeFolders.GetNodeByKey(sItemKey);
                        If treenode != null Then treenode.Parent.Nodes.Remove(treenode)

                        itm.Control.Items.Remove(itm);
                        private int iDropIndex = 0;

                        if (lstContents.ActiveItem IsNot null)
                        {
                            iDropIndex = lstContents.ActiveItem.Index

                            ' If we are moving an item down in the same list, increase the index
                            ' by one, as we want to slot it in the item below what we're dropping on
                            If lstSource == lstContents && iOldIndex <= iDropIndex Then iDropIndex += 1

                            ' If mouse is not over the active item, but is below it,
                            ' and the active item is last in the list, add, don't insert
                            if (iDropIndex = lstContents.Items.Count - 1)
                            {
                                private UltraListViewItem itemCursor = lstContents.ItemFromPoint(lstContents.PointToClient(MousePosition));
                                If itemCursor == null Then iDropIndex += 1
                            }
                        }

                        folder.Members.Insert(iDropIndex, sItemKey);

                        'Debug.WriteLine("Inserting " & itm.Text & " at position " & iDropIndex)
                        lstContents.Items.Insert(iDropIndex, itm);
                        lItems.Add(itm);
                        if (i = 0)
                        {
                            try
                            {
                                If lstContents.Groups.Count > 0 Then miGroupByType_Click(null, null) ' Ensure it's grouped correctly
                                itm.Activate() ' Sometimes get an Object Ref error here, even when itm exists;
                            Catch
                            }
                        }

                        if (treenode IsNot null)
                        {
                            ' Convert folder drop position to tree position (i.e. ignore non-folders)
                            private int iTreeIndex = 0;
                            for (int iFolder = 0; iFolder <= iDropIndex - 1; iFolder++)
                            {
                                If Adventure.dictFolders.ContainsKey(folder.Members(iFolder)) Then iTreeIndex += 1
                            Next;
                            private Infragistics.Win.UltraWinTree.UltraTreeNode treenodedest = fGenerator.FolderList1.treeFolders.GetNodeByKey(folder.Key);
                            If treenodedest != null Then treenodedest.Nodes.Insert(iTreeIndex, treenode) ' .Add(treenode)
                        }
                    Else
                        private clsItem item = Adventure.dictAllItems(itm.SubItems(3).Text);
                        if (item IsNot null)
                        {
                            if (TypeOf item Is clsObject || TypeOf item Is clsCharacter)
                            {
                                if (Not lstContents.Items.Exists(itm.Key))
                                {
                                    private UltraListViewItem itmNew = itm.Clone(true);
                                    lstContents.Items.Add(itmNew);
                                    if (TypeOf item Is clsObject)
                                    {
                                        private clsObject ob = CType(item, clsObject);
                                        private New clsObjectLocation loc;
                                        if (ob.IsStatic)
                                        {
                                            loc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
                                        Else
                                            loc.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                        }
                                        loc.Key = sParentKey;
                                        ob.Move(loc);
                                    ElseIf TypeOf item == clsCharacter Then
                                        private clsCharacter ch = CType(item, clsCharacter);
                                        private New clsCharacterLocation(ch) loc;
                                        loc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                        loc.Key = sParentKey;
                                        ch.Move(loc);
                                    }
                                    Adventure.Changed = true;
                                Else
                                    ErrMsg("This location already contains item """ + item.CommonName + """");
                                }
                            Else
                                if (Not bShownError)
                                {
                                    ErrMsg("Only Objects and Characters may be dragged into the Location Contents window");
                                    bShownError = True
                                }
                            }
                        }
                    }

                Next;

                tmrDrop.Enabled = true;
                tmrDrop.Interval = 1;
                tmrDrop.Start();
                lstSource.EndUpdate();
                lstContents.EndUpdate();

            }
        }
        catch (Exception ex)
        {
            ErrMsg("Folder drop error", ex);
        }

    }

    private void tmrDrop_Tick(object sender, System.EventArgs e)
    {
        tmrDrop.Enabled = false;
        for (int i = lItems.Count - 1; i <= 0; i += -1)
        {
            lstContents.SelectedItems.Add(lItems(i));
        Next;
        'For Each itm As UltraListViewItem In lItems
        '    lstContents.SelectedItems.Add(itm)
        'Next
    }

#End Region


    private void lstContents_ItemSelectionChanged(object sender, Infragistics.Win.UltraWinListView.ItemSelectionChangedEventArgs e)
    {
        'Debug.WriteLine("Selection changed")
        'SetHotTrackColours()
        fGenerator.UTMMain.Tools("Cut").SharedProps.Enabled = lstContents.SelectedItems.Count > 0;
        fGenerator.UTMMain.Tools("Copy").SharedProps.Enabled = lstContents.SelectedItems.Count > 0;
        fGenerator.UTMMain.Tools("Delete").SharedProps.Enabled = lstContents.SelectedItems.Count > 0;

        For Each li As UltraListViewItem In lstContents.SelectedItems
            if (li.SubItems.Count > 2 && li.SubItems(2).Text = "Location")
            {
                fGenerator.Map1.SelectNode(li.SubItems(3).Text);
            }
        Next;

    }

    private void Folder_Load(object sender, System.EventArgs e)
    {
        'Infragistics.Win.AppStyling.StyleManager.Load("../../Windows7.isl")
    }


    private void miAddSpecificTaskForObject(object sender, System.EventArgs e)
    {

        if (lstContents.SelectedItems.Count = 1)
        {
            private string sTaskKey = CType(sender, ToolStripMenuItem).Tag.ToString;
            private string sObKey = lstContents.SelectedItems(0).SubItems(3).Text;
            private clsTask tasGeneral = Adventure.htblTasks(sTaskKey);

            private New clsTask tas;
            if (tasGeneral IsNot null)
            {
                tas.TaskType = clsTask.TaskTypeEnum.Specific;
                tas.GeneralKey = tasGeneral.Key;

                private bool bAdded = false;
                ReDim tas.Specifics(tasGeneral.References.Count - 1);
                for (int i = 0; i <= tasGeneral.References.Count - 1; i++)
                {
                    tas.Specifics(i) = New clsTask.Specific;
                    With tas.Specifics(i);
                        switch (tasGeneral.References(i))
                        {
                            case "%object%":
                            case "%object1%":
                            case "%objects%":
                                {
                                if (Not bAdded)
                                {
                                    .Type = ReferencesType.Object;
                                    .Multiple = false;
                                    .Keys.Add(sObKey);
                                    bAdded = True
                                }
                            default:
                                {
                                ' Leave bare bones
                        }
                    }
                Next;

                'ReDim tas.Specifics(0)
                'tas.Specifics(0) = New clsTask.Specific
                'With tas.Specifics(0)
                '    .Type = ReferencesType.Object
                '    .Multiple = False
                '    .Keys.Add(sObKey)
                'End With
            }
            tas.Description = (ToolStripMenuItem)(sender).Text;
            fGenerator.sDestinationList = folder.Name;
            tas.EditItem();
        }

    }


    private void miAddSpecificTaskForLocation(object sender, System.EventArgs e)
    {

        if (lstContents.SelectedItems.Count = 1)
        {
            private string sTaskKey = CType(sender, ToolStripMenuItem).Tag.ToString;
            private string sLocKey = lstContents.SelectedItems(0).SubItems(3).Text;
            private clsTask tasGeneral = Adventure.htblTasks(sTaskKey);

            private New clsTask tas;
            if (tasGeneral IsNot null)
            {
                tas.TaskType = clsTask.TaskTypeEnum.Specific;
                tas.GeneralKey = tasGeneral.Key;

                private bool bAdded = false;
                ReDim tas.Specifics(tasGeneral.References.Count - 1);
                for (int i = 0; i <= tasGeneral.References.Count - 1; i++)
                {
                    tas.Specifics(i) = New clsTask.Specific;
                    With tas.Specifics(i);
                        switch (tasGeneral.References(i))
                        {
                            case "%location%":
                                {
                                if (Not bAdded)
                                {
                                    .Type = ReferencesType.Location;
                                    .Multiple = false;
                                    .Keys.Add(sLocKey);
                                    bAdded = True
                                }
                            default:
                                {
                                ' Leave bare bones
                        }
                    }
                Next;
            ElseIf sTaskKey = "PlayerEntersLocation" Then
                tas.TaskType = clsTask.TaskTypeEnum.System;
                tas.LocationTrigger = sLocKey;
            }

            tas.Description = (ToolStripMenuItem)(sender).Text;
            fGenerator.sDestinationList = folder.Name;
            tas.EditItem();
        }

    }


    private void miAddSpecificTaskForCharacter(object sender, System.EventArgs e)
    {

        if (lstContents.SelectedItems.Count = 1)
        {
            private string sTaskKey = CType(sender, ToolStripMenuItem).Tag.ToString;
            private string sChKey = lstContents.SelectedItems(0).SubItems(3).Text;
            private clsTask tasGeneral = Adventure.htblTasks(sTaskKey);

            private New clsTask tas;
            if (tasGeneral IsNot null)
            {
                tas.TaskType = clsTask.TaskTypeEnum.Specific;
                tas.GeneralKey = tasGeneral.Key;

                private bool bAdded = false;
                ReDim tas.Specifics(tasGeneral.References.Count - 1);
                for (int i = 0; i <= tasGeneral.References.Count - 1; i++)
                {
                    tas.Specifics(i) = New clsTask.Specific;
                    With tas.Specifics(i);
                        switch (tasGeneral.References(i))
                        {
                            case "%character%":
                            case "%character1%":
                            case "%characters%":
                                {
                                if (Not bAdded)
                                {
                                    .Type = ReferencesType.Character;
                                    .Multiple = false;
                                    .Keys.Add(sChKey);
                                    bAdded = True
                                }
                            default:
                                {
                                ' Leave bare bones
                        }
                    }
                Next;
            }
            tas.Description = (ToolStripMenuItem)(sender).Text;
            fGenerator.sDestinationList = folder.Name;
            tas.EditItem();
        }

    }


    private void miAddSpecificTask_Click(object sender, System.EventArgs e)
    {
        if (lstContents.SelectedItems.Count = 1 && miAddSpecificTask.DropDownItems.Count = 0)
        {
            private clsTask tas = Adventure.htblTasks(lstContents.SelectedItems(0).SubItems(3).Text);
            Edit("Task", tas);
        }
    }


    private void miAddObjectToLocation_Click(object sender, System.EventArgs e)
    {
        if (lstContents.SelectedItems.Count = 1 && miAddObjectToLocation.DropDownItems.Count = 0)
        {
            private string sParentKey = lstContents.SelectedItems(0).SubItems(3).Text;
            private New clsObject ob;
            private New clsObjectLocation obLoc;
            private New clsProperty sod;
            sod = Adventure.htblAllProperties("StaticOrDynamic").Copy
            ob.htblActualProperties.Add(sod);
            private New clsProperty sl;
            sl = Adventure.htblAllProperties("StaticLocation").Copy
            'sl.Value = sParentKey
            ob.htblActualProperties.Add(sl);
            ob.IsStatic = true;
            obLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
            obLoc.Key = sParentKey;
            ob.Move(obLoc);
            fGenerator.sDestinationList = folder.Name;
            ob.EditItem();
        }
    }


    private void miAddSubObject_Click(object sender, System.EventArgs e)
    {
        if (lstContents.SelectedItems.Count = 1 && miAddObjectToLocation.DropDownItems.Count = 0)
        {
            private string sParentKey = lstContents.SelectedItems(0).SubItems(3).Text;
            private New clsObject ob;
            private New clsObjectLocation obLoc;
            private New clsProperty sod;
            sod = Adventure.htblAllProperties("StaticOrDynamic").Copy
            ob.htblActualProperties.Add(sod);
            private New clsProperty sl;
            sl = Adventure.htblAllProperties("StaticLocation").Copy
            ob.htblActualProperties.Add(sl);
            ob.IsStatic = true;
            if (Adventure.htblObjects.ContainsKey(sParentKey))
            {
                obLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject;
            ElseIf Adventure.htblCharacters.ContainsKey(sParentKey) Then
                obLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter;
            }
            obLoc.Key = sParentKey;
            ob.Move(obLoc);
            fGenerator.sDestinationList = folder.Name;
            ob.EditItem();
        }
    }


    private void miAddItem_Click(object sender, System.EventArgs e)
    {

        fGenerator.sDestinationList = folder.Name;
        switch (miAddItem.Text)
        {
            case "Add Location":
                {
                private New frmLocation(New clsLocation, True) fLocation;
            case "Add Object":
                {
                private New frmObject(New clsObject, True) fObject;
            case "Add Task":
                {
                private New frmTask(New clsTask, True) fTask;
            case "Add Event":
                {
                private New frmEvent(New clsEvent, True) fEvent;
            case "Add Character":
                {
                private New frmCharacter(New clsCharacter, True) fCharacter;
            case "Add Property":
                {
                private New frmProperty(New clsProperty, True) fProperty;
            case "Add Variable":
                {
                private New frmVariable(New clsVariable, True) fVariable;
            case "Add Text Override":
                {
                private New frmTextOverride(New clsALR) fALR;
            case "Add Group":
                {
                private New frmGroup(New clsGroup, True) fGroup;
            case "Add Hint":
                {
                private New frmHint(New clsHint, True) fHint;
            case "Add Synonym":
                {
                private New frmSynonym(New clsSynonym) fSynonym;
            case "Add User Function":
                {
                private New frmUserFunction(New clsUserFunction) fFunction;
        }
    }


    private void lstContents_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        'If lstContents.ActiveItem IsNot Nothing AndAlso lstContents.ActiveItem.IsInEditMode Then Exit Sub ' Not working for some reason
        If bRenaming Then Exit Sub

        if (e.KeyData = Keys.Delete)
        {
            DeleteItems(SelectedKeys);
        }
    }

    private void lstContents_ItemEnteredEditMode(object sender, Infragistics.Win.UltraWinListView.ItemEnteredEditModeEventArgs e)
    {
        bRenaming = True
    }

    private void folder_MembersChanged(object sender, System.EventArgs e)
    {
        RaiseEvent MembersChanged(sender, e);
    }

    private void miExportFolder_Click(object sender, EventArgs e)
    {
        if (miExportFolder.Tag.ToString = "")
        {
            fGenerator.ExportModule(Me.folder.Key);
        Else
            fGenerator.ExportModule(miExportFolder.Tag.ToString);
        }
    }

}

}