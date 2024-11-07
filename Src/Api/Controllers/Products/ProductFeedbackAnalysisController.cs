using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Application.Services;

namespace IqueiriumBackendProject.Src.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductFeedbackAnalysisController : ControllerBase
    {
        private readonly ProductFeedbackAnalysisService _analysisService;

        public ProductFeedbackAnalysisController(ProductFeedbackAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        // Endpoint para adicionar uma análise a uma lista de feedbacks específicos
        [HttpPost("analyze")]
        public async Task<ActionResult<List<ProductFeedbackAnalysisResponseDto>>> AnalyzeFeedbacks([FromBody] ProductFeedbackAnalysisCreateDto analysisDto)
        {
            var analyses = await _analysisService.AnalyzeFeedback(analysisDto);
            if (analyses.Count == 1)
            {
                return CreatedAtAction(nameof(GetFeedbackAnalyses), new { feedbackId = analyses[0].ProductFeedbackId }, analyses);
            }
            return Created("api/productfeedbackanalysis/analyze", analyses);
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
