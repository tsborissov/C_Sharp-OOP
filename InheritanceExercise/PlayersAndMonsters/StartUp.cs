namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var soulMasterPesho = new SoulMaster("Pesho", 10);

            System.Console.WriteLine(soulMasterPesho.ToString());

        }
    }
}