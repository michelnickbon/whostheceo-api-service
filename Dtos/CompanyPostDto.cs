using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiService.Dtos
{
	public class CompanyPostDto
	{
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? CeoId { get; set; }
        public string CompanyDescription { get; set; }
    }
}
