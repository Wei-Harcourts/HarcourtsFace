using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;

namespace Harcourts.Face.WebsiteCommon.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    public class Person : IPerson
    {
        private readonly string _personIdentity;
        private readonly string _fullname;
        private readonly string _workTitleOrPosition;
        private readonly string _emailAddress;
        private readonly string _photoUriString;
        private readonly string _profileUriString;

        /// <summary>
        /// Initializes with raw data.
        /// </summary>
        public Person(IPerson person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            _personIdentity = person.PersonIdentity;
            _fullname = person.FullName;
            _workTitleOrPosition = person.WorkTitleOrPosition;
            _emailAddress = person.EmailAddress;
            _photoUriString = (person.PhotoUri == null) ? null : person.PhotoUri.AbsoluteUri;
            _profileUriString = (person.ProfileUri == null) ? null : person.ProfileUri.AbsoluteUri;
        }

        /// <summary>
        /// The identity of the person.
        /// </summary>
        public string PersonIdentity
        {
            get { return _personIdentity; }
        }

        /// <summary>
        /// The full name of the person.
        /// </summary>
        public string FullName
        {
            get { return _fullname; }
        }

        /// <summary>
        /// The work title or position of the person.
        /// </summary>
        public string WorkTitleOrPosition
        {
            get { return _workTitleOrPosition; }
        }

        /// <summary>
        /// The email address of the person.
        /// </summary>
        public string EmailAddress
        {
            get { return _emailAddress; }
        }

        /// <summary>
        /// The URI to the photo of the person.
        /// </summary>
        public Uri PhotoUri
        {
            get { return new Uri(_photoUriString, UriKind.Absolute); }
        }

        /// <summary>
        /// The URI to the profile page of the person.
        /// </summary>
        public Uri ProfileUri
        {
            get { return new Uri(_profileUriString, UriKind.Absolute); }
        }

        /// <summary>
        /// Returns the person name.
        /// </summary>
        public override string ToString()
        {
            return FullName;
        }
    }
}
