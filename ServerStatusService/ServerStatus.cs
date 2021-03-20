using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ServerStatusService
{
    public class ServerStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }
    }

    public class ServerStatusConfiguration : IEntityTypeConfiguration<ServerStatus>
    {
        public void Configure(EntityTypeBuilder<ServerStatus> builder)
        {
            builder.ToTable("ServerStatus");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).IsRequired();
        }
    }
}
