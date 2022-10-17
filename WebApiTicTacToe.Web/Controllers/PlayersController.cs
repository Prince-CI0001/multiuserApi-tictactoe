using Microsoft.AspNetCore.Mvc;
using WebApiTicTacToe.Contracts;

namespace WebApiTicTacToe.Web.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IBoardState _boardState;
        public PlayersController(IGameRepository gameRepository, IBoardState boardState)
        {
            _gameRepository = gameRepository ??
                throw new ArgumentNullException(nameof(gameRepository));
            _boardState = boardState;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<PlayerDto>> GetPlayers()
        {
            var playersFromRepo = _gameRepository.GetPlayers();
            var players = new List<PlayerDto>();
            foreach (var player in playersFromRepo)
            {
                players.Add(player.Translate());
                
                
            }
            return Ok(players);
        }
        [HttpGet("{playerId}")]
        public IActionResult GetPlayer(Guid playerId)
        {
            var playerFromRepo = _gameRepository.GetPlayers(playerId);
            return Ok(playerFromRepo.Translate());
        }

        [HttpPost]
        public ActionResult<PlayerDto> createPlayer(PlayerForCreationDto player)
        {
            var newPlayer = new Player() {
                Id = Guid.NewGuid(),
                Name = player.Name,
            };
            _boardState.EmptyMatrix();
            _gameRepository.AddPlayer(newPlayer);
            _gameRepository.save();
            return newPlayer.Translate();
        }
    }
}
