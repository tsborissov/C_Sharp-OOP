using MilitaryElite.interfaces;
using System;

namespace MilitaryElite.models
{
    public class Spy : Soldier, ISpy
    {
        public Spy(int id, string firstName, string lastName, int spyCodeNumber)
            : base(id, firstName, lastName)
        {
            this.SpyCodeNumber = spyCodeNumber;
        }

        public int SpyCodeNumber { get; }

        public override string ToString()
        {
            return
                base.ToString() +
                Environment.NewLine +
                $"Code Number: {this.SpyCodeNumber}";
        }
    }
}
