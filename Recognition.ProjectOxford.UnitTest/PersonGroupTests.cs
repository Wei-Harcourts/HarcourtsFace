using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    [TestFixture]
    public class PersonGroupTests: FaceServiceTests
    {
        [Test, Explicit]
        public async void BuildHarcourtsNzPersonGroup()
        {
            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    await client.CreatePersonGroupAsync("faces-harcourts-co-nz", "Harcourts New Zealand Faces");
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }
            }
        }

        [Test, Explicit]
        public async void AddJaminMarshall()
        {
            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    await client.CreatePersonAsync("faces-harcourts-co-nz", "Jamin Marshall", "jamin.marshall@harcourts.co.nz");
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
