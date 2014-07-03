namespace Implementation.Models
{
    public class Asset : ModelWithIdentity
    {
        public Asset(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }

        public decimal Price { get; protected set; }

        public Bid CurrentWinningBid { get; set; }
    }
}