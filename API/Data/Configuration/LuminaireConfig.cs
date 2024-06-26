using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configuration
{
    public class LuminaireConfig : IEntityTypeConfiguration<LuminareModel>
    {
        public void Configure(EntityTypeBuilder<LuminareModel> builder)
        {
            builder.HasKey(x => x.UID);
            builder.HasIndex(x => x.Address).IsUnique();
        }
    }
}