using IqueiriumBackendProject.Src.Application.Dtos.Products;
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IqueiriumBackendProject.Src.Application.Services.Products
{
    public class ProductFeedbackService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do serviço <see cref="ProductFeedbackService"/>.
        /// </summary>
        /// <param name="context">
        /// Instância do <see cref="ApplicationDbContext"/> usada para acessar e manipular os dados de feedbacks no banco de dados.
        /// </param>
        public ProductFeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Submete um novo feedback para um produto e salva no banco de dados.
        /// </summary>
        /// <param name="feedbackDto">
        /// Objeto contendo os dados necessários para criar o feedback, incluindo o ID do produto, 
        /// o conteúdo, o tipo de feedback e o ID do usuário que o submeteu.
        /// </param>
        /// <returns>
        /// Um objeto <see cref="ProductFeedbackResponseDTO"/> contendo os dados do feedback criado.
        /// </returns>
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

        /// <summary>
        /// Obtém todos os feedbacks associados a um produto específico.
        /// </summary>
        /// <param name="productId">
        /// O ID do produto cujos feedbacks devem ser recuperados.
        /// </param>
        /// <returns>
        /// Uma coleção de objetos <see cref="ProductFeedbackResponseDTO"/> contendo os detalhes dos feedbacks do produto especificado.
        /// </returns>
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

        /// <summary>
        /// Obtém um feedback específico com base no ID fornecido.
        /// </summary>
        /// <param name="id">
        /// O ID do feedback que deve ser recuperado.
        /// </param>
        /// <returns>
        /// Um objeto <see cref="ProductFeedbackResponseDTO"/> contendo os detalhes do feedback solicitado,
        /// ou <c>null</c> se nenhum feedback com o ID especificado for encontrado.
        /// </returns>
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
