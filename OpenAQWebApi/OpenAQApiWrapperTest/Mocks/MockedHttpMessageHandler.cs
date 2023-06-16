﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAQApiWrapperTest.Mocks
{
    public class MockedHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<Task<HttpResponseMessage>> _responseFactory;
        private readonly HttpResponseMessage _httpResponseMessage;

        public MockedHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }

        public MockedHttpMessageHandler(Func<Task<HttpResponseMessage>> responseFactory)
        {
            _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_responseFactory != null)
            {
                return await _responseFactory.Invoke();
            }

            return _httpResponseMessage;
        }
    }

}
