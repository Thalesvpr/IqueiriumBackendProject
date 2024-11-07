namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    public class ProductFeedbackAnalysisResponseDto
    {
        public int Id { get; set; }
        public int ProductFeedbackId { get; set; }

        public int AnalystUserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
