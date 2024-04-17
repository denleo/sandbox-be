using Gateway.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateway.Data.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.HasIndex(u => u.FirebaseId).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(x => x.AvatarUrl).IsRequired(false);
    }
}