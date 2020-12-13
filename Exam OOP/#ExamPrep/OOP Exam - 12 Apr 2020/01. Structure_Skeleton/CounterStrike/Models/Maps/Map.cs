using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            var terrorists = players.Where(t => t.GetType() == typeof(Terrorist)).ToList();
            var counterTerrorists = players.Where(c => c.GetType() == typeof(CounterTerrorist)).ToList();

            while (terrorists.Any(x => x.IsAlive) && counterTerrorists.Any(x => x.IsAlive))
            {
                foreach (var terrorist in terrorists.Where(t => t.IsAlive))
                {
                    foreach (var counterTerrorist in counterTerrorists.Where(c => c.IsAlive))
                    {
                        counterTerrorist.TakeDamage(terrorist.Gun.Fire());
                    }
                }

                foreach (var counterTerrorist in counterTerrorists.Where(c => c.IsAlive))
                {
                    if (!counterTerrorist.IsAlive)
                    {
                        continue;
                    }
                    
                    foreach (var terrorist in terrorists.Where(t => t.IsAlive))
                    {
                        if (!terrorist.IsAlive)
                        {
                            continue;
                        }

                        terrorist.TakeDamage(counterTerrorist.Gun.Fire());
                    }
                }
            }

            if (terrorists.Any(x => x.IsAlive))
            {
                return "Terrorist wins!";
            }
            else
            {
                return "Counter Terrorist wins!";
            }
        }
    }
}
