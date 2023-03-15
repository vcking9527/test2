using Nest;
using System;
using System.Linq.Expressions;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEsDeleteService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        DeleteByQueryResponse DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "") where T : class, new();
    }
}
