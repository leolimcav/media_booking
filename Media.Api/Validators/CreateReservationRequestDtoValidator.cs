using FluentValidation;
using Media.Api.Endpoints.Reservations;

namespace Media.Api.Validators;

public sealed class CreateReservationRequestDtoValidator : Validator<CreateReservationRequestDto>
{
    public CreateReservationRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("name should not be empty")
            .MinimumLength(3)
            .WithMessage("name should have at least 3 characters");

        RuleFor(x => x.Device)
            .NotEmpty()
            .WithMessage("device should not be empty")
            .MinimumLength(2)
            .WithMessage("device name should have at least 2 characters");

        RuleFor(x => x.Classroom)
            .NotEmpty()
            .WithMessage("classroom should not be empty")
            .MinimumLength(2)
            .WithMessage("classroom should have at least 2 characters");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("start date should not be empty")
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("start date should be greater than or equal to today");
        
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("end date should not be empty")
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("end date should be greater than or equal to today");
        
        RuleFor(x => x.EndDate.Day)
            .Equal(x => x.StartDate.Day)
            .WithMessage("end date day should be equal to start date day");
    }
}
