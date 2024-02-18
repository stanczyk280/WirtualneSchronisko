using Domain;
using Domain.Models;

namespace Persistence
{
    public static class SeedData
    {
        public static async Task Seed(DataContext dbContext)
        {
            if (dbContext.Shelters.Any() || dbContext.Animals.Any())
            {
                return;
            }

            var shelter1 = new Shelter
            {
                Name = "Shelter 1",
                City = "City 1",
                Street = "Street 1",
                PostalCode = "12345",
                Email = "shelter1@example.com",
                Phone = "123-456-7890",
                Description = "Description for Shelter 1",
                MainPhotoUri = "photo1.jpg"
            };

            var shelter2 = new Shelter
            {
                Name = "Shelter 2",
                City = "City 2",
                Street = "Street 2",
                PostalCode = "67890",
                Email = "shelter2@example.com",
                Phone = "987-654-3210",
                Description = "Description for Shelter 2",
                MainPhotoUri = "photo2.jpg"
            };

            await dbContext.Shelters.AddRangeAsync(shelter1, shelter2);
            await dbContext.SaveChangesAsync();

            await SeedAnimalsForShelter(dbContext, shelter1.Id, 5);
            await SeedAnimalsForShelter(dbContext, shelter2.Id, 5);
        }

        private static async Task SeedAnimalsForShelter(DataContext dbContext, int shelterId, int numberOfAnimals)
        {
            var animals = new List<Animal>();

            for (int i = 1; i <= numberOfAnimals; i++)
            {
                var animal = new Animal
                {
                    ShelterId = shelterId,
                    Name = $"Animal{i}",
                    Species = "Species",
                };

                animals.Add(animal);
            }

            await dbContext.Animals.AddRangeAsync(animals);
            await dbContext.SaveChangesAsync();
        }

        public static async Task DeleteAllData(DataContext dbContext)
        {
            // Remove all animals
            dbContext.Animals.RemoveRange(dbContext.Animals);

            // Remove all shelters
            dbContext.Shelters.RemoveRange(dbContext.Shelters);

            // Save changes to apply deletions
            await dbContext.SaveChangesAsync();
        }
    }
}