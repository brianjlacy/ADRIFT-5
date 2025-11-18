using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
internal static class WebStuff
{

    'Public WebPage As _Default
    public int iId = 0;
    'Private _sURL As String
    public string sURL
        {
            get
            {
            'Return _sURL
            return SafeString(HttpContext.Current.Session.Item("sURL"));
        }
set(String)
            '_sURL = value
            HttpContext.Current.Session.Item("sURL") = value;
        }
    }


    public int MapHeight
        {
            get
            {
            return SafeInt(HttpContext.Current.Session.Item("MapHeight"));
        }
set(Integer)
            HttpContext.Current.Session.Item("MapHeight") = value;
        }
    }

    public int MapWidth
        {
            get
            {
            return SafeInt(HttpContext.Current.Session.Item("MapWidth"));
        }
set(Integer)
            HttpContext.Current.Session.Item("MapWidth") = value;
        }
    }


public class Cursors
    {
        internal static object Arrow;
        internal static object WaitCursor;
        internal static object Current;
        internal static object SizeAll;
        internal static object NoMove2D;
        internal static object Hand;
    }
    public Cursors Cursor;

    public Point MousePosition;

public class clsApplication
    {
        internal static object ExecutablePath;
        internal static string StartupPath;
        internal static string LocalUserAppDataPath;
        internal static string ProductVersion;

        public void DoEvents()
        {

        }

    }
    public clsApplication Application;
    public _Default ' New frmRunner fRunner;


    public Microsoft.VisualBasic.MsgBoxResult MsgBox(string Prompt, Microsoft.VisualBasic.MsgBoxStyle Buttons, string Title)
    {
        switch (MessageBox.Show(Prompt, Title))
        {
            case DialogResult.OK:
                {
                return MsgBoxResult.Ok;
            case DialogResult.Cancel:
                {
                return MsgBoxResult.Cancel;
            default:
                {
                return MsgBoxResult.Cancel;
        }
    }
    public Microsoft.VisualBasic.MsgBoxResult MsgBox(string Prompt, Microsoft.VisualBasic.MsgBoxStyle Buttons)
    {
        switch (MessageBox.Show(Prompt))
        {
            case DialogResult.OK:
                {
                return MsgBoxResult.Ok;
            case DialogResult.Cancel:
                {
                return MsgBoxResult.Cancel;
            default:
                {
                return MsgBoxResult.Cancel;
        }
    }
    public Microsoft.VisualBasic.MsgBoxResult MsgBox(string Prompt)
    {
        MessageBox.Show(Prompt);
        return MsgBoxResult.Ok;
    }
public class MessageBox
    {
        private static New Hashtable[] m_executingPages;

        public static System.Windows.Forms.DialogResult Show(string text)
        {
            return Show(text, "ADRIFT WebRunner");
        }
        public static System.Windows.Forms.DialogResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButtons.OK);
        }
        public static System.Windows.Forms.DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons, MessageBoxIcon.None);
        }
        public static System.Windows.Forms.DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }
        public static System.Windows.Forms.DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    {
                    HttpContext.Current.Response.Write("<script type=""text/javascript"">alert(""" + text + """);</script>");
                    return DialogResult.OK;
                case MessageBoxButtons.YesNo:
                    {
                    HttpContext.Current.Response.Write("<script type=""text/javascript"">alert(""" + text + "\n\n" + "Yes | [[No]]"");</script>");
                    'HttpContext.Current.Response.Write("<script type=""text/javascript"">confirm(""" & text & "\n" & "(Yes / [No])"");</script>")
                    return DialogResult.No;
                case MessageBoxButtons.YesNoCancel:
                    {
                    HttpContext.Current.Response.Write("<script type=""text/javascript"">alert(""" + text + "\n\n" + "Yes | No | [[Cancel]]"");</script>");
                    return DialogResult.Cancel;
            }
            return DialogResult.Cancel;
        }

    }



    public string InputBox(string Prompt, string Title, string DefaultResponse)
    {
        HttpContext.Current.Response.Write("<script type=""text/javascript"">alert(""Inputbox for " + Prompt + " not allowed in WebRunner"");</script>");
        return "";
    }

}

}