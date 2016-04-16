using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Harcourts.Face.Recognition.Betaface
{
    public class BetaFaceApiResponse
    {
        [JsonProperty("int_response")]
        public int Status { get; set; }
    }
}
