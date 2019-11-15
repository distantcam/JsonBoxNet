using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonBoxNet
{
	public interface IJsonBox
	{
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateAsync<T>(IEnumerable<T> values);
		Task<IEnumerable<IJsonBoxRecord<T>>> CreateAsync<T>(params T[] values);
		Task<IJsonBoxRecord<T>> CreateAsync<T>(T value);
		Task<IJsonBoxMessage> UpdateAsync<T>(T value, string id);
		Task<IJsonBoxMessage> DeleteAsync(string id);
		Task<IJsonBoxMessage> DeleteQueryAsync(params string[] queries);

		Task<IEnumerable<IJsonBoxRecord<T>>> GetAllAsync<T>();
		Task<IEnumerable<IJsonBoxRecord<T>>> GetAsync<T>(string sort = null, int? skip = null, int? limit = null, params string[] queries);
		Task<IJsonBoxRecord<T>> GetByIdAsync<T>(string id);
		Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection);
		Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries);
	}
}
