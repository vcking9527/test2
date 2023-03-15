using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResolve(ExpressionParameter parameter)
        {
            Expression = parameter.CurrentExpression;
            Context = parameter.Context;
            BaseParameter = parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        protected Expression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Expression ExactExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected ExpressionContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool? IsLeft { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private ExpressionParameter BaseParameter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseResolve Start()
        {
            var expression = Expression;
            var parameter = new ExpressionParameter
            {
                Context = Context,
                CurrentExpression = expression,
                BaseExpression = ExactExpression,
                BaseParameter = BaseParameter
            };
            return expression switch
            {
                //else if (expression is BinaryExpression && expression.NodeType == ExpressionType.Coalesce)
                //{
                //    return new CoalesceResolveItems(parameter);
                //}
                LambdaExpression _ => new LambdaExpressionResolve(parameter),
                //else if (expression is BlockExpression)
                //{
                //    Check.ThrowNotSupportedException("BlockExpression");
                //}
                //else if (expression is ConditionalExpression)
                //{
                //    return new ConditionalExpressionResolve(parameter);
                //}
                BinaryExpression _ => new BinaryExpressionResolve(parameter),
                //else if (expression is MemberExpression && ((MemberExpression)expression).Expression == null)
                //{
                //    return new MemberNoExpressionResolve(parameter);
                //}
                MethodCallExpression _ => new MethodCallExpressionResolve(parameter),
                //else if (expression is MemberExpression && ((MemberExpression)expression).Expression.NodeType == ExpressionType.New)
                //{
                //    return new MemberNewExpressionResolve(parameter);
                //}
                MemberExpression memberExpression when memberExpression.Expression.NodeType == ExpressionType.Constant => new MemberConstExpressionResolve(parameter),
                ConstantExpression _ => new ConstantExpressionResolve(parameter),
                MemberExpression _ => new MemberExpressionResolve(parameter),
                _ => null
            };
        }
    }
}