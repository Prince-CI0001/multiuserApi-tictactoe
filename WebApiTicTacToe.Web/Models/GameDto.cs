namespace WebApiTicTacToe.Web.Models
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string? GameState { get; set; }

        public Guid PlayerId { get; set; }

        public string? winner { get; set; }  
    }
}
