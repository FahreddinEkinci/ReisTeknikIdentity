namespace ReisTeknikIdentity.Web.ViewModels
{
    public class ProductDetailViewModel
    {
        public ProductDetailViewModel()
        {
            ImageUrls=  new List<string>();
        }
       
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public List<string> ImageUrls { get; set; }
        public decimal Price { get; set; }
    }
}
