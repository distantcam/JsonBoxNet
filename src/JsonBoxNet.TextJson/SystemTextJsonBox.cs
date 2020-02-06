using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonBoxNet
{
	public class SystemTextJsonBox : JsonBox
	{
		[DebuggerDisplay("{DebuggerDisplay,nq}")]
		class JsonBoxRecord<T> : IJsonBoxRecord<T>
		{
			[JsonPropertyName("_id")] public string Id { get; set; }
			[JsonPropertyName("_createdOn")] public DateTime CreatedOn { get; set; }
			[JsonPropertyName("_updatedOn")] public DateTime? UpdatedOn { get; set; }
			[JsonIgnore] public T Value { get; set; }

			[JsonIgnore]
			private string DebuggerDisplay => $"[{Id}] - Created {CreatedOn}";
		}

		class JsonBoxMessage : IJsonBoxMessage
		{
			[JsonPropertyName("message")] public string Message { get; set; }

			public override string ToString() => Message;
		}

		public SystemTextJsonBox(HttpClient httpClient, string boxId) : base(httpClient, boxId)
		{
		}

		protected override IJsonBoxMessage CreateMessage(string json) => JsonSerializer.Deserialize<JsonBoxMessage>(json);

		protected override IJsonBoxRecord<T> CreateRecord<T>(string json)
		{
			var value = JsonSerializer.Deserialize<T>(json);
			var record = JsonSerializer.Deserialize<JsonBoxRecord<T>>(json);
			record.Value = value;
			return record;
		}

		protected override IEnumerable<IJsonBoxRecord<T>> CreateRecords<T>(string json)
		{
			using (var document = JsonDocument.Parse(json))
			{
				foreach (var item in document.RootElement.EnumerateArray())
				{
					var record = new JsonBoxRecord<T>();
					record.Value = JsonSerializer.Deserialize<T>(item.GetRawText());
					if (item.TryGetProperty("_id", out var id)) record.Id = id.GetString();
					if (item.TryGetProperty("_createdOn", out var createdOn)) record.CreatedOn = createdOn.GetDateTime();
					if (item.TryGetProperty("_updatedOn", out var updatedOn)) record.UpdatedOn = updatedOn.GetDateTime();
					yield return record;
				}
			}
		}

		protected override string SerializeToString(object value) => JsonSerializer.Serialize(value);
	}
}