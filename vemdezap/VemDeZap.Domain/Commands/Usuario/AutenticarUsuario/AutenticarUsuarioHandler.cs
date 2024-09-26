using MediatR;
using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Extensions;
using VemDeZap.Domain.Interfaces.Repositories;

namespace VemDeZap.Domain.Commands.Usuario.AutenticarUsuario
{
    public class AutenticarUsuarioHandler : Notifiable, IRequestHandler<AutenticarUsuarioResquest, AutenticarUsuarioResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _repositoryUsuario;

        public AutenticarUsuarioHandler( IMediator mediator, IUsuarioRepository repositoryUsuario )
        {
            _mediator = mediator;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<AutenticarUsuarioResponse> Handle( AutenticarUsuarioResquest request, CancellationToken cancellationToken )
        {
            //Valida se o objeto request esta nulo
            if ( request == null )
            {
                AddNotification( "Request", "Request é obrigatório" );
                return null;
            }

            request.Senha = request.Senha.ConvertToMD5( );

            var usuario = _repositoryUsuario.ObterPor( x => x.Email == request.Email && x.Senha == request.Senha );

            if ( usuario == null )
            {
                AddNotification( "Usuario", "Usuário não encontrado." );
                return new AutenticarUsuarioResponse( )
                {
                    Autenticado = false
                };
            }

            //Cria objeto de resposta
            var response = ( AutenticarUsuarioResponse )usuario;

            ////Retorna o resultado
            return await Task.FromResult( response );
        }
    }
}