using MilitaryElite.enumerations;
using MilitaryElite.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(int id, string firstName, string lastName, decimal salary, string soldierCorp, ICollection<IMission> missions)
            : base(id, firstName, lastName, salary, soldierCorp)
        {
            this.Missions = missions;
        }

        public ICollection<IMission> Missions { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.Missions)
            {
                sb.AppendLine($"  Code Name: {item.CodeName} State: {item.MissionState}");
            }

            return
                (base.ToString() +
                Environment.NewLine +
                "Missions:" +
                Environment.NewLine +
                sb.ToString()).TrimEnd();
        }
    }
}
