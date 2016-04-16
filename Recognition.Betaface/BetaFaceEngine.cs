using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.Recognition.Betaface.Json;
using RestSharp;

namespace Harcourts.Face.Recognition.Betaface
{
    public class BetaFaceEngine
    {
        public const string API_Key = "d45fd466-51e2-4701-8da8-04351c872236";
        public const string API_Secret = "171e8465-f548-401d-b63b-caf0dc28df5f";

        private RestClient _client;

        public BetaFaceEngine()
        {
            _client = new RestClient("http://www.betafaceapi.com/service_json.svc");
        }

        public void UploadImageUrl(Uri imageUri, DetectionFlags detectionFlags, string originalFileName = null)
        {
            if (imageUri == null)
            {
                throw new ArgumentNullException("imageUri");
            }

            var request = CreateRequest("UploadNewImage_Url");
            request.AddParameter("detection_flags", detectionFlags.ToParameter());
            request.AddParameter("image_url", imageUri.AbsoluteUri);
            if (originalFileName == null)
            {
                originalFileName = Path.GetFileName(imageUri.AbsolutePath);
            }
            request.AddParameter("original_filename", originalFileName);

            var response = _client.Execute<BetaFaceApiResponse>(request);
        }

        protected RestRequest CreateRequest(string methodName)
        {
            var request = new RestRequest(methodName, Method.POST) {JsonSerializer = new JsonSerializer()};
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", API_Key, ParameterType.GetOrPost);
            request.AddParameter("api_secret", API_Secret, ParameterType.GetOrPost);
            return request;
        }
    }
}
