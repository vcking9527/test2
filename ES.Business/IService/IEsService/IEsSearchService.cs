using System;
using System.Collections.Generic;
using System.Text;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEsSearchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEsQueryableService<T> Queryable<T>() where T : class;
    }
}
