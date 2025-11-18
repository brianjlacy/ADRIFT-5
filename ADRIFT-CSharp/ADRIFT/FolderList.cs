using Infragistics.Win.UltraWinTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


public class FolderList
{

    private const string DESKTOP = "Desktop";

    Public WriteOnly Property HideLibrary() As Boolean
set(ByVal Boolean)
            treeFolders.BeginUpdate();
            ' Hide a folder if it is NOT empty, but only contains hidden items
            For Each node As UltraTreeNode In treeFolders.Nodes
                SetNodeVisible(node, ! value);
            Next;
            treeFolders.EndUpdate();
        }
    }


    private bool SetNodeVisible(UltraTreeNode node, bool bVisible)
    {

        ' If all subnodes are folders and they're all hidden, hide the parent folder
        private bool bAllNodesHidden = node.Nodes.Count > 0;
        For Each subnode As UltraTreeNode In node.Nodes
            If SetNodeVisible(subnode, bVisible) Then bAllNodesHidden = false
        Next;

        if (Not bVisible)
        {
            private bool bAllHidden = false;
            if (Adventure.dictFolders(node.Key).Members.Count > 0)
            {
                bAllHidden = True
                if (bAllNodesHidden)
                {
                    bAllHidden = True
                Else
                    For Each sKey As String In Adventure.dictFolders(node.Key).Members
                        private clsItem itmGen = Adventure.dictAllItems(sKey);
                        if (itmGen IsNot null && Not (itmGen.IsLibrary && (TypeOf itmGen Is clsTask || TypeOf itmGen Is clsProperty)))
                        {
                            bAllHidden = False
                            Exit For;
                        }
                    Next;
                }
            }
            node.Visible = ! bAllHidden;
        Else
            node.Visible = true;
        }

        return node.Visible;

    }


    private void miLocation_Click(System.Object sender, System.EventArgs e)
    {
        private New frmLocation(New clsLocation, True) fLocation;
    }

    private void miObject_Click(System.Object sender, System.EventArgs e)
    {
        private New frmObject(New clsObject, True) fObject;
    }

    private void miTask_Click(System.Object sender, System.EventArgs e)
    {
        private New frmTask(New clsTask, True) fTask;
    }

    private void miEvent_Click(System.Object sender, System.EventArgs e)
    {
        private New frmEvent(New clsEvent, True) fEvent;
    }

    private void miCharacter_Click(System.Object sender, System.EventArgs e)
    {
        private New frmCharacter(New clsCharacter, True) fCharacter;
    }

    private void miGroup_Click(System.Object sender, System.EventArgs e)
    {
        private New frmGroup(New clsGroup, True) fGroup;
    }

    private void miVariable_Click(System.Object sender, System.EventArgs e)
    {
        private New frmVariable(New clsVariable, True) fVariable;
    }

    private void miTextOverride_Click(System.Object sender, System.EventArgs e)
    {
        private New frmTextOverride(New clsALR) fALR;
    }

    private void miHint_Click(System.Object sender, System.EventArgs e)
    {
        private New frmHint(New clsHint, True) fHint;
    }

    private void miNewFolder_Click(System.Object sender, System.EventArgs e)
    {
        AddNewFolder();
    }

    private void miRenameFolder_Click(System.Object sender, System.EventArgs e)
    {
        If treeFolders.ActiveNode != null Then RenameFolder(treeFolders.ActiveNode)
    }

    private void miOpenInNew_Click(System.Object sender, System.EventArgs e)
    {
        If treeFolders.ActiveNode != null Then OpenNewFolder(treeFolders.ActiveNode.Tag.ToString)
    }

    public UltraTreeNode AddFolder(clsFolder folder, UltraTreeNode nodeParent = null)
    {

        private UltraTreeNode nodeNew;
        if (nodeParent IsNot null)
        {
            if (treeFolders.GetNodeByKey(folder.Key) IsNot null)
            {
                nodeNew = treeFolders.GetNodeByKey(folder.Key)
                nodeNew.Text = folder.Name;
            Else
                nodeNew = nodeParent.Nodes.Add(folder.Key, folder.Name)
            }
        Else
            if (folder.Key = "ROOT")
            {
                nodeNew = treeFolders.Nodes.Add(folder.Key, folder.Name)
            Else
                nodeNew = treeFolders.Nodes("ROOT").Nodes.Add(folder.Key, folder.Name)
            }
        }
        nodeNew.Tag = folder.Key;
        nodeNew.Override.NodeAppearance.Image = My.Resources.Resources.imgFolderClosed16;
        nodeNew.Expanded = folder.Expanded;

        For Each sMember As String In folder.Members
            if (Adventure.dictFolders.ContainsKey(sMember))
            {
                AddFolder(Adventure.dictFolders(sMember), nodeNew);
            }
        Next;

        return nodeNew;

    }


    private clsFolder AddNewFolder(UltraTreeNode nodeParent = null, string sName = "", ref node As UltraTreeNode = Nothing, string sFolderKey = "")
    {

        If nodeParent == null Then nodeParent = treeFolders.ActiveNode

        private New clsFolder(sFolderKey) oFolder;
        if (Adventure IsNot null)
        {
            Adventure.dictFolders.Add(oFolder.Key, oFolder);
            If nodeParent != null Then Adventure.dictFolders(SafeString(nodeParent.Tag)).Members.Add(oFolder.Key)
        }
        If sName <> "" Then oFolder.Name = sName
        private UltraTreeNode nodeNew = AddFolder(oFolder, nodeParent);
        If sName = "" Then RenameFolder(nodeNew)
        node = nodeNew
        For Each folder As frmFolder In fGenerator.MDIFolders
            if (folder.Folder.folder.Key = nodeParent.Key)
            {
                folder.Folder.AddSingleItem(oFolder.Key);
            }
        Next;
        'UpdateListItem(oFolder.Key, oFolder.Name)
        return oFolder;

    }


    internal void RenameFolder(UltraTreeNode node)
    {
        node.BeginEdit();
    }

    private void treeFolders_AfterCollapse(object sender, Infragistics.Win.UltraWinTree.NodeEventArgs e)
    {
        Adventure.dictFolders(e.TreeNode.Tag.ToString).Expanded = false;
    }

    private void treeFolders_AfterExpand(object sender, Infragistics.Win.UltraWinTree.NodeEventArgs e)
    {
        Adventure.dictFolders(e.TreeNode.Tag.ToString).Expanded = true;
    }

    private Point ptStart;
    private bool bAllowedToDrag = false;
    private void treeFolders_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.Button = Windows.Forms.MouseButtons.Right)
        {
            If treeFolders.GetNodeFromPoint(e.Location) != null Then treeFolders.ActiveNode = treeFolders.GetNodeFromPoint(e.Location)
        }
        private UltraTreeNode itemCursor = treeFolders.GetNodeFromPoint(treeFolders.PointToClient(MousePosition));
        bAllowedToDrag = (itemCursor IsNot Nothing)
        ptStart = treeFolders.PointToClient(MousePosition)
    }

    public void InitialiseTree()
    {

        treeFolders.Nodes.Clear();
        bAllowFolderScroll = False

        private UltraTreeNode nodeDesktop = null;

        If ! Adventure.dictFolders.ContainsKey("ROOT") Then AddNewFolder(null, DESKTOP, nodeDesktop, "ROOT")
        If ! treeFolders.Nodes.Exists("ROOT") Then AddFolder(Adventure.dictFolders("ROOT"))

        if (Adventure.dictFolders.Count = 1)
        {
            private clsFolder folder;
            folder = AddNewFolder(nodeDesktop, "Locations", , "Locations")
            folder.Size.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh1", "0")) / 15);
            folder.Size.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw1", "0")) / 15);
            folder.Location.X = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx1", "0")) / 15);
            folder.Location.Y = CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby1", "0")) / 15);
            'Adventure.dictFolders("ROOT").Members.Add("Locations")
            For Each sKey As String In Adventure.htblLocations.Keys
                folder.Members.Add(sKey);
            Next;
            OpenNewFolder(folder.Key);
            folder = AddNewFolder(nodeDesktop, "Objects", , "Objects")
            folder.Size.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh2", "0")) / 15);
            folder.Size.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw2", "0")) / 15);
            folder.Location.X = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx2", "0")) / 15);
            folder.Location.Y = CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby2", "0")) / 15);
            'Adventure.dictFolders("ROOT").Members.Add("Objects")
            For Each sKey As String In Adventure.htblObjects.Keys
                folder.Members.Add(sKey);
            Next;
            OpenNewFolder(folder.Key);
            folder = AddNewFolder(nodeDesktop, "Tasks", , "Tasks")
            folder.Size.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh3", "0")) / 15);
            folder.Size.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw3", "0")) / 15);
            folder.Location.X = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx3", "0")) / 15);
            folder.Location.Y = CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby3", "0")) / 15);
            'Adventure.dictFolders("ROOT").Members.Add("Tasks")
            For Each sKey As String In Adventure.htblTasks.Keys
                folder.Members.Add(sKey);
            Next;
            OpenNewFolder(folder.Key);
            folder = AddNewFolder(nodeDesktop, "Events", , "Events")
            folder.Size.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh4", "0")) / 15);
            folder.Size.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw4", "0")) / 15);
            folder.Location.X = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx4", "0")) / 15);
            folder.Location.Y = CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby4", "0")) / 15);
            'Adventure.dictFolders("ROOT").Members.Add("Events")
            For Each sKey As String In Adventure.htblEvents.Keys
                folder.Members.Add(sKey);
            Next;
            OpenNewFolder(folder.Key);
            folder = AddNewFolder(nodeDesktop, "Characters", , "Characters")
            folder.Size.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh0", "0")) / 15);
            folder.Size.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw0", "0")) / 15);
            folder.Location.X = CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx0", "0")) / 15);
            folder.Location.Y = CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby0", "0")) / 15);
            'Adventure.dictFolders("ROOT").Members.Add("Characters")
            For Each sKey As String In Adventure.htblCharacters.Keys
                folder.Members.Add(sKey);
            Next;
            OpenNewFolder(folder.Key);
            folder = AddNewFolder(nodeDesktop, "Groups", , "Groups")
            'Adventure.dictFolders("ROOT").Members.Add("Groups")
            For Each sKey As String In Adventure.htblGroups.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "Variables", , "Variables")
            'Adventure.dictFolders("ROOT").Members.Add("Variables")
            For Each sKey As String In Adventure.htblVariables.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "Text Overrides", , "Text Overrides")
            'Adventure.dictFolders("ROOT").Members.Add("Text Overrides")
            For Each sKey As String In Adventure.htblALRs.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "Hints", , "Hints")
            'Adventure.dictFolders("ROOT").Members.Add("Hints")
            For Each sKey As String In Adventure.htblHints.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "Properties", , "Properties")
            'Adventure.dictFolders("ROOT").Members.Add("Properties")
            For Each sKey As String In Adventure.htblAllProperties.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "Synonyms", , "Synonyms")
            'Adventure.dictFolders("ROOT").Members.Add("Properties")
            For Each sKey As String In Adventure.htblSynonyms.Keys
                folder.Members.Add(sKey);
            Next;
            folder = AddNewFolder(nodeDesktop, "User Functions", , "User Functions")
            'Adventure.dictFolders("ROOT").Members.Add("Properties")
            For Each sKey As String In Adventure.htblUDFs.Keys
                folder.Members.Add(sKey);
            Next;
        Else
            ' Load existing folders
            For Each folder As clsFolder In Adventure.dictFolders.Values
                If treeFolders.GetNodeByKey(folder.Key) == null Then AddFolder(folder)
                'If folder.Visible Then OpenNewFolder(folder.Key)
            Next;

            ' Ensure all our items are in at least one folder
            For Each sKey As String In Adventure.htblLocations.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Locations"))
                    {
                        Adventure.dictFolders("Locations").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblObjects.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Objects"))
                    {
                        Adventure.dictFolders("Objects").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblTasks.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Tasks"))
                    {
                        Adventure.dictFolders("Tasks").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblEvents.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Events"))
                    {
                        Adventure.dictFolders("Events").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblCharacters.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Characters"))
                    {
                        Adventure.dictFolders("Characters").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblAllProperties.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Properties"))
                    {
                        Adventure.dictFolders("Properties").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblVariables.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Variables"))
                    {
                        Adventure.dictFolders("Variables").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblGroups.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Groups"))
                    {
                        Adventure.dictFolders("Groups").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblALRs.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Text Overrides"))
                    {
                        Adventure.dictFolders("Text Overrides").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblHints.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Hints"))
                    {
                        Adventure.dictFolders("Hints").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblUDFs.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("User Functions"))
                    {
                        Adventure.dictFolders("User Functions").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;
            For Each sKey As String In Adventure.htblSynonyms.Keys
                if (Not ItemIsInFolder(sKey))
                {
                    if (Adventure.dictFolders.ContainsKey("Synonyms"))
                    {
                        Adventure.dictFolders("Synonyms").Members.Add(sKey);
                    Else
                        Adventure.dictFolders("ROOT").Members.Add(sKey);
                    }
                }
            Next;

            For Each folder As clsFolder In Adventure.dictFolders.Values
                If folder.Visible Then OpenNewFolder(folder.Key)
            Next;

        }

        bAllowFolderScroll = True

    }


    private bool ItemIsInFolder(string sItemKey)
    {

        For Each folder As clsFolder In Adventure.dictFolders.Values
            If folder.Members.Contains(sItemKey) Then Return true
        Next;
        return false;

    }


    private void cmsFolders_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {

        miExpand.Text = "Expand";
        if (treeFolders.ActiveNode IsNot null)
        {
            if (treeFolders.ActiveNode.HasNodes)
            {
                miExpand.Enabled = true;
                if (treeFolders.ActiveNode.Expanded)
                {
                    miExpand.Text = "Collapse";
                Else
                    miExpand.Text = "Expand";
                }
            Else
                miExpand.Enabled = false;
            }

            miOpenInNew.Enabled = true;
            For Each child As frmFolder In fGenerator.MDIFolders
                if (child.Folder.folder.Key = treeFolders.ActiveNode.Tag.ToString)
                {
                    miOpenInNew.Enabled = false;
                    Exit For;
                }
            Next;
            miDeleteFolder.Enabled = (treeFolders.ActiveNode != treeFolders.Nodes("ROOT"));
            fGenerator.sDestinationList = treeFolders.ActiveNode.Key;
        Else
            miExpand.Enabled = false;
        }

    }


    private void miExpand_Click(object sender, System.EventArgs e)
    {

        if (treeFolders.ActiveNode IsNot null)
        {
            if (treeFolders.ActiveNode.HasNodes)
            {
                if (treeFolders.ActiveNode.Expanded)
                {
                    treeFolders.ActiveNode.CollapseAll();
                Else
                    treeFolders.ActiveNode.ExpandAll();
                }
            }
        }

    }


    public void OpenNewFolder(string sFolder)
    {
        For Each child As frmFolder In fGenerator.MDIFolders
            if (child.Folder.folder.Key = sFolder)
            {
                If (Now.Subtract(child.Folder.dtLoaded).TotalMilliseconds < 500 && child.Folder.sLastKey <> "") _
                || (child.Folder.lstContents.Items.Count <> child.Folder.folder.Members.Count) Then;
                    child.Folder.LoadFolder(child.Folder.sLastKey);
                    Exit For;
                Else
                    child.Folder.SetActive();
                    Exit Sub;
                }
            }
        Next;
        LoadFolder(sFolder);
    }


    private void treeFolders_DoubleClick(object sender, System.EventArgs e)
    {
        if (treeFolders.ActiveNode IsNot null)
        {
            If ! treeFolders.ActiveNode.HasNodes Then OpenNewFolder(treeFolders.ActiveNode.Tag.ToString)
        }
    }


    private void LoadFolder(string sFolderKey, string sPaneKey = "")
    {

        private New frmFolder frmFolder;
        'GetFormPosition(CType(frmFolder, Form))
        SetWindowStyle(frmFolder);
        frmFolder.Folder.LoadFolder(sFolderKey);
        If frmFolder.Folder.folder.Size.Width > 0 || frmFolder.Folder.folder.Size.Height > 0 Then frmFolder.Size = New Size(frmFolder.Folder.folder.Size.Width, frmFolder.Folder.folder.Size.Height)
        'If frmFolder.Folder.folder.Size.Height > 0 Then frmFolder.Height = frmFolder.Folder.folder.Size.Height
        if (frmFolder.Folder.folder.Location.X <> -100 && frmFolder.Folder.folder.Location.Y <> -100)
        {
            frmFolder.StartPosition = FormStartPosition.Manual;
            frmFolder.Location = New Point(frmFolder.Folder.folder.Location.X, frmFolder.Folder.folder.Location.Y);
        Else
            frmFolder.StartPosition = FormStartPosition.WindowsDefaultLocation;
        }
        frmFolder.MdiParent = fGenerator;
        frmFolder.Show();
        Application.DoEvents();
        frmFolder.ScrollIntoView();

    }


    public bool bSettingNode = false;
    private void treeFolders_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
    {

        If bSettingNode Then Exit Sub

        if (e.NewSelections IsNot null && e.NewSelections.Count = 1)
        {
            For Each child As frmFolder In fGenerator.MDIFolders
                if (child.Folder.folder.Key = e.NewSelections(0).Tag.ToString)
                {
                    child.Folder.SetActive();
                    child.Folder.BringToFront();

                    'child.ScrollControlIntoView(
                    child.ScrollIntoView();

                    Exit Sub;
                }
            Next;
            'If fGenerator.ActiveFolder IsNot Nothing Then
            '    fGenerator.ActiveFolder.LoadFolder(e.NewSelections(0).Tag.ToString)
            'End If
        }
    }

    private void treeFolders_Click(object sender, System.EventArgs e)
    {

    }

    private void treeFolders_AfterLabelEdit(object sender, Infragistics.Win.UltraWinTree.NodeEventArgs e)
    {

        private string sKey = e.TreeNode.Key;
        switch (Adventure.GetTypeFromKeyNice(sKey))
        {
            case "Folder":
                {
                Adventure.dictFolders(sKey).Name = e.TreeNode.Text;
                For Each child As frmFolder In fGenerator.MDIFolders
                    If child.Folder.folder.Key = sKey Then child.lblCaption.Text = e.TreeNode.Text
                    if (child.Folder.lstContents.Items.Exists(sKey))
                    {
                        child.Folder.lstContents.Items(sKey).Value = e.TreeNode.Text;
                    }
                Next;
            default:
                {
                TODO();
        }

    }


    private void miDeleteFolder_Click(object sender, System.EventArgs e)
    {
        if (treeFolders.ActiveNode IsNot null)
        {
            If ! treeFolders.ActiveNode.HasNodes Then DeleteItems(treeFolders.ActiveNode.Tag.ToString)
        }
    }


    private void treeFolders_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.Button = Windows.Forms.MouseButtons.Left && bAllowedToDrag)
        {
            ' This line prevents the DoubleClick event from firing...
            private Point ptNow = treeFolders.PointToClient(MousePosition);
            ' Make sure mouse has moved at least 3 pixels before starting drag
            if (Math.Abs(ptStart.X - ptNow.X) > 3 || Math.Abs(ptStart.Y - ptNow.Y) > 3)
            {
                treeFolders.DoDragDrop(treeFolders.SelectedNodes, DragDropEffects.Move | DragDropEffects.Scroll);
            }
        }
    }


    private void treeFolders_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {

        if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
        {
            e.Effect = DragDropEffects.Move;
        Else
            e.Effect = DragDropEffects.None;
        }

    }


    private void treeFolders_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {

        private UltraTreeNode nodeHover = treeFolders.GetNodeFromPoint(treeFolders.PointToClient(MousePosition));

        if (nodeHover IsNot null)
        {
            treeFolders.HotTrackingNode = nodeHover;
            if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
            {
                With (Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection)(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"));
                    ' Don't allow dragging a folder into itself, or any sub-folder thereof
                    for (int i = .Count - 1; i <= 0; i += -1)
                    {
                        private string sItemKey = .Item(i).SubItems(3).Text;
                        if (nodeHover.Key = sItemKey)
                        {
                            e.Effect = DragDropEffects.None;
                            Exit Sub;
                        }
                        if (Adventure.dictFolders.ContainsKey(sItemKey))
                        {
                            if (Adventure.dictFolders(sItemKey).ContainsKey(nodeHover.Key))
                            {
                                e.Effect = DragDropEffects.None;
                                Exit Sub;
                            }
                        }
                    Next;
                }

                e.Effect = DragDropEffects.Move;
            ElseIf e.Data.GetDataPresent("Infragistics.Win.UltraWinTree.SelectedNodesCollection") Then
                With (Infragistics.Win.UltraWinTree.SelectedNodesCollection)(e.Data.GetData("Infragistics.Win.UltraWinTree.SelectedNodesCollection"));
                    ' Don't allow dragging a folder into itself, or any sub-folder thereof
                    for (int i = .Count - 1; i <= 0; i += -1)
                    {
                        private string sItemKey = .Item(i).Key;
                        if (nodeHover.Key = sItemKey)
                        {
                            e.Effect = DragDropEffects.None;
                            Exit Sub;
                        }
                        if (Adventure.dictFolders.ContainsKey(sItemKey))
                        {
                            if (Adventure.dictFolders(sItemKey).ContainsKey(nodeHover.Key))
                            {
                                e.Effect = DragDropEffects.None;
                                Exit Sub;
                            }
                        }
                    Next;
                }

                e.Effect = DragDropEffects.Move;
            Else
                e.Effect = DragDropEffects.None;
            }
        Else
            e.Effect = DragDropEffects.None;
        }

    }


    private void treeFolders_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {

        try
        {
            private UltraTreeNode nodeHover = treeFolders.GetNodeFromPoint(treeFolders.PointToClient(MousePosition));

            if (nodeHover IsNot null)
            {
                private bool bShownError = false;
                private New List<Infragistics.Win.UltraWinListView.UltraListViewItem> lItems;

                if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
                {
                    private Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection items = CType(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"), Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection);
                    If items.Count = 0 Then Exit Sub
                    private Infragistics.Win.UltraWinListView.UltraListView lstSource = items.First.Control;
                    lstSource.BeginUpdate();

                    for (int i = items.Count - 1; i <= 0; i += -1)
                    {
                        private Infragistics.Win.UltraWinListView.UltraListViewItem itm = items(i);
                        private string sItemKey = itm.SubItems(3).Text;
                        private int iOldIndex = itm.Index;


                        Adventure.dictFolders((Folder)(itm.Control.Parent).folder.Key).Members.Remove(sItemKey);

                        private Infragistics.Win.UltraWinTree.UltraTreeNode treenode = fGenerator.FolderList1.treeFolders.GetNodeByKey(sItemKey);
                        If treenode != null Then treenode.Parent.Nodes.Remove(treenode)

                        itm.Control.Items.Remove(itm);

                        private clsFolder folderDest = null;
                        private Infragistics.Win.UltraWinListView.UltraListView lstContents = null;
                        For Each f As frmFolder In fGenerator.MDIFolders
                            if (f.Folder IsNot null && f.Folder.folder IsNot null && f.Folder.folder.Key = nodeHover.Key)
                            {
                                folderDest = f.Folder.folder
                                lstContents = f.Folder.lstContents
                                Exit For;
                            }
                        Next;
                        If folderDest == null Then folderDest = Adventure.dictFolders(nodeHover.Key)

                        private UltraTreeNode nodeDest = fGenerator.FolderList1.treeFolders.GetNodeByKey(folderDest.Key);
                        If nodeDest != null && treenode != null Then nodeDest.Nodes.Add(treenode)

                        folderDest.Members.Insert(folderDest.Members.Count, sItemKey);

                        If lstContents != null Then lstContents.Items.Insert(folderDest.Members.Count - 1, itm)

                        lItems.Add(itm);
                        If i = 0 Then itm.Activate()

                    Next;

                    lstSource.EndUpdate();

                ElseIf e.Data.GetDataPresent("Infragistics.Win.UltraWinTree.SelectedNodesCollection") Then
                    With (Infragistics.Win.UltraWinTree.SelectedNodesCollection)(e.Data.GetData("Infragistics.Win.UltraWinTree.SelectedNodesCollection"));
                        If .Count = 1 Then ' Should be - we only allow to drag a single folder in the tree
                            private UltraTreeNode node = .Item(0);
                            private string sItemKey = node.Key;
                            private UltraTreeNode nodeParent = node.Parent;

                            if (nodeParent IsNot null)
                            {
                                nodeParent.Nodes.Remove(node);

                                private clsFolder folderDest = null;
                                'Dim lstContents As Infragistics.Win.UltraWinListView.UltraListView = Nothing
                                private ADRIFT.Folder VisibleSrcFolder = null;
                                private ADRIFT.Folder VisibleDestFolder = null;
                                For Each f As frmFolder In fGenerator.MDIFolders
                                    if (f.Folder IsNot null && f.Folder.folder IsNot null)
                                    {
                                        if (f.Folder.folder.Key = nodeHover.Key)
                                        {
                                            VisibleDestFolder = f.Folder
                                            folderDest = VisibleDestFolder.folder
                                        }
                                        If f.Folder.folder.Key = nodeParent.Key Then VisibleSrcFolder = f.Folder
                                        ' lstContents = f.Folder.lstContents
                                        If VisibleSrcFolder != null && VisibleDestFolder != null Then Exit For
                                    }
                                Next;
                                If folderDest == null Then folderDest = Adventure.dictFolders(nodeHover.Key)

                                folderDest.Members.Insert(folderDest.Members.Count, sItemKey);

                                If VisibleSrcFolder != null Then VisibleSrcFolder.RemoveSingleItem(sItemKey)
                                If VisibleDestFolder != null Then VisibleDestFolder.AddSingleItem(sItemKey)

                                Adventure.dictFolders(nodeParent.Key).Members.Remove(sItemKey);

                                'If lstContents IsNot Nothing Then  lstContents.Items.Insert(folderDest.Members.Count - 1, itm)
                                nodeHover.Nodes.Add(node);

                                'lItems.Add(itm)
                                'If i = 0 Then itm.Activate()

                            }

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Folder drop error", ex);
        }

    }


    private void miExportFolder_Click(object sender, EventArgs e)
    {
        if (treeFolders.ActiveNode IsNot null)
        {
            fGenerator.ExportModule(treeFolders.ActiveNode.Tag.ToString);
        }
    }

}

}