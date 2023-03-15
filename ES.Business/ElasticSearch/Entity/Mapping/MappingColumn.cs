using System;

namespace ES.Business.ElasticSearch.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingColumn
    {
        /// <summary>
        /// 
        /// </summary>
        public Type PropertyInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SearchName { get; set; }
    }
}