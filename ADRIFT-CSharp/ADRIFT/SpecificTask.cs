using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class SpecificTask
{

    private clsTask tas;
    private string sGeneralKey;
    Public Event Changed()



    Friend ReadOnly Property GetSpecifics As clsTask.Specific()
        {
            get
            {
            If tas != null Then Return tas.Specifics Else Return null
        }
    }


    internal void LoadSpecific(string sTaskGeneralKey, ref ParentTask As clsTask)
    {

        try
        {
            Dim sBlocks() As String
            private int iLength;
            private bool bReference;
            private bool bLastBlock;
            private int iNumSpecific = 0;
            private string sOrigBlock;

            tas = ParentTask
            sGeneralKey = sTaskGeneralKey

            private string sTaskText = Adventure.htblTasks(sGeneralKey).MakeNice;

            With Me.txtSpecific;
                .Text = "";

                sBlocks = Split(sTaskText, "%")

                For Each sBlock As String In sBlocks

                    bReference = False
                    sOrigBlock = sBlock

                    switch (sBlock.ToLower)
                    {
                        case "character":
                        case "character1":
                        case "character2":
                        case "character3":
                        case "character4":
                        case "character5":
                        case "characters":
                        case "direction":
                        case "direction1":
                        case "direction2":
                        case "direction3":
                        case "direction4":
                        case "direction5":
                        case "number":
                        case "number1":
                        case "number2":
                        case "number3":
                        case "number4":
                        case "number5":
                        case "numbers":
                        case "object":
                        case "object1":
                        case "object2":
                        case "object3":
                        case "object4":
                        case "object5":
                        case "objects":
                        case "text":
                        case "text1":
                        case "text2":
                        case "text3":
                        case "text4":
                        case "text5":
                        case "location":
                        case "location1":
                        case "location2":
                        case "location3":
                        case "location4":
                        case "location5":
                        case "item":
                        case "item1":
                        case "item2":
                        case "item3":
                        case "item4":
                        case "item5":
                            {
                            if (sTaskText.Substring(iLength - 1, 1) = "%" && sTaskText.Substring(iLength + sBlock.Length, 1) = "%")
                            {
                                bReference = True
                                iNumSpecific += 1;
                                if (iNumSpecific > tas.Specifics.Length)
                                {
                                    ReDim Preserve tas.Specifics(iNumSpecific - 1);
                                    tas.Specifics(iNumSpecific - 1) = New clsTask.Specific;
                                    tas.Specifics(iNumSpecific - 1).Keys.Add("");
                                }
                                With tas.Specifics(iNumSpecific - 1);
                                    switch (sBlock.ToLower)
                                    {
                                        case "character":
                                        case "character1":
                                        case "character2":
                                        case "character3":
                                        case "character4":
                                        case "character5":
                                            {
                                            .Type = ReferencesType.Character;
                                            .Multiple = false;
                                        case "characters":
                                            {
                                            .Type = ReferencesType.Character;
                                            .Multiple = true;
                                        case "direction":
                                        case "direction1":
                                        case "direction2":
                                        case "direction3":
                                        case "direction4":
                                        case "direction5":
                                            {
                                            .Type = ReferencesType.Direction;
                                            .Multiple = false;
                                        case "number":
                                        case "number1":
                                        case "number2":
                                        case "number3":
                                        case "number4":
                                        case "number5":
                                            {
                                            .Type = ReferencesType.Number;
                                            .Multiple = false;
                                        case "numbers":
                                            {
                                            .Type = ReferencesType.Number;
                                            .Multiple = true;
                                        case "object":
                                        case "object1":
                                        case "object2":
                                        case "object3":
                                        case "object4":
                                        case "object5":
                                            {
                                            .Type = ReferencesType.Object;
                                            .Multiple = false;
                                        case "objects":
                                            {
                                            .Type = ReferencesType.Object;
                                            .Multiple = true;
                                        case "text":
                                        case "text1":
                                        case "text2":
                                        case "text3":
                                        case "text4":
                                        case "text5":
                                            {
                                            .Type = ReferencesType.Text;
                                            .Multiple = false;
                                        case "location":
                                        case "location1":
                                        case "location2":
                                        case "location3":
                                        case "location4":
                                        case "location5":
                                            {
                                            .Type = ReferencesType.Location;
                                            .Multiple = false;
                                        case "item":
                                        case "item1":
                                        case "item2":
                                        case "item3":
                                        case "item4":
                                        case "item5":
                                            {
                                            .Type = ReferencesType.Item;
                                            .Multiple = false;
                                    }

                                    if (Not .Keys Is null && .Keys.Count > 0)
                                    {
                                        sBlock = .List
                                        If sBlock = "" Then sBlock = sOrigBlock
                                        switch (sBlock.ToLower)
                                        {
                                            case "object1":
                                                {
                                                sBlock = "first object"
                                            case "object2":
                                                {
                                                sBlock = "second object"
                                            case "object3":
                                                {
                                                sBlock = "third object"
                                            case "object4":
                                                {
                                                sBlock = "fourth object"
                                            case "object5":
                                                {
                                                sBlock = "fifth object"
                                            case "character1":
                                                {
                                                sBlock = "first character"
                                            case "character2":
                                                {
                                                sBlock = "second character"
                                            case "character3":
                                                {
                                                sBlock = "third character"
                                            case "character4":
                                                {
                                                sBlock = "fourth character"
                                            case "character5":
                                                {
                                                sBlock = "fifth character"
                                            case "direction1":
                                                {
                                                sBlock = "first direction"
                                            case "direction1":
                                                {
                                                sBlock = "second direction"
                                            case "direction1":
                                                {
                                                sBlock = "third direction"
                                            case "direction1":
                                                {
                                                sBlock = "fourth direction"
                                            case "direction1":
                                                {
                                                sBlock = "fifth direction"
                                            case "number1":
                                                {
                                                sBlock = "first number"
                                            case "number2":
                                                {
                                                sBlock = "second number"
                                            case "number3":
                                                {
                                                sBlock = "third number"
                                            case "number4":
                                                {
                                                sBlock = "fourth number"
                                            case "number5":
                                                {
                                                sBlock = "fifth number"
                                            case "text1":
                                                {
                                                sBlock = "first text"
                                            case "text2":
                                                {
                                                sBlock = "second text"
                                            case "text3":
                                                {
                                                sBlock = "third text"
                                            case "text4":
                                                {
                                                sBlock = "fourth text"
                                            case "text5":
                                                {
                                                sBlock = "fifth text"
                                            case "item1":
                                                {
                                                sBlock = "first item"
                                            case "item2":
                                                {
                                                sBlock = "second item"
                                            case "item3":
                                                {
                                                sBlock = "third item"
                                            case "item4":
                                                {
                                                sBlock = "fourth item"
                                            case "item5":
                                                {
                                                sBlock = "fifth item"
                                            case "location1":
                                                {
                                                sBlock = "first location"
                                            case "location2":
                                                {
                                                sBlock = "second location"
                                            case "location3":
                                                {
                                                sBlock = "third location"
                                            case "location4":
                                                {
                                                sBlock = "fourth location"
                                            case "location5":
                                                {
                                                sBlock = "fifth location"
                                        }
                                    }

                                }
                            }
                        default:
                            {
                    }


                    if (bReference)
                    {
                        'sBlock = "%" & sBlock & "%"
                        .SelectionFont = New Font(.SelectionFont, FontStyle.Underline);
                        .SelectionColor = Color.Blue;
                    Else
                        If ! bLastBlock && iLength > 0 Then sBlock = "%" + sBlock
                        .SelectionFont = New Font(.SelectionFont, FontStyle.Regular);
                        .SelectionColor = Color.Black;
                    }

                    If sBlock <> "" Then .SelectedText = sBlock
                    .SelectionStart = .TextLength;
                    bLastBlock = bReference

                    iLength += sOrigBlock.Length + 1;

                Next;

            }
        }
        catch (ObjectDisposedException exD)
        {
            ' Ignore
        }
        catch (Exception ex)
        {
            ErrMsg("LoadSpecific error", ex);
        }

    }



    private void txtSpecific_GotFocus(object sender, System.EventArgs e)
    {
        Me.BlueBorder.Focus();
    }



    private void txtSpecific_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private int iCharPos = txtSpecific.GetCharIndexFromPosition(new Point(e.X, e.Y));
        private bool bLastPos = false;
        private bool bPos = false;
        private int iSpecificCount = 0;

        if (iCharPos > -1 && e.X < txtSpecific.GetPositionFromCharIndex(txtSpecific.Text.Length).X)
        {
            txtSpecific.SelectionStart = iCharPos;
            if (txtSpecific.SelectionFont.Underline)
            {
                'txtSpecific.Cursor = Cursors.Hand
                for (int iPos = 0; iPos <= iCharPos; iPos++)
                {
                    txtSpecific.SelectionStart = iPos;
                    bPos = txtSpecific.SelectionFont.Underline
                    if (bPos && Not bLastPos)
                    {
                        iSpecificCount += 1;
                    }
                    bLastPos = bPos
                Next;
                tas.Specifics(iSpecificCount - 1).Keys = ChooseKey(iSpecificCount, txtSpecific.PointToScreen(New Point(e.X, e.Y)));
                LoadSpecific(sGeneralKey, tas);
                RaiseEvent Changed();
            }
        }

    }



    private void txtSpecific_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        private int iCharPos = txtSpecific.GetCharIndexFromPosition(new Point(e.X, e.Y));

        if (iCharPos > -1 && e.X < txtSpecific.GetPositionFromCharIndex(txtSpecific.Text.Length).X)
        {
            txtSpecific.SelectionStart = iCharPos;
            if (txtSpecific.SelectionFont.Underline)
            {
                txtSpecific.Cursor = Cursors.Hand;
            Else
                txtSpecific.Cursor = Cursors.Arrow;
            }
        Else
            txtSpecific.Cursor = Cursors.Arrow;
        }

    }


    private StringArrayList ChooseKey(int iSpecific, Point P)
    {

        private New frmPickKeys(P, tas.Specifics(iSpecific - 1).Multiple) fPickKeys;

        ChooseKey = Nothing

        With fPickKeys;
            .Text = "Select ";
            switch (tas.Specifics(iSpecific - 1).Type)
            {
                case ReferencesType.Object:
                    {
                    .Text &= "Object";
                    fPickKeys.AddItem("[ Referenced Object ]", "", My.Resources.Resources.imgObjectStatic16);
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        fPickKeys.AddItem(ob);
                    Next;
                case ReferencesType.Character:
                    {
                    .Text &= "Character";
                    fPickKeys.AddItem("[ The Player Character ]", THEPLAYER, My.Resources.Resources.imgPlayer16);
                    fPickKeys.AddItem("[ Referenced Character ]", "", My.Resources.Resources.imgCharacter16);
                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                        fPickKeys.AddItem(ch);
                    Next;
                case ReferencesType.Location:
                    {
                    .Text &= "Location";
                    fPickKeys.AddItem("[ Referenced Location ]", "", My.Resources.Resources.imgLocation16);
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        fPickKeys.AddItem(loc);
                    Next;
                case ReferencesType.Direction:
                    {
                    .Text &= "Direction";
                    fPickKeys.AddItem("[ Referenced Direction ]", "", My.Resources.Resources.imgCentre16);
                    For Each eDir As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                        fPickKeys.AddItem(DirectionName(eDir), eDir.ToString, My.Resources.Resources.imgCentre16);
                    Next;
                case ReferencesType.Text:
                    {
                    .Text &= "Text";
                    fPickKeys.AddItem("[ Referenced Text ]", "", My.Resources.Resources.imgALR16);
                    fPickKeys.AddItem("[ Specific Text ]", "SPECIFICTEXT", My.Resources.Resources.imgALR16);
                case ReferencesType.Item:
                    {
                    .Text &= "Item";
                    fPickKeys.AddItem("[ Referenced Item ]", "", My.Resources.Resources.imgHelp16);
                    For Each ob As clsObject In Adventure.htblObjects.Values
                        fPickKeys.AddItem(ob);
                    Next;
                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                        fPickKeys.AddItem(ch);
                    Next;
                    For Each loc As clsLocation In Adventure.htblLocations.Values
                        fPickKeys.AddItem(loc);
                    Next;
            }
            If tas.Specifics(iSpecific - 1).Multiple Then .Text &= "s"

            if (tas.Specifics(iSpecific - 1).Type = ReferencesType.Text)
            {
                if (tas.Specifics(iSpecific - 1).Keys IsNot null && tas.Specifics(iSpecific - 1).Keys.Count = 1)
                {
                    private string sText = tas.Specifics(iSpecific - 1).Keys(0);
                    if (sText = "")
                    {
                        SelectListItem(.lvwKeys, "");
                    Else
                        .lvwKeys.Items(1).SubItems(1).Value = sText;
                        SelectListItem(.lvwKeys, sText);
                    }
                }
            Else
                if (Not tas.Specifics(iSpecific - 1).Keys Is null)
                {
                    For Each sKey As String In tas.Specifics(iSpecific - 1).Keys
                        SelectListItem(.lvwKeys, sKey);
                    Next;
                Else
                    SelectListItem(.lvwKeys, "");
                }
            }

            .Show();
            while (.Visible)
            {
                Application.DoEvents();
                Threading.Thread.Sleep(10);
            }

            if (.lvwKeys.SelectedItems.Count > 0)
            {
                private New StringArrayList sal;

                For Each lvi As Infragistics.Win.UltraWinListView.UltraListViewItem In .lvwKeys.SelectedItems
                    private string sKey = lvi.Key ' .SubItems(1).Text;
                    if (tas.Specifics(iSpecific - 1).Type = ReferencesType.Text && sKey <> "")
                    {
                        sKey = InputBox("Enter specific text:", "Specific text", IIf(sKey = "SPECIFICTEXT", "", sKey).ToString)
                    }
                    sal.Add(sKey);
                Next;
                return sal;
            }

            .Dispose();

        }

    }


    'Private Sub SelectListItem(ByVal lvw As ListView, ByVal sKey As String)

    '    For Each lvi As ListViewItem In lvw.Items
    '        If lvi.SubItems(1).Text = sKey Then
    '            lvi.Selected = True
    '            Exit Sub
    '        End If
    '    Next

    'End Sub
    private void SelectListItem(Infragistics.Win.UltraWinListView.UltraListView lvw, string sKey)
    {

        lvw.SelectedItems.Clear();
        For Each lvi As Infragistics.Win.UltraWinListView.UltraListViewItem In lvw.Items
            if (lvi.Key = sKey Then ' lvi.SubItems(1).Text = sKey)
            {
                lvw.SelectedItems.Add(lvi);
                lvw.ActiveItem = lvi;
                Exit Sub;
            }
        Next;

    }

}


}