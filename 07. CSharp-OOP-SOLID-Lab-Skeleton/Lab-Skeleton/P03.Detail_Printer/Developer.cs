using System;
using System.Collections.Generic;
using System.Text;

namespace P03.DetailPrinter
{
    public class Developer : Employee
    {
        public Developer(string name, ICollection<string> technologies) 
            : base(name)
        {
            this.Technologies = new List<string>(technologies);
        }

        public IReadOnlyCollection<string> Technologies { get; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + string.Join(Environment.NewLine, this.Technologies);
        }
    }
}
