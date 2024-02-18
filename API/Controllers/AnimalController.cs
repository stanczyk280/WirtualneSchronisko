using API.Repositories;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimals()
        {
            try
            {
                var animalDTOs = await _animalRepository.GetAnimals();
                return Ok(animalDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when retrieving data from the database: " + ex.Message);
            }
        }

        [HttpGet("GetAnimalsByShelter/{shelterId}")]
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimalsByShelter(int shelterId)
        {
            try
            {
                var animalDTOs = await _animalRepository.GetAnimalsByShelter(shelterId);
                return Ok(animalDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when retrieving data from the database: " + ex.Message);
            }
        }

        [HttpGet("GetAnimal/{id}")]
        public async Task<ActionResult<AnimalDTO>> GetAnimal(int id)
        {
            try
            {
                var animalDTO = await _animalRepository.GetAnimal(id);

                if (animalDTO == null)
                {
                    return NotFound($"Animal with ID {id} not found");
                }

                return Ok(animalDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when retrieving data from the database: " + ex.Message);
            }
        }

        [HttpPost("AddAnimal/{shelterId}")]
        public async Task<ActionResult<AnimalDTO>> AddAnimal(AnimalDTO animalDTO, int shelterId)
        {
            try
            {
                if (animalDTO == null)
                {
                    return BadRequest();
                }

                var addedAnimalDTO = await _animalRepository.AddAnimal(animalDTO, shelterId);

                if (addedAnimalDTO != null)
                {
                    return CreatedAtAction(nameof(GetAnimal), new { id = addedAnimalDTO.Id }, addedAnimalDTO);
                }

                return NotFound($"Shelter with ID {shelterId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when trying to post data to the database: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalDTO>> UpdateAnimal(int id, AnimalDTO updatedAnimalDTO)
        {
            try
            {
                if (id != updatedAnimalDTO.Id)
                {
                    return BadRequest($"Id mismatch. Given id: {id}, Checked id: {updatedAnimalDTO.Id}");
                }

                var updatedAnimal = await _animalRepository.UpdateAnimal(updatedAnimalDTO);

                if (updatedAnimal == null)
                {
                    return NotFound($"Animal with ID {id} not found");
                }

                return Ok(updatedAnimal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred when trying to put data to the database: " + ex.Message);
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