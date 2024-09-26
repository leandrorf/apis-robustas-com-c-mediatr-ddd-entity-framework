using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities
{
    public class CampanhaEntity : EntityBase
    {
        public CampanhaEntity( )
        {
        }

        public string Nome { get; set; }
        public UsuarioEntity Usuario { get; set; }
    }
}