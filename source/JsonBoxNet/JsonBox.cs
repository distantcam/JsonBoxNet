﻿using System.Collections.Generic;
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
		public async Task<IEnumerable<IJsonBoxRecord<T>>> CreateAsync<T>(IEnumerable<T> values) => CreateRecords<T>(await client.CreateAsync(SerializeToString(values)));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetAsync<T>(string sort = null, int? skip = null, int? limit = null, params string[] queries) => CreateRecords<T>(await client.GetAsync(sort, skip, limit, queries));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetAllAsync<T>() => CreateRecords<T>(await client.GetAllAsync());
		public async Task<IJsonBoxRecord<T>> GetByIdAsync<T>(string id) => CreateRecord<T>(await client.GetByIdAsync(id));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection) => CreateRecords<T>(await client.GetCollectionAsync(collection));
		public async Task<IEnumerable<IJsonBoxRecord<T>>> GetCollectionAsync<T>(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries) => CreateRecords<T>(await client.GetCollectionAsync(collection, sort, skip, limit, queries));
		public Task UpdateAsync<T>(T value, string id) => client.UpdateAsync(SerializeToString(value), id);
		public Task DeleteAsync(string id) => client.DeleteAsync(id);

		protected abstract string SerializeToString(object value);
		protected abstract IJsonBoxRecord<T> CreateRecord<T>(string json);
		protected abstract IEnumerable<IJsonBoxRecord<T>> CreateRecords<T>(string json);
	}
}
