using UkrainiansId.Domain.Interfaces;
using UkrainiansId.Domain.Models;
using UkrainiansId.Infrastructure.Data.EntityFramework.Context;
namespace UkrainiansId.Infrastructure.Data.Repositories.EF
{
    public class AuthenticationDataRepository : EFRepository<AuthenticationData>, IAuthenticationDataRepository
    {
        public AuthenticationDataRepository(UkrainiansIdContext _db) : base(_db) { }
    }
}