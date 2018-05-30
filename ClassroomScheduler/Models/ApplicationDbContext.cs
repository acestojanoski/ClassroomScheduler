using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassroomScheduler.Models;

namespace ClassroomScheduler.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-To-Many relations
            modelBuilder.Entity<Building>()
                .HasMany(cr => cr.ClassRooms)
                .WithOne(b => b.Building)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithOne(et => et.EventType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassRoom>()
                .HasMany(e => e.Events)
                .WithOne(cr => cr.ClassRoom)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-To-Many relations
            modelBuilder.Entity<ProfessorCourse>()
                .HasKey(pc => new { pc.ProfessorId, pc.CourseId });

            modelBuilder.Entity<ProfessorCourse>()
                .HasOne(pt => pt.Professor)
                .WithMany(p => p.Courses)
                .HasForeignKey(pt => pt.ProfessorId);

            modelBuilder.Entity<ProfessorCourse>()
                .HasOne(pt => pt.Course)
                .WithMany(p => p.Professors)
                .HasForeignKey(pt => pt.CourseId);
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<ApplicationUser> Professors { get; set; }
        public DbSet<ProfessorCourse> ProfessorCourses { get; set; }
        public DbSet<EventRepetition> EventRepetitions { get; set; }
    }
}
