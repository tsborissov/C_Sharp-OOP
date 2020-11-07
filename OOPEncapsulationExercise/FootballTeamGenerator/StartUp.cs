using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{

    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teamsCollection = new List<Team>();

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (input == "END")
                    {
                        break;
                    }

                    string[] tokens = input.Split(';');

                    string command = tokens[0];
                    string teamName = tokens[1];

                    Team targetTeam = teamsCollection.FirstOrDefault(x => x.TeamName == teamName);


                    if (command == "Team")
                    {
                        if (targetTeam == null)
                        {
                            teamsCollection.Add(new Team(teamName));
                        }
                    }
                    else if (command == "Add")
                    {
                        ValidateTeam(targetTeam, teamName);

                        string playerNameToAdd = tokens[2];
                        int endurance = int.Parse(tokens[3]);
                        int sprint = int.Parse(tokens[4]);
                        int dribble = int.Parse(tokens[5]);
                        int passing = int.Parse(tokens[6]);
                        int shooting = int.Parse(tokens[7]);

                        Player playerToAdd = new Player(playerNameToAdd, endurance, sprint, dribble, passing, shooting);

                        targetTeam.AddPlayer(playerToAdd);
                    }
                    else if (command == "Remove")
                    {
                        ValidateTeam(targetTeam, teamName);

                        string playerNameToRemove = tokens[2];

                        targetTeam.RemovePlayer(playerNameToRemove);
                    }
                    else if (command == "Rating")
                    {
                        ValidateTeam(targetTeam, teamName);

                        Console.WriteLine($"{teamName} - {targetTeam.Rating}");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message); ;
                }
            }
        }

        private static void ValidateTeam(Team targetTeam, string teamName)
        {
            if (targetTeam == null)
            {
                throw new Exception($"Team {teamName} does not exist.");
            }
        }
    }
}
