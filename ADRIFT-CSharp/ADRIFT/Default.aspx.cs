using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ADRIFT
{

public class _Default
{
    Inherits System.Web.UI.Page;

    Friend WithEvents pbxGraphics As Object ' clsImage;
    Friend WithEvents btnMore As New Windows.Forms.Button;
    internal New System.Windows.Forms.Panel[] pnlBottom;
    'Friend WithEvents StatusBar As New Windows.Forms.StatusBar
    Friend WithEvents txtInput As New Windows.Forms.RichTextBox;
    Friend WithEvents txtOutput As New Windows.Forms.RichTextBox;


    'Friend Property Map As ADRIFT.Map
    '    Get
    '        If UserSession.Map Is Nothing Then
    '            UserSession.Map = New ADRIFT.Map
    '            'Dim f As New Windows.Forms.Form
    '            'f.Controls.Add(UserSession.Map)
    '            UserSession.Map.RecalculateNode(Adventure.Map.FindNode(Adventure.Player.Location.LocationKey))
    '            UserSession.Map.SelectNode(Adventure.Player.Location.LocationKey)
    '        End If
    '        Return UserSession.Map
    '        'Dim m As New ADRIFT.Map
    '        'm.RecalculateNode(Adventure.Map.FindNode(Adventure.Player.Location.LocationKey))
    '        'm.SelectNode(Adventure.Player.Location.LocationKey)
    '        'Return m
    '    End Get
    '    Set(value As ADRIFT.Map)
    '        UserSession.Map = value
    '    End Set
    'End Property

    'Private Property LastLoad() As Date
    '    Get
    '        If HttpContext.Current.Session.Item("LastLoad") Is Nothing Then
    '            Return Date.MinValue
    '        Else
    '            Return CType(HttpContext.Current.Session.Item("LastLoad"), Date)
    '        End If
    '    End Get
    '    Set(value As Date)
    '        HttpContext.Current.Session.Item("LastLoad") = value
    '    End Set
    'End Property

    private bool KeyPressed
        {
            get
            {
            return SafeBool(HttpContext.Current.Session.Item("KeyPressed"));
        }
set(Boolean)
            HttpContext.Current.Session.Item("KeyPressed") = value;
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        'If Now.Subtract(LastLoad).TotalMilliseconds < 200 Then Exit Sub
        ''Debug.WriteLine(Now.Subtract(LastLoad).TotalMilliseconds & " milliseconds")
        'LastLoad = Now
        'Debug.WriteLine("Callback from: " & ScriptManager1.AsyncPostBackSourceElementID)
        'Debug.WriteLine(Now.TimeOfDay.ToString & " Page_Load, IsPostBack: " & Page.IsPostBack & ", KeyPressed: " & KeyPressed & ", txtInputWeb.Text: " & txtInputWeb.Text)
        'For Each Channel As HtmlGenericControl In New HtmlGenericControl() {Channel1}
        '    Channel.InnerHtml = ""
        'Next

        If Request.Browser.Browser = "Chrome" || Request.Browser.Browser = "Safari" Then ' For some reason Chrome seems to be calling a second Page_Load
            if (Page.IsPostBack && txtInputWeb.Text = "" && Not KeyPressed && WaitKeyBuffer <> "")
            {
                ' User has pressed Enter whilst in a <waitkey>, but nothing in Inputbox
                KeyPressed = True
            }
            If ! KeyPressed && Page.IsPostBack Then Exit Sub
            KeyPressed = False
        }

        fRunner = Me

        'txtOutputWeb.InnerText = HttpContext.Current.Request.PhysicalApplicationPath
        'Exit Sub

        IO.Directory.SetCurrentDirectory(DataPath) 'IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data"));

        If Page.ToString = "ASP.about_aspx" Then About()
        If Page.ToString <> "ASP.default_aspx" Then Exit Sub

        'If StatusBar.Panels.Count = 0 Then
        '    Dim pnlDescription As New StatusBarPanel
        '    pnlDescription.Name = "Description"
        '    StatusBar.Panels.Add(pnlDescription)
        '    Dim pnlScore As New StatusBarPanel
        '    pnlScore.Name = "Score"
        '    StatusBar.Panels.Add(pnlScore)
        'End If

        if (UserSession Is null)
        {
            UserSession = New RunnerSession
        }
        if (Adventure Is null)
        {
            GlobalStartup();
            SetColours();
        Else
            if (Not Page.IsPostBack)
            {
                ' Hmm, returning to the page from another
                SetColours();
                ' Clear text and set to last message
                UserSession.UpdateStatusBar();
                txtOutputWeb.InnerHtml = "";
                if (Not Locked)
                {
                    private string sStartLoc = Adventure.Player.Location.LocationKey;
                    If Adventure.htblLocations.ContainsKey(sStartLoc) Then UserSession.Display(Adventure.htblLocations(sStartLoc).ViewLocation + vbCrLf + vbCrLf, true)
                }
            }
        }

        if (Locked)
        {
            ' If there is anything in the waitkey buffer, output it to screen now
            Locked = False
            private string sBuffer = WaitKeyBuffer;
            if (sBuffer <> "")
            {
                WaitKeyBuffer = ""
                AppendHTML(sBuffer);
            }
            txtInputWeb.Text = "";
        ElseIf txtInputWeb.Text <> "" Then
            private string sCommand = txtInputWeb.Text;
            txtInputWeb.Text = "";
            UserSession.Process(sCommand);
        }

        'If IsPostBack Then


        if (Request("advid") <> "")
        {
            ' Load from adrift.co
            if (SafeInt(Request("advid")) <> iId)
            {
                iId = SafeInt(Request("advid"))
                MsgBox("Load from adrift.co by ID");
            }
        ElseIf Request("game") <> "" Then
            if (Request("game") <> sURL)
            {
                sURL = Request("game")
                if (sURL.ToLower.EndsWith(".taf") || sURL.ToLower.EndsWith(".blorb"))
                {

                    private string sFile = "";
                    if (IO.File.Exists(sURL))
                    {
                        sFile = sURL
                    ElseIf IO.File.Exists(IO.Path.GetFileName(sURL)) Then
                        sFile = IO.Path.GetFileName(sURL)
                    Else
                        private string sTxt = sURL.Replace("/", "_").Replace("\", "_") + ".txt";
                        if (IO.File.Exists(sTxt))
                        {
                            ' This game has been downloaded before
                            ' sTxt contains the IFID

                        Else
                            ' We need to download the file
RetryDownload:;
                            try
                            {
                                private New System.Net.WebClient wc;
                                sFile = IO.Path.GetFileName(sURL)
                                if (sURL.ToUpper.StartsWith("HTTPS://"))
                                {
                                    System.Net.ServicePointManager.Expect100Continue = true;
                                    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls | Net.SecurityProtocolType.Tls11 | Net.SecurityProtocolType.Tls12 | Net.SecurityProtocolType.Ssl3;
                                }
                                wc.DownloadFile(sURL, sFile);
                                wc.Dispose();

                                Threading.Thread.Sleep(10);
                            }
                            catch (System.Net.HttpListenerException exHLE)
                            {
                                ErrMsg("Error downloading " + sURL, exHLE);
                            }
                            catch (Exception ex)
                            {
                                if (ex.HResult = -2146233079 && sURL.ToUpper.StartsWith("HTTP://"))
                                {
                                    sURL = "https://" & sURL.Substring(7)
                                    GoTo RetryDownload;
                                }
                                ErrMsg("Error downloading " + sURL, ex);
                            }
                        }
                    }

                    if (IO.File.Exists(sFile))
                    {
                        ' Ok, just load the local file
                        txtOutputWeb.InnerText = "";
                        UserSession.OpenAdventure(sFile);
                        if (Adventure.dVersion < 5 || Adventure.BabelTreatyInfo Is null)
                        {
                            UserSession.sGameFolder = IO.Path.Combine(DataPath, IO.Path.GetFileNameWithoutExtension(sFile));
                        Else
                            UserSession.sGameFolder = IO.Path.Combine(DataPath, Adventure.BabelTreatyInfo.Stories(0).Identification.IFID);
                        }

                        if (Not IO.Directory.Exists(UserSession.sGameFolder))
                        {
                            IO.Directory.CreateDirectory(UserSession.sGameFolder);
                        }

                        'If sFile = "cursed.taf" OrElse sFile = "how suzy got her powers.taf" Then CType(Page.Master, Site).footer.InnerHtml = "Please be aware that v5 is not yet 100% compatible with v4.  For this reason, please play this game on <a href=""https://www.adrift.co/files/ADRIFT40r.zip"">v4 Runner</a>."
                    Else
                        ErrMsg("File not found");
                    }

                    'If IO.Path.GetFileName(sURL) = sURL Then
                    '    ' Filename only - look in App_Data folder
                    '    sURL = IO.Path.Combine(IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data"), sURL)
                    'End If

                    SetColours();
                Else
                    ErrMsg("WebRunner can only play taf or blorb files!");
                }
            }
            'Response.Redirect("Default.aspx")
        }
        txtInputWeb.Focus();

    }


    private void About()
    {

        private New List<string> lKeys;
        For Each sKey As String In txtOutputWebAbout.Style.Keys
            lKeys.Add(sKey);
        Next;
        For Each sKey As String In New String() {"font-family", "font-size", "color"}
            If lKeys.Contains(sKey) Then txtOutputWebAbout.Style.Remove(sKey)
        Next;
        if (Adventure IsNot null)
        {
            txtOutputWebAbout.Style.Add("font-family", Adventure.DefaultFontName);
            txtOutputWebAbout.Style.Add("font-size", Adventure.DefaultFontSize.ToString + "pt");
        }
        txtOutputWebAbout.Style.Add("color", "#" + Hex(GetOutputColour.ToArgb).Substring(2));

        UserSession.Display("ADRIFT WebRunner version " + System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString + vbCrLf + vbCrLf, true);
        if (Adventure IsNot null)
        {
            if (Adventure.BabelTreatyInfo.Stories IsNot null)
            {
                UserSession.Display(Adventure.Title + vbCrLf + "by " + Adventure.Author + vbCrLf + "Version " + Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.Version + vbCrLf + vbCrLf, true);
                if (Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release IsNot null)
                {
                    private string sBuild = "Built ";
                    if (Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.CompilerVersion <> "")
                    {
                        sBuild &= "using ADRIFT Developer " + Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.CompilerVersion + " ";
                    Else
                        sBuild &= "using ADRIFT ";
                        If Adventure.dVersion >= 5 Then sBuild &= "Developer " Else sBuild &= "Generator "
                        sBuild &= Adventure.Version + " ";
                    }
                    If Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.ReleaseDate <> Date.MinValue Then sBuild &= "on " + Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release.ReleaseDate.ToShortDateString
                    UserSession.Display(sBuild, true);
                }
            }
        }

        UserSession.Display(vbCrLf + vbCrLf + "ADRIFT WebRunner is still in development, and there may be bugs.  If you find any, please notify <a href=""mailto:campbell@adrift.org.uk"">Campbell</a> or add it to the ADRIFT <a href=""https://www.adrift.co/cgi/new/adrift.cgi?script=bugs5"">bugs list</a>.  At present, there are a few things that WebRunner can't do that the Runner app does.  For the full experience, please <a href=""http://www.adrift.co/download"">download</a> ADRIFT Runner for your operating system.", true);

    }


    public void SetInput(string sText)
    {
        txtInputWeb.Text = sText;
    }

    public void SubmitCommand()
    {

    }

    internal void SetBackgroundColour(Color colBackground = null)
    {
        If colBackground = null Then colBackground = GetBackgroundColour()
        fRunner.txtOutput.BackColor = colBackground;
        'fRunner.pnlTop.BackColor = colBackground
        fRunner.txtInput.BackColor = colBackground;
        fRunner.pnlBottom.BackColor = colBackground;
    }

    private void SetColours()
    {

        private New List<string> lKeys;
        For Each sKey As String In txtOutputWeb.Style.Keys
            lKeys.Add(sKey);
        Next;
        if (Adventure IsNot null)
        {
            For Each sKey As String In New String() {"font-family", "font-size", "color"}
                If lKeys.Contains(sKey) Then txtOutputWeb.Style.Remove(sKey)
            Next;
            txtOutputWeb.Style.Add("font-family", Adventure.DefaultFontName);
            txtOutputWeb.Style.Add("font-size", Adventure.DefaultFontSize.ToString + "pt");
        }
        txtOutputWeb.Style.Add("color", "#" + Hex(GetOutputColour.ToArgb).Substring(2));

        lKeys.Clear();
        For Each sKey As String In InputWeb.Style.Keys
            lKeys.Add(sKey);
        Next;
        For Each sKey As String In New String() {"font-family", "font-size", "color"}
            If lKeys.Contains(sKey) Then InputWeb.Style.Remove(sKey)
        Next;
        if (Adventure IsNot null)
        {
            InputWeb.Style.Add("font-family", Adventure.DefaultFontName);
            InputWeb.Style.Add("font-size", Adventure.DefaultFontSize.ToString + "pt");
        }
        InputWeb.Style.Add("color", "#" + Hex(GetInputColour.ToArgb).Substring(2));

        lKeys.Clear();
        For Each sKey As String In txtInputWeb.Style.Keys
            lKeys.Add(sKey);
        Next;
        For Each sKey As String In New String() {"font-family", "font-size", "color"}
            If lKeys.Contains(sKey) Then txtInputWeb.Style.Remove(sKey)
        Next;
        if (Adventure IsNot null)
        {
            txtInputWeb.Style.Add("font-family", Adventure.DefaultFontName);
            txtInputWeb.Style.Add("font-size", Adventure.DefaultFontSize.ToString + "pt");
        }
        txtInputWeb.Style.Add("color", "#" + Hex(GetInputColour.ToArgb).Substring(2));
    }


    private bool LoadFromID(int iId)
    {

    }
    private bool LoadFromURL(string sURL)
    {

    }
    private bool LoadFromFile(string sFile)
    {
        return UserSession.OpenAdventure(sFile);
        'Return OpenAdventure("C:\Users\CampbellWild\Documents\ADRIFT5\Games\jim5.taf")
    }

    protected void txtInputWeb_TextChanged(object sender, EventArgs e)
    {
        'If Locked Then
        '    Locked = False
        '    Dim sBuffer As String = WaitKeyBuffer
        '    If sBuffer <> "" Then
        '        WaitKeyBuffer = ""
        '        AppendHTML(sBuffer)
        '    End If
        'Else
        '    Process(txtInputWeb.Text)
        'End If
        'txtInputWeb.Text = ""
        KeyPressed = True
        'Debug.WriteLine(Now.TimeOfDay.ToString & " txtInputWeb_TextChanged: " & txtInputWeb.Text)
        'Page_Load(Nothing, Nothing) ' Chrome seems to do a Page_Load after this, but Firefox doesn't :-/
    }

    private void txtInput_TextChanged(object sender, System.EventArgs e)
    {
        if (txtInput.Text <> txtInputWeb.Text)
        {
            txtInputWeb.Text = txtInput.Text;
        }
    }



    'Public Sub AppendHTML(ByVal sText As String)
    '    sText = sText.Replace("<c>", "<font color=""#" & Hex(colInput.ToArgb).Substring(2) & """>").Replace("</c>", "</font>")
    '    txtOutputWeb.Text &= sText
    'End Sub

    public void AppendHTML(string sSource)
    {

        if (txtOutputWeb IsNot null && WaitKeyBuffer <> "")
        {
            WaitKeyBuffer &= sSource;
            Exit Sub;
        }

        if (sSource.Contains("<>"))
        {
            sSource = sSource.Replace("<>", "")
        }

        if (sSource.Contains("<waitkey>"))
        {
            private string sBefore = sSource.Substring(0, sSource.IndexOf("<waitkey>"));
            If sBefore <> "" Then AppendHTML(sBefore)
            WaitKeyBuffer = sSource.Substring(sSource.IndexOf("<waitkey>") + 9)
            WaitKey();
            Exit Sub;
        }

        if (sSource.Contains("<cls>"))
        {
            private string sBefore = sSource.Substring(0, sSource.IndexOf("<cls>"));
            If sBefore <> "" Then AppendHTML(sBefore)
            txtOutputWeb.InnerHtml = "";
            private string sAfter = sSource.Substring(sSource.IndexOf("<cls>") + 5);
            UserSession.stackFonts.Clear();
            UserSession.stackColours.Clear();
            If sAfter <> "" Then AppendHTML(sAfter)
            Exit Sub;
        }

        ' Corrections to source...
        sSource = sSource.Replace("<font face=""Wingdings"" size=14>Ø</font>", "<font size=14>" & ChrW(&H27A2) & "</font>")
        sSource = sSource.Replace(vbCrLf, "<br>").Replace(vbLf, "<br>").Replace("centre>", "center>")
        while (sSource.Contains("  "))
        {
            sSource = sSource.Replace("  ", "&nbsp;&nbsp;")
        }
        while (sSource.Contains("<br> "))
        {
            sSource = sSource.Replace("<br> ", "<br>&nbsp;")
        }
        sSource = sSource.Replace("<br>", "<br/>")

        private New List<string> Chunks;
        private string sCurrentChunk = "";
        private int iLevel = 0;
        for (int i = 0; i <= sSource.Length - 1; i++)
        {
            switch (sSource(i))
            {
                case "<"c:
                    {
                    if (iLevel = 0)
                    {
                        If sCurrentChunk <> "" Then Chunks.Add(sCurrentChunk)
                        sCurrentChunk = ""
                    }
                    iLevel += 1;
                    sCurrentChunk &= "<";
                case ">"c:
                    {
                    sCurrentChunk &= ">";
                    iLevel -= 1;
                    if (iLevel = 0)
                    {
                        Chunks.Add(sCurrentChunk);
                        sCurrentChunk = ""
                    }
                default:
                    {
                    sCurrentChunk &= sSource(i);
            }
        Next;
        If sCurrentChunk <> "" Then Chunks.Add(sCurrentChunk)
        'For Each sChunk As String In sSource.Split("<")
        '    If sChunk <> "" Then
        '        If sChunk.Contains(">") Then
        '            Chunks.Add("<" & sChunk.Split(">")(0) & ">")
        '            If sChunk.Split(">")(1) <> "" Then Chunks.Add(sChunk.Split(">")(1))
        '        Else
        '            Chunks.Add(sChunk)
        '        End If
        '    End If
        'Next

        for (int iChunk = 0; iChunk <= Chunks.Count - 1; iChunk++)
        {
            With Chunks(iChunk);
                if (.StartsWith("<") && .EndsWith(">"))
                {
                    private string sTag = .Substring(1, .Length - 2);

                    if (sTag.StartsWith("!--") && sTag.EndsWith("--"))
                    {
                        Chunks(iChunk) = "";
                        sTag = ""
                    }

                    private string sTagName = sTag;
                    If sTagName.Contains(" ") Then sTagName = sLeft(sTag, sTagName.IndexOf(" "))
                    switch (sTagName)
                    {
                        case "c":
                            {
                            Chunks(iChunk) = "<font color=""#" + Hex(GetInputColour.ToArgb).Substring(2) + """>";
                        case "/c":
                            {
                            Chunks(iChunk) = "</font>";
                        case "del":
                            {
                            if (iChunk > 0 && Chunks(iChunk - 1).Length > 0)
                            {
                                Chunks(iChunk - 1) = Chunks(iChunk - 1).Substring(0, Chunks(iChunk - 1).Length - 1);
                            Else
                                If txtOutputWeb.InnerHtml.EndsWith("<br>") Then txtOutputWeb.InnerHtml = txtOutputWeb.InnerHtml.Substring(0, txtOutputWeb.InnerHtml.Length - 4)
                            }
                            Chunks(iChunk) = "";
                        case "font":
                            {
                            private Font fontPrevious;
                            private string colourPrevious;
                            private int i;

                            if (UserSession.stackFonts.Count > 0)
                            {
                                fontPrevious = UserSession.stackFonts.Peek
                            Else
                                fontPrevious = Adventure.DefaultFont
                            }
                            if (UserSession.stackColours.Count > 0)
                            {
                                colourPrevious = UserSession.stackColours.Peek
                            Else
                                colourPrevious = System.Drawing.ColorTranslator.ToHtml(GetOutputColour)
                            }

                            sTag = sTag.Replace("= ", "=").Replace(" =", "=")

                            private string sFace = fontPrevious.FontFamily.Name;
                            i = sInstr(sTag, "face=")
                            if (i > 0)
                            {
                                private string sBuffer = sMid(sTag, i + 5, sTag.Length - i - 4);
                                if (Left(sBuffer, 1) = Chr(34))
                                {
                                    sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                                Else
                                    If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                                }

                                sFace = sBuffer
                            }

                            private int iSize = SafeInt(fontPrevious.SizeInPoints);
                            i = sInstr(sTag, "size=")
                            if (i > 0)
                            {
                                private string sBuffer = sMid(sTag, i + 5, sTag.Length - i - 4);
                                if (Left(sBuffer, 1) = Chr(34))
                                {
                                    sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                                Else
                                    If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                                }

                                if (Not (sBuffer.Contains("-") || sBuffer.Contains("+")))
                                {
                                    iSize = SafeInt(sBuffer)
                                ElseIf CSng(sBuffer) < 0 Then
                                    private int iOffset = SafeInt(sBuffer);
                                    iSize += iOffset;
                                ElseIf Left(sBuffer, 1) = "+" Then
                                    private int iOffset = SafeInt(sBuffer.Substring(1));
                                    iSize += iOffset;
                                }
                            }

                            i = sInstr(sTag, "color=")
                            private string sColour = colourPrevious;
                            if (i > 0)
                            {
                                private string sBuffer = sMid(sTag, i + 6, sTag.Length - i - 5);
                                If sInstr(sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(sBuffer, " ") - 1)
                                If Right(sBuffer, 1) = Chr(34) && Left(sBuffer, 1) = Chr(34) Then sBuffer = sMid(sBuffer, 2, sBuffer.Length - 2)
                                sBuffer = ColourLookup(sBuffer)

                                If ! sBuffer.StartsWith("#") Then sBuffer = "#" + sBuffer
                                'sNewTag &= " color=""" & sBuffer & """"
                                'NewColour = Color.FromArgb(rr, gg, bb)
                                sColour = sBuffer
                            }
                            If ! sColour.StartsWith("#") Then sColour = "#" + sColour

                            private New Font(sFace, iSize, GraphicsUnit.Point) newfont;

                            private bool bColour = sColour <> colourPrevious;
                            private bool bFace = newfont.FontFamily.Name <> fontPrevious.FontFamily.Name;
                            private bool bSize = newfont.SizeInPoints <> fontPrevious.SizeInPoints;

                            UserSession.stackFonts.Push(newfont);
                            UserSession.stackColours.Push(sColour);

                            private string sFontColour = "";
                            If bColour Then sFontColour = " color=""" + sColour + """"
                            private string sStyle = "";
                            if (bFace || bSize)
                            {
                                sStyle = " style="""
                                If bFace Then sStyle &= "font-family:" + newfont.FontFamily.Name + ";"
                                If bSize Then sStyle &= "font-size:" + newfont.SizeInPoints + "pt;"
                                sStyle &= """";
                            }
                            Chunks(iChunk) = "<font" + sFontColour + sStyle + ">";

                        case "/font":
                            {
                            If UserSession.stackFonts.Count > 0 Then UserSession.stackFonts.Pop()
                            If UserSession.stackColours.Count > 0 Then UserSession.stackColours.Pop()
                            Chunks(iChunk) = "</font>";

                        case "img":
                        case "/img":
                            {
                            Chunks(iChunk) = "";

                            private int i;

                            i = sInstr(sTag, " src=")
                            if (i > 0)
                            {
                                private string sBuffer = sMid(sTag, i + 5, sTag.Length - i - 4);
                                If sBuffer.StartsWith("""") Then sBuffer = sBuffer.Substring(1)
                                If sBuffer.EndsWith("""") Then sBuffer = sBuffer.Substring(0, sBuffer.Length - 1)
                                sBuffer = Trim(sBuffer)

                                private bool bVisible = false;

                                try
                                {
                                    if (UserSession.bGraphics)
                                    {
                                        ' First ensure Img directory exists
                                        if (Not IO.Directory.Exists(Server.MapPath("/img")))
                                        {
                                            IO.Directory.CreateDirectory(Server.MapPath("/img"));
                                            IO.File.Create(Server.MapPath("/img") + "/index.htm");
                                        }
                                        ' Then ensure IFID directory exists
                                        if (Not IO.Directory.Exists(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID)))
                                        {
                                            IO.Directory.CreateDirectory(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID));
                                            IO.File.Create(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID) + "/index.htm");
                                        }

                                        private string sImgPath = "/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID;
                                        ' Then check to see if image file exists

                                        if (Adventure.BlorbMappings IsNot null && Adventure.BlorbMappings.Count > 0)
                                        {
                                            private int iResource = 0;
                                            If Adventure.BlorbMappings.ContainsKey(sBuffer) Then iResource = Adventure.BlorbMappings(sBuffer)
                                            if (iResource > 0)
                                            {
                                                private string sURL = "";
                                                private string sProperties = "";
                                                For Each sExtn As String In New String() {"jpg", "png", "gif"}
                                                    if (IO.File.Exists(Server.MapPath(sImgPath) + "/Image" + iResource + "." + sExtn))
                                                    {
                                                        sURL = sImgPath & "/Image" & iResource & "." & sExtn
                                                        private System.Drawing.Image img = Blorb.GetImage(iResource);
                                                        sProperties = " Height = " & img.Height & " Width = " & img.Width
                                                        Exit For;
                                                    }
                                                Next;
                                                if (sURL = "")
                                                {
                                                    private string sExtn = "";
                                                    private System.Drawing.Image img = Blorb.GetImage(iResource, , sExtn);
                                                    img.Save(Server.MapPath(sImgPath) + "/Image" + iResource + "." + sExtn);
                                                    sProperties = " Height = " & img.Height & " Width = " & img.Width
                                                    sURL = sImgPath & "/Image" & iResource & "." & sExtn
                                                }
                                                Chunks(iChunk) = "<center><img src=""" + sURL + """" + sProperties + " /></center>";
                                            }
                                        Else
                                            '    fRunner.pbxGraphics.Load(sBuffer)
                                        }
                                    }
                                Catch
                                }
                            }

                        case "audio":
                        case "/audio":
                            {
                            Chunks(iChunk) = "";

                            try
                            {
                                if (UserSession.bSound)
                                {
                                    private int i;
                                    private int iChannel = 1;
                                    private bool bLooping = false;
                                    private string sSrc = "";
                                    private string sBuffer;

                                    i = sInstr(sTag, "channel=")
                                    if (i > 0)
                                    {
                                        sBuffer = sMid(sTag, i + 8, sTag.Length - i - 8)
                                        if (Left(sBuffer, 1) = Chr(34))
                                        {
                                            sBuffer = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                                        Else
                                            If sInstr(1, sBuffer, " ") > 0 Then sBuffer = Left(sBuffer, sInstr(1, sBuffer, " "))
                                        }
                                        iChannel = SafeInt(sBuffer)
                                    }

                                    i = sInstr(sTag, " src=")
                                    if (i > 0)
                                    {
                                        sBuffer = sMid(sTag, i + 5, sTag.Length - i - 4)
                                        if (Left(sBuffer, 1) = Chr(34))
                                        {
                                            sSrc = sMid(sBuffer, 2, sInstr(2, sBuffer, Chr(34)) - 2)
                                        Else
                                            If sInstr(1, sBuffer, " ") > 0 Then sSrc = Left(sBuffer, sInstr(1, sBuffer, " "))
                                        }
                                    }

                                    i = sInstr(sTag, " loop=")
                                    if (i > 0)
                                    {
                                        sBuffer = sMid(sTag, i + 6, sTag.Length - i - 6)
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

                                    ' TODO - We actually need an UpdatePanel per channel...
                                    private HtmlGenericControl Channel = null;
                                    switch (iChannel)
                                    {
                                        case 1:
                                            {
                                            Channel = Channel1
                                        case 2:
                                            {
                                            Channel = Channel2
                                        case 3:
                                            {
                                            Channel = Channel3
                                        case 4:
                                            {
                                            Channel = Channel4
                                        case 5:
                                            {
                                            Channel = Channel5
                                        case 6:
                                            {
                                            Channel = Channel6
                                        case 7:
                                            {
                                            Channel = Channel7
                                        case 8:
                                            {
                                            Channel = Channel8
                                    }

                                    if (Channel IsNot null)
                                    {
                                        if (sTag.Contains(" pause"))
                                        {
                                            Channel.InnerHtml = "";
                                            UpdatePanelAudioChannels.Update();
                                        ElseIf sTag.Contains(" stop") Then
                                            Channel.InnerHtml = "";
                                            UpdatePanelAudioChannels.Update();
                                        Else
                                            ' First ensure Img directory exists
                                            if (Not IO.Directory.Exists(Server.MapPath("/img")))
                                            {
                                                IO.Directory.CreateDirectory(Server.MapPath("/img"));
                                                IO.File.Create(Server.MapPath("/img") + "/index.htm");
                                            }
                                            ' Then ensure IFID directory exists
                                            if (Not IO.Directory.Exists(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID)))
                                            {
                                                IO.Directory.CreateDirectory(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID));
                                                IO.File.Create(Server.MapPath("/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID) + "/index.htm");
                                            }

                                            private string sImgPath = "/img/" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID;

                                            if (Adventure.BlorbMappings IsNot null && Adventure.BlorbMappings.Count > 0)
                                            {
                                                private int iResource = 0;
                                                If Adventure.BlorbMappings.ContainsKey(sSrc) Then iResource = Adventure.BlorbMappings(sSrc)
                                                if (iResource > 0)
                                                {
                                                    private string sURL = "";
                                                    For Each sExtn As String In New String() {"mp3", "wav", "mid"}
                                                        if (IO.File.Exists(Server.MapPath(sImgPath) + "/Sound" + iResource + "." + sExtn))
                                                        {
                                                            sURL = sImgPath & "/Sound" & iResource & "." & sExtn
                                                            Exit For;
                                                        }
                                                    Next;
                                                    if (sURL = "")
                                                    {
                                                        private string sExtn = "";
                                                        private ADRIFT.clsBlorb.SoundFile sound = Blorb.GetSound(iResource, , sExtn);
                                                        sound.Save(Server.MapPath(sImgPath) + "/Sound" + iResource + "." + sExtn);
                                                        sURL = sImgPath & "/Sound" & iResource & "." & sExtn
                                                    }

                                                    ' BGSOUND tag now obsolete
                                                    'Channel.InnerHtml = "<NOEMBED><BGSOUND SRC=""" & sURL & """ LOOP=" & IIf(bLooping, "1", "0").ToString & "></NOEMBED>" _
                                                    '& "<EMBED SRC=""" & sURL & """ AUTOSTART=""True"" HIDDEN=""True"" LOOP=""" & bLooping.ToString.ToLower & """>"

                                                    'Channel.InnerText = $"<audio src=""{sURL}"" loop={If(bLooping, "Y", "N")} autoplay>Your browser does not support the <code>audio</code> element.</audio>"

                                                    'Channel.InnerHtml = $"<NOEMBED><audio src=""{sURL}"" loop={If(bLooping, "Y", "N")} autoplay>Your browser does not support the <code>audio</code> element.</audio></NOEMBED>" &
                                                    ' "<EMBED SRC=""{sURL}"" AUTOSTART=""True"" HIDDEN=""True"" LOOP=""{bLooping.ToString.ToLower}"">"

                                                    private bool bMuted = muteButton.ToolTip = "Unmute audio";
                                                    Channel.InnerHtml = $"<video controls autoplay {If(bMuted, "muted", "")} id=""audio{iChannel}"" {If(bLooping, " loop", "")} src=""{sURL}"">Your browser does not support the <code>video</code> element.</video>";

                                                    muteButton.Visible = true;
                                                    UpdatePanelAudioChannels.Update();
                                                }
                                            Else
                                                '    fRunner.pbxGraphics.Load(sBuffer)
                                            }


                                            'PlaySound(sSrc, iChannel, bLooping)
                                        }
                                    }
                                }
                            Catch
                            }

                    }
                }
            }
        Next;

        sSource = ""
        For Each sChunk As String In Chunks
            sSource &= sChunk;
        Next;

        if (txtOutputWeb IsNot null)
        {
            txtOutputWeb.InnerHtml &= sSource;
        ElseIf txtOutputWebAbout != null Then
            txtOutputWebAbout.InnerHtml &= sSource;
        }

    }



    private string WaitKeyBuffer
        {
            get
            {
            return SafeString(HttpContext.Current.Session.Item("WaitKeyBuffer"));
        }
set(String)
            HttpContext.Current.Session.Item("WaitKeyBuffer") = value;
        }
    }


    public bool Locked { get; set; }
        {
            get
            {
            return SafeBool(HttpContext.Current.Session.Item("Locked"));
        }
set(ByVal Boolean)
            HttpContext.Current.Session.Item("Locked") = value;
        }
    }

    public void WaitKey()
    {
        Locked = True
    }

    public void Close()
    {

    }

    public void SetTitle(string sText)
    {
        'CType(Page.Master, Site).txtHeader.InnerHtml = "<h1 style=""margin:0px;font-variant:small-caps;font-size:20px;font-family:Arial,sans-serif;font-weight:200;"">" & sText & "</h1>"
        Page.Header.Title = sText;
    }
    'Private Sub StatusBar_TextChanged(sender As Object, e As System.EventArgs) Handles StatusBar.TextChanged

    'End Sub

    public void UpdateStatusBar(string sDescription, string sScore, string sUserStatus)
    {
        txtStatusBar.InnerHtml = sDescription;
        txtScore.InnerHtml = sScore;
        txtUserStatus.InnerHtml = ReplaceALRs(sUserStatus);
        UpdatePanelStatusBar.Update();
    }


    private void WebSplitter1_SplitterPaneSizeChanged(object sender, Infragistics.Web.UI.LayoutControls.SplitterPaneSizeChangedEventArgs e)
    {
        Debug.WriteLine(e.Pane.Size);
        MapWidth = CInt(WebSplitter1.Panes(1).Size.Value)
    }

    public void RefreshMap()
    {
        ' Required for IE, Firefox or Safari (not Chrome), so it thinks it's a unique image
        MapImage.ImageUrl = "~/MapHandler.ashx?" + New Random().Next;

        UpdatePanelMap.Update();
    }


    private void muteButton_Click(object sender, ImageClickEventArgs e)
    {

        if (muteButton.ToolTip = "Unmute audio")
        {
            muteButton.ToolTip = "Mute audio";
            muteButton.ImageUrl = "https://play.adrift.co/img/unmute.png";
            muteButton.OnClientClick = "MuteAudio(true)";
        Else
            muteButton.ToolTip = "Unmute audio";
            muteButton.ImageUrl = "https://play.adrift.co/img/mute.png";
            muteButton.OnClientClick = "MuteAudio(false)";
        }

    }

}
}