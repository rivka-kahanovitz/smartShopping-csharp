using Microsoft.AspNetCore.Mvc;
using common.DTOs;
using common.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IService<StoreDto> _service;

        public StoreController(IService<StoreDto> service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<StoreDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<StoreDto> GetById(int id)
        {
            var store = _service.GetById(id);
            if (store == null)
                return NotFound();
            return Ok(store);
        }
        [Authorize]
        [HttpPost]
        public ActionResult<StoreDto> Add(StoreDto storeDto)
        {
            var newStore = _service.AddItem(storeDto);
            return CreatedAtAction(nameof(GetById), new { id = newStore.Id }, newStore);
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<StoreDto> Update(int id, StoreDto storeDto)
        {
            return Ok(_service.Update(id, storeDto));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
