using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class SubEventGUI
{

    public clsEvent.SubEvent cse;
    Public Event ValueChanged(ByVal o As Object, ByVal e As EventArgs)


    private frmEvent ParentEventForm()
    {
        return CType(Me.Parent.Parent.Parent.Parent.Parent, frmEvent);
    }


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
                cse.sKey = isTasks.Key;
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
                cse.sKey = isLocationGroup.Key;
        }

        switch (cmbAction.SelectedItem.DataValue.ToString)
        {
            case "DisplayMessage":
                {
                cse.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
            case "ChangeLook":
                {
                cse.eWhat = clsEvent.SubEvent.WhatEnum.SetLook;
            case "ExecuteTask":
                {
                cse.eWhat = clsEvent.SubEvent.WhatEnum.ExecuteTask;
            case "UnsetTask":
                {
                cse.eWhat = clsEvent.SubEvent.WhatEnum.UnsetTask;
        }

    }



    private void SetHeight(int iHeight)
    {

        if (Me.Height <> iHeight)
        {
            Me.Height = iHeight;
            if (Not Me.Parent Is null)
            {
                ParentEventForm.RedrawSubEvents();
            }
        }

    }


    public void LoadSubEvent(clsEvent.SubEvent se)
    {

        cse = se

        With cse;
            switch (.eWhen)
            {
                case clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent:
                    {
                    SetCombo(cmbFromStart, "BeforeEnd");
                case clsEvent.SubEvent.WhenEnum.FromLastSubEvent:
                    {
                    SetCombo(cmbFromStart, "FromLast");
                case clsEvent.SubEvent.WhenEnum.FromStartOfEvent:
                    {
                    SetCombo(cmbFromStart, "FromStart");
            }

            XtoYturns1.SetValues(.ftTurns.iFrom, .ftTurns.iTo);

            switch (.eWhat)
            {
                case clsEvent.SubEvent.WhatEnum.DisplayMessage:
                    {
                    isLocationGroup.Key = .sKey;
                    SetCombo(cmbAction, "DisplayMessage");
                case clsEvent.SubEvent.WhatEnum.ExecuteTask:
                    {
                    SetCombo(isTasks.cmbList, .sKey);
                    SetCombo(cmbAction, "ExecuteTask");
                case clsEvent.SubEvent.WhatEnum.SetLook:
                    {
                    isLocationGroup.Key = .sKey;
                    SetCombo(cmbAction, "ChangeLook");
                case clsEvent.SubEvent.WhatEnum.UnsetTask:
                    {
                    SetCombo(isTasks.cmbList, .sKey);
                    SetCombo(cmbAction, "UnsetTask");
            }
            switch (.eMeasure)
            {
                case clsEvent.SubEvent.MeasureEnum.Turns:
                    {
                    SetCombo(cmbTurnsSeconds, "Turns");
                case clsEvent.SubEvent.MeasureEnum.Seconds:
                    {
                    SetCombo(cmbTurnsSeconds, "Seconds");
            }

            txtDescription.Description = .oDescription;

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
        ParentEventForm.DoSubEventButtons();
    }


    private void cmbAction_LostFocus(object sender, System.EventArgs e)
    {
        if (Not Me.Focused)
        {
            lblHighlight.Visible = false;
            ParentEventForm.DoSubEventButtons();
        }
    }


    private void cmbFromStart_ValueChanged(System.Object sender, System.EventArgs e)
    {
        SortDropdowns();
    }


    internal void SortDropdowns()
    {

        switch (cmbFromStart.SelectedItem.DataValue.ToString)
        {
            case "FromStart":
                {
                lblAfterWith.Text = "After";
                cse.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent;
                if (cmbTurnsSeconds.Items.Count = 1)
                {
                    cmbTurnsSeconds.Items.Add("Seconds", "seconds");
                }
            case "FromLast":
                {
                lblAfterWith.Text = "After";
                cse.eWhen = clsEvent.SubEvent.WhenEnum.FromLastSubEvent;
                if (cmbTurnsSeconds.Items.Count = 1)
                {
                    cmbTurnsSeconds.Items.Add("Seconds", "seconds");
                }
            case "BeforeEnd":
                {
                lblAfterWith.Text = "With";
                cse.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                if (cmbTurnsSeconds.Items.Count = 2 && CInt(ParentEventForm.optEventType.Value) = 0)
                {
                    cmbTurnsSeconds.SelectedIndex = 0;
                    cmbTurnsSeconds.Items.Remove(1);
                }
                if (cmbTurnsSeconds.Items.Count = 1 && CInt(ParentEventForm.optEventType.Value) = 1)
                {
                    cmbTurnsSeconds.Items.Add("Seconds", "seconds");
                }
        }

    }


    public void New()
    {

        cse = New clsEvent.SubEvent("")

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        txtDescription.Description = cse.oDescription;
        If Adventure.htblGroups.ContainsKey(ALLROOMS) Then Me.isLocationGroup.Key = ALLROOMS

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
        cse.oDescription = txtDescription.Description;
    }

    private void isLocationGroup_SelectionChanged()
    {
        cse.sKey = isLocationGroup.Key;
    }

    private void isTasks_SelectionChanged()
    {
        cse.sKey = isTasks.Key;
    }

    private void XtoYturns1_ValueChanged()
    {
        cse.ftTurns.iFrom = XtoYturns1.From;
        cse.ftTurns.iTo = XtoYturns1.To;

        RaiseEvent ValueChanged(Me, New EventArgs);
    }

    private void lblBorder_Click(System.Object sender, System.EventArgs e)
    {
        If ! Me.Focused Then Me.XtoYturns1.txtFrom.Focus()
    }

    private void lblGroup_Click(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then Me.txtDescription.rtxtSource.Focus()
    }

    private void cmbTurnsSeconds_ValueChanged(object sender, System.EventArgs e)
    {
        If cmbTurnsSeconds.SelectedItem.DataValue.ToString = "Turns" Then cse.eMeasure = clsEvent.SubEvent.MeasureEnum.Turns Else cse.eMeasure = clsEvent.SubEvent.MeasureEnum.Seconds
    }

}

}