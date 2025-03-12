using Emgu.CV.CvEnum;
using Emgu.CV;
using System.Drawing;
using System;

//Install - Package Emgu.CV
//Install-Package Emgu.CV.Bitmap
//Install - Package Emgu.CV.runtime.windows


namespace Textventure
{
    internal class Program
    {
        static void Main()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\pics\test1.jpg");
            int width = 50 * 2;  //double your width
            int height = 50;  // keep the height normal

            string asciiArt = ConvertImageToAscii(imagePath, width, height);
            Console.WriteLine(asciiArt);
        }

        static string ConvertImageToAscii(string imagePath, int width, int height)
        {
            Mat image = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);

            CvInvoke.Resize(image, image, new Size(width, height));

            byte[] imageData = new byte[image.Rows * image.Cols];
            image.CopyTo(imageData);

            string asciiChars = " ░▒▓█";

            string asciiResult = "";
            string horizontalTopBar = "████████";
            string horizontalMidBar = "██  ";

            for (int i = 0; i < image.Cols; i++)
            {
                horizontalTopBar += "█";
                horizontalMidBar += " ";
            }

            horizontalMidBar += "  ██";
            asciiResult += horizontalTopBar + "\n" + horizontalMidBar + "\n";

            for (int y = 0; y < image.Rows; y++)
            {
                asciiResult += "██  ";
                for (int x = 0; x < image.Cols; x++)
                {
                    byte intensity = imageData[y * image.Cols + x]; // Access pixel value
                    int index = intensity * (asciiChars.Length - 1) / 255; // Scale to ASCII index
                    asciiResult += asciiChars[index];
                }
                asciiResult += "  ██\n";
            }

            asciiResult += horizontalMidBar + "\n";
            asciiResult += horizontalTopBar;

            return asciiResult;
        }
    }
}