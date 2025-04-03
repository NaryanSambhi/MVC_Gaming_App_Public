using Microsoft.AspNetCore.Mvc;
using Z_Team_Group_Project.Models;
using Z_Team_Group_Project.Services;

namespace Z_Team_Group_Project.Controllers
{
    public class GameController : Controller
    {
        private readonly BlackJackGame _game;

        public GameController(BlackJackGame game)
        {
            _game = game;
        }

        public IActionResult Blackjack()
        {
            if (_game.PlayerHand.Count == 0 && _game.DealerHand.Count == 0)
            {
                _game.StartGame();
            }
            if (_game.CalculateScore(_game.PlayerHand) == 21 || _game.CalculateScore(_game.DealerHand) == 21)
            {
                _game.GameOver = true;

            }

            return View(_game);
        }

        public IActionResult Hit()
        {
            _game.PlayerHits();

            if (_game.CalculateScore(_game.PlayerHand) > 21)
            {
                return RedirectToAction("Stand");
            }


            return RedirectToAction("Blackjack");
        }

        public IActionResult Stand()
        {
            while (_game.CalculateScore(_game.DealerHand) < 17)
            {
                _game.DealerPlays();
            }
            _game.GameOver = true;
            return RedirectToAction("Blackjack");
        }

        public IActionResult Reset()
        {
            _game.PlayerHand.Clear();
            _game.DealerHand.Clear();
            _game.Deck = new Deck();
            _game.Deck.Shuffle();
            _game.GameOver = false;
            return RedirectToAction("Blackjack");
        }
    }
}
