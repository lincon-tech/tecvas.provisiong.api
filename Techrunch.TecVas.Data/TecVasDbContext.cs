﻿using Techrunch.TecVas.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Techrunch.TecVas.Entities.Common;
using Techrunch.TecVas.Entities.BusinessAccount;
using Techrunch.TecVas.Entities.Epurse;
using Techrunch.TecVas.Entities.Inventory;
using Techrunch.TecVas.Entities.Subscription;
using Techrunch.TecVas.Entities.Product;

namespace Techrunch.TecVas.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class TecVasDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _connectionString;
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfiguration _config;

        public TecVasDbContext(DbContextOptions<TecVasDbContext> options) : base(options)
        {
            //reducing database "chatter" in code first
            //step 1: turn off initialization -https://romiller.com/2014/06/10/reducing-code-first-database-chatter/
            //Database.SetInitializer<BodcContext>(null);
        }

        public TecVasDbContext()
        {

        }
        public DbSet<TopUpTransactionLog> TopUpRequests { get; set; }
        public DbSet<EpurseAccountMaster> EpurseAccounts { get; set; }
        public DbSet<EpurseAcctTransactions> EpurseAcctTransactions { get; set; }
        public DbSet<BusinessAccount> BusinessAccounts { get; set; }
        public DbSet<StockDetails> StockDetails { get; set; }
        public DbSet<VtuProducts> VtuProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApiCredentials> ApiCredentials { get; set; }

        public DbSet<DirectSalesMaster> DirectSalesMasters { get; set; }

        public DbSet<DirectSalesDetail> DirectSalesDetails { get; set; }

        public DbSet<StockMaster> StockMasters { get; set; }
        public DbSet<PartnerServiceProvider> PartnerServiceProviders { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }
        
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<StockLevels> StockLevels { get; set; }

        public DbSet<CarrierPrefix> CarrierPrefixes { get; set; }
        public DbSet<PartnerStockSalesHistory> PartnerStockSalesHistories { get; set; }
        private void OnEntityUpdating()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedAt = now;
                            break;
                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            break;
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseOracle(@"Data Source=10.161.10.195:1525/epin;User ID=epin;Password=emts818!", options => options
                //.UseOracleSQLCompatibility("11"));
                    

            }
            
        }
        public override int SaveChanges()
        {
            OnEntityUpdating();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnEntityUpdating();
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            //builder.Entity<HpinPinMailerJobs>().ToTable("HY_PIN_MAILER_JOBS");
            //builder.Entity<HpinPinMailerJobsDetail>().ToTable("HY_PIN_MAILER_JOBS_DETAIL");

            //one to many
            //builder.Entity<PinMailerJob>().HasMany(cg => cg.PinJobetails).WithOne(bb => bb.MailerJob);
            //builder.Entity<SalesOrder>().HasMany(cg => cg.OrderDetails).WithOne(bb => bb.SalesOrder);
            //builder.Entity<SalesOrder>().HasMany(cg => cg.PinMailerJobs).WithOne(bb => bb.SalesOrder);


            // builder.Entity<SalesOrderDetail>()
            //.HasOne(p => p.SalesOrder)
            //.WithMany(b => b.OrderDetails);
            // base.OnModelCreating(builder);

            // builder.Entity<PinMailerJobsDetail>()
            //.HasOne(p => p.MailerJob)
            //.WithMany(b => b.Jobetails);
            // base.OnModelCreating(builder);

            // builder.Entity<PinMailerJob>()
            //.HasOne(p => p.SalesOrder)
            //.WithMany(b => b.MailerJobs);
            // base.OnModelCreating(builder);

        }

        public void SeedData()
        {
            SeedRoles();
        }
        private void SeedRoles()
        {
            //var roles = new List<string>() { "User", "Admin" };

            //roles.ForEach((role) =>
            //{
            //    var exists = Roles.Any(x => x.Name == role);

            //    if (!exists)
            //    {
            //        Roles.Add(new Role
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = role,
            //            NormalizedName = role.ToUpper(),
            //            ConcurrencyStamp = Guid.NewGuid().ToString()
            //        });
            //    }
            //});
        }
        public static class ModelBuilderExtensions
        {
            //public static void SetupEnumStringConverters(this ModelBuilder builder)
            //{
            //    foreach (var entityType in builder.Model.GetEntityTypes())
            //    {
            //        foreach (var property in entityType.GetProperties())
            //        {
            //            if (property.ClrType.IsEnum)
            //            {
            //                builder.Entity(entityType.Name)
            //                    .Property(property.Name)
            //                    .HasConversion<string>();
            //            }
            //        }
            //    }
            //}
        }
    }
}

