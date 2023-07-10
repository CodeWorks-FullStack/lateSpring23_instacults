namespace instaCults.Services;

public class CultMembersService
{
    private readonly CultMembersRepository _repo;
    private readonly CultsService _cultsService;

    public CultMembersService(CultMembersRepository repo, CultsService cultsService)
    {
        _repo = repo;
        _cultsService = cultsService;
    }

    public CultMember Create(CultMember cultMemberData)
    {
        CultMember newCultMember = _repo.Create(cultMemberData);
        return newCultMember;
    }

    internal List<Cultist> GetMembersByCultId(int cultId)
    {
        List<Cultist> cultists = _repo.GetMembersByCultId(cultId);
        return cultists;
    }

    internal CultMember GetById(int cultmemberId)
    {
        CultMember cm = _repo.GetById(cultmemberId);
        if (cm == null) new Exception("Invalid id");
        return cm;
    }

    internal string RemoveCultMember(int cultMemberId, string userId)
    {

        CultMember cultMember = GetById(cultMemberId);
        // NOTE remove the membership if I am the cult member
        // if (cultMember.AccountId != userId) new Exception("Unauthorized to remove!");

        Cult foundCult = _cultsService.GetById(cultMember.CultId);
        if (foundCult.LeaderId != userId || cultMember.AccountId != userId) new Exception("That ain't your cult");

        int rows = _repo.RemoveCultMember(cultMemberId);
        if (rows > 1) new Exception("Something went WRONG");
        return "Successfully removed membership";
    }
}