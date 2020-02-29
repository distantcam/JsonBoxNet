using System.Net.Http;
using System.Threading.Tasks;
using JsonBoxNet;

class TextJsonUsage
{
	static HttpClient httpClient;

	#region SystemTextShared
	static TextJsonUsage()
	{
		httpClient = new HttpClient();
	}
	#endregion

	async Task Create(string theBoxId)
	{
		#region SystemTextCreate
		var box = new SystemTextJsonBox(
			httpClient,
			theBoxId);
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
		#region SystemTextDelete
		var box = new SystemTextJsonBox(
			httpClient,
			theBoxId);
		await box.DeleteAsync(itemId);
		#endregion
	}
}
