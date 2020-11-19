using SOLID.Layouts;
using System;

namespace SOLID.Factories
{
    public class LayoutFactory
    {
        public static ILayout CreateLayout(string type)
        {
            switch (type)
            {
                case "SimpleLayout":
                    return new SimpleLayout();
                case "XmlLayout":
                    return new XmlLayout();
                case "JsonLayout":
                    return new JsonLayout();
                default:
                    throw new ArgumentException("Invalid Layout Type!");
            }
        }
    }
}
