using System;
using System.Text;
using System.Collections.Generic;

using RobotService.Core.Contracts;
using RobotService.Models.Garages;
using RobotService.Models.Robots;
using RobotService.Models.Robots.Contracts;
using RobotService.Models.Garages.Contracts;
using RobotService.Utilities.Enums;
using RobotService.Utilities.Messages;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Procedures;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly IGarage garage;
        private readonly Dictionary<ProcedureType, IProcedure> procedures;

        public Controller()
        {
            this.garage = new Garage();
            this.procedures = new Dictionary<ProcedureType, IProcedure>();

            this.SeedProcedures();
        }


        public string Charge(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.Charge];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.ChargeProcedure, robotName);
        }

        public string Chip(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.Chip];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.ChipProcedure, robotName);
        }

        

        public string History(string procedureType)
        {
            Enum.TryParse(procedureType, out ProcedureType procedureEnum);

            return this.procedures[procedureEnum].History();
        }

        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {

            if (!Enum.TryParse(robotType, out RobotTypes robotTypeEnum))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidRobotType, robotType));
            }

            IRobot currentRobot = null;

            switch (robotTypeEnum)
            {
                case RobotTypes.HouseholdRobot:
                    currentRobot = new HouseholdRobot(name, energy, happiness, procedureTime);
                    break;
                case RobotTypes.WalkerRobot:
                    currentRobot = new WalkerRobot(name, energy, happiness, procedureTime);
                    break;
                case RobotTypes.PetRobot:
                    currentRobot = new PetRobot(name, energy, happiness, procedureTime);
                    break;
            }

            this.garage.Manufacture(currentRobot);

            return String.Format(OutputMessages.RobotManufactured, name);
        }

        public string Polish(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.Polish];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.PolishProcedure, robotName);
        }

        public string Rest(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.Rest];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.RestProcedure, robotName);
        }

        public string Sell(string robotName, string ownerName)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);

            this.garage.Sell(robotName, ownerName);

            if (currentRobot.IsChipped)
            {
                return string.Format(OutputMessages.SellChippedRobot, ownerName);
            }
            else
            {
                return string.Format(OutputMessages.SellNotChippedRobot, ownerName);
            }
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.TechCheck];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.TechCheckProcedure, robotName);
        }

        public string Work(string robotName, int procedureTime)
        {
            IRobot currentRobot = this.GetRobotByName(robotName);
            IProcedure currentProcedure = this.procedures[ProcedureType.Work];

            currentProcedure.DoService(currentRobot, procedureTime);

            return string.Format(OutputMessages.WorkProcedure, robotName, procedureTime);
        }
        
       
        private void SeedProcedures()
        {
            this.procedures.Add(ProcedureType.Charge, new Charge());
            this.procedures.Add(ProcedureType.Chip, new Chip());
            this.procedures.Add(ProcedureType.Polish, new Polish());
            this.procedures.Add(ProcedureType.Rest, new Rest());
            this.procedures.Add(ProcedureType.TechCheck, new TechCheck());
            this.procedures.Add(ProcedureType.Work, new Work());
        }

        private IRobot GetRobotByName(string robotName)
        {
            if (!this.garage.Robots.ContainsKey(robotName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }

            return this.garage.Robots[robotName];
        }
    }
}
