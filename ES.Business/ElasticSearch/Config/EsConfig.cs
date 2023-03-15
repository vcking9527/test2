using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class EsConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string Urls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Uri> Uris => Urls.Split(',').Select(x => new Uri(x)).ToList();
    }
}
