using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Infra.Repositories.Map
{
    public class MapSolicitacao : IEntityTypeConfiguration<Solicitacao>
    {
    
        public void Configure(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.ToTable("Solicitacao");

            ////Propriedades
            builder.Property(x => x.Id);
            builder.Property(x => x.Ano).HasMaxLength(4);
            builder.Property(x => x.Mes).HasMaxLength(2);
            builder.Property(x => x.Matricula); ;
            builder.Property(x => x.TipoFolha);
            builder.Property(x => x.Status).IsRequired();



            ////Foreikey


        }
    }
}
