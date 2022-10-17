using WebApiTicTacToe.Contracts;

namespace WebApiTicTacToe.Web
{
    public static class PlayerTranslator
    {
        public static PlayerDto Translate(this Player Player)
        {
            return new PlayerDto()
            {
                Id = Player.Id,
                Name = Player.Name
            };
        }
    }
}
