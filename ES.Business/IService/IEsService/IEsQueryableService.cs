using ES.Business.ElasticSearch;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEsQueryableService<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEsQueryableService<T> Where(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<T> ToList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ToListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> ToPageList(int pageIndex, int pageSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalNumber"></param>
        /// <returns></returns>
        List<T> ToPageList(int pageIndex, int pageSize, ref long totalNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IEsQueryableService<T> OrderBy(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEsQueryableService<T> GroupBy(Expression<Func<T, object>> expression);
    }
}