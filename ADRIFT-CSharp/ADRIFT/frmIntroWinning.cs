using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmIntroWinning
{

    public void New()
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        LoadForm();
        Me.Show();

    }


    private bool bChanged;
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


    private void LoadForm()
    {

        txtIntro.Description = Adventure.Introduction;
        chkDisplayFirstRoom.Checked = Adventure.ShowFirstRoom;
        If Adventure.Player != null Then cmbStartLocation.Key = Adventure.Player.Location.Key
        txtWinning.Description = Adventure.WinningText;
        Changed = False

    }



    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyIntroWinning();
        DialogResult = Windows.Forms.DialogResult.OK
        Me.Close();
    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyIntroWinning()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close();
    }


    private void ApplyIntroWinning()
    {

        Adventure.Introduction = txtIntro.Description.Copy;
        Adventure.ShowFirstRoom = chkDisplayFirstRoom.Checked;
        if (Adventure.Player IsNot null)
        {
            private clsCharacterLocation loc = Adventure.Player.Location;
            loc.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
            loc.Key = cmbStartLocation.Key;
            Adventure.Player.Move(loc);
        }
        Adventure.WinningText = txtWinning.Description.Copy;

        Adventure.Changed = true;

    }



    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyIntroWinning();
        Changed = False
    }


    private void txtIntro_Changed(object sender, System.EventArgs e)
    {
        Changed = True
    }
    private void cmbStartLocation_SelectionChanged()
    {
        Changed = True
    }


    private void frmIntroWinning_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        SaveFormPosition(Me);
    }


    private void frmIntroWinning_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
        cmbStartLocation.BackColor = Color.Transparent;
    }

}
}