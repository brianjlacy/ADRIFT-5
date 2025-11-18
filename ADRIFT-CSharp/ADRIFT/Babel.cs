using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ADRIFT
{


<System.SerializableAttribute(), _;
    System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://babel.ifarchive.org/protocol/iFiction/"), _;
    System.Xml.Serialization.XmlRootAttribute("ifindex", [Namespace]:="http://babel.ifarchive.org/protocol/iFiction/", _;
    IsNullable:=false)> _;
public class clsBabelTreatyInfo
{

    public void New()
    {
        sVersion = "1.0"
        ReDim oStories(0);
        oStories(0) = New clsStory;
    }


    public bool FromString(string sXML)
    {

        try
        {
            private New XmlSerializer(GetType(clsBabelTreatyInfo)) serializer;
            private byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sXML);
            private New System.IO.MemoryStream(bytes) ms;
            Adventure.BabelTreatyInfo = (clsBabelTreatyInfo)(serializer.Deserialize(ms));
            ms.Close();
            return true;
        }
        catch (Exception ex)
        {
            ErrMsg("FromString error parsing ifindex", ex);
            return false;
        }

    }


    Public Shadows Function ToString(Optional ByVal bIncludeXMLTag As Boolean = true) As String

        try
        {
            private New XmlSerializer(GetType(clsBabelTreatyInfo)) serializer;
            private New System.IO.MemoryStream ms;
            private New System.Xml.XmlTextWriter(ms, System.Text.Encoding.UTF8) xmlTextWriter;

            serializer.Serialize(xmlTextWriter, Me);

            ms = CType(xmlTextWriter.BaseStream, System.IO.MemoryStream)
            private string sXML = (new System.Text.UTF8Encoding).GetString(ms.ToArray);
            private int iOffset = 0;
            while (iOffset < sXML.Length && sXML(iOffset) <> "<"c)
            {
                iOffset += 1;
            }

            if (bIncludeXMLTag)
            {
                return sXML.Substring(iOffset);
            Else
                return sXML.Substring(iOffset).Replace("<?xml version=""1.0"" encoding=""utf-8""?>", "");
            }
        }
        catch (Exception ex)
        {
            ErrMsg("ToString error parsing ifindex", ex);
            return "";
        }

    }


    Private oStories() As clsStory
    <System.Xml.Serialization.XmlElementAttribute("story", GetType(clsStory))> _;
    public clsStory[] Stories { get; set; }
        {
            get
            {
            return oStories;
        }
set(ByVal Value As clsStory())
            oStories = Value
        }
    }


    private string sVersion;
    <System.Xml.Serialization.XmlAttributeAttribute("version")> _;
    public string Version { get; set; }
        {
            get
            {
            return sVersion;
        }
set(ByVal String)
            sVersion = value
        }
    }


public class clsStory
    {

        public void New()
        {
            Identification.GenerateNewIFID();
            Releases = New clsBabelTreatyInfo.clsStory.clsReleases
        }


        ' The <identification> section is mandatory. The content here identifies
        ' to which story file the metadata belongs. (This is necessary because
        ' the metadata may be held on some remote server, quite separate from
        ' the story file.)
public class clsIdentification
        {

            public void GenerateNewIFID()
            {
                With System.Reflection.Assembly.GetExecutingAssembly.GetName.Version;
                    sIFID = "ADRIFT-" & .Major & .Minor.ToString("00") & "-" & sRight(Guid.NewGuid.ToString, 32).ToUpper
                }
            }

            private string sIFID;
            <System.Xml.Serialization.XmlElement("ifid")> _;
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

            <System.Xml.Serialization.XmlElement("format")> _;
            public string Format { get; set; }
                {
                    get
                    {
                    return "adrift";
                }
set(ByVal String)
                    ' Ignore
                }
            }

            private int iBAFN;
            <System.Xml.Serialization.XmlElement("bafn")> _;
            public int BAFN { get; set; }
                {
                    get
                    {
                    return iBAFN;
                }
set(ByVal Integer)
                    iBAFN = value
                    If iBAFN <> 0 Then bBAFNSpecified = true
                }
            }

            private bool bBAFNSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool BAFNSpecified { get; set; }
                {
                    get
                    {
                    return bBAFNSpecified;
                }
set(ByVal Value As Boolean)
                    bBAFNSpecified = Value
                }
            }

        }
        <System.Xml.Serialization.XmlElement("identification")> _;
        public New clsIdentification Identification;

        ' The <bibliographic> section is mandatory.
public class clsBibliographic
        {

            <System.Xml.Serialization.XmlElement("title")> _;
            public string Title { get; set; }
                {
                    get
                    {
                    return Adventure.Title;
                }
set(ByVal String)
                    ' Ignore
                }
            }

            <System.Xml.Serialization.XmlElement("author")> _;
            public string Author { get; set; }
                {
                    get
                    {
                    return Adventure.Author;
                }
set(ByVal String)
                    ' Ignore
                }
            }

            ' Always reads this from the current culture, so can change if modified on a different machine
            private string sLanguage;
            <System.Xml.Serialization.XmlElement("language")> _;
            public string Language { get; set; }
                {
                    get
                    {
                    return Threading.Thread.CurrentThread.CurrentCulture.ToString();
                }
set(ByVal String)
                    ' Ignore
                }
            }

            private string sHeadline;
            <System.Xml.Serialization.XmlElement("headline")> _;
            public string Headline { get; set; }
                {
                    get
                    {
                    return sHeadline;
                }
set(ByVal String)
                    sHeadline = value
                }
            }

            private DateTime dtFirstPublished;
            <System.Xml.Serialization.XmlElement("firstpublished")> _;
            public DateTime FirstPublished { get; set; }
                {
                    get
                    {
                    return dtFirstPublished;
                }
set(ByVal DateTime)
                    dtFirstPublished = value
                    If value <> Date.MinValue Then bFirstPublishedSpecified = true
                }
            }

            private bool bFirstPublishedSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool FirstPublishedSpecified { get; set; }
                {
                    get
                    {
                    return bFirstPublishedSpecified;
                }
set(ByVal Value As Boolean)
                    bFirstPublishedSpecified = Value
                }
            }

            private string sGenre;
            <System.Xml.Serialization.XmlElement("genre")> _;
            public string Genre { get; set; }
                {
                    get
                    {
                    return sGenre;
                }
set(ByVal String)
                    sGenre = value
                }
            }

            private string sGroup;
            public string Group { get; set; }
                {
                    get
                    {
                    return sGroup;
                }
set(ByVal String)
                    sGroup = value
                }
            }

            private string sDescription;
            <System.Xml.Serialization.XmlElement("description")> _;
            public string Description { get; set; }
                {
                    get
                    {
                    return sDescription;
                }
set(ByVal String)
                    sDescription = value
                }
            }

            private string sSeries;
            public string Series { get; set; }
                {
                    get
                    {
                    return sSeries;
                }
set(ByVal String)
                    sSeries = value
                }
            }

            private int iSeriesNumber;
            public int SeriesNumber { get; set; }
                {
                    get
                    {
                    return iSeriesNumber;
                }
set(ByVal Integer)
                    iSeriesNumber = value
                    If iSeriesNumber > 0 Then bSeriesNumberSpecified = true
                }
            }

            private bool bSeriesNumberSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool SeriesNumberSpecified { get; set; }
                {
                    get
                    {
                    return bSeriesNumberSpecified;
                }
set(ByVal Value As Boolean)
                    bSeriesNumberSpecified = Value
                }
            }

public enum ForgivenessEnum
            {
                Merciful;
                Polite;
                Tough;
                Nasty;
                Cruel;
            }
            private ForgivenessEnum eForgiveness;
            <System.Xml.Serialization.XmlElement("forgiveness")> _;
            public ForgivenessEnum Forgiveness { get; set; }
                {
                    get
                    {
                    return eForgiveness;
                }
set(ByVal ForgivenessEnum)
                    eForgiveness = value
                    bForgivenessSpecified = True
                }
            }

            private bool bForgivenessSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool ForgivenessSpecified { get; set; }
                {
                    get
                    {
                    return bForgivenessSpecified;
                }
set(ByVal Value As Boolean)
                    bForgivenessSpecified = Value
                }
            }

        }
        <System.Xml.Serialization.XmlElement("bibliographic")> _;
        public New clsBibliographic Bibliographic;

        ' The <resources> tag is optional. This section, if present, details
        ' the other files (if any) which are intended to accompany the story
        ' file, and to be available to any player. By "other" is meant files
        ' which are not embedded in the story file. (So, for instance, pictures
        ' in a blorbed Z-machine story file do not count as "other".)
        <System.SerializableAttribute(), _;
            System.Xml.Serialization.XmlTypeAttribute([TypeName]:="resources", [Namespace]:="http://babel.ifarchive.org/protocol/iFiction/")> _;
public class clsResources
        {

public class clsAuxiliary
            {

                private string sLeafName;
                public string LeafName { get; set; }
                    {
                        get
                        {
                        return sLeafName;
                    }
set(ByVal String)
                        sLeafName = value
                    }
                }

                private string sDescription;
                public string Description { get; set; }
                    {
                        get
                        {
                        return sDescription;
                    }
set(ByVal String)
                        sDescription = value
                    }
                }

            }
            public New clsAuxiliary Auxiliary;

        }
        public clsResources Resources;

        ' The <contacts> tag is optional.
        <System.SerializableAttribute(), _;
            System.Xml.Serialization.XmlTypeAttribute([TypeName]:="contacts", [Namespace]:="http://babel.ifarchive.org/protocol/iFiction/")> _;
public class clsContacts
        {

            private string sURL;
            public string URL { get; set; }
                {
                    get
                    {
                    return sURL;
                }
set(ByVal String)
                    sURL = value
                }
            }

            private string sAuthorEmail;
            public string AuthorEmail { get; set; }
                {
                    get
                    {
                    return sAuthorEmail;
                }
set(ByVal String)
                    sAuthorEmail = value
                }
            }

        }
        public clsContacts Contacts;

        ' The <cover> tag is optional, except that it is mandatory for an
        ' iFiction record embedded in a story file which contains a cover
        ' image; and the information must, of course, be correct.

public class clsCover
        {

            internal System.Drawing.Image imgCoverArt;


            'Private sFilename As String
            '<System.Xml.Serialization.XmlIgnoreAttribute()> _
            'Public Property Filename() As String
            '    Get
            '        Return sFilename
            '    End Get
            '    Set(ByVal value As String)
            '        If sFormat = "" Then sFormat = sRight(value, 3)
            '        sFilename = value
            '    End Set
            'End Property

            ' This is required to be either "jpg" or "png". No other casings,
            ' spellings or image formats are permitted.
            private string sFormat;
            <System.Xml.Serialization.XmlElement("format")> _;
            public string Format { get; set; }
                {
                    get
                    {
                    If imgCoverArt != null Then Return sFormat Else Return null
                }
set(ByVal String)
                    if (value = "" || value = "jpg" || value = "png")
                    {
                        sFormat = value
                    Else
                        throw New Exception("Only jpg or png allowed!")
                    }
                }
            }

            <System.Xml.Serialization.XmlElement("height")> _;
            public int Height { get; set; }
                {
                    get
                    {
                    If imgCoverArt != null Then Return imgCoverArt.Height Else Return 0
                }
set(ByVal Integer)
                    ' Provided for serialization only
                }
            }

            private bool bHeightSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool HeightSpecified { get; set; }
                {
                    get
                    {
                    return imgCoverArt IsNot null;
                }
set(ByVal Value As Boolean)
                    ' Ignore
                }
            }

            <System.Xml.Serialization.XmlElement("width")> _;
            public int Width { get; set; }
                {
                    get
                    {
                    If imgCoverArt != null Then Return imgCoverArt.Width Else Return 0
                }
set(ByVal Integer)
                    ' Provided for serialization only
                }
            }

            private bool bWidthSpecified;
            <System.Xml.Serialization.XmlIgnoreAttribute()> _;
            public bool WidthSpecified { get; set; }
                {
                    get
                    {
                    return imgCoverArt IsNot null;
                }
set(ByVal Value As Boolean)
                    ' Ignore
                }
            }

        }
        <System.Xml.Serialization.XmlElement("cover")> _;
        public clsCover Cover;

public class clsADRIFT
        {

            public string Version { get; }
                {
                    get
                    {
                    return Application.ProductVersion;
                }
            }

        }
        public clsADRIFT ADRIFT;

        ' 5.11 - Releases
        '   Attached
        '   History
        <System.SerializableAttribute(), _;
           System.Xml.Serialization.XmlTypeAttribute([TypeName]:="releases", [Namespace]:="http://babel.ifarchive.org/protocol/iFiction/")> _;
public class clsReleases
        {

            public void New()
            {
                Attached = New clsBabelTreatyInfo.clsStory.clsReleases.clsAttached
            }


public class clsAttached
            {

                public void New()
                {
                    Release = New clsBabelTreatyInfo.clsStory.clsReleases.clsAttached.clsRelease
                }


public class clsRelease
                {

                    private int iVersion = 1;
                    <System.Xml.Serialization.XmlElement("version")> _;
                    public int Version { get; set; }
                        {
                            get
                            {
                            return iVersion;
                        }
set(ByVal Integer)
                            iVersion = value
                            If iVersion > 0 Then bVersionSpecified = true
                        }
                    }

                    private bool bVersionSpecified;
                    <System.Xml.Serialization.XmlIgnoreAttribute()> _;
                    public bool VersionSpecified { get; set; }
                        {
                            get
                            {
                            return bVersionSpecified;
                        }
set(ByVal Value As Boolean)
                            bVersionSpecified = Value
                        }
                    }

                    private DateTime dtReleaseDate;
                    <System.Xml.Serialization.XmlElement("releasedate")> _;
                    public string ReleaseDateXML { get; set; }
                        {
                            get
                            {
                            return ReleaseDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
                        }
set(ByVal String)
                            ReleaseDate = DateTime.Parse(value)
                        }
                    }

                    <System.Xml.Serialization.XmlIgnore()> _;
                    public DateTime ReleaseDate { get; set; }
                        {
                            get
                            {
#if Generator
                            return Date.Today;
#else
                            return dtReleaseDate;
#endif
                        }
set(ByVal DateTime)
                            dtReleaseDate = value
                        }
                    }

                    private string sCompiler;
                    <System.Xml.Serialization.XmlElement("compiler")> _;
                    public string Compiler { get; set; }
                        {
                            get
                            {
                            return "ADRIFT 5";
                        }
set(ByVal String)
                        }
                    }

                    private string sCompilerVersion;
                    <System.Xml.Serialization.XmlElement("compilerversion")> _;
                    public string CompilerVersion { get; set; }
                        {
                            get
                            {
                            return Application.ProductVersion;
                        }
set(ByVal String)
                        }
                    }

                }
                <System.Xml.Serialization.XmlElement("release")> _;
                public New clsRelease Release;

            }
            <System.Xml.Serialization.XmlElement("attached")> _;
            public New clsAttached Attached;

        }

        <System.Xml.Serialization.XmlElement("releases")> _;
        public clsReleases Releases;


        ' 5.12 - Colophon

        ' 5.13 - Annotation

        ' 5.14 - Examples

    }

}

}