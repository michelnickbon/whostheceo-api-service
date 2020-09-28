using System;
using System.Collections.Generic;

namespace ApiService.Models
{
    public partial class Ceo
    {
        public Ceo()
        {
            Company = new HashSet<Company>();
        }

        public int CeoId { get; set; }
        public string FullName { get; set; }
        public string ImageRef { get; set; }
        public string ShortBio { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
