using System;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class Program
    {
        static void Main()
        {
            var employee = new Employee("Ivan");
            var manager = new Manager("Pesho", new List<string>() { "doc1", "doc2", "doc3" });
            var developer = new Developer("Ivaylo", new List<string>() { "C#", "JS", "Python" });

            var employees = new List<Employee>();

            employees.Add(employee);
            employees.Add(manager);
            employees.Add(developer);

            var detailsPrinter = new DetailsPrinter(employees);

            detailsPrinter.PrintDetails();
        }
    }
}
