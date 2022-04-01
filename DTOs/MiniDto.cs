using System;
using MiniBackend.Models;

namespace MiniBackend.DTOs
{
    public record MiniDTO {
        public int Id { get; init; }
        public DateTime CompletionDate { get; init; }
        public string MiniName { get; init; }
        public string Sculptor { get; init; }
        public int GameId { get; init; }
        public string[] FileNames { get; init; }
    }
}