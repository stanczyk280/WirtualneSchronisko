using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Animal
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

        public Shelter Shelter { get; set; }
    }
}