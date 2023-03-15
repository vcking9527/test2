using System;
using System.Collections.Generic;

namespace ES.Business.ElasticSearch.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingIndex
    {
        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IndexName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MappingColumn> Columns { get; set; } = new List<MappingColumn>(0);
    }
}