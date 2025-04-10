using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetCore_Learning.Models
{
    public class AppUser : IdentityUser
    {
        public List<UserStock> UserStock { get; set; } = new List<UserStock>();
        public List<Comment> Comment { get; set; } = new List<Comment>();
    }
}