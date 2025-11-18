using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{


public class AutoCompleteCombo
{
    Inherits UltraWinEditors.UltraComboEditor;


    public void New()
    {
        if (fGenerator?.AutoComplete)
        {
            Me.AutoCompleteMode = AutoCompleteMode.Suggest;
            Me.AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
            Me.ValueList.AutoSuggestHighlightAppearance.ForeColor = Color.Red;
        Else
            Me.AutoCompleteMode = AutoCompleteMode.None;
            Me.AutoSuggestFilterMode = AutoSuggestFilterMode.Default;
            Me.ValueList.AutoSuggestHighlightAppearance.ForeColor = Color.Red;
        }
        Me.TextRenderingMode = TextRenderingMode.GDI;
    }


    Public Overrides Property DropDownStyle As DropDownStyle
        {
            get
            {
            if (fGenerator?.AutoComplete)
            {
                return DropDownStyle.DropDown;
            Else
                return DropDownStyle.DropDownList;
            }
        }
set(DropDownStyle)
            MyBase.DropDownStyle = value;
        }
    }



    ' Drop down the list as soon as we click into the combobox
    private void AutoCompleteCombo_MouseDown(object sender, MouseEventArgs e)
    {

        private AutoCompleteCombo combo = CType(sender, AutoCompleteCombo);
        private UIElement element = combo.UIElement.LastElementEntered;
        private UIElement editorWithTextUIElement = element.GetAncestor(GetType(EditorWithTextUIElement));

        If editorWithTextUIElement != null Then combo.DropDown()
    }
}

}