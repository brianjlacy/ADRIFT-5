using System;
using System.Collections.Generic;
using System.Drawing ' Makes it explicit over Web Image etc;
using System.Linq;
using System.Text.Encoding;
using System.Xml;

namespace ADRIFT
{


public class clsBlorb
{

    private static int iReadOffset = 0;
    private static IO.FileStream ' .MemoryStream stmBlorb;
    private static Long ' Where does the Blorb start within the file BlorbOffset;

    internal static byte[] ExecResource;
    internal static bool bObfuscated = true;
    internal static string ExecType = null;
    internal static New Dictionary<int, Image> ImageResources;
    internal static New Dictionary<int, SoundFile> SoundResources;
    internal static System.Xml.XmlDocument MetaData;
    internal static int Frontispiece = -1;
    internal static New Dictionary<string, UInt32> ResourceIndex;
    internal static string sFilename;
    internal static bool bEXE = false;
    internal static string Data;
    internal static string DataType;

    private static FormChunk cnkFORM;


    internal Image GetImage(int iResourceNumber, bool bStore = false, ref sExtn As String = Nothing)
    {

        private bool bStreamOpen = stmBlorb.CanRead;

        try
        {

            if (Not ImageResources.ContainsKey(iResourceNumber))
            {
                if (ResourceIndex.ContainsKey("Pict" + iResourceNumber))
                {
                    private UInt32 iOffset = ResourceIndex("Pict" + iResourceNumber);
                    private New PictResourceChunk cnkImage;

                    If ! bStreamOpen Then stmBlorb = New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    stmBlorb.Position = iOffset + BlorbOffset;

                    if (cnkImage.LoadChunk)
                    {
                        sExtn = cnkImage.img.sExtn
                        If ! bStore Then Return cnkImage.img.Image
                        ImageResources.Add(iResourceNumber, cnkImage.img.Image);
                    }
                }
            }
            If ImageResources.ContainsKey(iResourceNumber) Then Return ImageResources(iResourceNumber)

        }
        catch (Exception ex)
        {
            ErrMsg("GetImage error", ex);
        }
        finally
        {
            If ! bStreamOpen Then stmBlorb.Close()
        }
        return null;

    }


    internal SoundFile GetSound(int iResourceNumber, bool bStore = false, ref sExtn As String = Nothing)
    {

        private bool bStreamOpen = stmBlorb.CanRead;

        try
        {
            if (Not SoundResources.ContainsKey(iResourceNumber))
            {
                if (ResourceIndex.ContainsKey("Snd " + iResourceNumber))
                {
                    private UInt32 iOffset = ResourceIndex("Snd " + iResourceNumber);
                    private New SoundResourceChunk cnkSound;

                    If ! bStreamOpen Then stmBlorb = New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
                    stmBlorb.Position = iOffset + BlorbOffset;

                    if (cnkSound.LoadChunk)
                    {
                        sExtn = cnkSound.snd.sExtn
                        If ! bStore Then Return cnkSound.snd
                        SoundResources.Add(iResourceNumber, cnkSound.snd);
                    }
                }
            }
            If SoundResources.ContainsKey(iResourceNumber) Then Return SoundResources(iResourceNumber)

        }
        catch (Exception ex)
        {
            ErrMsg("GetSound error", ex);
        }
        finally
        {
            If ! bStreamOpen Then stmBlorb.Close()
        }
        return null;

    }



internal class SoundFile ' Temp, for now
    {
        public byte[] bytSound = {};
        public string sExtn;

        public bool Save(string sFilename)
        {
            private New IO.FileStream(sFilename, IO.FileMode.CreateNew) fs;
            fs.Write(bytSound, 0, bytSound.Length - 1);
            fs.Close();
        }

    }


internal class ImageFile
    {
        public byte[] bytImage = {};
        public string sExtn;

        Public ReadOnly Property Image As Image
            {
                get
                {
                private New IO.MemoryStream(bytImage) msImage;
                return new Bitmap(msImage);
            }
        }
    }


private abstract class Chunk
    {

        Public MustOverride Property ID As String

        private UInt32 iLength;
        Public Overridable Property Length As UInt32
            {
                get
                {
                return iLength;
            }
set(UInt32)
                iLength = value
            }
        }


        public virtual bool LoadChunk(int iStartPos = -1)
        {

            try
            {
                If iStartPos > -1 Then stmBlorb.Position = iStartPos

                ' First 4 bytes tell us what sort of chunk this is
                Dim bytID(3) As Byte
                iReadOffset += stmBlorb.Read(bytID, 0, 4);
                ID = UTF8.GetString(bytID)

                ' Next 4 bytes tell us the size of the chunk
                Dim bytSize(3) As Byte
                iReadOffset += stmBlorb.Read(bytSize, 0, 4);
                iLength = ByteToInt(bytSize)

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public void SkipPadding()
        {
            If stmBlorb.Position Mod 2 = 1 Then stmBlorb.Position += 1
        }


        public void WritePadding()
        {
            If stmBlorb.Position Mod 2 = 1 Then stmBlorb.WriteByte(CByte(0))
        }


        public New List<Chunk> Chunks;


        public virtual bool WriteChunk()
        {
            try
            {
                stmBlorb.Write(UTF8.GetBytes(ID), 0, 4) ' Chunk Type;
                stmBlorb.Write(IntToByte(Length), 0, 4) ' Size;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }


private class SkipChunk
    {
        Inherits Chunk;

        private bool bKnown = true;
        public void New(bool bKnown = true)
        {
            Me.bKnown = bKnown;
        }

        private string sID;
        Public Overrides Property ID As String
            {
                get
                {
                return sID;
            }
set(String)
                sID = value
            }
        }

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            if (Not bKnown)
            {
#if DEBUG
                ErrMsg("Skipping unknown chunk type """ + ID + """");
#endif
            }


            stmBlorb.Position += Me.Length;

            SkipPadding();

            return true;
        }
    }


private class FrontispieceChunk
    {
        Inherits Chunk;

        Public Overrides Property ID As String
            {
                get
                {
                return "Fspc";
            }
set(String)
                If value <> "Fspc" Then Throw New Exception("Bad ID in Frontispiece Chunk: " + value)
            }
        }


        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            ' Number of a Pict resource
            Dim bytSize(3) As Byte
            iReadOffset += stmBlorb.Read(bytSize, 0, 4);
            Frontispiece = CInt(ByteToInt(bytSize))

            SkipPadding();

            return true;

        }


        public override bool WriteChunk()
        {

            if (Frontispiece > -1)
            {
                Length = CUInt(4)
                If ! MyBase.WriteChunk Then Return false

                stmBlorb.Write(IntToByte(CUInt(Frontispiece)), 0, 4) ' Resource Number;

                WritePadding();
            }
            return true;

        }

    }


private class MetaDataChunk
    {
        Inherits Chunk;

        Public Overrides Property ID As String
            {
                get
                {
                return "IFmd";
            }
set(String)
                If value <> "IFmd" Then Throw New Exception("Bad ID in Metadata Chunk: " + value)
            }
        }

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            Dim bytMeta(CInt(Length) - 1) As Byte
            iReadOffset += stmBlorb.Read(bytMeta, 0, CInt(Length));
            private string sXML = UTF8.GetString(bytMeta);

            private New XmlDocument xmlMeta;
            xmlMeta.LoadXml(sXML);
            MetaData = xmlMeta

            SkipPadding();

            return true;
        }

        public override bool WriteChunk()
        {

            if (MetaData IsNot null)
            {
                Length = CUInt(UTF8.GetBytes(MetaData.OuterXml).Length) ' UTC bytes may be different from character count
                If ! MyBase.WriteChunk Then Return false

                stmBlorb.Write(UTF8.GetBytes(MetaData.OuterXml), 0, CInt(Length));

                WritePadding();
            }
            return true;

        }

    }


private class DataChunk
    {
        Inherits Chunk;

        private string sID;
        Public Overrides Property ID As String
            {
                get
                {
                If sID == null Then sID = "TEXT"
                return sID;
            }
set(String)
                switch (value)
                {
                    case "TEXT":
                    case "BINA":
                        {
                        sID = value
                    default:
                        {
                        throw New Exception("Bad ID in Data Resource Chunk: " & value)
                }
            }
        }

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            Dim bytData(CInt(Length) - 1) As Byte
            iReadOffset += stmBlorb.Read(bytData, 0, CInt(Length));
            private string sData = UTF8.GetString(bytData);
            DataType = sData.Substring(0, 4)
            Data = sData.Substring(4)

            SkipPadding();

            switch (DataType)
            {
                case "RLAY":
                    {
                    ' Restore Runner Layout
#if Runner AndAlso Not www AndAlso Not Mono
                    private string sIFID = "";
                    if (MetaData IsNot null)
                    {
                        sIFID = MetaData.GetElementsByTagName("ifid").Item(0).InnerText
                        If sIFID <> "" Then sIFID = "-" + sIFID
                    }
                    private string sXMLFile = DataPath(true) + "RunnerLayout" + sIFID + ".xml";
#else
                    private string sXMLFile = DataPath(true) + "RunnerLayout.xml";
#endif

                    if (Not IO.File.Exists(sXMLFile))
                    {
                        'If sLocalDataPath IsNot Nothing AndAlso Not IO.Directory.Exists(sLocalDataPath) Then IO.Directory.CreateDirectory(sLocalDataPath)
                        IO.File.WriteAllText(sXMLFile, Data);
                        '#If Runner AndAlso Not www AndAlso Not Mono Then
                        '                        If fRunner IsNot Nothing Then
                        '                            fRunner.RestoreLayout()
                        '                            Application.DoEvents()
                        '                        Else
                        '                            UserSession.bRequiresRestoreLayout = True
                        '                        End If
                        '#End If
                    }
            }

            return true;
        }

        public override bool WriteChunk()
        {

            if (DataType <> "")
            {
                private byte[] bytData = UTF8.GetBytes(DataType + Data);
                Length = CUInt(bytData.Length) ' UTC bytes may be different from character count
                If ! MyBase.WriteChunk Then Return false

                stmBlorb.Write(bytData, 0, CInt(Length));

                WritePadding();
            }
            return true;

        }

    }


private class ExecResourceChunk
    {
        Inherits Chunk;

        private string sID;
        Public Overrides Property ID As String
            {
                get
                {
                return sID;
            }
set(String)
                switch (value)
                {
                    case "ZCOD":
                    case "GLUL":
                    case "TAD2":
                    case "TAD3":
                    case "HUGO":
                    case "ALAN":
                    case "ADRI":
                    case "LEVE":
                    case "AGT ":
                    case "MAGS":
                    case "ADVS":
                    case "EXEC":
                        {
                        sID = value
                    default:
                        {
                        throw New Exception("Bad ID in Exec Resource Chunk: " & value)
                }
            }
        }

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            clsBlorb.ExecType = sID;

            Dim bytExec(CInt(Length) - 1) As Byte
            iReadOffset += stmBlorb.Read(bytExec, 0, CInt(Length));
            clsBlorb.ExecResource = bytExec;
            SkipPadding();

            return true;
        }

        public override bool WriteChunk()
        {

            if (ExecResource IsNot null)
            {
                If ! MyBase.WriteChunk Then Return false

                stmBlorb.Write(ExecResource, 0, ExecResource.Length);

                WritePadding();
            }
            return true;

        }

    }


private class PictResourceChunk
    {
        Inherits Chunk;

        private string sID;
        Public Overrides Property ID As String
            {
                get
                {
                if (img IsNot null)
                {
                    switch (img.sExtn)
                    {
                        case "jpeg":
                        case "jpg":
                            {
                            return "JPEG";
                        case "png":
                            {
                            return "PNG ";
                        case "gif":
                            {
                            return "GIF " ' Not valid Blorb;
                        default:
                            {
                            ErrMsg("Blorb does not support " + img.sExtn + " format");
                    }
                    return null;
                Else
                    return sID;
                }
            }
set(String)
                switch (value)
                {
                    case "PNG ":
                    case "JPEG":
                    case "GIF ":
                        {
                        sID = value
                    default:
                        {
                        throw New Exception("Bad ID in Picture Resource Chunk: " & value)
                }
            }
        }

        'Public img As Image
        public ImageFile ' As Byte[] img;


        public void SetImage(string sImage)
        {
            if (IO.File.Exists(sImage))
            {
                Length = CUInt(FileLen(sImage))
                private New IO.FileStream(sImage, IO.FileMode.Open, IO.FileAccess.Read) fs;
                img = New ImageFile
                ReDim img.bytImage(CInt(Length) - 1);
                fs.Read(img.bytImage, 0, CInt(Length));
                img.sExtn = IO.Path.GetExtension(sImage).ToLower.Substring(1);
                fs.Close();
                'Dim img As Image = Image.FromFile(sImage) ' Bitmap.FromFile(sImage)
                'Me.img = img
                'Select Case img.sExtn
                '    Case ".jpeg", ".jpg"
                '        ID = "JPEG"
                '    Case ".png"
                '        ID = "PNG "
                'End Select
                'Length = CUInt(FileLen(sImage))
            }
        }


        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            Dim bytPict(CInt(Length) - 1) As Byte
            iReadOffset += stmBlorb.Read(bytPict, 0, CInt(Length));

            private string sExtn = "";
            switch (ID)
            {
                case "GIF ":
                    {
                    sExtn = "gif" ' Not valid Blorb
                case "PNG ":
                    {
                    sExtn = "png"
                case "JPEG":
                    {
                    sExtn = "jpg"
            }

            img = New ImageFile
            img.bytImage = bytPict;
            img.sExtn = sExtn;

            'Dim msImage As New IO.MemoryStream(bytPict)
            'img = New Bitmap(msImage)
            'Select Case ID
            '    Case "PNG "
            '        img.sExtn = "png"
            '    Case "JPEG"
            '        img.sExtn = "jpg"
            'End Select

            SkipPadding();

            return true;
        }


        public override bool WriteChunk()
        {
            If ! MyBase.WriteChunk Then Return false

            'Dim format As Drawing.Imaging.ImageFormat = Drawing.Imaging.ImageFormat.Jpeg
            'Select ID
            '    Case "PNG "
            '        format = Drawing.Imaging.ImageFormat.Png
            '    Case "JPEG"
            '        format = Drawing.Imaging.ImageFormat.Jpeg
            'End Select
            'img.Save(stmBlorb, format)
            stmBlorb.Write(img.bytImage, 0, img.bytImage.Length);

            WritePadding();
            return true;

        }

    }


private class SoundResourceChunk
    {
        Inherits Chunk;

        private string sID;
        Public Overrides Property ID As String
            {
                get
                {
                if (snd IsNot null)
                {
                    switch (snd.sExtn)
                    {
                        case "mp3":
                            {
                            return "MP3 " ' Not valid Blorb;
                        case "wav":
                            {
                            return "WAVE" ' Not valid Blorb;
                        case "mid":
                            {
                            return "MIDI" ' Not valid Blorb;
                        case "aiff":
                        case "aif":
                            {
                            return "AIFF";
                        case "ogg":
                            {
                            return "OGGV";
                        case "mod":
                            {
                            return "MOD ";
                    }
                }
                return sID;
            }
set(String)
                switch (value)
                {
                    case "AIFF":
                    case "OGGV":
                    case "MOD ":
                        {
                        sID = value
                    case "MP3 ":
                    case "WAVE":
                    case "MIDI":
                        {
                        sID = value ' Tho not valid Blorb
                    default:
                        {
                        throw New Exception("Bad ID in Sound Resource Chunk: " & value)
                }
            }
        }

        public SoundFile snd;


        public void SetSound(string sSound)
        {
            try
            {
                if (IO.File.Exists(sSound))
                {
                    Length = CUInt(FileLen(sSound))
                    private New IO.FileStream(sSound, IO.FileMode.Open, IO.FileAccess.Read) fs;
                    snd = New SoundFile
                    ReDim snd.bytSound(CInt(Length) - 1);
                    fs.Read(snd.bytSound, 0, CInt(Length));
                    snd.sExtn = IO.Path.GetExtension(sSound).ToLower.Substring(1);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                ErrMsg("Error storing sound", ex);
            }
        }


        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            Dim bytSound(CInt(Length) - 1) As Byte
            iReadOffset += stmBlorb.Read(bytSound, 0, CInt(Length));

            'ReDim Preserve Blorb.PictResources(Blorb.PictResources.Length)
            snd = New SoundFile
            'Dim snd As New SoundFile
            snd.bytSound = bytSound;
            'Blorb.SoundResources.Add(snd)

            With snd 'Blorb.SoundResources(Blorb.PictResources.Length - 1);
                private New IO.MemoryStream(bytSound) msImage;
                switch (ID)
                {
                    case "AIFF":
                        {
                        .sExtn = "aif";
                    case "OGGV":
                        {
                        .sExtn = "ogg";
                    case "MOD ":
                        {
                        .sExtn = "mod";
                    case "MP3 ":
                        {
                        .sExtn = "mp3";
                    case "MIDI":
                        {
                        .sExtn = "mid";
                    case "WAVE":
                        {
                        .sExtn = "wav";
                }
            }

            SkipPadding();

            return true;
        }

        public override bool WriteChunk()
        {
            If ! MyBase.WriteChunk Then Return false

            stmBlorb.Write(snd.bytSound, 0, snd.bytSound.Length);

            WritePadding();
            return true;

        }

    }


private class ResourceIndexChunk
    {
        Inherits Chunk;

        Public Overrides Property ID As String
            {
                get
                {
                return "RIdx";
            }
set(String)
                If value <> "RIdx" Then Throw New Exception("Bad ID in Resource Index Chunk: " + value)
            }
        }

        'Private iNumberOfResources As Integer
        Public ReadOnly Property NumberOfResources As Integer
            {
                get
                {
                return clsBlorb.ResourceIndex.Count;
            }
        }


        Public Overrides Property Length As UInteger
            {
                get
                {
                return CUInt(4 + (12 * NumberOfResources));
            }
set(UInteger)
                MyBase.Length = value;
            }
        }


internal class ResourceIndex
        {

            private string sUsage;
            public string Usage
                {
                    get
                    {
                    return sUsage;
                }
set(String)
                    switch (value)
                    {
                        case "Pict":
                        case "Snd ":
                        case "Exec":
                            {
                            sUsage = value
                        default:
                            {
                            throw New Exception("Unknown Resource Usage: " & value)
                    }
                }
            }

            private UInt32 ' Number of resource iNumber;
            public UInt32 Number
                {
                    get
                    {
                    return iNumber;
                }
set(UInt32)
                    iNumber = value
                }
            }

            private UInt32 ' Starting position of resource iStart;
            public UInt32 Start
                {
                    get
                    {
                    return iStart;
                }
set(UInt32)
                    iStart = value
                }
            }
        }

        'Friend Resources() As ResourceIndex

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            ' Next 4 bytes tell us the number of resources in the index
            Dim bytNum(3) As Byte
            iReadOffset += stmBlorb.Read(bytNum, 0, 4);
            private int iNumberOfResources = CInt(ByteToInt(bytNum));

            'ReDim Resources(CInt(NumberOfResources - 1))

            for (int iResource = 0; iResource <= iNumberOfResources - 1; iResource++)
            {

                private string sKey;
                'Resources(iResource) = New ResourceIndex
                'With Resources(iResource)
                Dim bytUsage(3) As Byte
                iReadOffset += stmBlorb.Read(bytUsage, 0, 4);
                '.Usage = UTF8.GetString(bytUsage)
                sKey = UTF8.GetString(bytUsage)

                Dim btyNumber(3) As Byte
                iReadOffset += stmBlorb.Read(btyNumber, 0, 4);
                '.Number = ByteToInt(btyNumber)
                sKey &= ByteToInt(btyNumber);

                Dim bytStart(3) As Byte
                iReadOffset += stmBlorb.Read(bytStart, 0, 4);
                '.Start = ByteToInt(bytStart)

                clsBlorb.ResourceIndex.Add(sKey, ByteToInt(bytStart));
                'End With
            Next;

            SkipPadding();

            return true;
        }


        public override bool WriteChunk()
        {
            If ! MyBase.WriteChunk Then Return false

            stmBlorb.Write(IntToByte(CUInt(clsBlorb.ResourceIndex.Count)), 0, 4);

            For Each sKey As String In clsBlorb.ResourceIndex.Keys
                stmBlorb.Write(UTF8.GetBytes(sKey.Substring(0, 4)), 0, 4);
                stmBlorb.Write(IntToByte(CUInt(sKey.Substring(4))), 0, 4);
                stmBlorb.Write(IntToByte(clsBlorb.ResourceIndex(sKey)), 0, 4);
            Next;

            return true;

        }

    }


private class FormChunk
    {
        Inherits Chunk;

        Public Overrides Property ID As String
            {
                get
                {
                return "FORM";
            }
set(String)
                If value <> "FORM" Then Throw New Exception("Bad ID in Form Chunk: " + value)
            }
        }

        Public Overrides Property Length As UInteger
            {
                get
                {
                private UInt32 iLength = 0;
                For Each c As Chunk In Chunks
                    iLength += c.Length;
                Next;
                return iLength;
            }
set(UInteger)
                MyBase.Length = value;
            }
        }
        private string sSubTypeID;
        public string SubTypeID
            {
                get
                {
                return sSubTypeID;
            }
set(String)
                sSubTypeID = value
            }
        }

        public ResourceIndexChunk cnkResourceIndex;

        public override bool LoadChunk(int iStartPos = -1)
        {
            If ! MyBase.LoadChunk(iStartPos) Then Return false

            ' Next 4 bytes tell us the FORM type
            Dim bytID(3) As Byte
            iReadOffset += stmBlorb.Read(bytID, 0, 4);
            SubTypeID = UTF8.GetString(bytID)

            cnkResourceIndex = New ResourceIndexChunk
            If ! cnkResourceIndex.LoadChunk() Then Return false

            private long iStreamLength = stmBlorb.Length;
            If bEXE Then iStreamLength -= 6
            while (stmBlorb.Position < iStreamLength)
            {
                private Chunk cnk = null;

                ' Peek at the next 4 bytes to work out what chunk type it is
                Dim bytNext(3) As Byte
                iReadOffset = CInt(stmBlorb.Position)
                iReadOffset += stmBlorb.Read(bytNext, 0, 4);
                switch (UTF8.GetString(bytNext))
                {
                    case "ZCOD":
                    case "GLUL":
                    case "TAD2":
                    case "TAD3":
                    case "HUGO":
                    case "ALAN":
                    case "ADRI":
                    case "LEVE":
                    case "AGT ":
                    case "MAGS":
                    case "ADVS":
                    case "EXEC":
                        {
                        cnk = New ExecResourceChunk
                    case "GIF ":
                    case "PNG ":
                    case "JPEG":
                        {
                        cnk = New SkipChunk 'PictResourceChunk
                    case "AIFF":
                    case "OGGV":
                    case "MOD ":
                    case "MP3 ":
                    case "WAVE":
                    case "MIDI":
                        {
                        cnk = New SkipChunk 'SoundResourceChunk
                    case "IFmd":
                        {
                        cnk = New MetaDataChunk
                    case "Fspc":
                        {
                        cnk = New FrontispieceChunk
                    case "Plte":
                    case "IFhd" ' Colour Palette:
                    case Game Identifier:
                        {
                        cnk = New SkipChunk ' Just ignore them
                    case "TEXT":
                    case "BINA":
                        {
                        cnk = New DataChunk
                    default:
                        {
                        cnk = New SkipChunk(False)
                }
                If cnk == null || ! cnk.LoadChunk(iReadOffset - 4) Then Return false
            }

            SkipPadding();

            return true;
        }


        public override bool WriteChunk()
        {

            ' Gotta write the resources to the index so we can calculate the size of the index resource
            private UInt32 iResource = 0;
            For Each c As Chunk In Chunks
                private string sKey = "";
                switch (true)
                {
                    case TypeOf c Is ExecResourceChunk:
                        {
                        sKey = "Exec"
                    case TypeOf c Is PictResourceChunk:
                        {
                        sKey = "Pict"
                    case TypeOf c Is SoundResourceChunk:
                        {
                        sKey = "Snd "
                }
                if (sKey <> "")
                {
                    clsBlorb.ResourceIndex.Add(sKey + iResource, 0);
                    iResource = CUInt(iResource + 1)
                }
            Next;

            ' Then write out the resource lengths
            private UInt32 iOffset = 12;
            iResource = 0
            For Each c As Chunk In Chunks
                iOffset = CUInt(iOffset) ' + 8)
                private string sKey = "";
                switch (true)
                {
                    case TypeOf c Is ExecResourceChunk:
                        {
                        sKey = "Exec"
                    case TypeOf c Is PictResourceChunk:
                        {
                        sKey = "Pict"
                    case TypeOf c Is SoundResourceChunk:
                        {
                        sKey = "Snd "
                }
                if (sKey <> "")
                {
                    clsBlorb.ResourceIndex(sKey + iResource) = iOffset;
                    iResource = CUInt(iResource + 1)
                }
                iOffset = CUInt(iOffset + c.Length + 8)
                If iOffset Mod 2 = 1 Then iOffset = CUInt(iOffset + 1)
            Next;

            If ! MyBase.WriteChunk() Then Return false
            stmBlorb.Write(UTF8.GetBytes("IFRS"), 0, 4);

            For Each c As Chunk In Chunks
                If ! c.WriteChunk() Then Return false
            Next;


            WritePadding();
            return true;

        }
    }



    private void ClearBlorb()
    {
        ExecType = Nothing
        ExecResource = Nothing
        ImageResources.Clear();
        SoundResources.Clear();
        ResourceIndex.Clear();
        MetaData = Nothing
        Frontispiece = -1
    }



    public bool LoadBlorb(IO.FileStream stmBlorb, string sFilename, long BlorbOffset = 0)
    {

        try
        {
            ClearBlorb();
            clsBlorb.BlorbOffset = BlorbOffset;
            clsBlorb.sFilename = sFilename;
            clsBlorb.stmBlorb = stmBlorb;
            iReadOffset = 0
            cnkFORM = New FormChunk
            return cnkFORM.LoadChunk();
        }
        finally
        {
            'stmBlorb.Close()
            'stmBlorb.Dispose()
        }

    }


#if Generator
    ' Package current adventure into the Blorb
    public bool SaveBlorb(IO.FileStream stmBlorb)
    {

        ClearBlorb();
        cnkFORM = New FormChunk
        private New ResourceIndexChunk cnkResourceIndex;
        clsBlorb.stmBlorb = stmBlorb;

        cnkFORM.Chunks.Add(cnkResourceIndex);

        private int iResource = 0;

        ' Add Exec (without Babel)
        private New IO.MemoryStream stmExec;
        if (SaveFileToStream(stmExec, true, , true))
        {
            private New ExecResourceChunk cnkExec;
            ExecResource = stmExec.ToArray
            cnkExec.ID = "ADRI";
            cnkExec.Length = CUInt(ExecResource.Length);
            cnkFORM.Chunks.Add(cnkExec);
        }
        stmExec.Close();

        ' Add Images
        For Each sImage As String In Adventure.Images
            private New PictResourceChunk cnkImage;
            cnkImage.SetImage(sImage);
            cnkFORM.Chunks.Add(cnkImage);
        Next;

        ' Add Sound
        For Each sSound As String In Adventure.Sounds
            private New SoundResourceChunk cnkSound;
            cnkSound.SetSound(sSound);
            cnkFORM.Chunks.Add(cnkSound);
        Next;

        ' Add Metadata
        if (Adventure.BabelTreatyInfo IsNot null)
        {
            MetaData = New Xml.XmlDocument
            MetaData.LoadXml(Adventure.BabelTreatyInfo.ToString());
            private New MetaDataChunk cnkMeta;
            cnkFORM.Chunks.Add(cnkMeta);

            With Adventure.BabelTreatyInfo.Stories(0);
                ' Set Version/Release Date
                'With .Releases.Attached.Release
                '    .ReleaseDate = Date.Today
                '    '.Version += 1
                'End With

                ' Add Frontispiece
                if (Adventure.CoverFilename <> "" Then ' .Cover IsNot null && .Cover.Filename <> "")
                {
                    private New FrontispieceChunk cnkFront;
                    Frontispiece = 1 ' It's always the first resource after Exec, if it exists
                    cnkFORM.Chunks.Add(cnkFront);
                }
            }

        }

        ' Save Runner Layout
        private string sXMLFile = DataPath(true) + "RunnerLayout" + CurrentIFID() + ".xml";
        if (IO.File.Exists(sXMLFile))
        {
            DataType = "RLAY"
            Data = IO.File.ReadAllText(sXMLFile)
            private New DataChunk cnkData;
            cnkFORM.Chunks.Add(cnkData);
        }

        cnkFORM.WriteChunk();

        ' Increment version after exporting Blorb
        if (Adventure.BabelTreatyInfo IsNot null)
        {
            With Adventure.BabelTreatyInfo.Stories(0).Releases.Attached.Release;
                .Version += 1;
            }
        }

    }
#endif

    internal string CurrentIFID()
    {

        if (Adventure IsNot null && Adventure.BabelTreatyInfo IsNot null && Adventure.BabelTreatyInfo.Stories.Length = 1)
        {
            return "-" + Adventure.BabelTreatyInfo.Stories(0).Identification.IFID;
        }
        return "";

    }


    public bool OutputToFolder(string sFolder)
    {

        private IO.FileStream stmOutput = null;
        private IO.BinaryWriter bw;

        try
        {
            Cursor.Current = Cursors.WaitCursor;
            If ! stmBlorb.CanRead Then stmBlorb = New IO.FileStream(sFilename, IO.FileMode.Open)

            private int iResource = 0;
            For Each sKey As String In ResourceIndex.Keys
                iResource += 1;
                if (sKey.StartsWith("Pict"))
                {
                    private string sExtn = "";
                    private Image img = GetImage(CInt(sKey.Replace("Pict", "")), , sExtn);
                    if (img IsNot null)
                    {
                        img.Save(sFolder + IO.Path.DirectorySeparatorChar + "Image" + iResource + "." + sExtn);
                        img.Dispose();
                    }
                ElseIf sKey.StartsWith("Snd ") Then
                    private string sExtn = "";
                    private SoundFile snd = GetSound(CInt(sKey.Replace("Snd ", "")), , sExtn);
                    if (snd IsNot null)
                    {
                        snd.Save(sFolder + IO.Path.DirectorySeparatorChar + "Audio" + iResource + "." + sExtn);
                    }
                    'Dim snd As SoundFile = GetSound(CInt(sKey.Replace("Snd ", "")))
                    'stmOutput = New IO.FileStream(sFolder & IO.Path.DirectorySeparatorChar & "Sound" & iResource & "." & snd.sExtn, IO.FileMode.CreateNew)
                    'bw = New IO.BinaryWriter(stmOutput)
                    'bw.Write(snd.bytSound)
                    'bw.Close()
                    'stmOutput.Close()
                ElseIf sKey.StartsWith("Exec") Then
                    if (ExecResource IsNot null)
                    {
                        private string sExtn = "bin";
                        switch (ExecType)
                        {
                            case "ZCOD":
                                {
                                ' Hmm, which version...?
                                private string sVersion = "X";
                                if (MetaData IsNot null)
                                {
                                    ' SelectSingleNode is just not working, even after assigning a namespacemanager :-(
                                    For Each node As XmlNode In MetaData.DocumentElement.ChildNodes
                                        if (node.Name = "story")
                                        {
                                            For Each node2 As XmlNode In node.ChildNodes
                                                if (node2.Name = "releases")
                                                {
                                                    For Each node3 As XmlNode In node2.ChildNodes
                                                        if (node3.Name = "attached")
                                                        {
                                                            For Each node4 As XmlNode In node3.ChildNodes
                                                                if (node4.Name = "release")
                                                                {
                                                                    For Each node5 As XmlNode In node4.ChildNodes
                                                                        if (node5.Name = "compilerversion")
                                                                        {
                                                                            If node5.InnerXml.Length > 0 Then sVersion = node5.InnerXml(0)
                                                                        }
                                                                    Next;
                                                                }
                                                            Next;
                                                        }
                                                    Next;
                                                }
                                            Next;
                                        }
                                    Next;
                                }
                                sExtn = "z" & sVersion
                            case "GLUL":
                                {
                                sExtn = "ulx"
                            case "TAD2":
                                {
                                sExtn = "gam"
                            case "TAD3":
                                {
                                sExtn = "t3"
                            case "HUGO":
                                {
                                sExtn = "hex"
                            case "ALAN":
                                {
                                sExtn = "acd"
                            case "ADRI":
                                {
                                sExtn = "taf"
                            case "LEVE":
                                {
                            case "AGT ":
                                {
                                sExtn = "dat"
                            case "MAGS":
                                {
                            case "ADVS":
                                {
                                sExtn = "dat"
                            case "EXEC":
                                {
                        }
                        stmOutput = New IO.FileStream(sFolder & IO.Path.DirectorySeparatorChar & "Executable." & sExtn, IO.FileMode.CreateNew)
                        bw = New IO.BinaryWriter(stmOutput)
                        bw.Write(ExecResource);
                        bw.Close();
                        stmOutput.Close();
                    }
                Else
                    ErrMsg("Bad Resource key " + sKey);
                }
            Next;

            if (MetaData IsNot null)
            {
                private string sExtn = "xml";
                If MetaData.OuterXml.Contains("iFiction") Then sExtn = "iFiction"
                MetaData.Save(sFolder + IO.Path.DirectorySeparatorChar + "Metadata." + sExtn);
            }

            Cursor.Current = Cursors.Arrow;
            return true;

        }
        catch (IO.IOException exIO)
        {
            Cursor.Current = Cursors.Arrow;
            ErrMsg("Error expanding Blorb: " + exIO.Message);
            return false;
        }
        catch (Exception ex)
        {
            Cursor.Current = Cursors.Arrow;
            ErrMsg("Error expanding Blorb", ex);
            return false;
        }
        finally
        {
            stmBlorb.Close();
            stmBlorb.Dispose();
        }

    }


    private static Byte) As UInt32 ByteToInt(byt()
    {
        ' Reverse the bytes
        Dim bytRev(byt.Length - 1) As Byte
        for (int i = 0; i <= byt.Length - 1; i++)
        {
            bytRev(i) = byt(byt.Length - 1 - i);
        Next;
        return BitConverter.ToUInt32(bytRev, 0);
    }


    private static byte[] IntToByte(UInt32 int)
    {
        Dim bytRev(3) As Byte
        Dim byt(3) As Byte
        bytRev = BitConverter.GetBytes(int)
        for (int i = 0; i <= byt.Length - 1; i++)
        {
            byt(i) = bytRev(bytRev.Length - 1 - i);
        Next;
        return byt;
    }


}

}