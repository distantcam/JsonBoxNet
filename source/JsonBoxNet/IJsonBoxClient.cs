using System.Threading.Tasks;

namespace JsonBoxNet
{
	public interface IJsonBoxClient
	{
		Task<string> CreateAsync(string json);
		Task<string> CreateAsync(string collection, string json);
		Task<string> UpdateAsync(string id, string json);
		Task<string> DeleteAsync(string id);
		Task<string> DeleteQueryAsync(params string[] queries);

		Task<string> GetAsync(string sort = null, int? skip = null, int? limit = null, params string[] queries);
		Task<string> GetAllAsync();
		Task<string> GetByIdAsync(string id);
		Task<string> GetCollectionAsync(string collection);
		Task<string> GetCollectionAsync(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries);
	}
}
