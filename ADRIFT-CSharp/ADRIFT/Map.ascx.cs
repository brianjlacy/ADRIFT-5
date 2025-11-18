using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class MapControl
{
    Inherits System.Web.UI.UserControl;

    protected void Page_Load(object sender, System.EventArgs e)
    {

        UserSession.Map.PaintMe(New Size(500, 300));
        private System.Drawing.Image img = UserSession.Map.imgMap.Image 'Drawing.Image.FromFile("C:\Users\CampbellWild\Pictures\main.jpg");
        private New IO.MemoryStream ms;
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

        private byte[] buffer = ms.GetBuffer;
        ms.Close();

        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(buffer);
        Response.End();

    }



}
}