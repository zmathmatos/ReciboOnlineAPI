using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Qsti.ReciboOnline.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario : IRepositoryBase<Usuario, Guid> { }
    public interface IRepositoryFuncionario : IRepositoryBase<Funcionario, Guid> { }
    public interface IRepositoryRecibo : IRepositoryBase<Recibo, Guid> { }
    public interface IRepositorySolicitacao : IRepositoryBase<Solicitacao, Guid> { }
    public interface IRepositoryPushNotification : IRepositoryBase<PushNotification, Guid> { }
    public interface IRepositoryEmpresa : IRepositoryBase<Empresa, Guid> { }


}
 