using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    public class Student : Person
    {

        public Student(string name, int age, string school)
            : base(name, age)
        {
            this.School = school;
        }


        public string School { get; set; }
    }
}
