using CommandPattern.Core.Commands;
using CommandPattern.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace CommandPattern
{
    class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] inputTokens = args.Split().ToArray();

            string commandType = inputTokens[0];
            string[] commandArguments = inputTokens.Skip(1).ToArray();
            string result = string.Empty;
            //ICommand command = null;

            Type type = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name.StartsWith(commandType));

            if (type != null)
            {
                ICommand instance = (ICommand)Activator.CreateInstance(type);
                result = instance.Execute(commandArguments);
            }

            //if (commandType == "Hello")
            //{
            //    command = new HelloCommand();
            //}
            //else if (commandType == "Exit")
            //{
            //    command = new ExitCommand();
            //}

            return result;
        }
    }
}
