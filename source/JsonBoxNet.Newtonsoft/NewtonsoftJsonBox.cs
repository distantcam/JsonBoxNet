using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonBoxNet
{
	public class NewtonsoftJsonBox : JsonBox
	{
		[DebuggerDisplay("{DebuggerDisplay,nq}")]
		class JsonBoxRecord<T> : IJsonBoxRecord<T>
		{
			[JsonProperty("_id")] public string Id { get; set; }
			[JsonProperty("_createdOn")] public DateTime CreatedOn { get; set; }
			[JsonProperty("_updatedOn")] public DateTime? UpdatedOn { get; set; }
			[JsonIgnore] public T Value { get; set; }

			[JsonIgnore]
			private string DebuggerDisplay => $"[{Id}] - Created {CreatedOn}";
		}

		class JsonBoxMessage : IJsonBoxMessage
		{
			[JsonProperty("message")] public string Message { get; set; }

			public override string ToString() => Message;
		}

		readonly JsonSerializer serializer;

		public NewtonsoftJsonBox(HttpClient httpClient, string boxId, JsonSerializer serializer) : base(httpClient, boxId)
		{
			this.serializer = serializer;
		}

		protected override string SerializeToString(object value)
		{
			using var writer = new StringWriter();
			serializer.Serialize(writer, value);
			return writer.ToString();
		}

		protected override IJsonBoxRecord<T> CreateRecord<T>(string json)
		{
			var value = (T)serializer.Deserialize(new StringReader(json), typeof(T));
			var result = new JsonBoxRecord<T> { Value = value };
			serializer.Populate(new StringReader(json), result);
			return result;
		}

		protected override IEnumerable<IJsonBoxRecord<T>> CreateRecords<T>(string json)
		{
			var value = (IEnumerable<JObject>)serializer.Deserialize(new StringReader(json), typeof(IEnumerable<JObject>));
			var records = value.Select(j =>
			{
				var record = j.ToObject<JsonBoxRecord<T>>();
				record.Value = j.ToObject<T>();
				return record;
			}).ToArray();
			return records;
		}

		protected override IJsonBoxMessage CreateMessage(string json) => (IJsonBoxMessage)serializer.Deserialize(new StringReader(json), typeof(JsonBoxMessage));
	}
}
