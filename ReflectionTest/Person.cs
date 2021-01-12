using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionTest
{
    public abstract class Person
    {
        private string name;
        private int age;
        protected bool isCreated = false;
       
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.isCreated = true;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                this.name = value;
            }
        }

        public int Age
        {
            get => this.age;
            private set
            {
                this.age = value;
            }
        }

        public virtual void IncreaseAge()
        {
            this.Age++;
        }

        public virtual string ShowContactDetails()
        {
            return $"{this.Name} is {this.Age} years old.";
        }

    }
}
