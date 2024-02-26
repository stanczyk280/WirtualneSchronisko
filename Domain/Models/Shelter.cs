using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string MainPhotoUri { get; set; }
        public virtual List<Animal> Animals { get; set; }
    }
}