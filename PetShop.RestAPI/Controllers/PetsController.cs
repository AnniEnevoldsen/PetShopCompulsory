using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShopCompulsory.Core.Entities;
using PetShopCompulsory.Core.ServiceFolder;
using Microsoft.AspNetCore.Authorization;

namespace PetShop.RestAPI.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET api/values -- Get All Pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_petService.GetFilteredPets(filter));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            // return Ok(_petService.GetPets());
        }

        // GET api/values/5 ---Read by ID
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            return _petService.FindPetById(id);
        }

        // POST api/values -- Create
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            return _petService.AddPet(pet);
        }

        // PUT api/values/5 --- Update
       // [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            if (id < 1 || id != pet.Id)
            {
                return BadRequest("Baaaaad.. id stuff is not valid");
            }

            return Ok(_petService.EditPet(pet));
        }

        // DELETE api/values/5
        //[Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            var petOut = _petService.DeletePet(id);
            //if (petOut == null)
            //{
            //    return StatusCode(404, "Could not find pet with id" + id);
            //}
            return Ok($"Pet with id {id} is deleted");
        }
    }
}
