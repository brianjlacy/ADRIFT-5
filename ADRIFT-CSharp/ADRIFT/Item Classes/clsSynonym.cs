using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsSynonym
{
    Inherits clsItem;

    Friend Overrides ReadOnly Property AllDescriptions As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            return all;
        }
    }

    Public Overrides ReadOnly Property Clone As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsSynonym);
        }
    }

    Public Overrides ReadOnly Property CommonName As String
        {
            get
            {
            private string sName = "";
            For Each sFrom As String In ChangeFrom
                If sName <> "" Then sName &= ", "
                sName &= sFrom;
            Next;
            sName &= " -> " + ChangeTo;
            return sName;
        }
    }

    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        return true;

    }

    public override void EditItem()
    {
#if Generator
        private New frmSynonym(Me) fSynonym;
#endif
    }

    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        For Each sFrom As String In ChangeFrom
            iReplacements += MyBase.FindStringInStringProperty(sFrom, sSearchString, sReplace, bFindAll);
        Next;
        iReplacements += MyBase.FindStringInStringProperty(sTo, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }

    public override int ReferencesKey(string sKey)
    {
        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;

        return iCount;
    }

    private New StringArrayList arlFrom;
    internal StringArrayList ChangeFrom
        {
            get
            {
            return arlFrom;
        }
set(StringArrayList)
            arlFrom = value
        }
    }

    private string sTo;
    public string ChangeTo
        {
            get
            {
            return sTo;
        }
set(String)
            sTo = value
        }
    }

}

}