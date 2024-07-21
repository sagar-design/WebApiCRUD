namespace RepositoryPatternCrudEFCore.ViewModel
{
    public class ProductRequest
    {
        public int ProductID {  get; set; } 

        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
