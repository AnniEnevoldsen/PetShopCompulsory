using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShopCompulsory.Core.ApplicationService;
using PetShopCompulsory.Core.Entities;

namespace PetShop.RestAPI.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class PetOwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public PetOwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/PetOwner
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get()
        {
            return _ownerService.GetOwners();
        }

        // GET: api/PetOwner/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Owner> Get(int id)
        {
            return _ownerService.FindOwnerById(id);
        }

        // POST: api/PetOwner
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            return _ownerService.AddOwner(owner);
        }

        // PUT: api/PetOwner/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            return _ownerService.EditOwner(owner);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            var ownerOut = _ownerService.DeleteOwner(id);
            //if (ownerOut == null)
            //{
            //    return StatusCode(404, "Could not find owner with id" + id);
            //}
            return Ok($"Owner with id {id} is deleted");
        }
    }
}
