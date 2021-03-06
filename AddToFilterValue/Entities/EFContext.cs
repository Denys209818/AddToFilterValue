using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddToFilterValue.Entities
{
    public class EFContext : DbContext
    {
        public DbSet<FilterName> FilterNames { get; set; }
        public DbSet<FilterValue> FilterValue { get; set; }
        public DbSet<FilterNameValue> FilterNameValues { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=91.238.103.51;Port=5743;Database=denysdb;Username=denys;Password=qwerty1*;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilterNameValue>(addSettingsToNameValue => {
                addSettingsToNameValue.HasKey(primaryKeys => 
                new { primaryKeys.FilterNameId, primaryKeys.FilterValueId });

                addSettingsToNameValue.HasOne(virtualElementFromFilterName => 
                virtualElementFromFilterName.FilterName)
                .WithMany(virtualCollectionWithEntityToFilterName => 
                virtualCollectionWithEntityToFilterName.NameValues)
                .HasForeignKey(intParamWithForeignKeySettings => intParamWithForeignKeySettings.FilterNameId);

                addSettingsToNameValue.HasOne(virtualElementFromFilterValue => 
                virtualElementFromFilterValue.FilterValue)
                .WithMany(virtualCollectionWithEntityToFilterValue => 
                virtualCollectionWithEntityToFilterValue.NameValues)
                .HasForeignKey(intParamWithForeignKeySettings => intParamWithForeignKeySettings.FilterValueId);
            });
        }
    }
}
