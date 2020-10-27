using System.Collections.Generic;
using System.Text;

namespace StudentSystem
{
    public class StudentData
    {
        private Dictionary<string, Student> students { get; } = new Dictionary<string, Student>();

        
        public void Create(string name, int age, double grade)
        {
            if (!this.students.ContainsKey(name))
            {
                var student = new Student(name, age, grade);
                this.students[name] = student;
            }
        }

        public string GetDetails(string targetStudentName)
        {
            if (!this.students.ContainsKey(targetStudentName))
            {
                return null;
            }

            return this.students[targetStudentName].ToString();
        }

    }
}
