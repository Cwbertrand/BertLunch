namespace Model
{
    public class Review
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; } 
        public AppUser? User { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
