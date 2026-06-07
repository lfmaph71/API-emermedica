using AppEmermedica.Application.Usuarios.Requests;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppEmermedica.Tests.Validation
{
    public class RequestValidationTests
    {
        private static bool TryValidate(object model, out IList<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        [Theory]
        [InlineData("juan")]
        [InlineData("María López")]
        public void CreateUsuarioRequest_ValidNombre_ShouldBeValid(string nombre)
        {
            var model = new CreateUsuarioRequest { Nombre = nombre, Rol = "User" };

            var valid = TryValidate(model, out var results);

            Assert.True(valid);
            Assert.Empty(results);
        }

        [Theory]
        [InlineData("<script>alert(1)</script>")]
        [InlineData("name<>")]
        [InlineData("name; DROP TABLE Users;")]
        public void CreateUsuarioRequest_InvalidNombre_ShouldBeInvalid(string nombre)
        {
            var model = new CreateUsuarioRequest { Nombre = nombre, Rol = "User" };

            var valid = TryValidate(model, out var results);

            Assert.False(valid);
            Assert.Contains(results, r => r.MemberNames.Contains("Nombre"));
        }

        [Theory]
        [InlineData("Admin")]
        [InlineData("User")]
        [InlineData("Guest")]
        public void CreateUsuarioRequest_ValidRol_ShouldBeValid(string rol)
        {
            var model = new CreateUsuarioRequest { Nombre = "juan", Rol = rol };

            var valid = TryValidate(model, out var results);

            Assert.True(valid);
            Assert.Empty(results);
        }

        [Fact]
        public void CreateUsuarioRequest_InvalidRol_ShouldBeInvalid()
        {
            var model = new CreateUsuarioRequest { Nombre = "juan", Rol = "Root" };

            var valid = TryValidate(model, out var results);

            Assert.False(valid);
            Assert.Contains(results, r => r.MemberNames.Contains("Rol"));
        }

        [Fact]
        public void GetUsuarioByNameRequest_InvalidXssName_ShouldBeInvalid()
        {
            var model = new GetUsuarioByNameRequest { Nombre = "<img src=x onerror=alert(1)>" };

            var valid = TryValidate(model, out var results);

            Assert.False(valid);
            Assert.Contains(results, r => r.MemberNames.Contains("Nombre"));
        }
    }
}
