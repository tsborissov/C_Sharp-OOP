using System.Text;

namespace Cars
{
    public class Seat : ICar
    {

        public Seat(string model, string Color)
        {
            this.Model = model;
            this.Color = Color;
        }
        public string Model { get; private set; }

        public string Color { get; private set; }

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.Color} {nameof(Seat)} {this.Model}");
            sb.AppendLine($"{this.Start()}");
            sb.AppendLine($"{this.Stop()}");
            
            return sb.ToString().TrimEnd();
        }
    }
}
