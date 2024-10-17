using System;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

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

       
        Graphics graphics = Graphics.FromImage(image1);
        Brush brush = new SolidBrush(Color.Black);
        Font arial = new Font("Arial", 30, FontStyle.Regular);
        Font arialBoldItalic = new Font("Arial", 50, FontStyle.Bold | FontStyle.Italic);

       
        string imie = "Adam";
        string nazwisko = "Kruszynski";
        string cenna = "10 zl";
        string From = "Krakow";
        string to = "Warszawa";

        graphics.DrawString(imie, arial, brush, new Rectangle(200, 1360, 450, 100));
        graphics.DrawString(nazwisko, arial, brush, new Rectangle(270, 1470, 450, 100));
        graphics.DrawString(cenna, arial, brush, new Rectangle(670, 1410, 450, 100));
        graphics.DrawString(From, arialBoldItalic, brush, new Rectangle(85, 550, 450, 100));
        graphics.DrawString(to, arialBoldItalic, brush, new Rectangle(530, 550, 450, 100));

        string qrText = $"{imie} {nazwisko}, {cenna}, From: {From}, To: {to}";
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.L))
            {
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                 
                    graphics.DrawImage(qrCodeImage, new Rectangle(300, 1650, 350, 350)); 
                }
            }
        }

        
        string fileName = $"bilet_{DateTime.Now:ddssHH}.jpg";
        image1.Save(fileName, myImageCodecInfo, myEncoderParameters);

  
        graphics.Dispose();
        image1.Dispose();
    }

    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
        for (int j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
}



