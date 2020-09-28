using System;

namespace ApiService.Models
{
    public partial class History
    {
        public int HistoryId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? LoggedAt { get; set; }

        public virtual Company Company { get; set; }
    }
}
