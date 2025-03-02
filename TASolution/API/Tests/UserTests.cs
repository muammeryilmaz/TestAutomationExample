using NUnit.Framework;
using API.Client;
using API.Utilities;
using RestSharp;
using System.Collections.Generic;

namespace API.Tests
{
    /// <summary>
    /// Test class for verifying API functionality.
    /// </summary>
    public class UserTests
    {
        private IApiClient _apiClient;
        private const string BaseUrl = "https://reqres.in/";

        [SetUp]
        public void Setup()
        {
            LoggerHelper.LogInfo("Initializing API client.");
            _apiClient = new ApiClient(BaseUrl);
        }

        [Test]
        public void VerifyFetchingUserList()
        {
            string endpoint = "api/users?page=2";
            LoggerHelper.LogInfo($"Executing GET request to {endpoint}");

            RestResponse response = _apiClient.SendRequest(endpoint, Method.Get);

            LoggerHelper.LogInfo($"Validating response status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            LoggerHelper.LogInfo("Validating that at least one user exists.");
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);
            Assert.That((int)data["page"], Is.EqualTo(2));
            Assert.That(((IEnumerable<dynamic>)data["data"]).GetEnumerator().MoveNext(), Is.True);
        }

        [Test]
        public void VerifyCreatingNewUser()
        {
            string endpoint = "api/users";
            var requestBody = new { name = "John Doe", job = "Software Engineer" };

            LoggerHelper.LogInfo($"Executing POST request to {endpoint} with payload: {requestBody}");

            RestResponse response = _apiClient.SendRequest(endpoint, Method.Post, requestBody);

            LoggerHelper.LogInfo($"Validating response status code: {response.StatusCode}");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);
            LoggerHelper.LogInfo("Validating that response contains expected fields.");
            Assert.That((string)data["name"], Is.EqualTo("John Doe"));
            Assert.That((string)data["job"], Is.EqualTo("Software Engineer"));
            Assert.That((string)data["id"], Is.Not.Null.And.Not.Empty);
        }

        [TearDown]
        public void Cleanup()
        {
            LoggerHelper.LogInfo("Test execution completed.");
        }
    }
}
