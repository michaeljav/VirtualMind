using Currency.Data;
using Currency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Interface
{
    public interface IUserService
    {
        Task<User> GetById(int id);
       // AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

    }
}
