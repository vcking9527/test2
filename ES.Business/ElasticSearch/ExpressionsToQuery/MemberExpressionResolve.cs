using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public MemberExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var leftexp = Expression as MemberExpression;
            var memberName = leftexp.Member.Name;

            Context.LastFiled = memberName;
        }

        /// <summary>
        /// 
        /// </summary>
        public ExpressionParameter Parameter { get; set; }
    }
}