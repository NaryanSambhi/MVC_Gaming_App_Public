namespace Z_Team_Group_Project.Models
{
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int Score { get; set; }
        public string ImagePath => $"/Cards/{Rank}_of_{Suit.ToLower()}.png"; // Dynamic image path

    }
}
