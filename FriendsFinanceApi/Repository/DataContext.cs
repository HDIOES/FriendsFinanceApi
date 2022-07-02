using FriendsFinanceApi.Repository.Models;
using Microsoft.EntityFrameworkCore;


namespace FriendsFinanceApi.Repository
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activitys { get; set; }

        public DbSet<ActivityMember> ActivityMembers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<ActivityMember>()
        //            .HasKey(bc => new { bc.UserId, bc.ActivityId });
        //    modelBuilder.Entity<ActivityMember>()
        //        .HasOne(x => x.Activity)
        //        .WithMany(x => x.ActivityMembers)
        //        .HasForeignKey(x => x.ActivityId);

   
                     

        //    modelBuilder.Entity<Activity>()
        //        .HasOne(x => x.Owner);
        //    modelBuilder.Entity<User>();


        //}

        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.88.15;Port=5432;Database=ffapp;Username=postgres;Password=postgres");
        }
    }

}
