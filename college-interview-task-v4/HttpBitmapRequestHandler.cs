using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;

namespace college_interview_task_v4
{

    public class HttpBitmapRequestHandler : HttpRequestHandler<string, Bitmap>
    {
        public HttpBitmapRequestHandler(HttpClient client, IHttpResponseParser<Bitmap> parser) : base(client, parser)
        {

        }
        protected override void AssignHttpMessageContent(HttpRequestMessage httpRequestMessage, string? RequestMessageContent)
        {
            httpRequestMessage.Content = new StringContent(RequestMessageContent, Encoding.UTF8, "application/json");
        }
        
        public class BitmapParser : IHttpResponseParser<Bitmap>
        {
            public Bitmap ParseAsync(HttpResponseMessage response)
            {
                byte[] imgData = JsonConvert.DeserializeObject<byte[]>(response.Content.ReadAsStringAsync().Result);
                using var ms = new MemoryStream(imgData, 0, imgData.Length);
                return (Bitmap)Image.FromStream(ms);
            }
        }
    }
}
