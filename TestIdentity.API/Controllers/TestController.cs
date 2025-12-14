using TestIdentity.Aplication.DTOs.TestDTOs;
using TestIdentity.Aplication.Services.TestServices;
using TestIdentity.Domain.Utilities.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace TestIdentity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// Tüm test verilerini getirir / Gets all test data
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _testService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Yeni test verisi oluşturur / Creates new test data
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestCreateDTO testCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _testService.CreateAsync(testCreateDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// ID'ye göre test verisi getirir / Gets test data by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _testService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Test verisini günceller / Updates test data
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TestUpdateDTO testUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _testService.UpdateAsync(testUpdateDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Test verisini siler / Deletes test data
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

