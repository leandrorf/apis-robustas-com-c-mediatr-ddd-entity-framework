﻿using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Domain.Resources;

namespace VemDeZap.Domain.Commands.Grupo.ListarGrupo
{
    public class ListarGrupoHandler : Notifiable, IRequestHandler<ListarGrupoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IGrupoRepository _repositoryGrupo;

        public ListarGrupoHandler( IMediator mediator, IGrupoRepository repositoryGrupo )
        {
            _mediator = mediator;
            _repositoryGrupo = repositoryGrupo;
        }

        public async Task<Response> Handle( ListarGrupoRequest request, CancellationToken cancellationToken )
        {
            //Valida se o objeto request esta nulo
            if ( request == null )
            {
                AddNotification( "Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat( "Request" ) );
                return new Response( this );
            }

            var grupoCollection = _repositoryGrupo.Listar( ).ToList( );

            //Cria objeto de resposta
            var response = new Response( this, grupoCollection );

            ////Retorna o resultado
            return await Task.FromResult( response );
        }
    }
}