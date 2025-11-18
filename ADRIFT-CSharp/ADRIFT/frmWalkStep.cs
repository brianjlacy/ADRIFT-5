using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmWalkStep
{

    Public [step] As clsWalk.clsStep

    public void New([step] As clsWalk.clsStep)
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        Me.ItemSelector1.Key = [step].sLocation;
        Me.XtoYturns1.SetValues([step].ftTurns.iFrom, [step].ftTurns.iTo);

        Me.step = [step];

        Me.ShowDialog();

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {

        [step].sLocation = ItemSelector1.Key;
        [step].ftTurns.iFrom = XtoYturns1.From;
        [step].ftTurns.iTo = XtoYturns1.To;

        DialogResult = Windows.Forms.DialogResult.OK
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        DialogResult = Windows.Forms.DialogResult.Cancel
    }

    private void frmWalkStep_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
    }

}
}