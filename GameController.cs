using Microsoft.AspNetCore.Mvc;
using tictactou.Models; // Asegúrate de que este namespace sea correcto

namespace TuProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static readonly Game CurrentGame = new Game();

        // GET: api/game
        [HttpGet]
        public ActionResult<Game> GetGameState()
        {
            return CurrentGame;
        }

        // POST: api/game/move
        [HttpPost("move")]
        public ActionResult<Game> MakeMove(int row, int col)
        {
            if (CurrentGame.Board[row][col] == '-' && CurrentGame.Winner == null)
            {
                CurrentGame.Board[row][col] = CurrentGame.CurrentPlayer;
                CheckWinner();
                if (CurrentGame.Winner == null)
                {
                    SwitchPlayer();
                }
            }
            return CurrentGame;
        }

        // POST: api/game/reset
        [HttpPost("reset")]
        public ActionResult<Game> ResetGame()
        {
            CurrentGame.Board = new char[][]
            {
                new char[] { '-', '-', '-' },
                new char[] { '-', '-', '-' },
                new char[] { '-', '-', '-' }
            };
            CurrentGame.CurrentPlayer = 'X';
            CurrentGame.Winner = null;
            CurrentGame.IsDraw = false;
            return CurrentGame;
        }

        private void CheckWinner()
        {
            // Verificar filas
            for (int i = 0; i < 3; i++)
            {
                if (CurrentGame.Board[i][0] != '-' &&
                    CurrentGame.Board[i][0] == CurrentGame.Board[i][1] &&
                    CurrentGame.Board[i][1] == CurrentGame.Board[i][2])
                {
                    CurrentGame.Winner = CurrentGame.Board[i][0];
                    return;
                }
            }

            // Verificar columnas
            for (int i = 0; i < 3; i++)
            {
                if (CurrentGame.Board[0][i] != '-' &&
                    CurrentGame.Board[0][i] == CurrentGame.Board[1][i] &&
                    CurrentGame.Board[1][i] == CurrentGame.Board[2][i])
                {
                    CurrentGame.Winner = CurrentGame.Board[0][i];
                    return;
                }
            }

            // Verificar diagonales
            if (CurrentGame.Board[0][0] != '-' &&
                CurrentGame.Board[0][0] == CurrentGame.Board[1][1] &&
                CurrentGame.Board[1][1] == CurrentGame.Board[2][2])
            {
                CurrentGame.Winner = CurrentGame.Board[0][0];
                return;
            }

            if (CurrentGame.Board[0][2] != '-' &&
                CurrentGame.Board[0][2] == CurrentGame.Board[1][1] &&
                CurrentGame.Board[1][1] == CurrentGame.Board[2][0])
            {
                CurrentGame.Winner = CurrentGame.Board[0][2];
                return;
            }

            // Verificar empate
            bool isDraw = true;
            foreach (var row in CurrentGame.Board)
            {
                if (row.Contains('-'))
                {
                    isDraw = false;
                    break;
                }
            }
            CurrentGame.IsDraw = isDraw;
        }

        private void SwitchPlayer()
        {
            CurrentGame.CurrentPlayer = CurrentGame.CurrentPlayer == 'X' ? 'O' : 'X';
        }
    }
}
