using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        private readonly Random random = new Random();

        public string RandomString()
        {
            var index = random.Next(0, this.Capacity - 1);

            var element = this[index];

            this.RemoveAt(index);

            return element;
        }
    }
}
