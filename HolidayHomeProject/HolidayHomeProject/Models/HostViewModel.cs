using System.ComponentModel.DataAnnotations;
using HolidayHomeProject.DataAccessLayer.Dto;
using Microsoft.AspNetCore.Identity;

namespace HolidayHomeProject.Models
{
	public class HostViewModel
    {

        [Required]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "Host bio is required.")]
        public string Bio { get; set; }

        // House details
        [Required(ErrorMessage = "House address is required.")]
        public string Address { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Please enter a value between 1 and 20.")]
        public int MaxPeopleNo { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Please enter a valid rent price.")]
        public decimal RentPricePerDay { get; set; }

        // File uploads for house images
        public IFormFile? ThumbnailImage { get; set; }
        public List<IFormFile>? HouseImages { get; set; } = new List<IFormFile>();
        public IFormFile? BodyImage1 { get; set; }
        public IFormFile? BodyImage2 { get; set; }

        // Permissions
        public bool IsPermissionRequired { get; set; }
        public List<string>? Permissions { get; set; } = new List<string>();

    }
}
