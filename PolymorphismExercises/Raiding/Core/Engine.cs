using Raiding.Core.Interfaces;
using Raiding.Models;
using System;
using System.Collections.Generic;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly HeroFactory heroFactory;
        
        public Engine()
        {
            this.heroFactory = new HeroFactory();
        }

        public void Run()
        {
            int numberOfHeroes = int.Parse(Console.ReadLine());

            List<Hero> raidGroup = new List<Hero>();


            while (true)
            {
                if (raidGroup.Count == numberOfHeroes)
                {
                    break;
                }

                try
                {
                    raidGroup.Add(ProcessHeroInfo());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int bossPower = int.Parse(Console.ReadLine());

            int totalPower = 0;

            foreach (var hero in raidGroup)
            {
                totalPower += hero.Power;

                Console.WriteLine(hero.CastAbility());
            }

            if (totalPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }

        }

        private Hero ProcessHeroInfo()
        {
            string heroName = Console.ReadLine();
            string heroType = Console.ReadLine();

            Hero currentHero = this.heroFactory.CreateHero(heroType, heroName);

            return currentHero;
        }
    }
}
