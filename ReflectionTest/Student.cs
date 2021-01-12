using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionTest
{
    public class Student : Person
    {
        public Student(string name, int age, string schoolName) 
            : base(name, age)
        {
            this.SchoolName = schoolName;
        }

        public string SchoolName { get; private set; }

        public override string ShowContactDetails()
        {
            return base.ShowContactDetails() + $" Student's scool is {this.SchoolName}";
        }

        private bool Checkcreation()
        {
            return this.isCreated;
        }
    }
}
