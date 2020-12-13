using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using OnlineShop.Common.Constants;
using OnlineShop.Common.Enums;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly ICollection<IComputer> computers;
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComputer targetComputer = ValidateComputer(computerId);

            if (this.components.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComponent currentComponent = null;

            if (Enum.TryParse(componentType, out ComponentType componentTypeEnum))
            {
                switch (componentTypeEnum)
                {
                    case ComponentType.CentralProcessingUnit:
                        currentComponent = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                    case ComponentType.Motherboard:
                        currentComponent = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                    case ComponentType.PowerSupply:
                        currentComponent = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                    case ComponentType.RandomAccessMemory:
                        currentComponent = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                    case ComponentType.SolidStateDrive:
                        currentComponent = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                    case ComponentType.VideoCard:
                        currentComponent = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
                        break;
                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            targetComputer.AddComponent(currentComponent);
            this.components.Add(currentComponent);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (this.computers.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }
            
            IComputer currentComputer = null;

            if (Enum.TryParse(computerType, out ComputerType computerTypeEnum))
            {
                switch (computerTypeEnum)
                {
                    case ComputerType.DesktopComputer:
                        currentComputer = new DesktopComputer(id, manufacturer, model, price);
                        break;
                    case ComputerType.Laptop:
                        currentComputer = new Laptop(id, manufacturer, model, price);
                        break;
                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            this.computers.Add(currentComputer);

            return string.Format(SuccessMessages.AddedComputer, currentComputer.Id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IComputer targetComputer = ValidateComputer(computerId);

            if (this.peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            IPeripheral currentPerypheral = null;

            if (Enum.TryParse(peripheralType, out PeripheralType perypheralTypeEnum))
            {
                switch (perypheralTypeEnum)
                {
                    case PeripheralType.Headset:
                        currentPerypheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                        break;
                    case PeripheralType.Keyboard:
                        currentPerypheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                        break;
                    case PeripheralType.Monitor:
                        currentPerypheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                        break;
                    case PeripheralType.Mouse:
                        currentPerypheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                        break;
                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            targetComputer.AddPeripheral(currentPerypheral);
            this.peripherals.Add(currentPerypheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            double maxOverallPerformance = this.computers.Max(o => o.OverallPerformance);
            IComputer targetComputer = this.computers.FirstOrDefault(x => x.Price <= budget && x.OverallPerformance == maxOverallPerformance);

            this.computers.Remove(targetComputer);

            return targetComputer.ToString();
        }

        public string BuyComputer(int id)
        {
            IComputer targetComputer = ValidateComputer(id);

            this.computers.Remove(targetComputer);

            return targetComputer.ToString();
        }

        public string GetComputerData(int id)
        {
            IComputer targetComputer = ValidateComputer(id);

            return targetComputer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer targetComputer = ValidateComputer(computerId);

            IComponent targetComponent = targetComputer.RemoveComponent(componentType);
            this.components.Remove(targetComponent);

            return string.Format(SuccessMessages.RemovedComponent, componentType, targetComponent.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer targetComputer = ValidateComputer(computerId);

            IPeripheral targetPerypheral = targetComputer.RemovePeripheral(peripheralType);
            this.peripherals.Remove(targetPerypheral);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, targetPerypheral.Id);
        }

        private IComputer ValidateComputer(int computerId)
        {
            IComputer targetComputer = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (targetComputer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return targetComputer;
        }
    }
}
