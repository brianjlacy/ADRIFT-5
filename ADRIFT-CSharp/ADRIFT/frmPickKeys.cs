using Infragistics.Win;
using Infragistics.Win.UltraWinListView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{

public class frmPickKeys
{
    Inherits System.Windows.Forms.Form;
    Implements Infragistics.Win.IUIElementDrawFilter;

    private Point P;
    private bool bMultiple;
    private bool bLoaded = false;
    private bool bChanging = false;

#Region " Windows Form Designer generated code "

    internal void New(Point P, bool Multiple)
    {
        MyBase.New();

        'This call is required by the Windows Form Designer.
        InitializeComponent();

        'Add any initialization after the InitializeComponent() call
        Me.P = P;
        Me.bMultiple = Multiple;

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
    Friend WithEvents lvwKeys As MyListView;
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader;
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent();
        private Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance2 = new Infragistics.Win.Appearance();
        private Infragistics.Win.Appearance Appearance3 = new Infragistics.Win.Appearance();
        Me.lvwKeys = New ADRIFT.frmPickKeys.MyListView();
        Me.ColumnHeader1 = (System.Windows.Forms.ColumnHeader)(New System.Windows.Forms.ColumnHeader());
        (System.ComponentModel.ISupportInitialize)(Me.lvwKeys).BeginInit();
        Me.SuspendLayout();
        '
        'lvwKeys
        '
        Me.lvwKeys.AllowDrop = true;
        Me.lvwKeys.Dock = System.Windows.Forms.DockStyle.Fill;
        Me.lvwKeys.Filter = null;
        Me.lvwKeys.MainColumn.Sorting = Sorting.Ascending;
        Me.lvwKeys.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (Byte)(0));
        Appearance1.BorderColor = System.Drawing.Color.Red;
        Me.lvwKeys.ItemSettings.Appearance = Appearance1;
        Me.lvwKeys.ItemSettings.HideSelection = false;
        Me.lvwKeys.ItemSettings.HotTracking = true;
        Appearance2.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(247), Integer), (Byte)(CType(252), Integer), (Byte)(CType(254), Integer));
        Appearance2.BackColor2 = System.Drawing.Color.FromArgb((Byte)(CType(234), Integer), (Byte)(CType(246), Integer), (Byte)(CType(253), Integer));
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        Appearance2.BorderColor = System.Drawing.SystemColors.ActiveCaption;
        Appearance2.FontData.UnderlineAsString = "false";
        Appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
        Me.lvwKeys.ItemSettings.HotTrackingAppearance = Appearance2;
        Me.lvwKeys.ItemSettings.LassoSelectMode = Infragistics.Win.UltraWinListView.LassoSelectMode.LeftMouseButton;
        Appearance3.BackColor = System.Drawing.Color.FromArgb((Byte)(CType(238), Integer), (Byte)(CType(247), Integer), (Byte)(CType(253), Integer));
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb((Byte)(CType(218), Integer), (Byte)(CType(241), Integer), (Byte)(CType(252), Integer));
        Appearance3.BackColorDisabled = System.Drawing.Color.FromArgb((Byte)(CType(246), Integer), (Byte)(CType(246), Integer), (Byte)(CType(246), Integer));
        Appearance3.BackColorDisabled2 = System.Drawing.Color.FromArgb((Byte)(CType(231), Integer), (Byte)(CType(231), Integer), (Byte)(CType(231), Integer));
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        Appearance3.BorderColor = System.Drawing.Color.Lime;
        Appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
        Me.lvwKeys.ItemSettings.SelectedAppearance = Appearance3;
        Me.lvwKeys.Location = New System.Drawing.Point(0, 0);
        Me.lvwKeys.MainColumn.DataType = GetType(String);
        Me.lvwKeys.Name = "lvwKeys";
        Me.lvwKeys.Size = New System.Drawing.Size(234, 98);
        Me.lvwKeys.TabIndex = 0;
        Me.lvwKeys.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.Details;
        Me.lvwKeys.ViewSettingsDetails.ColumnHeaderStyle = Infragistics.Win.HeaderStyle.XPThemed;
        Me.lvwKeys.ViewSettingsDetails.ColumnHeadersVisible = false;
        Me.lvwKeys.ViewSettingsDetails.ColumnsShowSortIndicators = false;
        Me.lvwKeys.ViewSettingsDetails.FullRowSelect = true;
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 184;
        '
        'frmPickKeys
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13);
        Me.ClientSize = New System.Drawing.Size(234, 98);
        Me.Controls.Add(Me.lvwKeys);
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Me.Name = "frmPickKeys";
        Me.ShowInTaskbar = false;
        Me.Text = "PickKeys";
        (System.ComponentModel.ISupportInitialize)(Me.lvwKeys).EndInit();
        Me.ResumeLayout(false);

    }

#End Region

    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

    '    Const WM_PAINT As Integer = &HF  ' 15

    '    Select Case m.Msg
    '        Case WM_PAINT
    '            If lvwKeys.View = View.Details AndAlso lvwKeys.Columns.Count > 0 Then
    '                lvwKeys.Columns(lvwKeys.Columns.Count - 1).Width = Me.Width ' -2
    '            End If
    '    End Select

    '    MyBase.WndProc(m)

    'End Sub


    private void frmPickKeys_LostFocus(object sender, System.EventArgs e)
    {
        Me.Visible = false;
    }

    private void lvwKeys_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        If bLoaded && ! bMultiple + ! bChanging Then Me.Visible = false
    }

    private void frmPickKeys_Activated(object sender, System.EventArgs e)
    {
        bLoaded = True
    }

    private void frmPickKeys_VisibleChanged(object sender, System.EventArgs e)
    {

        if (Me.Visible)
        {
            Me.Location = P;
            if (lvwKeys.Items.Count > 0)
            {
                private Screen scrn = Screen.FromControl(Me);
                if (CInt(lvwKeys.VisibleCount * lvwKeys.ItemSizeResolved.Height) > scrn.WorkingArea.Height + scrn.Bounds.Y - Me.Top)
                {
                    Me.Height = scrn.WorkingArea.Height + scrn.Bounds.Y - Me.Top - 20;
                    lvwKeys.MainColumn.Width = Me.Width - 20;
                Else
                    Me.Height = (lvwKeys.VisibleCount * lvwKeys.ItemSizeResolved.Height) + 4 '.25);
                    lvwKeys.MainColumn.Width = Me.Width - 20;
                }
                If Me.Height > scrn.WorkingArea.Height / 2 Then Me.Height = CInt(scrn.WorkingArea.Height / 2)
            }
        }
    }

    public void AddItem(clsItem item)
    {

        private Infragistics.Win.UltraWinListView.UltraListViewItem lvi = Folder.GetItem(item, null);
        lvwKeys.Add(lvi);

    }
    public void AddItem(string sName, string sKey, Image img)
    {
        private New Infragistics.Win.UltraWinListView.UltraListViewItem(sKey) lvi;
        lvi.Value = sName;
        lvi.Appearance.Image = img;

        lvwKeys.Add(lvi);
    }

    'Private Sub frmPickKeys_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    lvwKeys.OwnerDraw = True
    'End Sub

    private void lvwKeys_ListChanging(object sender, EventArgs e)
    {
        bChanging = True
    }
    private void lvwKeys_ListChanged(object sender, EventArgs e)
    {
        bChanging = False
    }

    public DrawPhase Implements IUIElementDrawFilter.GetPhasesToFilter GetPhasesToFilter(ref drawParams As UIElementDrawParams)
    {
        'Throw New NotImplementedException()
        if (TypeOf drawParams.Element Is Infragistics.Win.EditorWithTextDisplayTextUIElement)
        {
            return DrawPhase.BeforeDrawForeground;
        Else
            return DrawPhase.None;
        }
    }

    public Boolean Implements IUIElementDrawFilter.DrawElement DrawElement(DrawPhase drawPhase, ref drawParams As UIElementDrawParams)
    {

        private EditorWithTextDisplayTextUIElement el = CType(drawParams.Element, EditorWithTextDisplayTextUIElement);

        If el == null Then Return false

        private string sText = el.Text;

        if (Not String.IsNullOrEmpty(lvwKeys.Filter) && sText.Contains(lvwKeys.Filter))
        {

            private New SolidBrush(SystemColors.MenuText) bBlack;
            private New SolidBrush(Color.Red) bRed;
            private string sPre = sText.Substring(0, sText.IndexOf(lvwKeys.Filter));
            private string sPost = sText.Substring(sText.IndexOf(lvwKeys.Filter) + lvwKeys.Filter.Length);
            private Rectangle r = el.RectInsidePadding ' el.Rect;
            private int iPreLength = 4;

            if (Not String.IsNullOrEmpty(sPre))
            {
                drawParams.Graphics.DrawString(sPre, lvwKeys.Font, bBlack, New Point(r.X, r.Y + 1));
                iPreLength = CInt(drawParams.Graphics.MeasureString(sPre.Replace(" ", "."), lvwKeys.Font).Width)
            }

            drawParams.Graphics.DrawString(lvwKeys.Filter, lvwKeys.Font, bRed, New Point(r.X + iPreLength - 4, r.Y + 1));

            if (Not String.IsNullOrEmpty(sPost))
            {
                private int iFilterLen = CInt(drawParams.Graphics.MeasureString(lvwKeys.Filter.Replace(" ", "."), lvwKeys.Font).Width);
                drawParams.Graphics.DrawString(sPost, lvwKeys.Font, bBlack, New Point(r.X + iPreLength + iFilterLen - 8, r.Y + 1));
            }

            return true;
        Else
            return false;
        }

    }

    private void frmPickKeys_Load(object sender, EventArgs e)
    {
        lvwKeys.DrawFilter = Me;
    }

internal class MyListView
    {
        Inherits Infragistics.Win.UltraWinListView.UltraListView;

        Public Event ListChanging(ByVal sender As Object, ByVal e As EventArgs)
        Public Event ListChanged(ByVal sender As Object, ByVal e As EventArgs)

        private int _VisibleCount = 0;
        Public ReadOnly Property VisibleCount As Integer
            {
                get
                {
                if (String.IsNullOrEmpty(Filter))
                {
                    return Items.Count;
                Else
                    return _VisibleCount;
                }
            }
        }

        private string _Filter;
        public string Filter
            {
                get
                {
                return _Filter;
            }
set(String)
                if (value <> _Filter)
                {
                    RaiseEvent ListChanging(Me, New EventArgs);
                    Me.SuspendLayout();
                    _VisibleCount = 0
                    For Each lvi As Infragistics.Win.UltraWinListView.UltraListViewItem In Items
                        if (lvi.Text.Contains(value))
                        {
                            If ! lvi.Visible Then lvi.Visible = true
                            _VisibleCount += 1;
                        Else
                            If lvi.Visible Then lvi.Visible = false
                        }
                    Next;
                    Me.ResumeLayout();
                    _Filter = value
                    RaiseEvent ListChanged(Me, New EventArgs);
                }
            }
        }

        public void Add(Infragistics.Win.UltraWinListView.UltraListViewItem lvi)
        {
            Items.Add(lvi);
        }

        private void MyListView_KeyPress(object sender, KeyPressEventArgs e)
        {

            If ! fGenerator?.AutoComplete Then Exit Sub

            switch (e.KeyChar)
            {
                case Convert.ToChar(Keys.Back):
                    {
                    If Filter?.Length > 0 Then Filter = Filter.Substring(0, Filter.Length - 1)
                    e.Handled = true;
                default:
                    {
                    if (IsInputChar(e.KeyChar))
                    {
                        Filter &= e.KeyChar;
                        e.Handled = true;
                    }
            }
        }

    }


internal class MyListView2
    {
        Inherits ListView;

        Public Event ListChanging(ByVal sender As Object, ByVal e As EventArgs)
        Public Event ListChanged(ByVal sender As Object, ByVal e As EventArgs)

        private New List<ListViewItem> _List;

        private string _Filter;
        public string Filter
            {
                get
                {
                return _Filter;
            }
set(String)
                if (value <> _Filter)
                {
                    RaiseEvent ListChanging(Me, New EventArgs);
                    'For Each lvi As ListViewItem In _List

                    'Next
                    Debug.WriteLine(value);
                    Me.SuspendLayout();
                    for (int i = Items.Count - 1; i <= 0; i += -1)
                    {
                        private ListViewItem lvi = Items.Item(i);
                        if (Not lvi.Text.Contains(value))
                        {
                            Items.RemoveAt(i);
                        }
                    Next;
                    Me.ResumeLayout();
                    _Filter = value
                    RaiseEvent ListChanged(Me, New EventArgs);
                }
            }
        }

        public void Add(ListViewItem lvi)
        {
            Items.Add(lvi);
            _List.Add(lvi);
        }


        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {

            private New SolidBrush(SystemColors.MenuText) bBlack;
            private Rectangle r = e.Item.GetBounds(ItemBoundsPortion.Label);


            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(New SolidBrush(SystemColors.MenuHighlight), e.Item.GetBounds(ItemBoundsPortion.Entire));
                'Dim p As New Pen(Color.Black)
                'P.DashStyle = Drawing2D.DashStyle.Dot
                'e.Graphics.DrawRectangle(p, e.Item.GetBounds(ItemBoundsPortion.Entire))
                bBlack = New SolidBrush(SystemColors.HighlightText)
            }

            if (Not String.IsNullOrEmpty(Filter) && e.Item.Text.Contains(Filter))
            {

                private New SolidBrush(Color.Red) bRed;
                private string sPre = e.Item.Text.Substring(0, e.Item.Text.IndexOf(Filter));
                private string sPost = e.Item.Text.Substring(e.Item.Text.IndexOf(Filter) + Filter.Length);

                e.Graphics.DrawString(sPre, Me.Font, bBlack, r.Location);

                private int iPreLength = CInt(e.Graphics.MeasureString(sPre.Replace(" ", "."), Me.Font).Width);
                e.Graphics.DrawString(Filter, Me.Font, bRed, New Point(r.X + iPreLength - 4, r.Y));

                if (Not String.IsNullOrEmpty(sPost))
                {
                    private int iFilterLen = CInt(e.Graphics.MeasureString(Filter, Me.Font).Width);
                    e.Graphics.DrawString(sPost, Me.Font, bBlack, New Point(r.X + iPreLength + iFilterLen - 8, r.Y));
                }

            Else
                e.Graphics.DrawString(e.Item.Text, Me.Font, bBlack, r.Location);
            }

        }



        private void MyListView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                    If Filter.Length > 0 Then Filter = Filter.Substring(0, Filter.Length - 1)
            }
        }

        private void MyListView_KeyPress(object sender, KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case Convert.ToChar(Keys.Back):
                    {
                    If Filter?.Length > 0 Then Filter = Filter.Substring(0, Filter.Length - 1)
                default:
                    {
                    if (IsInputChar(e.KeyChar))
                    {
                        Filter &= e.KeyChar;
                        e.Handled = true;
                    }
            }
        }
    }

}

}