using System.ComponentModel.DataAnnotations;

namespace HolidayHomeProject.DataAccessLayer.Dto
{
    public class HostBio
    {
        [Key]
        public int HostId { get; set; } // Unique ID for the host

        // The bio of the host
        [Required(ErrorMessage = "Biography is required.")]
        public string Bio { get; set; }

        // Foreign key references to UserAccountDTO
        [Required]
        public int UserId { get; set; }

        /*        // Navigation properties to link with UserAccountDTO
                [Required]
                public UserAccount UserAccount { get; set; }*/

        // Navigation property to houses
        public virtual ICollection<House> Houses { get; set; } = new List<House>();
    }
}
