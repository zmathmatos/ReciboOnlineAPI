using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qsti.ReciboOnline.Domain.Entities;

namespace Qsti.ReciboOnline.Infra.Repositories.Map
{
    public class MapPushNotification : IEntityTypeConfiguration<PushNotification>
    {

        public void Configure(EntityTypeBuilder<PushNotification> builder)
        {
            builder.ToTable("PushNotification");

            ////Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).HasMaxLength(200).IsRequired();

            //Foreikey
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");
        }
    }
}
