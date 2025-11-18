using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsImage
{

public class ImageEventArgs
    {
        Inherits EventArgs;

        public SizeModeEnum SizeMode;
    }


    Public Event SizeModeChanged(ByVal o As clsImage, ByVal e As ImageEventArgs)

public enum SizeModeEnum
    {
        ActualSizeCentred = 0
        StretchedKeepAspect = 1
        StretchToFill = 2
    }
    private SizeModeEnum eSizeMode;

    private Bitmap bmpAnimation = null;
    private bool bAnimating = false;


    public SizeModeEnum SizeMode { get; set; }
        {
            get
            {
            return eSizeMode;
        }
set(ByVal SizeModeEnum)

            private Image imgSize = null;
            if (bmpAnimation IsNot null)
            {
                imgSize = bmpAnimation
            Else
                imgSize = imgGraphics.Image
            }
            if (imgSize Is null)
            {
                eSizeMode = value
                Exit Property;
            }

            private string sMode = "";
            switch (value)
            {
                case SizeModeEnum.ActualSizeCentred:
                    {
                    imgGraphics.Visible = false;
                    imgGraphics.Dock = DockStyle.Fill;
                    imgGraphics.SizeMode = PictureBoxSizeMode.CenterImage;
                    imgGraphics.Visible = true;
                    sMode = "Actual Size, Centred"

                case SizeModeEnum.StretchedKeepAspect:
                    {
                    imgGraphics.Visible = false;
                    imgGraphics.Dock = DockStyle.None;
                    imgGraphics.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (imgSize.Height / imgSize.Width > Me.Height / Me.Width)
                    {
                        imgGraphics.Top = 0;
                        imgGraphics.Height = Me.Height;
                        imgGraphics.Width = CInt(imgSize.Width * Me.Height / imgSize.Height);
                        imgGraphics.Left = CInt((Me.Width - imgGraphics.Width) / 2);
                    Else
                        imgGraphics.Left = 0;
                        imgGraphics.Width = Me.Width;
                        imgGraphics.Height = CInt(imgSize.Height * Me.Width / imgSize.Width);
                        imgGraphics.Top = CInt((Me.Height - imgGraphics.Height) / 2);
                    }
                    imgGraphics.Visible = true;
                    sMode = "Fit Window, Keep Aspect"

                case SizeModeEnum.StretchToFill:
                    {
                    imgGraphics.Visible = false;
                    imgGraphics.Dock = DockStyle.Fill;
                    imgGraphics.SizeMode = PictureBoxSizeMode.StretchImage;
                    imgGraphics.Visible = true;
                    sMode = "Stretch to Window"

            }

            lblMode.AutoSize = false;
            lblMode.Width = imgGraphics.Width;
            lblMode.Location = New Point(0, 5) 'imgGraphics.Height - 20);

            if (value <> eSizeMode)
            {
                lblMode.Text = "Mode: " + sMode;
                lblMode.Parent = imgGraphics;
                lblMode.Visible = true;
                tmrLabel.Stop();
                tmrLabel.Interval = 1000;
                tmrLabel.Enabled = true;
                tmrLabel.Start();
                eSizeMode = value
                SharedModule.iImageSizeMode = CInt(eSizeMode);

                private New ImageEventArgs ea;
                ea.SizeMode = value;

                RaiseEvent SizeModeChanged(Me, ea);
            }

        }
    }


    private PictureBox pbxURL;
    Public Shadows Sub Load(ByVal url As String)

        StopAnimating();
        if (Adventure.dVersion >= 4 && Adventure.dVersion < 5 && Adventure.dictv4Media.Count > 0)
        {
            ' Grab the image directly from the TAF
            imgGraphics.Image = Getv4Image(url);
        Else
            private Bitmap imgTemp;
            if (url.Contains("://"))
            {
                If pbxURL == null Then pbxURL = New PictureBox
                pbxURL.Load(url);
                imgTemp = New Bitmap(pbxURL.Image)
            Else
                try
                {
                    imgTemp = New Bitmap(url)
                }
                catch (Exception ex)
                {
                    private Image img = Image.FromFile(url, false);
                    imgTemp = New Bitmap(img)
                }
            }

            private New Imaging.FrameDimension(imgTemp.FrameDimensionsList(0)) dimension;
            if (imgTemp.GetFrameCount(dimension) > 1)
            {
                imgGraphics.Image = null;
                bmpAnimation = imgTemp
                AnimateImage();
            Else
                imgGraphics.Image = imgTemp;
            }
        }
        SizeMode = SizeMode ' Just in case it was set before the image was loaded

    }


    public System.Drawing.Image Image { get; set; }
        {
            get
            {
            return imgGraphics.Image;
        }
set(ByVal System.Drawing.Image)
            imgGraphics.Image = value;
            SizeMode = SizeMode  ' Just in case it was set before the image was loaded
        }
    }


    private void StopAnimating()
    {
        if (bAnimating)
        {
            'RemoveHandler addressof OnFrameChanged
            ImageAnimator.StopAnimate(bmpAnimation, New eventhandler(AddressOf OnFrameChanged));
            bAnimating = False
            bmpAnimation = Nothing
        }
    }


    private void AnimateImage()
    {

        if (Not bAnimating)
        {
            ImageAnimator.Animate(bmpAnimation, New EventHandler(AddressOf OnFrameChanged));
            bAnimating = True
        }

    }


    private void OnFrameChanged(System.Object sender, System.EventArgs e)
    {
        imgGraphics.Invalidate();
    }


    private void imgGraphics_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {

        ' Begin the animation
        'AnimateImage()

        if (bAnimating)
        {
            ' Get the next frame ready for rendering
            ImageAnimator.UpdateFrames();

            private Rectangle rect;

            switch (SizeMode)
            {
                case SizeModeEnum.ActualSizeCentred:
                    {
                    rect = New Rectangle(CInt((imgGraphics.Width - bmpAnimation.Width) / 2), CInt((imgGraphics.Height - bmpAnimation.Height) / 2), bmpAnimation.Width, bmpAnimation.Height)
                case SizeModeEnum.StretchToFill:
                case SizeModeEnum.StretchedKeepAspect:
                    {
                    rect = New Rectangle(0, 0, imgGraphics.Width, imgGraphics.Height)
            }

            ' Draw the next frame in the animation
            e.Graphics.DrawImage(bmpAnimation, rect);

        }

    }

    'Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
    '    MyBase.OnPaint(e)
    'End Sub


    private void imgGraphics_Click(System.Object sender, System.EventArgs e)
    {

        switch (SizeMode)
        {
            case SizeModeEnum.ActualSizeCentred:
                {
                SizeMode = SizeModeEnum.StretchedKeepAspect
            case SizeModeEnum.StretchedKeepAspect:
                {
                SizeMode = SizeModeEnum.StretchToFill
            case SizeModeEnum.StretchToFill:
                {
                SizeMode = SizeModeEnum.ActualSizeCentred
        }

    }


    private void Image_Resize(object sender, System.EventArgs e)
    {
        If SizeMode = SizeModeEnum.StretchedKeepAspect Then SizeMode = SizeModeEnum.StretchedKeepAspect ' to redraw
    }



    private void tmrLabel_Tick(object sender, System.EventArgs e)
    {
        tmrLabel.Enabled = false;
        lblMode.Visible = false;
    }


    public Color BackColour { get; set; }
        {
            get
            {
            return imgGraphics.BackColor;
        }
set(ByVal Color)
            imgGraphics.BackColor = value;
            Me.BackColor = value;
        }
    }

}

}