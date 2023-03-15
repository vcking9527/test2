using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ES.Business.IService;
using ES.Business.ElasticSearch;
using ES.Business.ElasticSearch.Mapping;
using Nest;

namespace ES.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EsQueryableService<T> : IEsQueryableService<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IElasticClient _client;
        /// <summary>
        /// 
        /// </summary>
        private readonly MappingIndex _mappingIndex;
        /// <summary>
        /// 
        /// </summary>
        private readonly ISearchRequest _request;
        /// <summary>
        /// 
        /// </summary>
        private long _totalNumber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mappingIndex"></param>
        /// <param name="client"></param>
        public EsQueryableService(MappingIndex mappingIndex, IElasticClient client)
        {
            _mappingIndex = mappingIndex;
            _request = new SearchRequest(_mappingIndex.IndexName) {Size = 10000};
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        public QueryContainer QueryContainer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEsQueryableService<T> Where(Expression<Func<T, bool>> expression)
        {
            _Where(expression);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<T> ToList()
        {
            return _ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> ToListAsync()
        {
            return await _ToListAsync<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual List<T> ToPageList(int pageIndex, int pageSize)
        {
            _request.From = ((pageIndex < 1 ? 1 : pageIndex) - 1) * pageSize;
            _request.Size = pageSize;
            return _ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalNumber"></param>
        /// <returns></returns>
        public virtual List<T> ToPageList(int pageIndex, int pageSize, ref long totalNumber)
        {
            var list = ToPageList(pageIndex, pageSize);
            totalNumber = _totalNumber;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual IEsQueryableService<T> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc)
        {
            _OrderBy(expression, type);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEsQueryableService<T> GroupBy(Expression<Func<T, object>> expression)
        {
            _GroupBy(expression);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        private static void _GroupBy(Expression expression)
        {
            // var propertyName = ReflectionExtensionHelper.GetProperty(expression as LambdaExpression).Name;
            // propertyName = _mappingIndex.Columns.FirstOrDefault(x => x.PropertyName == propertyName)?.SearchName ?? propertyName;
            // _request.Aggregations = new AggregationDictionary
            // {
            //     TermQuery = new TermsAggregation(propertyName)
            //     {
            //         Field = propertyName,
            //         Size = 1000
            //     }
            // };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="type"></param>
        private void _OrderBy(Expression expression, OrderByType type = OrderByType.Asc)
        {
            var propertyName = ReflectionExtensionHelper.GetProperty(expression as LambdaExpression).Name;
            propertyName = _mappingIndex.Columns.FirstOrDefault(x => x.PropertyName == propertyName)?.SearchName ?? propertyName;
            _request.Sort = new ISort[]
            {
                new FieldSort
                {
                    Field = propertyName,
                    Order = type == OrderByType.Asc ? SortOrder.Ascending : SortOrder.Descending
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        private List<TResult> _ToList<TResult>() where TResult : class
        {
            _request.Query = QueryContainer;

            var response = _client.Search<TResult>(_request);

            if (!response.IsValid)
                throw new Exception($"查询失败:{response.OriginalException.Message}");

            _totalNumber = response.Total;
            return response.Documents.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        private async Task<List<TResult>> _ToListAsync<TResult>() where TResult : class
        {
            _request.Query = QueryContainer;

            var response = await _client.SearchAsync<TResult>(_request);

            if (!response.IsValid)
                throw new Exception($"查询失败:{response.OriginalException.Message}");
            _totalNumber = response.Total;
            return response.Documents.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        private void _Where(Expression expression)
        {
            QueryContainer = ExpressionsGetQuery.GetQuery(expression, _mappingIndex);
        }
    }
}