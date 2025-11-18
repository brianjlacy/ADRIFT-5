using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class Expression
{

    Public Event ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    public string Value { get; set; }
        {
            get
            {
            return txtExpression.Text;
        }
set(ByVal String)
            txtExpression.Text = value;
            if (value = "")
            {
                btnEdit.Appearance.Image = My.Resources.Resources.imgAdd16;
            Else
                btnEdit.Appearance.Image = My.Resources.Resources.imgEdit16;
            }
        }
    }


    private void btnEdit_Click(System.Object sender, System.EventArgs e)
    {
        MessageBox.Show("TODO: Expression Builder" + vbCrLf + vbCrLf _;
 + "Some supported functions are: ABS(), EITHER(), IF(), sInstr(), LCASE(), LEFT(), LEN(), MAX(), MIN(), MOD(), sMid(), PCASE(), RAND(), RIGHT(), STR(), UCASE(), VAL()" + vbCrLf + vbCrLf _;
 + "For example: IF(%variable1% = 1, %variable2% + 1, RAND(5, 7))", "Expression Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    private void txtExpression_TextChanged(object sender, System.EventArgs e)
    {
        RaiseEvent ValueChanged(sender, e);
    }


    ' Does this expression return a String or an Integer value?
    public clsVariable.VariableTypeEnum DataTypeOfExpression { get; }
        {
            get
            {
            try
            {
                If Trim(txtExpression.Text) = "" Then Return (clsVariable.VariableTypeEnum)(-1)

                private New clsVariable var;
                var.SetToExpression(Trim(txtExpression.Text), , true);

                if ((var.IntValue <> Integer.MinValue && var.IntValue <> 0) || IsNumeric(var.StringValue))
                {
                    return clsVariable.VariableTypeEnum.Numeric;
                Else
                    If Trim(txtExpression.Text) = "0" Then Return clsVariable.VariableTypeEnum.Numeric
                    return clsVariable.VariableTypeEnum.Text;
                }
            }
            catch (Exception ex)
            {
                ' Bad expression
                return CType(-1, clsVariable.VariableTypeEnum);
            }
        }
    }

}

}