using System.Linq.Expressions;

namespace ExpressionsAndIQueryable
{
    public class IncrementTransform : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add)
            {
                if (node.Left.NodeType == ExpressionType.Parameter && node.Right.NodeType == ExpressionType.Constant)
                {
                    var rightValue = (int)Expression.Lambda(node.Right).Compile().DynamicInvoke();
                    if (rightValue == 1)
                    {
                        return Expression.Increment((ParameterExpression)node.Left);
                    }
                }
            }

            if (node.NodeType == ExpressionType.Subtract)
            {
                if (node.Left.NodeType == ExpressionType.Parameter && node.Right.NodeType == ExpressionType.Constant)
                {
                    var rightValue = (int)Expression.Lambda(node.Right).Compile().DynamicInvoke();
                    if (rightValue == 1)
                    {
                        return Expression.Decrement((ParameterExpression)node.Left);
                    }
                }
            }

            return base.VisitBinary(node);
        }
    }
}
