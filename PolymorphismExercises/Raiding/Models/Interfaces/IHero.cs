namespace Raiding.Models.Interfaces
{
    public interface IHero
    {
        public string Name { get; }
        public int Power { get; }

        public string CastAbility();

    }
}
