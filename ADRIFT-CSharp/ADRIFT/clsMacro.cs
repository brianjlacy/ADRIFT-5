using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsMacro
{
    Implements ICloneable;

    public void New(string sKey)
    {
        MyBase.New();

        Key = sKey
    }


    private string sTitle;
    public string Title { get; set; }
        {
            get
            {
            return sTitle;
        }
set(ByVal String)
            ' TODO: Ensure unique for this adventure
            sTitle = value
        }
    }

    private string sCommands;
    public string Commands { get; set; }
        {
            get
            {
            return sCommands;
        }
set(ByVal String)
            sCommands = value
        }
    }

    private bool bShared;
    Public Property [Shared]() As Boolean
        {
            get
            {
            return bShared;
        }
set(ByVal Boolean)
            bShared = value
        }
    }

    private string sIFID;
    public string IFID { get; set; }
        {
            get
            {
            return sIFID;
        }
set(ByVal String)
            sIFID = value
        }
    }

    private Shortcut kShortcut = Windows.Forms.Shortcut.None;
    public Shortcut Shortcut { get; set; }
        {
            get
            {
            return kShortcut;
        }
set(ByVal Shortcut)
            kShortcut = value
        }
    }

    public Object Implements System.ICloneable.Clone Clone()
    {
        return Me.MemberwiseClone;
    }

    private string sKey;
    public string Key { get; set; }
        {
            get
            {
            return sKey;
        }
set(ByVal String)
            sKey = value
        }
    }

}
}