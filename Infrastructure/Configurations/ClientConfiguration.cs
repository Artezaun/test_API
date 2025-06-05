using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ClientConfiguration:IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

            builder.HasKey(c => c.id);

            builder.Property(c => c.id)
                .ValueGeneratedOnAdd(); // Эквивалент SERIAL (автоинкремент)

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar");

            builder.Property(c => c.SecondName)
                .IsRequired()
                .HasColumnType("varchar");

            builder.Property(c => c.BirthDate)
                .IsRequired()   
                .HasColumnType("timestamp with time zone");

        }
    }
}
