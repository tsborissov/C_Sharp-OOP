using System;

namespace FacadeDemo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                    .WithType("BMW")
                    .WithColor("Black")
                    .WithNumberOfDoors(5)
                .Built
                    .InCity("Leipzig")
                    .AtAddress("Address 254")
                .Build();

            Console.WriteLine(car);
        }
    }
}
