using Hotels.Models;
using System.Linq;

namespace HoteliApp.UnitTesting.TestDbSets
{
    class TestRoomDbSet : TestDbSet<Room>
    {
        public override Room Find(params object[] keyValues)
        {
            return this.FirstOrDefault(r => r.Id == (int)keyValues.Single());
        }
    }
}
