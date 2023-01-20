namespace Error.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        public int Count { get; set; }  


        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
