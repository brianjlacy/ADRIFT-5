using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmSynonym
{

    private bool bChanged;
    private clsSynonym cSyn;
    private int iSelectedIndex = 0;
    private bool bAllowChangeValue = true;


    public void New(clsSynonym Syn)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmSynonym)
            {
                if (CType(w, frmSynonym).cSyn.Key = Syn.Key && Syn.Key IsNot null)
                {
                    w.BringToFront();
                    w.Focus();
                    Exit Sub;
                }
            }
        Next;

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        LoadForm(Syn);

    }

    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            if (bChanged)
            {
                btnApply.Enabled = true;
            Else
                btnApply.Enabled = false;
            }
        }
    }

    private void LoadForm(ref cSyn As clsSynonym)
    {

        Me.cSyn = cSyn;

        With cSyn;
            Text = "Synonym"
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= " [" + .Key + "]"
            If .ChangeFrom.Count = 0 Then Text = "New Synonym"

            For Each sName As String In .ChangeFrom
                cmbFrom.Items.Add(sName);
            Next;
            if (cmbFrom.Items.Count > 0)
            {
                iSelectedIndex = 0
                cmbFrom.SelectedIndex = 0;
            Else
                cmbFrom.Items.Add("");
            }

            txtTo.Text = .ChangeTo;

        }

        Me.Show();
        Changed = False

        OpenForms.Add(Me);

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplySynonym();
        CloseSynonym(Me);
    }


    private void ApplySynonym()
    {

        With cSyn;
            .ChangeFrom.Clear();
            For Each vli As Infragistics.Win.ValueListItem In cmbFrom.Items
                If vli.DisplayText <> "" Then .ChangeFrom.Add(vli.DisplayText.Trim)
            Next;
            .ChangeTo = txtTo.Text;

            if (.Key = "")
            {
                .Key = .GetNewKey;
                Adventure.htblSynonyms.Add(cSyn);
            }
            .LastUpdated = Now;
            .IsLibrary = false;

            UpdateListItem(.Key, .CommonName);
        }

        Adventure.Changed = true;

    }

    private void cmbFrom_Enter(object sender, System.EventArgs e)
    {
        Me.AcceptButton = null;
    }

    private void cmbFrom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        switch (e.KeyData)
        {
            case Keys.Enter:
                {
                if (iSelectedIndex = cmbFrom.Items.Count - 1)
                {
                    iSelectedIndex += 1;
                    cmbFrom.Items.Add("");
                    bAllowChangeValue = False
                    cmbFrom.Clear();
                    bAllowChangeValue = True
                Else
                    iSelectedIndex += 1;
                    If cmbFrom.Items.Count > iSelectedIndex Then cmbFrom.SelectedItem = cmbFrom.Items(iSelectedIndex)
                    If cmbFrom.SelectedItem != null Then cmbFrom.SelectedText = cmbFrom.SelectedItem.DisplayText
                    cmbFrom.SelectionStart = 0;
                    cmbFrom.SelectionLength = cmbFrom.Text.Length;
                }
                'NamesDebug()
            case Keys.Up:
                {
                If iSelectedIndex > 0 Then iSelectedIndex -= 1
            case Keys.Down:
                {
                If iSelectedIndex < cmbFrom.Items.Count - 1 Then iSelectedIndex += 1
            case Keys.Delete:
                {
                If cmbFrom.SelectedText = cmbFrom.Text && iSelectedIndex > -1 Then cmbFrom.Items(iSelectedIndex).DataValue = ""
        }

        'If cmbFrom.Text = "" Then
        '    Debug.WriteLine("Text cleared on item " & iSelectedIndex)
        'End If
        'cmbFrom.Items(iSelectedIndex).DisplayText = cmbFrom.Text
        'NamesDebug()

    }

    private void cmbFrom_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        If cmbFrom.Items.Count > iSelectedIndex Then cmbFrom.Items(iSelectedIndex).DisplayText = cmbFrom.Text
        'NamesDebug()
    }

    private void cmbFrom_Leave(object sender, System.EventArgs e)
    {
        'AddName(True)
        for (int i = cmbFrom.Items.Count - 1; i <= 0; i += -1)
        {
            if (cmbFrom.Items(i).DisplayText = "")
            {
                If iSelectedIndex = i Then iSelectedIndex = 0
                If iSelectedIndex > i Then iSelectedIndex -= 1
                If cmbFrom.Items.Count > 1 Then cmbFrom.Items.RemoveAt(i)
            }
        Next;
        Me.AcceptButton = btnOK;
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {

        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplySynonym()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseSynonym(Me);

    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplySynonym();
        Changed = False
    }

    private void cmbFrom_SelectionChanged(object sender, System.EventArgs e)
    {
        if (bAllowChangeValue && cmbFrom.SelectedIndex > -1 && Not (cmbFrom.Text = "" && cmbFrom.Items(cmbFrom.SelectedIndex).DisplayText <> ""))
        {
            ' text won't match item when we change selection with mouse
            iSelectedIndex = cmbFrom.SelectedIndex
        Else
            If cmbFrom.SelectedIndex > -1 Then Debug.WriteLine("text: " + cmbFrom.Text + ", item: " + cmbFrom.Items(cmbFrom.SelectedIndex).DisplayText)
        }
    }

    private void StuffChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
    }


    private void frmTextOverride_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmHint_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgSynonym16.GetHicon);
        GetFormPosition(Me);
    }


    private void frmTextOverride_Shown(object sender, System.EventArgs e)
    {
        if (cmbFrom.Text = "")
        {
            cmbFrom.Focus();
        Else
            txtTo.Focus();
        }
    }

    private void frmTextOverride_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Synonyms");
    }

}
}