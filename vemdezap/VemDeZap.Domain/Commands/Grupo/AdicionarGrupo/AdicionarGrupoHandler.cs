using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using VemDeZap.Domain.Entities;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Domain.Resources;

namespace VemDeZap.Domain.Commands.Grupo.AdicionarGrupo
{
    public class AdicionarGrupoHandler : Notifiable, IRequestHandler<AdicionarGrupoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AdicionarGrupoHandler( IMediator mediator, IGrupoRepository grupoRepository, IUsuarioRepository usuarioRepository )
        {
            _mediator = mediator;
            _grupoRepository = grupoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Response> Handle( AdicionarGrupoRequest request, CancellationToken cancellationToken )
        {
            //Validar se o requeste veio preenchido
            if ( request == null )
            {
                AddNotification( "Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat( "Request" ) );

                return new Response( this );
            }

            var usuario = _usuarioRepository.ObterPorId( request.IdUsuario.Value );

            if ( usuario == null )
            {
                AddNotification( "Usuario", MSG.X0_NAO_INFORMADO.ToFormat( "Usuário" ) );
                return new Response( this );
            }

            var grupo = new GrupoEntity( usuario, request.Nome, request.Nicho );

            AddNotifications( grupo );

            if ( IsInvalid( ) )
            {
                return new Response( this );
            }

            grupo = _grupoRepository.Adicionar( grupo );

            var result = new { Id = grupo.Id, Nome = grupo.Nome, Nicho = grupo.Nicho };

            //Criar meu objeto de resposta
            var response = new Response( this, result );

            return await Task.FromResult( response );
        }
    }
}