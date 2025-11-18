using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
Option Strict On;


public class frmError
{

    'Private bSend As Boolean = False
    private int iBugId = 0;
private enum BugStatusEnum As Integer
    {
        Failed = 0
        Added = 1
        AlreadyExists = 2
        ExistsAndCompleted=3;
    }

    public void New(string sErrorMessage, Exception ex = null)
    {

        ' This call is required by the Windows Form Designer.
        InitializeComponent();

        ' Add any initialization after the InitializeComponent() call.
        private string sStackTrace = null;
        if (Not ex Is null)
        {
            sErrorMessage &= ": " + ex.Message;
            sStackTrace = New System.Diagnostics.StackTrace(True).ToString
            'sStackTrace = ex.StackTrace()
            'While Not ex.InnerException Is Nothing
            '    ex = ex.InnerException
            '    sStackTrace &= vbCrLf & ex.Message & vbCrLf & ex.StackTrace
            'End While
        }

        LoadForm(sErrorMessage, sStackTrace);

    }



    private void LoadForm(string sErrorMessage, string sStackTrace)
    {

        Me.lblErrorMessage.Text = sErrorMessage;
        SetOptimumSize();
        'lblFeedback.Appearance.BackColor = Color.FromArgb(rSession.cSystemColour.A, Math.Max(rSession.cSystemColour.R - 25, 0), Math.Max(rSession.cSystemColour.G - 25, 0), Math.Max(rSession.cSystemColour.B - 25, 0))
        if (sStackTrace IsNot null)
        {
            'bSend = True
            imgCross.Visible = true;
            imgExclamation.Visible = false;
            Me.txtStackTrace.Text = sStackTrace;
            'If rSession.bShowStackTrace Then
            OpenST();
            'Else
            'CloseST()
            'End If
            System.Media.SystemSounds.Hand.Play();
            LogError(sErrorMessage, sStackTrace);
        Else
            'bSend = False
            imgCross.Visible = false;
            imgExclamation.Visible = true;
            CloseST();
            btnUp.Visible = false;
            btnDown.Visible = false;
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle;
            System.Media.SystemSounds.Exclamation.Play();
            tabsError.Visible = false;
        }

    }



    private void LogError(string sErrorMessage, string sStackTrace)
    {
        ' Send the error to ADRIFT and await acknowlegement
        ' 0 - fail, 1 - added success, 2 - already exists, 3 - exists and completed
        ' if added or already exists, get bug id
        ' if completed, get date and version

        private string sParam = "sShortDesc=" + URLEncode(sErrorMessage);
        If sStackTrace != null Then sParam &= "&sException=" + URLEncode(sStackTrace)
        sParam &= "&Validation=203987";
        sParam &= "&sReleaseVer=0";
        sParam &= "&bEnhancement=0";
        sParam &= "&sFoundVer=" + My.Application.Info.Version.Major + "." + My.Application.Info.Version.Minor + "." + My.Application.Info.Version.Build;

        private string sResult = OpenURL("www.adrift.org.uk/cgi/new/submitbug.cgi?" + sParam);

        if (sResult.StartsWith("Unable to connect to host:"))
        {
            Me.tabsError.Tabs("Feedback").Visible = false;
            Me.lblInfo.Text = "Unable to log error with ADRIFT.  Please check your firewall settings - the more of these errors that are logged, the more stable ADRIFT will become.";
        Else

            Dim sResults() As String = sResult.Split("|"c)
            if (sResults.Length > 1 && sResults(1) <> "")
            {
                switch (CType(sResults(1), BugStatusEnum))
                {
                    case BugStatusEnum.Failed:
                        {
                        Me.tabsError.Tabs("Info").Visible = false;
                        Me.tabsError.Tabs("Feedback").Visible = false;
                    case BugStatusEnum.Added:
                        {
                        Me.tabsError.Tabs("Info").Visible = false;
                        iBugId = SafeInt(sResults(2))
                    case BugStatusEnum.AlreadyExists:
                        {
                        Me.tabsError.Tabs("Info").Visible = false;
                        iBugId = CInt(sResults(2))
                    case BugStatusEnum.ExistsAndCompleted:
                        {
                        Me.tabsError.Tabs("Feedback").Visible = false;
                        private DateTime dtCompleted = CDate(sResults(3));
                        private string sReleaseVer = sResults(4).ToString;
                        lblInfo.Text = "This bug was completed on " + dtCompleted.ToLongDateString + ".  You need to upgrade to version " + sReleaseVer + " to prevent this error from reoccurring.";
                }
            Else
                Me.tabsError.Tabs("Info").Visible = false;
                Me.tabsError.Tabs("Feedback").Visible = false;
            }
            'Me.txtFeedback.Text = sResult
        }

    }


    private void SendFeedback()
    {

        if (iBugId > 0 && txtFeedback.Text <> "")
        {
            ' Send additional info
            private string sParam = "sDescription=" + URLEncode(txtFeedback.Text);
            sParam &= "&Validation=203987";
            sParam &= "&iBugId=" + iBugId;
            sParam &= "&User=" + URLEncode(Environment.UserName);

            private string sResult = OpenURL("www.adrift.org.uk/cgi/new/submitfeedback.cgi?" + sParam);

        }

    }


    private void OpenST()
    {
        btnUp.Visible = true;
        btnDown.Visible = false;
        Me.Size = New Size(Me.Width, Me.Height + 150);
        'pnlDropdown.Height = Me.Height - pnlDropdown.Top - 45
    }


    private void CloseST()
    {
        btnUp.Visible = false;
        btnDown.Visible = true;
        Me.Size = New Size(Me.Width, btnOK.Top + btnOK.Height + 40);
    }


    private Size MeasureString(string str, int maxWidth, Font font)
    {

        try
        {
            private Graphics g = CreateGraphics();
            private SizeF strRectSizeF = g.MeasureString(str, font, maxWidth);
            g.Dispose();

            return new Size(CInt(Math.Ceiling(strRectSizeF.Width)), CInt(Math.Ceiling(strRectSizeF.Height)));
        Catch
            ' This has returned an Error creating window handle before, arrrgh!
            return new Size(CInt(Screen.PrimaryScreen.Bounds.Width / 2), CInt(Screen.PrimaryScreen.Bounds.Height / 3));
        }

    }



    private void SetOptimumSize()
    {

        private Size szText = MeasureString(lblErrorMessage.Text, CInt(SystemInformation.WorkingArea.Width * 0.6), txtStackTrace.Font);
        Me.Size = New Size(szText.Width + 240, szText.Height + 110);
        Me.MinimumSize = Me.Size;
        imgCross.Top = CInt((Me.Size.Height - imgCross.Height) / 2) - 20;
        imgExclamation.Top = imgCross.Top;
        lblErrorMessage.Height = Me.Size.Height - 80;
        btnOK.Top = Me.Size.Height - 67;
        btnUp.Top = btnOK.Top;
        btnDown.Top = btnOK.Top;

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        if (Me.txtFeedback.Text <> "" Then ' bSend)
        {
            '#If DEBUG Then
            '            If MsgBox("Debug mode - do you want email sent out?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then SendMail()
            '#Else
            '            SendMail()
            '#End If
            SendFeedback();
        }

        Me.DialogResult = Windows.Forms.DialogResult.OK;
    }


    private void btnExpand_Click(System.Object sender, System.EventArgs e)
    {
        If btnUp.Visible Then CloseST() Else OpenST()
    }



    private void frmError_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        'If btnUp.Visible Then rSession.bShowStackTrace = True
        Me.Hide();
    }


    private void frmError_Load(object sender, System.EventArgs e)
    {
        'SetWindowStyle(Me)
    }

}
}