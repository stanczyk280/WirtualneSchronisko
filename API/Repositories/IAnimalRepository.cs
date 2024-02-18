using Domain.DTOs;
using Domain.Models;

namespace API.Repositories
{
    public interface IAnimalRepository
    {
        public Task<AnimalDTO> GetAnimal(int id);

        public Task<IEnumerable<AnimalDTO>> GetAnimals();

        public Task<IEnumerable<AnimalDTO>> GetAnimalsByShelter(int shelterId);

        public Task<AnimalDTO> AddAnimal(AnimalDTO animal, int shelterId);

        public Task<AnimalDTO> UpdateAnimal(AnimalDTO animal);

        public Task DeleteAnimal(int id);
    }
}