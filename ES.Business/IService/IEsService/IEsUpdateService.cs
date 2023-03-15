using Nest;

namespace ES.Business.IService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEsUpdateService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IUpdateResponse<T> Update<T>(string key, T entity, string index = "") where T : class;
    }
}
