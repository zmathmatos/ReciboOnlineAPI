using System;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Infra.Repositories.Base;

namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RespositoryUsuario : RepositoryBase<Usuario, Guid>, IRepositoryUsuario
    {
        private readonly Context _context;
        public RespositoryUsuario(Context context) : base(context)
        {
            _context = context;
        }
    }
}
