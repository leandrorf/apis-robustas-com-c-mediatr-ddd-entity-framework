using MediatR;
using System.Diagnostics;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario.Notifications
{
    public class EnviarEmailDeAtivacaoDoUsuario : INotificationHandler<AdicionarUsuarioNotification>
    {
        public async Task Handle( AdicionarUsuarioNotification notification, CancellationToken cancellationToken )
        {
            Debug.WriteLine( "Enviar email de ativação para o usuário " + notification.Usuario.PrimeiroNome );
        }
    }
}