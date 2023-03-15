using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ES.Business.ElasticSearch;
using ES.Business.IService;
using Nest;

namespace ES.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class EsService : IEsIndexService, IEsDeleteService, IEsUpdateService, IEsAliasService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IElasticClient _elasticClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="esClientService"></param>
        public EsService(IEsClientService esClientService)
        {
            _elasticClient = esClientService.Client;
        }

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public DeleteByQueryResponse DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "")
            where T : class, new()
        {
            var indexName = index.GetIndex<T>();
            var request = new DeleteByQueryRequest<T>(indexName);
            var response = _elasticClient.DeleteByQuery(request);
            if (!response.IsValid)
            {

            }
            return response;
        }

        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IUpdateResponse<T> Update<T>(string key, T entity, string index = "") where T : class
        {
            var indexName = index.GetIndex<T>();
            var request = new UpdateRequest<T, object>(indexName, key)
            {
                Doc = entity
            };
            //request.Refresh = 0;
            var response = _elasticClient.Update(request);

            if (!response.IsValid)
            {


            }
            return response;
        }

        #endregion

        #region Index
        /// <inheritdoc cref="IEsIndexService.IndexExistsAsync" />
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<bool> IndexExistsAsync(string index)
        {
            var res = await _elasticClient.Indices.ExistsAsync(index);
            return res.Exists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync<T>(T entity, string index = "") where T : class
        {
            var indexName = index.GetIndex<T>();
            var exists = await IndexExistsAsync(indexName);
            if (!exists)
            {
                await ((ElasticClient)_elasticClient).CreateIndexAsync<T>(indexName);
                await AddAliasAsync(indexName, typeof(T).Name);
            }

            var response = await _elasticClient.IndexAsync(entity,
                s => s.Index(indexName));

            if (!response.IsValid)
            {

            }

            return response.IsValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<bool> InsertRangeAsync<T>(IEnumerable<T> entity, string index) where T : class
        {

            var indexName = index.GetIndex<T>();
            var exists = await IndexExistsAsync(indexName);
            if (!exists)
            {
                await ((ElasticClient)_elasticClient).CreateIndexAsync<T>(indexName);
                await AddAliasAsync(indexName, typeof(T).Name);
            }

            var bulkRequest = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>()
            };
            var operations = entity.Select(o => new BulkIndexOperation<T>(o)).Cast<IBulkOperation>().ToList();
            bulkRequest.Operations = operations;
            var response = await _elasticClient.BulkAsync(bulkRequest);

            return response.IsValid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task RemoveIndex(string index)
        {
            var exists = await IndexExistsAsync(index);
            if (!exists) return;
            var response = await _elasticClient.Indices.DeleteAsync(index);

            if (!response.IsValid)
                throw new Exception("删除index失败:" + response.OriginalException.Message);
        }
        #endregion

        #region Alias
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public async Task<BulkAliasResponse> AddAliasAsync(string index, string alias)
        {
            var response = await _elasticClient.Indices.BulkAliasAsync(b => b.Add(al => al
                .Index(index)
                .Alias(alias)));

            if (!response.IsValid)
                throw new Exception("添加Alias失败:" + response.OriginalException.Message);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public async Task<BulkAliasResponse> AddAliasAsync<T>(string alias) where T : class
        {
            return await AddAliasAsync(string.Empty.GetIndex<T>(), alias);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public BulkAliasResponse RemoveAlias(string index, string alias)
        {
            var response = _elasticClient.Indices.BulkAlias(b => b.Remove(al => al
                .Index(index)
                .Alias(alias)));

            if (!response.IsValid)
                throw new Exception("删除Alias失败:" + response.OriginalException.Message);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public BulkAliasResponse RemoveAlias<T>(string alias) where T : class
        {
            //  await _elasticClient.Sql.QueryAsync(x => "");
            return RemoveAlias(string.Empty.GetIndex<T>(), alias);
        }
        #endregion
    }
}