using Domain.DTOs;
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

        public async Task<ShelterDTO> GetShelter(int id)
        {
            var shelter = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == id);

            return shelter != null ? new ShelterDTO(shelter) : null;
        }

        public async Task<IEnumerable<ShelterDTO>> GetShelters()
        {
            var shelters = await _dbContext.Shelters.ToListAsync();
            return shelters.Select(shelter => new ShelterDTO(shelter));
        }

        public async Task<ShelterDTO> AddShelter(ShelterDTO shelterDTO)
        {
            var shelter = new Shelter
            {
                Name = shelterDTO.Name,
                City = shelterDTO.City,
                Street = shelterDTO.Street,
                PostalCode = shelterDTO.PostalCode,
                Email = shelterDTO.Email,
                Phone = shelterDTO.Phone,
                Description = shelterDTO.Description,
                MainPhotoUri = shelterDTO.MainPhotoUri
            };

            _dbContext.Shelters.Add(shelter);
            await _dbContext.SaveChangesAsync();

            var addedShelterDTO = new ShelterDTO(shelter);
            return addedShelterDTO;
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

        public async Task<ShelterDTO> UpdateShelter(ShelterDTO updatedShelterDTO)
        {
            var shelterToUpdate = await _dbContext.Shelters.FirstOrDefaultAsync(x => x.Id == updatedShelterDTO.Id);

            if (shelterToUpdate != null)
            {
                shelterToUpdate.Name = updatedShelterDTO.Name;
                shelterToUpdate.City = updatedShelterDTO.City;
                shelterToUpdate.Street = updatedShelterDTO.Street;
                shelterToUpdate.PostalCode = updatedShelterDTO.PostalCode;
                shelterToUpdate.Email = updatedShelterDTO.Email;
                shelterToUpdate.Phone = updatedShelterDTO.Phone;
                shelterToUpdate.Description = updatedShelterDTO.Description;
                shelterToUpdate.MainPhotoUri = updatedShelterDTO.MainPhotoUri;

                await _dbContext.SaveChangesAsync();

                return new ShelterDTO(shelterToUpdate);
            }

            return null;
        }
    }
}