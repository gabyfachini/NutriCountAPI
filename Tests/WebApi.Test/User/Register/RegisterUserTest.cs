﻿using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>> //cada classe é executada em um servidor
    {
        private readonly HttpClient _httpClient;
        public RegisterUserTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            await _httpClient.PostAsJsonAsync("User", request);
        }
    }
}
