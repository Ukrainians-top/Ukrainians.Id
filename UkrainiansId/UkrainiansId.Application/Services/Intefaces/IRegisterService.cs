using System.Threading.Tasks;

namespace UkrainiansId.Application.Services.Intefaces
{
    public interface IRegisterService
    {
        Task RegisterEmailAsync();
        Task RegisterGoogleAsync();
        Task RegisterFacebookAsync();
        Task RegisterTwitterAsync();
        Task RegisterMicrosoftAsync();
    }
}