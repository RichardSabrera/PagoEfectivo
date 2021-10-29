using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppPagoEfectivo.Connection
{
    public class CalledToApi
    {
        public Task<HttpResponseMessage> CallToApiGET(string urlBaseAddress, string requestUri, List<HeaderClass> listHeader)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBaseAddress);

                    foreach (var item in listHeader)
                    {
                        client.DefaultRequestHeaders.Add(item.HeaderName, item.HeaderValue);
                    }

                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = new HttpMethod("GET"),
                        RequestUri = new Uri(client.BaseAddress + requestUri),
                    };

                    return client.SendAsync(request);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = HttpStatusCode.BadRequest;
                httpResponseMessage.Content = new StringContent(ex.Message);

                return Task.FromResult(httpResponseMessage);
            }
        }

        public async Task<HttpResponseMessage> CallToApiGET(string urlBaseAddress, string requestUri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBaseAddress);

                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = new HttpMethod("GET"),
                        RequestUri = new Uri(client.BaseAddress + requestUri),
                    };

                    return await client.SendAsync(request);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = HttpStatusCode.BadRequest;
                httpResponseMessage.Content = new StringContent(ex.Message);

                return Task.FromResult(httpResponseMessage).Result;
            }
        }

    }
}
