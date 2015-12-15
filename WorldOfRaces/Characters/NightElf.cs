using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfRaces;
using WorldOfRaces.Attributes;
using WorldOfRaces.Characters;

[Enemy]
    public class NightElf : Character
    {
        private const int NightElfDamage = 90;
        private const int NightElfHealth = 110;
        private const char NightElffSymbol = 'N';

        public NightElf(Position position, string name)
            : base(position, NightElffSymbol, name, NightElfDamage, NightElfHealth)
        {
        }          
    }
}