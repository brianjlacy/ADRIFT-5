using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace ADRIFT
{
Option Strict On;
Option Explicit On;




internal static class GeneratorGlobal
{

public enum OverwriteLibrariesEnum
    {
        PromptPerLibrary = 0
        PromptPerItem = 1
        Always = 2
        Never = 3
    }

    public object oCopiedItem;
    public frmGenerator ' New frmGenerator fGenerator;
    public SearchReplace fSearch;
    public Infragistics.Win.UltraWinToolbars.ToolbarStyle eStyle;
    'Public eStyle2 As Infragistics.Win.style
    public Infragistics.Win.Office2007ColorScheme eColour2007;
    public Infragistics.Win.Office2010ColorScheme eColour2010;
    public Infragistics.Win.Office2013ColorScheme eColour2013;
    public New Collection colAllForms;
    public OverwriteLibrariesEnum eOverwriteLibraries = OverwriteLibrariesEnum.PromptPerLibrary;
    public bool bEnableAudio = true;
    public bool bEnableGraphics = true;
    public bool bEnablePreview = true;
    public New List<Form> OpenForms;
    public bool bAllowFolderScroll = true;
    public byte[] xmlRestrictions;
    public byte[] xmlActions;

    internal const string IMGREGEX = "<img src=""(https?:(//)[\w\d:#:%/;$()~_\?\+-=\\\.&]*?|[a-zA-Z]:(\\[\w_\. -~\[\]]+?)*?\.\w+?)"">" ' URL or Path;
    internal const string AUDREGEX = "<audio ((play )?src=(""(?<src>(https?:(//)[\w\d:#:%/;$()~_\?\+-=\\\.&]*?|[a-zA-Z]:(\\[\w_\. -~\[\]]+?)*?\.\w+?))"")?|pause|stop)( channel=(?<channel>\d))?( loop=(?<loop>(Y|N)))?>" ' URL or Path;

    internal int iRegistered = 0 ' 0 = not checked, 1m-2m = no, 2m-4m = yes;
    public bool IsRegistered { get; }
        {
            get
            {
            return true;
            '' If we run this in 64-bit mode, we check the 64-bit system32 folder instead of the syswow64 folder.  Hmm...
            '' This needs to always look in the 32-bit folder for builds registered with ADRIFT 4.0
            ''
            'If iRegistered = 0 Then
            '    If fGenerator.PartOne AndAlso fGenerator.PartTwo Then
            '        Dim sKeyValue As String = RegValue(RegistryHive.ClassesRoot, ".txt", "A" & "SCII".ToLower)
            '        If sKeyValue = "0x" & Chr(&H30) & 0 & "0" & "0000" & 3 Then
            '            iRegistered = Random(2000001, 4000000)
            '        Else
            '            iRegistered = Random(1000000, 2000000)
            '        End If
            '    Else
            '        iRegistered = Random(1000000, 2000000)
            '    End If
            'End If
            ''Return False
            'Return iRegistered >= 2000001
        }
    }

    public string RegValue(RegistryHive Hive, string Key, string ValueName, ref ErrInfo As String = "")
    {

        'DEMO USAGE

        'Dim sAns As String
        'Dim sErr As String = ""

        'sAns = RegValue(RegistryHive.LocalMachine, _
        '  "SOFTWARE\Microsoft\Windows\CurrentVersion", _
        '  "ProgramFilesDir", sErr)
        'If sAns <> "" Then
        '    Debug.WriteLine("Value = " & sAns)
        'Else
        '    Debug.WriteLine("This error occurred: " & sErr)
        'End If

        private RegistryKey objParent;
        private RegistryKey objSubkey;
        private string sAns = "";

        switch (Hive)
        {
            case RegistryHive.ClassesRoot:
                {
                objParent = Registry.ClassesRoot
            case RegistryHive.CurrentConfig:
                {
                objParent = Registry.CurrentConfig
            case RegistryHive.CurrentUser:
                {
                objParent = Registry.CurrentUser
            case RegistryHive.DynData:
                {
                objParent = Registry.DynData
            case RegistryHive.LocalMachine:
                {
                objParent = Registry.LocalMachine
            case RegistryHive.PerformanceData:
                {
                objParent = Registry.PerformanceData
            case RegistryHive.Users:
                {
                objParent = Registry.Users
            default:
                {
                objParent = Nothing
        }

        try
        {
            objSubkey = objParent.OpenSubKey(Key)
            'if can't be found, object is not initialized
            if (Not objSubkey Is null)
            {
                sAns = SafeString(objSubkey.GetValue(ValueName))
            }

        }
        catch (Exception ex)
        {
            ErrInfo = ex.Message
        }
        finally
        {

            'if no error but value is empty, populate errinfo
            if (ErrInfo = "" And sAns = "")
            {
                ErrInfo = _
                   "No value found for requested registry key";
            }
        }
        return sAns;
    }


    ' Developer doesn't strip carats, just Runner
    public string StripCarats(string sText)
    {
        return sText;
    }


    public Color culOffice2007 { get; }
        {
            get
            {
            switch (eColour2007)
            {
                case Office2007ColorScheme.Black:
                    {
                    return Color.FromArgb(83, 83, 83);
                case Office2007ColorScheme.Blue:
                    {
                    return Color.FromArgb(206, 223, 239);
                case Office2007ColorScheme.Silver:
                    {
                    return Color.FromArgb(167, 173, 177);
            }
        }
    }
    public Color culOffice2010 { get; }
        {
            get
            {
            switch (eColour2010)
            {
                case Office2010ColorScheme.Black:
                    {
                    return Color.FromArgb(83, 83, 83);
                case Office2010ColorScheme.Blue:
                    {
                    return Color.FromArgb(206, 223, 239);
                case Office2010ColorScheme.Silver:
                    {
                    return Color.FromArgb(167, 173, 177);
            }
        }
    }
    public Color culOffice2013 { get; }
        {
            get
            {
            switch (eColour2013)
            {
                case Office2013ColorScheme.DarkGray:
                    {
                    return Color.FromArgb(83, 83, 83);
                case Office2013ColorScheme.LightGray:
                    {
                    return Color.FromArgb(206, 223, 239);
                case Office2013ColorScheme.White:
                    {
                    return Color.FromArgb(167, 173, 177);
            }
        }
    }
    public string sDictionary;
    Public sMainDictionary, sUserDictionary As String

    private Generic.List<clsItem> lCopiedItems;
    public Generic.List<clsItem> CopiedItems { get; set; }
        {
            get
            {
            return lCopiedItems;
        }
set(ByVal Generic.List(Of clsItem))
            lCopiedItems = value

            ' Set paste icon
            if (value Is null || value.Count = 0)
            {
                fGenerator.UTMMain.Tools("Paste").SharedProps.Enabled = false;
            Else
                fGenerator.UTMMain.Tools("Paste").SharedProps.Enabled = true;
            }
        }
    }


    public object CopiedItem { get; set; }
        {
            get
            {
            return oCopiedItem;
        }
set(ByVal Object)
            oCopiedItem = value

            ' Set paste icon
            if (value Is null)
            {
                fGenerator.UTMMain.Tools("Paste").SharedProps.Enabled = false;
            Else
                fGenerator.UTMMain.Tools("Paste").SharedProps.Enabled = true;
            }
        }
    }


    public DialogResult YesNoCancel(string sMessage, string sTitle = "ADRIFT Developer", string sSaveSetting = "", bool bShowCancel = true)
    {

        if (sSaveSetting <> "")
        {
            private string sResult = GetSetting("ADRIFT", "Generator", sSaveSetting);
            If sResult <> "" && SafeInt(sResult) > 0 Then Return (DialogResult)(sResult)
        }

        private New frmYesNoCancel ync;

        ync.lblText.Text = sMessage;
        ync.Text = sTitle;
        ync.btnCancel.Visible = bShowCancel;

        private DialogResult result = ync.ShowDialog;

        if (ync.chkRemember.Checked && sSaveSetting <> "" && result <> DialogResult.Cancel)
        {
            SaveSetting("ADRIFT", "Generator", sSaveSetting, CInt(result).ToString);
        }

        return result;

    }


    'Public Sub Main()

    '    Try
    '        bGenerator = True

    '        Dim s As New Size
    '        Dim fGenerator As New frmGenerator

    '        s.Height = CInt(CInt(GetSetting("ADRIFT", "Generator", "Window Height", (fGenerator.Size.Height * 15).ToString)) / 15) '- 20 ' Add this back on when adding menu I think
    '        s.Width = CInt(CInt(GetSetting("ADRIFT", "Generator", "Window Width", (fGenerator.Size.Width * 15).ToString)) / 15)

    '        IntroMessage()
    '        GetSettings()

    '        fGenerator.Size = s
    '        Application.Run(fGenerator)

    '    Catch ex As Exception
    '        ErrMsg("Critical Error", ex)
    '    End Try

    'End Sub


    public void GetSettings()
    {

        sDictionary = CStr(GetSetting("ADRIFT", "Generator", "DictionaryLanguage", "English"))
        sUserDictionary = CStr(GetSetting("ADRIFT", "Generator", "UserDictionaryLocation", DataPath(True) & "userdictionary.dict"))
        sMainDictionary = CStr(GetSetting("ADRIFT", "Generator", "DictionaryLocation", ""))
        fGenerator.Map1.ShowAxes = CBool(GetSetting("ADRIFT", "Generator", "ShowAxes", "-1"));
        fGenerator.Map1.ShowGrid = CBool(GetSetting("ADRIFT", "Generator", "ShowGrid", "-1"));
        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(fGenerator.UTMMain.Tools("Show Axes")).Checked = fGenerator.Map1.ShowAxes;
        (Infragistics.Win.UltraWinToolbars.StateButtonTool)(fGenerator.UTMMain.Tools("ShowGridLines")).Checked = fGenerator.Map1.ShowGrid;
        fGenerator.UTMMain.Ribbon.IsMinimized = CBool(GetSetting("ADRIFT", "Generator", "RibbonMinimized", "0"));

        fGenerator.PartTwo = CStr(GetSetting("ADRIFT", "Shared", "RegName")).Length > 0;

    }



    public void DeleteItems(Generic.List(Of String sKeys)
    {

        If sKeys.Count = 0 Then Return false

        private New Generic.List<string> lReferencedKeys;
        private int iReferencedCount = 0;

        ' First check to see if the item is referenced in anything other than in the set we're deleting
        For Each sKey As String In sKeys
            For Each itm As clsItem In Adventure.dictAllItems.Values
                if (Not sKeys.Contains(itm.Key))
                {
                    private int iCount = itm.ReferencesKey(sKey);
                    if (iCount > 0)
                    {
                        iReferencedCount += iCount;
                        lReferencedKeys.Add(itm.Key);
                    }
                }
            Next;
        Next;

        ' If it is, pop up a question to verify the deletion, if not, just delete the sucker
        ' Generate a nice message
        private string sType = "";
        private string sCaption = "";
        private string sThisThese = "";
        private string sIsAre = "";
        private string sItThem = "";
        private string sList = ".  ";

        For Each sKey As String In sKeys
            if (sType = "")
            {
                sType = Adventure.GetTypeFromKeyNice(sKey)
            Else
                if (Adventure.GetTypeFromKeyNice(sKey) <> sType)
                {
                    sType = "items"
                    Exit For;
                }
            }
        Next;
        If sType <> "items" && sKeys.Count > 1 Then sType = Adventure.GetTypeFromKeyNice(sKeys(0), true)
        if (sKeys.Count = 1)
        {
            sCaption = "Delete " & Adventure.GetNameFromKey(sKeys(0), , , True)
            sThisThese = "This "
            sType = LCase(sType)
            sIsAre = " is"
            sItThem = " it"
        Else
            sCaption = "Delete " & sKeys.Count & " " & sType
            sThisThese = "These "
            sType = sKeys.Count & " " & LCase(sType)
            sIsAre = " are"
            sItThem = " them"
        }
        if (lReferencedKeys.Count < 30)
        {
            sList = ":" & vbCrLf
            For Each sKey As String In lReferencedKeys
                sList &= vbCrLf + "   " + Adventure.GetNameFromKey(sKey, false, , true);
            Next;
            sList &= vbCrLf + vbCrLf;
        }

        If (lReferencedKeys.Count = 0 && sKeys.Count = 1) _
            || (iReferencedCount = 0 && MessageBox.Show("Are you sure you wish to delete these " + sType + "?", sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) _;
            || (iReferencedCount > 0 && MessageBox.Show(sThisThese + sType + sIsAre + " referenced" + (" " + iReferencedCount + " times in ").Replace(" 1 times", " once").Replace(" 2 times", " twice") + CStr(lReferencedKeys.Count + " other items").Replace(" 1 other items", " another item") + sList + "Are you sure you wish to delete" + sItThem + "?", sCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then;

            For Each sKey As String In sKeys
                DeleteItem(sKey);
            Next;
        }

    }
    public bool DeleteItems(string sKey)
    {
        private New Generic.List<string> lItems;
        lItems.Add(sKey);
        return DeleteItems(lItems);
    }


    private bool DeleteItem(string sKey)
    {

        ' Remove all references to the item
        For Each itm As clsItem In Adventure.dictAllItems.Values
            if (Not TypeOf itm Is clsFolder)
            {
                if (Not itm.DeleteKey(sKey))
                {
                    ErrMsg("Error deleting " + Adventure.GetNameFromKey(sKey));
                    return false;
                }
            }
        Next;

        ' If item is a folder, ask whether we want to delete it's contents (if it has any)
        ' If not, don't delete
        if (Adventure.GetTypeFromKeyNice(sKey) = "Folder")
        {
            if (Adventure.dictFolders.ContainsKey(sKey))
            {
                if (Adventure.dictFolders(sKey).Members.Count > 0)
                {
                    switch (MessageBox.Show(Adventure.GetNameFromKey(sKey) + " contains " + Adventure.dictFolders(sKey).Members.Count + " items.  Would you like to also delete these?", "Delete folder contents?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            {
                            ' Add all contents to delete list

                        case DialogResult.No:
                            {
                            ' Hmm, need to move the contents to our parent folder

                        case DialogResult.Cancel:
                            {
                            return false;
                    }
                }
                private Infragistics.Win.UltraWinTree.UltraTreeNode nodDeleting = fGenerator.FolderList1.treeFolders.GetNodeByKey(sKey);
                if (nodDeleting IsNot null)
                {
                    nodDeleting.Parent.Nodes.Remove(nodDeleting);
                }
                For Each folder As frmFolder In fGenerator.MDIFolders
                    For Each lvi As Infragistics.Win.UltraWinListView.UltraListViewItem In folder.Folder.lstContents.Items
                        if (lvi.Key = sKey)
                        {
                            folder.Folder.lstContents.Items.Remove(lvi);
                            Exit For;
                        }
                    Next;
                Next;
                For Each folder As frmFolder In fGenerator.MDIFolders
                    if (folder.Folder.folder.Key = sKey)
                    {
                        folder.Close();
                        Exit For;
                    }
                Next;
                For Each folder As clsFolder In Adventure.dictFolders.Values
                    folder.DeleteKey(sKey);
                Next;
            }
        }

        ' Remove from any folders
        For Each child As frmFolder In fGenerator.MDIFolders
            if (child.Folder.folder.Members.Contains(sKey))
            {
                child.Folder.RemoveSingleItem(sKey);
            }
        Next;

        If Adventure.dictAllItems(sKey).IsLibrary Then Adventure.listExcludedItems.Add(sKey)

        ' Then get rid of the item itself
        switch (Adventure.GetTypeFromKeyNice(sKey))
        {
            case "Location":
                {
                Adventure.Map.DeleteNode(sKey) ' If we are a location, need to update the map;
                Adventure.htblLocations.Remove(sKey);
            case "Object":
                {
                Adventure.htblObjects.Remove(sKey);
            case "Task":
                {
                Adventure.htblTasks.Remove(sKey);
            case "Event":
                {
                Adventure.htblEvents.Remove(sKey);
            case "Character":
                {
                Adventure.htblCharacters.Remove(sKey);
            case "Variable":
                {
                Adventure.htblVariables.Remove(sKey);
            case "Text Override":
                {
                Adventure.htblALRs.Remove(sKey);
            case "Hint":
                {
                Adventure.htblHints.Remove(sKey);
            case "Group":
                {
                Adventure.htblGroups.Remove(sKey);
            case "Property":
                {
                Adventure.htblAllProperties.Remove(sKey);
            case "Folder":
                {
                Adventure.dictFolders.Remove(sKey);
            case "Synonym":
                {
                Adventure.htblSynonyms.Remove(sKey);
            case "User Function":
                {
                Adventure.htblUDFs.Remove(sKey);
            default:
                {
                TODO("Delete item of type " + Adventure.GetTypeFromKeyNice(sKey));
                return false;
        }

        Adventure.Changed = true;
        return true;

    }



    public void CutItems(Generic.List(Of String sKeys)
    {
        if (CopyItems(sKeys))
        {
            For Each sKey As String In sKeys
                'Adventure.dictAllItems.Remove(sKey)
                For Each child As frmFolder In fGenerator.MDIFolders
                    if (child.Folder.folder.Members.Contains(sKey))
                    {
                        child.Folder.CutSingleItem(sKey);
                    }
                Next;
            Next;
        Else
            return false;
        }
    }


    public void CopyItems(Generic.List(Of String sKeys)
    {

        try
        {
            If ! bAppendToCollection Then CopiedItems = New Generic.List(Of clsItem)
            For Each child As frmFolder In fGenerator.MDIFolders
                child.Folder.UnCutItems();
            Next;
            For Each sKey As String In sKeys
                private clsItem item = Adventure.GetItemFromKey(sKey);
                if (item IsNot null)
                {
                    item = item.Clone
                    item.IsLibrary = false;
                    If bAppendToCollection Then item.Tag = "INFOLDER"
                    CopiedItems.Add(item);

                    If TypeOf item == clsFolder Then CopyItems((clsFolder)(item).Members, true)
                    '    'Dim FolderItems As New List(Of clsItem)
                    '    With CType(item, clsFolder)
                    '        CopyItems(.Members, True)
                    '        '.Members.Clear()
                    '        'For Each FolderItem As clsItem In FolderItems
                    '        '    'FolderItem.Key = FolderItem.GetNewKey()
                    '        '    CopiedItems.Add(FolderItem)
                    '        '    '.Members.Add(FolderItem.Key)
                    '        'Next
                    '    End With
                    'End If
                }
            Next;
            fGenerator.UTMMain.Tools("Paste").SharedProps.Enabled = (CopiedItems.Count > 0);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }


    'Public Sub CopyItem(ByVal sKey As String)

    '    If Adventure.GetTypeFromKey(sKey) Is Nothing Then Exit Sub ' i.e. trying to copy an item that doesn't exist

    '    Select Case True
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsLocation)
    '            CopiedItem = Adventure.htblLocations(sKey).Clone
    '            CType(CopiedItem, Generator.clsLocation).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsObject)
    '            CopiedItem = Adventure.htblObjects(sKey).Clone
    '            CType(CopiedItem, Generator.clsObject).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsTask)
    '            CopiedItem = Adventure.htblTasks(sKey).Clone '.Copy
    '            CType(CopiedItem, Generator.clsTask).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsEvent)
    '            CopiedItem = Adventure.htblEvents(sKey).Clone
    '            CType(CopiedItem, Generator.clsEvent).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsCharacter)
    '            CopiedItem = Adventure.htblCharacters(sKey).Clone
    '            CType(CopiedItem, Generator.clsCharacter).CharacterType = clsCharacter.CharacterTypeEnum.NonPlayer
    '            CType(CopiedItem, Generator.clsCharacter).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsALR)
    '            CopiedItem = Adventure.htblALRs(sKey).Clone
    '            CType(CopiedItem, Generator.clsALR).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsGroup)
    '            CopiedItem = Adventure.htblGroups(sKey).Clone
    '            CType(CopiedItem, Generator.clsGroup).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsHint)
    '            CopiedItem = Adventure.htblHints(sKey).Clone
    '            CType(CopiedItem, Generator.clsHint).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsProperty)
    '            CopiedItem = Adventure.htblAllProperties(sKey).Clone
    '            CType(CopiedItem, Generator.clsProperty).IsLibrary = False
    '        Case Adventure.GetTypeFromKey(sKey) Is GetType(Generator.clsVariable)
    '            CopiedItem = Adventure.htblVariables(sKey).Clone
    '            CType(CopiedItem, Generator.clsVariable).IsLibrary = False
    '        Case True
    '            ErrMsg("Not created code to copy " & Adventure.GetTypeFromKey(sKey).ToString & " yet...")

    '    End Select

    'End Sub


    public void PasteItems()
    {

        if (CopiedItems IsNot null && CopiedItems.Count > 0)
        {
            private Generic.List<Infragistics.Win.UltraWinTree.UltraTreeNode> nodes = null;
            private New Dictionary<string, string> dictKeyChanges;
            private New List<clsItem> NewItems;

            ' If CutItems contains a folder, make sure we don't paste it into a child of itself
            if (fGenerator.ActiveFolder IsNot null)
            {
                For Each itm As clsItem In CopiedItems
                    if (TypeOf itm Is clsFolder)
                    {
                        if (CType(itm, clsFolder).ContainsKey(fGenerator.ActiveFolder.folder.Key))
                        {
                            ErrMsg("Cannot move a folder into a subsidiary of itself!");
                            Exit Sub;
                        }
                    }
                Next;
            }

            For Each child As frmFolder In fGenerator.MDIFolders
                child.Folder.RemoveCutItems(nodes);
            Next;
            For Each item As clsItem In CopiedItems
                item = item.Clone
                NewItems.Add(item);
                With item;
                    .IsLibrary = false;
                    if (Adventure.AllKeys.Contains(.Key))
                    {
                        private string sOldKey = .Key;
                        .Key = .GetNewKey;
                        If sOldKey <> "" Then dictKeyChanges.Add(sOldKey, .Key)
                        ' Get new priority too
                        if (TypeOf item Is clsTask)
                        {
                            (clsTask)(item).Priority = CurrentMaxPriority(.IsLibrary) + 1;
                        }
                    }
                    Adventure.dictAllItems.Add(item);
                    if (fGenerator.ActiveFolder IsNot null && SafeString(item.Tag) <> "INFOLDER")
                    {
                        fGenerator.ActiveFolder.AddSingleItem(.Key);

                        if (nodes IsNot null)
                        {
                            private Infragistics.Win.UltraWinTree.UltraTreeNode treenode = fGenerator.FolderList1.treeFolders.GetNodeByKey(fGenerator.ActiveFolder.folder.Key);
                            For Each node As Infragistics.Win.UltraWinTree.UltraTreeNode In nodes
                                If node != null Then treenode.Nodes.Add(node)
                            Next;
                        }

                    }
                }
            Next;

            ' Sort out any folder members
            For Each item As clsItem In NewItems
                if (TypeOf item Is clsFolder)
                {
                    private New clsFolder.MemberList NewMembers;
                    With (clsFolder)(item);
                        For Each sOldMember As String In .Members
                            if (dictKeyChanges.ContainsKey(sOldMember))
                            {
                                NewMembers.Add(dictKeyChanges(sOldMember));
                            Else
                                NewMembers.Add(sOldMember);
                            }
                        Next;
                        .Members = NewMembers;
                    }

                }
            Next;
        Else
            ErrMsg("There are no items in the clipboard!");
        }

    }


    'Public Sub PasteItem()

    'If Not CopiedItem Is Nothing Then

    '    Select Case True
    '        Case TypeOf CopiedItem Is clsLocation
    '            Dim cLocation As clsLocation = CType(CopiedItem, clsLocation).Clone
    '            With cLocation
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Location")
    '                Adventure.htblLocations.Add(cLocation, .Key)
    '                UpdateListItem(.Key, .ShortDescription)
    '            End With

    '        Case TypeOf CopiedItem Is clsObject
    '            Dim cObject As clsObject = CType(CopiedItem, clsObject).Clone
    '            With cObject
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Object")
    '                Adventure.htblObjects.Add(cObject, .Key)
    '                UpdateListItem(.Key, .FullName)
    '            End With

    '        Case TypeOf CopiedItem Is clsTask
    '            Dim cTask As clsTask = CType(CopiedItem, clsTask).Clone ' .Copy
    '            With cTask
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Task")
    '                Adventure.htblTasks.Add(cTask, .Key)
    '                UpdateListItem(.Key, .Description)
    '            End With

    '        Case TypeOf CopiedItem Is clsEvent
    '            Dim cEvent As clsEvent = CType(CopiedItem, clsEvent).Clone
    '            With cEvent
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Event")
    '                Adventure.htblEvents.Add(cEvent, .Key)
    '                UpdateListItem(.Key, .Description)
    '            End With

    '        Case TypeOf CopiedItem Is clsCharacter
    '            Dim cCharacter As clsCharacter = CType(CopiedItem, clsCharacter).Clone
    '            With cCharacter
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Character")
    '                Adventure.htblCharacters.Add(cCharacter, .Key)
    '                UpdateListItem(.Key, .ProperName)
    '            End With

    '        Case TypeOf CopiedItem Is clsALR
    '            Dim cALR As clsALR = CType(CopiedItem, clsALR).Clone
    '            With cALR
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("ALR")
    '                Adventure.htblALRs.Add(cALR, .Key)
    '                UpdateListItem(.Key, .OldText.ToString)
    '            End With

    '        Case TypeOf CopiedItem Is clsGroup
    '            Dim cGroup As clsGroup = CType(CopiedItem, clsGroup).Clone
    '            With cGroup
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("LocationGroup")
    '                Adventure.htblGroups.Add(cGroup, .Key)
    '                UpdateListItem(.Key, .Name)
    '            End With

    '        Case TypeOf CopiedItem Is clsHint
    '            Dim cHint As clsHint = CType(CopiedItem, clsHint).Clone
    '            With cHint
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Hint")
    '                Adventure.htblHints.Add(cHint, .Key)
    '                UpdateListItem(.Key, .Question)
    '            End With

    '        Case TypeOf CopiedItem Is clsProperty
    '            Dim cProperty As clsProperty = CType(CopiedItem, clsProperty).Clone
    '            With cProperty
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Property")
    '                Adventure.htblAllProperties.Add(cProperty, .Key)
    '                UpdateListItem(.Key, .Description)
    '            End With

    '        Case TypeOf CopiedItem Is clsVariable
    '            Dim cVariable As clsVariable = CType(CopiedItem, clsVariable).Clone
    '            With cVariable
    '                If Adventure.AllKeys.Contains(.Key) Then .Key = Adventure.GetNewKey("Variable")
    '                Adventure.htblVariables.Add(cVariable, .Key)
    '                UpdateListItem(.Key, .Name)
    '            End With

    '        Case Else
    '            ErrMsg("Not possible to paste items of type " & CopiedItem.GetType.ToString & "!")
    '    End Select

    'End If

    'End Sub


    'Public Class ListInfo

    '    Public sName As String
    '    'Public sAdventure As String ' "ALL" if global

    '    Public arlKeys As New Generic.List(Of String)

    '    Public s As Size
    '    Public p As Point
    '    Public bVisible As Boolean


    '    Public Sub New(ByVal Name As String, Optional ByVal bFirst As Boolean = False)
    '        sName = Name
    '        AddTool(fGenerator.UTMMain, "mnuView", "_LIST_" & sName, sName, "_LIST_", bFirst)
    '    End Sub

    '    Public Function GetForm() As frmList
    '        For Each frm As frmList In fGenerator.MdiChildren
    '            If frm.ListInfo Is Me Then Return frm
    '        Next
    '        Return Nothing
    '    End Function

    'End Class
    'Public colLists As New Collection



    'Private Sub GenerateDefaultLists()

    '    Dim LocationList As New ListInfo("Locations", True)
    '    With LocationList
    '        .bVisible = True
    '        .s = New Size(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw1", "3000")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh1", "500")) / 15))
    '        .p = New Point(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx1", "0")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby1", "0")) / 15))
    '    End With
    '    colLists.Add(LocationList, LocationList.sName)

    '    Dim ObjectList As New ListInfo("Objects")
    '    With ObjectList
    '        .bVisible = True
    '        .s = New Size(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw2", "200")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh2", "500")) / 15))
    '        .p = New Point(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx2", "150")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby2", "0")) / 15))
    '    End With
    '    colLists.Add(ObjectList, ObjectList.sName)

    '    Dim TaskList As New ListInfo("Tasks")
    '    With TaskList
    '        .bVisible = True
    '        .s = New Size(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw3", "200")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh3", "500")) / 15))
    '        .p = New Point(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx3", "300")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby3", "0")) / 15))
    '    End With
    '    colLists.Add(TaskList, TaskList.sName)

    '    Dim EventList As New ListInfo("Events")
    '    With EventList
    '        .bVisible = True
    '        .s = New Size(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw4", "200")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh4", "500")) / 15))
    '        .p = New Point(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx4", "450")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby4", "0")) / 15))
    '    End With
    '    colLists.Add(EventList, EventList.sName)

    '    Dim CharList As New ListInfo("Characters")
    '    With CharList
    '        .bVisible = True
    '        .s = New Size(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subw0", "200")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Subh0", "500")) / 15))
    '        .p = New Point(CInt(CInt(GetSetting("ADRIFT", "Generator", "Subx0", "600")) / 15), CInt(CInt(GetSetting("ADRIFT", "Generator", "Suby0", "0")) / 15))
    '    End With
    '    colLists.Add(CharList, CharList.sName)

    '    Dim VarList As New ListInfo("Variables")
    '    With VarList
    '        .bVisible = False
    '        .p = New Point(100, 100)
    '        .s = New Size(200, 400)
    '    End With
    '    colLists.Add(VarList, VarList.sName)

    '    Dim ALRList As New ListInfo("Text Overrides")
    '    With ALRList
    '        .bVisible = False
    '        .p = New Point(200, 100)
    '        .s = New Size(200, 400)
    '    End With
    '    colLists.Add(ALRList, ALRList.sName)

    '    Dim GroupList As New ListInfo("Groups")
    '    With GroupList
    '        .bVisible = False
    '        .p = New Point(300, 100)
    '        .s = New Size(200, 400)
    '    End With
    '    colLists.Add(GroupList, GroupList.sName)

    '    Dim HintList As New ListInfo("Hints")
    '    With HintList
    '        .bVisible = False
    '        .p = New Point(400, 100)
    '        .s = New Size(200, 400)
    '    End With
    '    colLists.Add(HintList, HintList.sName)

    '    Dim PropList As New ListInfo("Properties")
    '    With PropList
    '        .bVisible = False
    '        .p = New Point(500, 100)
    '        .s = New Size(200, 400)
    '    End With
    '    colLists.Add(PropList, PropList.sName)

    '    Dim bShowLibrary As Boolean = Not CBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "True"))

    '    With Adventure
    '        Dim li As ListInfo = CType(colLists("Locations"), ListInfo)
    '        For Each loc As clsLocation In .htblLocations.Values
    '            'If bShowLibrary OrElse Not loc.IsLibrary Then
    '            li.arlKeys.Add(loc.Key)
    '        Next
    '        li = CType(colLists("Objects"), ListInfo)
    '        For Each obj As clsObject In .htblObjects.Values
    '            'If bShowLibrary OrElse Not obj.IsLibrary Then
    '            li.arlKeys.Add(obj.Key)
    '        Next
    '        li = CType(colLists("Tasks"), ListInfo)
    '        For Each task As clsTask In .htblTasks.Values
    '            If bShowLibrary OrElse Not task.IsLibrary Then li.arlKeys.Add(task.Key)
    '        Next
    '        li = CType(colLists("Events"), ListInfo)
    '        For Each evnt As clsEvent In .htblEvents.Values
    '            'If bShowLibrary OrElse Not evnt.IsLibrary Then
    '            li.arlKeys.Add(evnt.Key)
    '        Next
    '        li = CType(colLists("Characters"), ListInfo)
    '        For Each cha As clsCharacter In .htblCharacters.Values
    '            'If bShowLibrary OrElse Not cha.IsLibrary OrElse cha.Key = .Player.Key Then
    '            li.arlKeys.Add(cha.Key)
    '        Next
    '        li = CType(colLists("Variables"), ListInfo)
    '        For Each var As clsVariable In .htblVariables.Values
    '            'If bShowLibrary OrElse Not var.IsLibrary Then
    '            li.arlKeys.Add(var.Key)
    '        Next
    '        li = CType(colLists("Groups"), ListInfo)
    '        For Each grp As clsGroup In .htblGroups.Values
    '            'If bShowLibrary OrElse Not grp.IsLibrary Then
    '            li.arlKeys.Add(grp.Key)
    '        Next
    '        li = CType(colLists("Text Overrides"), ListInfo)
    '        For Each alr As clsALR In .htblALRs.Values
    '            'If bShowLibrary OrElse Not alr.IsLibrary Then
    '            li.arlKeys.Add(alr.Key)
    '        Next
    '        li = CType(colLists("Hints"), ListInfo)
    '        For Each hnt As clsHint In .htblHints.Values
    '            'If bShowLibrary OrElse Not hnt.IsLibrary Then
    '            li.arlKeys.Add(hnt.Key)
    '        Next
    '        li = CType(colLists("Properties"), ListInfo)
    '        For Each prp As clsProperty In .htblAllProperties.Values
    '            If bShowLibrary OrElse Not prp.IsLibrary Then li.arlKeys.Add(prp.Key)
    '        Next

    '    End With

    'End Sub


    'Public Function FindList(ByVal sTitle As String) As frmList

    '    For Each frm As frmList In fGenerator.MdiChildren
    '        If frm.Text = sTitle Then
    '            Return frm
    '        End If
    '    Next

    '    Return Nothing

    'End Function



    'Public Sub ShowList(ByRef li As ListInfo)

    '    Dim frmList As frmList = FindList(li.sName)
    '    If frmList Is Nothing Then frmList = New frmList(li)
    '    frmList.Show() '.Visible = True
    '    frmList.Tag = ""

    'End Sub


    public void OpenModule(string sFilename)
    {

        ' Just do same as open adventure for now
        'Dim OldAdventure As clsAdventure = Adventure

        'Adventure = New clsAdventure
        'LoadDefaults()
        '        If LoadFile(sFilename, FileIO.FileTypeEnum.TextAdventure_TAF) Then
        if (LoadFile(sFilename, FileIO.FileTypeEnum.XMLModule_AMF, LoadWhatEnum.All, false))
        {
            fGenerator.FolderList1.InitialiseTree();
        Else
            '    Adventure = OldAdventure
        }

    }


    public string GetFileSize(string sfilename)
    {

        if (File.Exists(sfilename))
        {
            private New IO.FileInfo(sfilename) fi;
            private long lSize = fi.Length;
            if (lSize < 1024)
            {
                return lSize + " bytes";
            ElseIf lSize < 10240 Then
                return (lSize / 1024).ToString("#,##0.##") + " KB";
            ElseIf lSize < 1024 * 1024 Then
                return (lSize / 1024).ToString("#,##0.#") + " KB";
            Else
                return (lSize / 1024 / 1024).ToString("#,##0.##") + " MB";
            }
        Else
            return "";
        }

    }


    public void NotAvailable()
    {
        ErrMsg("Sorry, this feature is not available in the unregistered version.");
    }



    public void OpenAdventure(string sFilename)
    {

        private clsAdventure OldAdventure = Adventure;

        'SaveListsToXML()

        Adventure = New clsAdventure

        for (int i = OpenForms.Count - 1; i <= 0; i += -1)
        {
            if (OpenForms(i) IsNot fGenerator)
            {
                OpenForms(i).Close();
            }
        Next;

        private FileIO.FileTypeEnum eType = FileTypeEnum.TextAdventure_TAF;
        If sFilename.EndsWith("blorb") Then eType = FileTypeEnum.Blorb
#if DEBUG
        if (sFilename.EndsWith("exe"))
        {
            ' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
            ' Grab out the last 8 bytes, and see if it converts to a size
            Dim bData(5) As Byte
            private New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read) fStream;
            fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin);
            fStream.Read(bData, 0, 6);
            'fStream.Close()

            private string sFileSize = (new System.Text.ASCIIEncoding).GetString(bData).ToUpper;
            if (IsHex(sFileSize))
            {
                private long lFileSize = CLng("&H" + sFileSize);
                if (lFileSize > 0)
                {
                    Blorb = New clsBlorb
                    fStream.Seek(lFileSize, IO.SeekOrigin.Begin);
                    clsBlorb.bEXE = true;
                    if (Not Blorb.LoadBlorb(fStream, sFilename, lFileSize))
                    {
                        ErrMsg("Error loading embedded Blorb");
                        Exit Sub;
                    }
                }
            }
            fStream.Close();
            eType = FileTypeEnum.Exe
        }
#endif

        if (LoadFile(sFilename, eType, LoadWhatEnum.All, false))
        {
            'LoadLists(True)
            iLoading = 1
            fGenerator.FolderList1.InitialiseTree();
            iLoading = 0
            UpdateMainTitle();
            fGenerator.StatusBar1.Panels(0).Text = "File version: " + Adventure.Version;
            fGenerator.StatusBar1.Panels(1).Text = "File size: " + GetFileSize(sFilename);
            fGenerator.StatusBar1.Panels(2).Text = "Maximum score: " + Adventure.MaxScore;
            AddFileToList(Adventure.FullPath);
            if (Adventure.Password <> "")
            {
                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.Caption = "Unprotect Adventure";
                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Resources.imgLock32;
            Else
                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.Caption = "Protect Adventure";
                fGenerator.UTMMain.Tools("ProtectAdventure").SharedProps.AppearancesLarge.Appearance.Image = My.Resources.Resources.imgUnlock32;
            }
        Else
            Adventure = OldAdventure
        }

    }


    public void UpdateMainTitle()
    {

        if (Adventure IsNot null)
        {
            fGenerator.Text = Adventure.Title + " - ADRIFT Developer - [" + Adventure.Filename + "]";
        Else
            fGenerator.Text = "ADRIFT Developer";
        }

        'If fGenerator.SimpleMode Then fGenerator.Text &= " - Simple Mode: On"

    }

    '    Public Sub LoadLists(ByVal bNewAdventure As Boolean)

    '        Try

    '            If bNewAdventure Then
    '                Dim sListsFile As String = sLocalDataPath & Adventure.Filename.Replace(".taf", ".xml")


    '                ' Gets rid of all the configurable Lists from the menu
    '                For m As Integer = fGenerator.UTMMain.Tools.Count - 1 To 0 Step -1
    '                    If sLeft(fGenerator.UTMMain.Tools(m).Key, 6) = "_LIST_" Then
    '                        fGenerator.UTMMain.Tools.RemoveAt(m)
    '                    End If
    '                Next

    '                ' Clear any defined lists first
    '                For i As Integer = colLists.Count To 1 Step -1
    '                    colLists.Remove(i)
    '                Next

    '                If File.Exists(sListsFile) Then

    '                    Dim bShowLibrary As Boolean = Not CBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "True"))

    '                    Try
    '                        Dim xmlDoc As New XmlDocument
    '                        xmlDoc.Load(sListsFile)
    '                        Dim bFirst As Boolean = True

    '                        For Each nod As XmlNode In xmlDoc.SelectNodes("/Lists/List")
    '                            Dim li As New ListInfo(nod.SelectSingleNode("Name").InnerText, bFirst)
    '                            bFirst = False

    '                            li.p.X = CInt(nod.SelectSingleNode("Position/X").InnerText)
    '                            li.p.Y = CInt(nod.SelectSingleNode("Position/Y").InnerText)

    '                            li.s.Height = CInt(nod.SelectSingleNode("Size/Height").InnerText)
    '                            li.s.Width = CInt(nod.SelectSingleNode("Size/Width").InnerText)

    '                            li.bVisible = CBool(nod.SelectSingleNode("Visible").InnerText)

    '                            For Each nodItem As XmlNode In nod.SelectNodes("Item")
    '                                Dim bAddToList As Boolean = False
    '                                Select Case Adventure.GetTypeFromKeyNice(nodItem.InnerText, True)
    '                                    Case "Tasks", "Properties"
    '                                        bAddToList = bShowLibrary OrElse Not Adventure.IsItemLibrary(nodItem.InnerText)
    '                                    Case ""
    '                                        bAddToList = False
    '                                    Case Else
    '                                        bAddToList = True
    '                                End Select
    '                                If bAddToList Then ' If Not Adventure.GetTypeFromKey(nodItem.InnerText) Is Nothing Then
    '                                    'If bShowLibrary OrElse Not Adventure.IsItemLibrary(nodItem.InnerText) Then
    '                                    If Not li.arlKeys.Contains(nodItem.InnerText) Then li.arlKeys.Add(nodItem.InnerText)
    '                                    'End If
    '                                End If
    '                            Next
    '                            colLists.Add(li, li.sName)

    '                        Next

    '                        ' Need to bung everything that isn't in a List into a default list

    '                        For Each sKey As String In Adventure.AllKeys
    '                            'IsItemLibrary                            ' If not library item then...
    '                            If bShowLibrary OrElse Not Adventure.IsItemLibrary(sKey) Then
    '                                For Each li As ListInfo In colLists
    '                                    If li.arlKeys.Contains(sKey) Then GoTo NextList
    '                                Next
    '                                ' Not found, so add to default list
    '                                AddToDefaultList(sKey)
    '                            End If
    'NextList:
    '                        Next

    '                    Catch ex As Exception
    '                        ErrMsg("Error loading Lists XML file", ex)
    '                    End Try
    '                Else
    '                    GenerateDefaultLists()
    '                End If


    '                ' Mark any existing forms in new selection as to stay
    '                For Each li As ListInfo In colLists
    '                    Dim frm As frmList = FindList(li.sName)
    '                    If Not frm Is Nothing Then frm.Tag = "KEEP"
    '                Next
    '                For Each frm As frmList In fGenerator.MdiChildren
    '                    If CStr(frm.Tag) <> "KEEP" Then
    '                        frm.Visible = False
    '                        frm.Dispose()
    '                    End If
    '                Next
    '            End If

    '            For Each li As ListInfo In colLists
    '                Dim frm As frmList = FindList(li.sName)
    '                If frm Is Nothing Then
    '                    frm = New frmList(li)
    '                Else
    '                    If li.bVisible Then frm.Show()
    '                    frm.PopulateList(li)
    '                End If
    '                frm.Tag = ""
    '                frm.DoSummary()
    '            Next

    '            Dim bCascade As Boolean = False
    '            For Each frm As frmList In fGenerator.MdiChildren
    '                If frm.Width < 50 OrElse frm.Height < 50 Then
    '                    bCascade = True
    '                End If
    '            Next
    '            If bCascade Then fGenerator.LayoutMdi(MdiLayout.Cascade)

    '        Catch ex As Exception
    '            ErrMsg("LoadLists error", ex)
    '        End Try

    '    End Sub



    'Public Sub AddToDefaultList(ByVal sKey As String)

    '    ' Should we add to list
    '    Dim bAddToList As Boolean = True
    '    If CBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "True")) Then
    '        ' We only hide the task and property library items ... cos figure if someone adds the rest, the user would want to know about them
    '        Select Case Adventure.GetTypeFromKeyNice(sKey, True)
    '            Case "Tasks", "Properties"
    '                If Adventure.IsItemLibrary(sKey) Then Exit Sub
    '            Case Else
    '                ' Continue to add to list
    '        End Select
    '    End If


    '    '       If Not CBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "True")) OrElse Not Adventure.IsItemLibrary(sKey) Then
    '    For Each li As ListInfo In colLists
    '        If Adventure.GetTypeFromKeyNice(sKey, True) = li.sName Then
    '            li.arlKeys.Add(sKey)
    '            If Not li.GetForm Is Nothing Then li.GetForm.ItemList.AddItemToList(sKey)
    '            Exit Sub
    '        End If
    '    Next

    '    ' Ok, we didn't find a nice list, just bung it in the first one
    '    If colLists.Count > 0 Then
    '        With CType(colLists(1), ListInfo)
    '            .arlKeys.Add(sKey)
    '            If Not .GetForm Is Nothing Then .GetForm.ItemList.AddItemToList(sKey)
    '        End With
    '    Else
    '        'ErrMsg("No defined lists!")
    '    End If
    '    '        End If

    'End Sub


    'Public Sub SaveListsToXML()

    '    If Adventure Is Nothing Then Exit Sub

    '    Dim xmlWriter As XmlTextWriter
    '    Dim sListsFile As String = sLocalDataPath & Adventure.Filename.Replace(".taf", ".xml")

    '    xmlWriter = New XmlTextWriter(sListsFile, System.Text.Encoding.UTF8)

    '    With xmlWriter
    '        .Indentation = 4
    '        .IndentChar = " "c
    '        .Formatting = Formatting.Indented

    '        .WriteStartDocument()
    '        .WriteComment("File created by ADRIFT v" & Application.ProductVersion)

    '        .WriteStartElement("Lists")

    '        For Each li As ListInfo In colLists

    '            .WriteStartElement("List")

    '            .WriteElementString("Name", li.sName)

    '            .WriteStartElement("Size")
    '            .WriteElementString("Height", li.s.Height.ToString)
    '            .WriteElementString("Width", li.s.Width.ToString)
    '            .WriteEndElement() ' Size

    '            .WriteStartElement("Position")
    '            .WriteElementString("X", li.p.X.ToString)
    '            .WriteElementString("Y", li.p.Y.ToString)
    '            .WriteEndElement() ' Position

    '            .WriteElementString("Visible", li.bVisible.ToString)
    '            '.WriteElementString("Adventure", li.sAdventure)

    '            For Each sKey As String In li.arlKeys
    '                .WriteElementString("Item", sKey)
    '            Next

    '            .WriteEndElement() ' List

    '        Next

    '        .WriteEndElement() ' Lists

    '        .WriteEndDocument()

    '        .Flush()
    '        .Close()

    '    End With

    'End Sub


    public ButtonTool AddTool(ref UTMTarget As UltraToolbarsManager, string sTargetTool, string sKey, string sCaption, string sTag = "", bool bFirstInGroup = false, string sToolTip = "")
    {

        try
        {
            if (Not UTMTarget.Tools.Exists(sKey))
            {
                private New ButtonTool(sKey) newTool;

                newTool.SharedProps.Caption = sCaption;
                newTool.SharedProps.Tag = sTag;
                If sToolTip <> "" Then newTool.SharedProps.ToolTipText = sToolTip
                UTMTarget.Tools.Add(newTool);

                private ButtonTool newinstance = CType(CType(UTMTarget.Tools(sTargetTool), PopupMenuTool).Tools.AddTool(sKey), ButtonTool);
                newinstance.InstanceProps.IsFirstInGroup = bFirstInGroup;
                return newTool;
            }
        }
        catch (Exception ex)
        {
            ' Probably a duplicate key
        }
        return null;

    }




    'Public Sub EditLocation(ByVal Location As clsLocation)

    '    Dim frmLocation As New frmLocation

    '    With frmLocation
    '        .Text = "Location - " & Location.ShortDescription
    '        .txtShortDesc.Text = Location.ShortDescription
    '        .txtLongDesc.txtSource = Location.LongDescription
    '        .Changed = False

    '        .Show()
    '    End With

    'End Sub



    public void CloseLocation(frmLocation frmLocation)
    {

        SaveFormPosition(frmLocation);
        With frmLocation;
            .Close();
            .Dispose();
        }

    }

    public void CloseGroup(frmGroup frmGroup)
    {

        SaveFormPosition(frmGroup);
        With frmGroup;
            .Close();
            If ! .bModal Then .Dispose()
        }

    }

    public void CloseProperty(frmProperty frmProperty)
    {

        SaveFormPosition(frmProperty);
        With frmProperty;
            .Close();
            .Dispose();
        }

    }


    public void CloseObject(frmObject frmObject)
    {

        SaveFormPosition(frmObject);
        With frmObject;
            .Close();
            .Dispose();
        }

    }


    public void CloseCharacter(frmCharacter frmCharacter)
    {

        SaveFormPosition(frmCharacter);
        With frmCharacter;
            .Close();
            .Dispose();
        }

    }


    public void CloseVariable(frmVariable frmVariable)
    {

        SaveFormPosition(frmVariable);
        With frmVariable;
            .Close();
            .Dispose();
        }

    }

    public void CloseEvent(frmEvent frmEvent)
    {

        SaveFormPosition(frmEvent);
        With frmEvent;
            .Close();
            .Dispose();
        }

    }

    public void CloseTask(frmTask frmTask)
    {

        SaveFormPosition(frmTask);
        With frmTask;
            .Close();
            '.Visible = False
            If ! .bKeepOpen Then .Dispose()
        }

    }

    public void CloseHint(frmHint frmHint)
    {

        SaveFormPosition(frmHint);
        With frmHint;
            .Close();
            .Dispose();
        }

    }

    public void CloseALR(frmTextOverride frmALR)
    {

        SaveFormPosition(frmALR);
        With frmALR;
            .Close();
            .Dispose();
        }

    }

    public void CloseUDF(frmUserFunction frmUDF)
    {

        SaveFormPosition(frmUDF);
        With frmUDF;
            .Close();
            .Dispose();
        }

    }

    public void CloseSynonym(frmSynonym frmSyn)
    {

        SaveFormPosition(frmSyn);
        With frmSyn;
            .Close();
            .Dispose();
        }

    }

    public void CloseSettings(ref frmSettings As frmSettings)
    {

        SaveFormPosition(frmSettings);
        With frmSettings;
            .Close();
            .Dispose();
        }
        fGenerator.fSettings = null;

    }

    public void CloseOptions(ref frmOptions As frmOptions)
    {

        SaveFormPosition(frmOptions);
        With frmOptions;
            .Close();
            .Dispose();
        }
        fGenerator.fOptions = null;

    }

    public void SaveFormPosition(System.Windows.Forms.Form frmForm)
    {

        ' Function to record the position and state of a form
        SaveSetting("ADRIFT", "Generator", frmForm.Name + "_State", Format(frmForm.WindowState));

        if (frmForm.WindowState = FormWindowState.Normal)
        {
            SaveSetting("ADRIFT", "Generator", frmForm.Name + "_Top", Format(frmForm.Top));
            SaveSetting("ADRIFT", "Generator", frmForm.Name + "_Left", Format(frmForm.Left));
            SaveSetting("ADRIFT", "Generator", frmForm.Name + "_Height", Format(frmForm.Height));
            SaveSetting("ADRIFT", "Generator", frmForm.Name + "_Width", Format(frmForm.Width));
        }

        try
        {
            If colAllForms.Contains(frmForm.Handle.ToString) Then colAllForms.Remove(frmForm.Handle.ToString)
        }
        catch (Exception ex)
        {
            ErrMsg("SaveFormPosition", ex);
        }

    }


    public void GetFormPosition(ref frmForm As System.Windows.Forms.Form, UltraWinToolbars.UltraToolbarsManager utm = null, UltraWinDock.UltraDockManager udm = null, bool bRestoreSize = true, bool bExactPosition = false)
    {

        private FormWindowState iState;
        colAllForms.Add(frmForm, frmForm.Handle.ToString);

        'If rSession.SystemSettingInteger("SuppressWindowMemory", True) <> 0 Then Exit Sub

        ' Function to load the position of a form
        ' What state was it saved in?
        frmForm.AutoScaleMode = AutoScaleMode.Font;
        iState = CType(Val(GetSetting("ADRIFT", "Generator", frmForm.Name & "_State", System.Windows.Forms.FormWindowState.Normal.ToString)), FormWindowState)
        switch (iState)
        {
            case System.Windows.Forms.FormWindowState.Normal ' It was normal or unspecified - so carry on:
                {

                private int iTop = CInt(GetSetting("ADRIFT", "Generator", frmForm.Name + "_Top", frmForm.Top.ToString));
                private int iLeft = CInt(GetSetting("ADRIFT", "Generator", frmForm.Name + "_Left", frmForm.Left.ToString));
                private int iHeight = CInt(GetSetting("ADRIFT", "Generator", frmForm.Name + "_Height", frmForm.Height.ToString));
                private int iWidth = CInt(GetSetting("ADRIFT", "Generator", frmForm.Name + "_Width", frmForm.Width.ToString));

                If bExactPosition Then frmForm.Location = New Point(iLeft, iTop)
                If bRestoreSize Then frmForm.Size = New Size(iWidth, iHeight)

            case System.Windows.Forms.FormWindowState.Minimized ' It was minimised - so rest of data is rubbish:
                {

            case System.Windows.Forms.FormWindowState.Maximized ' It was maximised - so maximise!:
                {
                frmForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            case Else ' Err:
            case shouldn't get here:
                {
                ErrMsg("GetFormPosition Error");
        }
        If frmForm.Name <> "frmGenerator" && ! frmForm.Modal Then frmForm.ShowInTaskbar = SafeBool(GetSetting("ADRIFT", "Generator", "ShowInTaskBar", "0"))

        SetWindowStyle(frmForm, utm, udm);

    }



    public void SetWindowStyle(Form frmTarget, UltraWinToolbars.UltraToolbarsManager utm = null, UltraWinDock.UltraDockManager udm = null)
    {

        try
        {
            frmTarget.SuspendLayout();

            switch (eStyle)
            {
                case ToolbarStyle.Default:
                    {
                    'If Not utm Is Nothing Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2000
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.Default
                case ToolbarStyle.Office2003:
                    {
                    'If Not utm Is Nothing Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2003
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2003
                case ToolbarStyle.Office2007:
                    {
                    If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2007
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
                case ToolbarStyle.Office2010:
                    {
                    If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2010
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
                case ToolbarStyle.Office2013:
                    {
                    If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2013
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
                case ToolbarStyle.VisualStudio2005:
                    {
                    'If Not utm Is Nothing Then utm.Style = UltraWinToolbars.ToolbarStyle.VisualStudio2005
                    If udm != null Then udm.WindowStyle = UltraWinDock.WindowStyle.VisualStudio2005
                default:
                    {
            }

            For Each c As Control In frmTarget.Controls
                SetControlStyle(c);
            Next;

            frmTarget.ResumeLayout();
        Catch
        }

    }



    public void SetControlStyle(Control c)
    {

        'If TypeOf c Is UltraControlBase Then
        '    CType(c, UltraControlBase).StyleSetName = "c:\program files\Infragistics\NetAdvantage 2006 Volume 3 CLR 2.0\AppStylist\Styles\Office2007Blue.isl"
        'End If

        private EmbeddableElementDisplayStyle dsElement = EmbeddableElementDisplayStyle.Default;
        private UltraWinTabControl.ViewStyle vsTabs = UltraWinTabControl.ViewStyle.Default;
        private UltraWinStatusBar.ViewStyle vsStatusBar = UltraWinStatusBar.ViewStyle.Default;
        private Misc.GroupBoxViewStyle vsGroupBox = Misc.GroupBoxViewStyle.Default;
        private GlyphInfoBase giCheck = UIElementDrawParams.StandardCheckBoxGlyphInfo ' UIElementDrawParams.Office2007CheckBoxGlyphInfo;
        private Color culLabel = SystemColors.Control;
        private Infragistics.Win.UltraWinTree.UltraTreeDisplayStyle dsTree = UltraWinTree.UltraTreeDisplayStyle.Default;
        private GlyphInfoBase giRadio = UIElementDrawParams.StandardRadioButtonGlyphInfo;

        switch (eStyle)
        {
            case ToolbarStyle.Default:
                {
                dsElement = EmbeddableElementDisplayStyle.Standard
                vsTabs = UltraWinTabControl.ViewStyle.Standard
                vsStatusBar = UltraWinStatusBar.ViewStyle.Standard
                vsGroupBox = Misc.GroupBoxViewStyle.Office2000
                'giCheck = UIElementDrawParams.StandardCheckBoxGlyphInfo
                dsTree = UltraWinTree.UltraTreeDisplayStyle.Standard
            case ToolbarStyle.Office2003:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2003
                vsTabs = UltraWinTabControl.ViewStyle.Office2003
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2003
                vsGroupBox = Misc.GroupBoxViewStyle.Office2003
                dsTree = UltraWinTree.UltraTreeDisplayStyle.Standard
            case ToolbarStyle.Office2007:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2007
                vsTabs = UltraWinTabControl.ViewStyle.Office2007
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Office2007
                'gsCheck = GlyphStyle.Office2007
                giCheck = UIElementDrawParams.Office2007CheckBoxGlyphInfo
                giRadio = UIElementDrawParams.Office2007RadioButtonGlyphInfo
                culLabel = culOffice2007
                dsTree = UltraWinTree.UltraTreeDisplayStyle.WindowsVista
            case ToolbarStyle.Office2010:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2010
                vsTabs = UltraWinTabControl.ViewStyle.Standard ' UltraWinTabControl.ViewStyle.Office2007
                vsStatusBar = UltraWinStatusBar.ViewStyle.Standard ' UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Default ' Misc.GroupBoxViewStyle.Office2007
                giCheck = UIElementDrawParams.Office2010CheckBoxGlyphInfo
                giRadio = UIElementDrawParams.Office2010RadioButtonGlyphInfo
                culLabel = culOffice2007
                dsTree = UltraWinTree.UltraTreeDisplayStyle.WindowsVista
            case ToolbarStyle.Office2013:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2013
                vsTabs = UltraWinTabControl.ViewStyle.Standard ' UltraWinTabControl.ViewStyle.Office2007
                vsStatusBar = UltraWinStatusBar.ViewStyle.Standard ' UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Default ' Misc.GroupBoxViewStyle.Office2007
                giCheck = UIElementDrawParams.Office2013CheckBoxGlyphInfo
                giRadio = UIElementDrawParams.Office2013RadioButtonGlyphInfo
                culLabel = culOffice2013
                dsTree = UltraWinTree.UltraTreeDisplayStyle.WindowsVista
            case ToolbarStyle.VisualStudio2005:
                {
                dsElement = EmbeddableElementDisplayStyle.VisualStudio2005
                vsTabs = UltraWinTabControl.ViewStyle.VisualStudio2005
                vsStatusBar = UltraWinStatusBar.ViewStyle.VisualStudio2005
                vsGroupBox = Misc.GroupBoxViewStyle.VisualStudio2005
                dsTree = UltraWinTree.UltraTreeDisplayStyle.WindowsVista
        }

        switch (true)
        {
            case TypeOf c Is UltraWinEditors.UltraComboEditor:
                {
                (UltraWinEditors.UltraComboEditor)(c).DisplayStyle = dsElement;
            case TypeOf c Is UltraWinEditors.UltraDateTimeEditor:
                {
                (UltraWinEditors.UltraDateTimeEditor)(c).DisplayStyle = dsElement;
            case TypeOf c Is UltraWinTabControl.UltraTabControl:
                {
                private UltraWinTabControl.UltraTabControl tabs = CType(c, UltraWinTabControl.UltraTabControl);
                tabs.ViewStyle = vsTabs;
                'If rSession.bMultiRow Then
                'tabs.TabLayoutStyle = UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
                'Else
                tabs.TabLayoutStyle = UltraWinTabs.TabLayoutStyle.SingleRowAutoSize;
                'End If
            case TypeOf c Is UltraWinTabControl.UltraTabStripControl:
                {
                private UltraWinTabControl.UltraTabStripControl tabs = CType(c, UltraWinTabControl.UltraTabStripControl);
                tabs.ViewStyle = vsTabs;
                'tabs.HotTrack = rSession.bHotTracking
                'If rSession.bMultiRow Then
                'tabs.TabLayoutStyle = UltraWinTabs.TabLayoutStyle.MultiRowAutoSize
                'Else
                tabs.TabLayoutStyle = UltraWinTabs.TabLayoutStyle.SingleRowAutoSize;
                'End If
            case TypeOf c Is UltraWinStatusBar.UltraStatusBar:
                {
                (UltraWinStatusBar.UltraStatusBar)(c).ViewStyle = vsStatusBar;
                'ElseIf TypeOf c Is Misc.UltraButton Then
                'CType(c, Misc.UltraButton).HotTracking = rSession.bHotTracking
                If DarkInterface() Then (UltraWinStatusBar.UltraStatusBar)(c).Appearance.ForeColor = Color.White
            case TypeOf c Is Misc.UltraGroupBox:
                {
                (Misc.UltraGroupBox)(c).ViewStyle = vsGroupBox;
            case TypeOf c Is UltraWinProgressBar.UltraProgressBar:
                {
                (UltraWinProgressBar.UltraProgressBar)(c).Style = UltraWinProgressBar.ProgressBarStyle.Default;
            case TypeOf c Is UltraWinEditors.UltraCheckEditor:
                {
                (UltraWinEditors.UltraCheckEditor)(c).GlyphInfo = giCheck;
            case TypeOf c Is UltraWinEditors.UltraOptionSet:
                {
                (UltraWinEditors.UltraOptionSet)(c).GlyphInfo = giRadio;
            case TypeOf c Is Infragistics.Win.Misc.UltraLabel:
                {
                'If CStr(CType(c, Infragistics.Win.Misc.UltraLabel).Tag) = "bg" Then
                '    CType(c, Infragistics.Win.Misc.UltraLabel).Appearance.BackColor = culLabel
                'End If
            case TypeOf c Is Infragistics.Win.UltraWinTree.UltraTree:
                {
                (Infragistics.Win.UltraWinTree.UltraTree)(c).DisplayStyle = dsTree;
            case TypeOf c Is ADRIFT.ItemSelector:
                {
                'If c.BackColor = SystemColors.Control Then
                '    CType(c, Generator.ItemSelector).BackColor = culLabel ' Color.Transparent ' culLabel
                'End If
            case TypeOf c Is ADRIFT.XtoYturns:
                {
                'If c.BackColor = SystemColors.Control Then
                '    CType(c, Generator.XtoYturns).BackColor = culLabel
                'End If
            case TypeOf c Is ADRIFT.GenericProperty:
                {
                'If c.BackColor = SystemColors.Control Then
                '    With CType(c, Generator.GenericProperty)
                '        '.Background.Appearance.BackColor = culLabel
                '        '.chkSelected.BackColor = culLabel
                '        '.optSet.BackColor = culLabel
                '        '.optSet.Appearance.BackColorDisabled = culLabel
                '        'If eStyle = UltraWinTabControl.ViewStyle.Office2007 AndAlso eColour = Office2007ColorScheme.Black Then
                '        '    .ForeColor = Color.White
                '        '    .chkSelected.ForeColor = Color.White
                '        'Else
                '        '    .ForeColor = Color.Black
                '        '    .chkSelected.ForeColor = Color.Black
                '        'End If
                '    End With
                'End If
        }

        if (Not c.Controls Is null)
        {
            For Each cChild As Control In c.Controls
                SetControlStyle(cChild);
            Next;
        }

    }



    public void FillComboWithLocations(ref cmb As UltraWinEditors.UltraComboEditor)
    {

        cmb.Clear();
        cmb.Items.Add("", "<! Selected>");
        For Each loc As clsLocation In Adventure.htblLocations.Values
            cmb.Items.Add(loc.Key, loc.ShortDescription.ToString);
        Next;
        SetCombo(cmb, "");

    }


    public bool ComboContainsKey(Infragistics.Win.UltraWinEditors.UltraComboEditor cmb, string sKey)
    {

        ' We should be able to rewrite this to look at the underlying collection and use the key
        For Each vli As Infragistics.Win.ValueListItem In cmb.Items
            If CStr(vli.DataValue) = sKey Then Return true
        Next;

        return false;
    }



    'Public Function SelectedListItem() As ListViewItem

    '    For Each fList As frmList In fGenerator.MdiChildren
    '        If fList.ItemList.lvwList.SelectedItems.Count > 0 Then Return fList.ItemList.lvwList.SelectedItems(0)
    '    Next
    '    Return Nothing

    'End Function


    'Public Sub RemoveListItem(ByVal sKey As String)

    '    For Each fList As frmList In fGenerator.MdiChildren
    '        For Each lvi As ListViewItem In fList.ItemList.lvwList.Items
    '            If CStr(lvi.SubItems(2).Text) = sKey Then
    '                fList.ItemList.lvwList.Items.Remove(lvi)
    '                fList.ListInfo.arlKeys.Remove(lvi.SubItems(2).Text)
    '                If SelectedListItem() Is Nothing Then
    '                    fGenerator.UTMMain.Tools("Cut").SharedProps.Enabled = False
    '                    fGenerator.UTMMain.Tools("Copy").SharedProps.Enabled = False
    '                End If
    '                Exit Sub
    '            End If
    '        Next lvi
    '    Next fList

    'End Sub


    public void UpdateListItem(string sKey, string sName)
    {

        if (SafeBool(GetSetting("ADRIFT", "Generator", "HideLibraryItems", "-1")) && Adventure.IsItemLibrary(sKey))
        {
            switch (Adventure.GetTypeFromKeyNice(sKey))
            {
                case "Task":
                case "Property":
                    {
                    Exit Sub;
                default:
                    {
                    ' Fine
            }
        }

        ' Update existing items on open lists
        For Each frm As frmFolder In fGenerator.MDIFolders
            private frmFolder child = CType(frm, frmFolder);

            For Each lvi As Infragistics.Win.UltraWinListView.UltraListViewItem In child.Folder.lstContents.Items
                if (lvi.SubItems.Count > 3)
                {
                    if (CStr(lvi.SubItems(3).Text) = sKey)
                    {
                        lvi.Value = sName;
                        switch (Adventure.GetTypeFromKeyNice(sKey))
                        {
                            case "Task":
                                {
                                switch (Adventure.htblTasks(sKey).TaskType)
                                {
                                    case clsTask.TaskTypeEnum.General:
                                        {
                                        lvi.Appearance.Image = My.Resources.Resources.imgTaskGeneral16;
                                    case clsTask.TaskTypeEnum.Specific:
                                        {
                                        lvi.Appearance.Image = My.Resources.Resources.imgTaskSpecific16;
                                    case clsTask.TaskTypeEnum.System:
                                        {
                                        lvi.Appearance.Image = My.Resources.Resources.imgTaskSystem16;
                                }
                            case "Object":
                                {
                                if (Adventure.htblObjects(sKey).IsStatic)
                                {
                                    lvi.Appearance.Image = My.Resources.Resources.imgObjectStatic16;
                                Else
                                    lvi.Appearance.Image = My.Resources.Resources.imgObjectDynamic16;
                                }
                        }
                        lvi.SubItems(1).Value = Now;
                        Exit Sub;
                    }
                }
            Next;
        Next;

        ' Is it in a closed list?  If so, just exit
        For Each folder As clsFolder In Adventure.dictFolders.Values
            For Each sMember As String In folder.Members
                If sKey = sMember Then Exit Sub
            Next;
        Next;

        ' Not on any list so must be new - just add to active list
        if (fGenerator.sDestinationList <> "")
        {
            ' First, see if we have an open folder to put it in
            For Each frmChild As frmFolder In fGenerator.MDIFolders
                if (frmChild.Folder.folder.Key = fGenerator.sDestinationList)
                {
                    (frmFolder)(frmChild).Folder.AddSingleItem(sKey);
                    Exit Sub;
                }
            Next;
            ' Try to match a closed folder by key
            For Each folder As clsFolder In Adventure.dictFolders.Values
                if (folder.Key = fGenerator.sDestinationList)
                {
                    folder.Members.Add(sKey);
                    Exit Sub;
                }
            Next;
            ' Try to match an open folder by name
            For Each frmChild As frmFolder In fGenerator.MDIFolders
                if (frmChild.Folder.folder.Name = fGenerator.sDestinationList)
                {
                    (frmFolder)(frmChild).Folder.AddSingleItem(sKey);
                    Exit Sub;
                }
            Next;
            ' Try to match a closed folder by name
            For Each folder As clsFolder In Adventure.dictFolders.Values
                if (folder.Name = fGenerator.sDestinationList)
                {
                    folder.Members.Add(sKey);
                    Exit Sub;
                }
            Next;
        }

        ' If we find folder matching the type, bung it in there
        private string sType = Adventure.GetTypeFromKeyNice(sKey, true);
        For Each frmChild As frmFolder In fGenerator.MDIFolders
            if (frmChild.Folder.folder.Name = sType)
            {
                (frmFolder)(frmChild).Folder.AddSingleItem(sKey);
                Exit Sub;
            }
        Next;
        ' Try to match a closed folder by name
        For Each folder As clsFolder In Adventure.dictFolders.Values
            if (folder.Name = sType)
            {
                folder.Members.Add(sKey);
                Exit Sub;
            }
        Next;

        ' Finally, either stick it in the active folder, or the Root
        if (fGenerator.ActiveFolder IsNot null)
        {
            fGenerator.ActiveFolder.AddSingleItem(sKey);
        Else
            Adventure.dictFolders("ROOT").Members.Add(sKey);
        }

    }



    public clsBlorb OpenBlorbDialog(System.Windows.Forms.OpenFileDialog ofd)
    {


        ofd.Filter = "ADRIFT Blorb Files (*.blorb)|*.blorb|All Blorb Files (*.*blorb;*.blb;*.glb;*.zlb)|*.*blorb;*.blb;*.glb;*.zlb|All Files (*.*)|*.*";
        ofd.DefaultExt = "blorb";
        If ofd.FileName.Length > 5 && ! ofd.FileName.ToLower.EndsWith("blorb") Then ofd.FileName = ""
        if (ofd.ShowDialog() = DialogResult.OK && ofd.FileName <> "")
        {
            if (IO.File.Exists(ofd.FileName))
            {
#if DEBUG
                if (ofd.FileName.EndsWith("exe"))
                {
                    ' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
                    ' Grab out the last 8 bytes, and see if it converts to a size
                    Dim bData(5) As Byte
                    private New IO.FileStream(ofd.FileName, IO.FileMode.Open, IO.FileAccess.Read) fStream;
                    fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin);
                    fStream.Read(bData, 0, 6);
                    'fStream.Close()

                    private string sFileSize = (new System.Text.ASCIIEncoding).GetString(bData).ToUpper;

                    if (IsHex(sFileSize))
                    {
                        private long lFileSize = CLng("&H" + sFileSize);
                        if (lFileSize > 0)
                        {
                            Blorb = New clsBlorb
                            fStream.Seek(lFileSize, IO.SeekOrigin.Begin);
                            clsBlorb.bEXE = true;
                            if (Not Blorb.LoadBlorb(fStream, ofd.FileName, lFileSize))
                            {
                                ErrMsg("Error loading embedded Blorb");
                                return null;
                            }
                        }
                    }
                    fStream.Close();
                    return Blorb;
                }
#endif
                return LoadBlorb(ofd.FileName);
            Else
                ErrMsg("File not found!");
            }
        }

        return null;

    }


    public void OpenModuleDialog(System.Windows.Forms.OpenFileDialog ofd)
    {


        ofd.Filter = "ADRIFT Module Files (*.amf)|*.amf|All Files (*.*)|*.*";
        ofd.DefaultExt = "amf";
        If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("amf") Then ofd.FileName = ""
        if (ofd.ShowDialog() = DialogResult.OK && ofd.FileName <> "")
        {
            if (IO.File.Exists(ofd.FileName))
            {
                OpenModule(ofd.FileName);
            Else
                ErrMsg("File not found!");
            }
        }

    }


    public void OpenTrizbortDialog(System.Windows.Forms.OpenFileDialog ofd)
    {


        ofd.Filter = "Trizbort Map Files (*.trizbort)|*.trizbort|All Files (*.*)|*.*";
        ofd.DefaultExt = "trizbort";
        If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("trizbort") Then ofd.FileName = ""
        if (ofd.ShowDialog() = DialogResult.OK && ofd.FileName <> "")
        {
            if (IO.File.Exists(ofd.FileName))
            {
                ImportTrizbort(ofd.FileName);
            Else
                ErrMsg("File not found!");
            }
        }

    }


    private void PassSingleRestriction(clsRestriction rest, Dictionary(Of String dictReferences, clsItem)
    {

        switch (rest.eType)
        {
            case clsRestriction.RestrictionTypeEnum.Object:
                {
                For Each ref As String In dictReferences.Keys
                    if (ref = rest.sKey1)
                    {
                        private clsObject ob = CType(dictReferences(ref), clsObject);
                        switch (rest.eObject)
                        {
                            case clsRestriction.ObjectEnum.HaveProperty:
                                {
                                if (rest.eMust = clsRestriction.MustEnum.Must)
                                {
                                    If ! ob.HasProperty(rest.sKey2) Then Return false
                                Else
                                    If ob.HasProperty(rest.sKey2) Then Return false
                                }
                            case clsRestriction.ObjectEnum.BeHeldByCharacter:
                                {
                                ' Must be dynamic - may not actually be specified
                                if (rest.eMust = clsRestriction.MustEnum.Must)
                                {
                                    If ob.IsStatic Then Return false
                                }
                            case clsRestriction.ObjectEnum.BeWornByCharacter:
                                {
                                ' Must be wearable
                                if (rest.eMust = clsRestriction.MustEnum.Must)
                                {
                                    If ! ob.IsWearable Then Return false
                                }
                        }
                    }
                Next;
            case clsRestriction.RestrictionTypeEnum.Property:
                {
                For Each ref As String In dictReferences.Keys
                    if (ref = rest.sKey2)
                    {
                        private clsObject ob = CType(dictReferences(ref), clsObject);
                        if (rest.eMust = clsRestriction.MustEnum.Must)
                        {
                            If ! ob.HasProperty(rest.sKey1) Then Return false
                            If ob.GetPropertyValue(rest.sKey1) <> rest.StringValue Then Return false
                        }
                    }
                Next;
            case clsRestriction.RestrictionTypeEnum.Character:
                {
                if (rest.eMust = clsRestriction.MustEnum.Must)
                {
                    private clsObject ob = null;
                    Adventure.htblObjects.TryGetValue(rest.sKey2, ob);
                    if (ob IsNot null)
                    {
                        switch (rest.eCharacter)
                        {
                            case clsRestriction.CharacterEnum.BeHoldingObject:
                                {
                                If ob.IsStatic Then Return false
                            case clsRestriction.CharacterEnum.BeInsideObject:
                                {
                                If ! ob.IsContainer Then Return false
                            case clsRestriction.CharacterEnum.BeLyingOnObject:
                                {
                                If ! ob.HasProperty("Lieable") Then Return false
                            case clsRestriction.CharacterEnum.BeOnObject:
                                {
                                If ! ob.HasSurface Then Return false
                            case clsRestriction.CharacterEnum.BeSittingOnObject:
                                {
                                If ! ob.HasProperty("Sittable") Then Return false
                            case clsRestriction.CharacterEnum.BeStandingOnObject:
                                {
                                If ! ob.HasProperty("Standable") Then Return false
                            case clsRestriction.CharacterEnum.BeWearingObject:
                                {
                                If ! ob.IsWearable Then Return false
                        }
                    }
                }
        }

        return true;

    }


    public void PassRestrictions(RestrictionArrayList arlRestrictions, Dictionary(Of String dictReferences, clsItem)
    {

        iRestNum = 0

        if (tas IsNot null && tas.Parent <> "")
        {
            private clsTask tasParent = Adventure.htblTasks(tas.Parent);
            If ! PassRestrictions(tasParent.arlRestrictions, dictReferences, tasParent) Then Return false
            iRestNum = 0
        }

        if (arlRestrictions.Count = 0)
        {
            return true;
        Else
            return EvaluateRestrictionBlock(arlRestrictions, dictReferences, arlRestrictions.BracketSequence.Replace("[", "((").Replace("]", "))"), tas);
        }

    }


    ' Returns whether a task could pass with a particular set of restrictions
    private int iRestNum = 0;
    public void EvaluateRestrictionBlock(RestrictionArrayList arlRestrictions, Dictionary(Of String dictReferences, clsItem)
    {

        while (sBlock.Contains("A#O"))
        {
            private int i = sBlock.IndexOf("A#O");
            private int j = sBlock.Substring(0, i).LastIndexOf("(") + 1;
            sBlock = sLeft(sBlock, j) & "(" & sBlock.Substring(j, i - j) & "A#)O" & sBlock.Substring(i + 3)
        }

        switch (Left(sBlock, 1))
        {
            case "(":
                {
                ' Get block
                private int iBrackDepth = 1;
                private string sSubBlock = "(";
                private int iOffset = 1;
                while (iBrackDepth > 0)
                {
                    private string s = sBlock.Substring(iOffset, 1);
                    switch (s)
                    {
                        case "(":
                            {
                            iBrackDepth += 1;
                        case ")":
                            {
                            iBrackDepth -= 1;
                        default:
                            {
                            ' Do nothing
                    }
                    sSubBlock &= s;
                    iOffset += 1;
                }
                sSubBlock = sSubBlock.Substring(1, sSubBlock.Length - 2) 'Left(sSubBlock, sSubBlock.Length - 1)
                if (sBlock.Length - 2 = sSubBlock.Length)
                {
                    return EvaluateRestrictionBlock(arlRestrictions, dictReferences, sSubBlock);
                Else
                    switch (sBlock.Substring(sSubBlock.Length + 2, 1) 'sBlock.Substring(1, 1))
                    {
                        case "A":
                            {
                            private bool bFirst = EvaluateRestrictionBlock(arlRestrictions, dictReferences, sSubBlock);
                            if (Not bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(sSubBlock.Length + 3), "#"c);
                                return false;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, dictReferences, sBlock.Substring(sSubBlock.Length + 3));
                            }
                            'Return EvaluateRestrictionBlock(arlRestrictions, sSubBlock) AndAlso EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3)) 'sBlock.Substring(2))
                        case "O":
                            {
                            private bool bFirst = EvaluateRestrictionBlock(arlRestrictions, dictReferences, sSubBlock);
                            if (bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(sSubBlock.Length + 3), "#"c);
                                return true;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, dictReferences, sBlock.Substring(sSubBlock.Length + 3));
                            }
                            'Return EvaluateRestrictionBlock(arlRestrictions, sSubBlock) OrElse EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3)) 'sBlock.Substring(2))
                        default:
                            {
                            ' Error
                    }
                }
            case "#":
                {
                iRestNum += 1;
                if (sBlock.Length = 1)
                {
                    return PassSingleRestriction(arlRestrictions(iRestNum - 1), dictReferences);
                Else
                    switch (sBlock.Substring(1, 1))
                    {
                        case "A":
                            {
                            private bool bFirst = PassSingleRestriction(arlRestrictions(iRestNum - 1), dictReferences);
                            if (Not bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(2), "#"c);
                                return false;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, dictReferences, sBlock.Substring(2));
                            }
                            'Return PassSingleRestriction(arlRestrictions(iRestNum - 1)) AndAlso EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2))
                        case "O":
                            {
                            private bool bFirst = PassSingleRestriction(arlRestrictions(iRestNum - 1), dictReferences);
                            if (bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(2), "#"c);
                                return true;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, dictReferences, sBlock.Substring(2));
                            }
                            'Return PassSingleRestriction(arlRestrictions(iRestNum - 1)) OrElse EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2))
                        default:
                            {
                            ' Error
                    }
                }
            default:
                {
                ErrMsg("Bad Bracket Sequence");

        }

    }


    public void ShowHelp(Form form, string sTopic = null)
    {

        private string sHelpURL = fGenerator.HelpProvider1.HelpNamespace;

        if (IO.File.Exists(sHelpURL))
        {
            if (sTopic Is null)
            {
                Help.ShowHelp(form, sHelpURL, HelpNavigator.TableOfContents);
            Else
                Help.ShowHelp(form, sHelpURL, HelpNavigator.Topic, sTopic + ".htm");
            }
        Else
            sHelpURL = "http://help.adrift.co"
            if (sTopic Is null)
            {
                Help.ShowHelp(form, sHelpURL, HelpNavigator.TableOfContents);
            Else
                sHelpURL &= "/" + sTopic + ".html";
                Help.ShowHelp(form, sHelpURL, HelpNavigator.Topic, sTopic);
            }
        }

    }


    public void OpenALRDialog(System.Windows.Forms.OpenFileDialog ofd)
    {


        ofd.Filter = "Language Resource Files (*.alr)|*.alr|All Files (*.*)|*.*";
        ofd.DefaultExt = "alr";
        If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("alr") Then ofd.FileName = ""
        if (ofd.ShowDialog() = DialogResult.OK && ofd.FileName <> "")
        {
            if (IO.File.Exists(ofd.FileName))
            {
                ImportALR(ofd.FileName);
            Else
                ErrMsg("File not found!");
            }
        }

    }


    public void GetReferences(ReferencesType reftype, string sCommand, List(Of clsUserFunction.Argument Arguments)
    {

        If sCommand == null && Arguments == null Then Return New StringArrayList

        private New StringArrayList sal;
        If sCommand != null Then sCommand = sCommand.ToLower

        switch (reftype)
        {
            case ReferencesType.Location:
                {
                If sInstr(sCommand, "%location%") > 0 Then sal.Add("ReferencedLocation")
                if (Arguments IsNot null)
                {
                    For Each arg As clsUserFunction.Argument In Arguments
                        if (arg.Type = clsUserFunction.ArgumentType.Location)
                        {
                            sal.Add("Parameter-" + arg.Name);
                        }
                    Next;
                }
            case ReferencesType.Object:
                {
                if (sInstr(sCommand, "%objects%") > 0)
                {
                    sal.Add("ReferencedObjects");
                }
                If sInstr(sCommand, "%object%") > 0 Then sal.Add("ReferencedObject1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%object" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedObject" + iRef) Then sal.Add("ReferencedObject" + iRef)
                    }
                Next;
                if (Arguments IsNot null)
                {
                    For Each arg As clsUserFunction.Argument In Arguments
                        if (arg.Type = clsUserFunction.ArgumentType.Object)
                        {
                            sal.Add("Parameter-" + arg.Name);
                        }
                    Next;
                }
            case ReferencesType.Character:
                {
                if (sInstr(sCommand, "%characters%") > 0)
                {
                    sal.Add("ReferencedCharacters");
                }
                If sInstr(sCommand, "%character%") > 0 Then sal.Add("ReferencedCharacter1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%character" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedCharacter" + iRef) Then sal.Add("ReferencedCharacter" + iRef)
                    }
                Next;
                if (Arguments IsNot null)
                {
                    For Each arg As clsUserFunction.Argument In Arguments
                        if (arg.Type = clsUserFunction.ArgumentType.Character)
                        {
                            sal.Add("Parameter-" + arg.Name);
                        }
                    Next;
                }
            case ReferencesType.Item:
                {
                If sInstr(sCommand, "%item%") > 0 Then sal.Add("ReferencedItem1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%item" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedItem" + iRef) Then sal.Add("ReferencedItem" + iRef)
                    }
                Next;
            case ReferencesType.Direction:
                {
                If sInstr(sCommand, "%direction%") > 0 Then sal.Add("ReferencedDirection1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%direction" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedDirection" + iRef) Then sal.Add("ReferencedDirection" + iRef)
                    }
                Next;
            case ReferencesType.Number:
                {
                If sInstr(sCommand, "%number%") > 0 Then sal.Add("ReferencedNumber1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%number" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedNumber" + iRef) Then sal.Add("ReferencedNumber" + iRef)
                    }
                Next;
                If sInstr(sCommand, "%t_number%") > 0 && ! sal.Contains("ReferencedNumber1") Then sal.Add("ReferencedNumber1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%t_number" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedNumber" + iRef) Then sal.Add("ReferencedNumber" + iRef)
                    }
                Next;
                if (Arguments IsNot null)
                {
                    For Each arg As clsUserFunction.Argument In Arguments
                        if (arg.Type = clsUserFunction.ArgumentType.Number)
                        {
                            sal.Add("Parameter-" + arg.Name);
                        }
                    Next;
                }
            case ReferencesType.Text:
                {
                If sInstr(sCommand, "%text%") > 0 Then sal.Add("ReferencedText1")
                for (int iRef = 1; iRef <= 5; iRef++)
                {
                    if (sInstr(sCommand, "%text" + iRef + "%") > 0)
                    {
                        If ! sal.Contains("ReferencedText" + iRef) Then sal.Add("ReferencedText" + iRef)
                    }
                Next;
                if (Arguments IsNot null)
                {
                    For Each arg As clsUserFunction.Argument In Arguments
                        if (arg.Type = clsUserFunction.ArgumentType.Text)
                        {
                            sal.Add("Parameter-" + arg.Name);
                        }
                    Next;
                }
        }

        If sal.Contains("ReferencedObject1") && ! sal.Contains("ReferencedObject2") Then sal(sal.IndexOf("ReferencedObject1")) = "ReferencedObject"
        If sal.Contains("ReferencedCharacter1") && ! sal.Contains("ReferencedCharacter2") Then sal(sal.IndexOf("ReferencedCharacter1")) = "ReferencedCharacter"
        If sal.Contains("ReferencedDirection1") && ! sal.Contains("ReferencedDirection2") Then sal(sal.IndexOf("ReferencedDirection1")) = "ReferencedDirection"
        If sal.Contains("ReferencedNumber1") && ! sal.Contains("ReferencedNumber2") Then sal(sal.IndexOf("ReferencedNumber1")) = "ReferencedNumber"
        If sal.Contains("ReferencedText1") && ! sal.Contains("ReferencedText2") Then sal(sal.IndexOf("ReferencedText1")) = "ReferencedText"
        If sal.Contains("ReferencedLocation1") && ! sal.Contains("ReferencedLocation2") Then sal(sal.IndexOf("ReferencedLocation1")) = "ReferencedLocation"
        If sal.Contains("ReferencedItem1") && ! sal.Contains("ReferencedItem2") Then sal(sal.IndexOf("ReferencedItem1")) = "ReferencedItem"

        return sal;

    }

    private int CountInstr(string sText, string sFind)
    {

        private int iOffset;
        CountInstr = 0

        while (sText.IndexOf(sFind, iOffset) > -1)
        {
            CountInstr += 1;
            iOffset = sText.IndexOf(sFind, iOffset) + 1
        }

    }


    public void LoadDefaults()
    {
        CreatePlayer();
        CreateRequiredProperties();
    }


    private void CreatePlayer()
    {
        private New clsCharacter Player;
        With Player;
            .ProperName = "Player";
            .CharacterType = clsCharacter.CharacterTypeEnum.Player;
            .Key = "Player";
        }
        Adventure.htblCharacters.Add(Player, Player.Key);
    }

    private void CreateRequiredProperties()
    {

        private New clsProperty pStaticDynamic;
        With pStaticDynamic;
            .Key = "StaticOrDynamic";
            .Description = "Object type";
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Static");
            .arlStates.Add("Dynamic");
            .Mandatory = true;
        }
        Adventure.htblAllProperties.Add(pStaticDynamic);

        private New clsProperty pSurface;
        With pSurface;
            .Key = "Surface";
            .Description = "This object has a surface";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pSurface);

        private New clsProperty pSurfaceHold;
        With pSurfaceHold;
            .Key = "SurfaceHold";
            .Description = "... and the surface can hold";
            .Type = clsProperty.PropertyTypeEnum.Integer;
            .DependentKey = "Surface";
        }
        Adventure.htblAllProperties.Add(pSurfaceHold);

        private New clsProperty pReadable;
        With pReadable;
            .Key = "Readable";
            .Description = "This object is readable";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pReadable);

        private New clsProperty pReadText;
        With pReadText;
            .Key = "ReadText";
            .Description = "... and description when read";
            .Type = clsProperty.PropertyTypeEnum.Text;
            .DependentKey = "Readable";
        }
        Adventure.htblAllProperties.Add(pReadText);

        private New clsProperty pWearable;
        With pWearable;
            .Key = "Wearable";
            .Description = "Wearable";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Dynamic";
        }
        Adventure.htblAllProperties.Add(pWearable);

        private New clsProperty pContainer;
        With pContainer;
            .Key = "Container";
            .Description = "This object is a container";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pContainer);

        private New clsProperty pContainerHold;
        With pContainerHold;
            .Key = "ContainerHold";
            .Description = "... and the container can hold";
            .Type = clsProperty.PropertyTypeEnum.Integer;
            .DependentKey = "Container";
        }
        Adventure.htblAllProperties.Add(pContainerHold);

        private New clsProperty pOpenable;
        With pOpenable;
            .Key = "Openable";
            .Description = "Object can be opened and closed, starting off";
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Open");
            .arlStates.Add("Closed");
        }
        Adventure.htblAllProperties.Add(pOpenable);

        private New clsProperty pLockable;
        With pLockable;
            .Key = "Lockable";
            .Description = "... and is lockable, with key";
            .Type = clsProperty.PropertyTypeEnum.ObjectKey;
            .DependentKey = "Openable";
        }
        Adventure.htblAllProperties.Add(pLockable);

        private New clsProperty pLocked;
        With pLocked;
            .Key = "Locked";
            .Description = "... and starts off locked";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
            .DependentKey = "Lockable";
        }
        Adventure.htblAllProperties.Add(pLocked);

        private New clsProperty pSittable;
        With pSittable;
            .Key = "Sittable";
            .Description = "Characters can sit on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pSittable);

        private New clsProperty pStandable;
        With pStandable;
            .Key = "Standable";
            .Description = "Characters can stand on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pStandable);

        private New clsProperty pLieable;
        With pLieable;
            .Key = "Lieable";
            .Description = "Characters can lie on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pLieable);

        private New clsProperty pEdible;
        With pEdible;
            .Key = "Edible";
            .Description = "Object is edible";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Dynamic";
        }
        Adventure.htblAllProperties.Add(pEdible);

        private New clsProperty pDynamicLocation;
        With pDynamicLocation;
            .Key = "DynamicLocation";
            .Description = "Location of the object";
            .Mandatory = true;
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Hidden");
            .arlStates.Add("Held By Character");
            .arlStates.Add("Worn By Character");
            .arlStates.Add("In Location");
            .arlStates.Add("Inside Object");
            .arlStates.Add("On Object");
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Dynamic";
        }
        Adventure.htblAllProperties.Add(pDynamicLocation);

        private New clsProperty pHeldByWho;
        With pHeldByWho;
            .Key = "HeldByWho";
            .Description = "Held by who";
            .Mandatory = true;
            .Type = clsProperty.PropertyTypeEnum.CharacterKey;
            .DependentKey = "DynamicLocation";
            .DependentValue = "Held By Character";
        }
        Adventure.htblAllProperties.Add(pHeldByWho);

        private New clsProperty pInLocation;
        With pInLocation;
            .Key = "InLocation";
            .Description = "In location";
            .Mandatory = true;
            .Type = clsProperty.PropertyTypeEnum.LocationKey;
            .DependentKey = "DynamicLocation";
            .DependentValue = "In Location";
        }
        Adventure.htblAllProperties.Add(pInLocation);

        private New clsProperty pStaticLocation;
        With pStaticLocation;
            .Key = "StaticLocation";
            .Description = "Location of the object";
            .Mandatory = true;
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Nowhere");
            .arlStates.Add("Single Location");
            .arlStates.Add("Multiple Locations");
            .arlStates.Add("All Locations");
            .arlStates.Add("Part Of Character");
            .arlStates.Add("Part Of Object");
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Static";
        }
        Adventure.htblAllProperties.Add(pStaticLocation);

    }


    'Public Function GetFunctionArgs(ByVal sText As String) As String

    '    If sInstr(sText, "[") = 0 OrElse sInstr(sText, "]") = 0 Then Return ""

    '    ' Work out this bracket chunk, then run ReplaceFunctions on it
    '    Dim iOffset As Integer = 1
    '    Dim iLevel As Integer = 1

    '    While iLevel > 0
    '        Select Case sText.Substring(iOffset, 1)
    '            Case "["
    '                iLevel += 1
    '            Case "]"
    '                iLevel -= 1
    '            Case Else
    '                ' Ignore
    '        End Select
    '        iOffset += 1
    '    End While

    '    Return sText.Substring(1, iOffset - 2)

    'End Function


    'Public Function ReplaceFunctions(ByVal sText As String) As String

    '    If sInstr(sText, "%") = 0 Then Return sText

    '    'sText = sText.Replace("%player%", Player.Key)
    '    sText = sText.Replace("%object%", "%object1%")


    '    For Each sFunction As String In FunctionNames()
    '        While sInstr(sText, "%" & sFunction & "[") > 0
    '            Dim sArgs As String = GetFunctionArgs(sText.Substring(InStr(sText, "%" & sFunction & "[") + sFunction.Length))
    '            Dim iArgsLength As Integer = sArgs.Length
    '            If iArgsLength > 0 Then
    '                sArgs = ReplaceFunctions(sArgs)
    '                If sInstr(sText, "%" & sFunction & "[" & sArgs & "]%") > 0 Then

    '                    Dim sResult As String = ""
    '                    Dim htblObjects As New ObjectHashTable

    '                    Select Case sArgs
    '                        Case "%objects%"

    '                        Case Else
    '                            If Adventure.htblObjects.ContainsKey(sArgs) Then htblObjects.Add(Adventure.htblObjects(sArgs), sArgs)
    '                    End Select

    '                    Select Case sFunction
    '                        Case "DisplayObject"
    '                            sResult = htblObjects(sArgs).Description
    '                        Case "TheObject", "TheObjects"
    '                            sResult = htblObjects.List
    '                        Case "DisplayLocation"
    '                            sResult = Adventure.htblLocations(sArgs).ViewLocation
    '                        Case "LocationOf"
    '                            sResult = Adventure.htblCharacters(sArgs).Location
    '                        Case "LCase"
    '                            sResult = sArgs.ToLower
    '                        Case "ListHeld"
    '                            'sResult = Adventure.htblCharacters(sArgs).HeldObjects.List("and", True, False)
    '                        Case "ListExits"
    '                            'sResult = Adventure.htblCharacters(sArgs).ListExits
    '                        Case "ListObjectsInLocation"
    '                            sResult = Adventure.htblLocations(sArgs).ObjectsInLocation.List("and", False, False)
    '                    End Select

    '                    If sResult = "" Then
    '                        sResult = "<c>*** Bad Function ***</c>"
    '                    End If

    '                    sText = Replace(sText, "%" & sFunction & "[" & sArgs & "]%", sResult)

    '                Else
    '                    sText = Replace(sText, "%" & sFunction & "[", "*** HERE ***")
    '                End If
    '            Else
    '                sText = Replace(sText, "%" & sFunction & "[]%", "")
    '                sText = Replace(sText, "%" & sFunction & "[", "")
    '            End If
    '        End While
    '    Next

    '    Return sText

    'End Function

}


}