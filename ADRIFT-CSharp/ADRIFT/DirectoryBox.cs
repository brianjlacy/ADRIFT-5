using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ADRIFT
{


public class DirectoryBox
{
    Inherits System.Windows.Forms.UserControl;

#Region " Windows Form Designer generated code "

    public void New()
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        Me.BackColor = Color.Transparent;

    }

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        if (disposing)
        {
            if (Not (components Is null))
            {
                components.Dispose();
            }
        }
        MyBase.Dispose(disposing);
    }

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Public WithEvents txtDir As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Private WithEvents btnSearch As Infragistics.Win.Misc.UltraButton
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog;
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog;
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.txtDir = New Infragistics.Win.UltraWinEditors.UltraTextEditor;
        Me.btnSearch = New Infragistics.Win.Misc.UltraButton;
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog;
        Me.ofd = New System.Windows.Forms.OpenFileDialog;
        Me.sfd = New System.Windows.Forms.SaveFileDialog;
        (System.ComponentModel.ISupportInitialize)(Me.txtDir).BeginInit();
        Me.SuspendLayout();
        '
        'txtDir
        '
        Me.txtDir.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
                    | System.Windows.Forms.AnchorStyles.Left) _;
                    | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtDir.AutoSize = false;
        Me.txtDir.Location = New System.Drawing.Point(0, 0);
        Me.txtDir.Name = "txtDir";
        Me.txtDir.Size = New System.Drawing.Size(248, 22);
        Me.txtDir.TabIndex = 0;
        '
        'btnSearch
        '
        Me.btnSearch.BackColorInternal = System.Drawing.Color.Transparent;
        Me.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
        Me.btnSearch.Location = New System.Drawing.Point(248, 0);
        Me.btnSearch.Name = "btnSearch";
        Me.btnSearch.Size = New System.Drawing.Size(32, 22);
        Me.btnSearch.TabIndex = 1;
        Me.btnSearch.Text = "···";
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1";
        Me.sfd.FileName = "SaveFileDialog1";
        '
        'DirectoryBox
        '
        Me.BackColor = System.Drawing.SystemColors.Control;
        Me.Controls.Add(Me.btnSearch);
        Me.Controls.Add(Me.txtDir);
        Me.Name = "DirectoryBox";
        Me.Size = New System.Drawing.Size(280, 22);
        (System.ComponentModel.ISupportInitialize)(Me.txtDir).EndInit();
        Me.ResumeLayout(false);

    }

#End Region

    Public Shadows Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    private string sFileFilter;
    private string sPath;


public enum OpenOrSaveEnum
    {
        Open;
        Save;
    }

    private OpenOrSaveEnum eOpenOrSave = OpenOrSaveEnum.Open;
    public OpenOrSaveEnum OpenOrSave { get; set; }
set(OpenOrSaveEnum)
            eOpenOrSave = value
        }
        {
            get
            {
            return eOpenOrSave;
        }
    }


    public string FileFilter { get; set; }
        {
            get
            {
            If sFileFilter = "" Then sFileFilter = "All Files (*.*)|*.*"
            return sFileFilter;
        }
set(ByVal String)
            sFileFilter = value
        }
    }

    public string Directory { get; set; }
        {
            get
            {
            if (BoxType = BoxTypeEnum.Directory)
            {
                return txtDir.Text;
            Else
                return "*** Incorrect BoxType ***";
            }
        }
set(ByVal Value As String)
            if (BoxType = BoxTypeEnum.Directory)
            {
                txtDir.Text = Value;
            }
        }
    }

    public string Filename { get; set; }
        {
            get
            {
            if (BoxType = BoxTypeEnum.File)
            {
                return sPath;
            Else
                return "*** Incorrect BoxType ***";
            }
        }
set(ByVal Value As String)
            if (BoxType = BoxTypeEnum.File)
            {
                if (Value IsNot null && Value.Contains("..."))
                {
                    while (Value.StartsWith("..."))
                    {
                        Value = sRight(Value, Value.Length - 3)
                    }
                    Value = Value
                Else
                    sPath = Value
                    txtDir.Text = Abbreviate(sPath);
                }
            }
        }
    }


    private Size MeasureString(string str, int maxWidth, Font font)
    {

        private Graphics g = CreateGraphics();
        private SizeF strRectSizeF = g.MeasureString(str, font, maxWidth);
        g.Dispose();

        return new Size(CInt(Math.Ceiling(strRectSizeF.Width)), CInt(Math.Ceiling(strRectSizeF.Height)));

    }


    private string Abbreviate(string sPath)
    {

        private string sAbb = sPath;
        private string[] sDirs = Split(sPath, "\");

        if (txtDir.Width > 0)
        {
            while (txtDir.Width < MeasureString(sAbb, 0, txtDir.Font).Width + 10  ' sAbb.Length * 5)
            {
                private string sOldAbb = sAbb;
                for (int i = sDirs.Length - 2; i <= 0; i += -1)
                {
                    if (sDirs(i) <> "...")
                    {
                        sDirs(i) = "...";
                        sAbb = ""
                        For Each sDir As String In sDirs
                            sAbb &= sDir + "\";
                        Next;
                        sAbb = sLeft(sAbb, sAbb.Length - 1)
                        while (sAbb.Contains("...\..."))
                        {
                            sAbb = sAbb.Replace("...\...", "...")
                        }
                        Exit For;
                    }
                Next;
                If sAbb = sOldAbb Then Return sAbb
            }
        }

        return sAbb;

    }


public enum BoxTypeEnum
    {
        File;
        Directory;
    }
    private BoxTypeEnum eBoxType;
    public BoxTypeEnum BoxType { get; set; }
        {
            get
            {
            return eBoxType;
        }
set(ByVal BoxTypeEnum)
            eBoxType = value
        }
    }


    private void txtDir_TextChanged(object sender, System.EventArgs e)
    {

        if (BoxType = BoxTypeEnum.Directory)
        {
            Directory = txtDir.Text
        Else
            Filename = txtDir.Text
        }

        if ((BoxType = BoxTypeEnum.Directory && IO.Directory.Exists(Directory)) || (BoxType = BoxTypeEnum.File && IO.File.Exists(Filename)))
        {
            txtDir.ForeColor = SystemColors.ControlText;
        Else
            txtDir.ForeColor = Color.DarkRed;
        }

        RaiseEvent TextChanged(sender, e);

    }

    public void OpenFileDialog()
    {
        btnSearch_Click(null, null);
    }
    private void btnSearch_Click(System.Object sender, System.EventArgs e)
    {

        try
        {
            if (BoxType = BoxTypeEnum.Directory)
            {
                With fbd;
                    .SelectedPath = Directory;
                    if (.ShowDialog = DialogResult.OK)
                    {
                        Directory = .SelectedPath
                    }
                }
            Else
                if (eOpenOrSave = OpenOrSaveEnum.Open)
                {
                    With ofd;
                        If IO.Directory.Exists(Filename) Then .InitialDirectory = Filename
                        try
                        {
                            .FileName = Filename;
                        Catch
                        }
                        .Filter = FileFilter;

                        if (.ShowDialog = DialogResult.OK)
                        {
                            Filename = .FileName
                        }
                    }
                Else
                    With sfd;
                        If IO.Directory.Exists(Filename) Then .InitialDirectory = Filename
                        try
                        {
                            .FileName = Filename;
                        Catch
                        }
                        .Filter = FileFilter;

                        if (.ShowDialog = DialogResult.OK)
                        {
                            Filename = .FileName
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrMsg("btnSearch error", ex);
        }

    }

    ' Hide the Text property
    <Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Text Property not in use", true)> _;
    Public Overrides Property Text() As String
        {
            get
            {
            return null;
        }
set(ByVal Value As String)
            Value = Nothing
        }
    }

    private void DirectoryBox_Resize(object sender, System.EventArgs e)
    {
        txtDir.Text = Abbreviate(sPath);
    }

}

}