using MediatR;
using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Entities;
using VemDeZap.Domain.Interfaces.Repositories;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioHandler : Notifiable, IRequestHandler<AdicionarUsuarioRequest, Response>
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMediator _Mediator;

        public AdicionarUsuarioHandler( IUsuarioRepository usuarioRepository, IMediator mediator )
        {
            _UsuarioRepository = usuarioRepository;
            _Mediator = mediator;
        }

        public async Task<Response> Handle( AdicionarUsuarioRequest request, CancellationToken cancellationToken )
        {
            if ( request == null )
            {
                AddNotification( "Request", "Informe os dados do usuário" );
                return new Response( this );
            }

            if ( _UsuarioRepository.Existe( x => x.Email == request.Email ) )
            {
                AddNotification( "Email", "E-mail já cadastrado no sistema" );
                return new Response( this );
            }

            var usuario = new UsuarioEntity(
                request.PrimeiroNome,
                request.UltimoNome,
                request.Email,
                request.Senha );

            AddNotifications( usuario );

            if ( IsInvalid( ) )
            {
                return new Response( this );
            }

            var result = _UsuarioRepository.Adicionar( usuario );

            var notification = new AdicionarUsuarioNotification( result );

            await _Mediator.Publish( notification );

            return await Task.FromResult( new Response( this, result ) );
        }
    }
}