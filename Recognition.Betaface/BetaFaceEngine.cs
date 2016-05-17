using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.Recognition.Betaface.Json;
using Harcourts.Face.Recognition.Betaface.Response;
using RestSharp;

namespace Harcourts.Face.Recognition.Betaface
{
    /// <summary>
    /// Represents the engine to use API service provided on BetaFace.
    /// </summary>
    public sealed class BetaFaceEngine
    {
        public const string API_Key = "d45fd466-51e2-4701-8da8-04351c872236";
        public const string API_Secret = "171e8465-f548-401d-b63b-caf0dc28df5f";

        private readonly RestClient _client;

        public BetaFaceEngine()
        {
            _client = new RestClient("http://www.betafaceapi.com/service_json.svc");
            _client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
        }

        /// <summary>
        /// Uploads an image by providing the image URI.
        /// </summary>
        public BetafaceImageResponse UploadImageUrl(Uri imageUri, DetectionFlags detectionFlags,
            string originalFileName = null)
        {
            if (imageUri == null)
            {
                throw new ArgumentNullException("imageUri");
            }

            AssertUriIsAbsoluteHttpOrHttps(imageUri);

            var response = Call<BetafaceImageResponse>("UploadNewImage_Url",
                r =>
                {
                    r.AddParameter("detection_flags", detectionFlags.ToParameter());
                    r.AddParameter("image_url", imageUri.AbsoluteUri);
                    if (originalFileName == null)
                    {
                        originalFileName = Path.GetFileName(imageUri.AbsolutePath);
                    }
                    r.AddParameter("original_filename", originalFileName);
                });
            return response;
        }

        /// <summary>
        /// Gets the image information with the specified unique image ID.
        /// </summary>
        public BetafaceImageInfoResponse GetImageInfo(string imageUid)
        {
            if (string.IsNullOrEmpty(imageUid))
            {
                throw new ArgumentNullException(imageUid);
            }

            var response = Call<BetafaceImageInfoResponse>("GetImageInfo",
                r =>
                {
                    r.AddParameter("img_uid", imageUid);
                });
            return response;
        }

        private TResponseData Call<TResponseData>(string method, Action<IRestRequest> configureRequest)
            where TResponseData : class, new()
        {
            var request = CreateRequest(method);
            configureRequest.Invoke(request);
            var response = _client.Execute<TResponseData>(request);
            return AssertResponseOkAndGetData(response);
        }

        private static RestRequest CreateRequest(string methodName)
        {
            var request = new RestRequest(methodName, Method.POST) {JsonSerializer = NewtonsoftJsonSerializer.Default};
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", API_Key, ParameterType.GetOrPost);
            request.AddParameter("api_secret", API_Secret, ParameterType.GetOrPost);
            return request;
        }

        private static void AssertUriIsAbsoluteHttpOrHttps(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (!uri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("Uri is not absolute.");
            }

            var schema = uri.Scheme.ToLowerInvariant();
            if (!schema.Equals("http") && !schema.Equals("https"))
            {
                throw new InvalidOperationException("Uri is not using HTTP or HTTPs.");
            }
        }

        private static TResponseData AssertResponseOkAndGetData<TResponseData>(IRestResponse<TResponseData> response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new BetaFaceHttpException(response);
            }

            return response.Data;
        }
    }
}
