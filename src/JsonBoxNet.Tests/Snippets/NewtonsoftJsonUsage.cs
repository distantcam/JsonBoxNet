using System.Net.Http;
using System.Threading.Tasks;
using JsonBoxNet;
using Newtonsoft.Json;

class NewtonsoftJsonUsage
{
	static HttpClient httpClient;
	static JsonSerializer serializer;

	#region NewtonsoftShared
	static NewtonsoftJsonUsage()
	{
		serializer = new JsonSerializer();
		httpClient = new HttpClient();
	}
	#endregion

	async Task Create(string theBoxId)
	{
		#region NewtonsoftCreate
		var box = new NewtonsoftJsonBox(
			httpClient,
			theBoxId,
			serializer);
		var person = new Person
		{
			Age = 10,
			Name = "Lesle"
		};
		await box.CreateAsync(person);
		#endregion
	}
	async Task Delete(string theBoxId, string itemId)
	{
		#region NewtonsoftDelete
		var box = new NewtonsoftJsonBox(
			httpClient,
			theBoxId,
			serializer);
		await box.DeleteAsync(itemId);
		#endregion
	}
}
