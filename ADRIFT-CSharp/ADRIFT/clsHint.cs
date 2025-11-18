using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsHint
{
    Inherits clsItem;

    'Private sKey As String
    private string sQuestion;
    private Description oSubtleHint;
    private Description oSledgeHammerHint;
    internal New RestrictionArrayList arlRestrictions;


    public void New()
    {
        oSubtleHint = New Description
        oSledgeHammerHint = New Description
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
    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        If ! arlRestrictions.DeleteKey(sKey) Then Return false

        return (true);

    }


    public string Question { get; set; }
        {
            get
            {
            return sQuestion;
        }
set(ByVal Value As String)
            sQuestion = Value
        }
    }

    internal Description SubtleHint { get; set; }
        {
            get
            {
            return oSubtleHint;
        }
set(ByVal Value As Description)
            oSubtleHint = Value
        }
    }

    internal Description SledgeHammerHint { get; set; }
        {
            get
            {
            return oSledgeHammerHint;
        }
set(ByVal Value As Description)
            oSledgeHammerHint = Value
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

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsHint);
        }
    }

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Question;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            all.Add(SledgeHammerHint);
            all.Add(SubtleHint);
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sQuestion, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmHint(Me, True) fHint;
#endif
    }

    public override int ReferencesKey(string sKey)
    {

    }

}

}