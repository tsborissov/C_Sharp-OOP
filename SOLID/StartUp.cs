using SOLID.Core;

namespace SOLID
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine();

            engine.Run();
        }
    }
}
