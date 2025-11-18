using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _;
partial class ExpandableDescription
{
    Inherits System.Windows.Forms.UserControl;

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _;
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && components IsNot null)
            {
                components.Dispose();
            }
        }
        finally
        {
            MyBase.Dispose(disposing);
        }
    }

    'Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _;
    private void InitializeComponent()
    {
        Me.components = New System.ComponentModel.Container();
        private Infragistics.Win.UltraWinEditors.DropDownEditorButton DropDownEditorButton1 = new Infragistics.Win.UltraWinEditors.DropDownEditorButton();
        Me.txtShortDesc = New Infragistics.Win.UltraWinEditors.UltraTextEditor();
        Me.GenTextbox1 = New ADRIFT.GenTextbox();
        Me.UltraPopupControlContainer1 = New Infragistics.Win.Misc.UltraPopupControlContainer(Me.components);
        (System.ComponentModel.ISupportInitialize)(Me.txtShortDesc).BeginInit();
        Me.SuspendLayout();
        '
        'txtShortDesc
        '
        Me.txtShortDesc.AutoSize = false;
        DropDownEditorButton1.PreferredDropDownSize = New System.Drawing.Size(0, 0);
        Me.txtShortDesc.ButtonsRight.Add(DropDownEditorButton1);
        Me.txtShortDesc.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.txtShortDesc.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Me.txtShortDesc.Location = New System.Drawing.Point(0, 0);
        Me.txtShortDesc.Name = "txtShortDesc";
        Me.txtShortDesc.Size = New System.Drawing.Size(537, 22);
        Me.txtShortDesc.TabIndex = 5;
        '
        'GenTextbox1
        '
        Me.GenTextbox1.AllowAlternateDescriptions = true;
        Me.GenTextbox1.BackColor = System.Drawing.Color.Transparent;
        Me.GenTextbox1.Location = New System.Drawing.Point(54, 0);
        Me.GenTextbox1.Name = "GenTextbox1";
        Me.GenTextbox1.sCommand = null;
        Me.GenTextbox1.Size = New System.Drawing.Size(311, 266);
        Me.GenTextbox1.TabIndex = 8;
        Me.GenTextbox1.Visible = false;
        '
        'UltraPopupControlContainer1
        '
        Me.UltraPopupControlContainer1.PopupControl = Me.GenTextbox1;
        Me.UltraPopupControlContainer1.PreferredDropDownSize = New System.Drawing.Size(0, 0);
        '
        'ExpandableDescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!);
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Me.Controls.Add(Me.GenTextbox1);
        Me.Controls.Add(Me.txtShortDesc);
        Me.Name = "ExpandableDescription";
        Me.Size = New System.Drawing.Size(537, 22);
        (System.ComponentModel.ISupportInitialize)(Me.txtShortDesc).EndInit();
        Me.ResumeLayout(false);

    }
    Friend WithEvents txtShortDesc As Infragistics.Win.UltraWinEditors.UltraTextEditor;
    Friend WithEvents UltraPopupControlContainer1 As Infragistics.Win.Misc.UltraPopupControlContainer;
    Friend WithEvents GenTextbox1 As ADRIFT.GenTextbox;

}

}