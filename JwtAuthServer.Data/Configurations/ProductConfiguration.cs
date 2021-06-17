using JwtAuthServer.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthServer.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Name).IsRequired().HasMaxLength(200);
            builder.Property(k => k.Stock).IsRequired();
            builder.Property(k => k.Price).HasColumnType("decimal(18,2");
            builder.Property(k => k.UserId).IsRequired();
        }
    }
}
