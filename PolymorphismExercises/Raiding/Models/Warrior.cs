using Raiding.Models;

namespace Raiding.Factories
{
    public class Warrior : Hero
    {
        public Warrior(string name, int power)
            : base(name, power)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
        }
    }
}
