using Shared.Enums;
using StaffManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Contracts.Authentication.Dtos
{
    public record RegisterUserDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Surname is required.")]
        [MaxLength(50, ErrorMessage = "Surname must be at most 50 characters long.")]
        public string Surname { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Telephone number is required.")]
        [Phone(ErrorMessage = "Invalid telephone number format.")]
        [MaxLength(15, ErrorMessage = "Telephone number must be at most 15 characters long.")]
        public string Telephone { get; init; }

        public string UserRole { get; set; }

        public Gender Gender { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
    }


}
