using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionsAndIQueryable.Task2
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var res = mapper.Map(new Foo() { Name ="FooName", Id = 666});
        }
    }
}
