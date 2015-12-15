using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfRaces.Exceptions;
using WorldOfRaces.Interfaces;
using WorldOfRaces.Items;

namespace WorldOfRaces.Characters
{
    // heal balancing added
    public class Hero : Character, IHero
    {
        private readonly List<Item> inventory;

        public Hero(Position position, char objectSymbol, string name, HeroRace race)
            : base(position, objectSymbol, name, 0, 0)
        {
            this.Race = race;
            this.inventory = new List<Item>();
            this.SetPlayerStats();
        }

        public HeroRace Race { get; private set; }

        public IEnumerable<Item> Inventory
        {
            get
            {
                return this.inventory;
            }
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    this.Position = new Position(this.Position.X, this.Position.Y - 1);
                    break;
                case "down":
                    this.Position = new Position(this.Position.X, this.Position.Y + 1);
                    break;
                case "right":
                    this.Position = new Position(this.Position.X + 1, this.Position.Y);
                    break;
                case "left":
                    this.Position = new Position(this.Position.X - 1, this.Position.Y);
                    break;
                default:
                    throw new ArgumentException("Invalid direction.", "Direction");
            }
        }

        public void AddItemToInventory(Item item)
        {
            this.inventory.Add(item);
        }

        public void Heal()
        {
            var elexir = this.inventory.FirstOrDefault() as LifeElexir;

            if (elexir == null)
            {
                throw new NotEnoughElexirsException("You can't heal yourself, you need elexir!");
            }


            this.Health += elexir.HealthRestore;
            this.inventory.Remove(elexir);

            if (this.Race == HeroRace.Archer)
            {
                if (this.Health >= 600)
                {
                    this.Health = 600;
                }
            }
            if (this.Race == HeroRace.CrossBowMan)
            {
                if (this.Health >= 700)
                {
                    this.Health = 700;
                }
            }
            if (this.Race == HeroRace.Warrior)
            {
                if (this.Health >= 800)
                {
                    this.Health = 800;
                }
            }
            if (this.Race == HeroRace.Tank)
            {
                if (this.Health >= 1500)
                {
                    this.Health = 1500;
                }
            }
            if (this.Race == HeroRace.HeavyTank)
            {
                if (this.Health >= 2000)
                {
                    this.Health = 2000;
                }
            }
            if (this.Race == HeroRace.Knight)
            {
                if (this.Health >= 1000)
                {
                    this.Health = 1000;
                }
            }

        }



        public override string ToString()
        {
            return string.Format(
                "You are a brave {1}{4}Your name is: {0}{4}Your HealthPoints are: {3}{4}Your damage is: {2}",
                this.Name,
                this.Race,
                this.Damage,
                this.Health,
                Environment.NewLine);
        }



        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case HeroRace.Archer:
                    this.Damage = 450;
                    this.Health = 600;
                    break;
                case HeroRace.CrossBowMan:
                    this.Damage = 400;
                    this.Health = 700;
                    break;
                case HeroRace.Warrior:
                    this.Damage = 300;
                    this.Health = 800;
                    break;
                case HeroRace.Tank:
                    this.Damage = 150;
                    this.Health = 1500;
                    break;
                case HeroRace.HeavyTank:
                    this.Damage = 100;
                    this.Health = 2000;
                    break;
                case HeroRace.Knight:
                    this.Damage = 200;
                    this.Health = 1000;
                    break;
                default:
                    throw new ArgumentException("Unknown hero race.");
            }
        }

    }
}