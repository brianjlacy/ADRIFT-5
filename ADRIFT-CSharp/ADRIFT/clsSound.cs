using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public static class mdlSound
{

    Dim Channels(7) As clsSoundInterface
    private clsSoundInterface SoundInterfaceDotNet;

    public New Queue<int> qToLoop;

    public bool StopSound(int iChannel = 1)
    {

        try
        {
            iChannel -= 1;
            If iChannel < 0 || iChannel >= Channels.Length Then Return false

            if (Channels(iChannel) IsNot null)
            {
                private clsSoundInterface SoundInterface = Channels(iChannel);
                SoundInterface.Stop();
                return true;
            Else
                return false;
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error stopping audio", ex);
            return false;
        }

    }


    public bool PauseSound(int iChannel = 1)
    {

        try
        {
#if Runner
            If ! UserSession.bSound Then Return false
#endif

            iChannel -= 1;
            If iChannel < 0 || iChannel >= Channels.Length Then Return false

            if (Channels(iChannel) IsNot null)
            {
                private clsSoundInterface SoundInterface = Channels(iChannel);
                SoundInterface.Pause();
                return true;
            Else
                return false;
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error pausing audio", ex);
            return false;
        }

    }


    public void ResetSounds()
    {
        if (Channels IsNot null)
        {
            for (int i = 0; i <= 7; i++)
            {
                if (Channels(i) IsNot null)
                {
                    Channels(i).Stop();
                    Channels(i) = null;
                }
            Next;
        }
    }



    public void TidyUpSounds()
    {
        For Each sTempSoundFile As String In htblTempLookup.Values
            if (IO.File.Exists(sTempSoundFile))
            {
                try
                {
                    IO.File.Delete(sTempSoundFile);
                }
                catch (Exception ex)
                {
                    ' ignore - might be in use
                }
            }
        Next;
    }


    public void RepeatMMLoops()
    {
        if (qToLoop.Count > 0)
        {
            private int iChannel = qToLoop.Dequeue;
            private clsSoundInterface MM = Channels(iChannel);
            if (MM IsNot null)
            {
                while (MM.IsPlaying)
                {
                    Application.DoEvents();
                    Threading.Thread.Sleep(1);
                }
            }
            PlaySound("", iChannel + 1, true);
        }
    }


    internal New Dictionary<string, string> htblTempLookup;
    public bool PlaySound(string sSoundFile, int iChannel = 1, bool bLooping = false)
    {

        try
        {
#if Runner
            If ! UserSession.bSound Then Return false
#endif

            iChannel -= 1;
            private clsSoundInterface SoundInterface = null;

            ' Try to use any existing interface
            if (Channels(iChannel) IsNot null)
            {
                SoundInterface = Channels(iChannel)
                If sSoundFile = "" Then sSoundFile = SoundInterface.SoundFile
                if (Not SoundInterface.CanPlayFile(sSoundFile))
                {
                    SoundInterface.Stop();
                    SoundInterface = Nothing
                }
            }

            private int iInterface = 0;
            while (SoundInterface Is null)
            {
                ' TODO - Allow disabling of each interface in Settings
                switch (iInterface)
                {
#if Not (www Or Mono)
                    case 0:
                        {
                        If SafeBool(GetSetting("ADRIFT", "Shared", "WinMM", "1")) Then SoundInterface = New MMSoundInterface(iChannel)
                    case 1:
                        {
                        If SafeBool(GetSetting("ADRIFT", "Shared", "DirectX", "1")) Then SoundInterface = New DirectXSoundInterface
#endif
                    case 2:
                        {
                        If SafeBool(GetSetting("ADRIFT", "Shared", "SoundPlayer", "1")) Then SoundInterface = New SoundPlayerSoundInterface
                    case 3:
                        {
                        ' No more interfaces.  I guess we have to settle for no sound. :-(
                        Exit While;
                }

                if (SoundInterface IsNot null)
                {
                    SoundInterface.SoundFile = sSoundFile;

                    if (SoundInterface.IsInitialised && SoundInterface.CanPlayFile(sSoundFile))
                    {
                        ' Yay, we have an interface
                    Else
                        SoundInterface = Nothing
                    }
                }

                iInterface += 1;
            }

            Channels(iChannel) = SoundInterface;

            if (SoundInterface IsNot null)
            {
                private string sLoadFile = sSoundFile;
                private string sExtn = "";

                if (IO.File.Exists(sLoadFile))
                {
                    sLoadFile = sSoundFile
                Else
                    if (htblTempLookup.ContainsKey(sSoundFile))
                    {
                        sLoadFile = htblTempLookup(sSoundFile)
                        sExtn = IO.Path.GetExtension(sSoundFile).ToUpper.Substring(1)
                    Else
                        if (Adventure.BlorbMappings IsNot null && Adventure.BlorbMappings.Count > 0)
                        {
                            private int iResource = 0;
                            If Adventure.BlorbMappings.ContainsKey(sSoundFile) Then iResource = Adventure.BlorbMappings(sSoundFile)
                            if (iResource > 0)
                            {
                                private clsBlorb.SoundFile sf = Blorb.GetSound(iResource, true);
                                private string tmpFile = IO.Path.GetTempFileName;
                                private New IO.FileStream(tmpFile, IO.FileMode.Create) stmOutput;
                                stmOutput.Write(sf.bytSound, 0, sf.bytSound.Length - 1);
                                stmOutput.Close();
                                htblTempLookup.Add(sSoundFile, tmpFile);
                                sLoadFile = tmpFile
                                sExtn = IO.Path.GetExtension(sSoundFile).ToUpper.Substring(1)
                            }
                        ElseIf Adventure.dVersion >= 4 && Adventure.dVersion < 5 && Adventure.dictv4Media.Count > 0 Then
                            private string tmpFile = IO.Path.GetTempFileName;
                            if (Getv4Audio(sSoundFile, tmpFile))
                            {
                                htblTempLookup.Add(sSoundFile, tmpFile);
                                sLoadFile = tmpFile
                                sExtn = IO.Path.GetExtension(sSoundFile).ToUpper.Substring(1)
                            }
                        }
                    }

                }

                if (IO.File.Exists(sLoadFile))
                {
                    try
                    {
                        With SoundInterface;
                            private bool bChanged = true;
                            try
                            {
                                if (.SoundFile <> sLoadFile)
                                {
                                    .SoundFile = sLoadFile;
                                    If sExtn <> "" Then SoundInterface._sExtension = sExtn
                                Else
                                    bChanged = False
                                }
                            }
                            catch (Exception ex)
                            {
                                DisplayError("Error loading audio: " + ex.Message + " - " + ex.InnerException.Message);
                            }
                            If .Looping <> bLooping Then .Looping = bLooping
                            try
                            {
                                ' If user tries to play a sound that is already playing, just leave as is
#if Runner
                                UserSession.DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.High, "Playing sound type " + .Extension + " on interface " + .Name);
#endif
                                If bChanged || ! .IsPlaying Then .Play()
                            }
                            catch (Exception ex)
                            {
                                DisplayError("Error playing audio: " + ex.Message + " - " + ex.InnerException.Message);
                            }
                        }
                    }
                    catch (Exception exA)
                    {
                        'ErrMsg("Error initialising audio", exA)
                        DisplayError("Unable to play audio.  DLL missing?");
                    }
                Else
                    DisplayError("Audio file """ + sLoadFile + """ not found.");
                }
            Else
                'ErrMsg("No interface found capable of playing audio file " & sSoundFile)
#if Runner
                UserSession.DebugPrint(ItemEnum.General, "", DebugDetailLevelEnum.Medium, "No interface found capable of playing audio file " + sSoundFile);
#endif
            }

        }
        catch (Exception ex)
        {
            ErrMsg("Error playing audio", ex);
        }

    }

}



public abstract class clsSoundInterface
{

    Public MustOverride ReadOnly Property IsInitialised As Boolean
    public bool Looping { get; set; }
    'Public MustOverride Property SoundFile As String
    public abstract void Play();
    Public MustOverride ReadOnly Property IsPlaying() As Boolean
    Public MustOverride Sub [Stop]()
    public abstract void Pause();
    public abstract bool CanPlayFile(string sFilename);
    Public MustOverride ReadOnly Property Name As String
    public bool Paused = false;

    private string _SoundFile;
    Friend Overridable Property SoundFile As String;
        {
            get
            {
            return _SoundFile;
        }
set(String)
            if (value <> _SoundFile)
            {
                _SoundFile = value
                _sExtension = ""
            }
        }
    }


    internal string _sExtension;
    internal string Extension()
    {

        try
        {
            if (_sExtension = "")
            {
                switch (Right(_SoundFile, 3).ToUpper)
                {
                    case "WAV":
                    case "MP3":
                    case "MID":
                    case "IDI":
                        {
                        _sExtension = Right(_SoundFile, 3).ToUpper
                    default:
                        {
                        ' Ok, have to look at the file signature to work it out
                        'Dim data() As Byte = IO.File.ReadAllBytes(_SoundFile)
                        private New IO.BinaryReader(New IO.StreamReader(_SoundFile).BaseStream) reader;
                        Dim data() As Byte = reader.ReadBytes(4)
                        reader.Close();

                        if (data.Length > 2 && data(0) = CByte(Asc("I")) && data(1) = CByte(Asc("D")) && data(2) = CByte(Asc("3")))
                        {
                            _sExtension = "MP3"
                        ElseIf data.Length > 1 && data(0) = 255 && data(1) >= 224 Then ' FF Ex / FF Fx
                            _sExtension = "MP3"
                        ElseIf data.Length > 3 && data(0) = 0 && data(1) = 0 && data(2) = 0 && data(3) = 0 Then ' Unsure about this...
                            _sExtension = "MP3"
                        ElseIf data.Length > 3 && ((data(0) = 82 && data(1) = 73 && data(2) = 70 && data(3) = 70) || _
                            (data(0) = 48 && data(1) = 38 && data(2) = 178 && data(3) = 117)) Then;
                            _sExtension = "WAV"
                        ElseIf data.Length > 3 && data(0) = 77 && data(1) = 84 && data(2) = 104 && data(3) = 100 Then
                            _sExtension = "MID"
                        ElseIf data.Length > 3 && data(0) = 79 && data(1) = 103 && data(2) = 103 && data(3) = 83 Then
                            _sExtension = "OGG"
                        Else
                            ErrMsg("Unable to identify file format");
                        }
                }
            }
        }
        catch (Exception ex)
        {
            ErrMsg("Error identifying sound file", ex);
        }

        return _sExtension;

    }

}


public class SoundPlayerSoundInterface
{
    Inherits clsSoundInterface;

    Private WithEvents Player As System.Media.SoundPlayer

    public void New()
    {
        Player = New System.Media.SoundPlayer
    }

    Public Overrides ReadOnly Property IsInitialised As Boolean
        {
            get
            {
            return Player IsNot null;
        }
    }

    'Public Overrides Property SoundFile As String
    '    Get
    '        Return Player.SoundLocation
    '    End Get
    '    Set(value As String)
    '        Player.SoundLocation = value
    '    End Set
    'End Property

    public override void Play()
    {
        try
        {
            If Player.SoundLocation = "" Then Player.SoundLocation = MyBase.SoundFile

            if (Looping)
            {
                Player.PlayLooping();
            Else
                Player.Play();
            }
            Paused = False
        }
        catch (Exception ex)
        {
            throw New Exception("SoundPlayer audio error", ex)
        }
    }

    Public Overrides Sub [Stop]()
        Player.Stop();
    }

    'Public Overrides Function CanPlayExtension(ByVal sExtn As String) As Boolean
    '    Return sExtn.ToUpper = ".WAV"
    'End Function

    public override void Pause()
    {
        Player.Stop();
        Paused = True
    }

    Public Overrides ReadOnly Property IsPlaying As Boolean
        {
            get
            {
            return false ' for now... Player.;
        }
    }

    public override bool CanPlayFile(string sFilename)
    {
        return MyBase.Extension = "WAV";
    }

    Public Overrides ReadOnly Property Name As String
        {
            get
            {
            return "SoundPlayer";
        }
    }

}

#if Not www
public class MMSoundInterface
{
    Inherits clsSoundInterface;

    Private Declare Auto Function mciSendString Lib "winmm.dll" Alias "mciSendString" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As UInt32, ByVal hwndCallback As IntPtr) As Int32
    Private Declare Unicode Function mciGetErrorString Lib "winmm.dll" Alias "mciGetErrorStringW" (ByVal errorCode As Int32, ByVal errorText As String, ByVal errorTextSize As Int32) As Int32
    private int iChannel = 0;

    public void New(int iChannel)
    {
        Me.iChannel = iChannel;
    }


    private bool _isPaused = false;
    public bool IsPaused { get; }
        {
            get
            {
            try
            {
                private string sBuffer = Space(128);
                Command("status audiofile" + iChannel + " mode", sBuffer, true);
                return sBuffer = "paused";
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


    private int Command(string sCommand, ref sResult As String = Nothing, bool bIgnoreErrors = false)
    {

        try
        {
            private string sBuffer = Space(255);
#if Runner
            private int iResult = mciSendString(sCommand, sBuffer, 255, fRunner.Handle) ' Has hung on this line... :-(;
#else
            private int iResult = mciSendString(sCommand, sBuffer, 255, IntPtr.Zero) ' Has hung on this line... :-(;
#endif

            sBuffer = sBuffer.Replace(Chr(0), "").Trim
            sResult = sBuffer
            private int iErrorCode = iResult;

            if (iErrorCode > 0)
            {
                sBuffer = Space(128)
                iResult = mciGetErrorString(iErrorCode, sBuffer, 128)
                sBuffer = sBuffer.Replace(Chr(0), "").Trim
                If ! bIgnoreErrors Then ErrMsg("Error " + iErrorCode + " carrying out command """ + sCommand + """: " + sBuffer)
            }

            return iErrorCode;

        }
        catch (Exception ex)
        {
            return -1;
        }

    }


    Public Overrides ReadOnly Property IsInitialised As Boolean
        {
            get
            {
            return Command("status", , true) = 292 ' Complains, but at least we know that's it working;
        }
    }

    Public Overrides ReadOnly Property IsPlaying As Boolean
        {
            get
            {
            try
            {
                private string sBuffer = Space(128);
                Command("status audiofile" + iChannel + " mode", sBuffer, true);
                return sBuffer = "playing";
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public override void Pause()
    {
        Command("pause audiofile" + iChannel, , true);
    }




    public override void Play()
    {

        if (IsPaused)
        {
            if (Extension() = "MID")
            {
                Command("play audiofile" + iChannel + " from " + Position);
            Else
                Command("resume audiofile" + iChannel);
            }
        Else
            [Stop]();
            If Looping Then qToLoop.Enqueue(iChannel)
            switch (Extension())
            {
                case "WAV":
                    {
                    Command("open """ + SoundFile + """ type waveaudio alias audiofile" + iChannel);
                    'Command("play audiofile" & iChannel & " from 0" & If(_wait, " wait", "").ToString & If(Looping, " notify repeat", "").ToString)
                    Command("play audiofile" + iChannel + " from 0" + If(_wait, " wait", "").ToString + If(Looping, " notify", "").ToString);
                case "MP3":
                    {
                    Command("open """ + SoundFile + """ type mpegvideo alias audiofile" + iChannel);
                    Command("play audiofile" + iChannel + " from 0" + If(_wait, " wait", "").ToString + If(Looping, " repeat", "").ToString);
                case "MID":
                case "IDI":
                    {
                    Command("open sequencer!" + SoundFile + " alias audiofile" + iChannel);
                    Command("play audiofile" + iChannel + " from 0" + If(_wait, " wait", "").ToString + If(Looping, " repeat", "").ToString);
                default:
                    {
                    throw New Exception("Sound file type " & Extension() & " not supported.")
                    Close();
            }
        }

    }

    ''' <summary>
    ''' Sets the audio file's time format via the mciSendString API.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    private int Milliseconds { get; }
        {
            get
            {
            private string buf = Space(255);
            mciSendString("set audiofile" + iChannel + " time format milliseconds", null, 0, IntPtr.Zero);
            mciSendString("status audiofile" + iChannel + " length", buf, 255, IntPtr.Zero);

            buf = Replace(buf, Chr(0), "") ' Get rid of the nulls, they muck things up

            if (buf = "")
            {
                return 0;
            Else
                return CInt(buf);
            }
        }
    }

    ''' <summary>
    ''' Gets the channels of the file via the mciSendString API.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    private int Channels { get; }
        {
            get
            {
            private string buf = Space(255);
            mciSendString("status audiofile" + iChannel + " channels", buf, 255, IntPtr.Zero);

            if (IsNumeric(buf) = true)
            {
                return CInt(buf);
            Else
                return -1;
            }
        }
    }

    private bool _wait = false;
    ''' <summary>
    ''' Halt the program until the .wav file is done playing.  Be careful, this will lock the entire program up until the
    ''' file is done playing.  It behaves as if the Windows Sleep API is called while the file is playing (and maybe it is, I don't
    ''' actually know, I'm just theorizing).  :P
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    public bool Wait { get; set; }
        {
            get
            {
            return _wait;
        }
set(ByVal Boolean)
            _wait = value
        }
    }

    ''' <summary>
    ''' Used for debugging purposes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    private string Debug { get; }
        {
            get
            {
            private string buf = Space(255);
            mciSendString("status audiofile" + iChannel + " channels", buf, 255, IntPtr.Zero);

            return Str(buf);
        }
    }

    ReadOnly Property Position As Long
        {
            get
            {
            private string sBuffer = Space(255);
            Command("status audiofile" + iChannel + " position", sBuffer);
            return CLng(sBuffer);
        }
    }

    'Private _SoundFile As String
    Friend Overrides Property SoundFile As String;
        {
            get
            {
            return MyBase.SoundFile ' _SoundFile;
        }
set(String)
            if (value <> MyBase.SoundFile)
            {
                [Stop]();
                MyBase.SoundFile = value;
            }
        }
    }

    Public Overrides Sub [Stop]()
        Command("stop audiofile" + iChannel, , true);
        If qToLoop.Count > 0 && qToLoop.Peek = iChannel Then qToLoop.Dequeue()
        Close();
    }

    private void Close()
    {
        Command("close audiofile" + iChannel, , true);
    }

    public override bool CanPlayFile(string sFilename)
    {
        switch (Extension())
        {
            case "OGG":
                {
                return false;
            case "WAV":
            case "MP3":
            case "MID":
                {
                return true;
            default:
                {
                return false;
        }
    }

    Public Overrides ReadOnly Property Name As String
        {
            get
            {
            return "WinMM";
        }
    }
}


public class DirectXSoundInterface
{
    Inherits clsSoundInterface;

    'Private _dev As Microsoft.DirectX.DirectSound.Device
    'Private _buffer As Microsoft.DirectX.DirectSound.SecondaryBuffer
    private Microsoft.DirectX.AudioVideoPlayback.Audio audio;

    public void New()
    {
        '_dev = New Microsoft.DirectX.DirectSound.Device
        '_dev.SetCooperativeLevel(fRunner.Handle, Microsoft.DirectX.DirectSound.CooperativeLevel.Priority)
    }

    'Public Overrides Function CanPlayExtension(sExtn As String) As Boolean
    '    Select Case sExtn.ToUpper
    '        Case ".OGG"
    '            Return False
    '        Case ".WAV", ".MP3", ".MID"
    '            Return True
    '        Case Else
    '            Return True ' At least until I know what it supports
    '    End Select
    'End Function

    Public Overrides ReadOnly Property IsInitialised As Boolean
        {
            get
            {
            'Return _dev IsNot Nothing
            return true ' audio IsNot null ' true;
        }
    }

    public override void Play()
    {
        try
        {
            If audio != null && ! audio.Paused Then [Stop]()
            if (audio Is null && SoundFile <> "")
            {
                try
                {
                    audio = New Microsoft.DirectX.AudioVideoPlayback.Audio(SoundFile, False)
                }
                catch (Exception ex)
                {
                    throw New Exception("DirectX audio error", ex)
                }
            }
            if (audio IsNot null)
            {
                If Looping Then AddHandler audio.Ending, AddressOf MusicEnds
                audio.Play();
                Paused = False
            }
        }
        catch (Exception ex)
        {
            throw New Exception("DirectX audio error (play)", ex)
        }
    }

    private void MusicEnds(object sender, System.EventArgs e)
    {
        audio.CurrentPosition = 0;
    }

    'Private _sSoundFile As String = Nothing
    'Public Overrides Property SoundFile As String
    '    Get
    '        Return _sSoundFile
    '    End Get
    '    Set(value As String)
    '        _sSoundFile = value
    '    End Set
    'End Property

    Public Overrides Sub [Stop]()
        try
        {
            if (audio IsNot null)
            {
                audio.Stop();
                If Looping Then RemoveHandler audio.Ending, AddressOf MusicEnds
                audio.Dispose();
            }
            audio = Nothing
        }
        catch (Exception ex)
        {
            throw New Exception("DirectX audio error (stop)", ex)
        }
    }

    public override void Pause()
    {
        try
        {
            if (audio.Paused)
            {
                audio.Play();
                Paused = False
            Else
                audio.Pause();
                Paused = True
            }
        }
        catch (Exception ex)
        {
            throw New Exception("DirectX audio error (pause)", ex)
        }
    }

    Public Overrides ReadOnly Property IsPlaying As Boolean
        {
            get
            {
            return audio IsNot null && audio.Playing && audio.CurrentPosition < audio.Duration;
        }
    }

    public override bool CanPlayFile(string sFilename)
    {
        switch (Extension())
        {
            case "OGG":
                {
                return false;
            case "WAV":
            case "MP3":
            case "MID":
                {
                return true;
            default:
                {
                return false;
        }
    }

    Public Overrides ReadOnly Property Name As String
        {
            get
            {
            return "DirectX";
        }
    }

}
#endif

}