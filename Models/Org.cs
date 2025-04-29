using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Models
{
    public class Org
    {
        [Key]
        public Guid? org_id { get; set; }
        public string? org_name { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;
        public bool is_Active { get; set; } = true;
        public List<AppUser>? AspUser { get; set; } = new List<AppUser>();
    }
}