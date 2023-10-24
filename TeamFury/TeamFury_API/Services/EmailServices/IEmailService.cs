using Models.Models;

namespace TeamFury_API.Services.EmailServices
{
    public interface IEmailService
    {
       Task SendEmail(LeaveDays user);
    }
}
