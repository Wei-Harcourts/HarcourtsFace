using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    [TestFixture]
    public class PersonGroupTests : FaceServiceTests
    {
        [Test, Explicit]
        public async Task BuildHarcourtsNzPersonGroup()
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
        public async Task<Guid> AddJaminMarshall()
        {
            using (var client = CreateFaceServiceClient())
            {
                CreatePersonResult result;
                try
                {
                    result = await
                        client.CreatePersonAsync("faces-harcourts-co-nz", "Jamin Marshall",
                            "jamin.marshall@harcourts.co.nz");
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }

                return result.PersonId;
            }
        }

        [Test, Explicit]
        public async Task<Guid> AddFaceToJaminMarshall()
        {
            using (var client = CreateFaceServiceClient())
            {
                AddPersistedFaceResult result;
                try
                {
                    var personId = new Guid("6225b3c2-1ea0-4545-8239-05bc4aef0148");
                    const string imageUrl = "http://photos.harcourts.co.nz/V2/000/012/082/122-Jamin-Marshall.jpg";
                    result = await client.AddPersonFaceAsync(
                        "faces-harcourts-co-nz", personId, imageUrl, imageUrl);
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }

                return result.PersistedFaceId;
            }
        }

        [Test, Explicit]
        public async Task<Person[]> GetAllNzPersons()
        {
            using (var client = CreateFaceServiceClient())
            {
                Person[] result;
                try
                {
                    result = await client.GetPersonsAsync("faces-harcourts-co-nz");
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }

                return result;
            }
        }

        [Test, Explicit]
        public async Task DeleteNzPerson()
        {
            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    var personId = new Guid("7c73585d-983d-4f1f-84bb-c92fcc12f300");
                    await client.DeletePersonAsync("faces-harcourts-co-nz", personId);
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
