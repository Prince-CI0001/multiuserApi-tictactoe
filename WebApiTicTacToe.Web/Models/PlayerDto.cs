using System.ComponentModel.DataAnnotations;

namespace WebApiTicTacToe.Web
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
