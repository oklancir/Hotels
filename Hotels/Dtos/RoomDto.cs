namespace Hotels.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int RoomTypeId { get; set; }

    }
}