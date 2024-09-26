using VemDeZap.Domain.Entities;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Infra.Repositories.Base;

namespace VemDeZap.Infra.Repositories
{
    public class GrupoRespository : BaseRepository<GrupoEntity, Guid>, IGrupoRepository
    {
        private readonly DataContext _context;

        public GrupoRespository( DataContext context ) : base( context )
        {
            _context = context;
        }
    }
}