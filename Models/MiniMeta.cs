using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record MiniMeta {
        [Key]
        public int MetaId { get; init; }
        public string Style { get; init; }
        public string Scale { get; init; }
    }
}