namespace JsonBoxNet
{
	/// <summary>
	/// An object that represents a message from the API.
	/// </summary>
	public interface IJsonBoxMessage
    {
        /// <summary>
        /// The message.
        /// </summary>
        string Message { get; }
    }
}