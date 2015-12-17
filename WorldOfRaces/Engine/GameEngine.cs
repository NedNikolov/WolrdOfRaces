using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorldOfRaces;
using WorldOfRaces.Attributes;
using WorldOfRaces.Characters;
using WorldOfRaces.Exceptions;
using WorldOfRaces.Interfaces;
using WorldOfRaces.Items;

namespace WorldOfRaces.Engine
{
    public class GameEngine
    {
        public const int MapWidth = 5;
        public const int MapHeight = 5;

        private const int InitialNumberOfEnemies = 8;
        private const int InitialNumberOfElexirs = 5;

        private static readonly Random Rand = new Random();

        private readonly IInputReader reader;
        private readonly IRenderer renderer;

        private readonly string[] characterNames =
        {
            "Weiss",
            "Semias",
            "Blade",
            "Karver",
            "Glacier",
            "Kirnon",
            "Gastly",
            "Kaiser",
            "Butler",
            "Smoky"
        };

        private readonly IList<GameObject> characters;
        private readonly IList<GameObject> items;
        private IHero hero;

        public GameEngine(IInputReader reader, IRenderer renderer)
        {
            this.reader = reader;
            this.renderer = renderer;
            this.characters = new List<GameObject>();
            this.items = new List<GameObject>();
        }

        public bool IsRunning { get; private set; }

        public void Run()
        {
            this.IsRunning = true;

            var heroLand = GetHeroLand();
            var heroName = this.GetHeroName();
            HeroRace race = this.GetHeroRace();

            this.hero = new Hero(new Position(0, 0), 'H', heroName, race);

            this.PopulateEnemies();
            this.PopulateItems();

            while (this.IsRunning)
            {
                string command = this.reader.ReadLine();

                try
                {
                    this.ExecuteCommand(command);
                }
                catch (ObjectOutsideOfMapException ex)
                {
                    this.renderer.WriteLine(ex.Message);
                }
                catch (NotEnoughElexirsException ex)
                {
                    this.renderer.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    this.renderer.WriteLine(ex.Message);
                }

                if (this.characters.Count == 0)
                {
                    this.IsRunning = false;
                    this.renderer.WriteLine("Finally! The victory is in your hand and the evil is gone!");
                }
            }
        }

        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "help":
                    this.ExecuteHelpCommand();
                    break;
                case "map":
                    this.PrintMap();
                    break;
                case "left":
                case "right":
                case "up":
                case "down":
                    this.HeroMovement(command);
                    break;
                case "status":
                    this.ShowStatus();
                    break;
                case "inventory":
                    this.ShowInventory();
                    break;
                case "enemies":
                    this.renderer.WriteLine(
                "Number of enemies left: {0}",
                this.characters.Count); break;
                case "heal":
                    this.hero.Heal();
                    this.renderer.WriteLine("Healed!");
                    break;
                case "clear":
                    this.renderer.Clear();
                    break;
                case "enemiesInfo":
                    this.renderer.WriteLine("D - Dwarf Health: 200, Damage 200{0}M - Gnome Health: 260, Damage 120{0}G - Goblin Health: 250, Damage 100{0}O - Orc Health: 800, Damage 300{0}N - NightElf Health: 110, Damage 90{0}T - Tauren Health: 900, Damage 250{0}R - Troll Health: 270, Damage 115{0}U - Undead Health: 600, Damage 310{0}W - Worden Health: 700, Damage 140{0}", Environment.NewLine);
                    break;
                case "ElexirInfo":
                    this.renderer.WriteLine("LifeElexir has 2 values: Small = 100, Medium = 250, Large = 500");
                    break;
                case "retreat":
                    this.IsRunning = false;
                    this.renderer.WriteLine("Run, run, ruun!");
                    break;
                default:
                    throw new ArgumentException("Unknown command", "command");
            }
        }

        private void ShowStatus()
        {
            this.renderer.WriteLine(this.hero.ToString());
        }

        private void ShowInventory()
        {
            this.renderer.WriteLine("Number of elexirs: {0}", this.hero.Inventory.Count());
        }

        private void HeroMovement(string command)
        {
            this.hero.Move(command);

            ICharacter enemy =
                this.characters.Cast<ICharacter>()
                .FirstOrDefault(
                    e => e.Position.X == this.hero.Position.X
                        && e.Position.Y == this.hero.Position.Y
                        && e.Health > 0);

            if (enemy != null)
            {
                this.BeginBattle(enemy);
                return;
            }

            Item elexir =
                this.items.Cast<Item>()
                .FirstOrDefault(
                    e => e.Position.X == this.hero.Position.X
                        && e.Position.Y == this.hero.Position.Y
                        && e.ItemState == ItemState.Available);

            if (elexir != null)
            {
                this.hero.AddItemToInventory(elexir);
                elexir.ItemState = ItemState.Collected;
                this.renderer.WriteLine("Elexir collected!");
            }
        }

        private void BeginBattle(ICharacter enemy)
        {
            this.renderer.WriteLine("Enemy current health {0} damage {1}", enemy.Health, enemy.Damage);
            this.renderer.WriteLine("Your current health {0} and damage {1}", this.hero.Health, this.hero.Damage);
            this.hero.Attack(enemy);

            if (enemy.Health <= 0)
            {
                this.characters.Remove(enemy as GameObject);
                this.renderer.WriteLine("You attacked and slain him!");
                this.renderer.WriteLine("Your health after the battle {0} and damage {1}", this.hero.Health, this.hero.Damage);
                return;
            }
            if (enemy.Health > 0)
            {
                this.renderer.WriteLine("You attacked him, but he was strong and he decided to strike back!");
                this.renderer.WriteLine("Enemy health after your attack {0} damage {1}", enemy.Health, enemy.Damage);
                enemy.Attack(this.hero);
                if (this.hero.Health > 0)
                {
                    this.renderer.WriteLine("Your health after enemy attack {0} damage {1}", this.hero.Health, this.hero.Damage);
                }
                while (enemy.Health > 0 || this.hero.Health > 0)
                {
                    this.hero.Attack(enemy);
                    this.renderer.WriteLine("You attacked him again stronger!");
                    if (enemy.Health <= 0)
                    {
                        break;
                    }
                    this.renderer.WriteLine("Enemy health after your attack {0} damage {1}", enemy.Health, enemy.Damage);
                    enemy.Attack(this.hero);
                    this.renderer.WriteLine("He strike back!");
                    if (this.hero.Health <= 0)
                    {
                        break;
                    }
                    this.renderer.WriteLine("Your health after enemy attack {0} damage {1}", this.hero.Health, this.hero.Damage);
                }
            }
            if (this.hero.Health <= 0)
            {
                this.IsRunning = false;
                this.renderer.WriteLine("You are dead!");

            }
            if (this.hero.Health > 0)
            {
                this.characters.Remove(enemy as GameObject);
                this.renderer.WriteLine("You has slain an enemy!");
                this.renderer.WriteLine("Your health after the battle {0} and damage {1}", this.hero.Health, this.hero.Damage);
            }
        }

        private void PrintMap()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < MapHeight; row++)
            {
                for (int col = 0; col < MapWidth; col++)
                {
                    if (this.hero.Position.X == col && this.hero.Position.Y == row)
                    {
                        sb.Append('H');
                        continue;
                    }

                    var character =
                         this.characters
                         .Cast<ICharacter>()
                         .FirstOrDefault(c => c.Position.X == col
                             && c.Position.Y == row
                             && c.Health > 0);

                    var item = this.items
                        .Cast<Item>()
                        .FirstOrDefault(c => c.Position.X == col
                            && c.Position.Y == row
                            && c.ItemState == ItemState.Available);

                    if (character == null && item == null)
                    {
                        sb.Append('.');
                    }
                    else if (character != null)
                    {
                        var ch = character as GameObject;
                        sb.Append(ch.ObjectSymbol);
                    }
                    else
                    {
                        sb.Append(item.ObjectSymbol);
                    }
                }

                sb.AppendLine();
            }

            this.renderer.WriteLine(sb.ToString());
        }

        private void ExecuteHelpCommand()
        {
            string helpInfo = File.ReadAllText("../../HelpInfo.txt");

            this.renderer.WriteLine(helpInfo);
        }

        private HeroRace GetHeroRace()
        {
            this.renderer.WriteLine("Choose wisely:");
            this.renderer.WriteLine("1. Archer (damage: 450, health: 600)");
            this.renderer.WriteLine("2. CrossBowMan (damage: 400, health: 700)");
            this.renderer.WriteLine("3. Warrior (damage: 300, health: 800)");
            this.renderer.WriteLine("4. Tank (damage: 150, health: 1500)");
            this.renderer.WriteLine("5. HeavyTank (damage: 100, health: 2000)");
            this.renderer.WriteLine("6. Knight (damage: 200, health: 1000)");

            string choice = this.reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };

            while (!validChoises.Contains(choice))
            {
                this.renderer.WriteLine("Invalid choice of race, please re-enter.");
                choice = this.reader.ReadLine();
            }

            HeroRace race = (HeroRace)int.Parse(choice);

            return race;
        }

        private string GetHeroLand()
        {

            this.renderer.WriteLine("Choose your land:");
            this.renderer.WriteLine("1. Quxmus");
            this.renderer.WriteLine("2. Reliquary");
            this.renderer.WriteLine("3. Braeqakata");
            this.renderer.WriteLine("4. Amaranthine");

            string choice = this.reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", };

            while (!validChoises.Contains(choice))
            {
                this.renderer.WriteLine("Invalid choice of land, please re-enter.");
                choice = this.reader.ReadLine();
            }

            HeroLand race = (HeroLand)int.Parse(choice);

            return race.ToString();

        }

        private string GetHeroName()
        {
            this.renderer.WriteLine("Please enter your name:");

            string heroName = this.reader.ReadLine();
            while (string.IsNullOrWhiteSpace(heroName))
            {
                this.renderer.WriteLine("Player name cannot be empty. Please re-enter.");
                heroName = this.reader.ReadLine();
            }

            return heroName;
        }

        private void PopulateItems()
        {
            for (int i = 0; i < InitialNumberOfElexirs; i++)
            {
                Item item = this.CreateItem();
                this.items.Add(item);
            }
        }

        private Item CreateItem()
        {
            int currentX = Rand.Next(1, MapWidth);
            int currentY = Rand.Next(1, MapHeight);

            bool containsEnemy = this.characters
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);

            bool containsBeer = this.items
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);

            while (containsEnemy || containsBeer)
            {
                currentX = Rand.Next(1, MapWidth);
                currentY = Rand.Next(1, MapHeight);

                containsEnemy = this.characters
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);

                containsBeer = this.items
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);
            }

            int elexirType = Rand.Next(0, 3);

            LifeElexirSize elexirSize;

            switch (elexirType)
            {
                case 0:
                    elexirSize = LifeElexirSize.Small;
                    break;
                case 1:
                    elexirSize = LifeElexirSize.Medium;
                    break;
                case 2:
                    elexirSize = LifeElexirSize.Large;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("beerType", "Invalid beer type.");
            }

            return new LifeElexir(new Position(currentX, currentY), elexirSize);

        }


        private void PopulateEnemies()
        {
            for (int i = 0; i < InitialNumberOfEnemies; i++)
            {
                GameObject enemy = this.CreateEnemy();
                this.characters.Add(enemy);
            }
        }

        private GameObject CreateEnemy()
        {
            int currentX = Rand.Next(1, MapWidth);
            int currentY = Rand.Next(1, MapHeight);

            bool containsEnemy = this.characters
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);

            while (containsEnemy)
            {
                currentX = Rand.Next(1, MapWidth);
                currentY = Rand.Next(1, MapHeight);

                containsEnemy = this.characters
                .Any(e => e.Position.X == currentX && e.Position.Y == currentY);
            }

            int nameIndex = Rand.Next(0, this.characterNames.Length);
            string name = this.characterNames[nameIndex];

            var enemyTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.CustomAttributes
                    .Any(a => a.AttributeType == typeof(EnemyAttribute)))
                    .ToArray();

            var type = enemyTypes[Rand.Next(0, enemyTypes.Length)];

            GameObject character = Activator
                .CreateInstance(type, new Position(currentX, currentY), name) as GameObject;

            return character;
        }


    }
}