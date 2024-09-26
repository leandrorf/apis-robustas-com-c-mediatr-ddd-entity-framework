using VemDeZap.Domain.Entities;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Infra.Repositories.Base;

namespace VemDeZap.Infra.Repositories
{
    public class UsuarioRespository : BaseRepository<UsuarioEntity, Guid>, IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRespository( DataContext context ) : base( context )
        {
            _context = context;
        }
    }
}