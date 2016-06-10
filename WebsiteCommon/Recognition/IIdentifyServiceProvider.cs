using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Harcourts.Face.WebsiteCommon.Lookups;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteCommon.Recognition
{
    /// <summary>
    /// Provides methods to identify persons within specified person groups.
    /// </summary>
    public interface IIdentifyServiceProvider<in TImage, TKey>
    {
        /// <summary>
        /// Identifies persons from the specified image and returns 
        /// </summary>
        /// <param name="image"></param>
        Task<PersonLookupKey<TKey>[]> Identify(TImage image);
    }
}
