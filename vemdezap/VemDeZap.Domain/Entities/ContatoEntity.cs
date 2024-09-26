using VemDeZap.Domain.Entities.Base;
using VemDeZap.Domain.Enums;

namespace VemDeZap.Domain.Entities
{
    public class ContatoEntity : EntityBase
    {
        public ContatoEntity( )
        {
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public EnumNicho Nicho { get; set; }
        public UsuarioEntity Usuario { get; set; }
    }
}