using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public string LocalId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public string Description { get; set; }
        public string MainPhotoUri { get; set; }
        public int ShelterId { get; set; }

        [JsonConstructor]
        public AnimalDTO()
        {
        }

        public AnimalDTO(Animal animal)
        {
            Id = animal.Id;
            LocalId = animal.LocalId;
            Name = animal.Name;
            Species = animal.Species;
            Breed = animal.Breed;
            Age = animal.Age;
            Description = animal.Description;
            MainPhotoUri = animal.MainPhotoUri;
            ShelterId = animal.ShelterId;
        }
    }
}