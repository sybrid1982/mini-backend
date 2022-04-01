using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using UploadFilesServer.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IMinisRepository repository;
        private readonly IUploadService uploadService;

        public GamesController(IMinisRepository repository, IUploadService uploadService) 
        {
            this.repository = repository;
            this.uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        }

        [HttpGet]
        public IEnumerable<GameDTO> GetGames()
        {
            var games = repository.GetGames().Select( game => game.AsDto());
            return games;
        }

        // GET games/{id}
        [HttpGet("{id}")]
        public ActionResult<GameDTO> GetGame(int id)
        {
            var game = repository.GetGame(id);

            if( game is null) 
            {
                return NotFound();
            }

            return game.AsDto();
        }

        // POST /games
        [HttpPost]
        public ActionResult<GameDTO> CreateGame(CreateGameDTO gameDto)
        {
            int metaId = repository.FindMetaIdByValues(gameDto.Style, gameDto.Scale);

            MiniMeta meta;

            if(metaId >= 0) {
                meta = repository.GetMeta(metaId);
            } else {
                meta = new() {Style = gameDto.Style, Scale = gameDto.Scale };
                repository.CreateMeta(meta);
            }
            
            Game game = new()
            {
                GameName = gameDto.GameName,
                YearPublished = gameDto.YearPublished,
                BoxArtUrl = gameDto.BoxArtUrl,
                MiniMeta = meta
            };

            repository.CreateGame(game);

            return CreatedAtAction(nameof(GetGame), new { id = game.GameId}, game.AsDto());
        }

        // PUT /games/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateGame(int id, UpdateGameDTO gameDto)
        {
            var existingGame = repository.GetGame(id);
            if(existingGame is null)
            {
                return NotFound();
            }
            MiniMeta Meta = repository.GetMeta(gameDto.MetaId);

            Game updatedGame = existingGame with
            {
                GameName = gameDto.GameName,
                YearPublished = gameDto.YearPublished,
                BoxArtUrl = gameDto.BoxArtUrl,
                MiniMeta = Meta
            };

            repository.UpdateGame(updatedGame);

            return NoContent();
        }

        // DELETE /games/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            var existingGame = repository.GetGame(id);
            if(existingGame is null) {
                return NotFound();
            }

            repository.DeleteGame(id);

            return NoContent();
        }

        // POST /games/photo
        [HttpPost("photo")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            try
            {                
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var fileName = Path.GetRandomFileName() + extension;
                    string fileURL = await uploadService.UploadAsync(file.OpenReadStream(), fileName, file.ContentType, "box-art");

                    return Ok(new { fileURL });
                } else {
                    return BadRequest();
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}