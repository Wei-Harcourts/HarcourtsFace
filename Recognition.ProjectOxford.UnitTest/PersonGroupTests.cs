using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public class PersonGroupTests : FaceServiceTests
    {
        [Test, Explicit]
        public async Task CreateHarcourtsNzPersonGroup()
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
        public async Task BuildHarcourtsNzPersonGroup()
        {
            var scrawledPersons = await new ScrawlTests().ScrawlGrenadierAgents();
            var existingPersons = (await GetAllNzPersons()).ToDictionary(p => p.UserData, p => p);

            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    var counter = 0;
                    foreach (var person in scrawledPersons)
                    {
                        if (!existingPersons.ContainsKey(person.emailAddress))
                        {
                            var created = await client.CreatePersonAsync("faces-harcourts-co-nz", person.personName,
                                person.emailAddress);
                            client.AddPersonFaceAsync("faces-harcourts-co-nz", created.PersonId, person.photo, person.photo);
                            counter += 2;
                        }
                        else
                        {
                            var existingPerson = existingPersons[(string) person.emailAddress];
                            if (existingPerson.PersistedFaceIds == null || !existingPerson.PersistedFaceIds.Any())
                            {
                                client.AddPersonFaceAsync("faces-harcourts-co-nz", existingPerson.PersonId, person.photo, person.photo);
                                counter++;
                            }
                            else
                            {
                                // We can update the photo, but don't do for now as API calls are treasure.
                            }
                        }

                        if (counter >= 18)
                        {
                            await Task.Delay(new TimeSpan(0, 1, 0));
                            counter = 0;
                        }
                    }
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

        [Test, Explicit]
        public async Task TrainHarcourtsNzPersonGroup()
        {
            using (var client = CreateFaceServiceClient())
            {
                try
                {
                    await client.TrainPersonGroupAsync("faces-harcourts-co-nz");
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }
            }
        }

        [Test, Explicit]
        public async Task<Status> GetTrainHarcourtsNzPersonGroupStatus()
        {
            using (var client = CreateFaceServiceClient())
            {
                TrainingStatus result;
                try
                {
                    result = await client.GetPersonGroupTrainingStatusAsync("faces-harcourts-co-nz");
                }
                catch (Exception ex)
                {
                    ex.Dump();
                    throw;
                }

                return result.Status;
            }
        }
    }
}
