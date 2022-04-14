using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using UploadFilesServer.Services;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IMinisRepository repository;
        private readonly IUploadService uploadService;

        public PhotosController(IMinisRepository repository, IUploadService uploadService) {
            this.repository = repository;
            this.uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        }

        [HttpGet("{id}")]
        public ActionResult<PhotoDTO> GetPhoto(int id)
        {
            var existingPhoto = repository.GetPhoto(id);
            if(existingPhoto is null) {
                return NotFound();
            }

            return Ok(existingPhoto.AsDto());
        }

        [HttpGet("mini/all/{id}")]
        public IEnumerable<PhotoDTO> GetPhotosForMini(int id)
        {
            var photos = repository.GetPhotosForMini(id).Select( photo => photo.AsDto());
            return photos;
        }

        [HttpDelete("mini/{id}")]
        public async Task<ActionResult> DeletePhoto(int id)
        {
            var existingPhoto = repository.GetPhoto(id);
            if(existingPhoto is null) {
                return NotFound();
            }

            string[] pathParts = existingPhoto.Filename.Split('/');
            string fileName = pathParts[pathParts.Length - 1];

            await uploadService.DeleteBlobFileAsync(fileName, "mini-photos");

            repository.DeletePhoto(id);

            return NoContent();
        }

        // TODO: #1 create function for getting the prefered or first photo for a mini
        // TODO: PUT
        // TODO: DELETE
    }
}