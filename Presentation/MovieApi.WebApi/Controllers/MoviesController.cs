using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Application.Features.CQRSDesignPattern.Commands.MovieCommands;
using MovieApi.Application.Features.CQRSDesignPattern.Handlers.MovieHandlers;
using MovieApi.Application.Features.CQRSDesignPattern.Queries.MovieQueries;

namespace MovieApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CreateMovieCommandHandler _createMovieCommandHandler;
        private readonly GetMovieByIdQueryHandler _getMovieByIdQueryHandler;
        private readonly RemoveMovieCommandHandler _removeMovieCommandHandler;
        private readonly UpdateMovieCommandHandler _updateMovieCommandHandler;
        private readonly GetMovieQueryHandler _getMovieQueryHandler;

        public MoviesController(CreateMovieCommandHandler createMovieCommandHandler, GetMovieByIdQueryHandler getMovieByIdQueryHandler, RemoveMovieCommandHandler removeMovieCommandHandler, UpdateMovieCommandHandler updateMovieCommandHandler, GetMovieQueryHandler getMovieQueryHandler)
        {
            _createMovieCommandHandler = createMovieCommandHandler;
            _getMovieByIdQueryHandler = getMovieByIdQueryHandler;
            _removeMovieCommandHandler = removeMovieCommandHandler;
            _updateMovieCommandHandler = updateMovieCommandHandler;
            _getMovieQueryHandler = getMovieQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> MovieList()
        {
            var value = await _getMovieQueryHandler.Handle();
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            await _createMovieCommandHandler.Handle(command);
            return Ok("Movie Ekleme İşlemi Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(UpdateMovieCommand command)
        {
            await _updateMovieCommandHandler.Handle(command);
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _removeMovieCommandHandler.Handle(new RemoveMovieCommand(id));
            return Ok("Silme işlemi başarılı");
            
        }

        [HttpGet("GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var value = await _getMovieByIdQueryHandler.Handle(new GetMovieByIdQuery(id));
            return Ok(value);
        }
    }

}
