using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADRIFT
{

public class frmYesNoCancel
{

    private void btnYes_Click(System.Object sender, System.EventArgs e)
    {
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes;
        Me.Close();
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (btnCancel.Text = "Cancel")
        {
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Ignore;
        }

        Me.Close();
    }

    private void btnNo_Click(System.Object sender, System.EventArgs e)
    {
        Me.DialogResult = System.Windows.Forms.DialogResult.No;
        Me.Close();
    }

}

}