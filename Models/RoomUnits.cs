using System.ComponentModel.DataAnnotations;

namespace BlueHaisAnisHotelManagement.Models
{
    public class RoomUnits
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(75)]
        public string  Description { get; set; }
    }
}
