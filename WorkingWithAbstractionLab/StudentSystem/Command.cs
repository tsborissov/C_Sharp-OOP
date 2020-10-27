using System.Linq;

namespace StudentSystem
{
    public class Command
    {
        public Command(string name, string[] arguments)
        {
            this.Name = name;
            this.Arguments = arguments;
        }

        public string Name { get; set; }
        public string[] Arguments { get; set; }

        public static Command Parse(string text)
        {
            var parts = text.Split();

            if (parts.Length < 1)
            {
                return null;
            }

            return new Command
            (
                parts[0],
                parts.Skip(1).ToArray()

            );
        }
    }
}
