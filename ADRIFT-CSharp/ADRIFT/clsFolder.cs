using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsFolder
{
    Inherits clsItem;

    Public Shared Event MembersChanged(ByVal sender As Object, ByVal e As EventArgs)

public class MemberList
    {
        Inherits Generic.List(Of String);

        Public Shared Event MembersChanged(ByVal sender As Object, ByVal e As EventArgs)

        Public Overloads Sub Add(ByVal item As String)
            MyBase.Add(item);
            If iLoading = 0 Then RaiseEvent MembersChanged(Me, New EventArgs)
        }

        Public Overloads Sub Remove(ByVal item As String)
            MyBase.Remove(item);
            RaiseEvent MembersChanged(Me, New EventArgs);
        }

        Public Overloads Sub RemoveAt(ByVal index As Integer)
            MyBase.RemoveAt(index);
            RaiseEvent MembersChanged(Me, New EventArgs);
        }

        Public Overloads Sub Insert(ByVal index As Integer, ByVal item As String)
            MyBase.Insert(index, item);
            RaiseEvent MembersChanged(Me, New EventArgs);
        }

    }

    public string Name;
    Public WithEvents Members As New MemberList ' Generic.List(Of String)
    public bool Expanded = false;
    public Infragistics.Win.UltraWinListView.UltraListViewStyle ViewType = Infragistics.Win.UltraWinListView.UltraListViewStyle.Details;
    public int SortColumn = -1;
    public Infragistics.Win.UltraWinListView.Sorting SortDirection = Infragistics.Win.UltraWinListView.Sorting.None;
    public int GroupBy = 0;
    public Infragistics.Win.UltraWinListView.Sorting GroupDirection = Infragistics.Win.UltraWinListView.Sorting.None;
    public Size Size;
    public Point Location = new Point(-100, -100);
    Public Event ColumnChanged(ByVal sender As Object, ByVal iCol As Integer, ByVal bVisible As Boolean)
    public bool Visible = false;

    public bool bShowCreatedDate = false;
    public bool ShowCreatedDate { get; set; }
        {
            get
            {
            return bShowCreatedDate;
        }
set(ByVal Boolean)
            bShowCreatedDate = value
            RaiseEvent ColumnChanged(Me, 0, value);
        }
    }
    public bool bShowModifiedDate = false;
    public bool ShowModifiedDate { get; set; }
        {
            get
            {
            return bShowModifiedDate;
        }
set(ByVal Boolean)
            bShowModifiedDate = value
            RaiseEvent ColumnChanged(Me, 1, value);
        }
    }
    public bool bShowType = false;
    public bool ShowType { get; set; }
        {
            get
            {
            return bShowType;
        }
set(ByVal Boolean)
            bShowType = value
            RaiseEvent ColumnChanged(Me, 2, value);
        }
    }
    public bool bShowKey = false;
    public bool ShowKey { get; set; }
        {
            get
            {
            return bShowKey;
        }
set(ByVal Boolean)
            bShowKey = value
            RaiseEvent ColumnChanged(Me, 3, value);
        }
    }
    public bool bShowPriority = false;
    public bool ShowPriority { get; set; }
        {
            get
            {
            return bShowPriority;
        }
set(ByVal Boolean)
            bShowPriority = value
            RaiseEvent ColumnChanged(Me, 4, value);
        }
    }

    public override bool DeleteKey(string sKey)
    {
        If Members.Contains(sKey) Then Members.Remove(sKey)
        return true;
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            return new Generic.List(Of SharedModule.Description);
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        ' Name
    }

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Name;
        }
    }

    public override void EditItem()
    {

    }


    ' Is this folder sKey, or does it contain sKey in any subfolder
    public bool ContainsKey(string sKey, bool bRecursive = true)
    {
        If Key = sKey Then Return true
        If Members.Contains(sKey) Then Return true

        if (bRecursive)
        {
            For Each sMember As String In Members
                if (Adventure.dictFolders.ContainsKey(sMember))
                {
                    If Adventure.dictFolders(sMember).ContainsKey(sKey) Then Return true
                }
            Next;
        }

        return false;

    }


    public void New(string sKey = "")
    {
        If Adventure == null Then Exit Sub
        if (sKey = "")
        {
            Key = Me.GetNewKey ' Adventure.GetNewKey("Folder")
        Else
            Key = sKey
        }

        Name = "New Folder"
        Expanded = True
    }

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsFolder);
        }
    }


    public override int ReferencesKey(string sKey)
    {
        return 0;
    }

    private void Members_MembersChanged(object sender, System.EventArgs e)
    {
        RaiseEvent MembersChanged(Me, e);
    }

}

}