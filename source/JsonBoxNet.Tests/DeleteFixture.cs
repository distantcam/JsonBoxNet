using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using JsonBoxNet.TextJson;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace JsonBoxNet.Tests
{
	[TestFixture]
	public abstract class DeleteFixtureBase
	{
		protected abstract JsonBox CreateJsonBox(HttpClient client);

		JsonBox CreateJsonBox(string path, string message)
		{
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Delete, $"/testbox___jsonboxnet{path}")
				.Respond("application/json", $"{{\"message\": \"{message}\"}}");
			return CreateJsonBox(mockHttp.ToHttpClient());
		}

		[Test]
		public async Task DeleteRecord()
		{
			var fixture = new Fixture();
			var message = fixture.Create<string>();
			var id = Guid.NewGuid().ToString();
			var jsonBox = CreateJsonBox("/" + id, message);

			var result = await jsonBox.DeleteAsync(id);

			result.Message.Should().Be(message);
		}

		[Test]
		public async Task DeleteMultipleRecord()
		{
			var fixture = new Fixture();
			var message = fixture.Create<string>();
			var jsonBox = CreateJsonBox("?q=Name:Foo", message);

			var result = await jsonBox.DeleteQueryAsync("Name:Foo");

			result.Message.Should().Be(message);
		}
	}

	public class NewtonsoftJsonBox_DeleteFixture : DeleteFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new NewtonsoftJsonBox(client, "testbox___jsonboxnet", new JsonSerializer());
	}

	public class SystemTextJsonBox_DeleteFixture : DeleteFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new SystemTextJsonBox(client, "testbox___jsonboxnet");
	}
}