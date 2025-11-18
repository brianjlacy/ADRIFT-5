using Infragistics.Win;
using Infragistics.Win.UltraWinListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADRIFT
{

public class SearchReplace
{

    private bool bLoaded = false;
    private Folder fFolder;

    private void btnSelectFind_Click(System.Object sender, System.EventArgs e)
    {
        If btnSelectFind.Checked Then Exit Sub
        btnSelectFind.Checked = true;
        btnSelectReplace.Checked = false;
        pnlReplace.Visible = false;
        lvwFoundItems.Visible = false;
        btnReplace.Visible = false;
        btnReplaceAll.Visible = false;
        btnFindAll.Visible = true;
        btnFind.Left += 91;
        FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        ResizeForm();
    }

    private void btnSelectReplace_Click(System.Object sender, System.EventArgs e)
    {
        If btnSelectReplace.Checked Then Exit Sub
        btnSelectReplace.Checked = true;
        btnSelectFind.Checked = false;
        pnlReplace.Visible = true;
        lvwFoundItems.Visible = false;
        If bLoaded Then btnFind.Left -= 91
        btnFindAll.Visible = false;
        btnReplace.Visible = true;
        btnReplaceAll.Visible = true;
        FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow

        ' Something cuckoo started happening with z-order
        pnlLookIn.SendToBack();
        pnlReplace.SendToBack();
        pnlFind.SendToBack();
        Me.toolsSearchReplace.SendToBack();
        btnReplace.BringToFront();
        btnReplaceAll.BringToFront();

        ResizeForm();
    }


    private void ResizeForm()
    {
        private int iListHeight = 0;
        If lvwFoundItems.Visible Then iListHeight = lvwFoundItems.Height
        if (btnSelectFind.Checked)
        {
            Me.ClientSize = New Size(350, toolsSearchReplace.Height + pnlFind.Height + pnlLookIn.Height + grpSearchOptions.Height + StatusBar.Height + iListHeight) ' + 22);
        Else
            Me.ClientSize = New Size(350, toolsSearchReplace.Height + pnlFind.Height + pnlReplace.Height + pnlLookIn.Height + grpSearchOptions.Height + StatusBar.Height + iListHeight) '+ 22);
        }
    }

    public void SetFind()
    {
        btnSelectFind_Click(btnSelectFind, null);
    }


    public void SetReplace()
    {
        btnSelectReplace_Click(btnSelectReplace, null);
    }

    private void btnFind_Click(System.Object sender, System.EventArgs e)
    {

        For Each vli As ValueListItem In cmbFind.Items
            if (vli.DisplayText = cmbFind.Text)
            {
                cmbFind.Items.Remove(vli);
                Exit For;
            }
        Next;
        cmbFind.Items.Add(cmbFind.Text);

        if (cmbFind.Text <> "")
        {
            if (Search(cmbFind.Text))
            {
                btnFind.Text = "&Find Next";
            Else
                btnFind.Text = "&Find";
            }
        }

    }


    private void cmbFind_ValueChanged(System.Object sender, System.EventArgs e)
    {
        btnFind.Enabled = cmbFind.Text.Length > 0;
        btnFindAll.Enabled = cmbFind.Text.Length > 0;
        btnReplaceAll.Enabled = cmbFind.Text.Length > 0;
        btnFind.Text = "&Find";
    }


    private void grpSearchOptions_ExpandedStateChanged(object sender, System.EventArgs e)
    {
        ResizeForm();
        SaveSetting("ADRIFT", "Generator", "ShowSearchOptions", CInt(grpSearchOptions.Expanded).ToString);
    }


    private void SearchReplace_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        SaveFormPosition(Me);
    }


    private void SearchReplace_Load(object sender, System.EventArgs e)
    {
        fSearch = Me
        SetCombo(cmbLookIn, CInt(SearchOptions.SearchInWhat));
        grpSearchOptions.Expanded = CBool(GetSetting("ADRIFT", "Generator", "ShowSearchOptions", "0"));
        GetFormPosition(Me, , , false);

        lvwFoundItems.MainColumn.Key = "Item";
        lvwFoundItems.MainColumn.Text = "Item";
        lvwFoundItems.MainColumn.Width = CInt(lvwFoundItems.Width * 0.6);
        private New UltraListViewSubItemColumn colFolder;
        colFolder.Key = "Folder";
        colFolder.Text = "Folder";
        colFolder.Width = lvwFoundItems.Width - CInt(lvwFoundItems.Width * 0.6);
        lvwFoundItems.SubItemColumns.Add(colFolder) '"Folder", lvwFoundItems.Width - CInt(lvwFoundItems.Width * 0.6));
        lvwFoundItems.SubItemColumns.Add("sFolderKey");
        lvwFoundItems.SubItemColumns("sFolderKey").VisibleInDetailsView = DefaultableBoolean.false;
        'lvwFoundItems.FullRowSelect = True
        lvwFoundItems.View = UltraWinListView.UltraListViewStyle.Details;

        bLoaded = True
    }


    private void SearchReplace_Resize(object sender, System.EventArgs e)
    {
        if (lvwFoundItems.SubItemColumns.Count > 0)
        {
            lvwFoundItems.SubItemColumns(0).Width = 180;
            lvwFoundItems.MainColumn.Width = lvwFoundItems.Width - lvwFoundItems.SubItemColumns(0).Width - 2;
        }
    }


    private void SearchReplace_Shown(object sender, System.EventArgs e)
    {
        cmbFind.Focus();
    }


    private void btnReplaceAll_Click(object sender, System.EventArgs e)
    {

        For Each vli As ValueListItem In cmbFind.Items
            if (vli.DisplayText = cmbFind.Text)
            {
                cmbFind.Items.Remove(vli);
                Exit For;
            }
        Next;
        cmbFind.Items.Add(cmbFind.Text);

        For Each vli As ValueListItem In cmbReplace.Items
            if (vli.DisplayText = cmbFind.Text)
            {
                cmbReplace.Items.Remove(vli);
                Exit For;
            }
        Next;
        cmbReplace.Items.Add(cmbReplace.Text);

        If cmbFind.Text <> "" Then SearchAndReplace(cmbFind.Text, cmbReplace.Text)

    }


    private void cmbLookIn_SelectionChanged(object sender, System.EventArgs e)
    {
        If bLoaded Then SearchOptions.SearchInWhat = (clsSearchOptions.SearchInWhatEnum)(cmbLookIn.SelectedItem.DataValue)
    }

    private void chkMatchCase_CheckedChanged(object sender, System.EventArgs e)
    {
        If bLoaded Then SearchOptions.bSearchMatchCase = chkMatchCase.Checked
    }

    private void chkMatchWholeWord_CheckedChanged(object sender, System.EventArgs e)
    {
        If bLoaded Then SearchOptions.bFindExactWord = chkMatchWholeWord.Checked
    }

    private void btnClose_Click(object sender, System.EventArgs e)
    {
        Me.Close();
    }

    private void btnFindAll_Click(System.Object sender, System.EventArgs e)
    {

        If fFolder == null Then fFolder = New Folder

        For Each vli As ValueListItem In cmbFind.Items
            if (vli.DisplayText = cmbFind.Text)
            {
                cmbFind.Items.Remove(vli);
                Exit For;
            }
        Next;
        cmbFind.Items.Add(cmbFind.Text);

        if (cmbFind.Text <> "")
        {
            lvwFoundItems.Items.Clear();
            For Each item As clsItem In FindAll(cmbFind.Text)

                Dim subitems() As UltraListViewSubItem = {New UltraListViewSubItem, New UltraListViewSubItem}

                private clsFolder folder = GetFolder(item);
                if (folder IsNot null)
                {
                    subitems(0).Value = folder.Name;
                    subitems(1).Value = folder.Key;

                    ' Shouldn't already exist, but we've had issues with duplicate keys with different case
                    If ! lvwFoundItems.Items.Exists(item.Key) Then lvwFoundItems.Items.Add(fFolder.GetItem(item, subitems))
                }

            Next;
        }

        FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        lvwFoundItems.Visible = true;

        If Me.Height < 500 Then Me.Height = 500

    }

    private clsFolder GetFolder(clsItem item)
    {

        For Each Folder As clsFolder In Adventure.dictFolders.Values
            If Folder.ContainsKey(item.Key, false) Then Return Folder
        Next;
        return null;

    }


    private void lvwFoundItems_ItemDoubleClick(object sender, ItemDoubleClickEventArgs e)
    {
        EditItem(e.Item);
    }


    private void miEditItem_Click(System.Object sender, System.EventArgs e)
    {
        If lvwFoundItems.SelectedItems.Count = 1 Then EditItem(lvwFoundItems.SelectedItems(0))
    }


    private void EditItem(UltraListViewItem lvi)
    {
        Me.TopMost = false;
        private string sKey = lvi.Key;
        fFolder.Edit(Adventure.GetTypeFromKeyNice(sKey), , sKey);
    }


    private void miOpenFolder_Click(System.Object sender, System.EventArgs e)
    {

        if (lvwFoundItems.SelectedItems.Count = 1)
        {
            private string sFolderKey = lvwFoundItems.SelectedItems(0).SubItems(1).Value.ToString;
            fGenerator.FolderList1.OpenNewFolder(sFolderKey);
            For Each child As frmFolder In fGenerator.MDIFolders
                if (child.Folder.folder.Key = sFolderKey)
                {
                    For Each lvi As UltraListViewItem In child.Folder.lstContents.Items
                        if (lvi.Key = lvwFoundItems.SelectedItems(0).Key)
                        {
                            child.Folder.lstContents.SelectedItems.Clear();
                            child.Folder.lstContents.SelectedItems.Add(lvi);
                            lvi.Activate();
                            child.Focus();
                        }
                    Next;
                    Exit For;
                }
            Next;
        }

    }


    private void lvwFoundItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private UltraListViewItem itemCursor = lvwFoundItems.ItemFromPoint(lvwFoundItems.PointToClient(MousePosition));

        ' If user right-clicks on the text (rather than just the item), select the item
        if (itemCursor IsNot null && Not itemCursor.IsSelected && lvwFoundItems.PointToClient(MousePosition).X < lvwFoundItems.Width / 2)
        {
            lvwFoundItems.SelectedItems.Clear();
            lvwFoundItems.SelectedItems.Add(itemCursor);
            lvwFoundItems.ActiveItem = itemCursor;
        }

    }

}

}