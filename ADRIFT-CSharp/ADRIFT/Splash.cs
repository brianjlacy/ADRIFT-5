using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Splash
{

    private void Splash_Load(object sender, System.EventArgs e)
    {

        'lblVersion5.Text = "v" & sLeft(Application.ProductVersion, Application.ProductVersion.Length - 2)

        try
        {
            'For Each ff As FontFamily In GetFont.Families
            '    If ff.IsStyleAvailable(FontStyle.Regular) Then
            '        For Each lbl As Label In New Label() {lblADRIFT, lblADRIFTFull, lblCopyright, lblGenerator, lblVersion5}
            '            lbl.Font = New Font(ff, lbl.Font.Size, FontStyle.Regular)
            '        Next
            '    End If
            'Next
            'Dim sFont As String = Application.StartupPath & IO.Path.DirectorySeparatorChar & "LYDIAN.TTF"
            'If IO.File.Exists(sFont) Then
            '    Dim pfc As New System.Drawing.Text.PrivateFontCollection
            '    pfc.AddFontFile(sFont)
            '    For Each ff As FontFamily In pfc.Families
            '        If ff.IsStyleAvailable(FontStyle.Regular) Then
            '            For Each lbl As Label In New Label() {lblADRIFT, lblADRIFTFull, lblCopyright, lblGenerator, lblVersion5}
            '                lbl.Font = New Font(ff, lbl.Font.Size, FontStyle.Regular)
            '            Next
            '        End If
            '    Next
            'End If

            'Dim pfc As New System.Drawing.Text.PrivateFontCollection
            'Dim bytLydian As Byte() = My.Resources.Resources.Lydian
            'gch = Runtime.InteropServices.GCHandle.Alloc(bytLydian, Runtime.InteropServices.GCHandleType.Pinned)
            'pfc.AddMemoryFont(gch.AddrOfPinnedObject, bytLydian.Length)


            'Dim pfc As System.Drawing.Text.PrivateFontCollection = GetFontCollection()
            'For Each ff As FontFamily In pfc.Families
            '    If ff.IsStyleAvailable(FontStyle.Regular) Then
            '        For Each lbl As Label In New Label() {lblADRIFT, lblADRIFTFull, lblCopyright, lblGenerator, lblVersion5}
            '            lbl.UseCompatibleTextRendering = True
            '            lbl.Font = New Font(ff, lbl.Font.Size, FontStyle.Regular)
            '        Next
            '    End If
            'Next

        Catch
        }

        '#If Generator Then
        '        lblGenerator.Text = "Developer"
        '#Else
        '        lblGenerator.Text = "Runner"
        '#End If

    }


    private void imgAdrift_Click(object sender, System.EventArgs e)
    {
        Me.Close();
    }



    private void imgAdrift_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {

        If pfc == null Then pfc = GetFontCollection()
        private Font f;

        For Each ff As FontFamily In pfc.Families
            if (ff.IsStyleAvailable(FontStyle.Regular))
            {
                f = New Font(ff, 60, FontStyle.Regular)
#if Mono
                e.Graphics.DrawString("ADRIFT", f, New SolidBrush(Color.White), New Point(7, 0));
#else
                e.Graphics.DrawString("ADRIFT", f, New SolidBrush(Color.White), New Point(-3, 0));
#endif
                f = New Font(ff, 18, FontStyle.Regular)
#if Generator
                e.Graphics.DrawString("Developer", f, New SolidBrush(Color.White), New Point(8, 96));
#else
                e.Graphics.DrawString("Runner", f, New SolidBrush(Color.White), New Point(8, 96));
#endif
                f = New Font(ff, 11.25, FontStyle.Regular)
                e.Graphics.DrawString("© Campbell Wild 1998-2022", f, New SolidBrush(Color.White), New Point(369, 393));
                f = New Font(ff, 9.75, FontStyle.Regular)
                e.Graphics.DrawString("Adventure Development + Runner - Interactive Fiction Toolkit", f, New SolidBrush(Color.White), New Point(10, 81));
#if Mono
                e.Graphics.DrawString("v" + Application.ProductVersion, f, New SolidBrush(Color.White), New Point(257, 15));
#else
                e.Graphics.DrawString("v" + sLeft(Application.ProductVersion, Application.ProductVersion.Length - 2), f, New SolidBrush(Color.White), New Point(257, 15));
#endif

            }
        Next;
    }
}
}