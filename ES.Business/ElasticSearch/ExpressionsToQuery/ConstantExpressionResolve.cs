using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ConstantExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public ConstantExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as ConstantExpression;
            var value = ExpressionTool.GetValue(expression.Value);
            Context.LastValue = value;
        }
    }
}