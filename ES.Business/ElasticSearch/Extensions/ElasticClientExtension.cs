using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Nest;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public static class ElasticClientExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elasticClient"></param>
        /// <param name="indexName"></param>
        /// <param name="numberOfShards"></param>
        /// <param name="numberOfReplicas"></param>
        /// <returns></returns>
        public static async Task CreateIndexAsync<T>(this ElasticClient elasticClient, string indexName = "", int numberOfShards = 3, int numberOfReplicas = 1) where T : class
        {
            if (string.IsNullOrWhiteSpace(indexName)) throw new ArgumentException("索引名称不可为空");

            if (!(await elasticClient.Indices.ExistsAsync(indexName)).Exists)
            {
                //var indexState = new IndexState
                //{
                //    Settings = new IndexSettings
                //    {
                //        NumberOfReplicas = numberOfReplicas,
                //        NumberOfShards = numberOfShards

                //        // index.blocks.read_only：设为true,则索引以及索引的元数据只可读
                //        // index.blocks.read_only_allow_delete：设为true，只读时允许删除。
                //        // index.blocks.read：设为true，则不可读。
                //        // index.blocks.write：设为true，则不可写。
                //        // index.blocks.metadata：设为true，则索引元数据不可读写
                //    }
                //};
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("index.number_of_shards", numberOfShards);
                dict.Add("index.number_of_replicas", numberOfReplicas);
                dict.Add("index.max_result_window", 2000000000);
                dict.Add("index.refresh_interval", "5s");

                var indexState = new IndexState
                {
                    Settings = new IndexSettings(dict)

                };
                var response = await elasticClient.Indices.CreateAsync(indexName, p => p.InitializeUsing(indexState).Map<T>(x => x.AutoMap()));
                if (!response.IsValid)
                {
                   
                    throw new Exception($"创建索引失败:{response.OriginalException.Message}");

                }
            }
        }
    }
}