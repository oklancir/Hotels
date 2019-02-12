using Hotels.Models;
using System.Linq;
using HoteliApp.UnitTesting.TestDbSets;

namespace Hotels.UnitTests.TestDbSets
{
    class TestServiceProductDbSet : TestDbSet<ServiceProduct>
    {
        public override ServiceProduct Find(params object[] keyValues)
        {
            return this.FirstOrDefault(i => i.Id == (int)keyValues.Single());
        }
    }
}
