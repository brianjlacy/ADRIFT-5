using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class EventControl
{

    internal EventOrWalkControl cec;


    private void EventControl_Load(object sender, System.EventArgs e)
    {
        SetControlStyle(Me);
    }


    internal void LoadEventControl(EventOrWalkControl ec)
    {

        cec = ec

        With cec;

            switch (.eControl)
            {
                case EventOrWalkControl.ControlEnum.Start:
                    {
                    SetCombo(cmbStartStop, "Start");
                case EventOrWalkControl.ControlEnum.Stop:
                    {
                    SetCombo(cmbStartStop, "Stop");
                case EventOrWalkControl.ControlEnum.Suspend:
                    {
                    SetCombo(cmbStartStop, "Suspend");
                case EventOrWalkControl.ControlEnum.Resume:
                    {
                    SetCombo(cmbStartStop, "Resume");
            }

            switch (.eCompleteOrNot)
            {
                case EventOrWalkControl.CompleteOrNotEnum.Completion:
                    {
                    SetCombo(cmbComplete, "completion");
                case EventOrWalkControl.CompleteOrNotEnum.UnCompletion:
                    {
                    SetCombo(cmbComplete, "uncompletion");
            }

            SetCombo(isTasks.cmbList, .sTaskKey);

        }

    }



    public void New()
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        cec = New EventOrWalkControl

    }



    private void EventControl_GotFocus(object sender, System.EventArgs e)
    {

        Me.lblHighlight.Visible = true;
        private Control parent = Me.Parent.Parent.Parent.Parent.Parent;
        if (TypeOf parent Is frmWalk)
        {
            (frmWalk)(parent).DoControlButtons();
        ElseIf TypeOf parent == frmEvent Then
            (frmEvent)(parent).DoControlButtons();
        }

    }



    private void EventControl_LostFocus(object sender, System.EventArgs e)
    {

        if (Not Me.Focused)
        {
            lblHighlight.Visible = false;
            private Control parent = Me.Parent.Parent.Parent.Parent.Parent;
            if (TypeOf parent Is frmWalk)
            {
                (frmWalk)(parent).DoControlButtons();
            ElseIf TypeOf parent == frmEvent Then
                (frmEvent)(parent).DoControlButtons();
            }
        }
    }



    private void UltraLabel1_Click(System.Object sender, System.EventArgs e)
    {
        cmbStartStop.Focus();
    }



    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return cmbStartStop.Focused || cmbComplete.Focused || isTasks.Focused;
        }
    }



    private void cmbStartStop_SelectionChanged(object sender, System.EventArgs e)
    {

        if (cmbStartStop.SelectedItem.DataValue IsNot null && cec IsNot null)
        {
            switch (cmbStartStop.SelectedItem.DataValue.ToString)
            {
                case "Start":
                    {
                    cec.eControl = EventOrWalkControl.ControlEnum.Start;
                case "Stop":
                    {
                    cec.eControl = EventOrWalkControl.ControlEnum.Stop;
                case "Suspend":
                    {
                    cec.eControl = EventOrWalkControl.ControlEnum.Suspend;
                case "Resume":
                    {
                    cec.eControl = EventOrWalkControl.ControlEnum.Resume;
            }
        }

    }



    private void cmbComplete_SelectionChanged(object sender, System.EventArgs e)
    {

        if (cmbComplete.SelectedItem.DataValue IsNot null && cec IsNot null)
        {
            switch (cmbComplete.SelectedItem.DataValue.ToString)
            {
                case "completion":
                    {
                    cec.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.Completion;
                case "uncompletion":
                    {
                    cec.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.UnCompletion;
            }
        }

    }



    private void isTasks_SelectionChanged()
    {
        If cec != null Then cec.sTaskKey = isTasks.Key
    }

}

}