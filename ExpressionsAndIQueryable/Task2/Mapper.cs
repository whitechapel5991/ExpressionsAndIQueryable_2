using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsAndIQueryable.Task2
{
    public class Mapper<TSource, TDestination>
    {
        private readonly Func<TSource, TDestination> func;
        public Mapper(Func<TSource,TDestination> func)
        {
            this.func = func;
        }

        public TDestination Map(TSource source)
        {
            return func(source);
        }
    }
}