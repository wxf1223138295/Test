using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Identity.HttpClientResilience.User;

namespace User.Identity.Services
{
    public class AuthCodeService : IAuthCodeService
    {
        public async Task<bool> ValidateAuthCode()
        {
            

            return true;
        }
    }

}
