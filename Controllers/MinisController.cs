using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using UploadFilesServer.Services;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("minis")]
    public class MinisController : ControllerBase
    {
        private readonly IMinisRepository repository;
        private readonly IUploadService uploadService;
        public record UrlQueryParameters(int Limit = 50, int Page = 1);

        public MinisController(IMinisRepository repository, IUploadService uploadService) {
            this.repository = repository;
            this.uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        }

        [HttpGet]
        public IEnumerable<MiniDTO> GetMinis()
        {
            var minis = repository.GetMinis().Select( mini => mini.AsDto());
            // If we are fetching all minis, then we only need one photo for each
            return minis;
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetMinisListAsync(
            [FromQuery] UrlQueryParameters parameters,
            CancellationToken cancellationToken
        )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try {
                var minis = await repository.GetMinisByPageAsync(
                    parameters.Limit,
                    parameters.Page,
                    cancellationToken
                );

                return Ok(minis);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        // GET minis/{id}
        [HttpGet("{id}")]
        public ActionResult<MiniDTO> GetMini(int id)
        {
            var mini = repository.GetMini(id);

            if (mini is null) {
                return NotFound();
            }

            // In this case, return all images as we are only fetching for one mini
            return mini.AsDto();
        }

        // GET /minis/game/{id}
        // Gets all the minis associated with a game
        [HttpGet("game/{id}")]
        public IEnumerable<MiniDTO> GetMinisByGame(int id)
        {
            var minis = repository.GetMinisByGame(id).Select(mini => mini.AsDto());
            // In this case, return only first image (later, preferred or first)
            return minis;
        }


        // POST /minis
        [HttpPost]
        public ActionResult<MiniDTO> CreateMini(CreateMiniDto miniDto)
        {
            DateTime completionDate = miniDto.CompletionDate != null ? miniDto.CompletionDate : DateTime.UtcNow;
            Game game = repository.GetGame(miniDto.GameId);
            Mini mini = new()
            {
                MiniName = miniDto.MiniName,
                Sculptor = miniDto.Sculptor,
                Game = game,
                CompletionDate = completionDate
            };

            repository.CreateMini(mini);

            return CreatedAtAction(nameof(GetMini), new { id = mini.MiniId}, mini.AsDto());
        }

        // PUT /minis/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMini(int id, UpdateMiniDto miniDto)
        {
            var existingMini = repository.GetMini(id);
            if(existingMini is null) {
                return NotFound();
            }

            Game game = repository.GetGame(miniDto.GameId);

            Mini updatedMini = existingMini with
            {
                MiniName = miniDto.MiniName,
                Sculptor = miniDto.Sculptor,
                Game = game,
                CompletionDate = miniDto.CompletionDate
            };

            repository.UpdateMini(updatedMini);

            return NoContent();
        }

        // DELETE /minis/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMini(int id)
        {
            var existingMini = repository.GetMini(id);
            if(existingMini is null) {
                return NotFound();
            }

            repository.DeleteMini(id);

            return NoContent();
        }

        // POST /minis/photo/id
        [HttpPost("photo/{id}")]
        public async Task<IActionResult> OnPostUploadAsync(int id, IFormFile file)
        {
            try
            {                
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var fileName = Path.GetRandomFileName() + extension;
                    string fileURL = await uploadService.UploadAsync(file.OpenReadStream(), fileName, file.ContentType, "mini-photos");

                    Mini mini = repository.GetMini(id);

                    Photo photo = new()
                    {
                        Mini = mini,
                        Filename = fileURL
                    };

                    repository.CreatePhoto(photo);
                    // return Ok( new { fileName = filePath});

                    return Ok(new { fileURL });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // GET /minis/photo/id
        [HttpGet("photo/{id}")]
        public ActionResult<IEnumerable<PhotoDTO>> GetPhotosForMini(int id) {
            try {
                var photos = repository.GetPhotosForMini(id).Select(photo => photo.AsDto());
                return Ok( new {photos});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        // // DELETE /minis/photo/filename
        // [HttpDelete("photo/{filename}")]

        // public async Task<IActionResult> DeletePhotoFromMini(string filename) {

        // }

    }
}