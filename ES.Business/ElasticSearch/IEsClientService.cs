using Nest;

namespace ES.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEsClientService
    {
        /// <summary>
        /// 
        /// </summary>
        ElasticClient Client { get; }
    }
}
