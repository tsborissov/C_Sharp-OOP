using System;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());
            var school = Console.ReadLine();


            var student = new Student(name, age, school);

            Console.WriteLine($"Name: {student.Name} - Age: {student.Age} - School: {student.School}");

        }
    }
}
