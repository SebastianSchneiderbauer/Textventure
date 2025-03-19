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
        private static int screenWidth = 228; //büdl wird zentriert
        private static int screenHeight = 50;

        static void Main()
        {
            Console.SetBufferSize(400, 200);
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\pics\test4.png");

            int imageWidth = 50 * 2;  //double your width
            int imageHeight = 50;

            ConvertImageToAscii(imagePath, imageWidth, imageHeight);
        }

        static void ConvertImageToAscii(string imagePath, int width, int height) //des wird nimma augriffn des is so 50% ChatGPT und 50% Beten
        {
            //handle error case of image being too large
            if (width > screenWidth || width < 0) {
                width = screenWidth;
            }

            if (height > screenHeight || height < 0)
            {
                height = screenHeight;
            }

            Mat image = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);

            CvInvoke.Resize(image, image, new Size(width, height));

            byte[] imageData = new byte[image.Rows * image.Cols];
            image.CopyTo(imageData);

            string asciiChars = " ░▒▓█";

            string asciiResult = "";
            string horizontalTopBar = "████████";
            string horizontalMidBar = "██  ";

            for( int i = 0; i < (screenWidth - width) / 2; i++)
            {
                horizontalTopBar += "██";
                horizontalMidBar += "  ";
            }

            for (int i = 0; i < image.Cols; i++)
            {
                horizontalTopBar += "█";
                horizontalMidBar += " ";
            }

            horizontalMidBar += "  ██";
            asciiResult += horizontalTopBar + "\n" + horizontalMidBar + "\n";
            for(int i = 0; i < (screenHeight - image.Rows)/2; i++)
            {
                asciiResult += horizontalMidBar + "\n";
            }
            for (int y = 0; y < image.Rows; y++)
            {
                asciiResult += "██  ";
                for (int i = 0; i < (screenWidth - width) / 4; i++) 
                {
                    asciiResult += "  ";
                }
                for (int x = 0; x < image.Cols; x++)
                {
                    byte intensity = imageData[y * image.Cols + x]; // Access pixel value
                    int index = intensity * (asciiChars.Length - 1) / 255; // Scale to ASCII index
                    asciiResult += asciiChars[index];
                }
                for (int i = 0; i < (screenWidth - width) / 4; i++)
                {
                    asciiResult += "  ";
                }
                asciiResult += "  ██\n";
            }

            for (int i = 0; i < (screenHeight - image.Rows) / 2; i++)
            {
                asciiResult += horizontalMidBar + "\n";
            }

            asciiResult += horizontalMidBar + "\n";
            asciiResult += horizontalTopBar;


            Console.SetCursorPosition(0, 0);
            Console.WriteLine(asciiResult);
            for(int i = 0; i < 6+2; i++)
            {
                Console.WriteLine(horizontalMidBar);
            }
            Console.WriteLine(horizontalTopBar);

            Console.SetCursorPosition(4,55); Console.WriteLine("EXAMPLE TEXT");
            //TODO: text und optionen printen
        }
    }
}