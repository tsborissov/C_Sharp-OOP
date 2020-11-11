using System.Collections.Generic;

namespace MilitaryElite.interfaces
{
    public interface IEngineer
    {
        public ICollection<IRepair> Repairs { get; }
    }
}
