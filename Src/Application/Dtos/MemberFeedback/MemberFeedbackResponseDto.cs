﻿namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    /// <summary>
    /// DTO usado para retornar informações sobre o feedback de um membro.
    /// </summary>
    public class MemberFeedbackResponseDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string FeedbackType { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}