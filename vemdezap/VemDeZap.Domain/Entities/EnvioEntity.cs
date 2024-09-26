using VemDeZap.Domain.Entities.Base;

namespace VemDeZap.Domain.Entities
{
    public class EnvioEntity : EntityBase
    {
        public EnvioEntity( )
        {
        }

        public CampanhaEntity Campanha { get; set; }
        public GrupoEntity Grupo { get; set; }
        public ContatoEntity Contato { get; set; }
        public bool Enviado { get; set; }
    }
}