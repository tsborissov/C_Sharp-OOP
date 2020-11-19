using System;

namespace SOLID.Core
{
    public class Engine
    {
        private readonly CommandInterpreter commandInterpreter;

                
        public Engine()
        {
            this.commandInterpreter = new CommandInterpreter();
        }

        public void Run()
        {

            int counter = int.Parse(Console.ReadLine());

            for (int i = 0; i < counter; i++)
            {
                string[] inputInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                this.commandInterpreter.Read(inputInfo);
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                string[] inputInfo = input.Split('|');

                this.commandInterpreter.Read(inputInfo);
            }

            this.commandInterpreter.PrintInfo();
        }
    }
}
