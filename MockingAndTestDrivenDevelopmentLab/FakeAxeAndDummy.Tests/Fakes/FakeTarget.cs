using FakeAxeAndDummy.Contracts;

namespace FakeAxeAndDummy.Tests.Fakes
{
    public class FakeTarget : ITarget
    {
        public int Health => 10;

        public int GiveExperience()
        {
            return 25;
        }

        public bool IsDead()
        {
            return true;
        }

        public void TakeAttack(int attackPoints)
        {
            
        }
    }
}
