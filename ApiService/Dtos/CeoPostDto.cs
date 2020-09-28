using System;

namespace ApiService.Dtos
{
	public class CeoPostDto
	{
        public int CeoId { get; set; }
        public string FullName { get; set; }
        public string ImageRef { get; set; }
        public string ShortBio { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
    }
}
