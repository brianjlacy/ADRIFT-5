using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmFolder
{

    private Size pOrigSize;
    private Point pOrigLocation;
    private Point pOffsetInForm;
    private void lblCaption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        pOrigLocation = Me.Location
        pOffsetInForm = e.Location
        Me.BringToFront();
    }

    private void lblCaption_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (MouseButtons = Windows.Forms.MouseButtons.Left)
        {
            Me.Location = New Point(pOrigLocation.X + e.Location.X - pOffsetInForm.X, pOrigLocation.Y + e.Location.Y - pOffsetInForm.Y);
            pOrigLocation = Me.Location
        }
    }


    public void ScrollIntoView()
    {

        If ! bAllowFolderScroll Then Exit Sub
        If ! CBool(SafeInt(GetSetting("ADRIFT", "Generator", "ScrollIntoView", "-1"))) Then Exit Sub ' No front end for this, but this gives us the ability to turn this feature off
        'fGenerator.ScrollControlIntoView(Me)
        'Exit Sub

        ' If the folder is visible but off screen, scroll it into view
        private int iMDIwidth = fGenerator.ClientSize.Width - 22;
        private int iMDIheight = fGenerator.ClientSize.Height - fGenerator.StatusBar1.Height - CInt(If(fGenerator.UTMMain.Ribbon.IsMinimized, 83, 186));

        ' TODO - Adjust h/w depending whether MDI scrollbars are visible

        For Each da As Infragistics.Win.UltraWinDock.DockAreaPane In fGenerator.UDMGenerator.DockAreas
            With da;
                if (Not .Closed && .DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked)
                {
                    switch (.DockedLocation)
                    {
                        case Infragistics.Win.UltraWinDock.DockedLocation.DockedTop:
                        case Infragistics.Win.UltraWinDock.DockedLocation.DockedBottom:
                            {
                            iMDIheight -= .Size.Height + 5;
                        case Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft:
                        case Infragistics.Win.UltraWinDock.DockedLocation.DockedRight:
                            {
                            iMDIwidth -= .Size.Width + 5;
                    }
                }
            }
        Next;

        private int iMaxMove = 25;
        while ((Location.X + Width > iMDIwidth && Location.X > -1) || (Location.Y + Height > iMDIheight && Location.Y > -1) || Location.X < 0 || Location.Y < 0)
        {
            private int iX = 0;
            If Location.X + Width > iMDIwidth && Location.X > 0 Then iX += Math.Min(Location.X + Width - iMDIwidth, iMaxMove)
            If Location.X < 0 Then iX += Math.Max(Location.X, -iMaxMove)
            private int iY = 0;
            If Location.Y + Height > iMDIheight && Location.Y > 0 Then iY += Math.Min(Location.Y + Height - iMDIheight, iMaxMove)
            If Location.Y < 0 Then iY += Math.Max(Location.Y, -iMaxMove)
            If iX = 0 && iY = 0 Then Exit While
            For Each c As frmFolder In fGenerator.MDIFolders
                c.Location = New Point(c.Location.X - iX, c.Location.Y - iY);
            Next;
            Application.DoEvents();
            Threading.Thread.Sleep(1);
        }

        ' TODO - Refresh the MDI scrollbars.  But how...?
        'fGenerator.PerformLayout()
        'Dim ptScroll As Point = fGenerator.AutoScrollOffset

    }


    private void Folder_ActiveChanged(object sender, bool bActive)
    {
        if (bActive)
        {
            lblCaption.Font = New Font(lblCaption.Font, FontStyle.Bold);
            BringToFront();
            fGenerator.UTMMain.Tools("Cut").SharedProps.Enabled = Folder.lstContents.SelectedItems.Count > 0;
            fGenerator.UTMMain.Tools("Copy").SharedProps.Enabled = Folder.lstContents.SelectedItems.Count > 0;
            Folder.lstContents.ItemSettings.SelectedAppearance.BackColor = Color.FromArgb(238, 247, 253);
            Folder.lstContents.ItemSettings.SelectedAppearance.BackColor2 = Color.FromArgb(217, 240, 252);
        Else
            lblCaption.Font = New Font(lblCaption.Font, FontStyle.Regular);
            Folder.lstContents.ItemSettings.SelectedAppearance.BackColor = Color.FromArgb(247, 247, 247);
            Folder.lstContents.ItemSettings.SelectedAppearance.BackColor2 = Color.FromArgb(231, 231, 231);
        }
    }

    private void Folder_LoadedFolder(object sender, System.EventArgs e)
    {
        lblCaption.Text = Folder.folder.Name;
        UltraStatusBar1.Text = Folder.lstContents.Items.Count + " item" + IIf(Folder.lstContents.Items.Count <> 1, "s", "").ToString;
        switch (Folder.folder.ViewType)
        {
            case Infragistics.Win.UltraWinListView.UltraListViewStyle.Details:
                {
                btnColumns.Visible = true;
            default:
                {
                btnColumns.Visible = false;
        }
    }

    private void frmFolder_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        If Folder.folder != null Then Folder.folder.Visible = false
    }

    private void frmFolder_GotFocus(object sender, System.EventArgs e)
    {
        Folder.SetActive();
    }

    private void btnClose_Click(System.Object sender, System.EventArgs e)
    {
        Folder.SetActive(false);
        Me.Close();
    }

    private void UltraStatusBar1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        pOrigSize = Me.Size
        pOffsetInForm = MousePosition ' e.Location
    }

    private void UltraStatusBar1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (MouseButtons = Windows.Forms.MouseButtons.Left && e.X > Width - 20)
        {
            private Point pNewPos = MousePosition;
            private Size sizeNew = new Size(pOrigSize.Width + pNewPos.X - pOffsetInForm.X, pOrigSize.Height + pNewPos.Y - pOffsetInForm.Y);
            Me.Size = sizeNew;
            pOrigSize = Me.Size
            pOffsetInForm = pNewPos
            Me.Refresh();
            Folder.Refresh();
            btnClose.Refresh();
            'fGenerator.Refresh()
        }
    }


    private void frmFolder_Move(object sender, System.EventArgs e)
    {
        If Folder.folder != null Then Folder.folder.Location = Me.Location
    }

    private void frmFolder_Resize(object sender, System.EventArgs e)
    {
        If Folder.folder != null Then Folder.folder.Size = Me.Size
    }

    private void Folder_MembersChanged(object sender, System.EventArgs e)
    {
        UltraStatusBar1.Text = Folder.folder.Members.Count + " item" + IIf(Folder.folder.Members.Count <> 1, "s", "").ToString;
    }

    private void Folder_ViewTypeChanged(object sender, Infragistics.Win.UltraWinListView.UltraListViewStyle Type)
    {

        switch (Type)
        {
            case Infragistics.Win.UltraWinListView.UltraListViewStyle.Details:
                {
                btnColumns.Visible = true;
            default:
                {
                btnColumns.Visible = false;
        }

    }

    private void btnColumns_DroppingDown(object sender, System.ComponentModel.CancelEventArgs e)
    {
        lstColumns.SetItemChecked(0, Folder.folder.ShowCreatedDate);
        lstColumns.SetItemChecked(1, Folder.folder.ShowModifiedDate);
        lstColumns.SetItemChecked(2, Folder.folder.ShowType);
        lstColumns.SetItemChecked(3, Folder.folder.ShowKey);
        lstColumns.SetItemChecked(4, Folder.folder.ShowPriority);
    }

    private void lstColumns_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
    {

        switch (e.Index)
        {
            case 0:
                {
                Folder.folder.ShowCreatedDate = e.NewValue = CheckState.Checked;
            case 1:
                {
                Folder.folder.ShowModifiedDate = e.NewValue = CheckState.Checked;
            case 2:
                {
                Folder.folder.ShowType = e.NewValue = CheckState.Checked;
            case 3:
                {
                Folder.folder.ShowKey = e.NewValue = CheckState.Checked;
            case 4:
                {
                Folder.folder.ShowPriority = e.NewValue = CheckState.Checked;
        }

        'If lstColumns.CheckedItems.Count = 0 Then
        '    'Folder.lstContents.GroupHeadersVisible = Infragistics.Win.DefaultableBoolean.False
        '    '    Folder.lstContents.ViewSettingsDetails.SubItemColumnsVisibleByDefault = False
        'Else
        '    'Folder.lstContents.GroupHeadersVisible = Infragistics.Win.DefaultableBoolean.True
        '    '   Folder.lstContents.ViewSettingsDetails.SubItemColumnsVisibleByDefault = True
        'End If

    }



    private void lstColumns_SelectedValueChanged(object sender, System.EventArgs e)
    {
        for (int i = 0; i <= 4; i++)
        {
            If lstColumns.SelectedIndex = i Then lstColumns.SetSelected(i, false)
        Next;
    }

}
}