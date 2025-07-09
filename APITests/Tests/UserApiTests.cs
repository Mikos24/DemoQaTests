using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using SeleniumTests.APITests.Models;

namespace SeleniumTests.APITests.Tests
{
    [TestFixture]
    public class UserApiTests
    {
        private const string BaseUrl = "https://demoqa.com";
        private const string Username = "qatest2025";
        private const string Password = "Test123!";

        private string token;

        [Test, Order(1)]
        public void GenerateToken_ValidCredentials_ShouldReturnToken()
        {
            // Arrange - Create client and request with user credentials
            RestClient client = new RestClient(BaseUrl);
            RestRequest request = new RestRequest("/Account/v1/GenerateToken", Method.Post);
            request.AddJsonBody(new AuthRequest { userName = Username, password = Password });

            // Act - Execute the token generation request
            RestResponse response = client.Execute(request);
            
            // Assert - Verify response status and token
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), 
                $"Expected OK but got {response.StatusCode}. Response: {response.Content}");
            
            Assert.IsNotNull(response.Content, "Response content should not be null");
            
            AuthResponse? data = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
            Assert.IsNotNull(data, "Deserialized data should not be null");
            Assert.IsNotNull(data.token, $"Token should not be null. Full response: {response.Content}");
            Assert.That(data.status, Is.EqualTo("Success"));

            // Store token for potential future use
            token = data.token;
        }

        [Test, Order(2)]
        public void GetBooksList_ShouldReturnAvailableBooks()
        {
            // Arrange - Create client and request for books endpoint
            RestClient client = new RestClient(BaseUrl);
            RestRequest request = new RestRequest("/BookStore/v1/Books", Method.Get);

            // Act - Execute the get books request
            RestResponse response = client.Execute(request);
            
            // Assert - Verify response status and structure
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), 
                $"Expected OK but got {response.StatusCode}. Response: {response.Content}");
            
            Assert.IsNotNull(response.Content, "Response content should not be null");
            
            // Parse and validate response data
            BooksResponse? data = JsonConvert.DeserializeObject<BooksResponse>(response.Content);
            Assert.IsNotNull(data, "Deserialized data should not be null");
            Assert.IsNotNull(data.books, "Books array should not be null");
            
            // Verify that books are available
            Assert.That(data.books.Count, Is.GreaterThan(0), "There should be at least one book available");
        }
    }
}
