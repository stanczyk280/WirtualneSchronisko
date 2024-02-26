using API.Repositories;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShelterController : BaseApiController
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterController(IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShelterDTO>>> GetShelters()
        {
            try
            {
                return Ok(await _shelterRepository.GetShelters());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when retriving data from the database: " + ex.Message);
            }
        }

        [HttpGet("GetShelter/{id}")]
        public async Task<ActionResult<ShelterDTO>> GetShelter(int id)
        {
            try
            {
                return Ok(await _shelterRepository.GetShelter(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when retriving data from the database: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Shelter>> AddShelter(ShelterDTO shelterDTO)
        {
            try
            {
                if (shelterDTO == null)
                {
                    return BadRequest();
                }

                var postedShelter = await _shelterRepository.AddShelter(shelterDTO);

                return CreatedAtAction(nameof(GetShelter), new { id = postedShelter.Id }, postedShelter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when trying to post data to the database: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShelterDTO>> UpdateShelter(int id, ShelterDTO updatedShelterDTO)
        {
            try
            {
                if (id != updatedShelterDTO.Id)
                {
                    return BadRequest($"Id missmatch Given id: {id}, Checked id: {updatedShelterDTO.Id}");
                }

                var shelterToUpdate = await _shelterRepository.GetShelter(id);

                if (shelterToUpdate == null)
                {
                    return NotFound($"Given shelter of \"{id}\" not found");
                }

                return await _shelterRepository.UpdateShelter(updatedShelterDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when trying to put data to the database: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Shelter>> DeleteShelter(int id)
        {
            try
            {
                var shelterToDelete = await _shelterRepository.GetShelter(id);

                if (shelterToDelete == null)
                {
                    return NotFound($"Given shelter of \"{id}\" not found");
                }

                await _shelterRepository.DeleteShelter(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occured when trying to delete data from the database: " + ex.Message);
            }
        }
    }
}