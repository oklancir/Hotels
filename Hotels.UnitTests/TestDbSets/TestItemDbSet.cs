using Hotels.Models;
using System.Linq;
using HoteliApp.UnitTesting.TestDbSets;

namespace Hotels.UnitTests.TestDbSets
{
    class TestItemDbSet : TestDbSet<Item>
    {
        public override Item Find(params object[] keyValues)
        {
            return this.FirstOrDefault(i => i.Id == (int)keyValues.Single());
        }
    }
}
