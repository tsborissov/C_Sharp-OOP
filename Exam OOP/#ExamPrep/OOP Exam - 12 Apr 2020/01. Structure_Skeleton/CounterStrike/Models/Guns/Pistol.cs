namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        public Pistol(string name, int bulletsCount) 
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int firedBullets = 1;

            if (this.BulletsCount < firedBullets)
            {
                return 0;
            }

            this.BulletsCount -= firedBullets;

            return firedBullets;
        }
    }
}
