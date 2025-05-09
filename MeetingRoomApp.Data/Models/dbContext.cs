using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MeetingRoomApp.Data.Models
{
    public class dbContext(DbContextOptions<dbContext> options) : DbContext(options)
    {

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AccountRole> AccountRole { get; set; }
        public virtual DbSet<MeetingRoom> MeetingRoom { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<TblMasterRole> TblMasterRole { get; set; }
        public virtual DbSet<TblSystemParameter> TblSystemParameter { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithOne(u => u.Account)
            .HasForeignKey<Account>(a => a.UserId);

            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(u => u.AccountRoles)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.User)
                .WithOne(u => u.Booking)
                .HasForeignKey<Booking>(a => a.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.MeetingRoom)
                .WithOne(u => u.Booking)
                .HasForeignKey<Booking>(a => a.MeetingRoomId);

        }
    }

}
