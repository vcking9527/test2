using System.Collections.Generic;
using System.Threading.Tasks;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEsIndexService
    {
        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<bool> IndexExistsAsync(string index);

        /// <summary>
        ///新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task<bool> InsertAsync<T>(T entity, string index = "") where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task<bool> InsertRangeAsync<T>(IEnumerable<T> entity, string index = "") where T : class;
        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task RemoveIndex(string index);
    }
}