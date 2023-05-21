using Demo.Api.Controllers;
using Demo.Api.InputModels;
using Demo.Api.Services;
using FakeItEasy;

namespace Demo.Testting
{
    public class AuthenticationControllerTest
    {
        [Fact]
        public async Task Returns_Ok_With_The_Token()
        {
            var dataStore = A.Fake<IUserService>();

            var fakeUser = A.Fake<RegistrationInputModel>();


            A.CallTo(() => dataStore.RegisterAsync(fakeUser));
            //var authController = new AuthenticationController();
        }
    }
}