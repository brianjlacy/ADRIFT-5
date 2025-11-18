using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsAdventure
{

    internal ItemDictionary dictAllItems;
    internal LocationHashTable htblLocations;
    internal ObjectHashTable htblObjects;
    internal TaskHashTable htblTasks;
    internal EventHashTable htblEvents;
    internal CharacterHashTable htblCharacters;
    internal GroupHashTable htblGroups;
    internal VariableHashTable htblVariables;
    internal ALRHashTable htblALRs;
    internal HintHashTable htblHints;
    internal UDFHashTable htblUDFs;
    internal PropertyHashTable htblAllProperties;
    internal PropertyHashTable htblObjectProperties;
    internal PropertyHashTable htblCharacterProperties;
    internal PropertyHashTable htblLocationProperties;
    internal StringArrayList htblAllTopicKeys;
    internal SynonymHashTable htblSynonyms;
    internal List<string> listKnownWords;
#if Generator
    internal FolderDictionary dictFolders;
#endif
    internal clsMap Map;

    private Description oIntroduction;
    private Description oWinningText;
    private string sTitle;
    private string sAuthor;
    private string sNotUnderstood;
    private string sFilename;
    private string sFullPath;
    private bool bChanged;

    Public ReadOnly Property DynamicObjects As IEnumerable(Of clsObject)
        {
            get
            {
#if Mono
            private New List<clsObject> l;
            For Each o As clsObject In htblObjects.Values
                If ! o.IsStatic Then l.Add(o)
            Next;
            return l;
#else
            return htblObjects.Values.Where(Function(o) o.IsStatic = false);
#endif
        }
    }

#if Mono
    public New List<string> listExcludedItems;
#else
    public New HashSet<String) ' List(Of String> listExcludedItems;
#endif


    public double dVersion;
    'Public bV4Compatibility As Boolean

    'Friend iCompassPoints As DirectionsEnum
    private int iMaxScore;
    public int iScore;
    public int Turns;
    Public sReferencedText(4) As String
    'Public iReferencedNumber(4) As Integer
    internal String ' Who we are in conversation with sConversationCharKey;
    internal String ' Where we currently are in the conversation tree sConversationNode;
    'Friend LastPerspective As clsCharacter.PerspectiveEnum = clsCharacter.PerspectiveEnum.None
    internal string sGameFilename;
    internal clsAction.EndGameEnum eGameState = clsAction.EndGameEnum.Running;
    internal bool bDisplayedWinLose = false;
    internal New Generic.Queue<string> qTasksToRun;
    'Friend Perspectives As New Dictionary(Of Integer, clsCharacter.PerspectiveEnum) ' Index where perspective starts in text
    internal int iElapsed;
    Friend sDirectionsRE(11) As String;
    internal List<clsTask> lstTasks;
    internal New Dictionary<string, List<int>> dictRandValues;


public enum TaskExecutionEnum
    {
        HighestPriorityTask;
        HighestPriorityPassingTask ' v4 logic - tries to execute first matching passing task, if that fails first matching failing task;
    }
    internal TaskExecutionEnum TaskExecution = TaskExecutionEnum.HighestPriorityTask;

    private string _UserStatus;
    public string sUserStatus
        {
            get
            {
            return _UserStatus;
        }
set(String)
            _UserStatus = value
        }
    }

    private string sCoverFilename;
    internal string CoverFilename
        {
            get
            {
            return sCoverFilename;
        }
set(String)
            if (value <> sCoverFilename)
            {
                try
                {
                    If BabelTreatyInfo.Stories(0).Cover == null Then BabelTreatyInfo.Stories(0).Cover = New clsBabelTreatyInfo.clsStory.clsCover
                    With BabelTreatyInfo.Stories(0).Cover;
                        .imgCoverArt = null;
                        if (IO.File.Exists(value))
                        {
                            '.imgCoverArt = Image.FromFile(value)
                            private int iLength = CInt(FileLen(value));
                            private byte[] bytImage;
                            private New IO.FileStream(value, IO.FileMode.Open, IO.FileAccess.Read) fs;
                            ReDim bytImage(iLength - 1);
                            fs.Read(bytImage, 0, CInt(iLength));
                            .imgCoverArt = New Bitmap(New IO.MemoryStream(bytImage));
                            fs.Close();
                            .Format = IO.Path.GetExtension(value).ToLower.Substring(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrMsg("Failed to set Cover Art", ex);
                }
                sCoverFilename = value
            }
        }
    }
    '<VBFixedString(8)>
    internal string Password;

    internal New Generic.Dictionary<clsCharacter.GenderEnum, Generic.List<string>> lCharMentionedThisTurn;
    'Friend lSheMentionedThisTurn As Generic.List(Of String)
    'Friend lItMentionedThisTurn As Generic.List(Of String)

    public New clsBabelTreatyInfo BabelTreatyInfo;


public class v4Media
    {
        public int iOffset;
        public int iLength;
        public Boolean ' else Sound bImage;

        public void New(int iOffset, int iLength, bool bImage)
        {
            Me.iOffset = iOffset;
            Me.iLength = iLength;
            Me.bImage = bImage;
        }
    }
    public Dictionary<string, v4Media> dictv4Media;


    Public ReadOnly Property Version As String
        {
            get
            {
            If Adventure.dVersion = 0 Then Return "N/A"
            private string sVersion = String.Format("{0:0.000000}", Adventure.dVersion) ' E.g. 5.000020;
            return sVersion(0) + "." + CInt(sVersion.Substring(2, 2)) + "." + CInt(sVersion.Substring(4, 4));
        }
    }


    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
#if Generator
            fGenerator.UTMMain.Tools("mnuSave").SharedProps.Enabled = bChanged;
#endif
        }
    }

    private DateTime dtLastUpdated;
    internal DateTime LastUpdated { get; set; }
        {
            get
            {
            if (dtLastUpdated > Date.MinValue)
            {
                return dtLastUpdated;
            Else
                return Now;
            }
        }
set(ByVal Date)
            dtLastUpdated = value
        }
    }

    public string KeyPrefix { get; set; }

    private clsCharacter oPlayer;
    public clsCharacter Player { get; set; }
        {
            get
            {
            if (oPlayer Is null)
            {
                For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    if (ch.CharacterType = clsCharacter.CharacterTypeEnum.Player)
                    {
                        oPlayer = ch
                        return ch;
                    }
                Next;
                if (Adventure.htblCharacters.Count > 0)
                {
                    For Each ch As clsCharacter In Adventure.htblCharacters.Values
                        oPlayer = ch
                        return ch;
                    Next;
                }
            Else
                return oPlayer;
            }
            return null;
        }
set(ByVal Value As clsCharacter)
            oPlayer = Value
        }
    }


    ' Returns a list of all references to images within the adventure
    Public ReadOnly Property Images As List(Of String)
        {
            get
            {
            private New List<string> lImages;
            'If BabelTreatyInfo.Stories(0).Cover IsNot Nothing AndAlso BabelTreatyInfo.Stories(0).Cover.Filename <> "" Then lImages.Add(BabelTreatyInfo.Stories(0).Cover.Filename)
            If Adventure.CoverFilename <> "" Then lImages.Add(Adventure.CoverFilename)

            'For Each itm As clsItem In Me.dictAllItems.Values
            'For Each d As Description In itm.AllDescriptions
            For Each sd As SingleDescription In AllDescriptions ' d
                private string s = sd.Description;
                s = s.Replace(" src =", " src=")
                private int i = s.IndexOf("<img ");
                while (i > -1)
                {
                    private int j = s.IndexOf(" src=", i);
                    if (j > -1)
                    {
                        private int k = s.IndexOf(">", i);
                        private string sTag = s.Substring(j + 5, k - j - 5);
                        If sTag.StartsWith("""") Then sTag = sTag.Substring(1)
                        If sTag.EndsWith("""") Then sTag = sTag.Substring(0, sTag.Length - 1)
                        If ! lImages.Contains(sTag) && (IO.File.Exists(sTag) || sTag.StartsWith("http")) Then lImages.Add(sTag)
                    }
                    i = s.IndexOf("<img ", i + 1)
                }
            Next;
            'Next
            'Next

            return lImages;
        }
    }



    Friend ReadOnly Property AllDescriptions As List(Of SingleDescription)
        {
            get
            {
            private New List<SingleDescription> lDescriptions;
            For Each sd As SingleDescription In Adventure.Introduction
                lDescriptions.Add(sd);
            Next;
            For Each sd As SingleDescription In Adventure.WinningText
                lDescriptions.Add(sd);
            Next;
            For Each itm As clsItem In Adventure.dictAllItems.Values
                For Each d As Description In itm.AllDescriptions
                    For Each sd As SingleDescription In d
                        lDescriptions.Add(sd);
                    Next;
                Next;
            Next;
            return lDescriptions;
        }
    }

#if Generator
    ' Returns a list of all references to images within the adventure
    Public ReadOnly Property Sounds As List(Of String)
        {
            get
            {
            private New List<string> lSounds;

            For Each sd As SingleDescription In AllDescriptions ' d
                private string s = sd.Description;
                private New System.Text.RegularExpressions.Regex(AUDREGEX, System.Text.RegularExpressions.RegexOptions.IgnoreCase) re;
                if (re.IsMatch(s))
                {
                    For Each m As System.Text.RegularExpressions.Match In re.Matches(s)
                        if (m.Groups("src").Length > 0)
                        {
                            private string sLocation = m.Groups("src").Value ' sMid(sMatch, 10, sMatch.Length - 10);
                            If IO.File.Exists(sLocation) && ! lSounds.Contains(sLocation) Then lSounds.Add(sLocation)
                        }
                    Next;
                }
            Next;

            return lSounds;
        }
    }
#endif


    public New Dictionary<string, int> BlorbMappings;


    public int Score { get; set; }
        {
            get
            {
            if (htblVariables.ContainsKey("Score"))
            {
                iScore = htblVariables("Score").IntValue
            }
            return iScore;
        }
set(ByVal Integer)
            if (value <> iScore)
            {
                if (htblVariables.ContainsKey("Score"))
                {
                    htblVariables("Score").IntValue = value;
                }
            }
            iScore = value
#if Runner
            UserSession.UpdateStatusBar();
#endif
        }
    }

    public int MaxScore { get; set; }
        {
            get
            {
            if (htblVariables.ContainsKey("MaxScore"))
            {
                iMaxScore = htblVariables("MaxScore").IntValue
            }
            return iMaxScore;
        }
set(ByVal Integer)
            if (value <> iMaxScore)
            {
                if (htblVariables.ContainsKey("MaxScore"))
                {
                    htblVariables("MaxScore").IntValue = value;
                }
#if Generator
                fGenerator.StatusBar1.Panels(2).Text = "Maximum score: " + value;
#endif
            }
            iMaxScore = value
        }
    }

#Region "Key Stuff"
    public bool IsItemLibrary(string sKey)
    {

        if (AllKeys.Contains(sKey))
        {
            switch (GetTypeFromKeyNice(sKey))
            {
                case "Location":
                    {
                    return htblLocations(sKey).IsLibrary;
                case "Object":
                    {
                    return htblObjects(sKey).IsLibrary;
                case "Task":
                    {
                    return htblTasks(sKey).IsLibrary;
                case "Event":
                    {
                    return htblEvents(sKey).IsLibrary;
                case "Character":
                    {
                    return htblCharacters(sKey).IsLibrary && sKey <> Player.Key;
                case "Group":
                    {
                    return htblGroups(sKey).IsLibrary;
                case "Variable":
                    {
                    return htblVariables(sKey).IsLibrary;
                case "Text Override":
                    {
                    return htblALRs(sKey).IsLibrary;
                case "Hint":
                    {
                    return htblHints(sKey).IsLibrary;
                case "Property":
                    {
                    return htblAllProperties(sKey).IsLibrary;
                case "UserFunction":
                    {
                    return htblUDFs(sKey).IsLibrary;
                case "Synonym":
                    {
                    return htblSynonyms(sKey).IsLibrary;
            }
        Else
            return false;
        }

    }


    public clsItem GetItemFromKey(string sKey)
    {

        If sKey = "" Then Return null

        If htblLocations.ContainsKey(sKey) Then Return htblLocations(sKey)
        If htblObjects.ContainsKey(sKey) Then Return htblObjects(sKey)
        If htblTasks.ContainsKey(sKey) Then Return htblTasks(sKey)
        If htblEvents.ContainsKey(sKey) Then Return htblEvents(sKey)
        If htblCharacters.ContainsKey(sKey) || sKey = THEPLAYER Then Return htblCharacters(sKey)
        If htblGroups.ContainsKey(sKey) Then Return htblGroups(sKey)
        If htblVariables.ContainsKey(sKey) Then Return htblVariables(sKey)
        If htblALRs.ContainsKey(sKey) Then Return htblALRs(sKey)
        If htblHints.ContainsKey(sKey) Then Return htblHints(sKey)
        If htblAllProperties.ContainsKey(sKey) Then Return htblAllProperties(sKey)
        If htblUDFs.ContainsKey(sKey) Then Return htblUDFs(sKey)
        If htblSynonyms.ContainsKey(sKey) Then Return htblSynonyms(sKey)

#if Generator
        If dictFolders.ContainsKey(sKey) Then Return dictFolders(sKey)
#endif
        return null;

    }


    public System.Type GetTypeFromKey(string sKey)
    {

        GetTypeFromKey = Nothing

        switch (sKey)
        {
            case "ReferencedObject":
            case "ReferencedObjects":
            case "ReferencedObject1":
            case "ReferencedObject2":
            case "ReferencedObject3":
            case "ReferencedObject4":
            case "ReferencedObject5":
                {
                return GetType(clsObject);
            case "ReferencedCharacter":
            case "ReferencedCharacters":
            case "ReferencedCharacter1":
            case "ReferencedCharacter2":
            case "ReferencedCharacter3":
            case "ReferencedCharacter4":
            case "ReferencedCharacter5":
                {
                return GetType(clsCharacter);
            case "ReferencedLocation":
                {
                return GetType(clsLocation);
        }
        If htblLocations.ContainsKey(sKey) Then Return htblLocations(sKey).GetType
        If htblObjects.ContainsKey(sKey) Then Return htblObjects(sKey).GetType
        If htblTasks.ContainsKey(sKey) Then Return htblTasks(sKey).GetType
        If htblEvents.ContainsKey(sKey) Then Return htblEvents(sKey).GetType
        If htblCharacters.ContainsKey(sKey) || sKey = THEPLAYER Then Return htblCharacters(sKey).GetType
        If htblGroups.ContainsKey(sKey) Then Return htblGroups(sKey).GetType
        If htblVariables.ContainsKey(sKey) Then Return htblVariables(sKey).GetType
        If htblALRs.ContainsKey(sKey) Then Return htblALRs(sKey).GetType
        If htblHints.ContainsKey(sKey) Then Return htblHints(sKey).GetType
        If htblAllProperties.ContainsKey(sKey) Then Return htblAllProperties(sKey).GetType
        If htblUDFs.ContainsKey(sKey) Then Return htblUDFs(sKey).GetType
        If htblSynonyms.ContainsKey(sKey) Then Return htblSynonyms(sKey).GetType
#if Generator
        If dictFolders.ContainsKey(sKey) Then Return dictFolders(sKey).GetType
#endif

    }


    public string GetTypeFromKeyNice(string sKey, bool bPlural = false)
    {

        if (bPlural)
        {
            If htblLocations.ContainsKey(sKey) Then Return "Locations"
            If htblObjects.ContainsKey(sKey) Then Return "Objects"
            If htblTasks.ContainsKey(sKey) Then Return "Tasks"
            If htblEvents.ContainsKey(sKey) Then Return "Events"
            If htblCharacters.ContainsKey(sKey) Then Return "Characters"
            If htblGroups.ContainsKey(sKey) Then Return "Groups"
            If htblVariables.ContainsKey(sKey) Then Return "Variables"
            If htblALRs.ContainsKey(sKey) Then Return "Text Overrides"
            If htblHints.ContainsKey(sKey) Then Return "Hints"
            If htblAllProperties.ContainsKey(sKey) Then Return "Properties"
            If htblUDFs.ContainsKey(sKey) Then Return "User Functions"
            If htblSynonyms.ContainsKey(sKey) Then Return "Synonyms"
#if Generator
            If dictFolders.ContainsKey(sKey) Then Return "Folders"
#endif
        Else
            If htblLocations.ContainsKey(sKey) Then Return "Location"
            If htblObjects.ContainsKey(sKey) Then Return "Object"
            If htblTasks.ContainsKey(sKey) Then Return "Task"
            If htblEvents.ContainsKey(sKey) Then Return "Event"
            If htblCharacters.ContainsKey(sKey) Then Return "Character"
            If htblGroups.ContainsKey(sKey) Then Return "Group"
            If htblVariables.ContainsKey(sKey) Then Return "Variable"
            If htblALRs.ContainsKey(sKey) Then Return "Text Override"
            If htblHints.ContainsKey(sKey) Then Return "Hint"
            If htblAllProperties.ContainsKey(sKey) Then Return "Property"
            If htblUDFs.ContainsKey(sKey) Then Return "User Function"
            If htblSynonyms.ContainsKey(sKey) Then Return "Synonym"
#if Generator
            If dictFolders.ContainsKey(sKey) Then Return "Folder"
#endif
        }

        return "";

    }


    public string GetNameFromKey(string sKey, bool bQuoted = true, bool bPrefixItem = true, bool bPCase = false)
    {

        private string sQ = "";
        private string sO = "";
        private string sC = "";

        GetNameFromKey = Nothing

        if (sKey Is null)
        {
            ErrMsg("Bad Key");
            return null;
        }

        if (bQuoted)
        {
            sQ = "'"
        Else
            sO = "[ "
            sC = " ]"
        }


        if (sKey.StartsWith("Referenced"))
        {
            switch (sLeft(sKey, 16))
            {
                case "ReferencedCharac":
                    {
                    switch (sKey)
                    {
                        case "ReferencedCharacters":
                            {
                            return sO + sKey.Replace("ReferencedCharacters", "Referenced Characters") + sC;
                        case "ReferencedCharacter":
                            {
                            return sO + sKey.Replace("ReferencedCharacter", "Referenced Character") + sC;
                        default:
                            {
                            return sO + sKey.Replace("ReferencedCharacter", "Referenced Character ") + sC;
                    }
                case "ReferencedDirect":
                    {
                    switch (sKey)
                    {
                        case "ReferencedDirections":
                            {
                            return sO + sKey.Replace("ReferencedDirections", "Referenced Directions") + sC;
                        case "ReferencedDirection":
                            {
                            return sO + sKey.Replace("ReferencedDirection", "Referenced Direction") + sC;
                        default:
                            {
                            return sO + sKey.Replace("ReferencedDirection", "Referenced Direction ") + sC;
                    }
                case "ReferencedObject":
                    {
                    switch (sKey)
                    {
                        case "ReferencedObjects":
                            {
                            return sO + sKey.Replace("ReferencedObjects", "Referenced Objects") + sC;
                        case "ReferencedObject":
                            {
                            return sO + sKey.Replace("ReferencedObject", "Referenced Object") + sC;
                        default:
                            {
                            return sO + sKey.Replace("ReferencedObject", "Referenced Object ") + sC;
                    }
                case "ReferencedNumber":
                    {
                    switch (sKey)
                    {
                        case "ReferencedNumbers":
                            {
                            return sO + sKey.Replace("ReferencedNumbers", "Referenced Numbers") + sC;
                        case "ReferencedNumber":
                            {
                            return sO + sKey.Replace("ReferencedNumber", "Referenced Number") + sC;
                        default:
                            {
                            return sO + sKey.Replace("ReferencedNumber", "Referenced Number ") + sC;
                    }
                case "ReferencedText":
                    {
                    switch (sKey)
                    {
                        case "ReferencedText":
                            {
                            return sO + sKey.Replace("ReferencedText", "Referenced Text") + sC;
                        default:
                            {
                            return sO + sKey.Replace("ReferencedText", "Referenced Text ") + sC;
                    }
                case "ReferencedLocati":
                    {
                    switch (sKey)
                    {
                        case "ReferencedLocation":
                            {
                            return sO + sKey.Replace("ReferencedLocation", "Referenced Location") + sC;
                    }
                case "ReferencedItem":
                    {
                    return sO + sKey.Replace("ReferencedItem", "Referenced Item") + sC;
            }
        ElseIf sKey.StartsWith("Parameter-") Then
            return sO + sKey.Replace("Parameter-", "") + sC;
        }
        If sKey = ANYOBJECT Then Return sO + "Any Object" + sC
        If sKey = ANYCHARACTER Then Return sO + "Any Character" + sC
        If sKey = NOOBJECT Then Return sO + "No Object" + sC
        If sKey = THEFLOOR Then Return sO + IIf(bPCase, "The Floor", "the Floor").ToString + sC
        'If sKey = THEPLAYER Then Return sO & IIf(bPCase, "The", "the").ToString & " Player Character" & sC
        If sKey = THEPLAYER Then Return IIf(bPCase, sO + "The Player Character" + sC, "the Player character").ToString
        If sKey = CHARACTERPROPERNAME Then Return IIf(bPrefixItem, IIf(bPCase, "Property ", "property "), "").ToString + sQ + "Name" + sQ
        If sKey = PLAYERLOCATION Then Return IIf(bPCase, sO + "The Player's Location" + sC, "the Player's location").ToString

        If htblLocations.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Location ", "location "), "").ToString + sQ + htblLocations(sKey).ShortDescription.ToString + sQ
        If htblObjects.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Object ", "object "), "").ToString + sQ + htblObjects(sKey).FullName + sQ
        If htblTasks.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Task ", "task "), "").ToString + sQ + htblTasks(sKey).Description + sQ
        If htblEvents.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Event ", "event "), "").ToString + sQ + htblEvents(sKey).Description + sQ
        If htblCharacters.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Character ", "character "), "").ToString + sQ + htblCharacters(sKey).Name(, false, false) + sQ
        If htblGroups.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Group ", "group "), "").ToString + sQ + htblGroups(sKey).Name + sQ
        If htblVariables.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Variable ", "variable "), "").ToString + sQ + htblVariables(sKey).Name + sQ
        If htblALRs.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Text Override ", "text override "), "").ToString + sQ + htblALRs(sKey).OldText + sQ
        If htblHints.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Hint ", "hint "), "").ToString + sQ + htblHints(sKey).Question + sQ
        If htblAllProperties.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Property ", "property "), "").ToString + sQ + htblAllProperties(sKey).Description + sQ
        If htblUDFs.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "User Function ", "user function "), "").ToString + sQ + htblUDFs(sKey).Name + sQ
        If htblSynonyms.ContainsKey(sKey) Then Return IIf(bPrefixItem, IIf(bPCase, "Synonym ", "synonym "), "").ToString + sQ + htblSynonyms(sKey).CommonName + sQ

        return sKey;

    }

#End Region


public enum TasksListEnum
    {
        AllTasks;
        GeneralTasks;
        GeneralAndOverrideableSpecificTasks;
        SpecificTasks;
        SystemTasks;
    }
    ' Could probably speed things up by making these permanent, defining on Adventure load, for Runner anyway
    internal TaskHashTable Tasks(TasksListEnum eTasksList)
    {

        private New TaskHashTable htblSubTasks;

        switch (eTasksList)
        {
            case TasksListEnum.AllTasks:
                {
                htblSubTasks = htblTasks
            case TasksListEnum.GeneralTasks:
                {
                For Each Task As clsTask In htblTasks.Values
                    If Task.TaskType = clsTask.TaskTypeEnum.General Then htblSubTasks.Add(Task, Task.Key)
                Next;
            case TasksListEnum.GeneralAndOverrideableSpecificTasks:
                {
                For Each Task As clsTask In htblTasks.Values
                    If Task.TaskType = clsTask.TaskTypeEnum.General Then htblSubTasks.Add(Task, Task.Key)
                    ' A specific task is overrideable if any of the specifics are unspecified
                    if (Task.TaskType = clsTask.TaskTypeEnum.Specific)
                    {
                        if (Task.Specifics IsNot null)
                        {
                            For Each s As clsTask.Specific In Task.Specifics
                                if (s.Keys.Count = 1)
                                {
                                    if (s.Keys(0) = "")
                                    {
                                        htblSubTasks.Add(Task, Task.Key);
                                        Exit For;
                                    }
                                }
                            Next;
                        }
                    }
                Next;
            case TasksListEnum.SpecificTasks:
                {
                For Each Task As clsTask In htblTasks.Values
                    If Task.TaskType = clsTask.TaskTypeEnum.Specific Then htblSubTasks.Add(Task, Task.Key)
                Next;
            case TasksListEnum.SystemTasks:
                {
                For Each Task As clsTask In htblTasks.Values
                    If Task.TaskType = clsTask.TaskTypeEnum.System Then htblSubTasks.Add(Task, Task.Key)
                Next;
        }

        return htblSubTasks;

    }


    internal ObjectHashTable Objects(string sPropertyKey, string sPropertyValue = "")
    {

        private New ObjectHashTable htblSubObjects;

        For Each ob As clsObject In Adventure.htblObjects.Values
            if (ob.HasProperty(sPropertyKey))
            {
                if (sPropertyValue = "" || ob.GetPropertyValue(sPropertyKey) = sPropertyValue)
                {
                    htblSubObjects.Add(ob, ob.Key);
                }
            }
        Next;

        return htblSubObjects;

    }


    internal Description Introduction { get; set; }
        {
            get
            {
            return oIntroduction;
        }
set(ByVal Value As Description)
            oIntroduction = Value
        }
    }

    internal Description WinningText { get; set; }
        {
            get
            {
            return oWinningText;
        }
set(ByVal Value As Description)
            oWinningText = Value
        }
    }

    public string Title { get; set; }
        {
            get
            {
            return sTitle;
        }
set(ByVal Value As String)
            sTitle = Value
        }
    }

    public string Author { get; set; }
        {
            get
            {
            return sAuthor;
        }
set(ByVal Value As String)
            sAuthor = Value
        }
    }

    private string sDefaultFontName = "Arial";
    public string DefaultFontName
        {
            get
            {
            return sDefaultFontName;
        }
set(String)
            If value <> "" Then sDefaultFontName = value
        }
    }


    public Color = Color.FromArgb(DEFAULT_BACKGROUNDCOLOUR) DeveloperDefaultBackgroundColour { get; set; }
    public Color = Color.FromArgb(DEFAULT_INPUTCOLOUR) ' 210, 37, 39) DeveloperDefaultInputColour { get; set; }
    public Color = Color.FromArgb(DEFAULT_OUTPUTCOLOUR) ' 25, 165, 138) DeveloperDefaultOutputColour { get; set; }
    public Color = Color.FromArgb(DEFAULT_LINKCOLOUR) ' 75, 215, 188) DeveloperDefaultLinkColour { get; set; }

    private int iDefaultFontSize = 12;
    public int DefaultFontSize
        {
            get
            {
            return iDefaultFontSize;
        }
set(Integer)
            If value >= 8 && value <= 36 Then iDefaultFontSize = value
        }
    }

    private Font oDefaultFont = null;
    Public ReadOnly Property DefaultFont As Font
        {
            get
            {
            if (oDefaultFont Is null)
            {
                oDefaultFont = New Font(DefaultFontName, DefaultFontSize, GraphicsUnit.Point)
            }
            return oDefaultFont;
        }
    }

    private int _iWaitTurns = 3;
    public int WaitTurns
        {
            get
            {
            return _iWaitTurns;
        }
set(Integer)
            _iWaitTurns = Math.Max(value, 0)
        }
    }

    public string NotUnderstood { get; set; }
        {
            get
            {
            return sNotUnderstood;
        }
set(ByVal Value As String)
            sNotUnderstood = Value
        }
    }

    public string Filename { get; set; }
        {
            get
            {
            return sFilename;
        }
set(ByVal Value As String)
            sFilename = Value
        }
    }

    public string FullPath { get; set; }
        {
            get
            {
            return sFullPath;
        }
set(ByVal Value As String)
            sFullPath = Value
        }
    }

    private bool bShowFirstRoom = true;
    public bool ShowFirstRoom { get; set; }
        {
            get
            {
            return bShowFirstRoom;
        }
set(ByVal Boolean)
            bShowFirstRoom = value
        }
    }

    private bool bShowExits = true;
    public bool ShowExits { get; set; }
        {
            get
            {
            return bShowExits;
        }
set(ByVal Boolean)
            bShowExits = value
        }
    }

    private bool bEnableMenu = true;
    public bool EnableMenu
        {
            get
            {
            return bEnableMenu;
        }
set(Boolean)
            bEnableMenu = value
        }
    }

    private bool bEnableDebugger = true;
    public bool EnableDebugger
        {
            get
            {
            return bEnableDebugger;
        }
set(Boolean)
            bEnableDebugger = value
        }
    }

    public void New()
    {

        Title = "Untitled"
        Author = "Anonymous"
        sFilename = "untitled.taf"
        oDefaultFont = Nothing
        sGameFilename = ""
#if Runner
        With UserSession;
            .sIt = "";
            .sThem = "";
            .sHim = "";
            .sHer = "";
            .ClearAutoCompletes();
        }
#endif
        for (int i = 0; i <= 11; i++)
        {
            Enabled((EnabledOptionEnum)(i)) = true;
        Next;
        Turns = 0
        dictAllItems = New ItemDictionary
        htblLocations = New LocationHashTable
        htblObjects = New ObjectHashTable
        htblTasks = New TaskHashTable
        htblEvents = New EventHashTable
        htblCharacters = New CharacterHashTable
        htblGroups = New GroupHashTable
        htblVariables = New VariableHashTable
        htblALRs = New ALRHashTable
        htblHints = New HintHashTable
        htblUDFs = New UDFHashTable
        htblAllProperties = New PropertyHashTable
        htblObjectProperties = New PropertyHashTable
        htblCharacterProperties = New PropertyHashTable
        htblLocationProperties = New PropertyHashTable
        htblSynonyms = New SynonymHashTable
        Introduction = New Description
        WinningText = New Description
        '#If Not www Then
        Map = New clsMap
        '#End If

        sDirectionsRE(DirectionsEnum.North) = "North/N";
        sDirectionsRE(DirectionsEnum.NorthEast) = "NorthEast/NE/North-East/N-E";
        sDirectionsRE(DirectionsEnum.East) = "East/E";
        sDirectionsRE(DirectionsEnum.SouthEast) = "SouthEast/SE/South-East/S-E";
        sDirectionsRE(DirectionsEnum.South) = "South/S";
        sDirectionsRE(DirectionsEnum.SouthWest) = "SouthWest/SW/South-West/S-W";
        sDirectionsRE(DirectionsEnum.West) = "West/W";
        sDirectionsRE(DirectionsEnum.NorthWest) = "NorthWest/NW/North-West/N-W";
        sDirectionsRE(DirectionsEnum.In) = "In/Inside";
        sDirectionsRE(DirectionsEnum.Out) = "Out/O/Outside";
        sDirectionsRE(DirectionsEnum.Up) = "Up/U";
        sDirectionsRE(DirectionsEnum.Down) = "Down/D";

#if Generator
        fGenerator.Map1.Map = Map;
        dictFolders = New FolderDictionary
        'For Each pane As Infragistics.Win.UltraWinDock.DockAreaPane In fGenerator.UDMGenerator.DockAreas
        '    pane.Close()
        'Next
        'For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In fGenerator.UDMGenerator.ControlPanes
        '    cp.Close()
        'Next
        For Each child As frmFolder In fGenerator.MDIFolders
            child.Close();
        Next;
#endif

        For Each eGen As clsCharacter.GenderEnum In New clsCharacter.GenderEnum() {clsCharacter.GenderEnum.Male, clsCharacter.GenderEnum.Female, clsCharacter.GenderEnum.Unknown}
            lCharMentionedThisTurn.Add(eGen, New Generic.List(Of String));
        Next;

    }

internal enum EnabledOptionEnum
    {
        ShowExits;
        EightPointCompass;
        BattleSystem;
        Sound;
        Graphics;
        UserStatusBox;
        Score;
        ControlPanel;
        Debugger;
        Map;
        AutoComplete;
        MouseClicks;
    }
    'Friend Function Enabled(ByVal eOption As EnabledOptionEnum) As Boolean


    'End Function
    Private bEnabled(11) As Boolean
    Friend Property Enabled(ByVal eOption As EnabledOptionEnum) As Boolean
        {
            get
            {
            return bEnabled(eOption);
        }
set(ByVal Value As Boolean)
            bEnabled(eOption) = Value;
        }
    }





    internal List<string> AllKeys { get; }
        {
            get
            {
            private New List<string> salKeys;

            For Each sKey As String In Me.htblALRs.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblCharacters.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblEvents.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblGroups.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblHints.Keys
                salKeys.Add(sKey);
            Next;
            For Each skey As String In Me.htblLocations.Keys
                salKeys.Add(skey);
            Next;
            For Each skey As String In Me.htblObjects.Keys
                salKeys.Add(skey);
            Next;
            For Each sKey As String In Me.htblAllProperties.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblTasks.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblVariables.Keys
                salKeys.Add(sKey);
            Next;
            For Each sKey As String In Me.htblUDFs.Keys
                salKeys.Add(sKey);
            Next;
#if Generator
            For Each sKey As String In dictFolders.Keys
                salKeys.Add(sKey);
            Next;
#endif

            return salKeys;

        }
    }

}

}