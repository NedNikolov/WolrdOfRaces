using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters
{
    using Attributes;

    [Enemy]
    public class Undeal : Character
    {
        private const int UndealDamage = 310;
        private const int UndealHealth = 600;
        private const char UndealfSymbol = 'U';

        public Undeal(Position position, string name)
            : base(position, UndealfSymbol, name, UndealDamage, UndealHealth)
        {
        }
    }
}
