using System.ComponentModel.DataAnnotations;

namespace BirdsSweden300.api.ViewModels.Bird
{
    public class BirdPostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Namn måste anges")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; } = "no-bird.png";
        public string Species { get; set; }
        public string Genus { get; set; }
        public string Family { get; set; }
        public bool Seen { get; set; } = false;
    }
}