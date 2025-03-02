using RestSharp;
using Newtonsoft.Json;
using API.Utilities;
using System;

namespace API.Client
{
    /// <summary>
    /// Implementation of IApiClient for handling API requests using RestSharp.
    /// </summary>
    public class ApiClient : IApiClient
    {
        private readonly RestClient _client;

        /// <summary>
        /// Initializes a new instance of ApiClient with the specified base URL.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API.</param>
        public ApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        /// <summary>
        /// Sends an API request with the given endpoint, method, and optional request body.
        /// </summary>
        /// <param name="endpoint">The API endpoint.</param>
        /// <param name="method">The HTTP method (GET, POST, etc.).</param>
        /// <param name="body">The request payload (optional).</param>
        /// <returns>The API response.</returns>
        public RestResponse SendRequest(string endpoint, Method method, object body = null)
        {
            var request = new RestRequest(endpoint, method);

            if (body != null)
            {
                string jsonBody = JsonConvert.SerializeObject(body);
                request.AddJsonBody(jsonBody);
            }

            LoggerHelper.LogInfo($"Sending Request: {method} {endpoint} - Body: {body}");

            var response = _client.Execute(request);

            LoggerHelper.LogInfo($"Received Response: {response.StatusCode} - Body: {response.Content}");

            return response;
        }
    }
}
