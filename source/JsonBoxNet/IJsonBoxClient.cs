using System.Threading.Tasks;

namespace JsonBoxNet
{
	/// <summary>
	/// The base interface for working with jsonbox.io with strings representing the json objects.
	/// </summary>
	public interface IJsonBoxClient
	{
		/// <summary>
		/// Create a new record.
		/// </summary>
		/// <param name="json">The JSON object to store.</param>
		/// <returns>The JSON record.</returns>
		Task<string> CreateAsync(string json);
		/// <summary>
		/// Create a new record.
		/// </summary>
		/// <param name="collection">The collection to store the object in.</param>
		/// <param name="json">The JSON object to store.</param>
		/// <returns>The JSON record.</returns>
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
