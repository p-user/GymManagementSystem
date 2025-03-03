
namespace StaffManagement.StaffManagement.Features.Trainer.CreateTrainer
{
   
    public record class CreateTrainerCommand(RegisterUserDto dto) : IRequest<CreateTrainerCommandResponse>;
    public record CreateTrainerCommandResponse(string message);

    public class CreateTrainerCommandHandler(StaffDbContext staffDbContext, ISender _sender): IRequestHandler<CreateTrainerCommand, CreateTrainerCommandResponse>
    {
        public async Task<CreateTrainerCommandResponse> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
        {
            //set to make sure that the user is a trainer
            request.dto.UserRole = DefaultRoles.TrainerRole;

            var response = await _sender.Send(new RegisterUserCommand(request.dto));
            if (response.UserId == Guid.Empty) { throw new Exception("AuthenticationId was not provided! Something went wrong");  }

            //register the user as a trainer
            var trainer = CreateTrainer(request.dto, response.UserId);
            var added = await staffDbContext.Trainers.AddAsync(trainer);
            await staffDbContext.SaveChangesAsync(cancellationToken);

            return new CreateTrainerCommandResponse(response.message);
        }

        private Models.Trainer CreateTrainer(RegisterUserDto dto, Guid userId)
        {
           var fullName = new Models.FullName(dto.Name, dto.Surname);
           var contact = new Models.ContactInfo(dto.Email, dto.Telephone); 

           return Models.Trainer.Create(userId, fullName, dto.DateOfBirth, dto.Gender, contact, dto.EmploymentType, null, dto.HireDate, null, null);

        }
    }
    
}
