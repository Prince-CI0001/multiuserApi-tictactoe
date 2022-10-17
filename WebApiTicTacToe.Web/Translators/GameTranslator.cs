using WebApiTicTacToe.Contracts;
using WebApiTicTacToe.Web.Models;

namespace WebApiTicTacToe.Web.Translators
{
    public static class GameTranslator
    {
        public static GameDto Translate(this Game Game)
        {
            return new GameDto()
            {
                Id = Game.Id,
                GameState = Game.GameState,
                PlayerId = Game.PlayerId,
                winner = Game.winner
            };
        }
    }
}
