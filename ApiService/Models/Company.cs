using System;
using System.Collections.Generic;

namespace ApiService.Models
{
    public partial class Company
    {
        public Company()
        {
            History = new HashSet<History>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? CeoId { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyWebsite { get; set; }

        public virtual Ceo Ceo { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
