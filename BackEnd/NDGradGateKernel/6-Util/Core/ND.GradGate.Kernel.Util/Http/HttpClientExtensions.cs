﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Util.Http
{
    public static class HttpClientExtensions
    {
        #region Methods
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            return client.PatchAsync(CreateUri(requestUri), content);

        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content)
        {
            return client.PatchAsync(requestUri, content, CancellationToken.None);
        }
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.PatchAsync(CreateUri(requestUri), content, cancellationToken);
        }
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {

                Content = content
            }, cancellationToken);
        }
        public static Uri? CreateUri(string uri)
        {
            return string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);
        }
        #endregion
    }
}
