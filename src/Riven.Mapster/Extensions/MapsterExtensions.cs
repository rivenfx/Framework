using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Riven
{
    public static class MapsterExtensions
    {
        /// <summary>
        /// Mapping to a new object. 
        /// creates the destination object and maps values to it.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source, TypeAdapterConfig config = null)
        {
            return source.Adapt<TDestination>(config);
        }

        /// <summary>
        /// Mapping to an existing object.
        /// 
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source, TDestination destination, TypeAdapterConfig config = null)
        {
            return source.Adapt(destination, config);
        }

        /// <summary>
        /// Mapster also provides extensions to map queryables.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="sourceQueryable"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable sourceQueryable, TypeAdapterConfig config = null)
        {
            if (config == null)
            {
                return sourceQueryable.ProjectToType<TDestination>();
            }

            return sourceQueryable.ProjectToType<TDestination>(config);
        }
    }
}
