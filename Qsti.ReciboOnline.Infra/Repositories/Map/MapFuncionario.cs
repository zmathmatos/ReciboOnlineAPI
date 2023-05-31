using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Infra.Repositories.Map
{
    public class MapFuncionario : IEntityTypeConfiguration<Funcionario>
    {
    
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("Funcionario");

            ////Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Matricula).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
