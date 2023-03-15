using System.Linq.Expressions;
using ES.Business.ElasticSearch.Mapping;
using Nest;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ExpressionsGetQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="mappingIndex"></param>
        /// <returns></returns>
        public static QueryContainer GetQuery(Expression expression, MappingIndex mappingIndex)
        {
            var parameter = new ExpressionParameter {CurrentExpression = expression, Context = new ExpressionContext(mappingIndex)};
            new BaseResolve(parameter).Start();
            return parameter.Context.QueryContainer;
        }
    }
}