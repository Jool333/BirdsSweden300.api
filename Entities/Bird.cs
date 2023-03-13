namespace BirdsSweden300.api.Entities
{
    public class Bird 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Genus { get; set; }
        public string Family { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; } = "no-bird.png";
        public bool Seen { get; set; }

    }
}