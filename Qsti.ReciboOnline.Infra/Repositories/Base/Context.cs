using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using Qsti.ReciboOnline.Domain.Entities;
using Qsti.ReciboOnline.Infra.Repositories.Map;

namespace Qsti.ReciboOnline.Infra.Repositories.Base
{
    public partial class Context : DbContext
    {
        //Criar as tabelas
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<PushNotification> PushNotification { get; set; }
        public DbSet<Recibo> Recibo { get; set; }
        public DbSet<Solicitacao> Solicitacao { get; set; }


        IConfiguration _configuration;

        public Context(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }

      //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //  {
      //      if (!optionsBuilder.IsConfigured)
      //      {
      //         optionsBuilder.UseMySql("Server=192.168.1.4;Database=qstireciboonline;Uid=root;Pwd=MySQL@dmin2023;");
      //      }
      //  }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conexao = _configuration.GetSection("Conexao").Value;
                optionsBuilder.UseMySql(conexao);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ignorar classes
            modelBuilder.Ignore<Notification>();
            //modelBuilder.Ignore<Nome>();
            //modelBuilder.Ignore<Email>();

            //aplicar configurações
            modelBuilder.ApplyConfiguration(new MapUsuario());
            modelBuilder.ApplyConfiguration(new MapFuncionario());
            modelBuilder.ApplyConfiguration(new MapPushNotification());
            modelBuilder.ApplyConfiguration(new MapRecibo());
            modelBuilder.ApplyConfiguration(new MapSolicitacao());
            modelBuilder.ApplyConfiguration(new MapEmpresa());

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
