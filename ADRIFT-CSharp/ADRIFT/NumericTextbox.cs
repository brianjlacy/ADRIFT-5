using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ADRIFT
{


public class NumericTextbox
{
    Inherits System.Windows.Forms.UserControl;

#Region " Windows Form Designer generated code "

    public void New()
    {
        Me.New(2);
    }
    public void New(int iMaxDecimalPlaces)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        Me.iMaxDecimalPlaces = iMaxDecimalPlaces;

        cDecimalSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.Chars(0)
        cGroupSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator.Chars(0)

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
    Private WithEvents txtNumeric As Infragistics.Win.UltraWinEditors.UltraTextEditor
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance;
        Me.txtNumeric = New Infragistics.Win.UltraWinEditors.UltraTextEditor;
        (System.ComponentModel.ISupportInitialize)(Me.txtNumeric).BeginInit();
        Me.SuspendLayout();
        '
        'txtNumeric
        '
        Appearance1.TextHAlign = Infragistics.Win.HAlign.Right;
        Me.txtNumeric.Appearance = Appearance1;
        Me.txtNumeric.AutoSize = false;
        Me.txtNumeric.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.txtNumeric.Location = New System.Drawing.Point(0, 0);
        Me.txtNumeric.Name = "txtNumeric";
        Me.txtNumeric.Size = New System.Drawing.Size(200, 22);
        Me.txtNumeric.TabIndex = 0;
        Me.txtNumeric.Text = "0";
        '
        'NumericTextbox
        '
        Me.Controls.Add(Me.txtNumeric);
        Me.Name = "NumericTextbox";
        Me.Size = New System.Drawing.Size(200, 22);
        (System.ComponentModel.ISupportInitialize)(Me.txtNumeric).EndInit();
        Me.ResumeLayout(false);

    }

#End Region


    Public Shadows Event GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Overrides ReadOnly Property Focused() As Boolean
        {
            get
            {
            return Me.txtNumeric.Focused;
        }
    }


    private void NumericTextbox_GotFocus(object sender, System.EventArgs e)
    {
        RaiseEvent GotFocus(Me, e)        ' This doesn't get called cos we do stuff below...;
    }

    private void NumericTextbox_LostFocus(object sender, System.EventArgs e)
    {
        If ! Me.Focused Then RaiseEvent LostFocus(Me, e)
    }


    private int iMaxDecimalPlaces;
    private int iMinDecimalPlaces;
    private double dNumericValue;
    public double MaxValue = Double.MaxValue;
    public double MinValue = Double.MinValue;
    private char cDecimalSeparator;
    private char cGroupSeparator;


    Public Event ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)


    public int MaxDecimalPlaces { get; set; }
        {
            get
            {
            return iMaxDecimalPlaces;
        }
set(ByVal Value As Integer)
            iMaxDecimalPlaces = Value
            FormatBox()
        }
    }


    public int MinDecimalPlaces { get; set; }
        {
            get
            {
            return iMinDecimalPlaces;
        }
set(ByVal Value As Integer)
            If Value < 0 Then Value = 0
            If Value > iMaxDecimalPlaces Then Throw New Exception("MinDecimalPlaces cannot be greater than MaxDecimalPlaces")
            iMinDecimalPlaces = Value
            FormatBox()
        }
    }

    private void txtNumeric_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        RaiseEvent KeyDown(Me, e) ' Re-raise event, so outside can deal with;
    }


    private void txtNumeric_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
    {

        switch (e.KeyChar)
        {
            case "0"c To "9"c:
                {
                if (txtNumeric.SelectionStart = txtNumeric.Text.Length && txtNumeric.Text.Length > iMaxDecimalPlaces && txtNumeric.Text.Substring(txtNumeric.Text.Length - (iMaxDecimalPlaces + 1), 1) = cDecimalSeparator)
                {
                    e.Handled = true ' Don't let user type if they're trying to add too many decimal places;
                }
                ' Allow
            case cDecimalSeparator:
                {
                if (iMaxDecimalPlaces > 0 && sInstr(txtNumeric.Text, cDecimalSeparator) = 0)
                {
                    ' Allow
                Else
                    e.Handled = true;
                }
            case "M"c:
            case "m"c:
                {
                Value *= 1000000;
                e.Handled = true;
            case "K"c:
            case "k"c:
                {
                Value *= 1000;
                e.Handled = true;
            case "+"c:
                {
                Value += 1000;
                e.Handled = true;
            case "-"c:
                {
                if (Me.MaxValue >= 0)
                {
                    e.Handled = true;
                Else
                    if (txtNumeric.Text = "0" || txtNumeric.Text = ("0" + cDecimalSeparator).PadRight(Me.iMaxDecimalPlaces + 2, "0"c))
                    {
                        ' Allow, as they're presumably starting to type a negative number
                    Else
                        Value = -Value
                        e.Handled = true;
                    }
                }
            case Chr(8):
                {
                ' Allow
            default:
                {
                e.Handled = true;
                Debug.WriteLine(e.KeyChar);
        }

    }


    public double Value { get; set; }
        {
            get
            {
            Value = dNumericValue
        }
set(ByVal dValue As Double)
            dNumericValue = Math.Max(Math.Min(dValue, MaxValue), MinValue)
            FormatBox()
        }
    }


    Public Shadows Sub Focus()
        Me.txtNumeric.Focus();
    }


    private void FormatBox()
    {

                private string sFormat = "#,##0" ' This is interpreted into correct format by Culture Info;
        private int iFromRight;
        If txtNumeric.IsInEditMode Then iFromRight = Len(txtNumeric.Text) - txtNumeric.SelectionStart

        if (iMaxDecimalPlaces > 0)
        {
            if (txtNumeric.IsInEditMode)
            {
                sFormat &= ".".PadRight(iMaxDecimalPlaces + 1, "#"c);
            Else
                sFormat &= ".".PadRight(iMinDecimalPlaces + 1, "0"c).PadRight(iMaxDecimalPlaces + 1, "#"c);
            }
        }

        private bool bSkipFormat = false;
        if (txtNumeric.IsInEditMode)
        {
            for (int i = 0; i <= iMaxDecimalPlaces - 1; i++)
            {
                if (txtNumeric.Text.Contains(cDecimalSeparator) && iFromRight = 0 && (sRight(txtNumeric.Text, 1) = "0" || sRight(txtNumeric.Text, 1) = cDecimalSeparator))
                {
                    'If sRight(txtNumeric.Text, i + 1) = ".".PadRight(i + 1, "0"c) AndAlso iFromRight = 0 Then
                    'If sRight(txtNumeric.Text, i + 1) = cDecimalSeparator.ToString.PadRight(i + 1, "0"c) AndAlso iFromRight = 0 Then
                    bSkipFormat = True
                    Exit For;
                }
            Next;
        }
        If ! bSkipFormat && txtNumeric.Text <> "-" Then  ' Format function removes trailing dot if no numerics after it
            txtNumeric.Text = Format(Value, sFormat);
        }

        If txtNumeric.IsInEditMode Then txtNumeric.SelectionStart = Len(txtNumeric.Text) - iFromRight

    }


    private void txtNumeric_AfterEnterEditMode(System.Object sender, System.EventArgs e)
    {

        txtNumeric.SelectionStart = 0;
        txtNumeric.SelectionLength = txtNumeric.Text.Length;
        RaiseEvent GotFocus(Me, null);

    }


    private void txtNumeric_TextChanged(System.Object sender, System.EventArgs e)
    {

        Value = Val(txtNumeric.Text.Replace(cGroupSeparator, "").Replace(cDecimalSeparator, "."))
        FormatBox()

        if (txtNumeric.IsInEditMode && (txtNumeric.Text = "0" || txtNumeric.Text = ("0" + cDecimalSeparator).PadRight(Me.iMaxDecimalPlaces + 2, "0"c)))
        {
            txtNumeric.SelectionStart = 0;
            txtNumeric.SelectionLength = txtNumeric.Text.Length;
        }

        RaiseEvent ValueChanged(sender, e);

    }

    private void NumericTextbox_Resize(object sender, System.EventArgs e)
    {
        txtNumeric.Size = Me.Size;
    }

    private void NumericTextbox_ForeColorChanged(object sender, System.EventArgs e)
    {
        txtNumeric.ForeColor = Me.ForeColor;
    }

    private void txtNumeric_AfterExitEditMode(object sender, System.EventArgs e)
    {
        FormatBox()
        If ! Me.Focused Then RaiseEvent LostFocus(Me, e)
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

    Public Overrides Property BackColor() As System.Drawing.Color
        {
            get
            {
            return Me.txtNumeric.Appearance.BackColor;
        }
set(ByVal System.Drawing.Color)
            Me.txtNumeric.Appearance.BackColor = value;
        }
    }

}
}