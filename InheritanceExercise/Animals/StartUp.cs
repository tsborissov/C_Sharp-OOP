using System;
using System.Linq;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Beast!")
                {
                    break;
                }

                string animalType = input;

                string[] tokens = Console.ReadLine().Split().ToArray();

                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string gender = tokens[2];

                try
                {
                    switch (animalType)
                    {
                        case "Dog":
                            Dog dog = new Dog(name, age, gender);
                            Console.WriteLine(dog);
                            break;

                        case "Cat":
                            Cat cat = new Cat(name, age, gender);
                            Console.WriteLine(cat);
                            break;

                        case "Frog":
                            Frog frog = new Frog(name, age, gender);
                            Console.WriteLine(frog);
                            break;

                        case "Kittens":
                            Kitten kitten = new Kitten(name, age);
                            Console.WriteLine(kitten);
                            break;

                        case "Tomcat":
                            Tomcat tomcat = new Tomcat(name, age);
                            Console.WriteLine(tomcat);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
