using DocumentsStorage.Infra.Data.Extentions;
using DocumentsStorage.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PPECB.UserLinking.Infra.Data.Mappings
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public override void Map(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Files");

            builder.HasKey(t => t.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id");
        }
    }
}
