using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ADRIFT
{
' Purpose: Replacement Class for VB GetSetting and
' ''' SaveSetting.  Designed to replace the
' saving of application Settings in the registry
' with saving in an xml file in the installation directory.
' Allows user to set an alternate path in case of install
' directory lockdown.  Allows user to have their own Settings
' xml file or use one shared by all users on the same machine.
' Allows user to have multiple "AppNames" creating multiple
' xml files and multiple Settings (e.g., Settings, Registration, etc.)
' Completely simulates VB GetSetting and SaveSetting by simply
' instantiating this class and prefacing Get/SaveSetting calls with
' the instance object name, e.g., r.GetSetting(....).
' Author:  Les Smith
' Date Created: 01/24/2004 at 11:00:35
' CopyRight:  KnowDotNet
' '''***************************************
'


Option Strict On;

public class CSettings
{

#Region " Class Variables "
    private DataSet ds;
    private string xmlFile = String.Empty;
    private bool _AllUsers;
    private const string XML = ".xml";
    private string _AlternatePath = String.Empty;
#End Region

#Region " Public SaveSetting Overloaded Methods "
    Public Overloads Sub SaveSetting(ByVal AppTitle As String, _
       ByVal Settings As String, _;
       ByVal Key As String, _;
       ByVal Value As Boolean);
        SaveSetting(AppTitle, Settings, Key, CStr(Value));
    }
    Public Overloads Sub SaveSetting(ByVal AppTitle As String, _
       ByVal Settings As String, _;
       ByVal Key As String, _;
       ByVal Value As Integer);
        SaveSetting(AppTitle, Settings, Key, CStr(Value));
    }
    Public Overloads Sub SaveSetting(ByVal AppTitle As String, _
       ByVal Settings As String, _;
       ByVal Key As String, _;
       ByVal Value As String);

        ' this method sets or adds the value row for the passed key

        try
        {
            If xmlFile.Length = 0 Then SetupXMLFileName(AppTitle)

            if (ds Is null)
            {
                private DataTable dt = GetXml(Settings);
                if (dt Is null)
                {
                    ds = New DataSet
                    ds.DataSetName = "ADRIFTSettings";
                    dt = CreateDT(Settings, Key, Value)
                    ds.Tables.Add(dt);
                }
            Else
                private DataTable dt = ds.Tables(Settings);
                if (dt Is null)
                {
                    ' create new datatable named Settings
                    dt = CreateDT(Settings, Key, Value)
                    ds.Tables.Add(dt);
                Else
                    private int i;
                    private bool b;
                    For i = 0 To dt.Rows.Count - 1
                        if (CStr(dt.Rows(i).Item("key")) = Key)
                        {
                            dt.Rows(i).Item("value") = Value;
                            b = True
                            Exit For;
                        }
                    Next;
                    if (Not b)
                    {
                        AddRow(dt, Key, Value);
                    }
                }
            }
            ds.WriteXml(xmlFile);
        }
        catch (System.Exception ex)
        {
#if Not Debug Then ' cos it gets really annoying...
            ErrMsg("SaveSetting error", ex);
#endif
        }
    }
#End Region

#Region " Public Overloaded GetSetting Methods "
    Public Overloads Function GetSetting(ByVal AppTitle As String, _
       ByVal Settings As String, _;
       ByVal key As String, _;
       ByVal keyvalue As Integer) _;
       As Integer;
        private object o = GetSetting(AppTitle, Settings, key, CStr(keyvalue));
        if (o Is null)
        {
            return keyvalue;
        Else
            return CType(o, Integer);
        }
    }
    Public Overloads Function GetSetting(ByVal AppTitle As String, _
          ByVal Settings As String, _;
          ByVal key As String, _;
          ByVal keyvalue As Boolean) _;
          As Boolean;
        private object o = GetSetting(AppTitle, Settings, key, CStr(keyvalue));
        if (o Is null)
        {
            return keyvalue;
        Else
            return CType(o, Boolean);
        }
    }


    Public Overloads Function GetSetting(ByVal AppTitle As String, _
        ByVal Settings As String, _;
        ByVal key As String, _;
        ByVal keyvalue As String) _;
        As Object;

        private int i;
        private DataRow dr;
        private DataTable dt;


        try
        {
            If xmlFile.Length = 0 Then SetupXMLFileName(AppTitle)

            ' this method returns the value specified by the key
            if (ds Is null)
            {
                dt = GetXml(Settings)
                If dt == null Then Return keyvalue
            Else
                dt = ds.Tables(Settings)
            }
            if (dt IsNot null)
            {
                For i = 0 To dt.Rows.Count - 1
                    dr = dt.Rows(i)
                    if (CStr(dr("Key")) = key)
                    {
                        return dr("Value");
                    }
                Next;
            }
        }
        catch (System.Exception ex)
        {
            ErrMsg("GetSetting error", ex);
        }
        return keyvalue;

    }
#End Region

#Region " Private Methods "
    Private Function CreateDT(ByVal Settings As String, ByVal key As String, _
       ByVal value As Object) As DataTable;
        private DataTable dt;
        dt = New DataTable(Settings)
        dt.Columns.Add("Key", Type.GetType("System.String"));
        dt.Columns.Add("Value", Type.GetType("System.String"));
        AddRow(dt, key, value);
        return dt;
    }
    Private Sub AddRow(ByRef dt As DataTable, ByVal key As String, _
       ByVal value As Object);
        private DataRow newRow = dt.NewRow;
        newRow(0) = key;
        newRow(1) = value;
        dt.Rows.Add(newRow);
    }
    private DataTable GetXml(string tablename)
    {
        if (Not IO.File.Exists(xmlFile))
        {
            return null;
        }
        ds = New DataSet
        ds.DataSetName = "ADRIFTSettings";

        ds.ReadXml(xmlFile);
        private DataTable dt = ds.Tables(tablename);
        return dt;

    }
    private void SetupXMLFileName(string fn)
    {
        ' Returns filename for xmlfile, generated by using
        ' AppTitle supplied to the two public methods and then
        ' boolean supplied to the constructor.
        ' install directory may be locked so check to see if
        ' caller supplied an alternate directory.
        private string s;
        if (_AlternatePath.Length = 0)
        {
            s = IO.Path.GetDirectoryName( _
               Reflection.Assembly.GetExecutingAssembly.Location());
        Else
            s = IO.Path.GetDirectoryName(_AlternatePath)
        }
        if (_AllUsers)
        {
            xmlFile = s & IO.Path.DirectorySeparatorChar & fn & XML
        Else
            xmlFile = s & IO.Path.DirectorySeparatorChar & fn & "_" & System.Security.Principal.WindowsIdentity.GetCurrent.Name.Replace(IO.Path.DirectorySeparatorChar, "_") & XML ' Environ("UserName") & XML
        }
    }
#End Region

#Region " Constructor "
    public void New(bool AllUsers)
    {
        Me._AllUsers = AllUsers;
    }
#End Region

#Region " Property Methods "
    public string AlternatePath { get; set; }
        {
            get
            {
            return _AlternatePath;
        }
set(ByVal Value As String)
            _AlternatePath = Value
        }
    }
#End Region
}
}