using VemDeZap.Domain.Entities;

namespace VemDeZap.Domain.Commands.Usuario.AutenticarUsuario
{
    public class AutenticarUsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Autenticado { get; set; }

        public static explicit operator AutenticarUsuarioResponse( UsuarioEntity usuario )
        {
            return new AutenticarUsuarioResponse( )
            {
                Id = usuario.Id,
                Nome = usuario.PrimeiroNome,
                Autenticado = true
            };
        }
    }
}