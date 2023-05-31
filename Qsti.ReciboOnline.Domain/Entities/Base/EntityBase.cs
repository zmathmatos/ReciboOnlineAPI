using prmToolkit.NotificationPattern;
using System;

namespace Qsti.ReciboOnline.Domain.Entities.Base
{
    public abstract class EntityBase : Notifiable
    {
        //private Guid matricula;

        protected EntityBase()
        {
            Id = Guid.NewGuid();
         
     
        }

        public Guid Id { get; set; }
   
    }
}
