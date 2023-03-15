using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberConstExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public MemberConstExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as MemberExpression;
            var value = ExpressionTool.GetMemberValue(expression.Member, expression);
            Context.LastValue = value;
        }
    }
}