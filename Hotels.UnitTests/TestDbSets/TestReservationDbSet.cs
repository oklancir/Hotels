using Hotels.Models;
using StoreApp.Tests;
using System.Linq;

namespace HoteliApp.UnitTesting.TestDbSets
{
    class TestReservationDbSet : TestDbSet<Reservation>
    {
        public override Reservation Find(params object[] keyValues)
        {
            return this.FirstOrDefault(r => r.Id == (int)keyValues.Single());
        }
    }
}
