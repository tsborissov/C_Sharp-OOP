using System;

namespace P01.Stream_Progress
{
    public class Program
    {
        public static void Main()
        {
            StreamProgressInfo fileProgressInfo = new StreamProgressInfo(new File("Name", 1000, 100));
            StreamProgressInfo musicFileProgressInfo = new StreamProgressInfo(new Music("Artist", "Album", 13000, 1200));

            Console.WriteLine(fileProgressInfo.CalculateCurrentPercent());
            Console.WriteLine(musicFileProgressInfo.CalculateCurrentPercent());
        }
    }
}
