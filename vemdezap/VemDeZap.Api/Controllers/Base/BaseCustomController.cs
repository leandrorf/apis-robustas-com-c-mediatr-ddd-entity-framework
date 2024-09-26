using Microsoft.AspNetCore.Mvc;
using VemDeZap.Domain.Commands;
using VemDeZap.Infra.Repositories.Transactions;

namespace VemDeZap.Api.Controllers.Base
{
    public class BaseCustomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseCustomController( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        [NonAction]
        public async Task<IActionResult> ResponseAsync( Response response )
        {
            if ( !response.Notifications.Any( ) )
            {
                try
                {
                    _unitOfWork.SaveChanges( );

                    return Ok( response );
                }
                catch ( Exception ex )
                {
                    return BadRequest( $"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}" );
                }
            }
            else
            {
                return Ok( response );
            }
        }

        [NonAction]
        public async Task<IActionResult> ResponseExceptionAsync( Exception ex )
        {
            return BadRequest( new { errors = ex.Message, exception = ex.ToString( ) } );
        }
    }
}