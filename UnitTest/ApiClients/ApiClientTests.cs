using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Harcourts.Face.Client;
using Harcourts.Face.Website;
using NUnit.Framework;

namespace Harcourts.Face.UnitTest.ApiClients
{
    [Explicit]
    public class ApiClientTests
    {
        [Test]
        public void Identify_Test()
        {
            var client = new FaceApiClient("");
            var request = new IdentifyRequest
                          {
                              ImageType = ImageStreamType.Jpeg,
                              ImageStream = File.OpenRead(@"C:\Work\cat.jpg")
                          };
            var response = client.Identify(request);
        }

        [Test]
        public void InMemory_Test()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            var server = new HttpServer(config);

            HttpMessageHandler handler = server;
            handler = new MyHandler();

            var invoker = new HttpMessageInvoker(handler);
            var request = new HttpRequestMessage();
            var responseMessage = invoker.SendAsync(request, CancellationToken.None);
            responseMessage.Wait();
            
            
            
        }

        private class MyHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                //var requestContext = new HttpRequestContext();
                //request.SetRequestContext(requestContext);
                var context = request.GetRequestContext();

                var route = request.GetRouteData();

                throw new NotImplementedException();
            }
        }
    }
}
