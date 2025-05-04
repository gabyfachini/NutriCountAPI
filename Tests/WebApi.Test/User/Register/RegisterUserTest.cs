using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory> //cada classe é executada em um servidor
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(WebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public object HttpStatusCode { get; private set; }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var response = await _httpClient.PostAsJsonAsync("User", request);

            response.StatusCode().Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }
    }
}
