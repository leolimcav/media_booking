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

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("date should not be empty");
        
        RuleFor(x => x.Date)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("date should be greater than or equal to today");
    }
}
