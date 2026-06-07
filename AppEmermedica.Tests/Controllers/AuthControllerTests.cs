using AppEmermedica.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Xunit;

namespace AppEmermedica.Tests.Controllers
{
    public class AuthControllerTests
    {
        private static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "EsteEsUnSecretoMuyLargo1234567890",
                    ["Jwt:Issuer"] = "AppEmermedica",
                    ["Jwt:Audience"] = "AppEmermedicaUsers",
                    ["Jwt:DurationMinutes"] = "60"
                })
                .Build();

        [Fact]
        public void Login_NullRequest_ReturnsBadRequest()
        {
            var controller = new AuthController(BuildConfiguration());

            var result = controller.Login(null!);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_InvalidRole_ReturnsBadRequest()
        {
            var controller = new AuthController(BuildConfiguration());

            var result = controller.Login(new LoginRequest
            {
                Nombre = "juan",
                Rol = "InvalidRole"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_ValidRequest_ReturnsJwtTokenWithCorrectClaims()
        {
            var controller = new AuthController(BuildConfiguration());

            var result = controller.Login(new LoginRequest
            {
                Nombre = "juan",
                Rol = "Admin"
            });

            var ok = Assert.IsType<OkObjectResult>(result);
            var tokenValue = ok.Value?.GetType().GetProperty("token")?.GetValue(ok.Value) as string;

            Assert.False(string.IsNullOrWhiteSpace(tokenValue));

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenValue!);

            Assert.Contains("AppEmermedicaUsers", jwt.Audiences);
            Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Name && c.Value == "juan");
            Assert.Equal(3, tokenValue!.Split('.').Length);
        }
    }
}
