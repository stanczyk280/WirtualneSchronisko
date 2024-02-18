using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly DataContext _dbContext;

        public ShelterRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shelter> GetShelter(int id)
        {
            return await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Shelter>> GetShelters()
        {
            return await _dbContext.Shelters.ToListAsync();
        }

        public async Task<Shelter> AddShelter(Shelter shelter)
        {
            var result = await _dbContext.Shelters.AddAsync(shelter);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteShelter(int id)
        {
            var result = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                _dbContext.Shelters.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Shelter> UpdateShelter(Shelter shelter)
        {
            var shelterToUpdate = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == shelter.Id);

            if (shelterToUpdate != null)
            {
                shelterToUpdate.Name = shelter.Name;
                shelterToUpdate.City = shelter.City;
                shelterToUpdate.Street = shelter.Street;
                shelterToUpdate.PostalCode = shelter.PostalCode;
                shelterToUpdate.Email = shelter.Email;
                shelterToUpdate.Phone = shelter.Phone;
                shelterToUpdate.Description = shelter.Description;
                shelterToUpdate.MainPhotoUri = shelter.MainPhotoUri;

                await _dbContext.SaveChangesAsync();

                return shelterToUpdate;
            }

            return null;
        }
    }
}