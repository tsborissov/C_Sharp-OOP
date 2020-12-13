using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components { get => this.components.AsReadOnly(); }

        public IReadOnlyCollection<IPeripheral> Peripherals { get => this.peripherals.AsReadOnly(); }

        public void AddComponent(IComponent component)
        {
            if (this.Components.Any(t => t.GetType() == component.GetType()))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }

            this.components.Add(component);
        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.Peripherals.Any(t => t.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }

            this.peripherals.Add(peripheral);
        }
        public IComponent RemoveComponent(string componentType)
        {
            IComponent targetComponent = this.Components.FirstOrDefault(t => t.GetType().Name == componentType);

            if (targetComponent == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            this.components.Remove(targetComponent);

            return targetComponent;
        }
        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral targetPeripheral = this.Peripherals.FirstOrDefault(t => t.GetType().Name == peripheralType);

            if (targetPeripheral == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            this.peripherals.Remove(targetPeripheral);

            return targetPeripheral;
        }

        public override decimal Price
        {
            get
            {
                decimal componentsPrice = 0;
                decimal perypheralsPrice = 0;

                if (this.Components.Any())
                {
                    componentsPrice = this.Components.Sum(c => c.Price);
                }

                if (this.Peripherals.Any())
                {
                    perypheralsPrice = this.Peripherals.Sum(p => p.Price);
                }
                
                return base.Price + componentsPrice + perypheralsPrice;
            }

            set
            {
                base.Price = value;
            }
        }

        public override double OverallPerformance
        {
            get
            {
                double componentsAverageOverallPerformance = 0;

                if (this.Components.Any())
                {
                    componentsAverageOverallPerformance = this.Components.Average(o => o.OverallPerformance);
                }

                return base.OverallPerformance + componentsAverageOverallPerformance;
            }
            set
            {
                base.OverallPerformance = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());

            if (this.Components.Count == 0)
            {
                sb.AppendLine($" Components (0):");
            }
            else
            {
                sb.AppendLine($" Components ({this.Components.Count}):");

                foreach (var component in this.Components)
                {
                    sb.AppendLine($"  {component.ToString()}");
                }
            }

            if (this.peripherals.Count == 0)
            {
                sb.AppendLine($" Peripherals (0); Average Overall Performance (0.00):");
            }
            else
            {
                sb.AppendLine($" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({this.Peripherals.Average(x => x.OverallPerformance):F2}):");

                foreach (var peripheral in this.Peripherals)
                {
                    sb.AppendLine($"  {peripheral.ToString()}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
