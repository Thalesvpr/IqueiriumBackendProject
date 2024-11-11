using IqueiriumBackendProject.Src.Application.Dtos.Products; // Importa os DTOs (Data Transfer Objects) relacionados à análise de feedback de produtos
using IqueiriumBackendProject.Src.Domain.Entities.ProductEntities; // Importa as entidades de domínio relacionadas à análise de feedbacks de produtos
using IqueiriumBackendProject.Src.Infrastructure.Data; // Importa o contexto do banco de dados da aplicação
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para manipulação de dados no banco

namespace IqueiriumBackendProject.Src.Application.Services.Products // Define o namespace do serviço de análise de feedback de produtos
{
    public class ProductFeedbackAnalysisService // Define a classe de serviço para a análise de feedbacks de produtos
    {
        private readonly ApplicationDbContext _context; // Declara uma variável para armazenar o contexto do banco de dados

        public ProductFeedbackAnalysisService(ApplicationDbContext context) // Construtor da classe de serviço, que recebe o contexto do banco de dados
        {
            _context = context; // Atribui o contexto recebido à variável _context
        }

        public async Task<List<ProductFeedbackAnalysisResponseDto>> AnalyzeFeedback(ProductFeedbackAnalysisCreateDto analysisDto) // Método para analisar feedbacks, recebe um DTO de criação de análise
        {
            var analyses = new List<ProductFeedbackAnalysisResponseDto>(); // Cria uma lista para armazenar as respostas das análises

            foreach (var feedbackId in analysisDto.ProductFeedbackIds) // Itera sobre os IDs dos feedbacks fornecidos no DTO
            {
                var analysis = new ProductFeedbackAnalysis // Cria uma nova instância de análise de feedback
                {
                    Content = analysisDto.Content, // Atribui o conteúdo da análise
                    AnalystUserId = analysisDto.AnalystUserId, // Atribui o ID do analista
                    ProductFeedbackId = feedbackId // Atribui o ID do feedback que será analisado
                };

                _context.ProductFeedbackAnalyses.Add(analysis); // Adiciona a análise ao contexto para persistir no banco
                await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados de forma assíncrona

                analyses.Add(new ProductFeedbackAnalysisResponseDto // Adiciona a análise criada à lista de respostas
                {
                    Id = analysis.Id, // Atribui o ID da análise
                    ProductFeedbackId = feedbackId, // Atribui o ID do feedback
                    Content = analysis.Content, // Atribui o conteúdo da análise
                    CreatedDate = analysis.CreatedDate // Atribui a data de criação da análise
                });
            }

            return analyses; // Retorna a lista de respostas de análise
        }

        public async Task<List<ProductFeedbackAnalysisResponseDto>> GetFeedbackAnalyses(int feedbackId) // Método para obter as análises de um feedback específico
        {
            return await _context.ProductFeedbackAnalyses // Consulta o banco de dados para obter as análises associadas ao ID do feedback
                .Where(pfa => pfa.ProductFeedbackId == feedbackId) // Filtra as análises pelo ID do feedback
                .Select(pfa => new ProductFeedbackAnalysisResponseDto // Mapeia as análises para o DTO de resposta
                {
                    Id = pfa.Id, // Atribui o ID da análise
                    ProductFeedbackId = feedbackId, // Atribui o ID do feedback associado
                    Content = pfa.Content, // Atribui o conteúdo da análise
                    CreatedDate = pfa.CreatedDate, // Atribui a data de criação da análise
                    AnalystUserId = pfa.AnalystUserId // Atribui o ID do analista responsável pela análise
                })
                .ToListAsync(); // Converte a consulta para uma lista de resultados e executa de forma assíncrona
        }
    }
}
