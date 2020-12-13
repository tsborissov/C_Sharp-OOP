﻿using System.Linq;
using System.Collections.Generic;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly List<IDriver> drivers;

        public DriverRepository()
        {
            this.drivers = new List<IDriver>();
        }
        
        public void Add(IDriver model)
        {
            this.drivers.Add(model);
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
            return this.drivers.ToList();
        }

        public IDriver GetByName(string name)
        {
            return this.drivers.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IDriver model)
        {
            return this.drivers.Remove(model);
        }
    }
}
