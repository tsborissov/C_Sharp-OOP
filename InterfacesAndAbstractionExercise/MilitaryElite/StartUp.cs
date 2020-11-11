using MilitaryElite.enumerations;
using MilitaryElite.interfaces;
using MilitaryElite.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryElite
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ICollection<ISoldier> soldiers = new List<ISoldier>();
            ISoldier soldier = null;

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                string[] tokens = input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string soldierType = tokens[0];
                int id = int.Parse(tokens[1]);
                string firstName = tokens[2];
                string lastName = tokens[3];
                decimal salary;
                string corps = string.Empty;

                if (soldierType == typeof(Private).Name)
                {
                    salary = decimal.Parse(tokens[4]);

                    soldier = new Private(id, firstName, lastName, salary);
                }
                else if (soldierType == typeof(LieutenantGeneral).Name)
                {
                    salary = decimal.Parse(tokens[4]);

                    ICollection<ISoldier> leutanantPrivates = new List<ISoldier>();

                    for (int i = 5; i < tokens.Length; i++)
                    {
                        foreach (var currentSoldier in soldiers)
                        {
                            if (currentSoldier.Id == int.Parse(tokens[i]))
                            {
                                leutanantPrivates.Add(currentSoldier);
                            }
                        }
                    }

                    soldier = new LieutenantGeneral(id, firstName, lastName, salary, leutanantPrivates);
                }
                else if (soldierType == typeof(Engineer).Name)
                {
                    salary = decimal.Parse(tokens[4]);
                    corps = tokens[5];

                    if (Enum.TryParse(typeof(SoldierCorpEnum), corps, out _))
                    {
                        List<IRepair> repairs = new List<IRepair>();

                        for (int i = 6; i < tokens.Length; i += 2)
                        {
                            string partName = tokens[i];
                            int hoursWorked = int.Parse(tokens[i + 1]);

                            repairs.Add(new Repair(partName, hoursWorked));
                        }

                        soldier = new Engineer(id, firstName, lastName, salary, corps, repairs);
                    }
                }
                else if (soldierType == typeof(Commando).Name)
                {
                    salary = decimal.Parse(tokens[4]);
                    corps = tokens[5];

                    if (Enum.TryParse(typeof(SoldierCorpEnum), corps, out _))
                    {
                        List<IMission> missions = new List<IMission>();

                        for (int i = 6; i < tokens.Length; i += 2)
                        {
                            string missionCodeName = tokens[i];
                            string missionState = tokens[i + 1];

                            if (Enum.TryParse(typeof(MissionStateEnum), missionState, out _))
                            {
                                missions.Add(new Mission(missionCodeName, missionState));
                            }
                        }

                        soldier = new Commando(id, firstName, lastName, salary, corps, missions);
                    }

                }
                else if (soldierType == typeof(Spy).Name)
                {
                    int codeNumber = int.Parse(tokens[4]);

                    soldier = new Spy(id, firstName, lastName, codeNumber);
                }

                soldiers.Add(soldier);
            }

            PrintResult(soldiers);
        }

        private static void PrintResult(ICollection<ISoldier> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.ToString());
            }
        }
    }
}
