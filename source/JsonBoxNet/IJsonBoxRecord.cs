using System;

namespace JsonBoxNet
{
	public interface IJsonBoxRecord<T>
	{
		string Id { get; }
		DateTime CreatedOn { get; }
		DateTime? UpdatedOn { get; }
		T Value { get; }
	}
}
