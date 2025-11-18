using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmVariable
{
    Inherits System.Windows.Forms.Form;

#Region " Windows Form Designer generated code "

    public bool bKeepOpen = false;

    public void New(ref var As clsVariable, bool bShow)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmVariable)
            {
                if (CType(w, frmVariable).cVariable.Key = var.Key && var.Key IsNot null)
                {
                    w.BringToFront();
                    w.Focus();
                    Exit Sub;
                }
            }
        Next;

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        Application.EnableVisualStyles();
        LoadForm(var, bShow);
        bKeepOpen = Not bShow

    }

    'Form overrides dispose to clean up the component list.
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
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents lblName As System.Windows.Forms.Label;
    Friend WithEvents txtName As System.Windows.Forms.TextBox;
    Friend WithEvents optInteger As System.Windows.Forms.RadioButton;
    Friend WithEvents grpType As Infragistics.Win.Misc.UltraGroupBox;
    Friend WithEvents optText As System.Windows.Forms.RadioButton;
    Friend WithEvents txtNumericValue As System.Windows.Forms.TextBox;
    Friend WithEvents lblValue As System.Windows.Forms.Label;
    Friend WithEvents txtLength As System.Windows.Forms.TextBox;
    Friend WithEvents lblLength As System.Windows.Forms.Label;
    Friend WithEvents chkArray As System.Windows.Forms.CheckBox;
    Friend WithEvents txtTextValue As System.Windows.Forms.TextBox;
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType(frmVariable));
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider();
        Me.txtName = New System.Windows.Forms.TextBox();
        Me.grpType = New Infragistics.Win.Misc.UltraGroupBox();
        Me.lblLength = New System.Windows.Forms.Label();
        Me.txtLength = New System.Windows.Forms.TextBox();
        Me.optText = New System.Windows.Forms.RadioButton();
        Me.optInteger = New System.Windows.Forms.RadioButton();
        Me.chkArray = New System.Windows.Forms.CheckBox();
        Me.txtNumericValue = New System.Windows.Forms.TextBox();
        Me.txtTextValue = New System.Windows.Forms.TextBox();
        Me.lblName = New System.Windows.Forms.Label();
        Me.lblValue = New System.Windows.Forms.Label();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).BeginInit();
        Me.grpType.SuspendLayout();
        Me.SuspendLayout();
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 325);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(315, 48);
        Me.UltraStatusBar1.TabIndex = 5;
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(211, 335);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 14;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(115, 335);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 13;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(19, 335);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 12;
        Me.btnOK.Text = "OK";
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.HelpProvider1.SetHelpString(Me.txtName, "This is the name you want to refer to your variable as.  If you call it ""HitCount" + _;
        """, you would refer to it as %HitCount% within text boxes.");
        Me.txtName.Location = New System.Drawing.Point(62, 17);
        Me.txtName.Name = "txtName";
        Me.HelpProvider1.SetShowHelp(Me.txtName, true);
        Me.txtName.Size = New System.Drawing.Size(237, 20);
        Me.txtName.TabIndex = 16;
        '
        'grpType
        '
        Me.grpType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.grpType.Controls.Add(Me.lblLength);
        Me.grpType.Controls.Add(Me.txtLength);
        Me.grpType.Controls.Add(Me.optText);
        Me.grpType.Controls.Add(Me.optInteger);
        Me.grpType.Controls.Add(Me.chkArray);
        Me.HelpProvider1.SetHelpString(Me.grpType, "Select whether you want your variable to store numbers, or text");
        Me.grpType.Location = New System.Drawing.Point(19, 51);
        Me.grpType.Name = "grpType";
        Me.HelpProvider1.SetShowHelp(Me.grpType, true);
        Me.grpType.Size = New System.Drawing.Size(280, 89);
        Me.grpType.TabIndex = 18;
        Me.grpType.Text = "Variable Type";
        Me.grpType.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
        '
        'lblLength
        '
        Me.lblLength.AutoSize = true;
        Me.lblLength.BackColor = System.Drawing.Color.Transparent;
        Me.lblLength.Enabled = false;
        Me.lblLength.Location = New System.Drawing.Point(127, 57);
        Me.lblLength.Name = "lblLength";
        Me.lblLength.Size = New System.Drawing.Size(63, 13);
        Me.lblLength.TabIndex = 16;
        Me.lblLength.Text = "... of length:";
        '
        'txtLength
        '
        Me.txtLength.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtLength.Enabled = false;
        Me.HelpProvider1.SetHelpString(Me.txtLength, "This is the length of the array (the number of unique values stored in the array)" + _;
        "");
        Me.txtLength.Location = New System.Drawing.Point(192, 54);
        Me.txtLength.Name = "txtLength";
        Me.HelpProvider1.SetShowHelp(Me.txtLength, true);
        Me.txtLength.Size = New System.Drawing.Size(56, 20);
        Me.txtLength.TabIndex = 22;
        Me.txtLength.Text = "0";
        Me.txtLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        '
        'optText
        '
        Me.optText.AutoSize = true;
        Me.optText.BackColor = System.Drawing.Color.Transparent;
        Me.optText.Location = New System.Drawing.Point(20, 53);
        Me.optText.Name = "optText";
        Me.optText.Size = New System.Drawing.Size(46, 17);
        Me.optText.TabIndex = 18;
        Me.optText.Text = "&Text";
        Me.optText.UseVisualStyleBackColor = false;
        '
        'optInteger
        '
        Me.optInteger.AutoSize = true;
        Me.optInteger.BackColor = System.Drawing.Color.Transparent;
        Me.optInteger.Checked = true;
        Me.optInteger.Location = New System.Drawing.Point(20, 30);
        Me.optInteger.Name = "optInteger";
        Me.optInteger.Size = New System.Drawing.Size(62, 17);
        Me.optInteger.TabIndex = 17;
        Me.optInteger.TabStop = true;
        Me.optInteger.Text = "&Number";
        Me.optInteger.UseVisualStyleBackColor = false;
        '
        'chkArray
        '
        Me.chkArray.AutoSize = true;
        Me.chkArray.BackColor = System.Drawing.Color.Transparent;
        Me.HelpProvider1.SetHelpString(Me.chkArray, "Check this checkbox to define the variable as an array (a list of variables, each" + _;
        " with their own value)");
        Me.chkArray.Location = New System.Drawing.Point(115, 31);
        Me.chkArray.Name = "chkArray";
        Me.HelpProvider1.SetShowHelp(Me.chkArray, true);
        Me.chkArray.Size = New System.Drawing.Size(116, 17);
        Me.chkArray.TabIndex = 0;
        Me.chkArray.Text = "Variable is an &Array";
        Me.chkArray.UseVisualStyleBackColor = false;
        '
        'txtNumericValue
        '
        Me.txtNumericValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtNumericValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.HelpProvider1.SetHelpString(Me.txtNumericValue, resources.GetString("txtNumericValue.HelpString"));
        Me.txtNumericValue.Location = New System.Drawing.Point(91, 149);
        Me.txtNumericValue.Name = "txtNumericValue";
        Me.HelpProvider1.SetShowHelp(Me.txtNumericValue, true);
        Me.txtNumericValue.Size = New System.Drawing.Size(208, 22);
        Me.txtNumericValue.TabIndex = 20;
        Me.txtNumericValue.Text = "0";
        Me.txtNumericValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        '
        'txtTextValue
        '
        Me.txtTextValue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtTextValue.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.HelpProvider1.SetHelpString(Me.txtTextValue, resources.GetString("txtTextValue.HelpString"));
        Me.txtTextValue.Location = New System.Drawing.Point(21, 173);
        Me.txtTextValue.Multiline = true;
        Me.txtTextValue.Name = "txtTextValue";
        Me.txtTextValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        Me.HelpProvider1.SetShowHelp(Me.txtTextValue, true);
        Me.txtTextValue.Size = New System.Drawing.Size(278, 142);
        Me.txtTextValue.TabIndex = 22;
        Me.txtTextValue.Text = "Blah blah...";
        '
        'lblName
        '
        Me.lblName.AutoSize = true;
        Me.lblName.Location = New System.Drawing.Point(18, 20);
        Me.lblName.Name = "lblName";
        Me.lblName.Size = New System.Drawing.Size(38, 13);
        Me.lblName.TabIndex = 15;
        Me.lblName.Text = "&Name:";
        '
        'lblValue
        '
        Me.lblValue.Location = New System.Drawing.Point(18, 153);
        Me.lblValue.Name = "lblValue";
        Me.lblValue.Size = New System.Drawing.Size(80, 17);
        Me.lblValue.TabIndex = 19;
        Me.lblValue.Text = "Initial &Value:";
        '
        'frmVariable
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(315, 373);
        Me.Controls.Add(Me.txtTextValue);
        Me.Controls.Add(Me.txtNumericValue);
        Me.Controls.Add(Me.lblValue);
        Me.Controls.Add(Me.grpType);
        Me.Controls.Add(Me.txtName);
        Me.Controls.Add(Me.lblName);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.Name = "frmVariable";
        Me.ShowInTaskbar = false;
        Me.Text = "Variable -";
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.grpType).EndInit();
        Me.grpType.ResumeLayout(false);
        Me.grpType.PerformLayout();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }

#End Region


    private clsVariable cVariable;
    private bool bChanged;
    private int iSelectedIndex = -1;
    private bool bUpdating = false;


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


    private void LoadForm(ref cVariable As clsVariable, bool bShow)
    {

        Me.cVariable = cVariable;

        private int iHeight;

        With cVariable;
            Text = "Variable - " & .Name
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= "  [" + .Key + "]"
            If .Name = "" Then Text = "New Variable"

            txtName.Text = .Name;

            if (.Type = clsVariable.VariableTypeEnum.Numeric)
            {
                optInteger.Checked = true;
                if (.Length > 1 && .StringValue.Length > 0)
                {
                    txtTextValue.Text = .StringValue.Replace(", ", vbCrLf);
                Else
                    txtNumericValue.Text = .IntValue.ToString;
                    txtTextValue.Text = "";
                }
                iHeight = 370
            Else
                optText.Checked = true;
                txtTextValue.Text = .StringValue;
                iHeight = 435
            }

            if (.Length > 1)
            {
                txtLength.Text = .Length.ToString;
                txtLength.Enabled = true;
                lblLength.Enabled = true;
                bUpdating = True
                chkArray.Checked = true;
                bUpdating = False
            Else
                txtLength.Text = "1";
                txtLength.Enabled = false;
                lblLength.Enabled = false;
                chkArray.Checked = false;
            }

            Changed = False

            if (.Key = "Score" || .Key = "MaxScore")
            {
                grpType.Enabled = false;
            }
        }

        If bShow Then Me.Show()
        Me.Height = iHeight;

        OpenForms.Add(Me);

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        if (ApplyVariable())
        {
            CloseVariable(Me);
        }
    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        if (ApplyVariable())
        {
            Changed = False
        }
    }

    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {
        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (result = Windows.Forms.DialogResult.Yes)
            {
                If ! ApplyVariable() Then Exit Sub
            }
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseVariable(Me);
    }


    private bool ApplyVariable()
    {

        ' remember to strip off the unselected properties

        With cVariable;

            .Name = txtName.Text.Replace(" ", "");
            If .Name = "" Then .Name = "UnnamedVariable"
            .LastUpdated = Now;
            .IsLibrary = false;

            if (chkArray.Checked)
            {
                .Length = CInt(Val(txtLength.Text));
            Else
                .Length = 1;
            }

            if (Me.optInteger.Checked)
            {
                .Type = clsVariable.VariableTypeEnum.Numeric;
                if (chkArray.Checked)
                {
                    if (Int(Val(txtLength.Text)) < 2)
                    {
                        ErrMsg("Arrays must be at least 2 in length");
                        return false;
                    }
                    'If sInstr(txtNumericValue.Text, ",") > 0 Then
                    '    ' Each array entry set to it's own value separated by a comma
                    '    If InstrCount(txtNumericValue.Text, ",") = CInt(Val(txtLength.Text)) - 1 Then
                    '        .StringValue = txtNumericValue.Text
                    '        .IntValue = 0
                    '    Else
                    '        ErrMsg("You have not defined a value for each array entry")
                    '        Return False
                    '    End If
                    'Else
                    '    ' All array entries set to same value
                    '    .IntValue = CInt(Val(txtNumericValue.Text))
                    '    .StringValue = ""
                    'End If
                    .StringValue = txtTextValue.Text.Replace(vbCrLf, ", ");
                    while (.StringValue.EndsWith(", "))
                    {
                        .StringValue = sLeft(.StringValue, .StringValue.Length - 2);
                    }
                    while (.StringValue.Contains(", , "))
                    {
                        .StringValue = .StringValue.Replace(", , ", ", ");
                    }
                Else
                    .IntValue = CInt(Val(txtNumericValue.Text));
                    .StringValue = "";
                }
            Else
                .Type = clsVariable.VariableTypeEnum.Text;
                .StringValue = txtTextValue.Text;
            }

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Variable");
                Adventure.htblVariables.Add(cVariable, .Key);
            }

            UpdateListItem(.Key, .Name);

            If .Key = "MaxScore" Then Adventure.MaxScore = .IntValue

        }

        Adventure.Changed = true;
        return true;

    }


    private void frmVariable_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }



    private void frmVariable_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgVariable16.GetHicon);
        GetFormPosition(Me);
    }


    private void optInteger_Click(object sender, System.EventArgs e)
    {

        if (optInteger.Checked)
        {
            if (Not chkArray.Checked)
            {
                Height = 271
                MinimumSize = New Size(331, 271)
                MaximumSize = New Size(400, 271)
                txtNumericValue.Text = SafeInt(txtTextValue.Text).ToString;
                txtNumericValue.Visible = true;
                txtTextValue.Visible = false;
            Else
                If Height < 435 Then Height = 435
                MinimumSize = New Size(331, 400)
                MaximumSize = New Size(800, 800)
                private string sNewText = "";
                For Each sVal As String In txtTextValue.Text.Split(Chr(10))
                    sNewText &= SafeInt(sVal) + vbCrLf;
                Next;
                txtTextValue.Text = sLeft(sNewText, sNewText.Length - 1);
                txtTextValue.TextAlign = HorizontalAlignment.Right;
                txtNumericValue.Visible = false;
                txtTextValue.Visible = true;
            }
        }

    }


    private void optText_Click(object sender, System.EventArgs e)
    {

        if (optText.Checked)
        {
            If Height < 435 Then Height = 435
            MinimumSize = New Size(331, 400)
            MaximumSize = New Size(800, 800)
            txtTextValue.TextAlign = HorizontalAlignment.Left;
            txtNumericValue.Visible = false;
            txtTextValue.Visible = true;
        }

    }


    private void txtLength_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {

        if (sender Is txtLength || optInteger.Checked)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Enter:
                case Keys.Delete:
                case Keys.Back:
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    {
                    ' Ok
                default:
                    {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
            }
        }

    }



    private void txtLength_Leave(object sender, System.EventArgs e)
    {

        private int iCurrentLength = CharacterCount(txtTextValue.Text, Chr(10)) + 1;
        if (iCurrentLength > SafeInt(txtLength.Text))
        {
            private int iOffset = 0;
            for (int i = 0; i <= SafeInt(txtLength.Text) - 1; i++)
            {
                iOffset = txtTextValue.Text.IndexOf(vbCrLf, iOffset + 1)
            Next;
            txtTextValue.Text = sLeft(txtTextValue.Text, iOffset);
        }

    }


    private void txtLength_TextChanged(System.Object sender, System.EventArgs e)
    {

        If bUpdating Then Exit Sub

        private int iCurrentLength = CharacterCount(txtTextValue.Text, Chr(10)) + 1;
        if (iCurrentLength < SafeInt(txtLength.Text))
        {
            private string sInitialValue = "";
            if (optInteger.Checked && txtTextValue.Text = "")
            {
                sInitialValue = "0"
            Else
                sInitialValue = txtTextValue.Text
                if (sInitialValue.Contains(vbCrLf))
                {
                    sInitialValue = sLeft(sInitialValue, sInitialValue.IndexOf(vbCrLf))
                }
            }
            for (int i = iCurrentLength; i <= SafeInt(txtLength.Text) - 1; i++)
            {
                txtTextValue.Text &= vbCrLf + sInitialValue;
            Next;
        }

    }


    private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        If e.KeyCode = Keys.Space Then e.SuppressKeyPress = true
    }


    private void Stuff_TextChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
    }


    private void chkArray_CheckedChanged(System.Object sender, System.EventArgs e)
    {

        if (chkArray.Checked)
        {
            lblLength.Enabled = true;
            txtLength.Enabled = true;
            lblValue.Text = "Initial &Values:";
            txtNumericValue.Visible = false;
            txtTextValue.Visible = true;
            private string sInitialValue;
            if (optInteger.Checked)
            {
                txtTextValue.TextAlign = HorizontalAlignment.Right;
                sInitialValue = txtNumericValue.Text
            Else
                txtTextValue.TextAlign = HorizontalAlignment.Left;
                sInitialValue = txtTextValue.Text.Replace(vbCrLf, "<br>")
            }
            If Height < 435 Then Height = 435
            MinimumSize = New Size(331, 400)
            MaximumSize = New Size(800, 800)
            if (Not bUpdating)
            {
                if (CharacterCount(txtTextValue.Text, Chr(10)) - 1 <> SafeInt(txtLength.Text))
                {
                    txtTextValue.Text = "";
                    for (int i = 0; i <= SafeInt(txtLength.Text) - 2; i++)
                    {
                        txtTextValue.Text &= sInitialValue + vbCrLf;
                    Next;
                    txtTextValue.Text &= sInitialValue;
                }
            }
        Else
            lblLength.Enabled = false;
            txtLength.Enabled = false;
            lblValue.Text = "Initial &Value:";
            txtTextValue.TextAlign = HorizontalAlignment.Left;
            private string sInitialValue = txtTextValue.Text;
            if (sInitialValue.Contains(vbCrLf))
            {
                sInitialValue = sLeft(sInitialValue, sInitialValue.IndexOf(vbCrLf))
            }
            txtTextValue.Text = sInitialValue.Replace("<br>", vbCrLf);
            txtNumericValue.Text = SafeInt(sInitialValue).ToString;
            if (optInteger.Checked)
            {
                Height = 271
                MinimumSize = New Size(331, 271)
                MaximumSize = New Size(400, 271)
                txtNumericValue.Visible = true;
                txtTextValue.Visible = false;
            }
        }
        Changed = True

    }

    private void frmVariable_Shown(object sender, System.EventArgs e)
    {
        if (txtName.Text = "")
        {
            txtName.Focus();
        Else
            if (txtNumericValue.Visible)
            {
                txtNumericValue.Focus();
            ElseIf txtTextValue.Visible Then
                txtTextValue.SelectionStart = txtTextValue.TextLength;
                txtTextValue.Focus();
            }
        }
    }


    private void txtTextValue_GotFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = null;
    }

    private void txtTextValue_LostFocus(object sender, System.EventArgs e)
    {
        Me.AcceptButton = btnOK;
    }

    private void txtTextValue_TextChanged(object sender, System.EventArgs e)
    {

        if (chkArray.Checked)
        {
            private int iCount = 0;
            For Each sVal As String In txtTextValue.Text.Split(Chr(10))
                If optText.Checked || sVal.Replace(Chr(13), "") <> "" Then iCount += 1
            Next;
            txtLength.Text = iCount.ToString ' (CharacterCount(txtTextValue.Text, Chr(10)) + 1).ToString;
            'If optInteger.Checked AndAlso txtTextValue.Text.EndsWith(Chr(10)) Then txtLength.Text = (CharacterCount(txtTextValue.Text, Chr(10))).ToString
        }

    }

    private void frmVariable_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "Variables");
    }

}

}