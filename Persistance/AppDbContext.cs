using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<ReactActivity> ReactActivities { get; set; }

    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>(x =>
        {
            x.HasKey(aa => new { aa.UserId, aa.ActivityId });

            x.HasOne(aa => aa.User)
             .WithMany(u => u.Activities)
             .HasForeignKey(aa => aa.UserId);

            x.HasOne(aa => aa.Activity)
             .WithMany(a => a.Attendees)
             .HasForeignKey(aa => aa.ActivityId);
        });
    }
}
