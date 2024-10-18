﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HolidayHomeProject.DataAccessLayer.Dto
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
		[MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]
		public string Password { get; set; }

        public bool IsHost {  get; set; }
    }
}
