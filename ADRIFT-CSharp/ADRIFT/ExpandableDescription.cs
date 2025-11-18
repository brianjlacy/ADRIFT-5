using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class ExpandableDescription
{

    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    'Private Sub btnDropdown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDropdown.Click
    '    If GenTextbox1.tabsDescriptions.SelectedTab IsNot Nothing AndAlso GenTextbox1.tabsDescriptions.SelectedTab.VisibleIndex = 0 AndAlso txtShortDesc.IsInEditMode Then
    '        GenTextbox1.rtxtSource.SelectionStart = txtShortDesc.SelectionStart
    '        GenTextbox1.rtxtSource.SelectionLength = txtShortDesc.SelectionLength
    '    Else
    '        GenTextbox1.rtxtSource.SelectionStart = GenTextbox1.rtxtSource.TextLength
    '    End If
    '    UltraPopupControlContainer1.PopupControl.Width = txtShortDesc.Width
    '    Me.UltraPopupControlContainer1.Show(Me, PointToScreen(New Point(0, 0)))
    '    GenTextbox1.Focus()
    'End Sub


    private void txtShortDesc_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
        if (GenTextbox1.tabsDescriptions.SelectedTab IsNot null && GenTextbox1.tabsDescriptions.SelectedTab.VisibleIndex = 0 && txtShortDesc.IsInEditMode)
        {
            GenTextbox1.rtxtSource.SelectionStart = txtShortDesc.SelectionStart;
            GenTextbox1.rtxtSource.SelectionLength = txtShortDesc.SelectionLength;
        Else
            GenTextbox1.rtxtSource.SelectionStart = GenTextbox1.rtxtSource.TextLength;
        }
        UltraPopupControlContainer1.PopupControl.Width = txtShortDesc.Width - txtShortDesc.ButtonsRight(0).WidthResolved - 3;
        Me.UltraPopupControlContainer1.Show(Me, PointToScreen(New Point(0, 0)));
        GenTextbox1.Focus();
    }


    Public Overrides Property Text() As String
        {
            get
            {
            return txtShortDesc.Text;
        }
set(ByVal String)
            txtShortDesc.Text = value;
        }
    }


    internal Description Description { get; set; }
        {
            get
            {
            return GenTextbox1.Description;
        }
set(ByVal Description)
            GenTextbox1.Description = value;
            if (value.Count > 0)
            {
                txtShortDesc.Text = value.Item(0).Description;
            Else
                txtShortDesc.Text = "";
            }
        }
    }



    private void txtShortDesc_TextChanged(object sender, System.EventArgs e)
    {
        Description.Item(0).Description = txtShortDesc.Text;
        if (GenTextbox1.tabsDescriptions.SelectedTab IsNot null && GenTextbox1.tabsDescriptions.SelectedTab.VisibleIndex = 0)
        {
            GenTextbox1.rtxtSource.Text = txtShortDesc.Text;
        }
        RaiseEvent Changed(Me, e);
    }


    private void UltraPopupControlContainer1_Closed(object sender, System.EventArgs e)
    {
        txtShortDesc.Text = Description.Item(0).Description;
        txtShortDesc.Focus();
        txtShortDesc.SelectionStart = txtShortDesc.TextLength;
    }


    private void ExpandableDescription_Load(object sender, System.EventArgs e)
    {
        GenTextbox1.Tabs.Tabs("tabGraphics").Visible = false;
        GenTextbox1.Visible = false;
    }


    private void GenTextbox1_Changed(object sender, EventArgs e)
    {
        RaiseEvent Changed(Me, e);
    }

}

}