using System;
using Qsti.ReciboOnline.Infra.Repositories.Base;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories;


namespace Qsti.ReciboOnline.Infra.Repositories
{
    public class RepositorySolicitacao : RepositoryBase<Solicitacao, Guid>, IRepositorySolicitacao
    {
        private readonly Context _context;
        public RepositorySolicitacao(Context context) : base(context)
        {
            _context = context;
        }
    }
}
