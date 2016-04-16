using System;
using Harcourts.Face.Recognition.Betaface;
using NUnit.Framework;

namespace Harcourts.Face.UnitTest
{
    [TestFixture]
    internal class RecognitionTests
    {
        [Test]
        public void Detect_Test()
        {
            var engine = new BetaFaceEngine();
            var imageUri = new Uri("http://photos.harcourts.co.nz/V2/000/012/082/122-Jamin-Marshall.jpg");
            engine.UploadImageUrl(imageUri, DetectionFlags.Default);
        }
    }
}
