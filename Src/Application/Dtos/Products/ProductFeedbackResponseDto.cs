namespace IqueiriumBackendProject.Src.Application.Dtos.Products
{
    /// <summary>
    /// DTO usado para retornar as informações de um feedback de produto.
    /// </summary>
    public class ProductFeedbackResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public string FeedbackType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
