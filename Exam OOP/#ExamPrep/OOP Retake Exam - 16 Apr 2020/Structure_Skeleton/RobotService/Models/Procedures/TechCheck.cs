using RobotService.Models.Robots.Contracts;

namespace RobotService.Models.Procedures
{
    class TechCheck : Procedure
    {
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);

            robot.Energy -= 8;

            if (robot.IsChecked)
            {
                robot.Energy -= 8;
            }
            else
            {
                robot.IsChecked = true;
            }

            robot.ProcedureTime -= procedureTime;
            this.Robots.Add(robot);
        }
    }
}
