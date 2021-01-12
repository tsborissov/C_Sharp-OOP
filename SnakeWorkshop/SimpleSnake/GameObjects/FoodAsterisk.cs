namespace SimpleSnake.GameObjects
{
    class FoodAsterisk : Food
    {
        private const char foodSymbol = '*';
        private const int points = 1;

        public FoodAsterisk(Wall wall)
            : base(wall, foodSymbol, points)
        {
        }
    }
}
