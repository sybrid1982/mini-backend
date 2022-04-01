using System;
using MiniBackend.Models;

namespace MiniBackend.DTOs
{
    public record PhotoDTO {
        public int Id { get; init; }
        public string FileName { get; init; }
        public int MiniId { get; init; }
    }
}