using Domain;

namespace API.Repositories
{
    public interface IAnimalRepository
    {
        public Task<Animal> GetAnimal(int id);

        public Task<IEnumerable<Animal>> GetAnimals();

        public Task<IEnumerable<Animal>> GetAnimalsByShelter(int shelterId);

        public Task<Animal> AddAnimal(Animal animal, int shelterId);

        public Task<Animal> UpdateAnimal(Animal animal);

        public Task DeleteAnimal(int id);
    }
}