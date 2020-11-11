using MilitaryElite.enumerations;
using MilitaryElite.interfaces;
using System;

namespace MilitaryElite.models
{
    public class Mission : IMission
    {
        public Mission(string codeName, string missionState)
        {
            this.CodeName = codeName;
            this.MissionState = missionState;
        }

        public string CodeName { get; }

        public string MissionState { get; private set; }

        public void CompleteMission(string missionName)
        {
            this.MissionState = "Finished";
        }
    }
}
