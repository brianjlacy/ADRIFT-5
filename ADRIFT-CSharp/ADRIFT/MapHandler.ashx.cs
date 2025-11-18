using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ADRIFT
{

public class MapHandler
{
    Implements System.Web.IHttpHandler;
    Implements IRequiresSessionState;

    private void ProcessRequest(HttpContext context)
    {

        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hello World!")

        ' Write your handler implementation here.
        'Using image As System.Drawing.Image = GetImage(context.Request.QueryString("ID"))
        '    context.Response.ContentType = "image/jpeg"
        '    image.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        'End Using
        'If UserSession Is Nothing Then Exit Sub
        '  CType(Page.Master, Site).footer.InnerHtml = "Please be aware that v5 is not yet 100% compatible with v4.  For this reason, please play this game on <a href=""https://www.adrift.co/files/ADRIFT40r.zip"">v4 Runner</a>."
        If UserSession == null Then Exit Sub

        private int iHeight = MapHeight;
        private int iWidth = MapWidth;

        If iHeight = 0 Then iHeight = 500
        If iWidth = 0 Then iWidth = 400

        private string sId = context.Request.QueryString("ID");
        UserSession.Map.PaintMe(New Size(iWidth, iHeight));
        private System.Drawing.Image img = UserSession.Map.imgMap.Image 'Drawing.Image.FromFile("C:\Users\CampbellWild\Pictures\main.jpg");
        img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        'Dim ms As New IO.MemoryStream
        'img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)

        'Dim buffer As Byte() = ms.GetBuffer
        'ms.Close()

        'context.Response.ContentType = "image/jpeg"
        'context.Response.BinaryWrite(buffer)
        'context.Response.End()

    }

    private Boolean Implements IHttpHandler.IsReusable IsReusable { get; }
        {
            get
            {
            return false;
        }
    }

}
}