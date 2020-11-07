namespace BorderControl
{
    public interface IPerson : IBuyer
    {
        public string Name { get; }
        public int Age { get; }
    }
}
