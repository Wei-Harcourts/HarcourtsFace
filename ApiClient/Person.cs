using System;

namespace Harcourts.Face.Client
{
    public class Person
    {
        public Guid PersonIdentity { get; set; }
        public string FullName { get; set; }
        public string WorkTitleOrPosition { get; set; }
        public string EmailAddress { get; set; }
        public Uri PhotoUri { get; set; }
        public Uri ProfileUri { get; set; }
    }
}
