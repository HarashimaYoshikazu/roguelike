interface ISkill
{
    SkillDef SkillId { get; }
    void SetUp();
    void Update();
    void LevelUp();
}
//TODO　ひとまずEnumを使うが他の方法を模索したい
public enum SkillDef
{
    Invalid = 0,
    NetAttack =1,
    Bullet = 2
}

