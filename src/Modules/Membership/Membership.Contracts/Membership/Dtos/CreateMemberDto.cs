

using Authentication.Contracts.Authentication.Dtos;
using Shared.Constants;
using Shared.Enums;

namespace Membership.Contracts.Membership.Dtos
{
    public class CreateMemberDto : RegisterUserDto
    {
        public override string UserRole { get; set; } = DefaultRoles.MemberRole;
        public Gender Gender { get; set; }


    }
}