using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class LambdaExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public LambdaExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var lambda = Expression as LambdaExpression;
            var expression = lambda.Body;
            Expression = expression;
            Start();
        }
    }
}