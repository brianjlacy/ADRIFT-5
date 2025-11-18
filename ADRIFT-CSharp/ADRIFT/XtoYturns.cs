using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class XtoYturns
{

    Public Shadows Event GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ValueChanged()
    private bool bShowRange = false;


    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return txtFrom.Focused || txtTo.Focused || btnExpand.Focused;
        }
    }


    public int MinValue { get; set; }
        {
            get
            {
            if (txtFrom.MinValue = Double.MinValue)
            {
                return Integer.MinValue;
            Else
                return CInt(txtFrom.MinValue);
            }
        }
set(ByVal Integer)
            txtFrom.MinValue = value;
            txtTo.MinValue = value;
        }
    }

    public int MaxValue { get; set; }
        {
            get
            {
            if (txtFrom.MaxValue = Double.MaxValue)
            {
                return Integer.MaxValue;
            Else
                return CInt(txtFrom.MaxValue);
            }
        }
set(ByVal Integer)
            txtFrom.MaxValue = value;
            txtTo.MaxValue = value;
        }
    }


    private void XtoYturns_GotFocus(object sender, System.EventArgs e)
    {
        RaiseEvent GotFocus(Me, e);
    }

    private void XtoYturns_LostFocus(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then RaiseEvent LostFocus(Me, e)
    }


    public int From { get; set; }
        {
            get
            {
            return CInt(txtFrom.Value);
        }
set(ByVal Integer)
            txtFrom.Value = value;
        }
    }

    Public Property [To]() As Integer
        {
            get
            {
            'If txtTo.Visible Then
            return CInt(txtTo.Value);
            'Else
            'Return Nothing
            'End If
        }
set(ByVal Integer)
            txtTo.Value = value;
        }
    }

    private void btnExpand_Click(System.Object sender, System.EventArgs e)
    {

        if (Not bShowRange)
        {
            btnExpand.Appearance.Image = My.Resources.Resources.imgVariable16;
            Me.ToolTip1.SetToolTip(Me.btnExpand, "Click here to set a single value");
            lblTo.Visible = true;
            txtTo.Visible = true;
            bShowRange = True
        Else
            btnExpand.Appearance.Image = My.Resources.Resources.imgOneToOne;
            Me.ToolTip1.SetToolTip(Me.btnExpand, "Click here to set a range of values");
            lblTo.Visible = false;
            txtTo.Visible = false;
            bShowRange = False
        }
        XtoYturns_Resize(null, null);

    }


    private void XtoYturns_Load(object sender, System.EventArgs e)
    {
        bShowRange = Not bShowRange ' Because the Click below inverts it
        btnExpand_Click(null, null);
    }


    private void XtoYturns_Resize(object sender, System.EventArgs e)
    {

        if (Not bShowRange)
        {
            txtFrom.Width = Me.Width - 23;
        Else
            txtFrom.Width = CInt(Me.Width / 2) - 22;
            txtTo.Width = txtFrom.Width;
            If txtTo.Left + txtTo.Width < Me.Width Then txtTo.Width += 1
            lblTo.Left = txtFrom.Left + txtFrom.Width + 4;
            txtTo.Left = lblTo.Left + 17;
        }

    }

    private void txtFrom_ValueChanged(object sender, System.EventArgs e)
    {
        If txtFrom.Value > txtTo.Value || ! bShowRange Then txtTo.Value = txtFrom.Value
        RaiseEvent ValueChanged();
    }

    private void txtTo_ValueChanged(object sender, System.EventArgs e)
    {
        If txtTo.Value < txtFrom.Value Then txtFrom.Value = txtTo.Value
        RaiseEvent ValueChanged();
    }


    public void SetValues(int iFrom, int iTo)
    {
        txtFrom.Value = iFrom;
        txtTo.Value = iTo;

        if ((iFrom = iTo) = bShowRange)
        {
            btnExpand_Click(null, null);
        }

        'If iFrom = iTo Then

        '    '    btnExpand.Text = "+"
        '    '    XtoYturns_Resize(Nothing, Nothing)
        '    '    lblTo.Visible = False
        '    '    txtTo.Visible = False
        'Else
        '    '    btnExpand.Text = "-"
        '    '    XtoYturns_Resize(Nothing, Nothing)
        '    '    lblTo.Visible = True
        '    '    txtTo.Visible = True
        'End If

    }

}

}