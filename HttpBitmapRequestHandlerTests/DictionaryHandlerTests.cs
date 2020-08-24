using college_interview_task_v4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using static college_interview_task_v4.HttpDictionaryRequestHandler;

namespace HttpStringRequestHandlerTests
{
    [TestClass]
    public class DictionaryHandlerTests
    {
        [TestMethod]
        public void DictionaryHandlerCreatesValidMessage()
        {


            //Arrange//
            HttpRequestMessage expectedMessage = new HttpRequestMessage
            {
                Content = "Example message" as HttpContent,
                Method = HttpMethod.Get,
                RequestUri = (Uri)"https://localhost:44338/"
            }

            string requestUri = "";
            HttpMethod httpMethod = HttpMethod.Get;
            string messageContent = "Example message";

            HttpDictionaryRequestHandler dictionaryRequestHandler = new HttpDictionaryRequestHandler(new HttpClient(), new DictionaryParser());

            //Act//
            HttpRequestMessage requestMessage = dictionaryRequestHandler.CreatePreparedRequestMessage(requestUri, httpMethod, messageContent, null);

            //Assert//
            Assert.AreEqual(requestUri, requestMessage.RequestUri);

        }

        [TestMethod]
        public void DictionaryHandlerParsesContentCorrectly()
        {
            //Arrange//
            Dictionary<string, string> expectedValues = new Dictionary<string, string>
            {
                {"a", "value1"},
                {"a", "value1"},
                {"a", "value1"}
            };



            DictionaryParser parser = new DictionaryParser();


            //Act//
            parser.ParseAsync(httpResponse);

            //Assert//

        }
    }
}
