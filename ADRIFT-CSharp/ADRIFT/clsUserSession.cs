using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace ADRIFT
{
' Everything specific to the user's session that isn't stored in Adventure needs to go here


internal class RunnerSession
{

    Public sIt, sHim, sHer, sThem As String
#if Not www
    public frmDebugger Debugger;
    internal frmUserSplash UserSplash;
#else
    internal New Stack<Font> stackFonts;
    internal New Stack<string> stackColours;
#endif
    public DateTime dtDebug;
    public int iDebugIndent;
    public bool bNoDebug;
    public int iPrepProgress;
    public New StateStack States;
    public bool bAutoComplete;
    internal string sTranscriptFile;
    public int iMarginWidth;
    public bool bEXE = false;
    private New Generic.List<TaskKey> listTaskKeys;
    internal int iMatchedTaskCommand;
    internal New Generic.Dictionary<string, clsMacro> dictMacros;
    internal bool bShowShortLocations = true;
    internal bool bSystemTask = false;
    Private root, obroot, chroot As AutoComplete
    internal New StringArrayList salCommands;
    internal int iPreviousOffset;
    public string sGameFolder;
    public bool bGraphics = true;
    Friend WithEvents Map As ADRIFT.Map;
    internal Font DefaultFont;
    internal bool bUseDefaultFont = false;
    internal string sTurnOutput = "";
    internal bool bRequiresRestoreLayout = false;
    internal string sRememberedVerb = "";
    internal bool bTestingOutput = false;

    public Boolean = True bSound { get; set; }
    Private WithEvents tmrEvents As New Windows.Forms.Timer


    internal void ClearAutoCompletes()
    {
        obroot = Nothing
        chroot = Nothing
    }

private class TaskKey
    {
        Implements IComparable;

        public string sTaskKey;
        public int iPriority;

        public Integer Implements System.IComparable.CompareTo CompareTo(object obj)
        {
            return Me.iPriority.CompareTo(CType(obj, TaskKey).iPriority);
        }
    }




    Friend NewReferences() As clsNewReference ' We should really keep a NewReferences collection PER parent task.  So if we execute tasks within our actions, they have their own References set!!!;

    private New OrderedHashTable htblResponsesPass;
    private New OrderedHashTable htblResponsesFail;
    'Private bTaskHasOutput As Boolean = False

    'Public d As String
    public New TaskHashTable htblCompleteableGeneralTasks;
    public New TaskHashTable ' If we don't get a winner first time round, try again, matching against objects/characters that don't exist htblSecondChanceTasks;
    public New ObjectHashTable htblVisibleObjects;
    public New ObjectHashTable htblSeenObjects;
    Public sInput, sLastProperInput As String
    private int _irestnum;
    internal int iRestNum
        {
            get
            {
            return _irestnum;
        }
set(Integer)
            _irestnum = value
        }
    }
    private string sAmbTask;
    public string sOutputText;
    Public sRestrictionText, sRestrictionTextMulti As String

    internal string sRouteError { get; set; }

    private Hashtable ValidRefs;
    'Private arlMentionedObs As New StringArrayList
    'Private sMentionedObsPattern As String
    'Private bDisplayDebug As Boolean = False 'True

    'Friend Class PronounOffset
    '    Public Offset As Integer
    '    Public Perspective As PerspectiveEnum
    '    Public Key As String ' What is the pronoun applying to?
    '    Public Pronoun As PronounEnum

    '    Sub New(ByVal iOffset As Integer, ePerspective As PerspectiveEnum, sKey As String, Optional ePronoun As PronounEnum = PronounEnum.SubjectInitiator)
    '        Me.Offset = iOffset
    '        Me.Perspective = ePerspective
    '    End Sub
    'End Class
    'Friend Pronouns As New List(Of PronounOffset)

    internal New PronounInfoList PronounKeys;
    'Friend LastMalePronoun As String
    'Friend LastFemalePronoun As String
    'Friend LastItPronoun As String
    'Friend LastPronoun As String
    'Friend LastObjectiveMalePronoun As String
    'Friend LastPossessiveMalePronoun As String
    'Friend LastReflectiveMalePronoun As String
    'Friend LastSubjectiveMalePronoun As String
    'Friend LastObjectiveFemalePronoun As String
    'Friend LastPossessiveFemalePronoun As String
    'Friend LastReflectiveFemalePronoun As String
    'Friend LastSubjectiveFemalePronoun As String
    'Friend LastObjectiveItPronoun As String
    'Friend LastPossessiveItPronoun As String
    'Friend LastReflectiveItPronoun As String
    'Friend LastSubjectiveItPronoun As String

    'Public Sub Main()

    '    Try
    '        bGenerator = False

    '        Dim s As New Size

    '        s.Height = CInt(CInt(GetSetting("ADRIFT", "Runner", "Window Height", (fRunner.Size.Height * 15).ToString)) / 15) '- 20 ' Add this back on when adding menu I think
    '        s.Width = CInt(CInt(GetSetting("ADRIFT", "Runner", "Window Width", (fRunner.Size.Width * 15).ToString)) / 15)

    '        IntroMessage()

    '        fRunner.Size = s
    '        Application.Run(fRunner)

    '    Catch ex As Exception
    '        ErrMsg("Critical Error", ex)
    '    End Try

    'End Sub
private class SortByLength
    {
        Inherits Generic.Comparer(Of String);

        public override int Compare(string x, string y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }


    public void SaveMacros()
    {

        try
        {
            private string sMacrosFile = DataPath(true) + "ADRIFTMacros.xml";

            'If Not IO.Directory.Exists(sLocalDataPath) Then IO.Directory.CreateDirectory(sLocalDataPath)

            private New System.Xml.XmlTextWriter(sMacrosFile, System.Text.Encoding.UTF8) xmlWriter;
            With xmlWriter;
                .Indentation = 4;
                .IndentChar = " "c;
                .Formatting = Formatting.Indented;

                .WriteStartDocument();
                .WriteComment("File created by ADRIFT v" + Application.ProductVersion);

                .WriteStartElement("Macros");

                For Each sTitle As String In dictMacros.Keys
                    .WriteStartElement("Macro");

                    private clsMacro m = dictMacros(sTitle);

                    .WriteElementString("Key", m.Key);
                    .WriteElementString("Title", m.Title);
                    .WriteElementString("Shared", m.Shared.ToString);
                    .WriteElementString("IFID", m.IFID);
                    If m.Shortcut <> Keys.None Then .WriteElementString("Shortcut", m.Shortcut.ToString)
                    .WriteElementString("Commands", m.Commands);

                    .WriteEndElement() ' Macro;
                Next;

                .WriteEndElement() ' Macros;

                .WriteEndDocument();

                .Flush();
                .Close();

            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error saving macros", ex);
        }

    }


    ' Load Macros from file
    public void LoadMacros()
    {

        try
        {
            private string sMacrosFile = DataPath(true) + "ADRIFTMacros.xml";
            dictMacros.Clear();

            if (IO.File.Exists(sMacrosFile))
            {
                private New System.Xml.XmlDocument xmlDoc;
                xmlDoc.Load(sMacrosFile);
                private bool bFirst = true;

                For Each nod As System.Xml.XmlNode In xmlDoc.SelectNodes("/Macros/Macro")

                    private string sTitle = nod.SelectSingleNode("Title").InnerText;
                    private string sCommands = nod.SelectSingleNode("Commands").InnerText;
                    private string sKey = nod.SelectSingleNode("Key").InnerText;

                    if (sKey <> "" && sTitle <> "" && sCommands <> "")
                    {
                        private New clsMacro(sKey) macro;
                        macro.Title = sTitle;
                        macro.Commands = sCommands;

                        If nod.SelectSingleNode("Shared") != null Then macro.Shared = SafeBool(nod.SelectSingleNode("Shared").InnerText)
                        If nod.SelectSingleNode("IFID") != null Then macro.IFID = SafeString(nod.SelectSingleNode("IFID").InnerText)
                        If nod.SelectSingleNode("Shortcut") != null Then macro.Shortcut = (nod.SelectSingleNode("Shortcut")([Enum].Parse(GetType(Shortcut)).InnerText), Shortcut)

                        dictMacros.Add(macro.Key, macro);
                    }
                Next;
            }

#if Not www
            fRunner.ReloadMacros();
#endif

        }
        catch (Exception ex)
        {
            ErrMsg("Error loading macros", ex);
        }

    }


    public bool OpenAdventure(string sFilename, bool bSilentError = false)
    {

#if Not www And Not Mono
        If Adventure != null Then fRunner.SaveLayout()

        ' Close any custom frames
        For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In fRunner.UDMRunner.ControlPanes
            if (cp.Key.StartsWith("pane_"))
            {
                cp.Control.Dispose();
                cp.Close();
            }
        Next;
#endif

        Adventure = New clsAdventure

        States.Clear();
        salCommands.Clear();
        salCommands.Add(fRunner.txtInput.Text);

        'LoadDefaults()
        'CreatePlayer() ' For now
        iPreviousOffset = 0
        private FileIO.FileTypeEnum eFileType = FileTypeEnum.TextAdventure_TAF;
        If sFilename.ToLower.EndsWith(".blorb") Then eFileType = FileTypeEnum.Blorb
        If sFilename.ToLower.EndsWith(".exe") Then eFileType = FileTypeEnum.Exe

        if (LoadFile(sFilename, eFileType, LoadWhatEnum.All, false, , , , bSilentError))
        {
            for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
            {
                Adventure.sDirectionsRE(eDirection) = Adventure.sDirectionsRE(eDirection).ToLower;
            Next;
#if Not www
            AddFileToList(Adventure.FullPath);
            if (bEXE)
            {
                fRunner.Text = StripCarats(Adventure.Title);
            Else
                fRunner.Text = StripCarats(Adventure.Title) + " - ADRIFT Runner";
            }
#if Not Mono
            fRunner.RestoreLayout();
#endif
#else
            fRunner.SetTitle(StripCarats(Adventure.Title) + " - ADRIFT WebRunner");
#endif
#if Mono
            fRunner.miOpenGame.Enabled = true;
            fRunner.miRestartGame.Enabled = true;
            fRunner.miSaveGame.Enabled = true;
            fRunner.miSaveGameAs.Enabled = true;
            fRunner.miMacros.Enabled = true;
            If ! bEXE Then fRunner.miDebugger.Visible = Adventure.EnableDebugger
#elif Not www
            fRunner.UTMMain.Tools("OpenGame").SharedProps.Enabled = true;
            fRunner.UTMMain.Tools("RestartGame").SharedProps.Enabled = true;
            fRunner.UTMMain.Tools("SaveGame").SharedProps.Enabled = true;
            fRunner.UTMMain.Tools("SaveGameAs").SharedProps.Enabled = true;
            fRunner.UTMMain.Tools("Macros").SharedProps.Enabled = true;
            If ! bEXE Then fRunner.UTMMain.Tools("Debugger").SharedProps.Visible = Adventure.EnableDebugger
#endif

            'Sort tasks by priority
            listTaskKeys.Clear();
            For Each tas As clsTask In Adventure.htblTasks.Values
                private New TaskKey tk;
                tk.sTaskKey = tas.Key;
                tk.iPriority = tas.Priority;
                listTaskKeys.Add(tk);
            Next;
            listTaskKeys.Sort();

            Adventure.eGameState = clsAction.EndGameEnum.Running;

#if Not www
            if (fRunner.pbxGraphics IsNot null && fRunner.pbxGraphics.Visible && Adventure.Images.Count = 0)
            {
#if Mono
                fRunner.SplitContainerMapGraphics.Panel2Collapsed = true;
#else
                fRunner.UDMRunner.PaneFromKey("Graphics").Close();
#endif
                'ElseIf bGraphics AndAlso Not fRunner.pbxGraphics.Visible AndAlso Adventure.Images.Count > 0 Then
                '    fRunner.UDMRunner.PaneFromKey("Graphics").Show()
            }
#endif

            ' Initialise any array values
            For Each v As clsVariable In Adventure.htblVariables.Values
                if (v.Length > 1)
                {
                    Dim sInitialValue() As String = v.StringValue.Split(Chr(10))
                    if (sInitialValue.Length = v.Length)
                    {
                        private int i = 1;
                        For Each sValue As String In sInitialValue
                            if (v.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                v.IntValue(i) = SafeInt(sValue);
                            Else
                                v.StringValue(i) = sValue.Replace(Chr(13), "");
                            }
                            i += 1;
                        Next;
                    }
                }
            Next;

            For Each t As clsTask In Adventure.htblTasks.Values
                for (int i = 0; i <= t.arlCommands.Count - 1; i++)
                {
                    t.arlCommands(i) = CorrectCommand(t.arlCommands(i));
                Next;
                if (t.TaskType = clsTask.TaskTypeEnum.System && t.RunImmediately)
                {
                    AttemptToExecuteTask(t.Key, true);
                }
            Next;

            'For Each ob As clsObject In Adventure.htblObjects.Values ' for now
            '    ob.SeenBy(Player.Key) = True
            'Next

            UpdateStatusBar();
            InitialiseInputBox();
            fRunner.SetBackgroundColour();
#if Not www
            fRunner.pbxGraphics.Image = null;
#endif

            Adventure.Player.HasSeenLocation(Adventure.Player.Location.LocationKey) = true;
            '#If Not www Then
            UserSession.Map.RecalculateNode(Adventure.Map.FindNode(Adventure.Player.Location.LocationKey));
            UserSession.Map.SelectNode(Adventure.Player.Location.LocationKey);
            '#End If
            fRunner.txtOutput.Clear();
            Display("<c>" + Adventure.Title + "</c>" + vbCrLf, true);
            Display(Adventure.Introduction.ToString, true);

            If Adventure.ShowFirstRoom && Adventure.htblLocations.ContainsKey(Adventure.Player.Location.LocationKey) Then Display(vbCrLf + pSpace(Adventure.htblLocations(Adventure.Player.Location.LocationKey).ViewLocation), true)

            For Each e As clsEvent In Adventure.htblEvents.Values
                switch (e.WhenStart)
                {
                    case clsEvent.WhenStartEnum.AfterATask:
                        {
                        e.Status = clsEvent.StatusEnum.NotYetStarted;
                    case clsEvent.WhenStartEnum.BetweenXandYTurns:
                        {
                        e.Status = clsEvent.StatusEnum.CountingDownToStart;
                        e.TimerToEndOfEvent = e.StartDelay.Value + e.Length.Value;
                    case clsEvent.WhenStartEnum.Immediately:
                        {
                        'e.Status = clsEvent.StatusEnum.Running
                        'e.TimerToEndOfEvent = 0
                        e.Start(true);
                }
                'e.IncrementTimer() ' .DoAnySubEvents()
                'e.DoAnySubEvents()
            Next;

            ' Needs to be a separate loop in case a later event runs a task that starts an earlier event
            For Each e As clsEvent In Adventure.htblEvents.Values
                e.bJustStarted = false;
            Next;

            For Each c As clsCharacter In Adventure.htblCharacters.Values
                For Each w As clsWalk In c.arlWalks
                    If w.StartActive Then w.Start(true)
                Next;
                For Each w As clsWalk In c.arlWalks
                    w.bJustStarted = false;
                Next;

                ' Sort our topics by descending length
                private New Generic.SortedDictionary<string, Generic.List<String)>> topickeys;
                For Each t As clsTopic In c.htblTopics.Values
                    If t.bCommand Then t.Keywords = CorrectCommand(t.Keywords)
                    If ! topickeys.ContainsKey(t.Keywords) Then topickeys.Add(t.Keywords, New Generic.List(Of String))
                    topickeys(t.Keywords).Add(t.Key);
                Next;
                private New TopicHashTable htblTopicsNew;
                For Each sTopic As String In topickeys.Keys
                    For Each sKey As String In topickeys(sTopic)
                        htblTopicsNew.Add(c.htblTopics(sKey));
                    Next;
                Next;
                c.htblTopics = htblTopicsNew;
            Next;

            Display(vbCrLf + vbCrLf, true);

            For Each task As clsTask In Adventure.htblTasks.Values
                ReDim task.NewReferences(task.References.Count - 1);
            Next;

            if (Adventure.htblLocations.Count = 0)
            {
                ErrMsg("This adventure has no locations.  Cannot continue.");
                return false;
            }

            'bSystemTask = True
            PrepareForNextTurn();
            'bSystemTask = False
#if Not www
            If ! Debugger == null && ! Debugger.IsDisposed Then Debugger.BuildTree()
            fRunner.ReloadMacros();
#endif

            tmrEvents.Interval = 1000;
            tmrEvents.Start();

            return true;
        Else
            return false;
        }

    }


    public void InitialiseInputBox(string sCursor = "Ø")
    {

#if Not www
        try
        {
            With fRunner.txtInput;
                .ReadOnly = false;
                if (sCursor = "@")
                {
                    .Tag = "Comment";
                Else
                    .Tag = null;
                }
                .Clear();
                .ForeColor = GetInputColour();
#if Mono
                switch (sCursor)
                {
                    case "Ø" ' 27A2:
                        {
                        sCursor = ChrW(&H27A2).ToString
                    case "@" ' 270D:
                        {
                        sCursor = ChrW(&H270D).ToString
                }
                .SelectionFont = New Font("OpenSymbol", 14);
                'sCursor = ">"
                '.SelectionFont = New Font("Arial", 14)
'#ElseIf www Then
'                sCursor = ""
#else
                .SelectionFont = New Font("Wingdings", 14);
#endif
                '.SelectedText = sCursor
                if (sCursor <> "")
                {
                    .AppendText(sCursor);

                    .SelectionStart = 1;
                    if (bUseDefaultFont)
                    {
                        .SelectionFont = DefaultFont;
                    Else
                        if (Adventure IsNot null)
                        {
                            .SelectionFont = Adventure.DefaultFont;
                        Else
                            .SelectionFont = New Font("Arial", 12, GraphicsUnit.Point);
                        }
                    }

                    .AppendText(" ");

                    .SelectionStart = 2;
                }
                if (bUseDefaultFont)
                {
                    .SelectionFont = DefaultFont;
                Else
                    if (Adventure IsNot null)
                    {
                        .SelectionFont = Adventure.DefaultFont;
                    Else
                        .SelectionFont = New Font("Arial", 12, GraphicsUnit.Point);
                    }
                }
                .Focus();
                Debug.WriteLine("txtInput Focused");
            }

        }
        catch (ObjectDisposedException exOD)
        {
            ' Fail silently - shutting down
        }
        catch (Exception ex)
        {
            ErrMsg("InitialiseInputBox error", ex);
        }
#endif

    }



    '    Public Sub DisplayDebug(ByVal sText As String)
    '#If DEBUG Then
    '        If bDisplayDebug Then
    '#If False Then
    '            Debug.WriteLine(sText.Replace(vbCrLf, ""))
    '            sText = "<i><font color=""#3333FF"" size=11 face=""Courier New"">" & sText & "</font></i>"
    '            sText = ReplaceALRs(ReplaceFunctions(sText))
    '            'If sOutputText <> "" AndAlso Right(sOutputText, 2) <> "  " AndAlso Right(sOutputText, 2) <> vbCrLf Then sOutputText &= "  "
    '            'sOutputText &= stext
    '            Source2HTML(sText, fRunner.txtOutput, False)
    '#End If
    '        End If
    '#End If
    '    End Sub

    public bool bDisplaying = false ' In case any output is once only - don't want it to trigger when we're just testing the text;
    public void Display(string sText, bool bCommit = false, bool bAllowALR = true, bool bRecord = true)
    {

        bDisplaying = True
        private bool bAllowPSpace = true;

        'If sText.Length > 0 Then sText = sText.Substring(0, 1).ToUpper & sText.Substring(1)
        if (Adventure IsNot null && Adventure.dVersion < 5)
        {
            ' ViewRoom function used to always start at beginning of line, so no pspace
            If sText.StartsWith("%DisplayLocation[") Then bAllowPSpace = false
        }

        If Adventure != null && bAllowALR Then sText = ReplaceALRs(sText) ' ReplaceALRs(ReplaceFunctions(sText))
        'If sOutputText <> "" AndAlso Right(sOutputText, 2) <> "  " AndAlso Right(sOutputText, 2) <> vbCrLf Then sOutputText &= "  "
        if (bAllowPSpace && sText <> vbCrLf && sText <> "")
        {
            sOutputText = pSpace(sOutputText) & sText ' &= sText
        Else
            sOutputText = sOutputText & sText ' &= sText
        }

        if (bCommit)
        {
            If ! (sText.StartsWith("<c>") && sText.EndsWith("</c>" + vbCrLf)) Then UnderlineNouns(sOutputText)
            Source2HTML(sOutputText, fRunner.txtOutput, false);
#if Mono
            if (fRunner.miStartTranscript.Text = "Stop Transcript")
            {
#elif Not www
            if (fRunner.UTMMain.Tools("miStartTranscript").SharedProps.Caption = "Stop Transcript")
            {
#else
            if (false)
            {
#endif
                try
                {
                    private New IO.StreamWriter(sTranscriptFile, True) stmWriter;
                    stmWriter.Write(StripCarats(sOutputText).Replace("Ø", ">"));
                    stmWriter.Close();
                }
                catch (IO.IOException exIO)
                {
                    ErrMsg("Unable to output to transcript: " + exIO.Message);
                }
            }

            if (bRecord)
            {
                while (sOutputText.EndsWith(vbCrLf))
                {
                    sOutputText = sOutputText.Substring(0, sOutputText.Length - 2)
                }
                sTurnOutput &= sOutputText;
            }
            sOutputText = ""
            'fRunner.txtOutput.ScrollToCaret()
        }

        bDisplaying = False

    }


    private void UnderlineNouns(ref sText As String)
    {

        If Adventure == null || ! Adventure.EnableMenu Then Exit Sub
        If ! CBool(GetSetting("ADRIFT", "Runner", "EnableMenu", "1")) || ! CBool(GetSetting("ADRIFT", "Runner", "DisplayLinks", "0")) Then Exit Sub

        private New List<string> listNouns;

        For Each ob As clsObject In Adventure.htblObjects.Values
            if (ob.IsVisibleTo(Adventure.Player.Key))
            {
                For Each sNoun As String In ob.arlNames
                    If ! listNouns.Contains(sNoun) Then listNouns.Add(sNoun)
                Next;
            }
        Next;
        For Each ch As clsCharacter In Adventure.htblCharacters.Values
            if (Adventure.Player.CanSeeCharacter(ch.Key))
            {
                For Each sNoun As String In ch.arlDescriptors
                    If ! listNouns.Contains(sNoun) && sNoun <> "me" Then listNouns.Add(sNoun)
                Next;
                if (ch.ProperName <> "" Then ' (Not Adventure.htblCharacterProperties.ContainsKey("Known") || ch.Known || ch.arlDescriptors.Count = 0) && ch.ProperName <> "")
                {
                    If ! listNouns.Contains(ch.ProperName) Then listNouns.Add(ch.ProperName)
                }
            }
        Next;
        for (DirectionsEnum eDir = DirectionsEnum.North; eDir <= DirectionsEnum.NorthWest; eDir++)
        {
            if (Adventure.Player.HasRouteInDirection(eDir, false))
            {
                For Each sDir As String In Adventure.sDirectionsRE(eDir).Split("/"c)
                    If sDir.Length > 1 && ! listNouns.Contains(sDir) Then listNouns.Add(sDir)
                Next;
            }
        Next;

        private New Dictionary<string, string> dictTags;
        private New System.Text.RegularExpressions.Regex(".*?(?<tags><.*?>).*?") reIgnore;
        private DateTime dtStart = Now;
        while (reIgnore.IsMatch(sText))
        {
            private string sMatch = reIgnore.Match(sText).Groups("tags").Value;
            private string sGUID = System.Guid.NewGuid.ToString.Substring(0, 5);
            while (dictTags.ContainsKey(sGUID))
            {
                sGUID = System.Guid.NewGuid.ToString.Substring(0, 5)
            }
            dictTags.Add(sGUID, sMatch);
            sText = sText.Replace(sMatch, sGUID)
        }

        ' Make sure we don't replace anything inside a tag (could be part of an image filename!)
        For Each sNoun As String In listNouns
            private New System.Text.RegularExpressions.Regex("[""'\n ](?<noun>" & sNoun & ")["".,!;'\?\n(</c>) ]", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Multiline) re;
            if (re.IsMatch(sText))
            {
                sText = System.Text.RegularExpressions.Regex.Replace(sText, "(?<pre>[""'\n ])(?<noun>" & sNoun & ")(?<post>["".,!;'\?\n(</c>) ])", _
                    Function(match As System.Text.RegularExpressions.Match);
                        return match.Groups("pre").Value + "<u><font color=""#" + Hex(GetLinkColour.ToArgb).Substring(2) + """>" + match.Groups("noun").Value + "</font></u>" + match.Groups("post").Value;
                    End Function, System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Singleline)
            }
        Next;

        dtStart = Now
        For Each sGUID As String In dictTags.Keys
            sText = sText.Replace(sGUID, dictTags(sGUID))
        Next;

    }


    public void LoadDefaults()
    {
        CreatePlayer();
        CreateRequiredProperties();
    }


    private void CreatePlayer()
    {
        private New clsCharacter Player;
        With Player;
            .ProperName = "Player";
            .CharacterType = clsCharacter.CharacterTypeEnum.Player;
            .Key = "Player";
        }
        Adventure.htblCharacters.Add(Player, Player.Key);
    }

    private void CreateRequiredProperties()
    {

        private New clsProperty pStaticDynamic;
        With pStaticDynamic;
            .Key = "StaticOrDynamic";
            .Description = "Object type";
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Static");
            .arlStates.Add("Dynamic");
            .Mandatory = true;
        }
        Adventure.htblAllProperties.Add(pStaticDynamic);

        private New clsProperty pSurface;
        With pSurface;
            .Key = "Surface";
            .Description = "This object has a surface";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pSurface);

        private New clsProperty pSurfaceHold;
        With pSurfaceHold;
            .Key = "SurfaceHold";
            .Description = "Surface can hold";
            .Type = clsProperty.PropertyTypeEnum.Integer;
            .DependentKey = "Surface";
        }
        Adventure.htblAllProperties.Add(pSurfaceHold);

        private New clsProperty pReadable;
        With pReadable;
            .Key = "Readable";
            .Description = "This object is readable";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pReadable);

        private New clsProperty pReadText;
        With pReadText;
            .Key = "ReadText";
            .Description = "Description when read";
            .Type = clsProperty.PropertyTypeEnum.Text;
            .DependentKey = "Readable";
        }
        Adventure.htblAllProperties.Add(pReadText);

        private New clsProperty pWearable;
        With pWearable;
            .Key = "Wearable";
            .Description = "Wearable";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Dynamic";
        }
        Adventure.htblAllProperties.Add(pWearable);

        private New clsProperty pContainer;
        With pContainer;
            .Key = "Container";
            .Description = "This object is a container";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pContainer);

        private New clsProperty pContainerHold;
        With pContainerHold;
            .Key = "ContainerHold";
            .Description = "Container can hold";
            .Type = clsProperty.PropertyTypeEnum.Integer;
            .DependentKey = "Container";
        }
        Adventure.htblAllProperties.Add(pContainerHold);

        private New clsProperty pOpenable;
        With pOpenable;
            .Key = "Openable";
            .Description = "Object can be opened and closed";
            .Type = clsProperty.PropertyTypeEnum.StateList;
            .arlStates.Add("Open");
            .arlStates.Add("Closed");
        }
        Adventure.htblAllProperties.Add(pOpenable);

        private New clsProperty pLockable;
        With pLockable;
            .Key = "Lockable";
            .Description = "& is lockable, with key";
            .Type = clsProperty.PropertyTypeEnum.ObjectKey;
            .DependentKey = "Openable";
        }
        Adventure.htblAllProperties.Add(pLockable);

        private New clsProperty pSittable;
        With pSittable;
            .Key = "Sittable";
            .Description = "Characters can sit on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pSittable);

        private New clsProperty pStandable;
        With pStandable;
            .Key = "Standable";
            .Description = "Characters can stand on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pStandable);

        private New clsProperty pLieable;
        With pLieable;
            .Key = "Lieable";
            .Description = "Characters can lie on this object";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
        }
        Adventure.htblAllProperties.Add(pLieable);

        private New clsProperty pEdible;
        With pEdible;
            .Key = "Edible";
            .Description = "Object is edible";
            .Type = clsProperty.PropertyTypeEnum.SelectionOnly;
            .DependentKey = "StaticOrDynamic";
            .DependentValue = "Dynamic";
        }
        Adventure.htblAllProperties.Add(pEdible);

    }


    public void Process(string sCommand)
    {

        if (Not Adventure Is null)
        {
#if Not www
            iPreviousOffset = fRunner.txtOutput.TextLength
#endif
            If iPrepProgress = 0 Then iPrepProgress = 1
            bNoDebug = False
            dtDebug = Now
            iDebugIndent = 0
            bSystemTask = False
            'htblResponses.Clear()
            private DateTime dtStart = Now;
            while (iPrepProgress <> 2 && Now < dtStart.AddSeconds(10) ' Wait for the other thread to finish)
            {
                Application.DoEvents();
                Threading.Thread.Sleep(1);
            }
            sInput = sCommand

            if (EvaluateInput(0, false) = "***SYSTEM***")
            {
                sOutputText = ""
                Exit Sub;
            }
            bNoDebug = True

            CheckEndOfGame();
            If Adventure.eGameState <> clsAction.EndGameEnum.Running Then Exit Sub
            'If Not Adventure.bDisplayedWinLose Then
            '    Select Case Adventure.eGameState
            '        Case clsAction.EndGameEnum.Win
            '            Display("<center><c><b>*** You have won ***</b></c></center>" & vbCrLf, True)
            '        Case clsAction.EndGameEnum.Lose
            '            Display("<center><c><b>*** You have lost ***</b></c></center>" & vbCrLf, True)
            '        Case clsAction.EndGameEnum.Neutral
            '            ' Don't display anything
            '        Case clsAction.EndGameEnum.Running
            '            ' Continue
            '    End Select
            '    If Adventure.eGameState <> clsAction.EndGameEnum.Running Then States.RecordState()
            'End If
            'If Adventure.eGameState <> clsAction.EndGameEnum.Running Then
            '    Adventure.Player.WalkTo = ""
            '    If Not Adventure.bDisplayedWinLose AndAlso Adventure.WinningText.ToString(True) <> "" Then
            '        Display(Adventure.WinningText.ToString & vbCrLf)
            '    End If
            '    If Adventure.MaxScore > 0 AndAlso Not Adventure.bDisplayedWinLose Then
            '        Display("In that game you scored " & Adventure.Score & " out of a possible " & Adventure.MaxScore & ", in " & Adventure.Turns & " turns." & vbCrLf & vbCrLf, True)
            '    End If
            '    Display("Would you like to <c>restart</c>, <c>restore</c> a saved game, <c>quit</c> or <c>undo</c> the last command?" & vbCrLf & vbCrLf, True)
            '    Adventure.bDisplayedWinLose = True
            '    Exit Sub
            'End If

            iPrepProgress = 0
            PrepareForNextTurn();

            Adventure.Player.DoWalk();

#if Not www
            If Debugger != null && ! Debugger.IsDisposed Then Debugger.RefreshValues()
#endif

        }

    }


    private void CheckEndOfGame()
    {

        if (Not Adventure.bDisplayedWinLose)
        {
            switch (Adventure.eGameState)
            {
                case clsAction.EndGameEnum.Win:
                    {
                    Display("<center><c><b>*** You have won ***</b></c></center>" + vbCrLf, true);
                case clsAction.EndGameEnum.Lose:
                    {
                    Display("<center><c><b>*** You have lost ***</b></c></center>" + vbCrLf, true);
                case clsAction.EndGameEnum.Neutral:
                    {
                    ' Don't display anything
                case clsAction.EndGameEnum.Running:
                    {
                    ' Continue
            }
            If Adventure.eGameState <> clsAction.EndGameEnum.Running Then States.RecordState()
        }
        if (Adventure.eGameState <> clsAction.EndGameEnum.Running)
        {
            Adventure.Player.WalkTo = "";
            if (Not Adventure.bDisplayedWinLose && Adventure.WinningText.ToString(true) <> "")
            {
                Display(Adventure.WinningText.ToString + vbCrLf);
            }
            if (Adventure.MaxScore > 0 && Not Adventure.bDisplayedWinLose)
            {
                Display("In that game you scored " + Adventure.Score + " out of a possible " + Adventure.MaxScore + ", in " + Adventure.Turns + " turns." + vbCrLf + vbCrLf, true);
            }
            Display("Would you like to <c>restart</c>, <c>restore</c> a saved game, <c>quit</c> or <c>undo</c> the last command?" + vbCrLf + vbCrLf, true);
            Adventure.bDisplayedWinLose = true;
        }

    }


    private bool KeyListsMatch(StringArrayList salSpecifics, ArrayList salReferences, bool bMultiple)
    {

        if (salSpecifics.Count = salReferences.Count)
        {
            For Each sSpecific As String In salSpecifics
                private bool bFound = false;
                For Each salRef As StringArrayList In salReferences
                    if (salRef(0) = sSpecific)
                    {
                        bFound = True
                        Exit For;
                    }
                Next;
                If ! bFound Then Return false
            Next;
        Else
            return false;
        }

        return true;

    }


    ' Ensures every Reference has at least one object assigned to it
    private bool AllRefsHaveAtLeastOne()
    {

        If NewReferences == null Then Return false

        for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
        {
            With NewReferences(iRef);
                if (.Items.Count > 0)
                {
                    For Each itm As clsSingleItem In .Items
                        If itm.MatchingPossibilities.Count = 0 Then Return false
                    Next;
                Else
                    return false;
                }
            }
        Next;

        return true;

    }


    ' If we have multiple commands, and we are matching on a different command, make sure we get the right ref
    ' E.g. give %object% to %character%
    '      give %character% %object%
    private int GetAlternateRef(clsTask task, int iRef)
    {

        while (task.TaskType = clsTask.TaskTypeEnum.Specific && task.Parent <> "")
        {
            task = Adventure.htblTasks(task.Parent)
        }

        private string sMatchingRef = task.References(iRef);
        private int iMatch = 0;

        For Each sOrigRef As String In task.RefsInCommand(task.TaskCommand(task, False, 0))
            If sOrigRef = sMatchingRef Then Return iMatch
            iMatch += 1;
        Next;

        return iRef;

    }

    ' Specifics are always defined in the order of the first command in the task
    ' We may be matching on a different command, in which case Specifics will be
    ' in a different order from the References
    '
    private bool RefsMatchSpecifics(clsTask tasChild)
    {

        Dim aSpecifics() As clsTask.Specific = tasChild.Specifics
        private New ArrayList alSpecs;
        private New StringArrayList salRefs;

        ' See if we have all the Specifics we need in the References
        if (NewReferences IsNot null && aSpecifics.Length = NewReferences.Length)
        {
            for (int iSpec = 0; iSpec <= aSpecifics.Length - 1; iSpec++)
            {
                ' Make sure References contains all Specifics
                With aSpecifics(iSpec)
                    For Each sKey As String In .Keys ' We must find all of these in order for the task to match
                        private bool bKeyFoundInRefs = false;
                        If sKey = "" Then ' i.e. match any object/character etc...
                            bKeyFoundInRefs = True
                        Else
                            If sKey.Contains("%") Then sKey = ReplaceFunctions(sKey)

                            private int iReference = iSpec;
                            ' TODO - If this is matching on a second command in the task where the refs are the other way around, it fails to match the specifics
                            ' This is because Specific tasks always match on the first General task command
                            if (UserSession.iMatchedTaskCommand > 0)
                            {
                                iReference = GetAlternateRef(tasChild, iReference)
                            }

                            ' Grab the correct Reference
                            private clsNewReference NewRef = NewReferences(iReference);
                            For Each Ref As clsNewReference In NewReferences
                                if (Ref.Index = iSpec)
                                {
                                    NewRef = Ref
                                    Exit For;
                                }
                            Next;

                            for (int iRef = NewRef.Items.Count - 1; iRef <= 0; iRef += -1)
                            {
                                if (NewRef.Items(iRef).MatchingPossibilities(0).ToLower = sKey.ToLower)
                                {
                                    bKeyFoundInRefs = True
                                    alSpecs.Add(iSpec);
                                    salRefs.Add(sKey);
                                    Exit For;
                                }
                            Next;
                        }
                        If ! bKeyFoundInRefs Then Return false
                    Next;
                }
            Next;
        Else
            ' Not matching same amount
            return false;
        }

        return true;

    }


    private bool RefsMatchSpecificsOld(clsTask tasChild)
    {

        Dim aSpecifics() As clsTask.Specific = tasChild.Specifics
        private New ArrayList alSpecs;
        private New StringArrayList salRefs;

        ' See if we have all the Specifics we need in the References
        if (NewReferences IsNot null && aSpecifics.Length = NewReferences.Length)
        {
            for (int iSpec = 0; iSpec <= aSpecifics.Length - 1; iSpec++)
            {
                ' Make sure References contains all Specifics
                With aSpecifics(iSpec)
                    For Each sKey As String In .Keys ' We must find all of these in order for the task to match
                        private bool bKeyFoundInRefs = false;
                        if (sKey = "")
                        {
                            bKeyFoundInRefs = True
                        Else
                            If sKey = "%Player%" Then sKey = Adventure.Player.Key
                            for (int iRef = NewReferences(iSpec).Items.Count - 1; iRef <= 0; iRef += -1)
                            {
                                if (NewReferences(iSpec).Items(iRef).MatchingPossibilities(0) = sKey)
                                {
                                    bKeyFoundInRefs = True
                                    'If .Multiple Then
                                    alSpecs.Add(iSpec);
                                    salRefs.Add(sKey);
                                    'End If
                                    Exit For;
                                }
                            Next;
                        }
                        If ! bKeyFoundInRefs Then Return false
                    Next;
                }
            Next;
        Else
            ' Not matching same amount
            return false;
        }

        return true;

    }


    internal void DebugPrint(ItemEnum eItemType, string sKey, DebugDetailLevelEnum eDetailLevel, string sMessage, bool bNewLine = true)
    {
#if Not www
        'Debug.WriteLine(sMessage)
        If bNoDebug Then Exit Sub
        If Threading.Thread.CurrentThread.Name = "Background" Then Exit Sub
        If Debugger == null || Debugger.IsDisposed Then Exit Sub
        Debugger.Print(eItemType, sKey, eDetailLevel, CStr(IIf(sMessage = "", "(no output)", sMessage)), bNewLine);
#endif
    }


    private clsNewReference, ByVal iRefIndex As Integer, ByVal sReferenceKeys[] ExecuteSubTasks(string sTaskKey, bool bCalledFromEvent, bool bChildTask, InReferences()
    {

        if (iRefIndex < InReferences.Length)
        {
            if (InReferences(iRefIndex) Is null || InReferences(iRefIndex).Items Is null || InReferences(iRefIndex).Items.Count = 0)
            {
                sReferenceKeys(iRefIndex) = "";
                sReferenceCommands(iRefIndex) = "";
                If ExecuteSubTasks(sTaskKey, bCalledFromEvent, bChildTask, InReferences, iRefIndex + 1, sReferenceKeys, sReferenceCommands, bTaskHasOutputNew, bPassingOnly) Then Return true
            Else
                for (int iItem = 0; iItem <= InReferences(iRefIndex).Items.Count - 1; iItem++)
                {
                    if (InReferences(iRefIndex).Items(iItem).MatchingPossibilities.Count > 0)
                    {
                        sReferenceKeys(iRefIndex) = InReferences(iRefIndex).Items(iItem).MatchingPossibilities(0);
                    Else
                        TODO("Check that this is intended...") ' Assume it's ok to leave this reference key as null as there was no match;
                    }
                    sReferenceCommands(iRefIndex) = InReferences(iRefIndex).Items(iItem).sCommandReference;
                    If ExecuteSubTasks(sTaskKey, bCalledFromEvent, bChildTask, InReferences, iRefIndex + 1, sReferenceKeys, sReferenceCommands, bTaskHasOutputNew, bPassingOnly) Then ExecuteSubTasks = true
                Next;
            }
        Else
            ' Ok, let's execute 'em
            If AttemptToExecuteSubTask(sTaskKey, sReferenceKeys, bCalledFromEvent, bChildTask, sReferenceCommands, bTaskHasOutputNew, bPassingOnly) Then Return true
        }

    }


    ' EvaluateResponses - Should we replace functions for anything output by this task?  We do this so that e.g. ViewRoom will be at the time the task was executed
    ' bPassingOnly - Only do anything with the task if it passes.  Means we already have a failing task with output, so don't need another
    ' bAssignSpecificRefs - If we are attempting to run a specific task(e.g. from an event) then set the refs to those defined in the task
    '
    public bool AttemptToExecuteTask(string sTaskKey, bool bCalledFromEvent = false, bool bSkipRestrictionsTest = false, bool bChildTask = false, ref bContinue As Boolean = False, ref bTaskHasOutputNew As Boolean = False, bool bEvaluateResponses = false, bool bPassingOnly = false, bool bAssignSpecificRefs = false)
    {

        ' E.g. if our task is "get red ball and blue ball from box", then subtasks are
        ' "get red ball from box" and
        ' "get blue ball from box"

        private clsTask task = Adventure.htblTasks(sTaskKey);

        If task.Completed && ! task.Repeatable Then Return false
        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Low, "Attempting to execute task " + task.Description + "...");

        private OrderedHashTable htblResponsesPassTemp = null;
        private OrderedHashTable htblResponsesFailTemp = null;
        if (Not bChildTask)
        {
            If htblResponsesPass.Count > 0 Then htblResponsesPassTemp = htblResponsesPass.Clone
            If htblResponsesFail.Count > 0 Then htblResponsesFailTemp = htblResponsesFail.Clone
            htblResponsesPass.Clear() ' Will this cause a problem, or do we just need to call it before events run tasks for example?;
            htblResponsesFail.Clear();
            'bTaskHasOutput = False
        }
        'Dim bTaskHasOutputTemp As Boolean = bTaskHasOutput
        'bTaskHasOutput = False

        ' This is really just in case anyone tries to run a specific task from an event - we need to know what the refs should be
        if (task.TaskType = clsTask.TaskTypeEnum.Specific && bAssignSpecificRefs)
        {
            ReDim NewReferences(task.Specifics.Length - 1);
            for (int i = 0; i <= NewReferences.Length - 1; i++)
            {
                NewReferences(i) = New clsNewReference(task.Specifics(i).Type);
                With NewReferences(i);
                    For Each sKey As String In task.Specifics(i).Keys
                        .Items.Add(New clsSingleItem(sKey));
                    Next;
                }
            Next;
        }


        private bool bPass = false;
        Dim InReferences() As clsNewReference = task.CopyNewRefs(NewReferences)
        If InReferences == null Then ReDim InReferences(-1)

        ' If we mention any characters on the command line, add them to the mentioned characters list (so we get "the" char rather than "a" char)
        For Each ref As clsNewReference In InReferences
            if (ref IsNot null)
            {
                if (ref.ReferenceType = ReferencesType.Character)
                {
                    For Each itm As clsSingleItem In ref.Items
                        if (itm.MatchingPossibilities.Count = 1)
                        {
                            if (Adventure.htblCharacters.ContainsKey(itm.MatchingPossibilities(0)))
                            {
                                private clsCharacter ch = Adventure.htblCharacters(itm.MatchingPossibilities(0));
                                Adventure.lCharMentionedThisTurn(ch.Gender).Add(ch.Key);
                            }
                        }
                    Next;
                }
            }
        Next;

        Dim sReferenceKeys(InReferences.Length - 1) As String
        Dim sReferenceCommands(InReferences.Length - 1) As String
        bPass = ExecuteSubTasks(sTaskKey, bCalledFromEvent, bChildTask, InReferences, 0, sReferenceKeys, sReferenceCommands, bTaskHasOutputNew, bPassingOnly)

        If bPassingOnly && ! bPass Then Return false

        if (Not bChildTask || bEvaluateResponses)
        {
            ' Create htblResponses from everything in htblResponsesPass, plus anything from ResponsesFail that isn't in ResponsesPass
            private New OrderedHashTable htblResponses;
            For Each sKey As String In htblResponsesPass.OrderedKeys
                htblResponses.Add(sKey, htblResponsesPass(sKey));
            Next;

            if (htblResponsesPass.Count = 0 && htblResponsesFail.Count >= 0 && task.FailOverride.ToString <> "" && ContainsWord(sInput, "all"))
            {
                Display(task.FailOverride.ToString);
            Else

                ' Get A, B, C from D
                ' Pass: You pick up A, B from D
                ' Fail: You can't get A from D
                ' Fail: B, C is too heavy to pick up
                ' Need: You pick up A, B from D.  C is too heavy to pick up.

                ' TODO - We could have responses from different tasks here.  We need to only match references from the same parent task
                ' So, for example, if we have an action that executes a different task with a different number of references, the different
                ' tasks should not cancel each other out.
                ' E.g. Stand on current object.  Has no references, but executes task as action which has a parameter

                ' So look at every reference combination in each failure message.  If we don't have that combination in our pass set then add it
                For Each sFailMessage As String In htblResponsesFail.OrderedKeys
                    Dim refsFail() As clsNewReference = CType(htblResponsesFail(sFailMessage), clsNewReference())
                    private bool bAllMatch = false;

                    For Each sPassMessage As String In htblResponsesPass.OrderedKeys
                        Dim refsPass() As clsNewReference = CType(htblResponsesPass(sPassMessage), clsNewReference())
                        bAllMatch = True

                        for (int iRef = 0; iRef <= refsFail.Length - 1; iRef++)
                        {
                            private clsNewReference refFail = refsFail(iRef);
                            If refFail == null Then ' I think this is where our task could be executing a different task, with different references...
                                bAllMatch = False
                            Else
                                for (int iItm = 0; iItm <= refFail.Items.Count - 1; iItm++)
                                {
                                    private clsSingleItem itmFail = refFail.Items(iItm);
                                    ' There should only be one matching possibility here, so no need to iterate them

                                    ' resPass(iRef) Is Nothing - think this may also be different task/different refs issue...
                                    ' Should only do this if it is for the same parent task.  If we execute a sub-task, that should become the parent
                                    if (refsPass.Length <= iRef || refsPass(iRef) Is null || refsPass(iRef).Items.Count <= iItm || refsPass(iRef).Items(iItm).MatchingPossibilities(0) <> itmFail.MatchingPossibilities(0))
                                    {
                                        ' This fail is different from this pass
                                        bAllMatch = False
                                        GoTo NextPassMessage;
                                    }

                                Next iItm;
                            }
                        Next iRef;
NextPassMessage:;
                        If bAllMatch Then Exit For
                    Next sPassMessage;

                    if (Not bAllMatch)
                    {
                        ' We've found a failure message that didn't match any pass messages
                        if (htblResponses.ContainsKey(sFailMessage))
                        {
                            ' Need to add refs for this message into the message
                            ' TODO
                            sFailMessage = sFailMessage
                        Else
                            ' Need to add this message
                            htblResponses.Add(sFailMessage, htblResponsesFail(sFailMessage));
                        }
                    }

                Next sFailMessage;

                'If task.eDisplayCompletion = clsTask.BeforeAfterEnum.Before Then states.SetLastState()

                For Each sMessage As String In htblResponses.OrderedKeys
                    private clsNewReference[] refs = CType(htblResponses(sMessage), clsNewReference());

                    NewReferences = refs
                    Display(sMessage);
                Next;

                'If task.eDisplayCompletion = clsTask.BeforeAfterEnum.Before Then states.SetCurrentState()

            }

            if (bEvaluateResponses)
            {
                htblResponsesPass.Clear();
                htblResponsesFail.Clear();
            }
        }

        if (bPass)
        {
            ' Need to remember htblResponses, as they'll be cleared out if events run tasks...
            'Dim htblResponsesPassCopy As OrderedHashTable = htblResponsesPass.Clone
            'Dim htblResponsesFailCopy As OrderedHashTable = htblResponsesFail.Clone
            'Dim bTaskHasOutputTemp2 As Boolean = bTaskHasOutput

            'If Not task.Completed Then
            ' Check any walks/events to see if anything triggers on this task completing
            For Each c As clsCharacter In Adventure.htblCharacters.Values
                For Each w As clsWalk In c.arlWalks
                    For Each ctrl As EventOrWalkControl In w.WalkControls
                        if (ctrl.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.Completion && ctrl.sTaskKey = task.Key)
                        {
                            ' If a child task of the current task has affected the walk control, ignore this as a trigger
                            if (w.sTriggeringTask = "" || Not task.Children(true).Contains(w.sTriggeringTask))
                            {
                                switch (ctrl.eControl)
                                {
                                    case EventOrWalkControl.ControlEnum.Resume:
                                        {
                                        'If w.Status = clsWalk.StatusEnum.Paused Then
                                        w.Resume();
                                    case EventOrWalkControl.ControlEnum.Start:
                                        {
                                        'If w.Status <> clsWalk.StatusEnum.Running Then
                                        w.Start();
                                    case EventOrWalkControl.ControlEnum.Stop:
                                        {
                                        'If w.Status = clsWalk.StatusEnum.Running Then
                                        w.Stop();
                                    case EventOrWalkControl.ControlEnum.Suspend:
                                        {
                                        'If w.Status = clsWalk.StatusEnum.Running Then
                                        w.Pause();
                                }
                                w.sTriggeringTask = task.Key;
                            }
                        }
                    Next;
                Next;
            Next;
            For Each e As clsEvent In Adventure.htblEvents.Values
                For Each ctrl As EventOrWalkControl In e.EventControls
                    if (ctrl.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.Completion && ctrl.sTaskKey = task.Key)
                    {
                        ' If a child task of the current task has affected the walk control, ignore this as a trigger
                        if (e.sTriggeringTask = "" || Not task.Children(true).Contains(e.sTriggeringTask))
                        {
                            switch (ctrl.eControl)
                            {
                                case EventOrWalkControl.ControlEnum.Resume:
                                    {
                                    'If e.Status = clsWalk.StatusEnum.Paused Then
                                    e.Resume();
                                case EventOrWalkControl.ControlEnum.Start:
                                    {
                                    'If e.Status <> clsWalk.StatusEnum.Running Then
                                    e.Start();
                                case EventOrWalkControl.ControlEnum.Stop:
                                    {
                                    'If e.Status = clsWalk.StatusEnum.Running Then
                                    e.Stop();
                                case EventOrWalkControl.ControlEnum.Suspend:
                                    {
                                    'If e.Status = clsWalk.StatusEnum.Running Then
                                    e.Pause();
                            }
                            e.sTriggeringTask = task.Key;
                        }
                    }
                Next;
            Next;
            'End If

            '' Now mark task as completed before executing actions, so any sub tasks that check this task's Completed status will work as per v4
            ''task.Completed = True

            'bTaskHasOutput = bTaskHasOutputTemp2
            'htblResponsesPass = htblResponsesPassCopy
            'htblResponsesFail = htblResponsesFailCopy

        }


        if (Not bCalledFromEvent)
        {
            if (task.ContinueToExecuteLowerPriority)
            {
                bContinue = True
                DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Continuing trying to execute lower priority tasks (multiple matches)");
            Else
                if (bPass)
                {
                    if (bTaskHasOutputNew)
                    {
                        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Task passes and has output.  Will not execute lower priority tasks");
                    Else
                        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Task passes but has no output, therefore will continue to execute lower priority tasks");
                        bContinue = True
                    }
                Else
                    if (Adventure.TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityPassingTask)
                    {
                        if (bTaskHasOutputNew)
                        {
                            DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Task fails but has output.  Will continue trying execute lower priority tasks");
                            ' Hmm, we actually want to execute lower priority tasks EXCEPT library ones...
                            bContinue = True
                        Else
                            DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Task does not pass and also has no output, therefore will continue to execute lower priority tasks");
                            bContinue = True
                        }
                    Else
                        if (Not bTaskHasOutputNew)
                        {
                            DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Task does not pass and also has no output, therefore will continue to execute lower priority tasks");
                            bContinue = True
                        }
                    }
                }
            }

            if (bContinue && Not bChildTask)
            {
                UpdateStatusBar();
                'DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Continuing trying to execute lower priority tasks")
                EvaluateInput(task.Priority + 1, ! bPass && bTaskHasOutputNew);
            }
        }

        If htblResponsesPassTemp != null Then htblResponsesPass = htblResponsesPassTemp.Clone
        If htblResponsesFailTemp != null Then htblResponsesFail = htblResponsesFailTemp.Clone

        return bPass;
        ' Testing: ? task.Key & ", " & bTaskHasOutputNew

    }


    ' Returns True if the SubTask was completed successfully
    public String, ByVal bCalledFromEvent As Boolean, ByVal bChildTask As Boolean, ByVal sReferenceCommands[] AttemptToExecuteSubTask(string sTaskKey, sReferences()
    {

        try
        {
            AttemptToExecuteSubTask = False

            private clsTask task = Adventure.htblTasks(sTaskKey);
            'NewReferences = task.CopyNewRefs(task.NewReferencesWorking)

            ReDim NewReferences(sReferences.Length - 1);

            if (sReferences.Length = 0)
            {
                DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Checking reference free task " + task.Description);
            Else
                private string sSubTask = ParentTaskCommand(task) '.arlCommands(0);
                for (int iRef = 0; iRef <= task.References.Count - 1; iRef++)
                {
                    'If sReferences(iRef) <> "" Then
                    sSubTask = sSubTask.Replace(task.References(iRef), Adventure.GetNameFromKey(sReferences(iRef), False, False))
                    ' Set References to be ones from this particular subtask
                    private clsNewReference r = null ' clsReferences;
                    switch (task.References(iRef))
                    {
                        case "%object%":
                        case "%object1%":
                        case "%object2%":
                        case "%object3%":
                        case "%object4%":
                        case "%object5%":
                        case "%objects%":
                            {
                            r = New clsNewReference(ReferencesType.Object)
                        case "%character%":
                        case "%character1%":
                        case "%character2%":
                        case "%character3%":
                        case "%character4%":
                        case "%character5%":
                        case "%characters%":
                            {
                            r = New clsNewReference(ReferencesType.Character)
                        case "%location%":
                        case "%location1%":
                        case "%location2":
                        case "%location3":
                        case "%location4":
                        case "%location5":
                            {
                            r = New clsNewReference(ReferencesType.Location)
                        case "%item%":
                        case "%item1%":
                        case "%item2%":
                        case "%item3%":
                        case "%item4%":
                        case "%item5%":
                            {
                            r = New clsNewReference(ReferencesType.Item)
                        case "%direction%":
                        case "%direction1%":
                        case "%direction2%":
                        case "%direction3%":
                        case "%direction4%":
                        case "%direction5%":
                            {
                            r = New clsNewReference(ReferencesType.Direction)
                        case "%text%":
                        case "%text1%":
                        case "%text2%":
                        case "%text3%":
                        case "%text4%":
                        case "%text5%":
                            {
                            r = New clsNewReference(ReferencesType.Text)
                        case "%number%":
                        case "%number1%":
                        case "%number2%":
                        case "%number3%":
                        case "%number4%":
                        case "%number5%":
                            {
                            r = New clsNewReference(ReferencesType.Number)
                        default:
                            {
                            ErrMsg("To do");
                    }
                    r.sParentTask = sTaskKey;
                    if (sReferences(iRef) <> "")
                    {
                        private New clsSingleItem ' SingleRef itm;
                        itm.MatchingPossibilities.Add(sReferences(iRef));
                        'ReDim r.Items(0)
                        'r.Items(0) = (itm)
                        'If task.NewReferencesWorking IsNot Nothing AndAlso task.NewReferencesWorking.Length >= iRef AndAlso task.NewReferencesWorking(iRef).Items.Count > 0 AndAlso task.NewReferencesWorking(iRef).Items(0).sCommandReference = "all" Then itm.sCommandReference = "all"
                        itm.sCommandReference = sReferenceCommands(iRef);
                        r.Items.Add(itm);
                        r.ReferenceMatch = task.References(iRef).Replace("%", "");
                        'r.bMultiple = True
                    }
                    if (task.arlCommands.Count > 0)
                    {
                        private StringArrayList sRefs = task.RefsInCommand(task.arlCommands(0));
                        switch (task.References(iRef))
                        {
                            case "%objects%":
                            case "%object1%":
                            case "%object2%":
                            case "%object3%":
                            case "%object4%":
                            case "%object5%":
                            case "%characters%":
                            case "%character1%":
                            case "%character2%":
                            case "%character3%":
                            case "%character4%":
                            case "%character5%":
                            case "%direction1%":
                            case "%direction2%":
                            case "%direction3%":
                            case "%direction4%":
                            case "%direction5%":
                            case "%number1%":
                            case "%number2%":
                            case "%number3%":
                            case "%number4%":
                            case "%number5%":
                            case "%text1%":
                            case "%text2%":
                            case "%text3%":
                            case "%text4%":
                            case "%text5%":
                            case "%item1%":
                            case "%item2%":
                            case "%item3%":
                            case "%item4%":
                            case "%item5%":
                                {
                                for (int iRef2 = 0; iRef2 <= sRefs.Count - 1; iRef2++)
                                {
                                    if (task.References(iRef) = sRefs(iRef2))
                                    {
                                        r.Index = iRef2;
                                        Exit For;
                                    }
                                Next;
                        }
                    }
                    NewReferences(iRef) = r;
                Next;
                DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Checking " + CStr(IIf(sReferences.Length = 1, "single", IIf(sReferences.Length = 2, "double", "triple or more"))) + " reference task " + sSubTask);
            }

            private string sMessage = "";
            private bool bPass = PassRestrictions(task.arlRestrictions, , task);
            private bool bOutputMessages = false;

            if (bPass)
            {
                DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Passed Restrictions");

                ' Remove any failing refs if this ref passes
                For Each sFailMessage As String In htblResponsesFail.OrderedKeys
                    Dim refsFail() As clsNewReference = CType(htblResponsesFail(sFailMessage), clsNewReference())

                    for (int iRef = 0; iRef <= refsFail.Length - 1; iRef++)
                    {
                        private clsNewReference refFail = refsFail(iRef);
                        if (refFail IsNot null)
                        {
                            for (int iItm = 0; iItm <= refFail.Items.Count - 1; iItm++)
                            {
                                private clsSingleItem itmFail = refFail.Items(iItm);
                                ' There should only be one matching possibility here, so no need to iterate them
                                if (NewReferences(iRef) Is null || NewReferences(iRef).Items(iItm).MatchingPossibilities(0) <> itmFail.MatchingPossibilities(0))
                                {
                                    ' This fail is different from this pass
                                    GoTo NextMessage;
                                }
                            Next iItm;
                        }
                    Next iRef;

                    ' Ok, lets remove the failed one
                    htblResponsesFail.Remove(sFailMessage);
                    Exit For ' There should only be one matching the refs, so bomb out so we don't cause problem iterating loop;
NextMessage:;
                Next sFailMessage;

                ' If this task contains references, see if we have a specific task
                If task.Children.Count > 0 Then DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Checking whether any of our child tasks should override...")
                'Dim bOverridden As Boolean = False

                private bool bShouldParentOutputText = true;
                private bool bShouldParentExecuteTasks = true;
                'Dim bAnyMatchingChildTasks As Boolean = False
                private bool bAnyChildHasOutput = false;
                private New List<string> lAfterChildren;


                For Each sChildTask As String In task.Children ' in order or priority...
                    private clsTask tasChild = Adventure.htblTasks(sChildTask);
                    if (task.TaskType = clsTask.TaskTypeEnum.Specific)
                    {
                        ' If our parent is a Specific task, we may need to pad our Specifics out with any that have been dropped
                        if (task.Specifics.Length <> tasChild.Specifics.Length)
                        {
                            ' TODO - These may be in the wrong order if we're matching on a secondary command!
                            Dim newSpecs(task.Specifics.Length - 1) As clsTask.Specific
                            private int iChild = 0;
                            for (int i = 0; i <= task.Specifics.Length - 1; i++)
                            {
                                newSpecs(i) = task.Specifics(i);
                                if (newSpecs(i).Keys.Count = 0 || (newSpecs(i).Keys.Count = 1 && newSpecs(i).Keys(0) = ""))
                                {
                                    newSpecs(i) = tasChild.Specifics(iChild);
                                    iChild += 1;
                                }
                            Next;
                            tasChild.Specifics = newSpecs;
                        }
                    }
                    private int iMatchCount = 0;

                    If RefsMatchSpecifics(tasChild) Then ' This should remove the ref so it doesn't get processed when we execute the main task

                        'If Not bAnyMatchingChildTasks Then
                        '    bAnyMatchingChildTasks = True
                        '    bShouldParentOutputText = False ' Change the default to False if we have any child tasks
                        '    bShouldParentExecuteTasks = False
                        'End If
                        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Overriding child task found: " + tasChild.Description);


                        switch (tasChild.SpecificOverrideType)
                        {
                            case clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly:
                            case clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions:
                            case clsTask.SpecificOverrideTypeEnum.BeforeTextOnly:
                            case clsTask.SpecificOverrideTypeEnum.Override:
                                {

                                iDebugIndent += 1;
                                private bool bContinueExecutingTasks = false;

                                if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.Override)
                                {
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Override Parent");
                                Else
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Run Before Parent");
                                }

                                ' Make a note of how many failing responses we have, so we know if this task has failing output
                                private int iFailRefsBefore = 0;
                                if (task.References.Count = 0)
                                {
                                    iFailRefsBefore += htblResponsesFail.Count
                                Else
                                    For Each responses() As clsNewReference In htblResponsesFail.Values
                                        For Each response As clsNewReference In responses
                                            iFailRefsBefore += response.Items.Count
                                        Next;
                                    Next;
                                }


                                private bool bChildTaskHasOutput = false;
                                if (AttemptToExecuteTask(tasChild.Key, bCalledFromEvent, , true, bContinueExecutingTasks, bChildTaskHasOutput, , bAnyChildHasOutput))
                                {
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task passes");
                                    'bOverridden = True
                                    if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions Then ' tasChild.ExecuteParentActions)
                                    {
                                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Execute Parent actions...");
                                        'bOverridden = False
                                        'bShouldParentExecuteTasks = True
                                    Else
                                        bShouldParentExecuteTasks = False
                                    }
                                    if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextOnly)
                                    {
                                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Output Parent text...");
                                        'bShouldParentOutputText = True
                                        ' If we don't continue on text output, we need to look at the parent text
                                        ' Hmm, this means any sibling child task with output after one with no output doesn't get run
                                        'If bContinueExecutingTasks Then
                                        '    Select Case tasChild.ContinueToExecuteLowerPriority
                                        '        Case clsTask.ContinueEnum.ContinueOnNoOutput
                                        '            bContinueExecutingTasks = False  ' We assume the parent has output
                                        '    End Select
                                        'End If
                                    Else
                                        bShouldParentOutputText = False
                                    }
                                    AttemptToExecuteSubTask = True
                                Else
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task fails");
                                    'bShouldParentOutputText = True
                                    'bShouldParentExecuteTasks = True

                                    'If tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions OrElse tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextOnly Then
                                    'Else
                                    'If tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly OrElse tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.Override Then
                                    ' Ok, compare failing output vs what it was before - if we have failing output, this takes precedence over parent if set
                                    private int iFailRefsAfter = 0;
                                    if (task.References.Count = 0)
                                    {
                                        iFailRefsAfter += htblResponsesFail.Count
                                    Else
                                        For Each responses() As clsNewReference In htblResponsesFail.Values
                                            For Each response As clsNewReference In responses
                                                iFailRefsAfter += response.Items.Count
                                            Next;
                                        Next;
                                    }

                                    if (iFailRefsAfter > iFailRefsBefore)
                                    {
                                        'bOverridden = True
                                        if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextOnly || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.Override)
                                        {
                                            bShouldParentOutputText = False
                                        }
                                        if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.Override)
                                        {
                                            bShouldParentExecuteTasks = False
                                        }
                                        bAnyChildHasOutput = True
                                    }
                                    'else
                                    'bShouldParentOutputText = False
                                    'End If

                                }


                                bTaskHasOutputNew = bTaskHasOutputNew OrElse bChildTaskHasOutput ' If the child task has output, ensure we are aware of it
                                iDebugIndent -= 1;

                                if (Not bContinueExecutingTasks)
                                {
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Do not continue executing other child tasks.");
                                    Exit For;
                                Else
                                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Continue executing other child tasks.");
                                }

                            default:
                                {
                                lAfterChildren.Add(tasChild.Key);
                                'bShouldParentOutputText = True
                                'bShouldParentExecuteTasks = True
                                if (tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.AfterActionsOnly || tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.AfterTextOnly)
                                {
                                    ' TODO - Need to check PassRestrictions to see if we need to suppress parent Text or Actions

                                }

                        }

                    }
                Next;

                private string sBeforeActionsMessage = "";
                private int iResponsePosition = -1;
                if (task.eDisplayCompletion = clsTask.BeforeAfterEnum.Before && bShouldParentOutputText)
                {
                    ' We may have already printed these refs out in a child task, so only print them here if we haven't done that
                    If NewReferences.Length > 0 Then PrintOutReferences()
                    sMessage = task.CompletionMessage.ToString
                    If sMessage == null Then sMessage = ""
                    DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, sMessage);
                    ' Replace any functions that could be affected by our actions...
                    ' Can't do this, because %TheObject[%objects%]% will interpret so we get multiple entries if do get all
                    ' But then it means that %ParentOf[%objects%]% will be incorrect if action moves it
                    'sMessage = sMessage.Replace("%ParentOf[", "%PrevParentOf[").Replace("%ListObjectsOn[", "%PrevListObjectsOn[").Replace(".Parent.", ".PrevParent.")

                    ' If we do this then drop all gives us: Ok, I drop X.  Ok, I drop Y.  Ok, I drop Z.
                    UserSession.bDisplaying = true;
                    bTestingOutput = True ' Ensure any DisplayOnce descriptions aren't marked as displayed, as we'll mark them in final output (Display)
                    sBeforeActionsMessage = ReplaceExpressions(ReplaceFunctions(sMessage))
                    bTestingOutput = False
                    If sBeforeActionsMessage = sMessage Then sBeforeActionsMessage = ""
                    UserSession.bDisplaying = false;
                    if (sBeforeActionsMessage = "")
                    {
                        ' It is safe to add the response now
                        If ! task.AggregateOutput Then sMessage = ReplaceExpressions(ReplaceFunctions(sMessage))
                        If AddResponse(bOutputMessages, sMessage, sReferences, true) Then bTaskHasOutputNew = true
                    Else
                        ' The response changes with functions, so we can't add yet until we know whether the actions affect the output
                        private OrderedHashTable htblResponses = CType(IIf(bPass, htblResponsesPass, htblResponsesFail), OrderedHashTable);
                        iResponsePosition = htblResponses.Count
                    }
                }

                task.Completed = true;
                If bShouldParentExecuteTasks Then ExecuteActions(task, bCalledFromEvent, bTaskHasOutputNew)

                if (sBeforeActionsMessage <> "")
                {
                    ' Check to see if the actions had any effect on the message.  If so, add the replaced message.  If not, add the unreplaced message
                    UserSession.bDisplaying = true;
                    bTestingOutput = True
                    If sBeforeActionsMessage <> ReplaceExpressions(ReplaceFunctions(sMessage)) Then sMessage = sBeforeActionsMessage
                    bTestingOutput = False
                    UserSession.bDisplaying = false;
                    If ! task.AggregateOutput Then sMessage = ReplaceExpressions(ReplaceFunctions(sMessage))
                    If AddResponse(bOutputMessages, sMessage, sReferences, true, iResponsePosition) Then bTaskHasOutputNew = true
                }

                if (task.eDisplayCompletion = clsTask.BeforeAfterEnum.After && bShouldParentOutputText)
                {
                    sMessage = task.CompletionMessage.ToString
                    If sMessage == null Then sMessage = ""
                    If ! task.AggregateOutput Then sMessage = ReplaceExpressions(ReplaceFunctions(sMessage))
                    DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, sMessage);
                    If AddResponse(bOutputMessages, sMessage, sReferences, true) Then bTaskHasOutputNew = true
                }


                For Each sChildTask As String In lAfterChildren
                    iDebugIndent += 1;

                    private clsTask tasChild = Adventure.htblTasks(sChildTask);
                    DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Run After Parent");

                    private bool bChildTaskHasOutput = false;
                    private bool bContinueExecutingTasks = false;
                    if (AttemptToExecuteTask(tasChild.Key, bCalledFromEvent, , true, bContinueExecutingTasks, bChildTaskHasOutput))
                    {
                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task passes");
                        AttemptToExecuteSubTask = True
                    Else
                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task fails");
                    }


                    bTaskHasOutputNew = bTaskHasOutputNew OrElse bChildTaskHasOutput ' If the child task has output, ensure we are aware of it
                    iDebugIndent -= 1;
                    if (Not bContinueExecutingTasks)
                    {
                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Do not continue executing other child tasks.");
                        Exit For;
                    Else
                        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Continue executing other child tasks.");
                    }
                Next;

                AttemptToExecuteSubTask = True

            Else
                if (Not bPassingOnly)
                {
                    DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.Medium, "Failed Restrictions");
                    sMessage = sRestrictionText

                    If sMessage == null Then sMessage = ""
                    DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, sMessage);
                    If NewReferences.Length > 0 Then PrintOutReferences()

                    if (task.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.AfterTextAndActions || task.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.AfterTextOnly)
                    {
                        ' Although this is a failing restriction, it is being output _after_ a successful task, so we need to append to the success list :-/
                        If sMessage <> "" Then bPass = true
                    }
                }
            }

            If AddResponse(bOutputMessages, sMessage, sReferences, bPass) Then bTaskHasOutputNew = true

        }
        catch (Exception ex)
        {
            ErrMsg("Error executing subtask " + sTaskKey, ex);
        }

    }



    'Private Sub ExecuteChildTask(ByVal task As clsTask, ByVal tasChild As clsTask, ByVal bCalledFromEvent As Boolean, ByRef bShouldParentOutputText As Boolean, ByRef bShouldParentExecuteTasks As Boolean)

    '    iDebugIndent += 1
    '    Dim bContinueExecutingTasks As Boolean = False
    '    Dim iFailRefsBefore As Integer = 0
    '    If task.References.Count = 0 Then
    '        iFailRefsBefore += htblResponsesFail.Count
    '    Else
    '        For Each responses() As clsNewReference In htblResponsesFail.Values
    '            For Each response As clsNewReference In responses
    '                iFailRefsBefore += response.Items.Count
    '            Next
    '        Next
    '    End If


    '    Dim bChildTaskHasOutput As Boolean = False
    '    If AttemptToExecuteTask(tasChild.Key, bCalledFromEvent, , True, bContinueExecutingTasks, bChildTaskHasOutput) Then
    '        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task passes")
    '        bOverridden = True
    '        If tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly OrElse tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions Then ' tasChild.ExecuteParentActions Then
    '            DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Execute Parent actions...")
    '            'bOverridden = False
    '            bShouldParentExecuteTasks = True
    '        End If
    '        If tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions OrElse tasChild.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextOnly Then
    '            DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Output Parent text...")
    '            bShouldParentOutputText = True
    '        End If
    '        AttemptToExecuteSubTask = True
    '    Else
    '        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.High, "Child task fails")

    '        Dim iFailRefsAfter As Integer = 0
    '        If task.References.Count = 0 Then
    '            iFailRefsAfter += htblResponsesFail.Count
    '        Else
    '            For Each responses() As clsNewReference In htblResponsesFail.Values
    '                For Each response As clsNewReference In responses
    '                    iFailRefsAfter += response.Items.Count
    '                Next
    '            Next
    '        End If

    '        If iFailRefsAfter > iFailRefsBefore Then
    '            bOverridden = True
    '            bShouldParentOutputText = False
    '        End If

    '    End If


    '    bTaskHasOutput = bTaskHasOutput OrElse bChildTaskHasOutput ' If the child task has output, ensure we are aware of it
    '    iDebugIndent -= 1
    '    If Not bContinueExecutingTasks Then
    '        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Do not continue executing other child tasks.")
    '        Exit For
    '    Else
    '        DebugPrint(ItemEnum.Task, tasChild.Key, DebugDetailLevelEnum.Medium, "Continue executing other child tasks.")
    '    End If

    'End Sub


private class clsReponse
    {
        Public sReferences() As String
        public bool bPass;
    }

    'Private Function RefsMatch(ByVal refs1() As clsNewReference, ByVal refs2() As clsNewReference) As Boolean

    'End Function

    ' Determines whether tags in a message correspond to actual output
    private bool bHasOutput(string sText)
    {
        if (sText = "")
        {
            return false;
        Else
            if (StripCarats(sText) = "")
            {
                private string sTextLower = sText.ToLower;
                For Each sValid As String In New String() {"<br>", "<center>", "<centre>", "<i>", "</i>", "<b>", "</b>", "<u>", "</u>", "<c>", "</c>", "<font", "</font>", "<right>", "</right>", "<left>", "</left>", "<del>", "<wait", "<cls>", "<img "}
                    If sTextLower.Contains(sValid) Then Return true
                Next;
                ' Ok, so we have an unknown tag.  But the user may be text replacing it, so check for that
                if (Adventure.htblALRs.ContainsKey(sText))
                {
                    return true;
                Else
                    return false;
                }
            Else
                return true;
            }
        }
    }


    ' Returns True if a response was added, or False otherwise
    private String, ByVal bPass As Boolean, Optional iPosition As Integer = -1) As Boolean AddResponse(ref bOutputMessages As Boolean, string sMessage, sReferences()
    {

        If bOutputMessages || ! bHasOutput(sMessage) Then Return false '  StripCarats(sMessage) = ""
        'If StripCarats(sMessage) = "" Then Exit Sub

        private OrderedHashTable htblResponses = CType(IIf(bPass, htblResponsesPass, htblResponsesFail), OrderedHashTable);

        if (htblResponses.ContainsKey(sMessage))
        {
            ' Add our new references to the ones already there
            for (int iRef = 0; iRef <= sReferences.Length - 1; iRef++)
            {
                if (CType(htblResponses(sMessage), clsNewReference()).Length > iRef && CType(htblResponses(sMessage), clsNewReference())(iRef) IsNot null && Not CType(htblResponses(sMessage), clsNewReference())(iRef).ContainsKey(sReferences(iRef)))
                {
                    'CType(htblResponses(sMessage), clsNewReference())(iRef) IsNot Nothing AndAlso Not - could add this, but reference shouldn't be Nothing...
                    'If CType(htblResponses(sMessage), clsNewReference())(iRef).ContainsKey(sReferences(iRef)) Then
                    (clsNewReference()(htblResponses(sMessage)))(iRef).Items.Add(New clsSingleItem(sReferences(iRef)));
                    'CType(htblResponses(sMessage), clsTask.clsNewReference())(iRef).bMultiple = True
                }
            Next;
        Else
            ' Store our references
            if (iPosition > -1)
            {
                htblResponses.Insert(sMessage, NewReferences, iPosition);
            Else
                htblResponses.Add(sMessage, NewReferences);
            }
            'bTaskHasOutput = True
        }

        bOutputMessages = True
        return true;

    }



    public void UncompleteTask(string sTaskKey)
    {

        Adventure.htblTasks(sTaskKey).Completed = false;
        For Each c As clsCharacter In Adventure.htblCharacters.Values
            For Each w As clsWalk In c.arlWalks
                For Each wc As EventOrWalkControl In w.WalkControls
                    if (wc.sTaskKey = sTaskKey && wc.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.UnCompletion)
                    {
                        switch (wc.eControl)
                        {
                            case EventOrWalkControl.ControlEnum.Start:
                                {
                                If w.Status = clsEvent.StatusEnum.NotYetStarted Then w.Start()
                            case EventOrWalkControl.ControlEnum.Stop:
                                {
                                If w.Status = clsEvent.StatusEnum.Running Then w.Stop()
                            case EventOrWalkControl.ControlEnum.Suspend:
                                {
                                If w.Status = clsEvent.StatusEnum.Running Then w.Pause()
                            case EventOrWalkControl.ControlEnum.Resume:
                                {
                                If w.Status = clsEvent.StatusEnum.Paused Then w.Resume()
                        }
                    }
                Next;
            Next;
        Next;
        For Each e As clsEvent In Adventure.htblEvents.Values
            For Each ec As EventOrWalkControl In e.EventControls
                if (ec.sTaskKey = sTaskKey && ec.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.UnCompletion)
                {
                    switch (ec.eControl)
                    {
                        case EventOrWalkControl.ControlEnum.Start:
                            {
                            If e.Status = clsEvent.StatusEnum.NotYetStarted Then e.Start()
                        case EventOrWalkControl.ControlEnum.Stop:
                            {
                            If e.Status = clsEvent.StatusEnum.Running Then e.Stop()
                        case EventOrWalkControl.ControlEnum.Suspend:
                            {
                            If e.Status = clsEvent.StatusEnum.Running Then e.Pause()
                        case EventOrWalkControl.ControlEnum.Resume:
                            {
                            If e.Status = clsEvent.StatusEnum.Paused Then e.Resume()
                    }
                }
            Next;
        Next;

    }


    ' Return the token that the reference we're looking at is
    private string GetReferenceKey(string sTaskCommand, int iRefNum)
    {

        Dim sTokens() As String = sTaskCommand.Split(" "c)
        private int iWhichRef = 0;
        For Each sToken As String In sTokens
            switch (sToken)
            {
                case "%character1%":
                case "%character2%":
                case "%character3%":
                case "%character4%":
                case "%character5%":
                case "%characters%":
                case "%direction%":
                case "%number%":
                case "%numbers%":
                case "%object1%":
                case "%object2%":
                case "%object3%":
                case "%object4%":
                case "%object5%":
                case "%objects%":
                case "%text%":
                    {
                    iWhichRef += 1;
            }
            if (iWhichRef = iRefNum)
            {
                return sToken.Replace("%object", "ReferencedObject").Replace("%direction", "ReferencedDirection").Replace("%", "");
                Exit For;
            }
        Next;

        return "";

    }



    private void ExecuteSingleAction(clsAction actx, string sTaskCommand, clsTask task, bool bCalledFromEvent = false, ref bTaskHasOutputNew As Boolean = False)
    {

        try
        {

            private clsAction act = actx.Copy;

            ' Replace references
            switch (act.sKey1)
            {
                case "ReferencedObject":
                case "ReferencedObject1":
                case "ReferencedObject2":
                case "ReferencedObject3":
                case "ReferencedObject4":
                case "ReferencedObject5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "ReferencedObjects":
                    {
                    for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
                    {
                        With NewReferences(iRef);
                            if (.Items.Count > 0 && .ReferenceType = ReferencesType.Object && GetReferenceKey(sTaskCommand, iRef + 1) = "ReferencedObjects")
                            {
                                for (int iOb = 0; iOb <= .Items.Count - 1 ' alRefs.Count - 1; iOb++)
                                {
                                    if (.Items(iOb).MatchingPossibilities.Count > 0)
                                    {
                                        act.sKey1 = .Items(iOb).MatchingPossibilities(0);
                                        ExecuteSingleAction(act, sTaskCommand, task, bTaskHasOutputNew);
                                    }
                                Next;
                            }
                        }
                    Next;
                    Exit Sub;
                case "ReferencedDirection":
                case "ReferencedDirection1":
                case "ReferencedDirection2":
                case "ReferencedDirection3":
                case "ReferencedDirection4":
                case "ReferencedDirection5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "ReferencedCharacter":
                case "ReferencedCharacter1":
                case "ReferencedCharacter2":
                case "ReferencedCharacter3":
                case "ReferencedCharacter4":
                case "ReferencedCharacter5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "ReferencedLocation":
                case "ReferencedLocation1":
                case "ReferencedLocation2":
                case "ReferencedLocation3":
                case "ReferencedLocation4":
                case "ReferencedLocation5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "ReferencedItem":
                case "ReferencedItem1":
                case "ReferencedItem2":
                case "ReferencedItem3":
                case "ReferencedItem4":
                case "ReferencedItem5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "ReferencedNumber":
                case "ReferencedNumber1":
                case "ReferencedNumber2":
                case "ReferencedNumber3":
                case "ReferencedNumber4":
                case "ReferencedNumber5":
                    {
                    act.sKey1 = GetReference(act.sKey1);
                case "%Player%":
                    {
                    act.sKey1 = Adventure.Player.Key;

            }
            switch (act.sKey2)
            {
                case "ReferencedObject":
                case "ReferencedObject1":
                case "ReferencedObject2":
                case "ReferencedObject3":
                case "ReferencedObject4":
                case "ReferencedObject5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "ReferencedObjects":
                    {
                    for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
                    {
                        With NewReferences(iRef);
                            if (.Items.Count > 0 && .ReferenceType = ReferencesType.Object && GetReferenceKey(sTaskCommand, iRef) = "ReferencedObjects")
                            {
                                for (int iOb = 0; iOb <= .Items.Count - 1 ' alRefs.Count - 1; iOb++)
                                {
                                    if (.Items(iOb).MatchingPossibilities.Count > 0)
                                    {
                                        act.sKey2 = .Items(iOb).MatchingPossibilities(0);
                                        ExecuteSingleAction(act, sTaskCommand, task, bTaskHasOutputNew);
                                    }
                                Next;
                            }
                        }
                    Next;
                    Exit Sub;
                case "ReferencedDirection":
                case "ReferencedDirection1":
                case "ReferencedDirection2":
                case "ReferencedDirection3":
                case "ReferencedDirection4":
                case "ReferencedDirection5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "ReferencedCharacter":
                case "ReferencedCharacter1":
                case "ReferencedCharacter2":
                case "ReferencedCharacter3":
                case "ReferencedCharacter4":
                case "ReferencedCharacter5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "ReferencedLocation":
                case "ReferencedLocation1":
                case "ReferencedLocation2":
                case "ReferencedLocation3":
                case "ReferencedLocation4":
                case "ReferencedLocation5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "ReferencedItem":
                case "ReferencedItem1":
                case "ReferencedItem2":
                case "ReferencedItem3":
                case "ReferencedItem4":
                case "ReferencedItem5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "ReferencedNumber":
                case "ReferencedNumber1":
                case "ReferencedNumber2":
                case "ReferencedNumber3":
                case "ReferencedNumber4":
                case "ReferencedNumber5":
                    {
                    act.sKey2 = GetReference(act.sKey2);
                case "%Player%":
                    {
                    act.sKey2 = Adventure.Player.Key;

            }

            for (int i = 0; i <= 5; i++)
            {
                private int iRef = i;
                If iRef > 0 Then iRef -= 1

                'Dim sNumber As String = "%number" & If(i > 0, i.ToString, "").ToString & "%"
                'If act.StringValue.Contains(sNumber) Then act.StringValue = act.StringValue.Replace(sNumber, Adventure.iReferencedNumber(iRef).ToString)

                private string sRefText = "%text" + If(i > 0, i.ToString, "").ToString + "%";
                if (act.eItem = clsAction.ItemEnum.Conversation && act.StringValue = sRefText)
                {
                    act.StringValue = Adventure.sReferencedText(iRef);
                }
            Next;


            private bool bBadKeys = false;
            switch (act.eItem)
            {
                case clsAction.ItemEnum.EndGame:
                case clsAction.ItemEnum.Time:
                    {
                case clsAction.ItemEnum.SetVariable:
                case clsAction.ItemEnum.IncreaseVariable:
                case clsAction.ItemEnum.DecreaseVariable:
                case clsAction.ItemEnum.SetTasks:
                case clsAction.ItemEnum.Conversation:
                    {
                    If act.sKey1 == null Then bBadKeys = true
                default:
                    {
                    If act.sKey1 == null || act.sKey2 == null Then bBadKeys = true
            }
            if (bBadKeys)
            {
                DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Bad key(s) for action ");
                Exit Sub;
            }

            switch (act.eItem)
            {
                case clsAction.ItemEnum.MoveObject:
                case clsAction.ItemEnum.AddObjectToGroup:
                case clsAction.ItemEnum.RemoveObjectFromGroup:
                    {
                    private ObjectHashTable obs = null;

                    switch (act.eMoveObjectWhat)
                    {
                        case clsAction.MoveObjectWhatEnum.Object:
                            {
                            obs = New ObjectHashTable
                            obs.Add(Adventure.htblObjects(act.sKey1), act.sKey1);
                        case clsAction.MoveObjectWhatEnum.EverythingAtLocation:
                            {
                            obs = New ObjectHashTable
                            For Each ob As clsObject In Adventure.htblObjects.Values
                                if (Not ob.IsStatic && ob.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation && ob.Location.Key = act.sKey1)
                                {
                                    obs.Add(ob, ob.Key);
                                }
                            Next;
                        case clsAction.MoveObjectWhatEnum.EverythingHeldBy:
                            {
                            obs = Adventure.htblCharacters(act.sKey1).HeldObjects
                        case clsAction.MoveObjectWhatEnum.EverythingInGroup:
                            {
                            obs = New ObjectHashTable
                            For Each sKey As String In Adventure.htblGroups(act.sKey1).arlMembers
                                obs.Add(Adventure.htblObjects(sKey), sKey);
                            Next;
                        case clsAction.MoveObjectWhatEnum.EverythingInside:
                            {
                            obs = Adventure.htblObjects(act.sKey1).Children(clsObject.WhereChildrenEnum.InsideObject)
                        case clsAction.MoveObjectWhatEnum.EverythingOn:
                            {
                            obs = Adventure.htblObjects(act.sKey1).Children(clsObject.WhereChildrenEnum.OnObject)
                        case clsAction.MoveObjectWhatEnum.EverythingWithProperty:
                            {
                            obs = New ObjectHashTable
                            private clsProperty prop = Adventure.htblObjectProperties(act.sKey1);
                            For Each ob As clsObject In Adventure.htblObjects.Values
                                if (ob.HasProperty(prop.Key))
                                {
                                    if (prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                    {
                                        obs.Add(ob, ob.Key);
                                    Else
                                        If ob.GetPropertyValue(prop.Key) = act.sPropertyValue Then obs.Add(ob, ob.Key)
                                    }
                                }
                            Next;
                        case clsAction.MoveObjectWhatEnum.EverythingWornBy:
                            {
                            obs = Adventure.htblCharacters(act.sKey1).WornObjects
                    }

                    'Select Case act.sKey1
                    '    Case ALLHELDOBJECTS
                    '        obs = Adventure.Player.HeldObjects
                    '    Case ALLWORNOBJECTS
                    '        obs = Adventure.Player.WornObjects
                    '    Case Else
                    '        obs = New ObjectHashTable
                    '        Dim ob As clsObject = Adventure.htblObjects(act.sKey1)
                    '        obs.Add(ob, ob.Key)
                    'End Select
                    if (obs IsNot null)
                    {
                        For Each ob As clsObject In obs.Values

                            switch (act.eItem)
                            {
                                case clsAction.ItemEnum.MoveObject:
                                    {
                                    private New clsObjectLocation dest;
                                    switch (act.eMoveObjectTo)
                                    {
                                        case clsAction.MoveObjectToEnum.InsideObject:
                                            {
                                            ' Make sure we're not moving inside and object inside ourselves
                                            'If Adventure.htblObjects.ContainsKey(act.sKey2) Then
                                            '    If ob.Key = act.sKey2 OrElse ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).ContainsKey(act.sKey2) Then ' Adventure.htblObjects(act.sKey2).ChildrenInside(True).ContainsKey(ob.Key) Then
                                            '        DisplayError("Recursive object container relationship")
                                            '        Exit Sub
                                            '    Else
                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject;
                                            dest.Key = act.sKey2;
                                            '    End If
                                            'End If
                                        case clsAction.MoveObjectToEnum.OntoObject:
                                            {
                                            'If ob.Key = act.sKey2 OrElse ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).ContainsKey(act.sKey2) Then
                                            '    DisplayError("Recursive object surface relationship")
                                            '    Exit Sub
                                            'Else
                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject;
                                            dest.Key = act.sKey2;
                                            'End If

                                        case clsAction.MoveObjectToEnum.ToCarriedBy:
                                            {
                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter;
                                            dest.Key = act.sKey2;

                                        case clsAction.MoveObjectToEnum.ToLocation:
                                            {
                                            if (ob.IsStatic)
                                            {
                                                If act.sKey2 = "Hidden" Then ' i.e. Nowhere
                                                    dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                                                Else
                                                    dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
                                                    dest.Key = act.sKey2;
                                                }
                                            Else
                                                if (act.sKey2 = "Hidden")
                                                {
                                                    dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                                Else
                                                    dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                    dest.Key = act.sKey2;
                                                }
                                            }


                                        case clsAction.MoveObjectToEnum.ToPartOfCharacter:
                                            {
                                            dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter;
                                            dest.Key = act.sKey2;

                                        case clsAction.MoveObjectToEnum.ToPartOfObject:
                                            {
                                            dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject;
                                            dest.Key = act.sKey2;

                                        case clsAction.MoveObjectToEnum.ToLocationGroup:
                                            {
                                            if (ob.IsStatic)
                                            {
                                                dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.LocationGroup;
                                                dest.Key = act.sKey2;
                                            Else
                                                ' Need to select one room at random
                                                private clsGroup group = Adventure.htblGroups(act.sKey2);
                                                'Dim iLocation As Integer = CInt(Rnd() * group.arlMembers.Count)
                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                dest.Key = group.RandomKey ' group.arlMembers(iLocation);
                                            }

                                        case clsAction.MoveObjectToEnum.ToSameLocationAs:
                                            {
                                            if (ob.IsStatic)
                                            {
                                                if (Adventure.htblCharacters.ContainsKey(act.sKey2))
                                                {
                                                    ' Move Static ob to same location as a character
                                                    private clsCharacter chDest = Adventure.htblCharacters(act.sKey2);
                                                    if (chDest.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
                                                    {
                                                        dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
                                                        dest.Key = chDest.Location.Key;
                                                    ElseIf chDest.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden Then
                                                        dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                                                    }
                                                ElseIf Adventure.htblObjects.ContainsKey(act.sKey2) Then
                                                    ' Move Static ob to same location as an object
                                                    private clsObject obDest = Adventure.htblObjects(act.sKey2);
                                                    if (obDest.IsStatic)
                                                    {
                                                        dest = obDest.Location.Copy
                                                    Else
                                                        if (obDest.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden)
                                                        {
                                                            dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                                                        Else
                                                            dest.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.SingleLocation;
                                                            For Each loc As clsLocation In obDest.LocationRoots.Values
                                                                dest.Key = loc.Key;
                                                                Exit For;
                                                            Next;
                                                        }
                                                    }
                                                Else
                                                    ErrMsg("Cannot move object to same location as " + act.sKey2 + " - key not found!");
                                                }

                                            Else
                                                if (Adventure.htblCharacters.ContainsKey(act.sKey2))
                                                {
                                                    ' Move dynamic ob to same location as a character
                                                    private clsCharacter chDest = Adventure.htblCharacters(act.sKey2);

                                                    switch (chDest.Location.ExistWhere)
                                                    {
                                                        case clsCharacterLocation.ExistsWhereEnum.AtLocation:
                                                            {
                                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                            dest.Key = chDest.Location.Key;
                                                        case clsCharacterLocation.ExistsWhereEnum.Hidden:
                                                            {
                                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                                        case clsCharacterLocation.ExistsWhereEnum.InObject:
                                                            {
                                                            ' Move the object inside the object we're in - but only if it allows dynamic objects
                                                            private clsObject obDest = Adventure.htblObjects(chDest.Location.Key);
                                                            if (obDest.IsContainer)
                                                            {
                                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject;
                                                                dest.Key = chDest.Location.Key;
                                                            Else
                                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                                dest.Key = chDest.Location.LocationKey;
                                                            }
                                                        case clsCharacterLocation.ExistsWhereEnum.OnCharacter:
                                                            {
                                                            ' Move to the location that the character is at
                                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                            dest.Key = chDest.Location.LocationKey;
                                                        case clsCharacterLocation.ExistsWhereEnum.OnObject:
                                                            {
                                                            ' Move the object onto the object we're on - but only if it allows dynamic objects
                                                            private clsObject obDest = Adventure.htblObjects(chDest.Location.Key);
                                                            if (obDest.HasSurface)
                                                            {
                                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject;
                                                                dest.Key = chDest.Location.Key;
                                                            Else
                                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                                dest.Key = chDest.Location.LocationKey;
                                                            }
                                                    }

                                                ElseIf Adventure.htblObjects.ContainsKey(act.sKey2) Then
                                                    private clsObject obDest = Adventure.htblObjects(act.sKey2);

                                                    if (obDest.IsStatic)
                                                    {
                                                        ' If the static object exists in more than one place, pick a random location
                                                        switch (obDest.Location.StaticExistWhere)
                                                        {
                                                            case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                                                                {
                                                                TODO("Move dynamic object to one of all rooms");
                                                            case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                                                                {
                                                                TODO("Move dynamic object to one of a location group");
                                                            case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                                                                {
                                                                obDest.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                                            case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                                                                {
                                                                TODO("Move dynamic object to same room as character");
                                                            case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                                                                {
                                                                TODO("Move dynamic object to same room as part of object");
                                                            case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                                                                {
                                                                dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                                                dest.Key = obDest.Location.Key;
                                                        }
                                                    Else
                                                        if (obDest.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden)
                                                        {
                                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                                        Else
                                                            dest = obDest.Location.Copy
                                                        }
                                                    }
                                                Else
                                                    ErrMsg("Cannot move object to same location as " + act.sKey2 + " - key not found!");
                                                }

                                            }

                                        case clsAction.MoveObjectToEnum.ToWornBy:
                                            {
                                            dest.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter;
                                            dest.Key = act.sKey2;

                                    }

                                    ob.Move(dest);

                                case clsAction.ItemEnum.AddObjectToGroup:
                                    {
                                    If ! Adventure.htblGroups(act.sKey2).arlMembers.Contains(ob.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Add(ob.Key)
                                    'ob.bCalculatedGroups = False
                                    ob.ResetInherited();
                                case clsAction.ItemEnum.RemoveObjectFromGroup:
                                    {
                                    If Adventure.htblGroups(act.sKey2).arlMembers.Contains(ob.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Remove(ob.Key)
                                    'ob.bCalculatedGroups = False
                                    ob.ResetInherited();
                            }

                        Next;
                    }

                case clsAction.ItemEnum.MoveCharacter:
                case clsAction.ItemEnum.AddCharacterToGroup:
                case clsAction.ItemEnum.RemoveCharacterFromGroup:
                    {
                    private CharacterHashTable chars = null;

                    switch (act.eMoveCharacterWho)
                    {
                        case clsAction.MoveCharacterWhoEnum.Character:
                            {
                            chars = New CharacterHashTable
                            chars.Add(Adventure.htblCharacters(act.sKey1), act.sKey1);
                        case clsAction.MoveCharacterWhoEnum.EveryoneAtLocation:
                            {
                            chars = Adventure.htblLocations(act.sKey1).CharactersDirectlyInLocation(True)
                        case clsAction.MoveCharacterWhoEnum.EveryoneInGroup:
                            {
                            chars = New CharacterHashTable
                            For Each sKey As String In Adventure.htblGroups(act.sKey1).arlMembers
                                chars.Add(Adventure.htblCharacters(sKey), sKey);
                            Next;
                        case clsAction.MoveCharacterWhoEnum.EveryoneInside:
                            {
                            chars = Adventure.htblObjects(act.sKey1).ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject)
                        case clsAction.MoveCharacterWhoEnum.EveryoneOn:
                            {
                            chars = Adventure.htblObjects(act.sKey1).ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject)
                        case clsAction.MoveCharacterWhoEnum.EveryoneWithProperty:
                            {
                            chars = New CharacterHashTable
                            private clsProperty prop = Adventure.htblCharacterProperties(act.sKey1);
                            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                                if (ch.HasProperty(prop.Key))
                                {
                                    if (prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                    {
                                        chars.Add(ch, ch.Key);
                                    Else
                                        If ch.GetPropertyValue(prop.Key) = act.sPropertyValue Then chars.Add(ch, ch.Key)
                                    }
                                }
                            Next;
                    }

                    if (chars IsNot null)
                    {
                        For Each ch As clsCharacter In chars.Values

                            switch (act.eItem)
                            {
                                case clsAction.ItemEnum.MoveCharacter:
                                    {
                                    private New clsCharacterLocation(ch) dest;

                                    ' Default new destination to current location
                                    dest.ExistWhere = ch.Location.ExistWhere;
                                    dest.Key = ch.Location.Key;
                                    dest.Position = ch.Location.Position;

                                    switch (act.eMoveCharacterTo)
                                    {
                                        case clsAction.MoveCharacterToEnum.InDirection:
                                            {
                                            private DirectionsEnum d = CType([Enum].Parse(GetType(DirectionsEnum), act.sKey2), DirectionsEnum);
                                            private clsDirection dDetails = null;
                                            If Adventure.htblLocations.ContainsKey(ch.Location.LocationKey) Then dDetails = Adventure.htblLocations(ch.Location.LocationKey).arlDirections(d)
                                            private string sRouteErrorTask = sRouteError ' because sRouteError gets overwritten by checking route restrictions;
                                            private string sRestrictionTextTemp = sRestrictionText;
                                            sRestrictionText = ""

                                            if (dDetails IsNot null && ch.HasRouteInDirection(d, false))
                                            {
                                                if (Adventure.htblLocations.ContainsKey(dDetails.LocationKey))
                                                {
                                                    dest.Key = dDetails.LocationKey;
                                                ElseIf Adventure.htblGroups.ContainsKey(dDetails.LocationKey) Then
                                                    dest.Key = Adventure.htblGroups(dDetails.LocationKey).RandomKey;
                                                }
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                'ch.Location.Key = dDetails.LocationKey
                                                'Display("You move " & WriteEnum(d).ToLower & vbCrLf & vbCrLf & Adventure.htblLocations(dest.LocationKey).ViewLocation)
                                            Else
                                                if (sRestrictionText <> "")
                                                {
                                                    Display(sRestrictionText);
                                                Else
                                                    ' Need to grab out the restriction text from the movement task
                                                    If sRouteErrorTask <> "" Then Display(sRouteErrorTask)
                                                }
                                                dest = Nothing
                                            }
                                            sRestrictionText = sRestrictionText

                                        case clsAction.MoveCharacterToEnum.ToLocation:
                                            {
                                            if (act.sKey2 = "Hidden")
                                            {
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                                                dest.Key = "";
                                            Else
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                dest.Key = act.sKey2;
                                            }
                                        case clsAction.MoveCharacterToEnum.ToLocationGroup:
                                            {
                                            dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                            dest.Key = Adventure.htblGroups(act.sKey2).RandomKey;
                                        case clsAction.MoveCharacterToEnum.ToLyingOn:
                                            {
                                            dest.Position = clsCharacterLocation.PositionEnum.Lying;
                                            if (act.sKey2 = THEFLOOR)
                                            {
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                dest.Key = ch.Location.LocationKey;
                                                'For Each sKey As String In ch.LocationRoots.Keys
                                                '    dest.Key = sKey
                                                '    Exit For
                                                'Next
                                            Else
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject;
                                                dest.Key = act.sKey2;
                                            }
                                        case clsAction.MoveCharacterToEnum.ToSameLocationAs:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(act.sKey2))
                                            {
                                                dest.ExistWhere = Adventure.htblCharacters(act.sKey2).Location.ExistWhere;
                                                dest.Key = Adventure.htblCharacters(act.sKey2).Location.Key;
                                            ElseIf Adventure.htblObjects.ContainsKey(act.sKey2) Then
                                                With Adventure.htblObjects(act.sKey2);
                                                    if (.IsStatic)
                                                    {
                                                        switch (.Location.StaticExistWhere)
                                                        {
                                                            case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                                                            case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                                                                {
                                                                ' Doesn't make sense to map
                                                            case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                                                                {
                                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                                                            case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                                                                {
                                                                TODO("Move Char to same location as object that is part of a character");
                                                            case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                                                                {
                                                                TODO("Move Char to same location as object that is part of an object");
                                                            case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                                                                {
                                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                                dest.Key = .Location.Key;
                                                        }
                                                    Else
                                                        switch (.Location.DynamicExistWhere)
                                                        {
                                                            case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                                                            case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                                                                {
                                                                dest.ExistWhere = Adventure.htblCharacters(.Key).Location.ExistWhere;
                                                                dest.Key = Adventure.htblCharacters(.Key).Location.Key;
                                                            case clsObjectLocation.DynamicExistsWhereEnum.Hidden:
                                                                {
                                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                                                            case clsObjectLocation.DynamicExistsWhereEnum.InLocation:
                                                                {
                                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                                dest.Key = .Location.Key;
                                                            case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                                                            case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                                                                {
                                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                                For Each l As clsLocation In Adventure.htblObjects(.Key).LocationRoots.Values
                                                                    dest.Key = l.Key;
                                                                    Exit For;
                                                                Next;
                                                        }
                                                    }

                                                }
                                            }
                                        case clsAction.MoveCharacterToEnum.ToSittingOn:
                                            {
                                            dest.Position = clsCharacterLocation.PositionEnum.Sitting;
                                            if (act.sKey2 = THEFLOOR)
                                            {
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                dest.Key = ch.Location.LocationKey;
                                                'For Each sKey As String In ch.LocationRoots.Keys
                                                '    dest.Key = sKey
                                                '    Exit For
                                                'Next
                                            Else
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject;
                                                dest.Key = act.sKey2;
                                            }
                                        case clsAction.MoveCharacterToEnum.ToStandingOn:
                                            {
                                            dest.Position = clsCharacterLocation.PositionEnum.Standing;
                                            if (act.sKey2 = THEFLOOR)
                                            {
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                                dest.Key = ch.Location.LocationKey;
                                            Else
                                                dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject;
                                                dest.Key = act.sKey2;
                                            }
                                        case clsAction.MoveCharacterToEnum.ToSwitchWith:
                                            {
                                            'For Each sKey As String In ch.LocationRoots.Keys
                                            '    dest.Key = sKey
                                            '    Exit For
                                            'Next
                                            if (ch.Key = Adventure.Player.Key || act.sKey2 = Adventure.Player.Key)
                                            {
                                                ' Don't move the characters, but change which one is the player
                                                private PerspectiveEnum eCurrentPerspective = Adventure.Player.Perspective;
                                                private string sOldPlayerKey = Adventure.Player.Key;
                                                Adventure.Player.CharacterType = clsCharacter.CharacterTypeEnum.NonPlayer;
                                                if (ch.Key = Adventure.Player.Key)
                                                {
                                                    Adventure.Player = Adventure.htblCharacters(act.sKey2);
                                                Else
                                                    Adventure.Player = Adventure.htblCharacters(ch.Key);
                                                }
                                                Adventure.Player.CharacterType = clsCharacter.CharacterTypeEnum.Player;
                                                Adventure.Player.Perspective = eCurrentPerspective;
                                                ' If the old Player character has any descriptors that match any pronouns for the player perspective, move them to the new Player
                                                ' TODO - Change this to configurable pronouns
                                                With Adventure.htblCharacters(sOldPlayerKey);
                                                    for (int iDescriptor = .arlDescriptors.Count - 1; iDescriptor <= 0; iDescriptor += -1)
                                                    {
                                                        private string sDescriptor = .arlDescriptors(iDescriptor);
                                                        private string[] sPronouns = {};
                                                        switch (eCurrentPerspective)
                                                        {
                                                            case PerspectiveEnum.FirstPerson:
                                                                {
                                                                sPronouns = New String() {"I", "me", "myself"}
                                                            case PerspectiveEnum.SecondPerson:
                                                                {
                                                                sPronouns = New String() {"I", "me", "myself", "you", "yourself"} ' include 1st in 2nd
                                                            case PerspectiveEnum.ThirdPerson:
                                                                {

                                                        }
                                                        For Each sPronoun As String In sPronouns
                                                            if (sPronoun.ToLower = sDescriptor.ToLower)
                                                            {
                                                                .arlDescriptors.RemoveAt(iDescriptor);
                                                                If ! Adventure.Player.arlDescriptors.Contains(sPronoun) Then Adventure.Player.arlDescriptors.Add(sPronoun)
                                                            }
                                                        Next;
                                                    Next;
                                                }
                                            Else
                                                ' Move the characters about
                                                private clsCharacterLocation loc = ch.Location;
                                                ch.Location = Adventure.htblCharacters(act.sKey2).Location;
                                                Adventure.htblCharacters(act.sKey2).Location = loc;
                                            }
                                        case clsAction.MoveCharacterToEnum.InsideObject:
                                            {
                                            dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject;
                                            dest.Key = act.sKey2;
                                        case clsAction.MoveCharacterToEnum.OntoCharacter:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(act.sKey2))
                                            {
                                                if (ch.Key = act.sKey2 || ch.Children(true).ContainsKey(act.sKey2))
                                                {
                                                    DisplayError("Recursive character relationship");
                                                Else
                                                    dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter;
                                                    dest.Key = act.sKey2;
                                                }
                                            }
                                        case clsAction.MoveCharacterToEnum.ToParentLocation:
                                            {
                                            dest.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                                            private string sCurrent = ch.Location.Key;
                                            if (Adventure.htblObjects.ContainsKey(sCurrent))
                                            {
                                                For Each sKey As String In Adventure.htblObjects(sCurrent).LocationRoots.Keys
                                                    dest.Key = sKey;
                                                    Exit For;
                                                Next;
                                            ElseIf Adventure.htblCharacters.ContainsKey(sCurrent) Then
                                                dest.Key = Adventure.htblCharacters(sCurrent).Location.LocationKey;
                                            }
                                        default:
                                            {
                                            TODO("Move Character to " + act.eMoveCharacterTo.ToString);
                                    }
                                    If dest != null Then ch.Move(dest)

                                case clsAction.ItemEnum.AddCharacterToGroup:
                                    {
                                    If ! Adventure.htblGroups(act.sKey2).arlMembers.Contains(ch.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Add(ch.Key)
                                    'ch.bCalculatedGroups = False
                                    ch.ResetInherited();
                                case clsAction.ItemEnum.RemoveCharacterFromGroup:
                                    {
                                    If Adventure.htblGroups(act.sKey2).arlMembers.Contains(ch.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Remove(ch.Key)
                                    'ch.bCalculatedGroups = False
                                    ch.ResetInherited();
                            }

                        Next;
                    }


                case clsAction.ItemEnum.AddLocationToGroup:
                case clsAction.ItemEnum.RemoveLocationFromGroup:
                    {
                    private LocationHashTable locs = null;

                    switch (act.eMoveLocationWhat)
                    {
                        case clsAction.MoveLocationWhatEnum.Location:
                            {
                            locs = New LocationHashTable
                            locs.Add(Adventure.htblLocations(act.sKey1), act.sKey1);
                        case clsAction.MoveLocationWhatEnum.LocationOf:
                            {
                            if (Adventure.htblCharacters.ContainsKey(act.sKey1))
                            {
                                'locs = Adventure.htblCharacters(act.sKey1).LocationRoots
                                locs = New LocationHashTable
                                private string sLocKey = Adventure.htblCharacters(act.sKey1).Location.LocationKey;
                                If sLocKey <> HIDDEN Then locs.Add(Adventure.htblLocations(sLocKey), act.sKey1)
                            ElseIf Adventure.htblObjects.ContainsKey(act.sKey1) Then
                                locs = Adventure.htblObjects(act.sKey1).LocationRoots
                            }
                        case clsAction.MoveLocationWhatEnum.EverywhereInGroup:
                            {
                            locs = New LocationHashTable
                            For Each sKey As String In Adventure.htblGroups(act.sKey1).arlMembers
                                locs.Add(Adventure.htblLocations(sKey), sKey);
                            Next;
                        case clsAction.MoveLocationWhatEnum.EverywhereWithProperty:
                            {
                            locs = New LocationHashTable
                            private clsProperty prop = Adventure.htblLocationProperties(act.sKey1);
                            For Each loc As clsLocation In Adventure.htblLocations.Values
                                if (loc.HasProperty(prop.Key))
                                {
                                    if (prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                    {
                                        locs.Add(loc, loc.Key);
                                    Else
                                        If loc.GetPropertyValue(prop.Key) = act.sPropertyValue Then locs.Add(loc, loc.Key)
                                    }
                                }
                            Next;
                    }

                    if (locs IsNot null)
                    {
                        For Each loc As clsLocation In locs.Values

                            switch (act.eItem)
                            {
                                case clsAction.ItemEnum.AddLocationToGroup:
                                    {
                                    If ! Adventure.htblGroups(act.sKey2).arlMembers.Contains(loc.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Add(loc.Key)
                                    'loc.bCalculatedGroups = False
                                    loc.ResetInherited();
                                case clsAction.ItemEnum.RemoveLocationFromGroup:
                                    {
                                    If Adventure.htblGroups(act.sKey2).arlMembers.Contains(loc.Key) Then Adventure.htblGroups(act.sKey2).arlMembers.Remove(loc.Key)
                                    'loc.bCalculatedGroups = False
                                    loc.ResetInherited();
                            }

                        Next;
                    }


                case clsAction.ItemEnum.SetProperties:
                    {
                    if (Adventure.htblObjects.ContainsKey(act.sKey1))
                    {
                        private clsObject ob = Adventure.htblObjects(act.sKey1);

                        switch (act.sKey2)
                        {
                            case OBJECTARTICLE:
                                {
                                private string sResult = EvaluateExpression(act.sPropertyValue);
                                If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                ob.Article = sResult;
                            case OBJECTPREFIX:
                                {
                                private string sResult = EvaluateExpression(act.sPropertyValue);
                                If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                ob.Prefix = sResult;
                            case OBJECTNOUN:
                                {
                                private string sResult = EvaluateExpression(act.sPropertyValue);
                                If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                ob.arlNames(0) = sResult;
                            default:
                                {
                                private clsProperty prop = null;
                                If ob.HasProperty(act.sKey2) Then prop = ob.GetProperty(act.sKey2)

                                if (prop Is null)
                                {
                                    'If act.StringValue = sSELECTED Then
                                    ' We're trying to add this as a property
                                    if (Adventure.htblObjectProperties.ContainsKey(act.sKey2))
                                    {
                                        prop = CType(Adventure.htblObjectProperties(act.sKey2).Clone, clsProperty)
                                        prop.Selected = true;
                                        ob.AddProperty(prop);

                                        switch (prop.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                ' Nothing more to do
                                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                                {
                                                private string sObKey = act.sPropertyValue;
                                                If sObKey.StartsWith("Referenced") Then sObKey = GetReference(sObKey)
                                                'prop.Value = sObKey
                                                ob.SetPropertyValue(prop.Key, sObKey);
                                        }
                                        'End If
                                    Else
                                        DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " + act.sKey2 + " not found.");
                                    }
                                    'Else
                                    '    DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " & act.sKey2 & " not found.")
                                    'End If
                                Else
                                    if (act.StringValue = sUNSELECTED)
                                    {
                                        ' We're trying to remove this property
                                        ob.RemoveProperty(act.sKey2);
                                    Else
                                        'prop.Value = act.sPropertyValue ' act.StringValue
                                        switch (prop.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.Integer:
                                            case clsProperty.PropertyTypeEnum.Text:
                                            case clsProperty.PropertyTypeEnum.StateList:
                                            case clsProperty.PropertyTypeEnum.ValueList:
                                                {
                                                ' Could be dropdown or expression
                                                private string sResult = EvaluateExpression(act.sPropertyValue);
                                                If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                                prop.Value = sResult;
                                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                            case clsProperty.PropertyTypeEnum.LocationKey:
                                                {
                                                private string sObKey = act.sPropertyValue;
                                                If sObKey.StartsWith("Referenced") Then sObKey = GetReference(sObKey)
                                                'prop.Value = sObKey
                                                prop.Value = sObKey;
                                            default:
                                                {
                                                prop.Value = act.sPropertyValue;
                                        }
                                    }
                                }
                        }

                    ElseIf Adventure.htblCharacters.ContainsKey(act.sKey1) Then
                        private clsProperty prop = null;
                        private clsCharacter ch = Adventure.htblCharacters(act.sKey1);
                        If ch.HasProperty(act.sKey2) Then prop = ch.GetProperty(act.sKey2)

                        if (prop Is null)
                        {
                            switch (act.sKey2)
                            {
                                case CHARACTERPROPERNAME:
                                    {
                                    prop = New clsProperty
                                    prop.Key = act.sKey2;
                                    prop.Type = clsProperty.PropertyTypeEnum.Text;
                            }
                        }

                        if (prop Is null)
                        {
                            if (act.StringValue = sSELECTED)
                            {
                                ' We're trying to add this as a property
                                if (Adventure.htblCharacterProperties.ContainsKey(act.sKey2))
                                {
                                    prop = CType(Adventure.htblCharacterProperties(act.sKey2).Clone, clsProperty)
                                    if (prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                    {
                                        prop.Selected = true;
                                        ch.AddProperty(prop);
                                        'Adventure.htblCharacters(act.sKey1).bCalculatedGroups = False
                                    }
                                Else
                                    DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " + act.sKey2 + " not found.");
                                }
                            Else
                                DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " + act.sKey2 + " not found.");
                            }
                        Else
                            if (act.StringValue = sUNSELECTED)
                            {
                                ' We're trying to remove this property
                                ch.RemoveProperty(act.sKey2);
                                'Adventure.htblObjects(act.sKey1).bCalculatedGroups = False
                            Else
                                switch (prop.Type)
                                {
                                    case clsProperty.PropertyTypeEnum.Integer:
                                    case clsProperty.PropertyTypeEnum.Text:
                                    case clsProperty.PropertyTypeEnum.StateList:
                                    case clsProperty.PropertyTypeEnum.ValueList:
                                        {
                                        ' Could be dropdown or expression
                                        private string sResult = EvaluateExpression(act.sPropertyValue);
                                        If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                        prop.Value = sResult;
                                    case clsProperty.PropertyTypeEnum.ObjectKey:
                                    case clsProperty.PropertyTypeEnum.CharacterKey:
                                    case clsProperty.PropertyTypeEnum.LocationKey:
                                        {
                                        private string sObKey = act.sPropertyValue;
                                        If sObKey.StartsWith("Referenced") Then sObKey = GetReference(sObKey)
                                        prop.Value = sObKey;
                                    default:
                                        {
                                        prop.Value = act.sPropertyValue;
                                }
                            }
                        }

                        switch (act.sKey2)
                        {
                            case CHARACTERPROPERNAME:
                                {
                                ch.ProperName = prop.Value;
                            case "CharacterPosition":
                                {
                                ch.Location.ResetPosition();
                        }

                    ElseIf Adventure.htblLocations.ContainsKey(act.sKey1) Then
                        private clsProperty prop = null;
                        If Adventure.htblLocations(act.sKey1).HasProperty(act.sKey2) Then prop = Adventure.htblLocations(act.sKey1).GetProperty(act.sKey2)

                        if (prop Is null)
                        {
                            if (act.StringValue = sSELECTED)
                            {
                                ' We're trying to add this as a property
                                if (Adventure.htblLocationProperties.ContainsKey(act.sKey2))
                                {
                                    prop = CType(Adventure.htblLocationProperties(act.sKey2).Clone, clsProperty)
                                    if (prop.Type = clsProperty.PropertyTypeEnum.SelectionOnly)
                                    {
                                        prop.Selected = true;
                                        Adventure.htblLocations(act.sKey1).AddProperty(prop);
                                    }
                                Else
                                    DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " + act.sKey2 + " not found.");
                                }
                            Else
                                DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.Error, "Property " + act.sKey2 + " not found.");
                            }
                        Else
                            if (act.StringValue = sUNSELECTED)
                            {
                                ' We're trying to remove this property
                                Adventure.htblLocations(act.sKey1).RemoveProperty(act.sKey2);
                            Else
                                switch (prop.Type)
                                {
                                    case clsProperty.PropertyTypeEnum.Integer:
                                    case clsProperty.PropertyTypeEnum.Text:
                                    case clsProperty.PropertyTypeEnum.StateList:
                                    case clsProperty.PropertyTypeEnum.ValueList:
                                        {
                                        ' Could be dropdown or expression
                                        private string sResult = EvaluateExpression(act.sPropertyValue);
                                        If sResult == null && act.sPropertyValue <> "" Then sResult = act.sPropertyValue
                                        prop.Value = sResult;
                                    case clsProperty.PropertyTypeEnum.ObjectKey:
                                    case clsProperty.PropertyTypeEnum.CharacterKey:
                                    case clsProperty.PropertyTypeEnum.LocationKey:
                                        {
                                        private string sObKey = act.sPropertyValue;
                                        If sObKey.StartsWith("Referenced") Then sObKey = GetReference(sObKey)
                                        prop.Value = sObKey;
                                    default:
                                        {
                                        prop.Value = act.sPropertyValue;
                                }
                                'prop.Value = act.StringValue
                                If prop.Key = "CharacterPosition" Then Adventure.htblCharacters(act.sKey1).Location.ResetPosition()
                            }
                        }
                    }

                case clsAction.ItemEnum.SetVariable:
                case clsAction.ItemEnum.IncreaseVariable:
                case clsAction.ItemEnum.DecreaseVariable:
                    {
                    private clsVariable var = Adventure.htblVariables(act.sKey1);
                    private string sExpr = act.StringValue;
                    If act.eItem = clsAction.ItemEnum.IncreaseVariable Then sExpr = "%" + var.Name + "% + " + sExpr
                    If act.eItem = clsAction.ItemEnum.DecreaseVariable Then sExpr = "%" + var.Name + "% - " + sExpr

                    if (act.eVariables = clsAction.VariablesEnum.Assignment)
                    {
                        private int iIndex = 1;
                        if (var.Length > 1 && Not act.sKey2 Is null)
                        {
                            if (act.sKey2.StartsWith("ReferencedNumber"))
                            {
                                'Dim iRef As Integer = Math.Max(SafeInt(act.sKey2.Replace("ReferencedNumber", "")) - 1, 0)
                                'iIndex = Adventure.iReferencedNumber(iRef)
                                iIndex = SafeInt(GetReference(act.sKey2))
                            ElseIf IsNumeric(act.sKey2) Then
                                iIndex = CInt(Val(act.sKey2))
                            Else
                                iIndex = Adventure.htblVariables(act.sKey2).IntValue
                            }
                        }
                        if (var.Key <> "Score" || (task IsNot null && Not task.Scored) Then '|| act.IntValue < 0)
                        {
                            var.SetToExpression(sExpr, iIndex);
                            if (var.Key = "Score")
                            {
                                task.Scored = true;
                                Adventure.Score = var.IntValue;
                            }
                        }
                    Else
                        for (int iLoop = act.IntValue; iLoop <= CInt(act.sKey2); iLoop++)
                        {
                            var.SetToExpression(sExpr, iLoop);
                        Next;
                    }

                    'Case clsAction.ItemEnum.Score
                    '        If Not task.Scored OrElse act.IntValue < 0 Then
                    '            task.Scored = True
                    '            Adventure.Score += act.IntValue
                    '        End If

                case clsAction.ItemEnum.SetTasks:
                    {
                    private clsTask tas2X = Adventure.htblTasks(act.sKey1);

                    private int iFrom = 1;
                    private int iTo = 1;

                    if (act.sPropertyValue <> "")
                    {
                        iFrom = act.IntValue
                        iTo = SafeInt(act.sPropertyValue)
                    }

                    for (int iLoop = iFrom; iLoop <= iTo; iLoop++)
                    {
                        if (act.eSetTasks = clsAction.SetTasksEnum.Execute)
                        {
                            ' Store the existing refs
                            Dim oExistingRefs() As clsNewReference = NewReferences

                            DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.High, "Executing task '" + tas2X.Description + "'.");
                            if (act.StringValue <> "")
                            {
                                ' Rewrite the references based on our parameters
                                Dim sParams() As String = act.StringValue.Split("|"c)
                                private int iWhichNewRefAreWeLookingAt = -1;
                                Dim ReferencesNew(sParams.Length - 1) As clsNewReference
                                For Each sParam As String In sParams
                                    iWhichNewRefAreWeLookingAt += 1;

                                    ' Find each ref in the new task that our parameter corresponds to
                                    private bool bFoundMatchingRef = false;
                                    private int iWhichOldRefAreWeLookingAt = -1;
                                    For Each sRef As String In tas2X.References
                                        iWhichOldRefAreWeLookingAt += 1;
                                        ' Again, we may be looking outside NewRefs if we're looking at subtask with different refs... :-/
                                        If sParam = sRef && iWhichOldRefAreWeLookingAt < NewReferences.Length Then ' Ok, found same ref, so we just pass the ref thru
                                            ReferencesNew(iWhichNewRefAreWeLookingAt) = NewReferences(iWhichOldRefAreWeLookingAt);
                                            bFoundMatchingRef = True
                                        }
                                    Next;
                                    if (Not bFoundMatchingRef)
                                    {
                                        ' Need to work this out on our own...
                                        private clsNewReference UserDefinedRef = null;

                                        if (sParam.Contains("."))
                                        {
                                            ' Hmm, let's see if this is an OO function
                                            For Each sProp As String In Adventure.htblAllProperties.Keys
                                                if (sParam.EndsWith("." + sProp))
                                                {
                                                    private clsProperty prop = Adventure.htblAllProperties(sProp);
                                                    switch (prop.Type)
                                                    {
                                                        case clsProperty.PropertyTypeEnum.CharacterKey:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Character)
                                                        case clsProperty.PropertyTypeEnum.Integer:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Number)
                                                        case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Text)
                                                        case clsProperty.PropertyTypeEnum.LocationKey:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Location)
                                                        case clsProperty.PropertyTypeEnum.ObjectKey:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Object)
                                                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                            {
                                                            switch (prop.PropertyOf)
                                                            {
                                                                case clsProperty.PropertyOfEnum.AnyItem:
                                                                    {
                                                                    UserDefinedRef = New clsNewReference(ReferencesType.Item)
                                                                case clsProperty.PropertyOfEnum.Characters:
                                                                    {
                                                                    UserDefinedRef = New clsNewReference(ReferencesType.Character)
                                                                case clsProperty.PropertyOfEnum.Locations:
                                                                    {
                                                                    UserDefinedRef = New clsNewReference(ReferencesType.Location)
                                                                case clsProperty.PropertyOfEnum.Objects:
                                                                    {
                                                                    UserDefinedRef = New clsNewReference(ReferencesType.Object)
                                                            }
                                                        case clsProperty.PropertyTypeEnum.StateList:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Text)
                                                        case clsProperty.PropertyTypeEnum.Text:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Text)
                                                        case clsProperty.PropertyTypeEnum.ValueList:
                                                            {
                                                            UserDefinedRef = New clsNewReference(ReferencesType.Number)
                                                    }
                                                    Exit For;
                                                }
                                            Next;
                                            if (UserDefinedRef Is null)
                                            {
                                                if (sParam.EndsWith(".Worn"))
                                                {
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Object)
                                                ElseIf sParam.EndsWith(".List") Then
                                                    TODO();
                                                ElseIf sParam.EndsWith(".Count") Then
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Number)
                                                ElseIf sParam.EndsWith(".Exits") Then
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Direction)
                                                }
                                            }
                                        }
                                        if (UserDefinedRef Is null)
                                        {
                                            ' Gotta guess the type of ref...
                                            switch (sLeft(sParam, 6).ToLower)
                                            {
                                                case "%convc":
                                                    {
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Character)
                                                case "%paren":
                                                    {
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Object)
                                                case "%text%":
                                                    {
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Text)
                                                case "%loop%":
                                                case "%numbe":
                                                    {
                                                    UserDefinedRef = New clsNewReference(ReferencesType.Number)
                                                default:
                                                    {
                                                    if (IsNumeric(sLeft(sParam, 6).ToLower))
                                                    {
                                                        UserDefinedRef = New clsNewReference(ReferencesType.Number)
                                                    Else
                                                        UserDefinedRef = New clsNewReference(ReferencesType.Object)
                                                    }
                                            }
                                        }
                                        UserDefinedRef.sParentTask = tas2X.Key;

                                        ' Now work out, e.g. %ParentOf[%objects%]% ...
                                        private string sFunctionRef = ReplaceFunctions(sParam);
                                        If sFunctionRef.ToLower = "%loop%" Then sFunctionRef = iLoop.ToString

                                        if (Not sFunctionRef.Contains("***"))
                                        {
                                            private New List<string> listRefs;
                                            if (sFunctionRef.Contains("|"))
                                            {
                                                For Each sRef As String In sFunctionRef.Split("|"c)
                                                    listRefs.Add(sRef);
                                                Next;
                                            Else
                                                listRefs.Add(sFunctionRef);
                                            }

                                            For Each sRef As String In listRefs
                                                private New clsSingleItem itm;
                                                If sRef = "nothing" Then sRef = null ' List function
                                                itm.MatchingPossibilities.Add(sRef) 'ReplaceFunctions(sParam));
                                                UserDefinedRef.Items.Add(itm);
                                            Next;
                                            if (sParam.StartsWith("%ParentOf"))
                                            {
                                                UserDefinedRef.ReferenceType = ReferencesType.Object;
                                            }
                                        Else
                                            DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Error calculating parameter " + sParam);
                                        }
                                        ReferencesNew(iWhichNewRefAreWeLookingAt) = UserDefinedRef;
                                    }
                                Next;
                                NewReferences = ReferencesNew
                                PrintOutReferences();
                            Else
                                ' Explicitly clear the refs for this task being called
                                NewReferences = Nothing
                            }
                            ' was True in ChildTask.  But it's not a child, it's a seperate task call...
                            ' was bCalledFromEvent in second param - but think this should be set if calling from task
                            ' Re the above, a reason why would be good.  Means later we don't do the continue? check
                            private bool bSubTaskHasOutput = false;
                            ' Did have EvaluateResponses = True, but we need to evaluate at the end in case we are inserting responses before ones in actions
                            AttemptToExecuteTask(act.sKey1, bCalledFromEvent, , true, , bSubTaskHasOutput, false) 'true);
                            If bSubTaskHasOutput Then bTaskHasOutputNew = true
                            NewReferences = oExistingRefs
                        Else
                            if (tas2X.Completed)
                            {
                                DebugPrint(ItemEnum.Task, act.sKey1, DebugDetailLevelEnum.High, "Task '" + tas2X.Description + "' being uncompleted.");

                                if (tas2X.Completed)
                                {
                                    ' Check any walks/events to see if anything triggers on this task completing
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        For Each w As clsWalk In c.arlWalks
                                            For Each ctrl As EventOrWalkControl In w.WalkControls
                                                if (ctrl.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.UnCompletion && ctrl.sTaskKey = tas2X.Key)
                                                {
                                                    switch (ctrl.eControl)
                                                    {
                                                        case EventOrWalkControl.ControlEnum.Resume:
                                                            {
                                                            If w.Status = clsWalk.StatusEnum.Paused Then w.Resume()
                                                        case EventOrWalkControl.ControlEnum.Start:
                                                            {
                                                            If w.Status <> clsWalk.StatusEnum.Running Then w.Start()
                                                        case EventOrWalkControl.ControlEnum.Stop:
                                                            {
                                                            If w.Status = clsWalk.StatusEnum.Running Then w.Stop()
                                                        case EventOrWalkControl.ControlEnum.Suspend:
                                                            {
                                                            If w.Status = clsWalk.StatusEnum.Running Then w.Pause()
                                                    }
                                                }
                                            Next;
                                        Next;
                                    Next;
                                    For Each e As clsEvent In Adventure.htblEvents.Values
                                        For Each ctrl As EventOrWalkControl In e.EventControls
                                            if (ctrl.eCompleteOrNot = EventOrWalkControl.CompleteOrNotEnum.UnCompletion && ctrl.sTaskKey = tas2X.Key)
                                            {
                                                switch (ctrl.eControl)
                                                {
                                                    case EventOrWalkControl.ControlEnum.Resume:
                                                        {
                                                        If e.Status = clsWalk.StatusEnum.Paused Then e.Resume()
                                                    case EventOrWalkControl.ControlEnum.Start:
                                                        {
                                                        If e.Status <> clsWalk.StatusEnum.Running Then e.Start()
                                                    case EventOrWalkControl.ControlEnum.Stop:
                                                        {
                                                        If e.Status = clsWalk.StatusEnum.Running Then e.Stop()
                                                    case EventOrWalkControl.ControlEnum.Suspend:
                                                        {
                                                        If e.Status = clsWalk.StatusEnum.Running Then e.Pause()
                                                }
                                            }
                                        Next;
                                    Next;
                                }

                                tas2X.Completed = false;
                            }
                        }
                    Next;

                case clsAction.ItemEnum.Time:
                    {
                    ' This isn't perfect - not sure what'll happen with any fail messages
                    For Each sMessage As String In htblResponsesPass.OrderedKeys
                        private clsNewReference[] refs = CType(htblResponsesPass(sMessage), clsNewReference());

                        NewReferences = refs
                        Display(sMessage);
                    Next;
                    htblResponsesPass.Clear();
                    for (int i = 0; i <= EvaluateExpression(act.StringValue, true) - 1; i++)
                    {
                        TurnBasedStuff();
                    Next;
                    htblResponsesPass.Clear();

                case clsAction.ItemEnum.EndGame:
                    {
                    Adventure.eGameState = act.eEndgame;

                case clsAction.ItemEnum.Conversation:
                    {
                    switch (act.eConversation)
                    {
                        case clsAction.ConversationEnum.EnterConversation:
                            {
                            Adventure.sConversationCharKey = act.sKey1;
                        case clsAction.ConversationEnum.LeaveConversation:
                            {
                            If Adventure.sConversationCharKey = act.sKey1 Then Adventure.sConversationCharKey = ""
                        default:
                            {
                            ExecuteConversation(act.sKey1, act.eConversation, act.StringValue, bTaskHasOutputNew);
                            'bTaskHasOutput = True
                    }

            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error executing action " + actx.Summary, ex);
        }

    }





    private void ExecuteConversation(string sCharKey, clsAction.ConversationEnum ConvType, string sCommandOrSubject, ref bTaskHasOutputNew As Boolean)
    {

        DebugPrint(ItemEnum.Character, sCharKey, DebugDetailLevelEnum.Medium, "Execute Conversation " + ConvType.ToString + ": " + sCommandOrSubject);

        ' If currently in a conversation with a different character, search for an Implicit Farewell for other char
        if (Adventure.sConversationCharKey <> "" && Adventure.sConversationCharKey <> sCharKey)
        {
            private clsTopic farewell = FindConversationNode(Adventure.htblCharacters(Adventure.sConversationCharKey), ConvType, "");
            if (farewell IsNot null)
            {
                If AddResponse(false, farewell.oConversation.ToString, New String() {}, true) Then bTaskHasOutputNew = true
            }

            Adventure.sConversationCharKey = "";
            Adventure.sConversationNode = "";
        }


        private clsCharacter ConvChar = Adventure.htblCharacters(sCharKey);

        ' If not currently in conversation and ConvType != Intro, then search for an Implicit Intro for that char

        if (Adventure.sConversationCharKey = "" Then ' && ConvType <> clsAction.ConversationEnum.Greet)
        {
            ' Try to find an explicit intro
            private clsTopic intro = FindConversationNode(ConvChar, ConvType Or clsAction.ConversationEnum.Greet, sCommandOrSubject);
            ' If not, look for an implicit one
            If intro == null Then intro = FindConversationNode(ConvChar, clsAction.ConversationEnum.Greet, "")
            if (intro IsNot null)
            {
                If AddResponse(false, intro.oConversation.ToString, New String() {}, true) Then bTaskHasOutputNew = true
                'Display(intro.oConversation.ToString)
                Adventure.sConversationNode = intro.Key;
                If intro.bAsk || intro.bTell || intro.bCommand Then ' We matched an explicit intro, so no need to look further
                    Adventure.sConversationCharKey = sCharKey;
                    If intro.arlActions.Count > 0 Then ExecuteActions(intro.arlActions, bTaskHasOutputNew)
                    Exit Sub;
                }
                ' TODO - Run the implicit actions if we didn't find an explict match later.
            }
        }


        ' Enter conversation with character
        Adventure.sConversationCharKey = sCharKey;


        ' Find conversation node (try to match on Farewell commands first)
        private clsTopic topic = null;
        private string sRestrictionTextTemp = sRestrictionText;
        sRestrictionText = ""
        If ConvType = clsAction.ConversationEnum.Command Then topic = FindConversationNode(ConvChar, ConvType | clsAction.ConversationEnum.Farewell, sCommandOrSubject)
        if (topic Is null)
        {
            topic = FindConversationNode(ConvChar, ConvType, sCommandOrSubject)
        Else
            Adventure.sConversationCharKey = "";
            Adventure.sConversationNode = "";
        }

        if (topic IsNot null)
        {
            If AddResponse(false, topic.oConversation.ToString, New String() {}, true) Then bTaskHasOutputNew = true
            'Display(topic.oConversation.ToString)
            ' If topic has children, set the conversation node
            if (ConvChar.htblTopics.DoesTopicHaveChildren(topic.Key))
            {
                Adventure.sConversationNode = topic.Key;
            Else
                If ! topic.bStayInNode Then Adventure.sConversationNode = ""
            }
            If topic.arlActions.Count > 0 Then ExecuteActions(topic.arlActions, bTaskHasOutputNew)
        Else
            ' Hmm, no conversation found.  Need to give a default response back...
            Adventure.sConversationNode = "";
            private string sMessage = "";
            if (sRestrictionText <> "")
            {
                sMessage = sRestrictionText
            Else
                ' TODO: Need to make this configurable within Generator
                switch (ConvType)
                {
                    case clsAction.ConversationEnum.Ask:
                        {
                        if (Adventure.dVersion < 5)
                        {
                            sMessage = "%CharacterName[" & ConvChar.Key & "]% does not respond to your question."
                        Else
                            sMessage = "%CharacterName[" & ConvChar.Key & "]% doesn't appear to understand you."
                        }
                    case clsAction.ConversationEnum.Farewell:
                        {
                        sMessage = "%CharacterName[" & ConvChar.Key & "]% doesn't appear to understand you."
                    case clsAction.ConversationEnum.Greet:
                        {
                        sMessage = "%CharacterName[" & ConvChar.Key & "]% doesn't appear to understand you."
                    case clsAction.ConversationEnum.Tell:
                        {
                        sMessage = "%CharacterName[" & ConvChar.Key & "]% doesn't appear to understand you."
                    case clsAction.ConversationEnum.Command:
                        {
                        sMessage = "%CharacterName[" & ConvChar.Key & "]% ignores you."
                        'AddResponse(bOutputMessages, ConvChar.Name & " ignores you.",  sReferences, bPass)
                }
            }

            If AddResponse(false, sMessage, New String() {}, true) Then bTaskHasOutputNew = true
        }
        sRestrictionText = sRestrictionTextTemp ' Dunno if we really need to do this, but may as well to be safe

    }


    internal void TriggerTimerEvent(string sEventKey, clsEvent.SubEvent se)
    {

        With Adventure.htblEvents(sEventKey);
            .RunSubEvent(se);
        }
        Display(vbCrLf + vbCrLf, true);

        CheckEndOfGame();

    }


    internal clsTopic FindConversationNode(clsCharacter ConvChar, clsAction.ConversationEnum ConvType, string sCommandOrSubject)
    {

        private int iConvType = CInt(ConvType);
        private bool bFarewell = false;
        if (iConvType >= CInt(clsAction.ConversationEnum.Farewell))
        {
            bFarewell = True
            iConvType -= CInt(clsAction.ConversationEnum.Farewell);
        }
        private bool bCommand = false;
        if (iConvType >= CInt(clsAction.ConversationEnum.Command))
        {
            bCommand = True
            iConvType -= CInt(clsAction.ConversationEnum.Command);
        }
        private bool bTell = false;
        if (iConvType >= CInt(clsAction.ConversationEnum.Tell))
        {
            bTell = True
            iConvType -= CInt(clsAction.ConversationEnum.Tell);
        }
        private bool bAsk = false;
        if (iConvType >= CInt(clsAction.ConversationEnum.Ask))
        {
            bAsk = True
            iConvType -= CInt(clsAction.ConversationEnum.Ask);
        }
        private bool bIntro = false;
        if (iConvType >= CInt(clsAction.ConversationEnum.Greet))
        {
            bIntro = True
            iConvType -= CInt(clsAction.ConversationEnum.Greet);
        }

        ' Iterate thru all the leaves of the current node
        With ConvChar;

            ' Should we sort our topics, perhaps by length?
            private double dfHighestPercent = 0;
            private int iMostMatches = 0;
            private clsTopic topicBest = null;

            For Each topic As clsTopic In .htblTopics.Values
                private int iMatchedKeywords = 0;

                if ((topic.ParentKey = "" || topic.ParentKey = Adventure.sConversationNode) && (Not bIntro || topic.bIntroduction) && (Not bFarewell || topic.bFarewell) && (bCommand = topic.bCommand) && (Not bAsk || topic.bAsk) && (Not bTell || topic.bTell))
                {

                    if (bAsk || bTell)
                    {
                        ' Keyword matching
                        ' Find the node that matches the most keywords
                        ' Then if there are more than one, pick the one that matches most as a percentage
                        private string[] sKeywords = topic.Keywords.Split(","c);
                        private bool bLowPriority = false;

                        For Each sKeyword As String In sKeywords
                            if (ContainsWord(sCommandOrSubject, sKeyword.ToLower.Trim) || sKeyword = "*")
                            {
                                'If sTopic.Trim = sCommandOrSubject Then
                                If PassRestrictions(topic.arlRestrictions) Then iMatchedKeywords += 1 ' Return topic
                                If sKeyword = "*" Then bLowPriority = true
                            }
                        Next;
                        private double dfPercentMatched = iMatchedKeywords / sKeywords.Length;
                        'If dfPercentMatched = 1 Then Return topic
                        If bLowPriority && dfPercentMatched = 1 Then dfPercentMatched = 0.001

                        if (iMatchedKeywords > iMostMatches || (iMatchedKeywords = iMostMatches && dfPercentMatched > dfHighestPercent))
                        {
                            topicBest = topic
                            dfHighestPercent = dfPercentMatched
                            iMostMatches = iMatchedKeywords
                        }
                    }

                    if (bCommand)
                    {
                        ' RE matching
                        For Each re As System.Text.RegularExpressions.Regex In GetRegularExpression(topic.Keywords.Trim.Replace("?", "\?"), sCommandOrSubject, False)
                            if (re IsNot null && re.IsMatch(sCommandOrSubject))
                            {
                                if (PassRestrictions(topic.arlRestrictions))
                                {
                                    for (int i = 0; i <= 4; i++)
                                    {
                                        private int iRef = i + 1;
                                        private string sNumber = "%number" + iRef.ToString + "%";
                                        private string sRefText = "%text" + iRef.ToString + "%";

                                        If topic.Keywords.Contains(sRefText) Then ' Needs full parsing really...
                                            Adventure.sReferencedText(i) = re.Match(sCommandOrSubject).Groups("text" + iRef.ToString).Value.Trim;
                                            ' Update Refs
                                            For Each ref As clsNewReference In NewReferences
                                                if (ref.ReferenceType = ReferencesType.Text && ref.ReferenceMatch = sRefText.Replace("%", ""))
                                                {
                                                    ref.Items.Clear();
                                                    private New clsSingleItem refItem;
                                                    refItem.bExplicitlyMentioned = true;
                                                    refItem.MatchingPossibilities.Add(Adventure.sReferencedText(i));
                                                    refItem.sCommandReference = sCommandOrSubject;
                                                    ref.Items.Add(refItem);
                                                    ref.sParentTask = "Topic";
                                                }
                                            Next;
                                        }
                                    Next;

                                    return topic;
                                }
                            }
                        Next;
                        'Dim re As System.Text.RegularExpressions.Regex = GetRegularExpression(topic.Keywords.Trim, sCommandOrSubject, False)
                    }

                    if (Not bAsk && Not bTell && Not bCommand)
                    {
                        ' No matching whatsoever
                        If PassRestrictions(topic.arlRestrictions) Then Return topic
                    }

                }
            Next;

            If topicBest != null Then Return topicBest
        }

        return null;

    }


    private void ExecuteActions(ActionArrayList Actions, ref bTaskHasOutputNew As Boolean = False)
    {

        DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Executing Actions...");
        iDebugIndent += 1;
        For Each act As clsAction In Actions
            ExecuteSingleAction(act, "", null, bTaskHasOutputNew);
        Next;
        iDebugIndent -= 1;

    }
    private void ExecuteActions(clsTask task, bool bCalledFromEvent = false, ref bTaskHasOutputNew As Boolean = False)
    {

        'If task.eDisplayCompletion = clsTask.BeforeAfterEnum.Before Then Display(task.CompletionMessage)
        'If task.eDisplayCompletion = clsTask.BeforeAfterEnum.Before AndAlso Not bOutputMessages Then AddResponse(bOutputMessages, sMessage, sReferences)

        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Executing Actions...");
        iDebugIndent += 1;
        For Each act As clsAction In task.arlActions
            'If act.eItem = clsAction.ItemEnum.SetTasks Then ' For now, whilst we work on the parser...
            ExecuteSingleAction(act, ParentTaskCommand(task), task, bCalledFromEvent, bTaskHasOutputNew);
            'End If
        Next;
        iDebugIndent -= 1;
        'If task.eDisplayCompletion = clsTask.BeforeAfterEnum.After Then Display(task.CompletionMessage)

    }


    public void ShowUserSplash()
    {

        if (clsBlorb.Frontispiece > -1)
        {

            private Image imgSplash = Blorb.GetImage(clsBlorb.Frontispiece);
            if (imgSplash IsNot null)
            {
#if Not www
                UserSplash = New frmUserSplash
                UserSplash.BackgroundImage = imgSplash;
                UserSplash.ClientSize = imgSplash.Size;
                if (fRunner IsNot null)
                {
                    fRunner.Icon = Icon.FromHandle(New Bitmap(imgSplash).GetHicon);
                    UserSplash.StartPosition = FormStartPosition.Manual;
                    UserSplash.Location = New Point(CInt(fRunner.Location.X + fRunner.Width / 2 - UserSplash.Width / 2), CInt(fRunner.Location.Y + fRunner.Height / 2 - UserSplash.Height / 2));
                Else
                    UserSplash.StartPosition = FormStartPosition.CenterScreen;
                }
                UserSplash.TopMost = true;
                UserSplash.Show();
                Application.DoEvents();
#endif
            }
        }
    }


    ' Returns the command from the parent task if we're a specific task.  Does this recursively in case we're specific of a specific etc.
    public string ParentTaskCommand(clsTask task)
    {

        while (task.TaskType = clsTask.TaskTypeEnum.Specific)
        {
            task = Adventure.htblTasks(task.GeneralKey)
        }
        if (task.arlCommands.Count > 0)
        {
            if (task.arlCommands.Count > iMatchedTaskCommand)
            {
                return task.arlCommands(iMatchedTaskCommand);
            Else
                return task.arlCommands(0);
            }
        Else
            return "";
        }

    }

    private string ToProper(string sText, bool bStrict = false)
    {

        private string sReturn = null;

        If sText.Length > 0 Then sReturn = sText.Substring(0, 1).ToUpper
        if (sText.Length > 1)
        {
            If bStrict Then sReturn &= sText.Substring(1).ToLower Else sReturn &= sText.Substring(1)
        }

        return sReturn;

    }


    private string AmbWord(StringArrayList sKeys, ReferencesType RefType)
    {

        private bool bFoundInAllItemsSoFar;
        private bool bFoundInThisItem;

        AmbWord = Nothing

        ' Work out a common word that's in all the object names, and is also in the input
        For Each sWord As String In Split(sLastProperInput, " ")
            bFoundInAllItemsSoFar = True
            For Each sKey As String In sKeys
                bFoundInThisItem = False
                switch (RefType)
                {
                    case ReferencesType.Object:
                        {
                        if (Adventure.htblObjects.ContainsKey(sKey))
                        {
                            For Each sName As String In Adventure.htblObjects(sKey).arlNames
                                if (sWord = sName)
                                {
                                    bFoundInThisItem = True
                                    GoTo NextItem;
                                }
                            Next sName;
                        }
                    case ReferencesType.Character:
                        {
                        if (Adventure.htblCharacters.ContainsKey(sKey))
                        {
                            if (sWord = Adventure.htblCharacters(sKey).ProperName)
                            {
                                bFoundInThisItem = True
                                GoTo NextItem;
                            }
                            For Each sName As String In Adventure.htblCharacters(sKey).arlDescriptors
                                if (sWord = sName)
                                {
                                    bFoundInThisItem = True
                                    GoTo NextItem;
                                }
                            Next sName;
                        }
                    case ReferencesType.Location:
                        {
                        if (Adventure.htblLocations.ContainsKey(sKey))
                        {
                            For Each sName As String In Adventure.htblLocations(sKey).ShortDescription.ToString.ToLower.Split(" "c)
                                if (sWord = sName)
                                {
                                    bFoundInThisItem = True
                                    GoTo NextItem;
                                }
                            Next sName;
                        }
                }

                if (Not bFoundInThisItem)
                {
                    bFoundInAllItemsSoFar = False
                    GoTo NextWord;
                }
NextItem:;
            Next sKey;
            If bFoundInAllItemsSoFar Then Return sWord
NextWord:;
        Next sWord;

    }


    private void DisplayAmbiguityQuestion()
    {

        NewReferences = Adventure.htblTasks(sAmbTask).NewReferencesWorking
        If NewReferences == null Then Exit Sub

        for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
        {
            Debug.WriteLine("Reference " + iRef);
            With NewReferences(iRef);
                Debug.WriteLine("Number of Items in this Reference: " + .Items.Count);
                For Each itm As clsSingleItem In .Items
                    if (itm.MatchingPossibilities.Count > 1)
                    {
                        switch (.ReferenceType)
                        {
                            case ReferencesType.Object:
                                {
                                private New ObjectHashTable htblObs;
                                For Each sKey As String In itm.MatchingPossibilities
                                    If ! htblObs.ContainsKey(sKey) Then htblObs.Add(Adventure.htblObjects(sKey), sKey)
                                Next;
                                private bool bCanSeeAny = false;
                                For Each sKey As String In htblObs.Keys
                                    If ! bCanSeeAny && Adventure.Player.CanSeeObject(sKey) Then bCanSeeAny = true
                                Next;
                                If ! bCanSeeAny Then     ' Want to try to move this into the library at some point, as we _may_ want to resolve ambiguous items that aren't visible to the player
                                    private bool bAnyPlural = false;
                                    For Each ob As clsObject In htblObs.Values
                                        if (ob.IsPlural)
                                        {
                                            bAnyPlural = True
                                            Exit For;
                                        }
                                    Next;
                                    if (bAnyPlural)
                                    {
                                        Display("You can't see any " + AmbWord(itm.MatchingPossibilities, .ReferenceType) + "!" + vbCrLf);
                                    Else
                                        Display("You can't see any " + (New clsObject).GuessPluralFromNoun(AmbWord(itm.MatchingPossibilities, .ReferenceType)) + "!" + vbCrLf);
                                    }
                                    sAmbTask = Nothing
                                Else
                                    Display("Which " + AmbWord(itm.MatchingPossibilities, .ReferenceType) + "?");
                                    Display(ToProper(htblObs.List("or")) + "." + vbCrLf);
                                }
                                Exit Sub;
                            case ReferencesType.Character:
                                {
                                private New CharacterHashTable htblChars;
                                For Each sKey As String In itm.MatchingPossibilities
                                    htblChars.Add(Adventure.htblCharacters(sKey), sKey);
                                Next;
                                private bool bCanSeeAny = false;
                                For Each sKey As String In htblChars.Keys
                                    If ! bCanSeeAny && Adventure.Player.CanSeeCharacter(sKey) Then bCanSeeAny = true
                                Next;
                                if (Not bCanSeeAny)
                                {
                                    Display("You can't see any " + AmbWord(itm.MatchingPossibilities, .ReferenceType) + "!" + vbCrLf);
                                    sAmbTask = Nothing
                                Else
                                    Display("Which " + AmbWord(itm.MatchingPossibilities, .ReferenceType) + "?");
                                    Display(ToProper(htblChars.List("or")) + "." + vbCrLf);
                                }
                                Exit Sub;
                            case ReferencesType.Location:
                                {
                                private New LocationHashTable htblLocs;
                                For Each sKey As String In itm.MatchingPossibilities
                                    htblLocs.Add(Adventure.htblLocations(sKey), sKey);
                                Next;
                                'Dim bCanSeeAny As Boolean = False
                                'For Each sKey As String In htblLocs.Keys
                                '    If Not bCanSeeAny AndAlso Adventure.Player.CanSeeCharacter(sKey) Then bCanSeeAny = True
                                'Next
                                'If Not bCanSeeAny Then
                                '    Display("You can't see any " & AmbWord(itm.MatchingPossibilities) & "!" & vbCrLf)
                                '    sAmbTask = Nothing
                                'Else
                                Display("Which " + AmbWord(itm.MatchingPossibilities, .ReferenceType) + "?");
                                Display(ToProper(htblLocs.List("or")) + "." + vbCrLf);
                                'End If
                                Exit Sub;
                            default:
                                {
                                ErrMsg("Unable to disambiguate reference types " + .ReferenceType.ToString);
                        }
                    ElseIf itm.MatchingPossibilities.Count = 0 Then
                        Display("Sorry, that does not clarify the ambiguity." + vbCrLf);
                        sAmbTask = Nothing
                        Exit Sub;
                    }
                Next;
            }
        Next;

    }



    private void PrintOutReferences()
    {

        If NewReferences == null Then Exit Sub
        for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
        {
            switch (iRef)
            {
                case 0:
                    {
                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "First Reference: ", false);
                case 1:
                    {
                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "Second Reference: ", false);
                case 2:
                    {
                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "Third Reference: ", false);
                case 3:
                    {
                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "Fourth Reference: ", false);
                default:
                    {
                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "Reference " + iRef + ": ", false);
            }

            private int iCount = 0;
            if (NewReferences(iRef) IsNot null)
            {
                For Each itm As clsSingleItem In NewReferences(iRef).Items
                    For Each sKey As String In itm.MatchingPossibilities
                        if (sKey IsNot null)
                        {
                            switch (NewReferences(iRef).ReferenceType)
                            {
                                case ReferencesType.Object:
                                    {
                                    If Adventure.htblObjects.ContainsKey(sKey) Then DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, Adventure.htblObjects(sKey).FullName, false) ' + " (" + sr.bExcept + ")")
                                case ReferencesType.Direction:
                                    {
                                    DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, sKey, false);
                                case ReferencesType.Character:
                                    {
                                    If Adventure.htblCharacters.ContainsKey(sKey) Then DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, Adventure.htblCharacters(sKey).ProperName, false) ' + " (" + sr.bExcept + ")")
                                case ReferencesType.Number:
                                    {

                                case ReferencesType.Text:
                                    {

                            }
                            iCount += 1;
                            If iCount < NewReferences(iRef).Items.Count Then DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, ", ", false)
                        Else
                            DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "NULL reference", false);
                        }
                    Next;
                Next;
            Else
                DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "null", false);
            }
            DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "");
        Next;

    }


    private void GrabIt()
    {

        try
        {
            private string sNewIt = "";
            private string sNewThem = "";
            private string sNewHim = "";
            private string sNewHer = "";
            Dim sWords() As String = sInput.Split(" "c) ' fRunner.txtInput.Text.Split(" "c)
            private New StringArrayList PossibleItKeys;
            private New StringArrayList PossibleThemKeys;
            private New StringArrayList PossibleHimKeys;
            private New StringArrayList PossibleHerKeys;


            ' First, look at anything visible, then seen
            for (eScope iScope = eScope.Visible; iScope <= eScope.Seen; iScope++)
            {
                For Each sWord As String In sWords

                    private ObjectHashTable htblObs = null;
                    private CharacterHashTable htblChars = null;
                    if (iScope = eScope.Visible)
                    {
                        htblObs = Adventure.htblObjects.VisibleTo(Adventure.Player.Key)
                        htblChars = Adventure.htblCharacters.VisibleTo(Adventure.Player.Key)
                    Else
                        htblObs = Adventure.htblObjects.SeenBy(Adventure.Player.Key)
                        htblChars = Adventure.htblCharacters.SeenBy(Adventure.Player.Key)
                    }

                    if (sNewIt = "")
                    {
                        For Each ob As clsObject In htblObs.Values
                            'If Not ob.IsPlural Then
                            For Each sName As String In ob.arlNames
                                if (sWord = sName)
                                {
                                    '                                    sNewIt = "the " & sWord
                                    If ! PossibleItKeys.Contains(ob.Key) Then PossibleItKeys.Add(ob.Key)
                                }
                            Next;
                            'End If
                        Next ob;
                    }
                    if (sNewThem = "")
                    {
                        For Each ob As clsObject In htblObs.Values
                            if (ob.IsPlural)
                            {
                                For Each sName As String In ob.arlNames
                                    if (sWord = sName)
                                    {
                                        If ! PossibleThemKeys.Contains(ob.Key) Then PossibleThemKeys.Add(ob.Key)
                                    }
                                Next;
                            }
                        Next ob;
                    }
                    For Each ch As clsCharacter In htblChars.Values
                        private bool bMatch = false;
                        If sWord = ch.ProperName.ToLower Then bMatch = true
                        For Each sName As String In ch.arlDescriptors
                            if (sWord = sName)
                            {
                                bMatch = True
                                Exit For;
                            }
                        Next;
                        if (bMatch)
                        {
                            switch (ch.Gender)
                            {
                                case clsCharacter.GenderEnum.Male:
                                    {
                                    '                                   sNewHim = ch.Name
                                    If ! PossibleItKeys.Contains(ch.Key) Then PossibleHimKeys.Add(ch.Key)
                                case clsCharacter.GenderEnum.Female:
                                    {
                                    '                                  sNewHer = ch.Name
                                    If ! PossibleItKeys.Contains(ch.Key) Then PossibleHerKeys.Add(ch.Key)
                                case clsCharacter.GenderEnum.Unknown:
                                    {
                                    '                                 sNewIt = ch.Name
                                    If ! PossibleItKeys.Contains(ch.Key) Then PossibleItKeys.Add(ch.Key)
                            }
                        }
                    Next;
                Next sWord;

                if (sNewIt = "")
                {
                    if (PossibleItKeys.Count = 1)
                    {
                        if (Adventure.htblObjects.ContainsKey(PossibleItKeys(0)))
                        {
                            sNewIt = Adventure.htblObjects(PossibleItKeys(0)).FullName(ArticleTypeEnum.Definite)
                        Else
                            sNewIt = Adventure.htblCharacters(PossibleItKeys(0)).Name
                        }
                    ElseIf PossibleItKeys.Count > 1 Then
                        private New StringArrayList arlKeys;

                        For Each sKey As String In PossibleItKeys
                            if (Adventure.htblObjects.ContainsKey(sKey))
                            {
                                private clsObject ob = Adventure.htblObjects(sKey);
                                For Each sPrefixWord As String In ob.Prefix.Split(" "c)
                                    For Each sWord As String In sWords
                                        if (sPrefixWord = sWord)
                                        {
                                            arlKeys.Add(sKey);
                                            GoTo NextOb;
                                        }
                                    Next;
                                Next;
                            }
NextOb:;
                            if (Adventure.htblCharacters.ContainsKey(sKey))
                            {
                                private clsCharacter ch = Adventure.htblCharacters(sKey);
                                For Each sPrefixWord As String In ch.Prefix.Split(" "c)
                                    For Each sWord As String In sWords
                                        if (sPrefixWord = sWord)
                                        {
                                            arlKeys.Add(sKey);
                                            GoTo NextChar;
                                        }
                                    Next;
                                Next;
                            }
NextChar:;
                        Next;

                        if (arlKeys.Count = 1)
                        {
                            if (Adventure.htblObjects.ContainsKey(arlKeys(0)))
                            {
                                sNewIt = Adventure.htblObjects(arlKeys(0)).FullName(ArticleTypeEnum.Definite)
                            ElseIf Adventure.htblCharacters.ContainsKey(arlKeys(0)) Then
                                sNewIt = Adventure.htblCharacters(arlKeys(0)).Name
                            }
                        }
                    }
                }

                if (sNewThem = "")
                {
                    if (PossibleThemKeys.Count = 1)
                    {
                        if (Adventure.htblObjects.ContainsKey(PossibleThemKeys(0)))
                        {
                            sNewThem = Adventure.htblObjects(PossibleThemKeys(0)).FullName(ArticleTypeEnum.Definite)
                        Else
                            sNewThem = Adventure.htblCharacters(PossibleThemKeys(0)).Name
                        }
                    ElseIf PossibleThemKeys.Count > 1 Then
                        private New StringArrayList arlKeys;

                        For Each sKey As String In PossibleThemKeys
                            if (Adventure.htblObjects.ContainsKey(sKey))
                            {
                                private clsObject ob = Adventure.htblObjects(sKey);
                                if (ob.IsPlural)
                                {
                                    For Each sPrefixWord As String In ob.Prefix.Split(" "c)
                                        For Each sWord As String In sWords
                                            if (sPrefixWord = sWord)
                                            {
                                                arlKeys.Add(sKey);
                                                GoTo NextObT;
                                            }
                                        Next;
                                    Next;
                                }
                            }
NextObT:;
                            if (Adventure.htblCharacters.ContainsKey(sKey))
                            {
                                private clsCharacter ch = Adventure.htblCharacters(sKey);
                                For Each sPrefixWord As String In ch.Prefix.Split(" "c)
                                    For Each sWord As String In sWords
                                        if (sPrefixWord = sWord)
                                        {
                                            arlKeys.Add(sKey);
                                            GoTo NextCharT;
                                        }
                                    Next;
                                Next;
                            }
NextCharT:;
                        Next;

                        if (arlKeys.Count = 1)
                        {
                            if (Adventure.htblObjects.ContainsKey(arlKeys(0)))
                            {
                                sNewThem = Adventure.htblObjects(arlKeys(0)).FullName(ArticleTypeEnum.Definite)
                            ElseIf Adventure.htblCharacters.ContainsKey(arlKeys(0)) Then
                                sNewThem = Adventure.htblCharacters(arlKeys(0)).Name
                            }
                        }
                    }
                }

                if (sNewHim = "")
                {
                    if (PossibleHimKeys.Count = 1)
                    {
                        if (Adventure.htblCharacters.ContainsKey(PossibleHimKeys(0)))
                        {
                            sNewHim = Adventure.htblCharacters(PossibleHimKeys(0)).Name(, False, False, ArticleTypeEnum.Definite)
                        }
                    ElseIf PossibleHimKeys.Count > 1 Then
                        private New StringArrayList arlKeys;

                        For Each sKey As String In PossibleHimKeys
                            if (Adventure.htblCharacters.ContainsKey(sKey))
                            {
                                private clsCharacter ch = Adventure.htblCharacters(sKey);
                                For Each sPrefixWord As String In ch.Prefix.Split(" "c)
                                    For Each sWord As String In sWords
                                        if (sPrefixWord = sWord)
                                        {
                                            arlKeys.Add(sKey);
                                            GoTo NextChar1;
                                        }
                                    Next;
                                Next;
                            }
NextChar1:;
                        Next;

                        if (arlKeys.Count = 1)
                        {
                            if (Adventure.htblCharacters.ContainsKey(arlKeys(0)))
                            {
                                sNewHim = Adventure.htblCharacters(arlKeys(0)).Name(, False, False, ArticleTypeEnum.Definite)
                            }
                        }
                    }
                }

                if (sNewHer = "")
                {
                    if (PossibleHerKeys.Count = 1)
                    {
                        if (Adventure.htblCharacters.ContainsKey(PossibleHerKeys(0)))
                        {
                            sNewHer = Adventure.htblCharacters(PossibleHerKeys(0)).Name(, False, False, ArticleTypeEnum.Definite)
                        }
                    ElseIf PossibleHerKeys.Count > 1 Then
                        private New StringArrayList arlKeys;

                        For Each sKey As String In PossibleHerKeys
                            if (Adventure.htblCharacters.ContainsKey(sKey))
                            {
                                private clsCharacter ch = Adventure.htblCharacters(sKey);
                                For Each sPrefixWord As String In ch.Prefix.Split(" "c)
                                    For Each sWord As String In sWords
                                        if (sPrefixWord = sWord)
                                        {
                                            arlKeys.Add(sKey);
                                            GoTo NextChar2;
                                        }
                                    Next;
                                Next;
                            }
NextChar2:;
                        Next;

                        if (arlKeys.Count = 1)
                        {
                            if (Adventure.htblCharacters.ContainsKey(arlKeys(0)))
                            {
                                sNewHer = Adventure.htblCharacters(arlKeys(0)).Name(, False, False, ArticleTypeEnum.Definite)
                            }
                        }
                    }
                }

            Next;

            If sNewIt <> "" Then sIt = sNewIt
            If sNewThem <> "" Then sThem = sNewThem
            If sNewHim <> "" Then sHim = sNewHim
            If sNewHer <> "" Then sHer = sNewHer

            If sIt = "" Then sIt = "Absolutely null"
            If sThem = "" Then sThem = "Absolutely null"
            If sHim = "" Then sHim = "No male"
            If sHer = "" Then sHer = "No female"

        }
        catch (Exception ex)
        {
            ErrMsg("Error grabbing ""it""", ex);
            sIt = "Absolutely Nothing"
        }

    }


    'Private Sub GetMentionedObs()

    '    arlMentionedObs.Clear()
    '    sMentionedObsPattern = ""

    '    For Each ob As clsObject In Adventure.htblObjects.Values
    '        For Each sNoun As String In ob.arlNames
    '            If sInput.Contains(sNoun) Then
    '                arlMentionedObs.Add(ob.Key)
    '                Exit For ' sNoun
    '            End If
    '        Next
    '    Next

    '    Dim sb As New System.Text.StringBuilder("")
    '    For Each sKey As String In arlMentionedObs
    '        If sb.Length > 0 Then sb.Append("|")
    '        sb.Append(Adventure.htblObjects(sKey).sRegularExpressionString)
    '    Next
    '    sMentionedObsPattern = sb.ToString

    'End Sub

    private void ReplaceWord(ref sText As String, string sFind, string sReplace)
    {
        sText = sText.Replace(" " & sFind & " ", " " & sReplace & " ")
        If sText.StartsWith(sFind + " ") Then sText = sReplace + " " + sRight(sText, sText.Length - (sFind.Length + 1))
        If sText.EndsWith(" " + sFind) Then sText = sLeft(sText, sText.Length - (sFind.Length + 1)) + " " + sReplace
        If sText = sFind Then sText = sReplace
    }


    internal void Restart()
    {
        OpenAdventure(Adventure.FullPath);
    }


    internal bool SaveGame(bool bSaveAs = false)
    {

        try
        {
#if www
            private string sFilename;
            if (UserSession.sGameFolder <> "")
            {
                private string sFile = Guid.NewGuid.ToString.Substring(0, 8);
                sFilename = UserSession.sGameFolder & IO.Path.DirectorySeparatorChar & sFile
                Display("Saving game...");
            Else
                Display("Save function not currently available for online play.  Please <a href=""https://www.adrift.co/download"">download</a> Runner app for enhanced functionality.");
                return false;
            }
#else
            private string sFilename = Adventure.sGameFilename;
            if (bSaveAs || Adventure.sGameFilename = "")
            {
                private New Windows.Forms.SaveFileDialog sfd;
                sfd.Filter = "ADRIFT Saved Games (*.tas)|*.tas|All Files (*.*)|*.*";
                sfd.FileName = sFilename;
                sfd.DefaultExt = "tas";
                If sfd.FileName.Length > 3 && ! sfd.FileName.ToLower.EndsWith("tas") Then sfd.FileName = ""
                if (sfd.ShowDialog() = DialogResult.Cancel)
                {
                    Display("Cancelled");
                    return false;
                }
                sFilename = sfd.FileName
            }
#endif

            if (sFilename <> "")
            {
                If ! sFilename.ToLower.EndsWith(".tas") Then sFilename &= ".tas"
                Adventure.sGameFilename = sFilename;
                private New StateStack ss;
                if (SaveState(ss.GetState, sFilename))
                {
#if www
                    Display("You can restore this with the command ""restore " + IO.Path.GetFileNameWithoutExtension(sFilename) + """");
#else
                    Display("Game """ + IO.Path.GetFileNameWithoutExtension(sFilename) + """ saved");
#endif
                    SaveSetting("ADRIFT", "Runner", "Game Path", IO.Path.GetDirectoryName(sFilename));
                    Adventure.Changed = false;
                    return true;
                Else
                    Display("Error saving game");
                    return false;
                }
            }

        }
        catch (Exception ex)
        {
            Display("Error saving game: " + ex.Message);
            return false;
        }

        return false;

    }


    internal void Restore(string sFilename = "")
    {

#if www
        if (sFilename = "")
        {
            Display("Please use the syntax ""restore xxxxxxxx""" + vbCrLf + vbCrLf, true);
            Exit Sub;
        }
        If ! sFilename.Contains(IO.Path.DirectorySeparatorChar) Then sFilename = UserSession.sGameFolder + IO.Path.DirectorySeparatorChar + sFilename
        sFilename &= ".tas";
#else
        if (sFilename <> "")
        {
            If ! sFilename.ToLower.EndsWith(".tas") Then sFilename &= ".tas"
        Else
            private New Windows.Forms.OpenFileDialog ofd;
            ofd.Filter = "ADRIFT Saved Games (*.tas)|*.tas|All Files (*.*)|*.*";
            ofd.DefaultExt = "tas";
            If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("tas") Then ofd.FileName = ""
            ofd.ShowDialog();
            sFilename = ofd.FileName
        }
#endif

        if (sFilename <> "")
        {
            if (IO.File.Exists(sFilename))
            {
                States.Clear();
                For Each t As clsTask In Adventure.htblTasks.Values
                    t.Completed = false ' Just in case the save file doesn't cover that task;
                Next;
                private New StateStack ss;
                ss.LoadState(sFilename);
                Display("Game """ + IO.Path.GetFileNameWithoutExtension(sFilename) + """ restored" + vbCrLf, true);
                Adventure.sGameFilename = sFilename;
                Adventure.eGameState = clsAction.EndGameEnum.Running;
                Adventure.bDisplayedWinLose = false;
                UpdateStatusBar();
                Display(Adventure.htblLocations(Adventure.Player.Location.LocationKey).ViewLocation, true);
                'If States.Count = 0 Then States.RecordState() ' So we're able to Undo immediately after a restore
                bSystemTask = False ' Allow events to run
                PrepareForNextTurn();
                '#If Not www Then
                UserSession.Map.RecalculateNode(Adventure.Map.FindNode(Adventure.Player.Location.LocationKey));
                UserSession.Map.SelectNode(Adventure.Player.Location.LocationKey);
                '#End If
            Else
                Display("Save file not found.", true);
            }
        }
    }


    internal bool bQuitting = false;
    internal bool Quit(bool bJustGame = false)
    {
        if (Adventure IsNot null && Adventure.eGameState = clsAction.EndGameEnum.Running)
        {
            if (Adventure.Changed)
            {
                switch (MessageBox.Show("Would you like to save your current position?", "Quit Game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                        SaveGame(true);
                    case DialogResult.No:
                        {
                        ' Continue
                    case DialogResult.Cancel:
                        {
                        return false;
                }
            }
        }

        if (bJustGame)
        {
            Adventure.eGameState = clsAction.EndGameEnum.Neutral;
        Else
            If ! bQuitting Then fRunner.Close()
        }

        return true;

    }


    internal void Undo()
    {
        if (States.SetLastState)
        {
            Adventure.bDisplayedWinLose = false;
            '#If Not www Then
            UserSession.Map.RecalculateNode(Adventure.Map.FindNode(Adventure.Player.Location.LocationKey));
            UserSession.Map.SelectNode(Adventure.Player.Location.LocationKey);
            '#End If
            UpdateStatusBar();
            'fRunner.Map.imgMap.Refresh()
            private string sText = sTurnOutput;
            Display("Undone.", , , false);
            If sText <> "" Then Display(sText)
        Else
            Display("Sorry, <c>undo</c> is not currently available.");
        }
        bSystemTask = True
    }


    private string GetBlock(string sText)
    {

        Dim cText() As Char = sText.ToCharArray
        private string sOut = "";
        private int iLevel = 0;

        For Each c As Char In cText
            switch (c)
            {
                case "{"c:
                case "["c:
                    {
                    iLevel += 1;
                case "}"c:
                case "]"c:
                    {
                    iLevel -= 1;
            }
            sOut &= c;
            If iLevel = 0 Then Return sOut
        Next;

        return sText;

    }


    private StringArrayList MySplit(string sText, ref iMinLength As Integer)
    {

        Dim sSplits() As String = Split(sText, "/")
        private New StringArrayList sal;
        private int iLevel = 0;
        private string s = "";

        For Each sSplit As String In sSplits
            If iLevel > 0 Then s &= "/"
            s &= sSplit;
            for (int i = 0; i <= sSplit.Length - 1; i++)
            {
                switch (sSplit(i))
                {
                    case "["c:
                    case "{"c:
                        {
                        iLevel += 1;
                    case "]"c:
                    case "}"c:
                        {
                        iLevel -= 1;
                }
            Next;
            if (iLevel = 0)
            {
                sal.Add(s);
                If s.Length < iMinLength Then iMinLength = s.Length
                s = ""
            }
        Next;

        return sal;

    }


    ' This should split any block, e.g. [examine/ex/x/look {at/in/under}] %object%
    ' into:
    ' examine %object%
    ' ex %object%
    ' x %object%
    ' look %object%
    ' look at %object%
    ' look in %object%
    ' look under %object%
    '
    ' [get/take/pick {up}] {the} hat {from Grandad}
    private StringArrayList ExpandBlock(string sBlock)
    {

        private New StringArrayList sal;

        if (sBlock = "")
        {
            sal.Add("");
            return sal;
        }

        private string sPre = System.Text.RegularExpressions.Regex.Replace(sBlock, "({|\[).*$", "");

        'If sPre.Length > txtInput.TextLength - 2 Then
        '    sal.Add(sPre)
        '    Return sal
        'End If

        private string sMid = GetBlock(sRight(sBlock, sBlock.Length - sPre.Length));
        private StringArrayList salPost = null;

        if (sMid <> "")
        {
            private bool bOptional = false;
            if (sLeft(sMid, 1) = "{")
            {
                salPost = ExpandBlock(sRight(sBlock, sBlock.Length - sMid.Length - sPre.Length))
                For Each sPost As String In salPost
                    sal.Add(sPre + sPost);
                Next;
                bOptional = True
            }
            sMid = Mid(sMid, 2, sMid.Length - 2)

            private int iMinLength = Integer.MaxValue;
            private StringArrayList sSplits = MySplit(sMid, iMinLength);
            if (salPost Is null && sPre.Length + iMinLength <= fRunner.txtInput.TextLength - 2)
            {
                salPost = ExpandBlock(sRight(sBlock, sBlock.Length - (sMid.Length + 2) - sPre.Length))
            }

            For Each sSplit As String In sSplits
                private StringArrayList salSplit = ExpandBlock(sSplit);
                For Each s As String In salSplit
                    If bOptional Then s = "@@@" + s
                    if (salPost IsNot null)
                    {
                        For Each sPost As String In salPost
                            If s <> "" && sPost <> "" && ! s.EndsWith(" ") && ! sPost.StartsWith(" ") Then sPost = " " + sPost
                            sal.Add(sPre + s + sPost);
                            If sal.Count > 1000 Then Return sal ' PK Girl can return millions of possibilities - Yikes, need to redesign!
                        Next;
                    Else
                        sal.Add(sPre + s);
                    }
                Next;
            Next;
        Else
            salPost = ExpandBlock(sRight(sBlock, sBlock.Length - sMid.Length - sPre.Length))
            For Each sPost As String In salPost
                sal.Add(sPre + sPost);
                If sal.Count > 1000 Then Return sal
            Next;
        }

        return sal;

    }


    internal void DoAutoComplete()
    {

        private RichTextBox txtInput = fRunner.txtInput;
        Dim sWords() As String = txtInput.Text.Substring(2).Split(" "c)
        private AutoComplete node = root;
        'Dim obnode As AutoComplete = obroot

        ' i             is exact match, has longer word
        ' in            is exact match, no longer word for some tasks, longer word for other
        ' inv           is exact match, has longer word
        ' inve          no exact match, has longer word
        ' inventory     is exact match, no longer word

        private New StringArrayList salAllowedTasks;
        For Each sWord As String In sWords

            private bool bLongerWord = false;
            if (Not node Is null)
            {
                if (node.htblChildren.ContainsKey(sWord))
                {
                    ' Ok, we've got a complete word.  Let's see if a task exists with longer version of it...
                    private AutoComplete acNode = node.htblChildren(sWord);
                    For Each sTask As String In acNode.salTasks
                        'If htblCompleteableGeneralTasks.ContainsKey(sTask) Then
                        For Each acChildNode As AutoComplete In node.htblChildren
                            if (Not acChildNode Is acNode)
                            {
                                if (acChildNode.salTasks.Contains(sTask))
                                {
                                    if (acChildNode.sWord.StartsWith(sWord) && acChildNode.sWord.Length > sWord.Length)
                                    {
                                        ' Ok, we've found a longer applicable word
                                        bLongerWord = True
                                        salAllowedTasks.Add(sTask);
                                        'GoTo AfterLongerWord
                                    }
                                }
                            }
                        Next;
                        'End If
                    Next;
                }
                'AfterLongerWord:

                ' We want it to select the word if it's a whole word in our list, and there's not a longer one available
                if (node.htblChildren.ContainsKey(sWord) && (txtInput.Text.EndsWith(sWord + " ") || Array.IndexOf(sWords, sWord) < sWords.Length - 1))
                {
                    node = node.htblChildren(sWord)
                    ListChildren(node, false);
                Else

                    if (sWord <> "" || (node.htblChildren.Count = 1 && Not node.htblChildren.ItemByIndex(0).sWord.StartsWith("%")))
                    {
                        For Each sChild As AutoComplete In node.htblChildren
                            private bool bOk = false;
                            if (salAllowedTasks.Count = 0)
                            {
                                bOk = True
                            Else
                                For Each sTask As String In salAllowedTasks
                                    if (sChild.salTasks.Contains(sTask))
                                    {
                                        bOk = True
                                        Exit For;
                                    }
                                Next;
                            }
                            if (bOk)
                            {
                                switch (sChild.sWord)
                                {
                                    case "%direction1%":
                                    case "%direction2%":
                                    case "%direction3%":
                                    case "%direction4%":
                                    case "%direction5%":
                                        {
                                        for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest; d++)
                                        {
                                            private StringArrayList sal = ExpandBlock(DirectionRE(d));
                                            For Each sDir As String In sal
                                                if (sDir = sWord)
                                                {
                                                    node = node.htblChildren(sChild.sWord)
                                                    ListChildren(node, false);
                                                    GoTo NextWord;
                                                ElseIf sDir.StartsWith(sWord) && sWord = sWords(sWords.Length - 1) Then
                                                    private string sRemainder = sDir.Substring(sWord.Length);

                                                    txtInput.SelectedText = sRemainder;
                                                    txtInput.SelectionStart = txtInput.Text.Length - sRemainder.Length;
                                                    txtInput.SelectionLength = sRemainder.Length;
                                                    Exit Sub;
                                                }
                                            Next;
                                        Next;

                                    case "%object1%":
                                    case "%object2%":
                                    case "%object3%":
                                    case "%object4%":
                                    case "%object5%":
                                    case "%objects%":
                                        {
                                        ' for all completable tasks in node.saltasks
                                        ' loop thru all objects refs that could pass the task
                                        ' Grab the text matching the actual object
                                        private string sObName = "";
                                        for (int i = sWords.Length - 1; i <= 0; i += -1)
                                        {
                                            if (sWords(i) = node.sWord)
                                            {
                                                for (int iOb = i + 1; iOb <= sWords.Length - 1; iOb++)
                                                {
                                                    sObName &= sWords(iOb) + " ";
                                                Next;
                                            }
                                        Next;
                                        If sObName.Length > 1 Then sObName = sLeft(sObName, sObName.Length - 1)
                                        private string[] sObWords = sObName.Split(" "c);

                                        private AutoComplete obnode = obroot;

                                        if (sChild.sWord = "%objects%")
                                        {
                                            For Each sAllWord As String In New String() {"all", "everything"}
                                                if (Not obnode.htblChildren.ContainsKey(sAllWord))
                                                {
                                                    obnode.htblChildren.Add(New AutoComplete(sAllWord));
                                                }
                                            Next;
                                        }
                                        For Each sObWord As String In sObWords
                                            if (obnode.htblChildren.ContainsKey(sObWord) && (txtInput.Text.EndsWith(sObWord + " ") || Array.IndexOf(sObWords, sObWord) < sObWords.Length))
                                            {
                                                obnode = obnode.htblChildren(sObWord)
                                                ListChildren(obnode, true);
                                                if (obnode.htblChildren.Count = 0)
                                                {
                                                    node = node.htblChildren(sChild.sWord)
                                                    ListChildren(node, false);
                                                    GoTo NextObWord;
                                                }
                                            Else
                                                if (sObWord <> "" || obnode.htblChildren.Count = 1)
                                                {
                                                    For Each sObChild As AutoComplete In obnode.htblChildren
                                                        if (sObChild.sWord.StartsWith(sObWord) && (sObChild.sWord = "all" || sObChild.sWord = "everything" || PlayerSeeOb(sObChild.salTasks, sChild.salTasks)) && sObWord = sWords(sWords.Length - 1))
                                                        {
                                                            'Debug.WriteLine(sChild.sWord)
                                                            private string sRemainder = sObChild.sWord.Substring(sObWord.Length);

                                                            txtInput.SelectedText = sRemainder;
                                                            txtInput.SelectionStart = txtInput.Text.Length - sRemainder.Length;
                                                            txtInput.SelectionLength = sRemainder.Length;
                                                            Exit Sub ' For;
                                                        }
                                                    Next;
                                                }
                                            }

NextObWord:;
                                        Next;

                                    case "%character1%":
                                    case "%character2%":
                                    case "%character3%":
                                    case "%character4%":
                                    case "character5%":
                                    case "%characters%":
                                        {
                                        ' for all completable tasks in node.saltasks
                                        ' loop thru all objects refs that could pass the task
                                        ' Grab the text matching the actual object
                                        private string sChName = "";
                                        for (int i = sWords.Length - 1; i <= 0; i += -1)
                                        {
                                            if (sWords(i) = node.sWord)
                                            {
                                                for (int iCh = i + 1; iCh <= sWords.Length - 1; iCh++)
                                                {
                                                    sChName &= sWords(iCh) + " ";
                                                Next;
                                            }
                                        Next;
                                        If sChName.Length > 1 Then sChName = sLeft(sChName, sChName.Length - 1)
                                        private string[] sChWords = sChName.Split(" "c);

                                        private AutoComplete chnode = chroot;

                                        For Each sChWord As String In sChWords
                                            if (chnode.htblChildren.ContainsKey(sChWord) && (txtInput.Text.EndsWith(sChWord + " ") || Array.IndexOf(sChWords, sChWord) < sChWords.Length))
                                            {
                                                chnode = chnode.htblChildren(sChWord)
                                                'ListChildren(chnode, True)
                                                if (chnode.htblChildren.Count = 0)
                                                {
                                                    node = node.htblChildren(sChild.sWord)
                                                    'ListChildren(node, False)
                                                    GoTo NextChWord;
                                                }
                                            Else
                                                if (sChWord <> "" || chnode.htblChildren.Count = 1)
                                                {
                                                    For Each sObChild As AutoComplete In chnode.htblChildren
                                                        if (sObChild.sWord.StartsWith(sChWord) && PlayerSeeCh(sObChild.salTasks, sChild.salTasks) && sChWord = sWords(sWords.Length - 1))
                                                        {
                                                            'Debug.WriteLine(sChild.sWord)
                                                            private string sRemainder = sObChild.sWord.Substring(sChWord.Length);

                                                            txtInput.SelectedText = sRemainder;
                                                            txtInput.SelectionStart = txtInput.Text.Length - sRemainder.Length;
                                                            txtInput.SelectionLength = sRemainder.Length;
                                                            Exit Sub ' For;
                                                        }
                                                    Next;
                                                }
                                            }

NextChWord:;
                                        Next;

                                        'If obnode.htblChildren.ContainsKey(sWord) AndAlso (txtInput.Text.EndsWith(sWord & " ") OrElse Array.IndexOf(sWords, sWord) < sWords.Length) Then
                                        '    obnode = obnode.htblChildren(sWord)
                                        '    ListChildren(obnode, True)
                                        '    If obnode.htblChildren.Count = 0 Then
                                        '        node = node.htblChildren(sChild.sWord)
                                        '        ListChildren(node, False)
                                        '        GoTo NextWord
                                        '    End If
                                        'Else
                                        '    If sWord <> "" OrElse obnode.htblChildren.Count = 1 Then
                                        '        For Each sObChild As AutoComplete In obnode.htblChildren
                                        '            If sObChild.sWord.StartsWith(sWord) AndAlso PlayerSeeOb(sObChild.salTasks, sChild.salTasks) Then
                                        '                'Debug.WriteLine(sChild.sWord)
                                        '                Dim sRemainder As String = sObChild.sWord.Substring(sWord.Length)

                                        '                txtInput.SelectedText = sRemainder
                                        '                txtInput.SelectionStart = txtInput.Text.Length - sRemainder.Length
                                        '                txtInput.SelectionLength = sRemainder.Length
                                        '                Exit Sub ' For
                                        '            End If
                                        '        Next
                                        '    End If
                                        'End If

                                    default:
                                        {
                                        if (sChild.sWord.StartsWith(sWord) && PlayerCanExTask(sChild.salTasks) && sWord = sWords(sWords.Length - 1))
                                        {
                                            'Debug.WriteLine(sChild.sWord)
                                            private string sRemainder = sChild.sWord.Substring(sWord.Length);

                                            bAllowBuildAutos = False
                                            txtInput.SelectedText = sRemainder;
                                            txtInput.SelectionStart = txtInput.Text.Length - sRemainder.Length;
                                            txtInput.SelectionLength = sRemainder.Length;
                                            bAllowBuildAutos = True
                                            Exit Sub ' For;
                                        }
                                }
                            }
                        Next;
                    }
                }
            }
NextWord:;
        Next;

    }


    ' This function should return True if, for any Task in salTaskList
    ' any Object in salObList could pass all the task references specific to that reference
    private bool PlayerSeeOb(StringArrayList salObList, StringArrayList salTaskList)
    {

        For Each sKey As String In salObList
            If Adventure.Player.CanSeeObject(sKey) Then Return true
        Next;

        return false;

    }


    private bool PlayerSeeCh(StringArrayList salChList, StringArrayList salTaskList)
    {

        For Each sKey As String In salChList
            If Adventure.Player.CanSeeCharacter(sKey) Then Return true
        Next;

        return false;

    }


    ' This function should return True if, not including any references, the task
    ' could be completed
    private bool PlayerCanExTask(StringArrayList salList)
    {

        For Each sKey As String In salList
            'Display(sKey, True) ' Debugging...
            If Adventure.htblTasks(sKey).IsCompleteable Then Return true
        Next;

        return false;

    }


    private void ListChildren(AutoComplete node, bool bObs)
    {

        If node.htblChildren.Count > 0 Then Debug.WriteLine("")
        For Each child As AutoComplete In node.htblChildren
            If ! bObs || PlayerSeeOb(child.salTasks, node.salTasks) Then Debug.WriteLine(child.sWord)
        Next;

    }


    private void SortNodes(AutoCompleteSortedArrayList nodes)
    {

        For Each node As AutoComplete In nodes
            If node.htblChildren.Count > 0 Then SortNodes(node.htblChildren)
        Next;
        nodes.Sort();

        ''Exit Sub
        'For Each node As AutoComplete In nodes
        '    Debug.WriteLine(node.sWord)
        'Next
        'Debug.WriteLine("---")
    }


    private void PrintOutNodes(AutoComplete node, int iLevel)
    {

        for (int i = 0; i <= iLevel - 1; i++)
        {
            'DisplayDebug("   ")
            Debug.Write("   ");
        Next;
        'DisplayDebug(node.sWord & vbCrLf)
        Debug.WriteLine(node.sWord + " [" + node.iPriority + "]");
        For Each n As AutoComplete In node.htblChildren
            PrintOutNodes(n, iLevel + 1);
        Next;

    }


    private bool bAllowBuildAutos = true;
    public void BuildAutos()
    {

        If ! bAutoComplete || ! bAllowBuildAutos || Adventure == null Then Exit Sub
        If fRunner.txtInput == null || fRunner.txtInput.IsDisposed Then Exit Sub
        If fRunner.txtInput.TextLength < 2 Then Exit Sub

        private DateTime dtAutos = Now;
        private string sInput = fRunner.txtInput.Text.Substring(2);
        sInput = sLeft(sInput, fRunner.txtInput.SelectionStart - 2)

        root = New AutoComplete
        root.sWord = null;

        For Each tas As clsTask In htblCompleteableGeneralTasks.Values
            if (tas.AutoFillPriority > 0)
            {
                For Each sCommand As String In tas.arlCommands
                    private StringArrayList salList = ExpandBlock(sCommand);
                    For Each sList As String In salList
                        if (sInput = "" || sList.StartsWith(sInput) || sList.Contains("%"))
                        {
                            private int iPriorityOffset = 0;
                            sList = sList.Replace("*", "")
                            while (sList.Contains("  "))
                            {
                                sList = sList.Replace("  ", " ")
                            }
                            If sList.Contains("@@@") Then ' This prevents "{walk} west" from giving us w[alk] instead of w[est]
                                If sList.StartsWith("@@@") Then iPriorityOffset = 1
                                sList = sList.Replace("@@@", "")
                            }
                            If sList.StartsWith(" ") Then sList = sRight(sList, sList.Length - 1)
                            'Debug.WriteLine(sList)
                            private AutoComplete node = root;
                            Dim sWords() As String = Split(sList, " ")
                            For Each sWord As String In sWords
                                if (Not node.htblChildren.ContainsKey(sWord))
                                {
                                    private New AutoComplete newnode;
                                    newnode.sWord = sWord;
                                    node.htblChildren.Add(newnode);
                                }
                                node = node.htblChildren(sWord)
                                If ! node.salTasks.Contains(tas.Key) Then node.salTasks.Add(tas.Key)
                                If tas.AutoFillPriority < node.iPriority Then node.iPriority = tas.AutoFillPriority + iPriorityOffset
                            Next;
                        }
                    Next;
                Next;
            }
        Next;

        'PrintOutNodes(root, 0)

        if (obroot Is null)
        {
            obroot = New AutoComplete
            obroot.sWord = null;

            For Each ob As clsObject In Adventure.htblObjects.Values
                private StringArrayList salList = ExpandBlock(ob.sRegularExpressionString(true));
                For Each sList As String In salList
                    private int iPriorityOffset = 0;
                    while (sList.Contains("  "))
                    {
                        sList = sList.Replace("  ", " ")
                    }
                    If sList.Contains("@@@") Then ' This prevents "{space} ship" from giving us s[pace] instead of s[hip]
                        If sList.StartsWith("@@@") Then iPriorityOffset = 1
                        sList = sList.Replace("@@@", "")
                    }
                    If sList.StartsWith(" ") Then sList = sRight(sList, sList.Length - 1)
                    private AutoComplete node = obroot;
                    Dim sWords() As String = Split(sList, " ")
                    For Each sWord As String In sWords
                        if (Not node.htblChildren.ContainsKey(sWord))
                        {
                            'Dim newnode As New AutoComplete(sWord)
                            'newnode.sWord = sWord
                            'node.htblChildren.Add(newnode)
                            node.htblChildren.Add(New AutoComplete(sWord));
                        }
                        node = node.htblChildren(sWord)
                        If ! node.salTasks.Contains(ob.Key) Then node.salTasks.Add(ob.Key)
                        node.iPriority = iPriorityOffset;
                    Next;
                Next;
            Next;
            SortNodes(obroot.htblChildren);

            chroot = New AutoComplete
            chroot.sWord = null;

            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                private StringArrayList salList = ExpandBlock(ch.sRegularExpressionString(true));
                For Each sList As String In salList
                    while (sList.Contains("  "))
                    {
                        sList = sList.Replace("  ", " ")
                    }
                    If sList.StartsWith(" ") Then sList = sRight(sList, sList.Length - 1)
                    private AutoComplete node = chroot;
                    Dim sWords() As String = Split(sList, " ")
                    For Each sWord As String In sWords
                        if (Not node.htblChildren.ContainsKey(sWord))
                        {
                            private New AutoComplete newnode;
                            newnode.sWord = sWord;
                            node.htblChildren.Add(newnode);
                        }
                        node = node.htblChildren(sWord)
                        If ! node.salTasks.Contains(ch.Key) Then node.salTasks.Add(ch.Key)
                    Next;
                Next;
            Next;
        }

        'root.htblChildren.Sort()
        SortNodes(root.htblChildren);
        'DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.High, "Built Autos")

        'Debug.WriteLine("-----------------------------")
        'PrintOutNodes(root, 0)

        Debug.WriteLine("Autos built in " + Now.Subtract(dtAutos).ToString);

    }


    private string EvaluateInput(int iMinimumPriority, bool bPassingOnly)
    {

        private char cCursor = "Ø"c;
        private char cCommentCursor = "@"c;
        private string sCursorFont = "Wingdings";

#if Mono
        cCursor = ChrW(&H27A2)
        cCommentCursor = ChrW(&H270D)
        sCursorFont = "OpenSymbol"
#endif

        private bool bComment = false;
#if Not www
        bComment = (fRunner.txtInput.Text.Length > 0 AndAlso fRunner.txtInput.Text(0) = cCommentCursor)
#endif

        InitialiseInputBox();

        if (bComment)
        {
            Display("<c><font face=""" + sCursorFont + """ size=18>" + cCommentCursor + "</font> " + sInput + "</c>" + vbCrLf, true, false);
            return "";
        }

        if (iMinimumPriority = 0)
        {
            Display("<c><font face=""" + sCursorFont + """ size=14>" + cCursor + "</font> " + sInput + "</c>" + vbCrLf, true, false, false);
            sInput = sInput.ToLower
            if (ContainsWord(sInput, "it"))
            {
                Display("<c>(" + sIt + ")</c>" + vbCrLf, true);
                ReplaceWord(sInput, "it", sIt);
                'sInput = sInput.Replace(" it ", " " & sIt & " ")
                'If sInput.StartsWith("it ") Then sInput = sIt & " " & sRight(sInput, sInput.Length - 3)
                'If sInput.EndsWith(" it") Then sInput = sLeft(sInput, sInput.Length - 3) & " " & sIt
                'If sInput = "it" Then sInput = sIt
            }
            if (ContainsWord(sInput, "them"))
            {
                Display("<c>(" + sThem + ")</c>" + vbCrLf, true);
                ReplaceWord(sInput, "them", sThem);
            }
            if (ContainsWord(sInput, "him"))
            {
                Display("<c>(" + sHim + ")</c>" + vbCrLf, true);
                ReplaceWord(sInput, "him", sHim);
            }
            if (ContainsWord(sInput, "her"))
            {
                Display("<c>(" + sHer + ")</c>" + vbCrLf, true);
                ReplaceWord(sInput, "her", sHer);
            }
            if ((sInput = "again" || sInput = "g") && salCommands.Count > 2)
            {
                Display("<c>(" + salCommands(salCommands.Count - 3) + ")</c>" + vbCrLf, true);
                sInput = salCommands(salCommands.Count - 3)
                salCommands.RemoveAt(salCommands.Count - 2) ' Don't store 'again';
            }
            GrabIt();

            private string sPreSyn = sInput;
            For Each syn As clsSynonym In Adventure.htblSynonyms.Values
                For Each sFrom As String In syn.ChangeFrom
                    if (ContainsWord(sInput, sFrom))
                    {
                        ReplaceWord(sInput, sFrom, syn.ChangeTo);
                    }
                Next;
            Next;
            if (sInput <> sPreSyn)
            {
                DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.Medium, "Synonyms changed input """ + sPreSyn + """ to """ + sInput + """");
            }

            'If ContainsWord(sInput, "me") Then ReplaceWord(sInput, "me", Adventure.Player.Name)

            If CBool(GetSetting("ADRIFT", "Runner", "BlankLine", "0")) Then Display(vbCrLf)
        }

        sInput = sInput.ToLower

        ' Don't actually respond to the tasks here, in case the user has created a task to override the system one.
        if (Adventure.eGameState <> clsAction.EndGameEnum.Running || Adventure.dVersion < 5)
        {
            if (SystemTasks(true))
            {
                return "";
            Else
                if (Adventure.eGameState <> clsAction.EndGameEnum.Running)
                {
                    Display("Please give one of the answers above." + vbCrLf);
                    return "";
                }
            }
        }

        private string sTaskKey = null;

        if (sAmbTask IsNot null)
        {
            sTaskKey = ResolveAmbiguity(sInput)

            if (Not sTaskKey Is null)
            {
                sRestrictionTextMulti = ""
                sRestrictionText = sRestrictionTextMulti
            }
        Else
            sLastProperInput = sInput
            Erase NewReferences;
        }

        ' only run this if we're not resolving an ambiguity or it left no tasks
        if (sTaskKey Is null)
        {
            Dim cRefCopy(-1) As clsNewReference
            private bool bCopied = false;
            private string sRememberAmbTask = null;
            for (int i = 0; i <= 4; i++)
            {
                Adventure.sReferencedText(i) = "";
                'Adventure.iReferencedNumber(i) = 0
            Next;
            ' If Not sAmbTask Is Nothing Then remember References, so we can reapply them if we don't get a new command
            if (Not sAmbTask Is null)
            {
                ReDim cRefCopy(NewReferences.Length - 1);
                Array.Copy(NewReferences, cRefCopy, NewReferences.Length);
                bCopied = True
                sRememberAmbTask = sAmbTask
            }
            'GetMentionedObs()
            sTaskKey = GetGeneralTask(sInput, iMinimumPriority, False)
            If Adventure.sReferencedText(0) = "" Then Adventure.sReferencedText(0) = sInput
            ' And If sTaskKey Is Nothing Then restore here
            If sTaskKey == null && sAmbTask != null && iMinimumPriority > 0 && sOutputText <> "" Then sAmbTask = null ' Suppress ambiguity question if we're running further down task list and we already have a failure response
            If sTaskKey == null && sAmbTask == null && ! sRememberAmbTask == null Then sAmbTask = sRememberAmbTask
            if (sTaskKey Is null && Not sAmbTask Is null && bCopied)
            {
                ReDim NewReferences(cRefCopy.Length - 1);
                Array.Copy(cRefCopy, NewReferences, cRefCopy.Length);
            }

        }

        if (Not sAmbTask Is null && sTaskKey Is null)
        {
            ' Display ambiguity question
            DisplayAmbiguityQuestion();
        Else
            sAmbTask = Nothing
            if (sTaskKey IsNot null)
            {
                'RemoveExcepts()
                'htblReferencesFail.Clear()
                'CalcFailRefs(References, Adventure.htblTasks(sTaskKey).arlRestrictions)
                private clsTask taskRun = Adventure.htblTasks(sTaskKey);
                NewReferences = taskRun.NewReferencesWorking
                AttemptToExecuteTask(sTaskKey, , , , , , , bPassingOnly);
                If taskRun.bSystemTask Then bSystemTask = true

                while (Adventure.qTasksToRun.Count > 0)
                {
                    private string sKey = Adventure.qTasksToRun.Dequeue;
                    AttemptToExecuteTask(sKey, true);
                }

                if (iMinimumPriority = 0)
                {
                    if (sOutputText = "")
                    {
                        NotUnderstood();
                    Else
                        ' Ok, successful task.  So long as we didn't execute a system task
                        If ! bSystemTask Then TurnBasedStuff()
                        Adventure.Changed = true;
                        sRememberedVerb = ""
                    }
                }
            Else
                if (iMinimumPriority = 0)
                {
                    if (Adventure.eGameState = clsAction.EndGameEnum.Running)
                    {
                        If ! SystemTasks() Then NotUnderstood()
                        bSystemTask = True
                    }
                }
            }
            If iMinimumPriority = 0 && sOutputText <> "***SYSTEM***" Then Display(vbCrLf)
        }

        if (sOutputText <> "***SYSTEM***")
        {
            if (iMinimumPriority = 0)
            {
                Display(vbCrLf, true);
                DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.Low, "ENDOFTURN");
            }
            Debug.WriteLine(Now.Subtract(dtDebug).ToString());

            UpdateStatusBar();
        }

        return sOutputText;

    }



private class TaskSorter
    {
        Implements System.Collections.Generic.IComparer(Of clsTask);

        public Integer Implements System.Collections.Generic.IComparer<clsTask> Compare(clsTask x, clsTask y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }

    private void NotUnderstood()
    {

        if (sRememberedVerb <> "")
        {
            sInput = sRememberedVerb & " " & sInput
            sRememberedVerb = ""
            EvaluateInput(-1, false);
            If sOutputText <> "" Then Exit Sub
        }

        With Adventure;
            if (.lstTasks Is null)
            {
                .lstTasks = New List(Of clsTask);
                For Each tas As clsTask In .htblTasks.Values
                    .lstTasks.Add(tas);
                Next;
                .lstTasks.Sort(New TaskSorter);
            }
            if (.listKnownWords Is null)
            {
                .listKnownWords = New List(Of String);
                ' Verbs
                For Each tas As clsTask In .lstTasks ' Adventure.htblTasks.Values
                    if (tas.TaskType = clsTask.TaskTypeEnum.General)
                    {
                        For Each sCommand As String In tas.arlCommands
                            sCommand = sCommand.Replace("[", " ").Replace("]", " ").Replace("{", " ").Replace("}", " ").Replace("/", " ")
                            while (sCommand.Contains("  "))
                            {
                                sCommand = sCommand.Replace("  ", " ")
                            }
                            For Each sWord As String In sCommand.Split(" "c)
                                If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                            Next;
                        Next;
                    }
                Next;
                ' Nouns
                For Each ob As clsObject In .htblObjects.Values
                    If ! .listKnownWords.Contains(ob.Article) Then .listKnownWords.Add(ob.Article)
                    For Each sWord As String In ob.Prefix.Split(" "c)
                        If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                    Next;
                    For Each sWord As String In ob.arlNames
                        If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                    Next;
                Next;
                For Each ch As clsCharacter In .htblCharacters.Values
                    If ! .listKnownWords.Contains(ch.Article) Then .listKnownWords.Add(ch.Article)
                    if (ch.Prefix IsNot null)
                    {
                        For Each sWord As String In ch.Prefix.Split(" "c)
                            If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                        Next;
                    }
                    For Each sWord As String In ch.arlDescriptors
                        If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                    Next;
                    If ! .listKnownWords.Contains(ch.ProperName) Then .listKnownWords.Add(ch.ProperName.ToLower)
                Next;
                ' Adverbs
                For Each sWord As String In sDirectionsRE.Split("|"c)
                    If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                Next;
                ' Additional words
                For Each sWord As String In New String() {"and"}
                    If ! .listKnownWords.Contains(sWord) Then .listKnownWords.Add(sWord)
                Next;
            }

            ' Check all words typed, to ensure they are valid
            For Each sWord As String In sInput.Split(" "c)
                if (Not .listKnownWords.Contains(sWord))
                {
                    Display("I did not understand the word """ + sWord + """.");
                    Exit Sub;
                }
            Next;

            ' If the user just entered a verb, check to see if we understand verb noun
            if (Not sInput.Contains(" "))
            {
                For Each tas As clsTask In .lstTasks 'Adventure.htblTasks.Values
                    if (tas.TaskType = clsTask.TaskTypeEnum.General)
                    {
                        if (tas.arlCommands(0).Contains(sInput) && tas.arlCommands(0).Contains(" "))
                        {
                            if (tas.arlCommands(0).Contains("%object"))
                            {
                                For Each lRegEx As List(Of System.Text.RegularExpressions.Regex) In tas.RegExs
                                    For Each RegEx As System.Text.RegularExpressions.Regex In lRegEx
                                        if (RegEx.IsMatch(sInput + " sdkfjdslkj"))
                                        {
                                            sRememberedVerb = sInput
                                            Display(PCase(sInput) + " what?");
                                            Exit Sub;
                                        }
                                    Next;
                                Next;
                            ElseIf tas.arlCommands(0).Contains("%character") Then
                                For Each lRegEx As List(Of System.Text.RegularExpressions.Regex) In tas.RegExs
                                    For Each RegEx As System.Text.RegularExpressions.Regex In lRegEx
                                        if (RegEx.IsMatch(sInput + " sdkfjdslkj"))
                                        {
                                            sRememberedVerb = sInput
                                            Display(PCase(sInput) + " who?");
                                            Exit Sub;
                                        }
                                    Next;
                                Next;
                            ElseIf tas.arlCommands(0).Contains("%direction%") Then
                                For Each lRegEx As List(Of System.Text.RegularExpressions.Regex) In tas.RegExs
                                    For Each RegEx As System.Text.RegularExpressions.Regex In lRegEx
                                        if (RegEx.IsMatch(sInput + " " + DirectionName(DirectionsEnum.North)))
                                        {
                                            sRememberedVerb = sInput
                                            Display(PCase(sInput) + " where?");
                                            Exit Sub;
                                        }
                                    Next;
                                Next;
                            }
                        }
                    }
                Next;
            }

            ' If the user just entered a noun, give a default message
            For Each ob As clsObject In Adventure.htblObjects.Values
                if (Adventure.Player.HasSeenObject(ob.Key))
                {
                    if (Adventure.Player.CanSeeObject(ob.Key))
                    {
                        private New System.Text.RegularExpressions.Regex(ob.sRegularExpressionString) re;
                        if (re.IsMatch(sInput))
                        {
                            Display("I don't understand what you want to do with " + ob.FullName(ArticleTypeEnum.Definite) + ".");
                            Exit Sub;
                        }
                    Else
                        ' Hmm, should we give a response when mentioning objects seen but not visible
                    }
                }
            Next;
            For Each ch As clsCharacter In Adventure.htblCharacters.Values
                if (Adventure.Player.HasSeenCharacter(ch.Key))
                {
                    if (Adventure.Player.CanSeeCharacter(ch.Key))
                    {
                        private New System.Text.RegularExpressions.Regex(ch.sRegularExpressionString) re;
                        if (re.IsMatch(sInput))
                        {
                            Display("I don't understand what you want to do with " + ch.Name + ".");
                            Exit Sub;
                        }
                    Else
                        ' Hmm, should we give a response when mentioning characters seen but not visible
                    }
                }
            Next;
        }

        Display(Adventure.NotUnderstood);
    }


    private bool SystemTasks(bool bEarly = false)
    {

        SystemTasks = True
        switch (sInput)
        {
            case "restart":
                {
                Restart();
                If ! bEarly Then sOutputText = "***SYSTEM***"
            case "restore":
                {
                Restore();
                Display(vbCrLf + vbCrLf, true);
                If ! bEarly Then sOutputText = "***SYSTEM***"
            case "save":
                {
                If bEarly Then Return false Else SaveGame()
            case "save as":
            case "saveas":
                {
                If bEarly Then Return false Else SaveGame(true)
            case "quit":
                {
                Quit(! bEarly);
            case "undo":
                {
                Undo();
                If bEarly Then Display(vbCrLf + vbCrLf, true)
            case "wait":
            case "z":
                {
                if (bEarly)
                {
                    return false;
                Else
                    sOutputText = ReplaceALRs("Time passes...") ' TODO - Bring this into Developer as a task
                    for (int i = 0; i <= Adventure.WaitTurns - 1; i++)
                    {
                        TurnBasedStuff();
                    Next;
                }
            default:
                {
                If bEarly Then Return false
                if (sInput.StartsWith("save ") && CharacterCount(sInput, " "c) = 1)
                {
                    private string sFilename = sInput.Replace("save ", "");
                    If sFilename.StartsWith("""") && sFilename.EndsWith("""") Then sFilename = sFilename.Substring(1, sFilename.Length - 2)
                    For Each c As Char In IO.Path.GetInvalidPathChars
                        if (sFilename.Contains(c))
                        {
                            Display("Bad Filename");
                            return true;
                        }
                    Next;
                    if (Not sFilename.Contains(IO.Path.DirectorySeparatorChar))
                    {
                        if (Adventure.sGameFilename <> "")
                        {
                            private string sPath = IO.Path.GetDirectoryName(Adventure.sGameFilename);
                            sFilename = sPath & IO.Path.DirectorySeparatorChar & sFilename
                        Else
                            sFilename = GetSetting("ADRIFT", "Runner", "Game Path", System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) & "\" & sFilename
                        }
                    }
                    Adventure.sGameFilename = sFilename;
                    SaveGame();
                    return true;
                }

                if (sInput.StartsWith("restore ") && CharacterCount(sInput, " "c) = 1)
                {
                    private string sFilename = sInput.Replace("restore ", "");
                    If sFilename.StartsWith("""") && sFilename.EndsWith("""") Then sFilename = sFilename.Substring(1, sFilename.Length - 2)
                    For Each c As Char In IO.Path.GetInvalidPathChars
                        if (sFilename.Contains(c))
                        {
                            Display("Bad Filename");
                            return true;
                        }
                    Next;
                    if (Not sFilename.Contains(IO.Path.DirectorySeparatorChar))
                    {
                        if (Adventure.sGameFilename <> "")
                        {
                            private string sPath = IO.Path.GetDirectoryName(Adventure.sGameFilename);
                            sFilename = sPath & IO.Path.DirectorySeparatorChar & sFilename
                        Else
                            sFilename = GetSetting("ADRIFT", "Runner", "Game Path", System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) & "\" & sFilename
                        }
                    }
                    Restore(sFilename);
                    return true;
                }
                return false;
        }

    }


    public void UpdateStatusBar()
    {
        'fRunner.UltraStatusBar1.Text = Adventure.htblLocations(Adventure.Player.Location.Key).ShortDescription
        If Adventure == null Then Exit Sub

        try
        {
            private string sDescription = "";
            private string sScore = "";
            private string sUserStatus = "";

            if (Adventure.eGameState = clsAction.EndGameEnum.Running)
            {
                if (Adventure.Player IsNot null)
                {
                    if (Adventure.Player.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden || Adventure.Player.Location.LocationKey = "")
                    {
                        sDescription = "(Nowhere)"
                    Else
                        sDescription = Adventure.htblLocations(Adventure.Player.Location.LocationKey).ShortDescriptionSafe
                        'If Adventure.dVersion < 5 Then
                        '    ' v4 didn't replace ALRs on statusbar and map
                        '    sDescription = StripCarats(ReplaceFunctions(Adventure.htblLocations(Adventure.Player.Location.LocationKey).ShortDescription.ToString))
                        'Else
                        '    sDescription = StripCarats(ReplaceALRs(Adventure.htblLocations(Adventure.Player.Location.LocationKey).ShortDescription.ToString))
                        'End If
                        if (Adventure.Player.Location.ExistWhere <> clsCharacterLocation.ExistsWhereEnum.AtLocation)
                        {
                            sDescription &= " (" + Adventure.Player.Location.ToString + ")";
                        }
                    }
                }
                sUserStatus = Adventure.sUserStatus
            Else
                sDescription = "Game Over"
            }

            If Adventure.MaxScore > 0 Then sScore = "Score: " + Adventure.Score

            If ! Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) Then sScore = ""
            fRunner.UpdateStatusBar(sDescription, sScore, sUserStatus);

            '#If Not www Then
            if (UserSession.Map.Map IsNot null)
            {
                private MapNode node = UserSession.Map.Map.FindNode(Adventure.Player.Location.LocationKey);
                If node != null Then node.Text = sDescription
            }
            '#End If
            '            With fRunner.StatusBar
            '                '#If Not www Then
            '                If Adventure.eGameState = clsAction.EndGameEnum.Running Then
            '                    If Adventure.Player IsNot Nothing Then
            '                        If Adventure.Player.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden OrElse Adventure.Player.Location.LocationKey = "" Then
            '                            .Panels("Description").Text = "(Nowhere)"
            '                        Else
            '                            .Panels("Description").Text = StripCarats(ReplaceALRs(Adventure.htblLocations(Adventure.Player.Location.LocationKey).ShortDescription.ToString))
            '#If Not www Then
            '                            If fRunner.Map.Map IsNot Nothing Then
            '                                Dim node As MapNode = fRunner.Map.Map.FindNode(Adventure.Player.Location.LocationKey)
            '                                If node IsNot Nothing Then node.Text = .Panels("Description").Text
            '                            End If
            '#End If
            '                            If Adventure.Player.Location.ExistWhere <> clsCharacterLocation.ExistsWhereEnum.AtLocation Then
            '                                .Panels("Description").Text &= " (" & Adventure.Player.Location.ToString & ")"
            '                            End If
            '                        End If
            '                    End If
            '                Else
            '                    .Panels("Description").Text = "Game Over"
            '                End If
            '                If Adventure.MaxScore > 0 Then
            '                    .Panels("Score").Text = "Score: " & Adventure.Score
            '#If Mono OrElse www Then
            '                Else
            '                    .Panels("Score").Text = ""
            '                    .Panels("Score").MinWidth = 1
            '                    .Panels("Score").Width = 1
            '#Else
            '                    .Panels("Score").Visible = True
            '                Else
            '                    .Panels("Score").Visible = False
            '#End If

            '                End If
            '                '#End If
            '            End With
        }
        catch (ObjectDisposedException exOD)
        {
            ' Fail silently
        }
        catch (Exception ex)
        {
            ErrMsg("UpdateStatusBar error", ex);
        }

    }

    internal bool bEventsRunning = false;
    private void TurnBasedStuff()
    {

        If Adventure.eGameState <> clsAction.EndGameEnum.Running Then Exit Sub

        For Each c As clsCharacter In Adventure.htblCharacters.Values
            For Each w As clsWalk In c.arlWalks
                If Adventure.eGameState <> clsAction.EndGameEnum.Running Then Exit Sub
                w.IncrementTimer();
            Next;
        Next;
        bEventsRunning = True
        For Each e As clsEvent In Adventure.htblEvents.Values
            If Adventure.eGameState <> clsAction.EndGameEnum.Running Then Exit Sub
            If e.EventType = clsEvent.EventTypeEnum.TurnBased Then e.IncrementTimer()
        Next;
        ' Needs to be a separate loop in case a later event runs a task that starts an earlier event
        For Each e As clsEvent In Adventure.htblEvents.Values
            If e.EventType = clsEvent.EventTypeEnum.TurnBased Then e.bJustStarted = false
        Next;
        bEventsRunning = False

    }
    private void TimeBasedStuff()
    {

        If Adventure.eGameState <> clsAction.EndGameEnum.Running || fRunner.Locked Then Exit Sub

        bEventsRunning = True

        For Each e As clsEvent In Adventure.htblEvents.Values
            If Adventure.eGameState <> clsAction.EndGameEnum.Running Then Exit For
            If e.EventType = clsEvent.EventTypeEnum.TimeBased Then e.IncrementTimer()
        Next;
        ' Needs to be a separate loop in case a later event runs a task that starts an earlier event
        For Each e As clsEvent In Adventure.htblEvents.Values
            If e.EventType = clsEvent.EventTypeEnum.TimeBased Then e.bJustStarted = false
        Next;
        bEventsRunning = False
#if Not www
        If Debugger != null && Debugger.HasText && sOutputText <> "" Then DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.Low, "ENDOFTURN")
#endif
        If sOutputText <> "" Then Display("", true)

        CheckEndOfGame();

    }


    'Private Sub RemoveExcepts()

    '    If References Is Nothing Then Exit Sub

    '    ' First, remove any non-except refs where we have an except ref
    '    For iRef As Integer = 0 To References.Length - 1
    '        For Each srEx As SingleRef In References(iRef).alReferences
    '            If srEx.bExcept Then
    '                For Each srOk As SingleRef In References(iRef).alReferences
    '                    If Not srOk.bExcept Then
    '                        If srEx.salWhat(0) = srOk.salWhat(0) Then srOk.salWhat(0) = ""
    '                    End If
    '                Next srOk
    '            End If
    '        Next srEx
    '    Next
    '    ' Then remove the except refs
    '    For iRef As Integer = 0 To References.Length - 1
    '        For Each srEx As SingleRef In References(iRef).alReferences
    '            If srEx.bExcept Then srEx.salWhat(0) = ""
    '        Next srEx
    '    Next

    '    RemoveBlankRefs()

    'End Sub


    'Private Sub RemoveBlankRefs()

    '    For iRef As Integer = References.Length - 1 To 0 Step -1
    '        For irefSA As Integer = References(iRef).alReferences.Count - 1 To 0 Step -1
    '            If References(iRef).alReferences(irefSA).salWhat.Count > 0 Then
    '                'If References(iRef).alReferences(irefSA).salWhat(0) = "" Then
    '                '    References(iRef).alReferences.RemoveAt(irefSA)
    '                '    If References(iRef).alReferences(irefSA).salWhat.Count = 0 Then Exit For
    '                'End If
    '                For iSal As Integer = References(iRef).alReferences(irefSA).salWhat.Count - 1 To 0 Step -1
    '                    If References(iRef).alReferences(irefSA).salWhat(iSal) = "" Then
    '                        References(iRef).alReferences(irefSA).salWhat.RemoveAt(iSal)
    '                    End If
    '                Next
    '                If References(iRef).alReferences(irefSA).salWhat.Count = 0 Then
    '                    References(iRef).alReferences.RemoveAt(irefSA)
    '                End If
    '            End If
    '        Next
    '        If References(iRef).alReferences.Count = 0 Then
    '            If References.Length = 1 Then
    '                ReDim References(-1)
    '            Else
    '                For iR As Integer = iRef To References.Length - 2
    '                    References(iR) = References(iR + 1)
    '                Next
    '                ReDim Preserve References(References.Length - 1)
    '            End If
    '        End If
    '    Next

    'End Sub


    private void PrepareForNextTurn()
    {
        '#If Not www Then
        UserSession.Map.imgMap.Refresh();
        '#End If
        If ! bSystemTask && Adventure.eGameState = clsAction.EndGameEnum.Running Then States.RecordState()

        sTurnOutput = ""
        'bTaskHasOutput = False

        ' Mark objects as seen for all the characters
        For Each ch As clsCharacter In Adventure.htblCharacters.Values
            For Each ob As clsObject In Adventure.htblObjects.Values
                If ch.CanSeeObject(ob.Key) Then ob.SeenBy(ch.Key) = true
            Next;
            'For Each l As clsLocation In Adventure.htblLocations.Values
            '    If ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation Then
            '        l.SeenBy(ch.Key) = True
            '    End If
            'Next
            'For Each sLocKey As String In ch.LocationRoots.Keys
            private string sLocKey = ch.Location.LocationKey;
            if (sLocKey <> HIDDEN && sLocKey <> "")
            {
                private clsLocation locChar = Adventure.htblLocations(sLocKey);
                If locChar != null Then locChar.SeenBy(ch.Key) = true
            }

            'Next
            For Each ch2 As clsCharacter In Adventure.htblCharacters.Values
                if (Not ch.SeenBy(ch2.Key) && ch.CanSeeCharacter(ch2.Key))
                {
                    ch.SeenBy(ch2.Key) = true;
                }
            Next;
            'ch.bTurnMention = False
            ch.dictHasRouteCache.Clear();
            ch.dictRouteErrors.Clear();
            if (ch.Introduced)
            {
                if (ch IsNot Adventure.Player && Not Adventure.Player.CanSeeCharacter(ch.Key))
                {
                    ch.Introduced = false;
                }
            }
        Next;
        For Each eGen As clsCharacter.GenderEnum In New clsCharacter.GenderEnum() {clsCharacter.GenderEnum.Male, clsCharacter.GenderEnum.Female, clsCharacter.GenderEnum.Unknown}
            Adventure.lCharMentionedThisTurn(eGen) = New Generic.List(Of String);
        Next;

        'htblCompleteableGeneralTasks = Adventure.htblTasks
        ' Fill htblCompleteableGeneralTasks with tasks
        ' This is WAY too slow...
        htblCompleteableGeneralTasks.Clear();
        For Each tas As clsTask In Adventure.Tasks(clsAdventure.TasksListEnum.GeneralTasks).Values
            if (tas.TaskType = clsTask.TaskTypeEnum.General && (Not tas.Completed || tas.Repeatable))
            {
                htblCompleteableGeneralTasks.Add(tas, tas.Key);
            }
        Next;

        ' We can't do this, because we need failing restriction tasks also
        ' but we can if we only include any with text output...
        'Dim t As New Threading.Thread(AddressOf CalculateCompleteableTasks)
        't.IsBackground = True
        't.Name = "Background"
        't.Start()
        iPrepProgress = 2

        htblVisibleObjects.Clear();
        htblSeenObjects.Clear();
        For Each ob As clsObject In Adventure.htblObjects.Values
            If ob.SeenBy(Adventure.Player.Key) Then htblSeenObjects.Add(ob, ob.Key)
            If ob.IsVisibleTo(Adventure.Player.Key) Then htblVisibleObjects.Add(ob, ob.Key)
            ob.PrevParent = ob.Parent;
        Next;
        For Each ch As clsCharacter In Adventure.htblCharacters.Values
            ch.PrevParent = ch.Parent;
        Next;

        PronounKeys.Clear();
        'Adventure.dictRandValues = New Dictionary(Of String, List(Of Integer))

        BuildAutos();
        'CalcValidReferences()

    }



    ' Runs this on a background thread so if user types a command we just resume where we are
    private void CalculateCompleteableTasks()
    {

        private New TaskHashTable htblTasks;

        try
        {
            ' TODO: This should only add if completeable in current room, i.e. pass all restrictions ignoring ones regarding references.
            For Each sKey As String In htblCompleteableGeneralTasks.Keys
                private clsTask task = Adventure.htblTasks(sKey);
                If iPrepProgress = 1 || task.HasReferences || task.IsCompleteable Then htblTasks.Add(task, sKey)
            Next;
        Catch
            htblTasks = htblCompleteableGeneralTasks
        }

        htblCompleteableGeneralTasks = htblTasks
        iPrepProgress = 2

    }


    private string ResolveAmbiguity(string sInput)
    {

        private bool bResolved = false;

        ResolveAmbiguity = sAmbTask ' the default unless we're still ambiguous

        for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
        {
            Debug.WriteLine("Reference " + iRef);
            if (NewReferences(iRef) IsNot null)
            {
                With NewReferences(iRef);
                    Debug.WriteLine("Number of Refs in this Reference: " + .Items.Count);
                    for (int i = 0; i <= .Items.Count - 1; i++)
                    {
                        private StringArrayList salRefs = .Items(i).MatchingPossibilities;
                        Debug.WriteLine("Number of matching references: " + salRefs.Count);
                        if (salRefs.Count <> 1)
                        {
                            if (Not bResolved)
                            {
                                .Items(i).MatchingPossibilities = PossibleKeys(salRefs, sInput, .ReferenceType);
                            }
                            If .Items(i).MatchingPossibilities.Count <> 1 Then Return null
                            If .ReferenceType = ReferencesType.Object Then sIt = Adventure.htblObjects(.Items(i).MatchingPossibilities(0)).FullName(ArticleTypeEnum.Definite)
                            If .ReferenceType = ReferencesType.Object && Adventure.htblObjects(.Items(i).MatchingPossibilities(0)).IsPlural Then sThem = Adventure.htblObjects(.Items(i).MatchingPossibilities(0)).FullName(ArticleTypeEnum.Definite)
                            if (.ReferenceType = ReferencesType.Character)
                            {
                                With Adventure.htblCharacters(.Items(i).MatchingPossibilities(0));
                                    switch (.Gender)
                                    {
                                        case clsCharacter.GenderEnum.Male:
                                            {
                                            sHim = .Name
                                        case clsCharacter.GenderEnum.Female:
                                            {
                                            sHer = .Name
                                        case clsCharacter.GenderEnum.Unknown:
                                            {
                                            sIt = .Name
                                    }
                                }
                            }
                            bResolved = True
                        }
                    Next;
                }
            }
        Next;

    }


    private bool ContainsWord(string sSentence, string sCheckWord)
    {

        Dim sChecks() As String = Split(sCheckWord, " ")
        Dim sWords() As String = Split(sSentence.ToLower, " ")

        For Each sCheck As String In sChecks
            private bool bFound = false;
            For Each sWord As String In sWords
                if (sWord = sCheck)
                {
                    bFound = True
                    Exit For;
                }
            Next;
            If ! bFound Then Return false
        Next;

        return true;

        'For Each sWord As String In sWords
        '    If sWord = sCheckWord Then Return True
        'Next

        'Return False

    }

    ' This takes a list of possible refs, then narrows the field (hopefully to 1) given the input
    private StringArrayList PossibleKeys(StringArrayList salKeys, string sInput, ReferencesType sRefType)
    {

        private New StringArrayList salReturn;
        ' Check each word in the refined text to ensure they all match

        switch (sRefType)
        {
            case ReferencesType.Object:
                {
                For Each sKey As String In salKeys
                    private clsObject ob = Adventure.htblObjects(sKey);
                    private bool bMatchAll = true;
                    For Each sWord As String In sInput.Split(" "c)
                        private bool bWordInObject = false;
                        if (sWord = "the" || sWord = ob.Article || ContainsWord(ob.Prefix, sWord))
                        {
                            bWordInObject = True
                        Else
                            For Each sName As String In ob.arlNames
                                if (ContainsWord(sName, sWord))
                                {
                                    bWordInObject = True
                                    Exit For;
                                }
                            Next;
                        }
                        if (Not bWordInObject)
                        {
                            bMatchAll = False
                            Exit For;
                        }
                    Next;
                    If bMatchAll Then salReturn.Add(sKey)
                Next;

            case ReferencesType.Character:
                {
                For Each sKey As String In salKeys
                    private clsCharacter ch = Adventure.htblCharacters(sKey);
                    private bool bMatchAll = true;
                    For Each sWord As String In sInput.Split(" "c)
                        private bool bWordInChar = false;
                        if (ContainsWord(ch.Prefix, sWord) || ContainsWord(ch.ProperName, sWord))
                        {
                            bWordInChar = True
                        Else
                            For Each sName As String In ch.arlDescriptors
                                if (ContainsWord(sName, sWord))
                                {
                                    bWordInChar = True
                                    Exit For;
                                }
                            Next;
                        }
                        if (Not bWordInChar)
                        {
                            bMatchAll = False
                            Exit For;
                        }
                    Next;
                    If bMatchAll Then salReturn.Add(sKey)
                Next;

            case ReferencesType.Location:
                {
                For Each sKey As String In salKeys
                    private clsLocation loc = Adventure.htblLocations(sKey);
                    private bool bMatchAll = true;
                    For Each sWord As String In sInput.Split(" "c)
                        private bool bWordInLoc = false;
                        if (ContainsWord(loc.ShortDescription.ToString(true), sWord))
                        {
                            bWordInLoc = True
                        }
                        if (Not bWordInLoc)
                        {
                            bMatchAll = False
                            Exit For;
                        }
                    Next;
                    If bMatchAll Then salReturn.Add(sKey)
                Next;

        }

        return salReturn;

    }


    private string GetReference(string sReference)
    {

        If NewReferences == null Then Return null

        for (int iRef = 0; iRef <= NewReferences.Length - 1; iRef++)
        {
            With NewReferences(iRef);
                if (NewReferences(iRef) IsNot null && .Items IsNot null Then '&& .Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                {

                    switch (sReference)
                    {
                        case "ReferencedObject":
                        case "ReferencedObject1":
                        case "ReferencedObject2":
                        case "ReferencedObject3":
                        case "ReferencedObject4":
                        case "ReferencedObject5":
                        case "ReferencedObjects":
                            {
                            if (.ReferenceType = ReferencesType.Object)
                            {
                                If sReference = "ReferencedObjects" _
                                    || (.ReferenceMatch = "object1" && (sReference = "ReferencedObject" || sReference = "ReferencedObject1")) _;
                                    || (.ReferenceMatch = "object2" && sReference = "ReferencedObject2") _;
                                    || (.ReferenceMatch = "object3" && sReference = "ReferencedObject3") _;
                                    || (.ReferenceMatch = "object4" && sReference = "ReferencedObject4") _;
                                    || (.ReferenceMatch = "object5" && sReference = "ReferencedObject5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                        case "ReferencedDirection":
                        case "ReferencedDirection1":
                        case "ReferencedDirection2":
                        case "ReferencedDirection3":
                        case "ReferencedDirection4":
                        case "ReferencedDirection5":
                            {
                            if (.ReferenceType = ReferencesType.Direction)
                            {
                                If (.ReferenceMatch = "direction1" && (sReference = "ReferencedDirection" || sReference = "ReferencedDirection1")) _
                                    || (.ReferenceMatch = "direction2" && sReference = "ReferencedDirection2") _;
                                    || (.ReferenceMatch = "direction3" && sReference = "ReferencedDirection3") _;
                                    || (.ReferenceMatch = "direction4" && sReference = "ReferencedDirection4") _;
                                    || (.ReferenceMatch = "direction5" && sReference = "ReferencedDirection5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                        case "ReferencedCharacter":
                        case "ReferencedCharacter1":
                        case "ReferencedCharacter2":
                        case "ReferencedCharacter3":
                        case "ReferencedCharacter4":
                        case "ReferencedCharacter5":
                            {
                            if (.ReferenceType = ReferencesType.Character)
                            {
                                If sReference = "ReferencedCharacters" _
                                    || (.ReferenceMatch = "character1" && (sReference = "ReferencedCharacter" || sReference = "ReferencedCharacter1")) _;
                                    || (.ReferenceMatch = "character2" && sReference = "ReferencedCharacter2") _;
                                    || (.ReferenceMatch = "character3" && sReference = "ReferencedCharacter3") _;
                                    || (.ReferenceMatch = "character4" && sReference = "ReferencedCharacter4") _;
                                    || (.ReferenceMatch = "character5" && sReference = "ReferencedCharacter5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                        case "ReferencedLocation":
                        case "ReferencedLocation1":
                        case "ReferencedLocation2":
                        case "ReferencedLocation3":
                        case "ReferencedLocation4":
                        case "ReferencedLocation5":
                            {
                            if (.ReferenceType = ReferencesType.Location)
                            {
                                If (.ReferenceMatch = "location1" && (sReference = "ReferencedLocation" || sReference = "ReferencedLocation1")) _
                                    || (.ReferenceMatch = "location2" && sReference = "ReferencedLocation2") _;
                                    || (.ReferenceMatch = "location3" && sReference = "ReferencedLocation3") _;
                                    || (.ReferenceMatch = "location4" && sReference = "ReferencedLocation4") _;
                                    || (.ReferenceMatch = "location5" && sReference = "ReferencedLocation5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                        case "ReferencedItem":
                        case "ReferencedItem1":
                        case "ReferencedItem2":
                        case "ReferencedItem3":
                        case "ReferencedItem4":
                        case "ReferencedItem5":
                            {
                            if (.ReferenceType = ReferencesType.Item)
                            {
                                If (.ReferenceMatch = "item1" && (sReference = "ReferencedItem" || sReference = "ReferencedItem1")) _
                                    || (.ReferenceMatch = "item2" && sReference = "ReferencedItem2") _;
                                    || (.ReferenceMatch = "item3" && sReference = "ReferencedItem3") _;
                                    || (.ReferenceMatch = "item4" && sReference = "ReferencedItem4") _;
                                    || (.ReferenceMatch = "item5" && sReference = "ReferencedItem5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                        case "ReferencedNumber":
                        case "ReferencedNumber1":
                        case "ReferencedNumber2":
                        case "ReferencedNumber3":
                        case "ReferencedNumber4":
                        case "ReferencedNumber5":
                            {
                            if (.ReferenceType = ReferencesType.Number)
                            {
                                If (.ReferenceMatch = "number1" && (sReference = "ReferencedNumber" || sReference = "ReferencedNumber1")) _
                                    || (.ReferenceMatch = "number2" && sReference = "ReferencedNumber2") _;
                                    || (.ReferenceMatch = "number3" && sReference = "ReferencedNumber3") _;
                                    || (.ReferenceMatch = "number4" && sReference = "ReferencedNumber4") _;
                                    || (.ReferenceMatch = "number5" && sReference = "ReferencedNumber5") Then;
                                    if (.Items.Count > 0 && .Items(0).MatchingPossibilities.Count > 0)
                                    {
                                        return .Items(0).MatchingPossibilities(0);
                                    Else
                                        return null;
                                    }
                                }
                            }
                    }

                }
            }
        Next;

        return null;

    }


    'Private Function GetReferenceOld(ByVal sReference As String) As String

    '    Dim iRefCount As Integer = 0

    '    If References Is Nothing Then Return Nothing

    '    For iRef As Integer = 0 To References.Length - 1
    '        With References(iRef)
    '            If References(iRef) IsNot Nothing AndAlso References(iRef).alReferences IsNot Nothing AndAlso .alReferences.Count > 0 AndAlso .alReferences(0).salWhat.Count > 0 Then

    '                Select Case sReference
    '                    Case "ReferencedObject", "ReferencedObject1", "ReferencedObject2", "ReferencedObject3", "ReferencedObject4", "ReferencedObject5", "ReferencedObjects"
    '                        If .sRefType = ReferencesType.Object Then
    '                            iRefCount += 1
    '                            If sReference = "ReferencedObjects" _
    '                                OrElse (iRefCount = 1 AndAlso (sReference = "ReferencedObject" OrElse sReference = "ReferencedObject1")) _
    '                                OrElse (iRefCount = 2 AndAlso sReference = "ReferencedObject2") _
    '                                OrElse (iRefCount = 3 AndAlso sReference = "ReferencedObject3") _
    '                                OrElse (iRefCount = 4 AndAlso sReference = "ReferencedObject4") _
    '                                OrElse (iRefCount = 5 AndAlso sReference = "ReferencedObject5") Then
    '                                Return .alReferences(0).salWhat(0)
    '                            End If
    '                        End If
    '                    Case "ReferencedDirection"
    '                        If .sRefType = ReferencesType.Direction Then Return .alReferences(0).salWhat(0)
    '                End Select

    '            End If
    '        End With
    '    Next

    '    Return Nothing

    'End Function


    private bool PassSingleRestriction(clsRestriction restx, bool bIgnoreReferences = false)
    {

        try
        {
            private New clsRestriction rest;
            rest = restx.Copy
            private bool r = false;

            'If rest.sKey1 = "ReferencedObjects" OrElse rest.sKey2 = "ReferencedObjects" Then
            '    Exit Function ' for now
            'End If
            'Debug.WriteLine(rest.Summary)

            ' Replace references
            switch (rest.sKey1)
            {
                case "ReferencedObject":
                case "ReferencedObject1":
                case "ReferencedObject2":
                case "ReferencedObject3":
                case "ReferencedObject4":
                case "ReferencedObject5":
                case "ReferencedObjects":
                case "ReferencedDirection":
                case "ReferencedCharacter":
                case "ReferencedCharacter1":
                case "ReferencedCharacter2":
                case "ReferencedCharacter3":
                case "ReferencedCharacter4":
                case "ReferencedCharacter5":
                case "ReferencedLocation":
                case "ReferencedItem":
                    {
                    rest.sKey1 = GetReference(rest.sKey1);
                    If bIgnoreReferences && rest.sKey1 == null Then Return true
            }
            switch (rest.sKey2)
            {
                case "ReferencedObject":
                case "ReferencedObject1":
                case "ReferencedObject2":
                case "ReferencedObject3":
                case "ReferencedObject4":
                case "ReferencedObject5":
                case "ReferencedObjects":
                case "ReferencedDirection":
                case "ReferencedCharacter":
                case "ReferencedCharacter1":
                case "ReferencedCharacter2":
                case "ReferencedCharacter3":
                case "ReferencedCharacter4":
                case "ReferencedCharacter5":
                case "ReferencedLocation":
                case "ReferencedItem":
                    {
                    rest.sKey2 = GetReference(rest.sKey2);
                    If bIgnoreReferences && rest.sKey2 == null Then Return true
            }

            If rest.sKey1 = "%Player%" && Adventure.Player != null Then rest.sKey1 = Adventure.Player.Key
            If rest.sKey2 = "%Player%" && Adventure.Player != null Then rest.sKey2 = Adventure.Player.Key

            if (Not rest.eType = clsRestriction.RestrictionTypeEnum.Expression && (rest.sKey1 Is null || (rest.sKey2 Is null && rest.eType <> clsRestriction.RestrictionTypeEnum.Task && rest.eType <> clsRestriction.RestrictionTypeEnum.Variable && rest.eType <> clsRestriction.RestrictionTypeEnum.Direction && Not (rest.eType = clsRestriction.RestrictionTypeEnum.Object && rest.eObject = clsRestriction.ObjectEnum.BeHidden))))
            {
                r = False
                GoTo SkipTest;
            }

            switch (rest.eType)
            {
                case clsRestriction.RestrictionTypeEnum.Location:
                    {
                    private clsLocation loc = null;
                    switch (rest.sKey1)
                    {
                        case PLAYERLOCATION:
                            {
                            loc = Adventure.htblLocations(Adventure.Player.Location.LocationKey)
                        default:
                            {
                            If Adventure.htblLocations.ContainsKey(rest.sKey1) Then loc = Adventure.htblLocations(rest.sKey1)
                    }
                    if (loc IsNot null)
                    {
                        switch (rest.eLocation)
                        {
                            case clsRestriction.LocationEnum.HaveBeenSeenByCharacter:
                                {
                                r = loc.SeenBy(rest.sKey2)
                            case clsRestriction.LocationEnum.BeInGroup:
                                {
                                switch (rest.sKey1)
                                {
                                    case PLAYERLOCATION:
                                        {
                                        r = Adventure.htblGroups(rest.sKey2).arlMembers.Contains(Adventure.Player.Location.LocationKey)
                                    default:
                                        {
                                        r = Adventure.htblGroups(rest.sKey2).arlMembers.Contains(rest.sKey1)
                                }
                            case clsRestriction.LocationEnum.HaveProperty:
                                {
                                r = loc.HasProperty(rest.sKey2)
                            case clsRestriction.LocationEnum.BeLocation:
                                {
                                switch (rest.sKey1)
                                {
                                    case PLAYERLOCATION:
                                        {
                                        r = (Adventure.Player.Location.LocationKey = rest.sKey2)
                                    default:
                                        {
                                        r = (rest.sKey1 = rest.sKey2)
                                }
                            case clsRestriction.LocationEnum.Exist:
                                {
                                r = True
                        }
                    }

                case clsRestriction.RestrictionTypeEnum.Object:
                    {
                    switch (rest.eObject)
                    {
                        case clsRestriction.ObjectEnum.BeAtLocation:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    r = Adventure.htblLocations(rest.sKey2).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, False).Count = 0
                                case ANYOBJECT:
                                    {
                                    r = Adventure.htblLocations(rest.sKey2).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, False).Count > 0
                                default:
                                    {
                                    r = Adventure.htblObjects(rest.sKey1).ExistsAtLocation(rest.sKey2)
                            }
                        case clsRestriction.ObjectEnum.BeHeldByCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("No object held by any character test");
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).HeldObjects.Count = 0
                                    }
                                case ANYOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Any object held by any character test");
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).HeldObjects.Count > 0
                                    }
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            'r = Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter
                                            r = Adventure.htblObjects(rest.sKey1).IsHeldByAnyone
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).IsHoldingObject(rest.sKey1)
                                    }
                            }
                        case clsRestriction.ObjectEnum.BeHidden:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No object hidden");
                                    'r = Adventure.htblLocations(rest.sKey2).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, False).Count = 0
                                case ANYOBJECT:
                                    {
                                    TODO("Any object hidden");
                                    'r = Adventure.htblLocations(rest.sKey2).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, False).Count > 0
                                default:
                                    {
                                    With Adventure.htblObjects(rest.sKey1);
                                        if (.IsStatic)
                                        {
                                            r = .Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms
                                        Else
                                            r = .Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden
                                        }
                                    }
                            }
                        case clsRestriction.ObjectEnum.BeInGroup:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    r = Adventure.htblGroups(rest.sKey2).arlMembers.Count = 0
                                case ANYOBJECT:
                                    {
                                    r = Adventure.htblGroups(rest.sKey2).arlMembers.Count > 0
                                default:
                                    {
                                    r = Adventure.htblGroups(rest.sKey2).arlMembers.Contains(rest.sKey1)
                            }
                        case clsRestriction.ObjectEnum.BeInsideObject:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("No object in no object test");
                                        case ANYOBJECT:
                                            {
                                            TODO("No object in any object test");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.InsideObject).Count = 0
                                    }
                                case ANYOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("Any object in no object test");
                                        case ANYOBJECT:
                                            {
                                            TODO("Any object in any object test");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                                    }
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("Object inside no object test");
                                        case ANYOBJECT:
                                            {
                                            r = (Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject)
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey1).IsInside(rest.sKey2)
                                    }
                            }
                            'If rest.sKey2 = ANYOBJECT Then
                            '    r = (Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject)
                            'Else
                            '    If rest.sKey1 = ANYOBJECT Then
                            '        r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                            '    ElseIf rest.sKey1 = ANYCHARACTER Then
                            '        r = Adventure.htblObjects(rest.sKey2).ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                            '    Else
                            '        r = Adventure.htblObjects(rest.sKey1).IsInside(rest.sKey2)
                            '    End If
                            'End If
                        case clsRestriction.ObjectEnum.BeInState:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    For Each ob As clsObject In Adventure.htblObjects.Values
                                        For Each prop As clsProperty In ob.htblProperties.Values
                                            if (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.Value = rest.sKey2 && prop.AppendToProperty = "")
                                            {
                                                r = False
                                                GoTo SkipTest;
                                            }
                                        Next;
                                    Next;
                                    r = True
                                case ANYOBJECT:
                                    {
                                    For Each ob As clsObject In Adventure.htblObjects.Values
                                        For Each prop As clsProperty In ob.htblProperties.Values
                                            if (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.Value = rest.sKey2 && prop.AppendToProperty = "")
                                            {
                                                r = True
                                                GoTo SkipTest;
                                            }
                                        Next;
                                    Next;
                                default:
                                    {
                                    ' Ok, this is a fudge option
                                    For Each prop As clsProperty In Adventure.htblObjects(rest.sKey1).htblProperties.Values
                                        if (prop.Type = clsProperty.PropertyTypeEnum.StateList && prop.Value = rest.sKey2 && prop.AppendToProperty = "")
                                        {
                                            r = True
                                            GoTo SkipTest;
                                        }
                                    Next;
                            }
                        case clsRestriction.ObjectEnum.BeOnObject:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("No Object must be on No Object test");
                                        case ANYOBJECT:
                                            {
                                            TODO("No Object must be on Any Object test");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.OnObject).Count = 0
                                    }
                                case ANYOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("Any Object must be on No Object test");
                                        case ANYOBJECT:
                                            {
                                            TODO("Any Object must be on Any Object test");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.OnObject).Count > 0
                                    }
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("Object must be on No Object test");
                                        case ANYOBJECT:
                                            {
                                            r = (Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject)
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey1).IsOn(rest.sKey2)
                                    }
                            }
                            'If rest.sKey2 = ANYOBJECT Then
                            '    r = (Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject)
                            'Else
                            '    If rest.sKey1 = ANYOBJECT Then
                            '        r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.OnObject).Count > 0
                            '    ElseIf rest.sKey1 = ANYCHARACTER Then
                            '        r = Adventure.htblObjects(rest.sKey2).ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).Count > 0
                            '    Else
                            '        r = Adventure.htblObjects(rest.sKey1).IsOn(rest.sKey2)
                            '    End If
                            'End If
                            'r = Adventure.htblObjects(rest.sKey1).Parent = rest.sKey2
                        case clsRestriction.ObjectEnum.BePartOfCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No Object is part of character test");
                                case ANYOBJECT:
                                    {
                                    TODO("Any Object is part of character test");
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Object is part of any character test");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter _
                                            && Adventure.htblObjects(rest.sKey1).Location.Key = rest.sKey2;
                                    }
                            }
                            'If rest.sKey2 = ANYCHARACTER Then
                            '    TODO("Object is part of any character")
                            'Else
                            '    r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter _
                            '    AndAlso Adventure.htblObjects(rest.sKey1).Location.Key = rest.sKey2
                            'End If
                        case clsRestriction.ObjectEnum.BePartOfObject:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No Object is part of object test");
                                case ANYOBJECT:
                                    {
                                    TODO("Any Object is part of object test");
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case NOOBJECT:
                                            {
                                            TODO("Object is part of No object test");
                                        case ANYOBJECT:
                                            {
                                            r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject _
                                             && Adventure.htblObjects(rest.sKey1).Location.Key = rest.sKey2;
                                    }
                            }
                            'If rest.sKey2 = ANYOBJECT Then
                            '    r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject
                            'Else
                            '    r = Adventure.htblObjects(rest.sKey1).Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfObject _
                            '    AndAlso Adventure.htblObjects(rest.sKey1).Location.Key = rest.sKey2
                            'End If
                        case clsRestriction.ObjectEnum.HaveBeenSeenByCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No Object has been seen by character test");
                                case ANYOBJECT:
                                    {
                                    TODO("Any Object has been seen by character test");
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Object has been seen by any character test");
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).HasSeenObject(rest.sKey1)
                                    }
                            }
                            'If rest.sKey2 = ANYCHARACTER Then
                            '    TODO("Object has been seen by any character")
                            'Else
                            '    r = Adventure.htblCharacters(rest.sKey2).HasSeenObject(rest.sKey1)
                            'End If
                            'Case clsRestriction.ObjectEnum.StaticOrDynamic
                        case clsRestriction.ObjectEnum.BeVisibleToCharacter:
                            {
                            private clsCharacter ch = Adventure.htblCharacters(rest.sKey2);
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("No Object visible to any character test");
                                        default:
                                            {
                                            r = True
                                            For Each ob As clsObject In Adventure.htblObjects.Values
                                                if (ch.CanSeeObject(ob.Key))
                                                {
                                                    r = False
                                                    Exit For;
                                                }
                                            Next;
                                    }
                                case ANYOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Any Object visible to any character test");
                                        default:
                                            {
                                            r = False
                                            For Each ob As clsObject In Adventure.htblObjects.Values
                                                if (ch.CanSeeObject(ob.Key))
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Next;
                                    }
                                default:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Object visible to any character test");
                                        default:
                                            {
                                            r = ch.CanSeeObject(rest.sKey1)
                                    }
                            }
                            'If rest.sKey2 = ANYCHARACTER Then
                            '    TODO("Object visible to any character")
                            'Else
                            '    r = Adventure.htblCharacters(rest.sKey2).CanSeeObject(rest.sKey1)
                            'End If
                        case clsRestriction.ObjectEnum.BeWornByCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("No Object worn by any character test");
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).WornObjects.Count = 0
                                    }
                                case ANYOBJECT:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYCHARACTER:
                                            {
                                            TODO("Any Object worn by any character test");
                                        default:
                                            {
                                            r = Adventure.htblCharacters(rest.sKey2).WornObjects.Count > 0
                                    }
                                default:
                                    {
                                    if (Adventure.htblObjects.ContainsKey(rest.sKey1))
                                    {
                                        switch (rest.sKey2)
                                        {
                                            case ANYCHARACTER:
                                                {
                                                r = Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter
                                            default:
                                                {
                                                r = Adventure.htblCharacters(rest.sKey2).IsWearingObject(rest.sKey1)
                                        }
                                    }
                            }

                            'If rest.sKey2 = ANYCHARACTER Then
                            '    r = Adventure.htblObjects(rest.sKey1).Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter
                            'Else
                            '    r = Adventure.htblCharacters(rest.sKey2).IsWearingObject(rest.sKey1)
                            'End If
                        case clsRestriction.ObjectEnum.Exist:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    r = Adventure.htblObjects.Count = 0
                                case ANYOBJECT:
                                    {
                                    r = Adventure.htblObjects.Count > 0
                                default:
                                    {
                                    r = Adventure.htblObjects.ContainsKey(rest.sKey1)
                            }
                        case clsRestriction.ObjectEnum.HaveProperty:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    For Each ob As clsObject In Adventure.htblObjects.Values
                                        if (ob.HasProperty(rest.sKey2))
                                        {
                                            r = False
                                            GoTo SkipTest;
                                        }
                                    Next;
                                    r = True
                                case ANYOBJECT:
                                    {
                                    For Each ob As clsObject In Adventure.htblObjects.Values
                                        if (ob.HasProperty(rest.sKey2))
                                        {
                                            r = True
                                            GoTo SkipTest;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblObjects.ContainsKey(rest.sKey1) AndAlso Adventure.htblObjects(rest.sKey1).HasProperty(rest.sKey2) ' .GetPropertiesIncludingGroups.ContainsKey(rest.sKey2)
                            }
                        case clsRestriction.ObjectEnum.BeExactText:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No Object be exact text test");
                                case ANYOBJECT:
                                    {
                                    TODO("Any Object be exact text test");
                                default:
                                    {
                                    r = restx.sKey1 = "ReferencedObjects" AndAlso NewReferences IsNot Nothing AndAlso (NewReferences(0).Items(0).sCommandReference = "all" OrElse (NewReferences.Length > 1 AndAlso NewReferences(1).Items(0).sCommandReference = "all")) ' Should probably verify that %objects% was actually reference 0
                            }
                        case clsRestriction.ObjectEnum.BeObject:
                            {
                            switch (rest.sKey1)
                            {
                                case NOOBJECT:
                                    {
                                    TODO("No Object be specific object test");
                                case ANYOBJECT:
                                    {
                                    TODO("Any Object be specific object test");
                                default:
                                    {
                                    r = (rest.sKey1 = rest.sKey2)
                            }
                    }

                case clsRestriction.RestrictionTypeEnum.Task:
                    {
                    switch (rest.eTask)
                    {
                        case clsRestriction.TaskEnum.Complete:
                            {
                            If Adventure.htblTasks.ContainsKey(rest.sKey1) Then r = Adventure.htblTasks(rest.sKey1).Completed
                    }

                case clsRestriction.RestrictionTypeEnum.Variable:
                    {
                    private clsVariable var;
                    private int iIndex = 1;
                    if (rest.sKey1.StartsWith("ReferencedNumber"))
                    {
                        private int iRef = Math.Max(SafeInt(rest.sKey1.Replace("ReferencedNumber", "")) - 1, 0);
                        var = New clsVariable
                        var.Type = clsVariable.VariableTypeEnum.Numeric;
                        'var.IntValue = Adventure.iReferencedNumber(iRef)
                        var.IntValue = SafeInt(GetReference(rest.sKey1));
                    ElseIf rest.sKey1.StartsWith("ReferencedText") Then
                        private int iRef = Math.Max(SafeInt(rest.sKey1.Replace("ReferencedText", "")) - 1, 0);
                        var = New clsVariable
                        var.Type = clsVariable.VariableTypeEnum.Text;
                        var.StringValue = Adventure.sReferencedText(iRef);
                    Else
                        var = Adventure.htblVariables(rest.sKey1)
                        if (rest.sKey2 <> "")
                        {
                            if (Adventure.htblVariables.ContainsKey(rest.sKey2))
                            {
                                iIndex = Adventure.htblVariables(rest.sKey2).IntValue
                            Else
                                iIndex = SafeInt(EvaluateExpression(rest.sKey2))
                            }
                        }
                    }

                    private int iIntVal;
                    private string sStringVal = "";
                    if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                    {
                        if (rest.IntValue = Integer.MinValue && rest.StringValue <> "")
                        {
                            iIntVal = Adventure.htblVariables(rest.StringValue).IntValue ' Variable value
                        Else
                            if (rest.StringValue <> "" && rest.StringValue <> rest.IntValue.ToString)
                            {
                                iIntVal = SafeInt(EvaluateExpression(rest.StringValue)) ' SafeInt(ReplaceFunctions(rest.StringValue)) ' Expression
                            Else
                                iIntVal = rest.IntValue ' Integer value
                            }
                        }
                    Else
                        if (rest.IntValue = Integer.MinValue && rest.StringValue <> "")
                        {
                            sStringVal = Adventure.htblVariables(rest.StringValue).StringValue
                        Else
                            ''sStringVal = rest.StringValue
                            'Dim varExp As New clsVariable
                            'varExp.Type = clsVariable.VariableTypeEnum.Text
                            'varExp.SetToExpression(rest.StringValue)
                            'sStringVal = varExp.StringValue
                            sStringVal = EvaluateExpression(rest.StringValue)
                        }
                    }

                    switch (rest.eVariable)
                    {
                        case clsRestriction.VariableEnum.EqualTo:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                r = (var.IntValue(iIndex) = iIntVal)
                            Else
                                r = (var.StringValue(iIndex) = sStringVal)
                            }
                        case clsRestriction.VariableEnum.GreaterThan:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                r = (var.IntValue(iIndex) > iIntVal)
                            Else
                                r = (var.StringValue(iIndex) > sStringVal)
                            }
                        case clsRestriction.VariableEnum.GreaterThanOrEqualTo:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                r = (var.IntValue(iIndex) >= iIntVal)
                            Else
                                r = (var.StringValue(iIndex) >= sStringVal)
                            }
                        case clsRestriction.VariableEnum.LessThan:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                r = (var.IntValue(iIndex) < iIntVal)
                            Else
                                r = (var.StringValue(iIndex) < sStringVal)
                            }
                        case clsRestriction.VariableEnum.LessThanOrEqualTo:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                r = (var.IntValue(iIndex) <= iIntVal)
                            Else
                                r = (var.StringValue(iIndex) <= sStringVal)
                            }
                        case clsRestriction.VariableEnum.Contain:
                            {
                            if (var.Type = clsVariable.VariableTypeEnum.Text)
                            {
                                r = var.StringValue(iIndex) IsNot Nothing AndAlso var.StringValue(iIndex).Contains(sStringVal)
                            }
                    }

                case clsRestriction.RestrictionTypeEnum.Character:
                    {
                    'Select Case rest.sKey1
                    '    Case ANYCHARACTER
                    '        For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    '            Dim restSub As clsRestriction = rest.Copy
                    '            restSub.sKey1 = ch.Key
                    '            If PassSingleRestriction(restSub) Then Return True
                    '        Next
                    '        Return False

                    '    Case Else
                    'Dim ch As clsCharacter = Adventure.htblCharacters(rest.sKey1)
                    switch (rest.eCharacter)
                    {
                        case clsRestriction.CharacterEnum.BeAlone:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                                        if (ch.IsAlone)
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                    r = False
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).IsAlone
                            }

                        case clsRestriction.CharacterEnum.BeAloneWith:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character be alone");
                                default:
                                    {
                                    if (rest.sKey2 = ANYCHARACTER)
                                    {
                                        r = Adventure.htblCharacters(rest.sKey1).AloneWithChar IsNot Nothing
                                    Else
                                        r = Adventure.htblCharacters(rest.sKey1).AloneWithChar = rest.sKey2
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeAtLocation:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = False
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.Location.LocationKey = rest.sKey2)
                                        {
                                            'If c.LocationRoots.ContainsKey(rest.sKey2) Then
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).Location.LocationKey = rest.sKey2
                            }

                        case clsRestriction.CharacterEnum.BeCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = True ' Bit pointless
                                default:
                                    {
                                    r = (rest.sKey1 = rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeInConversationWith:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character be in conversation with");
                                default:
                                    {
                                    if (rest.sKey2 = ANYCHARACTER)
                                    {
                                        r = (Adventure.sConversationCharKey <> "")
                                    Else
                                        r = (Adventure.sConversationCharKey = rest.sKey2)
                                    }
                            }

                        case clsRestriction.CharacterEnum.Exist:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = Adventure.htblCharacters.Count > 0
                                default:
                                    {
                                    r = Adventure.htblCharacters.ContainsKey(rest.sKey1)
                            }

                        case clsRestriction.CharacterEnum.HaveRouteInDirection:
                            {
                            sRouteError = rest.oMessage.ToString
                            private string sSpecificError = "";
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = False
                                    switch (rest.sKey2)
                                    {
                                        case ANYDIRECTION:
                                            {
                                            For Each c As clsCharacter In Adventure.htblCharacters.Values
                                                For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                                                    if (c.HasRouteInDirection(d, false, , sSpecificError))
                                                    {
                                                        r = True
                                                        GoTo DoneAnyCharAnyDir;
                                                    }
                                                Next;
                                            Next;
DoneAnyCharAnyDir:
                                        default:
                                            {
                                            For Each c As clsCharacter In Adventure.htblCharacters.Values
                                                if (c.HasRouteInDirection(CType([Enum].Parse(GetType(DirectionsEnum), rest.sKey2), DirectionsEnum), false, , sSpecificError))
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Next;
                                    }
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    switch (rest.sKey2)
                                    {
                                        case ANYDIRECTION:
                                            {
                                            r = False
                                            For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                                                if (ch.HasRouteInDirection(d, false, , sSpecificError))
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Next;
                                        default:
                                            {
                                            r = ch.HasRouteInDirection(CType([Enum].Parse(GetType(DirectionsEnum), rest.sKey2), DirectionsEnum), False, , sSpecificError)
                                    }
                            }
                            If sSpecificError <> "" Then sRouteError = sSpecificError

                        case clsRestriction.CharacterEnum.HaveSeenCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character seen character");
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).HasSeenCharacter(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.HaveSeenLocation:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character seen location");
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).HasSeenLocation(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.HaveSeenObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character seen object");
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).HasSeenObject(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeHoldingObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = Adventure.htblObjects(rest.sKey2).IsHeldByAnyone
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).IsHoldingObject(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeInSameLocationAsCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = Not Adventure.htblCharacters(rest.sKey2).IsAlone
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    if (rest.sKey2 = ANYCHARACTER)
                                    {
                                        r = False
                                        For Each c As clsCharacter In Adventure.htblCharacters.Values
                                            if (c.Key <> rest.sKey1 && c.Location.LocationKey = ch.Location.LocationKey)
                                            {
                                                r = True
                                                Exit For;
                                            }
                                        Next;
                                    Else
                                        r = ch.Location.LocationKey = Adventure.htblCharacters(rest.sKey2).Location.LocationKey
                                    }
                                    'r = Adventure.htblCharacters(rest.sKey1).LocationRoot.Key = Adventure.htblCharacters(rest.sKey2).LocationRoot.Key
                                    'For Each loc As clsLocation In Adventure.htblCharacters(rest.sKey1).LocationRoots.Values
                                    '    r = Adventure.htblCharacters(rest.sKey2).LocationRoots.ContainsKey(loc.Key)
                                    '    Exit For
                                    'Next
                            }

                        case clsRestriction.CharacterEnum.BeVisibleToCharacter:
                            {
                            private New List<clsCharacter> lChars;

                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        lChars.Add(c);
                                    Next;
                                default:
                                    {
                                    lChars.Add(Adventure.htblCharacters(rest.sKey1));
                            }

                            r = False
                            For Each c As clsCharacter In lChars
                                if (rest.sKey2 = ANYCHARACTER)
                                {
                                    For Each c2 As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.Key <> c2.Key)
                                        {
                                            if (c.CanSeeCharacter(c2.Key))
                                            {
                                                r = True
                                                Exit For;
                                            }
                                        }
                                    Next;
                                Else
                                    if (c.CanSeeCharacter(rest.sKey2))
                                    {
                                        r = True
                                        Exit For;
                                    }
                                }
                            Next;

                        case clsRestriction.CharacterEnum.BeInSameLocationAsObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.CanSeeObject(rest.sKey2))
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).CanSeeObject(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeLyingOnObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.Location.Position = clsCharacterLocation.PositionEnum.Lying)
                                        {
                                            if (rest.sKey2 = ANYOBJECT)
                                            {
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            ElseIf rest.sKey2 = THEFLOOR Then
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Else
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject && c.Location.Key = rest.sKey2)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            }
                                        }
                                    Next;
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Lying AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject
                                    ElseIf rest.sKey2 = THEFLOOR Then
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Lying AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation
                                    Else
                                        r = (ch.Location.Position = clsCharacterLocation.PositionEnum.Lying AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject AndAlso ch.Location.Key = rest.sKey2)
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeInGroup:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (Adventure.htblGroups(rest.sKey2).arlMembers.Contains(c.Key))
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblGroups(rest.sKey2).arlMembers.Contains(rest.sKey1)
                            }

                        case clsRestriction.CharacterEnum.BeOfGender:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character be of gender");
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).Gender = CType([Enum].Parse(GetType(clsCharacter.GenderEnum), rest.sKey2), clsCharacter.GenderEnum)
                            }

                        case clsRestriction.CharacterEnum.BeSittingOnObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.Location.Position = clsCharacterLocation.PositionEnum.Sitting)
                                        {
                                            if (rest.sKey2 = ANYOBJECT)
                                            {
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            ElseIf rest.sKey2 = THEFLOOR Then
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Else
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject && c.Location.Key = rest.sKey2)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            }
                                        }
                                    Next;
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Sitting AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject
                                    ElseIf rest.sKey2 = THEFLOOR Then
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Sitting AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation
                                    Else
                                        r = (ch.Location.Position = clsCharacterLocation.PositionEnum.Sitting AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject AndAlso ch.Location.Key = rest.sKey2)
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeStandingOnObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.Location.Position = clsCharacterLocation.PositionEnum.Standing)
                                        {
                                            if (rest.sKey2 = ANYOBJECT)
                                            {
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            ElseIf rest.sKey2 = THEFLOOR Then
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            Else
                                                if (c.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject && c.Location.Key = rest.sKey2)
                                                {
                                                    r = True
                                                    Exit For;
                                                }
                                            }
                                        }
                                    Next;
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Standing AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject
                                    ElseIf rest.sKey2 = THEFLOOR Then
                                        r = ch.Location.Position = clsCharacterLocation.PositionEnum.Standing AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation
                                    Else
                                        r = (ch.Location.Position = clsCharacterLocation.PositionEnum.Standing AndAlso ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject AndAlso ch.Location.Key = rest.sKey2)
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeWearingObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    r = Adventure.htblObjects(rest.sKey2).IsWornByAnyone
                                default:
                                    {
                                    r = Adventure.htblCharacters(rest.sKey1).IsWearingObject(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeWithinLocationGroup:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (Adventure.htblGroups(rest.sKey2).arlMembers.Contains(c.Location.LocationKey))
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblGroups(rest.sKey2).arlMembers.Contains(Adventure.htblCharacters(rest.sKey1).Location.LocationKey)
                            }

                        case clsRestriction.CharacterEnum.HaveProperty:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.HasProperty(rest.sKey2))
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    r = Adventure.htblCharacters.ContainsKey(rest.sKey1) AndAlso Adventure.htblCharacters(rest.sKey1).HasProperty(rest.sKey2)
                            }

                        case clsRestriction.CharacterEnum.BeInPosition:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    For Each c As clsCharacter In Adventure.htblCharacters.Values
                                        if (c.HasProperty("CharacterPosition") && c.GetPropertyValue("CharacterPosition") = rest.sKey2)
                                        {
                                            r = True
                                            Exit For;
                                        }
                                    Next;
                                default:
                                    {
                                    if (Adventure.htblCharacters(rest.sKey1).HasProperty("CharacterPosition"))
                                    {
                                        r = Adventure.htblCharacters(rest.sKey1).GetPropertyValue("CharacterPosition") = rest.sKey2
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeInsideObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    switch (rest.sKey2)
                                    {
                                        case ANYOBJECT:
                                            {
                                            TODO("Any Character be inside any object");
                                        default:
                                            {
                                            r = Adventure.htblObjects(rest.sKey2).ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                                    }
                                default:
                                    {
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        r = (Adventure.htblCharacters(rest.sKey1).Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject)
                                    Else
                                        if (rest.sKey1 = ANYOBJECT)
                                        {
                                            r = Adventure.htblObjects(rest.sKey2).Children(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                                        ElseIf rest.sKey1 = ANYCHARACTER Then
                                            r = Adventure.htblObjects(rest.sKey2).ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Count > 0
                                        Else
                                            r = Adventure.htblCharacters(rest.sKey1).Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.InObject AndAlso Adventure.htblCharacters(rest.sKey1).Location.Key = rest.sKey2
                                        }
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeOnObject:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        TODO("Any Character on Any Object");
                                    Else
                                        r = Adventure.htblObjects(rest.sKey2).ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).Count > 0
                                    }
                                default:
                                    {
                                    if (rest.sKey2 = ANYOBJECT)
                                    {
                                        r = (Adventure.htblCharacters(rest.sKey1).Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                                    Else
                                        private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                        r = ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject AndAlso ch.Location.Key = rest.sKey2 ' Adventure.htblObjects(rest.sKey1).IsOn(rest.sKey2)
                                    }
                            }

                        case clsRestriction.CharacterEnum.BeOnCharacter:
                            {
                            switch (rest.sKey1)
                            {
                                case ANYCHARACTER:
                                    {
                                    TODO("Any Character be on character");
                                default:
                                    {
                                    private clsCharacter ch = Adventure.htblCharacters(rest.sKey1);
                                    if (rest.sKey2 = ANYCHARACTER)
                                    {
                                        r = (ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter)
                                    Else
                                        r = ch.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnCharacter AndAlso ch.Location.Key = rest.sKey2
                                    }
                            }

                    }

                case clsRestriction.RestrictionTypeEnum.Item:
                    {

                    private clsObject obItem = null;
                    private clsCharacter chItem = null;
                    private clsLocation locItem = null;

                    if (Not Adventure.htblObjects.TryGetValue(rest.sKey1, obItem) && Not Adventure.htblCharacters.TryGetValue(rest.sKey1, chItem) && Not Adventure.htblLocations.TryGetValue(rest.sKey1, locItem))
                    {
                        r = False
                        GoTo SkipTest;
                    }

                    switch (rest.eItem)
                    {
                        case clsRestriction.ItemEnum.BeAtLocation:
                            {
                            if (obItem IsNot null)
                            {
                                r = obItem.IsVisibleAtLocation(rest.sKey2)
                            ElseIf chItem != null Then
                                r = chItem.Location.LocationKey = rest.sKey2
                            Else
                                r = locItem.Key = rest.sKey2
                            }
                        case clsRestriction.ItemEnum.BeCharacter:
                            {
                            r = chItem IsNot Nothing AndAlso chItem.Key = rest.sKey2
                        case clsRestriction.ItemEnum.BeInGroup:
                            {
                            private clsGroup grp = null;
                            if (Adventure.htblGroups.TryGetValue(rest.sKey2, grp))
                            {
                                r = grp.arlMembers.Contains(rest.sKey1)
                            }
                        case clsRestriction.ItemEnum.BeInSameLocationAsCharacter:
                            {
                            private clsCharacter chKey2 = null;
                            if (Adventure.htblCharacters.TryGetValue(rest.sKey2, chKey2))
                            {
                                if (obItem IsNot null)
                                {
                                    r = chKey2.CanSeeObject(obItem.Key)
                                ElseIf chItem != null Then
                                    r = chKey2.CanSeeCharacter(chItem.Key)
                                Else
                                    r = chKey2.Location.LocationKey = locItem.Key
                                }
                            }
                        case clsRestriction.ItemEnum.BeInSameLocationAsObject:
                            {
                            private clsObject obKey2 = null;
                            if (Adventure.htblObjects.TryGetValue(rest.sKey2, obKey2))
                            {
                                r = False
                                if (obItem IsNot null)
                                {
                                    For Each sOb1Loc As String In obItem.LocationRoots.Keys
                                        For Each sOb2Loc As String In obKey2.LocationRoots.Keys
                                            If sOb1Loc = sOb2Loc Then r = true
                                            Exit For;
                                        Next;
                                    Next;
                                ElseIf chItem != null Then
                                    r = chItem.CanSeeObject(obKey2.Key)
                                Else
                                    r = obKey2.LocationRoots.ContainsKey(locItem.Key)
                                }
                            }
                        case clsRestriction.ItemEnum.BeInsideObject:
                            {
                            private clsObject obKey2 = null;
                            if (Adventure.htblObjects.TryGetValue(rest.sKey2, obKey2))
                            {
                                r = obKey2.Children(clsObject.WhereChildrenEnum.InsideObject, True).ContainsKey(rest.sKey1)
                            }
                        case clsRestriction.ItemEnum.BeLocation:
                            {
                            r = locItem IsNot Nothing AndAlso locItem.Key = rest.sKey2
                        case clsRestriction.ItemEnum.BeObject:
                            {
                            r = obItem IsNot Nothing AndAlso obItem.Key = rest.sKey2
                        case clsRestriction.ItemEnum.BeOnCharacter:
                            {
                            private clsCharacter chKey2 = null;
                            if (Adventure.htblCharacters.TryGetValue(rest.sKey2, chKey2))
                            {
                                if (obItem IsNot null)
                                {
                                    r = obItem.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter AndAlso obItem.Location.Key = chKey2.Key
                                ElseIf chItem != null Then
                                    r = chKey2.Children(True).ContainsKey(chItem.Key)
                                Else
                                    r = False
                                }
                            }
                        case clsRestriction.ItemEnum.BeType:
                            {
                            r = (rest.sKey2 = "Object" AndAlso obItem IsNot Nothing) OrElse (rest.sKey2 = "Character" AndAlso chItem IsNot Nothing) OrElse (rest.sKey2 = "Location" AndAlso locItem IsNot Nothing)
                        case clsRestriction.ItemEnum.Exist:
                            {
                            r = obItem IsNot Nothing OrElse chItem IsNot Nothing OrElse locItem IsNot Nothing
                        case clsRestriction.ItemEnum.HaveProperty:
                            {
                            if (obItem IsNot null)
                            {
                                r = obItem.HasProperty(rest.sKey2)
                            ElseIf chItem != null Then
                                r = chItem.HasProperty(rest.sKey2)
                            Else
                                r = locItem.HasProperty(rest.sKey2)
                            }
                    }

                case clsRestriction.RestrictionTypeEnum.Property:
                    {
                    private clsItem item = null;
                    private bool bItemContainsProperty = false;
                    if (Adventure.htblObjects.ContainsKey(rest.sKey2))
                    {
                        item = Adventure.htblObjects(rest.sKey2)
                        bItemContainsProperty = CType(item, clsObject).HasProperty(rest.sKey1)
                    ElseIf Adventure.htblCharacters.ContainsKey(rest.sKey2) Then
                        item = Adventure.htblCharacters(rest.sKey2)
                        bItemContainsProperty = CType(item, clsCharacter).HasProperty(rest.sKey1)
                    ElseIf Adventure.htblLocations.ContainsKey(rest.sKey2) Then
                        item = Adventure.htblLocations(rest.sKey2)
                        bItemContainsProperty = CType(item, clsLocation).HasProperty(rest.sKey1)
                    }

                    if (Not bItemContainsProperty)
                    {
                        r = False
                    Else
                        private clsProperty prop;
                        if (TypeOf item Is clsObject)
                        {
                            prop = CType(item, clsObject).GetProperty(rest.sKey1)
                        ElseIf TypeOf item == clsCharacter Then
                            prop = CType(item, clsCharacter).GetProperty(rest.sKey1)
                        Else
                            prop = CType(item, clsLocation).GetProperty(rest.sKey1)
                        }

                        switch (prop.Type)
                        {
                            case clsProperty.PropertyTypeEnum.CharacterKey:
                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                            case clsProperty.PropertyTypeEnum.LocationKey:
                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                {
                                private string sKey = GetReference(rest.StringValue);
                                If sKey == null Then sKey = rest.StringValue
                                r = (prop.Value = sKey)
                            case clsProperty.PropertyTypeEnum.Integer:
                                {
                                private int iCompareValue = 0;
                                if (Adventure.htblVariables.ContainsKey(rest.StringValue))
                                {
                                    iCompareValue = Adventure.htblVariables(rest.StringValue).IntValue
                                Else
                                    if (bIgnoreReferences)
                                    {
                                        For Each sRef As String In ReferenceNames()
                                            if (rest.StringValue.Contains(sRef))
                                            {
                                                r = True
                                                GoTo SkipTest;
                                            }
                                        Next;
                                    }
                                    iCompareValue = EvaluateExpression(rest.StringValue, True)
                                }
                                switch (CType(rest.IntValue, clsRestriction.VariableEnum))
                                {
                                    case clsRestriction.VariableEnum.LessThan:
                                        {
                                        r = (SafeInt(prop.Value) < iCompareValue)
                                    case clsRestriction.VariableEnum.LessThanOrEqualTo:
                                        {
                                        r = (SafeInt(prop.Value) <= iCompareValue)
                                    case clsRestriction.VariableEnum.EqualTo:
                                    case CType(-1:
                                    case clsRestriction.VariableEnum):
                                        {
                                        r = (SafeInt(prop.Value) = iCompareValue)
                                    case clsRestriction.VariableEnum.GreaterThanOrEqualTo:
                                        {
                                        r = (SafeInt(prop.Value) >= iCompareValue)
                                    case clsRestriction.VariableEnum.GreaterThan:
                                        {
                                        r = (SafeInt(prop.Value) > iCompareValue)
                                }
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                r = True
                            case clsProperty.PropertyTypeEnum.StateList:
                                {
                                r = prop.Value = rest.StringValue
                            case clsProperty.PropertyTypeEnum.Text:
                                {
                                private string sStringVal;
                                ' Could be an expression or simple text
                                if (rest.StringValue.Contains(""""))
                                {
                                    sStringVal = EvaluateExpression(rest.StringValue)
                                Else
                                    sStringVal = ReplaceFunctions(rest.StringValue)
                                }
                                r = (prop.Value = sStringVal)
                            case clsProperty.PropertyTypeEnum.ValueList:
                                {
                                private int iCompareValue = 0;
                                if (IsNumeric(rest.StringValue))
                                {
                                    iCompareValue = SafeInt(rest.StringValue)
                                Else
                                    if (bIgnoreReferences)
                                    {
                                        For Each sRef As String In ReferenceNames()
                                            if (rest.StringValue.Contains(sRef))
                                            {
                                                r = True
                                                GoTo SkipTest;
                                            }
                                        Next;
                                    }
                                    iCompareValue = EvaluateExpression(rest.StringValue, True)
                                }
                                switch (CType(rest.IntValue, clsRestriction.VariableEnum))
                                {
                                    case clsRestriction.VariableEnum.LessThan:
                                        {
                                        r = (SafeInt(prop.Value) < iCompareValue)
                                    case clsRestriction.VariableEnum.LessThanOrEqualTo:
                                        {
                                        r = (SafeInt(prop.Value) <= iCompareValue)
                                    case clsRestriction.VariableEnum.EqualTo:
                                    case CType(-1:
                                    case clsRestriction.VariableEnum):
                                        {
                                        r = (SafeInt(prop.Value) = iCompareValue)
                                    case clsRestriction.VariableEnum.GreaterThanOrEqualTo:
                                        {
                                        r = (SafeInt(prop.Value) >= iCompareValue)
                                    case clsRestriction.VariableEnum.GreaterThan:
                                        {
                                        r = (SafeInt(prop.Value) > iCompareValue)
                                }
                        }
                    }

                case clsRestriction.RestrictionTypeEnum.Direction:
                    {
                    private string sRefDirection = GetReference("ReferencedDirection");
                    r = rest.sKey1 = sRefDirection

                case clsRestriction.RestrictionTypeEnum.Expression:
                    {
                    r = SafeBool(EvaluateExpression(rest.StringValue))

            }

SkipTest:;
            if (r = (rest.eMust = clsRestriction.MustEnum.Must))
            {
                sRestrictionText = ""
                DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, restx.Summary + ": Passed");
                return true;
            Else
                sRestrictionText = restx.oMessage.ToString ' Use original, so we mark any Display Once as seen
                if (rest.eType = clsRestriction.RestrictionTypeEnum.Character && rest.eCharacter = clsRestriction.CharacterEnum.HaveRouteInDirection)
                {
                    If sRouteError <> sRestrictionText && sRouteError <> "" Then sRestrictionText = sRouteError
                }
                DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, restx.Summary + ": Failed");
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error evaluating PassSingleRestriction for restriction """ + restx.ToString + """", ex);
        }

    }


    private bool EvaluateRestrictionBlock(RestrictionArrayList arlRestrictions, string sBlock, bool bIgnoreReferences = false, clsTask tas = null)
    {

        'Debug.WriteLine(sBlock)

        while (sBlock.Contains("A#O"))
        {
            ' #A#O# => (#A#)O#
            ' #A#A#O# => (#A#A#)O#
            '(#O#)A#O# => ((#O#)A#)O#
            '#A(#A#O#) => #A((#A#)O#)
            ' Insert a ( at beginning, or after previous (
            private int i = sBlock.IndexOf("A#O");
            private int j = sBlock.Substring(0, i).LastIndexOf("(") + 1;
            sBlock = sLeft(sBlock, j) & "(" & sBlock.Substring(j, i - j) & "A#)O" & sBlock.Substring(i + 3)
        }

        switch (Left(sBlock, 1))
        {
            case "(":
                {
                ' Get block
                private int iBrackDepth = 1;
                private string sSubBlock = "(";
                private int iOffset = 1;
                while (iBrackDepth > 0)
                {
                    private string s = sBlock.Substring(iOffset, 1);
                    switch (s)
                    {
                        case "(":
                            {
                            iBrackDepth += 1;
                        case ")":
                            {
                            iBrackDepth -= 1;
                        default:
                            {
                            ' Do nothing
                    }
                    sSubBlock &= s;
                    iOffset += 1;
                }
                sSubBlock = sSubBlock.Substring(1, sSubBlock.Length - 2) 'Left(sSubBlock, sSubBlock.Length - 1)
                if (sBlock.Length - 2 = sSubBlock.Length)
                {
                    return EvaluateRestrictionBlock(arlRestrictions, sSubBlock, bIgnoreReferences);
                Else
                    switch (sBlock.Substring(sSubBlock.Length + 2, 1) 'sBlock.Substring(1, 1))
                    {
                        case "A":
                            {
                            private bool bFirst = EvaluateRestrictionBlock(arlRestrictions, sSubBlock, bIgnoreReferences);
                            if (Not bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(sSubBlock.Length + 3), "#"c);
                                return false;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3), bIgnoreReferences);
                            }
                            'Return EvaluateRestrictionBlock(arlRestrictions, sSubBlock) AndAlso EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3)) 'sBlock.Substring(2))
                        case "O":
                            {
                            private bool bFirst = EvaluateRestrictionBlock(arlRestrictions, sSubBlock, bIgnoreReferences);
                            if (bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(sSubBlock.Length + 3), "#"c);
                                return true;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3), bIgnoreReferences);
                            }
                            'Return EvaluateRestrictionBlock(arlRestrictions, sSubBlock) OrElse EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(sSubBlock.Length + 3)) 'sBlock.Substring(2))
                        default:
                            {
                            ' Error
                    }
                }
            case "#":
                {
                iRestNum += 1;
                if (sBlock.Length = 1)
                {
                    return PassSingleRestriction(arlRestrictions(iRestNum - 1), bIgnoreReferences);
                Else
                    switch (sBlock.Substring(1, 1))
                    {
                        case "A":
                            {
                            private bool bFirst = PassSingleRestriction(arlRestrictions(iRestNum - 1), bIgnoreReferences);
                            if (Not bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(2), "#"c);
                                return false;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2), bIgnoreReferences);
                            }
                            'Return PassSingleRestriction(arlRestrictions(iRestNum - 1)) AndAlso EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2))
                        case "O":
                            {
                            private bool bFirst = PassSingleRestriction(arlRestrictions(iRestNum - 1), bIgnoreReferences);
                            if (bFirst)
                            {
                                iRestNum += CharacterCount(sBlock.Substring(2), "#"c);
                                return true;
                            Else
                                return EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2), bIgnoreReferences);
                            }
                            'Return PassSingleRestriction(arlRestrictions(iRestNum - 1)) OrElse EvaluateRestrictionBlock(arlRestrictions, sBlock.Substring(2))
                        default:
                            {
                            ' Error
                    }
                }
            default:
                {
                if (tas IsNot null)
                {
                    ErrMsg("Bad Bracket Sequence in task """ + tas.Description + """");
                Else
                    ErrMsg("Bad Bracket Sequence");
                }

        }

    }



    ' IgnoreReferences is for when we are evaluating whether the task is completable or not, but we don't have any refs yet
    public bool PassRestrictions(RestrictionArrayList arlRestrictions, bool bIgnoreReferences = false, clsTask tas = null)
    {

        private int iRestNumIn = iRestNum;
        iRestNum = 0

        try
        {
            sRouteError = ""

            if (arlRestrictions.Count = 0)
            {
                return true;
            Else
                ' We have to check each combination of objects from our task
                ' e.g. "get %objects% from %object2%
                ' "get red ball and blue ball from box"
                ' get red ball from box
                ' get blue ball from box
                return EvaluateRestrictionBlock(arlRestrictions, arlRestrictions.BracketSequence, bIgnoreReferences, tas);
            }

        }
        finally
        {
            iRestNum = iRestNumIn
        }

    }


    private bool InputMatchesObjects(clsTask task, string sInput, int iNewRef, bool bExcepts = false, bool bPlural = false, bool bSecondChance = false)
    {

        private System.Text.RegularExpressions.Regex re;

        if (Not bPlural)
        {
            re = New System.Text.RegularExpressions.Regex("^((?<all>all( .+)?)|(?<objects1>.+))( (except|but|apart from) (?<objects2>.+))?$", System.Text.RegularExpressions.RegexOptions.RightToLeft Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            if (re.IsMatch(sInput))
            {
                For Each sGroupName As String In re.GetGroupNames
                    switch (sGroupName)
                    {

                        case "all":
                            {
                            private string sAll = re.Match(sInput).Groups(sGroupName).Value.Trim;
                            if (sAll <> "")
                            {
                                if (sAll = "all")
                                {
                                    ' User isn't refining 'all', so we need to populate list with all objects
                                    private int iItemNum = 0;
                                    For Each ob As clsObject In Adventure.htblObjects.SeenBy.Values
                                        task.NewReferences(iNewRef).Items.Add(New clsSingleItem);
                                        task.NewReferences(iNewRef).Items(iItemNum).MatchingPossibilities.Add(ob.Key);
                                        task.NewReferences(iNewRef).Items(iItemNum).bExplicitlyMentioned = false;
                                        task.NewReferences(iNewRef).Items(iItemNum).sCommandReference = "all";
                                        iItemNum += 1;
                                    Next;
                                Else
                                    ' i.e. all balls
                                    ' object1 should be plural here, in which case we want to match any object with that as the plural, i.e. balls, cactii, sheep
                                    If ! InputMatchesObjects(task, sAll.Substring(4), iNewRef, , true) Then GoTo NextCheck ' Return false
                                }
                            }

                        case "objects1":
                            {
                            private string sObs = re.Match(sInput).Groups(sGroupName).Value.Trim;
                            if (sObs <> "" && Not sObs.StartsWith("all "))
                            {
                                ' i.e. balls
                                ' object1 should be plural here, in which case we want to match any object with that as the plural, i.e. balls, cactii, sheep
                                If ! InputMatchesObjects(task, sObs, iNewRef, bExcepts, true) Then GoTo NextCheck
                            }

                        case "objects2":
                            {
                            private string sExcepts = re.Match(sInput).Groups(sGroupName).Value.Trim;
                            if (sExcepts <> "")
                            {
                                ' Need to go thru and remove any matching ref
                                InputMatchesObjects(task, sExcepts, iNewRef, true);
                            }

                    }
                Next;
                return true;
            }

NextCheck:;
            re = New System.Text.RegularExpressions.Regex("^(?<commaseparatedobjects>(.+), )*(?<object2>.+) and (?<object3>.+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            if (re.IsMatch(sInput))
            {
                private System.Text.RegularExpressions.Match m = re.Match(sInput);
                private int iItemNum = 0;
                if (m.Groups("commaseparatedobjects").Length > 0)
                {
                    For Each sObject As String In m.Groups("commaseparatedobjects").Value.TrimEnd(New Char() {","c, " "c}).Split(","c)
                        'If Not bExcepts Then task.NewReferences(iNewRef).Items.Add(New clsSingleItem)
                        sObject = sObject.Trim
                        If ! InputMatchesObject(task, sObject, iNewRef, iItemNum, bExcepts) Then Return false
                        iItemNum += 1;
                    Next;
                }
                'If Not bExcepts Then task.NewReferences(iNewRef).Items.Add(New clsSingleItem)
                If ! InputMatchesObject(task, m.Groups("object2").Value, iNewRef, iItemNum, bExcepts) Then Return false
                iItemNum += 1;
                'If Not bExcepts Then task.NewReferences(iNewRef).Items.Add(New clsSingleItem)
                If ! InputMatchesObject(task, m.Groups("object3").Value, iNewRef, iItemNum, bExcepts) Then Return false
                return true;
            }

        }

        ' Try to match on unique names before looking at plurals
        ' So if we have bar and bars, get bars tries to take the bars before taking the bar
        'If bPlural AndAlso InputMatchesObject(task, sInput, iNewRef, 0, bExcepts, False, bSecondChance) Then Return True
        return InputMatchesObject(task, sInput, iNewRef, 0, bExcepts, bPlural, bSecondChance);

    }


    private bool InputMatchesObject(clsTask task, string sInput, int iReferenceNum, int iItemNum, bool bExcepts = false, bool bPlural = false, bool bSecondChance = false)
    {

        private bool bResult = false;
        private bool bAddedItem = false;

        If iItemNum = 0 && bPlural Then iItemNum = -1
        For Each ob As clsObject In Adventure.htblObjects.Values '.SeenBy.Values
            if (System.Text.RegularExpressions.Regex.IsMatch(sInput, "^" + ob.sRegularExpressionString(, bPlural) + "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                bResult = True
                if (Not bExcepts)
                {
                    If bPlural Then iItemNum += 1
                    If bPlural || ! bAddedItem Then task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
                    bAddedItem = True

                    task.NewReferences(iReferenceNum).Items(iItemNum).MatchingPossibilities.Add(ob.Key);
                    task.NewReferences(iReferenceNum).Items(iItemNum).bExplicitlyMentioned = true;
                    task.NewReferences(iReferenceNum).Items(iItemNum).sCommandReference = sInput;
                Else
                    for (int iItem = task.NewReferences(iReferenceNum).Items.Count - 1; iItem <= 0; iItem += -1)
                    {
                        private clsSingleItem itm = task.NewReferences(iReferenceNum).Items(iItem);
                        If itm.MatchingPossibilities.Contains(ob.Key) Then itm.MatchingPossibilities.Remove(ob.Key)
                        If itm.MatchingPossibilities.Count = 0 Then task.NewReferences(iReferenceNum).Items.RemoveAt(iItem)
                    Next;
                }
            }
        Next;

        'If Not bAddedItem Then
        '    task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
        '    task.NewReferences(iReferenceNum).Items(iItemNum).sCommandReference = sInput
        'End If

        if (Not bResult && bSecondChance && task.HasObjectExistRestriction)
        {
            ' TODO - This needs to check that it is the correct 'must exist' check, rather than just any
            ' If our task has a check that objects should exist, return True as a match so the task can deal with that in it's restrictions
            'task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
            'task.NewReferences(iReferenceNum).Items(0).MatchingPossibilities.Add("NonExistingObject")
            'task.NewReferences(iReferenceNum).Items(0).bExplicitlyMentioned = False
            'task.NewReferences(iReferenceNum).Items(0).sCommandReference = sInput
            return true;
        }

        return bResult;

    }


    private bool InputMatchesCharacters(clsTask task, string sInput, int iNewRef, bool bExcepts = false, bool bSecondChance = false)
    {

        private System.Text.RegularExpressions.Regex re;

NextCheck:;
        re = New System.Text.RegularExpressions.Regex("^(?<commaseparatedcharacters>(.+), )*(?<character2>.+) and (?<character3>.+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        if (re.IsMatch(sInput))
        {
            private System.Text.RegularExpressions.Match m = re.Match(sInput);
            private int iItemNum = 0;
            if (m.Groups("commaseparatedcharacters").Length > 0)
            {
                For Each sCharacter As String In m.Groups("commaseparatedcharacters").Value.TrimEnd(New Char() {","c, " "c}).Split(","c)
                    'If Not bExcepts Then task.NewReferences(iNewRef).Items.Add(New clsSingleItem)
                    sCharacter = sCharacter.Trim
                    If ! InputMatchesCharacter(task, sCharacter, iNewRef, iItemNum, bExcepts) Then Return false
                    iItemNum += 1;
                Next;
            }
            If ! InputMatchesCharacter(task, m.Groups("character2").Value, iNewRef, iItemNum, bExcepts) Then Return false
            iItemNum += 1;
            If ! InputMatchesCharacter(task, m.Groups("character3").Value, iNewRef, iItemNum, bExcepts) Then Return false
            return true;
        }

        return InputMatchesCharacter(task, sInput, iNewRef, 0, bExcepts, bSecondChance);

    }


    private bool InputMatchesLocation(clsTask task, string sInput, int iReferenceNum, int iItemNum, bool bExcepts = false, bool bSecondChance = false)
    {

        private bool bResult = false;
        private bool bAddedLoc = false;

        For Each l As clsLocation In Adventure.htblLocations.Values '.SeenBy.Values
            if (System.Text.RegularExpressions.Regex.IsMatch(sInput, "^" + l.sRegularExpressionString() + "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                bResult = True
                if (Not bExcepts)
                {
                    If ! bAddedLoc Then task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
                    bAddedLoc = True

                    task.NewReferences(iReferenceNum).Items(iItemNum).MatchingPossibilities.Add(l.Key);
                    task.NewReferences(iReferenceNum).Items(iItemNum).bExplicitlyMentioned = true;
                    task.NewReferences(iReferenceNum).Items(iItemNum).sCommandReference = sInput;
                Else
                    for (int iItem = task.NewReferences(iReferenceNum).Items.Count - 1; iItem <= 0; iItem += -1)
                    {
                        private clsSingleItem itm = task.NewReferences(iReferenceNum).Items(iItem);
                        If itm.MatchingPossibilities.Contains(l.Key) Then itm.MatchingPossibilities.Remove(l.Key)
                        If itm.MatchingPossibilities.Count = 0 Then task.NewReferences(iReferenceNum).Items.RemoveAt(iItem)
                    Next;
                }
            }
        Next;

        if (Not bResult && bSecondChance && task.HasLocationExistRestriction)
        {
            ' TODO - This needs to check that it is the correct 'must exist' check, rather than just any
            ' If our task has a check that objects should exist, return True as a match so the task can deal with that in it's restrictions
            'task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
            'task.NewReferences(iReferenceNum).Items(0).MatchingPossibilities.Add("NonExistingObject")
            'task.NewReferences(iReferenceNum).Items(0).bExplicitlyMentioned = False
            'task.NewReferences(iReferenceNum).Items(0).sCommandReference = sInput
            return true;
        }

        return bResult;

    }


    private bool InputMatchesCharacter(clsTask task, string sInput, int iReferenceNum, int iItemNum, bool bExcepts = false, bool bSecondChance = false)
    {

        private bool bResult = false;
        private bool bAddedChar = false;

        'If iItemNum = 0 AndAlso bPlural Then iItemNum = -1
        For Each ch As clsCharacter In Adventure.htblCharacters.Values '.SeenBy.Values
            if (System.Text.RegularExpressions.Regex.IsMatch(sInput, "^" + ch.sRegularExpressionString() + "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                bResult = True
                if (Not bExcepts)
                {
                    'If bPlural Then iItemNum += 1
                    'If bPlural OrElse Not bAddedChar Then task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
                    If ! bAddedChar Then task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
                    bAddedChar = True

                    task.NewReferences(iReferenceNum).Items(iItemNum).MatchingPossibilities.Add(ch.Key);
                    task.NewReferences(iReferenceNum).Items(iItemNum).bExplicitlyMentioned = true;
                    task.NewReferences(iReferenceNum).Items(iItemNum).sCommandReference = sInput;
                Else
                    for (int iItem = task.NewReferences(iReferenceNum).Items.Count - 1; iItem <= 0; iItem += -1)
                    {
                        private clsSingleItem itm = task.NewReferences(iReferenceNum).Items(iItem);
                        If itm.MatchingPossibilities.Contains(ch.Key) Then itm.MatchingPossibilities.Remove(ch.Key)
                        If itm.MatchingPossibilities.Count = 0 Then task.NewReferences(iReferenceNum).Items.RemoveAt(iItem)
                    Next;
                }
            }
        Next;

        if (Not bResult && bSecondChance && task.HasCharacterExistRestriction)
        {
            ' TODO - This needs to check that it is the correct 'must exist' check, rather than just any
            ' If our task has a check that objects should exist, return True as a match so the task can deal with that in it's restrictions
            'task.NewReferences(iReferenceNum).Items.Add(New clsSingleItem)
            'task.NewReferences(iReferenceNum).Items(0).MatchingPossibilities.Add("NonExistingObject")
            'task.NewReferences(iReferenceNum).Items(0).bExplicitlyMentioned = False
            'task.NewReferences(iReferenceNum).Items(0).sCommandReference = sInput
            return true;
        }

        return bResult;

    }



    ' If the command contains asterixes, strip them out, then add them in one by one until we (possibly) get a match
    internal List<System.Text.RegularExpressions.Regex> GetRegularExpression(string sCommand, string sInput, bool bRightToLeft)
    {

        private New List<System.Text.RegularExpressions.Regex> lRegExs;

        try
        {
            private System.Text.RegularExpressions.RegexOptions options = System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            If bRightToLeft Then options = options | System.Text.RegularExpressions.RegexOptions.RightToLeft

            if (sCommand.Contains("*"))
            {
                sCommand = sCommand.Replace("**", "*")
                private int iAsterixCount = 0;
                private string sTestCommand = sCommand;

                while (sTestCommand.Contains("*"))
                {
                    sTestCommand = Replace(sTestCommand, "*", "", , 1)
                    iAsterixCount += 1;
                }

                ' We should really try all combinations of wildcard removal.
                ' E.g. " * %object * " should give us " * %object% " and " %object% * "
                ' otherwise we may always end up matching the object name in *

                ' For * # * # * we want
                ' _ # _ # _
                ' * # _ # _
                ' _ # * # _
                ' _ # _ # *
                ' * # * # _
                ' * # _ # *
                ' _ # * # *
                ' * # * # *

                for (int iAsterix = iAsterixCount - 1; iAsterix <= -1; iAsterix += -1)
                {
                    sTestCommand = sCommand
                    for (int i = 0; i <= iAsterix; i++)
                    {
                        sTestCommand = Replace(sTestCommand, "*", "", , 1)
                    Next;
                    private string sPattern = ConvertToRE(sTestCommand);
                    private New System.Text.RegularExpressions.Regex(sPattern, options) re;
                    If sInput = "" || re.IsMatch(sInput) Then lRegExs.Add(re) ' Return re
                Next;

                return lRegExs ' null;
            Else
                private string sPattern = ConvertToRE(sCommand);
                lRegExs.Add(New System.Text.RegularExpressions.Regex(sPattern, options));
                return lRegExs;
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error in command """ + sCommand + """", ex);
            return lRegExs 'new System.Text.RegularExpressions.Regex("");
        }

    }


    'Public Function DirectionRE(ByVal d As DirectionsEnum) As String

    '    Select Case d
    '        Case [Global].DirectionsEnum.North
    '            Return "(north|n)"
    '        Case [Global].DirectionsEnum.East
    '            Return "(east|e)"
    '        Case [Global].DirectionsEnum.South
    '            Return "(south|s)"
    '        Case [Global].DirectionsEnum.West
    '            Return "(west|w)"
    '        Case [Global].DirectionsEnum.Up
    '            Return "(up|u)"
    '        Case [Global].DirectionsEnum.Down
    '            Return "(down|d)"
    '        Case [Global].DirectionsEnum.In
    '            Return "(inside|in)"
    '        Case [Global].DirectionsEnum.Out
    '            Return "(outside|out|o)"
    '        Case [Global].DirectionsEnum.NorthEast
    '            Return "(northeast|north-east|north_east|n-e|ne)"
    '        Case [Global].DirectionsEnum.SouthEast
    '            Return "(southeast|south-east|south_east|s-e|se)"
    '        Case [Global].DirectionsEnum.SouthWest
    '            Return "(southwest|south-west|south_west|s-w|sw)"
    '        Case [Global].DirectionsEnum.NorthWest
    '            Return "(northwest|north-west|north_west|n-w|nw)"
    '        Case Else
    '            Return ""
    '    End Select

    'End Function


    private void InputMatchesCommand(clsTask task, string sInput, string sCommand, bool bSecondChance, bool bRightToLeft, List(Of System.Text.RegularExpressions.Regex RegExs)
    {

        If RegExs == null Then RegExs = GetRegularExpression(sCommand, sInput, bRightToLeft)

        For Each re As System.Text.RegularExpressions.Regex In RegExs
            private int iNewReferences = 0;

            ' Clear the references
            if (task.NewReferences IsNot null)
            {
                for (int i = 0; i <= task.NewReferences.Length - 1; i++)
                {
                    task.NewReferences(i) = null;
                Next;
            }

            If ! re.IsMatch(sInput) Then GoTo DoesntMatch

            private StringArrayList sRefs = task.References;

            For Each sGroupName As String In re.GetGroupNames
                switch (sGroupName)
                {
                    case "objects":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Object);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sObjectsInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! InputMatchesObjects(task, sObjectsInput, iNewReferences - 1, , , bSecondChance) Then GoTo DoesntMatch ' Return false

                    case "object1":
                    case "object2":
                    case "object3":
                    case "object4":
                    case "object5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Object);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sObjectInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! InputMatchesObject(task, sObjectInput, iNewReferences - 1, 0, , , bSecondChance) Then GoTo DoesntMatch ' Return false

                    case "characters":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Character);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sCharactersInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! InputMatchesCharacters(task, sCharactersInput, iNewReferences - 1, , bSecondChance) Then GoTo DoesntMatch

                    case "character1":
                    case "character2":
                    case "character3":
                    case "character4":
                    case "character5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Character);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sCharacterInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! InputMatchesCharacter(task, sCharacterInput, iNewReferences - 1, 0, , bSecondChance) Then GoTo DoesntMatch

                    case "location1":
                    case "location2":
                    case "location3":
                    case "location4":
                    case "location5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Location);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sLocationInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! InputMatchesLocation(task, sLocationInput, iNewReferences - 1, 0, , bSecondChance) Then GoTo DoesntMatch

                    case "item1":
                    case "item2":
                    case "item3":
                    case "item4":
                    case "item5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Item);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sItemInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        If ! (InputMatchesObjects(task, sItemInput, iNewReferences - 1, , , bSecondChance) || InputMatchesCharacter(task, sItemInput, iNewReferences - 1, 0, , bSecondChance) || InputMatchesLocation(task, sItemInput, iNewReferences - 1, 0, , bSecondChance)) Then GoTo DoesntMatch

                    case "direction1":
                    case "direction2":
                    case "direction3":
                    case "direction4":
                    case "direction5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Direction);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sDirInput = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        With task.NewReferences(iNewReferences - 1);
                            '.ReferenceType = ReferencesType.Direction
                            private string sDirTest = "";
                            for (DirectionsEnum dr = DirectionsEnum.North; dr <= DirectionsEnum.NorthWest; dr++)
                            {
                                sDirTest = DirectionRE(dr, True, True)
                                if (System.Text.RegularExpressions.Regex.IsMatch(sDirInput, "^" + sDirTest + "$"))
                                {
                                    .Items.Add(New clsSingleItem);
                                    .Items(0).MatchingPossibilities.Add(dr.ToString) ' DirectionName(dr));
                                    .Items(0).sCommandReference = sDirInput;
                                    Exit For;
                                }
                            Next;
                        }

                    case "number1":
                    case "number2":
                    case "number3":
                    case "number4":
                    case "number5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Number);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sNumber = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        With task.NewReferences(iNewReferences - 1);
                            if (System.Text.RegularExpressions.Regex.IsMatch(sNumber, "^-?[0-9]+$"))
                            {
                                private int iRef = Math.Max(SafeInt(sGroupName.Replace("number", "")) - 1, 0);
                                .Items.Add(New clsSingleItem);
                                .Items(0).MatchingPossibilities.Add(sNumber);
                                .Items(0).sCommandReference = sNumber;
                                'Adventure.iReferencedNumber(iRef) = SafeInt(sNumber)
                                'Exit For
                            }
                        }

                    case "text1":
                    case "text2":
                    case "text3":
                    case "text4":
                    case "text5":
                        {
                        iNewReferences += 1;
                        task.NewReferences(iNewReferences - 1) = New clsNewReference(ReferencesType.Text);
                        task.NewReferences(iNewReferences - 1).ReferenceMatch = sGroupName;

                        private string sText = re.Match(sInput).Groups(sGroupName).Value.Trim;
                        With task.NewReferences(iNewReferences - 1);
                            if (System.Text.RegularExpressions.Regex.IsMatch(sText, "^.*$"))
                            {
                                private int iRef = Math.Max(SafeInt(sGroupName.Replace("text", "")) - 1, 0);
                                .Items.Add(New clsSingleItem);
                                .Items(0).MatchingPossibilities.Add(sText);
                                .Items(0).sCommandReference = sText;
                                Adventure.sReferencedText(iRef) = sText;
                                'Exit For
                            }
                        }

                }

            Next;

            return true;
DoesntMatch:
            if (sCommand.Contains("% %") && Not re.RightToLeft)
            {
                ' Ok, we have 2 references back to back.  Try a match from right to left
                return InputMatchesCommand(task, sInput, sCommand, bSecondChance, true);
            }
            'If Not bSecondChance AndAlso task.HasObjectExistRestriction AndAlso Not htblSecondChanceTasks.ContainsKey(task.Key) Then htblSecondChanceTasks.Add(task, task.Key)
            if (Not bSecondChance && Not htblSecondChanceTasks.ContainsKey(task.Key))
            {
                If iNewReferences > 0 && iNewReferences <= task.NewReferences.Length && (
                    (task.NewReferences(iNewReferences - 1).ReferenceType = ReferencesType.Object && task.HasObjectExistRestriction) _;
                    || (task.NewReferences(iNewReferences - 1).ReferenceType = ReferencesType.Character && task.HasCharacterExistRestriction) _;
                    || (task.NewReferences(iNewReferences - 1).ReferenceType = ReferencesType.Location && task.HasLocationExistRestriction)) Then;
                    htblSecondChanceTasks.Add(task, task.Key);
                }
            }

            'Return False

        Next;

        return false;

        'Dim re As System.Text.RegularExpressions.Regex = GetRegularExpression(sCommand, sInput, bRightToLeft)
        'If re Is Nothing Then Return False

        'Dim sPattern As String = ConvertToRE(sCommand)
        'Dim re As New System.Text.RegularExpressions.Regex(sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)










    }


    private clsNewReference CopyRef(clsNewReference ref)
    {

        private clsNewReference nr = null;

        if (ref IsNot null)
        {
            nr = New clsNewReference(ref.ReferenceType)
            for (int iItem = 0; iItem <= ref.Items.Count - 1; iItem++)
            {
                private New clsSingleItem itm;
                itm.MatchingPossibilities = ref.Items(iItem).MatchingPossibilities.Clone;
                itm.bExplicitlyMentioned = ref.Items(iItem).bExplicitlyMentioned;
                itm.sCommandReference = ref.Items(iItem).sCommandReference;
                nr.Items.Add(itm);
            Next;
            nr.Index = ref.Index;
            nr.ReferenceMatch = ref.ReferenceMatch;
        }

        return nr;

    }


    private void RefineMatchingPossibilitesUsingRestrictions(clsTask task)
    {

        DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Checking scope: Applicable");

        Dim NewRefs(task.NewReferencesWorking.Length - 1) As clsNewReference

        'Dim htblNotAdded(task.NewReferencesWorking.Length - 1) As Generic.Dictionary(Of String, ReferencesType) ' Hashtable
        'Dim htblAdded(task.NewReferencesWorking.Length - 1) As Generic.Dictionary(Of String, ReferencesType) 'Hashtable
        Dim lAdded(task.NewReferencesWorking.Length - 1) As Generic.List(Of String)

        for (int i = 0; i <= task.NewReferencesWorking.Length - 1; i++)
        {
            'htblNotAdded(i) = New Generic.Dictionary(Of String, ReferencesType) 'Hashtable
            'htblAdded(i) = New Generic.Dictionary(Of String, ReferencesType) 'Hashtable
            lAdded(i) = New Generic.List(Of String);
            if (task.NewReferencesWorking(i) IsNot null)
            {
                NewRefs(i) = New clsNewReference(task.NewReferencesWorking(i).ReferenceType);
                NewRefs(i).Index = task.NewReferencesWorking(i).Index;
                NewRefs(i).ReferenceMatch = task.NewReferencesWorking(i).ReferenceMatch;
            }
        Next;

        ' We have to try every combination of references against all the others to see if it is a successful combination
        if (task.NewReferencesWorking IsNot null && task.NewReferencesWorking.Length > 0 && task.NewReferencesWorking(0) IsNot null)
        {

            For Each itm0 As clsSingleItem In task.NewReferencesWorking(0).Items

                private bool bAddedItem0 = false;
                ReDim NewReferences(0);
                NewReferences(0) = New clsNewReference(task.NewReferencesWorking(0).ReferenceType);
                NewReferences(0).Index = task.NewReferencesWorking(0).Index;
                NewReferences(0).ReferenceMatch = task.NewReferencesWorking(0).ReferenceMatch;
                private New clsSingleItem itmOut0;
                itmOut0.sCommandReference = itm0.sCommandReference;
                'NewRefs(0).Items.Add(itmOut0)

                For Each sKey0 As String In itm0.MatchingPossibilities

                    NewReferences(0).Items.Clear();
                    private New clsSingleItem itmSingle0;
                    itmSingle0.MatchingPossibilities.Add(sKey0);
                    NewReferences(0).Items.Add(itmSingle0);

                    if (task.NewReferencesWorking.Length > 1)
                    {

                        For Each itm1 As clsSingleItem In task.NewReferencesWorking(1).Items

                            private bool bAddedItem1 = false;
                            ReDim Preserve NewReferences(1);
                            NewReferences(1) = New clsNewReference(task.NewReferencesWorking(1).ReferenceType);
                            NewReferences(1).Index = task.NewReferencesWorking(1).Index;
                            NewReferences(1).ReferenceMatch = task.NewReferencesWorking(1).ReferenceMatch;
                            private New clsSingleItem itmOut1;
                            itmOut1.sCommandReference = itm1.sCommandReference;
                            'NewRefs(1).Items.Add(itmOut1)

                            For Each sKey1 As String In itm1.MatchingPossibilities

                                NewReferences(1).Items.Clear();
                                private New clsSingleItem itmSingle1;
                                itmSingle1.MatchingPossibilities.Add(sKey1);
                                NewReferences(1).Items.Add(itmSingle1);

                                if (task.NewReferencesWorking.Length > 2)
                                {
                                    ' TODO, when more than 2 refs...
                                Else
                                    if (PassRestrictions(task.arlRestrictions))
                                    {
                                        If ! bAddedItem0 Then NewRefs(0).Items.Add(itmOut0) ' was && ! lAdded(0).Contains(sKey0)
                                        If ! bAddedItem1 Then NewRefs(1).Items.Add(itmOut1) ' was && ! lAdded(1).Contains(sKey1)
                                        If ! lAdded(0).Contains(sKey0) Then itmOut0.MatchingPossibilities.Add(sKey0)
                                        If ! lAdded(1).Contains(sKey1) Then itmOut1.MatchingPossibilities.Add(sKey1)
                                        bAddedItem0 = True
                                        bAddedItem1 = True
                                        If ! lAdded(0).Contains(sKey0) Then lAdded(0).Add(sKey0)
                                        If ! lAdded(1).Contains(sKey1) Then lAdded(1).Add(sKey1)
                                    }
                                }
                            Next sKey1;

                            ' Make a note whether we added the reference or not
                            'If bAddedItem1 Then
                            '    If Not htblAdded(1).ContainsKey(itm1.sCommandReference) AndAlso Not htblNotAdded(1).ContainsKey(itm1.sCommandReference) Then htblAdded(1).Add(itm1.sCommandReference, task.NewReferencesWorking(1).ReferenceType)
                            '    If htblNotAdded(1).ContainsKey(itm1.sCommandReference) Then htblNotAdded(1).Remove(itm1.sCommandReference)
                            'Else
                            '    If Not htblAdded(1).ContainsKey(itm1.sCommandReference) AndAlso Not htblNotAdded(1).ContainsKey(itm1.sCommandReference) Then htblNotAdded(1).Add(itm1.sCommandReference, task.NewReferencesWorking(1).ReferenceType)
                            'End If

                        Next itm1;
                    Else
                        if (PassRestrictions(task.arlRestrictions))
                        {
                            If ! bAddedItem0 && ! lAdded(0).Contains(sKey0) Then NewRefs(0).Items.Add(itmOut0)
                            itmOut0.MatchingPossibilities.Add(sKey0);
                            bAddedItem0 = True
                            If ! lAdded(0).Contains(sKey0) Then lAdded(0).Add(sKey0)
                        }
                    }

                Next sKey0;

                ' Make a note whether we added the reference or not
                'If bAddedItem0 Then
                '    If Not htblAdded(0).ContainsKey(itm0.sCommandReference) Then htblAdded(0).Add(itm0.sCommandReference, task.NewReferencesWorking(0).ReferenceType)
                '    If htblNotAdded(0).ContainsKey(itm0.sCommandReference) Then htblNotAdded(0).Remove(itm0.sCommandReference)
                'Else
                '    If Not htblAdded(0).ContainsKey(itm0.sCommandReference) AndAlso Not htblNotAdded(0).ContainsKey(itm0.sCommandReference) Then htblNotAdded(0).Add(itm0.sCommandReference, task.NewReferencesWorking(0).ReferenceType)
                'End If
            Next itm0;

        }

        'If False Then ' Can't see why we need to do this...
        '    ' Make sure we still add any items referenced that don't actually match our task definition
        '    For iRef As Integer = 0 To task.NewReferencesWorking.Length - 1
        '        For Each sExplicitCommandNotAdded As String In htblNotAdded(iRef).Keys
        '            Dim itm As New clsSingleItem
        '            For scope As eScope = eScope.Visible To eScope.Seen
        '                Dim bAddedItem As Boolean = False
        '                Select Case CType(htblNotAdded(iRef)(sExplicitCommandNotAdded), ReferencesType)
        '                    Case ReferencesType.Object
        '                        For Each ob As clsObject In Adventure.htblObjects.Values '.SeenBy.Values
        '                            If System.Text.RegularExpressions.Regex.IsMatch(sExplicitCommandNotAdded, "^" & ob.sRegularExpressionString & "$") Then
        '                                itm.MatchingPossibilities.Add(ob.Key)
        '                                If NewRefs(iRef) Is Nothing Then NewRefs(iRef) = New clsNewReference(ReferencesType.Object)
        '                                NewRefs(iRef).Items.Add(itm)
        '                                bAddedItem = True
        '                            End If
        '                        Next ob
        '                    Case ReferencesType.Character
        '                        For Each ch As clsCharacter In Adventure.htblCharacters.Values '.SeenBy.Values
        '                            If System.Text.RegularExpressions.Regex.IsMatch(sExplicitCommandNotAdded, "^" & ch.sRegularExpressionString & "$") Then
        '                                itm.MatchingPossibilities.Add(ch.Key)
        '                                If NewRefs(iRef) Is Nothing Then NewRefs(iRef) = New clsNewReference(ReferencesType.Character)
        '                                NewRefs(iRef).Items.Add(itm)
        '                                bAddedItem = True
        '                            End If
        '                        Next ch
        '                    Case ReferencesType.Direction
        '                        Dim sDir As String = ""
        '                        For d As DirectionsEnum = DirectionsEnum.North To DirectionsEnum.NorthWest ' Adventure.iCompassPoints ' DirectionsEnum.NorthWest
        '                            sDir = DirectionRE(d, True, True)
        '                            If System.Text.RegularExpressions.Regex.IsMatch(sExplicitCommandNotAdded, "^" & sDir & "$") Then
        '                                itm.MatchingPossibilities.Add(d.ToString)
        '                                If NewRefs(iRef) Is Nothing Then NewRefs(iRef) = New clsNewReference(ReferencesType.Direction)
        '                                NewRefs(iRef).Items.Add(itm)
        '                                bAddedItem = True
        '                                Exit For
        '                            End If
        '                        Next

        '                    Case Else
        '                        'TODO("Refine matching possibilites for " & CType(htblNotAdded(iRef)(sExplicitCommandNotAdded), ReferencesType).ToString & " references")
        '                End Select
        '                If bAddedItem Then Exit For ' So we don't do the next scope
        '            Next scope
        '        Next sExplicitCommandNotAdded
        '    Next iRef
        'End If

        ' If this returns no items and we had items before, return all visible items


        ' Check to see if this refined our possibilities to unique items
        private bool bCheckNextScope = false;
        'Dim ApplicableRefs() As clsNewReference = Nothing ' Because even tho we may not resolve to a single item, it may well be a better result than Visible or Seen scopes
        'Dim bResetNewRefs(NewRefs.Length - 1) As Boolean

        for (int iNR = 0; iNR <= NewRefs.Length - 1 ' Each nr As clsNewReference In NewRefs; iNR++)
        {
            private clsNewReference nr = NewRefs(iNR);
            private bool bResetRef = false;
            if (nr IsNot null && nr.Items IsNot null)
            {
                if (nr.Items.Count = 0)
                {
                    bCheckNextScope = True
                    bResetRef = True
                }
                for (int iI = 0; iI <= nr.Items.Count - 1 ' Each itm As clsSingleItem In nr.Items; iI++)
                {
                    private clsSingleItem itm = nr.Items(iI);
                    if (itm.MatchingPossibilities.Count = 0)
                    {
                        bCheckNextScope = True
                        ' Reset refs to original
                        if (task.NewReferencesWorking(iNR).Items.Count > iI)
                        {
                            itm.MatchingPossibilities = task.NewReferencesWorking(iNR).Items(iI).MatchingPossibilities.Clone;
                            bResetRef = True
                        }
                    ElseIf itm.MatchingPossibilities.Count > 1 Then
                        bCheckNextScope = True
                        'ApplicableRefs = CType(NewRefs.Clone, clsNewReference())
                        ' Leave existing refs to further refine by visibility
                    }
                Next;
                If bResetRef Then NewRefs(iNR) = CopyRef(task.NewReferences(iNR))
            }
        Next;
        if (bCheckNextScope)
        {
            DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Checking scope: Visible");

            'If bResetNewRefs Then NewRefs = task.CopyNewRefs(task.NewReferences) ' Think this is problem, as it is resetting NewRefs, refined above
            For Each nr As clsNewReference In NewRefs
                if (nr IsNot null)
                {
                    For Each itm As clsSingleItem In nr.Items
                        for (int i = itm.MatchingPossibilities.Count - 1; i <= 0; i += -1)
                        {
                            switch (nr.ReferenceType)
                            {
                                case ReferencesType.Object:
                                    {
                                    If ! Adventure.htblObjects(itm.MatchingPossibilities(i)).IsVisibleTo(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                case ReferencesType.Character:
                                    {
                                    If ! Adventure.htblCharacters(itm.MatchingPossibilities(i)).CanSeeCharacter(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                case ReferencesType.Direction:
                                case ReferencesType.Number:
                                case ReferencesType.Text:
                                    {
                                    ' Don't think these are applicable
                                case ReferencesType.Location:
                                    {
                                    ' Doesn't make sense to restrict by visible for Location
                                case ReferencesType.Item:
                                    {
                                    private string sItemKey = itm.MatchingPossibilities(i);
                                    private clsObject ob = null;
                                    private clsCharacter ch = null;
                                    if (Adventure.htblObjects.TryGetValue(sItemKey, ob))
                                    {
                                        If ! ob.IsVisibleTo(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                    ElseIf Adventure.htblCharacters.TryGetValue(sItemKey, ch) Then
                                        If ! ch.CanSeeCharacter(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                    }
                                default:
                                    {
                                    TODO("Refine visible possibilites for " + nr.ReferenceType.ToString + " references");
                            }
                        Next i;
                    Next itm;
                }
            Next nr;
        }

        ' Check to see if this refined our possibilities to unique items
        bCheckNextScope = False

        for (int iNR = 0; iNR <= NewRefs.Length - 1 ' Each nr As clsNewReference In NewRefs; iNR++)
        {
            private clsNewReference nr = NewRefs(iNR);
            private bool bResetRef = false;
            if (nr IsNot null && nr.Items IsNot null)
            {
                if (nr.Items.Count = 0)
                {
                    bCheckNextScope = True
                    bResetRef = True
                }
                for (int iI = nr.Items.Count - 1; iI <= 0; iI += -1 ' Each itm As clsSingleItem In nr.Items)
                {
                    private clsSingleItem itm = nr.Items(iI);
                    if (itm.MatchingPossibilities.Count = 0)
                    {
                        nr.Items.RemoveAt(iI);
                        if (nr.Items.Count = 0)
                        {
                            bCheckNextScope = True
                            ' Reset refs to original
                            itm.MatchingPossibilities = task.NewReferencesWorking(iNR).Items(iI).MatchingPossibilities.Clone;
                            bResetRef = True
                        Else
                            ' We still have some items, so at least our Visible scope reduced from previous
                        }
                    ElseIf itm.MatchingPossibilities.Count > 1 Then
                        ' bCheckNextScope = True - no point checking next scope as that will always be a superset of Visible
                        'If ApplicableRefs Is Nothing Then ApplicableRefs = CType(NewRefs.Clone, clsNewReference()) ' Only use this ambiguous set if the Applicable scope didn't return anything
                        ' Leave existing refs to further refine by visibility
                        bCheckNextScope = True ' JCW - Added 13/08/12 - not sure how we would be leaving to further refine otherwise...
                    }
                Next;
                If bResetRef Then NewRefs(iNR) = CopyRef(task.NewReferences(iNR))
            }
        Next;

        if (bCheckNextScope)
        {
            DebugPrint(ItemEnum.Task, task.Key, DebugDetailLevelEnum.High, "Checking scope: Seen");

            ' Remove any unseen references from our set
            'If bResetNewRefs Then NewRefs = task.CopyNewRefs(task.NewReferences)
            For Each nr As clsNewReference In NewRefs
                if (nr IsNot null)
                {
                    For Each itm As clsSingleItem In nr.Items
                        for (int i = itm.MatchingPossibilities.Count - 1; i <= 0; i += -1)
                        {
                            switch (nr.ReferenceType)
                            {
                                case ReferencesType.Object:
                                    {
                                    If ! Adventure.htblObjects(itm.MatchingPossibilities(i)).SeenBy(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                case ReferencesType.Character:
                                    {
                                    If ! Adventure.htblCharacters(itm.MatchingPossibilities(i)).SeenBy(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                                case ReferencesType.Location:
                                    {
                                    If ! Adventure.htblLocations(itm.MatchingPossibilities(i)).SeenBy(Adventure.Player.Key) Then itm.MatchingPossibilities.RemoveAt(i)
                            }
                        Next i;
                    Next itm;
                }
            Next nr;
        }

        bCheckNextScope = False

        ' Check to see if this refined our possibilities to unique items
        for (int iNR = 0; iNR <= NewRefs.Length - 1; iNR++)
        {
            private clsNewReference nr = NewRefs(iNR);
            private bool bResetRef = false;
            if (nr IsNot null && nr.Items IsNot null)
            {
                if (nr.Items.Count = 0)
                {
                    bCheckNextScope = True
                    bResetRef = True
                }
                for (int iI = 0; iI <= nr.Items.Count - 1; iI++)
                {
                    private clsSingleItem itm = nr.Items(iI);
                    if (itm.MatchingPossibilities.Count = 0)
                    {
                        bCheckNextScope = True
                        bResetRef = True
                        ' Reset refs to original
                        'itm.MatchingPossibilities = task.NewReferencesWorking(iNR).Items(iI).MatchingPossibilities.Clone
                    ElseIf itm.MatchingPossibilities.Count > 1 Then
                        bCheckNextScope = True
                        ' Leave existing refs to further refine by visibility
                    }
                Next;
                If bResetRef Then NewRefs(iNR) = CopyRef(task.NewReferences(iNR))
            }
        Next;

        'If bCheckNextScope AndAlso ApplicableRefs IsNot Nothing Then
        '    task.NewReferencesWorking = ApplicableRefs
        'Else
        task.NewReferencesWorking = NewRefs;
        'End If


    }


    private string GetGeneralTask(string sInput, int iMinimumPriority, bool bSecondChance)
    {

        'Dim iPriorityPass As Integer = Integer.MaxValue ' Lowest priority task that matches input and passes restrictions
        private int iPriorityFail = Integer.MaxValue ' Lowest priority task that matches input and has output;
        'Dim bCurrentPassed As Boolean = False
        'Dim GeneralTaskRefs(-1) As clsReferences
        'Dim GeneralTaskReferencesFail As New Hashtable
        'Dim bWeHaveFailureOutput As Boolean = False
        'Dim bIsTaskLowPriority As Boolean = False

        private string sNoRefTask = "" ' We may match on a task, but find no references.  If so, at least return this rather than no task at all;

        GetGeneralTask = Nothing
        If ! bSecondChance Then htblSecondChanceTasks.Clear()
        If iMinimumPriority > 0 && iMinimumPriority < iPriorityFail Then iPriorityFail = iMinimumPriority ' Because if we're continuing on from an earlier execution, we shouldn't run lower priority tasks

        try
        {
            sAmbTask = Nothing
            private TaskHashTable htblTasks = CType(IIf(bSecondChance, htblSecondChanceTasks, htblCompleteableGeneralTasks), TaskHashTable);

            'For Each tas As clsTask In htblTasks.Values
            For Each tk As TaskKey In listTaskKeys
                if (htblTasks.ContainsKey(tk.sTaskKey))
                {
                    private clsTask tas = htblTasks(tk.sTaskKey);

                    if (tas.Priority >= iMinimumPriority && Not (tas.LowPriority && tas.Priority > iPriorityFail))
                    {

                        ' Don't run Library tasks if this is a continuation task (v4)
                        if (iMinimumPriority > 0 && tas.IsLibrary && Adventure.dVersion < 5)
                        {
                            GoTo FoundTask;
                        }

                        for (int iCom = 0; iCom <= tas.arlCommands.Count - 1; iCom++)
                        {
                            private string sCom = tas.arlCommands(iCom);

                            'If tas.Priority < iPriority OrElse bWeHaveFailureOutput Then ' was Not bWeHaveFailureOutput
                            'Dim sPattern As String = ConvertToRE(sCom) ' TODO: Cache the RegEx pattern rather than calculating it every turn!

                            if (InputMatchesCommand(tas, sInput, sCom, bSecondChance, false, tas.RegExs(iCom)))
                            {

                                DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.Medium, "Task '" + tas.Description + "' matches input.");
                                iMatchedTaskCommand = iCom

                                ' Now see if user typed unique references (or none at all) which allow us to run the task

                                private bool bOkToRun = true;

                                tas.NewReferencesWorking = tas.CopyNewRefs(tas.NewReferences);
                                ' If any refs are vague, refine using restrictions
                                private bool bMatchesPreRefine = true;
                                For Each nr As clsNewReference In tas.NewReferences
                                    if (nr IsNot null)
                                    {
                                        For Each itm As clsSingleItem In nr.Items
                                            if (itm.MatchingPossibilities.Count <> 1)
                                            {
                                                bMatchesPreRefine = False
                                                Exit For;
                                            }
                                        Next itm;
                                    }
                                Next nr;

                                ' Remove any references that don't pass the restrictions from our set
                                RefineMatchingPossibilitesUsingRestrictions(tas);

                                ' If we still have at least one matching possibility in each reference item, then run this task, else we don't pass
                                For Each nr As clsNewReference In tas.NewReferencesWorking
                                    if (nr IsNot null)
                                    {
                                        For Each itm As clsSingleItem In nr.Items
                                            if (itm.MatchingPossibilities.Count = 0)
                                            {
                                                ' Can we fall back to previous refs?  We need to fire this task rather than fall thru to not understood...
                                                if (bMatchesPreRefine)
                                                {
                                                    tas.NewReferencesWorking = tas.CopyNewRefs(tas.NewReferences);
                                                Else
                                                    DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "No matches found.");
                                                    ' Should we perhaps fall back anyway and prompt for disambiguity - unless a lower priority one matches.  Hmm.... may need
                                                    ' to run the whole thing again with a different parameter.
                                                    If sNoRefTask = "" Then sNoRefTask = tas.Key
                                                    bOkToRun = False
                                                }
                                                Exit For;
                                            }
                                            if (itm.MatchingPossibilities.Count > 1)
                                            {
                                                if (sAmbTask Is null)
                                                {
                                                    ' We have an ambiguity that needs to be resolved
                                                    DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Multiple matches.  Prompt for ambiguity.");
                                                    sAmbTask = tas.Key
                                                Else
                                                    DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Multiple matches, but we already have an ambiguity.");
                                                }
                                                bOkToRun = False
                                            }
                                        Next itm;
                                    }
                                Next nr;

                                If bOkToRun Then ' || tas.ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueNever
                                    ' Woo-hoo, we have a unique match!
                                    if (tas.References.Count > 0)
                                    {
                                        DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Command matches without ambiguity.");
                                    Else
                                        DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Command matches.");
                                    }

                                    NewReferences = tas.NewReferencesWorking
                                    private bool bDoesThisPass = PassRestrictions(tas.arlRestrictions, , tas);

                                    if (Not bDoesThisPass && tas.FailOverride.ToString <> "" && sRestrictionText = "" && ContainsWord(sInput, "all"))
                                    {
                                        sRestrictionText = tas.FailOverride.ToString
                                    }

                                    ' If we pass restrictions, or if we haven't yet passed but we have output

                                    if (bDoesThisPass || sRestrictionText <> "")
                                    {
                                        'If (bDoesThisPass AndAlso (Not bCurrentPassed OrElse tas.Priority < iPriority)) _
                                        '   OrElse ((Not bCurrentPassed OrElse (bIsTaskLowPriority AndAlso Not tas.LowPriority)) AndAlso tas.Priority < iPriority AndAlso sRestrictionText <> "") Then

                                        if (bDoesThisPass)
                                        {
                                            if (tas.Priority > iPriorityFail)
                                            {
                                                DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.Medium, "Task passes restrictions and overrides previous failing task output");
                                            Else
                                                DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.Medium, "Task passes restrictions.");
                                            }
                                        ElseIf sRestrictionText <> "" Then ' && tas.Priority < iPriority Then
                                            DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.Medium, "Task doesn't pass restrictions, but is current highest priority failing task with restriction output.");
                                        }

                                        GetGeneralTask = tas.Key
                                        If sRestrictionText <> "" Then iPriorityFail = tas.Priority

                                        DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Task priority: " + tas.Priority);

                                        If bDoesThisPass || Adventure.TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityTask Then GoTo FoundTask

                                    Else
                                        DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.Medium, "Task does not pass restrictions.");
                                    }

                                    Exit For ' task;

                                }

                            }

                            'End If
                        Next;
                    }
                }
            Next;
FoundTask:;

            If GetGeneralTask == null && sNoRefTask <> "" Then GetGeneralTask = sNoRefTask

            'References = GeneralTaskRefs
            'htblReferencesFail = CType(GeneralTaskReferencesFail.Clone, Hashtable)
            if (GetGeneralTask Is null && sAmbTask Is null && Not bSecondChance)
            {
                ' Ok, no luck.  Let's go back and see if one of our 'exist' tasks worked
                DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.Medium, "No matches found.  Checking again using existance.");
                return GetGeneralTask(sInput, iMinimumPriority, true);
            }

        }
        catch (Exception ex)
        {
            ErrMsg("GetGeneralTask Error", ex);
        }

    }


    private bool IsNoun(string sName)
    {

        private New System.Text.RegularExpressions.Regex("") re;

        For Each ob As clsObject In Adventure.htblObjects.Values
            private string sOb = ob.sRegularExpressionString;
            If System.Text.RegularExpressions.Regex.IsMatch(sName, "^" + sOb + "s?$") Then Return true
        Next ob;

        return false;

    }



    '    ' Work out which objects were actually mentioned on the command line that we need in our task
    '    Private Function CalcRefs(ByVal cTask As clsTask, ByVal sCommand As String, ByVal sPattern As String, ByVal Scope As eScope) As Boolean

    '        Dim sBlocks() As String
    '        Dim iLength As Integer = 0
    '        'Dim bReference As Boolean
    '        Dim sOrigBlock As String
    '        Dim re As New System.Text.RegularExpressions.Regex("")
    '        Dim sReplace As String = Nothing
    '        Dim iRefCount As Integer = 0
    '        Dim iRefOb As Integer
    '        Dim sOrigPattern As String = sPattern
    '        'If Not References Is Nothing Then
    '        '    Return True
    '        'End If


    '        sBlocks = Split(sPattern, "%")
    '        Erase References
    '        'Erase StartReferences
    '        'Erase ReferencesPass
    '        'Erase ReferencesFail

    '        DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "Calculating References for task " & cTask.Description & ", ", False)

    '        Select Case Scope
    '            Case eScope.Applicable
    '                DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "scope: Applicable")
    '            Case eScope.Visible
    '                DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "scope: Visible")
    '            Case eScope.Seen
    '                DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "scope: Seen")
    '        End Select

    '        For Each sBlock As String In sBlocks

    '            'bReference = False
    '            sOrigBlock = sBlock

    '            Select Case sBlock.ToLower
    '                Case "character", "characters", "direction", "number", "numbers", "object1", "object2", "object3", "object4", "object5", "objects", "text"
    '                    If sOrigPattern.Substring(iLength - 1, 1) = "%" AndAlso sOrigPattern.Substring(iLength + sBlock.Length, 1) = "%" Then
    '                        'bReference = True
    '                        iRefCount += 1

    '                        Select Case sBlock.ToLower
    '                            Case "object1", "object2", "object3", "object4", "object5", "objects"
    '                                iRefOb += 1

    '                        End Select


    '                        ReDim Preserve References(iRefCount - 1)
    '                        References(iRefCount - 1) = New clsReferences
    '                        With References(iRefCount - 1)

    '                            '.alReferences.Add(New StringArrayList) ' List of all keys matching our input

    '                            'For Each salRefs As StringArrayList In .alReferences
    '                            '    .iTotalMatching += salRefs.Count
    '                            'Next

    '                            'Dim iAddCheck As Integer = salKeys.Count

    '                            Select Case sBlock.ToLower

    '                                Case "character"
    '                                    .sRefType = ReferencesType.Character
    '                                    .bMultiple = False

    '                                Case "characters"
    '                                    .sRefType = ReferencesType.Character
    '                                    .bMultiple = True

    '                                Case "direction"
    '                                    .sRefType = ReferencesType.Direction
    '                                    .bMultiple = False

    '                                    .alReferences.Add(New SingleRef)
    '                                    For dr As DirectionsEnum = DirectionsEnum.North To Adventure.iCompassPoints ' DirectionsEnum.NorthWest
    '                                        Select Case dr
    '                                            Case [Global].DirectionsEnum.North
    '                                                sReplace = "(north|n)"
    '                                            Case [Global].DirectionsEnum.East
    '                                                sReplace = "(east|e)"
    '                                            Case [Global].DirectionsEnum.South
    '                                                sReplace = "(south|s)"
    '                                            Case [Global].DirectionsEnum.West
    '                                                sReplace = "(west|w)"
    '                                            Case [Global].DirectionsEnum.Up
    '                                                sReplace = "(up|u)"
    '                                            Case [Global].DirectionsEnum.Down
    '                                                sReplace = "(down|d)"
    '                                            Case [Global].DirectionsEnum.In
    '                                                sReplace = "(in)"
    '                                            Case [Global].DirectionsEnum.Out
    '                                                sReplace = "(outside|out|o)"
    '                                            Case [Global].DirectionsEnum.NorthEast
    '                                                sReplace = "(northeast|north-east|north_east|n-e|ne)"
    '                                            Case [Global].DirectionsEnum.SouthEast
    '                                                sReplace = "(southeast|south-east|south_east|s-e|se)"
    '                                            Case [Global].DirectionsEnum.SouthWest
    '                                                sReplace = "(southwest|south-west|south_west|s-w|sw)"
    '                                            Case [Global].DirectionsEnum.NorthWest
    '                                                sReplace = "(northwest|north-west|north_west|n-w|nw)"
    '                                        End Select
    '                                        If System.Text.RegularExpressions.Regex.IsMatch(sCommand, ConvertToRE(Replace(sPattern, "%direction%", sReplace, 1, 1))) Then
    '                                            'salKeys.Add(dr.ToString)
    '                                            .alReferences(0).salWhat.Add(dr.ToString)
    '                                            Exit For
    '                                        End If
    '                                    Next

    '                                Case "number"
    '                                    .sRefType = ReferencesType.Number
    '                                    .bMultiple = False

    '                                Case "numbers"
    '                                    .sRefType = ReferencesType.Number
    '                                    .bMultiple = True

    '                                Case "object1", "object2", "object3", "object4", "object5"
    '                                    .sRefType = ReferencesType.Object
    '                                    .bMultiple = False

    '                                    .alReferences.Add(New SingleRef)

    '                                    Dim bFound As Boolean = False
    '                                    For iScope As eScope = eScope.Applicable To eScope.Seen
    '                                        'For Each ob As clsObject In ValidReferencedObjects(cTask, iRefOb, iScope).Values
    '                                        '    sReplace = ob.sRegularExpressionString
    '                                        '    If System.Text.RegularExpressions.Regex.IsMatch(sCommand, ConvertToRE(Replace(sPattern, "%object" & iRefOb & "%", sReplace, 1, 1))) Then
    '                                        '        .alReferences(0).salWhat.Add(ob.Key)
    '                                        '        bFound = True
    '                                        '    End If
    '                                        'Next
    '                                        For Each ob As clsObject In Adventure.htblObjects.Values
    '                                            sReplace = ob.sRegularExpressionString
    '                                            If System.Text.RegularExpressions.Regex.IsMatch(sCommand, ConvertToRE(Replace(sPattern, "%object" & iRefOb & "%", sReplace, 1, 1))) Then
    '                                                If ValidReferencedObjects(cTask, iRefOb, iScope).ContainsKey(ob.Key) Then
    '                                                    .alReferences(0).salWhat.Add(ob.Key)
    '                                                    bFound = True
    '                                                End If
    '                                            End If
    '                                        Next
    '                                        If bFound Then Exit For
    '                                    Next

    '                                Case "objects"
    '                                    .sRefType = ReferencesType.Object
    '                                    .bMultiple = True

    '                                    sPattern = ConvertToRE(sPattern, False, False)
    '                                    Dim sObjectList As String = System.Text.RegularExpressions.Regex.Replace(sCommand, "^" & Left(sPattern, sInstr(sPattern, "%objects%") - 1), "")
    '                                    Dim sEndPattern As String = Right(sPattern, sPattern.Length - sInstr(sPattern, "%objects%") - 8) & "$"
    '                                    ' Need to match off any references still in sEndPattern - just use wildcards for now (lazy!)
    '                                    sEndPattern = ConvertToRE(sEndPattern, True, False)
    '                                    sObjectList = " " & System.Text.RegularExpressions.Regex.Replace(sObjectList, sEndPattern, "")

    '                                    If sObjectList <> "" Then
    '                                        sObjectList = sObjectList.Replace(" and ", " , ").Replace(" all ", " ,all, ").Replace(" but ", " except ").Replace(" apart from ", " except ").Replace(" except ", " ,except, ")
    '                                        Dim sObjects() As String = Split(sObjectList, ",")
    '                                        ' remove any empty entries
    '                                        For i As Integer = sObjects.Length - 1 To 0 Step -1
    '                                            If Trim(sObjects(i)) = "" Then
    '                                                For j As Integer = i To sObjects.Length - 2
    '                                                    sObjects(j) = sObjects(j + 1)
    '                                                Next
    '                                                ReDim Preserve sObjects(sObjects.Length - 2)
    '                                            End If
    '                                        Next
    'InsertAll:
    '                                        DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "Ok, we've got our list of requested objects: ", False)
    '                                        For Each sObject As String In sObjects
    '                                            DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, sObject & ", ", False)
    '                                        Next
    '                                        DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "")

    '                                        Dim iRef As Integer = 0
    '                                        Dim bExcept As Boolean = False
    '                                        Dim iObNum As Integer = 0
    '                                        Dim iExceptApplyTo As Integer = 0
    '                                        Dim sAllReference As String = Nothing
    '                                        For Each sObject As String In sObjects
    '                                            If sObject <> "" Then ' We've swallowed it in our all check
    '                                                iObNum += 1
    '                                                sObject = Trim(sObject)
    '                                                'InsertAll:
    '                                                Select Case sObject
    '                                                    Case "and"
    '                                                        ' ignore
    '                                                    Case "all"
    '                                                        ' Check the next word(s).  If if's a noun, apply it to all, else get all in scope
    '                                                        If iObNum < sObjects.Length Then
    '                                                            sAllReference = Trim(sObjects(iObNum))
    '                                                            If IsNoun(sAllReference) Then
    '                                                                sObjects(iObNum) = ""
    '                                                                iObNum += 1
    '                                                                If Not bExcept Then iExceptApplyTo = .alReferences.Count
    '                                                            Else
    '                                                                sAllReference = Nothing
    '                                                            End If
    '                                                        End If

    '                                                        Dim bFound As Boolean = False
    '                                                        For iScope As eScope = eScope.Applicable To eScope.Seen
    '                                                            For Each ob As clsObject In ValidReferencedObjects(cTask, iRefOb, iScope).Values
    '                                                                If sAllReference Is Nothing OrElse System.Text.RegularExpressions.Regex.IsMatch(sAllReference, "^" & ob.sRegularExpressionString & "(s)?$") Then
    '                                                                    .alReferences.Add(New SingleRef)
    '                                                                    .alReferences(iRef).salWhat.Add(ob.Key)
    '                                                                    .alReferences(iRef).bExcept = bExcept
    '                                                                    iRef += 1
    '                                                                    bFound = True
    '                                                                End If
    '                                                            Next
    '                                                            If bFound Then Exit For
    '                                                        Next

    '                                                    Case "except"
    '                                                        bExcept = Not bExcept  ' True
    '                                                    Case Else
    '                                                        Dim bAdded As Boolean = False
    '                                                        Dim bDealtWith As Boolean = False
    '                                                        'If iRef > 0 Then
    '                                                        .alReferences.Add(New SingleRef)
    '                                                        Dim bFound As Boolean = False
    '                                                        For iScope As eScope = eScope.Applicable To eScope.Seen
    '                                                            For Each ob As clsObject In ValidReferencedObjects(cTask, iRefOb, iScope).Values
    '                                                                sReplace = ob.sRegularExpressionString
    '                                                                If System.Text.RegularExpressions.Regex.IsMatch(sObject, "^" & sReplace & "$") OrElse (Not sAllReference Is Nothing AndAlso System.Text.RegularExpressions.Regex.IsMatch(sObject & " " & sAllReference, "^" & sReplace & "(s)?$")) Then
    '                                                                    bDealtWith = True
    '                                                                    .alReferences(iRef).salWhat.Add(ob.Key)
    '                                                                    .alReferences(iRef).bExcept = bExcept
    '                                                                    bAdded = True
    '                                                                    bFound = True
    '                                                                Else
    '                                                                    If System.Text.RegularExpressions.Regex.IsMatch(sObject, "^" & sReplace & "s$") Then
    '                                                                        bDealtWith = True
    '                                                                        ' Check previous references to make sure they don't apply to this noun
    '                                                                        ' e.g. get red and blue balls.  This is a bit of a fudge
    '                                                                        For i As Integer = 0 To iObNum - 2
    '                                                                            If .alReferences(i).salWhat.Count = 0 Then
    '                                                                                sObjects(i) = (sObjects(i) & " " & ob.arlNames(0)).Replace("  ", " ")
    '                                                                            End If
    '                                                                        Next
    '                                                                        ' End of fudge

    '                                                                        ' Insert the implied 'all' before the subject
    '                                                                        '.alReferences.RemoveAt(.alReferences.Count - 1)
    '                                                                        .alReferences.Clear()
    '                                                                        ReDim Preserve sObjects(sObjects.Length)
    '                                                                        For i As Integer = sObjects.Length - 2 To iObNum - 1 Step -1
    '                                                                            sObjects(i + 1) = sObjects(i)
    '                                                                        Next
    '                                                                        sObjects(iObNum - 1) = "all" ' Try "get all except big balls"
    '                                                                        sObject = "all"

    '                                                                        GoTo InsertAll

    '                                                                    End If
    '                                                                End If
    '                                                            Next
    '                                                            If bFound Then Exit For
    '                                                        Next

    '                                                        If iRef > -1 AndAlso iRef < .alReferences.Count AndAlso .alReferences(iRef).salWhat.Count = 0 Then .alReferences.RemoveAt(iRef)
    '                                                        If bAdded Then iRef += 1
    '                                                End Select
    '                                            End If
    '                                        Next sObject
    '                                    End If
    '                                Case "text"
    '                            End Select

    '                            Dim iMatchCount As Integer = 0
    '                            For Each sr As SingleRef In .alReferences
    '                                iMatchCount += sr.salWhat.Count
    '                            Next
    '                            DebugPrint(ItemEnum.Task, cTask.Key, DebugDetailLevelEnum.High, "Found " & iMatchCount & " matching " & .sRefType.ToString & CStr(IIf(iMatchCount = 1, "", "s")))
    '                            'If salKeys.Count = iAddCheck Then Return False
    '                            If iMatchCount = 0 Then Return False ' We didn't find any new
    '                            PrintOutReferences()

    '                            ' Remove this pattern so we don't reprocess it
    '                            sPattern = Replace(sPattern, "%" & sBlock & "%", ".*", 1, 1)

    '                        End With

    '                    End If
    '                Case Else
    '                    ' Not a reference
    '            End Select

    '            iLength += sOrigBlock.Length + 1

    '        Next

    '        Return True

    '    End Function


    'Private Function DeepCopy(ByVal Refs() As clsReferences) As clsReferences()

    '    Dim NewRefs() As clsReferences

    '    If Refs Is Nothing Then
    '        ReDim NewRefs(-1)
    '        Return NewRefs
    '    End If
    '    ReDim NewRefs(Refs.Length - 1)

    '    For iRef As Integer = 0 To Refs.Length - 1
    '        NewRefs(iRef) = New clsReferences
    '        NewRefs(iRef).bMultiple = Refs(iRef).bMultiple
    '        NewRefs(iRef).sRefType = Refs(iRef).sRefType
    '        Dim ref As clsReferences = Refs(iRef)
    '        For Each sal As SingleRef In ref.alReferences
    '            NewRefs(iRef).alReferences.Add(sal)
    '        Next
    '    Next

    '    Return NewRefs

    'End Function


    'Private Function CalcFailRefs(ByVal Refs() As clsReferences, ByVal arlRestrictions As RestrictionArrayList) As Boolean

    '    ' First, we need to be in a state where every reference is individual, then we check if it passes

    '    Dim bMultiple As Boolean = False
    '    CalcFailRefs = True

    '    If Refs Is Nothing Then Exit Function

    '    ' If any refs are multiples, run CalcFailRefs on each one in turn, else evaluate
    '    For Each ref As clsReferences In Refs
    '        If ref.alReferences.Count > 1 Then
    '            bMultiple = True
    '            Exit For
    '        End If
    '    Next

    '    If bMultiple Then
    '        ' Create a new copy of the reference with each ref individually, in turn
    '        For Each ref As clsReferences In Refs
    '            If ref.alReferences.Count > 1 Then
    '                For iSal As Integer = ref.alReferences.Count - 1 To 0 Step -1
    '                    'For Each sal As StringArrayList In ref.alReferences
    '                    Dim sal As StringArrayList = ref.alReferences(iSal).salWhat
    '                    If sal.Count > 0 Then
    '                        Dim alRef As New RefArrayList
    '                        Dim sr As New SingleRef
    '                        sr.salWhat = sal
    '                        alRef.Add(sr)
    '                        Dim alOrigRefs As RefArrayList = ref.alReferences
    '                        ref.alReferences = alRef
    '                        If Not CalcFailRefs(Refs, arlRestrictions) Then
    '                            ' Remove this ref from References
    '                            alOrigRefs.RemoveAt(iSal)
    '                        End If
    '                        ref.alReferences = alOrigRefs
    '                    End If
    '                Next
    '            End If
    '        Next
    '    Else
    '        ' Ok, looking at single refs only
    '        bNoDebug = True
    '        If Not PassRestrictions(arlRestrictions) Then
    '            bNoDebug = False
    '            AddFail(Refs)
    '            Return False
    '        End If
    '        bNoDebug = False
    '    End If

    'End Function

    'Private Sub AddFail(ByRef Refs() As clsReferences)

    '    If Not Refs Is Nothing AndAlso Refs.Length > 0 AndAlso Refs(0).alReferences.Count = 0 Then
    '        DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Failed (no refs!)")
    '    ElseIf Not Refs Is Nothing AndAlso Refs.Length > 0 AndAlso Refs(0).alReferences(0).salWhat.Count > 0 Then
    '        DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Failed restriction for '" & Adventure.GetNameFromKey(Refs(0).alReferences(0).salWhat(0)) & "'")
    '    Else
    '        DebugPrint(ItemEnum.Task, "", DebugDetailLevelEnum.High, "Failed restriction for key NOTHING")
    '    End If

    '    If sRestrictionText Is Nothing Then sRestrictionText = ""
    '    If htblReferencesFail.ContainsKey(sRestrictionText) Then
    '        ' Add this key into the references in the hashtable
    '        Dim RefH() As clsReferences
    '        RefH = CType(htblReferencesFail(sRestrictionText), clsReferences())
    '        For i As Integer = 0 To RefH.Length - 1
    '            If Refs(i).alReferences.Count > 0 AndAlso Not RefH(i).alReferences.Contains(Refs(i).alReferences(0)) Then
    '                RefH(i).alReferences.Add(Refs(i).alReferences(0))
    '            End If
    '        Next
    '    Else
    '        ' Add a new entry into the hashtable with this reference
    '        htblReferencesFail.Add(sRestrictionText, DeepCopy(Refs))
    '    End If

    'End Sub



    'Private Function ValidReferencedObjects(ByVal cTask As clsTask, ByVal iReferenceNumber As Integer, ByVal Scope As eScope) As ObjectHashTable

    '    Select Case Scope
    '        Case eScope.Applicable

    '            Dim htblObs As New ObjectHashTable

    '            If References(iReferenceNumber - 1).sRefType <> ReferencesType.Object Then Return Nothing

    '            If ValidRefs Is Nothing Then CalcValidReferences(cTask)
    '            Dim salKeys As StringArrayList() = CType(ValidRefs(cTask.Key), StringArrayList())
    '            If Not salKeys Is Nothing Then
    '                For Each sKey As String In salKeys(iReferenceNumber - 1)
    '                    If Not htblObs.ContainsKey(sKey) Then htblObs.Add(Adventure.htblObjects(sKey), sKey)
    '                Next
    '            End If

    '            Return htblObs

    '        Case eScope.Visible
    '            Return htblVisibleObjects

    '        Case eScope.Seen
    '            Return htblSeenObjects

    '        Case Else
    '            Return Nothing

    '    End Select

    'End Function


    ''' <summary>
    ''' Converts a lazy Advanced Command to strict format, removing possibility for double spaces
    ''' </summary>
    ''' <param name="sCommand"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    public string CorrectCommand(string sCommand)
    {
        private string sNewCommand = ProcessBlock(sCommand);
        if (sNewCommand <> sCommand)
        {
            Debug.WriteLine(String.Format("Converted ""{0}"" to ""{1}""", sCommand, sNewCommand));
            sCommand = sCommand
        }
        return sNewCommand;
    }



    ' E.g. from "get {the} ball" > "get ", from "{the} ball" > "the", " ball" > ""
    private string GetSubBlock(ref sBlock As String)
    {

        private int iDepth = 0;
        private string sNewBlock = "";

        for (int i = 0; i <= sBlock.Length - 1; i++)
        {
            sNewBlock &= sBlock(i);
            switch (sBlock(i))
            {
                case "{"c:
                case "["c:
                    {
                    if (iDepth = 0 && sNewBlock <> sBlock(i))
                    {
                        sBlock = sRight(sBlock, sBlock.Length - sNewBlock.Length + 1)
                        return sLeft(sNewBlock, i);
                    }
                    iDepth += 1;
                case "]"c:
                case "}"c:
                    {
                    iDepth -= 1;
                    if (iDepth = 0)
                    {
                        sBlock = sRight(sBlock, sBlock.Length - sNewBlock.Length)
                        return sNewBlock;
                    }
                case "/"c:
                    {
                    if (iDepth = 0)
                    {
                        sBlock = sRight(sBlock, sBlock.Length - sNewBlock.Length)
                        return sNewBlock;
                    }
            }
        Next;

        sBlock = ""
        return sNewBlock;

    }


    ' Returns True if any part of the block is mandatory (either inside [] or not in brackets at all)
    private bool ContainsMandatoryText(string sBlock)
    {

        If sBlock = "" Then Return false

        'If sBlock.Contains("[") AndAlso sBlock.Contains("]") Then
        '    If sBlock.LastIndexOf("]") > sBlock.IndexOf("[") Then Return True
        'End If

        private int iLevel = 0;
        for (int i = 0; i <= sBlock.Length - 1; i++)
        {
            switch (sBlock(i))
            {
                case " "c:
                    {
                    ' Ignore
                case "{"c:
                    {
                    iLevel += 1;
                case "}"c:
                    {
                    iLevel -= 1;
                default:
                    {
                    If iLevel = 0 Then Return true
            }
        Next;

        return false;

    }


    ' A block should be the complete entry between two brackets, or between a bracket and a slash
    private string ProcessBlockOld(string sBlock)
    {

        private string sAfter = sBlock;
        private string sNextBlock = "";
        private string sBefore = "";

        do
        {
            sNextBlock = GetSubBlock(sAfter)
            if (sNextBlock <> "")
            {
                if (sNextBlock.StartsWith("{"))
                {
                    ' */ "] {#} {" => "]{ #}{"
                    ' "] {#} [" => ?
                    ' "} {#} [" => ?
                    ' "} {#} {" => ? /*
                    ' "{#} " => "{# }" if block starts with open bracket
                    ' " {#} " => " {# }"
                    ' "}{#} " => "}{# }"        -- should this be " }{#} " => " }{# }" ?
                    if ((sBefore = "" || sRight(sBefore, 1) = " " || sRight(sBefore, 1) = "}") && sAfter.StartsWith(" "))
                    {
                        if (sNextBlock.Contains("/"))
                        {
                            sNextBlock = "{[" & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 2) & "] }"
                        Else
                            sNextBlock = sLeft(sNextBlock, sNextBlock.Length - 1) & " }"
                        }
                        sAfter = sRight(sAfter, sAfter.Length - 1)
                    }

                    ' End block
                    ' " {#}" => "{ #}" or "{ [#/#]}
                    if (sBefore.EndsWith(" ") && sAfter = "")
                    {
                        if (sNextBlock.Contains("/"))
                        {
                            sNextBlock = "{ [" & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 2) & "]}"
                        Else
                            sNextBlock = "{ " & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 1)
                        }
                        sBefore = sLeft(sBefore, sBefore.Length - 1)
                    }
                    sBefore &= "{" + ProcessBlock(sMid(sNextBlock, 2, sNextBlock.Length - 2)) + "}";
                ElseIf sNextBlock.StartsWith("[") Then
                    sBefore &= "[" + ProcessBlock(sMid(sNextBlock, 2, sNextBlock.Length - 2)) + "]";
                ElseIf sNextBlock.EndsWith("/") Then
                    sBefore &= ProcessBlock(sLeft(sNextBlock, sNextBlock.Length - 1)) + "/";
                Else
                    sBefore &= sNextBlock;
                }
            }
        Loop While sAfter <> "";

        return sBefore;

    }


    ' A block should be the complete entry between two brackets, or between a bracket and a slash
    private string ProcessBlock(string sBlock)
    {

        private string sAfter = sBlock;
        private string sNextBlock = "";
        private string sBefore = "";

        do
        {
            sNextBlock = GetSubBlock(sAfter)
            if (sNextBlock <> "")
            {
                if (sNextBlock.StartsWith("{"))
                {
                    ' */ "] {#} {" => "]{ #}{"
                    ' "] {#} [" => ?
                    ' "} {#} [" => ?
                    ' "} {#} {" => ? /*
                    ' "{#} " => "{# }" if block starts with open bracket
                    ' " {#} " => " {# }"
                    ' "}{#} " => "}{# }"        -- should this be " }{#} " => " }{# }" ?

                    private bool bContainsMandatory = ContainsMandatoryText(sAfter);
                    if (bContainsMandatory && sAfter.StartsWith(" "))
                    {
                        ' If before final [] block then _{x}_ => _{x_}
                        if (sBefore = "" || sBefore.EndsWith(" ") || sBefore.EndsWith("}"))
                        {
                            if (sNextBlock.Contains("/"))
                            {
                                sNextBlock = "{[" & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 2) & "] }"
                            Else
                                sNextBlock = sLeft(sNextBlock, sNextBlock.Length - 1) & " }"
                            }
                            sAfter = sRight(sAfter, sAfter.Length - 1)
                        }
                    ElseIf ! bContainsMandatory && sBefore.EndsWith(" ") Then
                        ' If after final [] block then _{x}_ => {_x}_
                        if (sNextBlock.Contains("/"))
                        {
                            sNextBlock = "{ [" & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 2) & "]}"
                        Else
                            sNextBlock = "{ " & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 1)
                        }
                        sBefore = sLeft(sBefore, sBefore.Length - 1)
                    }

                    ' End block
                    ' " {#}" => "{ #}" or "{ [#/#]}
                    if (sBefore.EndsWith(" ") && sAfter = "")
                    {
                        if (sNextBlock.Contains("/"))
                        {
                            sNextBlock = "{ [" & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 2) & "]}"
                        Else
                            sNextBlock = "{ " & sLeft(sNextBlock.Substring(1), sNextBlock.Length - 1)
                        }
                        sBefore = sLeft(sBefore, sBefore.Length - 1)
                    }
                    sBefore &= "{" + ProcessBlock(sMid(sNextBlock, 2, sNextBlock.Length - 2)) + "}";
                ElseIf sNextBlock.StartsWith("[") Then
                    sBefore &= "[" + ProcessBlock(sMid(sNextBlock, 2, sNextBlock.Length - 2)) + "]";
                ElseIf sNextBlock.EndsWith("/") Then
                    sBefore &= ProcessBlock(sLeft(sNextBlock, sNextBlock.Length - 1)) + "/";
                Else
                    sBefore &= sNextBlock;
                }
            }
        Loop While sAfter <> "";

        return sBefore;

    }


    private string sDirectionsRE;
    public string ConvertToRE(string sCommand, bool bReplaceRefs = true, bool bTerminate = true)
    {

        private string sC = sCommand;

        ' Convert any special RE characters
        'sC = sC.Replace("&", "\&")
        sC = sC.Replace("+", "\+").Replace("(", "\(").Replace(")", "\)")

        ' Convert wildcards
        sC = sC.Replace(" * ", " ([[#]] )?")
        sC = sC.Replace("* ", "([[#]] )?")
        sC = sC.Replace(" *", "( [[#]])?")
        sC = sC.Replace("*", "[[#]]")
        sC = sC.Replace("[[#]]", ".*?")
        'sC = sC.Replace("*", ".*")

        'sC = sC.Replace(" ", "( )?").Replace("( )?*( )?", "( .* | )").Replace("( )?*", "( .*| )").Replace("*( )?", "(.* | )")
        sC = sC.Replace("_", " ")

        ' Convert optional text and mandatory text
        sC = sC.Replace("{", "(").Replace("}", ")?").Replace("[", "(").Replace("]", ")").Replace("/", "|")

        if (bReplaceRefs)
        {
            ' Replace references
            if (sDirectionsRE Is null)
            {
                for (DirectionsEnum eDirection = DirectionsEnum.North; eDirection <= DirectionsEnum.NorthWest; eDirection++)
                {
                    sDirectionsRE &= Adventure.sDirectionsRE(eDirection).ToLower.Replace("/", "|");
                    If eDirection <> DirectionsEnum.NorthWest Then sDirectionsRE &= "|"
                Next;
            }
            sC = sC.Replace("%direction%", "(?<direction>" & sDirectionsRE & ")") ' north|east|south|west|up|down|in|out|northeast|southeast|southwest|northwest|n|e|s|w|u|d|o|ne|se|sw|nw|north-east|south-east|south-west|north-west|n-e|s-e|s-w|n-w|outside|inside)")
            ' TODO - replace above with custom names if changed

            sC = sC.Replace("%objects%", "(?<objects>.+?)")
            sC = sC.Replace("%characters%", "(?<characters>.+?)")
            'sC = sC.Replace("%text%", "(?<text>.+?)") ' ? after + makes this a non-greedy match
            'sC = sC.Replace("%number%", "(?<number>-?[0-9]+)")
            'sC = sC.Replace("%location%", "(?<location>.+?)")
            'sC = sC.Replace("%item%", "(?<item>.+?)")

            for (int iref = 1; iref <= 5; iref++)
            {
                sC = sC.Replace("%object" & iref & "%", "(?<object" & iref & ">.+?)")
                sC = sC.Replace("%character" & iref & "%", "(?<character" & iref & ">.+?)")
                sC = sC.Replace("%number" & iref & "%", "(?<number" & iref & ">-?[0-9]+)")
                sC = sC.Replace("%text" & iref & "%", "(?<text" & iref & ">.+?)")
                sC = sC.Replace("%location" & iref & "%", "(?<location" & iref & ">.+?)")
                sC = sC.Replace("%item" & iref & "%", "(?<item" & iref & ">.+?)")
                sC = sC.Replace("%direction" & iref & "%", "(?<direction" & iref & ">" & sDirectionsRE & ")")
            Next;
        }

        'While sC.Contains("( )?( )?")
        '    sC = sC.Replace("( )?( )?", "( )?")
        'End While
        'sC = sC.Replace("( )?", " ")
        sC = sC.Trim

        if (bTerminate)
        {
            return "^" + sC + "$";
        Else
            return sC;
        }

    }





    '' Calculate the possible valid references for each task prior to command execution
    'Private Sub CalcValidReferences(ByVal tas As clsTask)

    '    Try
    '        bNoDebug = True

    '        Dim tmpRefs() As clsReferences = References

    '        ValidRefs = New Hashtable
    '        ValidRefs.Clear()

    '        'For Each tas As clsTask In htblCompleteableGeneralTasks.Values

    '        Dim iNumRefs As Integer = NumberOfRefs(tas)
    '        Dim sRefs(iNumRefs - 1) As StringArrayList

    '        ReDim References(iNumRefs - 1)
    '        For iRef As Integer = 0 To iNumRefs - 1
    '            References(iRef) = New clsReferences
    '        Next

    '        If iNumRefs > 0 Then
    '            sRefs(0) = New StringArrayList
    '            ' i.e. if we have referenced objects, go thru all the objects and check if it passes restrictions
    '            For Each sKey0 As String In GetHtblKeys(GetReferenceKey(tas.arlCommands(0), 1), 0)

    '                If iNumRefs > 1 Then
    '                    sRefs(1) = New StringArrayList
    '                    For Each sKey1 As String In GetHtblKeys(GetReferenceKey(tas.arlCommands(0), 2), 1)

    '                        If iNumRefs > 2 Then
    '                            ' Do as and when I need more than 2 refs...
    '                        Else
    '                            If Check(tas, New String() {sKey0, sKey1}) Then
    '                                sRefs(0).Add(sKey0)
    '                                sRefs(1).Add(sKey1)
    '                                bNoDebug = False
    '                                DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "References " & Adventure.GetNameFromKey(sKey0) & " & " & Adventure.GetNameFromKey(sKey1) & " match.")
    '                                bNoDebug = True
    '                            End If
    '                        End If

    '                    Next
    '                Else
    '                    If Check(tas, New String() {sKey0}) Then
    '                        sRefs(0).Add(sKey0)
    '                        bNoDebug = False
    '                        DebugPrint(ItemEnum.Task, tas.Key, DebugDetailLevelEnum.High, "Reference " & Adventure.GetNameFromKey(sKey0) & " matches.")
    '                        bNoDebug = True
    '                    End If
    '                End If

    '            Next
    '        Else
    '            ' Nothing to check
    '        End If

    '        ValidRefs.Add(tas.Key, sRefs)

    '        'Next

    '        References = tmpRefs

    '        bNoDebug = False

    '    Catch ex As Exception
    '        ErrMsg("CalcCalidReferences error", ex)
    '    End Try

    'End Sub



    '' Try out different things in the references to see which one passes the restrictions
    'Private Function Check(ByVal cTask As clsTask, ByVal sRefs() As String) As Boolean

    '    ReDim References(sRefs.Length - 1)
    '    For iRef As Integer = 0 To sRefs.Length - 1
    '        References(iRef) = New clsReferences
    '        References(iRef).alReferences.Add(New SingleRef)
    '        Select Case GetReferenceKey(cTask.arlCommands(0), iRef + 1)
    '            Case "ReferencedObject1", "ReferencedObject2", "ReferencedObject3", "ReferencedObject4", "ReferencedObject5", "ReferencedObjects"
    '                References(iRef).sRefType = ReferencesType.Object
    '            Case "ReferencedDirection"
    '                References(iRef).sRefType = ReferencesType.Direction
    '            Case Else
    '                iRef = iRef
    '        End Select
    '        'References(iRef).sRefType = ReferencesType.Object
    '        References(iRef).alReferences(0).salWhat.Add(sRefs(iRef))
    '    Next

    '    Return PassRestrictions(cTask.arlRestrictions)
    '    'If PassRestrictions(cTask.arlRestrictions) Then

    '    'End If

    'End Function


    private System.Collections.ICollection GetHtblKeys(string sRefType, int iRef)
    {

        switch (sRefType)
        {
            case "ReferencedObject1":
            case "ReferencedObject2":
            case "ReferencedObject3":
            case "ReferencedObject4":
            case "ReferencedObject5":
            case "ReferencedObjects":
                {
                return Adventure.htblObjects.Keys;
            case "ReferencedDirection":
            case "number":
            case "character":
            case "text":
                {
                return new Collection;
            default:
                {
                iRef = iRef
        }

        return null;

    }

    private object GetHtblAtPos(Hashtable htbl, int iPos)
    {

        private int iCounter = 0;
        For Each item As Object In htbl.Values
            iCounter += 1;
            If iCounter = iPos Then Return item
        Next;

        return null;

    }


    ' Returns the number of references in a task
    private int NumberOfRefs(clsTask cTask)
    {

        Dim sTokens() As String = cTask.arlCommands(0).Split(" "c)
        private int iNoRefs = 0;

        For Each sToken As String In sTokens
            switch (sToken)
            {
                case "%character%":
                case "%characters":
                case "%direction%":
                case "%number%":
                case "%numbers%":
                case "%object1%":
                case "%object2%":
                case "%object3%":
                case "%object4%":
                case "%object5%":
                case "%objects%":
                case "%text%":
                case "%location%":
                    {
                    iNoRefs += 1;
            }
        Next;

        return iNoRefs;

    }




    internal void RunnerStartup()
    {
        bShowShortLocations = CBool(GetSetting("ADRIFT", "Runner", "showshortroom", "-1"))
        bGraphics = CBool(GetSetting("ADRIFT", "Runner", "Graphics", "-1"))
        bSound = CBool(GetSetting("ADRIFT", "Runner", "Sound", "-1"))

        bUseDefaultFont = CBool(GetSetting("ADRIFT", "Runner", "Myfont", "0"))
        private string sFontName = GetSetting("ADRIFT", "Runner", "FontName", "Arial");
        private float iFontSize = CSng(GetSetting("ADRIFT", "Runner", "Font Size", "12"));
        DefaultFont = New Font(sFontName, iFontSize, CType(IIf(CBool(GetSetting("ADRIFT", "Runner", "FontBold", "0")), FontStyle.Bold, FontStyle.Regular), FontStyle) Or CType(IIf(CBool(GetSetting("ADRIFT", "Runner", "FontItalic", "0")), FontStyle.Italic, FontStyle.Regular), FontStyle))

    }


    public void New()
    {
        Me.Map = New ADRIFT.Map();
        Me.Map.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.Map.Location = New System.Drawing.Point(0, 0);
        Me.Map.Map = null;
        Me.Map.Name = "Map";
        Me.Map.ShowAxes = false;
        Me.Map.ShowGrid = false;
        Me.Map.Size = New System.Drawing.Size(189, 190);
        Me.Map.TabIndex = 0;
    }


    private void tmrEvents_Tick(object sender, EventArgs e)
    {
        TimeBasedStuff();
    }

}
}