namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    public class ProductFeedbackResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public string FeedbackType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
