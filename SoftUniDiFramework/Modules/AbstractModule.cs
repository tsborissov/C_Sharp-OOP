using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniDiFramework.Modules
{
    public abstract class AbstractModule
    {
        private IDictionary<Type, Dictionary<string, Type>> implementations;
        private IDictionary<Type, object> instances;

        protected AbstractModule()
        {
            this.implementations = new Dictionary<Type, Dictionary<string, Type>>();
            this.instances = new Dictionary<Type, object>();
        }
    }
}
