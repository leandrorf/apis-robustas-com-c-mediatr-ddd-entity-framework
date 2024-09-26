using VemDeZap.Infra.Repositories.Base;

namespace VemDeZap.Infra.Repositories.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork( DataContext context )
        {
            _context = context;
        }

        public void SaveChanges( )
        {
            _context.SaveChanges( );
        }
    }
}