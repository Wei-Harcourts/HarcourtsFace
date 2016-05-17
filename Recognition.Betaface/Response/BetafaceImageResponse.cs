using Newtonsoft.Json;

namespace Harcourts.Face.Recognition.Betaface.Response
{
    /// <summary>
    /// The response of API call to upload image url to BetaFace.
    /// </summary>
    public sealed class BetafaceImageResponse : BetaFaceApiResponse
    {
        /// <summary>
        /// The unique image ID.
        /// </summary>
        [JsonProperty("img_uid")]
        public string ImageUid { get; set; }
    }
}
