using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Enums;
using CounterStrike.Utilities.Messages;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Repositories.Contracts;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IGun> guns;
        private readonly IRepository<IPlayer> players;
        private readonly IMap map;


        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }
        

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun currentGun = null; 

            if (Enum.TryParse(type, out GunTypes gunTypeEnum))
            {
                switch (gunTypeEnum)
                {
                    case GunTypes.Pistol:
                        currentGun = new Pistol(name, bulletsCount);
                        break;
                    case GunTypes.Rifle:
                        currentGun = new Rifle(name, bulletsCount);
                        break;
                }
            }
            else
            {
                // TODO : Exception message finishes with ! or . ?
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            this.guns.Add(currentGun);

            return string.Format(OutputMessages.SuccessfullyAddedGun, currentGun.Name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun currentGun = this.guns.FindByName(gunName);

            if (currentGun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }
            
            IPlayer currentPlayer = null;

            if (Enum.TryParse(type, out PlayerTypes playerEnum))
            {
                switch(playerEnum)
                {
                    case PlayerTypes.Terrorist:
                        currentPlayer = new Terrorist(username, health, armor, currentGun);
                        break;
                    case PlayerTypes.CounterTerrorist:
                        currentPlayer = new CounterTerrorist(username, health, armor, currentGun);
                        break;
                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            this.players.Add(currentPlayer);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, currentPlayer.Username);
        }

        public string Report()
        {
            var playersOrdered = this.players.Models
                .OrderBy(t => t.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenBy(u => u.Username)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var player in playersOrdered)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartGame()
        {
            var allAlivePlayers = this.players.Models.Where(p => p.IsAlive).ToList();
            
            string result = this.map.Start(allAlivePlayers);

            return result;
        }
    }
}
