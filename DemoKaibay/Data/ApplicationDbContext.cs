using DemoKaibay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoKaibay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Auction>()
                .HasMany(c => c.Bids)
                .WithOne(d => d.Auction)
                .HasPrincipalKey(e => e.Id);


            base.OnModelCreating(builder);
        }

    }
}