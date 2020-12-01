using System;

namespace SingletonDemo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var db1 = SingletonDataContainer.Instance;
            Console.WriteLine(db1.GetPopulation("Washington, D.C."));

            var db2 = SingletonDataContainer.Instance;
            Console.WriteLine(db2.GetPopulation("London"));

            var db3 = SingletonDataContainer.Instance;
            var db4 = SingletonDataContainer.Instance;
        }
    }
}
