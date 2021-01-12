using System;
using System.Reflection;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Type studentType = typeof(ReflectionTest.Student);

            var student = (Student) Activator.CreateInstance(studentType, "Some Name", 30, "Some School");

            

            var fields = studentType.GetFields(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.NonPublic);

            var properties = studentType.GetProperties();

            var methods = studentType.GetMembers();
        }
    }
}
