using Xunit;
using ApiService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using ApiService.Dtos;
using System.Net.Http;
using System.Text;

namespace ApiService.IntegrationTest.Tests
{
	public class CompaniesIntegrationTests
	{
		[Fact]
		public async void GetAllCompanies_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var request = await client.GetAsync("/api/companies");
			var response = await request.Content.ReadAsStringAsync();
			var entities = JsonConvert.DeserializeObject<List<Company>>(response);

			request.EnsureSuccessStatusCode();
			Assert.NotNull(request);
			Assert.NotNull(entities[0].GetType().GetProperty("CompanyId"));
			Assert.Equal("application/json; charset=utf-8", request.Content.Headers.ContentType.ToString());
		}

		[Fact]
		public async void GetCompanyList_ShouldReturnOK()
		{
			using var client = new TestClientProvider().Client;
			var request = await client.GetAsync("/api/Companies/List");
			var response = await request.Content.ReadAsStringAsync();

			request.EnsureSuccessStatusCode();
			Assert.NotNull(request);
			Assert.Equal("application/json; charset=utf-8", request.Content.Headers.ContentType.ToString());
		}

		[Fact]
		public async void Post_ShouldReturnCreatedCompany()
		{
			var company = new CompanyPostDto();
			company.CompanyName = "TestCompany";
			company.CompanyDescription = "Test description";

			var json = JsonConvert.SerializeObject(company);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using var client = new TestClientProvider().Client;
			var request = await client.PostAsync("/api/Companies", content);
			var response = await request.Content.ReadAsStringAsync();
			var createdCompany = JsonConvert.DeserializeObject<Company>(response);

			request.EnsureSuccessStatusCode();
			Assert.Equal(createdCompany.CompanyName, company.CompanyName);
			Assert.Equal(createdCompany.CompanyDescription, company.CompanyDescription);
		}
		
		[Fact]
		public async void Put_ShouldReturnOK()
		{
			var company = new CompanyPostDto();
			company.CompanyId = 1;
			company.CompanyName = "NewTestCompany";
			company.CompanyDescription = "New test description";

			var json = JsonConvert.SerializeObject(company);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using var client = new TestClientProvider().Client;	
			var request = await client.PutAsync("/api/Companies/" + company.CompanyId, content);

			request.EnsureSuccessStatusCode();
		}

		[Fact]
		public async void Delete_ShouldReturnOK()
		{
			var companyId = 5; // Temp solution
			using var client = new TestClientProvider().Client;
			var request = await client.DeleteAsync("/api/Companies/" + companyId);
			request.EnsureSuccessStatusCode();
		}

	}
}
