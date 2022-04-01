using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MiniBackend.Models {
    public record Mini {
        public Mini ()
        {
            this.Paints = new HashSet<Paint>();
            this.Photos = new HashSet<Photo>();
        }
        [Key]
        public int MiniId { get; init; }
        public DateTime CompletionDate { get; init; }
        [Required]
        public string MiniName { get; init; }
        public string Sculptor { get; init; }
        [Required]
        public Game Game { get; init; }
        [InverseProperty("Mini")]
        public ICollection<Photo> Photos { get; init; }
        public virtual ICollection<Paint> Paints { get; init; }
    }

    public record PaintBrand {
        [Key]
        public int BrandId { get; init; }
        [Required]
        public string BrandName { get; init; }
    }

    public record ColorFamily
    {
        [Key]
        public int ColorId { get; init; }
        [Required]
        public string ColorFamilyName { get; init; }
    }

    public record Paint {
        public Paint()
        {
            this.Minis = new HashSet<Mini>();
        }

        [Key]
        public int PaintId { get; init; }
        [Required]
        public string ColorName { get; init; }
        public string? SerialNumber { get; init; }
        public ColorFamily ColorFamily { get; init; }
        [Required]
        public PaintBrand PaintBrand { get; init; }
        public virtual ICollection<Mini> Minis { get; init; }
    }
}