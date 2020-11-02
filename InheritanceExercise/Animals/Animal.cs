using System;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        private int age;

        public Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name { get; set; }
        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Invalid input!");
                }

                this.age = value;
            }
        }
        public string Gender { get; set; }

        public virtual string ProduceSound()
        {
            return null;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(this.GetType().ToString());
            result.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            result.AppendLine(ProduceSound());
            
            return result.ToString().TrimEnd();
        }
    }
}
