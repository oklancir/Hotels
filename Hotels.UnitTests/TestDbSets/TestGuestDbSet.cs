using Hotels.Models;
using StoreApp.Tests;
using System.Linq;

namespace HoteliApp.UnitTesting.TestDbSets
{
    class TestGuestDbSet : TestDbSet<Guest>
    {
        public override Guest Find(params object[] keyValues)
        {
            return this.FirstOrDefault(g => g.Id == (int)keyValues.Single());
        }
    }
}
