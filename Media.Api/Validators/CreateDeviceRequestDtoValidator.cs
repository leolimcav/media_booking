using FluentValidation;
using Media.Api.Endpoints.Devices;

namespace Media.Api.Validators;

public class CreateDeviceRequestDtoValidator : Validator<CreateDeviceRequestDto>
{
    public CreateDeviceRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("name is required.")
            .MinimumLength(2)
            .WithMessage("name is too short.");
    }
}
