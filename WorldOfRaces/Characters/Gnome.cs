using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfRaces;
using WorldOfRaces.Attributes;
using WorldOfRaces.Characters;

[Enemy]
    public class Gnome : Character
    {
        private const int GnomeDamage = 120;
        private const int GnomeHealth = 260;
        private const char GnomefSymbol = 'M';

        public Gnome(Position position, string name)
            : base(position, GnomefSymbol, name, GnomeDamage, GnomeHealth)
        {
        }
    }
}