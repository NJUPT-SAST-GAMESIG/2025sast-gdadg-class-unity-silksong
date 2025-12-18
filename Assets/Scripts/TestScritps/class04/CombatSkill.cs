[System.Serializable]
public class CombatSkill
{
    public string skillName;
    public int attackPower;

    public CombatSkill(string name, int power)
    {
        this.skillName = name;
        this.attackPower = power;
    }
}
