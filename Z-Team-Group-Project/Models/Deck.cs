using System;
using System.Collections.Generic;
using System.Linq;

namespace Z_Team_Group_Project.Models
{
    public class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            random = new Random();
            cards = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    int score = rank == "Ace" ? 11 : (rank == "King" || rank == "Queen" || rank == "Jack" ? 10 : int.Parse(rank));
                    cards.Add(new Card { Suit = suit, Rank = rank, Score = score });
                }
            }
        }

        public void Shuffle()
        {
            cards = cards.OrderBy(card => random.Next()).ToList();
        }

        public Card DealCard()
        {
            var card = cards.First();
            cards.RemoveAt(0);
            return card;
        }
    }
}
