using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Data;
using WebApiTicTacToe.Contracts;

namespace WebApiTicTacToe.Core
{
    public class GameRepository : IGameRepository
    {
        private readonly GameState _context;

        public GameRepository(GameState context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddPlayer(Player player)
        {
            if(player == null)
                throw new ArgumentNullException(nameof(player));


            foreach(var game in player.Games)
            {
                game.Id= player.Id;
            }
            _context.Player.Add(player);
        }

        public void AddGame(Game game)
        {
            _context.Game.Add(game);
        }

        public void DeleteGame(Game game)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Player> GetPlayers(IEnumerable<Guid> playerIds)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<Game> GetGame(Guid gameId)
        {
            if (gameId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(gameId));
            }

            return _context.Game.Where(c => c.Id == gameId);
        }

        public Game GetGame(Guid playerId, Guid gameId)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            if (gameId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(gameId));
            }

            return _context.Game
              .Where(c => c.PlayerId == playerId && c.Id == gameId).FirstOrDefault();
        }

        public Player GetPlayers(Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            return _context.Player.FirstOrDefault(a => a.Id == playerId);
        }

        public bool PlayerExists(Guid playerId)
        {
            if (playerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            return _context.Player.Any(a => a.Id == playerId);
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _context.Player.ToList<Player>();
        }

        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateGame(string gamestate, Guid playerId, Guid gameId)
        {
            var game = _context.Game
              .Where(c => c.PlayerId == playerId && c.Id == gameId).FirstOrDefault();
            game.GameState = gamestate;
            _context.SaveChanges();            
        }
        public void Winner(string win, Guid playerId, Guid gameId)
        {
            var game = _context.Game
              .Where(c => c.PlayerId == playerId && c.Id == gameId).FirstOrDefault();
            game.winner = win;
            _context.SaveChanges();
        }
    }
}
