namespace IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback
{
    /// <summary>
    /// DTO usado para retornar informações sobre um relatório de feedback de membro.
    /// </summary>
    public class MemberFeedbackReportResponseDto
    {
        public int Id { get; set; }
        public int MemberFeedbackId { get; set; }
        public int ReporterId { get; set; }
        public string ReporterName { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime ReportedAt { get; set; }
    }
}
