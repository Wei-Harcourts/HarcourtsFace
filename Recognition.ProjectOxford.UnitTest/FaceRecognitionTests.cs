using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public class FaceRecognitionTests : FaceServiceTests
    {
        [Test, Explicit]
        public async Task TestJaminMarshall()
        {
            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    var jamin1 = new Uri(
                        "https://scontent-syd1-1.xx.fbcdn.net/v/t1.0-9/" +
                        "11703204_10153416465486718_8837409305822250578_n.jpg?" +
                        "oh=29ca804ce6ab8a1b6d59fcedbe060a39&oe=57A4B2F6",
                        UriKind.Absolute);
                    var jamin2 = new Uri(
                        "https://scontent-syd1-1.xx.fbcdn.net/v/t1.0-9/" +
                        "10434315_10152787820126718_9009613909719145234_n.jpg?" +
                        "oh=6910e6dcc9aaafe76c5eb1688a87010d&oe=57E715EE",
                        UriKind.Absolute);

                    var faces = await client.DetectAsync(jamin2.AbsoluteUri);
                    var results = await client.IdentifyAsync(
                        "faces-harcourts-co-nz", faces.Select(f => f.FaceId).Take(10).ToArray());
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }
            }
        }
    }
}
