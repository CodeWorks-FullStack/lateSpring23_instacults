namespace instaCults.Repositories;

public class CultsRepository
{
    private readonly IDbConnection _db;

    public CultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Cult CreateCult(Cult cultData)
    {
        string sql = @"
       INSERT INTO cults (name, description, tags, leaderId)
       VALUES(@name, @description, @tags, @leaderId);

       SELECT
       cult.*,
       act.*
       FROM cults cult
       JOIN accounts act ON act.id = cult.leaderId
       WHERE cult.id = LAST_INSERT_ID()
       ;";
        Cult newCult = _db.Query<Cult, Account, Cult>(sql, (cult, profile) =>
        {
            cult.Leader = profile;
            return cult;
        }, cultData).FirstOrDefault();
        // let newCult = await dbContext.Cults.find({cultId})
        return newCult;
    }

    internal void Edit(Cult cult)
    {
        string sql = @"
       UPDATE cults
       SET
       name = @name,
       description=@description,
       tags = @tags,
       popularity = @popularity
       WHERE id =@id
       ;";
        _db.Execute(sql, cult);
    }

    internal List<Cult> GetAllCults()
    {
        string sql = @"
        SELECT
        cult.*,
        act.*
        FROM cults cult
        JOIN accounts act ON act.id = cult.leaderId        
        ;";
        List<Cult> cults = _db.Query<Cult, Account, Cult>(sql, (cult, profile) =>
        {
            cult.Leader = profile;
            return cult;
        }).ToList();
        return cults;
    }

    internal Cult GetById(int cultId)
    {
        string sql = @"
        SELECT
        cult.*,
        act.*
        FROM cults cult
        JOIN accounts act ON act.id = cult.leaderId
        WHERE cult.id = @cultId
        ;";
        Cult foundCult = _db.Query<Cult, Account, Cult>(sql, (cult, profile) =>
        {
            cult.Leader = profile;
            return cult;
        }, new { cultId }).FirstOrDefault();
        return foundCult;
    }

    internal List<Cult> SearchCults(string search)
    {
        search = '%' + search + '%';
        string sql = @"
       SELECT
       cult.*,
       act.*
       FROM cults cult
       JOIN accounts act ON act.id = cult.leaderId
       WHERE cult.tags LIKE @search
       ;";
        List<Cult> cults = _db.Query<Cult, Account, Cult>(sql, (cult, profile) =>
        {
            cult.Leader = profile;
            return cult;
        }, new { search }).ToList();
        return cults;
    }
}