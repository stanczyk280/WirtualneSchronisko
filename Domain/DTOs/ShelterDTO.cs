using Domain.Models;
using System.Text.Json.Serialization;

namespace Domain.DTOs
{
    public class ShelterDTO
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

        [JsonConstructor]
        public ShelterDTO()
        { }

        public ShelterDTO(Shelter shelter)
        {
            Id = shelter.Id;
            Name = shelter.Name;
            City = shelter.City;
            Street = shelter.Street;
            PostalCode = shelter.PostalCode;
            Email = shelter.Email;
            Phone = shelter.Phone;
            Description = shelter.Description;
            MainPhotoUri = shelter.MainPhotoUri;
        }
    }
}