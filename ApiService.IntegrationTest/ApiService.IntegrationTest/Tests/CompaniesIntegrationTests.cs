using System;
using Xunit;

namespace ApiService.IntegrationTest.Tests
{
	public class CompaniesIntegrationTests
	{
		[Fact]
		public async void GetAllCompanies_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var response = await client.GetAsync("/api/companies");
			var result = await response.Content.ReadAsStringAsync();
			// work in progress
		}
	}
}
