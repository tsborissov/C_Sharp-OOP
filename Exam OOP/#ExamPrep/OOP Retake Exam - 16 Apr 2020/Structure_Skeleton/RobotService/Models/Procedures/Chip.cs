using System;
using RobotService.Utilities.Messages;
using RobotService.Models.Robots.Contracts;


namespace RobotService.Models.Procedures
{
    public class Chip : Procedure
    {
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);

            if (robot.IsChipped)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AlreadyChipped, robot.Name));
            }

            robot.ProcedureTime -= procedureTime;
            robot.IsChipped = true;
            robot.Happiness -= 5;
            this.Robots.Add(robot);
        }
    }
}
