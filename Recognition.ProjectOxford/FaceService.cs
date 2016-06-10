using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteCommon;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Recognition;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using PersonFace = Microsoft.ProjectOxford.Face.Contract.Face;

namespace Harcourts.Face.Recognition.ProjectOxford
{
    /// <summary>
    /// Service object for face APIs.
    /// </summary>
    public class FaceService : IIdentifyServiceProvider<Stream, Guid>
    {
        /// <summary>
        /// The API subscription key.
        /// </summary>
        public static readonly string SubscriptionKey;

        /// <summary>
        /// The face group ID of the Harcourts New Zealand Group.
        /// </summary>
        public const string TheNewZealandGroup = "faces-harcourts-co-nz";

        /// <summary>
        /// The bottom line of acceptable confidence level.
        /// </summary>
        public static double ConfidenceBottomLine = 0.5d;

        static FaceService()
        {
            SubscriptionKey = ConfigurationManager.AppSettings["ProjectOxford.Face.SubscriptionKey"];
        }

        /// <summary>
        /// Identify faces in the image stream.
        /// </summary>
        public async Task<PersonLookupKey<Guid>[]> Identify(Stream imageStream)
        {
            Contract.Ensures(Contract.Result<PersonLookupKey<Guid>[]>() != null);

            try
            {
                // Only use the first 10 large clear faces in the image stream and discard the others.
                var faces = await DetectPersonFacesFromImageStream(imageStream);

                // Identify the faces in the face group.
                var identifyResults = await IdentifyPersons(TheNewZealandGroup, faces);

                // Get all the candidates with confidence greater than the bottom line.
                var goodResults =
                    identifyResults.SelectMany(r => r.Candidates)
                        .GroupBy(c => c.PersonId)
                        .Select(g => g.First())
                        .Where(c => c.Confidence >= ConfidenceBottomLine)
                        .Select(c => new PersonLookupKey<Guid>(c.PersonId))
                        .Distinct()
                        .ToArray();

                return goodResults;
            }
            catch (RaiseErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RaiseErrorException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Call to detect faces in the image stream.
        /// </summary>
        private async Task<IEnumerable<PersonFace>> DetectPersonFacesFromImageStream(Stream imageStream)
        {
            Contract.Ensures(Contract.Result<IEnumerable<PersonFace>>() != null);

            return await CallAndGetResponse(
                async (client, state) =>
                      {
                          var faces = await client.DetectAsync((Stream) state);
                          return faces ?? new PersonFace[0];
                      },
                imageStream);
        }

        /// <summary>
        /// Identify person faces among the persons in the specified group.
        /// </summary>
        private async Task<IEnumerable<IdentifyResult>> IdentifyPersons(string group, IEnumerable<PersonFace> faces,
            int facesTaken = 10)
        {
            Contract.Requires(facesTaken == 10,
                "The maximum number of faces taken from the detection result must be 10. See details of the Face API.");
            Contract.Ensures(Contract.Result<IEnumerable<IdentifyResult>>() != null);

            return await CallAndGetResponse(
                async (client, state) =>
                      {
                          // Only the first 10 large and clear faces will be selected.
                          var faceIds =
                              ((IEnumerable<PersonFace>) state).Take(facesTaken).Select(f => f.FaceId).ToArray();
                          var identifyResults = await client.IdentifyAsync(group, faceIds,
                              maxNumOfCandidatesReturned: 1);
                          return identifyResults ?? new IdentifyResult[0];
                      },
                faces);
        }

        /// <summary>
        /// Calls the Face API and returns response.
        /// </summary>
        /// <typeparam name="TReturn">The type of data object in the response.</typeparam>
        /// <param name="taskFunc">The function to call the API.</param>
        /// <param name="userState">The custom user state.</param>
        /// <param name="getErrorMessage">
        /// The handler to customize the error message returned by the Face API
        /// when <see cref="FaceAPIException"/> occurred.
        /// </param>
        protected async Task<TReturn> CallAndGetResponse<TReturn>(
            Func<IFaceServiceClient, object, Task<TReturn>> taskFunc,
            object userState = null,
            Func<FaceAPIException, string> getErrorMessage = null)
        {
            try
            {
                using (var client = CreateServiceClient())
                {
                    var result = await taskFunc.Invoke(client, userState);
                    return result;
                }
            }
            catch (FaceAPIException ex)
            {
                var errorMessage = getErrorMessage != null ? getErrorMessage.Invoke(ex) : ex.ErrorMessage;
                throw new RaiseErrorException(ex.HttpStatus, errorMessage);
            }
        }

        /// <summary>
        /// Creates a proxy client to use the Face API with configured settings. 
        /// </summary>
        protected virtual FaceServiceClient CreateServiceClient()
        {
            Contract.Ensures(Contract.Result<FaceServiceClient>() != null);

            return new FaceServiceClient(SubscriptionKey);
        }
    }
}
