using RestSharp;

namespace API.Client
{
    /// <summary>
    /// Interface for handling API requests.
    /// This allows different API client implementations to be used interchangeably.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Sends an API request with the given endpoint, method, and optional request body.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="method">The HTTP method (GET, POST, etc.).</param>
        /// <param name="body">The request payload (optional).</param>
        /// <returns>The API response.</returns>
        RestResponse SendRequest(string endpoint, Method method, object body = null);
    }
}
