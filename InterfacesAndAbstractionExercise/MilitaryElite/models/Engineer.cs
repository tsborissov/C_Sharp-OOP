using MilitaryElite.enumerations;
using MilitaryElite.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.models
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        public Engineer(int id, string firstName, string lastName, decimal salary, string soldierCorp, ICollection<IRepair> repairs)
            : base(id, firstName, lastName, salary, soldierCorp)
        {
            this.Repairs = repairs;
        }

        public ICollection<IRepair> Repairs { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.Repairs)
            {
                sb.AppendLine($"  Part Name: {item.PartName} Hours Worked: {item.HoursWorked}");
            }

            return
                (base.ToString() +
                Environment.NewLine +
                "Repairs:" +
                Environment.NewLine +
                sb.ToString()).TrimEnd();
        }
    }
}
