using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace IqueiriumBackendProject.Src.Application.Services
{
    public class ProductFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public ProductFeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductFeedbackResponseDTO> SubmitFeedback(ProductFeedbackCreateDTO feedbackDto)
        {
            var feedback = new ProductFeedback
            {
                ProductId = feedbackDto.ProductId,
                Content = feedbackDto.Content,
                FeedbackType = feedbackDto.FeedbackType,
                CreatedDate = DateTime.UtcNow,
                UserId = feedbackDto.UserId,
            };

            _context.ProductFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return new ProductFeedbackResponseDTO
            {
                Id = feedback.Id,
                ProductId = feedback.ProductId,
                Content = feedback.Content,
                FeedbackType = feedback.FeedbackType,
                CreatedDate = feedback.CreatedDate
            };
        }

        public async Task<IEnumerable<ProductFeedbackResponseDTO>> GetFeedbacksByProduct(int productId)
        {
            return await _context.ProductFeedbacks
                .Where(f => f.ProductId == productId)
                .Select(f => new ProductFeedbackResponseDTO
                {
                    Id = f.Id,
                    ProductId = f.ProductId,
                    Content = f.Content,
                    FeedbackType = f.FeedbackType,
                    CreatedDate = f.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<ProductFeedbackResponseDTO> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.ProductFeedbacks
                .Where(f => f.Id == id)
                .Select(f => new ProductFeedbackResponseDTO
                {
                    Id = f.Id,
                    ProductId = f.ProductId,
                    Content = f.Content,
                    FeedbackType = f.FeedbackType,
                    CreatedDate = f.CreatedDate
                })
                .FirstOrDefaultAsync();

            return feedback;
        }
    }
}
