using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace college_interview_task_v4
{
    public class HttpDictionaryRequestHandler : HttpRequestHandler<string, Dictionary<string, string>>
    {
        public HttpDictionaryRequestHandler(HttpClient client, IHttpResponseParser<Dictionary<string, string>> parser) : base(client, parser)
        {

        }

        protected override void AssignHttpMessageContent(HttpRequestMessage httpRequestMessage, string? RequestMessageContent)
        {
            httpRequestMessage.Content = new StringContent(RequestMessageContent, Encoding.UTF8, "application/json");
        }

        public class DictionaryParser : IHttpResponseParser<Dictionary<string, string>>
        {
            public Dictionary<string, string> ParseAsync(HttpResponseMessage response)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                if(responseContent != null)
                {
                    Dictionary<string, string> valuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                    return valuePairs;
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
        }
    }
}
