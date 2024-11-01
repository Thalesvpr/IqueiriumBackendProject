using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities.ManyToMany;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Application.Services
{
    public class ProductFeedbackAnalysisService
    {
        private readonly ApplicationDbContext _context;

        public ProductFeedbackAnalysisService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductFeedbackAnalysisResponseDto> AnalyzeFeedback(ProductFeedbackAnalysisCreateDto analysisDto)
        {
            // Cria uma nova análise de feedback
            var analysis = new ProductFeedbackAnalysis
            {
                Content = analysisDto.Content,
                CreatedDate = DateTime.UtcNow
            };

            _context.ProductFeedbackAnalyses.Add(analysis);
            await _context.SaveChangesAsync();

            // Cria a relação entre ProductFeedback e ProductFeedbackAnalysis na tabela de junção
            var feedbackAnalysisRelation = new ProductFeedbackAnalysisProductFeedback
            {
                ProductFeedbackId = analysisDto.ProductFeedbackId,
                ProductFeedbackAnalysisId = analysis.Id
            };

            _context.ProductFeedbackAnalysisProductFeedbacks.Add(feedbackAnalysisRelation);
            await _context.SaveChangesAsync();

            return new ProductFeedbackAnalysisResponseDto
            {
                Id = analysis.Id,
                ProductFeedbackId = analysisDto.ProductFeedbackId,
                Content = analysis.Content,
                CreatedDate = analysis.CreatedDate
            };
        }

        public async Task<IEnumerable<ProductFeedbackAnalysisResponseDto>> GetFeedbackAnalyses(int feedbackId)
        {
            // Recupera todas as análises associadas ao feedback específico usando a tabela de junção
            return await _context.ProductFeedbackAnalysisProductFeedbacks
                .Where(rel => rel.ProductFeedbackId == feedbackId)
                .Select(rel => new ProductFeedbackAnalysisResponseDto
                {
                    Id = rel.ProductFeedbackAnalysis.Id,
                    ProductFeedbackId = feedbackId,
                    Content = rel.ProductFeedbackAnalysis.Content,
                    CreatedDate = rel.ProductFeedbackAnalysis.CreatedDate
                })
                .ToListAsync();
        }
    }
}
