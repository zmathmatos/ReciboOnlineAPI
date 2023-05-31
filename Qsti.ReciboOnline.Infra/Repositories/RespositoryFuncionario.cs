using System;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;
using Qsti.ReciboOnline.Infra.Repositories.Base;

namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RespositoryFuncionario : RepositoryBase<Funcionario, Guid>, IRepositoryFuncionario
    {
        private readonly Context _context;
        public RespositoryFuncionario(Context context) : base(context)
        {
            _context = context;
        }
    }
}
