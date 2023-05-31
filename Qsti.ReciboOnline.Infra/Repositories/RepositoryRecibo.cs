using System;
using Qsti.ReciboOnline.Infra.Repositories.Base;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using System.Linq;

namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RepositoryRecibo : RepositoryBase<Recibo, Guid>, IRepositoryRecibo
    {
        private readonly Context _context;
        public RepositoryRecibo(Context context) : base(context)
        {
            _context = context;
        }

    }
}
