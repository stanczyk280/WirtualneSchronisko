using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DataContext _dbContext;

        public AnimalRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Animal> GetAnimal(int id)
        {
            return await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            return await _dbContext.Animals.ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByShelter(int shelterId)
        {
            var result = await _dbContext.Animals
            .Where(x => x.ShelterId == shelterId)
            .ToListAsync();

            return result;
        }

        public async Task<Animal> AddAnimal(Animal animal, int shelterId)
        {
            var shelter = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == shelterId);

            if (shelter != null)
            {
                animal.ShelterId = shelterId;

                _dbContext.Animals.Add(animal);
                await _dbContext.SaveChangesAsync();

                return animal;
            }

            return null;
        }

        public async Task DeleteAnimal(int id)
        {
            var animalToDelete = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == id);
            if (animalToDelete != null)
            {
                _dbContext.Animals.Remove(animalToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Animal> UpdateAnimal(Animal animal)
        {
            var animalToUpdate = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == animal.Id);

            if (animalToUpdate != null)
            {
                animalToUpdate.LocalId = animal.LocalId;
                animalToUpdate.Name = animal.Name;
                animalToUpdate.Species = animal.Species;
                animalToUpdate.Breed = animal.Breed;
                animalToUpdate.Age = animal.Age;
                animalToUpdate.Description = animal.Description;
                animalToUpdate.MainPhotoUri = animal.MainPhotoUri;

                await _dbContext.SaveChangesAsync();

                return animalToUpdate;
            }

            return null;
        }
    }
}