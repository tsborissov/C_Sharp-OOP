using MilitaryElite.enumerations;
using MilitaryElite.interfaces;
using System;

namespace MilitaryElite.models
{
    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        public SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, string soldierCorp)
            : base(id, firstName, lastName, salary)
        {
            this.SoldierCorp = soldierCorp;
        }

        public string SoldierCorp { get; }

        public override string ToString()
        {
            return
                base.ToString() +
                Environment.NewLine +
                $"Corps: {this.SoldierCorp}";

        }
    }
}
