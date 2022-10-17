using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTicTacToe.Contracts
{
    public interface IBoardState
    {
        public string ExecuteMove(string location,Guid playerId,Guid gameId);

        public void EmptyMatrix();
    }
}
