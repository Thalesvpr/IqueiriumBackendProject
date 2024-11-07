using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Application.Services.Products
{
    public class ProductFeedbackAnalysisService
    {
        private readonly ApplicationDbContext _context;

        public ProductFeedbackAnalysisService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductFeedbackAnalysisResponseDto>> AnalyzeFeedback(ProductFeedbackAnalysisCreateDto analysisDto)
        {
            var analyses = new List<ProductFeedbackAnalysisResponseDto>();

            foreach (var feedbackId in analysisDto.ProductFeedbackIds)
            {
                var analysis = new ProductFeedbackAnalysis
                {
                    Content = analysisDto.Content,
                    AnalystUserId = analysisDto.AnalystUserId,
                    ProductFeedbackId = feedbackId
                };

                _context.ProductFeedbackAnalyses.Add(analysis);
                await _context.SaveChangesAsync();

                analyses.Add(new ProductFeedbackAnalysisResponseDto
                {
                    Id = analysis.Id,
                    ProductFeedbackId = feedbackId,
                    Content = analysis.Content,
                    CreatedDate = analysis.CreatedDate
                });
            }

            return analyses;
        }

        public async Task<List<ProductFeedbackAnalysisResponseDto>> GetFeedbackAnalyses(int feedbackId)
        {
            return await _context.ProductFeedbackAnalyses
                .Where(pfa => pfa.ProductFeedbackId == feedbackId)
                .Select(pfa => new ProductFeedbackAnalysisResponseDto
                {
                    Id = pfa.Id,
                    ProductFeedbackId = feedbackId,
                    Content = pfa.Content,
                    CreatedDate = pfa.CreatedDate,
                    AnalystUserId = pfa.AnalystUserId
                })
                .ToListAsync();
        }
    }
}
