﻿using System.Linq;
using System.Reflection;
using System.Text;

namespace AuthorProblem
{
    public class Tracker
    {

        public string PrintMethodsByAuthor()
        {
            var classType = typeof(StartUp);
            var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            var sb = new StringBuilder();

            foreach (var method in methods)
            {
                if (method.CustomAttributes.Any(n => n.AttributeType == typeof(AuthorAttribute)))
                {
                    var attributes = method.GetCustomAttributes(false);

                    foreach (AuthorAttribute attr in attributes)
                    {
                        sb.AppendLine($"{method.Name} is written by {attr.Name}");
                    }
                }
            }

            return sb.ToString().Trim();
        }

    }
}
