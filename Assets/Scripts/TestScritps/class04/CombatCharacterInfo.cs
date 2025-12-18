using System.Collections.Generic;

[System.Serializable]
public class CombatCharacterInfo
{
    public string name;
    public float maxHp;
    public float currentHp;
    public int attackPower;
    public int defensePower;
    public List<CombatSkill> skillList;

    public CombatCharacterInfo(string name, float hp, int atk, int def)
    {
        this.name = name;
        this.maxHp = hp;
        this.currentHp = hp;
        this.attackPower = atk;
        this.defensePower = def;
        this.skillList = new List<CombatSkill>();
    }
}
