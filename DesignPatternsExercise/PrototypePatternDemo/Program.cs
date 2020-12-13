using System;

namespace PrototypePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SandwichMenu sandwichMenu = new SandwichMenu();

            sandwichMenu["BLT"] = new Sandwich("Wheat", "Beacon", "", "Lettuce, Tomato");
            sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly");
            sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");
            
            sandwichMenu["LoadedBLT"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");
            sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");
            sandwichMenu["Vegetarian"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

            Sandwich sandwich1 = sandwichMenu["BLT"].Clone() as Sandwich;
            Sandwich sandwich2 = sandwichMenu["ThreeMeatCombo"].Clone() as Sandwich;
            Sandwich sandwich3 = sandwichMenu["Vegetarian"].Clone() as Sandwich;
        }
    }
}
