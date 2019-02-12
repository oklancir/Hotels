using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Models;

namespace Hotels.UnitTests.TestDbSets
{
    class TestRoomTypeDbSet : TestDbSet<RoomType>
    {
        public override RoomType Find(params object[] keyValues)
        {
            return this.FirstOrDefault(rt => rt.Id == (int)keyValues.Single());
        }
    }
}
