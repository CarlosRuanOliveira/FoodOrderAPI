namespace Domain.Entities
{
    public class MenuItem
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public decimal PriceCents { get; private set; }

        public MenuItem(string name, decimal priceCents)
        {
            Name = name;
            PriceCents = priceCents;
        }

        public void Update(string name, decimal priceCents)
        {
            Name = name;
            PriceCents = priceCents;
        }
    }
}