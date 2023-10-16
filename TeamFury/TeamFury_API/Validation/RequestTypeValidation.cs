using FluentValidation;
using Models.DTOs;

namespace TeamFury_API.Validation;

public class RequestTypeValidation : AbstractValidator<RequestTypeDto>
{
    public RequestTypeValidation()
    {
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.MaxDays).NotEmpty().GreaterThan(0);
    }
}