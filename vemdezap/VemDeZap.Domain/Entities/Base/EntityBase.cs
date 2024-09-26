using prmToolkit.NotificationPattern;

namespace VemDeZap.Domain.Entities.Base
{
    public abstract class EntityBase : Notifiable
    {
        public Guid Id { get; set; }

        protected EntityBase( )
        {
            Id = Guid.NewGuid( );
        }
    }
}