namespace RepositoryPatternCrudEFCore.Entity
{
    public class Order
    {
        public int OrderId { get; set; }    

        public DateTime OrderDate { get; set; }

        //Navigation Property

        public int ProductId { get; set; }

        //Navigation Property

        public Product Product { get; set; }
    }
}
