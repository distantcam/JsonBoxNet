using System;

namespace JsonBoxNet
{
	/// <summary>
	/// A wrapper for the record.
	/// </summary>
	/// <typeparam name="T">The type  of the custom data stored in the record.</typeparam>
	public interface IJsonBoxRecord<T>
	{
		/// <summary>
		/// The ID of the record.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// The date and time the record was created.
		/// </summary>
		DateTime CreatedOn { get; }

		/// <summary>
		/// The date and time the record was updated, or null if the record hasn't been updated.
		/// </summary>
		DateTime? UpdatedOn { get; }

		/// <summary>
		/// The custom data in the record.
		/// </summary>
		T Value { get; }
	}
}
