using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters.classType
{
    public class Warrior:Character
    {
        protected Warrior():base(200,0,130)
        {

        }
        public override void Attack(Character target)
        {
            if (this.Health > (this.Health - 40))
            {
                this.Damage += this.Damage + 10;
            }
        }
    }
}
