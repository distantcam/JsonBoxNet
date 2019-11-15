using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonBoxNet
{
	public abstract class JsonBox : IJsonBox
	{
		protected readonly IJsonBoxClient client;

		protected JsonBox(HttpClient httpClient, string boxId)
		{
			client = new JsonBoxClient(httpClient, boxId);
		}

		public async Task<IJsonBoxRecord<T>> CreateAsync<T>(T value) => CreateRecord<T>(await client.CreateAsync(SerializeToString(value)));
		public async Task<IJsonBoxRecord<T>> CreateAsync<T>(string collection, T value) => CreateRecord<T>(await client.CreateAsync(collection, SerializeToString(value)));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> CreateMultipleAsync<T>(IEnumerable<T> values) => CreateRecords<T>(await client.CreateAsync(SerializeToString(values)));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> CreateMultipleAsync<T>(string collection, IEnumerable<T> values) => CreateRecords<T>(await client.CreateAsync(collection, SerializeToString(values)));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> CreateMultipleAsync<T>(params T[] values) => CreateRecords<T>(await client.CreateAsync(SerializeToString(values)));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> CreateMultipleAsync<T>(string collection, params T[] values) => CreateRecords<T>(await client.CreateAsync(collection, SerializeToString(values)));

		public async Task<IJsonBoxMessage> UpdateAsync<T>(string id, T value) => CreateMessage(await client.UpdateAsync(SerializeToString(value), id));
		public async Task<IJsonBoxMessage> DeleteAsync(string id) => CreateMessage(await client.DeleteAsync(id));
		public async Task<IJsonBoxMessage> DeleteQueryAsync(params string[] queries) => CreateMessage(await client.DeleteQueryAsync(queries));

		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetAsync<T>(string sort = null, int? skip = null, int? limit = null, params string[] queries) => CreateRecords<T>(await client.GetAsync(sort, skip, limit, queries));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetAllAsync<T>() => CreateRecords<T>(await client.GetAllAsync());
		public async Task<IJsonBoxRecord<T>> GetByIdAsync<T>(string id) => CreateRecord<T>(await client.GetByIdAsync(id));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection) => CreateRecords<T>(await client.GetCollectionAsync(collection));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries) => CreateRecords<T>(await client.GetCollectionAsync(collection, sort, skip, limit, queries));

		protected abstract string SerializeToString(object value);
		protected abstract IJsonBoxRecord<T> CreateRecord<T>(string json);
		protected abstract IEnumerable<IJsonBoxRecord<T>> CreateRecords<T>(string json);
		protected abstract IJsonBoxMessage CreateMessage(string json);
	}
}
