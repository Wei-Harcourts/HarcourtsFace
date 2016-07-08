using System;
using AutoMapper;
using Harcourts.Face.WebsiteCommon.Mapping;
using Harcourts.Face.WebsiteCommon.Models;

namespace Harcourts.Face.WebsiteService.Mapping
{
    /// <summary>
    /// Maps from dynamic object to person POCO.
    /// </summary>
    internal class ScrawlResultPersonMapper : AbstractMapper<dynamic, PersonPoco>
    {
        /// <summary>
        /// Configures mapping.
        /// </summary>
        /// <param name="config">The mapper configuration.</param>
        protected override void ConfigureMapping(IMapperConfiguration config)
        {
            config.CreateMap<dynamic, PersonPoco>()
                // Ignore all unmapped.
                .IgnoreAllMembers()
                // Explicitly set mapping.
                .ForMember(p => p.EmailAddress, cfg => cfg.ResolveUsing(x => (string) x.emailAddress))
                .ForMember(p => p.FullName, cfg => cfg.ResolveUsing(x => (string) x.personName))
                .ForMember(p => p.PhotoUri, cfg => cfg.ResolveUsing(x => new Uri((string) x.photo, UriKind.Absolute)))
                .ForMember(p => p.ProfileUri,
                    cfg => cfg.ResolveUsing(x => new Uri((string) x.profile, UriKind.Absolute)))
                .ForMember(p => p.WorkTitleOrPosition, cfg => cfg.ResolveUsing(x => (string) x.position))
                ;
        }
    }
}
