using System.ComponentModel.DataAnnotations;

namespace HolidayHomeProject.DataAccessLayer.Dto
{
    public class House
    {
        [Key]
        public int HouseId { get; set; } // Unique ID for the house

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } // The address of the house
        public string? Description { get; set; } // Description of the house
        
        [Required]
        [Range(0, 20, ErrorMessage = "Maximum People can be 1 to 20 for stay")]
        public int MaxPeopleNo { get; set; } // Max number of people that can live in the house

        [Required]
        [Range(0, 10000, ErrorMessage = "Rent price must be between 0 and 10000.")]
        public decimal RentPricePerDay { get; set; } // Rent price per day for the house

        // String to store the image URLs (path after saving)
        public string? ThumbnailImagePath { get; set; } // Path for thumbnail image
        public List<string>? HouseImagesPaths { get; set; } = new List<string>(); // Paths for house images
        public string? BodyImage1Path { get; set; } // Path for body image 1
        public string? BodyImage2Path { get; set; } // Path for body image 2

        public bool IsPermissionRequired { get; set; }
        public List<string>? Permissions { get; set; }

        public int HostId { get; set; }

        // Navigation property back to host
        public virtual HostBio Host { get; set; }
    }
}
