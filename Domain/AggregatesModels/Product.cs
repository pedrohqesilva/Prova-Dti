namespace Application.Domain.AggregatesModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
        public bool Valid { get; set; } = true;
    }
}