using MilitaryElite.enumerations;

namespace MilitaryElite.interfaces
{
    public interface IMission
    {
        public string CodeName { get; }
        public string MissionState { get; }

        public void CompleteMission(string missionName);
    }
}
