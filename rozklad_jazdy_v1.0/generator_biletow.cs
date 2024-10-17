using System;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

class Example_SetJPEGQuality
{
    public static void Main()
    {
        ImageCodecInfo myImageCodecInfo;
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        Bitmap image1;
   
     

        image1 = new Bitmap("bilet.jpg", true);

        myImageCodecInfo = GetEncoderInfo("image/jpeg");

        myEncoder = Encoder.Quality;

        myEncoderParameters = new EncoderParameters(1);

        myEncoderParameter = new EncoderParameter(myEncoder, 25L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        image1.Save("bilet-1.jpg", myImageCodecInfo, myEncoderParameters);

        Graphics graphics = Graphics.FromImage(image1);


        Brush brush = new SolidBrush(Color.Black);


        Font arial = new Font("Arial", 30, FontStyle.Regular);
        Font arialBoldItalic = new Font("Arial", 50, FontStyle.Bold | FontStyle.Italic);


        string imie = "Mateusz";
        string nazwisko = "Janiczek";
        string cenna = "10 zl";
        string From = "Krakow";
        string to = "Warszawa";
   
        Rectangle rectangleImie = new Rectangle(200, 1360, 450, 100);
        Rectangle rectangleNazwisko = new Rectangle(270, 1470, 450, 100);
        Rectangle rectangleCena = new Rectangle(670, 1410, 450, 100);

        Rectangle rectangleFrom = new Rectangle(85, 550, 450, 100);
        Rectangle rectangleTo = new Rectangle(530, 550, 450, 100);

        graphics.DrawString(imie, arial, brush, rectangleImie);
        graphics.DrawString(nazwisko, arial, brush, rectangleNazwisko);
        graphics.DrawString(cenna, arial, brush, rectangleCena);
        graphics.DrawString(From, arialBoldItalic, brush, rectangleFrom);
        graphics.DrawString(to, arialBoldItalic, brush, rectangleTo);


        image1.Save("bilet-1.jpg", myImageCodecInfo, myEncoderParameters);

    }
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
}   



