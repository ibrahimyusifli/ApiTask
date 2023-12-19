
using Api202.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api202.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryRepository repository,ICategoryService service)
        {           
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            //return Ok(await _repository.GetAll(orderExpression:c=>c.Name,skip:(page-1)*take,take:take);
            return Ok(await _service.GetAllAsync(page,take));
        }

        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCatgoryDto categoryDto)
        {
            await _service.Create(categoryDto);
            return StatusCode(StatusCodes.Status201Created); 
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            

            Category existed = await _repository.GetByIdAsync(id);

            if (existed != null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name=name;
            _repository.Update(existed);


            await _repository.SaveChangeAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            //return BadRequest();

            Category existed = await _repository.GetByIdAsync(id);

            if (existed != null) return StatusCode(StatusCodes.Status404NotFound);
            //return NotFound();

            _repository.Delete(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }

    }
}
