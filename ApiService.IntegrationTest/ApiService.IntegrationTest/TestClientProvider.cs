using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiService.IntegrationTest
{
	public class TestClientProvider
	{
		public HttpClient Client { get; private set; }

		public TestClientProvider()
		{
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
			var server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());
			Client = server.CreateClient();
		}

	}
}
