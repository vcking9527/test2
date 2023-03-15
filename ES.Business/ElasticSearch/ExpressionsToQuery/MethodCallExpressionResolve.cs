using System.Linq;
using System.Linq.Expressions;
using Nest;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class MethodCallExpressionResolve : BaseResolve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public MethodCallExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var express = Expression as MethodCallExpression;
            if (express == null) return;
            var methodName = express.Method.Name;

            if (methodName == "Contains")
            {
                Context.LastQueryBase = new QueryStringQuery();
                NativeExtensionMethod(express);
            }

            Context.SetQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        private void NativeExtensionMethod(MethodCallExpression express)
        {
            var args = express.Arguments.ToList();
            args.Insert(0, express.Object);

            foreach (var item in args)
            {
                Expression = item;
                Start();
            }
        }
    }
}