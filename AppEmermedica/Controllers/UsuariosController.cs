using AppEmermedica.Application.Interfaces;
using AppEmermedica.Application.Usuarios.Commands;
using AppEmermedica.Application.Usuarios.Queries;
using AppEmermedica.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetUsuariosByName([FromQuery][Required][StringLength(50)] string nombre)
        {
            try
            {
                var usuarios = await _mediator.Send(new GetUsuarioByNameQuery(nombre));
                if (usuarios == null)
                    return NotFound("No se encontraron usuarios con ese nombre.");

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios por nombre.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //var createdUsuario = await _usuarioService.Create(usuario);
                var createdUsuario = await _mediator.Send(new CreateUsuarioCommand(usuario));
                _logger.LogInformation("Usuario creado exitosamente: {Nombre}", createdUsuario.Nombre);
                return CreatedAtAction(nameof(GetUsuariosByName), new { nombre = createdUsuario.Nombre }, createdUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
