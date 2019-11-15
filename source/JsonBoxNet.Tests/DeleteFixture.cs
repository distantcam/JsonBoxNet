using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace JsonBoxNet.Tests
{
	[TestFixture]
	public class DeleteFixture
	{
		static readonly HttpClient httpClient = new HttpClient();

		IJsonBox jsonBox;

		[SetUp]
		public void Setup()
		{
			var serializer = new JsonSerializer();
			jsonBox = new NewtonsoftJsonBox(httpClient, "testbox___jsonboxnet", serializer);
		}

		async Task<string> CreateRecord()
		{
			var response = await httpClient.PostAsync("https://jsonbox.io/testbox___jsonboxnet",
				new StringContent("{ \"Name\": \"Foo\", \"Age\": 13 }", System.Text.Encoding.UTF8, "application/json"));
			var msg = await response.Content.ReadAsStringAsync();
			var json = JObject.Parse(msg);
			return json["_id"].Value<string>();
		}

		[Test]
		public async Task DeleteRecord()
		{
			var id = await CreateRecord();

			var result = await jsonBox.DeleteAsync(id);

			Assert.IsEmpty(await jsonBox.GetAllAsync<TestObject>());
			Assert.AreEqual("Record removed.", result.Message);
		}

		[Test]
		public async Task DeleteMultipleRecord()
		{
			var id1 = await CreateRecord();
			var id2 = await CreateRecord();

			var result = await jsonBox.DeleteQueryAsync("Name:Foo");

			Assert.IsEmpty(await jsonBox.GetAllAsync<TestObject>());
			Assert.AreEqual("2 Records removed.", result.Message);
		}

		[TearDown]
		public async Task Teardown()
		{
			await httpClient.DeleteAsync("https://jsonbox.io/testbox___jsonboxnet?q=Name:Foo");
		}
	}
}