using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Models;

namespace NetCore_Learning.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, string role, bool isAccess);
    }
}