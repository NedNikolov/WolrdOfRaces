namespace RPG_Ned.Items
{
    using Attributes;

    [Item]
    public class LifeElexir : Item
    {
         private const char ElexirSymbol = 'L';

         public LifeElexir(Position position, LifeElexirSize elexirSize)
             : base(position, ElexirSymbol)
        {
            this.LifeElexirSize = elexirSize;
        }

        public int HealthRestore
        {
            get
            {
                return (int)this.LifeElexirSize;
            }
        }

        private LifeElexirSize LifeElexirSize { get; set; }
    }
}