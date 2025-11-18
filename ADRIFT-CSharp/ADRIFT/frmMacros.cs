using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
Option Strict On;

public class frmMacros
{

    'Friend htblMacros As StringHashTable
    internal Generic.Dictionary<string, clsMacro> ldictMacros;
    private New StringArrayList arlKeys;
    private bool bSetting = false;

    private bool bChanged = false;
    private bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Boolean)
            bChanged = value
            btnApply.Enabled = bChanged;
        }
    }


    private void btnAdd_Click(System.Object sender, System.EventArgs e)
    {
        private string sTitle = InputBox("Enter menu title for macro:", "Create Macro", "");
        if (sTitle <> "")
        {
            private string sKey = Guid.NewGuid.ToString;
            private New clsMacro(sKey) macro;
            macro.Title = sTitle;
            macro.IFID = Adventure.BabelTreatyInfo.Stories(0).Identification.IFID;

            ldictMacros.Add(macro.Key, macro);
            lstMacros.Items.Add(sTitle);
            arlKeys.Add(macro.Key);
            lstMacros.SelectedIndex = lstMacros.Items.Count - 1;
            Changed = True
            'htblMacros.Add(sTitle, "")
        }
    }


    private void lstMacros_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {

        if (lstMacros.SelectedItem IsNot null)
        {
            private bool bTemp = bChanged;
            bSetting = True
            private string sKey = arlKeys(lstMacros.SelectedIndex) ' GetKeyFromTitle(lstMacros.SelectedItem.ToString);
            if (sKey <> "")
            {
                With ldictMacros(sKey);
                    txtCommands.Text = .Commands;
                    chkShared.Checked = .Shared;
#if Mono
                    cmbShortcut.Text = .Shortcut.ToString;
#else
                    SetCombo(cmbShortcut, .Shortcut);
#endif

                }
            }
            btnUp.Enabled = lstMacros.SelectedIndex > 0;
            btnDown.Enabled = lstMacros.SelectedIndex < lstMacros.Items.Count - 1;
            chkShared.Enabled = true;
            txtCommands.Enabled = true;
            btnRemove.Enabled = true;
            cmbShortcut.Enabled = true;

            bSetting = False
            bChanged = bTemp
        }

    }


    'Private Sub LoadCommands(ByVal sTitle As String)

    '    If ldictMacros.ContainsKey(sTitle) Then
    '        txtCommands.Text = ldictMacros(sTitle).Commands
    '    Else
    '        txtCommands.Clear()
    '    End If
    '    txtCommands.Enabled = True

    'End Sub

    private void txtCommands_Move(object sender, System.EventArgs e)
    {
        lblCommands.Left = SplitContainer1.SplitterDistance + SplitContainer1.Left + SplitContainer1.SplitterWidth;
    }

    private void txtCommands_GotFocus(object sender, System.EventArgs e)
    {
        AcceptButton = Nothing
    }

    private void txtCommands_LostFocus(object sender, System.EventArgs e)
    {
        AcceptButton = btnOK
    }

    private void txtCommands_TextChanged(object sender, System.EventArgs e)
    {

        If bSetting Then Exit Sub

        if (lstMacros.SelectedItem IsNot null)
        {
            private string sKey = arlKeys(lstMacros.SelectedIndex) ' GetKeyFromTitle(lstMacros.SelectedItem.ToString);
            If sKey <> "" Then ldictMacros(sKey).Commands = txtCommands.Text

            Changed = True
        }

    }


    private void btnCancel_Click(object sender, System.EventArgs e)
    {
        if (Changed)
        {
            if (MessageBox.Show("Lose changes?", "Edit Macros", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = Nothing
                Exit Sub;
            }
        }

        DialogResult = Windows.Forms.DialogResult.Cancel
        Close();

    }


    'Private Function GetKeyFromTitle(ByVal sTitle As String) As String
    '    ' Find the key from the title
    '    Dim sKey As String = ""
    '    For Each macro As clsMacro In ldictMacros.Values
    '        If macro.Title = sTitle AndAlso (macro.Shared OrElse macro.IFID = Adventure.BabelTreatyInfo.Stories(0).Identification.IFID) Then
    '            Return macro.Key
    '        End If
    '    Next
    '    Return ""
    'End Function


    private void Apply()
    {

        With UserSession;
            .dictMacros = New Generic.Dictionary(Of String, clsMacro);

            for (int i = 0; i <= lstMacros.Items.Count - 1 ' Each sTitle As String In lstMacros.Items; i++)
            {
                private string sKey = arlKeys(i) ' GetKeyFromTitle(sTitle);
                if (sKey <> "")
                {
                    If ! .dictMacros.ContainsKey(sKey) Then .dictMacros.Add(sKey, ldictMacros(sKey))
                }
            Next;
            For Each macro As clsMacro In ldictMacros.Values
                if (Not .dictMacros.ContainsKey(macro.Key))
                {
                    .dictMacros.Add(macro.Key, macro);
                }
            Next;
            'dictMacros = ldictMacros
            .SaveMacros();
            fRunner.ReloadMacros();
            Changed = False
        }

    }


    private void btnOK_Click(object sender, System.EventArgs e)
    {
        Apply();
        DialogResult = Windows.Forms.DialogResult.OK
        Close();
    }


    private void frmMacros_Load(object sender, System.EventArgs e)
    {

        GetFormPosition(Me);

#if Mono
        btnUp.Text = ChrW(&H25B2);
        btnUp.Font = New Font("OpenSymbol", btnUp.Font.Size);
        btnDown.Text = ChrW(&H25BC);
        btnDown.Font = New Font("OpenSymbol", btnUp.Font.Size);
#endif
        For Each key As Keys In New Keys() {Keys.None, Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12}
#if Mono
            cmbShortcut.Items.Add(New ComboBoxItem(key));
#else
            cmbShortcut.Items.Add(key, key.ToString);
#endif
        Next;

        For Each sKey As String In ldictMacros.Keys
            if (ldictMacros.ContainsKey(sKey))
            {
                With ldictMacros(sKey);
                    if (.IFID = Adventure.BabelTreatyInfo.Stories(0).Identification.IFID || .Shared)
                    {
                        lstMacros.Items.Add(.Title);
                        arlKeys.Add(.Key);
                    }
                }
            }
        Next;

#if Mono
        cmbShortcut.Text = "None";
#else
        SetCombo(cmbShortcut, Keys.None);
#endif

        Changed = False

    }


    private void chkShared_CheckedChanged(object sender, System.EventArgs e)
    {

        If bSetting Then Exit Sub

        if (lstMacros.SelectedItem IsNot null)
        {
            private string sKey = arlKeys(lstMacros.SelectedIndex) ' GetKeyFromTitle(lstMacros.SelectedItem.ToString);
            if (sKey <> "")
            {
                ldictMacros(sKey).Shared = chkShared.Checked;

                Changed = True
            }
        }

    }


#if Mono
    private void cmbShortcut_SelectionChanged(object sender, System.EventArgs e)
    {
#else
    private void cmbShortcut_SelectionChanged(object sender, System.EventArgs e)
    {
#endif

        If bSetting Then Exit Sub

        if (lstMacros.SelectedItem IsNot null)
        {
            private string sKey = arlKeys(lstMacros.SelectedIndex) ' GetKeyFromTitle(lstMacros.SelectedItem.ToString);
            if (sKey <> "")
            {
#if Mono
                ldictMacros(sKey).Shortcut = (ComboBoxItem)(CType(cmbShortcut.SelectedItem).KeyboardShortcut, Shortcut);
#else
                ldictMacros(sKey).Shortcut = (Shortcut)(cmbShortcut.SelectedItem.DataValue);
#endif

                Changed = True
            }
        }

    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        Apply();
    }

    private void btnUpDown_Click(object sender, System.EventArgs e)
    {

        private int iUpDown = CInt(IIf(sender Is btnUp, -1, 1));

        if (lstMacros.SelectedItem IsNot null)
        {
            private string sTmp = lstMacros.SelectedItem.ToString;
            private string sTmpKey = arlKeys(lstMacros.SelectedIndex);
            lstMacros.Items(lstMacros.SelectedIndex) = lstMacros.Items(lstMacros.SelectedIndex + iUpDown).ToString;
            arlKeys(lstMacros.SelectedIndex) = arlKeys(lstMacros.SelectedIndex + iUpDown);
            lstMacros.Items(lstMacros.SelectedIndex + iUpDown) = sTmp;
            arlKeys(lstMacros.SelectedIndex + iUpDown) = sTmpKey;
            lstMacros.SelectedIndex = lstMacros.SelectedIndex + iUpDown;
            Changed = True
        }

    }

    private void btnRemove_Click(object sender, System.EventArgs e)
    {

        if (lstMacros.SelectedItem IsNot null)
        {
            private string sKey = arlKeys(lstMacros.SelectedIndex) ' GetKeyFromTitle(lstMacros.SelectedItem.ToString);
            if (sKey <> "")
            {
                ldictMacros.Remove(sKey);
                arlKeys.RemoveAt(lstMacros.SelectedIndex);
                lstMacros.Items.RemoveAt(lstMacros.SelectedIndex);
                bSetting = True
                txtCommands.Clear();
                bSetting = False
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                cmbShortcut.Enabled = false;
                btnRemove.Enabled = false;
                chkShared.Enabled = false;
                Changed = True
            }
        }

    }

}
}