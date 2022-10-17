using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTicTacToe.Contracts;
using WebApiTicTacToe.Web.Models;
using WebApiTicTacToe.Web.Translators;

namespace WebApiTicTacToe.Web.Controllers
{
    [ApiController]
    [Route("api/players/{playerId}/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ??
                throw new ArgumentNullException(nameof(gameRepository));
        }
        [HttpGet]
        public ActionResult<IEnumerable<GameDto>> GetGameForPlayer(Guid playerId)
        {
            var gameForPlayerFromRepo = _gameRepository.GetGame(playerId);
            var games = new List<GameDto>();
            foreach (var game in gameForPlayerFromRepo)
            {
                games.Add(game.Translate());


            }
            return Ok(games);
        }
        [HttpGet("{gameId}")]
        public ActionResult<GameDto> GetGameForPlayer(Guid playerId, Guid gameId)
        {
            var gameForPlayerFromRepo = _gameRepository.GetGame(playerId, gameId);
            return Ok(gameForPlayerFromRepo.Translate());
        }

        [HttpPost]

        public ActionResult<GameDto> CreateGameForPlayer([FromRoute] Guid playerId)
        {
            var newGame = new Game()
            {
                Id = Guid.NewGuid(),
                PlayerId = playerId
            };
            _gameRepository.AddGame(newGame);
            _gameRepository.save();  
            return newGame.Translate();
        }

    }
}
