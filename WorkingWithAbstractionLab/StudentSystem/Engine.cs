using System;

namespace StudentSystem
{
    public class Engine
    {
        private readonly StudentData studentData;
        private readonly IInputOutputProvider inputOutputProvider;

        public Engine(StudentData studentData, IInputOutputProvider inputOutputProvider)
        {
            this.studentData = studentData;
            this.inputOutputProvider = inputOutputProvider;
        }

        public void Process()
        {
            var end = false;
            
            while (!end)
            {
                var line = this.inputOutputProvider.GetInput();

                var command = Command.Parse(line);
                var commandName = command.Name;
                var arguments = command.Arguments;

                switch (commandName)
                {
                    case "Create":
                        var name = arguments[0];
                        var age = int.Parse(arguments[1]);
                        var grade = double.Parse(arguments[2]);
                        this.studentData.Create(name, age, grade);
                        break;

                    case "Show":
                        var studentName = arguments[0];
                        var details = this.studentData.GetDetails(studentName);
                        this.inputOutputProvider.ShowOutput(details);
                        break;

                    case "Exit":
                        end = true;
                        break;

                }
            }
        }
    }
}
