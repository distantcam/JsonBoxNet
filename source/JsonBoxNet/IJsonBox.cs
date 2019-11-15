using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonBoxNet
{
	public interface IJsonBox
	{
		Task<IJsonBoxRecord<T>> CreateAsync<T>(T value);
		Task<IJsonBoxRecord<T>> CreateAsync<T>(string collection, T value);
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateManyAsync<T>(params T[] values);
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateManyAsync<T>(string collection, params T[] values);
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateManyAsync<T>(IEnumerable<T> values);
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateManyAsync<T>(string collection, IEnumerable<T> values);

		Task<IJsonBoxMessage> UpdateAsync<T>(string id, T value);
		Task<IJsonBoxMessage> DeleteAsync(string id);
		Task<IJsonBoxMessage> DeleteQueryAsync(params string[] queries);

		Task<IEnumerable<IJsonBoxRecord<T>>> GetAllAsync<T>();
		Task<IEnumerable<IJsonBoxRecord<T>>> GetAsync<T>(string sort = null, int? skip = null, int? limit = null, params string[] queries);
		Task<IJsonBoxRecord<T>> GetByIdAsync<T>(string id);
		Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection);
		Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries);
	}
}
