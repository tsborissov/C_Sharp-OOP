using MilitaryElite.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, ICollection<ISoldier> privates)
            : base(id, firstName, lastName, salary)
        {
            this.Privates = privates;
        }

        public ICollection<ISoldier> Privates { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.Privates)
            {
                sb.Append("  ");
                sb.AppendLine(item.ToString());
            }

            return base.ToString() +
                Environment.NewLine +
                "Privates:" +
                Environment.NewLine +
                sb.ToString();
        }
    }
}
