using Domain;

namespace API.Repositories
{
    public interface IShelterRepository
    {
        Task<IEnumerable<Shelter>> GetShelters();

        Task<Shelter> GetShelter(int id);

        Task<Shelter> AddShelter(Shelter shelter);

        Task<Shelter> UpdateShelter(Shelter shelter);

        Task DeleteShelter(int id);
    }
}