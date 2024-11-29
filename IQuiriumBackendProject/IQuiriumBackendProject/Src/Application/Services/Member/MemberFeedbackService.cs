using IqueiriumBackendProject.Src.Domain.Entities.MemberFeedbackEntities;
using IqueiriumBackendProject.Src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IqueiriumBackendProject.Src.Application.Services.Member
{
    /// <summary>
    /// Serviço para gerenciar feedbacks entre membros.
    /// </summary>
    public class MemberFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public MemberFeedbackService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Envia um feedback entre membros.
        /// </summary>
        public async Task<MemberFeedback> SubmitFeedbackAsync(int senderId, int recipientId, string feedbackType, string content)
        {
            if (string.IsNullOrWhiteSpace(feedbackType))
                throw new ArgumentException("O tipo de feedback não pode ser nulo ou vazio.", nameof(feedbackType));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("O conteúdo do feedback não pode ser nulo ou vazio.", nameof(content));

            if (senderId <= 0 || recipientId <= 0)
                throw new ArgumentException("IDs de remetente e destinatário devem ser válidos.");

            var senderExists = await _context.Users.AnyAsync(u => u.Id == senderId);
            var recipientExists = await _context.Users.AnyAsync(u => u.Id == recipientId);

            if (!senderExists || !recipientExists)
                throw new InvalidOperationException("Remetente ou destinatário não encontrados.");

            var feedback = new MemberFeedback
            {
                SenderId = senderId,
                RecipientId = recipientId,
                FeedbackType = feedbackType,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            _context.MemberFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        /// <summary>
        /// Obtém todos os feedbacks recebidos por um membro específico.
        /// </summary>
        public async Task<IEnumerable<MemberFeedback>> GetFeedbacksByRecipientAsync(int recipientId)
        {
            if (recipientId <= 0)
                throw new ArgumentException("ID do destinatário deve ser válido.", nameof(recipientId));

            return await _context.MemberFeedbacks
                .Where(f => f.RecipientId == recipientId)
                .Include(f => f.Sender)
                .Include(f => f.Recipient)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém os detalhes de um feedback específico pelo ID.
        /// </summary>
        public async Task<MemberFeedback> GetFeedbackByIdAsync(int feedbackId)
        {
            if (feedbackId <= 0)
                throw new ArgumentException("ID do feedback deve ser válido.", nameof(feedbackId));

            var feedback = await _context.MemberFeedbacks
                .Include(f => f.Sender)
                .Include(f => f.Recipient)
                .FirstOrDefaultAsync(f => f.Id == feedbackId);

            if (feedback == null)
                throw new KeyNotFoundException("Feedback não encontrado.");

            return feedback;
        }

        /// <summary>
        /// Reporta um feedback como ofensivo ou impróprio.
        /// </summary>
        public async Task ReportFeedbackAsync(int feedbackId, int reporterId, string reason)
        {
            if (feedbackId <= 0 || reporterId <= 0)
                throw new ArgumentException("IDs de feedback e reportador devem ser válidos.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("A razão do report não pode ser nula ou vazia.", nameof(reason));

            var feedbackExists = await _context.MemberFeedbacks.AnyAsync(f => f.Id == feedbackId);

            if (!feedbackExists)
                throw new InvalidOperationException("Feedback não encontrado para reportar.");

            var report = new MemberFeedbackReport
            {
                MemberFeedbackId = feedbackId,
                ReporterId = reporterId,
                Reason = reason,
                ReportedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.MemberFeedbackReports.Add(report);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um feedback existente.
        /// </summary>
        public async Task<bool> UpdateFeedbackAsync(int feedbackId, string feedbackType, string content)
        {
            if (feedbackId <= 0)
                throw new ArgumentException("ID do feedback deve ser válido.", nameof(feedbackId));

            if (string.IsNullOrWhiteSpace(feedbackType))
                throw new ArgumentException("O tipo de feedback não pode ser nulo ou vazio.", nameof(feedbackType));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("O conteúdo do feedback não pode ser nulo ou vazio.", nameof(content));

            var feedback = await _context.MemberFeedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);

            if (feedback == null)
                return false;

            feedback.FeedbackType = feedbackType;
            feedback.Content = content;

            _context.MemberFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Exclui um feedback pelo ID.
        /// </summary>
        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            if (feedbackId <= 0)
                throw new ArgumentException("ID do feedback deve ser válido.", nameof(feedbackId));

            var feedback = await _context.MemberFeedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);

            if (feedback == null)
                return false;

            _context.MemberFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
