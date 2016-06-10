using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Harcourts.Face.WebsiteCommon.Mapping
{
    /// <summary>
    /// Provides auto mapping ability.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    public abstract class AbstractMapper<TSource, TDestination>
    {
        /// <summary>
        /// Maps from the source to the destination.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        public TDestination Map(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(ConfigureMapping);
            var mapper = config.CreateMapper();
            var mapped = mapper.Map(source, destination);
            return mapped;
        }

        /// <summary>
        /// Configures mapping.
        /// </summary>
        /// <param name="config">The mapper configuration.</param>
        protected abstract void ConfigureMapping(IMapperConfiguration config);
    }
}
