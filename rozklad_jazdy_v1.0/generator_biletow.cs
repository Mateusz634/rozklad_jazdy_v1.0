using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Bitmap image1;

			image1 = new Bitmap("bilet.jpg", true);
			RectangleF cloneRect = new RectangleF(0, 0, 100, 100);
			System.Drawing.Imaging.PixelFormat format =
				image1.PixelFormat;
			Bitmap cloneBitmap = image1.Clone(cloneRect, format);

		}
	}
}
