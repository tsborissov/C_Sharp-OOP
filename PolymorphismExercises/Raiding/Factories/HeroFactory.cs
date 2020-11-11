using Raiding.Common;
using Raiding.Factories;
using System;

namespace Raiding.Models
{
    public class HeroFactory
    {
        private const int DRUID_POWER = 80;
        private const int PALADIN_POWER = 100;
        private const int ROGUE_POWER = 80;
        private const int WARRIOR_POWER = 100;

        public HeroFactory()
        {

        }
        public Hero CreateHero(string type, string name)
        {
            Hero hero;
            
            if (type == "Druid")
            {
                hero = new Druid(name, DRUID_POWER);
            }
            else if (type == "Paladin")
            {
                hero = new Paladin(name, PALADIN_POWER);
            }
            else if (type == "Rogue")
            {
                hero = new Rogue(name, ROGUE_POWER);
            }
            else if (type == "Warrior")
            {
                hero = new Warrior(name, WARRIOR_POWER);
            }
            else
            {
                throw new InvalidOperationException (ExceptionMessages.InvalidHeroType);
            }

            return hero;
        }
    }
}
