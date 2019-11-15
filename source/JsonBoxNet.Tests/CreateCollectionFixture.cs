using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonBoxNet.Tests
{
	[TestFixture]
	public class CreateCollectionFixture
	{
		static readonly HttpClient httpClient = new HttpClient();

		IJsonBox jsonBox;

		[SetUp]
		public void Setup()
		{
			var serializer = new JsonSerializer();
			jsonBox = new NewtonsoftJsonBox(httpClient, "testbox___jsonboxnet", serializer);
		}

		[Test]
		public async Task CreateRecord()
		{
			var item = new TestObject { Name = "Foo", Age = 13 };
			var record = await jsonBox.CreateAsync("collection", item);

			Assert.AreEqual(item.Name, record.Value.Name);
			Assert.AreEqual(item.Age, record.Value.Age);
			Assert.AreNotEqual(default(DateTime), record.CreatedOn);
			Assert.Null(record.UpdatedOn);
		}

		[Test]
		public async Task CreateManyRecord1()
		{
			var item1 = new TestObject { Name = "Foo", Age = 13 };
			var item2 = new TestObject { Name = "Bar", Age = 15 };
			var records = (await jsonBox.CreateManyAsync("collection", item1, item2)).ToArray();

			Assert.AreEqual(item1.Name, records[0].Value.Name);
			Assert.AreEqual(item1.Age, records[0].Value.Age);
			Assert.AreNotEqual(default(DateTime), records[0].CreatedOn);
			Assert.Null(records[0].UpdatedOn);

			Assert.AreEqual(item2.Name, records[1].Value.Name);
			Assert.AreEqual(item2.Age, records[1].Value.Age);
			Assert.AreNotEqual(default(DateTime), records[1].CreatedOn);
			Assert.Null(records[1].UpdatedOn);
		}

		[Test]
		public async Task CreateManyRecord2()
		{
			var item1 = new TestObject { Name = "Foo", Age = 13 };
			var item2 = new TestObject { Name = "Bar", Age = 15 };
			var records = (await jsonBox.CreateManyAsync<TestObject>("collection", new List<TestObject> { item1, item2 })).ToArray();

			Assert.AreEqual(item1.Name, records[0].Value.Name);
			Assert.AreEqual(item1.Age, records[0].Value.Age);
			Assert.AreNotEqual(default(DateTime), records[0].CreatedOn);
			Assert.Null(records[0].UpdatedOn);

			Assert.AreEqual(item2.Name, records[1].Value.Name);
			Assert.AreEqual(item2.Age, records[1].Value.Age);
			Assert.AreNotEqual(default(DateTime), records[1].CreatedOn);
			Assert.Null(records[1].UpdatedOn);
		}

		[Test]
		public void CollectionNameTooLong()
		{
			var item = new TestObject { Name = "Foo", Age = 13 };

			Assert.ThrowsAsync(typeof(ArgumentException), async () => await jsonBox.CreateAsync("collection_collection", item));
			Assert.ThrowsAsync(typeof(ArgumentException), async () => await jsonBox.CreateManyAsync("collection_collection", item));
			Assert.ThrowsAsync(typeof(ArgumentException), async () => await jsonBox.CreateManyAsync("collection_collection", new List<TestObject> { item }));
		}

		[TearDown]
		public async Task Teardown()
		{
			await httpClient.DeleteAsync("https://jsonbox.io/testbox___jsonboxnet?q=Name:Foo");
			await httpClient.DeleteAsync("https://jsonbox.io/testbox___jsonboxnet?q=Name:Bar");
		}
	}
}