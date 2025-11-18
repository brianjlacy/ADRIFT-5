using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ADRIFT
{


internal static class Internet
{

    public string OpenURL(string sInURL)
    {

        private int iP;
        private string sURL;
        private string sURLHost;
        private string sURLPath;

        Dim abyteReceive(1024) As Byte

        'The path parsing should be more robust ...
        iP = sInstr(UCase$(sInURL), "HTTP://")
        if (iP > 0)
        {
            sURL = Mid$(sInURL, iP + 7)
        Else
            sURL = sInURL
        }

        iP = sInstr(sURL, "/")
        if (iP > 0)
        {
            sURLHost = sMid(sURL, 1, iP - 1)
            sURLPath = sMid(sURL, iP)
        Else
            sURLHost = sURL
            sURLPath = "/"
        }

        'should be supporting HTTP 1.1
        private string s = "";
        private string sGet = "GET " + sURLPath + " HTTP/1.0" + vbCrLf + " Host: " + sURLHost + vbCrLf + "Connection: Close" + vbCrLf + vbCrLf;
        private Encoding asciiGet = Encoding.ASCII;
        Dim abyteGet() As Byte = asciiGet.GetBytes(sGet)

        try
        {
            private IPHostEntry hostentry = Dns.GetHostEntry(sURLHost) ' Dns.GetHostByName(sURLHost);
            private IPAddress hostadd = hostentry.AddressList(0);


            private IPEndPoint EPhost = new IPEndPoint(hostadd, 80);
            private Socket sockHTTP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sockHTTP.Connect(EPhost);

            If ! sockHTTP.Connected Then Return "Unable to connect to host: " + sURLHost

            sockHTTP.Send(abyteGet, abyteGet.Length, 0);

            private int iBytes = sockHTTP.Receive(abyteReceive, abyteReceive.Length, 0);
            s = ("HTML from " & sURL & "(" & hostadd.ToString & "):").Replace("|", ":") & vbCrLf
            s &= asciiGet.GetString(abyteReceive, 0, iBytes);

            Do While iBytes > 0
                iBytes = sockHTTP.Receive(abyteReceive, abyteReceive.Length, 0)
                s &= asciiGet.GetString(abyteReceive, 0, iBytes);
            Loop;

            sockHTTP.Close();
            sockHTTP = Nothing

        }
        catch (Exception ex)
        {
            return "Unable to connect to host: " + sURLHost + ", " + ex.Message;
        }

        return s;

    }


    public string URLEncode(string sURLCode)
    {
        URLEncode = sURLCode.Replace(vbCrLf, "<br>").Replace("%", "%25").Replace(" ", "%20").Replace("""", "%22").Replace("#", "%23").Replace("$", "%24").Replace("&", "%26").Replace("+", "%2B").Replace(",", "%2C").Replace("/", "%2F").Replace(":", "%3A").Replace(";", "%3B").Replace("<", "%3C").Replace("=", "%3D").Replace(">", "%3E").Replace(">", "%3E").Replace("?", "%3F").Replace("@", "%40").Replace("\", "%5C%5C")
    }

}

}