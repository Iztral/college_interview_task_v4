using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace college_interview_task_v4
{
    public abstract class HttpRequestHandler<TRequest, TResponse>
    {
        private readonly HttpClient _httpClientProxy;

        public interface IHttpResponseParser<out TResult>
        {
            TResult ParseAsync(HttpResponseMessage response);
        }

        private IHttpResponseParser<TResponse> Parser { get; }

        protected HttpRequestHandler(HttpClient httpClientProxy, IHttpResponseParser<TResponse> parser)
        {
            Parser = parser;
            _httpClientProxy = httpClientProxy;
        }

        public async Task<TResponse> Handle(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _httpClientProxy.SendAsync(httpRequestMessage, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new EndOfStreamException(string.Join(",", response.Headers));
            }

            try
            {
                return Parser.ParseAsync(response);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("todo: add missing implementation of " + ex.ToString());
            }
        }

        #region request preparation
        public HttpRequestMessage CreatePreparedRequestMessage(string requestUri, HttpMethod httpMethod, TRequest RequestMessageContent,
            IDictionary<string, string>? additionalHeaders)
        {
            HttpRequestMessage httpRequestMessage = CreateEmptyHttpMessage(requestUri, httpMethod);
            FillHttpMessage(httpRequestMessage, RequestMessageContent, additionalHeaders);

            return httpRequestMessage;
        }

        private HttpRequestMessage CreateEmptyHttpMessage(string requestUri, HttpMethod httpMethod)
        {
            return new HttpRequestMessage(httpMethod, $"{_httpClientProxy.BaseAddress}{requestUri}");
        }

        private void FillHttpMessage(HttpRequestMessage httpRequestMessageToFill, TRequest RequestMessageContent, IDictionary<string, string>? additionalHeaders)
        {
            if (RequestMessageContent != null)
            {
                AssignHttpMessageContent(httpRequestMessageToFill, RequestMessageContent);
            }

            if (additionalHeaders != null)
            {
                AssignHttpMessageHeaders(httpRequestMessageToFill, additionalHeaders);
            }
        }

        protected abstract void AssignHttpMessageContent(HttpRequestMessage httpRequestMessage, TRequest RequestMessageContent);


        private void AssignHttpMessageHeaders(HttpRequestMessage httpRequestMessage, IDictionary<string, string> additionalHeaders)
        {
            foreach (var header in additionalHeaders)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }
        }
        #endregion
    }
}