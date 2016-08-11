using System.Collections.Generic;
using System.Net;
using System.Text;
using Harcourts.Face.Client.Json;
using Newtonsoft.Json;
using RestSharp;

namespace Harcourts.Face.Client
{
    public class FaceApiClient
    {
        private readonly RestClient _restClient;
        private readonly string _consumerKey;

        public FaceApiClient(string consumerKey)
        {
            _consumerKey = consumerKey;
            _restClient = new RestClient("http://whostheagent.apphb.com/api/face") {Encoding = Encoding.UTF8};
            _restClient.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
        }

        public IdentifyResponse Identify(IdentifyRequest request)
        {
            var requst = new RestRequest("/identify")
                         {
                             JsonSerializer = NewtonsoftJsonSerializer.Default
                         };
            requst.AddHeader("x-whos-the-agent-key", _consumerKey ?? string.Empty);
            var httpResponse = _restClient.Post(requst);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<FaceApiError>(httpResponse.Content ?? string.Empty);
                var exception = new FaceApiException(httpResponse.StatusCode, error.Message);
                return IdentifyResponse.CreateError(exception);
            }

            var data = JsonConvert.DeserializeObject<IEnumerable<Person>>(httpResponse.Content ?? string.Empty);
            return IdentifyResponse.CreateSuccessful(data);
        }

        private class FaceApiError
        {
            public string Message { get; set; }
        }
    }
}
