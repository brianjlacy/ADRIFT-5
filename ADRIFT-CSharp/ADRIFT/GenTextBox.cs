using Infragistics.Win.UltraWinSpellChecker;
using Infragistics.Win.UltraWinToolbars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;

namespace ADRIFT
{
'Imports System.Data


public class GenTextbox
{

    private bool bLocked;
    Public Shadows Event GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    internal Description oDescription;
    private bool bLoading = true;
    private string sWord;
    private int iWordStart;
    private Infragistics.Win.UltraWinTabControl.UltraTab tabRightClick;
    private string sGraphicsTag;
    private string sAudioTag;
    Private WithEvents tmrToolDropdown As New Timer

    Public Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long

    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    internal List<clsUserFunction.Argument> Arguments { get; set; }


    public void New()
    {
        MyBase.New();
        'DebugTimeRecord("GenTextbox New")
        'This call is required by the Windows Form Designer.
        InitializeComponent();
        'DebugTimeFinish("GenTextbox New")
        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        Me.BackColor = Color.Transparent;
        rtxtSource.Arguments = Arguments;
    }


    private bool bFirstTabHasRestrictions = false;
    public bool FirstTabHasRestrictions
        {
            get
            {
            return bFirstTabHasRestrictions;
        }
set(Boolean)
            bFirstTabHasRestrictions = value
        }
    }


    private string msCommand;
    public String ' Used for restrictions to know about %object% etc sCommand { get; set; }
        {
            get
            {
            return msCommand;
        }
set(ByVal String)
            msCommand = value
            RestrictSummary.sCommand = value;
        }
    }

    ' Hide the Text property
    <Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Text Property not in use", true)>;
    Public Overrides Property Text() As String
        {
            get
            {
            return null;
        }
set(ByVal Value As String)
            Value = Nothing
        }
    }


    internal Description Description { get; set; }
        {
            get
            {
            return oDescription;
        }
set(ByVal Description)
            if (oDescription IsNot value)
            {
                oDescription = value
                if (tabsDescriptions.Tabs.Count > 2)
                {
                    for (int i = tabsDescriptions.Tabs.Count - 2; i <= 1; i += -1)
                    {
                        tabsDescriptions.Tabs.RemoveAt(i);
                    Next;
                }
                if (oDescription IsNot null && tabsDescriptions.Tabs.Count = 2)
                {
                    If oDescription(0).sTabLabel <> "" Then tabsDescriptions.Tabs(0).Text = oDescription(0).sTabLabel
                    for (int iTabs = 1; iTabs <= oDescription.Count - 1; iTabs++)
                    {
                        private string sTabLabel = "Alternative Description " + iTabs;
                        If oDescription(iTabs).sTabLabel <> "" Then sTabLabel = oDescription(iTabs).sTabLabel
                        tabsDescriptions.Tabs.Insert(iTabs, "tab" + iTabs, sTabLabel);
                    Next;
                    if (oDescription.Count > 1)
                    {
                        Me.Visible = false;
                        tabsDescriptions.Visible = true;
                        Tabs.Parent = SplitContainer.Panel2;
                        Me.Visible = true;
                    }
                    If oDescription.Count > 0 Then LoadTab(oDescription(0))
                }
                bLoading = False
            }
        }
    }


    private bool bAllowAlternateDescriptions = true;
    public bool AllowAlternateDescriptions { get; set; }
        {
            get
            {
            return bAllowAlternateDescriptions;
        }
set(ByVal Boolean)
            bAllowAlternateDescriptions = value
        }
    }


    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return Me.rtxtSource.Focused || Me.rtxtPreview.Focused '|| rtxtSource.Parent.Focused;
        }
    }


    private void LocationGroup_GotFocus(object sender, System.EventArgs e)
    {
        RaiseEvent GotFocus(Me, e);
    }

    private void LocationGroup_LostFocus(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then RaiseEvent LostFocus(Me, e)
    }


    'Public Property txtSource() As String
    '    Get
    '        Return Me.rtxtSource.Text
    '    End Get
    '    Set(ByVal Value As String)
    '        Me.rtxtSource.Text = Value
    '        FormatTextbox()
    '    End Set
    'End Property

    'Public Property txtPreview() As String
    '    Get
    '        Return Me.rtxtPreview.Text
    '    End Get
    '    Set(ByVal Value As String)
    '        Me.rtxtPreview.Text = Value
    '    End Set
    'End Property


    'Private PreviewFunctions As New Dictionary(Of String, String)
    private Dictionary<string, string> ReplacedFunctions;

    private void Tabs_SelectedTabChanged(System.Object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {
        switch (e.Tab.Key)
        {
            case "tabSource":
                {
                'UTMText.Visible = False
                'UTMText.Toolbars.Item(0).Visible = False
                'tabpageSource.Enabled = True
                'tabpagePreview.Enabled = False
                'tabpageGraphics.Enabled = False
                'rtxtPreview.Enabled = False
                'rtxtSource.Enabled = True
                'fileGraphics.Enabled = False
            case "tabPreview":
                {
                ' TODO - Only allow this as an option, and if selected, disable the preview box from being edited
                ReplacedFunctions = New Dictionary(Of String, String)
                private string sSourceText = ReplaceALRs(ReplaceFunctions(rtxtSource.Text, Replacements:=ReplacedFunctions));
                'PreviewFunctions = CalculatePreviewFunctions(rtxtSource.Text, sSourceText)

                ' Compare source to the replaced text
                ' Work out a list of all the replaced functions
                ' Then make these parts of the preview immutable, and display with a slightly lighter/grey background
                ' Then when we convert back from preview to source, we simply switch out the functions
                ' If the function evaluates to blank, retain the original function

                Source2HTML(sSourceText, rtxtPreview, true,,, ReplacedFunctions) ' If we do above won't be able to use formatting;
                'UTMText.Visible = True
                UTMText.Toolbars.Item(0).Visible = true;
                'rtxtPreview.Enabled = True
                'rtxtSource.Enabled = False
                'fileGraphics.Enabled = False
                'tabpageSource.Enabled = False
                'tabpagePreview.Enabled = True
                'tabpageGraphics.Enabled = False
            case "tabGraphics":
                {
                'rtxtPreview.Enabled = False
                'rtxtSource.Enabled = False
                'fileGraphics.Enabled = True
                'tabpageSource.Enabled = False
                'tabpagePreview.Enabled = False
                'tabpageGraphics.Enabled = True
        }

    }


    private Dictionary<string, string> CalculatePreviewFunctions(string sSource, string sPreview)
    {

        ' Compare the two strings
        private int iSource = 0 ' position in source;
        private int iPreview = 0 ' position in preview;

        private string sMatch;
        if (sSource(iSource) = sPreview(iPreview))
        {
            sMatch &= sSource(iSource);
        Else
            ' Find where the source and preview match again

        }

    }


    private bool bFormatting = false;



private enum BlockTypeEnum As Integer
    {
        NoFormat = 0
        StandardTag = 1
        [Function] = 2;
        Key = 3
        Variable = 4
        References = 5
        Comments = 6
        EmbeddedExpression = 7
    }



    private void ReplaceIfNotFormatted(string sSearch, string sReplace, Color colour, System.Text.RegularExpressions.RegexOptions regexoptions = System.Text.RegularExpressions.RegexOptions.IgnoreCase, int iFromStart = 0, int iFromEnd = 0)
    {

        private New System.Text.RegularExpressions.Regex(sSearch, regexoptions) re;
        private int iOffset = 0;
        For Each match As System.Text.RegularExpressions.Match In re.Matches(sTextCode)
            if (match.Index + iOffset < sTextCode.Length)
            {
                iMatchNo += 1;
                private int iMatchIndex = match.Index + iOffset;
                private string sActualReplace = sReplace.Replace("$&", match.Value);
                private string sStart = sActualReplace.Substring(0, iFromStart);
                private string sEnd = sActualReplace.Substring(sActualReplace.Length - iFromEnd);
                sActualReplace = sStart & "<font color=""#" & Hex(colour.ToArgb) & """>" & sActualReplace.Substring(iFromStart, sActualReplace.Length - iFromEnd - iFromStart) & "</font>" & sEnd
                dictMatches.Add(iMatchNo, sActualReplace);
                iOffset -= match.Value.Length - 7 - iMatchNo.ToString.Length;
                sTextCode = re.Replace(sTextCode, "@[@#" & iMatchNo & "@]@", 1, iMatchIndex)
            }
        Next;

    }


    'Private fte As Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor
    private Dictionary<int, string> dictMatches;
    private int iMatchNo;
    private string sTextCode;

    internal void FormatTextbox()
    {
        'Exit Sub
        dictMatches = New Dictionary(Of Integer, String)
        iMatchNo = 0

        'If fte Is Nothing Then fte = New Infragistics.Win.FormattedLinkLabel.UltraFormattedTextEditor
        'If rtxtSource.fte.Text <> rtxtSource.Text Then
        'rtxtSource.fte.Text = rtxtSource.Text
        sTextCode = rtxtSource.fte.Value.ToString ' Unformatted version of the encoding

        ' XML comments
        ReplaceIfNotFormatted("&lt;!--.*?--&gt;", "<i>$&</i>", Color.DimGray, System.Text.RegularExpressions.RegexOptions.IgnoreCase) ' | System.Text.RegularExpressions.RegexOptions.Singleline);
        ReplaceIfNotFormatted("&lt;!--.*?(?!--&gt;)$", "<i>$&</i>", Color.DimGray, System.Text.RegularExpressions.RegexOptions.IgnoreCase) ' | System.Text.RegularExpressions.RegexOptions.Singleline);

        ' Embedded expressions
        ReplaceIfNotFormatted("&lt;#.*?#&gt;", "$&", Color.DodgerBlue, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
        ReplaceIfNotFormatted("&lt;#.*?(?!#&gt;)$", "$&", Color.DodgerBlue, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);

        ' Standard Tags
        For Each sTag As String In New String() {"b", "/b", "u", "/u", "i", "/i", "c", "/c", "font", "/font", "br", "center", "/center", "centre", "/centre", "del", "img", "audio"}
            ReplaceIfNotFormatted("&lt;" + sTag + "&gt;", "&lt;<b><font color=""#" + Hex(Color.Purple.ToArgb) + """>" + sTag + "</font></b>&gt;", Color.DarkBlue);
        Next;
        ReplaceIfNotFormatted("&lt;font ", "&lt;<b><font color=""#" + Hex(Color.Purple.ToArgb) + """>font</font></b> ", Color.DarkBlue);
        ReplaceIfNotFormatted("&lt;img ", "&lt;<b><font color=""#" + Hex(Color.Purple.ToArgb) + """>img</font></b> ", Color.DarkBlue);
        ReplaceIfNotFormatted("&lt;audio ", "&lt;<b><font color=""#" + Hex(Color.Purple.ToArgb) + """>audio</font></b> ", Color.DarkBlue);

        ' Functions
        For Each sFunction As String In FunctionNames()
            ReplaceIfNotFormatted("%" + sFunction + "%", "%" + sFunction + "%", Color.Blue);
            ReplaceIfNotFormatted("%" + sFunction + "\[", "%" + sFunction + "[", Color.Blue);
        Next;

        ' Variables
        if (Adventure IsNot null)
        {
            For Each cVar As clsVariable In Adventure.htblVariables.Values
                ReplaceIfNotFormatted("%" + cVar.Name + "(%|\[.*?]%)", "$&", Color.Purple);
            Next;
        }

        ' References
        For Each sRef As String In ReferenceNames()
            ReplaceIfNotFormatted(sRef, "$&", Color.Green);
        Next;

        ' Keys - Only display if they're used within square brackets, or in a function
        if (Adventure IsNot null)
        {
            For Each sKey As String In Adventure.AllKeys
                sKey = sKey.Replace("|", "\|") ' Imported v4 State keys...
                private New System.Text.RegularExpressions.Regex(sKey, System.Text.RegularExpressions.RegexOptions.ExplicitCapture) re;
                private int iOffset = 0;
                For Each match As System.Text.RegularExpressions.Match In re.Matches(sTextCode)
                    private int iMatchIndex = match.Index + iOffset;
                    private string sBefore = "";
                    If iMatchIndex > 0 Then sBefore = sTextCode(iMatchIndex - 1)
                    private string sAfter = "";
                    If iMatchIndex + match.Value.Length < sTextCode.Length Then sAfter = sTextCode(iMatchIndex + match.Value.Length)
                    If sAfter = "." && iMatchIndex + match.Value.Length < sTextCode.Length - 1 Then sAfter &= sTextCode(iMatchIndex + match.Value.Length + 1)
                    if ((sBefore = "[" && sAfter = "]") || (sBefore = ".") || (sAfter.StartsWith(".") && sAfter <> ". " && sAfter <> "." && sAfter <> ".&" && sAfter <> ".<"))
                    {
                        iMatchNo += 1;
                        dictMatches.Add(iMatchNo, "<font color=""#" + Hex(Color.OrangeRed.ToArgb) + """>" + sKey + "</font>");
                        iOffset -= match.Value.Length - 7 - iMatchNo.ToString.Length;
                        sTextCode = re.Replace(sTextCode, "@[@#" & iMatchNo & "@]@", 1, iMatchIndex)
                    }
                Next;
            Next;
        }

        ' OO keywords
        For Each sKey As String In New String() {"Children", "Contents", "Count", "Description", "Descriptor", "Exits", "Held", "Length", "List", "LocationTo", "Location", "Name", "Objects", "Parent", "Position", "ProperName", "Sum", "WornAndHeld", "Worn"}
            ReplaceIfNotFormatted("\." + sKey, "$&", Color.DarkRed, System.Text.RegularExpressions.RegexOptions.ExplicitCapture, 1);
        Next;

        ' Arguments
        if (Arguments IsNot null)
        {
            For Each arg As clsUserFunction.Argument In Arguments
                ReplaceIfNotFormatted("%" + arg.Name + "%", "%" + arg.Name + "%", Color.Teal);
            Next;
        }

        ' End-Functions
        ReplaceIfNotFormatted("\]%", "]%", Color.Blue);

        for (int i = 0; i <= 1 ' Do it twice in case a later replacement has an earlier dictionary key as it's value (e.g. <#<!--x-->#>); i++)
        {
            For Each iMatch As Integer In dictMatches.Keys
                sTextCode = sTextCode.Replace("@[@#" & iMatch & "@]@", dictMatches(iMatch))
            Next;
        Next;

        bTextChanging = True
        if (rtxtSource.Value.ToString <> sTextCode)
        {
            rtxtSource.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.FormattedText;
            rtxtSource.Value = sTextCode;
        Else
            sTextCode = sTextCode
        }

        bTextChanging = False
        'End If

    }



    private string GetNextWord(ref iStart As Integer, ref iOffset As Integer)
    {

        'Dim iStart As Integer = iOffset
        while (iOffset < rtxtSource.TextLength)
        {
            switch (rtxtSource.Text(iOffset))
            {
                case " "c:
                case "%"c:
                case Chr(10):
                    {
                    'iOffset += 1
                    if (iOffset <> iStart)
                    {
                        return rtxtSource.Text.Substring(iStart, iOffset - iStart);
                    Else
                        iStart += 1;
                        iOffset += 1;
                    }
                case "<"c:
                case "["c:
                    {
                    if (iOffset <> iStart)
                    {
                        return rtxtSource.Text.Substring(iStart, iOffset - iStart);
                    Else
                        iOffset += 1;
                    }
                case ">"c:
                case "]"c:
                    {
                    iOffset += 1;
                    return rtxtSource.Text.Substring(iStart, iOffset - iStart);
                    'Case "%"c

                default:
                    {
                    iOffset += 1;
            }
        }

        return rtxtSource.Text.Substring(iStart, iOffset - iStart);

    }


    internal bool bTextChanging = false;
    private void rtxtSource_TextChanged(object sender, System.EventArgs e)
    {
        If bTextChanging Then Exit Sub
        FormatTextbox()
        UpdateDescription();
        'UpdatePreview()
    }


    'Private Sub UpdatePreview()
    '    Source2HTML(rtxtPreview.Text, rtxtPreview, True)
    'End Sub

    'Private Sub cmsIntellisense_ItemClicked(sender As Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)
    '    'rtxtSource.AppendText(e.ClickedItem.Tag.ToString)
    'End Sub



    'Private Sub cmsIntellisense_PreviewKeyDown(sender As Object, e As System.Windows.Forms.PreviewKeyDownEventArgs)
    '    Select Case e.KeyData
    '        Case Keys.Space, Keys.Tab
    '            If rtxtSource.Text.EndsWith(sIntellisenseWord) Then
    '                rtxtSource.Text = sLeft(rtxtSource.Text, rtxtSource.TextLength - sIntellisenseWord.Length)
    '                rtxtSource.SelectionStart = rtxtSource.TextLength
    '                rtxtSource.SelectedText = lstIntellisense.SelectedItems(0).Tag.ToString
    '                rtxtSource.SelectedText = lstIntellisense.Items(0).Tag.ToString
    '            End If
    '            rtxtSource.SelectedText = " "
    '            rtxtSource.SelectionStart = rtxtSource.TextLength
    '            popupList.Close() ' cmsIntellisense.Hide()
    '        Case Keys.Up, Keys.Down, Keys.Enter
    '            ' ignore
    '        Case Else
    '            'rtxtSource.AppendText("x")
    '            Dim cNew As Char = CChar(ChrW(e.KeyValue))
    '            If Not e.Shift Then cNew = Char.ToLower(cNew)
    '            sIntellisenseWord &= cNew
    '            rtxtSource.SelectedText = cNew

    '            'rtxtSource.SelectedText = CChar(ChrW(e.KeyValue))
    '            'cmsIntellisense.BringToFront()
    '            'e.SuppressKeyPress = True
    '            'e.Handled = True
    '            'cmsIntellisense.Show()
    '    End Select

    'End Sub


    private void UpdateDescription()
    {
        if (Not bLoading)
        {
            private int iIndex = tabsDescriptions.SelectedTab.VisibleIndex;
            If Description != null Then Description(iIndex).Description = rtxtSource.Text
            If Description(iIndex).Description.EndsWith(vbCrLf) Then Description(iIndex).Description = sLeft(Description(iIndex).Description, Description(iIndex).Description.Length - 2) ' Fix UltraFormattedTextEditor bug that is adding an extra Carriage Return (urgh!)
            'If fileGraphics.Filename <> "" Then Description(tabsDescriptions.SelectedTab.Index).Description &= "<img src=""" & fileGraphics.Filename & """>"
            'If fileGraphics.Filename <> "" Then Description(iIndex).Description = "<img src=""" & fileGraphics.Filename & """>" & Description(iIndex).Description
            If sGraphicsTag <> "" Then Description(iIndex).Description = sGraphicsTag + Description(iIndex).Description
            If sAudioTag <> "" Then Description(iIndex).Description = sAudioTag + Description(iIndex).Description
            RaiseEvent Changed(Me, New EventArgs);
        }
    }


    Private WithEvents tmrMoveFocus As New Timer

    private void GenTextbox_Enter(object sender, System.EventArgs e)
    {
        tmrMoveFocus.Interval = 1;
        tmrMoveFocus.Start();
    }


    private void tmrMoveFocus_Tick(object sender, System.EventArgs e)
    {
        tmrMoveFocus.Enabled = false;
        ' Focus on textbox, not tab on enter
        if (Tabs.SelectedTab IsNot null)
        {
            switch (Tabs.SelectedTab.Key)
            {
                case "tabSource":
                    {
                    rtxtSource.Focus();
                case "tabPreview":
                    {
                    rtxtPreview.Focus();
                case "tabGraphics":
                    {
                    fileGraphics.Focus();
            }
        }
    }


    private void GenTextbox_GotFocus(object sender, System.EventArgs e)
    {
        DisallowOKButton();
    }

    private void AllowOKButton()
    {

        private Control ctlParent = Me.Parent;
        while (Not TypeOf ctlParent Is Form)
        {
            ctlParent = ctlParent.Parent
            If ctlParent == null Then Exit Sub
        }

        With (Form)(ctlParent);
            For Each ctlButton As Control In .Controls
                if (TypeOf ctlButton Is Infragistics.Win.Misc.UltraButton)
                {
                    if (CType(ctlButton, Infragistics.Win.Misc.UltraButton).Text = "OK")
                    {
                        .AcceptButton = (Infragistics.Win.Misc.UltraButton)(ctlButton);
                    }
                }
            Next;
        }

    }

    private void DisallowOKButton()
    {

        private Control ctlParent = Me.Parent;
        while (Not TypeOf ctlParent Is Form)
        {
            ctlParent = ctlParent.Parent
            If ctlParent == null Then Exit Sub
        }

        With (Form)(ctlParent);
            .AcceptButton = null;
        }

    }


    private void InitSpellCheck()
    {

        try
        {
            Me.UltraSpellChecker1 = New Infragistics.Win.UltraWinSpellChecker.UltraSpellChecker(Me.components);
            (System.ComponentModel.ISupportInitialize)(Me.UltraSpellChecker1).BeginInit();
            'Me.UltraSpellChecker1.SetSpellCheckerSettings(Me.rtxtSource, New Infragistics.Win.UltraWinSpellChecker.SpellCheckerSettings(True))
            rtxtSource.SpellChecker = Me.UltraSpellChecker1;


            With UltraSpellChecker1;
                .ContainingControl = Me;
                .Mode = Infragistics.Win.UltraWinSpellChecker.SpellCheckingMode.AsYouType;
                .SpellOptions.AllowCapitalizedWords = true;
                .SpellOptions.ConsiderationRange = 15;
                .UnderlineSpellingErrorColor = System.Drawing.Color.Red;
                .UnderlineSpellingErrorStyle = Infragistics.Win.UltraWinSpellChecker.UnderlineErrorsStyle.Squiggle;
                .UseAppStyling = true;
                (System.ComponentModel.ISupportInitialize)(Me.UltraSpellChecker1).EndInit();

                try
                {
                    .UserDictionary = sUserDictionary;
                }
                catch (UnauthorizedAccessException ex)
                {
                    ErrMsg("Error accessing user dictionary at " + sUserDictionary + vbCrLf + vbCrLf + "Please go to Settings and set the dictionary to a path you have write permissions for.");
                }
                if (sMainDictionary <> "")
                {
                    try
                    {
                        .Dictionary = sMainDictionary;
                    }
                    catch (Exception exDict)
                    {
                        ErrMsg("Error assigning main dictionary """ + sMainDictionary + """", exDict);
                        .Dictionary = "";
                    }
                }
                switch (sDictionary)
                {
                    case "English":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.English;
                        '.Dictionary = ""
                    case "Dutch":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.Dutch;
                    case "French":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.French;
                    case "German":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.German;
                    case "Italian":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.Italian;
                    case "Portugese":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.Portuguese;
                    case "Spanish":
                        {
                        .SpellOptions.LanguageParser = Infragistics.Win.UltraWinSpellChecker.LanguageType.Spanish;
                }
                try
                {
                    For Each sFunction As String In FunctionNames()
                        .IgnoreAll(sFunction);
                    Next;
                    For Each sReference As String In ReferenceNames()
                        .IgnoreAll(sReference.Replace("%", ""));
                    Next;
                    For Each sKey As String In Adventure.AllKeys()
                        If ! sKey.Contains("|") && ! sKey.Contains("_") && ! sKey.Contains(" ") Then .IgnoreAll(sKey)
                    Next;
                    For Each oVar As clsVariable In Adventure.htblVariables.Values
                        If ! oVar.Name.Contains("_") && ! oVar.Name.Contains(" ") && ! oVar.Name.Contains("/") && ! oVar.Name.Contains("&") && ! oVar.Name.Contains("!") && ! oVar.Name.Contains("#") && ! oVar.Name.Contains("?") && ! oVar.Name.Contains("(") && ! oVar.Name.Contains(")") Then .IgnoreAll(oVar.Name)
                    Next;
                Catch
                    ' In case it's not a 'proper' word.
                }
            }
        }
        catch (System.IO.DirectoryNotFoundException exDNF)
        {
            ErrMsg(exDNF.Message);
        }
        catch (Exception ex)
        {
            ErrMsg("Error Initialising Spell Check", ex);
        }

    }


    private void GenTextbox_Load(object sender, System.EventArgs e)
    {

        If DesignMode Then Exit Sub

        try
        {
            'DebugTimeRecord("GenTextbox Load")
            if (Description IsNot null && Description.Count < 2)
            {
                if (Not FirstTabHasRestrictions)
                {
                    Tabs.Parent = Me;
                Else
                    SplitContainer.Parent = Me;
                    Tabs.Parent = SplitContainer.Panel2;
                }
                tabsDescriptions.Visible = false;
            }

            imgGraphics.AllowDrop = true;
            If ! bEnableGraphics Then Tabs.Tabs("tabGraphics").Visible = false
            If ! bEnableAudio Then Tabs.Tabs("tabAudio").Visible = false
            If ! bEnablePreview Then Tabs.Tabs("tabPreview").Visible = false

            if (Not (bEnableGraphics Or bEnableAudio Or bEnablePreview))
            {
                ' Don't display the right-hand tabs at all!
                ' Stick it on a TextEditor so we get a nice border
                'Dim pnl As New Infragistics.Win.UltraWinEditors.UltraComboEditor ' .UltraTextEditor
                'pnl.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList
                'pnl.Parent = Tabs.Parent
                'pnl.AutoSize = False
                ''pnl.Multiline = True
                'pnl.Dock = DockStyle.Fill
                'pnl.ReadOnly = True
                'pnl.BringToFront()

                Tabs.Visible = false;
                tabsDescriptions.Parent = Me;
                rtxtSource.Dock = DockStyle.Fill ' DockStyle.None;
                'rtxtSource.Location = New Point(0, 0)
                'rtxtSource.Size = New Size(pnl.Width, pnl.Height)
                'rtxtSource.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right
                rtxtSource.Parent = Tabs.Parent ' pnl;
                rtxtSource.fte.Parent = Tabs.Parent ' Me.ParentForm ' pnl;
                rtxtSource.fte.BringToFront() 'SendToBack();
                'pnl.UseOsThemes = Infragistics.Win.DefaultableBoolean.False
                'pnl.BorderStyle = Infragistics.Win.UIElementBorderStyle.None
            Else
                rtxtSource.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                rtxtSource.Size = New Size(tabpageSource.Width + 2, tabpageSource.Height + 2);
            }
            'rtxtSource.AutoWordSelection = False

            'DebugTimeFinish("GenTextbox Load")
        }
        catch (Exception ex)
        {
            ErrMsg("Error loading GenTextbox", ex);
        }

    }

    private void GenTextbox_LostFocus(object sender, System.EventArgs e)
    {
        AllowOKButton();
    }

    private void GenTextbox_Resize(object sender, System.EventArgs e)
    {

        private int iTotalWidth = 0;
        private int iTabs = 1;

        If bEnableAudio Then iTabs += 1
        If bEnableGraphics Then iTabs += 1
        If bEnablePreview Then iTabs += 1

        if (Height > (45 * iTabs))
        {
            Tabs.Tabs("tabSource").Text = "Source";
            Tabs.Tabs("tabPreview").Text = "Preview";
            Tabs.Tabs("tabGraphics").Text = "Graphics";
            Tabs.Tabs("tabAudio").Text = "Audio";
        ElseIf Height > (25 * iTabs) Then
            Tabs.Tabs("tabSource").Text = "Src";
            Tabs.Tabs("tabPreview").Text = "Pvw";
            Tabs.Tabs("tabGraphics").Text = "Gfx";
            Tabs.Tabs("tabAudio").Text = "Aud";
        Else
            Tabs.Tabs("tabSource").Text = "S";
            Tabs.Tabs("tabPreview").Text = "P";
            Tabs.Tabs("tabGraphics").Text = "G";
            Tabs.Tabs("tabAudio").Text = "A";
        }

        'If Height < 120 Then
        '    pnlButtons.Location = New Point(50, 19)
        '    pnlChannel.Location = New Point(pnlButtons.Width + 70, 21)
        '    chkLoop.Location = New Point(pnlButtons.Width + 72, 46)
        'Else
        '    pnlButtons.Location = New Point(111, 2)
        '    pnlChannel.Location = New Point(134, 58)
        '    chkLoop.Location = New Point(247, 61)
        'End If

        if (Height > 220)
        {
            chkPlay.Size = New Size(192, 50);
            chkPlay.Location = New Point(1, 1);
            chkPlay.Appearance.ImageHAlign = Infragistics.Win.HAlign.Left;
            chkPlay.Text = "Play Audio";
            chkPause.Size = New Size(192, 50);
            chkPause.Location = New Point(1, 52);
            chkPause.Appearance.ImageHAlign = Infragistics.Win.HAlign.Left;
            chkPause.Text = "Pause Audio";
            chkStop.Size = New Size(192, 50);
            chkStop.Location = New Point(1, 103);
            chkStop.Appearance.ImageHAlign = Infragistics.Win.HAlign.Left;
            chkStop.Text = "Stop Audio";
            pnlButtons.Size = New Size(194, 154);
            pnlButtons.Location = New Point(CInt((pnlAudio.Width - pnlButtons.Width) / 2), CInt((pnlAudio.Height - pnlButtons.Height) / 2) - 20);
            pnlChannel.Location = New Point(pnlButtons.Location.X, pnlButtons.Location.Y + pnlButtons.Height + 10);
            chkLoop.Location = New Point(pnlChannel.Location.X + pnlChannel.Width + 40, pnlChannel.Location.Y + 3);
        Else
            chkPlay.Size = New Size(64, 50);
            chkPlay.Location = New Point(1, 1);
            chkPlay.Appearance.ImageHAlign = Infragistics.Win.HAlign.Center;
            chkPlay.Text = "";
            chkPause.Size = New Size(64, 50);
            chkPause.Location = New Point(65, 1);
            chkPause.Appearance.ImageHAlign = Infragistics.Win.HAlign.Center;
            chkPause.Text = "";
            chkStop.Size = New Size(64, 50);
            chkStop.Location = New Point(129, 1);
            chkStop.Appearance.ImageHAlign = Infragistics.Win.HAlign.Center;
            chkStop.Text = "";
            pnlButtons.Size = New Size(193, 52);
            pnlButtons.Location = New Point(CInt((pnlAudio.Width - pnlButtons.Width) / 2), CInt((pnlAudio.Height - pnlButtons.Height) / 2) - 20);
            pnlChannel.Location = New Point(pnlButtons.Location.X, pnlButtons.Location.Y + pnlButtons.Height + 10);
            chkLoop.Location = New Point(pnlChannel.Location.X + pnlChannel.Width + 40, pnlChannel.Location.Y + 3);
        }

        if (iTabs > 1)
        {
            rtxtSource.Size = New Size(tabpageSource.Width + 2, tabpageSource.Height + 2) '  New Size(rtxtPreview.Width + 2, rtxtPreview.Height + 2) 'rtxtPreview.Parent.Parent.Size.Width + 2, rtxtPreview.Parent.Parent.Size.Height + 2);
        }

    }


    ' Fudge, because this event is firing before the text selection change registers
    private bool bPoppingUp = false;
    private void UTMText_BeforeToolDropdown(object sender, Infragistics.Win.UltraWinToolbars.BeforeToolDropdownEventArgs e)
    {
        if (Not bPoppingUp)
        {
            e.Cancel = true;
            bPoppingUp = True
            tmrToolDropdown.Interval = 1;
            tmrToolDropdown.Start();
        Else
            bPoppingUp = False
            DropDownTool();
        }
    }


    private void tmrToolDropdown_Tick(object sender, System.EventArgs e)
    {
        tmrToolDropdown.Stop();
        UTMText.ShowPopup("mnuPopup");
    }


    private void DropDownTool()
    {
        try
        {
            'Application.DoEvents() ' Allow the cursor to finish moving first
            Debug.WriteLine(Now.ToString + " UTMText_BeforeToolDropdown");

            ' Check the current word
            private int x1 = rtxtSource.SelectionStart;
            Debug.Write("X1: " + x1);
            while (x1 > 0 && System.Text.RegularExpressions.Regex.IsMatch(rtxtSource.Text(x1 - 1), "[A-Za-z]") ' && rtxtSource.Text(x1 - 1) <> " " && rtxtSource.Text(x1 - 1) <> "." && rtxtSource.Text(x1 - 1) <> "," && rtxtSource.Text(x1 - 1) <> vbLf)
            {
                x1 -= 1;
            }
            private int x2 = rtxtSource.SelectionStart;
            Debug.WriteLine(", X2: " + x2);
            while (x2 <= rtxtSource.TextLength - 1 && System.Text.RegularExpressions.Regex.IsMatch(rtxtSource.Text(x2), "[A-Za-z]") ' && rtxtSource.Text(x2) <> " " && rtxtSource.Text(x2) <> "." && rtxtSource.Text(x2) <> "," && rtxtSource.Text(x2) <> vbCr)
            {
                x2 += 1;
            }
            x2 -= 1;
            private string sSelectedWord = rtxtSource.Text.Substring(x1, x2 - x1 + 1);
            Debug.WriteLine("Selected word: #" + sSelectedWord + "# (" + x1 + ", " + x2 - x1 + 1 + ")");
            iWordStart = x1

            (PopupMenuTool)(UTMText.Tools("mnuPopup")).Tools.Clear();

            if (sSelectedWord <> "")
            {
                If UltraSpellChecker1 == null Then InitSpellCheck()
                private WordCheckResult result = UltraSpellChecker1.CheckWord(sSelectedWord);
                if (Not result.SpelledCorrectly)
                {
                    sWord = sSelectedWord
                    private int iIndex = 0;
                    For Each sSuggestion As String In result.SpellingError.Suggestions
                        Debug.WriteLine(sSuggestion);
                        private ButtonTool menu = AddToMenu("mnuPopup", "Suggestion_" + sSuggestion, sSuggestion, true, iIndex);
                        iIndex += 1;
                    Next;
                    AddToMenu("mnuPopup", "Ignore", "I&gnore").InstanceProps.IsFirstInGroup = true;
                    AddToMenu("mnuPopup", "IgnoreAll", "&Ignore All");
                    AddToMenu("mnuPopup", "AddToDictionary", "&Add to Dictionary");

                    AddToMenu("mnuPopup", "Language", "&Language").InstanceProps.IsFirstInGroup = true;
                    With AddToMenu("mnuPopup", "CheckSpelling", "&Spelling...").InstanceProps;
                        .AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgSpelling;
                    }

                    With AddToMenu("mnuPopup", "Cut", "Cu&t");
                        .InstanceProps.IsFirstInGroup = true;
                        .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgCut;
                        .SharedProps.Enabled = (rtxtSource.SelectionLength > 0);
                    }
                    With AddToMenu("mnuPopup", "Copy", "&Copy");
                        .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgCopy;
                        .SharedProps.Enabled = (rtxtSource.SelectionLength > 0);
                    }
                    With AddToMenu("mnuPopup", "Paste", "&Paste");
                        .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgPaste;
                        .SharedProps.Enabled = Clipboard.ContainsText;
                    }

                    Exit Sub;
                }
            }

            sWord = ""
            With AddToMenu("mnuPopup", "Cut", "Cu&t");
                .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgCut;
                .SharedProps.Enabled = (rtxtSource.SelectionLength > 0);
            }
            With AddToMenu("mnuPopup", "Copy", "&Copy");
                .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgCopy;
                .SharedProps.Enabled = (rtxtSource.SelectionLength > 0);
            }
            With AddToMenu("mnuPopup", "Paste", "&Paste");
                .InstanceProps.AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgPaste;
                .SharedProps.Enabled = Clipboard.ContainsText;
            }

            if (bAllowAlternateDescriptions)
            {
                With AddToMenu("mnuPopup", "AddAlternateDescription", "Add Alternative Description").InstanceProps;
                    .IsFirstInGroup = true;
                    .AppearancesSmall.Appearance.Image = Global.ADRIFT.My.Resources.Resources.imgNew16;
                }
            }

            With ("ShowOnce", "Only Display &Once", , , ButtonType.StateButtonTool)(AddToMenu("mnuPopup"), Infragistics.Win.UltraWinToolbars.StateButtonTool);
                .Checked = Description(tabsDescriptions.SelectedTab.VisibleIndex).DisplayOnce;
            }
            With ("ReturnToDefault", "&Return to Default", , , ButtonType.StateButtonTool)(AddToMenu("mnuPopup"), Infragistics.Win.UltraWinToolbars.StateButtonTool);
                .SharedProps.Enabled = Description(tabsDescriptions.SelectedTab.VisibleIndex).DisplayOnce;
                .Checked = Description(tabsDescriptions.SelectedTab.VisibleIndex).ReturnToDefault;
                If ! .SharedProps.Enabled Then .Checked = false
            }

        }
        catch (Exception ex)
        {
            ErrMsg("BeforeToolDropdown error", ex);
        }

    }


    private void UTMText_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
        DoToolClick(e.Tool.Key, e.Tool.SharedProps.Caption, CStr(e.Tool.SharedProps.Tag))
    }


    private void DoToolClick(string sKey, string sCaption, string sTag)
    {

        fGenerator.sDestinationList = "";

        switch (sKey)
        {
            case "AddAlternateDescription":
                {
                AddTab();
            case "AddToDictionary":
                {
                If UltraSpellChecker1 == null Then InitSpellCheck()
                UltraSpellChecker1.AddWordToUserDictionary(sWord);
            case "CheckSpelling":
                {
                If UltraSpellChecker1 == null Then InitSpellCheck()
                UltraSpellChecker1.ShowSpellCheck(Me.rtxtSource);
            case "Copy":
                {
                'Try
                '    Clipboard.SetText(rtxtSource.SelectedText)
                'Catch
                'End Try
                rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Copy);
            case "Cut":
                {
                'Try
                '    Clipboard.SetText(rtxtSource.SelectedText)
                '    rtxtSource.SelectedText = ""
                'Catch
                'End Try
                rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Cut);
            case "Ignore":
                {
                If UltraSpellChecker1 == null Then InitSpellCheck()
                UltraSpellChecker1.IgnoreError(Me.rtxtSource, UltraSpellChecker1.GetErrorAtPoint(Me.rtxtSource, rtxtSource.GetPositionFromCharIndex(rtxtSource.EditInfo.SelectionStart)));
            case "IgnoreAll":
                {
                If UltraSpellChecker1 == null Then InitSpellCheck()
                UltraSpellChecker1.IgnoreAll(sWord);
            case "Paste":
                {
                'rtxtSource.SelectedText = Clipboard.GetText
                rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Paste);
            case "ShowOnce":
                {
                Description(tabsDescriptions.SelectedTab.VisibleIndex).DisplayOnce = (StateButtonTool)(UTMText.Tools("ShowOnce")).Checked;
            case "ReturnToDefault":
                {
                Description(tabsDescriptions.SelectedTab.VisibleIndex).ReturnToDefault = (StateButtonTool)(UTMText.Tools("ReturnToDefault")).Checked;
            default:
                {
                if (sKey.StartsWith("Suggestion_"))
                {
                    private int iPos = rtxtSource.SelectionStart;
                    rtxtSource.Text = sLeft(rtxtSource.Text, iWordStart) + sKey.Replace("Suggestion_", "") + sRight(rtxtSource.Text, rtxtSource.TextLength - iWordStart - sWord.Length);
                    rtxtSource.SelectionStart = iPos;
                }
        }

    }



    private void tabsDescriptions_SelectedTabChanged(System.Object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {

        if (e.Tab IsNot null)
        {
            If e.Tab.Key = "tabNew" Then AddTab()

            if (e.Tab.Key = "tabDefault" && Not FirstTabHasRestrictions)
            {
                SplitContainer.Panel1Collapsed = true;
            Else
                SplitContainer.Panel1Collapsed = false;
            }
            If Description != null Then LoadTab(Description(tabsDescriptions.SelectedTab.VisibleIndex))
        }

    }



    private void AddTab()
    {

        if (Not tabsDescriptions.Visible)
        {
            SplitContainer.Parent = tabsDescriptions.SharedControlsPage;
            if (Tabs.Visible)
            {
                Tabs.BeginUpdate();
                Tabs.Parent = SplitContainer.Panel2;
                Tabs.Dock = DockStyle.None;
                Tabs.Location = New Point(-1, -1);
                Tabs.Size = New Size(SplitContainer.Panel2.Width + 2, SplitContainer.Panel2.Height + 2);
                Tabs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                Tabs.EndUpdate();
            Else
                private OOTextbox pnl = rtxtSource ' Infragistics.Win.UltraWinEditors.UltraComboEditor = CType(rtxtSource.Parent, Infragistics.Win.UltraWinEditors.UltraComboEditor);
                pnl.Parent = SplitContainer.Panel2;
                pnl.Dock = DockStyle.None;
                pnl.Location = New Point(-1, -1);
                pnl.Size = New Size(SplitContainer.Panel2.Width + 2, SplitContainer.Panel2.Height + 2);
                pnl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            }
            tabsDescriptions.Visible = true;
        }

        private int iTabs = tabsDescriptions.Tabs.Count - 1;
        private New SingleDescription[] sd;
        Description.Add(sd);
        private string sTabName = "Alternative Description " + iTabs;
        while (tabsDescriptions.Tabs.Exists("tab" + iTabs))
        {
            iTabs += 1;
            sTabName = "Alternative Description " & iTabs
        }
        bAdding = True
        private Infragistics.Win.UltraWinTabControl.UltraTab tabNew = tabsDescriptions.Tabs.Insert(tabsDescriptions.Tabs.Count - 1, "tab" + iTabs, sTabName);
        tabNew.VisibleIndex = tabsDescriptions.Tabs.Count - 2;
        bAdding = False
        tabsDescriptions.SelectedTab = tabsDescriptions.Tabs("tab" + iTabs);
        fileGraphics.Filename = "";
        LoadTab();

    }


    private bool StartsWithIgnoringTags(string sDescription, string sMatch)
    {

        If sDescription.StartsWith(sMatch) Then Return true

        ' Ok, get rid of any tags at the start
        while (sDescription.StartsWith("<"))
        {
            if (sDescription.Contains(">"))
            {
                sDescription = sDescription.Substring(sDescription.IndexOf(">") + 1)
                return StartsWithIgnoringTags(sDescription, sMatch);
            Else
                return false;
            }
        }

        return false;

    }

    private void LoadGraphics(ref sDescription As String)
    {

        private New System.Text.RegularExpressions.Regex(IMGREGEX, System.Text.RegularExpressions.RegexOptions.IgnoreCase) re;
        if (re.IsMatch(sDescription))
        {
            private string sMatch = re.Match(sDescription).ToString;
            If re.Matches(sDescription).Count = 1 && sDescription.StartsWith(sMatch) Then ' Can't ignore tags here as we want <cls> and <centre> to be allowed before initial image
                private string sLocation = sMid(sMatch, 10, sMatch.Length - 10);
                If sLocation.StartsWith("""") Then sLocation = sLocation.Substring(1)
                If sLocation.EndsWith("""") Then sLocation = sLocation.Substring(0, sLocation.Length - 1)
                Me.fileGraphics.Filename = sLocation;
                sDescription = sDescription.Replace(sMatch, "")
            Else
                Tabs.Tabs("tabGraphics").Visible = false;
            }
        Else
            Me.fileGraphics.Filename = "";
        }

    }

    private void LoadAudio(ref sDescription As String)
    {

        private New System.Text.RegularExpressions.Regex(AUDREGEX, System.Text.RegularExpressions.RegexOptions.IgnoreCase) re;
        if (re.IsMatch(sDescription))
        {
            private System.Text.RegularExpressions.Match m = re.Match(sDescription);
            private string sMatch = m.ToString;
            if (re.Matches(sDescription).Count = 1 && StartsWithIgnoringTags(sDescription, sMatch) Then ' sDescription.StartsWith(sMatch))
            {
                if (m.Groups("src").Length > 0)
                {
                    private string sLocation = m.Groups("src").Value ' sMid(sMatch, 10, sMatch.Length - 10);
                    If sLocation.StartsWith("""") Then sLocation = sLocation.Substring(1)
                    If sLocation.EndsWith("""") Then sLocation = sLocation.Substring(0, sLocation.Length - 1)
                    Me.fileAudio.Filename = sLocation;
                }
                if (m.Groups("channel").Length > 0)
                {
                    nudChannel.Value = SafeInt(m.Groups("channel").Value);
                }
                if (m.Groups("loop").Length > 0)
                {
                    chkLoop.Checked = (m.Groups("loop").Value.ToUpper = "Y");
                }
                if (sMatch.Contains(" pause"))
                {
                    chkPause.Checked = true;
                ElseIf sMatch.Contains(" stop") Then
                    chkStop.Checked = true;
                Else
                    chkPlay.Checked = true;
                }
                sDescription = sDescription.Replace(sMatch, "")
            Else
                Tabs.Tabs("tabAudio").Visible = false;
            }
        Else
            Me.fileAudio.Filename = "";
        }

    }


    private void LoadTab(SingleDescription sd = null)
    {

        bLoading = True
        If sd == null Then sd = New SingleDescription

        'SplitContainer.Panel1Collapsed = False
        RestrictSummary.LoadRestrictions(sd.Restrictions);
        RestrictSummary.sCommand = Me.sCommand;
        RestrictSummary.Arguments = Me.Arguments;
        SetMetLabel();

        private string sDescription = sd.Description;

        private int iGraphics = sDescription.IndexOf("<img ");
        private int iAudio = sDescription.IndexOf("<audio ");

        if (iGraphics < iAudio)
        {
            LoadGraphics(sDescription);
            LoadAudio(sDescription);
        Else
            LoadAudio(sDescription);
            LoadGraphics(sDescription);
        }

        rtxtSource.Text = sDescription;
        If sDescription.EndsWith(vbCrLf) Then rtxtSource.Text &= vbCrLf ' Add the extra vbCrLf to fix the UltraFormattedTextEditor bug (urgh!)
        SetCombo(cmbDisplayWhen, sd.eDisplayWhen);
        bLoading = False

        'Audio_Changed(Nothing, Nothing) ' So we save our sAudioTag
        'fileGraphics_TextChanged(Nothing, Nothing)

    }


    private void RestrictSummary_RestrictionsChanged()
    {
        Description(tabsDescriptions.SelectedTab.VisibleIndex).Restrictions = RestrictSummary.arlRestrictions;
        SetMetLabel();
        RaiseEvent Changed(Me, New EventArgs);
    }


    private void SetMetLabel()
    {
        if (RestrictSummary.arlRestrictions.Count <= 1)
        {
            lblAreAllMetThen.Text = "   is met then";
        Else
            lblAreAllMetThen.Text = "are all met then";
        }
    }


    private void fileGraphics_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {

        private string sFilename = String.Empty;
        if (GetFilename(sFilename, e))
        {
            fileGraphics.Filename = sFilename;
        }

    }


    private void fileGraphics_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {

        private string sFilename = String.Empty;
        if (GetFilename(sFilename, e))
        {
            e.Effect = DragDropEffects.Copy;
        Else
            e.Effect = DragDropEffects.None;
        }

    }


    private bool GetFilename(ref filename As String, DragEventArgs e)
    {

        try
        {
            private bool ret = false;
            filename = String.Empty

            if (((e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy))
            {
                private object oData = e.Data.GetData("FileName");
                if (oData IsNot null)
                {
                    If TypeOf oData == String() Then filename = (String()(oData))(0)
                Else
                    filename = CStr((e.Data).GetData("System.String")) ' FileName into Array...?
                }

                if (filename IsNot null)
                {
                    private string ext = IO.Path.GetExtension(filename).ToLower;
                    switch (ext)
                    {
                        case ".jpg":
                        case ".png":
                        case ".bmp":
                        case ".gif":
                            {
                            return true;
                        default:
                            {
                            return false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }

    }



private enum ButtonType
    {
        ButtonTool;
        StateButtonTool;
    }
    private ButtonTool AddToMenu(string sMenuKey, string sKey, string sDescription, bool bBold = false, int iIndex = -1, ButtonType eType = ButtonType.ButtonTool)
    {

        if (Not UTMText.Tools.Exists(sKey))
        {
            private ButtonTool newTool;
            switch (eType)
            {
                case ButtonType.StateButtonTool:
                    {
                    newTool = New StateButtonTool(sKey)
                default:
                    {
                    newTool = New ButtonTool(sKey)
            }
            newTool.SharedProps.Caption = sDescription;
            If bBold Then newTool.SharedProps.AppearancesSmall.Appearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.true
            UTMText.Tools.Add(newTool);
        }

        private ButtonTool newInstance = null;

        if (iIndex > -1)
        {
            newInstance = CType(CType(UTMText.Tools(sMenuKey), PopupMenuTool).Tools.InsertTool(iIndex, sKey), ButtonTool)
        Else
            newInstance = CType(CType(UTMText.Tools(sMenuKey), PopupMenuTool).Tools.AddTool(sKey), ButtonTool)
        }

        return newInstance;

    }


    'Private iSelStartCache As Integer = -1
    private void rtxtSource_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {

        'If e.Button = Windows.Forms.MouseButtons.Right AndAlso rtxtSource.SelectionLength = 0 Then
        '    rtxtSource.SelectionStart = rtxtSource.GetCharIndexFromPosition(rtxtSource.PointToClient(MousePosition))
        '    'If rtxtSource.SelectionStart = rtxtSource.TextLength - 1 Then rtxtSource.SelectionStart = rtxtSource.TextLength ' Dunno why it's doing this...
        '    Dim ptMouse As Point = rtxtSource.PointToClient(MousePosition)
        '    Dim ptLastChar As Point = rtxtSource.GetPositionFromCharIndex(rtxtSource.TextLength)
        '    If ptMouse.Y > ptLastChar.Y + 16 OrElse (ptMouse.X > ptLastChar.X AndAlso ptMouse.Y > ptLastChar.Y) Then
        '        rtxtSource.SelectionStart = rtxtSource.TextLength
        '    End If
        'End If

        'If Not Control.ModifierKeys = Keys.Shift Then
        '    iSelStartCache = rtxtSource.GetCharIndexFromPosition(rtxtSource.PointToClient(MousePosition))
        '    If rtxtSource.SelectionStart > iSelStartCache Then
        '        iSelStartCache = rtxtSource.SelectionStart
        '    End If
        'End If

        'If Control.ModifierKeys = Keys.Shift Then
        '    FixSelections()
        'End If
        'rtxtSource.AutoWordSelection = False
    }


    'Private Sub rtxtSource_SelectionChanged(sender As Object, e As System.EventArgs) Handles rtxtSource.SelectionChanged
    '    'If Not Control.ModifierKeys = Keys.Shift AndAlso Not MouseButtons = Windows.Forms.MouseButtons.Left Then iSelStartCache = rtxtSource.SelectionStart
    'End Sub


    'Private Sub rtxtSource_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles rtxtSource.MouseMove

    '    If MouseButtons = Windows.Forms.MouseButtons.Left AndAlso iSelStartCache <> -1 Then
    '        FixSelections()
    '    End If

    'End Sub


    '' The RichTextBox doesn't select things properly on certain characters, so we have to overrride it... :(
    'Private Sub FixSelections()
    '    Exit Sub
    '    Dim ptMouse As Point = rtxtSource.PointToClient(MousePosition)
    '    Dim iNewIndex As Integer = rtxtSource.GetCharIndexFromPosition(ptMouse) ' + 1

    '    ' Select right to the end if we're definitely past the end of the text
    '    Dim ptLastChar As Point = rtxtSource.GetPositionFromCharIndex(rtxtSource.TextLength)
    '    If ptMouse.Y > ptLastChar.Y + 16 OrElse (ptMouse.X > ptLastChar.X AndAlso ptMouse.Y > ptLastChar.Y) Then
    '        iNewIndex = rtxtSource.TextLength '+ 1
    '    End If

    '    rtxtSource.SuspendLayout()
    '    If iNewIndex > iSelStartCache Then
    '        If rtxtSource.SelectionStart <> iSelStartCache Then rtxtSource.SelectionStart = iSelStartCache
    '        If rtxtSource.SelectionLength <> iNewIndex - iSelStartCache Then rtxtSource.SelectionLength = iNewIndex - iSelStartCache
    '    ElseIf iNewIndex < iSelStartCache Then
    '        If rtxtSource.SelectionStart <> iNewIndex Then rtxtSource.SelectionStart = iNewIndex
    '        If rtxtSource.SelectionLength <> iSelStartCache - iNewIndex Then rtxtSource.SelectionLength = iSelStartCache - iNewIndex
    '    End If
    '    rtxtSource.ResumeLayout()
    '    'rtxtSource.Visible = True

    'End Sub


    'Private bLastPressedDot As Boolean = False
    private void rtxtSource_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        If UltraSpellChecker1 == null Then InitSpellCheck()

        'bLastPressedDot = False
        switch (e.KeyData)
        {
            case Keys.Control Or Keys.T:
                {
                AddTab();
                'Case Keys.OemPeriod
                '    bLastPressedDot = True
        }

    }


    private void Audio_Changed(object sender, System.EventArgs e)
    {
        try
        {
            If Description == null Then Exit Sub ' || bLoading  ' this stops audio being set on load

            private int iIndex = 0;
            If tabsDescriptions.SelectedTab != null Then iIndex = tabsDescriptions.SelectedTab.VisibleIndex

            private string sType = "";
            if (chkPlay.Checked)
            {
                sType = " play"
            ElseIf chkPause.Checked Then
                sType = " pause"
            ElseIf chkStop.Checked Then
                sType = " stop"
            }

            ' Trim off any existing <audio> tag
            private New System.Text.RegularExpressions.Regex(AUDREGEX, System.Text.RegularExpressions.RegexOptions.IgnoreCase) re;

            if (re.IsMatch(Description(iIndex).Description))
            {
                if (re.Matches(Description(iIndex).Description).Count = 1)
                {
                    private string sMatch = re.Match(Description(iIndex).Description).ToString;
                    'If Description(iIndex).Description.StartsWith(sMatch) Then
                    Description(iIndex).Description = Description(iIndex).Description.Replace(sMatch, "");
                    'End If
                }
            }

            sAudioTag = ""
            if (sType <> "")
            {
                private string sSrc = "";

                if (sType = " play" && fileAudio.Filename <> "")
                {
                    if (IO.File.Exists(fileAudio.Filename) || fileAudio.Filename.ToLower.StartsWith("http"))
                    {
                        fileAudio.txtDir.ForeColor = SystemColors.ControlText;

                        'Description(iIndex).Description = "<audio src=""" & fileAudio.Filename & """>" & Description(iIndex).Description
                        sSrc = " src=""" & fileAudio.Filename & """"
                    }
                }

                private string sChannel = "";
                If nudChannel.Value > 1 Then sChannel = " channel=" + nudChannel.Value

                private string sLoop = "";
                If chkLoop.Checked && sType = " play" Then sLoop = " loop=Y"

                private string sAudio = "<audio" + sType + sSrc + sChannel + sLoop + ">";

                if (Not (sType = " play" && fileAudio.Filename = ""))
                {
                    sAudioTag = sAudio
                    Description(iIndex).Description = sAudioTag + Description(iIndex).Description;
                }
            }

            If sender != null Then RaiseEvent Changed(sender, e)

        }
        catch (Exception ex)
        {
            ErrMsg("Load Audio error", ex);
        }
    }


    private void chkPlay_CheckedValueChanged(object sender, System.EventArgs e)
    {
        if (chkPlay.Checked && fileAudio.Filename = "")
        {
            fileAudio.OpenFileDialog();
        }
    }


    private void fileGraphics_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            'If bLoading Then Exit Sub ' this stops graphics from appearing when loading the form

            private int iIndex = 0;
            If tabsDescriptions.SelectedTab != null Then iIndex = tabsDescriptions.SelectedTab.VisibleIndex

            ' Trim off any existing <img> tag - Why, this just deletes the tag from the text?
            private New System.Text.RegularExpressions.Regex(IMGREGEX, System.Text.RegularExpressions.RegexOptions.IgnoreCase) re;

            if (re.IsMatch(Description(iIndex).Description))
            {
                if (re.Matches(Description(iIndex).Description).Count = 1)
                {
                    private string sMatch = re.Match(Description(iIndex).Description).ToString;
                    'If Description(iIndex).Description.StartsWith(sMatch) Then
                    Description(iIndex).Description = Description(iIndex).Description.Replace(sMatch, "");
                    'End If
                }
            }

            sGraphicsTag = ""
            if (fileGraphics.Filename <> "")
            {
                if (IO.File.Exists(fileGraphics.Filename) || fileGraphics.Filename.ToLower.StartsWith("http"))
                {
                    fileGraphics.txtDir.ForeColor = Color.DarkRed;
                    Me.Cursor = Cursors.WaitCursor;
                    imgGraphics.Load(fileGraphics.Filename) ' Loads either from file or from URL;
                    fileGraphics.txtDir.ForeColor = SystemColors.ControlText;
                    lblImageInstructions.Visible = false;

                    'Dim bResult As Boolean
                    'bResult = New System.Text.RegularExpressions.Regex("[a-zA-Z]:(\\[\w_\.]+)*\.\w+").IsMatch("C:\Users\CampbellWild\Pictures\hon\GEGEBEASKJVV.jpg")

                    if (fileGraphics.Filename <> "")
                    {
                        sGraphicsTag = "<img src=""" & fileGraphics.Filename & """>"
                        Description(iIndex).Description = sGraphicsTag + Description(iIndex).Description;
                    }
                    Me.Cursor = Cursors.Arrow;
                }
            Else
                imgGraphics.Image = null;
                lblImageInstructions.Visible = true;
                'If Description(iIndex).Description.StartsWith("<img src=") AndAlso Description(iIndex).Description.Contains(">") Then
                '    Description(iIndex).Description = Description(iIndex).Description.Substring(Description(iIndex).Description.IndexOf(">") + 1)
                'End If
            }

            If sender != null Then RaiseEvent Changed(sender, e)

        }
        catch (System.ArgumentException exArg)
        {
            imgGraphics.Image = null;
        }
        catch (System.Net.WebException exWeb)
        {
            imgGraphics.Image = null;
        }
        catch (IO.FileNotFoundException exFNF)
        {
            imgGraphics.Image = null;
        }
        catch (IO.IOException exIO)
        {
            imgGraphics.Image = null;
        }
        catch (Exception ex)
        {
            ErrMsg("Load Graphics error", ex);
        }
    }


    Public Shadows Function Focus() As Boolean
        return rtxtSource.Focus();
    }


    private void cmbDisplayWhen_ValueChanged(object sender, System.EventArgs e)
    {
        private int iIndex = 0;
        If tabsDescriptions.SelectedTab != null Then iIndex = tabsDescriptions.SelectedTab.VisibleIndex
        private SingleDescription.DisplayWhenEnum eNewDisplayWhen = CType(cmbDisplayWhen.SelectedItem.DataValue, SingleDescription.DisplayWhenEnum);
        if (Description(iIndex).eDisplayWhen <> eNewDisplayWhen)
        {
            Description(iIndex).eDisplayWhen = eNewDisplayWhen;
            RaiseEvent Changed(Me, New EventArgs);
        }

    }


    private void mnuDeleteTab_Click(object sender, System.EventArgs e)
    {

        if (tabRightClick IsNot null)
        {
            if (Description(tabRightClick.Index).Description <> "" || Description(tabRightClick.Index).Restrictions.Count > 0)
            {
                If MsgBox("Are you sure you wish to delete tab " + tabRightClick.Text + "?", MsgBoxStyle.YesNo | MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
            }

            Description.RemoveAt(tabRightClick.VisibleIndex);
            tabsDescriptions.Tabs.RemoveAt(tabRightClick.Index);
            RaiseEvent Changed(sender, e);
        }

    }

    private void cmsTabs_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        tabRightClick = tabsDescriptions.TabFromPoint(tabsDescriptions.PointToClient(MousePosition))
        if (tabRightClick Is null)
        {
            e.Cancel = true;
        ElseIf tabRightClick.Key = "tabDefault" || tabRightClick.Text = " " Then
            e.Cancel = true;
        }
        'mnuDeleteTab.Visible = Not (tabRightClick Is Nothing OrElse tabRightClick.Key = "tabDefault")
        'If tabRightClick.Text = " " Then e.Cancel = True ' "New" tab
    }


    private void miRenameTab_Click(object sender, System.EventArgs e)
    {

        if (tabRightClick IsNot null)
        {
            private string sTabLabel = InputBox("Enter tab label:", "Rename tab", tabRightClick.Text);
            if (sTabLabel <> "")
            {
                tabRightClick.Text = sTabLabel;

                private int iIndex = tabRightClick.VisibleIndex;
                If Description != null Then Description(iIndex).sTabLabel = sTabLabel
                RaiseEvent Changed(Me, New EventArgs);
            }
        }
    }



    private bool bAdding = false;
    private void tabsDescriptions_TabMoved(object sender, Infragistics.Win.UltraWinTabControl.TabMovedEventArgs e)
    {

        If bAdding Then Exit Sub

        private int iTab1 = CInt(e.Tab.Tag) ' e.Tab.VisibleIndex;
        private int iTab2 = CInt(e.RelativeTab.Tag) ' e.RelativeTab.VisibleIndex;

        'Debug.WriteLine("Moving tab at position " & iTab1 & " to " & e.RelativePosition.ToString & " tab at position " & iTab2)

        if (iTab1 < iTab2)
        {
            If e.RelativePosition = Infragistics.Win.RelativePosition.Before Then iTab2 -= 1
        Else
            If e.RelativePosition = Infragistics.Win.RelativePosition.After Then iTab2 += 1
        }

        'If iTab1 = iTab2 Then
        '    Debug.WriteLine("Nothing needs doing")
        'Else
        '    Debug.WriteLine("Move tab " & iTab1 & " to " & iTab2)
        'End If

        ''Exit Sub

        'For i As Integer = Math.Min(iTab1, iTab2) To Math.Max(iTab1, iTab2) - 1
        '    Dim tempDesc As SingleDescription = oDescription(i)
        '    oDescription(i) = oDescription(i + 1)
        '    oDescription(i + 1) = tempDesc
        'Next

        private SingleDescription tempDesc = oDescription(iTab1);
        if (iTab2 > iTab1)
        {
            for (int i = iTab1; i <= iTab2 - 1; i++)
            {
                oDescription(i) = oDescription(i + 1);
            Next;
        Else
            for (int i = iTab1; i <= iTab2 + 1; i += -1)
            {
                oDescription(i) = oDescription(i - 1);
            Next;
        }
        oDescription(iTab2) = tempDesc;

        'oDescription(iTab1) = oDescription(iTab2)
        'oDescription(iTab2) = tempDesc


    }


    ' Don't allow user to move a tab before the default one
    private void tabsDescriptions_TabMoving(object sender, Infragistics.Win.UltraWinTabControl.TabMovingEventArgs e)
    {
        If e.Tab == tabsDescriptions.Tabs(0) Then e.Cancel = true
        If e.RelativePosition = Infragistics.Win.RelativePosition.Before && e.RelativeTab == tabsDescriptions.Tabs(0) Then e.Cancel = true
        If e.RelativePosition = Infragistics.Win.RelativePosition.After && e.RelativeTab == tabsDescriptions.Tabs(tabsDescriptions.Tabs.Count - 1) Then e.Cancel = true
        e.Tab.Tag = e.Tab.VisibleIndex;
        e.RelativeTab.Tag = e.RelativeTab.VisibleIndex;
    }



    private void chkPlay_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        fileAudio.Enabled = chkPlay.Checked;
        chkLoop.Enabled = chkPlay.Checked;
        nudChannel.Enabled = chkPlay.Checked || chkPause.Checked || chkStop.Checked;
        lblChannel.Enabled = nudChannel.Enabled;
        if (chkPlay.Checked)
        {
            chkPause.Checked = false;
            chkStop.Checked = false;
        }
    }

    private void chkPause_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        nudChannel.Enabled = chkPlay.Checked || chkPause.Checked || chkStop.Checked;
        lblChannel.Enabled = nudChannel.Enabled;
        if (chkPause.Checked)
        {
            chkPlay.Checked = false;
            chkStop.Checked = false;
        }
    }

    private void chkStop_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        nudChannel.Enabled = chkPlay.Checked || chkPause.Checked || chkStop.Checked;
        lblChannel.Enabled = nudChannel.Enabled;
        if (chkStop.Checked)
        {
            chkPlay.Checked = false;
            chkPause.Checked = false;
        }
    }

    private void chkPlay_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If e.Button = Windows.Forms.MouseButtons.Right Then PlaySound(fileAudio.Filename, CInt(nudChannel.Value), chkLoop.Checked)
    }

    private void chkPause_Click(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If e.Button = Windows.Forms.MouseButtons.Right Then PauseSound(CInt(nudChannel.Value))
    }

    private void chkStop_Click(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If e.Button = Windows.Forms.MouseButtons.Right Then StopSound(CInt(nudChannel.Value))
    }

    'Private Sub chkPlayPauseStop_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    '    'CType(sender, Infragistics.Win.UltraWinEditors.UltraCheckEditor).HotTrackingAppearance.BorderColor = Color.Transparent
    'End Sub

    private void chkPauseStop_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        nudChannel.Focus();
        'CType(sender, Infragistics.Win.UltraWinEditors.UltraCheckEditor).HotTrackingAppearance.BorderColor = SystemColors.ActiveBorder
    }

    private void chkPlay_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        If fileAudio.Enabled Then fileAudio.Focus() Else nudChannel.Focus()
        'Type(sender, Infragistics.Win.UltraWinEditors.UltraCheckEditor).HotTrackingAppearance.BorderColor = SystemColors.ActiveBorder
    }

}

}