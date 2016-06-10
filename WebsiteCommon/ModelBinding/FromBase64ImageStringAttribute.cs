using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Newtonsoft.Json;

namespace Harcourts.Face.WebsiteCommon.ModelBinding
{
    /// <summary>
    /// Binds the parameter value from base64 encoded image string from the request body.
    /// </summary>
    public class FromBase64ImageStringAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType.IsAssignableFrom(typeof (MemoryStream)))
            {
                return new FromBase64ImageStringBinding(parameter);
            }
            return parameter.BindAsError("Unrecognized parameter type.");
        }

        internal class FromBase64ImageStringBinding : HttpParameterBinding
        {
            private static readonly List<string> SupportedRecognizedMimeTypes;
            private static readonly string NotSupportedExceptionMessage;

            static FromBase64ImageStringBinding()
            {
                SupportedRecognizedMimeTypes = new List<string>()
                                               {
                                                   "image/jpeg",
                                                   "image/png",
                                                   "image/gif",
                                                   "image/bitmap"
                                               };
                NotSupportedExceptionMessage = string.Format(
                    "Image type is not supported. Supported image types are {0}.",
                    string.Join(", ", SupportedRecognizedMimeTypes.Select(type => string.Format("'{0}'", type)))
                    );
            }

            public FromBase64ImageStringBinding(HttpParameterDescriptor descriptor)
                : base(descriptor)
            {
            }

            public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
                HttpActionContext actionContext, CancellationToken cancellationToken)
            {
                try
                {
                    var body = await actionContext.Request.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(body);

                    var originalDataString = (string) data.image;
                    var mimeType = SupportedRecognizedMimeTypes.FirstOrDefault(type => originalDataString.StartsWith(
                        string.Format("data:{0};base64,", type), StringComparison.InvariantCultureIgnoreCase));
                    if (mimeType == null)
                    {
                        throw new RaiseErrorException(HttpStatusCode.BadRequest, NotSupportedExceptionMessage);
                    }

                    // Get the data string by removing the prefix.
                    var dataString = originalDataString.Substring(mimeType.Length + "data:;base64,".Length);
                    var stream = new MemoryStream(Convert.FromBase64String(dataString));
                    actionContext.ActionArguments[Descriptor.ParameterName] = stream;
                }
                catch (RaiseErrorException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw new RaiseErrorException(HttpStatusCode.BadRequest, "Request is badly formatted.");
                }
            }
        }
    }
}
