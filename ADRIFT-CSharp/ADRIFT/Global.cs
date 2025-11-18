using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
internal static class SharedModule
{

#if www
    public clsAdventure Adventure
        {
            get
            {
            return CType(HttpContext.Current.Session.Item("Adventure"), clsAdventure);
        }
set(clsAdventure)
            HttpContext.Current.Session.Item("Adventure") = value;
        }
    }
#else
    public clsAdventure Adventure { get; set; }
#endif

    public clsBlorb Blorb;
    public int iLoading;
    'Public bGenerator As Boolean
    public double dVersion = 5.0;
    public const string ANYOBJECT = "AnyObject";
    public const string ANYCHARACTER = "AnyCharacter";
    public const string ANYDIRECTION = "AnyDirection";
    public const string NOOBJECT = "NoObject";
    public const string NOCHARACTER = "NoCharacter";
    'Public Const ALLHELDOBJECTS As String = "AllHeldObjects"
    'Public Const ALLWORNOBJECTS As String = "AllWornObjects"
    public const string ALLROOMS = "AllLocations";
    public const string NOROOMS = "NoLocations";
    public const string THEFLOOR = "TheFloor";
    public const string THEPLAYER = "%Player%";
    public const string CHARACTERPROPERNAME = "CharacterProperName";
    public const string PLAYERLOCATION = "PlayerLocation";
    public const string HIDDEN = "Hidden";

    public const int DEFAULT_BACKGROUNDCOLOUR = -16777216 ' Color.Black.ToArgb;
    public const int DEFAULT_INPUTCOLOUR = -3005145 ' = Color.FromArgb(210, 37, 39);
    public const int DEFAULT_OUTPUTCOLOUR = -15096438 ' Color = Color.FromArgb(25, 165, 138);
    public const int DEFAULT_LINKCOLOUR = -11806788 ' = Color.FromArgb(75, 215, 188);

    ' Mandatory properties
    public const string SHORTLOCATIONDESCRIPTION = "ShortLocationDescription";
    public const string LONGLOCATIONDESCRIPTION = "LongLocationDescription";
    public const string OBJECTARTICLE = "_ObjectArticle";
    public const string OBJECTPREFIX = "_ObjectPrefix";
    public const string OBJECTNOUN = "_ObjectNoun";


    public const string sLOCATION = "Location";
    public const string sOBJECT = "Object";
    public const string sTASK = "Task";
    public const string sEVENT = "Event";
    public const string sCHARACTER = "Character";
    public const string sGROUP = "Group";
    public const string sVARIABLE = "Variable";
    public const string sPROPERTY = "Property";
    public const string sHINT = "Hint";
    public const string sALR = "Text Override";
    public const string sGENERAL = "General";
    public const string sSELECTED = "<Selected>";
    public const string sUNSELECTED = "<Unselected>";

    'Public sLocalDataPath As String
    'Public colInput As Color = Color.FromArgb(210, 37, 39)
    'Public colOutput As Color = Color.FromArgb(25, 165, 138)
    'Public colLink As Color = Color.FromArgb(75, 215, 188)

    public Integer ' SizeModeEnum iImageSizeMode = generator.c SizeModeEnum.ActualSizeCentred;

    'Private Declare Auto Function AddFontMemResourceEx Lib "Gdi32.dll" (ByVal pbFont As IntPtr, ByVal cbFont As Integer, ByVal pdv As Integer, ByRef pcFonts As Integer) As IntPtr
    internal System.Drawing.Text.PrivateFontCollection pfc = null;

public enum PerspectiveEnum
    {
        None = 0
        FirstPerson = 1     ' I/Me/Myself
        SecondPerson = 2    ' You/Yourself
        ThirdPerson = 3     ' He/She/Him/Her
        ' It
        ' We
        ' They
    }


public enum ReferencesType
    {
        [Object];
        Character;
        Number;
        Text;
        Direction;
        Location;
        Item;
    }


internal enum ArticleTypeEnum
    {
        Definite ' The;
        Indefinite ' A;
        None;
    }

#Region "Microsoft.VisualBasic compatability"

    public int Asc(char c)
    {
        return Convert.ToInt32(c);
    }
    public int Asc(string s)
    {
        'If System.Text.Encoding.Default.GetBytes(s(0))(0) <> Microsoft.VisualBasic.Asc(s) Then
        '    Return Microsoft.VisualBasic.Asc(s)
        'Else
        return System.Text.Encoding.Default.GetBytes(s(0))(0) 'Convert.ToInt32(CChar(s));
        'End If
    }

#if False
public enum MsgBoxStyle
    {
        OkOnly = 0
        OkCancel = 1
        YesNoCancel = 3
        YesNo = 4
        Question = 32
        Exclamation = 48
    }
public enum MsgBoxResult
    {
        Ok = 1
        Cancel = 2
        Abort = 3
        Yes = 6
        No = 7
    }

    public char Chr(int i)
    {
        return Convert.ToChar(i);
    }

    public char ChrW(int i)
    {
        return Convert.ToChar(i);
    }

    public string Dir()
    {
        TODO();
    }

    public long FileLen(string PathName)
    {
        TODO();
    }

    public string Format(object Expression, string Stye = "")
    {
        TODO();
    }

    public string GetSetting(string AppName, string Section, string Key, [Default] As String = "")
    {
        TODO();
    }

    public string Hex(int Number)
    {
        TODO();
    }

    public object IIf(bool bTest, object oTrue, object oFalse)
    {
        if (bTest)
        {
            return oTrue;
        Else
            return oFalse;
        }
    }

    public string InputBox(string Prompt, string Title = "", string DefaultResponse = "")
    {
        TODO();
    }

public enum CompareMethod
    {
        Binary = 0
        Text = 1
    }
    public int Instr(string sText, string sSearchFor, CompareMethod Compare = CompareMethod.Binary)
    {
        TODO() 'Return sInstr(sText, sSearchFor);
    }

    public int Int(object Number)
    {
        TODO();
    }

    public bool IsDate(object Expression)
    {
        TODO();
    }

    public bool IsDBNull(object Expression)
    {
        TODO();
    }

    public bool IsNumeric(object Expression)
    {
        TODO();
    }

    public string LCase(string s)
    {
        return s.ToLower;
    }

    public string Left(string sString, int iLength)
    {
        return sLeft(sString, iLength);
    }

    public int Len(string sString)
    {
        If sString == null Then Return 0
        return sString.Length;
    }

    public string Mid(string sString, int iStart)
    {
        return sMid(sString, iStart);
    }

    public string Mid(string sString, int iStart, int iLength)
    {
        return sMid(sString, iStart, iLength);
    }

    public DialogResult MsgBox(string Prompt)
    {
        return MessageBox.Show(Prompt, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    public MsgBoxResult MsgBox(string Prompt, MsgBoxStyle Buttons)
    {
        TODO() ' Return MessageBox.Show(Prompt, Application.ProductName, MessageBoxButtons.OK);
    }
    public MsgBoxResult MsgBox(string Prompt, MsgBoxStyle Buttons, string Title)
    {
        TODO() ' Return MessageBox.Show(Prompt, Title, MessageBoxButtons.OK);
    }

    public DateTime Now()
    {
        return Date.Now;
    }

    public void Randomize(double Number)
    {
        TODO();
    }
    public string Replace(string Expression, string Find, string Replacement, int Start = 1, int Count = -1)
    {
        TODO();
    }
    public string Right(string sString, int iLength)
    {
        return sRight(sString, iLength);
    }

    public int Rnd()
    {
        TODO();
    }
    public float Rnd(float Number)
    {
        TODO();
    }

    public void SaveSetting(string AppName, string Section, string Key, string Setting)
    {
        TODO();
    }

    public string Space(int Number)
    {
        TODO();
    }

    public string[] Split(string Expression, string Delimiter = "", int Limit = -1)
    {
        TODO();
    }

    public string Str(object Number)
    {
        TODO();
    }

    public string UCase(string s)
    {
        return s.ToUpper;
    }

    public double Val(object Expression)
    {

    }

    Public ReadOnly Property vbCrLf As String
        {
            get
            {
            return Environment.NewLine;
        }
    }

    Public ReadOnly Property vbLf As Char
        {
            get
            {
            return Convert.ToChar(10);
        }
    }

#endif
#End Region

#if Not (Mono Or www)
    public bool DarkInterface()
    {

        switch (eStyle)
        {
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007:
                {
                return eColour2007 = Infragistics.Win.Office2007ColorScheme.Black;
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010:
                {
                return false ' All light;
            case Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013:
                {
                return false ' All light;
        }
        return false;

    }
#endif


    Public ReadOnly Property BinPath(Optional ByVal bSeparatorCharacter As Boolean = False) As String
        {
            get
            {
#if www
            return IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data") + IO.Path.DirectorySeparatorChar;
#else
            return Application.StartupPath + If(bSeparatorCharacter, IO.Path.DirectorySeparatorChar, "").ToString;
            'Dim sPath As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & IO.Path.DirectorySeparatorChar & "ADRIFT"
            'If Not IO.Directory.Exists(sPath) Then IO.Directory.CreateDirectory(sPath)
            'Return sPath ' & IO.Path.DirectorySeparatorChar
#endif
        }
    }
    Public ReadOnly Property DataPath(Optional ByVal bSeparatorCharacter As Boolean = False) As String
        {
            get
            {
#if www
            return IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data") + IO.Path.DirectorySeparatorChar;
#else
            'Return Application.StartupPath & IO.Path.DirectorySeparatorChar
            private string sPath;
            if (Application.LocalUserAppDataPath IsNot null)
            {
                sPath = Application.LocalUserAppDataPath.Substring(0, Application.LocalUserAppDataPath.IndexOf("\ADRIFT")) & "\ADRIFT" '& IO.Path.DirectorySeparatorChar
            Else
                sPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments & IO.Path.DirectorySeparatorChar & "ADRIFT"
            }

            If ! IO.Directory.Exists(sPath) Then IO.Directory.CreateDirectory(sPath)
            return sPath + If(bSeparatorCharacter, IO.Path.DirectorySeparatorChar, "").ToString;
#endif
        }
    }


    public void TidyUp()
    {
        TidyUpSounds();
    }


    private New List<string> sWarnings;
    public void WarnOnce(string sMessage)
    {

        if (Not sWarnings.Contains(sMessage))
        {
            ErrMsg(sMessage);
            sWarnings.Add(sMessage);
        }

    }


    public Drawing.Text.PrivateFontCollection GetFontCollection()
    {

        private Drawing.Text.PrivateFontCollection _pfc = null;
        'Public Function GetFont() As Drawing.Text.PrivateFontCollection

        'Dim pfc As New System.Drawing.Text.PrivateFontCollection

        try
        {
            if (_pfc Is null)
            {
                private string sFont = Application.StartupPath + IO.Path.DirectorySeparatorChar + "LYDIAN.TTF";

                _pfc = New Drawing.Text.PrivateFontCollection

                if (IO.File.Exists(sFont) && false)
                {
                    _pfc.AddFontFile(sFont);
                Else

                    private byte[] bytLydian = My.Resources.Resources.LYDIAN;
                    private Runtime.InteropServices.GCHandle gch = Runtime.InteropServices.GCHandle.Alloc(bytLydian, Runtime.InteropServices.GCHandleType.Pinned);
                    _pfc.AddMemoryFont(gch.AddrOfPinnedObject, bytLydian.Length);
                    gch.Free();

                    'Dim FntPtr As IntPtr = Runtime.InteropServices.Marshal.AllocHGlobal(Runtime.InteropServices.Marshal.SizeOf(GetType(Byte)) * bytLydian.Length) ' ByteStrm.Length)
                    'Dim ptrMem As IntPtr = Runtime.InteropServices.Marshal.AllocCoTaskMem(bytLydian.Length)

                    ''Copy the byte array holding the font into the allocated memory.
                    'Runtime.InteropServices.Marshal.Copy(bytLydian, 0, ptrMem, bytLydian.Length)

                    ''Add the font to the PrivateFontCollection
                    ''FntFC.AddMemoryFont(FntPtr, bytLydian.Length)
                    '_pfc.AddMemoryFont(ptrMem, bytLydian.Length)

                    'Dim pcFonts As Int32
                    'pcFonts = 1
                    'AddFontMemResourceEx(ptrMem, bytLydian.Length, 0, pcFonts)

                    ''Free the memory
                    ''Runtime.InteropServices.Marshal.FreeHGlobal(FntPtr)
                    'Runtime.InteropServices.Marshal.FreeCoTaskMem(ptrMem)
                    'Next
                }
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error extracting font", ex);
        }

        return _pfc;

    }

    'Public Function GetFont() As Drawing.Text.PrivateFontCollection
    '    'Get the namespace of the application
    '    'Dim NameSpc As String = Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString()
    '    'Dim FntStrm As IO.Stream
    '    Dim FntFC As New Drawing.Text.PrivateFontCollection()
    '    Try
    '        'Dim i As Integer
    '        'For i = 0 To FontResource.GetUpperBound(0)
    '        'Get the resource stream area where the font is located
    '        'FntStrm = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ADRIFT." + FontResource(i))
    '        'Load the font off the stream into a byte array
    '        'Dim ByteStrm(CType(FntStrm.Length, Integer)) As Byte
    '        'FntStrm.Read(ByteStrm, 0, Int(CType(FntStrm.Length, Integer)))
    '        'Allocate some memory on the global heap
    '        Dim bytLydian As Byte() = My.Resources.Resources.Lydian
    '        Dim FntPtr As IntPtr = Runtime.InteropServices.Marshal.AllocHGlobal(Runtime.InteropServices.Marshal.SizeOf(GetType(Byte)) * bytLydian.Length) ' ByteStrm.Length)
    '        'Copy the byte array holding the font into the allocated memory.
    '        Runtime.InteropServices.Marshal.Copy(bytLydian, 0, FntPtr, bytLydian.Length)
    '        'Add the font to the PrivateFontCollection
    '        FntFC.AddMemoryFont(FntPtr, bytLydian.Length)
    '        Dim pcFonts As Int32
    '        pcFonts = 1
    '        '            AddFontMemResourceEx(FntPtr, bytLydian.Length, 0, pcFonts)
    '        'Free the memory
    '        Runtime.InteropServices.Marshal.FreeHGlobal(FntPtr)
    '        'Next
    '    Catch ex As Exception
    '        ErrMsg("Error extracting font", ex)
    '    End Try
    '    Return FntFC
    'End Function


    'Public Sub Rnd(ByVal a As String)
    '    ' Don't use this...
    'End Sub
    private Random r;
    public int Random(int iMax)
    {
        If r == null Then r = New Random(GetNextSeed)
        'Dim r As New Random(GetNextSeed)
        return r.Next(iMax + 1);
    }
    public int Random(int iMin, int iMax)
    {
        If r == null Then r = New Random(GetNextSeed)
        'Dim r As New Random(GetNextSeed)
        if (iMax < iMin)
        {
            private int i = iMax;
            iMax = iMin
            iMin = i
        }
        return r.Next(iMin, iMax + 1);
    }

    private int GetNextSeed()
    {
        Static iLastSeed As Integer = 0;

        private int iNextSeed = CInt(Now.Ticks % Integer.MaxValue);
        while (iNextSeed = iLastSeed)
        {
            iNextSeed -= Now.Millisecond;
        }
        iLastSeed = iNextSeed

        return iNextSeed;

    }


public enum DirectionsEnum
    {
        North = 0
        East = 1
        South = 2
        West = 3
        Up = 4
        Down = 5
        [In] = 6;
        Out = 7
        NorthEast = 8
        SouthEast = 9
        SouthWest = 10
        NorthWest = 11
    }


public enum ItemEnum
    {
        Location;
        [Object];
        Task;
        [Event];
        Character;
        Group;
        Variable;
        [Property];
        Hint;
        ALR;
        General;
    }


    public void GlobalStartup()
    {
        'If Not Application.LocalUserAppDataPath Is Nothing Then
        '    sLocalDataPath = Application.LocalUserAppDataPath.Substring(0, Application.LocalUserAppDataPath.IndexOf("ADRIFT")) & "ADRIFT" & IO.Path.DirectorySeparatorChar
        'End If

#if www
        private Version version = system.Reflection.Assembly.GetCallingAssembly.GetName.Version;
#else
        private Version version = new Version(Application.ProductVersion);
#endif
        dVersion = CDbl(version.Major & System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & Format(version.Minor, "000") & Format(version.Build, "000") & Format(version.Revision))
    }


    internal bool IsHex(string sHex)
    {
        for (int i = 0; i <= sHex.Length - 1; i++)
        {
            If "0123456789ABCDEF".IndexOf(sHex(i)) = -1 Then Return false
        Next;
        return true;
    }


    public string ItemToString(ItemEnum item, bool bPlural = false)
    {

        ItemToString = ""

        switch (item)
        {
            case ItemEnum.Location:
                {
                ItemToString = sLOCATION
            case ItemEnum.Object:
                {
                ItemToString = sOBJECT
            case ItemEnum.Task:
                {
                ItemToString = sTASK
            case ItemEnum.Event:
                {
                ItemToString = sEVENT
            case ItemEnum.Character:
                {
                ItemToString = sCHARACTER
            case ItemEnum.Group:
                {
                ItemToString = sGROUP
            case ItemEnum.Variable:
                {
                ItemToString = sVARIABLE
            case ItemEnum.Property:
                {
                ItemToString = sPROPERTY
            case ItemEnum.Hint:
                {
                ItemToString = sHINT
            case ItemEnum.ALR:
                {
                ItemToString = sALR
            case ItemEnum.General:
                {
                ItemToString = sGENERAL
        }

        if (bPlural)
        {
            switch (item)
            {
                case ItemEnum.Location:
                case ItemEnum.Object:
                case ItemEnum.Task:
                case ItemEnum.Event:
                case ItemEnum.Character:
                case ItemEnum.Group:
                case ItemEnum.Variable:
                case ItemEnum.Hint:
                case ItemEnum.ALR:
                case ItemEnum.General:
                    {
                    return ItemToString + "s";
                case ItemEnum.Property:
                    {
                    return "Properties";
            }
        }

    }


    Private WithEvents WebClient As New System.Net.WebClient
    public void CheckForUpdate(bool bAtStart = true)
    {

        ' Try to download from https://www.adrift.co/cgi/versioncheck.cgi?from=5.0.26.1&atstart=0

        ' <?xml>
        '   <versioncheck>
        '     <currentversion>5.0.26.1</currentversion>
        '     <change type="enhancement" item="1234" title="Blah">Blah blah</change>
        '     <change type="bug" item="2345" title="Oops">La de dah</change>
        '   </versioncheck>

#if DEBUG
        Exit Sub;
#endif

        'Dim URI As New System.Uri("https://www.adrift.co/cgi/versioncheck.cgi?from=" & Application.ProductVersion & If(bAtStart, "", "&atstart=0").ToString)
        'Dim URI As New System.Uri("https://www.adrift.co/cgi/versioncheck.cgi?from=5.0.0.0" & If(bAtStart, "", "&atstart=0").ToString)

        WebClient.DownloadStringAsync(New System.Uri("https://www.adrift.co/cgi/versioncheck.cgi?from=" + Application.ProductVersion + If(bAtStart, "", "&atstart=0").ToString));

    }

#if Not www
    private void WebClient_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
    {

        try
        {
            If e.Error != null Then Exit Sub

            private New Xml.XmlDocument xmlVersion;
            private string sXML = e.Result.Replace("&eacute;", "E").Replace("&agrave;", "A").Replace("&aelig;", "ae").Replace("&raquo;", "&gt;");
            xmlVersion.LoadXml(sXML);
            private string sOldVersion = Application.ProductVersion;
            private string sNewVersion = "";

            With xmlVersion.Item("versioncheck");
                private bool bUpdate = false;

                if (.Item("currentversion") IsNot null)
                {
                    sNewVersion = .Item("currentversion").InnerText
                    if (sOldVersion <> sNewVersion)
                    {
                        Dim OldVersion() As String = sOldVersion.Split("."c)
                        Dim NewVersion() As String = sNewVersion.Split("."c)

                        for (int i = 0; i <= 3; i++)
                        {
                            private int iOld = 0;
                            private int iNew = 0;
                            If OldVersion.Length > i Then iOld = SafeInt(OldVersion(i))
                            If NewVersion.Length > i Then iNew = SafeInt(NewVersion(i))

                            if (iOld < iNew)
                            {
                                bUpdate = True
                                Exit For;
                            ElseIf iOld > iNew Then
                                Exit Sub ' We are apparently newer than the published version;
                            }
                        Next;
                    }
                }

                If ! bUpdate Then Exit Sub

#if Generator
                private New frmYesNoCancel ync;
                ync.Text = "ADRIFT Update";
                ync.lblText.Text = "There is a new version of ADRIFT available.  Would you like to download this now?";
                ync.btnCancel.Text = "Details";
                ync.CancelButton = ync.btnNo;
                private string sDialogResult = GetSetting("ADRIFT", "Shared", "DownloadUpdates", "");
                If sDialogResult = "" Then sDialogResult = CInt(ync.ShowDialog()).ToString

                switch (CType(sDialogResult, DialogResult))
                {
                    case DialogResult.Yes:
                        {
                        DownloadUpdate()
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        {
                        ' Do nothing
                    case DialogResult.Ignore ' About:
                        {
                        private string sEnhancements = "";
                        private string sBugs = "";
                        For Each nodChange As Xml.XmlElement In xmlVersion.SelectNodes("/versioncheck/change")
                            private int iItem = 0;
                            If nodChange.HasAttribute("item") Then iItem = SafeInt(nodChange.GetAttribute("item"))
                            private string sTitle = "";
                            If nodChange.HasAttribute("title") Then sTitle = nodChange.GetAttribute("title")
                            private string sChange = sTitle + " [#" + iItem + "]";
                            if (nodChange.HasAttribute("type"))
                            {
                                switch (nodChange.GetAttribute("type"))
                                {
                                    case "enhancement":
                                        {
                                        sEnhancements &= sChange + vbCrLf;
                                    case "bug":
                                        {
                                        sBugs &= sChange + vbCrLf;
                                }
                            }
                        Next;

                        private New WhatsNew frmWhatsNew;
                        With frmWhatsNew;
                            .txtWhatsNew.SelectionFont = New Font("Arial", 12, FontStyle.Bold);
                            .txtWhatsNew.SelectedText = "Changes between " + sOldVersion + " and " + sNewVersion + vbCrLf + vbCrLf;
                            if (sEnhancements <> "")
                            {
                                .txtWhatsNew.SelectionFont = New Font("Arial", 14, FontStyle.Bold);
                                .txtWhatsNew.SelectedText = "Enhancements" + vbCrLf;
                                .txtWhatsNew.SelectionBullet = true;
                                .txtWhatsNew.SelectionFont = New Font("Arial", 10, FontStyle.Regular);
                                .txtWhatsNew.SelectedText = sEnhancements;
                                .txtWhatsNew.SelectionBullet = false;
                            }
                            if (sBugs <> "")
                            {
                                .txtWhatsNew.SelectionFont = New Font("Arial", 14, FontStyle.Bold);
                                .txtWhatsNew.SelectedText = vbCrLf + "Bug Fixes" + vbCrLf;
                                .txtWhatsNew.SelectionBullet = true;
                                .txtWhatsNew.SelectionFont = New Font("Arial", 10, FontStyle.Regular);
                                .txtWhatsNew.SelectedText = sBugs;
                                .txtWhatsNew.SelectionBullet = false;
                            }
                            .txtWhatsNew.SelectionStart = 0;

                            If .ShowDialog = DialogResult.OK Then DownloadUpdate()
                        }

                }

                If ync.chkRemember.Checked Then SaveSetting("ADRIFT", "Shared", "DownloadUpdates", sDialogResult)
#endif

            }
        }
        catch (Exception ex)
        {
            ' Hmm, just ignore
        }

    }
#endif

    private void DownloadUpdate()
    {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start("https://www.adrift.co/download");
        Cursor.Current = Cursors.Arrow;
    }


    public void OpenAdventureDialog(System.Windows.Forms.OpenFileDialog ofd)
    {

        try
        {
            ofd.DefaultExt = "taf";
#if Runner
            ofd.Filter = "ADRIFT Text Adventures (*.taf; *.blorb)|*.taf; *.blorb|All Files (*.*)|*.*";
            If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("taf") && ! ofd.FileName.ToLower.EndsWith(".blorb") Then ofd.FileName = ""
#else
            ofd.Filter = "ADRIFT Text Adventures (*.taf)|*.taf|All Files (*.*)|*.*";
            If ofd.FileName.Length > 3 && ! ofd.FileName.ToLower.EndsWith("taf") Then ofd.FileName = ""
#endif

            if (ofd.ShowDialog() = DialogResult.OK)
            {
                if (ofd.FileName <> "")
                {
                    if (IO.File.Exists(ofd.FileName))
                    {
#if Runner
                        UserSession.OpenAdventure(ofd.FileName);
#else
                        OpenAdventure(ofd.FileName);
#endif

                    Else
                        ErrMsg("File not found!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error displaying Open File Dialog", ex);
        }

    }


    public bool IsRunningOnMono()
    {
        return Type.GetType("Mono.Runtime") IsNot null;
    }

#if Not www
    public void RemoveFileFromList(string sFilename)
    {

        If sFilename == null Then Exit Sub

        private New List<string> lRecents;
        private int iRetrieve;
        private string sProject;
#if Generator
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager UTMMain = fGenerator.UTMMain;
        sProject = "Generator"
#else
#if Mono
#else
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager UTMMain = fRunner.UTMMain;
#endif
        sProject = "Runner"
#endif

        for (int iPrevious = 1; iPrevious <= 20; iPrevious++)
        {
            private string sPrevious = GetSetting("ADRIFT", sProject, "Recent_" + iPrevious, "");
            'If sPrevious = sFilename Then DeleteSetting("ADRIFT", sProject, "Recent_" & iPrevious)
            If sPrevious <> "" && sPrevious.ToLower <> sFilename.ToLower Then lRecents.Add(sPrevious)
        Next;

        For Each sRecent As String In lRecents
            iRetrieve += 1;
            SaveSetting("ADRIFT", sProject, "Recent_" + iRetrieve, sRecent);
        Next;
        for (int iPrevious = iRetrieve + 1; iPrevious <= 20; iPrevious++)
        {
            try
            {
                DeleteSetting("ADRIFT", sProject, "Recent_" + iPrevious);
            Catch
            }
        Next;

#if Mono
        fRunner.miRecentAdventures.DropDownItems.Clear();
        AddPrevious();
#else
        for (int m = UTMMain.Tools.Count - 1; m <= 0; m += -1)
        {
            if (CStr(UTMMain.Tools(m).SharedProps.Tag) = "_RECENT_")
            {
                UTMMain.Tools.RemoveAt(m);
            }
        Next;

        AddPrevious(UTMMain, sProject);
#endif

    }


    public void AddFileToList(string sFilename)
    {

        If sFilename == null Then Exit Sub

        Dim sRecents(19) As String
        private int iRetrieve;
        private string sPrevious;
        private string sProject;
#if Generator
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager UTMMain = fGenerator.UTMMain;
        sProject = "Generator"
#else
#if Mono
#else
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager UTMMain = fRunner.UTMMain;
#endif
        sProject = "Runner"
#endif

        for (int iPrevious = 1; iPrevious <= 20; iPrevious++)
        {
            sPrevious = GetSetting("ADRIFT", sProject, "Recent_" & iPrevious, "")
            if (sPrevious.ToUpper <> sFilename.ToUpper && iRetrieve < 19)
            {
                iRetrieve += 1;
                sRecents(iRetrieve) = sPrevious;
            }
        Next;
        sRecents(0) = sFilename;

        for (int iPrevious = 1; iPrevious <= 20; iPrevious++)
        {
            SaveSetting("ADRIFT", sProject, "Recent_" + iPrevious, SafeString(sRecents(iPrevious - 1)));
        Next;

#if Mono
        fRunner.miRecentAdventures.DropDownItems.Clear();
        AddPrevious();
#else
        for (int m = UTMMain.Tools.Count - 1; m <= 0; m += -1)
        {
            if (CStr(UTMMain.Tools(m).SharedProps.Tag) = "_RECENT_")
            {
                UTMMain.Tools.RemoveAt(m);
            }
        Next;

        AddPrevious(UTMMain, sProject);
#endif

    }
#endif


    public void ErrMsg(string sMessage, System.Exception ex = null)
    {

        private string sErrorMsg = sMessage;
        If ! ex == null Then sErrorMsg &= ": " + ex.Message

        if (Not ex Is null)
        {
            sErrorMsg &= vbCrLf + ex.StackTrace;
            while (Not ex.InnerException Is null)
            {
                ex = ex.InnerException
                sErrorMsg &= vbCrLf + ex.Message + vbCrLf + ex.StackTrace;
            }
        }

#if www
        throw New Exception(sMessage, ex)
        Exit Sub;
#endif

        ' We've got to remember the current active form, otherwise for some reason focus goes off ADRIFT (never a good thing!)
        try
        {
            private Form frmActive = Form.ActiveForm;
#if Not Mono AndAlso Not www
            private New frmError(sMessage, ex) fError;
            fError.ShowDialog();
            if (Not frmActive Is null)
            {
                if (Not frmActive.InvokeRequired)
                {
                    If ! frmActive.Focused Then frmActive.Focus()
                }
            }
#else
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation, "ADRIFT Error");
#endif
        Catch
            ' We've had an Error creating window handle error here, yikes!
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation, "ADRIFT Error");
        }

    }


    public int CharacterCount(string sText, char cCharacter)
    {
        CharacterCount = 0
        for (int i = 0; i <= sText.Length - 1; i++)
        {
            If sText.Substring(i, 1) = cCharacter Then CharacterCount += 1
        Next;
    }


    public string[] GetLibraries()
    {

        private string sLibraries = GetSetting("ADRIFT", "Generator", "Libraries");

        if (sLibraries = "")
        {
            ' Attempt to find the libarary in current directory
            private string SL = "StandardLibrary.amf";

            For Each sDir As String In New String() {IO.Path.GetDirectoryName(SafeString(Application.ExecutablePath)), DataPath}
                if (IO.File.Exists(sDir + IO.Path.DirectorySeparatorChar + SL))
                {
                    sLibraries = sDir & IO.Path.DirectorySeparatorChar & SL
                    Exit For;
                }
            Next;
        }

        return sLibraries.Split("|"c);

    }



    ' A replacement for VB.Left - Substring returns an error if you try to access part of the string that doesn't exist.
    public string sLeft(string sString, int iLength)
    {
        If sString == null || iLength < 0 Then Return ""
        if (sString.Length < iLength)
        {
            if (sString <> Left(sString, iLength))
            {
                sString = sString
            }
            return sString;
        Else
            if (sString.Substring(0, Math.Max(iLength, 0)) <> Left(sString, iLength))
            {
                sString = sString
            }
            return sString.Substring(0, Math.Max(iLength, 0));
        }
    }


    ' A replacement for VB.Right
    public string sRight(string sString, int iLength)
    {
        If sString == null || iLength < 0 Then Return ""
        if (iLength > sString.Length)
        {
            if (sString <> Right(sString, iLength))
            {
                sString = sString
            }
            return sString;
        Else
            if (sString.Substring(sString.Length - iLength) <> Right(sString, iLength))
            {
                sString = sString
            }
            return sString.Substring(sString.Length - iLength);
        }
    }


    ' A replacement for VB.Mid
    public string sMid(string sString, int iStart)
    {
        If sString == null Then Return ""
        If iStart < 1 Then iStart = 1
        if (iStart > sString.Length)
        {
            return "";
        Else
            return sString.Substring(iStart - 1);
        }
    }

    public string sMid(string sString, int iStart, int iLength)
    {
        If sString == null Then Return ""
        If iLength < 0 Then Return ""
        If iStart < 1 Then iStart = 1
        if (iStart > sString.Length)
        {
            return "";
        ElseIf iLength > sString.Length || iLength + iStart > sString.Length Then
            'If Mid(sString, iStart, iLength) <> sString.Substring(iStart - 1) Then
            '    sString = sString
            'End If
            return sString.Substring(iStart - 1);
        Else
            'If Mid(sString, iStart, iLength) <> sString.Substring(iStart - 1, iLength) Then
            '    sString = sString
            'End If
            return sString.Substring(iStart - 1, iLength);
        }
    }


    public int sInstr(string sText, string sSearchFor)
    {
        if (InStr(sText, sSearchFor) <> sInstr(1, sText, sSearchFor))
        {
            sText = sText
        }
        return sInstr(1, sText, sSearchFor);
    }
    public int sInstr(int startIndex, string sText, string sSearchFor)
    {
        If sText == null || sText = "" Then Return 0 ' -1
        if (sText.IndexOf(sSearchFor, startIndex - 1) + 1 <> InStr(startIndex, sText, sSearchFor))
        {
            sText = sText
        }
        return sText.IndexOf(sSearchFor, startIndex - 1) + 1;
    }


    public string DoubleToString(double dfDouble, string sFormat = "#, ##0")
    {
        return dfDouble.ToString(sFormat, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
    }


    private Font GetDefaultFont()
    {
#if Runner
        if (UserSession.bUseDefaultFont)
        {
            return UserSession.DefaultFont;
        Else
#endif
        if (Adventure IsNot null)
        {
            return Adventure.DefaultFont;
        Else
            return new Font("Arial", 12, GraphicsUnit.Point);
        }
#if Runner
        }
#endif
    }


    private Boolean? _AllowDevToSetColours;
    public bool AllowDevToSetColours
        {
            get
            {
            if (Not _AllowDevToSetColours.HasValue)
            {
                _AllowDevToSetColours = CBool(GetSetting("ADRIFT", "Runner", "AllowColours", "1"))
            }
            return _AllowDevToSetColours.Value;
        }
set(Boolean)
            _AllowDevToSetColours = value
        }
    }


    internal Color GetBackgroundColour()
    {

        if (Adventure IsNot null)
        {
#if Runner
        if (Not AllowDevToSetColours || Adventure.DeveloperDefaultBackgroundColour = null)
        {
            return ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Background", ColorTranslator.ToOle(Color.FromArgb(DEFAULT_BACKGROUNDCOLOUR)).ToString)));
        Else
            return Adventure.DeveloperDefaultBackgroundColour;
        }
#else
            return Adventure.DeveloperDefaultBackgroundColour;
#endif
        Else
            return Color.FromArgb(DEFAULT_BACKGROUNDCOLOUR);
        }

    }

    internal Color GetInputColour()
    {

        if (Adventure IsNot null)
        {
#if Runner
            if (Not AllowDevToSetColours || Adventure.DeveloperDefaultInputColour = null)
            {
                return ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text1", ColorTranslator.ToOle(Color.FromArgb(DEFAULT_INPUTCOLOUR)).ToString)));
            Else
                return Adventure.DeveloperDefaultInputColour;
            }
#else
            return Adventure.DeveloperDefaultInputColour;
#endif
        Else
            return Color.FromArgb(DEFAULT_INPUTCOLOUR);
        }

    }

    internal Color GetOutputColour()
    {

        if (Adventure IsNot null)
        {
#if Runner
            if (Not AllowDevToSetColours || Adventure.DeveloperDefaultOutputColour = null)
            {
                return ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text2", ColorTranslator.ToOle(Color.FromArgb(DEFAULT_OUTPUTCOLOUR)).ToString)));
            Else
                return Adventure.DeveloperDefaultOutputColour;
            }
#else
            return Adventure.DeveloperDefaultOutputColour;
#endif
        Else
            return Color.FromArgb(DEFAULT_OUTPUTCOLOUR);
        }

    }

    internal Color GetLinkColour()
    {

        if (Adventure IsNot null)
        {
#if Runner
            if (Not AllowDevToSetColours || Adventure.DeveloperDefaultLinkColour = null)
            {
                return ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "LinkColour", ColorTranslator.ToOle(Color.FromArgb(DEFAULT_LINKCOLOUR)).ToString)));
            Else
                return Adventure.DeveloperDefaultLinkColour;
            }
#else
            return Adventure.DeveloperDefaultLinkColour;
#endif
        Else
            return Color.FromArgb(DEFAULT_LINKCOLOUR);
        }

    }

internal enum eJustification
    {
        [Left];
        [Right];
        [Central];
    }
    public void Source2HTML(string sSource, ref RichText As RichTextBox, bool bClearRTB, bool bDebug = false, ref sUnprocessedText As String = Nothing, Dictionary(Of String Replacements, String)
    {

#if www
        fRunner.AppendHTML(sSource);
        Exit Sub;
#else

        private New Generic.List<Font> arlFonts;
        private New Generic.List<Color> arlColours;
        private New Generic.List<eJustification> arlJust;
        private int iFontLevel = 0;
        private int iJustLevel = 0;
        private string sChunk;
        private bool bItalic;
        private bool bBold;
        private bool bUnderline;
        private bool bColour;
        private Color Colour;

        try
        {

            If bClearRTB Then RichText.Text = ""

            private Font f = GetDefaultFont();

            arlFonts.Add(f);
            arlColours.Add(GetOutputColour);
            arlJust.Add(eJustification.Left);

            while (sSource <> "")
            {

                if (sLeft(sSource, 1) = "<")
                {

                    private string sToken;
                    private string sBuffer;


                    if (sInstr(1, sSource, ">") > 0)
                    {
                        ' Handle embedded tags
                        private int i = 1;
                        private int iLevel = 0;
                        while (i < sSource.Length - 1 && Not (iLevel = 0 && sSource(i) = ">"))
                        {
                            switch (sSource(i))
                            {
                                case "<"c:
                                    {
                                    iLevel += 1;
                                case ">"c:
                                    {
                                    iLevel -= 1;
                            }
                            i += 1;
                        }
                        sToken = sSource.Substring(0, i + 1)
                        'sToken = sLeft(sSource, sInstr(1, sSource, ">"))
                    Else
                        sToken = sSource
                    }

                    private string sTokenOrig = sToken;
                    sToken = sToken.ToLower
                    private int iTokenLen = sToken.Length;
                    sToken = sToken.Replace(" =", "=").Replace("= ", "=").Replace("colour", "color")

                    switch (sToken)
                    {
                        case "<i>" : bItalic = true:
                            {
                        case "</i>" : bItalic = false:
                            {
                        case "<b>" : bBold = true:
                            {
                        case "</b>" : bBold = false:
                            {
                        case "<u>" : bUnderline = true:
                            {
                        case "</u>" : bUnderline = false:
                            {
                        case "<c>" : bColour = true:
                            {
                        case "</c>" : bColour = false:
                            {
                        case "<br>" : sSource = sToken + vbCrLf + Right(sSource:
                        case Len(sSource) - Len(sToken)):
                            {
                        case "</font>":
                            {
                            if (iFontLevel > 0)
                            {
                                arlFonts.RemoveAt(iFontLevel);
                                arlColours.RemoveAt(iFontLevel);
                                iFontLevel -= 1
                                f = GetDefaultFont() ' Restore it in case we changed the default
                            }
                        case "</window>":
                            {
                            sUnprocessedText = sSource.Substring(sToken.Length)
                            Exit Sub;
                        case "<center>":
                        case "<centre>":
                            {
                            iJustLevel += 1;
                            arlJust.Add(eJustification.Central);
                        case "</center>":
                        case "</centre>":
                        case "</right>":
                        case "</left>":
                            {
                            if (iJustLevel > 0)
                            {
                                arlJust.RemoveAt(iJustLevel);
                                iJustLevel -= 1;
                            }
                        case "<right>":
                            {
                            iJustLevel += 1;
                            arlJust.Add(eJustification.Right);
                        case "<left>":
                            {
                            iJustLevel += 1;
                            arlJust.Add(eJustification.Left);
                        case "<del>":
                            {
                            RichText.SelectionStart = RichText.TextLength - 1;
                            RichText.SelectionLength = 1;
                            RichText.SelectedText = Chr(0);
                        case "<waitkey>":
                            {
#if Runner
                            'RichText.SelectionStart = RichText.TextLength
                            'RichText.ScrollToCaret()
                            if (Not bDebug)
                            {
                                ScrollToEnd(RichText);
                                fRunner.WaitKey();
                            }
                            Adventure.Player.WalkTo = "" ' Terminate any walks;
#endif
                        case "<cls>":
                            {
                            if (Not bDebug)
                            {
#if Runner
                                If RichText == fRunner.txtOutput Then UserSession.iPreviousOffset = 0
#endif
                                RichText.Clear();
                            }
                    }

#if Runner
                    if (sLeft(sToken, 6) = "<wait ")
                    {
                        private string sTime = sRight(sToken, sToken.Length - 6).Replace(">", "");
                        private double dfTime = SafeDbl(sTime);
                        if (dfTime > 0 && Not bDebug)
                        {
                            ScrollToEnd(RichText);
                            fRunner.Locked = true;
                            Application.DoEvents();
                            Threading.Thread.Sleep(SafeInt(dfTime * 1000));
                            fRunner.Locked = false;
                        }
                    }

                    if (sLeft(sToken, 8) = "<window ")
                    {
                        private string sWindow = sSource.Substring(8, sToken.Length - 8).Replace(">", "") ' Take from sSource rather than sToken so we get the defined case;

                        if (sWindow <> "")
                        {
                            private RichTextBox txtOutput = null;
#if Not Mono
                            private Infragistics.Win.UltraWinDock.DockablePaneBase pane = fRunner.UDMRunner.PaneFromKey("pane_" + sWindow);
                            if (pane Is null)
                            {
                                pane = New Infragistics.Win.UltraWinDock.DockableControlPane("pane_" & sWindow)
                                fRunner.UDMRunner.ControlPanes.Add((Infragistics.Win.UltraWinDock.DockableControlPane)(pane));
                                fRunner.UDMRunner.DockAreas(0).Panes.Add(pane);
                                txtOutput = New RichTextBox
                                With txtOutput;
                                    .Font = fRunner.txtOutput.Font;
                                    .ForeColor = fRunner.txtOutput.ForeColor;
                                    .BackColor = fRunner.txtOutput.BackColor;
                                    .ReadOnly = true;
                                    .Name = "window_" + sWindow;
                                    AddHandler .MouseDown, AddressOf frmRunner.txtOutput_MouseDown;
                                    AddHandler .GotFocus, AddressOf frmRunner.txtOutput_GotFocus;
                                }
                                With (Infragistics.Win.UltraWinDock.DockableControlPane)(pane);
                                    .Control = txtOutput;
                                    .Text = sWindow;
                                    .Closed = false;
                                }
                                ' The layout restored on game load won't have included this control, so re-load it - not ideal :(
                                fRunner.RestoreLayout((Infragistics.Win.UltraWinDock.DockableControlPane)(pane));
                            Else
                                txtOutput = CType(CType(pane, Infragistics.Win.UltraWinDock.DockableControlPane).Control, RichTextBox)
                            }
#endif

                            private string sUnprocessed = "";

                            Source2HTML(sSource.Substring(sToken.Length), txtOutput, bClearRTB, bDebug, sUnprocessed);
                            sSource = sToken & sUnprocessed
                        }
                    }

                    if (sLeft(sToken, 5) = "<img ")
                    {
                        private int i;

                        i = sInstr(sToken, " src=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sTokenOrig, i + 5, sTokenOrig.Length - i - 5)
                            if (Left(sBuffer, 1) = Chr(34))
                            {
                                sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            Else
                                If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                            }

                            private bool bVisible = false;
#if Not Mono AndAlso Not www
                            For Each cp As Infragistics.Win.UltraWinDock.DockableControlPane In fRunner.UDMRunner.ControlPanes
                                If cp.Text = "Graphics" && cp.IsVisible Then bVisible = true
                            Next;
#endif
                            try
                            {
                                if (UserSession.bGraphics && Not bDebug)
                                {
                                    if (Adventure.BlorbMappings IsNot null && Adventure.BlorbMappings.Count > 0)
                                    {
                                        private int iResource = 0;
                                        If Adventure.BlorbMappings.ContainsKey(sBuffer) Then iResource = Adventure.BlorbMappings(sBuffer)
                                        if (iResource > 0)
                                        {
                                            fRunner.pbxGraphics.Image = Blorb.GetImage(iResource, true);
                                        }
                                    Else
                                        fRunner.pbxGraphics.Load(sBuffer);
                                    }

                                    if (Not bVisible)
                                    {
                                        if (false)
                                        {
                                            ' RTF method
                                            private Image img = fRunner.pbxGraphics.Image;
                                            private New IO.MemoryStream stmMemory;
                                            'img.Save(stmMemory, System.Drawing.Imaging.ImageFormat.Bmp)
                                            stmMemory = WmfStuff.MakeMetafileStream(img)

                                            'Dim converter As New ImageConverter
                                            'Dim bytImage() As Byte = CType(converter.ConvertTo(img, GetType(Byte())), Byte())
                                            Dim bytImage() As Byte = stmMemory.ToArray
                                            private string sHex = BitConverter.ToString(bytImage).Replace("-", "").ToLower;


                                            private string sRTF = "{\pict\wmetafile8\picw" + img.Width + "\pich" + img.Height + "\picwgoal" + img.Width * 15 + "\pichgoal" + img.Height * 15 + " " + sHex + "}";
                                            'Dim sRTF As String = "{\pict\wbitmap8\picw" & img.Width & "\pich" & img.Height & "\picwgoal" & img.Width * 15 & "\pichgoal" & img.Height * 15 & " " & sHex & "}"
                                            RichText.ReadOnly = false;
                                            'RichText.Rtf.Insert(RichText.Rtf.Length - 1, sRTF)
                                            RichText.Rtf = RichText.Rtf.Insert(RichText.Rtf.Length - 3, sRTF);
                                            'RichText.SelectedRtf = sRTF
                                            RichText.ReadOnly = true;

                                        Else
                                            ' Clipboard method
                                            'Dim oExistingData As IDataObject = Nothing
                                            private string sExistingText = null;
                                            private Image imgExistingImage = null;
                                            try
                                            {
                                                If Clipboard.ContainsText Then sExistingText = Clipboard.GetText
                                                If Clipboard.ContainsImage Then imgExistingImage = Clipboard.GetImage
                                                'oExistingData = Clipboard.GetDataObject
                                            Catch
                                            }
                                            Clipboard.SetImage(fRunner.pbxGraphics.Image);
                                            RichText.ReadOnly = false;
                                            'Dim align As HorizontalAlignment = RichText.SelectionAlignment
                                            'RichText.SelectionAlignment = HorizontalAlignment.Center

                                            RichText.Paste();
                                            'RichText.SelectedText = vbCrLf
                                            'RichText.SelectionAlignment = align
                                            RichText.ReadOnly = true;

                                            'Put whatever was on the clipboard before, back on
                                            'If oExistingData IsNot Nothing Then
                                            'Clipboard.SetDataObject(oExistingData, True, 1, 0)
                                            'End If
                                            If sExistingText != null Then Clipboard.SetText(sExistingText)
                                            If imgExistingImage != null Then Clipboard.SetImage(imgExistingImage)
                                        }
                                    }
                                }
                            Catch
                            }
                        }
                    }
#endif

                    if (sLeft(sToken, 7) = "<audio ")
                    {
                        private int i;
                        private int iChannel = 1;
                        private bool bLooping = false;
                        private string sSrc = "";

                        i = sInstr(sToken, "channel=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sTokenOrig, i + 8, sTokenOrig.Length - i - 8)
                            if (Left(sBuffer, 1) = Chr(34))
                            {
                                sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            Else
                                If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                            }
                            iChannel = SafeInt(sBuffer)
                        }

                        i = sInstr(sToken, " src=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sTokenOrig, i + 5, sTokenOrig.Length - i - 5)
                            if (Left(sBuffer, 1) = Chr(34))
                            {
                                sSrc = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            Else
                                If sInstr(1, sBuffer, " ") > 0 Then sSrc = Left(sBuffer, sInstr(1, sBuffer, " "))
                            }
                        }

                        i = sInstr(sToken, " loop=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sTokenOrig, i + 6, sTokenOrig.Length - i - 6)
                            If Left(sBuffer, 1) = Chr(34) Then sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            switch (sBuffer)
                            {
                                case "Y":
                                case "y":
                                case "1":
                                case "true":
                                case "true":
                                case "TRUE":
                                    {
                                    bLooping = True
                            }
                        }

#if Runner
                        if (UserSession.bSound && Not bDebug)
                        {
#endif
                        if (sToken.Contains(" pause"))
                        {
                            PauseSound(iChannel);
                        ElseIf sToken.Contains(" stop") Then
                            StopSound(iChannel);
                        Else
                            PlaySound(sSrc, iChannel, bLooping);
                        }
#if Runner
                        }
#endif

                    }


                    if ((sLeft(sToken, 8) = "<bgcolor" || sLeft(sToken, 9) = "<bgcolour") && AllowDevToSetColours)
                    {
                        sBuffer = sToken.Substring(9)
                        sBuffer = sBuffer.Substring(0, sBuffer.Length - 1)
                        while (sBuffer.StartsWith(" ") || sBuffer.StartsWith("="))
                        {
                            sBuffer = sBuffer.Substring(1)
                        }
                        private Color bgColour;
                        If Right(sBuffer, 1) = Chr(34) && Left(sBuffer, 1) = Chr(34) Then sBuffer = sMid(sBuffer, 2, sBuffer.Length - 2)
                        if (sBuffer.ToLower = "default")
                        {
                            bgColour = GetBackgroundColour()
                        Else
                            sBuffer = ColourLookup(sBuffer)

                            ' Interpret the colour
                            private byte rr = invhex(Left(sBuffer, 2));
                            private byte gg = invhex(Mid(sBuffer, 3, 2));
                            private byte bb = invhex(Right(sBuffer, 2));

                            bgColour = Color.FromArgb(rr, gg, bb)
                        }
#if Runner
                        fRunner.SetBackgroundColour(bgColour);
#endif
                    }


                    if (sLeft(sToken, 6) = "<font ")
                    {

                        iFontLevel += 1
                        private New Font(f, f.Style) NewFont;
                        private New Color NewColour;
                        NewColour = arlColours(iFontLevel - 1)
                        private int i;

                        i = sInstr(sToken, "color=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sToken, i + 6, sToken.Length - i - 6)
                            If sInstr(sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(sBuffer, " ") - 1)
                            If Right(sBuffer, 1) = Chr(34) && Left(sBuffer, 1) = Chr(34) Then sBuffer = sMid(sBuffer, 2, sBuffer.Length - 2)
                            if (AllowDevToSetColours || (sBuffer.ToUpper = "#" + Hex(GetLinkColour.ToArgb).Substring(2) && sSource.Contains("</u>")))
                            {
                                if (sBuffer.ToLower = "default")
                                {
                                    NewColour = GetOutputColour()
                                Else
                                    sBuffer = ColourLookup(sBuffer)

                                    ' Interpret the colour
                                    private byte rr = invhex(Left(sBuffer, 2));
                                    private byte gg = invhex(Mid(sBuffer, 3, 2));
                                    private byte bb = invhex(Right(sBuffer, 2));

                                    NewColour = Color.FromArgb(rr, gg, bb)
                                }
                            }
                        }

                        i = sInstr(sToken, "face=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sToken, i + 5, sToken.Length - i - 5)
                            if (Left(sBuffer, 1) = Chr(34))
                            {
                                sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            Else
                                If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                            }

                            if (CBool(GetSetting("ADRIFT", "Runner", "AllowFonts", "1")) || sSource.StartsWith("<font face=""Wingdings"" size=14>Ø</font>") || sSource.StartsWith("<font face=""Wingdings"" size=18>@</font>"))
                            {
                                NewFont = New Font(sBuffer, f.Size, f.Style, GraphicsUnit.Point)
                                f = NewFont
                            }
                        }

                        i = sInstr(sToken, "size=")
                        if (i > 0)
                        {
                            sBuffer = sMid(sToken, i + 5, sToken.Length - i - 5)
                            if (Left(sBuffer, 1) = Chr(34))
                            {
                                sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                            Else
                                If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                            }

                            if (CSng(sBuffer) < 0)
                            {
                                sBuffer = (arlFonts(iFontLevel - 1).Size + CSng(sBuffer)).ToString
                            }
                            if (Left(sBuffer, 1) = "+")
                            {
                                sBuffer = (arlFonts(iFontLevel - 1).Size + CSng(sBuffer.Substring(1))).ToString
                            }

                            if (CBool(GetSetting("ADRIFT", "Runner", "AllowFonts", "1")))
                            {
                                NewFont = New Font(f.FontFamily, CSng(sBuffer), f.Style, GraphicsUnit.Point)
                            }
                        }

                        arlFonts.Add(NewFont);
                        arlColours.Add(NewColour);

                    }

                    sSource = sRight(sSource, sSource.Length - iTokenLen)

                }

                if (sInstr(1, sSource, "<") > 0)
                {
                    sChunk = sLeft(sSource, sInstr(1, sSource, "<") - 1)
                    sSource = sRight(sSource, sSource.Length - sInstr(1, sSource, "<") + 1)
                Else
                    sChunk = sSource
                    sSource = ""
                }

                With RichText;
                    If .IsDisposed Then Exit Sub

                    .SelectionStart = .TextLength;

                    private New FontStyle fStyle;
                    fStyle = FontStyle.Regular
                    If bBold Then fStyle = FontStyle.Bold
                    If bItalic Then fStyle = fStyle | FontStyle.Italic
                    If bUnderline Then fStyle = fStyle | FontStyle.Underline
                    If bColour Then Colour = GetInputColour() Else Colour = arlColours(iFontLevel)

                    try
                    {
                        switch (arlJust(iJustLevel))
                        {
                            case eJustification.Left:
                                {
                                if (.SelectionAlignment <> HorizontalAlignment.Left && sRight(RichText.Text, 1) <> vbLf)
                                {
                                    .AppendText(vbCrLf) ' sChunk = vbCrLf + sChunk;
                                    If sChunk.StartsWith(vbLf) Then sChunk = sChunk.Substring(1)
                                }
                                .SelectionAlignment = HorizontalAlignment.Left;
                            case eJustification.Central:
                                {
                                if (.SelectionAlignment <> HorizontalAlignment.Center && sRight(RichText.Text, 1) <> vbLf)
                                {
                                    .AppendText(vbCrLf);
                                    If sChunk.StartsWith(vbLf) Then sChunk = sChunk.Substring(1)
                                }
                                .SelectionAlignment = HorizontalAlignment.Center;
                            case eJustification.Right:
                                {
                                if (.SelectionAlignment <> HorizontalAlignment.Right && sRight(RichText.Text, 1) <> vbLf)
                                {
                                    .AppendText(vbCrLf) ' sChunk = vbCrLf + sChunk;
                                    If sChunk.StartsWith(vbLf) Then sChunk = sChunk.Substring(1)
                                }
                                .SelectionAlignment = HorizontalAlignment.Right;
                        }
                    Catch
                    }

                    private Font font = new Font(arlFonts(iFontLevel), fStyle);
                    if (font IsNot null)
                    {
                        try
                        {
                            .SelectionFont = font;
                            'sChunk = font.ToString & sChunk
                        }
                        catch (Exception ex)
                        {
                            'ErrMsg("Error setting font " & font.Name, ex)
                        }
                    }

                    try
                    {
                        if (.SelectionColor <> Colour)
                        {
                            If sChunk.StartsWith(vbCrLf) Then sChunk = " " + sChunk
                            '    .AppendText(" " & vbCrLf) ' A fudge, because for some reason we seem to be applying the new colour to the last char before the CR
                            '    sChunk = sChunk.Substring(2)
                            'End If
                            .SelectionColor = Colour;
                        }
                    }
                    catch (Exception ex)
                    {
                        'ErrMsg("Error setting selection colour", ex)
                    }

                    '.SelectedText = sChunk.Replace("&gt;", ">").Replace("&lt;", "<")
                    .AppendText(sChunk.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&perc;", "%").Replace("&quot;", """"));

                    ' A nasty workaround because there seems to be a bug colouring extended Ascii characters.  :-(
                    private bool bRecolour = false;
                    For Each c As Char In sChunk
                        If Asc(c) > 128 Then
                            bRecolour = True
                            Exit For;
                        }
                    Next;
                    if (bRecolour)
                    {
                        private int iChunkLength = Math.Min(sChunk.Replace(vbCrLf, "@").Length, .TextLength);
                        .SelectionStart = .TextLength - iChunkLength;
                        .SelectionLength = iChunkLength;
                        .SelectionColor = Colour;
                    }
                    '.ScrollToCaret()

                    '                .SelectedText = sChunk
                    '.SelectionStart = .TextLength

                    'If Not bClearRTB Then
                    '    .Focus()
                    '    .ScrollToCaret()
                    'End If

                }

            }

            if (Replacements IsNot null)
            {
                ' Highlight all replacements with slightly lighter/darker background
                private Color bgColour = GetBackgroundColour();
                if (CInt(bgColour.R) + CInt(bgColour.G) + CInt(bgColour.B) > 384)
                {
                    ' Go darker
                    bgColour = Color.FromArgb(bgColour.A, Math.Max(bgColour.R - 40, 0), Math.Max(bgColour.G - 40, 0), Math.Max(bgColour.B - 40, 0))
                Else
                    ' Go ligher
                    bgColour = Color.FromArgb(bgColour.A, Math.Min(bgColour.R + 40, 255), Math.Min(bgColour.G + 40, 255), Math.Min(bgColour.B + 40, 255))
                }
                For Each kvp As KeyValuePair(Of String, String) In Replacements
                        private int i = RichText.Text.IndexOf(kvp.Value);
                    if (i > -1)
                    {
                        RichText.SelectionStart = i;
                        RichText.SelectionLength = kvp.Value.Length;
                        RichText.SelectionBackColor = bgColour;
                    }
                Next;
                }

                ScrollToEnd(RichText);

        }
        catch (ObjectDisposedException exOD)
        {
            ' Fail silently - we're shutting down
        }
        catch (Exception ex)
        {
            ErrMsg("Source2HTML error", ex);
        }

#endif

    }


private class WmfStuff
    {

        <Flags> _;
private enum EmfToWmfBitsFlags
        {
            EmfToWmfBitsFlagsDefault = &H0
            EmfToWmfBitsFlagsEmbedEmf = &H1
            EmfToWmfBitsFlagsIncludePlaceable = &H2
            EmfToWmfBitsFlagsNoXORClip = &H4
        }

        private static int MM_ISOTROPIC = 7;
        private static int MM_ANISOTROPIC = 8;

        <System.Runtime.InteropServices.DllImport("gdiplus.dll")> _;
        private static void GdipEmfToWmfBits(IntPtr _hEmf, UInteger _bufferSize, Byte( _buffer)
        {
        }
        <System.Runtime.InteropServices.DllImport("gdi32.dll")> _;
        private static void SetMetaFileBitsEx(UInteger _bufferSize, Byte( _buffer)
        {
        }
        <System.Runtime.InteropServices.DllImport("gdi32.dll")> _;
        private static IntPtr CopyMetaFile(IntPtr hWmf, string filename)
        {
        }
        <System.Runtime.InteropServices.DllImport("gdi32.dll")> _;
        private static bool DeleteMetaFile(IntPtr hWmf)
        {
        }
        <System.Runtime.InteropServices.DllImport("gdi32.dll")> _;
        private static bool DeleteEnhMetaFile(IntPtr hEmf)
        {
        }

        public static IO.MemoryStream MakeMetafileStream(System.Drawing.Image image)
        {
            private System.Drawing.Imaging.Metafile metafile = null;
            using (g As Graphics = Graphics.FromImage(image))
            {
                private IntPtr hDC = g.GetHdc();
                metafile = New System.Drawing.Imaging.Metafile(hDC, System.Drawing.Imaging.EmfType.EmfOnly)
                g.ReleaseHdc(hDC);
            End Using

            using (g As Graphics = Graphics.FromImage(metafile))
            {
                g.DrawImage(image, 0, 0);
            End Using
            private IntPtr _hEmf = metafile.GetHenhmetafile();
            private UInteger _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
            private byte[] _buffer = new Byte(CInt(_bufferSize) - 1) {};
            GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
            DeleteEnhMetaFile(_hEmf);

            private New IO.MemoryStream[] stream;
            stream.Write(_buffer, 0, CInt(_bufferSize));
            stream.Seek(0, 0);

            return stream;
        }

    }


    ' Scrolls to the end of the text, and adds a <Wait> box if necessary
    internal void ScrollToEnd(RichTextBox RichText)
    {

        With RichText;
            .SelectionStart = .TextLength + 1;
            .ScrollToCaret() ' Scroll to end;
#if Runner
            if (RichText Is fRunner.txtOutput)
            {
                private int iCurrentTop = .GetCharIndexFromPosition(new Point(0, 0));
                if (iCurrentTop > UserSession.iPreviousOffset)
                {
                    .SelectionStart = UserSession.iPreviousOffset;
                    .ScrollToCaret() ' Scroll back to start of this text section;

                    if (.GetCharIndexFromPosition(new Point(.Width, .Height)) < .TextLength - 1)
                    {
                        fRunner.btnMore.Size = New Size(fRunner.pnlBottom.Size.Width - 1, fRunner.pnlBottom.Size.Height - 1);
                        fRunner.btnMore.Location = New Point(fRunner.pnlBottom.Location.X - 1, fRunner.pnlBottom.Location.Y - 1);
                        fRunner.btnMore.Visible = true;
                    }
                }
            }
#endif
        }

    }


    ' Escape any characters that are special in RE's
    internal string MakeTextRESafe(string sText)
    {

        sText = sText.Replace("+", "\+").Replace("*", "\*")

        return sText;
    }


    internal byte invhex(string hexcode)
    {

        private byte n;

        if (Left(hexcode, 1) = "0")
        {
            For n = 0 To 15
                If Hex(n) = UCase(Right(hexcode, 1)) Then Return n
            Next n;
        }
        For n = 16 To 254
            If Hex(n) = UCase(hexcode) Then Return n
        Next n;
        If Hex(255) = UCase(hexcode) Then Return 255
        return 0;

    }


    internal string ColourLookup(string sCode)
    {

        sCode = sCode.Replace("""", "")

        ColourLookup = Nothing

        switch (LCase(sCode))
        {
            case "black" : ColourLookup = "000000":
                {
            case "blue" : ColourLookup = "0000ff":
                {
            case "cyan":
            case "turquoise":
            case "aqua" : ColourLookup = "00ffff":
                {
            case "default":
                {
                'ColourLookup = Right(Hex(colour(0)), 2) & sMid(Hex(colour(0)), 3, 2) & Left(Hex(colour(0)), 2)
            case "gray" : ColourLookup = "808080":
                {
            case "green" : ColourLookup = "008000":
                {
            case "lime" : ColourLookup = "00ff00":
                {
            case "magenta":
            case "fuchsia" : ColourLookup = "ff00ff":
                {
            case "maroon" : ColourLookup = "800000":
                {
            case "navy" : ColourLookup = "000080":
                {
            case "olive" : ColourLookup = "808000":
                {
            case "orange" : ColourLookup = "ff8000":
                {
            case "pink" : ColourLookup = "ff8888":
                {
            case "purple" : ColourLookup = "800080":
                {
            case "red" : ColourLookup = "ff0000":
                {
            case "silver" : ColourLookup = "c0c0c0":
                {
            case "teal" : ColourLookup = "008080":
                {
            case "white" : ColourLookup = "ffffff":
                {
            case "yellow" : ColourLookup = "ffff00":
                {

            default:
                {
                If Left(sCode, 1) = "#" Then sCode = Right(sCode, Len(sCode) - 1)
                If Len(sCode) > 6 Then sCode = "ffffff"
                while (Len(sCode) < 6)
                {
                    sCode = sCode & "0"
                }
                ColourLookup = sCode
        }

    }

    public string DirectionName(DirectionsEnum dir)
    {

        private string sName = Adventure.sDirectionsRE(dir);
        If sName.Contains("/") Then sName = sName.Substring(0, sName.IndexOf("/"))
        return sName;

        'Select Case dir
        '    Case DirectionsEnum.North
        '        Return "North"
        '    Case DirectionsEnum.NorthEast
        '        Return "North East"
        '    Case DirectionsEnum.East
        '        Return "East"
        '    Case DirectionsEnum.SouthEast
        '        Return "South East"
        '    Case DirectionsEnum.South
        '        Return "South"
        '    Case DirectionsEnum.SouthWest
        '        Return "South West"
        '    Case DirectionsEnum.West
        '        Return "West"
        '    Case DirectionsEnum.NorthWest
        '        Return "North West"
        '    Case DirectionsEnum.Up
        '        Return "Up"
        '    Case DirectionsEnum.Down
        '        Return "Down"
        '    Case DirectionsEnum.In
        '        Return "Inside"
        '    Case DirectionsEnum.Out
        '        Return "Outside"
        'End Select

        'Return Nothing

    }



    public string DirectionRE(DirectionsEnum dir, bool bBrackets = true, bool bRealRE = false)
    {

        'Dim sRE As String = ""
        'Dim sOr As String = CStr(IIf(bRealRE, "|", "/"))

        '' Allow these to be customised later
        'Select Case dir
        '    Case DirectionsEnum.North
        '        sRE = "north" & sOr & "n"
        '    Case DirectionsEnum.NorthEast
        '        sRE = "north east" & sOr & "northeast" & sOr & "north-east" & sOr & "ne"
        '    Case DirectionsEnum.East
        '        sRE = "east" & sOr & "e"
        '    Case DirectionsEnum.SouthEast
        '        sRE = "south east" & sOr & "southeast" & sOr & "south-east" & sOr & "se"
        '    Case DirectionsEnum.South
        '        sRE = "south" & sOr & "s"
        '    Case DirectionsEnum.SouthWest
        '        sRE = "south west" & sOr & "southwest" & sOr & "south-west" & sOr & "sw"
        '    Case DirectionsEnum.West
        '        sRE = "west" & sOr & "w"
        '    Case DirectionsEnum.NorthWest
        '        sRE = "north west" & sOr & "northwest" & sOr & "north-west" & sOr & "nw"
        '    Case DirectionsEnum.Up
        '        sRE = "up" & sOr & "u"
        '    Case DirectionsEnum.Down
        '        sRE = "down" & sOr & "d"
        '    Case DirectionsEnum.In
        '        sRE = "inside" & sOr & "in"
        '    Case DirectionsEnum.Out
        '        sRE = "outside" & sOr & "out" & sOr & "o"
        'End Select

        private string sRE = Adventure.sDirectionsRE(dir).ToLower;
        If bRealRE Then sRE = sRE.Replace("/", "|")

        if (bBrackets)
        {
            if (bRealRE)
            {
                return "(" + sRE + ")";
            Else
                return "[" + sRE + "]";
            }
        Else
            return sRE;
        }

    }

    public bool KeyExists(string sKey)
    {

        With Adventure;
            If .htblLocations.ContainsKey(sKey) Then Return true
            If .htblObjects.ContainsKey(sKey) Then Return true
            If .htblTasks.ContainsKey(sKey) Then Return true
            If .htblEvents.ContainsKey(sKey) Then Return true
            If .htblCharacters.ContainsKey(sKey) Then Return true
            If .htblGroups.ContainsKey(sKey) Then Return true
            If .htblVariables.ContainsKey(sKey) Then Return true
            If .htblALRs.ContainsKey(sKey) Then Return true
            If .htblHints.ContainsKey(sKey) Then Return true
            If .htblAllProperties.ContainsKey(sKey) Then Return true
        }

        return false;

    }

    public string FindProperty(StringArrayList arlStates)
    {

        private bool bMatch;

        For Each prop As clsProperty In Adventure.htblAllProperties.Values
            bMatch = True
            if (prop.arlStates.Count = arlStates.Count)
            {
                For Each sState As String In arlStates
                    if (Not prop.arlStates.Contains(sState))
                    {
                        bMatch = False
                        Exit For;
                    }
                Next;
                if (bMatch)
                {
                    return prop.Key;
                }
            }
        Next;

        return null;

    }

    public string ToProper(string sText, bool bForceRestLower = true)
    {
        if (bForceRestLower)
        {
            return sLeft(sText, 1).ToUpper + sRight(sText, sText.Length - 1).ToLower;
        Else
            return sLeft(sText, 1).ToUpper + sRight(sText, sText.Length - 1);
        }
    }

    public StringArrayList FunctionNames()
    {
        private New StringArrayList Names;
        For Each sName As String In New String() {"AloneWithChar", "CharacterDescriptor", "CharacterName", "CharacterProper", "ConvCharacter", "DisplayCharacter", "DisplayLocation", "DisplayObject", "Held", "LCase", "ListCharactersOn", "ListCharactersIn", "ListCharactersOnAndIn", "ListHeld", "ListExits", "ListObjectsAtLocation", "ListWorn", "ListObjectsOn", "ListObjectsIn", "ListObjectsOnAndIn", "LocationName", "LocationOf", "NumberAsText", "ObjectName", "ObjectsIn", "ParentOf", "PCase", "Player", "PopUpChoice", "PopUpInput", "PrevListObjectsOn", "PrevParentOf", "ProperName", "PropertyValue", "Release", "Replace", "Sum", "TaskCompleted", "TheObject", "TheObjects", "Turns", "UCase", "Version", "Worn"}
            Names.Add(sName);
        Next;
        if (Adventure IsNot null)
        {
            For Each UDF As clsUserFunction In Adventure.htblUDFs.Values
                Names.Add(UDF.Name);
            Next;
        }
        return Names;
    }

    public string[] ReferenceNames()
    {
        return new String() {"%object%", "%objects%", "%object1%", "%object2%", "%object3%", "%object4%", "%object5%", "%direction%", "%direction1%", "%direction2%", "%direction3%", "%direction4%", "%direction5%", "%character%", "%character1%", "%character2%", "%character3%", "%character4%", "%character5%", "%text%", "%text1%", "%text2%", "%text3%", "%text4%", "%text5%", "%number%", "%number1%", "%number2%", "%number3%", "%number4%", "%number5%", "%location%", "%location1%", "%location2%", "%location3%", "%location4%", "%location5%", "%item%", "%item1%", "%item2%", "%item3%", "%item4%", "%item5%"};
    }

    public int InstrCount(string sText, string sSubText)
    {

        private int iOffset = 1;
        private int iCount = 0;

        while (iOffset < sText.Length)
        {
            if (sInstr(iOffset, sText, sSubText) > 0)
            {
                iCount += 1;
                iOffset = sInstr(iOffset, sText, sSubText) + 1
            Else
                return iCount;
            }
        }

        return iCount;

    }


    public string EvaluateExpression(string sExpression)
    {

        If sExpression = "" Then Return ""

        private New clsVariable var;
        var.Type = clsVariable.VariableTypeEnum.Text;
        var.SetToExpression(sExpression);
        'If var.StringValue <> "" Then ' Err, what if the function returns ""?
        return var.StringValue;
        'Else
        'Return sExpression
        'End If

    }
    public int EvaluateExpression(string sExpression, bool bInt)
    {

        private New clsVariable var;
        var.Type = clsVariable.VariableTypeEnum.Numeric;
        var.SetToExpression(sExpression);
        return var.IntValue;

    }


    public string ReplaceExpressions(string sText)
    {

        private New System.Text.RegularExpressions.Regex("<#(?<expression>.*?)#>", System.Text.RegularExpressions.RegexOptions.Singleline) reExp;
        For Each m As System.Text.RegularExpressions.Match In reExp.Matches(sText)
            sText = sText.Replace(m.Value, EvaluateExpression(m.Groups("expression").Value))
        Next;
        return sText;

    }


    public string ReplaceALRs(string sText, bool bAutoCapitalise = true)
    {

        try
        {
            If sText = "" Then Return ""

            sText = ReplaceFunctions(sText)
            sText = ReplaceExpressions(sText)

            private bool bChanged = false;

            '' Auto-capitalise - Do before ALRs as the user sees the final output so wants to replace that
            'If bAutoCapitalise Then
            '    Dim reCap As New System.Text.RegularExpressions.Regex("^(?<cap>[a-z])|\n(?<cap>[a-z])|[a-z][\.\!\?] ( )?(?<cap>[a-z])")
            '    While reCap.IsMatch(sText)
            '        Dim match As System.Text.RegularExpressions.Match = reCap.Match(sText)
            '        Dim sCap As String = match.Groups("cap").Value
            '        Dim iIndex As Integer = match.Groups("cap").Index
            '        sText = sText.Substring(0, iIndex) & sCap.ToUpper & sText.Substring(iIndex + sCap.Length)
            '    End While
            'End If

            ' Replace ALRs
            For Each alr As clsALR In Adventure.htblALRs.Values
                if (sText.Contains(alr.OldText))
                {
                    private string sALR = alr.NewText.ToString ' Get it in a variable in case we have DisplayOnce;
                    If sText = sALR Then Exit For
                    sText = sText.Replace(alr.OldText, ReplaceALRs(sALR, False))
                }
            Next;

            ' Auto-capitalise - needs to happen after ALR replacements, as some replacements may be both intra and start of sentences
            if (bAutoCapitalise Then '&& bChanged)
            {
                private New System.Text.RegularExpressions.Regex("^(?<cap>[a-z])|\n(?<cap>[a-z])|[a-z][\.\!\?][] reCap;
                while (reCap.IsMatch(sText))
                {
                    private System.Text.RegularExpressions.Match match = reCap.Match(sText);
                    private string sCap = match.Groups("cap").Value;
                    private int iIndex = match.Groups("cap").Index;
                    sText = sText.Substring(0, iIndex) & sCap.ToUpper & sText.Substring(iIndex + sCap.Length)
                    bChanged = True
                }
            }

            ' Do a second round of ALR replacements if we auto-capped anything, as user may want to replace the auto-capped version
            if (bChanged)
            {
                For Each alr As clsALR In Adventure.htblALRs.Values
                    if (sText.Contains(alr.OldText))
                    {
                        private string sALR = alr.NewText.ToString ' Get it in a variable in case we have DisplayOnce;
                        If sText = sALR Then Exit For
                        sText = sText.Replace(alr.OldText, ReplaceALRs(sALR, False))
                    }
                Next;
            }

            return sText;

        }
        catch (Exception ex)
        {
            ErrMsg("ReplaceALRs error", ex);
            return sText;
        }

    }



    public string pSpace(ref sText As String)
    {

        if (sText Is null || sText.Length = 0)
        {
            sText = ""
            return sText;
        Else
            If sText.EndsWith(vbLf) Then Return sText ' || sText.ToLower.EndsWith("<br>")
        }

        sText &= "  ";
        return sText;

    }


    public void DisplayError(string sText)
    {
#if Runner
        UserSession.DebugPrint(null, "", DebugDetailLevelEnum.Error, "<i><c>*** " + sText + "***</c></i>");
#else
        ErrMsg(sText);
#endif
    }


    ' A case ignoring search
    public bool Contains(string sTextToSearchIn, string sTextToFind, bool bExactWord = false, int iStart = 0)
    {
        sTextToFind = sTextToFind.Replace(".", "\.")
        private string sPattern = If(bExactWord, "\b" + sTextToFind + "\b", sTextToFind).ToString;
        private New System.Text.RegularExpressions.Regex(sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) regex;
        return regex.IsMatch(sTextToSearchIn.Substring(iStart));
    }
    public bool ContainsWord(string sTextToSearchIn, string sTextToFind)
    {
        return Contains(sTextToSearchIn, sTextToFind, true);
    }

    public string PreviousFunction(string sFunction, string sArgs)
    {

#if Runner
        With UserSession;
            private string sNewFunction = sFunction.Replace("prev", "");
            private clsGameState PreviousState = CType(.States.Peek, clsGameState) ' Note the previous state;
            .States.RecordState() ' Save where we are now;
            .States.RestoreState(PreviousState) ' Load up the previous state;

            PreviousFunction = ReplaceFunctions("%" & sNewFunction & "[" & sArgs & "]%")

            .States.Pop() ' Get rid of the 'current' state and load it back as present;
        }
#else
        return "";
#endif

    }


    ' Chops the last character off a string
    public string ChopLast(string sText)
    {

        if (sText.Length > 0)
        {
            return sText.Substring(0, sText.Length - 1);
        Else
            return "";
        }

    }


    ' If this is in an expression, we need to replace anything with a quoted value
    public string ReplaceOO(string sText, bool bExpression)
    {

        private New System.Text.RegularExpressions.Regex(".*?(?<embeddedexpression><#.*?#>).*?") reIgnore;
        if (reIgnore.IsMatch(sText))
        {
            private string sMatch = reIgnore.Match(sText).Groups("embeddedexpression").Value;
            private string sGUID;
            sGUID = ":" & System.Guid.NewGuid.ToString & ":"
            private string sReturn = ReplaceOO(sText.Replace(sMatch, sGUID), bExpression);
            return sReturn.Replace(sGUID, sMatch);
        Else
            ' Match anything unless it's between <# ... #> symbols
            'Dim re As New System.Text.RegularExpressions.Regex("(?!<#.*?)(?<firstkey>%?[A-Za-z][\w\|_-]*%?)(?<nextkey>\.%?[A-Za-z][\w\|_-]*%?(\(.+?\))?)+(?!.*?#>)")
            private New System.Text.RegularExpressions.Regex("(?!<#.*?)(?<firstkey>%?[A-Za-z][\w\|_]*%?)(?<nextkey>\.%?[A-Za-z][\w\|_]*%?(\(.+?\))?)+(?!.*?#>)") re;
            'Dim re As New System.Text.RegularExpressions.Regex("(?!<#.*?)(?<firstkey>%?[A-Za-z]([\w\|_]|-(?!\d))*%?)(?<nextkey>\.%?[A-Za-z]([\w\|_]|-(?!\d))*%?(\(.+?\))?)+(?!.*?#>)")
            private string sPrevious = "";
            private int iStartAt = 0;

            while (iStartAt < sText.Length && re.IsMatch(sText, iStartAt)  ' && sText <> sPrevious)
            {
                sPrevious = sText

                private string sMatch = re.Match(sText, iStartAt).Value;
                private bool bIntValue = false;
                private string sReplace = ReplaceOOProperty(sMatch, bInt:=bIntValue);
                private int iPrevStart = iStartAt;
                iStartAt = re.Match(sText, iStartAt).Index + sMatch.Length

#if Generator
                ' Replace functions with simple values, so that validation works on them ok (e.g. when typing an expression into an expression box, so we know it's a valid int etc)
                if (sReplace = "#*!~#")
                {
                    For Each p As clsProperty In Adventure.htblAllProperties.Values
                        if (sMatch.EndsWith("." + p.Key))
                        {
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                    {
                                    sReplace = "0"
                            }
                        }
                    Next;
                }
#endif

                if (sReplace <> "#*!~#")
                {
                    If sReplace.Contains(sMatch) Then sReplace = sReplace.Replace(sMatch, "*** RECURSIVE REPLACE ***")
                    If bExpression && ! bIntValue && ! Contains(sText, """" + sMatch + """", , iPrevStart + 1) Then sReplace = """" + sReplace + """"
                    sText = sText.Substring(0, iPrevStart) & Replace(sText, sMatch, sReplace, iPrevStart + 1, 1) 'sText = sText.Replace(sMatch, sReplace)
                    iStartAt += sReplace.Length - sMatch.Length;
                }

            }

            return ReplaceFunctions(sText, , false);
        }

    }


    private void ReplaceOOProperty(string sProperty, clsObject ob = null, clsCharacter ch = null, clsLocation loc = null, List(Of clsItemWithProperties lst)
    {

        private string sFunction = sProperty;
        private string sArgs = "";
        private string sRemainder = "";

        private int iIndexOfDot = sFunction.IndexOf(".");
        private int iIndexOfOB = sFunction.IndexOf("(");

        if (iIndexOfOB > -1 && sFunction.Contains(")"))
        {
            private int iIndexOfCB = sFunction.IndexOf(")", iIndexOfOB);
            if (iIndexOfDot > -1)
            {
                if (iIndexOfDot < iIndexOfOB)
                {
                    sRemainder = sFunction.Substring(iIndexOfDot + 1)
                    sFunction = sFunction.Substring(0, iIndexOfDot)
                Else
                    sArgs = sFunction.Substring(iIndexOfOB + 1, iIndexOfCB - iIndexOfOB - 1) '.ToLower
                    sRemainder = sFunction.Substring(iIndexOfCB + 2)
                    sFunction = sFunction.Substring(0, iIndexOfOB)
                }
            Else
                sArgs = sFunction.Substring(iIndexOfOB + 1, sFunction.LastIndexOf(")") - iIndexOfOB - 1) '.ToLower
                sRemainder = sFunction.Substring(iIndexOfCB + 1)
                sFunction = sFunction.Substring(0, iIndexOfOB)
            }
        ElseIf iIndexOfDot > 0 Then
            sRemainder = sFunction.Substring(iIndexOfDot + 1)
            sFunction = sFunction.Substring(0, iIndexOfDot)
        }

        'If sFunction.Contains(".") Then
        '    sFunction = sFunction.Substring(0, sFunction.IndexOf("."))
        '    sRemainder = sProperty.Substring(sProperty.IndexOf(".") + 1)
        'End If
        'If sFunction.Contains("(") AndAlso sFunction.Contains(")") Then
        '    sArgs = sFunction.Substring(sFunction.IndexOf("(") + 1, sFunction.LastIndexOf(")") - sFunction.IndexOf("(") - 1).ToLower
        '    sFunction = sFunction.Substring(0, sFunction.IndexOf("("))
        'End If


        if (lst IsNot null || (lstDirs IsNot null && (sFunction = "List" || sFunction = "Count" || sFunction = "Name" || sFunction = "")))
        {
            switch (sFunction)
            {
                case "":
                    {
                    private string sResult = "";
                    if (lst IsNot null)
                    {
                        for (int i = 0; i <= lst.Count - 1; i++)
                        {
                            sResult &= lst(i).Key;
                            If i < lst.Count - 1 Then sResult &= "|"
                        Next;
                    Else
                        for (int i = 0; i <= lstDirs.Count - 1; i++)
                        {
                            sResult &= lstDirs(i).ToString;
                            If i < lstDirs.Count - 1 Then sResult &= "|"
                        Next;
                    }

                    return sResult;

                case "Count":
                    {
                    if (lst IsNot null)
                    {
                        return lst.Count.ToString;
                    ElseIf lstDirs != null Then
                        return lstDirs.Count.ToString;
                    }
                    bInt = True
                    return "0";

                case "Sum":
                    {
                    private int iSum = 0;
                    if (lst IsNot null && sPropertyKey IsNot null)
                    {
                        For Each oItem As clsItemWithProperties In lst
                            if (oItem.HasProperty(sPropertyKey))
                            {
                                iSum += oItem.htblProperties(sPropertyKey).IntData;
                            }
                        Next;
                    }
                    bInt = True
                    return iSum.ToString;

                case "Description":
                    {
                    private string sResult = "";
                    For Each oItem As clsItem In lst
                        pSpace(sResult);
                        switch (true)
                        {
                            case TypeOf oItem Is clsObject:
                                {
                                sResult &= (clsObject)(oItem).Description.ToString;
                            case TypeOf oItem Is clsCharacter:
                                {
                                sResult &= (clsCharacter)(oItem).Description.ToString;
                            case TypeOf oItem Is clsLocation:
                                {
                                if (CType(oItem, clsLocation).ViewLocation = "")
                                {
                                    sResult &= "There is nothing of interest here.";
                                Else
                                    sResult &= (clsLocation)(oItem).ViewLocation;
                                }
                        }
                    Next;
                    return sResult;

                case "List":
                case "Name":
                    {
                    ' List(and) - And separated list - Default
                    ' List(or) - Or separated list
                    private string sSeparator = " and ";
                    private string sArgsTest = sArgs.ToLower;

                    If sArgsTest.Contains("or") Then sSeparator = " or "
                    If sArgsTest.Contains("rows") Then sSeparator = vbCrLf

                    ' List(definite/the) - List the objects names - Default
                    ' List(indefinite) - List a/an object
                    private ArticleTypeEnum Article = ArticleTypeEnum.Definite;
                    If sArgsTest.Contains("indefinite") Then Article = ArticleTypeEnum.Indefinite
                    If sArgsTest.Contains("none") Then Article = ArticleTypeEnum.None

                    ' List(true) - List anything in/on everything in the list (single level) - Default
                    ' List(false) - Do not list anything in/on
                    private bool bListInOn = true ' List any objects in or on anything in this list;
                    if (sFunction = "Name" || sArgsTest.Contains("false") || sArgsTest.Contains("0"))
                    {
                        bListInOn = False
                    }

                    private bool bForcePronoun = false;
#if Runner
                    private PronounEnum ePronoun = PronounEnum.Subjective;
                    If sArgsTest.Contains("none") Then ePronoun = PronounEnum.None
                    If sArgsTest.Contains("force") Then bForcePronoun = true
                    If sArgsTest.Contains("objective") || sArgsTest.Contains("object") || sArgsTest.Contains("target") Then ePronoun = PronounEnum.Objective
                    If sArgsTest.Contains("possessive") || sArgsTest.Contains("possess") Then ePronoun = PronounEnum.Possessive
                    If sArgsTest.Contains("reflective") || sArgsTest.Contains("reflect") Then ePronoun = PronounEnum.Reflective
#else
                    private bool ePronoun = false;
#endif

                    private string sList = "";
                    private int i = 0;
                    if (lst IsNot null)
                    {
                        For Each oItem As clsItem In lst
                            i += 1;
                            if (sSeparator = vbCrLf)
                            {
                                If i > 1 Then sList &= sSeparator
                            Else
                                If i > 1 && i < lst.Count Then sList &= ", "
                                If lst.Count > 1 && i = lst.Count Then sList &= sSeparator
                            }

                            if (TypeOf oItem Is clsObject)
                            {
                                sList &= (clsObject)(oItem).FullName(Article);
                                If bListInOn && (clsObject)(oItem).Children(clsObject.WhereChildrenEnum.InsideOrOnObject).Count > 0 Then sList &= ".  " + ChopLast((clsObject)(oItem).DisplayObjectChildren)
                            ElseIf TypeOf oItem == clsCharacter Then
                                ' List(definite/the) - List the objects names - Default
                                ' List(indefinite) - List a/an object
                                Article = ArticleTypeEnum.Indefinite ' opposite default from objects
                                If sArgsTest.Contains("definite") Then Article = ArticleTypeEnum.Definite
                                sList &= (clsCharacter)(oItem).Name(ePronoun, , , Article, bForcePronoun);
                            ElseIf TypeOf oItem == clsLocation Then
                                sList &= (clsLocation)(oItem).ShortDescription.ToString;
                            }
                        Next;
                    ElseIf lstDirs != null Then
                        For Each oDir As DirectionsEnum In lstDirs
                            i += 1;
                            If i > 1 && i < lstDirs.Count Then sList &= ", "
                            If lstDirs.Count > 1 && i = lstDirs.Count Then sList &= sSeparator
                            sList &= LCase(DirectionName(oDir));
                        Next;
                    }
                    If sList = "" Then sList = "nothing"
                    return sList;

                case "Parent":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    private New List<string> lstKeys;

                    For Each oItem As clsItemWithProperties In lst
                        private string sParent = oItem.Parent;
                        if (sParent <> "")
                        {
                            if (Not lstKeys.Contains(sParent))
                            {
                                private clsItemWithProperties oItemNew = CType(Adventure.dictAllItems(sParent), clsItemWithProperties);
                                lstKeys.Add(sParent);
                                lstNew.Add(oItemNew);
                            }
                        }
                    Next;

                    if (sRemainder <> "")
                    {
                        return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    }

                case "Children":
                    {
                    private New List<clsItemWithProperties> lstNew;

                    For Each oItem As clsItemWithProperties In lst
                        switch (true)
                        {
                            case TypeOf oItem Is clsObject:
                                {
                                ob = CType(oItem, clsObject)
                                switch (sArgs.ToLower.Replace(" ", ""))
                                {
                                    case "":
                                    case "all":
                                    case "onandin":
                                    case "all:
                                    case onandin":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "characters:
                                    case in":
                                        {
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "characters:
                                    case on":
                                        {
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "characters:
                                    case onandin":
                                    case "characters":
                                        {
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "in":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "objects:
                                    case in":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                    case "objects:
                                    case on":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.OnObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                    case "objects:
                                    case onandin":
                                    case "objects":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                }

                            case TypeOf oItem Is clsCharacter:
                                {
                                ' No real reason we couldn't give Children from a Character

                        }
                    Next;

                    if (sRemainder <> "" || lstNew.Count > 0)
                    {
                        return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    }

                case "Contents":
                    {
                    private New List<clsItemWithProperties> lstNew;

                    For Each oItem As clsItemWithProperties In lst
                        switch (true)
                        {
                            case TypeOf oItem Is clsObject:
                                {
                                ob = CType(oItem, clsObject)
                                switch (sArgs.ToLower)
                                {
                                    case "":
                                    case "all":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "characters":
                                        {
                                        For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                            lstNew.Add(oCh);
                                        Next;
                                    case "objects":
                                        {
                                        For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                            lstNew.Add(oOb);
                                        Next;
                                }
                        }
                    Next;

                    'If sRemainder <> "" OrElse lstNew.Count > 0 Then
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    'End If

                case "Objects":
                    {
                    private New List<clsItemWithProperties> lstNew;

                    For Each oItem As clsItemWithProperties In lst
                        switch (true)
                        {
                            case TypeOf oItem Is clsLocation:
                                {
                                loc = CType(oItem, clsLocation)
                                For Each obLoc As clsObject In loc.ObjectsInLocation.Values
                                    lstNew.Add(obLoc);
                                Next;
                            default:
                                {
                                TODO();
                        }
                    Next;

                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);

                default:
                    {
                    if (Adventure.htblAllProperties.ContainsKey(sFunction))
                    {
                        private New List<clsItemWithProperties> lstNew;
                        private New List<string> lstKeys;
                        private int iTotal = 0;
                        private string sResult = "";
                        private bool bIntResult = false;
                        private bool bPropertyFound = false;
                        private string sNewPropertyKey = null;

                        For Each oItem As clsItemWithProperties In lst
                            if (oItem.HasProperty(sFunction))
                            {
                                bPropertyFound = True
                                private clsProperty p = oItem.GetProperty(sFunction);

                                private bool bValueOK = true;
                                if (sArgs <> "")
                                {
                                    bValueOK = False
                                    If sArgs.Contains(".") Then sArgs = ReplaceFunctions(sArgs)
                                    if (sArgs.Contains("+") || sArgs.Contains("-"))
                                    {
                                        private string sArgsNew = EvaluateExpression(sArgs);
                                        If sArgsNew != null Then sArgs = sArgsNew
                                    }
                                    switch (p.Type)
                                    {
                                        case clsProperty.PropertyTypeEnum.ValueList:
                                            {
                                            private int iCheckValue = 0;
                                            if (IsNumeric(sArgs))
                                            {
                                                iCheckValue = SafeInt(sArgs)
                                            Else
                                                If p.ValueList.ContainsKey(sArgs) Then iCheckValue = p.ValueList(sArgs)
                                            }
                                            bValueOK = p.IntData = iCheckValue
                                            bInt = True
                                        case clsProperty.PropertyTypeEnum.Integer:
                                            {
                                            private int iCheckValue = 0;
                                            If IsNumeric(sArgs) Then iCheckValue = SafeInt(sArgs)
                                            bValueOK = p.IntData = iCheckValue
                                            bInt = True
                                        case clsProperty.PropertyTypeEnum.StateList:
                                            {
                                            bValueOK = p.Value.ToLower = sArgs.ToLower
                                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                                            {
                                            switch (sArgs.ToLower)
                                            {
                                                case "false":
                                                case "0":
                                                    {
                                                    bValueOK = False
                                                default:
                                                    {
                                                    bValueOK = True ' p.Value = "True" ' True
                                            }
                                            bInt = True
                                        default:
                                            {
                                            TODO("Property filter check not yet implemented for " + p.Type.ToString);
                                    }
                                }

                                if (bValueOK)
                                {
                                    switch (p.Type)
                                    {
                                        case clsProperty.PropertyTypeEnum.CharacterKey:
                                            {
                                            private clsCharacter chP = Adventure.htblCharacters(p.Value);
                                            if (Not lstKeys.Contains(chP.Key))
                                            {
                                                lstKeys.Add(chP.Key);
                                                lstNew.Add(chP);
                                            }
                                        case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                            {
                                            For Each sItem As String In Adventure.htblGroups(p.Value).arlMembers
                                                if (Not lstKeys.Contains(sItem))
                                                {
                                                    private clsItemWithProperties oItemNew = CType(Adventure.dictAllItems(sItem), clsItemWithProperties);
                                                    lstKeys.Add(oItemNew.Key);
                                                    lstNew.Add(oItemNew);
                                                }
                                            Next;
                                        case clsProperty.PropertyTypeEnum.LocationKey:
                                            {
                                            private clsItemWithProperties oItemNew = Adventure.htblLocations(p.Value);
                                            if (Not lstKeys.Contains(oItemNew.Key))
                                            {
                                                lstKeys.Add(oItemNew.Key);
                                                lstNew.Add(oItemNew);
                                            }
                                        case clsProperty.PropertyTypeEnum.ObjectKey:
                                            {
                                            private clsItemWithProperties oItemNew = Adventure.htblObjects(p.Value);
                                            if (Not lstKeys.Contains(oItemNew.Key))
                                            {
                                                lstKeys.Add(oItemNew.Key);
                                                lstNew.Add(oItemNew);
                                            }
                                        case clsProperty.PropertyTypeEnum.Integer:
                                        case clsProperty.PropertyTypeEnum.ValueList:
                                        case clsProperty.PropertyTypeEnum.StateList:
                                            {
                                            lstNew.Add(oItem);
                                            sNewPropertyKey = sFunction
                                            bInt = p.Type = clsProperty.PropertyTypeEnum.Integer OrElse p.Type = clsProperty.PropertyTypeEnum.ValueList
                                            'iTotal += p.IntData
                                            'bIntResult = True
                                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                                            {
                                            ' Selection Only property to further reduce list
                                            lstNew.Add(oItem);
                                            bInt = True
                                        case clsProperty.PropertyTypeEnum.Text:
                                            {
                                            if (sResult <> "")
                                            {
                                                If oItem == lst(lst.Count - 1) Then sResult &= " and " Else sResult &= ", "
                                            }
                                            sResult &= p.Value;
                                    }
                                }
                            Else
                                if (Adventure.htblAllProperties.ContainsKey(sFunction))
                                {
                                    private clsProperty p = Adventure.htblAllProperties(sFunction);
                                    private bool bValueOK = false ' Because this is equiv of arg = (true);
                                    if (sArgs <> "")
                                    {
                                        bValueOK = False
                                        If sArgs.Contains(".") Then sArgs = ReplaceFunctions(sArgs)
                                        if (sArgs.Contains("+") || sArgs.Contains("-"))
                                        {
                                            private string sArgsNew = EvaluateExpression(sArgs);
                                            If sArgsNew != null Then sArgs = sArgsNew
                                        }
                                        switch (p.Type)
                                        {
                                            ' Opposite of above, since this item _doesn't_ contain this property
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                switch (sArgs.ToLower)
                                                {
                                                    case "false":
                                                    case "0":
                                                        {
                                                        bValueOK = True
                                                    default:
                                                        {
                                                        bValueOK = False
                                                }
                                                bInt = True
                                            case clsProperty.PropertyTypeEnum.StateList:
                                                {
                                                bValueOK = False ' Since we don't have the property
                                            default:
                                                {
                                                TODO("Property filter check not yet implemented for " + p.Type.ToString);
                                        }
                                    }
                                    if (bValueOK)
                                    {
                                        switch (p.Type)
                                        {
                                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                                {
                                                ' Selection Only property to further reduce list
                                                lstNew.Add(oItem);
                                        }
                                    }
                                }
                            }
                        Next;

                        if (Not bPropertyFound)
                        {
                            switch (Adventure.htblAllProperties(sFunction).Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                    {
                                    bIntResult = True
                                    bInt = True
                            }
                        }

                        if (sRemainder <> "" || (lstNew.Count > 0 && Not bIntResult))
                        {
                            return ReplaceOOProperty(sRemainder, lst:=lstNew, sPropertyKey:=sNewPropertyKey, bInt:=bInt);
                        ElseIf bIntResult Then
                            return iTotal.ToString;
                        Else
                            return sResult;
                        }
                    }
            }

        ElseIf ob != null Then
            switch (sFunction)
            {
                case "Adjective":
                case "Prefix":
                    {
                    return ob.Prefix;

                case "Article":
                    {
                    return ob.Article;

                case "Children":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    switch (sArgs.ToLower.Replace(" ", ""))
                    {
                        case "":
                        case "all":
                        case "onandin":
                        case "all:
                        case onandin":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "characters:
                        case in":
                            {
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "characters:
                        case on":
                            {
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "characters:
                        case onandin":
                        case "characters":
                            {
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "in":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "objects:
                        case in":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                        case "objects:
                        case on":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.OnObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                        case "objects:
                        case onandin":
                        case "objects":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                    }
                    'If sRemainder <> "" OrElse lstNew.Count > 0 Then
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    'End If

                case "Contents":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    switch (sArgs.ToLower)
                    {
                        case "":
                        case "all":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "characters":
                            {
                            For Each oCh As clsCharacter In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Values
                                lstNew.Add(oCh);
                            Next;
                        case "objects":
                            {
                            For Each oOb As clsObject In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Values
                                lstNew.Add(oOb);
                            Next;
                    }
                    'If sRemainder <> "" OrElse lstNew.Count > 0 Then
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    'End If

                case "Count":
                    {
                    bInt = True
                    return "1";

                case "Description":
                    {
                    return ob.Description.ToString;

                case "Location":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    For Each sLocKey As String In ob.LocationRoots.Keys
                        private clsLocation oLoc = Adventure.htblLocations(sLocKey);
                        lstNew.Add(oLoc);
                    Next;
                    'If sRemainder <> "" Then
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
                    'End If

                case "Name":
                case "List":
                    {
                    ' Name(definite/the) - List the objects names - Default
                    ' Name(indefinite) - List a/an object
                    private ArticleTypeEnum Article = ArticleTypeEnum.Definite;
                    If sArgs.ToLower.Contains("indefinite") Then Article = ArticleTypeEnum.Indefinite
                    If sArgs.ToLower.Contains("none") Then Article = ArticleTypeEnum.None

                    return ob.FullName(Article);

                case "Noun":
                    {
                    return ob.arlNames(0);

                case "Parent":
                    {
                    private string sParent = ob.Parent;
                    private clsObject oParentOb = null;
                    private clsCharacter oParentCh = null;
                    private clsLocation oParentLoc = null;
                    Adventure.htblObjects.TryGetValue(sParent, oParentOb);
                    Adventure.htblCharacters.TryGetValue(sParent, oParentCh);
                    Adventure.htblLocations.TryGetValue(sParent, oParentLoc);
                    return ReplaceOOProperty(sRemainder, oParentOb, oParentCh, oParentLoc, bInt:=bInt);

                case "":
                    {
                    return ob.Key;

                default:
                    {
                    private clsProperty p = null;
                    private clsObject oOb = null;
                    private clsCharacter oCh = null;
                    private clsLocation oLoc = null;
                    private List<clsItemWithProperties> lstNew = null;

                    if (ob.htblProperties.TryGetValue(sFunction, p))
                    {
                        switch (p.Type)
                        {
                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                {
                                oCh = Adventure.htblCharacters(p.Value)
                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                {
                                If lstNew == null Then lstNew = New List(Of clsItemWithProperties)
                                For Each sItem As String In Adventure.htblGroups(p.Value).arlMembers
                                    private clsItemWithProperties oItem = CType(Adventure.dictAllItems(sItem), clsItemWithProperties);
                                    lstNew.Add(oItem);
                                Next;
                            case clsProperty.PropertyTypeEnum.LocationKey:
                                {
                                oLoc = Adventure.htblLocations(p.Value)
                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                {
                                oOb = Adventure.htblObjects(p.Value)
                            case clsProperty.PropertyTypeEnum.Integer:
                            case clsProperty.PropertyTypeEnum.ValueList:
                                {
                                return p.IntData.ToString;
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                return "1";
                            case clsProperty.PropertyTypeEnum.Text:
                            case clsProperty.PropertyTypeEnum.StateList:
                                {
                                return p.Value;
                        }
                    Else
                        if (Adventure.htblObjectProperties.ContainsKey(sFunction))
                        {
                            ' Ok, item doesn't have property.  Give it a default
                            switch (Adventure.htblObjectProperties(sFunction).Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    bInt = True
                                    return "0";
                            }
                        }
                    }
                    if (sRemainder <> "")
                    {
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    ElseIf oLoc != null Then
                        return oLoc.Key;
                    ElseIf oOb != null Then
                        return oOb.Key;
                    ElseIf oCh != null Then
                        return oCh.Key;
                    ElseIf lstNew != null && lstNew.Count > 0 Then
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    }

            }

        ElseIf ch != null Then
            switch (sFunction)
            {
                ' Case "Children"
                '   No real reason we couldn't do this.  If we do, remember to add to List above

                case "Count":
                    {
                    bInt = True
                    return "1";

                case "Descriptor":
                    {
                    return ch.Descriptor.ToString;

                case "Description":
                    {
                    return ch.Description.ToString;

                case "Exits":
                    {
                    private New List<DirectionsEnum> lstNew;

                    for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest ' Adventure.iCompassPoints; d++)
                    {
#if Runner
                        if (Adventure.Player.HasRouteInDirection(d, false, Adventure.Player.Location.LocationKey))
                        {
                            lstNew.Add(d);
                        }
#endif
                    Next;
                    return ReplaceOOProperty(sRemainder, , ch, , , lstNew, bInt:=bInt);

                case "Held":
                    {
                    private New List<clsItemWithProperties> lstNew;
#if Runner
                    switch (sArgs.ToLower)
                    {
                        case "":
                        case "true":
                        case "1":
                        case "-1":
                            {
                            For Each obHeld As clsObject In ch.HeldObjects(True).Values
                                lstNew.Add(obHeld);
                            Next;
                        case "false":
                        case "0":
                            {
                            For Each obHeld As clsObject In ch.HeldObjects(False).Values
                                lstNew.Add(obHeld);
                            Next;
                    }
#endif
                    return ReplaceOOProperty(sRemainder, , ch, , lstNew, bInt:=bInt);

                case "Location":
                    {
                    private string sLoc = ch.Location.LocationKey;
                    private clsLocation oLoc = null;
                    Adventure.htblLocations.TryGetValue(sLoc, oLoc);
                    return ReplaceOOProperty(sRemainder, , , oLoc, bInt:=bInt);

                case "Name":
                    {
                    private bool bForcePronoun = false;

                    private bool bTheNames = false ' opposite default from objects;
                    private ArticleTypeEnum Article = ArticleTypeEnum.Definite;
                    private bool bExplicitArticle = false;
#if Runner
                    private PronounEnum ePronoun = PronounEnum.Subjective;
                    private string sArgsTest = sArgs.ToLower;

                    if (sArgsTest.Contains("none"))
                    {
                        ' Could mean either article or pronoun
                        if (sArgsTest.Contains("definite") || sArgsTest.Contains("indefinite"))
                        {
                            ePronoun = PronounEnum.None
                        ElseIf sArgsTest.Contains("force") || sArgsTest.Contains("objective") || sArgsTest.Contains("possessive") || sArgsTest.Contains("reflective") Then
                            Article = ArticleTypeEnum.None
                        Else
                            ' If only None is specified, assume they mean pronouns, as less likely they'll disable articles on character names
                            ePronoun = PronounEnum.None
                        }
                    }
                    If sArgsTest.Contains("force") Then bForcePronoun = true
                    If sArgsTest.Contains("objective") || sArgsTest.Contains("object") || sArgsTest.Contains("target") Then ePronoun = PronounEnum.Objective
                    If sArgsTest.Contains("possessive") || sArgsTest.Contains("possess") Then ePronoun = PronounEnum.Possessive
                    If sArgsTest.Contains("reflective") || sArgsTest.Contains("reflect") Then ePronoun = PronounEnum.Reflective
                    'If sArgsTest.Contains("definite") Then Article = ArticleTypeEnum.Definite
                    if (ContainsWord(sArgsTest, "definite"))
                    {
                        Article = ArticleTypeEnum.Definite
                        bExplicitArticle = True
                    }
                    if (ContainsWord(sArgsTest, "indefinite"))
                    {
                        Article = ArticleTypeEnum.Indefinite
                        bExplicitArticle = True
                    }
#else
                    private bool ePronoun = false;
#endif
                    ' List(definite/the) - List the objects names - Default
                    ' List(indefinite) - List a/an object

                    return ch.Name(ePronoun, , , Article, bForcePronoun, bExplicitArticle);

                case "Parent":
                    {
                    private string sParent = ch.Parent;
                    private clsObject oParentOb = null;
                    private clsCharacter oParentCh = null;
                    private clsLocation oParentLoc = null;
                    Adventure.htblObjects.TryGetValue(sParent, oParentOb);
                    Adventure.htblCharacters.TryGetValue(sParent, oParentCh);
                    Adventure.htblLocations.TryGetValue(sParent, oParentLoc);
                    return ReplaceOOProperty(sRemainder, oParentOb, oParentCh, oParentLoc, bInt:=bInt);

                case "ProperName":
                    {
                    return ch.ProperName;

                case "Worn":
                    {
                    private New List<clsItemWithProperties> lstNew;
#if Runner
                    switch (sArgs.ToLower)
                    {
                        case "":
                        case "true":
                        case "1":
                        case "-1":
                            {
                            For Each obWorn As clsObject In ch.WornObjects(True).Values
                                lstNew.Add(obWorn);
                            Next;
                        case "false":
                        case "0":
                            {
                            For Each obWorn As clsObject In ch.WornObjects(False).Values
                                lstNew.Add(obWorn);
                            Next;
                    }
#endif
                    return ReplaceOOProperty(sRemainder, , ch, , lstNew, bInt:=bInt);

                case "WornAndHeld":
                    {
                    private New List<clsItemWithProperties> lstNew;
#if Runner
                    switch (sArgs.ToLower)
                    {
                        case "":
                        case "true":
                        case "1":
                        case "-1":
                            {
                            For Each obWorn As clsObject In ch.WornObjects(True).Values
                                lstNew.Add(obWorn);
                            Next;
                            For Each obHeld As clsObject In ch.HeldObjects(True).Values
                                lstNew.Add(obHeld);
                            Next;
                        case "false":
                        case "0":
                            {
                            For Each obWorn As clsObject In ch.WornObjects(False).Values
                                lstNew.Add(obWorn);
                            Next;
                            For Each obHeld As clsObject In ch.HeldObjects(False).Values
                                lstNew.Add(obHeld);
                            Next;
                    }
#endif
                    return ReplaceOOProperty(sRemainder, , ch, , lstNew, bInt:=bInt);

                case "":
                    {
                    return ch.Key;

                default:
                    {
                    private clsProperty p = null;
                    private clsObject oOb = null;
                    private clsCharacter oCh = null;
                    private clsLocation oLoc = null;
                    private List<clsItemWithProperties> lstNew = null;

                    if (ch.htblProperties.TryGetValue(sFunction, p))
                    {
                        switch (p.Type)
                        {
                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                {
                                oCh = Adventure.htblCharacters(p.Value)
                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                {
                                lstNew = New List(Of clsItemWithProperties)
                                For Each sItem As String In Adventure.htblGroups(p.Value).arlMembers
                                    private clsItemWithProperties oItem = CType(Adventure.dictAllItems(sItem), clsItemWithProperties);
                                    lstNew.Add(oItem);
                                Next;
                            case clsProperty.PropertyTypeEnum.LocationKey:
                                {
                                oLoc = Adventure.htblLocations(p.Value)
                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                {
                                oOb = Adventure.htblObjects(p.Value)
                            case clsProperty.PropertyTypeEnum.Integer:
                            case clsProperty.PropertyTypeEnum.ValueList:
                                {
                                bInt = True
                                return p.IntData.ToString;
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                bInt = True
                                return "1";
                            case clsProperty.PropertyTypeEnum.Text:
                            case clsProperty.PropertyTypeEnum.StateList:
                                {
                                return p.Value;
                        }
                    Else
                        if (Adventure.htblCharacterProperties.ContainsKey(sFunction))
                        {
                            ' Ok, item doesn't have property.  Give it a default
                            switch (Adventure.htblCharacterProperties(sFunction).Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    return "0";
                            }
                        }
                        '' Ok, item doesn't have property.  Give it a default
                        'If Adventure.htblAllProperties.TryGetValue(sNextKey, p) Then
                        '    Select Case p.Type
                        '        Case clsProperty.PropertyTypeEnum.CharacterKey, clsProperty.PropertyTypeEnum.LocationGroupKey, clsProperty.PropertyTypeEnum.LocationKey, clsProperty.PropertyTypeEnum.ObjectKey
                        '            bList = True
                        '        Case clsProperty.PropertyTypeEnum.Integer, clsProperty.PropertyTypeEnum.ValueList
                        '            bReturnInt = True
                        '        Case clsProperty.PropertyTypeEnum.SelectionOnly
                        '            bReturnInt = True
                        '        Case clsProperty.PropertyTypeEnum.Text, clsProperty.PropertyTypeEnum.StateList
                        '    End Select
                        '    Else
                        '    ' Duff property
                        'End If
                    }
                    if (sRemainder <> "")
                    {
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    ElseIf oLoc != null Then
                        return oLoc.Key;
                    ElseIf oOb != null Then
                        return oOb.Key;
                    ElseIf oCh != null Then
                        return oCh.Key;
                    ElseIf lstNew != null && lstNew.Count > 0 Then
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    }

            }

        ElseIf loc != null Then
            switch (sFunction)
            {
                case "Characters":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    For Each chLoc As clsCharacter In loc.CharactersVisibleAtLocation.Values
                        lstNew.Add(chLoc);
                    Next;
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);

                case "Contents":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    switch (sArgs.ToLower)
                    {
                        case "":
                        case "all":
                            {
                            For Each oOb As clsObject In loc.ObjectsInLocation.Values
                                lstNew.Add(oOb);
                            Next;
                            For Each oCh As clsCharacter In loc.CharactersDirectlyInLocation.Values
                                lstNew.Add(oCh);
                            Next;
                        case "characters":
                            {
                            For Each oCh As clsCharacter In loc.CharactersDirectlyInLocation.Values
                                lstNew.Add(oCh);
                            Next;
                        case "objects":
                            {
                            For Each oOb As clsObject In loc.ObjectsInLocation.Values
                                lstNew.Add(oOb);
                            Next;
                    }
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);

                case "Count":
                    {
                    bInt = True
                    return "1";

                case "Description":
                case LONGLOCATIONDESCRIPTION:
                    {
                    private string sResult = loc.ViewLocation;
                    If sResult = "" Then sResult = "There is nothing of interest here."
                    return sResult;

                case "Exits":
                    {
                    private New List<DirectionsEnum> lstNew;

                    for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest ' Adventure.iCompassPoints; d++)
                    {
#if Runner
                        if (loc.arlDirections(d).LocationKey <> "")
                        {
                            lstNew.Add(d);
                        }
#endif
                    Next;
                    return ReplaceOOProperty(sRemainder, , ch, , , lstNew, bInt:=bInt);

                case "LocationTo":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    private clsLocation locTo = null;

                    For Each sDir As String In sArgs.ToLower.Split("|"c)
                        For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                            if (sDir = d.ToString.ToLower)
                            {
                                private string sLocTo = loc.arlDirections(d).LocationKey;
                                if (sLocTo IsNot null)
                                {
                                    locTo = Nothing
                                    Adventure.htblLocations.TryGetValue(sLocTo, locTo);
                                    if (locTo IsNot null)
                                    {
                                        lstNew.Add(locTo);
                                    }
                                }
                                Exit For;
                            }
                        Next;
                    Next;

                    if (lstNew.Count = 1)
                    {
                        locTo = CType(lstNew(0), clsLocation)
                        lstNew = Nothing
                    }
                    return ReplaceOOProperty(sRemainder, , , locTo, lstNew, bInt:=bInt);
                    'Dim sLocTo As String = ""

                    'For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                    '    If sArgs.ToLower = d.ToString.ToLower Then
                    '        sLocTo = loc.arlDirections(d).LocationKey
                    '        If sLocTo Is Nothing Then sLocTo = ""
                    '        Exit For
                    '    End If
                    'Next

                    'Dim locTo As clsLocation = Nothing
                    'Adventure.htblLocations.TryGetValue(sLocTo, locTo)
                    'If locTo IsNot Nothing Then
                    '    Return ReplaceOOProperty(sRemainder, , , locTo)
                    'Else
                    '    Return ""
                    'End If

                case "Name":
                case SHORTLOCATIONDESCRIPTION:
                    {
                    return loc.ShortDescription.ToString;

                case "Objects":
                    {
                    private New List<clsItemWithProperties> lstNew;
                    For Each obLoc As clsObject In loc.ObjectsInLocation.Values
                        lstNew.Add(obLoc);
                    Next;
                    return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);

                case "":
                    {
                    return loc.Key;

                default:
                    {
                    private clsProperty p = null;
                    private clsObject oOb = null;
                    private clsCharacter oCh = null;
                    private clsLocation oLoc = null;
                    private List<clsItemWithProperties> lstNew;

                    if (loc.htblProperties.TryGetValue(sFunction, p))
                    {
                        switch (p.Type)
                        {
                            case clsProperty.PropertyTypeEnum.CharacterKey:
                                {
                                oCh = Adventure.htblCharacters(p.Value)
                            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                {
                                lstNew = New List(Of clsItemWithProperties)
                                For Each sItem As String In Adventure.htblGroups(p.Value).arlMembers
                                    private clsItemWithProperties oItem = CType(Adventure.dictAllItems(sItem), clsItemWithProperties);
                                    lstNew.Add(oItem);
                                Next;
                            case clsProperty.PropertyTypeEnum.LocationKey:
                                {
                                oLoc = Adventure.htblLocations(p.Value)
                            case clsProperty.PropertyTypeEnum.ObjectKey:
                                {
                                oOb = Adventure.htblObjects(p.Value)
                            case clsProperty.PropertyTypeEnum.Integer:
                            case clsProperty.PropertyTypeEnum.ValueList:
                                {
                                bInt = True
                                return p.IntData.ToString;
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                bInt = True
                                return "1";
                            case clsProperty.PropertyTypeEnum.Text:
                            case clsProperty.PropertyTypeEnum.StateList:
                                {
                                return p.Value;
                        }
                    Else
                        if (Adventure.htblLocationProperties.ContainsKey(sFunction))
                        {
                            ' Ok, item doesn't have property.  Give it a default
                            switch (Adventure.htblLocationProperties(sFunction).Type)
                            {
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                case clsProperty.PropertyTypeEnum.SelectionOnly:
                                    {
                                    return "0";
                            }
                        }
                    }
                    if (sRemainder <> "")
                    {
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    ElseIf oLoc != null Then
                        return oLoc.Key;
                    ElseIf oOb != null Then
                        return oOb.Key;
                    ElseIf oCh != null Then
                        return oCh.Key;
                    ElseIf lstNew.Count > 0 Then
                        return ReplaceOOProperty(sRemainder, oOb, oCh, oLoc, lstNew, bInt:=bInt);
                    }

            }

        ElseIf evt != null Then
            switch (sFunction)
            {
                case "Length":
                    {
                    return evt.Length.Value.ToString;
#if Runner
                case "Position":
                    {
                    return evt.TimerFromStartOfEvent.ToString;
#endif
                case "":
                    {
                    return evt.Key;

            }

        Else
            if (sFunction.Contains("|"))
            {
                private New List<clsItemWithProperties> lstNew;
                For Each sItem As String In sFunction.Split("|"c)
                    private clsItemWithProperties oItem = CType(Adventure.dictAllItems(sItem), clsItemWithProperties);
                    lstNew.Add(oItem);
                Next;
                return ReplaceOOProperty(sRemainder, , , , lstNew, bInt:=bInt);
            Else
                if (Adventure.AllKeys.Contains(sFunction))
                {
                    private List<clsItemWithProperties> lstNew = null;
                    if (Adventure.htblObjects.ContainsKey(sFunction))
                    {
                        ob = Adventure.htblObjects(sFunction)
                    ElseIf Adventure.htblCharacters.ContainsKey(sFunction) Then
                        ch = Adventure.htblCharacters(sFunction)
                    ElseIf Adventure.htblLocations.ContainsKey(sFunction) Then
                        loc = Adventure.htblLocations(sFunction)
                    ElseIf Adventure.htblEvents.ContainsKey(sFunction) Then
                        evt = Adventure.htblEvents(sFunction)
                    ElseIf Adventure.htblGroups.ContainsKey(sFunction) Then
                        private clsGroup grp = Adventure.htblGroups(sFunction);
                        lstNew = New List(Of clsItemWithProperties)
                        For Each sMember As String In grp.arlMembers
                            private clsItemWithProperties itm = CType(Adventure.dictAllItems(sMember), clsItemWithProperties);
                            lstNew.Add(itm);
                        Next;
                    }

                    return ReplaceOOProperty(sRemainder, ob, ch, loc, lstNew, , evt, bInt:=bInt);
                Else
                    For Each d As DirectionsEnum In [Enum].GetValues(GetType(DirectionsEnum))
                        if (d.ToString = sFunction)
                        {
                            private New List<DirectionsEnum> NewDir;
                            NewDir.Add(d);
                            return ReplaceOOProperty(sRemainder, lstDirs:=NewDir, bInt:=bInt);
                        }
                    Next;
                }
            }
        }

        return "#*!~#";

    }

#if 0
    private string ReplaceOOPropertyOld(StringArrayList lstKeys, string sProperty, bool bList)
    {

        private New System.Text.StringBuilder sResults;
        private int iResult = 0;
        private bool bReturnInt = false;
        private New System.Text.StringBuilder sAppend;
        private New System.Text.RegularExpressions.Regex("(?<nextkey>[A-Za-z]([\w\|_-])*(\([A-Za-z ,]+?\))?)(?<followingkeys>\.[A-Za-z]([\w\|_-])*(\([A-Za-z ,]+?\))?)*") re;
        private string sMatch = "";
        private string sNextKey = "";
        private New StringArrayList lstSubKeys;

        if (re.IsMatch(sProperty))
        {
            sMatch = re.Match(sProperty).Value
            sNextKey = re.Match(sProperty).Groups("nextkey").Value
        }

        For Each sKey As String In lstKeys
            'Dim re As New System.Text.RegularExpressions.Regex("(?<nextkey>[A-Za-z]([\w\|_-])*(\([A-Za-z ,]+?\))?)(?<followingkeys>\.[A-Za-z]([\w\|_-])*(\([A-Za-z ,]+?\))?)*")
            if (sMatch <> "" Then ' re.IsMatch(sProperty))
            {
                'Dim sMatch As String = re.Match(sProperty).Value
                'Dim sNextKey As String = re.Match(sProperty).Groups("nextkey").Value
                'Dim lstSubKeys As New StringArrayList
                '        Dim bList As Boolean = False

                private clsItemWithProperties item = null;
                private clsObject ob = null;
                private clsCharacter ch = null;
                private clsLocation loc = null;

                if (Adventure.htblObjects.TryGetValue(sKey, ob))
                {
                    item = ob
                Else
                    if (Adventure.htblCharacters.TryGetValue(sKey, ch))
                    {
                        item = ch
                    Else
                        if (Adventure.htblLocations.TryGetValue(sKey, loc))
                        {
                            item = loc
                        }
                    }
                }

                private string sFunction = sNextKey.Replace(" ", "");
                private string sArgs = "";
                if (sFunction.Contains("(") && sFunction.Contains(")"))
                {
                    if (sFunction.LastIndexOf(")") > sFunction.IndexOf("("))
                    {
                        sArgs = sFunction
                        sArgs = sArgs.Substring(sArgs.IndexOf("(") + 1, sArgs.LastIndexOf(")") - sArgs.IndexOf("(") - 1).ToLower
                    }
                    sFunction = sFunction.Substring(0, sFunction.Length - sArgs.Length - 2)
                }

                switch (sFunction)
                {
                    case "Children":
                        {
                        bList = True
                        if (ob IsNot null)
                        {
                            switch (sArgs)
                            {
                                case "":
                                case "all":
                                case "onandin":
                                case "all:
                                case onandin":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "characters:
                                case in":
                                    {
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "characters:
                                case on":
                                    {
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "characters:
                                case onandin":
                                    {
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideOrOnObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "in":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "objects:
                                case in":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "objects:
                                case on":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.OnObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "objects:
                                case onandin":
                                case "objects":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }
                        }

                    case "Contents":
                        {
                        bList = True
                        if (ob IsNot null)
                        {
                            switch (sArgs)
                            {
                                case "":
                                case "all":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "characters":
                                    {
                                    For Each sChild As String In ob.ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "objects":
                                    {
                                    For Each sChild As String In ob.Children(clsObject.WhereChildrenEnum.InsideObject, True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }
                        ElseIf loc != null Then
                            switch (sArgs)
                            {
                                case "":
                                case "all":
                                    {
                                    For Each sChild As String In loc.ObjectsInLocation.Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In loc.CharactersDirectlyInLocation.Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "characters":
                                    {
                                    For Each sChild As String In loc.CharactersDirectlyInLocation.Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "objects":
                                    {
                                    For Each sChild As String In loc.ObjectsInLocation.Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }
                        }

                    case "Count":
                        {
                        iResult += 1;
                        bReturnInt = True

                    case "Description":
                    case LONGLOCATIONDESCRIPTION:
                        {
                        if (ob IsNot null)
                        {
                            sResults.Append(ob.Description.ToString);
                        ElseIf ch != null Then
                            sResults.Append(ch.Description.ToString);
                        ElseIf loc != null Then
                            private string sResult = loc.ViewLocation;
                            If sResult = "" Then sResult = "There is nothing of interest here."
                            sResults.Append(sResult);
                        }

                    case "Exits":
                        {
#if Runner
                        if (loc IsNot null)
                        {
                            for (DirectionsEnum d = DirectionsEnum.North; d <= DirectionsEnum.NorthWest ' Adventure.iCompassPoints; d++)
                            {
                                if (Adventure.Player.HasRouteInDirection(d, false, Adventure.Player.Location.LocationKey))
                                {
                                    lstSubKeys.Add(d.ToString);
                                }
                            Next;
                        }
#endif
                    case "Held":
                        {
#if Runner
                        if (ch IsNot null)
                        {
                            switch (sArgs)
                            {
                                case "":
                                case "true":
                                case "1":
                                case "-1":
                                    {
                                    For Each sChild As String In ch.HeldObjects(True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "false":
                                case "0":
                                    {
                                    For Each sChild As String In ch.HeldObjects(False).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }

                        }
#endif

                    case "Name":
                    case "List" ':
                    case "List(Or)":
                    case "List(true)":
                    case "List(1)":
                    case "List(false)":
                    case "List(0)":
                        {
                        ' List(and) - And separated list - Default
                        ' List(or) - Or separated list
                        private string sSeparator = " and ";
                        If sArgs.Contains("or") Then sSeparator = " or "

                        ' List(definite/the) - List the objects names - Default
                        ' List(indefinite) - List a/an object
                        private bool bTheNames = true;
                        If sArgs.Contains("indefinite") Then bTheNames = false

                        ' List(true) - List anything in/on everything in the list (single level) - Default
                        ' List(false) - Do not list anything in/on
                        private bool bListInOn = true ' List any objects in or on anything in this list;
                        if (sFunction = "Name" || sArgs.Contains("false") || sArgs.Contains("0"))
                        {
                            bListInOn = False
                        }

                        if (lstKeys.Count > 1 && sResults.ToString <> "")
                        {
                            If sKey = lstKeys(lstKeys.Count - 1) Then sResults.Append(sSeparator) Else sResults.Append(", ")
                        }
                        if (ob IsNot null)
                        {
                            sResults.Append(ob.FullName(bTheNames));
                            If bListInOn && ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject).Count > 0 Then sAppend.Append(".  " + ChopLast(ob.DisplayObjectChildren))
                        ElseIf ch != null Then
                            sResults.Append(ch.Name);
                        ElseIf loc != null Then
                            sResults.Append(loc.ShortDescription.ToString);
                        }

                    case "Objects":
                        {
                        if (loc IsNot null)
                        {
                            For Each obLoc As clsObject In loc.ObjectsInLocation.Values
                                lstSubKeys.Add(obLoc.Key);
                            Next;
                        }

                    case "Parent":
                        {
                        if (ob IsNot null)
                        {
                            If ob.Parent <> "" Then lstSubKeys.Add(ob.Parent)
                        ElseIf ch != null Then
                            If ch.Parent <> "" Then lstSubKeys.Add(ch.Parent)
                        }

                    case "PrevParent":
                        {
                        if (ob IsNot null)
                        {
                            If ob.Parent <> "" Then lstSubKeys.Add(ob.PrevParent)
                        ElseIf ch != null Then
                            If ch.Parent <> "" Then lstSubKeys.Add(ch.PrevParent)
                        }

                    case SHORTLOCATIONDESCRIPTION:
                        {
                        If loc != null Then sResults.Append(loc.ShortDescription.ToString)

                    case "Worn" ':
                    case "Worn(false)":
                    case "Worn(0)":
                    case "Worn(true)":
                    case "Worn(1)":
                        {
#if Runner
                        if (ch IsNot null)
                        {
                            switch (sArgs)
                            {
                                case "":
                                case "true":
                                case "1":
                                case "-1":
                                    {
                                    For Each sChild As String In ch.WornObjects(True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "false":
                                case "0":
                                    {
                                    For Each sChild As String In ch.WornObjects(False).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }
                            if (lstSubKeys.Count = 0)
                            {
                                sResults.Append("nothing");
                            }
                        }
#endif

                    case "WornAndHeld" ':
                    case "Worn(false)":
                    case "Worn(0)":
                    case "Worn(true)":
                    case "Worn(1)":
                        {
#if Runner
                        if (ch IsNot null)
                        {
                            switch (sArgs)
                            {
                                case "":
                                case "true":
                                case "1":
                                case "-1":
                                    {
                                    For Each sChild As String In ch.WornObjects(True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In ch.HeldObjects(True).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                case "false":
                                case "0":
                                    {
                                    For Each sChild As String In ch.WornObjects(False).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                                    For Each sChild As String In ch.HeldObjects(False).Keys
                                        lstSubKeys.Add(sChild);
                                    Next;
                            }
                            if (lstSubKeys.Count = 0)
                            {
                                sResults.Append("nothing");
                            }
                        }
#endif

                    default:
                        {
                        if (item IsNot null)
                        {
                            private clsProperty p = null;
                            if (item.htblProperties.TryGetValue(sNextKey, p))
                            {
                                switch (p.Type)
                                {
                                    case clsProperty.PropertyTypeEnum.CharacterKey:
                                    case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                    case clsProperty.PropertyTypeEnum.LocationKey:
                                    case clsProperty.PropertyTypeEnum.ObjectKey:
                                        {
                                        bList = True
                                        lstSubKeys.Add(p.Value);
                                    case clsProperty.PropertyTypeEnum.Integer:
                                    case clsProperty.PropertyTypeEnum.ValueList:
                                        {
                                        iResult += p.IntData;
                                        bReturnInt = True
                                    case clsProperty.PropertyTypeEnum.SelectionOnly:
                                        {
                                        iResult += 1;
                                        bReturnInt = True
                                    case clsProperty.PropertyTypeEnum.Text:
                                    case clsProperty.PropertyTypeEnum.StateList:
                                        {
                                        if (lstKeys.Count > 1 && sResults.ToString <> "")
                                        {
                                            If sKey = lstKeys(lstKeys.Count - 1) Then sResults.Append(" and ") Else sResults.Append(", ")
                                        }
                                        sResults.Append(p.Value);
                                }
                            Else
                                ' Ok, item doesn't have property.  Give it a default
                                if (Adventure.htblAllProperties.TryGetValue(sNextKey, p))
                                {
                                    switch (p.Type)
                                    {
                                        case clsProperty.PropertyTypeEnum.CharacterKey:
                                        case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                        case clsProperty.PropertyTypeEnum.LocationKey:
                                        case clsProperty.PropertyTypeEnum.ObjectKey:
                                            {
                                            bList = True
                                        case clsProperty.PropertyTypeEnum.Integer:
                                        case clsProperty.PropertyTypeEnum.ValueList:
                                            {
                                            bReturnInt = True
                                        case clsProperty.PropertyTypeEnum.SelectionOnly:
                                            {
                                            bReturnInt = True
                                        case clsProperty.PropertyTypeEnum.Text:
                                        case clsProperty.PropertyTypeEnum.StateList:
                                            {
                                    }
                                Else
                                    ' Duff property
                                }
                            }
                        ElseIf lstKeys.Count > 0 Then
                            private clsProperty p = null;
                            if (Adventure.htblAllProperties.TryGetValue(sNextKey, p))
                            {

                            }
                        }
                }

#if Debug And Runner
                if (Not bReturnInt && sMatch = sNextKey && sResults.ToString = "" && lstSubKeys.Count = 0)
                {
                    TODO(sNextKey);
                }
#endif

                'If bList AndAlso sMatch = sNextKey Then sMatch &= ".List"

                if (sMatch <> sNextKey)
                {
                    private string sSubProperty = sMatch.Substring(sNextKey.Length + 1);
                    if (sSubProperty.Length > 0)
                    {
                        if (lstSubKeys.Count = 0 && sResults.ToString <> "")
                        {
                            return sResults.ToString;
                        Else
                            return ReplaceOOPropertyOld(lstSubKeys, sSubProperty, bList);
                        }
                    }
                Else
                    if (lstSubKeys.Count > 0 && sResults.ToString = "")
                    {
                        For Each sSubKey As String In lstSubKeys
                            If sResults.Length > 0 Then sResults.Append("|")
                            sResults.Append(sSubKey);
                        Next;
                    }
                }

            }
        Next;

        If sAppend.ToString <> "" Then sResults.Append(sAppend.ToString)

        if (lstKeys.Count = 0)
        {
            ' We need to work out the datatype
            switch (sProperty)
            {
                case "Count":
                    {
                    bReturnInt = True
                case "List":
                    {
                    bReturnInt = False
                    return "";
                default:
                    {
                    if (sNextKey <> "" && sNextKey <> sMatch)
                    {
                        private string sSubProperty = sMatch.Substring(sNextKey.Length + 1);
                        if (sSubProperty.Length > 0)
                        {
                            return ReplaceOOPropertyOld(lstKeys, sSubProperty, bList);
                        }
                    Else
                        private clsProperty p = null;
                        if (Adventure.htblAllProperties.TryGetValue(sProperty, p))
                        {
                            switch (p.Type)
                            {
                                case clsProperty.PropertyTypeEnum.CharacterKey:
                                case clsProperty.PropertyTypeEnum.LocationGroupKey:
                                case clsProperty.PropertyTypeEnum.LocationKey:
                                case clsProperty.PropertyTypeEnum.ObjectKey:
                                    {
                                    ' Recurse again...
                                case clsProperty.PropertyTypeEnum.Integer:
                                case clsProperty.PropertyTypeEnum.ValueList:
                                    {
                                    bReturnInt = True
                                case clsProperty.PropertyTypeEnum.StateList:
                                case clsProperty.PropertyTypeEnum.Text:
                                    {
                                    bReturnInt = False
                                    return "#*!~#" ' We don't want to replace Properties of nothing (e.g. notakey.Property);
                            }
                        Else
                            return "#*!~#";
                        }
                    }
            }
        }

        if (bReturnInt)
        {
            return iResult.ToString;
        Else
            return sResults.ToString;
        }

    }
#endif


    private List<string> SplitArgs(string sArgs)
    {

        private New List<string> lArgs;
        private int iLevel = 0;
        private string sArg = "";

        for (int i = 0; i <= sArgs.Length - 1; i++)
        {
            switch (sArgs(i))
            {
                case ":
                case "c:
                    {
                    if (iLevel = 0)
                    {
                        If sArg <> "" Then lArgs.Add(sArg)
                        sArg = ""
                    Else
                        sArg &= sArgs(i);
                    }
                case "("c:
                case "["c:
                    {
                    iLevel += 1;
                    sArg &= sArgs(i);
                case ")"c:
                case "]"c:
                    {
                    iLevel -= 1;
                    sArg &= sArgs(i);
                default:
                    {
                    sArg &= sArgs(i);
            }
        Next;
        If sArg <> "" Then lArgs.Add(sArg)

        return lArgs;

    }


    private void EvaluateUDF(clsUserFunction udf, ref sText As String)
    {

        ' This will need to be a bit more sophisticated once we have arguments...
        private New System.Text.RegularExpressions.Regex("%" & udf.Name & "(\[.*?\])?%") re;

        if (re.IsMatch(sText))
        {
            ' We can't test udf.Output.ToString until we've replaced the parameters below

            ' Test for recursion
            For Each d As SingleDescription In udf.Output
                if (d.Description.Contains("%" + udf.Name + If(udf.Arguments.Count = 0, "%", "[")))
                {
                    ErrMsg("Recursive User Defined Function - " + udf.Name);
                    Exit Sub;
                }
            Next;

            ' Replace each parameter with it's resolved value
            private string sMatch = re.Match(sText).Value;
            private Description dOut = udf.Output.Copy;

#if Runner
                ' Backup existing Refs
                private clsNewReference[] refsCopy = UserSession.NewReferences;
                private clsNewReference[] refsUDF = {};
                private int iRefNo = 0;
#endif

            if (sMatch.Contains("[") && sMatch.Contains("]"))
            {
                private string sArgs = sMatch.Substring(sMatch.IndexOf("["c) + 1, sMatch.LastIndexOf("]"c) - sMatch.IndexOf("["c) - 1);

                'Dim sArg() As String = sArgs.Split(","c)
                private List<string> sArg = SplitArgs(sArgs);
                private int i = 0;
                For Each arg As clsUserFunction.Argument In udf.Arguments
                    private string sEvaluatedArg = ReplaceFunctions(sArg(i));

#if Runner
                        If sEvaluatedArg.Contains("|") Then ' Means it evaluated to multiple items
                            ' Depending on arg type, create an objects parameter, and set the refs
                            switch (arg.Type)
                            {
                                case clsUserFunction.ArgumentType.Object:
                                    {
                                    private New clsNewReference(ReferencesType.Object) refOb;
                                    With refOb;
                                        For Each sOb As String In sEvaluatedArg.Split("|"c)
                                            private New clsSingleItem itmOb;
                                            itmOb.MatchingPossibilities.Add(sOb);
                                            itmOb.bExplicitlyMentioned = true;
                                            .Items.Add(itmOb);
                                        Next;
                                    }
                                    'sEvaluatedArg = "%objects%"
                                    ReDim Preserve refsUDF(iRefNo);
                                    refsUDF(iRefNo) = refOb;
                                    iRefNo += 1;
                            }
                        }
#endif

                    ' Our function argument could be an expression
                    if (new System.Text.RegularExpressions.Regex("\d( )*[+-/*^]( )*\d").IsMatch(sEvaluatedArg))
                    {
                        private string sExpr = EvaluateExpression(sEvaluatedArg);
                        If sExpr != null Then sEvaluatedArg = sExpr
                    }

                    For Each d As SingleDescription In dOut
                        d.Description = d.Description.Replace("%" + arg.Name + "%", sEvaluatedArg);
                        For Each r As clsRestriction In d.Restrictions
                            If r.sKey1 = "Parameter-" + arg.Name Then r.sKey1 = If(sEvaluatedArg.Contains("|"), "ReferencedObjects", sEvaluatedArg).ToString
                            If r.sKey2 = "Parameter-" + arg.Name Then r.sKey2 = If(sEvaluatedArg.Contains("|"), "ReferencedObjects", sEvaluatedArg).ToString
                        Next;
                    Next;
                    i += 1;
                Next;
            }

#if Runner
            UserSession.NewReferences = refsUDF;
#endif
            private string sFunctionResult = dOut.ToString;
#if Runner
            ' Restore Refs
            UserSession.NewReferences = refsCopy;
#endif
            sText = ReplaceFunctions(re.Replace(sText, sFunctionResult, 1))
        }

    }


    public void ReplaceFunctions(string sText, bool bExpression = false, bool bAllowOO = true, Dictionary(Of String Replacements, String)
    {

        try
        {
            private New Dictionary<string, string> dictGUIDs;
            while (sText.Contains("<#"))
            {
                private int iStart = sText.IndexOf("<#");
                private int iEnd = sText.IndexOf("#>", iStart);
                private string sGUID = ":" + System.Guid.NewGuid.ToString + ":";
                private string sExpression = sText.Substring(iStart, iEnd - iStart + 2);
                dictGUIDs.Add(sGUID, sExpression);
                sText = sText.Replace(sExpression, sGUID)
            }

            For Each udf As clsUserFunction In Adventure.htblUDFs.Values
                EvaluateUDF(udf, sText);
            Next;

            if (sInstr(sText, "%") > 0)
            {

                private string sCheck;

                do
                {
                    sCheck = sText

                    sText = ReplaceIgnoreCase(sText, "%Player%", Adventure.Player.Key, Replacements)

                    sText = ReplaceIgnoreCase(sText, "%object%", "%object1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%character%", "%character1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%location%", "%location1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%direction%", "%direction1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%item%", "%item1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%text%", "%text1%", Replacements)
                    sText = ReplaceIgnoreCase(sText, "%number%", "%number1%", Replacements)

                    'If bExpression Then
                    '    sText = ReplaceIgnoreCase(sText, "%text%", """" & Adventure.sReferencedText & """")
                    'Else
                    '    sText = ReplaceIgnoreCase(sText, "%text%", Adventure.sReferencedText)
                    'End If
                    sText = ReplaceIgnoreCase(sText, "%ConvCharacter%", Adventure.sConversationCharKey)
                    sText = ReplaceIgnoreCase(sText, "%turns%", Adventure.Turns.ToString)
                    private string sVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString;
                    sText = ReplaceIgnoreCase(sText, "%version%", sVersion.Substring(0, 1) & sVersion.Substring(2, 1) & sVersion.Substring(4)) ' .dVersion.ToString)
                    sText = ReplaceIgnoreCase(sText, "%release%", Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.Version.ToString)
                    'sText = ReplaceIgnoreCase(sText, "%room%", Adventure.htblLocations(Adventure.Player.Location.LocationKey).ShortDescription.ToString)

                    if (Contains(sText, "%AloneWithChar%"))
                    {
                        private string sAloneWithChar = Adventure.Player.AloneWithChar;
                        If sAloneWithChar == null Then sAloneWithChar = NOCHARACTER
                        sText = ReplaceIgnoreCase(sText, "%AloneWithChar%", sAloneWithChar)
                    }
                    if (sText.Contains("%CharacterName["))
                    {
                        For Each sPronoun As String In New String() {"subject", "subjective", "personal", "target", "object", "objective", "possessive"}
                            sText = ReplaceIgnoreCase(sText, "%CharacterName[" & sPronoun & "]%", "%CharacterName[%Player%, " & sPronoun & "]%")
                        Next;
                    }
                    sText = ReplaceIgnoreCase(sText, "%CharacterName%", "%CharacterName[%Player%]%") ' Function without args points to Player

#if Runner
                    With UserSession;
                        if (.NewReferences IsNot null)
                        {
                            for (int iObRef = 1; iObRef <= 5; iObRef++)
                            {
                                if (Contains(sText, "%object" + iObRef + "%") Then ' sInstr(sText, "%object" + iObRef + "%") > 0)
                                {
                                    ' Get the first object reference
                                    'Dim iActRef As Integer = 0
                                    private bool bQuote = bExpression && Not Contains(sText, "%object" + iObRef + "%.") && Not Contains(sText, """%object" + iObRef + "%""");
                                    For Each nr As clsNewReference In .NewReferences
                                        if (nr.ReferenceType = ReferencesType.Object)
                                        {
                                            'iActRef += 1
                                            'nr.ReferenceMatch = "objects" OrElse
                                            if (nr.ReferenceMatch = "object" + iObRef Then ' iActRef = iObRef)
                                            {
                                                private string sObjects = "";
                                                For Each itm As clsSingleItem In nr.Items
                                                    If sObjects <> "" Then sObjects &= "|"
                                                    sObjects &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sObjects = """" + sObjects + """"
                                                If sObjects <> "" Then sText = ReplaceIgnoreCase(sText, "%object" + iObRef + "%", sObjects)
                                                Exit For;
                                            }
                                        }
                                    Next;
                                }
                            Next iObRef;

                            if (Contains(sText, "%objects%"))
                            {
                                ' Get the first object reference
                                private bool bQuote = bExpression && Not Contains(sText, "%objects%.") && Not Contains(sText, """%objects%""");
                                For Each nr As clsNewReference In .NewReferences
                                    if (nr.ReferenceType = ReferencesType.Object && nr.ReferenceMatch = "objects" Then ' && nr.bMultiple)
                                    {
                                        private string sObjects = "";
                                        For Each itm As clsSingleItem In nr.Items
                                            If sObjects <> "" Then sObjects &= "|"
                                            sObjects &= itm.MatchingPossibilities(0);
                                        Next;
                                        If bQuote Then sObjects = """" + sObjects + """"
                                        If sObjects <> "" Then sText = ReplaceIgnoreCase(sText, "%objects%", sObjects)
                                        Exit For;
                                    }
                                Next;
                            }

                            for (int iCharRef = 1; iCharRef <= 5; iCharRef++)
                            {
                                if (Contains(sText, "%character" + iCharRef + "%") Then ' sInstr(sText, "%character" + iCharRef + "%") > 0)
                                {
                                    ' Get the first character reference
                                    'Dim iActRef As Integer = 0
                                    private bool bQuote = bExpression && Not Contains(sText, "%character" + iCharRef + "%.") && Not Contains(sText, """%character" + iCharRef + "%""");
                                    For Each nr As clsNewReference In .NewReferences
                                        if (nr.ReferenceType = ReferencesType.Character)
                                        {
                                            'iActRef += 1
                                            if (nr.ReferenceMatch = "character" + iCharRef Then ' iActRef = iCharRef)
                                            {
                                                private string sCharacters = "";
                                                For Each itm As clsSingleItem In nr.Items
                                                    If sCharacters <> "" Then sCharacters &= "|"
                                                    sCharacters &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sCharacters = """" + sCharacters + """"
                                                If sCharacters <> "" Then sText = ReplaceIgnoreCase(sText, "%character" + iCharRef + "%", sCharacters)
                                                Exit For;
                                            }
                                        }
                                    Next;
                                }
                            Next iCharRef;

                            if (Contains(sText, "%characters%"))
                            {
                                ' Get the first character reference
                                private bool bQuote = bExpression && Not Contains(sText, "%characters%.") && Not Contains(sText, """%characters%""");
                                For Each nr As clsNewReference In .NewReferences
                                    if (nr.ReferenceType = ReferencesType.Character Then ' && nr.bMultiple)
                                    {
                                        private string sCharacters = "";
                                        For Each itm As clsSingleItem In nr.Items
                                            If sCharacters <> "" Then sCharacters &= "|"
                                            sCharacters &= itm.MatchingPossibilities(0);
                                        Next;
                                        If bQuote Then sCharacters = """" + sCharacters + """"
                                        If sCharacters <> "" Then sText = ReplaceIgnoreCase(sText, "%characters%", sCharacters)
                                        Exit For;
                                    }
                                Next;
                            }

                            for (int iLocRef = 1; iLocRef <= 5; iLocRef++)
                            {
                                if (Contains(sText, "%location" + iLocRef + "%"))
                                {
                                    ' Get the first location reference
                                    private bool bQuote = bExpression && Not Contains(sText, "%location" + iLocRef + "%.") && Not Contains(sText, """%location" + iLocRef + "%""");
                                    For Each nr As clsNewReference In .NewReferences
                                        if (nr.ReferenceType = ReferencesType.Location)
                                        {
                                            'iActRef += 1
                                            if (nr.ReferenceMatch = "location" + iLocRef Then ' iActRef = iLocRef)
                                            {
                                                private string sLocations = "";
                                                For Each itm As clsSingleItem In nr.Items
                                                    If sLocations <> "" Then sLocations &= "|"
                                                    sLocations &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sLocations = """" + sLocations + """"
                                                If sLocations <> "" Then sText = ReplaceIgnoreCase(sText, "%location" + iLocRef + "%", sLocations)
                                                Exit For;
                                            }
                                        }
                                    Next;
                                }
                            Next iLocRef;

                            for (int iItemRef = 1; iItemRef <= 5; iItemRef++)
                            {
                                if (Contains(sText, "%item" + iItemRef + "%"))
                                {
                                    ' Get the first item reference
                                    private bool bQuote = bExpression && Not Contains(sText, "%item" + iItemRef + "%.") && Not Contains(sText, """%item" + iItemRef + "%""");
                                    For Each nr As clsNewReference In .NewReferences
                                        if (nr.ReferenceType = ReferencesType.Item)
                                        {
                                            'iActRef += 1
                                            if (nr.ReferenceMatch = "item" + iItemRef Then ' iActRef = iItemRef)
                                            {
                                                private string sItems = "";
                                                For Each itm As clsSingleItem In nr.Items
                                                    If sItems <> "" Then sItems &= "|"
                                                    sItems &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sItems = """" + sItems + """"
                                                If sItems <> "" Then sText = ReplaceIgnoreCase(sText, "%item" + iItemRef + "%", sItems)
                                                Exit For;
                                            }
                                        }
                                    Next;
                                }
                            Next iItemRef;

                            for (int iDirRef = 1; iDirRef <= 5; iDirRef++)
                            {
                                if (Contains(sText, "%direction" + iDirRef + "%"))
                                {
                                    ' Get the first direction reference
                                    private bool bQuote = bExpression && Not Contains(sText, "%direction" + iDirRef + "%.") && Not Contains(sText, """%direction" + iDirRef + "%""");
                                    For Each nr As clsNewReference In .NewReferences
                                        if (nr.ReferenceType = ReferencesType.Direction)
                                        {
                                            'iActRef += 1
                                            if (nr.ReferenceMatch = "direction" + iDirRef Then ' iActRef = iDirRef)
                                            {
                                                private string sDirections = "";
                                                For Each itm As clsSingleItem In nr.Items
                                                    If sDirections <> "" Then sDirections &= "|"
                                                    sDirections &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sDirections = """" + sDirections + """"
                                                If sDirections <> "" Then sText = ReplaceIgnoreCase(sText, "%direction" + iDirRef + "%", sDirections)
                                                Exit For;
                                            }
                                        }
                                    Next;
                                }
                            Next iDirRef;


                            for (int i = 1; i <= 5; i++)
                            {
                                private int iRef = i '- 1;
                                'If iRef > 0 Then iRef -= 1

                                private string sNumber = "%number" + If(i > 0, i.ToString, "").ToString + "%";
                                if (Contains(sText, sNumber))
                                {
                                    private bool bQuote = bExpression && Not Contains(sText, "%number" + If(i > 0, i.ToString, "").ToString + "%.") && Not Contains(sText, """%number" + If(i > 0, i.ToString, "").ToString + "%""");
                                    For Each ref As clsNewReference In .NewReferences
                                        if (ref.ReferenceType = ReferencesType.Number)
                                        {
                                            'iNumRef += 1
                                            if (ref.ReferenceMatch = "number" + iRef Then ' iNumRef - 1 = iRef)
                                            {
                                                'If ref.Items.Count = 1 AndAlso ref.Items(0).MatchingPossibilities.Count = 1 Then
                                                '    sText = ReplaceIgnoreCase(sText, sNumber, ref.Items(0).MatchingPossibilities(0))
                                                'End If
                                                'Exit For
                                                private string sNumbers = "";
                                                For Each itm As clsSingleItem In ref.Items
                                                    If sNumbers <> "" Then sNumbers &= "|"
                                                    sNumbers &= itm.MatchingPossibilities(0);
                                                Next;
                                                If bQuote Then sNumbers = """" + sNumbers + """"
                                                If sNumbers <> "" Then sText = ReplaceIgnoreCase(sText, sNumber, sNumbers)
                                                Exit For;
                                            }
                                        }
                                    Next;

                                    'sText = ReplaceIgnoreCase(sText, sNumber, Adventure.iReferencedNumber(iRef).ToString)
                                }

                                private string sRefText = "%text" + If(i > 0, i.ToString, "").ToString + "%";
                                if (Contains(sText, sRefText))
                                {
                                    private bool bQuote = bExpression && Not Contains(sText, "%text" + If(i > 0, i.ToString, "").ToString + "%.") && Not Contains(sText, """%text" + If(i > 0, i.ToString, "").ToString + "%""");
                                    For Each ref As clsNewReference In .NewReferences
                                        if (ref.ReferenceType = ReferencesType.Text)
                                        {
                                            'iTextRef += 1
                                            if (ref.ReferenceMatch = "text" + iRef Then ' iTextRef - 1 = iRef)
                                            {
                                                if (ref.Items.Count = 1 && ref.Items(0).MatchingPossibilities.Count = 1)
                                                {
                                                    if (bExpression)
                                                    {
                                                        sText = ReplaceIgnoreCase(sText, sRefText, """" & ref.Items(0).MatchingPossibilities(0) & """")
                                                    Else
                                                        sText = ReplaceIgnoreCase(sText, sRefText, ref.Items(0).MatchingPossibilities(0))
                                                    }
                                                }
                                            }
                                        }
                                    Next;
                                    if (bQuote)
                                    {
                                        sText = ReplaceIgnoreCase(sText, sRefText, """" & Adventure.sReferencedText(iRef) & """")
                                    Else
                                        sText = ReplaceIgnoreCase(sText, sRefText, Adventure.sReferencedText(iRef))
                                    }
                                }
                            Next;

                        }
                    }
#endif

                    For Each var As clsVariable In Adventure.htblVariables.Values
                        if (var.Length = 1)
                        {
                            while (InStr(sText, "%" + var.Name + "%", CompareMethod.Text) > 0)
                            {
                                if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    sText = ReplaceIgnoreCase(sText, "%" & var.Name & "%", var.IntValue.ToString) ' sText.Replace("%" & var.Name & "%", var.IntValue.ToString)
                                Else
                                    if (bExpression)
                                    {
                                        sText = ReplaceIgnoreCase(sText, "%" & var.Name & "%", """" & var.StringValue & """")
                                    Else
                                        sText = ReplaceIgnoreCase(sText, "%" & var.Name & "%", var.StringValue)
                                    }
                                }
                            }
                        ElseIf var.Length > 1 Then
                            while (sInstr(sText, "%" + var.Name + "[") > 0)
                            {
                                private int iOffset = sInstr(sText, "%" + var.Name + "[") + var.Name.Length + 1;
                                private string sIndex = sText.Substring(iOffset, sText.IndexOf("]"c, iOffset) - iOffset);
                                if (IsNumeric(sIndex))
                                {
                                    if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                                    {
                                        sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", var.IntValue(CInt(sIndex)).ToString)
                                    Else
                                        if (bExpression)
                                        {
                                            sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", """" & var.StringValue(CInt(sIndex)).ToString & """")
                                        Else
                                            sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", var.StringValue(CInt(sIndex)).ToString)
                                        }
                                    }
                                Else
                                    private int iIndex = 0;
                                    if (IsNumeric(sIndex))
                                    {
                                        iIndex = SafeInt(sIndex)
                                    Else
                                        private New clsVariable varIndex;
                                        varIndex.Type = clsVariable.VariableTypeEnum.Numeric;
                                        varIndex.SetToExpression(sIndex);
                                        iIndex = varIndex.IntValue
                                    }
                                    if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                                    {
                                        sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", var.IntValue(iIndex).ToString)
                                    Else
                                        if (bExpression)
                                        {
                                            sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", """" & var.StringValue(iIndex).ToString & """")
                                        Else
                                            sText = sText.Replace("%" & var.Name & "[" & sIndex & "]%", var.StringValue(iIndex).ToString)
                                        }
                                    }
                                }
                            }
                        }
                    Next;

                    ' Need this to evaluate text in order, not replace by function order
                    ' This is in case eg. "%DisplayCharacter% %CharacterName%" where DisplayCharacter contains name, then we must evaluate first
                    ' name first in case we abbreviate, else we'll abbreviate the first name with 'he' instead of the second.
                    '
                    private string sFirstFunction = "";
                    private int iFirstLocation = Integer.MaxValue;
                    For Each sFunctionCheck As String In FunctionNames()
                        sFunctionCheck = sFunctionCheck.ToLower
                        private int iMatchCheck = sInstr(sText.ToLower, "%" + sFunctionCheck + "[");
                        if (iMatchCheck > 0 && iMatchCheck < iFirstLocation)
                        {
                            sFirstFunction = sFunctionCheck
                            iFirstLocation = iMatchCheck
                        }
                    Next;

                    'For Each sFunction As String In FunctionNames()
                    if (sFirstFunction <> "")
                    {
                        'sFunction = sFunction.ToLower
                        private string sFunction = sFirstFunction;
                        private int iMatchLoc = sInstr(sText.ToLower, "%" + sFunction + "[");
                        while (iMatchLoc > 0)
                        {
                            private string sArgs = GetFunctionArgs(sText.Substring(InStr(sText.ToLower, "%" + sFunction + "[") + sFunction.Length));
                            private int iArgsLength = sArgs.Length;
                            private bool bFunctionIsArgument = (iMatchLoc > 1 && sText.Substring(iMatchLoc - 2, 1) = "[" && sText.Substring(iMatchLoc + sFunction.Length + iArgsLength, 2) = "]%");

                            if (iArgsLength > 0 || sFunction.ToLower = "sum")
                            {

                                private string sOldArgs = sArgs;
                                sArgs = ReplaceFunctions(sArgs)
                                sText = sText.Substring(0, iMatchLoc - 1) & Replace(sText, sOldArgs, sArgs, iMatchLoc, 1) ' sText.Replace(sOldArgs, sArgs) ' Only replace 1 because subsequent functions could return different values (e.g. CharacterName)

                                if (sInstr(sText.ToLower, "%" + sFunction + "[" + sArgs.ToLower + "]%") > 0)
                                {

                                    private bool bAllowBlank = false;
                                    private string sResult = "";
                                    private New ObjectHashTable htblObjects;
                                    private New CharacterHashTable htblCharacters;
                                    private New LocationHashTable htblLocations;

                                    if (sArgs.Contains("|"))
                                    {
                                        ' We've got a pipe-separated list of parent keys
                                        Dim sKeys() As String = sArgs.Split("|"c)
                                        For Each sKey As String In sKeys
                                            If sKey.Contains(",") Then sKey = sLeft(sKey, sInstr(sKey, ",") - 1) ' trim off any other args
                                            If ! htblObjects.ContainsKey(sKey) && Adventure.htblObjects.ContainsKey(sKey) Then htblObjects.Add(Adventure.htblObjects(sKey), sKey)
                                            If ! htblCharacters.ContainsKey(sKey) && Adventure.htblCharacters.ContainsKey(sKey) Then htblCharacters.Add(Adventure.htblCharacters(sKey), sKey)
                                            If ! htblLocations.ContainsKey(sKey) && Adventure.htblLocations.ContainsKey(sKey) Then htblLocations.Add(Adventure.htblLocations(sKey), sKey)
                                        Next;
                                    Else
                                        private string sKey = sArgs;
                                        If sKey.Contains(",") Then sKey = sLeft(sKey, sInstr(sKey, ",") - 1)
                                        If ! htblObjects.ContainsKey(sKey) && Adventure.htblObjects.ContainsKey(sKey) Then htblObjects.Add(Adventure.htblObjects(sKey), sKey)
                                        If ! htblCharacters.ContainsKey(sKey) && Adventure.htblCharacters.ContainsKey(sKey) Then htblCharacters.Add(Adventure.htblCharacters(sKey), sKey)
                                        If ! htblLocations.ContainsKey(sKey) && Adventure.htblLocations.ContainsKey(sKey) Then htblLocations.Add(Adventure.htblLocations(sKey), sKey)
                                    }

                                    switch (sFunction)
                                    {
                                        case "CharacterDescriptor".ToLower:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                ' This needs to be The, depending whether we have referred to them already... :-/
                                                sResult = htblCharacters(sArgs).Descriptor
                                            Else
                                                DisplayError("Bad Argument to &perc;CharacterDescriptor[]&perc; - Character Key """ + sArgs + """ not found");
                                            }

                                        case "CharacterName".ToLower:
                                            {
                                            Dim sKeys() As String = Split(sArgs.Replace(" ", ""), ",")
                                            private string sKey = "";
                                            If sKeys.Length > 0 Then sKey = sKeys(0)
#if Runner
                                            private PronounEnum ePronoun = PronounEnum.Subjective;
                                            if (sKeys.Length = 2)
                                            {
                                                switch (sKeys(1).ToLower)
                                                {
                                                    case "subject":
                                                    case "subjective":
                                                    case "personal":
                                                        {
                                                        ePronoun = PronounEnum.Subjective
                                                    case "target":
                                                    case "object":
                                                    case "objective":
                                                        {
                                                        ePronoun = PronounEnum.Objective
                                                    case "possess":
                                                    case "possessive":
                                                        {
                                                        ePronoun = PronounEnum.Possessive
                                                    case "none":
                                                        {
                                                        ePronoun = PronounEnum.None
                                                }
                                            }

                                            if (htblCharacters.ContainsKey(sKey))
                                            {
                                                sResult = htblCharacters(sKey).Name(ePronoun)
                                                ' Slight fudge - if the name is the start of a sentence, auto-cap it... (consistent with v4)
                                                if (iMatchLoc > 3)
                                                {
                                                    if (sText.Substring(iMatchLoc - 3, 2) = vbCrLf || sText.Substring(iMatchLoc - 3, 2) = "  ")
                                                    {
                                                        sResult = PCase(sResult)
                                                    }
                                                }

                                                'sResult = AutoCapitalise(sText, sResult, iMatchLoc)
#if Runner
                                                'If bDisplaying Then Perspectives.Add(New PerspectiveOffset(iMatchLoc, htblCharacters(sArgs).Perspective))
                                                If UserSession.bDisplaying Then UserSession.PronounKeys.Add(sKey, ePronoun, htblCharacters(sKey).Gender, UserSession.sOutputText.Length + iMatchLoc)
                                                'Select Case htblCharacters(sKey).Gender
                                                '    Case clsCharacter.GenderEnum.Male
                                                '        Select Case ePronoun
                                                '            Case PronounEnum.Objective
                                                '                UserSession.LastObjectiveMalePronoun = sKey
                                                '            Case PronounEnum.Possessive
                                                '                UserSession.LastPossessiveMalePronoun = sKey
                                                '            Case PronounEnum.Reflective
                                                '                UserSession.LastReflectiveMalePronoun = sKey
                                                '            Case PronounEnum.Subjective
                                                '                UserSession.LastSubjectiveMalePronoun = sKey
                                                '        End Select
                                                '    Case clsCharacter.GenderEnum.Female
                                                '        Select Case ePronoun
                                                '            Case PronounEnum.Objective
                                                '                UserSession.LastObjectiveFemalePronoun = sKey
                                                '            Case PronounEnum.Possessive
                                                '                UserSession.LastPossessiveFemalePronoun = sKey
                                                '            Case PronounEnum.Reflective
                                                '                UserSession.LastReflectiveFemalePronoun = sKey
                                                '            Case PronounEnum.Subjective
                                                '                UserSession.LastSubjectiveFemalePronoun = sKey
                                                '        End Select
                                                '    Case clsCharacter.GenderEnum.Unknown
                                                '        Select Case ePronoun
                                                '            Case PronounEnum.Objective
                                                '                UserSession.LastObjectiveItPronoun = sKey
                                                '            Case PronounEnum.Possessive
                                                '                UserSession.LastPossessiveItPronoun = sKey
                                                '            Case PronounEnum.Reflective
                                                '                UserSession.LastReflectiveItPronoun = sKey
                                                '            Case PronounEnum.Subjective
                                                '                UserSession.LastSubjectiveItPronoun = sKey
                                                '        End Select
                                                'End Select
#endif
                                            ElseIf sKey = NOCHARACTER Then
                                                sResult = "Nobody"
                                            Else
                                                DisplayError("Bad Argument to &perc;CharacterName[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
#else
                                            if (Adventure.htblCharacters.ContainsKey(sKey))
                                            {
                                                sResult = htblCharacters(sKey).Name()
                                            }
#endif

                                        case "CharacterProper".ToLower:
                                        case "ProperName".ToLower:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = htblCharacters(sArgs).ProperName
                                                'sResult = AutoCapitalise(sText, sResult, iMatchLoc)
                                                '#If Runner Then
                                                '                                            If bDisplaying Then Perspectives.Add(New PerspectiveOffset(iMatchLoc, htblCharacters(sArgs).Perspective))
                                                '#End If

                                            Else
                                                DisplayError("Bad Argument to &perc;CharacterProper[]&perc; - Character Key """ + sArgs + """ not found");
                                            }

                                        case "DisplayCharacter".ToLower:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = htblCharacters(sArgs).Description.ToString
                                                If sResult = "" Then sResult = "%CharacterName% see[//s] nothing interesting about %CharacterName[" + sArgs + ", target]%."
                                            Else
                                                DisplayError("Bad Argument to &perc;DisplayCharacter[]&perc; - Character Key """ + sArgs + """ not found");
                                            }

                                        case "DisplayLocation".ToLower:
                                            {
                                            if (Adventure.htblLocations.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblLocations(sArgs).ViewLocation
                                                If sResult = "" Then sResult = "There is nothing of interest here."
                                            Else
                                                DisplayError("Bad Argument to &perc;DisplayLocation[]&perc; - Location Key """ + sArgs + """ not found");
                                            }

                                        case "DisplayObject".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = htblObjects(sArgs).Description.ToString
                                                If sResult = "" Then sResult = "%CharacterName% see[//s] nothing interesting about " + htblObjects(sArgs).FullName(ArticleTypeEnum.Definite) + "."
                                            Else
                                                DisplayError("Bad Argument to &perc;DisplayObject[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "Held".ToLower:
                                            {
#if Runner
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                For Each ob As clsObject In Adventure.htblCharacters(sArgs).HeldObjects.Values
                                                    If sResult <> "" Then sResult &= "|"
                                                    sResult &= ob.Key;
                                                Next;
                                            Else
                                                DisplayError("Bad Argument to &perc;Held[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
                                            bAllowBlank = True
#endif
                                        case "LCase".ToLower:
                                            {
                                            sResult = sArgs.ToLower

                                        case "ListCharactersOn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblObjects(sArgs).ChildrenCharacters(clsObject.WhereChildrenEnum.OnObject).List()
                                            Else
                                                DisplayError("Bad Argument to &perc;ListCharactersOn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "ListCharactersIn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblObjects(sArgs).ChildrenCharacters(clsObject.WhereChildrenEnum.InsideObject).List()
                                            Else
                                                DisplayError("Bad Argument to &perc;ListCharactersIn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "ListCharactersOnAndIn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblObjects(sArgs).DisplayCharacterChildren
                                            Else
                                                DisplayError("Bad Argument to &perc;ListCharactersOnAndIn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "ListHeld".ToLower:
                                            {
#if Runner
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblCharacters(sArgs).HeldObjects.List(, True, ArticleTypeEnum.Indefinite)
                                            Else
                                                DisplayError("Bad Argument to &perc;ListHeld[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
#endif
                                        case "ListExits".ToLower:
                                            {
#if Runner
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblCharacters(sArgs).ListExits
                                            Else
                                                DisplayError("Bad Argument to &perc;ListExits[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
#endif
                                        case "ListObjectsAtLocation".ToLower:
                                            {
                                            if (Adventure.htblLocations.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblLocations(sArgs).ObjectsInLocation(clsLocation.WhichObjectsToListEnum.AllObjects, True).List(, , ArticleTypeEnum.Indefinite)
                                            Else
                                                DisplayError("Bad Argument to &perc;ListObjectsAtLocation[]&perc; - Location Key """ + sArgs + """ not found");
                                            }

                                        case "ListObjectsOn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblObjects(sArgs).Children(clsObject.WhereChildrenEnum.OnObject).List(, , ArticleTypeEnum.Indefinite)
                                            Else
                                                DisplayError("Bad Argument to &perc;ListObjectsOn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "ListObjectsIn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblObjects(sArgs).Children(clsObject.WhereChildrenEnum.InsideObject).List(, , ArticleTypeEnum.Indefinite)
                                            Else
                                                DisplayError("Bad Argument to &perc;ListObjectsIn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }

                                        case "ListObjectsOnAndIn".ToLower:
                                            {
                                            if (htblObjects.Count > 0)
                                            {
                                                For Each ob As clsObject In htblObjects.Values
                                                    if (htblObjects.Count = 1 || ob.Children(clsObject.WhereChildrenEnum.InsideOrOnObject).Count > 0)
                                                    {
                                                        If sResult <> "" Then pSpace(sResult)
                                                        sResult &= ob.DisplayObjectChildren;
                                                    }
                                                Next;
                                            Else
                                                DisplayError("Bad Argument to &perc;ListObjectsOnAndIn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }
                                            'If Adventure.htblObjects.ContainsKey(sArgs) Then
                                            '    sResult = Adventure.htblObjects(sArgs).DisplayObjectChildren
                                            'Else
                                            '    DisplayError("Bad Argument to &perc;ListObjectsOnAndIn[]&perc; - Object Key """ & sArgs & """ not found")
                                            'End If

                                        case "ListWorn".ToLower:
                                            {
#if Runner
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblCharacters(sArgs).WornObjects.List("and", True, ArticleTypeEnum.Indefinite)
                                            Else
                                                DisplayError("Bad Argument to &perc;ListWorn[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
#endif
                                        case "LocationName".ToLower:
                                            {
                                            if (Adventure.htblLocations.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblLocations(sArgs).ShortDescription.ToString
                                            Else
                                                DisplayError("Bad Argument to &perc;LocationName[]&perc; - Location Key """ + sArgs + """ not found");
                                            }

                                        case "LocationOf".ToLower:
                                            {
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblCharacters(sArgs).Location.LocationKey
                                            ElseIf Adventure.htblObjects.ContainsKey(sArgs) Then
                                                For Each sLocKey As String In Adventure.htblObjects(sArgs).LocationRoots.Keys
                                                    If sResult <> "" Then sResult &= "|"
                                                    sResult &= sLocKey;
                                                Next;
                                            Else
                                                DisplayError("Bad Argument to &perc;LocationOf[]&perc; - Character Key """ + sArgs + """ not found");
                                            }

                                        case "NumberAsText".ToLower:
                                            {
                                            sResult = NumberToString(sArgs)

                                        case "ObjectName".ToLower:
                                            {
                                            sResult = htblObjects.List(, , ArticleTypeEnum.Indefinite)

                                        case "ObjectsIn".ToLower:
                                            {
                                            if (Adventure.htblObjects.ContainsKey(sArgs))
                                            {
                                                For Each ob As clsObject In Adventure.htblObjects(sArgs).Children(clsObject.WhereChildrenEnum.InsideObject).Values
                                                    If sResult <> "" Then sResult &= "|"
                                                    sResult &= ob.Key;
                                                Next;
                                            Else
                                                DisplayError("Bad Argument to &perc;ObjectsIn[]&perc; - Object Key """ + sArgs + """ not found");
                                            }
                                            bAllowBlank = True

                                        case "ParentOf".ToLower:
                                            {
                                            'Dim htblParents As New ObjectHashTable
                                            'For Each ob As clsObject In htblObjects.Values
                                            '    htblParents.Add(Adventure.htblObjects(ob.Parent), ob.Parent)
                                            'Next
                                            'sResult = htblParents.List
                                            For Each ob As clsObject In htblObjects.Values
                                                If sResult <> "" Then sResult &= "|"
                                                sResult &= ob.Parent;
                                            Next;
                                            For Each ch As clsCharacter In htblCharacters.Values
                                                If sResult <> "" Then sResult &= "|"
                                                sResult &= ch.Parent;
                                            Next;
                                            '' Just take the first one.  Not sure about multiple...
                                            ''Dim htblParents As New ObjectHashTable
                                            'For Each ob As clsObject In htblObjects.Values
                                            '    sResult = ob.Parent
                                            '    'If Not htblParents.Contains(ob.Parent) Then
                                            '    'Return ob.Parent
                                            '    'End If
                                            '    'htblParents.Add(Adventure.htblObjects(ob.Parent), ob.Parent)
                                            'Next
                                        case "PCase".ToLower:
                                            {
                                            sResult = PCase(sArgs, , bExpression)

                                        case "PopUpChoice".ToLower:
                                            {
                                            Dim sKeys() As String = sArgs.Split(","c) ' TODO - Improve this to read quotes and commas properly
                                            if (sKeys.Length = 3)
                                            {
                                                ' For now - needs improving
                                                private New clsVariable var;
                                                var.Type = clsVariable.VariableTypeEnum.Text;
                                                switch (MsgBox(sKeys(0) + vbCrLf + vbCrLf + "Yes for " + sKeys(1) + ", No for " + sKeys(2) + " (dialog box to be improved!)", MsgBoxStyle.YesNo))
                                                {
                                                    case MsgBoxResult.Yes:
                                                        {
                                                        var.SetToExpression(sKeys(1));
                                                    case MsgBoxResult.No:
                                                        {
                                                        var.SetToExpression(sKeys(2));
                                                }
                                                sResult = var.StringValue
                                                If bExpression Then sResult = """" + sResult + """"
                                            Else
                                                DisplayError("Bad arguments to PopUpChoice function: PopUpChoice[prompt, choice1, choice2]");
                                            }

                                        case "PopUpInput".ToLower:
                                            {
                                            Dim sKeys() As String = sArgs.Split(","c) ' TODO - Improve this to read quotes and commas properly
                                            if (sKeys.Length = 1 || sKeys.Length = 2)
                                            {
                                                'Dim var As New clsVariable
                                                'var.Type = clsVariable.VariableTypeEnum.Text
                                                'var.SetToExpression(sKeys(0))
                                                private string sDefault = "";
                                                If sKeys.Length = 2 Then sDefault = EvaluateExpression(sKeys(1))
                                                sResult = """" & InputBox(EvaluateExpression(sKeys(0)), "ADRIFT", sDefault) & """"
                                            Else
                                                DisplayError("Expecting 1 or two arguments to PopUpInput[prompt, default]");
                                            }

                                        case "PrevListObjectsOn".ToLower:
                                            {
                                            ' Maintain a 'last turn' state
                                            ' Call ListObjectsOn on this state
                                            sResult = PreviousFunction(sFunction, sArgs)

                                        case "PrevParentOf".ToLower:
                                            {
                                            ' Get rid of PrevParent, and do the same as above
                                            For Each ob As clsObject In htblObjects.Values
                                                If sResult <> "" Then sResult &= "|"
                                                sResult &= ob.PrevParent;
                                            Next;
                                            For Each ch As clsCharacter In htblCharacters.Values
                                                If sResult <> "" Then sResult &= "|"
                                                sResult &= ch.PrevParent;
                                            Next;

                                        case "PropertyValue".ToLower:
                                            {
                                            bAllowBlank = True
                                            Dim sKeys() As String = Split(sArgs.Replace(" ", ""), ",")
                                            if (sKeys.Length = 2)
                                            {
                                                if (htblObjects.Count + htblCharacters.Count + htblLocations.Count > 0)
                                                {
                                                    private New StringArrayList arlOutput;
                                                    For Each ob As clsObject In htblObjects.Values
                                                        if (ob.HasProperty(sKeys(1)))
                                                        {
                                                            arlOutput.Add(ob.GetPropertyValue(sKeys(1)));
                                                        Else
                                                            DisplayError("Bad 2nd Argument to &perc;PropertyValue[]&perc; - Property Key """ + sKeys(1) + """ not found");
                                                        }
                                                    Next;
                                                    For Each ch As clsCharacter In htblCharacters.Values
                                                        if (ch.HasProperty(sKeys(1)))
                                                        {
                                                            arlOutput.Add(ch.GetPropertyValue(sKeys(1)));
                                                        Else
                                                            DisplayError("Bad 2nd Argument to &perc;PropertyValue[]&perc; - Property Key """ + sKeys(1) + """ not found");
                                                        }
                                                    Next;
                                                    For Each l As clsLocation In htblLocations.Values
                                                        if (l.HasProperty(sKeys(1)))
                                                        {
                                                            arlOutput.Add(l.GetPropertyValue(sKeys(1)));
                                                        Else
                                                            DisplayError("Bad 2nd Argument to &perc;PropertyValue[]&perc; - Property Key """ + sKeys(1) + """ not found");
                                                        }
                                                    Next;
                                                    sResult = arlOutput.List
                                                Else
                                                    ' Only warn about the first arg if it isn't from a function
                                                    private string sOrig = Split(sOldArgs.Replace(" ", ""), ",")(0);
                                                    If sOrig = sKeys(0) Then DisplayError("Bad 1st Argument to &perc;PropertyValue[]&perc; - Object/Character Key """ + sKeys(0) + """ not found")
                                                }
                                            Else
                                                DisplayError("Bad call to &perc;PropertyValue[]&perc; - Two arguments expected; Object Key, Property Key");
                                            }

                                            'Case "ProperName".ToLower
                                            '    If Adventure.htblCharacters.ContainsKey(sArgs) Then
                                            '        sResult = htblCharacters(sArgs).ProperName
                                            '    Else
                                            '        DisplayError("Bad Argument to &perc;ProperName[]&perc; - Character Key """ & sArgs & """ not found")
                                            '    End If

                                        case "Sum".ToLower:
                                            {
                                            ' Sum the numbers from a string
                                            private New System.Text.StringBuilder sInput;
                                            For Each c As Char In sArgs.ToCharArray
                                                switch (c)
                                                {
                                                    case "0"c To "9"c:
                                                    case "-"c:
                                                        {
                                                        sInput.Append(c);
                                                    default:
                                                        {
                                                        sInput.Append(" ");
                                                }
                                            Next;
                                            while (sInput.ToString.Contains("  "))
                                            {
                                                sInput.Replace("  ", " ");
                                            }
                                            private int iTotal = 0;
                                            For Each s As String In sInput.ToString.Split(" "c)
                                                iTotal += SafeInt(s);
                                            Next;
                                            sResult = iTotal.ToString

                                        case "TaskCompleted".ToLower:
                                            {
                                            if (Adventure.htblTasks.ContainsKey(sArgs))
                                            {
                                                sResult = Adventure.htblTasks(sArgs).Completed.ToString
                                            Else
                                                DisplayError("Bad Argument to &perc;TaskCompleted[]&perc; - Task Key """ + sArgs + """ not found");
                                            }

                                        case "TheObject".ToLower:
                                        case "TheObjects".ToLower:
                                            {
                                            sResult = htblObjects.List

                                        case "UCase".ToLower:
                                            {
                                            sResult = sArgs.ToUpper

                                        case "Worn".ToLower:
                                            {
#if Runner
                                            if (Adventure.htblCharacters.ContainsKey(sArgs))
                                            {
                                                For Each ob As clsObject In Adventure.htblCharacters(sArgs).WornObjects.Values
                                                    If sResult <> "" Then sResult &= "|"
                                                    sResult &= ob.Key;
                                                Next;
                                            Else
                                                DisplayError("Bad Argument to &perc;Worn[]&perc; - Character Key """ + sArgs + """ not found");
                                            }
#endif
                                    }

                                    if (sResult = "" && Not bAllowBlank)
                                    {
                                        DisplayError("Bad Function - null output");
                                    }

                                    sText = ReplaceIgnoreCase(sText, "%" & sFunction & "[" & sArgs & "]%", sResult)

                                Else
                                    private string sBadFunction = "";
                                    If sText.ToLower.Contains("%" + sFunction) Then sBadFunction = "%" + sFunction
                                    If sText.ToLower.Contains("%" + sFunction + "[") Then sBadFunction = "%" + sFunction + "["
                                    If sText.ToLower.Contains("%" + sFunction + "[" + sArgs.ToLower) Then sBadFunction = "%" + sFunction + "[" + sArgs
                                    If sText.ToLower.Contains("%" + sFunction + "[" + sArgs.ToLower + "]") Then sBadFunction = "%" + sFunction + "[" + sArgs + "]"
                                    sText = ReplaceIgnoreCase(sText, sBadFunction, " <c><u>" & sBadFunction.Replace("%", "&perc;") & "</u></c>")

                                    DisplayError("Bad function " + sFunction);

                                }
                            Else
                                sText = ReplaceIgnoreCase(sText, "%" & sFunction & "[]%", "")
                                sText = ReplaceIgnoreCase(sText, "%" & sFunction & "[", "")

                                DisplayError("No arguments given to function " + sFunction);

                            }

                            iMatchLoc = sInstr(sText.ToLower, "%" & sFunction & "[")
                        }
                        'Next
                    }

                Loop While sText <> sCheck;
            }


            if (bAllowOO)
            {
                private string sPrev;
                do
                {
                    sPrev = sText
                    sText = ReplaceOO(sText, bExpression)
                Loop While sText <> sPrev;
            }


            private New System.Text.RegularExpressions.Regex("\[(?<first>.*?)/(?<second>.*?)/(?<third>.*?)\]") rePerspective;
            while (rePerspective.IsMatch(sText))
            {
                'If Adventure.LastPerspective = clsCharacter.PerspectiveEnum.None Then Adventure.LastPerspective = Adventure.Player.Perspective
                private System.Text.RegularExpressions.Match match = rePerspective.Match(sText);
                private string sFirst = match.Groups("first").Value;
                If sFirst.Contains("[") Then sFirst = sFirst.Substring(sFirst.LastIndexOf("[") + 1)
                private string sSecond = match.Groups("second").Value;
                private string sThird = match.Groups("third").Value;
                private string sValue = "";

                If sFirst.Contains("[") || sFirst.Contains("]") || sSecond.Contains("[") || sSecond.Contains("]") || sThird.Contains("[") || sThird.Contains("]") Then Exit While

                switch (GetPerspective(match.Index) ' Adventure.LastPerspective)
                {
                    case PerspectiveEnum.FirstPerson:
                        {
                        sValue = sFirst
                    case PerspectiveEnum.SecondPerson:
                        {
                        sValue = sSecond
                    case PerspectiveEnum.ThirdPerson:
                        {
                        sValue = sThird
                }

                sText = sText.Replace("[" & sFirst & "/" & sSecond & "/" & sThird & "]", sValue)
                'For Each p As PerspectiveOffset In Perspectives
                '    If p.Offset > match.Index Then
                '        p.Offset -= ("[" & sFirst & "/" & sSecond & "/" & sThird & "]").Length - sValue.Length
                '    End If
                'Next
            }

            For Each sGUID As String In dictGUIDs.Keys
                sText = sText.Replace(sGUID, dictGUIDs(sGUID))
            Next;

            return sText;

        }
        catch (Exception ex)
        {
            ErrMsg("ReplaceFunctions error", ex);
            return sText;
        }

    }


    '' E.g. "one. two three", "two", 6 => "one. Two three"
    'Public Function AutoCapitalise(ByVal sWholeText As String, ByVal sText As String, ByVal iOffset As Integer) As String

    '    Dim bCapitalise As Boolean = False

    '    If iOffset = 1 Then
    '        bCapitalise = True
    '    Else
    '        If iOffset > 1 AndAlso sWholeText(iOffset - 2) = vbLf Then
    '            bCapitalise = True
    '        End If
    '        If sWholeText.Length > iOffset - 1 AndAlso iOffset > 1 AndAlso sWholeText(iOffset - 2) = " " Then
    '            If sWholeText(iOffset - 3) = " " Then
    '                If sWholeText(iOffset - 4) = "." Then
    '                    bCapitalise = True
    '                End If
    '            ElseIf sWholeText(iOffset - 3) = "." Then
    '                bCapitalise = True
    '            End If
    '        End If
    '    End If

    '    If bCapitalise Then
    '        Return PCase(sText)
    '    Else
    '        Return sText
    '    End If

    'End Function


    'Adventure.htblCharacters(PronounKeys(PronounKeys.Count - 1).Key).Perspective '
    ' Return the highest perspective that is less the iOffset
    private PerspectiveEnum GetPerspective(int iOffset)
    {

        private int iHighest = 0;
        private PerspectiveEnum ePerspective = PerspectiveEnum.None;

#if Runner
        For Each p As PronounInfo In UserSession.PronounKeys
            if (iOffset >= p.Offset && p.Offset > iHighest)
            {
                ePerspective = Adventure.htblCharacters(p.Key).Perspective
                iHighest = p.Offset
            }
        Next;
#endif

        if (ePerspective <> PerspectiveEnum.None)
        {
            return ePerspective;
        Else
            return Adventure.Player.Perspective;
        }

    }


    ' Convert a number between 0 and 999 into words.
    private _ GroupToWords(int num)
    {
        String;
        Static one_to_nineteen() As String = {"zero", "one", _;
            "two", "three", "four", "five", "six", "seven", _;
            "eight", "nine", "ten", "eleven", "twelve", _;
            "thirteen", "fourteen", "fifteen", "sixteen", _;
            "seventeen", "eighteen", "nineteen"}
        Static multiples_of_ten() As String = {"twenty", _;
            "thirty", "forty", "fifty", "sixty", "seventy", _;
            "eighty", "ninety"}

        ' If the number is 0, return an empty string.
        If num = 0 Then Return ""

        ' Handle the hundreds digit.
        private int digit;
        private string result = "";
        private bool bAnd = false;
        if (num > 99)
        {
            digit = num \ 100
            num = num Mod 100
            bAnd = True
            result = one_to_nineteen(digit) & " hundred"
        }

        ' If num = 0, we have hundreds only.
        If num = 0 Then Return result.Trim()

        If bAnd Then result &= " and"

        ' See if the rest is less than 20.
        if (num < 20)
        {
            ' Look up the correct name.
            result &= " " + one_to_nineteen(num);
        Else
            ' Handle the tens digit.
            digit = num \ 10
            num = num Mod 10
            result &= " " + multiples_of_ten(digit - 2);

            ' Handle the final digit.
            if (num > 0)
            {
                result &= " " + one_to_nineteen(num);
            }
        }

        return result.Trim();
    }


    ' Return a word representation of the whole number value.
    private string NumberToString(string num_str, bool use_us_group_names = true)
    {

        private string result = "";
        private string sIn = num_str;

        if (Not IsNumeric(num_str))
        {
            private New clsVariable v;
            v.SetToExpression(num_str);
            num_str = v.IntValue.ToString
        }

        try
        {

            ' Get the appropiate group names.
            Dim groups() As String
            if (use_us_group_names)
            {
                groups = New String() {"", "thousand", "million", _
                    "billion", "trillion", "quadrillion", _;
                    "quintillion", "sextillion", "septillion", _;
                    "octillion", "nonillion", "decillion", _;
                    "undecillion", "duodecillion", "tredecillion", _;
                    "quattuordecillion", "quindecillion", _;
                    "sexdecillion", "septendecillion", _;
                    "octodecillion", "novemdecillion", _;
                    "vigintillion"}
            Else
                groups = New String() {"", "thousand", "million", _
                    "milliard", "billion", "1000 billion", _;
                    "trillion", "1000 trillion", "quadrillion", _;
                    "1000 quadrillion", "quintillion", "1000 " + _;
                    "quintillion", "sextillion", "1000 sextillion", _;
                    "septillion", "1000 septillion", "octillion", _;
                    "1000 octillion", "nonillion", "1000 " + _;
                    "nonillion", "decillion", "1000 decillion"}
            }

            ' Clean the string a bit.
            ' Remove "$", ",", leading zeros, and
            ' anything after a decimal point.
            private const string CURRENCY = "$";
            private const string SEPARATOR = ",";
            private const string DECIMAL_POINT = ".";
            num_str = num_str.Replace(CURRENCY, _
                "").Replace(SEPARATOR, "");
            num_str = num_str.TrimStart(New Char() {"0"c})
            private int pos = num_str.IndexOf(DECIMAL_POINT);
            if (pos = 0)
            {
                return "zero";
            ElseIf pos > 0 Then
                num_str = num_str.Substring(0, pos - 1)
            }

            ' See how many groups there will be.
            private int num_groups = (num_str.Length + 2) \ 3;

            ' Pad so length is a multiple of 3.
            num_str = num_str.PadLeft(num_groups * 3, " "c)

            ' Process the groups, largest first.
            private int group_num;
            For group_num = num_groups - 1 To 0 Step -1
                ' Get the next three digits.
                private string group_str = num_str.Substring(0, 3);
                num_str = num_str.Substring(3)
                private int group_value = CInt(group_str);

                ' Convert the group into words.
                if (group_value > 0)
                {
                    if (group_num >= groups.Length)
                    {
                        result &= GroupToWords(group_value) + _;
                            " ?, ";
                    Else
                        result &= GroupToWords(group_value) + _;
                            " " + groups(group_num) + ", ";
                    }
                }
            Next group_num;

            ' Remove the trailing ", ".
            if (result.EndsWith(", "))
            {
                result = result.Substring(0, result.Length - 2)
            }

            result = result.Trim()

        }
        catch (Exception ex)
        {
            ErrMsg("NumberToString error parsing """ + sIn + """", ex);
        }

        If result = "" Then result = "zero"
        return result;

    }


    public DirectionsEnum OppositeDirection(DirectionsEnum dir)
    {
        switch (dir)
        {
            case DirectionsEnum.North:
                {
                return DirectionsEnum.South;
            case DirectionsEnum.NorthEast:
                {
                return DirectionsEnum.SouthWest;
            case DirectionsEnum.East:
                {
                return DirectionsEnum.West;
            case DirectionsEnum.SouthEast:
                {
                return DirectionsEnum.NorthWest;
            case DirectionsEnum.South:
                {
                return DirectionsEnum.North;
            case DirectionsEnum.SouthWest:
                {
                return DirectionsEnum.NorthEast;
            case DirectionsEnum.West:
                {
                return DirectionsEnum.East;
            case DirectionsEnum.NorthWest:
                {
                return DirectionsEnum.SouthEast;
            case DirectionsEnum.Up:
                {
                return DirectionsEnum.Down;
            case DirectionsEnum.Down:
                {
                return DirectionsEnum.Up;
            case DirectionsEnum.In:
                {
                return DirectionsEnum.Out;
            case DirectionsEnum.Out:
                {
                return DirectionsEnum.In;
            default:
                {
                return dir;
        }
    }



    public void ReplaceIgnoreCase(string Expression, string Find, string Replacement, Dictionary(Of String Replacements, String)
    {
        If Replacement == null Then Replacement = ""
        private New System.Text.RegularExpressions.Regex(Find.Replace("[", "\[").Replace("]", "\]").Replace("(", "\(").Replace(")", "\)").Replace("|", "\|").Replace("*", "\*").Replace("?", "\?").Replace("$", "\$").Replace("^", "\^").Replace("+", "\+"), System.Text.RegularExpressions.RegexOptions.IgnoreCase) regex;
        If Replacements != null && ! Replacements.ContainsKey(Find) && regex.IsMatch(Expression, Replacement.Replace("$", "$$")) Then Replacements.Add(Find, Replacement)
        return regex.Replace(Expression, Replacement.Replace("$", "$$"), 1) ' Prevent Substitutions (http://msdn.microsoft.com/en-us/library/ewy2t5e0.aspx);
    }


    public string PCase(string sText, bool bStrictLower = false, bool bExpression = false)
    {

        private bool bQuotes = false;
        if (bExpression && sText.StartsWith("""") && sText.EndsWith(""""))
        {
            bQuotes = True
            sText = sText.Substring(1, sText.Length - 2)
        }
        PCase = ""

        try
        {
            if (sText.Length > 0)
            {
                PCase = Left(sText, 1).ToUpper
                if (sText.Length > 1)
                {
                    if (bStrictLower)
                    {
                        PCase &= Right(sText, sText.Length - 1).ToLower;
                    Else
                        PCase &= Right(sText, sText.Length - 1);
                    }
                }
            Else
                return "";
            }
        }
        finally
        {
            If bQuotes Then PCase = """" + PCase + """"
        }

    }


    public string GetFunctionArgs(string sText)
    {

        try
        {
            If sInstr(sText, "[") = 0 || sInstr(sText, "]") = 0 Then Return ""

            ' Work out this bracket chunk, then run ReplaceFunctions on it
            private int iOffset = 1;
            private int iLevel = 1;

            while (iLevel > 0)
            {
                switch (sText.Substring(iOffset, 1))
                {
                    case "[":
                        {
                        iLevel += 1;
                    case "]":
                        {
                        iLevel -= 1;
                    default:
                        {
                        ' Ignore
                }
                iOffset += 1;
            }

            return sText.Substring(1, iOffset - 2);
        }
        catch (Exception ex)
        {
            DisplayError("Error obtaining function arguments from """ + sText + """");
            return "";
        }

    }


    public void Sleep(int iSeconds)
    {
        Threading.Thread.Sleep(iSeconds * 1000);
    }


    public void IntroMessage()
    {
        'MessageBox.Show("This is an early alpha release of ADRIFT 5.0 for <Ken Franklin>, dated 19th Nov 2006.  Please do NOT distribute!", "ADRIFT 5.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    }



    public clsProperty.PropertyOfEnum EnumParsePropertyPropertyOf(string sValue)
    {
        switch (sValue)
        {
            case "Objects":
                {
                return clsProperty.PropertyOfEnum.Objects;
            case "Characters":
                {
                return clsProperty.PropertyOfEnum.Characters;
            case "Locations":
                {
                return clsProperty.PropertyOfEnum.Locations;
            case "AnyItem":
                {
                return clsProperty.PropertyOfEnum.AnyItem;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsProperty.PropertyTypeEnum EnumParsePropertyType(string sValue)
    {
        switch (sValue)
        {
            case "CharacterKey":
                {
                return clsProperty.PropertyTypeEnum.CharacterKey;
            case "Integer":
                {
                return clsProperty.PropertyTypeEnum.Integer;
            case "LocationGroupKey":
                {
                return clsProperty.PropertyTypeEnum.LocationGroupKey;
            case "LocationKey":
                {
                return clsProperty.PropertyTypeEnum.LocationKey;
            case "ObjectKey":
                {
                return clsProperty.PropertyTypeEnum.ObjectKey;
            case "SelectionOnly":
                {
                return clsProperty.PropertyTypeEnum.SelectionOnly;
            case "StateList":
                {
                return clsProperty.PropertyTypeEnum.StateList;
            case "ValueList":
                {
                return clsProperty.PropertyTypeEnum.ValueList;
            case "Text":
                {
                return clsProperty.PropertyTypeEnum.Text;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsAction.MoveCharacterToEnum EnumParseMoveCharacter(string sValue)
    {
        switch (sValue)
        {
            case "InDirection":
                {
                return clsAction.MoveCharacterToEnum.InDirection;
            case "ToLocation":
                {
                return clsAction.MoveCharacterToEnum.ToLocation;
            case "ToLocationGroup":
                {
                return clsAction.MoveCharacterToEnum.ToLocationGroup;
            case "ToLyingOn":
                {
                return clsAction.MoveCharacterToEnum.ToLyingOn;
            case "ToSameLocationAs":
                {
                return clsAction.MoveCharacterToEnum.ToSameLocationAs;
            case "ToSittingOn":
                {
                return clsAction.MoveCharacterToEnum.ToSittingOn;
            case "ToStandingOn":
                {
                return clsAction.MoveCharacterToEnum.ToStandingOn;
            case "ToSwitchWith":
                {
                return clsAction.MoveCharacterToEnum.ToSwitchWith;
            case "InsideObject":
                {
                return clsAction.MoveCharacterToEnum.InsideObject;
            case "OntoCharacter":
                {
                return clsAction.MoveCharacterToEnum.OntoCharacter;
            case "ToParentLocation":
                {
                return clsAction.MoveCharacterToEnum.ToParentLocation;
            case "ToGroup":
                {
                return clsAction.MoveCharacterToEnum.ToGroup;
            case "FromGroup":
                {
                return clsAction.MoveCharacterToEnum.FromGroup;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsAction.MoveLocationToEnum EnumParseMoveLocation(string sValue)
    {
        switch (sValue)
        {
            case "ToGroup":
                {
                return clsAction.MoveLocationToEnum.ToGroup;
            case "FromGroup":
                {
                return clsAction.MoveLocationToEnum.FromGroup;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsAction.SetTasksEnum EnumParseSetTask(string sValue)
    {
        switch (sValue)
        {
            case "Execute":
                {
                return clsAction.SetTasksEnum.Execute;
            case "Unset":
                {
                return clsAction.SetTasksEnum.Unset;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsAction.EndGameEnum EnumParseEndGame(string sValue)
    {
        switch (sValue)
        {
            case "Win":
                {
                return clsAction.EndGameEnum.Win;
            case "Lose":
                {
                return clsAction.EndGameEnum.Lose;
            case "Neutral":
                {
                return clsAction.EndGameEnum.Neutral;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsAction.MoveObjectToEnum EnumParseMoveObject(string sValue)
    {
        switch (sValue)
        {
            case "InsideObject":
                {
                return clsAction.MoveObjectToEnum.InsideObject;
            case "OntoObject":
                {
                return clsAction.MoveObjectToEnum.OntoObject;
            case "ToCarriedBy":
                {
                return clsAction.MoveObjectToEnum.ToCarriedBy;
            case "ToLocation":
                {
                return clsAction.MoveObjectToEnum.ToLocation;
            case "ToLocationGroup":
                {
                return clsAction.MoveObjectToEnum.ToLocationGroup;
            case "ToPartOfCharacter":
                {
                return clsAction.MoveObjectToEnum.ToPartOfCharacter;
            case "ToPartOfObject":
                {
                return clsAction.MoveObjectToEnum.ToPartOfObject;
            case "ToSameLocationAs":
                {
                return clsAction.MoveObjectToEnum.ToSameLocationAs;
            case "ToWornBy":
                {
                return clsAction.MoveObjectToEnum.ToWornBy;
            case "ToGroup":
                {
                return clsAction.MoveObjectToEnum.ToGroup;
            case "FromGroup":
                {
                return clsAction.MoveObjectToEnum.FromGroup;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsRestriction.MustEnum EnumParseMust(string sValue)
    {
        switch (sValue)
        {
            case "Must":
                {
                return clsRestriction.MustEnum.Must;
            case "MustNot":
                {
                return clsRestriction.MustEnum.MustNot;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsRestriction.LocationEnum EnumParseLocation(string sValue)
    {
        if (Adventure.dVersion < 5.000015)
        {
            If sValue = "SeenByCharacter" Then sValue = "HaveBeenSeenByCharacter"
        }
        switch (sValue)
        {
            case "BeInGroup":
                {
                return clsRestriction.LocationEnum.BeInGroup;
            case "HaveBeenSeenByCharacter":
                {
                return clsRestriction.LocationEnum.HaveBeenSeenByCharacter;
            case "HaveProperty":
                {
                return clsRestriction.LocationEnum.HaveProperty;
            case "BeLocation":
                {
                return clsRestriction.LocationEnum.BeLocation;
            case "Exist":
                {
                return clsRestriction.LocationEnum.Exist;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public DirectionsEnum EnumParseDirections(string sValue)
    {
        switch (sValue)
        {
            case "Down":
                {
                return DirectionsEnum.Down;
            case "East":
                {
                return DirectionsEnum.East;
            case "In":
                {
                return DirectionsEnum.In;
            case "North":
                {
                return DirectionsEnum.North;
            case "NorthEast":
                {
                return DirectionsEnum.NorthEast;
            case "NorthWest":
                {
                return DirectionsEnum.NorthWest;
            case "Out":
                {
                return DirectionsEnum.Out;
            case "South":
                {
                return DirectionsEnum.South;
            case "SouthEast":
                {
                return DirectionsEnum.SouthEast;
            case "SouthWest":
                {
                return DirectionsEnum.SouthWest;
            case "Up":
                {
                return DirectionsEnum.Up;
            case "West":
                {
                return DirectionsEnum.West;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsTask.BeforeAfterEnum EnumParseBeforeAfter(string sValue)
    {
        switch (sValue)
        {
            case "After":
                {
                return clsTask.BeforeAfterEnum.After;
            case "Before":
                {
                return clsTask.BeforeAfterEnum.Before;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsRestriction.VariableEnum EnumParseVariable(string sValue)
    {
        switch (sValue)
        {
            case "EqualTo":
                {
                return clsRestriction.VariableEnum.EqualTo;
            case "GreaterThan":
                {
                return clsRestriction.VariableEnum.GreaterThan;
            case "GreaterThanOrEqualTo":
                {
                return clsRestriction.VariableEnum.GreaterThanOrEqualTo;
            case "LessThan":
                {
                return clsRestriction.VariableEnum.LessThan;
            case "LessThanOrEqualTo":
                {
                return clsRestriction.VariableEnum.LessThanOrEqualTo;
            case "Contain":
                {
                return clsRestriction.VariableEnum.Contain;
            default:
                {
                return null;
        }
    }


    public clsRestriction.ItemEnum EnumParseItem(string sValue)
    {
        switch (sValue)
        {
            case "BeAtLocation":
                {
                return clsRestriction.ItemEnum.BeAtLocation;
            case "BeCharacter":
                {
                return clsRestriction.ItemEnum.BeCharacter;
            case "BeInGroup":
                {
                return clsRestriction.ItemEnum.BeInGroup;
            case "BeInSameLocationAsCharacter":
                {
                return clsRestriction.ItemEnum.BeInSameLocationAsCharacter;
            case "BeInSameLocationAsObject":
                {
                return clsRestriction.ItemEnum.BeInSameLocationAsObject;
            case "BeInsideObject":
                {
                return clsRestriction.ItemEnum.BeInsideObject;
            case "BeLocation":
                {
                return clsRestriction.ItemEnum.BeLocation;
            case "BeObject":
                {
                return clsRestriction.ItemEnum.BeObject;
            case "BeOnCharacter":
                {
                return clsRestriction.ItemEnum.BeOnCharacter;
            case "BeOnObject":
                {
                return clsRestriction.ItemEnum.BeOnObject;
            case "BeType":
                {
                return clsRestriction.ItemEnum.BeType;
            case "Exist":
                {
                return clsRestriction.ItemEnum.Exist;
            case "HaveProperty":
                {
                return clsRestriction.ItemEnum.HaveProperty;
            default:
                {
                TODO();
        }
    }


    public clsRestriction.CharacterEnum EnumParseCharacter(string sValue)
    {
        switch (sValue)
        {
            case "BeAlone":
                {
                return clsRestriction.CharacterEnum.BeAlone;
            case "BeAloneWith":
                {
                return clsRestriction.CharacterEnum.BeAloneWith;
            case "BeAtLocation":
                {
                return clsRestriction.CharacterEnum.BeAtLocation;
            case "BeCharacter":
                {
                return clsRestriction.CharacterEnum.BeCharacter;
            case "BeInConversationWith":
                {
                return clsRestriction.CharacterEnum.BeInConversationWith;
            case "Exist":
                {
                return clsRestriction.CharacterEnum.Exist;
            case "HaveRouteInDirection":
                {
                return clsRestriction.CharacterEnum.HaveRouteInDirection;
            case "HaveSeenCharacter":
                {
                return clsRestriction.CharacterEnum.HaveSeenCharacter;
            case "HaveSeenLocation":
                {
                return clsRestriction.CharacterEnum.HaveSeenLocation;
            case "HaveSeenObject":
                {
                return clsRestriction.CharacterEnum.HaveSeenObject;
            case "BeHoldingObject":
                {
                return clsRestriction.CharacterEnum.BeHoldingObject;
            case "BeInSameLocationAsCharacter":
                {
                return clsRestriction.CharacterEnum.BeInSameLocationAsCharacter;
            case "BeInSameLocationAsObject":
                {
                return clsRestriction.CharacterEnum.BeInSameLocationAsObject;
            case "BeLyingOnObject":
                {
                return clsRestriction.CharacterEnum.BeLyingOnObject;
            case "BeMemberOfGroup":
            case "BeInGroup":
                {
                return clsRestriction.CharacterEnum.BeInGroup;
            case "BeOfGender":
                {
                return clsRestriction.CharacterEnum.BeOfGender;
            case "BeSittingOnObject":
                {
                return clsRestriction.CharacterEnum.BeSittingOnObject;
            case "BeStandingOnObject":
                {
                return clsRestriction.CharacterEnum.BeStandingOnObject;
            case "BeWearingObject":
                {
                return clsRestriction.CharacterEnum.BeWearingObject;
            case "BeWithinLocationGroup":
                {
                return clsRestriction.CharacterEnum.BeWithinLocationGroup;
            case "HaveProperty":
                {
                return clsRestriction.CharacterEnum.HaveProperty;
            case "BeInPosition":
                {
                return clsRestriction.CharacterEnum.BeInPosition;
            case "BeInsideObject":
                {
                return clsRestriction.CharacterEnum.BeInsideObject;
            case "BeOnObject":
                {
                return clsRestriction.CharacterEnum.BeOnObject;
            case "BeOnCharacter":
                {
                return clsRestriction.CharacterEnum.BeOnCharacter;
            case "BeVisibleToCharacter":
                {
                return clsRestriction.CharacterEnum.BeVisibleToCharacter;
            default:
                {
                'Throw New Exception("Value """ & sValue & """ not parsed!")
                return null;
        }

    }


    public clsRestriction.ObjectEnum EnumParseObject(string sValue)
    {
        switch (sValue)
        {
            case "BeExactText":
                {
                return clsRestriction.ObjectEnum.BeExactText;
            case "BeAtLocation":
                {
                return clsRestriction.ObjectEnum.BeAtLocation;
            case "BeHeldByCharacter":
                {
                return clsRestriction.ObjectEnum.BeHeldByCharacter;
            case "BeHidden":
                {
                return clsRestriction.ObjectEnum.BeHidden;
            case "BeInGroup":
                {
                return clsRestriction.ObjectEnum.BeInGroup;
            case "BeInsideObject":
                {
                return clsRestriction.ObjectEnum.BeInsideObject;
            case "BeInState":
                {
                return clsRestriction.ObjectEnum.BeInState;
            case "BeOnObject":
                {
                return clsRestriction.ObjectEnum.BeOnObject;
            case "BePartOfCharacter":
                {
                return clsRestriction.ObjectEnum.BePartOfCharacter;
            case "BePartOfObject":
                {
                return clsRestriction.ObjectEnum.BePartOfObject;
            case "BeVisibleToCharacter":
                {
                return clsRestriction.ObjectEnum.BeVisibleToCharacter;
            case "BeWornByCharacter":
                {
                return clsRestriction.ObjectEnum.BeWornByCharacter;
            case "Exist":
                {
                return clsRestriction.ObjectEnum.Exist;
            case "HaveBeenSeenByCharacter":
                {
                return clsRestriction.ObjectEnum.HaveBeenSeenByCharacter;
            case "HaveProperty":
                {
                return clsRestriction.ObjectEnum.HaveProperty;
            case "BeObject":
                {
                return clsRestriction.ObjectEnum.BeObject;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsEvent.SubEvent.MeasureEnum EnumParseSubEventMeasure(string sValue)
    {
        switch (sValue)
        {
            case "Turns":
                {
                return clsEvent.SubEvent.MeasureEnum.Turns;
            case "Seconds":
                {
                return clsEvent.SubEvent.MeasureEnum.Seconds;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }

    public clsEvent.SubEvent.WhatEnum EnumParseSubEventWhat(string sValue)
    {
        switch (sValue)
        {
            case "DisplayMessage":
                {
                return clsEvent.SubEvent.WhatEnum.DisplayMessage;
            case "ExecuteTask":
                {
                return clsEvent.SubEvent.WhatEnum.ExecuteTask;
            case "SetLook":
                {
                return clsEvent.SubEvent.WhatEnum.SetLook;
            case "UnsetTask":
                {
                return clsEvent.SubEvent.WhatEnum.UnsetTask;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsTask.TaskTypeEnum EnumParseTaskType(string sValue)
    {
        switch (sValue)
        {
            case "General":
                {
                return clsTask.TaskTypeEnum.General;
            case "Specific":
                {
                return clsTask.TaskTypeEnum.Specific;
            case "System":
                {
                return clsTask.TaskTypeEnum.System;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public ReferencesType EnumParseSpecificType(string sValue)
    {
        switch (sValue)
        {
            case "Character":
                {
                return ReferencesType.Character;
            case "Direction":
                {
                return ReferencesType.Direction;
            case "Number":
                {
                return ReferencesType.Number;
            case "Object":
                {
                return ReferencesType.Object;
            case "Text":
                {
                return ReferencesType.Text;
            case "Location":
                {
                return ReferencesType.Location;
            case "Item":
                {
                return ReferencesType.Item;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsCharacter.CharacterTypeEnum EnumParseCharacterType(string sValue)
    {
        switch (sValue)
        {
            case "NonPlayer":
                {
                return clsCharacter.CharacterTypeEnum.NonPlayer;
            case "Player":
                {
                return clsCharacter.CharacterTypeEnum.Player;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsVariable.VariableTypeEnum EnumParseVariableType(string sValue)
    {
        switch (sValue)
        {
            case "Numeric":
                {
                return clsVariable.VariableTypeEnum.Numeric;
            case "Text":
                {
                return clsVariable.VariableTypeEnum.Text;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }


    public clsGroup.GroupTypeEnum EnumParseGroupType(string sValue)
    {
        switch (sValue)
        {
            case "Characters":
                {
                return clsGroup.GroupTypeEnum.Characters;
            case "Locations":
                {
                return clsGroup.GroupTypeEnum.Locations;
            case "Objects":
                {
                return clsGroup.GroupTypeEnum.Objects;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }



    public string WriteEnum(clsAction.EndGameEnum e)
    {
        switch (e)
        {
            case clsAction.EndGameEnum.Win:
                {
                return "Win";
            case clsAction.EndGameEnum.Lose:
                {
                return "Lose";
            case clsAction.EndGameEnum.Neutral:
                {
                return "Neutral";
            default:
                {
                return null;
        }
    }

    public string WriteEnum(clsEvent.SubEvent.WhatEnum w)
    {
        switch (w)
        {
            case clsEvent.SubEvent.WhatEnum.DisplayMessage:
                {
                return "DisplayMessage";
            case clsEvent.SubEvent.WhatEnum.ExecuteTask:
                {
                return "ExecuteTask";
            case clsEvent.SubEvent.WhatEnum.SetLook:
                {
                return "SetLook";
            case clsEvent.SubEvent.WhatEnum.UnsetTask:
                {
                return "UnsetTask";
            default:
                {
                return null;
        }
    }

    public string WriteEnum(clsEvent.SubEvent.MeasureEnum m)
    {
        switch (m)
        {
            case clsEvent.SubEvent.MeasureEnum.Turns:
                {
                return "Turns";
            case clsEvent.SubEvent.MeasureEnum.Seconds:
                {
                return "Seconds";
            default:
                {
                return null;
        }
    }

    public string WriteEnum(DirectionsEnum d)
    {
        switch (d)
        {
            case DirectionsEnum.Down:
                {
                return "Down";
            case DirectionsEnum.East:
                {
                return "East";
            case DirectionsEnum.In:
                {
                return "In";
            case DirectionsEnum.North:
                {
                return "North";
            case DirectionsEnum.NorthEast:
                {
                return "NorthEast";
            case DirectionsEnum.NorthWest:
                {
                return "NorthWest";
            case DirectionsEnum.Out:
                {
                return "Out";
            case DirectionsEnum.South:
                {
                return "South";
            case DirectionsEnum.SouthEast:
                {
                return "SouthEast";
            case DirectionsEnum.SouthWest:
                {
                return "SouthWest";
            case DirectionsEnum.Up:
                {
                return "Up";
            case DirectionsEnum.West:
                {
                return "West";
        }
        return null;
    }

    public string WriteEnum(clsProperty.PropertyOfEnum p)
    {
        switch (p)
        {
            case clsProperty.PropertyOfEnum.Objects:
                {
                return "Objects";
            case clsProperty.PropertyOfEnum.Characters:
                {
                return "Characters";
            case clsProperty.PropertyOfEnum.Locations:
                {
                return "Locations";
            case clsProperty.PropertyOfEnum.AnyItem:
                {
                return "AnyItem";
        }
        return null;
    }

    public string WriteEnum(clsProperty.PropertyTypeEnum t)
    {
        switch (t)
        {
            case clsProperty.PropertyTypeEnum.CharacterKey:
                {
                return "CharacterKey";
            case clsProperty.PropertyTypeEnum.Integer:
                {
                return "Integer";
            case clsProperty.PropertyTypeEnum.LocationGroupKey:
                {
                return "LocationGroupKey";
            case clsProperty.PropertyTypeEnum.LocationKey:
                {
                return "LocationKey";
            case clsProperty.PropertyTypeEnum.ObjectKey:
                {
                return "ObjectKey";
            case clsProperty.PropertyTypeEnum.SelectionOnly:
                {
                return "SelectionOnly";
            case clsProperty.PropertyTypeEnum.StateList:
                {
                return "StateList";
            case clsProperty.PropertyTypeEnum.ValueList:
                {
                return "ValueList";
            case clsProperty.PropertyTypeEnum.Text:
                {
                return "Text";
        }
        return null;
    }

    public string WriteEnum(clsRestriction.MustEnum m)
    {
        switch (m)
        {
            case clsRestriction.MustEnum.Must:
                {
                return "Must";
            case clsRestriction.MustEnum.MustNot:
                {
                return "MustNot";
        }
        return null;
    }

    public string WriteEnum(clsRestriction.LocationEnum l)
    {
        switch (l)
        {
            case clsRestriction.LocationEnum.BeInGroup:
                {
                return "BeInGroup";
            case clsRestriction.LocationEnum.HaveBeenSeenByCharacter:
                {
                return "HaveBeenSeenByCharacter";
            case clsRestriction.LocationEnum.HaveProperty:
                {
                return "HaveProperty";
            case clsRestriction.LocationEnum.BeLocation:
                {
                return "BeLocation";
            case clsRestriction.LocationEnum.Exist:
                {
                return "Exist";
        }
        return null;
    }

    public string WriteEnum(clsRestriction.ObjectEnum o)
    {
        switch (o)
        {
            case clsRestriction.ObjectEnum.BeAtLocation:
                {
                return "BeAtLocation";
            case clsRestriction.ObjectEnum.BeHeldByCharacter:
                {
                return "BeHeldByCharacter";
            case clsRestriction.ObjectEnum.BeInGroup:
                {
                return "BeInGroup";
            case clsRestriction.ObjectEnum.BeInsideObject:
                {
                return "BeInsideObject";
            case clsRestriction.ObjectEnum.BeInState:
                {
                return "BeInState";
            case clsRestriction.ObjectEnum.BeOnObject:
                {
                return "BeOnObject";
            case clsRestriction.ObjectEnum.BePartOfCharacter:
                {
                return "BePartOfCharacter";
            case clsRestriction.ObjectEnum.BePartOfObject:
                {
                return "BePartOfObject";
            case clsRestriction.ObjectEnum.BeVisibleToCharacter:
                {
                return "BeVisibleToCharacter";
            case clsRestriction.ObjectEnum.BeWornByCharacter:
                {
                return "BeWornByCharacter";
            case clsRestriction.ObjectEnum.Exist:
                {
                return "Exist";
            case clsRestriction.ObjectEnum.HaveBeenSeenByCharacter:
                {
                return "HaveBeenSeenByCharacter";
            case clsRestriction.ObjectEnum.HaveProperty:
                {
                return "HaveProperty";
            case clsRestriction.ObjectEnum.BeExactText:
                {
                return "BeExactText";
            case clsRestriction.ObjectEnum.BeHidden:
                {
                return "BeHidden";
            case clsRestriction.ObjectEnum.BeObject:
                {
                return "BeObject";
        }
        return null;
    }

    public string WriteEnum(clsRestriction.ItemEnum c)
    {
        switch (c)
        {
            case clsRestriction.ItemEnum.BeAtLocation:
                {
                return "BeAtLocation";
            case clsRestriction.ItemEnum.BeCharacter:
                {
                return "BeCharacter";
            case clsRestriction.ItemEnum.BeInSameLocationAsCharacter:
                {
                return "BeInSameLocationAsCharacter";
            case clsRestriction.ItemEnum.BeInSameLocationAsObject:
                {
                return "BeInSameLocationAsObject";
            case clsRestriction.ItemEnum.BeInsideObject:
                {
                return "BeInsideObject";
            case clsRestriction.ItemEnum.BeLocation:
                {
                return "BeLocation";
            case clsRestriction.ItemEnum.BeInGroup:
                {
                return "BeInGroup";
            case clsRestriction.ItemEnum.BeObject:
                {
                return "BeObject";
            case clsRestriction.ItemEnum.BeOnCharacter:
                {
                return "BeOnCharacter";
            case clsRestriction.ItemEnum.Exist:
                {
                return "Exist";
            case clsRestriction.ItemEnum.HaveProperty:
                {
                return "HaveProperty";
            case clsRestriction.ItemEnum.BeType:
                {
                return "BeType";
            default:
                {
                TODO();
        }
        return null;
    }


    public string WriteEnum(clsRestriction.CharacterEnum c)
    {
        switch (c)
        {
            case clsRestriction.CharacterEnum.BeAlone:
                {
                return "BeAlone";
            case clsRestriction.CharacterEnum.BeAloneWith:
                {
                return "BeAloneWith";
            case clsRestriction.CharacterEnum.BeAtLocation:
                {
                return "BeAtLocation";
            case clsRestriction.CharacterEnum.BeCharacter:
                {
                return "BeCharacter";
            case clsRestriction.CharacterEnum.BeInConversationWith:
                {
                return "BeInConversationWith";
            case clsRestriction.CharacterEnum.Exist:
                {
                return "Exist";
            case clsRestriction.CharacterEnum.HaveRouteInDirection:
                {
                return "HaveRouteInDirection";
            case clsRestriction.CharacterEnum.HaveSeenCharacter:
                {
                return "HaveSeenCharacter";
            case clsRestriction.CharacterEnum.HaveSeenLocation:
                {
                return "HaveSeenLocation";
            case clsRestriction.CharacterEnum.HaveSeenObject:
                {
                return "HaveSeenObject";
            case clsRestriction.CharacterEnum.BeHoldingObject:
                {
                return "BeHoldingObject";
            case clsRestriction.CharacterEnum.BeInSameLocationAsCharacter:
                {
                return "BeInSameLocationAsCharacter";
            case clsRestriction.CharacterEnum.BeInSameLocationAsObject:
                {
                return "BeInSameLocationAsObject";
            case clsRestriction.CharacterEnum.BeLyingOnObject:
                {
                return "BeLyingOnObject";
            case clsRestriction.CharacterEnum.BeInGroup:
                {
                return "BeInGroup";
            case clsRestriction.CharacterEnum.BeOfGender:
                {
                return "BeOfGender";
            case clsRestriction.CharacterEnum.BeSittingOnObject:
                {
                return "BeSittingOnObject";
            case clsRestriction.CharacterEnum.BeStandingOnObject:
                {
                return "BeStandingOnObject";
            case clsRestriction.CharacterEnum.BeWearingObject:
                {
                return "BeWearingObject";
            case clsRestriction.CharacterEnum.BeWithinLocationGroup:
                {
                return "BeWithinLocationGroup";
            case clsRestriction.CharacterEnum.HaveProperty:
                {
                return "HaveProperty";
            case clsRestriction.CharacterEnum.BeInPosition:
                {
                return "BeInPosition";
            case clsRestriction.CharacterEnum.BeInsideObject:
                {
                return "BeInsideObject";
            case clsRestriction.CharacterEnum.BeOnObject:
                {
                return "BeOnObject";
            case clsRestriction.CharacterEnum.BeOnCharacter:
                {
                return "BeOnCharacter";
            case clsRestriction.CharacterEnum.BeVisibleToCharacter:
                {
                return "BeVisibleToCharacter";
        }
        return null;
    }

    public string WriteEnum(clsRestriction.VariableEnum v)
    {
        switch (v)
        {
            case clsRestriction.VariableEnum.EqualTo:
                {
                return "EqualTo";
            case clsRestriction.VariableEnum.GreaterThan:
                {
                return "GreaterThan";
            case clsRestriction.VariableEnum.GreaterThanOrEqualTo:
                {
                return "GreaterThanOrEqualTo";
            case clsRestriction.VariableEnum.LessThan:
                {
                return "LessThan";
            case clsRestriction.VariableEnum.LessThanOrEqualTo:
                {
                return "LessThanOrEqualTo";
            case clsRestriction.VariableEnum.Contain:
                {
                return "Contain";
        }
        return null;
    }

    public string WriteEnum(clsTask.BeforeAfterEnum ba)
    {
        switch (ba)
        {
            case clsTask.BeforeAfterEnum.After:
                {
                return "After";
            case clsTask.BeforeAfterEnum.Before:
                {
                return "Before";
        }
        return null;
    }

    public string WriteEnum(clsAction.MoveCharacterToEnum mc)
    {
        switch (mc)
        {
            case clsAction.MoveCharacterToEnum.InDirection:
                {
                return "InDirection";
            case clsAction.MoveCharacterToEnum.ToLocation:
                {
                return "ToLocation";
            case clsAction.MoveCharacterToEnum.ToLocationGroup:
                {
                return "ToLocationGroup";
            case clsAction.MoveCharacterToEnum.ToLyingOn:
                {
                return "ToLyingOn";
            case clsAction.MoveCharacterToEnum.ToSameLocationAs:
                {
                return "ToSameLocationAs";
            case clsAction.MoveCharacterToEnum.ToSittingOn:
                {
                return "ToSittingOn";
            case clsAction.MoveCharacterToEnum.ToStandingOn:
                {
                return "ToStandingOn";
            case clsAction.MoveCharacterToEnum.ToSwitchWith:
                {
                return "ToSwitchWith";
            case clsAction.MoveCharacterToEnum.InsideObject:
                {
                return "InsideObject";
            case clsAction.MoveCharacterToEnum.OntoCharacter:
                {
                return "OntoCharacter";
            case clsAction.MoveCharacterToEnum.ToParentLocation:
                {
                return "ToParentLocation";
            case clsAction.MoveCharacterToEnum.ToGroup:
                {
                return "ToGroup";
            case clsAction.MoveCharacterToEnum.FromGroup:
                {
                return "FromGroup";
        }
        return null;
    }

    public string WriteEnum(clsAction.SetTasksEnum st)
    {
        switch (st)
        {
            case clsAction.SetTasksEnum.Execute:
                {
                return "Execute";
            case clsAction.SetTasksEnum.Unset:
                {
                return "Unset";
        }
        return null;
    }

    public string WriteEnum(clsAction.MoveLocationToEnum ml)
    {
        switch (ml)
        {
            case clsAction.MoveLocationToEnum.FromGroup:
                {
                return "FromGroup";
            case clsAction.MoveLocationToEnum.ToGroup:
                {
                return "ToGroup";
        }
        return null;
    }

    public string WriteEnum(clsAction.MoveObjectToEnum mo)
    {
        switch (mo)
        {
            case clsAction.MoveObjectToEnum.InsideObject:
                {
                return "InsideObject";
            case clsAction.MoveObjectToEnum.OntoObject:
                {
                return "OntoObject";
            case clsAction.MoveObjectToEnum.ToCarriedBy:
                {
                return "ToCarriedBy";
            case clsAction.MoveObjectToEnum.ToLocation:
                {
                return "ToLocation";
            case clsAction.MoveObjectToEnum.ToLocationGroup:
                {
                return "ToLocationGroup";
            case clsAction.MoveObjectToEnum.ToPartOfCharacter:
                {
                return "ToPartOfCharacter";
            case clsAction.MoveObjectToEnum.ToPartOfObject:
                {
                return "ToPartOfObject";
            case clsAction.MoveObjectToEnum.ToSameLocationAs:
                {
                return "ToSameLocationAs";
            case clsAction.MoveObjectToEnum.ToWornBy:
                {
                return "ToWornBy";
            case clsAction.MoveObjectToEnum.ToGroup:
                {
                return "ToGroup";
            case clsAction.MoveObjectToEnum.FromGroup:
                {
                return "FromGroup";
        }
        return null;
    }

    public string WriteEnum(clsTask.TaskTypeEnum tt)
    {
        switch (tt)
        {
            case clsTask.TaskTypeEnum.General:
                {
                return "General";
            case clsTask.TaskTypeEnum.Specific:
                {
                return "Specific";
            case clsTask.TaskTypeEnum.System:
                {
                return "System";
        }
        return null;
    }

    public string WriteEnum(ReferencesType s)
    {
        switch (s)
        {
            case ReferencesType.Character:
                {
                return "Character";
            case ReferencesType.Direction:
                {
                return "Direction";
            case ReferencesType.Number:
                {
                return "Number";
            case ReferencesType.Object:
                {
                return "Object";
            case ReferencesType.Text:
                {
                return "Text";
            case ReferencesType.Location:
                {
                return "Location";
            case ReferencesType.Item:
                {
                return "Item";
        }
        return null;
    }

    public string WriteEnum(clsCharacter.CharacterTypeEnum ct)
    {
        switch (ct)
        {
            case clsCharacter.CharacterTypeEnum.NonPlayer:
                {
                return "NonPlayer";
            case clsCharacter.CharacterTypeEnum.Player:
                {
                return "Player";
        }
        return null;
    }

    public string WriteEnum(clsVariable.VariableTypeEnum vt)
    {
        switch (vt)
        {
            case clsVariable.VariableTypeEnum.Numeric:
                {
                return "Numeric";
            case clsVariable.VariableTypeEnum.Text:
                {
                return "Text";
        }
        return null;
    }

    public string WriteEnum(clsGroup.GroupTypeEnum gt)
    {
        switch (gt)
        {
            case clsGroup.GroupTypeEnum.Characters:
                {
                return "Characters";
            case clsGroup.GroupTypeEnum.Locations:
                {
                return "Locations";
            case clsGroup.GroupTypeEnum.Objects:
                {
                return "Objects";
        }
        return null;
    }

    ' cos Val() is not international-friendly...
    public decimal SafeDec(object Expression)
    {
        try
        {
            If Expression == null || IsDBNull(Expression) Then Return 0
            If IsNumeric(Expression) Then Return CDec(Expression) Else Return 0
        }
        catch (Exception ex)
        {
            ErrMsg("SafeDec error with expression <" + Expression.ToString + ">", ex);
            return 0;
        }
    }
    public double SafeDbl(object Expression)
    {
        try
        {
            private string sExpression = SafeString(Expression);
            If sExpression.Contains(",") Then sExpression = sExpression.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator)
            If sExpression.Contains(".") Then sExpression = sExpression.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)

            If sExpression = "" || ! IsNumeric(sExpression) Then Return 0
            private double df = CDbl(sExpression);
            If Double.IsInfinity(df) || Double.IsNaN(df) Then Return 0 Else Return df
        }
        catch (Exception ex)
        {
            ErrMsg("SafeDbl error with expression <" + Expression.ToString + ">", ex);
            return 0;
        }
    }
    public int SafeInt(object Expression)
    {
        try
        {
            If Expression == null || IsDBNull(Expression) Then Return 0
            If IsNumeric(Expression) Then Return CInt(Int(Expression)) Else Return 0
        }
        catch (OverflowException exOF)
        {
            return Integer.MaxValue;
        }
        catch (Exception ex)
        {
            ErrMsg("SafeInt error with expression <" + Expression.ToString + ">", ex);
            return 0;
        }
    }
    public bool SafeBool(object Expression)
    {
        try
        {
            If Expression == null || IsDBNull(Expression) Then Return false
            switch (Expression.ToString.ToUpper)
            {
                case true.ToString.ToUpper:
                    {
                    return true;
                case false.ToString.ToUpper:
                    {
                    return false;
                default:
                    {
                    try
                    {
                        return CBool(Expression);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
            }
        }
        catch (Exception ex)
        {
            ErrMsg("SafeBool error with expression <" + Expression.ToString + ">", ex);
            return false;
        }
    }
    public string SafeString(object Expression)
    {
        try
        {
            If Expression == null || IsDBNull(Expression) Then Return ""
            return Expression.ToString;
        }
        catch (Exception ex)
        {
            ErrMsg("SafeString error with expression <" + Expression.ToString + ">", ex);
            return "";
        }
    }
    public DateTime SafeDate(object Expression)
    {
        try
        {
            If Expression == null || IsDBNull(Expression) || ! IsDate(Expression) Then Return New Date
            return CDate(Expression);
        }
        catch (Exception ex)
        {
            ErrMsg("SafeDate error with expression <" + Expression.ToString + ">", ex);
            return new Date;
        }
    }


public class EventOrWalkControl
    {
        Implements ICloneable;

public enum ControlEnum
        {
            Start;
            [Stop];
            Suspend;
            [Resume];
        }
        public ControlEnum eControl;

public enum CompleteOrNotEnum
        {
            Completion;
            UnCompletion;
        }
        public CompleteOrNotEnum eCompleteOrNot;

        public string sTaskKey;

        private Object Implements System.ICloneable.Clone Clone()
        {
            return Me.MemberwiseClone;
        }
        public EventOrWalkControl CloneMe()
        {
            return CType(Me.Clone, EventOrWalkControl);
        }

    }


public class FromTo
    {
        Implements ICloneable;

        public int iFrom;
        public int iTo;
        private int iValue = Integer.MinValue;

        public int Value { get; }
            {
                get
                {
                if (iValue = Integer.MinValue)
                {
                    iValue = Random(iFrom, iTo) ' CInt(Rnd() * (iTo - iFrom)) + iFrom
                }
                return iValue;
            }
        }

        public void Reset()
        {
            iValue = Integer.MinValue
        }

        private Object Implements System.ICloneable.Clone Clone()
        {
            return Me.MemberwiseClone;
        }
        public FromTo CloneMe()
        {
            return CType(Clone(), FromTo);
        }

        Public Shadows ReadOnly Property ToString() As String
            {
                get
                {
                if (iFrom = iTo)
                {
                    return iFrom.ToString;
                Else
                    return iFrom.ToString + " to " + iTo.ToString;
                }
            }
        }

    }


    <DebuggerDisplay("{Description}")> _;
public class SingleDescription
    {
        Implements ICloneable;

public enum DisplayWhenEnum
        {
            StartDescriptionWithThis;
            StartAfterDefaultDescription;
            AppendToPreviousDescription;
        }

        internal New RestrictionArrayList Restrictions;
        internal DisplayWhenEnum eDisplayWhen = DisplayWhenEnum.AppendToPreviousDescription;
        internal string sTabLabel = "";
        public string Description = "";
        internal bool DisplayOnce = false;
        internal Boolean = False ReturnToDefault { get; set; }
        internal bool Displayed;

        private Object Implements System.ICloneable.Clone Clone()
        {
            return Me.MemberwiseClone;
        }
        public SingleDescription CloneMe()
        {
            return CType(Clone(), SingleDescription);
        }

    }


public class Description
    {
        Inherits List(Of SingleDescription);

        public void New()
        {
            Me.New("");
        }
        public void New(string sDescription)
        {

            private New SingleDescription sd;
            With sd;
                sd.Description = sDescription;
                sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
            }
            MyBase.Add(sd);

        }

        'Shadows Sub Add(ByVal desc As SingleDescription)
        '    MyBase.Add(desc)
        'End Sub

        'Shadows Sub Remove(ByVal desc As SingleDescription)
        '    MyBase.Remove(Dir)
        'End Sub

        'Default Shadows Property Item(ByVal idx As Integer) As SingleDescription
        '    Get
        '        Return CType(MyBase.Item(idx), SingleDescription)
        '    End Get
        '    Set(ByVal Value As SingleDescription)
        '        MyBase.Item(idx) = Value
        '    End Set
        'End Property


        public int ReferencesKey(string sKey)
        {

            private int iCount = 0;
            For Each m As SingleDescription In Me
                For Each r As clsRestriction In m.Restrictions
                    If r.ReferencesKey(sKey) Then iCount += 1
                Next;
            Next;
            return iCount;

        }


        public bool DeleteKey(string sKey)
        {

            For Each m As SingleDescription In Me
                If ! m.Restrictions.DeleteKey(sKey) Then Return false
            Next;

            return true;

        }


        'Should we add spaces between one desctiption tab and another
        private bool AddSpace(string sText)
        {

            If sText = "" Then Return false
            If sText.EndsWith(" ") || sText.EndsWith(vbLf) Then Return false

            ' Add spaces after end of sentences
            If sText.EndsWith(".") || sText.EndsWith("!") || sText.EndsWith("?") Then Return true

            ' If it's a function then add a space -small chance the function could evaluate to "", but we'll take that chance
            If sText.EndsWith("%") Then Return true

            ' If text ends in an OO function, return True
            If New System.Text.RegularExpressions.Regex(".*?(%?[A-Za-z][\w\|_-]*%?)(\.%?[A-Za-z][\w\|_-]*%?(\([A-Za-z ,_]+?\))?)+").IsMatch(sText) Then Return true

            ' Otherwise return False
            return false;

        }


        ' If bTesting is set, we're just testing the value, so don't mark text as having been output
        Public Shadows ReadOnly Property ToString(Optional ByVal bTesting As Boolean = false) As String
            {
                get
                {
                private New System.Text.StringBuilder sb;

#if Runner
                private string sRestrictionTextIn = UserSession.sRestrictionText;
                private int iRestNumIn = UserSession.iRestNum;
                private string sRouteErrorIn = UserSession.sRouteError;

                try
                {
                    For Each sd As SingleDescription In Me
                        With sd;
                            if (Not sd.DisplayOnce || Not sd.Displayed)
                            {
                                private bool bDisplayed = false;
                                if (sd.Restrictions.Count = 0 || UserSession.PassRestrictions(sd.Restrictions))
                                {
                                    switch (sd.eDisplayWhen)
                                    {
                                        case SingleDescription.DisplayWhenEnum.AppendToPreviousDescription:
                                            {
                                            If AddSpace(sb.ToString) Then sb.Append("  ")
                                            sb.Append("<>" + sd.Description);
                                        case SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription:
                                            {
                                            if (sd Is Me(0))
                                            {
                                                sb = New System.Text.StringBuilder(sd.Description)
                                            Else
                                                private string sDefault = Me(0).Description;
                                                If AddSpace(sDefault) Then sDefault &= "  "
                                                sb = New System.Text.StringBuilder(sDefault & sd.Description)
                                            }
                                        case SingleDescription.DisplayWhenEnum.StartDescriptionWithThis:
                                            {
                                            sb = New System.Text.StringBuilder(sd.Description)
                                    }
                                    bDisplayed = True
                                Else
                                    if (UserSession.sRestrictionText <> "")
                                    {
                                        switch (sd.eDisplayWhen)
                                        {
                                            case SingleDescription.DisplayWhenEnum.AppendToPreviousDescription:
                                                {
                                                If AddSpace(sb.ToString) Then sb.Append("  ")
                                                sb.Append("<>" + UserSession.sRestrictionText);
                                            case SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription:
                                                {
                                                if (sd Is Me(0))
                                                {
                                                    sb = New System.Text.StringBuilder(UserSession.sRestrictionText)
                                                Else
                                                    private string sDefault = Me(0).Description;
                                                    If AddSpace(sDefault) Then sDefault &= "  "
                                                    sb = New System.Text.StringBuilder(sDefault & UserSession.sRestrictionText)
                                                }
                                            case SingleDescription.DisplayWhenEnum.StartDescriptionWithThis:
                                                {
                                                sb = New System.Text.StringBuilder(UserSession.sRestrictionText)
                                        }
                                    }
                                }
                                if (.DisplayOnce)
                                {
                                    ' Is this right, or should it mark Displayed = True if any text is output?
                                    if (Not (bTesting || UserSession.bTestingOutput) && bDisplayed)
                                    {
                                        sd.Displayed = true;
                                        if (sd.ReturnToDefault)
                                        {
                                            For Each sd2 As SingleDescription In Me
                                                sd2.Displayed = false;
                                                If sd2 == sd Then Exit For
                                            Next;
                                        }
                                    }
                                    return sb.ToString;
                                }
                            }
                        }
                    Next;

                Catch
                }
                finally
                {
                    UserSession.sRestrictionText = sRestrictionTextIn;
                    UserSession.iRestNum = iRestNumIn;
                    UserSession.sRouteError = sRouteErrorIn;
                }
#else
                if (Me.Count > 0)
                {
                    return Me(0).Description;
                }
#endif

                return sb.ToString;

            }
        }


        public Description Copy()
        {

            private New Description d;
            d.Clear();

            For Each sd As SingleDescription In Me
                private New SingleDescription sdNew;
                sdNew.Description = sd.Description;
                sdNew.eDisplayWhen = sd.eDisplayWhen;
                sdNew.Restrictions = sd.Restrictions.Copy;
                sdNew.DisplayOnce = sd.DisplayOnce;
                sdNew.ReturnToDefault = sd.ReturnToDefault;
                sdNew.sTabLabel = sd.sTabLabel;
                d.Add(sdNew);
            Next;

            return d;

        }

    }


    public void TODO(string sFunction = "")
    {
        If sFunction = "" Then sFunction = "This section" Else sFunction = "Function """ + sFunction + """"
        MsgBox("TODO - " + sFunction + " still has to be completed.  Please inform Campbell of what you were doing.", MsgBoxStyle.Exclamation);
    }


    'Private Declare Function QueryPerformanceCounter Lib "kernel32" (ByRef counts As Long) As Integer
    'Public hDebugTimes As Collections.Generic.Dictionary(Of String, clsDebugTime)
    'Private hPerformanceCounter As Collections.Generic.Dictionary(Of String, Long)
    'Public lMaxMemory As Long = 0

    'Friend Class clsDebugTime
    '    Public dtDate As Date
    '    Public lMemory As Long
    'End Class

    '    Public Sub DebugTimeRecord(ByVal sFunction As String)
    '#If DEBUG Then

    '        If hDebugTimes Is Nothing Then hDebugTimes = New Collections.Generic.Dictionary(Of String, clsDebugTime)

    '        SyncLock hDebugTimes ' Make this procedure threadsafe
    '            If hDebugTimes.ContainsKey(sFunction) Then
    '                Debug.WriteLine("Mid " & sFunction & " (" & ((Now.Ticks - hDebugTimes(sFunction).dtDate.Ticks) / 10000000.0).ToString("#,##0.0##") & "s so far)")
    '            Else
    '                Dim oTime As New clsDebugTime

    '                oTime.dtDate = Now
    '                oTime.lMemory = System.GC.GetTotalMemory(False)

    '                hDebugTimes.Add(sFunction, oTime)
    '                Debug.WriteLine("In  " & sFunction)
    '            End If
    '        End SyncLock

    '#End If
    '    End Sub

    '    Dim lCountsPerSecond As Long
    '    Public Sub DebugTimeRecord(ByVal sFunction As String, ByVal iCall As Integer, ByVal bSmallTimings As Boolean)
    '#If DEBUG Then
    '        'If Not bSmallTimings Then
    '        '    DebugTimeRecord(sFunction)
    '        '    Exit Sub
    '        'End If

    '        'Dim lResult As Long
    '        'QueryPerformanceCounter(lResult)

    '        'If lResult = 0 Then
    '        '    ErrMsg("Performance Counter not supported by current hardware")
    '        '    DebugTimeRecord(sFunction)
    '        '    Exit Sub
    '        'End If

    '        'If hPerformanceCounter Is Nothing Then
    '        '    ' Work out a rough estimate of counts per second
    '        '    Threading.Thread.Sleep(1000)
    '        '    Dim lStart As Long = lResult
    '        '    QueryPerformanceCounter(lResult)
    '        '    lCountsPerSecond = lResult - lStart

    '        '    hPerformanceCounter = New Collections.Generic.Dictionary(Of String, Long)
    '        'End If


    '        'SyncLock hPerformanceCounter ' Make this procedure threadsafe
    '        '    If hPerformanceCounter.ContainsKey(sFunction) Then
    '        '        Debug.WriteLine("Mid " & sFunction & " call " & iCall & ", Performance Counter: " & ((lResult - hPerformanceCounter(sFunction)) / 10000).ToString("#,##0") & ", approx " & ((lResult - hPerformanceCounter(sFunction)) / lCountsPerSecond).ToString("0.000000") & " seconds")
    '        '    Else
    '        '        hPerformanceCounter.Add(sFunction, lResult)
    '        '        Debug.WriteLine("In  " & sFunction)
    '        '    End If
    '        'End SyncLock

    '#End If
    '    End Sub

    '    Public Sub DebugTimeFinish(ByVal sFunction As String)
    '#If DEBUG Then
    '        If (Not hDebugTimes Is Nothing) Then
    '            Dim oTime As clsDebugTime = Nothing
    '            Try
    '                If hDebugTimes.ContainsKey(sFunction) Then oTime = hDebugTimes(sFunction)

    '                If oTime IsNot Nothing Then
    '                    If System.GC.GetTotalMemory(False) > lMaxMemory Then lMaxMemory = System.GC.GetTotalMemory(False)

    '                    Debug.WriteLine("Out " & sFunction & " (" & ((Now.Ticks - oTime.dtDate.Ticks) / 10000000.0).ToString("#,##0.0##") & "s) Total memory used " & (System.GC.GetTotalMemory(False) / 1048576L).ToString("#,##0") & "Mb Function added " & (System.GC.GetTotalMemory(False) - oTime.lMemory).ToString("#,##0") & " bytes  MaxMemory " & (lMaxMemory / 1048576L).ToString("#,##0") & "Mb")
    '                    hDebugTimes.Remove(sFunction)
    '                Else
    '                    Debug.WriteLine("Unable to find Debug Time for function " & sFunction)
    '                End If
    '            Catch ex As Exception
    '                Debug.WriteLine("Unable to find Debug Time for function " & sFunction)
    '            End Try

    '        End If
    '#End If
    '    End Sub

    '    Public Sub DebugTimeFinish(ByVal sFunction As String, ByVal bSmallTimings As Boolean)
    '#If DEBUG Then
    '        'If Not bSmallTimings Then
    '        '    DebugTimeFinish(sFunction)
    '        '    Exit Sub
    '        'End If

    '        'If (Not hPerformanceCounter Is Nothing) Then
    '        '    Try
    '        '        Dim lResult As Long
    '        '        QueryPerformanceCounter(lResult)

    '        '        If lResult > 0 Then
    '        '            Debug.WriteLine("Out " & sFunction & ", Performance Counter: " & ((lResult - hPerformanceCounter(sFunction)) / 10000).ToString("#,##0") & ", approx " & ((lResult - hPerformanceCounter(sFunction)) / lCountsPerSecond).ToString("0.000000") & " seconds")
    '        '            hPerformanceCounter.Remove(sFunction)
    '        '        Else
    '        '            Debug.WriteLine("Performance Counter not supported by current hardware")
    '        '            DebugTimeRecord(sFunction)
    '        '            Exit Sub
    '        '        End If
    '        '    Catch ex As Exception
    '        '        Debug.WriteLine("Unable to find Debug Time for function " & sFunction)
    '        '    End Try

    '        'End If
    '#End If
    '    End Sub

public class clsSearchOptions
    {

public enum SearchInWhatEnum As Integer
        {
            Uninitialised = -1
            AllItems = 0
            NonLibraryItems = 1
        }

        public string sLastKey = "";
        public string sLastSearch = "";
        private SearchInWhatEnum eSearchInWhat = SearchInWhatEnum.Uninitialised;
        public SearchInWhatEnum SearchInWhat { get; set; }
            {
                get
                {
                if (eSearchInWhat = SearchInWhatEnum.Uninitialised)
                {
                    eSearchInWhat = CType(GetSetting("ADRIFT", "Generator", "SearchInWhat", CInt(SearchInWhatEnum.NonLibraryItems).ToString), SearchInWhatEnum)
                }
                return eSearchInWhat;
            }
set(ByVal SearchInWhatEnum)
                eSearchInWhat = value
                SaveSetting("ADRIFT", "Generator", "SearchInWhat", CInt(value).ToString);
            }
        }

        public bool bSearchMatchCase = false;
        public bool bFindExactWord = false;

    }
    public New clsSearchOptions SearchOptions;


public class SearchResult
    {
        private clsItem item;
        private int iOccuranceNumber;
        private System.Windows.Forms.Control ctrlFound;
    }
    public SearchResult LastSearchResult;

    public bool Search(string sFind)
    {

        With SearchOptions;
            If sFind <> .sLastSearch Then .sLastKey = ""
            .sLastSearch = sFind;
            private bool bFoundStart = (.sLastKey = "");
            For Each item As clsItem In Adventure.dictAllItems.Values
                if (bFoundStart)
                {
                    private bool bLookIn = false;
                    switch (.SearchInWhat)
                    {
                        case clsSearchOptions.SearchInWhatEnum.AllItems:
                            {
                            bLookIn = True
                        case clsSearchOptions.SearchInWhatEnum.NonLibraryItems:
                            {
                            bLookIn = Not item.IsLibrary
                    }
                    if (bLookIn && item.SearchFor(sFind))
                    {
                        'MsgBox("Item " & item.CommonName & " matches.")
                        item.EditItem();
                        .sLastKey = item.Key;
#if Generator
                        fGenerator.UTMMain.Tools("FindNext").SharedProps.Enabled = true;
                        fGenerator.UTMMain.Tools("FindNext").SharedProps.ToolTipText = "Find Next """ + sFind + """";
#endif
                        return true;
                    }
                }
                If item.Key = .sLastKey Then bFoundStart = true
            Next;
            MessageBox.Show("The following specified text was not found: " + sFind, "ADRIFT - Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            .sLastKey = "";
            return false;
        }

    }


    public List<clsItem> FindAll(string sFind)
    {

        private New List<clsItem> lstResults;

        With SearchOptions;
            If sFind <> .sLastSearch Then .sLastKey = ""
            .sLastSearch = sFind;
            'Dim bFoundStart As Boolean = (.sLastKey = "")
            For Each item As clsItem In Adventure.dictAllItems.Values
                'If bFoundStart Then
                private bool bLookIn = false;
                switch (.SearchInWhat)
                {
                    case clsSearchOptions.SearchInWhatEnum.AllItems:
                        {
                        bLookIn = True
                    case clsSearchOptions.SearchInWhatEnum.NonLibraryItems:
                        {
                        bLookIn = Not item.IsLibrary
                }
                if (bLookIn && item.SearchFor(sFind))
                {
                    'MsgBox("Item " & item.CommonName & " matches.")
                    'item.EditItem()
                    .sLastKey = item.Key;
                    '#If Generator Then
                    '                    fGenerator.UTMMain.Tools("FindNext").SharedProps.Enabled = True
                    '                    fGenerator.UTMMain.Tools("FindNext").SharedProps.ToolTipText = "Find Next """ & sFind & """"
                    '#End If
                    lstResults.Add(item);
                }
                'End If
                'If item.Key = .sLastKey Then bFoundStart = True
            Next;
            if (lstResults.Count = 0)
            {
                MessageBox.Show("The following specified text was not found: " + sFind, "ADRIFT - Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                .sLastKey = "";
            }
        }

        return lstResults;

    }


    public void SearchAndReplace(string sFind, string sReplace, bool bSilent = false)
    {

        private int iReplacements = 0;

        With SearchOptions;
            For Each item As clsItem In Adventure.dictAllItems.Values
                private bool bLookIn = false;
                switch (.SearchInWhat)
                {
                    case clsSearchOptions.SearchInWhatEnum.AllItems:
                        {
                        bLookIn = True
                    case clsSearchOptions.SearchInWhatEnum.NonLibraryItems:
                        {
                        bLookIn = Not item.IsLibrary
                }
                If bLookIn Then iReplacements += item.SearchAndReplace(sFind, sReplace)
            Next;
        }

        if (Not bSilent)
        {
            if (iReplacements = 0)
            {
                MessageBox.Show("The following specified text was not found: " + sFind, "ADRIFT - Search + Replace", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Else
                MessageBox.Show(iReplacements + " occurance(s) replaced.", "ADRIFT - Search + Replace", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }


    public void SetCombo(ref cmb As ComboBox, string sKey, bool bBoldSelections = false)
    {

        cmb.SelectedItem = sKey;
        'For Each vli As Infragistics.Win.ValueListItem In cmb.Items
        '    If CStr(vli.DataValue) = sKey Then
        '        cmb.SelectedItem = vli
        '        If bBoldSelections AndAlso sKey <> "" Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
        '        Exit Sub
        '    End If
        'Next

    }
    public void SetCombo(ref cmb As ComboBox, int iKey, bool bBoldSelections = false)
    {

        cmb.SelectedItem = iKey;
        'For Each vli As Infragistics.Win.ValueListItem In cmb.Items
        '    If CInt(vli.DataValue) = iKey Then
        '        cmb.SelectedItem = vli
        '        If bBoldSelections AndAlso iKey > -1 Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
        '        Exit Sub
        '    End If
        'Next

    }

#if Not Mono AndAlso Not www Then ' Can't go in Infragistics module because it can't detect signatures are different :-(
    public void SetCombo(ref cmb As Infragistics.Win.UltraWinEditors.UltraComboEditor, string sKey, bool bBoldSelections = false)
    {

        For Each vli As Infragistics.Win.ValueListItem In cmb.Items
            if (CStr(vli.DataValue) = sKey)
            {
                cmb.SelectedItem = vli;
                If bBoldSelections && sKey <> "" Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
                Exit Sub;
            }
        Next;

    }
    public void SetCombo(ref cmb As Infragistics.Win.UltraWinEditors.UltraComboEditor, int iKey, bool bBoldSelections = false)
    {

        For Each vli As Infragistics.Win.ValueListItem In cmb.Items
            if (CInt(vli.DataValue) = iKey)
            {
                cmb.SelectedItem = vli;
                If bBoldSelections && iKey > -1 Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
                Exit Sub;
            }
        Next;

    }

#if Not Runner

    public void SetCombo(ref cmb As AutoCompleteCombo, string sKey, bool bBoldSelections = false)
    {

        For Each vli As Infragistics.Win.ValueListItem In cmb.Items
            if (CStr(vli.DataValue) = sKey)
            {
                cmb.SelectedItem = vli;
                If bBoldSelections && sKey <> "" Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
                Exit Sub;
            }
        Next;

    }
    public void SetCombo(ref cmb As AutoCompleteCombo, int iKey, bool bBoldSelections = false)
    {

        For Each vli As Infragistics.Win.ValueListItem In cmb.Items
            if (CInt(vli.DataValue) = iKey)
            {
                cmb.SelectedItem = vli;
                If bBoldSelections && iKey > -1 Then cmb.Font = New Font(cmb.Font, FontStyle.Bold) Else cmb.Font = New Font(cmb.Font, FontStyle.Regular)
                Exit Sub;
            }
        Next;

    }
#endif
#endif

}

}