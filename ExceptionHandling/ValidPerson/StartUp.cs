using System;

namespace ValidPerson
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            try
            {
                //Person validPerson = new Person("Ivan", "Ivanov", 29);
                //Person invalidFirstNamePerson = new Person("", "Nikolov", 22);
                //Person invalidLastNamePerson = new Person("Pesho", "", 31);
                //Person invalidAgePerson = new Person("Dedo", "Milcho", 122);
                Student student = new Student("Gin4o", "gin4o@email.com");
            }
            catch (InvalidPersonNameException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Exception thrown: {0}", ex.Message);
            }
            
        }
    }
}
