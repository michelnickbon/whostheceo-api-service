using Xunit;
using ApiService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiService.IntegrationTest.Tests
{
	public class CompaniesIntegrationTests
	{
		[Fact]
		public async void GetAllCompanies_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var response = await client.GetAsync("/api/companies");
			var content = await response.Content.ReadAsStringAsync();
			var entities = JsonConvert.DeserializeObject<List<Company>>(content);

			response.EnsureSuccessStatusCode();
			Assert.NotNull(response);
			Assert.NotNull(entities[0].GetType().GetProperty("CompanyId"));
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}

		[Fact]
		public async void GetCompanyList_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var response = await client.GetAsync("/api/Companies/List");
			var content = await response.Content.ReadAsStringAsync();

			response.EnsureSuccessStatusCode();
			Assert.NotNull(response);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}

		[Fact]
		public async void Post_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var response = await client.GetAsync("/api/Companies/List");
			var content = await response.Content.ReadAsStringAsync();

			response.EnsureSuccessStatusCode();
			Assert.NotNull(response);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}

	}
}
