using System.Text.RegularExpressions;

namespace ValidPerson
{
    public class Student
    {
        private string name;
        private string email;
        private string pattern;
        private Regex regex;


        public Student(string name, string email)
        {
            this.pattern = @"[\d\W]";
            this.regex = new Regex(pattern);

            this.Name = name;
            this.Email = email;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set 
            {
                if (this.regex.IsMatch(value))
                {
                    throw new InvalidPersonNameException($"{value} is not a valid name.");
                }

                this.name = value; 
            }
        }


        public  string Email
        {
            get 
            {
                return this.email;
            }
            set 
            {
                this.email = value; 
            }
        }


    }
}
