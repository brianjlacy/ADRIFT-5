using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class SubWalk
{

    public clsWalk.SubWalk csw;



    private void cmbAction_ValueChanged(System.Object sender, System.EventArgs e)
    {

        switch (cmbAction.SelectedItem.DataValue.ToString)
        {
            case Is = "ExecuteTask":
            case "UnsetTask":
                {
                SetHeight(68);
                txtDescription.Visible = false;
                'TaskList1.Visible = True
                isTasks.Visible = true;
                lblGroup.Visible = false;
                isLocationGroup.Visible = false;
            default:
                {
                SetHeight(153);
                txtDescription.Visible = true;
                'TaskList1.Visible = False
                isTasks.Visible = false;
                lblGroup.Visible = true;
                isLocationGroup.Visible = true;
        }

        switch (cmbAction.SelectedItem.DataValue.ToString)
        {
            case "DisplayMessage":
                {
                csw.eWhat = clsWalk.SubWalk.WhatEnum.DisplayMessage;
            case "ExecuteTask":
                {
                csw.eWhat = clsWalk.SubWalk.WhatEnum.ExecuteTask;
            case "UnsetTask":
                {
                csw.eWhat = clsWalk.SubWalk.WhatEnum.UnsetTask;
        }

    }



    private void SetHeight(int iHeight)
    {

        if (Me.Height <> iHeight)
        {
            Me.Height = iHeight;
            if (Not Me.Parent Is null)
            {
                (frmWalk)(Me.Parent.Parent.Parent.Parent.Parent).RedrawSubWalks();
            }
        }

    }


    public void LoadSubWalk(clsWalk.SubWalk sw)
    {

        csw = sw

        With csw;
            XtoYturns1.SetValues(.ftTurns.iFrom, .ftTurns.iTo);

            switch (.eWhen)
            {
                case clsWalk.SubWalk.WhenEnum.BeforeEndOfWalk:
                    {
                    SetCombo(cmbFromStart, "BeforeEnd");
                case clsWalk.SubWalk.WhenEnum.FromLastSubWalk:
                    {
                    SetCombo(cmbFromStart, "FromLast");
                case clsWalk.SubWalk.WhenEnum.FromStartOfWalk:
                    {
                    SetCombo(cmbFromStart, "FromStart");
                case clsWalk.SubWalk.WhenEnum.ComesAcross:
                    {
                    SetCombo(cmbFromStart, "ComesAcross");
                    isObjectChar.Key = .sKey;
            }
            switch (.eWhat)
            {
                case clsWalk.SubWalk.WhatEnum.DisplayMessage:
                    {
                    SetCombo(cmbAction, "DisplayMessage");
                case clsWalk.SubWalk.WhatEnum.ExecuteTask:
                    {
                    SetCombo(cmbAction, "ExecuteTask");
                    SetCombo(isTasks.cmbList, .sKey2);
                case clsWalk.SubWalk.WhatEnum.UnsetTask:
                    {
                    SetCombo(cmbAction, "UnsetTask");
                    SetCombo(isTasks.cmbList, .sKey2);
            }

            txtDescription.Description = .oDescription;
            isLocationGroup.Key = .sKey3 ' Hmm, looks like we'll need sKey3;

        }

    }

    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return Me.cmbAction.Focused || Me.cmbFromStart.Focused || Me.XtoYturns1.Focused || Me.isLocationGroup.Focused || Me.txtDescription.Focused || Me.isTasks.Focused;
        }
    }



    private void SubEvent_GotFocus(object sender, System.EventArgs e)
    {
        Me.lblHighlight.Visible = true;
        (frmWalk)(Me.Parent.Parent.Parent.Parent.Parent).DoSubEventButtons();
    }


    private void cmbAction_LostFocus(object sender, System.EventArgs e)
    {
        if (Not Me.Focused)
        {
            lblHighlight.Visible = false;
            (frmWalk)(Me.Parent.Parent.Parent.Parent.Parent).DoSubEventButtons();
        }
    }


    private void cmbFromStart_ValueChanged(System.Object sender, System.EventArgs e)
    {

        switch (cmbFromStart.SelectedItem.DataValue.ToString)
        {
            case "FromStart":
                {
                lblAfterWith.Text = "After";
                csw.eWhen = clsWalk.SubWalk.WhenEnum.FromStartOfWalk;
                isObjectChar.Visible = false;
                XtoYturns1.Visible = true;
                UltraLabel2.Visible = true;
                cmbFromStart.Location = New Point(215, 10);
            case "FromLast":
                {
                lblAfterWith.Text = "After";
                csw.eWhen = clsWalk.SubWalk.WhenEnum.FromLastSubWalk;
                isObjectChar.Visible = false;
                XtoYturns1.Visible = true;
                UltraLabel2.Visible = true;
                cmbFromStart.Location = New Point(215, 10);
            case "BeforeEnd":
                {
                lblAfterWith.Text = "With";
                csw.eWhen = clsWalk.SubWalk.WhenEnum.BeforeEndOfWalk;
                isObjectChar.Visible = false;
                XtoYturns1.Visible = true;
                UltraLabel2.Visible = true;
                cmbFromStart.Location = New Point(215, 10);
            case "ComesAcross":
                {
                lblAfterWith.Text = "If";
                csw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                isObjectChar.Visible = true;
                XtoYturns1.Visible = false;
                UltraLabel2.Visible = false;
                cmbFromStart.Location = New Point(30, 10);
        }

    }

    public void New()
    {

        csw = New clsWalk.SubWalk

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        txtDescription.Description = csw.oDescription;

    }

    private void SubEvent_Load(object sender, System.EventArgs e)
    {
        SetControlStyle(Me);
    }

    private void SubEvent_Resize(object sender, System.EventArgs e)
    {
        Me.lblHighlight.Height = Me.Height - 5;
    }


    private void txtDescription_txtSourceChanged(object sender, System.EventArgs e)
    {
        csw.oDescription = txtDescription.Description;
    }

    private void isObjectChar_SelectionChanged()
    {
        csw.sKey = isObjectChar.Key;
    }

    private void isTasks_SelectionChanged()
    {
        csw.sKey2 = isTasks.Key;
    }

    private void isLocationGroup_SelectionChanged()
    {
        csw.sKey3 = isLocationGroup.Key;
    }

    private void XtoYturns1_ValueChanged()
    {
        csw.ftTurns.iFrom = XtoYturns1.From;
        csw.ftTurns.iTo = XtoYturns1.To;
    }

    private void lblBorder_Click(System.Object sender, System.EventArgs e)
    {
        if (Not Me.Focused)
        {
            If XtoYturns1.Visible Then XtoYturns1.txtFrom.Focus() Else cmbFromStart.Focus()
        }
    }

    private void lblGroup_Click(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then Me.txtDescription.rtxtSource.Focus()
    }

}

}