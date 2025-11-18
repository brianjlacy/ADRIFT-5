using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class OOTextbox
{
    Inherits Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor ' RichTextBox;

    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long);

    Friend WithEvents lstIntellisense As New Infragistics.Win.UltraWinListView.UltraListView;
    Friend WithEvents popupList As New Infragistics.Win.Misc.UltraPopupControlContainer;
    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip;
    Private WithEvents cmsDrop As New System.Windows.Forms.ContextMenuStrip
    internal New Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor fte;
    internal List<clsUserFunction.Argument> Arguments { get; set; }

    private bool bLoaded = false;


    Public Overloads Property BorderStyle As Infragistics.Win.UIElementBorderStyle
        {
            get
            {
            return MyBase.BorderStyle;
        }
set(Infragistics.Win.UIElementBorderStyle)
            MyBase.BorderStyle = value;
            If value = Infragistics.Win.UIElementBorderStyle.None Then MyBase.UseOsThemes = Infragistics.Win.DefaultableBoolean.false
        }
    }


    private void OOTextbox_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
    {

        If bLoaded Then Exit Sub
        bLoaded = True

        'Me.UseOsThemes = Infragistics.Win.DefaultableBoolean.True
        Me.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI ' Stops font changing slightly when we format it;
        'Me.TextSmoothingMode = Infragistics.Win.FormattedLinkLabel.TextSmoothingMode.AntiAlias

        private Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn UltraListViewSubItemColumn1 = new Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn("Help");
        private Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn UltraListViewSubItemColumn2 = new Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn("Title");
        '
        'lstIntellisense
        '
        Me.lstIntellisense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.lstIntellisense.ItemSettings.HideSelection = false;
        Me.lstIntellisense.ItemSettings.SelectionType = Infragistics.Win.UltraWinListView.SelectionType.[Single];
        Me.lstIntellisense.Location = New System.Drawing.Point(148, 46);
        Me.lstIntellisense.MainColumn.AutoSizeMode = Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems;
        Me.lstIntellisense.MainColumn.DataType = GetType(String);
        Me.lstIntellisense.MainColumn.Sorting = Infragistics.Win.UltraWinListView.Sorting.Ascending;
        Me.lstIntellisense.Name = "lstIntellisense";
        Me.lstIntellisense.Size = New System.Drawing.Size(162, 254);
        UltraListViewSubItemColumn1.Key = "Help";
        UltraListViewSubItemColumn2.Key = "Title";
        Me.lstIntellisense.SubItemColumns.AddRange(New Infragistics.Win.UltraWinListView.UltraListViewSubItemColumn() {UltraListViewSubItemColumn1, UltraListViewSubItemColumn2});
        Me.lstIntellisense.TabIndex = 3;
        Me.lstIntellisense.Text = "UltraListView1";
        Me.lstIntellisense.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.List;
        Me.lstIntellisense.ViewSettingsDetails.AllowColumnMoving = false;
        Me.lstIntellisense.ViewSettingsDetails.AllowColumnSorting = false;
        Me.lstIntellisense.ViewSettingsDetails.ColumnAutoSizeMode = Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.VisibleItems;
        Me.lstIntellisense.ViewSettingsDetails.FullRowSelect = true;
        Me.lstIntellisense.ViewSettingsList.MultiColumn = false;
        Me.lstIntellisense.Visible = false;
        '
        'popupList
        '
        Me.popupList.PopupControl = Me.lstIntellisense;

        'Me.AutoWordSelection = False

        fte.WrapText = false;
        fte.TreatValueAs = Me.TreatValueAs;
        'Dim p As Control = Me
        'While p.Parent IsNot Nothing
        '    p = p.Parent
        'End While
        fte.Parent = Me.Parent ' p;
        fte.Location = Me.Location ' New Point(p.Width, 0) '-1000, -1000);
        fte.Size = Me.Size ' New Size(40, 40) ' Me.Size;
        fte.ReadOnly = true;
        fte.TabStop = false;
        fte.UseOsThemes = Infragistics.Win.DefaultableBoolean.false;
        fte.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
        fte.Appearance.BackColor = Color.FromArgb(160, 160, 160) ' Border colour;
        fte.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
        fte.BringToFront();

    }


    private void rtxtSource_DragEnter(System.Object sender, System.Windows.Forms.DragEventArgs e)
    {

        For Each sFormat As String In e.Data.GetFormats
            switch (sFormat)
            {
                case "Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection":
                    {
                    With (Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection)(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"));
                        if (.Count = 1)
                        {
                            switch (.Item(0).SubItems(2).Text)
                            {
                                case "Location":
                                case "Object":
                                case "Task":
                                case "Character":
                                case "Property":
                                case "Variable":
                                case "Event":
                                    {
                                    e.Effect = DragDropEffects.Move | DragDropEffects.Copy ' For some reason, doesn't work without Move on UFTE;
                                default:
                                    {
                                    e.Effect = DragDropEffects.None;
                            }
                        Else
                            e.Effect = DragDropEffects.None;
                        }
                    }

                case "System.String":
                    {
                    If (e.KeyState + 8) = 8 Then '|| iHighlightLength = 0 Then ' Ctrl key
                        e.Effect = DragDropEffects.Copy;
                    Else
                        e.Effect = DragDropEffects.Move;
                    }

                case "DeviceIndependentBitmap":
                    {
                    e.Effect = DragDropEffects.None;
                    Exit For;

                default:
                    {
                    'e.Effect = DragDropEffects.None
            }
        Next;

        If e.Effect <> DragDropEffects.None Then Me.Focus()

    }


    private bool bDragLeave = false;
    private void OOTextbox_DragLeave(object sender, System.EventArgs e)
    {
        bDragLeave = True
        Debug.WriteLine("DragLeave");
        if (iHighlightLength > 0)
        {
            EditInfo.SelectionStart = iHighlightStart;
            EditInfo.SelectionLength = iHighlightLength;
        }
    }
    private void OOTextbox_MouseLeave(object sender, System.EventArgs e)
    {
        Debug.WriteLine("MouseLeave");
        bDragLeave = False
    }
    private void OOTextbox_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
    {
        'Debug.WriteLine("MouseEnterElement " & e.Element.Control.Name & e.Element.ToString)
        'If bDragLeave Then
        '    mouse_event(&H4, 0, 0, 0, 1) ' Release the mouse (not sure why it gets stuck down)
        '    bDragLeave = False
        'End If
    }
    private void OOTextbox_Enter(object sender, System.EventArgs e)
    {
        Debug.WriteLine("Enter");
    }
    Private WithEvents tmrReselect As New Timer
    private void OOTextbox_MouseEnter(object sender, System.EventArgs e)
    {
        Debug.WriteLine("MouseEnter");
        if (bDragLeave)
        {
            if (MouseButtons = Windows.Forms.MouseButtons.None)
            {
                Debug.WriteLine("Unstick mouse");
                'Application.DoEvents()
                EditInfo.SelectionLength = iHighlightLength;
                EditInfo.SelectionStart = iHighlightStart;
                mouse_event(&H4, 0, 0, 0, 1) ' Release the mouse (not sure why it gets stuck down);
                tmrReselect.Interval = 1;
                tmrReselect.Enabled = true;
            ElseIf MouseButtons = Windows.Forms.MouseButtons.Left Then
                tmrReselect.Interval = 1;
                tmrReselect.Enabled = true;
            }
        }
    }
    private void tmrReselect_Tick(object sender, System.EventArgs e)
    {

        'mouse_event(&H4, 0, 0, 0, 1) ' Release the mouse (not sure why it gets stuck down)
        if (iHighlightLength > 0 && (EditInfo.SelectionStart <> iHighlightStart || EditInfo.SelectionLength <> iHighlightLength))
        {
            EditInfo.SelectionLength = iHighlightLength;
            EditInfo.SelectionStart = iHighlightStart;
            Focus();
        Else
            tmrReselect.Enabled = false;
            bDragLeave = False
            ClearHighlight();
        }

    }

    private void rtxtSource_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {

        try
        {
            if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection") || e.Data.GetDataPresent("System.String"))
            {

                ' Set cursor to where we're pointing
                private int iY = e.Y - Me.PointToScreen(new Point(0, 0)).Y;
                private int iX = e.X - Me.PointToScreen(new Point(0, 0)).X;
                private int iNewIndex;

                SelectionLength = 0
                iNewIndex = GetCharIndexFromPosition(New Point(iX, iY))

                private int iTextLength = Text.Replace(vbCrLf, "@").Length 'Text.Length - Math.Max(fte.EditInfo.GetLineCount - 1, 0);
                If iY > GetPositionFromCharIndex(iTextLength).Y + 14 _
                    || (iY > GetPositionFromCharIndex(iTextLength).Y && iX > GetPositionFromCharIndex(iTextLength).X) _;
                    Then iNewIndex = Text.Length;
                Debug.WriteLine("HERE");
                SelectionStart = iNewIndex
                'Application.DoEvents()

                if (e.Data.GetDataPresent("System.String"))
                {
                    if (iHighlightLength > 0 && iNewIndex >= iHighlightStart && iNewIndex <= iHighlightStart + iHighlightLength)
                    {
                        e.Effect = DragDropEffects.None;
                    Else
                        If (e.KeyState + 8) = 8 Then '|| iHighlightLength = 0 Then ' Ctrl key
                            e.Effect = DragDropEffects.Copy;
                        Else
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                }

            }
        Catch
            ErrMsg("DragOver error");
        }

    }


    private void rtxtSource_DragDrop(System.Object sender, System.Windows.Forms.DragEventArgs e)
    {

        try
        {
            'If UltraSpellChecker1 Is Nothing Then InitSpellCheck()

            if (e.Data.GetDataPresent("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"))
            {
                With (Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection)(e.Data.GetData("Infragistics.Win.UltraWinListView.UltraListViewSelectedItemsCollection"));

                    If .Count = 0 Then Exit Sub

                    .Item(0).Activate();

                    Application.DoEvents();

                    cmsDrop.Items.Clear();
                    cmsDrop.Tag = .Item(0).Key;

                    switch (.Item(0).SubItems(2).Text)
                    {
                        case "Location":
                            {
                            cmsDrop.Items.Add("Location Name", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Location Key", My.Resources.Resources.imgLocation16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Display Location", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Exits from Location", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Objects at Location", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                        case "Object":
                            {
                            cmsDrop.Items.Add("Object Name", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Object Key", My.Resources.Resources.imgObjectDynamic16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Object Description", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("List objects On", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("List objects Inside", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("List objects On and Inside", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Parent of Object", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                        case "Task":
                            {
                            cmsDrop.Items.Add("Task Key", My.Resources.Resources.imgTaskGeneral16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Task Completed", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                        case "Character":
                            {
                            cmsDrop.Items.Add("Character Name", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Character Key", My.Resources.Resources.imgCharacter16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Character Description", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("List objects held by Character", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("List objects worn by Character", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            'cmsDrop.Items.Add("List objects worn and held by Character", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem)
                            cmsDrop.Items.Add("Location of Character", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Parent of Character", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                        case "Property":
                            {
                            cmsDrop.Items.Add("Property Value", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Property Key", My.Resources.Resources.imgProperty16, AddressOf SelectedMenuItem);
                        case "Variable":
                            {
                            cmsDrop.Items.Add("Variable Value", My.Resources.Resources.imgALR16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Variable Key", My.Resources.Resources.imgVariable16, AddressOf SelectedMenuItem);
                        case "Event":
                            {
                            cmsDrop.Items.Add("Event Length", My.Resources.Resources.imgVariable16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Event Position", My.Resources.Resources.imgVariable16, AddressOf SelectedMenuItem);
                            cmsDrop.Items.Add("Event Key", My.Resources.Resources.imgEvent16, AddressOf SelectedMenuItem);
                    }
                    cmsDrop.Show(MousePosition);

                }
            ElseIf e.Data.GetDataPresent("System.String") Then
                With SafeString(e.Data.GetData("System.String"));
                    iSelTextStart = EditInfo.SelectionStart

                    mouse_event(&H4, 0, 0, 0, 1) ' Release the mouse (not sure why it gets stuck down);


                    if (iHighlightLength > 0 && iSelTextStart >= iHighlightStart && iSelTextStart <= iHighlightStart + iHighlightLength)
                    {
                        ClearHighlight();
                        Exit Sub;
                    }

                    SelectedText = .ToString
                    iSelTextLength = .ToString.Length

                    if ((e.Effect And DragDropEffects.Move) = DragDropEffects.Move)
                    {
                        if (iSelTextStart > iHighlightStart)
                        {
                            iSelTextStart -= iHighlightLength;
                        Else
                            iHighlightStart += iHighlightLength;
                        }
                        EditInfo.SelectionStart = iHighlightStart;
                        EditInfo.SelectionLength = iHighlightLength;
                        try
                        {
                            EditInfo.Cut();
                        Catch
                            try
                            {
                                Threading.Thread.Sleep(1);
                                EditInfo.Cut();
                            Catch
                                ' Give up
                            }
                        }
                    }

                    tmrSelectText.Interval = 1;
                    tmrSelectText.Start();
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Drop Error: " + ex.Message, "ADRIFT Developer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }

    Private WithEvents tmrSelectText As New Timer
    private int iSelTextStart;
    private int iSelTextLength;
    private void tmrSelectText_Tick(object sender, System.EventArgs e)
    {
        tmrSelectText.Enabled = false;
        EditInfo.SelectionStart = iSelTextStart;
        EditInfo.SelectionLength = iSelTextLength;
    }

    private void SelectedMenuItem(object sender, EventArgs e)
    {

        private ToolStripMenuItem mnu = CType(sender, ToolStripMenuItem);
        private string sKey = mnu.Owner.Tag.ToString;
        private string sResult = "";

        switch (CType(sender, ToolStripMenuItem).Text)
        {
            case "Location Name":
            case "Object Name":
            case "Character Name":
                {
                sResult = sKey & ".Name"
            case "Location Key":
            case "Object Key":
            case "Task Key":
            case "Character Key":
            case "Property Key":
            case "Variable Key":
            case "Event Key":
                {
                sResult = sKey
            case "Display Location":
                {
                sResult = sKey & ".Description"
            case "Exits from Location":
                {
                sResult = sKey & ".Exits.List"
            case "Objects at Location":
                {
                sResult = sKey & ".Objects.List" '(Indefinite)"
            case "Object Description":
                {
                sResult = sKey & ".Description"
            case "List objects On":
                {
                sResult = sKey & ".Children(On).List"
            case "List objects Inside":
                {
                sResult = sKey & ".Contents.List"
            case "List objects On and Inside":
                {
                sResult = sKey & ".Children.List"
            case "Parent of Object":
            case "Parent of Character":
                {
                sResult = sKey & ".Parent"
            case "Task Completed":
                {
                sResult = "%TaskCompleted[" & sKey & "]%"
            case "Character Description":
                {
                sResult = sKey & ".Description"
            case "List objects held by Character":
                {
                sResult = sKey & ".Held.List" '(Indefinite)"
            case "List objects worn by Character":
                {
                sResult = sKey & ".Worn.List" '(Indefinite)"
                'Case "List objects worn and held by Character"
                '    sResult = sKey & ".WornAndHeld.List(Indefinite)"
            case "Location of Character":
                {
                sResult = sKey & ".Location"
            case "Property Value":
                {
                sResult = "<Item Key>." & sKey
            case "Variable Value":
                {
                private clsVariable var = null;
                if (Adventure.htblVariables.TryGetValue(sKey, var))
                {
                    sResult = "%" & var.Name & If(var.Length > 1, "[]", "").ToString & "%"
                }
            case "Event Length":
                {
                sResult = sKey & ".Length"
            case "Event Position":
                {
                sResult = sKey & ".Position"
        }

        if (sResult <> "")
        {
            SelectedText = sResult
        }

    }


    ' E.g. Player.Held(False).Readable.Count => Player, Held, Readable, Count
    private void GetKeywords(ref htblArgs As Dictionary(Of Integer, String)
    {

        private New StringArrayList arlKeywords;
        htblArgs = New Dictionary(Of Integer, String)

        private string sKeyword = "";
        private string sArgs = "";
        private int iLevel = 0;
        private int i = SelectionStart - 1;
        while (Text.Substring(i, 1) <> "(" && Text.Substring(i, 1) <> ".")
        {
            i -= 1;
            If i < 1 Then Return arlKeywords
        }
        i -= 1;

        while (i >= 0)
        {
            switch (Text.Substring(i, 1))
            {
                case "a" To "z":
                case "A" To "Z":
                case "0" To "9":
                case "_":
                case "-":
                    {
                    If iLevel = 0 Then sKeyword = Text.Substring(i, 1) + sKeyword Else sArgs = Text.Substring(i, 1) + sArgs
                case " ":
                case ":
                case ":
                    {
                    If iLevel = 0 Then Exit While
                case "(":
                    {
                    iLevel += 1;
                case ")":
                    {
                    iLevel -= 1;
                case "%":
                    {
                    If iLevel = 0 Then sKeyword = Text.Substring(i, 1) + sKeyword Else sArgs = Text.Substring(i, 1) + sArgs
                    If sKeyword.Length > 1 Then Exit While
                case ".":
                    {
                    arlKeywords.Add(sKeyword);
                    If sArgs <> "" Then htblArgs.Add(arlKeywords.Count - 1, sArgs.ToLower)
                    sKeyword = ""
                    sArgs = ""
                default:
                    {
                    Exit While;
            }
            i -= 1;
        }

        arlKeywords.Add(sKeyword);
        return arlKeywords;

    }


    private void GetKeywordType(StringArrayList arlKeywords, Dictionary(Of Integer htblArgs, String)
    {

        If arlKeywords.Count = 0 Then Exit Sub
        private int i = 0;
        private string sKeyword;
Start:;
        sKeyword = arlKeywords(i)

        if (Arguments IsNot null)
        {
            For Each arg As clsUserFunction.Argument In Arguments
                if (sKeyword = "%" + arg.Name + "%")
                {
                    switch (arg.Type)
                    {
                        case clsUserFunction.ArgumentType.Character:
                            {
                            bChar = True
                        case clsUserFunction.ArgumentType.Location:
                            {
                            bLoc = True
                        case clsUserFunction.ArgumentType.Object:
                            {
                            bObject = True
                    }
                }
            Next;
        }

        switch (sKeyword)
        {
            case "%objects%":
            case "%object%":
            case "%object1%":
            case "%object2%":
            case "%object3%":
            case "%object4%":
            case "%object5%":
                {
                bObject = True
                If sKeyword = "%objects%" Then bObjectList = true
            case "%characters%":
            case "%character%":
            case "%character1%":
            case "%character2%":
            case "%character3%":
            case "%character4%":
            case "%character5%":
            case "%Player%":
            case "%ConvCharacter%":
            case "%AloneWithChar%":
                {
                bChar = True
                If sKeyword = "%characters%" Then bCharacterList = true
            case "%location%":
            case "%location1%":
            case "%location2%":
            case "%location3%":
            case "%location4%":
            case "%location5%":
                {
                bLoc = True
            case "%item%":
            case "%item1%":
            case "%item2%":
            case "%item3%":
            case "%item4%":
            case "%item5%":
                {
                bObject = True
                bChar = True
                bLoc = True
                bItem = True
            case "Parent":
                {
                ' Could be object or character
                bObject = True
                bChar = True
            case "Characters":
                {
                if (i < arlKeywords.Count - 1)
                {
                    bCharacterList = True
                }
            case "Contents":
                {
                if (i < arlKeywords.Count - 1)
                {
                    private string sArgs = "";
                    If htblArgs.ContainsKey(i) Then sArgs = htblArgs(i)
                    If sArgs = "" || sArgs = "all" || sArgs = "objects" Then bObjectList = true
                    If sArgs = "" || sArgs = "all" || sArgs = "characters" Then bCharacterList = true
                }
            case "Held":
            case "Worn":
            case "Objects":
            case "WornAndHeld":
                {
                if (i < arlKeywords.Count - 1)
                {
                    bObjectList = True
                    'bCharacterList = True
                }
            case "Children":
                {
                if (i < arlKeywords.Count - 1)
                {
                    private string sArgs = "";
                    If htblArgs.ContainsKey(i) Then sArgs = htblArgs(i)
                    If sArgs = "" || sArgs = "all" || sArgs = "in" || sArgs = "on" || sArgs.Contains("objects") Then bObjectList = true
                    If sArgs = "" || sArgs = "all" || sArgs = "in" || sArgs = "on" || sArgs.Contains("characters") Then bCharacterList = true
                }
            case "Exits":
                {
                If i < arlKeywords.Count - 1 Then bExitList = true
            case "Location":
                {
                bLoc = True
            case "LocationTo":
                {
                bLoc = True
            case "%direction%":
            case "%direction1%":
            case "%direction2%":
            case "%direction3%":
            case "%direction4%":
            case "%direction5%":
                {
                bDirection = True
            default:
                {
                if (Adventure.htblObjects.ContainsKey(sKeyword))
                {
                    bObject = True
                    oObject = Adventure.htblObjects(sKeyword)
                ElseIf Adventure.htblCharacters.ContainsKey(sKeyword) Then
                    bChar = True
                    oCharacter = Adventure.htblCharacters(sKeyword)
                ElseIf Adventure.htblLocations.ContainsKey(sKeyword) Then
                    bLoc = True
                    oLocation = Adventure.htblLocations(sKeyword)
                ElseIf Adventure.htblAllProperties.ContainsKey(sKeyword) Then
                    switch (Adventure.htblAllProperties(sKeyword).Type)
                    {
                        case clsProperty.PropertyTypeEnum.ObjectKey:
                            {
                            bObject = True
                        case clsProperty.PropertyTypeEnum.CharacterKey:
                            {
                            bChar = True
                        case clsProperty.PropertyTypeEnum.LocationKey:
                            {
                            bLoc = True
                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                        case clsProperty.PropertyTypeEnum.StateList:
                            {
                            ' Check previous word
                            if (i < arlKeywords.Count - 1)
                            {
                                i += 1;
                                GoTo Start;
                            }
                        case clsProperty.PropertyTypeEnum.ValueList:
                        case clsProperty.PropertyTypeEnum.Integer:
                            {
                            ' Check previous word
                            bSummable = True
                            if (i < arlKeywords.Count - 1)
                            {
                                i += 1;
                                GoTo Start;
                            }
                    }
                ElseIf Adventure.htblEvents.ContainsKey(sKeyword) Then
                    bEvt = True
                    oEvent = Adventure.htblEvents(sKeyword)
                ElseIf Adventure.htblGroups.ContainsKey(sKeyword) Then
                    private clsGroup grp = Adventure.htblGroups(sKeyword);
                    switch (grp.GroupType)
                    {
                        case clsGroup.GroupTypeEnum.Characters:
                            {
                            bCharacterList = True
                        case clsGroup.GroupTypeEnum.Locations:
                            {
                            bLocationList = True
                        case clsGroup.GroupTypeEnum.Objects:
                            {
                            bObjectList = True
                    }
                ElseIf Arguments != null Then
                    For Each arg As clsUserFunction.Argument In Arguments
                        if ("%" + arg.Name + "%" = sKeyword)
                        {
                            switch (arg.Type)
                            {
                                case clsUserFunction.ArgumentType.Object:
                                    {
                                    bObject = True
                                case clsUserFunction.ArgumentType.Location:
                                    {
                                    bLoc = True
                                case clsUserFunction.ArgumentType.Character:
                                    {
                                    bChar = True
                            }
                        }
                    Next;
                }
                For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                    if (sKeyword = d.ToString)
                    {
                        bDirection = True
                        Exit For;
                    }
                Next;
        }

    }


    private string sIntellisenseWord = "";
    private Point ptDropdown;

    private void Intellisense()
    {

        try
        {
            If bIntellisenseWorking | DesignMode Then Exit Sub
            With lstIntellisense;

                if (.Visible && SelectionStart > 0 && (Text.Substring(SelectionStart - 1, 1) = "." || Text.Substring(SelectionStart - 1, 1) = "(" || Text.Substring(SelectionStart - 1, 1) = ")" || Text.Substring(SelectionStart - 1, 1) = " "))
                {
                    ' Case-correct anything
                    if (.Items.Exists(sIntellisenseWord))
                    {
                        SelectIntellisense();
                    }
                    popupList.Close();
                }

                if (.Visible)
                {

                    sIntellisenseWord = ""
                    private int iEnd = SelectionStart;
                    private int iStart = iEnd;
                    while (iStart > 0)
                    {
                        If Text(iStart - 1) = "."c || Text(iStart - 1) = "(" || Text(iStart - 1) = "," Then Exit While
                        iStart -= 1;
                    }
                    If iStart > -1 Then sIntellisenseWord = Text.Substring(iStart, iEnd - iStart)

                    private int iVisibleCount = 0;
                    private int iWidth = 0;
                    for (int i = .Items.Count - 1; i <= 0; i += -1)
                    {
                        if (Not .Items(i).Text.ToLower.Contains(sIntellisenseWord.ToLower) && Not .Items(i).Key.ToLower.Contains(sIntellisenseWord.ToLower))
                        {
                            .Items(i).Visible = false;
                            If .SelectedItems.Contains(.Items(i)) Then .SelectedItems.Remove(.Items(i))
                        Else
                            iVisibleCount += 1;
                            .Items(i).Visible = true;
                        }
                    Next;

                    if (.Items.Exists(sIntellisenseWord))
                    {
                        .SelectedItems.Clear();
                        .SelectedItems.Add(.Items(sIntellisenseWord));
                    }

                    if (.SelectedItems.Count = 0)
                    {
                        For Each vli As Infragistics.Win.UltraWinListView.UltraListViewItem In .Items
                            if (vli.Visible)
                            {
                                .SelectedItems.Add(vli);
                                Exit For;
                            }
                        Next;
                    }

                    if (iVisibleCount > 0)
                    {
                        .MainColumn.PerformAutoResize(Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems);
                        private New Size(.MainColumn.Width + 5, Math.Min(iVisibleCount, 16) * 18 + 2) sizeDropdown;
                        if (popupList.PreferredDropDownSize <> sizeDropdown)
                        {
                            popupList.PopupControl.Size = sizeDropdown;
                            popupList.PreferredDropDownSize = sizeDropdown;
                            popupList.Show(Me, ptDropdown);
                            IntellisenseHelp();
                        }
                        Exit Sub;
                    }
                    popupList.Close();
                }

                .Items.Clear();


                if (SelectionStart > 0 && (Text.Substring(SelectionStart - 1, 1) = "(" || Text.Substring(SelectionStart - 1, 1) = ","))
                {
                    ' Grab proceeding word
                    private int i = SelectionStart - 1;
                    while (Text.Substring(i, 1) <> "(")
                    {
                        i -= 1;
                        If i < 1 Then GoTo SkipArgs
                    }
                    i -= 1;
                    'Dim iBracketLevel As Integer = 0
                    private string sKeyword = "";
                    while (i >= 0)
                    {
                        switch (Text.Substring(i, 1))
                        {
                            case "a" To "z":
                            case "A" To "Z":
                            case "0" To "9":
                            case "_":
                            case "-":
                                {
                                sKeyword = Text.Substring(i, 1) & sKeyword
                            case "%":
                                {
                                sKeyword = Text.Substring(i, 1) & sKeyword
                                If sKeyword.Length > 1 Then Exit While
                            default:
                                {
                                Exit While;
                        }
                        i -= 1;
                    }

                    ' Functions that have arguments
                    switch (sKeyword)
                    {
                        case "Children":
                            {
                            AddTool("All", "All", My.Resources.Resources.imgProperty16, "Everything In and On this object.  This is the default.");
                            AddTool("Characters, In", "Characters, In", My.Resources.Resources.imgProperty16, "All Characters inside this object");
                            AddTool("Characters, On", "Characters, On", My.Resources.Resources.imgProperty16, "All Characters on this object");
                            AddTool("Characters", "Characters", My.Resources.Resources.imgProperty16, "All Characters on and inside this object");
                            AddTool("In", "In", My.Resources.Resources.imgProperty16, "Everything inside this object");
                            AddTool("Objects, In", "Objects, In", My.Resources.Resources.imgProperty16, "All Objects inside this object");
                            AddTool("Objects, On", "Objects, On", My.Resources.Resources.imgProperty16, "All Objects on this object");
                            AddTool("Objects", "Objects", My.Resources.Resources.imgProperty16, "All Objects on and inside this object");

                        case "Contents":
                            {
                            AddTool("All", "All", My.Resources.Resources.imgProperty16, "Everything Inside this object.  This is the default.");
                            AddTool("Characters", "Characters", My.Resources.Resources.imgProperty16, "All Characters inside this object");
                            AddTool("Objects", "Objects", My.Resources.Resources.imgProperty16, "All Objects inside this object");

                        case "Held":
                            {
                            AddTool("true", "true", My.Resources.Resources.imgProperty16, "Returns held items recursively (i.e. objects inside held items).  This is the default.");
                            AddTool("false", "false", My.Resources.Resources.imgProperty16, "Does not return items recursively (i.e. only directly held items)");

                        case "List":
                            {
                            private Dictionary<int, string> htblArgs = null;
                            private StringArrayList arlKeywords = GetKeywords(htblArgs);
                            arlKeywords.RemoveAt(0);
                            private bool bChar = false;
                            private bool bObject = false;
                            private bool bCharList = false;
                            private bool bObjectList = false;
                            private bool bLocationList = false;
                            GetKeywordType(arlKeywords, htblArgs, bObject, bObjectList, null, bChar, bCharList, null, false, null, false, false, null, bLocationList, null, null, null);
                            if (bObject || bObjectList)
                            {
                                AddTool("Definite", "Definite", My.Resources.Resources.imgProperty16, "Use the Definite article in names (e.g. *the* box).  This is the default.");
                                AddTool("Indefinite", "Indefinite", My.Resources.Resources.imgProperty16, "Use the Indefinite article in names (e.g. *a* box)");
                                AddTool("true", "true", My.Resources.Resources.imgProperty16, "List everything in/on every item in the list.  This is the default.");
                                AddTool("false", "false", My.Resources.Resources.imgProperty16, "Do not list anything in/on every item in the list.");
                            }
                            AddTool("&", "&", My.Resources.Resources.imgProperty16, "Use ""and"" as the list separator.  This is the default.");
                            AddTool("|", "|", My.Resources.Resources.imgProperty16, "Use ""or"" as the list separator.");
                            AddTool("Rows", "Rows", My.Resources.Resources.imgProperty16, "Display each item in the list on a separate row.");

                        case "LocationTo":
                            {
                            For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                                AddTool(DirectionName(d), d.ToString, My.Resources.Resources.imgChildren, "Returns the location " + DirectionName(d) + " from here.");
                            Next;
                            AddTool("%direction%", "%direction%", My.Resources.Resources.imgChildren, "Returns the location in the Referenced Direction from here.");

                        case "Name":
                            {
                            private Dictionary<int, string> htblArgs = null;
                            private StringArrayList arlKeywords = GetKeywords(htblArgs);
                            arlKeywords.RemoveAt(0);
                            private bool bChar = false;
                            private bool bObject = false;
                            GetKeywordType(arlKeywords, htblArgs, bObject, false, null, bChar, false, null, false, null, false, false, null, null, null, null, null);
                            if (bChar)
                            {
                                AddTool("None", "None", My.Resources.Resources.imgProperty16, "Do not replace with a pronoun at any time.", "Pronouns");
                                AddTool("Force", "Force", My.Resources.Resources.imgProperty16, "Enforce the use of pronouns.", "Pronouns");
                                AddTool("Objective", "Objective", My.Resources.Resources.imgProperty16, "Replace with the objective pronoun (me, you, him, her, it) when referred to multiple times.", "Pronouns");
                                AddTool("Possessive", "Possessive", My.Resources.Resources.imgProperty16, "Replace with the possessive pronoun (my, your, his, her, its) when referred to multiple times.", "Pronouns");
                                AddTool("Reflective", "Reflective", My.Resources.Resources.imgProperty16, "Replace with the reflective pronoun (myself, yourself, himself, herself, itself) when referred to multiple times.", "Pronouns");
                                AddTool("Subjective", "Subjective", My.Resources.Resources.imgProperty16, "Replace with the subjective pronoun (I, you, he, she, it) when referred to multiple times.  This is the default", "Pronouns");
                            }
                            if (bObject || bChar)
                            {
                                AddTool("Definite", "Definite", My.Resources.Resources.imgProperty16, "Use the Definite article in names (e.g. *the* " + IIf(bObject, "box", "man").ToString + IIf(bObject, ").  This is the default.", ").").ToString);
                                AddTool("Indefinite", "Indefinite", My.Resources.Resources.imgProperty16, "Use the Indefinite article in names (e.g. *a* " + IIf(bObject, "box", "man").ToString + IIf(bChar, ").", ").").ToString);
                                AddTool("None", "None", My.Resources.Resources.imgProperty16, "Suppress the article in names (e.g. just " + IIf(bObject, "box", "man").ToString + IIf(bChar, ").", ").").ToString);
                            }

                        case "Worn":
                            {
                            AddTool("true", "true", My.Resources.Resources.imgProperty16, "Returns worn items recursively (i.e. objects inside worn items).  This is the default.");
                            AddTool("false", "false", My.Resources.Resources.imgProperty16, "Does not return items recursively (i.e. only directly worn items)");

                        case "WornAndHeld":
                            {
                            AddTool("true", "true", My.Resources.Resources.imgProperty16, "Returns held and worn items recursively (i.e. objects inside worn/held items).  This is the default.");
                            AddTool("false", "false", My.Resources.Resources.imgProperty16, "Does not return items recursively (i.e. only directly worn or held items)");

                        default:
                            {
                            if (Adventure.htblAllProperties.ContainsKey(sKeyword))
                            {
                                private clsProperty prop = Adventure.htblAllProperties(sKeyword);
                                switch (prop.Type)
                                {
                                    case clsProperty.PropertyTypeEnum.ValueList:
                                        {
                                        For Each val As String In prop.ValueList.Keys
                                            AddTool(val, val, My.Resources.Resources.imgVariable16, "");
                                        Next;
                                    case clsProperty.PropertyTypeEnum.StateList:
                                        {
                                        For Each state As String In prop.arlStates
                                            AddTool(state, state, My.Resources.Resources.imgALR16, "");
                                        Next;
                                }
                            }
                    }

                    if (.Items.Count > 0)
                    {
                        ptDropdown = PointToScreen(GetPositionFromCharIndex(EditInfo.SelectionStart))
                        ptDropdown.Y += 18;

                        .MainColumn.PerformAutoResize(Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems);
                        private New Size(.MainColumn.Width + 5, Math.Min(.Items.Count, 16) * 18 + 2) sizeDropdown;
                        popupList.PopupControl.Size = sizeDropdown;
                        popupList.PreferredDropDownSize = sizeDropdown;
                        popupList.Show(Me, ptDropdown);

                        If lstIntellisense.SelectedItems.Count = 0 Then lstIntellisense.SelectedItems.Add(lstIntellisense.Items(0))
                        IntellisenseHelp();
                        Exit Sub;
                    }
                }
SkipArgs:;

                sIntellisenseWord = ""
                if (SelectionStart > 0 && Text.Substring(SelectionStart - 1, 1) = ".")
                {

                    private Dictionary<int, string> htblArgs = null;
                    private StringArrayList arlKeywords = GetKeywords(htblArgs);
                    private string sKeyword = "";
                    If arlKeywords.Count > 0 Then sKeyword = arlKeywords(arlKeywords.Count - 1)

                    Debug.WriteLine(sKeyword);
                    private bool bSubKeyword = arlKeywords.Count > 1 ' (i > 0 && Text.Substring(i, 1) = ".") || sPreviousKeyword <> "";
                    private bool bObject = false;
                    private bool bChar = false;
                    private bool bLoc = false;
                    private bool bEvt = false;
                    private clsObject oObject = null;
                    private clsCharacter oCharacter = null;
                    private clsLocation oLocation = null;
                    private clsEvent oEvent = null;
                    private bool bObjectList = false;
                    private bool bCharacterList = false;
                    private bool bLocationList = false;
                    private bool bExitList = false;
                    private bool bSummable = false;
                    private bool bItem = false;
                    private bool bDirection = false;

                    GetKeywordType(arlKeywords, htblArgs, bObject, bObjectList, oObject, bChar, bCharacterList, oCharacter, bLoc, oLocation, bExitList, bEvt, oEvent, bLocationList, bSummable, bItem, bDirection);

                    if (bObjectList || bCharacterList || bExitList || bLocationList)
                    {
                        AddTool("List", "List", My.Resources.Resources.imgALR16, "This returns a comma separated list" + vbCrLf + "The List function can take various parameter to control how the list outputs.  Multiple parameters can be supplied.", "List");
                        AddTool("Count", "Count", My.Resources.Resources.imgVariable16, "This returns an integer value of the number of items in the list");
                    }

                    if (bSummable)
                    {
                        AddTool("Sum", "Sum", My.Resources.Resources.imgVariable16, "This returns an integer value of the sum of the value of the items in the list");
                    }

                    if (bObjectList)
                    {
                        For Each p As clsProperty In Adventure.htblObjectProperties.Values
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.StateList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    AddTool(p.CommonName, p.Key, My.Resources.Resources.imgFilter, GetPropertyHelp(p, true));
                            }
                        Next;
                    }

                    if (bObject)
                    {
                        private string sObject = If(bItem, "item", "object").ToString;
                        AddTool(PCase(sObject) + " Name", "Name", My.Resources.Resources.imgALR16, "This returns the full " + sObject + " name");
                        AddTool("Description", "Description", My.Resources.Resources.imgALR16, "This returns the description of the " + sObject);
                        AddTool("Article", "Article", My.Resources.Resources.imgALR16, "The indefinite article of the " + sObject);
                        AddTool("Adjective", "Adjective", My.Resources.Resources.imgALR16, "The adjective (prefix) of the " + sObject);
                        AddTool("Noun", "Noun", My.Resources.Resources.imgALR16, "The primary noun of the " + sObject);
                        AddTool("Contents", "Contents", My.Resources.Resources.imgGroup16, "This returns a list of all objects/characters inside the " + sObject, "Contents([Objects/Characters])");
                        AddTool("Children", "Children", My.Resources.Resources.imgGroup16, "This returns a list of all objects/characters in/on the " + sObject, "Children([Objects/Characters], [In/On/OnAndIn])");
                        AddTool("Location", "Location", My.Resources.Resources.imgLocation16, "This returns the location of the " + sObject + ", regardless whether the object is in/on something else");
                        AddTool("Parent", "Parent", My.Resources.Resources.imgObjectDynamic16, "This returns the parent object/character/location of the " + sObject);
                        For Each p As clsProperty In Adventure.htblObjectProperties.Values
                            switch (p.Key)
                            {
                                case OBJECTARTICLE:
                                case OBJECTPREFIX:
                                case OBJECTNOUN:
                                    {
                                    ' Suppress
                                default:
                                    {
                                    private Image img = My.Resources.Resources.imgProperty16;
                                    switch (p.Type)
                                    {
                                        case clsProperty.PropertyTypeEnum.CharacterKey:
                                            {
                                            img = My.Resources.Resources.imgCharacter16
                                        case clsProperty.PropertyTypeEnum.Integer:
                                        case clsProperty.PropertyTypeEnum.ValueList:
                                            {
                                            img = My.Resources.Resources.imgVariable16
                                        case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                            {
                                            img = My.Resources.Resources.imgGroup16
                                        case clsProperty.PropertyTypeEnum.LocationKey:
                                            {
                                            img = My.Resources.Resources.imgLocation16
                                        case clsProperty.PropertyTypeEnum.ObjectKey:
                                            {
                                            img = My.Resources.Resources.imgObjectDynamic16
                                        case clsProperty.PropertyTypeEnum.Text:
                                        case clsProperty.PropertyTypeEnum.StateList:
                                            {
                                            img = My.Resources.Resources.imgALR16
                                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                                            {
                                            if (bObjectList)
                                            {
                                                img = My.Resources.Resources.imgFilter
                                            Else
                                                img = My.Resources.Resources.imgVariable16
                                            }
                                    }
                                    If oObject == null || oObject.HasProperty(p.Key) Then AddTool(p.CommonName, p.Key, img, GetPropertyHelp(p))
                            }
                        Next;
                    }

                    if (bCharacterList)
                    {
                        For Each p As clsProperty In Adventure.htblCharacterProperties.Values
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.StateList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    AddTool(p.CommonName, p.Key, My.Resources.Resources.imgFilter, GetPropertyHelp(p, true));
                            }
                        Next;
                    }

                    if (bChar)
                    {
                        AddTool("Proper Name", "ProperName", My.Resources.Resources.imgALR16, "This returns the Proper name of the character");
                        AddTool("Descriptor", "Descriptor", My.Resources.Resources.imgALR16, "This returns the descriptor of the character");
                        AddTool("Character Name", "Name", My.Resources.Resources.imgALR16, "This returns the proper name (if known) otherwise the descriptor, of the character.  This function can take parameters to specify the pronoun substitution.") ' Proper or Descriptor;
                        AddTool("Description", "Description", My.Resources.Resources.imgALR16, "This returns the description of the character");
                        AddTool("Held", "Held", My.Resources.Resources.imgGroup16, "This returns a list of everything held by the character" + vbCrLf + "The Held function can take a Boolean parameter to specify whether the list should recursively list objects inside or on other objects.  The default is true.");
                        AddTool("Location", "Location", My.Resources.Resources.imgLocation16, "This returns the location of the character, regardless whether the character is in/on something else");
                        AddTool("Worn", "Worn", My.Resources.Resources.imgGroup16, "This returns a list of everything worn by the character" + vbCrLf + "The Worn function can take a Boolean parameter to specify whether the list should recursively list objects inside or on other objects.  The default is true.");
                        AddTool("WornAndHeld", "WornAndHeld", My.Resources.Resources.imgGroup16, "This returns a list of everything worn and held by the character" + vbCrLf + "The WornAndHeld function can take a Boolean parameter to specify whether the list should recursively list objects inside or on other objects.  The default is true.");
                        AddTool("Parent", "Parent", My.Resources.Resources.imgObjectStatic16, "This returns the parent object/character/location of the character");
                        For Each p As clsProperty In Adventure.htblCharacterProperties.Values
                            private Image img = My.Resources.Resources.imgProperty16;
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.CharacterKey:
                                    {
                                    img = My.Resources.Resources.imgCharacter16
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                    {
                                    img = My.Resources.Resources.imgVariable16
                                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                    {
                                    img = My.Resources.Resources.imgGroup16
                                case clsProperty.PropertyTypeEnum.LocationKey:
                                    {
                                    img = My.Resources.Resources.imgLocation16
                                case clsProperty.PropertyTypeEnum.ObjectKey:
                                    {
                                    img = My.Resources.Resources.imgObjectDynamic16
                                case clsProperty.PropertyTypeEnum.Text:
                                case clsProperty.PropertyTypeEnum.StateList:
                                    {
                                    img = My.Resources.Resources.imgALR16
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    if (bCharacterList)
                                    {
                                        img = My.Resources.Resources.imgFilter
                                    Else
                                        img = My.Resources.Resources.imgVariable16
                                    }
                            }
                            'If p.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                            If oCharacter == null || oCharacter.HasProperty(p.Key) Then AddTool(p.CommonName, p.Key, img, GetPropertyHelp(p))
                            'End If
                        Next;
                    }

                    if (bLocationList)
                    {
                        For Each p As clsProperty In Adventure.htblLocationProperties.Values
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.StateList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    AddTool(p.CommonName, p.Key, My.Resources.Resources.imgFilter, GetPropertyHelp(p, true));
                            }
                        Next;
                    }

                    if (bLoc)
                    {
                        AddTool("Short Description", "Name", My.Resources.Resources.imgALR16, "This returns the short description of the location");
                        AddTool("Long Description", "Description", My.Resources.Resources.imgALR16, "This returns the long description of the location");
                        AddTool("LocationTo", "LocationTo", My.Resources.Resources.imgLocation16, "This returns a location relative to this one.  Requires direction parameter.");
                        AddTool("Objects", "Objects", My.Resources.Resources.imgGroup16, "This returns a list of the objects in the location");
                        AddTool("Characters", "Characters", My.Resources.Resources.imgGroup16, "This returns a list of the characters at the location");
                        AddTool("Exits", "Exits", My.Resources.Resources.imgGroup16, "This returns a list of the exits in the location");
                        For Each p As clsProperty In Adventure.htblLocationProperties.Values
                            private Image img = My.Resources.Resources.imgProperty16;
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.CharacterKey:
                                    {
                                    img = My.Resources.Resources.imgCharacter16
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                    {
                                    img = My.Resources.Resources.imgVariable16
                                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                    {
                                    img = My.Resources.Resources.imgGroup16
                                case clsProperty.PropertyTypeEnum.LocationKey:
                                    {
                                    img = My.Resources.Resources.imgLocation16
                                case clsProperty.PropertyTypeEnum.ObjectKey:
                                    {
                                    img = My.Resources.Resources.imgObjectDynamic16
                                case clsProperty.PropertyTypeEnum.Text:
                                case clsProperty.PropertyTypeEnum.StateList:
                                    {
                                    img = My.Resources.Resources.imgALR16
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    if (bLocationList)
                                    {
                                        img = My.Resources.Resources.imgFilter
                                    Else
                                        img = My.Resources.Resources.imgVariable16
                                    }
                            }
                            'If p.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                            If p.Key <> "LongLocationDescription" && p.Key <> "ShortLocationDescription" Then ' Duplicates with our shorthand versions
                                If oLocation == null || oLocation.HasProperty(p.Key) Then AddTool(p.CommonName, p.Key, img, GetPropertyHelp(p))
                            }
                        Next;
                    }

                    if (bEvt)
                    {
                        AddTool("Length", "Length", My.Resources.Resources.imgVariable16, "This returns the number of turns the event will run for");
                        AddTool("Position", "Position", My.Resources.Resources.imgVariable16, "This returns the current position within the event");
                    }

                    if (bDirection)
                    {
                        AddTool("Name", "Name", My.Resources.Resources.imgALR16, "This returns the custom name for this direction");
                    }

                    if (.Items.Count > 0)
                    {
                        ptDropdown = PointToScreen(GetPositionFromCharIndex(EditInfo.SelectionStart)) ' SelectionStart))
                        ptDropdown.Y += 18;

                        .MainColumn.PerformAutoResize(Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems);
                        private New Size(.MainColumn.Width + 5, Math.Min(.Items.Count, 16) * 18 + 2) sizeDropdown;
                        popupList.PopupControl.Size = sizeDropdown;
                        popupList.PreferredDropDownSize = sizeDropdown;
                        popupList.Show(Me, ptDropdown);
                        IntellisenseHelp();
                    }

                Else
                    If sIntellisenseWord = "" Then popupList.Close()
                }
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error building intellisense menu", ex);
        }

    }


    private void OOTextbox_TextChanged(object sender, System.EventArgs e)
    {
        If bHighlighting Then Exit Sub
        fte.Text = Text;
        Intellisense();
    }


    private void AddTool(string sName, string sKey, Image img, string sHelp, string sTitle = "")
    {

        if (Not lstIntellisense.Items.Exists(sKey))
        {
            private New Infragistics.Win.UltraWinListView.UltraListViewItem(sName, New Object[] lvi;
            lvi.Key = sKey;
            lvi.Appearance.Image = img;

            lstIntellisense.Items.Add(lvi);
        }

    }


    private string GetPropertyHelp(clsProperty p, bool bFilter = false)
    {

        private string sHelp = "";

        if (bFilter || p.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
        {
            sHelp = "This will restrict the "
            if (Adventure.htblObjectProperties.ContainsKey(p.Key))
            {
                sHelp &= "object";
            ElseIf Adventure.htblCharacterProperties.ContainsKey(p.Key) Then
                sHelp &= "character";
            ElseIf Adventure.htblLocationProperties.ContainsKey(p.Key) Then
                sHelp &= "location";
            }
            sHelp &= " list to those containing this property";
        Else
            if (Adventure.htblObjectProperties.ContainsKey(p.Key))
            {
                sHelp = "This is an object property"
            ElseIf Adventure.htblCharacterProperties.ContainsKey(p.Key) Then
                sHelp = "This is a character property"
            ElseIf Adventure.htblLocationProperties.ContainsKey(p.Key) Then
                sHelp = "This is a location property"
            }
            sHelp &= " that will return ";
            switch (p.Type)
            {
                case clsProperty.PropertyTypeEnum.CharacterKey:
                    {
                    sHelp &= "the key of a character";
                case clsProperty.PropertyTypeEnum.Integer:
                case clsProperty.PropertyTypeEnum.ValueList:
                    {
                    sHelp &= "an integer value";
                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                    {
                    sHelp &= "the key of a location group";
                case clsProperty.PropertyTypeEnum.LocationKey:
                    {
                    sHelp &= "the key of a location";
                case clsProperty.PropertyTypeEnum.ObjectKey:
                    {
                    sHelp &= "the key of an object";
                case clsProperty.PropertyTypeEnum.StateList:
                case clsProperty.PropertyTypeEnum.Text:
                    {
                    sHelp &= "a text value";
            }
        }

        return sHelp;

    }


    private void OOTextbox_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
    {
        if (lstIntellisense.Visible)
        {
            switch (e.KeyData)
            {
                case Keys.Tab:
                case Keys.Space:
                    {
                    if (lstIntellisense.SelectedItems.Count = 1)
                    {
                        e.IsInputKey = true;
                        SelectIntellisense();
                    }
                case Keys.Escape:
                    {
                    If lstIntellisense.Visible Then e.IsInputKey = true
            }
        }
    }


    private void OOTextbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        switch (e.KeyData)
        {
            case Keys.Escape:
                {
                if (lstIntellisense.Visible)
                {
                    popupList.Close();
                    e.SuppressKeyPress = true;
                }
            case Keys.Down:
                {
                if (lstIntellisense.Visible)
                {
                    if (lstIntellisense.SelectedItems.Count = 1)
                    {
                        lstIntellisense.PerformAction(Infragistics.Win.UltraWinListView.UltraListViewAction.ActivateBelow);
                    Else
                        lstIntellisense.PerformAction(Infragistics.Win.UltraWinListView.UltraListViewAction.ActivateFirst);
                    }
                    e.SuppressKeyPress = true;
                }
            case Keys.Up:
                {
                if (lstIntellisense.Visible)
                {
                    lstIntellisense.PerformAction(Infragistics.Win.UltraWinListView.UltraListViewAction.ActivateAbove);
                    e.SuppressKeyPress = true;
                }
            case Keys.Enter:
                {
                if (lstIntellisense.Visible)
                {
                    SelectIntellisense();
                    e.SuppressKeyPress = true;
                }
                If ! Multiline Then e.SuppressKeyPress = true
            case Keys.PageDown:
                {
                if (lstIntellisense.Visible)
                {
                    lstIntellisense.PerformAction(Infragistics.Win.UltraWinListView.UltraListViewAction.PageDown);
                    e.SuppressKeyPress = true;
                }
            case Keys.PageUp:
                {
                if (lstIntellisense.Visible)
                {
                    lstIntellisense.PerformAction(Infragistics.Win.UltraWinListView.UltraListViewAction.PageUp);
                    e.SuppressKeyPress = true;
                }
            case Keys.Tab:
                {
                e.SuppressKeyPress = true;
                e.Handled = true;
            case Keys.Control Or Keys.Z:
                {
                Undo() ' VB bug: http://support.microsoft.com/default.aspx?scid=kb;en-us;812943;
            case Keys.Shift Or Keys.Alt Or Keys.Back:
                {
                Redo();
        }
    }


    private bool bIntellisenseWorking = false;
    private void SelectIntellisense()
    {
        if (lstIntellisense.SelectedItems.Count = 1)
        {
            Debug.WriteLine("Selected " + lstIntellisense.SelectedItems(0).Key);
            private int iSelStart = SelectionStart;
            if (iSelStart >= sIntellisenseWord.Length && Text.Substring(iSelStart - sIntellisenseWord.Length, sIntellisenseWord.Length) = sIntellisenseWord)
            {
                bIntellisenseWorking = True
                Text = Text.Substring(0, iSelStart - sIntellisenseWord.Length) & lstIntellisense.SelectedItems(0).Key & Text.Substring(iSelStart)
                bIntellisenseWorking = False
                SelectionLength = 0
                SelectionStart = iSelStart - sIntellisenseWord.Length + lstIntellisense.SelectedItems(0).Key.Length
                if (Text.Substring(SelectionStart - lstIntellisense.SelectedItems(0).Key.Length - 1, 1) = "(")
                {
                    iSelStart = SelectionStart
                    Text = Text.Substring(0, SelectionStart) & ")" & Text.Substring(SelectionStart)
                    SelectionStart = iSelStart + 1
                }
                sIntellisenseWord = ""
            ElseIf iSelStart >= sIntellisenseWord.Length - 1 && Text.Substring(iSelStart - sIntellisenseWord.Length - 1, sIntellisenseWord.Length) = sIntellisenseWord Then
                bIntellisenseWorking = True
                Text = Text.Substring(0, iSelStart - sIntellisenseWord.Length - 1) & lstIntellisense.SelectedItems(0).Key & Text.Substring(iSelStart - 1)
                bIntellisenseWorking = False
                SelectionLength = 0
                SelectionStart = iSelStart - sIntellisenseWord.Length + lstIntellisense.SelectedItems(0).Key.Length
                sIntellisenseWord = ""
            }
            bIntellisenseWorking = True
            popupList.Close();
            bIntellisenseWorking = False
        }

    }


    private int iToolTipLeft;
    private void IntellisenseHelp()
    {

        if (lstIntellisense.SelectedItems.Count = 1)
        {
            private clsItem oItem = null;
            Adventure.dictAllItems.TryGetValue(lstIntellisense.SelectedItems(0).Key, oItem);

            With ToolTip1;
                private string sHelp = lstIntellisense.SelectedItems(0).SubItems(0).Text;
                private string sTitle = lstIntellisense.SelectedItems(0).SubItems(1).Text;
                if (sTitle = "")
                {
                    sTitle = lstIntellisense.SelectedItems(0).Text
                    If lstIntellisense.SelectedItems(0).Text <> lstIntellisense.SelectedItems(0).Key Then sTitle &= " (" + lstIntellisense.SelectedItems(0).Key + ")"
                }
                .ToolTipTitle = sTitle;
                iToolTipLeft = ptDropdown.X + lstIntellisense.Width '- 1
                private int iToolTipTop = ptDropdown.Y;

                if (Screen.FromControl(Me).WorkingArea.Width - iToolTipLeft < 300)
                {
                    iToolTipLeft = ptDropdown.X
                    iToolTipTop = ptDropdown.Y + lstIntellisense.Height '- 1
                }
                .Show(sHelp, Me, PointToClient(New Point(iToolTipLeft, iToolTipTop)));

            }
        }

    }


    private void ToolTip1_Popup(object sender, System.Windows.Forms.PopupEventArgs e)
    {
        if (Screen.FromControl(Me).WorkingArea.Width - e.ToolTipSize.Width < iToolTipLeft)
        {
            ' Ok, the tooltip has been moved to fit onto the screen
            iToolTipLeft = ptDropdown.X
            ToolTip1.Show(lstIntellisense.SelectedItems(0).SubItems(0).Text, Me, PointToClient(New Point(iToolTipLeft, ptDropdown.Y + lstIntellisense.Height)));
        }
    }


    private void lstIntellisense_ItemSelectionChanged(object sender, Infragistics.Win.UltraWinListView.ItemSelectionChangedEventArgs e)
    {
        IntellisenseHelp();
    }


    private void lstIntellisense_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        SelectIntellisense();
    }


    private void popupList_Closed(object sender, System.EventArgs e)
    {
        ToolTip1.Hide(Me);
    }








    ' Need to convert SelectionStart counting vbCrLf as 1 into count vbCrLf as 2...
    private int iSelectionStart = -1;
    public int SelectionStart
        {
            get
            {
            'If fte.Text <> Text Then fte.Text = Text
            If DesignMode Then Return -1
            'Debug.WriteLine("Get: " & Math.Min(EditInfo.SelectionStart + Math.Max(fte.EditInfo.GetLineNumber(EditInfo.SelectionStart) - 1, 0), Text.Length))
            return Math.Min(EditInfo.SelectionStart + Math.Max(fte.EditInfo.GetLineNumber(EditInfo.SelectionStart) - 1, 0), Text.Length);
            'Debug.WriteLine("Get: " & EditInfo.SelectionStart)
            'Return EditInfo.SelectionStart
            'If iSelectionStart > -1 Then Return iSelectionStart

            'Dim iSS As Integer = EditInfo.SelectionStart
            'If iSS >= Text.Length Then Return Text.Length '- 1
            'Dim x As Integer = New System.Text.RegularExpressions.Regex(vbCrLf).Matches(Text.Substring(0, iSS)).Count
            'Dim iPos As Integer = iSS '+ x '0
            'iSS += x
            'While iPos < iSS AndAlso iPos < Text.Length
            '    If Text(iPos) = vbCr Then iSS += 1
            '    iPos += 1
            'End While
            'iSelectionStart = iSS
            'Return iSS ' EditInfo.SelectionStart
        }
set(Integer)
            If DesignMode Then Exit Property
            'Debug.WriteLine("Set: " & value)
            private int iNewValue = value - Math.Max(fte.EditInfo.GetLineNumber(EditInfo.SelectionStart) - 1, 0);
            EditInfo.SelectionStart = iNewValue;
            iPreviousSelectionStart = iNewValue
        }
    }
    public int SelectionLength
        {
            get
            {
            If EditInfo == null Then Return 0
            return EditInfo.SelectionLength;
        }
set(Integer)
            If EditInfo != null Then EditInfo.SelectionLength = value
        }
    }


    public bool Multiline
        {
            get
            {
            return MyBase.WrapText;
        }
set(Boolean)
            MyBase.WrapText = value;
        }
    }

    <Obsolete("No longer required")> _;
    public bool AutoWordSelection { get; set; }

    public bool DetectUrls { get; set; }
    public string SelectedText
        {
            get
            {
            If EditInfo == null Then Return null
            return EditInfo.GetSelectedValueAsString(false);
        }
set(String)
            try
            {
                If value <> "" Then EditInfo.InsertValue(value.Replace("<", "&lt;").Replace(">", "&gt;"))
            Catch
            }
        }
    }
    public bool EnableAutoDragDrop { get; set; }

    Public Shadows Property WrapText As Boolean
        {
            get
            {
            return MyBase.WrapText;
        }
set(Boolean)
            MyBase.WrapText = value;
            if (Not value)
            {
                MyBase.ScrollBarDisplayStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarDisplayStyle.Never;
            }
        }
    }
    Public ReadOnly Property GetCharIndexFromPosition(ByVal pt As Point) As Integer
        {
            get
            {
            return EditInfo.GetCaretPositionFromMouseLocation(pt);
        }
    }
    Public ReadOnly Property GetPositionFromCharIndex(ByVal pos As Integer) As Point
        {
            get
            {
            return EditInfo.GetCaretLocation(pos).Location;
        }
    }
    Public ReadOnly Property TextLength As Integer
        {
            get
            {
            return MyBase.Text.Length;
        }
    }
    public void Undo()
    {
        EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Undo);
    }
    public void Redo()
    {
        EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Redo);
    }


    private void OOTextbox_EditStateChanged(object sender, Infragistics.Win.FormattedLinkLabel.EditStateChangedEventArgs e)
    {
        'Debug.WriteLine(Now.ToString & " EditStateChanged " & EditInfo.SelectionStart & ", " & EditInfo.SelectionLength)

        if (bResetPrevious || EditInfo.SelectionLength > 0)
        {
            iPreviousSelectionLength = EditInfo.SelectionLength
            iPreviousSelectionStart = EditInfo.SelectionStart
            bResetPrevious = False
        }
        if (EditInfo.SelectionLength = 0)
        {
            bResetPrevious = True
        }

    }

    private bool bResetPrevious = false;
    private Integer = 0 _iPreviousSelectionLength { get; set; }
    private int iPreviousSelectionLength
        {
            get
            {
            return _iPreviousSelectionLength;
        }
set(Integer)
            if (value > 0)
            {
                value = value
            }
            _iPreviousSelectionLength = value
        }
    }
    private Integer = 0 iPreviousSelectionStart { get; set; }
    private int iHighlightStart = 0;
    private int iHighlightLength = 0;
    private bool bHighlighting = false;
    private bool bAllowDragDrop = false;
    private void OOTextbox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        Debug.WriteLine("Down: SelectionLength: " + SelectionLength + ", SelectionStart: " + SelectionStart);
        bAllowDragDrop = False
        if (e.Button = Windows.Forms.MouseButtons.Left && iPreviousSelectionLength > 0 Then ' SelectionLength > 0)
        {
            private int iMousePosition = EditInfo.GetCaretPositionFromMouseLocation(e.Location);

            if (iMousePosition >= iPreviousSelectionStart && iMousePosition <= iPreviousSelectionStart + iPreviousSelectionLength)
            {

                'If iPreviousSelectionLength > 2000 Then iPreviousSelectionLength = 50 ' Without this we hang on the ApplyStyle line :-(

                private int iCursor = EditInfo.SelectionStart;
                private GenTextbox gtb = ParentGenTextBox();

                iHighlightStart = iPreviousSelectionStart
                iHighlightLength = iPreviousSelectionLength

                bHighlighting = True
                fte.ReadOnly = false;
                fte.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.FormattedText;
                private string sOldValue = Me.Value.ToString;
                fte.Value = Me.Value;
                fte.EditInfo.SelectionStart = iPreviousSelectionStart;
                fte.EditInfo.SelectionLength = iPreviousSelectionLength;

                'fte.BringToFront()
                'fte.Location = Me.Location
                'fte.Size = Me.Size
                'Application.DoEvents()

                fte.EditInfo.ApplyStyle("Background-color:#" + Hex(SystemColors.Highlight.ToArgb) + ";color:#" + Hex(SystemColors.HighlightText.ToArgb) + ";", false);
                'fte.EditInfo.ApplyStyle("Background-color:#FF0000;color:#" & Hex(SystemColors.HighlightText.ToArgb) & ";", False)
                If gtb != null Then gtb.bTextChanging = true
                If Value.ToString <> fte.Value.ToString Then Value = fte.Value
                if (gtb IsNot null)
                {
                    private int iLen = Text.Length;
                    gtb.FormatTextbox();
                    If Text.Length <> iLen Then Value = sOldValue
                    gtb.bTextChanging = false;
                }
                'fte.EditInfo.ApplyStyle("Background-color:#" & Hex(SystemColors.Highlight.ToArgb) & ";color:#" & Hex(SystemColors.HighlightText.ToArgb) & ";", False)
                fte.ReadOnly = true;
                bHighlighting = False
                bAllowDragDrop = True
                iPreviousSelectionStart = 0
                iPreviousSelectionLength = 0
            }
        }

    }

    private GenTextbox ParentGenTextBox()
    {

        private Control gtb = null;

        gtb = Me
        while (gtb.Parent IsNot null)
        {
            gtb = gtb.Parent
            If TypeOf gtb == GenTextbox Then Return (GenTextbox)(gtb)
        }

        return null;

    }


    private void OOTextbox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        Debug.WriteLine("Click: SelectionLength: " + SelectionLength + ", SelectionStart: " + SelectionStart);
    }


    private void OOTextbox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        if (bAllowDragDrop && e.Button = Windows.Forms.MouseButtons.Left && iHighlightLength > 0)
        {
            private int iMousePosition = EditInfo.GetCaretPositionFromMouseLocation(e.Location);
            bAllowDragDrop = False

            if (iMousePosition >= iHighlightStart && iMousePosition <= iHighlightStart + iHighlightLength && Text.Length >= iHighlightStart + iHighlightLength)
            {
                DoDragDrop(Text.Replace(vbCrLf, vbCr).Substring(iHighlightStart, iHighlightLength), DragDropEffects.Move | DragDropEffects.Copy)
            }
        }

    }


    private void OOTextbox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If iHighlightLength > 0 && ! bDragLeave Then ClearHighlight()
    }


    private void ClearHighlight()
    {
        iHighlightStart = 0
        iHighlightLength = 0
        private GenTextbox gtb = ParentGenTextBox();
        If gtb != null Then gtb.bTextChanging = true
        fte.Text = Text;
        if (gtb IsNot null)
        {
            gtb.FormatTextbox();
            gtb.bTextChanging = false;
        }
    }


    ' Ensure fte is visible (so selection etc works), yet not actually visible
    private void OOTextbox_Resize(object sender, System.EventArgs e)
    {
        fte.Location = New Point(Me.Location.X + Me.Size.Width - 3000, Me.Location.Y + Me.Size.Height - 3000);
    }

}

}