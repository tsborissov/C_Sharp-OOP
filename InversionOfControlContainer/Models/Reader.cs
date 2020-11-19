using InversionOfControlContainer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlContainer.Models
{
    public class Reader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
