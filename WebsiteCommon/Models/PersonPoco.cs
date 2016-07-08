using System;

namespace Harcourts.Face.WebsiteCommon.Models
{
    /// <summary>
    /// Represents a person POCO.
    /// </summary>
    public class PersonPoco : IPerson
    {
        /// <summary>
        /// The identity of the person.
        /// </summary>
        public string PersonIdentity { get; set; }

        /// <summary>
        /// The full name of the person.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The work title or position of the person.
        /// </summary>
        public string WorkTitleOrPosition { get; set; }

        /// <summary>
        /// The email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The URI to the photo of the person.
        /// </summary>
        public Uri PhotoUri { get; set; }

        /// <summary>
        /// The URI to the profile page of the person.
        /// </summary>
        public Uri ProfileUri { get; set; }
    }
}
