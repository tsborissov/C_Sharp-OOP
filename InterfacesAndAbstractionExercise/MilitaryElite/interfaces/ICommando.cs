using System.Collections.Generic;

namespace MilitaryElite.interfaces
{
    interface ICommando
    {
        public ICollection<IMission> Missions { get; }
    }
}
