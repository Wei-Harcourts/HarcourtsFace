using System;
using System.IO;

namespace Harcourts.Face.Client
{
    public class IdentifyRequest
    {
        public const int MaximumImageSize = 4 * 1024 * 1024;
        public Stream ImageStream { get; set; }
        public ImageStreamType ImageType { get; set; }

        internal IdentifyHttpRequestBody GetHttpRequestBody()
        {
            var image = ImageStream;
            var imageType = ImageType;
            if (image.Length > MaximumImageSize)
            {
                throw new ArgumentException("Image size is too big to process.");
            }

            string imageDataString;
            try
            {
                var buffer = new byte[MaximumImageSize];
                var actualReading = image.Read(buffer, 0, buffer.Length);
                imageDataString = Convert.ToBase64String(buffer, 0, actualReading);
            }
            finally
            {
                image.Close();
            }

            var data = string.Format("data:image/{0};base64,{1}",
                imageType.ToString().ToLowerInvariant(), imageDataString);

            return new IdentifyHttpRequestBody {Data = data};
        }

        internal class IdentifyHttpRequestBody
        {
            public string Data { get; set; }
        }
    }
}
