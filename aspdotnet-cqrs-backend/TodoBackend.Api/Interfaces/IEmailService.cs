using System.Threading.Tasks;
using TodoBackend.Api.Data.ViewModels;

namespace TodoBackend.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(UserViewModel userTo, UserViewModel userFrom, string subject, string  textContent, string htmlContent);
    }

}
