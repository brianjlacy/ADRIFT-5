using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class frmUserFunction
{

    private bool bChanged;
    private clsUserFunction cUDF;


    public void New(clsUserFunction UDF)
    {
        MyBase.New();

        ' Check that this window isn't already open
        For Each w As Form In OpenForms
            if (TypeOf w Is frmUserFunction)
            {
                if (CType(w, frmUserFunction).cUDF.Key = UDF.Key && UDF.Key IsNot null)
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
        LoadForm(UDF);

    }

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

    private void LoadForm(ref cUDF As clsUserFunction)
    {

        Me.cUDF = cUDF;

        With colType;
            For Each typ As clsUserFunction.ArgumentType In [Enum].GetValues(GetType(clsUserFunction.ArgumentType))
                .ValueList.ValueListItems.Add(CInt(typ), typ.ToString);
            Next;
            txtOutput.Arguments = cUDF.Arguments;
        }

        With cUDF;
            Text = "User Function"
            If SafeBool(GetSetting("ADRIFT", "Generator", "ShowKeys", "0")) Then Text &= " [" + .Key + "]"
            If .Name = "" Then Text = "New User Function"

            txtName.Text = .Name;
            txtOutput.Description = .Output.Copy;

            For Each arg As clsUserFunction.Argument In .Arguments
                grdArguments.Rows.Add(New Object() {arg.Name, CInt(arg.Type)});
            Next;

        }

        SetArgs();

        Me.Show();
        Changed = False

        OpenForms.Add(Me);

    }

    private void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        ApplyUDF();
        CloseUDF(Me);
    }


    private void ApplyUDF()
    {

        With cUDF;
            .Name = txtName.Text;
            .Output = txtOutput.Description.Copy;

            .Arguments.Clear();
            For Each row As DataGridViewRow In grdArguments.Rows
                private New clsUserFunction.Argument arg;
                if (row.Cells("colName").Value IsNot null)
                {
                    arg.Name = row.Cells("colName").Value.ToString;
                    arg.Type = (row.Cells("colType")([Enum].Parse(GetType(clsUserFunction.ArgumentType)).Value.ToString), clsUserFunction.ArgumentType);
                    .Arguments.Add(arg);
                }
            Next;

            if (.Key = "")
            {
                .Key = .GetNewKey;
                Adventure.htblUDFs.Add(cUDF, .Key);
            }
            .LastUpdated = Now;
            .IsLibrary = false;

            UpdateListItem(.Key, .Name);
        }

        Adventure.Changed = true;

    }


    private void btnCancel_Click(System.Object sender, System.EventArgs e)
    {

        if (Changed)
        {
            private DialogResult result = MessageBox.Show("Would you like to apply your changes?", "ADRIFT Developer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            If result = Windows.Forms.DialogResult.Yes Then ApplyUDF()
            If result = Windows.Forms.DialogResult.Cancel Then Exit Sub
        }
        CloseUDF(Me);

    }

    private void btnApply_Click(System.Object sender, System.EventArgs e)
    {
        ApplyUDF();
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
        Me.Icon = Icon.FromHandle(My.Resources.Resources.imgFunction16.GetHicon);
        GetFormPosition(Me);
    }


    private void frmTextOverride_Shown(object sender, System.EventArgs e)
    {
        if (txtName.Text = "")
        {
            txtName.Focus();
        Else
            txtOutput.rtxtSource.EditInfo.PerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.DocumentEnd);
            txtOutput.rtxtSource.Focus();
        }
    }

    private void frmTextOverride_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ShowHelp(Me, "UserDefinedFunctions");
    }

    private void grdArguments_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
        SetArgs();
    }


    private void SetArgs()
    {
        With txtOutput;
            .Arguments = New List(Of clsUserFunction.Argument);
            For Each row As DataGridViewRow In grdArguments.Rows
                private New clsUserFunction.Argument arg;
                if (row.Cells(0).Value IsNot null)
                {
                    arg.Name = row.Cells("colName").Value.ToString;
                    If row.Cells("colType").Value != DBNull.Value Then arg.Type = (row.Cells("colType")([Enum].Parse(GetType(clsUserFunction.ArgumentType)).Value.ToString), clsUserFunction.ArgumentType)
                    .Arguments.Add(arg);
                }
            Next;
            .rtxtSource.Arguments = .Arguments;
        }
    }

}
}