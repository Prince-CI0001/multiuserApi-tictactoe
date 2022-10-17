using Microsoft.EntityFrameworkCore;
using WebApiTicTacToe.Contracts;


namespace TicTacToe.Data
{
    public class GameState : DbContext
    {
        public GameState(DbContextOptions<GameState> options) : base(options)
        {

        }
        
        public DbSet<Player> Player { get; set; }
        public DbSet<Game> Game { get; set; }
    }
}