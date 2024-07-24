namespace LoginAPI.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public int? Uuid { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? Img_Url { get; set; }
        public bool? Is_Active { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}