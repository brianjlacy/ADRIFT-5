using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


internal static class RunnerInfragistics
{

    public Infragistics.Win.UltraWinToolbars.ToolbarStyle eStyle;
    public Infragistics.Win.Office2007ColorScheme eColour2007;
    public Infragistics.Win.Office2010ColorScheme eColour2010;
    public Infragistics.Win.Office2013ColorScheme eColour2013;


    public void SetWindowStyle(Form frmTarget, UltraWinToolbars.UltraToolbarsManager utm = null, UltraWinDock.UltraDockManager udm = null)
    {

        frmTarget.SuspendLayout();


        switch (eStyle)
        {
            case ToolbarStyle.Default:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2000
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.Windows
            case ToolbarStyle.Office2003:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2003
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2003
            case ToolbarStyle.Office2007:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2007
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
            case ToolbarStyle.Office2010:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2010
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
            case ToolbarStyle.Office2013:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.Office2013
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.Office2007
            case ToolbarStyle.VisualStudio2005:
                {
                If ! utm == null Then utm.Style = UltraWinToolbars.ToolbarStyle.VisualStudio2005
                If ! udm == null Then udm.WindowStyle = UltraWinDock.WindowStyle.VisualStudio2005
            default:
                {
        }

        For Each c As Control In frmTarget.Controls
            SetControlStyle(c);
        Next;

        frmTarget.ResumeLayout();

    }


    public void SetControlStyle(Control c)
    {

        'If TypeOf c Is UltraControlBase Then
        '    CType(c, UltraControlBase).StyleSetName = "c:\program files\Infragistics\NetAdvantage 2006 Volume 3 CLR 2.0\AppStylist\Styles\Office2007Blue.isl"
        'End If

        private EmbeddableElementDisplayStyle dsElement = EmbeddableElementDisplayStyle.Default;
        private UltraWinStatusBar.ViewStyle vsStatusBar = UltraWinStatusBar.ViewStyle.Default;
        private Misc.GroupBoxViewStyle vsGroupBox = Misc.GroupBoxViewStyle.Default;
        private UltraWinTabControl.ViewStyle vsTabControl = UltraWinTabControl.ViewStyle.Default;
        private GlyphStyle gsCheck = GlyphStyle.Default;


        switch (eStyle)
        {
            case ToolbarStyle.Default:
                {
                dsElement = EmbeddableElementDisplayStyle.Standard
                vsStatusBar = UltraWinStatusBar.ViewStyle.Standard
                vsGroupBox = Misc.GroupBoxViewStyle.Office2000
                vsTabControl = UltraWinTabControl.ViewStyle.Default
                gsCheck = GlyphStyle.Default
            case ToolbarStyle.Office2003:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2003
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2003
                vsGroupBox = Misc.GroupBoxViewStyle.Office2003
                vsTabControl = UltraWinTabControl.ViewStyle.Office2003
            case ToolbarStyle.Office2007:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2007
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Office2007
                gsCheck = GlyphStyle.Office2007
                vsTabControl = UltraWinTabControl.ViewStyle.Office2007
            case ToolbarStyle.Office2010:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2010
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Office2007
                gsCheck = GlyphStyle.Office2010
                vsTabControl = UltraWinTabControl.ViewStyle.Office2007
            case ToolbarStyle.Office2013:
                {
                dsElement = EmbeddableElementDisplayStyle.Office2013
                vsStatusBar = UltraWinStatusBar.ViewStyle.Office2007
                vsGroupBox = Misc.GroupBoxViewStyle.Office2007
                gsCheck = GlyphStyle.Office2013
                vsTabControl = UltraWinTabControl.ViewStyle.Office2007
            case ToolbarStyle.VisualStudio2005:
                {
                dsElement = EmbeddableElementDisplayStyle.VisualStudio2005
                vsStatusBar = UltraWinStatusBar.ViewStyle.VisualStudio2005
                vsGroupBox = Misc.GroupBoxViewStyle.VisualStudio2005
                vsTabControl = UltraWinTabControl.ViewStyle.VisualStudio2005
        }


        if (TypeOf c Is UltraWinStatusBar.UltraStatusBar)
        {
            (UltraWinStatusBar.UltraStatusBar)(c).ViewStyle = vsStatusBar;
            'ElseIf TypeOf c Is Misc.UltraButton Then
            'CType(c, Misc.UltraButton).HotTracking = rSession.bHotTracking
        ElseIf TypeOf c == Misc.UltraGroupBox Then
            (Misc.UltraGroupBox)(c).ViewStyle = vsGroupBox;
        ElseIf TypeOf c == UltraWinProgressBar.UltraProgressBar Then
            (UltraWinProgressBar.UltraProgressBar)(c).Style = UltraWinProgressBar.ProgressBarStyle.Default;
        ElseIf TypeOf c == UltraWinTabControl.UltraTabControl Then
            (UltraWinTabControl.UltraTabControl)(c).ViewStyle = vsTabControl;
        Else
            Debug.WriteLine(c.GetType.ToString);
        }

        if (Not c.Controls Is null)
        {
            For Each cChild As Control In c.Controls
                SetControlStyle(cChild);
            Next;
        }

    }


    public void AddTool(ref UTMTarget As UltraToolbarsManager, string sTargetTool, string sKey, string sCaption, string sTag = "", bool bFirstInGroup = false)
    {

        try
        {
            private New ButtonTool(sKey) newTool;
            if (Not UTMTarget.Tools.Exists(sKey))
            {
                newTool.SharedProps.Caption = sCaption;
                newTool.SharedProps.Tag = sTag;
                UTMTarget.Tools.Add(newTool);

                private ButtonTool newinstance = CType(CType(UTMTarget.Tools(sTargetTool), PopupMenuTool).Tools.AddTool(sKey), ButtonTool);
                newinstance.InstanceProps.IsFirstInGroup = bFirstInGroup;
            }
        }
        catch (Exception ex)
        {
            ' Probably a duplicate key
            ErrMsg("AddTool error", ex);
        }

    }

}

}