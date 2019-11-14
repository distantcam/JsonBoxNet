using System.Threading.Tasks;

namespace JsonBoxNet
{
	public interface IJsonBoxClient
	{
		Task<string> CreateAsync(string json);
		Task<string> UpdateAsync(string json, string id);
		Task<string> DeleteAsync(string id);

		Task<string> GetAsync(string sort = null, int? skip = null, int? limit = null, params string[] queries);
		Task<string> GetAllAsync();
		Task<string> GetByIdAsync(string id);
		Task<string> GetCollectionAsync(string collection);
		Task<string> GetCollectionAsync(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries);
	}
}
