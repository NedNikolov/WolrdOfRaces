using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters
{
    using Attributes;

    [Enemy]
    public class Troll : Character
    {
        private const int TrollDamage = 115;
        private const int TrollHealth = 270;
        private const char TrollfSymbol = 'R';

        public Troll(Position position, string name)
            : base(position, TrollfSymbol, name, TrollDamage, TrollHealth)
        {
        }
    }
}
