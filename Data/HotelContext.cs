

namespace BlueHaisAnisHotelManagement.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {


        }

        public virtual DbSet<RoomUnits> RoomUnits { get; set; }
        public virtual DbSet<RoomProfile> RoomProfiles { get; set; }

    }
}
