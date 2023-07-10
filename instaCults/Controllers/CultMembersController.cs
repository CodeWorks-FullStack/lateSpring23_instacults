namespace instaCults.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CultMembersController : ControllerBase
{
    private readonly CultMembersService _cultMembersService;
    private readonly Auth0Provider _auth0;

    public CultMembersController(CultMembersService cultMembersService, Auth0Provider auth0)
    {
        _cultMembersService = cultMembersService;
        _auth0 = auth0;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CultMember>> CreateCultMember([FromBody] CultMember cultMemberData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            cultMemberData.AccountId = userInfo.Id;
            CultMember newCultMember = _cultMembersService.Create(cultMemberData);
            return Ok(newCultMember);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{cultMemberId}")]
    [Authorize]

    public async Task<ActionResult<string>> RemoveCultMember(int cultMemberId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string message = _cultMembersService.RemoveCultMember(cultMemberId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}