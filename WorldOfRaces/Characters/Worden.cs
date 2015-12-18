using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters
{
    using Attributes;

    [Enemy]
    public class Worden : Character
    {
        private const int WordenDamage = 150;
        private const int WordenHealth = 700;
        private const char WordenSymbol = 'W';

        public Worden(Position position, string name)
            : base(position, WordenSymbol, name, WordenDamage, WordenHealth)
        {
        }
    }
}
