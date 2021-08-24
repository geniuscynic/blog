using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace XjjXmm.Door.Middleware
{
    public class ProxyMiddleware
    {
        // private ProxyHttpClient _proxyHttpClient;
        private const string CDN_HEADER_NAME = "Cache-Control";
        private static readonly string[] NotForwardedHttpHeaders = new[] {"Connection", "Host"};
        private readonly RequestDelegate _next;
        private readonly ILogger<ProxyMiddleware> _logger;

        public ProxyMiddleware(
            RequestDelegate next,
            ILogger<ProxyMiddleware> logger

        )
        {
            _next = next;
            _logger = logger;
            //_proxyHttpClient = proxyHttpClient;
        }

        /// <summary>
        /// 通过中间件,拦截访问,检测前缀,并转发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="urlRewriter"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IUrlRewriter urlRewriter, IHttpClientFactory httpClientFactory)
        {
            var targetUri = await urlRewriter.RewriteUri(context);

            if (targetUri != null)
            {
                var requestMessage = GenerateProxifiedRequest(context, targetUri);
                await SendAsync(context, requestMessage, httpClientFactory);

                return;
            }

            await _next(context);
        }

        private async Task SendAsync(HttpContext context, HttpRequestMessage requestMessage,
            IHttpClientFactory httpClientFactory)
        {


            using (var responseMessage = await httpClientFactory.CreateClient().SendAsync(requestMessage,
                HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                context.Response.StatusCode = (int) responseMessage.StatusCode;

                foreach (var header in responseMessage.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                context.Response.Headers.Remove("transfer-encoding");

                if (!context.Response.Headers.ContainsKey(CDN_HEADER_NAME))
                {
                    context.Response.Headers.Add(CDN_HEADER_NAME, "no-cache, no-store");
                }

                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
        }

        private static HttpRequestMessage GenerateProxifiedRequest(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();
            CopyRequestContentAndHeaders(context, requestMessage);
            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetMethod(context.Request.Method);
            return requestMessage;
        }

        private static void CopyRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;
            if (!HttpMethods.IsGet(requestMethod) &&
                !HttpMethods.IsHead(requestMethod) &&
                !HttpMethods.IsDelete(requestMethod) &&
                !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            foreach (var header in context.Request.Headers)
            {
                if (!NotForwardedHttpHeaders.Contains(header.Key))
                {
                    if (header.Key != "User-Agent")
                    {
                        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) &&
                            requestMessage.Content != null)
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                        }
                    }
                    else
                    {
                        string userAgent = header.Value.Count > 0
                            ? (header.Value[0] + " " + context.TraceIdentifier)
                            : string.Empty;

                        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, userAgent) &&
                            requestMessage.Content != null)
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, userAgent);
                        }
                    }

                }
            }
        }

        private static HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
            return new HttpMethod(method);
        }
    }
}
