using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmWalk
{

    public void New(ref lWalk As clsWalk)
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        Application.EnableVisualStyles();
        LoadForm(lWalk);

    }


    private clsWalk cWalk;
    private bool bChanged;
    Private WithEvents tmrButtons As New Timer
    private SubWalk LastSelectedSubWalk;
    private EventControl LastSelectedControl;
    Private TempSubWalks() As clsWalk.SubWalk
    Private TempControls() As EventOrWalkControl


    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            'If bChanged Then
            '    btnApply.Enabled = True
            'Else
            '    btnApply.Enabled = False
            'End If
        }
    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyWalk();
        DialogResult = Windows.Forms.DialogResult.OK
    }

    'Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ApplyWalk()
    '    Changed = False
    'End Sub

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes to this walk?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyWalk()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        DialogResult = Windows.Forms.DialogResult.Cancel
    }


    private void ApplyWalk()
    {

        With cWalk;
            .Description = txtDescription.Text;
            .SubWalks = TempSubWalks;
            .WalkControls = TempControls;

            .arlSteps.Clear();
            For Each lvi As ListViewItem In lvwSteps.Items
                .arlSteps.Add(GetStepInfo(lvi));
            Next;

            If .Description = "" Then .Description = .GetDefaultDescription
            .Loops = chkRepeat.Checked;
            .StartActive = (optStart.CheckedIndex = 1);

            '        If .Key = "" Then
            '            .Key = Adventure.GetNewKey("Walk")
            '            Adventure.htblEvents.Add(cEvent, .Key)
            '        End If

            'UpdateListItem(.Key, .Description)
        }

    }


    private void LoadForm(ref cWalk As clsWalk)
    {

        Me.cWalk = cWalk;
        ReDim TempSubWalks(cWalk.SubWalks.Length - 1);
        for (int i = 0; i <= cWalk.SubWalks.Length - 1; i++)
        {
            TempSubWalks(i) = cWalk.SubWalks(i).CloneMe;
        Next;
        ReDim TempControls(cWalk.WalkControls.Length - 1);
        for (int i = 0; i <= cWalk.WalkControls.Length - 1; i++)
        {
            TempControls(i) = cWalk.WalkControls(i).CloneMe;
        Next;

        With cWalk;
            '    Text = "Event - " & .Description
            '    If CBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "False")) Then Text &= "  [" & .Key & "]"
            If .Description = "" Then Text = "New Walk" Else Text = "Edit Walk"

            txtDescription.Text = .Description;
            chkRepeat.Checked = .Loops;
            If .StartActive Then optStart.CheckedIndex = 1 Else optStart.CheckedIndex = 0

            '    x2yHowLong.SetValues(.Length.iFrom, .Length.iTo)
            '    Select Case .WhenStart
            '        Case clsEvent.WhenStartEnum.AfterATask
            '            optStart.CheckedIndex = 0
            '        Case clsEvent.WhenStartEnum.BetweenXandXTurns
            '            optStart.CheckedIndex = 1
            '            x2yStartWait.SetValues(.StartDelay.iFrom, .StartDelay.iTo)
            '        Case clsEvent.WhenStartEnum.Immediately
            '            optStart.CheckedIndex = 2
            '    End Select

            For Each wc As EventOrWalkControl In TempControls
                AddWalkControl(wc);
            Next;
            For Each sw As clsWalk.SubWalk In TempSubWalks
                AddSubWalk(sw);
            Next;
            For Each stp As clsWalk.clsStep In .arlSteps
                private string sDestination = "";
                switch (stp.sLocation)
                {
                    case "Hidden":
                        {
                        sDestination = "Hidden"
                        'Case "FollowPlayer"
                        '    sDestination = "Follow Player"
                    default:
                        {
                        If Adventure.htblCharacters.ContainsKey(stp.sLocation) Then sDestination = "Follow "
                        sDestination &= Adventure.GetNameFromKey(stp.sLocation);
                }
                private ListViewItem lvi = lvwSteps.Items.Add(sDestination);
                'lvi.SubItems.Add(stp.iWaitTurns.ToString)
                if (stp.ftTurns.iFrom = stp.ftTurns.iTo)
                {
                    lvi.SubItems.Add(stp.ftTurns.iFrom.ToString);
                Else
                    lvi.SubItems.Add(stp.ftTurns.iFrom.ToString + " - " + stp.ftTurns.iTo.ToString);
                }
                lvi.SubItems.Add(stp.sLocation);
            Next;

        }

        Changed = False
        Me.ShowDialog();

    }


    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }

    private void frmEvent_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
    }



    private void btnAddControl_Click(System.Object sender, System.EventArgs e)
    {
        AddWalkControl();
    }


    private void AddWalkControl(EventOrWalkControl cwc = null)
    {

        private bool bAdding = (cwc Is null);
        private New EventControl wc;
        wc.Parent = pnlTaskControlInner;
        wc.UltraLabel1.Text = "this walk on";
        pnlTaskControlInner.Height = pnlTaskControlInner.Controls.Count * wc.Height;
        wc.Left = 0;
        wc.Top = (pnlTaskControlInner.Controls.Count - 1) * wc.Height;
        wc.Width = pnlTaskControlInner.Width;
        wc.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

        'If Not cwc Is Nothing Then wc.LoadEventControl(cwc)
        'ReDim Preserve TempControls(TempControls.Length)
        'cwc = wc.cec
        'TempControls(TempControls.Length - 1) = cwc
        if (Not bAdding)
        {
            wc.LoadEventControl(cwc);
        Else
            ReDim Preserve TempControls(TempControls.Length);
            TempControls(TempControls.Length - 1) = wc.cec;
        }

    }


    private void btnAddActivity_Click(System.Object sender, System.EventArgs e)
    {
        AddSubWalk();
    }


    public void DoSubEventButtons()
    {
        tmrButtons.Interval = 200 ' This gap needs to be enough to allow the click event on the Remove button to fire before we disable it;
        tmrButtons.Enabled = true;
    }


    private void SubEventButtons()
    {

        tmrButtons.Enabled = false;

        private bool bRemoveSubWalk = false;
        private bool bUp = false;
        private bool bDown = false;

        LastSelectedSubWalk = Nothing
        For Each swk As SubWalk In pnlSubWalksInner.Controls
            if (swk.Focused)
            {
                If ! swk == pnlSubWalksInner.Controls(0) Then bUp = true
                If ! swk == pnlSubWalksInner.Controls(pnlSubWalksInner.Controls.Count - 1) Then bDown = true
                LastSelectedSubWalk = swk
                bRemoveSubWalk = True
            }
        Next;

        btnRemoveActivity.Enabled = bRemoveSubWalk;
        btnUpActivity.Enabled = bUp;
        btnDownActivity.Enabled = bDown;

    }


    private void AddSubWalk(clsWalk.SubWalk csw = null)
    {

        private bool bAdding = (csw Is null);
        private int iTop;
        For Each swk As SubWalk In pnlSubWalksInner.Controls
            iTop += swk.Height;
        Next;

        private New SubWalk sw;
        sw.Parent = pnlSubWalksInner;
        pnlSubWalksInner.Height = iTop + sw.Height;
        sw.Left = 0;
        sw.Top = iTop;
        sw.Width = pnlSubWalksInner.Width;
        sw.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;


        'If Not csw Is Nothing Then sw.LoadSubWalk(csw)
        'ReDim Preserve TempSubWalks(TempSubWalks.Length)
        'csw = sw.csw
        'TempSubWalks(TempSubWalks.Length - 1) = csw
        if (Not bAdding)
        {
            sw.LoadSubWalk(csw);
        Else
            ReDim Preserve TempSubWalks(TempSubWalks.Length);
            TempSubWalks(TempSubWalks.Length - 1) = sw.csw;
        }

    }


    private void RemoveSubWalk(SubWalk sw)
    {

        private bool bShuffle = false;

        for (int i = 0; i <= TempSubWalks.Length - 1; i++)
        {
            if (TempSubWalks(i) Is sw.csw)
            {
                pnlSubWalksInner.Controls.RemoveAt(i);
                sw.csw = null;
                bShuffle = True
            }
            If bShuffle && i < TempSubWalks.Length - 1 Then TempSubWalks(i) = TempSubWalks(i + 1)
        Next;
        'pnlSubEventsInner.Controls.Remove(se)
        ReDim Preserve TempSubWalks(TempSubWalks.Length - 2);
        RedrawSubWalks();

    }


    private void RemoveControl(EventControl ctrl)
    {

        private bool bShuffle = false;

        for (int i = 0; i <= TempControls.Length - 1; i++)
        {
            if (TempControls(i) Is ctrl.cec)
            {
                pnlTaskControlInner.Controls.RemoveAt(i);
                ctrl.cec = null;
                bShuffle = True
            }
            If bShuffle && i < TempControls.Length - 1 Then TempControls(i) = TempControls(i + 1)
        Next;
        'pnlSubEventsInner.Controls.Remove(se)
        ReDim Preserve TempControls(TempControls.Length - 2);
        RedrawControls();

    }


    public void RedrawSubWalks()
    {

        'For Each se As SubEvent In pnlSubEventsInner.Controls
        private int iHeight = 0;
        if (pnlSubWalksInner.Controls.Count > 0)
        {
            pnlSubWalksInner.Controls(0).Top = 0;
            iHeight = pnlSubWalksInner.Controls(0).Height
        }

        for (int iSE = 1; iSE <= pnlSubWalksInner.Controls.Count - 1; iSE++)
        {
            iHeight += pnlSubWalksInner.Controls(iSE).Height;
            pnlSubWalksInner.Controls(iSE).Top = pnlSubWalksInner.Controls(iSE - 1).Top + pnlSubWalksInner.Controls(iSE - 1).Height;
        Next;
        pnlSubWalksInner.Height = iHeight;
        SubEventButtons();

    }


    public void RedrawControls()
    {

        'For Each se As SubEvent In pnlSubEventsInner.Controls
        private int iHeight = 0;
        if (pnlTaskControlInner.Controls.Count > 0)
        {
            pnlTaskControlInner.Controls(0).Top = 0;
            iHeight = pnlTaskControlInner.Controls(0).Height
        }

        for (int iSE = 1; iSE <= pnlTaskControlInner.Controls.Count - 1; iSE++)
        {
            iHeight += pnlTaskControlInner.Controls(iSE).Height;
            pnlTaskControlInner.Controls(iSE).Top = pnlTaskControlInner.Controls(iSE - 1).Top + pnlTaskControlInner.Controls(iSE - 1).Height;
        Next;
        pnlTaskControlInner.Height = iHeight;
        ControlButtons();

    }


    private void MoveUpDown(bool bIsUp)
    {

        private clsWalk.SubWalk sw = LastSelectedSubWalk.csw;
        private clsWalk.SubWalk swSwap;
        private int iIndex;

        for (int i = 0; i <= TempSubWalks.Length - 1; i++)
        {
            if (TempSubWalks(i) Is LastSelectedSubWalk.csw)
            {
                iIndex = i
                Exit For;
            }
        Next;

        if (bIsUp)
        {
            swSwap = TempSubWalks(iIndex - 1)
            LastSelectedSubWalk.LoadSubWalk(swSwap);
            (SubWalk)(pnlSubWalksInner.Controls(iIndex - 1)).LoadSubWalk(sw);
        Else
            swSwap = TempSubWalks(iIndex + 1)
            LastSelectedSubWalk.LoadSubWalk(swSwap);
            (SubWalk)(pnlSubWalksInner.Controls(iIndex + 1)).LoadSubWalk(sw);
        }

        TempSubWalks(iIndex) = swSwap;
        TempSubWalks(CInt(IIf(bIsUp, iIndex - 1, iIndex + 1))) = sw;

        RedrawSubWalks();

    }


    private void btnRemoveSubEvent_Click(object sender, System.EventArgs e)
    {
        If ! LastSelectedSubWalk == null Then RemoveSubWalk(LastSelectedSubWalk)
    }

    private void btnUpDown_Click(object sender, System.EventArgs e)
    {
        If ! LastSelectedSubWalk == null Then MoveUpDown(sender == btnUpActivity)
    }

    private void tmrButtons_Tick(object sender, System.EventArgs e)
    {
        if (tmrButtons.Interval = 200)
        {
            SubEventButtons();
        Else
            ControlButtons();
        }
    }


    private void WalkButtons()
    {

        if (lvwSteps.SelectedItems.Count <> 1)
        {
            btnUpStep.Enabled = false;
            btnDownStep.Enabled = false;
        Else
            If lvwSteps.SelectedIndices(0) > 0 Then btnUpStep.Enabled = true
            If lvwSteps.SelectedIndices(0) < lvwSteps.Items.Count - 1 Then btnDownStep.Enabled = true
        }

    }


    public void DoControlButtons()
    {
        tmrButtons.Interval = 201 ' This gap needs to be enough to allow the click event on the Remove button to fire before we disable it;
        tmrButtons.Enabled = true;
    }

    private void ControlButtons()
    {

        tmrButtons.Enabled = false;

        private bool bRemoveControl = false;
        'Dim bUp As Boolean = False
        'Dim bDown As Boolean = False

        LastSelectedControl = Nothing
        For Each ctrl As EventControl In pnlTaskControlInner.Controls
            if (ctrl.Focused)
            {
                'If Not sev Is pnlSubEventsInner.Controls(0) Then bUp = True
                'If Not sev Is pnlSubEventsInner.Controls(pnlSubEventsInner.Controls.Count - 1) Then bDown = True
                LastSelectedControl = ctrl
                bRemoveControl = True
            }
        Next;

        btnRemoveControl.Enabled = bRemoveControl;
        'btnUp.Enabled = bUp
        'btnDown.Enabled = bDown

    }

    private void lvwSteps_DoubleClick(object sender, System.EventArgs e)
    {
        If lvwSteps.SelectedItems.Count = 1 Then EditStep(lvwSteps.SelectedItems(0))
    }


    private void EditStep(ListViewItem lvi)
    {
        EditStep(GetStepInfo(lvi), lvi);
    }


    private clsWalk.clsStep GetStepInfo(ListViewItem lvi)
    {

        If lvi == null Then Return null

        Dim [step] As New clsWalk.clsStep
        [step].sLocation = lvi.SubItems(2).Text;
        private string ft = lvi.SubItems(1).Text;
        if (ft.Contains("-"))
        {
            [step].ftTurns.iFrom = CInt(ft.Split("-"c)(0));
            [step].ftTurns.iTo = CInt(ft.Split("-"c)(1));
        Else
            [step].ftTurns.iFrom = CInt(ft);
            [step].ftTurns.iTo = [step].ftTurns.iFrom;
        }

        return [step];

    }


    private bool EditStep([step] As clsWalk.clsStep, ListViewItem lvi = null)
    {

        private New frmWalkStep([step]) frmStep;
        if (frmStep.DialogResult = Windows.Forms.DialogResult.OK)
        {
            SaveLVI([step], lvi);
            return true;
        Else
            return false;
        }

    }


    private void SaveLVI([step] As clsWalk.clsStep, ListViewItem lvi)
    {

        private string sDestination = "";
        switch ([step].sLocation)
        {
            case "Hidden":
                {
                sDestination = "Hidden"
            default:
                {
                If Adventure.htblCharacters.ContainsKey([step].sLocation) Then sDestination = "Follow "
                sDestination &= Adventure.GetNameFromKey([step].sLocation);
        }

        if (lvi Is null)
        {
            lvi = New ListViewItem
            lvwSteps.Items.Add(lvi);
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
        }

        lvi.Text = sDestination;

        'lvi.SubItems.Add(stp.iWaitTurns.ToString)
        if ([step].ftTurns.iFrom = [step].ftTurns.iTo)
        {
            lvi.SubItems(1).Text = [step].ftTurns.iFrom.ToString;
        Else
            lvi.SubItems(1).Text = [step].ftTurns.iFrom.ToString + " - " + [step].ftTurns.iTo.ToString;
        }
        lvi.SubItems(2).Text = [step].sLocation;
    }


    private void btnAddStep_Click(object sender, System.EventArgs e)
    {
        Dim [step] As New clsWalk.clsStep
        If EditStep([step]) Then lvwSteps.Items(lvwSteps.Items.Count - 1).EnsureVisible()
    }



    private void lvwSteps_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (lvwSteps.SelectedItems.Count > 0)
        {
            btnEditStep.Enabled = true;
            btnRemoveStep.Enabled = true;
        Else
            btnEditStep.Enabled = false;
            btnRemoveStep.Enabled = false;
        }
        WalkButtons();

    }

    private void btnRemoveStep_Click(object sender, System.EventArgs e)
    {

        If lvwSteps.SelectedItems.Count = 0 Then Exit Sub
        lvwSteps.Items.Remove(lvwSteps.SelectedItems(0));

    }

    private void btnRemoveControl_Click(object sender, System.EventArgs e)
    {
        If LastSelectedControl != null Then RemoveControl(LastSelectedControl)
    }


    private void btnStep_Click(object sender, System.EventArgs e)
    {

        private int iUpDown = CInt(IIf(sender Is btnUpStep, -1, 1));

        private ListViewItem lvi1 = lvwSteps.SelectedItems(0);
        private ListViewItem lvi2 = lvwSteps.Items(lvi1.Index + iUpDown);

        private clsWalk.clsStep step1 = GetStepInfo(lvi1);
        private clsWalk.clsStep step2 = GetStepInfo(lvi2);

        SaveLVI(step1, lvi2);
        SaveLVI(step2, lvi1);

        lvi1.Selected = false;
        lvi2.Selected = true;
        lvwSteps.Focus();

    }

}
}