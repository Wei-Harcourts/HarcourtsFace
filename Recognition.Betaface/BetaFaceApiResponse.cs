using Newtonsoft.Json;

namespace Harcourts.Face.Recognition.Betaface
{
    /// <summary>
    /// The base response of API calls to BetaFace.
    /// </summary>
    public class BetaFaceApiResponse
    {
        /// <summary>
        /// The status code of the response. 0 means succeeded, 1 means pending, and 
        /// negative number means failed. Check <see cref="StatusDescription"/> for descriptive text.
        /// </summary>
        [JsonProperty("int_response")]
        public int Status { get; set; }

        /// <summary>
        /// The description of the <see cref="Status"/>.
        /// </summary>
        [JsonProperty("string_response")]
        public string StatusDescription { get; set; }
    }
}
