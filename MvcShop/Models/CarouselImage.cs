namespace MvcShop.Models
{
    public class CarouselImage
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ImageUrl { get; set; } = string.Empty; // can be relative (/uploads/... ) or external
        public int SortOrder { get; set; }
    }
}
