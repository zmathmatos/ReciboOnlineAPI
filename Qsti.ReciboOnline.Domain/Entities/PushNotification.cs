using prmToolkit.NotificationPattern;
using Qsti.ReciboOnline.Domain.Entities.Base;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class PushNotification : EntityBase
    {
        protected PushNotification()
        {

        }

        public PushNotification(Usuario usuario, string token)
        {
            Usuario = usuario;
            Token = token;

            new AddNotifications<PushNotification>(this)
              .IfNullOrInvalidLength(x => x.Token, 1, 200);
        }
        public PushNotification(Funcionario funcionario, string token)
        {
            Funcionario = funcionario;
            Token = token;

            new AddNotifications<PushNotification>(this)
              .IfNullOrInvalidLength(x => x.Token, 1, 200);
        }

        public Usuario Usuario { get; set; }
        public Funcionario Funcionario { get; set; }
        public string Token { get; set; }
    }
}
