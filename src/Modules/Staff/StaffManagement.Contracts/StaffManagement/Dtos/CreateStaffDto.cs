

using Authentication.Contracts.Authentication.Dtos;
using Shared.Enums;
using StaffManagement.Models;

namespace StaffManagement.Contracts.StaffManagement.Dtos
{

    public class CreateStaffDto : RegisterUserDto
    {

        public Gender Gender { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public override string UserRole { get; set; }

    }

}
