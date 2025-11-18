using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Password
{

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        DialogResult = Windows.Forms.DialogResult.OK
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        DialogResult = Windows.Forms.DialogResult.Cancel
    }

    private void Password_Load(object sender, System.EventArgs e)
    {
        SetWindowStyle(Me);
    }

}
}