using FluentValidation;
using Models.DTOs;

namespace TeamFury_API.Validation
{
    public class RequestCreateValidation : AbstractValidator<RequestCreateDTO>
    {
        public RequestCreateValidation()
        {
            RuleFor(model => model.StartDate).NotEmpty();
            RuleFor(model => model.EndDate).NotEmpty();
            RuleFor(model => model.RequestTypeID).NotEmpty();
        }

    }
}
