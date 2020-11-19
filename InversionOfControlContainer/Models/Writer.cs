using InversionOfControlContainer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlContainer.Models
{
    public class Writer : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
