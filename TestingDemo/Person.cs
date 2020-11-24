namespace TestingDemo
{
    public class Person
    {
        public Person(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string PersonName()
        {
            return "My name is " + this.Name;
        }
    }
}
