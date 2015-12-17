namespace WorldOfRaces.Characters
{
    using Attributes;

    [Enemy]
    public class Tauren : Character
    {
        private const int TaurenDamage = 250;
        private const int TaurenHealth = 900;
        private const char TaurenfSymbol = 'T';

        public Tauren(Position position, string name)
            : base(position, TaurenfSymbol, name, TaurenDamage, TaurenHealth)
        {
        }
    }
}
