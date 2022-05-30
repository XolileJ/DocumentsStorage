using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentsStorage.Infra.Data.Extentions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
