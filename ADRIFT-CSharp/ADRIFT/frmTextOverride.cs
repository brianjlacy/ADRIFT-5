using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmTextOverride
{
    Inherits System.Windows.Forms.Form;

    private clsALR cALR;
    Friend WithEvents txtNew As ADRIFT.GenTextbox;
    Friend WithEvents txtOld As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    private bool bChanged;


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

#Region " Windows Form Designer generated code "

    public void New(clsALR ALR)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmTextOverride)
            {
                if (CType(w, frmTextOverride).cALR.Key = ALR.Key && ALR.Key IsNot null)
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
        LoadForm(ALR);

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
    Friend WithEvents btnApply As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton;
    Friend WithEvents UltraStatusBar1 As Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
    Friend WithEvents lblNew As Infragistics.Win.Misc.UltraLabel;
    Private WithEvents lblOriginal As Infragistics.Win.Misc.UltraLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        Me.btnApply = New Infragistics.Win.Misc.UltraButton();
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton();
        Me.btnOK = New Infragistics.Win.Misc.UltraButton();
        Me.UltraStatusBar1 = New Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
        Me.lblNew = New Infragistics.Win.Misc.UltraLabel();
        Me.lblOriginal = New Infragistics.Win.Misc.UltraLabel();
        Me.txtOld = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.txtNew = New ADRIFT.GenTextbox();
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).BeginInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtOld).BeginInit();
        Me.SuspendLayout();
        '
        'btnApply
        '
        Me.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnApply.Enabled = false;
        Me.btnApply.Location = New System.Drawing.Point(416, 324);
        Me.btnApply.Name = "btnApply";
        Me.btnApply.Size = New System.Drawing.Size(88, 32);
        Me.btnApply.TabIndex = 15;
        Me.btnApply.Text = "Apply";
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        Me.btnCancel.Location = New System.Drawing.Point(320, 324);
        Me.btnCancel.Name = "btnCancel";
        Me.btnCancel.Size = New System.Drawing.Size(88, 32);
        Me.btnCancel.TabIndex = 14;
        Me.btnCancel.Text = "Cancel";
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
        Me.btnOK.Location = New System.Drawing.Point(224, 324);
        Me.btnOK.Name = "btnOK";
        Me.btnOK.Size = New System.Drawing.Size(88, 32);
        Me.btnOK.TabIndex = 13;
        Me.btnOK.Text = "OK";
        '
        'UltraStatusBar1
        '
        Me.UltraStatusBar1.Location = New System.Drawing.Point(0, 318);
        Me.UltraStatusBar1.Name = "UltraStatusBar1";
        Me.UltraStatusBar1.Size = New System.Drawing.Size(520, 48);
        Me.UltraStatusBar1.TabIndex = 12;
        '
        'lblNew
        '
        Me.lblNew.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblNew.Location = New System.Drawing.Point(12, 152);
        Me.lblNew.Name = "lblNew";
        Me.lblNew.Size = New System.Drawing.Size(160, 16);
        Me.lblNew.TabIndex = 17;
        Me.lblNew.Text = "Replacement text:";
        '
        'lblOriginal
        '
        Me.lblOriginal.BackColorInternal = System.Drawing.Color.Transparent;
        Me.lblOriginal.Location = New System.Drawing.Point(12, 8);
        Me.lblOriginal.Name = "lblOriginal";
        Me.lblOriginal.Size = New System.Drawing.Size(91, 16);
        Me.lblOriginal.TabIndex = 16;
        Me.lblOriginal.Text = "Original text:";
        '
        'txtOld
        '
        Me.txtOld.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtOld.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtOld.Location = New System.Drawing.Point(12, 30);
        Me.txtOld.Multiline = true;
        Me.txtOld.Name = "txtOld";
        Me.txtOld.Size = New System.Drawing.Size(496, 104);
        Me.txtOld.TabIndex = 19;
        '
        'txtNew
        '
        Me.txtNew.AllowAlternateDescriptions = true;
        Me.txtNew.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) _;
            | System.Windows.Forms.AnchorStyles.Left) _;
            | System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles);
        Me.txtNew.BackColor = System.Drawing.Color.Transparent;
        Me.txtNew.FirstTabHasRestrictions = false;
        Me.txtNew.Location = New System.Drawing.Point(12, 174);
        Me.txtNew.Name = "txtNew";
        Me.txtNew.sCommand = null;
        Me.txtNew.Size = New System.Drawing.Size(496, 130);
        Me.txtNew.TabIndex = 18;
        '
        'frmTextOverride
        '
        Me.AcceptButton = Me.btnOK;
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.CancelButton = Me.btnCancel;
        Me.ClientSize = New System.Drawing.Size(520, 366);
        Me.Controls.Add(Me.txtOld);
        Me.Controls.Add(Me.txtNew);
        Me.Controls.Add(Me.lblNew);
        Me.Controls.Add(Me.lblOriginal);
        Me.Controls.Add(Me.btnApply);
        Me.Controls.Add(Me.btnCancel);
        Me.Controls.Add(Me.btnOK);
        Me.Controls.Add(Me.UltraStatusBar1);
        Me.HelpButton = true;
        Me.MaximizeBox = false;
        Me.MinimizeBox = false;
        Me.MinimumSize = New System.Drawing.Size(536, 402);
        Me.Name = "frmTextOverride";
        Me.Text = "Text Override -";
        (System.ComponentModel.ISupportInitialize)(Me.UltraStatusBar1).EndInit();
        (System.ComponentModel.ISupportInitialize)(Me.txtOld).EndInit();
        Me.ResumeLayout(false);
        Me.PerformLayout();

    }

#End Region

    private void LoadForm(ref cALR As clsALR)
    {

        Me.cALR = cALR;


        With cALR;
            Text = "Text Override"
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= " [" + .Key + "]"
            If .OldText = "" Then Text = "New Text Override"

            txtOld.Text = .OldText;
            txtNew.Description = .NewText.Copy;

        }

        Me.Show();
        Changed = False

        OpenForms.Add(Me);

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyALR();
        CloseALR(Me);
    }


    private void ApplyALR()
    {

        With cALR;
            .OldText = txtOld.Text;
            .NewText = txtNew.Description.Copy;

            if (.Key = "")
            {
                .Key = .GetNewKey ' Adventure.GetNewKey("Override");
                Adventure.htblALRs.Add(cALR, .Key);
            }
            .LastUpdated = Now;
            .IsLibrary = false;

            UpdateListItem(.Key, .OldText.ToString);
        }

        Adventure.Changed = true;

    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {

        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyALR()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseALR(Me);

    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyALR();
        Changed = False
    }

    private void StuffChanged(System.Object sender, System.EventArgs e)
    {
        Changed = True
    }


    private void frmTextOverride_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        OpenForms.Remove(Me);
    }


    private void frmHint_Load(object sender, System.EventArgs e)
    {
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgALR16.GetHicon);
        GetFormPosition(Me);
    }

    private void frmTextOverride_Shown(object sender, System.EventArgs e)
    {
        if (txtOld.Text = "")
        {
            txtOld.Focus();
        Else
            'txtNew.rtxtSource.SelectionStart = txtNew.rtxtSource.TextLength
            txtNew.rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.DocumentEnd);
            txtNew.rtxtSource.Focus();
        }
    }

    private void frmTextOverride_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "TextOverrides");
    }

}

}