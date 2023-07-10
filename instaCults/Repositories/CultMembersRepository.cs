namespace instaCults.Repositories;

public class CultMembersRepository
{
    private readonly IDbConnection _db;

    public CultMembersRepository(IDbConnection db)
    {
        _db = db;
    }


    public CultMember Create(CultMember cultMemberData)
    {
        string sql = @"
    INSERT INTO cult_members (cultId, accountId)
    VALUES(@cultId, @accountId);
    SELECT LAST_INSERT_ID()
    ;";
        int lastInsertId = _db.ExecuteScalar<int>(sql, cultMemberData);
        cultMemberData.Id = lastInsertId;
        return cultMemberData;
    }

    internal CultMember GetById(int cultmemberId)
    {
        string sql = @"
        SELECT
        *
        FROM cult_members
WHERE id = @cultmemberId
        ;";
        CultMember cm = _db.Query<CultMember>(sql, new { cultmemberId }).FirstOrDefault();
        return cm;
    }

    internal List<Cultist> GetMembersByCultId(int cultId)
    {
        string sql = @"
      SELECT
      cm.*,
      act.*
      FROM cult_members cm
      JOIN accounts act ON act.id = cm.accountId
      WHERE cm.cultId = @cultId
      ;";
        List<Cultist> cultists = _db.Query<CultMember, Cultist, Cultist>(sql, (cultMember, cultist) =>
        {
            cultist.CultMemberId = cultMember.Id;
            return cultist;
        }, new { cultId }).ToList();
        return cultists;
    }

    public int RemoveCultMember(int cultMemberId)
    {
        string sql = @"
       DELETE FROM cult_members WHERE id = @cultMemberId LIMIT 1;
       ;";
        int rows = _db.Execute(sql, new { cultMemberId });
        return rows;
    }
}