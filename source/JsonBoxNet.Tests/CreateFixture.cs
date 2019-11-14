using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JsonBoxNet.Tests
{
	[TestFixture]
	public class CreateFixture
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
			var record = await jsonBox.CreateAsync(item);

			Assert.AreEqual(item.Name, record.Value.Name);
			Assert.AreEqual(item.Age, record.Value.Age);
			Assert.AreNotEqual(default(DateTime), record.CreatedOn);
			Assert.Null(record.UpdatedOn);
		}

		[TearDown]
		public async Task Teardown()
		{
			await httpClient.DeleteAsync("https://jsonbox.io/testbox___jsonboxnet?q=Name:foo,Name:bar");
		}
	}
}