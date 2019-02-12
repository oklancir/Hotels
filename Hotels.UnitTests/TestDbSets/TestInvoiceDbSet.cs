using Hotels.Models;
using System.Linq;
using HoteliApp.UnitTesting.TestDbSets;

namespace Hotels.UnitTests.TestDbSets
{
    class TestInvoiceDbSet : TestDbSet<Invoice>
    {
        public override Invoice Find(params object[] keyValues)
        {
            return this.FirstOrDefault(i => i.Id == (int)keyValues.Single());
        }
    }
}
