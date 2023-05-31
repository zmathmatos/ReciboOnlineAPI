using Qsti.ReciboOnline.Domain.Interfaces.Services;
using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Qsti.ReciboOnline.Infra.Services.ServiceEmail
{
    public class ServiceEmail : IServiceEmail
    {
        IConfiguration _configuration;

        protected ServiceEmail()
        {

        }
        public ServiceEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string destinatario, string assunto, string corpoMensagem)
        {
            try
            {
                string email = _configuration.GetSection("ServiceEmail").GetSection("Email").Value;
                string smtp = _configuration.GetSection("ServiceEmail").GetSection("Smtp").Value;
                int porta = int.Parse(_configuration.GetSection("ServiceEmail").GetSection("Porta").Value);
                string senha = _configuration.GetSection("ServiceEmail").GetSection("Senha").Value;


                bool bValidaEmail = ValidaEnderecoEmail(destinatario);
                if (bValidaEmail == false)
                    return false;

                string ambienteDeProducao = _configuration.GetSection("AmbienteDeProducao").Value;
                if (bool.Parse(ambienteDeProducao) == false)
                {
                    corpoMensagem += "<br><br><br>";
                    corpoMensagem += "################################################<br><br>";
                    corpoMensagem += "Ambiente: Desenvolvimento <br>";
                    corpoMensagem += "Origem: " + email + "<br>";
                    corpoMensagem += "Destino: " + destinatario + "<br><br>";
                    corpoMensagem += "################################################" ;
                    destinatario = _configuration.GetSection("EmailDesenv").Value;
                }

                MailMessage mensagemEmail = new MailMessage(email, destinatario, assunto, corpoMensagem);
                mensagemEmail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(smtp, porta);
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(email, senha);
                client.Credentials = cred;

                client.Send(mensagemEmail);



                mensagemEmail.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //AddNotification("Resquest", ex.Message);

                //string erro = ex.InnerException.ToString();
                return false;
            }
        }

        private static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar))
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EnviarParaUsuario(string assunto, string corpoMensagem)
        {
            string emailUsuario = _configuration.GetSection("EmailUsuario").Value;

            return Enviar(emailUsuario, assunto, corpoMensagem);

        }

    }
}
