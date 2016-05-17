using System;
using Harcourts.Face.Recognition.Betaface;
using NUnit.Framework;

namespace Harcourts.Face.UnitTest
{
    [TestFixture]
    internal class RecognitionTests
    {
        [Test]
        public void DevsTest()
        {
            var engine = new BetaFaceEngine();
            var imageUri = new Uri("http://photos.harcourts.co.nz/V2/000/012/082/122-Jamin-Marshall.jpg");
            var image = engine.UploadImageUrl(imageUri, DetectionFlags.Default);

            var imageInfo = engine.GetImageInfo(image.ImageUid);
        }
    }
}
