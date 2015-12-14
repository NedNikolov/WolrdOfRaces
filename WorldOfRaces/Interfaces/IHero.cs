namespace WorldOfRaces.Interfaces
{
    using Characters;
    public interface IHero : ICharacter, IMoving, ICollect, IHealing
    {
        HeroRace Race { get; }
    }
}
