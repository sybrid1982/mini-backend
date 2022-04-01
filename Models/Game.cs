using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record Game {
        [Key]
        public int GameId { get; init; }
        public string YearPublished { get; init; }
        public string GameName { get; init; }
        public string BoxArtUrl { get; init; }     // location of the image
        public virtual MiniMeta MiniMeta { get; init; }
    }
}