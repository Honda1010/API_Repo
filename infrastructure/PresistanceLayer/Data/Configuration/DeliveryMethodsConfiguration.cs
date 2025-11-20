using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistanceLayer.Data.Configuration
{
	public class DeliveryMethodsConfiguration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.ToTable("DeliveryMethods");
			builder.Property(d => d.Price).HasColumnType("decimal(8,2)");
			builder.Property(d => d.ShortName).HasMaxLength(50).IsRequired();
			builder.Property(d => d.Description).HasMaxLength(100).IsRequired();
			builder.Property(d => d.DeliveryTime).HasMaxLength(50).IsRequired();
		}
	}
}
