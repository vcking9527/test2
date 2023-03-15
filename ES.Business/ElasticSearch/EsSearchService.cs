using System;
using ES.Business.IService;
using ES.Business.ElasticSearch;
using ES.Business.ElasticSearch.Mapping;
using Nest;

namespace ES.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EsSearchService : IEsSearchService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IElasticClient _elasticClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="esClientProvider"></param>
        public EsSearchService(IEsClientService esClientProvider)
        {
            _elasticClient = esClientProvider.Client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEsQueryableService<T> Queryable<T>() where T : class
        {
            var mapping = InitMappingInfo<T>();
            return new EsQueryableService<T>(mapping, _elasticClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static MappingIndex InitMappingInfo<T>()
        {
            return InitMappingInfo(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static MappingIndex InitMappingInfo(Type type)
        {
            var mapping = new MappingIndex {Type = type, IndexName = type.Name};
            foreach (var property in type.GetProperties())
                mapping.Columns.Add(new MappingColumn
                {
                    PropertyInfo = property.PropertyType,
                    PropertyName = property.Name,
                    SearchName = FiledHelp.GetValues(property.PropertyType.Name, property.Name)
                });
            return mapping;
        }
    }
}