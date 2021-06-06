using Microsoft.EntityFrameworkCore;
using Nibo.Domain.Models;
using Nibo.Infra.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Infra.Context
{
    public class NiboContext: DbContext
    {
        public NiboContext(DbContextOptions<NiboContext> options): base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionMap());
        }
    }
}
