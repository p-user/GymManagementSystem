﻿namespace Membership.Membership.Features.Member.CreateMember
{
    public class CreateMemberEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/membership/GymMembers", CreateMember)
            .WithDescription("Creates a new member for the system")
            .WithName("CreateMember")
            .WithTags("Member")
            .Produces<string>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new member");
        }

        private async Task<IResult> CreateMember(ISender sender, [FromBody] CreateMemberDto dto)
        {
            Results<string> response = await sender.Send(new CreateMemberCommand(dto));
            return response.Match(success => Microsoft.AspNetCore.Http.Results.Ok(success), ApiResults.Problem);


        }
    }
}
