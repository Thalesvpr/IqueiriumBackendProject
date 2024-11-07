using Microsoft.AspNetCore.Mvc;
using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services.Products;

namespace IqueiriumBackendProject.Src.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductFeedbackController : ControllerBase
    {
        private readonly ProductFeedbackService _feedbackService;

        public ProductFeedbackController(ProductFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // Endpoint para enviar feedback sobre um produto
        [HttpPost("submit")]
        public async Task<ActionResult<ProductFeedbackResponseDTO>> SubmitFeedback([FromBody] ProductFeedbackCreateDTO feedbackDto)
        {
            var createdFeedback = await _feedbackService.SubmitFeedback(feedbackDto);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.Id }, createdFeedback);
        }

        // Endpoint para listar feedbacks de um produto específico
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductFeedbackResponseDTO>>> GetFeedbacksByProduct(int productId)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByProduct(productId);
            return Ok(feedbacks);
        }

        // Endpoint para obter um feedback específico
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFeedbackResponseDTO>> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }
    }
}
