using Hotels.Models;
using StoreApp.Tests;
using System.Linq;

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
