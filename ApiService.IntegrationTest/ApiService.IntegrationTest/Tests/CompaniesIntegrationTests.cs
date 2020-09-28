using System;
using Xunit;

namespace ApiService.IntegrationTest.Tests
{
	public class CompaniesIntegrationTests
	{
		[Fact]
		public async void GetAllCompanies_ShouldReturnOK()
		{
			var client = new TestClientProvider().Client;
			var response = await client.GetAsync("/api/companies");
			// work in progress
		}
	}
}
