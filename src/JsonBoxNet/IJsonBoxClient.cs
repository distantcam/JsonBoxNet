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
		/// <param name="collection">The collection name to store the object in.</param>
		/// <param name="json">The JSON object to store.</param>
		/// <returns>The JSON record.</returns>
		Task<string> CreateAsync(string collection, string json);

		/// <summary>
		/// Update an existing record.
		/// </summary>
		/// <param name="id">The ID of the record to update.</param>
		/// <param name="json">The updated JSON object.</param>
		/// <returns>A status message as a JSON object.</returns>
		Task<string> UpdateAsync(string id, string json);

		/// <summary>
		/// Delete a record.
		/// </summary>
		/// <param name="id">The ID of the record to delete.</param>
		/// <returns>A status message as a JSON object.</returns>
		Task<string> DeleteAsync(string id);

		/// <summary>
		/// Deletes all records matching the queries.
		/// </summary>
		/// <param name="queries">The query items for filtering. See <see href="https://github.com/vasanthv/jsonbox#filtering">Filtering</see> for details.</param>
		/// <returns>A status message as a JSON object.</returns>
		Task<string> DeleteQueryAsync(params string[] queries);

		/// <summary>
		/// Returns all records matching the queries.
		/// </summary>
		/// <param name="sort">Sort the records by this field. Prefix with '-' to reverse the order. See <see href="https://github.com/vasanthv/jsonbox#read">Read</see> for details.</param>
		/// <param name="skip">Skip the number of records.</param>
		/// <param name="limit">Limit the number of returned records.</param>
		/// <param name="queries">The query items for filtering. See <see href="https://github.com/vasanthv/jsonbox#filtering">Filtering</see> for details.</param>
		/// <returns>The filtered list of records.</returns>
		Task<string> GetAsync(string sort = null, int? skip = null, int? limit = null, params string[] queries);

		/// <summary>
		/// Returns all records.
		/// </summary>
		/// <returns>The list of records.</returns>
		Task<string> GetAllAsync();

		/// <summary>
		/// Returns a single record matching the ID.
		/// </summary>
		/// <param name="id">The ID of the record to return.</param>
		/// <returns>The record as a JSON object, or an error message as a JSON object.</returns>
		Task<string> GetByIdAsync(string id);

		/// <summary>
		/// Returns a collection of records.
		/// </summary>
		/// <param name="collection">The name of the collection.</param>
		/// <returns>The list of records in the collection.</returns>
		Task<string> GetCollectionAsync(string collection);

		/// <summary>
		/// Returns a filtered collection of records.
		/// </summary>
		/// <param name="collection">The name of the collection.</param>
		/// <param name="sort">Sort the records by this field. Prefix with '-' to reverse the order. See <see href="https://github.com/vasanthv/jsonbox#read">Read</see> for details.</param>
		/// <param name="skip">Skip the number of records.</param>
		/// <param name="limit">Limit the number of returned records.</param>
		/// <param name="queries">The query items for filtering. See <see href="https://github.com/vasanthv/jsonbox#filtering">Filtering</see> for details.</param>
		/// <returns>The filtered list of records from the collection.</returns>
		Task<string> GetCollectionAsync(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries);
	}
}
