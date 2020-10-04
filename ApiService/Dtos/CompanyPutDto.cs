namespace ApiService.Dtos
{
	public class CompanyPutDto
	{
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? CeoId { get; set; }
        public string CompanyDescription { get; set; }
    }
}
