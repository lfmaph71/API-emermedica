using AppEmermedica.Controllers;
using AppEmermedica.Application.Interfaces;
using AppEmermedica.Application.Usuarios.Commands;
using AppEmermedica.Application.Usuarios.Queries;
using AppEmermedica.Application.Usuarios.Requests;
using AppEmermedica.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppEmermedica.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        private readonly Mock<IUsuario> _usuarioService = new();
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<ILogger<UsuariosController>> _logger = new();

        private UsuariosController CreateController()
        {
            return new UsuariosController(
                _usuarioService.Object,
                _logger.Object,
                _mediator.Object
            );
        }

        [Fact]
        public async Task GetUsuariosByName_ModelStateInvalid_ReturnsBadRequest()
        {
            var controller = CreateController();
            controller.ModelState.AddModelError("Nombre", "Requerido");

            var result = await controller.GetUsuariosByName(new GetUsuarioByNameRequest());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetUsuariosByName_UsuarioNoExiste_ReturnsNotFound()
        {
            _mediator
                .Setup(m => m.Send(It.IsAny<GetUsuarioByNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Usuario)null!);

            var controller = CreateController();
            var request = new GetUsuarioByNameRequest { Nombre = "juan" };

            var result = await controller.GetUsuariosByName(request);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetUsuariosByName_UsuarioExiste_ReturnsOk()
        {
            var expected = new Usuario { Id = 1, Nombre = "juan", Rol = "User" };

            _mediator
                .Setup(m => m.Send(It.Is<GetUsuarioByNameQuery>(q => q.name == "juan"), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var controller = CreateController();
            var request = new GetUsuarioByNameRequest { Nombre = "juan" };

            var result = await controller.GetUsuariosByName(request);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, ok.Value);
        }

        [Fact]
        public async Task Create_ModelStateInvalid_ReturnsBadRequest()
        {
            var controller = CreateController();
            controller.ModelState.AddModelError("Nombre", "Requerido");

            var result = await controller.Create(new CreateUsuarioRequest());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_Valido_ReturnsCreatedAtAction()
        {
            var request = new CreateUsuarioRequest
            {
                Nombre = "maria",
                Rol = "User"
            };

            var created = new Usuario { Id = 1, Nombre = "maria", Rol = "User" };

            _mediator
                .Setup(m => m.Send(It.Is<CreateUsuarioCommand>(c => c.usuario.Nombre == "maria" && c.usuario.Rol == "User"), It.IsAny<CancellationToken>()))
                .ReturnsAsync(created);

            var controller = CreateController();

            var result = await controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(UsuariosController.GetUsuariosByName), createdResult.ActionName);
            Assert.Equal("maria", createdResult.RouteValues!["nombre"]);
            Assert.Equal(created, createdResult.Value);
        }
    }
}
