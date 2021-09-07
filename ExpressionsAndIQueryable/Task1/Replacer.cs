using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsAndIQueryable
{
    class Replacer : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> _to;
        public Replacer(Dictionary<Expression, Expression> to)
        {
            _to = to;
        }
        public override Expression Visit(Expression node)
        {
            return _to.ContainsKey(node) ? _to[node] : base.Visit(node);
        }
    }
}
