namespace P03_FootballBetting.Data.Models
{
    using Enumerations;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        public int BetId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        public Prediction Prediction { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
