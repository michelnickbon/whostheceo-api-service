using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiService.Dtos
{
	public class HistoryPostDto
	{
		public int HistoryId { get; set; }
		public int? CompanyId { get; set; }
		public DateTime? LoggedAt { get; set; }
	}
}
