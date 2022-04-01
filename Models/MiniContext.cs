using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MiniBackend.Models
{
    public class MiniContext : DbContext
    {
        public MiniContext(DbContextOptions<MiniContext> options)
            : base(options)
        {
        }

        public DbSet<Mini> Minis { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<MiniMeta> MiniMeta { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<Paint> Paints { get; set; } = null!;
    }
}