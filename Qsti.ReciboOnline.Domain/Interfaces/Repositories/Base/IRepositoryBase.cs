
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Qsti.ReciboOnline.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntidade, TId>
     where TEntidade : class
     where TId : struct

    {
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where);
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        /// <summary>
        /// Lista entidade podendo realizar includes de objetos via String. Ex: var ticketCollection = _repositoryTicket.ListarPor("Funcionario.Empresa");"
        /// </summary>
        
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params string[] includeProperties);
        
        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params string[] includeProperties);
        
        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        /// <summary>
        /// Lista entidade ordenada podendo realizar includes de objetos via String. Ex: var ticketCollection = _repositoryTicket.ListarEOrdenadosPor(x=>x.Id=1, "Funcionario.Empresa");"
        /// </summary>
        
        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, params string[] includeProperties);
        
        TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        TEntidade ObterPor(Func<TEntidade, bool> where, params string[] includeProperties);
        
        TEntidade ObterPor(Func<TEntidade, bool> where);
        
        bool Existe(Func<TEntidade, bool> where);
        
        bool Existe(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);
        
        bool Existe(Func<TEntidade, bool> where, params string[] includeProperties);
        
        IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties);
        
        /// <summary>
        /// Lista entidsade podendo realizar includes de objetos via String. Ex: var ticketCollection = _repositoryTicket.Listar("Funcionario.Empresa");"
        /// </summary>
        
        IQueryable<TEntidade> Listar(params string[] includeProperties);
        
        IQueryable<TEntidade> Listar();
        TEntidade ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade ObterPorId(TId id, params string[] includeProperties);

        TEntidade ObterPorId(TId id);

        TEntidade Adicionar(TEntidade entidade);

        TEntidade Editar(TEntidade entidade);

        void Remover(TEntidade entidade);

        void Remover(IEnumerable<TEntidade> entidades);

        void AdicionarLista(IEnumerable<TEntidade> entidades);

        
    }
}