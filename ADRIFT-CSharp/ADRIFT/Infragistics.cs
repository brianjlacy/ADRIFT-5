using Infragistics;
using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{

internal static class InfragisticsStuff
{

#if Generator
    public Infragistics.Win.UltraWinToolbars.ToolbarStyle EnumParseViewStyle(string sValue)
    {
        switch (sValue)
        {
            case "Default":
            case "Standard":
                {
                return UltraWinToolbars.ToolbarStyle.Default;
            case "Office2003":
                {
                return UltraWinToolbars.ToolbarStyle.Office2003;
            case "Office2007":
                {
                return UltraWinToolbars.ToolbarStyle.Office2007;
            case "VisualStudio2005":
                {
                return UltraWinToolbars.ToolbarStyle.VisualStudio2005;
            case "Office2010":
                {
                return UltraWinToolbars.ToolbarStyle.Office2010;
            case "Office2013":
                {
                return UltraWinToolbars.ToolbarStyle.Office2013;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }
#else
    public Infragistics.Win.UltraWinToolbars.ToolbarStyle EnumParseViewStyle(string sValue)
    {
        switch (sValue)
        {
            case "Default":
            case "Standard":
                {
                return UltraWinToolbars.ToolbarStyle.Default;
            case "Office2003":
                {
                return UltraWinToolbars.ToolbarStyle.Office2003;
            case "Office2007":
                {
                return UltraWinToolbars.ToolbarStyle.Office2007;
            case "Office2010":
                {
                return UltraWinToolbars.ToolbarStyle.Office2010;
            case "Office2013":
                {
                return UltraWinToolbars.ToolbarStyle.Office2013;
            case "VisualStudio2005":
                {
                return UltraWinToolbars.ToolbarStyle.VisualStudio2005;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }

#endif


    public void AddPrevious(Infragistics.Win.UltraWinToolbars.UltraToolbarsManager UTMMain, string sProject)
    {

        private string sFilename;

        for (int iPrevious = 1; iPrevious <= 20; iPrevious++)
        {
            sFilename = GetSetting("ADRIFT", sProject, "Recent_" & iPrevious, "")
            if (sFilename <> "")
            {
#if Generator
                private string sJustFile = sFilename;
                If sJustFile.Contains("\") Then sJustFile = sRight(sJustFile, sJustFile.Length - sJustFile.LastIndexOf("\") - 1)
                If iPrevious < 10 Then sJustFile = "&" + iPrevious + "   " + sJustFile Else sJustFile = "     " + sJustFile
                AddTool(UTMMain, "mnuRecentAdventures", sFilename, sJustFile, "_RECENT_", , sFilename);

                UTMMain.Ribbon.ApplicationMenu.ToolAreaRight.Tools.AddTool(sFilename);
#else
                fRunner.AddTool(UTMMain, "mnuRecentAdventures", sFilename, "&" + iPrevious + "  " + sFilename, "_RECENT_");
#endif

            }
        Next;

    }


    public void SetOptSet(Infragistics.Win.UltraWinEditors.UltraOptionSet opt, int iKey)
    {

        For Each vli As Infragistics.Win.ValueListItem In opt.Items
            if (CInt(vli.DataValue) = iKey)
            {
                opt.CheckedItem = vli;
                Exit Sub;
            }
        Next;

    }



    public Infragistics.Win.Office2007ColorScheme EnumParseColourScheme2007(string sValue)
    {
        switch (sValue)
        {
            case "Blue":
                {
                return Infragistics.Win.Office2007ColorScheme.Blue;
            case "Silver":
                {
                return Infragistics.Win.Office2007ColorScheme.Silver;
            case "Black":
                {
                return Infragistics.Win.Office2007ColorScheme.Black;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }
    public Infragistics.Win.Office2010ColorScheme EnumParseColourScheme2010(string sValue)
    {
        switch (sValue)
        {
            case "Blue":
                {
                return Infragistics.Win.Office2010ColorScheme.Blue;
            case "Silver":
                {
                return Infragistics.Win.Office2010ColorScheme.Silver;
            case "Black":
                {
                return Infragistics.Win.Office2010ColorScheme.Black;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }
    public Infragistics.Win.Office2013ColorScheme EnumParseColourScheme2013(string sValue)
    {
        switch (sValue)
        {
            case "Blue":
            case "LightGray":
                {
                return Infragistics.Win.Office2013ColorScheme.LightGray;
            case "Silver":
            case "White":
                {
                return Infragistics.Win.Office2013ColorScheme.White;
            case "Black":
            case "DarkGray":
                {
                return Infragistics.Win.Office2013ColorScheme.DarkGray;
            default:
                {
                throw New Exception("Value " & sValue & " not parsed!")
                return null;
        }
    }

}

}