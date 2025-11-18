using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsGroup
{
    Inherits clsItem;

    private string sName;
    'Private sKey As String

public enum GroupTypeEnum
    {
        Locations = 0
        Objects = 1
        Characters = 2
    }
    private GroupTypeEnum eGroupType;

    internal New PropertyHashTable htblProperties;

    private New StringArrayList sarlMembers;
    internal StringArrayList arlMembers { get; set; }
        {
            get
            {
            if (Key = ALLROOMS)
            {
                sarlMembers.Clear();
                For Each sKey As String In Adventure.htblLocations.Keys
                    sarlMembers.Add(sKey);
                Next;
            }
            return sarlMembers;
        }
set(ByVal StringArrayList)
            sarlMembers = value
        }
    }

    'Public Property Key() As String
    '    Get
    '        Return sKey
    '    End Get
    '    Set(ByVal Value As String)
    '        If Not KeyExists(Value) Then
    '            sKey = Value
    '        Else
    '            Throw New Exception("Key " & sKey & " already exists")
    '        End If
    '    End Set
    'End Property


    public string Name { get; set; }
        {
            get
            {
            return sName;
        }
set(ByVal Value As String)
            sName = Value
        }
    }

    public GroupTypeEnum GroupType { get; set; }
        {
            get
            {
            return eGroupType;
        }
set(ByVal Value As GroupTypeEnum)
            eGroupType = Value
        }
    }

    public string RandomKey { get; }
        {
            get
            {
            return arlMembers(Random(Me.arlMembers.Count - 1));
        }
    }
    'Private bIsLibrary As Boolean
    'Public Property IsLibrary() As Boolean
    '    Get
    '        Return bIsLibrary
    '    End Get
    '    Set(ByVal value As Boolean)
    '        bIsLibrary = value
    '    End Set
    'End Property

    'Private dtLastUpdated As Date
    'Friend Property LastUpdated() As Date
    '    Get
    '        If dtLastUpdated > Date.MinValue Then
    '            Return dtLastUpdated
    '        Else
    '            Return Now
    '        End If
    '    End Get
    '    Set(ByVal value As Date)
    '        dtLastUpdated = value
    '    End Set
    'End Property

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsGroup);
        }
    }

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Name;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sName, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmGroup(Me, True) fRoomGroup;
#endif
    }

    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        If ! Key = ALLROOMS && Me.arlMembers.Contains(sKey) Then iCount += 1
        iCount += htblProperties.ReferencesKey(sKey);

        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        If arlMembers.Contains(sKey) Then arlMembers.Remove(sKey)
        If ! htblProperties.DeleteKey(sKey) Then Return false

        return true;

    }


}

}