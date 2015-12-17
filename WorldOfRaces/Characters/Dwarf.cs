using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfRaces;
using WorldOfRaces.Attributes;
using WorldOfRaces.Characters;

    [Enemy]
    public class Dwarf : Character
    {
        private const int DwarfDamage = 200;
        private const int DwarfHealth = 250;
        private const char DwarfSymbol = 'D';

        public Dwarf(Position position, string name)
            : base(position, DwarfSymbol, name, DwarfDamage, DwarfHealth)
        {
        }
    }
