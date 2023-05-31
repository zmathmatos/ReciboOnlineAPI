using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Infra.Repositories.Map
{
    public class MapRecibo : IEntityTypeConfiguration<Recibo>
    {
    
        public void Configure(EntityTypeBuilder<Recibo> builder)
        {
            builder.ToTable("Recibo");

            ////Propriedades
            builder.Property(x => x.Id);
            builder.Property(x => x.Matricula).HasMaxLength(10);
            builder.Property(x => x.Ano).HasMaxLength(4);
            builder.Property(x => x.Mes).HasMaxLength(2);
            builder.Property(x => x.CNPJ).HasMaxLength(14);
            builder.Property(x => x.RazaoSocial).HasMaxLength(40); ;
            builder.Property(x => x.Logradouro).HasMaxLength(50); ;
            builder.Property(x => x.Numero); ;
            builder.Property(x => x.Cidade).HasMaxLength(20); ;
            builder.Property(x => x.UF).HasMaxLength(2); ;
            builder.Property(x => x.Nome).HasMaxLength(40); ;
            builder.Property(x => x.Funcao).HasMaxLength(40); ;
            builder.Property(x => x.CentroCusto).HasMaxLength(25); ;
            builder.Property(x => x.ContaCorrente).HasMaxLength(25); ;
            builder.Property(x => x.CodigoPd); ;
            builder.Property(x => x.DescricaoCodigoPD).HasMaxLength(50); ;
            builder.Property(x => x.Referencia).HasMaxLength(20); ;
            builder.Property(x => x.Valor).HasMaxLength(20); ;
            builder.Property(x => x.TipoCodigo).HasMaxLength(1); ;
            builder.Property(x => x.DescricaoTipo).HasMaxLength(20); ;
            builder.Property(x => x.SalarioBase).HasMaxLength(20); ;
            builder.Property(x => x.TotalProventos).HasMaxLength(20); ;
            builder.Property(x => x.TotalDescontos).HasMaxLength(20); ;
            builder.Property(x => x.TotalLiquido).HasMaxLength(20); ;
            builder.Property(x => x.FGTSMes).HasMaxLength(20); ;
            builder.Property(x => x.BaseFGTS).HasMaxLength(20); ;
            builder.Property(x => x.BaseINSS).HasMaxLength(20); ;
            builder.Property(x => x.BaseIR).HasMaxLength(20); ;
            builder.Property(x => x.BaseIRFerias).HasMaxLength(20); ;
            builder.Property(x => x.OrdemTipo).HasMaxLength(20); ;
            builder.Property(x => x.Status); ;
            builder.Property(x => x.Visualizado); ;








        }
    }
}
