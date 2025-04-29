using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetCore_Learning.Models
{
    public class AppUser : IdentityUser
    {
        public List<UserStock> UserStock { get; set; } = new List<UserStock>();
        public List<Comment> Comment { get; set; } = new List<Comment>();
        [ForeignKey(nameof(Org))]
        public Guid? org_id { get; set; }
        public Org? Org { get; set; }
    }
}