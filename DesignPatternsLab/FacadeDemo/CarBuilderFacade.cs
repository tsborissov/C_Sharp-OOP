using System;
using System.Collections.Generic;
using System.Text;

namespace FacadeDemo
{
    public class CarBuilderFacade
    {
        public CarBuilderFacade()
        {
            Car = new Car();
        }

        public Car Car { get; set; }

        public Car Build() => Car;

        public CarInfoBuilder Info => new CarInfoBuilder(Car);
        public CarAddresBuilder Built => new CarAddresBuilder(Car);
    }
}
