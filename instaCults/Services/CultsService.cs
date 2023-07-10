namespace instaCults.Services;

public class CultsService
{
    private readonly CultsRepository _repo;

    public CultsService(CultsRepository repo)
    {
        _repo = repo;
    }

    internal Cult CreateCult(Cult cultData)
    {
        Cult newCult = _repo.CreateCult(cultData);
        return newCult;
    }

    internal List<Cult> GetAllCults()
    {
        List<Cult> cults = _repo.GetAllCults();
        return cults;
    }

    internal Cult GetById(int cultId)
    {
        Cult cult = _repo.GetById(cultId);
        if (cult == null) new Exception("Invalid cult id");
        cult.Popularity++;
        _repo.Edit(cult);
        return cult;
    }

    internal List<Cult> SearchCults(string search)
    {
        List<Cult> cults = _repo.SearchCults(search);
        return cults;
    }
}