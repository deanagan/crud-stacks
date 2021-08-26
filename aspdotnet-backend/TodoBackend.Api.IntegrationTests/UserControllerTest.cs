
using System.Net;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;


using System;
using System.Net.Http;
using System.Threading.Tasks;


using FakeItEasy;

using TodoBackend.Api.Interfaces;
using TodoBackend.Api.Data.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace TodoBackend.Api.IntegrationTests
{
    public class UsersControllerIntegrationTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private TestWebApplicationFactory<Startup> _factory;

        private IUnitOfWork _fakeUnitOfWork;
        private User _dummyUser2;
        private User _dummyUser1;
        private Role _dummyRole;

        public UsersControllerIntegrationTest(TestWebApplicationFactory<Startup> factory)
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>(uow => uow.Implements<IUnitOfWork>());
            _factory = factory;
            _factory.ClientOptions.BaseAddress = new Uri("http://localhost/v1/api/users");
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001");
                builder.UseStartup<TestStartup>();
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(typeof(IUnitOfWork), _fakeUnitOfWork);
                });
            }).CreateClient();

            _dummyRole = new Role
            {
                Id = 1,
                Name = "RoleKind",
                Description = "A Role of some kind"
            };

            _dummyUser1 = new User
            {
                Id = 1,
                Name = "Hello",
                Email = "Test@Hello.com",
                Hash = "234dasd3rfs",
                RoleId = 1,
                Role = _dummyRole
            };

            _dummyUser2 = new User
            {
                Id = 2,
                Name = "Hello",
                Email = "Test@Hello.com",
                Hash = "234dasd3rfs",
                RoleId = 1,
                Role = _dummyRole
            };

            A.CallTo(() => _fakeUnitOfWork.Users.GetAsync(1)).Returns(_dummyUser1);
            A.CallTo(() => _fakeUnitOfWork.Roles.GetAsync(1)).Returns(_dummyRole);
            A.CallTo(() => _fakeUnitOfWork.Users.GetAllAsync()).Returns(new List<User>{_dummyUser1, _dummyUser2});
        }

        [Fact]
        public async Task ShouldReturnOk_WhenGettingAllUsers()
        {
            var response = await _client.GetAsync("users");

            response.EnsureSuccessStatusCode();
        }

        // [Fact]
        // public async Task ShouldReturnExpectedMediaType_WhenGettingAllUsers()
        // {
        //     var response = await _client.GetAsync("");

        //     response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
        // }

        // [Fact]
        // public async Task ShouldReturnExpectedHeader_WhenGettingAllUsers()
        // {
        //     var response = await _client.GetAsync("users");

        //     using (new AssertionScope())
        //     {
        //         response.Content.Should().NotBeNull();
        //         response.Content.Headers.ContentLength.Should().BeGreaterThan(0);
        //     }
        // }

        // [Fact]
        // public async Task ShouldReturnExpectedContent_WhenGettingAllUsers()
        // {
        //     var response = await _client.GetStringAsync("users");

        //     response.Should().NotBeNull();
        // }

        [Fact]
        public async void ReturnOk_WhenDoingGetUser()
        {
            // Act
            var result = await _client.GetAsync("users/1");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void ReturnCorrectContent_WhenDoingGetUser()
        {
            var result = await _client.GetFromJsonAsync<UserView>("users/1");

            using (new AssertionScope())
            {
                result.Should().BeOfType<UserView>();
                result.Id.Should().Be(_dummyUser1.Id);
                result.Name.Should().Be(_dummyUser1.Name);
                result.Email.Should().Be(_dummyUser1.Email);
                result.Role.Should().BeEquivalentTo(_dummyUser1.Role);
            }
        }

        [Fact]
        public async void ReturnContentCreated_WhenCreatingUser()
        {
            // Arrange
            var user = new User
            {
                Name = "Steve Rogers",
                RoleId = 2,
                Email = "steve.rogers@todo.io",
            };

            // Act
            var result = await _client.PostAsync("users"
                , new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8
                , "application/json"));

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

    }
}
