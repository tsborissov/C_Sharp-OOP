using System;

namespace FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public string Name 
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.name = value;
            }
        }

        public int Endurance 
        {
            get => this.endurance;
            private set
            {
                ValidateStat(value, nameof(this.Endurance));

                this.endurance = value;
            }
        }

        public int Sprint 
        { 
            get => this.sprint;
            private set
            {
                ValidateStat(value, nameof(this.Sprint));

                this.sprint = value;
            }
        }

        public int Dribble 
        {
            get => this.dribble;
            private set
            {
                ValidateStat(value, nameof(this.Dribble));

                this.dribble = value;
            }
        }

        public int Passing 
        {
            get => this.passing;
            private set
            {
                ValidateStat(value, nameof(this.Passing));

                this.passing = value;
            }
        }

        public int Shooting 
        {
            get => this.shooting;
            private set
            {
                ValidateStat(value, nameof(this.Shooting));

                this.shooting = value;
            }
        }

        public double SkillLevel 
        {
            get => (this.endurance + this.sprint + this.dribble + this.passing + this.shooting) * 1.0 / 5;
        }

        private void ValidateStat(int statValue, string statName)
        {
            if (statValue < 0 || statValue > 100)
            {
                throw new ArgumentException($"{statName} should be between 0 and 100.");
            }

            return;
        }
    }
}
