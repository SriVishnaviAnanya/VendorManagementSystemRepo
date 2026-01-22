using Microsoft.AspNetCore.Mvc;

[ApiController]

[Route("api/approval")]

public class ApprovalController : ControllerBase

{

    private readonly ApprovalService _service;

    public ApprovalController(ApprovalService service)

    {

        _service = service;

    }

    [HttpPost("submit")]

    public async Task<IActionResult> Submit([FromBody] SubmitApprovalDto dto)

    {

        await _service.SubmitForApproval(

            dto.ContractId, dto.VendorId,

            dto.VendorName, dto.ContractNumber);

        return Ok("Submitted for approval");

    }

    [HttpPost("approve/{id}")]
    public async Task<IActionResult> Approve(int id, [FromBody] string comments)
    {
        try
        {
            await _service.Approve(id, comments);
            return Ok("Approved");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("reject/{id}")]

    public async Task<IActionResult> Reject(int id, [FromBody] string comments)

    {

        try

        {

            await _service.Reject(id, comments);

            return Ok("Rejected");

        }

        catch (Exception ex)

        {

            return NotFound(ex.Message);

        }

    }


}
