using System;
using System.Collections.Generic;
using System.Linq;
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
	public abstract class CreateCollectionFixtureBase
	{
		protected abstract JsonBox CreateJsonBox(HttpClient client);

		JsonBox CreateJsonBox(string collection, string json)
		{
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, $"/testbox___jsonboxnet/{collection}")
				.Respond("application/json", json);
			return CreateJsonBox(mockHttp.ToHttpClient());
		}

		[Test]
		public async Task CreateRecord()
		{
			// Arrange
			var fixture = new Fixture();
			var item = fixture.Create<TestObject>();

			var jsonBox = CreateJsonBox("collection", JsonConvert.SerializeObject(item));

			// Act
			var record = await jsonBox.CreateAsync("collection", item);

			// Assert
			record.Value.Should().BeEquivalentTo(item);
		}

		[Test]
		public async Task CreateManyRecords_UsingArray()
		{
			// Arrange
			var fixture = new Fixture();
			var item1 = fixture.Create<TestObject>();
			var item2 = fixture.Create<TestObject>();

			var jsonBox = CreateJsonBox("collection", JsonConvert.SerializeObject(new TestObject[] { item1, item2 }));

			// Act
			var records = (await jsonBox.CreateManyAsync("collection", item1, item2)).ToArray();

			// Assert
			records[0].Value.Should().BeEquivalentTo(item1);
			records[1].Value.Should().BeEquivalentTo(item2);
		}

		[Test]
		public async Task CreateManyRecords_UsingList()
		{
			// Arrange
			var fixture = new Fixture();
			var item1 = fixture.Create<TestObject>();
			var item2 = fixture.Create<TestObject>();

			var jsonBox = CreateJsonBox("collection", JsonConvert.SerializeObject(new TestObject[] { item1, item2 }));

			// Act
			var records = (await jsonBox.CreateManyAsync<TestObject>("collection", new List<TestObject> { item1, item2 })).ToArray();

			// Assert
			records[0].Value.Should().BeEquivalentTo(item1);
			records[1].Value.Should().BeEquivalentTo(item2);
		}

		[Test]
		public async Task CollectionNameTooLong()
		{
			var mockHttp = new MockHttpMessageHandler();
			var jsonBox = new NewtonsoftJsonBox(mockHttp.ToHttpClient(), "testbox___jsonboxnet", new JsonSerializer());

			var fixture = new Fixture();
			var item = fixture.Create<TestObject>();

			await jsonBox.Invoking(async jb => await jsonBox.CreateAsync("collection_collection", item))
				.Should().ThrowAsync<ArgumentException>();
			await jsonBox.Invoking(async jb => await jsonBox.CreateManyAsync("collection_collection", item))
				.Should().ThrowAsync<ArgumentException>();
			await jsonBox.Invoking(async jb => await jsonBox.CreateManyAsync("collection_collection", new List<TestObject> { item }))
				.Should().ThrowAsync<ArgumentException>();
		}
	}

	public class NewtonsoftJsonBox_CreateCollectionFixture : CreateCollectionFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new NewtonsoftJsonBox(client, "testbox___jsonboxnet", new JsonSerializer());
	}

	public class SystemTextJsonBox_CreateCollectionFixture : CreateCollectionFixtureBase
	{
		protected override JsonBox CreateJsonBox(HttpClient client) =>
			new SystemTextJsonBox(client, "testbox___jsonboxnet");
	}
}