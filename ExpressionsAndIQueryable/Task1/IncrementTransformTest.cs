using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsAndIQueryable
{
    [TestClass]
    public class IncrementTransformTest
    {
        [TestMethod]
        public void AddToIncrementTransformTest()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a + 1) * (a + 5) * (1 + a);
            var result_exp = (new IncrementTransform().VisitAndConvert(source_exp, ""));

            Console.WriteLine(source_exp + " " + source_exp.Compile().Invoke(3));
            Console.WriteLine(result_exp + " " + result_exp.Compile().Invoke(3));
        }

        [TestMethod]
        public void ConstantTransformTest()
        {
            Expression<Func<int, int, int>> source_exp =
                (a, b) => a + (a + 1) * (a + 5) * (1 + b);
            var dictionary = new Dictionary<Expression, Expression>
            {
                { source_exp.Parameters[0], Expression.Constant(3) },
                { source_exp.Parameters[1], Expression.Constant(4) }
            };
            var result_exp = ReplaceParameter(source_exp, dictionary);

            //Console.WriteLine(source_exp + " " + source_exp.Compile().Invoke(3));
            Console.WriteLine(result_exp + " " + result_exp.Compile().Invoke());
        }

        public static Expression<Func<int>> ReplaceParameter
        (
            Expression<Func<int, int, int>> inputExpression,
            Dictionary<Expression, Expression> element
        )
        {
            var replacer = new Replacer(element);
            var body = replacer.Visit(inputExpression.Body);
            return Expression.Lambda<Func<int>>(body);
        }
    }
}
