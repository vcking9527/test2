﻿using System.Linq.Expressions;
using System.Reflection;

namespace ES.Business.ElasticSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionExtensionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        public static MemberInfo GetProperty(LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (; ; )
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        {
                            var memberExpression = (MemberExpression)expr;
                            var mi = memberExpression.Member;
                            return mi;
                        }
                    default:
                        return null;
                }
        }
    }
}