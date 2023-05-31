using prmToolkit.NotificationPattern;
using Qsti.ReciboOnline.Domain.Entities.Base;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class Empresa : EntityBase
    {
        protected Empresa()
        {

        }
        public Empresa(string nome)
        {
            Nome = nome;
            Administrador = false;
            new AddNotifications<Empresa>(this)
              .IfNullOrInvalidLength(x => x.Nome, 3, 150);
        }

        public string Nome { get; set; }
        public bool Administrador { get; set; }
    }
}
