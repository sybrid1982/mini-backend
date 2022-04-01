using System.ComponentModel.DataAnnotations;

namespace MiniBackend.DTOs
{
    public record CreateGameDTO {
        public string YearPublished { get; init; }
        [Required]
        public string GameName { get; init; }
        public string BoxArtUrl { get; init; }
        public string Style { get; init; }
        public string Scale { get; init; }
    }
}