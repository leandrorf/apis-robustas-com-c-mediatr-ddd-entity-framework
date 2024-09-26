using MediatR;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioNotification : INotification
    {
        public UsuarioEntity Usuario { get; set; }

        public AdicionarUsuarioNotification( UsuarioEntity usuario )
        {
            Usuario = usuario;
        }
    }
}