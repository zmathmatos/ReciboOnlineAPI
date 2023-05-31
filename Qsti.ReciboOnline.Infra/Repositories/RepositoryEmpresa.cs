using System;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Infra.Repositories.Base;

namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RepositoryEmpresa : RepositoryBase<Empresa, Guid>, IRepositoryEmpresa
    {
        private readonly Context _context;
        public RepositoryEmpresa(Context context) : base(context)
        {
            _context = context;
        }
    }
}
