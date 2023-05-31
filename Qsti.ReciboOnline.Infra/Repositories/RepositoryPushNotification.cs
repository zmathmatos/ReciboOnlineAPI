using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Infra.Repositories.Base;
using System;

namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RepositoryPushNotification : RepositoryBase<PushNotification, Guid>, IRepositoryPushNotification
    {
        private readonly Context _context;
        public RepositoryPushNotification(Context context) : base(context)
        {
            _context = context;
        }
    }
}
