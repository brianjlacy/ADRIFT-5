using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public sealed class AboutBox
{

    private void AboutBox1_Load(System.Object sender, System.EventArgs e)
    {
        ' Set the title of the form.
        private string ApplicationTitle;
        if (My.Application.Info.Title <> "")
        {
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        }
        Me.Text = String.Format("About {0}", ApplicationTitle);
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project
        '    properties dialog (under the "Project" menu).
        'Me.lblProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.Major + "." + My.Application.Info.Version.Minor + "." + My.Application.Info.Version.Build + "." + My.Application.Info.Version.Revision);
        Me.LabelCopyright.Text = My.Application.Info.Copyright;
        Me.lblInfo.Text = My.Application.Info.Description.Split(Chr(10))(0) + vbCrLf + vbCrLf ' Split here because RPX appends to this;
#if Mono
        lblInfo.Text = lblInfo.Text.Replace("Windows", "Linux");
#endif
#if Generator
        Me.lblInfo.Text &= "This product is Donationware, not Freeware.  If you enjoy this product and use it a lot, you are encouraged to make a donation.";
        if (IsRegistered)
        {
            private string sRegisteredTo = Dencode(GetSetting("ADRIFT", "Shared", "RegName", ""));
            if (sRegisteredTo <> "")
            {
                Me.lblRegistered.Text = "Registered to " + sRegisteredTo 'Dencode(sRegisteredTo, 0).ToString;
            Else
                Me.lblRegistered.Text = "" 'Registered copy";
            }
        Else
            Me.lblRegistered.Text = "" 'Unregistered copy";
            'lblRegistered.Appearance.ForeColor = Color.Red
        }
#else
        Me.lblRegistered.Text = "";
        Me.lblInfo.Text &= "This product is Freeware.";
#if Not Mono
        btnDonate.Visible = false;
#endif
#endif

        Me.lblInfo.Text &= "  " + ApplicationTitle + " was created using Visual Studio 2005/08/10.";
        Me.lblInfo.Text &= vbCrLf + vbCrLf + "Splash image ""Adrift"" © V. Milovic 2010 (http://www.brokentoyland.com) and used with permission.";

        ' Randomly swap the OK/Donate buttons around
#if Not Mono
        if (Random(0, 1) = 1)
        {
            private int iX = btnDonate.Location.X;
            btnDonate.Location = New Point(OKButton.Location.X, btnDonate.Location.Y);
            OKButton.Location = New Point(iX, OKButton.Location.Y);
            'CancelButton = btnDonate
            AcceptButton = btnDonate
            btnDonate.TabIndex = 0;
            btnDonate.TabStop = true;
        }
#endif
        'Try
        '    'For Each ff As FontFamily In GetFont.Families
        '    '    If ff.IsStyleAvailable(FontStyle.Regular) Then
        '    '        lblProductName.Font = New Font(ff, lblProductName.Font.Size, FontStyle.Regular)
        '    '    End If
        '    'Next
        '    Dim sFont As String = Application.StartupPath & IO.Path.DirectorySeparatorChar & "LYDIAN.TTF"
        '    If IO.File.Exists(sFont) Then
        '        Dim pfc As New System.Drawing.Text.PrivateFontCollection
        '        pfc.AddFontFile(sFont)
        '        For Each ff As FontFamily In pfc.Families
        '            If ff.IsStyleAvailable(FontStyle.Regular) Then
        '                lblProductName.Font = New Font(ff, lblProductName.Font.Size, FontStyle.Regular)
        '            End If
        '        Next
        '    End If
        'Catch ex As Exception
        'End Try

    }

    private void OKButton_Click(System.Object sender, System.EventArgs e)
    {
        Me.Close();
    }

    private void LogoPictureBox_Click(System.Object sender, System.EventArgs e)
    {
        System.Diagnostics.Process.Start("http://www.brokentoyland.com");
    }

    private void UltraGroupBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {

        If pfc == null Then pfc = GetFontCollection()
        private Font f;

        For Each ff As FontFamily In pfc.Families
            if (ff.IsStyleAvailable(FontStyle.Regular))
            {
                f = New Font(ff, 14, FontStyle.Regular)
                e.Graphics.DrawString(My.Application.Info.ProductName, f, New SolidBrush(Color.Black), New Point(354, 13));
            }
        Next;

    }

#if Not Mono
    private void btnDonate_Click(System.Object sender, System.EventArgs e)
    {
        System.Diagnostics.Process.Start("https://www.paypal.com/uk/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=FTJPYPFLJLCV6");
    }
#endif

}

}