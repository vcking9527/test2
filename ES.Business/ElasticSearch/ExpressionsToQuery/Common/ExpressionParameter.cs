using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ExpressionParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public ExpressionContext Context { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExpressionParameter BaseParameter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Expression BaseExpression { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Expression CurrentExpression { get; set; }
    }
}
