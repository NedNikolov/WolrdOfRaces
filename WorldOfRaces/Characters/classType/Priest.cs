using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfRaces.Characters.classType
{
    using Interfaces;
    public class Priest:Character,IHeal
    {
        protected Priest() : base(125, 200, 100)
        {

        }
        public override void Attack(Character target)
        {
            this.Mana -= 50;
            target.Health -= this.Damage;
            this.Health = this.Damage / 10;
        }
        public void Heal(Character target)
        {
            this.Mana -= 100;
            this.Health += 70; 
        }
    }
}
