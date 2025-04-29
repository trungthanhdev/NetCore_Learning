using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Models
{
    [Table("Portfolios")]
    public class UserStock
    {
        [Key]
        public Guid UserStock_id { get; set; }
        [ForeignKey(nameof(Stock))]
        public Guid Stock_id { get; set; }
        public Stock? Stock { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string User_id { get; set; } = "";
        public AppUser? AppUser { get; set; }
        [ForeignKey(nameof(Org))]
        public Guid org_id { get; set; }
        public Org? Org { get; set; }
    }
}