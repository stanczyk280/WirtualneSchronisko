using Domain.DTOs;

namespace API.Repositories
{
    public interface IShelterRepository
    {
        Task<IEnumerable<ShelterDTO>> GetShelters();

        Task<ShelterDTO> GetShelter(int id);

        Task<ShelterDTO> AddShelter(ShelterDTO shelter);

        Task<ShelterDTO> UpdateShelter(ShelterDTO shelter);

        Task DeleteShelter(int id);
    }
}