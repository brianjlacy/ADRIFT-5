using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
internal static class Mono
{

    private New CSettings(False) s;

    public string GetSetting(string AppName, string Section, string Key, [Default] As String = "")
    {
        '    Return [Default]
        return SafeString(s.GetSetting(AppName, Section, Key, [Default]));
    }

    public void SaveSetting(string AppName, string Section, string Key, string Value)
    {
        s.SaveSetting(AppName, Section, Key, Value);
    }

    public void AddPrevious()
    {

        private string sFilename;

        for (int iPrevious = 1; iPrevious <= 10; iPrevious++)
        {
            sFilename = GetSetting("ADRIFT", "Runner", "Recent_" & iPrevious, "")
            if (sFilename <> "")
            {
                private ToolStripItem tsi = fRunner.miRecentAdventures.DropDownItems.Add("&" + iPrevious + "  " + sFilename, null, AddressOf fRunner.miRecentAdventures_Click);
                tsi.Tag = sFilename;
            }
        Next;

    }

public class ComboBoxItem
    {
        public Keys KeyboardShortcut;

        Public Shadows ReadOnly Property ToString As String
            {
                get
                {
                return KeyboardShortcut.ToString;
            }
        }

        public void New(Keys KeyboardShortcut)
        {
            Me.KeyboardShortcut = KeyboardShortcut;
        }
    }


    private Color MenuBackColor = Color.FromArgb(60, 59, 55);
    private Color MenuForeColor = Color.FromArgb(223, 219, 210);

    public void SetWindowStyle(Form frmTarget)
    {

        'frmTarget.SuspendLayout()

        ''frmTarget.ForeColor = Color.White
        ''frmTarget.BackColor = Color.SlateGray

        'For Each c As Control In frmTarget.Controls
        '    SetControlStyle(c)
        'Next

        'frmTarget.ResumeLayout()

    }

    public void SetControlStyle(Control c)
    {

        switch (true)
        {
            case TypeOf c Is StatusBar:
                {
                With (StatusBar)(c);

                }
            case TypeOf c Is MenuStrip:
                {
                With (MenuStrip)(c);
                    c.BackColor = MenuBackColor;
                    c.ForeColor = MenuForeColor;
                }
            case TypeOf c Is TabControl:
                {
                With (TabControl)(c);
                    c.BackColor = MenuBackColor;
                    c.ForeColor = MenuForeColor;
                }
            case TypeOf c Is ToolStrip:
                {
                With (ToolStrip)(c);
                    c.ForeColor = MenuForeColor;
                    c.BackColor = MenuBackColor;
                }

            case TypeOf c Is Label:
                {

            case TypeOf c Is SplitterPanel Or TypeOf c Is RichTextBox Or TypeOf c Is SplitContainer Or TypeOf c Is Panel:
                {
                ' Ignore
            default:
                {
                'MsgBox(c.ToString)
        }

        if (Not c.Controls Is null)
        {
            For Each cChild As Control In c.Controls
                SetControlStyle(cChild);
            Next;
        }

    }

}



}