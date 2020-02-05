using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using JsonBoxNet.TextJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace JsonBoxNet.Tests
{
	[TestFixture]
	public abstract class CreateFixtureBase
	{
		protected abstract JsonBox CreateJsonBox(HttpClient client);

		JsonBox CreateJsonBox(string json)
		{
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, "/testbox___jsonboxnet")
				.Respond("application/json", json);
			return CreateJsonBox(mockHttp.ToHttpClient());
		}

		[Test]
		public async Task CreateRecord()
		{
			// Arrange
			var fixture = new Fixture();
			var item = fixture.Create<TestObject>();

			var id = Guid.NewGuid();
			var wrapper = JObject.Parse($"{{'_id': '{id}'}}");
			var itemJson = JObject.FromObject(item);
			wrapper.Merge(itemJson);

			var jsonBox = CreateJsonBox(wrapper.ToString());

			// Act
			var record = await jsonBox.CreateAsync(item);

			// Assert
			record.Value.Should().BeEquivalentTo(item);
			record.Id.Should().Be(id.ToString());
			record.UpdatedOn.Should().BeNull();
		}

		[Test]
		public async Task CreateMultipleRecords_UsingArray()
		{
			// Arrange
			var fixture = new Fixture();
			var item1 = fixture.Create<TestObject>();
			var item2 = fixture.Create<TestObject>();

			var jsonBox = CreateJsonBox(JsonConvert.SerializeObject(new object[] { item1, item2 }));

			// Act
			var records = (await jsonBox.CreateManyAsync(item1, item2)).ToArray();

			// Assert
			records[0].Value.Should().BeEquivalentTo(item1);
			records[1].Value.Should().BeEquivalentTo(item2);
		}

		[Test]
		public async Task CreateMultipleRecords_UsingList()
		{
			// Arrange
			var fixture = new Fixture();
			var item1 = fixture.Create<TestObject>();
			var item2 = fixture.Create<TestObject>();

			var jsonBox = CreateJsonBox(JsonConvert.SerializeObject(new List<TestObject> { item1, item2 }));

			// Act
			var records = (await jsonBox.CreateManyAsync(item1, item2)).ToArray();

			// Assert
			records[0].Value.Should().BeEquivalentTo(item1);
			records[1].Value.Should().BeEquivalentTo(item2);
		}
	}

	public class NewtonsoftJsonBox_CreateFixture : CreateFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new NewtonsoftJsonBox(client, "testbox___jsonboxnet", new JsonSerializer());
	}

	public class SystemTextJsonBox_CreateFixture : CreateFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new SystemTextJsonBox(client, "testbox___jsonboxnet");
	}
}