using System;

namespace Qsti.ReciboOnline.Domain.Commands.Funcionario.ListarFuncionario
{
    public class ListarFuncionarioResponse
    {
        public ListarFuncionarioResponse(Guid id, string nome, int qtdeAplicacoes)
        {
            Id = id;
            Nome = nome;
            QtdeAplicacoes = qtdeAplicacoes;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int QtdeAplicacoes { get; set; }
    }
}
