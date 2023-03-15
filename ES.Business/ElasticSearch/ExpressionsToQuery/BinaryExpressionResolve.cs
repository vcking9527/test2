using System.Linq.Expressions;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class BinaryExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public BinaryExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = Expression as BinaryExpression;
            var operatorValue = ExpressionTool.GetOperator(expression.NodeType);

            Context.LastQueryBase = operatorValue;

            if (ExpressionTool.IsOperator(expression.NodeType))
                Context.LastOperator = expression.NodeType;

            var leftExpression = expression.Left;
            var rightExpression = expression.Right;

            Expression = leftExpression;
            IsLeft = true;
            Start();

            IsLeft = false;
            Expression = rightExpression;
            Start();
            IsLeft = null;

            Context.SetQuery();
        }
    }
}