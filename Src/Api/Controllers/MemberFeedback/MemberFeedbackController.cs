using IqueiriumBackendProject.Src.Application.Dtos.MemberFeedback;
using IqueiriumBackendProject.Src.Application.Services.Member;
using Microsoft.AspNetCore.Mvc;

namespace IqueiriumBackendProject.Src.Api.Controllers.MemberFeedbacks
{
    [Route("api/member-feedback")]
    [ApiController]
    public class MemberFeedbackController : ControllerBase
    {
        private readonly MemberFeedbackService _memberFeedbackService;

        /// <summary>
        /// Inicializa uma nova instância do controlador de feedbacks entre membros.
        /// </summary>
        /// <param name="memberFeedbackService">Serviço de manipulação de feedbacks entre membros.</param>
        public MemberFeedbackController(MemberFeedbackService memberFeedbackService)
        {
            _memberFeedbackService = memberFeedbackService;
        }

        /// <summary>
        /// Obtém todos os feedbacks recebidos por um membro específico.
        /// 
        /// </summary>
        /// <param name="recipientId">ID do membro que recebeu os feedbacks.</param>
        /// <returns>
        /// Uma resposta HTTP 200 com a lista de feedbacks recebidos.
        /// </returns>
        [HttpGet("recipient/{recipientId}")]
        public async Task<IActionResult> GetByRecipient(int recipientId)
        {
            var feedbacks = await _memberFeedbackService.GetFeedbacksByRecipientAsync(recipientId);
            return Ok(feedbacks);
        }

        /// <summary>
        /// Obtém os detalhes de um feedback específico pelo ID.
        /// </summary>
        /// <param name="id">ID do feedback.</param>
        /// <returns>
        /// Uma resposta HTTP 200 com os dados do feedback correspondente ao ID informado.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _memberFeedbackService.GetFeedbackByIdAsync(id);
            return feedback != null ? Ok(feedback) : NotFound("Feedback não encontrado.");
        }

        /// <summary>
        /// Envia um feedback entre membros.
        /// </summary>
        /// <param name="feedbackDto">Objeto contendo os dados do feedback a ser criado.</param>
        /// <returns>
        /// Uma resposta HTTP 201 com os dados do feedback criado.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] MemberFeedbackCreateDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedback = await _memberFeedbackService.SubmitFeedbackAsync(
                feedbackDto.SenderId,
                feedbackDto.RecipientId,
                feedbackDto.FeedbackType,
                feedbackDto.Content
            );

            return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
        }

        /// <summary>
        /// Reporta um feedback como ofensivo ou impróprio.
        /// </summary>
        /// <param name="reportDto">Objeto contendo os dados do report.</param>
        /// <returns>
        /// Uma resposta HTTP 204 indicando que o report foi realizado com sucesso.
        /// </returns>
        [HttpPost("report")]
        public async Task<IActionResult> ReportFeedback([FromBody] MemberFeedbackReportCreateDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _memberFeedbackService.ReportFeedbackAsync(
                reportDto.MemberFeedbackId,
                reportDto.ReporterId,
                reportDto.Reason
            );

            return NoContent();
        }


        /// <summary>
        /// Atualiza um feedback existente.
        /// </summary>
        /// <param name="id">ID do feedback a ser atualizado.</param>
        /// <param name="updateDto">Objeto contendo os dados atualizados do feedback.</param>
        /// <returns>
        /// Uma resposta HTTP 204 indicando que a operação foi bem-sucedida.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] MemberFeedbackUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _memberFeedbackService.UpdateFeedbackAsync(
                id,
                updateDto.FeedbackType,
                updateDto.Content
            );

            return updated ? NoContent() : NotFound("Feedback não encontrado.");
        }


        /// <summary>
        /// Exclui um feedback pelo ID.
        /// </summary>
        /// <param name="id">ID do feedback a ser excluído.</param>
        /// <returns>
        /// Uma resposta HTTP 204 indicando que a operação foi bem-sucedida.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var deleted = await _memberFeedbackService.DeleteFeedbackAsync(id);
            return deleted ? NoContent() : NotFound("Feedback não encontrado.");
        }
    }
}
