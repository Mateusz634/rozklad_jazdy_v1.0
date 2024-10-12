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
	
		image1 = new Bitmap("bilet.jpg",true);

		myImageCodecInfo = GetEncoderInfo("image/jpeg");

		myEncoder = Encoder.Quality;
	
		myEncoderParameters = new EncoderParameters(1);

		myEncoderParameter = new EncoderParameter(myEncoder, 25L);
		myEncoderParameters.Param[0] = myEncoderParameter;
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
