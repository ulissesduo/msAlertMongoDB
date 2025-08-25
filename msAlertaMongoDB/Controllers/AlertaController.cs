using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using msAlertaMongoDB.DTO;
using msAlertaMongoDB.Entity;
using msAlertaMongoDB.Service;

namespace msAlertaMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : Controller
    {
        private readonly IAlertaService alertaService;
        private readonly IMapper mapper;

        public AlertaController(IAlertaService _alertaService, IMapper _mapper)
        {
            alertaService = _alertaService;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlertaResponseDto>>> GetAll() 
        {
            var list = await alertaService.GetAllAlertas();
            var response = mapper.Map<List<AlertaResponseDto>>(list);
            return Ok(response);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaResponseDto>> GetAlertaByID(string id) 
        {
            var alertById = await alertaService.GetAlertaById(id);
            if(alertById == null ) return NotFound();
            return Ok(alertById);
        }

        [HttpPost]
        public async Task<ActionResult<AlertaResponseDto>> CreateAlert([FromBody] AlertaRequestDto alert)
        {
            var entity = mapper.Map<Alerta>(alert);
            var created = await alertaService.CreateAlert(entity);
            var response = mapper.Map<AlertaResponseDto>(created);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AlertaResponseDto>> UpdateAlert(string id, [FromBody] AlertaRequestDto alert)
        {
            var existing = await alertaService.GetAlertaById(id);
            if (existing == null) return NotFound();

            var updatedEntity = mapper.Map<Alerta>(alert);
            updatedEntity.Id = id; 

            var updated = await alertaService.UpdateAlert(id, updatedEntity);
            var response = mapper.Map<AlertaResponseDto>(updated);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(string id)
        {
            var existing = await alertaService.GetAlertaById(id);
            if (existing == null) return NotFound();

            var deleted = await alertaService.DeleteAlert(id);
            if (!deleted) return BadRequest("Failed to delete alert.");

            return NoContent();
        }

    }
}
