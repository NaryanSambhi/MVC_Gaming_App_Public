using Z_Team_Group_Project.Models;
using System.Collections.Generic;

namespace Z_Team_Group_Project.Services
{
    public class BlackJackGame
    {
        public Deck Deck { get; set; }
        public List<Card> PlayerHand { get; private set; }
        public List<Card> DealerHand { get; private set; }
        public bool GameOver { get; set; }

        public BlackJackGame()
        {
            Deck = new Deck();
            Deck.Shuffle();
            PlayerHand = new List<Card>();
            DealerHand = new List<Card>();
        }

        public void StartGame()
        {
            PlayerHand.Add(Deck.DealCard());
            PlayerHand.Add(Deck.DealCard());
            DealerHand.Add(Deck.DealCard());
            DealerHand.Add(Deck.DealCard());
        }

        public void PlayerHits()
        {
            PlayerHand.Add(Deck.DealCard());
        }


        public void DealerPlays()
        {
            DealerHand.Add(Deck.DealCard());
        }

        public int CalculateScore(List<Card> Hand)
        {
            int score = Hand.Sum(card => card.Score);
            int aceCount = Hand.Count(card => card.Rank == "Ace");

            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }
            return score;
        }

    }
}
