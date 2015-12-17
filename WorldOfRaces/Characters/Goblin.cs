using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfRaces;
using WorldOfRaces.Attributes;
using WorldOfRaces.Characters;

 [Enemy]
    public class Goblin : Character
    {
        private const int GoblinDamage = 100;
        private const int GoblinHealth = 250;
        private const char GoblinSymbol = 'G';

        public Goblin(Position position, string name)
            : base(position, GoblinSymbol, name, GoblinDamage, GoblinHealth)
        {
        }
    }
