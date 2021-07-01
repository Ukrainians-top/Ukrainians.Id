using System.Threading.Tasks;

namespace UkrainiansId.Application.Services.Intefaces
{
    public interface IAuthenticationService
    {
        Task LoginByEmailAsync(string login, string password);
        Task LoginByGoogleAsync();
        Task LoginByFacebookAsync();
        Task LoginByMicrosoftAsync();
        Task LoginByTwitterAsync();
    }
}