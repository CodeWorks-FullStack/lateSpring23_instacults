namespace instaCults.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CultsController : ControllerBase
{
    private readonly CultsService _cultsService;
    private readonly Auth0Provider _auth0;
    private readonly CultMembersService _cultMembersService;

    public CultsController(CultsService cultsService, Auth0Provider auth0, CultMembersService cultMembersService)
    {
        _cultsService = cultsService;
        _auth0 = auth0;
        _cultMembersService = cultMembersService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Cult>> CreateCult([FromBody] Cult cultData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            cultData.LeaderId = userInfo.Id;
            Cult newCult = _cultsService.CreateCult(cultData);
            return Ok(newCult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Cult>> GetAllCults(string search)
    {
        try
        {
            List<Cult> cults;
            if (search == null)
            {
                cults = _cultsService.GetAllCults();
            }
            else
            {
                cults = _cultsService.SearchCults(search);
            }

            return Ok(cults);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{cultId}")]
    public ActionResult<Cult> GetById(int cultId)
    {
        try
        {
            Cult cult = _cultsService.GetById(cultId);
            return Ok(cult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{cultId}/cultmembers")]
    [Authorize]
    public ActionResult<List<Cultist>> GetMembersByCultId(int cultId)
    {
        try
        {
            List<Cultist> cultists = _cultMembersService.GetMembersByCultId(cultId);
            return cultists;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}