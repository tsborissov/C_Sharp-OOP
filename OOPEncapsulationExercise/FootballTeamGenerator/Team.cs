using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class Team
    {
        private string teamName;
        private List<Player> players;

        public Team(string name)
        {
            this.TeamName = name;

            this.players = new List<Player>();
        }


        public string TeamName 
        {
            get => this.teamName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.teamName = value;
            }
        }
        public int Rating => CalculateRating();
        

        private int CalculateRating()
        {
            if (this.players.Count == 0)
            {
                return 0;
            }
            else
            {
                return (int)Math.Round(this.players.Sum(x => x.SkillLevel) / this.players.Count);
            }
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            Player targetPlayer = players.FirstOrDefault(x => x.Name == playerName);
            
            if(targetPlayer == null)
            {
                throw new Exception($"Player {playerName} is not in {this.TeamName} team.");
            }

            this.players.Remove(targetPlayer);
        }

    }
}
