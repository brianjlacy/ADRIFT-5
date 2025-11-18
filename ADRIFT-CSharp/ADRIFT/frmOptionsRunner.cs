using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmOptionsRunner
{

    private bool bChanged;
    private Font fontDefault;
    private bool bLoadedAdvanced = false;


    public bool Changed { get; set; }
        {
            get
            {
            return bChanged;
        }
set(ByVal Value As Boolean)
            bChanged = Value
            if (bChanged)
            {
                btnApply.Enabled = true;
            Else
                btnApply.Enabled = false;
            }
        }
    }


    private void LoadForm()
    {

        With UserSession;
            .iMarginWidth = CInt(GetSetting("ADRIFT", "Runner", "Margin", "10"));
            nudMargin.Value = .iMarginWidth;

            chkBlankLine.Checked = CBool(GetSetting("ADRIFT", "Runner", "BlankLine", "0"));
            chkLocationNames.Checked = .bShowShortLocations;
            chkGraphics.Checked = .bGraphics;
            chkSound.Checked = .bSound;
            chkUseMyFont.Checked = CBool(GetSetting("ADRIFT", "Runner", "Myfont", "0"));

            pnlInputColour.BackColor = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text1", ColorTranslator.ToOle(Color.FromArgb(210, 37, 39)).ToString)));
            pnlOutputColour.BackColor = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Text2", ColorTranslator.ToOle(Color.FromArgb(25, 165, 138)).ToString)));
            pnlLinkColour.BackColor = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "LinkColour", ColorTranslator.ToOle(Color.FromArgb(75, 215, 188)).ToString)));
            pnlBackgroundColour.BackColor = ColorTranslator.FromOle(CInt(GetSetting("ADRIFT", "Runner", "Background", ColorTranslator.ToOle(Color.Black).ToString)));

            chkAllowColours.Checked = CBool(GetSetting("ADRIFT", "Runner", "AllowColours", "1"));
            chkAllowFonts.Checked = CBool(GetSetting("ADRIFT", "Runner", "AllowFonts", "1"));
            chkEnableMenu.Checked = CBool(GetSetting("ADRIFT", "Runner", "EnableMenu", "1"));
            chkDisplayLinks.Checked = CBool(GetSetting("ADRIFT", "Runner", "DisplayLinks", "0"));

            if (Adventure IsNot null)
            {
                chkDisplayLinks.Enabled = Adventure.EnableMenu;
                chkEnableMenu.Enabled = Adventure.EnableMenu;
            }

            fontDefault = .DefaultFont
            txtFont.Text = fontDefault.Name + ", " + SafeInt(Math.Round(fontDefault.Size, MidpointRounding.AwayFromZero));

            Changed = False
            'Me.Show()
        }

    }


    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyOptions();
        Me.Close();
    }


    private void ApplyOptions()
    {

        With UserSession;
            .iMarginWidth = CInt(nudMargin.Value);
            fRunner.SetMargins();

            SaveSetting("ADRIFT", "Runner", "Margin", .iMarginWidth.ToString);
            SaveSetting("ADRIFT", "Runner", "BlankLine", CInt(chkBlankLine.Checked).ToString);
            .bShowShortLocations = chkLocationNames.Checked;
            SaveSetting("ADRIFT", "Runner", "showshortroom", CInt(.bShowShortLocations).ToString);

            'colInput = pnlInputColour.BackColor
            SaveSetting("ADRIFT", "Runner", "Text1", ColorTranslator.ToOle(pnlInputColour.BackColor).ToString);
            fRunner.SetInputColour();
            'fRunner.txtInput.ForeColor = colInput

            'colOutput = pnlOutputColour.BackColor
            SaveSetting("ADRIFT", "Runner", "Text2", ColorTranslator.ToOle(pnlOutputColour.BackColor).ToString);

            'colLink = pnlLinkColour.BackColor
            SaveSetting("ADRIFT", "Runner", "LinkColour", ColorTranslator.ToOle(pnlLinkColour.BackColor).ToString);
            SaveSetting("ADRIFT", "Runner", "Background", ColorTranslator.ToOle(pnlBackgroundColour.BackColor).ToString);

            SaveSetting("ADRIFT", "Runner", "AllowColours", CInt(chkAllowColours.Checked).ToString);
            AllowDevToSetColours = chkAllowColours.Checked

            fRunner.SetBackgroundColour();
            'fRunner.txtOutput.BackColor = pnlBackgroundColour.BackColor
            'fRunner.pnlTop.BackColor = pnlBackgroundColour.BackColor
            'fRunner.txtInput.BackColor = pnlBackgroundColour.BackColor
            'fRunner.pnlBottom.BackColor = pnlBackgroundColour.BackColor

            .DefaultFont = fontDefault;
            fRunner.txtInput.SelectionFont = .DefaultFont;

            SaveSetting("ADRIFT", "Runner", "AllowFonts", CInt(chkAllowFonts.Checked).ToString);
            SaveSetting("ADRIFT", "Runner", "EnableMenu", CInt(chkEnableMenu.Checked).ToString);
            SaveSetting("ADRIFT", "Runner", "DisplayLinks", CInt(chkDisplayLinks.Checked).ToString);

            UserSession.bUseDefaultFont = chkUseMyFont.Checked;
            SaveSetting("ADRIFT", "Runner", "Myfont", CInt(chkUseMyFont.Checked).ToString);

            SaveSetting("ADRIFT", "Runner", "FontName", .DefaultFont.Name);
            SaveSetting("ADRIFT", "Runner", "Font Size", .DefaultFont.Size.ToString);
            SaveSetting("ADRIFT", "Runner", "FontBold", CInt(.DefaultFont.Bold).ToString);
            SaveSetting("ADRIFT", "Runner", "FontItalic", CInt(.DefaultFont.Italic).ToString);

            .bGraphics = chkGraphics.Checked;
            SaveSetting("ADRIFT", "Runner", "Graphics", CInt(.bGraphics).ToString);
            .bSound = chkSound.Checked;
            SaveSetting("ADRIFT", "Runner", "Sound", CInt(.bSound).ToString);

            if (bLoadedAdvanced)
            {
                private bool bResetSound = false;
                If chkSoundWinMM.Checked <> SafeBool(GetSetting("ADRIFT", "Shared", "WinMM", "1")) Then bResetSound = true
                If chkSoundDirectX.Checked <> SafeBool(GetSetting("ADRIFT", "Shared", "DirectX", "1")) Then bResetSound = true
                If chkSoundSoundPlayer.Checked <> SafeBool(GetSetting("ADRIFT", "Shared", "SoundPlayer", "1")) Then bResetSound = true

                SaveSetting("ADRIFT", "Shared", "WinMM", CInt(chkSoundWinMM.Checked).ToString);
                SaveSetting("ADRIFT", "Shared", "DirectX", CInt(chkSoundDirectX.Checked).ToString);
                SaveSetting("ADRIFT", "Shared", "SoundPlayer", CInt(chkSoundSoundPlayer.Checked).ToString);

                If bResetSound Then ResetSounds()

            }

            if (Not .bSound)
            {
                for (int iChannel = 1; iChannel <= 8; iChannel++)
                {
                    StopSound(iChannel);
                Next;
            }
            'Adventure.Changed = True
        }

    }


    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyOptions();
        Changed = False
    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Runner", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyOptions()
            if (result = Windows.Forms.DialogResult.Cancel)
            {
                DialogResult = Nothing
                Exit Sub;
            }
        }
        Me.Close();
    }



    private void frmOptions_Load(object sender, System.EventArgs e)
    {
        GetFormPosition(Me);
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgOptions16.GetHicon);
    }


    private void StuffChanged(object sender, System.EventArgs e)
    {
        Changed = True
    }


    private void pnlInputColour_Click(object sender, System.EventArgs e)
    {

        private New ColorDialog dlgColour;
        dlgColour.Color = (Panel)(sender).BackColor;

        if (dlgColour.ShowDialog = Windows.Forms.DialogResult.OK)
        {
            (Panel)(sender).BackColor = dlgColour.Color;
            Changed = True
        }

    }


    private void btnResetColours_Click(System.Object sender, System.EventArgs e)
    {

        pnlInputColour.BackColor = Color.FromArgb(DEFAULT_INPUTCOLOUR) ' 210, 37, 39);
        pnlOutputColour.BackColor = Color.FromArgb(DEFAULT_OUTPUTCOLOUR) ' 25, 165, 138);
        pnlLinkColour.BackColor = Color.FromArgb(DEFAULT_LINKCOLOUR) ' 75, 215, 188);
        pnlBackgroundColour.BackColor = Color.FromArgb(DEFAULT_BACKGROUNDCOLOUR) ' Color.Black;
        nudMargin.Value = 10;
        chkBlankLine.Checked = false;
        chkLocationNames.Checked = true;
        chkGraphics.Checked = true;
        chkSound.Checked = true;
        chkAllowFonts.Checked = true;
        chkAllowColours.Checked = true;
        chkEnableMenu.Checked = true;
        chkDisplayLinks.Checked = false;

        fontDefault = New Font("Arial", 12, FontStyle.Regular)
        txtFont.Text = "Arial, 12";
        chkUseMyFont.Checked = false;

        Changed = True
    }


    private void btnSetFont_Click(System.Object sender, System.EventArgs e)
    {

        With fd;
            .FontMustExist = false;
            .MinSize = 8;
            .MaxSize = 36;

            'If txtFont.Text <> "" Then
            '    Dim sName As String = ""
            '    Dim emSize As Single = 0

            '    Dim sFont() As String = txtFont.Text.Split(","c)
            '    If sFont(0) <> "" Then sName = sFont(0)

            '    If sFont(1) <> "" Then
            '        If SafeDbl(sFont(1)) > 0 Then emSize = CSng(SafeDbl(sFont(1)))
            '    End If
            '    If sName <> "" AndAlso emSize > 0 Then
            '        Dim f As New Font(sName, emSize)
            '        .Font = f
            '    End If
            'End If
            .Font = fontDefault;
            if (.ShowDialog = Windows.Forms.DialogResult.OK)
            {
                Dim sFontInfo() As String = .Font.ToString.Split(","c)
                txtFont.Text = sFontInfo(0).Replace("[Font: Name=", "") + ", " + SafeInt(Math.Round(SafeDbl(sFontInfo(1).Replace(" Size=", "")), MidpointRounding.AwayFromZero));
                fontDefault = .Font
                Changed = True
            }
        }
    }

    private void chkUseMyFont_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        txtFont.Enabled = chkUseMyFont.Checked;
        btnSetFont.Enabled = chkUseMyFont.Checked;
    }

    private void chkEnableMenu_CheckedChanged(System.Object sender, System.EventArgs e)
    {
        chkDisplayLinks.Enabled = chkEnableMenu.Checked;
    }

#if Mono
    private void tabsOptions_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (tabsOptions.SelectedTab is tabAdvanced)
        {
#else
    private void tabsOptions_SelectedTabChanged(System.Object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {

        if (e.Tab.Text = "Advanced")
        {
#endif

            if (Not bLoadedAdvanced)
            {

                try
                {
#if Mono
                    private clsSoundInterface IWinMM = null;
#else
                    private New MMSoundInterface(0) IWinMM;
#endif

                    chkSoundWinMM.Enabled = IWinMM != null && IWinMM.IsInitialised;
                    if (chkSoundWinMM.Enabled)
                    {
                        chkSoundWinMM.Checked = SafeBool(GetSetting("ADRIFT", "Shared", "WinMM", "1"));
                    Else
                        chkSoundWinMM.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    chkSoundWinMM.Enabled = false;
                    chkSoundWinMM.Checked = false;
                }

                try
                {
#if Mono
                    private clsSoundInterface IDirectX = null;
#else
                    private New DirectXSoundInterface IDirectX;
#endif

                    chkSoundDirectX.Enabled = IDirectX != null && IDirectX.IsInitialised;
                    if (chkSoundDirectX.Enabled)
                    {
                        chkSoundDirectX.Checked = SafeBool(GetSetting("ADRIFT", "Shared", "DirectX", "1"));
                    Else
                        chkSoundDirectX.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    chkSoundDirectX.Enabled = false;
                    chkSoundDirectX.Checked = false;
                }

                try
                {
                    private New SoundPlayerSoundInterface ISoundPlayer;
                    chkSoundSoundPlayer.Enabled = ISoundPlayer != null && ISoundPlayer.IsInitialised;
                    if (chkSoundSoundPlayer.Enabled)
                    {
                        chkSoundSoundPlayer.Checked = SafeBool(GetSetting("ADRIFT", "Shared", "SoundPlayer", "1"));
                    Else
                        chkSoundSoundPlayer.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    chkSoundSoundPlayer.Enabled = false;
                    chkSoundSoundPlayer.Checked = false;
                }

                bLoadedAdvanced = True
            }
        }

    }

    private void pnlBackgroundColour_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
    {

    }

    private void pnlInputColour_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
    {

    }
}

}