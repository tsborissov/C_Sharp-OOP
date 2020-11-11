using System.Collections;
using System.Collections.Generic;

namespace MilitaryElite.interfaces
{
    public interface ILieutenantGeneral
    {
        public ICollection<ISoldier> Privates { get; }
    }
}
