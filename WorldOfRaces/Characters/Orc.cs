namespace WorldOfRaces.Characters
{
    using Attributes;

    [Enemy]
    public class Orc : Character
    {
        private const int OrcDamage = 300;
        private const int OrcHealth = 800;
        private const char OrcfSymbol = 'O';

        public Orc(Position position, string name)
            : base(position, OrcfSymbol, name, OrcDamage, OrcHealth)
        {
        }
    }
}
