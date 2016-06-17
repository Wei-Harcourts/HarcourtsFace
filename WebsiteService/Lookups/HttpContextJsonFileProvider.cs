using System.IO;
using System.Web;

namespace Harcourts.Face.WebsiteService.Lookups
{
    /// <summary>
    /// Provides methods to resolve data file based HTTP context.
    /// </summary>
    public class HttpContextJsonFileProvider : IJsonFileProvider
    {
        /// <summary>
        /// Returns the data file name based on current HTTP context.
        /// </summary>
        /// <param name="dataFileName">The data file name.</param>
        public string GetDataFilePath(string dataFileName)
        {
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"),
                dataFileName.TrimStart('/'));
            return path;
        }
    }
}
