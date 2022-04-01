using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record Photo {
        [Key]
        public int PhotoId { get; init; }
        [Required]
        public string Filename { get; init; }
        public Mini Mini { get; init; }
    }
}