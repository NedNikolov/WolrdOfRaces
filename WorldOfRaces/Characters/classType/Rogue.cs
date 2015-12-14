using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters.classType
{
    using Characters;
    public class Rogue:Character
    {
        public Rogue():base(150,40,100)
        {

        }
        public override void Attack(Character target)
        {
            this.Mana -= 10;
            this.Damage += 20;
        }
    }
}
