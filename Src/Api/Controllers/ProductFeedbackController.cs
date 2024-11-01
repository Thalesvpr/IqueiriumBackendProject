namespace IqueiriumBackendProject.Src.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::IqueiriumBackendProject.Src.Application.Dtos.Products;
    using global::IqueiriumBackendProject.Src.Application.Services;

    namespace IqueiriumBackendProject.Src.Application.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProductFeedbackController : ControllerBase
        {
            private readonly ProductFeedbackService _feedbackService;
            private readonly ProductFeedbackAnalysisService _analysisService;

            public ProductFeedbackController(ProductFeedbackService feedbackService, ProductFeedbackAnalysisService analysisService)
            {
                _feedbackService = feedbackService;
                _analysisService = analysisService;
            }

            // Endpoint para enviar feedback sobre um produto
            [HttpPost("submit")]
            public async Task<ActionResult<ProductFeedbackResponseDTO>> SubmitFeedback(ProductFeedbackCreateDTO feedbackDto)
            {
                var createdFeedback = await _feedbackService.SubmitFeedback(feedbackDto);
                return CreatedAtAction(nameof(SubmitFeedback), new { id = createdFeedback.Id }, createdFeedback);
            }

            // Endpoint para listar feedbacks de um produto específico
            [HttpGet("product/{productId}")]
            public async Task<ActionResult<IEnumerable<ProductFeedbackResponseDTO>>> GetFeedbacksByProduct(int productId)
            {
                var feedbacks = await _feedbackService.GetFeedbacksByProduct(productId);
                return Ok(feedbacks);
            }

            // Endpoint para adicionar uma análise a um feedback específico
            [HttpPost("{feedbackId}/analyze")]
            public async Task<ActionResult<ProductFeedbackAnalysisResponseDto>> AnalyzeFeedback(int feedbackId, ProductFeedbackAnalysisCreateDto analysisDto)
            {
                analysisDto.ProductFeedbackId = feedbackId; // Define o ID do feedback
                var analysis = await _analysisService.AnalyzeFeedback(analysisDto);
                return CreatedAtAction(nameof(AnalyzeFeedback), new { id = analysis.Id }, analysis);
            }

            // Endpoint para listar todas as análises associadas a um feedback específico
            [HttpGet("{feedbackId}/analyses")]
            public async Task<ActionResult<IEnumerable<ProductFeedbackAnalysisResponseDto>>> GetFeedbackAnalyses(int feedbackId)
            {
                var analyses = await _analysisService.GetFeedbackAnalyses(feedbackId);
                return Ok(analyses);
            }
        }
    }
}