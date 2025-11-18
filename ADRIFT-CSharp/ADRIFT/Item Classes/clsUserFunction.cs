using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsUserFunction
{
    Inherits clsItem;

    public string Name { get; set; }
    internal Description Output { get; set; }

internal enum ArgumentType
    {
        [Object];
        Character;
        Location;
        Number;
        Text;
    }


internal class Argument
    {
        public string Name { get; set; }
        public ArgumentType Type { get; set; }
    }
    internal List<Argument> Arguments;

    public void New()
    {
        Output = New Description
        Arguments = New List(Of Argument)
    }

    Friend Overrides ReadOnly Property AllDescriptions As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New List<Description> all;
            all.Add(Output);
            return all;
        }
    }

    Public Overrides ReadOnly Property Clone As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsItem);
        }
    }

    Public Overrides ReadOnly Property CommonName As String
        {
            get
            {
            return Name;
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
        private New frmUserFunction(Me) fUDF;
#endif
    }

    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.Name, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }

    public override int ReferencesKey(string sKey)
    {

    }

}

}