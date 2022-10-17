namespace WebApiTicTacToe.Contracts
{
    public interface IGameRepository
    {
        IEnumerable<Player> GetPlayers();
        Player GetPlayers(Guid playerId);
        void AddPlayer(Player player);

        bool PlayerExists(Guid playerId);
        bool save();

        IEnumerable<Game> GetGame(Guid GameId);
        Game GetGame(Guid playerId, Guid GameId);
        void AddGame(Game game);
        void UpdateGame(string gamestate,Guid playerId,Guid gameId);
        void Winner(string ch, Guid playerId, Guid gameId);
        void DeleteGame(Game game);

        

    }
}
