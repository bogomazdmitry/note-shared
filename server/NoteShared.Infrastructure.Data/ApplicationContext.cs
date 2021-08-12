using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NoteShared.Infrastructure.Data.Entity.Users;
using NoteShared.Infrastructure.Data.Entity.Notes;
using NoteShared.Infrastructure.Data.Entity.NoteDesigns;
using NoteShared.Infrastructure.Data.Entity.NoteHistories;
using NoteShared.Infrastructure.Data.Entity.NoteTexts;

namespace NoteShared.Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteDesign> NoteDesigns { get; set; }

        public DbSet<NoteHistory> NoteHistories { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Notes)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Note>()
                .HasOne(u => u.NoteHistory)
                .WithOne(p => p.Note)
                .HasForeignKey<Note>(p => p.HistoryID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Note>()
                .HasOne(u => u.NoteDesign)
                .WithOne(p => p.Note)
                .HasForeignKey<NoteDesign>(u => u.NoteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<NoteText>()
                .HasMany(u => u.Notes)
                .WithOne(p => p.NoteText)
                .HasForeignKey(u => u.NoteTextID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
