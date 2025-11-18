using ComponentAce.Compression.Libs.zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encoding;
using System.Xml;

namespace ADRIFT
{


internal static class FileIO
{

    'Private WithEvents zi As DotZLib.Inflater
    'Private WithEvents zd As DotZLib.Deflater
    private BinaryReader br;
    private BinaryWriter bw;
    private string sLoadString;
    Private bAdventure() As Byte
    private New StringArrayList salWithStates;
    private int iStartPriority;
    private double dFileVersion;
internal enum LoadWhatEnum
    {
        All;
        Properies;
        AllExceptProperties;
    }


    private Description LoadDescription(XmlElement nodContainerXML, string sNode)
    {

        private New Description Description;

        If nodContainerXML.Item(sNode) == null || nodContainerXML.GetElementsByTagName(sNode).Count = 0 Then Return Description

        private XmlElement nodDescription = CType(nodContainerXML.GetElementsByTagName(sNode)(0), XmlElement);

        For Each nodDesc As XmlElement In nodDescription.SelectNodes("Description") '. nodDescription.GetElementsByTagName("Description")
            private New SingleDescription sd;
            sd.Restrictions = LoadRestrictions(nodDesc);
            sd.eDisplayWhen = (nodDesc.Item("DisplayWhen")([Enum].Parse(GetType(SingleDescription.DisplayWhenEnum)).InnerText), SingleDescription.DisplayWhenEnum);
            If nodDesc.Item("Text") != null Then sd.Description = nodDesc.Item("Text").InnerText
            If nodDesc.Item("DisplayOnce") != null Then sd.DisplayOnce = GetBool(nodDesc.Item("DisplayOnce").InnerText)
            If nodDesc.Item("ReturnToDefault") != null Then sd.ReturnToDefault = GetBool(nodDesc.Item("ReturnToDefault").InnerText)
            If nodDesc.Item("TabLabel") != null Then sd.sTabLabel = SafeString(nodDesc.Item("TabLabel").InnerText)
            Description.Add(sd);
        Next;

        If Description.Count > 1 Then Description.RemoveAt(0)
        return Description;

    }


    private void SaveDescription(XmlTextWriter xmlWriter, Description Description, string sNode)
    {

        If Description.Count = 0 || (Description.Count = 1 && Description(0).Description = "") Then Exit Sub

        With xmlWriter;
            .WriteStartElement(sNode) ' Description;
            For Each sd As SingleDescription In Description
                .WriteStartElement("Description") ' SingleDescription;
                If sd.Restrictions.Count > 0 Then SaveRestrictions(xmlWriter, sd.Restrictions)
                .WriteElementString("DisplayWhen", sd.eDisplayWhen.ToString);
                'If sd.Description = " " Then sd.Description = "&nbsp;" ' XML converts " " to Null when reading back
                .WriteElementString("Text", sd.Description);
                If sd.DisplayOnce Then .WriteElementString("DisplayOnce", "1")
                If sd.ReturnToDefault Then .WriteElementString("ReturnToDefault", "1")
                if (sd.sTabLabel <> "Default Description" && Not sd.sTabLabel.StartsWith("Alternative Description ") && sd.sTabLabel <> "")
                {
                    .WriteElementString("TabLabel", sd.sTabLabel);
                }
                .WriteEndElement() ' Description;
            Next;
            .WriteEndElement() ' sNode;
        }
    }


#if Runner
    ' Write a state to file
    internal bool SaveState(clsGameState state, string sFilePath)
    {

        try
        {
            private New MemoryStream stmMemory;
            private New System.Xml.XmlTextWriter(stmMemory, System.Text.Encoding.UTF8) xmlWriter;
            Dim bData() As Byte

            With xmlWriter;
                .Indentation = 4 ' Change later;

                .WriteStartDocument();
                .WriteStartElement("Game");

                ' TODO: Ideally this should only save values that are different from the initial TAF state

                For Each sKey As String In state.htblLocationStates.Keys
                    private clsGameState.clsLocationState locs = CType(state.htblLocationStates(sKey), clsGameState.clsLocationState);
                    .WriteStartElement("Location");
                    .WriteElementString("Key", sKey);
                    For Each sPropKey As String In locs.htblProperties.Keys
                        private clsGameState.clsLocationState.clsStateProperty props = CType(locs.htblProperties(sPropKey), clsGameState.clsLocationState.clsStateProperty);
                        .WriteStartElement("Property");
                        .WriteElementString("Key", sPropKey);
                        .WriteElementString("Value", props.Value);
                        .WriteEndElement();
                    Next;
                    For Each sDisplayedKey As String In locs.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    '.WriteElementString("Seen", CInt(locs.bSeen).ToString)
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblObjectStates.Keys
                    private clsGameState.clsObjectState obs = CType(state.htblObjectStates(sKey), clsGameState.clsObjectState);
                    .WriteStartElement("Object");
                    .WriteElementString("Key", sKey);
                    if (obs.Location.DynamicExistWhere <> clsObjectLocation.DynamicExistsWhereEnum.Hidden)
                    {
                        .WriteElementString("DynamicExistWhere", obs.Location.DynamicExistWhere.ToString);
                    }
                    if (obs.Location.StaticExistWhere <> clsObjectLocation.StaticExistsWhereEnum.NoRooms)
                    {
                        .WriteElementString("StaticExistWhere", obs.Location.StaticExistWhere.ToString);
                    }
                    .WriteElementString("LocationKey", obs.Location.Key);
                    For Each sPropKey As String In obs.htblProperties.Keys
                        private clsGameState.clsObjectState.clsStateProperty props = CType(obs.htblProperties(sPropKey), clsGameState.clsObjectState.clsStateProperty);
                        .WriteStartElement("Property");
                        .WriteElementString("Key", sPropKey);
                        .WriteElementString("Value", props.Value);
                        .WriteEndElement();
                    Next;
                    For Each sDisplayedKey As String In obs.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblTaskStates.Keys
                    private clsGameState.clsTaskState tas = CType(state.htblTaskStates(sKey), clsGameState.clsTaskState);
                    .WriteStartElement("Task");
                    .WriteElementString("Key", sKey);
                    .WriteElementString("Completed", tas.Completed.ToString);
                    .WriteElementString("Scored", tas.Scored.ToString);
                    For Each sDisplayedKey As String In tas.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblEventStates.Keys
                    private clsGameState.clsEventState evs = CType(state.htblEventStates(sKey), clsGameState.clsEventState);
                    .WriteStartElement("Event");
                    .WriteElementString("Key", sKey);
                    .WriteElementString("Status", evs.Status.ToString);
                    .WriteElementString("Timer", evs.TimerToEndOfEvent.ToString);
                    .WriteElementString("SubEventTime", evs.iLastSubEventTime.ToString);
                    .WriteElementString("SubEventIndex", evs.iLastSubEventIndex.ToString);
                    For Each sDisplayedKey As String In evs.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblCharacterStates.Keys
                    private clsGameState.clsCharacterState chs = CType(state.htblCharacterStates(sKey), clsGameState.clsCharacterState);
                    .WriteStartElement("Character");
                    .WriteElementString("Key", sKey);
                    if (chs.Location.ExistWhere <> clsCharacterLocation.ExistsWhereEnum.Hidden)
                    {
                        .WriteElementString("ExistWhere", chs.Location.ExistWhere.ToString);
                    }
                    if (chs.Location.Position <> clsCharacterLocation.PositionEnum.Standing)
                    {
                        .WriteElementString("Position", chs.Location.Position.ToString);
                    }
                    If chs.Location.Key <> "" Then .WriteElementString("LocationKey", chs.Location.Key)
                    For Each ws As clsGameState.clsCharacterState.clsWalkState In chs.lWalks
                        .WriteStartElement("Walk");
                        .WriteElementString("Status", ws.Status.ToString);
                        .WriteElementString("Timer", ws.TimerToEndOfWalk.ToString);
                        .WriteEndElement();
                    Next;
                    For Each sSeen As String In chs.lSeenKeys
                        .WriteElementString("Seen", sSeen);
                    Next;
                    For Each sPropKey As String In chs.htblProperties.Keys
                        private clsGameState.clsCharacterState.clsStateProperty props = CType(chs.htblProperties(sPropKey), clsGameState.clsCharacterState.clsStateProperty);
                        .WriteStartElement("Property");
                        .WriteElementString("Key", sPropKey);
                        .WriteElementString("Value", props.Value);
                        .WriteEndElement();
                    Next;
                    'If chs.Location.ObjectKey <> "" Then .WriteElementString("ObjectKey", chs.Location.ObjectKey)
                    For Each sDisplayedKey As String In chs.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblVariableStates.Keys
                    private clsGameState.clsVariableState vars = CType(state.htblVariableStates(sKey), clsGameState.clsVariableState);
                    .WriteStartElement("Variable");
                    .WriteElementString("Key", sKey);

                    private clsVariable v = Adventure.htblVariables(sKey);
                    for (int i = 0; i <= vars.Value.Length - 1; i++)
                    {
                        if (v.Type = clsVariable.VariableTypeEnum.Numeric)
                        {
                            If vars.Value(i) <> "0" Then .WriteElementString("Value_" + i, vars.Value(i))
                        Else
                            If vars.Value(i) <> "" Then .WriteElementString("Value_" + i, vars.Value(i))
                        }
                    Next;
                    '.WriteElementString("Value", vars.Value)
                    For Each sDisplayedKey As String In vars.htblDisplayedDescriptions.Keys
                        .WriteElementString("Displayed", sDisplayedKey);
                    Next;
                    .WriteEndElement();
                Next;

                For Each sKey As String In state.htblGroupStates.Keys
                    private clsGameState.clsGroupState grps = CType(state.htblGroupStates(sKey), clsGameState.clsGroupState);
                    .WriteStartElement("Group");
                    .WriteElementString("Key", sKey);
                    For Each sMember As String In grps.lstMembers
                        .WriteElementString("Member", sMember);
                    Next;
                    .WriteEndElement();
                Next;

                .WriteElementString("Turns", Adventure.Turns.ToString);

                .WriteEndElement() ' Game;
                .WriteEndDocument();
                .Flush();

                private New MemoryStream outStream;
                private New ZOutputStream(outStream, zlibConst.Z_BEST_COMPRESSION) zStream;

                try
                {
                    stmMemory.Position = 0;
                    CopyStream(stmMemory, zStream);
                }
                finally
                {
                    zStream.Close();
                    bData = outStream.ToArray
                    outStream.Close();
                }
            }

            private New IO.FileStream(sFilePath, FileMode.Create) stmFile;
            private New BinaryWriter(stmFile) bw;

            bw.Write(bData);
            bw.Close();
            stmFile.Close();

            return true;

        }
        catch (Exception ex)
        {
            ErrMsg("Error saving game state", ex);
            return false;
        }

    }
#endif

#if Generator
    public bool SaveFileToStream(IO.Stream stm, bool bCompress, long lLength = 0, bool bBlorb = false, bool bSelectedOnly = false)
    {

        private bool bAppend = lLength > 0;

        If ! Save500(bCompress, bBlorb, bSelectedOnly) Then Return false

        bw = New BinaryWriter(stm)

        'system.Text.Encoding.
        If bCompress Then bw.Write(Dencode("Version " + DoubleToString(dVersion, "0.00"), 0)) ' dVersion.ToString("0.00"), 0))
        private int iBabelLength = 0;
        if (bCompress && Not bBlorb)
        {
            private string sCoverImage = Adventure.CoverFilename;
            Adventure.CoverFilename = null ' Because we don't want to save Cover info in compressed TAF;
            private byte[] bBabel = System.Text.Encoding.UTF8.GetBytes(Adventure.BabelTreatyInfo.ToString(false));
            Adventure.CoverFilename = sCoverImage;
            iBabelLength = bBabel.Length + 4
            private string sLen = Hex(iBabelLength - 4);
            while (sLen.Length < 4)
            {
                sLen = "0" & sLen
            }
            bw.Write(System.Text.Encoding.UTF8.GetBytes(sLen));
            bw.Write(bBabel);
        }
        if (bBlorb)
        {
            bw.Write(System.Text.Encoding.UTF8.GetBytes("0000"));
            iBabelLength = 4
        }
        bw.Write(bAdventure);
        if (bCompress)
        {
            If Adventure.Password == null Then Adventure.Password = "        "
            while (Adventure.Password.Length < 8)
            {
                Adventure.Password &= " ";
            }
            bw.Write(Dencode(sLeft(Adventure.Password, 4) + "Wild" + sMid(Adventure.Password, 5, 4), bAdventure.Length + 13 + iBabelLength)) ' For compatibility;
            bw.Write(New Byte() {&HD, &HA});
        }

        if (bAppend)
        {
            ' Write the size of Runner to the end of the file for reference later
            bw.Write(String.Format("{0:X6}", lLength));
        }

        return true;

    }


    ' Assumes overwrite checking already done etc
    public bool SaveFile(string sFilename, bool bCompress, long lLength = 0, bool bSelectedOnly = false)
    {

        private IO.FileStream stmNewFile = null;
        private bool bAppend = lLength > 0;

        Cursor.Current = Cursors.WaitCursor;

        if (Not bAppend)
        {
            if (bCompress)
            {
                If ! sFilename.ToLower.EndsWith(".taf") Then sFilename &= ".taf"
            Else
                If ! (sFilename.ToLower.EndsWith(".amf") || sFilename.ToLower.EndsWith(".xml")) Then sFilename &= ".amf"
            }
        }

        try
        {

            if (bAppend)
            {
                stmNewFile = New IO.FileStream(sFilename, FileMode.Append)
            Else
                stmNewFile = New IO.FileStream(sFilename, FileMode.Create)
            }

            if (SaveFileToStream(stmNewFile, bCompress, bSelectedOnly:=bSelectedOnly))
            {

                With Adventure;
                    .dVersion = dVersion ' 5;
                    if (bCompress)
                    {
                        .Filename = Path.GetFileName(sFilename);
                        .FullPath = sFilename;
                    }
                }

                fGenerator.StatusBar1.Panels(1).Text = "File size: " + GetFileSize(sFilename);
                fGenerator.Text = Adventure.Title + " - ADRIFT Developer - [" + Adventure.Filename + "]";
                fGenerator.StatusBar1.Panels(0).Text = "File version: " + Adventure.Version;
                If bCompress Then Adventure.Changed = false
                return true;
            Else
                return false;
            }

        }
        catch (UnauthorizedAccessException exUAE)
        {
            ErrMsg("You do not have permission to save """ + sFilename + """." + vbCrLf + "Please check that it is not read only, and that you have permissions for that directory.");
            return false;
        }
        catch (IOException exIO)
        {
            if (exIO.Message.Contains("The process cannot access the file"))
            {
                ErrMsg("Error accessing file """ + sFilename + """.  Please check it is not open in another application.");
            Else
                ErrMsg("I/O Error saving " + sFilename, exIO);
            }
            return false;
        }
        catch (Exception ex)
        {
            ErrMsg("Error saving " + sFilename, ex);
            return false;
        }
        finally
        {
            If ! bw == null Then bw.Close()
            bw = Nothing
            If ! stmNewFile == null Then stmNewFile.Close()
            stmNewFile = Nothing
            Cursor.Current = Cursors.Arrow;
        }

    }


    public bool SaveiFictionRecord(string sFilename)
    {

        try
        {
            private New IO.StreamWriter(sFilename, False, System.Text.Encoding.UTF8) stmWriter;
            stmWriter.Write(Adventure.BabelTreatyInfo.ToString);
            stmWriter.Close();
            return true;
        }
        catch (Exception ex)
        {
            ErrMsg("Error saving iFiction record", ex);
            return false;
        }

    }



    public bool SaveBlorb(string sFilename, IO.FileMode mode = FileMode.Create)
    {

        private IO.FileStream stmNewFile = null;

        try
        {
            Cursor.Current = Cursors.WaitCursor;

            stmNewFile = New IO.FileStream(sFilename, mode)

            private New clsBlorb blorbSave;
            blorbSave.SaveBlorb(stmNewFile);

            return true;

        }
        catch (UnauthorizedAccessException exUAE)
        {
            ErrMsg("You do not have permission to save """ + sFilename + """." + vbCrLf + "Please check that it is not read only, and that you have permissions for that directory.");
            return false;
        }
        catch (IOException exIO)
        {
            if (exIO.Message.Contains("The process cannot access the file"))
            {
                ErrMsg("Error accessing file """ + sFilename + """.  Please check it is not open in another application.");
            Else
                ErrMsg("I/O Error saving " + sFilename, exIO);
            }
            return false;
        }
        catch (Exception ex)
        {
            ErrMsg("Error saving Blorb " + sFilename, ex);
            return false;
        }
        finally
        {
            If bw != null Then bw.Close()
            bw = Nothing
            If stmNewFile != null Then stmNewFile.Close()
            stmNewFile.Dispose();
            stmNewFile = Nothing
            Cursor.Current = Cursors.Arrow;
        }

    }



    public clsBlorb LoadBlorb(string sFilename)
    {

        private IO.FileStream stmBlorb = null;
        private New clsBlorb blorbNew;

        try
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            stmBlorb = New IO.FileStream(sFilename, FileMode.Open)
            if (Not blorbNew.LoadBlorb(stmBlorb, sFilename))
            {
                Cursor.Current = Cursors.Arrow;
                ErrMsg("Error loading blorb " + sFilename);
            Else
                return blorbNew;
            }

        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (stmBlorb IsNot null)
            {
                stmBlorb.Close();
                stmBlorb.Dispose();
            }
            Cursor.Current = Cursors.Arrow;
        }

        return null;

    }

#endif


    internal ActionArrayList LoadActions(XmlElement nodContainerXML)
    {

        private New ActionArrayList Actions;

        If nodContainerXML.Item("Actions") == null Then Return Actions

        For Each nod As XmlNode In nodContainerXML.Item("Actions").ChildNodes
            if (TypeOf nod Is XmlElement)
            {
                private XmlElement nodAct = CType(nod, XmlElement);
                'For Each nodAct As XmlElement In nodContainerXML.Item("Actions").ChildNodes
                'For Each nodAct As XmlElement In nodContainerXML.GetElementsByTagName("Actions") 'nodContainerXML.SelectNodes("Actions") '
                private New clsAction act;
                private string sAct;
                private string sType = null;
                'If Not nodAct.Item("EndGame") Is Nothing Then sType = "EndGame"
                'If Not nodAct.Item("MoveCharacter") Is Nothing Then sType = "MoveCharacter"
                'If Not nodAct.Item("MoveObject") Is Nothing Then sType = "MoveObject"
                'If Not nodAct.Item("SetProperty") Is Nothing Then sType = "SetProperty"
                'If Not nodAct.Item("Score") Is Nothing Then sType = "Score"
                'If Not nodAct.Item("SetTasks") Is Nothing Then sType = "SetTasks"
                'If Not nodAct.Item("SetVariable") Is Nothing Then sType = "SetVariable"
                sType = nodAct.Name

                If sType == null Then Return Actions

                sAct = nodAct.InnerText
                Dim sElements() As String = Split(sAct, " ")
                switch (sType)
                {
                    case "EndGame":
                        {
                        act.eItem = clsAction.ItemEnum.EndGame;
                        act.eEndgame = EnumParseEndGame(sElements(0));

                    case "MoveObject":
                    case "AddObjectToGroup":
                    case "RemoveObjectFromGroup":
                        {
                        switch (sType)
                        {
                            case "MoveObject":
                                {
                                act.eItem = clsAction.ItemEnum.MoveObject;
                            case "AddObjectToGroup":
                                {
                                act.eItem = clsAction.ItemEnum.AddObjectToGroup;
                            case "RemoveObjectFromGroup":
                                {
                                act.eItem = clsAction.ItemEnum.RemoveObjectFromGroup;
                        }

                        If dFileVersion <= 5.000016 Then ' Upgrade previous file format
                            act.sKey1 = sElements(0);
                            act.eMoveObjectWhat = clsAction.MoveObjectWhatEnum.Object;
                            switch (act.sKey1)
                            {
                                case "AllHeldObjects":
                                    {
                                    act.eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingHeldBy;
                                    act.sKey1 = THEPLAYER;
                                case "AllWornObjects":
                                    {
                                    act.eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingWornBy;
                                    act.sKey1 = THEPLAYER;
                                default:
                                    {
                                    ' Leave as is
                            }
                            act.eMoveObjectTo = EnumParseMoveObject(sElements(1));
                            act.sKey2 = sElements(2);
                        Else
                            act.eMoveObjectWhat = (sElements(0)([Enum].Parse(GetType(clsAction.MoveObjectWhatEnum))), clsAction.MoveObjectWhatEnum);
                            act.sKey1 = sElements(1);
                            if (sElements.Length > 4)
                            {
                                for (int iEl = 2; iEl <= sElements.Length - 3; iEl++)
                                {
                                    act.sPropertyValue &= sElements(iEl);
                                    If iEl < sElements.Length - 3 Then act.sPropertyValue &= " "
                                Next;
                            }
                            switch (act.eItem)
                            {
                                case clsAction.ItemEnum.AddObjectToGroup:
                                    {
                                    act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToGroup;
                                case clsAction.ItemEnum.RemoveObjectFromGroup:
                                    {
                                    act.eMoveObjectTo = clsAction.MoveObjectToEnum.FromGroup;
                                case clsAction.ItemEnum.MoveObject:
                                    {
                                    act.eMoveObjectTo = (sElements(sElements.Length - 2)([Enum].Parse(GetType(clsAction.MoveObjectToEnum))), clsAction.MoveObjectToEnum);
                            }
                            act.sKey2 = sElements(sElements.Length - 1);
                        }

                    case "MoveCharacter":
                    case "AddCharacterToGroup":
                    case "RemoveCharacterFromGroup":
                        {
                        switch (sType)
                        {
                            case "MoveCharacter":
                                {
                                act.eItem = clsAction.ItemEnum.MoveCharacter;
                            case "AddCharacterToGroup":
                                {
                                act.eItem = clsAction.ItemEnum.AddCharacterToGroup;
                            case "RemoveCharacterFromGroup":
                                {
                                act.eItem = clsAction.ItemEnum.RemoveCharacterFromGroup;
                        }

                        If dFileVersion <= 5.000016 Then ' Upgrade previous file format
                            act.eItem = clsAction.ItemEnum.MoveCharacter;
                            act.sKey1 = sElements(0);
                            act.eMoveCharacterTo = EnumParseMoveCharacter(sElements(1));
                            act.sKey2 = sElements(2);
                            if (act.eMoveCharacterTo = clsAction.MoveCharacterToEnum.InDirection && IsNumeric(act.sKey2))
                            {
                                act.sKey2 = WriteEnum((DirectionsEnum)(SafeInt(act.sKey2)));
                            }
                        Else
                            act.eMoveCharacterWho = (sElements(0)([Enum].Parse(GetType(clsAction.MoveCharacterWhoEnum))), clsAction.MoveCharacterWhoEnum);
                            act.sKey1 = sElements(1);
                            if (sElements.Length > 4)
                            {
                                for (int iEl = 2; iEl <= sElements.Length - 3; iEl++)
                                {
                                    act.sPropertyValue &= sElements(iEl);
                                    If iEl < sElements.Length - 3 Then act.sPropertyValue &= " "
                                Next;
                            }
                            switch (act.eItem)
                            {
                                case clsAction.ItemEnum.AddCharacterToGroup:
                                    {
                                    act.eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToGroup;
                                case clsAction.ItemEnum.RemoveCharacterFromGroup:
                                    {
                                    act.eMoveCharacterTo = clsAction.MoveCharacterToEnum.FromGroup;
                                case clsAction.ItemEnum.MoveCharacter:
                                    {
                                    act.eMoveCharacterTo = (sElements(sElements.Length - 2)([Enum].Parse(GetType(clsAction.MoveCharacterToEnum))), clsAction.MoveCharacterToEnum);
                            }
                            act.sKey2 = sElements(sElements.Length - 1);
                        }

                    case "AddLocationToGroup":
                    case "RemoveLocationFromGroup":
                        {
                        switch (sType)
                        {
                            case "AddLocationToGroup":
                                {
                                act.eItem = clsAction.ItemEnum.AddLocationToGroup;
                            case "RemoveLocationFromGroup":
                                {
                                act.eItem = clsAction.ItemEnum.RemoveLocationFromGroup;
                        }

                        act.eMoveLocationWhat = (sElements(0)([Enum].Parse(GetType(clsAction.MoveLocationWhatEnum))), clsAction.MoveLocationWhatEnum);
                        act.sKey1 = sElements(1);
                        if (sElements.Length > 4)
                        {
                            for (int iEl = 2; iEl <= sElements.Length - 3; iEl++)
                            {
                                act.sPropertyValue &= sElements(iEl);
                                If iEl < sElements.Length - 3 Then act.sPropertyValue &= " "
                            Next;
                        }
                        switch (act.eItem)
                        {
                            case clsAction.ItemEnum.AddLocationToGroup:
                                {
                                act.eMoveLocationTo = clsAction.MoveLocationToEnum.ToGroup;
                            case clsAction.ItemEnum.RemoveLocationFromGroup:
                                {
                                act.eMoveLocationTo = clsAction.MoveLocationToEnum.FromGroup;
                        }
                        act.sKey2 = sElements(sElements.Length - 1);

                    case "SetProperty":
                        {
                        act.eItem = clsAction.ItemEnum.SetProperties;
                        act.sKey1 = sElements(0);
                        act.sKey2 = sElements(1);
                        'act.StringValue = sElements(2)
                        private string sValue = "";
                        for (int i = 2; i <= sElements.Length - 1; i++)
                        {
                            sValue &= sElements(i);
                            If i < sElements.Length - 1 Then sValue &= " "
                        Next;
                        act.StringValue = sValue;
                        act.sPropertyValue = sValue;
                    case "Score":
                        {
                        'rest.eItem = clsRestriction.ItemEnum.Character
                        'rest.sKey1 = sElements(0)
                        'rest.eMust = CType([Enum].Parse(GetType(clsRestriction.MustEnum), sElements(1)), clsRestriction.MustEnum)
                        'rest.eCharacter = CType([Enum].Parse(GetType(clsRestriction.CharacterEnum), sElements(2)), clsRestriction.CharacterEnum)
                        'rest.sKey2 = sElements(3)
                    case "SetTasks":
                        {
                        act.eItem = clsAction.ItemEnum.SetTasks;
                        private int iStartIndex = 0;
                        private int iEndIndex = 1;
                        if (sElements(0) = "FOR")
                        {
                            ' sElements(1) = %Loop%
                            ' sElements(2) = '='
                            act.IntValue = CInt(sElements(3));
                            ' sElements(4) = TO
                            act.sPropertyValue = sElements(5);
                            ' sElements(6) = :
                            iStartIndex = 7
                            iEndIndex = 4
                        }
                        act.eSetTasks = EnumParseSetTask(sElements(iStartIndex));
                        act.sKey1 = sElements(iStartIndex + 1);
                        for (int iElement = iStartIndex + 2; iElement <= sElements.Length - iEndIndex; iElement++)
                        {
                            act.StringValue &= sElements(iElement);
                        Next;
                        if (act.StringValue IsNot null)
                        {
#if Generator
                            act.StringValue = act.StringValue;
#else
                            ' Simplify Runner so it only has to deal with multiple, or specific refs
                            act.StringValue = FixInitialRefs(act.StringValue);
#endif
                            If act.StringValue.StartsWith("(") Then act.StringValue = sRight(act.StringValue, act.StringValue.Length - 1)
                            If act.StringValue.EndsWith(")") Then act.StringValue = sLeft(act.StringValue, act.StringValue.Length - 1)
                        }

                    case "SetVariable":
                    case "IncVariable":
                    case "DecVariable":
                    case "ExecuteTask":
                        {
                        switch (sType)
                        {
                            case "SetVariable":
                                {
                                act.eItem = clsAction.ItemEnum.SetVariable;
                            case "IncVariable":
                                {
                                act.eItem = clsAction.ItemEnum.IncreaseVariable;
                            case "DecVariable":
                                {
                                act.eItem = clsAction.ItemEnum.DecreaseVariable;
                        }

                        if (sElements(0) = "FOR")
                        {
                            act.eVariables = clsAction.VariablesEnum.Loop;
                            ' sElements(1) = %Loop%
                            ' sElements(2) = '='
                            act.IntValue = CInt(sElements(3));
                            ' sElements(4) = TO
                            act.sKey2 = sElements(5);
                            ' sElements(6) = :
                            ' sElements(7) = SET
                            act.sKey1 = sElements(8).Split("["c)(0);
                            ' sElements(9) = '='
                            for (int iElement = 10; iElement <= sElements.Length - 4; iElement++)
                            {
                                act.StringValue &= sElements(iElement);
                                If iElement < sElements.Length - 4 Then act.StringValue &= " "
                            Next;
                        Else
                            act.eVariables = clsAction.VariablesEnum.Assignment;
                            if (sInstr(sElements(0), "[") > 0)
                            {
                                act.sKey1 = sElements(0).Split("["c)(0);
                                act.sKey2 = sElements(0).Split("["c)(1).Replace("]", "");
                            Else
                                act.sKey1 = sElements(0);
                            }
                            ' sElements(1) = '='
                            'act.StringValue = sElements(2)
                            for (int iElement = 2; iElement <= sElements.Length - 1; iElement++)
                            {
                                act.StringValue &= sElements(iElement);
                                If iElement < sElements.Length - 1 Then act.StringValue &= " "
                            Next;
                            if (dFileVersion > 5.0000321)
                            {
                                if (act.StringValue.StartsWith("""") && act.StringValue.EndsWith(""""))
                                {
                                    act.StringValue = act.StringValue.Substring(1, act.StringValue.Length - 2);
                                }
                            }
                        }

                    case "Time":
                        {
                        act.eItem = clsAction.ItemEnum.Time;
                        for (int i = 1; i <= sElements.Length - 2; i++)
                        {
                            If i > 1 Then act.StringValue &= " "
                            act.StringValue &= sElements(i);
                        Next;
                        act.StringValue = act.StringValue.Substring(1, act.StringValue.Length - 2);

                    case "Conversation":
                        {
                        act.eItem = clsAction.ItemEnum.Conversation;
                        switch (sElements(0).ToUpper)
                        {
                            case "GREET":
                            case "FAREWELL":
                                {
                                if (sElements(0).ToUpper = "GREET")
                                {
                                    act.eConversation = clsAction.ConversationEnum.Greet;
                                Else
                                    act.eConversation = clsAction.ConversationEnum.Farewell;
                                }
                                act.sKey1 = sElements(1);
                                if (sElements.Length > 2)
                                {
                                    ' sElements(2) = "with"
                                    for (int iElement = 3; iElement <= sElements.Length - 1; iElement++)
                                    {
                                        act.StringValue &= sElements(iElement);
                                        If iElement < sElements.Length - 1 Then act.StringValue &= " "
                                    Next;
                                    If act.StringValue.StartsWith("'") Then act.StringValue = act.StringValue.Substring(1)
                                    If act.StringValue.EndsWith("'") Then act.StringValue = sLeft(act.StringValue, act.StringValue.Length - 1)
                                }

                            case "ASK":
                            case "TELL":
                                {
                                if (sElements(0).ToUpper = "ASK")
                                {
                                    act.eConversation = clsAction.ConversationEnum.Ask;
                                Else
                                    act.eConversation = clsAction.ConversationEnum.Tell;
                                }
                                act.sKey1 = sElements(1);
                                ' sElements(2) = "About"
                                for (int iElement = 3; iElement <= sElements.Length - 1; iElement++)
                                {
                                    act.StringValue &= sElements(iElement);
                                    If iElement < sElements.Length - 1 Then act.StringValue &= " "
                                Next;
                                If act.StringValue.StartsWith("'") Then act.StringValue = act.StringValue.Substring(1)
                                If act.StringValue.EndsWith("'") Then act.StringValue = sLeft(act.StringValue, act.StringValue.Length - 1)

                            case "SAY":
                                {
                                act.eConversation = clsAction.ConversationEnum.Command;
                                ' sElements(0) = "Say"
                                for (int iElement = 1; iElement <= sElements.Length - 3; iElement++)
                                {
                                    act.StringValue &= sElements(iElement);
                                    If iElement < sElements.Length - 3 Then act.StringValue &= " "
                                Next;
                                If act.StringValue.StartsWith("'") Then act.StringValue = act.StringValue.Substring(1)
                                If act.StringValue.EndsWith("'") Then act.StringValue = sLeft(act.StringValue, act.StringValue.Length - 1)
                                ' sElements(len - 2) = "to"
                                act.sKey1 = sElements(sElements.Length - 1);

                            case "ENTERWITH":
                            case "LEAVEWITH":
                                {
                                if (sElements(0).ToUpper = "ENTERWITH")
                                {
                                    act.eConversation = clsAction.ConversationEnum.EnterConversation;
                                Else
                                    act.eConversation = clsAction.ConversationEnum.LeaveConversation;
                                }
                                act.sKey1 = sElements(1);
                        }
                }

                Actions.Add(act);

            }
        Next;

        return Actions;

    }


    internal RestrictionArrayList LoadRestrictions(XmlElement nodContainerXML)
    {

        private New RestrictionArrayList Restrictions;

        'If nodContainerXML.GetElementsByTagName("Restrictions").Count = 0 Then Return Restrictions ' This doesn't work as it can return the restrictions within description for example when we're just looking at task restrictions
        If nodContainerXML.SelectNodes("Restrictions").Count = 0 Then Return Restrictions

        'Debug.WriteLine(nodContainerXML.NodeType)
        private XmlElement nodRestrictions = CType(nodContainerXML.SelectNodes("Restrictions")(0), XmlElement) 'CType(nodContainerXML.GetElementsByTagName("Restrictions")(0), XmlElement);

        'Debug.WriteLine("Rest1: " & Now.ToString("mm:ss.fff"))
        For Each nodRest As XmlElement In nodRestrictions.SelectNodes("Restriction") 'GetElementsByTagName("Restriction")
            private New clsRestriction rest;
            private string sRest;
            private string sType = null;
            If ! nodRest.Item("Location") == null Then sType = "Location"
            If ! nodRest.Item("Object") == null Then sType = "Object"
            If ! nodRest.Item("Task") == null Then sType = "Task"
            If ! nodRest.Item("Character") == null Then sType = "Character"
            If ! nodRest.Item("Variable") == null Then sType = "Variable"
            If ! nodRest.Item("Property") == null Then sType = "Property"
            If nodRest.Item("Direction") != null Then sType = "Direction"
            If nodRest.Item("Expression") != null Then sType = "Expression"
            If nodRest.Item("Item") != null Then sType = "Item"

            If nodRest.Item(sType) == null Then Exit For

            sRest = nodRest.Item(sType).InnerText
            'Debug.WriteLine("Rest2: " & Now.ToString("mm:ss.fff"))
            Dim sElements() As String = Split(sRest, " ")
            switch (sType)
            {
                case "Location":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Location;
                    rest.sKey1 = sElements(0);
                    rest.eMust = EnumParseMust(sElements(1));

                    rest.eLocation = EnumParseLocation(sElements(2));
                    rest.sKey2 = sElements(3);
                case "Object":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Object;
                    rest.sKey1 = sElements(0);
                    rest.eMust = EnumParseMust(sElements(1));
                    rest.eObject = EnumParseObject(sElements(2));
                    rest.sKey2 = sElements(3);
                case "Task":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                    rest.sKey1 = sElements(0);
                    rest.eMust = EnumParseMust(sElements(1));
                    rest.eTask = clsRestriction.TaskEnum.Complete;
                case "Character":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Character;
                    rest.sKey1 = sElements(0);
                    rest.eMust = EnumParseMust(sElements(1));
                    rest.eCharacter = EnumParseCharacter(sElements(2));
                    rest.sKey2 = sElements(3);
                case "Item":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Item;
                    rest.sKey1 = sElements(0);
                    rest.eMust = EnumParseMust(sElements(1));
                    rest.eItem = EnumParseItem(sElements(2));
                    rest.sKey2 = sElements(3);
                case "Variable":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Variable;
                    rest.sKey1 = sElements(0);
                    if (rest.sKey1.Contains("[") && rest.sKey1.Contains("]"))
                    {
                        rest.sKey2 = rest.sKey1.Substring(rest.sKey1.IndexOf("[") + 1, rest.sKey1.LastIndexOf("]") - rest.sKey1.IndexOf("[") - 1);
                        rest.sKey1 = rest.sKey1.Substring(0, rest.sKey1.IndexOf("["));
                    }
                    rest.eMust = EnumParseMust(sElements(1));
                    rest.eVariable = (sElements(2)([Enum].Parse(GetType(clsRestriction.VariableEnum)).Substring(2)), clsRestriction.VariableEnum);

                    private string sValue = "";
                    for (int i = 3; i <= sElements.Length - 1; i++)
                    {
                        sValue &= sElements(i);
                        If i < sElements.Length - 1 Then sValue &= " "
                    Next;
                    if (sElements.Length = 4 && IsNumeric(sElements(3)))
                    {
                        rest.IntValue = CInt(sElements(3)) ' Integer value;
                        rest.StringValue = rest.IntValue.ToString;
                    Else
                        if (sValue.StartsWith("""") && sValue.EndsWith(""""))
                        {
                            rest.StringValue = sValue.Substring(1, sValue.Length - 2) ' String constant;
                        ElseIf sValue.StartsWith("'") && sValue.EndsWith("'") Then
                            rest.StringValue = sValue.Substring(1, sValue.Length - 2) ' Expression;
                        Else
                            rest.StringValue = sElements(3);
                            rest.IntValue = Integer.MinValue ' A key to a variable;
                        }
                    }

                case "Property":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Property;
                    rest.sKey1 = sElements(0);
                    rest.sKey2 = sElements(1);
                    rest.eMust = EnumParseMust(sElements(2));
                    private int iStartExpression = 3;
                    rest.IntValue = -1;
                    For Each eEquals As clsRestriction.VariableEnum In [Enum].GetValues(GetType(clsRestriction.VariableEnum))
                        If sElements(3) = eEquals.ToString Then rest.IntValue = CInt(eEquals)
                    Next;
                    If rest.IntValue > -1 Then iStartExpression = 4 Else rest.IntValue = CInt(clsRestriction.VariableEnum.EqualTo)
                    private string sValue = "";
                    for (int i = iStartExpression; i <= sElements.Length - 1; i++)
                    {
                        sValue &= sElements(i);
                        If i < sElements.Length - 1 Then sValue &= " "
                    Next;
                    rest.StringValue = sValue;

                case "Direction":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Direction;
                    rest.eMust = EnumParseMust(sElements(0));
                    rest.sKey1 = sElements(1);
                    rest.sKey1 = sRight(rest.sKey1, rest.sKey1.Length - 2) ' Trim off the Be;

                case "Expression":
                    {
                    rest.eType = clsRestriction.RestrictionTypeEnum.Expression;
                    rest.eMust = clsRestriction.MustEnum.Must;
                    private string sValue = "";
                    for (int i = 0; i <= sElements.Length - 1; i++)
                    {
                        sValue &= sElements(i);
                        If i < sElements.Length - 1 Then sValue &= " "
                    Next;
                    rest.StringValue = sValue;

            }
            'Debug.WriteLine("Rest3: " & Now.ToString("mm:ss.fff"))
            rest.oMessage = LoadDescription(nodRest, "Message") ' nodRest.Item("Message").InnerText;
            'Debug.WriteLine("Rest4: " & Now.ToString("mm:ss.fff"))

            'rest.StringValue = FixInitialRefs(rest.StringValue)
            Restrictions.Add(rest);

        Next;
        'Debug.WriteLine("Rest5: " & Now.ToString("mm:ss.fff"))
        'Restrictions.BracketSequence = nodContainerXML.GetElementsByTagName("BracketSequence")(0).InnerText ' x.SelectSingleNode("BracketSequence").InnerText ' nodContainerXML.SelectSingleNode("Restrictions/BracketSequence").InnerText
        Restrictions.BracketSequence = nodRestrictions.SelectNodes("BracketSequence")(0).InnerText ' GetElementsByTagName("BracketSequence")(0).InnerText;

        if (Not bAskedAboutBrackets && dFileVersion < 5.000026 && Restrictions.BracketSequence.Contains("#A#O#"))
        {
            bCorrectBracketSequences = MessageBox.Show("There was a logic correction in version 5.0.26 which means OR restrictions after AND restrictions were not evaluated.  Would you like to auto-correct these tasks?" & vbCrLf & vbCrLf & "You may not wish to do so if you have already used brackets around any OR restrictions following AND restrictions.", "Adventure Upgrade", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
            bAskedAboutBrackets = True
        }
        If bCorrectBracketSequences Then Restrictions.BracketSequence = CorrectBracketSequence(Restrictions.BracketSequence)

#if Runner
        Restrictions.BracketSequence = Restrictions.BracketSequence.Replace("[", "((").Replace("]", "))");
#endif
        'Debug.WriteLine("Rest6: " & Now.ToString("mm:ss.fff"))
        return Restrictions;

    }


    private string FixInitialRefs(string sCommand)
    {
#if Runner
        If sCommand == null Then Return ""
        return sCommand.Replace("%object%", "%object1%").Replace("%character%", "%character1%").Replace("%location%", "%location1%").Replace("%number%", "%number1%").Replace("%text%", "%text1%").Replace("%item%", "%item1%").Replace("%direction%", "%direction1%");
#else
        return sCommand;
#endif
    }


    internal void SaveRestrictions(ref xmlWriter As XmlTextWriter, RestrictionArrayList Restrictions)
    {

        If Restrictions.Count = 0 Then Exit Sub

        With xmlWriter;
            .WriteStartElement("Restrictions");
            For Each rest As clsRestriction In Restrictions
                .WriteStartElement("Restriction");
                switch (rest.eType)
                {
                    case clsRestriction.RestrictionTypeEnum.Location:
                        {
                        .WriteElementString("Location", rest.sKey1 + " " + WriteEnum(rest.eMust) + " " + WriteEnum(rest.eLocation) + " " + rest.sKey2);
                    case clsRestriction.RestrictionTypeEnum.Object:
                        {
                        .WriteElementString("Object", rest.sKey1 + " " + WriteEnum(rest.eMust) + " " + WriteEnum(rest.eObject) + " " + rest.sKey2);
                    case clsRestriction.RestrictionTypeEnum.Task:
                        {
                        .WriteElementString("Task", rest.sKey1 + " " + WriteEnum(rest.eMust) + " BeComplete");
                    case clsRestriction.RestrictionTypeEnum.Character:
                        {
                        .WriteElementString("Character", rest.sKey1 + " " + WriteEnum(rest.eMust) + " " + WriteEnum(rest.eCharacter) + " " + rest.sKey2);
                    case clsRestriction.RestrictionTypeEnum.Item:
                        {
                        .WriteElementString("Item", rest.sKey1 + " " + WriteEnum(rest.eMust) + " " + WriteEnum(rest.eItem) + " " + rest.sKey2);
                    case clsRestriction.RestrictionTypeEnum.Variable:
                        {
                        private string sValue;
                        private clsVariable.VariableTypeEnum eVarType;
                        if (rest.sKey1.StartsWith("ReferencedNumber"))
                        {
                            eVarType = clsVariable.VariableTypeEnum.Numeric
                        ElseIf rest.sKey1.StartsWith("ReferencedText") Then
                            eVarType = clsVariable.VariableTypeEnum.Text
                        Else
                            private clsVariable var = Adventure.htblVariables(rest.sKey1);
                            If var != null Then eVarType = var.Type
                        }

                        if (rest.IntValue = Integer.MinValue)
                        {
                            sValue = rest.StringValue ' Key to variable
                        Else
                            if (eVarType = clsVariable.VariableTypeEnum.Numeric)
                            {
                                if (rest.StringValue <> "" && rest.StringValue <> rest.IntValue.ToString)
                                {
                                    sValue = "'" & rest.StringValue & "'" ' Expression
                                Else
                                    sValue = rest.IntValue.ToString ' Integer
                                }
                            Else
                                sValue = """" & rest.StringValue & """" ' String constant
                            }
                        }
                        private string sVar = rest.sKey1;
                        if (rest.sKey2 <> "")
                        {
                            if (Adventure.htblVariables.ContainsKey(rest.sKey2))
                            {
                                sVar &= "[%" + Adventure.htblVariables(rest.sKey2).Name + "%]";
                            Else
                                sVar &= "[" + rest.sKey2 + "]";
                            }
                        }
                        .WriteElementString("Variable", sVar + " " + WriteEnum(rest.eMust) + " Be" + rest.eVariable.ToString + " " + sValue);

                    case clsRestriction.RestrictionTypeEnum.Property:
                        {
                        private string sEquals = "";
                        If rest.IntValue > -1 Then sEquals = (clsRestriction.VariableEnum)(rest.IntValue).ToString + " "
                        .WriteElementString("Property", rest.sKey1 + " " + rest.sKey2 + " " + WriteEnum(rest.eMust) + " " + sEquals + rest.StringValue);
                    case clsRestriction.RestrictionTypeEnum.Direction:
                        {
                        .WriteElementString("Direction", WriteEnum(rest.eMust) + " Be" + rest.sKey1);
                    case clsRestriction.RestrictionTypeEnum.Expression:
                        {
                        .WriteElementString("Expression", rest.StringValue);
                }
                If rest.oMessage.Count > 0 Then SaveDescription(xmlWriter, rest.oMessage, "Message") ' .WriteElementString("Message", rest.sMessage)
                .WriteEndElement() ' Restriction;
            Next;
            .WriteElementString("BracketSequence", Restrictions.BracketSequence);
            .WriteEndElement();
        }


    }


    internal void SaveActions(ref xmlWriter As XmlTextWriter, ActionArrayList Actions)
    {

        If Actions.Count = 0 Then Exit Sub

        With xmlWriter;
            .WriteStartElement("Actions");
            For Each act As clsAction In Actions
                '.WriteStartElement("Action")
                '.WriteElementString("Type", act.eItem.ToString)
                switch (act.eItem)
                {
                    case clsAction.ItemEnum.EndGame:
                        {
                        .WriteElementString("EndGame", WriteEnum(act.eEndgame));
                    case clsAction.ItemEnum.MoveCharacter:
                    case clsAction.ItemEnum.AddCharacterToGroup:
                    case clsAction.ItemEnum.RemoveCharacterFromGroup:
                        {
                        '.WriteElementString("MoveCharacter", act.sKey1 & " " & WriteEnum(act.eMoveCharacterTo) & " " & act.sKey2)
                        .WriteElementString(act.eItem.ToString, act.eMoveCharacterWho.ToString + " " + act.sKey1 + " " + IIf(act.sPropertyValue <> "", act.sPropertyValue + " ", "").ToString + WriteEnum(act.eMoveCharacterTo) + " " + act.sKey2);
                    case clsAction.ItemEnum.MoveObject:
                    case clsAction.ItemEnum.AddObjectToGroup:
                    case clsAction.ItemEnum.RemoveObjectFromGroup:
                        {
                        .WriteElementString(act.eItem.ToString, act.eMoveObjectWhat.ToString + " " + act.sKey1 + " " + IIf(act.sPropertyValue <> "", act.sPropertyValue + " ", "").ToString + WriteEnum(act.eMoveObjectTo) + " " + act.sKey2);
                    case clsAction.ItemEnum.AddLocationToGroup:
                    case clsAction.ItemEnum.RemoveLocationFromGroup:
                        {
                        .WriteElementString(act.eItem.ToString, act.eMoveLocationWhat.ToString + " " + act.sKey1 + " " + IIf(act.sPropertyValue <> "", act.sPropertyValue + " ", "").ToString + WriteEnum(act.eMoveLocationTo) + " " + act.sKey2);
                    case clsAction.ItemEnum.SetProperties:
                        {
                        .WriteElementString("SetProperty", act.sKey1 + " " + act.sKey2 + " " + act.sPropertyValue);
                    case clsAction.ItemEnum.SetTasks:
                        {
                        private string sAction = "";
                        If act.sPropertyValue <> "" Then sAction = "FOR Loop = " + act.IntValue + " TO " + act.sPropertyValue + " : "
                        private string sParams = "";
                        If act.StringValue <> "" Then sParams = " (" + act.StringValue + ")"
                        sAction &= WriteEnum(act.eSetTasks) + " " + act.sKey1 + sParams;
                        If act.sPropertyValue <> "" Then sAction &= " : NEXT Loop"
                        .WriteElementString("SetTasks", sAction);
                    case clsAction.ItemEnum.SetVariable:
                    case clsAction.ItemEnum.IncreaseVariable:
                    case clsAction.ItemEnum.DecreaseVariable:
                        {
                        private clsVariable var = Adventure.htblVariables(act.sKey1);
                        private string sAction;
                        if (act.eVariables = clsAction.VariablesEnum.Assignment)
                        {
                            sAction = act.sKey1
                            if (Not act.sKey2 Is null)
                            {
                                sAction &= "[" + act.sKey2 + "]";
                            }
                            'If var.Type = clsVariable.VariableTypeEnum.Numeric Then
                            '    sAction &= " = " & act.StringValue
                            'Else
                            '    sAction &= " = " & act.StringValue ' This should contain an expression, which may be quoted
                            'End If
                            sAction &= " = """ + act.StringValue + """";
                        Else
                            sAction = "FOR Loop = " & act.IntValue & " TO " & act.sKey2 & " : SET " & act.sKey1 & "[Loop] = " & act.StringValue & " : NEXT Loop"
                        }
                        switch (act.eItem)
                        {
                            case clsAction.ItemEnum.SetVariable:
                                {
                                .WriteElementString("SetVariable", sAction);
                            case clsAction.ItemEnum.IncreaseVariable:
                                {
                                .WriteElementString("IncVariable", sAction);
                            case clsAction.ItemEnum.DecreaseVariable:
                                {
                                .WriteElementString("DecVariable", sAction);
                        }
                    case clsAction.ItemEnum.Conversation:
                        {
                        switch (act.eConversation)
                        {
                            case clsAction.ConversationEnum.Greet:
                                {
                                .WriteElementString("Conversation", "Greet " + act.sKey1 + IIf(act.StringValue <> "", " With '" + act.StringValue + "'", "").ToString);
                            case clsAction.ConversationEnum.Ask:
                                {
                                .WriteElementString("Conversation", "Ask " + act.sKey1 + " About '" + act.StringValue + "'");
                            case clsAction.ConversationEnum.Tell:
                                {
                                .WriteElementString("Conversation", "Tell " + act.sKey1 + " About '" + act.StringValue + "'");
                            case clsAction.ConversationEnum.Command:
                                {
                                .WriteElementString("Conversation", "Say '" + act.StringValue + "' To " + act.sKey1);
                            case clsAction.ConversationEnum.Farewell:
                                {
                                .WriteElementString("Conversation", "Farewell " + act.sKey1 + IIf(act.StringValue <> "", " With '" + act.StringValue + "'", "").ToString);
                            case clsAction.ConversationEnum.EnterConversation:
                                {
                                .WriteElementString("Conversation", "EnterWith " + act.sKey1);
                            case clsAction.ConversationEnum.LeaveConversation:
                                {
                                .WriteElementString("Conversation", "LeaveWith " + act.sKey1);
                        }
                    case clsAction.ItemEnum.Time:
                        {
                        .WriteElementString("Time", "Skip """ + act.StringValue + """ turns");
                }
                '.WriteEndElement()
            Next;
            .WriteEndElement();
        }


    }



#if Generator
    internal List<string> ExportList;

    private bool IsItemSelected(string sKey)
    {

        if (ExportList Is null)
        {
            For Each folder As frmFolder In fGenerator.MDIFolders
                For Each item As Infragistics.Win.UltraWinListView.UltraListViewItem In folder.Folder.lstContents.SelectedItems
                    If item.SubItems(3).Text = sKey Then Return true
                Next;
            Next;
        Else
            return ExportList.Contains(sKey);
        }

        return false;

    }

    internal bool Save500(bool bCompress, bool bBlorb, bool bSelectedOnly = false)
    {

        private New MemoryStream stmMemory;
        private New System.Xml.XmlTextWriter(stmMemory, System.Text.Encoding.UTF8) xmlWriter;
        private clsAdventure a = Adventure ' Shorter name;

        if (Not IsRegistered && bCompress)
        {
            ' Make sure we have no more than 25 non-library items.
            private int iNonLibrary = 0;
            For Each itm As clsItem In Adventure.dictAllItems.Values
                If ! itm.IsLibrary Then iNonLibrary += 1
            Next;
            if (iNonLibrary > 25)
            {
                ErrMsg("Sorry.  The unregistered version of ADRIFT will only allow you to save adventures with a maximum of 25 items." + vbCrLf _;
 + "So that you don't lose any work, try exporting the file as a module.");
                return false;
            }
        }

        With xmlWriter;
            .Formatting = Xml.Formatting.Indented;
            if (bCompress)
            {
                .Indentation = 0;
            Else
                .Indentation = 4;
            }

            .WriteStartDocument();

            .WriteStartElement("Adventure");
            private Version version = new Version(Application.ProductVersion);
            .WriteElementString("Version", version.Major + "." + Format(version.Minor, "000") + Format(version.Build, "000") + version.Revision);
            .WriteElementString("LastUpdated", SetDate(Now)) ' Now.ToString("yyyy-MM-dd HH:mm:ss"));
            .WriteElementString("Title", a.Title);
            .WriteElementString("Author", a.Author);

            If a.DefaultFontName <> "Arial" Then .WriteElementString("FontName", a.DefaultFontName)
            If a.DefaultFontSize <> 12 Then .WriteElementString("FontSize", a.DefaultFontSize.ToString)
            If a.DeveloperDefaultBackgroundColour <> Color.FromArgb(DEFAULT_BACKGROUNDCOLOUR) Then .WriteElementString("BackgroundColour", ColorTranslator.ToOle(a.DeveloperDefaultBackgroundColour).ToString)
            If a.DeveloperDefaultInputColour <> Color.FromArgb(DEFAULT_INPUTCOLOUR) Then .WriteElementString("InputColour", ColorTranslator.ToOle(a.DeveloperDefaultInputColour).ToString)
            If a.DeveloperDefaultOutputColour <> Color.FromArgb(DEFAULT_OUTPUTCOLOUR) Then .WriteElementString("OutputColour", ColorTranslator.ToOle(a.DeveloperDefaultOutputColour).ToString)
            If a.DeveloperDefaultLinkColour <> Color.FromArgb(DEFAULT_LINKCOLOUR) Then .WriteElementString("LinkColour", ColorTranslator.ToOle(a.DeveloperDefaultLinkColour).ToString)
            If a.sUserStatus <> "" Then .WriteElementString("UserStatus", a.sUserStatus)
            if (Not bCompress && a.BabelTreatyInfo IsNot null && a.BabelTreatyInfo.Stories.Length = 1 && a.BabelTreatyInfo.Stories(0).Bibliographic IsNot null && a.BabelTreatyInfo.Stories(0).Bibliographic.Description <> "")
            {
                .WriteElementString("Description", a.BabelTreatyInfo.Stories(0).Bibliographic.Description);
            }
            '.WriteElementString("Introduction", a.Introduction)
            SaveDescription(xmlWriter, a.Introduction, "Introduction");

            ' We don't want to save these settings in any modules
            if (bCompress)
            {
                .WriteElementString("ShowFirstLocation", SafeInt(a.ShowFirstRoom).ToString);
                .WriteElementString("ShowExits", SafeInt(a.ShowExits).ToString);
                .WriteElementString("EnableMenu", SafeInt(a.EnableMenu).ToString);
                If ! a.EnableDebugger Then .WriteElementString("EnableDebugger", SafeInt(a.EnableDebugger).ToString)
                .WriteElementString("Elapsed", a.iElapsed.ToString);
            }

            If Adventure.CoverFilename <> "" Then .WriteElementString("Cover", Adventure.CoverFilename)
            SaveDescription(xmlWriter, a.WinningText, "EndGameText");
            If a.TaskExecution <> clsAdventure.TaskExecutionEnum.HighestPriorityTask Then .WriteElementString("TaskExecution", a.TaskExecution.ToString)
            If a.WaitTurns <> 3 Then .WriteElementString("WaitTurns", a.WaitTurns.ToString)
            If a.KeyPrefix <> "" Then .WriteElementString("KeyPrefix", a.KeyPrefix)

            If a.sDirectionsRE(DirectionsEnum.North) <> "North/N" Then .WriteElementString("DirectionNorth", a.sDirectionsRE(DirectionsEnum.North))
            If a.sDirectionsRE(DirectionsEnum.NorthEast) <> "NorthEast/NE/North-East/N-E" Then .WriteElementString("DirectionNorthEast", a.sDirectionsRE(DirectionsEnum.NorthEast))
            If a.sDirectionsRE(DirectionsEnum.East) <> "East/E" Then .WriteElementString("DirectionEast", a.sDirectionsRE(DirectionsEnum.East))
            If a.sDirectionsRE(DirectionsEnum.SouthEast) <> "SouthEast/SE/South-East/S-E" Then .WriteElementString("DirectionSouthEast", a.sDirectionsRE(DirectionsEnum.SouthEast))
            If a.sDirectionsRE(DirectionsEnum.South) <> "South/S" Then .WriteElementString("DirectionSouth", a.sDirectionsRE(DirectionsEnum.South))
            If a.sDirectionsRE(DirectionsEnum.SouthWest) <> "SouthWest/SW/South-West/S-W" Then .WriteElementString("DirectionSouthWest", a.sDirectionsRE(DirectionsEnum.SouthWest))
            If a.sDirectionsRE(DirectionsEnum.West) <> "West/W" Then .WriteElementString("DirectionWest", a.sDirectionsRE(DirectionsEnum.West))
            If a.sDirectionsRE(DirectionsEnum.NorthWest) <> "NorthWest/NW/North-West/N-W" Then .WriteElementString("DirectionNorthWest", a.sDirectionsRE(DirectionsEnum.NorthWest))
            If a.sDirectionsRE(DirectionsEnum.Up) <> "Up/U" Then .WriteElementString("DirectionUp", a.sDirectionsRE(DirectionsEnum.Up))
            If a.sDirectionsRE(DirectionsEnum.Down) <> "Down/D" Then .WriteElementString("DirectionDown", a.sDirectionsRE(DirectionsEnum.Down))
            If a.sDirectionsRE(DirectionsEnum.In) <> "In/Inside" Then .WriteElementString("DirectionIn", a.sDirectionsRE(DirectionsEnum.In))
            If a.sDirectionsRE(DirectionsEnum.Out) <> "Out/O/Outside" Then .WriteElementString("DirectionOut", a.sDirectionsRE(DirectionsEnum.Out))

            For Each folder As clsFolder In a.dictFolders.Values
                if (Not bSelectedOnly || IsItemSelected(folder.Key))
                {
                    .WriteStartElement("Folder");
                    .WriteElementString("Key", folder.Key);
                    .WriteElementString("Name", folder.Name);
                    For Each sKey As String In folder.Members
                        .WriteElementString("Member", sKey);
                    Next;
                    If ! folder.Expanded Then .WriteElementString("Expanded", "0")
                    if (folder.Size.Width > 0)
                    {
                        .WriteElementString("Height", folder.Size.Height.ToString);
                        .WriteElementString("Width", folder.Size.Width.ToString);
                    }
                    If folder.Visible Then .WriteElementString("Visible", SafeInt(folder.Visible).ToString)
                    .WriteElementString("X", folder.Location.X.ToString);
                    .WriteElementString("Y", folder.Location.Y.ToString);
                    If folder.ViewType <> Infragistics.Win.UltraWinListView.UltraListViewStyle.Details Then .WriteElementString("Type", folder.ViewType.ToString)
                    If folder.SortColumn <> -1 Then .WriteElementString("SortColumn", folder.SortColumn.ToString)
                    If folder.SortDirection <> Infragistics.Win.UltraWinListView.Sorting.None Then .WriteElementString("SortDirection", folder.SortDirection.ToString)
                    If folder.GroupBy <> 0 Then .WriteElementString("GroupBy", folder.GroupBy.ToString)
                    If folder.GroupDirection <> Infragistics.Win.UltraWinListView.Sorting.None Then .WriteElementString("GroupDirection", folder.GroupDirection.ToString)
                    If folder.ShowCreatedDate Then .WriteElementString("ShowCreatedDate", "1")
                    If folder.ShowModifiedDate Then .WriteElementString("ShowModifiedDate", "1")
                    If folder.ShowType Then .WriteElementString("ShowType", "1")
                    If folder.ShowKey Then .WriteElementString("ShowKey", "1")
                    If folder.ShowPriority Then .WriteElementString("ShowPriority", "1")

                    If folder.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(folder.LastUpdated));
                    .WriteElementString("Created", SetDate(folder.Created));

                    .WriteEndElement() ' Folder;
                }

            Next;

            For Each prop As clsProperty In a.htblAllProperties.Values
                if (Not bSelectedOnly || IsItemSelected(prop.Key))
                {
                    .WriteStartElement("Property");
                    .WriteElementString("Key", prop.Key);
                    .WriteElementString("Description", prop.Description);
                    If prop.Mandatory Then .WriteElementString("Mandatory", SafeInt(prop.Mandatory).ToString)
                    .WriteElementString("PropertyOf", WriteEnum(prop.PropertyOf));
                    .WriteElementString("Type", WriteEnum(prop.Type));
                    switch (prop.Type)
                    {
                        case clsProperty.PropertyTypeEnum.StateList:
                            {
                            For Each sState As String In prop.arlStates
                                .WriteElementString("State", sState);
                            Next;
                            If prop.AppendToProperty != null Then .WriteElementString("AppendTo", prop.AppendToProperty)
                        case clsProperty.PropertyTypeEnum.ValueList:
                            {
                            For Each sKey As String In prop.ValueList.Keys
                                .WriteStartElement("ValueList");
                                .WriteElementString("Label", sKey);
                                .WriteElementString("Value", prop.ValueList(sKey).ToString);
                                .WriteEndElement();
                            Next;
                    }
                    If prop.PrivateTo <> "" Then .WriteElementString("PrivateTo", prop.PrivateTo)
                    If prop.PopupDescription <> "" Then .WriteElementString("Tooltip", prop.PopupDescription)

                    if (Not prop.DependentKey Is null && prop.DependentKey.Length > 0)
                    {
                        .WriteElementString("DependentKey", prop.DependentKey);
                        If ! prop.DependentValue == null && prop.DependentValue.Length > 0 Then .WriteElementString("DependentValue", prop.DependentValue)
                    }
                    if (Not prop.RestrictProperty Is null && prop.RestrictProperty.Length > 0)
                    {
                        .WriteElementString("RestrictProperty", prop.RestrictProperty);
                        If ! prop.RestrictValue == null && prop.RestrictValue.Length > 0 Then .WriteElementString("RestrictValue", prop.RestrictValue)
                    }
                    If prop.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(prop.LastUpdated));
                    .WriteElementString("Created", SetDate(prop.Created));

                    .WriteEndElement();
                }
            Next;


            For Each loc As clsLocation In a.htblLocations.Values
                if (Not bSelectedOnly || IsItemSelected(loc.Key))
                {
                    .WriteStartElement("Location");
                    .WriteElementString("Key", loc.Key);
                    SaveDescription(xmlWriter, loc.ShortDescription, "ShortDescription");
                    SaveDescription(xmlWriter, loc.LongDescription, "LongDescription");
                    for (DirectionsEnum di = DirectionsEnum.North; di <= DirectionsEnum.NorthWest; di++)
                    {
                        private clsDirection dtn = loc.arlDirections(di);
                        if (dtn.LocationKey <> "")
                        {
                            .WriteStartElement("Movement");
                            .WriteElementString("Direction", WriteEnum(di));
                            .WriteElementString("Destination", dtn.LocationKey);
                            SaveRestrictions(xmlWriter, dtn.Restrictions);
                            .WriteEndElement() ' Movement;
                        }
                    Next di;
                    For Each prop As clsProperty In loc.htblActualProperties.Values
                        .WriteStartElement("Property");
                        .WriteElementString("Key", prop.Key);
                        switch (prop.Type)
                        {
                            case clsProperty.PropertyTypeEnum.Text:
                                {
                                SaveDescription(xmlWriter, prop.StringData, "Value");
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                ' Do nothing
                            default:
                                {
                                .WriteElementString("Value", prop.Value);
                        }
                        .WriteEndElement() ' Property;
                    Next;
                    If loc.HideOnMap Then .WriteElementString("Hide", "1")
                    If loc.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(loc.LastUpdated));
                    .WriteElementString("Created", SetDate(loc.Created));

                    .WriteEndElement() ' Location;
                }
            Next loc;


            For Each ob As clsObject In a.htblObjects.Values
                if (Not bSelectedOnly || IsItemSelected(ob.Key))
                {
                    .WriteStartElement("Object");
                    .WriteElementString("Key", ob.Key);
                    .WriteElementString("Article", ob.Article);
                    .WriteElementString("Prefix", ob.Prefix);
                    For Each sName As String In ob.arlNames
                        .WriteElementString("Name", sName);
                    Next;
                    '.WriteElementString("Description", ob.Description)
                    SaveDescription(xmlWriter, ob.Description, "Description");
                    ' store key to property, plus value
                    For Each prop As clsProperty In ob.htblActualProperties.Values
                        '.WriteElementString("Property", prop.Key, prop.Value)
                        .WriteStartElement("Property");
                        .WriteElementString("Key", prop.Key);
                        switch (prop.Type)
                        {
                            case clsProperty.PropertyTypeEnum.Text:
                                {
                                SaveDescription(xmlWriter, prop.StringData, "Value");
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                ' Do nothing
                            default:
                                {
                                .WriteElementString("Value", prop.Value);
                        }
                        .WriteEndElement() ' Property;
                    Next;

                    If ob.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(ob.LastUpdated));
                    .WriteElementString("Created", SetDate(ob.Created));

                    .WriteEndElement();
                }
            Next;


            For Each tas As clsTask In a.htblTasks.Values
                if (Not bSelectedOnly || IsItemSelected(tas.Key))
                {
                    .WriteStartElement("Task");
                    .WriteElementString("Key", tas.Key);
                    .WriteElementString("Priority", tas.Priority.ToString);
                    If tas.AutoFillPriority <> 10 && tas.TaskType = clsTask.TaskTypeEnum.General Then .WriteElementString("AutoFillPriority", tas.AutoFillPriority.ToString)
                    .WriteElementString("Type", WriteEnum(tas.TaskType));
                    switch (tas.TaskType)
                    {
                        case clsTask.TaskTypeEnum.General:
                            {
                            For Each sCommand As String In tas.arlCommands
                                .WriteElementString("Command", sCommand);
                            Next;
                        case clsTask.TaskTypeEnum.Specific:
                            {
                            .WriteElementString("GeneralTask", tas.GeneralKey);
                            if (tas.Specifics IsNot null)
                            {
                                For Each spec As clsTask.Specific In tas.Specifics
                                    .WriteStartElement("Specific");
                                    .WriteElementString("Type", WriteEnum(spec.Type));
                                    .WriteElementString("Multiple", SafeInt(spec.Multiple).ToString);
                                    For Each sKey As String In spec.Keys
                                        .WriteElementString("Key", sKey);
                                    Next;
                                    .WriteEndElement();
                                Next;
                            }
                        case clsTask.TaskTypeEnum.System:
                            {
                            If tas.RunImmediately Then .WriteElementString("RunImmediately", SafeInt(tas.RunImmediately).ToString)
                            If tas.LocationTrigger <> "" Then .WriteElementString("LocationTrigger", tas.LocationTrigger)
                    }

                    .WriteElementString("Description", tas.Description);
                    '.WriteElementString("CompletionMessage", tas.CompletionMessage)
                    SaveDescription(xmlWriter, tas.CompletionMessage, "CompletionMessage");
                    .WriteElementString("Repeatable", SafeInt(tas.Repeatable).ToString);
                    '.WriteElementString("Continue", tas.ContinueToExecuteLowerPriority.ToString)
                    If tas.ContinueToExecuteLowerPriority Then .WriteElementString("Continue", "ContinueAlways")
                    If tas.LowPriority Then .WriteElementString("LowPriority", SafeInt(tas.LowPriority).ToString)
                    If tas.PreventOverriding Then .WriteElementString("PreventOverriding", SafeInt(tas.PreventOverriding).ToString)
                    If tas.ReplaceDuplicateKey Then .WriteElementString("ReplaceTask", SafeInt(tas.ReplaceDuplicateKey).ToString)
                    If tas.TaskType = clsTask.TaskTypeEnum.Specific Then .WriteElementString("SpecificOverrideType", tas.SpecificOverrideType.ToString)
                    If tas.FailOverride.ToString <> "" Then SaveDescription(xmlWriter, tas.FailOverride, "FailOverride")
                    If tas.eDisplayCompletion <> clsTask.BeforeAfterEnum.Before Then .WriteElementString("MessageBeforeOrAfter", WriteEnum(tas.eDisplayCompletion))
                    SaveRestrictions(xmlWriter, tas.arlRestrictions);
                    SaveActions(xmlWriter, tas.arlActions);
                    If tas.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(tas.LastUpdated));
                    .WriteElementString("Created", SetDate(tas.Created));
                    If ! tas.AggregateOutput Then .WriteElementString("Aggregate", "0")

                    .WriteEndElement() ' Task;
                }
            Next;



            For Each ev As clsEvent In a.htblEvents.Values
                if (Not bSelectedOnly || IsItemSelected(ev.Key))
                {
                    .WriteStartElement("Event");
                    .WriteElementString("Key", ev.Key);
                    .WriteElementString("Description", ev.Description);
                    .WriteElementString("Type", ev.EventType.ToString);
                    .WriteElementString("WhenStart", ev.WhenStart.ToString);
                    if (ev.WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns)
                    {
                        .WriteElementString("StartDelay", ev.StartDelay.ToString);
                    }
                    .WriteElementString("Length", ev.Length.ToString);
                    If ev.Repeating Then .WriteElementString("Repeating", SafeInt(ev.Repeating).ToString)
                    If ev.RepeatCountdown Then .WriteElementString("RepeatCountdown", CInt(ev.RepeatCountdown).ToString)

                    ' Controls
                    For Each ctl As EventOrWalkControl In ev.EventControls
                        .WriteElementString("Control", ctl.eControl.ToString + " " + ctl.eCompleteOrNot.ToString + " " + ctl.sTaskKey);
                    Next;
                    ' SubEvents
                    For Each se As clsEvent.SubEvent In ev.SubEvents
                        .WriteStartElement("SubEvent");
                        .WriteElementString("When", se.ftTurns.ToString + " " + se.eWhen.ToString);
                        .WriteElementString("What", WriteEnum(se.eWhat));
                        .WriteElementString("Measure", WriteEnum(se.eMeasure));
                        switch (se.eWhat)
                        {
                            case clsEvent.SubEvent.WhatEnum.DisplayMessage:
                            case clsEvent.SubEvent.WhatEnum.SetLook:
                                {
                                SaveDescription(xmlWriter, se.oDescription, "Action");
                                If se.sKey <> "" Then .WriteElementString("OnlyApplyAt", se.sKey)
                            case clsEvent.SubEvent.WhatEnum.ExecuteTask:
                            case clsEvent.SubEvent.WhatEnum.UnsetTask:
                                {
                                .WriteElementString("Action", se.eWhat.ToString + " " + se.sKey);
                        }

                        .WriteEndElement() ' SubEvent;
                    Next;

                    If ev.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(ev.LastUpdated));
                    .WriteElementString("Created", SetDate(ev.Created));

                    .WriteEndElement() ' Event;
                }
            Next;



            For Each ch As clsCharacter In a.htblCharacters.Values
                if (Not bSelectedOnly || IsItemSelected(ch.Key))
                {
                    .WriteStartElement("Character");
                    .WriteElementString("Key", ch.Key);
                    .WriteElementString("Name", ch.ProperName);
                    .WriteElementString("Article", ch.Article);
                    .WriteElementString("Prefix", ch.Prefix);
                    For Each sAlias As String In ch.arlDescriptors
                        .WriteElementString("Descriptor", sAlias);
                    Next;
                    .WriteElementString("Type", WriteEnum(ch.CharacterType));
                    If ch.Perspective <> PerspectiveEnum.ThirdPerson Then .WriteElementString("Perspective", ch.Perspective.ToString)
                    SaveDescription(xmlWriter, ch.Description, "Description");
                    '.WriteElementString("Location", chr.Location)
                    ' store key to property, plus value
                    For Each prop As clsProperty In ch.htblActualProperties.Values
                        '.WriteElementString("Property", prop.Key, prop.Value)
                        .WriteStartElement("Property");
                        .WriteElementString("Key", prop.Key);
                        switch (prop.Type)
                        {
                            case clsProperty.PropertyTypeEnum.Text:
                                {
                                SaveDescription(xmlWriter, prop.StringData, "Value");
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                ' Do nothing
                            default:
                                {
                                .WriteElementString("Value", prop.Value);
                        }
                        .WriteEndElement() ' Property;
                    Next;
                    For Each walk As clsWalk In ch.arlWalks
                        .WriteStartElement("Walk");
                        .WriteElementString("Description", walk.Description);
                        'If walk.FromDesc <> "" Then .WriteElementString("FromDesc", walk.FromDesc)
                        .WriteElementString("Loops", SafeInt(walk.Loops).ToString);
                        '.WriteElementString("ShowMove", walk.ShowMove.ToString)
                        .WriteElementString("StartActive", SafeInt(walk.StartActive).ToString);
                        'If walk.ToDesc <> "" Then .WriteElementString("ToDesc", walk.ToDesc)
                        For Each [step] As clsWalk.clsStep In walk.arlSteps
                            .WriteElementString("Step", [step].sLocation + " " + [step].ftTurns.ToString);
                        Next;
                        ' Controls
                        For Each ctl As EventOrWalkControl In walk.WalkControls
                            .WriteElementString("Control", ctl.eControl.ToString + " " + ctl.eCompleteOrNot.ToString + " " + ctl.sTaskKey);
                        Next;
                        ' SubWalks
                        For Each sw As clsWalk.SubWalk In walk.SubWalks
                            .WriteStartElement("Activity");
                            switch (sw.eWhen)
                            {
                                case clsWalk.SubWalk.WhenEnum.ComesAcross:
                                    {
                                    .WriteElementString("When", sw.eWhen.ToString + " " + sw.sKey);
                                default:
                                    {
                                    .WriteElementString("When", sw.ftTurns.ToString + " " + sw.eWhen.ToString);
                            }

                            switch (sw.eWhat)
                            {
                                case clsWalk.SubWalk.WhatEnum.DisplayMessage:
                                    {
                                    SaveDescription(xmlWriter, sw.oDescription, "Action");
                                case clsWalk.SubWalk.WhatEnum.ExecuteTask:
                                case clsWalk.SubWalk.WhatEnum.UnsetTask:
                                    {
                                    .WriteElementString("Action", sw.eWhat.ToString + " " + sw.sKey2);
                            }

                            If sw.sKey3 <> "" Then .WriteElementString("OnlyApplyAt", sw.sKey3)

                            .WriteEndElement() ' Activity;
                        Next;
                        .WriteEndElement();
                    Next;
                    For Each topic As clsTopic In ch.htblTopics.Values
                        .WriteStartElement("Topic");
                        .WriteElementString("Key", topic.Key);
                        If topic.ParentKey <> "" Then .WriteElementString("ParentKey", topic.ParentKey)
                        .WriteElementString("Summary", topic.Summary);
                        .WriteElementString("Keywords", topic.Keywords);
                        SaveDescription(xmlWriter, topic.oConversation, "Description");
                        If topic.bAsk Then .WriteElementString("IsAsk", SafeInt(topic.bAsk).ToString)
                        If topic.bCommand Then .WriteElementString("IsCommand", SafeInt(topic.bCommand).ToString)
                        If topic.bFarewell Then .WriteElementString("IsFarewell", SafeInt(topic.bFarewell).ToString)
                        If topic.bIntroduction Then .WriteElementString("IsIntro", SafeInt(topic.bIntroduction).ToString)
                        If topic.bTell Then .WriteElementString("IsTell", SafeInt(topic.bTell).ToString)
                        If topic.bStayInNode Then .WriteElementString("StayInNode", SafeInt(topic.bStayInNode).ToString)
                        SaveRestrictions(xmlWriter, topic.arlRestrictions);
                        SaveActions(xmlWriter, topic.arlActions);
                        .WriteEndElement() ' Topic;
                    Next;
                    If ch.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(ch.LastUpdated));
                    .WriteElementString("Created", SetDate(ch.Created));

                    .WriteEndElement() ' Character;
                }
            Next;


            For Each var As clsVariable In a.htblVariables.Values
                if (Not bSelectedOnly || IsItemSelected(var.Key))
                {
                    .WriteStartElement("Variable");
                    .WriteElementString("Key", var.Key);
                    .WriteElementString("Name", var.Name);
                    .WriteElementString("Type", WriteEnum(var.Type));
                    if (var.Type = clsVariable.VariableTypeEnum.Text)
                    {
                        .WriteElementString("InitialValue", var.StringValue);
                    ElseIf (var.Type = clsVariable.VariableTypeEnum.Numeric && var.Length > 1 && var.StringValue.Contains(",")) Then
                        .WriteElementString("InitialValue", var.StringValue);
                    Else
                        .WriteElementString("InitialValue", var.IntValue.ToString);
                    }
                    If var.Length > 1 Then .WriteElementString("ArrayLength", var.Length.ToString)
                    If var.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(var.LastUpdated));
                    .WriteElementString("Created", SetDate(var.Created));

                    .WriteEndElement() ' Variable;
                }
            Next;


            For Each grp As clsGroup In a.htblGroups.Values
                if (Not bSelectedOnly || IsItemSelected(grp.Key))
                {
                    .WriteStartElement("Group");
                    .WriteElementString("Key", grp.Key);
                    .WriteElementString("Type", WriteEnum(grp.GroupType));
                    .WriteElementString("Name", grp.Name);
                    For Each sMember As String In grp.arlMembers
                        .WriteElementString("Member", sMember);
                    Next;
                    For Each prop As clsProperty In grp.htblProperties.Values
                        .WriteStartElement("Property");
                        .WriteElementString("Key", prop.Key);
                        switch (prop.Type)
                        {
                            case clsProperty.PropertyTypeEnum.Text:
                                {
                                SaveDescription(xmlWriter, prop.StringData, "Value");
                            case clsProperty.PropertyTypeEnum.SelectionOnly:
                                {
                                ' Do nothing
                            default:
                                {
                                .WriteElementString("Value", prop.Value);
                        }
                        .WriteEndElement() ' Property;
                    Next;
                    If grp.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(grp.LastUpdated));
                    .WriteElementString("Created", SetDate(grp.Created));

                    .WriteEndElement() ' Group;
                }
            Next;


            For Each alr As clsALR In a.htblALRs.Values
                if (Not bSelectedOnly || IsItemSelected(alr.Key))
                {
                    .WriteStartElement("TextOverride");
                    .WriteElementString("Key", alr.Key);
                    .WriteElementString("OldText", alr.OldText);
                    SaveDescription(xmlWriter, alr.NewText, "NewText");
                    If alr.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(alr.LastUpdated));
                    .WriteElementString("Created", SetDate(alr.Created));
                    .WriteEndElement() ' TextOverride;
                }
            Next;


            For Each hint As clsHint In a.htblHints.Values
                if (Not bSelectedOnly || IsItemSelected(hint.Key))
                {
                    .WriteStartElement("Hint");
                    .WriteElementString("Key", hint.Key);
                    .WriteElementString("Question", hint.Question);
                    SaveDescription(xmlWriter, hint.SubtleHint, "Subtle");
                    SaveDescription(xmlWriter, hint.SledgeHammerHint, "Sledgehammer");
                    SaveRestrictions(xmlWriter, hint.arlRestrictions);
                    If hint.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(hint.LastUpdated));
                    .WriteElementString("Created", SetDate(hint.Created));
                    .WriteEndElement() ' Hint;
                }
            Next;


            For Each syn As clsSynonym In a.htblSynonyms.Values
                if (Not bSelectedOnly || IsItemSelected(syn.Key))
                {
                    .WriteStartElement("Synonym");
                    .WriteElementString("Key", syn.Key);
                    For Each sFrom As String In syn.ChangeFrom
                        .WriteElementString("From", sFrom);
                    Next;
                    .WriteElementString("To", syn.ChangeTo);
                    .WriteEndElement() ' Synonym;
                }
            Next;


            For Each udf As clsUserFunction In a.htblUDFs.Values
                if (Not bSelectedOnly || IsItemSelected(udf.Key))
                {
                    .WriteStartElement("Function");
                    .WriteElementString("Key", udf.Key);
                    .WriteElementString("Name", udf.Name);
                    SaveDescription(xmlWriter, udf.Output, "Output");
                    For Each arg As clsUserFunction.Argument In udf.Arguments
                        .WriteStartElement("Argument");
                        .WriteElementString("Name", arg.Name);
                        .WriteElementString("Type", arg.Type.ToString);
                        .WriteEndElement() ' Argument;
                    Next;
                    If udf.IsLibrary Then .WriteElementString("Library", "1")
                    .WriteElementString("LastUpdated", SetDate(udf.LastUpdated));
                    .WriteElementString("Created", SetDate(udf.Created));
                    .WriteEndElement() ' Function;
                }
            Next;


            For Each sExclude As String In a.listExcludedItems
                .WriteElementString("Exclude", sExclude);
            Next;


            if (Not bSelectedOnly)
            {
                private XmlWriter w = xmlWriter;
                .WriteStartElement("Map");
                With a.Map.Pages;
                    For Each iPage As Integer In .Keys
                        w.WriteStartElement("Page");
                        w.WriteElementString("Key", iPage.ToString);
                        If a.Map.SelectedPage = iPage.ToString Then w.WriteElementString("Selected", "1")
                        With .Item(iPage);
                            w.WriteElementString("Label", .Label);
                            For Each node As MapNode In .Nodes
                                With node;
                                    w.WriteStartElement("Node");
                                    w.WriteElementString("Key", .Key);
                                    w.WriteElementString("X", .Location.X.ToString);
                                    w.WriteElementString("Y", .Location.Y.ToString);
                                    w.WriteElementString("Z", .Location.Z.ToString);
                                    If .Height <> 4 Then w.WriteElementString("Height", .Height.ToString)
                                    If .Width <> 6 Then w.WriteElementString("Width", .Width.ToString)
                                    For Each link As MapLink In .Links.Values
                                        With link;
                                            w.WriteStartElement("Link");
                                            w.WriteElementString("SourceAnchor", .eSourceLinkPoint.ToString);
                                            w.WriteElementString("DestinationAnchor", .eDestinationLinkPoint.ToString);
                                            if (.OrigMidPoints.Length > 0)
                                            {
                                                For Each p As Point3D In .OrigMidPoints
                                                    w.WriteStartElement("Anchor");
                                                    w.WriteElementString("X", p.X.ToString);
                                                    w.WriteElementString("Y", p.Y.ToString);
                                                    w.WriteElementString("Z", p.Z.ToString);
                                                    w.WriteEndElement() ' Anchor;
                                                Next;
                                            }
                                            w.WriteEndElement() ' Link;
                                        }
                                    Next;
                                    w.WriteEndElement() ' Node;
                                }
                            Next;
                        }
                        w.WriteEndElement() ' Page;
                    Next;
                }
                .WriteEndElement() ' Map;
            }


            if (bBlorb)
            {
                private List<string> lImages = Adventure.Images;
                private List<string> lSounds = Adventure.Sounds;
                if (lImages.Count + lSounds.Count > 0)
                {
                    .WriteStartElement("FileMappings");
                    private int iResource = 1;
                    For Each sImage As String In lImages
                        .WriteStartElement("Mapping");
                        .WriteElementString("Resource", iResource.ToString);
                        .WriteElementString("File", sImage);
                        .WriteEndElement() ' Mapping;
                        iResource += 1;
                    Next;
                    For Each sSound As String In lSounds
                        .WriteStartElement("Mapping");
                        .WriteElementString("Resource", iResource.ToString);
                        .WriteElementString("File", sSound);
                        .WriteEndElement() ' Mapping;
                        iResource += 1;
                    Next;
                    .WriteEndElement() ' FileMappings;
                }
            }

            .WriteEndElement() ' Adventure;

            .WriteEndDocument();
            .Flush();
            ' Finished writing to memory stream

            ReDim bAdventure(-1);
            if (bCompress)
            {
                private New System.IO.MemoryStream outStream;
                private New ZOutputStream(outStream, zlibConst.Z_BEST_COMPRESSION) zStream;

                try
                {
                    stmMemory.Position = 0;
                    CopyStream(stmMemory, zStream);
                }
                finally
                {
                    zStream.Close();
                    bAdventure = outStream.ToArray
                    ObfuscateByteArray(bAdventure);
                    outStream.Close();
                    'inStream.Close()
                }
            Else
                ' Redirect the memory stream direct to our binary output
                bAdventure = stmMemory.ToArray
            }

            .Close();
        }

        return true;

    }
#endif


    private void IsEqual(ref mb1 As Byte()
    {

        If (mb1.length <> mb2.length) Then ' make sure arrays same length
            return false;
        Else
            for (int i = 0; i <= mb1.Length - 1 ' run array length looking for miscompare; i++)
            {
                If mb1(i) <> mb2(i) Then Return false
            Next;
        }

        return true;
    }

    private bool IsHex(string sHex)
    {
        for (int i = 0; i <= sHex.Length - 1; i++)
        {
            If "0123456789ABCDEF".IndexOf(sHex(i)) = -1 Then Return false
        Next;
        return true;
    }


    ' "Tweak" any v5 library tasks that are different from v4
    private void TweakTasksForv4()
    {

        if (Adventure.htblTasks.ContainsKey("GiveObjectToChar"))
        {
            With Adventure.htblTasks("GiveObjectToChar");
                .CompletionMessage = New Description("%CharacterName[%character%, subject]% doesn't seem interested in %objects%.Name.");
                .arlActions.Clear();
            }
        }

        if (Adventure.htblTasks.ContainsKey("Look"))
        {
            With Adventure.htblTasks("Look");
                .arlCommands(0) = "[look/l]{ room}";
                .arlCommands.Add("[x/examine] room");
            }
        }

        SearchAndReplace("Sorry, I'm not sure which object or character you are trying to examine.", "You see no such thing.", true);

    }


#if Generator
    internal bool ImportTrizbort(string sFilename)
    {

        try
        {
            if (Not IO.File.Exists(sFilename))
            {
                ErrMsg("File '" + sFilename + "' not found.");
                return false;
            }

            private New System.Xml.Serialization.XmlSerializer(GetType(trizbort)) oXML;
            private New IO.FileStream(sFilename, IO.FileMode.Open, FileAccess.Read) stmFile;
            private trizbort trizbort = CType(oXML.Deserialize(stmFile), trizbort);
            return fGenerator.Map1.ImportTrizbort(trizbort);

        }
        catch (Exception ex)
        {
            ErrMsg("Error importing Trizbort map", ex);
            return false;
        }

    }


    internal bool ImportALR(string sFilename)
    {

        try
        {
            if (Not IO.File.Exists(sFilename))
            {
                ErrMsg("File '" + sFilename + "' not found.");
                return false;
            }

            If Adventure == null Then Return false

            private int i;
            For Each sLine As String In IO.File.ReadAllLines(sFilename)
                if (sLine.Length > 0)
                {
                    if (Not sLine.StartsWith("#") && sLine.Contains("|"))
                    {
                        Dim sData() As String = sLine.Split("|"c)
                        if (sData.Length = 2)
                        {
                            private New clsALR alr;
                            alr.Key = alr.GetNewKey ' Adventure.GetNewKey("Override");
                            alr.OldText = sData(0);
                            alr.NewText = New Description(sData(1));
                            alr.LastUpdated = Now;
                            Adventure.htblALRs.Add(alr, alr.Key);
                            UpdateListItem(alr.Key, alr.OldText);
                            i += 1;
                        }
                    }
                }
            Next;

            If i > 0 Then MessageBox.Show(i + " records imported", "Import Text Overrides", MessageBoxButtons.OK, MessageBoxIcon.Information)

            return true;

        }
        catch (Exception ex)
        {
            ErrMsg("Error importing Language Resource File", ex);
            return false;
        }

    }


    internal bool ExportALR(string sFilename)
    {

        try
        {
            If Adventure == null Then Return false
            if (Adventure.htblALRs.Count = 0)
            {
                ErrMsg("null to export");
                return false;
            }

            Dim sLines(Adventure.htblALRs.Count - 1) As String
            private int i = 0;

            ' Should this output as XML, to retain Description?
            For Each alr As clsALR In Adventure.htblALRs.Values
                sLines(i) = alr.OldText.Replace("|", "") + "|" + alr.NewText.ToString.Replace("|", "");
                i += 1;
            Next;
            IO.File.WriteAllLines(sFilename, sLines);

            MessageBox.Show(i + " records exported", "Export Text Overrides", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }
        catch (Exception ex)
        {
            ErrMsg("Error exporting Language Resource File", ex);
            return false;
        }

    }

#endif


    private bool Authenticate(Stream oStream, long iFileLen)
    {

        try
        {
            oStream.Seek(iFileLen - 14, SeekOrigin.Begin);
            br = New BinaryReader(oStream)
            private byte[] bPass = Dencode(br.ReadBytes(12), iFileLen - 13);
            private string sPassString = System.Text.Encoding.UTF8.GetString(bPass);
            Adventure.Password = (sLeft(sPassString, 4) + sRight(sPassString, 4)).Trim;

#if Generator
            if (sPassString <> "    Wild    ")
            {
                private New Password fPassword;
                fPassword.Text = "Open Adventure";
                fPassword.lblText.Text = "Please enter password:";
                fPassword.PictureBox1.Image = My.Resources.Resources.imgLock32;
#if DEBUG
                fPassword.txtPassword.Text = Adventure.Password.TrimEnd;
#endif
                if (fPassword.ShowDialog = DialogResult.OK)
                {
                    private string sPasswordCheck = fPassword.txtPassword.Text;
                    if (sLeft(sPasswordCheck, 8).TrimEnd <> Adventure.Password.TrimEnd)
                    {
                        ErrMsg("Incorrect password.  Unable to load this adventure.");
#if DEBUG
                        ' Allow
#else
                            return false;
#endif
                    }
                Else
                    return false;
                }
            }
#endif
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }

    }



    ' This loads from file into our data structure
    ' Assumes file exists
    '
public enum FileTypeEnum
    {
        TextAdventure_TAF;
        XMLModule_AMF;
        v4Module_AMF;
        GameState_TAS;
        Blorb;
        Exe;
    }
#if Runner
    internal bool LoadFile(string sFilename, FileTypeEnum eFileType, LoadWhatEnum eLoadWhat, bool bLibrary, DateTime dtAdvDate = #1/1/1900#, ref state As clsGameState = Nothing, long lOffset = 0, bool bSilentError = false)
    {
#else
    internal bool LoadFile(string sFilename, FileTypeEnum eFileType, LoadWhatEnum eLoadWhat, bool bLibrary, DateTime dtAdvDate = #1/1/1900#, long lOffset = 0, bool bSilentError = false)
    {
#endif

        private IO.FileStream stmOriginalFile = null;
        Cursor.Current = Cursors.WaitCursor;

        try
        {
            if (Not IO.File.Exists(sFilename))
            {
                'ErrMsg(IO.Directory.GetCurrentDirectory & ", " & sFilename)
                ErrMsg("File '" + sFilename + "' not found.");
#if Not www
                RemoveFileFromList(sFilename);
#endif
                return false;
            }

            '            If lOffset = 0 AndAlso sFilename.ToUpper.EndsWith(".EXE") Then
            '                Try
            '                    ' Work out whether we have a TAF appended on the end.  If so, run that in Executable mode
            '                    ' Grab out the last 8 bytes, and see if it converts to a size
            '                    Dim bData(5) As Byte
            '                    Dim fStream As New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
            '                    fStream.Seek(fStream.Length - 6, IO.SeekOrigin.Begin)
            '                    fStream.Read(bData, 0, 6)
            '                    fStream.Close()

            '                    Dim sFileSize As String = (New System.Text.ASCIIEncoding).GetString(bData).ToUpper
            '                    If IsHex(sFileSize) Then
            '                        Dim lFileSize As Long = CLng("&H" & sFileSize)
            '                        If lFileSize > 0 Then
            '                            ' Ok, check the offset to see that the appended data is really a TAF...
            '                            fStream = New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
            '                            fStream.Seek(lFileSize, IO.SeekOrigin.Begin)
            '                            ReDim bData(11)
            '                            fStream.Read(bData, 0, 12)
            '                            fStream.Close()
            '                            Dim sVersion As String = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(bData), 1))
            '                            If sVersion = "Version 5.00" Then
            '#If Runner Then
            '                                Return LoadFile(sFilename, eFileType, eLoadWhat, bLibrary, dtAdvDate, state, lFileSize, bSilentError)
            '#Else
            '                                Return LoadFile(sFilename, eFileType, eLoadWhat, bLibrary, dtAdvDate, lFileSize, bSilentError)
            '#End If
            '                            End If
            '                        End If
            '                    End If

            '                Catch ex As Exception
            '                    If Not bSilentError Then ErrMsg("Error loading EXE", ex)
            '                    Return False
            '                End Try
            '            End If

            stmOriginalFile = New IO.FileStream(sFilename, IO.FileMode.Open, FileAccess.Read)

            if (eFileType = FileTypeEnum.TextAdventure_TAF)
            {

                stmOriginalFile.Seek(FileLen(sFilename) - 14, SeekOrigin.Begin);
                br = New BinaryReader(stmOriginalFile)
                private byte[] bPass = Dencode(br.ReadBytes(12), FileLen(sFilename) - 13);
                private string sPassString = System.Text.Encoding.UTF8.GetString(bPass);
                'Dim encodingEuropean As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
                'Dim sPassString As String = encodingEuropean.GetString(Dencode(encodingEuropean.GetString(br.ReadBytes(12)), FileLen(sFilename) - 13))
                'Dim sPassString As String = System.Text.Encoding.UTF8.GetString(Dencode(System.Text.Encoding.UTF8.GetString(br.ReadBytes(12)), FileLen(sFilename) - 13))
                'Dim sPassString As String = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(br.ReadBytes(12)), FileLen(sFilename) - 13))
                Adventure.Password = (sLeft(sPassString, 4) + sRight(sPassString, 4)).Trim;

#if Generator
                if (sPassString <> "    Wild    ")
                {
                    private New Password fPassword;
                    fPassword.Text = "Open Adventure";
                    fPassword.lblText.Text = "Please enter password:";
                    fPassword.PictureBox1.Image = My.Resources.Resources.imgLock32;
#if DEBUG
                    fPassword.txtPassword.Text = Adventure.Password.TrimEnd;
#endif
                    if (fPassword.ShowDialog = DialogResult.OK)
                    {
                        private string sPasswordCheck = fPassword.txtPassword.Text;
                        if (sLeft(sPasswordCheck, 8).TrimEnd <> Adventure.Password.TrimEnd)
                        {
                            ErrMsg("Incorrect password.  Unable to load this adventure.");
#if DEBUG
                            ' Allow
#else
                            return false;
#endif
                        }
                    Else
                        return false;
                    }
                }
#endif
            }

            if (lOffset > 0)
            {
                stmOriginalFile.Seek(lOffset, SeekOrigin.Begin);
                lOffset += 7 ' for the footer;
            Else
                stmOriginalFile.Seek(0, SeekOrigin.Begin);
            }

            br = New BinaryReader(stmOriginalFile)

            iLoading += 1;

            switch (eFileType)
            {
                case FileTypeEnum.Exe:
                    {
                    if (clsBlorb.ExecResource IsNot null && clsBlorb.ExecResource.Length > 0)
                    {
                        If ! Load500(Decompress(clsBlorb.ExecResource, dVersion >= 5.00002 && clsBlorb.bObfuscated, 16, clsBlorb.ExecResource.Length - 30), false) Then Return false
                        clsBlorb.bObfuscated = false;
                        If clsBlorb.MetaData != null Then Adventure.BabelTreatyInfo.FromString(clsBlorb.MetaData.OuterXml)
                        Adventure.FullPath = SafeString(Application.ExecutablePath);
                    Else
                        return false;
                    }

                case FileTypeEnum.Blorb:
                    {
#if Not Generator OrElse Debug
                    ' Allow
#else
                    ErrMsg("Blorb files can only be opened directly by Runner, or may be Imported to extract an adventure file.");
                    return false;
#endif
                    Blorb = New clsBlorb
                    if (Blorb.LoadBlorb(stmOriginalFile, sFilename))
                    {

                        private byte[] bVersion = new Byte(11) {};
                        Array.Copy(clsBlorb.ExecResource, bVersion, 12);

                        private string sVersion;
                        if (IsEqual(bVersion, new Byte() {60, 66, 63, 201, 106, 135, 194, 207, 146, 69, 62, 97}))
                        {
                            sVersion = "Version 5.00"
                        ElseIf IsEqual(bVersion, New Byte() {60, 66, 63, 201, 106, 135, 194, 207, 147, 69, 62, 97}) Then
                            sVersion = "Version 4.00"
                        ElseIf IsEqual(bVersion, New Byte() {60, 66, 63, 201, 106, 135, 194, 207, 148, 69, 55, 97}) Then
                            sVersion = "Version 3.90"
                        Else
                            sVersion = System.Text.Encoding.UTF8.GetString(Dencode(bVersion, 1))
                        }

                        if (Left(sVersion, 8) <> "Version ")
                        {
                            ErrMsg("! an ADRIFT Blorb file");
                            return false;
                        }

#if Runner
                        UserSession.ShowUserSplash();
#endif

                        With Adventure;
                            .dVersion = Double.Parse(sVersion.Replace("Version ", ""), Globalization.CultureInfo.InvariantCulture.NumberFormat) 'CDbl(sVersion.Replace("Version ", ""));
                            .Filename = Path.GetFileName(sFilename);
                            .FullPath = sFilename;
                        }

                        private bool bDeObfuscate = clsBlorb.MetaData Is null || clsBlorb.MetaData.OuterXml.Contains("compilerversion") ' Nasty, but works;
                        ' Was this a pre-obfuscated size blorb?
                        if (clsBlorb.ExecResource.Length > 16 && clsBlorb.ExecResource(12) = 48 && clsBlorb.ExecResource(13) = 48 && clsBlorb.ExecResource(14) = 48 && clsBlorb.ExecResource(15) = 48)
                        {
                            If ! Load500(Decompress(clsBlorb.ExecResource, bDeObfuscate, 16, clsBlorb.ExecResource.Length - 30), false) Then Return false
                        Else
                            If ! Load500(Decompress(clsBlorb.ExecResource, bDeObfuscate, 12, clsBlorb.ExecResource.Length - 26), false) Then Return false
                        }

                        If clsBlorb.MetaData != null Then Adventure.BabelTreatyInfo.FromString(clsBlorb.MetaData.OuterXml)
                    Else
                        return false;
                    }

                case FileTypeEnum.TextAdventure_TAF:
                    {
                    private byte[] bVersion = br.ReadBytes(12);
                    private string sVersion;
                    if (IsEqual(bVersion, new Byte() {60, 66, 63, 201, 106, 135, 194, 207, 146, 69, 62, 97}))
                    {
                        sVersion = "Version 5.00"
                    ElseIf IsEqual(bVersion, New Byte() {60, 66, 63, 201, 106, 135, 194, 207, 147, 69, 62, 97}) Then
                        sVersion = "Version 4.00"
                    ElseIf IsEqual(bVersion, New Byte() {60, 66, 63, 201, 106, 135, 194, 207, 148, 69, 55, 97}) Then
                        sVersion = "Version 3.90"
                    Else
                        sVersion = System.Text.Encoding.UTF8.GetString(Dencode(bVersion, 1))
                    }
                    'Dim sVersion As String = System.Text.Encoding.Default.GetString(br.ReadBytes(12))
                    switch (true)
                    {
                        ' For Mono, because random from same seed gives a different number. :-(
                        'Case ChrW(60) & ChrW(66) & ChrW(63) & ChrW(65533) & ChrW(106) & ChrW(65533) & ChrW(65533) & ChrW(978) & ChrW(69) & ChrW(62) & ChrW(97)
                        'Case bVersion = Byte() {}
                        '    sVersion = "Version 5.00"
                        'Case ChrW(60) & ChrW(66) & ChrW(63) & ChrW(65533) & ChrW(106) & ChrW(65533) & ChrW(65533) & ChrW(979) & ChrW(69) & ChrW(62) & ChrW(97)
                        '    sVersion = "Version 4.00"
                        'Case ChrW(60) & ChrW(66) & ChrW(63) & ChrW(65533) & ChrW(106) & ChrW(65533) & ChrW(65533) & ChrW(980) & ChrW(69) & ChrW(55) & ChrW(97)
                        '    sVersion = "Version 3.90"
                        default:
                            {
                            'sVersion = System.Text.Encoding.Default.GetString(Dencode(sVersion, 1))

                    }

                    if (Left(sVersion, 8) <> "Version ")
                    {
                        ErrMsg("! an ADRIFT Text Adventure file");
                        return false;
                    }

                    With Adventure;
                        .dVersion = Double.Parse(sVersion.Replace("Version ", ""), Globalization.CultureInfo.InvariantCulture.NumberFormat);
                        .Filename = Path.GetFileName(sFilename);
                        .FullPath = sFilename;
                    }

                    Debug.WriteLine("Start Load: " + Now);
                    switch (sVersion)
                    {
                        'Case "Version 3.90"
                        '    Dim br2 As BinaryReader = br
                        '    LoadLibraries(LoadWhatEnum.Properies)
                        '    br = br2
                        '    iStartPriority = 0
                        '    If LoadOlder(3.9) Then
                        '        iStartPriority = 50000
                        '        LoadLibraries(LoadWhatEnum.AllExceptProperties)
                        '        'CreateMandatoryProperties()
                        '        br = br2
                        '        Set400SpecificTasks()
                        '        Adventure.dVersion = 3.90002
                        '    Else
                        '        Return False
                        '    End If
                        case "Version 3.90":
                        case "Version 4.00":
                            {
                            private BinaryReader br2 = br;
                            'LoadDefaults() ' If mandatory properties like StaticOrDynamic don't exist, create them
                            LoadLibraries(LoadWhatEnum.Properies);
                            br = br2
                            iStartPriority = 0
                            if (LoadOlder(CDbl(sVersion.Substring(8))))
                            {
                                iStartPriority = 50000
                                LoadLibraries(LoadWhatEnum.AllExceptProperties, "standardlibrary");
                                'CreateMandatoryProperties()
                                TweakTasksForv4();
                                br = br2
                                Set400SpecificTasks();
                                Adventure.dVersion = CDbl(sVersion.Substring(8));
                                If Adventure.dVersion = 4 Then Adventure.dVersion = 4.000052
                            Else
                                return false;
                            }
                        case "Version 5.00":
                            {
                            private string sSize = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));
                            private string sCheck = System.Text.Encoding.UTF8.GetString(br.ReadBytes(8)) 'CStr(br.ReadChars(8));
                            private int iBabelLength = 0;
                            private string sBabel = null;
                            private bool bObfuscate = true;
                            if (sSize = "0000" || sCheck = "<ifindex")
                            {
                                stmOriginalFile.Seek(16, 0) ' Set to just after the size chunk;
                                ' 5.0.20 format onwards
                                iBabelLength = CInt("&H" & sSize)
                                if (iBabelLength > 0)
                                {
                                    Dim bBabel() As Byte = br.ReadBytes(iBabelLength)
                                    sBabel = System.Text.Encoding.UTF8.GetString(bBabel)
                                }
                                iBabelLength += 4 ' For size header;
                            Else
                                ' Pre 5.0.20
                                ' THIS COULD BE AN EXTRACTED TAF, THEREFORE NO METADATA!!!
                                ' Ok, we have no uncompressed Babel info at the start.  Start over...
                                stmOriginalFile.Seek(0, SeekOrigin.Begin);
                                br = New BinaryReader(stmOriginalFile)
                                br.ReadBytes(12);
                                bObfuscate = False
                            }
                            private MemoryStream stmFile = FileToMemoryStream(true, CInt(FileLen(sFilename) - 26 - lOffset - iBabelLength), bObfuscate);
                            If stmFile == null Then Return false
                            If ! Load500(stmFile, false, false, eLoadWhat, dtAdvDate) Then Return false ' - 12)))
                            if (sBabel <> "")
                            {
                                Adventure.BabelTreatyInfo.FromString(sBabel);
                                private string sTemp = Adventure.CoverFilename;
                                Adventure.CoverFilename = null;
                                Adventure.CoverFilename = sTemp ' Just to re-set the image in the Babel structure;
                                'With Adventure.BabelTreatyInfo.Stories(0).Bibliographic
                                '    Adventure.Title = .Title
                                '    Adventure.Author = .Author
                                'End With
                            }
#if Generator
                            ' Check each library file.  If it is a later date than the adventure file, ask whether we want to overwrite library items
                            ' TODO - This needs to check each individual item date rather than the adventure, in case the user has saved their game
                            ' since the library was saved, but hasn't updated the individual items.
                            ' It should also prompt to say what would be added/updated
                            '
                            OverwriteLibraries(LoadWhatEnum.All);
                            '#Else
                            '                            If lOffset > 0 Then bEXE = True
#endif

                        default:
                            {
                            ErrMsg("ADRIFT " + sVersion + " Adventures are not currently supported in ADRIFT v" + dVersion.ToString("0.0"));
                            return false;
                    }
                    Debug.WriteLine("End Load: " + Now);

                case FileTypeEnum.v4Module_AMF:
                    {
                    TODO("Version 4.0 Modules");

                case FileTypeEnum.XMLModule_AMF:
                    {
                    If ! Load500(FileToMemoryStream(false, CInt(FileLen(sFilename)), false), bLibrary, true, eLoadWhat, dtAdvDate, sFilename) Then Return false

#if Runner
                case FileTypeEnum.GameState_TAS:
                    {
                    state = LoadState(FileToMemoryStream(True, CInt(FileLen(sFilename)), False))
#endif

            }

            If br != null Then br.Close()
            br = Nothing
            stmOriginalFile.Close();
            stmOriginalFile = Nothing

            If Adventure.NotUnderstood = "" Then Adventure.NotUnderstood = "Sorry, I didn't understand that command."
            if (Adventure.Player IsNot null && (Adventure.Player.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden || Adventure.Player.Location.Key = ""))
            {
                if (Adventure.htblLocations.Count > 0)
                {
                    private clsCharacterLocation locFirst = Adventure.Player.Location;
                    locFirst.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                    For Each sKey As String In Adventure.htblLocations.Keys
                        locFirst.Key = sKey;
                        Exit For;
                    Next;
                    Adventure.Player.Move(locFirst);
                }
            }

            iLoading -= 1;

            return true;
        }
        catch (Exception ex)
        {
            If ! br == null Then br.Close()
            If ! stmOriginalFile == null Then stmOriginalFile.Close()
            ErrMsg("Error loading " + sFilename, ex);
            return false;
        }
        finally
        {
            Cursor.Current = Cursors.Arrow;
        }

    }


#if Runner
    private clsGameState LoadState(MemoryStream stmMemory)
    {

        try
        {
            private New clsGameState NewState;
            'Dim stmMemory As MemoryStream = FileToMemoryStream(True, CInt(FileLen(sFilePath)))
            private New XmlDocument xmlDoc;
            xmlDoc.Load(stmMemory);

            With xmlDoc.Item("Game");
                For Each nodLoc As XmlElement In xmlDoc.SelectNodes("/Game/Location")
                    With nodLoc;
                        private New clsGameState.clsLocationState locs;
                        private string sKey = .Item("Key").InnerText;
                        For Each nodProp As XmlElement In nodLoc.SelectNodes("Property")
                            private New clsGameState.clsLocationState.clsStateProperty props;
                            private string sPropKey = nodProp.Item("Key").InnerText;
                            props.Value = nodProp.Item("Value").InnerText;
                            locs.htblProperties.Add(sPropKey, props);
                        Next;
                        For Each nodDisplayed As XmlElement In nodLoc.SelectNodes("Displayed")
                            locs.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                        Next;
                        'If .Item("Seen") IsNot Nothing Then locs.bSeen = CBool(.Item("Seen").InnerText)
                        NewState.htblLocationStates.Add(sKey, locs);
                    }
                Next;

                For Each nodOb As XmlElement In xmlDoc.SelectNodes("/Game/Object")
                    With nodOb;
                        private New clsGameState.clsObjectState obs;
                        obs.Location = New clsObjectLocation;
                        private string sKey = .Item("Key").InnerText;
                        if (.Item("DynamicExistWhere") IsNot null)
                        {
                            obs.Location.DynamicExistWhere = (.Item("DynamicExistWhere")([Enum].Parse(GetType(clsObjectLocation.DynamicExistsWhereEnum)).InnerText), clsObjectLocation.DynamicExistsWhereEnum);
                        Else
                            obs.Location.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                        }
                        if (.Item("StaticExistWhere") IsNot null)
                        {
                            obs.Location.StaticExistWhere = (.Item("StaticExistWhere")([Enum].Parse(GetType(clsObjectLocation.StaticExistsWhereEnum)).InnerText), clsObjectLocation.StaticExistsWhereEnum);
                        Else
                            obs.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.NoRooms;
                        }
                        If .Item("LocationKey") != null Then obs.Location.Key = .Item("LocationKey").InnerText
                        For Each nodProp As XmlElement In nodOb.SelectNodes("Property")
                            private New clsGameState.clsObjectState.clsStateProperty props;
                            private string sPropKey = nodProp.Item("Key").InnerText;
                            props.Value = nodProp.Item("Value").InnerText;
                            obs.htblProperties.Add(sPropKey, props);
                        Next;
                        For Each nodDisplayed As XmlElement In nodOb.SelectNodes("Displayed")
                            obs.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                        Next;

                        NewState.htblObjectStates.Add(sKey, obs);
                    }
                Next;

                For Each nodTas As XmlElement In xmlDoc.SelectNodes("/Game/Task")
                    With nodTas;
                        private New clsGameState.clsTaskState tas;
                        private string sKey = .Item("Key").InnerText;
                        tas.Completed = CBool(.Item("Completed").InnerText);
                        If .Item("Scored") != null Then tas.Scored = CBool(.Item("Scored").InnerText)
                        For Each nodDisplayed As XmlElement In nodTas.SelectNodes("Displayed")
                            tas.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                        Next;

                        NewState.htblTaskStates.Add(sKey, tas);
                    }
                Next;

                For Each nodEv As XmlElement In xmlDoc.SelectNodes("/Game/Event")
                    With nodEv;
                        private New clsGameState.clsEventState evs;
                        private string sKey = .Item("Key").InnerText;
                        evs.Status = (.Item("Status")([Enum].Parse(GetType(clsEvent.StatusEnum)).InnerText), clsEvent.StatusEnum);
                        evs.TimerToEndOfEvent = SafeInt(.Item("Timer").InnerText);
                        If .Item("SubEventTime") != null Then evs.iLastSubEventTime = SafeInt(.Item("SubEventTime").InnerText)
                        If .Item("SubEventIndex") != null Then evs.iLastSubEventIndex = SafeInt(.Item("SubEventIndex").InnerText)
                        For Each nodDisplayed As XmlElement In nodEv.SelectNodes("Displayed")
                            evs.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                        Next;
                        NewState.htblEventStates.Add(sKey, evs);
                    }
                Next;

                For Each nodCh As XmlElement In xmlDoc.SelectNodes("/Game/Character")
                    With nodCh;
                        private New clsGameState.clsCharacterState chs;
                        private string sKey = .Item("Key").InnerText;
                        if (Adventure.htblCharacters.ContainsKey(sKey))
                        {
                            chs.Location = New clsCharacterLocation(Adventure.htblCharacters(sKey));
                            if (.Item("ExistWhere") IsNot null)
                            {
                                chs.Location.ExistWhere = (.Item("ExistWhere")([Enum].Parse(GetType(clsCharacterLocation.ExistsWhereEnum)).InnerText), clsCharacterLocation.ExistsWhereEnum);
                            Else
                                chs.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                            }
                            if (.Item("Position") IsNot null)
                            {
                                chs.Location.Position = (.Item("Position")([Enum].Parse(GetType(clsCharacterLocation.PositionEnum)).InnerText), clsCharacterLocation.PositionEnum);
                            Else
                                chs.Location.Position = clsCharacterLocation.PositionEnum.Standing;
                            }
                            if (.Item("LocationKey") IsNot null)
                            {
                                chs.Location.Key = .Item("LocationKey").InnerText;
                            Else
                                chs.Location.Key = "";
                            }
                            For Each nodW As XmlElement In nodCh.SelectNodes("Walk")
                                With nodW;
                                    private New clsGameState.clsCharacterState.clsWalkState ws;
                                    ws.Status = (.Item("Status")([Enum].Parse(GetType(clsWalk.StatusEnum)).InnerText), clsWalk.StatusEnum);
                                    ws.TimerToEndOfWalk = SafeInt(.Item("Timer").InnerText);
                                    chs.lWalks.Add(ws);
                                }
                            Next;
                            For Each nodProp As XmlElement In nodCh.SelectNodes("Property")
                                private New clsGameState.clsCharacterState.clsStateProperty props;
                                private string sPropKey = nodProp.Item("Key").InnerText;
                                props.Value = nodProp.Item("Value").InnerText;
                                chs.htblProperties.Add(sPropKey, props);
                            Next;
                            For Each nodSeen As XmlElement In nodCh.SelectNodes("Seen")
                                chs.lSeenKeys.Add(nodSeen.InnerText);
                            Next;
                            For Each nodDisplayed As XmlElement In nodCh.SelectNodes("Displayed")
                                chs.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                            Next;
                            NewState.htblCharacterStates.Add(sKey, chs);
                        Else
                            DisplayError("Character key " + sKey + " not found in adventure!");
                        }
                    }
                Next;

                For Each nodVar As XmlElement In xmlDoc.SelectNodes("/Game/Variable")
                    With nodVar;
                        private New clsGameState.clsVariableState vars;
                        private string sKey = .Item("Key").InnerText;
                        if (Adventure.htblVariables.ContainsKey(sKey))
                        {
                            private clsVariable v = Adventure.htblVariables(sKey);

                            ReDim vars.Value(v.Length - 1);
                            for (int i = 0; i <= v.Length - 1; i++)
                            {
                                if (.Item("Value_" + i) IsNot null)
                                {
                                    vars.Value(i) = SafeString(.Item("Value_" + i).InnerText);
                                Else
                                    if (v.Type = clsVariable.VariableTypeEnum.Numeric)
                                    {
                                        vars.Value(i) = "0";
                                    Else
                                        vars.Value(i) = "";
                                    }
                                }
                            Next;
                            If .Item("Value") != null Then ' Old style
                                vars.Value(0) = SafeString(.Item("Value").InnerText);
                            }

                            For Each nodDisplayed As XmlElement In nodVar.SelectNodes("Displayed")
                                vars.htblDisplayedDescriptions.Add(nodDisplayed.InnerText, true);
                            Next;
                            NewState.htblVariableStates.Add(sKey, vars);
                        Else
                            DisplayError("Variable key " + sKey + " not found in adventure!");
                        }

                    }
                Next;

                For Each nodGrp As XmlElement In xmlDoc.SelectNodes("/Game/Group")
                    With nodGrp;
                        private New clsGameState.clsGroupState grps;
                        private string sKey = .Item("Key").InnerText;
                        For Each nodMember As XmlElement In nodGrp.SelectNodes("Member")
                            grps.lstMembers.Add(nodMember.InnerText);
                        Next;
                        NewState.htblGroupStates.Add(sKey, grps);
                    }
                Next;

                If .Item("Turns") != null Then Adventure.Turns = SafeInt(.Item("Turns").InnerText)

            }

            return NewState;

        }
        catch (Exception ex)
        {
            ErrMsg("Error loading game state", ex);
        }

        return null;

    }
#endif


    private void Set400SpecificTasks()
    {

        private clsTask tasGetParent = Adventure.htblTasks("TakeObjects") ' Use the parent task, because we don't know if they're being lazy or not...;

        if (Not tasGetParent Is null)
        {
            ' Convert get/drop tasks into Specific tasks - this may not be perfect, but it's a good guess
            For Each tas As clsTask In Adventure.htblTasks.Values
                For Each sCommand As String In tas.arlCommands
                    if (sCommand.Contains("get ") || sCommand = "get *")
                    {
                        ' We've got to work out if a particular object is referenced
                        ' First look to see if the command makes it unique
                        private int iObsFound = 0;
                        private string sFound = "";
                        For Each ob As clsObject In Adventure.htblObjects.Values
                            if (sCommand.Contains(ob.arlNames(0)))
                            {
                                if (Not ob.IsStatic)
                                {
                                    iObsFound += 1;
                                    sFound = ob.Key
                                Else
                                    ' Don't override any tasks where the player is getting something and that something exists as a static object
                                    GoTo NextTask;
                                }
                            }
                        Next;
                        if (iObsFound > 1)
                        {
                            ' Ok, lets look in the restrictions/actions to see if they point us to an exact object
                            For Each rest As clsRestriction In tas.arlRestrictions
                                if (Adventure.htblObjects.ContainsKey(rest.sKey1) && sCommand.Contains(Adventure.htblObjects(rest.sKey1).arlNames(0)))
                                {
                                    sFound = rest.sKey1
                                ElseIf Adventure.htblObjects.ContainsKey(rest.sKey2) && sCommand.Contains(Adventure.htblObjects(rest.sKey2).arlNames(0)) Then
                                    sFound = rest.sKey2
                                }
                                If sFound <> "" Then Exit For
                            Next;
                            if (sFound = "")
                            {
                                For Each act As clsAction In tas.arlActions
                                    if (Adventure.htblObjects.ContainsKey(act.sKey1) && sCommand.Contains(Adventure.htblObjects(act.sKey1).arlNames(0)))
                                    {
                                        sFound = act.sKey1
                                    ElseIf Adventure.htblObjects.ContainsKey(act.sKey2) && sCommand.Contains(Adventure.htblObjects(act.sKey2).arlNames(0)) Then
                                        sFound = act.sKey2
                                    }
                                    If sFound <> "" Then Exit For
                                Next;
                            }
                        }
                        if (sFound <> "" || sCommand = "get *")
                        {
                            tas.TaskType = clsTask.TaskTypeEnum.Specific;
                            tas.GeneralKey = tasGetParent.Key;
                            ReDim tas.Specifics(0);
                            private New clsTask.Specific s;
                            With s;
                                .Multiple = false;
                                .Keys.Add(sFound);
                            }
                            tas.Specifics(0) = s;
                            'If sCommand = "get *" Then
                            '    tas.ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnFail
                            'Else
                            '    tas.ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnNoOutput
                            'End If
                            Exit For;
                        }
                    }
                Next;
NextTask:;
            Next;
        }

        private clsTask tasExamineChar = Adventure.htblTasks("ExamineCharacter") ' Use the parent task, because we don't know if they're being lazy or not...;
        if (tasExamineChar IsNot null)
        {
            ' Make it a system task, i.e. don't run events
            tasExamineChar.bSystemTask = true;
        }
        'Dim tasExamineObjects As clsTask = Adventure.htblTasks("ExamineObjects") ' Use the parent task, because we don't know if they're being lazy or not...
        'If tasExamineObjects IsNot Nothing Then
        '    ' Make it a system task, i.e. don't run events
        '    tasExamineObjects.bSystemTask = True
        'End If

    }



    private void ObfuscateByteArray(ref bytData As Byte()
    {

        private int[] iRandomKey = {41, 236, 221, 117, 23, 189, 44, 187, 161, 96, 4, 147, 90, 91, 172, 159, 244, 50, 249, 140, 190, 244, 82, 111, 170, 217, 13, 207, 25, 177, 18, 4, 3, 221, 160, 209, 253, 69, 131, 37, 132, 244, 21, 4, 39, 87, 56, 203, 119, 139, 231, 180, 190, 13, 213, 53, 153, 109, 202, 62, 175, 93, 161, 239, 77, 0, 143, 124, 186, 219, 161, 175, 175, 212, 7, 202, 223, 77, 72, 83, 160, 66, 88, 142, 202, 93, 70, 246, 8, 107, 55, 144, 122, 68, 117, 39, 83, 37, 183, 39, 199, 188, 16, 155, 233, 55, 5, 234, 6, 11, 86, 76, 36, 118, 158, 109, 5, 19, 36, 239, 185, 153, 115, 79, 164, 17, 52, 106, 94, 224, 118, 185, 150, 33, 139, 228, 49, 188, 164, 146, 88, 91, 240, 253, 21, 234, 107, 3, 166, 7, 33, 63, 0, 199, 109, 46, 72, 193, 246, 216, 3, 154, 139, 37, 148, 156, 182, 3, 235, 185, 60, 73, 111, 145, 151, 94, 169, 118, 57, 186, 165, 48, 195, 86, 190, 55, 184, 206, 180, 93, 155, 111, 197, 203, 143, 189, 208, 202, 105, 121, 51, 104, 24, 237, 203, 216, 208, 111, 48, 15, 132, 210, 136, 60, 51, 211, 215, 52, 102, 92, 227, 232, 79, 142, 29, 204, 131, 163, 2, 217, 141, 223, 12, 192, 134, 61, 23, 214, 139, 230, 102, 73, 158, 165, 216, 201, 231, 137, 152, 187, 230, 155, 99, 12, 149, 75, 25, 138, 207, 254, 85, 44, 108, 86, 129, 165, 197, 200, 182, 245, 187, 1, 169, 128, 245, 153, 74, 170, 181, 83, 229, 250, 11, 70, 243, 242, 123, 0, 42, 58, 35, 141, 6, 140, 145, 58, 221, 71, 35, 51, 4, 30, 210, 162, 0, 229, 241, 227, 22, 252, 1, 110, 212, 123, 24, 90, 32, 37, 99, 142, 42, 196, 158, 123, 209, 45, 250, 28, 238, 187, 188, 3, 134, 130, 79, 199, 39, 105, 70, 14, 0, 151, 234, 46, 56, 181, 185, 138, 115, 54, 25, 183, 227, 149, 9, 63, 128, 87, 208, 210, 234, 213, 244, 91, 63, 254, 232, 81, 44, 81, 51, 183, 222, 85, 142, 146, 218, 112, 66, 28, 116, 111, 168, 184, 161, 4, 31, 241, 121, 15, 70, 208, 152, 116, 35, 43, 163, 142, 238, 58, 204, 103, 94, 34, 2, 97, 217, 142, 6, 119, 100, 16, 20, 179, 94, 122, 44, 59, 185, 58, 223, 247, 216, 28, 11, 99, 31, 105, 49, 98, 238, 75, 129, 8, 80, 12, 17, 134, 181, 63, 43, 145, 234, 2, 170, 54, 188, 228, 22, 168, 255, 103, 213, 180, 91, 213, 143, 65, 23, 159, 66, 111, 92, 164, 136, 25, 143, 11, 99, 81, 105, 165, 133, 121, 14, 77, 12, 213, 114, 213, 166, 58, 83, 136, 99, 135, 118, 205, 173, 123, 124, 207, 111, 22, 253, 188, 52, 70, 122, 145, 167, 176, 129, 196, 63, 89, 225, 91, 165, 13, 200, 185, 207, 65, 248, 8, 27, 211, 64, 1, 162, 193, 94, 231, 213, 153, 53, 111, 124, 81, 25, 198, 91, 224, 45, 246, 184, 142, 73, 9, 165, 26, 39, 159, 178, 194, 0, 45, 29, 245, 161, 97, 5, 120, 238, 229, 81, 153, 239, 165, 35, 114, 223, 83, 244, 1, 94, 238, 20, 2, 79, 140, 137, 54, 91, 136, 153, 190, 53, 18, 153, 8, 81, 135, 176, 184, 193, 226, 242, 72, 164, 30, 159, 164, 230, 51, 58, 212, 171, 176, 100, 17, 25, 27, 165, 20, 215, 206, 29, 102, 75, 147, 100, 221, 11, 27, 32, 88, 162, 59, 64, 123, 252, 203, 93, 48, 237, 229, 80, 40, 77, 197, 18, 132, 173, 136, 238, 54, 225, 156, 225, 242, 197, 140, 252, 17, 185, 193, 153, 202, 19, 226, 49, 112, 111, 232, 20, 78, 190, 117, 38, 242, 125, 244, 24, 134, 128, 224, 47, 130, 45, 234, 119, 6, 90, 78, 182, 112, 206, 76, 118, 43, 75, 134, 20, 107, 147, 162, 20, 197, 116, 160, 119, 107, 117, 238, 116, 208, 115, 118, 144, 217, 146, 22, 156, 41, 107, 43, 21, 33, 50, 163, 127, 114, 254, 251, 166, 247, 223, 173, 242, 222, 203, 106, 14, 141, 114, 11, 145, 107, 217, 229, 253, 88, 187, 156, 153, 53, 233, 235, 255, 104, 141, 243, 146, 209, 33, 5, 109, 122, 72, 125, 240, 198, 131, 178, 14, 40, 8, 15, 182, 95, 153, 169, 71, 77, 166, 38, 182, 97, 97, 113, 13, 244, 173, 138, 80, 215, 215, 61, 107, 108, 157, 22, 35, 91, 244, 55, 213, 8, 142, 113, 44, 217, 52, 159, 206, 228, 171, 68, 42, 250, 78, 11, 24, 215, 112, 252, 24, 249, 97, 54, 80, 202, 164, 74, 194, 131, 133, 235, 88, 110, 81, 173, 211, 240, 68, 51, 191, 13, 187, 108, 44, 147, 18, 113, 30, 146, 253, 76, 235, 247, 30, 219, 167, 88, 32, 97, 53, 234, 221, 75, 94, 192, 236, 188, 169, 160, 56, 40, 146, 60, 61, 10, 62, 245, 10, 189, 184, 50, 43, 47, 133, 57, 0, 97, 80, 117, 6, 122, 207, 226, 253, 212, 48, 112, 14, 108, 166, 86, 199, 125, 89, 213, 185, 174, 186, 20, 157, 178, 78, 99, 169, 2, 191, 173, 197, 36, 191, 139, 107, 52, 154, 190, 88, 175, 63, 105, 218, 206, 230, 157, 22, 98, 107, 174, 214, 175, 127, 81, 166, 60, 215, 84, 44, 107, 57, 251, 21, 130, 170, 233, 172, 27, 234, 147, 227, 155, 125, 10, 111, 80, 57, 207, 203, 176, 77, 71, 151, 16, 215, 22, 165, 110, 228, 47, 92, 69, 145, 236, 118, 68, 84, 88, 35, 252, 241, 250, 119, 215, 203, 59, 50, 117, 225, 86, 2, 8, 137, 124, 30, 242, 99, 4, 171, 148, 68, 61, 55, 186, 55, 157, 9, 144, 147, 43, 252, 225, 171, 206, 190, 83, 207, 191, 68, 155, 227, 47, 140, 142, 45, 84, 188, 20};

        for (int i = 0; i <= bytData.Length - 1; i++)
        {
            If i >= iOffset && (iLength = 0 || i < iLength + iOffset) Then bytData(i) = CByte(bytData(i) Xor iRandomKey((i - iOffset) Mod 1024))
        Next;

    }


    private void Decompress(Byte( bZLib)
    {

        If bObfuscate Then ObfuscateByteArray(bZLib, iOffset, iLength)
        private New System.IO.MemoryStream outStream;
        private New ZOutputStream(outStream) zStream;
        If iLength = 0 Then iLength = bZLib.Length - iOffset
        private New System.IO.MemoryStream(bZLib, iOffset, iLength) inStream;
        try
        {
            If ! CopyStream(inStream, zStream) Then Return null
            return new MemoryStream(outStream.GetBuffer);
        }
        catch (Exception ex)
        {
            ErrMsg("Error decompressing byte array", ex);
        }
        finally
        {
            zStream.Close();
            outStream.Close();
            inStream.Close();
        }

        return null;
    }


    private MemoryStream FileToMemoryStream(bool bCompressed, int iLength, bool bObfuscate)
    {

        'ReDim bAdventure(-1) ' if this needs to be done, why here?

        if (bCompressed)
        {
            Dim bAdvZLib() As Byte = br.ReadBytes(iLength)
            return Decompress(bAdvZLib, bObfuscate);
        Else
            return new MemoryStream(br.ReadBytes(iLength));
        }

    }


    ' Grab the numeric part of the key (if any) and increment it
    private string IncrementKey(string sKey)
    {

        private New System.Text.RegularExpressions.Regex("") re;

        private string sJustKey = System.Text.RegularExpressions.Regex.Replace(sKey, "\d*$", "");
        private string sNumber = sKey.Replace(sJustKey, "");
        If sNumber = "" Then sNumber = "0"
        private int iNumber = CInt(sNumber);
        return sJustKey + iNumber + 1;

    }


private class LoadAbortException
    {
        Inherits Exception;
    }


    private bool bPromptedLibraryOverride = false;
    private DialogResult PerLibraryResponse = DialogResult.None;
private enum LoadItemEum As Integer
    {
        No = 0
        Yes = 1
        Both = 2
    }
    private LoadItemEum ShouldWeLoadLibraryItem(string sItemKey)
    {

        If Adventure.listExcludedItems.Contains(sItemKey) Then Return LoadItemEum.No

#if Runner
        return LoadItemEum.Yes;
#else
        ' We only override library items.  If a player modifies a library item, it is no longer a library item, so we don't overwrite it
        private clsItem itm = null;
        if (Adventure.dictAllItems.TryGetValue(sItemKey, itm))
        {
            If itm.LoadedFromLibrary Then Return LoadItemEum.Yes
            if (Not itm.IsLibrary)
            {
                switch (eOverwriteLibraries)
                {
                    case OverwriteLibrariesEnum.PromptPerItem:
                        {
                        switch (MessageBox.Show("Library item """ + Adventure.dictAllItems(sItemKey).CommonName + """ is newer than the one in your adventure.  However, you have customised this item.  " + vbCrLf + vbCrLf + _)
                        {
                                                    "Select YES to overwrite your custom version with the updated version from the library." + vbCrLf + vbCrLf + "Select NO to keep your custom version, or " + vbCrLf + vbCrLf + "Select CANCEL to keep both versions " + _;
                                                    "(useful in case you want to compare the two, but you will probably need to delete one manually)", "Update from Library", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            case DialogResult.Yes:
                                {
                                return LoadItemEum.Yes;
                            case DialogResult.No:
                                {
                                return LoadItemEum.No;
                            case DialogResult.Cancel:
                                {
                                return LoadItemEum.Both;
                        }
                    default:
                        {
                        return LoadItemEum.No ' Don't override user's custom library;
                }
            }
        }

        switch (eOverwriteLibraries)
        {
            case OverwriteLibrariesEnum.Always:
                {
                return LoadItemEum.Yes;
            case OverwriteLibrariesEnum.Never:
                {
                return LoadItemEum.No;
            case OverwriteLibrariesEnum.PromptPerItem:
                {
                if (Not Adventure.dictAllItems.ContainsKey(sItemKey))
                {
                    return LoadItemEum.Yes;
                Else
                    switch (MessageBox.Show("Library item """ + Adventure.dictAllItems(sItemKey).CommonName + """ is newer than the one in your adventure.  Update?", "Update from Library", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        case DialogResult.Yes:
                            {
                            return LoadItemEum.Yes;
                        case DialogResult.No:
                            {
                            return LoadItemEum.No;
                        case DialogResult.Cancel:
                            {
                            throw New LoadAbortException
                    }
                }
            case OverwriteLibrariesEnum.PromptPerLibrary:
                {
                if (Not Adventure.dictAllItems.ContainsKey(sItemKey))
                {
                    return LoadItemEum.Yes;
                Else
                    if (Not bPromptedLibraryOverride)
                    {
                        bPromptedLibraryOverride = True
                        PerLibraryResponse = MessageBox.Show("Library file is more recent than your adventure.  Do you want to update/insert any newer items?", "Update Library items", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    }
                    if (PerLibraryResponse = DialogResult.Yes)
                    {
                        return LoadItemEum.Yes;
                    Else
                        return LoadItemEum.No;
                    }
                    'Return PerLibraryResponse = DialogResult.Yes
                }
        }
#endif

    }


    private void SetChar(ref sText As String, int iPos, char Character)
    {
        If iPos > sText.Length - 1 Then Exit Sub
        sText = sLeft(sText, iPos) & Character & sRight(sText, sText.Length - iPos - 1)
    }


    private DateTime GetDate(string sDate)
    {

        ' Non UK locals save "yyyy-MM-dd HH:mm:ss" in their own format.  Convert these for pre-5.0.22 saves
        if (dFileVersion < 5.000022)
        {
            if (sDate.Length = 19)
            {
                SetChar(sDate, 13, ":"c);
                SetChar(sDate, 16, ":"c);
            }
        }

        private DateTime dtReturn;
        If Date.TryParse(sDate, dtReturn) Then Return dtReturn

        return Date.MinValue;

    }


    private string SetDate(DateTime dtDate)
    {
        return dtDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
    }


    ' Corrects the bracket sequences for ORs after ANDs
    private string CorrectBracketSequence(string sSequence)
    {

        if (sSequence.Contains("#A#O#"))
        {
            for (int i = 10; i <= 0; i += -1)
            {
                private string sSearch = "#A#";
                for (int j = 0; j <= i; j++)
                {
                    sSearch &= "O#";
                Next;
                while (sSequence.Contains(sSearch))
                {
                    private string sReplace = "#A(#";
                    for (int j = 0; j <= i; j++)
                    {
                        sReplace &= "O#";
                    Next;
                    sReplace &= ")";
                    sSequence = sSequence.Replace(sSearch, sReplace)
                    iCorrectedTasks += 1;
                }
            Next;
        }

        return sSequence;

    }


    private bool GetBool(string sBool)
    {

        switch (sBool.ToUpper)
        {
            case "0":
            case "FALSE":
                {
                return false;
            case "-1":
            case "1":
            case "TRUE":
            case "VRAI":
                {
                return true;
            default:
                {
                return false;
        }

    }


    private bool bCorrectBracketSequences = false;
    private bool bAskedAboutBrackets = false;
    private int iCorrectedTasks = 0;

    private bool Load500(MemoryStream stmMemory, bool bLibrary, bool bAppend = false, LoadWhatEnum eLoadWhat = LoadWhatEnum.All, DateTime dtAdvDate = #1/1/1900#, string sFilename = "")
    {

        try
        {
            If stmMemory == null Then Return false

            private New XmlDocument xmlDoc;
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(stmMemory);
            private clsAdventure a = Adventure;
            private bool bAddDuplicateKeys = true;
            private New StringHashTable htblDuplicateKeyMapping;
            private New StringArrayList arlNewTasks;
            'Dim bOverwrite As Boolean = False

            ' Temp
            'Adventure.iCompassPoints = [Global].DirectionsEnum.Out
            With xmlDoc.Item("Adventure");
                if (.Item("Version") IsNot null)
                {
                    'Dim sVersion As String = .Item("Version").InnerText
                    'Dim sVerSplit() As String = sVersion.Split("."c)
                    'dFileVersion = SafeDbl(sVerSplit(0) & System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & sVerSplit(1))
                    'a.dVersion = 0
                    'For i As Integer = 0 To sVerSplit.Length - 1
                    '    a.dVersion += SafeInt(sVerSplit(i)) * 10 ^ (2 * -i)
                    'Next
                    '.WriteElementString("Version", version.Major & "." & Format(version.Minor, "000") & Format(version.Build, "000"))

                    dFileVersion = SafeDbl(.Item("Version").InnerText)
                    if (dFileVersion > dVersion)
                    {
                        MsgBox("This file is a later version than the software.  It is advisable that you upgrade to ensure it runs properly.", MsgBoxStyle.Exclamation);
                    }
                    If ! bLibrary Then a.dVersion = dFileVersion
                Else
                    throw New Exception("Version tag not specified")
                }

                if (bLibrary && dtAdvDate > #1/1/1900#)
                {
#if Generator
                    ' We are loading a library and already have an adventure loaded.  Check what the user wants to do
                    If eOverwriteLibraries = OverwriteLibrariesEnum.Never Then Return true ' I.e. user never wants libraries to overwrite
                    bPromptedLibraryOverride = False
#endif
                    '' Only load the library if it is more recent than the adventure file
                    'If Not .Item("LastUpdated") Is Nothing AndAlso CDate(.Item("LastUpdated").InnerText) < dtAdvDate Then
                    '    Exit Sub
                    'Else
                    '    ' Library is more recent than our adventure
                    '    If MessageBox.Show("Library file is more recent than your adventure.  Do you want to update/insert any newer items?", "Update Library items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
                    '    bOverwrite = True
                    'End If
                }
                if (eLoadWhat = LoadWhatEnum.All)
                {
                    bAskedAboutBrackets = False
                    iCorrectedTasks = 0

                    If ! bLibrary && .Item("Title") != null Then a.Title = .Item("Title").InnerText
                    If ! bLibrary && .Item("Author") != null Then a.Author = .Item("Author").InnerText
                    If .Item("LastUpdated") != null Then a.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                    If ! .Item("Introduction") == null Then a.Introduction = LoadDescription(xmlDoc.Item("Adventure"), "Introduction") ' New Description(.Item("Introduction").InnerText)
                    If .Item("FontName") != null Then a.DefaultFontName = .Item("FontName").InnerText
                    If .Item("FontSize") != null Then a.DefaultFontSize = SafeInt(.Item("FontSize").InnerText)
#if Runner
                    a.DeveloperDefaultBackgroundColour = null;
                    a.DeveloperDefaultInputColour = null;
                    a.DeveloperDefaultOutputColour = null;
                    a.DeveloperDefaultLinkColour = null;
#endif
                    If .Item("BackgroundColour") != null Then a.DeveloperDefaultBackgroundColour = ColorTranslator.FromOle(CInt(.Item("BackgroundColour").InnerText))
                    If .Item("InputColour") != null Then a.DeveloperDefaultInputColour = ColorTranslator.FromOle(CInt(.Item("InputColour").InnerText))
                    If .Item("OutputColour") != null Then a.DeveloperDefaultOutputColour = ColorTranslator.FromOle(CInt(.Item("OutputColour").InnerText))
                    If .Item("LinkColour") != null Then a.DeveloperDefaultLinkColour = ColorTranslator.FromOle(CInt(.Item("LinkColour").InnerText))
                    If .Item("ShowFirstLocation") != null Then a.ShowFirstRoom = GetBool(.Item("ShowFirstLocation").InnerText)
                    If .Item("UserStatus") != null Then a.sUserStatus = .Item("UserStatus").InnerText
                    'If Not .Item("V4Compatibility") Is Nothing Then a.bV4Compatibility = CBool(.Item("V4Compatibility").InnerText)
                    '#If Generator Then
                    'If Not .Item("IFID") Is Nothing Then a.BabelTreatyInfo.Stories(0).Identification.IFID = .Item("IFID").InnerText
                    If .Item("ifindex") != null Then Adventure.BabelTreatyInfo.FromString(.Item("ifindex").OuterXml) ' Pre 5.0.20
                    if (.Item("Cover") IsNot null)
                    {
                        'If Adventure.BabelTreatyInfo.Stories(0).Cover Is Nothing Then Adventure.BabelTreatyInfo.Stories(0).Cover = New clsBabelTreatyInfo.clsStory.clsCover
                        'Adventure.BabelTreatyInfo.Stories(0).Cover.Filename = .Item("Cover").InnerText
                        Adventure.CoverFilename = .Item("Cover").InnerText;
                    }
                    '#End If
                    If .Item("ShowExits") != null Then a.ShowExits = GetBool(.Item("ShowExits").InnerText)
                    If .Item("EnableMenu") != null Then a.EnableMenu = GetBool(.Item("EnableMenu").InnerText)
                    If .Item("EnableDebugger") != null Then a.EnableDebugger = GetBool(.Item("EnableDebugger").InnerText)
                    If .Item("EndGameText") != null Then a.WinningText = LoadDescription(xmlDoc.Item("Adventure"), "EndGameText")
                    If .Item("Elapsed") != null Then a.iElapsed = SafeInt(.Item("Elapsed").InnerText)
                    If .Item("TaskExecution") != null Then a.TaskExecution = (.Item("TaskExecution")([Enum].Parse(GetType(clsAdventure.TaskExecutionEnum)).InnerText), clsAdventure.TaskExecutionEnum)
                    If .Item("WaitTurns") != null Then a.WaitTurns = SafeInt(.Item("WaitTurns").InnerText)
                    if (.Item("KeyPrefix") IsNot null)
                    {
                        a.KeyPrefix = .Item("KeyPrefix").InnerText;
#if Generator
                        fGenerator.KeyPrefix = a.KeyPrefix;
#endif
                    }

                    If .Item("DirectionNorth") != null && .Item("DirectionNorth").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.North) = .Item("DirectionNorth").InnerText
                    If .Item("DirectionNorthEast") != null && .Item("DirectionNorthEast").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.NorthEast) = .Item("DirectionNorthEast").InnerText
                    If .Item("DirectionEast") != null && .Item("DirectionEast").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.East) = .Item("DirectionEast").InnerText
                    If .Item("DirectionSouthEast") != null && .Item("DirectionSouthEast").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.SouthEast) = .Item("DirectionSouthEast").InnerText
                    If .Item("DirectionSouth") != null && .Item("DirectionSouth").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.South) = .Item("DirectionSouth").InnerText
                    If .Item("DirectionSouthWest") != null && .Item("DirectionSouthWest").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.SouthWest) = .Item("DirectionSouthWest").InnerText
                    If .Item("DirectionWest") != null && .Item("DirectionWest").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.West) = .Item("DirectionWest").InnerText
                    If .Item("DirectionNorthWest") != null && .Item("DirectionNorthWest").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.NorthWest) = .Item("DirectionNorthWest").InnerText
                    If .Item("DirectionIn") != null && .Item("DirectionIn").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.In) = .Item("DirectionIn").InnerText
                    If .Item("DirectionOut") != null && .Item("DirectionOut").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.Out) = .Item("DirectionOut").InnerText
                    If .Item("DirectionUp") != null && .Item("DirectionUp").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.Up) = .Item("DirectionUp").InnerText
                    If .Item("DirectionDown") != null && .Item("DirectionDown").InnerText <> "" Then a.sDirectionsRE(DirectionsEnum.Down) = .Item("DirectionDown").InnerText

                }

                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.AllExceptProperties)
                {
                    Debug.WriteLine("End Intro: " + Now);

#if Generator
                    ' Folders
                    For Each nodFol As XmlElement In xmlDoc.SelectNodes("/Adventure/Folder")
                        With nodFol;
                            private string sKey = .Item("Key").InnerText;
                            private clsFolder folder = null;
                            private bool bAppending = false;
                            ' If we load a module, only add folders if they don't already exist.
                            ' If they do, just merge the contents
                            '
                            if (a.dictFolders.ContainsKey(sKey))
                            {
                                if (a.dictFolders(sKey).Name = .Item("Name").InnerText)
                                {
                                    ' Ok, the imported folder is the same as one we have.  Use our existing one
                                    folder = a.dictFolders(sKey)
                                    bAppending = True
                                }
                                if (folder Is null)
                                {
                                    if (a.dictFolders.ContainsKey(sKey))
                                    {
                                        if ((.Item("Library") IsNot null && GetBool(.Item("Library").InnerText)) || bLibrary)
                                        {
                                            If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.dictFolders(sKey).LastUpdated Then GoTo NextFol
                                            'If ShouldWeLoadLibraryItem(sKey) Then a.dictFolders.Remove(sKey)
                                            switch (ShouldWeLoadLibraryItem(sKey))
                                            {
                                                case LoadItemEum.Yes:
                                                    {
                                                    a.dictFolders.Remove(sKey);
                                                case LoadItemEum.No:
                                                    {
                                                    GoTo NextFol;
                                                case LoadItemEum.Both:
                                                    {
                                                    ' Keep key, but still add this new one
                                            }
                                        }
                                    }
                                    if (bAddDuplicateKeys)
                                    {
                                        while (a.dictFolders.ContainsKey(sKey))
                                        {
                                            sKey = IncrementKey(sKey)
                                        }
                                    Else
                                        GoTo NextFol;
                                    }
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextFol;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            if (Not bAppending)
                            {
                                folder = New clsFolder(sKey)
                                folder.Name = .Item("Name").InnerText;
                            }
                            For Each nodMember As XmlElement In nodFol.SelectNodes("Member")
                                if (Not folder.Members.Contains(nodMember.InnerText))
                                {
                                    ' Make sure this item doesn't exist in any other folder
                                    private string sCurrentFolderKey = "";
                                    For Each f As clsFolder In Adventure.dictFolders.Values
                                        if (f.Members.Contains(nodMember.InnerText))
                                        {
                                            sCurrentFolderKey = f.Key
                                            Exit For;
                                        }
                                    Next;
                                    ' If we're not a library, force the item into the new folder if necessary
                                    if (sCurrentFolderKey = "" || Not bLibrary)
                                    {
                                        if (sCurrentFolderKey <> "")
                                        {
                                            ' Remove from the current folder
                                            Adventure.dictFolders(sCurrentFolderKey).Members.Remove(nodMember.InnerText);
                                        }
                                        folder.Members.Add(nodMember.InnerText);
                                    }
                                }
                            Next;
                            if (Not bAppending)
                            {
                                If .Item("Expanded") != null Then folder.Expanded = GetBool(.Item("Expanded").InnerText)
                                If .Item("Height") != null Then folder.Size.Height = SafeInt(.Item("Height").InnerText)
                                If .Item("Width") != null Then folder.Size.Width = SafeInt(.Item("Width").InnerText)
                                If .Item("Visible") != null Then folder.Visible = GetBool(.Item("Visible").InnerText)
                                If .Item("X") != null Then folder.Location.X = SafeInt(.Item("X").InnerText)
                                If .Item("Y") != null Then folder.Location.Y = SafeInt(.Item("Y").InnerText)
                                If .Item("Type") != null Then folder.ViewType = (.Item("Type")([Enum].Parse(GetType(Infragistics.Win.UltraWinListView.UltraListViewStyle)).InnerText), Infragistics.Win.UltraWinListView.UltraListViewStyle)
                                If .Item("SortColumn") != null Then folder.SortColumn = SafeInt(.Item("SortColumn").InnerText)
                                If .Item("SortDirection") != null Then folder.SortDirection = (.Item("SortDirection")([Enum].Parse(GetType(Infragistics.Win.UltraWinListView.Sorting)).InnerText), Infragistics.Win.UltraWinListView.Sorting)
                                If .Item("GroupBy") != null Then folder.GroupBy = SafeInt(.Item("GroupBy").InnerText)
                                If .Item("GroupDirection") != null Then folder.GroupDirection = (.Item("GroupDirection")([Enum].Parse(GetType(Infragistics.Win.UltraWinListView.Sorting)).InnerText), Infragistics.Win.UltraWinListView.Sorting)
                                If .Item("ShowCreatedDate") != null Then folder.ShowCreatedDate = GetBool(.Item("ShowCreatedDate").InnerText)
                                If .Item("ShowModifiedDate") != null Then folder.ShowModifiedDate = GetBool(.Item("ShowModifiedDate").InnerText)
                                If .Item("ShowType") != null Then folder.ShowType = GetBool(.Item("ShowType").InnerText)
                                If .Item("ShowKey") != null Then folder.ShowKey = GetBool(.Item("ShowKey").InnerText)
                                If .Item("ShowPriority") != null Then folder.ShowPriority = GetBool(.Item("ShowPriority").InnerText)

                                If ! .Item("Library") == null Then folder.IsLibrary = GetBool(.Item("Library").InnerText)
                                if (bLibrary)
                                {
                                    folder.IsLibrary = true;
                                    folder.LoadedFromLibrary = true;
                                }

                                If ! .Item("LastUpdated") == null Then folder.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                                a.dictFolders.Add(folder.Key, folder);
                            }
                        }
NextFol:;
                    Next;
#endif
                }


                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.Properies)
                {
                    ' Properties
                    For Each nodProp As XmlElement In xmlDoc.SelectNodes("/Adventure/Property")
                        private New clsProperty prop;
                        With nodProp;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then prop.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblAllProperties.ContainsKey(sKey))
                            {
                                if (a.htblAllProperties.ContainsKey(sKey))
                                {
                                    if (prop.IsLibrary || bLibrary)
                                    {
                                        If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblAllProperties(sKey).LastUpdated Then GoTo NextProp
                                        'If ShouldWeLoadLibraryItem(sKey) Then a.htblAllProperties.Remove(sKey)
                                        switch (ShouldWeLoadLibraryItem(sKey))
                                        {
                                            case LoadItemEum.Yes:
                                                {
                                                a.htblAllProperties.Remove(sKey);
                                            case LoadItemEum.No:
                                                {
                                                GoTo NextProp;
                                            case LoadItemEum.Both:
                                                {
                                                ' Keep key, but still add this new one
                                        }
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblAllProperties.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextProp;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextProp;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            prop.Key = sKey;
                            if (bLibrary)
                            {
                                prop.IsLibrary = true;
                                prop.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then prop.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                            prop.Description = .Item("Description").InnerText;
                            if (Not .Item("Mandatory") Is null)
                            {
                                prop.Mandatory = GetBool(.Item("Mandatory").InnerText);
                            }
                            If .Item("PropertyOf") != null Then prop.PropertyOf = EnumParsePropertyPropertyOf(.Item("PropertyOf").InnerText)
                            If .Item("AppendTo") != null Then prop.AppendToProperty = .Item("AppendTo").InnerText
                            prop.Type = EnumParsePropertyType(.Item("Type").InnerText);
                            switch (prop.Type)
                            {
                                case clsProperty.PropertyTypeEnum.StateList:
                                    {
                                    For Each nodState As XmlElement In nodProp.SelectNodes("State")
                                        prop.arlStates.Add(nodState.InnerText);
                                    Next;
                                    If prop.arlStates.Count > 0 Then prop.Value = prop.arlStates(0)
                                case clsProperty.PropertyTypeEnum.ValueList:
                                    {
                                    '.WriteStartElement("ValueList")
                                    'For Each sKey As String In prop.ValueList.Keys
                                    '    .WriteElementString("Label", sKey)
                                    '    .WriteElementString("Value", prop.ValueList(sKey).ToString)
                                    'Next
                                    '.WriteEndElement()
                                    For Each nodValueList As XmlElement In nodProp.SelectNodes("ValueList")
                                        if (nodValueList.Item("Label") IsNot null)
                                        {
                                            private string sLabel = nodValueList("Label").InnerText;
                                            private int iValue = 0;
                                            If nodValueList.Item("Value") != null Then iValue = SafeInt(nodValueList("Value").InnerText)
                                            prop.ValueList.Add(sLabel, iValue);
                                        }
                                    Next;
                            }
                            If .Item("PrivateTo") != null Then prop.PrivateTo = .Item("PrivateTo").InnerText
                            If .Item("Tooltip") != null Then prop.PopupDescription = .Item("Tooltip").InnerText

                            if (Not .Item("DependentKey") Is null)
                            {
                                if (.Item("DependentKey").InnerText <> sKey)
                                {
                                    prop.DependentKey = .Item("DependentKey").InnerText;
                                    if (Not .Item("DependentValue") Is null)
                                    {
                                        prop.DependentValue = .Item("DependentValue").InnerText;
                                    }
                                }
                            }
                            if (Not .Item("RestrictProperty") Is null)
                            {
                                prop.RestrictProperty = .Item("RestrictProperty").InnerText;
                                if (Not .Item("RestrictValue") Is null)
                                {
                                    prop.RestrictValue = .Item("RestrictValue").InnerText;
                                }
                            }
                        }
                        a.htblAllProperties.Add(prop);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(prop.Key)
                        '#End If
NextProp:;
                    Next;
                    a.htblAllProperties.SetSelected();
                    Debug.WriteLine("End Properties: " + Now);

                    CreateMandatoryProperties();
                }



                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.AllExceptProperties)
                {
                    ' Locations
                    For Each nodLoc As XmlElement In xmlDoc.SelectNodes("/Adventure/Location") ' .GetElementsByTagName("Location")
                        private New clsLocation loc;
                        With nodLoc;
                            private string sKey = .Item("Key").InnerText;
                            If ! .Item("Library") == null Then loc.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblLocations.ContainsKey(sKey))
                            {
                                if (a.htblLocations.ContainsKey(sKey))
                                {
                                    if (loc.IsLibrary || bLibrary)
                                    {
                                        If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblLocations(sKey).LastUpdated Then GoTo NextLoc
                                        'If ShouldWeLoadLibraryItem(sKey) Then a.htblLocations.Remove(sKey)
                                        switch (ShouldWeLoadLibraryItem(sKey))
                                        {
                                            case LoadItemEum.Yes:
                                                {
                                                a.htblLocations.Remove(sKey);
                                            case LoadItemEum.No:
                                                {
                                                GoTo NextLoc;
                                            case LoadItemEum.Both:
                                                {
                                                ' Keep key, but still add this new one
                                        }
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblLocations.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextLoc;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextLoc;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            loc.Key = sKey;
                            if (bLibrary)
                            {
                                loc.IsLibrary = true;
                                loc.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then loc.LastUpdated = CDate(.Item("LastUpdated").InnerText)
                            if (dFileVersion < 5.000015)
                            {
                                loc.ShortDescription = New Description(.Item("ShortDescription").InnerText);
                            Else
                                loc.ShortDescription = LoadDescription(nodLoc, "ShortDescription") ' .Item("ShortDescription").InnerText;
                            }
                            loc.LongDescription = LoadDescription(nodLoc, "LongDescription") ' .Item("LongDescription").InnerText;

                            For Each nodDir As XmlElement In nodLoc.SelectNodes("Movement") ' .GetElementsByTagName("Direction")
                                private DirectionsEnum xdir = EnumParseDirections(nodDir.Item("Direction").InnerText);

                                With loc.arlDirections(xdir);
                                    .LocationKey = nodDir.Item("Destination").InnerText;
                                    .Restrictions = LoadRestrictions(nodDir);
                                }
                            Next;
                            For Each nodProp As XmlElement In .SelectNodes("Property") ' .GetElementsByTagName("Property")
                                private New clsProperty prop;
                                private string sPropKey = nodProp.Item("Key").InnerText;
                                if (Adventure.htblAllProperties.ContainsKey(sPropKey))
                                {
                                    prop = Adventure.htblAllProperties(sPropKey).Copy
                                    if (prop.Type = clsProperty.PropertyTypeEnum.Text)
                                    {
                                        prop.StringData = LoadDescription(nodProp, "Value");
                                    ElseIf prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                                        prop.Value = nodProp.Item("Value").InnerText;
                                    }
                                    prop.Selected = true;
                                    'loc.htblActualProperties.Add(prop)
                                    'loc.bCalculatedGroups = False
                                    loc.AddProperty(prop);
                                }
                            Next;
                            If .Item("Hide") != null Then loc.HideOnMap = GetBool(.Item("Hide").InnerText)
                        }
                        a.htblLocations.Add(loc, loc.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(loc.Key)
                        '#End If
NextLoc:;
                    Next nodLoc;
                    Debug.WriteLine("End Locations: " + Now);
                }



                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.AllExceptProperties)
                {
                    ' Objects
                    For Each nodOb As XmlElement In .SelectNodes("/Adventure/Object")
                        private New clsObject ob;
                        With nodOb;
                            private string sKey = .Item("Key").InnerText;
                            If ! .Item("Library") == null Then ob.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblObjects.ContainsKey(sKey))
                            {
                                if (a.htblObjects.ContainsKey(sKey))
                                {
                                    if (ob.IsLibrary || bLibrary)
                                    {
                                        If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblObjects(sKey).LastUpdated Then GoTo Nextob
                                        'If ShouldWeLoadLibraryItem(sKey) Then a.htblObjects.Remove(sKey)
                                        switch (ShouldWeLoadLibraryItem(sKey))
                                        {
                                            case LoadItemEum.Yes:
                                                {
                                                a.htblObjects.Remove(sKey);
                                            case LoadItemEum.No:
                                                {
                                                GoTo NextOb;
                                            case LoadItemEum.Both:
                                                {
                                                ' Keep key, but still add this new one
                                        }
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblObjects.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextOb;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextOb;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            ob.Key = sKey;
                            if (bLibrary)
                            {
                                ob.IsLibrary = true;
                                ob.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then ob.LastUpdated = CDate(.Item("LastUpdated").InnerText)
                            If .Item("Article") != null Then ob.Article = .Item("Article").InnerText
                            If .Item("Prefix") != null Then ob.Prefix = .Item("Prefix").InnerText
                            For Each nodName As XmlElement In .GetElementsByTagName("Name")
                                ob.arlNames.Add(nodName.InnerText);
                            Next;
                            ob.Description = LoadDescription(nodOb, "Description") ' .Item("Description").InnerText;

                            For Each nodProp As XmlElement In .SelectNodes("Property") ' .GetElementsByTagName("Property")
                                private New clsProperty prop;
                                private string sPropKey = nodProp.Item("Key").InnerText;
                                if (Adventure.htblAllProperties.ContainsKey(sPropKey))
                                {
                                    prop = Adventure.htblAllProperties(sPropKey).Copy
                                    if (prop.Type = clsProperty.PropertyTypeEnum.Text)
                                    {
                                        prop.StringData = LoadDescription(nodProp, "Value");
                                    ElseIf prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                                        prop.Value = nodProp.Item("Value").InnerText;
                                    }
                                    'prop = Adventure.htblAllProperties(nodProp.NamespaceURI).Copy
                                    'prop.Value = nodProp.InnerText
                                    prop.Selected = true;
                                    'ob.htblActualProperties.Add(prop)
                                    'ob.bCalculatedGroups = False
                                    ob.AddProperty(prop);
                                }
                            Next;
                            ob.Location = ob.Location ' Assigns the location object from the object properties;
                        }
                        a.htblObjects.Add(ob, ob.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(ob.Key)
                        '#End If
NextOb:;
                    Next;
                    Debug.WriteLine("End Objects: " + Now);

                    ' Tasks
                    For Each nodTask As XmlElement In .SelectNodes("/Adventure/Task")
                        private New clsTask tas;
                        With nodTask;
                            'Debug.WriteLine("Task1: " & Now.ToString("mm:ss.fff"))
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then tas.IsLibrary = GetBool(.Item("Library").InnerText)
                            If .Item("ReplaceTask") != null Then tas.ReplaceDuplicateKey = GetBool(.Item("ReplaceTask").InnerText)
                            if (a.htblTasks.ContainsKey(sKey))
                            {
                                if (tas.IsLibrary || bLibrary)
                                {
                                    ' We skip loading the task if it is not newer than the one we currently have loaded
                                    ' If there's no timestamp, we have to assume it is newer
                                    If .Item("LastUpdated") != null && CDate(.Item("LastUpdated").InnerText) <= a.htblTasks(sKey).LastUpdated Then GoTo NextTask
                                    'If Not a.htblTasks(sKey).IsLibrary Then GoTo NextTask ' If the user modified a library task, it's no longer marked as library, so don't import the updated library item
                                    ' If a library item is newer than the library in your game, prompt
                                    'If Not tas.ReplaceDuplicateKey AndAlso ShouldWeLoadLibraryItem(sKey) Then a.htblTasks.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblTasks.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            ' Set the timestamp of the custom version to now, so it's more recent than the "newer" library.  That way we won't be prompted next time
                                            a.htblTasks(sKey).LastUpdated = Now;
                                            GoTo NextTask;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (tas.ReplaceDuplicateKey)
                                {
                                    If a.htblTasks.ContainsKey(sKey) Then a.htblTasks.Remove(sKey)
                                Else
                                    if (bAddDuplicateKeys)
                                    {
                                        private string sOldKey = sKey;
                                        while (a.htblTasks.ContainsKey(sKey))
                                        {
                                            sKey = IncrementKey(sKey)
                                        }
                                        htblDuplicateKeyMapping.Add(sOldKey, sKey);
                                    Else
                                        GoTo NextTask;
                                    }
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextTask;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            tas.Key = sKey;
                            tas.Priority = CInt(.Item("Priority").InnerText);
                            If bLibrary && ! tas.IsLibrary Then tas.Priority += 50000
                            if (bLibrary)
                            {
                                tas.IsLibrary = true;
                                tas.LoadedFromLibrary = true;
                            }
                            'If .Item("Library") IsNot Nothing Then tas.IsLibrary = GetBool(.Item("Library").InnerText)
                            If ! .Item("LastUpdated") == null Then tas.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                            tas.Priority = CInt(.Item("Priority").InnerText) '+ iStartPriority;
                            If ! .Item("AutoFillPriority") == null Then tas.AutoFillPriority = CInt(.Item("AutoFillPriority").InnerText)
                            tas.TaskType = EnumParseTaskType(.Item("Type").InnerText);
                            'Debug.WriteLine("Task2: " & Now.ToString("mm:ss.fff"))
                            tas.CompletionMessage = LoadDescription(nodTask, "CompletionMessage") ' .Item("CompletionMessage").InnerText;
                            switch (tas.TaskType)
                            {
                                case clsTask.TaskTypeEnum.General:
                                    {
                                    For Each nodCommand As XmlElement In .GetElementsByTagName("Command")  '.SelectNodes("Command")
#if Generator
                                        tas.arlCommands.Add(nodCommand.InnerText);
#else
                                        ' Simplify Runner so it only has to deal with multiple, or specific refs
                                        tas.arlCommands.Add(FixInitialRefs(nodCommand.InnerText));
#endif
                                    Next;
                                case clsTask.TaskTypeEnum.Specific:
                                    {
                                    tas.GeneralKey = .Item("GeneralTask").InnerText;
                                    private int iSpecCount = 0;
                                    ReDim tas.Specifics(-1);
                                    For Each nodSpec As XmlElement In .GetElementsByTagName("Specific") ' .SelectNodes("Specific")
                                        private New clsTask.Specific spec;
                                        iSpecCount += 1;
                                        spec.Type = EnumParseSpecificType(nodSpec.Item("Type").InnerText);
                                        spec.Multiple = GetBool(nodSpec.Item("Multiple").InnerText);
                                        For Each nodKey As XmlElement In nodSpec.GetElementsByTagName("Key")
                                            spec.Keys.Add(nodKey.InnerText);
                                        Next;
                                        ReDim Preserve tas.Specifics(iSpecCount - 1);
                                        tas.Specifics(iSpecCount - 1) = spec;
                                    Next;
                                    If .Item("ExecuteParentActions") != null Then ' Old checkbox method
                                        if (GetBool(.Item("ExecuteParentActions").InnerText))
                                        {
                                            if (tas.CompletionMessage.ToString(true) = "")
                                            {
                                                tas.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions;
                                            Else
                                                tas.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeActionsOnly;
                                            }
                                        Else
                                            if (tas.CompletionMessage.ToString(true) = "")
                                            {
                                                tas.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextOnly;
                                            Else
                                                tas.SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.Override;
                                            }
                                        }
                                    }
                                    if (.Item("SpecificOverrideType") IsNot null)
                                    {
                                        tas.SpecificOverrideType = (.Item("SpecificOverrideType")([Enum].Parse(GetType(clsTask.SpecificOverrideTypeEnum)).InnerText), clsTask.SpecificOverrideTypeEnum);
                                    }
                                case clsTask.TaskTypeEnum.System:
                                    {
                                    If .Item("RunImmediately") != null Then tas.RunImmediately = GetBool(.Item("RunImmediately").InnerText)
                                    If .Item("LocationTrigger") != null Then tas.LocationTrigger = .Item("LocationTrigger").InnerText
                            }
                            'Debug.WriteLine("Task3: " & Now.ToString("mm:ss.fff"))
                            tas.Description = .Item("Description").InnerText;
                            tas.Repeatable = GetBool(.Item("Repeatable").InnerText);
                            If .Item("Aggregate") != null Then tas.AggregateOutput = GetBool(.Item("Aggregate").InnerText)
                            if (.Item("Continue") IsNot null)
                            {
                                switch (.Item("Continue").InnerText)
                                {
                                    case "ContinueNever":
                                    case "ContinueOnFail":
                                    case "ContinueOnNoOutput":
                                        {
                                        tas.ContinueToExecuteLowerPriority = false;
                                    case "ContinueAlways":
                                        {
                                        tas.ContinueToExecuteLowerPriority = true;
                                }
                            }
                            'tas.ContinueToExecuteLowerPriority = CType([Enum].Parse(GetType(clsTask.ContinueEnum), .Item("Continue").InnerText), clsTask.ContinueEnum)
                            If .Item("LowPriority") != null Then tas.LowPriority = GetBool(.Item("LowPriority").InnerText)
                            'Debug.WriteLine("Task4: " & Now.ToString("mm:ss.fff"))
                            tas.arlRestrictions = LoadRestrictions(nodTask);
                            'Debug.WriteLine("Task5: " & Now.ToString("mm:ss.fff"))
                            tas.arlActions = LoadActions(nodTask);
                            'Debug.WriteLine("Task6: " & Now.ToString("mm:ss.fff"))
                            'If Not .Item("FailOverride") Is Nothing Then tas.FailOverride = .Item("FailOverride").InnerText
                            tas.FailOverride = LoadDescription(nodTask, "FailOverride");
                            If ! .Item("MessageBeforeOrAfter") == null Then tas.eDisplayCompletion = EnumParseBeforeAfter(.Item("MessageBeforeOrAfter").InnerText) Else tas.eDisplayCompletion = clsTask.BeforeAfterEnum.Before
                            If .Item("PreventOverriding") != null Then tas.PreventOverriding = GetBool(.Item("PreventOverriding").InnerText)
                        }
                        a.htblTasks.Add(tas, tas.Key);
                        arlNewTasks.Add(tas.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(tas.Key)
                        '#End If
NextTask:;
                    Next;
                    Debug.WriteLine("End Tasks: " + Now);


                    For Each nodEvent As XmlElement In .SelectNodes("/Adventure/Event")
                        private New clsEvent ev;
                        With nodEvent;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then ev.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblEvents.ContainsKey(sKey))
                            {
                                if (a.htblEvents.ContainsKey(sKey))
                                {
                                    if (ev.IsLibrary || bLibrary)
                                    {
                                        If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblEvents(sKey).LastUpdated Then GoTo NextEvent
                                        'If ShouldWeLoadLibraryItem(sKey) Then a.htblEvents.Remove(sKey)
                                        switch (ShouldWeLoadLibraryItem(sKey))
                                        {
                                            case LoadItemEum.Yes:
                                                {
                                                a.htblEvents.Remove(sKey);
                                            case LoadItemEum.No:
                                                {
                                                GoTo NextEvent;
                                            case LoadItemEum.Both:
                                                {
                                                ' Keep key, but still add this new one
                                        }
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblEvents.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextEvent;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextEvent;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            ev.Key = sKey;
                            if (bLibrary)
                            {
                                ev.IsLibrary = true;
                                ev.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then ev.LastUpdated = CDate(.Item("LastUpdated").InnerText)
                            If .Item("Type") != null Then ev.EventType = (.Item("Type")([Enum].Parse(GetType(clsEvent.EventTypeEnum)).InnerText), clsEvent.EventTypeEnum)
                            ev.Description = .Item("Description").InnerText;
                            ev.WhenStart = (.Item("WhenStart")([Enum].Parse(GetType(clsEvent.WhenStartEnum)).InnerText), clsEvent.WhenStartEnum);
                            If .Item("Repeating") != null Then ev.Repeating = GetBool(.Item("Repeating").InnerText)
                            If .Item("RepeatCountdown") != null Then ev.RepeatCountdown = GetBool(.Item("RepeatCountdown").InnerText)

                            Dim sData() As String
                            if (.Item("StartDelay") IsNot null)
                            {
                                sData = .Item("StartDelay").InnerText.Split(" "c)
                                ev.StartDelay.iFrom = CInt(sData(0));
                                if (sData.Length = 1)
                                {
                                    ev.StartDelay.iTo = CInt(sData(0));
                                Else
                                    ev.StartDelay.iTo = CInt(sData(2));
                                }
                            }
                            sData = .Item("Length").InnerText.Split(" "c)
                            ev.Length.iFrom = CInt(sData(0));
                            if (sData.Length = 1)
                            {
                                ev.Length.iTo = CInt(sData(0));
                            Else
                                ev.Length.iTo = CInt(sData(2));
                            }

                            For Each nodCtrl As XmlElement In nodEvent.GetElementsByTagName("Control")
                                private New EventOrWalkControl ctrl;
                                sData = nodCtrl.InnerText.Split(" "c)
                                ctrl.eControl = (sData(0)([Enum].Parse(GetType(EventOrWalkControl.ControlEnum))), EventOrWalkControl.ControlEnum);
                                ctrl.eCompleteOrNot = (sData(1)([Enum].Parse(GetType(EventOrWalkControl.CompleteOrNotEnum))), EventOrWalkControl.CompleteOrNotEnum);
                                ctrl.sTaskKey = sData(2);
                                ReDim Preserve ev.EventControls(ev.EventControls.Length);
                                ev.EventControls(ev.EventControls.Length - 1) = ctrl;
                            Next;

                            For Each nodSubEvent As XmlElement In nodEvent.GetElementsByTagName("SubEvent")
                                private New clsEvent.SubEvent(ev.Key) se;
                                sData = nodSubEvent.Item("When").InnerText.Split(" "c)

                                se.ftTurns.iFrom = CInt(sData(0));
                                if (sData.Length = 4)
                                {
                                    se.ftTurns.iTo = CInt(sData(2));
                                    se.eWhen = (sData(3)([Enum].Parse(GetType(clsEvent.SubEvent.WhenEnum)).ToString), clsEvent.SubEvent.WhenEnum);
                                Else
                                    se.ftTurns.iTo = CInt(sData(0));
                                    se.eWhen = (sData(1)([Enum].Parse(GetType(clsEvent.SubEvent.WhenEnum)).ToString), clsEvent.SubEvent.WhenEnum);
                                }

                                if (nodSubEvent.Item("Action") IsNot null)
                                {
                                    if (nodSubEvent.Item("Action").InnerXml.Contains("<Description>"))
                                    {
                                        se.oDescription = LoadDescription(nodSubEvent, "Action");
                                    Else
                                        sData = nodSubEvent.Item("Action").InnerText.Split(" "c)
                                        se.eWhat = (sData(0)([Enum].Parse(GetType(clsEvent.SubEvent.WhatEnum)).ToString), clsEvent.SubEvent.WhatEnum);
                                        se.sKey = sData(1);
                                    }
                                }

                                If nodSubEvent.Item("Measure") != null Then se.eMeasure = EnumParseSubEventMeasure(nodSubEvent("Measure").InnerText)
                                If nodSubEvent.Item("What") != null Then se.eWhat = EnumParseSubEventWhat(nodSubEvent("What").InnerText)
                                If nodSubEvent.Item("OnlyApplyAt") != null Then se.sKey = nodSubEvent.Item("OnlyApplyAt").InnerText

                                ReDim Preserve ev.SubEvents(ev.SubEvents.Length);
                                ev.SubEvents(ev.SubEvents.Length - 1) = se;
                            Next;

                        }

                        a.htblEvents.Add(ev, ev.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(ev.Key)
                        '#End If
NextEvent:;
                    Next;



                    For Each nodChar As XmlElement In .SelectNodes("/Adventure/Character")
                        private New clsCharacter chr;
                        With nodChar;
                            private string sKey = .Item("Key").InnerText;
                            If ! .Item("Library") == null Then chr.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblCharacters.ContainsKey(sKey))
                            {
                                if (a.htblCharacters.ContainsKey(sKey))
                                {
                                    if (chr.IsLibrary || bLibrary)
                                    {
                                        If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblCharacters(sKey).LastUpdated Then GoTo NextChar
                                        'If ShouldWeLoadLibraryItem(sKey) Then a.htblCharacters.Remove(sKey)
                                        switch (ShouldWeLoadLibraryItem(sKey))
                                        {
                                            case LoadItemEum.Yes:
                                                {
                                                a.htblCharacters.Remove(sKey);
                                            case LoadItemEum.No:
                                                {
                                                GoTo NextChar;
                                            case LoadItemEum.Both:
                                                {
                                                ' Keep key, but still add this new one
                                        }
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblCharacters.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextChar;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextChar;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            chr.Key = sKey;
                            if (bLibrary)
                            {
                                chr.IsLibrary = true;
                                chr.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then chr.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                            'chr.Name = .Item("Name").InnerText
                            If ! .Item("Name") == null Then chr.ProperName = .Item("Name").InnerText
                            If ! .Item("Article") == null Then chr.Article = .Item("Article").InnerText
                            If ! .Item("Prefix") == null Then chr.Prefix = .Item("Prefix").InnerText
                            If .Item("Perspective") != null Then chr.Perspective = (.Item("Perspective")([Enum].Parse(GetType(PerspectiveEnum)).InnerText), PerspectiveEnum) Else chr.Perspective = PerspectiveEnum.ThirdPerson
                            If dFileVersion < 5.00002 Then chr.Perspective = PerspectiveEnum.SecondPerson
                            For Each nodName As XmlElement In .GetElementsByTagName("Descriptor")
                                If nodName.InnerText <> "" Then chr.arlDescriptors.Add(nodName.InnerText)
                            Next;
                            'chr.Location = .Item("Location").InnerText
                            chr.Description = LoadDescription(nodChar, "Description");
                            For Each nodProp As XmlElement In .SelectNodes("Property") ' .GetElementsByTagName("Property")
                                private New clsProperty prop;
                                private string sPropKey = nodProp.Item("Key").InnerText;
                                if (Adventure.htblAllProperties.ContainsKey(sPropKey))
                                {
                                    prop = Adventure.htblAllProperties(sPropKey).Copy
                                    if (prop.Type = clsProperty.PropertyTypeEnum.Text)
                                    {
                                        prop.StringData = LoadDescription(nodProp, "Value");
                                    ElseIf prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                                        prop.Value = nodProp.Item("Value").InnerText;
                                    }
                                    prop.Selected = true;
                                    'chr.htblActualProperties.Add(prop)
                                    'chr.bCalculatedGroups = False
                                    chr.AddProperty(prop);
                                Else
                                    ErrMsg("Error loading character " + chr.Name + ": Property " + sPropKey + " not found.");
                                }
                            Next;
                            'chr.Location = chr.Location

                            For Each nodWalk As XmlElement In .GetElementsByTagName("Walk")
                                private New clsWalk walk;
                                walk.sKey = sKey;
                                walk.Description = nodWalk.Item("Description").InnerText;
                                'If nodWalk.Item("FromDesc") IsNot Nothing Then walk.FromDesc = nodWalk.Item("FromDesc").InnerText
                                walk.Loops = GetBool(nodWalk.Item("Loops").InnerText);
                                'walk.ShowMove = GetBool(nodWalk.Item("ShowMove").InnerText)
                                walk.StartActive = GetBool(nodWalk.Item("StartActive").InnerText);
                                'If nodWalk.Item("ToDesc") IsNot Nothing Then walk.ToDesc = nodWalk.Item("ToDesc").InnerText
                                For Each nodStep As XmlElement In nodWalk.GetElementsByTagName("Step")
                                    Dim [step] As New clsWalk.clsStep
                                    Dim sData() As String = nodStep.InnerText.Split(" "c)
                                    [step].sLocation = sData(0);
                                    [step].ftTurns.iFrom = CInt(sData(1));
                                    if (sData.Length = 2)
                                    {
                                        [step].ftTurns.iTo = CInt(sData(1));
                                    Else
                                        [step].ftTurns.iTo = CInt(sData(3));
                                    }
                                    if (dFileVersion < 5.000029)
                                    {
                                        if ([step].sLocation = "%Player%")
                                        {
                                            If [step].ftTurns.iFrom < 1 Then [step].ftTurns.iFrom = 1
                                            If [step].ftTurns.iTo < 1 Then [step].ftTurns.iTo = 1
                                        }
                                    }
                                    walk.arlSteps.Add([step]);
                                Next;
                                For Each nodCtrl As XmlElement In nodWalk.GetElementsByTagName("Control")
                                    private New EventOrWalkControl ctrl;
                                    Dim sData() As String = nodCtrl.InnerText.Split(" "c)
                                    ctrl.eControl = (sData(0)([Enum].Parse(GetType(EventOrWalkControl.ControlEnum))), EventOrWalkControl.ControlEnum);
                                    ctrl.eCompleteOrNot = (sData(1)([Enum].Parse(GetType(EventOrWalkControl.CompleteOrNotEnum))), EventOrWalkControl.CompleteOrNotEnum);
                                    ctrl.sTaskKey = sData(2);
                                    ReDim Preserve walk.WalkControls(walk.WalkControls.Length);
                                    walk.WalkControls(walk.WalkControls.Length - 1) = ctrl;
                                Next;
                                For Each nodSubWalk As XmlElement In nodWalk.GetElementsByTagName("Activity")
                                    private New clsWalk.SubWalk sw;
                                    Dim sData() As String = nodSubWalk.Item("When").InnerText.Split(" "c)
                                    if (sData(0) = clsWalk.SubWalk.WhenEnum.ComesAcross.ToString)
                                    {
                                        sw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                                        sw.sKey = sData(1);
                                    Else
                                        sw.ftTurns.iFrom = CInt(sData(0));
                                        if (sData.Length = 4)
                                        {
                                            sw.ftTurns.iTo = CInt(sData(2));
                                            sw.eWhen = (sData(3)([Enum].Parse(GetType(clsWalk.SubWalk.WhenEnum)).ToString), clsWalk.SubWalk.WhenEnum);
                                        Else
                                            sw.ftTurns.iTo = CInt(sData(0));
                                            sw.eWhen = (sData(1)([Enum].Parse(GetType(clsWalk.SubWalk.WhenEnum)).ToString), clsWalk.SubWalk.WhenEnum);
                                        }
                                    }

                                    if (nodSubWalk.Item("Action") IsNot null)
                                    {
                                        if (nodSubWalk.Item("Action").InnerXml.Contains("<Description>"))
                                        {
                                            sw.oDescription = LoadDescription(nodSubWalk, "Action");
                                        Else
                                            sData = nodSubWalk.Item("Action").InnerText.Split(" "c)
                                            sw.eWhat = (sData(0)([Enum].Parse(GetType(clsWalk.SubWalk.WhatEnum)).ToString), clsWalk.SubWalk.WhatEnum);
                                            sw.sKey2 = sData(1);
                                        }
                                    }

                                    If nodSubWalk.Item("OnlyApplyAt") != null Then sw.sKey3 = nodSubWalk.Item("OnlyApplyAt").InnerText

                                    ReDim Preserve walk.SubWalks(walk.SubWalks.Length);
                                    walk.SubWalks(walk.SubWalks.Length - 1) = sw;
                                Next;

                                chr.arlWalks.Add(walk);
                            Next;

                            For Each nodTopic As XmlElement In .GetElementsByTagName("Topic")
                                private New clsTopic topic;
                                topic.Key = nodTopic.Item("Key").InnerText;
                                If nodTopic.Item("ParentKey") != null Then topic.ParentKey = nodTopic.Item("ParentKey").InnerText
                                topic.Summary = nodTopic.Item("Summary").InnerText;
#if Generator
                                topic.Keywords = nodTopic.Item("Keywords").InnerText;
#else
                                ' Simplify Runner so it only has to deal with multiple, or specific refs
                                topic.Keywords = FixInitialRefs(nodTopic.Item("Keywords").InnerText);
#endif

                                topic.oConversation = LoadDescription(nodTopic, "Description");
                                If nodTopic.Item("IsAsk") != null Then topic.bAsk = GetBool(nodTopic.Item("IsAsk").InnerText)
                                If nodTopic.Item("IsCommand") != null Then topic.bCommand = GetBool(nodTopic.Item("IsCommand").InnerText)
                                If nodTopic.Item("IsFarewell") != null Then topic.bFarewell = GetBool(nodTopic.Item("IsFarewell").InnerText)
                                If nodTopic.Item("IsIntro") != null Then topic.bIntroduction = GetBool(nodTopic.Item("IsIntro").InnerText)
                                If nodTopic.Item("IsTell") != null Then topic.bTell = GetBool(nodTopic.Item("IsTell").InnerText)
                                If nodTopic.Item("StayInNode") != null Then topic.bStayInNode = GetBool(nodTopic.Item("StayInNode").InnerText)
                                topic.arlRestrictions = LoadRestrictions(nodTopic);
                                topic.arlActions = LoadActions(nodTopic);
                                chr.htblTopics.Add(topic);
                            Next;

                            chr.Location = chr.Location ' Assigns the location object from the character properties;
                            chr.CharacterType = EnumParseCharacterType(.Item("Type").InnerText);
                        }
                        if (eLoadWhat = LoadWhatEnum.All)
                        {
                            a.htblCharacters.Add(chr, chr.Key);
                        Else
                            ' Only add the Player character if we don't already have one
                            If chr.CharacterType = clsCharacter.CharacterTypeEnum.NonPlayer || a.Player == null Then a.htblCharacters.Add(chr, chr.Key)
                        }
                        '#If Generator Then
                        '                        If bAppend AndAlso a.htblCharacters.Contains(chr.Key) Then AddToDefaultList(chr.Key)
                        '#Else
#if Runner
                        For Each sCharFunction As String In New String() {"CharacterName", "DisplayCharacter", "ListHeld", "ListExits", "ListWorn", "LocationOf", "ParentOf", "ProperName"}
                            chr.SearchAndReplace("%" + sCharFunction + "%", "%" + sCharFunction + "[" + chr.Key + "]%");
                        Next;
                        'chr.bCalculatedGroups = False
#endif
NextChar:;
                    Next;
                    Debug.WriteLine("End Chars: " + Now);

                }

                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.Properies)
                {
                    For Each nodVar As XmlElement In .SelectNodes("/Adventure/Variable")
                        private New clsVariable var;
                        With nodVar;
                            private string sKey = .Item("Key").InnerText;
                            If ! .Item("Library") == null Then var.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblVariables.ContainsKey(sKey))
                            {
                                if (var.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblVariables(sKey).LastUpdated Then GoTo NextVar
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblVariables.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblVariables.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextVar;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblVariables.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextVar;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextVar;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            var.Key = sKey;
                            if (bLibrary)
                            {
                                var.IsLibrary = true;
                                var.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then var.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                            var.Name = .Item("Name").InnerText;
                            var.Type = EnumParseVariableType(.Item("Type").InnerText);
                            private string sValue = .Item("InitialValue").InnerText;
                            If ! .Item("ArrayLength") == null Then var.Length = CInt(Val(.Item("ArrayLength").InnerText))
                            if (var.Type = clsVariable.VariableTypeEnum.Text || (var.Length > 1 && sValue.Contains(",")))
                            {
                                if (var.Type = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    Dim iValues() As String = Split(sValue, ",")
                                    for (int iIndex = 1; iIndex <= iValues.Length; iIndex++)
                                    {
                                        var.IntValue(iIndex) = SafeInt(iValues(iIndex - 1));
                                    Next;
                                    var.StringValue = sValue;
                                Else
                                    for (int iIndex = 1; iIndex <= var.Length; iIndex++)
                                    {
                                        var.StringValue(iIndex) = sValue;
                                    Next;
                                }

                                'var.IntValue = -1
                            Else
                                for (int i = 1; i <= var.Length; i++)
                                {
                                    var.IntValue(i) = CInt(Val(sValue));
                                Next i;
                                ''var.StringValue = ""
                            }
                        }
                        a.htblVariables.Add(var, var.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(var.Key)
                        '#End If
NextVar:;
                    Next;
                }

                if (eLoadWhat = LoadWhatEnum.All || eLoadWhat = LoadWhatEnum.AllExceptProperties)
                {
                    For Each nodGroup As XmlElement In .SelectNodes("/Adventure/Group")
                        private New clsGroup grp;
                        With nodGroup;
                            private string sKey = .Item("Key").InnerText;
                            If ! .Item("Library") == null Then grp.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblGroups.ContainsKey(sKey))
                            {
                                if (grp.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblGroups(sKey).LastUpdated Then GoTo NextGroup
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblGroups.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblGroups.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextGroup;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblGroups.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextGroup;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextGroup;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            grp.Key = sKey;
                            if (bLibrary)
                            {
                                grp.IsLibrary = true;
                                grp.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then grp.LastUpdated = GetDate(.Item("LastUpdated").InnerText)
                            grp.Name = .Item("Name").InnerText;
                            grp.GroupType = EnumParseGroupType(.Item("Type").InnerText);
                            For Each nodMember As XmlElement In .GetElementsByTagName("Member")
                                grp.arlMembers.Add(nodMember.InnerText);
                            Next;
                            For Each nodProp As XmlElement In .GetElementsByTagName("Property")
                                private New clsProperty prop;
                                private string sPropKey = nodProp.Item("Key").InnerText;
                                if (Adventure.htblAllProperties.ContainsKey(sPropKey))
                                {
                                    prop = Adventure.htblAllProperties(sPropKey).Copy
                                    if (nodProp.Item("Value") IsNot null)
                                    {
                                        if (prop.Type = clsProperty.PropertyTypeEnum.Text)
                                        {
                                            prop.StringData = LoadDescription(nodProp, "Value");
                                        ElseIf prop.Type <> clsProperty.PropertyTypeEnum.SelectionOnly Then
                                            prop.Value = nodProp.Item("Value").InnerText;
                                        }
                                    }
                                    prop.Selected = true;
                                    if (prop.PropertyOf = grp.GroupType)
                                    {
                                        grp.htblProperties.Add(prop);
                                    Else
                                        'ErrMsg("Trying to add incorrect property to group!")
                                    }
                                Else
                                    ErrMsg("Error loading group " + grp.Name + ": Property " + sPropKey + " not found.");
                                }
                            Next;
                        }
                        a.htblGroups.Add(grp, grp.Key);

                        For Each sMember As String In grp.arlMembers
                            private clsItemWithProperties itm = CType(Adventure.GetItemFromKey(sMember), clsItemWithProperties);
                            If itm != null Then itm.ResetInherited() ' In case we've accessed properties, and built inherited before the group existed
                        Next;

                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(grp.Key)
                        '#End If
NextGroup:;
                    Next;


                    For Each nodALR As XmlElement In .SelectNodes("/Adventure/TextOverride")
                        private New clsALR alr;
                        With nodALR;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then alr.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblALRs.ContainsKey(sKey))
                            {
                                if (alr.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblALRs(sKey).LastUpdated Then GoTo NextALR
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblALRs.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblALRs.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextALR;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblALRs.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextALR;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextALR;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            alr.Key = sKey;
                            if (bLibrary)
                            {
                                alr.IsLibrary = true;
                                alr.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then alr.LastUpdated = CDate(.Item("LastUpdated").InnerText)
                            alr.OldText = CStr(.Item("OldText").InnerText);
                            alr.NewText = LoadDescription(nodALR, "NewText");
                        }
                        a.htblALRs.Add(alr, alr.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(alr.Key)
                        '#End If
NextALR:;
                    Next;

                    For Each nodHint As XmlElement In .SelectNodes("/Adventure/Hint")
                        private New clsHint hint;
                        With nodHint;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then hint.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblHints.ContainsKey(sKey))
                            {
                                if (hint.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblHints(sKey).LastUpdated Then GoTo NextHint
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblHints.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblHints.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextHint;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblHints.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextHint;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextHint;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            hint.Key = sKey;
                            if (bLibrary)
                            {
                                hint.IsLibrary = true;
                                hint.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then hint.LastUpdated = CDate(.Item("LastUpdated").InnerText)
                            hint.Question = CStr(.Item("Question").InnerText);
                            hint.SubtleHint = LoadDescription(nodHint, "Subtle");
                            hint.SledgeHammerHint = LoadDescription(nodHint, "Sledgehammer");
                            hint.arlRestrictions = LoadRestrictions(nodHint);
                        }
                        a.htblHints.Add(hint, hint.Key);
                        '#If Not Runner Then
                        '                        If bAppend Then AddToDefaultList(hint.Key)
                        '#End If
NextHint:;
                    Next;

                    For Each nodSynonym As XmlElement In .SelectNodes("/Adventure/Synonym")
                        private New clsSynonym synonym;
                        With nodSynonym;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then synonym.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblSynonyms.ContainsKey(sKey))
                            {
                                if (synonym.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblSynonyms(sKey).LastUpdated Then GoTo NextSynonym
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblSynonyms.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblSynonyms.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextSynonym;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }

                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblSynonyms.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo NextSynonym;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextSynonym;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            synonym.Key = sKey;
                            if (bLibrary)
                            {
                                synonym.IsLibrary = true;
                                synonym.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then synonym.LastUpdated = CDate(.Item("LastUpdated").InnerText)

                            For Each nodFrom As XmlElement In .GetElementsByTagName("From")
                                synonym.ChangeFrom.Add(nodFrom.InnerText);
                            Next;
                            synonym.ChangeTo = .Item("To").InnerText;
                        }
                        a.htblSynonyms.Add(synonym);
NextSynonym:;
                    Next;


                    For Each nodUDF As XmlElement In .SelectNodes("/Adventure/Function")
                        private New clsUserFunction udf;
                        With nodUDF;
                            private string sKey = .Item("Key").InnerText;
                            If .Item("Library") != null Then udf.IsLibrary = GetBool(.Item("Library").InnerText)
                            if (a.htblUDFs.ContainsKey(sKey))
                            {
                                if (udf.IsLibrary || bLibrary)
                                {
                                    If .Item("LastUpdated") == null || CDate(.Item("LastUpdated").InnerText) <= a.htblUDFs(sKey).LastUpdated Then GoTo NextUDF
                                    'If ShouldWeLoadLibraryItem(sKey) Then a.htblUDFs.Remove(sKey)
                                    switch (ShouldWeLoadLibraryItem(sKey))
                                    {
                                        case LoadItemEum.Yes:
                                            {
                                            a.htblUDFs.Remove(sKey);
                                        case LoadItemEum.No:
                                            {
                                            GoTo NextUDF;
                                        case LoadItemEum.Both:
                                            {
                                            ' Keep key, but still add this new one
                                    }
                                }
                                if (bAddDuplicateKeys)
                                {
                                    while (a.htblUDFs.ContainsKey(sKey))
                                    {
                                        sKey = IncrementKey(sKey)
                                    }
                                Else
                                    GoTo Nextudf;
                                }
                            ElseIf bLibrary && ShouldWeLoadLibraryItem(sKey) = LoadItemEum.No Then
                                GoTo NextUDF;
                            }
                            If a.listExcludedItems.Contains(sKey) Then a.listExcludedItems.Remove(sKey)
                            udf.Key = sKey;
                            if (bLibrary)
                            {
                                udf.IsLibrary = true;
                                udf.LoadedFromLibrary = true;
                            }
                            If ! .Item("LastUpdated") == null Then udf.LastUpdated = CDate(.Item("LastUpdated").InnerText)

                            udf.Name = CStr(.Item("Name").InnerText);
                            udf.Output = LoadDescription(nodUDF, "Output");
                            For Each nodArgument As XmlElement In .GetElementsByTagName("Argument")
                                With nodArgument;
                                    private New clsUserFunction.Argument arg;
                                    arg.Name = .Item("Name").InnerText;
                                    arg.Type = (.Item("Type")([Enum].Parse(GetType(clsUserFunction.ArgumentType)).InnerText), clsUserFunction.ArgumentType);
                                    udf.Arguments.Add(arg);
                                }
                            Next;

                        }
                        a.htblUDFs.Add(udf, udf.Key);
NextUDF:;
                    Next;

                    if (Not bLibrary)
                    {
                        For Each nodExclude As XmlElement In .SelectNodes("/Adventure/Exclude")
                            a.listExcludedItems.Add(nodExclude.InnerText);
                        Next;
                    }

#if Not www
                    if (Not bLibrary)
                    {
                        if (.Item("Map") IsNot null)
                        {
                            Adventure.Map.Pages.Clear();
                            For Each nodPage As XmlElement In .SelectNodes("/Adventure/Map/Page")
                                With nodPage;
                                    private string sPageKey = .Item("Key").InnerText;
                                    if (IsNumeric(sPageKey))
                                    {
                                        private New MapPage(SafeInt(sPageKey)) page;
                                        If .Item("Label") != null Then page.Label = .Item("Label").InnerText
                                        If .Item("Selected") != null && GetBool(.Item("Selected").InnerText) Then Adventure.Map.SelectedPage = sPageKey
                                        For Each nodNode As XmlElement In .GetElementsByTagName("Node")
                                            With nodNode;
                                                private New MapNode node;
                                                If .Item("Key") != null Then node.Key = .Item("Key").InnerText
                                                private clsLocation loc = Adventure.htblLocations(node.Key);
                                                if (loc IsNot null)
                                                {
                                                    node.Text = loc.ShortDescriptionSafe ' StripCarats(ReplaceALRs(loc.ShortDescription.ToString));
                                                    If .Item("X") != null Then node.Location.X = SafeInt(.Item("X").InnerText)
                                                    If .Item("Y") != null Then node.Location.Y = SafeInt(.Item("Y").InnerText)
                                                    If .Item("Z") != null Then node.Location.Z = SafeInt(.Item("Z").InnerText)
                                                    If .Item("Height") != null Then node.Height = SafeInt(.Item("Height").InnerText) Else node.Height = 4
                                                    If .Item("Width") != null Then node.Width = SafeInt(.Item("Width").InnerText) Else node.Width = 6

                                                    If loc.arlDirections(DirectionsEnum.Up).LocationKey != null Then node.bHasUp = true
                                                    If loc.arlDirections(DirectionsEnum.Down).LocationKey != null Then node.bHasDown = true
                                                    If loc.arlDirections(DirectionsEnum.In).LocationKey != null Then node.bHasIn = true
                                                    If loc.arlDirections(DirectionsEnum.Out).LocationKey != null Then node.bHasOut = true

                                                    For Each nodLink As XmlElement In .GetElementsByTagName("Link")
                                                        With nodLink;
                                                            private New MapLink link;
                                                            link.sSource = node.Key;
                                                            If .Item("SourceAnchor") != null Then link.eSourceLinkPoint = (.Item("SourceAnchor")([Enum].Parse(GetType(DirectionsEnum)).InnerText), DirectionsEnum)
                                                            link.sDestination = loc.arlDirections(link.eSourceLinkPoint).LocationKey;
                                                            if (Adventure.Map.DottedLink(loc.arlDirections(link.eSourceLinkPoint)))
                                                            {
                                                                link.Style = Drawing2D.DashStyle.Dot;
                                                            Else
                                                                link.Style = Drawing2D.DashStyle.Solid;
                                                            }
                                                            If .Item("DestinationAnchor") != null Then link.eDestinationLinkPoint = (.Item("DestinationAnchor")([Enum].Parse(GetType(DirectionsEnum)).InnerText), DirectionsEnum)
                                                            private string sDest = loc.arlDirections(link.eSourceLinkPoint).LocationKey;
                                                            if (sDest IsNot null && Adventure.htblLocations.ContainsKey(sDest))
                                                            {
                                                                private clsLocation locDest = Adventure.htblLocations(sDest);
                                                                if (locDest IsNot null)
                                                                {
                                                                    if (locDest.arlDirections(link.eDestinationLinkPoint).LocationKey = loc.Key)
                                                                    {
                                                                        link.Duplex = true;
                                                                        If Adventure.Map.DottedLink(locDest.arlDirections(link.eDestinationLinkPoint)) Then link.Style = Drawing2D.DashStyle.Dot
                                                                    }
                                                                }
                                                            }
                                                            For Each nodAnchor As XmlElement In .GetElementsByTagName("Anchor")
                                                                private New Point3D p;
                                                                With nodAnchor;
                                                                    If .Item("X") != null Then p.X = SafeInt(.Item("X").InnerText)
                                                                    If .Item("Y") != null Then p.Y = SafeInt(.Item("Y").InnerText)
                                                                    If .Item("Z") != null Then p.Z = SafeInt(.Item("Z").InnerText)
                                                                }
                                                                ReDim Preserve link.OrigMidPoints(link.OrigMidPoints.Length);
                                                                link.OrigMidPoints(link.OrigMidPoints.Length - 1) = p;
                                                                private New Anchor ar;
                                                                ar.Visible = true;
                                                                ar.Parent = link;
                                                                link.Anchors.Add(ar);
                                                            Next;
                                                            node.Links.Add(link.eSourceLinkPoint, link);
                                                        }
                                                    Next;

                                                    node.Page = page.iKey;
                                                    page.AddNode(node);
                                                }
                                            }
                                        Next;
                                        Adventure.Map.Pages.Add(page.iKey, page);
                                        Adventure.Map.Pages(page.iKey).SortNodes();
                                    }
                                }
                            Next;
                        }
                    }
#endif

                    ' Now fix any remapped keys
                    ' This must only remap our newly imported tasks, not all the original ones!
                    '
                    For Each sOldKey As String In htblDuplicateKeyMapping.Keys
                        'For Each tas As clsTask In a.htblTasks.Values
                        For Each sTask As String In arlNewTasks
                            private clsTask tas = a.htblTasks(sTask);
                            If tas.GeneralKey = sOldKey Then tas.GeneralKey = htblDuplicateKeyMapping(sOldKey)
                            For Each act As clsAction In tas.arlActions
                                If act.sKey1 = sOldKey Then act.sKey1 = htblDuplicateKeyMapping(sOldKey)
                                If act.sKey2 = sOldKey Then act.sKey2 = htblDuplicateKeyMapping(sOldKey)
                            Next;
                        Next;
                    Next;

                    Adventure.BlorbMappings.Clear();
                    if (.Item("FileMappings") IsNot null)
                    {
                        For Each nodMapping As XmlElement In .SelectNodes("/Adventure/FileMappings/Mapping")
                            With nodMapping;
                                private int iResource = SafeInt(.Item("Resource").InnerText);
                                private string sFile = .Item("File").InnerText;
                                Adventure.BlorbMappings.Add(sFile, iResource);
                            }
                        Next;
                    }

                }

            }

            ' Correct any old style functions
            ' Player.Held.Weight > Player.Held.Weight.Sum
            if (Not bLibrary && dFileVersion < 5.0000311)
            {
                For Each sd As SingleDescription In Adventure.AllDescriptions
                    For Each p As clsProperty In Adventure.htblAllProperties.Values
                        if (p.Type = clsProperty.PropertyTypeEnum.Integer || p.Type = clsProperty.PropertyTypeEnum.ValueList)
                        {
                            if (sd.Description.Contains("." + p.Key))
                            {
                                sd.Description = sd.Description.Replace("." + p.Key, "." + p.Key + ".Sum");
                            }
                        }
                    Next;
                Next;
            }


            if (eLoadWhat = LoadWhatEnum.All && iCorrectedTasks > 0)
            {
                MessageBox.Show(iCorrectedTasks + " tasks have been updated.", "Adventure Upgrade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            '#If Not www Then
            With Adventure;
                if (.Map.Pages.Count = 1 && .Map.Pages.ContainsKey(0) && .Map.Pages(0).Nodes.Count = 0)
                {
                    .Map = New clsMap;
                    .Map.RecalculateLayout();
                }
#if Generator
                fGenerator.Map1.Map = .Map;
                '#ElseIf www Then
                '                If UserSession.Map Is Nothing Then UserSession.Map = New ADRIFT.Map() ' For www
#else
                UserSession.Map.Map = .Map;
#endif
            }
            '#End If
            dFileVersion = dVersion ' Set back to current version so copy/paste etc has correct versions

            return true;

        }
        catch (LoadAbortException exLAE)
        {
            ' Ignore
            return false;
        }
        catch (XmlException exXML)
        {
            if (bLibrary)
            {
                ErrMsg("The file '" + sFilename + "' you are trying to load is not a valid ADRIFT Module.", exXML);
            Else
                ErrMsg("Error loading Adventure", exXML);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Root element is missing"))
            {
                ErrMsg("The file you are trying to load is not a valid ADRIFT v5.0 file.");
            Else
                ErrMsg("Error loading Adventure", ex);
            }
            return false;
        }

    }



    'Private Sub Load500(ByVal iLength As Integer)

    '    Try
    '        Dim bAdvZLib() As Byte = br.ReadBytes(iLength)
    '        'Dim sPassword As String = Dencode(System.Text.Encoding.Default.GetString(br.ReadBytes(12)), lSize + 1)

    '        ReDim bAdventure(-1)

    '        zi = New DotZLib.Inflater
    '        zi.Add(bAdvZLib)
    '        zi.Finish()
    '        zi.Dispose()

    '        With Adventure
    '            Dim sBuffer As String
    '            Dim iPos As Integer = 0

    '            .Title = gl(bAdventure, iPos)
    '            .Author = gl(bAdventure, iPos)
    '            .Introduction = gl(bAdventure, iPos)

    '            Dim iNumLocations As Integer = CInt(gl(bAdventure, iPos))
    '            For iLoc As Integer = 1 To iNumLocations
    '                Dim location As New clsLocation
    '                With location
    '                    .Key = gl(bAdventure, iPos)
    '                    .ShortDescription = gl(bAdventure, iPos)
    '                    .LongDescription = gl(bAdventure, iPos)
    '                    For di As DirectionsEnum = DirectionsEnum.North To DirectionsEnum.Out_
    '                        Dim dtn As clsDirection = location.arlDirections(di)
    '                        With dtn
    '                            .LocationKey = gl(bAdventure, iPos)
    '                            If .LocationKey <> "" Then
    '                                LoadRestrictions(.Restrictions, iPos)
    '                            Else
    '                                .LocationKey = Nothing
    '                            End If
    '                        End With
    '                    Next
    '                End With
    '                .htblLocations.Add(location, location.Key)
    '            Next

    '            Dim iNumTasks As Integer = CInt(gl(bAdventure, iPos))
    '            For iTas As Integer = 1 To iNumTasks
    '                Dim task As New clsTask
    '                With task
    '                    .Key = gl(bAdventure, iPos)
    '                    .Priority = CInt(gl(bAdventure, iPos))
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "General"
    '                            .TaskType = clsTask.TaskTypeEnum.General
    '                        Case "Specific"
    '                            .TaskType = clsTask.TaskTypeEnum.Specific
    '                            Dim iNumSpecifics As Integer = CInt(gl(bAdventure, iPos))
    '                            ReDim .Specifics(iNumSpecifics)
    '                            For iSpec As Integer = 1 To iNumSpecifics
    '                                With .Specifics(iSpec)
    '                                    sBuffer = gl(bAdventure, iPos)
    '                                    Select Case sBuffer
    '                                        Case "Character"
    '                                            .Type = ReferencesType.Character
    '                                        Case "Number"
    '                                            .Type = ReferencesType.Number
    '                                        Case "Object_"
    '                                            .Type = ReferencesType.Object_
    '                                        Case "Text"
    '                                            .Type = ReferencesType.Text
    '                                    End Select
    '                                    .Multiple = CBool(gl(bAdventure, iPos))
    '                                    Dim iNumKeys As Integer = CInt(gl(bAdventure, iPos))
    '                                    For iKey As Integer = 1 To iNumKeys
    '                                        .Keys.Add(gl(bAdventure, iPos))
    '                                    Next
    '                                End With
    '                            Next
    '                        Case "System"
    '                            .TaskType = clsTask.TaskTypeEnum.System
    '                    End Select
    '                    .Description = gl(bAdventure, iPos)
    '                    .CompletionMessage = gl(bAdventure, iPos)
    '                    LoadRestrictions(.arlRestrictions, iPos)
    '                End With
    '                .htblTasks.Add(task, task.Key)
    '            Next

    '        End With

    '    Catch ex As Exception
    '        ErrMsg("Error loading Adventure", ex)
    '    Finally
    '        bAdventure = Nothing
    '    End Try

    'End Sub


    'Private Sub LoadRestrictions(ByVal arlRestrictions As RestrictionArrayList, ByRef iPos As Integer)

    '    Dim sBuffer As String
    '    Dim NumRestrictions As Integer = CInt(gl(bAdventure, iPos))

    '    For iRest As Integer = 1 To NumRestrictions
    '        Dim rest As New clsRestriction
    '        With rest
    '            sBuffer = gl(bAdventure, iPos)
    '            Select Case sBuffer
    '                Case "Location"
    '                    .eItem = clsRestriction.ItemEnum.Location
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "InGroup"
    '                            .eLocation = clsRestriction.LocationEnum.InGroup
    '                        Case "SeenByCharacter"
    '                            .eLocation = clsRestriction.LocationEnum.SeenByCharacter
    '                    End Select
    '                    ' TODO
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "Must"
    '                            .eMust = clsRestriction.MustEnum.Must
    '                        Case "MustNot"
    '                            .eMust = clsRestriction.MustEnum.MustNot
    '                    End Select
    '                    .sKey1 = gl(bAdventure, iPos)
    '                    .sKey2 = gl(bAdventure, iPos)

    '                Case "Object_"
    '                    .eItem = clsRestriction.ItemEnum.Object_
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "AtLocation"
    '                            .eObject = clsRestriction.ObjectEnum.BeAtLocation
    '                        Case "HeldByCharacter"
    '                            .eObject = clsRestriction.ObjectEnum.HeldByCharacter
    '                        Case "InGroup"
    '                            .eObject = clsRestriction.ObjectEnum.InGroup
    '                        Case "InsideObject"
    '                            .eObject = clsRestriction.ObjectEnum.InsideObject
    '                        Case "InState"
    '                            .eObject = clsRestriction.ObjectEnum.InState
    '                        Case "OnObject"
    '                            .eObject = clsRestriction.ObjectEnum.OnObject
    '                        Case "PartOfCharacter"
    '                            .eObject = clsRestriction.ObjectEnum.PartOfCharacter
    '                        Case "PartOfObject"
    '                            .eObject = clsRestriction.ObjectEnum.PartOfObject
    '                        Case "SeenByCharacter"
    '                            .eObject = clsRestriction.ObjectEnum.SeenByCharacter
    '                    End Select
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "Must"
    '                            .eMust = clsRestriction.MustEnum.Must
    '                        Case "MustNot"
    '                            .eMust = clsRestriction.MustEnum.MustNot
    '                    End Select
    '                    .sKey1 = gl(bAdventure, iPos)
    '                    .sKey2 = gl(bAdventure, iPos)

    '                Case "Task"
    '                    .eItem = clsRestriction.ItemEnum.Task
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "Complete"
    '                            .eTask = clsRestriction.TaskEnum.Complete
    '                    End Select
    '                    sBuffer = gl(bAdventure, iPos)
    '                    Select Case sBuffer
    '                        Case "Must"
    '                            .eMust = clsRestriction.MustEnum.Must
    '                        Case "MustNot"
    '                            .eMust = clsRestriction.MustEnum.MustNot
    '                    End Select
    '                    .sKey1 = gl(bAdventure, iPos)
    '                    .sKey2 = gl(bAdventure, iPos)
    '            End Select
    '            .sMessage = gl(bAdventure, iPos)
    '        End With
    '        arlRestrictions.Add(rest)
    '    Next
    'End Sub


    private clsRestriction LocRestriction(string sLocKey, bool bMust)
    {

        private New clsRestriction r;
        r.eType = clsRestriction.RestrictionTypeEnum.Character;
        r.sKey1 = "%Player%";
        if (bMust)
        {
            r.eMust = clsRestriction.MustEnum.Must;
        Else
            r.eMust = clsRestriction.MustEnum.MustNot;
        }
        if (Adventure.htblLocations.ContainsKey(sLocKey))
        {
            r.eCharacter = clsRestriction.CharacterEnum.BeAtLocation;
        ElseIf Adventure.htblGroups.ContainsKey(sLocKey) Then
            r.eCharacter = clsRestriction.CharacterEnum.BeWithinLocationGroup;
        }
        r.sKey2 = sLocKey;
        return r;

    }


    ' Converts v4 functions to v5
    private string ConvText(string s400Text)
    {
        private string s500Text = s400Text.Replace("%theobject%", "%TheObject[%object%]%").Replace("%character%", "%CharacterName[%character%]%").Replace("%room%", "%LocationName[%LocationOf[Player]%]%").Replace("%obstatus%", "%LCase[%PropertyValue[%object%,OpenStatus]%]%");
        while (s500Text.Contains("%t_"))
        {
            private int iStart = s500Text.IndexOf("%t_");
            private int iEnd = s500Text.IndexOf("%", iStart + 1) + 1;
            s500Text = s500Text.Replace(s500Text.Substring(iStart, iEnd - iStart), "%NumberAsText[%" & s500Text.Substring(iStart + 3, iEnd - iStart - 3) & "]%")
        }
        while (s500Text.Contains("%in_"))
        {
            private int iStart = s500Text.IndexOf("%in_");
            private int iEnd = s500Text.IndexOf("%", iStart + 1) + 1;
            ' convert object name to key
            private string sKey = "";
            For Each ob As clsObject In Adventure.htblObjects.Values
                if (s500Text.Substring(iStart + 4, iEnd - iStart - 5) = ob.arlNames(0))
                {
                    sKey = ob.Key
                    Exit For;
                }
            Next;
            s500Text = s500Text.Replace(s500Text.Substring(iStart, iEnd - iStart), "%ListObjectsIn[" & sKey & "]%")
        }
        while (s500Text.Contains("%on_"))
        {
            private int iStart = s500Text.IndexOf("%on_");
            private int iEnd = s500Text.IndexOf("%", iStart + 1) + 1;
            ' convert object name to key
            private string sKey = "";
            For Each ob As clsObject In Adventure.htblObjects.Values
                if (s500Text.Substring(iStart + 4, iEnd - iStart - 5) = ob.arlNames(0))
                {
                    sKey = ob.Key
                    Exit For;
                }
            Next;
            s500Text = s500Text.Replace(s500Text.Substring(iStart, iEnd - iStart), "%ListObjectsOn[" & sKey & "]%")
        }
        while (s500Text.Contains("%onin_"))
        {
            private int iStart = s500Text.IndexOf("%onin_");
            private int iEnd = s500Text.IndexOf("%", iStart + 1) + 1;
            ' convert object name to key
            private string sKey = "";
            For Each ob As clsObject In Adventure.htblObjects.Values
                if (s500Text.Substring(iStart + 6, iEnd - iStart - 7) = ob.arlNames(0))
                {
                    sKey = ob.Key
                    Exit For;
                }
            Next;
            s500Text = s500Text.Replace(s500Text.Substring(iStart, iEnd - iStart), "%ListObjectsOnAndIn[" & sKey & "]%")
        }
        ' %status_
        ' %state_
        return s500Text;
    }



    internal bool CopyStream(ref input As System.IO.MemoryStream, ref output As ZOutputStream)
    {

        try
        {
            private int iBlock = 1024;
            private int iBytes;
            private byte[] buffer1 = new Byte(iBlock - 1) {};
            iBytes = input.Read(buffer1, 0, iBlock)
            Do While (iBytes > 0)
                output.Write(buffer1, 0, iBytes);
                iBytes = input.Read(buffer1, 0, iBlock)
            Loop;
            output.Flush();
            return true;
        }
        catch (ZStreamException ex)
        {
            ErrMsg("CopyStream error", ex);
            return false;
        }

    }


    internal void CreateMandatoryProperties()
    {

        For Each sKey As String In New String() {OBJECTARTICLE, OBJECTPREFIX, OBJECTNOUN}
            if (Not Adventure.htblObjectProperties.ContainsKey(sKey))
            {
                private New clsProperty prop;
                With prop;
                    .Key = sKey;
                    switch (.Key)
                    {
                        case OBJECTARTICLE:
                            {
                            .Description = "Object Article";
                        case OBJECTPREFIX:
                            {
                            .Description = "Object Prefix";
                        case OBJECTNOUN:
                            {
                            .Description = "Object Name";
                    }
                    .PropertyOf = clsProperty.PropertyOfEnum.Objects;
                    .Type = clsProperty.PropertyTypeEnum.Text;
                }
                Adventure.htblAllProperties.Add(prop);
            }
            Adventure.htblObjectProperties(sKey).GroupOnly = true;
        Next;

        if (Not Adventure.htblLocationProperties.ContainsKey(SHORTLOCATIONDESCRIPTION))
        {
            private New clsProperty prop;
            With prop;
                .Key = SHORTLOCATIONDESCRIPTION;
                .Description = "Short Location Description";
                .PropertyOf = clsProperty.PropertyOfEnum.Locations;
                .Type = clsProperty.PropertyTypeEnum.Text;
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblLocationProperties(SHORTLOCATIONDESCRIPTION).GroupOnly = true;

        if (Not Adventure.htblLocationProperties.ContainsKey(LONGLOCATIONDESCRIPTION))
        {
            private New clsProperty prop;
            With prop;
                .Key = LONGLOCATIONDESCRIPTION;
                .Description = "Long Location Description";
                .PropertyOf = clsProperty.PropertyOfEnum.Locations;
                .Type = clsProperty.PropertyTypeEnum.Text;
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblLocationProperties(LONGLOCATIONDESCRIPTION).GroupOnly = true;

        if (Not Adventure.htblCharacterProperties.ContainsKey(CHARACTERPROPERNAME))
        {
            private New clsProperty prop;
            With prop;
                .Key = CHARACTERPROPERNAME;
                .Description = "Character Proper Name";
                .PropertyOf = clsProperty.PropertyOfEnum.Characters;
                .Type = clsProperty.PropertyTypeEnum.Text;
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblCharacterProperties(CHARACTERPROPERNAME).GroupOnly = true;

        if (Not Adventure.htblObjectProperties.ContainsKey("StaticOrDynamic"))
        {
            private New clsProperty prop;
            With prop;
                .Key = "StaticOrDynamic";
                .Description = "Object type";
                .Mandatory = true;
                .PropertyOf = clsProperty.PropertyOfEnum.Objects;
                .Type = clsProperty.PropertyTypeEnum.StateList;
                .arlStates.Add("Static");
                .arlStates.Add("Dynamic");
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblObjectProperties("StaticOrDynamic").GroupOnly = true;

        if (Not Adventure.htblObjectProperties.ContainsKey("StaticLocation"))
        {
            private New clsProperty prop;
            With prop;
                .Key = "StaticLocation";
                .Description = "Location of the object";
                .Mandatory = true;
                .PropertyOf = clsProperty.PropertyOfEnum.Objects;
                .Type = clsProperty.PropertyTypeEnum.StateList;
                .arlStates.Add("Hidden");
                .arlStates.Add("Single Location");
                .arlStates.Add("Location Group");
                .arlStates.Add("Everywhere");
                .arlStates.Add("Part of Character");
                .arlStates.Add("Part of Object");
                .DependentKey = "StaticOrDynamic";
                .DependentValue = "Static";
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblObjectProperties("StaticLocation").GroupOnly = true;

        if (Not Adventure.htblObjectProperties.ContainsKey("DynamicLocation"))
        {
            private New clsProperty prop;
            With prop;
                .Key = "DynamicLocation";
                .Description = "Location of the object";
                .Mandatory = true;
                .PropertyOf = clsProperty.PropertyOfEnum.Objects;
                .Type = clsProperty.PropertyTypeEnum.StateList;
                .arlStates.Add("Hidden");
                .arlStates.Add("Held by Character");
                .arlStates.Add("Worn by Character");
                .arlStates.Add("In Location");
                .arlStates.Add("Inside Object");
                .arlStates.Add("On Object");
                .DependentKey = "StaticOrDynamic";
                .DependentValue = "Dynamic";
            }
            Adventure.htblAllProperties.Add(prop);
        }
        Adventure.htblObjectProperties("DynamicLocation").GroupOnly = true;

        For Each sProp As String In New String() {"AtLocation", "AtLocationGroup", "PartOfWhat", "PartOfWho", "HeldByWho", "WornByWho", "InLocation", "InsideWhat", "OnWhat"}
            If Adventure.htblObjectProperties.ContainsKey(sProp) Then Adventure.htblObjectProperties(sProp).GroupOnly = true
        Next;

    }

#if False
    private bool Load390()
    {

        try
        {

#if Mono
            ErrMsg("Unfortunately it is not possible to load ADRIFT 3.90 files directly, using Mono, as the Mono framework does not implement a .Net compatible Random function.  Sorry!");
            return false;
#endif
            Cursor.Current = Cursors.WaitCursor;

            if (Adventure.htblAllProperties.Count = 0)
            {
                ErrMsg("You must select at least one library within Generator > File > Settings > Libraries before loading ADRIFT v3.9 adventures.");
                return false;
            }

            ' Safety check...
            private string sPropCheck = "";
            For Each sProperty As String In New String() {"AtLocation", "AtLocationGroup", "CharacterAtLocation", "CharacterLocation", "CharEnters", "CharExits", "Container", "DynamicLocation", "HeldByWho", "InLocation", "InsideWhat", "Lockable", "LockKey", "LockStatus", "ListDescription", "OnWhat", "Openable", "OpenStatus", "PartOfWho", "Readable", "ReadText", "ShowEnterExit", "StaticLocation", "StaticOrDynamic", "Surface", "Wearable", "WornByWho"}
                if (Not Adventure.htblAllProperties.ContainsKey(sProperty))
                {
                    sPropCheck &= sProperty + vbCrLf;
                }
            Next;
            if (sPropCheck <> "")
            {
                ErrMsg("Library must contain the following properties before loading ADRIFT v3.9 files:" + vbCrLf + sPropCheck);
                return false;
            }

            br.BaseStream.Position = 0;
            Dim bRawData() As Byte = br.ReadBytes(CInt(br.BaseStream.Length))
            private string sRawData = System.Text.Encoding.Default.GetString(bRawData);
            bAdventure = Dencode(sRawData, 0)

            'br.ReadBytes(2) ' CrLf
            'Dim lSize As Long = CLng(System.Text.Encoding.Default.GetString(br.ReadBytes(8)))

            'Dim sPassword As String = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(br.ReadBytes(12)), lSize + 1))
            private clsAdventure a = Adventure;


            With Adventure;

                private int iStartMaxPriority = CurrentMaxPriority();
                private int iStartLocations = .htblLocations.Count;
                private int iStartObs = .htblObjects.Count;
                private int iStartTask = .htblTasks.Count;
                private int iStartChar = .htblCharacters.Count;
                private int iStartVariable = .htblVariables.Count;
                private bool bSound = false;
                private bool bGraphics = false;
                private string sFilename;
                private int iFilesize;
                .dictv4Media = New Dictionary(Of String, clsAdventure.v4Media);

                'Dim htblKeyMapping As New StringHashTable

                '.bV4Compatibility = True

                salWithStates.Clear();
                private int iPos = 0;
                private string sBuffer = null;
                ' Read the introduction

                private string sTerminator = "**";
                .dVersion = SafeDbl(GetLine(bAdventure, iPos).Substring(8));

                while (iPos < bAdventure.Length - 1 And sBuffer <> sTerminator)
                {
                    sBuffer = GetLine(bAdventure, iPos)
                    If sBuffer <> sTerminator Then .Introduction.Item(0).Description &= "<br>" + sBuffer
                }
                private int iStartLocation = CInt(GetLine(bAdventure, iPos));
                sBuffer = Nothing
                while (iPos < bAdventure.Length - 1 And sBuffer <> sTerminator)
                {
                    sBuffer = GetLine(bAdventure, iPos)
                    If sBuffer <> sTerminator Then .WinningText.Item(0).Description &= "<br>" + sBuffer
                }
                .Title = GetLine(bAdventure, iPos);
                .Author = GetLine(bAdventure, iPos);
                .NotUnderstood = GetLine(bAdventure, iPos);
                private int iPer = CInt(GetLine(bAdventure, iPos)) + 1 ' Perspective;
                .ShowExits = CBool(GetLine(bAdventure, iPos));
                GetLine(bAdventure, iPos) ' WaitNum;
                .ShowFirstRoom = CBool(GetLine(bAdventure, iPos)) ' ShowFirst;
                private bool bBattleSystem = CBool(GetLine(bAdventure, iPos));
                .MaxScore = CInt(GetLine(bAdventure, iPos));

                ' Delete the default char in new file
                .htblCharacters.Remove("Player");
                private New clsCharacter Player;
                With Player;
                    .Key = "Player";
                    .CharacterType = clsCharacter.CharacterTypeEnum.Player;
                    .Perspective = (PerspectiveEnum)(iPer);
                    .ProperName = GetLine(bAdventure, iPos);
                    If .ProperName = "" || .ProperName = "Anonymous" Then .ProperName = "Player"
                    .arlDescriptors.Add("myself");
                    .arlDescriptors.Add("me");
                    private bool bPromptName = CBool(GetLine(bAdventure, iPos))  ' PromptName;
                    .Description = New Description(ConvText(GetLine(bAdventure, iPos))) ' Description;
                    private int iTask = CInt(GetLine(bAdventure, iPos)) ' Task;
                    if (iTask > 0)
                    {
                        GetLine(bAdventure, iPos) ' Description;
                    }
                    .Location.Position = (iPos)(GetLine(bAdventure), clsCharacterLocation.PositionEnum);
                    private int iOnWhat = CInt(GetLine(bAdventure, iPos)) ' OnWhat;
                    private bool bPromptGender = false;
                    switch (SafeInt(GetLine(bAdventure, iPos)) ' Sex)
                    {
                        case 0 ' Male:
                            {
                            .Gender = clsCharacter.GenderEnum.Male;
                        case 1 ' Female:
                            {
                            .Gender = clsCharacter.GenderEnum.Female;
                        case 2 ' Prompt:
                            {
                            .Gender = clsCharacter.GenderEnum.Unknown;
                            bPromptGender = True
                    }

                    if (bPromptName || bPromptGender)
                    {
                        private New clsTask tasPrompt;
                        With tasPrompt;
                            .Key = "Task" + (Adventure.htblTasks.Count + 1);
                            .Description = "Generated task for Player prompts";
                            .TaskType = clsTask.TaskTypeEnum.System;
                            .RunImmediately = true;
                            .Priority = iStartMaxPriority + Adventure.htblTasks.Count + 1;
                            if (bPromptGender)
                            {
                                private New clsAction act;
                                act.eItem = clsAction.ItemEnum.SetProperties;
                                act.sKey1 = Player.Key;
                                act.sKey2 = "Gender";
                                act.StringValue = "%PopUpChoice[""Please select player gender"", ""Male"", ""Female""]%";
                                tasPrompt.arlActions.Add(act);
                            }
                            if (bPromptName)
                            {
                                private New clsAction act;
                                act.eItem = clsAction.ItemEnum.SetProperties;
                                act.sKey1 = Player.Key;
                                act.sKey2 = "CharacterProperName";
                                act.StringValue = "%PopUpInput[""Please enter your name"", ""Anonymous""]%";
                                tasPrompt.arlActions.Add(act);
                            }
                        }
                        Adventure.htblTasks.Add(tasPrompt, tasPrompt.Key);
                    }
                    .MaxSize = CInt(GetLine(bAdventure, iPos));
                    .MaxWeight = CInt(GetLine(bAdventure, iPos));
                    '.Location = "Location" & iStartLocation + 1

                    if (iOnWhat = 0)
                    {
                        .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                        .Location.Key = "Location" + iStartLocation + 1;
                    Else
                        .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject;
                        .Location.Key = iOnWhat.ToString ' Will adjust this below;
                    }


                    .Location = .Location;

                    if (bBattleSystem)
                    {
                        GetLine(bAdventure, iPos) ' Min Stamina;
                        'GetLine(bAdventure, iPos) ' Max Stamina
                        GetLine(bAdventure, iPos) ' Min Strength;
                        'GetLine(bAdventure, iPos) ' Max Strength
                        'GetLine(bAdventure, iPos) ' Min Accuracy
                        'GetLine(bAdventure, iPos) ' Max Accuracy
                        GetLine(bAdventure, iPos) ' Min Defence;
                        'GetLine(bAdventure, iPos) ' Max Defence
                        'GetLine(bAdventure, iPos) ' Min Agility
                        'GetLine(bAdventure, iPos) ' Max Agility
                        'GetLine(bAdventure, iPos) ' Recovery
                    }
                }
                .htblCharacters.Add(Player, Player.Key);

                'Adventure.iCompassPoints = CType(8 + 4 * CInt(GetLine(bAdventure, iPos)), DirectionsEnum)
                private int iCompassPoints = CType(8 + 4 * CInt(GetLine(bAdventure, iPos)), DirectionsEnum) ' CInt(GetLine(bAdventure, iPos));
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Map) = ! CBool(GetLine(bAdventure, iPos));
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                bSound = CBool(GetLine(bAdventure, iPos))
                bGraphics = CBool(GetLine(bAdventure, iPos))
                for (int i = 0; i <= 1; i++)
                {
                    if (bSound)
                    {
                        sFilename = GetLine(bAdventure, iPos) ' Filename
                        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                    }
                    if (bGraphics)
                    {
                        sFilename = GetLine(bAdventure, iPos) ' Filename
                        if (sFilename <> "")
                        {
                            If i = 0 Then Adventure.Introduction(0).Description = "<img src=""" + sFilename + """>" + Adventure.Introduction(0).Description
                            If i = 1 Then Adventure.WinningText(0).Description = "<img src=""" + sFilename + """>" + Adventure.WinningText(0).Description
                        }
                        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                    }
                Next;
                'GetLine(bAdventure, iPos) ' Enable Panel
                'GetLine(bAdventure, iPos) ' Panel Text
                GetLine(bAdventure, iPos) ' Size ratio;
                GetLine(bAdventure, iPos) ' Weight ratio;
                'Dim s1 As String = GetLine(bAdventure, iPos) ' ?


                '----------------------------------------------------------------------------------
                ' Locations
                '----------------------------------------------------------------------------------


                private int iNumLocations = CInt(GetLine(bAdventure, iPos));
                Dim iLocations(iNumLocations, 11, 2) As Integer ' Temp Store
                private int iLoc;
                private New Collection colNewLocs;
                For iLoc = 1 To iNumLocations
                    private New clsLocation Location;
                    With Location;
                        '.Key = "Location" & iLoc.ToString
                        private string sKey = "Location" + iLoc.ToString;
                        if (a.htblLocations.ContainsKey(sKey))
                        {
                            while (a.htblLocations.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        colNewLocs.Add(sKey);
                        .ShortDescription = New Description(GetLine(bAdventure, iPos));
                        .LongDescription = New Description(ConvText(GetLine(bAdventure, iPos)));
                        private string srdesc1 = GetLine(bAdventure, iPos) '?;
                        if (srdesc1 <> "")
                        {
                            private New SingleDescription sd;
                            sd.Description = ConvText(srdesc1);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                            sd.Restrictions.BracketSequence = "#";
                            .LongDescription.Add(sd);
                        }
                        for (int i = 0; i <= iCompassPoints - 1; i++)
                        {
                            iLocations(iLoc, i, 0) = SafeInt(GetLine(bAdventure, iPos)) ' Rooms;
                            if (iLocations(iLoc, i, 0) <> 0)
                            {
                                iLocations(iLoc, i, 1) = SafeInt(GetLine(bAdventure, iPos)) ' Tasks;
                                iLocations(iLoc, i, 2) = SafeInt(GetLine(bAdventure, iPos)) ' Completed;
                                'iLocations(iLoc, i, 3) = SafeInt(GetLine(bAdventure, iPos)) ' Mode
                            }
                        Next;
                        private string srdesc2_0 = GetLine(bAdventure, iPos) '?;
                        private int irtaskno2_0 = SafeInt(GetLine(bAdventure, iPos));
                        if (srdesc2_0 <> "" || irtaskno2_0 > 0)
                        {
                            private New SingleDescription sd;
                            sd.Description = ConvText(srdesc2_0);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                            if (irtaskno2_0 > 0)
                            {
                                private New clsRestriction rest;
                                rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                rest.sKey1 = "Task" + irtaskno2_0;
                                rest.eMust = clsRestriction.MustEnum.Must;
                                rest.eTask = clsRestriction.TaskEnum.Complete;
                                sd.Restrictions.Add(rest);
                            }
                            sd.Restrictions.BracketSequence = "#";
                            .LongDescription.Add(sd);
                        }
                        private string srdesc2_1 = GetLine(bAdventure, iPos) '?;
                        private int irtaskno2_1 = SafeInt(GetLine(bAdventure, iPos));
                        if (srdesc2_1 <> "" || irtaskno2_1 > 0)
                        {
                            private New SingleDescription sd;
                            sd.Description = ConvText(srdesc2_1);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                            if (irtaskno2_1 > 0)
                            {
                                private New clsRestriction rest;
                                rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                rest.sKey1 = "Task" + irtaskno2_1;
                                rest.eMust = clsRestriction.MustEnum.Must;
                                rest.eTask = clsRestriction.TaskEnum.Complete;
                                sd.Restrictions.Add(rest);
                            }
                            sd.Restrictions.BracketSequence = "#";
                            .LongDescription.Add(sd);
                        }
                        private int irobject = SafeInt(GetLine(bAdventure, iPos));
                        private string srdesc3 = GetLine(bAdventure, iPos) '?;
                        private int irhideob = SafeInt(GetLine(bAdventure, iPos));
                        if (srdesc3 <> "")
                        {
                            private New SingleDescription sd;
                            sd.Description = ConvText(srdesc3);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Character;
                            rest.sKey1 = "Player";
                            switch (CInt(irhideob / 10))
                            {
                                case 0:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.MustNot;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                case 1:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                case 2:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.MustNot;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject;
                                case 3:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject;
                                case 4:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.MustNot;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                                case 5:
                                    {
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                            }
                            rest.sKey2 = irobject.ToString ' Needs to be converted once we've loaded objects;
                            sd.Restrictions.Add(rest);
                            sd.Restrictions.BracketSequence = "#";
                            .LongDescription.Add(sd);
                        }

                        if (bSound)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        if (bGraphics)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            if (sFilename <> "")
                            {
                                .LongDescription(0).Description = "<img src=""" + sFilename + """>" + .LongDescription(0).Description;
                            }
                            'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        for (int i = 0; i <= 3; i++)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    .LongDescription(0).Description = "<img src=""" + sFilename + """>" + .LongDescription(0).Description;
                                }
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                        Next;
                        'Dim iNumAltDesc As Integer = CInt(GetLine(bAdventure, iPos))
                        'For iAlt As Integer = 0 To iNumAltDesc - 1
                        '    Dim sd As New SingleDescription

                        '    sd.Description = ConvText(GetLine(bAdventure, iPos)) ' Description
                        '    Dim rest As New clsRestriction
                        '    Dim iTaskObPlayer As Integer = CInt(GetLine(bAdventure, iPos)) ' Options
                        '    Select Case iTaskObPlayer
                        '        Case 0 ' Task
                        '            rest.eType = clsRestriction.RestrictionTypeEnum.Task
                        '        Case 1 ' Object
                        '            rest.eType = clsRestriction.RestrictionTypeEnum.Object
                        '        Case 2 ' Player
                        '            rest.eType = clsRestriction.RestrictionTypeEnum.Character
                        '            rest.sKey1 = "%Player%"
                        '    End Select
                        '    If bSound Then
                        '        sFilename = GetLine(bAdventure, iPos) ' Filename
                        '        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        '        If sFilename <> "" AndAlso iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, False))
                        '    End If
                        '    If bGraphics Then
                        '        sFilename = GetLine(bAdventure, iPos) ' Filename
                        '        If sFilename <> "" Then sd.Description = "<img src=""" & sFilename & """>" & sd.Description
                        '        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        '        If sFilename <> "" AndAlso iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, True))
                        '    End If
                        '    rest.oMessage = New Description(ConvText(GetLine(bAdventure, iPos))) ' Description
                        '    iTaskObPlayer = CInt(GetLine(bAdventure, iPos)) ' Options
                        '    Select Case rest.eType
                        '        Case clsRestriction.RestrictionTypeEnum.Task
                        '            rest.sKey1 = "Task" & iTaskObPlayer
                        '        Case clsRestriction.RestrictionTypeEnum.Object
                        '        Case clsRestriction.RestrictionTypeEnum.Character
                        '            Select Case iTaskObPlayer
                        '                Case 0 ' is not holding
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject
                        '                    rest.eMust = clsRestriction.MustEnum.MustNot
                        '                Case 1 ' is holding
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject
                        '                    rest.eMust = clsRestriction.MustEnum.Must
                        '                Case 2 ' is not wearing
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject
                        '                    rest.eMust = clsRestriction.MustEnum.MustNot
                        '                Case 3 ' is wearing
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject
                        '                    rest.eMust = clsRestriction.MustEnum.Must
                        '                Case 4 ' is not same room as
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject
                        '                    rest.eMust = clsRestriction.MustEnum.MustNot
                        '                Case 5 ' is in same room as
                        '                    rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject
                        '                    rest.eMust = clsRestriction.MustEnum.Must
                        '            End Select
                        '    End Select
                        '    If bSound Then
                        '        sFilename = GetLine(bAdventure, iPos) ' Filename
                        '        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        '        If sFilename <> "" AndAlso iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, False))
                        '    End If
                        '    If bGraphics Then
                        '        sFilename = GetLine(bAdventure, iPos) ' Filename
                        '        If sFilename <> "" Then sd.Description = "<img src=""" & sFilename & """>" & sd.Description
                        '        iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                        '        If sFilename <> "" AndAlso iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, True))
                        '    End If
                        '    GetLine(bAdventure, iPos) ' Hideobs
                        '    GetLine(bAdventure, iPos) ' New Short Desc
                        '    iTaskObPlayer = CInt(GetLine(bAdventure, iPos)) ' Options
                        '    Select Case rest.eType
                        '        Case clsRestriction.RestrictionTypeEnum.Task
                        '            If iTaskObPlayer = 0 Then rest.eMust = clsRestriction.MustEnum.Must Else rest.eMust = clsRestriction.MustEnum.MustNot
                        '            rest.eTask = clsRestriction.TaskEnum.Complete
                        '        Case clsRestriction.RestrictionTypeEnum.Object
                        '            rest.sKey2 = rest.sKey2
                        '        Case clsRestriction.RestrictionTypeEnum.Character
                        '            rest.sKey2 = iTaskObPlayer.ToString
                        '    End Select
                        '    Dim iDisplayWhen As Integer = CInt(GetLine(bAdventure, iPos))
                        '    sd.eDisplayWhen = CType(iDisplayWhen, SingleDescription.DisplayWhenEnum) ' Show When
                        '    If Not (rest.eType = clsRestriction.RestrictionTypeEnum.Task AndAlso rest.sKey1 = "Task0") Then sd.Restrictions.Add(rest)
                        '    sd.Restrictions.BracketSequence = "#"
                        '    .LongDescription.Add(sd)
                        'Next
                        If Adventure.Enabled(clsAdventure.EnabledOptionEnum.Map) Then GetLine(bAdventure, iPos) ' No Map
                    }
                    .htblLocations.Add(Location, Location.Key);
NextLoc:;
                Next;
                'Dim h As New Hashtable
                'h.Add("1", "3")
                'For Each x As System.Collections.DictionaryEntry In h
                '    Dim y As String = CStr(x.Value)
                '    Debug.WriteLine(x)
                'Next

                'iLoc = 0
                '                For Each loc As clsLocation In .htblLocations.Values


                '----------------------------------------------------------------------------------
                ' Objects
                '----------------------------------------------------------------------------------



                private int iNumObjects = CInt(GetLine(bAdventure, iPos));
                private New Collection colNewObs;
                for (int iObj = 1; iObj <= iNumObjects; iObj++)
                {
                    private New clsObject NewObject;
                    With NewObject;
                        '.Key = "Object" & iObj.ToString
                        private string sKey = "Object" + iObj.ToString;
                        if (a.htblObjects.ContainsKey(sKey))
                        {
                            while (a.htblObjects.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        colNewObs.Add(sKey);
                        .Prefix = GetLine(bAdventure, iPos);
                        ConvertPrefix(.Article, .Prefix);
                        .arlNames.Add(GetLine(bAdventure, iPos));
                        private string sAlias = GetLine(bAdventure, iPos);
                        If sAlias <> "" Then .arlNames.Add(sAlias)
                        'Dim iNumAliases As Integer = CInt(GetLine(bAdventure, iPos))
                        'For iAlias As Integer = 1 To iNumAliases
                        '    .arlNames.Add(GetLine(bAdventure, iPos))
                        'Next

                        private New clsProperty sod;
                        sod = Adventure.htblAllProperties("StaticOrDynamic").Copy
                        .htblActualProperties.Add(sod);
                        .bCalculatedGroups = false;
                        .IsStatic = SafeBool(GetLine(bAdventure, iPos));

                        .Description = New Description(ConvText(GetLine(bAdventure, iPos)));
                        private New clsObjectLocation cObjectLocation;
                        if (.IsStatic)
                        {
                            GetLine(bAdventure, iPos) ' ! needed here?;
                            private New clsProperty sl;
                            sl = Adventure.htblAllProperties("StaticLocation").Copy
                            .htblActualProperties.Add(sl);
                            .bCalculatedGroups = false;
                        Else
                            private New clsProperty dl;
                            dl = Adventure.htblAllProperties("DynamicLocation").Copy
                            .htblActualProperties.Add(dl);
                            .bCalculatedGroups = false;

                            private int iv4Loc = CInt(GetLine(bAdventure, iPos));
                            switch (iv4Loc)
                            {
                                case 0:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                case 1:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter;
                                case 2:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject;
                                case 3:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject;
                                case iNumLocations + 4:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter;
                                default:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                    cObjectLocation.Key = "Location" + iv4Loc - 3 + iStartLocations;
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("InLocation").Copy
                                    .htblActualProperties.Add(p);
                                    .bCalculatedGroups = false;
                            }
                        }

                        private string sTaskKey = "Task" + GetLine(bAdventure, iPos);
                        private bool bTaskState = CBool(GetLine(bAdventure, iPos));
                        private string sDescription = GetLine(bAdventure, iPos);
                        if (sTaskKey <> "Task0")
                        {
                            private New SingleDescription sd;
                            sd.Description = sDescription;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.eMust = (clsRestriction.MustEnum.MustNot, clsRestriction.MustEnum.Must)(IIf(bTaskState), clsRestriction.MustEnum);
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            rest.sKey1 = sTaskKey;
                            sd.Restrictions.Add(rest);
                            sd.Restrictions.BracketSequence = "#";
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            .Description.Add(sd);
                        }

                        'Dim AltDesc As New clsObject.AlternativeDescription
                        'With AltDesc
                        '    .sTaskKey = "Task" & GetLine(bAdventure, iPos)
                        '    .bTaskState = CBool(GetLine(bAdventure, iPos))
                        '    .sDescription = GetLine(bAdventure, iPos)
                        'End With
                        '.colAlternativeDescriptions.Add(AltDesc)

                        private New clsObjectLocation StaticLoc;
                        if (.IsStatic)
                        {
                            StaticLoc.StaticExistWhere = (iPos)(GetLine(bAdventure), clsObjectLocation.StaticExistsWhereEnum);
                            '.Location = StaticLoc

                            switch (StaticLoc.StaticExistWhere)
                            {
                                case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                                    {

                                case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                                    {
                                    StaticLoc.Key = "Location" + GetLine(bAdventure, iPos) ' StaticKey;
                                    private New clsProperty pLoc;
                                    pLoc = Adventure.htblAllProperties("AtLocation").Copy
                                    .htblActualProperties.Add(pLoc);
                                    .bCalculatedGroups = false;
                                case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                                    {
                                    private New StringArrayList salRooms;
                                    for (int iRoom = 0; iRoom <= iNumLocations; iRoom++)
                                    {
                                        If CBool(GetLine(bAdventure, iPos)) Then salRooms.Add("Location" + iRoom) ' StaticLoc.StaticKey = "Location" + iRoom ' TODO - Generate a roomgroup and assign that
                                    Next;
                                    StaticLoc.Key = GetRoomGroupFromList(iPos, salRooms, "object '" + .FullName + "'").Key ' StaticKey;
                                    private New clsProperty pLG;
                                    pLG = Adventure.htblAllProperties("AtLocationGroup").Copy
                                    .htblActualProperties.Add(pLG);
                                    .bCalculatedGroups = false;
                                case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                                    {

                                case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                                    {
                                    ' Key defined later
                                case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                                    {
                                    ' Key defined later
                            }
                        }


                        If CBool(GetLine(bAdventure, iPos)) Then ' container
                            private New clsProperty c;
                            c = Adventure.htblAllProperties("Container").Copy
                            c.Selected = true;
                            .htblActualProperties.Add(c);
                            .bCalculatedGroups = false;
                        }
                        If CBool(GetLine(bAdventure, iPos)) Then ' surface
                            private New clsProperty s;
                            s = Adventure.htblAllProperties("Surface").Copy
                            s.Selected = true;
                            .htblActualProperties.Add(s);
                            .bCalculatedGroups = false;
                        }
                        GetLine(bAdventure, iPos) ' Num Holds;

                        if (Not .IsStatic)
                        {
                            If CBool(GetLine(bAdventure, iPos)) Then  ' wearable
                                private New clsProperty w;
                                w = Adventure.htblAllProperties("Wearable").Copy
                                w.Selected = true;
                                .htblActualProperties.Add(w);
                                .bCalculatedGroups = false;
                            }
                            GetLine(bAdventure, iPos) ' weight;
                            private int iParent = CInt(GetLine(bAdventure, iPos));
                            switch (cObjectLocation.DynamicExistWhere)
                            {
                                case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                                    {
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("HeldByWho").Copy
                                    .htblActualProperties.Add(p);
                                    .bCalculatedGroups = false;

                                    if (iParent = 0)
                                    {
                                        cObjectLocation.Key = "%Player%";
                                    Else
                                        cObjectLocation.Key = "Character" + iParent;
                                    }
                                    .Location = cObjectLocation;
                                case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                                    {
                                    if (iParent = 0)
                                    {
                                        cObjectLocation.Key = "%Player%";
                                    Else
                                        cObjectLocation.Key = "Character" + iParent;
                                    }
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("WornByWho").Copy
                                    .htblActualProperties.Add(p);
                                    .bCalculatedGroups = false;
                                    p.Value = cObjectLocation.Key;
                                    .Location = cObjectLocation;
                                case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                                    {
                                    .Location = cObjectLocation;
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("InsideWhat").Copy
                                    .htblActualProperties.Add(p);
                                    .bCalculatedGroups = false;
                                    p.Value = iParent.ToString;
                                case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                                    {
                                    .Location = cObjectLocation;
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("OnWhat").Copy
                                    .htblActualProperties.Add(p);
                                    .bCalculatedGroups = false;
                                    p.Value = iParent.ToString;
                                default:
                                    {
                                    .Location = cObjectLocation;
                            }
                            '.Location = cObjectLocation
                            '.Move(cObjectLocation)
                        }
                        if (.IsStatic && StaticLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter)
                        {
                            private int iChar = CInt(GetLine(bAdventure, iPos));
                            if (iChar = 0)
                            {
                                StaticLoc.Key = "%Player%" ' StaticKey;
                            Else
                                StaticLoc.Key = "Character" + iChar ' StaticKey;
                            }
                            private New clsProperty c;
                            c = Adventure.htblAllProperties("PartOfWho").Copy
                            c.StringData = New Description(ConvText(StaticLoc.Key)) ' StaticKey;
                            .htblActualProperties.Add(c);
                            .bCalculatedGroups = false;
                        }
                        If .IsStatic Then .Move(StaticLoc)

                        private int iOpenableLockable = CInt(GetLine(bAdventure, iPos));
                        If iOpenableLockable > 1 Then iOpenableLockable = 11 - iOpenableLockable
                        ' 0 = Not openable
                        ' 5 = Openable, open
                        ' 6 = Openable, closed
                        ' 7 = Openable, locked
                        if (iOpenableLockable > 0)
                        {
                            private New clsProperty op;
                            op = Adventure.htblAllProperties("Openable").Copy
                            op.Selected = true;
                            .htblActualProperties.Add(op);
                            .bCalculatedGroups = false;

                            private New clsProperty pOS;
                            pOS = Adventure.htblAllProperties("OpenStatus").Copy
                            pOS.Selected = true;
                            if (iOpenableLockable = 5)
                            {
                                pOS.Value = "Open";
                            Else
                                pOS.Value = "Closed";
                            }
                            .htblActualProperties.Add(pOS);
                            .bCalculatedGroups = false;

                            'End If
                            'If iOpenableLockable > 1 Then
                            'Dim iKey As Integer = CInt(GetLine(bAdventure, iPos))
                            'If iKey > -1 Then
                            '    Dim pLk As New clsProperty
                            '    pLk = Adventure.htblAllProperties("Lockable").Copy
                            '    pLk.Selected = True
                            '    .htblActualProperties.Add(pLk)
                            '    .bCalculatedGroups = False

                            '    Dim pKey As New clsProperty
                            '    pKey = Adventure.htblAllProperties("LockKey").Copy
                            '    pKey.Selected = True
                            '    pKey.Value = CStr(iKey) ' "Object" & iKey + iStartObs
                            '    .htblActualProperties.Add(pKey)
                            '    .bCalculatedGroups = False

                            '    Dim pLS As New clsProperty
                            '    pLS = Adventure.htblAllProperties("LockStatus").Copy
                            '    pLS.Selected = True
                            '    If iOpenableLockable = 7 Then pOS.Value = "Locked"
                            '    .htblActualProperties.Add(pLS)
                            '    .bCalculatedGroups = False

                            'End If
                        }

                        '.Openable = CBool(GetLine(bAdventure, iPos))
                        'If .Lockable Then GetLine(bAdventure, iPos) ' key
                        private int iSitStandLie = CInt(GetLine(bAdventure, iPos))  ' Sittable;
                        If iSitStandLie = 1 || iSitStandLie = 3 Then .IsSittable = true
                        .IsStandable = .IsSittable;
                        If iSitStandLie = 2 || iSitStandLie = 3 Then .IsLieable = true
                        If ! .IsStatic Then GetLine(bAdventure, iPos) ' edible

                        if (CBool(GetLine(bAdventure, iPos)))
                        {
                            private New clsProperty r;
                            r = Adventure.htblAllProperties("Readable").Copy
                            r.Selected = true;
                            .htblActualProperties.Add(r);
                            .bCalculatedGroups = false;
                        }
                        if (.Readable)
                        {
                            private string sReadText = GetLine(bAdventure, iPos);
                            if (sReadText <> "")
                            {
                                private New clsProperty r;
                                r = Adventure.htblAllProperties("ReadText").Copy
                                r.Selected = true;
                                .htblActualProperties.Add(r);
                                .bCalculatedGroups = false;
                                .ReadText = sReadText;
                            }
                        }

                        If ! .IsStatic Then GetLine(bAdventure, iPos) ' weapon

                        'Dim iState As Integer = CInt(GetLine(bAdventure, iPos))
                        'If iState > 0 Then
                        '    Dim sStates As String = GetLine(bAdventure, iPos)
                        '    Dim arlStates As New StringArrayList
                        '    For Each sState As String In sStates.Split("|"c)
                        '        arlStates.Add(ToProper(sState))
                        '    Next
                        '    Dim sPKey As String = FindProperty(arlStates)
                        '    Dim s As New clsProperty
                        '    If sPKey Is Nothing Then
                        '        s.Type = clsProperty.PropertyTypeEnum.StateList
                        '        s.Description = "Object can be " & sStates.Replace("|", " or ")
                        '        s.Key = sStates
                        '        s.arlStates = arlStates
                        '        s.Value = arlStates(iState - 1)
                        '        Adventure.htblAllProperties.Add(s.Copy)
                        '    Else
                        '        s = Adventure.htblAllProperties(sPKey).Copy
                        '        s.Value = arlStates(iState - 1)
                        '    End If
                        '    s.Selected = True
                        '    .htblActualProperties.Add(s)
                        '    .bCalculatedGroups = False
                        '    salWithStates.Add(.Key)

                        '    Dim iShowState As Integer = CInt(GetLine(bAdventure, iPos)) ' showstate

                        'End If
                        'Dim bSpecificallyList As Boolean = CBool(GetLine(bAdventure, iPos)) ' showhide
                        'If .IsStatic Then
                        '    .ExplicitlyList = bSpecificallyList
                        'Else
                        '    .ExplicitlyExclude = bSpecificallyList
                        'End If
                        ' GSFX
                        for (int i = 0; i <= 1; i++)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    .Description(0).Description = "<img src=""" + sFilename + """>" + .Description(0).Description;
                                }
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }
                        Next;
                        ' Battle
                        if (bBattleSystem)
                        {
                            GetLine(bAdventure, iPos) ' Armour;
                            GetLine(bAdventure, iPos) ' Hit Points;
                            GetLine(bAdventure, iPos) ' Hit Method;
                            'GetLine(bAdventure, iPos) ' Accuracy
                        }
                        'Dim sSpecialList As String = GetLine(bAdventure, iPos) ' alsohere
                        'If sSpecialList <> "" Then
                        '    Dim r As New clsProperty
                        '    If NewObject.IsStatic Then
                        '        r = Adventure.htblAllProperties("ListDescription").Copy
                        '    Else
                        '        r = Adventure.htblAllProperties("ListDescriptionDynamic").Copy
                        '    End If
                        '    r.Selected = True
                        '    .htblActualProperties.Add(r)
                        '    .bCalculatedGroups = False
                        '    .ListDescription = sSpecialList
                        'End If
                        'Dim s2 As String = GetLine(bAdventure, iPos) ' initial

                        '.htblProperties = .GetPropertiesIncludingGroups()
                    }

                    .htblObjects.Add(NewObject, NewObject.Key);
                Next;

                ' Sort out object keys
                if (Adventure.Player.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                {
                    switch (Adventure.Player.Location.Position)
                    {
                        case clsCharacterLocation.PositionEnum.Standing:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Standable);
                        case clsCharacterLocation.PositionEnum.Sitting:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Sittable);
                        case clsCharacterLocation.PositionEnum.Lying:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Lieable);
                    }
                }
                For Each sLoc As String In colNewLocs
                    private clsLocation loc = a.htblLocations(sLoc);
                    For Each sd As SingleDescription In loc.LongDescription
                        if (sd.Restrictions.Count > 0)
                        {
                            if (sd.Restrictions(0).eType = clsRestriction.RestrictionTypeEnum.Character)
                            {
                                switch (sd.Restrictions(0).eCharacter)
                                {
                                    case clsRestriction.CharacterEnum.BeInSameLocationAsObject:
                                    case clsRestriction.CharacterEnum.BeHoldingObject:
                                        {
                                        sd.Restrictions(0).sKey2 = GetObKey(CInt(sd.Restrictions(0).sKey2) - 1, ComboEnum.Dynamic);
                                    case clsRestriction.CharacterEnum.BeWearingObject:
                                        {
                                        sd.Restrictions(0).sKey2 = GetObKey(CInt(sd.Restrictions(0).sKey2) - 1, ComboEnum.Wearable);
                                }
                            }
                        }
                    Next;
                Next;
                For Each sOb As String In colNewObs
                    private clsObject ob = a.htblObjects(sOb);
                    if (ob.Lockable)
                    {
                        'ob.htblActualProperties("LockKey").Value = GetObKey(CInt(ob.htblActualProperties("LockKey").Value) + iStartObs, ComboEnum.Dynamic)
                        ob.SetPropertyValue("LockKey", GetObKey(CInt(ob.htblActualProperties("LockKey").Value) + iStartObs, ComboEnum.Dynamic));
                        'ob.bCalculatedGroups = False
                    }
                    if (ob.htblActualProperties.ContainsKey("OnWhat"))
                    {
                        'ob.htblActualProperties("OnWhat").Value = GetObKey(CInt(ob.htblActualProperties("OnWhat").Value), ComboEnum.Surface)
                        ob.SetPropertyValue("OnWhat", GetObKey(CInt(ob.htblActualProperties("OnWhat").Value), ComboEnum.Surface));
                        'ob.bCalculatedGroups = False
                    }
                    if (ob.htblActualProperties.ContainsKey("InsideWhat"))
                    {
                        'ob.htblActualProperties("InsideWhat").Value = GetObKey(CInt(ob.htblActualProperties("InsideWhat").Value), ComboEnum.Container)
                        ob.SetPropertyValue("InsideWhat", GetObKey(CInt(ob.htblActualProperties("InsideWhat").Value), ComboEnum.Container));
                        ' ob.bCalculatedGroups = False
                    }
                Next;

                ' Sort out location restrictions
                'For iLoc = 1 To .htblLocations.Count
                iLoc = 0
                For Each sLoc As String In colNewLocs
                    iLoc += 1;
                    private clsLocation loc = Adventure.htblLocations(sLoc) '"Location" + iLoc);
                    'Dim loc As clsLocation = CType(CType(x, DictionaryEntry).Value, clsLocation)
                    for (DirectionsEnum iDir = DirectionsEnum.North; iDir <= DirectionsEnum.NorthWest; iDir++)
                    {
                        '                        For iDir As Integer = 0 To 11
                        if (iLocations(iLoc, iDir, 0) > 0)
                        {
                            if (iLocations(iLoc, iDir, 0) <= Adventure.htblLocations.Count)
                            {
                                loc.arlDirections(iDir).LocationKey = "Location" + iLocations(iLoc, iDir, 0);
                            Else
                                loc.arlDirections(iDir).LocationKey = "Group" + iLocations(iLoc, iDir, 0) - Adventure.htblLocations.Count;
                            }
                            if (iLocations(iLoc, iDir, 1) > 0)
                            {
                                private New clsRestriction rest;
                                'If iLocations(iLoc, iDir, 3) = 0 Then
                                rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                rest.sKey1 = "Task" + iLocations(iLoc, iDir, 1) + iStartTask;
                                if (iLocations(iLoc, iDir, 2) = 0)
                                {
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                Else
                                    rest.eMust = clsRestriction.MustEnum.MustNot;
                                }
                                rest.eTask = clsRestriction.TaskEnum.Complete;
                                'Else
                                'rest.eType = clsRestriction.RestrictionTypeEnum.Property ' clsRestriction.ItemEnum.Object
                                '' Filter on objects with state
                                'rest.sKey1 = "OpenStatus"
                                'rest.sKey2 = GetObKey(iLocations(iLoc, iDir, 1) - 1, ComboEnum.WithStateOrOpenable)
                                'rest.eMust = clsRestriction.MustEnum.Must
                                ''rest.eObject = clsRestriction.ObjectEnum.InState
                                'Dim ob As clsObject = Adventure.htblObjects(rest.sKey2)
                                'If ob.Openable Then
                                '    If iLocations(iLoc, iDir, 2) = 0 Then rest.StringValue = "Open"
                                '    If iLocations(iLoc, iDir, 2) = 1 Then rest.StringValue = "Closed"
                                '    If ob.Lockable AndAlso iLocations(iLoc, iDir, 2) = 2 Then rest.StringValue = "Locked"
                                'End If
                                'Select Case iLocations(iLoc, iDir, 2)

                                'End Select
                                'End If
                                loc.arlDirections(iDir).Restrictions.Add(rest);
                                loc.arlDirections(iDir).Restrictions.BracketSequence = "#";
                            }
                        }
                    Next;
                Next;


                '----------------------------------------------------------------------------------
                ' Tasks
                '----------------------------------------------------------------------------------



                private int iNumTasks = CInt(GetLine(bAdventure, iPos));
                'Dim colNewTasks As New Collection
                for (int iTask = 1; iTask <= iNumTasks; iTask++)
                {
                    private New clsTask NewTask;
                    With NewTask;
                        '.Key = "Task" & iTask.ToString
                        private string sKey = "Task" + iTask.ToString;
                        'Dim sOrigKey As String = sKey
                        if (a.htblTasks.ContainsKey(sKey))
                        {
                            while (a.htblTasks.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        'If sKey <> sOrigKey Then htblKeyMapping.Add(sOrigKey, sKey)
                        .Key = sKey;
                        'colNewTasks.Add(sKey)
                        .Priority = iStartMaxPriority + iTask;
                        private int iNumCommands = CInt(GetLine(bAdventure, iPos));
                        for (int i = 1; i <= iNumCommands + 1; i++)
                        {
                            private string sCommand = GetLine(bAdventure, iPos);

#if Generator
                            .arlCommands.Add(sCommand);
#else
                            ' Simplify Runner so it only has to deal with multiple, or specific refs
                            .arlCommands.Add(sCommand.Replace("%object%", "%object1%").Replace("%character%", "%character1%"));
#endif
                        Next;
                        .TaskType = clsTask.TaskTypeEnum.System;
                        For Each sCommand As String In .arlCommands
                            if (Left(sCommand, 1) <> "#")
                            {
                                .TaskType = clsTask.TaskTypeEnum.General;
                                Exit For;
                            }
                        Next;
                        .Description = .arlCommands(0);
                        .CompletionMessage = New Description(ConvText(GetLine(bAdventure, iPos)));
                        private string sMessage1 = GetLine(bAdventure, iPos);
                        private string sMessage2 = GetLine(bAdventure, iPos);
                        private string sMessage3 = GetLine(bAdventure, iPos);
                        private int iShowRoom = SafeInt(GetLine(bAdventure, iPos));
                        if (iShowRoom > 0)
                        {
                            If .CompletionMessage(0).Description <> "" Then .CompletionMessage(0).Description &= "  "
                            .CompletionMessage(0).Description &= "%DisplayLocation[Location" + iShowRoom + "]%";
                        }
                        If sMessage3 <> "" && .CompletionMessage.ToString = "" Then .eDisplayCompletion = clsTask.BeforeAfterEnum.After Else .eDisplayCompletion = clsTask.BeforeAfterEnum.Before
                        If sMessage3 <> "" Then .CompletionMessage(0).Description = pSpace(.CompletionMessage.ToString) + sMessage3 ' &= "  " + sMessage3
                        'If .CompletionMessage.ToString = "" Then .ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnNoOutput Else .ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnFail
                        If .CompletionMessage.ToString = "" Then .SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions ' .ExecuteParentActions = true
                        ' Needs to be ContinueOnFail so that a failing task with output will be overridden by a lower priority succeeding task, as per v4
                        '.ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnFail ' clsTask.ContinueEnum.ContinueOnNoOutput
                        .bContinueToExecuteLowerPriority = false;
                        .Repeatable = CBool(GetLine(bAdventure, iPos));
                        GetLine(bAdventure, iPos) ' Reversible - give warning if this is set;
                        iNumCommands = CInt(GetLine(bAdventure, iPos))
                        for (int i = 1; i <= iNumCommands + 1; i++)
                        {
                            '.arlReverseCommands.Add(
                            GetLine(bAdventure, iPos);
                        Next;

                        ' Convert rooms executable in into restrictions
                        ' If up to 3 rooms, add as seperate restrictions
                        ' If up to 3 away from all then add as separate restrictions
                        ' Otherwise, create a room group and have that as single restriction
                        '
                        private int iDoWhere = SafeInt(GetLine(bAdventure, iPos)) ' 0=None, 1=Single, 2=Multiple, 3=All;
                        if (iDoWhere = 1)
                        {
                            .arlRestrictions.Add(LocRestriction("Location" + CInt(GetLine(bAdventure, iPos)) + 1 + iStartLocations, true));
                            .arlRestrictions.BracketSequence = "#";
                        }
                        if (iDoWhere = 2)
                        {
                            private bool bHere;
                            private int iCount = 0;
                            private New StringArrayList salHere;
                            private New StringArrayList salNotHere;
                            for (int i = 1; i <= iNumLocations 'Adventure.htblLocations.Count; i++)
                            {
                                bHere = CBool(GetLine(bAdventure, iPos))
                                if (bHere)
                                {
                                    iCount += 1;
                                    salHere.Add("Location" + i + iStartLocations);
                                Else
                                    salNotHere.Add("Location" + i + iStartLocations);
                                }
                            Next;
                            switch (iCount)
                            {
                                case 2:
                                case 3:
                                    {
                                    For Each sLocKey As String In salHere
                                        .arlRestrictions.Add(LocRestriction(sLocKey, true));
                                    Next;
                                    if (iCount = 2)
                                    {
                                        .arlRestrictions.BracketSequence = "(#O#)";
                                    Else
                                        .arlRestrictions.BracketSequence = "(#O#O#)";
                                    }
                                case iNumLocations - 1:
                                case iNumLocations - 2:
                                    {
                                    For Each sLocKey As String In salNotHere
                                        .arlRestrictions.Add(LocRestriction(sLocKey, false));
                                    Next;
                                    if (iCount = iNumLocations - 1)
                                    {
                                        .arlRestrictions.BracketSequence = "#";
                                    Else
                                        .arlRestrictions.BracketSequence = "(#O#)";
                                    }
                                default:
                                    {
                                    .arlRestrictions.Add(LocRestriction(GetRoomGroupFromList(iPos, salHere, "task '" + .Description + "'").Key, true));
                                    .arlRestrictions.BracketSequence = "#";
                            }
                        }

                        private string sQuestion = GetLine(bAdventure, iPos);
                        if (sQuestion <> "")
                        {
                            private New clsHint NewHint;
                            With NewHint;
                                .Key = "Hint" + (Adventure.htblHints.Count + 1).ToString;
                                .Question = sQuestion;
                                .SubtleHint = New Description(ConvText(GetLine(bAdventure, iPos)));
                                .SledgeHammerHint = New Description(ConvText(GetLine(bAdventure, iPos)));
                            }
                            Adventure.htblHints.Add(NewHint, NewHint.Key);
                        }


                        private int iNumRestriction = CInt(GetLine(bAdventure, iPos));
                        for (int i = 1; i <= iNumRestriction; i++)
                        {
                            private New clsRestriction NewRestriction;
                            With NewRestriction;
                                private int iMode = CInt(GetLine(bAdventure, iPos));
                                private int iCombo0 = CInt(GetLine(bAdventure, iPos));
                                private int iCombo1 = CInt(GetLine(bAdventure, iPos));
                                private int iCombo2;
                                If iMode = 0 || iMode > 2 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                If iMode = 4 && iCombo0 > 0 Then iCombo0 += 1
                                'Dim sText As String = Nothing
                                'If iMode = 4 Then sText = GetLine(bAdventure, iPos)
                                switch (iMode)
                                {
                                    case 0 ' Object Locations:
                                        {
                                        .eType = clsRestriction.RestrictionTypeEnum.Object;
                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .sKey1 = NOOBJECT;
                                            case 1:
                                                {
                                                .sKey1 = ANYOBJECT;
                                            case 2:
                                                {
                                                .sKey1 = "ReferencedObject";
                                            default:
                                                {
                                                .sKey1 = GetObKey(iCombo0 - 3, ComboEnum.Dynamic);
                                        }
                                        switch (iCombo1)
                                        {
                                            case 0:
                                            case 6:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeAtLocation;
                                                If iCombo1 = 6 Then .eMust = clsRestriction.MustEnum.MustNot
                                                if (iCombo2 = 0)
                                                {
                                                    .eObject = clsRestriction.ObjectEnum.BeHidden;
                                                Else
                                                    .sKey2 = "Location" + iCombo2;
                                                }
                                            case 1:
                                            case 7:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeHeldByCharacter;
                                                If iCombo1 = 7 Then .eMust = clsRestriction.MustEnum.MustNot
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                }
                                            case 2:
                                            case 8:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeWornByCharacter;
                                                If iCombo1 = 8 Then .eMust = clsRestriction.MustEnum.MustNot
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                }
                                            case 3:
                                            case 9:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeVisibleToCharacter;
                                                If iCombo1 = 9 Then .eMust = clsRestriction.MustEnum.MustNot
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                }
                                            case 4:
                                            case 10:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeInsideObject;
                                                If iCombo1 = 10 Then .eMust = clsRestriction.MustEnum.MustNot
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' Nothing
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Container);
                                                }
                                            case 5:
                                            case 11:
                                                {
                                                .eObject = clsRestriction.ObjectEnum.BeOnObject;
                                                If iCombo1 = 11 Then .eMust = clsRestriction.MustEnum.MustNot
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' Nothing
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Surface);
                                                }
                                        }

                                        'GetLine(bAdventure, iPos) ' combo(2)
                                    case 1 ' Object status:
                                        {
                                        .eType = clsRestriction.RestrictionTypeEnum.Object;
                                        if (iCombo0 = 0)
                                        {
                                            .sKey1 = "ReferencedObject";
                                        Else
                                            .sKey1 = GetObKey(iCombo0 - 1, ComboEnum.WithStateOrOpenable);
                                        }
                                        .eMust = clsRestriction.MustEnum.Must;
                                        .eObject = clsRestriction.ObjectEnum.BeInState;
                                        private clsObject ob = Adventure.htblObjects(.sKey1);
                                        switch (iCombo1)
                                        {
                                            case 0:
                                                {
                                                if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                {
                                                    .sKey2 = "Open";
                                                    '            .sKey2 = "OpenStatus"
                                                    '            .StringValue = "Open"
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.arlStates(iCombo1);
                                                            '.sKey2 = prop.Key
                                                            '.StringValue = prop.arlStates(iCombo1)
                                                        }
                                                    Next;
                                                }
                                            case 1:
                                                {
                                                if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                {
                                                    .sKey2 = "Closed";
                                                    '            .sKey2 = "OpenStatus"
                                                    '            .StringValue = "Closed"
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.arlStates(iCombo1);
                                                            '.sKey2 = prop.Key
                                                            '.StringValue = prop.arlStates(iCombo1)
                                                        }
                                                    Next;
                                                }
                                            case 2:
                                                {
                                                if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                {
                                                    if (.sKey1 = "ReferencedObject" || ob.Lockable)
                                                    {
                                                        .sKey2 = "Locked";
                                                        '.sKey2 = "LockStatus"
                                                        '.StringValue = "Locked"
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                .sKey2 = prop.arlStates(iCombo1 - 2);
                                                                '.sKey2 = prop.Key
                                                                '.StringValue = prop.arlStates(iCombo1 - 2)
                                                            }
                                                        Next;
                                                    }
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.arlStates(iCombo1);
                                                        }
                                                    Next;
                                                }
                                            default:
                                                {
                                                private int iOffset = 0;
                                                if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                {
                                                    If .sKey1 = "ReferencedObject" || ob.Lockable Then iOffset = 3 Else iOffset = 2
                                                }
                                                For Each prop As clsProperty In ob.htblActualProperties.Values
                                                    if (prop.Key.IndexOf("|"c) > 0)
                                                    {
                                                        .sKey2 = prop.arlStates(iCombo1 - iOffset);
                                                        '.sKey2 = prop.Key
                                                        '.StringValue = prop.arlStates(iCombo1 - iOffset)
                                                    }
                                                Next;
                                        }


                                    case 2 ' Task status:
                                        {
                                        .eType = clsRestriction.RestrictionTypeEnum.Task;
                                        .sKey1 = "Task" + iCombo0 + iStartTask;
                                        if (iCombo1 = 0)
                                        {
                                            .eMust = clsRestriction.MustEnum.Must;
                                        Else
                                            .eMust = clsRestriction.MustEnum.MustNot;
                                        }
                                        .eTask = clsRestriction.TaskEnum.Complete;

                                    case 3 ' Characters:
                                        {
                                        .eType = clsRestriction.RestrictionTypeEnum.Character;
                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .sKey1 = "%Player%";
                                            case 1:
                                                {
                                                .sKey1 = "ReferencedCharacter";
                                            default:
                                                {
                                                .sKey1 = "Character" + iCombo0 - 1 + iStartChar;
                                        }
                                        switch (iCombo1)
                                        {
                                            case 0 ' Same room as:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsCharacter;
                                            case 1 ' Not same room as:
                                                {
                                                .eMust = clsRestriction.MustEnum.MustNot;
                                                .eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsCharacter;
                                            case 2 ' Alone:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeAlone;
                                            case 3 ' Not alone:
                                                {
                                                .eMust = clsRestriction.MustEnum.MustNot;
                                                .eCharacter = clsRestriction.CharacterEnum.BeAlone;
                                            case 4 ' standing on:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeStandingOnObject;
                                            case 5 ' sitting on:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeSittingOnObject;
                                            case 6 ' lying on:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeLyingOnObject;
                                            case 7  ' gender:
                                                {
                                                .eMust = clsRestriction.MustEnum.Must;
                                                .eCharacter = clsRestriction.CharacterEnum.BeOfGender;
                                        }
                                        switch (iCombo1)
                                        {
                                            case 0:
                                            case 1:
                                                {
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                }
                                            case 4:
                                                {
                                                ' Standables
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = "TheFloor";
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Standable);
                                                }
                                            case 5:
                                                {
                                                ' Sittables
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = "TheFloor";
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Sittable);
                                                }
                                            case 6:
                                                {
                                                ' Lyables
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = "TheFloor";
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Lieable);
                                                }

                                            case 7:
                                                {
                                                ' Gender
                                                .sKey2 = (clsCharacter.GenderEnum)(iCombo2).ToString;
                                        }
                                    case 4 ' Variables:
                                        {
                                        .eType = clsRestriction.RestrictionTypeEnum.Variable;
                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .sKey1 = "ReferencedNumber";
                                            case 1:
                                                {
                                                .sKey1 = "ReferencedText";
                                            default:
                                                {
                                                .sKey1 = "Variable" + (iCombo0 - 1);
                                        }
                                        .sKey2 = "" ' Arrays not used in v4;
                                        .eMust = clsRestriction.MustEnum.Must;
                                        switch (iCombo1)
                                        {
                                            case 0:
                                            case 10:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.LessThan;
                                            case 1:
                                            case 11:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.LessThanOrEqualTo;
                                            case 2:
                                            case 12:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.EqualTo;
                                            case 3:
                                            case 13:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.GreaterThanOrEqualTo;
                                            case 4:
                                            case 14:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.GreaterThan;
                                            case 5:
                                            case 15:
                                                {
                                                .eVariable = clsRestriction.VariableEnum.EqualTo;
                                                .eMust = clsRestriction.MustEnum.MustNot;
                                        }
                                        if (iCombo1 < 10)
                                        {
                                            .IntValue = iCombo2;
                                            '.StringValue = sText
                                        Else
                                            .IntValue = Integer.MinValue;
                                            .StringValue = "Variable" + iCombo2;
                                        }
                                        'GetLine(bAdventure, iPos) ' combo(2)
                                        'GetLine(bAdventure, iPos) ' text
                                }

                                .oMessage = New Description(ConvText(GetLine(bAdventure, iPos)));
                            }
                            .arlRestrictions.Add(NewRestriction);
                        Next;

                        private int iNumActions = CInt(GetLine(bAdventure, iPos));
                        for (int i = 1; i <= iNumActions; i++)
                        {
                            private New clsAction NewAction;
                            With NewAction;
                                private int m = CInt(GetLine(bAdventure, iPos)) ' mode;
                                private int iCombo0 = CInt(GetLine(bAdventure, iPos));
                                Dim iCombo1, iCombo2, iCombo3 As Integer

                                If m < 4 || m = 6 Then iCombo1 = CInt(GetLine(bAdventure, iPos))
                                If m = 0 || m = 1 || m = 3 || m = 6 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                If m > 4 Then m += 1
                                If m = 1 && iCombo1 = 2 Then iCombo2 += 2
                                if (m = 7)
                                {
                                    If iCombo0 >= 5 && iCombo0 <= 6 Then iCombo0 += 2
                                    If iCombo0 = 7 Then iCombo0 = 11
                                }

                                private string sExpression = "";
                                'If m < 4 Or m = 5 Or m = 6 Or m = 7 Then iCombo1 = CInt(GetLine(bAdventure, iPos))
                                'If m = 0 Or m = 1 Or m = 3 Or m = 6 Or m = 7 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                if (m = 3)
                                {
                                    if (iCombo1 = 5)
                                    {
                                        sExpression = GetLine(bAdventure, iPos) ' expression
                                    Else
                                        iCombo3 = CInt(GetLine(bAdventure, iPos)) ' combo(3)
                                    }
                                }
                                switch (m)
                                {
                                    case 0 ' Move object:
                                        {
                                        .eItem = clsAction.ItemEnum.MoveObject;
                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingHeldBy;
                                                .sKey1 = THEPLAYER;
                                                '.sKey1 = "AllHeldObjects"
                                            case 1:
                                                {
                                                .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingWornBy;
                                                .sKey1 = THEPLAYER;
                                                '.sKey1 = "AllWornObjects"
                                            case 2:
                                                {
                                                .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.Object;
                                                .sKey1 = "ReferencedObject";
                                            default:
                                                {
                                                .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.Object;
                                                .sKey1 = GetObKey(iCombo0 - 3, ComboEnum.Dynamic);
                                        }

                                        switch (iCombo1)
                                        {
                                            case 0:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                                if (iCombo2 = 0)
                                                {
                                                    .sKey2 = "Hidden";
                                                Else
                                                    .sKey2 = "Location" + iCombo2 + iStartLocations;
                                                }
                                            case 1:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocationGroup;
                                            case 2:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.InsideObject;
                                                .sKey2 = GetObKey(iCombo2, ComboEnum.Container);
                                            case 3:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.OntoObject;
                                                .sKey2 = GetObKey(iCombo2, ComboEnum.Surface);
                                            case 4:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.ToCarriedBy;
                                            case 5:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.ToWornBy;
                                            case 6:
                                                {
                                                .eMoveObjectTo = clsAction.MoveObjectToEnum.ToSameLocationAs;
                                        }

                                        if (iCombo1 > 3)
                                        {
                                            switch (iCombo2)
                                            {
                                                case 0:
                                                    {
                                                    .sKey2 = "%Player%";
                                                case 1:
                                                    {
                                                    .sKey2 = "ReferencedCharacter";
                                                default:
                                                    {
                                                    .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                            }
                                        }

                                    case 1 ' Move character:
                                        {
                                        .eItem = clsAction.ItemEnum.MoveCharacter;
                                        .eMoveCharacterWho = clsAction.MoveCharacterWhoEnum.Character;

                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .sKey1 = THEPLAYER;
                                            case 1:
                                                {
                                                .sKey1 = "ReferencedCharacter";
                                            default:
                                                {
                                                .sKey1 = "Character" + iCombo0 - 1 + iStartChar;
                                        }

                                        switch (iCombo1)
                                        {
                                            case 0:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLocation;
                                                if (.sKey1 = "%Player%")
                                                {
                                                    .sKey2 = "Location" + iCombo2 + iStartLocations + 1;
                                                Else
                                                    .sKey2 = "Location" + iCombo2 + iStartLocations;
                                                }
                                                If .sKey2 = "Location0" Then .sKey2 = "Hidden"
                                            case 1:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLocationGroup;

                                            case 2:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToSameLocationAs;
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 2 + iStartChar;
                                                }
                                            case 3:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToStandingOn;
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = THEFLOOR;
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Standable);
                                                }
                                            case 4:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToSittingOn;
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = THEFLOOR;
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Sittable);
                                                }
                                            case 5:
                                                {
                                                .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLyingOn;
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        ' The floor
                                                        .sKey2 = THEFLOOR;
                                                    default:
                                                        {
                                                        .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Lieable);
                                                }
                                        }

                                    case 2 ' Change ob status:
                                        {
                                        .eItem = clsAction.ItemEnum.SetProperties;
                                        .sKey1 = GetObKey(iCombo0, ComboEnum.WithStateOrOpenable);
                                        private clsObject ob = Adventure.htblObjects(.sKey1);
                                        switch (iCombo1)
                                        {
                                            case 0:
                                                {
                                                if (ob.Openable)
                                                {
                                                    .sKey2 = "OpenStatus";
                                                    .sPropertyValue = "Open";
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.Key;
                                                            .sPropertyValue = prop.arlStates(iCombo1);
                                                        }
                                                    Next;
                                                }
                                            case 1:
                                                {
                                                if (ob.Openable)
                                                {
                                                    .sKey2 = "OpenStatus";
                                                    .sPropertyValue = "Closed";
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.Key;
                                                            .sPropertyValue = prop.arlStates(iCombo1);
                                                        }
                                                    Next;
                                                }
                                            case 2:
                                                {
                                                if (ob.Openable)
                                                {
                                                    if (ob.Lockable)
                                                    {
                                                        .sKey2 = "OpenStatus";
                                                        .sPropertyValue = "Locked";
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                .sKey2 = prop.Key;
                                                                .sPropertyValue = prop.arlStates(iCombo1 - 2);
                                                            }
                                                        Next;
                                                    }
                                                Else
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.Key;
                                                            .sPropertyValue = prop.arlStates(iCombo1);
                                                        }
                                                    Next;
                                                }
                                            default:
                                                {
                                                private int iOffset = 0;
                                                if (ob.Openable)
                                                {
                                                    If ob.Lockable Then iOffset = 3 Else iOffset = 2
                                                }
                                                For Each prop As clsProperty In ob.htblActualProperties.Values
                                                    if (prop.Key.IndexOf("|"c) > 0)
                                                    {
                                                        .sKey2 = prop.Key;
                                                        .sPropertyValue = prop.arlStates(iCombo1 - iOffset);
                                                    }
                                                Next;
                                        }

                                    case 3 ' Change variable:
                                        {
                                        .eItem = clsAction.ItemEnum.Variables;
                                        .sKey1 = "Variable" + iCombo0 + 1 '+ iStartVariable;
                                        .eVariables = clsAction.VariablesEnum.Assignment;
                                        .StringValue = iCombo1 + "|" + iCombo2 + "|" + iCombo3 + "|" + sExpression;
                                        'Select Case iCombo1
                                        '    Case 0 ' to exact value
                                        '        .StringValue = iCombo2.ToString
                                        '    Case 1 ' by exact value
                                        '        .StringValue = .sKey1 & " + " & iCombo2.ToString
                                        '    Case 2 ' To Random value between X and Y
                                        '        .StringValue = "Rand(" & iCombo2 & ", " & iCombo3 & ")"
                                        '    Case 3 ' By Random value between X and Y
                                        '        .StringValue = .sKey1 & " + Rand(" & iCombo2 & ", " & iCombo3 & ")"
                                        '    Case 4 ' to referenced number
                                        '        .StringValue = "%number%"
                                        '    Case 5 ' to expression
                                        '        .StringValue = sExpression
                                        '    Case 6, 7, 8, 9, 10
                                        '        .StringValue = ""

                                        '        ' BUT... if this is a string variable
                                        '        ' 0 = Exact Text
                                        '        ' 1 = to referenced text
                                        '        ' 2 = to expression
                                        'End Select

                                    case 4 ' Change score:
                                        {
                                        .eItem = clsAction.ItemEnum.Variables;
                                        .sKey1 = "Score";
                                        .eVariables = clsAction.VariablesEnum.Assignment;
                                        .StringValue = "1|" + iCombo0 + "|0|";
                                        '.IntValue = iCombo0

                                    case 5 ' Set Task:
                                        {
                                        .eItem = clsAction.ItemEnum.SetTasks;
                                        if (iCombo0 = 0)
                                        {
                                            .eSetTasks = clsAction.SetTasksEnum.Execute;
                                        Else
                                            .eSetTasks = clsAction.SetTasksEnum.Unset;
                                        }
                                        .sKey1 = "Task" + iCombo1 + iStartTask + 1;
                                        .StringValue = "";

                                    case 6 ' End game:
                                        {
                                        .eItem = clsAction.ItemEnum.EndGame;
                                        switch (iCombo0)
                                        {
                                            case 0:
                                                {
                                                .eEndgame = clsAction.EndGameEnum.Win;
                                            case 1:
                                                {
                                                .eEndgame = clsAction.EndGameEnum.Neutral;
                                            case 2:
                                            case 3:
                                                {
                                                .eEndgame = clsAction.EndGameEnum.Lose;
                                        }
                                    case 7 ' Battles:
                                        {
                                        ' TODO

                                }
                            }
                            .arlActions.Add(NewAction);
                        Next;

                        private string sBrackSeq = "" 'GetLine(bAdventure, iPos);
                        If .arlRestrictions.Count > 0 Then sBrackSeq = "#"
                        for (int i = 1; i <= .arlRestrictions.Count - 1; i++)
                        {
                            sBrackSeq &= "A#";
                        Next;
                        'If sBrackSeq <> "" AndAlso .arlRestrictions.BracketSequence <> "" Then
                        '    .arlRestrictions.BracketSequence &= "A"
                        'End If
                        .arlRestrictions.BracketSequence = sBrackSeq;
#if Runner
                        .arlRestrictions.BracketSequence = .arlRestrictions.BracketSequence.Replace("[", "((").Replace("]", "))");
#endif

                        if (bSound)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        if (bGraphics)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            If sFilename <> "" Then .CompletionMessage(0).Description = "<img src=""" + sFilename + """>" + .CompletionMessage(0).Description
                            'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                        }
                    }
                    .htblTasks.Add(NewTask, NewTask.Key);
                Next;


                '' Sort task mappings
                'For Each sTask As String In colNewTasks
                '    Dim task As clsTask = a.htblTasks(sTask)
                '    For Each act As clsAction In task.arlActions
                '        If act.eItem = clsAction.ItemEnum.SetTasks Then
                '            If htblKeyMapping.ContainsKey(act.sKey1) Then act.sKey1 = htblKeyMapping(act.sKey1)
                '        End If
                '    Next
                'Next


                '----------------------------------------------------------------------------------
                ' Events
                '----------------------------------------------------------------------------------



                private int iNumEvents = CInt(GetLine(bAdventure, iPos));
                for (int iEvent = 1; iEvent <= iNumEvents; iEvent++)
                {
                    private New clsEvent NewEvent;
                    private string sLocationKey = "";
                    With NewEvent;
                        .Key = "Event" + iEvent.ToString;
                        .Description = GetLine(bAdventure, iPos);
                        .WhenStart = (iPos)(GetLine(bAdventure), clsEvent.WhenStartEnum);
                        if (.WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns)
                        {
                            .StartDelay.iFrom = CInt(GetLine(bAdventure, iPos)) - 1 ' Start1;
                            .StartDelay.iTo = CInt(GetLine(bAdventure, iPos)) - 1 ' Start2;
                        }

                        if (.WhenStart = clsEvent.WhenStartEnum.AfterATask)
                        {
                            private string sStartTask = "Task" + GetLine(bAdventure, iPos);
                            private New EventOrWalkControl ec;
                            ec.eControl = EventOrWalkControl.ControlEnum.Start;
                            ec.sTaskKey = sStartTask;
                            ReDim Preserve .EventControls(.EventControls.Length);
                            .EventControls(.EventControls.Length - 1) = ec;
                        }

                        .Repeating = CBool(GetLine(bAdventure, iPos));
                        private int iTaskMode = CInt(GetLine(bAdventure, iPos)) ' task mode;
                        .Length.iFrom = CInt(GetLine(bAdventure, iPos));
                        .Length.iTo = CInt(GetLine(bAdventure, iPos));
                        if (.WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns)
                        {
                            .Length.iFrom -= 1;
                            .Length.iTo -= 1;
                        }
                        sBuffer = CStr(GetLine(bAdventure, iPos)) ' des1
                        if (sBuffer <> "")
                        {
                            private New clsEvent.SubEvent se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sBuffer));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        sBuffer = CStr(GetLine(bAdventure, iPos)) ' des2
                        if (sBuffer <> "")
                        {
                            private New clsEvent.SubEvent se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.SetLook;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sBuffer));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        private string sEndMessage = CStr(GetLine(bAdventure, iPos)) ' des3;

                        private int iWhichRooms = CInt(GetLine(bAdventure, iPos));
                        switch (iWhichRooms)
                        {
                            case 0 ' No rooms:
                                {
                                sLocationKey = ""
                            case 1 ' Single Room:
                                {
                                sLocationKey = "Location" & (CInt(GetLine(bAdventure, iPos)) + 1)
                            case 2 ' Multiple Rooms:
                                {
                                private bool bShowRoom;
                                private New StringArrayList arlShowInRooms;
                                for (int n = 1; n <= iNumLocations 'Adventure.htblLocations.Count; n++)
                                {
                                    bShowRoom = CBool(GetLine(bAdventure, iPos))
                                    If bShowRoom Then arlShowInRooms.Add("Location" + n)
                                Next;
                                sLocationKey = GetRoomGroupFromList(iPos, arlShowInRooms, "event '" & .Description & "'").Key
                            case 3 ' All Rooms:
                                {
                                sLocationKey = ALLROOMS
                        }

                        'Dim NewEventDescription As New clsEventDescription
                        'With NewEventDescription
                        '    .arlShowInRooms = arlShowInRooms.Clone
                        'End With
                        for (int i = 0; i <= 1; i++)
                        {
                            private int iTask = CInt(GetLine(bAdventure, iPos));
                            private int iCompleteOrNot = CInt(GetLine(bAdventure, iPos));
                            if (iTask > 0)
                            {
                                private New EventOrWalkControl ec;
                                ec.eControl = (EventOrWalkControl.ControlEnum.Suspend, EventOrWalkControl.ControlEnum.Resume)(IIf(i = 0), EventOrWalkControl.ControlEnum);
                                ec.sTaskKey = "Task" + (iTask - 1);
                                ec.eCompleteOrNot = (EventOrWalkControl.CompleteOrNotEnum.Completion, EventOrWalkControl.CompleteOrNotEnum.UnCompletion)(IIf(iCompleteOrNot = 0), EventOrWalkControl.CompleteOrNotEnum);
                                ReDim Preserve .EventControls(.EventControls.Length);
                                .EventControls(.EventControls.Length - 1) = ec;
                            }
                            'For n As Integer = 0 To 1
                            '    GetLine(bAdventure, iPos) ' pause(i,n)
                            'Next
                            private int iFrom = CInt(GetLine(bAdventure, iPos)) ' from(i);
                            sBuffer = CStr(GetLine(bAdventure, iPos)) ' ftext(i)
                            if (sBuffer <> "")
                            {
                                private New clsEvent.SubEvent se;
                                se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                                se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                                se.ftTurns.iFrom = iFrom;
                                se.ftTurns.iTo = iFrom;
                                se.oDescription = New Description(ConvText(sBuffer));
                                ReDim Preserve .SubEvents(.SubEvents.Length);
                                .SubEvents(.SubEvents.Length - 1) = se;
                            }
                        Next;
                        if (sEndMessage <> "")
                        {
                            private New clsEvent.SubEvent se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sEndMessage));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        private clsTask tas = null;
                        Dim iDoneTask(1) As Boolean
                        Dim iMoveObs(2, 1) As Integer
                        For Each i As Integer In New Integer() {1, 2, 0}
                            for (int j = 0; j <= 1; j++)
                            {
                                iMoveObs(i, j) = CInt(GetLine(bAdventure, iPos));
                            Next;
                        Next;
                        for (int i = 0; i <= 2; i++)
                        {
                            private int iObKey = iMoveObs(i, 0) ' CInt(GetLine(bAdventure, iPos));
                            private int iMoveTo = iMoveObs(i, 1) ' CInt(GetLine(bAdventure, iPos));
                            if (iObKey > 0)
                            {
                                private bool bNewTask = true;
                                If i = 1 && NewEvent.Length.iTo = 0 && iDoneTask(0) Then bNewTask = false
                                If i = 2 && ((NewEvent.Length.iTo = 0 && ! iDoneTask(1)) || iDoneTask(1)) Then bNewTask = false

                                if (bNewTask)
                                {
                                    private bool bMultiple = false;
                                    if (tas IsNot null)
                                    {
                                        bMultiple = True
                                        tas.Description = "Generated task #" + i + " for event " + NewEvent.Description;
                                    }
                                    tas = New clsTask
                                    tas.Key = "Task" + (Adventure.htblTasks.Count + 1);
                                    tas.Description = "Generated task" + IIf(bMultiple, " #" + i + 1, "").ToString + " for event " + NewEvent.Description;
                                    tas.Priority = iStartMaxPriority + Adventure.htblTasks.Count + 1;
                                }
                                If i < 2 Then iDoneTask(i) = true

                                With tas;
                                    .TaskType = clsTask.TaskTypeEnum.System;
                                    .Repeatable = true;
                                    private New clsAction act;
                                    act.eItem = clsAction.ItemEnum.MoveObject;
                                    act.sKey1 = "Object" + iObKey;
                                    switch (iMoveTo)
                                    {
                                        case 0 ' Hidden:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                            act.sKey2 = "Hidden";
                                        case 1 ' Players hands:
                                            {
                                            if (Adventure.htblObjects("Object" + iObKey).IsStatic)
                                            {
                                                act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation '  Don't allow for static;
                                                act.sKey2 = "Hidden";
                                            Else
                                                act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToCarriedBy;
                                                act.sKey2 = "%Player%";
                                            }
                                        case 2 ' Same room as player:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToSameLocationAs;
                                            act.sKey2 = "%Player%";
                                        case Else ' Locations:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                            act.sKey2 = "Location" + (iMoveTo - 2);
                                    }

                                    .arlActions.Add(act);
                                }
                                if (bNewTask)
                                {
                                    Adventure.htblTasks.Add(tas, tas.Key);
                                    private New clsEvent.SubEvent se;
                                    se.eWhat = clsEvent.SubEvent.WhatEnum.ExecuteTask;
                                    If i = 0 Then se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent Else se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent
                                    se.ftTurns.iFrom = 0;
                                    se.ftTurns.iTo = 0;
                                    se.sKey = tas.Key;
                                    ReDim Preserve .SubEvents(.SubEvents.Length);
                                    .SubEvents(.SubEvents.Length - 1) = se;
                                }
                            }
                        Next;
                        For Each se As clsEvent.SubEvent In .SubEvents
                            If se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage || se.eWhat = clsEvent.SubEvent.WhatEnum.SetLook Then se.sKey = sLocationKey
                        Next;
                        private string sExecuteTask = "Task" + GetLine(bAdventure, iPos);
                        if (sExecuteTask <> "Task0")
                        {
                            private New clsEvent.SubEvent se;
                            if (iTaskMode = 0)
                            {
                                se.eWhat = clsEvent.SubEvent.WhatEnum.ExecuteTask;
                            Else
                                se.eWhat = clsEvent.SubEvent.WhatEnum.UnsetTask;
                            }
                            se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.sKey = sExecuteTask;
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        for (int i = 0; i <= 4; i++)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                'If sFilename <> "" Then
                                '    . .Description(0).Description &= "<img src=""" & sFilename & """>"
                                'End If
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }
                        Next;
                    }
                    .htblEvents.Add(NewEvent, NewEvent.Key);
                Next;


                '----------------------------------------------------------------------------------
                ' Characters
                '----------------------------------------------------------------------------------


                private int iNumChars = CInt(GetLine(bAdventure, iPos));
                for (int iChar = 1; iChar <= iNumChars; iChar++)
                {
                    private New clsCharacter NewChar;
                    With NewChar;
                        '.Key = "Character" & iChar.ToString
                        private string sKey = "Character" + iChar.ToString;
                        if (a.htblCharacters.ContainsKey(sKey))
                        {
                            while (a.htblCharacters.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        .CharacterType = clsCharacter.CharacterTypeEnum.NonPlayer;
                        .ProperName = GetLine(bAdventure, iPos);
                        .Prefix = GetLine(bAdventure, iPos);
                        ConvertPrefix(.Article, .Prefix);
                        private string sAlias = GetLine(bAdventure, iPos);
                        If sAlias <> "" Then .arlDescriptors.Add(sAlias)
                        'Dim iNumAliases As Integer = CInt(GetLine(bAdventure, iPos))
                        'For i As Integer = 1 To iNumAliases
                        ' .arlDescriptors.Add(GetLine(bAdventure, iPos))
                        'Next
                        'If iNumAliases = 0 AndAlso .Prefix = "" Then .Article = ""
                        .Description = New Description(ConvText(GetLine(bAdventure, iPos)));
                        '.Location = "Location" & GetLine(bAdventure, iPos)
                        .Known = true;

                        private int iCharLoc = CInt(GetLine(bAdventure, iPos));
                        if (iCharLoc > 0)
                        {
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                            .Location.Key = "Location" + iCharLoc;
                        Else
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                        }
                        'If Adventure.htblAllProperties.ContainsKey("CharacterLocation") Then
                        '    Dim cl As New clsProperty
                        '    cl = Adventure.htblAllProperties("CharacterLocation").Copy
                        '    If iCharLoc = 0 Then cl.Value = "Hidden" Else cl.Value = "At Location"
                        '    .htblProperties.Add(cl, cl.Key)

                        '    If cl.Value = "At Location" Then
                        '        Dim al As New clsProperty
                        '        al = Adventure.htblAllProperties("CharacterAtLocation").Copy
                        '        al.Value = "Location" & iCharLoc
                        '        .htblProperties.Add(al, al.Key)
                        '    End If
                        'End If
                        private clsProperty p;
                        'p = Adventure.htblAllProperties("Known").Copy
                        'p.Selected = True
                        '.htblActualProperties.Add(p)
                        '.bCalculatedGroups = False

                        private string sDesc2 = GetLine(bAdventure, iPos);
                        private string sDescTask = GetLine(bAdventure, iPos);
                        if (sDescTask <> "0")
                        {
                            private New SingleDescription sd;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.sKey1 = "Task" + sDescTask;
                            rest.eMust = clsRestriction.MustEnum.Must;
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            sd.Restrictions.Add(rest);
                            sd.Description = sDesc2;
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            sd.Restrictions.BracketSequence = "#";
                            .Description.Add(sd);
                        }
                        private int iNumSubjects = CInt(GetLine(bAdventure, iPos));
                        for (int i = 1; i <= iNumSubjects; i++)
                        {
                            private New clsTopic Topic;
                            With Topic;
                                .Key = "Topic" + i;
                                .Keywords = GetLine(bAdventure, iPos);
                                .Summary = "Ask about " + .Keywords;
                                .oConversation = New Description(ConvText(GetLine(bAdventure, iPos)));
                                .bAsk = true;
                                private int iTask = CInt(GetLine(bAdventure, iPos)) ' Rep Task;
                                if (iTask > 0)
                                {
                                    private New SingleDescription sd;
                                    private New clsRestriction rest;
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    rest.sKey1 = "Task" + iTask;
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eTask = clsRestriction.TaskEnum.Complete;
                                    sd.Restrictions.Add(rest);
                                    sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                                    sd.Restrictions.BracketSequence = "#";
                                    .oConversation.Add(sd);
                                }
                                .oConversation.Item(.oConversation.Count - 1).Description &= GetLine(bAdventure, iPos) ' Response 2;
                            }
                            .htblTopics.Add(Topic);
                        Next;

                        private int iNumWalks = CInt(GetLine(bAdventure, iPos));
                        private New StringArrayList arlNewDescriptions;
                        for (int i = 1; i <= iNumWalks; i++)
                        {
                            private New clsWalk Walk;
                            With Walk;
                                .sKey = sKey;
                                private string sStartTaskKey = "";
                                private int iNumberOfSteps = CInt(GetLine(bAdventure, iPos));
                                .Loops = CBool(GetLine(bAdventure, iPos));
                                private int iTask = CInt(GetLine(bAdventure, iPos));
                                if (iTask = 0)
                                {
                                    .StartActive = true;
                                Else
                                    .StartActive = false;
                                    sStartTaskKey = "Task" & iTask
                                    private New EventOrWalkControl wc;
                                    wc.eControl = EventOrWalkControl.ControlEnum.Start;
                                    wc.sTaskKey = sStartTaskKey;
                                    ReDim Preserve .WalkControls(.WalkControls.Length);
                                    .WalkControls(.WalkControls.Length - 1) = wc;
                                }
                                private int iCharTask = CInt(GetLine(bAdventure, iPos)) ' Runtask;
                                private int iObFind = CInt(GetLine(bAdventure, iPos)) ' Obfind;
                                private int iObTask = CInt(GetLine(bAdventure, iPos)) ' Obtask;
                                if (iObFind > 0 && iObTask > 0)
                                {
                                    private New clsWalk.SubWalk sw;
                                    sw.eWhat = clsWalk.SubWalk.WhatEnum.ExecuteTask;
                                    sw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                                    sw.sKey = "Object" + iObFind;
                                    sw.sKey2 = "Task" + iObTask;
                                    ReDim Preserve .SubWalks(.SubWalks.Length);
                                    .SubWalks(.SubWalks.Length - 1) = sw;
                                }
                                private string sTerminateTaskKey = "Task" + GetLine(bAdventure, iPos);
                                if (sTerminateTaskKey <> "Task0")
                                {
                                    private New EventOrWalkControl wc;
                                    wc.eControl = EventOrWalkControl.ControlEnum.Stop;
                                    wc.sTaskKey = sTerminateTaskKey;
                                    ReDim Preserve .WalkControls(.WalkControls.Length);
                                    .WalkControls(.WalkControls.Length - 1) = wc;
                                }
                                'Dim iCharFind As Integer = CInt(GetLine(bAdventure, iPos)) ' Who
                                if (iCharTask > 0)
                                {
                                    private New clsWalk.SubWalk sw;
                                    sw.eWhat = clsWalk.SubWalk.WhatEnum.ExecuteTask;
                                    sw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                                    sw.sKey = "%Player%";
                                    sw.sKey2 = "Task" + iCharTask;
                                    ReDim Preserve .SubWalks(.SubWalks.Length);
                                    .SubWalks(.SubWalks.Length - 1) = sw;
                                }
                                private string sNewDescription = GetLine(bAdventure, iPos);
                                if (sNewDescription <> "")
                                {
                                    arlNewDescriptions.Add(sStartTaskKey);
                                    arlNewDescriptions.Add(sNewDescription);
                                }
                                'Dim sDescription As String = ""
                                for (int j = 1; j <= iNumberOfSteps; j++)
                                {
                                    private New clsWalk.clsStep Stp;
                                    With Stp;
                                        private int iLocation = CInt(GetLine(bAdventure, iPos));
                                        switch (iLocation)
                                        {
                                            case 0 ' Hidden:
                                                {
                                                .sLocation = "Hidden";
                                                'sDescription &= "Hidden"
                                            case 1 ' Follow Player:
                                                {
                                                .sLocation = "%Player%";
                                                'sDescription &= "Follow Player"
                                            case Else ' Locations:
                                                {
                                                if (iLocation - 1 > Adventure.htblLocations.Count)
                                                {
                                                    ' Location Group
                                                    'sDescription = "Group " & iLocation - 1
                                                    .sLocation = "Group" + iLocation - Adventure.htblLocations.Count - 1;
                                                Else
                                                    ' Location
                                                    .sLocation = "Location" + iLocation - 1;
                                                    'sDescription &= Adventure.htblLocations(.sLocation).ShortDescription
                                                }
                                        }
                                        private int iWaitTurns = CInt(GetLine(bAdventure, iPos));
                                        If .sLocation = "%Player%" && iWaitTurns = 1 Then iWaitTurns = 0
                                        .ftTurns.iFrom = iWaitTurns;
                                        .ftTurns.iTo = iWaitTurns;
                                    }
                                    .arlSteps.Add(Stp);
                                Next;
                                .Description = .GetDefaultDescription;
                            }
                            .arlWalks.Add(Walk);
                        Next;
                        private bool bShowMove = CBool(GetLine(bAdventure, iPos));
                        private string sFromDesc = null;
                        private string sToDesc = null;
                        if (bShowMove)
                        {
                            p = Adventure.htblAllProperties("ShowEnterExit").Copy
                            p.Selected = true;
                            .htblActualProperties.Add(p);
                            .bCalculatedGroups = false;
                            sFromDesc = GetLine(bAdventure, iPos)
                            p = Adventure.htblAllProperties("CharEnters").Copy
                            p.Selected = true;
                            p.StringData = New Description(ConvText(sFromDesc));
                            .htblActualProperties.Add(p);
                            .bCalculatedGroups = false;
                            sToDesc = GetLine(bAdventure, iPos)
                            p = Adventure.htblAllProperties("CharExits").Copy
                            p.Selected = true;
                            p.StringData = New Description(ConvText(sToDesc));
                            .htblActualProperties.Add(p);
                            .bCalculatedGroups = false;
                        }
                        'For Each Walk As clsWalk In .arlWalks
                        '    Walk.ShowMove = bShowMove
                        '    Walk.FromDesc = sFromDesc
                        '    Walk.ToDesc = sToDesc
                        'Next
                        private string sIsHereDesc = GetLine(bAdventure, iPos);
                        If sIsHereDesc = "#" Then sIsHereDesc = "%CharacterName[" + sKey + "]% is here."
                        if (sIsHereDesc <> "" || arlNewDescriptions.Count > 0)
                        {
                            p = Adventure.htblAllProperties("CharHereDesc").Copy
                            p.Selected = true;
                            .htblActualProperties.Add(p);
                            .bCalculatedGroups = false;
                        }
                        If sIsHereDesc <> "" Then .htblActualProperties("CharHereDesc").StringData = New Description(ConvText(sIsHereDesc))
                        for (int i = 0; i <= arlNewDescriptions.Count - 1; i += 2)
                        {
                            private New SingleDescription sd;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.sKey1 = arlNewDescriptions(i);
                            rest.eMust = clsRestriction.MustEnum.Must;
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            sd.Restrictions.Add(rest);
                            sd.Restrictions.BracketSequence = "#";
                            sd.Description = arlNewDescriptions(i + 1);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            .htblActualProperties("CharHereDesc").StringData.Add(sd);
                        Next;
                        .Gender = (iPos)(GetLine(bAdventure), clsCharacter.GenderEnum);
                        for (int i = 0; i <= 3; i++)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    If i = 0 Then .Description(0).Description = "<img src=""" + sFilename + """>" + .Description(0).Description
                                    If i = 1 Then .Description(1).Description = "<img src=""" + sFilename + """>" + .Description(1).Description
                                    If i = 2 Then .htblActualProperties("CharEnters").StringData(0).Description = "<img src=""" + sFilename + """>" + .htblActualProperties("CharEnters").StringData(0).Description
                                    If i = 3 Then .htblActualProperties("CharExits").StringData(0).Description = "<img src=""" + sFilename + """>" + .htblActualProperties("CharExits").StringData(0).Description
                                }
                                'iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }
                        Next;
                        if (bBattleSystem)
                        {
                            GetLine(bAdventure, iPos) ' Attitude;
                            GetLine(bAdventure, iPos) ' Min Stamina;
                            'GetLine(bAdventure, iPos) ' Max Stamina
                            GetLine(bAdventure, iPos) ' Min Strength;
                            'GetLine(bAdventure, iPos) ' Max Strength
                            'GetLine(bAdventure, iPos) ' Min Accuracy
                            'GetLine(bAdventure, iPos) ' Max Accuracy
                            GetLine(bAdventure, iPos) ' Min Defence;
                            'GetLine(bAdventure, iPos) ' Max Defence
                            'GetLine(bAdventure, iPos) ' Min Agility
                            'GetLine(bAdventure, iPos) ' Max Agility
                            GetLine(bAdventure, iPos) ' Speed;
                            GetLine(bAdventure, iPos) ' Die Task;
                            'GetLine(bAdventure, iPos) ' Recovery
                            'GetLine(bAdventure, iPos) ' Low Task
                        }
                    }
                    .htblCharacters.Add(NewChar, NewChar.Key);
                Next;


                '----------------------------------------------------------------------------------
                ' Groups
                '----------------------------------------------------------------------------------


                ' Only room groups defined in 4.0 files
                private int iNumGroups = CInt(GetLine(bAdventure, iPos));
                for (int iGroup = 1; iGroup <= iNumGroups; iGroup++)
                {
                    private New clsGroup NewGroup;
                    With NewGroup;
                        .Key = "Group" + iGroup.ToString;
                        .Name = GetLine(bAdventure, iPos);
                        private bool bIncluded;
                        for (int i = 1; i <= iNumLocations 'Adventure.htblLocations.Count; i++)
                        {
                            bIncluded = CBool(GetLine(bAdventure, iPos))
                            If bIncluded Then .arlMembers.Add("Location" + i.ToString)
                        Next;
                    }
                    .htblGroups.Add(NewGroup, NewGroup.Key);
                Next;

                ' Sort out anything which needed groups defined
                For Each c As clsCharacter In Adventure.htblCharacters.Values
                    For Each w As clsWalk In c.arlWalks
                        For Each g As clsGroup In Adventure.htblGroups.Values
                            if (w.Description.Contains("<" + g.Key + ">"))
                            {
                                w.Description = w.Description.Replace("<" + g.Key + ">", g.Name);
                            }
                        Next;
                    Next;
                Next;


                '----------------------------------------------------------------------------------
                ' Synonyms
                '----------------------------------------------------------------------------------

                private int iNumSyn = CInt(GetLine(bAdventure, iPos));
                for (int iSyn = 1; iSyn <= iNumSyn; iSyn++)
                {
                    private string sTo = GetLine(bAdventure, iPos) ' System Command;
                    private string sFrom = GetLine(bAdventure, iPos) ' Alternative Command;
                    private clsSynonym synNew = null;
                    For Each syn As clsSynonym In Adventure.htblSynonyms.Values
                        if (syn.ChangeTo = sTo)
                        {
                            synNew = syn
                            Exit For;
                        }
                    Next;
                    if (synNew Is null)
                    {
                        synNew = New clsSynonym
                        synNew.Key = "Synonym" + iSyn.ToString;
                    }
                    With synNew;
                        .ChangeTo = sTo;
                        .ChangeFrom.Add(sFrom);
                    }
                    If ! .htblSynonyms.ContainsKey(synNew.Key) Then .htblSynonyms.Add(synNew)
                Next;


                '----------------------------------------------------------------------------------
                ' Variables
                '----------------------------------------------------------------------------------

                private int iNumVariables = CInt(GetLine(bAdventure, iPos));
                for (int iVar = 1; iVar <= iNumVariables; iVar++)
                {
                    private New clsVariable NewVariable;
                    With NewVariable;
                        .Key = "Variable" + iVar.ToString;
                        .Name = GetLine(bAdventure, iPos);
                        .Type = clsVariable.VariableTypeEnum.Numeric ' (iPos)(GetLine(bAdventure), clsVariable.VariableTypeEnum);
                        'If .Type = clsVariable.VariableTypeEnum.Numeric Then
                        .IntValue = CInt(GetLine(bAdventure, iPos));
                        'Else
                        '.StringValue = GetLine(bAdventure, iPos)
                        'End If
                    }
                    .htblVariables.Add(NewVariable, NewVariable.Key);
                Next;

                ' Change Variable names in Actions/Restrictions, and sort assignments
                For Each tas As clsTask In Adventure.htblTasks.Values
                    For Each rest As clsRestriction In tas.arlRestrictions
                        if (rest.eType = clsRestriction.RestrictionTypeEnum.Variable)
                        {
                            if (Adventure.htblVariables.ContainsKey(rest.sKey1))
                            {
                                if (Adventure.htblVariables(rest.sKey1).Type = clsVariable.VariableTypeEnum.Text)
                                {
                                    switch (rest.eVariable)
                                    {
                                        case clsRestriction.VariableEnum.LessThan:
                                            {
                                            rest.eVariable = clsRestriction.VariableEnum.EqualTo;
                                        case clsRestriction.VariableEnum.LessThanOrEqualTo:
                                            {
                                            rest.eVariable = clsRestriction.VariableEnum.EqualTo;
                                            rest.eMust = clsRestriction.MustEnum.MustNot;
                                    }
                                    rest.StringValue = """" + rest.StringValue + """";
                                }
                            }
                        }
                    Next;
                    For Each act As clsAction In tas.arlActions
                        if (act.eItem = clsAction.ItemEnum.Variables)
                        {
                            private int iCombo1 = CInt(act.StringValue.Split("|"c)(0));
                            private int iCombo2 = CInt(act.StringValue.Split("|"c)(1));
                            private int iCombo3 = CInt(act.StringValue.Split("|"c)(2));
                            private string sExpression = act.StringValue.Split("|"c)(3);
                            if (a.htblVariables(act.sKey1).Type = clsVariable.VariableTypeEnum.Numeric)
                            {
                                switch (iCombo1)
                                {
                                    case 0 ' to exact value:
                                        {
                                        act.StringValue = iCombo2.ToString;
                                    case 1 ' by exact value:
                                        {
                                        act.StringValue = "%" + a.htblVariables(act.sKey1).Name + "% + " + iCombo2.ToString;
                                    case 2 ' To Random value between X and Y:
                                        {
                                        act.StringValue = "Rand(" + iCombo2 + ", " + iCombo3 + ")";
                                    case 3 ' By Random value between X and Y:
                                        {
                                        act.StringValue = "%" + a.htblVariables(act.sKey1).Name + "% + Rand(" + iCombo2 + ", " + iCombo3 + ")";
                                    case 4 ' to referenced number:
                                        {
                                        act.StringValue = "%number%";
                                    case 5 ' to expression:
                                        {
                                        act.StringValue = sExpression;
                                    case 6:
                                    case 7:
                                    case 8:
                                    case 9:
                                    case 10:
                                        {
                                        act.StringValue = "";
                                }
                            Else
                                switch (iCombo1)
                                {
                                    case 0 ' exact text:
                                        {
                                        act.StringValue = """" + sExpression.Replace("""", "\""") + """";
                                    case 1 ' to referenced text:
                                        {
                                        act.StringValue = "%text%";
                                    case 2 ' to expression:
                                        {
                                        act.StringValue = sExpression;
                                }
                            }

                        }
                        if (sInstr(act.StringValue, "Variable") > 0)
                        {
                            for (int iVar = Adventure.htblVariables.Count; iVar <= 1; iVar += -1)
                            {
                                If Adventure.htblVariables.ContainsKey("Variable" + iVar) Then act.StringValue = act.StringValue.Replace("Variable" + iVar, "%" + Adventure.htblVariables("Variable" + iVar).Name + "%")
                            Next;
                        }
                    Next;
                Next;



                '----------------------------------------------------------------------------------
                ' ALRs
                '----------------------------------------------------------------------------------

                private int iNumALR = CInt(GetLine(bAdventure, iPos));
                for (int iALR = 1; iALR <= iNumALR; iALR++)
                {
                    private New clsALR NewALR;
                    With NewALR;
                        .Key = "ALR" + iALR.ToString;
                        .OldText = GetLine(bAdventure, iPos);
                        .NewText = New Description(ConvText(GetLine(bAdventure, iPos)));
                    }
                    .htblALRs.Add(NewALR, NewALR.Key);
                Next;


                private bool bSetFont = CBool(SafeInt(GetLine(bAdventure, iPos))) ' Set Font?;
                if (bSetFont)
                {
                    private string sFont = GetLine(bAdventure, iPos) ' Font;
                    if (sFont.Contains(","))
                    {
                        Adventure.DefaultFontName = sFont.Split(","c)(0);
                        Adventure.DefaultFontSize = SafeInt(sFont.Split(","c)(1));
                    }
                }

                'Dim iMediaOffset As Integer = bAdvZLib.Length + 23
                'For Each m As clsAdventure.v4Media In a.dictv4Media.Values
                '    m.iOffset = iMediaOffset
                '    iMediaOffset += m.iLength + 1
                'Next

                '--- the rest ---
                while (iPos < bAdventure.Length - 1)
                {
                    private string s3 = GetLine(bAdventure, iPos);
                }

                ' Make sure all the 'seen's are set
                For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    ch.Move(ch.Location);
                Next;

#if Not www
                .Map = New clsMap;
                .Map.RecalculateLayout();
#if Generator
                fGenerator.Map1.Map = .Map;

                if (a.dictv4Media.Count > 0)
                {
                    if (MessageBox.Show("Would you like to extract all media from this TAF?", "Extract Media", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
                    {
                        fGenerator.fbd.ShowDialog();
                        if (fGenerator.fbd.SelectedPath <> "")
                        {
                            For Each sFile As String In a.dictv4Media.Keys
                                With a.dictv4Media(sFile);
                                    if (.bImage)
                                    {
                                        private Image img = Getv4Image(sFile);
                                        if (img IsNot null)
                                        {
                                            private string sJustFile = IO.Path.GetFileName(sFile);
                                            img.Save(fGenerator.fbd.SelectedPath + "\" + sJustFile);
                                            For Each itm As clsItem In a.dictAllItems.Values
                                                itm.SearchAndReplace(sFile, fGenerator.fbd.SelectedPath + "\" + sJustFile);
                                            Next;
                                        }
                                    Else
                                        ' TODO Sound
                                    }
                                }
                            Next;
                        }
                        a.dictv4Media.Clear();
                    }
                }
#else
                fRunner.Map.Map = .Map;
#endif
#endif

            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error loading Adventure", ex);
            return false;
        }
        finally
        {
            bAdventure = Nothing
        }

        return true;

    }
#endif


    private bool LoadOlder(double v)
    {

        try
        {

            if (v < 4)
            {
#if Mono
                ErrMsg("Unfortunately it is not possible to load ADRIFT 3.90 files directly, using Mono, as the Mono framework does not implement a .Net compatible Random function.  Sorry!");
                return false;
#endif
            }

            Cursor.Current = Cursors.WaitCursor;

            if (Adventure.htblAllProperties.Count = 0)
            {
                ErrMsg("You must select at least one library within Generator > File > Settings > Libraries before loading ADRIFT v" + v.ToString("#.0") + " adventures.");
                return false;
            }

            ' Safety check...
            private string sPropCheck = "";
            For Each sProperty As String In New String() {"AtLocation", "AtLocationGroup", "CharacterAtLocation", "CharacterLocation", "CharEnters", "CharExits", "Container", "DynamicLocation", "HeldByWho", "InLocation", "InsideWhat", "Lockable", "LockKey", "LockStatus", "ListDescription", "OnWhat", "Openable", "OpenStatus", "PartOfWho", "Readable", "ReadText", "ShowEnterExit", "StaticLocation", "StaticOrDynamic", "Surface", "Wearable", "WornByWho"}
                if (Not Adventure.htblAllProperties.ContainsKey(sProperty))
                {
                    sPropCheck &= sProperty + vbCrLf;
                }
            Next;
            if (sPropCheck <> "")
            {
                ErrMsg("Library must contain the following properties before loading ADRIFT v" + v.ToString("#.0") + " files:" + vbCrLf + sPropCheck);
                return false;
            }

            Dim bAdvZLib() As Byte = Nothing
            if (v < 4)
            {
                br.BaseStream.Position = 0;
                Dim bRawData() As Byte = br.ReadBytes(CInt(br.BaseStream.Length))
                private string sRawData = System.Text.Encoding.Default.GetString(bRawData);
                bAdventure = Dencode(sRawData, 0)
            Else
                br.ReadBytes(2) ' CrLf;
                private long lSize = CLng(System.Text.Encoding.Default.GetString(br.ReadBytes(8)));
                bAdvZLib = br.ReadBytes(CInt(lSize - 23))
                private string sPassword = System.Text.Encoding.Default.GetString(Dencode(System.Text.Encoding.Default.GetString(br.ReadBytes(12)), lSize + 1));


                ReDim bAdventure(-1);

                private New System.IO.MemoryStream outStream;
                private New ZOutputStream(outStream) zStream;
                private New System.IO.MemoryStream(bAdvZLib) inStream;
                try
                {
                    CopyStream(inStream, zStream);
                    bAdventure = outStream.ToArray
                }
                finally
                {
                    zStream.Close();
                    outStream.Close();
                    inStream.Close();
                }
            }

            private clsAdventure a = Adventure;
            With Adventure;

                private int iStartMaxPriority = CurrentMaxPriority();
                private int iStartLocations = .htblLocations.Count;
                private int iStartObs = .htblObjects.Count;
                private int iStartTask = .htblTasks.Count;
                private int iStartChar = .htblCharacters.Count;
                private int iStartVariable = .htblVariables.Count;
                private bool bSound = false;
                private bool bGraphics = false;
                private string sFilename;
                private int iFilesize;
                .dictv4Media = New Dictionary(Of String, clsAdventure.v4Media);

                'Dim htblKeyMapping As New StringHashTable

                '.bV4Compatibility = True
                .TaskExecution = clsAdventure.TaskExecutionEnum.HighestPriorityPassingTask;

                salWithStates.Clear();
                private int iPos = 0;
                private string sBuffer = null;
                ' Read the introduction

                private string sTerminator;
                if (v < 4)
                {
                    sTerminator = "**"
                Else
                    sTerminator = "½Ð"
#if Mono
                If IsRunningOnMono() Then sTerminator = ChrW(65533) + ChrW(65533)
#endif
                }


                while (iPos < bAdventure.Length - 1 And sBuffer <> sTerminator)
                {
                    sBuffer = GetLine(bAdventure, iPos)
                    If sBuffer <> sTerminator Then .Introduction.Item(0).Description &= "<br>" + sBuffer
                }
                private int iStartLocation = CInt(GetLine(bAdventure, iPos));
                sBuffer = Nothing
                while (iPos < bAdventure.Length - 1 And sBuffer <> sTerminator)
                {
                    sBuffer = GetLine(bAdventure, iPos)
                    If sBuffer <> sTerminator Then .WinningText.Item(0).Description &= "<br>" + sBuffer
                }
                .Title = GetLine(bAdventure, iPos);
                .Author = GetLine(bAdventure, iPos);
                if (v < 3.9)
                {
                    private string sNoOb = GetLine(bAdventure, iPos);
                    'player.maxsize = Format(noob) & "2"
                    'Player.MaxWeight = Format(noob) & "2"
                }
                .NotUnderstood = GetLine(bAdventure, iPos);
                private int iPer = CInt(GetLine(bAdventure, iPos)) + 1 ' Perspective;
                .ShowExits = CBool(GetLine(bAdventure, iPos));
                .WaitTurns = SafeInt(GetLine(bAdventure, iPos)) ' WaitNum;

                private int iCompassPoints = 8;
                private bool bBattleSystem = false;

                if (v >= 3.9)
                {
                    .ShowFirstRoom = CBool(GetLine(bAdventure, iPos)) ' ShowFirst;
                    bBattleSystem = CBool(GetLine(bAdventure, iPos))
                    .MaxScore = CInt(GetLine(bAdventure, iPos));

                    ' Delete the default char in new file
                    .htblCharacters.Remove("Player");
                    private New clsCharacter Player;
                    With Player;
                        .Key = "Player";
                        .CharacterType = clsCharacter.CharacterTypeEnum.Player;
                        .Perspective = (PerspectiveEnum)(iPer);
                        .ProperName = GetLine(bAdventure, iPos);
                        If .ProperName = "" || .ProperName = "Anonymous" Then .ProperName = "Player"
                        private bool bPromptName = CBool(GetLine(bAdventure, iPos))  ' PromptName;
                        .arlDescriptors.Add("myself");
                        .arlDescriptors.Add("me");

                        private New clsProperty p;
                        p = Adventure.htblCharacterProperties("Known").Copy
                        p.Selected = true;
                        .AddProperty(p);

                        .Description = New Description(ConvText(GetLine(bAdventure, iPos))) ' Description;
                        private int iTask = CInt(GetLine(bAdventure, iPos)) ' Task;
                        if (iTask > 0)
                        {
                            GetLine(bAdventure, iPos) ' Description;
                        }
                        .Location.Position = (iPos)(GetLine(bAdventure), clsCharacterLocation.PositionEnum);
                        private int iOnWhat = CInt(GetLine(bAdventure, iPos)) ' OnWhat;
                        private bool bPromptGender = false;
                        switch (SafeInt(GetLine(bAdventure, iPos)) ' Sex)
                        {
                            case 0 ' Male:
                                {
                                .Gender = clsCharacter.GenderEnum.Male;
                            case 1 ' Female:
                                {
                                .Gender = clsCharacter.GenderEnum.Female;
                            case 2 ' Prompt:
                                {
                                .Gender = clsCharacter.GenderEnum.Unknown;
                                bPromptGender = True
                        }

                        if (bPromptName || bPromptGender)
                        {
                            private New clsTask tasPrompt;
                            With tasPrompt;
                                .Key = "GenTask" + (Adventure.htblTasks.Count + 1) ' Give it a unique key so it doesn't disrupt events calling tasks by index;
                                .Description = "Generated task for Player prompts";
                                .TaskType = clsTask.TaskTypeEnum.System;
                                .RunImmediately = true;
                                .Priority = iStartMaxPriority + Adventure.htblTasks.Count + 1;
                                if (bPromptGender)
                                {
                                    private New clsAction act;
                                    act.eItem = clsAction.ItemEnum.SetProperties;
                                    act.sKey1 = Player.Key;
                                    act.sKey2 = "Gender";
                                    act.sPropertyValue = "%PopUpChoice[""Please select player gender"", ""Male"", ""Female""]%";
                                    tasPrompt.arlActions.Add(act);
                                }
                                if (bPromptName)
                                {
                                    private New clsAction act;
                                    act.eItem = clsAction.ItemEnum.SetProperties;
                                    act.sKey1 = Player.Key;
                                    act.sKey2 = "CharacterProperName";
                                    act.sPropertyValue = "%PopUpInput[""Please enter your name"", ""Anonymous""]%";
                                    tasPrompt.arlActions.Add(act);
                                }
                            }
                            Adventure.htblTasks.Add(tasPrompt, tasPrompt.Key);
                        }
                        .MaxSize = CInt(GetLine(bAdventure, iPos));

                        if (Adventure.htblCharacterProperties.ContainsKey("MaxBulk"))
                        {
                            p = New clsProperty
                            p = Adventure.htblCharacterProperties("MaxBulk").Copy
                            p.Selected = true;
                            private int iSize2 = .MaxSize % 10;
                            p.Value = (CInt((.MaxSize - iSize2) / 10) * (3 ^ iSize2)).ToString;
                            .AddProperty(p);
                        }

                        .MaxWeight = CInt(GetLine(bAdventure, iPos));

                        if (Adventure.htblCharacterProperties.ContainsKey("MaxWeight"))
                        {
                            p = New clsProperty
                            p = Adventure.htblCharacterProperties("MaxWeight").Copy
                            p.Selected = true;
                            private int iWeight2 = .MaxWeight % 10;
                            p.Value = (CInt((.MaxWeight - iWeight2) / 10) * (3 ^ iWeight2)).ToString;
                            .AddProperty(p);
                        }


                        '.Location = "Location" & iStartLocation + 1

                        if (iOnWhat = 0)
                        {
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                            .Location.Key = "Location" + iStartLocation + 1;
                        Else
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject;
                            .Location.Key = iOnWhat.ToString ' Will adjust this below;
                        }


                        .Location = .Location;

                        if (bBattleSystem)
                        {
                            GetLine(bAdventure, iPos) ' Min Stamina;
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Max Stamina
                            GetLine(bAdventure, iPos) ' Min Strength;
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Max Strength
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Min Accuracy
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Max Accuracy
                            GetLine(bAdventure, iPos) ' Min Defence;
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Max Defence
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Min Agility
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Max Agility
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Recovery
                        }
                    }
                    .htblCharacters.Add(Player, Player.Key);

                    'Adventure.iCompassPoints = CType(8 + 4 * CInt(GetLine(bAdventure, iPos)), DirectionsEnum)
                    iCompassPoints = CType(8 + 4 * CInt(GetLine(bAdventure, iPos)), DirectionsEnum) ' CInt(GetLine(bAdventure, iPos))
                    Adventure.Enabled(clsAdventure.EnabledOptionEnum.Debugger) = ! CBool(GetLine(bAdventure, iPos)) '?;
                    Adventure.Enabled(clsAdventure.EnabledOptionEnum.Score) = ! CBool(GetLine(bAdventure, iPos)) '?;
                    Adventure.Enabled(clsAdventure.EnabledOptionEnum.Map) = ! CBool(GetLine(bAdventure, iPos));
                    Adventure.Enabled(clsAdventure.EnabledOptionEnum.AutoComplete) = ! CBool(GetLine(bAdventure, iPos)) '?;
                    Adventure.Enabled(clsAdventure.EnabledOptionEnum.ControlPanel) = ! CBool(GetLine(bAdventure, iPos)) '?;
                    Adventure.EnableMenu = ! CBool(GetLine(bAdventure, iPos)) '?;
                    bSound = CBool(GetLine(bAdventure, iPos))
                    bGraphics = CBool(GetLine(bAdventure, iPos))
                    for (int i = 0; i <= 1; i++)
                    {
                        if (bSound)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            if (sFilename <> "")
                            {
                                private string sLoop = "";
                                if (sFilename.EndsWith("##"))
                                {
                                    sLoop = " loop=Y"
                                    sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                }
                                If i = 0 Then Adventure.Introduction(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + Adventure.Introduction(0).Description
                                If i = 1 Then Adventure.WinningText(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + Adventure.WinningText(0).Description
                            }
                            If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        if (bGraphics)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            if (sFilename <> "")
                            {
                                If i = 0 Then Adventure.Introduction(0).Description = "<img src=""" + sFilename + """>" + Adventure.Introduction(0).Description
                                If i = 1 Then Adventure.WinningText(0).Description = "<img src=""" + sFilename + """>" + Adventure.WinningText(0).Description
                            }
                            If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                        }
                    Next;
                    if (v >= 4)
                    {
                        GetLine(bAdventure, iPos) ' Enable Panel;
                        Adventure.sUserStatus = GetLine(bAdventure, iPos) ' Panel Text;
                    }
                    GetLine(bAdventure, iPos) ' Size ratio;
                    GetLine(bAdventure, iPos) ' Weight ratio;
                Else
                    .ShowFirstRoom = false;
                }

                If v >= 4 Then GetLine(bAdventure, iPos) ' ?


                '----------------------------------------------------------------------------------
                ' Locations
                '----------------------------------------------------------------------------------


                private int iNumLocations = CInt(GetLine(bAdventure, iPos));
                private int iX = 3;
                If v < 4 Then iX = 2
                Dim iLocations(iNumLocations, 11, iX) As Integer ' Temp Store
                private int iLoc;
                private New Collection colNewLocs;
                For iLoc = 1 To iNumLocations
                    private New clsLocation Location;
                    With Location;
                        '.Key = "Location" & iLoc.ToString
                        private string sKey = "Location" + iLoc.ToString;
                        if (a.htblLocations.ContainsKey(sKey))
                        {
                            while (a.htblLocations.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        colNewLocs.Add(sKey);
                        .ShortDescription = New Description(GetLine(bAdventure, iPos));
                        .LongDescription = New Description(ConvText(GetLine(bAdventure, iPos)));
                        if (v < 4)
                        {
                            private string srdesc1 = GetLine(bAdventure, iPos) '?;
                            if (srdesc1 <> "")
                            {
                                private New SingleDescription sd;
                                sd.Description = ConvText(srdesc1);
                                sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                                sd.Restrictions.BracketSequence = "#";
                                .LongDescription.Add(sd);
                            }
                        }
                        for (int i = 0; i <= iCompassPoints - 1; i++)
                        {
                            iLocations(iLoc, i, 0) = SafeInt(GetLine(bAdventure, iPos)) ' Rooms;
                            if (iLocations(iLoc, i, 0) <> 0)
                            {
                                iLocations(iLoc, i, 1) = SafeInt(GetLine(bAdventure, iPos)) ' Tasks;
                                iLocations(iLoc, i, 2) = SafeInt(GetLine(bAdventure, iPos)) ' Completed;
                                If v >= 4 Then iLocations(iLoc, i, 3) = SafeInt(GetLine(bAdventure, iPos)) ' Mode
                            }
                        Next;
                        if (v < 4)
                        {
                            private string srdesc2_0 = GetLine(bAdventure, iPos) '?;
                            private int irtaskno2_0 = SafeInt(GetLine(bAdventure, iPos));
                            if (srdesc2_0 <> "" || irtaskno2_0 > 0)
                            {
                                private New SingleDescription sd;
                                sd.Description = ConvText(srdesc2_0);
                                sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                                if (irtaskno2_0 > 0)
                                {
                                    private New clsRestriction rest;
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    rest.sKey1 = "Task" + irtaskno2_0;
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eTask = clsRestriction.TaskEnum.Complete;
                                    sd.Restrictions.Add(rest);
                                }
                                sd.Restrictions.BracketSequence = "#";
                                .LongDescription.Add(sd);
                            }
                            private string srdesc2_1 = GetLine(bAdventure, iPos) '?;
                            private int irtaskno2_1 = SafeInt(GetLine(bAdventure, iPos));
                            if (srdesc2_1 <> "" || irtaskno2_1 > 0)
                            {
                                private New SingleDescription sd;
                                sd.Description = ConvText(srdesc2_1);
                                sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartAfterDefaultDescription;
                                if (irtaskno2_1 > 0)
                                {
                                    private New clsRestriction rest;
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    rest.sKey1 = "Task" + irtaskno2_1;
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eTask = clsRestriction.TaskEnum.Complete;
                                    sd.Restrictions.Add(rest);
                                }
                                sd.Restrictions.BracketSequence = "#";
                                .LongDescription.Add(sd);
                            }
                            private int irobject = SafeInt(GetLine(bAdventure, iPos));
                            private string srdesc3 = GetLine(bAdventure, iPos) '?;
                            private int irhideob = SafeInt(GetLine(bAdventure, iPos));
                            if (srdesc3 <> "")
                            {
                                private New SingleDescription sd;
                                sd.Description = ConvText(srdesc3);
                                sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                                private New clsRestriction rest;
                                rest.eType = clsRestriction.RestrictionTypeEnum.Character;
                                rest.sKey1 = "Player";
                                switch (CInt(irhideob / 10))
                                {
                                    case 0:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.MustNot;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                    case 1:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.Must;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                    case 2:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.MustNot;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject;
                                    case 3:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.Must;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject;
                                    case 4:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.MustNot;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                                    case 5:
                                        {
                                        rest.eMust = clsRestriction.MustEnum.Must;
                                        rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                                }
                                rest.sKey2 = irobject.ToString ' Needs to be converted once we've loaded objects;
                                sd.Restrictions.Add(rest);
                                sd.Restrictions.BracketSequence = "#";
                                .LongDescription.Add(sd);
                            }
                        }
                        if (bSound)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            if (sFilename <> "")
                            {
                                private string sLoop = "";
                                if (sFilename.EndsWith("##"))
                                {
                                    sLoop = " loop=Y"
                                    sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                }
                                .LongDescription(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .LongDescription(0).Description;
                            }
                            If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        if (bGraphics)
                        {
                            sFilename = GetLine(bAdventure, iPos) ' Filename
                            if (sFilename <> "")
                            {
                                .LongDescription(0).Description = "<img src=""" + sFilename + """>" + .LongDescription(0).Description;
                            }
                            If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                            If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                        }
                        private int iNumAltDesc;
                        if (v >= 4)
                        {
                            iNumAltDesc = CInt(GetLine(bAdventure, iPos))
                        Else
                            iNumAltDesc = 4
                        }
                        for (int iAlt = 0; iAlt <= iNumAltDesc - 1; iAlt++)
                        {
                            private clsRestriction rest = null;
                            private SingleDescription sd = null;
                            private int iTaskObPlayer;
                            if (v >= 4)
                            {
                                sd = New SingleDescription

                                sd.Description = ConvText(GetLine(bAdventure, iPos)) ' Description;
                                rest = New clsRestriction
                                iTaskObPlayer = CInt(GetLine(bAdventure, iPos)) ' Options
                                switch (iTaskObPlayer)
                                {
                                    case 0 ' Task:
                                        {
                                        rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    case 1 ' Object:
                                        {
                                        rest.eType = clsRestriction.RestrictionTypeEnum.Object;
                                    case 2 ' Player:
                                        {
                                        rest.eType = clsRestriction.RestrictionTypeEnum.Character;
                                        rest.sKey1 = "%Player%";
                                }
                            }
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    private string sLoop = "";
                                    if (sFilename.EndsWith("##"))
                                    {
                                        sLoop = " loop=Y"
                                        sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                    }
                                    if (v < 4)
                                    {
                                        .LongDescription(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .LongDescription(0).Description;
                                    Else
                                        sd.Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + sd.Description;
                                    }
                                }
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    if (v < 4)
                                    {
                                        .LongDescription(0).Description = "<img src=""" + sFilename + """>" + .LongDescription(0).Description;
                                    Else
                                        sd.Description = "<img src=""" + sFilename + """>" + sd.Description;
                                    }
                                }
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }

                            if (v >= 4)
                            {
                                rest.oMessage = New Description(ConvText(GetLine(bAdventure, iPos))) ' Description;
                                iTaskObPlayer = CInt(GetLine(bAdventure, iPos)) ' Options
                                switch (rest.eType)
                                {
                                    case clsRestriction.RestrictionTypeEnum.Task:
                                        {
                                        rest.sKey1 = "Task" + iTaskObPlayer;
                                    case clsRestriction.RestrictionTypeEnum.Object:
                                        {
                                        rest.sKey1 = "Object" + iTaskObPlayer;
                                        rest.eMust = clsRestriction.MustEnum.Must;
                                        rest.eObject = clsRestriction.ObjectEnum.BeInState;
                                    case clsRestriction.RestrictionTypeEnum.Character:
                                        {
                                        switch (iTaskObPlayer)
                                        {
                                            case 0 ' is not holding:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                                rest.eMust = clsRestriction.MustEnum.MustNot;
                                            case 1 ' is holding:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                                rest.eMust = clsRestriction.MustEnum.Must;
                                            case 2 ' is not wearing:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeWearingObject;
                                                rest.eMust = clsRestriction.MustEnum.MustNot;
                                            case 3 ' is wearing:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeHoldingObject;
                                                rest.eMust = clsRestriction.MustEnum.Must;
                                            case 4 ' is not same room as:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                                                rest.eMust = clsRestriction.MustEnum.MustNot;
                                            case 5 ' is in same room as:
                                                {
                                                rest.eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsObject;
                                                rest.eMust = clsRestriction.MustEnum.Must;
                                        }
                                }
                                if (bSound)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    private string sLoop = "";
                                    if (sFilename.EndsWith("##"))
                                    {
                                        sLoop = " loop=Y"
                                        sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                    }
                                    If sFilename <> "" Then sd.Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + sd.Description
                                    iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                                }
                                if (bGraphics)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    If sFilename <> "" Then sd.Description = "<img src=""" + sFilename + """>" + sd.Description
                                    iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                                }
                                GetLine(bAdventure, iPos) ' Hideobs;
                                private string sNewShort = GetLine(bAdventure, iPos) ' new Short Desc;
                                iTaskObPlayer = CInt(GetLine(bAdventure, iPos)) ' Options
                                switch (rest.eType)
                                {
                                    case clsRestriction.RestrictionTypeEnum.Task:
                                        {
                                        If iTaskObPlayer = 0 Then rest.eMust = clsRestriction.MustEnum.Must Else rest.eMust = clsRestriction.MustEnum.MustNot
                                        rest.eTask = clsRestriction.TaskEnum.Complete;
                                    case clsRestriction.RestrictionTypeEnum.Object:
                                        {
                                        rest.sKey2 = iTaskObPlayer.ToString;
                                    case clsRestriction.RestrictionTypeEnum.Character:
                                        {
                                        rest.sKey2 = iTaskObPlayer.ToString;
                                }
                                private int iDisplayWhen = CInt(GetLine(bAdventure, iPos));
                                sd.eDisplayWhen = (SingleDescription.DisplayWhenEnum)(iDisplayWhen) ' Show When;
                                If ! (rest.eType = clsRestriction.RestrictionTypeEnum.Task && rest.sKey1 = "Task0") Then sd.Restrictions.Add(rest)
                                sd.Restrictions.BracketSequence = "#";
                                .LongDescription.Add(sd);
                                if (sNewShort <> "")
                                {
                                    private SingleDescription sdShort = sd.CloneMe;
                                    sdShort.Description = sNewShort;
                                    sdShort.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                                    .ShortDescription.Add(sdShort);
                                }
                            }
                        Next;
                        If Adventure.Enabled(clsAdventure.EnabledOptionEnum.Map) Then .HideOnMap = CBool(SafeInt(GetLine(bAdventure, iPos))) ' No Map
                    }
                    .htblLocations.Add(Location, Location.Key);
NextLoc:;
                Next;
                'Dim h As New Hashtable
                'h.Add("1", "3")
                'For Each x As System.Collections.DictionaryEntry In h
                '    Dim y As String = CStr(x.Value)
                '    Debug.WriteLine(x)
                'Next

                'iLoc = 0
                '                For Each loc As clsLocation In .htblLocations.Values


                '----------------------------------------------------------------------------------
                ' Objects
                '----------------------------------------------------------------------------------


                private New Dictionary<string, string> dictDodgyStates;
                private int iNumObjects = CInt(GetLine(bAdventure, iPos));
                private New Collection colNewObs;
                for (int iObj = 1; iObj <= iNumObjects; iObj++)
                {
                    private New clsObject NewObject;
                    With NewObject;
                        '.Key = "Object" & iObj.ToString
                        private string sKey = "Object" + iObj.ToString;
                        if (a.htblObjects.ContainsKey(sKey))
                        {
                            while (a.htblObjects.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        colNewObs.Add(sKey);
                        .Prefix = GetLine(bAdventure, iPos);
                        ConvertPrefix(.Article, .Prefix);
                        .arlNames.Add(GetLine(bAdventure, iPos));
                        if (v < 4)
                        {
                            private string sAlias = GetLine(bAdventure, iPos);
                            If sAlias <> "" Then .arlNames.Add(sAlias)
                        Else
                            private int iNumAliases = CInt(GetLine(bAdventure, iPos));
                            for (int iAlias = 1; iAlias <= iNumAliases; iAlias++)
                            {
                                .arlNames.Add(GetLine(bAdventure, iPos));
                            Next;
                        }

                        private New clsProperty sod;
                        sod = Adventure.htblAllProperties("StaticOrDynamic").Copy
                        '.htblActualProperties.Add(sod)
                        '.bCalculatedGroups = False
                        .AddProperty(sod);
                        .IsStatic = SafeBool(GetLine(bAdventure, iPos));

                        .Description = New Description(ConvText(GetLine(bAdventure, iPos)));
                        private New clsObjectLocation cObjectLocation;
                        if (.IsStatic)
                        {
                            GetLine(bAdventure, iPos) ' ! needed here?;
                            private New clsProperty sl;
                            sl = Adventure.htblAllProperties("StaticLocation").Copy
                            .AddProperty(sl);
                            '.htblActualProperties.Add(sl)
                            '.bCalculatedGroups = False
                        Else
                            private New clsProperty dl;
                            dl = Adventure.htblAllProperties("DynamicLocation").Copy
                            '.htblActualProperties.Add(dl)
                            '.bCalculatedGroups = False
                            .AddProperty(dl);

                            private int iv4Loc = CInt(GetLine(bAdventure, iPos));
                            If v < 3.9 && iv4Loc > 2 Then iv4Loc -= 1

                            switch (iv4Loc)
                            {
                                case 0:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.Hidden;
                                case 1:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter;
                                case 2:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InObject;
                                case 3:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.OnObject;
                                case iNumLocations + 4:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter;
                                default:
                                    {
                                    cObjectLocation.DynamicExistWhere = clsObjectLocation.DynamicExistsWhereEnum.InLocation;
                                    cObjectLocation.Key = "Location" + iv4Loc - 3 + iStartLocations;
                                    private New clsProperty p;
                                    p = Adventure.htblAllProperties("InLocation").Copy
                                    '.htblActualProperties.Add(p)
                                    '.bCalculatedGroups = False
                                    .AddProperty(p);
                            }
                        }

                        private string sTaskKey = "Task" + GetLine(bAdventure, iPos);
                        private bool bTaskState = CBool(GetLine(bAdventure, iPos));
                        private string sDescription = ConvText(GetLine(bAdventure, iPos));
                        if (sTaskKey <> "Task0")
                        {
                            private New SingleDescription sd;
                            sd.Description = sDescription;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.eMust = (clsRestriction.MustEnum.MustNot, clsRestriction.MustEnum.Must)(IIf(bTaskState), clsRestriction.MustEnum);
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            rest.sKey1 = sTaskKey;
                            sd.Restrictions.Add(rest);
                            sd.Restrictions.BracketSequence = "#";
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            .Description.Add(sd);
                        }

                        'Dim AltDesc As New clsObject.AlternativeDescription
                        'With AltDesc
                        '    .sTaskKey = "Task" & GetLine(bAdventure, iPos)
                        '    .bTaskState = CBool(GetLine(bAdventure, iPos))
                        '    .sDescription = GetLine(bAdventure, iPos)
                        'End With
                        '.colAlternativeDescriptions.Add(AltDesc)

                        private New clsObjectLocation StaticLoc;
                        if (.IsStatic)
                        {
                            StaticLoc.StaticExistWhere = (iPos)(GetLine(bAdventure), clsObjectLocation.StaticExistsWhereEnum);
                            '.Location = StaticLoc

                            switch (StaticLoc.StaticExistWhere)
                            {
                                case clsObjectLocation.StaticExistsWhereEnum.NoRooms:
                                    {

                                case clsObjectLocation.StaticExistsWhereEnum.SingleLocation:
                                    {
                                    StaticLoc.Key = "Location" + GetLine(bAdventure, iPos) ' StaticKey;
                                    private New clsProperty pLoc;
                                    pLoc = Adventure.htblAllProperties("AtLocation").Copy
                                    '.htblActualProperties.Add(pLoc)
                                    '.bCalculatedGroups = False
                                    .AddProperty(pLoc);
                                case clsObjectLocation.StaticExistsWhereEnum.LocationGroup:
                                    {
                                    private New StringArrayList salRooms;
                                    for (int iRoom = 0; iRoom <= iNumLocations; iRoom++)
                                    {
                                        If CBool(GetLine(bAdventure, iPos)) Then salRooms.Add("Location" + iRoom) ' StaticLoc.StaticKey = "Location" + iRoom ' TODO - Generate a roomgroup and assign that
                                    Next;
                                    StaticLoc.Key = GetRoomGroupFromList(iPos, salRooms, "object '" + .FullName + "'").Key ' StaticKey;
                                    private New clsProperty pLG;
                                    pLG = Adventure.htblAllProperties("AtLocationGroup").Copy
                                    '.htblActualProperties.Add(pLG)
                                    '.bCalculatedGroups = False
                                    .AddProperty(pLG);
                                case clsObjectLocation.StaticExistsWhereEnum.AllRooms:
                                    {

                                case clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter:
                                    {
                                    ' Key defined later
                                case clsObjectLocation.StaticExistsWhereEnum.PartOfObject:
                                    {
                                    ' Key defined later
                            }
                        }


                        If CBool(GetLine(bAdventure, iPos)) Then ' container
                            private New clsProperty c;
                            c = Adventure.htblAllProperties("Container").Copy
                            c.Selected = true;
                            '.htblActualProperties.Add(c)
                            '.bCalculatedGroups = False
                            .AddProperty(c);
                        }
                        If CBool(GetLine(bAdventure, iPos)) Then ' surface
                            private New clsProperty s;
                            s = Adventure.htblAllProperties("Surface").Copy
                            s.Selected = true;
                            '.htblActualProperties.Add(s)
                            '.bCalculatedGroups = False
                            .AddProperty(s);
                        }
                        private int iCapacity = CInt(GetLine(bAdventure, iPos)) ' Num Holds;
                        If v < 3.9 Then iCapacity = iCapacity * 100 + 2
                        if (.IsContainer)
                        {
                            private int iCapacity2 = iCapacity % 10;
                            iCapacity -= iCapacity2;
                            iCapacity = CInt((iCapacity / 10) * (3 ^ iCapacity2))
                            if (Adventure.htblObjectProperties.ContainsKey("Capacity"))
                            {
                                private New clsProperty p;
                                p = Adventure.htblObjectProperties("Capacity").Copy
                                p.Selected = true;
                                p.Value = iCapacity.ToString;
                                .AddProperty(p);
                            }
                        }

                        if (Not .IsStatic)
                        {
                            If CBool(GetLine(bAdventure, iPos)) Then  ' wearable
                                private New clsProperty w;
                                w = Adventure.htblAllProperties("Wearable").Copy
                                w.Selected = true;
                                '.htblActualProperties.Add(w)
                                '.bCalculatedGroups = False
                                .AddProperty(w);
                            }
                            private int iSize = CInt(GetLine(bAdventure, iPos)) ' weight;
                            private int iWeight = iSize % 10;
                            if (v < 3.9)
                            {
                                switch (iSize)
                                {
                                    case 0:
                                        {
                                        iWeight = 22
                                    case 1:
                                        {
                                        iWeight = 23
                                    case 2:
                                        {
                                        iWeight = 24
                                    case 3:
                                        {
                                        iWeight = 32
                                    case 4:
                                        {
                                        iWeight = 42
                                }
                            Else
                                iSize -= iWeight;
                            }

                            private New clsProperty p;
                            if (Adventure.htblObjectProperties.ContainsKey("Size"))
                            {
                                p = Adventure.htblObjectProperties("Size").Copy
                                p.Selected = true;
                                p.Value = (3 ^ (iSize / 10)).ToString;
                                .AddProperty(p);
                            }

                            if (Adventure.htblObjectProperties.ContainsKey("Weight"))
                            {
                                p = New clsProperty
                                p = Adventure.htblObjectProperties("Weight").Copy
                                p.Selected = true;
                                p.Value = (3 ^ iWeight).ToString;
                                .AddProperty(p);
                            }

                            private int iParent = CInt(GetLine(bAdventure, iPos));
                            switch (cObjectLocation.DynamicExistWhere)
                            {
                                case clsObjectLocation.DynamicExistsWhereEnum.HeldByCharacter:
                                    {
                                    p = New clsProperty
                                    p = Adventure.htblAllProperties("HeldByWho").Copy
                                    '.htblActualProperties.Add(p)
                                    '.bCalculatedGroups = False
                                    .AddProperty(p);

                                    if (iParent = 0)
                                    {
                                        cObjectLocation.Key = "%Player%";
                                    Else
                                        cObjectLocation.Key = "Character" + iParent;
                                    }
                                    .Location = cObjectLocation;
                                case clsObjectLocation.DynamicExistsWhereEnum.WornByCharacter:
                                    {
                                    if (iParent = 0)
                                    {
                                        cObjectLocation.Key = "%Player%";
                                    Else
                                        cObjectLocation.Key = "Character" + iParent;
                                    }
                                    p = New clsProperty
                                    p = Adventure.htblAllProperties("WornByWho").Copy
                                    '.htblActualProperties.Add(p)
                                    '.bCalculatedGroups = False
                                    .AddProperty(p);
                                    p.Value = cObjectLocation.Key;
                                    .Location = cObjectLocation;
                                case clsObjectLocation.DynamicExistsWhereEnum.InObject:
                                    {
                                    .Location = cObjectLocation;
                                    p = New clsProperty
                                    p = Adventure.htblAllProperties("InsideWhat").Copy
                                    '.htblActualProperties.Add(p)
                                    '.bCalculatedGroups = False
                                    .AddProperty(p);
                                    p.Value = iParent.ToString;
                                case clsObjectLocation.DynamicExistsWhereEnum.OnObject:
                                    {
                                    .Location = cObjectLocation;
                                    p = New clsProperty
                                    p = Adventure.htblAllProperties("OnWhat").Copy
                                    '.htblActualProperties.Add(p)
                                    '.bCalculatedGroups = False
                                    .AddProperty(p);
                                    p.Value = iParent.ToString;
                                default:
                                    {
                                    .Location = cObjectLocation;
                            }
                            '.Location = cObjectLocation
                            '.Move(cObjectLocation)
                        }
                        if (.IsStatic && StaticLoc.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.PartOfCharacter)
                        {
                            private int iChar = CInt(GetLine(bAdventure, iPos));
                            if (iChar = 0)
                            {
                                StaticLoc.Key = "%Player%" ' StaticKey;
                            Else
                                StaticLoc.Key = "Character" + iChar ' StaticKey;
                            }
                            private New clsProperty c;
                            c = Adventure.htblAllProperties("PartOfWho").Copy
                            c.StringData = New Description(ConvText(StaticLoc.Key)) ' StaticKey;
                            '.htblActualProperties.Add(c)
                            '.bCalculatedGroups = False
                            .AddProperty(c);
                        }
                        If .IsStatic Then .Move(StaticLoc)

                        private int iOpenableLockable = CInt(GetLine(bAdventure, iPos));
                        If v < 4 && iOpenableLockable > 1 Then iOpenableLockable = 11 - iOpenableLockable
                        ' 0 = Not openable
                        ' 5 = Openable, open
                        ' 6 = Openable, closed
                        ' 7 = Openable, locked
                        if (iOpenableLockable > 0)
                        {
                            private New clsProperty op;
                            op = Adventure.htblAllProperties("Openable").Copy
                            op.Selected = true;
                            '.htblActualProperties.Add(op)
                            '.bCalculatedGroups = False
                            .AddProperty(op);

                            private New clsProperty pOS;
                            pOS = Adventure.htblAllProperties("OpenStatus").Copy
                            pOS.Selected = true;
                            if (iOpenableLockable = 5)
                            {
                                pOS.Value = "Open";
                            Else
                                pOS.Value = "Closed";
                            }
                            '.htblActualProperties.Add(pOS)
                            '.bCalculatedGroups = False
                            .AddProperty(pOS);

                            'End If
                            'If iOpenableLockable > 1 Then
                            if (v >= 4)
                            {
                                private int iKey = CInt(GetLine(bAdventure, iPos));
                                if (iKey > -1)
                                {
                                    private New clsProperty pLk;
                                    pLk = Adventure.htblAllProperties("Lockable").Copy
                                    pLk.Selected = true;
                                    '.htblActualProperties.Add(pLk)
                                    '.bCalculatedGroups = False
                                    .AddProperty(pLk);

                                    private New clsProperty pKey;
                                    pKey = Adventure.htblAllProperties("LockKey").Copy
                                    pKey.Selected = true;
                                    pKey.Value = CStr(iKey) ' "Object" + iKey + iStartObs;
                                    '.htblActualProperties.Add(pKey)
                                    '.bCalculatedGroups = False
                                    .AddProperty(pKey);

                                    private New clsProperty pLS;
                                    pLS = Adventure.htblAllProperties("LockStatus").Copy
                                    pLS.Selected = true;
                                    If iOpenableLockable = 7 Then pOS.Value = "Locked"
                                    '.htblActualProperties.Add(pLS)
                                    '.bCalculatedGroups = False
                                    .AddProperty(pLS);
                                }
                            }
                        }

                        '.Openable = CBool(GetLine(bAdventure, iPos))
                        'If .Lockable Then GetLine(bAdventure, iPos) ' key
                        private int iSitStandLie = CInt(GetLine(bAdventure, iPos))  ' Sittable;
                        If iSitStandLie = 1 || iSitStandLie = 3 Then .IsSittable = true
                        .IsStandable = .IsSittable;
                        If iSitStandLie = 2 || iSitStandLie = 3 Then .IsLieable = true
                        If ! .IsStatic Then GetLine(bAdventure, iPos) ' edible

                        if (CBool(GetLine(bAdventure, iPos)))
                        {
                            private New clsProperty r;
                            r = Adventure.htblAllProperties("Readable").Copy
                            r.Selected = true;
                            '.htblActualProperties.Add(r)
                            '.bCalculatedGroups = False
                            .AddProperty(r);
                        }
                        if (.Readable)
                        {
                            private string sReadText = GetLine(bAdventure, iPos);
                            if (sReadText <> "")
                            {
                                private New clsProperty r;
                                r = Adventure.htblAllProperties("ReadText").Copy
                                r.Selected = true;
                                '.htblActualProperties.Add(r)
                                '.bCalculatedGroups = False
                                .AddProperty(r);
                                .ReadText = sReadText;
                            }
                        }

                        If ! .IsStatic Then GetLine(bAdventure, iPos) ' weapon

                        if (v >= 4)
                        {
                            private int iState = CInt(GetLine(bAdventure, iPos));
                            if (iState > 0)
                            {
                                private string sStates = GetLine(bAdventure, iPos);
                                private New StringArrayList arlStates;
                                For Each sState As String In sStates.Split("|"c)
                                    arlStates.Add(ToProper(sState));
                                Next;
                                private string sPKey = FindProperty(arlStates);
                                private New clsProperty s;
                                if (sPKey Is null)
                                {
                                    s.Type = clsProperty.PropertyTypeEnum.StateList;
                                    s.Description = "Object can be " + sStates.Replace("|", " or ");
                                    s.Key = sStates '.Replace("|", "");
                                    s.arlStates = arlStates;
                                    s.Value = arlStates(iState - 1);
                                    s.PropertyOf = clsProperty.PropertyOfEnum.Objects;
                                    Adventure.htblAllProperties.Add(s.Copy);
                                Else
                                    s = Adventure.htblAllProperties(sPKey).Copy
                                    s.Value = arlStates(iState - 1);
                                    if (sStates <> sPKey)
                                    {
                                        ' Hmm, the states are not in the same order as before
                                        ' This can cause problems if restrictions/actions use the state index
                                        dictDodgyStates.Add(.Key, sStates);
                                    }
                                    sStates = sPKey
                                }
                                s.Selected = true;
                                '.htblActualProperties.Add(s)
                                '.bCalculatedGroups = False
                                .AddProperty(s);
                                salWithStates.Add(.Key);

                                private bool bShowState = CBool(GetLine(bAdventure, iPos)) ' showstate;
                                if (bShowState)
                                {
                                    .Description(0).Description &= "  " + .Key + ".Name is %LCase[" + .Key + "." + sStates + "]%.";
                                }
                            }
                            private bool bSpecificallyList = CBool(GetLine(bAdventure, iPos)) ' showhide;
                            if (.IsStatic)
                            {
                                .ExplicitlyList = bSpecificallyList;
                            Else
                                .ExplicitlyExclude = bSpecificallyList;
                            }
                        }
                        ' GSFX
                        for (int i = 0; i <= 1; i++)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    private string sLoop = "";
                                    if (sFilename.EndsWith("##"))
                                    {
                                        sLoop = " loop=Y"
                                        sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                    }
                                    .Description(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .Description(0).Description;
                                }
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                if (sFilename <> "")
                                {
                                    .Description(0).Description = "<img src=""" + sFilename + """>" + .Description(0).Description;
                                }
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }
                        Next;
                        ' Battle
                        if (bBattleSystem)
                        {
                            GetLine(bAdventure, iPos) ' Armour;
                            GetLine(bAdventure, iPos) ' Hit Points;
                            GetLine(bAdventure, iPos) ' Hit Method;
                            If v >= 4 Then GetLine(bAdventure, iPos) ' Accuracy
                        }
                        if (v >= 4)
                        {
                            private string sSpecialList = GetLine(bAdventure, iPos) ' alsohere;
                            if (sSpecialList <> "")
                            {
                                private New clsProperty r;
                                if (NewObject.IsStatic)
                                {
                                    r = Adventure.htblAllProperties("ListDescription").Copy
                                Else
                                    r = Adventure.htblAllProperties("ListDescriptionDynamic").Copy
                                }
                                r.Selected = true;
                                '.htblActualProperties.Add(r)
                                '.bCalculatedGroups = False
                                .AddProperty(r);
                                .ListDescription = sSpecialList;
                            }
                            private string s2 = GetLine(bAdventure, iPos) ' initial;
                        }
                        '.htblProperties = .GetPropertiesIncludingGroups()
                    }

                    .htblObjects.Add(NewObject, NewObject.Key);
                Next;

                ' Sort out object keys
                if (Adventure.Player.Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.OnObject)
                {
                    switch (Adventure.Player.Location.Position)
                    {
                        case clsCharacterLocation.PositionEnum.Standing:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Standable);
                        case clsCharacterLocation.PositionEnum.Sitting:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Sittable);
                        case clsCharacterLocation.PositionEnum.Lying:
                            {
                            Adventure.Player.Location.Key = GetObKey(CInt(Adventure.Player.Location.Key) - 1, ComboEnum.Lieable);
                    }
                }
                For Each sLoc As String In colNewLocs
                    private clsLocation loc = a.htblLocations(sLoc);
                    private New List<SingleDescription> listDescriptions;

                    For Each sd As SingleDescription In loc.ShortDescription
                        listDescriptions.Add(sd);
                    Next;
                    For Each sd As SingleDescription In loc.LongDescription
                        listDescriptions.Add(sd);
                    Next;

                    For Each sd As SingleDescription In listDescriptions
                        if (sd.Restrictions.Count > 0 && IsNumeric(sd.Restrictions(0).sKey2))
                        {
                            if (sd.Restrictions(0).eType = clsRestriction.RestrictionTypeEnum.Character)
                            {
                                switch (sd.Restrictions(0).eCharacter)
                                {
                                    case clsRestriction.CharacterEnum.BeInSameLocationAsObject:
                                    case clsRestriction.CharacterEnum.BeHoldingObject:
                                        {
                                        sd.Restrictions(0).sKey2 = GetObKey(CInt(sd.Restrictions(0).sKey2) - 1, ComboEnum.Dynamic);
                                    case clsRestriction.CharacterEnum.BeWearingObject:
                                        {
                                        sd.Restrictions(0).sKey2 = GetObKey(CInt(sd.Restrictions(0).sKey2) - 1, ComboEnum.Wearable);
                                }
                            ElseIf sd.Restrictions(0).eType = clsRestriction.RestrictionTypeEnum.Object Then
                                private clsObject ob = null;
                                private string sKey1 = sd.Restrictions(0).sKey1;
                                private string sKey2 = sd.Restrictions(0).sKey2;
                                If sKey1 <> "ReferencedObject" Then ob = Adventure.htblObjects(sKey1)

                                switch (CInt(sd.Restrictions(0).sKey2))
                                {
                                    case 1:
                                        {
                                        if (sKey1 = "ReferencedObject" || ob.Openable)
                                        {
                                            sKey2 = "Open"
                                        Else
                                            For Each prop As clsProperty In ob.htblActualProperties.Values
                                                if (prop.Key.IndexOf("|"c) > 0)
                                                {
                                                    sKey2 = prop.arlStates(CInt(sd.Restrictions(0).sKey2) - 1)
                                                }
                                            Next;
                                        }
                                    case 2:
                                        {
                                        if (sKey1 = "ReferencedObject" || ob.Openable)
                                        {
                                            sKey2 = "Closed"
                                        Else
                                            For Each prop As clsProperty In ob.htblActualProperties.Values
                                                if (prop.Key.IndexOf("|"c) > 0)
                                                {
                                                    sKey2 = prop.arlStates(CInt(sd.Restrictions(0).sKey2) - 1)
                                                }
                                            Next;
                                        }
                                    case 3:
                                        {
                                        if (sKey1 = "ReferencedObject" || ob.Openable)
                                        {
                                            if (sKey1 = "ReferencedObject" || ob.Lockable)
                                            {
                                                sKey2 = "Locked"
                                            Else
                                                For Each prop As clsProperty In ob.htblActualProperties.Values
                                                    if (prop.Key.IndexOf("|"c) > 0)
                                                    {
                                                        sKey2 = prop.arlStates(CInt(sd.Restrictions(0).sKey2) - 3)
                                                    }
                                                Next;
                                            }
                                        Else
                                            For Each prop As clsProperty In ob.htblActualProperties.Values
                                                if (prop.Key.IndexOf("|"c) > 0)
                                                {
                                                    sKey2 = prop.arlStates(CInt(sd.Restrictions(0).sKey2) - 1)
                                                }
                                            Next;
                                        }
                                    default:
                                        {
                                        private int iOffset = 0;
                                        if (sKey1 = "ReferencedObject" || ob.Openable)
                                        {
                                            If sKey1 = "ReferencedObject" || ob.Lockable Then iOffset = 4 Else iOffset = 3
                                        }
                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                            if (prop.Key.IndexOf("|"c) > 0)
                                            {
                                                sKey2 = prop.arlStates(CInt(sd.Restrictions(0).sKey2) - iOffset)
                                            }
                                        Next;
                                }
                                sd.Restrictions(0).sKey2 = sKey2;
                            }
                        }
                    Next;
                Next;
                For Each sOb As String In colNewObs
                    private clsObject ob = a.htblObjects(sOb);
                    if (ob.Lockable)
                    {
                        'ob.htblActualProperties("LockKey").Value = GetObKey(CInt(ob.htblActualProperties("LockKey").Value) + iStartObs, ComboEnum.Dynamic)
                        ob.SetPropertyValue("LockKey", GetObKey(CInt(ob.GetPropertyValue("LockKey")) + iStartObs, ComboEnum.Dynamic));
                        'ob.bCalculatedGroups = False
                    }
                    if (ob.HasProperty("OnWhat"))
                    {
                        'ob.htblActualProperties("OnWhat").Value = GetObKey(CInt(ob.htblActualProperties("OnWhat").Value), ComboEnum.Surface)
                        ob.SetPropertyValue("OnWhat", GetObKey(CInt(ob.GetPropertyValue("OnWhat")), ComboEnum.Surface));
                        'ob.bCalculatedGroups = False
                    }
                    if (ob.HasProperty("InsideWhat"))
                    {
                        'ob.htblActualProperties("InsideWhat").Value = GetObKey(CInt(ob.htblActualProperties("InsideWhat").Value), ComboEnum.Container)
                        ob.SetPropertyValue("InsideWhat", GetObKey(CInt(ob.GetPropertyValue("InsideWhat")), ComboEnum.Container));
                        ' ob.bCalculatedGroups = False
                    }
                Next;

                if (v < 3.9)
                {
                    'Dim parentob As Integer
                    For Each ob As clsObject In a.htblObjects.Values
                        With ob;
                            if (.Location.StaticExistWhere = clsObjectLocation.StaticExistsWhereEnum.LocationGroup)
                            {
                                '        If .Parent < 0 Then .Parent = 0
                                '        parentob = xns(.Parent, cont_surf)
                                'If object(parentob).container = 0 Then
                                '            ' change it to ON object
                                '            .room = .room + 1
                                '            .Parent = wxns(parentob, surface)
                                '        Else
                                '            .Parent = wxns(parentob, container)
                                '        End If
                            }
                        }
                    Next;
                }

                ' Sort out location restrictions
                'For iLoc = 1 To .htblLocations.Count
                iLoc = 0
                For Each sLoc As String In colNewLocs
                    iLoc += 1;
                    private clsLocation loc = Adventure.htblLocations(sLoc) '"Location" + iLoc);
                    'Dim loc As clsLocation = CType(CType(x, DictionaryEntry).Value, clsLocation)
                    for (DirectionsEnum iDir = DirectionsEnum.North; iDir <= DirectionsEnum.NorthWest; iDir++)
                    {
                        '                        For iDir As Integer = 0 To 11
                        if (iLocations(iLoc, iDir, 0) > 0)
                        {
                            if (iLocations(iLoc, iDir, 0) <= Adventure.htblLocations.Count)
                            {
                                loc.arlDirections(iDir).LocationKey = "Location" + iLocations(iLoc, iDir, 0);
                            Else
                                loc.arlDirections(iDir).LocationKey = "Group" + iLocations(iLoc, iDir, 0) - Adventure.htblLocations.Count;
                            }
                            if (iLocations(iLoc, iDir, 1) > 0)
                            {
                                private New clsRestriction rest;
                                if (v < 4 || iLocations(iLoc, iDir, 3) = 0)
                                {
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    rest.sKey1 = "Task" + iLocations(iLoc, iDir, 1) + iStartTask;
                                    if (iLocations(iLoc, iDir, 2) = 0)
                                    {
                                        rest.eMust = clsRestriction.MustEnum.Must;
                                    Else
                                        rest.eMust = clsRestriction.MustEnum.MustNot;
                                    }
                                    rest.eTask = clsRestriction.TaskEnum.Complete;
                                Else
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Property ' clsRestriction.ItemEnum.Object;
                                    ' Filter on objects with state
                                    rest.sKey1 = "OpenStatus";
                                    rest.sKey2 = GetObKey(iLocations(iLoc, iDir, 1) - 1, ComboEnum.WithStateOrOpenable);
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    'rest.eObject = clsRestriction.ObjectEnum.InState
                                    private clsObject ob = Adventure.htblObjects(rest.sKey2);
                                    if (ob.Openable)
                                    {
                                        If iLocations(iLoc, iDir, 2) = 0 Then rest.StringValue = "Open"
                                        If iLocations(iLoc, iDir, 2) = 1 Then rest.StringValue = "Closed"
                                        If ob.Lockable && iLocations(iLoc, iDir, 2) = 2 Then rest.StringValue = "Locked"
                                    }
                                    'Select Case iLocations(iLoc, iDir, 2)

                                    'End Select
                                }
                                loc.arlDirections(iDir).Restrictions.Add(rest);
                                loc.arlDirections(iDir).Restrictions.BracketSequence = "#";
                            }
                        }
                    Next;
                Next;


                '----------------------------------------------------------------------------------
                ' Tasks
                '----------------------------------------------------------------------------------



                private int iNumTasks = CInt(GetLine(bAdventure, iPos));
                'Dim colNewTasks As New Collection
                for (int iTask = 1; iTask <= iNumTasks; iTask++)
                {
                    private New clsTask NewTask;
                    With NewTask;
                        '.Key = "Task" & iTask.ToString
                        private string sKey = "Task" + iTask.ToString;
                        'Dim sOrigKey As String = sKey
                        if (a.htblTasks.ContainsKey(sKey))
                        {
                            while (a.htblTasks.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        'If sKey <> sOrigKey Then htblKeyMapping.Add(sOrigKey, sKey)
                        .Key = sKey;
                        'colNewTasks.Add(sKey)
                        .Priority = iStartMaxPriority + iTask;
                        private int iNumCommands = CInt(GetLine(bAdventure, iPos));
                        If v < 4 Then iNumCommands += 1
                        for (int i = 1; i <= iNumCommands; i++)
                        {
                            private string sCommand = GetLine(bAdventure, iPos);

#if Generator
                            .arlCommands.Add(sCommand);
#else
                            ' Simplify Runner so it only has to deal with multiple, or specific refs
                            .arlCommands.Add(sCommand.Replace("%object%", "%object1%").Replace("%character%", "%character1%"));
#endif
                        Next;
                        .TaskType = clsTask.TaskTypeEnum.System;
                        For Each sCommand As String In .arlCommands
                            if (Left(sCommand, 1) <> "#")
                            {
                                .TaskType = clsTask.TaskTypeEnum.General;
                                Exit For;
                            }
                        Next;
                        .Description = .arlCommands(0);
                        private string sMessage0 = GetLine(bAdventure, iPos);
                        .CompletionMessage = New Description(ConvText(sMessage0));
                        private string sMessage1 = GetLine(bAdventure, iPos);
                        private string sMessage2 = GetLine(bAdventure, iPos);
                        private string sMessage3 = GetLine(bAdventure, iPos);
                        private int iShowRoom = SafeInt(GetLine(bAdventure, iPos));
                        if (iShowRoom > 0)
                        {
                            If .CompletionMessage(0).Description <> "" Then .CompletionMessage(0).Description &= "  "
                            .CompletionMessage(0).Description &= "%DisplayLocation[Location" + iShowRoom + "]%";
                        }
                        If sMessage3 <> "" && .CompletionMessage.ToString = "" Then .eDisplayCompletion = clsTask.BeforeAfterEnum.After Else .eDisplayCompletion = clsTask.BeforeAfterEnum.Before
                        If .eDisplayCompletion = clsTask.BeforeAfterEnum.Before + sMessage0.Contains("%") Then .eDisplayCompletion = clsTask.BeforeAfterEnum.After ' v4 didn't handle this properly, so any text was substituted afterwards anyway
                        If sMessage3 <> "" Then .CompletionMessage(0).Description = pSpace(.CompletionMessage.ToString) + sMessage3 ' &= "  " + sMessage3
                        'If .CompletionMessage.ToString = "" Then .ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnNoOutput Else .ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnFail
                        If .CompletionMessage.ToString = "" Then .SpecificOverrideType = clsTask.SpecificOverrideTypeEnum.BeforeTextAndActions ' .ExecuteParentActions = true
                        ' Needs to be ContinueOnFail so that a failing task with output will be overridden by a lower priority succeeding task, as per v4
                        '.ContinueToExecuteLowerPriority = clsTask.ContinueEnum.ContinueOnFail ' clsTask.ContinueEnum.ContinueOnNoOutput
                        .ContinueToExecuteLowerPriority = false;
                        .Repeatable = CBool(GetLine(bAdventure, iPos));
                        if (v < 3.9)
                        {
                            GetLine(bAdventure, iPos) ' score;
                            GetLine(bAdventure, iPos) ' upto;
                            for (int i = 0; i <= 5; i++)
                            {
                                private int move_nn_0 = CInt(GetLine(bAdventure, iPos)) ' move(nn, 0);
                                private int move_nn_1 = CInt(GetLine(bAdventure, iPos)) ' move(nn, 1);
                                If v >= 3.8 Then GetLine(bAdventure, iPos) ' movemode(nn)
                            Next;
                        }
                        GetLine(bAdventure, iPos) ' Reversible - give warning if this is set;
                        iNumCommands = CInt(GetLine(bAdventure, iPos))
                        If v < 4 Then iNumCommands += 1
                        for (int i = 1; i <= iNumCommands; i++)
                        {
                            '.arlReverseCommands.Add(
                            GetLine(bAdventure, iPos);
                        Next;

                        if (v < 3.9)
                        {
                            for (int i = 0; i <= 1; i++)
                            {
                                GetLine(bAdventure, iPos) ' wear(nn);
                            Next;
                            for (int i = 0; i <= 3; i++)
                            {
                                GetLine(bAdventure, iPos) ' hold(nn);
                            Next;
                            GetLine(bAdventure, iPos) ' TaskNo;
                            GetLine(bAdventure, iPos) ' TaskDone;
                            for (int i = 0; i <= 3; i++)
                            {
                                GetLine(bAdventure, iPos) ' elses(i);
                            Next;
                            GetLine(bAdventure, iPos) ' hereornot;
                            GetLine(bAdventure, iPos) ' who;
                            GetLine(bAdventure, iPos) ' elses(4);
                            GetLine(bAdventure, iPos) ' obroom;
                        }

                        ' Convert rooms executable in into restrictions
                        ' If up to 3 rooms, add as seperate restrictions
                        ' If up to 3 away from all then add as separate restrictions
                        ' Otherwise, create a room group and have that as single restriction
                        '
                        private int iDoWhere = SafeInt(GetLine(bAdventure, iPos)) ' 0=None, 1=Single, 2=Multiple, 3=All;
                        if (iDoWhere = 1)
                        {
                            .arlRestrictions.Add(LocRestriction("Location" + CInt(GetLine(bAdventure, iPos)) + 1 + iStartLocations, true));
                            .arlRestrictions.BracketSequence = "#";
                        }
                        if (iDoWhere = 2)
                        {
                            private bool bHere;
                            private int iCount = 0;
                            private New StringArrayList salHere;
                            private New StringArrayList salNotHere;
                            for (int i = 1; i <= iNumLocations 'Adventure.htblLocations.Count; i++)
                            {
                                bHere = CBool(GetLine(bAdventure, iPos))
                                if (bHere)
                                {
                                    iCount += 1;
                                    salHere.Add("Location" + i + iStartLocations);
                                Else
                                    salNotHere.Add("Location" + i + iStartLocations);
                                }
                            Next;
                            switch (iCount)
                            {
                                case 2:
                                case 3:
                                    {
                                    For Each sLocKey As String In salHere
                                        .arlRestrictions.Add(LocRestriction(sLocKey, true));
                                    Next;
                                    if (iCount = 2)
                                    {
                                        .arlRestrictions.BracketSequence = "(#O#)";
                                    Else
                                        .arlRestrictions.BracketSequence = "(#O#O#)";
                                    }
                                case iNumLocations - 1:
                                case iNumLocations - 2:
                                    {
                                    For Each sLocKey As String In salNotHere
                                        .arlRestrictions.Add(LocRestriction(sLocKey, false));
                                    Next;
                                    if (iCount = iNumLocations - 1)
                                    {
                                        .arlRestrictions.BracketSequence = "#";
                                    Else
                                        .arlRestrictions.BracketSequence = "(#O#)";
                                    }
                                default:
                                    {
                                    .arlRestrictions.Add(LocRestriction(GetRoomGroupFromList(iPos, salHere, "task '" + .Description + "'").Key, true));
                                    .arlRestrictions.BracketSequence = "#";
                            }
                        }

                        if (v < 3.9)
                        {
                            GetLine(bAdventure, iPos) ' kills;
                            GetLine(bAdventure, iPos) ' holding;
                        }

                        private string sQuestion = GetLine(bAdventure, iPos);
                        if (sQuestion <> "")
                        {
                            private New clsHint NewHint;
                            With NewHint;
                                .Key = "Hint" + (Adventure.htblHints.Count + 1).ToString;
                                .Question = sQuestion;
                                .SubtleHint = New Description(ConvText(GetLine(bAdventure, iPos)));
                                .SledgeHammerHint = New Description(ConvText(GetLine(bAdventure, iPos)));
                            }
                            Adventure.htblHints.Add(NewHint, NewHint.Key);
                        }

                        if (v < 3.9)
                        {
                            private int iObStuff = CInt(GetLine(bAdventure, iPos));
                            if (iObStuff > 0)
                            {
                                GetLine(bAdventure, iPos) ' obstuff(1);
                                GetLine(bAdventure, iPos) ' obstuff(2);
                                GetLine(bAdventure, iPos) ' elses(5);
                            }
                            GetLine(bAdventure, iPos) ' winning;

                            '' convert task info into restrictions
                            '.restcount = 0
                            'For nn = 0 To 3
                            '    If hold(nn) > 0 Then
                            '        .restcount = .restcount + 1
                            '        ReDim Preserve .restriction(.restcount)
                            '        With .restriction(.restcount - 1)
                            '            .mode = 0
                            '            .combo(0) = hold(nn) + 1
                            '            If nn < 3 Then
                            '                If holding = 1 Then .combo(1) = 1 Else .combo(1) = 3
                            '                .combo(2) = 0
                            '                .message = elses(1)
                            '            Else
                            '                .combo(1) = 0
                            '                .combo(2) = obroom
                            '                .message = elses(4)
                            '            End If
                            '        End With
                            '    End If
                            'Next nn
                            'If taskno > 0 Then
                            '    .restcount = .restcount + 1
                            '    ReDim Preserve .restriction(.restcount)
                            '    With .restriction(.restcount - 1)
                            '        .mode = 2
                            '        .combo(0) = taskno
                            '        .combo(1) = taskdone
                            '        .message = elses(0)
                            '    End With
                            'End If
                            'For nn = 0 To 1
                            '    If wear(nn) > 0 Then
                            '        .restcount = .restcount + 1
                            '        ReDim Preserve .restriction(.restcount)
                            '        With .restriction(.restcount - 1)
                            '            .mode = 0
                            '            If wear(nn) > 2 Then
                            '                .combo(0) = wdynamic(xns(wear(nn) - 3, wearable)) + 2 '1
                            '            Else
                            '                If wear(nn) = 1 Then
                            '                    .combo(0) = 1
                            '                Else
                            '                    .combo(0) = 0
                            '                End If
                            '            End If
                            '            .combo(1) = 2
                            '            .combo(2) = 0
                            '            .message = elses(2)
                            '        End With
                            '    End If
                            'Next nn
                            'If who > 0 Then
                            '    .restcount = .restcount + 1
                            '    ReDim Preserve .restriction(.restcount)
                            '    With .restriction(.restcount - 1)
                            '        .mode = 3
                            '        .combo(0) = 0
                            '        If who > 1 Then
                            '            .combo(1) = hereornot
                            '            .combo(2) = who '- 2 '1
                            '        Else
                            '            If hereornot = 0 Then .combo(1) = 2 Else .combo(1) = 3
                            '        End If
                            '        .message = elses(3)
                            '    End With
                            'End If
                            'If obstuff(0) > 0 Then
                            '    .restcount = .restcount + 1
                            '    ReDim Preserve .restriction(.restcount)
                            '    With .restriction(.restcount - 1)
                            '        Select Case obstuff(1)
                            '            Case 1 'in
                            '                .mode = 0
                            '                .combo(0) = xns(obstuff(0) - 1, dynamic) + 3
                            '                .combo(1) = 4
                            '                .combo(2) = obstuff(2)
                            '            Case 2 'on
                            '                .mode = 0
                            '                .combo(0) = xns(obstuff(0) - 1, dynamic) + 3 '?
                            '                .combo(1) = 5
                            '                .combo(2) = obstuff(2)
                            '            Case 3 'open
                            '                .mode = 1
                            '        If object(obstuff(0) - 1).openable = 0 Then object(obstuff(0) - 1).openable = 5
                            '                .combo(0) = wxns(obstuff(0) - 1, openable) ' + 1 ' - 1
                            '                .combo(1) = 0
                            '            Case 4 'closed
                            '                .mode = 1
                            '        If object(obstuff(0) - 1).openable = 0 Then object(obstuff(0) - 1).openable = 5
                            '                .combo(0) = wxns(obstuff(0) - 1, openable) ' + 1
                            '                .combo(1) = 1
                            '            Case 5 'held by
                            '                .mode = 0
                            '                .combo(0) = xns(obstuff(0) - 1, dynamic) + 3
                            '                .combo(1) = 1
                            '                .combo(2) = obstuff(2) + 1
                            '            Case 6 'worn by
                            '                .mode = 0
                            '                .combo(0) = xns(obstuff(0) - 1, dynamic) + 3
                            '                .combo(1) = 2
                            '                .combo(2) = obstuff(2) + 1
                            '        End Select
                            '        .message = elses(5)
                            '    End With
                            'End If

                            '.actioncount = 0
                            'If score <> 0 Then
                            '    .actioncount = .actioncount + 1
                            '    ReDim Preserve .action(.actioncount)
                            '    With .action(.actioncount - 1)
                            '        .mode = 4
                            '        .combo(0) = score
                            '    End With
                            'End If
                            'If kills = 1 Then
                            '    .actioncount = .actioncount + 1
                            '    ReDim Preserve .action(.actioncount)
                            '    With .action(.actioncount - 1)
                            '        .mode = 6 '5
                            '        .combo(0) = 2
                            '    End With
                            'End If
                            'If winning = 1 Then
                            '    .actioncount = .actioncount + 1
                            '    ReDim Preserve .action(.actioncount)
                            '    With .action(.actioncount - 1)
                            '        .mode = 6 '5
                            '        .combo(0) = 0
                            '    End With
                            'End If
                            'For nn = 0 To 5
                            '    If move(nn, 0) > 0 Then
                            '        .actioncount = .actioncount + 1
                            '        ReDim Preserve .action(.actioncount)
                            '        With .action(.actioncount - 1)
                            '            Select Case movemode(nn)
                            '                Case 0 ' to room
                            '                    Select Case move(nn, 0)
                            '                        Case 1 ' Player
                            '                            .mode = 1
                            '                            .combo(0) = 0
                            '                            If move(nn, 1) > num_room + 1 Then
                            '                                .combo(1) = 1
                            '                                .combo(2) = move(nn, 1) - 2 - num_room
                            '                            Else
                            '                                .combo(1) = 0
                            '                                .combo(2) = move(nn, 1) - 2
                            '                            End If
                            '                        Case 2 ' Ref object
                            '                            .mode = 0
                            '                            .combo(0) = 2
                            '                            If move(nn, 1) = 1 Then ' to same room as player
                            '                                .combo(1) = 6
                            '                                .combo(2) = 0
                            '                            ElseIf move(nn, 1) > num_room + 1 Then
                            '                                .combo(1) = 1
                            '                                .combo(2) = move(nn, 1) - 2 - num_room
                            '                            Else
                            '                                .combo(1) = 0
                            '                                .combo(2) = max(move(nn, 1) - 1, 0)
                            '                            End If
                            '                        Case 3 ' All held
                            '                            .mode = 0
                            '                            .combo(0) = 0
                            '                            If move(nn, 1) = 1 Then ' to same room as player
                            '                                .combo(1) = 6
                            '                                .combo(2) = 0
                            '                            ElseIf move(nn, 1) > num_room + 1 Then
                            '                                .combo(1) = 1
                            '                                .combo(2) = move(nn, 1) - 2 - num_room
                            '                            Else
                            '                                .combo(1) = 0
                            '                                .combo(2) = max(move(nn, 1) - 1, 0)
                            '                            End If
                            '                        Case Else ' the rest
                            '                            .mode = 0
                            '                            .combo(0) = move(nn, 0) - 1
                            '                            If move(nn, 1) = 1 Then ' to same room as player
                            '                                .combo(1) = 6
                            '                                .combo(2) = 0
                            '                            ElseIf move(nn, 1) > num_room + 1 Then
                            '                                .combo(1) = 1
                            '                                .combo(2) = move(nn, 1) - 2 - num_room
                            '                            Else
                            '                                .combo(1) = 0
                            '                                .combo(2) = max(move(nn, 1) - 1, 0)
                            '                            End If
                            '                    End Select
                            '                Case 1, 2 ' inside/onto object
                            '                    .mode = 0
                            '                    If move(nn, 0) = 1 Then
                            '                        ' Can't move player in/on an object!
                            '                        task(n).actioncount = task(n).actioncount - 1
                            '                    End If
                            '                    If move(nn, 0) = 2 Then .combo(0) = 2
                            '                    If move(nn, 0) > 2 Then .combo(0) = move(nn, 0) - 1
                            '                    If movemode(nn) = 1 Then .combo(1) = 2 Else .combo(1) = 3
                            '                    .combo(2) = move(nn, 1) - 1
                            '                Case 3, 4 ' to carried/worn by
                            '                    .mode = 0
                            '                    If move(nn, 0) = 1 Then
                            '                        ' Can't move player to carried by someone
                            '                        task(n).actioncount = task(n).actioncount - 1
                            '                    End If
                            '                    If move(nn, 0) = 2 Then .combo(0) = 2
                            '                    If move(nn, 0) > 2 Then .combo(0) = move(nn, 0) - 1
                            '                    .combo(1) = movemode(nn) + 1
                            '                    If move(nn, 1) = 0 Then
                            '                        .combo(2) = 0
                            '                    Else
                            '                        .combo(2) = move(nn, 1) + 1
                            '                    End If
                            '            End Select
                            '        End With
                            '    End If
                            'Next nn

                        Else

                            private int iNumRestriction = CInt(GetLine(bAdventure, iPos));
                            for (int i = 1; i <= iNumRestriction; i++)
                            {
                                private New clsRestriction NewRestriction;
                                With NewRestriction;
                                    private int iMode = CInt(GetLine(bAdventure, iPos));
                                    private int iCombo0 = CInt(GetLine(bAdventure, iPos));
                                    private int iCombo1 = CInt(GetLine(bAdventure, iPos));
                                    private int iCombo2;
                                    If iMode = 0 || iMode > 2 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                    If v < 4 && iMode = 4 && iCombo0 > 0 Then iCombo0 += 1
                                    private string sText = null;
                                    if (v >= 4)
                                    {
                                        If iMode = 4 Then sText = GetLine(bAdventure, iPos)
                                    }
                                    switch (iMode)
                                    {
                                        case 0 ' Object Locations:
                                            {
                                            .eType = clsRestriction.RestrictionTypeEnum.Object;
                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .sKey1 = NOOBJECT;
                                                case 1:
                                                    {
                                                    .sKey1 = ANYOBJECT;
                                                case 2:
                                                    {
                                                    .sKey1 = "ReferencedObject";
                                                default:
                                                    {
                                                    .sKey1 = GetObKey(iCombo0 - 3, ComboEnum.Dynamic);
                                            }
                                            switch (iCombo1)
                                            {
                                                case 0:
                                                case 6:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeAtLocation;
                                                    If iCombo1 = 6 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    if (iCombo2 = 0)
                                                    {
                                                        .eObject = clsRestriction.ObjectEnum.BeHidden;
                                                    Else
                                                        .sKey2 = "Location" + iCombo2;
                                                    }
                                                case 1:
                                                case 7:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeHeldByCharacter;
                                                    If iCombo1 = 7 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            .sKey2 = "%Player%";
                                                        case 1:
                                                            {
                                                            .sKey2 = "ReferencedCharacter";
                                                        default:
                                                            {
                                                            .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                    }
                                                case 2:
                                                case 8:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeWornByCharacter;
                                                    If iCombo1 = 8 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            .sKey2 = "%Player%";
                                                        case 1:
                                                            {
                                                            .sKey2 = "ReferencedCharacter";
                                                        default:
                                                            {
                                                            .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                    }
                                                case 3:
                                                case 9:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeVisibleToCharacter;
                                                    If iCombo1 = 9 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            .sKey2 = "%Player%";
                                                        case 1:
                                                            {
                                                            .sKey2 = "ReferencedCharacter";
                                                        default:
                                                            {
                                                            .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                    }
                                                case 4:
                                                case 10:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeInsideObject;
                                                    If iCombo1 = 10 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' Nothing
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Container);
                                                    }
                                                case 5:
                                                case 11:
                                                    {
                                                    .eObject = clsRestriction.ObjectEnum.BeOnObject;
                                                    If iCombo1 = 11 Then .eMust = clsRestriction.MustEnum.MustNot
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' Nothing
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Surface);
                                                    }
                                            }

                                            'GetLine(bAdventure, iPos) ' combo(2)
                                        case 1 ' Object status:
                                            {
                                            .eType = clsRestriction.RestrictionTypeEnum.Object;
                                            if (iCombo0 = 0)
                                            {
                                                .sKey1 = "ReferencedObject";
                                            Else
                                                .sKey1 = GetObKey(iCombo0 - 1, ComboEnum.WithStateOrOpenable);
                                            }
                                            .eMust = clsRestriction.MustEnum.Must;
                                            .eObject = clsRestriction.ObjectEnum.BeInState;
                                            private clsObject ob = null;
                                            If .sKey1 <> "ReferencedObject" Then ob = Adventure.htblObjects(.sKey1)
                                            switch (iCombo1)
                                            {
                                                case 0:
                                                    {
                                                    if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                    {
                                                        .sKey2 = "Open";
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                if (dictDodgyStates.ContainsKey(.sKey1))
                                                                {
                                                                    private string sIntended = dictDodgyStates(.sKey1).Split("|"c)(iCombo1);
                                                                    For Each state As String In prop.arlStates
                                                                        if (sIntended = state.ToLower)
                                                                        {
                                                                            .sKey2 = state;
                                                                            Exit For;
                                                                        }
                                                                    Next;
                                                                Else
                                                                    .sKey2 = prop.arlStates(iCombo1);
                                                                }
                                                            }
                                                        Next;
                                                    }
                                                case 1:
                                                    {
                                                    if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                    {
                                                        .sKey2 = "Closed";
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                if (dictDodgyStates.ContainsKey(.sKey1))
                                                                {
                                                                    private string sIntended = dictDodgyStates(.sKey1).Split("|"c)(iCombo1);
                                                                    For Each state As String In prop.arlStates
                                                                        if (sIntended = state.ToLower)
                                                                        {
                                                                            .sKey2 = state;
                                                                            Exit For;
                                                                        }
                                                                    Next;
                                                                Else
                                                                    .sKey2 = prop.arlStates(iCombo1);
                                                                }
                                                            }
                                                        Next;
                                                    }
                                                case 2:
                                                    {
                                                    if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                    {
                                                        if (.sKey1 = "ReferencedObject" || ob.Lockable)
                                                        {
                                                            .sKey2 = "Locked";
                                                        Else
                                                            For Each prop As clsProperty In ob.htblActualProperties.Values
                                                                if (prop.Key.IndexOf("|"c) > 0)
                                                                {
                                                                    if (dictDodgyStates.ContainsKey(.sKey1))
                                                                    {
                                                                        private string sIntended = dictDodgyStates(.sKey1).Split("|"c)(iCombo1 - 2);
                                                                        For Each state As String In prop.arlStates
                                                                            if (sIntended = state.ToLower)
                                                                            {
                                                                                .sKey2 = state;
                                                                                Exit For;
                                                                            }
                                                                        Next;
                                                                    Else
                                                                        .sKey2 = prop.arlStates(iCombo1 - 2);
                                                                    }
                                                                }
                                                            Next;
                                                        }
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                if (dictDodgyStates.ContainsKey(.sKey1))
                                                                {
                                                                    private string sIntended = dictDodgyStates(.sKey1).Split("|"c)(iCombo1);
                                                                    For Each state As String In prop.arlStates
                                                                        if (sIntended = state.ToLower)
                                                                        {
                                                                            .sKey2 = state;
                                                                            Exit For;
                                                                        }
                                                                    Next;
                                                                Else
                                                                    .sKey2 = prop.arlStates(iCombo1);
                                                                }
                                                            }
                                                        Next;
                                                    }
                                                default:
                                                    {
                                                    private int iOffset = 0;
                                                    if (.sKey1 = "ReferencedObject" || ob.Openable)
                                                    {
                                                        If .sKey1 = "ReferencedObject" || ob.Lockable Then iOffset = 3 Else iOffset = 2
                                                    }
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.arlStates(iCombo1 - iOffset);
                                                        }
                                                    Next;
                                            }


                                        case 2 ' Task status:
                                            {
                                            .eType = clsRestriction.RestrictionTypeEnum.Task;
                                            .sKey1 = "Task" + iCombo0 + iStartTask;
                                            if (iCombo1 = 0)
                                            {
                                                .eMust = clsRestriction.MustEnum.Must;
                                            Else
                                                .eMust = clsRestriction.MustEnum.MustNot;
                                            }
                                            .eTask = clsRestriction.TaskEnum.Complete;

                                        case 3 ' Characters:
                                            {
                                            .eType = clsRestriction.RestrictionTypeEnum.Character;
                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .sKey1 = "%Player%";
                                                case 1:
                                                    {
                                                    .sKey1 = "ReferencedCharacter";
                                                default:
                                                    {
                                                    .sKey1 = "Character" + iCombo0 - 1 + iStartChar;
                                            }
                                            switch (iCombo1)
                                            {
                                                case 0 ' Same room as:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsCharacter;
                                                case 1 ' Not same room as:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.MustNot;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeInSameLocationAsCharacter;
                                                case 2 ' Alone:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeAlone;
                                                case 3 ' Not alone:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.MustNot;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeAlone;
                                                case 4 ' standing on:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeStandingOnObject;
                                                case 5 ' sitting on:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeSittingOnObject;
                                                case 6 ' lying on:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeLyingOnObject;
                                                case 7  ' gender:
                                                    {
                                                    .eMust = clsRestriction.MustEnum.Must;
                                                    .eCharacter = clsRestriction.CharacterEnum.BeOfGender;
                                            }
                                            switch (iCombo1)
                                            {
                                                case 0:
                                                case 1:
                                                    {
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            .sKey2 = "%Player%";
                                                        case 1:
                                                            {
                                                            .sKey2 = "ReferencedCharacter";
                                                        default:
                                                            {
                                                            .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                    }
                                                case 4:
                                                    {
                                                    ' Standables
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = "TheFloor";
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Standable);
                                                    }
                                                case 5:
                                                    {
                                                    ' Sittables
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = "TheFloor";
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Sittable);
                                                    }
                                                case 6:
                                                    {
                                                    ' Lyables
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = "TheFloor";
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Lieable);
                                                    }

                                                case 7:
                                                    {
                                                    ' Gender
                                                    .sKey2 = (clsCharacter.GenderEnum)(iCombo2).ToString;
                                            }
                                        case 4 ' Variables:
                                            {
                                            .eType = clsRestriction.RestrictionTypeEnum.Variable;
                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .sKey1 = "ReferencedNumber";
                                                case 1:
                                                    {
                                                    .sKey1 = "ReferencedText";
                                                default:
                                                    {
                                                    .sKey1 = "Variable" + (iCombo0 - 1);
                                            }
                                            .sKey2 = "" ' Arrays not used in v4;
                                            .eMust = clsRestriction.MustEnum.Must;
                                            switch (iCombo1)
                                            {
                                                case 0:
                                                case 10:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.LessThan;
                                                case 1:
                                                case 11:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.LessThanOrEqualTo;
                                                case 2:
                                                case 12:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.EqualTo;
                                                case 3:
                                                case 13:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.GreaterThanOrEqualTo;
                                                case 4:
                                                case 14:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.GreaterThan;
                                                case 5:
                                                case 15:
                                                    {
                                                    .eVariable = clsRestriction.VariableEnum.EqualTo;
                                                    .eMust = clsRestriction.MustEnum.MustNot;
                                            }
                                            if (iCombo1 < 10)
                                            {
                                                .IntValue = iCombo2;
                                                If v >= 4 Then .StringValue = sText
                                            Else
                                                .IntValue = Integer.MinValue;
                                                .StringValue = "Variable" + iCombo2;
                                            }
                                            'GetLine(bAdventure, iPos) ' combo(2)
                                            'GetLine(bAdventure, iPos) ' text
                                    }

                                    .oMessage = New Description(ConvText(GetLine(bAdventure, iPos)));
                                }
                                .arlRestrictions.Add(NewRestriction);
                            Next;

                            private int iNumActions = CInt(GetLine(bAdventure, iPos));
                            for (int i = 1; i <= iNumActions; i++)
                            {
                                private New clsAction NewAction;
                                With NewAction;
                                    private int m = CInt(GetLine(bAdventure, iPos)) ' mode;
                                    private int iCombo0 = CInt(GetLine(bAdventure, iPos));
                                    Dim iCombo1, iCombo2, iCombo3 As Integer
                                    private string sExpression = "";
                                    if (v < 4)
                                    {
                                        If m < 4 || m = 6 Then iCombo1 = CInt(GetLine(bAdventure, iPos))
                                        If m = 0 || m = 1 || m = 3 || m = 6 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                        If m > 4 Then m += 1
                                        If m = 1 && iCombo1 = 2 Then iCombo2 += 2
                                        if (m = 7)
                                        {
                                            If iCombo0 >= 5 && iCombo0 <= 6 Then iCombo0 += 2
                                            If iCombo0 = 7 Then iCombo0 = 11
                                        }
                                    Else
                                        If m < 4 | m = 5 | m = 6 | m = 7 Then iCombo1 = CInt(GetLine(bAdventure, iPos))
                                        If m = 0 | m = 1 | m = 3 | m = 6 | m = 7 Then iCombo2 = CInt(GetLine(bAdventure, iPos))
                                    }

                                    if (m = 3)
                                    {
                                        if (v < 4)
                                        {
                                            if (iCombo1 = 5)
                                            {
                                                sExpression = GetLine(bAdventure, iPos) ' expression
                                            Else
                                                iCombo3 = CInt(GetLine(bAdventure, iPos)) ' combo(3)
                                            }
                                        Else
                                            sExpression = GetLine(bAdventure, iPos) ' expression
                                            iCombo3 = CInt(GetLine(bAdventure, iPos)) ' combo(3)
                                        }
                                    }
                                    switch (m)
                                    {
                                        case 0 ' Move object:
                                            {
                                            .eItem = clsAction.ItemEnum.MoveObject;
                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingHeldBy;
                                                    .sKey1 = THEPLAYER;
                                                    '.sKey1 = "AllHeldObjects"
                                                case 1:
                                                    {
                                                    .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.EverythingWornBy;
                                                    .sKey1 = THEPLAYER;
                                                    '.sKey1 = "AllWornObjects"
                                                case 2:
                                                    {
                                                    .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.Object;
                                                    .sKey1 = "ReferencedObject";
                                                default:
                                                    {
                                                    .eMoveObjectWhat = clsAction.MoveObjectWhatEnum.Object;
                                                    .sKey1 = GetObKey(iCombo0 - 3, ComboEnum.Dynamic);
                                            }

                                            switch (iCombo1)
                                            {
                                                case 0:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                                    if (iCombo2 = 0)
                                                    {
                                                        .sKey2 = "Hidden";
                                                    Else
                                                        .sKey2 = "Location" + iCombo2 + iStartLocations;
                                                    }
                                                case 1:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocationGroup;
                                                case 2:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.InsideObject;
                                                    .sKey2 = GetObKey(iCombo2, ComboEnum.Container);
                                                case 3:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.OntoObject;
                                                    .sKey2 = GetObKey(iCombo2, ComboEnum.Surface);
                                                case 4:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.ToCarriedBy;
                                                case 5:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.ToWornBy;
                                                case 6:
                                                    {
                                                    .eMoveObjectTo = clsAction.MoveObjectToEnum.ToSameLocationAs;
                                            }

                                            if (iCombo1 > 3)
                                            {
                                                switch (iCombo2)
                                                {
                                                    case 0:
                                                        {
                                                        .sKey2 = "%Player%";
                                                    case 1:
                                                        {
                                                        .sKey2 = "ReferencedCharacter";
                                                    default:
                                                        {
                                                        .sKey2 = "Character" + iCombo2 - 1 + iStartChar;
                                                }
                                            }

                                        case 1 ' Move character:
                                            {
                                            .eItem = clsAction.ItemEnum.MoveCharacter;
                                            .eMoveCharacterWho = clsAction.MoveCharacterWhoEnum.Character;

                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .sKey1 = THEPLAYER;
                                                case 1:
                                                    {
                                                    .sKey1 = "ReferencedCharacter";
                                                default:
                                                    {
                                                    .sKey1 = "Character" + iCombo0 - 1 + iStartChar;
                                            }

                                            switch (iCombo1)
                                            {
                                                case 0:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLocation;
                                                    if (.sKey1 = "%Player%")
                                                    {
                                                        .sKey2 = "Location" + iCombo2 + iStartLocations + 1;
                                                    Else
                                                        .sKey2 = "Location" + iCombo2 + iStartLocations;
                                                    }
                                                    If .sKey2 = "Location0" Then .sKey2 = "Hidden"
                                                case 1:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLocationGroup;

                                                case 2:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToSameLocationAs;
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            .sKey2 = "%Player%";
                                                        case 1:
                                                            {
                                                            .sKey2 = "ReferencedCharacter";
                                                        default:
                                                            {
                                                            .sKey2 = "Character" + iCombo2 - 2 + iStartChar;
                                                    }
                                                case 3:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToStandingOn;
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = THEFLOOR;
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Standable);
                                                    }
                                                case 4:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToSittingOn;
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = THEFLOOR;
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Sittable);
                                                    }
                                                case 5:
                                                    {
                                                    .eMoveCharacterTo = clsAction.MoveCharacterToEnum.ToLyingOn;
                                                    switch (iCombo2)
                                                    {
                                                        case 0:
                                                            {
                                                            ' The floor
                                                            .sKey2 = THEFLOOR;
                                                        default:
                                                            {
                                                            .sKey2 = GetObKey(iCombo2 - 1, ComboEnum.Lieable);
                                                    }
                                            }

                                        case 2 ' Change ob status:
                                            {
                                            .eItem = clsAction.ItemEnum.SetProperties;
                                            .sKey1 = GetObKey(iCombo0, ComboEnum.WithStateOrOpenable);
                                            private clsObject ob = Adventure.htblObjects(.sKey1);
                                            switch (iCombo1)
                                            {
                                                case 0:
                                                    {
                                                    if (ob.Openable)
                                                    {
                                                        .sKey2 = "OpenStatus";
                                                        .sPropertyValue = "Open";
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                .sKey2 = prop.Key;
                                                                .sPropertyValue = prop.arlStates(iCombo1);
                                                            }
                                                        Next;
                                                    }
                                                case 1:
                                                    {
                                                    if (ob.Openable)
                                                    {
                                                        .sKey2 = "OpenStatus";
                                                        .sPropertyValue = "Closed";
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                .sKey2 = prop.Key;
                                                                .sPropertyValue = prop.arlStates(iCombo1);
                                                            }
                                                        Next;
                                                    }
                                                case 2:
                                                    {
                                                    if (ob.Openable)
                                                    {
                                                        if (ob.Lockable)
                                                        {
                                                            .sKey2 = "OpenStatus";
                                                            .sPropertyValue = "Locked";
                                                        Else
                                                            For Each prop As clsProperty In ob.htblActualProperties.Values
                                                                if (prop.Key.IndexOf("|"c) > 0)
                                                                {
                                                                    .sKey2 = prop.Key;
                                                                    .sPropertyValue = prop.arlStates(iCombo1 - 2);
                                                                }
                                                            Next;
                                                        }
                                                    Else
                                                        For Each prop As clsProperty In ob.htblActualProperties.Values
                                                            if (prop.Key.IndexOf("|"c) > 0)
                                                            {
                                                                .sKey2 = prop.Key;
                                                                .sPropertyValue = prop.arlStates(iCombo1);
                                                            }
                                                        Next;
                                                    }
                                                default:
                                                    {
                                                    private int iOffset = 0;
                                                    if (ob.Openable)
                                                    {
                                                        If ob.Lockable Then iOffset = 3 Else iOffset = 2
                                                    }
                                                    For Each prop As clsProperty In ob.htblActualProperties.Values
                                                        if (prop.Key.IndexOf("|"c) > 0)
                                                        {
                                                            .sKey2 = prop.Key;
                                                            .sPropertyValue = prop.arlStates(iCombo1 - iOffset);
                                                        }
                                                    Next;
                                            }

                                        case 3 ' Change variable:
                                            {
                                            .eItem = clsAction.ItemEnum.SetVariable;
                                            .sKey1 = "Variable" + iCombo0 + 1 '+ iStartVariable;
                                            .eVariables = clsAction.VariablesEnum.Assignment;
                                            .StringValue = iCombo1 + Chr(1) + iCombo2 + Chr(1) + iCombo3 + Chr(1) + sExpression;

                                        case 4 ' Change score:
                                            {
                                            .eItem = clsAction.ItemEnum.SetVariable;
                                            .sKey1 = "Score";
                                            .eVariables = clsAction.VariablesEnum.Assignment;
                                            .StringValue = "1" + Chr(1) + iCombo0 + Chr(1) + "0" + Chr(1);
                                            '.IntValue = iCombo0

                                        case 5 ' Set Task:
                                            {
                                            .eItem = clsAction.ItemEnum.SetTasks;
                                            if (iCombo0 = 0)
                                            {
                                                .eSetTasks = clsAction.SetTasksEnum.Execute;
                                            Else
                                                .eSetTasks = clsAction.SetTasksEnum.Unset;
                                            }
                                            .sKey1 = "Task" + iCombo1 + iStartTask + 1;
                                            .StringValue = "";

                                        case 6 ' End game:
                                            {
                                            .eItem = clsAction.ItemEnum.EndGame;
                                            switch (iCombo0)
                                            {
                                                case 0:
                                                    {
                                                    .eEndgame = clsAction.EndGameEnum.Win;
                                                case 1:
                                                    {
                                                    .eEndgame = clsAction.EndGameEnum.Neutral;
                                                case 2:
                                                case 3:
                                                    {
                                                    .eEndgame = clsAction.EndGameEnum.Lose;
                                            }
                                        case 7 ' Battles:
                                            {
                                            ' TODO

                                    }
                                }
                                .arlActions.Add(NewAction);
                            Next;
                        }

                        private string sBrackSeq = "";
                        if (v < 4)
                        {
                            If .arlRestrictions.Count > 0 Then sBrackSeq = "#"
                            for (int i = 1; i <= .arlRestrictions.Count - 1; i++)
                            {
                                sBrackSeq &= "A#";
                            Next;
                            .arlRestrictions.BracketSequence = sBrackSeq;
                        Else
                            sBrackSeq = GetLine(bAdventure, iPos)
                            if (sBrackSeq <> "" && .arlRestrictions.BracketSequence <> "")
                            {
                                .arlRestrictions.BracketSequence &= "A";
                            }
                            .arlRestrictions.BracketSequence &= sBrackSeq;
                        }

#if Runner
                        If .arlRestrictions.BracketSequence != null Then .arlRestrictions.BracketSequence = .arlRestrictions.BracketSequence.Replace("[", "((").Replace("]", "))")
#endif
                        if (v >= 3.9)
                        {
                            if (bSound)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                private string sLoop = "";
                                if (sFilename.EndsWith("##"))
                                {
                                    sLoop = " loop=Y"
                                    sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                }
                                If sFilename <> "" Then .CompletionMessage(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .CompletionMessage(0).Description
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                            }
                            if (bGraphics)
                            {
                                sFilename = GetLine(bAdventure, iPos) ' Filename
                                If sFilename <> "" Then .CompletionMessage(0).Description = "<img src=""" + sFilename + """>" + .CompletionMessage(0).Description
                                If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                            }
                        }
                    }
                    .htblTasks.Add(NewTask, NewTask.Key);
                Next;


                '' Sort task mappings
                'For Each sTask As String In colNewTasks
                '    Dim task As clsTask = a.htblTasks(sTask)
                '    For Each act As clsAction In task.arlActions
                '        If act.eItem = clsAction.ItemEnum.SetTasks Then
                '            If htblKeyMapping.ContainsKey(act.sKey1) Then act.sKey1 = htblKeyMapping(act.sKey1)
                '        End If
                '    Next
                'Next


                '----------------------------------------------------------------------------------
                ' Events
                '----------------------------------------------------------------------------------



                private int iNumEvents = CInt(GetLine(bAdventure, iPos));
                for (int iEvent = 1; iEvent <= iNumEvents; iEvent++)
                {
                    private New clsEvent NewEvent;
                    private string sLocationKey = "";
                    With NewEvent;
                        .Key = "Event" + iEvent.ToString;
                        .Description = GetLine(bAdventure, iPos);
                        .WhenStart = (iPos)(GetLine(bAdventure), clsEvent.WhenStartEnum);
                        if (.WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns)
                        {
                            .StartDelay.iFrom = CInt(GetLine(bAdventure, iPos)) - 1 ' Start1;
                            .StartDelay.iTo = CInt(GetLine(bAdventure, iPos)) - 1 ' Start2;
                        }

                        if (.WhenStart = clsEvent.WhenStartEnum.AfterATask)
                        {
                            private string sStartTask = "Task" + GetLine(bAdventure, iPos);
                            private New EventOrWalkControl ec;
                            ec.eControl = EventOrWalkControl.ControlEnum.Start;
                            ec.sTaskKey = sStartTask;
                            ReDim Preserve .EventControls(.EventControls.Length);
                            .EventControls(.EventControls.Length - 1) = ec;
                        }

                        .Repeating = CBool(GetLine(bAdventure, iPos));
                        private int iTaskMode = CInt(GetLine(bAdventure, iPos)) ' task mode;
                        .Length.iFrom = CInt(GetLine(bAdventure, iPos));
                        .Length.iTo = CInt(GetLine(bAdventure, iPos));
                        if (.WhenStart = clsEvent.WhenStartEnum.BetweenXandYTurns)
                        {
                            .Length.iFrom -= 1;
                            .Length.iTo -= 1;
                        }
                        sBuffer = CStr(GetLine(bAdventure, iPos)) ' des1
                        if (sBuffer <> "")
                        {
                            private New clsEvent.SubEvent(.Key) se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sBuffer));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        sBuffer = CStr(GetLine(bAdventure, iPos)) ' des2
                        if (sBuffer <> "")
                        {
                            private New clsEvent.SubEvent(.Key) se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.SetLook;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sBuffer));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        private string sEndMessage = CStr(GetLine(bAdventure, iPos)) ' des3;

                        private int iWhichRooms = CInt(GetLine(bAdventure, iPos));
                        switch (iWhichRooms)
                        {
                            case 0 ' No rooms:
                                {
                                sLocationKey = ""
                            case 1 ' Single Room:
                                {
                                sLocationKey = "Location" & (CInt(GetLine(bAdventure, iPos)) + 1)
                            case 2 ' Multiple Rooms:
                                {
                                private bool bShowRoom;
                                private New StringArrayList arlShowInRooms;
                                for (int n = 1; n <= iNumLocations 'Adventure.htblLocations.Count; n++)
                                {
                                    bShowRoom = CBool(GetLine(bAdventure, iPos))
                                    If bShowRoom Then arlShowInRooms.Add("Location" + n)
                                Next;
                                sLocationKey = GetRoomGroupFromList(iPos, arlShowInRooms, "event '" & .Description & "'").Key
                            case 3 ' All Rooms:
                                {
                                sLocationKey = ALLROOMS
                        }

                        'Dim NewEventDescription As New clsEventDescription
                        'With NewEventDescription
                        '    .arlShowInRooms = arlShowInRooms.Clone
                        'End With
                        for (int i = 0; i <= 1; i++)
                        {
                            private int iTask = CInt(GetLine(bAdventure, iPos));
                            private int iCompleteOrNot = CInt(GetLine(bAdventure, iPos));
                            if (iTask > 0)
                            {
                                private New EventOrWalkControl ec;
                                ec.eControl = (EventOrWalkControl.ControlEnum.Suspend, EventOrWalkControl.ControlEnum.Resume)(IIf(i = 0), EventOrWalkControl.ControlEnum);
                                ec.sTaskKey = "Task" + (iTask - 1);
                                ec.eCompleteOrNot = (EventOrWalkControl.CompleteOrNotEnum.Completion, EventOrWalkControl.CompleteOrNotEnum.UnCompletion)(IIf(iCompleteOrNot = 0), EventOrWalkControl.CompleteOrNotEnum);
                                ReDim Preserve .EventControls(.EventControls.Length);
                                .EventControls(.EventControls.Length - 1) = ec;
                            }
                            'For n As Integer = 0 To 1
                            '    GetLine(bAdventure, iPos) ' pause(i,n)
                            'Next
                            private int iFrom = CInt(GetLine(bAdventure, iPos)) ' from(i);
                            sBuffer = CStr(GetLine(bAdventure, iPos)) ' ftext(i)
                            if (sBuffer <> "")
                            {
                                private New clsEvent.SubEvent(.Key) se;
                                se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                                se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                                se.ftTurns.iFrom = iFrom;
                                se.ftTurns.iTo = iFrom;
                                se.oDescription = New Description(ConvText(sBuffer));
                                ReDim Preserve .SubEvents(.SubEvents.Length);
                                .SubEvents(.SubEvents.Length - 1) = se;
                            }
                        Next;
                        if (sEndMessage <> "")
                        {
                            private New clsEvent.SubEvent(.Key) se;
                            se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage;
                            se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.oDescription = New Description(ConvText(sEndMessage));
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        private clsTask tas = null;
                        Dim iDoneTask(1) As Boolean
                        Dim iMoveObs(2, 1) As Integer
                        For Each i As Integer In New Integer() {1, 2, 0}
                            for (int j = 0; j <= 1; j++)
                            {
                                iMoveObs(i, j) = CInt(GetLine(bAdventure, iPos));
                            Next;
                        Next;
                        for (int i = 0; i <= 2; i++)
                        {
                            private int iObKey = iMoveObs(i, 0) ' CInt(GetLine(bAdventure, iPos));
                            private int iMoveTo = iMoveObs(i, 1) ' CInt(GetLine(bAdventure, iPos));
                            if (iObKey > 0)
                            {
                                private bool bNewTask = true;
                                If i = 1 && NewEvent.Length.iTo = 0 && iDoneTask(0) Then bNewTask = false
                                If i = 2 && ((NewEvent.Length.iTo = 0 && ! iDoneTask(1)) || iDoneTask(1)) Then bNewTask = false

                                if (bNewTask)
                                {
                                    private bool bMultiple = false;
                                    if (tas IsNot null)
                                    {
                                        bMultiple = True
                                        tas.Description = "Generated task #" + i + " for event " + NewEvent.Description;
                                    }
                                    tas = New clsTask
                                    tas.Key = "GenTask" + (Adventure.htblTasks.Count + 1);
                                    tas.Description = "Generated task" + IIf(bMultiple, " #" + i + 1, "").ToString + " for event " + NewEvent.Description;
                                    tas.Priority = iStartMaxPriority + Adventure.htblTasks.Count + 1;
                                }
                                If i < 2 Then iDoneTask(i) = true

                                With tas;
                                    .TaskType = clsTask.TaskTypeEnum.System;
                                    .Repeatable = true;
                                    private New clsAction act;
                                    act.eItem = clsAction.ItemEnum.MoveObject;
                                    act.sKey1 = "Object" + iObKey;
                                    switch (iMoveTo)
                                    {
                                        case 0 ' Hidden:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                            act.sKey2 = "Hidden";
                                        case 1 ' Players hands:
                                            {
                                            if (Adventure.htblObjects("Object" + iObKey).IsStatic)
                                            {
                                                act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation '  Don't allow for static;
                                                act.sKey2 = "Hidden";
                                            Else
                                                act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToCarriedBy;
                                                act.sKey2 = "%Player%";
                                            }
                                        case 2 ' Same room as player:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToSameLocationAs;
                                            act.sKey2 = "%Player%";
                                        case Else ' Locations:
                                            {
                                            act.eMoveObjectTo = clsAction.MoveObjectToEnum.ToLocation;
                                            act.sKey2 = "Location" + (iMoveTo - 2);
                                    }

                                    .arlActions.Add(act);
                                }
                                if (bNewTask)
                                {
                                    Adventure.htblTasks.Add(tas, tas.Key);
                                    private New clsEvent.SubEvent(.Key) se;
                                    se.eWhat = clsEvent.SubEvent.WhatEnum.ExecuteTask;
                                    If i = 0 Then se.eWhen = clsEvent.SubEvent.WhenEnum.FromStartOfEvent Else se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent
                                    se.ftTurns.iFrom = 0;
                                    se.ftTurns.iTo = 0;
                                    se.sKey = tas.Key;
                                    ReDim Preserve .SubEvents(.SubEvents.Length);
                                    .SubEvents(.SubEvents.Length - 1) = se;
                                }
                            }
                        Next;
                        For Each se As clsEvent.SubEvent In .SubEvents
                            If se.eWhat = clsEvent.SubEvent.WhatEnum.DisplayMessage || se.eWhat = clsEvent.SubEvent.WhatEnum.SetLook Then se.sKey = sLocationKey
                        Next;
                        private string sExecuteTask = "Task" + GetLine(bAdventure, iPos);
                        if (sExecuteTask <> "Task0")
                        {
                            private New clsEvent.SubEvent(.Key) se;
                            if (iTaskMode = 0)
                            {
                                se.eWhat = clsEvent.SubEvent.WhatEnum.ExecuteTask;
                            Else
                                se.eWhat = clsEvent.SubEvent.WhatEnum.UnsetTask;
                            }
                            se.eWhen = clsEvent.SubEvent.WhenEnum.BeforeEndOfEvent;
                            se.ftTurns.iFrom = 0;
                            se.ftTurns.iTo = 0;
                            se.sKey = sExecuteTask;
                            ReDim Preserve .SubEvents(.SubEvents.Length);
                            .SubEvents(.SubEvents.Length - 1) = se;
                        }
                        if (v >= 3.9)
                        {
                            for (int i = 0; i <= 4; i++)
                            {
                                if (bSound)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                                }
                                if (bGraphics)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    'If sFilename <> "" Then
                                    '    . .Description(0).Description &= "<img src=""" & sFilename & """>"
                                    'End If
                                    If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                                }
                            Next;
                        }
                    }
                    .htblEvents.Add(NewEvent, NewEvent.Key);
                Next;


                '----------------------------------------------------------------------------------
                ' Characters
                '----------------------------------------------------------------------------------


                private int iNumChars = CInt(GetLine(bAdventure, iPos));
                for (int iChar = 1; iChar <= iNumChars; iChar++)
                {
                    private New clsCharacter NewChar;
                    With NewChar;
                        '.Key = "Character" & iChar.ToString
                        private string sKey = "Character" + iChar.ToString;
                        if (a.htblCharacters.ContainsKey(sKey))
                        {
                            while (a.htblCharacters.ContainsKey(sKey))
                            {
                                sKey = IncrementKey(sKey)
                            }
                        }
                        .Key = sKey;
                        .CharacterType = clsCharacter.CharacterTypeEnum.NonPlayer;
                        .ProperName = GetLine(bAdventure, iPos);
                        .Prefix = GetLine(bAdventure, iPos);
                        ConvertPrefix(.Article, .Prefix);
                        if (v < 4)
                        {
                            private string sAlias = GetLine(bAdventure, iPos);
                            If sAlias <> "" Then .arlDescriptors.Add(sAlias)
                        Else
                            private int iNumAliases = CInt(GetLine(bAdventure, iPos));
                            for (int i = 1; i <= iNumAliases; i++)
                            {
                                .arlDescriptors.Add(GetLine(bAdventure, iPos));
                            Next;
                            If iNumAliases = 0 && .Prefix = "" Then .Article = ""
                        }

                        .Description = New Description(ConvText(GetLine(bAdventure, iPos)));
                        '.Location = "Location" & GetLine(bAdventure, iPos)
                        .Known = true;

                        private int iCharLoc = CInt(GetLine(bAdventure, iPos));
                        if (iCharLoc > 0)
                        {
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.AtLocation;
                            .Location.Key = "Location" + iCharLoc;
                        Else
                            .Location.ExistWhere = clsCharacterLocation.ExistsWhereEnum.Hidden;
                        }
                        'If Adventure.htblAllProperties.ContainsKey("CharacterLocation") Then
                        '    Dim cl As New clsProperty
                        '    cl = Adventure.htblAllProperties("CharacterLocation").Copy
                        '    If iCharLoc = 0 Then cl.Value = "Hidden" Else cl.Value = "At Location"
                        '    .htblProperties.Add(cl, cl.Key)

                        '    If cl.Value = "At Location" Then
                        '        Dim al As New clsProperty
                        '        al = Adventure.htblAllProperties("CharacterAtLocation").Copy
                        '        al.Value = "Location" & iCharLoc
                        '        .htblProperties.Add(al, al.Key)
                        '    End If
                        'End If

                        private clsProperty p;
                        'p = Adventure.htblAllProperties("Known").Copy
                        'p.Selected = True
                        '.htblActualProperties.Add(p)
                        '.bCalculatedGroups = False

                        private string sDesc2 = GetLine(bAdventure, iPos);
                        private string sDescTask = GetLine(bAdventure, iPos);
                        if (sDescTask <> "0")
                        {
                            private New SingleDescription sd;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.sKey1 = "Task" + sDescTask;
                            rest.eMust = clsRestriction.MustEnum.Must;
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            sd.Restrictions.Add(rest);
                            sd.Description = sDesc2;
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            sd.Restrictions.BracketSequence = "#";
                            .Description.Add(sd);
                        }
                        private int iNumSubjects = CInt(GetLine(bAdventure, iPos));
                        for (int i = 1; i <= iNumSubjects; i++)
                        {
                            private New clsTopic Topic;
                            With Topic;
                                .Key = "Topic" + i;
                                .Keywords = GetLine(bAdventure, iPos);
                                .Summary = "Ask about " + .Keywords;
                                .oConversation = New Description(ConvText(GetLine(bAdventure, iPos)));
                                .bAsk = true;
                                private int iTask = CInt(GetLine(bAdventure, iPos)) ' Rep Task;
                                if (iTask > 0)
                                {
                                    private New SingleDescription sd;
                                    private New clsRestriction rest;
                                    rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                                    rest.sKey1 = "Task" + iTask;
                                    rest.eMust = clsRestriction.MustEnum.Must;
                                    rest.eTask = clsRestriction.TaskEnum.Complete;
                                    sd.Restrictions.Add(rest);
                                    sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                                    sd.Restrictions.BracketSequence = "#";
                                    .oConversation.Add(sd);
                                }
                                .oConversation.Item(.oConversation.Count - 1).Description &= GetLine(bAdventure, iPos) ' Response 2;
                            }
                            .htblTopics.Add(Topic);
                        Next;

                        private int iNumWalks = CInt(GetLine(bAdventure, iPos));
                        private New StringArrayList arlNewDescriptions;
                        for (int i = 1; i <= iNumWalks; i++)
                        {
                            private New clsWalk Walk;
                            With Walk;
                                .sKey = sKey;
                                private string sStartTaskKey = "";
                                private int iNumberOfSteps = CInt(GetLine(bAdventure, iPos));
                                .Loops = CBool(GetLine(bAdventure, iPos));
                                private int iTask = CInt(GetLine(bAdventure, iPos));
                                if (iTask = 0)
                                {
                                    .StartActive = true;
                                Else
                                    .StartActive = false;
                                    sStartTaskKey = "Task" & iTask
                                    private New EventOrWalkControl wc;
                                    wc.eControl = EventOrWalkControl.ControlEnum.Start;
                                    wc.sTaskKey = sStartTaskKey;
                                    ReDim Preserve .WalkControls(.WalkControls.Length);
                                    .WalkControls(.WalkControls.Length - 1) = wc;
                                }
                                private int iCharTask = CInt(GetLine(bAdventure, iPos)) ' Runtask;
                                private int iObFind = CInt(GetLine(bAdventure, iPos)) ' Obfind;
                                private int iObTask = CInt(GetLine(bAdventure, iPos)) ' Obtask;
                                if (iObFind > 0 && iObTask > 0)
                                {
                                    private New clsWalk.SubWalk sw;
                                    sw.eWhat = clsWalk.SubWalk.WhatEnum.ExecuteTask;
                                    sw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                                    sw.sKey = "Object" + iObFind;
                                    sw.sKey2 = "Task" + iObTask;
                                    ReDim Preserve .SubWalks(.SubWalks.Length);
                                    .SubWalks(.SubWalks.Length - 1) = sw;
                                }
                                private string sTerminateTaskKey = "Task" + GetLine(bAdventure, iPos);
                                if (sTerminateTaskKey <> "Task0")
                                {
                                    private New EventOrWalkControl wc;
                                    wc.eControl = EventOrWalkControl.ControlEnum.Stop;
                                    wc.sTaskKey = sTerminateTaskKey;
                                    ReDim Preserve .WalkControls(.WalkControls.Length);
                                    .WalkControls(.WalkControls.Length - 1) = wc;
                                }
                                if (v >= 3.9)
                                {
                                    private int iCharFind = 0;
                                    If v >= 4 Then iCharFind = SafeInt(GetLine(bAdventure, iPos)) ' Who
                                    if (iCharTask > 0)
                                    {
                                        private New clsWalk.SubWalk sw;
                                        sw.eWhat = clsWalk.SubWalk.WhatEnum.ExecuteTask;
                                        sw.eWhen = clsWalk.SubWalk.WhenEnum.ComesAcross;
                                        if (v >= 4 && iCharFind > 0)
                                        {
                                            sw.sKey = "Character" + iCharFind;
                                        Else
                                            sw.sKey = "%Player%";
                                        }
                                        sw.sKey2 = "Task" + iCharTask;
                                        ReDim Preserve .SubWalks(.SubWalks.Length);
                                        .SubWalks(.SubWalks.Length - 1) = sw;
                                    }
                                    private string sNewDescription = GetLine(bAdventure, iPos);
                                    if (sNewDescription <> "")
                                    {
                                        arlNewDescriptions.Add(sStartTaskKey);
                                        arlNewDescriptions.Add(sNewDescription);
                                    }
                                }

                                'Dim sDescription As String = ""
                                for (int j = 1; j <= iNumberOfSteps; j++)
                                {
                                    private New clsWalk.clsStep Stp;
                                    With Stp;
                                        private int iLocation = CInt(GetLine(bAdventure, iPos));
                                        switch (iLocation)
                                        {
                                            case 0 ' Hidden:
                                                {
                                                .sLocation = "Hidden";
                                                'sDescription &= "Hidden"
                                            case 1 ' Follow Player:
                                                {
                                                .sLocation = "%Player%";
                                                'sDescription &= "Follow Player"
                                            case Else ' Locations:
                                                {
                                                if (iLocation - 1 > Adventure.htblLocations.Count)
                                                {
                                                    ' Location Group
                                                    'sDescription = "Group " & iLocation - 1
                                                    .sLocation = "Group" + iLocation - Adventure.htblLocations.Count - 1;
                                                Else
                                                    ' Location
                                                    .sLocation = "Location" + iLocation - 1;
                                                    'sDescription &= Adventure.htblLocations(.sLocation).ShortDescription
                                                }
                                        }
                                        private int iWaitTurns = CInt(GetLine(bAdventure, iPos));
                                        'If .sLocation = "%Player%" AndAlso iWaitTurns = 1 Then iWaitTurns = 0
                                        .ftTurns.iFrom = iWaitTurns;
                                        .ftTurns.iTo = iWaitTurns;
                                    }
                                    .arlSteps.Add(Stp);
                                Next;
                                .Description = .GetDefaultDescription;
                            }
                            .arlWalks.Add(Walk);
                        Next;
                        private bool bShowMove = CBool(GetLine(bAdventure, iPos));
                        private string sFromDesc = null;
                        private string sToDesc = null;
                        if (bShowMove)
                        {
                            p = Adventure.htblAllProperties("ShowEnterExit").Copy
                            p.Selected = true;
                            '.htblActualProperties.Add(p)
                            '.bCalculatedGroups = False
                            .AddProperty(p);
                            sFromDesc = GetLine(bAdventure, iPos)
                            p = Adventure.htblAllProperties("CharEnters").Copy
                            p.Selected = true;
                            p.StringData = New Description(ConvText(sFromDesc));
                            '.htblActualProperties.Add(p)
                            '.bCalculatedGroups = False
                            .AddProperty(p);
                            sToDesc = GetLine(bAdventure, iPos)
                            p = Adventure.htblAllProperties("CharExits").Copy
                            p.Selected = true;
                            p.StringData = New Description(ConvText(sToDesc));
                            '.htblActualProperties.Add(p)
                            '.bCalculatedGroups = False
                            .AddProperty(p);
                        }
                        'For Each Walk As clsWalk In .arlWalks
                        '    Walk.ShowMove = bShowMove
                        '    Walk.FromDesc = sFromDesc
                        '    Walk.ToDesc = sToDesc
                        'Next
                        private string sIsHereDesc = GetLine(bAdventure, iPos);
                        If sIsHereDesc = "#" Then sIsHereDesc = "%CharacterName[" + sKey + "]% is here."
                        if (sIsHereDesc <> "" || arlNewDescriptions.Count > 0)
                        {
                            p = Adventure.htblAllProperties("CharHereDesc").Copy
                            p.Selected = true;
                            '.htblActualProperties.Add(p)
                            '.bCalculatedGroups = False
                            .AddProperty(p);
                        }
                        If sIsHereDesc <> "" Then .SetPropertyValue("CharHereDesc", New Description(ConvText(sIsHereDesc)))
                        for (int i = 0; i <= arlNewDescriptions.Count - 1; i += 2)
                        {
                            private New SingleDescription sd;
                            private New clsRestriction rest;
                            rest.eType = clsRestriction.RestrictionTypeEnum.Task;
                            rest.sKey1 = arlNewDescriptions(i);
                            rest.eMust = clsRestriction.MustEnum.Must;
                            rest.eTask = clsRestriction.TaskEnum.Complete;
                            sd.Restrictions.Add(rest);
                            sd.Restrictions.BracketSequence = "#";
                            sd.Description = arlNewDescriptions(i + 1);
                            sd.eDisplayWhen = SingleDescription.DisplayWhenEnum.StartDescriptionWithThis;
                            .GetProperty("CharHereDesc").StringData.Add(sd);
                        Next;
                        if (v >= 3.9)
                        {
                            .Gender = (iPos)(GetLine(bAdventure), clsCharacter.GenderEnum);
                            for (int i = 0; i <= 3; i++)
                            {
                                if (bSound)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    if (sFilename <> "")
                                    {
                                        private string sLoop = "";
                                        if (sFilename.EndsWith("##"))
                                        {
                                            sLoop = " loop=Y"
                                            sFilename = sFilename.Substring(0, sFilename.Length - 2)
                                        }
                                        If i = 0 Then .Description(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .Description(0).Description
                                        If i = 1 Then .Description(1).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .Description(1).Description
                                        If i = 2 Then .GetProperty("CharEnters").StringData(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .GetProperty("CharEnters").StringData(0).Description
                                        If i = 3 Then .GetProperty("CharExits").StringData(0).Description = "<audio play src=""" + sFilename + """" + sLoop + ">" + .GetProperty("CharExits").StringData(0).Description
                                    }
                                    If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, false))
                                }
                                if (bGraphics)
                                {
                                    sFilename = GetLine(bAdventure, iPos) ' Filename
                                    if (sFilename <> "")
                                    {
                                        If i = 0 Then .Description(0).Description = "<img src=""" + sFilename + """>" + .Description(0).Description
                                        If i = 1 Then .Description(1).Description = "<img src=""" + sFilename + """>" + .Description(1).Description
                                        If i = 2 Then .GetProperty("CharEnters").StringData(0).Description = "<img src=""" + sFilename + """>" + .GetProperty("CharEnters").StringData(0).Description
                                        If i = 3 Then .GetProperty("CharExits").StringData(0).Description = "<img src=""" + sFilename + """>" + .GetProperty("CharExits").StringData(0).Description
                                    }
                                    If v >= 4 Then iFilesize = SafeInt(GetLine(bAdventure, iPos)) ' Filesize
                                    If sFilename <> "" && iFilesize > 0 Then a.dictv4Media.Add(sFilename, New clsAdventure.v4Media(0, iFilesize, true))
                                }
                            Next;
                            if (bBattleSystem)
                            {
                                GetLine(bAdventure, iPos) ' Attitude;
                                GetLine(bAdventure, iPos) ' Min Stamina;
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Max Stamina
                                GetLine(bAdventure, iPos) ' Min Strength;
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Max Strength
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Min Accuracy
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Max Accuracy
                                GetLine(bAdventure, iPos) ' Min Defence;
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Max Defence
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Min Agility
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Max Agility
                                GetLine(bAdventure, iPos) ' Speed;
                                GetLine(bAdventure, iPos) ' Die Task;
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Recovery
                                If v >= 4 Then GetLine(bAdventure, iPos) ' Low Task
                            }
                        }
                    }
                    .htblCharacters.Add(NewChar, NewChar.Key);
                Next;


                '----------------------------------------------------------------------------------
                ' Groups
                '----------------------------------------------------------------------------------

                ' Only room groups defined in 4.0 files
                private int iNumGroups = CInt(GetLine(bAdventure, iPos));
                for (int iGroup = 1; iGroup <= iNumGroups; iGroup++)
                {
                    private New clsGroup NewGroup;
                    With NewGroup;
                        .Key = "Group" + iGroup.ToString;
                        .Name = GetLine(bAdventure, iPos);
                        private bool bIncluded;
                        for (int i = 1; i <= iNumLocations 'Adventure.htblLocations.Count; i++)
                        {
                            bIncluded = CBool(GetLine(bAdventure, iPos))
                            If bIncluded Then .arlMembers.Add("Location" + i.ToString)
                        Next;
                    }
                    .htblGroups.Add(NewGroup, NewGroup.Key);
                Next;

                ' Sort out anything which needed groups defined
                For Each c As clsCharacter In Adventure.htblCharacters.Values
                    For Each w As clsWalk In c.arlWalks
                        For Each g As clsGroup In Adventure.htblGroups.Values
                            if (w.Description.Contains("<" + g.Key + ">"))
                            {
                                w.Description = w.Description.Replace("<" + g.Key + ">", g.Name);
                            }
                        Next;
                    Next;
                Next;


                '----------------------------------------------------------------------------------
                ' Synonyms
                '----------------------------------------------------------------------------------

                private int iNumSyn = CInt(GetLine(bAdventure, iPos));
                for (int iSyn = 1; iSyn <= iNumSyn; iSyn++)
                {
                    private string sTo = GetLine(bAdventure, iPos) ' System Command;
                    private string sFrom = GetLine(bAdventure, iPos) ' Alternative Command;
                    private clsSynonym synNew = null;
                    For Each syn As clsSynonym In Adventure.htblSynonyms.Values
                        if (syn.ChangeTo = sTo)
                        {
                            synNew = syn
                            Exit For;
                        }
                    Next;
                    if (synNew Is null)
                    {
                        synNew = New clsSynonym
                        synNew.Key = "Synonym" + iSyn.ToString;
                    }
                    With synNew;
                        .ChangeTo = sTo;
                        .ChangeFrom.Add(sFrom);
                    }
                    If ! .htblSynonyms.ContainsKey(synNew.Key) Then .htblSynonyms.Add(synNew)
                Next;



                if (v >= 3.9)
                {

                    '----------------------------------------------------------------------------------
                    ' Variables
                    '----------------------------------------------------------------------------------

                    private int iNumVariables = CInt(GetLine(bAdventure, iPos));
                    for (int iVar = 1; iVar <= iNumVariables; iVar++)
                    {
                        private New clsVariable NewVariable;
                        With NewVariable;
                            .Key = "Variable" + iVar.ToString;
                            .Name = GetLine(bAdventure, iPos);
                            if (v < 4)
                            {
                                .Type = clsVariable.VariableTypeEnum.Numeric;
                                .IntValue = CInt(GetLine(bAdventure, iPos));
                            Else
                                .Type = (iPos)(GetLine(bAdventure), clsVariable.VariableTypeEnum);
                                if (.Type = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    .IntValue = CInt(GetLine(bAdventure, iPos));
                                Else
                                    .StringValue = GetLine(bAdventure, iPos);
                                }
                            }

                        }
                        .htblVariables.Add(NewVariable, NewVariable.Key);
                    Next;

                    ' Change Variable names in Actions/Restrictions, and sort assignments
                    For Each tas As clsTask In Adventure.htblTasks.Values
                        For Each rest As clsRestriction In tas.arlRestrictions
                            if (rest.eType = clsRestriction.RestrictionTypeEnum.Variable)
                            {
                                if (rest.sKey1 = "ReferencedText" || Adventure.htblVariables.ContainsKey(rest.sKey1))
                                {
                                    if (rest.sKey1 = "ReferencedText" || Adventure.htblVariables(rest.sKey1).Type = clsVariable.VariableTypeEnum.Text)
                                    {
                                        switch (rest.eVariable)
                                        {
                                            case clsRestriction.VariableEnum.LessThan:
                                                {
                                                rest.eVariable = clsRestriction.VariableEnum.EqualTo;
                                            case clsRestriction.VariableEnum.LessThanOrEqualTo:
                                                {
                                                rest.eVariable = clsRestriction.VariableEnum.EqualTo;
                                                rest.eMust = clsRestriction.MustEnum.MustNot;
                                        }
                                        rest.StringValue = """" + rest.StringValue + """";
                                    }
                                }
                            }
                        Next;
                        For Each act As clsAction In tas.arlActions
                            if (act.eItem = clsAction.ItemEnum.SetVariable)
                            {
                                private int iCombo1 = CInt(act.StringValue.Split(Chr(1))(0));
                                private int iCombo2 = CInt(act.StringValue.Split(Chr(1))(1));
                                private int iCombo3 = CInt(act.StringValue.Split(Chr(1))(2));
                                private string sExpression = act.StringValue.Split(Chr(1))(3);
                                if (a.htblVariables(act.sKey1).Type = clsVariable.VariableTypeEnum.Numeric)
                                {
                                    switch (iCombo1)
                                    {
                                        case 0 ' to exact value:
                                            {
                                            act.StringValue = iCombo2.ToString;
                                        case 1 ' by exact value:
                                            {
                                            act.StringValue = "%" + a.htblVariables(act.sKey1).Name + "% + " + iCombo2.ToString;
                                        case 2 ' To Random value between X and Y:
                                            {
                                            act.StringValue = "Rand(" + iCombo2 + ", " + iCombo3 + ")";
                                        case 3 ' By Random value between X and Y:
                                            {
                                            act.StringValue = "%" + a.htblVariables(act.sKey1).Name + "% + Rand(" + iCombo2 + ", " + iCombo3 + ")";
                                        case 4 ' to referenced number:
                                            {
                                            act.StringValue = "%number%";
                                        case 5 ' to expression:
                                            {
                                            act.StringValue = sExpression;
                                        case 6:
                                        case 7:
                                        case 8:
                                        case 9:
                                        case 10:
                                            {
                                            act.StringValue = "";
                                    }
                                Else
                                    switch (iCombo1)
                                    {
                                        case 0 ' exact text:
                                            {
                                            act.StringValue = """" + sExpression.Replace("""", "\""") + """";
                                        case 1 ' to referenced text:
                                            {
                                            act.StringValue = "%text%";
                                        case 2 ' to expression:
                                            {
                                            act.StringValue = sExpression;
                                    }
                                }

                            }
                            if (sInstr(act.StringValue, "Variable") > 0)
                            {
                                for (int iVar = Adventure.htblVariables.Count; iVar <= 1; iVar += -1)
                                {
                                    If Adventure.htblVariables.ContainsKey("Variable" + iVar) Then act.StringValue = act.StringValue.Replace("Variable" + iVar, "%" + Adventure.htblVariables("Variable" + iVar).Name + "%")
                                Next;
                            }
                        Next;
                    Next;



                    '----------------------------------------------------------------------------------
                    ' ALRs
                    '----------------------------------------------------------------------------------

                    private int iNumALR = CInt(GetLine(bAdventure, iPos));
                    for (int iALR = 1; iALR <= iNumALR; iALR++)
                    {
                        private New clsALR NewALR;
                        With NewALR;
                            .Key = "ALR" + iALR.ToString;
                            .OldText = GetLine(bAdventure, iPos);
                            .NewText = New Description(ConvText(GetLine(bAdventure, iPos)));
                        }
                        .htblALRs.Add(NewALR, NewALR.Key);
                    Next;
                }


                private bool bSetFont = CBool(SafeInt(GetLine(bAdventure, iPos))) ' Set Font?;
                if (bSetFont)
                {
                    private string sFont = GetLine(bAdventure, iPos) ' Font;
                    if (sFont.Contains(","))
                    {
                        Adventure.DefaultFontName = sFont.Split(","c)(0);
                        Adventure.DefaultFontSize = SafeInt(sFont.Split(","c)(1));
                    }
                }

                if (v >= 4)
                {
                    private int iMediaOffset = bAdvZLib.Length + 23;
                    For Each m As clsAdventure.v4Media In a.dictv4Media.Values
                        m.iOffset = iMediaOffset;
                        iMediaOffset += m.iLength + 1;
                    Next;
                }

                '--- the rest ---
                while (iPos < bAdventure.Length - 1)
                {
                    private string s3 = GetLine(bAdventure, iPos);
                }

                ' Make sure all the 'seen's are set
                For Each ch As clsCharacter In Adventure.htblCharacters.Values
                    ch.Move(ch.Location);
                Next;

#if Not www
                .Map = New clsMap;
                .Map.RecalculateLayout();
#if Generator
                fGenerator.Map1.Map = .Map;

                if (a.dictv4Media.Count > 0)
                {
                    if (MessageBox.Show("Would you like to extract all media from this TAF?", "Extract Media", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
                    {
                        fGenerator.fbd.ShowDialog();
                        if (fGenerator.fbd.SelectedPath <> "")
                        {
                            For Each sFile As String In a.dictv4Media.Keys
                                With a.dictv4Media(sFile);
                                    if (.bImage)
                                    {
                                        private Image img = Getv4Image(sFile);
                                        if (img IsNot null)
                                        {
                                            private string sJustFile = IO.Path.GetFileName(sFile);
                                            img.Save(fGenerator.fbd.SelectedPath + "\" + sJustFile);
                                            'For Each sd As SingleDescription In Adventure.Introduction
                                            '    sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            'For Each sd As SingleDescription In Adventure.WinningText
                                            '    sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            'For Each itm As clsItem In a.dictAllItems.Values
                                            '    itm.SearchAndReplace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            For Each sd As SingleDescription In a.AllDescriptions '.dictAllItems.Values
                                                sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath + "\" + sJustFile);
                                            Next;
                                        }
                                    Else
                                        private string sJustFile = IO.Path.GetFileName(sFile);
                                        if (Getv4Audio(sFile, fGenerator.fbd.SelectedPath + "\" + sJustFile))
                                        {
                                            'For Each sd As SingleDescription In Adventure.Introduction
                                            '    sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            'For Each sd As SingleDescription In Adventure.WinningText
                                            '    sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            'For Each itm As clsItem In a.dictAllItems.Values
                                            '    itm.SearchAndReplace(sFile, fGenerator.fbd.SelectedPath & "\" & sJustFile)
                                            'Next
                                            For Each sd As SingleDescription In a.AllDescriptions '.dictAllItems.Values
                                                sd.Description = sd.Description.Replace(sFile, fGenerator.fbd.SelectedPath + "\" + sJustFile);
                                            Next;
                                        }
                                    }
                                }
                            Next;
                        }
                        a.dictv4Media.Clear();
                    }
                }
#else
                UserSession.Map.Map = .Map;
#endif
#endif

            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error loading Adventure", ex);
            return false;
        }
        finally
        {
            bAdventure = Nothing
        }

        return true;

    }


    public Drawing.Image Getv4Image(string sFilename)
    {

        if (Adventure.dictv4Media.ContainsKey(sFilename))
        {
            With Adventure.dictv4Media(sFilename);
                private New IO.FileStream(Adventure.FullPath, IO.FileMode.Open, IO.FileAccess.Read) stmFile;
                stmFile.Position = .iOffset;
                Dim bytMedia(.iLength - 1) As Byte
                stmFile.Read(bytMedia, 0, .iLength);
                stmFile.Close();

                private New IO.MemoryStream(bytMedia) msImage;
                return new Bitmap(msImage);
            }
        Else
            ErrMsg("File " + sFilename + " not found in index.");
        }
        return null;

    }


    public bool Getv4Audio(string sFilename, string sOutputFile)
    {

        if (Adventure.dictv4Media.ContainsKey(sFilename))
        {
            With Adventure.dictv4Media(sFilename);
                private New IO.FileStream(Adventure.FullPath, IO.FileMode.Open, IO.FileAccess.Read) stmFile;
                stmFile.Position = .iOffset;
                Dim bytMedia(.iLength - 1) As Byte
                stmFile.Read(bytMedia, 0, .iLength);
                stmFile.Close();

                if (sOutputFile <> "")
                {
                    private New IO.FileStream(sOutputFile, IO.FileMode.Create) stmOutput;
                    stmOutput.Write(bytMedia, 0, bytMedia.Length - 1);
                    stmOutput.Close();
                }

                return true;
            }
        Else
            ErrMsg("File " + sFilename + " not found in index.");
        }

        return false;

    }


    private void ConvertPrefix(ref sArticle As String, ref sPrefix As String)
    {
        switch (sPrefix.ToLower)
        {
            case "":
                {
                sArticle = "a"
            case "a":
            case "an":
            case "hers":
            case "his":
            case "my":
            case "some":
            case "the":
            case "your":
                {
                sArticle = sPrefix
                sPrefix = ""
            default:
                {
                ' Ignore
        }
        if (sLeft(sPrefix, 2) = "a " || sLeft(sPrefix, 3) = "an " || sLeft(sPrefix, 5) = "some " || sLeft(sPrefix, 4) = "his " || sLeft(sPrefix, 5) = "hers " || sLeft(sPrefix, 4) = "the ")
        {
            sArticle = Split(sPrefix, " ")(0)
            sPrefix = sRight(sPrefix, Len(sPrefix) - sInstr(sPrefix, " "))
        }
    }


    private clsGroup GetRoomGroupFromList(ref iPos As Integer, StringArrayList salLocations, string sGeneratedDescription)
    {

        private clsGroup grp = null;
        ' Check to see if a room exists with same rooms
        For Each agrp As clsGroup In Adventure.htblGroups.Values
            if (agrp.GroupType = clsGroup.GroupTypeEnum.Locations)
            {
                if (agrp.arlMembers.Count = salLocations.Count)
                {
                    For Each sGroupKey As String In agrp.arlMembers
                        If ! salLocations.Contains(sGroupKey) Then GoTo NextGroup
                    Next;
                    grp = agrp
                    Exit For;
                }
            }
NextGroup:;
        Next;
        if (grp Is null)
        {
            grp = New clsGroup
            grp.Name = "Generated group for " + sGeneratedDescription;
            For Each sLocKey As String In salLocations
                grp.arlMembers.Add(sLocKey);
            Next;
            grp.Key = "GeneratedLocationGroup" + Adventure.htblGroups.Count + 1;
            grp.GroupType = clsGroup.GroupTypeEnum.Locations;
            Adventure.htblGroups.Add(grp, grp.Key);
        Else
            grp.Name &= " and " + sGeneratedDescription;
        }

        return grp;

    }


    public int CurrentMaxPriority(bool bIncludeLibrary = false)
    {

        private int iMax = 0;

        For Each tas As clsTask In Adventure.htblTasks.Values
            if (bIncludeLibrary)
            {
                If tas.Priority > iMax Then iMax = tas.Priority
            Else
                If tas.Priority > iMax && tas.Priority < 50000 Then iMax = tas.Priority
            }
        Next;

        return iMax;

    }


    internal void LoadLibraries(LoadWhatEnum eLoadWhat, string sOnlyLoad = "")
    {

        Dim sLibraries() As String = GetSetting("ADRIFT", "Generator", "Libraries").Split("|"c)
        private string sError = "";

        if (sLibraries.Length = 0 || (sLibraries.Length = 1 && sLibraries(0) = ""))
        {
            if (File.Exists(BinPath + Path.DirectorySeparatorChar + "StandardLibrary.amf"))
            {
                ReDim sLibraries(0);
                sLibraries(0) = BinPath + Path.DirectorySeparatorChar + "StandardLibrary.amf";
            }
        }

        For Each sLibrary As String In sLibraries
            private bool bLoad = true;
            if (sLibrary.Contains("#"))
            {
                bLoad = CBool(sLibrary.Split("#"c)(1))
                sLibrary = sLibrary.Split("#"c)(0)
            }
            if (bLoad && File.Exists(sLibrary) && (sOnlyLoad = "" || IO.Path.GetFileNameWithoutExtension(sLibrary).ToLower = sOnlyLoad))
            {
                private bool bLoadLibrary = true;
                '#If Generator Then
                '#If DEBUG Then
                '                If True Then ' Always check to see if the libraries are ok in Gen debug
                '#Else
                '                If Not IsRegistered Then ' Check to see if libraries are ok in Gen unregistered
                '#End If
                '#Else
                'If False Then ' Don't check in Runner
                '    '#End If
                '    ' Check it's a standard library and hasn't been tampered with
                '    Dim iSize As Integer = 0
                '    Dim dictChars As New Generic.Dictionary(Of Integer, Char)
                '    Dim bLibraryOK As Boolean = False
                '    If sLibrary.EndsWith("StandardLibrary.amf") Then
                '        iSize = 166126
                '        dictChars.Add(10013, "u"c)
                '        dictChars.Add(90056, "d"c)
                '    ElseIf sLibrary.EndsWith("Lighting.amf") Then
                '        iSize = 6192
                '        dictChars.Add(4979, "e"c)
                '    ElseIf sLibrary.EndsWith("MoneySystem.amf") Then
                '        iSize = 13457
                '        dictChars.Add(9980, "i"c)
                '    ElseIf sLibrary.EndsWith("ObjectManipulation.amf") Then
                '        iSize = 2825
                '        dictChars.Add(999, "n"c)
                '    Else
                '        iSize = -1
                '    End If
                '    If FileLen(sLibrary) = iSize Then
                '        bLibraryOK = True
                '        For Each i As Integer In dictChars.Keys
                '            Dim fStream As New IO.FileStream(sLibrary, IO.FileMode.Open, IO.FileAccess.Read)
                '            Dim bData(0) As Byte
                '            fStream.Seek(i, IO.SeekOrigin.Begin)
                '            fStream.Read(bData, 0, 1)
                '            fStream.Close()
                '            If dictChars(i) <> ChrW(CInt(bData(0))) Then bLibraryOK = False
                '        Next
                '    End If

                '    If Not bLibraryOK Then
                '        sError &= sLibrary & vbCrLf
                '        bLoadLibrary = False
                '    End If
                'End If
#if DEBUG
                bLoadLibrary = True
#endif
                If bLoadLibrary Then LoadFile(sLibrary, FileTypeEnum.XMLModule_AMF, eLoadWhat, true)
            }
        Next;

        if (sError <> "")
        {
            ErrMsg("Sorry.  The unregistered version of ADRIFT will only load the original library files.  The following libraries were not loaded:" + vbCrLf + vbCrLf + sError);
        }

    }


    internal void OverwriteLibraries(LoadWhatEnum eLoadWhat)
    {

        Dim sLibraries() As String = GetSetting("ADRIFT", "Generator", "Libraries").Split("|"c)

        For Each sLibrary As String In sLibraries
            private bool bLoad = true;
            if (sLibrary.Contains("#"))
            {
                bLoad = CBool(sLibrary.Split("#"c)(1))
                sLibrary = sLibrary.Split("#"c)(0)
            }
            if (bLoad && File.Exists(sLibrary))
            {
                LoadFile(sLibrary, FileTypeEnum.XMLModule_AMF, eLoadWhat, true, Adventure.LastUpdated);
            }
        Next;

    }


    ' v4 GetLine
    private Byte, ByRef iStartPos As Integer) As String GetLine(bData()
    {

        try
        {
            private int iEnd = Array.IndexOf(bData, CByte(13), iStartPos);
            if (iEnd < 0)
            {
                iEnd = bData.Length - 1
                If iEnd < iStartPos Then iEnd = iStartPos
            }

            try
            {
                GetLine = System.Text.Encoding.Default.GetString(bData, iStartPos, iEnd - iStartPos)
            Catch
                '    GetLine = "0"
                MsgBox("iStartPos: " + iStartPos + ", iEnd: " + iEnd + ", bData.Length: " + bData.Length);
                GetLine = ""
            }

            iStartPos = iEnd + 2
        }
        catch (Exception ex)
        {
            return "";
        }

    }


    '' v5.0 GetLine
    'Private Function gl(ByVal bData() As Byte, ByRef iStartPos As Integer) As String

    '    Dim iEnd As Integer = bData.IndexOf(bData, CByte(13), iStartPos)
    '    If iEnd < 0 Then iEnd = bData.Length - 1

    '    gl = System.Text.Encoding.UTF8.GetString(bData, iStartPos, iEnd - iStartPos)
    '    iStartPos = iEnd + 1

    'End Function


    ' Simple encryption
    private string Dencode(string sText)
    {

        Rnd(-1);
        Randomize(1976);

        Dencode = ""
        for (int n = 1; n <= sText.Length; n++)
        {
            'Dencode = Dencode & Chr((Asc(CChar(Mid(sText, n, 1))) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256)
            Dencode = Dencode & Chr((Asc(Mid(sText, n, 1)) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256)
        Next n;

    }
    internal byte[] Dencode(string sText, long lOffset)
    {

        Rnd(-1);
        Randomize(1976);

        for (long n = 1; n <= lOffset - 1; n++)
        {
            Rnd();
        Next;

        Dim result(sText.Length - 1) As Byte
        for (int n = 1; n <= sText.Length; n++)
        {
            'result(n - 1) = CByte((Asc(CChar(sMid(sText, n, 1))) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256)
            result(n - 1) = CByte((Asc(Mid(sText, n, 1)) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256);
        Next;

        return result;

    }
    internal void Dencode(Byte( bData)
    {

        Rnd(-1);
        Randomize(1976);

        for (long n = 1; n <= lOffset - 1; n++)
        {
            Rnd();
        Next;

        Dim result(bData.Length - 1) As Byte
        for (int n = 1; n <= bData.Length; n++)
        {
            'result(n - 1) = CByte((Asc(CChar(sMid(sText, n, 1))) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256)
            result(n - 1) = CByte((bData(n - 1) Xor Int(CInt(Rnd() * 255 - 0.5))) Mod 256);
        Next;

        return result;

    }



    'Private Sub z_DataAvailable(ByVal data() As Byte, ByVal startIndex As Integer, ByVal count As Integer) Handles zi.DataAvailable, zd.DataAvailable

    '    ' Append the new data to our bAdv byte array
    '    Dim bNew(bAdventure.Length - 1) As Byte
    '    bAdventure.CopyTo(bNew, 0)
    '    ReDim bAdventure(bNew.Length + count - 1)
    '    bNew.CopyTo(bAdventure, 0)
    '    If count < data.Length Then ' Special case so we can truncate data
    '        Dim bDataCopy(count - 1) As Byte
    '        Array.Copy(data, bDataCopy, count)
    '        data = bDataCopy
    '    End If
    '    data.CopyTo(bAdventure, bNew.Length)

    'End Sub


private enum ComboEnum
    {
        Dynamic;
        WithState;
        WithStateOrOpenable;
        Surface;
        Container;
        Wearable;
        Sittable;
        Standable;
        Lieable;
    }
    private string GetObKey(int iComboIndex, ComboEnum eCombo)
    {

        private int iMatching;
        private int i = 1;
        private string sKey;
        private clsObject ob = null;

        try
        {

            while (iMatching <= iComboIndex && i < Adventure.htblObjects.Count + 1)
            {
                sKey = "Object" & i
                ob = Adventure.htblObjects(sKey)
                switch (eCombo)
                {
                    case ComboEnum.Dynamic:
                        {
                        If ! ob.IsStatic Then iMatching += 1
                    case ComboEnum.WithState:
                        {
                        If salWithStates.Contains(sKey) Then iMatching += 1
                    case ComboEnum.WithStateOrOpenable:
                        {
                        If salWithStates.Contains(sKey) || ob.Openable Then iMatching += 1
                    case ComboEnum.Surface:
                        {
                        If ob.HasSurface Then iMatching += 1
                    case ComboEnum.Container:
                        {
                        If ob.IsContainer Then iMatching += 1
                    case ComboEnum.Wearable:
                        {
                        If ob.IsWearable Then iMatching += 1
                    case ComboEnum.Sittable:
                        {
                        If ob.IsSittable Then iMatching += 1
                    case ComboEnum.Standable:
                        {
                        If ob.IsStandable Then iMatching += 1
                    case ComboEnum.Lieable:
                        {
                        If ob.IsLieable Then iMatching += 1
                }
                i += 1;
            }
            if (ob IsNot null)
            {
                return ob.Key;
            Else
                return "";
            }
        }
        catch (Exception ex)
        {
            ErrMsg("GetObKey error", ex);
        }

        return null;

    }



}
}