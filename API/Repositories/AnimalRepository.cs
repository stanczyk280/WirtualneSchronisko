using Domain.DTOs;
using Domain.Models;
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

        public async Task<AnimalDTO> GetAnimal(int id)
        {
            var animal = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == id);
            return animal != null ? new AnimalDTO(animal) : null;
        }

        public async Task<IEnumerable<AnimalDTO>> GetAnimals()
        {
            var animals = await _dbContext.Animals.ToListAsync();
            return animals.Select(animal => new AnimalDTO(animal));
        }

        public async Task<IEnumerable<AnimalDTO>> GetAnimalsByShelter(int shelterId)
        {
            var animals = await _dbContext.Animals
            .Where(x => x.ShelterId == shelterId)
            .ToListAsync();

            var animalDTOs = animals.Select(animal => new AnimalDTO(animal)).ToList();
            return animalDTOs;
        }

        public async Task<AnimalDTO> AddAnimal(AnimalDTO animalDTO, int shelterId)
        {
            var shelter = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == shelterId);

            if (shelter != null)
            {
                var animal = new Animal
                {
                    LocalId = animalDTO.LocalId,
                    Name = animalDTO.Name,
                    Species = animalDTO.Species,
                    Breed = animalDTO.Breed,
                    Age = animalDTO.Age,
                    Description = animalDTO.Description,
                    MainPhotoUri = animalDTO.MainPhotoUri,
                    ShelterId = animalDTO.ShelterId
                };

                _dbContext.Animals.Add(animal);
                await _dbContext.SaveChangesAsync();

                var addedAnimalDTO = new AnimalDTO(animal);
                return addedAnimalDTO;
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

        public async Task<AnimalDTO> UpdateAnimal(AnimalDTO updatedAnimalDTO)
        {
            var animalToUpdate = await _dbContext.Animals.FirstOrDefaultAsync(x => x.Id == updatedAnimalDTO.Id);

            if (animalToUpdate != null)
            {
                animalToUpdate.LocalId = updatedAnimalDTO.LocalId;
                animalToUpdate.Name = updatedAnimalDTO.Name;
                animalToUpdate.Species = updatedAnimalDTO.Species;
                animalToUpdate.Breed = updatedAnimalDTO.Breed;
                animalToUpdate.Age = updatedAnimalDTO.Age;
                animalToUpdate.Description = updatedAnimalDTO.Description;
                animalToUpdate.MainPhotoUri = updatedAnimalDTO.MainPhotoUri;

                await _dbContext.SaveChangesAsync();

                return new AnimalDTO(animalToUpdate);
            }

            return null;
        }
    }
}