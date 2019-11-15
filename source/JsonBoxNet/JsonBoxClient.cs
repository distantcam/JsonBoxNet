using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonBoxNet
{
	public class JsonBoxClient : IJsonBoxClient
	{
		readonly HttpClient httpClient;
		readonly string baseUrl;

		public JsonBoxClient(HttpClient httpClient, string boxId)
		{
			if (!IsValid(boxId))
				throw new ArgumentException("The box id should contain only alphanumeric characters and _", nameof(boxId));
			if (boxId.Length < 20 || boxId.Length > 64)
				throw new ArgumentException("The box id must be at least 20 characters, and no more than 64", nameof(boxId));

			this.httpClient = httpClient;
			baseUrl = "https://jsonbox.io/" + boxId;
		}

		public Task<string> CreateAsync(string json) => HandleRequest(HttpMethod.Post, baseUrl, json);
		public Task<string> CreateAsync(string collection, string json) => HandleRequest(HttpMethod.Post, CreateCollectionUrl(collection), json);
		public Task<string> UpdateAsync(string id, string json) => HandleRequest(HttpMethod.Put, CreateIdUrl(id), json);
		public Task<string> DeleteAsync(string id) => HandleRequest(HttpMethod.Delete, CreateIdUrl(id));
		public Task<string> DeleteQueryAsync(params string[] queries) => HandleRequest(HttpMethod.Delete, AppendFilter(baseUrl, null, null, null, queries));

		public Task<string> GetAsync(string sort = null, int? skip = null, int? limit = null, params string[] queries) => HandleRequest(HttpMethod.Get, AppendFilter(baseUrl, sort, skip, limit, queries));
		public Task<string> GetAllAsync() => HandleRequest(HttpMethod.Get, baseUrl);
		public Task<string> GetCollectionAsync(string collection) => HandleRequest(HttpMethod.Get, CreateCollectionUrl(collection));
		public Task<string> GetCollectionAsync(string collection, string sort = null, int? skip = null, int? limit = null, params string[] queries) => HandleRequest(HttpMethod.Get, AppendFilter(CreateCollectionUrl(collection), sort, skip, limit, queries));
		public Task<string> GetByIdAsync(string id) => HandleRequest(HttpMethod.Get, CreateIdUrl(id));

		async Task<string> HandleRequest(HttpMethod method, string url, string json = null)
		{
			var request = new HttpRequestMessage(method, url);
			if (!string.IsNullOrEmpty(json))
			{
				request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
			}
			var response = await httpClient.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}

		string AppendFilter(string url, string sort, int? skip, int? limit, params string[] queries)
		{
			var parameters = new List<string>();

			if (!string.IsNullOrEmpty(sort)) parameters.Add("sort=" + sort);
			if (skip.HasValue) parameters.Add("skip=" + skip.Value);
			if (limit.HasValue) parameters.Add("limit=" + limit.Value);

			parameters.AddRange(queries.Select(q => "q=" + q));

			if (parameters.Any())
				return url + "?" + string.Join("&", parameters);

			return url;
		}

		string CreateCollectionUrl(string collection)
		{
			if (collection.Length > 20)
				throw new ArgumentException("The collection must be less than 20 characters", nameof(collection));

			var url = baseUrl;
			if (!string.IsNullOrEmpty(collection))
			{
				if (!IsValid(collection))
					throw new ArgumentException("The collection should contain only alphanumeric characters and _", nameof(collection));

				url += "/" + collection;
			}
			return url;
		}

		string CreateIdUrl(string id)
		{
			var url = baseUrl;
			if (!string.IsNullOrEmpty(id))
			{
				url += "/" + id;
			}
			return url;
		}

		bool IsValid(string value) => !string.IsNullOrEmpty(value) && value.All(c => char.IsLetterOrDigit(c) || c == '&' || c == '_');
	}
}
