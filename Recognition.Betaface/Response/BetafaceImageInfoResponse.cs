using Newtonsoft.Json;

namespace Harcourts.Face.Recognition.Betaface.Response
{
    /// <summary>
    /// The response with image information.
    /// </summary>
    public sealed class BetafaceImageInfoResponse : BetaFaceApiResponse
    {
        /// <summary>
        /// The SHA-256 checksum of the image file.
        /// </summary>
        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        /// <summary>
        /// The faces detected in the image.
        /// </summary>
        [JsonProperty("faces")]
        public dynamic[] Faces { get; set; }

        /// <summary>
        /// The original file name of the image file.
        /// </summary>
        [JsonProperty("original_filename")]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// The unique ID of the image.
        /// </summary>
        [JsonProperty("uid")]
        public string ImageUid { get; set; }
    }
}
