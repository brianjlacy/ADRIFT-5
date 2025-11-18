using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class WhatsNew
{

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
        DialogResult = Windows.Forms.DialogResult.Cancel
    }

    private void btnDownload_Click(object sender, System.EventArgs e)
    {
        Cursor.Current = Cursors.WaitCursor;
        DialogResult = Windows.Forms.DialogResult.OK
    }

}
}