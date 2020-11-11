using Raiding.Models;

namespace Raiding.Factories
{
    public class Rogue : Hero
    {
        public Rogue(string name, int power)
            : base(name, power)
        {
        }

        public override string CastAbility()
        {
            return $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
        }
    }
}
