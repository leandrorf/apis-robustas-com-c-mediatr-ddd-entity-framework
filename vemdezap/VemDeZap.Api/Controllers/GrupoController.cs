using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VemDeZap.Domain.Commands.Grupo.AdicionarGrupo;
using VemDeZap.Domain.Commands.Grupo.AlterarGrupo;
using VemDeZap.Domain.Commands.Grupo.ListarGrupo;
using VemDeZap.Domain.Commands.Grupo.RemoverGrupo;
using VemDeZap.Domain.Commands.Usuario.AutenticarUsuario;
using VemDeZap.Infra.Repositories.Transactions;

namespace VemDeZap.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class GrupoController : Base.BaseCustomController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GrupoController( IHttpContextAccessor httpContextAccessor, IMediator mediator, IUnitOfWork unitOfWork ) : base( unitOfWork )
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get( )
        {
            try
            {
                var request = new ListarGrupoRequest( );
                var result = await _mediator.Send( request, CancellationToken.None );
                return Ok( result );
            }
            catch ( System.Exception ex )
            {
                return NotFound( ex.Message );
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] AdicionarGrupoRequest request )
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst( "Usuario" ).Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>( usuarioClaims );

                request.IdUsuario = usuarioResponse.Id;

                var response = await _mediator.Send( request, CancellationToken.None );
                return await ResponseAsync( response );
            }
            catch ( System.Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] AlterarGrupoRequest request )
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst( "Usuario" ).Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>( usuarioClaims );

                request.IdUsuario = usuarioResponse.Id;

                var response = await _mediator.Send( request, CancellationToken.None );
                return await ResponseAsync( response );
            }
            catch ( System.Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [Authorize]
        [HttpDelete]
        [Route( "{id:Guid}" )]
        public async Task<IActionResult> Delete( Guid id )
        {
            try
            {
                var request = new RemoverGrupoResquest( id );
                var result = await _mediator.Send( request, CancellationToken.None );
                return await ResponseAsync( result );
            }
            catch ( System.Exception ex )
            {
                return NotFound( ex.Message );
            }
        }
    }
}