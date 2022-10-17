using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiTicTacToe.Contracts;

public class Game
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string GameState { get; set; } = "_________";

    [ForeignKey("PlayerId")]
    public Player Player { get; set; }

    public Guid PlayerId { get; set; }

    public string winner { get; set; } = "";
}
