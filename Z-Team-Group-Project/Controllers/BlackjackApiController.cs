using Microsoft.AspNetCore.Mvc;
using Z_Team_Group_Project.Services;
using Z_Team_Group_Project.Models;

namespace Z_Team_Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlackjackApiController : ControllerBase
    {
        private readonly BlackJackGame _game;

        public BlackjackApiController(BlackJackGame game)
        {
            _game = game;
        }

        // GET: api/BlackjackApi/Start
        [HttpGet("Start")]
        public IActionResult StartGame()
        {
            if (_game.PlayerHand.Count == 0 && _game.DealerHand.Count == 0)
            {
                _game.StartGame();
            }

            return Ok(GetGameState());
        }

        // POST: api/BlackjackApi/Hit
        [HttpPost("Hit")]
        public IActionResult Hit()
        {
            _game.PlayerHits();

            if (_game.CalculateScore(_game.PlayerHand) > 21)
            {
                return Stand();
            }

            return Ok(GetGameState());
        }

        // POST: api/BlackjackApi/Stand
        [HttpPost("Stand")]
        public IActionResult Stand()
        {
            while (_game.CalculateScore(_game.DealerHand) < 17)
            {
                _game.DealerPlays();
            }
            _game.GameOver = true;

            return Ok(GetGameState());
        }

        // POST: api/BlackjackApi/Reset
        [HttpPost("Reset")]
        public IActionResult Reset()
        {
            _game.PlayerHand.Clear();
            _game.DealerHand.Clear();
            _game.Deck = new Deck();
            _game.Deck.Shuffle();
            _game.GameOver = false;

            return Ok(GetGameState());
        }

        // Helper method to get the current game state
        private object GetGameState()
        {
            return new
            {
                PlayerHand = _game.PlayerHand,
                DealerHand = _game.DealerHand,
                PlayerScore = _game.CalculateScore(_game.PlayerHand),
                DealerScore = _game.CalculateScore(_game.DealerHand),
                GameOver = _game.GameOver
            };
        }
    }
}