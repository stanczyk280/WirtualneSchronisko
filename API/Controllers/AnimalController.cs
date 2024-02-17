using API.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AnimalController : BaseApiController
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IShelterRepository _shelterRepository;

        public AnimalController(IAnimalRepository animalRepository, IShelterRepository shelterRepository)
        {
            _animalRepository = animalRepository;
            _shelterRepository = shelterRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimals()
        {
            try
            {
                return Ok(await _animalRepository.GetAnimals());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when retriving data from the database: " + ex.Message);
            }
        }

        [HttpGet("GetAnimalsByShelter/{shelterId}")]
        public async Task<ActionResult<List<Animal>>> GetAnimalsByShelter(int shelterId)
        {
            try
            {
                return Ok(await _animalRepository.GetAnimalsByShelter(shelterId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when retriving data from the database: " + ex.Message);
            }
        }

        [HttpGet("GetAnimal/{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            try
            {
                return Ok(await _animalRepository.GetAnimal(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when retriving data from the database: " + ex.Message);
            }
        }

        [HttpPost("AddAnimal/{shelterId}")]
        public async Task<ActionResult<Animal>> AddAnimal(Animal animal, int shelterId)
        {
            try
            {
                if (animal == null)
                {
                    return BadRequest();
                }

                var postedAnimal = await _animalRepository.AddAnimal(animal, shelterId);

                if (postedAnimal != null)
                {
                    var simplifiedResponse = new
                    {
                        Id = postedAnimal.Id,
                        Name = postedAnimal.Name,
                    };

                    return CreatedAtAction(nameof(GetAnimal), new { id = postedAnimal.Id }, simplifiedResponse);
                }

                return NotFound($"Shelter with ID {shelterId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when trying to post data to the database: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, Animal animal)
        {
            try
            {
                if (id != animal.Id)
                {
                    return BadRequest($"Id missmatch Given id: {id}, Checked id: {animal.Id}");
                }

                var animalToUpdate = await _animalRepository.GetAnimal(id);

                if (animalToUpdate == null)
                {
                    return NotFound($"Given animal of \"{id}\" not found");
                }

                return await _animalRepository.UpdateAnimal(animal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when trying to put data to the database: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            try
            {
                var animalToDelete = await _animalRepository.GetAnimal(id);

                if (animalToDelete == null)
                {
                    return NotFound($"Given animal of \"{id}\" not found");
                }

                await _animalRepository.DeleteAnimal(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when trying to delete data from the database: " + ex.Message);
            }
        }
    }
}