using UkrainiansId.Domain.Interfaces;
using UkrainiansId.Domain.Models;
using UkrainiansId.Infrastructure.Data.EntityFramework.Context;

namespace UkrainiansId.Infrastructure.Data.Repositories.EF
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(UkrainiansIdContext _db) : base(_db) { }
    }
}