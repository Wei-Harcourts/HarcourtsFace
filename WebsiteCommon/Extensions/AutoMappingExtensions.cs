
namespace AutoMapper
{
    /// <summary>
    /// Extensions for auto mapping.
    /// </summary>
    public static class AutoMappingExtensions
    {
        /// <summary>
        /// Ignores mapping for all members.
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllMembers<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mapping)
        {
            mapping.ForAllMembers(cfg => cfg.Ignore());
            return mapping;
        }
    }
}