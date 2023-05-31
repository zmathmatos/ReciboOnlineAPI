using prmToolkit.NotificationPattern;
using Qsti.ReciboOnline.Domain.Entities.Base;
using Qsti.ReciboOnline.Domain.Enums.Funcionario;
using Qsti.ReciboOnline.Domain.Extensions;
using System;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class Funcionario : EntityBase
    {
        protected Funcionario()
        {

        }
        
        public Funcionario(string matricula, string nome, string email, string senha)
        {
            Matricula = matricula;
            Nome = nome;
            Email = email;
            Senha = senha;

            Status = EnumStatus.Ativo;
            DataCadastro = DateTime.Now;

            new AddNotifications<Funcionario>(this)
              .IfNullOrInvalidLength(x => x.Nome, 3, 150)
                .IfNotEmail(x => x.Email)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32)
                
                ;

            if (!string.IsNullOrEmpty(this.Senha))
            {
                this.Senha = Senha.ConvertToMD5();
            }
        }

        public void AlterarFuncionario(string nome, string email)
        {
           
            Nome = nome;
            Email = email;

            new AddNotifications<Funcionario>(this)
              .IfNullOrInvalidLength(x => x.Nome, 3, 150)
                .IfNotEmail(x => x.Email)
 
                ;
        }

        public void Bloquear(Usuario usuarioBloqueio, string motivo)
        {
            UsuarioBloqueio = usuarioBloqueio;
            Motivo = motivo;
            DataBloqueio = DateTime.Now;
            Status = EnumStatus.Bloqueado;

            new AddNotifications<Funcionario>(this)
             .IfNullOrInvalidLength(x => x.Motivo, 3, 255);
        }

        public void Desbloquear(Usuario usuarioDesbloqueio, string motivo)
        {
            UsuarioDesbloqueio = usuarioDesbloqueio;
            Motivo = motivo;
            DataDesbloqueio = DateTime.Now;
            Status = EnumStatus.Ativo;

            new AddNotifications<Funcionario>(this)
             .IfNullOrInvalidLength(x => x.Motivo, 3, 255);
        }

        public void AlterarSenha(string senha)
        {
            this.Senha = senha;

            new AddNotifications<Funcionario>(this)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32);

            if (!string.IsNullOrEmpty(this.Senha))
            {
                this.Senha = this.Senha.ConvertToMD5();
            }
        }

        public void MudarStatus(Funcionario funcionarioMudancaStatus, EnumStatus status)
        {
            Status = status;
            
            DataUltimaMudancaStatus = DateTime.Now;

            new AddNotifications<Funcionario>(this)
               .IfEnumInvalid(x => x.Status);
        }

        public string Matricula { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCadastro { get; set; }
        public EnumStatus Status { get; private set; }
        public DateTime DataUltimaMudancaStatus { get;  set; }
        public Usuario UsuarioDesbloqueio { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public Usuario UsuarioBloqueio { get; set; }
        public DateTime? DataDesbloqueio { get; set; }
        public string Motivo { get; set; }
       // public Empresa Empresa { get;  set; }
    }
}
