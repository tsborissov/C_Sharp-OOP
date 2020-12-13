namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        public Rifle(string name, int bulletsCount) 
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int firedBullets = 10;

            if (this.BulletsCount < firedBullets)
            {
                return 0;
            }

            this.BulletsCount -= firedBullets;

            return firedBullets;
        }
    }
}
