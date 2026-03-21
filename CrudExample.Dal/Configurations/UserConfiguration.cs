using CrudExample.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudExample.Dal.Configurations;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Surname).IsRequired();
        builder.Property(u => u.Username).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.Password).IsRequired();
    }
}