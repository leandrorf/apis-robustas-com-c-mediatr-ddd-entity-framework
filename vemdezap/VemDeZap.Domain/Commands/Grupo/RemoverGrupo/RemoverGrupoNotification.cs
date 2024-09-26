using MediatR;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Domain.Commands.Grupo.RemoverGrupo
{
    public class RemoverGrupoNotification : INotification
    {
        public RemoverGrupoNotification( GrupoEntity grupo )
        {
            Grupo = grupo;
        }

        public GrupoEntity Grupo { get; set; }
    }
}