namespace Person
{
    public class Child : Person
    {
        private const int CHILD_MAX_VALUE = 15;
        public Child(string name, int age)
            : base(name, age)
        {

        }

        public override int Age
        {
            get
            {
                return base.Age;
            }
            protected set
            {
                if (value <= CHILD_MAX_VALUE)
                {
                    base.Age = value;
                }
            }

        }
    }
}
