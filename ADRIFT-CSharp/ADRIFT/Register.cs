using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{

'<System.Security.Permissions.PermissionSet(Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
public class frmRegister
{

    ' Authentication Key
    '

    Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long;
    Friend Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As UInt32, ByVal sparam As UInt32, ByVal lparam As UInt32) As UInt32;
    private const Int32 BCM_FIRST = &H1600;
    private const Int32 BCM_SETSHIELD = (BCM_FIRST + &HC);

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        DialogResult = Windows.Forms.DialogResult.Cancel
    }

    public bool IsVistaOrHigher()
    {
        return Environment.OSVersion.Version.Major < 6;
    }

    ' Checks if the process is elevated
    'Public Function IsAdmin() As Boolean
    '    Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()
    '    Dim p As WindowsPrincipal = New WindowsPrincipal(id)
    '    Return p.IsInRole(WindowsBuiltInRole.Administrator)
    'End Function
    private bool IsAdmin()
    {
        return My.User.IsInRole(Microsoft.VisualBasic.ApplicationServices.BuiltInRole.Administrator);
    }

    ' Add a shield icon to a button
    public void AddShield(Button button)
    {
        button.FlatStyle = Windows.Forms.FlatStyle.System;
        private UInt32 success = SendMessage(button.Handle, BCM_SETSHIELD, 0, 1);
    }

    ' Restart the current process with administrator credentials
    public void RestartElevated()
    {
        private ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.UseShellExecute = true;
        startInfo.WorkingDirectory = Environment.CurrentDirectory;
        startInfo.FileName = Application.ExecutablePath;
        startInfo.Verb = "runas";
        try
        {
            private Process p = Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            return 'If cancelled, do nothing;
        }
        Application.Exit();
    }

    private string GetWinSysDir()
    {

        private string strPath;
        strPath = Space$(1024)
        private int iLen = CInt(GetSystemDirectory(strPath, Len(strPath)));
        strPath = sLeft(strPath, iLen)
        If sRight(strPath, 1) <> "\" Then strPath = strPath + "\"

        GetWinSysDir = strPath

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {

        try
        {
            Cursor = Cursors.WaitCursor
            Application.DoEvents();

            btnOK.Enabled = false;
            For Each txt As UltraTextEditor In New UltraTextEditor() {txtUsername, txtKey1, txtKey2, txtKey3, txtKey4, txtKey5}
                txt.Enabled = false;
            Next;

            private string sUsername = txtUsername.Text;
            private string sKey = txtKey1.Text + "-" + txtKey2.Text + "-" + txtKey3.Text + "-" + txtKey4.Text + "-" + txtKey5.Text;

            if (IsKeyValid(sUsername, sKey))
            {
                ' Attempt to send username, key and details to ADRIFT server
                private string sURL = String.Format("https://www.adrift.co/cgi/new/validate.cgi?fg=1&username={0}&k1={1}&k2={2}&k3={3}&k4={4}&k5={5}&identifier={6}", URLEncode(txtUsername.Text), txtKey1.Text, txtKey2.Text, txtKey3.Text, txtKey4.Text, txtKey5.Text, GetDetails);
                private string sResult;

                if (txtValidation.Text <> "")
                {
                    sResult = "script=displayvalidation######" & txtValidation.Text & vbCrLf & "###############"
                Else
                    sResult = OpenURL(sURL)
                }

                if (sResult.StartsWith("Unable to connect to host:"))
                {
                    if (sResult.Contains("No such host is known"))
                    {

                    Else

                    }
                    ' Copy the validation URL to the clipboard
                    Clipboard.SetText(sURL);
                    ErrMsg("Unable to connect to the ADRIFT server to validate registration.  Please visit the following URL to obtain your validation code:" + vbCrLf + vbCrLf + sURL + vbCrLf + vbCrLf + "For your convenience, this address has been copied to the clipboard.");
                    lblValidation.Visible = true;
                    txtValidation.Visible = true;
                Else
                    sResult = sResult
                    if (sResult.Contains("page=baduser") || sResult.Contains("page=badkey"))
                    {
                        ErrMsg("Error validating registration.  Your username or code does not appear to be valid." + vbCrLf + vbCrLf + "Please contact registrations@adrift.org.uk for further information.");
                    ElseIf sResult.Contains("page=exhaustedkey") Then
                        '  Code dished out too many times
                        ErrMsg("Although valid, this code has been used too many times and so has currently been suspended." + vbCrLf + vbCrLf + "Please contact registrations@adrift.org.uk for further information.");
                    ElseIf sResult.Contains("script=displayvalidation") Then
                        '  Received Validation code.  Check that it matches what we would expect
                        private string sValidationCode = sResult.Substring(sResult.IndexOf("script=displayvalidation") + 30, 12) ' Grab from sResult;
                        If sValidationCode.Contains(vbCrLf) Then sValidationCode = sLeft(sValidationCode, sValidationCode.IndexOf(vbCrLf))
                        if (Not IsAuthenticationValid(sValidationCode, sKey.Replace("-", "")))
                        {
                            ErrMsg("Validation code does not appear to be correct for this machine.  Please contact registrations@adrift.org.uk for further information.");
                        Else
                            if (Register(sUsername, sKey))
                            {
                                fGenerator.PartOne = true;
                                MsgBox("ADRIFT has been successfully registered!", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "ADRIFT Registered");
                                fGenerator.PartTwo = true;
                                fGenerator.UTMMain.Tools("Register").SharedProps.Visible = false;
                                DialogResult = Windows.Forms.DialogResult.OK
                            Else
                                ErrMsg("Registration failed.  Please contact registration@adrift.org.uk for further information.");
                            }
                        }
                    Else
                        ErrMsg("There was a problem validating your registration.  Please try again later.");
                    }
                }

                Exit Sub;
            Else
                Sleep(3);
                ErrMsg("Bad Key.  Please ensure you enter your username and key properly.  The username is case sensitive.");
                Exit Sub;
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error registering app", ex);
            Exit Sub;
        }
        finally
        {
            btnOK.Enabled = true;
            For Each txt As UltraTextEditor In New UltraTextEditor() {txtUsername, txtKey1, txtKey2, txtKey3, txtKey4, txtKey5}
                txt.Enabled = true;
            Next;
            Cursor = Cursors.Arrow
        }

        DialogResult = Windows.Forms.DialogResult.OK
    }


    private bool Register(string sUsername, string sKey)
    {

        private int iDebug = 0;

        try
        {
            private string sDLL = GetWinSysDir() + "comcat.dll";
            iDebug = 10
            If ! IO.File.Exists(sDLL) Then sDLL = GetWinSysDir() + "dxmasf.dll"
            iDebug = 20
            If ! IO.File.Exists(sDLL) Then sDLL = GetWinSysDir() + "imapi.dll"
            iDebug = 30
            If ! IO.File.Exists(sDLL) Then sDLL = GetWinSysDir() + "nrpsrv.dll"
            iDebug = 40

            if (IO.File.Exists(sDLL))
            {
                iDebug = 50
                private string sNewDLL = GetWinSysDir() + "idesync.dll";

                iDebug = 60
                If ! IO.File.Exists(sNewDLL) Then IO.File.Copy(sDLL, sNewDLL)
                iDebug = 70
                IO.File.SetAttributes(sNewDLL, IO.FileAttributes.Hidden);
                iDebug = 80
                IO.File.SetAttributes(sNewDLL, IO.FileAttributes.System);
                iDebug = 90
            Else
                iDebug = 100
                throw New Exception("Error #1")
            }

            iDebug = 110
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\.txt", "Ascii", "0x00000003");

            iDebug = 120
            SaveSetting("ADRIFT", "Shared", "RegName", Dencode(sUsername));
            iDebug = 130
            SaveSetting("ADRIFT", "Shared", "RegNum", Dencode(sKey));
            iDebug = 140

            return true;
        }
        catch (Exception ex)
        {
            ErrMsg("Registration error #" + iDebug);
            return false;
        }

    }


    private void frmRegister_Load(object sender, System.EventArgs e)
    {
        if (Not IsAdmin())
        {
            lblError.BringToFront();
            AddShield(btnRestart);
            btnRestart.Visible = true;
            btnOK.Enabled = false;
            txtUsername.Enabled = false;
        Else
            btnRestart.Visible = false;
            lblError.Visible = false;
        }

        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgKey16.GetHicon);
        GetFormPosition(Me);
    }


    private void btnRestart_Click(object sender, System.EventArgs e)
    {
        RestartElevated();
    }


    ' XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
    ' 2 = 4th Character, 4 = Length
    ' 1 = 2nd Last Character, 5 = Registered Month
    ' 3 = 4th Last Character
    ' 3 = Registered Year, 4 = First + Last character
    ' 5 = Registered Day
    '
    public bool IsKeyValid(string sUsername, string sKey)
    {

        If sKey.Length <> 29 Then Return false
        sKey = sKey.Replace("-", "")

        private int iChar = 4;
        If sUsername.Length < iChar Then iChar = sUsername.Length
        If GetChar(Asc(sUsername(iChar - 1)) + iChar) <> sKey(1) Then Return false
        If GetChar(sUsername.Length * 3) <> sKey(3) Then Return false
        iChar = sUsername.Length - 1
        If sUsername.Length < 2 Then iChar = 1
        If GetChar(Asc(sUsername(iChar - 1)) + iChar) <> sKey(5) Then Return false
        iChar = sUsername.Length - 3
        If sUsername.Length < 4 Then iChar = 1
        If GetChar(Asc(sUsername(iChar - 1)) + iChar) <> sKey(12) Then Return false
        If GetChar(Asc(sUsername(0)) + Asc(sUsername(sUsername.Length - 1))) <> sKey(18) Then Return false
        If GetChar(sUsername.Length * 3) <> sKey(20) Then Return false

        return true;

    }


    public string IntToBase(int iTradeId, byte bBase = 36)
    {

        ' Any more than base 36 will put you into [\]^_` type characters

        private int iLeftToProcess = iTradeId;
        private int iLeastSignificant;
        IntToBase = ""

        try
        {
            If bBase < 2 || bBase > 36 Then Throw New System.Exception("Base value must be between 2 and 36")

            while (iLeftToProcess > 0)
            {

                iLeastSignificant = iLeftToProcess Mod bBase
                if (iLeastSignificant < 10)
                {
                    IntToBase = Chr(48 + iLeastSignificant) & IntToBase ' 0-9
                Else
                    IntToBase = Chr(55 + iLeastSignificant) & IntToBase ' A-Z
                }

                iLeftToProcess \= bBase;

            }

        }
        catch (System.Exception ex)
        {
            ErrMsg("Undefined Error", ex);
        }

    }


    ' Probably won't be used, but reciprocal function to InToBase
    public int BaseToInt(string sBaseNum, byte bBase = 36)
    {

        try
        {

            If bBase < 2 || bBase > 36 Then Throw New System.Exception("Base value must be between 2 and 36")
            BaseToInt = 0

            For Each cDigit As Char In sBaseNum.ToCharArray
                switch (cDigit)
                {
                    case "0"c To "9"c:
                        {
                        BaseToInt = (BaseToInt * bBase) + Asc(cDigit) - 48
                    default:
                        {
                        BaseToInt = (BaseToInt * bBase) + Asc(cDigit) - 55
                }
            Next cDigit;

        }
        catch (System.Exception ex)
        {
            ErrMsg("Undefined Error", ex);
        }

    }


    ' 1st 2 characters of disk size -> b36
    ' 19th char of Key + 2 -> b36
    ' Processor Count + 13 -> b36
    ' Day of year + 5 -> b36
    ' Mem (in MB) + 3 -> b36
    ' 2nd char of Key - 1 -> b36
    ' Win Ver (3 * Major + Minor) -> b36
    '
    public bool IsAuthenticationValid(string sAuth, string sKey)
    {

        If sAuth.Length <> 7 Then Return false

        If sAuth(0) <> GetChar(SafeInt(sLeft(DiskSize.ToString, 2))) Then Return false
        If sAuth(1) <> GetChar(BaseToInt(sKey(19)) + 2) Then Return false
        If sAuth(2) <> GetChar(Environment.ProcessorCount + 13) Then Return false
        If sAuth(3) <> GetChar(Today.ToUniversalTime.DayOfYear + 3) && sAuth(3) <> GetChar(Today.ToUniversalTime.DayOfYear + 4) && sAuth(3) <> GetChar(Today.ToUniversalTime.DayOfYear + 5) Then Return false
        If sAuth(4) <> GetChar(SafeInt(TotalMemory() / 1024) + 3) Then Return false
        If sAuth(5) <> GetChar(BaseToInt(sKey(2)) - 1) Then Return false
        If sAuth(6) <> GetChar(3 * Environment.OSVersion.Version.Major + Environment.OSVersion.Version.Minor) Then Return false

        return true;

    }


    ' === Details section ===
    ' Disk size
    ' Total Memory
    ' Windows Version
    ' Processor Count
    '
    public string GetDetails()
    {

        private int iProcessorCount = Environment.ProcessorCount;
        private long lTotalBytes = DiskSize();
        private int lTotalMemory = SafeInt(TotalMemory() / 1024);
        private string sWinVer = Environment.OSVersion.Version.ToString;

        private string sResult = (iProcessorCount + "A" + lTotalBytes + "B" + sWinVer).Replace(".", "C") + "D" + lTotalMemory;
        private string sResult2 = "";

        for (int i = 0; i <= sResult.Length - 1; i++)
        {
            If i Mod 5 = 0 && sResult2 <> "" Then sResult2 &= "-"
            sResult2 &= IncrementHex(sResult(i));
        Next;
        'Return (iProcessorCount & "A" & lTotalBytes & "B" & sWinVer).Replace(".", "C") & "     " &
        return sResult2;

    }


    private long TotalMemory()
    {

        private New System.Management.ManagementClass("Win32_OperatingSystem") OS_Info;
        For Each OS_Object As System.Management.ManagementObject In OS_Info.GetInstances()
            return CLng(OS_Object("TotalVisibleMemorySize"));
            'Console.WriteLine("Physical mem. : {0}", OS_Object("TotalVisibleMemorySize").ToString)
            'Console.WriteLine("Available mem. : {0}", OS_Object("FreePhysicalMemory").ToString)
        Next;

    }


    private long DiskSize()
    {

        For Each d As IO.DriveInfo In IO.DriveInfo.GetDrives
            if (Application.ExecutablePath.StartsWith(d.Name))
            {
                return CLng(d.TotalSize / 1024 / 1024);
            }
        Next;
        return 0;

    }


    private string IncrementHex(char sHex)
    {

        private int iHex;
        switch (sHex)
        {
            case "0"c To "9"c:
                {
                iHex = SafeInt(sHex.ToString)
            case "A"c To "F"c:
                {
                iHex = Asc(sHex) - 55
        }

        iHex += 2;
        iHex = iHex Mod 16

        return Hex(iHex);

    }


    private void txtUsername_TextChanged(object sender, System.EventArgs e)
    {
        btnOK.Enabled = txtUsername.Text.Length > 0;
        CalcKey(txtUsername.Text);
    }


    private void CalcKey(string sUsername)
    {

        If sUsername.Length = 0 Then Exit Sub

        ' 4th char
        private int iChar = 4;
        If sUsername.Length < iChar Then iChar = sUsername.Length
        private char c2 = GetChar(Asc(sUsername(iChar - 1)) + iChar);

        ' Length
        private char c4 = GetChar(sUsername.Length * 3);

        ' 2nd Last char
        iChar = sUsername.Length - 1
        If sUsername.Length < 2 Then iChar = 1
        private char c6 = GetChar(Asc(sUsername(iChar - 1)) + iChar);

        ' 4th Last char
        iChar = sUsername.Length - 3
        If sUsername.Length < 4 Then iChar = 1
        private char c13 = GetChar(Asc(sUsername(iChar - 1)) + iChar);

        ' First + Last char
        private char c19 = GetChar(Asc(sUsername(0)) + Asc(sUsername(sUsername.Length - 1)));

        ' Username Length
        private char c21 = GetChar(sUsername.Length * 3);


        private string sKey = "X" + c2 + "X" + c4 + "X-" + c6 + "XXXX-XX" + c13 + "XX-XXX" + c19 + "X-" + c21 + "XXXX";

        Debug.WriteLine(sKey + "       " + GetDetails());

    }


    private char GetChar(int iVal)
    {

        If iVal < 0 Then iVal += 36
        iVal = iVal Mod 36
        If iVal < 10 Then Return (iVal.ToString)(0)
        return CChar(ChrW(Asc("A"c) + iVal - 10));
        'If iVal < 26 Then Return CChar(ChrW(Asc("A") + iVal))
        'Return ((iVal - 26).ToString)(0)

    }


    private void txtKey_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {

        switch (e.KeyChar)
        {
            case "a"c To "z"c:
            case "A"c To "Z"c:
            case "0"c To "9"c:
            case Chr(8):
                {
                ' Allow
            default:
                {
                e.Handled = true;
        }
    }



    private void txtKey_ValueChanged(System.Object sender, System.EventArgs e)
    {

        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtKey = CType(sender, Infragistics.Win.UltraWinEditors.UltraTextEditor);
        private int iStart = txtKey.SelectionStart;
        private int iLen = txtKey.SelectionLength;

        txtKey.Text = txtKey.Text.ToUpper;
        txtKey.SelectionStart = iStart;
        txtKey.SelectionLength = iLen;

        if (txtKey.TextLength = 5)
        {
            switch (true)
            {
                case sender Is txtKey1:
                    {
                    txtKey2.Focus();
                case sender Is txtKey2:
                    {
                    txtKey3.Focus();
                case sender Is txtKey3:
                    {
                    txtKey4.Focus();
                case sender Is txtKey4:
                    {
                    txtKey5.Focus();
                case sender Is txtKey5:
                    {
                    btnOK.Focus();
            }
        }
    }


    private void frmRegister_Shown(object sender, System.EventArgs e)
    {
        txtUsername.Focus();
    }

}
}