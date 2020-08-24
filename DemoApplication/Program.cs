using college_interview_task_v4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Threading;
using static college_interview_task_v4.HttpBitmapRequestHandler;
using static college_interview_task_v4.HttpDictionaryRequestHandler;

namespace DemoApplication
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the gallery viewer. Should I connect to the database? Y/N \n");
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Y)
            {
                HttpClient httpClient = new HttpClient();
                string requestUri;
                HttpRequestMessage requestMessage;
                HttpDictionaryRequestHandler listRequestHandler = new HttpDictionaryRequestHandler(httpClient, new DictionaryParser());
                requestUri = @"https://localhost:44338/api/storedimage/";
                requestMessage = listRequestHandler.CreatePreparedRequestMessage(requestUri, HttpMethod.Get, null, null);

                Dictionary<string, string> imageList = listRequestHandler.Handle(requestMessage, new CancellationToken()).Result;

                Console.WriteLine("\n Images in database: \n");
                foreach (var image in imageList)
                {
                    Console.WriteLine(image.Key + "    " + image.Value);
                }
                string imageId;
                do
                {
                    Console.WriteLine("Write image Id to download:");
                    imageId = Console.ReadLine();
                }
                while (!imageList.ContainsKey(imageId));



                requestUri = @"https://localhost:44338/api/storedimage/" + imageId;

                HttpBitmapRequestHandler httpBitmapRequestHandler = new HttpBitmapRequestHandler(httpClient, new BitmapParser());

                requestMessage = httpBitmapRequestHandler.CreatePreparedRequestMessage(requestUri, HttpMethod.Get, null, null);
                var downloadedImage = httpBitmapRequestHandler.Handle(requestMessage, new CancellationToken()).Result;

                Console.WriteLine("Save file as: \n");
                Bitmap bitmap = new Bitmap(downloadedImage);
                bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + Console.ReadLine(), ImageFormat.Jpeg);

            }
        }
    }
}
