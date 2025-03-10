using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        //Vermiş olduğumuz configration ayarlarını Entity üzerinden vermek kod kirliliğine sebep olur.
        //Bu nedenle IEntityTypeConfiguration inherit edilen class içerisinden tanımlanıyor.
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.Stock).IsRequired();
        }
    }
}
