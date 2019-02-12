using Hotels.Models;
using StoreApp.Tests;
using System.Linq;

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
