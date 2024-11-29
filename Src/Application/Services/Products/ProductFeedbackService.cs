using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa os DTOs (Data Transfer Objects) para os produtos
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa as entidades de domínio relacionadas aos feedbacks de produtos
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto de banco de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação de dados

namespace IqueiriumBackendProject.Src.Application.Services.Products // Define o namespace para o serviço de feedback de produtos
{
    public class ProductFeedbackService // Define a classe de serviço para gerenciamento de feedbacks de produtos
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
            _context = context; // Inicializa o contexto com o parâmetro passado
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
            // Criação de um novo objeto ProductFeedback usando os dados do DTO (feedbackDto)
            var feedback = new ProductFeedback
            {
                ProductId = feedbackDto.ProductId, // Define o ID do produto
                Content = feedbackDto.Content, // Define o conteúdo do feedback
                FeedbackType = feedbackDto.FeedbackType, // Define o tipo do feedback (positivo, negativo, etc.)
                CreatedDate = DateTime.UtcNow, // Define a data de criação como a data e hora atual em UTC
                UserId = feedbackDto.UserId, // Define o ID do usuário que está enviando o feedback
            };

            // Adiciona o feedback à tabela de feedbacks no banco de dados
            _context.ProductFeedbacks.Add(feedback);
            // Salva as mudanças no banco de dados de forma assíncrona
            await _context.SaveChangesAsync();

            // Retorna um DTO com os dados do feedback recém-criado para a resposta
            return new ProductFeedbackResponseDTO
            {
                Id = feedback.Id, // Retorna o ID do feedback
                ProductId = feedback.ProductId, // Retorna o ID do produto relacionado ao feedback
                Content = feedback.Content, // Retorna o conteúdo do feedback
                FeedbackType = feedback.FeedbackType, // Retorna o tipo de feedback
                CreatedDate = feedback.CreatedDate // Retorna a data de criação do feedback
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
            // Consulta o banco de dados para retornar os feedbacks do produto específico (productId)
            return await _context.ProductFeedbacks
                .Where(f => f.ProductId == productId) // Filtra os feedbacks pelo ID do produto
                .Select(f => new ProductFeedbackResponseDTO // Cria um DTO para cada feedback
                {
                    Id = f.Id, // Mapeia o ID do feedback
                    ProductId = f.ProductId, // Mapeia o ID do produto
                    Content = f.Content, // Mapeia o conteúdo do feedback
                    FeedbackType = f.FeedbackType, // Mapeia o tipo de feedback
                    CreatedDate = f.CreatedDate // Mapeia a data de criação do feedback
                })
                .ToListAsync(); // Executa a consulta e retorna os resultados como uma lista assíncrona
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
            // Consulta o banco de dados para buscar um feedback específico pelo ID
            var feedback = await _context.ProductFeedbacks
                .Where(f => f.Id == id) // Filtra os feedbacks pelo ID fornecido
                .Select(f => new ProductFeedbackResponseDTO // Cria um DTO para o feedback encontrado
                {
                    Id = f.Id, // Mapeia o ID do feedback
                    ProductId = f.ProductId, // Mapeia o ID do produto
                    Content = f.Content, // Mapeia o conteúdo do feedback
                    FeedbackType = f.FeedbackType, // Mapeia o tipo de feedback
                    CreatedDate = f.CreatedDate // Mapeia a data de criação do feedback
                })
                .FirstOrDefaultAsync(); // Retorna o primeiro feedback encontrado ou null caso não haja

            return feedback; // Retorna o feedback encontrado (ou null caso não exista)
        }
    }
}
