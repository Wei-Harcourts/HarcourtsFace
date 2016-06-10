using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.WebsiteCommon.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// The full name of the person.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// The work title or position of the person.
        /// </summary>
        string WorkTitleOrPosition { get; }

        /// <summary>
        /// The email address of the person.
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// The URI to the photo of the person.
        /// </summary>
        Uri PhotoUri { get; }

        /// <summary>
        /// The URI to the profile page of the person.
        /// </summary>
        Uri ProfileUri { get; }
    }
}
