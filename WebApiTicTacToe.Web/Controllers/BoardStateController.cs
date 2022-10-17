using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTicTacToe.Contracts;

namespace WebApiTicTacToe.Web.Controllers
{
    [ApiController]
    [Route("api/players/{playerId}/games/{gameId}")]
    public class BoardStateController : ControllerBase
    {

        private readonly IBoardState _BoardStateRepo;
        public BoardStateController(IBoardState BoardStateRepo)
        {
            _BoardStateRepo = BoardStateRepo ??
                throw new ArgumentNullException(nameof(BoardStateRepo));
        }
        [HttpPatch]
        public IActionResult Set([FromBody] string location,[FromRoute] Guid playerId, [FromRoute] Guid gameId)
        {

            var index = _BoardStateRepo.ExecuteMove(location,playerId,gameId);
            return Ok(index);
        }

    }
}
