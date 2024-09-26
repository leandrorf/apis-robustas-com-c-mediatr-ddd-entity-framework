using VemDeZap.Domain.Entities;
using VemDeZap.Domain.Interfaces.Repositories.Base;

namespace VemDeZap.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<UsuarioEntity, Guid>
    {
    }
}