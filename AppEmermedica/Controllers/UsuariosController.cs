using AppEmermedica.Application.Interfaces;
using AppEmermedica.Application.Usuarios.Commands;
using AppEmermedica.Application.Usuarios.Queries;
using AppEmermedica.Application.Usuarios.Requests;
using AppEmermedica.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppEmermedica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuario _usuarioService;
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMediator _mediator;

        public UsuariosController(IUsuario usuarioService, ILogger<UsuariosController> logger, IMediator mediator )
        {
            _usuarioService = usuarioService;
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsuariosByName([FromQuery] GetUsuarioByNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _mediator.Send(new GetUsuarioByNameQuery(request.Nombre));
            if (usuario == null)
                return NotFound("No se encontraron usuarios con ese nombre.");

            return Ok(usuario);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioEntity = new Usuario
            {
                Nombre = request.Nombre,
                Rol = request.Rol
            };

            var createdUsuario = await _mediator.Send(new CreateUsuarioCommand(usuarioEntity));
            _logger.LogInformation("Usuario creado exitosamente: {Nombre}", createdUsuario.Nombre);
            return CreatedAtAction(nameof(GetUsuariosByName), new { nombre = createdUsuario.Nombre }, createdUsuario);
        }
    }
}
