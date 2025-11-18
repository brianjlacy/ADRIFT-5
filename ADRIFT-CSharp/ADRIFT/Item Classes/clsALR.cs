using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsALR
{
    Inherits clsItem;

    'Private sKey As String
    private string sOldText;
    private Description oNewText;

    public void New()
    {
        NewText = New Description
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

    internal string OldText { get; set; }
        {
            get
            {
            return sOldText;
        }
set(ByVal Value As String)
            sOldText = Value
        }
    }

    internal Description NewText { get; set; }
        {
            get
            {
            If oNewText == null Then oNewText = New Description
            return oNewText;
        }
set(ByVal Value As Description)
            oNewText = Value
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
            return CType(Me.MemberwiseClone, clsALR);
        }
    }

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return OldText;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            all.Add(NewText);
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sOldText, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmTextOverride(Me) fALR;
#endif
    }


    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;

        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;

        return true;

    }

}

}