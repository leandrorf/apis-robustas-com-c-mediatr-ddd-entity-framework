using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VemDeZap.Infra.Repositories.Base
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext( string[ ] args )
        {
            var connectionString = "Server=localhost;Port=3306;Database=vemdezap;Uid=root;Pwd=5326";

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>( );

            optionsBuilder.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) );

            return new DataContext( optionsBuilder.Options );
        }
    }
}