
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api202.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository _repository;

        public CategoriesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {          
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            //return BadRequest();

            Category existed =await _repository.GetByIdAsync(id);

            if (existed != null) return StatusCode(StatusCodes.Status404NotFound);
            //return NotFound();

            return StatusCode(StatusCodes.Status200OK, existed);
            //return Ok(existed);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCatgoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name 
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangeAsync();

            return StatusCode(StatusCodes.Status201Created,category); 
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
