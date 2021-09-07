using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsAndIQueryable.Task2
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource), "source");

            var sourceProperties = typeof(TSource).GetProperties();
            var targetProperties = typeof(TDestination).GetProperties();

            var memberAssignment = new List<MemberAssignment>();
            foreach (var sourceProperty in sourceProperties) {
                foreach (var targetProperty in targetProperties) {
                    if (sourceProperty.Name == targetProperty.Name && sourceProperty.PropertyType == targetProperty.PropertyType) 
                    {
                        var memberProperty = Expression.Property(sourceParam, sourceProperty.Name);
                        memberAssignment.Add(Expression.Bind(
                            targetProperty,
                            memberProperty
                        ));
                        break;
                    }
                }
            }

            MemberInitExpression body = Expression.MemberInit(
                Expression.New(typeof(TDestination).GetConstructor(Type.EmptyTypes)),
                memberAssignment
            );

            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(
                body,
                sourceParam
                );

            Console.WriteLine(mapFunction);
            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}
