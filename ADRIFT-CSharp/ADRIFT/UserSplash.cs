using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmUserSplash
{

    Private WithEvents tmrSplash As New Timer

    private void UserSplash_Click(object sender, System.EventArgs e)
    {
        Me.Close();
    }

    private void UserSplash_Load(System.Object sender, System.EventArgs e)
    {
        tmrSplash.Interval = 30000;
        tmrSplash.Start();
        Cursor = Cursors.Hand
    }

    private void tmrSplash_Tick(object sender, System.EventArgs e)
    {
        Me.Close();
    }

}
}