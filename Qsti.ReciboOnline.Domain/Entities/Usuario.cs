using prmToolkit.NotificationPattern;
using System;
using Qsti.ReciboOnline.Domain.Entities.Base;
using Qsti.ReciboOnline.Domain.Extensions;
using Qsti.ReciboOnline.Domain.Enums.Usuario;

namespace Qsti.ReciboOnline.Domain.Entities
{
    public class Usuario : EntityBase
    {
        protected Usuario()
        {

        }
        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            new AddNotifications<Usuario>(this)
                .IfNullOrInvalidLength(x => x.Nome, 3, 150, "O primeiro nome deve conter entre 3 a 150 caracteres")
                
                .IfNotEmail(x=>x.Email)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32);

            if (!string.IsNullOrEmpty(this.Senha))
            {
                this.Senha = Senha.ConvertToMD5();
            }

            DataCadastro = DateTime.Now;
            Status = EnumStatus.Inativo;
        }

        public void AlterarSenha(string senha)
        {
            this.Senha = senha;

            new AddNotifications<Usuario>(this)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32);

            if (!string.IsNullOrEmpty(this.Senha))
            {
                this.Senha = this.Senha.ConvertToMD5();
            }
        }
        
        public  void MudarStatus(Usuario usuarioMudancaStatus, EnumStatus status)
        {
            Status = status;
            UsuarioUltimaAlteracao = usuarioMudancaStatus;
            DataUltimaMudancaStatus = DateTime.Now;

            new AddNotifications<Usuario>(this)
               .IfEnumInvalid(x => x.Status);
        }


        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public EnumStatus Status { get; private set; }
        public Usuario UsuarioUltimaAlteracao { get; set; }
        public DateTime DataUltimaMudancaStatus { get; private set; }
   
    }
}
